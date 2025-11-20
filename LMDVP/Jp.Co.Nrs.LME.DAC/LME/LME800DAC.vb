' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LME800    : 現場作業指示
'  作  成  者       :  [HOJO]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports System.Reflection

''' <summary>
''' LME800DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME800DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "テーブル名"
    ''' <summary>
    ''' テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM

        Public Const LME800IN As String = "LME800IN"
        Public Const LME800CNT As String = "LME800CNT"
        Public Const LME800OUT_SAGYO As String = "LME800OUT_SAGYO"
        Public Const LME800OUT_SAGYO_SIJI As String = "LME800OUT_SAGYO_SIJI"
        Public Const LME800IN_SAGYO As String = "LME800IN_SAGYO"
    End Class

#End Region

#Region "ファンクション名"
    ''' <summary>
    ''' ファンクション名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NM
        Public Const SAGYO_SIJI_SELECT As String = "SelectSagyoSiji"
        Public Const SAGYO_SIJI_INSERT As String = "InsertSagyoSiji"
        Public Const SAGYO_SIJI_CANCEL As String = "UpdateSagyoSijiCancel"
        Public Const SAGYO_SIJI_DELETE As String = "UpdateSagyoSijiDelete"
        Public Const SAGYO_INSERT As String = "InsertSagyo"
        Public Const CHECK_HAITA As String = "CheckHaita"
        Public Const CHECK_TABLET_USE As String = "CheckTabletUse"
        Public Const WH_STATUS_UPDATE As String = "UpdateWHSagyoShijiStatus"
        Public Const SAGYO_DATA As String = "SelectSagyoData"
    End Class

#End Region

#Region "Select"

#Region "作業指示取得"
    ''' <summary>
    ''' 作業指示取得取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TD_SAGYO_SIJI As String _
    = " SELECT                                                                          " & vbNewLine _
    & "   MAIN.SAGYO_SIJI_NO                       AS SAGYO_SIJI_NO                     " & vbNewLine _
    & " , MAIN.NRS_BR_CD                           AS NRS_BR_CD                         " & vbNewLine _
    & " , MAIN.WORK_SEQ                            AS WORK_SEQ                          " & vbNewLine _
    & " , MAIN.WORK_STATE_KB                       AS WORK_STATE_KB                     " & vbNewLine _
    & " , MAIN.CANCEL_FLG                          AS CANCEL_FLG                        " & vbNewLine _
    & " , CASE WHEN @PROC_TYPE = '00'  THEN '01'                                        " & vbNewLine _
    & "        WHEN @PROC_TYPE = '01'  THEN '02'                                        " & vbNewLine _
    & "        WHEN @PROC_TYPE = '02'  THEN '02'                                        " & vbNewLine _
    & "        ELSE ''                                                                  " & vbNewLine _
    & "   END                                      AS CANCEL_TYPE                       " & vbNewLine _
    & " , MAIN.CUST_CD_L                           AS CUST_CD_L                         " & vbNewLine _
    & " , MAIN.CUST_CD_M                           AS CUST_CD_M                         " & vbNewLine _
    & " , MAIN.CUST_NM_L                           AS CUST_NM_L                         " & vbNewLine _
    & " , MAIN.CUST_NM_M                           AS CUST_NM_M                         " & vbNewLine _
    & " , MAIN.WH_CD                               AS WH_CD                             " & vbNewLine _
    & " , MAIN.WH_NM                               AS WH_NM                             " & vbNewLine _
    & " , MAIN.SAGYO_COMP_DATE                     AS SAGYO_COMP_DATE                   " & vbNewLine _
    & " , MAIN.REMARK_1                            AS REMARK_1                          " & vbNewLine _
    & " , MAIN.REMARK_2                            AS REMARK_2                          " & vbNewLine _
    & " , MAIN.REMARK_3                            AS REMARK_3                          " & vbNewLine _
    & " , MAIN.REMARK_CHK_FLG                      AS REMARK_CHK_FLG                    " & vbNewLine _
    & " , MAIN.SYS_ENT_DATE                        AS SYS_ENT_DATE                      " & vbNewLine _
    & " , MAIN.SYS_ENT_TIME                        AS SYS_ENT_TIME                      " & vbNewLine _
    & " , MAIN.SYS_ENT_PGID                        AS SYS_ENT_PGID                      " & vbNewLine _
    & " , MAIN.SYS_ENT_USER                        AS SYS_ENT_USER                      " & vbNewLine _
    & " , MAIN.SYS_UPD_DATE                        AS SYS_UPD_DATE                      " & vbNewLine _
    & " , MAIN.SYS_UPD_TIME                        AS SYS_UPD_TIME                      " & vbNewLine _
    & " , MAIN.SYS_UPD_PGID                        AS SYS_UPD_PGID                      " & vbNewLine _
    & " , MAIN.SYS_UPD_USER                        AS SYS_UPD_USER                      " & vbNewLine _
    & " , MAIN.SYS_DEL_FLG                         AS SYS_DEL_FLG                       " & vbNewLine _
    & " FROM $LM_TRN$..TD_SAGYO_SIJI MAIN                                               " & vbNewLine _
    & " INNER JOIN (                                                                    " & vbNewLine _
    & "     SELECT                                                                      " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,SAGYO_SIJI_NO                                                           " & vbNewLine _
    & "        ,MAX(WORK_SEQ) AS WORK_SEQ                                               " & vbNewLine _
    & "     FROM $LM_TRN$..TD_SAGYO_SIJI                                                " & vbNewLine _
    & "     GROUP BY                                                                    " & vbNewLine _
    & "         NRS_BR_CD                                                               " & vbNewLine _
    & "        ,SAGYO_SIJI_NO                                                           " & vbNewLine _
    & "     )MAX_SEQ                                                                    " & vbNewLine _
    & " ON  MAX_SEQ.NRS_BR_CD     = MAIN.NRS_BR_CD                                      " & vbNewLine _
    & " AND MAX_SEQ.SAGYO_SIJI_NO = MAIN.SAGYO_SIJI_NO                                  " & vbNewLine _
    & " AND MAX_SEQ.WORK_SEQ      = MAIN.WORK_SEQ                                       " & vbNewLine _
    & " WHERE                                                                           " & vbNewLine _
    & "     MAIN.NRS_BR_CD     = @NRS_BR_CD                                             " & vbNewLine _
    & " AND MAIN.SAGYO_SIJI_NO = @SAGYO_SIJI_NO                                         " & vbNewLine

#End Region

#Region "作業登録データ取得"
    ''' <summary>
    ''' 作業登録データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO_DATA As String _
    = " SELECT                                                                                               " & vbNewLine _
    & "      SAGYO.NRS_BR_CD                                                        AS NRS_BR_CD             " & vbNewLine _
    & "     ,SAGYO.SAGYO_REC_NO                                                     AS SAGYO_REC_NO          " & vbNewLine _
    & "     ,ISNULL((SELECT MAX(WORK_SEQ)                                                                    " & vbNewLine _
    & "              FROM $LM_TRN$..TD_SAGYO_SIJI                                                            " & vbNewLine _
    & "              WHERE TD_SAGYO_SIJI.NRS_BR_CD      = SAGYO_SIJI.NRS_BR_CD                               " & vbNewLine _
    & "                 AND TD_SAGYO_SIJI.SAGYO_SIJI_NO = SAGYO_SIJI.SAGYO_SIJI_NO                           " & vbNewLine _
    & "              GROUP BY TD_SAGYO_SIJI.NRS_BR_CD,TD_SAGYO_SIJI.SAGYO_SIJI_NO                            " & vbNewLine _
    & "             ),0) + 1                                                        AS WORK_SEQ              " & vbNewLine _
    & "     ,SAGYO.SAGYO_SIJI_NO                                                    AS SAGYO_SIJI_NO         " & vbNewLine _
    & "     ,CASE WHEN TOUSITU.JISYATASYA_KB = '01' THEN '00'                                                " & vbNewLine _
    & "           WHEN TOUSITU.JISYATASYA_KB = '02' THEN '01'                                                " & vbNewLine _
    & "           ELSE                                    ''                                                 " & vbNewLine _
    & "      END                                                                    AS SAGYO_STATE_KB        " & vbNewLine _
    & "     ,SAGYO.WH_CD                                                            AS WH_CD                 " & vbNewLine _
    & "     ,SAGYO.GOODS_CD_NRS                                                     AS GOODS_CD_NRS          " & vbNewLine _
    & "     ,SAGYO.GOODS_NM_NRS                                                     AS GOODS_NM_NRS          " & vbNewLine _
    & "     ,ISNULL(GOODS.STD_IRIME_NB, 0)                                          AS IRIME                 " & vbNewLine _
    & "     ,ISNULL(GOODS.STD_IRIME_UT, '')                                         AS IRIME_UT              " & vbNewLine _
    & "     ,ISNULL(GOODS.PKG_NB, 0)                                                AS PKG_NB                " & vbNewLine _
    & "     ,ISNULL(GOODS.PKG_UT, 0)                                                AS PKG_UT                " & vbNewLine _
    & "     ,SAGYO.LOT_NO                                                           AS LOT_NO                " & vbNewLine _
    & "     ,ISNULL(DZT.TOU_NO, '')                                                 AS TOU_NO                " & vbNewLine _
    & "     ,ISNULL(DZT.SITU_NO, '')                                                AS SITU_NO               " & vbNewLine _
    & "     ,ISNULL(DZT.ZONE_CD, '')                                                AS ZONE_CD               " & vbNewLine _
    & "     ,ISNULL(DZT.LOCA, '')                                                   AS LOCA                  " & vbNewLine _
    & "     ,SAGYO.SAGYO_CD                                                         AS SAGYO_CD              " & vbNewLine _
    & "     ,ISNULL(MSAGYO.WH_SAGYO_NM, '')                                         AS SAGYO_NM              " & vbNewLine _
    & "     ,SAGYO.INV_TANI                                                         AS INV_TANI              " & vbNewLine _
    & "     ,ISNULL(MSAGYO.KOSU_BAI, '')                                            AS KOSU_BAI              " & vbNewLine _
    & "     ,SAGYO.SAGYO_NB                                                         AS SAGYO_NB              " & vbNewLine _
    & "     ,SAGYO.REMARK_SIJI                                                      AS REMARK                " & vbNewLine _
    & "     ,ISNULL(TOUSITU.JISYATASYA_KB,'')                                       AS JISYATASYA_KB         " & vbNewLine _
    & "     ,SAGYO.IOZS_KB                                                          AS IOZS_KB               " & vbNewLine _
    & "     ,ISNULL(SUSER.USER_NM,'')                                               AS USER_NM               " & vbNewLine _
    & " FROM $LM_TRN$..E_SAGYO_SIJI SAGYO_SIJI                                                               " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..E_SAGYO SAGYO                                                                    " & vbNewLine _
    & " ON  SAGYO_SIJI.NRS_BR_CD     = SAGYO.NRS_BR_CD                                                       " & vbNewLine _
    & " AND SAGYO_SIJI.SAGYO_SIJI_NO = SAGYO.SAGYO_SIJI_NO                                                   " & vbNewLine _
    & " AND SAGYO.SYS_DEL_FLG        = '0'                                                                   " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                    " & vbNewLine _
    & " ON  SAGYO.NRS_BR_CD    = GOODS.NRS_BR_CD                                                             " & vbNewLine _
    & " AND SAGYO.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                                                          " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO MSAGYO                                                                   " & vbNewLine _
    & " ON  MSAGYO.NRS_BR_CD = SAGYO.NRS_BR_CD                                                               " & vbNewLine _
    & " AND MSAGYO.SAGYO_CD  = SAGYO.SAGYO_CD                                                                " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..D_ZAI_TRS DZT                                                                    " & vbNewLine _
    & " ON  DZT.NRS_BR_CD   = SAGYO.NRS_BR_CD                                                                " & vbNewLine _
    & " AND DZT.ZAI_REC_NO  = SAGYO.ZAI_REC_NO                                                               " & vbNewLine _
    & " AND DZT.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_TOU_SITU TOUSITU                                                               " & vbNewLine _
    & " ON  TOUSITU.NRS_BR_CD = DZT.NRS_BR_CD                                                                " & vbNewLine _
    & " AND TOUSITU.WH_CD     = DZT.WH_CD                                                                    " & vbNewLine _
    & " AND TOUSITU.TOU_NO    = DZT.TOU_NO                                                                   " & vbNewLine _
    & " AND TOUSITU.SITU_NO   = DZT.SITU_NO                                                                  " & vbNewLine _
    & " LEFT JOIN $LM_MST$..S_USER SUSER                                                                     " & vbNewLine _
    & " ON  SUSER.USER_CD     = TOUSITU.USER_CD                                                              " & vbNewLine _
    & " AND SUSER.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
    & " WHERE SAGYO.NRS_BR_CD     = @NRS_BR_CD                                                               " & vbNewLine _
    & " AND   SAGYO.SAGYO_SIJI_NO = @SAGYO_SIJI_NO                                                           " & vbNewLine _
    & " AND   MSAGYO.WH_SAGYO_YN  = '01'                                                                     " & vbNewLine

#End Region

#End Region

#Region "Insert"

#Region "作業指示登録"
    ''' <summary>
    ''' 作業指示登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TD_SAGYO_SIJI As String _
    = " INSERT INTO $LM_TRN$..TD_SAGYO_SIJI (                                                                         " & vbNewLine _
    & "  SAGYO_SIJI_NO                                                                                                " & vbNewLine _
    & " ,NRS_BR_CD                                                                                                    " & vbNewLine _
    & " ,WORK_SEQ                                                                                                     " & vbNewLine _
    & " ,CANCEL_FLG                                                                                                   " & vbNewLine _
    & " ,WORK_STATE_KB                                                                                                " & vbNewLine _
    & " ,CUST_CD_L                                                                                                    " & vbNewLine _
    & " ,CUST_CD_M                                                                                                    " & vbNewLine _
    & " ,CUST_NM_L                                                                                                    " & vbNewLine _
    & " ,CUST_NM_M                                                                                                    " & vbNewLine _
    & " ,WH_CD                                                                                                        " & vbNewLine _
    & " ,WH_NM                                                                                                        " & vbNewLine _
    & " ,SAGYO_COMP_DATE                                                                                              " & vbNewLine _
    & " ,REMARK_1                                                                                                     " & vbNewLine _
    & " ,REMARK_2                                                                                                     " & vbNewLine _
    & " ,REMARK_3                                                                                                     " & vbNewLine _
    & " ,REMARK_CHK_FLG                                                                                               " & vbNewLine _
    & " ,SYS_ENT_DATE                                                                                                 " & vbNewLine _
    & " ,SYS_ENT_TIME                                                                                                 " & vbNewLine _
    & " ,SYS_ENT_PGID                                                                                                 " & vbNewLine _
    & " ,SYS_ENT_USER                                                                                                 " & vbNewLine _
    & " ,SYS_UPD_DATE                                                                                                 " & vbNewLine _
    & " ,SYS_UPD_TIME                                                                                                 " & vbNewLine _
    & " ,SYS_UPD_PGID                                                                                                 " & vbNewLine _
    & " ,SYS_UPD_USER                                                                                                 " & vbNewLine _
    & " ,SYS_DEL_FLG                                                                                                  " & vbNewLine _
    & " )                                                                                                             " & vbNewLine _
    & " SELECT                                                                                                        " & vbNewLine _
    & "      SAGYO_SIJI.SAGYO_SIJI_NO                                                   AS SAGYO_SIJI_NO              " & vbNewLine _
    & "     ,SAGYO_SIJI.NRS_BR_CD                                                       AS NRS_BR_CD                  " & vbNewLine _
    & "     ,ISNULL((SELECT MAX(WORK_SEQ)                                                                             " & vbNewLine _
    & "                  FROM $LM_TRN$..TD_SAGYO_SIJI                                                                 " & vbNewLine _
    & "                  WHERE TD_SAGYO_SIJI.NRS_BR_CD      = SAGYO_SIJI.NRS_BR_CD                                    " & vbNewLine _
    & "                     AND TD_SAGYO_SIJI.SAGYO_SIJI_NO = SAGYO_SIJI.SAGYO_SIJI_NO                                " & vbNewLine _
    & "                  GROUP BY TD_SAGYO_SIJI.NRS_BR_CD,TD_SAGYO_SIJI.SAGYO_SIJI_NO                                 " & vbNewLine _
    & "                 ),0) + 1                                                        AS WORK_SEQ                   " & vbNewLine _
    & "     ,'00'                                                                       AS CANCEL_FLG                 " & vbNewLine _
    & "     ,'00'                                                                       AS WORK_STATE_KB              " & vbNewLine _
    & "     ,CUST.CUST_CD_L                                                             AS CUST_CD_L                  " & vbNewLine _
    & "     ,CUST.CUST_CD_M                                                             AS CUST_CD_M                  " & vbNewLine _
    & "     ,CUST.CUST_NM_L                                                             AS CUST_NM_L                  " & vbNewLine _
    & "     ,CUST.CUST_NM_M                                                             AS CUST_NM_M                  " & vbNewLine _
    & "     ,SAGYO_SIJI.WH_CD                                                           AS WH_CD                      " & vbNewLine _
    & "     ,SOKO.WH_NM                                                                 AS WH_NM                      " & vbNewLine _
    & "     ,SAGYO_SIJI.SAGYO_SIJI_DATE                                                 AS SAGYO_COMP_DATE            " & vbNewLine _
    & "     ,SAGYO_SIJI.REMARK_1                                                        AS REMARK_1                   " & vbNewLine _
    & "     ,SAGYO_SIJI.REMARK_2                                                        AS REMARK_2                   " & vbNewLine _
    & "     ,SAGYO_SIJI.REMARK_3                                                        AS REMARK_3                   " & vbNewLine _
    & "     ,CASE WHEN LEN(SAGYO_SIJI.REMARK_3 + SAGYO_SIJI.REMARK_3 + SAGYO_SIJI.REMARK_3) = 0                       " & vbNewLine _
    & "           THEN '01'                                                                                           " & vbNewLine _
    & "           ELSE '00'                                                                                           " & vbNewLine _
    & "      END                                                                        AS REMARK_CHK_FLG             " & vbNewLine _
    & "     ,@SYS_ENT_DATE                                                              AS SYS_ENT_DATE               " & vbNewLine _
    & "     ,@SYS_ENT_TIME                                                              AS SYS_ENT_TIME               " & vbNewLine _
    & "     ,@SYS_ENT_PGID                                                              AS SYS_ENT_PGID               " & vbNewLine _
    & "     ,@SYS_ENT_USER                                                              AS SYS_ENT_USER               " & vbNewLine _
    & "     ,@SYS_UPD_DATE                                                              AS SYS_UPD_DATE               " & vbNewLine _
    & "     ,@SYS_UPD_TIME                                                              AS SYS_UPD_TIME               " & vbNewLine _
    & "     ,@SYS_UPD_PGID                                                              AS SYS_UPD_PGID               " & vbNewLine _
    & "     ,@SYS_UPD_USER                                                              AS SYS_UPD_USER               " & vbNewLine _
    & "     ,'0'                                                                        AS SYS_DEL_FLG                " & vbNewLine _
    & " FROM $LM_TRN$..E_SAGYO_SIJI SAGYO_SIJI                                                                        " & vbNewLine _
    & " LEFT JOIN (                                                                                                   " & vbNewLine _
    & "     SELECT                                                                                                    " & vbNewLine _
    & "          SAGYO.NRS_BR_CD                                                                                      " & vbNewLine _
    & "         ,SAGYO.SAGYO_SIJI_NO                                                                                  " & vbNewLine _
    & "         ,MIN(SAGYO.SAGYO_REC_NO) AS MIN_SAGYO_REC_NO                                                          " & vbNewLine _
    & "     FROM $LM_TRN$..E_SAGYO SAGYO                                                                              " & vbNewLine _
    & "     LEFT JOIN $LM_MST$..M_SAGYO MSAGYO                                                                        " & vbNewLine _
    & "     ON  MSAGYO.NRS_BR_CD = SAGYO.NRS_BR_CD                                                                    " & vbNewLine _
    & "     AND MSAGYO.SAGYO_CD  = SAGYO.SAGYO_CD                                                                     " & vbNewLine _
    & "     WHERE SAGYO.SYS_DEL_FLG  = '0'                                                                            " & vbNewLine _
    & "     AND   MSAGYO.WH_SAGYO_YN = '01'                                                                           " & vbNewLine _
    & "     GROUP BY SAGYO.NRS_BR_CD, SAGYO.SAGYO_SIJI_NO                                                             " & vbNewLine _
    & " )MIN_SAGYO                                                                                                    " & vbNewLine _
    & " ON  SAGYO_SIJI.NRS_BR_CD     = MIN_SAGYO.NRS_BR_CD                                                            " & vbNewLine _
    & " AND SAGYO_SIJI.SAGYO_SIJI_NO = MIN_SAGYO.SAGYO_SIJI_NO                                                        " & vbNewLine _
    & " LEFT JOIN $LM_TRN$..E_SAGYO SAGYO                                                                             " & vbNewLine _
    & " ON  SAGYO.NRS_BR_CD    = MIN_SAGYO.NRS_BR_CD                                                                  " & vbNewLine _
    & " AND SAGYO.SAGYO_REC_NO = MIN_SAGYO.MIN_SAGYO_REC_NO                                                           " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_CUST CUST                                                                               " & vbNewLine _
    & " ON  SAGYO.NRS_BR_CD = CUST.NRS_BR_CD                                                                          " & vbNewLine _
    & " AND SAGYO.CUST_CD_L = CUST.CUST_CD_L                                                                          " & vbNewLine _
    & " AND SAGYO.CUST_CD_M = CUST.CUST_CD_M                                                                          " & vbNewLine _
    & " AND CUST_CD_S = '00'                                                                                          " & vbNewLine _
    & " AND CUST_CD_SS = '00'                                                                                         " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SOKO SOKO                                                                               " & vbNewLine _
    & " ON  SOKO.NRS_BR_CD = SAGYO.NRS_BR_CD                                                                          " & vbNewLine _
    & " AND SOKO.WH_CD     = SAGYO_SIJI.WH_CD                                                                         " & vbNewLine _
    & " LEFT JOIN $LM_MST$..M_SAGYO MSAGYO                                                                            " & vbNewLine _
    & " ON  MSAGYO.NRS_BR_CD = SAGYO.NRS_BR_CD                                                                        " & vbNewLine _
    & " AND MSAGYO.SAGYO_CD  = SAGYO.SAGYO_CD                                                                         " & vbNewLine _
    & " WHERE SAGYO_SIJI.SAGYO_SIJI_NO = @SAGYO_SIJI_NO                                                               " & vbNewLine _
    & " AND   MSAGYO.WH_SAGYO_YN       = '01'                                                                         " & vbNewLine


#End Region

#Region "作業登録"
    ''' <summary>
    ''' 作業登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TD_SAGYO As String _
    = " INSERT INTO $LM_TRN$..TD_SAGYO (                                                                     " & vbNewLine _
    & "  NRS_BR_CD                                                                                           " & vbNewLine _
    & " ,SAGYO_REC_NO                                                                                        " & vbNewLine _
    & " ,WORK_SEQ                                                                                            " & vbNewLine _
    & " ,SAGYO_SIJI_NO                                                                                       " & vbNewLine _
    & " ,SAGYO_STATE_KB                                                                                      " & vbNewLine _
    & " ,WH_CD                                                                                               " & vbNewLine _
    & " ,GOODS_CD_NRS                                                                                        " & vbNewLine _
    & " ,GOODS_NM_NRS                                                                                        " & vbNewLine _
    & " ,IRIME                                                                                               " & vbNewLine _
    & " ,IRIME_UT                                                                                            " & vbNewLine _
    & " ,PKG_NB                                                                                              " & vbNewLine _
    & " ,PKG_UT                                                                                              " & vbNewLine _
    & " ,LOT_NO                                                                                              " & vbNewLine _
    & " ,TOU_NO                                                                                              " & vbNewLine _
    & " ,SITU_NO                                                                                             " & vbNewLine _
    & " ,ZONE_CD                                                                                             " & vbNewLine _
    & " ,LOCA                                                                                                " & vbNewLine _
    & " ,SAGYO_CD                                                                                            " & vbNewLine _
    & " ,SAGYO_NM                                                                                            " & vbNewLine _
    & " ,INV_TANI                                                                                            " & vbNewLine _
    & " ,KOSU_BAI                                                                                            " & vbNewLine _
    & " ,SAGYO_NB                                                                                            " & vbNewLine _
    & " ,REMARK                                                                                              " & vbNewLine _
    & " ,JISYATASYA_KB                                                                                       " & vbNewLine _
    & " ,IOZS_KB                                                                                             " & vbNewLine _
    & " ,SYS_ENT_DATE                                                                                        " & vbNewLine _
    & " ,SYS_ENT_TIME                                                                                        " & vbNewLine _
    & " ,SYS_ENT_PGID                                                                                        " & vbNewLine _
    & " ,SYS_ENT_USER                                                                                        " & vbNewLine _
    & " ,SYS_UPD_DATE                                                                                        " & vbNewLine _
    & " ,SYS_UPD_TIME                                                                                        " & vbNewLine _
    & " ,SYS_UPD_PGID                                                                                        " & vbNewLine _
    & " ,SYS_UPD_USER                                                                                        " & vbNewLine _
    & " ,SYS_DEL_FLG                                                                                         " & vbNewLine _
    & " )VALUES(                                                                                             " & vbNewLine _
    & "  @NRS_BR_CD                                                                                          " & vbNewLine _
    & " ,@SAGYO_REC_NO                                                                                       " & vbNewLine _
    & " ,@WORK_SEQ                                                                                           " & vbNewLine _
    & " ,@SAGYO_SIJI_NO                                                                                      " & vbNewLine _
    & " ,@SAGYO_STATE_KB                                                                                     " & vbNewLine _
    & " ,@WH_CD                                                                                              " & vbNewLine _
    & " ,@GOODS_CD_NRS                                                                                       " & vbNewLine _
    & " ,@GOODS_NM_NRS                                                                                       " & vbNewLine _
    & " ,@IRIME                                                                                              " & vbNewLine _
    & " ,@IRIME_UT                                                                                           " & vbNewLine _
    & " ,@PKG_NB                                                                                             " & vbNewLine _
    & " ,@PKG_UT                                                                                             " & vbNewLine _
    & " ,@LOT_NO                                                                                             " & vbNewLine _
    & " ,@TOU_NO                                                                                             " & vbNewLine _
    & " ,@SITU_NO                                                                                            " & vbNewLine _
    & " ,@ZONE_CD                                                                                            " & vbNewLine _
    & " ,@LOCA                                                                                               " & vbNewLine _
    & " ,@SAGYO_CD                                                                                           " & vbNewLine _
    & " ,@SAGYO_NM                                                                                           " & vbNewLine _
    & " ,@INV_TANI                                                                                           " & vbNewLine _
    & " ,@KOSU_BAI                                                                                           " & vbNewLine _
    & " ,@SAGYO_NB                                                                                           " & vbNewLine _
    & " ,@REMARK                                                                                             " & vbNewLine _
    & " ,@JISYATASYA_KB                                                                                      " & vbNewLine _
    & " ,@IOZS_KB                                                                                            " & vbNewLine _
    & " ,@SYS_ENT_DATE                                                                                       " & vbNewLine _
    & " ,@SYS_ENT_TIME                                                                                       " & vbNewLine _
    & " ,@SYS_ENT_PGID                                                                                       " & vbNewLine _
    & " ,@SYS_ENT_USER                                                                                       " & vbNewLine _
    & " ,@SYS_UPD_DATE                                                                                       " & vbNewLine _
    & " ,@SYS_UPD_TIME                                                                                       " & vbNewLine _
    & " ,@SYS_UPD_PGID                                                                                       " & vbNewLine _
    & " ,@SYS_UPD_USER                                                                                       " & vbNewLine _
    & " ,@SYS_DEL_FLG                                                                                        " & vbNewLine _
    & " )                                                                                                    " & vbNewLine

#End Region

#End Region

#Region "Update"

#Region "作業指示 キャンセル"
    Public Const SQL_UPDATE_TD_SAGYO_SIJI_CANCEL As String _
    = "UPDATE $LM_TRN$..TD_SAGYO_SIJI             " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " CANCEL_FLG   = '01'                       " & vbNewLine _
    & ",CANCEL_TYPE  = @CANCEL_TYPE               " & vbNewLine _
    & ",SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
    & ",SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
    & ",SYS_UPD_PGID = @SYS_UPD_PGID              " & vbNewLine _
    & ",SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD     = @NRS_BR_CD             " & vbNewLine _
    & "AND SAGYO_SIJI_NO = @SAGYO_SIJI_NO         " & vbNewLine _
    & "AND WORK_SEQ      = @WORK_SEQ              " & vbNewLine

#End Region

#Region "作業指示 削除"
    Public Const SQL_UPDATE_TD_SAGYO_SIJI_DEL As String _
    = "UPDATE $LM_TRN$..TD_SAGYO_SIJI             " & vbNewLine _
    & "SET                                        " & vbNewLine _
    & " WORK_STATE_KB = '99'                      " & vbNewLine _
    & ",SYS_UPD_DATE  = @SYS_UPD_DATE             " & vbNewLine _
    & ",SYS_UPD_TIME  = @SYS_UPD_TIME             " & vbNewLine _
    & ",SYS_UPD_PGID  = @SYS_UPD_PGID             " & vbNewLine _
    & ",SYS_UPD_USER  = @SYS_UPD_USER             " & vbNewLine _
    & "WHERE                                      " & vbNewLine _
    & "    NRS_BR_CD     = @NRS_BR_CD             " & vbNewLine _
    & "AND SAGYO_SIJI_NO = @SAGYO_SIJI_NO         " & vbNewLine _
    & "AND WORK_SEQ      = @WORK_SEQ              " & vbNewLine

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
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "取得"

#Region "作業指示取得"

    ''' <summary>
    ''' 作業指示取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoSiji(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionInsertAll()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LME800DAC.SQL_SELECT_TD_SAGYO_SIJI, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WORK_SEQ", "WORK_SEQ")
        map.Add("WORK_STATE_KB", "WORK_STATE_KB")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("CANCEL_TYPE", "CANCEL_TYPE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("REMARK_1", "REMARK_1")
        map.Add("REMARK_2", "REMARK_2")
        map.Add("REMARK_3", "REMARK_3")
        map.Add("REMARK_CHK_FLG", "REMARK_CHK_FLG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI))

        Return ds

    End Function

#End Region

#Region "作業登録データ取得"

    ''' <summary>
    ''' 作業登録データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionInsertAll()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LME800DAC.SQL_SELECT_SAGYO_DATA, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()


        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("WORK_SEQ", "WORK_SEQ")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("SAGYO_STATE_KB", "SAGYO_STATE_KB")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("KOSU_BAI", "KOSU_BAI")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("REMARK", "REMARK")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("USER_NM", "USER_NM")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LME800DAC.TABLE_NM.LME800IN_SAGYO))

        Return ds

    End Function

#End Region

#End Region

#Region "登録"

#Region "作業指示登録"
    ''' <summary>
    ''' 作業指示登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoSiji(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800IN).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionInsertAll()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LME800DAC.SQL_INSERT_TD_SAGYO_SIJI, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        MyBase.SetResultCount(rtn)

        Return ds

    End Function

#End Region

#Region "作業登録"
    ''' <summary>
    ''' 作業登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyo(ByVal ds As DataSet) As DataSet

        For Each dr As DataRow In ds.Tables(LME800DAC.TABLE_NM.LME800IN_SAGYO).Rows

            'DataSetのIN情報を取得
            Me._Row = dr

            'パラメータ設定
            Me._SqlPrmList = New ArrayList()
            Call Me.SetSelectConditionInsertSagyo()
            Call Me.SetParamCommonSystemUp()

            'スキーマ名設定
            Dim sql As String = Me.SetSchemaNm(LME800DAC.SQL_INSERT_TD_SAGYO, Me._Row.Item("NRS_BR_CD").ToString())

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        Next

        Return ds

    End Function

#End Region

#End Region

#Region "更新"

#Region "作業指示キャンセル"
    ''' <summary>
    ''' 作業指示キャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyoSijiCancel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdate()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LME800DAC.SQL_UPDATE_TD_SAGYO_SIJI_CANCEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "作業指示削除"
    ''' <summary>
    ''' 作業指示キャンセル
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyoSijiDelete(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectConditionUpdate()
        Call Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LME800DAC.SQL_UPDATE_TD_SAGYO_SIJI_DEL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "現場作業指示ステータス更新"
    ''' <summary>
    ''' 現場作業指示ステータス更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateWHSagyoShijiStatus(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("UPDATE $LM_TRN$..E_SAGYO_SIJI        " & vbNewLine)
        Me._StrSql.Append("SET                                  " & vbNewLine)
        Me._StrSql.Append("     WH_TAB_STATUS = @WH_TAB_STATUS  " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_DATE  = @SYS_UPD_DATE   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_TIME  = @SYS_UPD_TIME   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_PGID  = @SYS_UPD_PGID   " & vbNewLine)
        Me._StrSql.Append("    ,SYS_UPD_USER  = @SYS_UPD_USER   " & vbNewLine)
        Me._StrSql.Append("WHERE                                " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD     = @NRS_BR_CD       " & vbNewLine)
        Me._StrSql.Append("AND SAGYO_SIJI_NO = @SAGYO_SIJI_NO   " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me._Row("SAGYO_SIJI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", Me._Row("WH_TAB_STATUS_KB").ToString(), DBDataType.CHAR))
        Me.SetParamCommonSystemUp()

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        MyBase.SetResultCount(rtn)

        Return ds

    End Function
#End Region

#End Region

#Region "営業所チェック"
    ''' <summary>
    ''' 営業所チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckTabletUse(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT count(*) AS CNT FROM $LM_MST$..Z_KBN  " & vbNewLine)
        Me._StrSql.Append("WHERE                          " & vbNewLine)
        Me._StrSql.Append("    KBN_GROUP_CD = 'B007'      " & vbNewLine)
        Me._StrSql.Append("AND VALUE1 = 1.000             " & vbNewLine)
        Me._StrSql.Append("AND KBN_CD = @NRS_BR_CD        " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("CNT")))
        reader.Close()

        Return ds

    End Function
#End Region

#Region "排他チェック"
    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables(LME800DAC.TABLE_NM.LME800IN).Rows(0)

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append("SELECT count(*) AS CNT FROM $LM_TRN$..E_SAGYO_SIJI  " & vbNewLine)
        Me._StrSql.Append("WHERE                                               " & vbNewLine)
        Me._StrSql.Append("    NRS_BR_CD       = @NRS_BR_CD                    " & vbNewLine)
        Me._StrSql.Append("AND SAGYO_SIJI_NO   = @SAGYO_SIJI_NO                " & vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_DATE    = @SYS_UPD_DATE                 " & vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME    = @SYS_UPD_TIME                 " & vbNewLine)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me._Row("SAGYO_SIJI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog([GetType].Name, MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        map.Add("CNT", "CNT")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, LME800DAC.TABLE_NM.LME800CNT))

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

    ''' <summary>
    '''  パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectConditionInsertAll()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me._Row("SAGYO_SIJI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_TYPE", Me._Row("PROC_TYPE").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    '''  パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectConditionInsertSagyo()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", Me._Row("SAGYO_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WORK_SEQ", Me._Row("WORK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me._Row("SAGYO_SIJI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_STATE_KB", Me._Row("SAGYO_STATE_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", Me._Row("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me._Row("IRIME").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me._Row("IRIME_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me._Row("PKG_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me._Row("PKG_UT").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me._Row("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me._Row("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me._Row("ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me._Row("LOCA").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", Me._Row("SAGYO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", Me._Row("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", Me._Row("INV_TANI").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_BAI", Me._Row("KOSU_BAI").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", Me._Row("SAGYO_NB").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me._Row("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISYATASYA_KB", Me._Row("JISYATASYA_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", Me._Row("IOZS_KB").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

    End Sub

    ''' <summary>
    '''  パラメータ設定 作業指示更新用
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub SetSelectConditionUpdate()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", Me._Row("SAGYO_SIJI_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WORK_SEQ", Me._Row("WORK_SEQ").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_TYPE", Me._Row("CANCEL_TYPE").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetNowDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetNowTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 現在日付取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNowDate() As String
        Return String.Concat(Right("0000" & Now.Year.ToString(), 4), Right("00" & Now.Month.ToString(), 2), Right("00" & Now.Day.ToString(), 2))
    End Function

    ''' <summary>
    ''' 現在時刻取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetNowTime() As String
        Return String.Concat(Right("00" & Now.Hour.ToString(), 2), Right("00" & Now.Minute.ToString(), 2), Right("00" & Now.Second.ToString(), 2), Right("000" & Now.Millisecond.ToString(), 3))
    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region


#End Region

End Class
