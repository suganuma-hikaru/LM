' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF710    : 立合書（運送）
'  作  成  者       :  hori
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF710DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF710DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

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

#Region "検索"

    ''' <summary>
    '''帳票パターン検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF710IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '運送チェックリストのSQLをそのまま利用
        Me._StrSql.Append(LMF700DAC.SQL_SELECT_MPRT)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", "DF", DBDataType.CHAR))

        'パラメータ反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ発行
        MyBase.Logger.WriteSQLLog("LMF710DAC", "SelectMPrt", cmd)

        'SQL発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()

        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function

    ''' <summary>
    ''' 出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF710IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '運送チェックリストのSQLをそのまま利用
        Me._StrSql.Append(LMF700DAC.SQL_SELECT_PRINT_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", "DF", DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ発行
        MyBase.Logger.WriteSQLLog("LMF710DAC", "SelectPrintData", cmd)

        'SQL発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()

        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PRINT_SORT", "PRINT_SORT")
        map.Add("TOU_BETU_FLG", "TOU_BETU_FLG")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("PC_KB", "PC_KB")
        map.Add("KYORI", "KYORI")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("URIG_NM", "URIG_NM")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_UNSO", "REMARK_UNSO")
        map.Add("REMARK_SIJI", "REMARK_SIJI")
        map.Add("SAGYO_REC_NO_1", "SAGYO_REC_NO_1")
        map.Add("SAGYO_CD_1", "SAGYO_CD_1")
        map.Add("SAGYO_NM_1", "SAGYO_NM_1")
        map.Add("REMARK_SIJI_L_1", "REMARK_SIJI_L_1")
        map.Add("WH_SAGYO_YN_L_1", "WH_SAGYO_YN_L_1")
        map.Add("SAGYO_REC_NO_2", "SAGYO_REC_NO_2")
        map.Add("SAGYO_CD_2", "SAGYO_CD_2")
        map.Add("SAGYO_NM_2", "SAGYO_NM_2")
        map.Add("REMARK_SIJI_L_2", "REMARK_SIJI_L_2")
        map.Add("WH_SAGYO_YN_L_2", "WH_SAGYO_YN_L_2")
        map.Add("SAGYO_REC_NO_3", "SAGYO_REC_NO_3")
        map.Add("SAGYO_CD_3", "SAGYO_CD_3")
        map.Add("SAGYO_NM_3", "SAGYO_NM_3")
        map.Add("REMARK_SIJI_L_3", "REMARK_SIJI_L_3")
        map.Add("WH_SAGYO_YN_L_3", "WH_SAGYO_YN_L_3")
        map.Add("SAGYO_REC_NO_4", "SAGYO_REC_NO_4")
        map.Add("SAGYO_CD_4", "SAGYO_CD_4")
        map.Add("SAGYO_NM_4", "SAGYO_NM_4")
        map.Add("REMARK_SIJI_L_4", "REMARK_SIJI_L_4")
        map.Add("WH_SAGYO_YN_L_4", "WH_SAGYO_YN_L_4")
        map.Add("SAGYO_REC_NO_5", "SAGYO_REC_NO_5")
        map.Add("SAGYO_CD_5", "SAGYO_CD_5")
        map.Add("SAGYO_NM_5", "SAGYO_NM_5")
        map.Add("REMARK_SIJI_L_5", "REMARK_SIJI_L_5")
        map.Add("WH_SAGYO_YN_L_5", "WH_SAGYO_YN_L_5")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ZAN_KONSU", "ZAN_KONSU")
        map.Add("ZAN_HASU", "ZAN_HASU")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("REMARK_S", "REMARK_S")
        map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
        map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
        map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
        map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
        map.Add("REMARK_SIJI_M_1", "REMARK_SIJI_M_1")
        map.Add("WH_SAGYO_YN_M_1", "WH_SAGYO_YN_M_1")
        map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
        map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
        map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
        map.Add("REMARK_SIJI_M_2", "REMARK_SIJI_M_2")
        map.Add("WH_SAGYO_YN_M_2", "WH_SAGYO_YN_M_2")
        map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
        map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
        map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
        map.Add("REMARK_SIJI_M_3", "REMARK_SIJI_M_3")
        map.Add("WH_SAGYO_YN_M_3", "WH_SAGYO_YN_M_3")
        map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
        map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
        map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
        map.Add("REMARK_SIJI_M_4", "REMARK_SIJI_M_4")
        map.Add("WH_SAGYO_YN_M_4", "WH_SAGYO_YN_M_4")
        map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
        map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
        map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
        map.Add("REMARK_SIJI_M_5", "REMARK_SIJI_M_5")
        map.Add("WH_SAGYO_YN_M_5", "WH_SAGYO_YN_M_5")
        map.Add("SAGYO_MEI_REC_NO_D1", "SAGYO_MEI_REC_NO_D1")
        map.Add("SAGYO_MEI_CD_D1", "SAGYO_MEI_CD_D1")
        map.Add("SAGYO_MEI_NM_D1", "SAGYO_MEI_NM_D1")
        map.Add("REMARK_SIJI_M_D1", "REMARK_SIJI_M_D1")
        map.Add("WH_SAGYO_YN_M_D1", "WH_SAGYO_YN_M_D1")
        map.Add("SAGYO_MEI_REC_NO_D2", "SAGYO_MEI_REC_NO_D2")
        map.Add("SAGYO_MEI_CD_D2", "SAGYO_MEI_CD_D2")
        map.Add("SAGYO_MEI_NM_D2", "SAGYO_MEI_NM_D2")
        map.Add("REMARK_SIJI_M_D2", "REMARK_SIJI_M_D2")
        map.Add("WH_SAGYO_YN_M_D2", "WH_SAGYO_YN_M_D2")
        map.Add("SAIHAKKO_FLG", "SAIHAKKO_FLG")
        map.Add("OYA_CUST_GOODS_CD", "OYA_CUST_GOODS_CD")
        map.Add("OYA_GOODS_NM", "OYA_GOODS_NM")
        map.Add("OYA_KATA", "OYA_KATA")
        map.Add("OYA_OUTKA_TTL_NB", "OYA_OUTKA_TTL_NB")
        map.Add("SET_NAIYO", "SET_NAIYO")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("CUST_NM_S_H", "CUST_NM_S_H")
        map.Add("RPT_FLG", "RPT_FLG")
        map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
        map.Add("OUTKA_NO_S", "OUTKA_NO_S")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_NAIYO_1", "CUST_NAIYO_1")
        map.Add("CUST_NAIYO_2", "CUST_NAIYO_2")
        map.Add("CUST_NAIYO_3", "CUST_NAIYO_3")
        map.Add("DEST_REMARK", "DEST_REMARK")
        map.Add("DEST_SALES_CD", "DEST_SALES_CD")
        map.Add("DEST_SALES_NM_L", "DEST_SALES_NM_L")
        map.Add("DEST_SALES_NM_M", "DEST_SALES_NM_M")
        map.Add("ALCTD_NB_HEADKEI", "ALCTD_NB_HEADKEI")
        map.Add("ALCTD_QT_HEADKEI", "ALCTD_QT_HEADKEI")
        map.Add("HINMEI", "HINMEI")
        map.Add("NISUGATA", "NISUGATA")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("CUST_REF_NO", "CUST_REF_NO")           'add 20220610

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF710OUT")

        Return ds

    End Function

#End Region '検索

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region 'ユーティリティ

#End Region 'Method

End Class

