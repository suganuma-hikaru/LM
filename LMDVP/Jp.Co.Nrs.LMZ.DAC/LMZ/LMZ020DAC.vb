' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ020DAC : 商品マスタ照会
'  作  成  者       :  小林
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(MG.GOODS_CD_NRS)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' CUST_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                                                                          " & vbNewLine _
                                            & "     MG.NRS_BR_CD                                                                                               AS NRS_BR_CD                " & vbNewLine _
                                            & "    ,MG.GOODS_NM_1                                                                                              AS GOODS_NM_1               " & vbNewLine _
                                            & "    ,(SELECT KBN_NM1 FROM LM_MST..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'I001' AND MG.STD_IRIME_UT = ZK.KBN_CD)      AS STD_IRIME_NM             " & vbNewLine _
                                            & "    ,CASE WHEN @GOODS_CD_CUST_SEMI_DISP_FLG = '1' THEN ISNULL(MGD.SET_NAIYO,'')                                                             " & vbNewLine _
                                            & "          ELSE MG.GOODS_CD_CUST                                                                                                             " & vbNewLine _
                                            & "     END                                                                                                        AS GOODS_CD_CUST            " & vbNewLine _
                                            & "    ,(SELECT KBN_NM1 FROM LM_MST..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'K002' AND MG.NB_UT = ZK.KBN_CD)             AS NB_UT_NM                 " & vbNewLine _
                                            & "    ,MG.NB_UT                                                                                                   AS NB_UT                    " & vbNewLine _
                                            & "    ,(SELECT KBN_NM1 FROM LM_MST..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'N001' AND MG.PKG_UT = ZK.KBN_CD)            AS PKG_NM                   " & vbNewLine _
                                            & "    ,MG.SEARCH_KEY_1                                                                                            AS SEARCH_KEY_1             " & vbNewLine _
                                            & "    ,MG.SEARCH_KEY_2                                                                                            AS SEARCH_KEY_2             " & vbNewLine _
                                            & "    ,(SELECT KBN_NM1 FROM LM_MST..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'O002' AND MG.ONDO_KB = ZK.KBN_CD)           AS ONDO_KB_NM               " & vbNewLine _
                                            & "    ,MG.ONDO_KB                                                                                                 AS ONDO_KB                  " & vbNewLine _
                                            & "    ,MG.SHOBO_CD                                                                                                AS SHOBO_CD                 " & vbNewLine _
                                            & "    ,MC.CUST_NM_S                                                                                               AS CUST_NM_S                " & vbNewLine _
                                            & "    ,MC.CUST_NM_SS                                                                                              AS CUST_NM_SS               " & vbNewLine _
                                            & "    ,MG.STD_IRIME_NB                                                                                            AS STD_IRIME_NB             " & vbNewLine _
                                            & "    ,MG.STD_IRIME_UT                                                                                            AS STD_IRIME_UT             " & vbNewLine _
                                            & "    ,(SELECT KBN_NM1 FROM LM_MST..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'I001' AND MG.STD_IRIME_UT = ZK.KBN_CD)      AS STD_IRIME_UT_NM          " & vbNewLine _
                                            & "    ,MG.PKG_NB                                                                                                  AS PKG_NB                   " & vbNewLine _
                                            & "    ,MG.PKG_UT                                                                                                  AS PKG_UT                   " & vbNewLine _
                                            & "    ,(SELECT KBN_NM1 FROM LM_MST..Z_KBN ZK WHERE ZK.KBN_GROUP_CD = 'N001' AND MG.PKG_UT = ZK.KBN_CD)            AS PKG_UT_NM                " & vbNewLine _
                                            & "    ,MG.CUST_CD_S                                                                                               AS CUST_CD_S                " & vbNewLine _
                                            & "    ,MG.CUST_CD_SS                                                                                              AS CUST_CD_SS               " & vbNewLine _
                                            & "    ,MG.CUST_CD_L                                                                                               AS CUST_CD_L                " & vbNewLine _
                                            & "    ,MG.CUST_CD_M                                                                                               AS CUST_CD_M                " & vbNewLine _
                                            & "    ,MC.CUST_NM_L                                                                                               AS CUST_NM_L                " & vbNewLine _
                                            & "    ,MC.CUST_NM_M                                                                                               AS CUST_NM_M                " & vbNewLine _
                                            & "    ,MG.GOODS_CD_NRS                                                                                            AS GOODS_CD_NRS             " & vbNewLine _
                                            & "    ,MG.GOODS_NM_2                                                                                              AS GOODS_NM_2               " & vbNewLine _
                                            & "    ,MG.UP_GP_CD_1                                                                                              AS UP_GP_CD_1               " & vbNewLine _
                                            & "    ,MG.KIKEN_KB                                                                                                AS KIKEN_KB                 " & vbNewLine _
                                            & "    ,MG.DOKU_KB                                                                                                 AS DOKU_KB                  " & vbNewLine _
                                            & "    ,MG.UNSO_ONDO_KB                                                                                            AS UNSO_ONDO_KB             " & vbNewLine _
                                            & "    ,MG.ONDO_STR_DATE                                                                                           AS ONDO_STR_DATE            " & vbNewLine _
                                            & "    ,MG.ONDO_END_DATE                                                                                           AS ONDO_END_DATE            " & vbNewLine _
                                            & "    ,MG.STD_WT_KGS                                                                                              AS STD_WT_KGS               " & vbNewLine _
                                            & "    ,MG.INKA_KAKO_SAGYO_KB_1                                                                                    AS INKA_KAKO_SAGYO_KB_1     " & vbNewLine _
                                            & "    ,MG.INKA_KAKO_SAGYO_KB_2                                                                                    AS INKA_KAKO_SAGYO_KB_2     " & vbNewLine _
                                            & "    ,MG.INKA_KAKO_SAGYO_KB_3                                                                                    AS INKA_KAKO_SAGYO_KB_3     " & vbNewLine _
                                            & "    ,MG.INKA_KAKO_SAGYO_KB_4                                                                                    AS INKA_KAKO_SAGYO_KB_4     " & vbNewLine _
                                            & "    ,MG.INKA_KAKO_SAGYO_KB_5                                                                                    AS INKA_KAKO_SAGYO_KB_5     " & vbNewLine _
                                            & "    ,MG.TARE_YN                                                                                                 AS TARE_YN                  " & vbNewLine _
                                            & "    ,MG.SP_NHS_YN                                                                                               AS SP_NHS_YN                " & vbNewLine _
                                            & "    ,MG.COA_YN                                                                                                  AS COA_YN                   " & vbNewLine _
                                            & "    ,MG.LOT_CTL_KB                                                                                              AS LOT_CTL_KB               " & vbNewLine _
                                            & "    ,MG.LT_DATE_CTL_KB                                                                                          AS LT_DATE_CTL_KB           " & vbNewLine _
                                            & "    ,MG.CRT_DATE_CTL_KB                                                                                         AS CRT_DATE_CTL_KB          " & vbNewLine _
                                            & "    ,MG.SKYU_MEI_YN                                                                                             AS SKYU_MEI_YN              " & vbNewLine _
                                            & "    ,MG.HIKIATE_ALERT_YN                                                                                        AS HIKIATE_ALERT_YN         " & vbNewLine _
                                            & "    ,CASE WHEN MG.UNSO_HOKEN_YN = '01'  THEN  MG.KITAKU_GOODS_UP                                                                            " & vbNewLine _
                                            & "                                        ELSE  0            END                                                  AS KITAKU_GOODS_UP           " & vbNewLine


#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                                                                                          " & vbNewLine _
                                            & "    $LM_MST$..M_GOODS MG                                                                                                                    " & vbNewLine _
                                            & "    LEFT JOIN $LM_MST$..M_CUST MC                                                                                                           " & vbNewLine _
                                            & "    ON MG.NRS_BR_CD = MC.NRS_BR_CD                                                                                                          " & vbNewLine _
                                            & "    AND MG.CUST_CD_L = MC.CUST_CD_L                                                                                                         " & vbNewLine _
                                            & "    AND MG.CUST_CD_M = MC.CUST_CD_M                                                                                                         " & vbNewLine _
                                            & "    AND MG.CUST_CD_S = MC.CUST_CD_S                                                                                                         " & vbNewLine _
                                            & "    AND MG.CUST_CD_SS = MC.CUST_CD_SS                                                                                                       " & vbNewLine

    Private Const SQL_FROM_DATA_DETAIL_DISP As String _
                                            = "    LEFT JOIN                                                                                                                               " & vbNewLine _
                                            & "    $LM_MST$..M_GOODS_DETAILS MGD                                                                                                           " & vbNewLine _
                                            & "    ON  MGD.NRS_BR_CD    = MG.NRS_BR_CD                                                                                                     " & vbNewLine _
                                            & "    AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                                                                                                  " & vbNewLine _
                                            & "    AND MGD.SUB_KB       = '65'                                                                                                             " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                               " & vbNewLine _
                                         & "     GOODS_NM_1,GOODS_CD_CUST,GOODS_CD_NRS      " & vbNewLine

#End Region


#End Region
#End Region

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

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ020IN")
        Dim ediCustTbl As DataTable = ds.Tables("LMZ020OUT_EDI_CUST")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMZ020DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMZ020DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Me._StrSql.Append(LMZ020DAC.SQL_FROM_DATA_DETAIL_DISP)      'SQL構築(カウント用from句)

        Call Me.SetConditionMasterSQL(ediCustTbl)                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタデータ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ020IN")
        Dim ediCustTbl As DataTable = ds.Tables("LMZ020OUT_EDI_CUST")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL(SELECT句)
        Me._StrSql.Append(LMZ020DAC.SQL_SELECT_DATA)

        'SQL(FROM句)
        Me._StrSql.Append(LMZ020DAC.SQL_FROM_DATA)
        Me._StrSql.Append(LMZ020DAC.SQL_FROM_DATA_DETAIL_DISP)
        
        '条件設定
        Call Me.SetConditionMasterSQL(ediCustTbl)

        Me._StrSql.Append(LMZ020DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ020DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("STD_IRIME_NM", "STD_IRIME_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NM", "PKG_NM")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("ONDO_KB_NM", "ONDO_KB_NM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_IRIME_UT_NM", "STD_IRIME_UT_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PKG_UT_NM", "PKG_UT_NM")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("KIKEN_KB", "KIKEN_KB")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("INKA_KAKO_SAGYO_KB_1", "INKA_KAKO_SAGYO_KB_1")
        map.Add("INKA_KAKO_SAGYO_KB_2", "INKA_KAKO_SAGYO_KB_2")
        map.Add("INKA_KAKO_SAGYO_KB_3", "INKA_KAKO_SAGYO_KB_3")
        map.Add("INKA_KAKO_SAGYO_KB_4", "INKA_KAKO_SAGYO_KB_4")
        map.Add("INKA_KAKO_SAGYO_KB_5", "INKA_KAKO_SAGYO_KB_5")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("SP_NHS_YN", "SP_NHS_YN")
        map.Add("COA_YN", "COA_YN")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")
        map.Add("LT_DATE_CTL_KB", "LT_DATE_CTL_KB")
        map.Add("CRT_DATE_CTL_KB", "CRT_DATE_CTL_KB")
        map.Add("SKYU_MEI_YN", "SKYU_MEI_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")    'ADD 2022/01/12 026832

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ020OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal ediCustTbl As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append("WHERE MG.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '要望番号1604 2012/11/16 本明追加 START
            Me._StrSql.Append("AND MG.SYS_DEL_FLG = '0'")
            Me._StrSql.Append(vbNewLine)
            '要望番号1604 2012/11/16 本明追加 START

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.GOODS_NM_1 LIKE @GOODS_NM_1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("GOODS_CD_CUST").ToString()
            If ediCustTbl.Rows.Count > 0 AndAlso "1".Equals(ediCustTbl.Rows(0).Item("FLAG_19").ToString) Then
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append("AND MGD.SET_NAIYO LIKE @GOODS_CD_CUST")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                End If
            Else
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append("AND MG.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
                Else
                    Me._StrSql.Append("AND MG.GOODS_CD_CUST <> '' ")
                    Me._StrSql.Append(vbNewLine)
#End If
                End If

            End If

            whereStr = .Item("IRIME").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.STD_IRIME_NB = @IRIME")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", whereStr, DBDataType.NUMERIC))
            End If

            whereStr = .Item("IRIME_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.STD_IRIME_UT = @IRIME_UT")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("NB_UT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.NB_UT = @NB_UT")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEARCH_KEY_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.SEARCH_KEY_1 LIKE @SEARCH_KEY_1")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_1", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SEARCH_KEY_2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.SEARCH_KEY_2 LIKE @SEARCH_KEY_2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.ONDO_KB = @ONDO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SHOBO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.SHOBO_CD LIKE @SHOBO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHOBO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MC.CUST_NM_S LIKE @CUST_NM_S")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_S", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_NM_SS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append("AND MG.CUST_NM_SS LIKE @CUST_NM_SS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_SS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If ediCustTbl.Rows.Count > 0 AndAlso "1".Equals(ediCustTbl.Rows(0).Item("FLAG_19").ToString) Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST_SEMI_DISP_FLG", ediCustTbl.Rows(0).Item("FLAG_19").ToString, DBDataType.NVARCHAR))
            Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST_SEMI_DISP_FLG", String.Empty, DBDataType.NVARCHAR))
            End If

#If True Then       'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
            '使用可能か未設定時対象
            Me._StrSql.Append("AND MG.AVAL_YN IN ('01','')")
            Me._StrSql.Append(vbNewLine)

#End If
        End With

    End Sub


#End Region

#Region "EDI荷主マスタ取得"
    ''' <summary>
    ''' EDI荷主マスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectEdiCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMZ020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append("SELECT FLAG_19                         " & vbNewLine)
        Me._StrSql.Append("FROM $LM_MST$..M_EDI_CUST              " & vbNewLine)
        Me._StrSql.Append("WHERE                                  " & vbNewLine)
        Me._StrSql.Append("     INOUT_KB    = @INOUT_KB           " & vbNewLine)
        Me._StrSql.Append(" AND SYS_DEL_FLG = '0'                 " & vbNewLine)
        Me._StrSql.Append(" AND NRS_BR_CD   = @NRS_BR_CD          " & vbNewLine)
        Me._StrSql.Append(" AND CUST_CD_L   = @CUST_CD_L          " & vbNewLine)
        Me._StrSql.Append(" AND CUST_CD_M   = @CUST_CD_M          " & vbNewLine)

        '条件設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me._Row.Item("EDI_INOUT_KB").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString, DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMZ020DAC", "SelectEdiCustData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("FLAG_19", "FLAG_19")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMZ020OUT_EDI_CUST")

        Return ds



    End Function

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

End Class
