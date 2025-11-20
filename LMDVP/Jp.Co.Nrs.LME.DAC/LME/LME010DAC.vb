' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME010    : 作業料明細書作成
'  作  成  者       :  nishikawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME010DAC
''' </summary>
''' <remarks></remarks>
Public Class LME010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LME010IN"
    Public Const TABLE_NM_INOUT As String = "LME010INOUT"
    Public Const TABLE_NM_SAGYO As String = "LME010_SAGYO"
    Public Const TABLE_NM_UPDATE_KEY As String = "LME010OUT_UPDATE_KEY"
    Public Const TABLE_NM_UPDATE_VALUE As String = "LME010OUT_UPDATE_VALUE"
    Public Const TABLE_NM_PRINT As String = "LME010_PRINT_CHECK"
    '要望番号:1045 terakawa 2013.03.28 Start
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End


#Region "検索カウント"

    ''' <summary>
    ''' 検索カウント
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_DATA As String = " SELECT                                               " & vbNewLine _
                                           & " COUNT(E_SAGYO.SAGYO_REC_NO)            AS SELECT_CNT " & vbNewLine

#End Region '検索カウント

#Region "検索SELECT"

    ''' <summary>
    ''' 検索SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                          " & vbNewLine _
                                            & " ROW_NUMBER() OVER (ORDER BY SAGYO_SIJI_NO,INOUTKA_NO_LM) ROW_NO " & vbNewLine _
                                            & ",E_SAGYO.GOODS_NM_NRS        AS GOODS_NM_NRS                     " & vbNewLine _
                                            & ",E_SAGYO.LOT_NO              AS LOT_NO                           " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_NM            AS SAGYO_NM                         " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_NB            AS SAGYO_NB                         " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_UP            AS SAGYO_UP                         " & vbNewLine _
                                            & ",Z_KBN1.KBN_NM1              AS INV_TANI_NM                      " & vbNewLine _
                                            & "--(2012.12.18)要望番号1695 SAGYO_GKはDBの値を出力 -- START --       " & vbNewLine _
                                            & "--,CASE WHEN E_SAGYO.SAGYO_GK <> '0' THEN ROUND(E_SAGYO.SAGYO_GK,0) " & vbNewLine _
                                            & "--      WHEN M_SAGYO.KOSU_BAI = '01' THEN ROUND(E_SAGYO.SAGYO_UP,0) " & vbNewLine _
                                            & "--      ELSE ROUND(E_SAGYO.SAGYO_UP * E_SAGYO.SAGYO_NB,0)           " & vbNewLine _
                                            & "-- END                       AS SAGYO_GK                            " & vbNewLine _
                                            & ",ROUND(E_SAGYO.SAGYO_GK,0)   AS SAGYO_GK                            " & vbNewLine _
                                            & "--(2012.12.18)要望番号1695 SAGYO_GKはDBの値を出力 --  END  --       " & vbNewLine _
                                            & ",E_SAGYO.SEIQTO_CD           AS SEIQTO_CD                        " & vbNewLine _
                                            & ",M_SEIQTO.SEIQTO_NM          AS SEIQTO_NM                        " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_COMP_DATE     AS SAGYO_COMP_DATE                  " & vbNewLine _
                                            & ",E_SAGYO.DEST_NM             AS DEST_NM                          " & vbNewLine _
                                            & ",E_SAGYO.REMARK_SKYU         AS REMARK_SKYU                      " & vbNewLine _
                                            & ",Z_KBN2.KBN_NM1              AS TAX_KB_NM                        " & vbNewLine _
                                            & ",M_CUST.CUST_NM_L            AS CUST_NM_L                        " & vbNewLine _
                                            & ",CASE WHEN M_CUST.ITEM_CURR_CD = '' THEN 'JPY' ELSE M_CUST.ITEM_CURR_CD END AS ITEM_CURR_CD  " & vbNewLine _
                                            & ",M_CURR.ROUND_POS            AS ROUND_POS                        " & vbNewLine _
                                            & ",E_SAGYO.INOUTKA_NO_LM       AS INOUTKA_NO_LM                    " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_SIJI_NO       AS SAGYO_SIJI_NO                    " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_REC_NO        AS SAGYO_REC_NO                     " & vbNewLine _
                                            & ",S_USER.USER_NM              AS SAGYO_COMP_NM                    " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_CD            AS SAGYO_CD                         " & vbNewLine _
                                            & ",E_SAGYO.CUST_CD_L           AS CUST_CD_L                        " & vbNewLine _
                                            & ",E_SAGYO.CUST_CD_M           AS CUST_CD_M                        " & vbNewLine _
                                            & ",Z_KBN3.KBN_NM1              AS IOZS_NM                          " & vbNewLine _
                                            & ",E_SAGYO.SYS_UPD_DATE        AS SYS_UPD_DATE                     " & vbNewLine _
                                            & ",E_SAGYO.SYS_UPD_TIME        AS SYS_UPD_TIME                     " & vbNewLine _
                                            & ",UPD_USER.USER_NM            AS SYS_UPD_USER                     " & vbNewLine _
                                            & ",E_SAGYO.NRS_BR_CD           AS NRS_BR_CD                        " & vbNewLine _
                                            & ",E_SAGYO.WH_CD               AS WH_CD                            " & vbNewLine _
                                            & ",E_SAGYO.GOODS_CD_NRS        AS GOODS_CD_NRS                     " & vbNewLine _
                                            & ",E_SAGYO.INV_TANI            AS INV_TANI                         " & vbNewLine _
                                            & ",E_SAGYO.DEST_CD             AS DEST_CD                          " & vbNewLine _
                                            & ",E_SAGYO.WH_CD               AS WH_CD                            " & vbNewLine _
                                            & ",E_SAGYO.TAX_KB              AS TAX_KB                           " & vbNewLine _
                                            & ",E_SAGYO.IOZS_KB             AS IOZS_KB                          " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_COMP          AS SAGYO_COMP                       " & vbNewLine _
                                            & ",E_SAGYO.SKYU_CHK            AS SKYU_CHK                         " & vbNewLine _
                                            & ",E_SAGYO.REMARK_ZAI          AS REMARK_ZAI                       " & vbNewLine _
                                            & ",E_SAGYO.SAGYO_COMP_CD       AS SAGYO_COMP_CD                    " & vbNewLine _
                                            & ",E_SAGYO.DEST_SAGYO_FLG      AS DEST_SAGYO_FLG                   " & vbNewLine _
                                            & ",E_SAGYO.SYS_DEL_FLG         AS SYS_DEL_FLG                      " & vbNewLine _
                                            & ",'0'                         AS COPY_FLG                         " & vbNewLine _
                                            & ",'0'                         AS SAVE_FLG                         " & vbNewLine

#End Region '検索SELECT

#Region "検索FROM句"

    ''' <summary>
    ''' 検索FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = " FROM       $LM_TRN$..E_SAGYO   E_SAGYO                          " & vbNewLine _
                                            & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN1                           " & vbNewLine _
                                            & " ON   Z_KBN1.KBN_GROUP_CD      = 'S027'                          " & vbNewLine _
                                            & " AND  E_SAGYO.INV_TANI        = Z_KBN1.KBN_CD                    " & vbNewLine _
                                            & " LEFT JOIN  $LM_MST$..M_SAGYO M_SAGYO                            " & vbNewLine _
                                            & " ON  E_SAGYO.NRS_BR_CD       = M_SAGYO.NRS_BR_CD                 " & vbNewLine _
                                            & " AND E_SAGYO.SAGYO_CD        = M_SAGYO.SAGYO_CD                  " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_SEIQTO M_SEIQTO                           " & vbNewLine _
                                            & " ON  E_SAGYO.NRS_BR_CD       = M_SEIQTO.NRS_BR_CD                " & vbNewLine _
                                            & " AND E_SAGYO.SEIQTO_CD       = M_SEIQTO.SEIQTO_CD                " & vbNewLine _
                                            & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN2                           " & vbNewLine _
                                            & " ON   Z_KBN2.KBN_GROUP_CD      = 'Z001'                          " & vbNewLine _
                                            & " AND  E_SAGYO.TAX_KB         = Z_KBN2.KBN_CD                     " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST M_CUST                               " & vbNewLine _
                                            & " ON  E_SAGYO.NRS_BR_CD       = M_CUST.NRS_BR_CD                  " & vbNewLine _
                                            & " AND E_SAGYO.CUST_CD_L       = M_CUST.CUST_CD_L                  " & vbNewLine _
                                            & " AND E_SAGYO.CUST_CD_M       = M_CUST.CUST_CD_M                  " & vbNewLine _
                                            & " AND  M_CUST.CUST_CD_S       = '00'                              " & vbNewLine _
                                            & " AND  M_CUST.CUST_CD_SS      = '00'                              " & vbNewLine _
                                            & " LEFT JOIN COM_DB..M_CURR          M_CURR                        " & vbNewLine _
                                            & "   ON M_CURR.BASE_CURR_CD          = (CASE WHEN M_CUST.ITEM_CURR_CD = '' THEN 'JPY' ELSE M_CUST.ITEM_CURR_CD END) " & vbNewLine _
                                            & "  AND M_CURR.CURR_CD               = (SELECT CASE WHEN SEIQ_CURR_CD = '' THEN 'JPY' ELSE SEIQ_CURR_CD END FROM $LM_MST$..M_SEIQTO WHERE NRS_BR_CD = '40' AND SEIQTO_CD = M_CUST.UNCHIN_SEIQTO_CD AND SYS_DEL_FLG = '0') " & vbNewLine _
                                            & "  AND M_CURR.UP_FLG                = '00000'                     " & vbNewLine _
                                            & "  AND M_CURR.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..S_USER S_USER                               " & vbNewLine _
                                            & " ON  E_SAGYO.SAGYO_COMP_CD   = S_USER.USER_CD                    " & vbNewLine _
                                            & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN3                           " & vbNewLine _
                                            & " ON   Z_KBN3.KBN_GROUP_CD      = 'M010'                          " & vbNewLine _
                                            & " AND  E_SAGYO.IOZS_KB        = Z_KBN3.KBN_CD                     " & vbNewLine _
                                            & " LEFT JOIN                                                       " & vbNewLine _
                                            & " $LM_MST$..S_USER               UPD_USER                         " & vbNewLine _
                                            & " ON                                                              " & vbNewLine _
                                            & " E_SAGYO.SYS_UPD_USER = UPD_USER.USER_CD                         " & vbNewLine


#End Region '検索FROM句

#Region "検索ORDERBY句"

    ''' <summary>
    ''' 検索ORDERBY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDERBY As String = " ORDER BY SAGYO_SIJI_NO,INOUTKA_NO_LM "

#End Region '検索ORDERBY句

#Region "作業テーブル更新"
    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_KAKUTEI_SAGYO As String = " UPDATE " & vbNewLine _
                                        & " $LM_TRN$..E_SAGYO " & vbNewLine _
                                        & " SET " & vbNewLine _
                                        & " SKYU_CHK = @SKYU_CHK " & vbNewLine _
                                        & ", SAGYO_COMP_CD = @SAGYO_COMP_CD " & vbNewLine _
                                        & ", SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
                                        & ", SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
                                        & ", SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
                                        & ", SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
                                        & " WHERE " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SAGYO_REC_NO = @SAGYO_REC_NO " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA " & vbNewLine


#End Region '確定、確定解除

#Region "作業テーブル更新(削除）"
    ''' <summary>
    ''' 作業テーブル更新（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_SAGYO As String = " UPDATE " & vbNewLine _
                                        & " $LM_TRN$..E_SAGYO " & vbNewLine _
                                        & " SET " & vbNewLine _
                                        & " SYS_UPD_DATE  = @SYS_UPD_DATE " & vbNewLine _
                                        & ", SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
                                        & ", SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
                                        & ", SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
                                        & ", SYS_DEL_FLG  = @SYS_DEL_FLG " & vbNewLine _
                                        & " WHERE " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SAGYO_REC_NO = @SAGYO_REC_NO " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA " & vbNewLine


#End Region '保存（削除）

#Region "作業テーブル更新(複写）"
    ''' <summary>
    ''' 作業テーブル更新（複写）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INS_COPY_SAGYO As String = " INSERT INTO" & vbNewLine _
                                               & " $LM_TRN$..E_SAGYO " & vbNewLine _
                                               & " VALUES " & vbNewLine _
                                               & " ( " & vbNewLine _
                                               & "  @NRS_BR_CD " & vbNewLine _
                                               & " ,@SAGYO_REC_NO " & vbNewLine _
                                               & " ,@SAGYO_COMP " & vbNewLine _
                                               & " ,@SKYU_CHK " & vbNewLine _
                                               & " ,@SAGYO_SIJI_NO " & vbNewLine _
                                               & " ,@INOUTKA_NO_LM " & vbNewLine _
                                               & " ,@WH_CD " & vbNewLine _
                                               & " ,@IOZS_KB " & vbNewLine _
                                               & " ,@SAGYO_CD " & vbNewLine _
                                               & " ,@SAGYO_NM " & vbNewLine _
                                               & " ,@CUST_CD_L " & vbNewLine _
                                               & " ,@CUST_CD_M " & vbNewLine _
                                               & " ,@DEST_CD " & vbNewLine _
                                               & " ,@DEST_NM " & vbNewLine _
                                               & " ,@GOODS_CD_NRS " & vbNewLine _
                                               & " ,@GOODS_NM_NRS " & vbNewLine _
                                               & " ,@LOT_NO " & vbNewLine _
                                               & " ,@INV_TANI " & vbNewLine _
                                               & " ,@SAGYO_NB " & vbNewLine _
                                               & " ,@SAGYO_UP " & vbNewLine _
                                               & " ,@SAGYO_GK " & vbNewLine _
                                               & " ,@TAX_KB " & vbNewLine _
                                               & " ,@SEIQTO_CD " & vbNewLine _
                                               & " ,@REMARK_ZAI " & vbNewLine _
                                               & " ,@REMARK_SKYU " & vbNewLine _
                                               & " ,@SAGYO_COMP_CD " & vbNewLine _
                                               & " ,@SAGYO_COMP_DATE " & vbNewLine _
                                               & " ,@DEST_SAGYO_FLG " & vbNewLine _
                                               & " ,@SYS_ENT_DATE " & vbNewLine _
                                               & " ,@SYS_ENT_TIME " & vbNewLine _
                                               & " ,@SYS_ENT_PGID " & vbNewLine _
                                               & " ,@SYS_ENT_USER " & vbNewLine _
                                               & " ,@SYS_UPD_DATE " & vbNewLine _
                                               & " ,@SYS_UPD_TIME " & vbNewLine _
                                               & " ,@SYS_UPD_PGID " & vbNewLine _
                                               & " ,@SYS_UPD_USER " & vbNewLine _
                                               & " ,@SYS_DEL_FLG " & vbNewLine _
                                               & " ) " & vbNewLine


#End Region '保存（削除）

#Region "作業テーブル更新（一括変更）"
    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_HENKO As String = " UPDATE " & vbNewLine _
                                        & " $LM_TRN$..E_SAGYO " & vbNewLine _
                                        & " SET " & vbNewLine _
                                        & " $CHANGE_ITEM$" & vbNewLine _
                                        & ", SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
                                        & ", SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
                                        & ", SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
                                        & ", SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
                                        & " WHERE " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SAGYO_REC_NO = @SAGYO_REC_NO " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA " & vbNewLine _
                                        & " AND " & vbNewLine _
                                        & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA " & vbNewLine


#End Region '一括変更

#Region "作業テーブル更新(完了)"
    'START YANAI 20120319　作業画面改造
    'START YANAI 要望番号927
    '''' <summary>
    '''' 作業テーブル更新(完了)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPD_KANRYO_SAGYO As String = " UPDATE                                 " & vbNewLine _
    '                                    & " $LM_TRN$..E_SAGYO                               " & vbNewLine _
    '                                    & " SET                                             " & vbNewLine _
    '                                    & "  SAGYO_COMP = @SAGYO_COMP                       " & vbNewLine _
    '                                    & ", SAGYO_COMP_DATE = @SAGYO_COMP_DATE             " & vbNewLine _
    '                                    & ", SYS_UPD_DATE = @SYS_UPD_DATE                   " & vbNewLine _
    '                                    & ", SYS_UPD_TIME = @SYS_UPD_TIME                   " & vbNewLine _
    '                                    & ", SYS_UPD_PGID = @SYS_UPD_PGID                   " & vbNewLine _
    '                                    & ", SYS_UPD_USER = @SYS_UPD_USER                   " & vbNewLine _
    '                                    & " WHERE                                           " & vbNewLine _
    '                                    & " NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
    '                                    & " AND                                             " & vbNewLine _
    '                                    & " SAGYO_REC_NO = @SAGYO_REC_NO                    " & vbNewLine _
    '                                    & " AND                                             " & vbNewLine _
    '                                    & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA              " & vbNewLine _
    '                                    & " AND                                             " & vbNewLine _
    '                                    & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA              " & vbNewLine
    'START YANAI 要望番号968
    '''' <summary>
    '''' 作業テーブル更新(完了)
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPD_KANRYO_SAGYO As String = " UPDATE                                 " & vbNewLine _
    '                                    & " $LM_TRN$..E_SAGYO                               " & vbNewLine _
    '                                    & " SET                                             " & vbNewLine _
    '                                    & "  SAGYO_COMP = @SAGYO_COMP                       " & vbNewLine _
    '                                    & ", SYS_UPD_DATE = @SYS_UPD_DATE                   " & vbNewLine _
    '                                    & ", SYS_UPD_TIME = @SYS_UPD_TIME                   " & vbNewLine _
    '                                    & ", SYS_UPD_PGID = @SYS_UPD_PGID                   " & vbNewLine _
    '                                    & ", SYS_UPD_USER = @SYS_UPD_USER                   " & vbNewLine _
    '                                    & " WHERE                                           " & vbNewLine _
    '                                    & " NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
    '                                    & " AND                                             " & vbNewLine _
    '                                    & " SAGYO_REC_NO = @SAGYO_REC_NO                    " & vbNewLine _
    '                                    & " AND                                             " & vbNewLine _
    '                                    & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA              " & vbNewLine _
    '                                    & " AND                                             " & vbNewLine _
    '                                    & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA              " & vbNewLine
    ''' <summary>
    ''' 作業テーブル更新(完了)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_KANRYO_SAGYO As String = " UPDATE                                 " & vbNewLine _
                                        & " $LM_TRN$..E_SAGYO                               " & vbNewLine _
                                        & " SET                                             " & vbNewLine _
                                        & "  SAGYO_COMP = @SAGYO_COMP                       " & vbNewLine _
                                        & ", SAGYO_COMP_DATE = (CASE WHEN SAGYO_COMP_DATE <> '' THEN SAGYO_COMP_DATE ELSE @SYS_UPD_DATE END) " & vbNewLine _
                                        & ", SYS_UPD_DATE = @SYS_UPD_DATE                   " & vbNewLine _
                                        & ", SYS_UPD_TIME = @SYS_UPD_TIME                   " & vbNewLine _
                                        & ", SYS_UPD_PGID = @SYS_UPD_PGID                   " & vbNewLine _
                                        & ", SYS_UPD_USER = @SYS_UPD_USER                   " & vbNewLine _
                                        & " WHERE                                           " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                        & " AND                                             " & vbNewLine _
                                        & " SAGYO_REC_NO = @SAGYO_REC_NO                    " & vbNewLine _
                                        & " AND                                             " & vbNewLine _
                                        & " SYS_UPD_DATE = @SYS_UPD_DATE_HAITA              " & vbNewLine _
                                        & " AND                                             " & vbNewLine _
                                        & " SYS_UPD_TIME = @SYS_UPD_TIME_HAITA              " & vbNewLine
    'END YANAI 要望番号968
    'END YANAI 要望番号927
    'END YANAI 20120319　作業画面改造
#End Region

#Region "印刷チェック"

    Private Const SQL_SELECT_PRINT_CHECK As String = " SELECT                                               " & vbNewLine _
                                        & " M_CUST.CUST_NM_L            AS CUST_NM_L                        " & vbNewLine _
                                        & ",M_CUST.CUST_NM_M            AS CUST_NM_M                        " & vbNewLine _
                                        & ",Z_KBN.KBN_CD                AS CLOSE_DATE                       " & vbNewLine _
                                        & " FROM  $LM_MST$..M_CUST         M_CUST                           " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..M_SEIQTO  M_SEIQTO                         " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " M_CUST.SAGYO_SEIQTO_CD = M_SEIQTO.SEIQTO_CD                     " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.NRS_BR_CD       = M_SEIQTO.NRS_BR_CD                     " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN                            " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " Z_KBN.KBN_GROUP_CD      = 'S008'                                " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_SEIQTO.CLOSE_KB       = Z_KBN.KBN_CD                          " & vbNewLine _
                                        & " WHERE                                                           " & vbNewLine _
                                        & " M_CUST.NRS_BR_CD       = @NRS_BR_CD                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_L       = @CUST_CD_L                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_M       = @CUST_CD_M                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_S       = '00'                                   " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_SS      = '00'                                   " & vbNewLine


    Private Const SQL_SELECT_PRINT_CHECK_SEIQ As String = " SELECT                                          " & vbNewLine _
                                        & " M_CUST.CUST_NM_L            AS CUST_NM_L                        " & vbNewLine _
                                        & ",M_CUST.CUST_NM_M            AS CUST_NM_M                        " & vbNewLine _
                                        & ",Z_KBN.KBN_CD                AS CLOSE_DATE                       " & vbNewLine _
                                        & " FROM  $LM_MST$..M_CUST         M_CUST                           " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..M_SEIQTO  M_SEIQTO                         " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " M_SEIQTO.NRS_BR_CD     = M_CUST.NRS_BR_CD                       " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_SEIQTO.SEIQTO_CD     = @SEIQTO_CD                             " & vbNewLine _
                                        & " LEFT JOIN  $LM_MST$..Z_KBN     Z_KBN                            " & vbNewLine _
                                        & " ON                                                              " & vbNewLine _
                                        & " Z_KBN.KBN_GROUP_CD      = 'S008'                                " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_SEIQTO.CLOSE_KB       = Z_KBN.KBN_CD                          " & vbNewLine _
                                        & " WHERE                                                           " & vbNewLine _
                                        & " M_CUST.NRS_BR_CD       = @NRS_BR_CD                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_L       = @CUST_CD_L                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_M       = @CUST_CD_M                             " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_S       = '00'                                   " & vbNewLine _
                                        & " AND                                                             " & vbNewLine _
                                        & " M_CUST.CUST_CD_SS      = '00'                                   " & vbNewLine



#End Region

#Region "作業テーブル削除"
    'START YANAI 20120319　作業画面改造
#Region "作業テーブルの削除 SQL DELETE句"

    ''' <summary>
    ''' 作業テーブルの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SAGYOREC As String = "DELETE FROM $LM_TRN$..E_SAGYO " & vbNewLine

#End Region

    'END YANAI 20120319　作業画面改造
#End Region

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 条件設定用
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

    ''' <summary>
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region 'Field

#Region "Method"

#Region "作業テーブル更新（確定）"

    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル更新</remarks>
    Private Function UpdateSagyo(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables(LME010DAC.TABLE_NM_SAGYO)

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LME010DAC.SQL_UPD_KAKUTEI_SAGYO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.SetUpdPrm(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "UpdateSagyo", cmd)

        ''SQLの発行
        'If MyBase.GetUpdateResult(cmd) < 1 Then
        '    MyBase.SetMessage("E011")
        '    Return ds
        'End If

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

    'START YANAI 20120319　作業画面改造
#Region "作業テーブル更新（完了）"
    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル更新</remarks>
    Private Function UpdateKanryo(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables(LME010DAC.TABLE_NM_SAGYO)

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LME010DAC.SQL_UPD_KANRYO_SAGYO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.SetUpdKanryoPrm(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "UpdateSagyo", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region
    'END YANAI 20120319　作業画面改造

#Region "検索件数取得処理"

    ''' <summary>
    ''' 検索件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LME010DAC.SQL_COUNT_DATA)
        Me._StrSql.Append(LME010DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSQL()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LME010DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LME010DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSQL()

        Me._StrSql.Append(LME010DAC.SQL_SELECT_ORDERBY)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "SelectListData", cmd)

        'Debug.Print(cmd.CommandText)
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ROW_NO", "ROW_NO")
        map.Add("GOODS_NM_NRS", "GOODS_NM_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SAGYO_NM", "SAGYO_NM")
        map.Add("SAGYO_NB", "SAGYO_NB")
        map.Add("SAGYO_UP", "SAGYO_UP")
        map.Add("INV_TANI_NM", "INV_TANI_NM")
        map.Add("SAGYO_GK", "SAGYO_GK")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("ROUND_POS", "ROUND_POS")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SAGYO_COMP_DATE", "SAGYO_COMP_DATE")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("REMARK_SKYU", "REMARK_SKYU")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("INOUTKA_NO_LM", "INOUTKA_NO_LM")
        map.Add("SAGYO_SIJI_NO", "SAGYO_SIJI_NO")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("SAGYO_COMP_NM", "SAGYO_COMP_NM")
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("IOZS_NM", "IOZS_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("INV_TANI", "INV_TANI")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("SAGYO_COMP", "SAGYO_COMP")
        map.Add("SKYU_CHK", "SKYU_CHK")
        map.Add("REMARK_ZAI", "REMARK_ZAI")
        map.Add("SAGYO_COMP_CD", "SAGYO_COMP_CD")
        map.Add("DEST_SAGYO_FLG", "DEST_SAGYO_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("COPY_FLG", "COPY_FLG")
        map.Add("SAVE_FLG", "SAVE_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LME010DAC.TABLE_NM_INOUT)

        Return ds

    End Function

#End Region

#Region "作業テーブル追加（複写）"

    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル更新</remarks>
    Private Function InsertSagyoCopy(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables(LME010DAC.TABLE_NM_INOUT)

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LME010DAC.SQL_INS_COPY_SAGYO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.SetInsCopyPrm(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "InSertSagyo", cmd)

        'SQLの発行
        If MyBase.GetInsertResult(cmd) < 1 Then
            MyBase.SetMessage("S001")
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "作業テーブル更新（削除）"

    ''' <summary>
    ''' 作業テーブル更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル更新</remarks>
    Private Function UpdateSagyoDel(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables(LME010DAC.TABLE_NM_INOUT)

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(LME010DAC.SQL_UPD_DEL_SAGYO, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ設定
        Call Me.SetUpdDelPrm(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "UpdateSagyoDel", cmd)

        ''SQLの発行
        'If MyBase.GetUpdateResult(cmd) < 1 Then
        '    MyBase.SetMessage("E011")
        '    Return ds
        'End If

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "作業テーブル更新（削除）"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 作業テーブル削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル更新</remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LME010INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LME010DAC.SQL_DELETE_SAGYOREC)      'SQL構築(Delete句)
        Call Me.SetSQLWhereSagyoRec(inTbl)            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "DeleteData", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "作業テーブル更新（一括変更）"

    ''' <summary>
    ''' 作業テーブル更新（一括変更）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>作業テーブル更新（一括変更）</remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0
        Dim strSql As String = String.Empty

        'DataSetのIN情報を取得
        Dim dtKey As DataTable = ds.Tables(LME010DAC.TABLE_NM_UPDATE_KEY)
        Dim dtValue As DataTable = ds.Tables(LME010DAC.TABLE_NM_UPDATE_VALUE)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        strSql = Me.SetSchemaNm(LME010DAC.SQL_UPD_HENKO, dtKey.Rows(0).Item("NRS_BR_CD").ToString())

        strSql = Me.SetColNm(strSql, dtValue)

        'SQL作成
        Me._StrSql.Append(strSql)

        'SQLパラメータ設定
        Call Me.SetUpdHenkoPrm(dtKey, dtValue)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "UpdateHenko", cmd)

        ''SQLの発行
        'If MyBase.GetUpdateResult(cmd) < 1 Then
        '    MyBase.SetMessage("E011")
        '    Return ds
        'End If

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "請求ヘッダ"

    ''' <summary>
    ''' 請求ヘッダ取得(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求ヘッダ取得SQLの構築・発行</remarks>
    Private Function SelectGheaderDataSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_SAGYO)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Tables(LME010DAC.TABLE_NM_SAGYO).Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG000DAC.SQL_SELECT_SAGYO_CHK_DATE, ds.Tables(LME010DAC.TABLE_NM_SAGYO).Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "SelectGheaderData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")
        map.Add("STATE_KB", "STATE_KB")

        Return (MyBase.SetSelectResultToDataSet(map, ds, reader, "G_HED"))

    End Function


    ''' <summary>
    ''' 請求ヘッダ取得(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求ヘッダ取得SQLの構築・発行</remarks>
    Private Function SelectGheaderDataSagyoHozon(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_INOUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Tables(LME010DAC.TABLE_NM_INOUT).Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG000DAC.SQL_SELECT_SAGYO_CHK_DATE, ds.Tables(LME010DAC.TABLE_NM_INOUT).Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "SelectGheaderData", cmd)

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
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_G_HED_CHK)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMG000DAC.SQL_SELECT_NEW_KURO_COUNT, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_G_HED_CHK)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", Me._Row.Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        Dim motoSql As String = String.Empty

        motoSql = LMG000DAC.SQL_SELECT_IN_SKYU_DATE_SAGYO

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(motoSql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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

#Region "印刷チェック"

    Private Function SelectPrintCheck(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LME010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If String.IsNullOrEmpty(Me._Row.Item("SEIQTO_CD").ToString()) = True Then
            Me._StrSql.Append(LME010DAC.SQL_SELECT_PRINT_CHECK)
        Else
            Me._StrSql.Append(LME010DAC.SQL_SELECT_PRINT_CHECK_SEIQ)
        End If
        Call Me.SetPrm()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LME010DAC", "SelectPrintCheck", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CLOSE_DATE", "CLOSE_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LME010DAC.TABLE_NM_PRINT)

        Return ds
    End Function

    Private Sub SetPrm()

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

    End Sub

#End Region

#End Region 'Method

#Region "SQL"


    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="brCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

    ''' <summary>
    ''' カラム名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="dtVal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetColNm(ByVal sql As String, ByVal dtVal As DataTable) As String

        Dim colNm As String = dtVal.Rows(0).Item("EDIT_ITEM_NM").ToString
        Dim temp As String = String.Empty

        If colNm = "SAGYO_NB" Then
            temp = String.Concat(colNm, " = @EDIT_ITEM_VALUE ,", " SAGYO_GK = ROUND(@EDIT_ITEM_VALUE * SAGYO_UP,0) ")
        ElseIf colNm = "SAGYO_UP" Then
            temp = String.Concat(colNm, " = @EDIT_ITEM_VALUE ,", " SAGYO_GK = ROUND(SAGYO_NB * @EDIT_ITEM_VALUE,0) ")
        Else
            temp = String.Concat(colNm, " = @EDIT_ITEM_VALUE ")

        End If

        sql = sql.Replace("$CHANGE_ITEM$", temp)
        Return sql

    End Function


    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._StrSql.Append(" E_SAGYO.SKYU_CHK = @SAGYO_STATE_KB ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND E_SAGYO.NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND E_SAGYO.WH_CD = @WH_CD ")
        Me._StrSql.Append(vbNewLine)
        'START YANAI 20120319　作業画面改造
        'Me._StrSql.Append(" AND E_SAGYO.SAGYO_COMP = '01' ")
        Me._StrSql.Append(" AND E_SAGYO.SAGYO_COMP = @SAGYO_COMP ")
        'END YANAI 20120319　作業画面改造
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND E_SAGYO.SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_STATE_KB", Me._Row("SAGYO_STATE_KB"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))
        'START YANAI 20120319　作業画面改造
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", Me._Row("SAGYO_COMP"), DBDataType.CHAR))
        'END YANAI 20120319　作業画面改造

        With Me._Row

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SEIQTO_CD LIKE @SEIQTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作業コード
            whereStr = .Item("SAGYO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SAGYO_CD = @SAGYO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", whereStr, DBDataType.CHAR))
            End If

            '作業指示№
            whereStr = .Item("SAGYO_SIJI_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SAGYO_SIJI_NO LIKE @SAGYO_SIJI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作業日(FROM)
            whereStr = .Item("SAGYO_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SAGYO_COMP_DATE >= @SAGYO_DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '作業日(TO)
            whereStr = .Item("SAGYO_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SAGYO_COMP_DATE <= @SAGYO_DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM_NRS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.GOODS_NM_NRS LIKE @GOODS_NM_NRS")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'LOT№
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.LOT_NO LIKE @LOT_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作業名
            whereStr = .Item("SAGYO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SAGYO_NM LIKE @SAGYO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '請求単位
            whereStr = .Item("INV_TANI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.INV_TANI = @INV_TANI")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", whereStr, DBDataType.CHAR))
            End If

            '請求先名
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_SEIQTO.SEIQTO_NM LIKE @SEIQTO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.DEST_NM LIKE @DEST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '備考(請求用)
            whereStr = .Item("REMARK_SKYU").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.REMARK_SKYU LIKE @REMARK_SKYU")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.TAX_KB = @TAX_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", whereStr, DBDataType.CHAR))
            End If

            '荷主名(大)
            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_CUST.CUST_NM_L LIKE @CUST_NM_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '管理番号
            whereStr = .Item("INOUTKA_NO_LM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.INOUTKA_NO_LM LIKE @INOUTKA_NO_LM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '作業レコード番号
            whereStr = .Item("SAGYO_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.SAGYO_REC_NO LIKE @SAGYO_REC_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '確認作業者名
            whereStr = .Item("SAGYO_COMP_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND S_USER.USER_NM LIKE @SAGYO_COMP_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '入出在その他区分
            whereStr = .Item("IOZS_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND E_SAGYO.IOZS_KB = @IOZS_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", whereStr, DBDataType.CHAR))
            End If

            '最終更新者
            whereStr = .Item("SYS_UPD_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UPD_USER.USER_NM LIKE @SYS_UPD_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ''ソート順
            'Me._StrSql.Append(" ORDER BY SAGYO_SIJI_NO,INOUTKA_NO_LM ")

        End With

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（作業テーブル更新）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdPrm(ByVal ds As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", ds.Rows(0).Item("SKYU_CHK"), DBDataType.CHAR))

        '確認作業者コード　未確定時は空で更新
        If ds.Rows(0).Item("SKYU_CHK").Equals("01") Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", MyBase.GetUserID(), DBDataType.CHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", String.Empty))
        End If

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", ds.Rows(0).Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", ds.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", ds.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（作業テーブル削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdDelPrm(ByVal ds As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", ds.Rows(0).Item("SYS_DEL_FLG"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", ds.Rows(0).Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", ds.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", ds.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（作業テーブル複写）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInsCopyPrm(ByVal ds As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", ds.Rows(0).Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", ds.Rows(0).Item("SAGYO_COMP"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", ds.Rows(0).Item("SKYU_CHK"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", ds.Rows(0).Item("SAGYO_SIJI_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", ds.Rows(0).Item("INOUTKA_NO_LM"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", ds.Rows(0).Item("WH_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", ds.Rows(0).Item("IOZS_KB"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", ds.Rows(0).Item("SAGYO_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", ds.Rows(0).Item("SAGYO_NM"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", ds.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", ds.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", ds.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", ds.Rows(0).Item("DEST_NM"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", ds.Rows(0).Item("GOODS_CD_NRS"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", ds.Rows(0).Item("GOODS_NM_NRS"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", ds.Rows(0).Item("LOT_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", ds.Rows(0).Item("INV_TANI"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", ds.Rows(0).Item("SAGYO_NB"), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", ds.Rows(0).Item("SAGYO_UP"), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", ds.Rows(0).Item("SAGYO_GK"), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", ds.Rows(0).Item("TAX_KB"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", ds.Rows(0).Item("SEIQTO_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", ds.Rows(0).Item("REMARK_ZAI"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", ds.Rows(0).Item("REMARK_SKYU"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", ds.Rows(0).Item("SAGYO_COMP_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", ds.Rows(0).Item("DEST_SAGYO_FLG"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", ds.Rows(0).Item("SYS_DEL_FLG"), DBDataType.CHAR))


    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（作業テーブル一括変更）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdHenkoPrm(ByVal dtKey As DataTable, ByVal dtVal As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Dim editType As Integer = Convert.ToInt32(dtVal.Rows(0).Item("EDIT_ITEM_TYPE"))

        Select Case editType
            Case 3
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE", dtVal.Rows(0).Item("EDIT_ITEM_VALUE"), DBDataType.CHAR))
            Case 5
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE", dtVal.Rows(0).Item("EDIT_ITEM_VALUE"), DBDataType.NUMERIC))
            Case 12
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE", dtVal.Rows(0).Item("EDIT_ITEM_VALUE"), DBDataType.NVARCHAR))
            Case Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE", dtVal.Rows(0).Item("EDIT_ITEM_VALUE"), DBDataType.CHAR))
        End Select

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtKey.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", dtKey.Rows(0).Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", dtKey.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", dtKey.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（作業テーブル複写）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSagyoRec(ByVal dt As DataTable)

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" E_SAGYO.NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND E_SAGYO.SAGYO_REC_NO = @SAGYO_REC_NO ")

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", dt.Rows(0).Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' SQL文・パラメータ設定モジュール（作業テーブル完了）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdKanryoPrm(ByVal ds As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", ds.Rows(0).Item("SAGYO_COMP"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", ds.Rows(0).Item("SAGYO_REC_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", ds.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", ds.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub
    'END YANAI 20120319　作業画面改造

#End Region 'SQL

End Class

