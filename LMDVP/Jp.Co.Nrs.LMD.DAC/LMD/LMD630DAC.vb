' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD630    : 在庫受払表(月間入出庫重量集計表込み)
'  作  成  者       :  SBS菊池
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD630DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD630DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _rptRow As Data.DataRow

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

    ''' <summary>
    ''' 'SQL条件文
    ''' </summary>
    ''' <remarks></remarks>
    Private _NRS_BR_CD As String = String.Empty
    Private _CUST_CD_L As String = String.Empty
    Private _CUST_CD_M As String = String.Empty
    Private _PRINT_FROM As String = String.Empty
    Private _PRINT_TO As String = String.Empty
    Private _SYS_ENT_DATE As String = String.Empty
    Private _SYS_ENT_TIME As String = String.Empty
    Private _SYS_ENT_PGID As String = String.Empty
    Private _SYS_ENT_USER As String = String.Empty
    Private _SYS_UPD_DATE As String = String.Empty
    Private _SYS_UPD_TIME As String = String.Empty
    Private _SYS_UPD_PGID As String = String.Empty
    Private _SYS_UPD_USER As String = String.Empty
    Private _SYS_DEL_FLG As String = String.Empty
    Private _MONTH_AGO As String = String.Empty

#End Region

#Region "Const"

#Region "追加処理 SQL"


#Region "SQL_Insert_D_WK_INOUT1_FROM_INKA"

    Private Function SQL_Insert_D_WK_INOUT1_FROM_INKA() As String

        Dim sql As String = "" & vbNewLine _
                & "                INSERT INTO $LM_TRN$..D_WK_INOUT1                             " & vbNewLine _
                & "                (NRS_BR_CD                                                   " & vbNewLine _
                & "                ,REC_CTL_NO                                                  " & vbNewLine _
                & "                ,DATA_KBN                                                    " & vbNewLine _
                & "                ,BASE_DATE                                                   " & vbNewLine _
                & "                ,ZAI_REC_NO                                                  " & vbNewLine _
                & "                ,WH_CD                                                       " & vbNewLine _
                & "                ,TOU_NO                                                      " & vbNewLine _
                & "                ,SITU_NO                                                     " & vbNewLine _
                & "                ,GOODS_CD_NRS                                                " & vbNewLine _
                & "                ,CUST_CD_L                                                   " & vbNewLine _
                & "                ,CUST_CD_M                                                   " & vbNewLine _
                & "                ,ZEN_KURI_NB                                                 " & vbNewLine _
                & "                ,TOU_IN_NB                                                   " & vbNewLine _
                & "                ,TOU_OUT_NB                                                  " & vbNewLine _
                & "                ,TOU_KURI_NB                                                 " & vbNewLine _
                & "                ,DEL_KEY_DATE                                                " & vbNewLine _
                & "                ,SYS_ENT_DATE                                                " & vbNewLine _
                & "                ,SYS_ENT_TIME                                                " & vbNewLine _
                & "                ,SYS_ENT_PGID                                                " & vbNewLine _
                & "                ,SYS_ENT_USER                                                " & vbNewLine _
                & "                ,SYS_UPD_DATE                                                " & vbNewLine _
                & "                ,SYS_UPD_TIME                                                " & vbNewLine _
                & "                ,SYS_UPD_PGID                                                " & vbNewLine _
                & "                ,SYS_UPD_USER                                                " & vbNewLine _
                & "                ,SYS_DEL_FLG)                                                " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "     ----------入荷                                                          " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "     SELECT                                                                  " & vbNewLine _
                & "      NRS_BR_CD                                                              " & vbNewLine _
                & "     ,REC_CTL_NO                                                             " & vbNewLine _
                & "     ,'00' AS DATA_KBN                                                       " & vbNewLine _
                & "     ,BASE_DATE                                                              " & vbNewLine _
                & "     ,ZAI_REC_NO                                                             " & vbNewLine _
                & "     ,WH_CD                                                                  " & vbNewLine _
                & "     ,TOU_NO                                                                 " & vbNewLine _
                & "     ,SITU_NO                                                                " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                           " & vbNewLine _
                & "     ,CUST_CD_L                                                              " & vbNewLine _
                & "     ,CUST_CD_M                                                              " & vbNewLine _
                & "     ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                                        " & vbNewLine _
                & "     ,SUM(TOU_IN_NB)   AS TOU_IN_NB                                          " & vbNewLine _
                & "     ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                                         " & vbNewLine _
                & "     ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                                        " & vbNewLine _
                & "     ,INKA_DATE        AS DEL_KEY_DATE                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                               " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                               " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                               " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                               " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                               " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                               " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                               " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                               " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "     FROM                                                                    " & vbNewLine _
                & "     (                                                                       " & vbNewLine _
                & "      SELECT                                                                 " & vbNewLine _
                & "      ZAI.NRS_BR_CD                                                          " & vbNewLine _
                & "     ,ZAI.REC_CTL_NO                                                         " & vbNewLine _
                & "     ,ZAI.ZAI_REC_NO                                                         " & vbNewLine _
                & "     ,ZAI.WH_CD                                                              " & vbNewLine _
                & "     ,ZAI.CUST_CD_L                                                          " & vbNewLine _
                & "     ,ZAI.CUST_CD_M                                                          " & vbNewLine _
                & "     ,ZAI.TOU_NO                                                             " & vbNewLine _
                & "     ,ZAI.SITU_NO                                                            " & vbNewLine _
                & "     ,ZAI.GOODS_CD_NRS                                                       " & vbNewLine _
                & "     ,ZAI.ZEN_KURI_NB                                                        " & vbNewLine _
                & "     ,ZAI.TOU_IN_NB                                                          " & vbNewLine _
                & "     ,ZAI.TOU_OUT_NB                                                         " & vbNewLine _
                & "     ,ZAI.TOU_KURI_NB                                                        " & vbNewLine _
                & "     ,ZAI.BASE_DATE AS BASE_DATE                                             " & vbNewLine _
                & "     ,ZAI.INKA_DATE AS INKA_DATE                                             " & vbNewLine _
                & "      FROM                                                                   " & vbNewLine _
                & "      (                                                                      " & vbNewLine _
                & "       SELECT                                                                " & vbNewLine _
                & "        INKAL.NRS_BR_CD AS NRS_BR_CD                                         " & vbNewLine _
                & "       ,INKAS.INKA_NO_L + INKAS.INKA_NO_M + INKAS.INKA_NO_S AS REC_CTL_NO    " & vbNewLine _
                & "       ,INKAS.ZAI_REC_NO AS ZAI_REC_NO                                       " & vbNewLine _
                & "       ,INKAL.WH_CD AS WH_CD                                                 " & vbNewLine _
                & "       ,INKAL.CUST_CD_L AS CUST_CD_L                                         " & vbNewLine _
                & "       ,INKAL.CUST_CD_M AS CUST_CD_M                                         " & vbNewLine _
                & "       ,INKAS.TOU_NO AS TOU_NO                                               " & vbNewLine _
                & "       ,INKAS.SITU_NO AS SITU_NO                                             " & vbNewLine _
                & "       ,INKAM.GOODS_CD_NRS AS GOODS_CD_NRS                                   " & vbNewLine _
                & "       ,(INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU AS ZEN_KURI_NB             " & vbNewLine _
                & "       ,0 AS TOU_IN_NB                                                     " & vbNewLine _
                & "       ,0 AS TOU_OUT_NB                                                    " & vbNewLine _
                & "       ,(INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU AS TOU_KURI_NB             " & vbNewLine _
                & "             ,INKAL.INKA_DATE AS BASE_DATE                                   " & vbNewLine _
                & "       ,INKAL.INKA_DATE AS INKA_DATE                                         " & vbNewLine _
                & "       FROM $LM_TRN$..B_INKA_L INKAL                                         " & vbNewLine _
                & "       INNER JOIN $LM_TRN$..B_INKA_M INKAM ON                                " & vbNewLine _
                & "       INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                     " & vbNewLine _
                & "       AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                 " & vbNewLine _
                & "       AND INKAM.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "       INNER JOIN $LM_TRN$..B_INKA_S INKAS ON                                " & vbNewLine _
                & "       INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                     " & vbNewLine _
                & "       AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                 " & vbNewLine _
                & "       AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                 " & vbNewLine _
                & "       AND INKAS.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "       INNER JOIN $LM_MST$..M_GOODS GOODS ON                                 " & vbNewLine _
                & "       GOODS.NRS_BR_CD = INKAL.NRS_BR_CD                                     " & vbNewLine _
                & "       AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                           " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "       WHERE                                                                 " & vbNewLine _
                & "           INKAL.SYS_DEL_FLG = '0'                                           " & vbNewLine _
                & "       AND INKAL.INKA_STATE_KB >= '50'                                       " & vbNewLine _
                & "       AND INKAL.INKA_DATE >= '" & _MONTH_AGO & "'                           " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "      ) ZAI                                                                  " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "     ) BASEINKA                                                              " & vbNewLine _
                & "                                                                             " & vbNewLine _
                & "     GROUP BY                                                                " & vbNewLine _
                & "      NRS_BR_CD                                                              " & vbNewLine _
                & "     ,REC_CTL_NO                                                             " & vbNewLine _
                & "     ,ZAI_REC_NO                                                             " & vbNewLine _
                & "     ,WH_CD                                                                  " & vbNewLine _
                & "     ,TOU_NO                                                                 " & vbNewLine _
                & "     ,SITU_NO                                                                " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                           " & vbNewLine _
                & "     ,CUST_CD_L                                                              " & vbNewLine _
                & "     ,CUST_CD_M                                                              " & vbNewLine _
                & "     ---,TOU_IN_NB                                                              " & vbNewLine _
                & "     ---,TOU_OUT_NB                                                             " & vbNewLine _
                & "     ---,ZEN_KURI_NB                                                            " & vbNewLine _
                & "     ---,TOU_KURI_NB                                                            " & vbNewLine _
                & "     ,BASE_DATE                                                              " & vbNewLine _
                & "     ,INKA_DATE                                                              " & vbNewLine

        Return sql

    End Function

#End Region

#Region "SQL_Insert_D_WK_INOUT1_FROM_OUTKA"
    Private Function SQL_Insert_D_WK_INOUT1_FROM_OUTKA() As String
        Dim sql As String = "" & vbNewLine _
                & "     SET ARITHABORT ON                                                            " & vbNewLine _
                & "     SET ARITHIGNORE ON                                                           " & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT1                                            " & vbNewLine _
                & "                (NRS_BR_CD                                                        " & vbNewLine _
                & "                ,REC_CTL_NO                                                       " & vbNewLine _
                & "                ,DATA_KBN                                                         " & vbNewLine _
                & "                ,BASE_DATE                                                        " & vbNewLine _
                & "                ,ZAI_REC_NO                                                       " & vbNewLine _
                & "                ,WH_CD                                                            " & vbNewLine _
                & "                ,TOU_NO                                                           " & vbNewLine _
                & "                ,SITU_NO                                                          " & vbNewLine _
                & "                ,GOODS_CD_NRS                                                     " & vbNewLine _
                & "                ,CUST_CD_L                                                        " & vbNewLine _
                & "                ,CUST_CD_M                                                        " & vbNewLine _
                & "                ,ZEN_KURI_NB                                                      " & vbNewLine _
                & "                ,TOU_IN_NB                                                        " & vbNewLine _
                & "                ,TOU_OUT_NB                                                       " & vbNewLine _
                & "                ,TOU_KURI_NB                                                      " & vbNewLine _
                & "                ,DEL_KEY_DATE                                                     " & vbNewLine _
                & "                ,SYS_ENT_DATE                                                     " & vbNewLine _
                & "                ,SYS_ENT_TIME                                                     " & vbNewLine _
                & "                ,SYS_ENT_PGID                                                     " & vbNewLine _
                & "                ,SYS_ENT_USER                                                     " & vbNewLine _
                & "                ,SYS_UPD_DATE                                                     " & vbNewLine _
                & "                ,SYS_UPD_TIME                                                     " & vbNewLine _
                & "                ,SYS_UPD_PGID                                                     " & vbNewLine _
                & "                ,SYS_UPD_USER                                                     " & vbNewLine _
                & "                ,SYS_DEL_FLG)                                                     " & vbNewLine _
                & "                                                                                  " & vbNewLine _
                & "                                                                                  " & vbNewLine _
                & "     ---出荷---------------                                                       " & vbNewLine _
                & "     SELECT                                                                       " & vbNewLine _
                & "      NRS_BR_CD                                                                   " & vbNewLine _
                & "     ,REC_CTL_NO                                                                  " & vbNewLine _
                & "     ,'01' AS DATA_KBN                                                            " & vbNewLine _
                & "     ,BASE_DATE                                                                   " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                  " & vbNewLine _
                & "     ,WH_CD                                                                       " & vbNewLine _
                & "     ,TOU_NO                                                                      " & vbNewLine _
                & "     ,SITU_NO                                                                     " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                " & vbNewLine _
                & "     ,CUST_CD_L                                                                   " & vbNewLine _
                & "     ,CUST_CD_M                                                                   " & vbNewLine _
                & "     ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                                             " & vbNewLine _
                & "     ,SUM(TOU_IN_NB)   AS TOU_IN_NB                                               " & vbNewLine _
                & "     ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                                              " & vbNewLine _
                & "     ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                                             " & vbNewLine _
                & "     ,OUTKA_PLAN_DATE  AS DEL_KEY_DATE                                            " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                    " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                     " & vbNewLine _
                & "     FROM                                                                         " & vbNewLine _
                & "     (                                                                            " & vbNewLine _
                & "      SELECT                                                                      " & vbNewLine _
                & "      ZAI.NRS_BR_CD                                                               " & vbNewLine _
                & "     ,ZAI.REC_CTL_NO                                                              " & vbNewLine _
                & "     ,ZAI.ZAI_REC_NO                                                              " & vbNewLine _
                & "     ,ZAI.WH_CD                                                                   " & vbNewLine _
                & "     ,ZAI.CUST_CD_L                                                               " & vbNewLine _
                & "     ,ZAI.CUST_CD_M                                                               " & vbNewLine _
                & "     ,ZAI.TOU_NO                                                                  " & vbNewLine _
                & "     ,ZAI.SITU_NO                                                                 " & vbNewLine _
                & "     ,ZAI.GOODS_CD_NRS                                                            " & vbNewLine _
                & "     ,ZAI.ZEN_KURI_NB                                                             " & vbNewLine _
                & "     ,ZAI.TOU_IN_NB                                                               " & vbNewLine _
                & "     ,ZAI.TOU_OUT_NB                                                              " & vbNewLine _
                & "     ,ZAI.TOU_KURI_NB                                                             " & vbNewLine _
                & "     ,ZAI.BASE_DATE       AS BASE_DATE                                            " & vbNewLine _
                & "     ,ZAI.OUTKA_PLAN_DATE AS OUTKA_PLAN_DATE                                      " & vbNewLine _
                & "      FROM                                                                        " & vbNewLine _
                & "      (                                                                           " & vbNewLine _
                & "       SELECT                                                                     " & vbNewLine _
                & "        OUTKAL.NRS_BR_CD AS NRS_BR_CD                                             " & vbNewLine _
                & "       ,OUTKAS.OUTKA_NO_L + OUTKAS.OUTKA_NO_M + OUTKAS.OUTKA_NO_S AS REC_CTL_NO   " & vbNewLine _
                & "       ,OUTKAS.ZAI_REC_NO AS ZAI_REC_NO                                           " & vbNewLine _
                & "       ,OUTKAL.WH_CD AS WH_CD                                                     " & vbNewLine _
                & "       ,OUTKAL.CUST_CD_L AS CUST_CD_L                                             " & vbNewLine _
                & "       ,OUTKAL.CUST_CD_M AS CUST_CD_M                                             " & vbNewLine _
                & "       ,OUTKAS.TOU_NO AS TOU_NO                                                   " & vbNewLine _
                & "       ,OUTKAS.SITU_NO AS SITU_NO                                                 " & vbNewLine _
                & "       ,OUTKAM.GOODS_CD_NRS AS GOODS_CD_NRS                                       " & vbNewLine _
                & "       ,OUTKAS.ALCTD_NB * -1 AS ZEN_KURI_NB                                       " & vbNewLine _
                & "       ,0 AS TOU_IN_NB                                                          " & vbNewLine _
                & "       ,0 AS TOU_OUT_NB                                                         " & vbNewLine _
                & "       ,OUTKAS.ALCTD_NB * -1 AS TOU_KURI_NB                                       " & vbNewLine _
                & "             ,OUTKAL.OUTKA_PLAN_DATE AS BASE_DATE                                 " & vbNewLine _
                & "       ,OUTKAL.OUTKA_PLAN_DATE                                                    " & vbNewLine _
                & "       FROM $LM_TRN$..C_OUTKA_L OUTKAL                                            " & vbNewLine _
                & "       INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                   " & vbNewLine _
                & "       OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                        " & vbNewLine _
                & "       AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                  " & vbNewLine _
                & "       AND OUTKAM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                & "                                                                                  " & vbNewLine _
                & "       INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS ON                                   " & vbNewLine _
                & "       OUTKAS.NRS_BR_CD = OUTKAL.NRS_BR_CD                                        " & vbNewLine _
                & "       AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                  " & vbNewLine _
                & "       AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                  " & vbNewLine _
                & "       AND OUTKAS.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                & "       WHERE                                                                      " & vbNewLine _
                & "        OUTKAL.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                & "       AND OUTKAL.OUTKA_STATE_KB >= '60'                                          " & vbNewLine _
                & "       AND OUTKAL.OUTKA_PLAN_DATE >= '" & _MONTH_AGO & "'                         " & vbNewLine _
                & "                                                                                  " & vbNewLine _
                & "      ) ZAI                                                                       " & vbNewLine _
                & "                                                                                  " & vbNewLine _
                & "     ) BASEOUTKA                                                                  " & vbNewLine _
                & "     GROUP BY                                                                     " & vbNewLine _
                & "      NRS_BR_CD                                                                   " & vbNewLine _
                & "     ,REC_CTL_NO                                                                  " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                  " & vbNewLine _
                & "     ,WH_CD                                                                       " & vbNewLine _
                & "     ,TOU_NO                                                                      " & vbNewLine _
                & "     ,SITU_NO                                                                     " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                " & vbNewLine _
                & "     ,CUST_CD_L                                                                   " & vbNewLine _
                & "     ,CUST_CD_M                                                                   " & vbNewLine _
                & "     ---,TOU_IN_NB                                                                   " & vbNewLine _
                & "     ---,TOU_OUT_NB                                                                  " & vbNewLine _
                & "     ---,ZEN_KURI_NB                                                                 " & vbNewLine _
                & "     ---,TOU_KURI_NB                                                                 " & vbNewLine _
                & "     ,BASE_DATE                                                                   " & vbNewLine _
                & "     ,OUTKA_PLAN_DATE                                                             " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT1_FROM_IDO_BEFORE"
    Private Function SQL_Insert_D_WK_INOUT1_FROM_IDO_BEFORE() As String
        Dim sql As String = "" & vbNewLine _
                    & "     INSERT INTO $LM_TRN$..D_WK_INOUT1                 " & vbNewLine _
                    & "                (NRS_BR_CD                               " & vbNewLine _
                    & "                ,REC_CTL_NO                              " & vbNewLine _
                    & "                ,DATA_KBN                                " & vbNewLine _
                    & "                ,BASE_DATE                               " & vbNewLine _
                    & "                ,ZAI_REC_NO                              " & vbNewLine _
                    & "                ,WH_CD                                   " & vbNewLine _
                    & "                ,TOU_NO                                  " & vbNewLine _
                    & "                ,SITU_NO                                 " & vbNewLine _
                    & "                ,GOODS_CD_NRS                            " & vbNewLine _
                    & "                ,CUST_CD_L                               " & vbNewLine _
                    & "                ,CUST_CD_M                               " & vbNewLine _
                    & "                ,ZEN_KURI_NB                             " & vbNewLine _
                    & "                ,TOU_IN_NB                               " & vbNewLine _
                    & "                ,TOU_OUT_NB                              " & vbNewLine _
                    & "                ,TOU_KURI_NB                             " & vbNewLine _
                    & "                ,DEL_KEY_DATE                            " & vbNewLine _
                    & "                ,SYS_ENT_DATE                            " & vbNewLine _
                    & "                ,SYS_ENT_TIME                            " & vbNewLine _
                    & "                ,SYS_ENT_PGID                            " & vbNewLine _
                    & "                ,SYS_ENT_USER                            " & vbNewLine _
                    & "                ,SYS_UPD_DATE                            " & vbNewLine _
                    & "                ,SYS_UPD_TIME                            " & vbNewLine _
                    & "                ,SYS_UPD_PGID                            " & vbNewLine _
                    & "                ,SYS_UPD_USER                            " & vbNewLine _
                    & "                ,SYS_DEL_FLG)                            " & vbNewLine _
                    & "     -----移動前                                            " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "     SELECT                                              " & vbNewLine _
                    & "      NRS_BR_CD                                          " & vbNewLine _
                    & "     ,REC_CTL_NO                                         " & vbNewLine _
                    & "     ,'02' AS DATA_KBN                                   " & vbNewLine _
                    & "     ,BASE_DATE                                          " & vbNewLine _
                    & "     ,ZAI_REC_NO                                         " & vbNewLine _
                    & "     ,WH_CD                                              " & vbNewLine _
                    & "     ,TOU_NO                                             " & vbNewLine _
                    & "     ,SITU_NO                                            " & vbNewLine _
                    & "     ,GOODS_CD_NRS                                       " & vbNewLine _
                    & "     ,CUST_CD_L                                          " & vbNewLine _
                    & "     ,CUST_CD_M                                          " & vbNewLine _
                    & "     ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                    " & vbNewLine _
                    & "     ,SUM(TOU_IN_NB)   AS TOU_IN_NB                      " & vbNewLine _
                    & "     ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                     " & vbNewLine _
                    & "     ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                    " & vbNewLine _
                    & "     ,IDO_DATE         AS DEL_KEY_DATE                   " & vbNewLine _
                    & "     , '" & _SYS_ENT_DATE & "'                           " & vbNewLine _
                    & "     , '" & _SYS_ENT_TIME & "'                           " & vbNewLine _
                    & "     , '" & _SYS_ENT_PGID & "'                           " & vbNewLine _
                    & "     , '" & _SYS_ENT_USER & "'                           " & vbNewLine _
                    & "     , '" & _SYS_UPD_DATE & "'                           " & vbNewLine _
                    & "     , '" & _SYS_UPD_TIME & "'                           " & vbNewLine _
                    & "     , '" & _SYS_UPD_PGID & "'                           " & vbNewLine _
                    & "     , '" & _SYS_UPD_USER & "'                           " & vbNewLine _
                    & "     , '" & _SYS_DEL_FLG & "'                            " & vbNewLine _
                    & "     FROM                                                " & vbNewLine _
                    & "     (                                                   " & vbNewLine _
                    & "      SELECT                                             " & vbNewLine _
                    & "      ZAI.NRS_BR_CD                                      " & vbNewLine _
                    & "     ,ZAI.REC_CTL_NO                                     " & vbNewLine _
                    & "     ,ZAI.ZAI_REC_NO                                     " & vbNewLine _
                    & "     ,ZAI.WH_CD                                          " & vbNewLine _
                    & "     ,ZAI.CUST_CD_L                                      " & vbNewLine _
                    & "     ,ZAI.CUST_CD_M                                      " & vbNewLine _
                    & "     ,ZAI.TOU_NO                                         " & vbNewLine _
                    & "     ,ZAI.SITU_NO                                        " & vbNewLine _
                    & "     ,ZAI.GOODS_CD_NRS                                   " & vbNewLine _
                    & "     ,ZAI.ZEN_KURI_NB                                    " & vbNewLine _
                    & "     ,ZAI.TOU_IN_NB                                      " & vbNewLine _
                    & "     ,ZAI.TOU_OUT_NB                                     " & vbNewLine _
                    & "     ,ZAI.TOU_KURI_NB                                    " & vbNewLine _
                    & "     ,ZAI.BASE_DATE                                      " & vbNewLine _
                    & "     ,ZAI.IDO_DATE                                       " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "      FROM                                               " & vbNewLine _
                    & "      (                                                  " & vbNewLine _
                    & "       SELECT                                            " & vbNewLine _
                    & "        IDOTRS.NRS_BR_CD AS NRS_BR_CD                    " & vbNewLine _
                    & "       ,IDOTRS.REC_NO AS REC_CTL_NO                      " & vbNewLine _
                    & "       ,IDOTRS.O_ZAI_REC_NO AS ZAI_REC_NO                " & vbNewLine _
                    & "       ,ZAITRS.WH_CD AS WH_CD                            " & vbNewLine _
                    & "       ,ZAITRS.CUST_CD_L AS CUST_CD_L                    " & vbNewLine _
                    & "       ,ZAITRS.CUST_CD_M AS CUST_CD_M                    " & vbNewLine _
                    & "       ,ZAITRS.TOU_NO AS TOU_NO                          " & vbNewLine _
                    & "       ,ZAITRS.SITU_NO AS SITU_NO                        " & vbNewLine _
                    & "       ,ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS              " & vbNewLine _
                    & "       ,IDOTRS.N_PORA_ZAI_NB * -1 AS ZEN_KURI_NB         " & vbNewLine _
                    & "       ,0 AS TOU_IN_NB                                 " & vbNewLine _
                    & "       ,0 AS TOU_OUT_NB                                " & vbNewLine _
                    & "       ,IDOTRS.N_PORA_ZAI_NB * -1 AS TOU_KURI_NB         " & vbNewLine _
                    & "             ,IDOTRS.IDO_DATE  AS BASE_DATE              " & vbNewLine _
                    & "       ,IDOTRS.IDO_DATE                                  " & vbNewLine _
                    & "       FROM $LM_TRN$..D_IDO_TRS IDOTRS                   " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "       INNER JOIN $LM_TRN$..D_ZAI_TRS ZAITRS ON          " & vbNewLine _
                    & "       IDOTRS.O_ZAI_REC_NO = ZAITRS.ZAI_REC_NO           " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "       WHERE                                             " & vbNewLine _
                    & "           IDOTRS.SYS_DEL_FLG = '0'                      " & vbNewLine _
                    & "       AND IDOTRS.IDO_DATE >= '" & _MONTH_AGO & "'       " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "      ) ZAI                                              " & vbNewLine _
                    & "                                                         " & vbNewLine _
                    & "     ) BASEOLDIDO                                        " & vbNewLine _
                    & "     GROUP BY                                            " & vbNewLine _
                    & "      NRS_BR_CD                                          " & vbNewLine _
                    & "     ,REC_CTL_NO                                         " & vbNewLine _
                    & "     ,ZAI_REC_NO                                         " & vbNewLine _
                    & "     ,WH_CD                                              " & vbNewLine _
                    & "     ,TOU_NO                                             " & vbNewLine _
                    & "     ,SITU_NO                                            " & vbNewLine _
                    & "     ,GOODS_CD_NRS                                       " & vbNewLine _
                    & "     ,CUST_CD_L                                          " & vbNewLine _
                    & "     ,CUST_CD_M                                          " & vbNewLine _
                    & "     ---,TOU_IN_NB                                          " & vbNewLine _
                    & "     ---,TOU_OUT_NB                                         " & vbNewLine _
                    & "     ---,ZEN_KURI_NB                                        " & vbNewLine _
                    & "     ----,TOU_KURI_NB                                        " & vbNewLine _
                    & "     ,BASE_DATE                                          " & vbNewLine _
                    & "     ,IDO_DATE                                           " & vbNewLine

        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT1_FROM_IDO_AFTER"
    Private Function SQL_Insert_D_WK_INOUT1_FROM_IDO_AFTER() As String
        Dim sql As String = "" & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT1                    " & vbNewLine _
                & "                (NRS_BR_CD                               " & vbNewLine _
                & "                ,REC_CTL_NO                              " & vbNewLine _
                & "                ,DATA_KBN                                " & vbNewLine _
                & "                ,BASE_DATE                               " & vbNewLine _
                & "                ,ZAI_REC_NO                              " & vbNewLine _
                & "                ,WH_CD                                   " & vbNewLine _
                & "                ,TOU_NO                                  " & vbNewLine _
                & "                ,SITU_NO                                 " & vbNewLine _
                & "                ,GOODS_CD_NRS                            " & vbNewLine _
                & "                ,CUST_CD_L                               " & vbNewLine _
                & "                ,CUST_CD_M                               " & vbNewLine _
                & "                ,ZEN_KURI_NB                             " & vbNewLine _
                & "                ,TOU_IN_NB                               " & vbNewLine _
                & "                ,TOU_OUT_NB                              " & vbNewLine _
                & "                ,TOU_KURI_NB                             " & vbNewLine _
                & "                ,DEL_KEY_DATE                            " & vbNewLine _
                & "                ,SYS_ENT_DATE                            " & vbNewLine _
                & "                ,SYS_ENT_TIME                            " & vbNewLine _
                & "                ,SYS_ENT_PGID                            " & vbNewLine _
                & "                ,SYS_ENT_USER                            " & vbNewLine _
                & "                ,SYS_UPD_DATE                            " & vbNewLine _
                & "                ,SYS_UPD_TIME                            " & vbNewLine _
                & "                ,SYS_UPD_PGID                            " & vbNewLine _
                & "                ,SYS_UPD_USER                            " & vbNewLine _
                & "                ,SYS_DEL_FLG)                            " & vbNewLine _
                & "     -----移動後                                         " & vbNewLine _
                & "                                                         " & vbNewLine _
                & "                                                         " & vbNewLine _
                & "     SELECT                                              " & vbNewLine _
                & "      NRS_BR_CD                                          " & vbNewLine _
                & "     ,REC_CTL_NO                                         " & vbNewLine _
                & "     ,'03' AS DATA_KBN                                   " & vbNewLine _
                & "     ,BASE_DATE                                          " & vbNewLine _
                & "     ,ZAI_REC_NO                                         " & vbNewLine _
                & "     ,WH_CD                                              " & vbNewLine _
                & "     ,TOU_NO                                             " & vbNewLine _
                & "     ,SITU_NO                                            " & vbNewLine _
                & "     ,GOODS_CD_NRS                                       " & vbNewLine _
                & "     ,CUST_CD_L                                          " & vbNewLine _
                & "     ,CUST_CD_M                                          " & vbNewLine _
                & "     ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                    " & vbNewLine _
                & "     ,SUM(TOU_IN_NB)   AS TOU_IN_NB                      " & vbNewLine _
                & "     ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                     " & vbNewLine _
                & "     ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                    " & vbNewLine _
                & "     ,IDO_DATE         AS DEL_KEY_DATE                   " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                           " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                           " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                           " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                           " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                           " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                           " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                           " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                           " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                            " & vbNewLine _
                & "     FROM                                                " & vbNewLine _
                & "     (                                                   " & vbNewLine _
                & "      SELECT                                             " & vbNewLine _
                & "      ZAI.NRS_BR_CD                                      " & vbNewLine _
                & "     ,ZAI.REC_CTL_NO                                     " & vbNewLine _
                & "     ,ZAI.ZAI_REC_NO                                     " & vbNewLine _
                & "     ,ZAI.WH_CD                                          " & vbNewLine _
                & "     ,ZAI.CUST_CD_L                                      " & vbNewLine _
                & "     ,ZAI.CUST_CD_M                                      " & vbNewLine _
                & "     ,ZAI.TOU_NO                                         " & vbNewLine _
                & "     ,ZAI.SITU_NO                                        " & vbNewLine _
                & "     ,ZAI.GOODS_CD_NRS                                   " & vbNewLine _
                & "     ,ZAI.ZEN_KURI_NB                                    " & vbNewLine _
                & "     ,ZAI.TOU_IN_NB                                      " & vbNewLine _
                & "     ,ZAI.TOU_OUT_NB                                     " & vbNewLine _
                & "     ,ZAI.TOU_KURI_NB                                    " & vbNewLine _
                & "     ,ZAI.BASE_DATE                                      " & vbNewLine _
                & "     ,ZAI.IDO_DATE                                       " & vbNewLine _
                & "      FROM                                               " & vbNewLine _
                & "      (                                                  " & vbNewLine _
                & "       SELECT                                            " & vbNewLine _
                & "        IDOTRS.NRS_BR_CD AS NRS_BR_CD                    " & vbNewLine _
                & "       ,IDOTRS.REC_NO AS REC_CTL_NO                      " & vbNewLine _
                & "       ,IDOTRS.N_ZAI_REC_NO AS ZAI_REC_NO                " & vbNewLine _
                & "       ,ZAITRS.WH_CD AS WH_CD                            " & vbNewLine _
                & "       ,ZAITRS.CUST_CD_L AS CUST_CD_L                    " & vbNewLine _
                & "       ,ZAITRS.CUST_CD_M AS CUST_CD_M                    " & vbNewLine _
                & "       ,ZAITRS.TOU_NO AS TOU_NO                          " & vbNewLine _
                & "       ,ZAITRS.SITU_NO AS SITU_NO                        " & vbNewLine _
                & "       ,ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS              " & vbNewLine _
                & "       ,IDOTRS.N_PORA_ZAI_NB AS ZEN_KURI_NB              " & vbNewLine _
                & "       ,0 AS TOU_IN_NB                                 " & vbNewLine _
                & "       ,0 AS TOU_OUT_NB                                " & vbNewLine _
                & "       ,IDOTRS.N_PORA_ZAI_NB AS TOU_KURI_NB              " & vbNewLine _
                & "             ,IDOTRS.IDO_DATE  AS BASE_DATE              " & vbNewLine _
                & "       ,IDOTRS.IDO_DATE AS IDO_DATE                      " & vbNewLine _
                & "       FROM $LM_TRN$..D_IDO_TRS IDOTRS                   " & vbNewLine _
                & "                                                         " & vbNewLine _
                & "       INNER JOIN $LM_TRN$..D_ZAI_TRS ZAITRS ON          " & vbNewLine _
                & "       IDOTRS.N_ZAI_REC_NO = ZAITRS.ZAI_REC_NO           " & vbNewLine _
                & "                                                         " & vbNewLine _
                & "       WHERE                                             " & vbNewLine _
                & "           IDOTRS.SYS_DEL_FLG = '0'                      " & vbNewLine _
                & "       AND IDOTRS.IDO_DATE >= '" & _MONTH_AGO & "'       " & vbNewLine _
                & "                                                         " & vbNewLine _
                & "      ) ZAI                                              " & vbNewLine _
                & "                                                         " & vbNewLine _
                & "     ) BASENEWIDO                                        " & vbNewLine _
                & "     GROUP BY                                            " & vbNewLine _
                & "      NRS_BR_CD                                          " & vbNewLine _
                & "     ,REC_CTL_NO                                         " & vbNewLine _
                & "     ,ZAI_REC_NO                                         " & vbNewLine _
                & "     ,WH_CD                                              " & vbNewLine _
                & "     ,TOU_NO                                             " & vbNewLine _
                & "     ,SITU_NO                                            " & vbNewLine _
                & "     ,GOODS_CD_NRS                                       " & vbNewLine _
                & "     ,CUST_CD_L                                          " & vbNewLine _
                & "     ,CUST_CD_M                                          " & vbNewLine _
                & "     ---,TOU_IN_NB                                          " & vbNewLine _
                & "     ---,TOU_OUT_NB                                         " & vbNewLine _
                & "     ---,ZEN_KURI_NB                                        " & vbNewLine _
                & "     ---,TOU_KURI_NB                                        " & vbNewLine _
                & "     ,BASE_DATE                                          " & vbNewLine _
                & "     ,IDO_DATE                                           " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT2_1_1"
    Private Function SQL_Insert_D_WK_INOUT2_1_1() As String
        Dim sql As String = "" & vbNewLine _
                & "      INSERT INTO $LM_TRN$..D_WK_INOUT2                                                          " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "     ( NRS_BR_CD                                                                                 " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                 " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                 " & vbNewLine _
                & "     ,WH_CD                                                                                      " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                               " & vbNewLine _
                & "     ,CUST_CD_L                                                                                  " & vbNewLine _
                & "     ,CUST_CD_M                                                                                  " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                                " & vbNewLine _
                & "     ,TOU_IN_NB                                                                                  " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                                 " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                                " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                               " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                               " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                               " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                               " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                               " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                               " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                               " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                               " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                               " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "     SELECT                                                                                      " & vbNewLine _
                & "      NRS_BR_CD                                                                                  " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                 " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                 " & vbNewLine _
                & "     ,WH_CD                                                                                      " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                               " & vbNewLine _
                & "     ,CUST_CD_L                                                                                  " & vbNewLine _
                & "     ,CUST_CD_M                                                                                  " & vbNewLine _
                & "     ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                                                            " & vbNewLine _
                & "     ,SUM(TOU_IN_NB)   AS TOU_IN_NB                                                              " & vbNewLine _
                & "     ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                                                             " & vbNewLine _
                & "     ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                                                            " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                                   " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                                    " & vbNewLine _
                & "     FROM                                                                                        " & vbNewLine _
                & "     (                                                                                           " & vbNewLine _
                & "      SELECT                                                                                     " & vbNewLine _
                & "      ZAI.NRS_BR_CD                                                                              " & vbNewLine _
                & "     ,ZAI.REC_CTL_NO                                                                             " & vbNewLine _
                & "     ,ZAI.ZAI_REC_NO                                                                             " & vbNewLine _
                & "     ,ZAI.WH_CD                                                                                  " & vbNewLine _
                & "     ,ZAI.CUST_CD_L                                                                              " & vbNewLine _
                & "     ,ZAI.CUST_CD_M                                                                              " & vbNewLine _
                & "     ,ZAI.GOODS_CD_NRS                                                                           " & vbNewLine _
                & "     ,ZAI.ZEN_KURI_NB                                                                            " & vbNewLine _
                & "     ,ZAI.TOU_IN_NB                                                                              " & vbNewLine _
                & "     ,ZAI.TOU_OUT_NB                                                                             " & vbNewLine _
                & "     ,ZAI.TOU_KURI_NB                                                                            " & vbNewLine _
                & "     	FROM                                                                                    " & vbNewLine _
                & "     	(                                                                                       " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "     				SELECT                                                                      " & vbNewLine _
                & "     		 *                                                                                  " & vbNewLine _
                & "     		FROM $LM_TRN$..D_WK_INOUT1 AS INOUT                                                 " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "         	WHERE                                                                               " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "              INOUT.BASE_DATE < '" & _PRINT_FROM & "'                                            " & vbNewLine _
                & "     		AND INOUT.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                    " & vbNewLine _
                & "     		AND CUST_CD_L= CASE WHEN '" & _CUST_CD_L & "' = '' THEN CUST_CD_L ELSE '" & _CUST_CD_L & "' END         " & vbNewLine _
                & "     		AND CUST_CD_M= CASE WHEN '" & _CUST_CD_M & "' = '' THEN CUST_CD_M ELSE '" & _CUST_CD_M & "' END         " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "     	) ZAI                                                                                   " & vbNewLine _
                & "                                                                                                 " & vbNewLine _
                & "     ) BASEINOUT                                                                                 " & vbNewLine _
                & "     GROUP BY                                                                                    " & vbNewLine _
                & "      NRS_BR_CD                                                                                  " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                 " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                 " & vbNewLine _
                & "     ,WH_CD                                                                                      " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                               " & vbNewLine _
                & "     ,CUST_CD_L                                                                                  " & vbNewLine _
                & "     ,CUST_CD_M                                                                                  " & vbNewLine _
                & "     ---,TOU_IN_NB                                                                                  " & vbNewLine _
                & "     ---,TOU_OUT_NB                                                                                 " & vbNewLine _
                & "     ---,ZEN_KURI_NB                                                                                " & vbNewLine _
                & "     ---,TOU_KURI_NB                                                                                " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT2_1_2"
    Private Function SQL_Insert_D_WK_INOUT2_1_2() As String
        Dim sql As String = "" & vbNewLine _
                & "      INSERT INTO $LM_TRN$..D_WK_INOUT2                                                " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     ( NRS_BR_CD                                                                       " & vbNewLine _
                & "     ,REC_CTL_NO                                                                       " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                       " & vbNewLine _
                & "     ,WH_CD                                                                            " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                     " & vbNewLine _
                & "     ,CUST_CD_L                                                                        " & vbNewLine _
                & "     ,CUST_CD_M                                                                        " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                      " & vbNewLine _
                & "     ,TOU_IN_NB                                                                        " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                       " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                      " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                     " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                     " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                     " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                     " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                     " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                     " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                     " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                     " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                     " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     SELECT                                                                            " & vbNewLine _
                & "      INKAL.NRS_BR_CD AS NRS_BR_CD                                                     " & vbNewLine _
                & "     ,INKAS.INKA_NO_L + INKAS.INKA_NO_M + INKAS.INKA_NO_S AS REC_CTL_NO                " & vbNewLine _
                & "     ,INKAS.ZAI_REC_NO AS ZAI_REC_NO                                                   " & vbNewLine _
                & "     ,INKAL.WH_CD AS WH_CD                                                             " & vbNewLine _
                & "     ,INKAM.GOODS_CD_NRS AS GOODS_CD_NRS                                               " & vbNewLine _
                & "     ,INKAL.CUST_CD_L AS CUST_CD_L                                                     " & vbNewLine _
                & "     ,INKAL.CUST_CD_M AS CUST_CD_M                                                     " & vbNewLine _
                & "     ,0 AS ZEN_KURI_NB                                                               " & vbNewLine _
                & "     ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU) AS TOU_IN_NB                      " & vbNewLine _
                & "     ,0 AS TOU_OUT_NB                                                                " & vbNewLine _
                & "     ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU) AS TOU_KURI_NB                    " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                          " & vbNewLine _
                & "     FROM $LM_TRN$..B_INKA_L INKAL                                                     " & vbNewLine _
                & "     INNER JOIN $LM_TRN$..B_INKA_M INKAM ON                                            " & vbNewLine _
                & "     INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                 " & vbNewLine _
                & "     AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                             " & vbNewLine _
                & "     AND INKAM.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     INNER JOIN $LM_TRN$..B_INKA_S INKAS ON                                            " & vbNewLine _
                & "     INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                                 " & vbNewLine _
                & "     AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                             " & vbNewLine _
                & "     AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                             " & vbNewLine _
                & "     AND INKAS.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     INNER JOIN $LM_MST$..M_GOODS GOODS ON                                             " & vbNewLine _
                & "     GOODS.NRS_BR_CD = INKAL.NRS_BR_CD                                                 " & vbNewLine _
                & "     AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                       " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     WHERE                                                                             " & vbNewLine _
                & "     INKAL.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                      " & vbNewLine _
                & "     AND INKAL.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
                & "     AND INKAL.INKA_STATE_KB >= '50'                                                   " & vbNewLine _
                & "     AND INKAL.INKA_DATE >= '" & _PRINT_FROM & "'                                                " & vbNewLine _
                & "     AND INKAL.INKA_DATE <= '" & _PRINT_TO & "'                                                  " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     GROUP BY                                                                          " & vbNewLine _
                & "     INKAL.NRS_BR_CD                                                                   " & vbNewLine _
                & "     ,INKAS.INKA_NO_L                                                                  " & vbNewLine _
                & "     ,INKAS.INKA_NO_M                                                                  " & vbNewLine _
                & "     ,INKAS.INKA_NO_S                                                                  " & vbNewLine _
                & "     ,INKAS.ZAI_REC_NO                                                                 " & vbNewLine _
                & "     ,INKAL.WH_CD                                                                      " & vbNewLine _
                & "     ,INKAL.CUST_CD_L                                                                  " & vbNewLine _
                & "     ,INKAL.CUST_CD_M                                                                  " & vbNewLine _
                & "     ,INKAM.GOODS_CD_NRS                                                               " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT2_1_3"
    Private Function SQL_Insert_D_WK_INOUT2_1_3() As String
        Dim sql As String = "" & vbNewLine _
                & "      INSERT INTO $LM_TRN$..D_WK_INOUT2                                                " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     ( NRS_BR_CD                                                                       " & vbNewLine _
                & "     ,REC_CTL_NO                                                                       " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                       " & vbNewLine _
                & "     ,WH_CD                                                                            " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                     " & vbNewLine _
                & "     ,CUST_CD_L                                                                        " & vbNewLine _
                & "     ,CUST_CD_M                                                                        " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                      " & vbNewLine _
                & "     ,TOU_IN_NB                                                                        " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                       " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                      " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                     " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                     " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                     " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                     " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                     " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                     " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                     " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                     " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                     " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     SELECT                                                                            " & vbNewLine _
                & "      OUTKAL.NRS_BR_CD AS NRS_BR_CD                                                    " & vbNewLine _
                & "     ,OUTKAS.OUTKA_NO_L + OUTKAS.OUTKA_NO_M + OUTKAS.OUTKA_NO_S AS REC_CTL_NO          " & vbNewLine _
                & "     ,OUTKAS.ZAI_REC_NO AS ZAI_REC_NO                                                  " & vbNewLine _
                & "     ,OUTKAL.WH_CD AS WH_CD                                                            " & vbNewLine _
                & "     ,OUTKAM.GOODS_CD_NRS AS GOODS_CD_NRS                                              " & vbNewLine _
                & "     ,OUTKAL.CUST_CD_L AS CUST_CD_L                                                    " & vbNewLine _
                & "     ,OUTKAL.CUST_CD_M AS CUST_CD_M                                                    " & vbNewLine _
                & "     ,0 AS ZEN_KURI_NB                                                               " & vbNewLine _
                & "     ,0 AS TOU_IN_NB                                                                 " & vbNewLine _
                & "     ,SUM(OUTKAS.ALCTD_NB) AS TOU_OUT_NB                                               " & vbNewLine _
                & "     ,SUM(OUTKAS.ALCTD_NB * -1) AS TOU_KURI_NB                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                         " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                          " & vbNewLine _
                & "     FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                   " & vbNewLine _
                & "     INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                          " & vbNewLine _
                & "     OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                               " & vbNewLine _
                & "     AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                         " & vbNewLine _
                & "     AND OUTKAM.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS ON                                          " & vbNewLine _
                & "     OUTKAS.NRS_BR_CD = OUTKAL.NRS_BR_CD                                               " & vbNewLine _
                & "     AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                         " & vbNewLine _
                & "     AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                         " & vbNewLine _
                & "     AND OUTKAS.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     WHERE                                                                             " & vbNewLine _
                & "     OUTKAL.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                     " & vbNewLine _
                & "     AND OUTKAL.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                & "     AND OUTKAL.OUTKA_STATE_KB >= '60'                                                 " & vbNewLine _
                & "     AND OUTKAL.OUTKA_PLAN_DATE >= '" & _PRINT_FROM & "'                                         " & vbNewLine _
                & "     AND OUTKAL.OUTKA_PLAN_DATE <= '" & _PRINT_TO & "'                                           " & vbNewLine _
                & "                                                                                       " & vbNewLine _
                & "     GROUP BY                                                                          " & vbNewLine _
                & "      OUTKAL.NRS_BR_CD                                                                 " & vbNewLine _
                & "     ,OUTKAS.OUTKA_NO_L                                                                " & vbNewLine _
                & "     ,OUTKAS.OUTKA_NO_M                                                                " & vbNewLine _
                & "     ,OUTKAS.OUTKA_NO_S                                                                " & vbNewLine _
                & "     ,OUTKAS.ZAI_REC_NO                                                                " & vbNewLine _
                & "     ,OUTKAL.WH_CD                                                                     " & vbNewLine _
                & "     ,OUTKAM.GOODS_CD_NRS                                                              " & vbNewLine _
                & "     ,OUTKAL.CUST_CD_L                                                                 " & vbNewLine _
                & "     ,OUTKAL.CUST_CD_M                                                                 " & vbNewLine _
                & "     ---,OUTKAS.ALCTD_NB                                                                  " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT2_2_1"
    Private Function SQL_Insert_D_WK_INOUT2_2_1() As String
        Dim sql As String = "" & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT2                                                                               " & vbNewLine _
                & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                                     " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                                     " & vbNewLine _
                & "     ,WH_CD                                                                                                          " & vbNewLine _
                & "     ,TOU_NO                                                                                                         " & vbNewLine _
                & "     ,SITU_NO                                                                                                        " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      NRS_BR_CD                                                                                                      " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                                     " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                                     " & vbNewLine _
                & "     ,WH_CD                                                                                                          " & vbNewLine _
                & "     ,TOU_NO                                                                                                         " & vbNewLine _
                & "     ,SITU_NO                                                                                                        " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
                & "     ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                                                                                " & vbNewLine _
                & "     ,SUM(TOU_IN_NB)   AS TOU_IN_NB                                                                                  " & vbNewLine _
                & "     ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                                                                                 " & vbNewLine _
                & "     ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                                                                                " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     FROM                                                                                                            " & vbNewLine _
                & "     (                                                                                                               " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      ZAI.NRS_BR_CD                                                                                                  " & vbNewLine _
                & "     ,ZAI.REC_CTL_NO                                                                                                 " & vbNewLine _
                & "     ,ZAI.ZAI_REC_NO                                                                                                 " & vbNewLine _
                & "     ,ZAI.WH_CD                                                                                                      " & vbNewLine _
                & "     ,ZAI.TOU_NO                                                                                                     " & vbNewLine _
                & "     ,ZAI.SITU_NO                                                                                                    " & vbNewLine _
                & "     ,ZAI.CUST_CD_L                                                                                                  " & vbNewLine _
                & "     ,ZAI.CUST_CD_M                                                                                                  " & vbNewLine _
                & "     ,ZAI.GOODS_CD_NRS                                                                                               " & vbNewLine _
                & "     ,ZAI.ZEN_KURI_NB                                                                                                " & vbNewLine _
                & "     ,ZAI.TOU_IN_NB                                                                                                  " & vbNewLine _
                & "     ,ZAI.TOU_OUT_NB                                                                                                 " & vbNewLine _
                & "     ,ZAI.TOU_KURI_NB                                                                                                " & vbNewLine _
                & "     	FROM                                                                                                        " & vbNewLine _
                & "     	(                                                                                                           " & vbNewLine _
                & "     				SELECT                                                                                          " & vbNewLine _
                & "     		 *                                                                                                      " & vbNewLine _
                & "     		FROM $LM_TRN$..D_WK_INOUT1 AS INOUT                                                                      " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "         	WHERE                                                                                                   " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "                 INOUT.BASE_DATE < '" & _PRINT_FROM & "'                                                                       " & vbNewLine _
                & "     		AND INOUT.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                                        " & vbNewLine _
                & "             AND CUST_CD_L= CASE WHEN '" & _CUST_CD_L & "' = '' THEN CUST_CD_L ELSE '" & _CUST_CD_L & "' END                             " & vbNewLine _
                & "             AND CUST_CD_M= CASE WHEN '" & _CUST_CD_M & "' = '' THEN CUST_CD_M ELSE '" & _CUST_CD_M & "' END                             " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     	) ZAI                                                                                                       " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     ) BASEINOUT                                                                                                     " & vbNewLine _
                & "     GROUP BY                                                                                                        " & vbNewLine _
                & "      NRS_BR_CD                                                                                                      " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                                     " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                                     " & vbNewLine _
                & "     ,WH_CD                                                                                                          " & vbNewLine _
                & "     ,TOU_NO                                                                                                         " & vbNewLine _
                & "     ,SITU_NO                                                                                                        " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
                & "     ---,TOU_IN_NB                                                                                                      " & vbNewLine _
                & "     ---,TOU_OUT_NB                                                                                                     " & vbNewLine _
                & "     ---,ZEN_KURI_NB                                                                                                    " & vbNewLine _
                & "     ---,TOU_KURI_NB                                                                                                    " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT2_2_2"
    Private Function SQL_Insert_D_WK_INOUT2_2_2() As String
        Dim sql As String = "" & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT2                                                                               " & vbNewLine _
                & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
                & "     ,REC_CTL_NO                                                                                                     " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                                     " & vbNewLine _
                & "     ,WH_CD                                                                                                          " & vbNewLine _
                & "     ,TOU_NO                                                                                                         " & vbNewLine _
                & "     ,SITU_NO                                                                                                        " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      INKAL.NRS_BR_CD AS NRS_BR_CD                                                                                   " & vbNewLine _
                & "     ,INKAS.INKA_NO_L + INKAS.INKA_NO_M + INKAS.INKA_NO_S AS REC_CTL_NO                                              " & vbNewLine _
                & "     ,INKAS.ZAI_REC_NO AS ZAI_REC_NO                                                                                 " & vbNewLine _
                & "     ,INKAL.WH_CD AS WH_CD                                                                                           " & vbNewLine _
                & "     ,INKAS.TOU_NO AS TOU_NO                                                                                         " & vbNewLine _
                & "     ,INKAS.SITU_NO AS SITU_NO                                                                                       " & vbNewLine _
                & "     ,INKAM.GOODS_CD_NRS AS GOODS_CD_NRS                                                                             " & vbNewLine _
                & "     ,INKAL.CUST_CD_L AS CUST_CD_L                                                                                   " & vbNewLine _
                & "     ,INKAL.CUST_CD_M AS CUST_CD_M                                                                                   " & vbNewLine _
                & "     ,0 AS ZEN_KURI_NB                                                                                             " & vbNewLine _
                & "     ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU) AS TOU_IN_NB                                                    " & vbNewLine _
                & "     ,0 AS TOU_OUT_NB                                                                                              " & vbNewLine _
                & "     ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU) AS TOU_KURI_NB                                                  " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     FROM $LM_TRN$..B_INKA_L INKAL                                                                                   " & vbNewLine _
                & "     INNER JOIN $LM_TRN$..B_INKA_M INKAM ON                                                                          " & vbNewLine _
                & "     INKAM.NRS_BR_CD = INKAL.NRS_BR_CD                                                                               " & vbNewLine _
                & "     AND INKAM.INKA_NO_L = INKAL.INKA_NO_L                                                                           " & vbNewLine _
                & "     AND INKAM.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     INNER JOIN $LM_TRN$..B_INKA_S INKAS ON                                                                          " & vbNewLine _
                & "     INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                                                               " & vbNewLine _
                & "     AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                                                           " & vbNewLine _
                & "     AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                                                           " & vbNewLine _
                & "     AND INKAS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     INNER JOIN $LM_MST$..M_GOODS GOODS ON                                                                           " & vbNewLine _
                & "     GOODS.NRS_BR_CD = INKAL.NRS_BR_CD                                                                               " & vbNewLine _
                & "     AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                                                     " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     WHERE                                                                                                           " & vbNewLine _
                & "     INKAL.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                                                    " & vbNewLine _
                & "     AND INKAL.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                & "     AND INKAL.INKA_STATE_KB >= '50'                                                                                 " & vbNewLine _
                & "     AND INKAL.INKA_DATE >= '" & _PRINT_FROM & "'                                                                              " & vbNewLine _
                & "     AND INKAL.INKA_DATE <= '" & _PRINT_TO & "'                                                                                " & vbNewLine _
                & "     AND INKAL.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN INKAL.CUST_CD_L ELSE '" & _CUST_CD_L & "' END                        " & vbNewLine _
                & "     AND INKAL.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN INKAL.CUST_CD_M ELSE '" & _CUST_CD_M & "' END                        " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     GROUP BY                                                                                                        " & vbNewLine _
                & "     INKAL.NRS_BR_CD                                                                                                 " & vbNewLine _
                & "     ,INKAS.INKA_NO_L                                                                                                " & vbNewLine _
                & "     ,INKAS.INKA_NO_M                                                                                                " & vbNewLine _
                & "     ,INKAS.INKA_NO_S                                                                                                " & vbNewLine _
                & "     ,INKAS.ZAI_REC_NO                                                                                               " & vbNewLine _
                & "     ,INKAL.WH_CD                                                                                                    " & vbNewLine _
                & "     ,INKAL.CUST_CD_L                                                                                                " & vbNewLine _
                & "     ,INKAL.CUST_CD_M                                                                                                " & vbNewLine _
                & "     ,INKAS.TOU_NO                                                                                                   " & vbNewLine _
                & "     ,INKAS.SITU_NO                                                                                                  " & vbNewLine _
                & "     ,INKAM.GOODS_CD_NRS                                                                                             " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT2_2_3"
    Private Function SQL_Insert_D_WK_INOUT2_2_3() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT2                                                                               " & vbNewLine _
            & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,REC_CTL_NO                                                                                                     " & vbNewLine _
            & "     ,ZAI_REC_NO                                                                                                     " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,TOU_NO                                                                                                         " & vbNewLine _
            & "     ,SITU_NO                                                                                                        " & vbNewLine _
            & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
            & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
            & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
            & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
            & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
            & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
            & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      OUTKAL.NRS_BR_CD AS NRS_BR_CD                                                                                  " & vbNewLine _
            & "     ,OUTKAS.OUTKA_NO_L + OUTKAS.OUTKA_NO_M + OUTKAS.OUTKA_NO_S AS REC_CTL_NO                                        " & vbNewLine _
            & "     ,OUTKAS.ZAI_REC_NO AS ZAI_REC_NO                                                                                " & vbNewLine _
            & "     ,OUTKAL.WH_CD AS WH_CD                                                                                          " & vbNewLine _
            & "     ,ISNULL(ALLIDO.TOU_NO,OUTKAS.TOU_NO) AS TOU_NO                                                                  " & vbNewLine _
            & "     ,ISNULL(ALLIDO.SITU_NO,OUTKAS.SITU_NO) AS SITU_NO                                                               " & vbNewLine _
            & "     ,OUTKAM.GOODS_CD_NRS AS GOODS_CD_NRS                                                                            " & vbNewLine _
            & "     ,OUTKAL.CUST_CD_L AS CUST_CD_L                                                                                  " & vbNewLine _
            & "     ,OUTKAL.CUST_CD_M AS CUST_CD_M                                                                                  " & vbNewLine _
            & "     ,0 AS ZEN_KURI_NB                                                                                             " & vbNewLine _
            & "     ,0 AS TOU_IN_NB                                                                                               " & vbNewLine _
            & "     ,SUM(OUTKAS.ALCTD_NB) AS TOU_OUT_NB                                                                             " & vbNewLine _
            & "     ,SUM(OUTKAS.ALCTD_NB * -1) AS TOU_KURI_NB                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                                                 " & vbNewLine _
            & "     INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                                        " & vbNewLine _
            & "     OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                             " & vbNewLine _
            & "     AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                                                                       " & vbNewLine _
            & "     AND OUTKAM.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS ON                                                                        " & vbNewLine _
            & "     OUTKAS.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                             " & vbNewLine _
            & "     AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                                                                       " & vbNewLine _
            & "     AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                                                                       " & vbNewLine _
            & "     AND OUTKAS.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN (                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	 TOTIDO.N_ZAI_REC_NO AS REAL_ZAI_REC_NO                                                                     " & vbNewLine _
            & "     	,ZAITRS.TOU_NO AS TOU_NO                                                                                    " & vbNewLine _
            & "     	,ZAITRS.SITU_NO AS SITU_NO                                                                                  " & vbNewLine _
            & "     	,ZAITRS.ZONE_CD AS ZONE_CD                                                                                  " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM                                                                                                            " & vbNewLine _
            & "         (SELECT * FROM                                                                                              " & vbNewLine _
            & "             $LM_TRN$..D_IDO_TRS                                                                                     " & vbNewLine _
            & "          WHERE                                                                                                      " & vbNewLine _
            & "              NRS_BR_CD = '" & _NRS_BR_CD & "'                                                                                 " & vbNewLine _
            & "                AND IDO_DATE >= '" & _PRINT_FROM & "'                                                                          " & vbNewLine _
            & "                AND IDO_DATE <= '" & _PRINT_TO & "'                                                                            " & vbNewLine _
            & "                AND SYS_DEL_FLG = '0') AS IDOTRS                                                                     " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	INNER JOIN $LM_TRN$..D_ZAI_TRS ZAITRS ON                                                                    " & vbNewLine _
            & "     	ZAITRS.NRS_BR_CD = IDOTRS.NRS_BR_CD                                                                         " & vbNewLine _
            & "     	AND ZAITRS.ZAI_REC_NO = IDOTRS.O_ZAI_REC_NO                                                                 " & vbNewLine _
            & "         AND ZAITRS.SYS_UPD_DATE <= '" & _PRINT_FROM & "'                                                                      " & vbNewLine _
            & "         AND ZAITRS.SYS_UPD_DATE  >= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "     	AND ZAITRS.SYS_DEL_FLG = '0'                                                                                " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "         INNER JOIN (                                                                                                " & vbNewLine _
            & "     	SELECT  ISNULL(IDO9.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO8.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO7.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO6.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO5.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO4.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO3.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO2.REC_NO,                                                                                 " & vbNewLine _
            & "                 ISNULL(IDO1.REC_NO,IDO0.REC_NO))))))))) AS REC_NO                                                   " & vbNewLine _
            & "     		   ,IDO0.N_ZAI_REC_NO AS N_ZAI_REC_NO                                                                   " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     		   FROM $LM_TRN$..D_IDO_TRS IDO0                                                                        " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO1 ON                                                                " & vbNewLine _
            & "                IDO1.N_ZAI_REC_NO = IDO0.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO1.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO1.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO1.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO2 ON                                                                " & vbNewLine _
            & "                IDO2.N_ZAI_REC_NO = IDO1.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO2.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO2.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO2.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO3 ON                                                                " & vbNewLine _
            & "                IDO3.N_ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO3.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO3.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO3.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO4 ON                                                                " & vbNewLine _
            & "                IDO4.N_ZAI_REC_NO = IDO3.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO4.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO4.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO4.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO5 ON                                                                " & vbNewLine _
            & "                IDO5.N_ZAI_REC_NO = IDO4.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO5.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO5.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO5.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO6 ON                                                                " & vbNewLine _
            & "                IDO6.N_ZAI_REC_NO = IDO5.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO6.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO6.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO6.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO7 ON                                                                " & vbNewLine _
            & "                IDO7.N_ZAI_REC_NO = IDO6.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO7.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO7.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO7.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO8 ON                                                                " & vbNewLine _
            & "                IDO8.N_ZAI_REC_NO = IDO7.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO8.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO8.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO8.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                LEFT JOIN $LM_TRN$..D_IDO_TRS IDO9 ON                                                                " & vbNewLine _
            & "                IDO9.N_ZAI_REC_NO = IDO8.O_ZAI_REC_NO                                                                " & vbNewLine _
            & "                AND IDO9.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO9.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO9.SYS_DEL_FLG = '0'                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                WHERE IDO0.SYS_DEL_FLG = '0'                                                                         " & vbNewLine _
            & "                AND IDO0.IDO_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & "                AND IDO0.IDO_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & "                AND IDO0.NRS_BR_CD ='" & _NRS_BR_CD & "'                                                                       " & vbNewLine _
            & "     	) TOTIDO ON                                                                                                 " & vbNewLine _
            & "         IDOTRS.REC_NO = TOTIDO.REC_NO                                                                               " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )ALLIDO ON                                                                                                      " & vbNewLine _
            & "     ALLIDO.REAL_ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                                                      " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     OUTKAL.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                                                   " & vbNewLine _
            & "     AND OUTKAL.SYS_DEL_FLG = '0'                                                                                    " & vbNewLine _
            & "     AND OUTKAL.OUTKA_STATE_KB >= '60'                                                                               " & vbNewLine _
            & "     AND OUTKAL.OUTKA_PLAN_DATE >= '" & _PRINT_FROM & "'                                                                       " & vbNewLine _
            & "     AND OUTKAL.OUTKA_PLAN_DATE <= '" & _PRINT_TO & "'                                                                         " & vbNewLine _
            & "     AND OUTKAL.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN OUTKAL.CUST_CD_L ELSE '" & _CUST_CD_L & "' END                      " & vbNewLine _
            & "     AND OUTKAL.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN OUTKAL.CUST_CD_M ELSE '" & _CUST_CD_M & "' END                      " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "      OUTKAL.NRS_BR_CD                                                                                               " & vbNewLine _
            & "     ,OUTKAS.OUTKA_NO_L                                                                                              " & vbNewLine _
            & "     ,OUTKAS.OUTKA_NO_M                                                                                              " & vbNewLine _
            & "     ,OUTKAS.OUTKA_NO_S                                                                                              " & vbNewLine _
            & "     ,OUTKAS.ZAI_REC_NO                                                                                              " & vbNewLine _
            & "     ,OUTKAL.WH_CD                                                                                                   " & vbNewLine _
            & "     ,ALLIDO.TOU_NO                                                                                                  " & vbNewLine _
            & "     ,OUTKAS.TOU_NO                                                                                                  " & vbNewLine _
            & "     ,ALLIDO.SITU_NO                                                                                                 " & vbNewLine _
            & "     ,OUTKAS.SITU_NO                                                                                                 " & vbNewLine _
            & "     ,OUTKAM.GOODS_CD_NRS                                                                                            " & vbNewLine _
            & "     ,OUTKAL.CUST_CD_L                                                                                               " & vbNewLine _
            & "     ,OUTKAL.CUST_CD_M                                                                                               " & vbNewLine _
            & "     ,OUTKAS.ALCTD_NB                                                                                                " & vbNewLine
        Return sql

    End Function
#End Region



#Region "SQL_Insert_D_WK_INOUT3_1"
    Private Function SQL_Insert_D_WK_INOUT3_1() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT3                                                                               " & vbNewLine _
            & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
            & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
            & "     ,ZAI_REC_NO                                                                                                     " & vbNewLine _
            & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
            & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
            & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
            & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
            & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
            & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      SUMNB.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,SUMNB.WH_CD                                                                                                    " & vbNewLine _
            & "     ,SUMNB.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,SUMNB.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,SUMNB.GOODS_CD_NRS                                                                                             " & vbNewLine _
            & "     ,SUMNB.ZAI_REC_NO                                                                                               " & vbNewLine _
            & "     ,SUM(SUMNB.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(SUMNB.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(SUMNB.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(SUMNB.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT2 AS INOUT2                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )SUMNB                                                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "     SUMNB.NRS_BR_CD,                                                                                                " & vbNewLine _
            & "     SUMNB.WH_CD,                                                                                                    " & vbNewLine _
            & "     SUMNB.CUST_CD_L,                                                                                                " & vbNewLine _
            & "     SUMNB.CUST_CD_M,                                                                                                " & vbNewLine _
            & "     SUMNB.GOODS_CD_NRS,                                                                                             " & vbNewLine _
            & "     SUMNB.ZAI_REC_NO                                                                                                " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT3_2"
    Private Function SQL_Insert_D_WK_INOUT3_2() As String
        Dim sql As String = "" & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT3                                                                                  " & vbNewLine _
                & "     (NRS_BR_CD                                                                                                         " & vbNewLine _
                & "     ,WH_CD                                                                                                             " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                         " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                         " & vbNewLine _
                & "     ,TOU_NO                                                                                                            " & vbNewLine _
                & "     ,SITU_NO                                                                                                           " & vbNewLine _
                & "     ,GOODS_CD_NRS                                                                                                      " & vbNewLine _
                & "     ,ZAI_REC_NO                                                                                                        " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                                                       " & vbNewLine _
                & "     ,TOU_IN_NB                                                                                                         " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                                                        " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                                                       " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                                                      " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                                                      " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                                                      " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                                                      " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                                                      " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                                                      " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                                                      " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                                                      " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                                                      " & vbNewLine _
                & "                                                                                                                        " & vbNewLine _
                & "     SELECT                                                                                                             " & vbNewLine _
                & "      SUMNB.NRS_BR_CD                                                                                                   " & vbNewLine _
                & "     ,SUMNB.WH_CD                                                                                                       " & vbNewLine _
                & "     ,SUMNB.CUST_CD_L                                                                                                   " & vbNewLine _
                & "     ,SUMNB.CUST_CD_M                                                                                                   " & vbNewLine _
                & "     ,SUMNB.TOU_NO                                                                                                      " & vbNewLine _
                & "     ,SUMNB.SITU_NO                                                                                                     " & vbNewLine _
                & "     ,SUMNB.GOODS_CD_NRS                                                                                                " & vbNewLine _
                & "     ,SUMNB.ZAI_REC_NO                                                                                                  " & vbNewLine _
                & "     ,SUM(SUMNB.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                             " & vbNewLine _
                & "     ,SUM(SUMNB.TOU_IN_NB) AS TOU_IN_NB                                                                                 " & vbNewLine _
                & "     ,SUM(SUMNB.TOU_OUT_NB) AS TOU_OUT_NB                                                                               " & vbNewLine _
                & "     ,SUM(SUMNB.TOU_KURI_NB) AS TOU_KURI_NB                                                                             " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                                                          " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                                                           " & vbNewLine _
                & "     FROM(                                                                                                              " & vbNewLine _
                & "                                                                                                                        " & vbNewLine _
                & "     	SELECT                                                                                                         " & vbNewLine _
                & "     	*                                                                                                              " & vbNewLine _
                & "     	FROM $LM_TRN$..D_WK_INOUT2 AS INOUT2                                                                           " & vbNewLine _
                & "                                                                                                                        " & vbNewLine _
                & "     )SUMNB                                                                                                             " & vbNewLine _
                & "                                                                                                                        " & vbNewLine _
                & "     GROUP BY                                                                                                           " & vbNewLine _
                & "     SUMNB.NRS_BR_CD,                                                                                                   " & vbNewLine _
                & "     SUMNB.WH_CD,                                                                                                       " & vbNewLine _
                & "     SUMNB.CUST_CD_L,                                                                                                   " & vbNewLine _
                & "     SUMNB.CUST_CD_M,                                                                                                   " & vbNewLine _
                & "     SUMNB.TOU_NO,                                                                                                      " & vbNewLine _
                & "     SUMNB.SITU_NO,                                                                                                     " & vbNewLine _
                & "     SUMNB.GOODS_CD_NRS,                                                                                                " & vbNewLine _
                & "     SUMNB.ZAI_REC_NO                                                                                                   " & vbNewLine
        Return sql

    End Function
#End Region



#Region "SQL_Insert_D_WK_INOUT4_1"
    Private Function SQL_Insert_D_WK_INOUT4_1() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4                                                                                  " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     (NRS_BR_CD                                                                                                         " & vbNewLine _
            & "     ,WH_CD                                                                                                             " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                         " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                         " & vbNewLine _
            & "     ,CUST_CD_S                                                                                                         " & vbNewLine _
            & "     ,CUST_CD_SS                                                                                                        " & vbNewLine _
            & "     ,HANKI_KB                                                                                                          " & vbNewLine _
            & "     ,ZEN_KURI_NB                                                                                                       " & vbNewLine _
            & "     ,ZEN_KURI_QT                                                                                                       " & vbNewLine _
            & "     ,ZEN_KURI_PRICE                                                                                                    " & vbNewLine _
            & "     ,TOU_IN_NB                                                                                                         " & vbNewLine _
            & "     ,TOU_IN_QT                                                                                                         " & vbNewLine _
            & "     ,TOU_IN_PRICE                                                                                                      " & vbNewLine _
            & "     ,TOU_OUT_NB                                                                                                        " & vbNewLine _
            & "     ,TOU_OUT_QT                                                                                                        " & vbNewLine _
            & "     ,TOU_OUT_PRICE                                                                                                     " & vbNewLine _
            & "     ,TOU_KURI_NB                                                                                                       " & vbNewLine _
            & "     ,TOU_KURI_QT                                                                                                       " & vbNewLine _
            & "     ,TOU_KURI_PRICE                                                                                                    " & vbNewLine _
            & "     ,SYS_ENT_DATE                                                                                                      " & vbNewLine _
            & "     ,SYS_ENT_TIME                                                                                                      " & vbNewLine _
            & "     ,SYS_ENT_PGID                                                                                                      " & vbNewLine _
            & "     ,SYS_ENT_USER                                                                                                      " & vbNewLine _
            & "     ,SYS_UPD_DATE                                                                                                      " & vbNewLine _
            & "     ,SYS_UPD_TIME                                                                                                      " & vbNewLine _
            & "     ,SYS_UPD_PGID                                                                                                      " & vbNewLine _
            & "     ,SYS_UPD_USER                                                                                                      " & vbNewLine _
            & "     ,SYS_DEL_FLG)                                                                                                      " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     SELECT                                                                                                             " & vbNewLine _
            & "      SUMPRICE.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,SUMPRICE.WH_CD                                                                                                    " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,SUMPRICE.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,SUM(SUMPRICE.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(SUMPRICE.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(SUMPRICE.ZEN_KURI_QT * SUMPRICE.KITAKU_GOODS_UP) AS ZEN_KURI_PRICE                                            " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_IN_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_IN_PRICE                                                " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_OUT_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_OUT_PRICE                                              " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_KURI_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_KURI_PRICE                                            " & vbNewLine _
            & "     , '" & _SYS_ENT_DATE & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_ENT_TIME & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_ENT_PGID & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_ENT_USER & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_UPD_DATE & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_UPD_TIME & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_UPD_PGID & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_UPD_USER & "'                                                                                          " & vbNewLine _
            & "     , '" & _SYS_DEL_FLG & "'                                                                                           " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     FROM(                                                                                                              " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     SELECT                                                                                                             " & vbNewLine _
            & "      SUMQT.NRS_BR_CD                                                                                                   " & vbNewLine _
            & "     ,SUMQT.WH_CD                                                                                                       " & vbNewLine _
            & "     ,SUMQT.CUST_CD_L                                                                                                   " & vbNewLine _
            & "     ,SUMQT.CUST_CD_M                                                                                                   " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                         " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                                       " & vbNewLine _
            & "     ,CASE RTRIM(ISNULL(GOODS.SHOBO_CD,''))                                                                             " & vbNewLine _
            & "           WHEN '' THEN 'H'                                                                                             " & vbNewLine _
            & "           ELSE 'K'                                                                                                     " & vbNewLine _
            & "           END AS HANKI_KB                                                                                              " & vbNewLine _
            & "     ,SUMQT.ZEN_KURI_NB                                                                                                 " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.ZEN_KURI_NB AS ZEN_KURI_QT                                                     " & vbNewLine _
            & "     ,SUMQT.TOU_IN_NB                                                                                                   " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_IN_NB AS TOU_IN_QT                                                         " & vbNewLine _
            & "     ,SUMQT.TOU_OUT_NB                                                                                                  " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_OUT_NB AS TOU_OUT_QT                                                       " & vbNewLine _
            & "     ,SUMQT.TOU_KURI_NB                                                                                                 " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_KURI_NB AS TOU_KURI_QT                                                     " & vbNewLine _
            & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                                                             " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     FROM(                                                                                                              " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     	(                                                                                                              " & vbNewLine _
            & "     	SELECT                                                                                                         " & vbNewLine _
            & "     	*                                                                                                              " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT3 AS INOUT3                                                                           " & vbNewLine _
            & "     	)                                                                                                         " & vbNewLine _
            & "     )SUMQT                                                                                                         " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                                               " & vbNewLine _
            & "     GOODS.GOODS_CD_NRS = SUMQT.GOODS_CD_NRS                                                                            " & vbNewLine _
            & "     AND GOODS.NRS_BR_CD = SUMQT.NRS_BR_CD                                                                              " & vbNewLine _
            & "     AND GOODS.SYS_DEL_FLG = '0'                                                                                        " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     WHERE                                                                                                              " & vbNewLine _
            & "         SUMQT.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN SUMQT.CUST_CD_L ELSE '" & _CUST_CD_L & "' END                           " & vbNewLine _
            & "     AND SUMQT.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN SUMQT.CUST_CD_M ELSE '" & _CUST_CD_M & "' END                           " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     )SUMPRICE                                                                                                          " & vbNewLine _
            & "                                                                                                                        " & vbNewLine _
            & "     GROUP BY                                                                                                           " & vbNewLine _
            & "     SUMPRICE.NRS_BR_CD,                                                                                                " & vbNewLine _
            & "     SUMPRICE.WH_CD,                                                                                                    " & vbNewLine _
            & "     SUMPRICE.CUST_CD_L,                                                                                                " & vbNewLine _
            & "     SUMPRICE.CUST_CD_M,                                                                                                " & vbNewLine _
            & "     SUMPRICE.CUST_CD_S,                                                                                                " & vbNewLine _
            & "     SUMPRICE.CUST_CD_SS,                                                                                               " & vbNewLine _
            & "     SUMPRICE.HANKI_KB                                                                                                  " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT4_2"
    Private Function SQL_Insert_D_WK_INOUT4_2() As String
        Dim sql As String = "" & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT4                                                                               " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
                & "     ,WH_CD                                                                                                          " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_S                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_SS                                                                                                     " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,ZEN_KURI_QT                                                                                                    " & vbNewLine _
                & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
                & "     ,TOU_IN_QT                                                                                                      " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
                & "     ,TOU_OUT_QT                                                                                                     " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,TOU_KURI_QT                                                                                                    " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      SUMPRICE.NRS_BR_CD                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.WH_CD                                                                                                 " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_L                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_M                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_S                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_SS                                                                                            " & vbNewLine _
                & "     ,SUM(SUMPRICE.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                       " & vbNewLine _
                & "     ,SUM(SUMPRICE.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                       " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_IN_NB) AS TOU_IN_NB                                                                           " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_IN_QT) AS TOU_IN_QT                                                                           " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_OUT_NB) AS TOU_OUT_NB                                                                         " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_OUT_QT) AS TOU_OUT_QT                                                                         " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_KURI_NB) AS TOU_KURI_NB                                                                       " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_KURI_QT) AS TOU_KURI_QT                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     FROM(                                                                                                           " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      SUMQT.NRS_BR_CD                                                                                                " & vbNewLine _
                & "     ,SUMQT.WH_CD                                                                                                    " & vbNewLine _
                & "     ,SUMQT.CUST_CD_L                                                                                                " & vbNewLine _
                & "     ,SUMQT.CUST_CD_M                                                                                                " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                      " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                                    " & vbNewLine _
                & "     ,SUMQT.ZEN_KURI_NB                                                                                              " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.ZEN_KURI_NB AS ZEN_KURI_QT                                                  " & vbNewLine _
                & "     ,SUMQT.TOU_IN_NB                                                                                                " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_IN_NB AS TOU_IN_QT                                                      " & vbNewLine _
                & "     ,SUMQT.TOU_OUT_NB                                                                                               " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_OUT_NB AS TOU_OUT_QT                                                    " & vbNewLine _
                & "     ,SUMQT.TOU_KURI_NB                                                                                              " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_KURI_NB AS TOU_KURI_QT                                                  " & vbNewLine _
                & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                                                       " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     FROM(                                                                                                           " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     	(                                                                                                           " & vbNewLine _
                & "     	SELECT                                                                                                      " & vbNewLine _
                & "     	*                                                                                                           " & vbNewLine _
                & "     	FROM $LM_TRN$..D_WK_INOUT3 AS INOUT3                                                                        " & vbNewLine _
                & "     	)                                                                                                      " & vbNewLine _
                & "     )SUMQT                                                                                                      " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                                            " & vbNewLine _
                & "     GOODS.GOODS_CD_NRS = SUMQT.GOODS_CD_NRS                                                                         " & vbNewLine _
                & "     AND GOODS.NRS_BR_CD = SUMQT.NRS_BR_CD                                                                           " & vbNewLine _
                & "     AND GOODS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     WHERE                                                                                                           " & vbNewLine _
                & "     --SUMQT.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN SUMQT.CUST_CD_L ELSE '" & _CUST_CD_L & "' END                          " & vbNewLine _
                & "     --AND SUMQT.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN SUMQT.CUST_CD_M ELSE '" & _CUST_CD_M & "' END                      " & vbNewLine _
                & "     SUMQT.CUST_CD_L = '" & _CUST_CD_L & "'                                                                                    " & vbNewLine _
                & "     AND SUMQT.CUST_CD_M = '" & _CUST_CD_M & "'                                                                                " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     )SUMPRICE                                                                                                       " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     GROUP BY                                                                                                        " & vbNewLine _
                & "     SUMPRICE.NRS_BR_CD,                                                                                             " & vbNewLine _
                & "     SUMPRICE.WH_CD,                                                                                                 " & vbNewLine _
                & "     SUMPRICE.CUST_CD_L,                                                                                             " & vbNewLine _
                & "     SUMPRICE.CUST_CD_M,                                                                                             " & vbNewLine _
                & "     SUMPRICE.CUST_CD_S,                                                                                             " & vbNewLine _
                & "     SUMPRICE.CUST_CD_SS                                                                                             " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT4_3"
    Private Function SQL_Insert_D_WK_INOUT4_3() As String
        Dim sql As String = "" & vbNewLine _
                & "     INSERT INTO $LM_TRN$..D_WK_INOUT4                                                                               " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
                & "     ,WH_CD                                                                                                          " & vbNewLine _
                & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
                & "     ,TOU_NO                                                                                                         " & vbNewLine _
                & "     ,SITU_NO                                                                                                        " & vbNewLine _
                & "     ,CUST_CD_S                                                                                                      " & vbNewLine _
                & "     ,CUST_CD_SS                                                                                                     " & vbNewLine _
                & "     ,HANKI_KB                                                                                                       " & vbNewLine _
                & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,ZEN_KURI_QT                                                                                                    " & vbNewLine _
                & "     ,ZEN_KURI_PRICE                                                                                                 " & vbNewLine _
                & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
                & "     ,TOU_IN_QT                                                                                                      " & vbNewLine _
                & "     ,TOU_IN_PRICE                                                                                                   " & vbNewLine _
                & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
                & "     ,TOU_OUT_QT                                                                                                     " & vbNewLine _
                & "     ,TOU_OUT_PRICE                                                                                                  " & vbNewLine _
                & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
                & "     ,TOU_KURI_QT                                                                                                    " & vbNewLine _
                & "     ,TOU_KURI_PRICE                                                                                                 " & vbNewLine _
                & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
                & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
                & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      SUMPRICE.NRS_BR_CD                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.WH_CD                                                                                                 " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_L                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_M                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.TOU_NO                                                                                                " & vbNewLine _
                & "     ,SUMPRICE.SITU_NO                                                                                               " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_S                                                                                             " & vbNewLine _
                & "     ,SUMPRICE.CUST_CD_SS                                                                                            " & vbNewLine _
                & "     ,SUMPRICE.HANKI_KB                                                                                              " & vbNewLine _
                & "     ,SUM(SUMPRICE.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                       " & vbNewLine _
                & "     ,SUM(SUMPRICE.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                      " & vbNewLine _
                & "     ,SUM(SUMPRICE.ZEN_KURI_QT * SUMPRICE.KITAKU_GOODS_UP) AS ZEN_KURI_PRICE                                         " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_IN_NB) AS TOU_IN_NB                                                                           " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_IN_QT) AS TOU_IN_QT                                                                           " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_IN_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_IN_PRICE                                             " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_OUT_NB) AS TOU_OUT_NB                                                                         " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_OUT_QT) AS TOU_OUT_QT                                                                         " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_OUT_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_OUT_PRICE                                           " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_KURI_NB) AS TOU_KURI_NB                                                                       " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_KURI_QT) AS TOU_KURI_QT                                                                       " & vbNewLine _
                & "     ,SUM(SUMPRICE.TOU_KURI_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_KURI_PRICE                                         " & vbNewLine _
                & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
                & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     FROM(                                                                                                           " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      SUMQT.NRS_BR_CD                                                                                                " & vbNewLine _
                & "     ,SUMQT.WH_CD                                                                                                    " & vbNewLine _
                & "     ,SUMQT.CUST_CD_L                                                                                                " & vbNewLine _
                & "     ,SUMQT.CUST_CD_M                                                                                                " & vbNewLine _
                & "     ,SUMQT.TOU_NO                                                                                                   " & vbNewLine _
                & "     ,SUMQT.SITU_NO                                                                                                  " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                      " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                                    " & vbNewLine _
                & "     ,CASE RTRIM(ISNULL(GOODS.SHOBO_CD,''))                                                                          " & vbNewLine _
                & "           WHEN '' THEN 'H'                                                                                          " & vbNewLine _
                & "           ELSE 'K'                                                                                                  " & vbNewLine _
                & "           END AS HANKI_KB                                                                                           " & vbNewLine _
                & "     ,SUMQT.ZEN_KURI_NB                                                                                              " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.ZEN_KURI_NB AS ZEN_KURI_QT                                                  " & vbNewLine _
                & "     ,SUMQT.TOU_IN_NB                                                                                                " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_IN_NB AS TOU_IN_QT                                                      " & vbNewLine _
                & "     ,SUMQT.TOU_OUT_NB                                                                                               " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_OUT_NB AS TOU_OUT_QT                                                    " & vbNewLine _
                & "     ,SUMQT.TOU_KURI_NB                                                                                              " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_KURI_NB AS TOU_KURI_QT                                                  " & vbNewLine _
                & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                             " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     FROM(                                                                                                           " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     	(                                                                                                           " & vbNewLine _
                & "     	Select                                                                                                      " & vbNewLine _
                & "     	*                                                                                                           " & vbNewLine _
                & "     	FROM $LM_TRN$..D_WK_INOUT3 AS INOUT3                                                                        " & vbNewLine _
                & "     	)                                                                                                      " & vbNewLine _
                & "     )SUMQT                                                                                                      " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                                            " & vbNewLine _
                & "     GOODS.GOODS_CD_NRS = SUMQT.GOODS_CD_NRS                                                                         " & vbNewLine _
                & "     AND GOODS.NRS_BR_CD = SUMQT.NRS_BR_CD                                                                           " & vbNewLine _
                & "     AND GOODS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     WHERE                                                                                                           " & vbNewLine _
                & "     SUMQT.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN SUMQT.CUST_CD_L ELSE '" & _CUST_CD_L & "' END                          " & vbNewLine _
                & "     AND SUMQT.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN SUMQT.CUST_CD_M ELSE '" & _CUST_CD_M & "' END                      " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     )SUMPRICE                                                                                                       " & vbNewLine _
                & "                                                                                                                     " & vbNewLine _
                & "     GROUP BY                                                                                                        " & vbNewLine _
                & "     SUMPRICE.NRS_BR_CD,                                                                                             " & vbNewLine _
                & "     SUMPRICE.WH_CD,                                                                                                 " & vbNewLine _
                & "     SUMPRICE.CUST_CD_L,                                                                                             " & vbNewLine _
                & "     SUMPRICE.CUST_CD_M,                                                                                             " & vbNewLine _
                & "     SUMPRICE.CUST_CD_S,                                                                                             " & vbNewLine _
                & "     SUMPRICE.CUST_CD_SS,                                                                                            " & vbNewLine _
                & "     SUMPRICE.TOU_NO,                                                                                                " & vbNewLine _
                & "     SUMPRICE.SITU_NO,                                                                                               " & vbNewLine _
                & "     SUMPRICE.HANKI_KB      "
        Return sql

    End Function
#End Region

#Region "SQL_Insert_D_WK_INOUT4_4"
    Private Function SQL_Insert_D_WK_INOUT4_4() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4                                                                               " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     (NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
            & "     ,TOU_NO                                                                                                         " & vbNewLine _
            & "     ,SITU_NO                                                                                                        " & vbNewLine _
            & "     ,CUST_CD_S                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_SS                                                                                                     " & vbNewLine _
            & "     ,ZEN_KURI_NB                                                                                                    " & vbNewLine _
            & "     ,ZEN_KURI_QT                                                                                                    " & vbNewLine _
            & "     ,TOU_IN_NB                                                                                                      " & vbNewLine _
            & "     ,TOU_IN_QT                                                                                                      " & vbNewLine _
            & "     ,TOU_OUT_NB                                                                                                     " & vbNewLine _
            & "     ,TOU_OUT_QT                                                                                                     " & vbNewLine _
            & "     ,TOU_KURI_NB                                                                                                    " & vbNewLine _
            & "     ,TOU_KURI_QT                                                                                                    " & vbNewLine _
            & "     ,SYS_ENT_DATE                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_TIME                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_PGID                                                                                                   " & vbNewLine _
            & "     ,SYS_ENT_USER                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_DATE                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_TIME                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_PGID                                                                                                   " & vbNewLine _
            & "     ,SYS_UPD_USER                                                                                                   " & vbNewLine _
            & "     ,SYS_DEL_FLG)                                                                                                   " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      SUMPRICE.NRS_BR_CD                                                                                             " & vbNewLine _
            & "     ,SUMPRICE.WH_CD                                                                                                 " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_L                                                                                             " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_M                                                                                             " & vbNewLine _
            & "     ,SUMPRICE.TOU_NO                                                                                                " & vbNewLine _
            & "     ,SUMPRICE.SITU_NO                                                                                               " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_S                                                                                             " & vbNewLine _
            & "     ,SUMPRICE.CUST_CD_SS                                                                                            " & vbNewLine _
            & "     ,SUM(SUMPRICE.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                       " & vbNewLine _
            & "     ,SUM(SUMPRICE.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                       " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_IN_NB) AS TOU_IN_NB                                                                           " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_IN_QT) AS TOU_IN_QT                                                                           " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_OUT_NB) AS TOU_OUT_NB                                                                         " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_OUT_QT) AS TOU_OUT_QT                                                                         " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_KURI_NB) AS TOU_KURI_NB                                                                       " & vbNewLine _
            & "     ,SUM(SUMPRICE.TOU_KURI_QT) AS TOU_KURI_QT                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_DATE & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_TIME & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_PGID & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_ENT_USER & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_DATE & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_TIME & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_PGID & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_UPD_USER & "'                                                                                       " & vbNewLine _
            & "     , '" & _SYS_DEL_FLG & "'                                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      SUMQT.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,SUMQT.WH_CD                                                                                                    " & vbNewLine _
            & "     ,SUMQT.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,SUMQT.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,SUMQT.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,SUMQT.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                      " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                                    " & vbNewLine _
            & "     ,SUMQT.ZEN_KURI_NB                                                                                              " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.ZEN_KURI_NB AS ZEN_KURI_QT                                                  " & vbNewLine _
            & "     ,SUMQT.TOU_IN_NB                                                                                                " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_IN_NB AS TOU_IN_QT                                                      " & vbNewLine _
            & "     ,SUMQT.TOU_OUT_NB                                                                                               " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_OUT_NB AS TOU_OUT_QT                                                    " & vbNewLine _
            & "     ,SUMQT.TOU_KURI_NB                                                                                              " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * SUMQT.TOU_KURI_NB AS TOU_KURI_QT                                                  " & vbNewLine _
            & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "         (                                                                                                           " & vbNewLine _
            & "     	Select                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT3 AS INOUT3                                                                        " & vbNewLine _
            & "         )                                                                                                           " & vbNewLine _
            & "     ) SUMQT                                                                                                         " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                                            " & vbNewLine _
            & "     GOODS.GOODS_CD_NRS = SUMQT.GOODS_CD_NRS                                                                         " & vbNewLine _
            & "     AND GOODS.NRS_BR_CD = SUMQT.NRS_BR_CD                                                                           " & vbNewLine _
            & "     AND GOODS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     --SUMQT.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN SUMQT.CUST_CD_L ELSE '" & _CUST_CD_L & "' END                          " & vbNewLine _
            & "     --AND SUMQT.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN SUMQT.CUST_CD_M ELSE '" & _CUST_CD_M & "' END                      " & vbNewLine _
            & "     SUMQT.CUST_CD_L = '" & _CUST_CD_L & "'                                                                                    " & vbNewLine _
            & "     AND SUMQT.CUST_CD_M = '" & _CUST_CD_M & "'                                                                                " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )SUMPRICE                                                                                                       " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "     SUMPRICE.NRS_BR_CD,                                                                                             " & vbNewLine _
            & "     SUMPRICE.WH_CD,                                                                                                 " & vbNewLine _
            & "     SUMPRICE.CUST_CD_L,                                                                                             " & vbNewLine _
            & "     SUMPRICE.CUST_CD_M,                                                                                             " & vbNewLine _
            & "     SUMPRICE.CUST_CD_S,                                                                                             " & vbNewLine _
            & "     SUMPRICE.CUST_CD_SS,                                                                                            " & vbNewLine _
            & "     SUMPRICE.TOU_NO,                                                                                                " & vbNewLine _
            & "     SUMPRICE.SITU_NO                                                                                                " & vbNewLine
        Return sql

    End Function
#End Region



#Region "SELECT_D_WK_INOUT2_DATA"

#Region "SQL_SELECT_D_WK_INOUT2_DATA_1_1"
    Private Function SQL_SELECT_D_WK_INOUT2_DATA_1_1() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                                                     " & vbNewLine _
                & "  NRS_BR_CD        AS NRS_BR_CD                                                                             " & vbNewLine _
                & " ,REC_CTL_NO       AS REC_CTL_NO                                                                            " & vbNewLine _
                & " ,ZAI_REC_NO       AS ZAI_REC_NO                                                                            " & vbNewLine _
                & " ,WH_CD            AS WH_CD                                                                                 " & vbNewLine _
                & " ,GOODS_CD_NRS     AS GOODS_CD_NRS                                                                          " & vbNewLine _
                & " ,CUST_CD_L        AS CUST_CD_L                                                                             " & vbNewLine _
                & " ,CUST_CD_M        AS CUST_CD_M                                                                             " & vbNewLine _
                & " ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                                                                           " & vbNewLine _
                & " ,SUM(TOU_IN_NB)   AS TOU_IN_NB                                                                             " & vbNewLine _
                & " ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                                                                            " & vbNewLine _
                & " ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                                                                           " & vbNewLine _
                & " FROM                                                                                                       " & vbNewLine _
                & " (                                                                                                          " & vbNewLine _
                & "  SELECT                                                                                                    " & vbNewLine _
                & "  ZAI.NRS_BR_CD                                                                                             " & vbNewLine _
                & " ,ZAI.REC_CTL_NO                                                                                            " & vbNewLine _
                & " ,ZAI.ZAI_REC_NO                                                                                            " & vbNewLine _
                & " ,ZAI.WH_CD                                                                                                 " & vbNewLine _
                & " ,ZAI.CUST_CD_L                                                                                             " & vbNewLine _
                & " ,ZAI.CUST_CD_M                                                                                             " & vbNewLine _
                & " ,ZAI.GOODS_CD_NRS                                                                                          " & vbNewLine _
                & " ,ZAI.ZEN_KURI_NB                                                                                           " & vbNewLine _
                & " ,ZAI.TOU_IN_NB                                                                                             " & vbNewLine _
                & " ,ZAI.TOU_OUT_NB                                                                                            " & vbNewLine _
                & " ,ZAI.TOU_KURI_NB                                                                                           " & vbNewLine _
                & " FROM(                                                                                                      " & vbNewLine _
                & " 	SELECT                                                                                                 " & vbNewLine _
                & " 	 *                                                                                                     " & vbNewLine _
                & " 	FROM $LM_TRN$..D_WK_INOUT1 AS INOUT                                                                    " & vbNewLine _
                & "    	WHERE                                                                                                  " & vbNewLine _
                & "         INOUT.BASE_DATE < '" & _PRINT_FROM & "'                                                            " & vbNewLine _
                & " 	AND INOUT.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                             " & vbNewLine _
                & " 	AND CUST_CD_L       = CASE WHEN '" & _CUST_CD_L & "' = '' THEN CUST_CD_L ELSE '" & _CUST_CD_L & "' END " & vbNewLine _
                & " 	AND CUST_CD_M       = CASE WHEN '" & _CUST_CD_M & "' = '' THEN CUST_CD_M ELSE '" & _CUST_CD_M & "' END " & vbNewLine _
                & " 	) ZAI                                                                                                  " & vbNewLine _
                & " ) BASEINOUT                                                                                                " & vbNewLine _
                & " GROUP BY                                                                                                   " & vbNewLine _
                & "  NRS_BR_CD                                                                                                 " & vbNewLine _
                & " ,REC_CTL_NO                                                                                                " & vbNewLine _
                & " ,ZAI_REC_NO                                                                                                " & vbNewLine _
                & " ,WH_CD                                                                                                     " & vbNewLine _
                & " ,GOODS_CD_NRS                                                                                              " & vbNewLine _
                & " ,CUST_CD_L                                                                                                 " & vbNewLine _
                & " ,CUST_CD_M                                                                                                 " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT2_DATA_1_2"
    Private Function SQL_SELECT_D_WK_INOUT2_DATA_1_2() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                              " & vbNewLine _
                & "  INKAL.NRS_BR_CD                                         AS NRS_BR_CD               " & vbNewLine _
                & " ,INKAS.INKA_NO_L + INKAS.INKA_NO_M + INKAS.INKA_NO_S     AS REC_CTL_NO              " & vbNewLine _
                & " ,INKAS.ZAI_REC_NO                                        AS ZAI_REC_NO              " & vbNewLine _
                & " ,INKAL.WH_CD                                             AS WH_CD                   " & vbNewLine _
                & " ,INKAM.GOODS_CD_NRS                                      AS GOODS_CD_NRS            " & vbNewLine _
                & " ,INKAL.CUST_CD_L                                         AS CUST_CD_L               " & vbNewLine _
                & " ,INKAL.CUST_CD_M                                         AS CUST_CD_M               " & vbNewLine _
                & " ,0                                                       AS ZEN_KURI_NB             " & vbNewLine _
                & " ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU)          AS TOU_IN_NB               " & vbNewLine _
                & " ,0                                                       AS TOU_OUT_NB              " & vbNewLine _
                & " ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU)          AS TOU_KURI_NB             " & vbNewLine _
                & " FROM $LM_TRN$..B_INKA_L INKAL                                                       " & vbNewLine _
                & " INNER JOIN $LM_TRN$..B_INKA_M INKAM                                                 " & vbNewLine _
                & " ON  INKAM.NRS_BR_CD   = INKAL.NRS_BR_CD                                             " & vbNewLine _
                & " AND INKAM.INKA_NO_L   = INKAL.INKA_NO_L                                             " & vbNewLine _
                & " AND INKAM.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                & " INNER JOIN $LM_TRN$..B_INKA_S INKAS                                                 " & vbNewLine _
                & " ON  INKAS.NRS_BR_CD = INKAL.NRS_BR_CD                                               " & vbNewLine _
                & " AND INKAS.INKA_NO_L = INKAM.INKA_NO_L                                               " & vbNewLine _
                & " AND INKAS.INKA_NO_M = INKAM.INKA_NO_M                                               " & vbNewLine _
                & " AND INKAS.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                & " INNER JOIN $LM_MST$..M_GOODS GOODS                                                  " & vbNewLine _
                & " ON  GOODS.NRS_BR_CD    = INKAL.NRS_BR_CD                                            " & vbNewLine _
                & " AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                         " & vbNewLine _
                & " WHERE                                                                               " & vbNewLine _
                & "     INKAL.NRS_BR_CD      = '" & _NRS_BR_CD & "'                                     " & vbNewLine _
                & " AND INKAL.SYS_DEL_FLG    = '0'                                                      " & vbNewLine _
                & " AND INKAL.INKA_STATE_KB >= '50'                                                     " & vbNewLine _
                & " AND INKAL.INKA_DATE     >= '" & _PRINT_FROM & "'                                    " & vbNewLine _
                & " AND INKAL.INKA_DATE     <= '" & _PRINT_TO & "'                                      " & vbNewLine _
                & " GROUP BY                                                                            " & vbNewLine _
                & "  INKAL.NRS_BR_CD                                                                    " & vbNewLine _
                & " ,INKAS.INKA_NO_L                                                                    " & vbNewLine _
                & " ,INKAS.INKA_NO_M                                                                    " & vbNewLine _
                & " ,INKAS.INKA_NO_S                                                                    " & vbNewLine _
                & " ,INKAS.ZAI_REC_NO                                                                   " & vbNewLine _
                & " ,INKAL.WH_CD                                                                        " & vbNewLine _
                & " ,INKAL.CUST_CD_L                                                                    " & vbNewLine _
                & " ,INKAL.CUST_CD_M                                                                    " & vbNewLine _
                & " ,INKAM.GOODS_CD_NRS                                                                 " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT2_DATA_1_3"
    Private Function SQL_SELECT_D_WK_INOUT2_DATA_1_3() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                            " & vbNewLine _
                & "  OUTKAL.NRS_BR_CD                                             AS NRS_BR_CD        " & vbNewLine _
                & " ,OUTKAS.OUTKA_NO_L + OUTKAS.OUTKA_NO_M + OUTKAS.OUTKA_NO_S    AS REC_CTL_NO       " & vbNewLine _
                & " ,OUTKAS.ZAI_REC_NO                                            AS ZAI_REC_NO       " & vbNewLine _
                & " ,OUTKAL.WH_CD                                                 AS WH_CD            " & vbNewLine _
                & " ,OUTKAM.GOODS_CD_NRS                                          AS GOODS_CD_NRS     " & vbNewLine _
                & " ,OUTKAL.CUST_CD_L                                             AS CUST_CD_L        " & vbNewLine _
                & " ,OUTKAL.CUST_CD_M                                             AS CUST_CD_M        " & vbNewLine _
                & " ,0                                                            AS ZEN_KURI_NB      " & vbNewLine _
                & " ,0                                                            AS TOU_IN_NB        " & vbNewLine _
                & " ,SUM(OUTKAS.ALCTD_NB)                                         AS TOU_OUT_NB       " & vbNewLine _
                & " ,SUM(OUTKAS.ALCTD_NB * -1)                                    AS TOU_KURI_NB      " & vbNewLine _
                & " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                   " & vbNewLine _
                & " INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                             " & vbNewLine _
                & " ON  OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
                & " AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
                & " AND OUTKAM.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                & " INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                             " & vbNewLine _
                & " ON  OUTKAS.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
                & " AND OUTKAS.OUTKA_NO_L  = OUTKAM.OUTKA_NO_L                                        " & vbNewLine _
                & " AND OUTKAS.OUTKA_NO_M  = OUTKAM.OUTKA_NO_M                                        " & vbNewLine _
                & " AND OUTKAS.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
                & " WHERE                                                                             " & vbNewLine _
                & "     OUTKAL.NRS_BR_CD        = '" & _NRS_BR_CD & "'                                " & vbNewLine _
                & " AND OUTKAL.SYS_DEL_FLG      = '0'                                                 " & vbNewLine _
                & " AND OUTKAL.OUTKA_STATE_KB  >= '60'                                                " & vbNewLine _
                & " AND OUTKAL.OUTKA_PLAN_DATE >= '" & _PRINT_FROM & "'                               " & vbNewLine _
                & " AND OUTKAL.OUTKA_PLAN_DATE <= '" & _PRINT_TO & "'                                 " & vbNewLine _
                & " GROUP BY                                                                          " & vbNewLine _
                & "  OUTKAL.NRS_BR_CD                                                                 " & vbNewLine _
                & " ,OUTKAS.OUTKA_NO_L                                                                " & vbNewLine _
                & " ,OUTKAS.OUTKA_NO_M                                                                " & vbNewLine _
                & " ,OUTKAS.OUTKA_NO_S                                                                " & vbNewLine _
                & " ,OUTKAS.ZAI_REC_NO                                                                " & vbNewLine _
                & " ,OUTKAL.WH_CD                                                                     " & vbNewLine _
                & " ,OUTKAM.GOODS_CD_NRS                                                              " & vbNewLine _
                & " ,OUTKAL.CUST_CD_L                                                                 " & vbNewLine _
                & " ,OUTKAL.CUST_CD_M                                                                 " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT2_DATA_2_1"
    Private Function SQL_SELECT_D_WK_INOUT2_DATA_2_1() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                                                             " & vbNewLine _
                & "  NRS_BR_CD        AS NRS_BR_CD                                                                                     " & vbNewLine _
                & " ,REC_CTL_NO       AS REC_CTL_NO                                                                                    " & vbNewLine _
                & " ,ZAI_REC_NO       AS ZAI_REC_NO                                                                                    " & vbNewLine _
                & " ,WH_CD            AS WH_CD                                                                                         " & vbNewLine _
                & " ,TOU_NO           AS TOU_NO                                                                                        " & vbNewLine _
                & " ,SITU_NO          AS SITU_NO                                                                                       " & vbNewLine _
                & " ,GOODS_CD_NRS     AS GOODS_CD_NRS                                                                                  " & vbNewLine _
                & " ,CUST_CD_L        AS CUST_CD_L                                                                                     " & vbNewLine _
                & " ,CUST_CD_M        AS CUST_CD_M                                                                                     " & vbNewLine _
                & " ,SUM(ZEN_KURI_NB) AS ZEN_KURI_NB                                                                                   " & vbNewLine _
                & " ,SUM(TOU_IN_NB)   AS TOU_IN_NB                                                                                     " & vbNewLine _
                & " ,SUM(TOU_OUT_NB)  AS TOU_OUT_NB                                                                                    " & vbNewLine _
                & " ,SUM(TOU_KURI_NB) AS TOU_KURI_NB                                                                                   " & vbNewLine _
                & " FROM(                                                                                                              " & vbNewLine _
                & "     SELECT                                                                                                         " & vbNewLine _
                & "      ZAI.NRS_BR_CD                                                                                                 " & vbNewLine _
                & "     ,ZAI.REC_CTL_NO                                                                                                " & vbNewLine _
                & "     ,ZAI.ZAI_REC_NO                                                                                                " & vbNewLine _
                & "     ,ZAI.WH_CD                                                                                                     " & vbNewLine _
                & "     ,ZAI.TOU_NO                                                                                                    " & vbNewLine _
                & "     ,ZAI.SITU_NO                                                                                                   " & vbNewLine _
                & "     ,ZAI.CUST_CD_L                                                                                                 " & vbNewLine _
                & "     ,ZAI.CUST_CD_M                                                                                                 " & vbNewLine _
                & "     ,ZAI.GOODS_CD_NRS                                                                                              " & vbNewLine _
                & "     ,ZAI.ZEN_KURI_NB                                                                                               " & vbNewLine _
                & "     ,ZAI.TOU_IN_NB                                                                                                 " & vbNewLine _
                & "     ,ZAI.TOU_OUT_NB                                                                                                " & vbNewLine _
                & "     ,ZAI.TOU_KURI_NB                                                                                               " & vbNewLine _
                & " 	FROM                                                                                                           " & vbNewLine _
                & " 	(                                                                                                              " & vbNewLine _
                & " 	    SELECT                                                                                                     " & vbNewLine _
                & " 	    	 *                                                                                                     " & vbNewLine _
                & " 	    FROM $LM_TRN$..D_WK_INOUT1 AS INOUT                                                                        " & vbNewLine _
                & "         WHERE                                                                                                      " & vbNewLine _
                & "             INOUT.BASE_DATE < '" & _PRINT_FROM & "'                                                                " & vbNewLine _
                & " 	    AND INOUT.NRS_BR_CD = '" & _NRS_BR_CD & "'                                                                 " & vbNewLine _
                & "         AND CUST_CD_L       = CASE WHEN '" & _CUST_CD_L & "' = '' THEN CUST_CD_L ELSE '" & _CUST_CD_L & "' END     " & vbNewLine _
                & "         AND CUST_CD_M       = CASE WHEN '" & _CUST_CD_M & "' = '' THEN CUST_CD_M ELSE '" & _CUST_CD_M & "' END     " & vbNewLine _
                & " 	) ZAI                                                                                                          " & vbNewLine _
                & " ) BASEINOUT                                                                                                        " & vbNewLine _
                & " GROUP BY                                                                                                           " & vbNewLine _
                & "  NRS_BR_CD                                                                                                         " & vbNewLine _
                & " ,REC_CTL_NO                                                                                                        " & vbNewLine _
                & " ,ZAI_REC_NO                                                                                                        " & vbNewLine _
                & " ,WH_CD                                                                                                             " & vbNewLine _
                & " ,TOU_NO                                                                                                            " & vbNewLine _
                & " ,SITU_NO                                                                                                           " & vbNewLine _
                & " ,GOODS_CD_NRS                                                                                                      " & vbNewLine _
                & " ,CUST_CD_L                                                                                                         " & vbNewLine _
                & " ,CUST_CD_M                                                                                                         " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT2_DATA_2_2"
    Private Function SQL_SELECT_D_WK_INOUT2_DATA_2_2() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                                                               " & vbNewLine _
                & "  INKAL.NRS_BR_CD                                         AS NRS_BR_CD                                                " & vbNewLine _
                & " ,INKAS.INKA_NO_L + INKAS.INKA_NO_M + INKAS.INKA_NO_S     AS REC_CTL_NO                                               " & vbNewLine _
                & " ,INKAS.ZAI_REC_NO                                        AS ZAI_REC_NO                                               " & vbNewLine _
                & " ,INKAL.WH_CD                                             AS WH_CD                                                    " & vbNewLine _
                & " ,INKAS.TOU_NO                                            AS TOU_NO                                                   " & vbNewLine _
                & " ,INKAS.SITU_NO                                           AS SITU_NO                                                  " & vbNewLine _
                & " ,INKAM.GOODS_CD_NRS                                      AS GOODS_CD_NRS                                             " & vbNewLine _
                & " ,INKAL.CUST_CD_L                                         AS CUST_CD_L                                                " & vbNewLine _
                & " ,INKAL.CUST_CD_M                                         AS CUST_CD_M                                                " & vbNewLine _
                & " ,0                                                       AS ZEN_KURI_NB                                              " & vbNewLine _
                & " ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU)          AS TOU_IN_NB                                                " & vbNewLine _
                & " ,0                                                       AS TOU_OUT_NB                                               " & vbNewLine _
                & " ,SUM((INKAS.KONSU * GOODS.PKG_NB) + INKAS.HASU)          AS TOU_KURI_NB                                              " & vbNewLine _
                & " FROM $LM_TRN$..B_INKA_L INKAL                                                                                        " & vbNewLine _
                & " INNER JOIN $LM_TRN$..B_INKA_M INKAM                                                                                  " & vbNewLine _
                & " ON  INKAM.NRS_BR_CD   = INKAL.NRS_BR_CD                                                                              " & vbNewLine _
                & " AND INKAM.INKA_NO_L   = INKAL.INKA_NO_L                                                                              " & vbNewLine _
                & " AND INKAM.SYS_DEL_FLG = '0'                                                                                          " & vbNewLine _
                & " INNER JOIN $LM_TRN$..B_INKA_S INKAS                                                                                  " & vbNewLine _
                & " ON  INKAS.NRS_BR_CD   = INKAL.NRS_BR_CD                                                                              " & vbNewLine _
                & " AND INKAS.INKA_NO_L   = INKAM.INKA_NO_L                                                                              " & vbNewLine _
                & " AND INKAS.INKA_NO_M   = INKAM.INKA_NO_M                                                                              " & vbNewLine _
                & " AND INKAS.SYS_DEL_FLG = '0'                                                                                          " & vbNewLine _
                & " INNER JOIN $LM_MST$..M_GOODS GOODS                                                                                   " & vbNewLine _
                & " ON  GOODS.NRS_BR_CD    = INKAL.NRS_BR_CD                                                                             " & vbNewLine _
                & " AND GOODS.GOODS_CD_NRS = INKAM.GOODS_CD_NRS                                                                          " & vbNewLine _
                & " WHERE                                                                                                                " & vbNewLine _
                & " INKAL.NRS_BR_CD          = '" & _NRS_BR_CD & "'                                                                      " & vbNewLine _
                & " AND INKAL.SYS_DEL_FLG    = '0'                                                                                       " & vbNewLine _
                & " AND INKAL.INKA_STATE_KB >= '50'                                                                                      " & vbNewLine _
                & " AND INKAL.INKA_DATE     >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
                & " AND INKAL.INKA_DATE     <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
                & " AND INKAL.CUST_CD_L      = CASE WHEN '" & _CUST_CD_L & "' = '' THEN INKAL.CUST_CD_L ELSE '" & _CUST_CD_L & "' END    " & vbNewLine _
                & " AND INKAL.CUST_CD_M      = CASE WHEN '" & _CUST_CD_M & "' = '' THEN INKAL.CUST_CD_M ELSE '" & _CUST_CD_M & "' END    " & vbNewLine _
                & " GROUP BY                                                                                                             " & vbNewLine _
                & " INKAL.NRS_BR_CD                                                                                                      " & vbNewLine _
                & " ,INKAS.INKA_NO_L                                                                                                     " & vbNewLine _
                & " ,INKAS.INKA_NO_M                                                                                                     " & vbNewLine _
                & " ,INKAS.INKA_NO_S                                                                                                     " & vbNewLine _
                & " ,INKAS.ZAI_REC_NO                                                                                                    " & vbNewLine _
                & " ,INKAL.WH_CD                                                                                                         " & vbNewLine _
                & " ,INKAL.CUST_CD_L                                                                                                     " & vbNewLine _
                & " ,INKAL.CUST_CD_M                                                                                                     " & vbNewLine _
                & " ,INKAS.TOU_NO                                                                                                        " & vbNewLine _
                & " ,INKAS.SITU_NO                                                                                                       " & vbNewLine _
                & " ,INKAM.GOODS_CD_NRS                                                                                                  " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT2_DATA_2_3"
    Private Function SQL_SELECT_D_WK_INOUT2_DATA_2_3() As String
        Dim sql As String = "" & vbNewLine _
            & " SELECT                                                                                                                  " & vbNewLine _
            & "  OUTKAL.NRS_BR_CD                                              AS NRS_BR_CD                                             " & vbNewLine _
            & " ,OUTKAS.OUTKA_NO_L + OUTKAS.OUTKA_NO_M + OUTKAS.OUTKA_NO_S     AS REC_CTL_NO                                            " & vbNewLine _
            & " ,OUTKAS.ZAI_REC_NO                                             AS ZAI_REC_NO                                            " & vbNewLine _
            & " ,OUTKAL.WH_CD                                                  AS WH_CD                                                 " & vbNewLine _
            & " ,ISNULL(ALLIDO.TOU_NO,OUTKAS.TOU_NO)                           AS TOU_NO                                                " & vbNewLine _
            & " ,ISNULL(ALLIDO.SITU_NO,OUTKAS.SITU_NO)                         AS SITU_NO                                               " & vbNewLine _
            & " ,OUTKAM.GOODS_CD_NRS                                           AS GOODS_CD_NRS                                          " & vbNewLine _
            & " ,OUTKAL.CUST_CD_L                                              AS CUST_CD_L                                             " & vbNewLine _
            & " ,OUTKAL.CUST_CD_M                                              AS CUST_CD_M                                             " & vbNewLine _
            & " ,0                                                             AS ZEN_KURI_NB                                           " & vbNewLine _
            & " ,0                                                             AS TOU_IN_NB                                             " & vbNewLine _
            & " ,SUM(OUTKAS.ALCTD_NB)                                          AS TOU_OUT_NB                                            " & vbNewLine _
            & " ,SUM(OUTKAS.ALCTD_NB * -1)                                     AS TOU_KURI_NB                                           " & vbNewLine _
            & " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                                                         " & vbNewLine _
            & " INNER JOIN $LM_TRN$..C_OUTKA_M OUTKAM                                                                                   " & vbNewLine _
            & " ON  OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                                                               " & vbNewLine _
            & " AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                                                              " & vbNewLine _
            & " AND OUTKAM.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
            & " INNER JOIN $LM_TRN$..C_OUTKA_S OUTKAS                                                                                   " & vbNewLine _
            & " ON  OUTKAS.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                                                               " & vbNewLine _
            & " AND OUTKAS.OUTKA_NO_L  = OUTKAM.OUTKA_NO_L                                                                              " & vbNewLine _
            & " AND OUTKAS.OUTKA_NO_M  = OUTKAM.OUTKA_NO_M                                                                              " & vbNewLine _
            & " AND OUTKAS.SYS_DEL_FLG = '0'                                                                                            " & vbNewLine _
            & " LEFT JOIN (                                                                                                             " & vbNewLine _
            & " 	SELECT                                                                                                              " & vbNewLine _
            & " 	 TOTIDO.N_ZAI_REC_NO AS REAL_ZAI_REC_NO                                                                             " & vbNewLine _
            & " 	,ZAITRS.TOU_NO AS TOU_NO                                                                                            " & vbNewLine _
            & " 	,ZAITRS.SITU_NO AS SITU_NO                                                                                          " & vbNewLine _
            & " 	,ZAITRS.ZONE_CD AS ZONE_CD                                                                                          " & vbNewLine _
            & " FROM                                                                                                                    " & vbNewLine _
            & "     (SELECT * FROM                                                                                                      " & vbNewLine _
            & "         $LM_TRN$..D_IDO_TRS                                                                                             " & vbNewLine _
            & "      WHERE                                                                                                              " & vbNewLine _
            & "          NRS_BR_CD   = '" & _NRS_BR_CD & "'                                                                             " & vbNewLine _
            & "      AND IDO_DATE   >= '" & _PRINT_FROM & "'                                                                            " & vbNewLine _
            & "      AND IDO_DATE   <= '" & _PRINT_TO & "'                                                                              " & vbNewLine _
            & "      AND SYS_DEL_FLG = '0') AS IDOTRS                                                                                   " & vbNewLine _
            & "                                                                                                                         " & vbNewLine _
            & " 	 INNER JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                                                                              " & vbNewLine _
            & " 	 ON  ZAITRS.NRS_BR_CD     = IDOTRS.NRS_BR_CD                                                                        " & vbNewLine _
            & " 	 AND ZAITRS.ZAI_REC_NO    = IDOTRS.O_ZAI_REC_NO                                                                     " & vbNewLine _
            & "      AND ZAITRS.SYS_UPD_DATE <= '" & _PRINT_FROM & "'                                                                   " & vbNewLine _
            & "      AND ZAITRS.SYS_UPD_DATE >= '" & _PRINT_TO & "'                                                                     " & vbNewLine _
            & " 	 AND ZAITRS.SYS_DEL_FLG   = '0'                                                                                     " & vbNewLine _
            & "                                                                                                                         " & vbNewLine _
            & "      INNER JOIN (                                                                                                       " & vbNewLine _
            & " 	     SELECT                                                                                                         " & vbNewLine _
            & " 	      ISNULL(IDO9.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO8.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO7.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO6.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO5.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO4.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO3.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO2.REC_NO,                                                                                           " & vbNewLine _
            & "           ISNULL(IDO1.REC_NO,IDO0.REC_NO))))))))) AS REC_NO                                                             " & vbNewLine _
            & " 		 ,IDO0.N_ZAI_REC_NO                       AS N_ZAI_REC_NO                                                       " & vbNewLine _
            & " 		 FROM $LM_TRN$..D_IDO_TRS IDO0                                                                                  " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO1                                                                             " & vbNewLine _
            & "          ON  IDO1.N_ZAI_REC_NO = IDO0.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO1.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO1.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO1.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO2                                                                             " & vbNewLine _
            & "          ON  IDO2.N_ZAI_REC_NO = IDO1.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO2.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO2.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO2.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO3                                                                             " & vbNewLine _
            & "          ON  IDO3.N_ZAI_REC_NO = IDO2.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO3.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO3.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO3.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO4                                                                             " & vbNewLine _
            & "          ON  IDO4.N_ZAI_REC_NO = IDO3.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO4.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO4.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO4.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO5                                                                             " & vbNewLine _
            & "          ON  IDO5.N_ZAI_REC_NO = IDO4.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO5.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO5.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO5.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO6                                                                             " & vbNewLine _
            & "          ON  IDO6.N_ZAI_REC_NO = IDO5.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO6.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO6.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO6.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO7                                                                             " & vbNewLine _
            & "          ON  IDO7.N_ZAI_REC_NO = IDO6.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO7.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO7.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO7.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO8                                                                             " & vbNewLine _
            & "          ON  IDO8.N_ZAI_REC_NO = IDO7.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO8.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO8.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO8.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          LEFT JOIN $LM_TRN$..D_IDO_TRS IDO9                                                                             " & vbNewLine _
            & "          ON  IDO9.N_ZAI_REC_NO = IDO8.O_ZAI_REC_NO                                                                      " & vbNewLine _
            & "          AND IDO9.IDO_DATE    >= '" & _PRINT_FROM & "'                                                                  " & vbNewLine _
            & "          AND IDO9.IDO_DATE    <= '" & _PRINT_TO & "'                                                                    " & vbNewLine _
            & "          AND IDO9.SYS_DEL_FLG  = '0'                                                                                    " & vbNewLine _
            & "          WHERE IDO0.SYS_DEL_FLG = '0'                                                                                   " & vbNewLine _
            & "          AND IDO0.IDO_DATE     >= '" & _PRINT_FROM & "'                                                                 " & vbNewLine _
            & "          AND IDO0.IDO_DATE     <= '" & _PRINT_TO & "'                                                                   " & vbNewLine _
            & "          AND IDO0.NRS_BR_CD     = '" & _NRS_BR_CD & "'                                                                  " & vbNewLine _
            & " 	) TOTIDO ON                                                                                                         " & vbNewLine _
            & "     IDOTRS.REC_NO = TOTIDO.REC_NO                                                                                       " & vbNewLine _
            & " )ALLIDO                                                                                                                 " & vbNewLine _
            & " ON ALLIDO.REAL_ZAI_REC_NO = OUTKAS.ZAI_REC_NO                                                                           " & vbNewLine _
            & " WHERE                                                                                                                   " & vbNewLine _
            & "     OUTKAL.NRS_BR_CD        = '" & _NRS_BR_CD & "'                                                                      " & vbNewLine _
            & " AND OUTKAL.SYS_DEL_FLG      = '0'                                                                                       " & vbNewLine _
            & " AND OUTKAL.OUTKA_STATE_KB  >= '60'                                                                                      " & vbNewLine _
            & " AND OUTKAL.OUTKA_PLAN_DATE >= '" & _PRINT_FROM & "'                                                                     " & vbNewLine _
            & " AND OUTKAL.OUTKA_PLAN_DATE <= '" & _PRINT_TO & "'                                                                       " & vbNewLine _
            & " AND OUTKAL.CUST_CD_L        = CASE WHEN '" & _CUST_CD_L & "' = '' THEN OUTKAL.CUST_CD_L ELSE '" & _CUST_CD_L & "' END   " & vbNewLine _
            & " AND OUTKAL.CUST_CD_M        = CASE WHEN '" & _CUST_CD_M & "' = '' THEN OUTKAL.CUST_CD_M ELSE '" & _CUST_CD_M & "' END   " & vbNewLine _
            & " GROUP BY                                                                                                                " & vbNewLine _
            & "  OUTKAL.NRS_BR_CD                                                                                                       " & vbNewLine _
            & " ,OUTKAS.OUTKA_NO_L                                                                                                      " & vbNewLine _
            & " ,OUTKAS.OUTKA_NO_M                                                                                                      " & vbNewLine _
            & " ,OUTKAS.OUTKA_NO_S                                                                                                      " & vbNewLine _
            & " ,OUTKAS.ZAI_REC_NO                                                                                                      " & vbNewLine _
            & " ,OUTKAL.WH_CD                                                                                                           " & vbNewLine _
            & " ,ALLIDO.TOU_NO                                                                                                          " & vbNewLine _
            & " ,OUTKAS.TOU_NO                                                                                                          " & vbNewLine _
            & " ,ALLIDO.SITU_NO                                                                                                         " & vbNewLine _
            & " ,OUTKAS.SITU_NO                                                                                                         " & vbNewLine _
            & " ,OUTKAM.GOODS_CD_NRS                                                                                                    " & vbNewLine _
            & " ,OUTKAL.CUST_CD_L                                                                                                       " & vbNewLine _
            & " ,OUTKAL.CUST_CD_M                                                                                                       " & vbNewLine _
            & " ,OUTKAS.ALCTD_NB                                                                                                        " & vbNewLine
        Return sql

    End Function
#End Region

#End Region

#Region "SELECT_D_WK_INOUT3_DATA"

#Region "SQL_SELECT_D_WK_INOUT3_DATA_1"
    ''' <summary>
    ''' D_WK_INOUT3用データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SQL_SELECT_D_WK_INOUT3_DATA_1() As String
        Dim sql As String = "" & vbNewLine _
            & " SELECT                                                                                             " & vbNewLine _
            & "  SUMNB.NRS_BR_CD        AS NRS_BR_CD                                                               " & vbNewLine _
            & " ,SUMNB.WH_CD            AS WH_CD                                                                   " & vbNewLine _
            & " ,SUMNB.CUST_CD_L        AS CUST_CD_L                                                               " & vbNewLine _
            & " ,SUMNB.CUST_CD_M        AS CUST_CD_M                                                               " & vbNewLine _
            & " ,SUMNB.GOODS_CD_NRS     AS GOODS_CD_NRS                                                            " & vbNewLine _
            & " ,SUMNB.ZAI_REC_NO       AS ZAI_REC_NO                                                              " & vbNewLine _
            & " ,SUM(SUMNB.ZEN_KURI_NB) AS ZEN_KURI_NB                                                             " & vbNewLine _
            & " ,SUM(SUMNB.TOU_IN_NB)   AS TOU_IN_NB                                                               " & vbNewLine _
            & " ,SUM(SUMNB.TOU_OUT_NB)  AS TOU_OUT_NB                                                              " & vbNewLine _
            & " ,SUM(SUMNB.TOU_KURI_NB) AS TOU_KURI_NB                                                             " & vbNewLine _
            & " FROM(                                                                                              " & vbNewLine _
            & " $SQL_SELECT_D_WK_INOUT2$                                                                           " & vbNewLine _
            & " )SUMNB                                                                                             " & vbNewLine _
            & " GROUP BY                                                                                           " & vbNewLine _
            & " SUMNB.NRS_BR_CD,                                                                                   " & vbNewLine _
            & " SUMNB.WH_CD,                                                                                       " & vbNewLine _
            & " SUMNB.CUST_CD_L,                                                                                   " & vbNewLine _
            & " SUMNB.CUST_CD_M,                                                                                   " & vbNewLine _
            & " SUMNB.GOODS_CD_NRS,                                                                                " & vbNewLine _
            & " SUMNB.ZAI_REC_NO                                                                                   " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT3_DATA_2"
    ''' <summary>
    ''' D_WK_INOUT3用データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SQL_SELECT_D_WK_INOUT3_DATA_2() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                                                             " & vbNewLine _
                & "  SUMNB.NRS_BR_CD          AS NRS_BR_CD                                                                             " & vbNewLine _
                & " ,SUMNB.WH_CD              AS WH_CD                                                                                 " & vbNewLine _
                & " ,SUMNB.CUST_CD_L          AS CUST_CD_L                                                                             " & vbNewLine _
                & " ,SUMNB.CUST_CD_M          AS CUST_CD_M                                                                             " & vbNewLine _
                & " ,SUMNB.TOU_NO             AS TOU_NO                                                                                " & vbNewLine _
                & " ,SUMNB.SITU_NO            AS SITU_NO                                                                               " & vbNewLine _
                & " ,SUMNB.GOODS_CD_NRS       AS GOODS_CD_NRS                                                                          " & vbNewLine _
                & " ,SUMNB.ZAI_REC_NO         AS ZAI_REC_NO                                                                            " & vbNewLine _
                & " ,SUM(SUMNB.ZEN_KURI_NB)   AS ZEN_KURI_NB                                                                           " & vbNewLine _
                & " ,SUM(SUMNB.TOU_IN_NB)     AS TOU_IN_NB                                                                             " & vbNewLine _
                & " ,SUM(SUMNB.TOU_OUT_NB)    AS TOU_OUT_NB                                                                            " & vbNewLine _
                & " ,SUM(SUMNB.TOU_KURI_NB)   AS TOU_KURI_NB                                                                           " & vbNewLine _
                & " FROM(                                                                                                              " & vbNewLine _
                & " $SQL_SELECT_D_WK_INOUT2$                                                                                           " & vbNewLine _
                & " )SUMNB                                                                                                             " & vbNewLine _
                & " GROUP BY                                                                                                           " & vbNewLine _
                & " SUMNB.NRS_BR_CD,                                                                                                   " & vbNewLine _
                & " SUMNB.WH_CD,                                                                                                       " & vbNewLine _
                & " SUMNB.CUST_CD_L,                                                                                                   " & vbNewLine _
                & " SUMNB.CUST_CD_M,                                                                                                   " & vbNewLine _
                & " SUMNB.TOU_NO,                                                                                                      " & vbNewLine _
                & " SUMNB.SITU_NO,                                                                                                     " & vbNewLine _
                & " SUMNB.GOODS_CD_NRS,                                                                                                " & vbNewLine _
                & " SUMNB.ZAI_REC_NO                                                                                                   " & vbNewLine
        Return sql

    End Function
#End Region

#End Region

#Region "SELECT_D_WK_INOUT4_DATA"

#Region "SQL_SELECT_D_WK_INOUT4_DATA_1"
    Private Function SQL_SELECT_D_WK_INOUT4_DATA_1() As String
        Dim sql As String = "" & vbNewLine _
            & " SELECT                                                                                                             " & vbNewLine _
            & "  SUMPRICE.NRS_BR_CD                                    AS NRS_BR_CD                                                " & vbNewLine _
            & " ,SUMPRICE.WH_CD                                        AS WH_CD                                                    " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_L                                    AS CUST_CD_L                                                " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_M                                    AS CUST_CD_M                                                " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_S                                    AS CUST_CD_S                                                " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_SS                                   AS CUST_CD_SS                                               " & vbNewLine _
            & " ,SUMPRICE.HANKI_KB                                     AS HANKI_KB                                                 " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_NB)                             AS ZEN_KURI_NB                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_QT)                             AS ZEN_KURI_QT                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_QT * SUMPRICE.KITAKU_GOODS_UP)  AS ZEN_KURI_PRICE                                           " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_NB)                               AS TOU_IN_NB                                                " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_QT)                               AS TOU_IN_QT                                                " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_QT * SUMPRICE.KITAKU_GOODS_UP)    AS TOU_IN_PRICE                                             " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_NB)                              AS TOU_OUT_NB                                               " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_QT)                              AS TOU_OUT_QT                                               " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_QT * SUMPRICE.KITAKU_GOODS_UP)   AS TOU_OUT_PRICE                                            " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_NB)                             AS TOU_KURI_NB                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_QT)                             AS TOU_KURI_QT                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_QT * SUMPRICE.KITAKU_GOODS_UP)  AS TOU_KURI_PRICE                                           " & vbNewLine _
            & " , '" & _SYS_ENT_DATE & "'                              AS SYS_ENT_DATE                                             " & vbNewLine _
            & " , '" & _SYS_ENT_TIME & "'                              AS SYS_ENT_TIME                                             " & vbNewLine _
            & " , '" & _SYS_ENT_PGID & "'                              AS SYS_ENT_PGID                                             " & vbNewLine _
            & " , '" & _SYS_ENT_USER & "'                              AS SYS_ENT_USER                                             " & vbNewLine _
            & " , '" & _SYS_UPD_DATE & "'                              AS SYS_UPD_DATE                                             " & vbNewLine _
            & " , '" & _SYS_UPD_TIME & "'                              AS SYS_UPD_TIME                                             " & vbNewLine _
            & " , '" & _SYS_UPD_PGID & "'                              AS SYS_UPD_PGID                                             " & vbNewLine _
            & " , '" & _SYS_UPD_USER & "'                              AS SYS_UPD_USER                                             " & vbNewLine _
            & " , '" & _SYS_DEL_FLG & "'                               AS SYS_DEL_FLG                                              " & vbNewLine _
            & " FROM(                                                                                                              " & vbNewLine _
            & "     SELECT                                                                                                         " & vbNewLine _
            & "      INOUT3.NRS_BR_CD                                                                                              " & vbNewLine _
            & "     ,INOUT3.WH_CD                                                                                                  " & vbNewLine _
            & "     ,INOUT3.CUST_CD_L                                                                                              " & vbNewLine _
            & "     ,INOUT3.CUST_CD_M                                                                                              " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                     " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                                   " & vbNewLine _
            & "     ,CASE RTRIM(ISNULL(GOODS.SHOBO_CD,''))                                                                         " & vbNewLine _
            & "           WHEN '' THEN 'H'                                                                                         " & vbNewLine _
            & "           ELSE 'K'                                                                                                 " & vbNewLine _
            & "           END AS HANKI_KB                                                                                          " & vbNewLine _
            & "     ,INOUT3.ZEN_KURI_NB                                                                                            " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.ZEN_KURI_NB AS ZEN_KURI_QT                                                " & vbNewLine _
            & "     ,INOUT3.TOU_IN_NB                                                                                              " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_IN_NB AS TOU_IN_QT                                                    " & vbNewLine _
            & "     ,INOUT3.TOU_OUT_NB                                                                                             " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_OUT_NB AS TOU_OUT_QT                                                  " & vbNewLine _
            & "     ,INOUT3.TOU_KURI_NB                                                                                            " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_KURI_NB AS TOU_KURI_QT                                                " & vbNewLine _
            & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                            " & vbNewLine _
            & "     FROM(                                                                                                          " & vbNewLine _
            & "     	$SQL_SELECT_D_WK_INOUT3$                                                                                   " & vbNewLine _
            & "     )INOUT3                                                                                                        " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                              " & vbNewLine _
            & "     ON  GOODS.GOODS_CD_NRS = INOUT3.GOODS_CD_NRS                                                                   " & vbNewLine _
            & "     AND GOODS.NRS_BR_CD    = INOUT3.NRS_BR_CD                                                                      " & vbNewLine _
            & "     AND GOODS.SYS_DEL_FLG  = '0'                                                                                   " & vbNewLine _
            & "     WHERE                                                                                                          " & vbNewLine _
            & "         INOUT3.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN INOUT3.CUST_CD_L ELSE '" & _CUST_CD_L & "' END " & vbNewLine _
            & "     AND INOUT3.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN INOUT3.CUST_CD_M ELSE '" & _CUST_CD_M & "' END " & vbNewLine _
            & " )SUMPRICE                                                                                                          " & vbNewLine _
            & " GROUP BY                                                                                                           " & vbNewLine _
            & " SUMPRICE.NRS_BR_CD,                                                                                                " & vbNewLine _
            & " SUMPRICE.WH_CD,                                                                                                    " & vbNewLine _
            & " SUMPRICE.CUST_CD_L,                                                                                                " & vbNewLine _
            & " SUMPRICE.CUST_CD_M,                                                                                                " & vbNewLine _
            & " SUMPRICE.CUST_CD_S,                                                                                                " & vbNewLine _
            & " SUMPRICE.CUST_CD_SS,                                                                                               " & vbNewLine _
            & " SUMPRICE.HANKI_KB                                                                                                  " & vbNewLine
        Return sql

    End Function
#End Region

#If True Then   'ADD 2020/10/29　新 016063
#Region "SELECT_D_WK_INOUT4A_DATA"

#Region "SQL_SELECT_D_WK_INOUT4A_DATA_1"
    Private Function SQL_SELECT_D_WK_INOUT4A_DATA_1() As String
        Dim sql As String = "" & vbNewLine _
            & " SELECT                                                                                                             " & vbNewLine _
            & "  SUMPRICE.NRS_BR_CD                                    AS NRS_BR_CD                                                " & vbNewLine _
            & " ,SUMPRICE.WH_CD                                        AS WH_CD                                                    " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_L                                    AS CUST_CD_L                                                " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_M                                    AS CUST_CD_M                                                " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_S                                    AS CUST_CD_S                                                " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_SS                                   AS CUST_CD_SS                                               " & vbNewLine _
            & " ,SUMPRICE.GOODS_CD_NRS                                 AS GOODS_CD_NRS                                             " & vbNewLine _
            & " ,SUMPRICE.GOODS_NM_1                                   AS GOODS_NM_1                                               " & vbNewLine _
            & " ,SUMPRICE.SHOBOKIKEN_KB                                AS SHOBOKIKEN_KB                                            " & vbNewLine _
            & " ,SUMPRICE.SHOBOKIKEN_NM                                AS SHOBOKIKEN_NM                                            " & vbNewLine _
            & " ,SUMPRICE.HANKI_KB                                     AS HANKI_KB                                                 " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_NB)                             AS ZEN_KURI_NB                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_QT)                             AS ZEN_KURI_QT                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_QT * SUMPRICE.KITAKU_GOODS_UP)  AS ZEN_KURI_PRICE                                           " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_NB)                               AS TOU_IN_NB                                                " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_QT)                               AS TOU_IN_QT                                                " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_QT * SUMPRICE.KITAKU_GOODS_UP)    AS TOU_IN_PRICE                                             " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_NB)                              AS TOU_OUT_NB                                               " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_QT)                              AS TOU_OUT_QT                                               " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_QT * SUMPRICE.KITAKU_GOODS_UP)   AS TOU_OUT_PRICE                                            " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_NB)                             AS TOU_KURI_NB                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_QT)                             AS TOU_KURI_QT                                              " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_QT * SUMPRICE.KITAKU_GOODS_UP)  AS TOU_KURI_PRICE                                           " & vbNewLine _
            & " , '" & _SYS_ENT_DATE & "'                              AS SYS_ENT_DATE                                             " & vbNewLine _
            & " , '" & _SYS_ENT_TIME & "'                              AS SYS_ENT_TIME                                             " & vbNewLine _
            & " , '" & _SYS_ENT_PGID & "'                              AS SYS_ENT_PGID                                             " & vbNewLine _
            & " , '" & _SYS_ENT_USER & "'                              AS SYS_ENT_USER                                             " & vbNewLine _
            & " , '" & _SYS_UPD_DATE & "'                              AS SYS_UPD_DATE                                             " & vbNewLine _
            & " , '" & _SYS_UPD_TIME & "'                              AS SYS_UPD_TIME                                             " & vbNewLine _
            & " , '" & _SYS_UPD_PGID & "'                              AS SYS_UPD_PGID                                             " & vbNewLine _
            & " , '" & _SYS_UPD_USER & "'                              AS SYS_UPD_USER                                             " & vbNewLine _
            & " , '" & _SYS_DEL_FLG & "'                               AS SYS_DEL_FLG                                              " & vbNewLine _
            & " FROM(                                                                                                              " & vbNewLine _
            & "     SELECT                                                                                                         " & vbNewLine _
            & "      INOUT3.NRS_BR_CD                                                                                              " & vbNewLine _
            & "     ,INOUT3.WH_CD                                                                                                  " & vbNewLine _
            & "     ,INOUT3.CUST_CD_L                                                                                              " & vbNewLine _
            & "     ,INOUT3.CUST_CD_M                                                                                              " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                     " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                                   " & vbNewLine _
            & "     ,ISNULL(GOODS.GOODS_CD_NRS,'') AS GOODS_CD_NRS                                                                 " & vbNewLine _
            & "     ,ISNULL(GOODS.GOODS_NM_1,'')   AS GOODS_NM_1                                                                   " & vbNewLine _
            & "     ,ISNULL(GOODS.SHOBOKIKEN_KB ,'')   AS SHOBOKIKEN_KB                                                            " & vbNewLine _
            & "     ,ISNULL(KBN1.KBN_NM1 ,'')          AS SHOBOKIKEN_NM                                                            " & vbNewLine _
            & "     ,CASE RTRIM(ISNULL(GOODS.SHOBO_CD,''))                                                                         " & vbNewLine _
            & "           WHEN '' THEN 'H'                                                                                         " & vbNewLine _
            & "           ELSE 'K'                                                                                                 " & vbNewLine _
            & "           END AS HANKI_KB                                                                                          " & vbNewLine _
            & "     ,INOUT3.ZEN_KURI_NB                                                                                            " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.ZEN_KURI_NB AS ZEN_KURI_QT                                                " & vbNewLine _
            & "     ,INOUT3.TOU_IN_NB                                                                                              " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_IN_NB AS TOU_IN_QT                                                    " & vbNewLine _
            & "     ,INOUT3.TOU_OUT_NB                                                                                             " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_OUT_NB AS TOU_OUT_QT                                                  " & vbNewLine _
            & "     ,INOUT3.TOU_KURI_NB                                                                                            " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_KURI_NB AS TOU_KURI_QT                                                " & vbNewLine _
            & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                            " & vbNewLine _
            & "     FROM(                                                                                                          " & vbNewLine _
            & "     	$SQL_SELECT_D_WK_INOUT3$                                                                                   " & vbNewLine _
            & "     )INOUT3                                                                                                        " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                              " & vbNewLine _
            & "     ON  GOODS.GOODS_CD_NRS = INOUT3.GOODS_CD_NRS                                                                   " & vbNewLine _
            & "     AND GOODS.NRS_BR_CD    = INOUT3.NRS_BR_CD                                                                      " & vbNewLine _
            & "     AND GOODS.SYS_DEL_FLG  = '0'                                                                                   " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..Z_KBN KBN1                                                                                 " & vbNewLine _
            & "     ON  KBN1.KBN_GROUP_CD  = 'S126'                                                                                " & vbNewLine _
            & "     AND KBN1.KBN_CD        = GOODS.SHOBOKIKEN_KB                                                                   " & vbNewLine _
            & "     AND KBN1.SYS_DEL_FLG   = '0'                                                                                   " & vbNewLine _
            & "     WHERE                                                                                                          " & vbNewLine _
            & "         INOUT3.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN INOUT3.CUST_CD_L ELSE '" & _CUST_CD_L & "' END " & vbNewLine _
            & "     AND INOUT3.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN INOUT3.CUST_CD_M ELSE '" & _CUST_CD_M & "' END " & vbNewLine _
            & " )SUMPRICE                                                                                                          " & vbNewLine _
            & " GROUP BY                                                                                                           " & vbNewLine _
            & " SUMPRICE.NRS_BR_CD,                                                                                                " & vbNewLine _
            & " SUMPRICE.WH_CD,                                                                                                    " & vbNewLine _
            & " SUMPRICE.CUST_CD_L,                                                                                                " & vbNewLine _
            & " SUMPRICE.CUST_CD_M,                                                                                                " & vbNewLine _
            & " SUMPRICE.CUST_CD_S,                                                                                                " & vbNewLine _
            & " SUMPRICE.CUST_CD_SS,                                                                                               " & vbNewLine _
            & " SUMPRICE.HANKI_KB,                                                                                                 " & vbNewLine _
            & " SUMPRICE.GOODS_CD_NRS,                                                                                             " & vbNewLine _
            & " SUMPRICE.GOODS_NM_1,                                                                                               " & vbNewLine _
            & " SUMPRICE.SHOBOKIKEN_KB,                                                                                            " & vbNewLine _
            & " SUMPRICE.SHOBOKIKEN_NM                                                                                             " & vbNewLine
        Return sql

    End Function
#End Region
#End Region
#End If

#Region "SQL_SELECT_D_WK_INOUT4_DATA_2"
    Private Function SQL_SELECT_D_WK_INOUT4_DATA_2() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                                                         " & vbNewLine _
                & "  SUMPRICE.NRS_BR_CD        AS NRS_BR_CD                                                                        " & vbNewLine _
                & " ,SUMPRICE.WH_CD            AS WH_CD                                                                            " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_L        AS CUST_CD_L                                                                        " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_M        AS CUST_CD_M                                                                        " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_S        AS CUST_CD_S                                                                        " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_SS       AS CUST_CD_SS                                                                       " & vbNewLine _
                & " ,SUM(SUMPRICE.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                      " & vbNewLine _
                & " ,SUM(SUMPRICE.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                      " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_IN_NB)   AS TOU_IN_NB                                                                        " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_IN_QT)   AS TOU_IN_QT                                                                        " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_OUT_NB)  AS TOU_OUT_NB                                                                       " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_OUT_QT)  AS TOU_OUT_QT                                                                       " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_KURI_NB) AS TOU_KURI_NB                                                                      " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_KURI_QT) AS TOU_KURI_QT                                                                      " & vbNewLine _
                & " , '" & _SYS_ENT_DATE & "'  AS SYS_ENT_DATE                                                                     " & vbNewLine _
                & " , '" & _SYS_ENT_TIME & "'  AS SYS_ENT_TIME                                                                     " & vbNewLine _
                & " , '" & _SYS_ENT_PGID & "'  AS SYS_ENT_PGID                                                                     " & vbNewLine _
                & " , '" & _SYS_ENT_USER & "'  AS SYS_ENT_USER                                                                     " & vbNewLine _
                & " , '" & _SYS_UPD_DATE & "'  AS SYS_UPD_DATE                                                                     " & vbNewLine _
                & " , '" & _SYS_UPD_TIME & "'  AS SYS_UPD_TIME                                                                     " & vbNewLine _
                & " , '" & _SYS_UPD_PGID & "'  AS SYS_UPD_PGID                                                                     " & vbNewLine _
                & " , '" & _SYS_UPD_USER & "'  AS SYS_UPD_USER                                                                     " & vbNewLine _
                & " , '" & _SYS_DEL_FLG & "'   AS SYS_DEL_FLG                                                                      " & vbNewLine _
                & " FROM(                                                                                                          " & vbNewLine _
                & "     SELECT                                                                                                     " & vbNewLine _
                & "      INOUT3.NRS_BR_CD                                                                                          " & vbNewLine _
                & "     ,INOUT3.WH_CD                                                                                              " & vbNewLine _
                & "     ,INOUT3.CUST_CD_L                                                                                          " & vbNewLine _
                & "     ,INOUT3.CUST_CD_M                                                                                          " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_S,'00') AS CUST_CD_S                                                                 " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_SS,'00') AS CUST_CD_SS                                                               " & vbNewLine _
                & "     ,INOUT3.ZEN_KURI_NB                                                                                        " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.ZEN_KURI_NB AS ZEN_KURI_QT                                            " & vbNewLine _
                & "     ,INOUT3.TOU_IN_NB                                                                                          " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_IN_NB AS TOU_IN_QT                                                " & vbNewLine _
                & "     ,INOUT3.TOU_OUT_NB                                                                                         " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_OUT_NB AS TOU_OUT_QT                                              " & vbNewLine _
                & "     ,INOUT3.TOU_KURI_NB                                                                                        " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_KURI_NB AS TOU_KURI_QT                                            " & vbNewLine _
                & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0) AS KITAKU_GOODS_UP                                                        " & vbNewLine _
                & "     FROM(                                                                                                      " & vbNewLine _
                & "         	$SQL_SELECT_D_WK_INOUT3$                                                                           " & vbNewLine _
                & "     )INOUT3                                                                                                    " & vbNewLine _
                & "     LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                                       " & vbNewLine _
                & "     GOODS.GOODS_CD_NRS    = INOUT3.GOODS_CD_NRS                                                                " & vbNewLine _
                & "     AND GOODS.NRS_BR_CD   = INOUT3.NRS_BR_CD                                                                   " & vbNewLine _
                & "     AND GOODS.SYS_DEL_FLG = '0'                                                                                " & vbNewLine _
                & "     WHERE                                                                                                      " & vbNewLine _
                & "         INOUT3.CUST_CD_L = '" & _CUST_CD_L & "'                                                                " & vbNewLine _
                & "     AND INOUT3.CUST_CD_M = '" & _CUST_CD_M & "'                                                                " & vbNewLine _
                & " )SUMPRICE                                                                                                      " & vbNewLine _
                & " GROUP BY                                                                                                       " & vbNewLine _
                & " SUMPRICE.NRS_BR_CD,                                                                                            " & vbNewLine _
                & " SUMPRICE.WH_CD,                                                                                                " & vbNewLine _
                & " SUMPRICE.CUST_CD_L,                                                                                            " & vbNewLine _
                & " SUMPRICE.CUST_CD_M,                                                                                            " & vbNewLine _
                & " SUMPRICE.CUST_CD_S,                                                                                            " & vbNewLine _
                & " SUMPRICE.CUST_CD_SS                                                                                            " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT4_DATA_3"
    Private Function SQL_SELECT_D_WK_INOUT4_DATA_3() As String
        Dim sql As String = "" & vbNewLine _
                & " SELECT                                                                                                              " & vbNewLine _
                & "  SUMPRICE.NRS_BR_CD                                   AS NRS_BR_CD                                                  " & vbNewLine _
                & " ,SUMPRICE.WH_CD                                       AS WH_CD                                                      " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_L                                   AS CUST_CD_L                                                  " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_M                                   AS CUST_CD_M                                                  " & vbNewLine _
                & " ,SUMPRICE.TOU_NO                                      AS TOU_NO                                                     " & vbNewLine _
                & " ,SUMPRICE.SITU_NO                                     AS SITU_NO                                                    " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_S                                   AS CUST_CD_S                                                  " & vbNewLine _
                & " ,SUMPRICE.CUST_CD_SS                                  AS CUST_CD_SS                                                 " & vbNewLine _
                & " ,SUMPRICE.HANKI_KB                                    AS HANKI_KB                                                   " & vbNewLine _
                & " ,SUM(SUMPRICE.ZEN_KURI_NB)                            AS ZEN_KURI_NB                                                " & vbNewLine _
                & " ,SUM(SUMPRICE.ZEN_KURI_QT)                            AS ZEN_KURI_QT                                                " & vbNewLine _
                & " ,SUM(SUMPRICE.ZEN_KURI_QT * SUMPRICE.KITAKU_GOODS_UP) AS ZEN_KURI_PRICE                                             " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_IN_NB)                              AS TOU_IN_NB                                                  " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_IN_QT)                              AS TOU_IN_QT                                                  " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_IN_QT * SUMPRICE.KITAKU_GOODS_UP)   AS TOU_IN_PRICE                                               " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_OUT_NB)                             AS TOU_OUT_NB                                                 " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_OUT_QT)                             AS TOU_OUT_QT                                                 " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_OUT_QT * SUMPRICE.KITAKU_GOODS_UP)  AS TOU_OUT_PRICE                                              " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_KURI_NB)                            AS TOU_KURI_NB                                                " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_KURI_QT)                            AS TOU_KURI_QT                                                " & vbNewLine _
                & " ,SUM(SUMPRICE.TOU_KURI_QT * SUMPRICE.KITAKU_GOODS_UP) AS TOU_KURI_PRICE                                             " & vbNewLine _
                & " , '" & _SYS_ENT_DATE & "'                             AS SYS_ENT_DATE                                               " & vbNewLine _
                & " , '" & _SYS_ENT_TIME & "'                             AS SYS_ENT_TIME                                               " & vbNewLine _
                & " , '" & _SYS_ENT_PGID & "'                             AS SYS_ENT_PGID                                               " & vbNewLine _
                & " , '" & _SYS_ENT_USER & "'                             AS SYS_ENT_USER                                               " & vbNewLine _
                & " , '" & _SYS_UPD_DATE & "'                             AS SYS_UPD_DATE                                               " & vbNewLine _
                & " , '" & _SYS_UPD_TIME & "'                             AS SYS_UPD_TIME                                               " & vbNewLine _
                & " , '" & _SYS_UPD_PGID & "'                             AS SYS_UPD_PGID                                               " & vbNewLine _
                & " , '" & _SYS_UPD_USER & "'                             AS SYS_UPD_USER                                               " & vbNewLine _
                & " , '" & _SYS_DEL_FLG & "'                              AS SYS_DEL_FLG                                                " & vbNewLine _
                & " FROM(                                                                                                               " & vbNewLine _
                & "     SELECT                                                                                                          " & vbNewLine _
                & "      INOUT3.NRS_BR_CD                                 AS NRS_BR_CD                                                  " & vbNewLine _
                & "     ,INOUT3.WH_CD                                     AS WH_CD                                                      " & vbNewLine _
                & "     ,INOUT3.CUST_CD_L                                 AS CUST_CD_L                                                  " & vbNewLine _
                & "     ,INOUT3.CUST_CD_M                                 AS CUST_CD_M                                                  " & vbNewLine _
                & "     ,INOUT3.TOU_NO                                    AS TOU_NO                                                     " & vbNewLine _
                & "     ,INOUT3.SITU_NO                                   AS SITU_NO                                                    " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_S,'00')                     AS CUST_CD_S                                                  " & vbNewLine _
                & "     ,ISNULL(GOODS.CUST_CD_SS,'00')                    AS CUST_CD_SS                                                 " & vbNewLine _
                & "     ,CASE RTRIM(ISNULL(GOODS.SHOBO_CD,''))                                                                          " & vbNewLine _
                & "           WHEN '' THEN 'H'                                                                                          " & vbNewLine _
                & "           ELSE 'K'                                                                                                  " & vbNewLine _
                & "           END                                         AS HANKI_KB                                                   " & vbNewLine _
                & "     ,INOUT3.ZEN_KURI_NB                               AS ZEN_KURI_NB                                                " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.ZEN_KURI_NB  AS ZEN_KURI_QT                                                " & vbNewLine _
                & "     ,INOUT3.TOU_IN_NB                                 AS TOU_IN_NB                                                  " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_IN_NB    AS TOU_IN_QT                                                  " & vbNewLine _
                & "     ,INOUT3.TOU_OUT_NB                                AS TOU_OUT_NB                                                 " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_OUT_NB   AS TOU_OUT_QT                                                 " & vbNewLine _
                & "     ,INOUT3.TOU_KURI_NB                               AS TOU_KURI_NB                                                " & vbNewLine _
                & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_KURI_NB  AS TOU_KURI_QT                                                " & vbNewLine _
                & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0)                  AS KITAKU_GOODS_UP                                            " & vbNewLine _
                & "     FROM(                                                                                                           " & vbNewLine _
                & "         	$SQL_SELECT_D_WK_INOUT3$                                                                                " & vbNewLine _
                & "     )INOUT3                                                                                                         " & vbNewLine _
                & "     LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                               " & vbNewLine _
                & "     ON  GOODS.GOODS_CD_NRS = INOUT3.GOODS_CD_NRS                                                                    " & vbNewLine _
                & "     AND GOODS.NRS_BR_CD    = INOUT3.NRS_BR_CD                                                                       " & vbNewLine _
                & "     AND GOODS.SYS_DEL_FLG = '0'                                                                                     " & vbNewLine _
                & "     WHERE                                                                                                           " & vbNewLine _
                & "         INOUT3.CUST_CD_L = CASE WHEN '" & _CUST_CD_L & "' = '' THEN INOUT3.CUST_CD_L ELSE '" & _CUST_CD_L & "' END  " & vbNewLine _
                & "     AND INOUT3.CUST_CD_M = CASE WHEN '" & _CUST_CD_M & "' = '' THEN INOUT3.CUST_CD_M ELSE '" & _CUST_CD_M & "' END  " & vbNewLine _
                & " )SUMPRICE                                                                                                           " & vbNewLine _
                & " GROUP BY                                                                                                            " & vbNewLine _
                & " SUMPRICE.NRS_BR_CD,                                                                                                 " & vbNewLine _
                & " SUMPRICE.WH_CD,                                                                                                     " & vbNewLine _
                & " SUMPRICE.CUST_CD_L,                                                                                                 " & vbNewLine _
                & " SUMPRICE.CUST_CD_M,                                                                                                 " & vbNewLine _
                & " SUMPRICE.CUST_CD_S,                                                                                                 " & vbNewLine _
                & " SUMPRICE.CUST_CD_SS,                                                                                                " & vbNewLine _
                & " SUMPRICE.TOU_NO,                                                                                                    " & vbNewLine _
                & " SUMPRICE.SITU_NO,                                                                                                   " & vbNewLine _
                & " SUMPRICE.HANKI_KB                                                                                                   " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_SELECT_D_WK_INOUT4_DATA_4"
    Private Function SQL_SELECT_D_WK_INOUT4_DATA_4() As String
        Dim sql As String = "" & vbNewLine _
            & " SELECT                                                                                                        " & vbNewLine _
            & "  SUMPRICE.NRS_BR_CD                                  AS NRS_BR_CD                                             " & vbNewLine _
            & " ,SUMPRICE.WH_CD                                      AS WH_CD                                                 " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_L                                  AS CUST_CD_L                                             " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_M                                  AS CUST_CD_M                                             " & vbNewLine _
            & " ,SUMPRICE.TOU_NO                                     AS TOU_NO                                                " & vbNewLine _
            & " ,SUMPRICE.SITU_NO                                    AS SITU_NO                                               " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_S                                  AS CUST_CD_S                                             " & vbNewLine _
            & " ,SUMPRICE.CUST_CD_SS                                 AS CUST_CD_SS                                            " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_NB)                           AS ZEN_KURI_NB                                           " & vbNewLine _
            & " ,SUM(SUMPRICE.ZEN_KURI_QT)                           AS ZEN_KURI_QT                                           " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_NB)                             AS TOU_IN_NB                                             " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_IN_QT)                             AS TOU_IN_QT                                             " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_NB)                            AS TOU_OUT_NB                                            " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_OUT_QT)                            AS TOU_OUT_QT                                            " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_NB)                           AS TOU_KURI_NB                                           " & vbNewLine _
            & " ,SUM(SUMPRICE.TOU_KURI_QT)                           AS TOU_KURI_QT                                           " & vbNewLine _
            & " , '" & _SYS_ENT_DATE & "'                            AS SYS_ENT_DATE                                          " & vbNewLine _
            & " , '" & _SYS_ENT_TIME & "'                            AS SYS_ENT_TIME                                          " & vbNewLine _
            & " , '" & _SYS_ENT_PGID & "'                            AS SYS_ENT_PGID                                          " & vbNewLine _
            & " , '" & _SYS_ENT_USER & "'                            AS SYS_ENT_USER                                          " & vbNewLine _
            & " , '" & _SYS_UPD_DATE & "'                            AS SYS_UPD_DATE                                          " & vbNewLine _
            & " , '" & _SYS_UPD_TIME & "'                            AS SYS_UPD_TIME                                          " & vbNewLine _
            & " , '" & _SYS_UPD_PGID & "'                            AS SYS_UPD_PGID                                          " & vbNewLine _
            & " , '" & _SYS_UPD_USER & "'                            AS SYS_UPD_USER                                          " & vbNewLine _
            & " , '" & _SYS_DEL_FLG & "'                             AS SYS_DEL_FLG                                           " & vbNewLine _
            & " FROM(                                                                                                         " & vbNewLine _
            & "     SELECT                                                                                                    " & vbNewLine _
            & "      INOUT3.NRS_BR_CD                                AS NRS_BR_CD                                             " & vbNewLine _
            & "     ,INOUT3.WH_CD                                    AS WH_CD                                                 " & vbNewLine _
            & "     ,INOUT3.CUST_CD_L                                AS CUST_CD_L                                             " & vbNewLine _
            & "     ,INOUT3.CUST_CD_M                                AS CUST_CD_M                                             " & vbNewLine _
            & "     ,INOUT3.TOU_NO                                   AS TOU_NO                                                " & vbNewLine _
            & "     ,INOUT3.SITU_NO                                  AS SITU_NO                                               " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_S,'00')                    AS CUST_CD_S                                             " & vbNewLine _
            & "     ,ISNULL(GOODS.CUST_CD_SS,'00')                   AS CUST_CD_SS                                            " & vbNewLine _
            & "     ,INOUT3.ZEN_KURI_NB                              AS ZEN_KURI_NB                                           " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.ZEN_KURI_NB AS ZEN_KURI_QT                                           " & vbNewLine _
            & "     ,INOUT3.TOU_IN_NB                                AS TOU_IN_NB                                             " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_IN_NB   AS TOU_IN_QT                                             " & vbNewLine _
            & "     ,INOUT3.TOU_OUT_NB                               AS TOU_OUT_NB                                            " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_OUT_NB  AS TOU_OUT_QT                                            " & vbNewLine _
            & "     ,INOUT3.TOU_KURI_NB                              AS TOU_KURI_NB                                           " & vbNewLine _
            & "     ,ISNULL(GOODS.STD_WT_KGS,0) * INOUT3.TOU_KURI_NB AS TOU_KURI_QT                                           " & vbNewLine _
            & "     ,ISNULL(GOODS.KITAKU_GOODS_UP,0)                 AS KITAKU_GOODS_UP                                       " & vbNewLine _
            & "     FROM(                                                                                                     " & vbNewLine _
            & "          $SQL_SELECT_D_WK_INOUT3$                                                                             " & vbNewLine _
            & "     )INOUT3                                                                                                   " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_GOODS GOODS                                                                         " & vbNewLine _
            & "     ON  GOODS.GOODS_CD_NRS = INOUT3.GOODS_CD_NRS                                                              " & vbNewLine _
            & "     AND GOODS.NRS_BR_CD    = INOUT3.NRS_BR_CD                                                                 " & vbNewLine _
            & "     AND GOODS.SYS_DEL_FLG  = '0'                                                                              " & vbNewLine _
            & "     WHERE                                                                                                     " & vbNewLine _
            & "         INOUT3.CUST_CD_L = '" & _CUST_CD_L & "'                                                               " & vbNewLine _
            & "     AND INOUT3.CUST_CD_M = '" & _CUST_CD_M & "'                                                               " & vbNewLine _
            & " )SUMPRICE                                                                                                     " & vbNewLine _
            & " GROUP BY                                                                                                      " & vbNewLine _
            & " SUMPRICE.NRS_BR_CD,                                                                                           " & vbNewLine _
            & " SUMPRICE.WH_CD,                                                                                               " & vbNewLine _
            & " SUMPRICE.CUST_CD_L,                                                                                           " & vbNewLine _
            & " SUMPRICE.CUST_CD_M,                                                                                           " & vbNewLine _
            & " SUMPRICE.CUST_CD_S,                                                                                           " & vbNewLine _
            & " SUMPRICE.CUST_CD_SS,                                                                                          " & vbNewLine _
            & " SUMPRICE.TOU_NO,                                                                                              " & vbNewLine _
            & " SUMPRICE.SITU_NO                                                                                              " & vbNewLine
        Return sql

    End Function
#End Region

#End Region

#Region "INSERT_D_WK_INOUT4_DATA"

#Region "INSERT_D_WK_INOUT4_DATA_1"
    Private Function SQL_INSERT_D_WK_INOUT4_DATA_1() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4    " & vbNewLine _
            & "     (NRS_BR_CD                           " & vbNewLine _
            & "     ,WH_CD                               " & vbNewLine _
            & "     ,CUST_CD_L                           " & vbNewLine _
            & "     ,CUST_CD_M                           " & vbNewLine _
            & "     ,CUST_CD_S                           " & vbNewLine _
            & "     ,CUST_CD_SS                          " & vbNewLine _
            & "     ,HANKI_KB                            " & vbNewLine _
            & "     ,ZEN_KURI_NB                         " & vbNewLine _
            & "     ,ZEN_KURI_QT                         " & vbNewLine _
            & "     ,ZEN_KURI_PRICE                      " & vbNewLine _
            & "     ,TOU_IN_NB                           " & vbNewLine _
            & "     ,TOU_IN_QT                           " & vbNewLine _
            & "     ,TOU_IN_PRICE                        " & vbNewLine _
            & "     ,TOU_OUT_NB                          " & vbNewLine _
            & "     ,TOU_OUT_QT                          " & vbNewLine _
            & "     ,TOU_OUT_PRICE                       " & vbNewLine _
            & "     ,TOU_KURI_NB                         " & vbNewLine _
            & "     ,TOU_KURI_QT                         " & vbNewLine _
            & "     ,TOU_KURI_PRICE                      " & vbNewLine _
            & "     ,SYS_ENT_DATE                        " & vbNewLine _
            & "     ,SYS_ENT_TIME                        " & vbNewLine _
            & "     ,SYS_ENT_PGID                        " & vbNewLine _
            & "     ,SYS_ENT_USER                        " & vbNewLine _
            & "     ,SYS_UPD_DATE                        " & vbNewLine _
            & "     ,SYS_UPD_TIME                        " & vbNewLine _
            & "     ,SYS_UPD_PGID                        " & vbNewLine _
            & "     ,SYS_UPD_USER                        " & vbNewLine _
            & "     ,SYS_DEL_FLG)                        " & vbNewLine _
            & "                                          " & vbNewLine
        Return sql

    End Function
#End Region

#If True Then   'ADD 2020/10/29　新 016063
#Region "INSERT_D_WK_INOUT4_DATA"

#Region "INSERT_D_WK_INOUT4A_DATA_1"
    Private Function SQL_INSERT_D_WK_INOUT4A_DATA_1() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4A    " & vbNewLine _
            & "     (NRS_BR_CD                           " & vbNewLine _
            & "     ,WH_CD                               " & vbNewLine _
            & "     ,CUST_CD_L                           " & vbNewLine _
            & "     ,CUST_CD_M                           " & vbNewLine _
            & "     ,CUST_CD_S                           " & vbNewLine _
            & "     ,CUST_CD_SS                          " & vbNewLine _
            & "     ,GOODS_CD_NRS                        " & vbNewLine _
            & "     ,GOODS_NM_1                          " & vbNewLine _
            & "     ,SHOBOKIKEN_KB                       " & vbNewLine _
            & "     ,SHOBOKIKEN_NM                       " & vbNewLine _
            & "     ,HANKI_KB                            " & vbNewLine _
            & "     ,ZEN_KURI_NB                         " & vbNewLine _
            & "     ,ZEN_KURI_QT                         " & vbNewLine _
            & "     ,ZEN_KURI_PRICE                      " & vbNewLine _
            & "     ,TOU_IN_NB                           " & vbNewLine _
            & "     ,TOU_IN_QT                           " & vbNewLine _
            & "     ,TOU_IN_PRICE                        " & vbNewLine _
            & "     ,TOU_OUT_NB                          " & vbNewLine _
            & "     ,TOU_OUT_QT                          " & vbNewLine _
            & "     ,TOU_OUT_PRICE                       " & vbNewLine _
            & "     ,TOU_KURI_NB                         " & vbNewLine _
            & "     ,TOU_KURI_QT                         " & vbNewLine _
            & "     ,TOU_KURI_PRICE                      " & vbNewLine _
            & "     ,SYS_ENT_DATE                        " & vbNewLine _
            & "     ,SYS_ENT_TIME                        " & vbNewLine _
            & "     ,SYS_ENT_PGID                        " & vbNewLine _
            & "     ,SYS_ENT_USER                        " & vbNewLine _
            & "     ,SYS_UPD_DATE                        " & vbNewLine _
            & "     ,SYS_UPD_TIME                        " & vbNewLine _
            & "     ,SYS_UPD_PGID                        " & vbNewLine _
            & "     ,SYS_UPD_USER                        " & vbNewLine _
            & "     ,SYS_DEL_FLG)                        " & vbNewLine _
            & "                                          " & vbNewLine
        Return sql

    End Function
#End Region
#End Region
#End If
#Region "INSERT_D_WK_INOUT4_DATA_2"
    Private Function SQL_INSERT_D_WK_INOUT4_DATA_2() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4    " & vbNewLine _
            & "     (NRS_BR_CD                           " & vbNewLine _
            & "     ,WH_CD                               " & vbNewLine _
            & "     ,CUST_CD_L                           " & vbNewLine _
            & "     ,CUST_CD_M                           " & vbNewLine _
            & "     ,CUST_CD_S                           " & vbNewLine _
            & "     ,CUST_CD_SS                          " & vbNewLine _
            & "     ,ZEN_KURI_NB                         " & vbNewLine _
            & "     ,ZEN_KURI_QT                         " & vbNewLine _
            & "     ,TOU_IN_NB                           " & vbNewLine _
            & "     ,TOU_IN_QT                           " & vbNewLine _
            & "     ,TOU_OUT_NB                          " & vbNewLine _
            & "     ,TOU_OUT_QT                          " & vbNewLine _
            & "     ,TOU_KURI_NB                         " & vbNewLine _
            & "     ,TOU_KURI_QT                         " & vbNewLine _
            & "     ,SYS_ENT_DATE                        " & vbNewLine _
            & "     ,SYS_ENT_TIME                        " & vbNewLine _
            & "     ,SYS_ENT_PGID                        " & vbNewLine _
            & "     ,SYS_ENT_USER                        " & vbNewLine _
            & "     ,SYS_UPD_DATE                        " & vbNewLine _
            & "     ,SYS_UPD_TIME                        " & vbNewLine _
            & "     ,SYS_UPD_PGID                        " & vbNewLine _
            & "     ,SYS_UPD_USER                        " & vbNewLine _
            & "     ,SYS_DEL_FLG)                        " & vbNewLine _
        & "                                              " & vbNewLine
        Return sql

    End Function
#End Region

#Region "INSERT_D_WK_INOUT4_DATA_3"
    Private Function SQL_INSERT_D_WK_INOUT4_DATA_3() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4    " & vbNewLine _
            & "     (NRS_BR_CD                           " & vbNewLine _
            & "     ,WH_CD                               " & vbNewLine _
            & "     ,CUST_CD_L                           " & vbNewLine _
            & "     ,CUST_CD_M                           " & vbNewLine _
            & "     ,TOU_NO                              " & vbNewLine _
            & "     ,SITU_NO                             " & vbNewLine _
            & "     ,CUST_CD_S                           " & vbNewLine _
            & "     ,CUST_CD_SS                          " & vbNewLine _
            & "     ,HANKI_KB                            " & vbNewLine _
            & "     ,ZEN_KURI_NB                         " & vbNewLine _
            & "     ,ZEN_KURI_QT                         " & vbNewLine _
            & "     ,ZEN_KURI_PRICE                      " & vbNewLine _
            & "     ,TOU_IN_NB                           " & vbNewLine _
            & "     ,TOU_IN_QT                           " & vbNewLine _
            & "     ,TOU_IN_PRICE                        " & vbNewLine _
            & "     ,TOU_OUT_NB                          " & vbNewLine _
            & "     ,TOU_OUT_QT                          " & vbNewLine _
            & "     ,TOU_OUT_PRICE                       " & vbNewLine _
            & "     ,TOU_KURI_NB                         " & vbNewLine _
            & "     ,TOU_KURI_QT                         " & vbNewLine _
            & "     ,TOU_KURI_PRICE                      " & vbNewLine _
            & "     ,SYS_ENT_DATE                        " & vbNewLine _
            & "     ,SYS_ENT_TIME                        " & vbNewLine _
            & "     ,SYS_ENT_PGID                        " & vbNewLine _
            & "     ,SYS_ENT_USER                        " & vbNewLine _
            & "     ,SYS_UPD_DATE                        " & vbNewLine _
            & "     ,SYS_UPD_TIME                        " & vbNewLine _
            & "     ,SYS_UPD_PGID                        " & vbNewLine _
            & "     ,SYS_UPD_USER                        " & vbNewLine _
            & "     ,SYS_DEL_FLG)                        " & vbNewLine _
            & "                                          " & vbNewLine
        Return sql

    End Function
#End Region

#Region "INSERT_D_WK_INOUT4_DATA_4"
    Private Function SQL_INSERT_D_WK_INOUT4_DATA_4() As String
        Dim sql As String = "" & vbNewLine _
            & "     INSERT INTO $LM_TRN$..D_WK_INOUT4    " & vbNewLine _
            & "     (NRS_BR_CD                           " & vbNewLine _
            & "     ,WH_CD                               " & vbNewLine _
            & "     ,CUST_CD_L                           " & vbNewLine _
            & "     ,CUST_CD_M                           " & vbNewLine _
            & "     ,TOU_NO                              " & vbNewLine _
            & "     ,SITU_NO                             " & vbNewLine _
            & "     ,CUST_CD_S                           " & vbNewLine _
            & "     ,CUST_CD_SS                          " & vbNewLine _
            & "     ,ZEN_KURI_NB                         " & vbNewLine _
            & "     ,ZEN_KURI_QT                         " & vbNewLine _
            & "     ,TOU_IN_NB                           " & vbNewLine _
            & "     ,TOU_IN_QT                           " & vbNewLine _
            & "     ,TOU_OUT_NB                          " & vbNewLine _
            & "     ,TOU_OUT_QT                          " & vbNewLine _
            & "     ,TOU_KURI_NB                         " & vbNewLine _
            & "     ,TOU_KURI_QT                         " & vbNewLine _
            & "     ,SYS_ENT_DATE                        " & vbNewLine _
            & "     ,SYS_ENT_TIME                        " & vbNewLine _
            & "     ,SYS_ENT_PGID                        " & vbNewLine _
            & "     ,SYS_ENT_USER                        " & vbNewLine _
            & "     ,SYS_UPD_DATE                        " & vbNewLine _
            & "     ,SYS_UPD_TIME                        " & vbNewLine _
            & "     ,SYS_UPD_PGID                        " & vbNewLine _
            & "     ,SYS_UPD_USER                        " & vbNewLine _
            & "     ,SYS_DEL_FLG)                        " & vbNewLine _
            & "                                          " & vbNewLine
        Return sql

    End Function
#End Region



#End Region



#End Region

#Region "検索処理 SQL"

#Region "SQL_Select_D_WK_INOUT5_1"
    Private Function SQL_Select_D_WK_INOUT5_1() As String
        Dim sql As String = "" & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_FROM & "',1,4) + '/' +                                                                         " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_FROM & "',5,2) + '/' +                                                                        " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_FROM & "',7,2) AS FROM_DATE                                                                   " & vbNewLine _
            & "     ,SUBSTRING('" & _PRINT_TO & "',1,4) + '/' +                                                                          " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_TO & "',5,2) + '/' +                                                                          " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_TO & "',7,2) AS TO_DATE                                                                       " & vbNewLine _
            & "     ,TOTAL.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,TOTAL.NRS_BR_NM                                                                                                " & vbNewLine _
            & "     ,TOTAL.WH_CD                                                                                                    " & vbNewLine _
            & "     ,TOTAL.WH_NM                                                                                                    " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L + '-' + TOTAL.CUST_CD_M AS KITAKU_CODE                                                         " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L + ' ' + TOTAL.CUST_NM_M AS KITAKU_NM                                                           " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,CASE TOTAL.HANKI_KB                                                                                            " & vbNewLine _
            & "      WHEN 'H' THEN '一般品'                                                                                         " & vbNewLine _
            & "      WHEN 'K' THEN '危険品'                                                                                         " & vbNewLine _
            & "      ELSE '' END AS HANKI_NM                                                                                        " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_PRICE                                                                                           " & vbNewLine _
            & "     ,TOTAL.TOU_IN_NB                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_QT                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_PRICE                                                                                             " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_NB                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_QT                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_PRICE                                                                                            " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_PRICE                                                                                           " & vbNewLine _
            & "     ,CASE TOTAL.KURI_QT_KEI                                                                                         " & vbNewLine _
            & "        WHEN '0' THEN '0'                                                                                            " & vbNewLine _
            & "        ELSE ((TOTAL.TOU_IN_QT + TOTAL.TOU_OUT_QT) / TOTAL.KURI_QT_KEI) * 100                                        " & vbNewLine _
            & "      END AS KAITEN_RITSU                                                                                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(NRS.NRS_BR_NM,'')) AS NRS_BR_NM                                                                     " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,MAX(ISNULL(SOKO.WH_NM,'')) AS WH_NM                                                                            " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_L,'')) AS CUST_NM_L                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_M,'')) AS CUST_NM_M                                                                    " & vbNewLine _
            & "     ,'' AS CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,'' AS CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,CDCHG.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_PRICE) AS ZEN_KURI_PRICE                                                                    " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_PRICE) AS TOU_IN_PRICE                                                                        " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_PRICE) AS TOU_OUT_PRICE                                                                      " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_PRICE) AS TOU_KURI_PRICE                                                                    " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) + SUM(CDCHG.TOU_KURI_QT) AS KURI_QT_KEI                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT4 AS INOUT4                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )CDCHG                                                                                                          " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_NRS_BR NRS ON                                                                             " & vbNewLine _
            & "     NRS.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                                                              " & vbNewLine _
            & "     SOKO.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SOKO.WH_CD = CDCHG.WH_CD                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_CUST CUST ON                                                                              " & vbNewLine _
            & "     CUST.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND CUST.CUST_CD_L = CDCHG.CUST_CD_L                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_M = CDCHG.CUST_CD_M                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_S = CDCHG.CUST_CD_S                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_SS = CDCHG.CUST_CD_SS                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,CDCHG.HANKI_KB                                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )TOTAL                                                                                                          " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     NOT(TOTAL.ZEN_KURI_NB = 0 AND TOTAL.TOU_IN_NB = 0 AND TOTAL.TOU_OUT_NB = 0 AND TOTAL.TOU_KURI_NB = 0)           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ORDER BY                                                                                                        " & vbNewLine _
            & "      FROM_DATE                                                                                                      " & vbNewLine _
            & "     ,TO_DATE                                                                                                        " & vbNewLine _
            & "     ,NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
            & "     ,HANKI_KB                                                                                                       " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Select_D_WK_INOUT5_2"
    Private Function SQL_Select_D_WK_INOUT5_2() As String
        Dim sql As String = "" & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     left('" & _PRINT_FROM & "',4) + '年' + substring('" & _PRINT_FROM & "',5,2) + '月分' AS PRINT_NENTSUKI                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ,TOTAL.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,TOTAL.NRS_BR_NM                                                                                                " & vbNewLine _
            & "     ,TOTAL.WH_CD                                                                                                    " & vbNewLine _
            & "     ,TOTAL.WH_NM                                                                                                    " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L + '-' + TOTAL.CUST_CD_M AS CUST_CD_LM                                                          " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L + ' ' + TOTAL.CUST_NM_M AS CUST_NM_LM                                                          " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_IN_NB                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_QT                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_NB                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_QT                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_QT                                                                                              " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(NRS.NRS_BR_NM,'')) AS NRS_BR_NM                                                                     " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,MAX(ISNULL(SOKO.WH_NM,'')) AS WH_NM                                                                            " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_L,'')) AS CUST_NM_L                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_M,'')) AS CUST_NM_M                                                                    " & vbNewLine _
            & "     ,'' AS CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,'' AS CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT4 AS INOUT4                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )CDCHG                                                                                                          " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_NRS_BR NRS ON                                                                             " & vbNewLine _
            & "     NRS.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                                                              " & vbNewLine _
            & "     SOKO.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SOKO.WH_CD = CDCHG.WH_CD                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_CUST CUST ON                                                                              " & vbNewLine _
            & "     CUST.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND CUST.CUST_CD_L = CDCHG.CUST_CD_L                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_M = CDCHG.CUST_CD_M                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_S = CDCHG.CUST_CD_S                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_SS = CDCHG.CUST_CD_SS                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )TOTAL                                                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     NOT(TOTAL.ZEN_KURI_NB = 0 AND TOTAL.TOU_IN_NB = 0 AND TOTAL.TOU_OUT_NB = 0 AND TOTAL.TOU_KURI_NB = 0)           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ORDER BY                                                                                                        " & vbNewLine _
            & "     PRINT_NENTSUKI                                                                                                  " & vbNewLine _
            & "     ,NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Select_D_WK_INOUT5_3"
    Private Function SQL_Select_D_WK_INOUT5_3() As String
        Dim sql As String = "" & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_FROM & "',1,4) +                                                                               " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_FROM & "',5,2) +                                                                               " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_FROM & "',7,2) AS FROM_DATE                                                                    " & vbNewLine _
            & "     ,SUBSTRING('" & _PRINT_TO & "',1,4) +                                                                                " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_TO & "',5,2) +                                                                                 " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_TO & "',7,2) AS TO_DATE                                                                        " & vbNewLine _
            & "     ,TOTAL.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,TOTAL.NRS_BR_NM                                                                                                " & vbNewLine _
            & "     ,TOTAL.WH_CD                                                                                                    " & vbNewLine _
            & "     ,TOTAL.WH_NM                                                                                                    " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L + '-' + TOTAL.CUST_CD_M AS KITAKU_CODE                                                         " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L + ' ' + TOTAL.CUST_NM_M AS KITAKU_NM                                                           " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,TOTAL.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,TOTAL.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,CASE TOTAL.HANKI_KB                                                                                            " & vbNewLine _
            & "     WHEN 'H' THEN '一般品'                                                                                          " & vbNewLine _
            & "     WHEN 'K' THEN '危険品'                                                                                          " & vbNewLine _
            & "     ELSE '' END AS HANKI_NM                                                                                         " & vbNewLine _
            & "     ,ISNULL(TOTAL.ZEN_KURI_NB,0) AS ZEN_KURI_NB                                                                     " & vbNewLine _
            & "     ,ISNULL(TOTAL.ZEN_KURI_QT,0) AS ZEN_KURI_QT                                                                     " & vbNewLine _
            & "     ,ISNULL(TOTAL.ZEN_KURI_PRICE,0) AS ZEN_KURI_PRICE                                                               " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_IN_NB,0) AS TOU_IN_NB                                                                         " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_IN_QT,0) AS TOU_IN_QT                                                                         " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_IN_PRICE,0) AS TOU_IN_PRICE                                                                   " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_OUT_NB,0) AS TOU_OUT_NB                                                                       " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_OUT_QT,0) AS TOU_OUT_QT                                                                       " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_OUT_PRICE,0) AS TOU_OUT_PRICE                                                                 " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_KURI_NB,0) AS TOU_KURI_NB                                                                     " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_KURI_QT,0) AS TOU_KURI_QT                                                                     " & vbNewLine _
            & "     ,ISNULL(TOTAL.TOU_KURI_PRICE,0) AS TOU_KURI_PRICE                                                               " & vbNewLine _
            & "     ,ISNULL(CASE TOTAL.KURI_QT_KEI                                                                                  " & vbNewLine _
            & "     WHEN '0' THEN '0'                                                                                               " & vbNewLine _
            & "     ELSE ((TOTAL.TOU_IN_QT + TOTAL.TOU_OUT_QT) / TOTAL.KURI_QT_KEI) * 100                                           " & vbNewLine _
            & "     END,0) AS KAITEN_RITSU                                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     CDCHG.NRS_BR_CD                                                                                                 " & vbNewLine _
            & "     ,MAX(ISNULL(NRS.NRS_BR_NM,'')) AS NRS_BR_NM                                                                     " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,MAX(ISNULL(SOKO.WH_NM,'')) AS WH_NM                                                                            " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_L,'')) AS CUST_NM_L                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_M,'')) AS CUST_NM_M                                                                    " & vbNewLine _
            & "     ,'' AS CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,'' AS CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,CDCHG.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,CDCHG.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,CDCHG.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_PRICE) AS ZEN_KURI_PRICE                                                                    " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_PRICE) AS TOU_IN_PRICE                                                                        " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_PRICE) AS TOU_OUT_PRICE                                                                      " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_PRICE) AS TOU_KURI_PRICE                                                                    " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) + SUM(CDCHG.TOU_KURI_QT) AS KURI_QT_KEI                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     *                                                                                                               " & vbNewLine _
            & "     FROM $LM_TRN$..D_WK_INOUT4 AS INOUT4                                                                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )CDCHG                                                                                                          " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_NRS_BR NRS ON                                                                             " & vbNewLine _
            & "     NRS.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                                                              " & vbNewLine _
            & "     SOKO.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SOKO.WH_CD = CDCHG.WH_CD                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_CUST CUST ON                                                                              " & vbNewLine _
            & "     CUST.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND CUST.CUST_CD_L = CDCHG.CUST_CD_L                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_M = CDCHG.CUST_CD_M                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_S = CDCHG.CUST_CD_S                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_SS = CDCHG.CUST_CD_SS                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "     CDCHG.NRS_BR_CD                                                                                                 " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,CDCHG.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,CDCHG.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,CDCHG.HANKI_KB                                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )TOTAL                                                                                                          " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     NOT(TOTAL.ZEN_KURI_NB = 0 AND TOTAL.TOU_IN_NB = 0 AND TOTAL.TOU_OUT_NB = 0 AND TOTAL.TOU_KURI_NB = 0)           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ORDER BY                                                                                                        " & vbNewLine _
            & "     FROM_DATE                                                                                                       " & vbNewLine _
            & "     ,TO_DATE                                                                                                        " & vbNewLine _
            & "     ,NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,TOU_NO                                                                                                         " & vbNewLine _
            & "     ,SITU_NO                                                                                                        " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
            & "     ,HANKI_KB                                                                                                       " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Select_D_WK_INOUT5_4"
    Private Function SQL_Select_D_WK_INOUT5_4() As String
        Dim sql As String = "" & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     left('" & _PRINT_FROM & "',4) + '年' + substring('" & _PRINT_FROM & "',5,2) + '月分' AS PRINT_NENTSUKI                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ,TOTAL.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,TOTAL.NRS_BR_NM                                                                                                " & vbNewLine _
            & "     ,TOTAL.WH_CD                                                                                                    " & vbNewLine _
            & "     ,TOTAL.WH_NM                                                                                                    " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L + '-' + TOTAL.CUST_CD_M AS CUST_CD_LM                                                          " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L + ' ' + TOTAL.CUST_NM_M AS CUST_NM_LM                                                          " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,TOTAL.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,SITU.TOU_SITU_NM                                                                                               " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_IN_NB                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_QT                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_NB                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_QT                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_QT                                                                                              " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(NRS.NRS_BR_NM,'')) AS NRS_BR_NM                                                                     " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,MAX(ISNULL(SOKO.WH_NM,'')) AS WH_NM                                                                            " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_L,'')) AS CUST_NM_L                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_M,'')) AS CUST_NM_M                                                                    " & vbNewLine _
            & "     ,'' AS CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,'' AS CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,CDCHG.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,CDCHG.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT4 AS INOUT4                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )CDCHG                                                                                                          " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_NRS_BR NRS ON                                                                             " & vbNewLine _
            & "     NRS.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                                                              " & vbNewLine _
            & "     SOKO.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SOKO.WH_CD = CDCHG.WH_CD                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_CUST CUST ON                                                                              " & vbNewLine _
            & "     CUST.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND CUST.CUST_CD_L = CDCHG.CUST_CD_L                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_M = CDCHG.CUST_CD_M                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_S = CDCHG.CUST_CD_S                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_SS = CDCHG.CUST_CD_SS                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,CDCHG.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,CDCHG.SITU_NO                                                                                                  " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )TOTAL                                                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_TOU_SITU SITU ON                                                                          " & vbNewLine _
            & "     SITU.NRS_BR_CD = TOTAL.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SITU.WH_CD = TOTAL.WH_CD                                                                                    " & vbNewLine _
            & "     AND SITU.TOU_NO = TOTAL.TOU_NO                                                                                  " & vbNewLine _
            & "     AND SITU.SITU_NO = TOTAL.SITU_NO                                                                                " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     NOT(TOTAL.ZEN_KURI_NB = 0 AND TOTAL.TOU_IN_NB = 0 AND TOTAL.TOU_OUT_NB = 0 AND TOTAL.TOU_KURI_NB = 0)           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ORDER BY                                                                                                        " & vbNewLine _
            & "     PRINT_NENTSUKI                                                                                                  " & vbNewLine _
            & "     ,NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,TOU_NO                                                                                                         " & vbNewLine _
            & "     ,SITU_NO                                                                                                        " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine
        Return sql

    End Function
#End Region

#Region "SQL_Select_D_WK_INOUT5_5"
    Private Function SQL_Select_D_WK_INOUT5_5() As String
        Dim sql As String = "" & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     left('" & _PRINT_FROM & "',4) + '年' + substring('" & _PRINT_FROM & "',5,2) + '月分' AS PRINT_NENTSUKI                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ,TOTAL.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,TOTAL.NRS_BR_NM                                                                                                " & vbNewLine _
            & "     ,TOTAL.WH_CD                                                                                                    " & vbNewLine _
            & "     ,TOTAL.WH_NM                                                                                                    " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L + '-' + TOTAL.CUST_CD_M AS CUST_CD_LM                                                          " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L + ' ' + TOTAL.CUST_NM_M AS CUST_NM_LM                                                          " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,TOTAL.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,SITU.TOU_SITU_NM                                                                                               " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_IN_NB                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_QT                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_NB                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_QT                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_NO    AS SYS_PAGE_KEY                        -- SYS_PAGE_KEY(改Sheet条件)                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(NRS.NRS_BR_NM,'')) AS NRS_BR_NM                                                                     " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,MAX(ISNULL(SOKO.WH_NM,'')) AS WH_NM                                                                            " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_L,'')) AS CUST_NM_L                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_M,'')) AS CUST_NM_M                                                                    " & vbNewLine _
            & "     ,'' AS CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,'' AS CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,CDCHG.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,CDCHG.SITU_NO                                                                                                  " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT4 AS INOUT4                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )CDCHG                                                                                                          " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_NRS_BR NRS ON                                                                             " & vbNewLine _
            & "     NRS.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                                                              " & vbNewLine _
            & "     SOKO.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SOKO.WH_CD = CDCHG.WH_CD                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_CUST CUST ON                                                                              " & vbNewLine _
            & "     CUST.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND CUST.CUST_CD_L = CDCHG.CUST_CD_L                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_M = CDCHG.CUST_CD_M                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_S = CDCHG.CUST_CD_S                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_SS = CDCHG.CUST_CD_SS                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,CDCHG.TOU_NO                                                                                                   " & vbNewLine _
            & "     ,CDCHG.SITU_NO                                                                                                  " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )TOTAL                                                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_TOU_SITU SITU ON                                                                          " & vbNewLine _
            & "     SITU.NRS_BR_CD = TOTAL.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SITU.WH_CD = TOTAL.WH_CD                                                                                    " & vbNewLine _
            & "     AND SITU.TOU_NO = TOTAL.TOU_NO                                                                                  " & vbNewLine _
            & "     AND SITU.SITU_NO = TOTAL.SITU_NO                                                                                " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     NOT(TOTAL.ZEN_KURI_NB = 0 AND TOTAL.TOU_IN_NB = 0 AND TOTAL.TOU_OUT_NB = 0 AND TOTAL.TOU_KURI_NB = 0)           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ORDER BY                                                                                                        " & vbNewLine _
            & "     PRINT_NENTSUKI                                                                                                  " & vbNewLine _
            & "     ,NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,TOU_NO                                                                                                         " & vbNewLine _
            & "     ,SITU_NO                                                                                                        " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine
        Return sql

    End Function

#If True Then 'ADD 2020/10/29 新 016063
#Region "SQL_Select_D_WK_INOUT5_6"
    Private Function SQL_Select_D_WK_INOUT5_6() As String
        Dim sql As String = "" & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "     SUBSTRING('" & _PRINT_FROM & "',1,4) + '/' +                                                                         " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_FROM & "',5,2) + '/' +                                                                        " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_FROM & "',7,2) AS FROM_DATE                                                                   " & vbNewLine _
            & "     ,SUBSTRING('" & _PRINT_TO & "',1,4) + '/' +                                                                          " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_TO & "',5,2) + '/' +                                                                          " & vbNewLine _
            & "      SUBSTRING('" & _PRINT_TO & "',7,2) AS TO_DATE                                                                       " & vbNewLine _
            & "     ,TOTAL.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,TOTAL.NRS_BR_NM                                                                                                " & vbNewLine _
            & "     ,TOTAL.WH_CD                                                                                                    " & vbNewLine _
            & "     ,TOTAL.WH_NM                                                                                                    " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L + '-' + TOTAL.CUST_CD_M AS KITAKU_CODE                                                         " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L + ' ' + TOTAL.CUST_NM_M AS KITAKU_NM                                                           " & vbNewLine _
            & "     ,TOTAL.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_L                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_M                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,TOTAL.CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,TOTAL.GOODS_CD_NRS                                                                                             " & vbNewLine _
            & "     ,TOTAL.GOODS_NM_1                                                                                               " & vbNewLine _
            & "     ,TOTAL.SHOBOKIKEN_KB                                                                                            " & vbNewLine _
            & "     ,TOTAL.SHOBOKIKEN_NM                                                                                            " & vbNewLine _
            & "     ,TOTAL.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,CASE TOTAL.HANKI_KB                                                                                            " & vbNewLine _
            & "      WHEN 'H' THEN '一般品'                                                                                         " & vbNewLine _
            & "      WHEN 'K' THEN '危険品'                                                                                         " & vbNewLine _
            & "      ELSE '' END AS HANKI_NM                                                                                        " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.ZEN_KURI_PRICE                                                                                           " & vbNewLine _
            & "     ,TOTAL.TOU_IN_NB                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_QT                                                                                                " & vbNewLine _
            & "     ,TOTAL.TOU_IN_PRICE                                                                                             " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_NB                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_QT                                                                                               " & vbNewLine _
            & "     ,TOTAL.TOU_OUT_PRICE                                                                                            " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_NB                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_QT                                                                                              " & vbNewLine _
            & "     ,TOTAL.TOU_KURI_PRICE                                                                                           " & vbNewLine _
            & "     ,CASE TOTAL.KURI_QT_KEI                                                                                         " & vbNewLine _
            & "        WHEN '0' THEN '0'                                                                                            " & vbNewLine _
            & "        ELSE ((TOTAL.TOU_IN_QT + TOTAL.TOU_OUT_QT) / TOTAL.KURI_QT_KEI) * 100                                        " & vbNewLine _
            & "      END AS KAITEN_RITSU                                                                                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     SELECT                                                                                                          " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(NRS.NRS_BR_NM,'')) AS NRS_BR_NM                                                                     " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,MAX(ISNULL(SOKO.WH_NM,'')) AS WH_NM                                                                            " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_L,'')) AS CUST_NM_L                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,MAX(ISNULL(CUST.CUST_NM_M,'')) AS CUST_NM_M                                                                    " & vbNewLine _
            & "     ,'' AS CUST_CD_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_NM_S                                                                                                " & vbNewLine _
            & "     ,'' AS CUST_CD_SS                                                                                               " & vbNewLine _
            & "     ,'' AS CUST_NM_SS                                                                                               " & vbNewLine _
            & "     ,CDCHG.GOODS_CD_NRS                                                                                             " & vbNewLine _
            & "     ,CDCHG.GOODS_NM_1                                                                                               " & vbNewLine _
            & "     ,CDCHG.SHOBOKIKEN_KB                                                                                            " & vbNewLine _
            & "     ,CDCHG.SHOBOKIKEN_NM                                                                                            " & vbNewLine _
            & "     ,CDCHG.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_NB) AS ZEN_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) AS ZEN_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_PRICE) AS ZEN_KURI_PRICE                                                                    " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_NB) AS TOU_IN_NB                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_QT) AS TOU_IN_QT                                                                              " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_IN_PRICE) AS TOU_IN_PRICE                                                                        " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_NB) AS TOU_OUT_NB                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_QT) AS TOU_OUT_QT                                                                            " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_OUT_PRICE) AS TOU_OUT_PRICE                                                                      " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_NB) AS TOU_KURI_NB                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_QT) AS TOU_KURI_QT                                                                          " & vbNewLine _
            & "     ,SUM(CDCHG.TOU_KURI_PRICE) AS TOU_KURI_PRICE                                                                    " & vbNewLine _
            & "     ,SUM(CDCHG.ZEN_KURI_QT) + SUM(CDCHG.TOU_KURI_QT) AS KURI_QT_KEI                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     FROM(                                                                                                           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     	SELECT                                                                                                      " & vbNewLine _
            & "     	*                                                                                                           " & vbNewLine _
            & "     	FROM $LM_TRN$..D_WK_INOUT4A AS INOUT4                                                                        " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )CDCHG                                                                                                          " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_NRS_BR NRS ON                                                                             " & vbNewLine _
            & "     NRS.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                 " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_SOKO SOKO ON                                                                              " & vbNewLine _
            & "     SOKO.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND SOKO.WH_CD = CDCHG.WH_CD                                                                                    " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..M_CUST CUST ON                                                                              " & vbNewLine _
            & "     CUST.NRS_BR_CD = CDCHG.NRS_BR_CD                                                                                " & vbNewLine _
            & "     AND CUST.CUST_CD_L = CDCHG.CUST_CD_L                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_M = CDCHG.CUST_CD_M                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_S = CDCHG.CUST_CD_S                                                                            " & vbNewLine _
            & "     AND CUST.CUST_CD_SS = CDCHG.CUST_CD_SS                                                                          " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     GROUP BY                                                                                                        " & vbNewLine _
            & "      CDCHG.NRS_BR_CD                                                                                                " & vbNewLine _
            & "     ,CDCHG.WH_CD                                                                                                    " & vbNewLine _
            & "     ,CDCHG.CUST_CD_L                                                                                                " & vbNewLine _
            & "     ,CDCHG.CUST_CD_M                                                                                                " & vbNewLine _
            & "     ,CDCHG.HANKI_KB                                                                                                 " & vbNewLine _
            & "     ,CDCHG.GOODS_CD_NRS                                                                                             " & vbNewLine _
            & "     ,CDCHG.GOODS_NM_1                                                                                               " & vbNewLine _
            & "     ,CDCHG.SHOBOKIKEN_KB                                                                                            " & vbNewLine _
            & "     ,CDCHG.SHOBOKIKEN_NM                                                                                            " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     )TOTAL                                                                                                          " & vbNewLine _
            & "     WHERE                                                                                                           " & vbNewLine _
            & "     NOT(TOTAL.ZEN_KURI_NB = 0 AND TOTAL.TOU_IN_NB = 0 AND TOTAL.TOU_OUT_NB = 0 AND TOTAL.TOU_KURI_NB = 0)           " & vbNewLine _
            & "                                                                                                                     " & vbNewLine _
            & "     ORDER BY                                                                                                        " & vbNewLine _
            & "      FROM_DATE                                                                                                      " & vbNewLine _
            & "     ,TO_DATE                                                                                                        " & vbNewLine _
            & "     ,NRS_BR_CD                                                                                                      " & vbNewLine _
            & "     ,WH_CD                                                                                                          " & vbNewLine _
            & "     ,CUST_CD_L                                                                                                      " & vbNewLine _
            & "     ,CUST_CD_M                                                                                                      " & vbNewLine _
            & "     ,HANKI_KB                                                                                                       " & vbNewLine _
            & "     ,GOODS_CD_NRS                                                                                                   " & vbNewLine _
            & "     ,GOODS_NM_1                                                                                                     " & vbNewLine _
            & "     ,SHOBOKIKEN_KB                                                                                                  " & vbNewLine _
            & "     ,SHOBOKIKEN_NM                                                                                                  " & vbNewLine


        Return sql

    End Function
#End Region
#End If
#End Region

#End Region

#Region "削除処理 SQL"

    Private Function SQL_TruncateTable_D_WK_INOUT1() As String

        Dim sql As String = "  DELETE FROM $LM_TRN$..D_WK_INOUT1  " & vbNewLine _
                                                          & "  WHERE DEL_KEY_DATE >= '" & _MONTH_AGO & "'" & vbNewLine

        Return sql

    End Function

    Private Const SQL_TruncateTable_D_WK_INOUT2 As String = "  TRUNCATE TABLE $LM_TRN$..D_WK_INOUT2 " & vbNewLine
    Private Const SQL_TruncateTable_D_WK_INOUT3 As String = "  TRUNCATE TABLE $LM_TRN$..D_WK_INOUT3 " & vbNewLine
    Private Const SQL_TruncateTable_D_WK_INOUT4 As String = "  TRUNCATE TABLE $LM_TRN$..D_WK_INOUT4 " & vbNewLine
    Private Const SQL_TruncateTable_D_WK_INOUT5 As String = "  TRUNCATE TABLE $LM_TRN$..D_WK_INOUT5 " & vbNewLine
#If True Then   'ADD 2020/10/29　新 016063
    Private Const SQL_TruncateTable_D_WK_INOUT4A As String = "  TRUNCATE TABLE $LM_TRN$..D_WK_INOUT4A " & vbNewLine
#End If

#End Region

#End Region


#Region "Method"


#Region "削除処理"

    ''' <summary>
    '''ワークテーブル(D_WK_INOUT1)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Public Function Delete_D_WK_INOUT1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Me._StrSql.Append(Me.SQL_TruncateTable_D_WK_INOUT1)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMD630DAC", "DeleteD_WK_INOUT1", cmd)

        Dim updateCnt As Integer = 0

        ''SQLの発行
        'updateCnt = MyBase.GetUpdateResult(cmd)
        'MyBase.SetResultCount(updateCnt)


        ''SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT2)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Public Function Delete_D_WK_INOUT2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD630DAC.SQL_TruncateTable_D_WK_INOUT2)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMD630DAC", "DeleteD_WK_INOUT2", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT3)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Public Function Delete_D_WK_INOUT3(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD630DAC.SQL_TruncateTable_D_WK_INOUT3)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMD630DAC", "DeleteD_WK_INOUT3", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT4)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Public Function Delete_D_WK_INOUT4(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD630DAC.SQL_TruncateTable_D_WK_INOUT4)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMD630DAC", "DeleteD_WK_INOUT4", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

#If True Then   'ADD 2020/10/29　新 016063

    ''' <summary>
    '''ワークテーブル(D_WK_INOUT4A)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Public Function Delete_D_WK_INOUT4A(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD630DAC.SQL_TruncateTable_D_WK_INOUT4A)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMD630DAC", "DeleteD_WK_INOUT4A", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function
#End If

#End Region


#Region "追加処理"

    ''' <summary>
    '''ワークテーブル(D_WK_INOUT1)追加_入荷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT1_FROM_INKA(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT1_FROM_INKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMD630DAC", "Insert_D_WK_INOUT1_FROM_INKA", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    '''ワークテーブル(D_WK_INOUT1)追加_出荷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT1_FROM_OUTKA(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT1_FROM_OUTKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        MyBase.Logger.WriteSQLLog("LMD630DAC", "Insert_D_WK_INOUT1_FROM_OUTKA", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    '''ワークテーブル(D_WK_INOUT1)追加_移動(前)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT1_FROM_IDO_BEFORE(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT1_FROM_IDO_BEFORE)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        MyBase.Logger.WriteSQLLog("LMD630DAC", "Insert_D_WK_INOUT1_FROM_IDO_BEFORE", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    '''ワークテーブル(D_WK_INOUT1)追加_移動(後)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT1_FROM_IDO_AFTER(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT1_FROM_IDO_AFTER)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        MyBase.Logger.WriteSQLLog("LMD630DAC", "Insert_D_WK_INOUT1_FROM_IDO_AFTER", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT2)追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString

            Case "01", "02", "03", "08"
                '棟室なし
                Select Case Me._Row.Item("PTN_NUM").ToString
                    Case "0"
                        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT2_1_1)
                    Case "1"
                        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT2_1_2)
                    Case "2"
                        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT2_1_3)
                End Select

            Case Else
                '棟室あり
                Select Case Me._Row.Item("PTN_NUM").ToString
                    Case "0"
                        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT2_2_1)
                    Case "1"
                        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT2_2_2)
                    Case "2"
                        Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT2_2_3)
                End Select

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'WKテーブルから全件取得する場合があるためやむなくタイムアウト処理追加
        cmd.CommandTimeout = 6000

        MyBase.Logger.WriteSQLLog("LMD630DAC", "SQL_Insert_D_WK_INOUT2", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT3)追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT3(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString

            Case "01", "02", "03", "08"
                '棟室なし
                Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT3_1)

            Case Else
                '棟室あり
                Me._StrSql.Append(Me.SQL_Insert_D_WK_INOUT3_2)

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        MyBase.Logger.WriteSQLLog("LMD630DAC", "SQL_Insert_D_WK_INOUT3", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT4)追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Insert_D_WK_INOUT4(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        'Insert句

        'D_WK_INOUT4登録データ取得SQL
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString
            Case "01", "02", "03"
                Me._StrSql.Append(Me.SQL_INSERT_D_WK_INOUT4_DATA_1)
                Me._StrSql.Append(Me.SQL_SELECT_D_WK_INOUT4_DATA_1)
            Case "08"
                Me._StrSql.Append(Me.SQL_INSERT_D_WK_INOUT4_DATA_2)
                Me._StrSql.Append(Me.SQL_SELECT_D_WK_INOUT4_DATA_2)
            Case "04", "05", "06"
                Me._StrSql.Append(Me.SQL_INSERT_D_WK_INOUT4_DATA_3)
                Me._StrSql.Append(Me.SQL_SELECT_D_WK_INOUT4_DATA_3)
            Case "07", "09"
                Me._StrSql.Append(Me.SQL_INSERT_D_WK_INOUT4_DATA_4)
                Me._StrSql.Append(Me.SQL_SELECT_D_WK_INOUT4_DATA_4)
#If True Then   'ADD 2020/10/29　新 016063
            Case "10"
                Me._StrSql.Append(Me.SQL_INSERT_D_WK_INOUT4A_DATA_1)
                Me._StrSql.Append(Me.SQL_SELECT_D_WK_INOUT4A_DATA_1)
#End If
            Case Else
        End Select

        'D_WK_INOUT3登録データ取得SQL
        Dim sqlWkInout3 As New StringBuilder
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString
            Case "01", "02", "03", "08"
                '棟室なし
                sqlWkInout3.Append(Me.SQL_SELECT_D_WK_INOUT3_DATA_1)
            Case Else
                '棟室あり
                sqlWkInout3.Append(Me.SQL_SELECT_D_WK_INOUT3_DATA_2)
        End Select

        'D_WK_INOUT2登録データ取得SQL
        Dim sqlWkInout2 As New StringBuilder
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString
            Case "01", "02", "03", "08"
                '棟室なし
                sqlWkInout2.Append(Me.SQL_SELECT_D_WK_INOUT2_DATA_1_1)
                sqlWkInout2.Append(" UNION ALL " & vbNewLine)
                sqlWkInout2.Append(Me.SQL_SELECT_D_WK_INOUT2_DATA_1_2)
                sqlWkInout2.Append(" UNION ALL " & vbNewLine)
                sqlWkInout2.Append(Me.SQL_SELECT_D_WK_INOUT2_DATA_1_3)
            Case Else
                '棟室あり
                sqlWkInout2.Append(Me.SQL_SELECT_D_WK_INOUT2_DATA_2_1)
                sqlWkInout2.Append(" UNION ALL " & vbNewLine)
                sqlWkInout2.Append(Me.SQL_SELECT_D_WK_INOUT2_DATA_2_2)
                sqlWkInout2.Append(" UNION ALL " & vbNewLine)
                sqlWkInout2.Append(Me.SQL_SELECT_D_WK_INOUT2_DATA_2_3)
        End Select

        'スキーマ名設定
        Dim sql As String = String.Empty
        sql = Me._StrSql.ToString()
        sql = sql.Replace("$SQL_SELECT_D_WK_INOUT3$", sqlWkInout3.ToString)
        sql = sql.Replace("$SQL_SELECT_D_WK_INOUT2$", sqlWkInout2.ToString)
        sql = Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        cmd.CommandTimeout = 6000

        MyBase.Logger.WriteSQLLog("LMD630DAC", "SQL_Insert_D_WK_INOUT4", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)


        Return ds

    End Function


    ''' <summary>
    '''ワークテーブル(D_WK_INOUT5)追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function Select_D_WK_INOUT5(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD630IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '条件設定
        Call Me.SetSQLWhereData()

        'SQL作成
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString

            Case "01", "02", "03"

                Me._StrSql.Append(Me.SQL_Select_D_WK_INOUT5_1)

            Case "08"

                Me._StrSql.Append(Me.SQL_Select_D_WK_INOUT5_2)

            Case "06", "05", "04"

                Me._StrSql.Append(Me.SQL_Select_D_WK_INOUT5_3)

            Case "07"

                Me._StrSql.Append(Me.SQL_Select_D_WK_INOUT5_4)

            Case "09"

                Me._StrSql.Append(Me.SQL_Select_D_WK_INOUT5_5)

#If True Then 'ADD 2020/10/29 新 016063
            Case "10"
                Me._StrSql.Append(Me.SQL_Select_D_WK_INOUT5_6)
            Case Else

#End If

        End Select
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        MyBase.Logger.WriteSQLLog("LMD630DAC", "SQL_Insert_D_WK_INOUT5", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        Select Case Me._Row.Item("PRINT_SUB_FLAG").ToString

            Case "01", "02", "03"

                map.Add("FROM_DATE", "FROM_DATE")
                map.Add("TO_DATE", "TO_DATE")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("NRS_BR_NM", "NRS_BR_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("KITAKU_CODE", "KITAKU_CODE")
                map.Add("KITAKU_NM", "KITAKU_NM")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_NM_L", "CUST_NM_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_NM_M", "CUST_NM_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_NM_S", "CUST_NM_S")
                map.Add("CUST_CD_SS", "CUST_CD_SS")
                map.Add("CUST_NM_SS", "CUST_NM_SS")
                map.Add("HANKI_KB", "HANKI_KB")
                map.Add("HANKI_NM", "HANKI_NM")
                map.Add("ZEN_KURI_NB", "ZEN_KURI_NB")
                map.Add("ZEN_KURI_QT", "ZEN_KURI_QT")
                map.Add("ZEN_KURI_PRICE", "ZEN_KURI_PRICE")
                map.Add("TOU_IN_NB", "TOU_IN_NB")
                map.Add("TOU_IN_QT", "TOU_IN_QT")
                map.Add("TOU_IN_PRICE", "TOU_IN_PRICE")
                map.Add("TOU_OUT_NB", "TOU_OUT_NB")
                map.Add("TOU_OUT_QT", "TOU_OUT_QT")
                map.Add("TOU_OUT_PRICE", "TOU_OUT_PRICE")
                map.Add("TOU_KURI_NB", "TOU_KURI_NB")
                map.Add("TOU_KURI_QT", "TOU_KURI_QT")
                map.Add("TOU_KURI_PRICE", "TOU_KURI_PRICE")
                map.Add("KAITEN_RITSU", "KAITEN_RITSU")

            Case "08"

                map.Add("PRINT_NENTSUKI", "PRINT_NENTSUKI")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("NRS_BR_NM", "NRS_BR_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("CUST_CD_LM", "CUST_CD_LM")
                map.Add("CUST_NM_LM", "CUST_NM_LM")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_NM_L", "CUST_NM_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_NM_M", "CUST_NM_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_NM_S", "CUST_NM_S")
                map.Add("CUST_CD_SS", "CUST_CD_SS")
                map.Add("CUST_NM_SS", "CUST_NM_SS")
                map.Add("ZEN_KURI_NB", "ZEN_KURI_NB")
                map.Add("ZEN_KURI_QT", "ZEN_KURI_QT")
                map.Add("TOU_IN_NB", "TOU_IN_NB")
                map.Add("TOU_IN_QT", "TOU_IN_QT")
                map.Add("TOU_OUT_NB", "TOU_OUT_NB")
                map.Add("TOU_OUT_QT", "TOU_OUT_QT")
                map.Add("TOU_KURI_NB", "TOU_KURI_NB")
                map.Add("TOU_KURI_QT", "TOU_KURI_QT")

            Case "06", "05", "04"

                map.Add("FROM_DATE", "FROM_DATE")
                map.Add("TO_DATE", "TO_DATE")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("NRS_BR_NM", "NRS_BR_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("KITAKU_CODE", "KITAKU_CODE")
                map.Add("KITAKU_NM", "KITAKU_NM")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_NM_L", "CUST_NM_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_NM_M", "CUST_NM_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_NM_S", "CUST_NM_S")
                map.Add("CUST_CD_SS", "CUST_CD_SS")
                map.Add("CUST_NM_SS", "CUST_NM_SS")
                map.Add("TOU_NO", "TOU_NO")
                map.Add("SITU_NO", "SITU_NO")
                map.Add("HANKI_KB", "HANKI_KB")
                map.Add("HANKI_NM", "HANKI_NM")
                map.Add("ZEN_KURI_NB", "ZEN_KURI_NB")
                map.Add("ZEN_KURI_QT", "ZEN_KURI_QT")
                map.Add("ZEN_KURI_PRICE", "ZEN_KURI_PRICE")
                map.Add("TOU_IN_NB", "TOU_IN_NB")
                map.Add("TOU_IN_QT", "TOU_IN_QT")
                map.Add("TOU_IN_PRICE", "TOU_IN_PRICE")
                map.Add("TOU_OUT_NB", "TOU_OUT_NB")
                map.Add("TOU_OUT_QT", "TOU_OUT_QT")
                map.Add("TOU_OUT_PRICE", "TOU_OUT_PRICE")
                map.Add("TOU_KURI_NB", "TOU_KURI_NB")
                map.Add("TOU_KURI_QT", "TOU_KURI_QT")
                map.Add("TOU_KURI_PRICE", "TOU_KURI_PRICE")
                map.Add("KAITEN_RITSU", "KAITEN_RITSU")

            Case "07"

                map.Add("PRINT_NENTSUKI", "PRINT_NENTSUKI")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("NRS_BR_NM", "NRS_BR_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("CUST_CD_LM", "CUST_CD_LM")
                map.Add("CUST_NM_LM", "CUST_NM_LM")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_NM_L", "CUST_NM_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_NM_M", "CUST_NM_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_NM_S", "CUST_NM_S")
                map.Add("CUST_CD_SS", "CUST_CD_SS")
                map.Add("CUST_NM_SS", "CUST_NM_SS")
                map.Add("TOU_NO", "TOU_NO")
                map.Add("SITU_NO", "SITU_NO")
                map.Add("TOU_SITU_NM", "TOU_SITU_NM")
                map.Add("ZEN_KURI_NB", "ZEN_KURI_NB")
                map.Add("ZEN_KURI_QT", "ZEN_KURI_QT")
                map.Add("TOU_IN_NB", "TOU_IN_NB")
                map.Add("TOU_IN_QT", "TOU_IN_QT")
                map.Add("TOU_OUT_NB", "TOU_OUT_NB")
                map.Add("TOU_OUT_QT", "TOU_OUT_QT")
                map.Add("TOU_KURI_NB", "TOU_KURI_NB")
                map.Add("TOU_KURI_QT", "TOU_KURI_QT")

            Case "09"

                map.Add("PRINT_NENTSUKI", "PRINT_NENTSUKI")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("NRS_BR_NM", "NRS_BR_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("CUST_CD_LM", "CUST_CD_LM")
                map.Add("CUST_NM_LM", "CUST_NM_LM")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_NM_L", "CUST_NM_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_NM_M", "CUST_NM_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_NM_S", "CUST_NM_S")
                map.Add("CUST_CD_SS", "CUST_CD_SS")
                map.Add("CUST_NM_SS", "CUST_NM_SS")
                map.Add("TOU_NO", "TOU_NO")
                map.Add("SITU_NO", "SITU_NO")
                map.Add("TOU_SITU_NM", "TOU_SITU_NM")
                map.Add("ZEN_KURI_NB", "ZEN_KURI_NB")
                map.Add("ZEN_KURI_QT", "ZEN_KURI_QT")
                map.Add("TOU_IN_NB", "TOU_IN_NB")
                map.Add("TOU_IN_QT", "TOU_IN_QT")
                map.Add("TOU_OUT_NB", "TOU_OUT_NB")
                map.Add("TOU_OUT_QT", "TOU_OUT_QT")
                map.Add("TOU_KURI_NB", "TOU_KURI_NB")
                map.Add("TOU_KURI_QT", "TOU_KURI_QT")
                map.Add("SYS_PAGE_KEY", "SYS_PAGE_KEY")

#If True Then 'ADD 2020/10/29 新 016063
            Case "10"

                map.Add("FROM_DATE", "FROM_DATE")
                map.Add("TO_DATE", "TO_DATE")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("NRS_BR_NM", "NRS_BR_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("WH_NM", "WH_NM")
                map.Add("KITAKU_CODE", "KITAKU_CODE")
                map.Add("KITAKU_NM", "KITAKU_NM")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_NM_L", "CUST_NM_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_NM_M", "CUST_NM_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_NM_S", "CUST_NM_S")
                map.Add("CUST_CD_SS", "CUST_CD_SS")
                map.Add("CUST_NM_SS", "CUST_NM_SS")
                map.Add("HANKI_KB", "HANKI_KB")
                map.Add("HANKI_NM", "HANKI_NM")
                map.Add("ZEN_KURI_NB", "ZEN_KURI_NB")
                map.Add("ZEN_KURI_QT", "ZEN_KURI_QT")
                map.Add("ZEN_KURI_PRICE", "ZEN_KURI_PRICE")
                map.Add("TOU_IN_NB", "TOU_IN_NB")
                map.Add("TOU_IN_QT", "TOU_IN_QT")
                map.Add("TOU_IN_PRICE", "TOU_IN_PRICE")
                map.Add("TOU_OUT_NB", "TOU_OUT_NB")
                map.Add("TOU_OUT_QT", "TOU_OUT_QT")
                map.Add("TOU_OUT_PRICE", "TOU_OUT_PRICE")
                map.Add("TOU_KURI_NB", "TOU_KURI_NB")
                map.Add("TOU_KURI_QT", "TOU_KURI_QT")
                map.Add("TOU_KURI_PRICE", "TOU_KURI_PRICE")
                map.Add("KAITEN_RITSU", "KAITEN_RITSU")
                map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
                map.Add("GOODS_NM_1", "GOODS_NM_1")
                map.Add("SHOBOKIKEN_KB", "SHOBOKIKEN_KB")
                map.Add("SHOBOKIKEN_NM", "SHOBOKIKEN_NM")

#End If
            Case Else

        End Select

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD630OUT")

        Return ds

    End Function

#End Region




#Region "設定処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereData()

        _NRS_BR_CD = Me._Row("NRS_BR_CD").ToString()
        _CUST_CD_L = Me._Row("CUST_CD_L").ToString()
        _CUST_CD_M = Me._Row("CUST_CD_M").ToString()
        _PRINT_FROM = Me._Row("PRINT_FROM").ToString()
        _PRINT_TO = Me._Row("PRINT_TO").ToString()
        _SYS_ENT_DATE = MyBase.GetSystemDate().ToString()
        _SYS_ENT_TIME = MyBase.GetSystemTime().ToString()
        _SYS_ENT_PGID = MyBase.GetPGID().ToString()
        _SYS_ENT_USER = MyBase.GetUserID().ToString()
        _SYS_UPD_DATE = MyBase.GetSystemDate().ToString()
        _SYS_UPD_TIME = MyBase.GetSystemTime().ToString()
        _SYS_UPD_PGID = MyBase.GetPGID().ToString()
        _SYS_UPD_USER = MyBase.GetUserID().ToString()
        _SYS_DEL_FLG = LMConst.FLG.OFF
        _MONTH_AGO = Replace(Date.Parse(Format(CInt(MyBase.GetSystemDate()), "0000/00/00")).AddMonths(-3).ToString().Substring(0, 10), "/", "")

    End Sub



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

#End Region

End Class
