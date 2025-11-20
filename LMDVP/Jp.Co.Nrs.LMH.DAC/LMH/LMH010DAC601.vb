' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  024　　　 : 日本合成化学(名古屋)
'  作  成  者       :  nishikawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports System.Reflection
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME010DAC
''' </summary>
''' <remarks></remarks>
''' 
Public Class LMH010DAC601
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "イベント種別"


#End Region


#Region "データセットテーブル名"
    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Class TABLE_NM
        Public Const LMH010INOUT As String = "LMH010INOUT"
        Public Const LMH010_ZAI_TRS_GOODS As String = "LMH010_ZAI_TRS_GOODS"
        Public Const LMH010_INKAEDI_L As String = "LMH010_INKAEDI_L"
        Public Const LMH010_INKAEDI_M As String = "LMH010_INKAEDI_M"
        Public Const LMH010_DEST As String = "LMH010_DEST"
        Public Const LMH010_B_INKA_L As String = "LMH010_B_INKA_L"
        Public Const LMH010_B_INKA_M As String = "LMH010_B_INKA_M"
        Public Const LMH010_B_INKA_S As String = "LMH010_B_INKA_S"
        Public Const LMH010_OUTKA_L As String = "LMH010_OUTKA_L"
        Public Const LMH010_OUTKA_M As String = "LMH010_OUTKA_M"
        Public Const LMH010_OUTKA_S As String = "LMH010_OUTKA_S"
        Public Const LMH010_UNSO_L As String = "LMH010_UNSO_L"
        Public Const LMH010_UNSO_M As String = "LMH010_UNSO_M"

    End Class

#End Region

#Region "セミEDI処理"

#Region "列挙値"

    ''' <summary>
    ''' MCLC荷主固有項目
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MclcArrivalColumns

        ''' <summary>
        ''' 受注ヘッダ訂正No.
        ''' </summary>
        JYUCHU_HED_TEISEI_NO = 4

        ''' <summary>
        ''' 赤黒区分（受注ヘッダ）
        ''' </summary>
        ''' <remarks></remarks>
        AKA_KURO_KBN = 5

        ''' <summary>
        ''' 荷主オーダーNo.
        ''' </summary>
        ''' <remarks></remarks>
        CUST_ORDER_NO = 6

        ''' <summary>
        ''' 依頼種別コード
        ''' </summary>
        IRAI_SYUBETSU_CD = 11

        ''' <summary>
        ''' 指定作業日
        ''' </summary>
        SHITEI_WORK_DATE = 14

        ''' <summary>
        ''' データID
        ''' </summary>
        ''' <remarks></remarks>
        DATA_ID = 189

        ''' <summary>
        ''' 細目区分
        ''' </summary>
        ''' <remarks></remarks>
        DETAIL_KBN = 190

        ''' <summary>
        ''' 先方SPコード
        ''' </summary>
        SENPOU_SP_CD = 201

        ''' <summary>
        ''' 品名愛称
        ''' </summary>
        ITEM_AISYO = 209

        ''' <summary>
        ''' 品名コード
        ''' </summary>
        ''' <remarks></remarks>
        ITEM_CD = 210

        ''' <summary>
        ''' 品名略号
        ''' </summary>
        ''' <remarks></remarks>
        ITEM_RYAKUGO = 211

        ''' <summary>
        ''' ロットNo.
        ''' </summary>
        LOT_NO = 226

        ''' <summary>
        ''' 個数
        ''' </summary>
        KOSU = 228

        ''' <summary>
        ''' 数量
        ''' </summary>
        SUURYO = 230

        ''' <summary>
        ''' 文字項目（短）０３ (MCC元項目: 荷主伝票明細№)
        ''' </summary>
        STR_ITEM_SHORT_03 = 278

        ''' <summary>
        ''' 文字項目（短）０５ (MCC元項目: 荷主依頼明細№)
        ''' </summary>
        STR_ITEM_SHORT_05 = 280

        ''' <summary>
        ''' 文字項目（短）１５ (MCC元項目: データ作成日)
        ''' </summary>
        STR_ITEM_SHORT_15 = 290

        ''' <summary>
        ''' 文字項目（短）１６ (MCC元項目: データ作成時刻)
        ''' </summary>
        STR_ITEM_SHORT_16 = 291

    End Enum

#End Region ' "列挙値"


    ''' <summary>
    ''' カラム名接頭文字
    ''' </summary>
    ''' <remarks></remarks>
    Private Const COLUMN_NAME_PREFIX As String = "COLUMN_"

#End Region ' "セミEDI処理"

#Region "検索処理"


#Region "SELECT_EDI_L"
    ''' <summary>
    ''' 更新用検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_EDI_L As String = " SELECT                                                  " & vbNewLine _
                                    & " '0'                                     AS DEL_KB                " & vbNewLine _
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
                                    & " H_INKAEDI_L.OUT_FLAG = @OUT_FLAG                                 " & vbNewLine _
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
                                   & " H_INKAEDI_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                   & " AND                                                               " & vbNewLine _
                                   & " H_INKAEDI_M.OUT_KB = '0'                                          " & vbNewLine


#End Region  '入荷登録

#Region "SELECT_SEND"

    ''' <summary>
    ''' SELECT_SEND
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEND As String = " SELECT                                                         " & vbNewLine _
                                    & " '0'   AS   DEL_KB                                                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.NRS_BR_CD   AS   NRS_BR_CD                          " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.EDI_CTL_NO   AS   EDI_CTL_NO                        " & vbNewLine _
                                    & ",'000'   AS   EDI_CTL_NO_EDA                                            " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INKA_CTL_NO_L   AS   INKA_CTL_NO_L                  " & vbNewLine _
                                    & ",''   AS   FILE_NAME                                                    " & vbNewLine _
                                    & ",''   AS   REC_NO                                                       " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.ID   AS   RCV_ID                                    " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.UKETSUKE_NO   AS   RCV_UKETSUKE_NO                  " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.UKETSUKE_NO_EDA   AS   RCV_UKETSUKE_NO_EDA          " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INPUT_KB   AS   RCV_INPUT_KB                        " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.RCV_EDA_UP_FLG   AS   RCV_EDA_UP_FLG                " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.ID   AS   ID                                        " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.SYSTEM_KB   AS   SYSTEM_KB                          " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.UKETSUKE_NO   AS   UKETSUKE_NO                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.UKETSUKE_NO_EDA   AS   UKETSUKE_NO_EDA              " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.COMPANY_CD   AS   COMPANY_CD                        " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.BASHO_CD   AS   BASHO_CD                            " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INKA_BUMON   AS   INKA_BUMON                        " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INKA_GROUP   AS   INKA_GROUP                        " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INPUT_YMD   AS   INPUT_YMD                          " & vbNewLine _
                                    & ",CASE WHEN B_INKA_L.INKA_DATE IS NULL THEN H_INKAEDI_HED_NCGO.INKA_YMD  " & vbNewLine _
                                    & "      ELSE B_INKA_L.INKA_DATE                                           " & vbNewLine _
                                    & " END                          AS INKA_YMD                               " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INKA_YMD   AS   INKA_YMD                            " & vbNewLine _
                                    & ",'1'   AS   INPUT_KB                                                    " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.GOODS_RYAKU   AS   GOODS_RYAKU                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.GRADE_1   AS   GRADE_1                              " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.GRADE_2   AS   GRADE_2                              " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.YORYO   AS   YORYO                                  " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.NISUGATA_CD   AS   NISUGATA_CD                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.YUSHUTSU_KB   AS   YUSHUTSU_KB                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.ZAIKO_KB   AS   ZAIKO_KB                            " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INKA_BASHO_SP   AS   INKA_BASHO_SP                  " & vbNewLine _
                                    & ",''   AS   URI_BASHO                                                    " & vbNewLine _
                                    & ",''   AS   URI_BUMON                                                    " & vbNewLine _
                                    & ",''   AS   URI_GROUP                                                    " & vbNewLine _
                                    & ",'0'   AS   SENPO_KENSHU_SURYO                                          " & vbNewLine _
                                    & ",DTL1.LOT_NO   AS   LOT_NO_1                                            " & vbNewLine _
                                    & ",DTL1.LOT_NO2   AS   LOT_NO2_1                                          " & vbNewLine _
                                    & ",DTL1.KOSU   AS   KOSU_1                                                " & vbNewLine _
                                    & ",DTL1.SURYO   AS   SURYO_1                                              " & vbNewLine _
                                    & ",DTL2.LOT_NO   AS   LOT_NO_2                                            " & vbNewLine _
                                    & ",DTL2.LOT_NO2   AS   LOT_NO2_2                                          " & vbNewLine _
                                    & ",DTL2.KOSU   AS   KOSU_2                                                " & vbNewLine _
                                    & ",DTL2.SURYO   AS   SURYO_2                                              " & vbNewLine _
                                    & ",DTL3.LOT_NO   AS   LOT_NO_3                                            " & vbNewLine _
                                    & ",DTL3.LOT_NO2   AS   LOT_NO2_3                                          " & vbNewLine _
                                    & ",DTL3.KOSU   AS   KOSU_3                                                " & vbNewLine _
                                    & ",DTL3.SURYO   AS   SURYO_3                                              " & vbNewLine _
                                    & ",DTL4.LOT_NO   AS   LOT_NO_4                                            " & vbNewLine _
                                    & ",DTL4.LOT_NO2   AS   LOT_NO2_4                                          " & vbNewLine _
                                    & ",DTL4.KOSU   AS   KOSU_4                                                " & vbNewLine _
                                    & ",DTL4.SURYO   AS   SURYO_4                                              " & vbNewLine _
                                    & ",DTL5.LOT_NO   AS   LOT_NO_5                                            " & vbNewLine _
                                    & ",DTL5.LOT_NO2   AS   LOT_NO2_5                                          " & vbNewLine _
                                    & ",DTL5.KOSU   AS   KOSU_5                                                " & vbNewLine _
                                    & ",DTL5.SURYO   AS   SURYO_5                                              " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.TTL_KOSU   AS   TTL_KOSU                            " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.TTL_SURYO   AS   TTL_SURYO                          " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.IN_BIKO_ANK   AS   IN_BIKO_ANK                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.IN_BIKO_BIKO   AS   IN_BIKO_BIKO                    " & vbNewLine _
                                    & ",''   AS   OUT_BIKO_ANK                                                 " & vbNewLine _
                                    & ",''   AS   OUT_BIKO_BIKO                                                " & vbNewLine _
                                    & ",''   AS   SHORI_NO                                                     " & vbNewLine _
                                    & ",''   AS   SHORI_NO_EDA                                                 " & vbNewLine _
                                    & ",''   AS   ERROR_FLG                                                    " & vbNewLine _
                                    & ",''   AS   KO_UKETSUKE_NO                                               " & vbNewLine _
                                    & ",''   AS   KO_UKETSUKE_NO_EDA                                           " & vbNewLine _
                                    & ",''   AS   GEKKAN_KEIYAKU_NO                                            " & vbNewLine _
                                    & ",DTL1.KOBETSU_NISUGATA_CD   AS   KOBETSU_NISUGATA_CD_1                  " & vbNewLine _
                                    & ",DTL1.KENTEI_HORYU_KB   AS   KENTEI_KB_1                                " & vbNewLine _
                                    & ",DTL1.HOKAN_ICHI_1   AS   HOKAN_ICHI_11                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_KOSU_1   AS   HOKAN_KOSU_11                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_SURYO_1   AS   HOKAN_SURYO_11                               " & vbNewLine _
                                    & ",DTL1.HOKAN_ICHI_2   AS   HOKAN_ICHI_12                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_KOSU_2   AS   HOKAN_KOSU_12                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_SURYO_2   AS   HOKAN_SURYO_12                               " & vbNewLine _
                                    & ",DTL1.HOKAN_ICHI_3   AS   HOKAN_ICHI_13                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_KOSU_3   AS   HOKAN_KOSU_13                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_SURYO_3   AS   HOKAN_SURYO_13                               " & vbNewLine _
                                    & ",DTL1.HOKAN_ICHI_4   AS   HOKAN_ICHI_14                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_KOSU_4   AS   HOKAN_KOSU_14                                 " & vbNewLine _
                                    & ",DTL1.HOKAN_SURYO_4   AS   HOKAN_SURYO_14                               " & vbNewLine _
                                    & ",DTL2.KOBETSU_NISUGATA_CD   AS   KOBETSU_NISUGATA_CD_2                  " & vbNewLine _
                                    & ",DTL2.KENTEI_HORYU_KB   AS   KENTEI_KB_2                                " & vbNewLine _
                                    & ",DTL2.HOKAN_ICHI_1   AS   HOKAN_ICHI_21                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_KOSU_1   AS   HOKAN_KOSU_21                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_SURYO_1   AS   HOKAN_SURYO_21                               " & vbNewLine _
                                    & ",DTL2.HOKAN_ICHI_2   AS   HOKAN_ICHI_22                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_KOSU_2   AS   HOKAN_KOSU_22                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_SURYO_2   AS   HOKAN_SURYO_22                               " & vbNewLine _
                                    & ",DTL2.HOKAN_ICHI_3   AS   HOKAN_ICHI_23                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_KOSU_3   AS   HOKAN_KOSU_23                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_SURYO_3   AS   HOKAN_SURYO_23                               " & vbNewLine _
                                    & ",DTL2.HOKAN_ICHI_4   AS   HOKAN_ICHI_24                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_KOSU_4   AS   HOKAN_KOSU_24                                 " & vbNewLine _
                                    & ",DTL2.HOKAN_SURYO_4   AS   HOKAN_SURYO_24                               " & vbNewLine _
                                    & ",DTL3.KOBETSU_NISUGATA_CD   AS   KOBETSU_NISUGATA_CD_3                  " & vbNewLine _
                                    & ",DTL3.KENTEI_HORYU_KB   AS   KENTEI_KB_3                                " & vbNewLine _
                                    & ",DTL3.HOKAN_ICHI_1   AS   HOKAN_ICHI_31                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_KOSU_1   AS   HOKAN_KOSU_31                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_SURYO_1   AS   HOKAN_SURYO_31                               " & vbNewLine _
                                    & ",DTL3.HOKAN_ICHI_2   AS   HOKAN_ICHI_32                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_KOSU_2   AS   HOKAN_KOSU_32                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_SURYO_2   AS   HOKAN_SURYO_32                               " & vbNewLine _
                                    & ",DTL3.HOKAN_ICHI_3   AS   HOKAN_ICHI_33                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_KOSU_3   AS   HOKAN_KOSU_33                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_SURYO_3   AS   HOKAN_SURYO_33                               " & vbNewLine _
                                    & ",DTL3.HOKAN_ICHI_4   AS   HOKAN_ICHI_34                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_KOSU_4   AS   HOKAN_KOSU_34                                 " & vbNewLine _
                                    & ",DTL3.HOKAN_SURYO_4   AS   HOKAN_SURYO_34                               " & vbNewLine _
                                    & ",DTL4.KOBETSU_NISUGATA_CD   AS   KOBETSU_NISUGATA_CD_4                  " & vbNewLine _
                                    & ",DTL4.KENTEI_HORYU_KB   AS   KENTEI_KB_4                                " & vbNewLine _
                                    & ",DTL4.HOKAN_ICHI_1   AS   HOKAN_ICHI_41                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_KOSU_1   AS   HOKAN_KOSU_41                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_SURYO_1   AS   HOKAN_SURYO_41                               " & vbNewLine _
                                    & ",DTL4.HOKAN_ICHI_2   AS   HOKAN_ICHI_42                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_KOSU_2   AS   HOKAN_KOSU_42                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_SURYO_2   AS   HOKAN_SURYO_42                               " & vbNewLine _
                                    & ",DTL4.HOKAN_ICHI_3   AS   HOKAN_ICHI_43                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_KOSU_3   AS   HOKAN_KOSU_43                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_SURYO_3   AS   HOKAN_SURYO_43                               " & vbNewLine _
                                    & ",DTL4.HOKAN_ICHI_4   AS   HOKAN_ICHI_44                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_KOSU_4   AS   HOKAN_KOSU_44                                 " & vbNewLine _
                                    & ",DTL4.HOKAN_SURYO_4   AS   HOKAN_SURYO_44                               " & vbNewLine _
                                    & ",DTL5.KOBETSU_NISUGATA_CD   AS   KOBETSU_NISUGATA_CD_5                  " & vbNewLine _
                                    & ",DTL5.KENTEI_HORYU_KB   AS   KENTEI_KB_5                                " & vbNewLine _
                                    & ",DTL5.HOKAN_ICHI_1   AS   HOKAN_ICHI_51                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_KOSU_1   AS   HOKAN_KOSU_51                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_SURYO_1   AS   HOKAN_SURYO_51                               " & vbNewLine _
                                    & ",DTL5.HOKAN_ICHI_2   AS   HOKAN_ICHI_52                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_KOSU_2   AS   HOKAN_KOSU_52                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_SURYO_2   AS   HOKAN_SURYO_52                               " & vbNewLine _
                                    & ",DTL5.HOKAN_ICHI_3   AS   HOKAN_ICHI_53                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_KOSU_3   AS   HOKAN_KOSU_53                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_SURYO_3   AS   HOKAN_SURYO_53                               " & vbNewLine _
                                    & ",DTL5.HOKAN_ICHI_4   AS   HOKAN_ICHI_54                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_KOSU_4   AS   HOKAN_KOSU_54                                 " & vbNewLine _
                                    & ",DTL5.HOKAN_SURYO_4   AS   HOKAN_SURYO_54                               " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.INKA_SOKUSHORI_KB   AS   INKA_SOKUSHORI_KB          " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.GENKA_BUMON   AS   GENKA_BUMON                      " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.BIN_KB   AS   BIN_KB                                " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.YUSO_COMP_CD   AS   YUSO_COMP_CD                    " & vbNewLine _
                                    & ",''   AS   JUST_OUTKA_KB                                                " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.SHUGENRYO_KB   AS   SHUGENRYO_KB                    " & vbNewLine _
                                    & ",H_INKAEDI_HED_NCGO.GEKKAN_KB   AS   GEKKAN_KB                          " & vbNewLine _
                                    & ",''   AS   YOBI                                                         " & vbNewLine _
                                    & ",''   AS   YOBI2                                                        " & vbNewLine _
                                    & ",''   AS   ERROR_MSG_1                                                  " & vbNewLine _
                                    & ",''   AS   ERROR_MSG_2                                                  " & vbNewLine _
                                    & ",''   AS   ERROR_MSG_3                                                  " & vbNewLine _
                                    & ",''   AS   ERROR_MSG_4                                                  " & vbNewLine _
                                    & ",''   AS   ERROR_MSG_5                                                  " & vbNewLine _
                                    & ",''   AS   RECORD_STATUS                                                " & vbNewLine _
                                    & ",'2'  AS   JISSEKI_SHORI_FLG                                            " & vbNewLine _
                                    & " FROM                                                                   " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_HED_NCGO     H_INKAEDI_HED_NCGO                    " & vbNewLine _
                                    & " LEFT JOIN                                                              " & vbNewLine _
                                    & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO     DTL1                                  " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW     DTL1                                  " & vbNewLine _
                                    & " ON                                                                     " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.CRT_DATE = DTL1.CRT_DATE                            " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.FILE_NAME = DTL1.FILE_NAME                          " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.REC_NO = DTL1.REC_NO                                " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " DTL1.EDI_CTL_NO_CHU = '001'                                                       " & vbNewLine _
                                    & " LEFT JOIN                                                              " & vbNewLine _
                                    & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO     DTL2                                  " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW     DTL2                                  " & vbNewLine _
                                    & " ON                                                                     " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.CRT_DATE = DTL2.CRT_DATE                            " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.FILE_NAME = DTL2.FILE_NAME                          " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.REC_NO = DTL2.REC_NO                                " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " DTL2.EDI_CTL_NO_CHU = '002'                                                       " & vbNewLine _
                                    & " LEFT JOIN                                                              " & vbNewLine _
                                    & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO     DTL3                                  " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW     DTL3                                  " & vbNewLine _
                                    & " ON                                                                     " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.CRT_DATE = DTL3.CRT_DATE                            " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.FILE_NAME = DTL3.FILE_NAME                          " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.REC_NO = DTL3.REC_NO                                " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " DTL3.EDI_CTL_NO_CHU = '003'                                                       " & vbNewLine _
                                    & " LEFT JOIN                                                              " & vbNewLine _
                                    & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO     DTL4                                  " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW     DTL4                                  " & vbNewLine _
                                    & " ON                                                                     " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.CRT_DATE = DTL4.CRT_DATE                            " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.FILE_NAME = DTL4.FILE_NAME                          " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.REC_NO = DTL4.REC_NO                                " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " DTL4.EDI_CTL_NO_CHU = '004'                                                       " & vbNewLine _
                                    & " LEFT JOIN                                                              " & vbNewLine _
                                    & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO     DTL5                                  " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW     DTL5                                  " & vbNewLine _
                                    & " ON                                                                     " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.CRT_DATE = DTL5.CRT_DATE                            " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.FILE_NAME = DTL5.FILE_NAME                          " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.REC_NO = DTL5.REC_NO                                " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " DTL5.EDI_CTL_NO_CHU = '005'                                                       " & vbNewLine _
                                    & " LEFT JOIN                                                              " & vbNewLine _
                                    & " $LM_TRN$..B_INKA_L              B_INKA_L                               " & vbNewLine _
                                    & " ON                                                                     " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.NRS_BR_CD  = B_INKA_L.NRS_BR_CD                     " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.INKA_CTL_NO_L = B_INKA_L.INKA_NO_L                  " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " B_INKA_L.NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
                                    & " WHERE                                                                  " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.NRS_BR_CD  = @NRS_BR_CD                             " & vbNewLine _
                                    & " AND                                                                    " & vbNewLine _
                                    & " H_INKAEDI_HED_NCGO.EDI_CTL_NO = @EDI_CTL_NO                            " & vbNewLine


#End Region  '実績作成

#Region "SQL_SELECT_UKETSUKENO"

    Private Const SQL_SELECT_UKETSUKENO As String = " SELECT                                                " & vbNewLine _
                                 & " H_INKAEDI_HED_NCGO.UKETSUKE_NO       AS   UKETSUKE_NO                  " & vbNewLine _
                                 & ",H_INKAEDI_HED_NCGO.UKETSUKE_NO_EDA   AS   UKETSUKE_NO_EDA              " & vbNewLine _
                                 & ",@NRS_BR_CD                           AS   NRS_BR_CD                    " & vbNewLine _
                                 & ",@EDI_CTL_NO                          AS   EDI_CTL_NO                   " & vbNewLine _
                                 & " FROM                                                                   " & vbNewLine _
                                 & " $LM_TRN$..H_INKAEDI_HED_NCGO     H_INKAEDI_HED_NCGO                    " & vbNewLine _
                                 & " WHERE                                                                  " & vbNewLine _
                                 & " H_INKAEDI_HED_NCGO.NRS_BR_CD  = @NRS_BR_CD                             " & vbNewLine _
                                 & " AND                                                                    " & vbNewLine _
                                 & " H_INKAEDI_HED_NCGO.EDI_CTL_NO = @EDI_CTL_NO                            " & vbNewLine

#End Region '日本合成オーダーチェック

#Region "SQL_SELECT_ORDER_COUNT"
    Private Const SQL_SELECT_ORDER_COUNT As String = " SELECT                                                  " & vbNewLine _
                                     & " COUNT(*)                               AS REC_CNT                     " & vbNewLine _
                                     & " FROM                                                                  " & vbNewLine _
                                     & " $LM_TRN$..H_INKAEDI_HED_NCGO     H_INKAEDI_HED_NCGO                   " & vbNewLine _
                                     & " LEFT JOIN $LM_TRN$..B_INKA_L     B_INKA_L                             " & vbNewLine _
                                     & " ON                                                                    " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.INKA_CTL_NO_L = B_INKA_L.INKA_NO_L                 " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.NRS_BR_CD = B_INKA_L.NRS_BR_CD                     " & vbNewLine _
                                     & " WHERE                                                                 " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.NRS_BR_CD  = @NRS_BR_CD                            " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.EDI_CTL_NO <> @EDI_CTL_NO                          " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.UKETSUKE_NO = @UKETSUKE_NO                         " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.UKETSUKE_NO_EDA >= @UKETSUKE_NO_EDA                " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.DEL_KB = '0'                                       " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " ISNULL(B_INKA_L.SYS_DEL_FLG,'N') <> '1'                               " & vbNewLine
#End Region '日本合成オーダーチェック

#Region "SQL_SELECT_ORDER_TORIKESHI_COUNT"
    Private Const SQL_SELECT_ORDER_TORIKESHI_COUNT As String = " SELECT                                        " & vbNewLine _
                                     & " COUNT(*)                               AS REC_CNT                     " & vbNewLine _
                                     & " FROM                                                                  " & vbNewLine _
                                     & " $LM_TRN$..H_INKAEDI_HED_NCGO     H_INKAEDI_HED_NCGO                   " & vbNewLine _
                                     & " WHERE                                                                 " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.NRS_BR_CD  = @NRS_BR_CD                            " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.EDI_CTL_NO <> 'N00000000'                          " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.UKETSUKE_NO = @UKETSUKE_NO                         " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " H_INKAEDI_HED_NCGO.UKETSUKE_NO_EDA = @UKETSUKE_NO_EDA                 " & vbNewLine _
                                     & " AND                                                                   " & vbNewLine _
                                     & " AKADEN_KB = '1'                                                       " & vbNewLine

#End Region '日本合成オーダーチェック



#Region "M品取得"

    Private Const SQL_SELECT_ZAI_TRS_COND_M As String _
        = " SELECT                                                                              " & vbNewLine _
        & "        EDI.EDI_CTL_NO                          AS EDI_CTL_NO                        " & vbNewLine _
        & "      , EDI.EDI_CTL_NO_CHU                      AS EDI_CTL_NO_CHU                    " & vbNewLine _
        & "      , EDI.INKA_CTL_NO_L                       AS INKA_CTL_NO_L                     " & vbNewLine _
        & "      , EDI.OUTKA_FROM_ORD_NO                   AS OUTKA_FROM_ORD_NO                 " & vbNewLine _
        & "      , EDI.BUYER_ORD_NO                        AS BUYER_ORD_NO                      " & vbNewLine _
        & "      , ZT.NRS_BR_CD                            AS NRS_BR_CD                         " & vbNewLine _
        & "      , ZT.ZAI_REC_NO                           AS ZAI_REC_NO                        " & vbNewLine _
        & "      , ZT.WH_CD                                AS WH_CD                             " & vbNewLine _
        & "      , ZT.TOU_NO                               AS TOU_NO                            " & vbNewLine _
        & "      , ZT.SITU_NO                              AS SITU_NO                           " & vbNewLine _
        & "      , ZT.ZONE_CD                              AS ZONE_CD                           " & vbNewLine _
        & "      , ZT.LOCA                                 AS LOCA                              " & vbNewLine _
        & "      , ZT.LOT_NO                               AS LOT_NO                            " & vbNewLine _
        & "      , ZT.CUST_CD_L                            AS CUST_CD_L                         " & vbNewLine _
        & "      , ZT.CUST_CD_M                            AS CUST_CD_M                         " & vbNewLine _
        & "      , ZT.GOODS_CD_NRS                         AS GOODS_CD_NRS                      " & vbNewLine _
        & "      , ZT.GOODS_KANRI_NO                       AS GOODS_KANRI_NO                    " & vbNewLine _
        & "      , MG.GOODS_NM_1                           AS GOODS_NM                          " & vbNewLine _
        & "      , MG.NB_UT                                AS NB_UT                             " & vbNewLine _
        & "      , MG.PKG_NB                               AS PKG_NB                            " & vbNewLine _
        & "      , MG.PKG_UT                               AS PKG_UT                            " & vbNewLine _
        & "      , MG.STD_IRIME_NB                         AS STD_IRIME_NB                      " & vbNewLine _
        & "      , MG.STD_IRIME_UT                         AS STD_IRIME_UT                      " & vbNewLine _
        & "      , MG.STD_WT_KGS                           AS STD_WT_KGS                        " & vbNewLine _
        & "      , MG.TARE_YN                              AS TARE_YN                           " & vbNewLine _
        & "      , MG.ALCTD_KB                             AS ALCTD_KB                          " & vbNewLine _
        & "      , MG.COA_YN                               AS COA_YN                            " & vbNewLine _
        & "      , ZT.INKA_NO_L                            AS INKA_NO_L                         " & vbNewLine _
        & "      , ZT.INKA_NO_M                            AS INKA_NO_M                         " & vbNewLine _
        & "      , ZT.INKA_NO_S                            AS INKA_NO_S                         " & vbNewLine _
        & "      , ZT.ALLOC_PRIORITY                       AS ALLOC_PRIORITY                    " & vbNewLine _
        & "      , ZT.RSV_NO                               AS RSV_NO                            " & vbNewLine _
        & "      , ZT.SERIAL_NO                            AS SERIAL_NO                         " & vbNewLine _
        & "      , ZT.HOKAN_YN                             AS HOKAN_YN                          " & vbNewLine _
        & "      , ZT.TAX_KB                               AS TAX_KB                            " & vbNewLine _
        & "      , ZT.GOODS_COND_KB_1                      AS GOODS_COND_KB_1                   " & vbNewLine _
        & "      , ZT.GOODS_COND_KB_2                      AS GOODS_COND_KB_2                   " & vbNewLine _
        & "      , ZT.GOODS_COND_KB_3                      AS GOODS_COND_KB_3                   " & vbNewLine _
        & "      , ZT.OFB_KB                               AS OFB_KB                            " & vbNewLine _
        & "      , ZT.SPD_KB                               AS SPD_KB                            " & vbNewLine _
        & "      , ZT.REMARK_OUT                           AS REMARK_OUT                        " & vbNewLine _
        & "      , ZT.PORA_ZAI_NB                          AS PORA_ZAI_NB                       " & vbNewLine _
        & "      , ZT.ALCTD_NB                             AS ALCTD_NB                          " & vbNewLine _
        & "      , ZT.ALLOC_CAN_NB                         AS ALLOC_CAN_NB                      " & vbNewLine _
        & "      , ZT.IRIME                                AS IRIME                             " & vbNewLine _
        & "      , ZT.PORA_ZAI_QT                          AS PORA_ZAI_QT                       " & vbNewLine _
        & "      , ZT.ALCTD_QT                             AS ALCTD_QT                          " & vbNewLine _
        & "      , ZT.ALLOC_CAN_QT                         AS ALLOC_CAN_QT                      " & vbNewLine _
        & "      , ZT.INKO_DATE                            AS INKO_DATE                         " & vbNewLine _
        & "      , ZT.INKO_PLAN_DATE                       AS INKO_PLAN_DATE                    " & vbNewLine _
        & "      , ZT.ZERO_FLAG                            AS ZERO_FLAG                         " & vbNewLine _
        & "      , ZT.LT_DATE                              AS LT_DATE                           " & vbNewLine _
        & "      , ZT.GOODS_CRT_DATE                       AS GOODS_CRT_DATE                    " & vbNewLine _
        & "      , ZT.DEST_CD_P                            AS DEST_CD_P                         " & vbNewLine _
        & "      , ZT.REMARK                               AS REMARK                            " & vbNewLine _
        & "      , ZT.SMPL_FLAG                            AS SMPL_FLAG                         " & vbNewLine _
        & "      , ZT.SYS_UPD_DATE                         AS SYS_UPD_DATE                      " & vbNewLine _
        & "      , ZT.SYS_UPD_TIME                         AS SYS_UPD_TIME                      " & vbNewLine _
        & "   FROM $LM_TRN$..D_ZAI_TRS AS ZT                                                    " & vbNewLine _
        & "   LEFT JOIN $LM_MST$..M_GOODS AS MG                                                 " & vbNewLine _
        & "     ON MG.NRS_BR_CD = ZT.NRS_BR_CD                                                  " & vbNewLine _
        & "    AND MG.GOODS_CD_NRS = ZT.GOODS_CD_NRS                                            " & vbNewLine _
        & "   LEFT JOIN                                                                         " & vbNewLine _
        & "        (SELECT EL.NRS_BR_CD                                                         " & vbNewLine _
        & "              , EM.EDI_CTL_NO                                                        " & vbNewLine _
        & "              , EM.EDI_CTL_NO_CHU                                                    " & vbNewLine _
        & "              , EL.OUTKA_FROM_ORD_NO                                                 " & vbNewLine _
        & "              , EL.BUYER_ORD_NO                                                      " & vbNewLine _
        & "              , EL.INKA_CTL_NO_L                                                     " & vbNewLine _
        & "              , EL.INKA_STATE_KB                                                     " & vbNewLine _
        & "              , EM.CUST_GOODS_CD AS GOODS_CD_CUST                                    " & vbNewLine _
        & "              , EM.NRS_GOODS_CD  AS GOODS_CD_NRS                                     " & vbNewLine _
        & "              , EM.LOT_NO                                                            " & vbNewLine _
        & "              , EM.IRIME                                                             " & vbNewLine _
        & "              , EM.IRIME_UT                                                          " & vbNewLine _
        & "              , EM.NB                                                                " & vbNewLine _
        & "              , ZT.INKA_NO_L                                                         " & vbNewLine _
        & "           FROM $LM_TRN$..H_INKAEDI_L AS EL                                          " & vbNewLine _
        & "           LEFT JOIN                                                                 " & vbNewLine _
        & "                $LM_TRN$..H_INKAEDI_M AS EM                                          " & vbNewLine _
        & "             ON EM.NRS_BR_CD = EL.NRS_BR_CD                                          " & vbNewLine _
        & "            AND EM.EDI_CTL_NO = EL.EDI_CTL_NO                                        " & vbNewLine _
        & "            AND EM.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
        & "           LEFT JOIN                                                                 " & vbNewLine _
        & "                $LM_TRN$..B_INKA_L AS BL                                             " & vbNewLine _
        & "             ON BL.NRS_BR_CD   = EL.NRS_BR_CD                                        " & vbNewLine _
        & "            AND BL.INKA_NO_L   = EL.INKA_CTL_NO_L                                    " & vbNewLine _
        & "            AND BL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
        & "           LEFT JOIN                                                                 " & vbNewLine _
        & "                (SELECT ZT.NRS_BR_CD                                                 " & vbNewLine _
        & "                      , ZT.INKA_NO_L                                                 " & vbNewLine _
        & "                      , ZT.GOODS_CD_NRS                                              " & vbNewLine _
        & "                      , ZT.IRIME                                                     " & vbNewLine _
        & "                      , ZT.LOT_NO                                                    " & vbNewLine _
        & "                      , ZT.GOODS_COND_KB_3                                           " & vbNewLine _
        & "                      , ZT.OFB_KB                                                    " & vbNewLine _
        & "                      , ZT.SPD_KB                                                    " & vbNewLine _
        & "                      , SUM(ZT.ALLOC_CAN_NB) AS ALLOC_CAN_NB                         " & vbNewLine _
        & "                   FROM $LM_TRN$..D_ZAI_TRS AS ZT                                    " & vbNewLine _
        & "                  WHERE ZT.NRS_BR_CD       = @NRS_BR_CD                              " & vbNewLine _
        & "                    AND ZT.CUST_CD_L       = @CUST_CD_L                              " & vbNewLine _
        & "                    AND ZT.CUST_CD_M       = @CUST_CD_M                              " & vbNewLine _
        & "                    AND ZT.GOODS_COND_KB_3 = '00'  -- M品                            " & vbNewLine _
        & "                    AND ZT.OFB_KB          = '02'  -- 簿外品                         " & vbNewLine _
        & "                    AND ZT.SPD_KB          = '01'  -- 出荷可能                       " & vbNewLine _
        & "                    AND ZT.SYS_DEL_FLG     = '0'                                     " & vbNewLine _
        & "                  GROUP BY                                                           " & vbNewLine _
        & "                        ZT.NRS_BR_CD                                                 " & vbNewLine _
        & "                      , ZT.INKA_NO_L                                                 " & vbNewLine _
        & "                      , ZT.GOODS_CD_NRS                                              " & vbNewLine _
        & "                      , ZT.IRIME                                                     " & vbNewLine _
        & "                      , ZT.LOT_NO                                                    " & vbNewLine _
        & "                      , ZT.GOODS_COND_KB_3                                           " & vbNewLine _
        & "                      , ZT.OFB_KB                                                    " & vbNewLine _
        & "                      , ZT.SPD_KB                                                    " & vbNewLine _
        & "                ) AS ZT                                                              " & vbNewLine _
        & "             ON ZT.NRS_BR_CD    = EM.NRS_BR_CD                                       " & vbNewLine _
        & "            AND ZT.GOODS_CD_NRS = EM.NRS_GOODS_CD                                    " & vbNewLine _
        & "            AND ZT.IRIME        = EM.IRIME                                           " & vbNewLine _
        & "            AND ZT.LOT_NO       = EM.LOT_NO                                          " & vbNewLine _
        & "            AND ZT.ALLOC_CAN_NB = EM.NB                                              " & vbNewLine _
        & "          WHERE EL.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
        & "            AND EL.DEL_KB      = '0'                                                 " & vbNewLine _
        & "            AND EL.NRS_BR_CD   = @NRS_BR_CD                                          " & vbNewLine _
        & "            AND EL.CUST_CD_L   = @CUST_CD_L                                          " & vbNewLine _
        & "            AND EL.CUST_CD_M   = @CUST_CD_M                                          " & vbNewLine _
        & "            AND BL.INKA_NO_L IS NOT NULL                                             " & vbNewLine _
        & "            AND ZT.NRS_BR_CD IS NOT NULL) AS EDI                                     " & vbNewLine _
        & "     ON EDI.NRS_BR_CD    = ZT.NRS_BR_CD                                              " & vbNewLine _
        & "    AND EDI.GOODS_CD_NRS = ZT.GOODS_CD_NRS                                           " & vbNewLine _
        & "    AND EDI.LOT_NO       = ZT.LOT_NO                                                 " & vbNewLine _
        & "    AND EDI.IRIME        = ZT.IRIME                                                  " & vbNewLine _
        & "    AND EDI.INKA_NO_L    = ZT.INKA_NO_L                                              " & vbNewLine _
        & "  WHERE ZT.NRS_BR_CD       = @NRS_BR_CD                                              " & vbNewLine _
        & "    AND ZT.CUST_CD_L       = @CUST_CD_L                                              " & vbNewLine _
        & "    AND ZT.CUST_CD_M       = @CUST_CD_M                                              " & vbNewLine _
        & "    AND ZT.GOODS_COND_KB_3 = '00'  -- M品                                            " & vbNewLine _
        & "    AND ZT.OFB_KB          = '02'  -- 簿外品                                         " & vbNewLine _
        & "    AND ZT.SPD_KB          = '01'  -- 出荷可能                                       " & vbNewLine _
        & "    AND ZT.SYS_DEL_FLG     = '0'                                                     " & vbNewLine _
        & "    AND EDI.NRS_BR_CD IS NOT NULL                                                    " & vbNewLine _
        & "    AND ZT.ALLOC_CAN_NB > 0                                                          " & vbNewLine _
        & "    AND EDI.INKA_CTL_NO_L <> ZT.INKA_NO_L                                            " & vbNewLine _
        & "    AND LEN(ZT.INKO_DATE) > 0                                                        " & vbNewLine _
        & "    AND EDI_CTL_NO         = @EDI_CTL_NO                                             " & vbNewLine _
        & "  ORDER BY                                                                           " & vbNewLine _
        & "        EDI.EDI_CTL_NO DESC                                                          " & vbNewLine _
        & "      , EDI.EDI_CTL_NO_CHU                                                           " & vbNewLine

#End Region

#Region "M品届先取得(振替)"


    Private Const SQL_SELECT_M_DEST_COND_M As String _
        = " SELECT MD.NRS_BR_CD     AS NRS_BR_CD                                       " & vbNewLine _
        & "      , MD.CUST_CD_L     AS CUST_CD_L                                       " & vbNewLine _
        & "      , MD.DEST_CD       AS DEST_CD                                         " & vbNewLine _
        & "      , MD.DEST_NM       AS DEST_NM                                         " & vbNewLine _
        & "      , MD.AD_1          AS AD_1                                            " & vbNewLine _
        & "      , MD.AD_2          AS AD_2                                            " & vbNewLine _
        & "      , MD.SP_NHS_KB     AS SP_NHS_KB                                       " & vbNewLine _
        & "      , MD.COA_YN        AS COA_YN                                          " & vbNewLine _
        & "      , CASE WHEN LEN(MD.BIN_KB) > 0                                        " & vbNewLine _
        & "             THEN MD.BIN_KB                                                 " & vbNewLine _
        & "             ELSE '01'                                                      " & vbNewLine _
        & "        END              AS BIN_KB                                          " & vbNewLine _
        & "      , MD.SP_UNSO_CD    AS UNSO_CD                                         " & vbNewLine _
        & "      , MD.SP_UNSO_BR_CD AS UNSO_BR_CD                                      " & vbNewLine _
        & "   FROM $LM_MST$..M_DEST AS MD                                              " & vbNewLine _
        & "   LEFT JOIN                                                                " & vbNewLine _
        & "        $LM_MST$..Z_KBN  AS Z                                               " & vbNewLine _
        & "     ON Z.KBN_GROUP_CD = 'M027'                                             " & vbNewLine _
        & "    AND Z.KBN_NM1      = MD.NRS_BR_CD                                       " & vbNewLine _
        & "    AND Z.KBN_NM4      = MD.DEST_CD                                         " & vbNewLine _
        & "  WHERE Z.KBN_NM1 = @NRS_BR_CD                                              " & vbNewLine _
        & "    AND Z.KBN_NM2 = @CUST_CD_L                                              " & vbNewLine _
        & "    AND Z.KBN_NM3 = @CUST_CD_M                                              " & vbNewLine

#End Region


#Region "B_INKA_L"
    Private Const SQL_SELECT_INKA_L As String _
        = " SELECT BL.*                             " & vbNewLine _
        & "   FROM $LM_TRN$..B_INKA_L AS BL         " & vbNewLine _
        & "  WHERE BL.SYS_DEL_FLG = '0'             " & vbNewLine _
        & "    AND BL.NRS_BR_CD   = @NRS_BR_CD      " & vbNewLine _
        & "    AND BL.INKA_NO_L   = @INKA_NO_L      " & vbNewLine
#End Region


#Region "B_INKA_M"
    Private Const SQL_SELECT_INKA_M As String _
        = " SELECT BM.NRS_BR_CD                     " & vbNewLine _
        & "      , BM.INKA_NO_L                     " & vbNewLine _
        & "      , BM.INKA_NO_M                     " & vbNewLine _
        & "      , BM.GOODS_CD_NRS                  " & vbNewLine _
        & "      , BM.OUTKA_FROM_ORD_NO_M           " & vbNewLine _
        & "      , BM.BUYER_ORD_NO_M                " & vbNewLine _
        & "      , BM.REMARK                        " & vbNewLine _
        & "      , BM.PRINT_SORT                    " & vbNewLine _
        & "      , BM.SYS_UPD_DATE                  " & vbNewLine _
        & "      , BM.SYS_UPD_TIME                  " & vbNewLine _
        & "   FROM $LM_TRN$..B_INKA_M AS BM         " & vbNewLine _
        & "  WHERE BM.SYS_DEL_FLG = '0'             " & vbNewLine _
        & "    AND BM.NRS_BR_CD   = @NRS_BR_CD      " & vbNewLine _
        & "    AND BM.INKA_NO_L   = @INKA_NO_L      " & vbNewLine

#End Region

#Region "B_INKA_S"
    Private Const SQL_SELECT_MAX_INKA_S_NO As String _
        = " SELECT NRS_BR_CD      AS NRS_BR_CD  " & vbNewLine _
        & "      , INKA_NO_L      AS INKA_NO_L  " & vbNewLine _
        & "      , INKA_NO_M      AS INKA_NO_M  " & vbNewLine _
        & "      , MAX(INKA_NO_S) AS INKA_NO_S  " & vbNewLine _
        & "   FROM $LM_TRN$..B_INKA_S           " & vbNewLine _
        & "  WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "    AND INKA_NO_L = @INKA_NO_L       " & vbNewLine _
        & "  GROUP BY                           " & vbNewLine _
        & "        NRS_BR_CD                    " & vbNewLine _
        & "      , INKA_NO_L                    " & vbNewLine _
        & "      , INKA_NO_M                    " & vbNewLine
#End Region

#Region "セミEDI処理"

#Region "M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)"

    Private Const SQL_SELECT_GOODS_CNT As String _
        = " SELECT                                                     " & vbNewLine _
        & "     COUNT(*)             AS  MST_CNT                       " & vbNewLine _
        & " FROM                                                       " & vbNewLine _
        & "     $LM_MST$..M_GOODS    AS  MG                            " & vbNewLine _
        & " WHERE                                                      " & vbNewLine _
        & "     MG.SYS_DEL_FLG    = '0'                                " & vbNewLine _
        & " AND MG.NRS_BR_CD      = @NRS_BR_CD                         " & vbNewLine _
        & " AND MG.CUST_CD_L      = @CUST_CD_L                         " & vbNewLine _
        & " AND MG.CUST_CD_M      = @CUST_CD_M                         " & vbNewLine _
        & " AND MG.GOODS_CD_CUST  = @GOODS_CD_CUST                          " & vbNewLine

#End Region ' "M_GOODS(セミEDI時)"

#Region "EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT SQL"

    ''' <summary>
    ''' EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT SQL
    ''' </summary>
    Private Const SQL_SELECT_INKA_NO_L_AND_CNT As String = "" _
        & "SELECT                                                     " & vbNewLine _
        & "      COUNT(*) AS INKA_CNT                                 " & vbNewLine _
        & "    , MAX(B_INKA_L.INKA_NO_L) AS INKA_NO_L                 " & vbNewLine _
        & "    , MAX(H_INKAEDI_L.EDI_CTL_NO) AS EDI_CTL_NO            " & vbNewLine _
        & "FROM                                                       " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_L                                  " & vbNewLine _
        & "LEFT JOIN                                                  " & vbNewLine _
        & "    $LM_TRN$..B_INKA_L                                     " & vbNewLine _
        & "        ON  B_INKA_L.NRS_BR_CD = H_INKAEDI_L.NRS_BR_CD     " & vbNewLine _
        & "        AND B_INKA_L.INKA_NO_L = H_INKAEDI_L.INKA_CTL_NO_L " & vbNewLine _
        & "        AND B_INKA_L.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & "WHERE                                                      " & vbNewLine _
        & "    H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
        & "AND H_INKAEDI_L.CUST_CD_L = @CUST_CD_L                     " & vbNewLine _
        & "AND H_INKAEDI_L.CUST_CD_M = @CUST_CD_M                     " & vbNewLine _
        & "AND(   (H_INKAEDI_L.FREE_C02 =      'K'       AND H_INKAEDI_L.OUTKA_FROM_ORD_NO LIKE (@HACCHU_DENP_NO + '-' + @HACCHU_DENP_DTL_NO + '-' + '%')) " & vbNewLine _
        & "    OR (H_INKAEDI_L.FREE_C02 NOT IN('K', 'H') AND H_INKAEDI_L.OUTKA_FROM_ORD_NO =     @HACCHU_DENP_NO + '-' + @HACCHU_DENP_DTL_NO) " & vbNewLine _
        & "    OR (H_INKAEDI_L.FREE_C02 =           'H'  AND H_INKAEDI_L.OUTKA_FROM_ORD_NO =     @IO_DENP_NO)) " & vbNewLine _
        & "AND H_INKAEDI_L.SYS_DEL_FLG = '0'                          " & vbNewLine

#End Region ' "EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT SQL"

#Region "EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 SQL"

    ''' <summary>
    ''' EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 SQL
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_SELECT_INKAEDI_DTL_DATA_ID_DETAIL_DENP_NO_AND_DTL_NO As String = "" _
        & " SELECT                               " & vbNewLine _
        & "       CRT_DATE                       " & vbNewLine _
        & "     , FILE_NAME                      " & vbNewLine _
        & "     , REC_NO                         " & vbNewLine _
        & "     , GYO                            " & vbNewLine _
        & "     , DATA_ID_DETAIL                 " & vbNewLine _
        & "     , HACCHU_DENP_NO                 " & vbNewLine _
        & "     , HACCHU_DENP_DTL_NO             " & vbNewLine _
        & "     , IO_DENP_NO                     " & vbNewLine _
        & " FROM                                 " & vbNewLine _
        & "     $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW " & vbNewLine _
        & " WHERE                                " & vbNewLine _
        & "     CRT_DATE  = @CRT_DATE            " & vbNewLine _
        & " AND FILE_NAME = @FILE_NAME           " & vbNewLine _
        & " AND REC_NO    = @REC_NO              " & vbNewLine _
        & " AND GYO       = @GYO                 " & vbNewLine

#End Region ' "EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 SQL"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出)"

    Private Const SQL_SELECT_INKAEDI_DTL_NCGO_NEW_CANCEL As String = "  SELECT                          " & vbNewLine _
                                    & " --  CHKNCG.HACCHU_DENP_NO    AS  HACCHU_DENP_NO                   " & vbNewLine _
                                    & "   CHKNCG.EDI_CTL_NO        AS  EDI_CTL_NO                      " & vbNewLine _
                                    & " FROM                                                           " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW INKADTL                       " & vbNewLine _
                                    & " --取消対象抽出なのでINNER JOINでOK                             " & vbNewLine _
                                    & " INNER JOIN  $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW CHKNCG            " & vbNewLine _
                                    & "   ON CHKNCG.NRS_BR_CD       =  @NRS_BR_CD                      " & vbNewLine _
                                    & "  AND CHKNCG.CUST_CD_L       =  @CUST_CD_L                      " & vbNewLine _
                                    & "  AND CHKNCG.CUST_CD_M       =  @CUST_CD_M                      " & vbNewLine _
                                    & "  AND ( (CHKNCG.SYS_ENT_DATE   <> INKADTL.SYS_ENT_DATE          " & vbNewLine _
                                    & "      OR CHKNCG.SYS_ENT_TIME   <> INKADTL.SYS_ENT_TIME)         " & vbNewLine _
                                    & "    OR (CHKNCG.SYS_ENT_DATE  = INKADTL.SYS_ENT_DATE             " & vbNewLine _
                                    & "     AND CHKNCG.SYS_ENT_TIME = INKADTL.SYS_ENT_TIME             " & vbNewLine _
                                    & "     AND CHKNCG.REC_NO < INKADTL.REC_NO ))                      " & vbNewLine _
                                    & "  AND CHKNCG.DEL_KB         = '0'                               " & vbNewLine _
                                    & "  AND CHKNCG.HACCHU_DENP_NO     = INKADTL.HACCHU_DENP_NO        " & vbNewLine _
                                    & "  AND CHKNCG.HACCHU_DENP_DTL_NO = INKADTL.HACCHU_DENP_DTL_NO    " & vbNewLine _
                                    & "  AND CHKNCG.RENBAN             = INKADTL.RENBAN                " & vbNewLine _
                                    & "  AND RIGHT(CHKNCG.INKA_CTL_NO_L,8)   = '00000000'  --入荷未登録 " & vbNewLine _
                                    & "  AND CHKNCG.DATA_ID_DETAIL = 'K'   --転送・入荷                " & vbNewLine _
                                    & " WHERE                                                          " & vbNewLine _
                                    & "       INKADTL.NRS_BR_CD      =  @NRS_BR_CD                   " & vbNewLine _
                                    & "   AND INKADTL.CUST_CD_L      =  @CUST_CD_L                   " & vbNewLine _
                                    & "   AND INKADTL.CUST_CD_M      =  @CUST_CD_M                   " & vbNewLine _
                                    & "   AND INKADTL.SYS_ENT_DATE   =  @SYS_ENT_DATE                " & vbNewLine _
                                    & "   AND INKADTL.SYS_ENT_TIME   =  @SYS_ENT_TIME                " & vbNewLine _
                                    & "   AND INKADTL.DEL_KB         =  '3'                          " & vbNewLine _
                                    & "   AND INKADTL.DATA_ID_DETAIL = 'K'   --購入・入荷            " & vbNewLine _
                                    & "  GROUP BY CHKNCG.EDI_CTL_NO                                  " & vbNewLine _
                                    & "  UNION ALL                                                    " & vbNewLine _
                                    & "  SELECT                                                        " & vbNewLine _
                                    & " --  CHKNCG.OUTKA_DENP_NO    AS  OUTKA_DENP_NO                   " & vbNewLine _
                                    & "   CHKNCG.EDI_CTL_NO        AS  EDI_CTL_NO                      " & vbNewLine _
                                    & " FROM                                                           " & vbNewLine _
                                    & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW INKADTL                       " & vbNewLine _
                                    & " --取消対象抽出なのでINNER JOINでOK                             " & vbNewLine _
                                    & " INNER JOIN  $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW CHKNCG            " & vbNewLine _
                                    & "   ON CHKNCG.NRS_BR_CD       =  @NRS_BR_CD                      " & vbNewLine _
                                    & "  AND CHKNCG.CUST_CD_L       =  @CUST_CD_L                      " & vbNewLine _
                                    & "  AND CHKNCG.CUST_CD_M       =  @CUST_CD_M                      " & vbNewLine _
                                    & "  AND ( (CHKNCG.SYS_ENT_DATE   <> INKADTL.SYS_ENT_DATE          " & vbNewLine _
                                    & "      OR CHKNCG.SYS_ENT_TIME   <> INKADTL.SYS_ENT_TIME)         " & vbNewLine _
                                    & "    OR (CHKNCG.SYS_ENT_DATE  = INKADTL.SYS_ENT_DATE             " & vbNewLine _
                                    & "     AND CHKNCG.SYS_ENT_TIME = INKADTL.SYS_ENT_TIME             " & vbNewLine _
                                    & "     AND CHKNCG.REC_NO < INKADTL.REC_NO ))                      " & vbNewLine _
                                    & "  AND CHKNCG.DEL_KB         = '0'                               " & vbNewLine _
                                    & "  AND CHKNCG.OUTKA_DENP_NO     = INKADTL.OUTKA_DENP_NO          " & vbNewLine _
                                    & "  AND CHKNCG.OUTKA_DENP_DTL_NO = INKADTL.OUTKA_DENP_DTL_NO　    " & vbNewLine _
                                    & "  AND RIGHT(CHKNCG.INKA_CTL_NO_L,8)   = '00000000'  --入荷未登録 " & vbNewLine _
                                    & "  AND CHKNCG.DATA_ID_DETAIL    <> 'K'   --転送・入荷以外        " & vbNewLine _
                                    & " WHERE                                                          " & vbNewLine _
                                    & "       INKADTL.NRS_BR_CD      =  @NRS_BR_CD                   " & vbNewLine _
                                    & "   AND INKADTL.CUST_CD_L      =  @CUST_CD_L                   " & vbNewLine _
                                    & "   AND INKADTL.CUST_CD_M      =  @CUST_CD_M                   " & vbNewLine _
                                    & "   AND INKADTL.SYS_ENT_DATE   =  @SYS_ENT_DATE                " & vbNewLine _
                                    & "   AND INKADTL.SYS_ENT_TIME   =  @SYS_ENT_TIME                " & vbNewLine _
                                    & "   AND INKADTL.DEL_KB         =  '3'                          " & vbNewLine _
                                    & "   AND INKADTL.DATA_ID_DETAIL <> 'K'   --購入・入荷以外       " & vbNewLine _
                                    & "  GROUP BY CHKNCG.EDI_CTL_NO                                  " & vbNewLine

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出)"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大) 登録用)"

    Private Const SQL_SELECT_FOR_INKAEDI_L_FROM_INKAEDI_DTL_NCGO_NEW As String = "  SELECT DISTINCT                          " & vbNewLine _
                                                            & "--       CASE WHEN INNCG.INPUT_KBN   = '4'    --取消(入荷済み)  　" & vbNewLine _
                                                            & "       CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消(入荷済み)  　" & vbNewLine _
                                                            & "            THEN '0'                                            " & vbNewLine _
                                                            & "            ELSE CASE WHEN LEN(HIL.INKA_CTL_NO_L) > 0 THEN  '3'  --保留 " & vbNewLine _
                                                            & "                      ELSE INNCG.DEL_KB  END                    " & vbNewLine _
                                                            & "       END  AS   DEL_KB                                         " & vbNewLine _
                                                            & "      ,INNCG.NRS_BR_CD	  AS  NRS_BR_CD                   " & vbNewLine _
                                                            & "      ,INNCG.EDI_CTL_NO	  AS  EDI_CTL_NO                  " & vbNewLine _
                                                            & "      ,''                  AS  INKA_CTL_NO_L               " & vbNewLine _
                                                            & "      ,'10'                AS  INKA_TP                     " & vbNewLine _
                                                            & "      ,'10'                AS  INKA_KB                     " & vbNewLine _
                                                            & "      ,'10'                AS  INKA_STATE_KB               " & vbNewLine _
                                                            & "      ,INNCG.SEIRI_DATE    AS  INKA_DATE                   " & vbNewLine _
                                                            & "      ,'0900'                  AS  INKA_TIME                   " & vbNewLine _
                                                            & "--2018/02/13      ,@WH_CD              AS  NRS_WH_CD                   " & vbNewLine _
                                                            & "      ,ISNULL(KBN.KBN_NM2, @WH_CD)     AS  NRS_WH_CD       " & vbNewLine _
                                                            & "      ,INNCG.CUST_CD_L     AS  CUST_CD_L                   " & vbNewLine _
                                                            & "      ,INNCG.CUST_CD_M     AS  CUST_CD_M                   " & vbNewLine _
                                                            & "      ,''                  AS  CUST_NM_L                   " & vbNewLine _
                                                            & "      ,''                  AS  CUST_NM_M                   " & vbNewLine _
                                                            & "      ,0                   AS  INKA_PLAN_QT                " & vbNewLine _
                                                            & "      ,''                  AS  INKA_PLAN_QT_UT             " & vbNewLine _
                                                            & "      ,0                   AS  INKA_TTL_NB                 " & vbNewLine _
                                                            & "      ,'01'                AS  NAIGAI_KB                   " & vbNewLine _
                                                            & " --     ,INNCG.OUTKA_DENP_NO     AS  BUYER_ORD_NO            " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DATA_ID_DETAIL = 'K'                              " & vbNewLine _
                                                            & "            THEN INNCG.HACCHU_DENP_NO  + '-' + INNCG.HACCHU_DENP_DTL_NO  " & vbNewLine _
                                                            & "              + '-' + FORMAT(INNCG.RENBAN,'0000')                        " & vbNewLine _
                                                            & "            ELSE INNCG.OUTKA_DENP_NO  + '-' + INNCG.OUTKA_DENP_DTL_NO    " & vbNewLine _
                                                            & "                         END    AS  BUYER_ORD_NO                         " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DATA_ID_DETAIL = 'K'                              " & vbNewLine _
                                                            & "            THEN INNCG.HACCHU_DENP_NO  + '-' + INNCG.HACCHU_DENP_DTL_NO  " & vbNewLine _
                                                            & "              + '-' + FORMAT(INNCG.RENBAN,'0000')                        " & vbNewLine _
                                                            & "            ELSE INNCG.HACCHU_DENP_NO  + '-' + INNCG.HACCHU_DENP_DTL_NO   " & vbNewLine _
                                                            & "                         END    AS  OUTKA_FROM_ORD_NO                     " & vbNewLine _
                                                            & "      ,''                  AS  SEIQTO_CD                   " & vbNewLine _
                                                            & "      ,'1'                 AS  TOUKI_HOKAN_YN              " & vbNewLine _
                                                            & "      ,'1'                 AS  HOKAN_YN                    " & vbNewLine _
                                                            & "      ,'0'                 AS  HOKAN_FREE_KIKAN            " & vbNewLine _
                                                            & "      ,INNCG.SEIRI_DATE    AS  HOKAN_STR_DATE              " & vbNewLine _
                                                            & "      ,'1'                 AS  NIYAKU_YN                   " & vbNewLine _
                                                            & "      ,'01'                AS  TAX_KB                      " & vbNewLine _
                                                            & "      ,''                  AS  REMARK                      " & vbNewLine _
                                                            & "      ,''                  AS  NYUBAN_L                    " & vbNewLine _
                                                            & "      ,'90'                AS  UNCHIN_TP                   " & vbNewLine _
                                                            & "      ,''                  AS  UNCHIN_KB                   " & vbNewLine _
                                                            & "      ,''                  AS  OUTKA_MOTO                  " & vbNewLine _
                                                            & "      ,''                  AS  SYARYO_KB                   " & vbNewLine _
                                                            & "      ,''                  AS  UNSO_ONDO_KB                " & vbNewLine _
                                                            & "      ,''                  AS  UNSO_CD                     " & vbNewLine _
                                                            & "      ,''                  AS  UNSO_BR_CD                  " & vbNewLine _
                                                            & "      ,'0'                 AS  UNCHIN                      " & vbNewLine _
                                                            & "      ,''                  AS  YOKO_TARIFF_CD              " & vbNewLine _
                                                            & "      ,'0'                 AS  OUT_FLAG                    " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DEL_KB = '3' THEN '1' ELSE '0' END AS  AKAKURO_KB                  " & vbNewLine _
                                                            & "      ,'9'                 AS  JISSEKI_FLAG                " & vbNewLine _
                                                            & "      ,''                  AS  JISSEKI_USER                " & vbNewLine _
                                                            & "      ,''                  AS  JISSEKI_DATE                " & vbNewLine _
                                                            & "      ,''                  AS  JISSEKI_TIME                " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N01                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N02                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N03                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N04                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N05                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N06                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N07                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N08                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N09                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N10                    " & vbNewLine _
                                                            & "      ,INNCG.DATA_ID_AREA  AS  FREE_C01                    " & vbNewLine _
                                                            & "      ,INNCG.DATA_ID_DETAIL AS FREE_C02                    " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DATA_ID_DETAIL = 'K'                                                                   " & vbNewLine _
                                                            & "            THEN INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN)  " & vbNewLine _
                                                            & "            ELSE INNCG.OUTKA_DENP_NO       END    AS  FREE_C03                                                " & vbNewLine _
                                                            & "      ,INNCG.INPUT_KBN     AS  FREE_C04                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C05                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C06                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C07                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C08                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C09                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C10                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C11                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C12                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C13                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C14                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C15                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C16                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C17                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C18                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C19                    " & vbNewLine _
                                                            & "      ,INNCG.CUST_CD_L + INNCG.CUST_CD_M  AS  FREE_C20     " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C21                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C22                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C23                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C24                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C25                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C26                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C27                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C28                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C29                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C30                    " & vbNewLine _
                                                            & "      ,INNCG.UPD_USER      AS  CRT_USER                    " & vbNewLine _
                                                            & "      ,INNCG.UPD_DATE     AS  CRT_DATE                    " & vbNewLine _
                                                            & "      ,INNCG.UPD_TIME      AS  CRT_TIME                    " & vbNewLine _
                                                            & "      ,''                  AS  UPD_USER                    " & vbNewLine _
                                                            & "      ,''                  AS  UPD_DATE                    " & vbNewLine _
                                                            & "      ,''                  AS  UPD_TIME                    " & vbNewLine _
                                                            & "      ,''                  AS  EDIT_FLAG                   " & vbNewLine _
                                                            & "      ,''                  AS  MATCHING_FLAG               " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_DATE  AS  SYS_ENT_DATE                " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_TIME  AS  SYS_ENT_TIME                " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_PGID  AS  SYS_ENT_PGID                " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_USER  AS  SYS_ENT_USER                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_DATE  AS  SYS_UPD_DATE                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_TIME  AS  SYS_UPD_TIME                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_PGID  AS  SYS_UPD_PGID                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_USER  AS  SYS_UPD_USER                " & vbNewLine _
                                                            & "--      ,CASE WHEN INNCG.INPUT_KBN   = '4'    --取消         " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消      " & vbNewLine _
                                                            & "            THEN '1'                                       " & vbNewLine _
                                                            & "            ELSE INNCG.SYS_DEL_FLG  END  AS   SYS_DEL_FLG   " & vbNewLine _
                                                            & "  FROM                                                     " & vbNewLine _
                                                            & "      $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW INNCG               " & vbNewLine _
                                                            & "      --荷主マスタ                                         " & vbNewLine _
                                                            & "      LEFT JOIN $LM_MST$..M_CUST MC                        " & vbNewLine _
                                                            & "      on INNCG.NRS_BR_CD  = MC.NRS_BR_CD                   " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L = MC.CUST_CD_L                   " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M = MC.CUST_CD_M                   " & vbNewLine _
                                                            & "      AND MC.CUST_CD_S = '00' AND MC.CUST_CD_SS = '00'     " & vbNewLine _
                                                            & "      --H_INKAEDI_L(既に入荷済み場合は保留にする)      　  " & vbNewLine _
                                                            & "      LEFT JOIN $LM_TRN$..H_INKAEDI_L HIL                  " & vbNewLine _
                                                            & "      on  INNCG.NRS_BR_CD     = HIL.NRS_BR_CD              " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L     = HIL.CUST_CD_L              " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M     = HIL.CUST_CD_M              " & vbNewLine _
                                                            & "      AND HIL.FREE_C03 = CASE WHEN INNCG.DATA_ID_DETAIL = 'K' THEN                                                           " & vbNewLine _
                                                            & "                                   INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN) " & vbNewLine _
                                                            & "                              ELSE INNCG.OUTKA_DENP_NO    END                                                                  " & vbNewLine _
                                                            & "      AND HIL.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                                            & "      AND HIL.DEL_KB              = '0'                    " & vbNewLine _
                                                            & "      --区分マスタ        保管場所から倉庫CD取得     　    " & vbNewLine _
                                                            & "      LEFT JOIN $LM_MST$..Z_KBN  KBN                       " & vbNewLine _
                                                            & "      on   KBN.KBN_GROUP_CD =  'H026'                      " & vbNewLine _
                                                            & "      AND  KBN.KBN_NM1      =  INNCG.INKA_HOKAN_BASYO      " & vbNewLine _
                                                            & "      AND  KBN.SYS_DEL_FLG  =  '0'                         " & vbNewLine _
                                                            & "  WHERE                                                    " & vbNewLine _
                                                            & "          INNCG.NRS_BR_CD    = @NRS_BR_CD                  " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L    = @CUST_CD_L                  " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M    = @CUST_CD_M                  " & vbNewLine _
                                                            & "      AND INNCG.SYS_ENT_DATE = @SYS_ENT_DATE               " & vbNewLine _
                                                            & "      AND INNCG.SYS_ENT_TIME = @SYS_ENT_TIME               " & vbNewLine _
                                                            & "      AND INNCG.EDI_CTL_NO_CHU = '001'                     " & vbNewLine _
                                                            & "      AND ((INNCG.DEL_KB        = '0' )                       " & vbNewLine _
                                                            & "      -- 取消で入荷ずみも抽出　                               " & vbNewLine _
                                                            & "       OR (INNCG.DEL_KB        = '3'                          " & vbNewLine _
                                                            & " --      AND INNCG.INPUT_KBN      = '4'    --取消　         　  " & vbNewLine _
                                                            & "       AND LEN(HIL.INKA_CTL_NO_L)    > 0    ))  --入荷済み    " & vbNewLine _
                                                            & "      AND INNCG.DATA_ID_AREA <> '201' -- 出荷返品以外(入荷) " & vbNewLine _
                                                            & "  UNION ALL                                                 " & vbNewLine _
                                                            & "  SELECT                                                    " & vbNewLine _
                                                            & "--       CASE WHEN INNCG.INPUT_KBN   = '4'    --取消(入荷済み)  　" & vbNewLine _
                                                            & "       CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消(入荷済み)　" & vbNewLine _
                                                            & "            THEN '0'                                            " & vbNewLine _
                                                            & "            ELSE CASE WHEN LEN(HIL.INKA_CTL_NO_L) > 0 THEN  '3' " & vbNewLine _
                                                            & "                      ELSE INNCG.DEL_KB  END                    " & vbNewLine _
                                                            & "       END  AS   DEL_KB                                         " & vbNewLine _
                                                            & "      ,INNCG.NRS_BR_CD	  AS  NRS_BR_CD                   " & vbNewLine _
                                                            & "      ,INNCG.EDI_CTL_NO	  AS  EDI_CTL_NO                  " & vbNewLine _
                                                            & "      ,''                  AS  INKA_CTL_NO_L               " & vbNewLine _
                                                            & "      ,'10'                AS  INKA_TP                     " & vbNewLine _
                                                            & "      ,'10'                AS  INKA_KB                     " & vbNewLine _
                                                            & "      ,'10'                AS  INKA_STATE_KB               " & vbNewLine _
                                                            & "      ,INNCG.SEIRI_DATE    AS  INKA_DATE                   " & vbNewLine _
                                                            & "      ,'0900'                  AS  INKA_TIME                   " & vbNewLine _
                                                            & "--2018/02/13      ,@WH_CD              AS  NRS_WH_CD                   " & vbNewLine _
                                                            & "      ,ISNULL(KBN.KBN_NM2, @WH_CD)     AS  NRS_WH_CD       " & vbNewLine _
                                                            & "      ,INNCG.CUST_CD_L     AS  CUST_CD_L                   " & vbNewLine _
                                                            & "      ,INNCG.CUST_CD_M     AS  CUST_CD_M                   " & vbNewLine _
                                                            & "      ,''                  AS  CUST_NM_L                   " & vbNewLine _
                                                            & "      ,''                  AS  CUST_NM_M                   " & vbNewLine _
                                                            & "      ,0                   AS  INKA_PLAN_QT                " & vbNewLine _
                                                            & "      ,''                  AS  INKA_PLAN_QT_UT             " & vbNewLine _
                                                            & "      ,0                   AS  INKA_TTL_NB                 " & vbNewLine _
                                                            & "      ,'01'                AS  NAIGAI_KB                   " & vbNewLine _
                                                            & "      ,INNCG.OUTKA_DENP_NO AS  BUYER_ORD_NO            " & vbNewLine _
                                                            & "      ,INNCG.IO_DENP_NO    AS  OUTKA_FROM_ORD_NO --出荷の受注伝票NO.をセット(IO_DENP_NOを使用)  " & vbNewLine _
                                                            & "      ,''                  AS  SEIQTO_CD                   " & vbNewLine _
                                                            & "      ,'1'                 AS  TOUKI_HOKAN_YN              " & vbNewLine _
                                                            & "      ,'1'                 AS  HOKAN_YN                    " & vbNewLine _
                                                            & "      ,'0'                 AS  HOKAN_FREE_KIKAN            " & vbNewLine _
                                                            & "      ,INNCG.SEIRI_DATE    AS  HOKAN_STR_DATE              " & vbNewLine _
                                                            & "      ,'1'                 AS  NIYAKU_YN                   " & vbNewLine _
                                                            & "      ,'01'                AS  TAX_KB                      " & vbNewLine _
                                                            & "      ,''                  AS  REMARK                      " & vbNewLine _
                                                            & "      ,''                  AS  NYUBAN_L                    " & vbNewLine _
                                                            & "      ,'90'                AS  UNCHIN_TP                   " & vbNewLine _
                                                            & "      ,''                  AS  UNCHIN_KB                   " & vbNewLine _
                                                            & "      ,''                  AS  OUTKA_MOTO                  " & vbNewLine _
                                                            & "      ,''                  AS  SYARYO_KB                   " & vbNewLine _
                                                            & "      ,''                  AS  UNSO_ONDO_KB                " & vbNewLine _
                                                            & "      ,''                  AS  UNSO_CD                     " & vbNewLine _
                                                            & "      ,''                  AS  UNSO_BR_CD                  " & vbNewLine _
                                                            & "      ,'0'                 AS  UNCHIN                      " & vbNewLine _
                                                            & "      ,''                  AS  YOKO_TARIFF_CD              " & vbNewLine _
                                                            & "      ,'0'                 AS  OUT_FLAG                    " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DEL_KB = '3' THEN '1' ELSE '0' END   AS  AKAKURO_KB                  " & vbNewLine _
                                                            & "      ,'9'                 AS  JISSEKI_FLAG                " & vbNewLine _
                                                            & "      ,''                  AS  JISSEKI_USER                " & vbNewLine _
                                                            & "      ,''                  AS  JISSEKI_DATE                " & vbNewLine _
                                                            & "      ,''                  AS  JISSEKI_TIME                " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N01                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N02                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N03                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N04                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N05                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N06                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N07                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N08                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N09                    " & vbNewLine _
                                                            & "      ,'0'                 AS  FREE_N10                    " & vbNewLine _
                                                            & "      ,INNCG.DATA_ID_AREA  AS  FREE_C01                    " & vbNewLine _
                                                            & "      ,INNCG.DATA_ID_DETAIL AS FREE_C02                    " & vbNewLine _
                                                            & "--      ,CASE WHEN INNCG.DATA_ID_DETAIL = 'L'                                            " & vbNewLine _
                                                            & "--            THEN INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO                 " & vbNewLine _
                                                            & "--            ELSE INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN)  " & vbNewLine _
                                                            & "--       END                 AS  FREE_C03                                                " & vbNewLine _
                                                            & "--      ,INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN) AS  FREE_C03     " & vbNewLine _
                                                            & "      ,INNCG.OUTKA_DENP_NO AS  FREE_C03    --EDIキー       " & vbNewLine _
                                                            & "      ,INNCG.INPUT_KBN     AS  FREE_C04                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C05                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C06                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C07                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C08                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C09                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C10                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C11                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C12                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C13                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C14                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C15                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C16                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C17                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C18                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C19                    " & vbNewLine _
                                                            & "      ,INNCG.CUST_CD_L + INNCG.CUST_CD_M  AS  FREE_C20     " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C21                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C22                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C23                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C24                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C25                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C26                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C27                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C28                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C29                    " & vbNewLine _
                                                            & "      ,''                  AS  FREE_C30                    " & vbNewLine _
                                                            & "      ,INNCG.UPD_USER      AS  CRT_USER                    " & vbNewLine _
                                                            & "      ,INNCG.UPD_DATE     AS  CRT_DATE                    " & vbNewLine _
                                                            & "      ,INNCG.UPD_TIME      AS  CRT_TIME                    " & vbNewLine _
                                                            & "      ,''                  AS  UPD_USER                    " & vbNewLine _
                                                            & "      ,''                  AS  UPD_DATE                    " & vbNewLine _
                                                            & "      ,''                  AS  UPD_TIME                    " & vbNewLine _
                                                            & "      ,''                  AS  EDIT_FLAG                   " & vbNewLine _
                                                            & "      ,''                  AS  MATCHING_FLAG               " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_DATE  AS  SYS_ENT_DATE                " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_TIME  AS  SYS_ENT_TIME                " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_PGID  AS  SYS_ENT_PGID                " & vbNewLine _
                                                            & "      ,INNCG.SYS_ENT_USER  AS  SYS_ENT_USER                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_DATE  AS  SYS_UPD_DATE                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_TIME  AS  SYS_UPD_TIME                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_PGID  AS  SYS_UPD_PGID                " & vbNewLine _
                                                            & "      ,INNCG.SYS_UPD_USER  AS  SYS_UPD_USER                " & vbNewLine _
                                                            & "--      ,CASE WHEN INNCG.INPUT_KBN   = '4'    --取消         " & vbNewLine _
                                                            & "      ,CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消      " & vbNewLine _
                                                            & "            THEN '1'                                       " & vbNewLine _
                                                            & "            ELSE INNCG.SYS_DEL_FLG  END  AS   SYS_DEL_FLG   " & vbNewLine _
                                                            & "  FROM                                                     " & vbNewLine _
                                                            & "      $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW INNCG               " & vbNewLine _
                                                            & "      --荷主マスタ                                         " & vbNewLine _
                                                            & "      LEFT JOIN $LM_MST$..M_CUST MC                        " & vbNewLine _
                                                            & "      on INNCG.NRS_BR_CD  = MC.NRS_BR_CD                   " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L = MC.CUST_CD_L                   " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M = MC.CUST_CD_M                   " & vbNewLine _
                                                            & "      AND MC.CUST_CD_S = '00' AND MC.CUST_CD_SS = '00'     " & vbNewLine _
                                                            & "      --H_INKAEDI_L(既に入荷済み場合は保留にする)      　  " & vbNewLine _
                                                            & "      LEFT JOIN $LM_TRN$..H_INKAEDI_L HIL                  " & vbNewLine _
                                                            & "      on  INNCG.NRS_BR_CD     = HIL.NRS_BR_CD              " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L     = HIL.CUST_CD_L              " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M     = HIL.CUST_CD_M              " & vbNewLine _
                                                            & "--      AND INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN) = HIL.FREE_C03              " & vbNewLine _
                                                            & "      AND INNCG.OUTKA_DENP_NO  = HIL.FREE_C03               " & vbNewLine _
                                                            & "      AND HIL.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                                            & "      AND HIL.DEL_KB              = '0'                    " & vbNewLine _
                                                            & "      --区分マスタ        保管場所から倉庫CD取得     　    " & vbNewLine _
                                                            & "      LEFT JOIN $LM_MST$..Z_KBN  KBN                       " & vbNewLine _
                                                            & "      on    KBN.KBN_GROUP_CD =  'H026'                     " & vbNewLine _
                                                            & "      AND  KBN.KBN_NM1       =  INNCG.INKA_HOKAN_BASYO     " & vbNewLine _
                                                            & "      AND  KBN.SYS_DEL_FLG   =  '0'                        " & vbNewLine _
                                                            & "  WHERE                                                    " & vbNewLine _
                                                            & "          INNCG.NRS_BR_CD    = @NRS_BR_CD                  " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L    = @CUST_CD_L                  " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M    = @CUST_CD_M                  " & vbNewLine _
                                                            & "      AND INNCG.SYS_ENT_DATE = @SYS_ENT_DATE               " & vbNewLine _
                                                            & "      AND INNCG.SYS_ENT_TIME = @SYS_ENT_TIME               " & vbNewLine _
                                                            & "      AND INNCG.EDI_CTL_NO_CHU = '001'                      " & vbNewLine _
                                                            & "      AND ((INNCG.DEL_KB        = '0' )                       " & vbNewLine _
                                                            & "      -- 取消で入荷ずみも抽出　                               " & vbNewLine _
                                                            & "       OR (INNCG.DEL_KB        = '3'                          " & vbNewLine _
                                                            & "--       AND INNCG.INPUT_KBN      = '4'    --取消　         　  " & vbNewLine _
                                                            & "       AND LEN(HIL.INKA_CTL_NO_L)    > 0    ))  --入荷済み    " & vbNewLine _
                                                            & "      AND INNCG.DATA_ID_AREA = '201' -- 出荷返品           " & vbNewLine _
                                                            & "ORDER BY                                                   " & vbNewLine _
                                                            & "     EDI_CTL_NO                                            " & vbNewLine

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大) 登録用)"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(中) 登録用)"

    Private Const SQL_SELECT_FOR_INKAEDI_M_FROM_INKAEDI_DTL_NCGO_NEW As String = "SELECT    DISTINCT                              " & vbNewLine _
                                                            & "--     CASE WHEN INNCG.INPUT_KBN   = '4'    --取消(入荷済み)      　" & vbNewLine _
                                                            & "     CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消(入荷済み)     " & vbNewLine _
                                                            & "          THEN '0'                                                " & vbNewLine _
                                                            & "          ELSE CASE WHEN LEN(HIL.INKA_CTL_NO_L) > 0 THEN  '3'   --保留  " & vbNewLine _
                                                            & "                    ELSE INNCG.DEL_KB  END                        " & vbNewLine _
                                                            & "     END  AS   DEL_KB                                             " & vbNewLine _
                                                            & "    ,INNCG.NRS_BR_CD            AS  NRS_BR_CD                     " & vbNewLine _
                                                            & "    ,INNCG.EDI_CTL_NO           AS  EDI_CTL_NO                    " & vbNewLine _
                                                            & "    ,INNCG.EDI_CTL_NO_CHU       AS  EDI_CTL_NO_CHU                " & vbNewLine _
                                                            & "    ,''                         AS  INKA_CTL_NO_L                 " & vbNewLine _
                                                            & "    ,''                         AS  INKA_CTL_NO_M                 " & vbNewLine _
                                                            & "    ,''                         AS  NRS_GOODS_CD                  " & vbNewLine _
                                                            & "    ,INNCG.ITEM_RYAKUGO         AS  CUST_GOODS_CD                 " & vbNewLine _
                                                            & "    ,INNCG.ITEM_AISYO           AS  GOODS_NM                      " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3' THEN INNCG.KOSU * -1 ELSE INNCG.KOSU END AS  NB   " & vbNewLine _
                                                            & "--    ,ISNULL(MG.NB_UT,'')        AS  NB_UT                       " & vbNewLine _
                                                            & "--    ,ISNULL(MG.PKG_NB,1)        AS  PKG_NB                      " & vbNewLine _
                                                            & "--    ,ISNULL(MG.PKG_UT,'')       AS  PKG_UT                      " & vbNewLine _
                                                            & "    ,''                         AS  NB_UT                         " & vbNewLine _
                                                            & "    ,0                          AS  PKG_NB                        " & vbNewLine _
                                                            & "    ,''                         AS  PKG_UT                        " & vbNewLine _
                                                            & "    ,0                          AS  INKA_PKG_NB                   " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3' THEN INNCG.KOSU * -1 ELSE INNCG.KOSU END AS  HASU  " & vbNewLine _
                                                            & "  --  ,ISNULL(MG.STD_IRIME_NB,1)	AS	STD_IRIME                    " & vbNewLine _
                                                            & "  --  ,ISNULL(MG.STD_IRIME_UT,'')	STD_IRIME_UT                 " & vbNewLine _
                                                            & "  --  ,ISNULL(MG.STD_WT_KGS,0) 	AS	BETU_WT                      " & vbNewLine _
                                                            & "    ,0                          AS  STD_IRIME                     " & vbNewLine _
                                                            & "    ,''                         AS  STD_IRIME_UT                  " & vbNewLine _
                                                            & "    ,0                          AS  BETU_WT                       " & vbNewLine _
                                                            & "    ,0                          AS  CBM                           " & vbNewLine _
                                                            & "--    ,CASE WHEN MG.GOODS_CD_NRS IS NOT NULL THEN MG.ONDO_KB                         " & vbNewLine _
                                                            & "--          ELSE '02' END 	AS	ONDO_KB                                               " & vbNewLine _
                                                            & "    ,'01'                      AS  ONDO_KB                        " & vbNewLine _
                                                            & "    ,INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO   AS  OUTKA_FROM_ORD_NO              " & vbNewLine _
                                                            & "    ,''                        AS  BUYER_ORD_NO                   " & vbNewLine _
                                                            & "    ,''                        AS  REMARK                         " & vbNewLine _
                                                            & "    ,INNCG.SEIZO_LOT           AS  LOT_NO                         " & vbNewLine _
                                                            & "    ,''                        AS  SERIAL_NO                      " & vbNewLine _
                                                            & "--    ,ISNULL(MG.STD_IRIME_NB,1)  AS  IRIME                                                 " & vbNewLine _
                                                            & "--    ,ISNULL(MG.STD_IRIME_UT,'')  AS  IRIME_UT                                              " & vbNewLine _
                                                            & "    ,INNCG.SUURYO / INNCG.KOSU   AS  IRIME                        " & vbNewLine _
                                                            & "    ,''                          AS  IRIME_UT                     " & vbNewLine _
                                                            & "    ,'0'                         AS  OUT_KB                       " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3' THEN '1' ELSE '0' END  AS  AKAKURO_KB  " & vbNewLine _
                                                            & "    ,'9'                         AS  JISSEKI_FLAG                 " & vbNewLine _
                                                            & "    ,''                          AS  JISSEKI_USER                 " & vbNewLine _
                                                            & "    ,''                          AS  JISSEKI_DATE                 " & vbNewLine _
                                                            & "    ,''                          AS  JISSEKI_TIME                 " & vbNewLine _
                                                            & "    ,INNCG.KOSU                  AS  FREE_N01                     " & vbNewLine _
                                                            & "    ,INNCG.SUURYO                AS  FREE_N02                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N03                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N04                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N05                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N06                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N07                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N08                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N09                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N10                     " & vbNewLine _
                                                            & "    ,INNCG.ITEM_RYAKUGO          AS  FREE_C01                     " & vbNewLine _
                                                            & "    ,INNCG.ITEM_CD               AS  FREE_C02                     " & vbNewLine _
                                                            & "    ,INNCG.ITEM_AISYO            AS  FREE_C03                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C04                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C05                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C06                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C07                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C08                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C09                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C10                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C11                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C12                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C13                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C14                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C15                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C16                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C17                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C18                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C19                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C20                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C21                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C22                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C23                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C24                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C25                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C26                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C27                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C28                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C29                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C30                     " & vbNewLine _
                                                            & "    ,INNCG.UPD_USER              AS  CRT_USER                     " & vbNewLine _
                                                            & "    ,INNCG.UPD_DATE              AS  CRT_DATE                     " & vbNewLine _
                                                            & "    ,INNCG.UPD_TIME              AS  CRT_TIME                     " & vbNewLine _
                                                            & "    ,''                          AS  UPD_USER                     " & vbNewLine _
                                                            & "    ,''                          AS  UPD_DATE                     " & vbNewLine _
                                                            & "    ,''                          AS  UPD_TIME                     " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_DATE          AS  SYS_ENT_DATE                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_TIME          AS  SYS_ENT_TIME                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_PGID          AS  SYS_ENT_PGID                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_USER          AS  SYS_ENT_USER                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_DATE          AS  SYS_UPD_DATE                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_TIME          AS  SYS_UPD_TIME                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_PGID          AS  SYS_UPD_PGID                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_USER          AS  SYS_UPD_USER                 " & vbNewLine _
                                                            & "--    ,CASE WHEN INNCG.INPUT_KBN   = '4'    --取消                  " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消               " & vbNewLine _
                                                            & "          THEN '1'                                                " & vbNewLine _
                                                            & "          ELSE INNCG.SYS_DEL_FLG  END  AS   SYS_DEL_FLG           " & vbNewLine _
                                                            & "FROM                                                              " & vbNewLine _
                                                            & "    $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW INNCG                        " & vbNewLine _
                                                            & "      --H_INKAEDI_L(既に入荷済み場合は保留にする)      　         " & vbNewLine _
                                                            & "      LEFT JOIN $LM_TRN$..H_INKAEDI_L HIL                         " & vbNewLine _
                                                            & "      on  INNCG.NRS_BR_CD     = HIL.NRS_BR_CD                     " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L     = HIL.CUST_CD_L                     " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M     = HIL.CUST_CD_M                     " & vbNewLine _
                                                            & "      AND HIL.FREE_C03 = CASE WHEN INNCG.DATA_ID_DETAIL = 'K' THEN                                                           " & vbNewLine _
                                                            & "                                   INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN) " & vbNewLine _
                                                            & "                              ELSE INNCG.OUTKA_DENP_NO    END                                                                  " & vbNewLine _
                                                            & "      AND HIL.SYS_DEL_FLG         = '0'                    " & vbNewLine _
                                                            & "      AND (INNCG.SYS_ENT_DATE  <> HIL.SYS_ENT_DATE                " & vbNewLine _
                                                            & "        OR INNCG.SYS_ENT_TIME  <> HIL.SYS_ENT_TIME)               " & vbNewLine _
                                                            & "      AND HIL.SYS_DEL_FLG         = '0'                           " & vbNewLine _
                                                            & "      AND HIL.DEL_KB              = '0'                           " & vbNewLine _
                                                            & "WHERE                                                             " & vbNewLine _
                                                            & "        INNCG.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                            & "    AND INNCG.CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                                            & "    AND INNCG.CUST_CD_M = @CUST_CD_M                              " & vbNewLine _
                                                            & "    AND INNCG.SYS_ENT_DATE = @SYS_ENT_DATE                        " & vbNewLine _
                                                            & "    AND INNCG.SYS_ENT_TIME = @SYS_ENT_TIME                        " & vbNewLine _
                                                            & "    AND ((INNCG.DEL_KB        = '0' )                             " & vbNewLine _
                                                            & "    -- 取消で入荷ずみも抽出　                                     " & vbNewLine _
                                                            & "      OR (INNCG.DEL_KB        = '3'                               " & vbNewLine _
                                                            & "--      AND INNCG.INPUT_KBN      = '4'    --取消　         　       " & vbNewLine _
                                                            & "      AND LEN(HIL.INKA_CTL_NO_L)    > 0    ))  --入荷済み         " & vbNewLine _
                                                            & "    AND INNCG.DATA_ID_AREA <> '201' -- 出荷返品以外(入荷)         " & vbNewLine _
                                                            & "UNION ALL                                                         " & vbNewLine _
                                                            & "SELECT                                                            " & vbNewLine _
                                                            & "--     CASE WHEN INNCG.INPUT_KBN   = '4'    --取消(入荷済み)      　" & vbNewLine _
                                                            & "     CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消(入荷済み)    　" & vbNewLine _
                                                            & "          THEN '0'                                                " & vbNewLine _
                                                            & "          ELSE CASE WHEN LEN(HIL.INKA_CTL_NO_L) > 0 THEN  '3'     " & vbNewLine _
                                                            & "                    ELSE INNCG.DEL_KB  END                        " & vbNewLine _
                                                            & "     END  AS   DEL_KB                                             " & vbNewLine _
                                                            & "    ,INNCG.NRS_BR_CD            AS  NRS_BR_CD                     " & vbNewLine _
                                                            & "    ,INNCG.EDI_CTL_NO           AS  EDI_CTL_NO                    " & vbNewLine _
                                                            & "    ,INNCG.EDI_CTL_NO_CHU       AS  EDI_CTL_NO_CHU                " & vbNewLine _
                                                            & "    ,''                         AS  INKA_CTL_NO_L                 " & vbNewLine _
                                                            & "    ,''                         AS  INKA_CTL_NO_M                 " & vbNewLine _
                                                            & "    ,''                         AS  NRS_GOODS_CD                  " & vbNewLine _
                                                            & "    ,INNCG.ITEM_RYAKUGO         AS  CUST_GOODS_CD                 " & vbNewLine _
                                                            & "    ,INNCG.ITEM_AISYO           AS  GOODS_NM                      " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3' THEN INNCG.KOSU * -1 ELSE INNCG.KOSU END AS  NB  " & vbNewLine _
                                                            & "--    ,ISNULL(MG.NB_UT,'')        AS  NB_UT                       " & vbNewLine _
                                                            & "--    ,ISNULL(MG.PKG_NB,1)        AS  PKG_NB                      " & vbNewLine _
                                                            & "--    ,ISNULL(MG.PKG_UT,'')       AS  PKG_UT                      " & vbNewLine _
                                                            & "    ,''                         AS  NB_UT                         " & vbNewLine _
                                                            & "    ,0                          AS  PKG_NB                        " & vbNewLine _
                                                            & "    ,''                         AS  PKG_UT                        " & vbNewLine _
                                                            & "    ,0                          AS  INKA_PKG_NB                   " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3' THEN INNCG.KOSU * -1 ELSE INNCG.KOSU END AS  HASU  " & vbNewLine _
                                                            & "  --  ,ISNULL(MG.STD_IRIME_NB,1)	AS	STD_IRIME                    " & vbNewLine _
                                                            & "  --  ,ISNULL(MG.STD_IRIME_UT,'')	STD_IRIME_UT                 " & vbNewLine _
                                                            & "  --  ,ISNULL(MG.STD_WT_KGS,0) 	AS	BETU_WT                      " & vbNewLine _
                                                            & "    ,0                          AS  STD_IRIME                     " & vbNewLine _
                                                            & "    ,''                         AS  STD_IRIME_UT                  " & vbNewLine _
                                                            & "    ,0                          AS  BETU_WT                       " & vbNewLine _
                                                            & "    ,0                          AS  CBM                           " & vbNewLine _
                                                            & "--    ,CASE WHEN MG.GOODS_CD_NRS IS NOT NULL THEN MG.ONDO_KB                         " & vbNewLine _
                                                            & "--          ELSE '02' END 	AS	ONDO_KB                                               " & vbNewLine _
                                                            & "    ,'01'                      AS  ONDO_KB                        " & vbNewLine _
                                                            & "    ,INNCG.IO_DENP_NO + INNCG.IO_DENP_DTL_NO   AS  OUTKA_FROM_ORD_NO  --出荷の受注伝票NO.をセット(IO_DENP_NOを使用) " & vbNewLine _
                                                            & "    ,''                        AS  BUYER_ORD_NO                   " & vbNewLine _
                                                            & "    ,''                        AS  REMARK                         " & vbNewLine _
                                                            & "    ,INNCG.SEIZO_LOT           AS  LOT_NO                         " & vbNewLine _
                                                            & "    ,''                        AS  SERIAL_NO                      " & vbNewLine _
                                                            & "--    ,ISNULL(MG.STD_IRIME_NB,1)  AS  IRIME                                                 " & vbNewLine _
                                                            & "--    ,ISNULL(MG.STD_IRIME_UT,'')  AS  IRIME_UT                                              " & vbNewLine _
                                                            & "    ,INNCG.SUURYO / INNCG.KOSU   AS  IRIME                        " & vbNewLine _
                                                            & "    ,''                          AS  IRIME_UT                     " & vbNewLine _
                                                            & "    ,'0'                         AS  OUT_KB                       " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3' THEN '1' ELSE '0' END  AS  AKAKURO_KB   " & vbNewLine _
                                                            & "    ,'9'                         AS  JISSEKI_FLAG                 " & vbNewLine _
                                                            & "    ,''                          AS  JISSEKI_USER                 " & vbNewLine _
                                                            & "    ,''                          AS  JISSEKI_DATE                 " & vbNewLine _
                                                            & "    ,''                          AS  JISSEKI_TIME                 " & vbNewLine _
                                                            & "    ,INNCG.KOSU                  AS  FREE_N01                     " & vbNewLine _
                                                            & "    ,INNCG.SUURYO                AS  FREE_N02                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N03                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N04                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N05                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N06                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N07                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N08                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N09                     " & vbNewLine _
                                                            & "    ,'0'                         AS  FREE_N10                     " & vbNewLine _
                                                            & "    ,INNCG.ITEM_RYAKUGO          AS  FREE_C01                     " & vbNewLine _
                                                            & "    ,INNCG.ITEM_CD               AS  FREE_C02                     " & vbNewLine _
                                                            & "    ,INNCG.ITEM_AISYO            AS  FREE_C03                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C04                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C05                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C06                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C07                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C08                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C09                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C10                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C11                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C12                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C13                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C14                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C15                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C16                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C17                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C18                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C19                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C20                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C21                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C22                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C23                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C24                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C25                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C26                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C27                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C28                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C29                     " & vbNewLine _
                                                            & "    ,''                          AS  FREE_C30                     " & vbNewLine _
                                                            & "    ,INNCG.UPD_USER              AS  CRT_USER                     " & vbNewLine _
                                                            & "    ,INNCG.UPD_DATE              AS  CRT_DATE                     " & vbNewLine _
                                                            & "    ,INNCG.UPD_TIME              AS  CRT_TIME                     " & vbNewLine _
                                                            & "    ,''                          AS  UPD_USER                     " & vbNewLine _
                                                            & "    ,''                          AS  UPD_DATE                     " & vbNewLine _
                                                            & "    ,''                          AS  UPD_TIME                     " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_DATE          AS  SYS_ENT_DATE                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_TIME          AS  SYS_ENT_TIME                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_PGID          AS  SYS_ENT_PGID                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_ENT_USER          AS  SYS_ENT_USER                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_DATE          AS  SYS_UPD_DATE                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_TIME          AS  SYS_UPD_TIME                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_PGID          AS  SYS_UPD_PGID                 " & vbNewLine _
                                                            & "    ,INNCG.SYS_UPD_USER          AS  SYS_UPD_USER                 " & vbNewLine _
                                                            & "--    ,CASE WHEN INNCG.INPUT_KBN   = '4'    --取消                  " & vbNewLine _
                                                            & "    ,CASE WHEN INNCG.DEL_KB = '3'    --訂正赤・取消               " & vbNewLine _
                                                            & "          THEN '1'                                                " & vbNewLine _
                                                            & "          ELSE INNCG.SYS_DEL_FLG  END  AS   SYS_DEL_FLG           " & vbNewLine _
                                                            & "FROM                                                              " & vbNewLine _
                                                            & "    $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW INNCG                        " & vbNewLine _
                                                            & "      --H_INKAEDI_L(既に入荷済み場合は保留にする)      　         " & vbNewLine _
                                                            & "      LEFT JOIN $LM_TRN$..H_INKAEDI_L HIL                         " & vbNewLine _
                                                            & "      on  INNCG.NRS_BR_CD     = HIL.NRS_BR_CD                     " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_L     = HIL.CUST_CD_L                     " & vbNewLine _
                                                            & "      AND INNCG.CUST_CD_M     = HIL.CUST_CD_M                     " & vbNewLine _
                                                            & " --     AND INNCG.HACCHU_DENP_NO + '-' + INNCG.HACCHU_DENP_DTL_NO + '-' + CONVERT(VARCHAR,INNCG.RENBAN) = HIL.FREE_C03       " & vbNewLine _
                                                            & "      AND INNCG.OUTKA_DENP_NO  = HIL.FREE_C03                      " & vbNewLine _
                                                            & "      AND (INNCG.SYS_ENT_DATE  <> HIL.SYS_ENT_DATE                " & vbNewLine _
                                                            & "        OR INNCG.SYS_ENT_TIME  <> HIL.SYS_ENT_TIME)               " & vbNewLine _
                                                            & "      AND HIL.SYS_DEL_FLG         = '0'                           " & vbNewLine _
                                                            & "      AND HIL.DEL_KB              = '0'                           " & vbNewLine _
                                                            & "WHERE                                                             " & vbNewLine _
                                                            & "        INNCG.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                                            & "    AND INNCG.CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                                            & "    AND INNCG.CUST_CD_M = @CUST_CD_M                              " & vbNewLine _
                                                            & "    AND INNCG.SYS_ENT_DATE = @SYS_ENT_DATE                        " & vbNewLine _
                                                            & "    AND INNCG.SYS_ENT_TIME = @SYS_ENT_TIME                        " & vbNewLine _
                                                            & "    AND ((INNCG.DEL_KB        = '0' )                             " & vbNewLine _
                                                            & "    -- 取消で入荷ずみも抽出　                                     " & vbNewLine _
                                                            & "      OR (INNCG.DEL_KB        = '3'                               " & vbNewLine _
                                                            & "--      AND INNCG.INPUT_KBN      = '4'    --取消　         　       " & vbNewLine _
                                                            & "      AND LEN(HIL.INKA_CTL_NO_L)    > 0    ))  --入荷済み         " & vbNewLine _
                                                            & "    AND INNCG.DATA_ID_AREA = '201' -- 出荷返品                    " & vbNewLine _
                                                            & "ORDER BY                                                          " & vbNewLine _
                                                            & "     EDI_CTL_NO                                                   " & vbNewLine _
                                                            & "    ,EDI_CTL_NO_CHU                                               " & vbNewLine

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(中) 登録用)"

#End Region ' "セミEDI処理"

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

#Region "RCV_HED(入荷登録)"
    ''' <summary>
    ''' 受信ヘッダ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_RCV_HED As String = "UPDATE                                               " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_HED_NCGO                      " & vbNewLine _
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
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine


#End Region

#Region "RCV_DTL(入荷登録)"
    ''' <summary>
    ''' 受信明細更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_RCV_DTL As String = "UPDATE                                            " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
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
                                              & " SYS_DEL_FLG       = '0'"


#End Region

#Region "INKAEDI_L(実績取消)"
    ''' <summary>
    ''' INKAEDI_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKITORIKESI_EDI_L As String = "UPDATE                               " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                           " & vbNewLine _
                                              & " SET                                             " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG               " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                   " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                   " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                   " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE               " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME               " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID               " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER               " & vbNewLine _
                                              & " WHERE                                           " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                  " & vbNewLine _
                                              & " AND                                             " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                 " & vbNewLine _
                                              & " AND                                             " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE         " & vbNewLine _
                                              & " AND                                             " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME         " & vbNewLine

#End Region

#Region "RCV_HED(実績取消)"
    ''' <summary>
    ''' RCV_HED(実績取消)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKITORIKESI_RCV_HED As String = "UPDATE                               " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_HED_NCGO                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

#End Region

#Region "RCV_DTL(実績取消)"
    ''' <summary>
    ''' RCV_DTL(実績取消)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKITORIKESI_RCV_DTL As String = "UPDATE                               " & vbNewLine _
                                              & "-- $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG = '1'                           " & vbNewLine

#End Region

#Region "INKAEDI_L(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' EDI入荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_L As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_INKAEDI_L                                 " & vbNewLine _
                                         & " SET                                                   " & vbNewLine _
                                         & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                         & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                         & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                         & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
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

#Region "INKAEDI_M(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' EDI入荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_M As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_INKAEDI_M                                 " & vbNewLine _
                                         & " SET                                                   " & vbNewLine _
                                         & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                         & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                         & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                         & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                         & ",SYS_DEL_FLG  = @SYS_DEL_FLG                           " & vbNewLine _
                                         & " WHERE                                                 " & vbNewLine _
                                         & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine

#End Region

#Region "RCV_HED(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' 受信ヘッダ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_RCV_HED As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_HED_NCGO                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_DTL(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' 受信明細更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_RCV_DTL As String = "UPDATE                                " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine


#End Region

#Region "INKAEDI_L(実績作成済⇒実績未、実績送信済⇒実績未、実績作成)"
    ''' <summary>
    ''' INKAEDI_L(実績作成済⇒実績未、実績送信済⇒実績未、実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_EDI_L As String = "UPDATE                           " & vbNewLine _
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

#Region "INKAEDI_L(実績送信済⇒送信待ち)"
    ''' <summary>
    ''' H_INKAEDI_LのUPDATE文（H_INKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKIZUMI_SOUSINMACHI_EDI_L As String = "UPDATE $LM_TRN$..H_INKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & " WHERE                                                       " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                              " & vbNewLine _
                                              & " AND                                                         " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                             " & vbNewLine _
                                              & " AND                                                         " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE                     " & vbNewLine _
                                              & " AND                                                         " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME                     " & vbNewLine

#End Region

#Region "INKAEDI_M(実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' INKAEDI_M(実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_EDI_M As String = "UPDATE                           " & vbNewLine _
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
                                              & " JISSEKI_FLAG       <> '9'                          " & vbNewLine

#End Region

#Region "INKAEDI_M(実績送信済⇒送信待ち)"
    ''' <summary>
    ''' H_INKAEDI_MのUPDATE文（H_INKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKIZUMI_SOUSINMACHI_EDI_M As String = "UPDATE $LM_TRN$..H_INKAEDI_M SET       " & vbNewLine _
                                             & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                             & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                             & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                             & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                             & " WHERE                                                       " & vbNewLine _
                                             & " NRS_BR_CD         = @NRS_BR_CD                              " & vbNewLine _
                                             & " AND                                                         " & vbNewLine _
                                             & " EDI_CTL_NO        = @EDI_CTL_NO                             " & vbNewLine _



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

#Region "RCV_HED(実績作成、実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' RCV_HED(実績作成、実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_RCV_HED As String = "UPDATE                                 " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_HED_NCGO                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_JISSEKI_FLG  = @INKA_JISSEKI_FLG             " & vbNewLine _
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
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_HED(実績送信済⇒送信未)"
    ''' <summary>
    ''' RCV_HED(実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED As String = "UPDATE                                 " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_HED_NCGO                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
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
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_DTL(実績作成済⇒実績未)"
    ''' <summary>
    ''' RCV_DTL(実績作成済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIZUMI_JISSEKIMI_RCV_DTL As String = "UPDATE                         " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_JISSEKI_FLG  = @INKA_JISSEKI_FLG             " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG  = '2'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "RCV_DTL(実績作成)"
    ''' <summary>
    ''' RCV_DTL(実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKISAKUSEI_RCV_DTL As String = "UPDATE                                " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NWE                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_JISSEKI_FLG  = @INKA_JISSEKI_FLG             " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
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
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "RCV_DTL(実績送信済⇒実績未、実績送信済⇒送信未)"
    ''' <summary>
    ''' RCV_DTL(実績送信済⇒実績未、実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_JISSEKIMI_RCV_DTL As String = "UPDATE                                 " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_JISSEKI_FLG  = @INKA_JISSEKI_FLG             " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG  = '3'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "RCV_DTL(実績送信済⇒送信未)"
    ''' <summary>
    ''' RCV_DTL(実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL As String = "UPDATE                           " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG  = '3'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "SEND(実績送信済⇒送信未)"
    ''' <summary>
    ''' SEND(実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_SEND As String = "UPDATE                                    " & vbNewLine _
                                              & "$LM_TRN$..H_SENDINEDI_NCGO                         " & vbNewLine _
                                              & "SET                                                " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
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
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "INKA_L(実績作成済⇒実績未、実績送信済⇒実績未、実績作成)"
    ''' <summary>
    ''' INKA_L(実績作成済⇒実績未、実績送信済⇒実績未、実績作成)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_INKA_L As String = "UPDATE                          " & vbNewLine _
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

#Region "INKAEDI_L(入荷取消⇒未登録)"
    ''' <summary>
    ''' INKAEDI_L(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_EDI_L As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = ''                            " & vbNewLine _
                                              & ",OUT_FLAG          = @OUT_FLAG                     " & vbNewLine _
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
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine


#End Region

#Region "INKAEDI_M(入荷取消⇒未登録)"
    ''' <summary>
    ''' INKAEDI_M(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_EDI_M As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_M                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = ''                            " & vbNewLine _
                                              & ",INKA_CTL_NO_M     = ''                            " & vbNewLine _
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
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " OUT_KB            = '0'                           " & vbNewLine

#End Region

#Region "RCV_HED(入荷取消⇒未登録)"
    ''' <summary>
    ''' RCV_HED(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_RCV_HED As String = "UPDATE                                     " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_HED_NCGO                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = 'N00000000'                   " & vbNewLine _
                                              & ",INKA_USER         = @INKA_USER                    " & vbNewLine _
                                              & ",INKA_DATE         = @INKA_DATE                    " & vbNewLine _
                                              & ",INKA_TIME         = @INKA_TIME                    " & vbNewLine _
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
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_DTL(入荷取消⇒未登録)"
    ''' <summary>
    ''' RCV_DTL(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_RCV_DTL As String = "UPDATE                                     " & vbNewLine _
                                              & "--UPD 2017/01/10 $LM_TRN$..H_INKAEDI_DTL_NCGO                      " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW                      " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = 'N00000000'                   " & vbNewLine _
                                              & ",INKA_CTL_NO_M     = '000'                         " & vbNewLine _
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
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine


#End Region



#Region "M品一括振替"
#Region "D_ZAI_TRS(M品在庫0)"

#End Region
    Private Const SQL_UPD_D_ZAI_TRS_COND_M As String _
        = " UPDATE $LM_TRN$..D_ZAI_TRS                                " & vbNewLine _
        & "    SET                                                    " & vbNewLine _
        & "        ALCTD_NB     = 0                                   " & vbNewLine _
        & "      , ALLOC_CAN_NB = 0                                   " & vbNewLine _
        & "      , ALCTD_QT     = 0                                   " & vbNewLine _
        & "      , ALLOC_CAN_QT = 0                                   " & vbNewLine _
        & "      , PORA_ZAI_QT  = 0                                   " & vbNewLine _
        & "      , PORA_ZAI_NB  = 0                                   " & vbNewLine _
        & "      , SYS_UPD_DATE = @SYS_UPD_DATE                       " & vbNewLine _
        & "      , SYS_UPD_TIME = @SYS_UPD_TIME                       " & vbNewLine _
        & "      , SYS_UPD_PGID = @SYS_UPD_PGID                       " & vbNewLine _
        & "      , SYS_UPD_USER = @SYS_UPD_USER                       " & vbNewLine _
        & "  WHERE NRS_BR_CD    = @NRS_BR_CD                          " & vbNewLine _
        & "    AND ZAI_REC_NO   = @ZAI_REC_NO                         " & vbNewLine _
        & "    AND SYS_UPD_DATE = @LAST_UPD_DATE                      " & vbNewLine _
        & "    AND SYS_UPD_TIME = @LAST_UPD_TIME                      " & vbNewLine

    Private Const SQL_UPD_INKA_S_SYS_DEL_FLG_ON As String _
        = " UPDATE $LM_TRN$..B_INKA_S               " & vbNewLine _
        & "    SET                                  " & vbNewLine _
        & "        SYS_UPD_DATE = @SYS_UPD_DATE     " & vbNewLine _
        & "      , SYS_UPD_TIME = @SYS_UPD_TIME     " & vbNewLine _
        & "      , SYS_UPD_PGID = @SYS_UPD_PGID     " & vbNewLine _
        & "      , SYS_UPD_USER = @SYS_UPD_USER     " & vbNewLine _
        & "      , SYS_DEL_FLG  = '1'               " & vbNewLine _
        & "  WHERE SYS_DEL_FLG = '0'                " & vbNewLine _
        & "    AND NRS_BR_CD   = @NRS_BR_CD         " & vbNewLine _
        & "    AND INKA_NO_L   = @INKA_NO_L         " & vbNewLine


    Private Const SQL_UPD_INKAEDI_L_COND_M As String _
        = " UPDATE $LM_TRN$..H_INKAEDI_L               " & vbNewLine _
        & "    SET FREE_C24     = @FREE_C24            " & vbNewLine _
        & "      , SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
        & "      , SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine _
        & "      , SYS_UPD_PGID = @SYS_UPD_PGID        " & vbNewLine _
        & "      , SYS_UPD_USER = @SYS_UPD_USER        " & vbNewLine _
        & "  WHERE SYS_DEL_FLG  = '0'                  " & vbNewLine _
        & "    AND DEL_KB       = '0'                  " & vbNewLine _
        & "    AND EDI_CTL_NO   = @EDI_CTL_NO          " & vbNewLine _
        & "    AND SYS_UPD_DATE = @LAST_UPD_DATE       " & vbNewLine _
        & "    AND SYS_UPD_TIME = @LAST_UPD_TIME       " & vbNewLine

#End Region

#Region "セミEDI処理"

#Region "EDI入荷(大) 論理削除 SQL"

    ''' <summary>
    ''' EDI入荷(大) 論理削除 SQL
    ''' </summary>
    Private Const SQL_UPDATE_DEL_INKAIEDI_L As String = "" _
        & "UPDATE                             " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_L         " & vbNewLine _
        & "SET                                " & vbNewLine _
        & "      DEL_KB       = '1'           " & vbNewLine _
        & "    , UPD_USER     = @UPD_USER     " & vbNewLine _
        & "    , UPD_DATE     = @UPD_DATE     " & vbNewLine _
        & "    , UPD_TIME     = @UPD_TIME     " & vbNewLine _
        & "    , SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
        & "    , SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
        & "    , SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
        & "    , SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
        & "    , SYS_DEL_FLG  = '1'           " & vbNewLine _
        & "WHERE                              " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
        & "AND EDI_CTL_NO = @EDI_CTL_NO       " & vbNewLine

#End Region ' "EDI入荷(大) 論理削除 SQL"

#Region "EDI入荷(中) 論理削除 SQL"

    ''' <summary>
    ''' EDI入荷(中) 論理削除 SQL
    ''' </summary>
    Private Const SQL_UPDATE_DEL_INKAIEDI_M As String = "" _
        & "UPDATE                             " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_M         " & vbNewLine _
        & "SET                                " & vbNewLine _
        & "      DEL_KB       = '1'           " & vbNewLine _
        & "    , UPD_USER     = @UPD_USER     " & vbNewLine _
        & "    , UPD_DATE     = @UPD_DATE     " & vbNewLine _
        & "    , UPD_TIME     = @UPD_TIME     " & vbNewLine _
        & "    , SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
        & "    , SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
        & "    , SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
        & "    , SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
        & "    , SYS_DEL_FLG  = '1'           " & vbNewLine _
        & "WHERE                              " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
        & "AND EDI_CTL_NO = @EDI_CTL_NO       " & vbNewLine

#End Region ' "EDI入荷(中) 論理削除 SQL"

#Region "EDI受信データ(DTL) 論理削除 SQL"

    ''' <summary>
    ''' EDI受信データ(DTL) 論理削除 SQL
    ''' </summary>
    Private Const SQL_UPDATE_DEL_INKAEDI_DTL As String = "" _
        & "UPDATE                               " & vbNewLine _
        & "    $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW " & vbNewLine _
        & "SET                                  " & vbNewLine _
        & "      DEL_KB       = '1'             " & vbNewLine _
        & "    , UPD_USER     = @UPD_USER       " & vbNewLine _
        & "    , UPD_DATE     = @UPD_DATE       " & vbNewLine _
        & "    , UPD_TIME     = @UPD_TIME       " & vbNewLine _
        & "    , SYS_UPD_DATE = @SYS_UPD_DATE   " & vbNewLine _
        & "    , SYS_UPD_TIME = @SYS_UPD_TIME   " & vbNewLine _
        & "    , SYS_UPD_PGID = @SYS_UPD_PGID   " & vbNewLine _
        & "    , SYS_UPD_USER = @SYS_UPD_USER   " & vbNewLine _
        & "    , SYS_DEL_FLG  = '1'             " & vbNewLine _
        & "WHERE                                " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
        & "AND EDI_CTL_NO = @EDI_CTL_NO         " & vbNewLine _
        & "AND SYS_DEL_FLG  = '0'               " & vbNewLine

#End Region ' "EDI受信データ(DTL) 論理削除 SQL"

#Region "H_INKAEDI_DTL_NCGO_NEW(セミEDI時・入荷赤伝・取消・論理削除)"

    Private Const SQL_CANCEL_INKAEDI_DTL_NCGO_NEW As String = "UPDATE $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW SET         " & vbNewLine _
                                                        & "     DEL_KB              = '1'                   " & vbNewLine _
                                                        & "    ,DELETE_USER         = @DELETE_USER          " & vbNewLine _
                                                        & "    ,DELETE_DATE         = @DELETE_DATE          " & vbNewLine _
                                                        & "    ,DELETE_TIME         = @DELETE_TIME          " & vbNewLine _
                                                        & "    ,DELETE_EDI_NO       = @DELETE_EDI_NO        " & vbNewLine _
                                                        & "    ,DELETE_EDI_NO_CHU   = @DELETE_EDI_NO_CHU    " & vbNewLine _
                                                        & "    ,UPD_USER            = @UPD_USER             " & vbNewLine _
                                                        & "    ,UPD_DATE            = @UPD_DATE             " & vbNewLine _
                                                        & "    ,UPD_TIME            = @UPD_TIME             " & vbNewLine _
                                                        & "    ,SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                                        & "    ,SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                                        & "    ,SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                                        & "    ,SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                                        & "    ,SYS_DEL_FLG         = '1'                   " & vbNewLine _
                                                        & "  WHERE                                          " & vbNewLine _
                                                        & "     NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
                                                        & "     AND CUST_CD_L       = @CUST_CD_L            " & vbNewLine _
                                                        & "     AND CUST_CD_M       = @CUST_CD_M            " & vbNewLine _
                                                        & "     AND RIGHT(INKA_CTL_NO_L,8) = '00000000'  --入荷未登録  " & vbNewLine _
                                                        & "     AND DEL_KB          = '0'                   " & vbNewLine

#End Region ' "H_INKAEDI_DTL_NCGO_NEW(セミEDI時・入荷赤伝・取消・論理削除)"

#Region "H_INKAEDI_L 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

    Private Const SQL_CANCEL_INKAEDI_L As String = "UPDATE $LM_TRN$..H_INKAEDI_L     SET                    " & vbNewLine _
                                                        & "     DEL_KB              = '1'                   " & vbNewLine _
                                                        & "    ,UPD_USER            = @UPD_USER             " & vbNewLine _
                                                        & "    ,UPD_DATE            = @UPD_DATE             " & vbNewLine _
                                                        & "    ,UPD_TIME            = @UPD_TIME             " & vbNewLine _
                                                        & "    ,SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                                        & "    ,SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                                        & "    ,SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                                        & "    ,SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                                        & "    ,SYS_DEL_FLG         = '1'                   " & vbNewLine _
                                                        & "  WHERE                                          " & vbNewLine _
                                                        & "     NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
                                                        & "     AND CUST_CD_L       = @CUST_CD_L            " & vbNewLine _
                                                        & "     AND CUST_CD_M       = @CUST_CD_M            " & vbNewLine _
                                                        & "     AND LEN(INKA_CTL_NO_L) = 0   --入荷未登録   " & vbNewLine _
                                                        & "     AND DEL_KB          = '0'                   " & vbNewLine

#End Region ' "H_INKAEDI_L 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

#Region "H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

    Private Const SQL_CANCEL_INKAEDI_M As String = "UPDATE $LM_TRN$..H_INKAEDI_M     SET                    " & vbNewLine _
                                                        & "     DEL_KB              = '1'                   " & vbNewLine _
                                                        & "    ,UPD_USER            = @UPD_USER             " & vbNewLine _
                                                        & "    ,UPD_DATE            = @UPD_DATE             " & vbNewLine _
                                                        & "    ,UPD_TIME            = @UPD_TIME             " & vbNewLine _
                                                        & "    ,SYS_UPD_DATE        = @SYS_UPD_DATE         " & vbNewLine _
                                                        & "    ,SYS_UPD_TIME        = @SYS_UPD_TIME         " & vbNewLine _
                                                        & "    ,SYS_UPD_PGID        = @SYS_UPD_PGID         " & vbNewLine _
                                                        & "    ,SYS_UPD_USER        = @SYS_UPD_USER         " & vbNewLine _
                                                        & "    ,SYS_DEL_FLG         = '1'                   " & vbNewLine _
                                                        & "  WHERE                                          " & vbNewLine _
                                                        & "     NRS_BR_CD           = @NRS_BR_CD            " & vbNewLine _
                                                        & "     AND LEN(INKA_CTL_NO_L) = 0   --入荷未登録 ADD 2017/01/07 " & vbNewLine _
                                                        & "     AND DEL_KB          = '0'                   " & vbNewLine

#End Region ' "H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

#Region "H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)"

    Private Const SQL_UPDATE_UNSOEDI_DTL_NCGO_EDI_CTR_NO As String = "" _
                        & "UPDATE $LM_TRN$..H_UNSOEDI_DTL_NCGO                   " & vbNewLine _
                        & "   SET                                                 " & vbNewLine _
                        & "     EDI_CTL_NO          = IN1.EDI_CTL_NO            " & vbNewLine _
                        & "    ,UPD_USER            = @UPD_USER                   " & vbNewLine _
                        & "    ,UPD_DATE            = @UPD_DATE                   " & vbNewLine _
                        & "    ,UPD_TIME            = @UPD_TIME                   " & vbNewLine _
                        & "    ,SYS_UPD_DATE        = @SYS_UPD_DATE               " & vbNewLine _
                        & "    ,SYS_UPD_TIME        = @SYS_UPD_TIME               " & vbNewLine _
                        & "    ,SYS_UPD_PGID        = @SYS_UPD_PGID               " & vbNewLine _
                        & "    ,SYS_UPD_USER        = @SYS_UPD_USER               " & vbNewLine _
                        & "  	FROM $LM_TRN$..H_UNSOEDI_DTL_NCGO                           " & vbNewLine _
                        & "   --入荷の最新の取込日時取得                                                   " & vbNewLine _
                        & "   LEFT JOIN (SELECT TOP 1 MAX(IN2.SYS_ENT_DATE) AS SYS_ENT_DATE                " & vbNewLine _
                        & "                    ,MAX(IN2.SYS_ENT_TIME) AS SYS_ENT_TIME                      " & vbNewLine _
                        & "                FROM $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW IN2                       " & vbNewLine _
                        & "                WHERE IN2.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                        & "                GROUP BY IN2.SYS_ENT_DATE,IN2.SYS_ENT_TIME                      " & vbNewLine _
                        & "                ORDER BY IN2.SYS_ENT_DATE + IN2.SYS_ENT_TIME DESC) AS OUTWK     " & vbNewLine _
                        & "        ON OUTWK.SYS_ENT_DATE <> ''                                             " & vbNewLine _
                        & "   --入荷ファイルより                                                           " & vbNewLine _
                        & "   LEFT JOIN $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW IN1 ON                            " & vbNewLine _
                        & "                     IN1.OUTKA_DENP_NO  = H_UNSOEDI_DTL_NCGO.OUTKA_DENP_NO      " & vbNewLine _
                        & "                AND  IN1.OUTKA_DENP_DTL_NO  = H_UNSOEDI_DTL_NCGO.OUTKA_DENP_DTL_NO " & vbNewLine _
                        & "                AND  IN1.SYS_ENT_DATE   = OUTWK.SYS_ENT_DATE                       " & vbNewLine _
                        & "                AND  IN1.SYS_ENT_TIME   = OUTWK.SYS_ENT_TIME                       " & vbNewLine _
                        & "                AND  IN1.INPUT_KBN      = H_UNSOEDI_DTL_NCGO.INPUT_KBN          " & vbNewLine _
                        & "                AND  IN1.AKADEN_KBN     = H_UNSOEDI_DTL_NCGO.AKADEN_KBN         " & vbNewLine _
                        & "   WHERE                                                                        " & vbNewLine _
                        & "       H_UNSOEDI_DTL_NCGO.SYS_DEL_FLG       =  IN1.SYS_DEL_FLG                  " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.DEL_KB            =  IN1.DEL_KB                       " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.SYS_ENT_DATE      =  @SYS_UPD_DATE --今回取込日付     " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.SYS_ENT_TIME      =  @SYS_UPD_TIME --今回取込時間     " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.OUTKA_DENP_NO     =  IN1.OUTKA_DENP_NO                " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.OUTKA_DENP_DTL_NO =  IN1.OUTKA_DENP_DTL_NO            " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.INPUT_KBN         =  IN1.INPUT_KBN                    " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.AKADEN_KBN        =  IN1.AKADEN_KBN                   " & vbNewLine _
                        & "   AND IN1.OUTKA_DENP_DTL_NO <>  ''                                             " & vbNewLine _
                        & "   AND H_UNSOEDI_DTL_NCGO.DATA_ID_DETAIL    =  'H'  --ADD 2019/02/07 受注返品時だけ処理する(転送時：出荷・入荷とも同じ出荷伝票No.で作成されるため)  " & vbNewLine

#End Region ' "H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)"

#End Region ' "セミEDI処理"



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
                                              & ",''                           " & vbNewLine _
                                              & ",@LOT_NO                      " & vbNewLine _
                                              & ",@LOCA                        " & vbNewLine _
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
                                              & ",''                           " & vbNewLine _
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

#Region "SEND"
    Private Const SQL_INSERT_SEND As String = "INSERT INTO $LM_TRN$..H_SENDINEDI_NCGO " & vbNewLine _
                                              & "(                              " & vbNewLine _
                                              & "DEL_KB                         " & vbNewLine _
                                              & ",NRS_BR_CD                     " & vbNewLine _
                                              & ",EDI_CTL_NO                    " & vbNewLine _
                                              & ",EDI_CTL_NO_EDA                " & vbNewLine _
                                              & ",INKA_CTL_NO_L                 " & vbNewLine _
                                              & ",FILE_NAME                     " & vbNewLine _
                                              & ",REC_NO                        " & vbNewLine _
                                              & ",RCV_ID                        " & vbNewLine _
                                              & ",RCV_UKETSUKE_NO               " & vbNewLine _
                                              & ",RCV_UKETSUKE_NO_EDA           " & vbNewLine _
                                              & ",RCV_INPUT_KB                  " & vbNewLine _
                                              & ",RCV_EDA_UP_FLG                " & vbNewLine _
                                              & ",ID                            " & vbNewLine _
                                              & ",SYSTEM_KB                     " & vbNewLine _
                                              & ",UKETSUKE_NO                   " & vbNewLine _
                                              & ",UKETSUKE_NO_EDA               " & vbNewLine _
                                              & ",COMPANY_CD                    " & vbNewLine _
                                              & ",BASHO_CD                      " & vbNewLine _
                                              & ",INKA_BUMON                    " & vbNewLine _
                                              & ",INKA_GROUP                    " & vbNewLine _
                                              & ",INPUT_YMD                     " & vbNewLine _
                                              & ",INKA_YMD                      " & vbNewLine _
                                              & ",INPUT_KB                      " & vbNewLine _
                                              & ",GOODS_RYAKU                   " & vbNewLine _
                                              & ",GRADE_1                       " & vbNewLine _
                                              & ",GRADE_2                       " & vbNewLine _
                                              & ",YORYO                         " & vbNewLine _
                                              & ",NISUGATA_CD                   " & vbNewLine _
                                              & ",YUSHUTSU_KB                   " & vbNewLine _
                                              & ",ZAIKO_KB                      " & vbNewLine _
                                              & ",INKA_BASHO_SP                 " & vbNewLine _
                                              & ",URI_BASHO                     " & vbNewLine _
                                              & ",URI_BUMON                     " & vbNewLine _
                                              & ",URI_GROUP                     " & vbNewLine _
                                              & ",SENPO_KENSHU_SURYO            " & vbNewLine _
                                              & ",LOT_NO_1                      " & vbNewLine _
                                              & ",LOT_NO2_1                     " & vbNewLine _
                                              & ",KOSU_1                        " & vbNewLine _
                                              & ",SURYO_1                       " & vbNewLine _
                                              & ",LOT_NO_2                      " & vbNewLine _
                                              & ",LOT_NO2_2                     " & vbNewLine _
                                              & ",KOSU_2                        " & vbNewLine _
                                              & ",SURYO_2                       " & vbNewLine _
                                              & ",LOT_NO_3                      " & vbNewLine _
                                              & ",LOT_NO2_3                     " & vbNewLine _
                                              & ",KOSU_3                        " & vbNewLine _
                                              & ",SURYO_3                       " & vbNewLine _
                                              & ",LOT_NO_4                      " & vbNewLine _
                                              & ",LOT_NO2_4                     " & vbNewLine _
                                              & ",KOSU_4                        " & vbNewLine _
                                              & ",SURYO_4                       " & vbNewLine _
                                              & ",LOT_NO_5                      " & vbNewLine _
                                              & ",LOT_NO2_5                     " & vbNewLine _
                                              & ",KOSU_5                        " & vbNewLine _
                                              & ",SURYO_5                       " & vbNewLine _
                                              & ",TTL_KOSU                      " & vbNewLine _
                                              & ",TTL_SURYO                     " & vbNewLine _
                                              & ",IN_BIKO_ANK                   " & vbNewLine _
                                              & ",IN_BIKO_BIKO                  " & vbNewLine _
                                              & ",OUT_BIKO_ANK                  " & vbNewLine _
                                              & ",OUT_BIKO_BIKO                 " & vbNewLine _
                                              & ",SHORI_NO                      " & vbNewLine _
                                              & ",SHORI_NO_EDA                  " & vbNewLine _
                                              & ",ERROR_FLG                     " & vbNewLine _
                                              & ",KO_UKETSUKE_NO                " & vbNewLine _
                                              & ",KO_UKETSUKE_NO_EDA            " & vbNewLine _
                                              & ",GEKKAN_KEIYAKU_NO             " & vbNewLine _
                                              & ",KOBETSU_NISUGATA_CD_1         " & vbNewLine _
                                              & ",KENTEI_KB_1                   " & vbNewLine _
                                              & ",HOKAN_ICHI_11                 " & vbNewLine _
                                              & ",HOKAN_KOSU_11                 " & vbNewLine _
                                              & ",HOKAN_SURYO_11                " & vbNewLine _
                                              & ",HOKAN_ICHI_12                 " & vbNewLine _
                                              & ",HOKAN_KOSU_12                 " & vbNewLine _
                                              & ",HOKAN_SURYO_12                " & vbNewLine _
                                              & ",HOKAN_ICHI_13                 " & vbNewLine _
                                              & ",HOKAN_KOSU_13                 " & vbNewLine _
                                              & ",HOKAN_SURYO_13                " & vbNewLine _
                                              & ",HOKAN_ICHI_14                 " & vbNewLine _
                                              & ",HOKAN_KOSU_14                 " & vbNewLine _
                                              & ",HOKAN_SURYO_14                " & vbNewLine _
                                              & ",KOBETSU_NISUGATA_CD_2         " & vbNewLine _
                                              & ",KENTEI_KB_2                   " & vbNewLine _
                                              & ",HOKAN_ICHI_21                 " & vbNewLine _
                                              & ",HOKAN_KOSU_21                 " & vbNewLine _
                                              & ",HOKAN_SURYO_21                " & vbNewLine _
                                              & ",HOKAN_ICHI_22                 " & vbNewLine _
                                              & ",HOKAN_KOSU_22                 " & vbNewLine _
                                              & ",HOKAN_SURYO_22                " & vbNewLine _
                                              & ",HOKAN_ICHI_23                 " & vbNewLine _
                                              & ",HOKAN_KOSU_23                 " & vbNewLine _
                                              & ",HOKAN_SURYO_23                " & vbNewLine _
                                              & ",HOKAN_ICHI_24                 " & vbNewLine _
                                              & ",HOKAN_KOSU_24                 " & vbNewLine _
                                              & ",HOKAN_SURYO_24                " & vbNewLine _
                                              & ",KOBETSU_NISUGATA_CD_3         " & vbNewLine _
                                              & ",KENTEI_KB_3                   " & vbNewLine _
                                              & ",HOKAN_ICHI_31                 " & vbNewLine _
                                              & ",HOKAN_KOSU_31                 " & vbNewLine _
                                              & ",HOKAN_SURYO_31                " & vbNewLine _
                                              & ",HOKAN_ICHI_32                 " & vbNewLine _
                                              & ",HOKAN_KOSU_32                 " & vbNewLine _
                                              & ",HOKAN_SURYO_32                " & vbNewLine _
                                              & ",HOKAN_ICHI_33                 " & vbNewLine _
                                              & ",HOKAN_KOSU_33                 " & vbNewLine _
                                              & ",HOKAN_SURYO_33                " & vbNewLine _
                                              & ",HOKAN_ICHI_34                 " & vbNewLine _
                                              & ",HOKAN_KOSU_34                 " & vbNewLine _
                                              & ",HOKAN_SURYO_34                " & vbNewLine _
                                              & ",KOBETSU_NISUGATA_CD_4         " & vbNewLine _
                                              & ",KENTEI_KB_4                   " & vbNewLine _
                                              & ",HOKAN_ICHI_41                 " & vbNewLine _
                                              & ",HOKAN_KOSU_41                 " & vbNewLine _
                                              & ",HOKAN_SURYO_41                " & vbNewLine _
                                              & ",HOKAN_ICHI_42                 " & vbNewLine _
                                              & ",HOKAN_KOSU_42                 " & vbNewLine _
                                              & ",HOKAN_SURYO_42                " & vbNewLine _
                                              & ",HOKAN_ICHI_43                 " & vbNewLine _
                                              & ",HOKAN_KOSU_43                 " & vbNewLine _
                                              & ",HOKAN_SURYO_43                " & vbNewLine _
                                              & ",HOKAN_ICHI_44                 " & vbNewLine _
                                              & ",HOKAN_KOSU_44                 " & vbNewLine _
                                              & ",HOKAN_SURYO_44                " & vbNewLine _
                                              & ",KOBETSU_NISUGATA_CD_5         " & vbNewLine _
                                              & ",KENTEI_KB_5                   " & vbNewLine _
                                              & ",HOKAN_ICHI_51                 " & vbNewLine _
                                              & ",HOKAN_KOSU_51                 " & vbNewLine _
                                              & ",HOKAN_SURYO_51                " & vbNewLine _
                                              & ",HOKAN_ICHI_52                 " & vbNewLine _
                                              & ",HOKAN_KOSU_52                 " & vbNewLine _
                                              & ",HOKAN_SURYO_52                " & vbNewLine _
                                              & ",HOKAN_ICHI_53                 " & vbNewLine _
                                              & ",HOKAN_KOSU_53                 " & vbNewLine _
                                              & ",HOKAN_SURYO_53                " & vbNewLine _
                                              & ",HOKAN_ICHI_54                 " & vbNewLine _
                                              & ",HOKAN_KOSU_54                 " & vbNewLine _
                                              & ",HOKAN_SURYO_54                " & vbNewLine _
                                              & ",INKA_SOKUSHORI_KB             " & vbNewLine _
                                              & ",GENKA_BUMON                   " & vbNewLine _
                                              & ",BIN_KB                        " & vbNewLine _
                                              & ",YUSO_COMP_CD                  " & vbNewLine _
                                              & ",JUST_OUTKA_KB                 " & vbNewLine _
                                              & ",SHUGENRYO_KB                  " & vbNewLine _
                                              & ",GEKKAN_KB                     " & vbNewLine _
                                              & ",YOBI                          " & vbNewLine _
                                              & ",YOBI2                         " & vbNewLine _
                                              & ",ERROR_MSG_1                   " & vbNewLine _
                                              & ",ERROR_MSG_2                   " & vbNewLine _
                                              & ",ERROR_MSG_3                   " & vbNewLine _
                                              & ",ERROR_MSG_4                   " & vbNewLine _
                                              & ",ERROR_MSG_5                   " & vbNewLine _
                                              & ",RECORD_STATUS                 " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG             " & vbNewLine _
                                              & ",JISSEKI_USER                  " & vbNewLine _
                                              & ",JISSEKI_DATE                  " & vbNewLine _
                                              & ",JISSEKI_TIME                  " & vbNewLine _
                                              & ",SEND_USER                     " & vbNewLine _
                                              & ",SEND_DATE                     " & vbNewLine _
                                              & ",SEND_TIME                     " & vbNewLine _
                                              & ",ERR_RCV_USER                  " & vbNewLine _
                                              & ",ERR_RCV_DATE                  " & vbNewLine _
                                              & ",ERR_RCV_TIME                  " & vbNewLine _
                                              & ",CRT_USER                      " & vbNewLine _
                                              & ",CRT_DATE                      " & vbNewLine _
                                              & ",CRT_TIME                      " & vbNewLine _
                                              & ",UPD_USER                      " & vbNewLine _
                                              & ",UPD_DATE                      " & vbNewLine _
                                              & ",UPD_TIME                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                  " & vbNewLine _
                                              & ",SYS_ENT_TIME                  " & vbNewLine _
                                              & ",SYS_ENT_PGID                  " & vbNewLine _
                                              & ",SYS_ENT_USER                  " & vbNewLine _
                                              & ",SYS_UPD_DATE                  " & vbNewLine _
                                              & ",SYS_UPD_TIME                  " & vbNewLine _
                                              & ",SYS_UPD_PGID                  " & vbNewLine _
                                              & ",SYS_UPD_USER                  " & vbNewLine _
                                              & ",SYS_DEL_FLG                   " & vbNewLine _
                                              & " ) VALUES (                    " & vbNewLine _
                                              & " @DEL_KB                       " & vbNewLine _
                                              & ",@NRS_BR_CD                    " & vbNewLine _
                                              & ",@EDI_CTL_NO                   " & vbNewLine _
                                              & ",@EDI_CTL_NO_EDA               " & vbNewLine _
                                              & ",@INKA_CTL_NO_L                " & vbNewLine _
                                              & ",@FILE_NAME                    " & vbNewLine _
                                              & ",@REC_NO                       " & vbNewLine _
                                              & ",@RCV_ID                       " & vbNewLine _
                                              & ",@RCV_UKETSUKE_NO              " & vbNewLine _
                                              & ",@RCV_UKETSUKE_NO_EDA          " & vbNewLine _
                                              & ",@RCV_INPUT_KB                 " & vbNewLine _
                                              & ",@RCV_EDA_UP_FLG               " & vbNewLine _
                                              & ",@ID                           " & vbNewLine _
                                              & ",@SYSTEM_KB                    " & vbNewLine _
                                              & ",@UKETSUKE_NO                  " & vbNewLine _
                                              & ",@UKETSUKE_NO_EDA              " & vbNewLine _
                                              & ",@COMPANY_CD                   " & vbNewLine _
                                              & ",@BASHO_CD                     " & vbNewLine _
                                              & ",@INKA_BUMON                   " & vbNewLine _
                                              & ",@INKA_GROUP                   " & vbNewLine _
                                              & ",@INPUT_YMD                    " & vbNewLine _
                                              & ",@INKA_YMD                     " & vbNewLine _
                                              & ",@INPUT_KB                     " & vbNewLine _
                                              & ",@GOODS_RYAKU                  " & vbNewLine _
                                              & ",@GRADE_1                      " & vbNewLine _
                                              & ",@GRADE_2                      " & vbNewLine _
                                              & ",@YORYO                        " & vbNewLine _
                                              & ",@NISUGATA_CD                  " & vbNewLine _
                                              & ",@YUSHUTSU_KB                  " & vbNewLine _
                                              & ",@ZAIKO_KB                     " & vbNewLine _
                                              & ",@INKA_BASHO_SP                " & vbNewLine _
                                              & ",@URI_BASHO                    " & vbNewLine _
                                              & ",@URI_BUMON                    " & vbNewLine _
                                              & ",@URI_GROUP                    " & vbNewLine _
                                              & ",@SENPO_KENSHU_SURYO           " & vbNewLine _
                                              & ",@LOT_NO_1                     " & vbNewLine _
                                              & ",@LOT_NO2_1                    " & vbNewLine _
                                              & ",@KOSU_1                       " & vbNewLine _
                                              & ",@SURYO_1                      " & vbNewLine _
                                              & ",@LOT_NO_2                     " & vbNewLine _
                                              & ",@LOT_NO2_2                    " & vbNewLine _
                                              & ",@KOSU_2                       " & vbNewLine _
                                              & ",@SURYO_2                      " & vbNewLine _
                                              & ",@LOT_NO_3                     " & vbNewLine _
                                              & ",@LOT_NO2_3                    " & vbNewLine _
                                              & ",@KOSU_3                       " & vbNewLine _
                                              & ",@SURYO_3                      " & vbNewLine _
                                              & ",@LOT_NO_4                     " & vbNewLine _
                                              & ",@LOT_NO2_4                    " & vbNewLine _
                                              & ",@KOSU_4                       " & vbNewLine _
                                              & ",@SURYO_4                      " & vbNewLine _
                                              & ",@LOT_NO_5                     " & vbNewLine _
                                              & ",@LOT_NO2_5                    " & vbNewLine _
                                              & ",@KOSU_5                       " & vbNewLine _
                                              & ",@SURYO_5                      " & vbNewLine _
                                              & ",@TTL_KOSU                     " & vbNewLine _
                                              & ",@TTL_SURYO                    " & vbNewLine _
                                              & ",@IN_BIKO_ANK                  " & vbNewLine _
                                              & ",@IN_BIKO_BIKO                 " & vbNewLine _
                                              & ",@OUT_BIKO_ANK                 " & vbNewLine _
                                              & ",@OUT_BIKO_BIKO                " & vbNewLine _
                                              & ",@SHORI_NO                     " & vbNewLine _
                                              & ",@SHORI_NO_EDA                 " & vbNewLine _
                                              & ",@ERROR_FLG                    " & vbNewLine _
                                              & ",@KO_UKETSUKE_NO               " & vbNewLine _
                                              & ",@KO_UKETSUKE_NO_EDA           " & vbNewLine _
                                              & ",@GEKKAN_KEIYAKU_NO            " & vbNewLine _
                                              & ",@KOBETSU_NISUGATA_CD_1        " & vbNewLine _
                                              & ",@KENTEI_KB_1                  " & vbNewLine _
                                              & ",@HOKAN_ICHI_11                " & vbNewLine _
                                              & ",@HOKAN_KOSU_11                " & vbNewLine _
                                              & ",@HOKAN_SURYO_11               " & vbNewLine _
                                              & ",@HOKAN_ICHI_12                " & vbNewLine _
                                              & ",@HOKAN_KOSU_12                " & vbNewLine _
                                              & ",@HOKAN_SURYO_12               " & vbNewLine _
                                              & ",@HOKAN_ICHI_13                " & vbNewLine _
                                              & ",@HOKAN_KOSU_13                " & vbNewLine _
                                              & ",@HOKAN_SURYO_13               " & vbNewLine _
                                              & ",@HOKAN_ICHI_14                " & vbNewLine _
                                              & ",@HOKAN_KOSU_14                " & vbNewLine _
                                              & ",@HOKAN_SURYO_14               " & vbNewLine _
                                              & ",@KOBETSU_NISUGATA_CD_2        " & vbNewLine _
                                              & ",@KENTEI_KB_2                  " & vbNewLine _
                                              & ",@HOKAN_ICHI_21                " & vbNewLine _
                                              & ",@HOKAN_KOSU_21                " & vbNewLine _
                                              & ",@HOKAN_SURYO_21               " & vbNewLine _
                                              & ",@HOKAN_ICHI_22                " & vbNewLine _
                                              & ",@HOKAN_KOSU_22                " & vbNewLine _
                                              & ",@HOKAN_SURYO_22               " & vbNewLine _
                                              & ",@HOKAN_ICHI_23                " & vbNewLine _
                                              & ",@HOKAN_KOSU_23                " & vbNewLine _
                                              & ",@HOKAN_SURYO_23               " & vbNewLine _
                                              & ",@HOKAN_ICHI_24                " & vbNewLine _
                                              & ",@HOKAN_KOSU_24                " & vbNewLine _
                                              & ",@HOKAN_SURYO_24               " & vbNewLine _
                                              & ",@KOBETSU_NISUGATA_CD_3        " & vbNewLine _
                                              & ",@KENTEI_KB_3                  " & vbNewLine _
                                              & ",@HOKAN_ICHI_31                " & vbNewLine _
                                              & ",@HOKAN_KOSU_31                " & vbNewLine _
                                              & ",@HOKAN_SURYO_31               " & vbNewLine _
                                              & ",@HOKAN_ICHI_32                " & vbNewLine _
                                              & ",@HOKAN_KOSU_32                " & vbNewLine _
                                              & ",@HOKAN_SURYO_32               " & vbNewLine _
                                              & ",@HOKAN_ICHI_33                " & vbNewLine _
                                              & ",@HOKAN_KOSU_33                " & vbNewLine _
                                              & ",@HOKAN_SURYO_33               " & vbNewLine _
                                              & ",@HOKAN_ICHI_34                " & vbNewLine _
                                              & ",@HOKAN_KOSU_34                " & vbNewLine _
                                              & ",@HOKAN_SURYO_34               " & vbNewLine _
                                              & ",@KOBETSU_NISUGATA_CD_4        " & vbNewLine _
                                              & ",@KENTEI_KB_4                  " & vbNewLine _
                                              & ",@HOKAN_ICHI_41                " & vbNewLine _
                                              & ",@HOKAN_KOSU_41                " & vbNewLine _
                                              & ",@HOKAN_SURYO_41               " & vbNewLine _
                                              & ",@HOKAN_ICHI_42                " & vbNewLine _
                                              & ",@HOKAN_KOSU_42                " & vbNewLine _
                                              & ",@HOKAN_SURYO_42               " & vbNewLine _
                                              & ",@HOKAN_ICHI_43                " & vbNewLine _
                                              & ",@HOKAN_KOSU_43                " & vbNewLine _
                                              & ",@HOKAN_SURYO_43               " & vbNewLine _
                                              & ",@HOKAN_ICHI_44                " & vbNewLine _
                                              & ",@HOKAN_KOSU_44                " & vbNewLine _
                                              & ",@HOKAN_SURYO_44               " & vbNewLine _
                                              & ",@KOBETSU_NISUGATA_CD_5        " & vbNewLine _
                                              & ",@KENTEI_KB_5                  " & vbNewLine _
                                              & ",@HOKAN_ICHI_51                " & vbNewLine _
                                              & ",@HOKAN_KOSU_51                " & vbNewLine _
                                              & ",@HOKAN_SURYO_51               " & vbNewLine _
                                              & ",@HOKAN_ICHI_52                " & vbNewLine _
                                              & ",@HOKAN_KOSU_52                " & vbNewLine _
                                              & ",@HOKAN_SURYO_52               " & vbNewLine _
                                              & ",@HOKAN_ICHI_53                " & vbNewLine _
                                              & ",@HOKAN_KOSU_53                " & vbNewLine _
                                              & ",@HOKAN_SURYO_53               " & vbNewLine _
                                              & ",@HOKAN_ICHI_54                " & vbNewLine _
                                              & ",@HOKAN_KOSU_54                " & vbNewLine _
                                              & ",@HOKAN_SURYO_54               " & vbNewLine _
                                              & ",@INKA_SOKUSHORI_KB            " & vbNewLine _
                                              & ",@GENKA_BUMON                  " & vbNewLine _
                                              & ",@BIN_KB                       " & vbNewLine _
                                              & ",@YUSO_COMP_CD                 " & vbNewLine _
                                              & ",@JUST_OUTKA_KB                " & vbNewLine _
                                              & ",@SHUGENRYO_KB                 " & vbNewLine _
                                              & ",@GEKKAN_KB                    " & vbNewLine _
                                              & ",@YOBI                         " & vbNewLine _
                                              & ",@YOBI2                        " & vbNewLine _
                                              & ",@ERROR_MSG_1                  " & vbNewLine _
                                              & ",@ERROR_MSG_2                  " & vbNewLine _
                                              & ",@ERROR_MSG_3                  " & vbNewLine _
                                              & ",@ERROR_MSG_4                  " & vbNewLine _
                                              & ",@ERROR_MSG_5                  " & vbNewLine _
                                              & ",@RECORD_STATUS                " & vbNewLine _
                                              & ",@JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",@JISSEKI_USER                 " & vbNewLine _
                                              & ",@JISSEKI_DATE                 " & vbNewLine _
                                              & ",@JISSEKI_TIME                 " & vbNewLine _
                                              & ",@SEND_USER                    " & vbNewLine _
                                              & ",@SEND_DATE                    " & vbNewLine _
                                              & ",@SEND_TIME                    " & vbNewLine _
                                              & ",@ERR_RCV_USER                 " & vbNewLine _
                                              & ",@ERR_RCV_DATE                 " & vbNewLine _
                                              & ",@ERR_RCV_TIME                 " & vbNewLine _
                                              & ",@SYS_UPD_USER                 " & vbNewLine _
                                              & ",@SYS_UPD_DATE                 " & vbNewLine _
                                              & ",@CRT_TIME                     " & vbNewLine _
                                              & ",@SYS_UPD_USER                 " & vbNewLine _
                                              & ",@SYS_UPD_DATE                 " & vbNewLine _
                                              & ",@UPD_TIME                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                 " & vbNewLine _
                                              & ",@SYS_ENT_TIME                 " & vbNewLine _
                                              & ",@SYS_ENT_PGID                 " & vbNewLine _
                                              & ",@SYS_ENT_USER                 " & vbNewLine _
                                              & ",@SYS_UPD_DATE                 " & vbNewLine _
                                              & ",@SYS_UPD_TIME                 " & vbNewLine _
                                              & ",@SYS_UPD_PGID                 " & vbNewLine _
                                              & ",@SYS_UPD_USER                 " & vbNewLine _
                                              & ",'0'                           " & vbNewLine _
                                              & ")                              " & vbNewLine


#End Region



#Region "M品振替"

#Region "C_OUTKA_L"

    Private Const SQL_INSERT_OUTKA_L As String _
        = " INSERT INTO $LM_TRN$..C_OUTKA_L                        " & vbNewLine _
        & "           ( NRS_BR_CD                                  " & vbNewLine _
        & "           , OUTKA_NO_L                                 " & vbNewLine _
        & "           , FURI_NO                                    " & vbNewLine _
        & "           , OUTKA_KB                                   " & vbNewLine _
        & "           , SYUBETU_KB                                 " & vbNewLine _
        & "           , OUTKA_STATE_KB                             " & vbNewLine _
        & "           , OUTKAHOKOKU_YN                             " & vbNewLine _
        & "           , PICK_KB                                    " & vbNewLine _
        & "           , DENP_NO                                    " & vbNewLine _
        & "           , ARR_KANRYO_INFO                            " & vbNewLine _
        & "           , WH_CD                                      " & vbNewLine _
        & "           , OUTKA_PLAN_DATE                            " & vbNewLine _
        & "           , OUTKO_DATE                                 " & vbNewLine _
        & "           , ARR_PLAN_DATE                              " & vbNewLine _
        & "           , ARR_PLAN_TIME                              " & vbNewLine _
        & "           , HOKOKU_DATE                                " & vbNewLine _
        & "           , TOUKI_HOKAN_YN                             " & vbNewLine _
        & "           , END_DATE                                   " & vbNewLine _
        & "           , CUST_CD_L                                  " & vbNewLine _
        & "           , CUST_CD_M                                  " & vbNewLine _
        & "           , SHIP_CD_L                                  " & vbNewLine _
        & "           , SHIP_CD_M                                  " & vbNewLine _
        & "           , DEST_CD                                    " & vbNewLine _
        & "           , DEST_AD_3                                  " & vbNewLine _
        & "           , DEST_TEL                                   " & vbNewLine _
        & "           , NHS_REMARK                                 " & vbNewLine _
        & "           , SP_NHS_KB                                  " & vbNewLine _
        & "           , COA_YN                                     " & vbNewLine _
        & "           , CUST_ORD_NO                                " & vbNewLine _
        & "           , BUYER_ORD_NO                               " & vbNewLine _
        & "           , REMARK                                     " & vbNewLine _
        & "           , OUTKA_PKG_NB                               " & vbNewLine _
        & "           , DENP_YN                                    " & vbNewLine _
        & "           , PC_KB                                      " & vbNewLine _
        & "           , NIYAKU_YN                                  " & vbNewLine _
        & "           , DEST_KB                                    " & vbNewLine _
        & "           , DEST_NM                                    " & vbNewLine _
        & "           , DEST_AD_1                                  " & vbNewLine _
        & "           , DEST_AD_2                                  " & vbNewLine _
        & "           , ALL_PRINT_FLAG                             " & vbNewLine _
        & "           , NIHUDA_FLAG                                " & vbNewLine _
        & "           , NHS_FLAG                                   " & vbNewLine _
        & "           , DENP_FLAG                                  " & vbNewLine _
        & "           , COA_FLAG                                   " & vbNewLine _
        & "           , HOKOKU_FLAG                                " & vbNewLine _
        & "           , MATOME_PICK_FLAG                           " & vbNewLine _
        & "           , MATOME_PRINT_DATE                          " & vbNewLine _
        & "           , MATOME_PRINT_TIME                          " & vbNewLine _
        & "           , LAST_PRINT_DATE                            " & vbNewLine _
        & "           , LAST_PRINT_TIME                            " & vbNewLine _
        & "           , SASZ_USER                                  " & vbNewLine _
        & "           , OUTKO_USER                                 " & vbNewLine _
        & "           , KEN_USER                                   " & vbNewLine _
        & "           , OUTKA_USER                                 " & vbNewLine _
        & "           , HOU_USER                                   " & vbNewLine _
        & "           , ORDER_TYPE                                 " & vbNewLine _
        & "           , WH_KENPIN_WK_STATUS                        " & vbNewLine _
        & "           , SYS_ENT_DATE                               " & vbNewLine _
        & "           , SYS_ENT_TIME                               " & vbNewLine _
        & "           , SYS_ENT_PGID                               " & vbNewLine _
        & "           , SYS_ENT_USER                               " & vbNewLine _
        & "           , SYS_UPD_DATE                               " & vbNewLine _
        & "           , SYS_UPD_TIME                               " & vbNewLine _
        & "           , SYS_UPD_PGID                               " & vbNewLine _
        & "           , SYS_UPD_USER                               " & vbNewLine _
        & "           , SYS_DEL_FLG)                               " & vbNewLine _
        & "      VALUES                                            " & vbNewLine _
        & "            (@NRS_BR_CD                                 " & vbNewLine _
        & "           , @OUTKA_NO_L                                " & vbNewLine _
        & "           , @FURI_NO                                   " & vbNewLine _
        & "           , @OUTKA_KB                                  " & vbNewLine _
        & "           , @SYUBETU_KB                                " & vbNewLine _
        & "           , @OUTKA_STATE_KB                            " & vbNewLine _
        & "           , @OUTKAHOKOKU_YN                            " & vbNewLine _
        & "           , @PICK_KB                                   " & vbNewLine _
        & "           , @DENP_NO                                   " & vbNewLine _
        & "           , @ARR_KANRYO_INFO                           " & vbNewLine _
        & "           , @WH_CD                                     " & vbNewLine _
        & "           , @OUTKA_PLAN_DATE                           " & vbNewLine _
        & "           , @OUTKO_DATE                                " & vbNewLine _
        & "           , @ARR_PLAN_DATE                             " & vbNewLine _
        & "           , @ARR_PLAN_TIME                             " & vbNewLine _
        & "           , @HOKOKU_DATE                               " & vbNewLine _
        & "           , @TOUKI_HOKAN_YN                            " & vbNewLine _
        & "           , @END_DATE                                  " & vbNewLine _
        & "           , @CUST_CD_L                                 " & vbNewLine _
        & "           , @CUST_CD_M                                 " & vbNewLine _
        & "           , @SHIP_CD_L                                 " & vbNewLine _
        & "           , @SHIP_CD_M                                 " & vbNewLine _
        & "           , @DEST_CD                                   " & vbNewLine _
        & "           , @DEST_AD_3                                 " & vbNewLine _
        & "           , @DEST_TEL                                  " & vbNewLine _
        & "           , @NHS_REMARK                                " & vbNewLine _
        & "           , @SP_NHS_KB                                 " & vbNewLine _
        & "           , @COA_YN                                    " & vbNewLine _
        & "           , @CUST_ORD_NO                               " & vbNewLine _
        & "           , @BUYER_ORD_NO                              " & vbNewLine _
        & "           , @REMARK                                    " & vbNewLine _
        & "           , @OUTKA_PKG_NB                              " & vbNewLine _
        & "           , @DENP_YN                                   " & vbNewLine _
        & "           , @PC_KB                                     " & vbNewLine _
        & "           , @NIYAKU_YN                                 " & vbNewLine _
        & "           , @DEST_KB                                   " & vbNewLine _
        & "           , @DEST_NM                                   " & vbNewLine _
        & "           , @DEST_AD_1                                 " & vbNewLine _
        & "           , @DEST_AD_2                                 " & vbNewLine _
        & "           , @ALL_PRINT_FLAG                            " & vbNewLine _
        & "           , @NIHUDA_FLAG                               " & vbNewLine _
        & "           , @NHS_FLAG                                  " & vbNewLine _
        & "           , @DENP_FLAG                                 " & vbNewLine _
        & "           , @COA_FLAG                                  " & vbNewLine _
        & "           , @HOKOKU_FLAG                               " & vbNewLine _
        & "           , @MATOME_PICK_FLAG                          " & vbNewLine _
        & "           , @MATOME_PRINT_DATE                         " & vbNewLine _
        & "           , @MATOME_PRINT_TIME                         " & vbNewLine _
        & "           , @LAST_PRINT_DATE                           " & vbNewLine _
        & "           , @LAST_PRINT_TIME                           " & vbNewLine _
        & "           , @SASZ_USER                                 " & vbNewLine _
        & "           , @OUTKO_USER                                " & vbNewLine _
        & "           , @KEN_USER                                  " & vbNewLine _
        & "           , @OUTKA_USER                                " & vbNewLine _
        & "           , @HOU_USER                                  " & vbNewLine _
        & "           , @ORDER_TYPE                                " & vbNewLine _
        & "           , @WH_KENPIN_WK_STATUS                       " & vbNewLine _
        & "           , @SYS_ENT_DATE                              " & vbNewLine _
        & "           , @SYS_ENT_TIME                              " & vbNewLine _
        & "           , @SYS_ENT_PGID                              " & vbNewLine _
        & "           , @SYS_ENT_USER                              " & vbNewLine _
        & "           , @SYS_UPD_DATE                              " & vbNewLine _
        & "           , @SYS_UPD_TIME                              " & vbNewLine _
        & "           , @SYS_UPD_PGID                              " & vbNewLine _
        & "           , @SYS_UPD_USER                              " & vbNewLine _
        & "           , @SYS_DEL_FLG)                              " & vbNewLine

#End Region

#Region "C_OUTKA_M"


    Private Const SQL_INSERT_OUTKA_M As String _
        = " INSERT INTO $LM_TRN$..C_OUTKA_M                   " & vbNewLine _
        & "           ( NRS_BR_CD                           " & vbNewLine _
        & "           , OUTKA_NO_L                          " & vbNewLine _
        & "           , OUTKA_NO_M                          " & vbNewLine _
        & "           , EDI_SET_NO                          " & vbNewLine _
        & "           , COA_YN                              " & vbNewLine _
        & "           , CUST_ORD_NO_DTL                     " & vbNewLine _
        & "           , BUYER_ORD_NO_DTL                    " & vbNewLine _
        & "           , GOODS_CD_NRS                        " & vbNewLine _
        & "           , RSV_NO                              " & vbNewLine _
        & "           , LOT_NO                              " & vbNewLine _
        & "           , SERIAL_NO                           " & vbNewLine _
        & "           , ALCTD_KB                            " & vbNewLine _
        & "           , OUTKA_PKG_NB                        " & vbNewLine _
        & "           , OUTKA_HASU                          " & vbNewLine _
        & "           , OUTKA_QT                            " & vbNewLine _
        & "           , OUTKA_TTL_NB                        " & vbNewLine _
        & "           , OUTKA_TTL_QT                        " & vbNewLine _
        & "           , ALCTD_NB                            " & vbNewLine _
        & "           , ALCTD_QT                            " & vbNewLine _
        & "           , BACKLOG_NB                          " & vbNewLine _
        & "           , BACKLOG_QT                          " & vbNewLine _
        & "           , UNSO_ONDO_KB                        " & vbNewLine _
        & "           , IRIME                               " & vbNewLine _
        & "           , IRIME_UT                            " & vbNewLine _
        & "           , OUTKA_M_PKG_NB                      " & vbNewLine _
        & "           , REMARK                              " & vbNewLine _
        & "           , SIZE_KB                             " & vbNewLine _
        & "           , ZAIKO_KB                            " & vbNewLine _
        & "           , SOURCE_CD                           " & vbNewLine _
        & "           , YELLOW_CARD                         " & vbNewLine _
        & "           , GOODS_CD_NRS_FROM                   " & vbNewLine _
        & "           , PRINT_SORT                          " & vbNewLine _
        & "           , SYS_ENT_DATE                        " & vbNewLine _
        & "           , SYS_ENT_TIME                        " & vbNewLine _
        & "           , SYS_ENT_PGID                        " & vbNewLine _
        & "           , SYS_ENT_USER                        " & vbNewLine _
        & "           , SYS_UPD_DATE                        " & vbNewLine _
        & "           , SYS_UPD_TIME                        " & vbNewLine _
        & "           , SYS_UPD_PGID                        " & vbNewLine _
        & "           , SYS_UPD_USER                        " & vbNewLine _
        & "           , SYS_DEL_FLG)                        " & vbNewLine _
        & "      VALUES                                     " & vbNewLine _
        & "           ( @NRS_BR_CD                          " & vbNewLine _
        & "           , @OUTKA_NO_L                         " & vbNewLine _
        & "           , @OUTKA_NO_M                         " & vbNewLine _
        & "           , @EDI_SET_NO                         " & vbNewLine _
        & "           , @COA_YN                             " & vbNewLine _
        & "           , @CUST_ORD_NO_DTL                    " & vbNewLine _
        & "           , @BUYER_ORD_NO_DTL                   " & vbNewLine _
        & "           , @GOODS_CD_NRS                       " & vbNewLine _
        & "           , @RSV_NO                             " & vbNewLine _
        & "           , @LOT_NO                             " & vbNewLine _
        & "           , @SERIAL_NO                          " & vbNewLine _
        & "           , @ALCTD_KB                           " & vbNewLine _
        & "           , @OUTKA_PKG_NB                       " & vbNewLine _
        & "           , @OUTKA_HASU                         " & vbNewLine _
        & "           , @OUTKA_QT                           " & vbNewLine _
        & "           , @OUTKA_TTL_NB                       " & vbNewLine _
        & "           , @OUTKA_TTL_QT                       " & vbNewLine _
        & "           , @ALCTD_NB                           " & vbNewLine _
        & "           , @ALCTD_QT                           " & vbNewLine _
        & "           , @BACKLOG_NB                         " & vbNewLine _
        & "           , @BACKLOG_QT                         " & vbNewLine _
        & "           , @UNSO_ONDO_KB                       " & vbNewLine _
        & "           , @IRIME                              " & vbNewLine _
        & "           , @IRIME_UT                           " & vbNewLine _
        & "           , @OUTKA_M_PKG_NB                     " & vbNewLine _
        & "           , @REMARK                             " & vbNewLine _
        & "           , @SIZE_KB                            " & vbNewLine _
        & "           , @ZAIKO_KB                           " & vbNewLine _
        & "           , @SOURCE_CD                          " & vbNewLine _
        & "           , @YELLOW_CARD                        " & vbNewLine _
        & "           , @GOODS_CD_NRS_FROM                  " & vbNewLine _
        & "           , @PRINT_SORT                         " & vbNewLine _
        & "           , @SYS_ENT_DATE                       " & vbNewLine _
        & "           , @SYS_ENT_TIME                       " & vbNewLine _
        & "           , @SYS_ENT_PGID                       " & vbNewLine _
        & "           , @SYS_ENT_USER                       " & vbNewLine _
        & "           , @SYS_UPD_DATE                       " & vbNewLine _
        & "           , @SYS_UPD_TIME                       " & vbNewLine _
        & "           , @SYS_UPD_PGID                       " & vbNewLine _
        & "           , @SYS_UPD_USER                       " & vbNewLine _
        & "           , @SYS_DEL_FLG)                       " & vbNewLine

#End Region

#Region "C_OUTKA_S"

    Private Const SQL_INSERT_OUTKA_S As String _
        = " INSERT INTO $LM_TRN$..C_OUTKA_S             " & vbNewLine _
        & "           ( NRS_BR_CD                       " & vbNewLine _
        & "           , OUTKA_NO_L                      " & vbNewLine _
        & "           , OUTKA_NO_M                      " & vbNewLine _
        & "           , OUTKA_NO_S                      " & vbNewLine _
        & "           , TOU_NO                          " & vbNewLine _
        & "           , SITU_NO                         " & vbNewLine _
        & "           , ZONE_CD                         " & vbNewLine _
        & "           , LOCA                            " & vbNewLine _
        & "           , LOT_NO                          " & vbNewLine _
        & "           , SERIAL_NO                       " & vbNewLine _
        & "           , OUTKA_TTL_NB                    " & vbNewLine _
        & "           , OUTKA_TTL_QT                    " & vbNewLine _
        & "           , ZAI_REC_NO                      " & vbNewLine _
        & "           , INKA_NO_L                       " & vbNewLine _
        & "           , INKA_NO_M                       " & vbNewLine _
        & "           , INKA_NO_S                       " & vbNewLine _
        & "           , ZAI_UPD_FLAG                    " & vbNewLine _
        & "           , ALCTD_CAN_NB                    " & vbNewLine _
        & "           , ALCTD_NB                        " & vbNewLine _
        & "           , ALCTD_CAN_QT                    " & vbNewLine _
        & "           , ALCTD_QT                        " & vbNewLine _
        & "           , IRIME                           " & vbNewLine _
        & "           , BETU_WT                         " & vbNewLine _
        & "           , COA_FLAG                        " & vbNewLine _
        & "           , REMARK                          " & vbNewLine _
        & "           , SMPL_FLAG                       " & vbNewLine _
        & "           , REC_NO                          " & vbNewLine _
        & "           , SYS_ENT_DATE                    " & vbNewLine _
        & "           , SYS_ENT_TIME                    " & vbNewLine _
        & "           , SYS_ENT_PGID                    " & vbNewLine _
        & "           , SYS_ENT_USER                    " & vbNewLine _
        & "           , SYS_UPD_DATE                    " & vbNewLine _
        & "           , SYS_UPD_TIME                    " & vbNewLine _
        & "           , SYS_UPD_PGID                    " & vbNewLine _
        & "           , SYS_UPD_USER                    " & vbNewLine _
        & "           , SYS_DEL_FLG)                    " & vbNewLine _
        & "      VALUES                                 " & vbNewLine _
        & "           ( @NRS_BR_CD                      " & vbNewLine _
        & "           , @OUTKA_NO_L                     " & vbNewLine _
        & "           , @OUTKA_NO_M                     " & vbNewLine _
        & "           , @OUTKA_NO_S                     " & vbNewLine _
        & "           , @TOU_NO                         " & vbNewLine _
        & "           , @SITU_NO                        " & vbNewLine _
        & "           , @ZONE_CD                        " & vbNewLine _
        & "           , @LOCA                           " & vbNewLine _
        & "           , @LOT_NO                         " & vbNewLine _
        & "           , @SERIAL_NO                      " & vbNewLine _
        & "           , @OUTKA_TTL_NB                   " & vbNewLine _
        & "           , @OUTKA_TTL_QT                   " & vbNewLine _
        & "           , @ZAI_REC_NO                     " & vbNewLine _
        & "           , @INKA_NO_L                      " & vbNewLine _
        & "           , @INKA_NO_M                      " & vbNewLine _
        & "           , @INKA_NO_S                      " & vbNewLine _
        & "           , @ZAI_UPD_FLAG                   " & vbNewLine _
        & "           , @ALCTD_CAN_NB                   " & vbNewLine _
        & "           , @ALCTD_NB                       " & vbNewLine _
        & "           , @ALCTD_CAN_QT                   " & vbNewLine _
        & "           , @ALCTD_QT                       " & vbNewLine _
        & "           , @IRIME                          " & vbNewLine _
        & "           , @BETU_WT                        " & vbNewLine _
        & "           , @COA_FLAG                       " & vbNewLine _
        & "           , @REMARK                         " & vbNewLine _
        & "           , @SMPL_FLAG                      " & vbNewLine _
        & "           , @REC_NO                         " & vbNewLine _
        & "           , @SYS_ENT_DATE                   " & vbNewLine _
        & "           , @SYS_ENT_TIME                   " & vbNewLine _
        & "           , @SYS_ENT_PGID                   " & vbNewLine _
        & "           , @SYS_ENT_USER                   " & vbNewLine _
        & "           , @SYS_UPD_DATE                   " & vbNewLine _
        & "           , @SYS_UPD_TIME                   " & vbNewLine _
        & "           , @SYS_UPD_PGID                   " & vbNewLine _
        & "           , @SYS_UPD_USER                   " & vbNewLine _
        & "           , @SYS_DEL_FLG)                   " & vbNewLine

#End Region

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

#Region "セミEDI処理"

#Region "H_INKAEDI_DTL_NCGO_NEW"

    ''' <summary>
    ''' INSERT(H_INKAEDI_DTL_NCGO_NEW)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKAEDI_DTL As String _
                                        = "INSERT INTO                       " & vbNewLine _
                                        & " $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW " & vbNewLine _
                                        & "(                                 " & vbNewLine _
                                        & "  DEL_KB                          " & vbNewLine _
                                        & " ,CRT_DATE                        " & vbNewLine _
                                        & " ,FILE_NAME                       " & vbNewLine _
                                        & " ,REC_NO                          " & vbNewLine _
                                        & " ,GYO                             " & vbNewLine _
                                        & " ,NRS_BR_CD                       " & vbNewLine _
                                        & " ,EDI_CTL_NO                      " & vbNewLine _
                                        & " ,EDI_CTL_NO_CHU                  " & vbNewLine _
                                        & " ,INKA_CTL_NO_L                   " & vbNewLine _
                                        & " ,INKA_CTL_NO_M                   " & vbNewLine _
                                        & " ,CUST_CD_L                       " & vbNewLine _
                                        & " ,CUST_CD_M                       " & vbNewLine _
                                        & " ,DATA_ID_AREA                    " & vbNewLine _
                                        & " ,DATA_ID_DETAIL                  " & vbNewLine _
                                        & " ,DENSOUSAKI                      " & vbNewLine _
                                        & " ,INKA_H_Y_KBN                    " & vbNewLine _
                                        & " ,KANNOU_KBN                      " & vbNewLine _
                                        & " ,INPUT_KBN                       " & vbNewLine _
                                        & " ,AKADEN_KBN                      " & vbNewLine _
                                        & " ,DATA_CRE_DATE                   " & vbNewLine _
                                        & " ,DATA_CRE_TIME                   " & vbNewLine _
                                        & " ,COMP_CD                         " & vbNewLine _
                                        & " ,IRAISYA_NM                      " & vbNewLine _
                                        & " ,DENPYO_TYPE                     " & vbNewLine _
                                        & " ,HACCHU_DENP_NO                  " & vbNewLine _
                                        & " ,HACCHU_DENP_DTL_NO              " & vbNewLine _
                                        & " ,OUTKA_DENP_NO                   " & vbNewLine _
                                        & " ,OUTKA_DENP_DTL_NO               " & vbNewLine _
                                        & " ,IO_DENP_NO                      " & vbNewLine _
                                        & " ,IO_DENP_DTL_NO                  " & vbNewLine _
                                        & " ,KISYA_HOUKOKU_KNARI_NO          " & vbNewLine _
                                        & " ,INPUT_DATE                      " & vbNewLine _
                                        & " ,SEIRI_DATE                      " & vbNewLine _
                                        & " ,SYUKKA_DATE                     " & vbNewLine _
                                        & " ,NOUKI_DATE                      " & vbNewLine _
                                        & " ,ITEM_CD                         " & vbNewLine _
                                        & " ,ITEM_RYAKUGO                    " & vbNewLine _
                                        & " ,ITEM_AISYO                      " & vbNewLine _
                                        & " ,ITEM_GROUP                      " & vbNewLine _
                                        & " ,SEIZO_KBN                       " & vbNewLine _
                                        & " ,GRADE1                          " & vbNewLine _
                                        & " ,GRADE2                          " & vbNewLine _
                                        & " ,DAIHYO_NISUGATA_CD              " & vbNewLine _
                                        & " ,NISUGATA_CD                     " & vbNewLine _
                                        & " ,DAIHYO_NISUGATA_NM              " & vbNewLine _
                                        & " ,YOURYOU                         " & vbNewLine _
                                        & " ,SEIZO_LOT                       " & vbNewLine _
                                        & " ,ZAIKO_KANRI_LOT_NO              " & vbNewLine _
                                        & " ,KOSU_FUGO                       " & vbNewLine _
                                        & " ,KOSU                            " & vbNewLine _
                                        & " ,SUURYO_FUGO                     " & vbNewLine _
                                        & " ,SUURYO                          " & vbNewLine _
                                        & " ,INKA_PLANT                      " & vbNewLine _
                                        & " ,INKA_HOKAN_BASYO                " & vbNewLine _
                                        & " ,NOUNYUSAKI_BASYO_CD             " & vbNewLine _
                                        & " ,OUTKA_PLANT                     " & vbNewLine _
                                        & " ,OUT_IN_POINT                    " & vbNewLine _
                                        & " ,OUTKA_HOKAN_BASYO               " & vbNewLine _
                                        & " ,SYUKKAMOTO_TOKUISAKI_CD         " & vbNewLine _
                                        & " ,SYUKKAMOTO_NM1                  " & vbNewLine _
                                        & " ,SYUKKAMOTO_NM2                  " & vbNewLine _
                                        & " ,SYUKKAMOTO_NM4                  " & vbNewLine _
                                        & " ,SYUKKAMOTO_NM5                  " & vbNewLine _
                                        & " ,SYUKKAMOTO_SYOZAICHI            " & vbNewLine _
                                        & " ,SYUKKAMOTO_BASYO_CD             " & vbNewLine _
                                        & " ,SYUKKAMOTO_TEL                  " & vbNewLine _
                                        & " ,SYUKKAMOTO_ZIP                  " & vbNewLine _
                                        & " ,SYUKKA_TYPE                     " & vbNewLine _
                                        & " ,SYUKKA_TYPE_TXT                 " & vbNewLine _
                                        & " ,YUSO_SYUDAN                     " & vbNewLine _
                                        & " ,YUSO_SYUDAN_NM                  " & vbNewLine _
                                        & " ,YUSYUTSU_KBN                    " & vbNewLine _
                                        & " ,TOUSHI_NO                       " & vbNewLine _
                                        & " ,CONT_NO                         " & vbNewLine _
                                        & " ,HAISEN_PLAN_NO                  " & vbNewLine _
                                        & " ,RIEKI_SENTA                     " & vbNewLine _
                                        & " ,GENKA_SENTA                     " & vbNewLine _
                                        & " ,HENPIN_RIYU                     " & vbNewLine _
                                        & " ,NOUNYU_JYOUKEN_BIKOU            " & vbNewLine _
                                        & " ,UCHI_BIKOU                      " & vbNewLine _
                                        & " ,SOTO_BIKOU                      " & vbNewLine _
                                        & " ,RENBAN                          " & vbNewLine _
                                        & " ,ITEM_TXT                        " & vbNewLine _
                                        & " ,JIGYOUBU                        " & vbNewLine _
                                        & " ,SANSYO_CD                       " & vbNewLine _
                                        & " ,NOUNYU_JIKOKU_NM                " & vbNewLine _
                                        & " ,TANK_NO                         " & vbNewLine _
                                        & " ,UG                              " & vbNewLine _
                                        & " ,KIHON_SURYO_TANI                " & vbNewLine _
                                        & " ,JURYO_KANSAN_KEISU              " & vbNewLine _
                                        & " ,NOUNYU_JIKOKU_CD                " & vbNewLine _
                                        & " ,YOBI                            " & vbNewLine _
                                        & " ,ERR_CATEGORY1                   " & vbNewLine _
                                        & " ,ERR_CATEGORY1_NM                " & vbNewLine _
                                        & " ,ERR_KOUBAN1_1                   " & vbNewLine _
                                        & " ,ERR_KOUBAN1_2                   " & vbNewLine _
                                        & " ,ERR_KOUBAN1_3                   " & vbNewLine _
                                        & " ,ERR_KOUBAN1_4                   " & vbNewLine _
                                        & " ,ERR_KOUBAN1_5                   " & vbNewLine _
                                        & " ,ERR_CATEGORY2                   " & vbNewLine _
                                        & " ,ERR_CATEGORY2_NM                " & vbNewLine _
                                        & " ,ERR_KOUBAN2_1                   " & vbNewLine _
                                        & " ,ERR_KOUBAN2_2                   " & vbNewLine _
                                        & " ,ERR_KOUBAN2_3                   " & vbNewLine _
                                        & " ,ERR_KOUBAN2_4                   " & vbNewLine _
                                        & " ,ERR_KOUBAN2_5                   " & vbNewLine _
                                        & " ,ERR_CATEGORY3                   " & vbNewLine _
                                        & " ,ERR_CATEGORY3_NM                " & vbNewLine _
                                        & " ,ERR_KOUBAN3_1                   " & vbNewLine _
                                        & " ,ERR_KOUBAN3_2                   " & vbNewLine _
                                        & " ,ERR_KOUBAN3_3                   " & vbNewLine _
                                        & " ,ERR_KOUBAN3_4                   " & vbNewLine _
                                        & " ,ERR_KOUBAN3_5                   " & vbNewLine _
                                        & " ,ERR_YOBI                        " & vbNewLine _
                                        & " ,RECORD_STATUS                   " & vbNewLine _
                                        & " ,JISSEKI_SHORI_FLG               " & vbNewLine _
                                        & " ,JISSEKI_USER                    " & vbNewLine _
                                        & " ,JISSEKI_DATE                    " & vbNewLine _
                                        & " ,JISSEKI_TIME                    " & vbNewLine _
                                        & " ,SEND_USER                       " & vbNewLine _
                                        & " ,SEND_DATE                       " & vbNewLine _
                                        & " ,SEND_TIME                       " & vbNewLine _
                                        & " ,DELETE_USER                     " & vbNewLine _
                                        & " ,DELETE_DATE                     " & vbNewLine _
                                        & " ,DELETE_TIME                     " & vbNewLine _
                                        & " ,DELETE_EDI_NO                   " & vbNewLine _
                                        & " ,DELETE_EDI_NO_CHU               " & vbNewLine _
                                        & " ,UPD_USER                        " & vbNewLine _
                                        & " ,UPD_DATE                        " & vbNewLine _
                                        & " ,UPD_TIME                        " & vbNewLine _
                                        & " ,SYS_ENT_DATE                    " & vbNewLine _
                                        & " ,SYS_ENT_TIME                    " & vbNewLine _
                                        & " ,SYS_ENT_PGID                    " & vbNewLine _
                                        & " ,SYS_ENT_USER                    " & vbNewLine _
                                        & " ,SYS_UPD_DATE                    " & vbNewLine _
                                        & " ,SYS_UPD_TIME                    " & vbNewLine _
                                        & " ,SYS_UPD_PGID                    " & vbNewLine _
                                        & " ,SYS_UPD_USER                    " & vbNewLine _
                                        & " ,SYS_DEL_FLG                     " & vbNewLine _
                                        & ")VALUES(                          " & vbNewLine _
                                        & "  @DEL_KB                         " & vbNewLine _
                                        & " ,@CRT_DATE                       " & vbNewLine _
                                        & " ,@FILE_NAME                      " & vbNewLine _
                                        & " ,@REC_NO                         " & vbNewLine _
                                        & " ,@GYO                            " & vbNewLine _
                                        & " ,@NRS_BR_CD                      " & vbNewLine _
                                        & " ,@EDI_CTL_NO                     " & vbNewLine _
                                        & " ,@EDI_CTL_NO_CHU                 " & vbNewLine _
                                        & " ,@INKA_CTL_NO_L                  " & vbNewLine _
                                        & " ,@INKA_CTL_NO_M                  " & vbNewLine _
                                        & " ,@CUST_CD_L                      " & vbNewLine _
                                        & " ,@CUST_CD_M                      " & vbNewLine _
                                        & " ,@DATA_ID_AREA                   " & vbNewLine _
                                        & " ,@DATA_ID_DETAIL                 " & vbNewLine _
                                        & " ,@DENSOUSAKI                     " & vbNewLine _
                                        & " ,@INKA_H_Y_KBN                   " & vbNewLine _
                                        & " ,@KANNOU_KBN                     " & vbNewLine _
                                        & " ,@INPUT_KBN                      " & vbNewLine _
                                        & " ,@AKADEN_KBN                     " & vbNewLine _
                                        & " ,@DATA_CRE_DATE                  " & vbNewLine _
                                        & " ,@DATA_CRE_TIME                  " & vbNewLine _
                                        & " ,@COMP_CD                        " & vbNewLine _
                                        & " ,@IRAISYA_NM                     " & vbNewLine _
                                        & " ,@DENPYO_TYPE                    " & vbNewLine _
                                        & " ,@HACCHU_DENP_NO                 " & vbNewLine _
                                        & " ,@HACCHU_DENP_DTL_NO             " & vbNewLine _
                                        & " ,@OUTKA_DENP_NO                  " & vbNewLine _
                                        & " ,@OUTKA_DENP_DTL_NO              " & vbNewLine _
                                        & " ,@IO_DENP_NO                     " & vbNewLine _
                                        & " ,@IO_DENP_DTL_NO                 " & vbNewLine _
                                        & " ,@KISYA_HOUKOKU_KNARI_NO         " & vbNewLine _
                                        & " ,@INPUT_DATE                     " & vbNewLine _
                                        & " ,@SEIRI_DATE                     " & vbNewLine _
                                        & " ,@SYUKKA_DATE                    " & vbNewLine _
                                        & " ,@NOUKI_DATE                     " & vbNewLine _
                                        & " ,@ITEM_CD                        " & vbNewLine _
                                        & " ,@ITEM_RYAKUGO                   " & vbNewLine _
                                        & " ,@ITEM_AISYO                     " & vbNewLine _
                                        & " ,@ITEM_GROUP                     " & vbNewLine _
                                        & " ,@SEIZO_KBN                      " & vbNewLine _
                                        & " ,@GRADE1                         " & vbNewLine _
                                        & " ,@GRADE2                         " & vbNewLine _
                                        & " ,@DAIHYO_NISUGATA_CD             " & vbNewLine _
                                        & " ,@NISUGATA_CD                    " & vbNewLine _
                                        & " ,@DAIHYO_NISUGATA_NM             " & vbNewLine _
                                        & " ,@YOURYOU                        " & vbNewLine _
                                        & " ,@SEIZO_LOT                      " & vbNewLine _
                                        & " ,@ZAIKO_KANRI_LOT_NO             " & vbNewLine _
                                        & " ,@KOSU_FUGO                      " & vbNewLine _
                                        & " ,@KOSU                           " & vbNewLine _
                                        & " ,@SUURYO_FUGO                    " & vbNewLine _
                                        & " ,@SUURYO                         " & vbNewLine _
                                        & " ,@INKA_PLANT                     " & vbNewLine _
                                        & " ,@INKA_HOKAN_BASYO               " & vbNewLine _
                                        & " ,@NOUNYUSAKI_BASYO_CD            " & vbNewLine _
                                        & " ,@OUTKA_PLANT                    " & vbNewLine _
                                        & " ,@OUT_IN_POINT                   " & vbNewLine _
                                        & " ,@OUTKA_HOKAN_BASYO              " & vbNewLine _
                                        & " ,@SYUKKAMOTO_TOKUISAKI_CD        " & vbNewLine _
                                        & " ,@SYUKKAMOTO_NM1                 " & vbNewLine _
                                        & " ,@SYUKKAMOTO_NM2                 " & vbNewLine _
                                        & " ,@SYUKKAMOTO_NM4                 " & vbNewLine _
                                        & " ,@SYUKKAMOTO_NM5                 " & vbNewLine _
                                        & " ,@SYUKKAMOTO_SYOZAICHI           " & vbNewLine _
                                        & " ,@SYUKKAMOTO_BASYO_CD            " & vbNewLine _
                                        & " ,@SYUKKAMOTO_TEL                 " & vbNewLine _
                                        & " ,@SYUKKAMOTO_ZIP                 " & vbNewLine _
                                        & " ,@SYUKKA_TYPE                    " & vbNewLine _
                                        & " ,@SYUKKA_TYPE_TXT                " & vbNewLine _
                                        & " ,@YUSO_SYUDAN                    " & vbNewLine _
                                        & " ,@YUSO_SYUDAN_NM                 " & vbNewLine _
                                        & " ,@YUSYUTSU_KBN                   " & vbNewLine _
                                        & " ,@TOUSHI_NO                      " & vbNewLine _
                                        & " ,@CONT_NO                        " & vbNewLine _
                                        & " ,@HAISEN_PLAN_NO                 " & vbNewLine _
                                        & " ,@RIEKI_SENTA                    " & vbNewLine _
                                        & " ,@GENKA_SENTA                    " & vbNewLine _
                                        & " ,@HENPIN_RIYU                    " & vbNewLine _
                                        & " ,@NOUNYU_JYOUKEN_BIKOU           " & vbNewLine _
                                        & " ,@UCHI_BIKOU                     " & vbNewLine _
                                        & " ,@SOTO_BIKOU                     " & vbNewLine _
                                        & " ,@RENBAN                         " & vbNewLine _
                                        & " ,@ITEM_TXT                       " & vbNewLine _
                                        & " ,@JIGYOUBU                       " & vbNewLine _
                                        & " ,@SANSYO_CD                      " & vbNewLine _
                                        & " ,@NOUNYU_JIKOKU_NM               " & vbNewLine _
                                        & " ,@TANK_NO                        " & vbNewLine _
                                        & " ,@UG                             " & vbNewLine _
                                        & " ,@KIHON_SURYO_TANI               " & vbNewLine _
                                        & " ,@JURYO_KANSAN_KEISU             " & vbNewLine _
                                        & " ,@NOUNYU_JIKOKU_CD               " & vbNewLine _
                                        & " ,@YOBI                           " & vbNewLine _
                                        & " ,@ERR_CATEGORY1                  " & vbNewLine _
                                        & " ,@ERR_CATEGORY1_NM               " & vbNewLine _
                                        & " ,@ERR_KOUBAN1_1                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN1_2                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN1_3                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN1_4                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN1_5                  " & vbNewLine _
                                        & " ,@ERR_CATEGORY2                  " & vbNewLine _
                                        & " ,@ERR_CATEGORY2_NM               " & vbNewLine _
                                        & " ,@ERR_KOUBAN2_1                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN2_2                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN2_3                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN2_4                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN2_5                  " & vbNewLine _
                                        & " ,@ERR_CATEGORY3                  " & vbNewLine _
                                        & " ,@ERR_CATEGORY3_NM               " & vbNewLine _
                                        & " ,@ERR_KOUBAN3_1                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN3_2                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN3_3                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN3_4                  " & vbNewLine _
                                        & " ,@ERR_KOUBAN3_5                  " & vbNewLine _
                                        & " ,@ERR_YOBI                       " & vbNewLine _
                                        & " ,@RECORD_STATUS                  " & vbNewLine _
                                        & " ,@JISSEKI_SHORI_FLG              " & vbNewLine _
                                        & " ,@JISSEKI_USER                   " & vbNewLine _
                                        & " ,@JISSEKI_DATE                   " & vbNewLine _
                                        & " ,@JISSEKI_TIME                   " & vbNewLine _
                                        & " ,@SEND_USER                      " & vbNewLine _
                                        & " ,@SEND_DATE                      " & vbNewLine _
                                        & " ,@SEND_TIME                      " & vbNewLine _
                                        & " ,@DELETE_USER                    " & vbNewLine _
                                        & " ,@DELETE_DATE                    " & vbNewLine _
                                        & " ,@DELETE_TIME                    " & vbNewLine _
                                        & " ,@DELETE_EDI_NO                  " & vbNewLine _
                                        & " ,@DELETE_EDI_NO_CHU              " & vbNewLine _
                                        & " ,@UPD_USER                       " & vbNewLine _
                                        & " ,@UPD_DATE                       " & vbNewLine _
                                        & " ,@UPD_TIME                       " & vbNewLine _
                                        & " ,@SYS_ENT_DATE                   " & vbNewLine _
                                        & " ,@SYS_ENT_TIME                   " & vbNewLine _
                                        & " ,@SYS_ENT_PGID                   " & vbNewLine _
                                        & " ,@SYS_ENT_USER                   " & vbNewLine _
                                        & " ,@SYS_UPD_DATE                   " & vbNewLine _
                                        & " ,@SYS_UPD_TIME                   " & vbNewLine _
                                        & " ,@SYS_UPD_PGID                   " & vbNewLine _
                                        & " ,@SYS_UPD_USER                   " & vbNewLine _
                                        & " ,@SYS_DEL_FLG                    " & vbNewLine _
                                        & ")                                 " & vbNewLine

#End Region ' "H_INKAEDI_DTL_NCGO_NEW"

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' INSERT(H_INKAEDI_L)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKAEDI_L As String = "" _
        & "INSERT INTO $LM_TRN$..H_INKAEDI_L ( " & vbNewLine _
        & "      DEL_KB                        " & vbNewLine _
        & "    , NRS_BR_CD                     " & vbNewLine _
        & "    , EDI_CTL_NO                    " & vbNewLine _
        & "    , INKA_CTL_NO_L                 " & vbNewLine _
        & "    , INKA_TP                       " & vbNewLine _
        & "    , INKA_KB                       " & vbNewLine _
        & "    , INKA_STATE_KB                 " & vbNewLine _
        & "    , INKA_DATE                     " & vbNewLine _
        & "    , INKA_TIME                     " & vbNewLine _
        & "    , NRS_WH_CD                     " & vbNewLine _
        & "    , CUST_CD_L                     " & vbNewLine _
        & "    , CUST_CD_M                     " & vbNewLine _
        & "    , CUST_NM_L                     " & vbNewLine _
        & "    , CUST_NM_M                     " & vbNewLine _
        & "    , INKA_PLAN_QT                  " & vbNewLine _
        & "    , INKA_PLAN_QT_UT               " & vbNewLine _
        & "    , INKA_TTL_NB                   " & vbNewLine _
        & "    , NAIGAI_KB                     " & vbNewLine _
        & "    , BUYER_ORD_NO                  " & vbNewLine _
        & "    , OUTKA_FROM_ORD_NO             " & vbNewLine _
        & "    , SEIQTO_CD                     " & vbNewLine _
        & "    , TOUKI_HOKAN_YN                " & vbNewLine _
        & "    , HOKAN_YN                      " & vbNewLine _
        & "    , HOKAN_FREE_KIKAN              " & vbNewLine _
        & "    , HOKAN_STR_DATE                " & vbNewLine _
        & "    , NIYAKU_YN                     " & vbNewLine _
        & "    , TAX_KB                        " & vbNewLine _
        & "    , REMARK                        " & vbNewLine _
        & "    , NYUBAN_L                      " & vbNewLine _
        & "    , UNCHIN_TP                     " & vbNewLine _
        & "    , UNCHIN_KB                     " & vbNewLine _
        & "    , OUTKA_MOTO                    " & vbNewLine _
        & "    , SYARYO_KB                     " & vbNewLine _
        & "    , UNSO_ONDO_KB                  " & vbNewLine _
        & "    , UNSO_CD                       " & vbNewLine _
        & "    , UNSO_BR_CD                    " & vbNewLine _
        & "    , UNCHIN                        " & vbNewLine _
        & "    , YOKO_TARIFF_CD                " & vbNewLine _
        & "    , OUT_FLAG                      " & vbNewLine _
        & "    , AKAKURO_KB                    " & vbNewLine _
        & "    , JISSEKI_FLAG                  " & vbNewLine _
        & "    , JISSEKI_USER                  " & vbNewLine _
        & "    , JISSEKI_DATE                  " & vbNewLine _
        & "    , JISSEKI_TIME                  " & vbNewLine _
        & "    , FREE_N01                      " & vbNewLine _
        & "    , FREE_N02                      " & vbNewLine _
        & "    , FREE_N03                      " & vbNewLine _
        & "    , FREE_N04                      " & vbNewLine _
        & "    , FREE_N05                      " & vbNewLine _
        & "    , FREE_N06                      " & vbNewLine _
        & "    , FREE_N07                      " & vbNewLine _
        & "    , FREE_N08                      " & vbNewLine _
        & "    , FREE_N09                      " & vbNewLine _
        & "    , FREE_N10                      " & vbNewLine _
        & "    , FREE_C01                      " & vbNewLine _
        & "    , FREE_C02                      " & vbNewLine _
        & "    , FREE_C03                      " & vbNewLine _
        & "    , FREE_C04                      " & vbNewLine _
        & "    , FREE_C05                      " & vbNewLine _
        & "    , FREE_C06                      " & vbNewLine _
        & "    , FREE_C07                      " & vbNewLine _
        & "    , FREE_C08                      " & vbNewLine _
        & "    , FREE_C09                      " & vbNewLine _
        & "    , FREE_C10                      " & vbNewLine _
        & "    , FREE_C11                      " & vbNewLine _
        & "    , FREE_C12                      " & vbNewLine _
        & "    , FREE_C13                      " & vbNewLine _
        & "    , FREE_C14                      " & vbNewLine _
        & "    , FREE_C15                      " & vbNewLine _
        & "    , FREE_C16                      " & vbNewLine _
        & "    , FREE_C17                      " & vbNewLine _
        & "    , FREE_C18                      " & vbNewLine _
        & "    , FREE_C19                      " & vbNewLine _
        & "    , FREE_C20                      " & vbNewLine _
        & "    , FREE_C21                      " & vbNewLine _
        & "    , FREE_C22                      " & vbNewLine _
        & "    , FREE_C23                      " & vbNewLine _
        & "    , FREE_C24                      " & vbNewLine _
        & "    , FREE_C25                      " & vbNewLine _
        & "    , FREE_C26                      " & vbNewLine _
        & "    , FREE_C27                      " & vbNewLine _
        & "    , FREE_C28                      " & vbNewLine _
        & "    , FREE_C29                      " & vbNewLine _
        & "    , FREE_C30                      " & vbNewLine _
        & "    , CRT_USER                      " & vbNewLine _
        & "    , CRT_DATE                      " & vbNewLine _
        & "    , CRT_TIME                      " & vbNewLine _
        & "    , UPD_USER                      " & vbNewLine _
        & "    , UPD_DATE                      " & vbNewLine _
        & "    , UPD_TIME                      " & vbNewLine _
        & "    , EDIT_FLAG                     " & vbNewLine _
        & "    , MATCHING_FLAG                 " & vbNewLine _
        & "    , SYS_ENT_DATE                  " & vbNewLine _
        & "    , SYS_ENT_TIME                  " & vbNewLine _
        & "    , SYS_ENT_PGID                  " & vbNewLine _
        & "    , SYS_ENT_USER                  " & vbNewLine _
        & "    , SYS_UPD_DATE                  " & vbNewLine _
        & "    , SYS_UPD_TIME                  " & vbNewLine _
        & "    , SYS_UPD_PGID                  " & vbNewLine _
        & "    , SYS_UPD_USER                  " & vbNewLine _
        & "    , SYS_DEL_FLG                   " & vbNewLine _
        & ")VALUES(                            " & vbNewLine _
        & "      @DEL_KB                       " & vbNewLine _
        & "    , @NRS_BR_CD                    " & vbNewLine _
        & "    , @EDI_CTL_NO                   " & vbNewLine _
        & "    , @INKA_CTL_NO_L                " & vbNewLine _
        & "    , @INKA_TP                      " & vbNewLine _
        & "    , @INKA_KB                      " & vbNewLine _
        & "    , @INKA_STATE_KB                " & vbNewLine _
        & "    , @INKA_DATE                    " & vbNewLine _
        & "    , @INKA_TIME                    " & vbNewLine _
        & "    , @NRS_WH_CD                    " & vbNewLine _
        & "    , @CUST_CD_L                    " & vbNewLine _
        & "    , @CUST_CD_M                    " & vbNewLine _
        & "    , @CUST_NM_L                    " & vbNewLine _
        & "    , @CUST_NM_M                    " & vbNewLine _
        & "    , @INKA_PLAN_QT                 " & vbNewLine _
        & "    , @INKA_PLAN_QT_UT              " & vbNewLine _
        & "    , @INKA_TTL_NB                  " & vbNewLine _
        & "    , @NAIGAI_KB                    " & vbNewLine _
        & "    , @BUYER_ORD_NO                 " & vbNewLine _
        & "    , @OUTKA_FROM_ORD_NO            " & vbNewLine _
        & "    , @SEIQTO_CD                    " & vbNewLine _
        & "    , @TOUKI_HOKAN_YN               " & vbNewLine _
        & "    , @HOKAN_YN                     " & vbNewLine _
        & "    , @HOKAN_FREE_KIKAN             " & vbNewLine _
        & "    , @HOKAN_STR_DATE               " & vbNewLine _
        & "    , @NIYAKU_YN                    " & vbNewLine _
        & "    , @TAX_KB                       " & vbNewLine _
        & "    , @REMARK                       " & vbNewLine _
        & "    , @NYUBAN_L                     " & vbNewLine _
        & "    , @UNCHIN_TP                    " & vbNewLine _
        & "    , @UNCHIN_KB                    " & vbNewLine _
        & "    , @OUTKA_MOTO                   " & vbNewLine _
        & "    , @SYARYO_KB                    " & vbNewLine _
        & "    , @UNSO_ONDO_KB                 " & vbNewLine _
        & "    , @UNSO_CD                      " & vbNewLine _
        & "    , @UNSO_BR_CD                   " & vbNewLine _
        & "    , @UNCHIN                       " & vbNewLine _
        & "    , @YOKO_TARIFF_CD               " & vbNewLine _
        & "    , @OUT_FLAG                     " & vbNewLine _
        & "    , @AKAKURO_KB                   " & vbNewLine _
        & "    , @JISSEKI_FLAG                 " & vbNewLine _
        & "    , @JISSEKI_USER                 " & vbNewLine _
        & "    , @JISSEKI_DATE                 " & vbNewLine _
        & "    , @JISSEKI_TIME                 " & vbNewLine _
        & "    , @FREE_N01                     " & vbNewLine _
        & "    , @FREE_N02                     " & vbNewLine _
        & "    , @FREE_N03                     " & vbNewLine _
        & "    , @FREE_N04                     " & vbNewLine _
        & "    , @FREE_N05                     " & vbNewLine _
        & "    , @FREE_N06                     " & vbNewLine _
        & "    , @FREE_N07                     " & vbNewLine _
        & "    , @FREE_N08                     " & vbNewLine _
        & "    , @FREE_N09                     " & vbNewLine _
        & "    , @FREE_N10                     " & vbNewLine _
        & "    , @FREE_C01                     " & vbNewLine _
        & "    , @FREE_C02                     " & vbNewLine _
        & "    , @FREE_C03                     " & vbNewLine _
        & "    , @FREE_C04                     " & vbNewLine _
        & "    , @FREE_C05                     " & vbNewLine _
        & "    , @FREE_C06                     " & vbNewLine _
        & "    , @FREE_C07                     " & vbNewLine _
        & "    , @FREE_C08                     " & vbNewLine _
        & "    , @FREE_C09                     " & vbNewLine _
        & "    , @FREE_C10                     " & vbNewLine _
        & "    , @FREE_C11                     " & vbNewLine _
        & "    , @FREE_C12                     " & vbNewLine _
        & "    , @FREE_C13                     " & vbNewLine _
        & "    , @FREE_C14                     " & vbNewLine _
        & "    , @FREE_C15                     " & vbNewLine _
        & "    , @FREE_C16                     " & vbNewLine _
        & "    , @FREE_C17                     " & vbNewLine _
        & "    , @FREE_C18                     " & vbNewLine _
        & "    , @FREE_C19                     " & vbNewLine _
        & "    , @FREE_C20                     " & vbNewLine _
        & "    , @FREE_C21                     " & vbNewLine _
        & "    , @FREE_C22                     " & vbNewLine _
        & "    , @FREE_C23                     " & vbNewLine _
        & "    , @FREE_C24                     " & vbNewLine _
        & "    , @FREE_C25                     " & vbNewLine _
        & "    , @FREE_C26                     " & vbNewLine _
        & "    , @FREE_C27                     " & vbNewLine _
        & "    , @FREE_C28                     " & vbNewLine _
        & "    , @FREE_C29                     " & vbNewLine _
        & "    , @FREE_C30                     " & vbNewLine _
        & "    , @CRT_USER                     " & vbNewLine _
        & "    , @CRT_DATE                     " & vbNewLine _
        & "    , @CRT_TIME                     " & vbNewLine _
        & "    , @UPD_USER                     " & vbNewLine _
        & "    , @UPD_DATE                     " & vbNewLine _
        & "    , @UPD_TIME                     " & vbNewLine _
        & "    , @EDIT_FLAG                    " & vbNewLine _
        & "    , @MATCHING_FLAG                " & vbNewLine _
        & "    , @SYS_ENT_DATE                 " & vbNewLine _
        & "    , @SYS_ENT_TIME                 " & vbNewLine _
        & "    , @SYS_ENT_PGID                 " & vbNewLine _
        & "    , @SYS_ENT_USER                 " & vbNewLine _
        & "    , @SYS_UPD_DATE                 " & vbNewLine _
        & "    , @SYS_UPD_TIME                 " & vbNewLine _
        & "    , @SYS_UPD_PGID                 " & vbNewLine _
        & "    , @SYS_UPD_USER                 " & vbNewLine _
        & "    , @SYS_DEL_FLG                  " & vbNewLine _
        & ")                                   " & vbNewLine _
        & ""

#End Region ' "H_INKAEDI_L"

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' INSERT(H_INKAEDI_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKAEDI_M As String = "" _
        & "INSERT INTO $LM_TRN$..H_INKAEDI_M ( " & vbNewLine _
        & "      DEL_KB                        " & vbNewLine _
        & "    , NRS_BR_CD                     " & vbNewLine _
        & "    , EDI_CTL_NO                    " & vbNewLine _
        & "    , EDI_CTL_NO_CHU                " & vbNewLine _
        & "    , INKA_CTL_NO_L                 " & vbNewLine _
        & "    , INKA_CTL_NO_M                 " & vbNewLine _
        & "    , NRS_GOODS_CD                  " & vbNewLine _
        & "    , CUST_GOODS_CD                 " & vbNewLine _
        & "    , GOODS_NM                      " & vbNewLine _
        & "    , NB                            " & vbNewLine _
        & "    , NB_UT                         " & vbNewLine _
        & "    , PKG_NB                        " & vbNewLine _
        & "    , PKG_UT                        " & vbNewLine _
        & "    , INKA_PKG_NB                   " & vbNewLine _
        & "    , HASU                          " & vbNewLine _
        & "    , STD_IRIME                     " & vbNewLine _
        & "    , STD_IRIME_UT                  " & vbNewLine _
        & "    , BETU_WT                       " & vbNewLine _
        & "    , CBM                           " & vbNewLine _
        & "    , ONDO_KB                       " & vbNewLine _
        & "    , OUTKA_FROM_ORD_NO             " & vbNewLine _
        & "    , BUYER_ORD_NO                  " & vbNewLine _
        & "    , REMARK                        " & vbNewLine _
        & "    , LOT_NO                        " & vbNewLine _
        & "    , SERIAL_NO                     " & vbNewLine _
        & "    , IRIME                         " & vbNewLine _
        & "    , IRIME_UT                      " & vbNewLine _
        & "    , OUT_KB                        " & vbNewLine _
        & "    , AKAKURO_KB                    " & vbNewLine _
        & "    , JISSEKI_FLAG                  " & vbNewLine _
        & "    , JISSEKI_USER                  " & vbNewLine _
        & "    , JISSEKI_DATE                  " & vbNewLine _
        & "    , JISSEKI_TIME                  " & vbNewLine _
        & "    , FREE_N01                      " & vbNewLine _
        & "    , FREE_N02                      " & vbNewLine _
        & "    , FREE_N03                      " & vbNewLine _
        & "    , FREE_N04                      " & vbNewLine _
        & "    , FREE_N05                      " & vbNewLine _
        & "    , FREE_N06                      " & vbNewLine _
        & "    , FREE_N07                      " & vbNewLine _
        & "    , FREE_N08                      " & vbNewLine _
        & "    , FREE_N09                      " & vbNewLine _
        & "    , FREE_N10                      " & vbNewLine _
        & "    , FREE_C01                      " & vbNewLine _
        & "    , FREE_C02                      " & vbNewLine _
        & "    , FREE_C03                      " & vbNewLine _
        & "    , FREE_C04                      " & vbNewLine _
        & "    , FREE_C05                      " & vbNewLine _
        & "    , FREE_C06                      " & vbNewLine _
        & "    , FREE_C07                      " & vbNewLine _
        & "    , FREE_C08                      " & vbNewLine _
        & "    , FREE_C09                      " & vbNewLine _
        & "    , FREE_C10                      " & vbNewLine _
        & "    , FREE_C11                      " & vbNewLine _
        & "    , FREE_C12                      " & vbNewLine _
        & "    , FREE_C13                      " & vbNewLine _
        & "    , FREE_C14                      " & vbNewLine _
        & "    , FREE_C15                      " & vbNewLine _
        & "    , FREE_C16                      " & vbNewLine _
        & "    , FREE_C17                      " & vbNewLine _
        & "    , FREE_C18                      " & vbNewLine _
        & "    , FREE_C19                      " & vbNewLine _
        & "    , FREE_C20                      " & vbNewLine _
        & "    , FREE_C21                      " & vbNewLine _
        & "    , FREE_C22                      " & vbNewLine _
        & "    , FREE_C23                      " & vbNewLine _
        & "    , FREE_C24                      " & vbNewLine _
        & "    , FREE_C25                      " & vbNewLine _
        & "    , FREE_C26                      " & vbNewLine _
        & "    , FREE_C27                      " & vbNewLine _
        & "    , FREE_C28                      " & vbNewLine _
        & "    , FREE_C29                      " & vbNewLine _
        & "    , FREE_C30                      " & vbNewLine _
        & "    , CRT_USER                      " & vbNewLine _
        & "    , CRT_DATE                      " & vbNewLine _
        & "    , CRT_TIME                      " & vbNewLine _
        & "    , UPD_USER                      " & vbNewLine _
        & "    , UPD_DATE                      " & vbNewLine _
        & "    , UPD_TIME                      " & vbNewLine _
        & "    , SYS_ENT_DATE                  " & vbNewLine _
        & "    , SYS_ENT_TIME                  " & vbNewLine _
        & "    , SYS_ENT_PGID                  " & vbNewLine _
        & "    , SYS_ENT_USER                  " & vbNewLine _
        & "    , SYS_UPD_DATE                  " & vbNewLine _
        & "    , SYS_UPD_TIME                  " & vbNewLine _
        & "    , SYS_UPD_PGID                  " & vbNewLine _
        & "    , SYS_UPD_USER                  " & vbNewLine _
        & "    , SYS_DEL_FLG                   " & vbNewLine _
        & ")VALUES(                            " & vbNewLine _
        & "      @DEL_KB                       " & vbNewLine _
        & "    , @NRS_BR_CD                    " & vbNewLine _
        & "    , @EDI_CTL_NO                   " & vbNewLine _
        & "    , @EDI_CTL_NO_CHU               " & vbNewLine _
        & "    , @INKA_CTL_NO_L                " & vbNewLine _
        & "    , @INKA_CTL_NO_M                " & vbNewLine _
        & "    , @NRS_GOODS_CD                 " & vbNewLine _
        & "    , @CUST_GOODS_CD                " & vbNewLine _
        & "    , @GOODS_NM                     " & vbNewLine _
        & "    , @NB                           " & vbNewLine _
        & "    , @NB_UT                        " & vbNewLine _
        & "    , @PKG_NB                       " & vbNewLine _
        & "    , @PKG_UT                       " & vbNewLine _
        & "    , @INKA_PKG_NB                  " & vbNewLine _
        & "    , @HASU                         " & vbNewLine _
        & "    , @STD_IRIME                    " & vbNewLine _
        & "    , @STD_IRIME_UT                 " & vbNewLine _
        & "    , @BETU_WT                      " & vbNewLine _
        & "    , @CBM                          " & vbNewLine _
        & "    , @ONDO_KB                      " & vbNewLine _
        & "    , @OUTKA_FROM_ORD_NO            " & vbNewLine _
        & "    , @BUYER_ORD_NO                 " & vbNewLine _
        & "    , @REMARK                       " & vbNewLine _
        & "    , @LOT_NO                       " & vbNewLine _
        & "    , @SERIAL_NO                    " & vbNewLine _
        & "    , @IRIME                        " & vbNewLine _
        & "    , @IRIME_UT                     " & vbNewLine _
        & "    , @OUT_KB                       " & vbNewLine _
        & "    , @AKAKURO_KB                   " & vbNewLine _
        & "    , @JISSEKI_FLAG                 " & vbNewLine _
        & "    , @JISSEKI_USER                 " & vbNewLine _
        & "    , @JISSEKI_DATE                 " & vbNewLine _
        & "    , @JISSEKI_TIME                 " & vbNewLine _
        & "    , @FREE_N01                     " & vbNewLine _
        & "    , @FREE_N02                     " & vbNewLine _
        & "    , @FREE_N03                     " & vbNewLine _
        & "    , @FREE_N04                     " & vbNewLine _
        & "    , @FREE_N05                     " & vbNewLine _
        & "    , @FREE_N06                     " & vbNewLine _
        & "    , @FREE_N07                     " & vbNewLine _
        & "    , @FREE_N08                     " & vbNewLine _
        & "    , @FREE_N09                     " & vbNewLine _
        & "    , @FREE_N10                     " & vbNewLine _
        & "    , @FREE_C01                     " & vbNewLine _
        & "    , @FREE_C02                     " & vbNewLine _
        & "    , @FREE_C03                     " & vbNewLine _
        & "    , @FREE_C04                     " & vbNewLine _
        & "    , @FREE_C05                     " & vbNewLine _
        & "    , @FREE_C06                     " & vbNewLine _
        & "    , @FREE_C07                     " & vbNewLine _
        & "    , @FREE_C08                     " & vbNewLine _
        & "    , @FREE_C09                     " & vbNewLine _
        & "    , @FREE_C10                     " & vbNewLine _
        & "    , @FREE_C11                     " & vbNewLine _
        & "    , @FREE_C12                     " & vbNewLine _
        & "    , @FREE_C13                     " & vbNewLine _
        & "    , @FREE_C14                     " & vbNewLine _
        & "    , @FREE_C15                     " & vbNewLine _
        & "    , @FREE_C16                     " & vbNewLine _
        & "    , @FREE_C17                     " & vbNewLine _
        & "    , @FREE_C18                     " & vbNewLine _
        & "    , @FREE_C19                     " & vbNewLine _
        & "    , @FREE_C20                     " & vbNewLine _
        & "    , @FREE_C21                     " & vbNewLine _
        & "    , @FREE_C22                     " & vbNewLine _
        & "    , @FREE_C23                     " & vbNewLine _
        & "    , @FREE_C24                     " & vbNewLine _
        & "    , @FREE_C25                     " & vbNewLine _
        & "    , @FREE_C26                     " & vbNewLine _
        & "    , @FREE_C27                     " & vbNewLine _
        & "    , @FREE_C28                     " & vbNewLine _
        & "    , @FREE_C29                     " & vbNewLine _
        & "    , @FREE_C30                     " & vbNewLine _
        & "    , @CRT_USER                     " & vbNewLine _
        & "    , @CRT_DATE                     " & vbNewLine _
        & "    , @CRT_TIME                     " & vbNewLine _
        & "    , @UPD_USER                     " & vbNewLine _
        & "    , @UPD_DATE                     " & vbNewLine _
        & "    , @UPD_TIME                     " & vbNewLine _
        & "    , @SYS_ENT_DATE                 " & vbNewLine _
        & "    , @SYS_ENT_TIME                 " & vbNewLine _
        & "    , @SYS_ENT_PGID                 " & vbNewLine _
        & "    , @SYS_ENT_USER                 " & vbNewLine _
        & "    , @SYS_UPD_DATE                 " & vbNewLine _
        & "    , @SYS_UPD_TIME                 " & vbNewLine _
        & "    , @SYS_UPD_PGID                 " & vbNewLine _
        & "    , @SYS_UPD_USER                 " & vbNewLine _
        & "    , @SYS_DEL_FLG                  " & vbNewLine _
        & ")                                   " & vbNewLine _
        & ""

#End Region ' "H_INKAEDI_M"

#End Region ' "セミEDI処理"

#End Region

#Region "DELETE処理"

#Region "SEND(実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' SEND(実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_JISSEKIMODOSI_SEND As String = "DELETE                                    " & vbNewLine _
                                              & " $LM_TRN$..H_SENDINEDI_NCGO                        " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine

#End Region

#Region "セミEDI処理"

#Region "EDI受信(DTL)テーブル 物理削除 SQL"

    ''' <summary>
    ''' EDI受信(DTL)テーブル 物理削除 SQL
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_DELETE_INKAEDI_DTL As String = "" _
        & " DELETE                               " & vbNewLine _
        & " FROM                                 " & vbNewLine _
        & "     $LM_TRN$..H_INKAEDI_DTL_NCGO_NEW " & vbNewLine _
        & " WHERE                                " & vbNewLine _
        & "     CRT_DATE  = @CRT_DATE            " & vbNewLine _
        & " AND FILE_NAME = @FILE_NAME           " & vbNewLine _
        & " AND REC_NO    = @REC_NO              " & vbNewLine _
        & " AND GYO       = @GYO                 " & vbNewLine

#End Region ' "EDI受信(DTL)テーブル 物理削除 SQL"

#End Region ' "セミEDI処理"

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

        Dim eventShubetsu As Integer = 0
        If (IsDBNull(Me._Row("EVENT_SHUBETSU")) = False) Then
            Integer.TryParse(TryCast(Me._Row("EVENT_SHUBETSU"), String), eventShubetsu)
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_EDI_L)

        Call Me.SetPrmEdiL(eventShubetsu)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "SelectEdiL", cmd)

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
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_EDI_M)

        Call Me.SetPrmEdiM()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "SelectEdiM", cmd)

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
    Private Sub SetPrmEdiL(Optional ByVal eventShubetsu As Integer = 0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row("WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        If (CType(LMH010DAC.EventShubetsu.JIKKOU_TRANSFER_COND_M, Integer) = eventShubetsu) Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", LMConst.FLG.ON, DBDataType.CHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", LMConst.FLG.OFF, DBDataType.CHAR))
        End If

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

#Region "実績送信データ取得処理"
    ''' <summary>
    ''' 実績送信データ取得処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_SEND)

        Call Me.SetPrmEdiL()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "SelectSend", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable

        '取得データの格納先をマッピング
        map = Me.SetMapSend()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_SENDINEDI_NCGO")
        reader.Close()

        Return ds
    End Function

#End Region

#Region "実績作成オーダー番号チェック"

    ''' <summary>
    ''' 受付№、受付枝№取得処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUketsukeNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_UKETSUKENO)

        Call Me.SetPrmEdiL()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "SelectUketsukeNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UKETSUKE_NO", "UKETSUKE_NO")
        map.Add("UKETSUKE_NO_EDA", "UKETSUKE_NO_EDA")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_SENDINEDI_NCGO")
        reader.Close()

        Return ds
    End Function

    ''' <summary>
    ''' 同一オーダー番号（枝番含まず）での変更データの有無確認
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OrderChk(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_ORDER_COUNT)

        Dim dtIn As DataTable = ds.Tables("LMH010INOUT")

        Dim dt As DataTable = ds.Tables("H_SENDINEDI_NCGO")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOrderChkPrm(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtIn.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "OrderChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 同一オーダー番号（枝番含まず）での変更データの有無確認
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OrderChkTorikeshi(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_ORDER_TORIKESHI_COUNT)

        Dim dtIn As DataTable = ds.Tables("LMH010INOUT")

        Dim dt As DataTable = ds.Tables("H_SENDINEDI_NCGO")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOrderChkPrm(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtIn.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "OrderChkTorikeshi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

#End Region



#Region "M品在庫取得"

    ''' <summary>
    ''' M品在庫取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectZaiTrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.LMH010INOUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_ZAI_TRS_COND_M)

        'パラメータ設定
        Me.SetPrmSelectZaiTrsCondM()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item("NRS_BR_CD").ToString())
        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                    , Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim tableName As String = TABLE_NM.LMH010_ZAI_TRS_GOODS

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                Dim map As Hashtable = New Hashtable()
                For Each item As String In Enumerable.Range(0, reader.FieldCount) _
                                                     .Select(Function(i) reader.GetName(i))

                    If (ds.Tables(tableName).Columns.Contains(item)) Then
                        map.Add(item, item)
#If False Then  ' データセットのカラム名誤りを確認用コード(開発時のみ使用)
                    Else

                        Dim skipColums As String() = {}
                        If (skipColums.Contains(item)) Then
                            Throw New NotImplementedException(item)
                        End If
#End If
                    End If
                Next

                'DataReader→DataTableへの転記
                ds = MyBase.SetSelectResultToDataSet(map _
                                                   , ds _
                                                   , reader _
                                                   , tableName)
            End Using
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPrmSelectZaiTrsCondM()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M").ToString(), DBDataType.CHAR))

    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDestParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row("CUST_CD_M").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaMParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me._Row("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))

    End Sub


    Private Sub SetUpdateEdiInkaLCondMParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me._Row("FREE_C24").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub



    ''' <summary>
    ''' M品振替届先取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectDestCondM(ByVal ds As DataSet) As DataSet


        ' SQL設定
        Me._StrSql = New StringBuilder(LMH010DAC601.SQL_SELECT_M_DEST_COND_M)

        'INTableの条件rowの格納
        Me._Row = ds.Tables(TABLE_NM.LMH010INOUT).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetDestParameter()

        ' 格納先テーブル
        Dim outTableName As String = TABLE_NM.LMH010_DEST

        ' 検索実行
        Return Me.ExecuteSelect(ds _
                              , outTableName _
                              , MethodBase.GetCurrentMethod().Name)

    End Function



    ''' <summary>
    ''' 入荷L取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInkaL(ByVal ds As DataSet) As DataSet

        ' SQL設定
        Me._StrSql = New StringBuilder(LMH010DAC601.SQL_SELECT_INKA_L)

        ' 条件行設定
        Me._Row = ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetInkaMParameter()

        ' 格納先テーブル
        Dim outTableName As String = TABLE_NM.LMH010_B_INKA_L

        ' 検索実行
        Return Me.ExecuteSelect(ds _
                              , outTableName _
                              , MethodBase.GetCurrentMethod.Name)

    End Function


    ''' <summary>
    '''  入荷M取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInkaM(ByVal ds As DataSet) As DataSet

        ' SQL設定
        Me._StrSql = New StringBuilder(LMH010DAC601.SQL_SELECT_INKA_M)

        ' 条件行設定
        Me._Row = ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetInkaMParameter()

        ' 格納先テーブル
        Dim outTableName As String = TABLE_NM.LMH010_B_INKA_M

        ' 検索実行
        Return Me.ExecuteSelect(ds _
                              , outTableName _
                              , MethodBase.GetCurrentMethod.Name)

    End Function



    ''' <summary>
    '''  入荷SNo最大値取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMaxInkaS(ByVal ds As DataSet) As DataSet

        ' SQL設定
        Me._StrSql = New StringBuilder(LMH010DAC601.SQL_SELECT_MAX_INKA_S_NO)

        ' 条件行設定
        Me._Row = ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me.SetInkaMParameter()

        ' 格納先テーブル
        Dim outTableName As String = TABLE_NM.LMH010_B_INKA_S

        ' 検索実行
        Return Me.ExecuteSelect(ds _
                              , outTableName _
                              , MethodBase.GetCurrentMethod.Name)

    End Function


    ''' <summary>
    ''' SELECT実行(汎用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="outTableName"></param>
    ''' <param name="methodName">呼び出し元メソッド名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecuteSelect(ByVal ds As DataSet _
                                 , ByVal outTableName As String _
                                 , ByVal methodName As String
                                 ) As DataSet

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(Me.GetType.Name, methodName, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                Dim map As Hashtable = New Hashtable()
                For Each item As String In Enumerable.Range(0, reader.FieldCount) _
                                                     .Select(Function(i) reader.GetName(i))

                    If (ds.Tables(outTableName).Columns.Contains(item)) Then
                        map.Add(item, item)
                    End If
                Next

                'DataReader→DataTableへの転記
                Return MyBase.SetSelectResultToDataSet(map _
                                                     , ds _
                                                     , reader _
                                                     , outTableName)
            End Using
        End Using

    End Function

#End Region


#Region "セミEDI処理"

#Region "M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)"

    ''' <summary>
    ''' M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckGoods(ByVal ds As DataSet) As DataSet

        Dim dtRcvDtl As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_DTL")
        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)
        Dim resultCount As Integer = 0

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_GOODS_CNT)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectChkGoodsParameter(drSemiEdiInfo, dtRcvDtl)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), drSemiEdiInfo.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                reader.Read()

                '商品件数の設定
                resultCount = Convert.ToInt32(reader("MST_CNT"))

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

#End Region ' M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)

#Region "EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT"

    ''' <summary>
    ''' EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectInkaCntAndNoL(ByVal ds As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_INKA_NO_L_AND_CNT)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamSelectInkaCntAndNoL(dt)

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        Dim cnt As Integer = 0

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                reader.Read()

                ' 戻り値の設定
                dt.Rows(0).Item("INKA_CTL_NO_L") = reader("INKA_NO_L").ToString()
                dt.Rows(0).Item("EDI_CTL_NO") = reader("EDI_CTL_NO").ToString()

                ' 処理件数の設定
                cnt = Convert.ToInt32(reader("INKA_CNT"))

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(cnt)

        Return ds

    End Function

#End Region ' "EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT"

#Region "EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得"

    ''' <summary>
    ''' EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    Private Function SelectInkaEdiHacchuDenpNoAndDtlNoAndIoDenpNo(ByVal setDs As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_INKAEDI_DTL_DATA_ID_DETAIL_DENP_NO_AND_DTL_NO)

        Dim dt As DataTable = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamSelectInkaEdiHacchuDenpNoAndDtlNoAndIoDenpNo(dt)

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        Dim resultCount As Integer = 0

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("REC_NO", "REC_NO")
                map.Add("DATA_ID_DETAIL", "DATA_ID_DETAIL")
                map.Add("GYO", "GYO")
                map.Add("HACCHU_DENP_NO", "HACCHU_DENP_NO")
                map.Add("HACCHU_DENP_DTL_NO", "HACCHU_DENP_DTL_NO")
                map.Add("IO_DENP_NO", "IO_DENP_NO")

                'DataReader→DataTableへの転記
                '(IN 条件に使用した DataTable に上書き)
                setDs = MyBase.SetSelectResultToDataSet(map, setDs, reader, "LMH010_INKAEDI_DTL_NCGO_NEW")
                resultCount = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows.Count()

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(resultCount)

        Return setDs

    End Function

#End Region ' "EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出)"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaediDtlNcgoNewCancel(ByVal ds As DataSet) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0)
        Dim resultCount As Integer = 0

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_INKAEDI_DTL_NCGO_NEW_CANCEL)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectInkaediDtlNcgoNewCancelParameter(drSemiEdiInfo, drEdiRcvDtl)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), drSemiEdiInfo.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)


                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("EDI_CTL_NO", "EDI_CTL_NO")

                'DataReader→DataTableへの転記
                '(IN 条件に使用した DataTable に上書き)
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_INKAEDI_DTL_NCGO_NEW")
                resultCount = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows.Count()

            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出)"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大) 登録用)"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大) 登録用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectForInkaediL_FromInkaediDtlNcgoNew(ByVal ds As DataSet) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0)
        Dim resultCount As Integer = 0

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_FOR_INKAEDI_L_FROM_INKAEDI_DTL_NCGO_NEW)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectForInkaediL_M_FromInkaediDtlNcgoNewParameter(drSemiEdiInfo, drEdiRcvDtl)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), drSemiEdiInfo.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)


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

                'DataReader→DataTableへの転記
                '(IN 条件に使用した DataTable に上書き)
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_INKAEDI_L")
            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大) 登録用)"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(中) 登録用)"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(中) 登録用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectForInkaediM_FromInkaediDtlNcgoNew(ByVal ds As DataSet) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)
        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0)
        Dim resultCount As Integer = 0

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_SELECT_FOR_INKAEDI_M_FROM_INKAEDI_DTL_NCGO_NEW)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSelectForInkaediL_M_FromInkaediDtlNcgoNewParameter(drSemiEdiInfo, drEdiRcvDtl)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), drSemiEdiInfo.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)


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

                'DataReader→DataTableへの転記
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_INKAEDI_M")
            End Using

            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(中) 登録用)"

#End Region ' "セミEDI処理"

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

                setSql = LMH010DAC601.SQL_UPD_INKATOROKU_EDI_L

                '実績取消
            Case LMH010DAC.EventShubetsu.JISSEKI_TORIKESI

                setSql = LMH010DAC601.SQL_UPD_JISSEKITORIKESI_EDI_L

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC601.SQL_UPD_EDITORIKESI_EDI_L

                '実績作成済⇒実績未、実績送信済⇒実績未、実績作成
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JISSEKI_SAKUSE

                setSql = LMH010DAC601.SQL_UPD_JISSEKIMODOSI_EDI_L

            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                setSql = LMH010DAC601.SQL_UPD_TOUROKUMI_EDI_L

                '送信済み⇒送信待ち
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC601.SQL_JISSEKIZUMI_SOUSINMACHI_EDI_L

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "LMH010_INKAEDI_L", cmd)

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

                setSql = LMH010DAC601.SQL_UPD_INKATOROKU_EDI_M

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC601.SQL_UPD_EDITORIKESI_EDI_M

                '実績作成済⇒実績未、実績送信済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                setSql = LMH010DAC601.SQL_UPD_JISSEKIMODOSI_EDI_M

                '実績作成
            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE

                setSql = LMH010DAC601.SQL_UPD_JISSEKISAKUSEI_EDI_M

            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                setSql = LMH010DAC601.SQL_UPD_TOUROKUMI_EDI_M

                '送信済み⇒送信待ち
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC601.SQL_JISSEKIZUMI_SOUSINMACHI_EDI_M

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

            MyBase.Logger.WriteSQLLog("LMH010DAC601", "UpdateEdiM", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

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

                setSql = LMH010DAC601.SQL_UPD_RCV_HED

            Case LMH010DAC.EventShubetsu.JISSEKI_TORIKESI

                setSql = LMH010DAC601.SQL_UPD_JISSEKITORIKESI_RCV_HED

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC601.SQL_UPD_EDITORIKESI_RCV_HED

                '実績作成済⇒実績未、実績送信済⇒実績未、実績作成
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JISSEKI_SAKUSE

                setSql = LMH010DAC601.SQL_UPD_JISSEKIMODOSI_RCV_HED

                '実績送信済⇒送信未
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC601.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED

            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                setSql = LMH010DAC601.SQL_UPD_TOUROKUMI_RCV_HED

        End Select


        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetUpdPrmRcvHed(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))
        Call Me.SetUpdPrmDelDateRcv(eventShubetsu)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateRcvHed", cmd)

        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

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
                setSql = LMH010DAC601.SQL_UPDATE_RCV_DTL

                '実績取消
            Case LMH010DAC.EventShubetsu.JISSEKI_TORIKESI
                setSql = LMH010DAC601.SQL_UPD_JISSEKITORIKESI_RCV_DTL

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC601.SQL_UPD_EDITORIKESI_RCV_DTL

                '実績作成済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI

                setSql = LMH010DAC601.SQL_UPD_JISSEKIZUMI_JISSEKIMI_RCV_DTL

                '実績送信済⇒実績未、
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                setSql = LMH010DAC601.SQL_UPD_SOUSINZUMI_JISSEKIMI_RCV_DTL

                '実績送信済⇒送信未
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC601.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL

                '実績作成
            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE

                setSql = LMH010DAC601.SQL_UPD_JISSEKISAKUSEI_RCV_DTL

            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                setSql = LMH010DAC601.SQL_UPD_TOUROKUMI_RCV_DTL

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
            Call Me.SetUpdPrmDelDateRcv(eventShubetsu)

            Dim methodNm As String = Reflection.MethodBase.GetCurrentMethod.Name


            Call Me.SetJissekiParameterRcv(dtRow, eventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC601", "UpdateRcvDtl", cmd)

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

        setSql = SQL_UPD_JISSEKIMODOSI_INKA_L

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateInkaL", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "SEND"

    ''' <summary>
    ''' EDI送信テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSend(ByVal ds As DataSet) As DataSet

        'Delete件数格納変数
        Dim DelCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_SEND")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()


        Dim setSql As String = String.Empty

        setSql = SQL_UPD_JISSEKIMODOSI_SEND

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetPrmRcvSend(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateSend", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "D_ZAI_TRS"


    ''' <summary>
    ''' 在庫更新(M品在庫0)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTrsCondM(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String _
            = ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).Rows(0)("NRS_BR_CD").ToString()

        Dim resultCount As Integer = 0

        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Using cmd As SqlCommand _
            = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_UPD_D_ZAI_TRS_COND_M _
                                                   , nrsBrCd))

            For Each row As DataRow In ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).Rows

                'INTableの条件rowの格納
                Me._Row = row

                'パラメータ設定
                Me._SqlPrmList.Clear()
                Me.SetUpdZaiTrsParameter()

                'パラメータの反映
                cmd.Parameters.Clear()
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                        , Reflection.MethodBase.GetCurrentMethod.Name _
                                        , cmd)

                'SQLの発行
                resultCount += MyBase.GetUpdateResult(cmd)
            Next

            MyBase.SetResultCount(resultCount)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 入荷S更新(削除フラグON)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaSSysDelFlgOn(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String _
            = ds.Tables(TABLE_NM.LMH010INOUT).Rows(0)("NRS_BR_CD").ToString()

        Dim resultCount As Integer = 0

        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Using cmd As SqlCommand _
            = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_UPD_INKA_S_SYS_DEL_FLG_ON _
                                                   , nrsBrCd))

            For Each row As DataRow In ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows

                'INTableの条件rowの格納
                Me._Row = row

                'パラメータ設定
                Me._SqlPrmList.Clear()
                Me.SetInkaMParameter()
                Me.SetSysdataParameter()

                'パラメータの反映
                cmd.Parameters.Clear()
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                        , Reflection.MethodBase.GetCurrentMethod.Name _
                                        , cmd)

                'SQLの発行
                resultCount += MyBase.GetUpdateResult(cmd)
            Next

            MyBase.SetResultCount(resultCount)

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' EDIINKAL更新(M品振替追記)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiInkaLCondM(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String _
            = ds.Tables(TABLE_NM.LMH010INOUT).Rows(0)("NRS_BR_CD").ToString()

        Dim resultCount As Integer = 0

        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Using cmd As SqlCommand _
            = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_UPD_INKAEDI_L_COND_M _
                                                   , nrsBrCd))

            For Each row As DataRow In ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows

                'INTableの条件rowの格納
                Me._Row = row

                'パラメータ設定
                Me._SqlPrmList.Clear()
                Me.SetUpdateEdiInkaLCondMParameter()
                Me.SetSysdataParameter()

                'パラメータの反映
                cmd.Parameters.Clear()
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                        , Reflection.MethodBase.GetCurrentMethod.Name _
                                        , cmd)

                'SQLの発行
                resultCount += MyBase.GetUpdateResult(cmd)
            Next

            MyBase.SetResultCount(resultCount)

        End Using

        Return ds

    End Function

#End Region

#Region "セミEDI処理"

#Region "EDI入荷(大)テーブル更新(論理削除)"

    ''' <summary>
    ''' EDI入荷(大)テーブル更新(論理削除)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateDelInkaEdiL(ByVal ds As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_UPDATE_DEL_INKAIEDI_L)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamUpdateDelInkaEdiL_M_Dtl(dt)
        Call Me.SetSysdataParameter()

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            ' SQLの発行および処理件数の設定
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#End Region ' "EDI入荷(大)テーブル更新(論理削除)"

#Region "EDI入荷(中)テーブル更新(論理削除)"

    ''' <summary>
    ''' EDI入荷(中)テーブル更新(論理削除)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateDelInkaEdiM(ByVal ds As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_UPDATE_DEL_INKAIEDI_M)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamUpdateDelInkaEdiL_M_Dtl(dt)
        Call Me.SetSysdataParameter()

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            ' SQLの発行および処理件数の設定
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#End Region ' "EDI入荷(中)テーブル更新(論理削除)"

#Region "EDI受信データ(DTL)テーブル更新(論理削除)"

    ''' <summary>
    ''' EDI受信データ(DTL)テーブル更新(論理削除)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateDelInkaEdiDtl(ByVal ds As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_UPDATE_DEL_INKAEDI_DTL)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamUpdateDelInkaEdiL_M_Dtl(dt)
        Call Me.SetSysdataParameter()

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            ' SQLの発行および処理件数の設定
            MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

            'パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#End Region ' "EDI受信データ(DTL)テーブル更新(論理削除)"

#Region "H_INKAEDI_DTL_NCGO_NEW 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 更新 (セミEDI時・入荷赤伝・取消・論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaediDtlNcgoNewCancel(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dtInkaediDtlNcgoNew As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")
        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim setSql As String = LMH010DAC601.SQL_CANCEL_INKAEDI_DTL_NCGO_NEW

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, drSemiEdiInfo.Item("NRS_BR_CD").ToString()))
        Dim maxIdx As Integer = dtInkaediDtlNcgoNew.Rows().Count() - 1
        Dim j As Integer = 0
        For i As Integer = 0 To maxIdx
            j += 1
            If i = 0 Then
                Me._StrSql.Append(String.Concat("     AND(   (EDI_CTL_NO = ", "@EDI_CTL_NO_", j.ToString(), If(i < maxIdx, ")", "))"), vbNewLine))
            Else
                Me._StrSql.Append(String.Concat("         OR (EDI_CTL_NO = ", "@EDI_CTL_NO_", j.ToString(), If(i < maxIdx, ")", "))"), vbNewLine))
            End If
        Next

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetUpdateInkaediDtlNcgoNewCancelParameter(drSemiEdiInfo, dtInkaediDtlNcgoNew)
        Call Me.SetSysdataParameter()

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            updCnt = MyBase.GetUpdateResult(cmd)

        End Using

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

#Region "H_INKAEDI_L 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

    ''' <summary>
    ''' H_INKAEDI_L 更新 (セミEDI時・入荷赤伝・取消・論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaediL_Cancel(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dtInkaediDtlNcgoNew As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")
        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim setSql As String = LMH010DAC601.SQL_CANCEL_INKAEDI_L

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, drSemiEdiInfo.Item("NRS_BR_CD").ToString()))
        Dim maxIdx As Integer = dtInkaediDtlNcgoNew.Rows().Count() - 1
        Dim j As Integer = 0
        For i As Integer = 0 To maxIdx
            j += 1
            If i = 0 Then
                Me._StrSql.Append(String.Concat("     AND(   (EDI_CTL_NO = ", "@EDI_CTL_NO_", j.ToString(), If(i < maxIdx, ")", "))"), vbNewLine))
            Else
                Me._StrSql.Append(String.Concat("         OR (EDI_CTL_NO = ", "@EDI_CTL_NO_", j.ToString(), If(i < maxIdx, ")", "))"), vbNewLine))
            End If
        Next

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetUpdateInkaediL_M_CancelParameter(drSemiEdiInfo, dtInkaediDtlNcgoNew)
        Call Me.SetSysdataParameter()

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            updCnt = MyBase.GetUpdateResult(cmd)

        End Using

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#End Region ' "H_INKAEDI_L 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

#Region "H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

    ''' <summary>
    ''' H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaediM_Cancel(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dtInkaediDtlNcgoNew As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")
        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim setSql As String = LMH010DAC601.SQL_CANCEL_INKAEDI_M

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, drSemiEdiInfo.Item("NRS_BR_CD").ToString()))
        Dim maxIdx As Integer = dtInkaediDtlNcgoNew.Rows().Count() - 1
        Dim j As Integer = 0
        For i As Integer = 0 To maxIdx
            j += 1
            If i = 0 Then
                Me._StrSql.Append(String.Concat("     AND(   (EDI_CTL_NO = ", "@EDI_CTL_NO_", j.ToString(), If(i < maxIdx, ")", "))"), vbNewLine))
            Else
                Me._StrSql.Append(String.Concat("         OR (EDI_CTL_NO = ", "@EDI_CTL_NO_", j.ToString(), If(i < maxIdx, ")", "))"), vbNewLine))
            End If
        Next

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetUpdateInkaediL_M_CancelParameter(drSemiEdiInfo, dtInkaediDtlNcgoNew)
        Call Me.SetSysdataParameter()

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            updCnt = MyBase.GetUpdateResult(cmd)

        End Using

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#End Region ' "H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除)"

#Region "H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)"

    ''' <summary>
    ''' H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoediDtlNcgo_EdiCtlNo(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim drInkaediDtlNcgoNew As DataRow = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0)

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(LMH010DAC601.SQL_UPDATE_UNSOEDI_DTL_NCGO_EDI_CTR_NO, drInkaediDtlNcgoNew.Item("NRS_BR_CD").ToString())

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetUpdateUnsoediDtlNcgo_EdiCtlNoParameter(drInkaediDtlNcgoNew)

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            'SQLの発行
            updCnt = MyBase.GetUpdateResult(cmd)

        End Using

        MyBase.SetResultCount(updCnt)

        Return ds

    End Function

#End Region ' "H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)"

#End Region ' "セミEDI処理"


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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_INKA_L, dtRow.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetDataInsertParameter()
        Call Me.SetInkaLComParameter(dtRow)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "InsertInkaL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        MyBase.SetResultCount(rtn)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_INKA_M, brCd))

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

            MyBase.Logger.WriteSQLLog("LMH010DAC", "InsertInkaM", cmd)

            'SQLの発行
            rtn += MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(rtn)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_INKA_S, brCd))

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

            MyBase.Logger.WriteSQLLog("LMH010DAC", "InsertInkaS", cmd)

            'SQLの発行
            rtn += MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        MyBase.SetResultCount(rtn)

        Return ds

    End Function

#End Region

#Region "SEND"

    ''' <summary>
    ''' 実績送信新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>実績送信新規登録SQLの構築・発行</remarks>
    Private Function InsertSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("H_SENDINEDI_NCGO")
        'INTableの条件rowの格納
        Dim dtRow As DataRow = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_SEND, dtRow.Item("NRS_BR_CD").ToString()))
        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))


        'パラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetDataInsertParameter()
        Call Me.SetPrmSendIn(dtRow)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "InsertSend", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_SAGYO, nrsBrCd))
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

            MyBase.Logger.WriteSQLLog("LMH010DAC601", "InsertSagyo", cmd)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_UNSO_L, nrsBrCd))

        'パラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetDataInsertParameter()
        Call Me.SetUnsoLComParameter(drUnso)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC601", "InsertUnsoL", cmd)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_UNSO_M, nrsBrCd))


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

            MyBase.Logger.WriteSQLLog("LMH010DAC601", "InsertUnsoM", cmd)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables("F_UNCHIN_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter()
            Call Me.SetSysdataParameter()
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC601", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region


#Region "OUTKA_L"

    ''' <summary>
    ''' 出荷L登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaL(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables(TABLE_NM.LMH010_OUTKA_L).Rows(0)

        Dim nrsBrCd As String = Me._Row("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Using cmd As SqlCommand _
            = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_OUTKA_L _
                                                   , nrsBrCd))

            'パラメータ設定
            Me._SqlPrmList = New ArrayList()
            Me.SetOutkaLParameter()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                    , Reflection.MethodBase.GetCurrentMethod.Name _
                                    , cmd)

            'SQLの発行
            Dim resultCount As Integer = MyBase.GetInsertResult(cmd)

            MyBase.SetResultCount(resultCount)

        End Using

        Return ds

    End Function

#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' 出荷M登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaM(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Dim nrsBrCd As String = ds.Tables(TABLE_NM.LMH010_OUTKA_M).Rows(0)("NRS_BR_CD").ToString()

        'SQL文のコンパイル
        Using cmd As SqlCommand _
            = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_OUTKA_M _
                                                   , nrsBrCd))

            Dim resultCount As Integer = 0
            Me._SqlPrmList = New ArrayList()

            For Each row As DataRow In ds.Tables(TABLE_NM.LMH010_OUTKA_M).Rows

                Me._Row = row

                'パラメータ設定
                Me._SqlPrmList.Clear()
                Me.SetOutkaMParameter()

                'パラメータの反映
                cmd.Parameters.Clear()
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                        , Reflection.MethodBase.GetCurrentMethod.Name _
                                        , cmd)

                'SQLの発行
                resultCount += MyBase.GetInsertResult(cmd)
            Next

            MyBase.SetResultCount(resultCount)

        End Using

        Return ds

    End Function

#End Region

#Region "OUTKA_S"


    ''' <summary>
    ''' 出荷S登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaS(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String = ds.Tables(TABLE_NM.LMH010_OUTKA_S).Rows(0)("NRS_BR_CD").ToString()

        Dim resultCount As Integer = 0

        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Using cmd As SqlCommand _
            = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_OUTKA_S _
                                                   , nrsBrCd))

            For Each row As DataRow In ds.Tables(TABLE_NM.LMH010_OUTKA_S).Rows

                'INTableの条件rowの格納
                Me._Row = row

                'パラメータ設定
                Me._SqlPrmList.Clear()
                Me.SetOutkaSParameter()

                'パラメータの反映
                cmd.Parameters.Clear()
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name _
                                        , Reflection.MethodBase.GetCurrentMethod.Name _
                                        , cmd)

                'SQLの発行
                resultCount += MyBase.GetInsertResult(cmd)
            Next

            MyBase.SetResultCount(resultCount)

        End Using

        Return ds

    End Function

#End Region

#Region "セミEDI処理"

#Region "H_INKAEDI_DTL_NCGO_NEW(セミEDI)"

    ''' <summary>
    ''' EDI受信(DTL)テーブル新規登録
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertInkaEdiRcvDtl(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtInkaEdiRcvDtl As DataTable = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_INKAEDI_DTL, dtInkaEdiRcvDtl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            For Each drInkaEdiRcvDtl As DataRow In dtInkaEdiRcvDtl.Rows

                '条件rowの格納
                Me._Row = drInkaEdiRcvDtl

                'パラメータ設定
                Call Me.SetInsertInkaEdiRcvDtlParameter(Me._Row, Me._SqlPrmList)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

                'パラメータの初期化
                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

            Next

        End Using

        Return setDs

    End Function

#End Region ' "H_INKAEDI_DTL_NCGO_NEW(セミEDI)"

#Region "H_INKAEDI_L(セミEDI)"

    ''' <summary>
    ''' EDI入荷データ(大)テーブル新規登録
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI入荷データ(大)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertInkaEdiL(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtInkaEdiL As DataTable = setDs.Tables("LMH010_INKAEDI_L")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_INKAEDI_L, dtInkaEdiL.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            For Each drInkaEdiL As DataRow In dtInkaEdiL.Rows

                '条件rowの格納
                Me._Row = drInkaEdiL

                'パラメータ設定
                Call Me.SetInsertInEdiL_Parameter(Me._Row, Me._SqlPrmList)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

                'パラメータの初期化
                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

            Next

        End Using

        Return setDs

    End Function

#End Region ' "H_INKAEDI_L(セミEDI)"

#Region "H_INKAEDI_M(セミEDI)"

    ''' <summary>
    ''' EDI入荷データ(中)テーブル新規登録
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI入荷データ(中)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertInkaEdiM(ByVal setDs As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtInkaEdiM As DataTable = setDs.Tables("LMH010_INKAEDI_M")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMH010DAC601.SQL_INSERT_INKAEDI_M, dtInkaEdiM.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            For Each drInkaEdiM As DataRow In dtInkaEdiM.Rows

                '条件rowの格納
                Me._Row = drInkaEdiM

                'パラメータ設定
                Call Me.SetInsertInkaEdiM_Parameter(Me._Row, Me._SqlPrmList)

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

                'パラメータの初期化
                Me._SqlPrmList.Clear()
                cmd.Parameters.Clear()

            Next

        End Using

        Return setDs

    End Function

#End Region ' "H_INKAEDI_M(セミEDI)"

#End Region ' "セミEDI処理"


#End Region

#Region "DELETE処理"

#Region "SEND"

    ''' <summary>
    ''' EDI送信テーブル削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル削除SQLの構築・発行</remarks>
    Private Function DeleteSend(ByVal ds As DataSet) As DataSet

        'Delete件数格納変数
        Dim DelCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_SEND")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()


        Dim setSql As String = String.Empty

        setSql = SQL_DEL_JISSEKIMODOSI_SEND

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetPrmRcvSend(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "DeleteSend", cmd)

        'SQLの発行
        If MyBase.GetDeleteResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "セミEDI処理"

#Region "EDI受信(DTL)テーブル 物理削除"

    ''' <summary>
    ''' EDI受信(DTL)テーブル 物理削除
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    Private Function DeleteInkaEdi(ByVal setDs As DataSet) As DataSet

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMH010DAC601.SQL_DELETE_INKAEDI_DTL)

        Dim dt As DataTable = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW")

        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamDeleteInkaEdi(dt)

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, Reflection.MethodBase.GetCurrentMethod().Name, cmd)

            ' SQLの発行
            MyBase.GetDeleteResult(cmd)

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return setDs

    End Function

#End Region ' "EDI受信(DTL)テーブル 物理削除"

#End Region ' "セミEDI処理"

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

            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                 , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

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

            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                 , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case LMH010DAC.EventShubetsu.JISSEKI_SAKUSE
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 更新パラメータ削除日時設定(RCV)
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmDelDateRcv(ByVal eventShubetsu As Integer)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)
            'EDI取消、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", updTime, DBDataType.CHAR))

                'EDI取消⇒未登録、実績作成済⇒実績未、実績送信済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.CHAR))

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

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
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

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_JISSEKI_FLG", Me.NullConvertString(dt.Rows(0).Item("INKA_JISSEKI_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_USER", Me.NullConvertString(dt.Rows(0).Item("INKA_USER")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(dt.Rows(0).Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", Me.NullConvertString(dt.Rows(0).Item("INKA_TIME")), DBDataType.CHAR))


    End Sub

#End Region

#Region "更新パラメータ設定(RCV_DTL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmRcvDtl(ByVal row As DataRow)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_JISSEKI_FLG", Me.NullConvertString(row.Item("INKA_JISSEKI_FLG")), DBDataType.CHAR))
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", row.Item("LOCA").ToString(), DBDataType.NVARCHAR))
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

#Region "更新パラメータ設定(SEND)"
    ''' <summary>
    ''' 更新,検索パラメータ設定(SEND)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetPrmRcvSend(ByVal dt As DataTable)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
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

#Region "挿入パラメータ設定(SEND)"
    Private Sub SetPrmSendIn(ByVal row As DataRow)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(row.Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_EDA", Me.NullConvertString(row.Item("EDI_CTL_NO_EDA")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(row.Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(row.Item("FILE_NAME")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(row.Item("REC_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RCV_ID", Me.NullConvertString(row.Item("RCV_ID")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RCV_UKETSUKE_NO", Me.NullConvertString(row.Item("RCV_UKETSUKE_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RCV_UKETSUKE_NO_EDA", Me.NullConvertString(row.Item("RCV_UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RCV_INPUT_KB", Me.NullConvertString(row.Item("RCV_INPUT_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RCV_EDA_UP_FLG", Me.NullConvertString(row.Item("RCV_EDA_UP_FLG")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ID", Me.NullConvertString(row.Item("ID")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYSTEM_KB", Me.NullConvertString(row.Item("SYSTEM_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO", Me.NullConvertString(row.Item("UKETSUKE_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO_EDA", Me.NullConvertString(row.Item("UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMPANY_CD", Me.NullConvertString(row.Item("COMPANY_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BASHO_CD", Me.NullConvertString(row.Item("BASHO_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_BUMON", Me.NullConvertString(row.Item("INKA_BUMON")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_GROUP", Me.NullConvertString(row.Item("INKA_GROUP")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INPUT_YMD", Me.NullConvertString(row.Item("INPUT_YMD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_YMD", Me.NullConvertString(row.Item("INKA_YMD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INPUT_KB", Me.NullConvertString(row.Item("INPUT_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_RYAKU", Me.NullConvertString(row.Item("GOODS_RYAKU")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GRADE_1", Me.NullConvertString(row.Item("GRADE_1")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GRADE_2", Me.NullConvertString(row.Item("GRADE_2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YORYO", Me.NullConvertString(row.Item("YORYO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NISUGATA_CD", Me.NullConvertString(row.Item("NISUGATA_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSHUTSU_KB", Me.NullConvertString(row.Item("YUSHUTSU_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", Me.NullConvertString(row.Item("ZAIKO_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_BASHO_SP", Me.NullConvertString(row.Item("INKA_BASHO_SP")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@URI_BASHO", Me.NullConvertString(row.Item("URI_BASHO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@URI_BUMON", Me.NullConvertString(row.Item("URI_BUMON")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@URI_GROUP", Me.NullConvertString(row.Item("URI_GROUP")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SENPO_KENSHU_SURYO", Me.NullConvertZero(row.Item("SENPO_KENSHU_SURYO")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_1", Me.NullConvertString(row.Item("LOT_NO_1")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO2_1", Me.NullConvertString(row.Item("LOT_NO2_1")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_1", Me.NullConvertZero(row.Item("KOSU_1")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURYO_1", Me.NullConvertZero(row.Item("SURYO_1")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_2", Me.NullConvertString(row.Item("LOT_NO_2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO2_2", Me.NullConvertString(row.Item("LOT_NO2_2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_2", Me.NullConvertZero(row.Item("KOSU_2")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURYO_2", Me.NullConvertZero(row.Item("SURYO_2")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_3", Me.NullConvertString(row.Item("LOT_NO_3")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO2_3", Me.NullConvertString(row.Item("LOT_NO2_3")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_3", Me.NullConvertZero(row.Item("KOSU_3")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURYO_3", Me.NullConvertZero(row.Item("SURYO_3")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_4", Me.NullConvertString(row.Item("LOT_NO_4")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO2_4", Me.NullConvertString(row.Item("LOT_NO2_4")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_4", Me.NullConvertZero(row.Item("KOSU_4")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURYO_4", Me.NullConvertZero(row.Item("SURYO_4")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_5", Me.NullConvertString(row.Item("LOT_NO_5")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO2_5", Me.NullConvertString(row.Item("LOT_NO2_5")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOSU_5", Me.NullConvertZero(row.Item("KOSU_5")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURYO_5", Me.NullConvertZero(row.Item("SURYO_5")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TTL_KOSU", Me.NullConvertZero(row.Item("TTL_KOSU")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TTL_SURYO", Me.NullConvertZero(row.Item("TTL_SURYO")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_BIKO_ANK", Me.NullConvertString(row.Item("IN_BIKO_ANK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IN_BIKO_BIKO", Me.NullConvertString(row.Item("IN_BIKO_BIKO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_BIKO_ANK", Me.NullConvertString(row.Item("OUT_BIKO_ANK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_BIKO_BIKO", Me.NullConvertString(row.Item("OUT_BIKO_BIKO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHORI_NO", Me.NullConvertString(row.Item("SHORI_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHORI_NO_EDA", Me.NullConvertString(row.Item("SHORI_NO_EDA")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERROR_FLG", Me.NullConvertString(row.Item("ERROR_FLG")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KO_UKETSUKE_NO", Me.NullConvertString(row.Item("KO_UKETSUKE_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KO_UKETSUKE_NO_EDA", Me.NullConvertString(row.Item("KO_UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GEKKAN_KEIYAKU_NO", Me.NullConvertString(row.Item("GEKKAN_KEIYAKU_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_1", Me.NullConvertString(row.Item("KOBETSU_NISUGATA_CD_1")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENTEI_KB_1", Me.NullConvertString(row.Item("KENTEI_KB_1")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_11", Me.NullConvertString(row.Item("HOKAN_ICHI_11")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_11", Me.NullConvertZero(row.Item("HOKAN_KOSU_11")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_11", Me.NullConvertZero(row.Item("HOKAN_SURYO_11")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_12", Me.NullConvertString(row.Item("HOKAN_ICHI_12")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_12", Me.NullConvertZero(row.Item("HOKAN_KOSU_12")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_12", Me.NullConvertZero(row.Item("HOKAN_SURYO_12")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_13", Me.NullConvertString(row.Item("HOKAN_ICHI_13")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_13", Me.NullConvertZero(row.Item("HOKAN_KOSU_13")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_13", Me.NullConvertZero(row.Item("HOKAN_SURYO_13")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_14", Me.NullConvertString(row.Item("HOKAN_ICHI_14")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_14", Me.NullConvertZero(row.Item("HOKAN_KOSU_14")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_14", Me.NullConvertZero(row.Item("HOKAN_SURYO_14")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_2", Me.NullConvertString(row.Item("KOBETSU_NISUGATA_CD_2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENTEI_KB_2", Me.NullConvertString(row.Item("KENTEI_KB_2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_21", Me.NullConvertString(row.Item("HOKAN_ICHI_21")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_21", Me.NullConvertZero(row.Item("HOKAN_KOSU_21")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_21", Me.NullConvertZero(row.Item("HOKAN_SURYO_21")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_22", Me.NullConvertString(row.Item("HOKAN_ICHI_22")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_22", Me.NullConvertZero(row.Item("HOKAN_KOSU_22")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_22", Me.NullConvertZero(row.Item("HOKAN_SURYO_22")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_23", Me.NullConvertString(row.Item("HOKAN_ICHI_23")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_23", Me.NullConvertZero(row.Item("HOKAN_KOSU_23")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_23", Me.NullConvertZero(row.Item("HOKAN_SURYO_23")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_24", Me.NullConvertString(row.Item("HOKAN_ICHI_24")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_24", Me.NullConvertZero(row.Item("HOKAN_KOSU_24")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_24", Me.NullConvertZero(row.Item("HOKAN_SURYO_24")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_3", Me.NullConvertString(row.Item("KOBETSU_NISUGATA_CD_3")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENTEI_KB_3", Me.NullConvertString(row.Item("KENTEI_KB_3")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_31", Me.NullConvertString(row.Item("HOKAN_ICHI_31")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_31", Me.NullConvertZero(row.Item("HOKAN_KOSU_31")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_31", Me.NullConvertZero(row.Item("HOKAN_SURYO_31")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_32", Me.NullConvertString(row.Item("HOKAN_ICHI_32")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_32", Me.NullConvertZero(row.Item("HOKAN_KOSU_32")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_32", Me.NullConvertZero(row.Item("HOKAN_SURYO_32")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_33", Me.NullConvertString(row.Item("HOKAN_ICHI_33")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_33", Me.NullConvertZero(row.Item("HOKAN_KOSU_33")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_33", Me.NullConvertZero(row.Item("HOKAN_SURYO_33")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_34", Me.NullConvertString(row.Item("HOKAN_ICHI_34")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_34", Me.NullConvertZero(row.Item("HOKAN_KOSU_34")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_34", Me.NullConvertZero(row.Item("HOKAN_SURYO_34")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_4", Me.NullConvertString(row.Item("KOBETSU_NISUGATA_CD_4")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENTEI_KB_4", Me.NullConvertString(row.Item("KENTEI_KB_4")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_41", Me.NullConvertString(row.Item("HOKAN_ICHI_41")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_41", Me.NullConvertZero(row.Item("HOKAN_KOSU_41")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_41", Me.NullConvertZero(row.Item("HOKAN_SURYO_41")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_42", Me.NullConvertString(row.Item("HOKAN_ICHI_42")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_42", Me.NullConvertZero(row.Item("HOKAN_KOSU_42")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_42", Me.NullConvertZero(row.Item("HOKAN_SURYO_42")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_43", Me.NullConvertString(row.Item("HOKAN_ICHI_43")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_43", Me.NullConvertZero(row.Item("HOKAN_KOSU_43")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_43", Me.NullConvertZero(row.Item("HOKAN_SURYO_43")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_44", Me.NullConvertString(row.Item("HOKAN_ICHI_44")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_44", Me.NullConvertZero(row.Item("HOKAN_KOSU_44")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_44", Me.NullConvertZero(row.Item("HOKAN_SURYO_44")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_5", Me.NullConvertString(row.Item("KOBETSU_NISUGATA_CD_5")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KENTEI_KB_5", Me.NullConvertString(row.Item("KENTEI_KB_5")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_51", Me.NullConvertString(row.Item("HOKAN_ICHI_51")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_51", Me.NullConvertZero(row.Item("HOKAN_KOSU_51")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_51", Me.NullConvertZero(row.Item("HOKAN_SURYO_51")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_52", Me.NullConvertString(row.Item("HOKAN_ICHI_52")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_52", Me.NullConvertZero(row.Item("HOKAN_KOSU_52")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_52", Me.NullConvertZero(row.Item("HOKAN_SURYO_52")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_53", Me.NullConvertString(row.Item("HOKAN_ICHI_53")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_53", Me.NullConvertZero(row.Item("HOKAN_KOSU_53")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_53", Me.NullConvertZero(row.Item("HOKAN_SURYO_53")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ICHI_54", Me.NullConvertString(row.Item("HOKAN_ICHI_54")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_KOSU_54", Me.NullConvertZero(row.Item("HOKAN_KOSU_54")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_SURYO_54", Me.NullConvertZero(row.Item("HOKAN_SURYO_54")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_SOKUSHORI_KB", Me.NullConvertString(row.Item("INKA_SOKUSHORI_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GENKA_BUMON", Me.NullConvertString(row.Item("GENKA_BUMON")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(row.Item("BIN_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YUSO_COMP_CD", Me.NullConvertString(row.Item("YUSO_COMP_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JUST_OUTKA_KB", Me.NullConvertString(row.Item("JUST_OUTKA_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUGENRYO_KB", Me.NullConvertString(row.Item("SHUGENRYO_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GEKKAN_KB", Me.NullConvertString(row.Item("GEKKAN_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI", Me.NullConvertString(row.Item("YOBI")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOBI2", Me.NullConvertString(row.Item("YOBI2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERROR_MSG_1", Me.NullConvertString(row.Item("ERROR_MSG_1")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERROR_MSG_2", Me.NullConvertString(row.Item("ERROR_MSG_2")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERROR_MSG_3", Me.NullConvertString(row.Item("ERROR_MSG_3")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERROR_MSG_4", Me.NullConvertString(row.Item("ERROR_MSG_4")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERROR_MSG_5", Me.NullConvertString(row.Item("ERROR_MSG_5")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(row.Item("RECORD_STATUS")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(row.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERR_RCV_USER", String.Empty))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERR_RCV_DATE", String.Empty))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ERR_RCV_TIME", String.Empty))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

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
            'me._sqlprmlist.add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
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
            '▼▼▼要望番号:602
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(.Item("TAX_KB")), DBDataType.CHAR))
            '▲▲▲要望番号:602
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", Me.NullConvertString(.Item("SEIQ_TARIFF_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", Me.NullConvertString(.Item("SEIQ_ETARIFF_CD")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.NVARCHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "オーダー№チェックパラメータ設定"
    Private Sub SetOrderChkPrm(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO", Me.NullConvertString(dt.Rows(0).Item("UKETSUKE_NO")), DBDataType.NVARCHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO_EDA", Me.NullConvertString(dt.Rows(0).Item("UKETSUKE_NO_EDA")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO_EDA", Me.NullConvertString(dt.Rows(0).Item("UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))

    End Sub

#End Region


#Region "OUTKA_L"
    Private Sub SetOutkaLParameter()

        Me.SetDataInsertParameter()
        Me.SetSysdataParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(Me._Row.Item("OUTKA_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(Me._Row.Item("FURI_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(Me._Row.Item("OUTKA_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(Me._Row.Item("SYUBETU_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(Me._Row.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(Me._Row.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(Me._Row.Item("PICK_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(Me._Row.Item("DENP_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", Me.NullConvertString(Me._Row.Item("ARR_KANRYO_INFO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(Me._Row.Item("WH_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(Me._Row.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(Me._Row.Item("OUTKO_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(Me._Row.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(Me._Row.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(Me._Row.Item("HOKOKU_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(Me._Row.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@END_DATE", Me.NullConvertString(Me._Row.Item("END_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(Me._Row.Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(Me._Row.Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(Me._Row.Item("SHIP_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(Me._Row.Item("SHIP_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(Me._Row.Item("DEST_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(Me._Row.Item("DEST_AD_3")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(Me._Row.Item("DEST_TEL")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", Me.NullConvertString(Me._Row.Item("NHS_REMARK")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(Me._Row.Item("SP_NHS_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(Me._Row.Item("COA_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(Me._Row.Item("CUST_ORD_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(Me._Row.Item("BUYER_ORD_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(Me._Row.Item("REMARK")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.NullConvertString(Me._Row.Item("OUTKA_PKG_NB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(Me._Row.Item("DENP_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(Me._Row.Item("PC_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(Me._Row.Item("NIYAKU_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_KB", Me.NullConvertString(Me._Row.Item("DEST_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(Me._Row.Item("DEST_NM")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(Me._Row.Item("AD_1")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(Me._Row.Item("AD_2")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", Me.NullConvertString(Me._Row.Item("ALL_PRINT_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", Me.NullConvertString(Me._Row.Item("NIHUDA_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", Me.NullConvertString(Me._Row.Item("NHS_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", Me.NullConvertString(Me._Row.Item("DENP_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me.NullConvertString(Me._Row.Item("COA_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", Me.NullConvertString(Me._Row.Item("HOKOKU_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", Me.NullConvertString(Me._Row.Item("MATOME_PICK_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_PRINT_DATE", Me.NullConvertString(Me._Row.Item("MATOME_PRINT_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_PRINT_TIME", Me.NullConvertString(Me._Row.Item("MATOME_PRINT_TIME")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", Me.NullConvertString(Me._Row.Item("LAST_PRINT_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", Me.NullConvertString(Me._Row.Item("LAST_PRINT_TIME")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SASZ_USER", Me.NullConvertString(Me._Row.Item("SASZ_USER")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", Me.NullConvertString(Me._Row.Item("OUTKO_USER")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_USER", Me.NullConvertString(Me._Row.Item("KEN_USER")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", Me.NullConvertString(Me._Row.Item("OUTKA_USER")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOU_USER", Me.NullConvertString(Me._Row.Item("HOU_USER")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", Me.NullConvertString(Me._Row.Item("ORDER_TYPE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_KENPIN_WK_STATUS", Me.NullConvertString(Me._Row.Item("ORDER_TYPE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

#End Region

#Region "OUTKA_M"
    Private Sub SetOutkaMParameter()

        Me.SetDataInsertParameter()
        Me.SetSysdataParameter()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(Me._Row.Item("OUTKA_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me.NullConvertString(Me._Row.Item("OUTKA_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", Me.NullConvertString(Me._Row.Item("EDI_SET_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(Me._Row.Item("COA_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", Me.NullConvertString(Me._Row.Item("CUST_ORD_NO_DTL")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", Me.NullConvertString(Me._Row.Item("BUYER_ORD_NO_DTL")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me.NullConvertString(Me._Row.Item("GOODS_CD_NRS")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", Me.NullConvertString(Me._Row.Item("RSV_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(Me._Row.Item("LOT_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(Me._Row.Item("SERIAL_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", Me.NullConvertString(Me._Row.Item("ALCTD_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.NullConvertString(Me._Row.Item("OUTKA_PKG_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.NullConvertString(Me._Row.Item("OUTKA_HASU")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.NullConvertString(Me._Row.Item("OUTKA_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.NullConvertString(Me._Row.Item("OUTKA_TTL_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.NullConvertString(Me._Row.Item("OUTKA_TTL_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.NullConvertString(Me._Row.Item("ALCTD_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.NullConvertString(Me._Row.Item("ALCTD_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.NullConvertString(Me._Row.Item("BACKLOG_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.NullConvertString(Me._Row.Item("BACKLOG_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(Me._Row.Item("UNSO_ONDO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertString(Me._Row.Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(Me._Row.Item("IRIME_UT")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.NullConvertString(Me._Row.Item("OUTKA_M_PKG_NB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(Me._Row.Item("REMARK")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", Me.NullConvertString(Me._Row.Item("SIZE_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", Me.NullConvertString(Me._Row.Item("ZAIKO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", Me.NullConvertString(Me._Row.Item("SOURCE_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", Me.NullConvertString(Me._Row.Item("YELLOW_CARD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", Me.NullConvertString(Me._Row.Item("GOODS_CD_NRS_FROM")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.NullConvertString(Me._Row.Item("PRINT_SORT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub
#End Region

#Region "OUTKA_S"
    Private Sub SetOutkaSParameter()

        Me.SetDataInsertParameter()
        Me.SetSysdataParameter()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(Me._Row.Item("OUTKA_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me.NullConvertString(Me._Row.Item("OUTKA_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", Me.NullConvertString(Me._Row.Item("OUTKA_NO_S")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", Me.NullConvertString(Me._Row.Item("TOU_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", Me.NullConvertString(Me._Row.Item("SITU_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", Me.NullConvertString(Me._Row.Item("ZONE_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", Me.NullConvertString(Me._Row.Item("LOCA")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(Me._Row.Item("LOT_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(Me._Row.Item("SERIAL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.NullConvertString(Me._Row.Item("OUTKA_TTL_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.NullConvertString(Me._Row.Item("OUTKA_TTL_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me.NullConvertString(Me._Row.Item("ZAI_REC_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me.NullConvertString(Me._Row.Item("INKA_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", Me.NullConvertString(Me._Row.Item("INKA_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", Me.NullConvertString(Me._Row.Item("INKA_NO_S")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_UPD_FLAG", Me.NullConvertString(Me._Row.Item("ZAI_UPD_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_NB", Me.NullConvertString(Me._Row.Item("ALCTD_CAN_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.NullConvertString(Me._Row.Item("ALCTD_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_QT", Me.NullConvertString(Me._Row.Item("ALCTD_CAN_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.NullConvertString(Me._Row.Item("ALCTD_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertString(Me._Row.Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.NullConvertString(Me._Row.Item("BETU_WT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me.NullConvertString(Me._Row.Item("COA_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(Me._Row.Item("REMARK")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", Me.NullConvertString(Me._Row.Item("SMPL_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(Me._Row.Item("REC_NO")), DBDataType.CHAR))



        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))


    End Sub
#End Region

#Region "D_ZAI_TRS"



    Private Sub SetUpdZaiTrsParameter()

        Me.SetSysdataParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me.NullConvertString(Me._Row.Item("ZAI_REC_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_DATE", Me.NullConvertString(Me._Row.Item("SYS_UPD_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_UPD_TIME", Me.NullConvertString(Me._Row.Item("SYS_UPD_TIME")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))


    End Sub

#End Region

#Region "セミEDI処理"

#Region "M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)"

    ''' <summary>
    ''' M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectChkGoodsParameter(ByVal semiInfoDr As DataRow, ByVal dtRcvDtl As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(semiInfoDr.Item("NRS_BR_CD")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", dtRcvDtl.Rows(0).Item(GetColumnName(MclcArrivalColumns.ITEM_RYAKUGO)).ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(semiInfoDr.Item("CUST_CD_L")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(semiInfoDr.Item("CUST_CD_M")), DBDataType.VARCHAR))

    End Sub

#End Region ' "M_GOODS 件数取得(セミEDI：荷主商品コードより抽出)"

#Region "カラム名取得"

    ''' <summary>
    ''' カラム名取得
    ''' </summary>
    ''' <param name="column"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetColumnName(column As MclcArrivalColumns) As String

        Return String.Format("{0}{1}", COLUMN_NAME_PREFIX, Convert.ToInt32(column))

    End Function

#End Region ' "カラム名取得"

#Region "SQL パラメータ設定 EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT 用"

    ''' <summary>
    ''' SQL パラメータ設定 EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT 用
    ''' </summary>
    ''' <param name="dt"></param>
    Private Sub SetSqlParamSelectInkaCntAndNoL(ByVal dt As DataTable)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HACCHU_DENP_NO", Me.NullConvertString(dt.Rows(0).Item("HACCHU_DENP_NO")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HACCHU_DENP_DTL_NO", Me.NullConvertString(dt.Rows(0).Item("HACCHU_DENP_DTL_NO")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IO_DENP_NO", Me.NullConvertString(dt.Rows(0).Item("IO_DENP_NO")), DBDataType.VARCHAR))
    End Sub

#End Region ' "SQL パラメータ設定 EDI入荷データ件数および入荷データL 入荷管理番号L 等 SELECT 用"

#Region "SQL パラメータ設定 EDI入荷(大・中共通) / EDI受信データ(DTL) とも兼用 テーブル更新(論理削除) 用"

    ''' <summary>
    ''' SQL パラメータ設定 EDI入荷(大・中共通) / EDI受信データ(DTL) とも兼用 テーブル更新(論理削除) 用
    ''' </summary>
    ''' <param name="dt"></param>
    Private Sub SetSqlParamUpdateDelInkaEdiL_M_Dtl(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(MyBase.GetSystemTime())))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
    End Sub

#End Region '  "SQL パラメータ設定 EDI入荷(大・中共通) / EDI受信データ(DTL) とも兼用 テーブル更新(論理削除) 用"

#Region "SQL パラメータ設定 EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 用"

    ''' <summary>
    ''' SQL パラメータ設定 EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 用
    ''' </summary>
    ''' <param name="dt"></param>
    Private Sub SetSqlParamSelectInkaEdiHacchuDenpNoAndDtlNoAndIoDenpNo(ByVal dt As DataTable)

        With dt.Rows(0)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(.Item("GYO")), DBDataType.CHAR))
        End With

    End Sub

#End Region ' "SQL パラメータ設定 EDI受信(DTL)テーブル データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 用"

#Region "SQL パラメータ設定 EDI受信(DTL)テーブル 物理削除 用"

    ''' <summary>
    ''' SQL パラメータ設定 EDI受信(DTL)テーブル 物理削除 用
    ''' </summary>
    ''' <param name="dt"></param>
    Private Sub SetSqlParamDeleteInkaEdi(ByVal dt As DataTable)

        ' データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得 用パラメータ設定と兼用する。
        Call SetSqlParamSelectInkaEdiHacchuDenpNoAndDtlNoAndIoDenpNo(dt)

    End Sub

#End Region ' "SQL パラメータ設定 EDI受信(DTL)テーブル 物理削除 用"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出) パラメータ設定"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出) パラメータ設定
    ''' </summary>
    ''' <param name="semiInfoDr"></param>
    ''' <param name="drEdiRcvDtl"></param>
    Private Sub SetSelectInkaediDtlNcgoNewCancelParameter(ByVal semiInfoDr As DataRow, ByVal drEdiRcvDtl As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(semiInfoDr.Item("NRS_BR_CD")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(semiInfoDr.Item("CUST_CD_L")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(semiInfoDr.Item("CUST_CD_M")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.NullConvertString(drEdiRcvDtl.Item("SYS_ENT_DATE")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.NullConvertString(drEdiRcvDtl.Item("SYS_ENT_TIME")), DBDataType.VARCHAR))

    End Sub

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・入荷赤伝・取消抽出) パラメータ設定"

#Region "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大)(中) 登録用) パラメータ設定"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大)(中) 登録用) パラメータ設定
    ''' </summary>
    ''' <param name="semiInfoDr"></param>
    ''' <param name="drEdiRcvDtl"></param>
    Private Sub SetSelectForInkaediL_M_FromInkaediDtlNcgoNewParameter(ByVal semiInfoDr As DataRow, ByVal drEdiRcvDtl As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(semiInfoDr.Item("NRS_BR_CD")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(semiInfoDr.Item("WH_CD")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(semiInfoDr.Item("CUST_CD_L")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(semiInfoDr.Item("CUST_CD_M")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.NullConvertString(drEdiRcvDtl.Item("SYS_ENT_DATE")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.NullConvertString(drEdiRcvDtl.Item("SYS_ENT_TIME")), DBDataType.VARCHAR))

    End Sub

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 取得 (セミEDI時・EDI入荷(大)(中) 登録用) パラメータ設定"

#Region "EDI受信(DTL)新規追加パラメータ設定"

    ''' <summary>
    ''' EDI受信(DTL)新規追加パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertInkaEdiRcvDtlParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            'EDI受信（DTL）共通項目
            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", Me.NullConvertString(.Item("GYO")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(.Item("INKA_CTL_NO_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(.Item("INKA_CTL_NO_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.VARCHAR))

            'EDI受信（DTL）荷主個別項目
            prmList.Add(MyBase.GetSqlParameter("@DATA_ID_AREA", Me.NullConvertString(.Item("DATA_ID_AREA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_ID_DETAIL", Me.NullConvertString(.Item("DATA_ID_DETAIL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENSOUSAKI", Me.NullConvertString(.Item("DENSOUSAKI")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_H_Y_KBN", Me.NullConvertString(.Item("INKA_H_Y_KBN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANNOU_KBN", Me.NullConvertString(.Item("KANNOU_KBN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INPUT_KBN", Me.NullConvertString(.Item("INPUT_KBN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKADEN_KBN", Me.NullConvertString(.Item("AKADEN_KBN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_CRE_DATE", Me.NullConvertString(.Item("DATA_CRE_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_CRE_TIME", Me.NullConvertString(.Item("DATA_CRE_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COMP_CD", Me.NullConvertString(.Item("COMP_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRAISYA_NM", Me.NullConvertString(.Item("IRAISYA_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENPYO_TYPE", Me.NullConvertString(.Item("DENPYO_TYPE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HACCHU_DENP_NO", Me.NullConvertString(.Item("HACCHU_DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HACCHU_DENP_DTL_NO", Me.NullConvertString(.Item("HACCHU_DENP_DTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_DENP_NO", Me.NullConvertString(.Item("OUTKA_DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_DENP_DTL_NO", Me.NullConvertString(.Item("OUTKA_DENP_DTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IO_DENP_NO", Me.NullConvertString(.Item("IO_DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IO_DENP_DTL_NO", Me.NullConvertString(.Item("IO_DENP_DTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KISYA_HOUKOKU_KNARI_NO", Me.NullConvertString(.Item("KISYA_HOUKOKU_KNARI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INPUT_DATE", Me.NullConvertString(.Item("INPUT_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIRI_DATE", Me.NullConvertString(.Item("SEIRI_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKA_DATE", Me.NullConvertString(.Item("SYUKKA_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NOUKI_DATE", Me.NullConvertString(.Item("NOUKI_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ITEM_CD", Me.NullConvertString(.Item("ITEM_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ITEM_RYAKUGO", Me.NullConvertString(.Item("ITEM_RYAKUGO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ITEM_AISYO", Me.NullConvertString(.Item("ITEM_AISYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ITEM_GROUP", Me.NullConvertString(.Item("ITEM_GROUP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIZO_KBN", Me.NullConvertString(.Item("SEIZO_KBN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GRADE1", Me.NullConvertString(.Item("GRADE1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GRADE2", Me.NullConvertString(.Item("GRADE2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DAIHYO_NISUGATA_CD", Me.NullConvertString(.Item("DAIHYO_NISUGATA_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NISUGATA_CD", Me.NullConvertString(.Item("NISUGATA_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DAIHYO_NISUGATA_NM", Me.NullConvertString(.Item("DAIHYO_NISUGATA_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YOURYOU", Me.NullConvertString(.Item("YOURYOU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIZO_LOT", Me.NullConvertString(.Item("SEIZO_LOT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KANRI_LOT_NO", Me.NullConvertString(.Item("ZAIKO_KANRI_LOT_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU_FUGO", Me.NullConvertString(.Item("KOSU_FUGO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU", Me.NullConvertZero(.Item("KOSU")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SUURYO_FUGO", Me.NullConvertString(.Item("SUURYO_FUGO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SUURYO", Me.NullConvertZero(.Item("SUURYO")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLANT", Me.NullConvertString(.Item("INKA_PLANT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_HOKAN_BASYO", Me.NullConvertString(.Item("INKA_HOKAN_BASYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NOUNYUSAKI_BASYO_CD", Me.NullConvertString(.Item("NOUNYUSAKI_BASYO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLANT", Me.NullConvertString(.Item("OUTKA_PLANT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_IN_POINT", Me.NullConvertString(.Item("OUT_IN_POINT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HOKAN_BASYO", Me.NullConvertString(.Item("OUTKA_HOKAN_BASYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_TOKUISAKI_CD", Me.NullConvertString(.Item("SYUKKAMOTO_TOKUISAKI_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_NM1", Me.NullConvertString(.Item("SYUKKAMOTO_NM1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_NM2", Me.NullConvertString(.Item("SYUKKAMOTO_NM2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_NM4", Me.NullConvertString(.Item("SYUKKAMOTO_NM4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_NM5", Me.NullConvertString(.Item("SYUKKAMOTO_NM5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_SYOZAICHI", Me.NullConvertString(.Item("SYUKKAMOTO_SYOZAICHI")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_BASYO_CD", Me.NullConvertString(.Item("SYUKKAMOTO_BASYO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_TEL", Me.NullConvertString(.Item("SYUKKAMOTO_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKAMOTO_ZIP", Me.NullConvertString(.Item("SYUKKAMOTO_ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKA_TYPE", Me.NullConvertString(.Item("SYUKKA_TYPE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKKA_TYPE_TXT", Me.NullConvertString(.Item("SYUKKA_TYPE_TXT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_SYUDAN", Me.NullConvertString(.Item("YUSO_SYUDAN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_SYUDAN_NM", Me.NullConvertString(.Item("YUSO_SYUDAN_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSYUTSU_KBN", Me.NullConvertString(.Item("YUSYUTSU_KBN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUSHI_NO", Me.NullConvertZero(.Item("TOUSHI_NO")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CONT_NO", Me.NullConvertString(.Item("CONT_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAISEN_PLAN_NO", Me.NullConvertString(.Item("HAISEN_PLAN_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RIEKI_SENTA", Me.NullConvertString(.Item("RIEKI_SENTA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GENKA_SENTA", Me.NullConvertString(.Item("GENKA_SENTA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HENPIN_RIYU", Me.NullConvertString(.Item("HENPIN_RIYU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NOUNYU_JYOUKEN_BIKOU", Me.NullConvertString(.Item("NOUNYU_JYOUKEN_BIKOU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UCHI_BIKOU", Me.NullConvertString(.Item("UCHI_BIKOU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOTO_BIKOU", Me.NullConvertString(.Item("SOTO_BIKOU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RENBAN", Me.NullConvertZero(.Item("RENBAN")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ITEM_TXT", Me.NullConvertString(.Item("ITEM_TXT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIGYOUBU", Me.NullConvertString(.Item("JIGYOUBU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SANSYO_CD", Me.NullConvertString(.Item("SANSYO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NOUNYU_JIKOKU_NM", Me.NullConvertString(.Item("NOUNYU_JIKOKU_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TANK_NO", Me.NullConvertString(.Item("TANK_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UG", Me.NullConvertString(.Item("UG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KIHON_SURYO_TANI", Me.NullConvertString(.Item("KIHON_SURYO_TANI")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JURYO_KANSAN_KEISU", Me.NullConvertZero(.Item("JURYO_KANSAN_KEISU")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NOUNYU_JIKOKU_CD", Me.NullConvertString(.Item("NOUNYU_JIKOKU_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YOBI", Me.NullConvertString(.Item("YOBI")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_CATEGORY1", Me.NullConvertString(.Item("ERR_CATEGORY1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_CATEGORY1_NM", Me.NullConvertString(.Item("ERR_CATEGORY1_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN1_1", Me.NullConvertString(.Item("ERR_KOUBAN1_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN1_2", Me.NullConvertString(.Item("ERR_KOUBAN1_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN1_3", Me.NullConvertString(.Item("ERR_KOUBAN1_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN1_4", Me.NullConvertString(.Item("ERR_KOUBAN1_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN1_5", Me.NullConvertString(.Item("ERR_KOUBAN1_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_CATEGORY2", Me.NullConvertString(.Item("ERR_CATEGORY2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_CATEGORY2_NM", Me.NullConvertString(.Item("ERR_CATEGORY2_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN2_1", Me.NullConvertString(.Item("ERR_KOUBAN2_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN2_2", Me.NullConvertString(.Item("ERR_KOUBAN2_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN2_3", Me.NullConvertString(.Item("ERR_KOUBAN2_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN2_4", Me.NullConvertString(.Item("ERR_KOUBAN2_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN2_5", Me.NullConvertString(.Item("ERR_KOUBAN2_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_CATEGORY3", Me.NullConvertString(.Item("ERR_CATEGORY3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_CATEGORY3_NM", Me.NullConvertString(.Item("ERR_CATEGORY3_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN3_1", Me.NullConvertString(.Item("ERR_KOUBAN3_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN3_2", Me.NullConvertString(.Item("ERR_KOUBAN3_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN3_3", Me.NullConvertString(.Item("ERR_KOUBAN3_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN3_4", Me.NullConvertString(.Item("ERR_KOUBAN3_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_KOUBAN3_5", Me.NullConvertString(.Item("ERR_KOUBAN3_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_YOBI", Me.NullConvertString(.Item("ERR_YOBI")), DBDataType.NVARCHAR))

            'EDI受信（DTL）共通項目
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", "", DBDataType.VARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", "", DBDataType.VARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", "", DBDataType.VARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@DELETE_USER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", "", DBDataType.VARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))

            'システム管理用項目
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", .Item("SYS_ENT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", .Item("SYS_ENT_TIME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", .Item("SYS_ENT_PGID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", .Item("SYS_ENT_USER").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", .Item("SYS_UPD_PGID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", .Item("SYS_UPD_USER").ToString(), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.VARCHAR))

        End With

    End Sub

#End Region ' "EDI受信(DTL)新規追加パラメータ設定"

#Region "H_INKAEDI_DTL_NCGO_NEW 更新 (セミEDI時・入荷赤伝・取消・論理削除) パラメータ設定"

    ''' <summary>
    ''' H_INKAEDI_DTL_NCGO_NEW 更新 (セミEDI時・入荷赤伝・取消・論理削除) パラメータ設定
    ''' </summary>
    ''' <param name="semiInfoDr"></param>
    ''' <param name="dtInkaediDtlNcgoNew"></param>
    Private Sub SetUpdateInkaediDtlNcgoNewCancelParameter(ByVal semiInfoDr As DataRow, ByVal dtInkaediDtlNcgoNew As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", Me.GetColonEditTime(MyBase.GetSystemTime()), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", String.Concat(semiInfoDr.Item("BR_INITIAL").ToString(), "00000000"), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", "000", DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(MyBase.GetSystemTime()), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(semiInfoDr.Item("NRS_BR_CD")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(semiInfoDr.Item("CUST_CD_L")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(semiInfoDr.Item("CUST_CD_M")), DBDataType.VARCHAR))

        Dim j As Integer = 0
        For i As Integer = 0 To dtInkaediDtlNcgoNew.Rows().Count() - 1
            j += 1
            Dim ediCtlNo As String = dtInkaediDtlNcgoNew.Rows(i).Item("EDI_CTL_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@EDI_CTL_NO_", j.ToString()), ediCtlNo, DBDataType.VARCHAR))
        Next

    End Sub

#End Region ' "H_INKAEDI_DTL_NCGO_NEW 更新 (セミEDI時・入荷赤伝・取消・論理削除) パラメータ設定"

#Region "H_INKAEDI_L, H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除) パラメータ設定"

    ''' <summary>
    ''' H_INKAEDI_L, H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除) パラメータ設定
    ''' </summary>
    ''' <param name="semiInfoDr"></param>
    ''' <param name="dtInkaediDtlNcgoNew"></param>
    Private Sub SetUpdateInkaediL_M_CancelParameter(ByVal semiInfoDr As DataRow, ByVal dtInkaediDtlNcgoNew As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(MyBase.GetSystemTime()), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(semiInfoDr.Item("NRS_BR_CD")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(semiInfoDr.Item("CUST_CD_L")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(semiInfoDr.Item("CUST_CD_M")), DBDataType.VARCHAR))

        Dim j As Integer = 0
        For i As Integer = 0 To dtInkaediDtlNcgoNew.Rows().Count() - 1
            j += 1
            Dim ediCtlNo As String = dtInkaediDtlNcgoNew.Rows(i).Item("EDI_CTL_NO").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@EDI_CTL_NO_", j.ToString()), ediCtlNo, DBDataType.VARCHAR))
        Next

    End Sub

#End Region ' "H_INKAEDI_L, H_INKAEDI_M 更新 (セミEDI時・入荷赤伝・取消・論理削除) パラメータ設定"

#Region "H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ) パラメータ設定"

    ''' <summary>
    ''' H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ) パラメータ設定
    ''' </summary>
    ''' <param name="drInkaediDtlNcgoNew"></param>
    Private Sub SetUpdateUnsoediDtlNcgo_EdiCtlNoParameter(ByVal drInkaediDtlNcgoNew As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(drInkaediDtlNcgoNew.Item("SYS_UPD_DATE")), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.GetColonEditTime(drInkaediDtlNcgoNew.Item("SYS_UPD_TIME").ToString()), DBDataType.VARCHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.NullConvertString(drInkaediDtlNcgoNew.Item("SYS_UPD_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.NullConvertString(drInkaediDtlNcgoNew.Item("SYS_UPD_TIME")), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

#End Region ' "H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ) パラメータ設定"

#Region "EDI入荷データ(大)新規追加パラメータ設定"

    ''' <summary>
    ''' EDI入荷データ(大)新規追加パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertInEdiL_Parameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(.Item("INKA_CTL_NO_L")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TP", Me.NullConvertString(.Item("INKA_TP")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_KB", Me.NullConvertString(.Item("INKA_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", Me.NullConvertString(.Item("INKA_STATE_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(.Item("INKA_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TIME", Me.NullConvertString(.Item("INKA_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me.NullConvertString(.Item("NRS_WH_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(.Item("CUST_NM_L")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(.Item("CUST_NM_M")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", Me.NullConvertZero(.Item("INKA_PLAN_QT")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", Me.NullConvertString(.Item("INKA_PLAN_QT_UT")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", Me.NullConvertZero(.Item("INKA_TTL_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(.Item("NAIGAI_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(.Item("OUTKA_FROM_ORD_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.NullConvertString(.Item("SEIQTO_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", Me.NullConvertString(.Item("HOKAN_YN")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", Me.NullConvertZero(.Item("HOKAN_FREE_KIKAN")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", Me.NullConvertString(.Item("HOKAN_STR_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(.Item("TAX_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NYUBAN_L", Me.NullConvertString(.Item("NYUBAN_L")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", Me.NullConvertString(.Item("UNCHIN_TP")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", Me.NullConvertString(.Item("UNCHIN_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO", Me.NullConvertString(.Item("OUTKA_MOTO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(.Item("SYARYO_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(.Item("UNSO_ONDO_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN", Me.NullConvertZero(.Item("UNCHIN")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", Me.NullConvertString(.Item("YOKO_TARIFF_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(.Item("OUT_FLAG")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(.Item("JISSEKI_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(.Item("JISSEKI_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(.Item("JISSEKI_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(.Item("FREE_N01")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(.Item("FREE_N02")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(.Item("FREE_N03")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(.Item("FREE_N04")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(.Item("FREE_N05")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(.Item("FREE_N06")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(.Item("FREE_N07")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(.Item("FREE_N08")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(.Item("FREE_N09")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(.Item("FREE_N10")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", Me.NullConvertString(.Item("CRT_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(.Item("EDIT_FLAG")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(.Item("MATCHING_FLAG")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.NullConvertString(.Item("SYS_ENT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.NullConvertString(.Item("SYS_ENT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.NullConvertString(.Item("SYS_ENT_PGID")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.NullConvertString(.Item("SYS_ENT_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.NullConvertString(.Item("SYS_UPD_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.NullConvertString(.Item("SYS_UPD_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.NullConvertString(.Item("SYS_UPD_PGID")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.NullConvertString(.Item("SYS_UPD_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region ' "EDI入荷データ(大)新規追加パラメータ設定"

#Region "EDI入荷データ(中)新規追加パラメータ設定"

    ''' <summary>
    ''' EDI入荷データ(中)新規追加パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetInsertInkaEdiM_Parameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(.Item("INKA_CTL_NO_L")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(.Item("INKA_CTL_NO_M")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(.Item("NRS_GOODS_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(.Item("CUST_GOODS_CD")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(.Item("GOODS_NM")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NB", Me.NullConvertZero(.Item("NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", Me.NullConvertString(.Item("NB_UT")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.NullConvertZero(.Item("PKG_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me.NullConvertString(.Item("PKG_UT")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PKG_NB", Me.NullConvertZero(.Item("INKA_PKG_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.NullConvertZero(.Item("HASU")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@STD_IRIME", Me.NullConvertZero(.Item("STD_IRIME")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", Me.NullConvertString(.Item("STD_IRIME_UT")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.NullConvertZero(.Item("BETU_WT")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CBM", Me.NullConvertZero(.Item("CBM")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", Me.NullConvertString(.Item("ONDO_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(.Item("OUTKA_FROM_ORD_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(.Item("LOT_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(.Item("SERIAL_NO")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(.Item("IRIME")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(.Item("IRIME_UT")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_KB", Me.NullConvertString(.Item("OUT_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(.Item("JISSEKI_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(.Item("JISSEKI_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(.Item("JISSEKI_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(.Item("FREE_N01")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(.Item("FREE_N02")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(.Item("FREE_N03")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(.Item("FREE_N04")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(.Item("FREE_N05")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(.Item("FREE_N06")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(.Item("FREE_N07")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(.Item("FREE_N08")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(.Item("FREE_N09")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(.Item("FREE_N10")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", Me.NullConvertString(.Item("CRT_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.NullConvertString(.Item("SYS_ENT_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.NullConvertString(.Item("SYS_ENT_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.NullConvertString(.Item("SYS_ENT_PGID")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.NullConvertString(.Item("SYS_ENT_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.NullConvertString(.Item("SYS_UPD_DATE")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.NullConvertString(.Item("SYS_UPD_TIME")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.NullConvertString(.Item("SYS_UPD_PGID")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.NullConvertString(.Item("SYS_UPD_USER")), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.VARCHAR))

        End With

    End Sub

#End Region ' "EDI入荷データ(中)新規追加パラメータ設定"

#End Region ' "セミEDI処理"

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
    Private Function SetMapSend() As Hashtable

        Dim map As Hashtable = New Hashtable

        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_EDA", "EDI_CTL_NO_EDA")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("RCV_ID", "RCV_ID")
        map.Add("RCV_UKETSUKE_NO", "RCV_UKETSUKE_NO")
        map.Add("RCV_UKETSUKE_NO_EDA", "RCV_UKETSUKE_NO_EDA")
        map.Add("RCV_INPUT_KB", "RCV_INPUT_KB")
        map.Add("RCV_EDA_UP_FLG", "RCV_EDA_UP_FLG")
        map.Add("ID", "ID")
        map.Add("SYSTEM_KB", "SYSTEM_KB")
        map.Add("UKETSUKE_NO", "UKETSUKE_NO")
        map.Add("UKETSUKE_NO_EDA", "UKETSUKE_NO_EDA")
        map.Add("COMPANY_CD", "COMPANY_CD")
        map.Add("BASHO_CD", "BASHO_CD")
        map.Add("INKA_BUMON", "INKA_BUMON")
        map.Add("INKA_GROUP", "INKA_GROUP")
        map.Add("INPUT_YMD", "INPUT_YMD")
        map.Add("INKA_YMD", "INKA_YMD")
        map.Add("INPUT_KB", "INPUT_KB")
        map.Add("GOODS_RYAKU", "GOODS_RYAKU")
        map.Add("GRADE_1", "GRADE_1")
        map.Add("GRADE_2", "GRADE_2")
        map.Add("YORYO", "YORYO")
        map.Add("NISUGATA_CD", "NISUGATA_CD")
        map.Add("YUSHUTSU_KB", "YUSHUTSU_KB")
        map.Add("ZAIKO_KB", "ZAIKO_KB")
        map.Add("INKA_BASHO_SP", "INKA_BASHO_SP")
        map.Add("URI_BASHO", "URI_BASHO")
        map.Add("URI_BUMON", "URI_BUMON")
        map.Add("URI_GROUP", "URI_GROUP")
        map.Add("SENPO_KENSHU_SURYO", "SENPO_KENSHU_SURYO")
        map.Add("LOT_NO_1", "LOT_NO_1")
        map.Add("LOT_NO2_1", "LOT_NO2_1")
        map.Add("KOSU_1", "KOSU_1")
        map.Add("SURYO_1", "SURYO_1")
        map.Add("LOT_NO_2", "LOT_NO_2")
        map.Add("LOT_NO2_2", "LOT_NO2_2")
        map.Add("KOSU_2", "KOSU_2")
        map.Add("SURYO_2", "SURYO_2")
        map.Add("LOT_NO_3", "LOT_NO_3")
        map.Add("LOT_NO2_3", "LOT_NO2_3")
        map.Add("KOSU_3", "KOSU_3")
        map.Add("SURYO_3", "SURYO_3")
        map.Add("LOT_NO_4", "LOT_NO_4")
        map.Add("LOT_NO2_4", "LOT_NO2_4")
        map.Add("KOSU_4", "KOSU_4")
        map.Add("SURYO_4", "SURYO_4")
        map.Add("LOT_NO_5", "LOT_NO_5")
        map.Add("LOT_NO2_5", "LOT_NO2_5")
        map.Add("KOSU_5", "KOSU_5")
        map.Add("SURYO_5", "SURYO_5")
        map.Add("TTL_KOSU", "TTL_KOSU")
        map.Add("TTL_SURYO", "TTL_SURYO")
        map.Add("IN_BIKO_ANK", "IN_BIKO_ANK")
        map.Add("IN_BIKO_BIKO", "IN_BIKO_BIKO")
        map.Add("OUT_BIKO_ANK", "OUT_BIKO_ANK")
        map.Add("OUT_BIKO_BIKO", "OUT_BIKO_BIKO")
        map.Add("SHORI_NO", "SHORI_NO")
        map.Add("SHORI_NO_EDA", "SHORI_NO_EDA")
        map.Add("ERROR_FLG", "ERROR_FLG")
        map.Add("KO_UKETSUKE_NO", "KO_UKETSUKE_NO")
        map.Add("KO_UKETSUKE_NO_EDA", "KO_UKETSUKE_NO_EDA")
        map.Add("GEKKAN_KEIYAKU_NO", "GEKKAN_KEIYAKU_NO")
        map.Add("KOBETSU_NISUGATA_CD_1", "KOBETSU_NISUGATA_CD_1")
        map.Add("KENTEI_KB_1", "KENTEI_KB_1")
        map.Add("HOKAN_ICHI_11", "HOKAN_ICHI_11")
        map.Add("HOKAN_KOSU_11", "HOKAN_KOSU_11")
        map.Add("HOKAN_SURYO_11", "HOKAN_SURYO_11")
        map.Add("HOKAN_ICHI_12", "HOKAN_ICHI_12")
        map.Add("HOKAN_KOSU_12", "HOKAN_KOSU_12")
        map.Add("HOKAN_SURYO_12", "HOKAN_SURYO_12")
        map.Add("HOKAN_ICHI_13", "HOKAN_ICHI_13")
        map.Add("HOKAN_KOSU_13", "HOKAN_KOSU_13")
        map.Add("HOKAN_SURYO_13", "HOKAN_SURYO_13")
        map.Add("HOKAN_ICHI_14", "HOKAN_ICHI_14")
        map.Add("HOKAN_KOSU_14", "HOKAN_KOSU_14")
        map.Add("HOKAN_SURYO_14", "HOKAN_SURYO_14")
        map.Add("KOBETSU_NISUGATA_CD_2", "KOBETSU_NISUGATA_CD_2")
        map.Add("KENTEI_KB_2", "KENTEI_KB_2")
        map.Add("HOKAN_ICHI_21", "HOKAN_ICHI_21")
        map.Add("HOKAN_KOSU_21", "HOKAN_KOSU_21")
        map.Add("HOKAN_SURYO_21", "HOKAN_SURYO_21")
        map.Add("HOKAN_ICHI_22", "HOKAN_ICHI_22")
        map.Add("HOKAN_KOSU_22", "HOKAN_KOSU_22")
        map.Add("HOKAN_SURYO_22", "HOKAN_SURYO_22")
        map.Add("HOKAN_ICHI_23", "HOKAN_ICHI_23")
        map.Add("HOKAN_KOSU_23", "HOKAN_KOSU_23")
        map.Add("HOKAN_SURYO_23", "HOKAN_SURYO_23")
        map.Add("HOKAN_ICHI_24", "HOKAN_ICHI_24")
        map.Add("HOKAN_KOSU_24", "HOKAN_KOSU_24")
        map.Add("HOKAN_SURYO_24", "HOKAN_SURYO_24")
        map.Add("KOBETSU_NISUGATA_CD_3", "KOBETSU_NISUGATA_CD_3")
        map.Add("KENTEI_KB_3", "KENTEI_KB_3")
        map.Add("HOKAN_ICHI_31", "HOKAN_ICHI_31")
        map.Add("HOKAN_KOSU_31", "HOKAN_KOSU_31")
        map.Add("HOKAN_SURYO_31", "HOKAN_SURYO_31")
        map.Add("HOKAN_ICHI_32", "HOKAN_ICHI_32")
        map.Add("HOKAN_KOSU_32", "HOKAN_KOSU_32")
        map.Add("HOKAN_SURYO_32", "HOKAN_SURYO_32")
        map.Add("HOKAN_ICHI_33", "HOKAN_ICHI_33")
        map.Add("HOKAN_KOSU_33", "HOKAN_KOSU_33")
        map.Add("HOKAN_SURYO_33", "HOKAN_SURYO_33")
        map.Add("HOKAN_ICHI_34", "HOKAN_ICHI_34")
        map.Add("HOKAN_KOSU_34", "HOKAN_KOSU_34")
        map.Add("HOKAN_SURYO_34", "HOKAN_SURYO_34")
        map.Add("KOBETSU_NISUGATA_CD_4", "KOBETSU_NISUGATA_CD_4")
        map.Add("KENTEI_KB_4", "KENTEI_KB_4")
        map.Add("HOKAN_ICHI_41", "HOKAN_ICHI_41")
        map.Add("HOKAN_KOSU_41", "HOKAN_KOSU_41")
        map.Add("HOKAN_SURYO_41", "HOKAN_SURYO_41")
        map.Add("HOKAN_ICHI_42", "HOKAN_ICHI_42")
        map.Add("HOKAN_KOSU_42", "HOKAN_KOSU_42")
        map.Add("HOKAN_SURYO_42", "HOKAN_SURYO_42")
        map.Add("HOKAN_ICHI_43", "HOKAN_ICHI_43")
        map.Add("HOKAN_KOSU_43", "HOKAN_KOSU_43")
        map.Add("HOKAN_SURYO_43", "HOKAN_SURYO_43")
        map.Add("HOKAN_ICHI_44", "HOKAN_ICHI_44")
        map.Add("HOKAN_KOSU_44", "HOKAN_KOSU_44")
        map.Add("HOKAN_SURYO_44", "HOKAN_SURYO_44")
        map.Add("KOBETSU_NISUGATA_CD_5", "KOBETSU_NISUGATA_CD_5")
        map.Add("KENTEI_KB_5", "KENTEI_KB_5")
        map.Add("HOKAN_ICHI_51", "HOKAN_ICHI_51")
        map.Add("HOKAN_KOSU_51", "HOKAN_KOSU_51")
        map.Add("HOKAN_SURYO_51", "HOKAN_SURYO_51")
        map.Add("HOKAN_ICHI_52", "HOKAN_ICHI_52")
        map.Add("HOKAN_KOSU_52", "HOKAN_KOSU_52")
        map.Add("HOKAN_SURYO_52", "HOKAN_SURYO_52")
        map.Add("HOKAN_ICHI_53", "HOKAN_ICHI_53")
        map.Add("HOKAN_KOSU_53", "HOKAN_KOSU_53")
        map.Add("HOKAN_SURYO_53", "HOKAN_SURYO_53")
        map.Add("HOKAN_ICHI_54", "HOKAN_ICHI_54")
        map.Add("HOKAN_KOSU_54", "HOKAN_KOSU_54")
        map.Add("HOKAN_SURYO_54", "HOKAN_SURYO_54")
        map.Add("INKA_SOKUSHORI_KB", "INKA_SOKUSHORI_KB")
        map.Add("GENKA_BUMON", "GENKA_BUMON")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("YUSO_COMP_CD", "YUSO_COMP_CD")
        map.Add("JUST_OUTKA_KB", "JUST_OUTKA_KB")
        map.Add("SHUGENRYO_KB", "SHUGENRYO_KB")
        map.Add("GEKKAN_KB", "GEKKAN_KB")
        map.Add("YOBI", "YOBI")
        map.Add("YOBI2", "YOBI2")
        map.Add("ERROR_MSG_1", "ERROR_MSG_1")
        map.Add("ERROR_MSG_2", "ERROR_MSG_2")
        map.Add("ERROR_MSG_3", "ERROR_MSG_3")
        map.Add("ERROR_MSG_4", "ERROR_MSG_4")
        map.Add("ERROR_MSG_5", "ERROR_MSG_5")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        'map.Add("JISSEKI_USER", "JISSEKI_USER")
        'map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        'map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        'map.Add("SEND_USER", "SEND_USER")
        'map.Add("SEND_DATE", "SEND_DATE")
        'map.Add("SEND_TIME", "SEND_TIME")
        'map.Add("ERR_RCV_USER", "ERR_RCV_USER")
        'map.Add("ERR_RCV_DATE", "ERR_RCV_DATE")
        'map.Add("ERR_RCV_TIME", "ERR_RCV_TIME")
        'map.Add("CRT_USER", "CRT_USER")
        'map.Add("CRT_DATE", "CRT_DATE")
        'map.Add("CRT_TIME", "CRT_TIME")
        'map.Add("UPD_USER", "UPD_USER")
        'map.Add("UPD_DATE", "UPD_DATE")
        'map.Add("UPD_TIME", "UPD_TIME")

        Return map

    End Function
#End Region

#End Region 'Method

End Class
