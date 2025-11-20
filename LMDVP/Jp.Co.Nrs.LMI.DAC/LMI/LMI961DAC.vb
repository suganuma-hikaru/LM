' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI961    : GLIS見積情報照会（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI961DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI961DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_KENSAKU_IN As String = "LMI961KENSAKU_IN"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GAMEN_IN As String = "LMI961GAMEN_IN"

#End Region '制御用

#Region "検索処理SQL"

    Private Const SQL_SELECT_SEARCH_DATA As String =
          "SELECT                                                                      " & vbNewLine _
        & "       ISNULL(RIGHT(CMD.SKU_NUMBER,8),'') AS SKU_NUMBER                     " & vbNewLine _
        & "      ,ISNULL(STP1.LOCATION_ID,'') AS PLACE_CD_A_HWL                        " & vbNewLine _
        & "      ,ISNULL(STP2.LOCATION_ID,'') AS STAR_PLACE_CD_HWL                     " & vbNewLine _
        & "      ,LEFT(SPM.CON+',',CHARINDEX(',',SPM.CON+',')-1) AS CON                " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                                    " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                           " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                            " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD                           " & vbNewLine _
        & "    ON CMD.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND CMD.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND RIGHT(CMD.SKU_NUMBER,8) <> '10305599'                                " & vbNewLine _
        & "   AND CMD.SKU_NUMBER <> ''                                                 " & vbNewLine _
        & "   AND CMD.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1                          " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                                         " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                                       " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'                                                 " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2                          " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                                         " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                                       " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'                                                 " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                             " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                           " & vbNewLine

#End Region '検索処理SQL

#Region "受注作成処理SQL"

    Private Const SQL_SELECT_EDI_DATA As String =
          "SELECT                                                                                " & vbNewLine _
        & "       ISNULL(SPM.SHIPMENT_ID,'') AS SHIPMENT_ID                                      " & vbNewLine _
        & "      ,ISNULL(RIGHT(CMD1.SKU_NUMBER,8),'') AS SKU_NUMBER                              " & vbNewLine _
        & "      ,ISNULL(CMD1.MAXIMUM_WEIGHT,'') AS MAXIMUM_WEIGHT                               " & vbNewLine _
        & "      ,ISNULL(CMD2.NUMBER_PIECES,'') AS NUMBER_PIECES                                 " & vbNewLine _
        & "      ,ISNULL(STP1.LOCATION_ID,'') AS PLACE_CD_A_HWL_SAP                              " & vbNewLine _
        & "      ,ISNULL(LEFT(STP1.SCHEDULE_START_DATE_TIME,8),'') AS SCHEDULE_START_DATE_TIME_P " & vbNewLine _
        & "      ,ISNULL(STP2.LOCATION_ID,'') AS STAR_PLACE_CD_HWL_SAP                           " & vbNewLine _
        & "      ,ISNULL(LEFT(STP2.SCHEDULE_START_DATE_TIME,8),'') AS SCHEDULE_START_DATE_TIME_D " & vbNewLine _
        & "      ,LEFT(SPM.CON+',',CHARINDEX(',',SPM.CON+',')-1) AS CON                          " & vbNewLine _
        & "      ,ISNULL(STP2.STOP_NOTE,'') AS STOP_NOTE                                         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                                              " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                                     " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                                    " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                                                  " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                                      " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD1                                    " & vbNewLine _
        & "    ON CMD1.CRT_DATE = HED.CRT_DATE                                                   " & vbNewLine _
        & "   AND CMD1.FILE_NAME = HED.FILE_NAME                                                 " & vbNewLine _
        & "   AND RIGHT(CMD1.SKU_NUMBER,8) <> '10305599'                                         " & vbNewLine _
        & "   AND CMD1.SKU_NUMBER <> ''                                                          " & vbNewLine _
        & "   AND CMD1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD2                                    " & vbNewLine _
        & "    ON CMD2.CRT_DATE = HED.CRT_DATE                                                   " & vbNewLine _
        & "   AND CMD2.FILE_NAME = HED.FILE_NAME                                                 " & vbNewLine _
        & "   AND (   RIGHT(CMD2.SKU_NUMBER,8) = '10305599'                                      " & vbNewLine _
        & "        OR CMD2.SKU_NUMBER = ''                  )                                    " & vbNewLine _
        & "   AND CMD2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1                                    " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                                                   " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                                                 " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'                                                           " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2                                    " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                                                   " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                                                 " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'                                                           " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                                       " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                                     " & vbNewLine

#End Region '受注作成処理SQL

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow

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

#Region "検索処理"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>対象データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI961DAC.TABLE_NM_GAMEN_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim sSql As String = LMI961DAC.SQL_SELECT_SEARCH_DATA

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSearchParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(sSql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GOODS_CD_HWL", "SKU_NUMBER")
        map.Add("FROM_CD_HWL", "PLACE_CD_A_HWL")
        map.Add("TO_CD_HWL", "STAR_PLACE_CD_HWL")
        map.Add("TRN_ORD_NO", "CON")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GLZ9300IN_EST_LIST")

        Return ds

    End Function

#End Region '検索処理

#Region "受注作成処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ハネウェルＥＤＩ受信データ取得SQLの構築・発行</remarks>
    Private Function JuchuSakusei(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI961DAC.TABLE_NM_GAMEN_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Dim sSql As String = LMI961DAC.SQL_SELECT_EDI_DATA

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetEDIParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(sSql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("LOAD_NO", "SHIPMENT_ID")
        map.Add("GOODS_CD_HWL_SAP", "SKU_NUMBER")
        map.Add("GROSS_WEIGHT", "MAXIMUM_WEIGHT")
        map.Add("ORAP_CNT", "NUMBER_PIECES")
        map.Add("FROM_CD_HWL_SAP", "PLACE_CD_A_HWL_SAP")
        map.Add("FROM_DATE", "SCHEDULE_START_DATE_TIME_P")
        map.Add("TO_CD_HWL_SAP", "STAR_PLACE_CD_HWL_SAP")
        map.Add("TO_DATE", "SCHEDULE_START_DATE_TIME_D")
        map.Add("STAR_REM", "CON")
        map.Add("TRN_ORD_NO", "CON")
        map.Add("TRUCK_ARRG_REM", "STOP_NOTE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GLZ9300IN_BOOKING_DATA")

        Return ds

    End Function

#End Region '受注作成処理

#Region "パラメータ設定"

#Region "システム共通項目"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetDataUpdateParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataUpdateParameter(ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

#End Region


#Region "受注作成処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ検索のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetEDIParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

#End Region


#End Region 'パラメータ設定

#Region "ユーティリティ"

    ''' <summary>
    ''' スキーマ名取得
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cnt">件数</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        'SQLの発行
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region 'ユーティリティ

#End Region 'Method

End Class
