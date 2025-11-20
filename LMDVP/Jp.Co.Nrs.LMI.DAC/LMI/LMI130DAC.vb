' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI130DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI130DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

#End Region

#Region "追加対象データの検索"

#Region "追加対象データの検索 SQL SELECT句"

    ''' <summary>
    ''' 追加対象データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ADDDATA As String = " SELECT                                                                  " & vbNewLine _
                                               & " OUTKAL.NRS_BR_CD                      AS NRS_BR_CD                      " & vbNewLine _
                                               & ",OUTKAL.OUTKA_NO_L                     AS OUTKA_NO_L                     " & vbNewLine _
                                               & ",OUTKAM.OUTKA_NO_M                     AS OUTKA_NO_M                     " & vbNewLine _
                                               & ",OUTKAS.OUTKA_NO_S                     AS OUTKA_NO_S                     " & vbNewLine _
                                               & ",OUTKAL.OUTKA_PLAN_DATE                AS OUTKA_PLAN_DATE                " & vbNewLine _
                                               & ",ZAI.WH_CD                             AS WH_CD                          " & vbNewLine _
                                               & ",OUTKAL.CUST_CD_L                      AS CUST_CD_L                      " & vbNewLine _
                                               & ",OUTKAL.CUST_CD_M                      AS CUST_CD_M                      " & vbNewLine _
                                               & ",OUTKAL.DEST_CD                        AS DEST_CD                        " & vbNewLine _
                                               & ",CASE WHEN OUTKAL.DEST_KB = '01'                                         " & vbNewLine _
                                               & "      THEN OUTKAL.DEST_NM                                                " & vbNewLine _
                                               & "      ELSE DEST.DEST_NM                                                  " & vbNewLine _
                                               & " END AS DEST_NM                                                          " & vbNewLine _
                                               & ",GOODS.GOODS_CD_NRS                    AS GOODS_CD_NRS                   " & vbNewLine _
                                               & ",GOODS.GOODS_CD_CUST                   AS GOODS_CD_CUST                  " & vbNewLine _
                                               & ",GOODS.GOODS_NM_2                      AS GOODS_NM_1                     " & vbNewLine _
                                               & ",GOODS.GOODS_NM_3                      AS GOODS_NM_2                     " & vbNewLine _
                                               & ",CASE WHEN LEN(EDIM.FREE_C01) >= 8 THEN                                 " & vbNewLine _
                                               & "     CASE WHEN ISDATE(SUBSTRING(EDIM.FREE_C01,1,6) + '01') = 1 THEN SUBSTRING(EDIM.FREE_C01,1,6) + '01' " & vbNewLine _
                                               & "     ELSE '' END                                                         " & vbNewLine _
                                               & " ELSE '' END  AS LT_DATE                                                " & vbNewLine _
                                               & ",ZAI.LOT_NO                            AS LOT_NO                         " & vbNewLine _
                                               & ",OUTKAS.ALCTD_NB                       AS ALCTD_NB                       " & vbNewLine _
                                               & ",GOODS.CUST_CD_L                       AS CUST_CD_L                      " & vbNewLine _
                                               & ",GOODS.CUST_CD_M                       AS CUST_CD_M                      " & vbNewLine _
                                               & ",GOODS.CUST_CD_S                       AS CUST_CD_S                      " & vbNewLine _
                                               & ",GOODS.CUST_CD_SS                      AS CUST_CD_SS                     " & vbNewLine _
                                               & ",CUST.CUST_NM_L                        AS CUST_NM_L                      " & vbNewLine _
                                               & ",CUST.CUST_NM_M                        AS CUST_NM_M                      " & vbNewLine _
                                               & ",CUST.CUST_NM_S                        AS CUST_NM_S                      " & vbNewLine _
                                               & ",CUST.CUST_NM_SS                       AS CUST_NM_SS                     " & vbNewLine _
                                               & ",EDIM.FREE_C06                         AS GOODS_SYUBETU                  " & vbNewLine

#End Region

#Region "追加対象データの検索 SQL FROM句"

    ''' <summary>
    ''' 追加対象データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ADDDATA1 As String = "FROM                                                               " & vbNewLine _
                                                     & "$LM_TRN$..C_OUTKA_L OUTKAL                                         " & vbNewLine

    ''' <summary>
    ''' 追加対象データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ADDDATA2 As String = "INNER JOIN                                                         " & vbNewLine _
                                                     & "$LM_TRN$..D_ZAI_TRS ZAI                                            " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "ZAI.NRS_BR_CD = OUTKAS.NRS_BR_CD                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "ZAI.ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                 " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "ZAI.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_MST$..M_GOODS GOODS                                            " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                 " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                           " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_MST$..M_DEST DEST                                              " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                  " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                  " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "DEST.DEST_CD = OUTKAL.DEST_CD                                      " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_MST$..M_CUST CUST                                              " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "CUST.NRS_BR_CD = GOODS.NRS_BR_CD                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_L = GOODS.CUST_CD_L                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_M = GOODS.CUST_CD_M                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_S = GOODS.CUST_CD_S                                   " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                 " & vbNewLine _
                                                     & "LEFT JOIN                                                          " & vbNewLine _
                                                     & "$LM_TRN$..H_OUTKAEDI_M EDIM                                        " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "OUTKAM.NRS_BR_CD = EDIM.NRS_BR_CD                                  " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "OUTKAM.OUTKA_NO_L = EDIM.OUTKA_CTL_NO                              " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "OUTKAM.OUTKA_NO_M = EDIM.OUTKA_CTL_NO_CHU                          " & vbNewLine

#End Region

#Region "追加対象データの検索 SQL ORDER BY句"

    ''' <summary>
    ''' 追加対象データの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_ADDDATA As String = "ORDER BY                                                           " & vbNewLine _
                                                     & " OUTKAL.OUTKA_NO_L                                                 " & vbNewLine _
                                                     & ",OUTKAM.OUTKA_NO_M                                                 " & vbNewLine _
                                                     & ",OUTKAS.OUTKA_NO_S                                                 " & vbNewLine

#End Region

#Region "作業マスタ検索"
    ''' <summary>
    ''' 作業マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_M_SAGYO As String _
        = " SELECT                             " & vbNewLine _
        & " 	NRS_BR_CD                      " & vbNewLine _
        & " 	,SAGYO_CD                      " & vbNewLine _
        & " 	,SAGYO_NM                      " & vbNewLine _
        & " 	,INV_TANI                      " & vbNewLine _
        & " 	,KOSU_BAI                      " & vbNewLine _
        & " 	,SAGYO_UP                      " & vbNewLine _
        & " 	,ZEI_KBN                       " & vbNewLine _
        & "  FROM $LM_MST$..M_SAGYO            " & vbNewLine _
        & " WHERE                              " & vbNewLine _
        & " SYS_DEL_FLG = '0'                  " & vbNewLine _
        & " AND                                " & vbNewLine _
        & " SAGYO_CD = @SAGYO_CD               " & vbNewLine
#End Region

#Region "作業コード取得"
    Private Const SQL_SELECT_SAGYO_CD As String = _
          " SELECT KBN_NM1 AS NRS_BR_CD         " & vbNewLine _
        & "        ,KBN_NM2 AS SAGYO_CD         " & vbNewLine _
        & " FROM $LM_MST$..Z_KBN                " & vbNewLine _
        & " WHERE KBN_GROUP_CD = 'I011'         " & vbNewLine _
        & "   AND KBN_NM1 = @NRS_BR_CD          " & vbNewLine
#End Region

#Region "荷主マスタ情報取得"
    ''' <summary>
    ''' 荷主マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_M_CUST As String _
        = "SELECT M_CUST.SAGYO_SEIQTO_CD                                 " & vbNewLine _
        & "  FROM $LM_MST$..M_GOODS M_GOODS                              " & vbNewLine _
        & "  LEFT JOIN                                                   " & vbNewLine _
        & "       $LM_MST$..M_CUST M_CUST                                " & vbNewLine _
        & "    ON                                                        " & vbNewLine _
        & "       M_GOODS.NRS_BR_CD  = M_CUST.NRS_BR_CD                  " & vbNewLine _
        & "   AND                                                        " & vbNewLine _
        & "       M_GOODS.CUST_CD_L  = M_CUST.CUST_CD_L                  " & vbNewLine _
        & "   AND                                                        " & vbNewLine _
        & "       M_GOODS.CUST_CD_M  = M_CUST.CUST_CD_M                  " & vbNewLine _
        & "   AND                                                        " & vbNewLine _
        & "       M_GOODS.CUST_CD_S  = M_CUST.CUST_CD_S                  " & vbNewLine _
        & "   AND                                                        " & vbNewLine _
        & "       M_GOODS.CUST_CD_SS = M_CUST.CUST_CD_SS                 " & vbNewLine _
        & " WHERE                                                        " & vbNewLine _
        & "       M_CUST.NRS_BR_CD   = @NRS_BR_CD                        " & vbNewLine _
        & "   AND                                                        " & vbNewLine _
        & "       M_GOODS.GOODS_CD_NRS = @GOODS_CD_NRS                   " & vbNewLine
#End Region

#End Region

#Region "作業レコード追加"

    Private Const SQL_INSERT_E_SAGYO As String = "  INSERT INTO $LM_TRN$..E_SAGYO   " & vbNewLine _
                                               & "             ([NRS_BR_CD]         " & vbNewLine _
                                               & "             ,[SAGYO_REC_NO]      " & vbNewLine _
                                               & "             ,[SAGYO_COMP]        " & vbNewLine _
                                               & "             ,[SKYU_CHK]          " & vbNewLine _
                                               & "             ,[SAGYO_SIJI_NO]     " & vbNewLine _
                                               & "             ,[INOUTKA_NO_LM]     " & vbNewLine _
                                               & "             ,[WH_CD]             " & vbNewLine _
                                               & "             ,[IOZS_KB]           " & vbNewLine _
                                               & "             ,[SAGYO_CD]          " & vbNewLine _
                                               & "             ,[SAGYO_NM]          " & vbNewLine _
                                               & "             ,[CUST_CD_L]         " & vbNewLine _
                                               & "             ,[CUST_CD_M]         " & vbNewLine _
                                               & "             ,[DEST_CD]           " & vbNewLine _
                                               & "             ,[DEST_NM]           " & vbNewLine _
                                               & "             ,[GOODS_CD_NRS]      " & vbNewLine _
                                               & "             ,[GOODS_NM_NRS]      " & vbNewLine _
                                               & "             ,[LOT_NO]            " & vbNewLine _
                                               & "             ,[INV_TANI]          " & vbNewLine _
                                               & "             ,[SAGYO_NB]          " & vbNewLine _
                                               & "             ,[SAGYO_UP]          " & vbNewLine _
                                               & "             ,[SAGYO_GK]          " & vbNewLine _
                                               & "             ,[TAX_KB]            " & vbNewLine _
                                               & "             ,[SEIQTO_CD]         " & vbNewLine _
                                               & "             ,[REMARK_ZAI]        " & vbNewLine _
                                               & "             ,[REMARK_SKYU]       " & vbNewLine _
                                               & "             ,[SAGYO_COMP_CD]     " & vbNewLine _
                                               & "             ,[SAGYO_COMP_DATE]   " & vbNewLine _
                                               & "             ,[DEST_SAGYO_FLG]    " & vbNewLine _
                                               & "             ,[ZAI_REC_NO]        " & vbNewLine _
                                               & "             ,[PORA_ZAI_NB]       " & vbNewLine _
                                               & "             ,[PORA_ZAI_QT]       " & vbNewLine _
                                               & "             ,[SYS_ENT_DATE]      " & vbNewLine _
                                               & "             ,[SYS_ENT_TIME]      " & vbNewLine _
                                               & "             ,[SYS_ENT_PGID]      " & vbNewLine _
                                               & "             ,[SYS_ENT_USER]      " & vbNewLine _
                                               & "             ,[SYS_UPD_DATE]      " & vbNewLine _
                                               & "             ,[SYS_UPD_TIME]      " & vbNewLine _
                                               & "             ,[SYS_UPD_PGID]      " & vbNewLine _
                                               & "             ,[SYS_UPD_USER]      " & vbNewLine _
                                               & "             ,[SYS_DEL_FLG])      " & vbNewLine _
                                               & "       VALUES                     " & vbNewLine _
                                               & "             (@NRS_BR_CD          " & vbNewLine _
                                               & "             ,@SAGYO_REC_NO       " & vbNewLine _
                                               & "             ,@SAGYO_COMP         " & vbNewLine _
                                               & "             ,@SKYU_CHK           " & vbNewLine _
                                               & "             ,@SAGYO_SIJI_NO      " & vbNewLine _
                                               & "             ,@INOUTKA_NO_LM      " & vbNewLine _
                                               & "             ,@WH_CD              " & vbNewLine _
                                               & "             ,@IOZS_KB            " & vbNewLine _
                                               & "             ,@SAGYO_CD           " & vbNewLine _
                                               & "             ,@SAGYO_NM           " & vbNewLine _
                                               & "             ,@CUST_CD_L          " & vbNewLine _
                                               & "             ,@CUST_CD_M          " & vbNewLine _
                                               & "             ,@DEST_CD            " & vbNewLine _
                                               & "             ,@DEST_NM            " & vbNewLine _
                                               & "             ,@GOODS_CD_NRS       " & vbNewLine _
                                               & "             ,@GOODS_NM_NRS       " & vbNewLine _
                                               & "             ,@LOT_NO             " & vbNewLine _
                                               & "             ,@INV_TANI           " & vbNewLine _
                                               & "             ,@SAGYO_NB           " & vbNewLine _
                                               & "             ,@SAGYO_UP           " & vbNewLine _
                                               & "             ,@SAGYO_GK           " & vbNewLine _
                                               & "             ,@TAX_KB             " & vbNewLine _
                                               & "             ,@SEIQTO_CD          " & vbNewLine _
                                               & "             ,@REMARK_ZAI         " & vbNewLine _
                                               & "             ,@REMARK_SKYU        " & vbNewLine _
                                               & "             ,@SAGYO_COMP_CD      " & vbNewLine _
                                               & "             ,@SAGYO_COMP_DATE    " & vbNewLine _
                                               & "             ,@DEST_SAGYO_FLG     " & vbNewLine _
                                               & "             ,@ZAI_REC_NO         " & vbNewLine _
                                               & "             ,@PORA_ZAI_NB        " & vbNewLine _
                                               & "             ,@PORA_ZAI_QT        " & vbNewLine _
                                               & "             ,@SYS_ENT_DATE       " & vbNewLine _
                                               & "             ,@SYS_ENT_TIME       " & vbNewLine _
                                               & "             ,@SYS_ENT_PGID       " & vbNewLine _
                                               & "             ,@SYS_ENT_USER       " & vbNewLine _
                                               & "             ,@SYS_UPD_DATE       " & vbNewLine _
                                               & "             ,@SYS_UPD_TIME       " & vbNewLine _
                                               & "             ,@SYS_UPD_PGID       " & vbNewLine _
                                               & "             ,@SYS_UPD_USER       " & vbNewLine _
                                               & "             ,@SYS_DEL_FLG)       " & vbNewLine

#End Region

#End Region

#Region "Field"

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

#Region "SQLメイン処理"

#Region "追加対象データの検索"

    ''' <summary>
    ''' 追加対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectAddData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI130IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI130DAC.SQL_SELECT_ADDDATA)       'SQL構築 SELECT句
        Me._StrSql.Append(LMI130DAC.SQL_SELECT_FROM_ADDDATA1) 'SQL構築 FROM1句
        Call SetSQLFromADDATA1(inTbl.Rows(0))                 'From条件設定1(出荷(中)部分)
        Call SetSQLFromADDATA2(inTbl.Rows(0))                 'From条件設定2(出荷(小)部分)
        Me._StrSql.Append(LMI130DAC.SQL_SELECT_FROM_ADDDATA2) 'SQL構築 FROM2句
        Call SetSQLWhereADDATA(inTbl.Rows(0))                 '条件設定
        Me._StrSql.Append(LMI130DAC.SQL_SELECT_ORDER_ADDDATA) 'SQL構築 ORDER句

        'パラメータの設定
        Call SetSQLWhereParameter(inTbl.Rows(0))              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI130DAC", "SelectAddData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("WH_CD", "WH_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("GOODS_SYUBETU", "GOODS_SYUBETU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI130INOUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業データ取得
    ''' </summary>
    ''' <param name="ds">DataRow</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>SQLデータ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI130_SAGYO_CD")

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectParamSagyo(Me._SqlPrmList, inTbl.Rows(0).Item("SAGYO_CD").ToString())

        'スキーマ名設定
        Dim Sql As String = Me.SetSchemaNm(LMI130DAC.SQL_SELECT_M_SAGYO, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Sql)

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI130DAC", "SelectSagyoData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("KOSU_BAI", "KOSU_BAI")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("ZEI_KBN", "ZEI_KBN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI130_M_SAGYO")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業コードの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoCord(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI130INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim nrsBrCd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString()
        _SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", nrsBrCd, DBDataType.CHAR))

        'スキーマ名設定
        Dim Sql As String = Me.SetSchemaNm(LMI130DAC.SQL_SELECT_SAGYO_CD, nrsBrCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI130DAC", "SelectSagyoCord", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_CD", "SAGYO_CD")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI130_SAGYO_CD")

        reader.Close()

        Return ds

    End Function

#Region "SELECT_M_CUST"

    ''' <summary>
    ''' 荷主マスタ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI130INOUT")

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(LMI130DAC.SQL_SELECT_M_CUST, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        ' パラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Call Me.SetSelectParamCustMst(inTbl(0))

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI130DAC", "SelectCustData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI130_M_CUST")

        reader.Close()

        Return ds

    End Function

#End Region

    ''' <summary>
    ''' 登録処理（作業レコード）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRecord(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI130_E_SAGYO")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI130DAC.SQL_INSERT_E_SAGYO)

        'パラメータの設定
        Call SetSQLInsertSagyoParameter(inTbl(0))
        Call Me.SetParamCommonSystemIns()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI130DAC", "InsertSagyoRecord", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 追加対象データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLFromADDATA1(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("INNER JOIN ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("$LM_TRN$..C_OUTKA_M OUTKAM ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("ON ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("OUTKAM.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAM.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷管理番号(大)
            whereStr = .Item("OUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAM.OUTKA_NO_L = @OUTKA_NO_L")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷管理番号(中)
            whereStr = .Item("OUTKA_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAM.OUTKA_NO_M = @OUTKA_NO_M")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLFromADDATA2(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("INNER JOIN ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("$LM_TRN$..C_OUTKA_S OUTKAS ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("ON ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("OUTKAS.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAS.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷管理番号(大)
            whereStr = .Item("OUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAS.OUTKA_NO_L = @OUTKA_NO_L")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷管理番号(中)
            whereStr = .Item("OUTKA_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAS.OUTKA_NO_M = @OUTKA_NO_M")
                Me._StrSql.Append(vbNewLine)
            Else
                Me._StrSql.Append(" AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷管理番号(小)
            whereStr = .Item("OUTKA_NO_S").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAS.OUTKA_NO_S = @OUTKA_NO_S")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereADDATA(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("OUTKAL.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
            End If

            '出荷管理番号(大)
            whereStr = .Item("OUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L")
                Me._StrSql.Append(vbNewLine)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereParameter(ByVal inTblRow As DataRow)

        With inTblRow

            '営業所コード
            If String.IsNullOrEmpty(.Item("NRS_BR_CD").ToString()) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            End If

            '出荷管理番号(大)
            If String.IsNullOrEmpty(.Item("OUTKA_NO_L").ToString()) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            End If

            '出荷管理番号(中)
            If String.IsNullOrEmpty(.Item("OUTKA_NO_M").ToString()) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            End If

            '出荷管理番号(小)
            If String.IsNullOrEmpty(.Item("OUTKA_NO_S").ToString()) = False Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

#Region "E_SAGYOの検索パラメータ"
    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamSagyo(ByVal prmList As ArrayList, ByVal sagyoCD As String)

        'パラメータ設定
        prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", sagyoCD, DBDataType.CHAR))

    End Sub
#End Region

#Region "M_CUSTの検索パラメータ"
    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <param name="inTblRow">IN情報</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParamCustMst(ByVal inTblRow As DataRow)

        With inTblRow
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me.NullConvertString(.Item("GOODS_CD_NRS").ToString()), DBDataType.CHAR))
        End With

    End Sub
#End Region

    Private Sub SetSQLInsertSagyoParameter(ByVal inTblRow As DataRow)

        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", Me.NullConvertString(.Item("SAGYO_REC_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", Me.NullConvertString(.Item("SAGYO_COMP")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", Me.NullConvertString(.Item("SKYU_CHK")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me.NullConvertString(.Item("SAGYO_SIJI_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", Me.NullConvertString(.Item("INOUTKA_NO_LM")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", Me.NullConvertString(.Item("IOZS_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", Me.NullConvertString(.Item("SAGYO_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", Me.NullConvertString(.Item("SAGYO_NM")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me.NullConvertString(.Item("GOODS_CD_NRS")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", Me.NullConvertString(.Item("GOODS_NM_NRS")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(.Item("LOT_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", Me.NullConvertString(.Item("INV_TANI")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", Me.NullConvertZero(.Item("SAGYO_NB")), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", Me.NullConvertZero(.Item("SAGYO_UP")), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", Me.NullConvertZero(.Item("SAGYO_GK")), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(.Item("TAX_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.NullConvertString(.Item("SEIQTO_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", Me.NullConvertString(.Item("REMARK_ZAI")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", Me.NullConvertString(.Item("REMARK_SKYU")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", Me.NullConvertString(.Item("SAGYO_COMP_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", Me.NullConvertString(.Item("SAGYO_COMP_DATE")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", Me.NullConvertString(.Item("DEST_SAGYO_FLG")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me.NullConvertString(.Item("ZAI_REC_NO")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.NullConvertZero(.Item("PORA_ZAI_NB")), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.NullConvertZero(.Item("PORA_ZAI_QT")), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

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

#End Region

#End Region

#Region "変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        If String.IsNullOrEmpty(value.ToString()) = True Then
            value = 0
        End If

        Return value

    End Function

#End Region

#Region "ユーティリティ"

#End Region

#End Region

End Class
