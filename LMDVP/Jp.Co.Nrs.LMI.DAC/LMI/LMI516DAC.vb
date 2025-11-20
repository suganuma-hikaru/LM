'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI516DAC : 酢酸注文書(KHネオケム)(JNC)
'  作  成  者       :  narita
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI516DAC
''' </summary>
''' <remarks></remarks>
Public Class LMI516DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TABLE_NM
        Public Const PRT_IN As String = "LMI516IN"
        Public Const PRT_OUT As String = "LMI516OUT"
        Public Const M_RPT As String = "M_RPT"
    End Class

    ''' <summary>
    ''' 実施区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JISSI_KBN
        ''' <summary>
        ''' 未
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MI As String = "0"
        ''' <summary>
        ''' 済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SUMI As String = "1"
    End Class

#End Region 'Const

#Region "SQL"

#Region "帳票パターン取得"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = "" _
            & " SELECT" & vbNewLine _
            & "   ISNULL(MR2.NRS_BR_CD, MR1.NRS_BR_CD) AS NRS_BR_CD," & vbNewLine _
            & "   ISNULL(MR2.PTN_ID, MR1.PTN_ID) AS PTN_ID," & vbNewLine _
            & "   ISNULL(MR2.PTN_CD, MR1.PTN_CD) AS PTN_CD," & vbNewLine _
            & "   ISNULL(MR2.RPT_ID, MR1.RPT_ID) AS RPT_ID" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_MST$..M_RPT MR1" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_CUST_RPT MCR" & vbNewLine _
            & "   ON  MCR.NRS_BR_CD = MR1.NRS_BR_CD" & vbNewLine _
            & "   AND MCR.CUST_CD_L = @CUST_CD_L" & vbNewLine _
            & "   AND MCR.CUST_CD_M = '00'" & vbNewLine _
            & "   AND MCR.CUST_CD_S = '00'" & vbNewLine _
            & "   AND MCR.PTN_ID = MR1.PTN_ID" & vbNewLine _
            & "   AND MCR.SYS_DEL_FLG = '0'" & vbNewLine _
            & " LEFT JOIN $LM_MST$..M_RPT MR2" & vbNewLine _
            & "   ON  MR2.NRS_BR_CD = MCR.NRS_BR_CD" & vbNewLine _
            & "   AND MR2.PTN_ID = MCR.PTN_ID" & vbNewLine _
            & "   AND MR2.PTN_CD = MCR.PTN_CD" & vbNewLine _
            & "   AND MR2.SYS_DEL_FLG = '0'" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   MR1.NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND MR1.PTN_ID = 'D9'" & vbNewLine _
            & "   AND MR1.STANDARD_FLAG = '01'" & vbNewLine _
            & "   AND MR1.SYS_DEL_FLG = '0'" & vbNewLine

#End Region

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINT As String = "" _
            & " SELECT" & vbNewLine _
            & "   0 AS SORT_KBN," & vbNewLine _
            & "   HED.PRTFLG_SUB," & vbNewLine _
            & "   HED.SR_DEN_NO," & vbNewLine _
            & "   MEI.SURY_REQ," & vbNewLine _
            & "   KB1.KBN_NM1 AS TEL," & vbNewLine _
            & "   KB1.KBN_NM2 AS FAX," & vbNewLine _
            & "   HED.OUTKA_DATE_A," & vbNewLine _
            & "   MEI.JYUCHU_GOODS_CD" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED" & vbNewLine _
            & " INNER JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC MEI" & vbNewLine _
            & "   ON   MEI.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  MEI.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND  MEI.EDI_CTL_NO = HED.EDI_CTL_NO" & vbNewLine _
            & "   AND  MEI.INOUT_KB = HED.INOUT_KB" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB1" & vbNewLine _
            & "   ON   KB1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB1.KBN_GROUP_CD = 'J027'" & vbNewLine _
            & "   AND  KB1.KBN_CD = '01'" & vbNewLine _
            & " WHERE" & vbNewLine _
            & " HED.SYS_DEL_FLG = '0'" & vbNewLine _
            & " AND HED.MOD_KBN NOT IN ('E', 'L')" & vbNewLine _
            & " AND HED.OLD_DATA_FLG = ''" & vbNewLine _
            & " AND HED.PRTFLG_SUB = '1'" & vbNewLine _
            & " AND HED.EDI_CTL_NO <> @EDI_CTL_NO" & vbNewLine _
            & " AND SUBSTRING(HED.OUTKA_DATE_A,1,6) = @OUTKA_DATE_A" & vbNewLine _
            & " AND HED.NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & " AND HED.DATA_KIND = @DATA_KIND" & vbNewLine _
            & " AND HED.OUTKA_POSI_BU_CD_PA = @OUTKA_POSI_BU_CD_PA" & vbNewLine _
            & " AND MEI.JYUCHU_GOODS_CD = @JYUCHU_GOODS_CD" & vbNewLine _
            & " UNION ALL" & vbNewLine _
            & " SELECT" & vbNewLine _
            & "   1 AS SORT_KBN," & vbNewLine _
            & "   '0' AS PRTFLG_SUB," & vbNewLine _
            & "   HED.SR_DEN_NO," & vbNewLine _
            & "   MEI.SURY_REQ," & vbNewLine _
            & "   KB1.KBN_NM1 AS TEL," & vbNewLine _
            & "   KB1.KBN_NM2 AS FAX," & vbNewLine _
            & "   HED.OUTKA_DATE_A," & vbNewLine _
            & "   MEI.JYUCHU_GOODS_CD" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED" & vbNewLine _
            & " INNER JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC MEI" & vbNewLine _
            & "   ON   MEI.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  MEI.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND  MEI.EDI_CTL_NO = HED.EDI_CTL_NO" & vbNewLine _
            & "   AND  MEI.INOUT_KB = HED.INOUT_KB" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB1" & vbNewLine _
            & "   ON   KB1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB1.KBN_GROUP_CD = 'J027'" & vbNewLine _
            & "   AND  KB1.KBN_CD = '01'" & vbNewLine _
            & " WHERE" & vbNewLine _
            & " HED.SYS_DEL_FLG = '0'" & vbNewLine _
            & " AND HED.MOD_KBN NOT IN ('E', 'L')" & vbNewLine _
            & " AND HED.OLD_DATA_FLG = ''" & vbNewLine _
            & " AND HED.EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & " AND HED.NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & " AND HED.DATA_KIND = @DATA_KIND" & vbNewLine _
            & " AND HED.SR_DEN_NO = @SR_DEN_NO" & vbNewLine _
            & " AND HED.OUTKA_POSI_BU_CD_PA = @OUTKA_POSI_BU_CD_PA" & vbNewLine _
            & " AND MEI.JYUCHU_GOODS_CD = @JYUCHU_GOODS_CD" & vbNewLine _
            & " ORDER BY" & vbNewLine _
            & " SORT_KBN ," & vbNewLine _
            & " HED.OUTKA_DATE_A," & vbNewLine _
            & " HED.SR_DEN_NO" & vbNewLine _

#End Region

#Region "プリントフラグ更新"

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_PRTFLG As String = "" _
        & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
        & " SET" & vbNewLine _
        & "   PRTFLG_SUB = @PRTFLG_SUB," & vbNewLine _
        & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
        & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
        & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
        & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
        & " WHERE" & vbNewLine _
        & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
        & "   AND SR_DEN_NO = @SR_DEN_NO" & vbNewLine _
        & "   AND DATA_KIND = @DATA_KIND" & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'" & vbNewLine _
        & "   AND MOD_KBN NOT IN ('E', 'L')" & vbNewLine _
        & "   AND OLD_DATA_FLG = ''" & vbNewLine _
        & "   AND PRTFLG_SUB <> '1'" & vbNewLine

#End Region

#End Region 'SQL

#Region "Field"

    ''' <summary>
    ''' DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region 'Field

#Region "Method"

#Region "共通処理"

    ''' <summary>
    ''' 共通処理：スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#Region "帳票パターン取得"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.PRT_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI516DAC.SQL_SELECT_MPRT)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        If reader.HasRows() Then
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("PTN_ID", "PTN_ID")
            map.Add("PTN_CD", "PTN_CD")
            map.Add("RPT_ID", "RPT_ID")
            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.M_RPT)
        End If

        reader.Close()

        Return ds

    End Function

#End Region

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.PRT_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI516DAC.SQL_SELECT_PRINT)

        '条件およびパラメータの設定
        Call Me.SelectPrintDataSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("SURY_REQ", "SURY_REQ")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("PRTFLG_SUB", "PRTFLG_SUB")
        map.Add("JYUCHU_GOODS_CD", "JYUCHU_GOODS_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.PRT_OUT)

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ取得：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectPrintDataSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件
        With Me._Row
            'ＥＤＩ管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))

            '出荷日(年月のみ)@OUTKA_DATE_A
            whereStr = .Item("OUTKA_DATE_A").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_A", whereStr.Substring(0, 6), DBDataType.CHAR))

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            'データ種別
            whereStr = .Item("DATA_KIND").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", whereStr, DBDataType.CHAR))

            '送受信伝票番号
            whereStr = .Item("SR_DEN_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", whereStr, DBDataType.CHAR))

            '出荷場所部門コード
            whereStr = .Item("OUTKA_POSI_BU_CD_PA").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_CD_PA", whereStr, DBDataType.CHAR))

            '商品コード
            whereStr = .Item("JYUCHU_GOODS_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYUCHU_GOODS_CD", whereStr, DBDataType.CHAR))

        End With

    End Sub
#End Region

#Region "プリントフラグ更新"

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrtFlg(ByVal ds As DataSet) As DataSet

        For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.PRT_IN).Rows.Count - 1
            'DataSetのIN情報を取得
            Dim inTbl As DataTable = ds.Tables(TABLE_NM.PRT_IN)

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(dtIdx)

            'SQLの作成
            Me._StrSql = New StringBuilder()
            Me._StrSql.Append(LMI516DAC.SQL_UPDATE_PRTFLG)

            'SQLのコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの設定
            Me._SqlPrmList = New ArrayList()
            With Me._Row
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG_SUB", JISSI_KBN.SUMI, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", .Item("SR_DEN_NO").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
            End With

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        Next

        Return ds

    End Function

#End Region

#End Region 'Method

End Class
