' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM520    : 届先確認書
'  作  成  者       :  [KOBAYASHI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM520DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM520DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "SELECT                                               " & vbNewLine _
                                            & "    DST.NRS_BR_CD        AS NRS_BR_CD                " & vbNewLine _
                                            & "   ,'OL'                 AS PTN_ID                   " & vbNewLine _
                                            & "   ,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD " & vbNewLine _
                                            & "         ELSE MR2.PTN_CD                             " & vbNewLine _
                                            & "    END                  AS PTN_CD                   " & vbNewLine _
                                            & "   ,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID " & vbNewLine _
                                            & "         ELSE MR2.RPT_ID                             " & vbNewLine _
                                            & "    END                  AS RPT_ID                   " & vbNewLine _
                                            & "   ,DST.DEST_NM + '様'   AS DEST_NM                  " & vbNewLine _
                                            & "   ,DST.ZIP              AS ZIP                      " & vbNewLine _
                                            & "   ,DST.AD_1 + DST.AD_2  AS AD_1_2                   " & vbNewLine _
                                            & "   ,DST.AD_3             AS AD_3                     " & vbNewLine _
                                            & "   ,DST.DEST_CD          AS DEST_CD                  " & vbNewLine _
                                            & "   ,DST.CUST_CD_L        AS CUST_CD_L                " & vbNewLine _
                                            & "   ,CST.CUST_NM_L        AS CUST_NM_L                " & vbNewLine _
                                            & "   ,CDT.SET_NAIYO        AS CUST_NM_M                " & vbNewLine _
                                            & "   ,CDT.SET_NAIYO_2      AS CUST_TEL                 " & vbNewLine _
                                            & "   ,'株式会社 日陸'       AS NRS_NM                   " & vbNewLine _
                                            & "   ,NBR.TEL              AS NRS_TEL                  " & vbNewLine _
                                            & "FROM                                                 " & vbNewLine _
                                            & "    M_DEST AS DST                                    " & vbNewLine _
                                            & "LEFT JOIN M_CUST AS CST                              " & vbNewLine _
                                            & "ON  CST.NRS_BR_CD = DST.NRS_BR_CD                    " & vbNewLine _
                                            & "AND CST.CUST_CD_L = DST.CUST_CD_L                    " & vbNewLine _
                                            & "AND CST.CUST_CD_M  = '00'                            " & vbNewLine _
                                            & "AND CST.CUST_CD_S  = '00'                            " & vbNewLine _
                                            & "AND CST.CUST_CD_SS = '00'                            " & vbNewLine _
                                            & "AND CST.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "LEFT JOIN M_CUST_DETAILS AS CDT                      " & vbNewLine _
                                            & "ON  CDT.NRS_BR_CD = DST.NRS_BR_CD                    " & vbNewLine _
                                            & "AND CDT.CUST_CD = DST.CUST_CD_L                      " & vbNewLine _
                                            & "AND CDT.SUB_KB  = '61'                               " & vbNewLine _
                                            & "AND CDT.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "LEFT JOIN M_NRS_BR AS NBR                            " & vbNewLine _
                                            & "ON  NBR.NRS_BR_CD = DST.NRS_BR_CD                    " & vbNewLine _
                                            & "AND NBR.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "LEFT JOIN M_CUST_RPT AS MCR                          " & vbNewLine _
                                            & "ON  MCR.NRS_BR_CD = DST.NRS_BR_CD                    " & vbNewLine _
                                            & "AND MCR.CUST_CD_L = DST.CUST_CD_L                    " & vbNewLine _
                                            & "AND MCR.CUST_CD_M = '00'                             " & vbNewLine _
                                            & "AND MCR.CUST_CD_S = '00'                             " & vbNewLine _
                                            & "AND MCR.PTN_ID = 'OL'                                " & vbNewLine _
                                            & "AND MCR.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "LEFT JOIN M_RPT AS MR1                               " & vbNewLine _
                                            & "ON  MR1.NRS_BR_CD = MCR.NRS_BR_CD                    " & vbNewLine _
                                            & "AND MR1.PTN_ID = MCR.PTN_ID                          " & vbNewLine _
                                            & "AND MR1.PTN_CD = MCR.PTN_CD                          " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "LEFT JOIN M_RPT AS MR2                               " & vbNewLine _
                                            & "ON  MR2.NRS_BR_CD = DST.NRS_BR_CD                    " & vbNewLine _
                                            & "AND MR2.PTN_ID = 'OL'                                " & vbNewLine _
                                            & "AND MR2.STANDARD_FLAG = '01'                         " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                            & "WHERE                                                " & vbNewLine _
                                            & "    DST.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                            & "AND DST.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
                                            & "AND DST.DEST_CD = @DEST_CD                           " & vbNewLine _
                                            & "AND DST.SYS_DEL_FLG = '0'                            " & vbNewLine


#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ一覧表データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品マスタ一覧表データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM520IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構成
        Dim sql As String = Me.SetSchemaNm(LMM520DAC.SQL_SELECT_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetParamSearch()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM520DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1_2", "AD_1_2")
        map.Add("AD_3", "AD_3")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_TEL", "CUST_TEL")
        map.Add("NRS_NM", "NRS_NM")
        map.Add("NRS_TEL", "NRS_TEL")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM520OUT")

        Return ds

    End Function

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

#Region "パラメータの設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSearch()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))

        End With


    End Sub

#End Region

#End Region

End Class
