' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN010BLF : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMN010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _sqlPrmList As ArrayList

    ''' <summary>
    ''' マスタ用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクション用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMNTrnSchemaNm As String

    ''' <summary>
    ''' EDI用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnEDINm As String

    ''' <summary>
    ''' EDIマスタ用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstEDINm As String

    ''' <summary>
    ''' LMSのコネクション
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMS1 As SqlConnection = New SqlConnection


#End Region

#Region "Const"

    'マスタスキーマ名
    Private Const MST_SCHEMA As String = "LM_MST"

    'トランザクションスキーマ名
    Private Const TRN_SCHEMA As String = "LM_TRN"

#Region "区分マスタ"

    'ステータス「未設定」区分コード
    Private Const KbnCdMisettei As String = "00"
    'ステータス「設定済」区分コード
    Private Const KbnCdSetteiZumi As String = "01"

#End Region

#Region "検索処理 SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(*)                    AS SELECT_CNT  " & vbNewLine _
                                             & " FROM (	                                           " & vbNewLine
    ' 検索用SQL(N_OUTKASCM_L)用
    Private SQL_SELECT_DATA_SCML As String

    ''' <summary>
    ''' 検索用SQL(N_OUTKASCM_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlSelectData_SCML()

        Call Me.SetSchemaNm()

        Dim SQL As String = " SELECT                                                                    " & vbNewLine _
                          & "      SCML.SCM_CTL_NO_L         AS SCM_CTL_NO_L                            " & vbNewLine _
                          & "     ,SCML.SCM_CUST_CD          AS SCM_CUST_CD                             " & vbNewLine _
                          & "     ,SCML.STATUS_KBN           AS STATUS_KBN                              " & vbNewLine _
                          & "     ,SCML.BR_CD                AS BR_CD                                   " & vbNewLine _
                          & "     ,SCML.SOKO_CD              AS SOKO_CD                                 " & vbNewLine _
                          & "     ,SCML.CUST_ORD_NO_L        AS CUST_ORD_NO_L                           " & vbNewLine _
                          & "     ,SCML.MOUSHIOKURI_KBN      AS MOUSHIOKURI_KBN                         " & vbNewLine _
                          & "     ,SCML.OUTKA_DATE           AS OUTKA_DATE                              " & vbNewLine _
                          & "     ,SCML.ARR_DATE             AS ARR_DATE                                " & vbNewLine _
                          & "     ,SCML.DEST_NM              AS DEST_NM                                 " & vbNewLine _
                          & "     ,SCML.DEST_AD              AS DEST_AD                                 " & vbNewLine _
                          & "     ,SCML.DEST_ZIP             AS DEST_ZIP                                " & vbNewLine _
                          & "     ,SCML.REMARK1              AS REMARK1                                 " & vbNewLine _
                          & "     ,SCML.REMARK2              AS REMARK2                                 " & vbNewLine _
                          & "     ,(SELECT                                                              " & vbNewLine _
                          & "            COUNT(SCM_CTL_NO_M)                                            " & vbNewLine _
                          & "       FROM  " & Me._LMNTrnSchemaNm & "N_OUTKASCM_M SCMM                      " & vbNewLine _
                          & "       WHERE                                                               " & vbNewLine _
                          & "            SCMM.SCM_CTL_NO_L = SCML.SCM_CTL_NO_L                          " & vbNewLine _
                          & "       AND  SCMM.SYS_DEL_FLG = '0')  AS DTL_CNT                            " & vbNewLine _
                          & "     ,SUBSTRING(SCML.EDI_DATE,1,4) + '/'                                   " & vbNewLine _
                          & "      + SUBSTRING(SCML.EDI_DATE,5,2) + '/'                                 " & vbNewLine _
                          & "      + SUBSTRING(SCML.EDI_DATE,7,2) + ' '                                 " & vbNewLine _
                          & "      + SUBSTRING(SCML.EDI_TIME,1,2) + ':'                                 " & vbNewLine _
                          & "      + SUBSTRING(SCML.EDI_TIME,3,2) + ':'                                 " & vbNewLine _
                          & "      + SUBSTRING(SCML.EDI_TIME,5,2)   AS EDI_DATETIME                     " & vbNewLine _
                          & "     ,'0'                       AS INSERT_FLG                              " & vbNewLine _
                          & "     ,SCMH.CRT_DATE             AS CRT_DATE                                " & vbNewLine _
                          & "     ,SCMH.FILE_NAME            AS FILE_NAME                               " & vbNewLine _
                          & "     ,SCMH.REC_NO               AS REC_NO                                  " & vbNewLine _
                          & "     ,SCMH.SYS_UPD_DATE         AS HED_BP_SYS_UPD_DATE                     " & vbNewLine _
                          & "     ,SCMH.SYS_UPD_TIME         AS HED_BP_SYS_UPD_TIME                     " & vbNewLine _
                          & "     ,SCML.SYS_UPD_DATE         AS L_SYS_UPD_DATE                          " & vbNewLine _
                          & "     ,SCML.SYS_UPD_TIME         AS L_SYS_UPD_TIME                          " & vbNewLine _
                          & "FROM                                                                       " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_L  SCML                             " & vbNewLine _
                          & "LEFT JOIN " & Me._LMNTrnSchemaNm & "N_OUTKASCM_HED_BP  SCMH                   " & vbNewLine _
                          & "ON   SCMH.SCM_CTL_NO_L = SCML.SCM_CTL_NO_L                                 " & vbNewLine _
                          & "WHERE                                                                      " & vbNewLine _
                          & "     SCML.SYS_DEL_FLG = '0'                                                " & vbNewLine
        SQL_SELECT_DATA_SCML = SQL

    End Sub

    '検索用SQL(N_OUTKASCM_HED_BP)用
    Private SQL_SELECT_DATA_SCMH As String

    ''' <summary>
    ''' 検索用SQL(N_OUTKASCM_HED_BP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlSelectData_SCMH()

        Call Me.SetSchemaNm()

        Dim SQL As String = "UNION                                                                                                 " & vbNewLine _
                          & "SELECT                                                                                                " & vbNewLine _
                          & "     ''                        AS SCM_CTL_NO_L                                                        " & vbNewLine _
                          & "    ,''                        AS SCM_CUST_CD                                                         " & vbNewLine _
                          & "    ,'00'                      AS STATUS_KBN                                                          " & vbNewLine _
                          & "    ,ISNULL((SELECT                                                                                   " & vbNewLine _
                          & "           MSK.NRS_BR_CD                                                                              " & vbNewLine _
                          & "      FROM " & Me._MstSchemaNm & "M_SOKO  MSK                                                         " & vbNewLine _
                          & "      WHERE                                                                                           " & vbNewLine _
                          & "           MSK.WH_CD = (SELECT                                                                        " & vbNewLine _
                          & "                             DSK2.WH_CD                                                               " & vbNewLine _
                          & "                        FROM " & Me._MstSchemaNm & "M_DEFAULT_SOKO  DSK2                              " & vbNewLine _
                          & "                        WHERE                                                                         " & vbNewLine _
                          & "                             DSK2.SCM_CUST_CD = (SELECT                                               " & vbNewLine _
                          & "                                                      KBN_NM3                                         " & vbNewLine _
                          & "                                                 FROM " & Me._MstSchemaNm & "Z_KBN  KBN4              " & vbNewLine _
                          & "                                                 WHERE                                                " & vbNewLine _
                          & "                                                      KBN4.KBN_GROUP_CD = 'S033'                      " & vbNewLine _
                          & "                                                 AND  KBN4.KBN_CD = '00')                             " & vbNewLine _
                          & "                        AND  DSK2.JIS_CD = ISNULL((SELECT                                             " & vbNewLine _
                          & "                                                MAX(JIS_CD)                                           " & vbNewLine _
                          & "                                           FROM " & Me._MstSchemaNm & "M_ZIP  ZIP2                    " & vbNewLine _
                          & "                                           WHERE                                                      " & vbNewLine _
                          & "                                                ZIP2.ZIP_NO = SCMH.DEST_ZIP),''))),'')  AS BR_CD      " & vbNewLine _
                          & "    ,ISNULL((SELECT                                                                                   " & vbNewLine _
                          & "           DSK1.WH_CD                                                                                 " & vbNewLine _
                          & "      FROM " & Me._MstSchemaNm & "M_DEFAULT_SOKO  DSK1                                                " & vbNewLine _
                          & "      WHERE                                                                                           " & vbNewLine _
                          & "           DSK1.SCM_CUST_CD = (SELECT                                                                 " & vbNewLine _
                          & "                                    KBN_NM3                                                           " & vbNewLine _
                          & "                               FROM " & Me._MstSchemaNm & "Z_KBN  KBN1                                " & vbNewLine _
                          & "                               WHERE                                                                  " & vbNewLine _
                          & "                                    KBN1.KBN_GROUP_CD = 'S033'                                        " & vbNewLine _
                          & "                               AND  KBN1.KBN_CD = '00')                                               " & vbNewLine _
                          & "      AND  DSK1.JIS_CD = ISNULL((SELECT                                                               " & vbNewLine _
                          & "                               MAX(JIS_CD)                                                            " & vbNewLine _
                          & "                          FROM " & Me._MstSchemaNm & "M_ZIP  ZIP1                                     " & vbNewLine _
                          & "                          WHERE                                                                       " & vbNewLine _
                          & "                               ZIP1.ZIP_NO = SCMH.DEST_ZIP),'')),'')  AS SOKO_CD                      " & vbNewLine _
                          & "    ,SCMH.DENPYO_NO              AS CUST_ORD_NO_L                                                     " & vbNewLine _
                          & "    ,ISNULL((SELECT                                                                                   " & vbNewLine _
                          & "                  KBN2.KBN_CD                                                                         " & vbNewLine _
                          & "             FROM " & Me._MstSchemaNm & "Z_KBN  KBN2                                                  " & vbNewLine _
                          & "             WHERE                                                                                    " & vbNewLine _
                          & "                  KBN2.KBN_GROUP_CD = 'M008'                                                          " & vbNewLine _
                          & "             AND  KBN2.KBN_NM1 = SCMH.MOSIOKURI_KB),'')  AS MOUSHIOKURI_KBN                           " & vbNewLine _
                          & "    ,SCMH.OUTKA_PLAN_DATE           AS OUTKA_DATE                                                     " & vbNewLine _
                          & "    ,SCMH.ARR_PLAN_DATE             AS ARR_DATE                                                       " & vbNewLine _
                          & "    ,SCMH.DEST_NM1 + SCMH.DEST_NM2  AS DEST_NM                                                        " & vbNewLine _
                          & "    ,SCMH.DEST_AD1 + SCMH.DEST_AD2  AS DEST_AD                                                        " & vbNewLine _
                          & "    ,SCMH.DEST_ZIP                  AS DEST_ZIP                                                       " & vbNewLine _
                          & "    ,SCMH.BIKO_HED1                 AS REMARK1                                                        " & vbNewLine _
                          & "    ,SCMH.BIKO_HED2                 AS REMARK2                                                        " & vbNewLine _
                          & "    ,(SELECT                                                                                          " & vbNewLine _
                          & "           COUNT(GYO)                                                                                 " & vbNewLine _
                          & "      FROM " & Me._LMNTrnSchemaNm & "N_OUTKASCM_DTL_BP  SCMD                                             " & vbNewLine _
                          & "      WHERE                                                                                           " & vbNewLine _
                          & "           SCMD.CRT_DATE = SCMH.CRT_DATE                                                              " & vbNewLine _
                          & "      AND  SCMD.FILE_NAME = SCMH.FILE_NAME                                                            " & vbNewLine _
                          & "      AND  SCMD.REC_NO = SCMH.REC_NO )  AS DTL_CNT                                                    " & vbNewLine _
                          & "    ,SUBSTRING(SCMH.EDI_DATE,1,4) + '/'                                                               " & vbNewLine _
                          & "     + SUBSTRING(SCMH.EDI_DATE,5,2) + '/'                                                             " & vbNewLine _
                          & "     + SUBSTRING(SCMH.EDI_DATE,7,2)                                                                   " & vbNewLine _
                          & "     + ' '                                                                                            " & vbNewLine _
                          & "     + SCMH.EDI_TIME        AS EDI_DATETIME                                                           " & vbNewLine _
                          & "    ,'1'                            AS INSERT_FLG                                                     " & vbNewLine _
                          & "    ,SCMH.CRT_DATE                  AS CRT_DATE                                                       " & vbNewLine _
                          & "    ,SCMH.FILE_NAME                 AS FILE_NAME                                                      " & vbNewLine _
                          & "    ,SCMH.REC_NO                    AS REC_NO                                                         " & vbNewLine _
                          & "    ,SCMH.SYS_UPD_DATE              AS HED_BP_SYS_UPD_D                                               " & vbNewLine _
                          & "    ,SCMH.SYS_UPD_TIME              AS HED_BP_SYS_UPD_TIME                                            " & vbNewLine _
                          & "    ,''                             AS L_SYS_UPD_DATE                                                 " & vbNewLine _
                          & "    ,''                             AS L_SYS_UPD_TIME                                                 " & vbNewLine _
                          & "FROM                                                                                                  " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_HED_BP  SCMH                                                   " & vbNewLine _
                          & "WHERE                                                                                                 " & vbNewLine _
                          & "      SCMH.SYS_DEL_FLG = '0'                                                                          " & vbNewLine

        SQL_SELECT_DATA_SCMH = SQL

    End Sub


    ''' <summary>
    ''' (LMS)LMS側出荷日、納入日取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLKensakuGetLMSDate()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  HEDIH.CRT_DATE           AS CRT_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,HEDIH.FILE_NAME          AS FILE_NAME       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,HEDIH.REC_NO             AS REC_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,ISNULL(HEDIL.OUTKA_PLAN_DATE,'')    AS LMS_OUTKA_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,ISNULL(HEDIL.ARR_PLAN_DATE,'')      AS LMS_ARR_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_OUTKAEDI_HED_BP HEDIH       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("LEFT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_OUTKAEDI_L HEDIL       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIL.DEL_KB = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIL.EDI_CTL_NO = HEDIH.EDI_CTL_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.DEL_KB = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.CRT_DATE = @CRT_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.FILE_NAME = @FILE_NAME       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.REC_NO = @REC_NO       ")

        End With

    End Sub


#End Region

#Region "設定処理 SQL"

    ' 抽出用SQL(N_OUTKASCM_DTL_BP)用
    Private SQL_SELECT_DATA_SCMD As String

    ''' <summary>
    ''' 抽出用SQL(N_OUTKASCM_DTL_BP)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlSetteiSelectData_SCMD()

        Call Me.SetSchemaNm()

        Dim SQL As String = " SELECT                                                " & vbNewLine _
                          & "      CRT_DATE         AS CRT_DATE                     " & vbNewLine _
                          & "     ,FILE_NAME        AS FILE_NAME                    " & vbNewLine _
                          & "     ,REC_NO           AS REC_NO                       " & vbNewLine _
                          & "     ,GYO              AS GYO                          " & vbNewLine _
                          & "     ,ROW_NO           AS ROW_NO                       " & vbNewLine _
                          & "     ,GOODS_CD         AS GOODS_CD                     " & vbNewLine _
                          & "     ,GOODS_NM         AS GOODS_NM                     " & vbNewLine _
                          & "     ,LOT_NO           AS LOT_NO                       " & vbNewLine _
                          & "     ,PKG_NB           AS PKG_NB                       " & vbNewLine _
                          & "     ,OUTKA_PKG_NB     AS OUTKA_PKG_NB                 " & vbNewLine _
                          & "     ,OUTKA_NB         AS OUTKA_NB                     " & vbNewLine _
                          & "     ,TOTAL_WT         AS TOTAL_WT                     " & vbNewLine _
                          & "     ,TOTAL_QT         AS TOTAL_QT                     " & vbNewLine _
                          & "     ,BIKO_HED1        AS BIKO_HED1                    " & vbNewLine _
                          & "     ,BIKO_HED2        AS BIKO_HED2                    " & vbNewLine _
                          & "     ,BIKO_DTL         AS BIKO_DTL                     " & vbNewLine _
                          & "     ,@SCM_CTL_NO_L    AS SCM_CTL_NO_L                 " & vbNewLine _
                          & "     ,@SCM_CUST_CD     AS SCM_CUST_CD                  " & vbNewLine _
                          & "FROM                                                   " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_DTL_BP          " & vbNewLine _
                          & "WHERE                                                  " & vbNewLine _
                          & "     SYS_DEL_FLG = '0'                                 " & vbNewLine _
                          & "AND  CRT_DATE = @CRT_DATE                              " & vbNewLine _
                          & "AND  FILE_NAME = @FILE_NAME                            " & vbNewLine _
                          & "AND  REC_NO = @REC_NO                                  " & vbNewLine

        SQL_SELECT_DATA_SCMD = SQL

    End Sub

#Region "出荷EDIデータL新規登録用"

    ''' <summary>
    ''' 出荷EDIデータL新規登録用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SETTEI_INSERT_N_OUTKASCM_L As String = "(                     " & vbNewLine _
                                                           & "  SCM_CTL_NO_L        " & vbNewLine _
                                                           & " ,SCM_CUST_CD         " & vbNewLine _
                                                           & " ,STATUS_KBN          " & vbNewLine _
                                                           & " ,BR_CD               " & vbNewLine _
                                                           & " ,SOKO_CD             " & vbNewLine _
                                                           & " ,CUST_ORD_NO_L       " & vbNewLine _
                                                           & " ,EDI_DATE            " & vbNewLine _
                                                           & " ,EDI_TIME            " & vbNewLine _
                                                           & " ,MOUSHIOKURI_KBN     " & vbNewLine _
                                                           & " ,OUTKA_DATE          " & vbNewLine _
                                                           & " ,ARR_DATE            " & vbNewLine _
                                                           & " ,DEST_NM             " & vbNewLine _
                                                           & " ,DEST_AD             " & vbNewLine _
                                                           & " ,DEST_TEL            " & vbNewLine _
                                                           & " ,DEST_ZIP            " & vbNewLine _
                                                           & " ,REMARK1             " & vbNewLine _
                                                           & " ,REMARK2             " & vbNewLine _
                                                           & " ,SYS_ENT_DATE        " & vbNewLine _
                                                           & " ,SYS_ENT_TIME        " & vbNewLine _
                                                           & " ,SYS_ENT_PGID        " & vbNewLine _
                                                           & " ,SYS_ENT_USER        " & vbNewLine _
                                                           & " ,SYS_UPD_DATE        " & vbNewLine _
                                                           & " ,SYS_UPD_TIME        " & vbNewLine _
                                                           & " ,SYS_UPD_PGID        " & vbNewLine _
                                                           & " ,SYS_UPD_USER        " & vbNewLine _
                                                           & " ,SYS_DEL_FLG         " & vbNewLine _
                                                           & " )                    " & vbNewLine



#End Region

#Region "出荷EDIデータM新規登録用"

    ''' <summary>
    ''' 出荷EDIデータM新規登録用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SETTEI_INSERT_N_OUTKASCM_M As String = "(                                      " & vbNewLine _
                                                           & "  SCM_CTL_NO_L                         " & vbNewLine _
                                                           & " ,SCM_CTL_NO_M                         " & vbNewLine _
                                                           & " ,CUST_ORD_NO_M                        " & vbNewLine _
                                                           & " ,CUST_GOODS_CD                        " & vbNewLine _
                                                           & " ,GOODS_NM                             " & vbNewLine _
                                                           & " ,LOT_NO                               " & vbNewLine _
                                                           & " ,OUTKA_PKG_NB                         " & vbNewLine _
                                                           & " ,OUTKA_QT                             " & vbNewLine _
                                                           & " ,OUTKA_TTL_NB                         " & vbNewLine _
                                                           & " ,BETU_WT                              " & vbNewLine _
                                                           & " ,OUTKA_TTL_QT                         " & vbNewLine _
                                                           & " ,REMARK                               " & vbNewLine _
                                                           & " ,JISSEKI_KBN                          " & vbNewLine _
                                                           & " ,JISSEKI_USER                         " & vbNewLine _
                                                           & " ,JISSEKI_DATE                         " & vbNewLine _
                                                           & " ,JISSEKI_TIME                         " & vbNewLine _
                                                           & " ,SYS_ENT_DATE                         " & vbNewLine _
                                                           & " ,SYS_ENT_TIME                         " & vbNewLine _
                                                           & " ,SYS_ENT_PGID                         " & vbNewLine _
                                                           & " ,SYS_ENT_USER                         " & vbNewLine _
                                                           & " ,SYS_UPD_DATE                         " & vbNewLine _
                                                           & " ,SYS_UPD_TIME                         " & vbNewLine _
                                                           & " ,SYS_UPD_PGID                         " & vbNewLine _
                                                           & " ,SYS_UPD_USER                         " & vbNewLine _
                                                           & " ,SYS_DEL_FLG                          " & vbNewLine _
                                                           & ") VALUES (                             " & vbNewLine _
                                                           & "  @SCM_CTL_NO_L                        " & vbNewLine _
                                                           & " ,@SCM_CTL_NO_M                        " & vbNewLine _
                                                           & " ,@ROW_NO                              " & vbNewLine _
                                                           & " ,@GOODS_CD                            " & vbNewLine _
                                                           & " ,@GOODS_NM                            " & vbNewLine _
                                                           & " ,@LOT_NO                              " & vbNewLine _
                                                           & " ,@PKG_NB                              " & vbNewLine _
                                                           & " ,@OUTKA_PKG_NB                        " & vbNewLine _
                                                           & " ,@OUTKA_NB                            " & vbNewLine _
                                                           & " ,@TOTAL_WT                            " & vbNewLine _
                                                           & " ,@TOTAL_QT                            " & vbNewLine _
                                                           & " ,@BIKO_HED1 + @BIKO_HED2 + @BIKO_DTL  " & vbNewLine _
                                                           & " ,'00'                                 " & vbNewLine _
                                                           & " ,''                                   " & vbNewLine _
                                                           & " ,''                                   " & vbNewLine _
                                                           & " ,''                                   " & vbNewLine _
                                                           & " ,@SYS_ENT_DATE                        " & vbNewLine _
                                                           & " ,@SYS_ENT_TIME                        " & vbNewLine _
                                                           & " ,@SYS_ENT_PGID                        " & vbNewLine _
                                                           & " ,@SYS_ENT_USER                        " & vbNewLine _
                                                           & " ,@SYS_UPD_DATE                        " & vbNewLine _
                                                           & " ,@SYS_UPD_TIME                        " & vbNewLine _
                                                           & " ,@SYS_UPD_PGID                        " & vbNewLine _
                                                           & " ,@SYS_UPD_USER                        " & vbNewLine _
                                                           & " ,@SYS_DEL_FLG                         " & vbNewLine _
                                                           & " )                                     " & vbNewLine

#End Region

#Region "BP出荷EDI受信ヘッダデータ更新用"

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SETTEI_UPDATE_N_OUTKASCM_HED_BP As String = "   SCM_CTL_NO_L = @SCM_CTL_NO_L  " & vbNewLine _
                                                                & "  ,SCM_CUST_CD = @SCM_CUST_CD    " & vbNewLine _
                                                                & "  ,SYS_UPD_DATE = @SYS_UPD_DATE  " & vbNewLine _
                                                                & "  ,SYS_UPD_TIME = @SYS_UPD_TIME  " & vbNewLine _
                                                                & "  ,SYS_UPD_PGID = @SYS_UPD_PGID  " & vbNewLine _
                                                                & "  ,SYS_UPD_USER = @SYS_UPD_USER  " & vbNewLine _
                                                                & "  WHERE                          " & vbNewLine _
                                                                & "   CRT_DATE = @CRT_DATE          " & vbNewLine _
                                                                & "  AND FILE_NAME = @FILE_NAME     " & vbNewLine _
                                                                & "  AND REC_NO = @REC_NO           " & vbNewLine

#End Region

#Region "BP出荷EDI受信明細データ更新用"

    ''' <summary>
    ''' BP出荷EDI受信明細データ更新用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SETTEI_UPDATE_N_OUTKASCM_DTL_BP As String = "   SCM_CTL_NO_L = @SCM_CTL_NO_L  " & vbNewLine _
                                                                & "  ,SCM_CTL_NO_M = @SCM_CTL_NO_M  " & vbNewLine _
                                                                & "  ,SCM_CUST_CD = @SCM_CUST_CD    " & vbNewLine _
                                                                & "  ,SYS_UPD_DATE = @SYS_UPD_DATE  " & vbNewLine _
                                                                & "  ,SYS_UPD_TIME = @SYS_UPD_TIME  " & vbNewLine _
                                                                & "  ,SYS_UPD_PGID = @SYS_UPD_PGID  " & vbNewLine _
                                                                & "  ,SYS_UPD_USER = @SYS_UPD_USER  " & vbNewLine _
                                                                & "  WHERE                          " & vbNewLine _
                                                                & "   CRT_DATE = @CRT_DATE          " & vbNewLine _
                                                                & "  AND FILE_NAME = @FILE_NAME     " & vbNewLine _
                                                                & "  AND REC_NO = @REC_NO           " & vbNewLine _
                                                                & "  AND GYO = @GYO                 " & vbNewLine

#End Region

    ''' <summary>
    '''  出荷EDIデータL存在チェック用SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQL_EXIST_N_OUTKASCM_L_SELECT()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._strSql.Append("SELECT                                ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   COUNT(SCM_CTL_NO_L)  AS REC_CNT   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM                                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_L")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE                                 ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   SCM_CTL_NO_L = @SCM_CTL_NO_L    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND SYS_DEL_FLG = '0'               ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  BP出荷EDI受信ヘッダデータ存在チェック用SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQL_EXIST_N_OUTKASCM_HED_BP_SELECT()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._strSql.Append("SELECT                                ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   COUNT(DENPYO_NO)  AS REC_CNT   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM                                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_HED_BP")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE                                 ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("    CRT_DATE = @CRT_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND FILE_NAME = @FILE_NAME               ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND REC_NO = @REC_NO               ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND SYS_DEL_FLG = '0'               ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  出荷EDIデータL新規登録用SQL作成(HEAD句)
    ''' </summary>
    ''' <remarks>出荷EDIデータL新規登録用SQLの構築</remarks>
    Private Sub SQLSetteiInsertHeadN_OUTKASCM_L()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("INSERT INTO                         ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_L")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 出荷EDIデータL新規登録用SQL作成(SELECT句)
    ''' </summary>
    ''' <remarks>出荷EDIデータL新規登録用SQLの構築</remarks>
    Private Sub SQLSetteiInsertSelectN_OUTKASCM_L()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("SELECT                         ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  @SCM_CTL_NO_L   AS SCM_CTL_NO_L      ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,@HED_SCM_CUST_CD  AS SCM_CUST_CD      ")
        Me._strSql.Append(vbNewLine)
        If ((String.IsNullOrEmpty(Me._row.Item("HED_SOKO_CD").ToString)) And (String.IsNullOrEmpty(Me._row.Item("SOKO_CD").ToString))) _
        Or ((String.IsNullOrEmpty(Me._row.Item("HED_OUTKA_DATE").ToString)) And (String.IsNullOrEmpty(Me._row.Item("OUTKA_DATE").ToString))) _
        Or ((String.IsNullOrEmpty(Me._row.Item("HED_ARR_DATE").ToString)) And (String.IsNullOrEmpty(Me._row.Item("ARR_DATE").ToString))) Then
            Me._strSql.Append(String.Concat(" ,'", KbnCdMisettei, "'  AS STATUS_KBN "))
        Else
            Me._strSql.Append(String.Concat(" ,'", KbnCdSetteiZumi, "'  AS STATUS_KBN "))
        End If
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,ISNULL((SELECT            ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("        NRS_BR_CD           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   FROM            ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._MstSchemaNm)
        Me._strSql.Append("M_SOKO                   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   WHERE            ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   WH_CD = @SOKO_CD),'') AS BR_CD   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  ,@SOKO_CD  AS SOKO_CD  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SCMH.DENPYO_NO  AS CUST_ORD_NO_L       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SCMH.EDI_DATE  AS EDI_DATE     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,REPLACE(SCMH.EDI_TIME,':','') + '00'  AS EDI_TIME            ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,ISNULL((SELECT                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("         KBN.KBN_CD                   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   FROM                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._MstSchemaNm)
        Me._strSql.Append("Z_KBN KBN                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   WHERE                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("         KBN.KBN_GROUP_CD = 'M008'                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   AND                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("         KBN.KBN_NM1 = SCMH.MOSIOKURI_KB                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   AND                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("         KBN.SYS_DEL_FLG = '0'),'')  AS MOUSHIOKURI_KBN                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,@OUTKA_DATE  AS OUTKA_DATE                   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,@ARR_DATE  AS ARR_DATE                   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  ,SCMH.DEST_NM1 + SCMH.DEST_NM2   AS DEST_NM                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,SCMH.DEST_AD1 + SCMH.DEST_AD2   AS DEST_AD                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,SCMH.DEST_TEL  AS DEST_TEL                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,SCMH.DEST_ZIP  AS DEST_ZIP                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,SCMH.BIKO_HED1  AS REMARK1                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,SCMH.BIKO_HED2  AS REMARK2                    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_ENT_DATE  AS SYS_ENT_DATE                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_ENT_TIME  AS SYS_ENT_TIME                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_ENT_PGID  AS SYS_ENT_PGID                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_ENT_USER  AS SYS_ENT_USER                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_UPD_DATE  AS SYS_UPD_DATE                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_UPD_TIME  AS SYS_UPD_TIME                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_UPD_PGID  AS SYS_UPD_PGID                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_UPD_USER  AS SYS_UPD_USER                      ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   ,@SYS_DEL_FLG  AS SYS_DEL_FLG                        ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 出荷EDIデータL新規登録用SQL作成(FROM句)
    ''' </summary>
    ''' <remarks>出荷EDIデータL新規登録用SQLの構築</remarks>
    Private Sub SQLSetteiInsertFromN_OUTKASCM_L()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("FROM                         ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_HED_BP SCMH       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SCMH.CRT_DATE = @CRT_DATE         ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                          ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SCMH.FILE_NAME = @FILE_NAME       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                          ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SCMH.REC_NO = @REC_NO             ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                          ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SCMH.SYS_DEL_FLG = '0'             ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  出荷EDIデータM新規登録用SQL作成(HEAD句)
    ''' </summary>
    ''' <remarks>出荷EDIデータM新規登録用SQLの構築</remarks>
    Private Sub SQLSetteiInsertHeadN_OUTKASCM_M()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("INSERT INTO                         ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_M ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  出荷EDIデータL更新処理用SQL作成
    ''' </summary>
    ''' <remarks>出荷EDIデータL更新処理用SQLの構築</remarks>
    Private Sub SQLSetteiUpdateHeadN_OUTKASCM_L()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                              ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_L SET")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 設定時アップデート項目設定（N_OUTKASCM_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLSetteiUpdateBodyN_OUTKASCM_L()

        Call Me.SetSchemaNm()

        With Me._row

            'ステータス判定用パラメータ設定
            Dim HedSokoCd As String = .Item("HED_SOKO_CD").ToString
            Dim HedOutkaDate As String = .Item("HED_OUTKA_DATE").ToString
            Dim HedArrDate As String = .Item("HED_ARR_DATE").ToString
            Dim SprSokoCd As String = .Item("SOKO_CD").ToString
            Dim SprOutkaDate As String = .Item("OUTKA_DATE").ToString
            Dim SprArrDate As String = .Item("ARR_DATE").ToString

            'ステータス判定（倉庫、出荷日、納入日のすべてが設定いなければ「未設定」、設定されていれば「設定済」）
            If ((String.IsNullOrEmpty(HedSokoCd) And String.IsNullOrEmpty(SprSokoCd)) Or (String.IsNullOrEmpty(HedOutkaDate) And String.IsNullOrEmpty(SprOutkaDate)) Or (String.IsNullOrEmpty(HedArrDate) And String.IsNullOrEmpty(SprArrDate))) Then
                Me._strSql.Append(String.Concat(" STATUS_KBN = '", KbnCdMisettei, "'"))
                Me._strSql.Append(vbNewLine)
            Else
                Me._strSql.Append(String.Concat(" STATUS_KBN = '", KbnCdSetteiZumi, "'"))
                Me._strSql.Append(vbNewLine)
            End If

            If String.IsNullOrEmpty(HedSokoCd) = False Then
                Me._strSql.Append(" ,BR_CD = (SELECT                    ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append("                NRS_BR_CD            ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append("           FROM                      ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append(Me._MstSchemaNm)
                Me._strSql.Append("M_SOKO                               ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append("           WHERE                     ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append("                WH_CD = @HED_SOKO_CD ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append("           AND                       ")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append("                SYS_DEL_FLG = '0' )  ")
                Me._strSql.Append(vbNewLine)
            End If

            If String.IsNullOrEmpty(HedSokoCd) = False Then
                Me._strSql.Append(" ,SOKO_CD = @HED_SOKO_CD")
                Me._strSql.Append(vbNewLine)
            End If

            If String.IsNullOrEmpty(HedOutkaDate) = False Then
                Me._strSql.Append(" ,OUTKA_DATE = @HED_OUTKA_DATE")
                Me._strSql.Append(vbNewLine)
            End If

            If String.IsNullOrEmpty(HedArrDate) = False Then
                Me._strSql.Append(" ,ARR_DATE = @HED_ARR_DATE")
                Me._strSql.Append(vbNewLine)
            End If

            Me._strSql.Append(" ,SYS_UPD_DATE = @SYS_UPD_DATE")
            Me._strSql.Append(vbNewLine)

            Me._strSql.Append(" ,SYS_UPD_TIME = @SYS_UPD_TIME")
            Me._strSql.Append(vbNewLine)

            Me._strSql.Append(" ,SYS_UPD_PGID = @SYS_UPD_PGID")
            Me._strSql.Append(vbNewLine)

            Me._strSql.Append(" ,SYS_UPD_USER = @SYS_UPD_USER")
            Me._strSql.Append(vbNewLine)

            Me._strSql.Append(" WHERE ")
            Me._strSql.Append(vbNewLine)

            Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L")
            Me._strSql.Append(vbNewLine)

        End With

    End Sub

    ''' <summary>
    '''  BP出荷EDI受信ヘッダデータ更新処理用SQL作成
    ''' </summary>
    ''' <remarks>BP出荷EDI受信ヘッダデータ更新処理用SQLの構築</remarks>
    Private Sub SQLSetteiUpdateHeadN_OUTKASCM_HED_BP()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                               ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_HED_BP SET")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    '''  BP出荷EDI受信明細データ更新処理用SQL作成
    ''' </summary>
    ''' <remarks>BP出荷EDI受信明細データ更新処理用SQLの構築</remarks>
    Private Sub SQLSetteiUpdateHeadN_OUTKASCM_DTL_BP()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                               ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_DTL_BP SET ")
        Me._strSql.Append(vbNewLine)

    End Sub


#End Region

#Region "送信指示 SQL"

    ' 抽出用SQL(N_OUTKASCM_HED_BP)用
    Private SQL_SOUSHIN_SHIJI_SELECT_DATA_SCMH As String

    ''' <summary>
    ''' 抽出用SQL(N_OUTKASCM_DTL_BP)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlSoushinShijiSelectData_SCMH()

        Call Me.SetSchemaNm()

        Dim SQL As String = " SELECT                                                " & vbNewLine _
                          & "      SCML.BR_CD           AS BR_CD                    " & vbNewLine _
                          & "     ,SCMH.CRT_DATE        AS CRT_DATE                 " & vbNewLine _
                          & "     ,SCMH.FILE_NAME       AS FILE_NAME                " & vbNewLine _
                          & "     ,SCMH.REC_NO          AS REC_NO                   " & vbNewLine _
                          & "     ,SCMH.DATA_KB         AS DATA_KB                  " & vbNewLine _
                          & "     ,SCMH.KITAKU_CD       AS KITAKU_CD                " & vbNewLine _
                          & "     ,SCML.SOKO_CD         AS OUTKA_SOKO_CD            " & vbNewLine _
                          & "     ,SCMH.ORDER_TYPE      AS ORDER_TYPE               " & vbNewLine _
                          & "     ,SCML.OUTKA_DATE      AS OUTKA_PLAN_DATE          " & vbNewLine _
                          & "     ,SCMH.CUST_ORD_NO     AS CUST_ORD_NO              " & vbNewLine _
                          & "     ,SCMH.DEST_CD         AS DEST_CD                  " & vbNewLine _
                          & "     ,SCMH.DEST_JIS_CD     AS DEST_JIS_CD              " & vbNewLine _
                          & "     ,SCMH.DEST_NM1        AS DEST_NM1                 " & vbNewLine _
                          & "     ,SCMH.DEST_NM2        AS DEST_NM2                 " & vbNewLine _
                          & "     ,SCMH.DEST_AD1        AS DEST_AD1                 " & vbNewLine _
                          & "     ,SCMH.DEST_AD2        AS DEST_AD2                 " & vbNewLine _
                          & "     ,SCMH.DEST_TEL        AS DEST_TEL                 " & vbNewLine _
                          & "     ,SCMH.DEST_ZIP        AS DEST_ZIP                 " & vbNewLine _
                          & "     ,SCML.ARR_DATE        AS ARR_PLAN_DATE            " & vbNewLine _
                          & "     ,SCMH.ARR_PLAN_TIME   AS ARR_PLAN_TIME            " & vbNewLine _
                          & "     ,SCMH.HT_DATE         AS HT_DATE                  " & vbNewLine _
                          & "     ,SCMH.HT_TIME         AS HT_TIME                  " & vbNewLine _
                          & "     ,SCMH.HT_CAR_NO       AS HT_CAR_NO                " & vbNewLine _
                          & "     ,SCMH.HT_DRIVER       AS HT_DRIVER                " & vbNewLine _
                          & "     ,SCMH.HT_UNSO_CO      AS HT_UNSO_CO               " & vbNewLine _
                          & "     ,SCMH.MOSIOKURI_KB    AS MOSIOKURI_KB             " & vbNewLine _
                          & "     ,SCMH.BUMON_CD        AS BUMON_CD                 " & vbNewLine _
                          & "     ,SCMH.JIGYOBU_CD      AS JIGYOBU_CD               " & vbNewLine _
                          & "     ,SCMH.TOKUI_CD        AS TOKUI_CD                 " & vbNewLine _
                          & "     ,SCMH.TOKUI_NM        AS TOKUI_NM                 " & vbNewLine _
                          & "     ,SCMH.BUYER_ORD_NO    AS BUYER_ORD_NO             " & vbNewLine _
                          & "     ,SCMH.HACHU_NO        AS HACHU_NO                 " & vbNewLine _
                          & "     ,SCMH.DENPYO_NO       AS DENPYO_NO                " & vbNewLine _
                          & "     ,SCMH.TENPO_CD        AS TENPO_CD                 " & vbNewLine _
                          & "     ,SCMH.CHOKUSO_KB      AS CHOKUSO_KB               " & vbNewLine _
                          & "     ,SCMH.SEIKYU_KB       AS SEIKYU_KB                " & vbNewLine _
                          & "     ,SCMH.HACHU_DATE      AS HACHU_DATE               " & vbNewLine _
                          & "     ,SCMH.CHUMON_NM       AS CHUMON_NM                " & vbNewLine _
                          & "     ,SCMH.HOL_KB          AS HOL_KB                   " & vbNewLine _
                          & "     ,SCMH.BIKO_HED1       AS BIKO_HED1                " & vbNewLine _
                          & "     ,SCMH.BIKO_HED2       AS BIKO_HED2                " & vbNewLine _
                          & "     ,SCMH.HAKO_NM         AS HAKO_NM                  " & vbNewLine _
                          & "     ,SCMH.SIIRESAKI_CD    AS SIIRESAKI_CD             " & vbNewLine _
                          & "     ,SCMH.KR_TOKUI_CD     AS KR_TOKUI_CD              " & vbNewLine _
                          & "     ,SCMH.KEIHI_KB        AS KEIHI_KB                 " & vbNewLine _
                          & "     ,SCMH.JUCHU_KB        AS JUCHU_KB                 " & vbNewLine _
                          & "     ,SCMH.DEST_NM         AS DEST_NM                  " & vbNewLine _
                          & "     ,SCMH.KIGYO_NM        AS KIGYO_NM                 " & vbNewLine _
                          & "     ,SCMH.FILLER_1        AS FILLER_1                 " & vbNewLine _
                          & "     ,SCMH.SCM_CTL_NO_L    AS SCM_CTL_NO_L             " & vbNewLine _
                          & "FROM                                                   " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_HED_BP SCMH     " & vbNewLine _
                          & "LEFT JOIN                                              " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_L SCML          " & vbNewLine _
                          & "ON                                                     " & vbNewLine _
                          & "     SCMH.SCM_CTL_NO_L = SCML.SCM_CTL_NO_L             " & vbNewLine _
                          & "WHERE                                                  " & vbNewLine _
                          & "     SCMH.SYS_DEL_FLG = '0'                            " & vbNewLine _
                          & "AND  SCML.STATUS_KBN = '01'                            " & vbNewLine _
                          & "AND  SCMH.SCM_CTL_NO_L = @SCM_CTL_NO_L                 " & vbNewLine


        SQL_SOUSHIN_SHIJI_SELECT_DATA_SCMH = SQL

    End Sub

    ' 抽出用SQL(N_OUTKASCM_DTL_BP)用
    Private SQL_SOUSHIN_SHIJI_SELECT_DATA_SCMD As String

    ''' <summary>
    ''' 抽出用SQL(N_OUTKASCM_DTL_BP)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlSoushinShijiSelectData_SCMD()

        Call Me.SetSchemaNm()

        Dim SQL As String = " SELECT                                                        " & vbNewLine _
                          & "      SCML.BR_CD           AS BR_CD                            " & vbNewLine _
                          & "     ,SCMD.CRT_DATE        AS CRT_DATE                         " & vbNewLine _
                          & "     ,SCMD.FILE_NAME       AS FILE_NAME                        " & vbNewLine _
                          & "     ,SCMD.REC_NO          AS REC_NO                           " & vbNewLine _
                          & "     ,SCMD.GYO             AS GYO                              " & vbNewLine _
                          & "     ,SCMD.DATA_KB         AS DATA_KB                          " & vbNewLine _
                          & "     ,SCMD.KITAKU_CD       AS KITAKU_CD                        " & vbNewLine _
                          & "     ,SCML.SOKO_CD         AS OUTKA_SOKO_CD                    " & vbNewLine _
                          & "     ,SCMD.ORDER_TYPE      AS ORDER_TYPE                       " & vbNewLine _
                          & "     ,SCML.OUTKA_DATE      AS OUTKA_PLAN_DATE                  " & vbNewLine _
                          & "     ,SCMD.CUST_ORD_NO     AS CUST_ORD_NO                      " & vbNewLine _
                          & "     ,SCMD.ROW_NO          AS ROW_NO                           " & vbNewLine _
                          & "     ,SCMD.ROW_TYPE        AS ROW_TYPE                         " & vbNewLine _
                          & "     ,SCMD.GOODS_CD        AS GOODS_CD                         " & vbNewLine _
                          & "     ,SCMD.GOODS_NM        AS GOODS_NM                         " & vbNewLine _
                          & "     ,SCMD.LOT_NO          AS LOT_NO                           " & vbNewLine _
                          & "     ,SCMD.PKG_NB          AS PKG_NB                           " & vbNewLine _
                          & "     ,SCMD.OUTKA_PKG_NB    AS OUTKA_PKG_NB                     " & vbNewLine _
                          & "     ,SCMD.OUTKA_NB        AS OUTKA_NB                         " & vbNewLine _
                          & "     ,SCMD.TOTAL_WT        AS TOTAL_WT                         " & vbNewLine _
                          & "     ,SCMD.TOTAL_QT        AS TOTAL_QT                         " & vbNewLine _
                          & "     ,SCMD.LOT_FLAG        AS LOT_FLAG                         " & vbNewLine _
                          & "     ,SCMD.BIKO_HED1       AS BIKO_HED1                        " & vbNewLine _
                          & "     ,SCMD.BIKO_HED2       AS BIKO_HED2                        " & vbNewLine _
                          & "     ,SCMD.BIKO_DTL        AS BIKO_DTL                         " & vbNewLine _
                          & "     ,SCMD.BUYER_GOODS_CD  AS BUYER_GOODS_CD                   " & vbNewLine _
                          & "     ,SCMD.TENPO_TANKA     AS TENPO_TANKA                      " & vbNewLine _
                          & "     ,SCMD.TENPO_KINGAKU   AS TENPO_KINGAKU                    " & vbNewLine _
                          & "     ,SCMD.JAN_CD          AS JAN_CD                           " & vbNewLine _
                          & "     ,SCMD.TENPO_BAIKA     AS TENPO_BAIKA                      " & vbNewLine _
                          & "     ,SCMD.FILLER_1        AS FILLER_1                         " & vbNewLine _
                          & "     ,SCMD.SCM_CTL_NO_L    AS SCM_CTL_NO_L                     " & vbNewLine _
                          & "     ,SCMD.SCM_CTL_NO_M    AS SCM_CTL_NO_M                     " & vbNewLine _
                          & "FROM                                                           " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_DTL_BP SCMD             " & vbNewLine _
                          & "LEFT JOIN                                                      " & vbNewLine _
                          & "     " & Me._LMNTrnSchemaNm & "N_OUTKASCM_L SCML                  " & vbNewLine _
                          & "ON                                                             " & vbNewLine _
                          & "     SCMD.SCM_CTL_NO_L = SCML.SCM_CTL_NO_L                     " & vbNewLine _
                          & "WHERE                                                          " & vbNewLine _
                          & "     SCMD.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                          & "AND  SCMD.SCM_CTL_NO_L = @SCM_CTL_NO_L                         " & vbNewLine

        SQL_SOUSHIN_SHIJI_SELECT_DATA_SCMD = SQL

    End Sub

    ''' <summary>
    ''' 営業所コードリスト取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlGetBrCd()

        Call Me.SetSchemaNm()

        With Me._row

            Me._strSql.Append("SELECT      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" BR_CD  AS BR_CD ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._LMNTrnSchemaNm)
            Me._strSql.Append("N_OUTKASCM_L      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE            ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SYS_DEL_FLG = '0'    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" STATUS_KBN = '01'    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("GROUP BY ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" BR_CD ")

        End With

    End Sub

    ''' <summary>
    ''' 送信指示時アップデート項目設定（N_OUTKASCM_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLSoushinShijiUpdateBodyN_OUTKASCM_L()

        With Me._row

            Me._strSql.Append(" STATUS_KBN = '02'")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SYS_UPD_DATE = @SYS_UPD_DATE")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SYS_UPD_TIME = @SYS_UPD_TIME")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SYS_UPD_PGID = @SYS_UPD_PGID")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SYS_UPD_USER = @SYS_UPD_USER")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" WHERE ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L")
            Me._strSql.Append(vbNewLine)

        End With

    End Sub

#End Region

#Region "実績取込 SQL"

    ''' <summary>
    '''  BP入出庫報告EDI送信データ存在チェック用SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQL_EXIST_N_SEND_BP_SELECT()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        'SQL構築
        Me._strSql.Append("SELECT                                ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("   COUNT(SCM_CTL_NO_L)  AS REC_CNT   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM                                  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_SEND_BP")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE                                 ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("    SCM_CTL_NO_L = @SCM_CTL_NO_L    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND SCM_CTL_NO_M = @SCM_CTL_NO_M    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND SYS_DEL_FLG = '0'               ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 接続データベース名取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlGetDBName()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        With Me._row

            Me._strSql.Append("SELECT                  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  KBN_NM3  AS BR_CD      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,KBN_NM4  AS IKO_FLG    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,KBN_NM5  AS NEW_DB_NM  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,KBN_NM7  AS OLD_DB_NM1 ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,KBN_NM8  AS OLD_DB_NM2 ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM                    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN                   ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE                   ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SYS_DEL_FLG = '0'      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND                     ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_GROUP_CD = 'L001'  ")
            Me._strSql.Append(vbNewLine)

        End With

    End Sub


#Region "BP入出庫報告ＥＤＩ送信データ抽出用"

    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKI_TORIKOMI_SELECT_N_SEND_L As String = " SELECT         " & vbNewLine _
                                                                 & "  HSND.DEL_KB       AS DEL_KB       " & vbNewLine _
                                                                 & " ,HSND.CRT_DATE     AS CRT_DATE       " & vbNewLine _
                                                                 & " ,HSND.FILE_NAME    AS FILE_NAME        " & vbNewLine _
                                                                 & " ,HSND.REC_NO       AS REC_NO  " & vbNewLine _
                                                                 & " ,HSND.GYO          AS GYO   " & vbNewLine _
                                                                 & " ,HSND.INOUT_KB     AS INOUT_KB          " & vbNewLine _
                                                                 & " ,HSND.NRS_BR_CD    AS NRS_BR_CD          " & vbNewLine _
                                                                 & " ,HSND.EDI_CTL_NO   AS EDI_CTL_NO          " & vbNewLine _
                                                                 & " ,HSND.EDI_CTL_NO_CHU   AS EDI_CTL_NO_CHU          " & vbNewLine _
                                                                 & " ,HSND.INOUT_CTL_NO  AS INOUT_CTL_NO          " & vbNewLine _
                                                                 & " ,HSND.INOUT_CTL_NO_CHU   AS INOUT_CTL_NO_CHU          " & vbNewLine _
                                                                 & " ,HSND.CUST_CD_L   AS CUST_CD_L          " & vbNewLine _
                                                                 & " ,HSND.CUST_CD_M  AS CUST_CD_M          " & vbNewLine _
                                                                 & " ,HSND.KITAKU_CD    AS KITAKU_CD     " & vbNewLine _
                                                                 & " ,HSND.SOKO_CD      AS SOKO_CD   " & vbNewLine _
                                                                 & " ,HSND.ORDER_TYPE   AS ORDER_TYPE      " & vbNewLine _
                                                                 & " ,HSND.INOUT_DATE   AS INOUT_DATE         " & vbNewLine _
                                                                 & " ,HSND.FILLER_1     AS FILLER_1         " & vbNewLine _
                                                                 & " ,HSND.CUST_ORD_NO  AS CUST_ORD_NO         " & vbNewLine _
                                                                 & " ,HSND.DEST_CD      AS DEST_CD         " & vbNewLine _
                                                                 & " ,HSND.MEI_NO       AS MEI_NO         " & vbNewLine _
                                                                 & " ,HSND.GOODS_CD     AS GOODS_CD         " & vbNewLine _
                                                                 & " ,HSND.SEIZO_DATE   AS SEIZO_DATE         " & vbNewLine _
                                                                 & " ,HSND.LOT_NO       AS LOT_NO   " & vbNewLine _
                                                                 & " ,HSND.SOKO_NO      AS SOKO_NO    " & vbNewLine _
                                                                 & " ,HSND.LOCA         AS LOCA         " & vbNewLine _
                                                                 & " ,HSND.INOUT_PKG_NB AS INOUT_PKG_NB         " & vbNewLine _
                                                                 & " ,HSND.BUMON_CD     AS BUMON_CD         " & vbNewLine _
                                                                 & " ,HSND.JIGYOBU_CD   AS JIGYOBU_CD        " & vbNewLine _
                                                                 & " ,HSND.TOKUI_CD     AS TOKUI_CD         " & vbNewLine _
                                                                 & " ,HSND.OKURIJO_NO   AS OKURIJO_NO         " & vbNewLine _
                                                                 & " ,HSND.FILLER_2     AS FILLER_2         " & vbNewLine _
                                                                 & " ,HSND.RECORD_STATUS   AS RECORD_STATUS          " & vbNewLine _
                                                                 & " ,HSND.JISSEKI_SHORI_FLG   AS JISSEKI_SHORI_FLG          " & vbNewLine _
                                                                 & " ,HSND.JISSEKI_USER      AS JISSEKI_USER          " & vbNewLine _
                                                                 & " ,HSND.JISSEKI_DATE     AS JISSEKI_DATE          " & vbNewLine _
                                                                 & " ,HSND.JISSEKI_TIME    AS JISSEKI_TIME          " & vbNewLine _
                                                                 & " ,HSND.SEND_USER   AS SEND_USER         " & vbNewLine _
                                                                 & " ,HSND.SEND_DATE   AS SEND_DATE          " & vbNewLine _
                                                                 & " ,HSND.SEND_TIME  AS SEND_TIME          " & vbNewLine _
                                                                 & " ,HSND.DELETE_USER   AS DELETE_USER          " & vbNewLine _
                                                                 & " ,HSND.DELETE_DATE   AS DELETE_DATE          " & vbNewLine _
                                                                 & " ,HSND.DELETE_TIME   AS DELETE_TIME          " & vbNewLine _
                                                                 & " ,HSND.DELETE_EDI_NO  AS DELETE_EDI_NO          " & vbNewLine _
                                                                 & " ,HSND.DELETE_EDI_NO_CHU   AS DELETE_EDI_NO_CHU         " & vbNewLine _
                                                                 & " ,HSND.UPD_USER      AS UPD_USER         " & vbNewLine _
                                                                 & " ,HSND.UPD_DATE      AS UPD_DATE         " & vbNewLine _
                                                                 & " ,HSND.UPD_TIME      AS UPD_TIME         " & vbNewLine _
                                                                 & " ,@SCM_CUST_CD       AS SCM_CUST_CD         " & vbNewLine


#End Region

#Region "BP入出庫報告ＥＤＩ送信データ新規登録"

    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ新規登録用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKI_TORIKOMI_INSERT_N_SEND_BP As String = "(                       " & vbNewLine _
                                                                 & "  DEL_KB                " & vbNewLine _
                                                                 & " ,CRT_DATE              " & vbNewLine _
                                                                 & " ,FILE_NAME             " & vbNewLine _
                                                                 & " ,REC_NO                " & vbNewLine _
                                                                 & " ,GYO                   " & vbNewLine _
                                                                 & " ,INOUT_KB              " & vbNewLine _
                                                                 & " ,NRS_BR_CD             " & vbNewLine _
                                                                 & " ,EDI_CTL_NO            " & vbNewLine _
                                                                 & " ,EDI_CTL_NO_CHU        " & vbNewLine _
                                                                 & " ,INOUT_CTL_NO          " & vbNewLine _
                                                                 & " ,INOUT_CTL_NO_CHU      " & vbNewLine _
                                                                 & " ,CUST_CD_L             " & vbNewLine _
                                                                 & " ,CUST_CD_M             " & vbNewLine _
                                                                 & " ,KITAKU_CD             " & vbNewLine _
                                                                 & " ,SOKO_CD               " & vbNewLine _
                                                                 & " ,ORDER_TYPE            " & vbNewLine _
                                                                 & " ,INOUT_DATE            " & vbNewLine _
                                                                 & " ,FILLER_1              " & vbNewLine _
                                                                 & " ,CUST_ORD_NO           " & vbNewLine _
                                                                 & " ,DEST_CD               " & vbNewLine _
                                                                 & " ,MEI_NO                " & vbNewLine _
                                                                 & " ,GOODS_CD              " & vbNewLine _
                                                                 & " ,SEIZO_DATE            " & vbNewLine _
                                                                 & " ,LOT_NO                " & vbNewLine _
                                                                 & " ,SOKO_NO               " & vbNewLine _
                                                                 & " ,LOCA                  " & vbNewLine _
                                                                 & " ,INOUT_PKG_NB          " & vbNewLine _
                                                                 & " ,BUMON_CD              " & vbNewLine _
                                                                 & " ,JIGYOBU_CD            " & vbNewLine _
                                                                 & " ,TOKUI_CD              " & vbNewLine _
                                                                 & " ,OKURIJO_NO            " & vbNewLine _
                                                                 & " ,FILLER_2              " & vbNewLine _
                                                                 & " ,RECORD_STATUS         " & vbNewLine _
                                                                 & " ,JISSEKI_SHORI_FLG     " & vbNewLine _
                                                                 & " ,JISSEKI_USER          " & vbNewLine _
                                                                 & " ,JISSEKI_DATE          " & vbNewLine _
                                                                 & " ,JISSEKI_TIME          " & vbNewLine _
                                                                 & " ,SEND_USER             " & vbNewLine _
                                                                 & " ,SEND_DATE             " & vbNewLine _
                                                                 & " ,SEND_TIME             " & vbNewLine _
                                                                 & " ,DELETE_USER           " & vbNewLine _
                                                                 & " ,DELETE_DATE           " & vbNewLine _
                                                                 & " ,DELETE_TIME           " & vbNewLine _
                                                                 & " ,DELETE_EDI_NO         " & vbNewLine _
                                                                 & " ,DELETE_EDI_NO_CHU     " & vbNewLine _
                                                                 & " ,UPD_USER              " & vbNewLine _
                                                                 & " ,UPD_DATE              " & vbNewLine _
                                                                 & " ,UPD_TIME              " & vbNewLine _
                                                                 & " ,SCM_CTL_NO_L          " & vbNewLine _
                                                                 & " ,SCM_CTL_NO_M          " & vbNewLine _
                                                                 & " ,SCM_CUST_CD           " & vbNewLine _
                                                                 & " ,SYS_ENT_DATE          " & vbNewLine _
                                                                 & " ,SYS_ENT_TIME          " & vbNewLine _
                                                                 & " ,SYS_ENT_PGID          " & vbNewLine _
                                                                 & " ,SYS_ENT_USER          " & vbNewLine _
                                                                 & " ,SYS_UPD_DATE          " & vbNewLine _
                                                                 & " ,SYS_UPD_TIME          " & vbNewLine _
                                                                 & " ,SYS_UPD_PGID          " & vbNewLine _
                                                                 & " ,SYS_UPD_USER          " & vbNewLine _
                                                                 & " ,SYS_DEL_FLG           " & vbNewLine _
                                                                 & " ) VALUES (             " & vbNewLine _
                                                                 & "  @DEL_KB       " & vbNewLine _
                                                                 & " ,@CRT_DATE     " & vbNewLine _
                                                                 & " ,@FILE_NAME    " & vbNewLine _
                                                                 & " ,@REC_NO       " & vbNewLine _
                                                                 & " ,@GYO                                " & vbNewLine _
                                                                 & " ,@INOUT_KB                           " & vbNewLine _
                                                                 & " ,@NRS_BR_CD                         " & vbNewLine _
                                                                 & " ,@EDI_CTL_NO                        " & vbNewLine _
                                                                 & " ,@EDI_CTL_NO_CHU               " & vbNewLine _
                                                                 & " ,@INOUT_CTL_NO                    " & vbNewLine _
                                                                 & " ,@INOUT_CTL_NO_CHU            " & vbNewLine _
                                                                 & " ,@CUST_CD_L              " & vbNewLine _
                                                                 & " ,@CUST_CD_M              " & vbNewLine _
                                                                 & " ,@KITAKU_CD         " & vbNewLine _
                                                                 & " ,@SOKO_CD        " & vbNewLine _
                                                                 & " ,@ORDER_TYPE        " & vbNewLine _
                                                                 & " ,@INOUT_DATE           " & vbNewLine _
                                                                 & " ,@FILLER_1            " & vbNewLine _
                                                                 & " ,@CUST_ORD_NO          " & vbNewLine _
                                                                 & " ,@DEST_CD             " & vbNewLine _
                                                                 & " ,@MEI_NO              " & vbNewLine _
                                                                 & " ,@GOODS_CD             " & vbNewLine _
                                                                 & " ,@SEIZO_DATE          " & vbNewLine _
                                                                 & " ,@LOT_NO          " & vbNewLine _
                                                                 & " ,@SOKO_NO          " & vbNewLine _
                                                                 & " ,@LOCA                " & vbNewLine _
                                                                 & " ,@INOUT_PKG_NB            " & vbNewLine _
                                                                 & " ,@BUMON_CD             " & vbNewLine _
                                                                 & " ,@JIGYOBU_CD          " & vbNewLine _
                                                                 & " ,@TOKUI_CD          " & vbNewLine _
                                                                 & " ,@OKURIJO_NO           " & vbNewLine _
                                                                 & " ,@FILLER_2             " & vbNewLine _
                                                                 & " ,@RECORD_STATUS            " & vbNewLine _
                                                                 & " ,@JISSEKI_SHORI_FLG             " & vbNewLine _
                                                                 & " ,@JISSEKI_USER             " & vbNewLine _
                                                                 & " ,@JISSEKI_DATE           " & vbNewLine _
                                                                 & " ,@JISSEKI_TIME            " & vbNewLine _
                                                                 & " ,@SEND_USER             " & vbNewLine _
                                                                 & " ,@SEND_DATE             " & vbNewLine _
                                                                 & " ,@SEND_TIME             " & vbNewLine _
                                                                 & " ,@DELETE_USER            " & vbNewLine _
                                                                 & " ,@DELETE_DATE             " & vbNewLine _
                                                                 & " ,@DELETE_TIME             " & vbNewLine _
                                                                 & " ,@DELETE_EDI_NO             " & vbNewLine _
                                                                 & " ,@DELETE_EDI_NO_CHU    " & vbNewLine _
                                                                 & " ,@UPD_USER             " & vbNewLine _
                                                                 & " ,@UPD_DATE            " & vbNewLine _
                                                                 & " ,@UPD_TIME             " & vbNewLine _
                                                                 & " ,@SCM_CTL_NO_L         " & vbNewLine _
                                                                 & " ,@SCM_CTL_NO_M          " & vbNewLine _
                                                                 & " ,@SCM_CUST_CD           " & vbNewLine _
                                                                 & " ,@SYS_ENT_DATE           " & vbNewLine _
                                                                 & " ,@SYS_ENT_TIME           " & vbNewLine _
                                                                 & " ,@SYS_ENT_PGID          " & vbNewLine _
                                                                 & " ,@SYS_ENT_USER          " & vbNewLine _
                                                                 & " ,@SYS_UPD_DATE       " & vbNewLine _
                                                                 & " ,@SYS_UPD_TIME           " & vbNewLine _
                                                                 & " ,@SYS_UPD_PGID           " & vbNewLine _
                                                                 & " ,@SYS_UPD_USER           " & vbNewLine _
                                                                 & " ,@SYS_DEL_FLG           " & vbNewLine _
                                                                 & " )                       " & vbNewLine

#End Region

    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ抽出用SQL作成(FROM句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiTorikomiSelectFromN_SEND_BP()

        Me._strSql.Append("FROM                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._TrnEDINm)
        Me._strSql.Append("H_SENDEDI_BP HSND          ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" HSND.JISSEKI_SHORI_FLG = '2' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" HSND.INOUT_KB = '0' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" HSND.CUST_CD_L = @LMS_CUST_CD ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" (HSND.SEND_USER = '' OR SEND_USER IS NULL) ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" NRS_BR_CD = @BR_CD ")
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" HSND.DEL_KB = '0' ")

    End Sub

    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ抽出用SQL作成(SCM管理番号取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiTorikomiSelectN_OUTKASCM_DTL_BP()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("SELECT                       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  CRT_DATE   AS CRT_DATE        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,FILE_NAME  AS FILE_NAME               ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,REC_NO     AS REC_NO               ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,GYO        AS GYO               ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SCM_CTL_NO_L  AS SCM_CTL_NO_L       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SCM_CTL_NO_M  AS SCM_CTL_NO_M       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_DTL_BP      ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SYS_DEL_FLG = '0' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" CRT_DATE = @CRT_DATE ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" FILE_NAME = @FILE_NAME ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" REC_NO = @REC_NO ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" GYO = @GYO   ")

    End Sub


    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ新規登録用SQL作成(HEAD句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiTorikomiInsertHeadN_SEND_BP()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        Me._strSql.Append("INSERT INTO                         ")
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_SEND_BP ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 出荷EDI受信明細データ更新(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiTorikomiUpdateN_OUTKASCM_DTL_BP()

        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_DTL_BP    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_SHORI_FLG = '2' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_USER = @JISSEKI_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_DATE = @JISSEKI_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_TIME = @JISSEKI_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_M = @SCM_CTL_NO_M ")

    End Sub

    ''' <summary>
    ''' 出荷EDI受信明細データ更新(N_OUTKASCM_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiTorikomiUpdateN_OUTKASCM_M()

        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_M    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_KBN = '01' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_USER = @JISSEKI_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_DATE = @JISSEKI_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_TIME = @JISSEKI_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_M = @SCM_CTL_NO_M ")

    End Sub

#End Region

#Region "実績送信 SQL"

#Region "BP入出庫報告ＥＤＩ送信データ抽出用"

    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKI_SOUSHIN_SELECT_N_SEND_BP As String = " SELECT         " & vbNewLine _
                                                                 & "  CRT_DATE     AS CRT_DATE       " & vbNewLine _
                                                                 & " ,FILE_NAME    AS FILE_NAME        " & vbNewLine _
                                                                 & " ,REC_NO       AS REC_NO  " & vbNewLine _
                                                                 & " ,GYO          AS GYO   " & vbNewLine _
                                                                 & " ,KITAKU_CD    AS KITAKU_CD     " & vbNewLine _
                                                                 & " ,SOKO_CD      AS SOKO_CD   " & vbNewLine _
                                                                 & " ,ORDER_TYPE   AS ORDER_TYPE      " & vbNewLine _
                                                                 & " ,INOUT_DATE   AS INOUT_DATE         " & vbNewLine _
                                                                 & " ,FILLER_1     AS FILLER_1         " & vbNewLine _
                                                                 & " ,CUST_ORD_NO  AS CUST_ORD_NO         " & vbNewLine _
                                                                 & " ,DEST_CD      AS DEST_CD         " & vbNewLine _
                                                                 & " ,MEI_NO       AS MEI_NO         " & vbNewLine _
                                                                 & " ,GOODS_CD     AS GOODS_CD         " & vbNewLine _
                                                                 & " ,SEIZO_DATE   AS SEIZO_DATE         " & vbNewLine _
                                                                 & " ,LOT_NO       AS LOT_NO   " & vbNewLine _
                                                                 & " ,SOKO_NO      AS SOKO_NO    " & vbNewLine _
                                                                 & " ,LOCA         AS LOCA         " & vbNewLine _
                                                                 & " ,INOUT_PKG_NB AS INOUT_PKG_NB         " & vbNewLine _
                                                                 & " ,BUMON_CD     AS BUMON_CD         " & vbNewLine _
                                                                 & " ,JIGYOBU_CD   AS JIGYOBU_CD        " & vbNewLine _
                                                                 & " ,TOKUI_CD     AS TOKUI_CD         " & vbNewLine _
                                                                 & " ,OKURIJO_NO   AS OKURIJO_NO         " & vbNewLine _
                                                                 & " ,FILLER_2     AS FILLER_2         " & vbNewLine _
                                                                 & " ,SCM_CTL_NO_L      AS SCM_CTL_NO_L           " & vbNewLine _
                                                                 & " ,SCM_CTL_NO_M      AS SCM_CTL_NO_M          " & vbNewLine _
                                                                 & " ,SCM_CUST_CD       AS SCM_CUST_CD        " & vbNewLine _
                                                                 & " ,NRS_BR_CD       AS NRS_BR_CD        " & vbNewLine


#End Region

    ''' <summary>
    ''' BP入出庫報告ＥＤＩ送信データ抽出用SQL作成(FROM句)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinSelectFromN_SEND_BP()

        Call Me.SetSchemaNm()

        Me._strSql.Append("FROM                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_SEND_BP ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_SHORI_FLG = '2' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" INOUT_KB = '0' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" CUST_CD_L = (SELECT ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" KBN_NM5    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._MstSchemaNm)
        Me._strSql.Append("Z_KBN  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" KBN_GROUP_CD = 'S033'   ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" KBN_CD = '00'    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(")  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SEND_USER = '' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" DEL_KB = '0' ")

    End Sub

    ''' <summary>
    ''' BP出荷EDI受信送信データ（N_SEND_BP）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinUpdateN_SEND_BP()

        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_SEND_BP    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_SHORI_FLG = '3' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_USER = @SEND_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_DATE = @SEND_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_TIME = @SEND_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_M = @SCM_CTL_NO_M ")

    End Sub

    ''' <summary>
    ''' BP出荷EDI受信明細データ（N_OUTKASCM_DTL_BP）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinUpdateN_OUTKASCM_DTL_BP()

        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_DTL_BP    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_SHORI_FLG = '3' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_USER = @SEND_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_DATE = @SEND_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_TIME = @SEND_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_M = @SCM_CTL_NO_M ")

    End Sub

    ''' <summary>
    ''' 出荷EDIデータM(N_OUTKASCM_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinUpdateN_OUTKASCM_M()

        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_M    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_KBN = '02' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_USER = @JISSEKI_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_DATE = @JISSEKI_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",JISSEKI_TIME = @JISSEKI_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_M = @SCM_CTL_NO_M ")

    End Sub

    ''' <summary>
    ''' 出荷EDIデータL(N_OUTKASCM_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinUpdateN_OUTKASCM_L()

        Call Me.SetSchemaNm()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_L    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" STATUS_KBN = '03' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_DATE = @SYS_UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_TIME = @SYS_UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_PGID = @SYS_UPD_PGID    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SYS_UPD_USER = @SYS_UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" SCM_CTL_NO_L = @SCM_CTL_NO_L ")
        Me._strSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 営業所コードリスト取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlJissekiSoushinGetBrCd()

        Call Me.SetSchemaNm()

        With Me._row

            Me._strSql.Append("SELECT      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" BR_CD  AS BR_CD ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,@SCM_CUST_CD  AS SCM_CUST_CD ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._LMNTrnSchemaNm)
            Me._strSql.Append("N_SEND_BP      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" JISSEKI_SHORI_FLG = '2' ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" INOUT_KB = '0' ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" CUST_CD_L = (SELECT ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_NM3    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM     ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_GROUP_CD = 'S033'   ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_CD = '00'    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(")  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SEND_USER = '' ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" DEL_KB = '0' ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("GROUP BY ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" BR_CD ")

        End With

    End Sub

    ''' <summary>
    ''' 接続データベース名取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSqlJissekiSoushinGetBR_CD_LIST()

        'スキーマ名設定
        Call Me.SetSchemaNm()

        With Me._row

            Me._strSql.Append("SELECT           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_NM1  AS BR_CD           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",KBN_NM3  AS SV1_LM_MST           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",KBN_NM4  AS SV1_LM_TRN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",KBN_NM7  AS SV2_LM_MST           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",KBN_NM8  AS SV2_LM_TRN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",(SELECT           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM4           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  FROM           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  WHERE           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_GROUP_CD = 'L001'           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  AND           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM3 = @BR_CD)         AS IKO_FLG           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",(SELECT           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM5           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  FROM           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  WHERE           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_GROUP_CD = 'L001'           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  AND           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM3 = @BR_CD)         AS SV2_CONNECT_NM           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",(SELECT           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM7           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  FROM           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  WHERE           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_GROUP_CD = 'L001'           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  AND           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM3 = @BR_CD)         AS SV1_CONNECT_NM           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",(SELECT           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM5       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  FROM                 ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  WHERE           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_GROUP_CD = 'S033'           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  AND           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM4 = @BR_CD           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  AND           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("       KBN_NM3 = @SCM_CUST_CD)   AS LMS_CUST_CD           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(",@SCM_CUST_CD  AS SCM_CUST_CD           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstSchemaNm)
            Me._strSql.Append("Z_KBN           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_GROUP_CD = 'D003'           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND           ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" KBN_CD = @BR_CD           ")

        End With

    End Sub

    ''' <summary>
    ''' (LMS)BP出荷EDI受信ヘッダデータ(H_OUTKAEDI_DTL_BP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinUpdateH_OUTKAEDI_DTL_BP()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._TrnEDINm)
        Me._strSql.Append("H_OUTKAEDI_DTL_BP    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_SHORI_FLG = '3' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_USER = @SEND_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_DATE = @SEND_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_TIME = @SEND_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",UPD_USER = @UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",UPD_DATE = @UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",UPD_TIME = @UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" DEL_KB = '0'                    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" CRT_DATE = @CRT_DATE ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" FILE_NAME = @FILE_NAME ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" REC_NO = @REC_NO ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" GYO = @GYO                   ")

    End Sub

    ''' <summary>
    ''' (LMS)BP入出庫報告EDI送信データ(H_SENDEDI_BP)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinUpdateH_SENDEDI_BP()

        Me._strSql.Append("UPDATE                        ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._TrnEDINm)
        Me._strSql.Append("H_SENDEDI_BP    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SET     ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" JISSEKI_SHORI_FLG = '3' ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_USER = @SEND_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_DATE = @SEND_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",SEND_TIME = @SEND_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",UPD_USER = @UPD_USER    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",UPD_DATE = @UPD_DATE    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(",UPD_TIME = @UPD_TIME    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE  ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" DEL_KB = '0'                    ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" CRT_DATE = @CRT_DATE ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" FILE_NAME = @FILE_NAME ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" REC_NO = @REC_NO ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND                           ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" GYO = @GYO                   ")

    End Sub

    ''' <summary>
    ''' SCM側出荷日、納入日取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinCheckSCMDate()

        Call Me.SetSchemaNm()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  SEND.CRT_DATE   AS CRT_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SEND.FILE_NAME  AS FILE_NAME       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SEND.REC_NO     AS REC_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SEND.GYO        AS GYO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SEND.SCM_CTL_NO_L   AS SCM_CTL_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SEND.SCM_CTL_NO_M   AS SCM_CTL_NO_M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SCML.OUTKA_DATE     AS SCM_OUTKA_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,SCML.ARR_DATE       AS SCM_ARR_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._LMNTrnSchemaNm)
            Me._strSql.Append("N_SEND_BP SEND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("LEFT JOIN         ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._LMNTrnSchemaNm)
            Me._strSql.Append("N_OUTKASCM_L SCML       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SCML.SYS_DEL_FLG = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SCML.SCM_CTL_NO_L = SEND.SCM_CTL_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SEND.SYS_DEL_FLG = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SEND.CRT_DATE = @CRT_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SEND.FILE_NAME = @FILE_NAME       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SEND.REC_NO = @REC_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" SEND.GYO = @GYO       ")

        End With

    End Sub

    ''' <summary>
    ''' (LMS)LMS側出荷日、納入日取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLJissekiSoushinCheckLMSDate()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("  HEDIH.CRT_DATE           AS CRT_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,HEDIH.FILE_NAME          AS FILE_NAME       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,HEDIH.REC_NO             AS REC_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,@GYO                     AS GYO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,HEDIL.OUTKA_PLAN_DATE    AS LMS_OUTKA_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ,HEDIL.ARR_PLAN_DATE      AS LMS_ARR_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_OUTKAEDI_HED_BP HEDIH       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("LEFT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_OUTKAEDI_L HEDIL       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIL.DEL_KB = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIL.EDI_CTL_NO = HEDIH.EDI_CTL_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.DEL_KB = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.CRT_DATE = @CRT_DATE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.FILE_NAME = @FILE_NAME       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" HEDIH.REC_NO = @REC_NO       ")

        End With

    End Sub

#End Region

#Region "欠品状態更新 SQL"

    ''' <summary>
    ''' 荷主商品コード取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLUpdKeppinJoutaiN_OUTKASCM_M()

        Call Me.SetSchemaNm()

        Me._strSql.Append("SELECT       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  SCM_CTL_NO_L   AS SCM_CTL_NO_L       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SCM_CTL_NO_M   AS SCM_CTL_NO_M       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,CUST_GOODS_CD  AS CUST_GOODS_CD      ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._LMNTrnSchemaNm)
        Me._strSql.Append("N_OUTKASCM_M       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SCM_CTL_NO_L = @SCM_CTL_NO_L       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("AND       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("SYS_DEL_FLG = '0'       ")

    End Sub

#End Region

#End Region

#Region "Method"

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

    End Sub

#End Region 'Constructor

#Region "検索処理"

    ''' <summary>
    ''' 出荷EDI対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷EDI対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        'IN情報のステータス区分コード取得
        Dim StatusKbn As String = Me._row.Item("HED_STATUS_KBN").ToString

        'SQL作成
        Call Me.CreateSqlSelectData_SCML()                'SELECT_SCML SQL作成
        Call Me.CreateSqlSelectData_SCMH()                'SELECT_SCMH SQL作成
        Me._strSql.Append(LMN010DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._strSql.Append(SQL_SELECT_DATA_SCML)           'SQL構築(N_OUTKASCM_L)
        Call Me.SetConditionMasterSQL_KensakuSCML()       '抽出条件設定(N_OUTKASCM_L)

        If (String.IsNullOrEmpty(StatusKbn)) Or (StatusKbn = KbnCdMisettei) Then

            Me._strSql.Append(SQL_SELECT_DATA_SCMH)           'SQL構築(N_OUTKASCM_HED_BP)
            Call Me.SetConditionMasterSQL_KensakuSCMH()       '抽出条件設定(N_OUTKASCM_HED_BP)

        End If


        Me._strSql.Append(") MAIN")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDI対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷EDI対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        'IN情報のステータス区分コード取得
        Dim StatusKbn As String = Me._row.Item("HED_STATUS_KBN").ToString

        'SQL作成
        Call Me.CreateSqlSelectData_SCML()                'SELECT_SCML SQL作成
        Call Me.CreateSqlSelectData_SCMH()                'SELECT_SCMH SQL作成
        Me._strSql.Append(SQL_SELECT_DATA_SCML)           'SQL構築(N_OUTKASCM_L)
        Call Me.SetConditionMasterSQL_KensakuSCML()       '抽出条件設定(N_OUTKASCM_L)

        If (String.IsNullOrEmpty(StatusKbn)) Or (StatusKbn = KbnCdMisettei) Then

            Me._strSql.Append(SQL_SELECT_DATA_SCMH)           'SQL構築(N_OUTKASCM_HED_BP)
            Call Me.SetConditionMasterSQL_KensakuSCMH()       '抽出条件設定(N_OUTKASCM_HED_BP)

        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")
        map.Add("STATUS_KBN", "STATUS_KBN")
        map.Add("BR_CD", "BR_CD")
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("CUST_ORD_NO_L", "CUST_ORD_NO_L")
        map.Add("MOUSHIOKURI_KBN", "MOUSHIOKURI_KBN")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("ARR_DATE", "ARR_DATE")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("REMARK1", "REMARK1")
        map.Add("REMARK2", "REMARK2")
        map.Add("DTL_CNT", "DTL_CNT")
        map.Add("EDI_DATETIME", "EDI_DATETIME")
        map.Add("INSERT_FLG", "INSERT_FLG")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("HED_BP_SYS_UPD_DATE", "HED_BP_SYS_UPD_DATE")
        map.Add("HED_BP_SYS_UPD_TIME", "HED_BP_SYS_UPD_TIME")
        map.Add("L_SYS_UPD_DATE", "L_SYS_UPD_DATE")
        map.Add("L_SYS_UPD_TIME", "L_SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN010OUT")

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)LMS側出荷日、納入日を取得(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function KensakuGetLMSDateLMSVer1(ByVal ds As DataSet) As DataSet
        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010OUT")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLKensakuGetLMSDate()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamKensakuGetLMSDate()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN010DAC", "KensakuGetLMSDateLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("REC_NO", "REC_NO")
                map.Add("LMS_OUTKA_DATE", "LMS_OUTKA_DATE")
                map.Add("LMS_ARR_DATE", "LMS_ARR_DATE")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMS_DATE")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)LMS側出荷日、納入日を取得(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function KensakuGetLMSDateLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010OUT")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLKensakuGetLMSDate()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamKensakuGetLMSDate()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "KensakuGetLMSDateLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("LMS_OUTKA_DATE", "LMS_OUTKA_DATE")
        map.Add("LMS_ARR_DATE", "LMS_ARR_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMS_DATE")

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' 検索時条件設定（N_OUTKASCCM_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_KensakuSCML()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._row

            whereStr = .Item("HED_STATUS_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.STATUS_KBN = @HED_STATUS_KBN")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_STATUS_KBN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_SCM_CUST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.SCM_CUST_CD = @HED_SCM_CUST_CD")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_SCM_CUST_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_EDI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.EDI_DATE >= @HED_EDI_DATE_FROM")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_EDI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_EDI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.EDI_DATE <= @HED_EDI_DATE_TO")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_EDI_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_OUTKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.OUTKA_DATE >= @HED_OUTKA_DATE_FROM")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_OUTKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_OUTKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.OUTKA_DATE <= @HED_OUTKA_DATE_TO")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_OUTKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_ARR_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.ARR_DATE >= @HED_ARR_DATE_FROM")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_ARR_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_ARR_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.ARR_DATE <= @HED_ARR_DATE_TO")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_ARR_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SOKO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.SOKO_CD = @SOKO_CD")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.CUST_ORD_NO_L LIKE @CUST_ORD_NO_L")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("MOUSHIOKURI_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.MOUSHIOKURI_KBN = @MOUSHIOKURI_KBN")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@MOUSHIOKURI_KBN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.DEST_NM LIKE @DEST_NM")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_AD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.DEST_AD LIKE @DEST_AD")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_ZIP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.DEST_ZIP LIKE @DEST_ZIP")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("REMARK1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.REMARK1 LIKE @REMARK1")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REMARK1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("REMARK2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCML.REMARK2 LIKE @REMARK2")
                Me._strSql.Append(vbNewLine)
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REMARK2", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 検索時条件設定（N_OUTKASCM_HED_BP）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_KensakuSCMH()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._row

            Me._strSql.Append(" AND SCMH.SCM_CTL_NO_L = '' ")
            Me._strSql.Append(vbNewLine)

            whereStr = .Item("HED_EDI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.EDI_DATE >= @HED_EDI_DATE_FROM")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_EDI_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HED_EDI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.EDI_DATE <= @HED_EDI_DATE_TO")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_EDI_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HED_OUTKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.OUTKA_PLAN_DATE >= @HED_OUTKA_DATE_FROM")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_OUTKA_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HED_OUTKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.OUTKA_PLAN_DATE <= @HED_OUTKA_DATE_TO")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_OUTKA_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HED_ARR_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.ARR_PLAN_DATE >= @HED_ARR_DATE_FROM")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_ARR_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("HED_ARR_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.ARR_PLAN_DATE <= @HED_ARR_DATE_TO")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_ARR_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.DENPYO_NO LIKE @CUST_ORD_NO_L")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("MOUSHIOKURI_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.MOSIOKURI_KB = (SELECT KBN3.KBN_NM1")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append(" FROM " & Me._MstSchemaNm & "Z_KBN KBN3")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append(" WHERE")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append(" KBN3.KBN_GROUP_CD = 'M008'")
                Me._strSql.Append(vbNewLine)
                Me._strSql.Append(" AND KBN3.KBN_CD = @MOUSHIOKURI_KBN )")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@MOUSHIOKURI_KBN", whereStr, DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.DEST_NM1 + SCMH.DEST_NM2 LIKE @DEST_NM")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_AD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND DEST_AD1 + DEST_AD2 LIKE @DEST_AD")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_ZIP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.DEST_ZIP LIKE @DEST_ZIP")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("REMARK1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.BIKO_HED1 LIKE @REMARK1")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REMARK1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("REMARK2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._strSql.Append(" AND SCMH.BIKO_HED2 LIKE @REMARK2")
                Me._strSql.Append(vbNewLine)
                'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REMARK2", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(LMS側日付取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamKensakuGetLMSDate()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 出荷EDIデータL存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckExistN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQL_EXIST_N_OUTKASCM_L_SELECT()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'Select結果保持用
        Dim selectCnt As Integer = 0

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamExistChkN_OUTKASCM_L()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "CheckExistN_OTUKASCM_L", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            selectCnt = Convert.ToInt32(reader("REC_CNT"))
            reader.Close()

            If selectCnt > 0 Then
                MyBase.SetResultCount(selectCnt)
                Exit For
            End If

        Next

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckExistN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQL_EXIST_N_OUTKASCM_HED_BP_SELECT()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'Select結果保持用
        Dim selectCnt As Integer = 0

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamHaitaChkN_OUTKASCM_HED_BP()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "CheckExistN_OUTKASCM_HED_BP", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            selectCnt = Convert.ToInt32(reader("REC_CNT"))
            reader.Close()

            If selectCnt > 0 Then
                MyBase.SetResultCount(selectCnt)
                Exit For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQL_EXIST_N_OUTKASCM_L_SELECT()
        Me._strSql.Append("AND SYS_UPD_DATE = @L_SYS_UPD_DATE ")
        Me._strSql.Append("AND SYS_UPD_TIME = @L_SYS_UPD_TIME ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamHaitaChkN_OUTKASCM_L()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "CheckHaitaN_OUTKASCM_L", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            If MyBase.GetResultCount = 0 _
            OrElse i = max Then
                '処理件数の設定
                reader.Read()
                MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
                reader.Close()
                Exit For
            End If

            reader.Close()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQL_EXIST_N_OUTKASCM_HED_BP_SELECT()
        Me._strSql.Append("AND SYS_UPD_DATE = @HED_BP_SYS_UPD_DATE ")
        Me._strSql.Append("AND SYS_UPD_TIME = @HED_BP_SYS_UPD_TIME ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamHaitaChkN_OUTKASCM_HED_BP()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "CheckHaitaN_OUTKASCM_HED_BP", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            If MyBase.GetResultCount = 0 _
            OrElse i = max Then
                '処理件数の設定
                reader.Read()
                MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
                reader.Close()
                Exit For
            End If

            reader.Close()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetteiInsertN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLSetteiInsertHeadN_OUTKASCM_L()
        Me._strSql.Append(LMN010DAC.SQL_SETTEI_INSERT_N_OUTKASCM_L)
        Call Me.SQLSetteiInsertSelectN_OUTKASCM_L()
        Call Me.SQLSetteiInsertFromN_OUTKASCM_L()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertSCML()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010", "SetteiInsertN_OUTKASCM_L", cmd)

            'SQLの発行
            insCnt = insCnt + MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(insCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータM新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetteiInsertN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_OUTKASCM_DTL_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLSetteiInsertHeadN_OUTKASCM_M()
        Me._strSql.Append(LMN010DAC.SQL_SETTEI_INSERT_N_OUTKASCM_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertSCMM()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010", "SetteiInsertN_OUTKASCM_M", cmd)

            'SQLの発行
            insCnt = insCnt + MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(insCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetteiUpdateN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLSetteiUpdateHeadN_OUTKASCM_L()
        Call Me.SQLSetteiUpdateBodyN_OUTKASCM_L()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdateN_OUTKASCM_L()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "SetteiUpdateN_OUTKASCM_L", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetteiUpdateN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLSetteiUpdateHeadN_OUTKASCM_HED_BP()
        Me._strSql.Append(LMN010DAC.SQL_SETTEI_UPDATE_N_OUTKASCM_HED_BP)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdateN_OUTKASCM_HED_BP()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "SetteiUpdateN_OUTKASCM_HED_BP", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ抽出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetteiSelectN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlSetteiSelectData_SCMD()
        Me._strSql.Append(SQL_SELECT_DATA_SCMD)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSelectN_OUTKASCM_DTL_BP()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "SetteiSelectN_OUTKASCM_DTL_BP", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("TOTAL_WT", "TOTAL_WT")
        map.Add("TOTAL_QT", "TOTAL_QT")
        map.Add("BIKO_HED1", "BIKO_HED1")
        map.Add("BIKO_HED2", "BIKO_HED2")
        map.Add("BIKO_DTL", "BIKO_DTL")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_OUTKASCM_DTL_BP")

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetteiUpdateN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_OUTKASCM_DTL_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLSetteiUpdateHeadN_OUTKASCM_DTL_BP()
        Me._strSql.Append(LMN010DAC.SQL_SETTEI_UPDATE_N_OUTKASCM_DTL_BP)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamUpdateN_OUTKASCM_DTL_BP()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "SetteiUpdateN_OUTKASCM_DTL_BP", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function


#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(マスタ存在チェック(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChkN_OUTKASCM_L()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChkN_OUTKASCM_L()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@L_SYS_UPD_DATE", .Item("L_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@L_SYS_UPD_TIME", .Item("L_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック(N_OUTKASCM_HED_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChkN_OUTKASCM_HED_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row

            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_BP_SYS_UPD_DATE", .Item("HED_BP_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_BP_SYS_UPD_TIME", .Item("HED_BP_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertSCML()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_SCM_CUST_CD", .Item("HED_SCM_CUST_CD").ToString(), DBDataType.CHAR))

            '倉庫コード設定
            If String.IsNullOrEmpty(.Item("HED_SOKO_CD").ToString) Then
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Else
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("HED_SOKO_CD").ToString(), DBDataType.CHAR))
            End If

            '出荷日設定
            If String.IsNullOrEmpty(.Item("HED_OUTKA_DATE").ToString) Then
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.CHAR))
            Else
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("HED_OUTKA_DATE").ToString(), DBDataType.CHAR))
            End If

            '納入日設定
            If String.IsNullOrEmpty(.Item("HED_ARR_DATE").ToString) Then
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@ARR_DATE", .Item("ARR_DATE").ToString(), DBDataType.CHAR))
            Else
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@ARR_DATE", .Item("HED_ARR_DATE").ToString(), DBDataType.CHAR))
            End If

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))

            Call Me.SetParamCommonSystemIns()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録(N_OUTKASCM_M))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertSCMM()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", .Item("ROW_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", .Item("GOODS_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Item("OUTKA_PKG_NB").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NB", .Item("OUTKA_NB").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@TOTAL_WT", .Item("TOTAL_WT").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@TOTAL_QT", .Item("TOTAL_QT").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BIKO_HED1", .Item("BIKO_HED1").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BIKO_HED2", .Item("BIKO_HED2").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BIKO_DTL", .Item("BIKO_DTL").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemIns()
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateN_OUTKASCM_L()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            whereStr = .Item("HED_SOKO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_SOKO_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_OUTKA_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_OUTKA_DATE", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("HED_ARR_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._sqlPrmList.Add(MyBase.GetSqlParameter("@HED_ARR_DATE", whereStr, DBDataType.CHAR))
            End If

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用(N_OUTKASCM_HED_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateN_OUTKASCM_HED_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("HED_SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectN_OUTKASCM_DTL_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("HED_SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateN_OUTKASCM_DTL_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.NVARCHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

#End Region

#End Region

#Region "送信指示"

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ抽出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiSelectN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlSoushinShijiSelectData_SCMH()
        Me._strSql.Append(SQL_SOUSHIN_SHIJI_SELECT_DATA_SCMH)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSoushinShijiSelectN_OUTKASCM_DTL_BP()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "SoushinShijiSelectN_OUTKASCM_HED_BP", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("BR_CD", "BR_CD")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("DATA_KB", "DATA_KB")
        map.Add("KITAKU_CD", "KITAKU_CD")
        map.Add("OUTKA_SOKO_CD", "OUTKA_SOKO_CD")
        map.Add("ORDER_TYPE", "ORDER_TYPE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("DEST_NM1", "DEST_NM1")
        map.Add("DEST_NM2", "DEST_NM2")
        map.Add("DEST_AD1", "DEST_AD1")
        map.Add("DEST_AD2", "DEST_AD2")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HT_DATE", "HT_DATE")
        map.Add("HT_TIME", "HT_TIME")
        map.Add("HT_CAR_NO", "HT_CAR_NO")
        map.Add("HT_DRIVER", "HT_DRIVER")
        map.Add("HT_UNSO_CO", "HT_UNSO_CO")
        map.Add("MOSIOKURI_KB", "MOSIOKURI_KB")
        map.Add("BUMON_CD", "BUMON_CD")
        map.Add("JIGYOBU_CD", "JIGYOBU_CD")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("TOKUI_NM", "TOKUI_NM")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("HACHU_NO", "HACHU_NO")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("TENPO_CD", "TENPO_CD")
        map.Add("CHOKUSO_KB", "CHOKUSO_KB")
        map.Add("SEIKYU_KB", "SEIKYU_KB")
        map.Add("HACHU_DATE", "HACHU_DATE")
        map.Add("CHUMON_NM", "CHUMON_NM")
        map.Add("HOL_KB", "HOL_KB")
        map.Add("BIKO_HED1", "BIKO_HED1")
        map.Add("BIKO_HED2", "BIKO_HED2")
        map.Add("HAKO_NM", "HAKO_NM")
        map.Add("SIIRESAKI_CD", "SIIRESAKI_CD")
        map.Add("KR_TOKUI_CD", "KR_TOKUI_CD")
        map.Add("KEIHI_KB", "KEIHI_KB")
        map.Add("JUCHU_KB", "JUCHU_KB")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("KIGYO_NM", "KIGYO_NM")
        map.Add("FILLER_1", "FILLER_1")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_OUTKASCM_HED_BP")

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ抽出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiSelectN_OTUKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_OUTKASCM_HED_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.CreateSqlSoushinShijiSelectData_SCMD()
        Me._strSql.Append(SQL_SOUSHIN_SHIJI_SELECT_DATA_SCMD)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Call Me.SetParamSoushinShijiSelectN_OUTKASCM_DTL_BP()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "SoushinShijiSelectN_OTUKASCM_DTL_BP", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("BR_CD", "BR_CD")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("DATA_KB", "DATA_KB")
        map.Add("KITAKU_CD", "KITAKU_CD")
        map.Add("OUTKA_SOKO_CD", "OUTKA_SOKO_CD")
        map.Add("ORDER_TYPE", "ORDER_TYPE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("ROW_TYPE", "ROW_TYPE")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_NB", "OUTKA_NB")
        map.Add("TOTAL_WT", "TOTAL_WT")
        map.Add("TOTAL_QT", "TOTAL_QT")
        map.Add("LOT_FLAG", "LOT_FLAG")
        map.Add("BIKO_HED1", "BIKO_HED1")
        map.Add("BIKO_HED2", "BIKO_HED2")
        map.Add("BIKO_DTL", "BIKO_DTL")
        map.Add("BUYER_GOODS_CD", "BUYER_GOODS_CD")
        map.Add("TENPO_TANKA", "TENPO_TANKA")
        map.Add("TENPO_KINGAKU", "TENPO_KINGAKU")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("TENPO_BAIKA", "TENPO_BAIKA")
        map.Add("FILLER_1", "FILLER_1")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_OUTKASCM_DTL_BP")

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiUpdateN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_OUTKASCM_HED_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLSetteiUpdateHeadN_OUTKASCM_L()
        Call Me.SQLSoushinShijiUpdateBodyN_OUTKASCM_L()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamSoushinShijiUpdateN_OUTKASCM_L()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "SoushinShijiUpdateN_OUTKASCM_L", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(抽出用(N_OUTKASCM_HED_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSoushinShijiSelectN_OUTKASCM_HED_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSoushinShijiSelectN_OUTKASCM_DTL_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSoushinShijiUpdateN_OUTKASCM_L()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

#End Region

#End Region

#Region "実績取込"

    ''' <summary>
    ''' BP入出庫報告EDI送信データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckExistN_SEND_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQL_EXIST_N_SEND_BP_SELECT()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'Select結果保持用
        Dim selectCnt As Integer = 0

        Dim reader As SqlDataReader = Nothing

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamExistChkN_SEND_BP()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "CheckExistN_SEND_BP", cmd)

            'SQLの発行
            reader = MyBase.GetSelectResult(cmd)

            cmd.Parameters.Clear()

            '処理件数の設定
            reader.Read()
            selectCnt = Convert.ToInt32(reader("REC_CNT"))
            reader.Close()

            If selectCnt > 0 Then
                MyBase.SetResultCount(selectCnt)
                Exit For
            End If

        Next

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)実績取込データ取得(N_SEND_BP)(LMSVer1)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiSelectN_SEND_BPLMSVer1(ByVal ds As DataSet) As DataSet
        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("BR_CD_LIST")

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL構築
                Me._strSql.Append(LMN010DAC.SQL_JISSEKI_TORIKOMI_SELECT_N_SEND_L)     'SQL構築
                Call Me.SQLJissekiTorikomiSelectFromN_SEND_BP()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'SQLパラメータ初期化/設定
                Call Me.SetParamSelectSEND()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN010", "JissekiTorikomiSelectN_SEND_BPLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("DEL_KB", "DEL_KB")
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("REC_NO", "REC_NO")
                map.Add("GYO", "GYO")
                map.Add("INOUT_KB", "INOUT_KB")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("EDI_CTL_NO", "EDI_CTL_NO")
                map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
                map.Add("INOUT_CTL_NO", "INOUT_CTL_NO")
                map.Add("INOUT_CTL_NO_CHU", "INOUT_CTL_NO_CHU")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("KITAKU_CD", "KITAKU_CD")
                map.Add("SOKO_CD", "SOKO_CD")
                map.Add("ORDER_TYPE", "ORDER_TYPE")
                map.Add("INOUT_DATE", "INOUT_DATE")
                map.Add("FILLER_1", "FILLER_1")
                map.Add("CUST_ORD_NO", "CUST_ORD_NO")
                map.Add("DEST_CD", "DEST_CD")
                map.Add("MEI_NO", "MEI_NO")
                map.Add("GOODS_CD", "GOODS_CD")
                map.Add("SEIZO_DATE", "SEIZO_DATE")
                map.Add("LOT_NO", "LOT_NO")
                map.Add("SOKO_NO", "SOKO_NO")
                map.Add("LOCA", "LOCA")
                map.Add("INOUT_PKG_NB", "INOUT_PKG_NB")
                map.Add("BUMON_CD", "BUMON_CD")
                map.Add("JIGYOBU_CD", "JIGYOBU_CD")
                map.Add("TOKUI_CD", "TOKUI_CD")
                map.Add("OKURIJO_NO", "OKURIJO_NO")
                map.Add("FILLER_2", "FILLER_2")
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
                map.Add("UPD_USER", "UPD_USER")
                map.Add("UPD_DATE", "UPD_DATE")
                map.Add("UPD_TIME", "UPD_TIME")
                map.Add("SCM_CUST_CD", "SCM_CUST_CD")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_SEND_BP")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)実績取込データ取得(N_SEND_BP)(LMSVer2)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiSelectN_SEND_BPLMSVer2(ByVal ds As DataSet) As DataSet
        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("BR_CD_LIST")

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Me._strSql.Append(LMN010DAC.SQL_JISSEKI_TORIKOMI_SELECT_N_SEND_L)     'SQL構築
        Call Me.SQLJissekiTorikomiSelectFromN_SEND_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamSelectSEND()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010", "JissekiTorikomiSelectN_SEND_BPLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INOUT_CTL_NO", "INOUT_CTL_NO")
        map.Add("INOUT_CTL_NO_CHU", "INOUT_CTL_NO_CHU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("KITAKU_CD", "KITAKU_CD")
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("ORDER_TYPE", "ORDER_TYPE")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("FILLER_1", "FILLER_1")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("MEI_NO", "MEI_NO")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("SEIZO_DATE", "SEIZO_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SOKO_NO", "SOKO_NO")
        map.Add("LOCA", "LOCA")
        map.Add("INOUT_PKG_NB", "INOUT_PKG_NB")
        map.Add("BUMON_CD", "BUMON_CD")
        map.Add("JIGYOBU_CD", "JIGYOBU_CD")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("OKURIJO_NO", "OKURIJO_NO")
        map.Add("FILLER_2", "FILLER_2")
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
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_SEND_BP")

        Return ds

    End Function

    ''' <summary>
    ''' 実績取込データ取得(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiSelectN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiTorikomiSelectN_OUTKASCM_DTL_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamSelectSCMD()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010", "JissekiTorikomiSelectN_OUTKASCM_DTL_BP", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_OUTKASCM_DTL_BP")

        Return ds

    End Function

    ''' <summary>
    ''' 実績取込データ取込(N_SEND_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiInsertN_SEND_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiTorikomiInsertHeadN_SEND_BP()
        Me._strSql.Append(SQL_JISSEKI_TORIKOMI_INSERT_N_SEND_BP)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertSEND()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010", "JissekiTorikomiInsertN_SEND_BP", cmd)

            'SQLの発行
            insCnt = insCnt + MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(insCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDI受信明細データ更新(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiUpdateN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiTorikomiUpdateN_OUTKASCM_DTL_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiTorikomiUpdateSCMD()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiTorikomiUpdateN_OUTKASCM_DTL_BP", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータM更新(N_OUTKASCM_M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiUpdateN_OUTKASCM_M(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiTorikomiUpdateN_OUTKASCM_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiTorikomiUpdateSCMM()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiTorikomiUpdateN_OUTKASCM_M", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds


    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(存在チェック用(N_SEND_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChkN_SEND_BP()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出用(N_SEND_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectSEND()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(抽出用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectSCMD()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録用(N_SEND_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertSEND()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", .Item("INOUT_KB").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_CTL_NO", .Item("INOUT_CTL_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_CTL_NO_CHU", .Item("INOUT_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@KITAKU_CD", .Item("KITAKU_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", .Item("ORDER_TYPE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_DATE", .Item("INOUT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILLER_1", .Item("FILLER_1").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@MEI_NO", .Item("MEI_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD", .Item("GOODS_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEIZO_DATE", .Item("SEIZO_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_NO", .Item("SOKO_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_PKG_NB", .Item("INOUT_PKG_NB").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BUMON_CD", .Item("BUMON_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JIGYOBU_CD", .Item("JIGYOBU_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@TOKUI_CD", .Item("TOKUI_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OKURIJO_NO", .Item("OKURIJO_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILLER_2", .Item("FILLER_2").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", .Item("RECORD_STATUS").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", .Item("JISSEKI_SHORI_FLG").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", .Item("SEND_USER").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", .Item("SEND_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", .Item("SEND_TIME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", .Item("DELETE_USER").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", .Item("DELETE_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", .Item("DELETE_TIME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", .Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", .Item("DELETE_EDI_NO_CHU").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", .Item("UPD_USER").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", .Item("UPD_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", .Item("UPD_TIME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemIns()

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiTorikomiUpdateSCMD()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.NVARCHAR))
            Call Me.SetParamCommonSystemUpd()

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(N_OUTKASCM_M))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiTorikomiUpdateSCMM()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.GetSystemDate() & "", DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", (Me.GetSystemTime() & "").Substring(0, 8), DBDataType.NVARCHAR))
            Call Me.SetParamCommonSystemUpd()

        End With



    End Sub

#End Region

#End Region

#Region "実績送信"

    ''' <summary>
    ''' 実績送信データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinSelectN_SEND_BP(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Me._strSql.Append(LMN010DAC.SQL_JISSEKI_SOUSHIN_SELECT_N_SEND_BP)     'SQL構築
        Call Me.SQLJissekiSoushinSelectFromN_SEND_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())


        MyBase.Logger.WriteSQLLog("LMN010", "JissekiSoushinSelectN_SEND_BP", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("KITAKU_CD", "KITAKU_CD")
        map.Add("SOKO_CD", "SOKO_CD")
        map.Add("ORDER_TYPE", "ORDER_TYPE")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("FILLER_1", "FILLER_1")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("MEI_NO", "MEI_NO")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("SEIZO_DATE", "SEIZO_DATE")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SOKO_NO", "SOKO_NO")
        map.Add("LOCA", "LOCA")
        map.Add("INOUT_PKG_NB", "INOUT_PKG_NB")
        map.Add("BUMON_CD", "BUMON_CD")
        map.Add("JIGYOBU_CD", "JIGYOBU_CD")
        map.Add("TOKUI_CD", "TOKUI_CD")
        map.Add("OKURIJO_NO", "OKURIJO_NO")
        map.Add("FILLER_2", "FILLER_2")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")
        map.Add("SCM_CUST_CD", "SCM_CUST_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_SEND_BP")

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信送信データ（N_SEND_BP）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_SEND_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiSoushinUpdateN_SEND_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiSoushinUpdateNSEND()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateN_SEND_BP", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ（N_OUTKASCM_DTL_BP）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiSoushinUpdateN_OUTKASCM_DTL_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiSoushinUpdateSCMD()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateN_OUTKASCM_DTL_BP", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータM(N_OUTKASCM_M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiSoushinUpdateN_OUTKASCM_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiSoushinUpdateSCMM()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateN_OUTKASCM_M", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiSoushinUpdateN_OUTKASCM_L()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiSoushinUpdateSCML()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateN_OUTKASCM_L", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)BP出荷EDI受信ヘッダデータ(H_OUTKAEDI_DTL_BP)(LMSVer1)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateH_OUTKAEDI_DTL_BPLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL構築
                Call Me.SQLJissekiSoushinUpdateH_OUTKAEDI_DTL_BP()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                '更新処理件数設定用
                Dim updCnt As Integer = 0

                Dim max As Integer = inTbl.Rows.Count - 1
                For i As Integer = 0 To max

                    'INTableの条件rowの格納
                    Me._row = inTbl.Rows(i)

                    'SQLパラメータ初期化/設定
                    Call Me.SetParamJissekiSoushinUpdateHSCMD()

                    'パラメータの反映
                    For Each obj As Object In Me._sqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateH_OUTKAEDI_DTL_BPLMSVer1", cmd)

                    'SQLの発行
                    updCnt = updCnt + MyBase.GetUpdateResult(cmd)

                    cmd.Parameters.Clear()

                Next

                MyBase.SetResultCount(updCnt)

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using


    End Function

    ''' <summary>
    ''' (LMS)BP出荷EDI受信ヘッダデータ(H_OUTKAEDI_DTL_BP)(LMSVer2)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateH_OUTKAEDI_DTL_BPLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiSoushinUpdateH_OUTKAEDI_DTL_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiSoushinUpdateHSCMD()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateH_OUTKAEDI_DTL_BPLMSVer2", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)BP入出庫報告EDI送信データ(H_SENDEDI_BP)(LMSVer1)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateH_SENDEDI_BPLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続先名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL構築
                Call Me.SQLJissekiSoushinUpdateH_SENDEDI_BP()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                '更新処理件数設定用
                Dim updCnt As Integer = 0

                Dim max As Integer = inTbl.Rows.Count - 1
                For i As Integer = 0 To max

                    'INTableの条件rowの格納
                    Me._row = inTbl.Rows(i)

                    'SQLパラメータ初期化/設定
                    Call Me.SetParamJissekiSoushinUpdateHSEND()

                    'パラメータの反映
                    For Each obj As Object In Me._sqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateH_SENDEDI_BPLMSVer1", cmd)

                    'SQLの発行
                    updCnt = updCnt + MyBase.GetUpdateResult(cmd)

                    cmd.Parameters.Clear()

                Next

                MyBase.SetResultCount(updCnt)

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)BP入出庫報告EDI送信データ(H_SENDEDI_BP)(LMSVer2)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateH_SENDEDI_BPLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続先名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL構築
        Call Me.SQLJissekiSoushinUpdateH_SENDEDI_BP()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        '更新処理件数設定用
        Dim updCnt As Integer = 0

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamJissekiSoushinUpdateHSEND()

            'パラメータの反映
            For Each obj As Object In Me._sqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinUpdateH_SENDEDI_BPLMSVer2", cmd)

            'SQLの発行
            updCnt = updCnt + MyBase.GetUpdateResult(cmd)

            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

    ''' <summary>
    ''' SCM側出荷日、納入日を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinCheckSCMDate(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLJissekiSoushinCheckSCMDate()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamJissekiSoushinCheakSCMDate()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinCheckSCMDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")
        map.Add("SCM_OUTKA_DATE", "SCM_OUTKA_DATE")
        map.Add("SCM_ARR_DATE", "SCM_ARR_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SCM_DATE")

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)LMS側出荷日、納入日を取得(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinCheckLMSDateLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLJissekiSoushinCheckLMSDate()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamJissekiSoushinCheakLMSDate()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinCheckLMSDateLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("REC_NO", "REC_NO")
                map.Add("GYO", "GYO")
                map.Add("LMS_OUTKA_DATE", "LMS_OUTKA_DATE")
                map.Add("LMS_ARR_DATE", "LMS_ARR_DATE")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMS_DATE")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)LMS側出荷日、納入日を取得(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinCheckLMSDateLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '倉庫側接続情報得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("N_SEND_BP")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLJissekiSoushinCheckLMSDate()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamJissekiSoushinCheakLMSDate()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "JissekiSoushinCheckLMSDateLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("LMS_OUTKA_DATE", "LMS_OUTKA_DATE")
        map.Add("LMS_ARR_DATE", "LMS_ARR_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMS_DATE")

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(N_SEND_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinUpdateNSEND()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        '設定時刻
        Dim time As String = String.Empty
        time = Me.GetSystemTime().Substring(0, 2) & ":" & Me.GetSystemTime().Substring(2, 2) & ":" & Me.GetSystemTime().Substring(4, 2)

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.GetUserName() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", time, DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(N_OUTKASCM_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinUpdateSCMD()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        '設定時刻
        Dim time As String = String.Empty
        time = Me.GetSystemTime().Substring(0, 2) & ":" & Me.GetSystemTime().Substring(2, 2) & ":" & Me.GetSystemTime().Substring(4, 2)

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.GetUserName() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", time, DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(N_OUTKASCM_M))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinUpdateSCMM()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.GetUserID() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.GetSystemTime().Substring(0, 8), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(N_OUTKASCM_L))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinUpdateSCML()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(営業所接続情報取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinGetBR_CD_LIST()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(営業所コード取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinGetBR_CD()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CUST_CD", .Item("SCM_CUST_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(H_OUTKAEDI_DTL_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinUpdateHSCMD()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        '設定時刻
        Dim time As String = String.Empty
        time = Me.GetSystemTime().Substring(0, 2) & ":" & Me.GetSystemTime().Substring(2, 2) & ":" & Me.GetSystemTime().Substring(4, 2)

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.GetUserName() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", time, DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.GetUserName() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", time, DBDataType.CHAR))

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新用(H_SENDEDI_BP))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinUpdateHSEND()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        '設定時刻
        Dim time As String = String.Empty
        time = Me.GetSystemTime().Substring(0, 2) & ":" & Me.GetSystemTime().Substring(2, 2) & ":" & Me.GetSystemTime().Substring(4, 2)

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.GetUserName() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", time, DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.GetUserName() & "".ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", time, DBDataType.CHAR))

        End With



    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(出荷日、納入日チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCheakJissekiSoushin()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Call Me.SetParamCommonSystemUpd()

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(SCM側日付取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinCheakSCMDate()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(LMS側日付取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamJissekiSoushinCheakLMSDate()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "欠品状態更新"

    ''' <summary>
    ''' 荷主商品コード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdKeppinJoutaiSelectN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN010IN")

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLUpdKeppinJoutaiN_OUTKASCM_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdKeppinJoutai()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN010DAC", "UpdKeppinJoutaiSelectN_OUTKASCM_M", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "N_OUTKASCM_M")

        Return ds

    End Function

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(営業所コード取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdKeppinJoutai()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#End Region

#Region "システム共通"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID() & "", DBDataType.CHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate() & "", DBDataType.CHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime() & "", DBDataType.CHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID() & "", DBDataType.CHAR))
        Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID() & "", DBDataType.NVARCHAR))

    End Sub

#End Region

#End Region

#Region "スキーマ設定"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSchemaNm()

        Me._MstSchemaNm = String.Concat(MST_SCHEMA, "..")

        Me._LMNTrnSchemaNm = String.Concat(TRN_SCHEMA, "..")

    End Sub

#End Region

#Region "接続名取得"

    Private Sub GetLMSConnectName(ByVal ds As DataSet)

        'DataSetの接続DB名称情報を取得
        Dim dbTbl As DataTable = ds.Tables("BR_CD_LIST")

        '営業所コード取得
        Dim brCd As String = dbTbl.Rows(0).Item("BR_CD").ToString()

        Me._MstSchemaNm = String.Concat(MST_SCHEMA, "..")
        Me._LMNTrnSchemaNm = String.Concat(TRN_SCHEMA, "..")

        Me._TrnEDINm = String.Concat(Me.GetSchemaEDI(brCd), "..")
        Me._MstEDINm = String.Concat(Me.GetSchemaEDIMst(brCd), "..")

    End Sub

#End Region

#Region "LMS DB OPen/Close"

    ''' <summary>
    ''' LMSVer1のOPEN
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenConnectionLMS1(ByVal brCd As String)

        Me._LMS1.ConnectionString = Me.GetConnectionLMS1(brCd)
        Me._LMS1.Open()


    End Sub

    ''' <summary>
    '''  LMSVer1のCLOSE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseConnectionLMS1()

        Me._LMS1.Close()
        Me._LMS1.Dispose()

    End Sub

#End Region

#Region "LMNControl"

#Region "Feild"

    ''' <summary>
    ''' 区分マスタ保持用
    ''' </summary>
    ''' <remarks></remarks>
    Private _kbnDs As DataSet

#End Region

#Region "Const"

    Private Const COL_BR_CD As String = "COL_BR_CD"

    Private Const COL_IKO_FLG As String = "COL_IKO_FLG"

    Private Const COL_LMS_SV_NM As String = "COL_LMS_SV_NM"

    Private Const COL_LMS_SCHEMA_NM As String = "COL_LMS_SCHEMA_NM"

    Private Const COL_LMS2_SV_NM As String = "COL_LMS2_SV_NM"

    Private Const COL_LMS2_SCHEMA_NM As String = "COL_LMS2_SCHEMA_NM"

#End Region

    ''' <summary>
    ''' 区分マスタ設定初期処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LMNControl()

        If _kbnDs Is Nothing = True Then

            Me.CreateKbnDataSet()

            Me.SetConnectDataSet(_kbnDs)

        End If

    End Sub

    ''' <summary>
    ''' 区分マスタ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateKbnDataSet()

        '区分マスタ取得
        _kbnDs = New DataSet
        Dim dt As DataTable = New DataTable
        _kbnDs.Tables.Add(dt)
        _kbnDs.Tables(0).TableName = "Z_KBN"

        For i As Integer = 0 To 17
            _kbnDs.Tables("Z_KBN").Columns.Add(SetCol(i))
        Next

    End Sub

    ''' <summary>
    ''' 区分マスタ設定
    ''' </summary>
    ''' <param name="colno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCol(ByVal colno As Integer) As DataColumn
        Dim col As DataColumn = New DataColumn
        Dim colname As String = String.Empty
        col = New DataColumn
        Select Case colno
            Case 0
                colname = "KBN_GROUP_CD"
            Case 1
                colname = "KBN_CD"
            Case 2
                colname = "KBN_KEYWORD"
            Case 3 'KBN_NM1
                colname = "KBN_NM1"
            Case 4 'KBN_NM2
                colname = "KBN_NM2"
            Case 5 'KBN_NM3
                colname = "KBN_NM3"
            Case 6 'KBN_NM4
                colname = "KBN_NM4"
            Case 7 'KBN_NM5
                colname = "KBN_NM5"
            Case 8 'KBN_NM6
                colname = "KBN_NM6"
            Case 9 'KBN_NM7
                colname = "KBN_NM7"
            Case 10 'KBN_NM8
                colname = "KBN_NM8"
            Case 11 'KBN_NM9
                colname = "KBN_NM9"
            Case 12 'KBN_NM10
                colname = "KBN_NM10"
            Case 13
                colname = "VALUE1"
            Case 14
                colname = "VALUE2"
            Case 15
                colname = "VALUE3"
            Case 16
                colname = "SORT"
            Case 17
                colname = "REM"
        End Select

        col.ColumnName = colname
        col.Caption = colname

        Return col
    End Function


    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Friend Function GetSchemaEDI(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = dataRows(0).Item("KBN_NM6").ToString

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Friend Function GetSchemaEDIMst(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = Me._MstSchemaNm.Replace("..", "")

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' LMSVer1の接続文字列取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetConnectionLMS1(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")

        Dim DBName As String = String.Empty
        Dim loginSchemaNM As String = String.Empty
        Dim userId As String = "sa"
        Dim pass As String = "as"

        DBName = dataRows(0).Item("KBN_NM7").ToString
        loginSchemaNM = dataRows(0).Item("KBN_NM8").ToString

        Return String.Concat("Data Source=", DBName, ";Initial Catalog=", loginSchemaNM, ";Persist Security Info=True;User ID=", userId, ";Password=", pass)

    End Function


    ''' <summary>
    ''' 区分マスタの接続情報を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetConnectDataSet(ByVal ds As DataSet)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetConnection()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        MyBase.Logger.WriteSQLLog("LMNControlDAC", "SQLGetConnection", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_KEYWORD", "KBN_KEYWORD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")
        map.Add("KBN_NM5", "KBN_NM5")
        map.Add("KBN_NM6", "KBN_NM6")
        map.Add("KBN_NM7", "KBN_NM7")
        map.Add("KBN_NM8", "KBN_NM8")
        map.Add("KBN_NM9", "KBN_NM9")
        map.Add("KBN_NM10", "KBN_NM10")
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "Z_KBN")


    End Sub

    ''' <summary>
    '''区分マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetConnection()
   
        Me._strSql.Append("SELECT       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  KBN_GROUP_CD	AS	KBN_GROUP_CD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_CD		    AS	KBN_CD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_KEYWORD	AS	KBN_KEYWORD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM1		AS	KBN_NM1")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM2		AS	KBN_NM2")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM3		AS	KBN_NM3")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM4		AS	KBN_NM4")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM5		AS	KBN_NM5")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM6		AS	KBN_NM6")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM7		AS	KBN_NM7")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM8		AS	KBN_NM8")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM9		AS	KBN_NM9")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM10		AS	KBN_NM10")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE1		    AS	VALUE1")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE2	       	AS	VALUE2")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE3	    	AS	VALUE3")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SORT	    	AS	SORT")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,REM	    	AS	REM")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._MstSchemaNm)
        Me._strSql.Append("Z_KBN KBN       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" KBN.SYS_DEL_FLG = '0'       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" AND KBN.KBN_GROUP_CD ='L001'       ")


    End Sub


#End Region

End Class
