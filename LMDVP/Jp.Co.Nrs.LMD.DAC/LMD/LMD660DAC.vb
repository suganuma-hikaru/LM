' ==========================================================================
'  システム名       : LM
'  サブシステム名   : LMD       : 在庫管理
'  プログラムID     : LMD660    : 自動倉庫置場変更一覧
'  作  成  者       : hori
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD660DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD660DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理"

    ''' <summary>
    ''' 帳票種別検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "" _
            & " SELECT                                              " & vbNewLine _
            & "   MR1.NRS_BR_CD,                                    " & vbNewLine _
            & "   MR1.PTN_ID,                                       " & vbNewLine _
            & "   MR1.PTN_CD,                                       " & vbNewLine _
            & "   MR1.RPT_ID                                        " & vbNewLine _
            & " FROM                                                " & vbNewLine _
            & "   $LM_MST$..M_RPT MR1                               " & vbNewLine _
            & " WHERE                                               " & vbNewLine _
            & "   MR1.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
            & "   AND MR1.PTN_ID = 'D6'                             " & vbNewLine _
            & "   AND MR1.STANDARD_FLAG = '01'                      " & vbNewLine _
            & "   AND MR1.SYS_DEL_FLG = '0'                         " & vbNewLine

    ''' <summary>
    ''' 印刷データ検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINTDATA As String = "" _
            & " SELECT                                              " & vbNewLine _
            & "   DIT.NRS_BR_CD,                                    " & vbNewLine _
            & "   DZO.WH_CD,                                        " & vbNewLine _
            & "   DZO.CUST_CD_L,                                    " & vbNewLine _
            & "   DZO.CUST_CD_M,                                    " & vbNewLine _
            & "   MGS.CUST_CD_S,                                    " & vbNewLine _
            & "   MCT.CUST_NM_L,                                    " & vbNewLine _
            & "   MCT.CUST_NM_M,                                    " & vbNewLine _
            & "   @IDO_DATE AS IDO_DATE,                            " & vbNewLine _
            & "   DIT.OUTKO_NO,                                     " & vbNewLine _
            & "   DIT.INKO_NO,                                      " & vbNewLine _
            & "   MGS.GOODS_CD_CUST,                                " & vbNewLine _
            & "   MGS.GOODS_NM_1 AS GOODS_NM,                       " & vbNewLine _
            & "   DZO.LOT_NO,                                       " & vbNewLine _
            & "   DZO.IRIME,                                        " & vbNewLine _
            & "   MGS.STD_IRIME_UT,                                 " & vbNewLine _
            & "   DZO.INKO_DATE,                                    " & vbNewLine _
            & "   DIT.N_ALLOC_CAN_NB AS ALLOC_CAN_NB,               " & vbNewLine _
            & "   MGS.PKG_UT,                                       " & vbNewLine _
            & "   DIT.O_ALCTD_NB AS ALCTD_NB,                       " & vbNewLine _
            & "   DIT.O_PORA_ZAI_NB AS PORA_ZAI_NB,                 " & vbNewLine _
            & "   DZO.TOU_NO,                                       " & vbNewLine _
            & "   DZO.SITU_NO,                                      " & vbNewLine _
            & "   DZO.ZONE_CD,                                      " & vbNewLine _
            & "   DZO.LOCA,                                         " & vbNewLine _
            & "   DZO.REMARK,                                       " & vbNewLine _
            & "   DZO.REMARK_OUT,                                   " & vbNewLine _
            & "   DZN.TOU_NO AS TOU_NO_R,                           " & vbNewLine _
            & "   DZN.SITU_NO AS SITU_NO_R,                         " & vbNewLine _
            & "   DZN.ZONE_CD AS ZONE_CD_R,                         " & vbNewLine _
            & "   DZN.LOCA AS LOCA_R,                               " & vbNewLine _
            & "   DZN.ALLOC_CAN_NB AS IDO_KOSU                      " & vbNewLine _
            & " FROM                                                " & vbNewLine _
            & "   $LM_TRN$..D_IDO_TRS DIT                           " & vbNewLine _
            & " INNER JOIN                                          " & vbNewLine _
            & "   $LM_TRN$..D_ZAI_TRS DZO                           " & vbNewLine _
            & "   ON                                                " & vbNewLine _
            & "     DZO.NRS_BR_CD = DIT.NRS_BR_CD                   " & vbNewLine _
            & "     AND DZO.ZAI_REC_NO = DIT.O_ZAI_REC_NO           " & vbNewLine _
            & "     AND DZO.SYS_DEL_FLG = '0'                       " & vbNewLine _
            & " INNER JOIN                                          " & vbNewLine _
            & "   $LM_TRN$..D_ZAI_TRS DZN                           " & vbNewLine _
            & "   ON                                                " & vbNewLine _
            & "     DZN.NRS_BR_CD = DIT.NRS_BR_CD                   " & vbNewLine _
            & "     AND DZN.ZAI_REC_NO = DIT.N_ZAI_REC_NO           " & vbNewLine _
            & "     AND DZN.SYS_DEL_FLG = '0'                       " & vbNewLine _
            & " LEFT JOIN                                           " & vbNewLine _
            & "   $LM_MST$..M_GOODS MGS                             " & vbNewLine _
            & "   ON                                                " & vbNewLine _
            & "     MGS.NRS_BR_CD = DZO.NRS_BR_CD                   " & vbNewLine _
            & "     AND MGS.GOODS_CD_NRS = DZO.GOODS_CD_NRS         " & vbNewLine _
            & "     AND MGS.SYS_DEL_FLG = '0'                       " & vbNewLine _
            & " LEFT JOIN                                           " & vbNewLine _
            & "   $LM_MST$..M_CUST AS MCT                           " & vbNewLine _
            & "   ON                                                " & vbNewLine _
            & "     MCT.NRS_BR_CD = DZO.NRS_BR_CD                   " & vbNewLine _
            & "     AND MCT.CUST_CD_L = DZO.CUST_CD_L               " & vbNewLine _
            & "     AND MCT.CUST_CD_M = DZO.CUST_CD_M               " & vbNewLine _
            & "     AND MCT.CUST_CD_S = '00'                        " & vbNewLine _
            & "     AND MCT.CUST_CD_SS = '00'                       " & vbNewLine _
            & " WHERE                                               " & vbNewLine _
            & "   DIT.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
            & "   AND DIT.REC_NO = @REC_NO                          " & vbNewLine _
            & "   AND DIT.SYS_DEL_FLG = '0'                         " & vbNewLine

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
    '''出力対象帳票パターン検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD660IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMD660DAC.SQL_SELECT_MPRT, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        End With
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog("LMD660DAC", "SelectMPrt", cmd)

        'SQLの発行
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
    ''' 印刷データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD660IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMD660DAC.SQL_SELECT_PRINTDATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_DATE", .Item("IDO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
        End With
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog("LMD660DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("IDO_DATE", "IDO_DATE")
        map.Add("OUTKO_NO", "OUTKO_NO")
        map.Add("INKO_NO", "INKO_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("INKO_DATE", "INKO_DATE")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ALCTD_NB", "ALCTD_NB")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_OUT", "REMARK_OUT")
        map.Add("TOU_NO_R", "TOU_NO_R")
        map.Add("SITU_NO_R", "SITU_NO_R")
        map.Add("ZONE_CD_R", "ZONE_CD_R")
        map.Add("LOCA_R", "LOCA_R")
        map.Add("IDO_KOSU", "IDO_KOSU")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD660OUT")

        Return ds

    End Function

#End Region

#Region "共通"

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
