' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC541    : 受領書(YCC_サクラ)印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC541DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC541DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                           " & vbNewLine _
                                            & "	OUTL.NRS_BR_CD                                           AS NRS_BR_CD    " & vbNewLine _
                                            & ",'09'                                                     AS PTN_ID       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                         " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                        " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD       " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                         " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                    " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID       " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用(親コードなしレコード取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA1 As String = "SELECT                                                                       " & vbNewLine _
                                             & "*                                                                            " & vbNewLine _
                                             & "FROM                                                                         " & vbNewLine _
                                             & "(SELECT                                                                      " & vbNewLine _
                                             & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                           " & vbNewLine _
                                             & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                           " & vbNewLine _
                                             & "       ELSE MR3.RPT_ID                                                       " & vbNewLine _
                                             & "  END                     AS RPT_ID                                          " & vbNewLine _
                                             & " ,''                      AS NO                                              " & vbNewLine _
                                             & " ,OUTL.NRS_BR_CD          AS NRS_BR_CD                                       " & vbNewLine _
                                             & " ,EDIL.DEST_AD_1          AS DEST_AD_1                                       " & vbNewLine _
                                             & " ,EDIL.DEST_AD_2          AS DEST_AD_2                                       " & vbNewLine _
                                             & " ,EDIL.DEST_NM            AS DEST_NM                                         " & vbNewLine _
                                             & " ,OUTL.OUTKA_PLAN_DATE    AS OUTKA_PLAN_DATE                                 " & vbNewLine _
                                             & " ,OUTL.OUTKO_DATE         AS OUTKO_DATE                                      " & vbNewLine _
                                             & " ,EDIL.OUTKA_CTL_NO       AS OUTKA_CTL_NO                                    " & vbNewLine _
                                             & " ,EDIM.OUTKA_CTL_NO_CHU   AS OUTKA_CTL_NO_CHU                                " & vbNewLine _
                                             & " ,EDIM.FREE_C02           AS M_FREE_C02                                      " & vbNewLine _
                                             & " ,EDIM.CUST_GOODS_CD      AS OYA_CUST_GOODS_CD                               " & vbNewLine _
                                             & " ,EDIM.GOODS_NM           AS OYA_GOODS_NM                                    " & vbNewLine _
                                             & " ,EDIM.FREE_C08           AS M_FREE_C08                                      " & vbNewLine _
                                             & " ,EDIM.FREE_C07           AS OYA_KATA                                        " & vbNewLine _
                                             & " ,EDIM.OUTKA_TTL_NB       AS OYA_OUTKA_TTL_NB                                " & vbNewLine _
                                             & " ,EDIM.BUYER_ORD_NO_DTL   AS BUYER_ORD_NO_DTL                                " & vbNewLine _
                                             & " ,EDIM.FREE_C01           AS M_FREE_C01                                      " & vbNewLine _
                                             & " ,''                      AS KO_CUST_GOODS_CD                                " & vbNewLine _
                                             & " ,''                      AS KO_GOODS_NM                                     " & vbNewLine _
                                             & " ,''                      AS KO_KATA                                         " & vbNewLine _
                                             & " ,0                       AS KO_OUTKA_TTL_NB                                 " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用(親コードありレコード取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA2 As String = "UNION ALL                                                                    " & vbNewLine _
                                             & " SELECT                                                                      " & vbNewLine _
                                             & "  CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                           " & vbNewLine _
                                             & "       WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                           " & vbNewLine _
                                             & "       ELSE MR3.RPT_ID                                                       " & vbNewLine _
                                             & "  END                     AS RPT_ID                                          " & vbNewLine _
                                             & " ,''                      AS NO                                              " & vbNewLine _
                                             & " ,OUTL.NRS_BR_CD          AS NRS_BR_CD                                       " & vbNewLine _
                                             & " ,EDIL.DEST_AD_1          AS DEST_AD_1                                       " & vbNewLine _
                                             & " ,EDIL.DEST_AD_2          AS DEST_AD_2                                       " & vbNewLine _
                                             & " ,EDIL.DEST_NM            AS DEST_NM                                         " & vbNewLine _
                                             & " ,OUTL.OUTKA_PLAN_DATE    AS OUTKA_PLAN_DATE                                 " & vbNewLine _
                                             & " ,OUTL.OUTKO_DATE         AS OUTKO_DATE                                      " & vbNewLine _
                                             & " ,EDIL.OUTKA_CTL_NO       AS OUTKA_CTL_NO                                    " & vbNewLine _
                                             & " ,EDIM.OUTKA_CTL_NO_CHU   AS OUTKA_CTL_NO_CHU                                " & vbNewLine _
                                             & " ,EDIM.FREE_C02           AS M_FREE_C02                                      " & vbNewLine _
                                             & " ,EDIM.FREE_C11           AS OYA_CUST_GOODS_CD                               " & vbNewLine _
                                             & " ,EDIM.FREE_C12           AS OYA_GOODS_NM                                    " & vbNewLine _
                                             & " ,EDIM.FREE_C08           AS M_FREE_C08                                      " & vbNewLine _
                                             & " ,EDIM.FREE_C13           AS OYA_KATA                                        " & vbNewLine _
                                             & " ,EDIM.FREE_N02           AS OYA_OUTKA_TTL_NB                                " & vbNewLine _
                                             & " ,EDIM.BUYER_ORD_NO_DTL   AS BUYER_ORD_NO_DTL                                " & vbNewLine _
                                             & " ,EDIM.FREE_C01           AS M_FREE_C01                                      " & vbNewLine _
                                             & " ,EDIM.CUST_GOODS_CD      AS KO_CUST_GOODS_CD                                " & vbNewLine _
                                             & " ,EDIM.GOODS_NM           AS KO_GOODS_NM                                     " & vbNewLine _
                                             & " ,EDIM.FREE_C07           AS KO_KATA                                         " & vbNewLine _
                                             & " ,EDIM.OUTKA_TTL_NB       AS KO_OUTKA_TTL_NB                                 " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                               " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_L OUTL                                           " & vbNewLine _
                                     & "LEFT JOIN                                                          " & vbNewLine _
                                     & "--出荷M                                                            " & vbNewLine _
                                     & "$LM_TRN$..C_OUTKA_M OUTM                                           " & vbNewLine _
                                     & "ON  OUTM.NRS_BR_CD   = OUTL.NRS_BR_CD                              " & vbNewLine _
                                     & "AND OUTM.OUTKA_NO_L  = OUTL.OUTKA_NO_L                             " & vbNewLine _
                                     & "AND OUTM.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "LEFT JOIN                                                          " & vbNewLine _
                                     & "--出荷EDIL                                                         " & vbNewLine _
                                     & "$LM_TRN$..H_OUTKAEDI_L EDIL                                        " & vbNewLine _
                                     & "ON  EDIL.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "AND EDIL.NRS_BR_CD = OUTL.NRS_BR_CD                                " & vbNewLine _
                                     & "AND EDIL.OUTKA_CTL_NO = OUTL.OUTKA_NO_L                            " & vbNewLine _
                                     & "LEFT JOIN                                                          " & vbNewLine _
                                     & "--出荷EDIM                                                         " & vbNewLine _
                                     & "$LM_TRN$..H_OUTKAEDI_M EDIM                                        " & vbNewLine _
                                     & "ON  EDIM.NRS_BR_CD = EDIL.NRS_BR_CD                                " & vbNewLine _
                                     & "AND EDIM.EDI_CTL_NO = EDIL.EDI_CTL_NO                              " & vbNewLine _
                                     & "AND EDIM.NRS_BR_CD = OUTM.NRS_BR_CD                                " & vbNewLine _
                                     & "AND EDIM.OUTKA_CTL_NO = OUTM.OUTKA_NO_L                            " & vbNewLine _
                                     & "AND EDIM.OUTKA_CTL_NO_CHU = OUTM.OUTKA_NO_M                        " & vbNewLine _
                                     & "AND EDIM.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "--商品M                                                            " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                                     " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = EDIM.NRS_BR_CD                                  " & vbNewLine _
                                     & "AND MG.GOODS_CD_NRS = EDIM.NRS_GOODS_CD                            " & vbNewLine _
                                     & "AND MG.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                                     & "--出荷Lでの荷主帳票パターン取得                                    " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                " & vbNewLine _
                                     & "ON  OUTL.NRS_BR_CD = MCR1.NRS_BR_CD                                " & vbNewLine _
                                     & "AND OUTL.CUST_CD_L = MCR1.CUST_CD_L                                " & vbNewLine _
                                     & "AND OUTL.CUST_CD_M = MCR1.CUST_CD_M                                " & vbNewLine _
                                     & "AND '00' = MCR1.CUST_CD_S                                          " & vbNewLine _
                                     & "AND MCR1.PTN_ID = '09'                                             " & vbNewLine _
                                     & "AND MCR1.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "--帳票パターン取得                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                      " & vbNewLine _
                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                 " & vbNewLine _
                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                       " & vbNewLine _
                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                       " & vbNewLine _
                                     & "AND MR1.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                     & "--商品Mの荷主での荷主帳票パターン取得                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                " & vbNewLine _
                                     & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                  " & vbNewLine _
                                     & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                  " & vbNewLine _
                                     & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                  " & vbNewLine _
                                     & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                  " & vbNewLine _
                                     & "AND MCR2.PTN_ID = '09'                                             " & vbNewLine _
                                     & "AND MCR2.SYS_DEL_FLG = '0'                                         " & vbNewLine _
                                     & "--帳票パターン取得                                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                      " & vbNewLine _
                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                 " & vbNewLine _
                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                       " & vbNewLine _
                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                       " & vbNewLine _
                                     & "AND MR2.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                     & "--存在しない場合の帳票パターン取得                                 " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                      " & vbNewLine _
                                     & "ON  MR3.NRS_BR_CD = OUTL.NRS_BR_CD                                 " & vbNewLine _
                                     & "AND MR3.PTN_ID = '09'                                              " & vbNewLine _
                                     & "AND MR3.STANDARD_FLAG = '01'                                       " & vbNewLine _
                                     & "AND MR3.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                     & "--2018/12/11 ADD START 要望管理002112                              " & vbNewLine _
                                     & "LEFT JOIN $LM_MST$..M_GOODS_DETAILS MGD                            " & vbNewLine _
                                     & "ON  MGD.NRS_BR_CD = OUTL.NRS_BR_CD                                 " & vbNewLine _
                                     & "AND MGD.GOODS_CD_NRS = MG.GOODS_CD_NRS                             " & vbNewLine _
                                     & "AND MGD.SUB_KB = '71'                                              " & vbNewLine _
                                     & "AND MGD.SYS_DEL_FLG = '0'                                          " & vbNewLine _
                                     & "--2018/12/11 ADD END   要望管理002112                              " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句（帳票種別取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_MPrt As String = " WHERE                                                                       " & vbNewLine _
                                           & "     OUTL.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                           & " AND OUTM.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                           & " AND OUTL.NRS_BR_CD = @NRS_BR_CD                                             " & vbNewLine _
                                           & " AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                           " & vbNewLine _
                                           & " AND MG.DOKU_KB <> '01'                                                      " & vbNewLine _
                                           & " AND ISNULL(MGD.SET_NAIYO,'') <> '1'  --2018/12/11 ADD 要望管理002112        " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句（親コードなしレコード取得）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE1 As String = " WHERE                                                                       " & vbNewLine _
                                       & "     OUTL.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                       & " AND OUTM.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                       & " AND OUTL.NRS_BR_CD = @NRS_BR_CD                                             " & vbNewLine _
                                       & " AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                           " & vbNewLine _
                                       & " AND EDIM.FREE_C11 = ''                                                      " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句（親コードありレコード取得）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE2 As String = " WHERE                                                                       " & vbNewLine _
                                       & "     OUTL.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                       & " AND OUTM.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                       & " AND OUTL.NRS_BR_CD = @NRS_BR_CD                                             " & vbNewLine _
                                       & " AND OUTL.OUTKA_NO_L = @OUTKA_NO_L                                           " & vbNewLine _
                                       & " AND EDIM.FREE_C11 <> ''                                                     " & vbNewLine _
                                       & " ) MAIN                                                                      " & vbNewLine



    ''' <summary>
    ''' ORDER BY（①営業所コード、②出荷管理番号L、③納入先名、④ご注文主名、⑤親商品名、⑥子商品名）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                                     " & vbNewLine _
                                         & "     MAIN.NRS_BR_CD                                                          " & vbNewLine _
                                         & "    ,MAIN.OUTKA_CTL_NO                                                       " & vbNewLine _
                                         & "    ,MAIN.M_FREE_C02                                                         " & vbNewLine _
                                         & "    ,MAIN.DEST_NM                                                            " & vbNewLine _
                                         & "    ,MAIN.OYA_GOODS_NM                                                       " & vbNewLine _
                                         & "    ,MAIN.KO_GOODS_NM                                                        " & vbNewLine

    '& "    ,MAIN.OUTKA_CTL_NO_CHU                                                   " & vbNewLine _
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
        Dim inTbl As DataTable = ds.Tables("LMC541IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC541DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMC541DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC541DAC.SQL_WHERE_MPrt)       'SQL構築(データ抽出用Where句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC541DAC", "SelectMPrt", cmd)

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
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC541IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC541DAC.SQL_SELECT_DATA1)     'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC541DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC541DAC.SQL_WHERE1)           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMC541DAC.SQL_SELECT_DATA2)     'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMC541DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Me._StrSql.Append(LMC541DAC.SQL_WHERE2)           'SQL構築(データ抽出用Where句)
        Me._StrSql.Append(LMC541DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC541DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NO", "NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("M_FREE_C02", "M_FREE_C02")
        map.Add("OYA_CUST_GOODS_CD", "OYA_CUST_GOODS_CD")
        map.Add("OYA_GOODS_NM", "OYA_GOODS_NM")
        map.Add("M_FREE_C08", "M_FREE_C08")
        map.Add("OYA_KATA", "OYA_KATA")
        map.Add("OYA_OUTKA_TTL_NB", "OYA_OUTKA_TTL_NB")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("M_FREE_C01", "M_FREE_C01")
        map.Add("KO_CUST_GOODS_CD", "KO_CUST_GOODS_CD")
        map.Add("KO_GOODS_NM", "KO_GOODS_NM")
        map.Add("KO_KATA", "KO_KATA")
        map.Add("KO_OUTKA_TTL_NB", "KO_OUTKA_TTL_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC541OUT")

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

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("OUTKA_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", whereStr, DBDataType.CHAR))

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
