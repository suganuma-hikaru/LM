' ==========================================================================
'  システム名       : LM
'  サブシステム名   : LMD       : 在庫管理
'  プログラムID     : LMD670    : 強制出庫在庫一覧
'  作  成  者       : hori
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD670DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD670DAC
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
            & "   AND MR1.PTN_ID = 'DB'                             " & vbNewLine _
            & "   AND MR1.STANDARD_FLAG = '01'                      " & vbNewLine _
            & "   AND MR1.SYS_DEL_FLG = '0'                         " & vbNewLine

    ''' <summary>
    ''' 印刷データ検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINTDATA As String = "" _
            & " SELECT                                              " & vbNewLine _
            & "   DZT.NRS_BR_CD,                                    " & vbNewLine _
            & "   @IDO_DATE AS IDO_DATE,                            " & vbNewLine _
            & "   MGS.GOODS_CD_CUST,                                " & vbNewLine _
            & "   MGS.GOODS_NM_1 AS GOODS_NM,                       " & vbNewLine _
            & "   DZT.LOT_NO,                                       " & vbNewLine _
            & "   DZT.IRIME,                                        " & vbNewLine _
            & "   MGS.STD_IRIME_UT,                                 " & vbNewLine _
            & "   DZT.INKO_DATE,                                    " & vbNewLine _
            & "   DZT.ALLOC_CAN_NB,                                 " & vbNewLine _
            & "   MGS.PKG_UT,                                       " & vbNewLine _
            & "   DZT.ALCTD_NB,                                     " & vbNewLine _
            & "   DZT.PORA_ZAI_NB,                                  " & vbNewLine _
            & "   DZT.TOU_NO,                                       " & vbNewLine _
            & "   DZT.SITU_NO,                                      " & vbNewLine _
            & "   DZT.ZONE_CD,                                      " & vbNewLine _
            & "   DZT.LOCA,                                         " & vbNewLine _
            & "   DZT.REMARK,                                       " & vbNewLine _
            & "   DZT.REMARK_OUT                                    " & vbNewLine _
            & " FROM                                                " & vbNewLine _
            & "   $LM_TRN$..D_ZAI_TRS DZT                           " & vbNewLine _
            & " LEFT JOIN                                           " & vbNewLine _
            & "   $LM_MST$..M_GOODS MGS                             " & vbNewLine _
            & "   ON                                                " & vbNewLine _
            & "     MGS.NRS_BR_CD = DZT.NRS_BR_CD                   " & vbNewLine _
            & "     AND MGS.GOODS_CD_NRS = DZT.GOODS_CD_NRS         " & vbNewLine _
            & "     AND MGS.SYS_DEL_FLG = '0'                       " & vbNewLine _
            & " WHERE                                               " & vbNewLine _
            & "   DZT.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
            & "   AND DZT.ZAI_REC_NO = @ZAI_REC_NO                  " & vbNewLine _
            & "   AND DZT.SYS_DEL_FLG = '0'                         " & vbNewLine _
            & " ORDER BY                                            " & vbNewLine _
            & " DZT.WH_CD,DZT.TOU_NO,DZT.SITU_NO,DZT.ZONE_CD,DZT.LOCA,MGS.GOODS_NM_1,DZT.LOT_NO " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMD670IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMD670DAC.SQL_SELECT_MPRT, Me._Row.Item("NRS_BR_CD").ToString())

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
        MyBase.Logger.WriteSQLLog("LMD670DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMD670IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMD670DAC.SQL_SELECT_PRINTDATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IDO_DATE", .Item("IDO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        End With
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog("LMD670DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("IDO_DATE", "IDO_DATE")
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
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD670OUT")

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
