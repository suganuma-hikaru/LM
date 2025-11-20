' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 入荷管理
'  プログラムID     :  LMB060DAC : 入庫連絡票
'  作  成  者       :  hojo
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB060DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB060DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "Insert"

#Region "INKA_L"

    Private Const SQL_INSERT_INKA_L As String = "INSERT INTO $LM_TRN$..B_INKA_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",FURI_NO                      " & vbNewLine _
                                              & ",INKA_TP                      " & vbNewLine _
                                              & ",INKA_KB                      " & vbNewLine _
                                              & ",INKA_STATE_KB                " & vbNewLine _
                                              & ",INKA_DATE                    " & vbNewLine _
                                              & ",STORAGE_DUE_DATE             " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",INKA_PLAN_QT                 " & vbNewLine _
                                              & ",INKA_PLAN_QT_UT              " & vbNewLine _
                                              & ",INKA_TTL_NB                  " & vbNewLine _
                                              & ",BUYER_ORD_NO_L               " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_L          " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN               " & vbNewLine _
                                              & ",HOKAN_YN                     " & vbNewLine _
                                              & ",HOKAN_FREE_KIKAN             " & vbNewLine _
                                              & ",HOKAN_STR_DATE               " & vbNewLine _
                                              & ",NIYAKU_YN                    " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",CHECKLIST_PRT_DATE           " & vbNewLine _
                                              & ",CHECKLIST_PRT_USER           " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_DATE        " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_USER        " & vbNewLine _
                                              & ",UKETSUKE_DATE                " & vbNewLine _
                                              & ",UKETSUKE_USER                " & vbNewLine _
                                              & ",KEN_DATE                     " & vbNewLine _
                                              & ",KEN_USER                     " & vbNewLine _
                                              & ",INKO_DATE                    " & vbNewLine _
                                              & ",INKO_USER                    " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_DATE           " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_USER           " & vbNewLine _
                                              & ",UNCHIN_TP                    " & vbNewLine _
                                              & ",UNCHIN_KB                    " & vbNewLine _
                                              & ",WH_KENPIN_WK_STATUS          " & vbNewLine _
                                              & ",WH_TAB_STATUS                " & vbNewLine _
                                              & ",WH_TAB_YN                    " & vbNewLine _
                                              & ",WH_TAB_IMP_YN                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@FURI_NO                     " & vbNewLine _
                                              & ",@INKA_TP                     " & vbNewLine _
                                              & ",@INKA_KB                     " & vbNewLine _
                                              & ",@INKA_STATE_KB               " & vbNewLine _
                                              & ",@INKA_DATE                   " & vbNewLine _
                                              & ",@STORAGE_DUE_DATE            " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@INKA_PLAN_QT                " & vbNewLine _
                                              & ",@INKA_PLAN_QT_UT             " & vbNewLine _
                                              & ",@INKA_TTL_NB                 " & vbNewLine _
                                              & ",@BUYER_ORD_NO_L              " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_L         " & vbNewLine _
                                              & ",@TOUKI_HOKAN_YN              " & vbNewLine _
                                              & ",@HOKAN_YN                    " & vbNewLine _
                                              & ",@HOKAN_FREE_KIKAN            " & vbNewLine _
                                              & ",@HOKAN_STR_DATE              " & vbNewLine _
                                              & ",@NIYAKU_YN                   " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",@UNCHIN_TP                   " & vbNewLine _
                                              & ",@UNCHIN_KB                   " & vbNewLine _
                                              & ",@WH_KENPIN_WK_STATUS         " & vbNewLine _
                                              & ",@WH_TAB_STATUS               " & vbNewLine _
                                              & ",@WH_TAB_YN                   " & vbNewLine _
                                              & ",@WH_TAB_IMP_YN               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

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
    Private _StrSql As StringBuilder = New StringBuilder()

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = New ArrayList()

#End Region

#Region "更新"

#Region "INKA_L"
    ''' <summary>
    ''' 入荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertInkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB060IN")
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMB060DAC.SQL_INSERT_INKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetInkaLComParameter(Me._Row, Me._SqlPrmList)


        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB060DAC", "InsertInkaLData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        '更新日付を設定
        Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
        Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

        Return ds

    End Function

#End Region

#End Region '更新

#Region "パラメータ設定"

    ''' <summary>
    ''' INKA_Lの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TP", .Item("INKA_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_KB", .Item("INKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STORAGE_DUE_DATE", .Item("STORAGE_DUE_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", .Item("INKA_PLAN_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", .Item("INKA_PLAN_QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", .Item("INKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Item("BUYER_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", .Item("UNCHIN_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KENPIN_WK_STATUS", .Item("WH_KENPIN_WK_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", .Item("WH_TAB_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_IMP_YN", .Item("WH_TAB_IMP_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_STATUS").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetDataInsertParameter(ByVal prmList As ArrayList) As String()

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        SetDataInsertParameter = Me.SetSysdataParameter(prmList)
        Return SetDataInsertParameter

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataParameter(ByVal prmList As ArrayList) As String()

        'システム項目
        SetSysdataParameter = Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Return SetSysdataParameter

    End Function
    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataTimeParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", sysDateTime(1), DBDataType.CHAR))

        Return sysDateTime

    End Function
#End Region 'パラメータ設定

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

End Class
