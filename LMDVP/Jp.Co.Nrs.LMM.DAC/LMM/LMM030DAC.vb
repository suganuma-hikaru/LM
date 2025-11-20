' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM030DAC : 作業項目マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(SAGYO.SAGYO_CD)     AS SELECT_CNT        " & vbNewLine


    ''' <summary>
    ''' SAGYO_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                           " & vbNewLine _
                                            & "       SAGYO.NRS_BR_CD                          AS NRS_BR_CD                      " & vbNewLine _
                                            & "      ,NRSBR.NRS_BR_NM                          AS NRS_BR_NM                      " & vbNewLine _
                                            & "      ,SAGYO.SAGYO_CD                           AS SAGYO_CD                       " & vbNewLine _
                                            & "      ,SAGYO.SAGYO_NM                           AS SAGYO_NM                       " & vbNewLine _
                                            & "      ,SAGYO.SAGYO_RYAK                         AS SAGYO_RYAK                     " & vbNewLine _
                                            & "      ,SAGYO.CUST_CD_L                          AS CUST_CD_L                      " & vbNewLine _
                                            & "      ,CUST.CUST_NM_L                           AS CUST_NM_L                      " & vbNewLine _
                                            & "      ,SAGYO.INV_YN                             AS INV_YN                         " & vbNewLine _
                                            & "      ,KBN1.KBN_NM2                             AS INV_YN_NM                      " & vbNewLine _
                                            & "      ,SAGYO.FLWP_YN                            AS FLWP_YN                        " & vbNewLine _
                                            & "      ,KBN2.KBN_NM2                             AS FLWP_YN_NM                     " & vbNewLine _
                                            & "      ,SAGYO.SPL_RPT                            AS SPL_RPT                        " & vbNewLine _
                                            & "      ,SAGYO.INV_TANI                           AS INV_TANI                       " & vbNewLine _
                                            & "      ,SAGYO.KOSU_BAI                           AS KOSU_BAI                       " & vbNewLine _
                                            & "      ,SAGYO.SAGYO_UP                           AS SAGYO_UP                       " & vbNewLine _
                                            & "      ,CUST.ITEM_CURR_CD                        AS ITEM_CURR_CD                   " & vbNewLine _
                                            & "      ,SAGYO.ZEI_KBN                            AS ZEI_KBN                        " & vbNewLine _
                                            & "      ,SAGYO.SAGYO_REMARK                       AS SAGYO_REMARK                   " & vbNewLine _
                                            & "      ,SAGYO.SYS_ENT_DATE                       AS SYS_ENT_DATE                   " & vbNewLine _
                                            & "      ,USER1.USER_NM                            AS SYS_ENT_USER_NM                " & vbNewLine _
                                            & "      ,SAGYO.SYS_UPD_DATE                       AS SYS_UPD_DATE                   " & vbNewLine _
                                            & "      ,SAGYO.SYS_UPD_TIME                       AS SYS_UPD_TIME                   " & vbNewLine _
                                            & "      ,USER2.USER_NM                            AS SYS_UPD_USER_NM                " & vbNewLine _
                                            & "      ,SAGYO.SYS_DEL_FLG                        AS SYS_DEL_FLG                    " & vbNewLine _
                                            & "      ,KBN3.KBN_NM1                             AS SYS_DEL_NM                     " & vbNewLine _
                                            & "      ,SAGYO.WH_SAGYO_YN                        AS WH_SAGYO_YN                    " & vbNewLine _
                                            & "      ,KBN4.KBN_NM2                             AS WH_SAGYO_YN_NM                 " & vbNewLine _
                                            & "      ,SAGYO.WH_SAGYO_NM                        AS WH_SAGYO_NM                    " & vbNewLine _
                                            & "      ,SAGYO.WH_SAGYO_REMARK                    AS WH_SAGYO_REMARK                " & vbNewLine _
                                            & "      ,SAGYO.SAGYO_CD_SUB                       AS SAGYO_CD_SUB                   " & vbNewLine _
                                            & "      ,SAGY1.SAGYO_NM                           AS SAGYO_NM_SUB                   " & vbNewLine _
                                            & "      ,SAGY1.CUST_CD_L                          AS CUST_CD_L_SUB                  " & vbNewLine _
                                            & "      ,CUST1.CUST_NM_L                          AS CUST_NM_L_SUB                  " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                                        " & vbNewLine _
                                          & "                      $LM_MST$..M_SAGYO                      AS SAGYO       " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER                       AS USER1       " & vbNewLine _
                                          & "        ON SAGYO.SYS_ENT_USER  = USER1.USER_CD                              " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG   = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER                       AS USER2       " & vbNewLine _
                                          & "       ON SAGYO.SYS_UPD_USER   = USER2.USER_CD                              " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG   = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR                          " & vbNewLine _
                                          & "        ON SAGYO.NRS_BR_CD     = NRSBR.NRS_BR_CD                            " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG   = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                       " & vbNewLine _
                                          & "                      ( SELECT                                              " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                                  " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                                  " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                                  " & vbNewLine _
                                          & "                           ,CUST.ITEM_CURR_CD                               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_M)  AS CUST_CD_M               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS CUST_CD_S               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS CUST_CD_SS              " & vbNewLine _
                                          & "                        FROM  $LM_MST$..M_CUST   AS CUST                    " & vbNewLine _
                                          & "                        WHERE SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                           " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                           " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                           " & vbNewLine _
                                          & "                                  ,CUST.ITEM_CURR_CD                        " & vbNewLine _
                                          & "                        )                                    AS CUST        " & vbNewLine _
                                          & "        ON SAGYO.NRS_BR_CD     = CUST.NRS_BR_CD                             " & vbNewLine _
                                          & "       AND SAGYO.CUST_CD_L     = CUST.CUST_CD_L                             " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                        AS KBN1        " & vbNewLine _
                                          & "        ON SAGYO.INV_YN        = KBN1.KBN_CD                                " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD   = 'U009'                                     " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG    = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                        AS KBN2        " & vbNewLine _
                                          & "        ON SAGYO.FLWP_YN      = KBN2.KBN_CD                                 " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD   = 'U009'                                     " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG    = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                        AS KBN3        " & vbNewLine _
                                          & "        ON SAGYO.SYS_DEL_FLG   = KBN3.KBN_CD                                " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD   = 'S051'                                     " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG    = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                        AS KBN4        " & vbNewLine _
                                          & "        ON SAGYO.WH_SAGYO_YN   = KBN4.KBN_CD                                " & vbNewLine _
                                          & "       AND KBN4.KBN_GROUP_CD   = 'U009'                                     " & vbNewLine _
                                          & "       AND KBN4.SYS_DEL_FLG    = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_SAGYO                      AS SAGY1       " & vbNewLine _
                                          & "        ON SAGYO.SAGYO_CD_SUB  = SAGY1.SAGYO_CD                             " & vbNewLine _
                                          & "       AND SAGY1.SYS_DEL_FLG   = '0'                                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                       " & vbNewLine _
                                          & "                      ( SELECT                                              " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                                  " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                                  " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                                  " & vbNewLine _
                                          & "                           ,CUST.ITEM_CURR_CD                               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_M)  AS CUST_CD_M               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS CUST_CD_S               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS CUST_CD_SS              " & vbNewLine _
                                          & "                        FROM  $LM_MST$..M_CUST   AS CUST                    " & vbNewLine _
                                          & "                        WHERE SYS_DEL_FLG = '0'                             " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                           " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                           " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                           " & vbNewLine _
                                          & "                                  ,CUST.ITEM_CURR_CD                        " & vbNewLine _
                                          & "                        )                                    AS CUST1       " & vbNewLine _
                                          & "        ON SAGY1.NRS_BR_CD     = CUST1.NRS_BR_CD                            " & vbNewLine _
                                          & "       AND SAGY1.CUST_CD_L     = CUST1.CUST_CD_L                            " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                                 " & vbNewLine _
                                         & "     SAGYO.SAGYO_CD                                      " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "名称取得"

    ''' <summary>
    ''' 協力会社作業
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO_SUB As String = "SELECT                                                                      " & vbNewLine _
                                                 & "       SAGYO.NRS_BR_CD                                                      " & vbNewLine _
                                                 & "      ,SAGYO.SAGYO_CD                                                       " & vbNewLine _
                                                 & "      ,SAGYO.SAGYO_NM                                                       " & vbNewLine _
                                                 & "      ,SAGYO.CUST_CD_L                                                      " & vbNewLine _
                                                 & "      ,CUST.CUST_NM_L                                                       " & vbNewLine _
                                                 & "FROM $LM_MST$..M_SAGYO AS SAGYO                                             " & vbNewLine _
                                                 & "      LEFT OUTER JOIN                                                       " & vbNewLine _
                                                 & "                      ( SELECT                                              " & vbNewLine _
                                                 & "                            CUST.NRS_BR_CD                                  " & vbNewLine _
                                                 & "                           ,CUST.CUST_CD_L                                  " & vbNewLine _
                                                 & "                           ,CUST.CUST_NM_L                                  " & vbNewLine _
                                                 & "                           ,CUST.ITEM_CURR_CD                               " & vbNewLine _
                                                 & "                           ,MIN(CUST.CUST_CD_M)  AS CUST_CD_M               " & vbNewLine _
                                                 & "                           ,MIN(CUST.CUST_CD_S)  AS CUST_CD_S               " & vbNewLine _
                                                 & "                           ,MIN(CUST.CUST_CD_SS) AS CUST_CD_SS              " & vbNewLine _
                                                 & "                        FROM  $LM_MST$..M_CUST   AS CUST                    " & vbNewLine _
                                                 & "                        WHERE SYS_DEL_FLG = '0'                             " & vbNewLine _
                                                 & "                        GROUP BY   CUST.NRS_BR_CD                           " & vbNewLine _
                                                 & "                                  ,CUST.CUST_CD_L                           " & vbNewLine _
                                                 & "                                  ,CUST.CUST_NM_L                           " & vbNewLine _
                                                 & "                                  ,CUST.ITEM_CURR_CD                        " & vbNewLine _
                                                 & "                        )                                    AS CUST        " & vbNewLine _
                                                 & "        ON SAGYO.NRS_BR_CD     = CUST.NRS_BR_CD                             " & vbNewLine _
                                                 & "       AND SAGYO.CUST_CD_L     = CUST.CUST_CD_L                             " & vbNewLine _
                                                 & "WHERE SAGYO_CD = @SAGYO_CD                                                  " & vbNewLine _
                                                 & "  AND SYS_DEL_FLG = '0'                                                     " & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 作業コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_SAGYO As String = "SELECT                            " & vbNewLine _
                                            & "   COUNT(SAGYO_CD)  AS REC_CNT   " & vbNewLine _
                                            & "FROM $LM_MST$..M_SAGYO           " & vbNewLine _
                                            & "WHERE SAGYO_CD    = @SAGYO_CD    " & vbNewLine

    ''' <summary>
    ''' 協力会社作業コード存在チェック用
    ''' </summary>
    ''' <remarks>SYS_DEL_FLGは参照しない</remarks>
    Private Const SQL_EXIT_SAGYO_SUB As String = "SELECT                             " & vbNewLine _
                                               & "   COUNT(SAGYO_CD_SUB)  AS REC_CNT " & vbNewLine _
                                               & "FROM $LM_MST$..M_SAGYO             " & vbNewLine _
                                               & "WHERE NRS_BR_CD    = @NRS_BR_CD    " & vbNewLine _
                                               & "  AND SAGYO_CD_SUB = @SAGYO_CD_SUB " & vbNewLine
#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SAGYO           " & vbNewLine _
                                       & "(                                       " & vbNewLine _
                                       & "       NRS_BR_CD                        " & vbNewLine _
                                       & "      ,SAGYO_CD                         " & vbNewLine _
                                       & "      ,CUST_CD_L                        " & vbNewLine _
                                       & "      ,SAGYO_NM                         " & vbNewLine _
                                       & "      ,SAGYO_RYAK                       " & vbNewLine _
                                       & "      ,SPL_RPT                          " & vbNewLine _
                                       & "      ,INV_YN                           " & vbNewLine _
                                       & "      ,FLWP_YN                          " & vbNewLine _
                                       & "      ,INV_TANI                         " & vbNewLine _
                                       & "      ,KOSU_BAI                         " & vbNewLine _
                                       & "      ,SAGYO_UP                         " & vbNewLine _
                                       & "      ,ZEI_KBN                          " & vbNewLine _
                                       & "      ,SAGYO_REMARK                     " & vbNewLine _
                                       & "      ,WH_SAGYO_YN                      " & vbNewLine _
                                       & "      ,WH_SAGYO_NM                      " & vbNewLine _
                                       & "      ,WH_SAGYO_REMARK                  " & vbNewLine _
                                       & "      ,SAGYO_CD_SUB                     " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                     " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                     " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                     " & vbNewLine _
                                       & "      ,SYS_ENT_USER                     " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                     " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                     " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                     " & vbNewLine _
                                       & "      ,SYS_UPD_USER                     " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                      " & vbNewLine _
                                       & "      ) VALUES (                        " & vbNewLine _
                                       & "       @NRS_BR_CD                       " & vbNewLine _
                                       & "      ,@SAGYO_CD                        " & vbNewLine _
                                       & "      ,@CUST_CD_L                       " & vbNewLine _
                                       & "      ,@SAGYO_NM                        " & vbNewLine _
                                       & "      ,@SAGYO_RYAK                      " & vbNewLine _
                                       & "      ,@SPL_RPT                         " & vbNewLine _
                                       & "      ,@INV_YN                          " & vbNewLine _
                                       & "      ,@FLWP_YN                         " & vbNewLine _
                                       & "      ,@INV_TANI                        " & vbNewLine _
                                       & "      ,@KOSU_BAI                        " & vbNewLine _
                                       & "      ,@SAGYO_UP                        " & vbNewLine _
                                       & "      ,@ZEI_KBN                         " & vbNewLine _
                                       & "      ,@SAGYO_REMARK                    " & vbNewLine _
                                       & "      ,@WH_SAGYO_YN                     " & vbNewLine _
                                       & "      ,@WH_SAGYO_NM                     " & vbNewLine _
                                       & "      ,@WH_SAGYO_REMARK                 " & vbNewLine _
                                       & "      ,@SAGYO_CD_SUB                    " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                    " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                    " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                    " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                    " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                    " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                    " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                    " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                    " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                     " & vbNewLine _
                                       & ")                                       " & vbNewLine

    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SAGYO SET                          " & vbNewLine _
                                       & "        NRS_BR_CD             = @NRS_BR_CD            " & vbNewLine _
                                       & "       ,CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                       & "       ,SAGYO_NM              = @SAGYO_NM             " & vbNewLine _
                                       & "       ,SAGYO_RYAK            = @SAGYO_RYAK           " & vbNewLine _
                                       & "       ,SPL_RPT               = @SPL_RPT              " & vbNewLine _
                                       & "       ,INV_YN                = @INV_YN               " & vbNewLine _
                                       & "       ,FLWP_YN               = @FLWP_YN              " & vbNewLine _
                                       & "       ,INV_TANI              = @INV_TANI             " & vbNewLine _
                                       & "       ,KOSU_BAI              = @KOSU_BAI             " & vbNewLine _
                                       & "       ,SAGYO_UP              = @SAGYO_UP             " & vbNewLine _
                                       & "       ,ZEI_KBN               = @ZEI_KBN              " & vbNewLine _
                                       & "       ,SAGYO_REMARK          = @SAGYO_REMARK         " & vbNewLine _
                                       & "       ,WH_SAGYO_YN           = @WH_SAGYO_YN          " & vbNewLine _
                                       & "       ,WH_SAGYO_NM           = @WH_SAGYO_NM          " & vbNewLine _
                                       & "       ,WH_SAGYO_REMARK       = @WH_SAGYO_REMARK      " & vbNewLine _
                                       & "       ,SAGYO_CD_SUB          = @SAGYO_CD_SUB         " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         SAGYO_CD             = @SAGYO_CD             " & vbNewLine
    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_SAGYO SET                          " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         SAGYO_CD             = @SAGYO_CD             " & vbNewLine

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

#Region "検索処理"

    ''' <summary>
    ''' 作業項目マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業項目マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM030DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM030DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業項目マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM030DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM030DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM030DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("SAGYO_RYAK", "SAGYO_RYAK")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("INV_YN", "INV_YN")
        map.Add("INV_YN_NM", "INV_YN_NM")
        map.Add("FLWP_YN", "FLWP_YN")
        map.Add("FLWP_YN_NM", "FLWP_YN_NM")
        map.Add("SPL_RPT", "SPL_RPT")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("KOSU_BAI", "KOSU_BAI")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("ZEI_KBN", "ZEI_KBN")
        map.Add("SAGYO_REMARK", "SAGYO_REMARK")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("WH_SAGYO_YN", "WH_SAGYO_YN")
        map.Add("WH_SAGYO_YN_NM", "WH_SAGYO_YN_NM")
        map.Add("WH_SAGYO_NM", "WH_SAGYO_NM")
        map.Add("WH_SAGYO_REMARK", "WH_SAGYO_REMARK")
        map.Add("SAGYO_CD_SUB", "SAGYO_CD_SUB")
        map.Add("SAGYO_NM_SUB", "SAGYO_NM_SUB")
        map.Add("CUST_CD_L_SUB", "CUST_CD_L_SUB")
        map.Add("CUST_NM_L_SUB", "CUST_NM_L_SUB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM030OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SAGYO.SYS_DEL_FLG = @SYS_DEL_FLG  OR SAGYO.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SAGYO.NRS_BR_CD = @NRS_BR_CD OR SAGYO.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SAGYO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.SAGYO_CD LIKE @SAGYO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SAGYO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.SAGYO_NM LIKE @SAGYO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" CUST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SAGYO_RYAK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.SAGYO_RYAK LIKE @SAGYO_RYAK")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_RYAK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("INV_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.INV_YN = @INV_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("FLWP_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.FLWP_YN = @FLWP_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FLWP_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("WH_SAGYO_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.WH_SAGYO_YN = @WH_SAGYO_YN")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_SAGYO_YN", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SAGYO_CD_SUB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGYO.SAGYO_CD_SUB LIKE @SAGYO_CD_SUB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD_SUB", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SAGYO_NM_SUB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SAGY1.SAGYO_NM LIKE @SAGYO_NM_SUB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM_SUB", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 協力会社作業の名称取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoSub(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030_SAGYO")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM030DAC.SQL_SELECT_SAGYO_SUB)
        Call Me.SetParamSagyoSub()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "SelectSagyoSub", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'IN,OUT共用のためクリア
        ds.Tables("LMM030_SAGYO").Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM030_SAGYO")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 作業項目マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業項目マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSagyoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM030DAC.SQL_EXIT_SAGYO)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "SelectSagyoM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 協力会社作業コードの存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExistSagyoSub(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030_SAGYO")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM030DAC.SQL_EXIT_SAGYO_SUB)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString())
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistSagyoSub()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "SelectExistSagyoSub", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("REC_CNT")) >= 1 Then
            MyBase.SetMessage("E205", {"協力会社作業項目", "作業項目"})
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業項目マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSagyoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM030DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "InsertSagyoM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業項目マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSagyoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM030DAC.SQL_UPDATE _
                                                                                     , LMM030DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "UpdateSagyoM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業項目マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSagyoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM030DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "DeleteSagyoM", cmd)

        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
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
    ''' 契約通貨コード初期値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectItemCurrCd(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM030IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append("SELECT ITEM_CURR_CD                        " & vbNewLine)
        Me._StrSql.Append("FROM $LM_MST$..M_CUST                      " & vbNewLine)
        Me._StrSql.Append("WHERE NRS_BR_CD = @NRS_BR_CD               " & vbNewLine)
        Me._StrSql.Append("AND CUST_CD_L = @CUST_CD_L                 " & vbNewLine)
        Me._StrSql.Append("AND CUST_CD_M = '00'                       " & vbNewLine)
        Me._StrSql.Append("AND CUST_CD_S = '00'                       " & vbNewLine)
        Me._StrSql.Append("AND CUST_CD_SS = '00'                      " & vbNewLine)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )


        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM030DAC", "SelectItemCurrCd", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM030OUT")


        Return ds

    End Function


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

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(作業項目マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", Me._Row.Item("SAGYO_CD").ToString(), DBDataType.CHAR))


    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        Call Me.SetParamExistChk()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(協力会社作業の名称取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSagyoSub()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(協力会社作業コードの存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistSagyoSub()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD_SUB", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

        '画面で取得している更新日時項目
        Call Me.SetSysDateTime()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(削除・復活用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDelete()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新項目
        Call Me.SetParamCommonSystemDel()

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_RYAK", .Item("SAGYO_RYAK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPL_RPT", .Item("SPL_RPT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_YN", .Item("INV_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FLWP_YN", .Item("FLWP_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_BAI", .Item("KOSU_BAI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZEI_KBN", .Item("ZEI_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REMARK", .Item("SAGYO_REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_SAGYO_YN", .Item("WH_SAGYO_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_SAGYO_NM", .Item("WH_SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_SAGYO_REMARK", .Item("WH_SAGYO_REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD_SUB", .Item("SAGYO_CD_SUB").ToString(), DBDataType.NVARCHAR))

        End With

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", Me._Row.Item("SAGYO_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime()

        '画面パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
