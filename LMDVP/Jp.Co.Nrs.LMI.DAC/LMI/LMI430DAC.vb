' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LMI430DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI430DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NAME
        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = "LMI430IN"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT As String = "LMI430OUT"

        ''' <summary>
        ''' 入力テーブル(シリンダー)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_CYLINDER As String = "LMI430IN_CYLINDER"

        ''' <summary>
        ''' 出力テーブル(検品データ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_INSPECTION_DATA As String = "LMI430OUT_INSPECTION_DATA"

        ''' <summary>
        ''' 出力テーブル(検索取得数)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_COUNT As String = "LMI430OUT_COUNT"

        'ADD 2017/04/24
        ''' <summary>
        ''' 出力テーブル(読取データ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_READ_DATA As String = "LMI430OUT_READ_DATA"
    End Class

#Region "カラム名定義"
    Public Class COL_NAME

        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const INKA_CYL_FILE_NO_L As String = "INKA_CYL_FILE_NO_L"
        Public Const INKA_CYL_FILE_NO_M As String = "INKA_CYL_FILE_NO_M"
        Public Const LOAD_FILE_NAME As String = "LOAD_FILE_NAME"
        Public Const CYL_COUNT As String = "CYL_COUNT"
        Public Const CUST_CD_L As String = "CUST_CD_L"
        Public Const CUST_CD_M As String = "CUST_CD_M"
        Public Const INKA_DATE As String = "INKA_DATE"
        Public Const INKA_DATE_FROM As String = "INKA_DATE_FROM"
        Public Const INKA_DATE_TO As String = "INKA_DATE_TO"
        Public Const REMARK_1 As String = "REMARK_1"
        Public Const REMARK_2 As String = "REMARK_2"
        Public Const REMARK_3 As String = "REMARK_3"
        Public Const USER_CD As String = "USER_CD"
        Public Const LAST_UPD_DATE As String = "LAST_UPD_DATE"
        Public Const LAST_UPD_TIME As String = "LAST_UPD_TIME"
        Public Const CRT_DATE As String = "CRT_DATE"
        Public Const CRT_TIME As String = "CRT_TIME"
        Public Const CRT_USER_NM As String = "CRT_USER_NM"
        Public Const ROW_NO As String = "ROW_NO"
        Public Const GAS_NAME As String = "GAS_NAME"
        Public Const VOLUME As String = "VOLUME"
        Public Const SERIAL_NO As String = "SERIAL_NO"

        Public Const INSPECTION_USER_NM As String = "INSPECTION_USER_NM"
        Public Const IS_LOAD As String = "IS_LOAD"
        Public Const IS_SCAN As String = "IS_SCAN"

        Public Const SPREAD_ROW_NO As String = "SPREAD_ROW_NO"

        Public Const COUNT As String = "CNT"

        'ADD 2017/04/24 Start
        Public Const INSPECTION_DATE As String = "INSPECTION_DATE"
        Public Const INSPECTION_TIME As String = "INSPECTION_TIME"
        'ADD 2017/04/24 End
    End Class

    Public Class IN_CYL_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = COL_NAME.INKA_CYL_FILE_NO_L
        Public Const INKA_CYL_FILE_NO_M As String = COL_NAME.INKA_CYL_FILE_NO_M
        Public Const ROW_NO As String = COL_NAME.ROW_NO
        Public Const GAS_NAME As String = COL_NAME.GAS_NAME
        Public Const VOLUME As String = COL_NAME.VOLUME
        Public Const SERIAL_NO As String = COL_NAME.SERIAL_NO
    End Class

    Public Class INPUT_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = COL_NAME.INKA_CYL_FILE_NO_L
        Public Const LOAD_FILE_NAME As String = COL_NAME.LOAD_FILE_NAME
        Public Const CUST_CD_L As String = COL_NAME.CUST_CD_L
        Public Const CUST_CD_M As String = COL_NAME.CUST_CD_M
        Public Const INKA_DATE As String = COL_NAME.INKA_DATE
        Public Const INKA_DATE_FROM As String = COL_NAME.INKA_DATE_FROM
        Public Const INKA_DATE_TO As String = COL_NAME.INKA_DATE_TO
        Public Const REMARK_1 As String = COL_NAME.REMARK_1
        Public Const REMARK_2 As String = COL_NAME.REMARK_2
        Public Const REMARK_3 As String = COL_NAME.REMARK_3
        Public Const USER_CD As String = COL_NAME.USER_CD
        Public Const LAST_UPD_DATE As String = COL_NAME.LAST_UPD_DATE
        Public Const LAST_UPD_TIME As String = COL_NAME.LAST_UPD_TIME
        Public Const SPREAD_ROW_NO As String = COL_NAME.SPREAD_ROW_NO
    End Class

    Public Class OUTPUT_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = COL_NAME.INKA_CYL_FILE_NO_L
        Public Const CYL_COUNT As String = COL_NAME.CYL_COUNT
        Public Const CUST_CD_L As String = COL_NAME.CUST_CD_L
        Public Const CUST_CD_M As String = COL_NAME.CUST_CD_M
        Public Const INKA_DATE As String = COL_NAME.INKA_DATE
        Public Const LOAD_FILE_NAME As String = COL_NAME.LOAD_FILE_NAME
        Public Const REMARK_1 As String = COL_NAME.REMARK_1
        Public Const REMARK_2 As String = COL_NAME.REMARK_2
        Public Const REMARK_3 As String = COL_NAME.REMARK_3
        Public Const CRT_DATE As String = COL_NAME.CRT_DATE
        Public Const CRT_TIME As String = COL_NAME.CRT_TIME
        Public Const CRT_USER_NM As String = COL_NAME.CRT_USER_NM
        Public Const LAST_UPD_DATE As String = COL_NAME.LAST_UPD_DATE
        Public Const LAST_UPD_TIME As String = COL_NAME.LAST_UPD_TIME
    End Class


    Public Class OUT_INSPECT_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = COL_NAME.INKA_CYL_FILE_NO_L
        Public Const INKA_DATE As String = COL_NAME.INKA_DATE
        Public Const LOAD_FILE_NAME As String = COL_NAME.LOAD_FILE_NAME
        Public Const REMARK_1 As String = COL_NAME.REMARK_1
        Public Const REMARK_2 As String = COL_NAME.REMARK_2
        Public Const REMARK_3 As String = COL_NAME.REMARK_3
        Public Const GAS_NAME As String = COL_NAME.GAS_NAME
        Public Const VOLUME As String = COL_NAME.VOLUME
        Public Const SERIAL_NO As String = COL_NAME.SERIAL_NO
        Public Const INSPECTION_USER_NM As String = COL_NAME.INSPECTION_USER_NM
        Public Const IS_LOAD As String = COL_NAME.IS_LOAD
        Public Const IS_SCAN As String = COL_NAME.IS_SCAN
    End Class


    Public Class OUT_READ_COLUMN_NM
        Public Const NRS_BR_CD As String = COL_NAME.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = COL_NAME.INKA_CYL_FILE_NO_L
        Public Const SERIAL_NO As String = COL_NAME.SERIAL_NO
        Public Const CUST_CD_L As String = COL_NAME.CUST_CD_L
        Public Const CUST_CD_M As String = COL_NAME.CUST_CD_M
        Public Const INSPECTION_DATE As String = COL_NAME.INSPECTION_DATE
        Public Const INSPECTION_TIME As String = COL_NAME.INSPECTION_TIME
        Public Const INSPECTION_USER_NM As String = COL_NAME.INSPECTION_USER_NM

    End Class


#End Region


#Region "関数名定義"

    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME

        ''' <summary>
        ''' 取込ファイル一覧取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectLoadedInkaCylFileList As String = "SelectLoadedInkaCylFileList"

        ''' <summary>
        ''' 取込ファイル一覧件数取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectLoadedInkaCylFileListCount As String = "SelectLoadedInkaCylFileListCount"

        ''' <summary>
        ''' 検品結果取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectInspectionData As String = "SelectInspectionData"

        ''' <summary>
        ''' 行追加(InkaCylinderL)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InsertInkaCylinderL As String = "InsertInkaCylinderL"

        ''' <summary>
        ''' 行追加(InkaCylinderM)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InsertInkaCylinderM As String = "InsertInkaCylinderM"

        ''' <summary>
        ''' 論理削除(InkaCylinderL)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SoftDeleteInkaCylinderL As String = "SoftDeleteInkaCylinderL"

        ''' <summary>
        ''' 論理削除(InkaCylinderM)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SoftDeleteInkaCylinderM As String = "SoftDeleteInkaCylinderM"


        ''' <summary>
        ''' 読取データ結果取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectReadResulData As String = "SelectReadResulData"


    End Class
#End Region

#Region "検索"

    ''' <summary>
    ''' 取込ファイル一覧検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LOADED_INKA_CYL_FILES_COUNT As String _
        = " SELECT                                                                    " & vbNewLine _
        & "        count(*)            AS CNT                                         " & vbNewLine _
        & "   FROM                                                                    " & vbNewLine _
        & "        $LM_TRN$..I_INKA_CYLINDER_L AS HED                                 " & vbNewLine _
        & "  WHERE                                                                    " & vbNewLine _
        & "        HED.SYS_DEL_FLG = '0'                                              " & vbNewLine _
        & "    AND HED.NRS_BR_CD   = @NRS_BR_CD                                       " & vbNewLine _
        & "    AND HED.CUST_CD_L   = @CUST_CD_L                                       " & vbNewLine _
        & "    AND HED.CUST_CD_M   = @CUST_CD_M                                       " & vbNewLine

    ''' <summary>
    ''' 取込ファイル一覧検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LOADED_INKA_CYL_FILES As String _
        = " SELECT                                                                    " & vbNewLine _
        & "        HED.NRS_BR_CD            AS NRS_BR_CD                              " & vbNewLine _
        & "      , HED.INKA_CYL_FILE_NO_L   AS INKA_CYL_FILE_NO_L                     " & vbNewLine _
        & "      , HED.CUST_CD_L            AS CUST_CD_L                              " & vbNewLine _
        & "      , HED.CUST_CD_M            AS CUST_CD_M                              " & vbNewLine _
        & "      , HED.INKA_DATE            AS INKA_DATE                              " & vbNewLine _
        & "      , HED.LOAD_FILE_NAME       AS LOAD_FILE_NAME                         " & vbNewLine _
        & "      , ISNULL(DTL.ROW_COUNT, 0) AS CYL_COUNT                              " & vbNewLine _
        & "      , HED.REMARK_1             AS REMARK_1                               " & vbNewLine _
        & "      , HED.REMARK_2             AS REMARK_2                               " & vbNewLine _
        & "      , HED.REMARK_3             AS REMARK_3                               " & vbNewLine _
        & "      , HED.CRT_DATE             AS CRT_DATE                               " & vbNewLine _
        & "      , HED.CRT_TIME             AS CRT_TIME                               " & vbNewLine _
        & "      , SU.USER_NM               AS CRT_USER_NM                            " & vbNewLine _
        & "      , HED.SYS_UPD_DATE         AS LAST_UPD_DATE                          " & vbNewLine _
        & "      , HED.SYS_UPD_TIME         AS LAST_UPD_TIME                          " & vbNewLine _
        & "   FROM                                                                    " & vbNewLine _
        & "        $LM_TRN$..I_INKA_CYLINDER_L AS HED                                 " & vbNewLine _
        & "   LEFT JOIN                                                               " & vbNewLine _
        & "        (                                                                  " & vbNewLine _
        & "          SELECT                                                           " & vbNewLine _
        & "                 NRS_BR_CD                                                 " & vbNewLine _
        & "               , INKA_CYL_FILE_NO_L                                        " & vbNewLine _
        & "               , count(*) AS ROW_COUNT                                     " & vbNewLine _
        & "            FROM                                                           " & vbNewLine _
        & "                 $LM_TRN$..I_INKA_CYLINDER_M                               " & vbNewLine _
        & "           WHERE                                                           " & vbNewLine _
        & "                 SYS_DEL_FLG = '0'                                         " & vbNewLine _
        & "             AND NRS_BR_CD   = @NRS_BR_CD                                  " & vbNewLine _
        & "           GROUP BY                                                        " & vbNewLine _
        & "                 NRS_BR_CD                                                 " & vbNewLine _
        & "               , INKA_CYL_FILE_NO_L                                        " & vbNewLine _
        & "        ) AS DTL                                                           " & vbNewLine _
        & "     ON DTL.INKA_CYL_FILE_NO_L = HED.INKA_CYL_FILE_NO_L                    " & vbNewLine _
        & "    AND DTL.NRS_BR_CD          = HED.NRS_BR_CD                             " & vbNewLine _
        & "   LEFT JOIN                                                               " & vbNewLine _
        & "        $LM_MST$..S_USER    AS SU                                          " & vbNewLine _
        & "     ON SU.USER_CD = HED.CRT_USER_CD                                       " & vbNewLine _
        & "  WHERE                                                                    " & vbNewLine _
        & "        HED.SYS_DEL_FLG = '0'                                              " & vbNewLine _
        & "    AND HED.NRS_BR_CD   = @NRS_BR_CD                                       " & vbNewLine _
        & "    AND HED.CUST_CD_L   = @CUST_CD_L                                       " & vbNewLine _
        & "    AND HED.CUST_CD_M   = @CUST_CD_M                                       " & vbNewLine


    ''' <summary>
    ''' 取込ファイル一覧検索追加条件(入荷日From)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_LOADED_INKA_CYL_FILES_ADD_INKA_FROM As String _
        = "    AND INKA_DATE >= @INKA_DATE_FROM -- FROM                               " & vbNewLine

    ''' <summary>
    ''' 取込ファイル一覧検索追加条件(入荷日To)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_LOADED_INKA_CYL_FILES_ADD_INKA_TO As String _
        = "    AND INKA_DATE <= @INKA_DATE_TO   -- TO                                 " & vbNewLine

    ''' <summary>
    ''' 検品データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INSPECTION_DATA As String _
        = " SELECT                                                                                                 " & vbNewLine _
        & "        IL.NRS_BR_CD                 AS NRS_BR_CD                                                       " & vbNewLine _
        & "      , IL.INKA_CYL_FILE_NO_L        AS INKA_CYL_FILE_NO_L                                              " & vbNewLine _
        & "      , IL.LOAD_FILE_NAME            AS LOAD_FILE_NAME                                                  " & vbNewLine _
        & "      , IL.INKA_DATE                 AS INKA_DATE                                                       " & vbNewLine _
        & "      , IL.REMARK_1                  AS REMARK_1                                                        " & vbNewLine _
        & "      , IL.REMARK_2                  AS REMARK_2                                                        " & vbNewLine _
        & "      , IL.REMARK_3                  AS REMARK_3                                                        " & vbNewLine _
        & "      , CYL.GAS_NAME                 AS GAS_NAME                                                        " & vbNewLine _
        & "      , CYL.VOLUME                   AS VOLUME                                                          " & vbNewLine _
        & "      , CYL.SERIAL_NO                AS SERIAL_NO                                                       " & vbNewLine _
        & "      , ISNULL(USR.USER_NM, '')      AS INSPECTION_USER_NM                                              " & vbNewLine _
        & "      , CASE WHEN CYL.IS_LOAD = 1                                                                       " & vbNewLine _
        & "             THEN '○'                                                                                  " & vbNewLine _
        & "             ELSE '×'                                                                                  " & vbNewLine _
        & "        END                          AS IS_LOAD                                                         " & vbNewLine _
        & "      , CASE WHEN CYL.IS_SCAN = 1                                                                       " & vbNewLine _
        & "             THEN '○'                                                                                  " & vbNewLine _
        & "             ELSE '×'                                                                                  " & vbNewLine _
        & "        END                          AS IS_SCAN                                                         " & vbNewLine _
        & "   FROM                                                                                                 " & vbNewLine _
        & "        $LM_TRN$..I_INKA_CYLINDER_L AS IL                                                               " & vbNewLine _
        & "   LEFT JOIN                                                                                            " & vbNewLine _
        & "             (SELECT                                                                                    " & vbNewLine _
        & "                     ISNULL(IM.NRS_BR_CD, WK.NRS_BR_CD)                       AS NRS_BR_CD              " & vbNewLine _
        & "                   , ISNULL(IM.INKA_CYL_FILE_NO_L, WK.INKA_CYL_FILE_NO_L)     AS INKA_CYL_FILE_NO_L     " & vbNewLine _
        & "                   , ISNULL(IM.SERIAL_NO, WK.SERIAL_NO)                       AS SERIAL_NO              " & vbNewLine _
        & "                   , ISNULL(IM.GAS_NAME, '')                                  AS GAS_NAME               " & vbNewLine _
        & "                   , ISNULL(IM.VOLUME, '')                                    AS VOLUME                 " & vbNewLine _
        & "                   , ISNULL(WK.INSPECTION_USER_CD, '')                        AS INSPECTION_USER_CD     " & vbNewLine _
        & "                   , CASE WHEN IM.SERIAL_NO IS NULL                                                     " & vbNewLine _
        & "                          THEN 0                                                                        " & vbNewLine _
        & "                          ELSE 1                                                                        " & vbNewLine _
        & "                     END                                                      AS IS_LOAD                " & vbNewLine _
        & "                   , CASE WHEN WK.SERIAL_NO IS NULL                                                     " & vbNewLine _
        & "                         THEN 0                                                                         " & vbNewLine _
        & "                         ELSE 1                                                                         " & vbNewLine _
        & "                     END                                                      AS IS_SCAN                " & vbNewLine _
        & "                FROM                                                                                    " & vbNewLine _
        & "                     $LM_TRN$..I_INKA_CYLINDER_M AS IM                                                  " & vbNewLine _
        & "                FULL OUTER JOIN                                                                         " & vbNewLine _
        & "                     $LM_TRN$..I_INKA_CYLINDER_KENPIN_WK AS WK                                          " & vbNewLine _
        & "                  ON WK.INKA_CYL_FILE_NO_L = IM.INKA_CYL_FILE_NO_L                                      " & vbNewLine _
        & "                 AND WK.NRS_BR_CD = IM.NRS_BR_CD                                                        " & vbNewLine _
        & "                 AND RTRIM(LEFT(WK.SERIAL_NO, 6)) + RTRIM(SUBSTRING(WK.SERIAL_NO, 7, 6))                " & vbNewLine _
        & "                   = REPLACE(IM.SERIAL_NO, ' ', '')                                                     " & vbNewLine _
        & "                 AND WK.SYS_DEL_FLG = '0'                                                               " & vbNewLine _
        & "               WHERE (WK.NRS_BR_CD IS NOT NULL AND WK.SYS_DEL_FLG = '0')                                " & vbNewLine _
        & "                  OR (IM.NRS_BR_CD IS NOT NULL AND IM.SYS_DEL_FLG = '0')                                " & vbNewLine _
        & "             ) AS CYL                                                                                   " & vbNewLine _
        & "     ON CYL.INKA_CYL_FILE_NO_L = IL.INKA_CYL_FILE_NO_L                                                  " & vbNewLine _
        & "    AND CYL.NRS_BR_CD          = IL.NRS_BR_CD                                                           " & vbNewLine _
        & "   LEFT JOIN                                                                                            " & vbNewLine _
        & "        $LM_MST$..S_USER AS USR                                                                         " & vbNewLine _
        & "     ON USR.USER_CD = CYL.INSPECTION_USER_CD                                                            " & vbNewLine _
        & "  WHERE                                                                                                 " & vbNewLine _
        & "        IL.INKA_CYL_FILE_NO_L = @INKA_CYL_FILE_NO_L                                                     " & vbNewLine _
        & "    AND IL.NRS_BR_CD          = @NRS_BR_CD                                                              " & vbNewLine _
        & "    AND IL.SYS_DEL_FLG        = '0'                                                                     " & vbNewLine _
        & "  ORDER BY                                                                                              " & vbNewLine _
        & "        IL.INKA_DATE                                                                                    " & vbNewLine _
        & "      , IL.INKA_CYL_FILE_NO_L                                                                           " & vbNewLine _
        & "      , CYL.GAS_NAME                                                                                    " & vbNewLine _
        & "      , CYL.VOLUME                                                                                      " & vbNewLine _
        & "      , CYL.SERIAL_NO                                                                                   " & vbNewLine



    ''' <summary>
    ''' 読取データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SelectReadResulData As String _
        = " SELECT                                                                                                 " & vbNewLine _
        & "         WK.NRS_BR_CD                            AS  NRS_BR_CD                  " & vbNewLine _
        & "        ,ISNULL(M_NRS_BR.NRS_BR_NM,'')           AS  NRS_BR_NM                  " & vbNewLine _
        & "        ,WK.INKA_CYL_FILE_NO_L                   AS  INKA_CYL_FILE_NO_L         " & vbNewLine _
        & "        ,WK.SERIAL_NO                            AS  SERIAL_NO                  " & vbNewLine _
        & "        ,ISNULL(CL.CUST_CD_L,'')                 AS  CUST_CD_L                  " & vbNewLine _
        & "        ,ISNULL(CL.CUST_CD_M,'')                 AS  CUST_CD_M                  " & vbNewLine _
        & "        ,SUBSTRING(WK.INSPECTION_DATE,1,4)+'/'+                                 " & vbNewLine _
        & "         SUBSTRING(WK.INSPECTION_DATE,5,2)+'/'+                                 " & vbNewLine _
        & "         SUBSTRING(WK.INSPECTION_DATE,7,2)       AS INSPECTION_DATE             " & vbNewLine _
        & "        ,SUBSTRING(WK.INSPECTION_TIME,1,2) +':'+                                " & vbNewLine _
        & "         SUBSTRING(WK.INSPECTION_TIME,3,2)       AS INSPECTION_TIME             " & vbNewLine _
        & "        ,WK.INSPECTION_USER_CD + ' ' + ISNULL(USER_NM,'') AS INSPECTION_USER_NM " & vbNewLine _
        & "    FROM $LM_TRN$..I_INKA_CYLINDER_KENPIN_WK WK                                 " & vbNewLine _
        & "    LEFT JOIN $LM_MST$..S_USER  ON                                              " & vbNewLine _
        & "           S_USER.USER_CD  = WK.INSPECTION_USER_CD                              " & vbNewLine _
        & "    LEFT JOIN $LM_MST$..M_NRS_BR ON                                             " & vbNewLine _
        & "           M_NRS_BR.NRS_BR_CD = WK.NRS_BR_CD                                    " & vbNewLine _
        & "    LEFT JOIN $LM_TRN$..I_INKA_CYLINDER_L  CL ON                                " & vbNewLine _
        & "          CL.NRS_BR_CD          = WK.NRS_BR_CD                                  " & vbNewLine _
        & "      AND CL.INKA_CYL_FILE_NO_L = WK.INKA_CYL_FILE_NO_L                         " & vbNewLine _
        & "   WHERE WK.NRS_BR_CD       = @NRS_BR_CD                                  " & vbNewLine _
        & "--     AND  WK.INSPECTION_DATE = CASE WHEN @INKA_DATE_FROM = '' THEN              " & vbNewLine _
        & "--                                         CONVERT(VARCHAR,GETDATE(),112)         " & vbNewLine _
        & "--                                    ELSE @INKA_DATE_FROM    END                 " & vbNewLine


    Private Const SQL_SelectReadResulDataOrderBY As String _
        = "ORDER BY WK.INSPECTION_DATE                                                  " & vbNewLine _
        & "        ,WK.INSPECTION_TIME                                                  " & vbNewLine


    ''' <summary>
    ''' 読取データ検索追加条件(読取日From)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_ReadResul_FROM As String _
        = "    AND WK.INSPECTION_DATE >= @INKA_DATE_FROM -- FROM                               " & vbNewLine

    ''' <summary>
    ''' 読取データ検索追加条件(読取日To)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_ReadResul_TO As String _
        = "    AND WK.INSPECTION_DATE <= @INKA_DATE_TO   -- TO                                 " & vbNewLine

    ''' <summary>
    ''' 読取データ検索追加条件(荷主CD_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_ReadResul_CUSTL As String _
        = "    AND CL.CUST_CD_L = @CUST_CD_L   -- CUST_CD_L                                 " & vbNewLine

    ''' <summary>
    ''' 読取データ検索追加条件(荷主CD_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_ReadResul_CUSTM As String _
        = "    AND CL.CUST_CD_M = @CUST_CD_M   -- CUST_CD_L                                 " & vbNewLine

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 行追加(I_INKA_CYLINDER_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_I_INKA_CYLINDER_L As String _
        = " INSERT INTO $LM_TRN$..I_INKA_CYLINDER_L       " & vbNewLine _
        & "            (NRS_BR_CD                         " & vbNewLine _
        & "            ,INKA_CYL_FILE_NO_L                " & vbNewLine _
        & "            ,CUST_CD_L                         " & vbNewLine _
        & "            ,CUST_CD_M                         " & vbNewLine _
        & "            ,INKA_DATE                         " & vbNewLine _
        & "            ,CRT_DATE                          " & vbNewLine _
        & "            ,CRT_TIME                          " & vbNewLine _
        & "            ,CRT_USER_CD                       " & vbNewLine _
        & "            ,LOAD_FILE_NAME                    " & vbNewLine _
        & "            ,REMARK_1                          " & vbNewLine _
        & "            ,REMARK_2                          " & vbNewLine _
        & "            ,REMARK_3                          " & vbNewLine _
        & "            ,SYS_ENT_DATE                      " & vbNewLine _
        & "            ,SYS_ENT_TIME                      " & vbNewLine _
        & "            ,SYS_ENT_PGID                      " & vbNewLine _
        & "            ,SYS_ENT_USER                      " & vbNewLine _
        & "            ,SYS_UPD_DATE                      " & vbNewLine _
        & "            ,SYS_UPD_TIME                      " & vbNewLine _
        & "            ,SYS_UPD_PGID                      " & vbNewLine _
        & "            ,SYS_UPD_USER                      " & vbNewLine _
        & "            ,SYS_DEL_FLG)                      " & vbNewLine _
        & "      VALUES                                   " & vbNewLine _
        & "            (                                  " & vbNewLine _
        & "             @NRS_BR_CD                        " & vbNewLine _
        & "           , @INKA_CYL_FILE_NO_L               " & vbNewLine _
        & "           , @CUST_CD_L                        " & vbNewLine _
        & "           , @CUST_CD_M                        " & vbNewLine _
        & "           , @INKA_DATE                        " & vbNewLine _
        & "           , @CRT_DATE                         " & vbNewLine _
        & "           , @CRT_TIME                         " & vbNewLine _
        & "           , @CRT_USER_CD                      " & vbNewLine _
        & "           , @LOAD_FILE_NAME                   " & vbNewLine _
        & "           , @REMARK_1                         " & vbNewLine _
        & "           , @REMARK_2                         " & vbNewLine _
        & "           , @REMARK_3                         " & vbNewLine _
        & "           , @SYS_ENT_DATE                     " & vbNewLine _
        & "           , @SYS_ENT_TIME                     " & vbNewLine _
        & "           , @SYS_ENT_PGID                     " & vbNewLine _
        & "           , @SYS_ENT_USER                     " & vbNewLine _
        & "           , @SYS_UPD_DATE                     " & vbNewLine _
        & "           , @SYS_UPD_TIME                     " & vbNewLine _
        & "           , @SYS_UPD_PGID                     " & vbNewLine _
        & "           , @SYS_UPD_USER                     " & vbNewLine _
        & "           , @SYS_DEL_FLG                      " & vbNewLine _
        & "             )                                 " & vbNewLine

    ''' <summary>
    ''' 行追加(I_INKA_CYLINDER_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_I_INKA_CYLINDER_M As String _
        = " INSERT INTO $LM_TRN$..I_INKA_CYLINDER_M       " & vbNewLine _
        & "            (NRS_BR_CD                         " & vbNewLine _
        & "            ,INKA_CYL_FILE_NO_L                " & vbNewLine _
        & "            ,INKA_CYL_FILE_NO_M                " & vbNewLine _
        & "            ,ROW_NO                            " & vbNewLine _
        & "            ,GAS_NAME                          " & vbNewLine _
        & "            ,VOLUME                            " & vbNewLine _
        & "            ,SERIAL_NO                         " & vbNewLine _
        & "            ,SYS_ENT_DATE                      " & vbNewLine _
        & "            ,SYS_ENT_TIME                      " & vbNewLine _
        & "            ,SYS_ENT_PGID                      " & vbNewLine _
        & "            ,SYS_ENT_USER                      " & vbNewLine _
        & "            ,SYS_UPD_DATE                      " & vbNewLine _
        & "            ,SYS_UPD_TIME                      " & vbNewLine _
        & "            ,SYS_UPD_PGID                      " & vbNewLine _
        & "            ,SYS_UPD_USER                      " & vbNewLine _
        & "            ,SYS_DEL_FLG)                      " & vbNewLine _
        & "      VALUES                                   " & vbNewLine _
        & "            (                                  " & vbNewLine _
        & "             @NRS_BR_CD                        " & vbNewLine _
        & "           , @INKA_CYL_FILE_NO_L               " & vbNewLine _
        & "           , @INKA_CYL_FILE_NO_M               " & vbNewLine _
        & "           , @ROW_NO                           " & vbNewLine _
        & "           , @GAS_NAME                         " & vbNewLine _
        & "           , @VOLUME                           " & vbNewLine _
        & "           , @SERIAL_NO                        " & vbNewLine _
        & "           , @SYS_ENT_DATE                     " & vbNewLine _
        & "           , @SYS_ENT_TIME                     " & vbNewLine _
        & "           , @SYS_ENT_PGID                     " & vbNewLine _
        & "           , @SYS_ENT_USER                     " & vbNewLine _
        & "           , @SYS_UPD_DATE                     " & vbNewLine _
        & "           , @SYS_UPD_TIME                     " & vbNewLine _
        & "           , @SYS_UPD_PGID                     " & vbNewLine _
        & "           , @SYS_UPD_USER                     " & vbNewLine _
        & "           , @SYS_DEL_FLG                      " & vbNewLine _
        & "             )                                 " & vbNewLine

#End Region

#Region "論理削除"

    ''' <summary>
    ''' 論理削除(INKA_CYLINDER_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SYS_DEL_FLG_INKA_CYLINDER_L As String _
        = " UPDATE $LM_TRN$..I_INKA_CYLINDER_L                     " & vbNewLine _
        & "    SET SYS_UPD_DATE = @SYS_UPD_DATE                    " & vbNewLine _
        & "      , SYS_UPD_TIME = @SYS_UPD_TIME                    " & vbNewLine _
        & "      , SYS_UPD_PGID = @SYS_UPD_PGID                    " & vbNewLine _
        & "      , SYS_UPD_USER = @SYS_UPD_USER                    " & vbNewLine _
        & "      , SYS_DEL_FLG  = @SYS_DEL_FLG                     " & vbNewLine _
        & "  WHERE                                                 " & vbNewLine _
        & "        NRS_BR_CD          = @NRS_BR_CD                 " & vbNewLine _
        & "    AND INKA_CYL_FILE_NO_L = @INKA_CYL_FILE_NO_L        " & vbNewLine _
        & "    AND SYS_UPD_DATE       = @LAST_UPD_DATE             " & vbNewLine _
        & "    AND SYS_UPD_TIME       = @LAST_UPD_TIME             " & vbNewLine

    ''' <summary>
    ''' 論理削除(INKA_CYLINDER_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SYS_DEL_FLG_INKA_CYLINDER_M As String _
        = " UPDATE $LM_TRN$..I_INKA_CYLINDER_M                     " & vbNewLine _
        & "    SET SYS_UPD_DATE = @SYS_UPD_DATE                    " & vbNewLine _
        & "      , SYS_UPD_TIME = @SYS_UPD_TIME                    " & vbNewLine _
        & "      , SYS_UPD_PGID = @SYS_UPD_PGID                    " & vbNewLine _
        & "      , SYS_UPD_USER = @SYS_UPD_USER                    " & vbNewLine _
        & "      , SYS_DEL_FLG  = @SYS_DEL_FLG                     " & vbNewLine _
        & "  WHERE                                                 " & vbNewLine _
        & "        NRS_BR_CD          = @NRS_BR_CD                 " & vbNewLine _
        & "    AND INKA_CYL_FILE_NO_L = @INKA_CYL_FILE_NO_L        " & vbNewLine

#End Region

#End Region

#Region "Field"

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder = Nothing

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = Nothing

#End Region

#Region "Method"

#Region "SQLメイン処理"

#Region "取込データ取得"

    ''' <summary>
    ''' シリンダーファイル取込一覧取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLoadedInkaCylFileListCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        ' 営業所コード
        Dim nrsBrCd As String = inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_SELECT_LOADED_INKA_CYL_FILES_COUNT)       'SQL構築(Select句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' 検索条件設定
        Me.AppendSqlWhereClause(inRow)

        ' SQLパラメータ設定
        Me.SetSQLSelectLoadedInkaCylFileListParameter(inRow)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray())

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            Dim rowCount As Integer = 0

            If (reader.HasRows) Then

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                For Each column As String In {
                                               COL_NAME.COUNT
                                             }
                    map.Add(column, column)
                Next

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUT_COUNT)


                If (Integer.TryParse(ds.Tables(TABLE_NAME.OUT_COUNT).Rows(0) _
                                     .Item(COL_NAME.COUNT).ToString(), rowCount) = False) Then
                    rowCount = 0
                End If

            End If

            MyBase.SetResultCount(rowCount)

        End Using

        Return ds

    End Function



    ''' <summary>
    ''' シリンダーファイル取込一覧取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLoadedInkaCylFileList(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        ' 営業所コード
        Dim nrsBrCd As String = inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_SELECT_LOADED_INKA_CYL_FILES)       'SQL構築(Select句)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' 検索条件設定
        Me.AppendSqlWhereClause(inRow)

        ' SQLパラメータ設定
        Me.SetSQLSelectLoadedInkaCylFileListParameter(inRow)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray())

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            Dim rowCount As Integer = 0

            If (reader.HasRows) Then

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                For Each column As String In {
                                             OUTPUT_COLUMN_NM.NRS_BR_CD,
                                             OUTPUT_COLUMN_NM.INKA_CYL_FILE_NO_L,
                                             OUTPUT_COLUMN_NM.CUST_CD_L,
                                             OUTPUT_COLUMN_NM.CUST_CD_M,
                                             OUTPUT_COLUMN_NM.INKA_DATE,
                                             OUTPUT_COLUMN_NM.LOAD_FILE_NAME,
                                             OUTPUT_COLUMN_NM.CYL_COUNT,
                                             OUTPUT_COLUMN_NM.REMARK_1,
                                             OUTPUT_COLUMN_NM.REMARK_2,
                                             OUTPUT_COLUMN_NM.REMARK_3,
                                             OUTPUT_COLUMN_NM.CRT_DATE,
                                             OUTPUT_COLUMN_NM.CRT_TIME,
                                             OUTPUT_COLUMN_NM.CRT_USER_NM,
                                             OUTPUT_COLUMN_NM.LAST_UPD_DATE,
                                             OUTPUT_COLUMN_NM.LAST_UPD_TIME
                                           }
                    map.Add(column, column)
                Next

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUTPUT)

                rowCount = ds.Tables(TABLE_NAME.OUTPUT).Rows.Count

            End If

            MyBase.SetResultCount(rowCount)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' Where条件追加
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub AppendSqlWhereClause(ByVal row As DataRow)

        If (row Is Nothing) Then Return

        If (Len(row.Item(INPUT_COLUMN_NM.INKA_DATE_FROM)) > 0) Then

            Me._StrSql.Append(SQL_WHERE_LOADED_INKA_CYL_FILES_ADD_INKA_FROM)

        End If

        If (Len(row.Item(INPUT_COLUMN_NM.INKA_DATE_TO)) > 0) Then

            Me._StrSql.Append(SQL_WHERE_LOADED_INKA_CYL_FILES_ADD_INKA_TO)

        End If

    End Sub


    ''' <summary>
    ''' 読取Where条件追加
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub AppendSqlWhereClauseRead(ByVal row As DataRow)

        If (row Is Nothing) Then Return

        If (Len(row.Item(INPUT_COLUMN_NM.INKA_DATE_FROM)) > 0) Then

            Me._StrSql.Append(SQL_WHERE_ReadResul_FROM)

        End If

        If (Len(row.Item(INPUT_COLUMN_NM.INKA_DATE_TO)) > 0) Then

            Me._StrSql.Append(SQL_WHERE_ReadResul_TO)

        End If

        If (Len(row.Item(INPUT_COLUMN_NM.CUST_CD_L)) > 0) Then

            Me._StrSql.Append(SQL_WHERE_ReadResul_CUSTL)

        End If

        If (Len(row.Item(INPUT_COLUMN_NM.CUST_CD_M)) > 0) Then

            Me._StrSql.Append(SQL_WHERE_ReadResul_CUSTM)

        End If
    End Sub

    ''' <summary>
    ''' 検品結果取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SelectInspectionData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        ' 営業所コード
        Dim nrsBrCd As String = inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_SELECT_INSPECTION_DATA)       'SQL構築(Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd)

        Me.SetSQLSelectInspectionDataParameter(inRow)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            If (reader.HasRows) Then

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                For Each column As String In {
                                             OUT_INSPECT_COLUMN_NM.NRS_BR_CD,
                                             OUT_INSPECT_COLUMN_NM.INKA_CYL_FILE_NO_L,
                                             OUT_INSPECT_COLUMN_NM.LOAD_FILE_NAME,
                                             OUT_INSPECT_COLUMN_NM.INKA_DATE,
                                             OUT_INSPECT_COLUMN_NM.REMARK_1,
                                             OUT_INSPECT_COLUMN_NM.REMARK_2,
                                             OUT_INSPECT_COLUMN_NM.REMARK_3,
                                             OUT_INSPECT_COLUMN_NM.GAS_NAME,
                                             OUT_INSPECT_COLUMN_NM.VOLUME,
                                             OUT_INSPECT_COLUMN_NM.SERIAL_NO,
                                             OUT_INSPECT_COLUMN_NM.INSPECTION_USER_NM,
                                             OUT_INSPECT_COLUMN_NM.IS_LOAD,
                                             OUT_INSPECT_COLUMN_NM.IS_SCAN
                                            }
                    map.Add(column, column)
                Next

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUT_INSPECTION_DATA)

                MyBase.SetResultCount(ds.Tables(TABLE_NAME.OUTPUT).Rows.Count)

            End If

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' 読取結果取得 ADD 2017/04/24
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SelectReadResulData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inRow As DataRow = ds.Tables(TABLE_NAME.INPUT).Rows(0)

        ' 営業所コード
        Dim nrsBrCd As String = inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_SelectReadResulData)       'SQL構築(Select句)

        ' 検索条件設定
        Me.AppendSqlWhereClauseRead(inRow)

        'ソート
        Me._StrSql.Append(LMI430DAC.SQL_SelectReadResulDataOrderBY)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd)

        Me.SetSQLSelectReadResulDataParameter(inRow)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            If (reader.HasRows) Then

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                For Each column As String In {
                                             OUT_READ_COLUMN_NM.NRS_BR_CD,
                                             OUT_READ_COLUMN_NM.INKA_CYL_FILE_NO_L,
                                             OUT_READ_COLUMN_NM.SERIAL_NO,
                                             OUT_READ_COLUMN_NM.CUST_CD_L,
                                             OUT_READ_COLUMN_NM.CUST_CD_M,
                                             OUT_READ_COLUMN_NM.INSPECTION_DATE,
                                             OUT_READ_COLUMN_NM.INSPECTION_TIME,
                                             OUT_READ_COLUMN_NM.INSPECTION_USER_NM
                                            }
                    map.Add(column, column)
                Next

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NAME.OUT_READ_DATA)

                MyBase.SetResultCount(ds.Tables(TABLE_NAME.OUTPUT).Rows.Count)

            End If

        End Using

        Return ds

    End Function
#End Region

#Region "追加"

    ''' <summary>
    ''' 行追加(InkaCylinderL)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaCylinderL(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NAME.INPUT)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_INSERT_I_INKA_CYLINDER_L)

        For Each row As DataRow In inTbl.Rows

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                             , row.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString())
            'SQL文のコンパイル
            Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetSQLInsertInkaCylinderLParameter(row)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

            End Using

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 行追加(InkaCylinderM)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaCylinderM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NAME.IN_CYLINDER)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_INSERT_I_INKA_CYLINDER_M)

        Dim resultCount As Integer = 0

        For Each row As DataRow In inTbl.Rows

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                             , row.Item(IN_CYL_COLUMN_NM.NRS_BR_CD).ToString())

            'SQL文のコンパイル
            Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetSQLInsertInkaCylinderMParameter(row)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

                'SQLの発行                
                resultCount += MyBase.GetInsertResult(cmd)

            End Using

        Next

        ' 処理件数格納
        Me.SetResultCount(resultCount)

        Return ds

    End Function
#End Region

#Region "論理削除"

    ''' <summary>
    ''' 論理削除(入荷シリンダー)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoftDeleteInkaCylinder(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NAME.INPUT)

        Dim inRow As DataRow = inTbl.Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , inRow.Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString())
        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの初期化
            cmd.Parameters.Clear()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ（個別項目）設定
            Call Me.SetSQLSoftDeleteInkaCylinderParameter(inRow)

            'SQLパラメータ（システム項目）設定
            Call Me.SetSysdataParameter()

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray())

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Me.SetResultCount(MyBase.GetUpdateResult(cmd))

        End Using


        Return ds

    End Function

    ''' <summary>
    ''' 論理削除(InkaCylinderL)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoftDeleteInkaCylinderL(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_UPDATE_SYS_DEL_FLG_INKA_CYLINDER_L)

        Return SoftDeleteInkaCylinder(ds)

    End Function


    ''' <summary>
    ''' 論理削除(InkaCylinderM)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoftDeleteInkaCylinderM(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI430DAC.SQL_UPDATE_SYS_DEL_FLG_INKA_CYLINDER_M)

        Return SoftDeleteInkaCylinder(ds)

    End Function


#End Region

#End Region

#Region "SQLパラメータ設定"

    ''' <summary>
    ''' パラメータ設定(取込ファイル取得)
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectLoadedInkaCylFileListParameter(ByVal inTblRow As DataRow)

        With inTblRow
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item(INPUT_COLUMN_NM.CUST_CD_L).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item(INPUT_COLUMN_NM.CUST_CD_M).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", .Item(INPUT_COLUMN_NM.INKA_DATE_FROM).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", .Item(INPUT_COLUMN_NM.INKA_DATE_TO).ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定(検品結果検索)
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectInspectionDataParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CYL_FILE_NO_L", .Item(INPUT_COLUMN_NM.INKA_CYL_FILE_NO_L).ToString(), DBDataType.CHAR))
        End With

    End Sub


    ''' <summary>
    ''' パラメータ設定(読取結果検索)
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSelectReadResulDataParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", .Item(INPUT_COLUMN_NM.INKA_DATE_FROM).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", .Item(INPUT_COLUMN_NM.INKA_DATE_TO).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item(INPUT_COLUMN_NM.CUST_CD_L).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item(INPUT_COLUMN_NM.CUST_CD_M).ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定(論理削除)
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <remarks></remarks>
    Private Sub SetSQLSoftDeleteInkaCylinderParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CYL_FILE_NO_L", .Item(INPUT_COLUMN_NM.INKA_CYL_FILE_NO_L).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_DATE", .Item(INPUT_COLUMN_NM.LAST_UPD_DATE).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_TIME", .Item(INPUT_COLUMN_NM.LAST_UPD_TIME).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.ON, DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定(InkaCylinderL追加)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInsertInkaCylinderLParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CYL_FILE_NO_L", .Item(INPUT_COLUMN_NM.INKA_CYL_FILE_NO_L).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOAD_FILE_NAME", .Item(INPUT_COLUMN_NM.LOAD_FILE_NAME).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item(INPUT_COLUMN_NM.CUST_CD_L).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item(INPUT_COLUMN_NM.CUST_CD_M).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item(INPUT_COLUMN_NM.INKA_DATE).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_1", .Item(INPUT_COLUMN_NM.REMARK_1).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_2", .Item(INPUT_COLUMN_NM.REMARK_2).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_3", .Item(INPUT_COLUMN_NM.REMARK_3).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.GetColonEditTime(MyBase.GetSystemTime()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_USER_CD", MyBase.GetUserID(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定(InkaCylinderM追加)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLInsertInkaCylinderMParameter(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        With inTblRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item(IN_CYL_COLUMN_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CYL_FILE_NO_L", .Item(IN_CYL_COLUMN_NM.INKA_CYL_FILE_NO_L).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CYL_FILE_NO_M", .Item(IN_CYL_COLUMN_NM.INKA_CYL_FILE_NO_M).ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", .Item(IN_CYL_COLUMN_NM.ROW_NO).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GAS_NAME", .Item(IN_CYL_COLUMN_NM.GAS_NAME).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VOLUME", .Item(IN_CYL_COLUMN_NM.VOLUME).ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item(IN_CYL_COLUMN_NM.SERIAL_NO).ToString(), DBDataType.NVARCHAR))

        End With

    End Sub


#Region "パラメータ設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter()

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter()

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

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

#Region "ユーティリティ"

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

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

#End Region

#End Region

End Class


