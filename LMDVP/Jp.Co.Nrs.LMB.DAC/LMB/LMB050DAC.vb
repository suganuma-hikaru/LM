' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 
'  プログラムID     :  LMB050DAC : 
'  作  成  者       :  阿達
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"


#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = _
          " SELECT                                      " & vbNewLine _
        & "     COUNT(1)		   AS SELECT_CNT        " & vbNewLine

    ''' <summary>
    ''' データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = _
         " SELECT                                                 " & vbNewLine _
        & " MAIN.STATE AS  STATE                                  " & vbNewLine _
        & " ,MAIN.BUYER_ORD_NO_L AS  BUYER_ORD_NO_L               " & vbNewLine _
        & " ,MAIN.GOODS_CD_CUST AS  GOODS_CD_CUST                 " & vbNewLine _
        & " ,MAIN.GOODS_NM AS  GOODS_NM                           " & vbNewLine _
        & " ,MAIN.STD_IRIME_NB AS  STD_IRIME_NB                   " & vbNewLine _
        & " ,MAIN.STD_IRIME_UT AS  STD_IRIME_UT                   " & vbNewLine _
        & " ,MAIN.JISSEKI_INKA_NB AS JISSEKI_INKA_NB                " & vbNewLine _
        & " ,MAIN.NB_UT AS NB_UT                                  " & vbNewLine _
        & " ,MAIN.JISSEKI_INKA_QT AS JISSEKI_INKA_QT               " & vbNewLine _
        & " ,MAIN.PKG_UT AS PKG_UT                                " & vbNewLine _
        & " ,MAIN.PKG_NB AS  PKG_NB                               " & vbNewLine _
        & " ,MAIN.LOT_NO AS  LOT_NO                               " & vbNewLine _
        & " ,MAIN.OUTKA_MOTO_NM AS OUTKA_MOTO_NM                  " & vbNewLine _
        & " ,MAIN.REMARK_L AS  REMARK_L                           " & vbNewLine _
        & " ,MAIN.REMARK_M AS REMARK_M                            " & vbNewLine _
        & " ,MAIN.TOU_NO AS TOU_NO                                " & vbNewLine _
        & " ,MAIN.SITU_NO AS SITU_NO                              " & vbNewLine _
        & " ,MAIN.ZONE_CD AS ZONE_CD                              " & vbNewLine _
        & " ,MAIN.LOCA AS LOCA                                    " & vbNewLine _
        & " ,MAIN.OFB_KB AS OFB_KB                                " & vbNewLine _
        & " ,MAIN.GOODS_CD_NRS AS  GOODS_CD_NRS                   " & vbNewLine _
        & " ,MAIN.ONDO_KB AS ONDO_KB                              " & vbNewLine _
        & " ,MAIN.ONDO_STR_DATE AS  ONDO_STR_DATE                 " & vbNewLine _
        & " ,MAIN.ONDO_END_DATE AS ONDO_END_DATE                  " & vbNewLine _
        & " ,MAIN.SYS_UPD_DATE AS SYS_UPD_DATE                    " & vbNewLine _
        & " ,MAIN.SYS_UPD_TIME AS SYS_UPD_TIME                    " & vbNewLine _
        & " ,MAIN.M_GOODS_COUNT AS M_GOODS_COUNT                  " & vbNewLine _
        & " ,MAIN.M_GOODS_UT_NB_COUNT AS M_GOODS_UT_NB_COUNT      " & vbNewLine _
        & " ,MAIN.M_ZONE_COUNT AS M_ZONE_COUNT                    " & vbNewLine _
        & " ,MAIN.IRIME AS IRIME                                  " & vbNewLine _
        & " ,MAIN.JISSEKI_PKG_UT AS JISSEKI_PKG_UT                " & vbNewLine _
        & " ,MAIN.OUTKA_FROM_ORD_NO_L AS OUTKA_FROM_ORD_NO_L      " & vbNewLine _
        & " ,MAIN.CRT_DATE AS CRT_DATE                            " & vbNewLine _
        & " ,MAIN.STD_WT_KGS AS STD_WT_KGS                        " & vbNewLine _
        & "  FROM                                                 " & vbNewLine _
        & " (                                                     " & vbNewLine _
        & " SELECT                                                " & vbNewLine _
        & " Z1.KBN_NM1 AS STATE                                   " & vbNewLine _
        & " ,INRDC.INKA_NO_L + INRDC.INKA_NO_M + INRDC.INKA_NO_S AS BUYER_ORD_NO_L  " & vbNewLine _
        & " ,INRDC.GOODS_CD_CUST AS GOODS_CD_CUST                                   " & vbNewLine _
        & " ,INRDC.GOODS_NM_1 AS GOODS_NM                                           " & vbNewLine _
        & " ,ISNULL(MG2.STD_IRIME_NB,0) AS STD_IRIME_NB                             " & vbNewLine _
        & " ,ISNULL(MG2.STD_IRIME_UT,'') AS STD_IRIME_UT                            " & vbNewLine _
        & " ,INRDC.JISSEKI_INKA_NB AS JISSEKI_INKA_NB                               " & vbNewLine _
        & " ,ISNULL(MG2.NB_UT,'') AS NB_UT                                          " & vbNewLine _
        & " ,INRDC.JISSEKI_INKA_QT AS JISSEKI_INKA_QT                               " & vbNewLine _
        & " ,INRDC.PKG_UT AS PKG_UT                                                 " & vbNewLine _
        & " ,CASE WHEN INRDC.PKG_NB = 0 THEN MG1.PKG_NB ELSE INRDC.PKG_NB END AS PKG_NB " & vbNewLine _
        & " ,INRDC.LOT_NO AS LOT_NO                                                 " & vbNewLine _
        & " ,MD.DEST_NM AS OUTKA_MOTO_NM                                            " & vbNewLine _
        & " ,INRDC.REMARK_L AS REMARK_L                                             " & vbNewLine _
        & " ,INRDC.REMARK_M AS REMARK_M                                             " & vbNewLine _
        & " ,INRDC.TOU_NO AS TOU_NO                                                 " & vbNewLine _
        & " ,INRDC.SITU_NO AS SITU_NO                                               " & vbNewLine _
        & " ,INRDC.ZONE_CD AS ZONE_CD                                               " & vbNewLine _
        & " ,INRDC.LOCA AS LOCA                                                                          " & vbNewLine _
        & " ,INRDC.OFB_KB AS OFB_KB                                                                      " & vbNewLine _
        & " ,ISNULL(MG2.GOODS_CD_NRS,'') AS GOODS_CD_NRS                                                 " & vbNewLine _
        & " ,ISNULL(MG2.ONDO_KB,'') AS ONDO_KB                                                           " & vbNewLine _
        & " ,ISNULL(MG2.ONDO_STR_DATE,'') AS ONDO_STR_DATE                                               " & vbNewLine _
        & " ,ISNULL(MG2.ONDO_END_DATE,'') AS ONDO_END_DATE                                               " & vbNewLine _
        & " ,INRDC.SYS_UPD_DATE AS SYS_UPD_DATE                                                          " & vbNewLine _
        & " ,INRDC.SYS_UPD_TIME AS SYS_UPD_TIME                                                          " & vbNewLine _
        & " ,(SELECT COUNT(*) FROM LM_MST..M_GOODS MG1                                     " & vbNewLine _
        & "  WHERE INRDC.NRS_BR_CD = MG1.NRS_BR_CD AND                                     " & vbNewLine _
        & " INRDC.CUST_CD_L = MG1.CUST_CD_L AND                                            " & vbNewLine _
        & " INRDC.CUST_CD_M = MG1.CUST_CD_M AND                                            " & vbNewLine _
        & " INRDC.GOODS_CD_CUST = MG1.GOODS_CD_CUST AND                                    " & vbNewLine _
        & " MG1.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "  )                                                                             " & vbNewLine _
        & "  AS M_GOODS_COUNT                                                              " & vbNewLine _
        & " ,(SELECT COUNT(*) FROM LM_MST..M_GOODS MG2                                     " & vbNewLine _
        & "  WHERE INRDC.NRS_BR_CD = MG2.NRS_BR_CD AND                                     " & vbNewLine _
        & " INRDC.CUST_CD_L = MG2.CUST_CD_L AND                                            " & vbNewLine _
        & " INRDC.CUST_CD_M = MG2.CUST_CD_M AND                                            " & vbNewLine _
        & " INRDC.GOODS_CD_CUST = MG2.GOODS_CD_CUST AND                                    " & vbNewLine _
        & " INRDC.PKG_UT = MG2.PKG_UT AND                                                  " & vbNewLine _
        & " INRDC.PKG_NB = MG2.PKG_NB AND                                                  " & vbNewLine _
        & " INRDC.IRIME = MG2.STD_IRIME_NB AND                                             " & vbNewLine _
        & " MG2.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "  )                                                                             " & vbNewLine _
        & "  AS M_GOODS_UT_NB_COUNT                                                        " & vbNewLine _
        & " ,( SELECT COUNT(*) FROM LM_MST..M_ZONE MZ                                      " & vbNewLine _
        & " WHERE INRDC.NRS_BR_CD = MZ.NRS_BR_CD AND                                       " & vbNewLine _
        & " INRDC.WH_CD = MZ.WH_CD AND                                                     " & vbNewLine _
        & " INRDC.TOU_NO = MZ.TOU_NO AND                                                   " & vbNewLine _
        & " INRDC.SITU_NO = MZ.SITU_NO AND                                                 " & vbNewLine _
        & " INRDC.ZONE_CD = MZ.ZONE_CD AND                                                 " & vbNewLine _
        & " MZ.SYS_DEL_FLG = '0'                                                           " & vbNewLine _
        & " ) AS M_ZONE_COUNT                                                              " & vbNewLine _
        & " --,INRDC.IRIME AS IRIME 入目0の可能性があるためコメントアウト,修正 20150324    " & vbNewLine _
        & " ,CASE WHEN INRDC.IRIME = 0 THEN MG1.STD_IRIME_NB ELSE INRDC.IRIME END AS IRIME " & vbNewLine _
        & " ,INRDC.JISSEKI_PKG_UT AS JISSEKI_PKG_UT                                        " & vbNewLine _
        & " ,INRDC.BL_NO AS OUTKA_FROM_ORD_NO_L                                            " & vbNewLine _
        & " ,INRDC.CRT_DATE AS CRT_DATE                                                    " & vbNewLine _
        & " ,ISNULL(MG2.STD_WT_KGS,0) AS STD_WT_KGS                                        " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_BASE As String = _
              "FROM                                                                                                                                                  " & vbNewLine _
            & "   $LM_TRN$..H_INKARESULT_DTL_CLT AS INRDC                                   " & vbNewLine _
            & "   LEFT JOIN $LM_MST$..Z_KBN Z1                                              " & vbNewLine _
            & "   ON INRDC.DATA_KBN = Z1.KBN_CD                                             " & vbNewLine _
            & "   AND Z1.KBN_GROUP_CD = 'F009'                                              " & vbNewLine _
            & "   AND Z1.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
            & "   LEFT JOIN $LM_MST$..M_GOODS MG1                                           " & vbNewLine _
            & "   ON INRDC.NRS_BR_CD = MG1.NRS_BR_CD AND                                    " & vbNewLine _
            & " INRDC.CUST_CD_L = MG1.CUST_CD_L AND                                         " & vbNewLine _
            & " INRDC.CUST_CD_M = MG1.CUST_CD_M AND                                         " & vbNewLine _
            & " INRDC.GOODS_CD_CUST = MG1.GOODS_CD_CUST AND                                 " & vbNewLine _
            & " MG1.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
            & " LEFT JOIN $LM_MST$..M_DEST MD                                               " & vbNewLine _
            & " ON INRDC.OUTKA_MOTO_CD = MD.DEST_CD                                         " & vbNewLine _
            & " AND MD.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
            & " LEFT JOIN                                                                   " & vbNewLine _
            & "    -- 特定可能な商品を抽出したサブクエリ                                    " & vbNewLine _
            & "    -- GOODS_CD_CUSTに対して複数M_GOODSが存在するかチェック                  " & vbNewLine _
            & "    (                                                                        " & vbNewLine _
            & "        SELECT                                                               " & vbNewLine _
            & "            M_GOODS.*                                                        " & vbNewLine _
            & "        FROM                                                                 " & vbNewLine _
            & "            $LM_MST$..M_GOODS                                                  " & vbNewLine _
            & "        INNER JOIN                                                           " & vbNewLine _
            & "            (                                                                " & vbNewLine _
            & "                SELECT                                                       " & vbNewLine _
            & "                    NRS_BR_CD                                                " & vbNewLine _
            & "                    , CUST_CD_L                                              " & vbNewLine _
            & "                    , GOODS_CD_CUST                                          " & vbNewLine _
            & "                FROM                                                         " & vbNewLine _
            & "                    $LM_MST$..M_GOODS                                          " & vbNewLine _
            & "                WHERE                                                        " & vbNewLine _
            & "                        SYS_DEL_FLG = '0'                                    " & vbNewLine _
            & "                    AND NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
            & "                    AND CUST_CD_L = @CUST_CD_L                               " & vbNewLine _
            & "                GROUP BY                                                     " & vbNewLine _
            & "                      NRS_BR_CD                                              " & vbNewLine _
            & "                    , CUST_CD_L                                              " & vbNewLine _
            & "                    , GOODS_CD_CUST                                          " & vbNewLine _
            & "                HAVING                                                       " & vbNewLine _
            & "                    COUNT(GOODS_CD_CUST) = 1                                 " & vbNewLine _
            & "            ) ONLY_GOODS                                                     " & vbNewLine _
            & "        ON                                                                   " & vbNewLine _
            & "                M_GOODS.SYS_DEL_FLG = '0'                                    " & vbNewLine _
            & "            AND M_GOODS.NRS_BR_CD = ONLY_GOODS.NRS_BR_CD                     " & vbNewLine _
            & "            AND M_GOODS.CUST_CD_L = ONLY_GOODS.CUST_CD_L                     " & vbNewLine _
            & "            AND M_GOODS.GOODS_CD_CUST = ONLY_GOODS.GOODS_CD_CUST             " & vbNewLine _
            & "    ) MG2                                                                    " & vbNewLine _
            & " ON                                                                          " & vbNewLine _
            & "         INRDC.NRS_BR_CD = MG2.NRS_BR_CD                                     " & vbNewLine _
            & "     AND INRDC.CUST_CD_L = MG2.CUST_CD_L                                     " & vbNewLine _
            & "     AND INRDC.GOODS_CD_CUST = MG2.GOODS_CD_CUST                             " & vbNewLine _
            & "     AND MG2.SYS_DEL_FLG = '0'                                               " & vbNewLine


    '& " --LEFT JOIN LM_MST..M_GOODS MG2                                                                " & vbNewLine _
    '& " --ON INRDC.NRS_BR_CD = MG2.NRS_BR_CD AND                                                                " & vbNewLine _
    '& " --INRDC.CUST_CD_L = MG2.CUST_CD_L AND                                                                " & vbNewLine _
    '& " --INRDC.CUST_CD_M = MG2.CUST_CD_M AND                                                                " & vbNewLine _
    '& " --INRDC.GOODS_CD_CUST = MG2.GOODS_CD_CUST AND                                                                " & vbNewLine _
    '& " --INRDC.PKG_UT = MG2.PKG_UT AND                                                                " & vbNewLine _
    '& " --INRDC.PKG_NB = MG2.PKG_NB AND                                                                " & vbNewLine _
    '& " --INRDC.JISSEKI_PKG_UT = MG2.GOODS_CD_CUST AND                                                                " & vbNewLine _
    '& " --INRDC.IRIME = MG2.STD_IRIME_NB AND                                                                " & vbNewLine _
    '& " --MG2.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    '& " --LEFT JOIN LM_MST..M_ZONE MZ                                                                " & vbNewLine _
    '& " --ON INRDC.NRS_BR_CD = MZ.NRS_BR_CD AND                                                                " & vbNewLine _
    '& " --INRDC.WH_CD = MZ.WH_CD AND                                                                " & vbNewLine _
    '& " --INRDC.TOU_NO = MZ.TOU_NO AND                                                                " & vbNewLine _
    '& " --INRDC.SITU_NO = MZ.SITU_NO AND                                                                " & vbNewLine _
    '& " --INRDC.ZONE_CD = MZ.ZONE_CD AND                                                                " & vbNewLine _
    '& " --MZ.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    '& " -- WHERE                                                                " & vbNewLine _
    '& " -- INRDC.NRS_BR_CD =  @NRS_BR_CD AND                                                                " & vbNewLine _
    '& " -- INRDC.CUST_CD_L =  @CUST_CD_L AND                                                                " & vbNewLine _
    '& " -- INRDC.CUST_CD_M =  @CUST_CD_M AND                                                                " & vbNewLine _
    '& " -- INRDC.WH_CD =  @WH_CD AND                                                                " & vbNewLine _
    '& " -- INRDC.INKA_DATE =  @INKA_DATE AND                                                                " & vbNewLine _
    '& " ---                                                                " & vbNewLine _
    '& " -- INRDC.INKA_NO_L LIKE '%' +  @BUYER_ORD_NO_L + '%'  AND                 " & vbNewLine _
    '& " -- INRDC.GOODS_NM_1 LIKE '%' +  @GOODS_NM_1LIKE + '%' AND             " & vbNewLine _
    '& " -- INRDC.GOODS_CD_CUST LIKE '%' +  @GOODS_CD_CUST + '%' AND         " & vbNewLine _
    '& " -- INRDC.LOT_NO LIKE '%' +  @LOT_NO + '%' AND                                                                " & vbNewLine _
    '& " -- INRDC.REMARK_L LIKE '%' +  @REMARK_L + '%' AND                                                                " & vbNewLine _
    '& " -- INRDC.REMARK_M LIKE '%' +  @REMARK_M + '%' AND                                                                " & vbNewLine _
    '& " -- INRDC.TOU_NO =  @TOU_NO AND                                                                " & vbNewLine _
    '& " -- INRDC.SITU_NO =  @SITU_NO AND                                                                " & vbNewLine _
    '& " -- INRDC.ZONE_CD =  @ZONE_CD AND                                                                " & vbNewLine _
    '& " -- INRDC.LOCA =  @LOCA AND                                                                " & vbNewLine _
    '& " -- INRDC.OFB_KB =  @OFB_KB AND                                                                " & vbNewLine _
    '& " -- INRDC.OUTKA_MOTO_CD =  @OUTKA_FROM_ORD_NO_L AND                                                                " & vbNewLine _
    '& " --                                                                " & vbNewLine _
    '& "                                                                 " & vbNewLine _
    '& "                                                                                                                               " & vbNewLine _
    '& " --                                                                " & vbNewLine _
    '& " -- INRDC.JISSEKI_SHORI_FLG = '2' AND                                                                " & vbNewLine _
    '& " -- INRDC.SYS_DEL_FLG = '0'                                                                " & vbNewLine _
    '& "                                                                 " & vbNewLine _
    '& "                                                                 " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = _
     "    ORDER BY                                                                                                                                                                                                                                                        " & vbNewLine _
    & "     MAIN.GOODS_CD_CUST                                                                                                                                                                                                                                             " & vbNewLine _
    & "    ,MAIN.STD_IRIME_NB                                                                                                                                                                                                                                              " & vbNewLine _
    & "    ,MAIN.LOT_NO                                                                                                                                                                                                                                                    " & vbNewLine _
    & "    ,MAIN.TOU_NO                                                                                                                                                                                                                                                    " & vbNewLine _
    & "    ,MAIN.SITU_NO                                                                                                                                                                                                                                                   " & vbNewLine _
    & "    ,MAIN.ZONE_CD                                                                                                                                                                                                                                                   " & vbNewLine _
    & "    ,MAIN.LOCA                                                                                                                                                                                                                                                      " & vbNewLine

#End Region

#Region "GROUP BY"

    ''' <summary>
    ''' GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = _
           " ) MAIN                                                                " & vbNewLine _
                 & "    GROUP BY                                                                                                                                                                                                                                                        " & vbNewLine _
                 & " MAIN.STATE                                     " & vbNewLine _
                 & " ,MAIN.BUYER_ORD_NO_L                                     " & vbNewLine _
                 & " ,MAIN.GOODS_CD_CUST                                     " & vbNewLine _
                 & " ,MAIN.GOODS_NM                                     " & vbNewLine _
                 & " ,MAIN.STD_IRIME_NB                                     " & vbNewLine _
                 & " ,MAIN.STD_IRIME_UT                                     " & vbNewLine _
                 & " ,MAIN.JISSEKI_INKA_NB                                     " & vbNewLine _
                 & " ,MAIN.NB_UT                                     " & vbNewLine _
                 & " ,MAIN.JISSEKI_INKA_QT                                     " & vbNewLine _
                 & " ,MAIN.PKG_UT                                     " & vbNewLine _
                 & " ,MAIN.PKG_NB                                     " & vbNewLine _
                 & " ,MAIN.LOT_NO                                     " & vbNewLine _
                 & " ,MAIN.OUTKA_MOTO_NM                                     " & vbNewLine _
                 & " ,MAIN.REMARK_L                                     " & vbNewLine _
                 & " ,MAIN.REMARK_M                                     " & vbNewLine _
                 & " ,MAIN.TOU_NO                                     " & vbNewLine _
                 & " ,MAIN.SITU_NO                                     " & vbNewLine _
                 & " ,MAIN.ZONE_CD                                     " & vbNewLine _
                 & " ,MAIN.LOCA                                     " & vbNewLine _
                 & " ,MAIN.OFB_KB                                     " & vbNewLine _
                 & " ,MAIN.GOODS_CD_NRS                                    " & vbNewLine _
                 & " ,MAIN.ONDO_KB                                     " & vbNewLine _
                 & " ,MAIN.ONDO_STR_DATE                                     " & vbNewLine _
                 & " ,MAIN.ONDO_END_DATE                                     " & vbNewLine _
                 & " ,MAIN.SYS_UPD_DATE                                     " & vbNewLine _
                 & " ,MAIN.SYS_UPD_TIME                                     " & vbNewLine _
                 & " ,MAIN.M_GOODS_COUNT                                     " & vbNewLine _
                 & " ,MAIN.M_GOODS_UT_NB_COUNT                                     " & vbNewLine _
                 & " ,MAIN.M_ZONE_COUNT                                     " & vbNewLine _
                 & " ,MAIN.IRIME                                  " & vbNewLine _
                 & " ,MAIN.JISSEKI_PKG_UT                " & vbNewLine _
                 & " ,MAIN.OUTKA_FROM_ORD_NO_L      " & vbNewLine _
                 & " ,MAIN.CRT_DATE                            " & vbNewLine _
                 & " ,MAIN.STD_WT_KGS                            " & vbNewLine

#End Region

#Region "ｷｬﾝｾﾙｴﾗｰﾁｪｯｸ SQL"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HENKOUCHK As String = "" _
            & "SELECT                                                               " & vbNewLine _
            & "	COUNT(BIL.NRS_BR_CD) AS SELECT_CNT                                      " & vbNewLine _
            & "FROM                                                                 " & vbNewLine _
            & "	$LM_TRN$..B_INKA_L BIL                                              " & vbNewLine _
            & "	LEFT JOIN $LM_TRN$..B_INKA_M BIM                                    " & vbNewLine _
            & "	  ON BIL.NRS_BR_CD = BIM.NRS_BR_CD                                  " & vbNewLine _
            & "	  AND BIL.INKA_NO_L = BIM.INKA_NO_M                                 " & vbNewLine _
            & "	  AND BIM.SYS_DEL_FLG = '0'                                         " & vbNewLine _
            & "WHERE                                                                " & vbNewLine _
            & "		BIL.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
            & "	  AND BIL.INKA_NO_L = @INKA_NO_L                                      " & vbNewLine _
            & "	  AND BIM.INKA_NO_M = @INKA_NO_M                                      " & vbNewLine _
            & "　 AND SUBSTRING(BIL.BUYER_ORD_NO_L,1,14) = SUBSTRING(@BUYER_ORD_NO_L,1,14  )                                " & vbNewLine _
            & "	  AND BIL.SYS_DEL_FLG = '0'                                         " & vbNewLine

#End Region

#Region "区分() SQL"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_KBNF009 As String = "" _
            & "SELECT                          " & vbNewLine _
            & "	KBN_NM1 AS STATE               " & vbNewLine _
            & "FROM                            " & vbNewLine _
            & "	LM_MST..Z_KBN                  " & vbNewLine _
            & "WHERE                           " & vbNewLine _
            & "	    KBN_GROUP_CD = 'F009'      " & vbNewLine _
            & "	AND KBN_CD       = '2'         " & vbNewLine

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
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql_where As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 入荷検品選択検索(カウント)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB050IN")


        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB050DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMB050DAC.SQL_FROM_BASE)        'FROM句作成
        Call Me.SetConditionMasterSQL(inTbl)
        Me._StrSql.Append(Me._StrSql_where)               'Baseへの条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 入荷検品選択検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷検品選択検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB050IN")


        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB050DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMB050DAC.SQL_FROM_BASE)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL(inTbl)
        Me._StrSql.Append(Me._StrSql_where)
        Me._StrSql.Append(LMB050DAC.SQL_GROUP_BY)
        Me._StrSql.Append(LMB050DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB050DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("STATE", "STATE")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("JISSEKI_INKA_NB", "JISSEKI_INKA_NB")
        map.Add("NB_UT", "NB_UT")
        map.Add("JISSEKI_INKA_QT", "JISSEKI_INKA_QT")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("OUTKA_MOTO_NM", "OUTKA_MOTO_NM")
        map.Add("REMARK_L", "REMARK_L")
        map.Add("REMARK_M", "REMARK_M")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("OFB_KB", "OFB_KB")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("M_GOODS_COUNT", "M_GOODS_COUNT")
        map.Add("M_GOODS_UT_NB_COUNT", "M_GOODS_UT_NB_COUNT")
        map.Add("M_ZONE_COUNT", "M_ZONE_COUNT")

        map.Add("IRIME", "IRIME")
        map.Add("JISSEKI_PKG_UT", "JISSEKI_PKG_UT")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("STD_WT_KGS", "STD_WT_KGS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB050OUT")

        Return ds

    End Function


    ''' <summary>
    ''' 入荷検品選択検索(カウント)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHenkouChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB050IN")


        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB050DAC.SQL_SELECT_HENKOUCHK)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        SetHenkouChkDataSelectParameter(ds)
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB050DAC", "SelectHenkouChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 入荷検品選択検索(カウント)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnF009(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB050DAC.SQL_SELECT_KBNF009)     'SQL構築(区分取得用Select句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMB050DAC", "SelectKbnF009", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)


        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("STATE", "STATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB050OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal inTblNotExists As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            'NRS_BR_CD
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql_where.Append("WHERE")
            Me._StrSql_where.Append(vbNewLine)
            Me._StrSql_where.Append("INRDC.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            'CUST_CD_L
            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql_where.Append("AND RTRIM(INRDC.CUST_CD_L) = @CUST_CD_L")
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            'CUST_CD_M
            whereStr = .Item("CUST_CD_M").ToString()
            Me._StrSql_where.Append("AND RTRIM(INRDC.CUST_CD_M) = @CUST_CD_M")
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            'WH_CD
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.WH_CD) = @WH_CD")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            'INKA_DATE
            whereStr = .Item("INKA_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.INKA_DATE) = @INKA_DATE")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", whereStr, DBDataType.CHAR))
            End If

            'BUYER_ORD_NO_L
            'INではLのみくる想定 2015/1/13
            whereStr = .Item("BUYER_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.INKA_NO_L) LIKE @BUYER_ORD_NO_L ")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'GOODS_CD_CUST
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.GOODS_NM_1) LIKE @GOODS_NM_1")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'GOODS_CD_CUST
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.GOODS_CD_CUST) LIKE @GOODS_CD_CUST")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'LOT_NO
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.LOT_NO) LIKE @LOT_NO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

            'REMARK_L
            whereStr = .Item("REMARK_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND INRDC.REMARK_L LIKE @REMARK_L")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'REMARK_M
            whereStr = .Item("REMARK_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND INRDC.REMARK_M LIKE @REMARK_M")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_M", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'TOU_NO
            whereStr = .Item("TOU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND INRDC.TOU_NO = @TOU_NO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", whereStr, DBDataType.NVARCHAR))
            End If

            'SITU_NO
            whereStr = .Item("SITU_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.SITU_NO) = @SITU_NO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", whereStr, DBDataType.CHAR))
            End If

            'ZONE
            whereStr = .Item("ZONE_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.ZONE_CD) = @ZONE_CD")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", whereStr, DBDataType.CHAR))
            End If

            'ZONE_CD
            whereStr = .Item("LOCA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND INRDC.LOCA = @LOCA")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", whereStr, DBDataType.NVARCHAR))
            End If

            'OFB_KB
            whereStr = .Item("OFB_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(INRDC.OFB_KB) = @OFB_KB")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", whereStr, DBDataType.CHAR))
            End If

            ''OUTKA_MOTO_CD
            whereStr = .Item("OUTKA_FROM_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND INRDC.OUTKA_MOTO_CD LIKE @OUTKA_MOTO_CD")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

        '共通条件
        Me._StrSql_where.Append("AND INRDC.JISSEKI_SHORI_FLG in( '1', '2' ) AND INRDC.SYS_DEL_FLG = '0' ")
        Me._StrSql_where.Append(vbNewLine)


    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetHenkouChkDataSelectParameter(ByVal ds As DataSet)

        With ds

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Tables("LMB050IN").Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Tables("LMB050IN").Rows(0).Item("BUYER_ORD_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Tables("LMB050IN").Rows(0).Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Tables("LMB050IN").Rows(0).Item("INKA_NO_M").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "共通"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <param name="parameters">パラメータリスト</param>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd(ByVal parameters As List(Of SqlParameter))

        'パラメータ設定
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        parameters.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

#End Region

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

#End Region


End Class
