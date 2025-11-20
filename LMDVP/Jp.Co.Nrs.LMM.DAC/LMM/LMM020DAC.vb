' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM020DAC : 分析票管理マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM020DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(COA.NRS_BR_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    'START YANAI 要望番号376
    '''' <summary>
    '''' COA_Mデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
    '                                        & "	      COA.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
    '                                        & "	     ,NRSBR.NRS_BR_NM                             AS NRS_BR_NM                 " & vbNewLine _
    '                                        & "	     ,COA.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
    '                                        & "	     ,CUST.CUST_NM_L                              AS CUST_NM_L                 " & vbNewLine _
    '                                        & "	     ,COA.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
    '                                        & "	     ,CUST.CUST_NM_M                              AS CUST_NM_M                 " & vbNewLine _
    '                                        & "	     ,GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST             " & vbNewLine _
    '                                        & "	     ,GOODS.GOODS_NM_1                            AS GOODS_NM_1                " & vbNewLine _
    '                                        & "	     ,COA.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
    '                                        & "	     ,COA.LOT_NO                                  AS LOT_NO                    " & vbNewLine _
    '                                        & "      ,ISNULL(ZAI.PORA_ZAI_NB,0)                   AS ZAI_NB                    " & vbNewLine _
    '                                        & "	     ,COA.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
    '                                        & "	     ,DEST.DEST_NM                                AS DEST_NM                   " & vbNewLine _
    '                                        & "	     ,COA.COA_LINK                                AS COA_LINK                  " & vbNewLine _
    '                                        & "	     ,COA.COA_NAME                                AS COA_NAME                  " & vbNewLine _
    '                                        & "	     ,COA.SYS_ENT_DATE                            AS SYS_ENT_DATE              " & vbNewLine _
    '                                        & "	     ,USER1.USER_NM                               AS SYS_ENT_USER_NM           " & vbNewLine _
    '                                        & "	     ,COA.SYS_UPD_DATE                            AS SYS_UPD_DATE              " & vbNewLine _
    '                                        & "	     ,COA.SYS_UPD_TIME                            AS SYS_UPD_TIME              " & vbNewLine _
    '                                        & "	     ,USER2.USER_NM                               AS SYS_UPD_USER_NM           " & vbNewLine _
    '                                        & "	     ,COA.SYS_DEL_FLG                             AS SYS_DEL_FLG               " & vbNewLine _
    '                                        & "	     ,KBN1.KBN_NM1                                AS SYS_DEL_NM                " & vbNewLine
    ''' <summary>
    ''' COA_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
                                            & "	      COA.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                             AS NRS_BR_NM                 " & vbNewLine _
                                            & "	     ,COA.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_L                              AS CUST_NM_L                 " & vbNewLine _
                                            & "	     ,COA.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                            & "	     ,CUST.CUST_NM_M                              AS CUST_NM_M                 " & vbNewLine _
                                            & "	     ,GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST             " & vbNewLine _
                                            & "	     ,GOODS.GOODS_NM_1                            AS GOODS_NM_1                " & vbNewLine _
                                            & "	     ,COA.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
                                            & "	     ,COA.LOT_NO                                  AS LOT_NO                    " & vbNewLine _
                                            & "      ,ISNULL(ZAI.PORA_ZAI_NB,0)                   AS ZAI_NB                    " & vbNewLine _
                                            & "	     ,COA.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
                                            & "	     ,ISNULL(DEST.DEST_NM,KBN2.KBN_NM1)           AS DEST_NM                   " & vbNewLine _
                                            & "--ADD START 2018/11/14 要望番号001939                                           " & vbNewLine _
                                            & "	     ,COA.INKA_DATE                               AS INKA_DATE                 " & vbNewLine _
                                            & "	     ,COA.INKA_DATE_VERS_FLG                      AS INKA_DATE_VERS_FLG        " & vbNewLine _
                                            & "--ADD END   2018/11/14 要望番号001939                                           " & vbNewLine _
                                            & "	     ,COA.COA_LINK                                AS COA_LINK                  " & vbNewLine _
                                            & "	     ,COA.COA_NAME                                AS COA_NAME                  " & vbNewLine _
                                            & "	     ,COA.SYS_ENT_DATE                            AS SYS_ENT_DATE              " & vbNewLine _
                                            & "	     ,USER1.USER_NM                               AS SYS_ENT_USER_NM           " & vbNewLine _
                                            & "	     ,COA.SYS_UPD_DATE                            AS SYS_UPD_DATE              " & vbNewLine _
                                            & "	     ,COA.SYS_UPD_TIME                            AS SYS_UPD_TIME              " & vbNewLine _
                                            & "	     ,USER2.USER_NM                               AS SYS_UPD_USER_NM           " & vbNewLine _
                                            & "	     ,COA.SYS_DEL_FLG                             AS SYS_DEL_FLG               " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                AS SYS_DEL_NM                " & vbNewLine 
    'END YANAI 要望番号376

#End Region

#Region "FROM句"
    'START YANAI 要望番号376
    'Private Const SQL_FROM_DATA As String = "FROM                                                                         " & vbNewLine _
    '                                      & "                      $LM_MST$..M_COA AS COA                                 " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR                   AS NRSBR          " & vbNewLine _
    '                                      & "              ON COA.NRS_BR_CD       = NRSBR.NRS_BR_CD                       " & vbNewLine _
    '                                      & "             AND NRSBR.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN                                                        " & vbNewLine _
    '                                      & "                      ( SELECT                                               " & vbNewLine _
    '                                      & "                            CUST.NRS_BR_CD                                   " & vbNewLine _
    '                                      & "                           ,CUST.CUST_CD_L                                   " & vbNewLine _
    '                                      & "                           ,CUST.CUST_NM_L                                   " & vbNewLine _
    '                                      & "                           ,CUST.CUST_CD_M                                   " & vbNewLine _
    '                                      & "                           ,CUST.CUST_NM_M                                   " & vbNewLine _
    '                                      & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S               " & vbNewLine _
    '                                      & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS              " & vbNewLine _
    '                                      & "                            FROM      $LM_MST$..M_CUST     AS CUST           " & vbNewLine _
    '                                      & "                           WHERE      SYS_DEL_FLG = '0'                      " & vbNewLine _
    '                                      & "                        GROUP BY   CUST.NRS_BR_CD                            " & vbNewLine _
    '                                      & "                                  ,CUST.CUST_CD_L                            " & vbNewLine _
    '                                      & "                                  ,CUST.CUST_NM_L                            " & vbNewLine _
    '                                      & "                                  ,CUST.CUST_CD_M                            " & vbNewLine _
    '                                      & "                                  ,CUST.CUST_NM_M                            " & vbNewLine _
    '                                      & "                                                    )      AS CUST           " & vbNewLine _
    '                                      & "              ON COA.NRS_BR_CD       = CUST.NRS_BR_CD                        " & vbNewLine _
    '                                      & "             AND COA.CUST_CD_L       = CUST.CUST_CD_L                        " & vbNewLine _
    '                                      & "             AND COA.CUST_CD_M       = CUST.CUST_CD_M                        " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_GOODS     AS GOODS                         " & vbNewLine _
    '                                      & "              ON GOODS.NRS_BR_CD     = COA.NRS_BR_CD                         " & vbNewLine _
    '                                      & "             AND GOODS.CUST_CD_L     = COA.CUST_CD_L                         " & vbNewLine _
    '                                      & "             AND GOODS.CUST_CD_M     = COA.CUST_CD_M                         " & vbNewLine _
    '                                      & "             AND GOODS.GOODS_CD_NRS  = COA.GOODS_CD_NRS                      " & vbNewLine _
    '                                      & "             AND GOODS.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN                                                        " & vbNewLine _
    '                                      & "                      ( SELECT                                               " & vbNewLine _
    '                                      & "                            ZAI.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                           ,ZAI.LOT_NO                                       " & vbNewLine _
    '                                      & "                           ,ZAI.GOODS_CD_NRS                                 " & vbNewLine _
    '                                      & "                           ,SUM(ZAI.PORA_ZAI_NB) AS  PORA_ZAI_NB             " & vbNewLine _
    '                                      & "                            FROM      $LM_TRN$..D_ZAI_TRS  AS ZAI            " & vbNewLine _
    '                                      & "                           WHERE      SYS_DEL_FLG = '0'                      " & vbNewLine _
    '                                      & "                        GROUP BY   ZAI.NRS_BR_CD                             " & vbNewLine _
    '                                      & "                                  ,ZAI.LOT_NO                                " & vbNewLine _
    '                                      & "                                  ,ZAI.GOODS_CD_NRS                          " & vbNewLine _
    '                                      & "                                                    )      AS ZAI            " & vbNewLine _
    '                                      & "              ON ZAI.NRS_BR_CD       = COA.NRS_BR_CD                         " & vbNewLine _
    '                                      & "             AND ZAI.LOT_NO          = COA.LOT_NO                            " & vbNewLine _
    '                                      & "             AND ZAI.GOODS_CD_NRS    = COA.GOODS_CD_NRS                      " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..M_DEST                     AS DEST           " & vbNewLine _
    '                                      & "              ON DEST.NRS_BR_CD      = COA.NRS_BR_CD                         " & vbNewLine _
    '                                      & "             AND DEST.CUST_CD_L      = COA.CUST_CD_L                         " & vbNewLine _
    '                                      & "             AND DEST.DEST_CD        = COA.DEST_CD                           " & vbNewLine _
    '                                      & "             AND DEST.SYS_DEL_FLG    = '0'                                   " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..S_USER                     AS USER1          " & vbNewLine _
    '                                      & "              ON COA.SYS_ENT_USER    = USER1.USER_CD                         " & vbNewLine _
    '                                      & "             AND USER1.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..S_USER                     AS USER2          " & vbNewLine _
    '                                      & "              ON COA.SYS_UPD_USER    = USER2.USER_CD                         " & vbNewLine _
    '                                      & "             AND USER2.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
    '                                      & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                      AS KBN1           " & vbNewLine _
    '                                      & "              ON COA.SYS_DEL_FLG  = KBN1.KBN_CD                              " & vbNewLine _
    '                                      & "             AND KBN1.KBN_GROUP_CD   = 'S051'                                " & vbNewLine _
    '                                      & "             AND KBN1.SYS_DEL_FLG    = '0'                                   " & vbNewLine
    Private Const SQL_FROM_DATA As String = "FROM                                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_COA AS COA                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR                   AS NRSBR          " & vbNewLine _
                                          & "              ON COA.NRS_BR_CD       = NRSBR.NRS_BR_CD                       " & vbNewLine _
                                          & "             AND NRSBR.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                        " & vbNewLine _
                                          & "                      ( SELECT                                               " & vbNewLine _
                                          & "                            CUST.NRS_BR_CD                                   " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_L                                   " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_L                                   " & vbNewLine _
                                          & "                           ,CUST.CUST_CD_M                                   " & vbNewLine _
                                          & "                           ,CUST.CUST_NM_M                                   " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_S)  AS  CUST_CD_S               " & vbNewLine _
                                          & "                           ,MIN(CUST.CUST_CD_SS) AS  CUST_CD_SS              " & vbNewLine _
                                          & "                            FROM      $LM_MST$..M_CUST     AS CUST           " & vbNewLine _
                                          & "                           WHERE      SYS_DEL_FLG = '0'                      " & vbNewLine _
                                          & "                        GROUP BY   CUST.NRS_BR_CD                            " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_L                            " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_L                            " & vbNewLine _
                                          & "                                  ,CUST.CUST_CD_M                            " & vbNewLine _
                                          & "                                  ,CUST.CUST_NM_M                            " & vbNewLine _
                                          & "                                                    )      AS CUST           " & vbNewLine _
                                          & "              ON COA.NRS_BR_CD       = CUST.NRS_BR_CD                        " & vbNewLine _
                                          & "             AND COA.CUST_CD_L       = CUST.CUST_CD_L                        " & vbNewLine _
                                          & "             AND COA.CUST_CD_M       = CUST.CUST_CD_M                        " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS     AS GOODS                         " & vbNewLine _
                                          & "              ON GOODS.NRS_BR_CD     = COA.NRS_BR_CD                         " & vbNewLine _
                                          & "             AND GOODS.CUST_CD_L     = COA.CUST_CD_L                         " & vbNewLine _
                                          & "             AND GOODS.CUST_CD_M     = COA.CUST_CD_M                         " & vbNewLine _
                                          & "             AND GOODS.GOODS_CD_NRS  = COA.GOODS_CD_NRS                      " & vbNewLine _
                                          & "             AND GOODS.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN                                                        " & vbNewLine _
                                          & "                      ( SELECT                                               " & vbNewLine _
                                          & "                            ZAI.NRS_BR_CD                                    " & vbNewLine _
                                          & "                           ,ZAI.LOT_NO                                       " & vbNewLine _
                                          & "                           ,ZAI.GOODS_CD_NRS                                 " & vbNewLine _
                                          & "                           ,SUM(ZAI.PORA_ZAI_NB) AS  PORA_ZAI_NB             " & vbNewLine _
                                          & "                            FROM      $LM_TRN$..D_ZAI_TRS  AS ZAI            " & vbNewLine _
                                          & "                           WHERE      SYS_DEL_FLG = '0'                      " & vbNewLine _
                                          & "                        GROUP BY   ZAI.NRS_BR_CD                             " & vbNewLine _
                                          & "                                  ,ZAI.LOT_NO                                " & vbNewLine _
                                          & "                                  ,ZAI.GOODS_CD_NRS                          " & vbNewLine _
                                          & "                                                    )      AS ZAI            " & vbNewLine _
                                          & "              ON ZAI.NRS_BR_CD       = COA.NRS_BR_CD                         " & vbNewLine _
                                          & "             AND ZAI.LOT_NO          = COA.LOT_NO                            " & vbNewLine _
                                          & "             AND ZAI.GOODS_CD_NRS    = COA.GOODS_CD_NRS                      " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_DEST                     AS DEST           " & vbNewLine _
                                          & "              ON DEST.NRS_BR_CD      = COA.NRS_BR_CD                         " & vbNewLine _
                                          & "             AND DEST.CUST_CD_L      = COA.CUST_CD_L                         " & vbNewLine _
                                          & "             AND DEST.DEST_CD        = COA.DEST_CD                           " & vbNewLine _
                                          & "             AND DEST.SYS_DEL_FLG    = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER                     AS USER1          " & vbNewLine _
                                          & "              ON COA.SYS_ENT_USER    = USER1.USER_CD                         " & vbNewLine _
                                          & "             AND USER1.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER                     AS USER2          " & vbNewLine _
                                          & "              ON COA.SYS_UPD_USER    = USER2.USER_CD                         " & vbNewLine _
                                          & "             AND USER2.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                      AS KBN1           " & vbNewLine _
                                          & "              ON COA.SYS_DEL_FLG  = KBN1.KBN_CD                              " & vbNewLine _
                                          & "             AND KBN1.KBN_GROUP_CD   = 'S051'                                " & vbNewLine _
                                          & "             AND KBN1.SYS_DEL_FLG    = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN                      AS KBN2           " & vbNewLine _
                                          & "              ON KBN2.KBN_GROUP_CD   = 'A002'                                " & vbNewLine _
                                          & "             AND KBN2.KBN_CD   = '00'                                        " & vbNewLine _
                                          & "             AND KBN2.KBN_NM2  = COA.DEST_CD                                 " & vbNewLine _
                                          & "             AND KBN2.SYS_DEL_FLG    = '0'                                   " & vbNewLine
    'END YANAI 要望番号376

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                          " & vbNewLine _
                                         & "     COA.COA_LINK                                 " & vbNewLine _
                                         & "    ,COA.SYS_ENT_DATE                             " & vbNewLine _
                                         & "     DESC                                         " & vbNewLine _
                                         & "    ,COA.SYS_ENT_TIME                             " & vbNewLine _
                                         & "     DESC                                         " & vbNewLine _
                                         & "    ,COA.LOT_NO                                   " & vbNewLine _
                                         & "    ,COA.CUST_CD_L                                " & vbNewLine _
                                         & "     ASC                                          " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_COA As String = "SELECT                                      " & vbNewLine _
                                            & "   COUNT(NRS_BR_CD)  AS REC_CNT          " & vbNewLine _
                                            & "FROM $LM_MST$..M_COA                     " & vbNewLine _
                                            & "WHERE NRS_BR_CD        = @NRS_BR_CD      " & vbNewLine _
                                            & "  AND GOODS_CD_NRS     = @GOODS_CD_NRS   " & vbNewLine _
                                            & "  AND LOT_NO           = @LOT_NO         " & vbNewLine _
                                            & "  AND DEST_CD          = @DEST_CD        " & vbNewLine _
                                            & "--ADD START 2018/11/14 要望番号001939    " & vbNewLine _
                                            & "  AND INKA_DATE        = @INKA_DATE      " & vbNewLine _
                                            & "  AND INKA_DATE_VERS_FLG = @INKA_DATE_VERS_FLG " & vbNewLine _
                                            & "--ADD END   2018/11/14 要望番号001939    " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_COA        " & vbNewLine _
                                       & "(                                  " & vbNewLine _
                                       & "       NRS_BR_CD                   " & vbNewLine _
                                       & "      ,GOODS_CD_NRS                " & vbNewLine _
                                       & "      ,LOT_NO                      " & vbNewLine _
                                       & "      ,DEST_CD                     " & vbNewLine _
                                       & "--ADD START 2018/11/14 要望番号001939 " & vbNewLine _
                                       & "      ,INKA_DATE                   " & vbNewLine _
                                       & "      ,INKA_DATE_VERS_FLG          " & vbNewLine _
                                       & "--ADD END   2018/11/14 要望番号001939 " & vbNewLine _
                                       & "      ,CUST_CD_L                   " & vbNewLine _
                                       & "      ,CUST_CD_M                   " & vbNewLine _
                                       & "      ,COA_LINK                    " & vbNewLine _
                                       & "      ,COA_NAME                    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,SYS_ENT_USER                " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,SYS_UPD_USER                " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                 " & vbNewLine _
                                       & "      ) VALUES (                   " & vbNewLine _
                                       & "       @NRS_BR_CD                  " & vbNewLine _
                                       & "      ,@GOODS_CD_NRS               " & vbNewLine _
                                       & "      ,@LOT_NO                     " & vbNewLine _
                                       & "      ,@DEST_CD                    " & vbNewLine _
                                       & "--ADD START 2018/11/14 要望番号001939 " & vbNewLine _
                                       & "      ,@INKA_DATE                  " & vbNewLine _
                                       & "      ,@INKA_DATE_VERS_FLG         " & vbNewLine _
                                       & "--ADD END   2018/11/14 要望番号001939 " & vbNewLine _
                                       & "      ,@CUST_CD_L                  " & vbNewLine _
                                       & "      ,@CUST_CD_M                  " & vbNewLine _
                                       & "      ,@COA_LINK                   " & vbNewLine _
                                       & "      ,@COA_NAME                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE               " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME               " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID               " & vbNewLine _
                                       & "      ,@SYS_ENT_USER               " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE               " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME               " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID               " & vbNewLine _
                                       & "      ,@SYS_UPD_USER               " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                " & vbNewLine _
                                       & ")                                  " & vbNewLine
    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_COA SET                            " & vbNewLine _
                                       & "        CUST_CD_L             = @CUST_CD_L            " & vbNewLine _
                                       & "       ,CUST_CD_M             = @CUST_CD_M            " & vbNewLine _
                                       & "       ,COA_LINK              = @COA_LINK             " & vbNewLine _
                                       & "       ,COA_NAME              = @COA_NAME             " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     GOODS_CD_NRS         = @GOODS_CD_NRS         " & vbNewLine _
                                       & " AND     LOT_NO               = @LOT_NO               " & vbNewLine _
                                       & " AND     DEST_CD              = @DEST_CD              " & vbNewLine _
                                       & "--ADD START 2018/11/14 要望番号001939                 " & vbNewLine _
                                       & " AND     INKA_DATE            = @INKA_DATE            " & vbNewLine _
                                       & " AND     INKA_DATE_VERS_FLG   = @INKA_DATE_VERS_FLG   " & vbNewLine _
                                       & "--ADD END   2018/11/14 要望番号001939                 " & vbNewLine

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_COA SET                         " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     GOODS_CD_NRS         = @GOODS_CD_NRS         " & vbNewLine _
                                       & " AND     LOT_NO               = @LOT_NO               " & vbNewLine _
                                       & " AND     DEST_CD              = @DEST_CD              " & vbNewLine _
                                       & "--ADD START 2018/11/14 要望番号001939                 " & vbNewLine _
                                       & " AND     INKA_DATE            = @INKA_DATE            " & vbNewLine _
                                       & " AND     INKA_DATE_VERS_FLG   = @INKA_DATE_VERS_FLG   " & vbNewLine _
                                       & "--ADD END   2018/11/14 要望番号001939                 " & vbNewLine

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
    ''' 分析票管理マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM020DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM020DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 分析票管理マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM020DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM020DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM020DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("ZAI_NB", "ZAI_NB")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        'ADD START 2018/11/14 要望番号001939
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_DATE_VERS_FLG", "INKA_DATE_VERS_FLG")
        'ADD END   2018/11/14 要望番号001939
        map.Add("COA_LINK", "COA_LINK")
        map.Add("COA_NAME", "COA_NAME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM020OUT")

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
                andstr.Append(" (COA.SYS_DEL_FLG = @SYS_DEL_FLG  OR COA.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (COA.NRS_BR_CD = @NRS_BR_CD OR COA.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" COA.CUST_CD_L LIKE @CUST_CD_L")
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

            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                andstr.Append(vbNewLine)
                'START YANAI 要望番号886
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                'END YANAI 要望番号886
            End If

            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" GOODS.GOODS_NM_1 LIKE @GOODS_NM_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" COA.LOT_NO LIKE @LOT_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" DEST.DEST_NM LIKE @DEST_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If .Item("REC_CHK").ToString().Equals(LMConst.FLG.ON) = True Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" COA.COA_LINK = '' ")
                andstr.Append(vbNewLine)
            End If


            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 分析票管理マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM020DAC.SQL_EXIT_COA)
        Me._StrSql.Append("AND SYS_UPD_DATE = @SYS_UPD_DATE")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND SYS_UPD_TIME = @SYS_UPD_TIME")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "SelectCoaM", cmd)

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
    ''' 分析票管理マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM020DAC.SQL_EXIT_COA, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "CheckExistCoaM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function



    ''' <summary>
    ''' 分析票管理マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM020DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "InsertCoaM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 分析票管理マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM020DAC.SQL_UPDATE _
                                                                                     , LMM020DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "UpdateCoaM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 分析票管理マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM020IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM020DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM020DAC", "DeleteCoaM", cmd)

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
    ''' パラメータ設定モジュール(分析票管理マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            'ADD START 2018/11/14 要望番号001939
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_VERS_FLG", .Item("INKA_DATE_VERS_FLG").ToString(), DBDataType.CHAR))
            'ADD END   2018/11/14 要望番号001939
        End With

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            'ADD START 2018/11/14 要望番号001939
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_VERS_FLG", .Item("INKA_DATE_VERS_FLG").ToString(), DBDataType.CHAR))
            'ADD END   2018/11/14 要望番号001939
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_LINK", .Item("COA_LINK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_NAME", .Item("COA_NAME").ToString(), DBDataType.NVARCHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        'ADD START 2018/11/14 要望番号001939
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me._Row.Item("INKA_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_VERS_FLG", Me._Row.Item("INKA_DATE_VERS_FLG").ToString(), DBDataType.CHAR))
        'ADD END   2018/11/14 要望番号001939
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
