' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME020  : 作業料明細編集
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LME020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LME020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "データの検索"

#Region "データの検索 SQL SELECT句"

    ''' <summary>
    ''' データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO As String = "SELECT                                                                          " & vbNewLine _
                                             & " SAGYO.NRS_BR_CD                                 AS NRS_BR_CD                   " & vbNewLine _
                                             & ",SAGYO.SAGYO_REC_NO                              AS SAGYO_REC_NO                " & vbNewLine _
                                             & ",SAGYO.SAGYO_COMP                                AS SAGYO_COMP                  " & vbNewLine _
                                             & ",SAGYO.SKYU_CHK                                  AS SKYU_CHK                    " & vbNewLine _
                                             & ",SAGYO.SAGYO_SIJI_NO                             AS SAGYO_SIJI_NO               " & vbNewLine _
                                             & ",SAGYO.INOUTKA_NO_LM                             AS INOUTKA_NO_LM               " & vbNewLine _
                                             & ",SAGYO.WH_CD                                     AS WH_CD                       " & vbNewLine _
                                             & ",SAGYO.IOZS_KB                                   AS IOZS_KB                     " & vbNewLine _
                                             & ",SAGYO.SAGYO_CD                                  AS SAGYO_CD                    " & vbNewLine _
                                             & ",SAGYO.SAGYO_NM                                  AS SAGYO_NM                    " & vbNewLine _
                                             & ",SAGYO.CUST_CD_L                                 AS CUST_CD_L                   " & vbNewLine _
                                             & ",SAGYO.CUST_CD_M                                 AS CUST_CD_M                   " & vbNewLine _
                                             & ",SAGYO.DEST_CD                                   AS DEST_CD                     " & vbNewLine _
                                             & ",SAGYO.DEST_NM                                   AS DEST_NM                     " & vbNewLine _
                                             & ",SAGYO.GOODS_CD_NRS                              AS GOODS_CD_NRS                " & vbNewLine _
                                             & ",SAGYO.GOODS_NM_NRS                              AS GOODS_NM_NRS                " & vbNewLine _
                                             & ",SAGYO.LOT_NO                                    AS LOT_NO                      " & vbNewLine _
                                             & ",SAGYO.INV_TANI                                  AS INV_TANI                    " & vbNewLine _
                                             & ",SAGYO.SAGYO_NB                                  AS SAGYO_NB                    " & vbNewLine _
                                             & ",SAGYO.SAGYO_UP                                  AS SAGYO_UP                    " & vbNewLine _
                                             & ",SAGYO.SAGYO_GK                                  AS SAGYO_GK                    " & vbNewLine _
                                             & ",SAGYO.TAX_KB                                    AS TAX_KB                      " & vbNewLine _
                                             & ",SAGYO.SEIQTO_CD                                 AS SEIQTO_CD                   " & vbNewLine _
                                             & ",SAGYO.REMARK_ZAI                                AS REMARK_ZAI                  " & vbNewLine _
                                             & ",SAGYO.REMARK_SKYU                               AS REMARK_SKYU                 " & vbNewLine _
                                             & ",SAGYO.SAGYO_COMP_CD                             AS SAGYO_COMP_CD               " & vbNewLine _
                                             & ",SAGYO.SAGYO_COMP_DATE                           AS SAGYO_COMP_DATE             " & vbNewLine _
                                             & ",SAGYO.DEST_SAGYO_FLG                            AS DEST_SAGYO_FLG              " & vbNewLine _
                                             & ",SAGYO.SYS_UPD_DATE                              AS SYS_UPD_DATE                " & vbNewLine _
                                             & ",SAGYO.SYS_UPD_TIME                              AS SYS_UPD_TIME                " & vbNewLine _
                                             & ",SUSER.USER_NM                                   AS SAGYO_COMP_CD_NM            " & vbNewLine _
                                             & ",CUST.CUST_NM_L                                  AS CUST_NM                     " & vbNewLine _
                                             & ",CASE WHEN CUST.ITEM_CURR_CD = '' THEN 'JPY' ELSE CUST.ITEM_CURR_CD END AS ITEM_CURR_CD " & vbNewLine _
                                             & ",M_CURR.ROUND_POS                                AS ROUND_POS                   " & vbNewLine _
                                             & ",SEIQTO.SEIQTO_NM                                AS SEIQTO_NM                   " & vbNewLine _
                                             & ",GOODS.GOODS_CD_CUST                             AS GOODS_CD_CUST               " & vbNewLine

#End Region

#Region "データの検索 SQL FROM句"

    ''' <summary>
    ''' データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SAGYO As String = "FROM                                                                       " & vbNewLine _
                                                  & "$LM_TRN$..E_SAGYO SAGYO                                                    " & vbNewLine _
                                                  & "LEFT JOIN                                                                  " & vbNewLine _
                                                  & "$LM_MST$..S_USER SUSER                                                     " & vbNewLine _
                                                  & "ON                                                                         " & vbNewLine _
                                                  & "SUSER.USER_CD = SAGYO.SAGYO_COMP_CD                                        " & vbNewLine _
                                                  & "LEFT JOIN                                                                  " & vbNewLine _
                                                  & "$LM_MST$..M_CUST CUST                                                      " & vbNewLine _
                                                  & "ON                                                                         " & vbNewLine _
                                                  & "CUST.NRS_BR_CD = SAGYO.NRS_BR_CD                                           " & vbNewLine _
                                                  & "AND CUST.CUST_CD_L = SAGYO.CUST_CD_L                                       " & vbNewLine _
                                                  & "AND CUST.CUST_CD_M = SAGYO.CUST_CD_M                                       " & vbNewLine _
                                                  & "AND CUST.CUST_CD_S = '00'                                                  " & vbNewLine _
                                                  & "AND CUST.CUST_CD_SS = '00'                                                 " & vbNewLine _
                                                  & " LEFT JOIN COM_DB..M_CURR          M_CURR                  " & vbNewLine _
                                                  & "   ON M_CURR.BASE_CURR_CD          = (CASE WHEN CUST.ITEM_CURR_CD = '' THEN 'JPY' ELSE CUST.ITEM_CURR_CD END) " & vbNewLine _
                                                  & "  AND M_CURR.CURR_CD               = (SELECT CASE WHEN SEIQ_CURR_CD = '' THEN 'JPY' ELSE SEIQ_CURR_CD END FROM $LM_MST$..M_SEIQTO WHERE NRS_BR_CD = '40' AND SEIQTO_CD = CUST.UNCHIN_SEIQTO_CD AND SYS_DEL_FLG = '0') " & vbNewLine _
                                                  & "  AND M_CURR.UP_FLG                = '00000'               " & vbNewLine _
                                                  & "  AND M_CURR.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                                  & "LEFT JOIN                                                                  " & vbNewLine _
                                                  & "$LM_MST$..M_SEIQTO SEIQTO                                                  " & vbNewLine _
                                                  & "ON                                                                         " & vbNewLine _
                                                  & "SEIQTO.NRS_BR_CD = SAGYO.NRS_BR_CD                                         " & vbNewLine _
                                                  & "AND SEIQTO.SEIQTO_CD = SAGYO.SEIQTO_CD                                     " & vbNewLine _
                                                  & "LEFT JOIN                                                                  " & vbNewLine _
                                                  & "$LM_MST$..M_GOODS GOODS                                                    " & vbNewLine _
                                                  & "ON                                                                         " & vbNewLine _
                                                  & "GOODS.NRS_BR_CD = SAGYO.NRS_BR_CD                                          " & vbNewLine _
                                                  & "AND GOODS.GOODS_CD_NRS = SAGYO.GOODS_CD_NRS                                " & vbNewLine

#End Region

#Region "データの検索 SQL WHERE句"

    ''' <summary>
    ''' データの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SAGYO As String = "WHERE                                                                     " & vbNewLine _
                                                  & "SAGYO.NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                                                  & "AND                                                                        " & vbNewLine _
                                                  & "SAGYO.SAGYO_REC_NO = @SAGYO_REC_NO                                         " & vbNewLine _
                                                  & "AND                                                                        " & vbNewLine _
                                                  & "SAGYO.SYS_DEL_FLG = '0'                                                    " & vbNewLine

#End Region

#End Region

#Region "作業レコード新規作成"

#Region "作業レコードの新規作成 SQL INSERT句"

    ''' <summary>
    ''' 作業レコード新規追加 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO $LM_TRN$..E_SAGYO                          " & vbNewLine _
                                              & " ( 		                                           " & vbNewLine _
                                              & " NRS_BR_CD,                                           " & vbNewLine _
                                              & " SAGYO_REC_NO,                                        " & vbNewLine _
                                              & " SAGYO_COMP,                                          " & vbNewLine _
                                              & " SKYU_CHK,                                            " & vbNewLine _
                                              & " SAGYO_SIJI_NO,                                       " & vbNewLine _
                                              & " INOUTKA_NO_LM,                                       " & vbNewLine _
                                              & " WH_CD,                                               " & vbNewLine _
                                              & " IOZS_KB,                                             " & vbNewLine _
                                              & " SAGYO_CD,                                            " & vbNewLine _
                                              & " SAGYO_NM,                                            " & vbNewLine _
                                              & " CUST_CD_L,                                           " & vbNewLine _
                                              & " CUST_CD_M,                                           " & vbNewLine _
                                              & " DEST_CD,                                             " & vbNewLine _
                                              & " DEST_NM,                                             " & vbNewLine _
                                              & " GOODS_CD_NRS,                                        " & vbNewLine _
                                              & " GOODS_NM_NRS,                                        " & vbNewLine _
                                              & " LOT_NO,                                              " & vbNewLine _
                                              & " INV_TANI,                                            " & vbNewLine _
                                              & " SAGYO_NB,                                            " & vbNewLine _
                                              & " SAGYO_UP,                                            " & vbNewLine _
                                              & " SAGYO_GK,                                            " & vbNewLine _
                                              & " TAX_KB,                                              " & vbNewLine _
                                              & " SEIQTO_CD,                                           " & vbNewLine _
                                              & " REMARK_ZAI,                                          " & vbNewLine _
                                              & " REMARK_SKYU,                                         " & vbNewLine _
                                              & " SAGYO_COMP_CD,                                       " & vbNewLine _
                                              & " SAGYO_COMP_DATE,                                     " & vbNewLine _
                                              & " DEST_SAGYO_FLG,                                      " & vbNewLine _
                                              & " SYS_ENT_DATE,                                        " & vbNewLine _
                                              & " SYS_ENT_TIME,                                        " & vbNewLine _
                                              & " SYS_ENT_PGID,                                        " & vbNewLine _
                                              & " SYS_ENT_USER,                                        " & vbNewLine _
                                              & " SYS_UPD_DATE,                                        " & vbNewLine _
                                              & " SYS_UPD_TIME,                                        " & vbNewLine _
                                              & " SYS_UPD_PGID,                                        " & vbNewLine _
                                              & " SYS_UPD_USER,                                        " & vbNewLine _
                                              & " SYS_DEL_FLG                                          " & vbNewLine _
                                              & " ) VALUES (                                           " & vbNewLine _
                                              & " @NRS_BR_CD,                                          " & vbNewLine _
                                              & " @SAGYO_REC_NO,                                       " & vbNewLine _
                                              & " @SAGYO_COMP,                                         " & vbNewLine _
                                              & " @SKYU_CHK,                                           " & vbNewLine _
                                              & " @SAGYO_SIJI_NO,                                      " & vbNewLine _
                                              & " @INOUTKA_NO_LM,                                      " & vbNewLine _
                                              & " @WH_CD,                                              " & vbNewLine _
                                              & " @IOZS_KB,                                            " & vbNewLine _
                                              & " @SAGYO_CD,                                           " & vbNewLine _
                                              & " @SAGYO_NM,                                           " & vbNewLine _
                                              & " @CUST_CD_L,                                          " & vbNewLine _
                                              & " @CUST_CD_M,                                          " & vbNewLine _
                                              & " @DEST_CD,                                            " & vbNewLine _
                                              & " @DEST_NM,                                            " & vbNewLine _
                                              & " @GOODS_CD_NRS,                                       " & vbNewLine _
                                              & " @GOODS_NM_NRS,                                       " & vbNewLine _
                                              & " @LOT_NO,                                             " & vbNewLine _
                                              & " @INV_TANI,                                           " & vbNewLine _
                                              & " @SAGYO_NB,                                           " & vbNewLine _
                                              & " @SAGYO_UP,                                           " & vbNewLine _
                                              & " @SAGYO_GK,                                           " & vbNewLine _
                                              & " @TAX_KB,                                             " & vbNewLine _
                                              & " @SEIQTO_CD,                                          " & vbNewLine _
                                              & " @REMARK_ZAI,                                         " & vbNewLine _
                                              & " @REMARK_SKYU,                                        " & vbNewLine _
                                              & " @SAGYO_COMP_CD,                                      " & vbNewLine _
                                              & " @SAGYO_COMP_DATE,                                    " & vbNewLine _
                                              & " @DEST_SAGYO_FLG,                                     " & vbNewLine _
                                              & " @SYS_ENT_DATE,                                       " & vbNewLine _
                                              & " @SYS_ENT_TIME,                                       " & vbNewLine _
                                              & " @SYS_ENT_PGID,                                       " & vbNewLine _
                                              & " @SYS_ENT_USER,                                       " & vbNewLine _
                                              & " @SYS_UPD_DATE,                                       " & vbNewLine _
                                              & " @SYS_UPD_TIME,                                       " & vbNewLine _
                                              & " @SYS_UPD_PGID,                                       " & vbNewLine _
                                              & " @SYS_UPD_USER,                                       " & vbNewLine _
                                              & " @SYS_DEL_FLG                                         " & vbNewLine _
                                              & " )                                                    " & vbNewLine

#End Region

#End Region

#Region "作業レコード更新"

#Region "作業レコードの更新 SQL UPDATE句"

    'START YANAI 要望番号875
    '''' <summary>
    '''' 作業レコードの更新 SQL UPDATE句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE_SAGYO As String = "UPDATE $LM_TRN$..E_SAGYO SET                         " & vbNewLine _
    '                                          & "       SAGYO_COMP       = @SAGYO_COMP               " & vbNewLine _
    '                                          & "     , SKYU_CHK       = @SKYU_CHK                   " & vbNewLine _
    '                                          & "     , SAGYO_SIJI_NO       = @SAGYO_SIJI_NO         " & vbNewLine _
    '                                          & "     , INOUTKA_NO_LM       = @INOUTKA_NO_LM         " & vbNewLine _
    '                                          & "     , WH_CD       = @WH_CD                         " & vbNewLine _
    '                                          & "     , IOZS_KB       = @IOZS_KB                     " & vbNewLine _
    '                                          & "     , SAGYO_CD       = @SAGYO_CD                   " & vbNewLine _
    '                                          & "     , SAGYO_NM       = @SAGYO_NM                   " & vbNewLine _
    '                                          & "     , CUST_CD_L       = @CUST_CD_L                 " & vbNewLine _
    '                                          & "     , CUST_CD_M       = @CUST_CD_M                 " & vbNewLine _
    '                                          & "     , DEST_CD       = @DEST_CD                     " & vbNewLine _
    '                                          & "     , DEST_NM       = @DEST_NM                     " & vbNewLine _
    '                                          & "     , GOODS_CD_NRS       = @GOODS_CD_NRS           " & vbNewLine _
    '                                          & "     , GOODS_NM_NRS       = @GOODS_NM_NRS           " & vbNewLine _
    '                                          & "     , LOT_NO       = @LOT_NO                       " & vbNewLine _
    '                                          & "     , INV_TANI       = @INV_TANI                   " & vbNewLine _
    '                                          & "     , SAGYO_NB       = @SAGYO_NB                   " & vbNewLine _
    '                                          & "     , SAGYO_UP       = @SAGYO_UP                   " & vbNewLine _
    '                                          & "     , SAGYO_GK       = @SAGYO_GK                   " & vbNewLine _
    '                                          & "     , TAX_KB       = @TAX_KB                       " & vbNewLine _
    '                                          & "     , SEIQTO_CD       = @SEIQTO_CD                 " & vbNewLine _
    '                                          & "     , REMARK_ZAI       = @REMARK_ZAI               " & vbNewLine _
    '                                          & "     , REMARK_SKYU       = @REMARK_SKYU             " & vbNewLine _
    '                                          & "     , SAGYO_COMP_CD       = @SAGYO_COMP_CD         " & vbNewLine _
    '                                          & "     , SAGYO_COMP_DATE       = @SAGYO_COMP_DATE     " & vbNewLine _
    '                                          & "     , DEST_SAGYO_FLG       = @DEST_SAGYO_FLG       " & vbNewLine _
    '                                          & "     , SYS_UPD_DATE     = @SYS_UPD_DATE             " & vbNewLine _
    '                                          & "     , SYS_UPD_TIME     = @SYS_UPD_TIME             " & vbNewLine _
    '                                          & "     , SYS_UPD_PGID     = @SYS_UPD_PGID             " & vbNewLine _
    '                                          & "     , SYS_UPD_USER     = @SYS_UPD_USER             " & vbNewLine
    ''' <summary>
    ''' 作業レコードの更新 SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SAGYO As String = "UPDATE $LM_TRN$..E_SAGYO SET                         " & vbNewLine _
                                              & "       SAGYO_COMP       = @SAGYO_COMP               " & vbNewLine _
                                              & "     , SKYU_CHK       = @SKYU_CHK                   " & vbNewLine _
                                              & "     , SAGYO_SIJI_NO       = @SAGYO_SIJI_NO         " & vbNewLine _
                                              & "     , INOUTKA_NO_LM       = @INOUTKA_NO_LM         " & vbNewLine _
                                              & "     , WH_CD       = @WH_CD                         " & vbNewLine _
                                              & "     , IOZS_KB       = @IOZS_KB                     " & vbNewLine _
                                              & "     , SAGYO_CD       = @SAGYO_CD                   " & vbNewLine _
                                              & "     , SAGYO_NM       = @SAGYO_NM                   " & vbNewLine _
                                              & "     , CUST_CD_L       = @CUST_CD_L                 " & vbNewLine _
                                              & "     , CUST_CD_M       = @CUST_CD_M                 " & vbNewLine _
                                              & "     , DEST_CD       = @DEST_CD                     " & vbNewLine _
                                              & "     , DEST_NM       = @DEST_NM                     " & vbNewLine _
                                              & "     , GOODS_CD_NRS       = @GOODS_CD_NRS           " & vbNewLine _
                                              & "     , GOODS_NM_NRS       = @GOODS_NM_NRS           " & vbNewLine _
                                              & "     , LOT_NO       = @LOT_NO                       " & vbNewLine _
                                              & "     , INV_TANI       = @INV_TANI                   " & vbNewLine _
                                              & "     , SAGYO_NB       = @SAGYO_NB                   " & vbNewLine _
                                              & "     , SAGYO_UP       = @SAGYO_UP                   " & vbNewLine _
                                              & "     , SAGYO_GK       = @SAGYO_GK                   " & vbNewLine _
                                              & "     , TAX_KB       = @TAX_KB                       " & vbNewLine _
                                              & "     , SEIQTO_CD       = @SEIQTO_CD                 " & vbNewLine _
                                              & "     , REMARK_ZAI       = @REMARK_ZAI               " & vbNewLine _
                                              & "     , REMARK_SKYU       = @REMARK_SKYU             " & vbNewLine _
                                              & "     , SAGYO_COMP_CD       = @SAGYO_COMP_CD         " & vbNewLine _
                                              & "     , SAGYO_COMP_DATE       = @SAGYO_COMP_DATE     " & vbNewLine _
                                              & "     , SYS_UPD_DATE     = @SYS_UPD_DATE             " & vbNewLine _
                                              & "     , SYS_UPD_TIME     = @SYS_UPD_TIME             " & vbNewLine _
                                              & "     , SYS_UPD_PGID     = @SYS_UPD_PGID             " & vbNewLine _
                                              & "     , SYS_UPD_USER     = @SYS_UPD_USER             " & vbNewLine
    'END YANAI 要望番号875

#End Region

#Region "作業レコードの更新 SQL WHERE句"

    ''' <summary>
    ''' 作業レコードの更新 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_WHERE_SAGYO As String = " WHERE                                         " & vbNewLine _
                                                 & "      NRS_BR_CD        = @NRS_BR_CD              " & vbNewLine _
                                                 & "  AND SAGYO_REC_NO     = @SAGYO_REC_NO           " & vbNewLine

#End Region

#End Region

#Region "作業レコード削除"

#Region "作業レコードの削除 SQL DELETE句"

    ''' <summary>
    ''' 作業レコードの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SAGYO As String = "DELETE FROM $LM_TRN$..E_SAGYO " & vbNewLine

#End Region

#Region "作業レコードの削除 SQL WHERE句"

    ''' <summary>
    ''' 作業レコードの削除 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WHERE_SAGYO As String = " WHERE                                         " & vbNewLine _
                                                 & "      NRS_BR_CD        = @NRS_BR_CD              " & vbNewLine _
                                                 & "  AND SAGYO_REC_NO     = @SAGYO_REC_NO           " & vbNewLine

#End Region

#End Region

#End Region

#Region "Field"

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

#Region "SQLメイン処理"

    ''' <summary>
    ''' データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME020IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME020DAC.SQL_SELECT_SAGYO)        'SQL構築(Select句)
        Me._StrSql.Append(LME020DAC.SQL_SELECT_FROM_SAGYO)   'SQL構築(From句)
        Me._StrSql.Append(LME020DAC.SQL_SELECT_WHERE_SAGYO)  'SQL構築(Where句)

        'SQLパラメータ設定
        Call Me.SetSagyoSelect(inTbl.Rows(0), Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("SAGYO_COMP", "SAGYO_COMP")
        map.Add("SKYU_CHK", "SKYU_CHK")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("INOUTKA_NO_LM", "INOUTKA_NO_LM")
        map.Add("WH_CD", "WH_CD")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("SAGYO_GK", "SAGYO_GK")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("ROUND_POS", "ROUND_POS")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("REMARK_ZAI", "REMARK_ZAI")
        map.Add("REMARK_SKYU", "REMARK_SKYU")
        map.Add("SAGYO_COMP_CD", "SAGYO_COMP_CD")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("DEST_SAGYO_FLG", "DEST_SAGYO_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SAGYO_COMP_CD_NM", "SAGYO_COMP_CD_NM")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LME020INOUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME020INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME020DAC.SQL_INSERT_SAGYO)       'SQL構築(INSERT句)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSagyoHozon(inTbl.Rows(0))
        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns(inTbl.Rows(0))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", "InsertSaveAction", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 作業テーブル更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業テーブル更新登録SQLの構築・発行</remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME020INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME020DAC.SQL_UPDATE_SAGYO)       'SQL構築(UPDATE句)
        Me._StrSql.Append(LME020DAC.SQL_UPDATE_WHERE_SAGYO) 'SQL構築(WHERE句)

        'SQLパラメータ（個別項目）設定
        Call Me.SetSagyoHozon(inTbl.Rows(0))
        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(inTbl.Rows(0))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", "UpdateSaveAction", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 作業テーブル削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル削除</remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME020INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME020DAC.SQL_DELETE_SAGYO)       'SQL構築(Delete句)
        Me._StrSql.Append(LME020DAC.SQL_DELETE_WHERE_SAGYO) 'SQL構築(Where句)
        Call Me.SetSagyoDel(inTbl.Rows(0))                       '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", "DeleteSaveAction", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求ヘッダ取得(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求ヘッダ取得SQLの構築・発行</remarks>
    Private Function ChkSeiqDateSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME020INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", inTbl.Rows(0).Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG000DAC.SQL_SELECT_SAGYO_CHK_DATE, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", "SelectGheaderData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("STATE_KB", "STATE_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "G_HED"))

    End Function

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 新黒存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function NewKuroExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("G_HED_CHK")

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", inTbl.Rows(0).Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(inTbl.Rows(0).Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMG000DAC.SQL_SELECT_NEW_KURO_COUNT, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' '請求期間内チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InSkyuDateChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("G_HED_CHK")

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", inTbl.Rows(0).Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(inTbl.Rows(0).Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", inTbl.Rows(0).Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        Dim motoSql As String = String.Empty

        motoSql = LMG000DAC.SQL_SELECT_IN_SKYU_DATE_SAGYO

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(motoSql, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME020DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function
    '要望番号:1045 terakawa 2013.03.28 End


#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 作業レコード更新"

    ''' <summary>
    ''' 作業レコード検索パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoSelect(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 作業レコード更新"

    ''' <summary>
    ''' 作業レコード更新パラメータ設定(新規追加・更新時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoHozon(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK"), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG"), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 作業レコード更新パラメータ設定(削除時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoDel(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "共通"
    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal conditionRow As DataRow)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(conditionRow)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal conditionRow As DataRow)

        With conditionRow

            '更新日時
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME"), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns(ByVal conditionRow As DataRow)

        With conditionRow

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", .Item("SYS_ENT_DATE"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", .Item("SYS_ENT_TIME"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        End With

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
