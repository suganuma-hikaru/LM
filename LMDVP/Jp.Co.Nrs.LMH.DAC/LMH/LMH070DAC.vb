' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH070    : 先行手入力入出荷とEDIの紐付け
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
''' 
Public Class LMH070DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理"

#Region "初期検索"

    ''' <summary>
    ''' 初期検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKAEDI_L As String = " SELECT                                                            " & vbNewLine _
                                        & " H_INKAEDI_L.OUTKA_FROM_ORD_NO                       AS OUTKA_FROM_ORD_NO   " & vbNewLine _
                                        & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
                                        & ",H_INKAEDI_L.INKA_DATE                               AS INOUTKA_DATE  	   " & vbNewLine _
                                        & ",''                                                  AS DEST_CD       	   " & vbNewLine _
                                        & ",''                                                  AS DEST_NM      	   " & vbNewLine _
                                        & ",H_INKAEDI_L.SYS_UPD_DATE                            AS SYS_UPD_DATE	       " & vbNewLine _
                                        & ",H_INKAEDI_L.SYS_UPD_TIME                            AS SYS_UPD_TIME	       " & vbNewLine _
                                        & ",RCV_HED.SYS_UPD_DATE                                AS RCV_UPD_DATE	       " & vbNewLine _
                                        & ",RCV_HED.SYS_UPD_TIME                                AS RCV_UPD_TIME  	   " & vbNewLine _
                                        & " FROM                                                                       " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_L                                                      " & vbNewLine _
                                        & " LEFT JOIN                                                                  " & vbNewLine _
                                        & " $LM_MST$..M_CUST                       M_CUST                              " & vbNewLine _
                                        & " ON                                                                         " & vbNewLine _
                                        & " H_INKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                   " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_INKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                                   " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_INKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                                   " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " M_CUST.CUST_CD_S = '00'                                                    " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " M_CUST.CUST_CD_SS = '00'                                                   " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " M_CUST.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                        & "-- LEFT JOIN                                                                  " & vbNewLine _
                                        & "-- $LM_TRN$..$RCV_HED$                  RCV_HED                               " & vbNewLine _
                                        & "-- ON                                                                         " & vbNewLine _
                                        & "-- H_INKAEDI_L.NRS_BR_CD  = RCV_HED.NRS_BR_CD                                 " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- H_INKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO                                " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- RCV_HED.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                        & "-- WHERE                                                                      " & vbNewLine _
                                        & "-- H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- H_INKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO                                       " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- H_INKAEDI_L.SYS_DEL_FLG = '0'                                              " & vbNewLine

    '2012.02.29 大阪対応 START

    Private Const SQL_FROM_RCVTABLE_IN As String = "LEFT JOIN                              " & vbNewLine _
                                & "$LM_TRN$..$RCV_HED$        RCV_HED                     " & vbNewLine _
                                & "ON                                                     " & vbNewLine _
                                & "H_INKAEDI_L.NRS_BR_CD  = RCV_HED.NRS_BR_CD             " & vbNewLine _
                                & "AND                                                    " & vbNewLine _
                                & "H_INKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO            " & vbNewLine _
                                & " AND                                                   " & vbNewLine _
                                & " RCV_HED.SYS_DEL_FLG = '0'                             " & vbNewLine

    Private Const SQL_FROM_RCVTABLE_NULL_IN As String = "LEFT JOIN                         " & vbNewLine _
                                 & "(                                                     " & vbNewLine _
                                 & "SELECT                                                " & vbNewLine _
                                 & " H_INKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                 " & vbNewLine _
                                 & ",'' AS SYS_UPD_DATE                                   " & vbNewLine _
                                 & ",'' AS SYS_UPD_TIME                                   " & vbNewLine _
                                 & "FROM                                                  " & vbNewLine _
                                 & "$LM_TRN$..H_INKAEDI_L                 H_INKAEDI_L     " & vbNewLine _
                                 & "WHERE                                                 " & vbNewLine _
                                 & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                                 & ")                                     RCV_HED         " & vbNewLine _
                                 & "ON                                                    " & vbNewLine _
                                 & "H_INKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO           " & vbNewLine

    Private Const SQL_FROM_RCVTABLE_INOUT_IN As String = "AND                             " & vbNewLine _
                                    & "RCV_HED.INOUT_KB = '1'                             " & vbNewLine

    Private Const SQL_FROM_WHERE_IN As String = "WHERE                                    " & vbNewLine _
                                 & " H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
                                 & " AND                                                  " & vbNewLine _
                                 & " H_INKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO                 " & vbNewLine _
                                 & " AND                                                  " & vbNewLine _
                                 & " H_INKAEDI_L.SYS_DEL_FLG = '0'                        " & vbNewLine

    '2012.02.29 大阪対応 END

    ''' <summary>
    ''' 初期検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAEDI_L As String = " SELECT                                                           " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_ORD_NO                             AS OUTKA_FROM_ORD_NO  " & vbNewLine _
                                        & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M           AS CUST_NM            " & vbNewLine _
                                        & ",H_OUTKAEDI_L.OUTKA_PLAN_DATE                         AS INOUTKA_DATE       " & vbNewLine _
                                        & ",H_OUTKAEDI_L.DEST_CD                                 AS DEST_CD            " & vbNewLine _
                                        & ",H_OUTKAEDI_L.DEST_NM                                 AS DEST_NM            " & vbNewLine _
                                        & ",H_OUTKAEDI_L.SYS_UPD_DATE                            AS SYS_UPD_DATE       " & vbNewLine _
                                        & ",H_OUTKAEDI_L.SYS_UPD_TIME                            AS SYS_UPD_TIME       " & vbNewLine _
                                        & ",RCV_HED.SYS_UPD_DATE                                 AS RCV_UPD_DATE       " & vbNewLine _
                                        & ",RCV_HED.SYS_UPD_TIME                                 AS RCV_UPD_TIME       " & vbNewLine _
                                        & " FROM                                                                       " & vbNewLine _
                                        & " $LM_TRN$..H_OUTKAEDI_L                                                     " & vbNewLine _
                                        & " LEFT JOIN                                                                  " & vbNewLine _
                                        & " $LM_MST$..M_CUST                       M_CUST                              " & vbNewLine _
                                        & " ON                                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                  " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                                  " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                                  " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " M_CUST.CUST_CD_S = '00'                                                    " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " M_CUST.CUST_CD_SS = '00'                                                   " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " M_CUST.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                        & "-- LEFT JOIN                                                                  " & vbNewLine _
                                        & "-- $LM_TRN$..$RCV_HED$                  RCV_HED                               " & vbNewLine _
                                        & "-- ON                                                                         " & vbNewLine _
                                        & "-- H_OUTKAEDI_L.NRS_BR_CD  = RCV_HED.NRS_BR_CD                                " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO                               " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- RCV_HED.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                        & "-- WHERE                                                                      " & vbNewLine _
                                        & "-- H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- H_OUTKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO                                      " & vbNewLine _
                                        & "-- AND                                                                        " & vbNewLine _
                                        & "-- H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine

    '2012.02.29 大阪対応 START

    Private Const SQL_FROM_RCVTABLE_OUT As String = "LEFT JOIN                                          " & vbNewLine _
                                    & "$LM_TRN$..$RCV_HED$              RCV_HED                     " & vbNewLine _
                                    & "ON                                                           " & vbNewLine _
                                    & "H_OUTKAEDI_L.NRS_BR_CD  = RCV_HED.NRS_BR_CD                  " & vbNewLine _
                                    & "AND                                                          " & vbNewLine _
                                    & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO                 " & vbNewLine _
                                    & "AND                                                          " & vbNewLine _
                                    & "RCV_HED.SYS_DEL_FLG = '0'                                    " & vbNewLine


    Private Const SQL_FROM_RCVTABLE_NULL_OUT As String = "LEFT JOIN                                         " & vbNewLine _
                                        & "(                                                            " & vbNewLine _
                                        & "SELECT                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                       " & vbNewLine _
                                        & ",'' AS SYS_UPD_DATE                                          " & vbNewLine _
                                        & ",'' AS SYS_UPD_TIME                                          " & vbNewLine _
                                        & "FROM                                                         " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                H_OUTKAEDI_L           " & vbNewLine _
                                        & "WHERE                                                        " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                        & ")                                     RCV_HED                " & vbNewLine _
                                        & "ON                                                           " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO                 " & vbNewLine


    Private Const SQL_FROM_RCVTABLE_INOUT_OUT As String = "AND                                              " & vbNewLine _
                                        & "RCV_HED.INOUT_KB = '0'                                       " & vbNewLine

    Private Const SQL_FROM_WHERE_OUT As String = "WHERE                                                 " & vbNewLine _
                                        & " H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                                        & " AND                                                     " & vbNewLine _
                                        & " H_OUTKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO                   " & vbNewLine _
                                        & " AND                                                     " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                          " & vbNewLine

    '2012.02.29 大阪対応 END

    ''' <summary>
    ''' 初期検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKAEDI_M As String = " SELECT                                                            " & vbNewLine _
                                        & " H_INKAEDI_M.NRS_GOODS_CD                            AS NRS_GOODS_CD        " & vbNewLine _
                                        & ",H_INKAEDI_M.CUST_GOODS_CD                           AS CUST_GOODS_CD       " & vbNewLine _
                                        & ",H_INKAEDI_M.GOODS_NM                                AS GOODS_NM            " & vbNewLine _
                                        & ",H_INKAEDI_M.LOT_NO                                  AS LOT_NO              " & vbNewLine _
                                        & ",H_INKAEDI_M.IRIME                                   AS IRIME               " & vbNewLine _
                                        & ",H_INKAEDI_M.IRIME_UT                                AS IRIME_UT            " & vbNewLine _
                                        & ",H_INKAEDI_M.NB                                      AS NB                  " & vbNewLine _
                                        & ",H_INKAEDI_M.EDI_CTL_NO_CHU                          AS EDI_CTL_NO_CHU      " & vbNewLine _
                                        & " FROM                                                                       " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_M                                                      " & vbNewLine _
                                        & " WHERE                                                                      " & vbNewLine _
                                        & " H_INKAEDI_M.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_INKAEDI_M.EDI_CTL_NO = @EDI_CTL_NO                                       " & vbNewLine _
                                        & " AND                                                                        " & vbNewLine _
                                        & " H_INKAEDI_M.SYS_DEL_FLG = '0'                                              " & vbNewLine


    ''' <summary>
    ''' 初期検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAEDI_M As String = " SELECT                                                            " & vbNewLine _
                                         & " H_OUTKAEDI_M.NRS_GOODS_CD                            AS NRS_GOODS_CD       " & vbNewLine _
                                         & ",H_OUTKAEDI_M.CUST_GOODS_CD                           AS CUST_GOODS_CD      " & vbNewLine _
                                         & ",H_OUTKAEDI_M.GOODS_NM                                AS GOODS_NM           " & vbNewLine _
                                         & ",H_OUTKAEDI_M.LOT_NO                                  AS LOT_NO             " & vbNewLine _
                                         & ",H_OUTKAEDI_M.IRIME                                   AS IRIME              " & vbNewLine _
                                         & ",H_OUTKAEDI_M.IRIME_UT                                AS IRIME_UT           " & vbNewLine _
                                         & ",H_OUTKAEDI_M.OUTKA_TTL_NB                            AS NB                 " & vbNewLine _
                                         & ",H_OUTKAEDI_M.EDI_CTL_NO_CHU                          AS EDI_CTL_NO_CHU     " & vbNewLine _
                                         & " FROM                                                                       " & vbNewLine _
                                         & " $LM_TRN$..H_OUTKAEDI_M                                                     " & vbNewLine _
                                         & " WHERE                                                                      " & vbNewLine _
                                         & " H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
                                         & " AND                                                                        " & vbNewLine _
                                         & " H_OUTKAEDI_M.EDI_CTL_NO = @EDI_CTL_NO                                      " & vbNewLine _
                                         & " AND                                                                        " & vbNewLine _
                                         & " H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                             " & vbNewLine




    Private Const SQL_SELECT_INKA As String = " SELECT                                                             " & vbNewLine _
                                    & " B_INKA_L.OUTKA_FROM_ORD_NO_L                        AS OUTKA_FROM_ORD_NO   " & vbNewLine _
                                    & ",B_INKA_L.INKA_DATE                                  AS PLAN_DATE           " & vbNewLine _
                                    & ",B_INKA_M.INKA_NO_M                                  AS INOUTKA_NO_M        " & vbNewLine _
                                    & ",B_INKA_M.GOODS_CD_NRS                               AS GOODS_CD_NRS        " & vbNewLine _
                                    & ",M_GOODS.GOODS_CD_CUST                               AS GOODS_CD_CUST       " & vbNewLine _
                                    & ",M_GOODS.GOODS_NM_1                                  AS GOODS_NM            " & vbNewLine _
                                    & ",B_INKA_S.LOT_NO                                     AS LOT_NO              " & vbNewLine _
                                    & ",M_GOODS.STD_IRIME_NB                                AS IRIME               " & vbNewLine _
                                    & ",M_GOODS.STD_IRIME_UT                                AS IRIME_UT            " & vbNewLine _
                                    & ",B_INKA_S.NB                                         AS NB                  " & vbNewLine _
                                    & ",ISNULL(H_INKAEDI_L.EDI_CTL_NO,'')                   AS EDI_CTL_NO          " & vbNewLine _
                                    & ",B_INKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
                                    & ",B_INKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
                                    & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
                                    & ",''                                                  AS DEST_CD             " & vbNewLine _
                                    & ",''                                                  AS DEST_NM             " & vbNewLine _
                                    & " FROM                                                                       " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_L                                                         " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_M                     B_INKA_M                            " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_L.NRS_BR_CD = B_INKA_M.NRS_BR_CD                                    " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_L.INKA_NO_L = B_INKA_M.INKA_NO_L                                    " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_M.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " (                                                                          " & vbNewLine _
                                    & " SELECT                                                                     " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_M                                   AS INKA_NO_M          " & vbNewLine _
                                    & ",SUM(B_INKA_S.KONSU * M_GOODS.PKG_NB + B_INKA_S.HASU) AS NB                 " & vbNewLine _
                                    & ",B_INKA_S.LOT_NO                                      AS LOT_NO             " & vbNewLine _
                                    & " FROM                                                                       " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_S                     B_INKA_S                            " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_M                     B_INKA_M                            " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_S.NRS_BR_CD = B_INKA_M.NRS_BR_CD                                    " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_L = B_INKA_M.INKA_NO_L                                    " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_M = B_INKA_M.INKA_NO_M                                    " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " $LM_MST$..M_GOODS                       M_GOODS                            " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                     " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " M_GOODS.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                    & " WHERE                                                                      " & vbNewLine _
                                    & " B_INKA_S.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_L = @INOUTKA_NO_L                                         " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_S.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                    & " GROUP BY                                                                   " & vbNewLine _
                                    & " B_INKA_S.INKA_NO_M,B_INKA_S.LOT_NO                                         " & vbNewLine _
                                    & " )                                 B_INKA_S                                 " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_M.INKA_NO_M = B_INKA_S.INKA_NO_M                                    " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " $LM_MST$..M_CUST                       M_CUST                              " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                      " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                      " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                      " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " M_CUST.CUST_CD_S = '00'                                                    " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " M_CUST.CUST_CD_SS = '00'                                                   " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_L.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " M_CUST.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " $LM_MST$..M_GOODS                       M_GOODS                            " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                     " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " M_GOODS.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                    & " LEFT JOIN                                                                  " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_L                  H_INKAEDI_L                         " & vbNewLine _
                                    & " ON                                                                         " & vbNewLine _
                                    & " B_INKA_L.INKA_NO_L = H_INKAEDI_L.INKA_CTL_NO_L                             " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " M_GOODS.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                    & " WHERE                                                                      " & vbNewLine _
                                    & " B_INKA_L.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_L.INKA_NO_L = @INOUTKA_NO_L                                         " & vbNewLine _
                                    & " AND                                                                        " & vbNewLine _
                                    & " B_INKA_L.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                    & " ORDER BY B_INKA_M.INKA_NO_M                                                " & vbNewLine


    Private Const SQL_SELECT_OUTKA As String = " SELECT                                                         " & vbNewLine _
                                & " C_OUTKA_L.CUST_ORD_NO                                AS OUTKA_FROM_ORD_NO   " & vbNewLine _
                                & ",C_OUTKA_L.OUTKA_PLAN_DATE                            AS PLAN_DATE           " & vbNewLine _
                                & ",C_OUTKA_M.OUTKA_NO_M                                 AS INOUTKA_NO_M        " & vbNewLine _
                                & ",C_OUTKA_M.GOODS_CD_NRS                               AS GOODS_CD_NRS        " & vbNewLine _
                                & ",M_GOODS.GOODS_CD_CUST                                AS GOODS_CD_CUST       " & vbNewLine _
                                & ",M_GOODS.GOODS_NM_1                                   AS GOODS_NM            " & vbNewLine _
                                & ",C_OUTKA_M.LOT_NO                                     AS LOT_NO              " & vbNewLine _
                                & ",C_OUTKA_M.IRIME                                      AS IRIME               " & vbNewLine _
                                & ",C_OUTKA_M.IRIME_UT                                   AS IRIME_UT            " & vbNewLine _
                                & ",C_OUTKA_M.OUTKA_TTL_NB                               AS NB                  " & vbNewLine _
                                & ",ISNULL(H_OUTKAEDI_L.EDI_CTL_NO,'')                   AS EDI_CTL_NO          " & vbNewLine _
                                & ",C_OUTKA_L.CUST_CD_L                                  AS CUST_CD_L           " & vbNewLine _
                                & ",C_OUTKA_L.CUST_CD_M                                  AS CUST_CD_M           " & vbNewLine _
                                & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M           AS CUST_NM             " & vbNewLine _
                                & ",C_OUTKA_L.DEST_CD                                    AS DEST_CD             " & vbNewLine _
                                & ",M_DEST.DEST_NM                                       AS DEST_NM             " & vbNewLine _
                                & " FROM                                                                        " & vbNewLine _
                                & " $LM_TRN$..C_OUTKA_L                                                         " & vbNewLine _
                                & " LEFT JOIN                                                                   " & vbNewLine _
                                & " $LM_TRN$..C_OUTKA_M                     C_OUTKA_M                           " & vbNewLine _
                                & " ON                                                                          " & vbNewLine _
                                & " C_OUTKA_L.NRS_BR_CD = C_OUTKA_M.NRS_BR_CD                                   " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.OUTKA_NO_L = C_OUTKA_M.OUTKA_NO_L                                 " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_M.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                & " LEFT JOIN                                                                   " & vbNewLine _
                                & " $LM_MST$..M_CUST                       M_CUST                               " & vbNewLine _
                                & " ON                                                                          " & vbNewLine _
                                & " C_OUTKA_L.NRS_BR_CD = M_CUST.NRS_BR_CD                                      " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.CUST_CD_L = M_CUST.CUST_CD_L                                      " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.CUST_CD_M = M_CUST.CUST_CD_M                                      " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " M_CUST.CUST_CD_S = '00'                                                     " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " M_CUST.CUST_CD_SS = '00'                                                    " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " M_CUST.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                & " LEFT JOIN                                                                   " & vbNewLine _
                                & " $LM_MST$..M_GOODS                       M_GOODS                             " & vbNewLine _
                                & " ON                                                                          " & vbNewLine _
                                & " C_OUTKA_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                                     " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_M.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                               " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " M_GOODS.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                & " LEFT JOIN                                                                   " & vbNewLine _
                                & " $LM_TRN$..H_OUTKAEDI_L                  H_OUTKAEDI_L                        " & vbNewLine _
                                & " ON                                                                          " & vbNewLine _
                                & " C_OUTKA_L.OUTKA_NO_L = H_OUTKAEDI_L.OUTKA_CTL_NO                            " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " M_GOODS.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                & " LEFT JOIN                                                                   " & vbNewLine _
                                & " $LM_MST$..M_DEST                       M_DEST                               " & vbNewLine _
                                & " ON                                                                          " & vbNewLine _
                                & " C_OUTKA_L.NRS_BR_CD = M_DEST.NRS_BR_CD                                      " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.CUST_CD_L = M_DEST.CUST_CD_L                                      " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.DEST_CD = M_DEST.DEST_CD                                          " & vbNewLine _
                                & " WHERE                                                                       " & vbNewLine _
                                & " C_OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                            " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.OUTKA_NO_L = @INOUTKA_NO_L                                        " & vbNewLine _
                                & " AND                                                                         " & vbNewLine _
                                & " C_OUTKA_L.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
                                & " ORDER BY C_OUTKA_M.OUTKA_NO_M                                               " & vbNewLine


#End Region   '検索

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

#Region "検索処理"

#Region "初期検索処理(EDI_L)"

    ''' <summary>
    ''' 初期検索処理(EDI_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '2012.02.29 大阪対応START
        'SQL作成
        If _Row("INOUT_KB").Equals("1") Then
            Me._StrSql.Append(LMH070DAC.SQL_SELECT_INKAEDI_L)
            If String.IsNullOrEmpty(_Row("RCV_NM_HED").ToString()) = False Then
                Me._StrSql.Append(LMH070DAC.SQL_FROM_RCVTABLE_IN)
                If _Row("TBL_INOUT").Equals("1") = True Then
                    Me._StrSql.Append(LMH070DAC.SQL_FROM_RCVTABLE_INOUT_IN)
                Else
                End If
            Else
                Me._StrSql.Append(LMH070DAC.SQL_FROM_RCVTABLE_NULL_IN)
            End If
            Me._StrSql.Append(LMH070DAC.SQL_FROM_WHERE_IN)
        Else
            Me._StrSql.Append(LMH070DAC.SQL_SELECT_OUTKAEDI_L)
            If String.IsNullOrEmpty(_Row("RCV_NM_HED").ToString()) = False Then
                Me._StrSql.Append(LMH070DAC.SQL_FROM_RCVTABLE_OUT)
                If _Row("TBL_INOUT").Equals("1") = True Then
                    Me._StrSql.Append(LMH070DAC.SQL_FROM_RCVTABLE_INOUT_OUT)
                Else
                End If
            Else
                Me._StrSql.Append(LMH070DAC.SQL_FROM_RCVTABLE_NULL_OUT)
            End If
            Me._StrSql.Append(LMH070DAC.SQL_FROM_WHERE_OUT)
        End If
        '2012.02.29 大阪対応END

        'パラメータ設定
        Call Me.SetPrm()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '受信HEDテーブル名設定
        sql = Me.SetRcvTableNm(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH070DAC", "SelectListDataL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("INOUTKA_DATE", "INOUTKA_DATE")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("RCV_UPD_DATE", "RCV_UPD_DATE")
        map.Add("RCV_UPD_TIME", "RCV_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH070OUT_L")
        reader.Close()

        Return ds

    End Function

#End Region

#Region "初期検索処理(EDI_M)"

    ''' <summary>
    ''' 初期検索処理(EDI_M)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If _Row("INOUT_KB").Equals("1") Then
            Me._StrSql.Append(LMH070DAC.SQL_SELECT_INKAEDI_M)
        Else
            Me._StrSql.Append(LMH070DAC.SQL_SELECT_OUTKAEDI_M)
        End If

        'パラメータ設定
        Call Me.SetPrm()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH070DAC", "SelectListDataM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB", "NB")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH070OUT_M")
        reader.Close()

        Return ds

    End Function

#End Region

#Region "検索処理(入出荷)"

    ''' <summary>
    ''' 検索処理(入出荷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH070IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        If _Row("INOUT_KB").Equals("1") Then
            Me._StrSql.Append(LMH070DAC.SQL_SELECT_INKA)
        Else
            Me._StrSql.Append(LMH070DAC.SQL_SELECT_OUTKA)
        End If

        'パラメータ設定
        Call Me.SetPrm()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH070DAC", "SelectInOutka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("PLAN_DATE", "PLAN_DATE")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("NB", "NB")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH070_INOUTKA")
        reader.Close()

        Return ds

    End Function

#End Region

#Region "パラメータ設定処理"
    ''' <summary>
    ''' パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPrm()

        Dim nrsBrCd As String = Me._Row("NRS_BR_CD").ToString()
        Dim ediCtlNo As String = Me._Row("EDI_CTL_NO").ToString()
        Dim inoutkaNo As String = Me._Row("INOUTKA_NO").ToString()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", nrsBrCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", ediCtlNo, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", inoutkaNo, DBDataType.NVARCHAR))

    End Sub

#End Region


#End Region

#Region "SQL"

#Region "スキーマ名称設定"
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
#End Region

#Region "受信テーブル名設定"

    Private Function SetRcvTableNm(ByVal sql As String) As String

        Dim rcvTblNm As String = _Row("RCV_NM_HED").ToString()
        sql = sql.Replace("$RCV_HED$", rcvTblNm)

        Return sql

    End Function

#End Region

#End Region 'SQL

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        If String.IsNullOrEmpty(value.ToString()) = True Then
            value = 0
        End If

        Return value

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region

#End Region 'Method

End Class
