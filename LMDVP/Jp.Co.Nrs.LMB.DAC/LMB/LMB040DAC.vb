' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB040DAC : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB040DAC
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
          " SELECT                                                                                                                       " & vbNewLine _
        & "     (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'M018' AND KBN_CD = BASE.MST_EXISTS_KBN) AS MST_EXISTS_MARK    " & vbNewLine _
        & "     , BASE.*                                                                                                                 " & vbNewLine

#End Region

#Region "FROM句"

    Private Const SQL_FROM_BASE As String = _
              "FROM                                                                                                                                                  " & vbNewLine _
            & "    (                                                                                                                                                 " & vbNewLine _
            & "        SELECT                                                                                                                                        " & vbNewLine _
            & "            CASE                                                                                                                                      " & vbNewLine _
            & "                -- 商品特定済みの場合はCUST_Mレベルで扱い可能商品かを確認する                                                                             " & vbNewLine _
            & "                WHEN ISNULL(MG1.GOODS_CD_NRS,ISNULL(MG2.GOODS_CD_NRS,'')) <> '' THEN                                                                  " & vbNewLine _
            & "                    CASE                                                                                                                              " & vbNewLine _
            & "                        WHEN ISNULL(MG1.CUST_CD_M,ISNULL(MG2.CUST_CD_M,'')) = @CUST_CD_M THEN '00'                                                    " & vbNewLine _
            & "                        ELSE '04'                                                                                                                     " & vbNewLine _
            & "                    END                                                                                                                               " & vbNewLine _
            & "                ELSE                                                                                                                                  " & vbNewLine _
            & "                    -- 商品未特定(GOODS_CD_NRS未特定)の場合は複数件か0件のため、GOODS_CD_CUSTでマスタ件数の確認を行う。                                    " & vbNewLine _
            & "                    CASE (                                                                                                                            " & vbNewLine _
            & "                        SELECT                                                                                                                        " & vbNewLine _
            & "                            COUNT(*)                                                                                                                  " & vbNewLine _
            & "                        FROM                                                                                                                          " & vbNewLine _
            & "                            $LM_MST$..M_GOODS MG                                                                                                      " & vbNewLine _
            & "                        WHERE                                                                                                                         " & vbNewLine _
            & "                                MG.NRS_BR_CD = wk.NRS_BR_CD                                                                                           " & vbNewLine _
            & "                            AND MG.CUST_CD_L = wk.CUST_CD_L                                                                                           " & vbNewLine _
            & "                            AND MG.GOODS_CD_CUST = wk.GOODS_CD_CUST                                                                                   " & vbNewLine _
            & "                            AND MG.SYS_DEL_FLG = '0'                                                                                                  " & vbNewLine _
            & "                        )                                                                                                                             " & vbNewLine _
            & "                        WHEN 0 THEN '01'                                                                                                              " & vbNewLine _
            & "                        ELSE '02'                                                                                                                     " & vbNewLine _
            & "                    END                                                                                                                               " & vbNewLine _
            & "            END AS MST_EXISTS_KBN                                                                                                                     " & vbNewLine _
            & "            , SUBSTRING(wk.INPUT_DATE,1,4) +'/'+ SUBSTRING(wk.INPUT_DATE,5,2) +'/'+ SUBSTRING(wk.INPUT_DATE,7,2) AS KENPIN_DATE                       " & vbNewLine _
            & "            , wk.NRS_BR_CD                                                                                                                            " & vbNewLine _
            & "            , wk.WH_CD                                                                                                                                " & vbNewLine _
            & "            , wk.CUST_CD_L                                                                                                                            " & vbNewLine _
            & "            , wk.INPUT_DATE                                                                                                                           " & vbNewLine _
            & "            , wk.SEQ                                                                                                                                  " & vbNewLine _
            & "            , wk.GOODS_CD_NRS AS KENPIN_WK_GOODS_CD_NRS                                                                                               " & vbNewLine _
            & "            , ISNULL(MG1.GOODS_CD_NRS,ISNULL(MG2.GOODS_CD_NRS,'')) AS GOODS_CD_NRS                                                                    " & vbNewLine _
            & "            , wk.GOODS_KANRI_NO                                                                                                                       " & vbNewLine _
            & "            , wk.GOODS_CD_CUST                                                                                                                        " & vbNewLine _
            & "            , wk.LOT_NO                                                                                                                               " & vbNewLine _
            & " -- JT物流入荷検品対応 20160726                                                                                                                       " & vbNewLine _
            & "            , wk.GOODS_CRT_DATE                                                                                                                       " & vbNewLine _
            & "            , wk.LT_DATE                                                                                                                              " & vbNewLine _
            & " --         , ISNULL(MG1.GOODS_NM_1,ISNULL(MG2.GOODS_NM_1,'')) AS GOODS_NM                                                                          " & vbNewLine _
            & "            , CASE WHEN ISNULL(wk.GOODS_CD_NRS, '') = ''                                                                                              " & vbNewLine _
            & "                    AND ISNULL(wk.GOODS_NM_CUST, '') <> ''                                                                                            " & vbNewLine _
            & "                   THEN wk.GOODS_NM_CUST                                                                                                              " & vbNewLine _
            & "                   ELSE ISNULL(MG1.GOODS_NM_1,ISNULL(MG2.GOODS_NM_1,''))                                                                              " & vbNewLine _
            & "              END  AS GOODS_NM                                                                                                                        " & vbNewLine _
            & "            , wk.SERIAL_NO                                                                                                                            " & vbNewLine _
            & "            , wk.TOU_NO                                                                                                                               " & vbNewLine _
            & "            , wk.SITU_NO                                                                                                                              " & vbNewLine _
            & "            , wk.ZONE_CD                                                                                                                              " & vbNewLine _
            & "            , wk.LOCA                                                                                                                                 " & vbNewLine _
            & "            , wk.INKA_TTL_NB AS KENPIN_KAKUTEI_TTL_NB                                                                                                 " & vbNewLine _
            & "            , wk.INKA_TORI_FLG                                                                                                                        " & vbNewLine _
            & "            , (SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'K013' AND KBN_CD = '0' + wk.INKA_TORI_FLG) AS TORIKOMI_FLG_NM                      " & vbNewLine _
            & "            , wk.SYS_ENT_USER                                                                                                                         " & vbNewLine _
            & "            , wk.SYS_UPD_DATE                                                                                                                         " & vbNewLine _
            & "            , wk.SYS_UPD_TIME                                                                                                                         " & vbNewLine _
            & "            --, ISNULL(MG1.STD_IRIME_NB,ISNULL(MG2.STD_IRIME_NB,0)) AS STD_IRIME_NB                                                                   " & vbNewLine _
            & "            , CASE                                                                                                                                    " & vbNewLine _
            & "                 WHEN wk.IRIME = 0 THEN ISNULL(MG1.STD_IRIME_NB,ISNULL(MG2.STD_IRIME_NB,0))                                                           " & vbNewLine _
            & "          		ELSE wk.IRIME END AS STD_IRIME_NB                                                                                                    " & vbNewLine _
            & "            , ISNULL(MG1.STD_IRIME_UT,ISNULL(MG2.STD_IRIME_UT,'')) AS STD_IRIME_UT                                                                    " & vbNewLine _
            & "            , ISNULL(MG1.PKG_NB,ISNULL(MG2.PKG_NB,0)) AS PKG_NB                                                                                       " & vbNewLine _
            & "            , ISNULL(MG1.PKG_UT,ISNULL(MG2.PKG_UT,'')) AS PKG_UT                                                                                      " & vbNewLine _
            & "            , ISNULL(MG1.NB_UT,ISNULL(MG2.NB_UT,'')) AS NB_UT                                                                                         " & vbNewLine _
            & "            , ISNULL(MG1.ONDO_KB,ISNULL(MG2.ONDO_KB,'')) AS ONDO_KB                                                                                   " & vbNewLine _
            & "            , ISNULL(MG1.ONDO_STR_DATE,ISNULL(MG2.ONDO_STR_DATE,'')) AS ONDO_STR_DATE                                                                 " & vbNewLine _
            & "            , ISNULL(MG1.ONDO_END_DATE,ISNULL(MG2.ONDO_END_DATE,'')) AS ONDO_END_DATE                                                                 " & vbNewLine _
            & "            , ISNULL(MG1.STD_WT_KGS,ISNULL(MG2.STD_WT_KGS,0)) AS STD_WT_KGS                                                                           " & vbNewLine _
            & "            , CASE                                                                                                                                    " & vbNewLine _
            & "                WHEN ISNULL(MG1.PKG_NB,ISNULL(MG2.PKG_NB,0)) <> 0 THEN ROUND(wk.INKA_TTL_NB / ISNULL(MG1.PKG_NB,ISNULL(MG2.PKG_NB,0)),0,1)            " & vbNewLine _
            & "                ELSE 0                                                                                                                                " & vbNewLine _
            & "              END AS KONSU                                                                                                                            " & vbNewLine _
            & "            , CASE                                                                                                                                    " & vbNewLine _
            & "                WHEN ISNULL(MG1.PKG_NB,ISNULL(MG2.PKG_NB,0)) <> 0 THEN wk.INKA_TTL_NB % ISNULL(MG1.PKG_NB,ISNULL(MG2.PKG_NB,0))                       " & vbNewLine _
            & "                ELSE 0                                                                                                                                " & vbNewLine _
            & "              END AS HASU                                                                                                                             " & vbNewLine _
            & "            , CASE                                                                                                                                    " & vbNewLine _
            & "                WHEN ISNULL(MG1.PKG_NB,ISNULL(MG2.PKG_NB,0)) <> 0 THEN ROUND(wk.INKA_TTL_NB * ISNULL(MG1.STD_WT_KGS,ISNULL(MG2.STD_WT_KGS,0)),3,0)    " & vbNewLine _
            & "                ELSE 0                                                                                                                                " & vbNewLine _
            & "              END AS BETU_WT                                                                                                                          " & vbNewLine _
            & "            , ISNULL(MG1.INKA_KAKO_SAGYO_KB_1,ISNULL(MG2.INKA_KAKO_SAGYO_KB_1,'')) AS INKA_KAKO_SAGYO_KB_1                                            " & vbNewLine _
            & "            , ISNULL(MG1.INKA_KAKO_SAGYO_KB_2,ISNULL(MG2.INKA_KAKO_SAGYO_KB_2,'')) AS INKA_KAKO_SAGYO_KB_2                                            " & vbNewLine _
            & "            , ISNULL(MG1.INKA_KAKO_SAGYO_KB_3,ISNULL(MG2.INKA_KAKO_SAGYO_KB_3,'')) AS INKA_KAKO_SAGYO_KB_3                                            " & vbNewLine _
            & "            , ISNULL(MG1.INKA_KAKO_SAGYO_KB_4,ISNULL(MG2.INKA_KAKO_SAGYO_KB_4,'')) AS INKA_KAKO_SAGYO_KB_4                                            " & vbNewLine _
            & "            , ISNULL(MG1.INKA_KAKO_SAGYO_KB_5,ISNULL(MG2.INKA_KAKO_SAGYO_KB_5,'')) AS INKA_KAKO_SAGYO_KB_5                                            " & vbNewLine _
            & "            , ISNULL(MG1.CUST_CD_M,ISNULL(MG2.CUST_CD_M,''))           AS CUST_CD_M                                                                   " & vbNewLine _
            & "            , ISNULL(MG1.CUST_CD_S,ISNULL(MG2.CUST_CD_S,''))           AS CUST_CD_S                                                                   " & vbNewLine _
            & "            , ISNULL(MG1.CUST_CD_SS,ISNULL(MG2.CUST_CD_SS,''))         AS CUST_CD_SS                                                                  " & vbNewLine _
            & "            , ISNULL(MG1.TARE_YN,ISNULL(MG2.TARE_YN,''))               AS TARE_YN                                                                     " & vbNewLine _
            & "            , ISNULL(MG1.LOT_CTL_KB,ISNULL(MG2.LOT_CTL_KB,''))         AS LOT_CTL_KB                                                                  " & vbNewLine _
            & "            , ISNULL(MG1.LT_DATE_CTL_KB,ISNULL(MG2.LT_DATE_CTL_KB,'')) AS LT_DATE_CTL_KB                                                              " & vbNewLine _
            & "            , ISNULL(MG1.CRT_DATE_CTL_KB,ISNULL(MG2.CRT_DATE_CTL_KB,'')) AS CRT_DATE_CTL_KB                                                           " & vbNewLine _
            & "            , ISNULL(SU.USER_NM,'') AS USER_NM                                                                                                        " & vbNewLine _
            & "            , ISNULL(CUST_HANDY.DEFAULT_CHK_TANI, '') AS CHK_TANI                                                                                     " & vbNewLine _
            & "            , CASE WHEN IHONT.NEXT_TEST_DATE IS NULL     --ADD 2018/11/06 依頼番号 : 002669                                                           " & vbNewLine _
            & "                   THEN   ''                                                                                                                          " & vbNewLine _
            & "                   ELSE  SUBSTRING(IHONT.NEXT_TEST_DATE,1,4) +'/'+ SUBSTRING(IHONT.NEXT_TEST_DATE,5,2) +'/'+ SUBSTRING(IHONT.NEXT_TEST_DATE,7,2)      " & vbNewLine _
            & "              END        AS NEXT_TEST_DATE                                                                                                            " & vbNewLine _
            & "        FROM                                                                                                                                          " & vbNewLine _
            & "            $LM_TRN$..B_INKA_KENPIN_WK wk                                                                                                             " & vbNewLine _
            & "        LEFT JOIN                                                                                                                                     " & vbNewLine _
            & "            $LM_MST$..M_GOODS MG1                                                                                                                     " & vbNewLine _
            & "        ON                                                                                                                                            " & vbNewLine _
            & "                wk.NRS_BR_CD = MG1.NRS_BR_CD                                                                                                          " & vbNewLine _
            & "            AND wk.GOODS_CD_NRS = MG1.GOODS_CD_NRS                                                                                                    " & vbNewLine _
            & "            AND MG1.SYS_DEL_FLG = '0'                                                                                                                 " & vbNewLine _
            & "        LEFT JOIN                                                                                                                                     " & vbNewLine _
            & "            -- 特定可能な商品を抽出したサブクエリ                                                                                                     " & vbNewLine _
            & "            (                                                                                                                                         " & vbNewLine _
            & "                SELECT                                                                                                                                " & vbNewLine _
            & "                    M_GOODS.*                                                                                                                         " & vbNewLine _
            & "                FROM                                                                                                                                  " & vbNewLine _
            & "                    $LM_MST$..M_GOODS                                                                                                                 " & vbNewLine _
            & "                INNER JOIN                                                                                                                            " & vbNewLine _
            & "                    (                                                                                                                                 " & vbNewLine _
            & "                        SELECT                                                                                                                        " & vbNewLine _
            & "                            NRS_BR_CD                                                                                                                 " & vbNewLine _
            & "                            , CUST_CD_L                                                                                                               " & vbNewLine _
            & "                            , GOODS_CD_CUST                                                                                                           " & vbNewLine _
            & "                        FROM                                                                                                                          " & vbNewLine _
            & "                            $LM_MST$..M_GOODS                                                                                                         " & vbNewLine _
            & "                        WHERE                                                                                                                         " & vbNewLine _
            & "                                SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
            & "                            AND NRS_BR_CD = @NRS_BR_CD                                                                                                " & vbNewLine _
            & "                            AND CUST_CD_L = @CUST_CD_L                                                                                                " & vbNewLine _
            & "                        GROUP BY                                                                                                                      " & vbNewLine _
            & "                              NRS_BR_CD                                                                                                               " & vbNewLine _
            & "                            , CUST_CD_L                                                                                                               " & vbNewLine _
            & "                            , GOODS_CD_CUST                                                                                                           " & vbNewLine _
            & "                        HAVING                                                                                                                        " & vbNewLine _
            & "                            COUNT(GOODS_CD_CUST) = 1                                                                                                  " & vbNewLine _
            & "                    ) ONLY_GOODS                                                                                                                      " & vbNewLine _
            & "                ON                                                                                                                                    " & vbNewLine _
            & "                        M_GOODS.SYS_DEL_FLG = '0'                                                                                                     " & vbNewLine _
            & "                    AND M_GOODS.NRS_BR_CD = ONLY_GOODS.NRS_BR_CD                                                                                      " & vbNewLine _
            & "                    AND M_GOODS.CUST_CD_L = ONLY_GOODS.CUST_CD_L                                                                                      " & vbNewLine _
            & "                    AND M_GOODS.GOODS_CD_CUST = ONLY_GOODS.GOODS_CD_CUST                                                                              " & vbNewLine _
            & "            ) MG2                                                                                                                                     " & vbNewLine _
            & "        ON                                                                                                                                            " & vbNewLine _
            & "                wk.NRS_BR_CD = MG2.NRS_BR_CD                                                                                                          " & vbNewLine _
            & "            AND wk.CUST_CD_L = MG2.CUST_CD_L                                                                                                          " & vbNewLine _
            & "            AND wk.GOODS_CD_CUST = MG2.GOODS_CD_CUST                                                                                                  " & vbNewLine _
            & "            AND MG2.SYS_DEL_FLG = '0'                                                                                                                 " & vbNewLine _
            & "        LEFT JOIN                                                                                                                                     " & vbNewLine _
            & "            $LM_MST$..S_USER SU                                                                                                                       " & vbNewLine _
            & "        ON                                                                                                                                            " & vbNewLine _
            & "                SU.USER_CD = wk.SYS_ENT_USER                                                                                                          " & vbNewLine _
            & "            AND SU.SYS_DEL_FLG = '0'                                                                                                                  " & vbNewLine _
            & "        LEFT JOIN                                                                                                                                     " & vbNewLine _
            & "            $LM_MST$..M_CUST_HANDY AS CUST_HANDY                                                                                                                       " & vbNewLine _
            & "        ON                                                                                                                                            " & vbNewLine _
            & "                CUST_HANDY.NRS_BR_CD   = wk.NRS_BR_CD                                                                                                          " & vbNewLine _
            & "            AND CUST_HANDY.WH_CD       = wk.WH_CD                                                                                                          " & vbNewLine _
            & "            AND CUST_HANDY.CUST_CD_L   = wk.CUST_CD_L                                                                                                          " & vbNewLine _
            & "            AND CUST_HANDY.SYS_DEL_FLG = '0'                                                                                                                  " & vbNewLine _
            & " --ADD 2018/11/06 依頼番号 : 002669                                                                                                                   " & vbNewLine _
            & "        LEFT JOIN                                                                                                                                     " & vbNewLine _
            & "             $LM_TRN$..I_HON_TEIKEN IHONT                                                                                                             " & vbNewLine _
            & "          ON IHONT.SERIAL_NO   = wk.SERIAL_NO                                                                                                         " & vbNewLine _
            & "         AND IHONT.NRS_BR_CD   = wk.NRS_BR_CD                                                                                                         " & vbNewLine _
            & "         AND IHONT.SYS_DEL_FLG = '0'                                                                                                                  " & vbNewLine _
            & "        WHERE                                                                                                                                         " & vbNewLine _
            & "                wk.SYS_DEL_FLG = '0'                                                                                                                  " & vbNewLine _
            & "            AND wk.NRS_BR_CD = @NRS_BR_CD                                                                                                             " & vbNewLine _
            & "            AND wk.CUST_CD_L = @CUST_CD_L                                                                                                             " & vbNewLine _
            & "    ) BASE                                                                                                                                            " & vbNewLine _
            & "                                                                                                                                                      " & vbNewLine

#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String =
              "    ORDER BY                                                                                                                                                                                                                                                        " & vbNewLine _
            & "     BASE.INPUT_DATE                                                                                                                                                                                                                                                " & vbNewLine _
            & "    ,BASE.SEQ       --ADD 2020/12/23 017512対応                                                                                                                                                                                                                     " & vbNewLine _
            & "    ,BASE.GOODS_CD_CUST                                                                                                                                                                                                                                             " & vbNewLine _
            & "    ,BASE.STD_IRIME_NB                                                                                                                                                                                                                                              " & vbNewLine _
            & "    ,BASE.LOT_NO                                                                                                                                                                                                                                                    " & vbNewLine _
            & "    ,BASE.TOU_NO                                                                                                                                                                                                                                                    " & vbNewLine _
            & "    ,BASE.SITU_NO                                                                                                                                                                                                                                                   " & vbNewLine _
            & "    ,BASE.ZONE_CD                                                                                                                                                                                                                                                   " & vbNewLine _
            & "    ,BASE.LOCA                                                                                                                                                                                                                                                      " & vbNewLine

#End Region


#End Region

#Region "UPDATE SQL"

    ''' <summary>
    ''' 入荷検品ワーク更新用(INKA_TORI_FLG更新)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_INKA_TORI_FLG As String = _
              "UPDATE                                      " & vbNewLine _
            & "    $LM_TRN$..B_INKA_KENPIN_WK              " & vbNewLine _
            & "SET                                         " & vbNewLine _
            & "      INKA_TORI_FLG = @INKA_TORI_FLG        " & vbNewLine _
            & "    , SYS_UPD_DATE = @SYS_UPD_DATE          " & vbNewLine _
            & "    , SYS_UPD_TIME = @SYS_UPD_TIME          " & vbNewLine _
            & "WHERE                                       " & vbNewLine _
            & "        NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
            & "    AND WH_CD = @WH_CD                      " & vbNewLine _
            & "    AND CUST_CD_L = @CUST_CD_L              " & vbNewLine _
            & "    AND INPUT_DATE = @INPUT_DATE            " & vbNewLine _
            & "    AND SEQ = @SEQ                          " & vbNewLine _
            & "    AND SYS_UPD_DATE = @SYS_UPD_DATE_CUR    " & vbNewLine _
            & "    AND SYS_UPD_TIME = @SYS_UPD_TIME_CUR    " & vbNewLine

#End Region

#Region "削除処理 SQL"

#Region "DELETE SQL"

    ''' <summary>
    ''' 入荷検品ワーク削除用(INKA_TORI_FLG更新)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_B_INKA_KENPIN_WK As String = _
              "DELETE FROM                                                  " & vbNewLine _
            & "     $LM_TRN$..B_INKA_KENPIN_WK                              " & vbNewLine _
            & "WHERE                                                        " & vbNewLine _
            & "        B_INKA_KENPIN_WK.NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
            & "    AND B_INKA_KENPIN_WK.WH_CD        = @WH_CD               " & vbNewLine _
            & "    AND B_INKA_KENPIN_WK.CUST_CD_L    = @CUST_CD_L           " & vbNewLine _
            & "    AND B_INKA_KENPIN_WK.INPUT_DATE   = @INPUT_DATE          " & vbNewLine _
            & "    AND B_INKA_KENPIN_WK.SEQ          = @SEQ                 " & vbNewLine _
            & "    AND B_INKA_KENPIN_WK.SYS_UPD_DATE = @SYS_UPD_DATE_CUR    " & vbNewLine _
            & "    AND B_INKA_KENPIN_WK.SYS_UPD_TIME = @SYS_UPD_TIME_CUR    " & vbNewLine


#End Region

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

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 入荷検品選択対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷検品選択対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB040IN")
        Dim inTblNotExists As DataTable = ds.Tables("LMB040INPUTED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB040DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMB040DAC.SQL_FROM_BASE)        'FROM句作成

        Call Me.SetConditionMasterSQL(inTblNotExists)     '条件設定部分構築
        Me._StrSql.Append(Me._StrSql_where)               'Baseへの条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB040DAC", "SelectData", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMB040IN")
        Dim inTblNotExists As DataTable = ds.Tables("LMB040INPUTED")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSql_where = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB040DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMB040DAC.SQL_FROM_BASE)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL(inTblNotExists)                   '条件設定
        Me._StrSql.Append(Me._StrSql_where)
        Me._StrSql.Append(LMB040DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB040DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("MST_EXISTS_MARK", "MST_EXISTS_MARK")
        map.Add("MST_EXISTS_KBN", "MST_EXISTS_KBN")
        map.Add("KENPIN_DATE", "KENPIN_DATE")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("INPUT_DATE", "INPUT_DATE")
        map.Add("SEQ", "SEQ")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("LOT_NO", "LOT_NO")
        '2014.02.17 WIT対応START
        map.Add("SERIAL_NO", "SERIAL_NO")
        '2014.02.17 WIT対応END
#If True Then ' JT物流入荷検品対応 20160726 added inoue
        map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
        map.Add("CHK_TANI", "CHK_TANI")
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
        map.Add("LT_DATE", "LT_DATE")
#End If
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("KENPIN_KAKUTEI_TTL_NB", "KENPIN_KAKUTEI_TTL_NB")
        map.Add("INKA_TORI_FLG", "INKA_TORI_FLG")
        map.Add("TORIKOMI_FLG_NM", "TORIKOMI_FLG_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("NB_UT", "NB_UT")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("KONSU", "KONSU")
        map.Add("HASU", "HASU")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("INKA_KAKO_SAGYO_KB_1", "INKA_KAKO_SAGYO_KB_1")
        map.Add("INKA_KAKO_SAGYO_KB_2", "INKA_KAKO_SAGYO_KB_2")
        map.Add("INKA_KAKO_SAGYO_KB_3", "INKA_KAKO_SAGYO_KB_3")
        map.Add("INKA_KAKO_SAGYO_KB_4", "INKA_KAKO_SAGYO_KB_4")
        map.Add("INKA_KAKO_SAGYO_KB_5", "INKA_KAKO_SAGYO_KB_5")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")
        map.Add("LT_DATE_CTL_KB", "LT_DATE_CTL_KB")
        map.Add("CRT_DATE_CTL_KB", "CRT_DATE_CTL_KB")
        map.Add("USER_NM", "USER_NM")
        map.Add("NEXT_TEST_DATE", "NEXT_TEST_DATE")         '--ADD 2018/11/06 依頼番号 : 002669  

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB040OUT")

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
            Me._StrSql_where.Append("BASE.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            'CUST_CD_L
            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql_where.Append("AND BASE.CUST_CD_L = @CUST_CD_L")
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            'CUST_CD_M(検索条件としては設定しない)
            whereStr = .Item("CUST_CD_M").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

            'SAGYO_USER_CD
            whereStr = .Item("SAGYO_USER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.SYS_ENT_USER = @SAGYO_USER_CD")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_USER_CD", whereStr, DBDataType.NVARCHAR))
            End If

            'SYS_ENT_DATE(FROM)
            whereStr = .Item("SYS_ENT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.INPUT_DATE >= @SYS_ENT_DATE")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", whereStr, DBDataType.CHAR))
            End If

            '追加開始 2015.05.15 要望番号:2292
            'SYS_ENT_DATE_TO
            whereStr = .Item("SYS_ENT_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.INPUT_DATE <= @SYS_ENT_DATE_TO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE_TO", whereStr, DBDataType.CHAR))
            End If
            '追加終了 2015.05.15 要望番号:2292

            'GOODS_NM_1
            whereStr = .Item("GOODS_NM_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND (BASE.GOODS_NM LIKE @GOODS_NM_1 OR BASE.GOODS_NM IS NULL)")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_1", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'GOODS_CD_CUST
            whereStr = .Item("GOODS_CD_CUST").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.GOODS_CD_CUST LIKE @GOODS_CD_CUST")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'LOT_NO
            whereStr = .Item("LOT_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.LOT_NO LIKE @LOT_NO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '2014.02.17 追加START
            'SERIAL_NO
            whereStr = .Item("SERIAL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.SERIAL_NO LIKE @SERIAL_NO")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '2014.02.17 追加END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
            ' GOODS_CRT_DATE
            whereStr = .Item("GOODS_CRT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.GOODS_CRT_DATE LIKE @GOODS_CRT_DATE")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If
#End If

#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 

            ' LT_DATE
            whereStr = .Item("LT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.LT_DATE LIKE @LT_DATE")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", String.Concat("%", whereStr, "%"), DBDataType.CHAR))
            End If

#End If

            'OKIBA
            whereStr = .Item("OKIBA").ToString().Replace("-", "").Replace(" ", "")
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND RTRIM(BASE.TOU_NO) + RTRIM(BASE.SITU_NO) + RTRIM(BASE.ZONE_CD) + RTRIM(BASE.LOCA) LIKE @OKIBA")
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OKIBA", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'IS_ONRY_MISHORI
            whereStr = .Item("IS_ONRY_MISHORI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql_where.Append("AND BASE.INKA_TORI_FLG = '0'")
                Me._StrSql_where.Append(vbNewLine)
            End If

        End With

        Dim notExistsCnt As Integer = 0
        For Each notRow As DataRow In inTblNotExists.Rows
            If notExistsCnt = 0 Then
                Me._StrSql_where.Append("AND NOT ( ")
                Me._StrSql_where.Append(vbNewLine)
            Else
                Me._StrSql_where.Append("OR ")
                Me._StrSql_where.Append(vbNewLine)
            End If

            Me._StrSql_where.Append("(")
            Me._StrSql_where.Append(vbNewLine)

            whereStr = notRow.Item("NRS_BR_CD").ToString()
            Me._StrSql_where.Append(String.Concat("BASE.NRS_BR_CD = @NRS_BR_CD_", notExistsCnt.ToString()))
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@NRS_BR_CD_", notExistsCnt.ToString()), whereStr, DBDataType.CHAR))

            whereStr = notRow.Item("GOODS_CD_CUST").ToString()
            Me._StrSql_where.Append(String.Concat("AND BASE.GOODS_CD_CUST = @GOODS_CD_CUST_", notExistsCnt.ToString()))
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@GOODS_CD_CUST_", notExistsCnt.ToString()), whereStr, DBDataType.NVARCHAR))

            whereStr = notRow.Item("CUST_CD_L").ToString()
            Me._StrSql_where.Append(String.Concat("AND BASE.CUST_CD_L = @CUST_CD_L_", notExistsCnt.ToString()))
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@CUST_CD_L_", notExistsCnt.ToString()), whereStr, DBDataType.CHAR))

            '2014.02.18 WIT対応修正START
            If String.IsNullOrEmpty(notRow.Item("LOT_NO").ToString()) = False Then
                whereStr = notRow.Item("LOT_NO").ToString()
                Me._StrSql_where.Append(String.Concat("AND BASE.LOT_NO = @LOT_NO_", notExistsCnt.ToString()))
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@LOT_NO_", notExistsCnt.ToString()), whereStr, DBDataType.NVARCHAR))
            End If


            '2014.02.18 WIT対応追加START
            If String.IsNullOrEmpty(notRow.Item("SERIAL_NO").ToString()) = False Then
                whereStr = notRow.Item("SERIAL_NO").ToString()
                Me._StrSql_where.Append(String.Concat("AND BASE.SERIAL_NO = @SERIAL_NO_", notExistsCnt.ToString()))
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@SERIAL_NO_", notExistsCnt.ToString()), whereStr, DBDataType.NVARCHAR))
            End If
            '2014.02.18 WIT対応追加END

#If True Then ' JT物流入荷検品対応 20160726 added inoue
            If String.IsNullOrEmpty(notRow.Item("GOODS_CRT_DATE").ToString()) = False Then
                whereStr = notRow.Item("GOODS_CRT_DATE").ToString()
                Me._StrSql_where.Append(String.Concat("AND BASE.GOODS_CRT_DATE = @GOODS_CRT_DATE_", notExistsCnt.ToString()))
                Me._StrSql_where.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@GOODS_CRT_DATE_", notExistsCnt.ToString()), whereStr, DBDataType.CHAR))
            End If
#End If

            whereStr = notRow.Item("TOU_NO").ToString()
            Me._StrSql_where.Append(String.Concat("AND BASE.TOU_NO = @TOU_NO_", notExistsCnt.ToString()))
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@TOU_NO_", notExistsCnt.ToString()), whereStr, DBDataType.NVARCHAR))

            whereStr = notRow.Item("SITU_NO").ToString()
            Me._StrSql_where.Append(String.Concat("AND BASE.SITU_NO = @SITU_NO_", notExistsCnt.ToString()))
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@SITU_NO_", notExistsCnt.ToString()), whereStr, DBDataType.CHAR))

            whereStr = notRow.Item("ZONE_CD").ToString()
            Me._StrSql_where.Append(String.Concat("AND BASE.ZONE_CD = @ZONE_CD_", notExistsCnt.ToString()))
            Me._StrSql_where.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter(String.Concat("@ZONE_CD_", notExistsCnt.ToString()), whereStr, DBDataType.CHAR))

            Me._StrSql_where.Append(")")
            Me._StrSql_where.Append(vbNewLine)

            If notExistsCnt = inTblNotExists.Rows.Count - 1 Then
                Me._StrSql_where.Append(") ")
                Me._StrSql_where.Append(vbNewLine)
            End If

            notExistsCnt = notExistsCnt + 1
        Next

    End Sub

#End Region

#Region "更新処理"

    ''' ==========================================================================
    ''' <summary>入荷検品ワーク更新用(INKA_TORI_FLG更新)メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function UpdateInkaToriFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB040IN_UPDATE")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB040DAC.SQL_UPDATE_INKA_TORI_FLG, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Dim sqlParamList As List(Of SqlParameter) = Me.CreateUpdateInkaToriFlgParameterList(inRow)
        cmd.Parameters.AddRange(sqlParamList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷検品ワーク更新用(INKA_TORI_FLG更新)パラメータのリストを作成します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <returns>パラメータのリスト</returns>
    ''' <remarks></remarks>
    Private Function CreateUpdateInkaToriFlgParameterList(ByVal inRow As DataRow) As List(Of SqlParameter)
        Dim parameters As New List(Of SqlParameter)

        With parameters

            ' SET
            .Add(MyBase.GetSqlParameter("@INKA_TORI_FLG", LMConst.FLG.ON, DBDataType.CHAR))

            ' WHERE
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@WH_CD", inRow("WH_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@CUST_CD_L", inRow("CUST_CD_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INPUT_DATE", inRow("INPUT_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SEQ", inRow("SEQ").ToString(), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_CUR", inRow("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_CUR", inRow("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        ' 共通
        Me.SetParamCommonSystemUpd(parameters)

        Return parameters
    End Function

#End Region

#Region "削除処理"

    ''' ==========================================================================
    ''' <summary>入荷検品ワーク更新用(INKA_TORI_FLG更新)メソッド</summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' ==========================================================================
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB040IN_DELETE")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim sql As String = Me.SetSchemaNm(LMB040DAC.SQL_DELETE_B_INKA_KENPIN_WK, inRow("NRS_BR_CD").ToString)
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        'SQLパラメータ設定
        Dim sqlParamList As List(Of SqlParameter) = Me.CreateUpdateInkaToriFlgParameterList(inRow)
        cmd.Parameters.AddRange(sqlParamList.ToArray)

        MyBase.Logger.WriteSQLLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷検品ワーク更新用(INKA_TORI_FLG更新)パラメータのリストを作成します
    ''' </summary>
    ''' <param name="inRow">入力情報</param>
    ''' <returns>パラメータのリスト</returns>
    ''' <remarks></remarks>
    Private Function CreateDeleteActionParameterList(ByVal inRow As DataRow) As List(Of SqlParameter)
        Dim parameters As New List(Of SqlParameter)

        With parameters
            ' WHERE
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", inRow("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@WH_CD", inRow("WH_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@CUST_CD_L", inRow("CUST_CD_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@INPUT_DATE", inRow("INPUT_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SEQ", inRow("SEQ").ToString(), DBDataType.NUMERIC))
            .Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_CUR", inRow("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_CUR", inRow("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        Return parameters
    End Function

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
