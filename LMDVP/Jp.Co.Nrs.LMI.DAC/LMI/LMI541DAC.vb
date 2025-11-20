'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI541DAC : オフライン出荷 納品書(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI510DAC
''' </summary>
''' <remarks></remarks>
Public Class LMI541DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

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

#End Region ' "Const"

#Region "SQL"

#Region "帳票パターン取得 SQL"

    ''' <summary>
    ''' 帳票パターン取得 SELECT 句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT_SELECT As String = "" _
            & "SELECT                                                   " & vbNewLine _
            & "      @NRS_BR_CD                            AS NRS_BR_CD " & vbNewLine _
            & "    , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_ID   " & vbNewLine _
            & "        ELSE MR3.PTN_ID                                  " & vbNewLine _
            & "      END                                   AS PTN_ID    " & vbNewLine _
            & "    , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD   " & vbNewLine _
            & "        ELSE MR3.PTN_CD                                  " & vbNewLine _
            & "      END                                   AS PTN_CD    " & vbNewLine _
            & "    , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
            & "        ELSE MR3.RPT_ID                                  " & vbNewLine _
            & "      END                                   AS RPT_ID    " & vbNewLine _
            & ""

#End Region

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINT_SELECT As String = "" _
            & "SELECT                                                 " & vbNewLine _
            & "      CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID " & vbNewLine _
            & "        ELSE MR3.RPT_ID                                " & vbNewLine _
            & "      END        AS RPT_ID                             " & vbNewLine _
            & "    , OFFLN.OFFLINE_NO                                 " & vbNewLine _
            & "    , OFFLN.SHUBETSU                                   " & vbNewLine _
            & "    , OFFLN.OUTKA_DATE                                 " & vbNewLine _
            & "    , OFFLN.ARR_DATE                                   " & vbNewLine _
            & "    , OFFLN.ZIP                                        " & vbNewLine _
            & "    , OFFLN.DEST_AD                                    " & vbNewLine _
            & "    , OFFLN.COMP_NM                                    " & vbNewLine _
            & "    , OFFLN.BUSYO_NM                                   " & vbNewLine _
            & "    , OFFLN.TANTO_NM                                   " & vbNewLine _
            & "    , OFFLN.TEL                                        " & vbNewLine _
            & "    , OFFLN.GOODS_NM                                   " & vbNewLine _
            & "    , OFFLN.LOT_NO                                     " & vbNewLine _
            & "    , OFFLN.INOUTKA_NB                                 " & vbNewLine _
            & "    , OFFLN.ONDO                                       " & vbNewLine _
            & "    , OFFLN.DOKUGEKI                                   " & vbNewLine _
            & "    , OFFLN.REMARK                                     " & vbNewLine _
            & "    , OFFLN.HAISO                                      " & vbNewLine _
            & ""

#End Region ' "印刷データ取得 SQL"

#Region "帳票パターン取得/印刷データ取得 共通 SQL"

    Private Const SQL_SELECT_PRINT_FROM As String = "" _
            & "FROM                                       " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_FJF_OFF OFFLN " & vbNewLine _
            & "LEFT JOIN                                  " & vbNewLine _
            & "    $LM_MST$..M_CUST CUST                  " & vbNewLine _
            & "        ON  CUST.NRS_BR_CD = @NRS_BR_CD    " & vbNewLine _
            & "        AND CUST.CUST_CD_L = @CUST_CD_L    " & vbNewLine _
            & "        AND CUST.CUST_CD_M = @CUST_CD_M    " & vbNewLine _
            & "        AND CUST.CUST_CD_S = '00'          " & vbNewLine _
            & "        AND CUST.CUST_CD_SS = '00'         " & vbNewLine _
            & "        AND CUST.SYS_DEL_FLG = '0'         " & vbNewLine _
            & "-- 荷主帳票パターン                        " & vbNewLine _
            & "LEFT JOIN                                  " & vbNewLine _
            & "    $LM_MST$..M_CUST_RPT MCR1              " & vbNewLine _
            & "        ON  MCR1.NRS_BR_CD = @NRS_BR_CD    " & vbNewLine _
            & "        AND MCR1.CUST_CD_L = @CUST_CD_L    " & vbNewLine _
            & "        AND MCR1.CUST_CD_M = @CUST_CD_M    " & vbNewLine _
            & "        AND MCR1.CUST_CD_S = '00'          " & vbNewLine _
            & "        AND MCR1.PTN_ID = 'F0'             " & vbNewLine _
            & "-- 荷主帳票パターンに紐付く帳票パターン    " & vbNewLine _
            & "LEFT JOIN                                  " & vbNewLine _
            & "    $LM_MST$..M_RPT MR1                    " & vbNewLine _
            & "        ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD " & vbNewLine _
            & "        AND MR1.PTN_ID = MCR1.PTN_ID       " & vbNewLine _
            & "        AND MR1.PTN_CD = MCR1.PTN_CD       " & vbNewLine _
            & "        AND MR1.SYS_DEL_FLG = '0'          " & vbNewLine _
            & "-- 存在しない場合の帳票パターン            " & vbNewLine _
            & "LEFT JOIN                                  " & vbNewLine _
            & "    $LM_MST$..M_RPT MR3                    " & vbNewLine _
            & "        ON  MR3.NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
            & "        AND MR3.PTN_ID = 'F0'              " & vbNewLine _
            & "        AND MR3.STANDARD_FLAG = '01'       " & vbNewLine _
            & "        AND MR3.SYS_DEL_FLG = '0'          " & vbNewLine _
            & ""

    Private Const SQL_SELECT_PRINT_WHERE As String = "" _
            & "WHERE                       " & vbNewLine _
            & "    OFFLN.KEY_NO = @KEY_NO  " & vbNewLine _
            & "AND OFFLN.SYS_DEL_FLG = '0' " & vbNewLine _
            & ""

    Private Const SQL_SELECT_PRINT_ORDER_BY As String = "" _
            & "ORDER BY             " & vbNewLine _
            & "    OFFLN.OFFLINE_NO " & vbNewLine _
            & ""

#End Region ' "帳票パターン取得/印刷データ取得 共通 SQL"

#End Region ' "SQL"

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

#End Region ' "Field"

#Region "Method"

#Region "帳票パターン取得"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI540IN_PRINT")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_MPRT_SELECT)
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_PRINT_FROM)
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_PRINT_WHERE)

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの設定
            Me._SqlPrmList = New ArrayList()
            Call SetSqlParamSelect()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データの格納先をマッピング
                Dim map As Hashtable = New Hashtable()
                If reader.HasRows() Then
                    map.Add("NRS_BR_CD", "NRS_BR_CD")
                    map.Add("PTN_ID", "PTN_ID")
                    map.Add("PTN_CD", "PTN_CD")
                    map.Add("RPT_ID", "RPT_ID")
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")
                End If

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#End Region ' "帳票パターン取得"

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI540IN_PRINT")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_PRINT_SELECT)
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_PRINT_FROM)
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_PRINT_WHERE)
        Me._StrSql.Append(LMI541DAC.SQL_SELECT_PRINT_ORDER_BY)

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの設定
            Me._SqlPrmList = New ArrayList()
            Call SetSqlParamSelect()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データ格納先のマッピング
                Dim map As Hashtable = New Hashtable()
                Call SetFieldNamePrintData(map)
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI541OUT")

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#Region "取得データ格納先のマッピング"

    ''' <summary>
    ''' 印刷データ取得
    ''' 取得データ格納先のマッピング
    ''' </summary>
    ''' <param name="map"></param>
    Private Sub SetFieldNamePrintData(ByVal map As Hashtable)

        map.Add("RPT_ID", "RPT_ID")
        map.Add("OFFLINE_NO", "OFFLINE_NO")
        map.Add("SHUBETSU", "SHUBETSU")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("ARR_DATE", "ARR_DATE")
        map.Add("ZIP", "ZIP")
        map.Add("DEST_AD_1", "DEST_AD")
        map.Add("COMP_NM_1", "COMP_NM")
        map.Add("BUSYO_NM", "BUSYO_NM")
        map.Add("TANTO_NM", "TANTO_NM")
        map.Add("TEL", "TEL")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("INOUTKA_NB", "INOUTKA_NB")
        map.Add("ONDO", "ONDO")
        map.Add("DOKUGEKI", "DOKUGEKI")
        map.Add("REMARK", "REMARK")
        map.Add("HAISO", "HAISO")

    End Sub

#End Region ' "取得データ格納先のマッピング"

#End Region ' "印刷データ取得"

#Region "帳票パターン取得/印刷データ取得 共通 パラメータ設定"

    ''' <summary>
    ''' 帳票パターン取得/印刷データ取得 共通 パラメータ設定
    ''' </summary>
    Private Sub SetSqlParamSelect()

        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEY_NO", Me.NullConvertZero(.Item("KEY_NO")), DBDataType.NUMERIC))
        End With

    End Sub

#End Region ' "帳票パターン取得/印刷データ取得 共通 パラメータ設定"

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

#Region "編集・変換"

#Region "Null変換"

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

        Return value

    End Function

#End Region ' "Null変換"

#End Region ' "編集・変換"

#End Region ' "共通処理"

#End Region ' "Method"

End Class
