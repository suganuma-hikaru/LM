' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷データ検索
'  EDI荷主ID　　　　:  107　　　 : チッソ
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Oracle.ManagedDataAccess.Client
Imports Jp.Co.Nrs.Com.Base

''' <summary>
''' LMH030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030DAC107
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "出荷登録処理 データ抽出用SQL"

#Region "H_OUTKAEDI_L データ抽出用"
    ''' <summary>
    ''' H_OUTKAEDI_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const L_DEF_SQL_SELECT_DATA As String = " SELECT                                                                             " & vbNewLine _
                                             & " '0'                                                 AS DEL_KB                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NRS_BR_CD                              AS NRS_BR_CD                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EDI_CTL_NO                             AS EDI_CTL_NO                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_CTL_NO                           AS OUTKA_CTL_NO                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_KB                               AS OUTKA_KB                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYUBETU_KB                             AS SYUBETU_KB                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NAIGAI_KB                              AS NAIGAI_KB                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_STATE_KB                         AS OUTKA_STATE_KB                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKAHOKOKU_YN                         AS OUTKAHOKOKU_YN                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.PICK_KB                                AS PICK_KB                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NRS_BR_NM                              AS NRS_BR_NM                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.WH_CD                                  AS WH_CD                            " & vbNewLine _
                                             & ",H_OUTKAEDI_L.WH_NM                                  AS WH_NM                            " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKA_PLAN_DATE                        AS OUTKA_PLAN_DATE                  " & vbNewLine _
                                             & ",H_OUTKAEDI_L.OUTKO_DATE                             AS OUTKO_DATE                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.ARR_PLAN_DATE                          AS ARR_PLAN_DATE                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.ARR_PLAN_TIME                          AS ARR_PLAN_TIME                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.HOKOKU_DATE                            AS HOKOKU_DATE                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.TOUKI_HOKAN_YN                         AS TOUKI_HOKAN_YN                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_CD_L                              AS CUST_CD_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_CD_M                              AS CUST_CD_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_NM_L                              AS CUST_NM_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_NM_M                              AS CUST_NM_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_CD_L                              AS SHIP_CD_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_CD_M                              AS SHIP_CD_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_NM_L                              AS SHIP_NM_L                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SHIP_NM_M                              AS SHIP_NM_M                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EDI_DEST_CD                            AS EDI_DEST_CD                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_CD                                AS DEST_CD                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_NM                                AS DEST_NM                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_ZIP                               AS DEST_ZIP                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_1                              AS DEST_AD_1                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_2                              AS DEST_AD_2                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_3                              AS DEST_AD_3                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_4                              AS DEST_AD_4                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_AD_5                              AS DEST_AD_5                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_TEL                               AS DEST_TEL                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_FAX                               AS DEST_FAX                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_MAIL                              AS DEST_MAIL                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DEST_JIS_CD                            AS DEST_JIS_CD                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SP_NHS_KB                              AS SP_NHS_KB                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.COA_YN                                 AS COA_YN                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CUST_ORD_NO                            AS CUST_ORD_NO                      " & vbNewLine _
                                             & ",H_OUTKAEDI_L.BUYER_ORD_NO                           AS BUYER_ORD_NO                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_MOTO_KB                           AS UNSO_MOTO_KB                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_TEHAI_KB                          AS UNSO_TEHAI_KB                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYARYO_KB                              AS SYARYO_KB                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.BIN_KB                                 AS BIN_KB                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_CD                                AS UNSO_CD                          " & vbNewLine _
                                             & ",M_UNSOCO.UNSOCO_NM                                  AS UNSO_NM                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_BR_CD                             AS UNSO_BR_CD                       " & vbNewLine _
                                             & ",M_UNSOCO.UNSOCO_BR_NM                               AS UNSO_BR_NM                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNCHIN_TARIFF_CD                       AS UNCHIN_TARIFF_CD                 " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EXTC_TARIFF_CD                         AS EXTC_TARIFF_CD                   " & vbNewLine _
                                             & ",H_OUTKAEDI_L.REMARK                                 AS REMARK                           " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNSO_ATT                               AS UNSO_ATT                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.DENP_YN                                AS DENP_YN                          " & vbNewLine _
                                             & ",H_OUTKAEDI_L.PC_KB                                  AS PC_KB                            " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UNCHIN_YN                              AS UNCHIN_YN                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.NIYAKU_YN                              AS NIYAKU_YN                        " & vbNewLine _
                                             & ",'1'                                                 AS OUT_FLAG                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.AKAKURO_KB                             AS AKAKURO_KB                       " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_FLAG                           AS JISSEKI_FLAG                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_USER                           AS JISSEKI_USER                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_DATE                           AS JISSEKI_DATE                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.JISSEKI_TIME                           AS JISSEKI_TIME                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N01                               AS FREE_N01                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N02                               AS FREE_N02                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N03                               AS FREE_N03                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N04                               AS FREE_N04                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N05                               AS FREE_N05                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N06                               AS FREE_N06                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N07                               AS FREE_N07                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N08                               AS FREE_N08                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N09                               AS FREE_N09                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_N10                               AS FREE_N10                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C01                               AS FREE_C01                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C02                               AS FREE_C02                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C03                               AS FREE_C03                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C04                               AS FREE_C04                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C05                               AS FREE_C05                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C06                               AS FREE_C06                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C07                               AS FREE_C07                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C08                               AS FREE_C08                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C09                               AS FREE_C09                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C10                               AS FREE_C10                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C11                               AS FREE_C11                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C12                               AS FREE_C12                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C13                               AS FREE_C13                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C14                               AS FREE_C14                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C15                               AS FREE_C15                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C16                               AS FREE_C16                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C17                               AS FREE_C17                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C18                               AS FREE_C18                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C19                               AS FREE_C19                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C20                               AS FREE_C20                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C21                               AS FREE_C21                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C22                               AS FREE_C22                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C23                               AS FREE_C23                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C24                               AS FREE_C24                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C25                               AS FREE_C25                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C26                               AS FREE_C26                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C27                               AS FREE_C27                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C28                               AS FREE_C28                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C29                               AS FREE_C29                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.FREE_C30                               AS FREE_C30                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CRT_USER                               AS CRT_USER                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CRT_DATE                               AS CRT_DATE                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.CRT_TIME                               AS CRT_TIME                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UPD_USER                               AS UPD_USER                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UPD_DATE                               AS UPD_DATE                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.UPD_TIME                               AS UPD_TIME                         " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SCM_CTL_NO_L                           AS SCM_CTL_NO_L                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.EDIT_FLAG                              AS EDIT_FLAG                        " & vbNewLine _
                                             & ",H_OUTKAEDI_L.MATCHING_FLAG                          AS MATCHING_FLAG                    " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_DATE                           AS SYS_ENT_DATE                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_TIME                           AS SYS_ENT_TIME                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_PGID                           AS SYS_ENT_PGID                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_ENT_USER                           AS SYS_ENT_USER                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_PGID                           AS SYS_UPD_PGID                     " & vbNewLine _
                                             & ",H_OUTKAEDI_L.SYS_UPD_USER                           AS SYS_UPD_USER                     " & vbNewLine _
                                             & ",'0'                                                 AS SYS_DEL_FLG                      " & vbNewLine _
                                             & " FROM                                                                                    " & vbNewLine _
                                             & " $LM_TRN$..H_OUTKAEDI_L         H_OUTKAEDI_L                                             " & vbNewLine _
                                             & " LEFT JOIN                                                                               " & vbNewLine _
                                             & " $LM_MST$..M_UNSOCO             M_UNSOCO                                                 " & vbNewLine _
                                             & " ON                                                                                      " & vbNewLine _
                                             & " H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                                             " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                                               " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                                         " & vbNewLine _
                                             & " WHERE                                                                                   " & vbNewLine _
                                             & " H_OUTKAEDI_L.SYS_DEL_FLG   = '0'                                                        " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.NRS_BR_CD     = @NRS_BR_CD                                                 " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.EDI_CTL_NO    = @EDI_CTL_NO                                                " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.SYS_UPD_DATE  = @SYS_UPD_DATE                                              " & vbNewLine _
                                             & " AND                                                                                     " & vbNewLine _
                                             & " H_OUTKAEDI_L.SYS_UPD_TIME  = @SYS_UPD_TIME                                              " & vbNewLine

#End Region

#Region "H_OUTKAEDI_Mデータ抽出用"
    ''' <summary>
    ''' H_OUTKAEDI_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>

    Private Const M_DEF_SQL_SELECT_DATA As String = " SELECT                                                             " & vbNewLine _
                                        & " '0'                                             AS  DEL_KB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.NRS_BR_CD                          AS  NRS_BR_CD                " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                         AS  EDI_CTL_NO               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO_CHU                     AS  EDI_CTL_NO_CHU           " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_CTL_NO                       AS  OUTKA_CTL_NO             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_CTL_NO_CHU                   AS  OUTKA_CTL_NO_CHU         " & vbNewLine _
                                        & ",H_OUTKAEDI_M.COA_YN                             AS  COA_YN                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                    AS  CUST_ORD_NO_DTL          " & vbNewLine _
                                        & ",H_OUTKAEDI_M.BUYER_ORD_NO_DTL                   AS  BUYER_ORD_NO_DTL         " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_GOODS_CD                      AS  CUST_GOODS_CD            " & vbNewLine _
                                        & ",H_OUTKAEDI_M.NRS_GOODS_CD                       AS  NRS_GOODS_CD             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                           AS  GOODS_NM                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.RSV_NO                             AS  RSV_NO                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.LOT_NO                             AS  LOT_NO                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SERIAL_NO                          AS  SERIAL_NO                " & vbNewLine _
                                        & ",H_OUTKAEDI_M.ALCTD_KB                           AS  ALCTD_KB                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_PKG_NB                       AS  OUTKA_PKG_NB             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_HASU                         AS  OUTKA_HASU               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_QT                           AS  OUTKA_QT                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_TTL_NB                       AS  OUTKA_TTL_NB             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUTKA_TTL_QT                       AS  OUTKA_TTL_QT             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.KB_UT                              AS  KB_UT                    " & vbNewLine _
                                        & ",H_OUTKAEDI_M.QT_UT                              AS  QT_UT                    " & vbNewLine _
                                        & ",H_OUTKAEDI_M.PKG_NB                             AS  PKG_NB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.PKG_UT                             AS  PKG_UT                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.ONDO_KB                            AS  ONDO_KB                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UNSO_ONDO_KB                       AS  UNSO_ONDO_KB             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.IRIME                              AS  IRIME                    " & vbNewLine _
                                        & ",H_OUTKAEDI_M.IRIME_UT                           AS  IRIME_UT                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.BETU_WT                            AS  BETU_WT                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.REMARK                             AS  REMARK                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.OUT_KB                             AS  OUT_KB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.AKAKURO_KB                         AS  AKAKURO_KB               " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_FLAG                       AS  JISSEKI_FLAG             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_USER                       AS  JISSEKI_USER             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_DATE                       AS  JISSEKI_DATE             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.JISSEKI_TIME                       AS  JISSEKI_TIME             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SET_KB                             AS  SET_KB                   " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N01                           AS  FREE_N01                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N02                           AS  FREE_N02                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N03                           AS  FREE_N03                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N04                           AS  FREE_N04                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N05                           AS  FREE_N05                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N06                           AS  FREE_N06                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N07                           AS  FREE_N07                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N08                           AS  FREE_N08                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N09                           AS  FREE_N09                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_N10                           AS  FREE_N10                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C01                           AS  FREE_C01                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C02                           AS  FREE_C02                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C03                           AS  FREE_C03                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C04                           AS  FREE_C04                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C05                           AS  FREE_C05                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C06                           AS  FREE_C06                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C07                           AS  FREE_C07                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C08                           AS  FREE_C08                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C09                           AS  FREE_C09                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C10                           AS  FREE_C10                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C11                           AS  FREE_C11                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C12                           AS  FREE_C12                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C13                           AS  FREE_C13                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C14                           AS  FREE_C14                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C15                           AS  FREE_C15                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C16                           AS  FREE_C16                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C17                           AS  FREE_C17                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C18                           AS  FREE_C18                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C19                           AS  FREE_C19                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C20                           AS  FREE_C20                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C21                           AS  FREE_C21                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C22                           AS  FREE_C22                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C23                           AS  FREE_C23                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C24                           AS  FREE_C24                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C25                           AS  FREE_C25                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C26                           AS  FREE_C26                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C27                           AS  FREE_C27                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C28                           AS  FREE_C28                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C29                           AS  FREE_C29                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.FREE_C30                           AS  FREE_C30                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_USER                           AS  CRT_USER                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_DATE                           AS  CRT_DATE                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CRT_TIME                           AS  CRT_TIME                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_USER                           AS  UPD_USER                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_DATE                           AS  UPD_DATE                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.UPD_TIME                           AS  UPD_TIME                 " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SCM_CTL_NO_L                       AS  SCM_CTL_NO_L             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SCM_CTL_NO_M                       AS  SCM_CTL_NO_M             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_DATE                       AS  SYS_ENT_DATE             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_TIME                       AS  SYS_ENT_TIME             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_PGID                       AS  SYS_ENT_PGID             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_ENT_USER                       AS  SYS_ENT_USER             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_DATE                       AS  SYS_UPD_DATE             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_TIME                       AS  SYS_UPD_TIME             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_PGID                       AS  SYS_UPD_PGID             " & vbNewLine _
                                        & ",H_OUTKAEDI_M.SYS_UPD_USER                       AS  SYS_UPD_USER             " & vbNewLine _
                                        & ",'0'                                             AS  SYS_DEL_FLG              " & vbNewLine _
                                        & ",M_DESTGOODS.SAGYO_KB_1                          AS  SAGYO_KB_1               " & vbNewLine _
                                        & ",M_DESTGOODS.SAGYO_KB_2                          AS  SAGYO_KB_2               " & vbNewLine _
                                        & " FROM                                                                         " & vbNewLine _
                                        & " $LM_TRN$..H_OUTKAEDI_M                                                       " & vbNewLine _
                                        & " INNER JOIN                                                                   " & vbNewLine _
                                        & " $LM_TRN$..H_OUTKAEDI_L    H_OUTKAEDI_L                                       " & vbNewLine _
                                        & " ON                                                                           " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                              " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO                            " & vbNewLine _
                                        & " LEFT JOIN                                                                    " & vbNewLine _
                                        & " $LM_MST$..M_DESTGOODS    M_DESTGOODS                                         " & vbNewLine _
                                        & " ON                                                                           " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD = M_DESTGOODS.NRS_BR_CD                               " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_GOODS_CD = M_DESTGOODS.GOODS_CD_NRS                         " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_CD_L = M_DESTGOODS.CUST_CD_L                               " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.CUST_CD_M = M_DESTGOODS.CUST_CD_M                               " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.DEST_CD = M_DESTGOODS.CD                                        " & vbNewLine _
                                        & " WHERE                                                                        " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_DEL_FLG  = '0'                                              " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.SYS_DEL_FLG  = '0'                                              " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_M.OUT_KB   = '0'                                                  " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.NRS_BR_CD         = @NRS_BR_CD                                  " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.EDI_CTL_NO         = @EDI_CTL_NO                                " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_UPD_DATE  = @SYS_UPD_DATE                                   " & vbNewLine _
                                        & " AND                                                                          " & vbNewLine _
                                        & " H_OUTKAEDI_L.SYS_UPD_TIME  = @SYS_UPD_TIME                                   " & vbNewLine
#End Region

#Region "制御用"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#End Region

#Region "出荷登録処理　存在チェックSQL"

#Region "SELECT_M_DEST"

    Private Const SQL_SELECT_COUNT_M_DEST As String = " SELECT                                 " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & ",NRS_BR_CD			                    AS NRS_BR_CD   " & vbNewLine _
                                     & ",CUST_CD_L			                    AS CUST_CD_L   " & vbNewLine _
                                     & ",DEST_NM			                    AS DEST_NM	   " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_DEST                       M_DEST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_DEST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_DEST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine

#End Region

#Region "GROUP_BY_M_DEST"

    Private Const SQL_GROUP_BY_M_DEST_COUNT As String = " GROUP BY                             " & vbNewLine _
                                                & " NRS_BR_CD                                  " & vbNewLine _
                                                & ",CUST_CD_L                                  " & vbNewLine _
                                                & ",DEST_NM	                                   " & vbNewLine

#End Region


#End Region

#Region "出荷登録処理 更新用SQL"

#Region "H_OUTKAEDI_L(通常出荷登録)"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKASAVEEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB             " & vbNewLine _
                                              & ",OUTKA_CTL_NO      = @OUTKA_CTL_NO       " & vbNewLine _
                                              & ",OUTKA_KB          = @OUTKA_KB           " & vbNewLine _
                                              & ",SYUBETU_KB        = @SYUBETU_KB         " & vbNewLine _
                                              & ",NAIGAI_KB         = @NAIGAI_KB          " & vbNewLine _
                                              & ",OUTKA_STATE_KB    = @OUTKA_STATE_KB     " & vbNewLine _
                                              & ",OUTKAHOKOKU_YN    = @OUTKAHOKOKU_YN     " & vbNewLine _
                                              & ",PICK_KB           = @PICK_KB            " & vbNewLine _
                                              & ",NRS_BR_NM         = @NRS_BR_NM          " & vbNewLine _
                                              & ",WH_CD             = @WH_CD              " & vbNewLine _
                                              & ",WH_NM             = @WH_NM              " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE   = @OUTKA_PLAN_DATE    " & vbNewLine _
                                              & ",OUTKO_DATE        = @OUTKO_DATE         " & vbNewLine _
                                              & ",ARR_PLAN_DATE     = @ARR_PLAN_DATE      " & vbNewLine _
                                              & ",ARR_PLAN_TIME     = @ARR_PLAN_TIME      " & vbNewLine _
                                              & ",HOKOKU_DATE       = @HOKOKU_DATE        " & vbNewLine _
                                              & ",TOUKI_HOKAN_YN    = @TOUKI_HOKAN_YN     " & vbNewLine _
                                              & ",CUST_CD_L         = @CUST_CD_L          " & vbNewLine _
                                              & ",CUST_CD_M         = @CUST_CD_M          " & vbNewLine _
                                              & ",CUST_NM_L         = @CUST_NM_L          " & vbNewLine _
                                              & ",CUST_NM_M         = @CUST_NM_M          " & vbNewLine _
                                              & ",SHIP_CD_L         = @SHIP_CD_L          " & vbNewLine _
                                              & ",SHIP_CD_M         = @SHIP_CD_M          " & vbNewLine _
                                              & ",SHIP_NM_L         = @SHIP_NM_L          " & vbNewLine _
                                              & ",SHIP_NM_M         = @SHIP_NM_M          " & vbNewLine _
                                              & ",EDI_DEST_CD       = @EDI_DEST_CD        " & vbNewLine _
                                              & ",DEST_CD           = @DEST_CD            " & vbNewLine _
                                              & ",DEST_NM           = @DEST_NM            " & vbNewLine _
                                              & ",DEST_ZIP          = @DEST_ZIP           " & vbNewLine _
                                              & ",DEST_AD_1         = @DEST_AD_1          " & vbNewLine _
                                              & ",DEST_AD_2         = @DEST_AD_2          " & vbNewLine _
                                              & ",DEST_AD_3         = @DEST_AD_3          " & vbNewLine _
                                              & ",DEST_AD_4         = @DEST_AD_4          " & vbNewLine _
                                              & ",DEST_AD_5         = @DEST_AD_5          " & vbNewLine _
                                              & ",DEST_TEL          = @DEST_TEL           " & vbNewLine _
                                              & ",DEST_FAX          = @DEST_FAX           " & vbNewLine _
                                              & ",DEST_MAIL         = @DEST_MAIL          " & vbNewLine _
                                              & ",DEST_JIS_CD       = @DEST_JIS_CD        " & vbNewLine _
                                              & ",SP_NHS_KB         = @SP_NHS_KB          " & vbNewLine _
                                              & ",COA_YN            = @COA_YN             " & vbNewLine _
                                              & ",CUST_ORD_NO       = @CUST_ORD_NO        " & vbNewLine _
                                              & ",BUYER_ORD_NO      = @BUYER_ORD_NO       " & vbNewLine _
                                              & ",UNSO_MOTO_KB      = @UNSO_MOTO_KB       " & vbNewLine _
                                              & ",UNSO_TEHAI_KB     = @UNSO_TEHAI_KB      " & vbNewLine _
                                              & ",SYARYO_KB         = @SYARYO_KB          " & vbNewLine _
                                              & ",BIN_KB            = @BIN_KB             " & vbNewLine _
                                              & ",UNSO_CD           = @UNSO_CD            " & vbNewLine _
                                              & ",UNSO_NM           = @UNSO_NM            " & vbNewLine _
                                              & ",UNSO_BR_CD        = @UNSO_BR_CD         " & vbNewLine _
                                              & ",UNSO_BR_NM        = @UNSO_BR_NM         " & vbNewLine _
                                              & ",UNCHIN_TARIFF_CD  = @UNCHIN_TARIFF_CD   " & vbNewLine _
                                              & ",EXTC_TARIFF_CD    = @EXTC_TARIFF_CD     " & vbNewLine _
                                              & ",REMARK            = @REMARK             " & vbNewLine _
                                              & ",UNSO_ATT          = @UNSO_ATT           " & vbNewLine _
                                              & ",DENP_YN           = @DENP_YN            " & vbNewLine _
                                              & ",PC_KB             = @PC_KB           	  " & vbNewLine _
                                              & ",UNCHIN_YN         = @UNCHIN_YN          " & vbNewLine _
                                              & ",NIYAKU_YN         = @NIYAKU_YN          " & vbNewLine _
                                              & ",OUT_FLAG          = @OUT_FLAG           " & vbNewLine _
                                              & ",AKAKURO_KB        = @AKAKURO_KB         " & vbNewLine _
                                              & ",JISSEKI_FLAG      = @JISSEKI_FLAG       " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER       " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE       " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME       " & vbNewLine _
                                              & ",FREE_N01          = @FREE_N01           " & vbNewLine _
                                              & ",FREE_N02          = @FREE_N02           " & vbNewLine _
                                              & ",FREE_N03          = @FREE_N03           " & vbNewLine _
                                              & ",FREE_N04          = @FREE_N04           " & vbNewLine _
                                              & ",FREE_N05          = @FREE_N05           " & vbNewLine _
                                              & ",FREE_N06          = @FREE_N06           " & vbNewLine _
                                              & ",FREE_N07          = @FREE_N07           " & vbNewLine _
                                              & ",FREE_N08          = @FREE_N08           " & vbNewLine _
                                              & ",FREE_N09          = @FREE_N09           " & vbNewLine _
                                              & ",FREE_N10          = @FREE_N10           " & vbNewLine _
                                              & ",FREE_C01          = @FREE_C01           " & vbNewLine _
                                              & ",FREE_C02          = @FREE_C02           " & vbNewLine _
                                              & ",FREE_C03          = @FREE_C03           " & vbNewLine _
                                              & ",FREE_C04          = @FREE_C04           " & vbNewLine _
                                              & ",FREE_C05          = @FREE_C05           " & vbNewLine _
                                              & ",FREE_C06          = @FREE_C06           " & vbNewLine _
                                              & ",FREE_C07          = @FREE_C07           " & vbNewLine _
                                              & ",FREE_C08          = @FREE_C08           " & vbNewLine _
                                              & ",FREE_C09          = @FREE_C09           " & vbNewLine _
                                              & ",FREE_C10          = @FREE_C10           " & vbNewLine _
                                              & ",FREE_C11          = @FREE_C11           " & vbNewLine _
                                              & ",FREE_C12          = @FREE_C12           " & vbNewLine _
                                              & ",FREE_C13          = @FREE_C13           " & vbNewLine _
                                              & ",FREE_C14          = @FREE_C14           " & vbNewLine _
                                              & ",FREE_C15          = @FREE_C15           " & vbNewLine _
                                              & ",FREE_C16          = @FREE_C16           " & vbNewLine _
                                              & ",FREE_C17          = @FREE_C17           " & vbNewLine _
                                              & ",FREE_C18          = @FREE_C18           " & vbNewLine _
                                              & ",FREE_C19          = @FREE_C19           " & vbNewLine _
                                              & ",FREE_C20          = @FREE_C20           " & vbNewLine _
                                              & ",FREE_C21          = @FREE_C21           " & vbNewLine _
                                              & ",FREE_C22          = @FREE_C22           " & vbNewLine _
                                              & ",FREE_C23          = @FREE_C23           " & vbNewLine _
                                              & ",FREE_C24          = @FREE_C24           " & vbNewLine _
                                              & ",FREE_C25          = @FREE_C25           " & vbNewLine _
                                              & ",FREE_C26          = @FREE_C26           " & vbNewLine _
                                              & ",FREE_C27          = @FREE_C27           " & vbNewLine _
                                              & ",FREE_C28          = @FREE_C28           " & vbNewLine _
                                              & ",FREE_C29          = @FREE_C29           " & vbNewLine _
                                              & ",FREE_C30          = @FREE_C30           " & vbNewLine _
                                              & ",CRT_USER          = @CRT_USER           " & vbNewLine _
                                              & ",CRT_DATE          = @CRT_DATE           " & vbNewLine _
                                              & ",CRT_TIME          = @CRT_TIME           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER           " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE           " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME           " & vbNewLine _
                                              & ",SCM_CTL_NO_L      = @SCM_CTL_NO_L       " & vbNewLine _
                                              & ",EDIT_FLAG         = @EDIT_FLAG          " & vbNewLine _
                                              & ",MATCHING_FLAG     = @MATCHING_FLAG      " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE       " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME       " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID       " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER       " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD          " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO         " & vbNewLine
#End Region

#Region "H_OUTKAEDI_L(まとめ先の更新用)"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MATOMESAKI_OUTKAEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET      " & vbNewLine _
                                              & " FREE_C30          = @FREE_C30                        " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                        " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                        " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                        " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                              & "WHERE                                                 " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                       " & vbNewLine _
                                              & " AND                                                  " & vbNewLine _
                                              & " OUTKA_CTL_NO      = @OUTKA_CTL_NO                    " & vbNewLine _
                                              & " AND                                                  " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                      " & vbNewLine _
                                              & " AND                                                  " & vbNewLine _
                                              & " SYS_DEL_FLG       <> '1'                             " & vbNewLine


#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_MのUPDATE文（H_OUTKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_OUTKAEDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET      " & vbNewLine _
                                          & " DEL_KB            =  @DEL_KB            	    " & vbNewLine _
                                          & ",OUTKA_CTL_NO      =  @OUTKA_CTL_NO            " & vbNewLine _
                                          & ",OUTKA_CTL_NO_CHU  =  @OUTKA_CTL_NO_CHU        " & vbNewLine _
                                          & ",COA_YN            =  @COA_YN                  " & vbNewLine _
                                          & ",CUST_ORD_NO_DTL   =  @CUST_ORD_NO_DTL         " & vbNewLine _
                                          & ",BUYER_ORD_NO_DTL  =  @BUYER_ORD_NO_DTL        " & vbNewLine _
                                          & ",CUST_GOODS_CD     =  @CUST_GOODS_CD           " & vbNewLine _
                                          & ",NRS_GOODS_CD      =  @NRS_GOODS_CD            " & vbNewLine _
                                          & ",GOODS_NM          =  @GOODS_NM                " & vbNewLine _
                                          & ",RSV_NO            =  @RSV_NO                  " & vbNewLine _
                                          & ",LOT_NO            =  @LOT_NO                  " & vbNewLine _
                                          & ",SERIAL_NO         =  @SERIAL_NO               " & vbNewLine _
                                          & ",ALCTD_KB          =  @ALCTD_KB                " & vbNewLine _
                                          & ",OUTKA_PKG_NB      =  @OUTKA_PKG_NB            " & vbNewLine _
                                          & ",OUTKA_HASU        =  @OUTKA_HASU              " & vbNewLine _
                                          & ",OUTKA_QT          =  @OUTKA_QT                " & vbNewLine _
                                          & ",OUTKA_TTL_NB      =  @OUTKA_TTL_NB            " & vbNewLine _
                                          & ",OUTKA_TTL_QT      =  @OUTKA_TTL_QT            " & vbNewLine _
                                          & ",KB_UT             =  @KB_UT                   " & vbNewLine _
                                          & ",QT_UT             =  @QT_UT                   " & vbNewLine _
                                          & ",PKG_NB            =  @PKG_NB                  " & vbNewLine _
                                          & ",PKG_UT            =  @PKG_UT                  " & vbNewLine _
                                          & ",ONDO_KB           =  @ONDO_KB                 " & vbNewLine _
                                          & ",UNSO_ONDO_KB      =  @UNSO_ONDO_KB            " & vbNewLine _
                                          & ",IRIME             =  @IRIME                   " & vbNewLine _
                                          & ",IRIME_UT          =  @IRIME_UT                " & vbNewLine _
                                          & ",BETU_WT           =  @BETU_WT                 " & vbNewLine _
                                          & ",REMARK            =  @REMARK                  " & vbNewLine _
                                          & ",OUT_KB            =  @OUT_KB                  " & vbNewLine _
                                          & ",AKAKURO_KB        =  @AKAKURO_KB              " & vbNewLine _
                                          & ",JISSEKI_FLAG      =  @JISSEKI_FLAG            " & vbNewLine _
                                          & ",JISSEKI_USER      =  @JISSEKI_USER            " & vbNewLine _
                                          & ",JISSEKI_DATE      =  @JISSEKI_DATE            " & vbNewLine _
                                          & ",JISSEKI_TIME      =  @JISSEKI_TIME            " & vbNewLine _
                                          & ",SET_KB            =  @SET_KB                  " & vbNewLine _
                                          & ",FREE_N01          =  @FREE_N01                " & vbNewLine _
                                          & ",FREE_N02          =  @FREE_N02                " & vbNewLine _
                                          & ",FREE_N03          =  @FREE_N03                " & vbNewLine _
                                          & ",FREE_N04          =  @FREE_N04                " & vbNewLine _
                                          & ",FREE_N05          =  @FREE_N05                " & vbNewLine _
                                          & ",FREE_N06          =  @FREE_N06                " & vbNewLine _
                                          & ",FREE_N07          =  @FREE_N07                " & vbNewLine _
                                          & ",FREE_N08          =  @FREE_N08                " & vbNewLine _
                                          & ",FREE_N09          =  @FREE_N09                " & vbNewLine _
                                          & ",FREE_N10          =  @FREE_N10                " & vbNewLine _
                                          & ",FREE_C01          =  @FREE_C01                " & vbNewLine _
                                          & ",FREE_C02          =  @FREE_C02                " & vbNewLine _
                                          & ",FREE_C03          =  @FREE_C03                " & vbNewLine _
                                          & ",FREE_C04          =  @FREE_C04                " & vbNewLine _
                                          & ",FREE_C05          =  @FREE_C05                " & vbNewLine _
                                          & ",FREE_C06          =  @FREE_C06                " & vbNewLine _
                                          & ",FREE_C07          =  @FREE_C07                " & vbNewLine _
                                          & ",FREE_C08          =  @FREE_C08                " & vbNewLine _
                                          & ",FREE_C09          =  @FREE_C09                " & vbNewLine _
                                          & ",FREE_C10          =  @FREE_C10                " & vbNewLine _
                                          & ",FREE_C11          =  @FREE_C11                " & vbNewLine _
                                          & ",FREE_C12          =  @FREE_C12                " & vbNewLine _
                                          & ",FREE_C13          =  @FREE_C13                " & vbNewLine _
                                          & ",FREE_C14          =  @FREE_C14                " & vbNewLine _
                                          & ",FREE_C15          =  @FREE_C15                " & vbNewLine _
                                          & ",FREE_C16          =  @FREE_C16                " & vbNewLine _
                                          & ",FREE_C17          =  @FREE_C17                " & vbNewLine _
                                          & ",FREE_C18          =  @FREE_C18                " & vbNewLine _
                                          & ",FREE_C19          =  @FREE_C19                " & vbNewLine _
                                          & ",FREE_C20          =  @FREE_C20                " & vbNewLine _
                                          & ",FREE_C21          =  @FREE_C21                " & vbNewLine _
                                          & ",FREE_C22          =  @FREE_C22                " & vbNewLine _
                                          & ",FREE_C23          =  @FREE_C23                " & vbNewLine _
                                          & ",FREE_C24          =  @FREE_C24                " & vbNewLine _
                                          & ",FREE_C25          =  @FREE_C25                " & vbNewLine _
                                          & ",FREE_C26          =  @FREE_C26                " & vbNewLine _
                                          & ",FREE_C27          =  @FREE_C27                " & vbNewLine _
                                          & ",FREE_C28          =  @FREE_C28                " & vbNewLine _
                                          & ",FREE_C29          =  @FREE_C29                " & vbNewLine _
                                          & ",FREE_C30          =  @FREE_C30                " & vbNewLine _
                                          & ",CRT_USER          =  @CRT_USER                " & vbNewLine _
                                          & ",CRT_DATE          =  @CRT_DATE                " & vbNewLine _
                                          & ",CRT_TIME          =  @CRT_TIME                " & vbNewLine _
                                          & ",UPD_USER          =  @UPD_USER                " & vbNewLine _
                                          & ",UPD_DATE          =  @UPD_DATE                " & vbNewLine _
                                          & ",UPD_TIME          =  @UPD_TIME                " & vbNewLine _
                                          & ",SCM_CTL_NO_L      =  @SCM_CTL_NO_L            " & vbNewLine _
                                          & ",SCM_CTL_NO_M      =  @SCM_CTL_NO_M            " & vbNewLine _
                                          & ",SYS_UPD_DATE      =  @SYS_UPD_DATE            " & vbNewLine _
                                          & ",SYS_UPD_TIME      =  @SYS_UPD_TIME            " & vbNewLine _
                                          & ",SYS_UPD_PGID      =  @SYS_UPD_PGID            " & vbNewLine _
                                          & ",SYS_UPD_USER      =  @SYS_UPD_USER            " & vbNewLine _
                                          & ",SYS_DEL_FLG       =  @SYS_DEL_FLG             " & vbNewLine _
                                          & "WHERE   NRS_BR_CD  =  @NRS_BR_CD               " & vbNewLine _
                                          & "AND EDI_CTL_NO     =  @EDI_CTL_NO              " & vbNewLine _
                                          & "AND EDI_CTL_NO_CHU =  @EDI_CTL_NO_CHU          " & vbNewLine
#End Region

#Region "C_OUTKA_L(INSERT)"

    ''' <summary>
    ''' INSERT（OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKA_L As String = "INSERT INTO                                        " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_L                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",FURI_NO                                                 " & vbNewLine _
                                         & ",OUTKA_KB                                                " & vbNewLine _
                                         & ",SYUBETU_KB                                              " & vbNewLine _
                                         & ",OUTKA_STATE_KB                                          " & vbNewLine _
                                         & ",OUTKAHOKOKU_YN                                          " & vbNewLine _
                                         & ",PICK_KB                                                 " & vbNewLine _
                                         & ",DENP_NO                                                 " & vbNewLine _
                                         & ",ARR_KANRYO_INFO                                         " & vbNewLine _
                                         & ",WH_CD                                                   " & vbNewLine _
                                         & ",OUTKA_PLAN_DATE                                         " & vbNewLine _
                                         & ",OUTKO_DATE                                              " & vbNewLine _
                                         & ",ARR_PLAN_DATE                                           " & vbNewLine _
                                         & ",ARR_PLAN_TIME                                           " & vbNewLine _
                                         & ",HOKOKU_DATE                                             " & vbNewLine _
                                         & ",TOUKI_HOKAN_YN                                          " & vbNewLine _
                                         & ",END_DATE                                                " & vbNewLine _
                                         & ",CUST_CD_L                                               " & vbNewLine _
                                         & ",CUST_CD_M                                               " & vbNewLine _
                                         & ",SHIP_CD_L                                               " & vbNewLine _
                                         & ",SHIP_CD_M                                               " & vbNewLine _
                                         & ",DEST_CD                                                 " & vbNewLine _
                                         & ",DEST_AD_3                                               " & vbNewLine _
                                         & ",DEST_TEL                                                " & vbNewLine _
                                         & ",NHS_REMARK                                              " & vbNewLine _
                                         & ",SP_NHS_KB                                               " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO                                             " & vbNewLine _
                                         & ",BUYER_ORD_NO                                            " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",DENP_YN                                                 " & vbNewLine _
                                         & ",PC_KB                                                   " & vbNewLine _
                                         & ",NIYAKU_YN                                               " & vbNewLine _
                                         & ",ALL_PRINT_FLAG                                          " & vbNewLine _
                                         & ",NIHUDA_FLAG                                             " & vbNewLine _
                                         & ",NHS_FLAG                                                " & vbNewLine _
                                         & ",DENP_FLAG                                               " & vbNewLine _
                                         & ",COA_FLAG                                                " & vbNewLine _
                                         & ",HOKOKU_FLAG                                             " & vbNewLine _
                                         & ",MATOME_PICK_FLAG                                        " & vbNewLine _
                                         & ",LAST_PRINT_DATE                                         " & vbNewLine _
                                         & ",LAST_PRINT_TIME                                         " & vbNewLine _
                                         & ",SASZ_USER                                               " & vbNewLine _
                                         & ",OUTKO_USER                                              " & vbNewLine _
                                         & ",KEN_USER                                                " & vbNewLine _
                                         & ",OUTKA_USER                                              " & vbNewLine _
                                         & ",HOU_USER                                                " & vbNewLine _
                                         & ",ORDER_TYPE                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ",DEST_KB                                                 " & vbNewLine _
                                         & ",DEST_NM                                                 " & vbNewLine _
                                         & ",DEST_AD_1                                               " & vbNewLine _
                                         & ",DEST_AD_2                                               " & vbNewLine _
                                         & ",WH_TAB_STATUS                                           " & vbNewLine _
                                         & ",WH_TAB_YN                                               " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@FURI_NO                                                " & vbNewLine _
                                         & ",@OUTKA_KB                                               " & vbNewLine _
                                         & ",@SYUBETU_KB                                             " & vbNewLine _
                                         & ",@OUTKA_STATE_KB                                         " & vbNewLine _
                                         & ",@OUTKAHOKOKU_YN                                         " & vbNewLine _
                                         & ",@PICK_KB                                                " & vbNewLine _
                                         & ",@DENP_NO                                                " & vbNewLine _
                                         & ",@ARR_KANRYO_INFO                                        " & vbNewLine _
                                         & ",@WH_CD                                                  " & vbNewLine _
                                         & ",@OUTKA_PLAN_DATE                                        " & vbNewLine _
                                         & ",@OUTKO_DATE                                             " & vbNewLine _
                                         & ",@ARR_PLAN_DATE                                          " & vbNewLine _
                                         & ",@ARR_PLAN_TIME                                          " & vbNewLine _
                                         & ",@HOKOKU_DATE                                            " & vbNewLine _
                                         & ",@TOUKI_HOKAN_YN                                         " & vbNewLine _
                                         & ",@END_DATE                                               " & vbNewLine _
                                         & ",@CUST_CD_L                                              " & vbNewLine _
                                         & ",@CUST_CD_M                                              " & vbNewLine _
                                         & ",@SHIP_CD_L                                              " & vbNewLine _
                                         & ",@SHIP_CD_M                                              " & vbNewLine _
                                         & ",@DEST_CD                                                " & vbNewLine _
                                         & ",@DEST_AD_3                                              " & vbNewLine _
                                         & ",@DEST_TEL                                               " & vbNewLine _
                                         & ",@NHS_REMARK                                             " & vbNewLine _
                                         & ",@SP_NHS_KB                                              " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO                                            " & vbNewLine _
                                         & ",@BUYER_ORD_NO                                           " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@DENP_YN                                                " & vbNewLine _
                                         & ",@PC_KB                                                  " & vbNewLine _
                                         & ",@NIYAKU_YN                                              " & vbNewLine _
                                         & ",@ALL_PRINT_FLAG                                         " & vbNewLine _
                                         & ",@NIHUDA_FLAG                                            " & vbNewLine _
                                         & ",@NHS_FLAG                                               " & vbNewLine _
                                         & ",@DENP_FLAG                                              " & vbNewLine _
                                         & ",@COA_FLAG                                               " & vbNewLine _
                                         & ",@HOKOKU_FLAG                                            " & vbNewLine _
                                         & ",@MATOME_PICK_FLAG                                       " & vbNewLine _
                                         & ",@LAST_PRINT_DATE                                        " & vbNewLine _
                                         & ",@LAST_PRINT_TIME                                        " & vbNewLine _
                                         & ",@SASZ_USER                                              " & vbNewLine _
                                         & ",@OUTKO_USER                                             " & vbNewLine _
                                         & ",@KEN_USER                                               " & vbNewLine _
                                         & ",@OUTKA_USER                                             " & vbNewLine _
                                         & ",@HOU_USER                                               " & vbNewLine _
                                         & ",@ORDER_TYPE                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ",@DEST_KB                                                " & vbNewLine _
                                         & ",@DEST_NM                                                " & vbNewLine _
                                         & ",@DEST_AD_1                                              " & vbNewLine _
                                         & ",@DEST_AD_2                                              " & vbNewLine _
                                         & ",@WH_TAB_STATUS                                          " & vbNewLine _
                                         & ",@WH_TAB_YN                                              " & vbNewLine _
                                         & ")                                                        " & vbNewLine
#End Region

#Region "OUTKA_M"

    ''' <summary>
    ''' INSERT（OUTKA_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKA_M As String = "INSERT INTO                                        " & vbNewLine _
                                         & "$LM_TRN$..C_OUTKA_M                                      " & vbNewLine _
                                         & "(                                                        " & vbNewLine _
                                         & " NRS_BR_CD                                               " & vbNewLine _
                                         & ",OUTKA_NO_L                                              " & vbNewLine _
                                         & ",OUTKA_NO_M                                              " & vbNewLine _
                                         & ",EDI_SET_NO                                              " & vbNewLine _
                                         & ",COA_YN                                                  " & vbNewLine _
                                         & ",CUST_ORD_NO_DTL                                         " & vbNewLine _
                                         & ",BUYER_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",GOODS_CD_NRS                                            " & vbNewLine _
                                         & ",RSV_NO                                                  " & vbNewLine _
                                         & ",LOT_NO                                                  " & vbNewLine _
                                         & ",SERIAL_NO                                               " & vbNewLine _
                                         & ",ALCTD_KB                                                " & vbNewLine _
                                         & ",OUTKA_PKG_NB                                            " & vbNewLine _
                                         & ",OUTKA_HASU                                              " & vbNewLine _
                                         & ",OUTKA_QT                                                " & vbNewLine _
                                         & ",OUTKA_TTL_NB                                            " & vbNewLine _
                                         & ",OUTKA_TTL_QT                                            " & vbNewLine _
                                         & ",ALCTD_NB                                                " & vbNewLine _
                                         & ",ALCTD_QT                                                " & vbNewLine _
                                         & ",BACKLOG_NB                                              " & vbNewLine _
                                         & ",BACKLOG_QT                                              " & vbNewLine _
                                         & ",UNSO_ONDO_KB                                            " & vbNewLine _
                                         & ",IRIME                                                   " & vbNewLine _
                                         & ",IRIME_UT                                                " & vbNewLine _
                                         & ",OUTKA_M_PKG_NB                                          " & vbNewLine _
                                         & ",REMARK                                                  " & vbNewLine _
                                         & ",SIZE_KB                                                 " & vbNewLine _
                                         & ",ZAIKO_KB                                                " & vbNewLine _
                                         & ",SOURCE_CD                                               " & vbNewLine _
                                         & ",YELLOW_CARD                                             " & vbNewLine _
                                         & ",PRINT_SORT                                              " & vbNewLine _
                                         & ",SYS_ENT_DATE                                            " & vbNewLine _
                                         & ",SYS_ENT_TIME                                            " & vbNewLine _
                                         & ",SYS_ENT_PGID                                            " & vbNewLine _
                                         & ",SYS_ENT_USER                                            " & vbNewLine _
                                         & ",SYS_UPD_DATE                                            " & vbNewLine _
                                         & ",SYS_UPD_TIME                                            " & vbNewLine _
                                         & ",SYS_UPD_PGID                                            " & vbNewLine _
                                         & ",SYS_UPD_USER                                            " & vbNewLine _
                                         & ",SYS_DEL_FLG                                             " & vbNewLine _
                                         & ")VALUES(                                                 " & vbNewLine _
                                         & " @NRS_BR_CD                                              " & vbNewLine _
                                         & ",@OUTKA_NO_L                                             " & vbNewLine _
                                         & ",@OUTKA_NO_M                                             " & vbNewLine _
                                         & ",@EDI_SET_NO                                             " & vbNewLine _
                                         & ",@COA_YN                                                 " & vbNewLine _
                                         & ",@CUST_ORD_NO_DTL                                        " & vbNewLine _
                                         & ",@BUYER_ORD_NO_DTL                                       " & vbNewLine _
                                         & ",@GOODS_CD_NRS                                           " & vbNewLine _
                                         & ",@RSV_NO                                                 " & vbNewLine _
                                         & ",@LOT_NO                                                 " & vbNewLine _
                                         & ",@SERIAL_NO                                              " & vbNewLine _
                                         & ",@ALCTD_KB                                               " & vbNewLine _
                                         & ",@OUTKA_PKG_NB                                           " & vbNewLine _
                                         & ",@OUTKA_HASU                                             " & vbNewLine _
                                         & ",@OUTKA_QT                                               " & vbNewLine _
                                         & ",@OUTKA_TTL_NB                                           " & vbNewLine _
                                         & ",@OUTKA_TTL_QT                                           " & vbNewLine _
                                         & ",@ALCTD_NB                                               " & vbNewLine _
                                         & ",@ALCTD_QT                                               " & vbNewLine _
                                         & ",@BACKLOG_NB                                             " & vbNewLine _
                                         & ",@BACKLOG_QT                                             " & vbNewLine _
                                         & ",@UNSO_ONDO_KB                                           " & vbNewLine _
                                         & ",@IRIME                                                  " & vbNewLine _
                                         & ",@IRIME_UT                                               " & vbNewLine _
                                         & ",@OUTKA_M_PKG_NB                                         " & vbNewLine _
                                         & ",@REMARK                                                 " & vbNewLine _
                                         & ",@SIZE_KB                                                " & vbNewLine _
                                         & ",@ZAIKO_KB                                               " & vbNewLine _
                                         & ",@SOURCE_CD                                              " & vbNewLine _
                                         & ",@YELLOW_CARD                                            " & vbNewLine _
                                         & ",@PRINT_SORT                                             " & vbNewLine _
                                         & ",@SYS_ENT_DATE                                           " & vbNewLine _
                                         & ",@SYS_ENT_TIME                                           " & vbNewLine _
                                         & ",@SYS_ENT_PGID                                           " & vbNewLine _
                                         & ",@SYS_ENT_USER                                           " & vbNewLine _
                                         & ",@SYS_UPD_DATE                                           " & vbNewLine _
                                         & ",@SYS_UPD_TIME                                           " & vbNewLine _
                                         & ",@SYS_UPD_PGID                                           " & vbNewLine _
                                         & ",@SYS_UPD_USER                                           " & vbNewLine _
                                         & ",@SYS_DEL_FLG                                            " & vbNewLine _
                                         & ")                                                        " & vbNewLine

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
                                              & ",M_SOKO.SOKO_DEST_CD          " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
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
                                              & "M_SOKO.WH_CD  = @WH_CD                    " & vbNewLine _
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

#Region "C_OUTKA_L(まとめ)"
    Private Const SQL_UPDATE_OUTKAL_MATOME As String = "UPDATE $LM_TRN$..C_OUTKA_L SET " & vbNewLine _
                                          & " OUTKA_PKG_NB      = @OUTKA_PKG_NB        " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE        " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME        " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID        " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER        " & vbNewLine _
                                          & "WHERE   NRS_BR_CD  = @NRS_BR_CD           " & vbNewLine _
                                          & "AND OUTKA_NO_L     = @OUTKA_NO_L          " & vbNewLine _
                                          & "AND SYS_DEL_FLG     <> '1'                " & vbNewLine
#End Region

#Region "F_UNSO_L(まとめ)"
    ''' <summary>
    ''' F_UNSO_LのUPDATE文（F_UNSOI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_MATOMESAKI_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET   " & vbNewLine _
                                              & " UNSO_WT          = @UNSO_WT               " & vbNewLine _
                                              & ",UNSO_PKG_NB       = @UNSO_PKG_NB          " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE         " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME         " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID         " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER         " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD            " & vbNewLine _
                                              & "AND UNSO_NO_L   = @UNSO_NO_L               " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                 " & vbNewLine
#End Region

    '#Region "M_DEST INSERT(チッソは自動追加がない為使用しない)"

    '    ''' <summary>
    '    ''' 届先マスタINSERT文（M_DEST）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_INSERT_M_DEST_NCGO As String = "INSERT INTO $LM_MST$..M_DEST        " & vbNewLine _
    '                                       & "(                                   " & vbNewLine _
    '                                       & "      NRS_BR_CD                     " & vbNewLine _
    '                                       & "      ,CUST_CD_L                    " & vbNewLine _
    '                                       & "      ,DEST_CD                      " & vbNewLine _
    '                                       & "      ,EDI_CD                       " & vbNewLine _
    '                                       & "      ,DEST_NM                      " & vbNewLine _
    '                                       & "      ,ZIP                          " & vbNewLine _
    '                                       & "      ,AD_1                         " & vbNewLine _
    '                                       & "      ,AD_2                         " & vbNewLine _
    '                                       & "      ,AD_3                         " & vbNewLine _
    '                                       & "      ,CUST_DEST_CD                 " & vbNewLine _
    '                                       & "      ,SALES_CD                     " & vbNewLine _
    '                                       & "      ,SP_NHS_KB                    " & vbNewLine _
    '                                       & "      ,COA_YN                       " & vbNewLine _
    '                                       & "      ,SP_UNSO_CD                   " & vbNewLine _
    '                                       & "      ,SP_UNSO_BR_CD                " & vbNewLine _
    '                                       & "      ,DELI_ATT                     " & vbNewLine _
    '                                       & "      ,CARGO_TIME_LIMIT             " & vbNewLine _
    '                                       & "      ,LARGE_CAR_YN                 " & vbNewLine _
    '                                       & "      ,TEL                          " & vbNewLine _
    '                                       & "      ,FAX                          " & vbNewLine _
    '                                       & "      ,UNCHIN_SEIQTO_CD             " & vbNewLine _
    '                                       & "      ,JIS                          " & vbNewLine _
    '                                       & "      ,KYORI                        " & vbNewLine _
    '                                       & "      ,PICK_KB                      " & vbNewLine _
    '                                       & "      ,BIN_KB                       " & vbNewLine _
    '                                       & "      ,MOTO_CHAKU_KB                " & vbNewLine _
    '                                       & "      ,URIAGE_CD                    " & vbNewLine _
    '& "--'(2012.10.02)START UMANO 要望番号1485 支払運賃に伴う修正" & vbNewLine _
    '& "      ,SHIHARAI_AD                  " & vbNewLine _
    '& "--'(2012.10.02)END UMANO 要望番号1485 支払運賃に伴う修正" & vbNewLine _
    '                                       & "      ,SYS_ENT_DATE                 " & vbNewLine _
    '                                       & "      ,SYS_ENT_TIME                 " & vbNewLine _
    '                                       & "      ,SYS_ENT_PGID                 " & vbNewLine _
    '                                       & "      ,SYS_ENT_USER                 " & vbNewLine _
    '                                       & "      ,SYS_UPD_DATE                 " & vbNewLine _
    '                                       & "      ,SYS_UPD_TIME                 " & vbNewLine _
    '                                       & "      ,SYS_UPD_PGID                 " & vbNewLine _
    '                                       & "      ,SYS_UPD_USER                 " & vbNewLine _
    '                                       & "      ,SYS_DEL_FLG                  " & vbNewLine _
    '                                       & "      ) VALUES (                    " & vbNewLine _
    '                                       & "      @NRS_BR_CD                    " & vbNewLine _
    '                                       & "      ,@CUST_CD_L                   " & vbNewLine _
    '                                       & "      ,@DEST_CD                     " & vbNewLine _
    '                                       & "      ,@EDI_CD                      " & vbNewLine _
    '                                       & "      ,@DEST_NM                     " & vbNewLine _
    '                                       & "      ,@ZIP                         " & vbNewLine _
    '                                       & "      ,@AD_1                        " & vbNewLine _
    '                                       & "      ,@AD_2                        " & vbNewLine _
    '                                       & "      ,@AD_3                        " & vbNewLine _
    '                                       & "      ,@CUST_DEST_CD                " & vbNewLine _
    '                                       & "      ,@SALES_CD                    " & vbNewLine _
    '                                       & "      ,@SP_NHS_KB                   " & vbNewLine _
    '                                       & "      ,@COA_YN                      " & vbNewLine _
    '                                       & "      ,@SP_UNSO_CD                  " & vbNewLine _
    '                                       & "      ,@SP_UNSO_BR_CD               " & vbNewLine _
    '                                       & "      ,@DELI_ATT                    " & vbNewLine _
    '                                       & "      ,@CARGO_TIME_LIMIT            " & vbNewLine _
    '                                       & "      ,@LARGE_CAR_YN                " & vbNewLine _
    '                                       & "      ,@TEL                         " & vbNewLine _
    '                                       & "      ,@FAX                         " & vbNewLine _
    '                                       & "      ,@UNCHIN_SEIQTO_CD            " & vbNewLine _
    '                                       & "      ,@JIS                         " & vbNewLine _
    '                                       & "      ,@KYORI                       " & vbNewLine _
    '                                       & "      ,@PICK_KB                     " & vbNewLine _
    '                                       & "      ,@BIN_KB                      " & vbNewLine _
    '                                       & "      ,@MOTO_CHAKU_KB               " & vbNewLine _
    '                                       & "      ,@URIAGE_CD                   " & vbNewLine _
    '& "--'(2012.10.02)START UMANO 要望番号1485 支払運賃に伴う修正" & vbNewLine _
    '& "      ,@AD_1 + @AD_2 + @AD_3        " & vbNewLine _
    '& "--'(2012.10.02)END UMANO 要望番号1485 支払運賃に伴う修正" & vbNewLine _
    '                                       & "      ,@SYS_ENT_DATE                " & vbNewLine _
    '                                       & "      ,@SYS_ENT_TIME                " & vbNewLine _
    '                                       & "      ,@SYS_ENT_PGID                " & vbNewLine _
    '                                       & "      ,@SYS_ENT_USER                " & vbNewLine _
    '                                       & "      ,@SYS_UPD_DATE                " & vbNewLine _
    '                                       & "      ,@SYS_UPD_TIME                " & vbNewLine _
    '                                       & "      ,@SYS_UPD_PGID                " & vbNewLine _
    '                                       & "      ,@SYS_UPD_USER                " & vbNewLine _
    '                                       & "      ,@SYS_DEL_FLG                 " & vbNewLine _
    '                                       & ")                                   " & vbNewLine

    '#End Region

    '#Region "M_DEST UPDATE(届先項目差異有りの場合)(チッソは自動更新がない為使用しない)"
    '    ''' <summary>
    '    ''' 届先マスタUPDATE文（M_DEST）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_UPDATE_M_DEST_TOHO As String = "UPDATE $LM_MST$..M_DEST SET       " & vbNewLine _
    '                                              & " DEST_NM           = @DEST_NM       	" & vbNewLine _
    '                                              & ",ZIP               = @ZIP              " & vbNewLine _
    '                                              & ",AD_1              = @AD_1             " & vbNewLine _
    '                                              & ",AD_2              = @AD_2             " & vbNewLine _
    '                                              & ",AD_3              = @AD_3             " & vbNewLine _
    '                                              & ",TEL               = @TEL              " & vbNewLine _
    '                                              & ",JIS               = @JIS              " & vbNewLine _
    '                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE     " & vbNewLine _
    '                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME     " & vbNewLine _
    '                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID     " & vbNewLine _
    '                                              & ",SYS_UPD_USER      = @SYS_UPD_USER     " & vbNewLine _
    '                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD        " & vbNewLine _
    '                                              & "AND CUST_CD_L      = @CUST_CD_L        " & vbNewLine _
    '                                              & "AND DEST_CD        = @DEST_CD          " & vbNewLine

    '#End Region


#End Region

#Region "実績作成処理 受信対象データ抽出用SQL"

#Region "SELECT_Z_KBN(ORACLE接続情報取得)"
    Private Const SQL_SELECT_ORA_SCHMA As String = " SELECT                                    " & vbNewLine _
                                     & " KBN_NM1                                AS NIS_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = 'O008'                           " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.KBN_CD   = '00'                                 " & vbNewLine

#End Region

#Region "出荷追加データチェック"

    Private Const SQL_SELECT_OUTKA_ADD As String = " SELECT COUNT(*)     AS SELECT_CNT     FROM     " & vbNewLine _
                                                  & "(SELECT                                        " & vbNewLine _
                                                  & "CM.NRS_BR_CD,                                  " & vbNewLine _
                                                  & "CM.OUTKA_NO_L,                                 " & vbNewLine _
                                                  & "CM.GOODS_CD_NRS                                " & vbNewLine _
                                                  & "FROM $LM_TRN$..C_OUTKA_M CM                    " & vbNewLine _
                                                  & "LEFT JOIN $LM_TRN$..H_OUTKAEDI_M EM            " & vbNewLine _
                                                  & "ON  CM.NRS_BR_CD = EM.NRS_BR_CD                " & vbNewLine _
                                                  & "AND CM.OUTKA_NO_L = EM.OUTKA_CTL_NO            " & vbNewLine _
                                                  & "AND CM.OUTKA_NO_M = EM.OUTKA_CTL_NO_CHU        " & vbNewLine _
                                                  & "WHERE CM.SYS_DEL_FLG <> '1'                    " & vbNewLine _
                                                  & "AND  EM.EDI_CTL_NO IS NULL                     " & vbNewLine _
                                                  & "AND  CM.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                                  & "AND  CM.OUTKA_NO_L = @OUTKA_CTL_NO             " & vbNewLine _
                                                  & ") M                                            " & vbNewLine _
                                                  & "INNER JOIN  $LM_TRN$..H_OUTKAEDI_M  E          " & vbNewLine _
                                                  & "ON  M.NRS_BR_CD = E.NRS_BR_CD                  " & vbNewLine _
                                                  & "AND M.OUTKA_NO_L = E.OUTKA_CTL_NO              " & vbNewLine _
                                                  & "AND M.GOODS_CD_NRS = E.NRS_GOODS_CD            " & vbNewLine

#End Region

#Region "SELECT・FROM句"

    Private Const SQL_SELECT_SEARCH_HED_CSO_DATA As String = "SELECT                                             " & vbNewLine _
                                        & "      A.*, B.CNT                                                   " & vbNewLine _
                                        & "      FROM                                                         " & vbNewLine _
                                        & "       (SELECT *                                                   " & vbNewLine _
                                        & "        FROM                                                       " & vbNewLine _
                                        & "         (SELECT                                                   " & vbNewLine _
                                        & "          AA1.NRS_BR_CD,                                           " & vbNewLine _
                                        & "          AA1.EDI_CTL_NO,                                          " & vbNewLine _
                                        & "          AA1.EDI_CTL_NO_CHU,                                      " & vbNewLine _
                                        & "          AA1.OUTKA_CTL_NO,                                        " & vbNewLine _
                                        & "          AA1.OUTKA_CTL_NO_CHU,                                    " & vbNewLine _
                                        & "          AA1.FREE_C01,                                            " & vbNewLine _
                                        & "          AA1.FREE_C02,                                            " & vbNewLine _
                                        & "          AA1.OUTKO_DATE,                                          " & vbNewLine _
                                        & "          AA1.ARR_PLAN_DATE,                                       " & vbNewLine _
                                        & "          AA1.UNSO_CD,                                             " & vbNewLine _
                                        & "          AA1.UNSO_BR_CD,                                          " & vbNewLine _
                                        & "          AA1.LOT_NO,                                              " & vbNewLine _
                                        & "          SUM(AA1.ALCTD_QT) AS ALCTD_QT                            " & vbNewLine _
                                        & "          FROM                                                     " & vbNewLine _
                                        & "           (SELECT                                                 " & vbNewLine _
                                        & "            E1.NRS_BR_CD,                                          " & vbNewLine _
                                        & "            E1.EDI_CTL_NO,                                         " & vbNewLine _
                                        & "            E1.EDI_CTL_NO_CHU,                                     " & vbNewLine _
                                        & "            E1.OUTKA_CTL_NO,                                       " & vbNewLine _
                                        & "            E1.OUTKA_CTL_NO_CHU,                                   " & vbNewLine _
                                        & "            E1.FREE_C01,                                           " & vbNewLine _
                                        & "            E1.FREE_C02,                                           " & vbNewLine _
                                        & "            L1.OUTKO_DATE,                                         " & vbNewLine _
                                        & "            L1.ARR_PLAN_DATE,                                      " & vbNewLine _
                                        & "            F1.UNSO_CD,                                            " & vbNewLine _
                                        & "            F1.UNSO_BR_CD,                                         " & vbNewLine _
                                        & "            ISNULL(S1.LOT_NO,E1.LOT_NO) AS LOT_NO,                 " & vbNewLine _
                                        & "            ISNULL(S1.ALCTD_QT,0) AS ALCTD_QT                      " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "            FROM  $LM_TRN$..H_OUTKAEDI_M  E1                       " & vbNewLine _
                                        & "            INNER JOIN  $LM_TRN$..C_OUTKA_L  L1                    " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "             ON  E1.NRS_BR_CD = L1.NRS_BR_CD                       " & vbNewLine _
                                        & "             AND E1.OUTKA_CTL_NO = L1.OUTKA_NO_L                   " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "            LEFT JOIN  $LM_TRN$..F_UNSO_L  F1                      " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "             ON  L1.NRS_BR_CD = F1.NRS_BR_CD                       " & vbNewLine _
                                        & "             AND L1.OUTKA_NO_L = F1.INOUTKA_NO_L                   " & vbNewLine _
                                        & "             AND F1.MOTO_DATA_KB = '20'                            " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "            LEFT JOIN  $LM_TRN$..C_OUTKA_M  M1                     " & vbNewLine _
                                        & "             ON  E1.NRS_BR_CD = M1.NRS_BR_CD                       " & vbNewLine _
                                        & "             AND E1.OUTKA_CTL_NO = M1.OUTKA_NO_L                   " & vbNewLine _
                                        & "             AND E1.EDI_CTL_NO_CHU = M1.OUTKA_NO_M                 " & vbNewLine _
                                        & "             AND M1.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                        & "            LEFT JOIN  $LM_TRN$..C_OUTKA_S  S1                     " & vbNewLine _
                                        & "             ON  M1.NRS_BR_CD IS NOT NULL                          " & vbNewLine _
                                        & "             AND M1.NRS_BR_CD = S1.NRS_BR_CD                       " & vbNewLine _
                                        & "             AND M1.OUTKA_NO_L = S1.OUTKA_NO_L                     " & vbNewLine _
                                        & "             AND M1.OUTKA_NO_M = S1.OUTKA_NO_M                     " & vbNewLine _
                                        & "             AND S1.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "            WHERE E1.NRS_BR_CD = @NRS_BR_CD                        " & vbNewLine _
                                        & "             AND  E1.EDI_CTL_NO = @EDI_CTL_NO                      " & vbNewLine _
                                        & "--             AND  E1.OUTKA_CTL_NO = @OUTKA_CTL_NO                " & vbNewLine _
                                        & "--             AND  E1.FREE_C01 = ' & Trim(argHED_REC_NO) & '      " & vbNewLine _
                                        & "             AND  E1.OUT_KB = '0'                                  " & vbNewLine _
                                        & "           ) AA1                                                   " & vbNewLine _
                                        & "          GROUP BY                                                 " & vbNewLine _
                                        & "           AA1.NRS_BR_CD,                                          " & vbNewLine _
                                        & "           AA1.EDI_CTL_NO,                                         " & vbNewLine _
                                        & "           AA1.EDI_CTL_NO_CHU,                                     " & vbNewLine _
                                        & "           AA1.OUTKA_CTL_NO,                                       " & vbNewLine _
                                        & "           AA1.OUTKA_CTL_NO_CHU,                                   " & vbNewLine _
                                        & "           AA1.FREE_C01,                                           " & vbNewLine _
                                        & "           AA1.FREE_C02,                                           " & vbNewLine _
                                        & "           AA1.OUTKO_DATE,                                         " & vbNewLine _
                                        & "           AA1.ARR_PLAN_DATE,                                      " & vbNewLine _
                                        & "           AA1.UNSO_CD,                                            " & vbNewLine _
                                        & "           AA1.UNSO_BR_CD,                                         " & vbNewLine _
                                        & "           AA1.LOT_NO                                              " & vbNewLine _
                                        & "         ) A1                                                      " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "        UNION                                                      " & vbNewLine _
                                        & "        (SELECT                                                    " & vbNewLine _
                                        & "         E2.NRS_BR_CD,                                             " & vbNewLine _
                                        & "         E2.EDI_CTL_NO,                                            " & vbNewLine _
                                        & "         E2.EDI_CTL_NO_CHU,                                        " & vbNewLine _
                                        & "         E2.OUTKA_CTL_NO,                                          " & vbNewLine _
                                        & "         E2.OUTKA_CTL_NO_CHU,                                      " & vbNewLine _
                                        & "         E2.FREE_C01,                                              " & vbNewLine _
                                        & "         E2.FREE_C02,                                              " & vbNewLine _
                                        & "         L2.OUTKO_DATE,                                            " & vbNewLine _
                                        & "         L2.ARR_PLAN_DATE,                                         " & vbNewLine _
                                        & "         F2.UNSO_CD,                                               " & vbNewLine _
                                        & "         F2.UNSO_BR_CD,                                            " & vbNewLine _
                                        & "         S2.LOT_NO,                                                " & vbNewLine _
                                        & "         SUM(S2.ALCTD_QT) AS ALCTD_QT                              " & vbNewLine _
                                        & "         FROM  $LM_TRN$..H_OUTKAEDI_M  E2                          " & vbNewLine _
                                        & "         INNER JOIN  $LM_TRN$..C_OUTKA_L  L2                       " & vbNewLine _
                                        & "          ON  E2.NRS_BR_CD = L2.NRS_BR_CD                          " & vbNewLine _
                                        & "          AND E2.OUTKA_CTL_NO = L2.OUTKA_NO_L                      " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "          LEFT JOIN  $LM_TRN$..F_UNSO_L  F2                        " & vbNewLine _
                                        & "          ON  L2.NRS_BR_CD = F2.NRS_BR_CD                          " & vbNewLine _
                                        & "          AND L2.OUTKA_NO_L = F2.INOUTKA_NO_L                      " & vbNewLine _
                                        & "          AND F2.MOTO_DATA_KB = '20'                               " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "         INNER JOIN                                                " & vbNewLine _
                                        & "          (SELECT                                                  " & vbNewLine _
                                        & "           MZ.NRS_BR_CD,                                           " & vbNewLine _
                                        & "           MZ.OUTKA_NO_L,                                          " & vbNewLine _
                                        & "           MZ.OUTKA_NO_M,                                          " & vbNewLine _
                                        & "--           MZ.EDI_CTL_NO_CHU,                                    " & vbNewLine _
                                        & "--         MZ.OUTKA_CTL_NO_CHU,                                    " & vbNewLine _
                                        & "--           MZ.CUST_GOODS_CD                                      " & vbNewLine _
                                        & "           MZ.GOODS_CD_NRS                                         " & vbNewLine _
                                        & "           FROM  $LM_TRN$..C_OUTKA_M  MZ                           " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "           LEFT JOIN  $LM_TRN$..H_OUTKAEDI_M  EZ                   " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "            ON  MZ.NRS_BR_CD = EZ.NRS_BR_CD                        " & vbNewLine _
                                        & "            AND MZ.OUTKA_NO_L = EZ.OUTKA_CTL_NO                    " & vbNewLine _
                                        & "--            AND MZ.EDI_CTL_NO_CHU = EZ.EDI_CTL_NO_CHU            " & vbNewLine _
                                        & "            AND MZ.OUTKA_NO_M = EZ.OUTKA_CTL_NO_CHU                " & vbNewLine _
                                        & "           WHERE MZ.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                        & "            AND  EZ.EDI_CTL_NO IS NULL                             " & vbNewLine _
                                        & "          ) M2                                                     " & vbNewLine _
                                        & "          ON  E2.NRS_BR_CD = M2.NRS_BR_CD                          " & vbNewLine _
                                        & "          AND E2.OUTKA_CTL_NO = M2.OUTKA_NO_L                      " & vbNewLine _
                                        & "          AND E2.NRS_GOODS_CD = M2.GOODS_CD_NRS                    " & vbNewLine _
                                        & "         INNER JOIN  $LM_TRN$..C_OUTKA_S  S2                       " & vbNewLine _
                                        & "          ON  M2.NRS_BR_CD = S2.NRS_BR_CD                          " & vbNewLine _
                                        & "          AND M2.OUTKA_NO_L = S2.OUTKA_NO_L                        " & vbNewLine _
                                        & "          AND M2.OUTKA_NO_M = S2.OUTKA_NO_M                        " & vbNewLine _
                                        & "          AND S2.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "         WHERE E2.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                        & "--          AND  E2.OUTKA_CTL_NO = @OUTKA_CTL_NO                   " & vbNewLine _
                                        & "          AND  E2.EDI_CTL_NO = @EDI_CTL_NO                         " & vbNewLine _
                                        & "--          AND  E2.FREE_C01 = ' & Trim(argHED_REC_NO) & '         " & vbNewLine _
                                        & "          AND  E2.OUT_KB = '0'                                     " & vbNewLine _
                                        & "         GROUP BY                                                  " & vbNewLine _
                                        & "          E2.NRS_BR_CD,                                            " & vbNewLine _
                                        & "          E2.EDI_CTL_NO,                                           " & vbNewLine _
                                        & "          E2.EDI_CTL_NO_CHU,                                       " & vbNewLine _
                                        & "          E2.OUTKA_CTL_NO,                                         " & vbNewLine _
                                        & "          E2.OUTKA_CTL_NO_CHU,                                     " & vbNewLine _
                                        & "          E2.FREE_C01,                                             " & vbNewLine _
                                        & "          E2.FREE_C02,                                             " & vbNewLine _
                                        & "          L2.OUTKO_DATE,                                           " & vbNewLine _
                                        & "          L2.ARR_PLAN_DATE,                                        " & vbNewLine _
                                        & "          F2.UNSO_CD,                                              " & vbNewLine _
                                        & "          F2.UNSO_BR_CD,                                           " & vbNewLine _
                                        & "          S2.LOT_NO                                                " & vbNewLine _
                                        & "        )                                                          " & vbNewLine _
                                        & "      ) A,                                                         " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "      (SELECT                                                      " & vbNewLine _
                                        & "       EDI_CTL_NO_CHU,                                             " & vbNewLine _
                                        & "       COUNT(*) AS CNT                                             " & vbNewLine _
                                        & "       FROM                                                        " & vbNewLine _
                                        & "        (SELECT DISTINCT                                           " & vbNewLine _
                                        & "         CS.EDI_CTL_NO_CHU,                                        " & vbNewLine _
                                        & "         CS.LOT_NO                                                 " & vbNewLine _
                                        & "         FROM                                                      " & vbNewLine _
                                        & "          (SELECT *                                                " & vbNewLine _
                                        & "           FROM                                                    " & vbNewLine _
                                        & "            (SELECT                                                " & vbNewLine _
                                        & "             CE1.EDI_CTL_NO_CHU,                                   " & vbNewLine _
                                        & "             ISNULL(CS1.LOT_NO,CE1.LOT_NO) AS LOT_NO               " & vbNewLine _
                                        & "             FROM  $LM_TRN$..H_OUTKAEDI_M  CE1                     " & vbNewLine _
                                        & "             LEFT JOIN  $LM_TRN$..C_OUTKA_M  CM1                   " & vbNewLine _
                                        & "              ON  CE1.NRS_BR_CD = CM1.NRS_BR_CD                    " & vbNewLine _
                                        & "              AND CE1.OUTKA_CTL_NO = CM1.OUTKA_NO_L                " & vbNewLine _
                                        & "----              AND CE1.EDI_CTL_NO_CHU = CM1.EDI_CTL_NO_CHU      " & vbNewLine _
                                        & "              AND CE1.OUTKA_CTL_NO_CHU = CM1.OUTKA_NO_M            " & vbNewLine _
                                        & "              AND CM1.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                        & "             LEFT JOIN  $LM_TRN$..C_OUTKA_S  CS1                   " & vbNewLine _
                                        & "              ON  CM1.NRS_BR_CD IS NOT NULL                        " & vbNewLine _
                                        & "              AND CM1.NRS_BR_CD = CS1.NRS_BR_CD                    " & vbNewLine _
                                        & "              AND CM1.OUTKA_NO_L = CS1.OUTKA_NO_L                  " & vbNewLine _
                                        & "              AND CM1.OUTKA_NO_M = CS1.OUTKA_NO_M                  " & vbNewLine _
                                        & "              AND CS1.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                        & "             WHERE CE1.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                        & "             AND  CE1.EDI_CTL_NO = @EDI_CTL_NO                     " & vbNewLine _
                                        & "--              AND  CE1.OUTKA_CTL_NO = @OUTKA_CTL_NO              " & vbNewLine _
                                        & "--              AND  CE1.FREE_C01 = ' & Trim(argHED_REC_NO) & '    " & vbNewLine _
                                        & "              AND  CE1.DEL_KB <> '1'                               " & vbNewLine _
                                        & "            ) CSA                                                  " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "            UNION                                                  " & vbNewLine _
                                        & "            (SELECT                                                " & vbNewLine _
                                        & "             CE2.EDI_CTL_NO_CHU,                                   " & vbNewLine _
                                        & "             CS2.LOT_NO                                            " & vbNewLine _
                                        & "             FROM  $LM_TRN$..C_OUTKA_S  CS2                        " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "             INNER JOIN  $LM_TRN$..C_OUTKA_M  CM2                  " & vbNewLine _
                                        & "              ON  CS2.NRS_BR_CD = CM2.NRS_BR_CD                    " & vbNewLine _
                                        & "              AND CS2.OUTKA_NO_L = CM2.OUTKA_NO_L                  " & vbNewLine _
                                        & "              AND CS2.OUTKA_NO_M = CM2.OUTKA_NO_M                  " & vbNewLine _
                                        & "              AND CM2.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                        & "             LEFT JOIN  $LM_TRN$..H_OUTKAEDI_M  CEZ                " & vbNewLine _
                                        & "              ON  CM2.NRS_BR_CD = CEZ.NRS_BR_CD                    " & vbNewLine _
                                        & "              AND CM2.OUTKA_NO_L = CEZ.OUTKA_CTL_NO                " & vbNewLine _
                                        & "--              AND CM2.EDI_CTL_NO_CHU = CEZ.EDI_CTL_NO_CHU        " & vbNewLine _
                                        & "              AND CM2.OUTKA_NO_M = CEZ.OUTKA_CTL_NO_CHU            " & vbNewLine _
                                        & "              AND CEZ.DEL_KB <> '1'                                " & vbNewLine _
                                        & "--              AND CEZ.FREE_C01 = ' & Trim(argHED_REC_NO) & '     " & vbNewLine _
                                        & "             INNER JOIN  $LM_TRN$..H_OUTKAEDI_M  CE2               " & vbNewLine _
                                        & "              ON  CM2.NRS_BR_CD = CE2.NRS_BR_CD                    " & vbNewLine _
                                        & "              AND CM2.OUTKA_NO_L = CE2.OUTKA_CTL_NO                " & vbNewLine _
                                        & "              AND CM2.GOODS_CD_NRS = CE2.NRS_GOODS_CD              " & vbNewLine _
                                        & "              AND CE2.DEL_KB <> '1'                                " & vbNewLine _
                                        & "--              AND CE2.FREE_C01 = ' & Trim(argHED_REC_NO) & '     " & vbNewLine _
                                        & "             WHERE CS2.NRS_BR_CD = @NRS_BR_CD                      " & vbNewLine _
                                        & "              AND  CS2.OUTKA_NO_L = @OUTKA_CTL_NO                  " & vbNewLine _
                                        & "              AND  CS2.SYS_DEL_FLG = '0'                           " & vbNewLine _
                                        & "              AND  CEZ.EDI_CTL_NO_CHU IS NULL                      " & vbNewLine _
                                        & "            )                                                      " & vbNewLine _
                                        & "          ) CS                                                     " & vbNewLine _
                                        & "         GROUP BY                                                  " & vbNewLine _
                                        & "          CS.EDI_CTL_NO_CHU,                                       " & vbNewLine _
                                        & "          CS.LOT_NO                                                " & vbNewLine _
                                        & "        ) C                                                        " & vbNewLine _
                                        & "       GROUP BY EDI_CTL_NO_CHU                                     " & vbNewLine _
                                        & "      ) B                                                          " & vbNewLine _
                                        & "                                                                   " & vbNewLine _
                                        & "     WHERE A.EDI_CTL_NO_CHU = B.EDI_CTL_NO_CHU                     " & vbNewLine _
                                        & "     ORDER BY                                                      " & vbNewLine _
                                        & "      A.FREE_C01,                                                  " & vbNewLine _
                                        & "      A.FREE_C02,                                                  " & vbNewLine _
                                        & "      A.LOT_NO                                                     " & vbNewLine

#End Region

#Region "H_OUTKAEDI_HED_CSO(SELECT)"
    ''' <summary>                                                                         
    ''' チッソEDI受信HEDのSELECT文（H_OUTKAEDI_HED_CSO）                                  
    ''' </summary>                                                                        
    ''' <remarks></remarks>                                                               
    Private Const SQL_SELECT_JISSEKISAKUSEI_EDI_RCV_HED As String = "SELECT               " & vbNewLine _
                                              & "-- :NRS_BR_CD         AS  NRS_BR_CD        " & vbNewLine _
                                              & " HED_REC_NO                              " & vbNewLine _
                                              & ",NRS_TANTO                               " & vbNewLine _
                                              & ",DATA_KIND                               " & vbNewLine _
                                              & ",SEND_CODE                               " & vbNewLine _
                                              & ",SR_DEN_NO                               " & vbNewLine _
                                              & ",HIS_NO                                  " & vbNewLine _
                                              & ",PROC_YMD                                " & vbNewLine _
                                              & ",PROC_TIME                               " & vbNewLine _
                                              & ",PROC_NO                                 " & vbNewLine _
                                              & ",SEND_DEN_YMD                            " & vbNewLine _
                                              & ",SEND_DEN_TIME                           " & vbNewLine _
                                              & ",BPID_KKN                                " & vbNewLine _
                                              & ",BPID_SUB_KKN                            " & vbNewLine _
                                              & ",BPID_HAN                                " & vbNewLine _
                                              & ",RCV_COMP_CD                             " & vbNewLine _
                                              & ",SND_COMP_CD                             " & vbNewLine _
                                              & ",RB_KBN                                  " & vbNewLine _
                                              & ",MOD_KBN                                 " & vbNewLine _
                                              & ",DATA_KBN                                " & vbNewLine _
                                              & ",FAX_KBN                                 " & vbNewLine _
                                              & ",OUTKA_REQ_KBN                           " & vbNewLine _
                                              & ",INKA_P_KBN                              " & vbNewLine _
                                              & ",OUTKA_SEPT_KBN                          " & vbNewLine _
                                              & ",EM_OUTKA_KBN                            " & vbNewLine _
                                              & ",UNSO_ROUTE_P                            " & vbNewLine _
                                              & ",UNSO_ROUTE_A                            " & vbNewLine _
                                              & ",CAR_KIND_P                              " & vbNewLine _
                                              & ",CAR_KIND_A                              " & vbNewLine _
                                              & ",CAR_NO_P                                " & vbNewLine _
                                              & ",CAR_NO_A                                " & vbNewLine _
                                              & ",COMBI_NO_P                              " & vbNewLine _
                                              & ",COMBI_NO_A                              " & vbNewLine _
                                              & ",UNSO_REQ_YN                             " & vbNewLine _
                                              & ",DEST_CHK_KBN                            " & vbNewLine _
                                              & ",INKO_DATE_P                             " & vbNewLine _
                                              & ",INKO_DATE_A                             " & vbNewLine _
                                              & ",INKO_TIME                               " & vbNewLine _
                                              & ",OUTKA_DATE_P                            " & vbNewLine _
                                              & ",OUTKA_DATE_A                            " & vbNewLine _
                                              & ",OUTKA_TIME                              " & vbNewLine _
                                              & ",CARGO_BKG_DATE_P                        " & vbNewLine _
                                              & ",CARGO_BKG_DATE_A                        " & vbNewLine _
                                              & ",ARRIVAL_DATE_P                          " & vbNewLine _
                                              & ",ARRIVAL_DATE_A                          " & vbNewLine _
                                              & ",ARRIVAL_TIME                            " & vbNewLine _
                                              & ",UNSO_DATE                               " & vbNewLine _
                                              & ",UNSO_TIME                               " & vbNewLine _
                                              & ",ZAI_RPT_DATE                            " & vbNewLine _
                                              & ",BAILER_CD                               " & vbNewLine _
                                              & ",BAILER_NM                               " & vbNewLine _
                                              & ",BAILER_BU_CD                            " & vbNewLine _
                                              & ",BAILER_BU_NM                            " & vbNewLine _
                                              & ",SHIPPER_CD                              " & vbNewLine _
                                              & ",SHIPPER_NM                              " & vbNewLine _
                                              & ",SHIPPER_BU_CD                           " & vbNewLine _
                                              & ",SHIPPER_BU_NM                           " & vbNewLine _
                                              & ",CONSIGNEE_CD                            " & vbNewLine _
                                              & ",CONSIGNEE_NM                            " & vbNewLine _
                                              & ",CONSIGNEE_BU_CD                         " & vbNewLine _
                                              & ",CONSIGNEE_BU_NM                         " & vbNewLine _
                                              & ",SOKO_PROV_CD                            " & vbNewLine _
                                              & ",SOKO_PROV_NM                            " & vbNewLine _
                                              & ",UNSO_PROV_CD                            " & vbNewLine _
                                              & ",UNSO_PROV_NM                            " & vbNewLine _
                                              & ",ACT_UNSO_CD                             " & vbNewLine _
                                              & ",UNSO_TF_KBN                             " & vbNewLine _
                                              & ",UNSO_F_KBN                              " & vbNewLine _
                                              & ",DEST_CD                                 " & vbNewLine _
                                              & ",DEST_NM                                 " & vbNewLine _
                                              & ",DEST_BU_CD                              " & vbNewLine _
                                              & ",DEST_BU_NM                              " & vbNewLine _
                                              & ",DEST_AD_CD                              " & vbNewLine _
                                              & ",DEST_AD_NM                              " & vbNewLine _
                                              & ",DEST_YB_NO                              " & vbNewLine _
                                              & ",DEST_TEL_NO                             " & vbNewLine _
                                              & ",DEST_FAX_NO                             " & vbNewLine _
                                              & ",DELIVERY_NM                             " & vbNewLine _
                                              & ",DELIVERY_SAGYO                          " & vbNewLine _
                                              & ",ORDER_NO                                " & vbNewLine _
                                              & ",JYUCHU_NO                               " & vbNewLine _
                                              & ",PRI_SHOP_CD                             " & vbNewLine _
                                              & ",PRI_SHOP_NM                             " & vbNewLine _
                                              & ",INV_REM_NM                              " & vbNewLine _
                                              & ",INV_REM_KANA                            " & vbNewLine _
                                              & ",DEN_NO                                  " & vbNewLine _
                                              & ",MEI_DEN_NO                              " & vbNewLine _
                                              & ",OUTKA_POSI_CD                           " & vbNewLine _
                                              & ",OUTKA_POSI_NM                           " & vbNewLine _
                                              & ",OUTKA_POSI_BU_CD_PA                     " & vbNewLine _
                                              & ",OUTKA_POSI_BU_CD_NAIBU                  " & vbNewLine _
                                              & ",OUTKA_POSI_BU_NM_PA                     " & vbNewLine _
                                              & ",OUTKA_POSI_BU_NM_NAIBU                  " & vbNewLine _
                                              & ",OUTKA_POSI_AD_CD_PA                     " & vbNewLine _
                                              & ",OUTKA_POSI_AD_NM_PA                     " & vbNewLine _
                                              & ",OUTKA_POSI_TEL_NO_PA                    " & vbNewLine _
                                              & ",OUTKA_POSI_FAX_NO_PA                    " & vbNewLine _
                                              & ",UNSO_JURYO                              " & vbNewLine _
                                              & ",UNSO_JURYO_FLG                          " & vbNewLine _
                                              & ",UNIT_LOAD_CD                            " & vbNewLine _
                                              & ",UNIT_LOAD_SU                            " & vbNewLine _
                                              & ",REMARK                                  " & vbNewLine _
                                              & ",REMARK_KANA                             " & vbNewLine _
                                              & ",HARAI_KBN                               " & vbNewLine _
                                              & ",DATA_TYPE                               " & vbNewLine _
                                              & ",RTN_FLG                                 " & vbNewLine _
                                              & ",SND_CANCEL_FLG                          " & vbNewLine _
                                              & ",OLD_DATA_FLG                            " & vbNewLine _
                                              & ",PRINT_FLG                               " & vbNewLine _
                                              & ",PRINT_NO                                " & vbNewLine _
                                              & ",NRS_SYS_FLG                             " & vbNewLine _
                                              & ",OLD_SYS_FLG                             " & vbNewLine _
                                              & ",RTN_FILE_DATE                           " & vbNewLine _
                                              & ",RTN_FILE_TIME                           " & vbNewLine _
                                              & ",CRT_DATE                                " & vbNewLine _
                                              & ",CRT_TIME                                " & vbNewLine _
                                              & ",UPD_USER                                " & vbNewLine _
                                              & ",UPD_DATE                                " & vbNewLine _
                                              & " FROM $ORACLE$.CHISSO_HED                " & vbNewLine _
                                              & "WHERE   HED_REC_NO  = :HED_REC_NO        " & vbNewLine

#End Region

#End Region

#Region "実績作成処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                    " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                    " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                    " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                        " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                        " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                        " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                       " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                      " & vbNewLine
#End Region

    'チッソの場合、FREE_C09更新が必要な為通常版は使用しない。
    '#Region "H_OUTKAEDI_M"
    '    ''' <summary>
    '    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
    '                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                    " & vbNewLine _
    '                                              & ",JISSEKI_USER      = @JISSEKI_USER                    " & vbNewLine _
    '                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                    " & vbNewLine _
    '                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                    " & vbNewLine _
    '                                              & ",UPD_USER          = @UPD_USER                        " & vbNewLine _
    '                                              & ",UPD_DATE          = @UPD_DATE                        " & vbNewLine _
    '                                              & ",UPD_TIME          = @UPD_TIME                        " & vbNewLine _
    '                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
    '                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
    '                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
    '                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
    '                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                       " & vbNewLine _
    '                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                      " & vbNewLine _
    '                                              & "AND JISSEKI_FLAG   = '0'                              " & vbNewLine _
    '                                              & "AND OUT_KB         = '0'                              " & vbNewLine

    '#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_M_CHISSO As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " FREE_C09          = @FREE_C02                        " & vbNewLine _
                                              & ",JISSEKI_FLAG      = @JISSEKI_FLAG                    " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                    " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                    " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                        " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                        " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                        " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                    " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                    " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                       " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                      " & vbNewLine _
                                              & "AND JISSEKI_FLAG   = '0'                              " & vbNewLine _
                                              & "AND OUT_KB         = '0'                              " & vbNewLine

#End Region

#Region "CHISSO_HED(ORACLE)"
    ''' <summary>
    ''' チッソEDI受信HEDのUPDATE文CHISSO_HED(ORACLE)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_HED As String = "UPDATE $ORACLE$.CHISSO_HED SET           " & vbNewLine _
                                              & " HED_REC_NO      = :HED_REC_NO                               " & vbNewLine _
                                              & ",NRS_TANTO      = :NRS_TANTO                                 " & vbNewLine _
                                              & ",DATA_KIND      = :DATA_KIND                                 " & vbNewLine _
                                              & ",SEND_CODE      = :SEND_CODE                                 " & vbNewLine _
                                              & ",SR_DEN_NO      = :SR_DEN_NO                                 " & vbNewLine _
                                              & ",HIS_NO      = :HIS_NO                                       " & vbNewLine _
                                              & ",PROC_YMD      = :PROC_YMD                                   " & vbNewLine _
                                              & ",PROC_TIME      = :PROC_TIME                                 " & vbNewLine _
                                              & ",PROC_NO      = :PROC_NO                                     " & vbNewLine _
                                              & ",SEND_DEN_YMD      = :SEND_DEN_YMD                           " & vbNewLine _
                                              & ",SEND_DEN_TIME      = :SEND_DEN_TIME                         " & vbNewLine _
                                              & ",BPID_KKN      = :BPID_KKN                                   " & vbNewLine _
                                              & ",BPID_SUB_KKN      = :BPID_SUB_KKN                           " & vbNewLine _
                                              & ",BPID_HAN      = :BPID_HAN                                   " & vbNewLine _
                                              & ",RCV_COMP_CD      = :RCV_COMP_CD                             " & vbNewLine _
                                              & ",SND_COMP_CD      = :SND_COMP_CD                             " & vbNewLine _
                                              & ",RB_KBN      = :RB_KBN                                       " & vbNewLine _
                                              & ",MOD_KBN      = :MOD_KBN                                     " & vbNewLine _
                                              & ",DATA_KBN      = :DATA_KBN                                   " & vbNewLine _
                                              & ",FAX_KBN      = :FAX_KBN                                     " & vbNewLine _
                                              & ",OUTKA_REQ_KBN      = :OUTKA_REQ_KBN                         " & vbNewLine _
                                              & ",INKA_P_KBN      = :INKA_P_KBN                               " & vbNewLine _
                                              & ",OUTKA_SEPT_KBN      = :OUTKA_SEPT_KBN                       " & vbNewLine _
                                              & ",EM_OUTKA_KBN      = :EM_OUTKA_KBN                           " & vbNewLine _
                                              & ",UNSO_ROUTE_P      = :UNSO_ROUTE_P                           " & vbNewLine _
                                              & ",UNSO_ROUTE_A      = :UNSO_ROUTE_A                           " & vbNewLine _
                                              & ",CAR_KIND_P      = :CAR_KIND_P                               " & vbNewLine _
                                              & ",CAR_KIND_A      = :CAR_KIND_A                               " & vbNewLine _
                                              & ",CAR_NO_P      = :CAR_NO_P                                   " & vbNewLine _
                                              & ",CAR_NO_A      = :CAR_NO_A                                   " & vbNewLine _
                                              & ",COMBI_NO_P      = :COMBI_NO_P                               " & vbNewLine _
                                              & ",COMBI_NO_A      = :COMBI_NO_A                               " & vbNewLine _
                                              & ",UNSO_REQ_YN      = :UNSO_REQ_YN                             " & vbNewLine _
                                              & ",DEST_CHK_KBN      = :DEST_CHK_KBN                           " & vbNewLine _
                                              & ",INKO_DATE_P      = :INKO_DATE_P                             " & vbNewLine _
                                              & ",INKO_DATE_A      = :INKO_DATE_A                             " & vbNewLine _
                                              & ",INKO_TIME      = :INKO_TIME                                 " & vbNewLine _
                                              & ",OUTKA_DATE_P      = :OUTKA_DATE_P                           " & vbNewLine _
                                              & ",OUTKA_DATE_A      = :OUTKA_DATE_A                           " & vbNewLine _
                                              & ",OUTKA_TIME      = :OUTKA_TIME                               " & vbNewLine _
                                              & ",CARGO_BKG_DATE_P      = :CARGO_BKG_DATE_P                   " & vbNewLine _
                                              & ",CARGO_BKG_DATE_A      = :CARGO_BKG_DATE_A                   " & vbNewLine _
                                              & ",ARRIVAL_DATE_P      = :ARRIVAL_DATE_P                       " & vbNewLine _
                                              & ",ARRIVAL_DATE_A      = :ARRIVAL_DATE_A                       " & vbNewLine _
                                              & ",ARRIVAL_TIME      = :ARRIVAL_TIME                           " & vbNewLine _
                                              & ",UNSO_DATE      = :UNSO_DATE                                 " & vbNewLine _
                                              & ",UNSO_TIME      = :UNSO_TIME                                 " & vbNewLine _
                                              & ",ZAI_RPT_DATE      = :ZAI_RPT_DATE                           " & vbNewLine _
                                              & ",BAILER_CD      = :BAILER_CD                                 " & vbNewLine _
                                              & ",BAILER_NM      = :BAILER_NM                                 " & vbNewLine _
                                              & ",BAILER_BU_CD      = :BAILER_BU_CD                           " & vbNewLine _
                                              & ",BAILER_BU_NM      = :BAILER_BU_NM                           " & vbNewLine _
                                              & ",SHIPPER_CD      = :SHIPPER_CD                               " & vbNewLine _
                                              & ",SHIPPER_NM      = :SHIPPER_NM                               " & vbNewLine _
                                              & ",SHIPPER_BU_CD      = :SHIPPER_BU_CD                         " & vbNewLine _
                                              & ",SHIPPER_BU_NM      = :SHIPPER_BU_NM                         " & vbNewLine _
                                              & ",CONSIGNEE_CD      = :CONSIGNEE_CD                           " & vbNewLine _
                                              & ",CONSIGNEE_NM      = :CONSIGNEE_NM                           " & vbNewLine _
                                              & ",CONSIGNEE_BU_CD      = :CONSIGNEE_BU_CD                     " & vbNewLine _
                                              & ",CONSIGNEE_BU_NM      = :CONSIGNEE_BU_NM                     " & vbNewLine _
                                              & ",SOKO_PROV_CD      = :SOKO_PROV_CD                           " & vbNewLine _
                                              & ",SOKO_PROV_NM      = :SOKO_PROV_NM                           " & vbNewLine _
                                              & ",UNSO_PROV_CD      = :UNSO_PROV_CD                           " & vbNewLine _
                                              & ",UNSO_PROV_NM      = :UNSO_PROV_NM                           " & vbNewLine _
                                              & ",ACT_UNSO_CD      = :ACT_UNSO_CD                             " & vbNewLine _
                                              & ",UNSO_TF_KBN      = :UNSO_TF_KBN                             " & vbNewLine _
                                              & ",UNSO_F_KBN      = :UNSO_F_KBN                               " & vbNewLine _
                                              & ",DEST_CD      = :DEST_CD                                     " & vbNewLine _
                                              & ",DEST_NM      = :DEST_NM                                     " & vbNewLine _
                                              & ",DEST_BU_CD      = :DEST_BU_CD                               " & vbNewLine _
                                              & ",DEST_BU_NM      = :DEST_BU_NM                               " & vbNewLine _
                                              & ",DEST_AD_CD      = :DEST_AD_CD                               " & vbNewLine _
                                              & ",DEST_AD_NM      = :DEST_AD_NM                               " & vbNewLine _
                                              & ",DEST_YB_NO      = :DEST_YB_NO                               " & vbNewLine _
                                              & ",DEST_TEL_NO      = :DEST_TEL_NO                             " & vbNewLine _
                                              & ",DEST_FAX_NO      = :DEST_FAX_NO                             " & vbNewLine _
                                              & ",DELIVERY_NM      = :DELIVERY_NM                             " & vbNewLine _
                                              & ",DELIVERY_SAGYO      = :DELIVERY_SAGYO                       " & vbNewLine _
                                              & ",ORDER_NO      = :ORDER_NO                                   " & vbNewLine _
                                              & ",JYUCHU_NO      = :JYUCHU_NO                                 " & vbNewLine _
                                              & ",PRI_SHOP_CD      = :PRI_SHOP_CD                             " & vbNewLine _
                                              & ",PRI_SHOP_NM      = :PRI_SHOP_NM                             " & vbNewLine _
                                              & ",INV_REM_NM      = :INV_REM_NM                               " & vbNewLine _
                                              & ",INV_REM_KANA      = :INV_REM_KANA                           " & vbNewLine _
                                              & ",DEN_NO      = :DEN_NO                                       " & vbNewLine _
                                              & ",MEI_DEN_NO      = :MEI_DEN_NO                               " & vbNewLine _
                                              & ",OUTKA_POSI_CD      = :OUTKA_POSI_CD                         " & vbNewLine _
                                              & ",OUTKA_POSI_NM      = :OUTKA_POSI_NM                         " & vbNewLine _
                                              & ",OUTKA_POSI_BU_CD_PA      = :OUTKA_POSI_BU_CD_PA             " & vbNewLine _
                                              & ",OUTKA_POSI_BU_CD_NAIBU      = :OUTKA_POSI_BU_CD_NAIBU       " & vbNewLine _
                                              & ",OUTKA_POSI_BU_NM_PA      = :OUTKA_POSI_BU_NM_PA             " & vbNewLine _
                                              & ",OUTKA_POSI_BU_NM_NAIBU      = :OUTKA_POSI_BU_NM_NAIBU       " & vbNewLine _
                                              & ",OUTKA_POSI_AD_CD_PA      = :OUTKA_POSI_AD_CD_PA             " & vbNewLine _
                                              & ",OUTKA_POSI_AD_NM_PA      = :OUTKA_POSI_AD_NM_PA             " & vbNewLine _
                                              & ",OUTKA_POSI_TEL_NO_PA      = :OUTKA_POSI_TEL_NO_PA           " & vbNewLine _
                                              & ",OUTKA_POSI_FAX_NO_PA      = :OUTKA_POSI_FAX_NO_PA           " & vbNewLine _
                                              & ",UNSO_JURYO      = :UNSO_JURYO                               " & vbNewLine _
                                              & ",UNSO_JURYO_FLG      = :UNSO_JURYO_FLG                       " & vbNewLine _
                                              & ",UNIT_LOAD_CD      = :UNIT_LOAD_CD                           " & vbNewLine _
                                              & ",UNIT_LOAD_SU      = :UNIT_LOAD_SU                           " & vbNewLine _
                                              & ",REMARK      = :REMARK                                       " & vbNewLine _
                                              & ",REMARK_KANA      = :REMARK_KANA                             " & vbNewLine _
                                              & ",HARAI_KBN      = :HARAI_KBN                                 " & vbNewLine _
                                              & ",DATA_TYPE      = :DATA_TYPE                                 " & vbNewLine _
                                              & ",RTN_FLG      = :RTN_FLG                                     " & vbNewLine _
                                              & ",SND_CANCEL_FLG      = :SND_CANCEL_FLG                       " & vbNewLine _
                                              & ",OLD_DATA_FLG      = :OLD_DATA_FLG                           " & vbNewLine _
                                              & ",PRINT_FLG      = :PRINT_FLG                                 " & vbNewLine _
                                              & ",PRINT_NO      = :PRINT_NO                                   " & vbNewLine _
                                              & ",NRS_SYS_FLG      = :NRS_SYS_FLG                             " & vbNewLine _
                                              & ",OLD_SYS_FLG      = :OLD_SYS_FLG                             " & vbNewLine _
                                              & ",RTN_FILE_DATE      = :RTN_FILE_DATE                         " & vbNewLine _
                                              & ",RTN_FILE_TIME      = :RTN_FILE_TIME                         " & vbNewLine _
                                              & ",CRT_DATE      = :CRT_DATE                                   " & vbNewLine _
                                              & ",CRT_TIME      = :CRT_TIME                                   " & vbNewLine _
                                              & ",UPD_USER      = :UPD_USER                                   " & vbNewLine _
                                              & ",UPD_DATE      = :UPD_DATE                                   " & vbNewLine _
                                              & "WHERE   HED_REC_NO  = :HED_REC_NO                            " & vbNewLine

#End Region

#Region "CHISSO_MEI(ORACLE)"
    ''' <summary>
    ''' チッソEDI受信HEDのUPDATE文CHISSO_MEI(ORACLE)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_DTL_CNT1 As String = "UPDATE $ORACLE$.CHISSO_MEI SET      " & vbNewLine _
                                         & "-- SR_DEN_NO          = :SR_DEN_NO                             " & vbNewLine _
                                         & "--,HIS_NO             = :HIS_NO                                " & vbNewLine _
                                         & "--,MEI_NO_P           = :MEI_NO_P                              " & vbNewLine _
                                         & "--,MEI_NO_A           = :MEI_NO_A                              " & vbNewLine _
                                         & "--,JYUCHU_GOODS_CD    = :JYUCHU_GOODS_CD                       " & vbNewLine _
                                         & "--,GOODS_NM           = :GOODS_NM                              " & vbNewLine _
                                         & "--,GOODS_KANA1        = :GOODS_KANA1                           " & vbNewLine _
                                         & "--,GOODS_KANA2        = :GOODS_KANA2                           " & vbNewLine _
                                         & "--,NISUGATA_CD        = :NISUGATA_CD                           " & vbNewLine _
                                         & "--,IRISUU             = :IRISUU                                " & vbNewLine _
                                         & "--,LOT_NO_P           = :LOT_NO_P                              " & vbNewLine _
                                         & "   LOT_NO_A           = :LOT_NO_A                              " & vbNewLine _
                                         & "--,SURY_TANI_CD       = :SURY_TANI_CD                          " & vbNewLine _
                                         & "--,SURY_REQ           = :SURY_REQ                              " & vbNewLine _
                                         & ",SURY_RPT           = :SURY_RPT                              " & vbNewLine _
                                         & "--,MEI_REM1           = :MEI_REM1                              " & vbNewLine _
                                         & "--,MEI_REM2           = :MEI_REM2                              " & vbNewLine _
                                         & "--,CRT_DATE           = :CRT_DATE                              " & vbNewLine _
                                         & ",UPD_USER           = :UPD_USER                              " & vbNewLine _
                                         & ",UPD_DATE           = :UPD_DATE                              " & vbNewLine _
                                         & "WHERE   HED_REC_NO  = :HED_REC_NO                            " & vbNewLine _
                                         & "AND MEI_REC_NO      = :MEI_REC_NO                            " & vbNewLine

    Private Const SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_DTL_CNT2 As String = "UPDATE $ORACLE$.CHISSO_MEI SET      " & vbNewLine _
                                     & "-- SR_DEN_NO          = :SR_DEN_NO                             " & vbNewLine _
                                     & "--,HIS_NO             = :HIS_NO                                " & vbNewLine _
                                     & "--,MEI_NO_P           = :MEI_NO_P                              " & vbNewLine _
                                     & "--,MEI_NO_A           = :MEI_NO_A                              " & vbNewLine _
                                     & "--,JYUCHU_GOODS_CD    = :JYUCHU_GOODS_CD                       " & vbNewLine _
                                     & "--,GOODS_NM           = :GOODS_NM                              " & vbNewLine _
                                     & "--,GOODS_KANA1        = :GOODS_KANA1                           " & vbNewLine _
                                     & "--,GOODS_KANA2        = :GOODS_KANA2                           " & vbNewLine _
                                     & "--,NISUGATA_CD        = :NISUGATA_CD                           " & vbNewLine _
                                     & "--,IRISUU             = :IRISUU                                " & vbNewLine _
                                     & "--,LOT_NO_P           = :LOT_NO_P                              " & vbNewLine _
                                     & "   LOT_NO_A           = :LOT_NO_A                              " & vbNewLine _
                                     & "--,SURY_TANI_CD       = :SURY_TANI_CD                          " & vbNewLine _
                                     & ",SURY_REQ           = :SURY_REQ                              " & vbNewLine _
                                     & ",SURY_RPT           = :SURY_RPT                              " & vbNewLine _
                                     & "--,MEI_REM1           = :MEI_REM1                              " & vbNewLine _
                                     & "--,MEI_REM2           = :MEI_REM2                              " & vbNewLine _
                                     & "--,CRT_DATE           = :CRT_DATE                              " & vbNewLine _
                                     & ",UPD_USER           = :UPD_USER                              " & vbNewLine _
                                     & ",UPD_DATE           = :UPD_DATE                              " & vbNewLine _
                                     & "WHERE   HED_REC_NO  = :HED_REC_NO                            " & vbNewLine _
                                     & "AND MEI_REC_NO      = :MEI_REC_NO                            " & vbNewLine


#End Region

#Region "CHISSO_MEI(ORACLE)"

    ''' <summary>
    ''' チッソEDI受信DTLのINSERT CHISSO_MEI(ORACLE)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_JISSEKISAKUSEI_EDI_RCV_DTL As String = "INSERT INTO                       " & vbNewLine _
                                         & "$ORACLE$.CHISSO_MEI                                        " & vbNewLine _
                                         & "(                                                          " & vbNewLine _
                                         & " HED_REC_NO                                                " & vbNewLine _
                                         & ",MEI_REC_NO                                                " & vbNewLine _
                                         & ",SR_DEN_NO                                                 " & vbNewLine _
                                         & ",HIS_NO                                                    " & vbNewLine _
                                         & ",MEI_NO_P                                                  " & vbNewLine _
                                         & ",MEI_NO_A                                                  " & vbNewLine _
                                         & ",JYUCHU_GOODS_CD                                           " & vbNewLine _
                                         & ",GOODS_NM                                                  " & vbNewLine _
                                         & ",GOODS_KANA1                                               " & vbNewLine _
                                         & ",GOODS_KANA2                                               " & vbNewLine _
                                         & ",NISUGATA_CD                                               " & vbNewLine _
                                         & ",IRISUU                                                    " & vbNewLine _
                                         & ",LOT_NO_P                                                  " & vbNewLine _
                                         & ",LOT_NO_A                                                  " & vbNewLine _
                                         & ",SURY_TANI_CD                                              " & vbNewLine _
                                         & ",SURY_REQ                                                  " & vbNewLine _
                                         & ",SURY_RPT                                                  " & vbNewLine _
                                         & ",MEI_REM1                                                  " & vbNewLine _
                                         & ",MEI_REM2                                                  " & vbNewLine _
                                         & ",CRT_DATE                                                  " & vbNewLine _
                                         & ",UPD_USER                                                  " & vbNewLine _
                                         & ",UPD_DATE                                                  " & vbNewLine _
                                         & ")VALUES(                                                   " & vbNewLine _
                                         & " :HED_REC_NO                                               " & vbNewLine _
                                         & ",:MEI_REC_NO                                               " & vbNewLine _
                                         & ",:SR_DEN_NO                                                " & vbNewLine _
                                         & ",:HIS_NO                                                   " & vbNewLine _
                                         & ",:MEI_NO_P                                                 " & vbNewLine _
                                         & ",:MEI_NO_A                                                 " & vbNewLine _
                                         & ",:JYUCHU_GOODS_CD                                          " & vbNewLine _
                                         & ",:GOODS_NM                                                 " & vbNewLine _
                                         & ",:GOODS_KANA1                                              " & vbNewLine _
                                         & ",:GOODS_KANA2                                              " & vbNewLine _
                                         & ",:NISUGATA_CD                                              " & vbNewLine _
                                         & ",:IRISUU                                                   " & vbNewLine _
                                         & ",:LOT_NO_P                                                 " & vbNewLine _
                                         & ",:LOT_NO_A                                                 " & vbNewLine _
                                         & ",:SURY_TANI_CD                                             " & vbNewLine _
                                         & ",:SURY_REQ                                                 " & vbNewLine _
                                         & ",:SURY_RPT                                                 " & vbNewLine _
                                         & ",:MEI_REM1                                                 " & vbNewLine _
                                         & ",:MEI_REM2                                                 " & vbNewLine _
                                         & ",:CRT_DATE                                                 " & vbNewLine _
                                         & ",:UPD_USER                                                 " & vbNewLine _
                                         & ",:UPD_DATE                                                 " & vbNewLine _
                                         & ")                                                          " & vbNewLine

#End Region

#Region "C_OUTKA_L"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET       " & vbNewLine _
                                              & " OUTKA_STATE_KB    = @OUTKA_STATE_KB                 " & vbNewLine _
                                              & ",HOKOKU_DATE       = @HOKOKU_DATE                    " & vbNewLine _
                                              & ",HOU_USER          = @HOU_USER                       " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                   " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                   " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                   " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                   " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                      " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_NO_L                     " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                           " & vbNewLine
#End Region

#End Region

#Region "実績作成処理 同一まとめ番号データ取得用SQL"

    ''' <summary>
    ''' 同一まとめ番号データ取得用SQL
    ''' </summary>
    ''' <remarks>ダウケミは現状まとめ処理はないが、切り替えた時の為に記載</remarks>
    Private Const SQL_SELECT_DATA_MATOME As String = " SELECT                                                              " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD                             AS NRS_BR_CD            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDI_CTL_NO                            AS EDI_CTL_NO           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_DATE                          AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_TIME                          AS SYS_UPD_TIME         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.OUTKA_CTL_NO                          AS OUTKA_CTL_NO         " & vbNewLine _
                                            & ",C_OUTKA_L.SYS_UPD_DATE                             AS OUTKA_SYS_UPD_DATE   " & vbNewLine _
                                            & ",C_OUTKA_L.SYS_UPD_TIME                             AS OUTKA_SYS_UPD_TIME   " & vbNewLine _
                                            & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_DATE                    AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                            & ",H_OUTKAEDI_HED_SFJ.SYS_UPD_TIME                    AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                            & ",C_OUTKA_L.SYS_DEL_FLG                              AS OUTKA_DEL_KB         " & vbNewLine _
                                            & " FROM                                                                       " & vbNewLine _
                                            & " $LM_TRN$..H_OUTKAEDI_L                    H_OUTKAEDI_L                     " & vbNewLine _
                                            & " LEFT JOIN                                                                  " & vbNewLine _
                                            & " $LM_TRN$..C_OUTKA_L                       C_OUTKA_L                        " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                                " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                            " & vbNewLine _
                                            & " LEFT JOIN                                                                  " & vbNewLine _
                                            & " $LM_TRN$..H_INOUTKAEDI_HED_DOW                                             " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =H_INOUTKAEDI_HED_DOW.NRS_BR_CD                     " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.EDI_CTL_NO =H_INOUTKAEDI_HED_DOW.EDI_CTL_NO                   " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_INOUTKAEDI_HED_DOW.INOUT_KB = '0'                                        " & vbNewLine _
                                            & " INNER JOIN                                                                 " & vbNewLine _
                                            & " $LM_MST$..M_EDI_CUST                       M_EDI_CUST                      " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                                      " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " M_EDI_CUST.INOUT_KB = '0'                                                  " & vbNewLine _
                                            & " WHERE                                                                      " & vbNewLine _
                                            & " SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) = @MATOME_NO                          " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " (((M_EDI_CUST.FLAG_01 IN ('1','2')                                         " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '0'                                                " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.OUTKA_STATE_KB >= '60')                                          " & vbNewLine _
                                            & " OR                                                                         " & vbNewLine _
                                            & " (M_EDI_CUST.FLAG_01 = '2'                                                  " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " (H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                            " & vbNewLine _
                                            & " OR                                                                         " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '1'))                                              " & vbNewLine _
                                            & " OR                                                                         " & vbNewLine _
                                            & " (M_EDI_CUST.FLAG_01 = '4'                                                  " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '0'))                                              " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " (H_OUTKAEDI_L.JISSEKI_FLAG = '0'))                                         " & vbNewLine


#End Region

#Region "SELECT_M_DEST"

    Private Const SQL_SELECT_M_DEST As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                      AS MST_CNT     " & vbNewLine _
                                     & ",NRS_BR_CD			AS NRS_BR_CD	" & vbNewLine _
                                     & ",CUST_CD_L			AS CUST_CD_L				     " & vbNewLine _
                                     & ",DEST_CD			AS DEST_CD				     " & vbNewLine _
                                     & ",EDI_CD				AS EDI_CD				     " & vbNewLine _
                                     & ",DEST_NM			AS DEST_NM				     " & vbNewLine _
                                     & ",ZIP				AS ZIP				     " & vbNewLine _
                                     & ",AD_1				AS AD_1				     " & vbNewLine _
                                     & ",AD_2				AS AD_2				     " & vbNewLine _
                                     & ",AD_3				AS AD_3				     " & vbNewLine _
                                     & ",CUST_DEST_CD		AS CUST_DEST_CD			" & vbNewLine _
                                     & ",SALES_CD			AS SALES_CD			" & vbNewLine _
                                     & ",SP_NHS_KB			AS SP_NHS_KB			" & vbNewLine _
                                     & ",COA_YN				AS COA_YN				" & vbNewLine _
                                     & ",SP_UNSO_CD			AS SP_UNSO_CD			" & vbNewLine _
                                     & ",SP_UNSO_BR_CD			AS SP_UNSO_BR_CD			" & vbNewLine _
                                     & ",DELI_ATT			AS DELI_ATT			" & vbNewLine _
                                     & ",CARGO_TIME_LIMIT		AS CARGO_TIME_LIMIT	" & vbNewLine _
                                     & ",LARGE_CAR_YN			AS LARGE_CAR_YN			" & vbNewLine _
                                     & ",TEL				AS TEL				     " & vbNewLine _
                                     & ",FAX				AS FAX				     " & vbNewLine _
                                     & ",UNCHIN_SEIQTO_CD		AS UNCHIN_SEIQTO_CD				     " & vbNewLine _
                                     & ",JIS				AS JIS				     " & vbNewLine _
                                     & ",KYORI				AS KYORI				     " & vbNewLine _
                                     & ",PICK_KB			AS PICK_KB				     " & vbNewLine _
                                     & ",BIN_KB				AS BIN_KB				     " & vbNewLine _
                                     & ",MOTO_CHAKU_KB			AS MOTO_CHAKU_KB				     " & vbNewLine _
                                     & ",URIAGE_CD			AS URIAGE_CD				     " & vbNewLine _
                                     & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE    " & vbNewLine _
                                     & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME    " & vbNewLine _
                                     & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID    " & vbNewLine _
                                     & ",SYS_ENT_USER                   AS  SYS_ENT_USER    " & vbNewLine _
                                     & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE    " & vbNewLine _
                                     & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME    " & vbNewLine _
                                     & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID    " & vbNewLine _
                                     & ",SYS_UPD_USER                   AS  SYS_UPD_USER    " & vbNewLine _
                                     & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_DEST                       M_DEST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_DEST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_DEST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine

#End Region

#Region "GROUP_BY_M_DEST"

    Private Const SQL_GROUP_BY_M_DEST As String = " GROUP BY                     " & vbNewLine _
                                     & "NRS_BR_CD                                " & vbNewLine _
                                     & ",CUST_CD_L                               " & vbNewLine _
                                     & ",DEST_CD                                 " & vbNewLine _
                                     & ",EDI_CD                                  " & vbNewLine _
                                     & ",DEST_NM                                 " & vbNewLine _
                                     & ",ZIP                                     " & vbNewLine _
                                     & ",AD_1                                    " & vbNewLine _
                                     & ",AD_2                                    " & vbNewLine _
                                     & ",AD_3                                    " & vbNewLine _
                                     & ",CUST_DEST_CD                            " & vbNewLine _
                                     & ",SALES_CD                                " & vbNewLine _
                                     & ",SP_NHS_KB                               " & vbNewLine _
                                     & ",COA_YN                                  " & vbNewLine _
                                     & ",SP_UNSO_CD                              " & vbNewLine _
                                     & ",SP_UNSO_BR_CD                           " & vbNewLine _
                                     & ",DELI_ATT                                " & vbNewLine _
                                     & ",CARGO_TIME_LIMIT                        " & vbNewLine _
                                     & ",LARGE_CAR_YN                            " & vbNewLine _
                                     & ",TEL                                     " & vbNewLine _
                                     & ",FAX                                     " & vbNewLine _
                                     & ",UNCHIN_SEIQTO_CD                        " & vbNewLine _
                                     & ",JIS                                     " & vbNewLine _
                                     & ",KYORI                                   " & vbNewLine _
                                     & ",PICK_KB                                 " & vbNewLine _
                                     & ",BIN_KB                                  " & vbNewLine _
                                     & ",MOTO_CHAKU_KB                           " & vbNewLine _
                                     & ",URIAGE_CD                               " & vbNewLine _
                                     & ",SYS_ENT_DATE                            " & vbNewLine _
                                     & ",SYS_ENT_TIME                            " & vbNewLine _
                                     & ",SYS_ENT_PGID                            " & vbNewLine _
                                     & ",SYS_ENT_USER                            " & vbNewLine _
                                     & ",SYS_UPD_DATE                            " & vbNewLine _
                                     & ",SYS_UPD_TIME                            " & vbNewLine _
                                     & ",SYS_UPD_PGID                            " & vbNewLine _
                                     & ",SYS_UPD_USER                            " & vbNewLine _
                                     & ",SYS_DEL_FLG                             " & vbNewLine

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
    ''' ORDER BY句作成
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSqlOrderBy As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "Method"

#Region "出荷登録処理"

    ''' <summary>
    ''' EDI出荷データL取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.L_DEF_SQL_SELECT_DATA)      'SQL構築
        Call Me.setSQLSelectDataExists()                           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectEdiL", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_KB", "OUTKA_KB")
        map.Add("SYUBETU_KB", "SYUBETU_KB")
        map.Add("NAIGAI_KB", "NAIGAI_KB")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("OUTKAHOKOKU_YN", "OUTKAHOKOKU_YN")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")
        map.Add("TOUKI_HOKAN_YN", "TOUKI_HOKAN_YN")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_CD_M", "SHIP_CD_M")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("SHIP_NM_M", "SHIP_NM_M")
        map.Add("EDI_DEST_CD", "EDI_DEST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_AD_4", "DEST_AD_4")
        map.Add("DEST_AD_5", "DEST_AD_5")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_FAX", "DEST_FAX")
        map.Add("DEST_MAIL", "DEST_MAIL")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("UNSO_MOTO_KB", "UNSO_MOTO_KB")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SYARYO_KB", "SYARYO_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("UNCHIN_YN", "UNCHIN_YN")
        map.Add("NIYAKU_YN", "NIYAKU_YN")
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
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
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
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_OUTKAEDI_L")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_OUTKAEDI_L").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' EDI出荷データM取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データMテーブル更新対象データ結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.M_DEF_SQL_SELECT_DATA)      'SQL構築(データ抽出用)

        Call Me.setSQLSelectDataExists()                           '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectEdiM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKA_HASU", "OUTKA_HASU")
        map.Add("OUTKA_QT", "OUTKA_QT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("OUTKA_TTL_QT", "OUTKA_TTL_QT")
        map.Add("KB_UT", "KB_UT")
        map.Add("QT_UT", "QT_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("REMARK", "REMARK")
        map.Add("OUT_KB", "OUT_KB")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SET_KB", "SET_KB")
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
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("SCM_CTL_NO_M", "SCM_CTL_NO_M")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SAGYO_KB_1", "SAGYO_KB_1")
        map.Add("SAGYO_KB_2", "SAGYO_KB_2")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_OUTKAEDI_M")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_OUTKAEDI_M").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

    '#Region "存在チェック(出荷登録用)"

    '    ''' <summary>
    '    ''' 件数取得処理(届先マスタ)
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function SelectDataNcgoMdest(ByVal ds As DataSet) As DataSet

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

    '        'INTableの条件rowの格納
    '        Me._Row = dt.Rows(0)

    '        'SQL作成
    '        Me._StrSql.Append(LMH030DAC107.SQL_SELECT_COUNT_M_DEST)

    '        If String.IsNullOrEmpty(dt.Rows(0)("DEST_CD").ToString()) = True Then
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append("AND M_DEST.EDI_CD = @EDI_DEST_CD")
    '        Else
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append("AND M_DEST.DEST_CD = @DEST_CD")
    '        End If

    '        Me._SqlPrmList = New ArrayList()

    '        'パラメータ設定

    '        Call Me.SetMdestParameter(dt, 0)
    '        Me._StrSql.Append(LMH030DAC107.SQL_GROUP_BY_M_DEST_COUNT)

    '        'スキーマ設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectDataNcgoMdest", cmd)

    '        'SQLの発行
    '        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '        ''届先件数の設定
    '        Dim destCnt As Integer = 0

    '        'DataReader→DataTableへの転記
    '        Dim map As Hashtable = New Hashtable()

    '        If reader.HasRows() = True Then

    '            map.Add("MST_CNT", "MST_CNT")
    '            map.Add("NRS_BR_CD", "NRS_BR_CD")
    '            map.Add("CUST_CD_L", "CUST_CD_L")
    '            map.Add("DEST_NM", "DEST_NM")

    '            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST_SHIP_CD_L")

    '            '処理件数の設定
    '            destCnt = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count

    '        End If

    '        reader.Close()

    '        MyBase.SetResultCount(destCnt)
    '        Return ds

    '    End Function

    '#End Region

#Region "チッソ実績作成処理"

#Region "出荷データ（中）追加分の出荷ＥＤＩデータ（中）特定チェック"

    ''' <summary>
    ''' 出荷データ（中）追加分の出荷ＥＤＩデータ（中）特定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectChissoOutkaAdd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.SQL_SELECT_OUTKA_ADD)


        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.setSQLSelectDataExists()                   '条件設定

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectChissoOutkaAdd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds
    End Function

#End Region

#Region "区分マスタより接続情報を取得"

    ''' <summary>
    ''' ORACLEDBのスキーマ取得処理(区分マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOracleSchma(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.SQL_SELECT_ORA_SCHMA)

        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")


        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtIn.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectOracleSchma", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NIS_CNT", "NIS_CNT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_Z_KBN")

        'reader.Read()

        reader.Close()

        Return ds

    End Function

#End Region

#Region "データ取得処理(受信TBLの特定)"

    ''' <summary>
    ''' EDI管理番号より受信TBLの特定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI管理番号より受信TBLの特定SQLの構築・発行</remarks>
    Private Function SelectChissoEdiSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.SQL_SELECT_SEARCH_HED_CSO_DATA)      'SQL構築(データ抽出用Select・From句)
        Call Me.setSQLSelectDataExists()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectChissoEdiSend", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("CNT", "CNT")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_SEARCH_HED_CSO")

        Dim max As Integer = ds.Tables("LMH030_SEARCH_HED_CSO").Rows.Count - 1
        Dim newCnt As Integer = 0
        Dim oldCnt As Integer = 0

        For i As Integer = 0 To max

            If Convert.ToInt32(ds.Tables("LMH030_SEARCH_HED_CSO").Rows(i).Item("CNT")) > oldCnt Then

                newCnt = Convert.ToInt32(ds.Tables("LMH030_SEARCH_HED_CSO").Rows(i).Item("CNT"))
                oldCnt = newCnt

            End If

        Next

        '処理件数の設定
        MyBase.SetResultCount(newCnt)
        reader.Close()

        Return ds

    End Function

#End Region

#Region "データ取得処理"

    ''' <summary>
    ''' チッソ受信(HED)の値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>チッソ受信(HED)結果取得SQLの構築・発行</remarks>
    Private Function SelectEdiRcvHedChissoData(ByVal ds As DataSet) As DataSet

        'Oracleコネクション開始＝Oracle処理開始
        Using conOra As OracleConnection = New OracleConnection

            Try

                conOra.ConnectionString = ConnectionMGR.GetConnectionString_DirectConnection(Com.Base.ConnectionMGR.ConnectionDb.CHISSO)
                conOra.Open()

                'データ抽出
                ds = Me.SetDataChissoHed(ds, conOra)

                '入力チェック
                If Me.ChissoHedCheck(ds) = False Then
                    Return ds
                End If

                'HEDDATA編集
                ds = Me.EditDatatableChissoHed(ds)

                'CHISSO_HED UPDATE
                ds = Me.UpdateEdiChissoHed(ds, conOra)

                ''CHISSO_MEI INSERT
                'ds = Me.InsertEdiChissoMei(ds, conOra)

                ''DTLDATA編集
                'ds = Me.EditDatatableChissoDtl(ds)

                'CHISSO_MEI UPDATE
                ds = Me.UpdateEdiChissoMei(ds, conOra)

            Catch ex As Exception

                Throw
            Finally
                conOra.Close()
                conOra.Dispose()

            End Try

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' チッソ受信(HED)の値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>チッソ受信(HED)結果取得SQLの構築・発行</remarks>
    Private Function SetDataChissoHed(ByVal ds As DataSet, ByVal conOra As OracleConnection) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_SEARCH_HED_CSO")
        Dim scmTbl As DataTable = ds.Tables("LMH030_Z_KBN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.SQL_SELECT_JISSEKISAKUSEI_EDI_RCV_HED)      'SQL構築(データ抽出用Select・From句)
        Call Me.setSQLSelectHedpara()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNmOra(Me._StrSql.ToString(), scmTbl.Rows(0).Item("NIS_CNT").ToString())

        'SQL文のコンパイル
        Dim cmd As OracleCommand = MyBase.CreateSqlCommandOra(sql, conOra)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteOraLog("LMH030DAC107", "SetDataChissoHed", cmd)

        'SQLの発行
        Dim reader As OracleDataReader = MyBase.GetSelectResultOra(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("HED_REC_NO", "HED_REC_NO")
        map.Add("NRS_TANTO", "NRS_TANTO")
        map.Add("DATA_KIND", "DATA_KIND")
        map.Add("SEND_CODE", "SEND_CODE")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("PROC_YMD", "PROC_YMD")
        map.Add("PROC_TIME", "PROC_TIME")
        map.Add("PROC_NO", "PROC_NO")
        map.Add("SEND_DEN_YMD", "SEND_DEN_YMD")
        map.Add("SEND_DEN_TIME", "SEND_DEN_TIME")
        map.Add("BPID_KKN", "BPID_KKN")
        map.Add("BPID_SUB_KKN", "BPID_SUB_KKN")
        map.Add("BPID_HAN", "BPID_HAN")
        map.Add("RCV_COMP_CD", "RCV_COMP_CD")
        map.Add("SND_COMP_CD", "SND_COMP_CD")
        map.Add("RB_KBN", "RB_KBN")
        map.Add("MOD_KBN", "MOD_KBN")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("FAX_KBN", "FAX_KBN")
        map.Add("OUTKA_REQ_KBN", "OUTKA_REQ_KBN")
        map.Add("INKA_P_KBN", "INKA_P_KBN")
        map.Add("OUTKA_SEPT_KBN", "OUTKA_SEPT_KBN")
        map.Add("EM_OUTKA_KBN", "EM_OUTKA_KBN")
        map.Add("UNSO_ROUTE_P", "UNSO_ROUTE_P")
        map.Add("UNSO_ROUTE_A", "UNSO_ROUTE_A")
        map.Add("CAR_KIND_P", "CAR_KIND_P")
        map.Add("CAR_KIND_A", "CAR_KIND_A")
        map.Add("CAR_NO_P", "CAR_NO_P")
        map.Add("CAR_NO_A", "CAR_NO_A")
        map.Add("COMBI_NO_P", "COMBI_NO_P")
        map.Add("COMBI_NO_A", "COMBI_NO_A")
        map.Add("UNSO_REQ_YN", "UNSO_REQ_YN")
        map.Add("DEST_CHK_KBN", "DEST_CHK_KBN")
        map.Add("INKO_DATE_P", "INKO_DATE_P")
        map.Add("INKO_DATE_A", "INKO_DATE_A")
        map.Add("INKO_TIME", "INKO_TIME")
        map.Add("OUTKA_DATE_P", "OUTKA_DATE_P")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("OUTKA_TIME", "OUTKA_TIME")
        map.Add("CARGO_BKG_DATE_P", "CARGO_BKG_DATE_P")
        map.Add("CARGO_BKG_DATE_A", "CARGO_BKG_DATE_A")
        map.Add("ARRIVAL_DATE_P", "ARRIVAL_DATE_P")
        map.Add("ARRIVAL_DATE_A", "ARRIVAL_DATE_A")
        map.Add("ARRIVAL_TIME", "ARRIVAL_TIME")
        map.Add("UNSO_DATE", "UNSO_DATE")
        map.Add("UNSO_TIME", "UNSO_TIME")
        map.Add("ZAI_RPT_DATE", "ZAI_RPT_DATE")
        map.Add("BAILER_CD", "BAILER_CD")
        map.Add("BAILER_NM", "BAILER_NM")
        map.Add("BAILER_BU_CD", "BAILER_BU_CD")
        map.Add("BAILER_BU_NM", "BAILER_BU_NM")
        map.Add("SHIPPER_CD", "SHIPPER_CD")
        map.Add("SHIPPER_NM", "SHIPPER_NM")
        map.Add("SHIPPER_BU_CD", "SHIPPER_BU_CD")
        map.Add("SHIPPER_BU_NM", "SHIPPER_BU_NM")
        map.Add("CONSIGNEE_CD", "CONSIGNEE_CD")
        map.Add("CONSIGNEE_NM", "CONSIGNEE_NM")
        map.Add("CONSIGNEE_BU_CD", "CONSIGNEE_BU_CD")
        map.Add("CONSIGNEE_BU_NM", "CONSIGNEE_BU_NM")
        map.Add("SOKO_PROV_CD", "SOKO_PROV_CD")
        map.Add("SOKO_PROV_NM", "SOKO_PROV_NM")
        map.Add("UNSO_PROV_CD", "UNSO_PROV_CD")
        map.Add("UNSO_PROV_NM", "UNSO_PROV_NM")
        map.Add("ACT_UNSO_CD", "ACT_UNSO_CD")
        map.Add("UNSO_TF_KBN", "UNSO_TF_KBN")
        map.Add("UNSO_F_KBN", "UNSO_F_KBN")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_BU_CD", "DEST_BU_CD")
        map.Add("DEST_BU_NM", "DEST_BU_NM")
        map.Add("DEST_AD_CD", "DEST_AD_CD")
        map.Add("DEST_AD_NM", "DEST_AD_NM")
        map.Add("DEST_YB_NO", "DEST_YB_NO")
        map.Add("DEST_TEL_NO", "DEST_TEL_NO")
        map.Add("DEST_FAX_NO", "DEST_FAX_NO")
        map.Add("DELIVERY_NM", "DELIVERY_NM")
        map.Add("DELIVERY_SAGYO", "DELIVERY_SAGYO")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        map.Add("PRI_SHOP_CD", "PRI_SHOP_CD")
        map.Add("PRI_SHOP_NM", "PRI_SHOP_NM")
        map.Add("INV_REM_NM", "INV_REM_NM")
        map.Add("INV_REM_KANA", "INV_REM_KANA")
        map.Add("DEN_NO", "DEN_NO")
        map.Add("MEI_DEN_NO", "MEI_DEN_NO")
        map.Add("OUTKA_POSI_CD", "OUTKA_POSI_CD")
        map.Add("OUTKA_POSI_NM", "OUTKA_POSI_NM")
        map.Add("OUTKA_POSI_BU_CD_PA", "OUTKA_POSI_BU_CD_PA")
        map.Add("OUTKA_POSI_BU_CD_NAIBU", "OUTKA_POSI_BU_CD_NAIBU")
        map.Add("OUTKA_POSI_BU_NM_PA", "OUTKA_POSI_BU_NM_PA")
        map.Add("OUTKA_POSI_BU_NM_NAIBU", "OUTKA_POSI_BU_NM_NAIBU")
        map.Add("OUTKA_POSI_AD_CD_PA", "OUTKA_POSI_AD_CD_PA")
        map.Add("OUTKA_POSI_AD_NM_PA", "OUTKA_POSI_AD_NM_PA")
        map.Add("OUTKA_POSI_TEL_NO_PA", "OUTKA_POSI_TEL_NO_PA")
        map.Add("OUTKA_POSI_FAX_NO_PA", "OUTKA_POSI_FAX_NO_PA")
        map.Add("UNSO_JURYO", "UNSO_JURYO")
        map.Add("UNSO_JURYO_FLG", "UNSO_JURYO_FLG")
        map.Add("UNIT_LOAD_CD", "UNIT_LOAD_CD")
        map.Add("UNIT_LOAD_SU", "UNIT_LOAD_SU")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_KANA", "REMARK_KANA")
        map.Add("HARAI_KBN", "HARAI_KBN")
        map.Add("DATA_TYPE", "DATA_TYPE")
        map.Add("RTN_FLG", "RTN_FLG")
        map.Add("SND_CANCEL_FLG", "SND_CANCEL_FLG")
        map.Add("OLD_DATA_FLG", "OLD_DATA_FLG")
        map.Add("PRINT_FLG", "PRINT_FLG")
        map.Add("PRINT_NO", "PRINT_NO")
        map.Add("NRS_SYS_FLG", "NRS_SYS_FLG")
        map.Add("OLD_SYS_FLG", "OLD_SYS_FLG")
        map.Add("RTN_FILE_DATE", "RTN_FILE_DATE")
        map.Add("RTN_FILE_TIME", "RTN_FILE_TIME")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")

        ds = MyBase.SetSelectResultToDataSetOra(map, ds, reader, "LMH030_H_OUTKAEDI_HED_CSO")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_H_OUTKAEDI_HED_CSO").Rows.Count)
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' チッソ受信(HED)の値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>チッソ受信(HED)結果取得SQLの構築・発行</remarks>
    Private Function ChissoHedCheck(ByVal ds As DataSet) As Boolean

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()


        If MyBase.GetResultCount = 0 OrElse String.IsNullOrEmpty(ds.Tables("LMH030_H_OUTKAEDI_HED_CSO").Rows(0).Item("HED_REC_NO").ToString()) = True Then
            MyBase.SetMessageStore(LMH030DAC.GUIDANCE_KBN, "E078", New String() {"受信テーブル(CHISSO_HED)"}, rowNo, LMH030DAC.EXCEL_COLTITLE, ediCtlNo)
            Return False

        ElseIf ds.Tables("LMH030_H_OUTKAEDI_HED_CSO").Rows(0).Item("RTN_FLG").ToString().Equals("0") = False Then
            MyBase.SetMessageStore(LMH030DAC.GUIDANCE_KBN, "E375", New String() {"送信データが既に存在している", "実績作成"}, rowNo, LMH030DAC.EXCEL_COLTITLE, ediCtlNo)
            Return False

        ElseIf ds.Tables("LMH030_H_OUTKAEDI_HED_CSO").Rows(0).Item("OLD_DATA_FLG").ToString().Equals("Y") = True Then
            MyBase.SetMessageStore(LMH030DAC.GUIDANCE_KBN, "E320", New String() {"受信データが旧データ", "実績作成"}, rowNo, LMH030DAC.EXCEL_COLTITLE, ediCtlNo)
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' チッソ専用EDI受信(HED)の編集処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>チッソ専用EDI受信(HED)の編集処理</remarks>
    Private Function EditDatatableChissoHed(ByVal ds As DataSet) As DataSet

        Dim setDt As DataTable = ds.Tables("LMH030_SEARCH_HED_CSO")
        Dim setDsHed As DataTable = ds.Tables("LMH030_H_OUTKAEDI_HED_CSO")

        setDsHed.Rows(0).Item("UNSO_ROUTE_A") = setDsHed.Rows(0).Item("UNSO_ROUTE_P").ToString()
        setDsHed.Rows(0).Item("OUTKA_DATE_A") = setDt.Rows(0).Item("OUTKO_DATE").ToString()
        setDsHed.Rows(0).Item("ARRIVAL_DATE_A") = setDt.Rows(0).Item("ARR_PLAN_DATE").ToString()

        Return ds

    End Function

    '''' <summary>
    '''' チッソ専用EDI受信(DTL)の編集処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>チッソ専用EDI受信(DTL)の編集処理</remarks>
    'Private Function EditDatatableChissoDtl(ByVal ds As DataSet) As DataSet

    '    Return ds

    'End Function

#End Region

#End Region

#Region "Update"

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC107.SQL_UPDATE_OUTKASAVEEDI_L

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC107.SQL_UPDATE_JISSEKISAKUSEI_EDI_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetOutkaEdiLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetJissekiParameterEdiLM(inTbl.Rows(0), dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateOutkaEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

    'チッソの場合、出荷登録と実績作成のEDI出荷(中)更新ロジックを分割するSTART
#Region "H_OUTKAEDI_M(出荷登録時)"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新(出荷登録時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediMTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Dim loopflg As Integer = 0

        Dim rtn As Integer = 0

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録、紐付けSQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE
                setSql = LMH030DAC107.SQL_UPDATE_OUTKAEDI_M
                loopflg = 1

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = ediMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetJissekiParameterEdiLM(ediMTbl.Rows(i), dtEventShubetsu)
            Call Me.SetOutkaEdiMComParameter(Me._Row, Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateOutkaEdiMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M(実績作成時)"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新(実績作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiMDataJisseki(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtSChisso As DataTable = ds.Tables("LMH030_SEARCH_HED_CSO")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        setSql = LMH030DAC107.SQL_UPDATE_JISSEKISAKUSEI_EDI_M_CHISSO


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                                       , dtSChisso.Rows(0).Item("NRS_BR_CD").ToString()))
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = dtSChisso.Rows(0)

        'パラメータ設定
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetJissekiParameterEdiLM(Me._Row, dtEventShubetsu)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", "1", DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me._Row.Item("FREE_C02").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.NVARCHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateOutkaEdiMDataJisseki", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds

    End Function

#End Region
    'チッソの場合、出荷登録と実績作成のEDI出荷(中)更新ロジックを分割するEND

#Region "CHISSO_HED(ORACLE)"

    ''' <summary>
    ''' CHISSO_HED(ORACLE)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(HED)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiChissoHed(ByVal ds As DataSet, ByVal conOra As OracleConnection) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvHedTbl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_HED_CSO")
        Dim scmTbl As DataTable = ds.Tables("LMH030_Z_KBN")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediRcvHedTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成SQL CONST名
        setSql = LMH030DAC107.SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_HED

        'SQL文のコンパイル
        Dim cmdOra As OracleCommand = MyBase.CreateSqlCommandOra(Me.SetSchemaNmOra(setSql, scmTbl.Rows(0).Item("NIS_CNT").ToString()), conOra)

        'パラメータ設定
        Call Me.SetEdiRcvHedParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmdOra.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteOraLog("LMH030DAC107", "UpdateEdiChissoHed", cmdOra)

        'SQLの発行

        Dim updateCnt As Integer = 0

        updateCnt = MyBase.GetUpdateResultOra(cmdOra)
        'SQLの発行
        If updateCnt < 1 Then
            MyBase.SetMessage("E011")
        End If

        MyBase.SetResultCount(updateCnt)

        Return ds

    End Function

#End Region

#Region "CHISSO_MEI(ORACLE)"

    ''' <summary>
    ''' CHISSO_MEI(ORACLE)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(MEI)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiChissoMei(ByVal ds As DataSet, ByVal conOra As OracleConnection) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_SEARCH_HED_CSO")
        Dim ediRcvHedTbl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_HED_CSO")
        Dim scmTbl As DataTable = ds.Tables("LMH030_Z_KBN")
        Dim setSql As String = String.Empty
        ''INTableの条件rowの格納
        'Me._Row = ediRcvHedTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成SQL CONST名
        'If inTbl.Rows(0).Item("CNT").ToString().Equals("1") = True Then
        setSql = LMH030DAC107.SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_DTL_CNT1
        'Else
        'setSql = LMH030DAC107.SQL_UPDATE_JISSEKISAKUSEI_EDI_RCV_DTL_CNT2
        'End If

        'SQL文のコンパイル
        Dim cmdOra As OracleCommand = MyBase.CreateSqlCommandOra(Me.SetSchemaNmOra(setSql, scmTbl.Rows(0).Item("NIS_CNT").ToString()), conOra)

        'パラメータ設定
        'Call Me.SetEdiRcvMeiParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetEdiRcvMeiParameter(ediRcvHedTbl, inTbl, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmdOra.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteOraLog("LMH030DAC107", "UpdateEdiChissoMei", cmdOra)

        'SQLの発行

        Dim updateCnt As Integer = 0

        updateCnt = MyBase.GetUpdateResultOra(cmdOra)
        'SQLの発行
        If updateCnt < 1 Then
            MyBase.SetMessage("E011")
        End If

        MyBase.SetResultCount(updateCnt)

        Return ds

    End Function

#End Region

    '#Region "CHISSO_MEI(ORACLE)"

    '    ''' <summary>
    '    ''' CHISSO_MEI(ORACLE)テーブルついか
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    '    Private Function InsertEdiChissoMei(ByVal ds As DataSet, ByVal conOra As OracleConnection) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim scmTbl As DataTable = ds.Tables("LMH030_Z_KBN")
    '        Dim inTbl As DataTable = ds.Tables("LMH030_H_OUTKAEDI_HED_CSO")
    '        Dim setSql As String = String.Empty

    '        Dim rtn As Integer = 0

    '        '条件rowの格納
    '        Me._Row = inTbl.Rows(0)

    '        setSql = LMH030DAC107.SQL_INSERT_JISSEKISAKUSEI_EDI_RCV_DTL

    '        'SQL文のコンパイル
    '        Dim cmdOra As OracleCommand = MyBase.CreateSqlCommandOra(Me.SetSchemaNmOra(setSql, scmTbl.Rows(0).Item("NIS_CNT").ToString()), conOra)


    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'パラメータ設定
    '        Call Me.SetJissekiParameterRcvDtl(Me._Row, Me._SqlPrmList)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmdOra.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteOraLog("LMH030DAC107", "InsertEdiChissoMei", cmdOra)

    '        'SQLの発行
    '        MyBase.GetInsertResultOra(cmdOra)

    '        'パラメータの初期化
    '        cmdOra.Parameters.Clear()

    '        Return ds

    '    End Function

    '#End Region

#Region "C_OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録(まとめ時)
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC107.SQL_UPDATE_OUTKAL_MATOME

                '実績作成SQL CONST名
            Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                setSql = LMH030DAC107.SQL_UPDATE_JISSEKISAKUSEI_OUTKA_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateOutkaLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "まとめ先EDI出荷(大)"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMatomesakiEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC107.SQL_UPDATE_MATOMESAKI_OUTKAEDI_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiLMatomeParameter(inTbl.Rows(0), Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateMatomesakiEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "まとめ先運送(大)"

    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateMatomesakiUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_UNSO_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '出荷登録SQL CONST名
            Case LMH030DAC.EventShubetsu.SAVEOUTKA
                setSql = LMH030DAC107.SQL_UPDATE_MATOMESAKI_UNSO_L

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateMatomesakiUnsoLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

    '#Region "M_DEST(チッソは自動更新がないため使用しない)"

    '    ''' <summary>
    '    ''' 届先マスタ更新
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks>届先マスタ更新SQLの構築・発行</remarks>
    '    Private Function UpdateMDestData(ByVal ds As DataSet) As DataSet

    '        Dim inTbl As DataTable = ds.Tables("LMH030_M_DEST")
    '        Dim setSql As String = String.Empty
    '        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

    '        'SQL文のコンパイル
    '        setSql = SQL_UPDATE_M_DEST_TOHO

    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

    '        'INTableの条件rowの格納
    '        Me._Row = inTbl.Rows(0)

    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'パラメータ設定
    '        Call Me.SetMdestParameter(inTbl, 1)
    '        Call Me.SetMdestUpdateParameter(Me._Row, Me._SqlPrmList)
    '        Call Me.SetSysdataParameter(Me._SqlPrmList)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateMDestData", cmd)

    '        'SQLの発行
    '        Me.UpdateResultChk(cmd)

    '        Return ds

    '    End Function

    '#End Region

#End Region

#Region "Insert"

#Region "C_OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outkaLTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        'INTableの条件rowの格納
        Me._Row = outkaLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_OUTKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "InsertOutkaLData", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "C_OUTKA_M"

    ''' <summary>
    ''' 出荷(中)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function InsertOutkaMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim outkaMTbl As DataTable = ds.Tables("LMH030_C_OUTKA_M")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_OUTKA_M _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = outkaMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = outkaMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetOutkaMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC107", "UpdateOutkaMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

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
    Private Function InsertSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim sagyoTbl As DataTable = ds.Tables("LMH030_E_SAGYO")

        Dim rtn As Integer = 0

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_SAGYO _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = sagyoTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = sagyoTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetSagyoParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC107", "InsertSagyoData", cmd)

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
    Private Function InsertUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoLTbl As DataTable = ds.Tables("LMH030_UNSO_L")
        'INTableの条件rowの格納
        Me._Row = unsoLTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_UNSO_L _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetUnsoLComParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "InsertUnsoLData", cmd)

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
    Private Function InsertUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unsoMTbl As DataTable = ds.Tables("LMH030_UNSO_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_UNSO_M _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = unsoMTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = unsoMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnsoMComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC107", "InsertUnsoMData", cmd)

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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables("F_UNCHIN_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnchinComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC107", "InsertUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

    '#Region "M_DEST(チッソは自動追加がない為、使用しない)"

    '    ''' <summary>
    '    ''' 届先マスタ新規登録(荷送人コードまたは届先コードを元に新規登録:日本合成専用)
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks>届先マスタ新規登録SQLの構築・発行</remarks>
    '    Private Function InsertMDestData(ByVal ds As DataSet) As DataSet

    '        'DataSetのIN情報を取得
    '        Dim inTbl As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
    '        '★★★2011.11.11 要望番号439 届先マスタ自動追加 追加 START
    '        Dim mDestShipCnt As Integer = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count
    '        Dim mDestCnt As Integer = ds.Tables("LMH030_M_DEST").Rows.Count
    '        '★★★2011.11.11 要望番号439 届先マスタ自動追加 追加 END

    '        'DataSetのIN情報を取得
    '        '★★★2011.11.11 要望番号439 届先マスタ自動追加 修正 START
    '        If mDestShipCnt > 0 AndAlso ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("INSERT_TARGET_FLG").Equals("1") = True Then
    '            inTbl = ds.Tables("LMH030_M_DEST_SHIP_CD_L")
    '        ElseIf mDestCnt > 0 AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG").Equals("1") = True Then
    '            inTbl = ds.Tables("LMH030_M_DEST")
    '        End If
    '        '★★★2011.11.11 要望番号439 届先マスタ自動追加 修正 END

    '        'INTableの条件rowの格納
    '        Me._Row = inTbl.Rows(0)
    '        'SQLパラメータ初期化
    '        Me._SqlPrmList = New ArrayList()

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC107.SQL_INSERT_M_DEST_NCGO, Me._Row.Item("NRS_BR_CD").ToString()))

    '        'パラメータ設定
    '        Call Me.SetDataInsertParameter(Me._SqlPrmList)
    '        Call Me.SetMdestInsertParameter(Me._Row, Me._SqlPrmList)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        MyBase.Logger.WriteSQLLog("LMH030DAC107", "InsertMDestData", cmd)

    '        'SQLの発行
    '        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

    '        Return ds

    '    End Function

    '#End Region

#End Region

#Region "SQL"

#Region "スキーマ名称設定"
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

#Region "スキーマ名称設定(ORACLE)"
    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNmOra(ByVal sql As String, ByVal scmNm As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$ORACLE$", scmNm)

        Return sql

    End Function

#End Region

#Region "届先マスタ抽出パラメータ設定"
    ''' <summary>
    ''' 届先マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestParameter(ByVal dt As DataTable, ByVal prmUpdFlg As Integer)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.NVARCHAR))

        If prmUpdFlg = 1 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        End If

    End Sub


#End Region

#Region "EDI出荷(大,中),EDI受信TBL抽出パラメータ設定"

    ''' <summary>
    '''  パラメータ設定（EDI出荷(大・中),EDI受信テーブル・存在チェック）
    ''' </summary>
    ''' <remarks>出荷登録時出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelectDataExists()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me._Row("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "EDI受信(HED)TBL抽出パラメータ設定"

    ''' <summary>
    '''  パラメータ設定（EDI受信(HED)テーブル）
    ''' </summary>
    ''' <remarks>実績作成時データ検索用SQLの構築</remarks>
    Private Sub setSQLSelectHedpara()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(共通）
        'Me._SqlPrmList.Add(MyBase.GetSqlParameterOra(":NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataTypeOra.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameterOra(":HED_REC_NO", Me._Row("FREE_C01").ToString(), DBDataTypeOra.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameterOra("@HED_REC_NO", Me._Row("FREE_C02").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "共通パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時:EDI(大))
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(.Item("NAIGAI_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_NM", Me.NullConvertString(.Item("NRS_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_NM", Me.NullConvertString(.Item("WH_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(.Item("CUST_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(.Item("CUST_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", Me.NullConvertString(.Item("SHIP_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_M", Me.NullConvertString(.Item("SHIP_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", Me.NullConvertString(.Item("EDI_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(.Item("DEST_ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", Me.NullConvertString(.Item("DEST_AD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", Me.NullConvertString(.Item("DEST_AD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", Me.NullConvertString(.Item("DEST_FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", Me.NullConvertString(.Item("DEST_MAIL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.NullConvertString(.Item("DEST_JIS_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", Me.NullConvertString(.Item("UNSO_MOTO_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(.Item("SYARYO_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.NullConvertString(.Item("UNSO_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me.NullConvertString(.Item("UNSO_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me.NullConvertString(.Item("UNCHIN_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me.NullConvertString(.Item("EXTC_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", Me.NullConvertString(.Item("UNSO_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", Me.NullConvertString(.Item("UNCHIN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(.Item("OUT_FLAG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.CHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", Me.NullConvertString(.Item("CRT_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", Me.NullConvertString(.Item("SCM_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(.Item("EDIT_FLAG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(.Item("MATCHING_FLAG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定(実績日時用)"

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI出荷(大,中)用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterEdiLM(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(row.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(row.Item("JISSEKI_DATE")), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(row.Item("JISSEKI_TIME")), DBDataType.NVARCHAR))

            Case LMH030DAC.EventShubetsu.CREATEJISSEKI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime))

            Case Else

        End Select

    End Sub

#End Region

#Region "まとめ先EDI出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' まとめ先EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiLMatomeParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", String.Concat("04-", .Item("EDI_CTL_NO").ToString())))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))

        End With

    End Sub

#End Region

#Region "EDI出荷(中)更新パラメータ設定"

    ''' <summary>
    ''' EDI出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_KB", .Item("OUT_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.FormatNumValue(.Item("FREE_N01").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.FormatNumValue(.Item("FREE_N02").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.FormatNumValue(.Item("FREE_N03").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.FormatNumValue(.Item("FREE_N04").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.FormatNumValue(.Item("FREE_N05").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.FormatNumValue(.Item("FREE_N06").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.FormatNumValue(.Item("FREE_N07").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.FormatNumValue(.Item("FREE_N08").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.FormatNumValue(.Item("FREE_N09").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.FormatNumValue(.Item("FREE_N10").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", .Item("CRT_USER").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", .Item("CRT_TIME").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "EDI受信(DTL)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(DTL)の更新パラメータ設定
    ''' </summary>
    ''' <param name="ediRcvHedTbl">DataTable</param>
    ''' <param name="inTbl">DataTable</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiRcvMeiParameter(ByVal ediRcvHedTbl As DataTable, ByVal inTbl As DataTable, ByVal prmList As ArrayList)

        'prmList.Add(MyBase.GetSqlParameterOra(":NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataTypeOra.VARCHAR2))
        prmList.Add(MyBase.GetSqlParameterOra(":HED_REC_NO", ediRcvHedTbl.Rows(0).Item("HED_REC_NO").ToString(), DBDataTypeOra.CHAR))
        prmList.Add(MyBase.GetSqlParameterOra(":MEI_REC_NO", inTbl.Rows(0).Item("FREE_C02").ToString(), DBDataTypeOra.CHAR))
        'prmList.Add(MyBase.GetSqlParameterOra(":HIS_NO", Me.FormatNumValue(.Item("HIS_NO").ToString()), DBDataTypeOra.NUMBER))
        'prmList.Add(MyBase.GetSqlParameterOra(":MEI_NO_P", .Item("MEI_NO_P").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":MEI_NO_A", .Item("MEI_NO_A").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":JYUCHU_GOODS_CD", .Item("JYUCHU_GOODS_CD").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":GOODS_NM", .Item("GOODS_NM").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":GOODS_KANA1", .Item("GOODS_KANA1").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":GOODS_KANA2", .Item("GOODS_KANA2").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":NISUGATA_CD", .Item("NISUGATA_CD").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":IRISUU", .Item("IRISUU").ToString(), DBDataTypeOra.VARCHAR2))
        prmList.Add(MyBase.GetSqlParameterOra(":LOT_NO_A", inTbl.Rows(0)("LOT_NO").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":LOT_NO_P", .Item("LOT_NO_P").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":SURY_TANI_CD", .Item("SURY_TANI_CD").ToString(), DBDataTypeOra.VARCHAR2))

        'If inTbl.Rows(0).Item("CNT").ToString().Equals("1") = True Then
        prmList.Add(MyBase.GetSqlParameterOra(":SURY_RPT", Me.FormatNumValue(inTbl.Rows(0).Item("ALCTD_QT").ToString()), DBDataTypeOra.NUMBER))

        'Else
        'prmList.Add(MyBase.GetSqlParameterOra(":SURY_REQ", Me.FormatNumValue(ediRcvHedTbl.Rows(0).Item("ALCTD_QT").ToString()), DBDataTypeOra.NUMBER))
        'prmList.Add(MyBase.GetSqlParameterOra(":SURY_RPT", Me.FormatNumValue(inTbl.Rows(0).Item("ALCTD_QT").ToString()), DBDataTypeOra.NUMBER))
        'End If
        'prmList.Add(MyBase.GetSqlParameterOra(":MEI_REM1", .Item("MEI_REM1").ToString(), DBDataTypeOra.VARCHAR2))
        'prmList.Add(MyBase.GetSqlParameterOra(":MEI_REM2", .Item("MEI_REM2").ToString(), DBDataTypeOra.VARCHAR2))
        prmList.Add(MyBase.GetSqlParameterOra(":UPD_USER", MyBase.GetUserID(), DBDataTypeOra.VARCHAR2))
        prmList.Add(MyBase.GetSqlParameterOra(":UPD_DATE", MyBase.GetSystemDate(), DBDataTypeOra.CHAR))

    End Sub

#End Region

#Region "EDI受信(HED)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(HED)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiRcvHedParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameterOra(":HED_REC_NO", Me.NullConvertString(.Item("HED_REC_NO")), DBDataTypeOra.CHAR))
            prmList.Add(MyBase.GetSqlParameterOra(":NRS_TANTO", Me.NullConvertString(.Item("NRS_TANTO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DATA_KIND", Me.NullConvertString(.Item("DATA_KIND")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SEND_CODE", Me.NullConvertString(.Item("SEND_CODE")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SR_DEN_NO", Me.NullConvertString(.Item("SR_DEN_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":HIS_NO", Me.FormatNumValue(.Item("HIS_NO").ToString()), DBDataTypeOra.NUMBER))
            prmList.Add(MyBase.GetSqlParameterOra(":PROC_YMD", Me.NullConvertString(.Item("PROC_YMD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":PROC_TIME", Me.NullConvertString(.Item("PROC_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":PROC_NO", Me.FormatNumValue(.Item("PROC_NO").ToString()), DBDataTypeOra.NUMBER))
            prmList.Add(MyBase.GetSqlParameterOra(":SEND_DEN_YMD", Me.NullConvertString(.Item("SEND_DEN_YMD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SEND_DEN_TIME", Me.NullConvertString(.Item("SEND_DEN_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BPID_KKN", Me.NullConvertString(.Item("BPID_KKN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BPID_SUB_KKN", Me.NullConvertString(.Item("BPID_SUB_KKN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BPID_HAN", Me.NullConvertString(.Item("BPID_HAN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":RCV_COMP_CD", Me.NullConvertString(.Item("RCV_COMP_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SND_COMP_CD", Me.NullConvertString(.Item("SND_COMP_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":RB_KBN", Me.NullConvertString(.Item("RB_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":MOD_KBN", Me.NullConvertString(.Item("MOD_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DATA_KBN", Me.NullConvertString(.Item("DATA_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":FAX_KBN", Me.NullConvertString(.Item("FAX_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_REQ_KBN", Me.NullConvertString(.Item("OUTKA_REQ_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":INKA_P_KBN", Me.NullConvertString(.Item("INKA_P_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_SEPT_KBN", Me.NullConvertString(.Item("OUTKA_SEPT_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":EM_OUTKA_KBN", Me.NullConvertString(.Item("EM_OUTKA_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_ROUTE_P", Me.NullConvertString(.Item("UNSO_ROUTE_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_ROUTE_A", Me.NullConvertString(.Item("UNSO_ROUTE_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CAR_KIND_P", Me.NullConvertString(.Item("CAR_KIND_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CAR_KIND_A", Me.NullConvertString(.Item("CAR_KIND_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CAR_NO_P", Me.NullConvertString(.Item("CAR_NO_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CAR_NO_A", Me.NullConvertString(.Item("CAR_NO_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":COMBI_NO_P", Me.NullConvertString(.Item("COMBI_NO_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":COMBI_NO_A", Me.NullConvertString(.Item("COMBI_NO_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_REQ_YN", Me.NullConvertString(.Item("UNSO_REQ_YN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_CHK_KBN", Me.NullConvertString(.Item("DEST_CHK_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":INKO_DATE_P", Me.NullConvertString(.Item("INKO_DATE_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":INKO_DATE_A", Me.NullConvertString(.Item("INKO_DATE_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":INKO_TIME", Me.NullConvertString(.Item("INKO_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_DATE_P", Me.NullConvertString(.Item("OUTKA_DATE_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_DATE_A", Me.NullConvertString(.Item("OUTKA_DATE_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_TIME", Me.NullConvertString(.Item("OUTKA_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CARGO_BKG_DATE_P", Me.NullConvertString(.Item("CARGO_BKG_DATE_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CARGO_BKG_DATE_A", Me.NullConvertString(.Item("CARGO_BKG_DATE_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":ARRIVAL_DATE_P", Me.NullConvertString(.Item("ARRIVAL_DATE_P")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":ARRIVAL_DATE_A", Me.NullConvertString(.Item("ARRIVAL_DATE_A")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":ARRIVAL_TIME", Me.NullConvertString(.Item("ARRIVAL_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_DATE", Me.NullConvertString(.Item("UNSO_DATE")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_TIME", Me.NullConvertString(.Item("UNSO_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":ZAI_RPT_DATE", Me.NullConvertString(.Item("ZAI_RPT_DATE")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BAILER_CD", Me.NullConvertString(.Item("BAILER_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BAILER_NM", Me.NullConvertString(.Item("BAILER_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BAILER_BU_CD", Me.NullConvertString(.Item("BAILER_BU_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":BAILER_BU_NM", Me.NullConvertString(.Item("BAILER_BU_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SHIPPER_CD", Me.NullConvertString(.Item("SHIPPER_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SHIPPER_NM", Me.NullConvertString(.Item("SHIPPER_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SHIPPER_BU_CD", Me.NullConvertString(.Item("SHIPPER_BU_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SHIPPER_BU_NM", Me.NullConvertString(.Item("SHIPPER_BU_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CONSIGNEE_CD", Me.NullConvertString(.Item("CONSIGNEE_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CONSIGNEE_NM", Me.NullConvertString(.Item("CONSIGNEE_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CONSIGNEE_BU_CD", Me.NullConvertString(.Item("CONSIGNEE_BU_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CONSIGNEE_BU_NM", Me.NullConvertString(.Item("CONSIGNEE_BU_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SOKO_PROV_CD", Me.NullConvertString(.Item("SOKO_PROV_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SOKO_PROV_NM", Me.NullConvertString(.Item("SOKO_PROV_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_PROV_CD", Me.NullConvertString(.Item("UNSO_PROV_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_PROV_NM", Me.NullConvertString(.Item("UNSO_PROV_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":ACT_UNSO_CD", Me.NullConvertString(.Item("ACT_UNSO_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_TF_KBN", Me.NullConvertString(.Item("UNSO_TF_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_F_KBN", Me.NullConvertString(.Item("UNSO_F_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_BU_CD", Me.NullConvertString(.Item("DEST_BU_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_BU_NM", Me.NullConvertString(.Item("DEST_BU_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_AD_CD", Me.NullConvertString(.Item("DEST_AD_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_AD_NM", Me.NullConvertString(.Item("DEST_AD_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_YB_NO", Me.NullConvertString(.Item("DEST_YB_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_TEL_NO", Me.NullConvertString(.Item("DEST_TEL_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEST_FAX_NO", Me.NullConvertString(.Item("DEST_FAX_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DELIVERY_NM", Me.NullConvertString(.Item("DELIVERY_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DELIVERY_SAGYO", Me.NullConvertString(.Item("DELIVERY_SAGYO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":ORDER_NO", Me.NullConvertString(.Item("ORDER_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":JYUCHU_NO", Me.NullConvertString(.Item("JYUCHU_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":PRI_SHOP_CD", Me.NullConvertString(.Item("PRI_SHOP_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":PRI_SHOP_NM", Me.NullConvertString(.Item("PRI_SHOP_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":INV_REM_NM", Me.NullConvertString(.Item("INV_REM_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":INV_REM_KANA", Me.NullConvertString(.Item("INV_REM_KANA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DEN_NO", Me.NullConvertString(.Item("DEN_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":MEI_DEN_NO", Me.FormatNumValue(.Item("MEI_DEN_NO").ToString()), DBDataTypeOra.NUMBER))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_CD", Me.NullConvertString(.Item("OUTKA_POSI_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_NM", Me.NullConvertString(.Item("OUTKA_POSI_NM")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_BU_CD_PA", Me.NullConvertString(.Item("OUTKA_POSI_BU_CD_PA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_BU_CD_NAIBU", Me.NullConvertString(.Item("OUTKA_POSI_BU_CD_NAIBU")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_BU_NM_PA", Me.NullConvertString(.Item("OUTKA_POSI_BU_NM_PA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_BU_NM_NAIBU", Me.NullConvertString(.Item("OUTKA_POSI_BU_NM_NAIBU")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_AD_CD_PA", Me.NullConvertString(.Item("OUTKA_POSI_AD_CD_PA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_AD_NM_PA", Me.NullConvertString(.Item("OUTKA_POSI_AD_NM_PA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_TEL_NO_PA", Me.NullConvertString(.Item("OUTKA_POSI_TEL_NO_PA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OUTKA_POSI_FAX_NO_PA", Me.NullConvertString(.Item("OUTKA_POSI_FAX_NO_PA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_JURYO", Me.NullConvertString(.Item("UNSO_JURYO")), DBDataTypeOra.NUMBER))
            prmList.Add(MyBase.GetSqlParameterOra(":UNSO_JURYO_FLG", Me.NullConvertString(.Item("UNSO_JURYO_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNIT_LOAD_CD", Me.NullConvertString(.Item("UNIT_LOAD_CD")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UNIT_LOAD_SU", Me.FormatNumValue(.Item("UNIT_LOAD_SU").ToString()), DBDataTypeOra.NUMBER))
            prmList.Add(MyBase.GetSqlParameterOra(":REMARK", Me.NullConvertString(.Item("REMARK")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":REMARK_KANA", Me.NullConvertString(.Item("REMARK_KANA")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":HARAI_KBN", Me.NullConvertString(.Item("HARAI_KBN")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":DATA_TYPE", Me.NullConvertString(.Item("DATA_TYPE")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":RTN_FLG", Me.NullConvertString(.Item("RTN_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":SND_CANCEL_FLG", Me.NullConvertString(.Item("SND_CANCEL_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OLD_DATA_FLG", Me.NullConvertString(.Item("OLD_DATA_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":PRINT_FLG", Me.NullConvertString(.Item("PRINT_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":PRINT_NO", Me.NullConvertString(.Item("PRINT_NO")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":NRS_SYS_FLG", Me.NullConvertString(.Item("NRS_SYS_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":OLD_SYS_FLG", Me.NullConvertString(.Item("OLD_SYS_FLG")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":RTN_FILE_DATE", Me.NullConvertString(.Item("RTN_FILE_DATE")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":RTN_FILE_TIME", Me.NullConvertString(.Item("RTN_FILE_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UPD_USER", MyBase.GetUserID(), DBDataTypeOra.VARCHAR2))
            prmList.Add(MyBase.GetSqlParameterOra(":UPD_DATE", MyBase.GetSystemDate(), DBDataTypeOra.CHAR))

        End With

    End Sub

#End Region

#Region "出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(.Item("OUTKA_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(.Item("FURI_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(.Item("DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", Me.NullConvertString(.Item("ARR_KANRYO_INFO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", Me.NullConvertString(.Item("END_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", Me.NullConvertString(.Item("NHS_REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.NullConvertZero(.Item("OUTKA_PKG_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", Me.NullConvertString(.Item("ALL_PRINT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", Me.NullConvertString(.Item("NIHUDA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", Me.NullConvertString(.Item("NHS_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", Me.NullConvertString(.Item("DENP_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me.NullConvertString(.Item("COA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", Me.NullConvertString(.Item("HOKOKU_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", Me.NullConvertString(.Item("MATOME_PICK_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", Me.NullConvertString(.Item("LAST_PRINT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", Me.NullConvertString(.Item("LAST_PRINT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", Me.NullConvertString(.Item("SASZ_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", Me.NullConvertString(.Item("OUTKO_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", Me.NullConvertString(.Item("KEN_USER")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", Me.NullConvertString(.Item("OUTKA_USER")), DBDataType.CHAR))

            'イベント種別の判断
            Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

                '出荷登録処理
                '出荷登録SQL CONST名
                Case LMH030DAC.EventShubetsu.SAVEOUTKA
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", Me.NullConvertString(.Item("HOU_USER")), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))

                    '実績作成SQL CONST名
                Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

            End Select
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", Me.NullConvertString(.Item("ORDER_TYPE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_KB", Me.NullConvertString(.Item("DEST_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))

            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", Me.NullConvertString(.Item("WH_TAB_STATUS")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", Me.NullConvertString(.Item("WH_TAB_YN")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "出荷(中)更新パラメータ設定"

    ''' <summary>
    ''' 出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.FormatNumValue(.Item("BACKLOG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.FormatNumValue(.Item("BACKLOG_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.FormatNumValue(.Item("OUTKA_M_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "作業更新パラメータ設定"

    ''' <summary>
    ''' 作業の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "運送(大)パラメータ設定"

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", Me.FormatNumValue(.Item("UNSO_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", Me.FormatNumValue(.Item("UNSO_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

#End Region

#Region "運送(中)パラメータ設定"

    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", Me.FormatNumValue(.Item("UNSO_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", Me.FormatNumValue(.Item("UNSO_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "届先マスタ更新パラメータ設定(日本合成化学専用)"

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestUpdateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")

            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "届先マスタ自動追加用パラメータ設定(日本合成化学専用)"

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestInsertParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CD", Me.NullConvertString(.Item("EDI_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_DEST_CD", Me.NullConvertString(.Item("CUST_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SALES_CD", Me.NullConvertString(.Item("SALES_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_CD", Me.NullConvertString(.Item("SP_UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_BR_CD", Me.NullConvertString(.Item("SP_UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELI_ATT", Me.NullConvertString(.Item("DELI_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CARGO_TIME_LIMIT", Me.NullConvertString(.Item("CARGO_TIME_LIMIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LARGE_CAR_YN", Me.NullConvertString(.Item("LARGE_CAR_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FAX", Me.NullConvertString(.Item("FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_SEIQTO_CD", Me.NullConvertString(.Item("UNCHIN_SEIQTO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", Me.NullConvertZero(.Item("KYORI")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_CHAKU_KB", Me.NullConvertString(.Item("MOTO_CHAKU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@URIAGE_CD", Me.NullConvertString(.Item("URIAGE_CD")), DBDataType.NVARCHAR))

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
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))       '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", Me.FormatNumValue(.Item("SEIQ_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.CHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI送信(TBL)新規登録パラメータ設定"

    ''' <summary>
    ''' EDI送信(TBL)の新規登録パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendCreateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            Dim updTimeNormal As String = MyBase.GetSystemTime().Substring(0, 6)

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me.NullConvertString(.Item("INOUT_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_EDA", Me.NullConvertString(.Item("EDI_CTL_NO_EDA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(.Item("INKA_CTL_NO_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(.Item("INKA_CTL_NO_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_S", Me.NullConvertString(.Item("INKA_CTL_NO_S")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", Me.NullConvertString(.Item("OUTKA_CTL_NO_CHU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_SHO", Me.NullConvertString(.Item("OUTKA_CTL_NO_SHO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S0_MSYM_KEY", Me.NullConvertString(.Item("S0_MSYM_KEY")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MTRANS_HEAD", Me.NullConvertString(.Item("S1_MTRANS_HEAD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MPART_CD", Me.NullConvertString(.Item("S1_MPART_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MFILLER1", Me.NullConvertString(.Item("S1_MFILLER1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MCLIENT_CD1", Me.NullConvertString(.Item("S1_MCLIENT_CD1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MCOMPANY_CD1", Me.NullConvertString(.Item("S1_MCOMPANY_CD1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MFILLER2", Me.NullConvertString(.Item("S1_MFILLER2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MPRO_VERSION", Me.NullConvertString(.Item("S1_MPRO_VERSION")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MMESSAGE_TYP1", Me.NullConvertString(.Item("S1_MMESSAGE_TYP1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MDATE", Me.NullConvertString(.Item("S1_MDATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MRENBAN", Me.NullConvertString(.Item("S1_MRENBAN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S1_MFILLER3", Me.NullConvertString(.Item("S1_MFILLER3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MREC_TYP2", Me.NullConvertString(.Item("S2_MREC_TYP2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MVAN_NO", Me.NullConvertString(.Item("S2_MVAN_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MESSAGE_TYP2", Me.NullConvertString(.Item("S2_MESSAGE_TYP2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MREQ_FLAG", Me.NullConvertString(.Item("S2_MREQ_FLAG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MSYS_DATE", Me.NullConvertString(.Item("S2_MSYS_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MSYS_TIME", Me.NullConvertString(.Item("S2_MSYS_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MCLIENT_CD2", Me.NullConvertString(.Item("S2_MCLIENT_CD2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MCOMPANY_CD2", Me.NullConvertString(.Item("S2_MCOMPANY_CD2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MDEL_PLANT_CD", Me.NullConvertString(.Item("S2_MDEL_PLANT_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MDEL_NOTE_NO", Me.NullConvertString(.Item("S2_MDEL_NOTE_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MSHIPMENT_NO", Me.NullConvertString(.Item("S2_MSHIPMENT_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MSHIP_DATE", Me.NullConvertString(.Item("S2_MSHIP_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MRECE_DATE", Me.NullConvertString(.Item("S2_MRECE_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MCARR_BP_CD", Me.NullConvertString(.Item("S2_MCARR_BP_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MDAIKAN1", Me.NullConvertString(.Item("S2_MDAIKAN1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MSOLD_BP_CD", Me.NullConvertString(.Item("S2_MSOLD_BP_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MCUST_ORD_NO", Me.NullConvertString(.Item("S2_MCUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MCON_NM_KAN", Me.NullConvertString(.Item("S2_MCON_NM_KAN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MCON_ADDRESS_KAN", Me.NullConvertString(.Item("S2_MCON_ADDRESS_KAN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S2_MFILLER4", Me.NullConvertString(.Item("S2_MFILLER4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MREC_TYP3", Me.NullConvertString(.Item("S3_MREC_TYP3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MITEM_NO", Me.NullConvertString(.Item("S3_MITEM_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MMATERIAL_CD", Me.NullConvertString(.Item("S3_MMATERIAL_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MSTLOC_NO", Me.NullConvertString(.Item("S3_MSTLOC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MLOT_NO", Me.NullConvertString(.Item("S3_MLOT_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MORG_ORD_QTY", Me.NullConvertZero(.Item("S3_MORG_ORD_QTY")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@S3_MORG_ORD_UNIT", Me.NullConvertString(.Item("S3_MORG_ORD_UNIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MGOOD_QTY", Me.NullConvertZero(.Item("S3_MGOOD_QTY")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@S3_MGOOD_UNIT", Me.NullConvertString(.Item("S3_MGOOD_UNIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MDAIKAN_QTY", Me.NullConvertZero(.Item("S3_MDAIKAN_QTY")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@S3_MDAIKAN_UNIT", Me.NullConvertString(.Item("S3_MDAIKAN_UNIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MDOW_YOKO_NO", Me.NullConvertString(.Item("S3_MDOW_YOKO_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MDOW_YOKO_ITEM_NO", Me.NullConvertString(.Item("S3_MDOW_YOKO_ITEM_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MIDT_QTY", Me.NullConvertZero(.Item("S3_MIDT_QTY")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@S3_MIDT_UNIT", Me.NullConvertString(.Item("S3_MIDT_UNIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MTEXT", Me.NullConvertString(.Item("S3_MTEXT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S3_MFILLER5", Me.NullConvertString(.Item("S3_MFILLER5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S0_MMATERIAL_NAME1", Me.NullConvertString(.Item("S0_MMATERIAL_NAME1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S0_MMATERIAL_NAME2", Me.NullConvertString(.Item("S0_MMATERIAL_NAME2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S0_MSTATUS", Me.NullConvertString(.Item("S0_MSTATUS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@S0_MFLAG", Me.NullConvertString(.Item("S0_MFLAG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.NullConvertString(.Item("SEND_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.NullConvertString(.Item("SEND_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", Me.NullConvertString(.Item("SEND_TIME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", updTime, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.NVARCHAR))


        End With

    End Sub

#End Region

#Region "同一まとめデータ取得パラメータ設定(実績作成時)"
    ''' <summary>
    ''' 同一まとめデータ取得パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks>ダウケミは現状まとめ処理はないが、切り替えた時の為に記載</remarks>
    Private Sub SetMatomeSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", conditionRow.Item("MATOME_NO"), DBDataType.NVARCHAR))

    End Sub

#End Region

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

#Region "時間コロン編集"
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

#Region "UPDATE文の発行"
    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <remarks></remarks>
    Private Sub UpdateResultChk(ByVal cmd As SqlCommand)

        Dim updateCnt As Integer = 0

        updateCnt = MyBase.GetUpdateResult(cmd)
        'SQLの発行
        If updateCnt < 1 Then
            MyBase.SetMessage("E011")
        End If

        MyBase.SetResultCount(updateCnt)

    End Sub
#End Region

#End Region

#Region "届先マスタ"

    ''' <summary>
    ''' 件数取得処理(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMdestOutkaToroku(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC107.SQL_SELECT_M_DEST)

        If dt.Rows(0)("DEST_CD").ToString() = String.Empty Then
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.EDI_CD = @EDI_DEST_CD")
        Else
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.DEST_CD = @DEST_CD")
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestParameter(dt, 1)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC107.SQL_GROUP_BY_M_DEST_COUNT)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC107", "SelectDataMdestOutkaToroku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim destCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetDestMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST")

            '処理件数の設定
            destCnt = ds.Tables("LMH030_M_DEST").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(destCnt)
        Return ds
    End Function

    ''' <summary>
    ''' 届先マスタをHashTableに設定
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        '取得データの格納先をマッピング
        map.Add("MST_CNT", "MST_CNT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CUST_DEST_CD", "CUST_DEST_CD")
        map.Add("SALES_CD", "SALES_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("DELI_ATT", "DELI_ATT")
        map.Add("CARGO_TIME_LIMIT", "CARGO_TIME_LIMIT")
        map.Add("LARGE_CAR_YN", "LARGE_CAR_YN")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("JIS", "JIS")
        map.Add("KYORI", "KYORI")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("MOTO_CHAKU_KB", "MOTO_CHAKU_KB")
        map.Add("URIAGE_CD", "URIAGE_CD")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return map

    End Function


#End Region

#End Region

End Class
