' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  103　　　 : 富士フイルム(千葉)
'  作  成  者       :  terakawa
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
Public Class LMH010DAC103
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理"

#Region "SELECT_EDI_L"
    ''' <summary>
    ''' 更新用検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EDI_L As String = " SELECT                                                  " & vbNewLine _
                                    & "-- (2014.06.09) FFEM 修正START                                    " & vbNewLine _
                                    & "-- '0'                                     AS DEL_KB                " & vbNewLine _
                                    & " CASE WHEN H_INKAEDI_L.DEL_KB = '2' THEN H_INKAEDI_L.DEL_KB       " & vbNewLine _
                                    & "      WHEN H_INKAEDI_L.DEL_KB = '3' THEN H_INKAEDI_L.DEL_KB ELSE '0' END DEL_KB    " & vbNewLine _
                                    & "-- (2014.06.09) FFEM 修正END                                      " & vbNewLine _
                                    & ",H_INKAEDI_L.NRS_BR_CD                   AS NRS_BR_CD             " & vbNewLine _
                                    & ",H_INKAEDI_L.EDI_CTL_NO                  AS EDI_CTL_NO            " & vbNewLine _
                                    & ",H_INKAEDI_L.INKA_CTL_NO_L               AS INKA_CTL_NO_L         " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.INKA_TP <> '' THEN H_INKAEDI_L.INKA_TP     " & vbNewLine _
                                    & "      ELSE '10'                                                   " & vbNewLine _
                                    & " END                                     AS INKA_TP               " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.INKA_KB <> '' THEN H_INKAEDI_L.INKA_KB     " & vbNewLine _
                                    & "      ELSE '10'                                                   " & vbNewLine _
                                    & " END                                     AS INKA_KB               " & vbNewLine _
                                    & ",H_INKAEDI_L.INKA_STATE_KB               AS INKA_STATE_KB         " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.INKA_STATE_KB <> '' THEN H_INKAEDI_L.INKA_STATE_KB " & vbNewLine _
                                    & "      ELSE '10'                                                   " & vbNewLine _
                                    & " END                                     AS INKA_STATE_KB         " & vbNewLine _
                                    & ",H_INKAEDI_L.INKA_DATE                   AS INKA_DATE             " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.INKA_TIME <> '' THEN H_INKAEDI_L.INKA_TIME " & vbNewLine _
                                    & "      ELSE '0900'                                                 " & vbNewLine _
                                    & " END                                     AS INKA_TIME             " & vbNewLine _
                                    & ",H_INKAEDI_L.NRS_WH_CD                   AS NRS_WH_CD             " & vbNewLine _
                                    & ",H_INKAEDI_L.CUST_CD_L                   AS CUST_CD_L             " & vbNewLine _
                                    & ",H_INKAEDI_L.CUST_CD_M                   AS CUST_CD_M             " & vbNewLine _
                                    & ",M_CUST.CUST_NM_L                        AS CUST_NM_L             " & vbNewLine _
                                    & ",M_CUST.CUST_NM_M                        AS CUST_NM_M             " & vbNewLine _
                                    & ",H_INKAEDI_L.INKA_PLAN_QT                AS INKA_PLAN_QT          " & vbNewLine _
                                    & ",H_INKAEDI_L.INKA_PLAN_QT_UT             AS INKA_PLAN_QT_UT       " & vbNewLine _
                                    & ",H_INKAEDI_L.INKA_TTL_NB                 AS INKA_TTL_NB           " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.NAIGAI_KB <> '' THEN H_INKAEDI_L.NAIGAI_KB " & vbNewLine _
                                    & "      ELSE '01'                                                   " & vbNewLine _
                                    & " END                                     AS NAIGAI_KB             " & vbNewLine _
                                    & ",H_INKAEDI_L.BUYER_ORD_NO                AS BUYER_ORD_NO          " & vbNewLine _
                                    & ",H_INKAEDI_L.OUTKA_FROM_ORD_NO           AS OUTKA_FROM_ORD_NO     " & vbNewLine _
                                    & ",H_INKAEDI_L.SEIQTO_CD                   AS SEIQTO_CD             " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.TOUKI_HOKAN_YN <> '' THEN H_INKAEDI_L.TOUKI_HOKAN_YN " & vbNewLine _
                                    & "      ELSE '1'                                                    " & vbNewLine _
                                    & " END                                     AS TOUKI_HOKAN_YN        " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.HOKAN_YN <> '' THEN H_INKAEDI_L.HOKAN_YN   " & vbNewLine _
                                    & "      ELSE '1'                                                    " & vbNewLine _
                                    & " END                                     AS HOKAN_YN              " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.HOKAN_FREE_KIKAN <> '0' THEN H_INKAEDI_L.HOKAN_FREE_KIKAN " & vbNewLine _
                                    & "      ELSE M_CUST.HOKAN_FREE_KIKAN                                " & vbNewLine _
                                    & " END                                     AS HOKAN_FREE_KIKAN      " & vbNewLine _
                                    & "--,H_INKAEDI_L.HOKAN_STR_DATE              AS HOKAN_STR_DATE        " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.HOKAN_STR_DATE <> '' THEN H_INKAEDI_L.HOKAN_STR_DATE " & vbNewLine _
                                    & "      ELSE H_INKAEDI_L.INKA_DATE                                  " & vbNewLine _
                                    & " END                                     AS HOKAN_STR_DATE        " & vbNewLine _
                                    & ",H_INKAEDI_L.NIYAKU_YN                   AS NIYAKU_YN             " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.NIYAKU_YN <> '' THEN H_INKAEDI_L.NIYAKU_YN " & vbNewLine _
                                    & "      ELSE '1'                                                    " & vbNewLine _
                                    & " END                                     AS NIYAKU_YN             " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.TAX_KB <> '' THEN H_INKAEDI_L.TAX_KB       " & vbNewLine _
                                    & "      ELSE '01'                                                   " & vbNewLine _
                                    & " END                                     AS TAX_KB                " & vbNewLine _
                                    & ",H_INKAEDI_L.REMARK                      AS REMARK                " & vbNewLine _
                                    & ",H_INKAEDI_L.NYUBAN_L                    AS NYUBAN_L              " & vbNewLine _
                                    & ",CASE WHEN H_INKAEDI_L.UNCHIN_TP <> '' THEN H_INKAEDI_L.UNCHIN_TP " & vbNewLine _
                                    & "      ELSE '90'                                                   " & vbNewLine _
                                    & " END                                     AS UNCHIN_TP             " & vbNewLine _
                                    & ",H_INKAEDI_L.UNCHIN_KB                   AS UNCHIN_KB             " & vbNewLine _
                                    & ",H_INKAEDI_L.OUTKA_MOTO                  AS OUTKA_MOTO            " & vbNewLine _
                                    & ",H_INKAEDI_L.SYARYO_KB                   AS SYARYO_KB             " & vbNewLine _
                                    & ",H_INKAEDI_L.UNSO_ONDO_KB                AS UNSO_ONDO_KB          " & vbNewLine _
                                    & ",H_INKAEDI_L.UNSO_CD                     AS UNSO_CD               " & vbNewLine _
                                    & ",H_INKAEDI_L.UNSO_BR_CD                  AS UNSO_BR_CD            " & vbNewLine _
                                    & ",H_INKAEDI_L.UNCHIN                      AS UNCHIN                " & vbNewLine _
                                    & ",H_INKAEDI_L.YOKO_TARIFF_CD              AS YOKO_TARIFF_CD        " & vbNewLine _
                                    & ",'1'                                     AS OUT_FLAG              " & vbNewLine _
                                    & ",H_INKAEDI_L.AKAKURO_KB                  AS AKAKURO_KB            " & vbNewLine _
                                    & ",H_INKAEDI_L.JISSEKI_FLAG                AS JISSEKI_FLAG          " & vbNewLine _
                                    & ",H_INKAEDI_L.JISSEKI_USER                AS JISSEKI_USER          " & vbNewLine _
                                    & ",H_INKAEDI_L.JISSEKI_DATE                AS JISSEKI_DATE          " & vbNewLine _
                                    & ",H_INKAEDI_L.JISSEKI_TIME                AS JISSEKI_TIME          " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N01                    AS FREE_N01              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N02                    AS FREE_N02              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N03                    AS FREE_N03              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N04                    AS FREE_N04              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N05                    AS FREE_N05              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N06                    AS FREE_N06              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N07                    AS FREE_N07              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N08                    AS FREE_N08              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N09                    AS FREE_N09              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_N10                    AS FREE_N10              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C01                    AS FREE_C01              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C02                    AS FREE_C02              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C03                    AS FREE_C03              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C04                    AS FREE_C04              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C05                    AS FREE_C05              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C06                    AS FREE_C06              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C07                    AS FREE_C07              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C08                    AS FREE_C08              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C09                    AS FREE_C09              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C10                    AS FREE_C10              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C11                    AS FREE_C11              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C12                    AS FREE_C12              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C13                    AS FREE_C13              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C14                    AS FREE_C14              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C15                    AS FREE_C15              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C16                    AS FREE_C16              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C17                    AS FREE_C17              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C18                    AS FREE_C18              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C19                    AS FREE_C19              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C20                    AS FREE_C20              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C21                    AS FREE_C21              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C22                    AS FREE_C22              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C23                    AS FREE_C23              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C24                    AS FREE_C24              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C25                    AS FREE_C25              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C26                    AS FREE_C26              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C27                    AS FREE_C27              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C28                    AS FREE_C28              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C29                    AS FREE_C29              " & vbNewLine _
                                    & ",H_INKAEDI_L.FREE_C30                    AS FREE_C30              " & vbNewLine _
                                    & ",H_INKAEDI_L.CRT_USER                    AS CRT_USER              " & vbNewLine _
                                    & ",H_INKAEDI_L.CRT_DATE                    AS CRT_DATE              " & vbNewLine _
                                    & ",H_INKAEDI_L.CRT_TIME                    AS CRT_TIME              " & vbNewLine _
                                    & ",H_INKAEDI_L.UPD_USER                    AS UPD_USER              " & vbNewLine _
                                    & ",H_INKAEDI_L.UPD_DATE                    AS UPD_DATE              " & vbNewLine _
                                    & ",H_INKAEDI_L.UPD_TIME                    AS UPD_TIME              " & vbNewLine _
                                    & ",H_INKAEDI_L.EDIT_FLAG                   AS EDIT_FLAG	         " & vbNewLine _
                                    & ",H_INKAEDI_L.MATCHING_FLAG               AS MATCHING_FLAG	     " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_ENT_DATE                AS SYS_ENT_DATE          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_ENT_TIME                AS SYS_ENT_TIME          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_ENT_PGID                AS SYS_ENT_PGID          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_ENT_USER                AS SYS_ENT_USER          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_UPD_DATE                AS SYS_UPD_DATE          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_UPD_TIME                AS SYS_UPD_TIME          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_UPD_PGID                AS SYS_UPD_PGID          " & vbNewLine _
                                    & ",H_INKAEDI_L.SYS_UPD_USER                AS SYS_UPD_USER          " & vbNewLine _
                                    & ",'0'                                     AS SYS_DEL_FLG           " & vbNewLine _
                                    & " FROM                                                             " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_L                                            " & vbNewLine _
                                    & " LEFT JOIN                                                        " & vbNewLine _
                                    & " $LM_MST$..M_CUST                       M_CUST                    " & vbNewLine _
                                    & " ON                                                               " & vbNewLine _
                                    & " H_INKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                         " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                         " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " M_CUST.CUST_CD_S = '00'                                          " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " M_CUST.CUST_CD_SS = '00'                                         " & vbNewLine _
                                    & " WHERE                                                            " & vbNewLine _
                                    & " H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.NRS_WH_CD = @NRS_WH_CD                               " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.EDI_CTL_NO = @EDI_CTL_NO                             " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.OUT_FLAG = '0'                                       " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.SYS_UPD_DATE = @SYS_UPD_DATE                         " & vbNewLine _
                                    & " AND                                                              " & vbNewLine _
                                    & " H_INKAEDI_L.SYS_UPD_TIME = @SYS_UPD_TIME                         " & vbNewLine




#End Region  '入荷登録

#Region "SELECT_EDI_M"

    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EDI_M As String = " SELECT                                                  " & vbNewLine _
                                   & "CASE WHEN H_INKAEDI_M.DEL_KB = '3' THEN '0'                        " & vbNewLine _
                                   & "      ELSE H_INKAEDI_M.DEL_KB                                      " & vbNewLine _
                                   & " END                                     AS DEL_KB                 " & vbNewLine _
                                   & ",H_INKAEDI_M.NRS_BR_CD                   AS NRS_BR_CD              " & vbNewLine _
                                   & ",H_INKAEDI_M.EDI_CTL_NO                  AS EDI_CTL_NO             " & vbNewLine _
                                   & ",H_INKAEDI_M.EDI_CTL_NO_CHU              AS EDI_CTL_NO_CHU         " & vbNewLine _
                                   & ",H_INKAEDI_M.INKA_CTL_NO_L               AS INKA_CTL_NO_L          " & vbNewLine _
                                   & ",H_INKAEDI_M.INKA_CTL_NO_M               AS INKA_CTL_NO_M          " & vbNewLine _
                                   & ",H_INKAEDI_M.NRS_GOODS_CD                AS NRS_GOODS_CD           " & vbNewLine _
                                   & ",H_INKAEDI_M.CUST_GOODS_CD               AS CUST_GOODS_CD          " & vbNewLine _
                                   & ",H_INKAEDI_M.GOODS_NM                    AS GOODS_NM               " & vbNewLine _
                                   & ",H_INKAEDI_M.NB                          AS NB                     " & vbNewLine _
                                   & ",H_INKAEDI_M.NB_UT                       AS NB_UT                  " & vbNewLine _
                                   & ",H_INKAEDI_M.PKG_NB                      AS PKG_NB                 " & vbNewLine _
                                   & ",H_INKAEDI_M.PKG_UT                      AS PKG_UT                 " & vbNewLine _
                                   & ",H_INKAEDI_M.INKA_PKG_NB                 AS INKA_PKG_NB            " & vbNewLine _
                                   & ",H_INKAEDI_M.HASU                        AS HASU                   " & vbNewLine _
                                   & ",H_INKAEDI_M.STD_IRIME                   AS STD_IRIME              " & vbNewLine _
                                   & ",H_INKAEDI_M.STD_IRIME_UT                AS STD_IRIME_UT           " & vbNewLine _
                                   & ",H_INKAEDI_M.BETU_WT                     AS BETU_WT                " & vbNewLine _
                                   & ",H_INKAEDI_M.CBM                         AS CBM                    " & vbNewLine _
                                   & ",H_INKAEDI_M.ONDO_KB                     AS ONDO_KB                " & vbNewLine _
                                   & ",H_INKAEDI_M.OUTKA_FROM_ORD_NO           AS OUTKA_FROM_ORD_NO      " & vbNewLine _
                                   & ",H_INKAEDI_M.BUYER_ORD_NO                AS BUYER_ORD_NO           " & vbNewLine _
                                   & ",H_INKAEDI_M.REMARK                      AS REMARK                 " & vbNewLine _
                                   & ",H_INKAEDI_M.LOT_NO                      AS LOT_NO                 " & vbNewLine _
                                   & ",H_INKAEDI_M.SERIAL_NO                   AS SERIAL_NO              " & vbNewLine _
                                   & ",H_INKAEDI_M.IRIME                       AS IRIME                  " & vbNewLine _
                                   & ",H_INKAEDI_M.IRIME_UT                    AS IRIME_UT               " & vbNewLine _
                                   & ",H_INKAEDI_M.OUT_KB                      AS OUT_KB                 " & vbNewLine _
                                   & ",H_INKAEDI_M.AKAKURO_KB                  AS AKAKURO_KB             " & vbNewLine _
                                   & ",H_INKAEDI_M.JISSEKI_FLAG                AS JISSEKI_FLAG           " & vbNewLine _
                                   & ",H_INKAEDI_M.JISSEKI_USER                AS JISSEKI_USER           " & vbNewLine _
                                   & ",H_INKAEDI_M.JISSEKI_DATE                AS JISSEKI_DATE           " & vbNewLine _
                                   & ",H_INKAEDI_M.JISSEKI_TIME                AS JISSEKI_TIME           " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N01                    AS FREE_N01               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N02                    AS FREE_N02               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N03                    AS FREE_N03               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N04                    AS FREE_N04               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N05                    AS FREE_N05               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N06                    AS FREE_N06               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N07                    AS FREE_N07               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N08                    AS FREE_N08               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N09                    AS FREE_N09               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_N10                    AS FREE_N10               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C01                    AS FREE_C01               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C02                    AS FREE_C02               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C03                    AS FREE_C03               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C04                    AS FREE_C04               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C05                    AS FREE_C05               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C06                    AS FREE_C06               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C07                    AS FREE_C07               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C08                    AS FREE_C08               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C09                    AS FREE_C09               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C10                    AS FREE_C10               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C11                    AS FREE_C11               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C12                    AS FREE_C12               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C13                    AS FREE_C13               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C14                    AS FREE_C14               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C15                    AS FREE_C15               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C16                    AS FREE_C16               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C17                    AS FREE_C17               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C18                    AS FREE_C18               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C19                    AS FREE_C19               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C20                    AS FREE_C20               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C21                    AS FREE_C21               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C22                    AS FREE_C22               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C23                    AS FREE_C23               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C24                    AS FREE_C24               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C25                    AS FREE_C25               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C26                    AS FREE_C26               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C27                    AS FREE_C27               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C28                    AS FREE_C28               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C29                    AS FREE_C29               " & vbNewLine _
                                   & ",H_INKAEDI_M.FREE_C30                    AS FREE_C30               " & vbNewLine _
                                   & ",H_INKAEDI_M.CRT_USER                    AS CRT_USER               " & vbNewLine _
                                   & ",H_INKAEDI_M.CRT_DATE                    AS CRT_DATE               " & vbNewLine _
                                   & ",H_INKAEDI_M.CRT_TIME                    AS CRT_TIME               " & vbNewLine _
                                   & ",H_INKAEDI_M.UPD_USER                    AS UPD_USER               " & vbNewLine _
                                   & ",H_INKAEDI_M.UPD_DATE                    AS UPD_DATE               " & vbNewLine _
                                   & ",H_INKAEDI_M.UPD_TIME                    AS UPD_TIME               " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_ENT_DATE                AS SYS_ENT_DATE           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_ENT_TIME                AS SYS_ENT_TIME           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_ENT_PGID                AS SYS_ENT_PGID           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_ENT_USER                AS SYS_ENT_USER           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_UPD_DATE                AS SYS_UPD_DATE           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_UPD_TIME                AS SYS_UPD_TIME           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_UPD_PGID                AS SYS_UPD_PGID           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_UPD_USER                AS SYS_UPD_USER           " & vbNewLine _
                                   & ",H_INKAEDI_M.SYS_DEL_FLG                 AS SYS_DEL_FLG            " & vbNewLine _
                                   & " FROM                                                              " & vbNewLine _
                                   & " $LM_TRN$..H_INKAEDI_M                                             " & vbNewLine _
                                   & " WHERE                                                             " & vbNewLine _
                                   & " H_INKAEDI_M.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                   & " AND                                                               " & vbNewLine _
                                   & " H_INKAEDI_M.EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine _
                                   & " AND                                                               " & vbNewLine _
                                   & " H_INKAEDI_M.OUT_KB = '0'                                          " & vbNewLine _
                                   & "-- AND                                                               " & vbNewLine _
                                   & "-- H_INKAEDI_M.SYS_DEL_FLG = '0'                                     " & vbNewLine

    Private Const SQL_SELECT_SYS_DEL_ON As String = "AND                                             " & vbNewLine _
                                   & " H_INKAEDI_M.DEL_KB = '2'                                      " & vbNewLine

    Private Const SQL_SELECT_SYS_DEL_OFF As String = "AND                                            " & vbNewLine _
                                   & " H_INKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine



#End Region  '入荷登録

#Region "SELECT_SEND(富士フイルム専用)"

    ''' <summary>
    ''' SELECT_SEND(富士フイルム専用)変更(保留)データの実績作成
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_SELECT_SEND_FJF_EDIT As String =
                                "SELECT                                                   " & vbNewLine _
                              & "'0'                   AS DEL_KB                          " & vbNewLine _
                              & ",HIEM.FREE_C15         AS CRT_DATE                       " & vbNewLine _
                              & ",HIEM.FREE_C16         AS FILE_NAME                      " & vbNewLine _
                              & ",HIEM.FREE_C17         AS REC_NO                         " & vbNewLine _
                              & ",HIEM.FREE_C18         AS GYO                            " & vbNewLine _
                              & ",ISNULL((SELECT                                          " & vbNewLine _
                              & "MAX(CONVERT(Int,SIF.GYO))                                " & vbNewLine _
                              & "FROM $LM_TRN$..H_SENDINEDI_FJF AS SIF                    " & vbNewLine _
                              & "WHERE                                                    " & vbNewLine _
                              & "SIF.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "SIF.INKA_CTL_NO_L = @INKA_CTL_NO_L                       " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "SIF.INKA_CTL_NO_M = @INKA_CTL_NO_M                       " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "SIF.SYS_DEL_FLG = '0'                                    " & vbNewLine _
                              & "),0) + 1                AS EDA                           " & vbNewLine _
                              & ",HIEL.NRS_BR_CD                                          " & vbNewLine _
                              & ",HIEM.EDI_CTL_NO                                         " & vbNewLine _
                              & ",HIEM.EDI_CTL_NO_CHU                                     " & vbNewLine _
                              & ",@INKA_CTL_NO_L        AS INKA_CTL_NO_L                  " & vbNewLine _
                              & ",@INKA_CTL_NO_M        AS INKA_CTL_NO_M                  " & vbNewLine _
                              & ",''                    AS INKA_CTL_NO_S                  " & vbNewLine _
                              & ",HIEL.CUST_CD_L                                          " & vbNewLine _
                              & ",HIEL.CUST_CD_M                                          " & vbNewLine _
                              & ",'0'                   AS ZFVYCANKB                      " & vbNewLine _
                              & ",'10'                  AS FILE_ID                        " & vbNewLine _
                              & ",HIEL.FREE_C02         AS ZFVYKAISYAC                    " & vbNewLine _
                              & ",HIEL.OUTKA_FROM_ORD_NO       AS ZFVYDENNO               " & vbNewLine _
                              & ",HIEM.FREE_C19         AS ZFVYMEINO                      " & vbNewLine _
                              & ",HIEM.CUST_GOODS_CD    AS MATNR                          " & vbNewLine _
                              & ",HIEM.LOT_NO           AS CHARG                          " & vbNewLine _
                              & "--,CASE WHEN LEN(@GOODS_KANRI_NO) = 8 THEN @GOODS_KANRI_NO " & vbNewLine _
                              & "-- ELSE ''               END CAN_NO                        " & vbNewLine _
                              & ",''                    AS CAN_NO                        " & vbNewLine _
                              & ",HIEL.FREE_C16         AS ZFVYDENTYP                     " & vbNewLine _
                              & ",HIEL.INKA_DATE         AS ZFVYWADAT                      " & vbNewLine _
                              & ",HIEM.FREE_C03         AS ZFVYKWERKS                     " & vbNewLine _
                              & ",HIEM.FREE_C04         AS LGORT                          " & vbNewLine _
                              & ",''                    AS SEIZOU_DATE                    " & vbNewLine _
                              & ",''                    AS HOSHOU_KIGEN                   " & vbNewLine _
                              & ",''                    AS SERIAL_NO                      " & vbNewLine _
                              & ",''                    AS LABEL_SEQ                      " & vbNewLine _
                              & ",HIEM.NB               AS ZFVYSURYO                      " & vbNewLine _
                              & ",''                    AS RECORD_STATUS                  " & vbNewLine _
                              & "--,CASE WHEN LEN(@GOODS_KANRI_NO) = 8 THEN '1'             " & vbNewLine _
                              & "-- ELSE '0'              END CANI_HOUKOKU_FLG              " & vbNewLine _
                              & ",'0'                   AS CANI_HOUKOKU_FLG              " & vbNewLine _
                              & ",'2'                   AS JISSEKI_SHORI_FLG              " & vbNewLine _
                              & ",'0'                   AS LATEST_HOUKOKU_FLG             " & vbNewLine _
                              & ",''                    AS JISSEKI_USER                   " & vbNewLine _
                              & ",''                    AS JISSEKI_DATE                   " & vbNewLine _
                              & ",''                    AS JISSEKI_TIME                   " & vbNewLine _
                              & ",''                    AS SEND_USER                      " & vbNewLine _
                              & ",''                    AS SEND_DATE                      " & vbNewLine _
                              & ",''                    AS SEND_TIME                      " & vbNewLine _
                              & "--,CASE WHEN LEN(@GOODS_KANRI_NO) = 8 THEN '2'             " & vbNewLine _
                              & "-- ELSE '0'              END CANI_HOUKOKU_SHORI_FLG        " & vbNewLine _
                              & ",'0'                   AS CANI_HOUKOKU_SHORI_FLG        " & vbNewLine _
                              & ",''                    AS CANI_HOUKOKU_USER              " & vbNewLine _
                              & ",''                    AS CANI_HOUKOKU_DATE              " & vbNewLine _
                              & ",''                    AS CANI_HOUKOKU_TIME              " & vbNewLine _
                              & "FROM $LM_TRN$..H_INKAEDI_L HIEL                          " & vbNewLine _
                              & "LEFT JOIN                                                " & vbNewLine _
                              & "$LM_TRN$..H_INKAEDI_M HIEM                               " & vbNewLine _
                              & "ON                                                       " & vbNewLine _
                              & "HIEL.NRS_BR_CD = HIEM.NRS_BR_CD                          " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "HIEL.EDI_CTL_NO = HIEM.EDI_CTL_NO                        " & vbNewLine _
                              & "WHERE                                                    " & vbNewLine _
                              & "HIEL.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "HIEL.EDI_CTL_NO = @EDI_CTL_NO                            " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "HIEM.EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU                    " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "HIEL.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                              & "AND                                                      " & vbNewLine _
                              & "HIEL.DEL_KB = '3'                                        " & vbNewLine


    ''' <summary>
    ''' SELECT_SEND(富士フイルム専用)キャンセルデータの実績作成
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_SELECT_SEND_FJF_CANCEL As String = "SELECT                                              " & vbNewLine _
                              & "                            INFJF.DEL_KB                                     " & vbNewLine _
                              & "                          ,DTLFJF.CRT_DATE                                   " & vbNewLine _
                              & "                          ,DTLFJF.FILE_NAME                                  " & vbNewLine _
                              & "                          ,DTLFJF.REC_NO                                     " & vbNewLine _
                              & "                          ,DTLFJF.GYO                                        " & vbNewLine _
                              & "                          ,''                      AS EDA                    " & vbNewLine _
                              & "                          ,DTLFJF.NRS_BR_CD                                  " & vbNewLine _
                              & "--                          ,DTLFJF.EDI_CTL_NO                                 " & vbNewLine _
                              & "                          ,@EDI_CTL_NO             AS EDI_CTL_NO          " & vbNewLine _
                              & "                          ,DTLFJF.EDI_CTL_NO_CHU                             " & vbNewLine _
                              & "                          ,DTLFJF.INKA_CTL_NO_L    AS INKA_CTL_NO_L          " & vbNewLine _
                              & "                          ,DTLFJF.INKA_CTL_NO_M    AS INKA_CTL_NO_M          " & vbNewLine _
                              & "                          ,''                 AS INKA_CTL_NO_S               " & vbNewLine _
                              & "                          ,INFJF.CUST_CD_L                                   " & vbNewLine _
                              & "                          ,INFJF.CUST_CD_M                                   " & vbNewLine _
                              & "                          ,'1'                AS ZFVYCANKB                   " & vbNewLine _
                              & "                          ,INFJF.FILE_ID                                     " & vbNewLine _
                              & "                          ,INFJF.ZFVYKAISYAC                                 " & vbNewLine _
                              & "                          ,INFJF.ZFVYDENNO                                   " & vbNewLine _
                              & "                          ,INFJF.ZFVYMEINO                                   " & vbNewLine _
                              & "                          ,INFJF.MATNR                                       " & vbNewLine _
                              & "                          ,INFJF.CHARG                                       " & vbNewLine _
                              & "                          ,INFJF.CAN_NO                                      " & vbNewLine _
                              & "                          ,INFJF.ZFVYDENTYP                                  " & vbNewLine _
                              & "                          ,INFJF.ZFVYWADAT                                   " & vbNewLine _
                              & "                          ,INFJF.ZFVYHWERKS                                  " & vbNewLine _
                              & "                          ,INFJF.OUT_LGORT                                   " & vbNewLine _
                              & "                          ,INFJF.ZFVYKWERKS                                  " & vbNewLine _
                              & "                          ,INFJF.IN_LGORT                                    " & vbNewLine _
                              & "                          ,INFJF.SEIZOU_DATE                                 " & vbNewLine _
                              & "                          ,INFJF.HOSHOU_KIGEN                                " & vbNewLine _
                              & "                          ,INFJF.SERIAL_NO                                   " & vbNewLine _
                              & "                          ,INFJF.LABEL_SEQ                                   " & vbNewLine _
                              & "                          ,INFJF.ZFVYSURYO                                   " & vbNewLine _
                              & "                          ,INFJF.RECORD_STATUS                               " & vbNewLine _
                              & "                          ,INFJF.CANI_HOUKOKU_FLG                            " & vbNewLine _
                              & "                          ,'2'                  AS JISSEKI_SHORI_FLG         " & vbNewLine _
                              & "                          ,'0'                  AS LATEST_HOUKOKU_FLG        " & vbNewLine _
                              & "                          ,INFJF.JISSEKI_USER                                " & vbNewLine _
                              & "                          ,INFJF.JISSEKI_DATE                                " & vbNewLine _
                              & "                          ,INFJF.JISSEKI_TIME                                " & vbNewLine _
                              & "                          ,INFJF.SEND_USER                                   " & vbNewLine _
                              & "                          ,INFJF.SEND_DATE                                   " & vbNewLine _
                              & "                          ,INFJF.SEND_TIME                                   " & vbNewLine _
                              & "                          ,INFJF.CANI_HOUKOKU_SHORI_FLG                      " & vbNewLine _
                              & "                          ,INFJF.CANI_HOUKOKU_USER                           " & vbNewLine _
                              & "                          ,INFJF.CANI_HOUKOKU_DATE                           " & vbNewLine _
                              & "                          ,INFJF.CANI_HOUKOKU_TIME                           " & vbNewLine _
                              & "                          ,INFJF.SYS_DEL_FLG                                 " & vbNewLine _
                              & "                       FROM $LM_TRN$..H_SENDINEDI_FJF  INFJF                 " & vbNewLine _
                              & "                       LEFT JOIN                                             " & vbNewLine _
                              & "						   (SELECT                                            " & vbNewLine _
                              & "							SUB_DTL.NRS_BR_CD                                 " & vbNewLine _
                              & "						   ,SUB_DTL.CUST_CD_L                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.CUST_CD_M                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.ZFVYDENNO                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.ZFVYMEINO                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.CRT_DATE    AS CRT_DATE                                    " & vbNewLine _
                              & "						   ,SUB_DTL.FILE_NAME   AS FILE_NAME                                   " & vbNewLine _
                              & "						   ,SUB_DTL.REC_NO      AS REC_NO                                      " & vbNewLine _
                              & "						   ,SUB_DTL.GYO         AS GYO                                         " & vbNewLine _
                              & "						   ,SUB_DTL.EDI_CTL_NO                                                 " & vbNewLine _
                              & "						   ,SUB_DTL.EDI_CTL_NO_CHU                                             " & vbNewLine _
                              & "						   ,SUB_DTL.INKA_CTL_NO_L    AS INKA_CTL_NO_L                          " & vbNewLine _
                              & "						   ,SUB_DTL.INKA_CTL_NO_M    AS INKA_CTL_NO_M                          " & vbNewLine _
                              & "						   ,SUB_DTL.INOUT_KB                                                   " & vbNewLine _
                              & "						   ,SUB_DTL.JISSEKI_SHORI_FLG                                          " & vbNewLine _
                              & "						   FROM                                                                " & vbNewLine _
                              & "						   $LM_TRN$..H_INOUTKAEDI_DTL_FJF SUB_DTL                              " & vbNewLine _
                              & "							   ,(SELECT                                                        " & vbNewLine _
                              & "							   NRS_BR_CD                                                       " & vbNewLine _
                              & "							   ,CUST_CD_L                                                      " & vbNewLine _
                              & "							   ,CUST_CD_M                                                      " & vbNewLine _
                              & "							   ,ZFVYDENNO                                                      " & vbNewLine _
                              & "							   ,ZFVYMEINO                                                      " & vbNewLine _
                              & "							   ,MAX(SYS_ENT_DATE + SYS_ENT_TIME) AS SYS_ENT                    " & vbNewLine _
                              & "							   FROM                                                            " & vbNewLine _
                              & "							   $LM_TRN$..H_INOUTKAEDI_DTL_FJF                                  " & vbNewLine _
                              & "							   WHERE                                                           " & vbNewLine _
                              & "									 NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                              & "								AND  CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                              & "								AND  CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                              & "								AND  ZFVYDENNO = @OUTKA_FROM_ORD_NO                            " & vbNewLine _
                              & "                               AND  INOUT_KB     = '1'                                        " & vbNewLine _
                              & "								AND  DEL_KB IN('0','3')                                        " & vbNewLine _
                              & "							   GROUP BY                                                        " & vbNewLine _
                              & "							   NRS_BR_CD                                                       " & vbNewLine _
                              & "							   ,CUST_CD_L                                                      " & vbNewLine _
                              & "							   ,CUST_CD_M                                                      " & vbNewLine _
                              & "							   ,ZFVYDENNO                                                      " & vbNewLine _
                              & "							   ,ZFVYMEINO                                                      " & vbNewLine _
                              & "							   ) ENT_DTL                                                       " & vbNewLine _
                              & "						   WHERE                                                               " & vbNewLine _
                              & "								 SUB_DTL.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                              & "							AND  SUB_DTL.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                              & "							AND  SUB_DTL.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
                              & "							AND  SUB_DTL.ZFVYDENNO = @OUTKA_FROM_ORD_NO                        " & vbNewLine _
                              & "							AND  ENT_DTL.NRS_BR_CD = SUB_DTL.NRS_BR_CD                         " & vbNewLine _
                              & "							AND  ENT_DTL.CUST_CD_L = SUB_DTL.CUST_CD_L                         " & vbNewLine _
                              & "							AND  ENT_DTL.CUST_CD_M = SUB_DTL.CUST_CD_M                         " & vbNewLine _
                              & "							AND  ENT_DTL.ZFVYDENNO = SUB_DTL.ZFVYDENNO                         " & vbNewLine _
                              & "							AND  ENT_DTL.ZFVYMEINO = SUB_DTL.ZFVYMEINO                         " & vbNewLine _
                              & "							AND  ENT_DTL.SYS_ENT = SUB_DTL.SYS_ENT_DATE + SUB_DTL.SYS_ENT_TIME " & vbNewLine _
                              & "							AND  SUB_DTL.DEL_KB IN('0','3')                                    " & vbNewLine _
                              & "							AND  SUB_DTL.JISSEKI_SHORI_FLG IN('2','3')                         " & vbNewLine _
                              & "                           AND  SUB_DTL.INOUT_KB     = '1'                                    " & vbNewLine _
                              & "							GROUP BY                                                           " & vbNewLine _
                              & "							SUB_DTL.NRS_BR_CD                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.CUST_CD_L                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.CUST_CD_M                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.ZFVYDENNO                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.ZFVYMEINO                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.CRT_DATE                                                   " & vbNewLine _
                              & "						   ,SUB_DTL.FILE_NAME                                                  " & vbNewLine _
                              & "						   ,SUB_DTL.REC_NO                                                     " & vbNewLine _
                              & "						   ,SUB_DTL.GYO                                                        " & vbNewLine _
                              & "						   ,SUB_DTL.EDI_CTL_NO                                                 " & vbNewLine _
                              & "						   ,SUB_DTL.EDI_CTL_NO_CHU                                             " & vbNewLine _
                              & "						   ,SUB_DTL.INKA_CTL_NO_L                                              " & vbNewLine _
                              & "						   ,SUB_DTL.INKA_CTL_NO_M                                              " & vbNewLine _
                              & "						   ,SUB_DTL.INOUT_KB                                                   " & vbNewLine _
                              & "						   ,SUB_DTL.JISSEKI_SHORI_FLG                                          " & vbNewLine _
                              & "						   )  DTLFJF                                                           " & vbNewLine _
                              & "                       ON                                                    " & vbNewLine _
                              & "                             INFJF.NRS_BR_CD     = DTLFJF.NRS_BR_CD          " & vbNewLine _
                              & "                        AND  INFJF.CUST_CD_L     = DTLFJF.CUST_CD_L          " & vbNewLine _
                              & "                        AND  INFJF.CUST_CD_M     = DTLFJF.CUST_CD_M          " & vbNewLine _
                              & "                        AND  INFJF.ZFVYDENNO     = DTLFJF.ZFVYDENNO          " & vbNewLine _
                              & "                        AND  INFJF.ZFVYMEINO     = DTLFJF.ZFVYMEINO          " & vbNewLine _
                              & "                        AND  DTLFJF.JISSEKI_SHORI_FLG IN('2','3')            " & vbNewLine _
                              & "                        AND  DTLFJF.INOUT_KB     = '1'                       " & vbNewLine _
                              & "                       WHERE INFJF.SYS_DEL_FLG <> '1'                        " & vbNewLine _
                              & "                        AND  INFJF.NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                              & "                        AND  INFJF.CUST_CD_L = @CUST_CD_L                    " & vbNewLine _
                              & "                        AND  INFJF.CUST_CD_M = @CUST_CD_M                    " & vbNewLine _
                              & "                        AND  INFJF.ZFVYDENNO = @OUTKA_FROM_ORD_NO            " & vbNewLine _
                              & "                        AND  INFJF.ZFVYCANKB = '0'                           " & vbNewLine _
                              & "                        AND  INFJF.JISSEKI_SHORI_FLG IN('2','3')             " & vbNewLine _
                              & "--                      --  AND  INFJF.LATEST_HOUKOKU_FLG = '1'              " & vbNewLine _
                              & "                        AND  DTLFJF.CRT_DATE IS NOT NULL                     " & vbNewLine _
                      ' & "     INFJF.DEL_KB                   " & vbNewLine _
    '& "    ,DTLFJF.CRT_DATE                 " & vbNewLine _
    '& "    ,DTLFJF.FILE_NAME                " & vbNewLine _
    '& "    ,DTLFJF.REC_NO                   " & vbNewLine _
    '& "    ,DTLFJF.GYO                      " & vbNewLine _
    '& "    ,''                      AS EDA  " & vbNewLine _
    '& "    ,DTLFJF.NRS_BR_CD                " & vbNewLine _
    '& "    ,DTLFJF.EDI_CTL_NO               " & vbNewLine _
    '& "    ,DTLFJF.EDI_CTL_NO_CHU           " & vbNewLine _
    '& "    ,DTLFJF.INKA_CTL_NO_L    AS INKA_CTL_NO_L            " & vbNewLine _
    '& "    ,DTLFJF.INKA_CTL_NO_M    AS INKA_CTL_NO_M            " & vbNewLine _
    '& "    ,''                 AS INKA_CTL_NO_S            " & vbNewLine _
    '& "    ,INFJF.CUST_CD_L                " & vbNewLine _
    '& "    ,INFJF.CUST_CD_M                " & vbNewLine _
    '& "    ,'1'                AS ZFVYCANKB                " & vbNewLine _
    '& "    ,INFJF.FILE_ID                  " & vbNewLine _
    '& "    ,INFJF.ZFVYKAISYAC              " & vbNewLine _
    '& "    ,INFJF.ZFVYDENNO                " & vbNewLine _
    '& "    ,INFJF.ZFVYMEINO                " & vbNewLine _
    '& "    ,INFJF.MATNR                    " & vbNewLine _
    '& "    ,INFJF.CHARG                    " & vbNewLine _
    '& "    ,INFJF.CAN_NO                   " & vbNewLine _
    '& "    ,INFJF.ZFVYDENTYP               " & vbNewLine _
    '& "    ,INFJF.ZFVYWADAT                " & vbNewLine _
    '& "    ,INFJF.ZFVYKWERKS               " & vbNewLine _
    '& "    ,INFJF.LGORT                    " & vbNewLine _
    '& "    ,INFJF.SEIZOU_DATE              " & vbNewLine _
    '& "    ,INFJF.HOSHOU_KIGEN             " & vbNewLine _
    '& "    ,INFJF.SERIAL_NO                " & vbNewLine _
    '& "    ,INFJF.LABEL_SEQ                " & vbNewLine _
    '& "    ,INFJF.ZFVYSURYO                " & vbNewLine _
    '& "    ,INFJF.RECORD_STATUS            " & vbNewLine _
    '& "    ,INFJF.CANI_HOUKOKU_FLG         " & vbNewLine _
    '& "    ,'2'                  AS JISSEKI_SHORI_FLG        " & vbNewLine _
    '& "    ,'0'                  AS LATEST_HOUKOKU_FLG       " & vbNewLine _
    '& "    ,INFJF.JISSEKI_USER             " & vbNewLine _
    '& "    ,INFJF.JISSEKI_DATE             " & vbNewLine _
    '& "    ,INFJF.JISSEKI_TIME             " & vbNewLine _
    '& "    ,INFJF.SEND_USER                " & vbNewLine _
    '& "    ,INFJF.SEND_DATE                " & vbNewLine _
    '& "    ,INFJF.SEND_TIME                " & vbNewLine _
    '& "    ,INFJF.CANI_HOUKOKU_SHORI_FLG   " & vbNewLine _
    '& "    ,INFJF.CANI_HOUKOKU_USER        " & vbNewLine _
    '& "    ,INFJF.CANI_HOUKOKU_DATE        " & vbNewLine _
    '& "    ,INFJF.CANI_HOUKOKU_TIME        " & vbNewLine _
    '& "    ,INFJF.SYS_DEL_FLG              " & vbNewLine _
    '& " FROM $LM_TRN$..H_SENDINEDI_FJF  INFJF         " & vbNewLine _
    '& " LEFT JOIN                                     " & vbNewLine _
    '& " $LM_TRN$..H_INOUTKAEDI_DTL_FJF  DTLFJF        " & vbNewLine _
    '& " ON                                            " & vbNewLine _
    '& "       INFJF.NRS_BR_CD     = DTLFJF.NRS_BR_CD  " & vbNewLine _
    '& "  AND  INFJF.CUST_CD_L     = DTLFJF.CUST_CD_L  " & vbNewLine _
    '& "  AND  INFJF.CUST_CD_M     = DTLFJF.CUST_CD_M  " & vbNewLine _
    '& "  AND  INFJF.ZFVYDENNO     = DTLFJF.ZFVYDENNO  " & vbNewLine _
    '& "  AND  INFJF.ZFVYMEINO     = DTLFJF.ZFVYMEINO  " & vbNewLine _
    '& "  AND  DTLFJF.JISSEKI_SHORI_FLG = '3'          " & vbNewLine _
    '& "  AND  DTLFJF.INOUT_KB     = '1'               " & vbNewLine _
    '& " WHERE INFJF.SYS_DEL_FLG <> '1'                " & vbNewLine _
    '& "  AND  INFJF.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
    '& "  AND  INFJF.CUST_CD_L = @CUST_CD_L            " & vbNewLine _
    '& "  AND  INFJF.CUST_CD_M = @CUST_CD_M            " & vbNewLine _
    '& "  AND  INFJF.ZFVYDENNO = @OUTKA_FROM_ORD_NO    " & vbNewLine _
    '& "  AND  INFJF.ZFVYCANKB = '0'                   " & vbNewLine _
    '& "  AND  INFJF.JISSEKI_SHORI_FLG = '3'          " & vbNewLine _
    '& "  AND  INFJF.LATEST_HOUKOKU_FLG = '1'          " & vbNewLine _
    '& "  AND  DTLFJF.CRT_DATE IS NOT NULL             " & vbNewLine


#End Region

#Region "作業工程進捗情報取得"

    Private Const SQL_SELECT_Canister_SHOHIN As String = _
                                          " SELECT                                              " & vbNewLine _
                                        & "      COUNT(*)  AS ROW_CNT                           " & vbNewLine _
                                        & " FROM                                                " & vbNewLine _
                                        & "     $LM_TRN$..M_SAGYO_STATE_FJF MS                  " & vbNewLine _
                                        & " WHERE                                               " & vbNewLine _
                                        & "     MS.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                        & "     AND MS.MATNR = @GOODS_CD_CUST                   " & vbNewLine _
                                        & "     AND MS.CHARG = @LOT_NO                          " & vbNewLine _
                                        & "     AND MS.WIT_KENCHK_FLG = '1'                     " & vbNewLine

#End Region

#Region "M_HINMOKU_FJF(富士フイルム品目マスタ)"

    ''' <summary>
    ''' M_HINMOKU_FJF(富士フイルム品目マスタ)
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_SELECT_M_HINMOKU_FJF As String = " SELECT                              " & vbNewLine _
                                 & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                 & " FROM                                                  " & vbNewLine _
                                 & " $LM_TRN$..M_HINMOKU_FJF                  M_HINMOKU_FJF    " & vbNewLine _
                                 & " WHERE                                                 " & vbNewLine _
                                 & " M_HINMOKU_FJF.NRS_BR_CD   = @NRS_BR_CD                  " & vbNewLine _
                                 & " AND                                                   " & vbNewLine _
                                 & " M_HINMOKU_FJF.HINMOKU_CD  = @CUST_GOODS_CD             " & vbNewLine _
                                 & " AND                                                   " & vbNewLine _
                                 & " M_HINMOKU_FJF.M_GOODS_UPD_FLG  = '00'                   " & vbNewLine

#End Region

#Region "SELECT_INKA_L"
    Private Const SQL_SELECT_INKA_L As String = " SELECT                                       " & vbNewLine _
                                     & "  COUNT(INKA_NO_L)                  AS INKA_NO_L   " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_TRN$..B_INKA_L                     B_INKA_L       " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " B_INKA_L.SYS_DEL_FLG    = '0'                         " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.OUTKA_FROM_ORD_NO_L = @OUTKA_FROM_ORD_NO     " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.NRS_BR_CD         = @NRS_BR_CD               " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.CUST_CD_L         = @CUST_CD_L               " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.CUST_CD_M         = @CUST_CD_M               " & vbNewLine





#End Region

#End Region

#Region "UPDATE処理"

#Region "INKAEDI_L(入荷登録)"
    ''' <summary>
    ''' EDI入荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_INKATOROKU_EDI_L As String = " UPDATE                                   " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_L                                 " & vbNewLine _
                                        & " SET                                                   " & vbNewLine _
                                        & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                        & ",INKA_CTL_NO_L = @INKA_CTL_NO_L                        " & vbNewLine _
                                        & ",INKA_TP = @INKA_TP                                    " & vbNewLine _
                                        & ",INKA_KB = @INKA_KB                                    " & vbNewLine _
                                        & ",INKA_STATE_KB = @INKA_STATE_KB                        " & vbNewLine _
                                        & ",INKA_DATE = @INKA_DATE                                " & vbNewLine _
                                        & ",INKA_TIME = @INKA_TIME                                " & vbNewLine _
                                        & ",NRS_WH_CD = @NRS_WH_CD                                " & vbNewLine _
                                        & ",CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                                        & ",CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
                                        & ",CUST_NM_L = @CUST_NM_L                                " & vbNewLine _
                                        & ",CUST_NM_M = @CUST_NM_M                                " & vbNewLine _
                                        & ",INKA_PLAN_QT = @INKA_PLAN_QT                          " & vbNewLine _
                                        & ",INKA_PLAN_QT_UT = @INKA_PLAN_QT_UT                    " & vbNewLine _
                                        & ",INKA_TTL_NB = @INKA_TTL_NB                            " & vbNewLine _
                                        & ",NAIGAI_KB = @NAIGAI_KB                                " & vbNewLine _
                                        & ",BUYER_ORD_NO = @BUYER_ORD_NO                          " & vbNewLine _
                                        & ",OUTKA_FROM_ORD_NO = @OUTKA_FROM_ORD_NO                " & vbNewLine _
                                        & ",SEIQTO_CD = @SEIQTO_CD                                " & vbNewLine _
                                        & ",TOUKI_HOKAN_YN = @TOUKI_HOKAN_YN                      " & vbNewLine _
                                        & ",HOKAN_YN = @HOKAN_YN                                  " & vbNewLine _
                                        & ",HOKAN_FREE_KIKAN = @HOKAN_FREE_KIKAN                  " & vbNewLine _
                                        & ",HOKAN_STR_DATE = @HOKAN_STR_DATE                      " & vbNewLine _
                                        & ",NIYAKU_YN = @NIYAKU_YN                                " & vbNewLine _
                                        & ",TAX_KB = @TAX_KB                                      " & vbNewLine _
                                        & ",REMARK = @REMARK                                      " & vbNewLine _
                                        & ",NYUBAN_L = @NYUBAN_L                                  " & vbNewLine _
                                        & ",UNCHIN_TP = @UNCHIN_TP                                " & vbNewLine _
                                        & ",UNCHIN_KB = @UNCHIN_KB                                " & vbNewLine _
                                        & ",OUTKA_MOTO = @OUTKA_MOTO                              " & vbNewLine _
                                        & ",SYARYO_KB = @SYARYO_KB                                " & vbNewLine _
                                        & ",UNSO_ONDO_KB = @UNSO_ONDO_KB                          " & vbNewLine _
                                        & ",UNSO_CD = @UNSO_CD                                    " & vbNewLine _
                                        & ",UNSO_BR_CD = @UNSO_BR_CD                              " & vbNewLine _
                                        & ",UNCHIN = @UNCHIN                                      " & vbNewLine _
                                        & ",YOKO_TARIFF_CD = @YOKO_TARIFF_CD                      " & vbNewLine _
                                        & ",OUT_FLAG = @OUT_FLAG                                  " & vbNewLine _
                                        & ",AKAKURO_KB = @AKAKURO_KB                              " & vbNewLine _
                                        & ",JISSEKI_FLAG = @JISSEKI_FLAG                          " & vbNewLine _
                                        & ",JISSEKI_USER = @JISSEKI_USER                          " & vbNewLine _
                                        & ",JISSEKI_DATE = @JISSEKI_DATE                          " & vbNewLine _
                                        & ",JISSEKI_TIME = @JISSEKI_TIME                          " & vbNewLine _
                                        & ",FREE_N01 = @FREE_N01                                  " & vbNewLine _
                                        & ",FREE_N02 = @FREE_N02                                  " & vbNewLine _
                                        & ",FREE_N03 = @FREE_N03                                  " & vbNewLine _
                                        & ",FREE_N04 = @FREE_N04                                  " & vbNewLine _
                                        & ",FREE_N05 = @FREE_N05                                  " & vbNewLine _
                                        & ",FREE_N07 = @FREE_N07                                  " & vbNewLine _
                                        & ",FREE_N08 = @FREE_N08                                  " & vbNewLine _
                                        & ",FREE_N09 = @FREE_N09                                  " & vbNewLine _
                                        & ",FREE_N10 = @FREE_N10                                  " & vbNewLine _
                                        & ",FREE_C01 = @FREE_C01                                  " & vbNewLine _
                                        & ",FREE_C02 = @FREE_C02                                  " & vbNewLine _
                                        & ",FREE_C03 = @FREE_C03                                  " & vbNewLine _
                                        & ",FREE_C04 = @FREE_C04                                  " & vbNewLine _
                                        & ",FREE_C05 = @FREE_C05                                  " & vbNewLine _
                                        & ",FREE_C06 = @FREE_C06                                  " & vbNewLine _
                                        & ",FREE_C07 = @FREE_C07                                  " & vbNewLine _
                                        & ",FREE_C08 = @FREE_C08                                  " & vbNewLine _
                                        & ",FREE_C09 = @FREE_C09                                  " & vbNewLine _
                                        & ",FREE_C10 = @FREE_C10                                  " & vbNewLine _
                                        & ",FREE_C11 = @FREE_C11                                  " & vbNewLine _
                                        & ",FREE_C12 = @FREE_C12                                  " & vbNewLine _
                                        & ",FREE_C13 = @FREE_C13                                  " & vbNewLine _
                                        & ",FREE_C14 = @FREE_C14                                  " & vbNewLine _
                                        & ",FREE_C15 = @FREE_C15                                  " & vbNewLine _
                                        & ",FREE_C16 = @FREE_C16                                  " & vbNewLine _
                                        & ",FREE_C17 = @FREE_C17                                  " & vbNewLine _
                                        & ",FREE_C18 = @FREE_C18                                  " & vbNewLine _
                                        & ",FREE_C19 = @FREE_C19                                  " & vbNewLine _
                                        & ",FREE_C20 = @FREE_C20                                  " & vbNewLine _
                                        & ",FREE_C21 = @FREE_C21                                  " & vbNewLine _
                                        & ",FREE_C22 = @FREE_C22                                  " & vbNewLine _
                                        & ",FREE_C23 = @FREE_C23                                  " & vbNewLine _
                                        & ",FREE_C24 = @FREE_C24                                  " & vbNewLine _
                                        & ",FREE_C25 = @FREE_C25                                  " & vbNewLine _
                                        & ",FREE_C26 = @FREE_C26                                  " & vbNewLine _
                                        & ",FREE_C27 = @FREE_C27                                  " & vbNewLine _
                                        & ",FREE_C28 = @FREE_C28                                  " & vbNewLine _
                                        & ",FREE_C29 = @FREE_C29                                  " & vbNewLine _
                                        & ",FREE_C30 = @FREE_C30                                  " & vbNewLine _
                                        & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                        & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                        & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                        & ",MATCHING_FLAG = @MATCHING_FLAG                        " & vbNewLine _
                                        & ",EDIT_FLAG = @EDIT_FLAG                                " & vbNewLine _
                                        & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                        & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                        & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                        & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                        & ",SYS_DEL_FLG  = @SYS_DEL_FLG                           " & vbNewLine _
                                        & " WHERE                                                 " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                        & " AND                                                   " & vbNewLine _
                                        & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine _
                                        & " AND                                                   " & vbNewLine _
                                        & " SYS_UPD_DATE = @HAITA_SYS_UPD_DATE                    " & vbNewLine _
                                        & " AND                                                   " & vbNewLine _
                                        & " SYS_UPD_TIME = @HAITA_SYS_UPD_TIME                    " & vbNewLine




#End Region

#Region "INKAEDI_M(入荷登録)"
    ''' <summary>
    ''' EDI入荷(中)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_INKATOROKU_EDI_M As String = " UPDATE                                   " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_M                                 " & vbNewLine _
                                        & " SET                                                   " & vbNewLine _
                                        & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                        & ",NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                        & ",EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine _
                                        & ",INKA_CTL_NO_L = @INKA_CTL_NO_L                        " & vbNewLine _
                                        & ",INKA_CTL_NO_M = @INKA_CTL_NO_M                        " & vbNewLine _
                                        & ",NRS_GOODS_CD = @NRS_GOODS_CD                          " & vbNewLine _
                                        & ",CUST_GOODS_CD = @CUST_GOODS_CD                        " & vbNewLine _
                                        & ",GOODS_NM = @GOODS_NM                                  " & vbNewLine _
                                        & ",NB = @NB                                              " & vbNewLine _
                                        & ",NB_UT = @NB_UT                                        " & vbNewLine _
                                        & ",PKG_NB = @PKG_NB                                      " & vbNewLine _
                                        & ",PKG_UT = @PKG_UT                                      " & vbNewLine _
                                        & ",INKA_PKG_NB = @INKA_PKG_NB                            " & vbNewLine _
                                        & ",HASU = @HASU                                          " & vbNewLine _
                                        & ",STD_IRIME = @STD_IRIME                                " & vbNewLine _
                                        & ",STD_IRIME_UT = @STD_IRIME_UT                          " & vbNewLine _
                                        & ",BETU_WT = @BETU_WT                                    " & vbNewLine _
                                        & ",CBM = @CBM                                            " & vbNewLine _
                                        & ",ONDO_KB = @ONDO_KB                                    " & vbNewLine _
                                        & ",OUTKA_FROM_ORD_NO = @OUTKA_FROM_ORD_NO                " & vbNewLine _
                                        & ",BUYER_ORD_NO = @BUYER_ORD_NO                          " & vbNewLine _
                                        & ",REMARK = @REMARK                                      " & vbNewLine _
                                        & ",LOT_NO = @LOT_NO                                      " & vbNewLine _
                                        & ",SERIAL_NO = @SERIAL_NO                                " & vbNewLine _
                                        & ",IRIME = @IRIME                                        " & vbNewLine _
                                        & ",IRIME_UT = @IRIME_UT                                  " & vbNewLine _
                                        & ",OUT_KB = @OUT_KB                                      " & vbNewLine _
                                        & ",AKAKURO_KB = @AKAKURO_KB                              " & vbNewLine _
                                        & ",JISSEKI_FLAG = @JISSEKI_FLAG                          " & vbNewLine _
                                        & ",JISSEKI_USER = @JISSEKI_USER                          " & vbNewLine _
                                        & ",JISSEKI_DATE = @JISSEKI_DATE                          " & vbNewLine _
                                        & ",JISSEKI_TIME = @JISSEKI_TIME                          " & vbNewLine _
                                        & ",FREE_N01 = @FREE_N01                                  " & vbNewLine _
                                        & ",FREE_N02 = @FREE_N02                                  " & vbNewLine _
                                        & ",FREE_N03 = @FREE_N03                                  " & vbNewLine _
                                        & ",FREE_N04 = @FREE_N04                                  " & vbNewLine _
                                        & ",FREE_N05 = @FREE_N05                                  " & vbNewLine _
                                        & ",FREE_N06 = @FREE_N06                                  " & vbNewLine _
                                        & ",FREE_N07 = @FREE_N07                                  " & vbNewLine _
                                        & ",FREE_N08 = @FREE_N08                                  " & vbNewLine _
                                        & ",FREE_N09 = @FREE_N09                                  " & vbNewLine _
                                        & ",FREE_N10 = @FREE_N10                                  " & vbNewLine _
                                        & ",FREE_C01 = @FREE_C01                                  " & vbNewLine _
                                        & ",FREE_C02 = @FREE_C02                                  " & vbNewLine _
                                        & ",FREE_C03 = @FREE_C03                                  " & vbNewLine _
                                        & ",FREE_C04 = @FREE_C04                                  " & vbNewLine _
                                        & ",FREE_C05 = @FREE_C05                                  " & vbNewLine _
                                        & ",FREE_C06 = @FREE_C06                                  " & vbNewLine _
                                        & ",FREE_C07 = @FREE_C07                                  " & vbNewLine _
                                        & ",FREE_C08 = @FREE_C08                                  " & vbNewLine _
                                        & ",FREE_C09 = @FREE_C09                                  " & vbNewLine _
                                        & ",FREE_C10 = @FREE_C10                                  " & vbNewLine _
                                        & ",FREE_C11 = @FREE_C11                                  " & vbNewLine _
                                        & ",FREE_C12 = @FREE_C12                                  " & vbNewLine _
                                        & ",FREE_C13 = @FREE_C13                                  " & vbNewLine _
                                        & ",FREE_C14 = @FREE_C14                                  " & vbNewLine _
                                        & ",FREE_C15 = @FREE_C15                                  " & vbNewLine _
                                        & ",FREE_C16 = @FREE_C16                                  " & vbNewLine _
                                        & ",FREE_C17 = @FREE_C17                                  " & vbNewLine _
                                        & ",FREE_C18 = @FREE_C18                                  " & vbNewLine _
                                        & ",FREE_C19 = @FREE_C19                                  " & vbNewLine _
                                        & ",FREE_C20 = @FREE_C20                                  " & vbNewLine _
                                        & ",FREE_C21 = @FREE_C21                                  " & vbNewLine _
                                        & ",FREE_C22 = @FREE_C22                                  " & vbNewLine _
                                        & ",FREE_C23 = @FREE_C23                                  " & vbNewLine _
                                        & ",FREE_C24 = @FREE_C24                                  " & vbNewLine _
                                        & ",FREE_C25 = @FREE_C25                                  " & vbNewLine _
                                        & ",FREE_C26 = @FREE_C26                                  " & vbNewLine _
                                        & ",FREE_C27 = @FREE_C27                                  " & vbNewLine _
                                        & ",FREE_C28 = @FREE_C28                                  " & vbNewLine _
                                        & ",FREE_C29 = @FREE_C29                                  " & vbNewLine _
                                        & ",FREE_C30 = @FREE_C30                                  " & vbNewLine _
                                        & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                        & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                        & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                        & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                        & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                        & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                        & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                        & ",SYS_DEL_FLG = @SYS_DEL_FLG                            " & vbNewLine _
                                        & " WHERE                                                 " & vbNewLine _
                                        & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                        & " AND                                                   " & vbNewLine _
                                        & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine _
                                        & " AND                                                   " & vbNewLine _
                                        & " EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU                      " & vbNewLine


#End Region

    '富士フイルム追加箇所 terakawa 2012.08.02 Start
#Region "RCV_HED(入荷登録)"
    ''' <summary>
    ''' 受信ヘッダ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_RCV_HED As String = "UPDATE                                               " & vbNewLine _
                                              & " $LM_TRN$..H_INOUTKAEDI_HED_FJF                    " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L       	        " & vbNewLine _
                                              & ",INKA_USER         = @UPD_USER                     " & vbNewLine _
                                              & ",INKA_DATE         = @UPD_DATE                     " & vbNewLine _
                                              & ",INKA_TIME         = @UPD_TIME                     " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG       = '0'                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " INOUT_KB          = '1'                           " & vbNewLine


#End Region
    '富士フイルム追加箇所 terakawa 2012.08.02 End

#Region "RCV_DTL(入荷登録)"
    ''' <summary>
    ''' 受信明細更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_RCV_DTL As String = "UPDATE                                            " & vbNewLine _
                                              & " $LM_TRN$..H_INOUTKAEDI_DTL_FJF                       " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L    	        " & vbNewLine _
                                              & ",INKA_CTL_NO_M     = @INKA_CTL_NO_M        	    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO_CHU    = @EDI_CTL_NO_CHU               " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG       = '0'                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " INOUT_KB          = '1'                           " & vbNewLine


#End Region

#Region "INKAEDI_L(実績作成)"
    ''' <summary>
    ''' INKAEDI_L(実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKISAKUSEI_EDI_L As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                 " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine


#End Region

#Region "INKAEDI_M(実績作成)"
    ''' <summary>
    ''' INKAEDI_M(実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKISAKUSEI_EDI_M As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_M                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                 " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " OUT_KB            = '0'                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = '0'                           " & vbNewLine

#End Region

#Region "RCV_HED(実績作成)"
    ''' <summary>
    ''' RCV_HED(実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_RCV_HED_FJF As String = "UPDATE                                 " & vbNewLine _
                                              & " $LM_TRN$..H_INOUTKAEDI_HED_FJF                    " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = '2'                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & "--(2014.06.09)FFEM対応 START                         " & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & "--(2014.06.09)FFEM対応 END                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " INOUT_KB          = '1'                           " & vbNewLine

#End Region

#Region "RCV_DTL(実績作成)"

    ''' <summary>
    ''' RCV_DTL(実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPDATE_RCV_DTL_FJF As String = "UPDATE $LM_TRN$..H_INOUTKAEDI_DTL_FJF SET        " & vbNewLine _
                                              & "-- JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = '2'                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG  = '1'                          " & vbNewLine _
                                              & "--(2014.06.09)FFEM対応 START                         " & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & "--(2014.06.09)FFEM対応 END                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " INOUT_KB          = '1'                           " & vbNewLine


#End Region

#Region "INKA_L(実績作成)"
    ''' <summary>
    ''' INKA_L(実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKISAKUSEI_INKA_L As String = "UPDATE                                 " & vbNewLine _
                                              & " $LM_TRN$..B_INKA_L                                " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_STATE_KB     = @INKA_STATE_KB                " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " INKA_NO_L         = @INKA_NO_L                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE = @HAITA_SYS_UPD_DATE                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME = @HAITA_SYS_UPD_TIME                " & vbNewLine
#End Region

#End Region

#Region "INSERT処理"

#Region "INKA_L(入荷登録)"

    Private Const SQL_INSERT_INKA_L As String = "INSERT INTO $LM_TRN$..B_INKA_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",FURI_NO                      " & vbNewLine _
                                              & ",INKA_TP                      " & vbNewLine _
                                              & ",INKA_KB                      " & vbNewLine _
                                              & ",INKA_STATE_KB                " & vbNewLine _
                                              & ",INKA_DATE                    " & vbNewLine _
                                              & ",WH_CD                        " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",INKA_PLAN_QT                 " & vbNewLine _
                                              & ",INKA_PLAN_QT_UT              " & vbNewLine _
                                              & ",INKA_TTL_NB                  " & vbNewLine _
                                              & ",BUYER_ORD_NO_L               " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_L          " & vbNewLine _
                                              & ",SEIQTO_CD                    " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN               " & vbNewLine _
                                              & ",HOKAN_YN                     " & vbNewLine _
                                              & ",HOKAN_FREE_KIKAN             " & vbNewLine _
                                              & ",HOKAN_STR_DATE               " & vbNewLine _
                                              & ",NIYAKU_YN                    " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",CHECKLIST_PRT_DATE           " & vbNewLine _
                                              & ",CHECKLIST_PRT_USER           " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_DATE        " & vbNewLine _
                                              & ",UKETSUKELIST_PRT_USER        " & vbNewLine _
                                              & ",UKETSUKE_DATE                " & vbNewLine _
                                              & ",UKETSUKE_USER                " & vbNewLine _
                                              & ",KEN_DATE                     " & vbNewLine _
                                              & ",KEN_USER                     " & vbNewLine _
                                              & ",INKO_DATE                    " & vbNewLine _
                                              & ",INKO_USER                    " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_DATE           " & vbNewLine _
                                              & ",HOUKOKUSYO_PR_USER           " & vbNewLine _
                                              & ",UNCHIN_TP                    " & vbNewLine _
                                              & ",UNCHIN_KB                    " & vbNewLine _
                                              & ",WH_TAB_STATUS                " & vbNewLine _
                                              & ",WH_TAB_YN                    " & vbNewLine _
                                              & ",WH_TAB_IMP_YN                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@FURI_NO                     " & vbNewLine _
                                              & ",@INKA_TP                     " & vbNewLine _
                                              & ",@INKA_KB                     " & vbNewLine _
                                              & ",@INKA_STATE_KB               " & vbNewLine _
                                              & ",@INKA_DATE                   " & vbNewLine _
                                              & ",@WH_CD                       " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@INKA_PLAN_QT                " & vbNewLine _
                                              & ",@INKA_PLAN_QT_UT             " & vbNewLine _
                                              & ",@INKA_TTL_NB                 " & vbNewLine _
                                              & ",@BUYER_ORD_NO_L              " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_L         " & vbNewLine _
                                              & ",@SEIQTO_CD                   " & vbNewLine _
                                              & ",@TOUKI_HOKAN_YN              " & vbNewLine _
                                              & ",@HOKAN_YN                    " & vbNewLine _
                                              & ",@HOKAN_FREE_KIKAN            " & vbNewLine _
                                              & ",@HOKAN_STR_DATE              " & vbNewLine _
                                              & ",@NIYAKU_YN                   " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",@UNCHIN_TP                   " & vbNewLine _
                                              & ",@UNCHIN_KB                   " & vbNewLine _
                                              & ",@WH_TAB_STATUS               " & vbNewLine _
                                              & ",@WH_TAB_YN                   " & vbNewLine _
                                              & ",@WH_TAB_IMP_YN               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "INKA_M"

    Private Const SQL_INSERT_INKA_M As String = "INSERT INTO $LM_TRN$..B_INKA_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",INKA_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",OUTKA_FROM_ORD_NO_M          " & vbNewLine _
                                              & ",BUYER_ORD_NO_M               " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",PRINT_SORT                   " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@INKA_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@OUTKA_FROM_ORD_NO_M         " & vbNewLine _
                                              & ",@BUYER_ORD_NO_M              " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",99                           " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "INKA_S"

    Private Const SQL_INSERT_INKA_S As String = "INSERT INTO $LM_TRN$..B_INKA_S" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",INKA_NO_L                    " & vbNewLine _
                                              & ",INKA_NO_M                    " & vbNewLine _
                                              & ",INKA_NO_S                    " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",LOCA                         " & vbNewLine _
                                              & ",TOU_NO                       " & vbNewLine _
                                              & ",SITU_NO                      " & vbNewLine _
                                              & ",ZONE_CD                      " & vbNewLine _
                                              & ",KONSU                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SERIAL_NO                    " & vbNewLine _
                                              & ",GOODS_COND_KB_1              " & vbNewLine _
                                              & ",GOODS_COND_KB_2              " & vbNewLine _
                                              & ",GOODS_COND_KB_3              " & vbNewLine _
                                              & ",GOODS_CRT_DATE               " & vbNewLine _
                                              & ",LT_DATE                      " & vbNewLine _
                                              & ",SPD_KB                       " & vbNewLine _
                                              & ",OFB_KB                       " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",ALLOC_PRIORITY               " & vbNewLine _
                                              & ",REMARK_OUT                   " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@INKA_NO_L                   " & vbNewLine _
                                              & ",@INKA_NO_M                   " & vbNewLine _
                                              & ",@INKA_NO_S                   " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",@TOU_NO                      " & vbNewLine _
                                              & ",@SITU_NO                     " & vbNewLine _
                                              & ",@ZONE_CD                     " & vbNewLine _
                                              & ",@KONSU                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SERIAL_NO                   " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",@LT_DATE                     " & vbNewLine _
                                              & ",@SPD_KB                      " & vbNewLine _
                                              & ",@OFB_KB                      " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",''                           " & vbNewLine _
                                              & ",@ALLOC_PRIORITY              " & vbNewLine _
                                              & ",@REMARK_OUT                  " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "ZAI_TRS"

    Private Const SQL_INSERT_ZAI_TRS As String = "INSERT INTO $LM_TRN$..D_ZAI_TRS" & vbNewLine _
                                               & "(                              " & vbNewLine _
                                               & " NRS_BR_CD                     " & vbNewLine _
                                               & ",ZAI_REC_NO                    " & vbNewLine _
                                               & ",WH_CD                         " & vbNewLine _
                                               & ",TOU_NO                        " & vbNewLine _
                                               & ",SITU_NO                       " & vbNewLine _
                                               & ",ZONE_CD                       " & vbNewLine _
                                               & ",LOCA                          " & vbNewLine _
                                               & ",LOT_NO                        " & vbNewLine _
                                               & ",CUST_CD_L                     " & vbNewLine _
                                               & ",CUST_CD_M                     " & vbNewLine _
                                               & ",GOODS_CD_NRS                  " & vbNewLine _
                                               & ",GOODS_KANRI_NO                " & vbNewLine _
                                               & ",INKA_NO_L                     " & vbNewLine _
                                               & ",INKA_NO_M                     " & vbNewLine _
                                               & ",INKA_NO_S                     " & vbNewLine _
                                               & ",ALLOC_PRIORITY                " & vbNewLine _
                                               & ",RSV_NO                        " & vbNewLine _
                                               & ",SERIAL_NO                     " & vbNewLine _
                                               & ",HOKAN_YN                      " & vbNewLine _
                                               & ",TAX_KB                        " & vbNewLine _
                                               & ",GOODS_COND_KB_1               " & vbNewLine _
                                               & ",GOODS_COND_KB_2               " & vbNewLine _
                                               & ",GOODS_COND_KB_3               " & vbNewLine _
                                               & ",OFB_KB                        " & vbNewLine _
                                               & ",SPD_KB                        " & vbNewLine _
                                               & ",REMARK_OUT                    " & vbNewLine _
                                               & ",PORA_ZAI_NB                   " & vbNewLine _
                                               & ",ALCTD_NB                      " & vbNewLine _
                                               & ",ALLOC_CAN_NB                  " & vbNewLine _
                                               & ",IRIME                         " & vbNewLine _
                                               & ",PORA_ZAI_QT                   " & vbNewLine _
                                               & ",ALCTD_QT                      " & vbNewLine _
                                               & ",ALLOC_CAN_QT                  " & vbNewLine _
                                               & ",INKO_DATE                     " & vbNewLine _
                                               & ",INKO_PLAN_DATE                " & vbNewLine _
                                               & ",ZERO_FLAG                     " & vbNewLine _
                                               & ",LT_DATE                       " & vbNewLine _
                                               & ",GOODS_CRT_DATE                " & vbNewLine _
                                               & ",DEST_CD_P                     " & vbNewLine _
                                               & ",REMARK                        " & vbNewLine _
                                               & ",SMPL_FLAG                     " & vbNewLine _
                                               & ",SYS_ENT_DATE                  " & vbNewLine _
                                               & ",SYS_ENT_TIME                  " & vbNewLine _
                                               & ",SYS_ENT_PGID                  " & vbNewLine _
                                               & ",SYS_ENT_USER                  " & vbNewLine _
                                               & ",SYS_UPD_DATE                  " & vbNewLine _
                                               & ",SYS_UPD_TIME                  " & vbNewLine _
                                               & ",SYS_UPD_PGID                  " & vbNewLine _
                                               & ",SYS_UPD_USER                  " & vbNewLine _
                                               & ",SYS_DEL_FLG                   " & vbNewLine _
                                               & " )VALUES(                      " & vbNewLine _
                                               & " @NRS_BR_CD                    " & vbNewLine _
                                               & ",@ZAI_REC_NO                   " & vbNewLine _
                                               & ",@WH_CD                        " & vbNewLine _
                                               & ",@TOU_NO                       " & vbNewLine _
                                               & ",@SITU_NO                      " & vbNewLine _
                                               & ",@ZONE_CD                      " & vbNewLine _
                                               & ",@LOCA                         " & vbNewLine _
                                               & ",@LOT_NO                       " & vbNewLine _
                                               & ",@CUST_CD_L                    " & vbNewLine _
                                               & ",@CUST_CD_M                    " & vbNewLine _
                                               & ",@GOODS_CD_NRS                 " & vbNewLine _
                                               & ",@GOODS_KANRI_NO               " & vbNewLine _
                                               & ",@INKA_NO_L                    " & vbNewLine _
                                               & ",@INKA_NO_M                    " & vbNewLine _
                                               & ",@INKA_NO_S                    " & vbNewLine _
                                               & ",@ALLOC_PRIORITY               " & vbNewLine _
                                               & ",@RSV_NO                       " & vbNewLine _
                                               & ",@SERIAL_NO                    " & vbNewLine _
                                               & ",@HOKAN_YN                     " & vbNewLine _
                                               & ",@TAX_KB                       " & vbNewLine _
                                               & ",@GOODS_COND_KB_1              " & vbNewLine _
                                               & ",@GOODS_COND_KB_2              " & vbNewLine _
                                               & ",@GOODS_COND_KB_3              " & vbNewLine _
                                               & ",@OFB_KB                       " & vbNewLine _
                                               & ",@SPD_KB                       " & vbNewLine _
                                               & ",@REMARK_OUT                   " & vbNewLine _
                                               & ",@PORA_ZAI_NB                  " & vbNewLine _
                                               & ",@ALCTD_NB                     " & vbNewLine _
                                               & ",@ALLOC_CAN_NB                 " & vbNewLine _
                                               & ",@IRIME                        " & vbNewLine _
                                               & ",@PORA_ZAI_QT                  " & vbNewLine _
                                               & ",@ALCTD_QT                     " & vbNewLine _
                                               & ",@ALLOC_CAN_QT                 " & vbNewLine _
                                               & ",@INKO_DATE                    " & vbNewLine _
                                               & ",@INKO_PLAN_DATE               " & vbNewLine _
                                               & ",@ZERO_FLAG                    " & vbNewLine _
                                               & ",@LT_DATE                      " & vbNewLine _
                                               & ",@GOODS_CRT_DATE               " & vbNewLine _
                                               & ",@DEST_CD_P                    " & vbNewLine _
                                               & ",@REMARK                       " & vbNewLine _
                                               & ",@SMPL_FLAG                    " & vbNewLine _
                                               & ",@SYS_ENT_DATE                 " & vbNewLine _
                                               & ",@SYS_ENT_TIME                 " & vbNewLine _
                                               & ",@SYS_ENT_PGID                 " & vbNewLine _
                                               & ",@SYS_ENT_USER                 " & vbNewLine _
                                               & ",@SYS_UPD_DATE                 " & vbNewLine _
                                               & ",@SYS_UPD_TIME                 " & vbNewLine _
                                               & ",@SYS_UPD_PGID                 " & vbNewLine _
                                               & ",@SYS_UPD_USER                 " & vbNewLine _
                                               & ",@SYS_DEL_FLG                  " & vbNewLine _
                                               & ")                              " & vbNewLine

#End Region

#Region "富士フイルム入荷実績情報登録"

    Private Const SQL_INSERT_H_SENDINEDI_FJF As String = _
                    "INSERT                        " & vbNewLine _
                  & "    $LM_TRN$..H_SENDINEDI_FJF (   " & vbNewLine _
                  & "     DEL_KB                   " & vbNewLine _
                  & "    ,CRT_DATE                 " & vbNewLine _
                  & "    ,FILE_NAME                " & vbNewLine _
                  & "    ,REC_NO                   " & vbNewLine _
                  & "    ,GYO                      " & vbNewLine _
                  & "    ,EDA                      " & vbNewLine _
                  & "    ,NRS_BR_CD                " & vbNewLine _
                  & "    ,EDI_CTL_NO               " & vbNewLine _
                  & "    ,EDI_CTL_NO_CHU           " & vbNewLine _
                  & "    ,INKA_CTL_NO_L            " & vbNewLine _
                  & "    ,INKA_CTL_NO_M            " & vbNewLine _
                  & "    ,INKA_CTL_NO_S            " & vbNewLine _
                  & "    ,CUST_CD_L                " & vbNewLine _
                  & "    ,CUST_CD_M                " & vbNewLine _
                  & "    ,ZFVYCANKB                " & vbNewLine _
                  & "    ,FILE_ID                  " & vbNewLine _
                  & "    ,ZFVYKAISYAC              " & vbNewLine _
                  & "    ,ZFVYDENNO                " & vbNewLine _
                  & "    ,ZFVYMEINO                " & vbNewLine _
                  & "    ,MATNR                    " & vbNewLine _
                  & "    ,CHARG                    " & vbNewLine _
                  & "    ,CAN_NO                   " & vbNewLine _
                  & "    ,ZFVYDENTYP               " & vbNewLine _
                  & "    ,ZFVYWADAT                " & vbNewLine _
                  & "    ,ZFVYHWERKS               " & vbNewLine _
                  & "    ,OUT_LGORT                " & vbNewLine _
                  & "    ,ZFVYKWERKS               " & vbNewLine _
                  & "    ,IN_LGORT                 " & vbNewLine _
                  & "    ,SEIZOU_DATE              " & vbNewLine _
                  & "    ,HOSHOU_KIGEN             " & vbNewLine _
                  & "    ,SERIAL_NO                " & vbNewLine _
                  & "    ,LABEL_SEQ                " & vbNewLine _
                  & "    ,ZFVYSURYO                " & vbNewLine _
                  & "    ,RECORD_STATUS            " & vbNewLine _
                  & "    ,CANI_HOUKOKU_FLG         " & vbNewLine _
                  & "    ,JISSEKI_SHORI_FLG        " & vbNewLine _
                  & "    ,LATEST_HOUKOKU_FLG       " & vbNewLine _
                  & "    ,JISSEKI_USER             " & vbNewLine _
                  & "    ,JISSEKI_DATE             " & vbNewLine _
                  & "    ,JISSEKI_TIME             " & vbNewLine _
                  & "    ,SEND_USER                " & vbNewLine _
                  & "    ,SEND_DATE                " & vbNewLine _
                  & "    ,SEND_TIME                " & vbNewLine _
                  & "    ,CANI_HOUKOKU_SHORI_FLG   " & vbNewLine _
                  & "    ,CANI_HOUKOKU_USER        " & vbNewLine _
                  & "    ,CANI_HOUKOKU_DATE        " & vbNewLine _
                  & "    ,CANI_HOUKOKU_TIME        " & vbNewLine _
                  & "    ,DELETE_USER              " & vbNewLine _
                  & "    ,DELETE_DATE              " & vbNewLine _
                  & "    ,DELETE_TIME              " & vbNewLine _
                  & "    ,DELETE_EDI_NO            " & vbNewLine _
                  & "    ,DELETE_EDI_NO_CHU        " & vbNewLine _
                  & "    ,UPD_USER                 " & vbNewLine _
                  & "    ,UPD_DATE                 " & vbNewLine _
                  & "    ,UPD_TIME                 " & vbNewLine _
                  & "    ,SYS_ENT_DATE             " & vbNewLine _
                  & "    ,SYS_ENT_TIME             " & vbNewLine _
                  & "    ,SYS_ENT_PGID             " & vbNewLine _
                  & "    ,SYS_ENT_USER             " & vbNewLine _
                  & "    ,SYS_UPD_DATE             " & vbNewLine _
                  & "    ,SYS_UPD_TIME             " & vbNewLine _
                  & "    ,SYS_UPD_PGID             " & vbNewLine _
                  & "    ,SYS_UPD_USER             " & vbNewLine _
                  & "    ,SYS_DEL_FLG              " & vbNewLine _
                  & ")                             " & vbNewLine _
                  & "VALUES(                       " & vbNewLine _
                  & "     @DEL_KB                   " & vbNewLine _
                  & "    ,@CRT_DATE                 " & vbNewLine _
                  & "    ,@FILE_NAME                " & vbNewLine _
                  & "    ,@REC_NO                   " & vbNewLine _
                  & "    ,@GYO                      " & vbNewLine _
                  & ",ISNULL((SELECT                       " & vbNewLine _
                  & "MAX(CONVERT(Int,SIF.EDA))             " & vbNewLine _
                  & "FROM $LM_TRN$..H_SENDINEDI_FJF AS SIF " & vbNewLine _
                  & "WHERE                                 " & vbNewLine _
                  & "SIF.NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
                  & "AND                                   " & vbNewLine _
                  & "SIF.INKA_CTL_NO_L = @INKA_CTL_NO_L    " & vbNewLine _
                  & "AND                                   " & vbNewLine _
                  & "SIF.INKA_CTL_NO_M = @INKA_CTL_NO_M    " & vbNewLine _
                  & "AND                                   " & vbNewLine _
                  & "SIF.SYS_DEL_FLG = '0'                 " & vbNewLine _
                  & "),0) + 1                       " & vbNewLine _
                  & "    ,@NRS_BR_CD                " & vbNewLine _
                  & "    ,@EDI_CTL_NO               " & vbNewLine _
                  & "    ,@EDI_CTL_NO_CHU           " & vbNewLine _
                  & "    ,@INKA_CTL_NO_L            " & vbNewLine _
                  & "    ,@INKA_CTL_NO_M            " & vbNewLine _
                  & "    ,@INKA_CTL_NO_S            " & vbNewLine _
                  & "    ,@CUST_CD_L                " & vbNewLine _
                  & "    ,@CUST_CD_M                " & vbNewLine _
                  & "    ,@ZFVYCANKB                " & vbNewLine _
                  & "    ,@FILE_ID                  " & vbNewLine _
                  & "    ,@ZFVYKAISYAC              " & vbNewLine _
                  & "    ,@ZFVYDENNO                " & vbNewLine _
                  & "    ,@ZFVYMEINO                " & vbNewLine _
                  & "    ,@MATNR                    " & vbNewLine _
                  & "    ,@CHARG                    " & vbNewLine _
                  & "    ,@CAN_NO                   " & vbNewLine _
                  & "    ,@ZFVYDENTYP               " & vbNewLine _
                  & "    ,@ZFVYWADAT                " & vbNewLine _
                  & "    ,@ZFVYHWERKS               " & vbNewLine _
                  & "    ,@OUT_LGORT                " & vbNewLine _
                  & "    ,@ZFVYKWERKS               " & vbNewLine _
                  & "    ,@IN_LGORT                 " & vbNewLine _
                  & "    ,@SEIZOU_DATE              " & vbNewLine _
                  & "    ,@HOSHOU_KIGEN             " & vbNewLine _
                  & "    ,@SERIAL_NO                " & vbNewLine _
                  & "    ,@LABEL_SEQ                " & vbNewLine _
                  & "    ,@ZFVYSURYO                " & vbNewLine _
                  & "    ,@RECORD_STATUS            " & vbNewLine _
                  & "    ,@CANI_HOUKOKU_FLG         " & vbNewLine _
                  & "    ,@JISSEKI_SHORI_FLG        " & vbNewLine _
                  & "    ,@LATEST_HOUKOKU_FLG       " & vbNewLine _
                  & "    ,@JISSEKI_USER             " & vbNewLine _
                  & "    ,@JISSEKI_DATE             " & vbNewLine _
                  & "    ,@JISSEKI_TIME             " & vbNewLine _
                  & "    ,@SEND_USER                " & vbNewLine _
                  & "    ,@SEND_DATE                " & vbNewLine _
                  & "    ,@SEND_TIME                " & vbNewLine _
                  & "    ,@CANI_HOUKOKU_SHORI_FLG   " & vbNewLine _
                  & "    ,@CANI_HOUKOKU_USER        " & vbNewLine _
                  & "    ,@CANI_HOUKOKU_DATE        " & vbNewLine _
                  & "    ,@CANI_HOUKOKU_TIME        " & vbNewLine _
                  & "    ,@DELETE_USER              " & vbNewLine _
                  & "    ,@DELETE_DATE              " & vbNewLine _
                  & "    ,@DELETE_TIME              " & vbNewLine _
                  & "    ,@DELETE_EDI_NO            " & vbNewLine _
                  & "    ,@DELETE_EDI_NO_CHU        " & vbNewLine _
                  & "    ,@UPD_USER                 " & vbNewLine _
                  & "    ,@UPD_DATE                 " & vbNewLine _
                  & "    ,@UPD_TIME                 " & vbNewLine _
                  & "    ,@SYS_ENT_DATE             " & vbNewLine _
                  & "    ,@SYS_ENT_TIME             " & vbNewLine _
                  & "    ,@SYS_ENT_PGID             " & vbNewLine _
                  & "    ,@SYS_ENT_USER             " & vbNewLine _
                  & "    ,@SYS_UPD_DATE             " & vbNewLine _
                  & "    ,@SYS_UPD_TIME             " & vbNewLine _
                  & "    ,@SYS_UPD_PGID             " & vbNewLine _
                  & "    ,@SYS_UPD_USER             " & vbNewLine _
                  & "    ,'0'                      " & vbNewLine _
                  & ")                             " & vbNewLine

#End Region

#Region "SAGYO"

    ''' <summary>
    ''' INSERT（SAGYO）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO                                          " & vbNewLine _
                                         & "$LM_TRN$..E_SAGYO                                        " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",SAGYO_REC_NO                                            " & vbNewLine _
                                         & ",SAGYO_COMP                                              " & vbNewLine _
                                         & ",SKYU_CHK                                                " & vbNewLine _
                                         & ",SAGYO_SIJI_NO                                           " & vbNewLine _
                                         & ",INOUTKA_NO_LM                                           " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",IOZS_KB                                                 " & vbNewLine _
                                         & ",SAGYO_CD                                                " & vbNewLine _
                                         & ",SAGYO_NM                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",GOODS_NM_NRS                                            " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",INV_TANI                                                " & vbNewLine _
                                         & ",SAGYO_NB                                                " & vbNewLine _
                                         & ",SAGYO_UP                                                " & vbNewLine _
                                         & ",SAGYO_GK                                                " & vbNewLine _
                                         & ",TAX_KB                                                  " & vbNewLine _
                                         & ",SEIQTO_CD                                               " & vbNewLine _
                                         & ",REMARK_ZAI                                              " & vbNewLine _
                                         & ",REMARK_SKYU                                             " & vbNewLine _
                                         & ",REMARK_SIJI                                             " & vbNewLine _
                                         & ",SAGYO_COMP_CD                                           " & vbNewLine _
                                         & ",SAGYO_COMP_DATE                                         " & vbNewLine _
                                         & ",DEST_SAGYO_FLG                                          " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")SELECT                                                  " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@SAGYO_REC_NO                                           " & vbNewLine _
                                         & ",@SAGYO_COMP                                             " & vbNewLine _
                                         & ",@SKYU_CHK                                               " & vbNewLine _
                                         & ",@SAGYO_SIJI_NO                                          " & vbNewLine _
                                         & ",@INOUTKA_NO_LM                                          " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@IOZS_KB                                                " & vbNewLine _
                                         & ",@SAGYO_CD                                               " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_NM                                        " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@GOODS_NM_NRS                                           " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",M_SAGYO.INV_TANI                                        " & vbNewLine _
                                         & ",@SAGYO_NB                                               " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_UP                                        " & vbNewLine _
                                         & ",@SAGYO_GK                                               " & vbNewLine _
                                         & ",M_SAGYO.ZEI_KBN                                         " & vbNewLine _
                                         & ",M_CUST.SAGYO_SEIQTO_CD                                  " & vbNewLine _
                                         & ",M_SAGYO.SAGYO_REMARK                                    " & vbNewLine _
                                         & ",@REMARK_SKYU                                            " & vbNewLine _
                                         & ",M_SAGYO.WH_SAGYO_REMARK                                 " & vbNewLine _
                                         & ",@SAGYO_COMP_CD                                          " & vbNewLine _
                                         & ",@SAGYO_COMP_DATE                                        " & vbNewLine _
                                         & ",@DEST_SAGYO_FLG                                         " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & "FROM                                                     " & vbNewLine _
                                         & "$LM_MST$..M_SAGYO   M_SAGYO                              " & vbNewLine _
                                         & ",$LM_MST$..M_GOODS  M_GOODS                              " & vbNewLine _
                                         & "LEFT JOIN                                                " & vbNewLine _
                                         & "$LM_MST$..M_CUST  M_CUST                                 " & vbNewLine _
                                         & "ON                                                       " & vbNewLine _
                                         & "M_GOODS.NRS_BR_CD  = M_CUST.NRS_BR_CD                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_L  = M_CUST.CUST_CD_L                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_M  = M_CUST.CUST_CD_M                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_S  = M_CUST.CUST_CD_S                    " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.CUST_CD_SS  = M_CUST.CUST_CD_SS                  " & vbNewLine _
                                         & "WHERE                                                    " & vbNewLine _
                                         & "M_SAGYO.SAGYO_CD  = @SAGYO_CD                            " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.NRS_BR_CD  = @NRS_BR_CD                          " & vbNewLine _
                                         & "AND                                                      " & vbNewLine _
                                         & "M_GOODS.GOODS_CD_NRS  = @GOODS_CD_NRS                    " & vbNewLine

#End Region

#Region "UNSO_L"

    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $LM_TRN$..F_UNSO_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",YUSO_BR_CD                   " & vbNewLine _
                                              & ",INOUTKA_NO_L                 " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",JIYU_KB                      " & vbNewLine _
                                              & ",DENP_NO                      " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",ARR_ACT_TIME                 " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_REF_NO                  " & vbNewLine _
                                              & ",SHIP_CD                      " & vbNewLine _
                                              & ",ORIG_CD                      " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB             " & vbNewLine _
                                              & ",VCLE_KB                      " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD               " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD              " & vbNewLine _
                                              & ",AD_3                         " & vbNewLine _
                                              & ",UNSO_TEHAI_KB                " & vbNewLine _
                                              & ",BUY_CHU_NO                   " & vbNewLine _
                                              & ",AREA_CD                      " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG             " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD              " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD              " & vbNewLine _
                                              & ",TRIP_NO_SYUKA                " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI               " & vbNewLine _
                                              & ",TRIP_NO_HAIKA                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & "--'START UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD           " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD          " & vbNewLine _
                                              & "--'END   UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & " )SELECT                      " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@YUSO_BR_CD                  " & vbNewLine _
                                              & ",@INOUTKA_NO_L                " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@JIYU_KB                     " & vbNewLine _
                                              & ",@DENP_NO                     " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@OUTKA_PLAN_TIME             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@ARR_ACT_TIME                " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_REF_NO                 " & vbNewLine _
                                              & ",@SHIP_CD                     " & vbNewLine _
                                              & ",@ORIG_CD                     " & vbNewLine _
                                              & ",M_SOKO.SOKO_DEST_CD          " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",@VCLE_KB                     " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD              " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD             " & vbNewLine _
                                              & ",@AD_3                        " & vbNewLine _
                                              & ",@UNSO_TEHAI_KB               " & vbNewLine _
                                              & ",@BUY_CHU_NO                  " & vbNewLine _
                                              & ",@AREA_CD                     " & vbNewLine _
                                              & ",@TYUKEI_HAISO_FLG            " & vbNewLine _
                                              & ",@SYUKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@HAIKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@TRIP_NO_SYUKA               " & vbNewLine _
                                              & ",@TRIP_NO_TYUKEI              " & vbNewLine _
                                              & ",@TRIP_NO_HAIKA               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & "--'START UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD          " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD         " & vbNewLine _
                                              & "--'END   UMANO 要望番号1302 支払運賃に伴う修正" & vbNewLine _
                                              & "FROM                          " & vbNewLine _
                                              & "$LM_MST$..M_SOKO   M_SOKO     " & vbNewLine _
                                              & ",$LM_MST$..M_CUST  M_CUST     " & vbNewLine _
                                              & "WHERE                                     " & vbNewLine _
                                              & "M_SOKO.NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_SOKO.WH_CD  = @NRS_WH_CD                " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_L  = @CUST_CD_L            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_M  = @CUST_CD_M            " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_S  = '00'                  " & vbNewLine _
                                              & "AND                                       " & vbNewLine _
                                              & "M_CUST.CUST_CD_SS  = '00'                 " & vbNewLine

#End Region

#Region "UNSO_M"

    Private Const SQL_INSERT_UNSO_M As String = "INSERT INTO $LM_TRN$..F_UNSO_M" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",UNSO_NO_M                    " & vbNewLine _
                                              & ",GOODS_CD_NRS                 " & vbNewLine _
                                              & ",GOODS_NM                     " & vbNewLine _
                                              & ",UNSO_TTL_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_TTL_QT                  " & vbNewLine _
                                              & ",QT_UT                        " & vbNewLine _
                                              & ",HASU                         " & vbNewLine _
                                              & ",ZAI_REC_NO                   " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",IRIME                        " & vbNewLine _
                                              & ",IRIME_UT                     " & vbNewLine _
                                              & ",BETU_WT                      " & vbNewLine _
                                              & ",SIZE_KB                      " & vbNewLine _
                                              & ",ZBUKA_CD                     " & vbNewLine _
                                              & ",ABUKA_CD                     " & vbNewLine _
                                              & ",PKG_NB                       " & vbNewLine _
                                              & ",LOT_NO                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@UNSO_NO_M                   " & vbNewLine _
                                              & ",@GOODS_CD_NRS                " & vbNewLine _
                                              & ",@GOODS_NM                    " & vbNewLine _
                                              & ",@UNSO_TTL_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_TTL_QT                 " & vbNewLine _
                                              & ",@QT_UT                       " & vbNewLine _
                                              & ",@HASU                        " & vbNewLine _
                                              & ",@ZAI_REC_NO                  " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@IRIME                       " & vbNewLine _
                                              & ",@IRIME_UT                    " & vbNewLine _
                                              & ",@BETU_WT                     " & vbNewLine _
                                              & ",@SIZE_KB                     " & vbNewLine _
                                              & ",@ZBUKA_CD                    " & vbNewLine _
                                              & ",@ABUKA_CD                    " & vbNewLine _
                                              & ",@PKG_NB                      " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_INSERT_UNCHIN As String = "INSERT INTO $LM_TRN$..F_UNCHIN_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SEIQ_GROUP_NO                    " & vbNewLine _
                                              & ",SEIQ_GROUP_NO_M                  " & vbNewLine _
                                              & ",SEIQTO_CD                        " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SEIQ_SYARYO_KB                   " & vbNewLine _
                                              & ",SEIQ_PKG_UT                      " & vbNewLine _
                                              & ",SEIQ_NG_NB                       " & vbNewLine _
                                              & ",SEIQ_DANGER_KB                   " & vbNewLine _
                                              & ",SEIQ_TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD                   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD                  " & vbNewLine _
                                              & ",SEIQ_KYORI                       " & vbNewLine _
                                              & ",SEIQ_WT                          " & vbNewLine _
                                              & ",SEIQ_UNCHIN                      " & vbNewLine _
                                              & ",SEIQ_CITY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_WINT_EXTC                   " & vbNewLine _
                                              & ",SEIQ_RELY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_TOLL                        " & vbNewLine _
                                              & ",SEIQ_INSU                        " & vbNewLine _
                                              & ",SEIQ_FIXED_FLAG                  " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO                   " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO_M                 " & vbNewLine _
                                              & ",@SEIQTO_CD                       " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SEIQ_SYARYO_KB                  " & vbNewLine _
                                              & ",@SEIQ_PKG_UT                     " & vbNewLine _
                                              & ",@SEIQ_NG_NB                      " & vbNewLine _
                                              & ",@SEIQ_DANGER_KB                  " & vbNewLine _
                                              & ",@SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD                  " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD                 " & vbNewLine _
                                              & ",@SEIQ_KYORI                      " & vbNewLine _
                                              & ",@SEIQ_WT                         " & vbNewLine _
                                              & ",@SEIQ_UNCHIN                     " & vbNewLine _
                                              & ",@SEIQ_CITY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_WINT_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_RELY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_TOLL                       " & vbNewLine _
                                              & ",@SEIQ_INSU                       " & vbNewLine _
                                              & ",@SEIQ_FIXED_FLAG                 " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

#End Region

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

#Region "EDI(大)データ取得処理"
    ''' <summary>
    ''' EDI(大)データ取得処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC103.SQL_SELECT_EDI_L)

        Call Me.SetPrmEdiL()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "SelectEdiL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_TP", "INKA_TP")
        map.Add("INKA_KB", "INKA_KB")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_TIME", "INKA_TIME")
        map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("INKA_PLAN_QT", "INKA_PLAN_QT")
        map.Add("INKA_PLAN_QT_UT", "INKA_PLAN_QT_UT")
        map.Add("INKA_TTL_NB", "INKA_TTL_NB")
        map.Add("NAIGAI_KB", "NAIGAI_KB")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("TOUKI_HOKAN_YN", "TOUKI_HOKAN_YN")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("HOKAN_STR_DATE", "HOKAN_STR_DATE")
        map.Add("NIYAKU_YN", "NIYAKU_YN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("REMARK", "REMARK")
        map.Add("NYUBAN_L", "NYUBAN_L")
        map.Add("UNCHIN_TP", "UNCHIN_TP")
        map.Add("UNCHIN_KB", "UNCHIN_KB")
        map.Add("OUTKA_MOTO", "OUTKA_MOTO")
        map.Add("SYARYO_KB", "SYARYO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNCHIN", "UNCHIN")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("EDIT_FLAG", "EDIT_FLAG")
        map.Add("MATCHING_FLAG", "MATCHING_FLAG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_INKAEDI_L")
        reader.Close()

        Return ds
    End Function

#End Region

#Region "EDI(中)データ取得処理"
    ''' <summary>
    ''' EDI(中)データ取得処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC103.SQL_SELECT_EDI_M)
        '↓FFEM特殊処理↓
        '2014.06.09 追加START
        If ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("DEL_KB").ToString() = "2" Then
            Me._StrSql.Append(LMH010DAC103.SQL_SELECT_SYS_DEL_ON)
        Else
            Me._StrSql.Append(LMH010DAC103.SQL_SELECT_SYS_DEL_OFF)
        End If
        '↓FFEM特殊処理↓
        '2014.06.09 追加END

        Call Me.SetPrmEdiM()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "SelectEdiM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)


        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("NB", "NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("INKA_PKG_NB", "INKA_PKG_NB")
        map.Add("HASU", "HASU")
        map.Add("STD_IRIME", "STD_IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("CBM", "CBM")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("REMARK", "REMARK")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("OUT_KB", "OUT_KB")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_INKAEDI_M")
        reader.Close()

        Return ds
    End Function

#End Region

#Region "取得処理パラメータ設定(EDI_L)"
    Private Sub SetPrmEdiL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub
#End Region

#Region "取得処理パラメータ設定(EDI_M)"
    Private Sub SetPrmEdiM()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))

    End Sub
#End Region

#Region "実績送信データ取得処理(富士フイルム専用)"
    ''' <summary>
    ''' 実績送信データ取得処理(富士フイルム専用)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSendFujiFilm(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        'If Me._Row.Item("DEL_KB").ToString().Equals("3") = True Then
        '    Me._StrSql.Append(LMH010DAC103.SQL_SELECT_SEND_FJF_EDIT)
        'Else
        '    Me._StrSql.Append(LMH010DAC103.SQL_SELECT_SEND_FJF_CANCEL)
        'End If

        Me._StrSql.Append(LMH010DAC103.SQL_SELECT_SEND_FJF_CANCEL)

        Me._SqlPrmList = New ArrayList()

        Call Me.SetPrmCanJisseki(ds)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "SelectSendFujiFilm", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'reader.Read()

        'DataReader→DataTableへの転記
        Dim map As Hashtable



        '取得データの格納先をマッピング
        map = Me.SetMapSendFujiFilm()



        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_SENDINEDI_FJF")
        'MyBase.SetResultCount(ds.Tables("H_SENDINEDI_FJF").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#Region "M_GOODS"

    ''' <summary>
    ''' 件数取得処理(富士フイルム品目マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataHinmokuFjf(ByVal ds As DataSet) As DataSet

        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC103.SQL_SELECT_M_HINMOKU_FJF)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetHinmokuFjfParameter(dtM)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "SelectDataHinmokuFjf", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

    ''' <summary>
    ''' 件数取得処理(INKA_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>オーダー番号重複チェック</remarks>
    Private Function SelectOrderMotoData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC103.SQL_SELECT_INKA_L)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetInkaParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "SelectOrderMotoData", cmd)


        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("INKA_NO_L")))
        reader.Close()

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()


        Return ds

        
    End Function

#End Region

#Region "UPDATE処理"

#Region "INKAEDI_L"

    ''' <summary>
    ''' EDI(大)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiL(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)
            '入荷登録、紐付け
            Case LMH010DAC.EventShubetsu.TOROKU, LMH010DAC.EventShubetsu.HIMODUKE

                setSql = LMH010DAC103.SQL_UPD_INKATOROKU_EDI_L

                '実績作成
            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE

                setSql = LMH010DAC103.SQL_UPD_JISSEKISAKUSEI_EDI_L

        End Select

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, nrsBrCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetUpdPrmEdiL(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))
        Call Me.SetJissekiParameterEdiLEdiM(dt.Rows(0), eventShubetsu)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "LMH010_INKAEDI_L", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "INKAEDI_M"

    ''' <summary>
    ''' EDI(中)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim rtn As Integer = 0
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)
            '入荷登録、紐付け
            Case LMH010DAC.EventShubetsu.TOROKU, LMH010DAC.EventShubetsu.HIMODUKE

                setSql = LMH010DAC103.SQL_UPD_INKATOROKU_EDI_M

                '実績作成
            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE

                setSql = LMH010DAC103.SQL_UPD_JISSEKISAKUSEI_EDI_M

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, nrsBrCd))
        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetUpdPrmEdiM(dtRow)
            Call Me.SetJissekiParameterEdiLEdiM(dtRow, eventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "UpdateEdiM", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

    '富士フイルム追加箇所 terakawa 2012.08.02 Start
#Region "RCV_HED"

    ''' <summary>
    ''' 受信ヘッダ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateRcvHed(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_RCV_HED")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)
            '入荷登録
            Case LMH010DAC.EventShubetsu.TOROKU, LMH010DAC.EventShubetsu.HIMODUKE
                setSql = LMH010DAC103.SQL_UPD_RCV_HED

                '実績作成
            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE
                setSql = LMH010DAC103.SQL_UPDATE_RCV_HED_FJF

        End Select

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetUpdPrmRcvHed(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "UpdateRcvHed", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region
    '富士フイルム追加箇所 terakawa 2012.08.02 End

#Region "RCV_DTL"

    ''' <summary>
    ''' 受信明細更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateRcvDtl(ByVal ds As DataSet) As DataSet


        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_RCV_DTL")
        Dim rtn As Integer = 0

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)
            '入荷登録
            Case LMH010DAC.EventShubetsu.TOROKU, LMH010DAC.EventShubetsu.HIMODUKE
                setSql = LMH010DAC103.SQL_UPDATE_RCV_DTL

                '実績作成
            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE
                setSql = LMH010DAC103.SQL_UPDATE_RCV_DTL_FJF

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))
        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetUpdPrmRcvDtl(dtRow)

            Dim methodNm As String = Reflection.MethodBase.GetCurrentMethod.Name


            Call Me.SetJissekiParameterRcv(dtRow, eventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "UpdateRcvDtl", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "INKA_L"

    ''' <summary>
    ''' EDI(大)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaL(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_B_INKA_L")
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        setSql = SQL_UPD_JISSEKISAKUSEI_INKA_L

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, nrsBrCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetInkaLComParameter(dt.Rows(0))
        Call Me.SetHaitaDateTime(dt.Rows(0))
        Call Me.SetJissekiParameterEdiLEdiM(dt.Rows(0), eventShubetsu)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "UpdateInkaL", cmd)
        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#End Region

#Region "INSERT処理"

#Region "INKA_L"

    ''' <summary>
    ''' 入荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertInkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010_B_INKA_L")
        'INTableの条件rowの格納
        Dim dtRow As DataRow = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_INKA_L, dtRow.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetDataInsertParameter()
        Call Me.SetInkaLComParameter(dtRow)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertInkaL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "INKA_M"

    ''' <summary>
    ''' 入荷(中)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(中)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertInkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_B_INKA_M")
        Dim rtn As Integer = 0
        Dim brCd As String = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_INKA_M, brCd))

        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetDataInsertParameter()
            Call Me.SetInkaMComParameter(dtRow)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertInkaM", cmd)

            'SQLの発行
            rtn = MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "INKA_S"

    ''' <summary>
    ''' 入荷(小)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷(小)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertInkaS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_B_INKA_S")
        Dim rtn As Integer = 0
        Dim brCd As String = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_INKA_S, brCd))

        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetDataInsertParameter()
            Call Me.SetInkaSComParameter(dtRow)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertInkaS", cmd)

            'SQLの発行
            rtn = MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "ZAI_TRS"

    ''' <summary>
    ''' 在庫テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertZaiTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_ZAI_TRS")
        Dim rtn As Integer = 0
        Dim brCd As String = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_ZAI_TRS, brCd))

        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetDataInsertParameter()
            Call Me.SetZaiTrsComParameter(dtRow)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertZaiTrs", cmd)

            'SQLの発行
            rtn = MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "H_SENDINEDI_FJF"

    ''' <summary>
    ''' 実績送信新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>実績送信新規登録SQLの構築・発行</remarks>
    Private Function InsertSendFujiFilm(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("H_SENDINEDI_FJF")

        '営業所コード
        Dim brCd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_H_SENDINEDI_FJF, brCd))
        Dim max As Integer = inTbl.Rows.Count() - 1
        Dim dtRow As DataRow

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            dtRow = inTbl.Rows(i)
            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetDataInsertParameter()
            Call Me.SetPrmSendIn(dtRow)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertSendFujiFilm", cmd)

            'SQLの発行
            Dim rtn As Integer = MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "E_SAGYO_REC"

    ''' <summary>
    ''' 作業レコードの新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業レコードの新規作成SQLの構築・発行</remarks>
    Private Function InsertSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim sagyoTbl As DataTable = ds.Tables("LMH010_E_SAGYO")

        Dim rtn As Integer = 0

        Dim nrsBrCd As String = ds.Tables("LMH010_INKAEDI_L").Rows(0)("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_SAGYO, nrsBrCd))
        Dim max As Integer = sagyoTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = sagyoTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetDataInsertParameter()
            Call Me.SetSagyoParameter(Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertSagyo", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLTbl As DataTable = ds.Tables("LMH010_UNSO_L")
        'INTableの条件rowの格納
        Dim drUnso As DataRow = unsoLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim nrsBrCd As String = ds.Tables("LMH010_INKAEDI_L").Rows(0)("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_UNSO_L, nrsBrCd))

        'パラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetDataInsertParameter()
        Call Me.SetUnsoLComParameter(drUnso)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertUnsoL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送（中）テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送（中）テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoMTbl As DataTable = ds.Tables("LMH010_UNSO_M")

        Dim nrsBrCd As String = ds.Tables("LMH010_INKAEDI_L").Rows(0)("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_UNSO_M, nrsBrCd))


        Dim max As Integer = unsoMTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = unsoMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetDataInsertParameter()
            Call Me.SetUnsoMComParameter(Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertUnsoM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_UNCHIN_TRS").Rows.Count = 0 Then
            'F_UNCHIN_TRSが0件ということは本来無いが、一応念のために0件の時はINSERT処理が行われないようにする
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_UNCHIN_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC103.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables("F_UNCHIN_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter()
            '仮修正箇所 START
            Call Me.SetSysdataParameter()
            '仮修正箇所 END
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC103", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

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

#Region "更新パラメータ設定(共通)"
    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter()

        'システム項目
        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetHaitaDateTime(ByVal dr As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI_L,EDI_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterEdiLEdiM(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            Case LMH010DAC.EventShubetsu.TOROKU, LMH010DAC.EventShubetsu.HIMODUKE

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(row.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(row.Item("JISSEKI_DATE")), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(row.Item("JISSEKI_TIME")), DBDataType.CHAR))

            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ実績日時(Rcv)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterRcv(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter()

        'システム項目
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(EDI_L)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmEdiL(ByVal dt As DataTable)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(dt.Rows(0).Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(dt.Rows(0).Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", Me.NullConvertString(dt.Rows(0).Item("INKA_TP")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", Me.NullConvertString(dt.Rows(0).Item("INKA_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", Me.NullConvertString(dt.Rows(0).Item("INKA_STATE_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(dt.Rows(0).Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", Me.NullConvertString(dt.Rows(0).Item("INKA_TIME")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_WH_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(dt.Rows(0).Item("CUST_NM_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(dt.Rows(0).Item("CUST_NM_M")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", Me.NullConvertZero(dt.Rows(0).Item("INKA_PLAN_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", Me.NullConvertString(dt.Rows(0).Item("INKA_PLAN_QT_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", Me.NullConvertZero(dt.Rows(0).Item("INKA_TTL_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(dt.Rows(0).Item("NAIGAI_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.NullConvertString(dt.Rows(0).Item("SEIQTO_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(dt.Rows(0).Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", Me.NullConvertString(dt.Rows(0).Item("HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", Me.NullConvertZero(dt.Rows(0).Item("HOKAN_FREE_KIKAN")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", Me.NullConvertString(dt.Rows(0).Item("HOKAN_STR_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(dt.Rows(0).Item("NIYAKU_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(dt.Rows(0).Item("TAX_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(dt.Rows(0).Item("REMARK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NYUBAN_L", Me.NullConvertString(dt.Rows(0).Item("NYUBAN_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", Me.NullConvertString(dt.Rows(0).Item("UNCHIN_TP")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", Me.NullConvertString(dt.Rows(0).Item("UNCHIN_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_MOTO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(dt.Rows(0).Item("SYARYO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(dt.Rows(0).Item("UNSO_ONDO_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(dt.Rows(0).Item("UNSO_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(dt.Rows(0).Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN", Me.NullConvertString(dt.Rows(0).Item("UNCHIN")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", Me.NullConvertString(dt.Rows(0).Item("YOKO_TARIFF_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(dt.Rows(0).Item("OUT_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(dt.Rows(0).Item("JISSEKI_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(dt.Rows(0).Item("AKAKURO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(dt.Rows(0).Item("FREE_N01")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(dt.Rows(0).Item("FREE_N02")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(dt.Rows(0).Item("FREE_N03")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(dt.Rows(0).Item("FREE_N04")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(dt.Rows(0).Item("FREE_N05")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(dt.Rows(0).Item("FREE_N06")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(dt.Rows(0).Item("FREE_N07")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(dt.Rows(0).Item("FREE_N08")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(dt.Rows(0).Item("FREE_N09")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(dt.Rows(0).Item("FREE_N10")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(dt.Rows(0).Item("FREE_C01")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(dt.Rows(0).Item("FREE_C02")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(dt.Rows(0).Item("FREE_C03")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(dt.Rows(0).Item("FREE_C04")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(dt.Rows(0).Item("FREE_C05")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(dt.Rows(0).Item("FREE_C06")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(dt.Rows(0).Item("FREE_C07")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(dt.Rows(0).Item("FREE_C08")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(dt.Rows(0).Item("FREE_C09")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(dt.Rows(0).Item("FREE_C10")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(dt.Rows(0).Item("FREE_C11")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(dt.Rows(0).Item("FREE_C12")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(dt.Rows(0).Item("FREE_C13")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(dt.Rows(0).Item("FREE_C14")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(dt.Rows(0).Item("FREE_C15")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(dt.Rows(0).Item("FREE_C16")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(dt.Rows(0).Item("FREE_C17")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(dt.Rows(0).Item("FREE_C18")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(dt.Rows(0).Item("FREE_C19")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(dt.Rows(0).Item("FREE_C20")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(dt.Rows(0).Item("FREE_C21")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(dt.Rows(0).Item("FREE_C22")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(dt.Rows(0).Item("FREE_C23")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(dt.Rows(0).Item("FREE_C24")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(dt.Rows(0).Item("FREE_C25")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(dt.Rows(0).Item("FREE_C26")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(dt.Rows(0).Item("FREE_C27")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(dt.Rows(0).Item("FREE_C28")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(dt.Rows(0).Item("FREE_C29")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(dt.Rows(0).Item("FREE_C30")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(dt.Rows(0).Item("EDIT_FLAG")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(dt.Rows(0).Item("MATCHING_FLAG")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(dt.Rows(0).Item("SYS_DEL_FLG")), DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(EDI_M)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmEdiM(ByVal row As DataRow)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(row.Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(row.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(row.Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(row.Item("INKA_CTL_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(row.Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(row.Item("CUST_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(row.Item("GOODS_NM")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB", Me.NullConvertZero(row.Item("NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me.NullConvertString(row.Item("NB_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.NullConvertZero(row.Item("PKG_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me.NullConvertString(row.Item("PKG_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PKG_NB", Me.NullConvertZero(row.Item("INKA_PKG_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", Me.NullConvertZero(row.Item("HASU")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME", Me.NullConvertZero(row.Item("STD_IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", Me.NullConvertString(row.Item("STD_IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.NullConvertZero(row.Item("BETU_WT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CBM", Me.NullConvertZero(row.Item("CBM")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", Me.NullConvertString(row.Item("ONDO_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(row.Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(row.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(row.Item("REMARK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(row.Item("LOT_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(row.Item("SERIAL_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(row.Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(row.Item("IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_KB", Me.NullConvertString(row.Item("OUT_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(row.Item("AKAKURO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(row.Item("JISSEKI_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(row.Item("FREE_N01")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(row.Item("FREE_N02")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(row.Item("FREE_N03")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(row.Item("FREE_N04")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(row.Item("FREE_N05")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(row.Item("FREE_N06")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(row.Item("FREE_N07")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(row.Item("FREE_N08")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(row.Item("FREE_N09")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(row.Item("FREE_N10")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(row.Item("FREE_C01")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(row.Item("FREE_C02")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(row.Item("FREE_C03")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(row.Item("FREE_C04")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(row.Item("FREE_C05")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(row.Item("FREE_C06")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(row.Item("FREE_C07")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(row.Item("FREE_C08")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(row.Item("FREE_C09")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(row.Item("FREE_C10")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(row.Item("FREE_C11")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(row.Item("FREE_C12")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(row.Item("FREE_C13")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(row.Item("FREE_C14")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(row.Item("FREE_C15")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(row.Item("FREE_C16")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(row.Item("FREE_C17")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(row.Item("FREE_C18")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(row.Item("FREE_C19")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(row.Item("FREE_C20")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(row.Item("FREE_C21")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(row.Item("FREE_C22")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(row.Item("FREE_C23")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(row.Item("FREE_C24")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(row.Item("FREE_C25")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(row.Item("FREE_C26")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(row.Item("FREE_C27")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(row.Item("FREE_C28")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(row.Item("FREE_C29")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(row.Item("FREE_C30")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(row.Item("SYS_DEL_FLG")), DBDataType.CHAR))


    End Sub

#End Region

    '富士フイルム追加箇所 terakawa 2012.08.02 Start
#Region "更新パラメータ設定(RCV_HED)"
    ''' <summary>
    ''' 更新パラメータ設定(RCV_HED)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmRcvHed(ByVal dt As DataTable)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(dt.Rows(0).Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(dt.Rows(0).Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(dt.Rows(0).Item("SYS_DEL_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_USER", Me.NullConvertString(dt.Rows(0).Item("INKA_USER")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(dt.Rows(0).Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", Me.NullConvertString(dt.Rows(0).Item("INKA_TIME")), DBDataType.CHAR))


    End Sub

#End Region
    '富士フイルム追加箇所 terakawa 2012.08.02 End

#Region "更新パラメータ設定(RCV_DTL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmRcvDtl(ByVal row As DataRow)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(row.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(row.Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(row.Item("INKA_CTL_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(row.Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(row.Item("SYS_DEL_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(row.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(INKA_L)"
    ''' <summary>
    ''' INKA_Lの更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaLComParameter(ByVal row As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me.NullConvertString(row.Item("INKA_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(row.Item("FURI_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", Me.NullConvertString(row.Item("INKA_TP")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", Me.NullConvertString(row.Item("INKA_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", Me.NullConvertString(row.Item("INKA_STATE_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(row.Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(row.Item("NRS_WH_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(row.Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(row.Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", Me.NullConvertZero(row.Item("INKA_PLAN_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", Me.NullConvertString(row.Item("INKA_PLAN_QT_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", Me.NullConvertZero(row.Item("INKA_TTL_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", Me.NullConvertString(row.Item("BUYER_ORD_NO_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", Me.NullConvertString(row.Item("OUTKA_FROM_ORD_NO_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.NullConvertString(row.Item("SEIQTO_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(row.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", Me.NullConvertString(row.Item("HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", Me.NullConvertZero(row.Item("HOKAN_FREE_KIKAN")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", Me.NullConvertString(row.Item("HOKAN_STR_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(row.Item("NIYAKU_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(row.Item("TAX_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(row.Item("REMARK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", Me.NullConvertString(row.Item("REMARK_OUT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", Me.NullConvertString(row.Item("UNCHIN_TP")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", Me.NullConvertString(row.Item("UNCHIN_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(row.Item("SYS_DEL_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", Me.NullConvertString(row.Item("WH_TAB_STATUS")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", Me.NullConvertString(row.Item("WH_TAB_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_TAB_IMP_YN", Me.NullConvertString(row.Item("WH_TAB_IMP_YN")), DBDataType.CHAR))

    End Sub
#End Region

#Region "更新パラメータ設定(INKA_M)"
    ''' <summary>
    ''' 更新パラメータ設定(INKA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaMComParameter(ByVal row As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", row.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", row.Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", row.Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", row.Item("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(INKA_S)"
    ''' <summary>
    ''' INKA_Sの更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaSComParameter(ByVal row As DataRow)

        '富士フイルム 特殊設定　追加START
        Dim ltDate As String = String.Empty
        If String.IsNullOrEmpty((row.Item("LT_DATE").ToString())) = False Then
            Dim ltYear As Integer = Convert.ToInt32((row.Item("LT_DATE").ToString()).Substring(0, 4))
            Dim ltMonth As Integer = Convert.ToInt32((row.Item("LT_DATE").ToString()).Substring(4, 2))
            ltDate = Convert.ToString(Date.DaysInMonth(ltYear, ltMonth))
        End If
        '富士フイルム 特殊設定　追加END

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", row.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", row.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", row.Item("INKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        '要望番号1003 2012.05.08 追加START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", row.Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", row.Item("SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", row.Item("ZONE_CD").ToString(), DBDataType.CHAR))
        '要望番号1003 2012.05.08 追加END
        '富士フイルム 特殊設定　追加START
        If row.Item("LT_DATE").ToString.Length = 8 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", row.Item("LT_DATE").ToString(), DBDataType.CHAR))
        ElseIf row.Item("LT_DATE").ToString.Length = 6 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", String.Concat(row.Item("LT_DATE").ToString(), ltDate), DBDataType.CHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", String.Empty, DBDataType.CHAR))
        End If

        '富士フイルム 特殊設定　追加END
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KONSU", Me.FormatNumValue(row.Item("KONSU").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(row.Item("HASU").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(row.Item("IRIME").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(row.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", row.Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", row.Item("SPD_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", row.Item("OFB_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", row.Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", row.Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(ZAI_TRS)"

    ''' <summary>
    ''' ZAI_TRSの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_PLAN_DATE", .Item("INKO_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZERO_FLAG", .Item("ZERO_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD_P").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


        End With

    End Sub

#End Region

#Region "更新パラメータ設定(SEND)"
    ''' <summary>
    ''' 更新,検索パラメータ設定(SEND)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetPrmRcvSend(ByVal dt As DataTable)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(dt.Rows(0).Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

    End Sub

#End Region

#Region "挿入パラメータ設定(H_SENDINEDI_FJF)"
    Private Sub SetPrmSendIn(ByVal row As DataRow)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(row.Item("DEL_KB").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(row.Item("CRT_DATE").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(row.Item("FILE_NAME").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(row.Item("REC_NO").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(row.Item("GYO").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(row.Item("EDI_CTL_NO_CHU").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(row.Item("INKA_CTL_NO_L").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(row.Item("INKA_CTL_NO_M").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_S", Me.NullConvertString(row.Item("INKA_CTL_NO_S").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(row.Item("CUST_CD_L").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(row.Item("CUST_CD_M").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYCANKB", Me.NullConvertString(row.Item("ZFVYCANKB").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_ID", Me.NullConvertString(row.Item("FILE_ID").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYKAISYAC", Me.NullConvertString(row.Item("ZFVYKAISYAC").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYDENNO", Me.NullConvertString(row.Item("ZFVYDENNO").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYMEINO", Me.NullConvertString(row.Item("ZFVYMEINO").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATNR", Me.NullConvertString(row.Item("MATNR").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHARG", Me.NullConvertString(row.Item("CHARG").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAN_NO", Me.NullConvertString(row.Item("CAN_NO").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYDENTYP", Me.NullConvertString(row.Item("ZFVYDENTYP").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYWADAT", Me.NullConvertString(row.Item("ZFVYWADAT").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYHWERKS", Me.NullConvertString(row.Item("ZFVYHWERKS").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_LGORT", Me.NullConvertString(row.Item("OUT_LGORT").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYKWERKS", Me.NullConvertString(row.Item("ZFVYKWERKS").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_LGORT", Me.NullConvertString(row.Item("IN_LGORT").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIZOU_DATE", Me.NullConvertString(row.Item("SEIZOU_DATE").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOSHOU_KIGEN", Me.NullConvertString(row.Item("HOSHOU_KIGEN").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(row.Item("SERIAL_NO").ToString()), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LABEL_SEQ", Me.NullConvertString(row.Item("LABEL_SEQ").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZFVYSURYO", Me.NullConvertZero(row.Item("ZFVYSURYO").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(row.Item("RECORD_STATUS").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_FLG", Me.NullConvertString(row.Item("CANI_HOUKOKU_FLG").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(row.Item("JISSEKI_SHORI_FLG").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LATEST_HOUKOKU_FLG", Me.NullConvertString(row.Item("LATEST_HOUKOKU_FLG").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.GetSystemDate(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.GetSystemTime(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.NullConvertString(row.Item("SEND_USER").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.NullConvertString(row.Item("SEND_DATE").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", Me.NullConvertString(row.Item("SEND_TIME").ToString()), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_SHORI_FLG", Me.NullConvertString(row.Item("CANI_HOUKOKU_SHORI_FLG").ToString()), DBDataType.NVARCHAR))
        If Me.NullConvertString(row.Item("CANI_HOUKOKU_FLG").ToString()).Equals("1") = True Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_USER", Me.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_DATE", Me.GetSystemDate(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_TIME", Me.GetSystemTime(), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_USER", String.Empty, DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_DATE", String.Empty, DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANI_HOUKOKU_TIME", String.Empty, DBDataType.NVARCHAR))
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

    End Sub

#End Region

#Region "作業更新パラメータ設定"

    ''' <summary>
    ''' 作業の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", NullConvertZero(.Item("SAGYO_NB")).ToString(), DBDataType.NUMERIC))
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", NullConvertZero(.Item("SAGYO_GK")).ToString(), DBDataType.NUMERIC))
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "運送(大)パラメータ設定"

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me.NullConvertString(.Item("NRS_WH_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.NullConvertString(.Item("UNSO_NO_L")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", Me.NullConvertString(.Item("YUSO_BR_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", Me.NullConvertString(.Item("INOUTKA_NO_L")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me.NullConvertString(.Item("TRIP_NO")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIYU_KB", Me.NullConvertString(.Item("JIYU_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(.Item("DENP_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", Me.NullConvertString(.Item("OUTKA_PLAN_TIME")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", Me.NullConvertString(.Item("ARR_ACT_TIME")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", Me.NullConvertString(.Item("CUST_REF_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD", Me.NullConvertString(.Item("SHIP_CD")), DBDataType.CHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_CD", Me.NullConvertString(.Item("ORIG_CD")), DBDataType.NVARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", Me.FormatNumValue(.Item("UNSO_PKG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me.NullConvertString(.Item("NB_UT")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_WT", Me.FormatNumValue(.Item("UNSO_WT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(.Item("UNSO_ONDO_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me.NullConvertString(.Item("TARIFF_BUNRUI_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VCLE_KB", Me.NullConvertString(.Item("VCLE_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", Me.NullConvertString(.Item("MOTO_DATA_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(.Item("TAX_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", Me.NullConvertString(.Item("SEIQ_TARIFF_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", Me.NullConvertString(.Item("SEIQ_ETARIFF_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", Me.NullConvertString(.Item("BUY_CHU_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AREA_CD", Me.NullConvertString(.Item("AREA_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", Me.NullConvertString(.Item("TYUKEI_HAISO_FLG")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", Me.NullConvertString(.Item("SYUKA_TYUKEI_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", Me.NullConvertString(.Item("HAIKA_TYUKEI_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", Me.NullConvertString(.Item("TRIP_NO_SYUKA")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", Me.NullConvertString(.Item("TRIP_NO_TYUKEI")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", Me.NullConvertString(.Item("TRIP_NO_HAIKA")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

#End Region

#Region "運送(中)パラメータ設定"

    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow)

        With conditionRow

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.NullConvertString(.Item("UNSO_NO_L")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.NullConvertString(.Item("UNSO_NO_M")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me.NullConvertString(.Item("GOODS_CD_NRS")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(.Item("GOODS_NM")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", Me.FormatNumValue(.Item("UNSO_TTL_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me.NullConvertString(.Item("NB_UT")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", Me.FormatNumValue(.Item("UNSO_TTL_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@QT_UT", Me.NullConvertString(.Item("QT_UT")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me.NullConvertString(.Item("ZAI_REC_NO")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(.Item("UNSO_ONDO_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(.Item("IRIME_UT")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", Me.NullConvertString(.Item("SIZE_KB")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", Me.NullConvertString(.Item("ZBUKA_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", Me.NullConvertString(.Item("ABUKA_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(.Item("LOT_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "運賃パラメータ設定"

    ''' <summary>
    ''' 運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", Me.FormatNumValue(.Item("SEIQ_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", Me.FormatNumValue(.Item("SEIQ_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", Me.FormatNumValue(.Item("SEIQ_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_UNCHIN", Me.FormatNumValue(.Item("SEIQ_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CITY_EXTC", Me.FormatNumValue(.Item("SEIQ_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WINT_EXTC", Me.FormatNumValue(.Item("SEIQ_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_RELY_EXTC", Me.FormatNumValue(.Item("SEIQ_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TOLL", Me.FormatNumValue(.Item("SEIQ_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_INSU", Me.FormatNumValue(.Item("SEIQ_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", Me.FormatNumValue(.Item("DECI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", Me.FormatNumValue(.Item("DECI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", Me.FormatNumValue(.Item("DECI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", Me.FormatNumValue(.Item("DECI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", Me.FormatNumValue(.Item("DECI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", Me.FormatNumValue(.Item("DECI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", Me.FormatNumValue(.Item("DECI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", Me.FormatNumValue(.Item("DECI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", Me.FormatNumValue(.Item("DECI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", Me.FormatNumValue(.Item("KANRI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", Me.FormatNumValue(.Item("KANRI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", Me.FormatNumValue(.Item("KANRI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", Me.FormatNumValue(.Item("KANRI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", Me.FormatNumValue(.Item("KANRI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", Me.FormatNumValue(.Item("KANRI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))
            '仮修正箇所 START
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))
            '仮修正箇所 END

        End With

    End Sub

#End Region

#Region "富士フイルム品目マスタパラメータ設定"

    ''' <summary>
    ''' M_HINMOKU_FJF
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHinmokuFjfParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", dt.Rows(0).Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))

    End Sub

#End Region

    '↑FFEM特殊処理↓
    '2014.06.09 追加START
#Region "入荷キャンセル実績パラメータ設定"

    ''' <summary>
    ''' 入荷キャンセル実績パラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPrmCanJisseki(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", dt.Rows(0).Item("OUTKA_FROM_ORD_NO").ToString(), DBDataType.NVARCHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", dt.Rows(0).Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", dtM.Rows(0).Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", dt.Rows(0).Item("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", dtM.Rows(0).Item("INKA_CTL_NO_M").ToString(), DBDataType.CHAR))

    End Sub

#End Region

    ''' <summary>
    ''' B_INKA_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))

    End Sub
    '↓FFEM特殊処理↑
    '2014.06.09 追加START

#End Region 'SQL

#Region "変換"
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


    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region

#Region "マッピング配列作成"
    ''' <summary>
    ''' マッピング配列作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMapSendFujiFilm() As Hashtable

        Dim map As Hashtable = New Hashtable

        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("EDA", "EDA")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("INKA_CTL_NO_S", "INKA_CTL_NO_S")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("ZFVYCANKB", "ZFVYCANKB")
        map.Add("FILE_ID", "FILE_ID")
        map.Add("ZFVYKAISYAC", "ZFVYKAISYAC")
        map.Add("ZFVYDENNO", "ZFVYDENNO")
        map.Add("ZFVYMEINO", "ZFVYMEINO")
        map.Add("MATNR", "MATNR")
        map.Add("CHARG", "CHARG")
        map.Add("CAN_NO", "CAN_NO")
        map.Add("ZFVYDENTYP", "ZFVYDENTYP")
        map.Add("ZFVYWADAT", "ZFVYWADAT")
        map.Add("ZFVYHWERKS", "ZFVYHWERKS")
        map.Add("OUT_LGORT", "OUT_LGORT")
        map.Add("ZFVYKWERKS", "ZFVYKWERKS")
        map.Add("IN_LGORT", "IN_LGORT")
        map.Add("SEIZOU_DATE", "SEIZOU_DATE")
        map.Add("HOSHOU_KIGEN", "HOSHOU_KIGEN")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("LABEL_SEQ", "LABEL_SEQ")
        map.Add("ZFVYSURYO", "ZFVYSURYO")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("CANI_HOUKOKU_FLG", "CANI_HOUKOKU_FLG")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("LATEST_HOUKOKU_FLG", "LATEST_HOUKOKU_FLG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("CANI_HOUKOKU_SHORI_FLG", "CANI_HOUKOKU_SHORI_FLG")
        map.Add("CANI_HOUKOKU_USER", "CANI_HOUKOKU_USER")
        map.Add("CANI_HOUKOKU_DATE", "CANI_HOUKOKU_DATE")
        map.Add("CANI_HOUKOKU_TIME", "CANI_HOUKOKU_TIME")

        Return map

    End Function
#End Region

#End Region 'Method

End Class
