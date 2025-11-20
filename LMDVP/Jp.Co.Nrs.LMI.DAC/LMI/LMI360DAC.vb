' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI360  : ＤＩＣ運賃請求明細書作成
'  作  成  者       :  [篠原]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI360DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI360DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "変数"

#End Region

#Region "DIC横持ち運賃テーブルの削除"

    ''' <summary>
    ''' DIC横持ち運賃テーブルの削除 SQL DELETE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_YOKOUNCHIN As String = "DELETE FROM $LM_TRN$..I_DIC_UNCHIN_TRS " & vbNewLine '

#End Region

#Region "DIC横持ち運賃テーブルに追加するデータの検索"

#Region "DIC横持ち運賃テーブルに追加するデータの検索 SQL SELECT句"

    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MAKEDATA As String = "	SELECT DISTINCT  --出荷                                                                 " & vbNewLine _
                                                & "  FL.NRS_BR_CD              AS NRS_BR_CD        --NRS_BR_CD                               " & vbNewLine _
                                                & "	,UT.CUST_CD_L              AS CUST_CD_L        --CUST_CD_L                               " & vbNewLine _
                                                & "	,UT.CUST_CD_M              AS CUST_CD_M        --CUST_CD_M                               " & vbNewLine _
                                                & "	,UT.CUST_CD_S              AS CUST_CD_S        --CUST_CD_S                               " & vbNewLine _
                                                & "	,UT.CUST_CD_SS             AS CUST_CD_SS       --CUST_CD_SS                              " & vbNewLine _
                                                & "	,FL.MOTO_DATA_KB           AS MOTO_DATA_KB     --MOTO_DATA_KB                            " & vbNewLine _
                                                & "	,FL.OUTKA_PLAN_DATE        AS OUTKA_PLAN_DATE  --OUTKA_PLAN_DATE                         " & vbNewLine _
                                                & "	,FM.UNSO_TTL_QT            AS UNSO_WT          --WT                                      " & vbNewLine _
                                                & " ,UT.SEIQ_WT                AS SEIQ_WT          --SEIQ_WT                                 " & vbNewLine _
                                                & "	,UT.SEIQ_NG_NB             AS SEIQ_NG_NB       --SEIQ_NG_NB                              " & vbNewLine _
                                                & "	,UT.SEIQ_TARIFF_CD         AS SEIQ_TARIFF_CD   --SEIQ_TARIFF_CD                          " & vbNewLine _
                                                & "	,UT.SEIQ_DANGER_KB         AS SEIQ_DANGER_KB   --SEIQ_DANGER_KB                          " & vbNewLine _
                                                & "	,UT.DECI_UNCHIN            AS DECI_UNCHIN      --DECI_UNCHIN                             " & vbNewLine _
                                                & " ,FM.UNSO_NO_L              AS UNSO_NO_L        --UNSO_NO                                 " & vbNewLine _
                                                & "	,FM.UNSO_NO_M              AS UNSO_NO_M        --UNSO_NO_M                               " & vbNewLine _
                                                & " ,FL.INOUTKA_NO_L           AS INOUTKA_NO_L     --INOUTKA_CTL_NO                          " & vbNewLine _
                                                & " ,OM.OUTKA_NO_L             AS OUTKA_NO_L       --OUTKA_CTL_NO  --■■OUTKA_M B ■■--    " & vbNewLine _
                                                & "	,OM.OUTKA_NO_M             AS OUTKA_NO_M       --OUTKA_CTL_NO_CHU                        " & vbNewLine _
                                                & " ,MG.GOODS_CD_NRS           AS GOODS_CD_NRS     --NRS_GOODS_CD                            " & vbNewLine _
                                                & " ,MG.GOODS_CD_CUST          AS GOODS_CD_CUST    --CUST_GOODS_CD                           " & vbNewLine _
                                                & "	,MG.GOODS_NM_1             AS GOODS_NM         --GOODS_NM                                " & vbNewLine _
                                                & " ,''                        AS INKA_NO_L        --OUTKA_CTL_NO  --■■INKA_S C ■■--     " & vbNewLine _
                                                & "	,''                        AS INKA_NO_M        --OUTKA_CTL_NO_CHU                        " & vbNewLine _
                                                & "	,''                        AS INKA_NO_S                                                  " & vbNewLine _
                                                & "	,OS.LOT_NO                 AS LOT_NO           --LOT_NO  --■■OUTKA_M B ■■--          " & vbNewLine _
                                                & "-- ,'0'                         AS NB               --NB                                  " & vbNewLine _
                                                & " ,OM.OUTKA_TTL_NB             AS NB               --NB                                    " & vbNewLine _
                                                & "	,ISNULL(OM.IRIME,'0')      AS IRIME            --IRIME                               " & vbNewLine _
                                                & "-- ,ISNULL(OS.ALCTD_QT /                                                                    " & vbNewLine _
                                                & "--  NULLIF(OS.OUTKA_TTL_NB,'0'),'0') AS IRIME                                               " & vbNewLine _
                                                & "	,OM.IRIME_UT                 AS IRIME_UT         --IRIME_UT                              " & vbNewLine _
                                                & "	,ISNULL(OM.OUTKA_TTL_NB,'0') AS OUTKA_TTL_NB     --OUTKA_TTL_NB                          " & vbNewLine _
                                                & "	,ISNULL(YTD.KGS_PRICE,'0')   AS KGS_PRICE        --KGS_PRICE  --■■YOKO_TARIFF_DTL D■■--" & vbNewLine _
                                                & "	,'0'                         AS CAL_WT                                                   " & vbNewLine _
                                                & "	,'0'                         AS CAL_SURYO                                                " & vbNewLine _
                                                & "	,'0'                         AS CAL_UNCHIN                                               " & vbNewLine _
                                                & " ,ISNULL(OS.ALCTD_NB / NULLIF(MG.PKG_NB,'0'),'0')      AS KONSU                           " & vbNewLine _
                                                & " ,ISNULL(OS.ALCTD_NB % NULLIF(MG.PKG_NB,'0'),'0')      AS HASU                            " & vbNewLine _
                                                & " ,ISNULL(MG.PKG_NB,'0')       AS PKG_NB                                                   " & vbNewLine _
                                                & " ,'0'                         AS SURYO                                                    " & vbNewLine _
                                                & " ,''                          AS INOUTKA_NO_M                                             " & vbNewLine _
                                                & " ,''                          AS INOUTKA_NO_S                                             " & vbNewLine



    Private Const SQL_SELECT_MAKEDATA_IN As String = "	SELECT DISTINCT  --IN_PTN                                                                  " & vbNewLine _
                                                   & "   FL.NRS_BR_CD              AS NRS_BR_CD        --NRS_BR_CD                                 " & vbNewLine _
                                                   & "	,UT.CUST_CD_L              AS CUST_CD_L        --CUST_CD_L                                 " & vbNewLine _
                                                   & "	,UT.CUST_CD_M              AS CUST_CD_M        --CUST_CD_M                                 " & vbNewLine _
                                                   & "	,UT.CUST_CD_S              AS CUST_CD_S        --CUST_CD_S                                 " & vbNewLine _
                                                   & "	,UT.CUST_CD_SS             AS CUST_CD_SS       --CUST_CD_SS                                " & vbNewLine _
                                                   & "	,FL.MOTO_DATA_KB           AS MOTO_DATA_KB     --MOTO_DATA_KB                              " & vbNewLine _
                                                   & "	,FL.OUTKA_PLAN_DATE        AS OUTKA_PLAN_DATE  --OUTKA_PLAN_DATE                           " & vbNewLine _
                                                   & "  ,FM.UNSO_TTL_QT            AS UNSO_WT          --UNSO_WT                                   " & vbNewLine _
                                                   & "  ,UT.SEIQ_WT                AS SEIQ_WT          --SEIQ_WT                                   " & vbNewLine _
                                                   & "	,''                        AS SEIQ_NG_NB       --SEIQ_NG_NB入荷では不要                    " & vbNewLine _
                                                   & "	,UT.SEIQ_TARIFF_CD         AS SEIQ_TARIFF_CD   --SEIQ_TARIFF_CD                            " & vbNewLine _
                                                   & "	,UT.SEIQ_DANGER_KB         AS SEIQ_DANGER_KB   --SEIQ_DANGER_KB                            " & vbNewLine _
                                                   & "	,UT.DECI_UNCHIN            AS DECI_UNCHIN      --DECI_UNCHIN                               " & vbNewLine _
                                                   & "  ,FM.UNSO_NO_L              AS UNSO_NO_L        --UNSO_NO                                   " & vbNewLine _
                                                   & "	,FM.UNSO_NO_M              AS UNSO_NO_M        --UNSO_NO_M                                 " & vbNewLine _
                                                   & "  ,FL.INOUTKA_NO_L           AS INOUTKA_NO_L     --INOUTKA_CTL_NO                            " & vbNewLine _
                                                   & "  ,''                        AS OUTKA_NO_L       --OUTKA_CTL_NO                              " & vbNewLine _
                                                   & "  ,''                        AS OUTKA_NO_M       --OUTKA_CTL_NO                              " & vbNewLine _
                                                   & "  ,MG.GOODS_CD_NRS           AS GOODS_CD_NRS     --NRS_GOODS_CD　　--■■INKA_M B ■■--     " & vbNewLine _
                                                   & "  ,MG.GOODS_CD_CUST          AS GOODS_CD_CUST    --CUST_GOODS_CD                             " & vbNewLine _
                                                   & "	,MG.GOODS_NM_1             AS GOODS_NM         --GOODS_NM                                  " & vbNewLine _
                                                   & "  ,INS.INKA_NO_L             AS INKA_NO_L        --OUTKA_CTL_NO  --■■INKA_S C ■■--       " & vbNewLine _
                                                   & "	,INS.INKA_NO_M             AS INKA_NO_M        --OUTKA_CTL_NO_CHU                          " & vbNewLine _
                                                   & "	,INS.INKA_NO_S             AS INKA_NO_S        --                                          " & vbNewLine _
                                                   & "	,INS.LOT_NO                AS LOT_NO           --LOT_NO                                    " & vbNewLine _
                                                   & "  --,'0'                       AS NB               --NB                                      " & vbNewLine _
                                                   & "  ,ISNULL(INS.KONSU,'0') *                                                                   " & vbNewLine _
                                                   & "   ISNULL(MG.PKG_NB,'0') +                                --NB                               " & vbNewLine _
                                                   & "   ISNULL(INS.HASU,'0')      AS NB                                                           " & vbNewLine _
                                                   & "  ,ISNULL(INS.IRIME,'0')     AS IRIME            --IRIME                                     " & vbNewLine _
                                                   & "	,STD_IRIME_UT              AS IRIME_UT	        --IRIME_UT                                 " & vbNewLine _
                                                   & "  ,'0'                       AS OUTKA_TTL_NB     --OUTKA_TTL_NB                              " & vbNewLine _
                                                   & "	,ISNULL(YTD.KGS_PRICE,'0') AS KGS_PRICE        --KGS_PRICE                                 " & vbNewLine _
                                                   & "  ,'0'                       AS CAL_WT                                                       " & vbNewLine _
                                                   & "	,'0'                       AS CAL_SURYO                                                    " & vbNewLine _
                                                   & "	,'0'                       AS CAL_UNCHIN                                                   " & vbNewLine _
                                                   & "  ,ISNULL(INS.KONSU,'0')     AS KONSU                                                        " & vbNewLine _
                                                   & "  ,ISNULL(INS.HASU,'0')      AS HASU                                                         " & vbNewLine _
                                                   & "  ,ISNULL(MG.PKG_NB,'0')     AS PKG_NB                                                       " & vbNewLine _
                                                   & " ,'0'                        AS SURYO                                                        " & vbNewLine _
                                                   & " ,''                         AS INOUTKA_NO_M                                                 " & vbNewLine _
                                                   & " ,''                         AS INOUTKA_NO_S                                                 " & vbNewLine


#End Region

#Region "DIC横持ち運賃テーブルに追加するデータの検索 SQL FROM句"

    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MAKEDATA As String = "    FROM $LM_TRN$..F_UNSO_L FL                 " & vbNewLine _
                                                     & "    LEFT JOIN $LM_TRN$..F_UNSO_M FM            " & vbNewLine _
                                                     & "    ON  FL.NRS_BR_CD   = FM.NRS_BR_CD          " & vbNewLine _
                                                     & "	AND FL.UNSO_NO_L   = FM.UNSO_NO_L          " & vbNewLine _
                                                     & "	AND FM.SYS_DEL_FLG ='0'                    " & vbNewLine _
                                                     & "	LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT        " & vbNewLine _
                                                     & "	ON  FM.NRS_BR_CD   = UT.NRS_BR_CD          " & vbNewLine _
                                                     & "	AND FM.UNSO_NO_L   = UT.UNSO_NO_L          " & vbNewLine _
                                                     & "	AND FM.UNSO_NO_M   = UT.UNSO_NO_M          " & vbNewLine _
                                                     & "    AND UT.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                     & "	LEFT JOIN $LM_TRN$..C_OUTKA_M OM           " & vbNewLine _
                                                     & "	ON  OM.NRS_BR_CD   = FL.NRS_BR_CD          " & vbNewLine _
                                                     & "	AND OM.OUTKA_NO_L  = FL.INOUTKA_NO_L       " & vbNewLine _
                                                     & "	AND OM.OUTKA_NO_M  = FM.UNSO_NO_M          " & vbNewLine _
                                                     & "	AND OM.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                     & "	LEFT JOIN $LM_TRN$..C_OUTKA_S OS           " & vbNewLine _
                                                     & "	ON  OS.NRS_BR_CD   = OM.NRS_BR_CD          " & vbNewLine _
                                                     & "	AND OS.OUTKA_NO_L  = OM.OUTKA_NO_L         " & vbNewLine _
                                                     & "	AND OS.OUTKA_NO_M  = OM.OUTKA_NO_M         " & vbNewLine _
                                                     & "	AND OS.OUTKA_NO_S  = '001'                 " & vbNewLine _
                                                     & "	AND OS.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                     & "	LEFT JOIN $LM_MST$..M_YOKO_TARIFF_DTL YTD  " & vbNewLine _
                                                     & "	ON  FL.NRS_BR_CD      = YTD.NRS_BR_CD      " & vbNewLine _
                                                     & "	AND UT.SEIQ_TARIFF_CD = YTD.YOKO_TARIFF_CD " & vbNewLine _
                                                     & "    AND UT.SEIQ_DANGER_KB = YTD.DANGER_KB      " & vbNewLine _
                                                     & "    LEFT JOIN $LM_MST$..M_NRS_BR NB            " & vbNewLine _
                                                     & "	ON  UT.NRS_BR_CD = NB.NRS_BR_CD            " & vbNewLine _
                                                     & "	LEFT JOIN $LM_MST$..M_CUST MC              " & vbNewLine _
                                                     & "	ON  UT.NRS_BR_CD  = MC.NRS_BR_CD           " & vbNewLine _
                                                     & "	AND UT.CUST_CD_L  = MC.CUST_CD_L           " & vbNewLine _
                                                     & "	AND UT.CUST_CD_M  = MC.CUST_CD_M           " & vbNewLine _
                                                     & "	AND UT.CUST_CD_S  = MC.CUST_CD_S           " & vbNewLine _
                                                     & "	AND UT.CUST_CD_SS = MC.CUST_CD_SS          " & vbNewLine _
                                                     & "	LEFT JOIN $LM_MST$..M_GOODS MG             " & vbNewLine _
                                                     & "	ON  UT.NRS_BR_CD    = MG.NRS_BR_CD         " & vbNewLine _
                                                     & "	AND FM.GOODS_CD_NRS = MG.GOODS_CD_NRS      " & vbNewLine

    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MAKEDATA_IN As String = "    FROM $LM_TRN$..F_UNSO_L FL                 " & vbNewLine _
                                                        & "	   LEFT JOIN $LM_TRN$..F_UNSO_M AS FM   -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                                        & "    ON  FL.NRS_BR_CD   = FM.NRS_BR_CD          " & vbNewLine _
                                                        & "    AND FL.UNSO_NO_L   = FM.UNSO_NO_L          " & vbNewLine _
                                                        & "	   AND FM.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                        & "	   LEFT JOIN $LM_TRN$..F_UNCHIN_TRS UT        " & vbNewLine _
                                                        & "	   ON  FM.NRS_BR_CD   = UT.NRS_BR_CD          " & vbNewLine _
                                                        & "	   AND FM.UNSO_NO_L   = UT.UNSO_NO_L          " & vbNewLine _
                                                        & "	   AND FM.UNSO_NO_M   = UT.UNSO_NO_M          " & vbNewLine _
                                                        & "    AND UT.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                        & "	   LEFT JOIN $LM_TRN$..B_INKA_M IM            " & vbNewLine _
                                                        & "	   ON  IM.NRS_BR_CD   = FL.NRS_BR_CD          " & vbNewLine _
                                                        & "	   AND IM.INKA_NO_L   = FL.INOUTKA_NO_L       " & vbNewLine _
                                                        & "	   AND IM.INKA_NO_M   = FM.UNSO_NO_M          " & vbNewLine _
                                                        & "	   AND IM.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                                        & "	   LEFT JOIN $LM_TRN$..B_INKA_S INS           " & vbNewLine _
                                                        & "	   ON  IM.NRS_BR_CD    = INS.NRS_BR_CD        " & vbNewLine _
                                                        & "	   AND IM.INKA_NO_L    = INS.INKA_NO_L        " & vbNewLine _
                                                        & "	   AND IM.INKA_NO_M    = INS.INKA_NO_M        " & vbNewLine _
                                                        & "	   AND INS.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                                        & "    LEFT JOIN $LM_MST$..M_YOKO_TARIFF_DTL YTD  " & vbNewLine _
                                                        & "	   ON  FL.NRS_BR_CD      = YTD.NRS_BR_CD      " & vbNewLine _
                                                        & "	   AND UT.SEIQ_TARIFF_CD = YTD.YOKO_TARIFF_CD " & vbNewLine _
                                                        & "    AND UT.SEIQ_DANGER_KB = YTD.DANGER_KB      " & vbNewLine _
                                                        & "    LEFT JOIN $LM_MST$..M_NRS_BR NB            " & vbNewLine _
                                                        & "	   ON  UT.NRS_BR_CD = NB.NRS_BR_CD            " & vbNewLine _
                                                        & "    LEFT JOIN $LM_MST$..M_CUST MC              " & vbNewLine _
                                                        & "	   ON  UT.NRS_BR_CD  = MC.NRS_BR_CD           " & vbNewLine _
                                                        & "    AND UT.CUST_CD_L  = MC.CUST_CD_L           " & vbNewLine _
                                                        & "	   AND UT.CUST_CD_M  = MC.CUST_CD_M           " & vbNewLine _
                                                        & "    AND UT.CUST_CD_S  = MC.CUST_CD_S           " & vbNewLine _
                                                        & "	   AND UT.CUST_CD_SS = MC.CUST_CD_SS          " & vbNewLine _
                                                        & "	   LEFT JOIN $LM_MST$..M_GOODS MG             " & vbNewLine _
                                                        & "    ON  UT.NRS_BR_CD    = MG.NRS_BR_CD         " & vbNewLine _
                                                        & "	   AND FM.GOODS_CD_NRS = MG.GOODS_CD_NRS      " & vbNewLine

#End Region

#Region "DIC横持ち運賃テーブルに追加するデータの検索 SQL ORDER BY句"

    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_MAKEDATA As String = "ORDER BY                                                           " & vbNewLine _
                                                      & " FL.NRS_BR_CD                                                      " & vbNewLine _
                                                      & ",UT.CUST_CD_L                                                      " & vbNewLine _
                                                      & ",UT.CUST_CD_M                                                      " & vbNewLine _
                                                      & ",UT.CUST_CD_S                                                      " & vbNewLine _
                                                      & ",UT.CUST_CD_SS                                                     " & vbNewLine _
                                                      & ",FL.MOTO_DATA_KB                                                   " & vbNewLine _
                                                      & ",FL.OUTKA_PLAN_DATE                                                " & vbNewLine _
                                                      & ",MG.GOODS_NM_1                                                     " & vbNewLine _
                                                      & ",OS.LOT_NO                                                         " & vbNewLine _

    '''' <summary>
    '''' DIC横持ち運賃テーブルに追加するデータの検索 SQL ORDER BY句
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_ORDER_MAKEDATA_IN As String = "ORDER BY                                                        " & vbNewLine _
    '                                                  & " FL.NRS_BR_CD                                                      " & vbNewLine _
    '                                                  & ",UT.CUST_CD_L                                                      " & vbNewLine _
    '                                                  & ",UT.CUST_CD_M                                                      " & vbNewLine _
    '                                                  & ",UT.CUST_CD_S                                                      " & vbNewLine _
    '                                                  & ",UT.CUST_CD_SS                                                     " & vbNewLine _
    '                                                  & ",FL.MOTO_DATA_KB                                                   " & vbNewLine _
    '                                                  & ",FL.OUTKA_PLAN_DATE                                                " & vbNewLine _
    '                                                  & ",MG.GOODS_NM_1                                                     " & vbNewLine _
    '                                                  & ",INS.LOT_NO                                                        " & vbNewLine


    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_MAKEDATA_IN As String = "ORDER BY                                                        " & vbNewLine _
                                                      & " FL.NRS_BR_CD                                                      " & vbNewLine _
                                                      & "--,UT.CUST_CD_L                                                    " & vbNewLine _
                                                      & "--,UT.CUST_CD_M                                                    " & vbNewLine _
                                                      & "--,UT.CUST_CD_S                                                    " & vbNewLine _
                                                      & ",UT.CUST_CD_SS                                                     " & vbNewLine _
                                                      & ",FL.MOTO_DATA_KB                                                   " & vbNewLine _
                                                      & ",FL.OUTKA_PLAN_DATE                                                " & vbNewLine _
                                                      & ",INS.INKA_NO_L                                                     " & vbNewLine _
                                                      & ",INS.INKA_NO_M                                                     " & vbNewLine _
                                                      & ",INS.INKA_NO_S DESC                                                " & vbNewLine _
                                                      & "--,MG.GOODS_NM_1                                                   " & vbNewLine _
                                                      & "--,INS.LOT_NO                                                      " & vbNewLine


#End Region

#End Region

#Region "DIC横持ち運賃テーブルの新規作成"

    ''' <summary>
    ''' DIC横持ち運賃テーブル新規作成 SQL INSERT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_YOKOUNCHIN As String = "INSERT INTO $LM_TRN$..I_DIC_UNCHIN_TRS            " & vbNewLine _
                                                 & " ( 		                                           " & vbNewLine _
                                                 & "NRS_BR_CD,                                         " & vbNewLine _
                                                 & "CUST_CD_L,                                         " & vbNewLine _
                                                 & "CUST_CD_M,                                         " & vbNewLine _
                                                 & "CUST_CD_S,                                         " & vbNewLine _
                                                 & "CUST_CD_SS,                                        " & vbNewLine _
                                                 & "MOTO_DATA_KB,                                      " & vbNewLine _
                                                 & "OUTKA_PLAN_DATE,                                   " & vbNewLine _
                                                 & "GOODS_CD_NRS,                                      " & vbNewLine _
                                                 & "GOODS_CD_CUST,                                     " & vbNewLine _
                                                 & "GOODS_NM_1,                                        " & vbNewLine _
                                                 & "LOT_NO,                                            " & vbNewLine _
                                                 & "SEIQ_TARIFF_CD,                                    " & vbNewLine _
                                                 & "SEIQ_DANGER_KB,                                    " & vbNewLine _
                                                 & "KGS_PRICE,                                         " & vbNewLine _
                                                 & "IRIME,                                             " & vbNewLine _
                                                 & "IRIME_UT,                                          " & vbNewLine _
                                                 & "NB,                                                " & vbNewLine _
                                                 & "SURYO,                                             " & vbNewLine _
                                                 & "DECI_UNCHIN,                                       " & vbNewLine _
                                                 & "CAL_WT,                                            " & vbNewLine _
                                                 & "CAL_SURYO,                                         " & vbNewLine _
                                                 & "CAL_UNCHIN,                                        " & vbNewLine _
                                                 & "UNSO_NO_L,                                         " & vbNewLine _
                                                 & "UNSO_NO_M,                                         " & vbNewLine _
                                                 & "INOUTKA_NO_L,                                      " & vbNewLine _
                                                 & "INOUTKA_NO_M,                                      " & vbNewLine _
                                                 & "INOUTKA_NO_S,                                      " & vbNewLine _
                                                 & "SYS_ENT_DATE,                                      " & vbNewLine _
                                                 & "SYS_ENT_TIME,                                      " & vbNewLine _
                                                 & "SYS_ENT_PGID,                                      " & vbNewLine _
                                                 & "SYS_ENT_USER,                                      " & vbNewLine _
                                                 & "SYS_UPD_DATE,                                      " & vbNewLine _
                                                 & "SYS_UPD_TIME,                                      " & vbNewLine _
                                                 & "SYS_UPD_PGID,                                      " & vbNewLine _
                                                 & "SYS_UPD_USER,                                      " & vbNewLine _
                                                 & "SYS_DEL_FLG                                        " & vbNewLine _
                                                 & " ) VALUES (                                        " & vbNewLine _
                                                 & "@NRS_BR_CD,                                        " & vbNewLine _
                                                 & "@CUST_CD_L,                                        " & vbNewLine _
                                                 & "@CUST_CD_M,                                        " & vbNewLine _
                                                 & "@CUST_CD_S,                                        " & vbNewLine _
                                                 & "@CUST_CD_SS,                                       " & vbNewLine _
                                                 & "@MOTO_DATA_KB,                                     " & vbNewLine _
                                                 & "@OUTKA_PLAN_DATE,                                  " & vbNewLine _
                                                 & "@GOODS_CD_NRS,                                     " & vbNewLine _
                                                 & "@GOODS_CD_CUST,                                    " & vbNewLine _
                                                 & "@GOODS_NM_1,                                       " & vbNewLine _
                                                 & "@LOT_NO,                                           " & vbNewLine _
                                                 & "@SEIQ_TARIFF_CD,                                   " & vbNewLine _
                                                 & "@SEIQ_DANGER_KB,                                   " & vbNewLine _
                                                 & "@KGS_PRICE,                                        " & vbNewLine _
                                                 & "@IRIME,                                            " & vbNewLine _
                                                 & "@IRIME_UT,                                         " & vbNewLine _
                                                 & "@NB,                                               " & vbNewLine _
                                                 & "@SURYO,                                            " & vbNewLine _
                                                 & "@DECI_UNCHIN,                                      " & vbNewLine _
                                                 & "@CAL_WT,                                           " & vbNewLine _
                                                 & "@CAL_SURYO,                                        " & vbNewLine _
                                                 & "@CAL_UNCHIN,                                       " & vbNewLine _
                                                 & "@UNSO_NO_L,                                        " & vbNewLine _
                                                 & "@UNSO_NO_M,                                        " & vbNewLine _
                                                 & "@INOUTKA_NO_L,                                     " & vbNewLine _
                                                 & "@INOUTKA_NO_M,                                     " & vbNewLine _
                                                 & "@INOUTKA_NO_S,                                     " & vbNewLine _
                                                 & "@SYS_ENT_DATE,                                     " & vbNewLine _
                                                 & "@SYS_ENT_TIME,                                     " & vbNewLine _
                                                 & "@SYS_ENT_PGID,                                     " & vbNewLine _
                                                 & "@SYS_ENT_USER,                                     " & vbNewLine _
                                                 & "@SYS_UPD_DATE,                                     " & vbNewLine _
                                                 & "@SYS_UPD_TIME,                                     " & vbNewLine _
                                                 & "@SYS_UPD_PGID,                                     " & vbNewLine _
                                                 & "@SYS_UPD_USER,                                     " & vbNewLine _
                                                 & "@SYS_DEL_FLG                                       " & vbNewLine _
                                                 & " )                                                 " & vbNewLine

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

#Region "DIC横持運賃データテーブルの物理削除"

    ''' <summary>
    ''' DIC横持運賃データテーブルの物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteYokoUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI360IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI360DAC.SQL_DELETE_YOKOUNCHIN)      'SQL構築(Delete句)
        Call SetSQLWhereYokoUnchinDEL(inTbl.Rows(0))            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString, inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI360DAC", "DeleteYokoUnchin", cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "DIC横持ち運賃テーブルに追加するデータの検索"

    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMakeData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI360IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI360DAC.SQL_SELECT_MAKEDATA)       'SQL構築 SELECT句
        Me._StrSql.Append(LMI360DAC.SQL_SELECT_FROM_MAKEDATA)  'SQL構築 FROM句
        Call SetSQLWhereMAKEDATA(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMI360DAC.SQL_SELECT_ORDER_MAKEDATA) 'SQL構築 ORDER句

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI360DAC", "SelectMakeData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NB", "NB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("KGS_PRICE", "KGS_PRICE")
        map.Add("CAL_WT", "CAL_WT")
        map.Add("CAL_SURYO", "CAL_SURYO")
        map.Add("CAL_UNCHIN", "CAL_UNCHIN")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("SURYO", "SURYO")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI360INOUT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' DIC横持ち運賃テーブルに追加するデータの検索_IN
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMakeData_IN(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI360IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI360DAC.SQL_SELECT_MAKEDATA_IN)       'SQL構築 SELECT句
        Me._StrSql.Append(LMI360DAC.SQL_SELECT_FROM_MAKEDATA_IN)  'SQL構築 FROM句
        Call SetSQLWhereMAKEDATA_IN(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMI360DAC.SQL_SELECT_ORDER_MAKEDATA_IN) 'SQL構築 ORDER句

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI360DAC", "SelectMakeDataIn", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NB", "NB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("KGS_PRICE", "KGS_PRICE")
        map.Add("CAL_WT", "CAL_WT")
        map.Add("CAL_SURYO", "CAL_SURYO")
        map.Add("CAL_UNCHIN", "CAL_UNCHIN")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("SURYO", "SURYO")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI360INOUT")

        reader.Close()

        Return ds

    End Function


#End Region


#Region "DIC横持ち運賃テーブルの新規追加"

    ''' <summary>
    ''' DIC横持ち運賃テーブルの新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertYokoUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI360INOUT")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI360DAC.SQL_INSERT_YOKOUNCHIN)         'SQL構築(INSERT句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            If Convert.ToDecimal(inTbl.Rows(i).Item("CAL_UNCHIN").ToString) > 0 Then 'YOKO_GAKUはCAL_UNCHINに。

                'パラメータの初期化
                cmd.Parameters.Clear()

                'SQLパラメータ初期化
                Me._SqlPrmList = New ArrayList()

                'SQLパラメータ（個別項目）設定
                Call Me.SetYokoUnchinParameter(inTbl.Rows(i), Me._SqlPrmList)

                'SQLパラメータ（システム項目）設定
                Call Me.SetParamCommonSystemIns()

                'パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMI360DAC", "InsertYokoUnchin", cmd)

                'SQLの発行
                MyBase.GetInsertResult(cmd)

            End If
        Next

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 DIC横持ち運賃テーブルの削除"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereYokoUnchinDEL(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("1 = 1                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '出荷日FROM
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出荷日TO
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 DIC横持ち運賃テーブルに追加するデータの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereMAKEDATA(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("FL.SYS_DEL_FLG = '0'                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND FL.MOTO_DATA_KB = '20'                                     ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND UT.SEIQ_FIXED_FLAG = '01'                                      ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日FROM
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '納入日TO
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール_IN
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereMAKEDATA_IN(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("FL.SYS_DEL_FLG = '0'                                           ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND FL.MOTO_DATA_KB = '10'                                     ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND UT.SEIQ_FIXED_FLAG = '01'                                      ")
            Me._StrSql.Append(vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日FROM
            whereStr = .Item("DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.OUTKA_PLAN_DATE >= @DATE_FROM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '納入日TO
            whereStr = .Item("DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND FL.OUTKA_PLAN_DATE <= @DATE_TO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATE_TO", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

#End Region

#Region "SQL条件設定 DIC横持ち運賃テーブルの新規追加"

    ''' <summary>
    ''' DIC横持ち運賃テーブルの新規追加
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetYokoUnchinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KGS_PRICE", .Item("KGS_PRICE").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NB", .Item("NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SURYO", .Item("SURYO").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CAL_WT", .Item("CAL_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CAL_SURYO", .Item("CAL_SURYO").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CAL_UNCHIN", .Item("CAL_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_M", .Item("INOUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_S", .Item("INOUTKA_NO_S").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

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

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#Region "後埋め設定"

    ''' <summary>
    ''' 後埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">後埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>後埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function AtoCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value, value2)
        Next

        Return value

    End Function

#End Region

#Region "文字分割"

    ''' <summary>
    ''' 文字分割
    ''' </summary>
    ''' <param name="inStr">分割対象文字</param>
    ''' <param name="inByte">分割単位バイト数</param>
    ''' <param name="inCnt">分割する数</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Function stringCut(ByVal inStr As String, ByVal inByte As Integer, ByVal inCnt As Integer) As String()

        Dim newCnt As Integer = inCnt - 1
        Dim newByte As Integer = inByte - 1
        Dim oldStr(newCnt) As String
        Dim newStr(newCnt) As String
        Dim byteCnt As Integer = 1

        For i As Integer = 0 To newCnt
            For j As Integer = 0 To newByte
                oldStr(i) = String.Concat(oldStr(i), Mid(inStr, byteCnt, 1))
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(oldStr(i)) <= newByte + 1 Then
                    newStr(i) = oldStr(i)
                    byteCnt = byteCnt + 1
                Else
                    Exit For
                End If
            Next
        Next

        Return newStr

    End Function

#End Region

#End Region

End Class
