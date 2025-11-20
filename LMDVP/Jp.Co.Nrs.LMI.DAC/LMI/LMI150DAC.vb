' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI150  : 物産アニマルヘルス倉庫内処理編集
'  作  成  者       :  [HORI]
' ==========================================================================

Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI150DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI150DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "検索"

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = "" _
        & "SELECT                                       " & vbNewLine _
        & "   EDI.DEL_KB                                " & vbNewLine _
        & "  ,EDI.CRT_DATE                              " & vbNewLine _
        & "  ,EDI.FILE_NAME                             " & vbNewLine _
        & "  ,EDI.REC_NO                                " & vbNewLine _
        & "  ,EDI.GYO_NO                                " & vbNewLine _
        & "  ,EDI.NRS_BR_CD                             " & vbNewLine _
        & "  ,EDI.NRS_PROC_NO                           " & vbNewLine _
        & "  ,EDI.PROC_TYPE                             " & vbNewLine _
        & "  ,EDI.PROC_KBN                              " & vbNewLine _
        & "  ,EDI.PRTFLG                                " & vbNewLine _
        & "  ,CASE EDI.JISSEKI_FUYO                     " & vbNewLine _
        & "        WHEN '0' THEN '01'                   " & vbNewLine _
        & "        WHEN '1' THEN '02'                   " & vbNewLine _
        & "        ELSE ''                              " & vbNewLine _
        & "        END AS JISSEKI_FUYO                  " & vbNewLine _
        & "  ,EDI.OBIC_SHUBETU                          " & vbNewLine _
        & "  ,EDI.OBIC_TORIHIKI_KBN                     " & vbNewLine _
        & "  ,EDI.OBIC_DENP_NO                          " & vbNewLine _
        & "  ,EDI.OBIC_GYO_NO                           " & vbNewLine _
        & "  ,EDI.OBIC_DETAIL_NO                        " & vbNewLine _
        & "  ,EDI.PROC_DATE                             " & vbNewLine _
        & "  ,EDI.OUTKA_WH_TYPE                         " & vbNewLine _
        & "  ,EDI.OUTKA_CUST_CD_L                       " & vbNewLine _
        & "  ,EDI.OUTKA_CUST_CD_M                       " & vbNewLine _
        & "  ,EDI.INKA_WH_TYPE                          " & vbNewLine _
        & "  ,EDI.INKA_CUST_CD_L                        " & vbNewLine _
        & "  ,EDI.INKA_CUST_CD_M                        " & vbNewLine _
        & "  ,EDI.BEFORE_GOODS_RANK                     " & vbNewLine _
        & "  ,EDI.AFTER_GOODS_RANK                      " & vbNewLine _
        & "  ,EDI.GOODS_CD                              " & vbNewLine _
        & "  ,EDI.GOODS_NM                              " & vbNewLine _
        & "  ,EDI.NB                                    " & vbNewLine _
        & "  ,EDI.LOT_NO                                " & vbNewLine _
        & "  ,EDI.LT_DATE                               " & vbNewLine _
        & "  ,EDI.REMARK                                " & vbNewLine _
        & "  ,EDI.YOBI1                                 " & vbNewLine _
        & "  ,EDI.YOBI2                                 " & vbNewLine _
        & "  ,EDI.YOBI3                                 " & vbNewLine _
        & "  ,EDI.YOBI4                                 " & vbNewLine _
        & "  ,EDI.YOBI5                                 " & vbNewLine _
        & "  ,EDI.RECORD_STATUS                         " & vbNewLine _
        & "  ,CASE EDI.JISSEKI_SHORI_FLG                " & vbNewLine _
        & "        WHEN '1' THEN '0'                    " & vbNewLine _
        & "        WHEN '2' THEN '1'                    " & vbNewLine _
        & "        WHEN '3' THEN '1'                    " & vbNewLine _
        & "        ELSE ''                              " & vbNewLine _
        & "        END AS JISSEKI_SHORI_FLG             " & vbNewLine _
        & "  ,EDI.JISSEKI_USER                          " & vbNewLine _
        & "  ,EDI.JISSEKI_DATE                          " & vbNewLine _
        & "  ,EDI.JISSEKI_TIME                          " & vbNewLine _
        & "  ,EDI.SEND_USER                             " & vbNewLine _
        & "  ,EDI.SEND_DATE                             " & vbNewLine _
        & "  ,EDI.SEND_TIME                             " & vbNewLine _
        & "  ,EDI.DELETE_USER                           " & vbNewLine _
        & "  ,EDI.DELETE_DATE                           " & vbNewLine _
        & "  ,EDI.DELETE_TIME                           " & vbNewLine _
        & "  ,EDI.DELETE_EDI_NO                         " & vbNewLine _
        & "  ,EDI.DELETE_EDI_NO_CHU                     " & vbNewLine _
        & "  ,EDI.PRT_USER                              " & vbNewLine _
        & "  ,EDI.PRT_DATE                              " & vbNewLine _
        & "  ,EDI.PRT_TIME                              " & vbNewLine _
        & "  ,EDI.EDI_USER                              " & vbNewLine _
        & "  ,EDI.EDI_DATE                              " & vbNewLine _
        & "  ,EDI.EDI_TIME                              " & vbNewLine _
        & "  ,EDI.UPD_USER                              " & vbNewLine _
        & "  ,EDI.UPD_DATE                              " & vbNewLine _
        & "  ,EDI.UPD_TIME                              " & vbNewLine _
        & "  ,GOD.GOODS_CD_NRS                          " & vbNewLine _
        & "  ,CSO.CUST_NM_L AS OUTKA_CUST_NM_L          " & vbNewLine _
        & "  ,CSO.CUST_NM_M AS OUTKA_CUST_NM_M          " & vbNewLine _
        & "  ,CSI.CUST_NM_L AS INKA_CUST_NM_L           " & vbNewLine _
        & "  ,CSI.CUST_NM_M AS INKA_CUST_NM_M           " & vbNewLine _
        & "  ,EDI.SYS_UPD_DATE                          " & vbNewLine _
        & "  ,EDI.SYS_UPD_TIME                          " & vbNewLine _
        & "FROM                                         " & vbNewLine _
        & "  $LM_TRN$..H_WHEDI_BAH AS EDI               " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS ZKO                   " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          ZKO.KBN_GROUP_CD = 'B047'          " & vbNewLine _
        & "      AND ZKO.KBN_NM1 = EDI.OUTKA_WH_TYPE    " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    $LM_MST$..M_CUST AS CSO                  " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          CSO.NRS_BR_CD = EDI.NRS_BR_CD      " & vbNewLine _
        & "      AND CSO.CUST_CD_L = ZKO.KBN_NM2        " & vbNewLine _
        & "      AND CSO.CUST_CD_M = ZKO.KBN_NM3        " & vbNewLine _
        & "      AND CSO.CUST_CD_S = '00'               " & vbNewLine _
        & "      AND CSO.CUST_CD_SS = '00'              " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    $LM_MST$..Z_KBN AS ZKI                   " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          ZKI.KBN_GROUP_CD = 'B047'          " & vbNewLine _
        & "      AND ZKI.KBN_NM1 = EDI.INKA_WH_TYPE     " & vbNewLine _
        & "  LEFT JOIN                                  " & vbNewLine _
        & "    $LM_MST$..M_CUST AS CSI                  " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          CSI.NRS_BR_CD = EDI.NRS_BR_CD      " & vbNewLine _
        & "      AND CSI.CUST_CD_L = ZKI.KBN_NM2        " & vbNewLine _
        & "      AND CSI.CUST_CD_M = ZKI.KBN_NM3        " & vbNewLine _
        & "      AND CSI.CUST_CD_S = '00'               " & vbNewLine _
        & "      AND CSI.CUST_CD_SS = '00'              " & vbNewLine _
        & "  LEFT JOIN (                                " & vbNewLine _
        & "    SELECT                                   " & vbNewLine _
        & "       NRS_BR_CD                             " & vbNewLine _
        & "      ,CUST_CD_L                             " & vbNewLine _
        & "      ,CUST_CD_M                             " & vbNewLine _
        & "      ,CUST_CD_S                             " & vbNewLine _
        & "      ,CUST_CD_SS                            " & vbNewLine _
        & "      ,GOODS_CD_CUST                         " & vbNewLine _
        & "      ,MIN(GOODS_CD_NRS) AS GOODS_CD_NRS     " & vbNewLine _
        & "    FROM                                     " & vbNewLine _
        & "      $LM_MST$..M_GOODS                      " & vbNewLine _
        & "    GROUP BY                                 " & vbNewLine _
        & "       NRS_BR_CD                             " & vbNewLine _
        & "      ,CUST_CD_L                             " & vbNewLine _
        & "      ,CUST_CD_M                             " & vbNewLine _
        & "      ,CUST_CD_S                             " & vbNewLine _
        & "      ,CUST_CD_SS                            " & vbNewLine _
        & "      ,GOODS_CD_CUST                         " & vbNewLine _
        & "    HAVING                                   " & vbNewLine _
        & "      COUNT(*) = 1                           " & vbNewLine _
        & "    ) AS GOD                                 " & vbNewLine _
        & "    ON                                       " & vbNewLine _
        & "          GOD.NRS_BR_CD = EDI.NRS_BR_CD      " & vbNewLine _
        & "      AND GOD.CUST_CD_L = CSO.CUST_CD_L      " & vbNewLine _
        & "      AND GOD.CUST_CD_M = CSO.CUST_CD_M      " & vbNewLine _
        & "      AND GOD.CUST_CD_S = CSO.CUST_CD_S      " & vbNewLine _
        & "      AND GOD.CUST_CD_SS = CSO.CUST_CD_SS    " & vbNewLine _
        & "      AND GOD.GOODS_CD_CUST = EDI.GOODS_CD   " & vbNewLine _
        & "WHERE                                        " & vbNewLine _
        & "      EDI.NRS_PROC_NO = @NRS_PROC_NO         " & vbNewLine _
        & "  AND EDI.SYS_DEL_FLG = '0'                  " & vbNewLine _
        & "" & vbNewLine

    ''' <summary>
    ''' NRS処理番号の最大値を取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NRS_PROC_NO_MAX As String = "" _
        & "SELECT                                                   " & vbNewLine _
        & "   ISNULL(MAX(NRS_PROC_NO), '000000000') AS NRS_PROC_NO  " & vbNewLine _
        & "FROM                                                     " & vbNewLine _
        & "  $LM_TRN$..H_WHEDI_BAH                                  " & vbNewLine _
        & ""

#End Region '検索

#Region "登録"

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DATA As String = "" _
        & "INSERT INTO $LM_TRN$..H_WHEDI_BAH (  " & vbNewLine _
        & "   DEL_KB                            " & vbNewLine _
        & "  ,CRT_DATE                          " & vbNewLine _
        & "  ,FILE_NAME                         " & vbNewLine _
        & "  ,REC_NO                            " & vbNewLine _
        & "  ,GYO_NO                            " & vbNewLine _
        & "  ,NRS_BR_CD                         " & vbNewLine _
        & "  ,NRS_PROC_NO                       " & vbNewLine _
        & "  ,PROC_TYPE                         " & vbNewLine _
        & "  ,PROC_KBN                          " & vbNewLine _
        & "  ,PRTFLG                            " & vbNewLine _
        & "  ,JISSEKI_FUYO                      " & vbNewLine _
        & "  ,OBIC_SHUBETU                      " & vbNewLine _
        & "  ,OBIC_TORIHIKI_KBN                 " & vbNewLine _
        & "  ,OBIC_DENP_NO                      " & vbNewLine _
        & "  ,OBIC_GYO_NO                       " & vbNewLine _
        & "  ,OBIC_DETAIL_NO                    " & vbNewLine _
        & "  ,PROC_DATE                         " & vbNewLine _
        & "  ,OUTKA_WH_TYPE                     " & vbNewLine _
        & "  ,OUTKA_CUST_CD_L                   " & vbNewLine _
        & "  ,OUTKA_CUST_CD_M                   " & vbNewLine _
        & "  ,INKA_WH_TYPE                      " & vbNewLine _
        & "  ,INKA_CUST_CD_L                    " & vbNewLine _
        & "  ,INKA_CUST_CD_M                    " & vbNewLine _
        & "  ,BEFORE_GOODS_RANK                 " & vbNewLine _
        & "  ,AFTER_GOODS_RANK                  " & vbNewLine _
        & "  ,GOODS_CD                          " & vbNewLine _
        & "  ,GOODS_NM                          " & vbNewLine _
        & "  ,NB                                " & vbNewLine _
        & "  ,LOT_NO                            " & vbNewLine _
        & "  ,LT_DATE                           " & vbNewLine _
        & "  ,REMARK                            " & vbNewLine _
        & "  ,YOBI1                             " & vbNewLine _
        & "  ,YOBI2                             " & vbNewLine _
        & "  ,YOBI3                             " & vbNewLine _
        & "  ,YOBI4                             " & vbNewLine _
        & "  ,YOBI5                             " & vbNewLine _
        & "  ,RECORD_STATUS                     " & vbNewLine _
        & "  ,JISSEKI_SHORI_FLG                 " & vbNewLine _
        & "  ,JISSEKI_USER                      " & vbNewLine _
        & "  ,JISSEKI_DATE                      " & vbNewLine _
        & "  ,JISSEKI_TIME                      " & vbNewLine _
        & "  ,SEND_USER                         " & vbNewLine _
        & "  ,SEND_DATE                         " & vbNewLine _
        & "  ,SEND_TIME                         " & vbNewLine _
        & "  ,DELETE_USER                       " & vbNewLine _
        & "  ,DELETE_DATE                       " & vbNewLine _
        & "  ,DELETE_TIME                       " & vbNewLine _
        & "  ,DELETE_EDI_NO                     " & vbNewLine _
        & "  ,DELETE_EDI_NO_CHU                 " & vbNewLine _
        & "  ,PRT_USER                          " & vbNewLine _
        & "  ,PRT_DATE                          " & vbNewLine _
        & "  ,PRT_TIME                          " & vbNewLine _
        & "  ,EDI_USER                          " & vbNewLine _
        & "  ,EDI_DATE                          " & vbNewLine _
        & "  ,EDI_TIME                          " & vbNewLine _
        & "  ,UPD_USER                          " & vbNewLine _
        & "  ,UPD_DATE                          " & vbNewLine _
        & "  ,UPD_TIME                          " & vbNewLine _
        & "  ,SYS_ENT_DATE                      " & vbNewLine _
        & "  ,SYS_ENT_TIME                      " & vbNewLine _
        & "  ,SYS_ENT_PGID                      " & vbNewLine _
        & "  ,SYS_ENT_USER                      " & vbNewLine _
        & "  ,SYS_UPD_DATE                      " & vbNewLine _
        & "  ,SYS_UPD_TIME                      " & vbNewLine _
        & "  ,SYS_UPD_PGID                      " & vbNewLine _
        & "  ,SYS_UPD_USER                      " & vbNewLine _
        & "  ,SYS_DEL_FLG                       " & vbNewLine _
        & ") VALUES (                           " & vbNewLine _
        & "   @DEL_KB                           " & vbNewLine _
        & "  ,@CRT_DATE                         " & vbNewLine _
        & "  ,@FILE_NAME                        " & vbNewLine _
        & "  ,@REC_NO                           " & vbNewLine _
        & "  ,@GYO_NO                           " & vbNewLine _
        & "  ,@NRS_BR_CD                        " & vbNewLine _
        & "  ,@NRS_PROC_NO                      " & vbNewLine _
        & "  ,@PROC_TYPE                        " & vbNewLine _
        & "  ,@PROC_KBN                         " & vbNewLine _
        & "  ,@PRTFLG                           " & vbNewLine _
        & "  ,@JISSEKI_FUYO                     " & vbNewLine _
        & "  ,@OBIC_SHUBETU                     " & vbNewLine _
        & "  ,@OBIC_TORIHIKI_KBN                " & vbNewLine _
        & "  ,@OBIC_DENP_NO                     " & vbNewLine _
        & "  ,@OBIC_GYO_NO                      " & vbNewLine _
        & "  ,@OBIC_DETAIL_NO                   " & vbNewLine _
        & "  ,@PROC_DATE                        " & vbNewLine _
        & "  ,@OUTKA_WH_TYPE                    " & vbNewLine _
        & "  ,@OUTKA_CUST_CD_L                  " & vbNewLine _
        & "  ,@OUTKA_CUST_CD_M                  " & vbNewLine _
        & "  ,@INKA_WH_TYPE                     " & vbNewLine _
        & "  ,@INKA_CUST_CD_L                   " & vbNewLine _
        & "  ,@INKA_CUST_CD_M                   " & vbNewLine _
        & "  ,@BEFORE_GOODS_RANK                " & vbNewLine _
        & "  ,@AFTER_GOODS_RANK                 " & vbNewLine _
        & "  ,@GOODS_CD                         " & vbNewLine _
        & "  ,@GOODS_NM                         " & vbNewLine _
        & "  ,@NB                               " & vbNewLine _
        & "  ,@LOT_NO                           " & vbNewLine _
        & "  ,@LT_DATE                          " & vbNewLine _
        & "  ,@REMARK                           " & vbNewLine _
        & "  ,@YOBI1                            " & vbNewLine _
        & "  ,@YOBI2                            " & vbNewLine _
        & "  ,@YOBI3                            " & vbNewLine _
        & "  ,@YOBI4                            " & vbNewLine _
        & "  ,@YOBI5                            " & vbNewLine _
        & "  ,@RECORD_STATUS                    " & vbNewLine _
        & "  ,@JISSEKI_SHORI_FLG                " & vbNewLine _
        & "  ,@JISSEKI_USER                     " & vbNewLine _
        & "  ,@JISSEKI_DATE                     " & vbNewLine _
        & "  ,@JISSEKI_TIME                     " & vbNewLine _
        & "  ,@SEND_USER                        " & vbNewLine _
        & "  ,@SEND_DATE                        " & vbNewLine _
        & "  ,@SEND_TIME                        " & vbNewLine _
        & "  ,@DELETE_USER                      " & vbNewLine _
        & "  ,@DELETE_DATE                      " & vbNewLine _
        & "  ,@DELETE_TIME                      " & vbNewLine _
        & "  ,@DELETE_EDI_NO                    " & vbNewLine _
        & "  ,@DELETE_EDI_NO_CHU                " & vbNewLine _
        & "  ,@PRT_USER                         " & vbNewLine _
        & "  ,@PRT_DATE                         " & vbNewLine _
        & "  ,@PRT_TIME                         " & vbNewLine _
        & "  ,@EDI_USER                         " & vbNewLine _
        & "  ,@EDI_DATE                         " & vbNewLine _
        & "  ,@EDI_TIME                         " & vbNewLine _
        & "  ,@UPD_USER                         " & vbNewLine _
        & "  ,@UPD_DATE                         " & vbNewLine _
        & "  ,@UPD_TIME                         " & vbNewLine _
        & "  ,@SYS_ENT_DATE                     " & vbNewLine _
        & "  ,@SYS_ENT_TIME                     " & vbNewLine _
        & "  ,@SYS_ENT_PGID                     " & vbNewLine _
        & "  ,@SYS_ENT_USER                     " & vbNewLine _
        & "  ,@SYS_UPD_DATE                     " & vbNewLine _
        & "  ,@SYS_UPD_TIME                     " & vbNewLine _
        & "  ,@SYS_UPD_PGID                     " & vbNewLine _
        & "  ,@SYS_UPD_USER                     " & vbNewLine _
        & "  ,@SYS_DEL_FLG                      " & vbNewLine _
        & ")                                    " & vbNewLine _
        & "" & vbNewLine

#End Region '登録

#Region "更新"

    ''' <summary>
    ''' データ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DATA As String = "" _
        & "UPDATE $LM_TRN$..H_WHEDI_BAH SET             " & vbNewLine _
        & "   PROC_DATE          = @PROC_DATE           " & vbNewLine _
        & "  ,OUTKA_WH_TYPE      = @OUTKA_WH_TYPE       " & vbNewLine _
        & "  ,OUTKA_CUST_CD_L    = @OUTKA_CUST_CD_L     " & vbNewLine _
        & "  ,OUTKA_CUST_CD_M    = @OUTKA_CUST_CD_M     " & vbNewLine _
        & "  ,INKA_WH_TYPE       = @INKA_WH_TYPE        " & vbNewLine _
        & "  ,INKA_CUST_CD_L     = @INKA_CUST_CD_L      " & vbNewLine _
        & "  ,INKA_CUST_CD_M     = @INKA_CUST_CD_M      " & vbNewLine _
        & "  ,BEFORE_GOODS_RANK  = @BEFORE_GOODS_RANK   " & vbNewLine _
        & "  ,AFTER_GOODS_RANK   = @AFTER_GOODS_RANK    " & vbNewLine _
        & "  ,GOODS_CD           = @GOODS_CD            " & vbNewLine _
        & "  ,GOODS_NM           = @GOODS_NM            " & vbNewLine _
        & "  ,NB                 = @NB                  " & vbNewLine _
        & "  ,LOT_NO             = @LOT_NO              " & vbNewLine _
        & "  ,LT_DATE            = @LT_DATE             " & vbNewLine _
        & "  ,UPD_USER           = @UPD_USER            " & vbNewLine _
        & "  ,UPD_DATE           = @UPD_DATE            " & vbNewLine _
        & "  ,UPD_TIME           = @UPD_TIME            " & vbNewLine _
        & "  ,SYS_UPD_DATE       = @SYS_UPD_DATE        " & vbNewLine _
        & "  ,SYS_UPD_TIME       = @SYS_UPD_TIME        " & vbNewLine _
        & "  ,SYS_UPD_PGID       = @SYS_UPD_PGID        " & vbNewLine _
        & "  ,SYS_UPD_USER       = @SYS_UPD_USER        " & vbNewLine _
        & "WHERE                                        " & vbNewLine _
        & "      NRS_PROC_NO = @NRS_PROC_NO             " & vbNewLine _
        & "  AND SYS_UPD_DATE = @SYS_UPD_DATE_HAITA     " & vbNewLine _
        & "  AND SYS_UPD_TIME = @SYS_UPD_TIME_HAITA     " & vbNewLine _
        & "" & vbNewLine

#End Region '更新

#End Region 'SQL

#End Region 'Const

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
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI150IN")

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMI150DAC.SQL_SELECT_DATA, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_PROC_NO", inTbl.Rows(0).Item("NRS_PROC_NO").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI150DAC", "SelectData", cmd)

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
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("OUTKA_CUST_NM_L", "OUTKA_CUST_NM_L")
        map.Add("OUTKA_CUST_NM_M", "OUTKA_CUST_NM_M")
        map.Add("INKA_CUST_NM_L", "INKA_CUST_NM_L")
        map.Add("INKA_CUST_NM_M", "INKA_CUST_NM_M")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI150OUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' NRS処理番号の最大値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNrsProcNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI150IN")

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMI150DAC.SQL_SELECT_NRS_PROC_NO_MAX, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI150DAC", "SelectNrsProcNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_PROC_NO", "NRS_PROC_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "NRS_PROC_NO")

        reader.Close()

        Return ds

    End Function

#End Region '検索

#Region "登録"

    ''' <summary>
    ''' データ登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI150_H_WHEDI_BAH")

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMI150DAC.SQL_INSERT_DATA, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", inTbl.Rows(0).Item("DEL_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", inTbl.Rows(0).Item("FILE_NAME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", inTbl.Rows(0).Item("REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO_NO", inTbl.Rows(0).Item("GYO_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_PROC_NO", inTbl.Rows(0).Item("NRS_PROC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_TYPE", inTbl.Rows(0).Item("PROC_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_KBN", inTbl.Rows(0).Item("PROC_KBN").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", inTbl.Rows(0).Item("PRTFLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FUYO", inTbl.Rows(0).Item("JISSEKI_FUYO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OBIC_SHUBETU", inTbl.Rows(0).Item("OBIC_SHUBETU").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OBIC_TORIHIKI_KBN", inTbl.Rows(0).Item("OBIC_TORIHIKI_KBN").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OBIC_DENP_NO", inTbl.Rows(0).Item("OBIC_DENP_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OBIC_GYO_NO", inTbl.Rows(0).Item("OBIC_GYO_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OBIC_DETAIL_NO", inTbl.Rows(0).Item("OBIC_DETAIL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_DATE", inTbl.Rows(0).Item("PROC_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_WH_TYPE", inTbl.Rows(0).Item("OUTKA_WH_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CUST_CD_L", inTbl.Rows(0).Item("OUTKA_CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CUST_CD_M", inTbl.Rows(0).Item("OUTKA_CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_WH_TYPE", inTbl.Rows(0).Item("INKA_WH_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CUST_CD_L", inTbl.Rows(0).Item("INKA_CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CUST_CD_M", inTbl.Rows(0).Item("INKA_CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_GOODS_RANK", inTbl.Rows(0).Item("BEFORE_GOODS_RANK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AFTER_GOODS_RANK", inTbl.Rows(0).Item("AFTER_GOODS_RANK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", inTbl.Rows(0).Item("GOODS_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", inTbl.Rows(0).Item("GOODS_NM").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB", inTbl.Rows(0).Item("NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", inTbl.Rows(0).Item("LOT_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", inTbl.Rows(0).Item("LT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", inTbl.Rows(0).Item("REMARK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI1", inTbl.Rows(0).Item("YOBI1").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI2", inTbl.Rows(0).Item("YOBI2").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI3", inTbl.Rows(0).Item("YOBI3").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI4", inTbl.Rows(0).Item("YOBI4").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI5", inTbl.Rows(0).Item("YOBI5").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", inTbl.Rows(0).Item("RECORD_STATUS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", inTbl.Rows(0).Item("JISSEKI_SHORI_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", inTbl.Rows(0).Item("JISSEKI_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", inTbl.Rows(0).Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", inTbl.Rows(0).Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", inTbl.Rows(0).Item("SEND_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", inTbl.Rows(0).Item("SEND_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", inTbl.Rows(0).Item("SEND_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", inTbl.Rows(0).Item("DELETE_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", inTbl.Rows(0).Item("DELETE_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", inTbl.Rows(0).Item("DELETE_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", inTbl.Rows(0).Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", inTbl.Rows(0).Item("DELETE_EDI_NO_CHU").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_USER", inTbl.Rows(0).Item("PRT_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_DATE", inTbl.Rows(0).Item("PRT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TIME", inTbl.Rows(0).Item("PRT_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_USER", inTbl.Rows(0).Item("EDI_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE", inTbl.Rows(0).Item("EDI_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_TIME", inTbl.Rows(0).Item("EDI_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", inTbl.Rows(0).Item("UPD_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", inTbl.Rows(0).Item("UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", inTbl.Rows(0).Item("UPD_TIME").ToString(), DBDataType.CHAR))

        Dim setDate As String = String.Empty
        Dim setTime As String = String.Empty
        Call Me.SetSysdataParameter_Insert(Me._SqlPrmList, String.Empty, setDate, setTime)

        inTbl.Rows(0).Item("SYS_UPD_DATE_RESULT") = setDate
        inTbl.Rows(0).Item("SYS_UPD_TIME_RESULT") = setTime

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

#Region "更新"

    ''' <summary>
    ''' データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI150_H_WHEDI_BAH")

        'SQL作成
        Dim sql As String = Me.SetSchemaNm(LMI150DAC.SQL_UPDATE_DATA, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_DATE", inTbl.Rows(0).Item("PROC_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_WH_TYPE", inTbl.Rows(0).Item("OUTKA_WH_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CUST_CD_L", inTbl.Rows(0).Item("OUTKA_CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CUST_CD_M", inTbl.Rows(0).Item("OUTKA_CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_WH_TYPE", inTbl.Rows(0).Item("INKA_WH_TYPE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CUST_CD_L", inTbl.Rows(0).Item("INKA_CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CUST_CD_M", inTbl.Rows(0).Item("INKA_CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BEFORE_GOODS_RANK", inTbl.Rows(0).Item("BEFORE_GOODS_RANK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AFTER_GOODS_RANK", inTbl.Rows(0).Item("AFTER_GOODS_RANK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", inTbl.Rows(0).Item("GOODS_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", inTbl.Rows(0).Item("GOODS_NM").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB", inTbl.Rows(0).Item("NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", inTbl.Rows(0).Item("LOT_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", inTbl.Rows(0).Item("LT_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", inTbl.Rows(0).Item("UPD_USER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", inTbl.Rows(0).Item("UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", inTbl.Rows(0).Item("UPD_TIME").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_PROC_NO", inTbl.Rows(0).Item("NRS_PROC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", inTbl.Rows(0).Item("SYS_UPD_DATE_HAITA").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", inTbl.Rows(0).Item("SYS_UPD_TIME_HAITA").ToString(), DBDataType.CHAR))

        Dim setDate As String = String.Empty
        Dim setTime As String = String.Empty
        Call Me.SetSysdataParameter_Update(Me._SqlPrmList, String.Empty, setDate, setTime)

        inTbl.Rows(0).Item("SYS_UPD_DATE_RESULT") = setDate
        inTbl.Rows(0).Item("SYS_UPD_TIME_RESULT") = setTime

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI150DAC", "UpdateData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region '更新

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
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 登録用共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter_Insert(ByVal prmList As ArrayList, ByVal dataSetNm As String, ByRef setDate As String, ByRef setTime As String)

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
    Private Sub SetSysdataParameter_Update(ByVal prmList As ArrayList, ByVal dataSetNm As String, ByRef setDate As String, ByRef setTime As String)

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
