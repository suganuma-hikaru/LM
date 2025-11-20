' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140  : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI140DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI140DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索"

    ''' <summary>
    ''' データ検索(SELECT句 件数取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = "" _
        & "SELECT COUNT(*) AS SELECT_CNT    " & vbNewLine _
        & ""

    ''' <summary>
    ''' データ検索(SELECT句 項目取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SELECT As String = "" _
        & "SELECT                                   " & vbNewLine _
        & "   EDI.DEL_KB                            " & vbNewLine _
        & "  ,EDI.CRT_DATE                          " & vbNewLine _
        & "  ,EDI.FILE_NAME                         " & vbNewLine _
        & "  ,EDI.REC_NO                            " & vbNewLine _
        & "  ,EDI.GYO_NO                            " & vbNewLine _
        & "  ,EDI.NRS_BR_CD                         " & vbNewLine _
        & "  ,EDI.NRS_PROC_NO                       " & vbNewLine _
        & "  ,EDI.PROC_TYPE                         " & vbNewLine _
        & "  ,EDI.PROC_KBN                          " & vbNewLine _
        & "  ,EDI.PRTFLG                            " & vbNewLine _
        & "  ,EDI.JISSEKI_FUYO                      " & vbNewLine _
        & "  ,EDI.OBIC_SHUBETU                      " & vbNewLine _
        & "  ,EDI.OBIC_TORIHIKI_KBN                 " & vbNewLine _
        & "  ,EDI.OBIC_DENP_NO                      " & vbNewLine _
        & "  ,EDI.OBIC_GYO_NO                       " & vbNewLine _
        & "  ,EDI.OBIC_DETAIL_NO                    " & vbNewLine _
        & "  ,EDI.PROC_DATE                         " & vbNewLine _
        & "  ,EDI.OUTKA_WH_TYPE                     " & vbNewLine _
        & "  ,EDI.OUTKA_CUST_CD_L                   " & vbNewLine _
        & "  ,EDI.OUTKA_CUST_CD_M                   " & vbNewLine _
        & "  ,EDI.INKA_WH_TYPE                      " & vbNewLine _
        & "  ,EDI.INKA_CUST_CD_L                    " & vbNewLine _
        & "  ,EDI.INKA_CUST_CD_M                    " & vbNewLine _
        & "  ,EDI.BEFORE_GOODS_RANK                 " & vbNewLine _
        & "  ,EDI.AFTER_GOODS_RANK                  " & vbNewLine _
        & "  ,EDI.GOODS_CD                          " & vbNewLine _
        & "  ,EDI.GOODS_NM                          " & vbNewLine _
        & "  ,EDI.NB                                " & vbNewLine _
        & "  ,EDI.LOT_NO                            " & vbNewLine _
        & "  ,EDI.LT_DATE                           " & vbNewLine _
        & "  ,EDI.REMARK                            " & vbNewLine _
        & "  ,EDI.YOBI1                             " & vbNewLine _
        & "  ,EDI.YOBI2                             " & vbNewLine _
        & "  ,EDI.YOBI3                             " & vbNewLine _
        & "  ,EDI.YOBI4                             " & vbNewLine _
        & "  ,EDI.YOBI5                             " & vbNewLine _
        & "  ,EDI.RECORD_STATUS                     " & vbNewLine _
        & "  ,EDI.JISSEKI_SHORI_FLG                 " & vbNewLine _
        & "  ,EDI.JISSEKI_USER                      " & vbNewLine _
        & "  ,EDI.JISSEKI_DATE                      " & vbNewLine _
        & "  ,EDI.JISSEKI_TIME                      " & vbNewLine _
        & "  ,EDI.SEND_USER                         " & vbNewLine _
        & "  ,EDI.SEND_DATE                         " & vbNewLine _
        & "  ,EDI.SEND_TIME                         " & vbNewLine _
        & "  ,EDI.DELETE_USER                       " & vbNewLine _
        & "  ,EDI.DELETE_DATE                       " & vbNewLine _
        & "  ,EDI.DELETE_TIME                       " & vbNewLine _
        & "  ,EDI.DELETE_EDI_NO                     " & vbNewLine _
        & "  ,EDI.DELETE_EDI_NO_CHU                 " & vbNewLine _
        & "  ,EDI.PRT_USER                          " & vbNewLine _
        & "  ,EDI.PRT_DATE                          " & vbNewLine _
        & "  ,EDI.PRT_TIME                          " & vbNewLine _
        & "  ,EDI.EDI_USER                          " & vbNewLine _
        & "  ,EDI.EDI_DATE                          " & vbNewLine _
        & "  ,EDI.EDI_TIME                          " & vbNewLine _
        & "  ,EDI.UPD_USER                          " & vbNewLine _
        & "  ,EDI.UPD_DATE                          " & vbNewLine _
        & "  ,EDI.UPD_TIME                          " & vbNewLine _
        & "  ,EDI.SYS_UPD_DATE                      " & vbNewLine _
        & "  ,EDI.SYS_UPD_TIME                      " & vbNewLine _
        & "  ,KB1.KBN_NM1 AS JISSEKI_FUYO_NM        " & vbNewLine _
        & "  ,KB2.KBN_NM1 AS JISSEKI_SHORI_FLG_NM   " & vbNewLine _
        & "  ,KB3.KBN_NM1 AS PROC_TYPE_NM           " & vbNewLine _
        & "  ,KB4.KBN_NM1 AS PROC_KBN_NM            " & vbNewLine _
        & "  ,KB5.REM AS OUTKA_WH_TYPE_NM           " & vbNewLine _
        & "  ,KB6.REM AS INKA_WH_TYPE_NM            " & vbNewLine _
        & "  ,CN1.JOTAI_NM AS BEFORE_GOODS_RANK_NM  " & vbNewLine _
        & "  ,CN2.JOTAI_NM AS AFTER_GOODS_RANK_NM   " & vbNewLine _
        & ""

    ''' <summary>
    ''' データ検索(FROM句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "" _
        & "FROM                                                 " & vbNewLine _
        & "  $LM_TRN$..H_WHEDI_BAH AS EDI                       " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB1                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          KB1.KBN_GROUP_CD = 'WZ03'                  " & vbNewLine _
        & "      AND KB1.KBN_CD = (CASE EDI.JISSEKI_FUYO        " & vbNewLine _
        & "                          WHEN '0' THEN '01'         " & vbNewLine _
        & "                          WHEN '1' THEN '02'         " & vbNewLine _
        & "                          ELSE '' END                " & vbNewLine _
        & "                          )                          " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB2                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          KB2.KBN_GROUP_CD = 'H038'                  " & vbNewLine _
        & "      AND KB2.KBN_CD = (CASE EDI.JISSEKI_SHORI_FLG   " & vbNewLine _
        & "                          WHEN '1' THEN '0'          " & vbNewLine _
        & "                          WHEN '2' THEN '1'          " & vbNewLine _
        & "                          WHEN '3' THEN '1'          " & vbNewLine _
        & "                          ELSE '' END                " & vbNewLine _
        & "                          )                          " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB3                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          KB3.KBN_GROUP_CD = 'H036'                  " & vbNewLine _
        & "      AND KB3.KBN_CD = EDI.PROC_TYPE                 " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB4                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          KB4.KBN_GROUP_CD = 'H037'                  " & vbNewLine _
        & "      AND KB4.KBN_CD = EDI.PROC_KBN                  " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB5                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          KB5.KBN_GROUP_CD = 'B047'                  " & vbNewLine _
        & "      AND KB5.KBN_NM1 = EDI.OUTKA_WH_TYPE            " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS KB6                           " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          KB6.KBN_GROUP_CD = 'B047'                  " & vbNewLine _
        & "      AND KB6.KBN_NM1 = EDI.INKA_WH_TYPE             " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_CUSTCOND AS CN1                      " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          CN1.NRS_BR_CD = EDI.NRS_BR_CD              " & vbNewLine _
        & "      AND CN1.CUST_CD_L = EDI.OUTKA_CUST_CD_L        " & vbNewLine _
        & "      AND CN1.JOTAI_CD = EDI.BEFORE_GOODS_RANK       " & vbNewLine _
        & "  LEFT JOIN                                          " & vbNewLine _
        & "    $LM_MST$..M_CUSTCOND AS CN2                      " & vbNewLine _
        & "    ON                                               " & vbNewLine _
        & "          CN2.NRS_BR_CD = EDI.NRS_BR_CD              " & vbNewLine _
        & "      AND CN2.CUST_CD_L = EDI.OUTKA_CUST_CD_L        " & vbNewLine _
        & "      AND CN2.JOTAI_CD = EDI.AFTER_GOODS_RANK        " & vbNewLine _
        & ""

    ''' <summary>
    ''' データ検索(ORDER BY句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER As String = "" _
        & "ORDER BY             " & vbNewLine _
        & "  EDI.NRS_PROC_NO    " & vbNewLine _
        & ""

#End Region '検索

#Region "更新"

    ''' <summary>
    ''' 物産アニマルヘルス_倉庫内処理依頼EDIデータ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_H_WHEDI_BAH As String = "" _
        & "UPDATE $LM_TRN$..H_WHEDI_BAH SET             " & vbNewLine _
        & "   JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG    " & vbNewLine _
        & "  ,JISSEKI_USER      = @JISSEKI_USER         " & vbNewLine _
        & "  ,JISSEKI_DATE      = @JISSEKI_DATE         " & vbNewLine _
        & "  ,JISSEKI_TIME      = @JISSEKI_TIME         " & vbNewLine _
        & "  ,UPD_USER          = @UPD_USER             " & vbNewLine _
        & "  ,UPD_DATE          = @UPD_DATE             " & vbNewLine _
        & "  ,UPD_TIME          = @UPD_TIME             " & vbNewLine _
        & "  ,SYS_UPD_DATE      = @SYS_UPD_DATE         " & vbNewLine _
        & "  ,SYS_UPD_TIME      = @SYS_UPD_TIME         " & vbNewLine _
        & "  ,SYS_UPD_PGID      = @SYS_UPD_PGID         " & vbNewLine _
        & "  ,SYS_UPD_USER      = @SYS_UPD_USER         " & vbNewLine _
        & "WHERE                                        " & vbNewLine _
        & "      NRS_PROC_NO    = @NRS_PROC_NO          " & vbNewLine _
        & "  AND SYS_UPD_DATE   = @SYS_UPD_DATE_HAITA   " & vbNewLine _
        & "  AND SYS_UPD_TIME   = @SYS_UPD_TIME_HAITA   " & vbNewLine _
        & ""

#End Region '更新

#Region "登録"

    ''' <summary>
    ''' 物産アニマルヘルス_倉庫内処理実績EDIデータ登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_H_SENDWHEDI_BAH As String = "" _
        & "INSERT INTO $LM_TRN$..H_SENDWHEDI_BAH    " & vbNewLine _
        & "(                                        " & vbNewLine _
        & "   DEL_KB                                " & vbNewLine _
        & "  ,CRT_DATE                              " & vbNewLine _
        & "  ,FILE_NAME                             " & vbNewLine _
        & "  ,REC_NO                                " & vbNewLine _
        & "  ,GYO                                   " & vbNewLine _
        & "  ,NRS_BR_CD                             " & vbNewLine _
        & "  ,NRS_PROC_NO                           " & vbNewLine _
        & "  ,PROC_TYPE                             " & vbNewLine _
        & "  ,PROC_NO                               " & vbNewLine _
        & "  ,PROC_GYO                              " & vbNewLine _
        & "  ,PROC_DATE                             " & vbNewLine _
        & "  ,WH_TYPE                               " & vbNewLine _
        & "  ,BEFORE_GOODS_RANK                     " & vbNewLine _
        & "  ,AFTER_GOODS_RANK                      " & vbNewLine _
        & "  ,GOODS_CD                              " & vbNewLine _
        & "  ,GOODS_NM                              " & vbNewLine _
        & "  ,NB                                    " & vbNewLine _
        & "  ,LOT_NO                                " & vbNewLine _
        & "  ,LT_DATE                               " & vbNewLine _
        & "  ,YOBI1                                 " & vbNewLine _
        & "  ,YOBI2                                 " & vbNewLine _
        & "  ,YOBI3                                 " & vbNewLine _
        & "  ,YOBI4                                 " & vbNewLine _
        & "  ,YOBI5                                 " & vbNewLine _
        & "  ,RECORD_STATUS                         " & vbNewLine _
        & "  ,JISSEKI_SHORI_FLG                     " & vbNewLine _
        & "  ,JISSEKI_USER                          " & vbNewLine _
        & "  ,JISSEKI_DATE                          " & vbNewLine _
        & "  ,JISSEKI_TIME                          " & vbNewLine _
        & "  ,SEND_USER                             " & vbNewLine _
        & "  ,SEND_DATE                             " & vbNewLine _
        & "  ,SEND_TIME                             " & vbNewLine _
        & "  ,DELETE_USER                           " & vbNewLine _
        & "  ,DELETE_DATE                           " & vbNewLine _
        & "  ,DELETE_TIME                           " & vbNewLine _
        & "  ,DELETE_EDI_NO                         " & vbNewLine _
        & "  ,DELETE_EDI_NO_CHU                     " & vbNewLine _
        & "  ,UPD_USER                              " & vbNewLine _
        & "  ,UPD_DATE                              " & vbNewLine _
        & "  ,UPD_TIME                              " & vbNewLine _
        & "  ,SYS_ENT_DATE                          " & vbNewLine _
        & "  ,SYS_ENT_TIME                          " & vbNewLine _
        & "  ,SYS_ENT_PGID                          " & vbNewLine _
        & "  ,SYS_ENT_USER                          " & vbNewLine _
        & "  ,SYS_UPD_DATE                          " & vbNewLine _
        & "  ,SYS_UPD_TIME                          " & vbNewLine _
        & "  ,SYS_UPD_PGID                          " & vbNewLine _
        & "  ,SYS_UPD_USER                          " & vbNewLine _
        & "  ,SYS_DEL_FLG                           " & vbNewLine _
        & ") VALUES (                               " & vbNewLine _
        & "   @DEL_KB                               " & vbNewLine _
        & "  ,@CRT_DATE                             " & vbNewLine _
        & "  ,@FILE_NAME                            " & vbNewLine _
        & "  ,@REC_NO                               " & vbNewLine _
        & "  ,@GYO                                  " & vbNewLine _
        & "  ,@NRS_BR_CD                            " & vbNewLine _
        & "  ,@NRS_PROC_NO                          " & vbNewLine _
        & "  ,@PROC_TYPE                            " & vbNewLine _
        & "  ,@PROC_NO                              " & vbNewLine _
        & "  ,@PROC_GYO                             " & vbNewLine _
        & "  ,@PROC_DATE                            " & vbNewLine _
        & "  ,@WH_TYPE                              " & vbNewLine _
        & "  ,@BEFORE_GOODS_RANK                    " & vbNewLine _
        & "  ,@AFTER_GOODS_RANK                     " & vbNewLine _
        & "  ,@GOODS_CD                             " & vbNewLine _
        & "  ,@GOODS_NM                             " & vbNewLine _
        & "  ,@NB                                   " & vbNewLine _
        & "  ,@LOT_NO                               " & vbNewLine _
        & "  ,@LT_DATE                              " & vbNewLine _
        & "  ,@YOBI1                                " & vbNewLine _
        & "  ,@YOBI2                                " & vbNewLine _
        & "  ,@YOBI3                                " & vbNewLine _
        & "  ,@YOBI4                                " & vbNewLine _
        & "  ,@YOBI5                                " & vbNewLine _
        & "  ,@RECORD_STATUS                        " & vbNewLine _
        & "  ,@JISSEKI_SHORI_FLG                    " & vbNewLine _
        & "  ,@JISSEKI_USER                         " & vbNewLine _
        & "  ,@JISSEKI_DATE                         " & vbNewLine _
        & "  ,@JISSEKI_TIME                         " & vbNewLine _
        & "  ,@SEND_USER                            " & vbNewLine _
        & "  ,@SEND_DATE                            " & vbNewLine _
        & "  ,@SEND_TIME                            " & vbNewLine _
        & "  ,@DELETE_USER                          " & vbNewLine _
        & "  ,@DELETE_DATE                          " & vbNewLine _
        & "  ,@DELETE_TIME                          " & vbNewLine _
        & "  ,@DELETE_EDI_NO                        " & vbNewLine _
        & "  ,@DELETE_EDI_NO_CHU                    " & vbNewLine _
        & "  ,@UPD_USER                             " & vbNewLine _
        & "  ,@UPD_DATE                             " & vbNewLine _
        & "  ,@UPD_TIME                             " & vbNewLine _
        & "  ,@SYS_ENT_DATE                         " & vbNewLine _
        & "  ,@SYS_ENT_TIME                         " & vbNewLine _
        & "  ,@SYS_ENT_PGID                         " & vbNewLine _
        & "  ,@SYS_ENT_USER                         " & vbNewLine _
        & "  ,@SYS_UPD_DATE                         " & vbNewLine _
        & "  ,@SYS_UPD_TIME                         " & vbNewLine _
        & "  ,@SYS_UPD_PGID                         " & vbNewLine _
        & "  ,@SYS_UPD_USER                         " & vbNewLine _
        & "  ,@SYS_DEL_FLG                          " & vbNewLine _
        & ")                                        " & vbNewLine _
        & ""

#End Region '登録

#End Region 'Const

#Region "Field"

    '''' <summary>
    '''' 検索条件設定用
    '''' </summary>
    '''' <remarks></remarks>
    'Private _Row As Data.DataRow

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

    Private setWhereBr As Boolean
    Private setWhereCustL As Boolean
    Private setWhereCustM As Boolean
    Private setWhereCustS As Boolean
    Private setWhereCustSS As Boolean
    Private setWhereDateFrom As Boolean
    Private setWhereDateTo As Boolean
    Private setWhereDepart As Boolean

#End Region 'Field

#Region "Method"

#Region "検索"

    ''' <summary>
    ''' データ検索(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI140IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI140DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMI140DAC.SQL_SELECT_FROM)
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI140DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        Dim readerCnt As Integer = 0
        Do While reader.Read
            readerCnt = readerCnt + 1
        Loop
        reader.Close()

        '件数設定
        MyBase.SetResultCount(readerCnt)
        Return ds

    End Function

    ''' <summary>
    ''' データ検索(項目取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI140IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI140DAC.SQL_SELECT_SELECT)
        Me._StrSql.Append(LMI140DAC.SQL_SELECT_FROM)
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))
        Me._StrSql.Append(LMI140DAC.SQL_SELECT_ORDER)

        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI140DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO_NO", "GYO_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_PROC_NO", "NRS_PROC_NO")
        map.Add("PROC_TYPE", "PROC_TYPE")
        map.Add("PROC_KBN", "PROC_KBN")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("JISSEKI_FUYO", "JISSEKI_FUYO")
        map.Add("OBIC_SHUBETU", "OBIC_SHUBETU")
        map.Add("OBIC_TORIHIKI_KBN", "OBIC_TORIHIKI_KBN")
        map.Add("OBIC_DENP_NO", "OBIC_DENP_NO")
        map.Add("OBIC_GYO_NO", "OBIC_GYO_NO")
        map.Add("OBIC_DETAIL_NO", "OBIC_DETAIL_NO")
        map.Add("PROC_DATE", "PROC_DATE")
        map.Add("OUTKA_WH_TYPE", "OUTKA_WH_TYPE")
        map.Add("OUTKA_CUST_CD_L", "OUTKA_CUST_CD_L")
        map.Add("OUTKA_CUST_CD_M", "OUTKA_CUST_CD_M")
        map.Add("INKA_WH_TYPE", "INKA_WH_TYPE")
        map.Add("INKA_CUST_CD_L", "INKA_CUST_CD_L")
        map.Add("INKA_CUST_CD_M", "INKA_CUST_CD_M")
        map.Add("BEFORE_GOODS_RANK", "BEFORE_GOODS_RANK")
        map.Add("AFTER_GOODS_RANK", "AFTER_GOODS_RANK")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("NB", "NB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("LT_DATE", "LT_DATE")
        map.Add("REMARK", "REMARK")
        map.Add("YOBI1", "YOBI1")
        map.Add("YOBI2", "YOBI2")
        map.Add("YOBI3", "YOBI3")
        map.Add("YOBI4", "YOBI4")
        map.Add("YOBI5", "YOBI5")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("DELETE_USER", "DELETE_USER")
        map.Add("DELETE_DATE", "DELETE_DATE")
        map.Add("DELETE_TIME", "DELETE_TIME")
        map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
        map.Add("DELETE_EDI_NO_CHU", "DELETE_EDI_NO_CHU")
        map.Add("PRT_USER", "PRT_USER")
        map.Add("PRT_DATE", "PRT_DATE")
        map.Add("PRT_TIME", "PRT_TIME")
        map.Add("EDI_USER", "EDI_USER")
        map.Add("EDI_DATE", "EDI_DATE")
        map.Add("EDI_TIME", "EDI_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("JISSEKI_FUYO_NM", "JISSEKI_FUYO_NM")
        map.Add("JISSEKI_SHORI_FLG_NM", "JISSEKI_SHORI_FLG_NM")
        map.Add("PROC_TYPE_NM", "PROC_TYPE_NM")
        map.Add("PROC_KBN_NM", "PROC_KBN_NM")
        map.Add("OUTKA_WH_TYPE_NM", "OUTKA_WH_TYPE_NM")
        map.Add("INKA_WH_TYPE_NM", "INKA_WH_TYPE_NM")
        map.Add("BEFORE_GOODS_RANK_NM", "BEFORE_GOODS_RANK_NM")
        map.Add("AFTER_GOODS_RANK_NM", "AFTER_GOODS_RANK_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI140OUT")

        reader.Close()

        Return ds

    End Function

#End Region '検索

#Region "更新"

    ''' <summary>
    ''' 物産アニマルヘルス_倉庫内処理依頼EDIデータ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateEdi(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI140_H_WHEDI_BAH")

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(Me.SetSchemaNm(LMI140DAC.SQL_UPDATE_H_WHEDI_BAH, inTbl.Rows(0)("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Dim setDate As String = String.Empty
        Dim setTime As String = String.Empty
        Call Me.SetSysdataParameter_Update(Me._SqlPrmList, setDate, setTime)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", setDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.GetColonEditTime(setTime), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", setDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(setTime), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_PROC_NO", inTbl.Rows(0).Item("NRS_PROC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", inTbl.Rows(0).Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", inTbl.Rows(0).Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI140DAC", "UpdateEdi", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        Return ds

    End Function

#End Region '更新

#Region "登録"

    ''' <summary>
    ''' 物産アニマルヘルス_倉庫内処理実績EDIデータ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI140_H_SENDWHEDI_BAH")

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMI140DAC.SQL_INSERT_H_SENDWHEDI_BAH, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Dim setDate As String = String.Empty
        Dim setTime As String = String.Empty
        Call Me.SetSysdataParameter_Insert(Me._SqlPrmList, setDate, setTime)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", inTbl.Rows(0).Item("DEL_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", inTbl.Rows(0).Item("FILE_NAME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", inTbl.Rows(0).Item("REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO", inTbl.Rows(0).Item("GYO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_PROC_NO", inTbl.Rows(0).Item("NRS_PROC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_TYPE", inTbl.Rows(0).Item("PROC_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_NO", inTbl.Rows(0).Item("PROC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_GYO", inTbl.Rows(0).Item("PROC_GYO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_DATE", inTbl.Rows(0).Item("PROC_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TYPE", inTbl.Rows(0).Item("WH_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_GOODS_RANK", inTbl.Rows(0).Item("BEFORE_GOODS_RANK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AFTER_GOODS_RANK", inTbl.Rows(0).Item("AFTER_GOODS_RANK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", inTbl.Rows(0).Item("GOODS_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", inTbl.Rows(0).Item("GOODS_NM").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB", inTbl.Rows(0).Item("NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", inTbl.Rows(0).Item("LOT_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", inTbl.Rows(0).Item("LT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI1", inTbl.Rows(0).Item("YOBI1").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI2", inTbl.Rows(0).Item("YOBI2").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI3", inTbl.Rows(0).Item("YOBI3").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI4", inTbl.Rows(0).Item("YOBI4").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI5", inTbl.Rows(0).Item("YOBI5").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", inTbl.Rows(0).Item("RECORD_STATUS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", inTbl.Rows(0).Item("JISSEKI_SHORI_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", setDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.GetColonEditTime(setTime), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", inTbl.Rows(0).Item("SEND_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", inTbl.Rows(0).Item("SEND_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", inTbl.Rows(0).Item("SEND_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", inTbl.Rows(0).Item("DELETE_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", inTbl.Rows(0).Item("DELETE_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", inTbl.Rows(0).Item("DELETE_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", inTbl.Rows(0).Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", inTbl.Rows(0).Item("DELETE_EDI_NO_CHU").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", inTbl.Rows(0).Item("UPD_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", inTbl.Rows(0).Item("UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", inTbl.Rows(0).Item("UPD_TIME").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI150DAC", "InsertData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region '登録

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

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSelectData(ByVal inTblRow As DataRow)

        Dim whereStr As String = String.Empty
        Dim strTemp As String = String.Empty

        With inTblRow

            '固定条件
            Me._StrSql.Append("WHERE " & vbNewLine)
            Me._StrSql.Append("      EDI.SYS_DEL_FLG = '0' " & vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.NRS_BR_CD = @NRS_BR_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'ステータス
            strTemp = String.Empty
            whereStr = .Item("JISSEKI_SHORI_FLG_1").ToString()
            If LMConst.FLG.ON.Equals(whereStr) Then
                strTemp += "       OR EDI.JISSEKI_SHORI_FLG = '1' " & vbNewLine
            End If
            whereStr = .Item("JISSEKI_SHORI_FLG_2").ToString()
            If LMConst.FLG.ON.Equals(whereStr) Then
                strTemp += "       OR EDI.JISSEKI_SHORI_FLG = '2' " & vbNewLine
                strTemp += "       OR EDI.JISSEKI_SHORI_FLG = '3' " & vbNewLine
            End If
            If Not String.IsNullOrEmpty(strTemp) Then
                Me._StrSql.Append("  AND (1 = 0 " & vbNewLine)
                Me._StrSql.Append(strTemp)
                Me._StrSql.Append("       )" & vbNewLine)
            End If

            '処理日
            whereStr = .Item("PROC_DATE_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.PROC_DATE >= @PROC_DATE_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_DATE_FROM", whereStr, DBDataType.VARCHAR))
            End If

            whereStr = .Item("PROC_DATE_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.PROC_DATE <= @PROC_DATE_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_DATE_TO", whereStr, DBDataType.VARCHAR))
            End If

            '処理タイプ
            strTemp = String.Empty
            whereStr = .Item("PROC_TYPE_1").ToString()
            If LMConst.FLG.ON.Equals(whereStr) Then
                strTemp += "       OR EDI.PROC_TYPE = '0' " & vbNewLine
            End If
            whereStr = .Item("PROC_TYPE_2").ToString()
            If LMConst.FLG.ON.Equals(whereStr) Then
                strTemp += "       OR EDI.PROC_TYPE = '1' " & vbNewLine
            End If
            If Not String.IsNullOrEmpty(strTemp) Then
                Me._StrSql.Append("  AND (1 = 0 " & vbNewLine)
                Me._StrSql.Append(strTemp)
                Me._StrSql.Append("       )" & vbNewLine)
            End If

            '処理区分
            strTemp = String.Empty
            whereStr = .Item("PROC_KBN_1").ToString()
            If LMConst.FLG.ON.Equals(whereStr) Then
                strTemp += "       OR EDI.PROC_KBN = '0' " & vbNewLine
            End If
            whereStr = .Item("PROC_KBN_2").ToString()
            If LMConst.FLG.ON.Equals(whereStr) Then
                strTemp += "       OR EDI.PROC_KBN = '1' " & vbNewLine
            End If
            If Not String.IsNullOrEmpty(strTemp) Then
                Me._StrSql.Append("  AND (1 = 0 " & vbNewLine)
                Me._StrSql.Append(strTemp)
                Me._StrSql.Append("       )" & vbNewLine)
            End If

            '実績要否
            whereStr = .Item("JISSEKI_FUYO").ToString()
            If "01".Equals(whereStr) Then
                Me._StrSql.Append("  AND EDI.JISSEKI_FUYO = '0' " & vbNewLine)
            ElseIf "02".Equals(whereStr) Then
                Me._StrSql.Append("  AND EDI.JISSEKI_FUYO = '1' " & vbNewLine)
            End If

            '出庫倉庫種類
            whereStr = .Item("OUTKA_WH_TYPE").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.OUTKA_WH_TYPE = @OUTKA_WH_TYPE " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_WH_TYPE", whereStr, DBDataType.CHAR))
            End If

            '入庫倉庫種類
            whereStr = .Item("INKA_WH_TYPE").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.INKA_WH_TYPE = @INKA_WH_TYPE " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_WH_TYPE", whereStr, DBDataType.CHAR))
            End If

            '変更前商品ランク
            whereStr = .Item("BEFORE_GOODS_RANK").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.BEFORE_GOODS_RANK = @BEFORE_GOODS_RANK " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_GOODS_RANK", whereStr, DBDataType.CHAR))
            End If

            '変更後商品ランク
            whereStr = .Item("AFTER_GOODS_RANK").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.AFTER_GOODS_RANK = @AFTER_GOODS_RANK " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AFTER_GOODS_RANK", whereStr, DBDataType.CHAR))
            End If

            '商品コード
            whereStr = .Item("GOODS_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.GOODS_CD LIKE @GOODS_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.GOODS_NM LIKE @GOODS_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'LOT
            whereStr = .Item("LOT_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.LOT_NO LIKE @LOT_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            '明細摘要
            whereStr = .Item("REMARK").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.REMARK LIKE @REMARK " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'NRS処理番号
            whereStr = .Item("NRS_PROC_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("  AND EDI.NRS_PROC_NO LIKE @NRS_PROC_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_PROC_NO", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 登録用共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter_Insert(ByVal prmList As ArrayList, ByRef setDate As String, ByRef setTime As String)

        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        setDate = systemDate
        setTime = systemTime

    End Sub

    ''' <summary>
    ''' 更新用共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter_Update(ByVal prmList As ArrayList, ByRef setDate As String, ByRef setTime As String)

        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

        setDate = systemDate
        setTime = systemTime

    End Sub

#End Region '共通

#End Region 'Method

End Class
