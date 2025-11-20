' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG900DAC : 請求処理 請求取込データ抽出作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMG900DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG900DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "環境変更用定義"

    ''' <summary>
    ''' 都道府県区分コードのグループ区分（ABM_DB..Z_KBN）
    ''' </summary>
    ''' <remarks>
    ''' 経緯
    ''' 　開発時は"K00003"だったが、その後グループ区分を変更されて"T10001"に変更となる
    ''' 　しかしスムーズな移行が行われなかったため、この定義の変更のみで対応できるようにした
    ''' </remarks>
    Friend Const ABM_DB_TODOFUKEN As String = "T10001"
    'Friend Const ABM_DB_TODOFUKEN As String = "K00003"

#End Region

#Region "保管料検索 SQL"

    ''' <summary>
    ''' 保管料移行データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_EXIST_HOKAN_IKO_DATA As String = "SELECT                                               " & vbNewLine _
                                                  & "       COUNT(PRT.NRS_BR_CD)     AS    SELECT_CNT            " & vbNewLine _
                                                  & "FROM                                                        " & vbNewLine _
                                                  & "       (SELECT                                              " & vbNewLine _
                                                  & "            P.NRS_BR_CD              AS   NRS_BR_CD         " & vbNewLine _
                                                  & "           ,P.GOODS_CD_NRS           AS   GOODS_CD_NRS      " & vbNewLine _
                                                  & "           ,P.JOB_NO                 AS   JOB_NO            " & vbNewLine _
                                                  & "           ,P.LOT_NO                 AS   LOT_NO            " & vbNewLine _
                                                  & "           ,P.SEKY_FLG               AS   SEKY_FLG          " & vbNewLine _
                                                  & "           ,P.INV_DATE_TO            AS   INV_DATE_TO       " & vbNewLine _
                                                  & "           ,P.TAX_KB                 AS   TAX_KB            " & vbNewLine _
                                                  & "           ,SUM(P.STORAGE_AMO_TTL)   AS   STORAGE_AMO_TTL   " & vbNewLine _
                                                  & "           ,P.SYS_DEL_FLG            AS   SYS_DEL_FLG       " & vbNewLine _
                                                  & "        FROM                                                " & vbNewLine _
                                                  & "            $LM_TRN$..G_SEKY_MEISAI_PRT  AS  P              " & vbNewLine _
                                                  & "        WHERE                                               " & vbNewLine _
                                                  & "            P.SEKY_FLG             =   '00'                 " & vbNewLine _
                                                  & "        AND P.SYS_DEL_FLG          =   '0'                  " & vbNewLine _
                                                  & "        GROUP BY                                            " & vbNewLine _
                                                  & "            P.NRS_BR_CD                                     " & vbNewLine _
                                                  & "           ,P.GOODS_CD_NRS                                  " & vbNewLine _
                                                  & "           ,P.JOB_NO                                        " & vbNewLine _
                                                  & "           ,P.LOT_NO                                        " & vbNewLine _
                                                  & "           ,P.SEKY_FLG                                      " & vbNewLine _
                                                  & "           ,P.INV_DATE_TO                                   " & vbNewLine _
                                                  & "           ,P.TAX_KB                                        " & vbNewLine _
                                                  & "           ,P.SYS_DEL_FLG                                   " & vbNewLine _
                                                  & "        HAVING SUM(P.STORAGE_AMO_TTL)      <>  '0'          " & vbNewLine _
                                                  & "       )   AS  PRT                                          " & vbNewLine _
                                                  & "  LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )    AS  GOD         " & vbNewLine _
                                                  & "       ON     GOD.GOODS_CD_NRS    =   PRT.GOODS_CD_NRS      " & vbNewLine _
                                                  & "       AND    GOD.NRS_BR_CD       =   PRT.NRS_BR_CD         " & vbNewLine _
                                                  & "       LEFT JOIN       $LM_MST$..M_CUST     AS  CST         " & vbNewLine _
                                                  & "       ON     CST.NRS_BR_CD       =   GOD.NRS_BR_CD         " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_L       =   GOD.CUST_CD_L         " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_M       =   GOD.CUST_CD_M         " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_S       =   GOD.CUST_CD_S         " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_SS      =   GOD.CUST_CD_SS        " & vbNewLine _
                                                  & "       LEFT OUTER JOIN    (SELECT                           " & vbNewLine _
                                                  & "              ST.NRS_BR_CD     AS  NRS_BR_CD                " & vbNewLine _
                                                  & "             ,ST.GOODS_CD_NRS  AS  GOODS_CD_NRS             " & vbNewLine _
                                                  & "             ,ST.JOB_NO        AS  JOB_NO                   " & vbNewLine _
                                                  & "             ,ST.LOT_NO        AS  LOT_NO                   " & vbNewLine _
                                                  & "             ,ST.SEKY_FLG      AS  SEKY_FLG                 " & vbNewLine _
                                                  & "             ,ST.INV_DATE_TO   AS  INV_DATE_TO              " & vbNewLine _
                                                  & "             ,ST.WH_CD         AS  WH_CD                    " & vbNewLine _
                                                  & "             ,ST.TOU_NO        AS  TOU_NO                   " & vbNewLine _
                                                  & "             ,ST.TAX_KB        AS  TAX_KB                   " & vbNewLine _
                                                  & "             ,SUM(ST.SEKI_ARI_NB1) + SUM(ST.SEKI_ARI_NB2) + SUM(ST.SEKI_ARI_NB3)  AS  SUM_SEKI_ARI_NB_PER_OKIBA " & vbNewLine _
                                                  & "             ,ST.SYS_DEL_FLG   AS  SYS_DEL_FLG              " & vbNewLine _
                                                  & "             ,ST.SYS_ENT_PGID   AS  SYS_ENT_PGID            " & vbNewLine _
                                                  & "            FROM                                            " & vbNewLine _
                                                  & "              $LM_TRN$..G_SEKY_TBL  AS  ST                  " & vbNewLine _
                                                  & "   LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   AS  GOD " & vbNewLine _
                                                  & "               ON     GOD.GOODS_CD_NRS     =   ST.GOODS_CD_NRS    " & vbNewLine _
                                                  & "               AND    GOD.NRS_BR_CD        =   ST.NRS_BR_CD       " & vbNewLine _
                                                  & "               LEFT JOIN       $LM_MST$..M_CUST     AS  CST       " & vbNewLine _
                                                  & "               ON     CST.NRS_BR_CD        =   GOD.NRS_BR_CD      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_L        =   GOD.CUST_CD_L      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_M        =   GOD.CUST_CD_M      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_S        =   GOD.CUST_CD_S      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_SS       =   GOD.CUST_CD_SS     " & vbNewLine _
                                                  & "            WHERE                                                 " & vbNewLine _
                                                  & "                      ST.NRS_BR_CD         =   @NRS_BR_CD         " & vbNewLine _
                                                  & "               AND    ST.INV_DATE_TO       =   @SKYU_DATE         " & vbNewLine _
                                                  & "               AND    ST.SEKY_FLG          =   '00'               " & vbNewLine _
                                                  & "               AND    ST.SYS_DEL_FLG       =   '0'                " & vbNewLine _
                                                  & "               AND    CST.HOKAN_SEIQTO_CD  =   @SEIQTO_CD         " & vbNewLine _
                                                  & "            GROUP BY                                              " & vbNewLine _
                                                  & "              ST.NRS_BR_CD                                        " & vbNewLine _
                                                  & "             ,ST.GOODS_CD_NRS                                     " & vbNewLine _
                                                  & "             ,ST.JOB_NO                                           " & vbNewLine _
                                                  & "             ,ST.LOT_NO                                           " & vbNewLine _
                                                  & "             ,ST.SEKY_FLG                                         " & vbNewLine _
                                                  & "             ,ST.INV_DATE_TO                                      " & vbNewLine _
                                                  & "             ,ST.WH_CD                                            " & vbNewLine _
                                                  & "             ,ST.TOU_NO                                           " & vbNewLine _
                                                  & "             ,ST.TAX_KB                                           " & vbNewLine _
                                                  & "             ,ST.SYS_DEL_FLG                                      " & vbNewLine _
                                                  & "             ,ST.SYS_ENT_PGID                                     " & vbNewLine _
                                                  & "            )       AS  STBL                                      " & vbNewLine _
                                                  & "       ON     STBL.NRS_BR_CD      =   PRT.NRS_BR_CD               " & vbNewLine _
                                                  & "       AND    STBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                                  & "       AND    STBL.JOB_NO         =   PRT.JOB_NO                  " & vbNewLine _
                                                  & "       AND    STBL.LOT_NO         =   PRT.LOT_NO                  " & vbNewLine _
                                                  & "       AND    STBL.INV_DATE_TO    =   PRT.INV_DATE_TO             " & vbNewLine _
                                                  & "       AND    STBL.TAX_KB         =   PRT.TAX_KB                  " & vbNewLine _
                                                  & "       AND    STBL.SYS_DEL_FLG    =   '0'                         " & vbNewLine _
                                                  & "       LEFT JOIN                                                  " & vbNewLine _
                                                  & "               (SELECT                                            " & vbNewLine _
                                                  & "                    SUBSTBL.NRS_BR_CD         AS    NRS_BR_CD     " & vbNewLine _
                                                  & "                   ,SUBSTBL.GOODS_CD_NRS      AS    GOODS_CD_NRS  " & vbNewLine _
                                                  & "                   ,SUBSTBL.JOB_NO            AS    JOB_NO        " & vbNewLine _
                                                  & "                   ,SUBSTBL.LOT_NO            AS    LOT_NO        " & vbNewLine _
                                                  & "                   ,SUBSTBL.TAX_KB            AS    TAX_KB        " & vbNewLine _
                                                  & "                   ,SUM(SUBSTBL.SEKI_ARI_NB1) + SUM(SUBSTBL.SEKI_ARI_NB2) + SUM(SUBSTBL.SEKI_ARI_NB3)    AS    SUM_SEKI_ARI_NB   " & vbNewLine _
                                                  & "               FROM                                               " & vbNewLine _
                                                  & "                   $LM_TRN$..G_SEKY_MEISAI_PRT     AS  PRT        " & vbNewLine _
                                                  & "               INNER JOIN       $LM_TRN$..G_SEKY_TBL   SUBSTBL    " & vbNewLine _
                                                  & "               ON     SUBSTBL.NRS_BR_CD      =   PRT.NRS_BR_CD    " & vbNewLine _
                                                  & "               AND    SUBSTBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS " & vbNewLine _
                                                  & "               AND    SUBSTBL.JOB_NO         =   PRT.JOB_NO       " & vbNewLine _
                                                  & "               AND    SUBSTBL.LOT_NO         =   PRT.LOT_NO       " & vbNewLine _
                                                  & "               AND    SUBSTBL.SEKY_FLG       =   PRT.SEKY_FLG     " & vbNewLine _
                                                  & "               AND    SUBSTBL.TAX_KB         =   PRT.TAX_KB       " & vbNewLine _
                                                  & "               AND    SUBSTBL.SYS_DEL_FLG    =   '0'              " & vbNewLine _
                                                  & "     LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )  AS  GOD        " & vbNewLine _
                                                  & "               ON     GOD.GOODS_CD_NRS       =   PRT.GOODS_CD_NRS " & vbNewLine _
                                                  & "               AND    GOD.NRS_BR_CD          =   PRT.NRS_BR_CD    " & vbNewLine _
                                                  & "               LEFT JOIN       $LM_MST$..M_CUST    AS  CST        " & vbNewLine _
                                                  & "               ON     CST.NRS_BR_CD          =   GOD.NRS_BR_CD    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_L          =   GOD.CUST_CD_L    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_M          =   GOD.CUST_CD_M    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_S          =   GOD.CUST_CD_S    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_SS         =   GOD.CUST_CD_SS   " & vbNewLine _
                                                  & "               WHERE                                              " & vbNewLine _
                                                  & "                      PRT.NRS_BR_CD          =   @NRS_BR_CD       " & vbNewLine _
                                                  & "               AND    PRT.INV_DATE_TO        =   @SKYU_DATE       " & vbNewLine _
                                                  & "               AND    PRT.SEKY_FLG           =   '00'             " & vbNewLine _
                                                  & "               AND    PRT.SYS_DEL_FLG        =   '0'              " & vbNewLine _
                                                  & "               AND    CST.HOKAN_SEIQTO_CD    =   @SEIQTO_CD       " & vbNewLine _
                                                  & "               GROUP    BY                                        " & vbNewLine _
                                                  & "                    SUBSTBL.NRS_BR_CD                             " & vbNewLine _
                                                  & "                   ,SUBSTBL.GOODS_CD_NRS                          " & vbNewLine _
                                                  & "                   ,SUBSTBL.JOB_NO                                " & vbNewLine _
                                                  & "                   ,SUBSTBL.LOT_NO                                " & vbNewLine _
                                                  & "                   ,SUBSTBL.TAX_KB                                " & vbNewLine _
                                                  & "               )   AS  SUB                                        " & vbNewLine _
                                                  & "       ON     STBL.NRS_BR_CD       =    SUB.NRS_BR_CD             " & vbNewLine _
                                                  & "       AND    STBL.GOODS_CD_NRS    =    SUB.GOODS_CD_NRS          " & vbNewLine _
                                                  & "       AND    STBL.JOB_NO          =    SUB.JOB_NO                " & vbNewLine _
                                                  & "       AND    STBL.LOT_NO          =    SUB.LOT_NO                " & vbNewLine _
                                                  & "       AND    STBL.TAX_KB          =    SUB.TAX_KB                " & vbNewLine _
                                                  & "--(要望番号2108) 追加START 2013.10.10                             " & vbNewLine _
                                                  & "       AND    SUB.SUM_SEKI_ARI_NB <> 0                            " & vbNewLine _
                                                  & "--(要望番号2108) 追加END 2013.10.10                               " & vbNewLine _
                                                  & "       LEFT JOIN       $LM_MST$..M_SEIQTO   AS  SQT               " & vbNewLine _
                                                  & "       ON     SQT.NRS_BR_CD        =    PRT.NRS_BR_CD             " & vbNewLine _
                                                  & "       AND    SQT.SEIQTO_CD        =    CST.HOKAN_SEIQTO_CD       " & vbNewLine _
                                                  & "WHERE                                                             " & vbNewLine _
                                                  & "       PRT.NRS_BR_CD        =    @NRS_BR_CD                       " & vbNewLine _
                                                  & "AND    PRT.INV_DATE_TO      =    @SKYU_DATE                       " & vbNewLine _
                                                  & "AND    PRT.SEKY_FLG         =    '00'                             " & vbNewLine _
                                                  & "AND    PRT.SYS_DEL_FLG      =    '0'                              " & vbNewLine _
                                                  & "AND    CST.HOKAN_SEIQTO_CD  =    @SEIQTO_CD                       " & vbNewLine _
                                                  & "AND    STBL.SUM_SEKI_ARI_NB_PER_OKIBA <> 0                        " & vbNewLine _
                                                  & "--(要望番号2108) 削除START 2013.10.10                             " & vbNewLine _
                                                  & "--AND    SUB.SUM_SEKI_ARI_NB <> 0                            " & vbNewLine _
                                                  & "--(要望番号2108) 削除START 2013.10.10                             " & vbNewLine _
                                                  & "AND    STBL.SYS_ENT_PGID    =    'IKOU '                          " & vbNewLine

    ''' <summary>
    ''' 坪貸し料取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TUBOKASHI As String = "SELECT                                                   " & vbNewLine _
                                                 & "        '01'                      AS    GROUP_KB         " & vbNewLine _
                                                 & "        ,MAIN.SEIQKMK_CD          AS    SEIQKMK_CD       " & vbNewLine _
                                                 & "        ,MAIN.SEIQKMK_NM          AS    SEIQKMK_NM       " & vbNewLine _
                                                 & "        ,MAIN.KEIRI_KB            AS    KEIRI_KB         " & vbNewLine _
                                                 & "        ,'01'                     AS    TAX_KB           " & vbNewLine _
                                                 & "        ,MAIN.TAX_KB_NM           AS    TAX_KB_NM        " & vbNewLine _
                                                 & "        ,MAIN.SEIQKMK_CD          AS    SEIQKMK_CD       " & vbNewLine _
                                                 & "        ,ISNULL(MAIN.BUSYO_CD,MAIN.BUSYO_CD_DEF) AS    BUSYO_CD   " & vbNewLine _
                                                 & "        ,MAIN.KEISAN_TLGK         AS    KEISAN_TLGK      " & vbNewLine _
                                                 & "        ,CAST(ISNULL(SQT.STORAGE_NR,0) AS DECIMAL(5,2)) AS    NEBIKI_RT        " & vbNewLine _
                                                 & "        ,ISNULL(SQT.STORAGE_NG,0) AS    NEBIKI_GK        " & vbNewLine _
                                                 & "        ,''                       AS    TEKIYO           " & vbNewLine _
                                                 & "        ,'00'                     AS    TEMPLATE_IMP_FLG " & vbNewLine _
                                                 & "--真荷主初期値 start                                     " & vbNewLine _
                                                 & "        ,DEF.DEF_TCUST_BPCD       AS    TCUST_BPCD       " & vbNewLine _
                                                 & "        ,MBP.BP_NM1               AS    TCUST_BPNM       " & vbNewLine _
                                                 & "--真荷主初期値 end                                       " & vbNewLine _
                                                 & "--セグメント初期値 start                                 " & vbNewLine _
                                                 & "        ,DEF.DEF_SEG_SEIHIN       AS    PRODUCT_SEG_CD   " & vbNewLine _
                                                 & "        ,DEF.DEF_SEG_CHIIKI       AS    ORIG_SEG_CD      " & vbNewLine _
                                                 & "        ,DEF.DEF_SEG_CHIIKI       AS    DEST_SEG_CD      " & vbNewLine _
                                                 & "--セグメント初期値 end                                   " & vbNewLine _
                                                 & "--運賃横持科目分けの余波 start                           " & vbNewLine _
                                                 & "        ,''                       AS    SEIQKMK_CD_S     " & vbNewLine _
                                                 & "--運賃横持科目分けの余波 end                             " & vbNewLine _
                                                 & "FROM                                                     " & vbNewLine _
                                                 & "    (                                                    " & vbNewLine _
                                                 & "        SELECT                                           " & vbNewLine _
                                                 & "             KBN1.KBN_NM2        AS    SEIQKMK_CD        " & vbNewLine _
                                                 & "            ,KMK.SEIQKMK_NM      AS    SEIQKMK_NM        " & vbNewLine _
                                                 & "            ,KMK.KEIRI_KB        AS    KEIRI_KB          " & vbNewLine _
                                                 & "            ,KBN2.KBN_NM1        AS    TAX_KB_NM         " & vbNewLine _
                                                 & "            ,KBN3.KBN_NM1        AS    BUSYO_CD          " & vbNewLine _
                                                 & "            ,KBN4.KBN_CD         AS    BUSYO_CD_DEF      " & vbNewLine _
                                                 & "            ,SUM(ZON.TSUBO_AM)   AS    KEISAN_TLGK       " & vbNewLine _
                                                 & "            ,ZON.NRS_BR_CD       AS    NRS_BR_CD         " & vbNewLine _
                                                 & "            ,ZON.SEIQTO_CD       AS    SEIQTO_CD         " & vbNewLine _
                                                 & "        FROM                                             " & vbNewLine _
                                                 & "            $LM_MST$..M_ZONE                   ZON       " & vbNewLine _
                                                 & "        LEFT JOIN       $LM_MST$..Z_KBN        KBN1      " & vbNewLine _
                                                 & "        ON     KBN1.KBN_GROUP_CD   =   'S064'            " & vbNewLine _
                                                 & "        AND    KBN1.KBN_NM1        =   '01'              " & vbNewLine _
                                                 & "        AND    KBN1.KBN_NM3        =   '01'              " & vbNewLine _
                                                 & "        AND    KBN1.SYS_DEL_FLG    =   '0'               " & vbNewLine _
                                                 & "        LEFT JOIN       $LM_MST$..M_SEIQKMK    KMK       " & vbNewLine _
                                                 & "        ON     KMK.GROUP_KB        =   '01'              " & vbNewLine _
                                                 & "        AND    KMK.SEIQKMK_CD      =   KBN1.KBN_NM2      " & vbNewLine _
                                                 & "        AND    KMK.SYS_DEL_FLG     =   '0'               " & vbNewLine _
                                                 & "        LEFT JOIN       $LM_MST$..Z_KBN        KBN2      " & vbNewLine _
                                                 & "        ON     KBN2.KBN_GROUP_CD   =   'Z001'            " & vbNewLine _
                                                 & "        AND    KBN2.KBN_CD         =   '01'              " & vbNewLine _
                                                 & "        AND    KBN2.SYS_DEL_FLG    =   '0'               " & vbNewLine _
                                                 & "        LEFT JOIN       $LM_MST$..Z_KBN        KBN3      " & vbNewLine _
                                                 & "        ON     KBN3.KBN_GROUP_CD   =   'B008'            " & vbNewLine _
                                                 & "        AND    KBN3.KBN_NM2        =   ZON.WH_CD         " & vbNewLine _
                                                 & "        AND    KBN3.KBN_NM3        =   ZON.TOU_NO        " & vbNewLine _
                                                 & "        AND    KBN3.KBN_NM4        =   ZON.NRS_BR_CD     " & vbNewLine _
                                                 & "        AND    KBN3.SYS_DEL_FLG    =   '0'               " & vbNewLine _
                                                 & "        LEFT JOIN       $LM_MST$..Z_KBN          KBN4    " & vbNewLine _
                                                 & "        ON     KBN4.KBN_GROUP_CD   =   'B007'            " & vbNewLine _
                                                 & "        AND    KBN4.KBN_NM2        =   ZON.NRS_BR_CD     " & vbNewLine _
                                                 & "        AND    KBN4.KBN_CD         =   @BUSYO_CD         " & vbNewLine _
                                                 & "        AND    KBN4.SYS_DEL_FLG    =   '0'               " & vbNewLine _
                                                 & "        WHERE                                            " & vbNewLine _
                                                 & "               ZON.NRS_BR_CD       =   @NRS_BR_CD        " & vbNewLine _
                                                 & "        AND    ZON.SEIQTO_CD       =   @SEIQTO_CD        " & vbNewLine _
                                                 & "        AND    ZON.SYS_DEL_FLG     =   '0'               " & vbNewLine _
                                                 & "        AND    ZON.TSUBO_AM        <>   0                " & vbNewLine _
                                                 & "        GROUP BY                                         " & vbNewLine _
                                                 & "             KBN1.KBN_NM2                                " & vbNewLine _
                                                 & "            ,KMK.SEIQKMK_NM                              " & vbNewLine _
                                                 & "            ,KMK.KEIRI_KB                                " & vbNewLine _
                                                 & "            ,KBN2.KBN_NM1                                " & vbNewLine _
                                                 & "            ,KBN3.KBN_NM1                                " & vbNewLine _
                                                 & "            ,KBN4.KBN_CD                                 " & vbNewLine _
                                                 & "            ,ZON.NRS_BR_CD                               " & vbNewLine _
                                                 & "            ,ZON.SEIQTO_CD                               " & vbNewLine _
                                                 & "        ) MAIN                                           " & vbNewLine _
                                                 & "        LEFT JOIN       $LM_MST$..M_SEIQTO     SQT       " & vbNewLine _
                                                 & "        ON     SQT.NRS_BR_CD        =    MAIN.NRS_BR_CD  " & vbNewLine _
                                                 & "        AND    SQT.SEIQTO_CD        =    MAIN.SEIQTO_CD  " & vbNewLine _
                                                 & "--セグメント初期値取得 start                             " & vbNewLine _
                                                 & "LEFT JOIN                                                " & vbNewLine _
                                                 & "  (                                                      " & vbNewLine _
                                                 & "    SELECT TOP 1                                         " & vbNewLine _
                                                 & "       MCT.TCUST_BPCD AS DEF_TCUST_BPCD                  " & vbNewLine _
                                                 & "      ,SSG.CNCT_SEG_CD AS DEF_SEG_SEIHIN                 " & vbNewLine _
                                                 & "      ,CSG.CNCT_SEG_CD AS DEF_SEG_CHIIKI                 " & vbNewLine _
                                                 & "    FROM                                                 " & vbNewLine _
                                                 & "      $LM_MST$..M_CUST MCT                               " & vbNewLine _
                                                 & "    LEFT JOIN                                            " & vbNewLine _
                                                 & "      ABM_DB..M_SEGMENT SSG                              " & vbNewLine _
                                                 & "    ON                                                   " & vbNewLine _
                                                 & "      SSG.CNCT_SEG_CD = MCT.PRODUCT_SEG_CD               " & vbNewLine _
                                                 & "      AND SSG.DATA_TYPE_CD = '00002'                     " & vbNewLine _
                                                 & "      AND SSG.KBN_LANG = @KBN_LANG                       " & vbNewLine _
                                                 & "      AND SSG.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                 & "    LEFT JOIN                                            " & vbNewLine _
                                                 & "      $LM_MST$..M_SOKO SOKO                              " & vbNewLine _
                                                 & "    ON                                                   " & vbNewLine _
                                                 & "      SOKO.WH_CD = MCT.DEFAULT_SOKO_CD                   " & vbNewLine _
                                                 & "      AND SOKO.SYS_DEL_FLG = '0'                         " & vbNewLine _
                                                 & "    LEFT JOIN                                            " & vbNewLine _
                                                 & "      ABM_DB..Z_KBN AKB                                  " & vbNewLine _
                                                 & "    ON                                                   " & vbNewLine _
                                                 & "      AKB.KBN_GROUP_CD = '" & ABM_DB_TODOFUKEN & "'                " & vbNewLine _
                                                 & "      AND AKB.KBN_LANG = @KBN_LANG                       " & vbNewLine _
                                                 & "      AND AKB.KBN_NM3 = LEFT(SOKO.JIS_CD,2)              " & vbNewLine _
                                                 & "      AND AKB.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                 & "    LEFT JOIN                                            " & vbNewLine _
                                                 & "      ABM_DB..M_SEGMENT CSG                              " & vbNewLine _
                                                 & "    ON                                                   " & vbNewLine _
                                                 & "      CSG.DATA_TYPE_CD = '00001'                         " & vbNewLine _
                                                 & "      AND CSG.KBN_LANG = @KBN_LANG                       " & vbNewLine _
                                                 & "      AND CSG.KBN_GROUP_CD = AKB.KBN_GRP_REF1            " & vbNewLine _
                                                 & "      AND CSG.KBN_CD = AKB.KBN_CD_REF1                   " & vbNewLine _
                                                 & "      AND CSG.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                 & "    WHERE                                                " & vbNewLine _
                                                 & "      MCT.NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
                                                 & "      AND (   MCT.OYA_SEIQTO_CD = @SEIQTO_CD             " & vbNewLine _
                                                 & "           OR MCT.HOKAN_SEIQTO_CD = @SEIQTO_CD           " & vbNewLine _
                                                 & "           OR MCT.NIYAKU_SEIQTO_CD = @SEIQTO_CD          " & vbNewLine _
                                                 & "           OR MCT.UNCHIN_SEIQTO_CD = @SEIQTO_CD          " & vbNewLine _
                                                 & "           OR MCT.SAGYO_SEIQTO_CD = @SEIQTO_CD)          " & vbNewLine _
                                                 & "      AND MCT.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                                 & "    ORDER BY                                             " & vbNewLine _
                                                 & "       MCT.CUST_CD_L                                     " & vbNewLine _
                                                 & "      ,MCT.CUST_CD_M                                     " & vbNewLine _
                                                 & "  ) DEF                                                  " & vbNewLine _
                                                 & "ON 1 = 1                                                 " & vbNewLine _
                                                 & "--セグメント初期値取得 end                               " & vbNewLine _
                                                 & "--真荷主初期値取得 start                                 " & vbNewLine _
                                                 & "LEFT JOIN       ABM_DB..M_BP              MBP            " & vbNewLine _
                                                 & "ON     MBP.BP_CD          =   DEF.DEF_TCUST_BPCD         " & vbNewLine _
                                                 & "AND    MBP.SYS_DEL_FLG    =    '0'                       " & vbNewLine _
                                                 & "--真荷主初期値取得 end                                   " & vbNewLine

    ''' <summary>
    ''' 保管料取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HOKAN As String = " SET ARITHABORT ON                                                     " & vbNewLine _
                                              & " SET ARITHIGNORE ON                                       " & vbNewLine _
                                              & "    SELECT              " & vbNewLine _
                                              & "     '01'                      AS    GROUP_KB              " & vbNewLine _
                                              & "    ,KBN1.KBN_NM2              AS    SEIQKMK_CD            " & vbNewLine _
                                              & "    ,KMK.SEIQKMK_NM            AS    SEIQKMK_NM            " & vbNewLine _
                                              & "    ,KMK.KEIRI_KB              AS    KEIRI_KB              " & vbNewLine _
                                              & "    ,PRT.TAX_KB                AS    TAX_KB                " & vbNewLine _
                                              & "    ,KBN2.KBN_NM1              AS    TAX_KB_NM             " & vbNewLine _
                                              & "    ,ISNULL(KBN3.KBN_NM1,KBN4.KBN_CD) AS    BUSYO_CD       " & vbNewLine _
                                              & "    ,PRT.STORAGE_AMO_TTL       AS    STORAGE_AMO_TTL       " & vbNewLine _
                                              & "    ,ISNULL(SQT.STORAGE_MIN,0) AS    STORAGE_MIN           " & vbNewLine _
                                              & "    ,(STBL.SUM_SEKI_ARI_NB_PER_OKIBA) / SUB.SUM_SEKI_ARI_NB                          AS    ANBUN_RATE           " & vbNewLine _
                                              & "    ,PRT.STORAGE_AMO_TTL * (STBL.SUM_SEKI_ARI_NB_PER_OKIBA) / SUB.SUM_SEKI_ARI_NB    AS    SEIQ_STORAGE_AMO_TTL " & vbNewLine _
                                              & "    ,PRT.GOODS_CD_NRS          AS    GOODS_CD_NRS          " & vbNewLine _
                                              & "    ,PRT.LOT_NO                AS    LOT_NO                " & vbNewLine _
                                              & "    ,CAST(ISNULL(SQT.STORAGE_NR,0) AS DECIMAL(5,2))  AS    NEBIKI_RT             " & vbNewLine _
                                              & "    ,ISNULL(SQT.STORAGE_NG,0)  AS    NEBIKI_GK             " & vbNewLine _
                                              & "    ,''                        AS    TEKIYO                " & vbNewLine _
                                              & "    ,'00'                      AS    TEMPLATE_IMP_FLG      " & vbNewLine _
                                              & "    ,SUM_PRT.SUM_STORAGE_AMO_TTL AS  SUM_STORAGE_AMO_TTL   " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "    ,STBL.WH_CD                AS    WH_CD                 " & vbNewLine _
                                              & "    ,STBL.WH_KB                AS    WH_KB                 " & vbNewLine _
                                              & "    ,STBL.TOU_NO               AS    TOU_N                 " & vbNewLine _
                                              & "    ,STBL.SITU_NO              AS    SITU_NO               " & vbNewLine _
                                              & "    ,STBL.JISYATASYA_KB        AS    JISYATASYA_KB         " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "--真荷主 start                                             " & vbNewLine _
                                              & "    ,CST.TCUST_BPCD            AS    TCUST_BPCD            " & vbNewLine _
                                              & "    ,MBP.BP_NM1                AS    TCUST_BPNM            " & vbNewLine _
                                              & "--真荷主 end                                               " & vbNewLine _
                                              & "--製品セグメント start                                     " & vbNewLine _
                                              & "    ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''            " & vbNewLine _
                                              & "          THEN SSG2.CNCT_SEG_CD                            " & vbNewLine _
                                              & "          ELSE SSG1.CNCT_SEG_CD                            " & vbNewLine _
                                              & "          END AS PRODUCT_SEG_CD                            " & vbNewLine _
                                              & "--製品セグメント end                                       " & vbNewLine _
                                              & "--地域セグメント start                                     " & vbNewLine _
                                              & "    ,STBL.CNCT_SEG_CD          AS    ORIG_SEG_CD           " & vbNewLine _
                                              & "    ,STBL.CNCT_SEG_CD          AS    DEST_SEG_CD           " & vbNewLine _
                                              & "--地域セグメント end                                       " & vbNewLine _
                                              & "FROM                                                       " & vbNewLine _
                                              & "       (SELECT                                             " & vbNewLine _
                                              & "            P.NRS_BR_CD              AS   NRS_BR_CD        " & vbNewLine _
                                              & "           ,P.GOODS_CD_NRS           AS   GOODS_CD_NRS     " & vbNewLine _
                                              & "           ,P.JOB_NO                 AS   JOB_NO           " & vbNewLine _
                                              & "           ,P.LOT_NO                 AS   LOT_NO           " & vbNewLine _
                                              & "           ,P.SEKY_FLG               AS   SEKY_FLG         " & vbNewLine _
                                              & "           ,P.INV_DATE_TO            AS   INV_DATE_TO      " & vbNewLine _
                                              & "           ,P.TAX_KB                 AS   TAX_KB           " & vbNewLine _
                                              & "           ,SUM(P.STORAGE_AMO_TTL)   AS   STORAGE_AMO_TTL  " & vbNewLine _
                                              & "           ,P.SYS_DEL_FLG            AS   SYS_DEL_FLG      " & vbNewLine _
                                              & "        FROM                                               " & vbNewLine _
                                              & "            $LM_TRN$..G_SEKY_MEISAI_PRT  AS  P             " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "              P.NRS_BR_CD        =    @NRS_BR_CD                " & vbNewLine _
                                              & "       AND    P.INV_DATE_TO      =    @SKYU_DATE                " & vbNewLine _
                                              & "       AND    P.SEKY_FLG         =    '00'                      " & vbNewLine _
                                              & "       AND    P.SYS_DEL_FLG      =    '0'                       " & vbNewLine _
                                              & "        GROUP BY                                           " & vbNewLine _
                                              & "            P.NRS_BR_CD                                    " & vbNewLine _
                                              & "           ,P.GOODS_CD_NRS                                 " & vbNewLine _
                                              & "           ,P.JOB_NO                                       " & vbNewLine _
                                              & "           ,P.LOT_NO                                       " & vbNewLine _
                                              & "           ,P.SEKY_FLG                                     " & vbNewLine _
                                              & "           ,P.INV_DATE_TO                                  " & vbNewLine _
                                              & "           ,P.TAX_KB                                       " & vbNewLine _
                                              & "           ,P.SYS_DEL_FLG                                  " & vbNewLine _
                                              & "       )   AS  PRT                                         " & vbNewLine _
                                              & "LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   AS  GOD               " & vbNewLine _
                                              & "ON     GOD.GOODS_CD_NRS    =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                              & "AND    GOD.NRS_BR_CD       =   PRT.NRS_BR_CD               " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_CUST     AS  CST               " & vbNewLine _
                                              & "ON     CST.NRS_BR_CD       =   GOD.NRS_BR_CD               " & vbNewLine _
                                              & "AND    CST.CUST_CD_L       =   GOD.CUST_CD_L               " & vbNewLine _
                                              & "AND    CST.CUST_CD_M       =   GOD.CUST_CD_M               " & vbNewLine _
                                              & "AND    CST.CUST_CD_S       =   GOD.CUST_CD_S               " & vbNewLine _
                                              & "AND    CST.CUST_CD_SS      =   GOD.CUST_CD_SS              " & vbNewLine _
                                              & "--真荷主取得 start                                         " & vbNewLine _
                                              & "LEFT JOIN       ABM_DB..M_BP         AS  MBP               " & vbNewLine _
                                              & "ON     MBP.BP_CD           =   CST.TCUST_BPCD              " & vbNewLine _
                                              & "AND    MBP.SYS_DEL_FLG     =   '0'                         " & vbNewLine _
                                              & "--真荷主取得 end                                           " & vbNewLine _
                                              & "--製品セグメント取得① start                               " & vbNewLine _
                                              & "LEFT JOIN                                                  " & vbNewLine _
                                              & "  (                                                        " & vbNewLine _
                                              & "    SELECT *                                               " & vbNewLine _
                                              & "    FROM $LM_MST$..M_TANKA A                               " & vbNewLine _
                                              & "    WHERE                                                  " & vbNewLine _
                                              & "      A.STR_DATE <= @SKYU_DATE                             " & vbNewLine _
                                              & "      AND A.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                              & "      AND NOT EXISTS (                                     " & vbNewLine _
                                              & "        SELECT 1                                           " & vbNewLine _
                                              & "        FROM                                               " & vbNewLine _
                                              & "          $LM_MST$..M_TANKA B                              " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "          B.NRS_BR_CD = A.NRS_BR_CD                        " & vbNewLine _
                                              & "          AND B.CUST_CD_L = A.CUST_CD_L                    " & vbNewLine _
                                              & "          AND B.CUST_CD_M = A.CUST_CD_M                    " & vbNewLine _
                                              & "          AND B.UP_GP_CD_1 = A.UP_GP_CD_1                  " & vbNewLine _
                                              & "          AND B.STR_DATE <= @SKYU_DATE                     " & vbNewLine _
                                              & "          AND B.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                              & "          AND B.STR_DATE > A.STR_DATE                      " & vbNewLine _
                                              & "        )                                                  " & vbNewLine _
                                              & "  ) AS TNK                                                 " & vbNewLine _
                                              & "ON     TNK.NRS_BR_CD       =   GOD.NRS_BR_CD               " & vbNewLine _
                                              & "AND    TNK.CUST_CD_L       =   GOD.CUST_CD_L               " & vbNewLine _
                                              & "AND    TNK.CUST_CD_M       =   GOD.CUST_CD_M               " & vbNewLine _
                                              & "AND    TNK.UP_GP_CD_1      =   GOD.UP_GP_CD_1              " & vbNewLine _
                                              & "AND    TNK.SYS_DEL_FLG     =   '0'                         " & vbNewLine _
                                              & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG1             " & vbNewLine _
                                              & "ON     SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD         " & vbNewLine _
                                              & "AND    SSG1.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                              & "AND    SSG1.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                              & "AND    SSG1.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                              & "--製品セグメント取得① end                                 " & vbNewLine _
                                              & "--製品セグメント取得② start                               " & vbNewLine _
                                              & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG2             " & vbNewLine _
                                              & "ON     SSG2.CNCT_SEG_CD     =   CST.PRODUCT_SEG_CD         " & vbNewLine _
                                              & "AND    SSG2.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                              & "AND    SSG2.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                              & "AND    SSG2.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                              & "--製品セグメント取得② end                                 " & vbNewLine _
                                              & "LEFT OUTER JOIN    (SELECT                                 " & vbNewLine _
                                              & "       ST.NRS_BR_CD     AS  NRS_BR_CD                      " & vbNewLine _
                                              & "      ,ST.GOODS_CD_NRS  AS  GOODS_CD_NRS                   " & vbNewLine _
                                              & "      ,ST.JOB_NO        AS  JOB_NO                         " & vbNewLine _
                                              & "      ,ST.LOT_NO        AS  LOT_NO                         " & vbNewLine _
                                              & "      ,ST.SEKY_FLG      AS  SEKY_FLG                       " & vbNewLine _
                                              & "      ,ST.INV_DATE_TO   AS  INV_DATE_TO                    " & vbNewLine _
                                              & "      ,ST.WH_CD         AS  WH_CD                          " & vbNewLine _
                                              & "      ,ST.TOU_NO        AS  TOU_NO                         " & vbNewLine _
                                              & "      ,ST.TAX_KB        AS  TAX_KB                         " & vbNewLine _
                                              & "      ,SUM(ST.SEKI_ARI_NB1) + SUM(ST.SEKI_ARI_NB2) + SUM(ST.SEKI_ARI_NB3)  AS  SUM_SEKI_ARI_NB_PER_OKIBA " & vbNewLine _
                                              & "      ,ST.SYS_DEL_FLG   AS  SYS_DEL_FLG                    " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "      ,ST.SITU_NO                                          " & vbNewLine _
                                              & "      ,SOKO.WH_KB                                          " & vbNewLine _
                                              & "      ,CASE WHEN SOKO.WH_KB = '02'  --他社倉庫             " & vbNewLine _
                                              & "            THEN '02'                                      " & vbNewLine _
                                              & "            ELSE CASE WHEN MTS.JISYATASYA_KB = '02'  ----他社倉庫         " & vbNewLine _
                                              & "                      THEN '02'                            " & vbNewLine _
                                              & "                      ELSE '01'                            " & vbNewLine _
                                              & "                 END                                       " & vbNewLine _
                                              & "       END    AS  JISYATASYA_KB                            " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "--地域セグメント start                                     " & vbNewLine _
                                              & "      ,CSG1.CNCT_SEG_CD                                    " & vbNewLine _
                                              & "--地域セグメント end                                       " & vbNewLine _
                                              & "     FROM                                                  " & vbNewLine _
                                              & "       $LM_TRN$..G_SEKY_TBL  AS  ST                        " & vbNewLine _
                                              & "  LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   AS  GOD       " & vbNewLine _
                                              & "        ON     GOD.GOODS_CD_NRS     =   ST.GOODS_CD_NRS    " & vbNewLine _
                                              & "        AND    GOD.NRS_BR_CD        =   ST.NRS_BR_CD       " & vbNewLine _
                                              & "        LEFT JOIN       $LM_MST$..M_CUST     AS  CST       " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD        =   GOD.NRS_BR_CD      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L        =   GOD.CUST_CD_L      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M        =   GOD.CUST_CD_M      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S        =   GOD.CUST_CD_S      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS       =   GOD.CUST_CD_SS     " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "  LEFT JOIN       $LM_MST$..M_SOKO      AS  SOKO            " & vbNewLine _
                                              & "        ON     SOKO.WH_CD           =    ST.WH_CD          " & vbNewLine _
                                              & "--地域セグメント取得 start                                 " & vbNewLine _
                                              & "  LEFT JOIN       ABM_DB..Z_KBN         AS  AKB1           " & vbNewLine _
                                              & "        ON     AKB1.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'           " & vbNewLine _
                                              & "        AND    AKB1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                              & "        AND    AKB1.KBN_NM3         =   LEFT(SOKO.JIS_CD,2)   " & vbNewLine _
                                              & "        AND    AKB1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                              & "  LEFT JOIN       ABM_DB..M_SEGMENT     AS  CSG1           " & vbNewLine _
                                              & "        ON     CSG1.DATA_TYPE_CD    =   '00001'            " & vbNewLine _
                                              & "        AND    CSG1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                              & "        AND    CSG1.KBN_GROUP_CD    =   AKB1.KBN_GRP_REF1  " & vbNewLine _
                                              & "        AND    CSG1.KBN_CD          =   AKB1.KBN_CD_REF1   " & vbNewLine _
                                              & "        AND    CSG1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                              & "--地域セグメント取得 end                                   " & vbNewLine _
                                              & "  LEFT JOIN       $LM_MST$..M_TOU_SITU      AS  MTS          " & vbNewLine _
                                              & "        ON     MTS.WH_CD            =    ST.WH_CD          " & vbNewLine _
                                              & "        AND    MTS.TOU_NO           =    ST.TOU_NO         " & vbNewLine _
                                              & "        AND    MTS.SITU_NO          =    ST.SITU_NO        " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "     WHERE                                                 " & vbNewLine _
                                              & "               ST.NRS_BR_CD         =   @NRS_BR_CD         " & vbNewLine _
                                              & "        AND    ST.INV_DATE_TO       =   @SKYU_DATE         " & vbNewLine _
                                              & "        AND    ST.SEKY_FLG          =   '00'               " & vbNewLine _
                                              & "        AND    ST.SYS_DEL_FLG       =   '0'                " & vbNewLine _
                                              & "        AND    CST.HOKAN_SEIQTO_CD  =   @SEIQTO_CD         " & vbNewLine _
                                              & "     GROUP BY                                              " & vbNewLine _
                                              & "       ST.NRS_BR_CD                                        " & vbNewLine _
                                              & "      ,ST.GOODS_CD_NRS                                     " & vbNewLine _
                                              & "      ,ST.JOB_NO                                           " & vbNewLine _
                                              & "      ,ST.LOT_NO                                           " & vbNewLine _
                                              & "      ,ST.SEKY_FLG                                         " & vbNewLine _
                                              & "      ,ST.INV_DATE_TO                                      " & vbNewLine _
                                              & "      ,ST.WH_CD                                            " & vbNewLine _
                                              & "      ,ST.TOU_NO                                           " & vbNewLine _
                                              & "      ,ST.TAX_KB                                           " & vbNewLine _
                                              & "      ,ST.SYS_DEL_FLG                                      " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "      ,ST.SITU_NO                                          " & vbNewLine _
                                              & "      ,SOKO.WH_KB                                          " & vbNewLine _
                                              & "      ,MTS.JISYATASYA_KB                                   " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "--地域セグメント start                                     " & vbNewLine _
                                              & "      ,CSG1.CNCT_SEG_CD                                    " & vbNewLine _
                                              & "--地域セグメント end                                       " & vbNewLine _
                                              & "     )       AS  STBL                                      " & vbNewLine _
                                              & "ON     STBL.NRS_BR_CD      =   PRT.NRS_BR_CD               " & vbNewLine _
                                              & "AND    STBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                              & "AND    STBL.JOB_NO         =   PRT.JOB_NO                  " & vbNewLine _
                                              & "AND    STBL.LOT_NO         =   PRT.LOT_NO                  " & vbNewLine _
                                              & "AND    STBL.INV_DATE_TO    =   PRT.INV_DATE_TO             " & vbNewLine _
                                              & "AND    STBL.TAX_KB         =   PRT.TAX_KB                  " & vbNewLine _
                                              & "AND    STBL.SYS_DEL_FLG    =   '0'                         " & vbNewLine _
                                              & "LEFT JOIN                                                  " & vbNewLine _
                                              & "        (SELECT                                            " & vbNewLine _
                                              & "             SUBSTBL.NRS_BR_CD         AS    NRS_BR_CD     " & vbNewLine _
                                              & "            ,SUBSTBL.GOODS_CD_NRS      AS    GOODS_CD_NRS  " & vbNewLine _
                                              & "            ,SUBSTBL.JOB_NO            AS    JOB_NO        " & vbNewLine _
                                              & "            ,SUBSTBL.LOT_NO            AS    LOT_NO        " & vbNewLine _
                                              & "            ,SUBSTBL.TAX_KB            AS    TAX_KB        " & vbNewLine _
                                              & "            ,SUM(SUBSTBL.SEKI_ARI_NB1) + SUM(SUBSTBL.SEKI_ARI_NB2) + SUM(SUBSTBL.SEKI_ARI_NB3)    AS    SUM_SEKI_ARI_NB   " & vbNewLine _
                                              & "        FROM                                               " & vbNewLine _
                                              & "            $LM_TRN$..G_SEKY_MEISAI_PRT     AS  PRT        " & vbNewLine _
                                              & "        INNER JOIN       $LM_TRN$..G_SEKY_TBL   SUBSTBL    " & vbNewLine _
                                              & "        ON     SUBSTBL.NRS_BR_CD      =   PRT.NRS_BR_CD    " & vbNewLine _
                                              & "        AND    SUBSTBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS " & vbNewLine _
                                              & "        AND    SUBSTBL.JOB_NO         =   PRT.JOB_NO       " & vbNewLine _
                                              & "        AND    SUBSTBL.LOT_NO         =   PRT.LOT_NO       " & vbNewLine _
                                              & "        AND    SUBSTBL.SEKY_FLG       =   PRT.SEKY_FLG     " & vbNewLine _
                                              & "        AND    SUBSTBL.TAX_KB         =   PRT.TAX_KB       " & vbNewLine _
                                              & "        AND    SUBSTBL.SYS_DEL_FLG    =   '0'              " & vbNewLine _
                                              & "   LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   AS  GOD        " & vbNewLine _
                                              & "        ON     GOD.GOODS_CD_NRS       =   PRT.GOODS_CD_NRS " & vbNewLine _
                                              & "        AND    GOD.NRS_BR_CD          =   PRT.NRS_BR_CD    " & vbNewLine _
                                              & "        LEFT JOIN       $LM_MST$..M_CUST    AS  CST        " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD          =   GOD.NRS_BR_CD    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L          =   GOD.CUST_CD_L    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M          =   GOD.CUST_CD_M    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S          =   GOD.CUST_CD_S    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS         =   GOD.CUST_CD_SS   " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "               PRT.NRS_BR_CD          =   @NRS_BR_CD       " & vbNewLine _
                                              & "        AND    PRT.INV_DATE_TO        =   @SKYU_DATE       " & vbNewLine _
                                              & "        AND    PRT.SEKY_FLG           =   '00'             " & vbNewLine _
                                              & "        AND    PRT.SYS_DEL_FLG        =   '0'              " & vbNewLine _
                                              & "        AND    CST.HOKAN_SEIQTO_CD    =   @SEIQTO_CD       " & vbNewLine _
                                              & "      AND SUBSTBL.SEKI_ARI_NB1 + SUBSTBL.SEKI_ARI_NB2 + SUBSTBL.SEKI_ARI_NB3 <> 0" & vbNewLine _
                                              & "        GROUP    BY                                        " & vbNewLine _
                                              & "             SUBSTBL.NRS_BR_CD                             " & vbNewLine _
                                              & "            ,SUBSTBL.GOODS_CD_NRS                          " & vbNewLine _
                                              & "            ,SUBSTBL.JOB_NO                                " & vbNewLine _
                                              & "            ,SUBSTBL.LOT_NO                                " & vbNewLine _
                                              & "            ,SUBSTBL.TAX_KB                                " & vbNewLine _
                                              & "        )   AS  SUB                                        " & vbNewLine _
                                              & "ON     STBL.NRS_BR_CD       =    SUB.NRS_BR_CD             " & vbNewLine _
                                              & "AND    STBL.GOODS_CD_NRS    =    SUB.GOODS_CD_NRS          " & vbNewLine _
                                              & "AND    STBL.JOB_NO          =    SUB.JOB_NO                " & vbNewLine _
                                              & "AND    STBL.LOT_NO          =    SUB.LOT_NO                " & vbNewLine _
                                              & "AND    STBL.TAX_KB          =    SUB.TAX_KB                " & vbNewLine _
                                              & "--(要望番号2108) 追加START 2013.10.10                      " & vbNewLine _
                                              & "AND    SUB.SUM_SEKI_ARI_NB <> 0                            " & vbNewLine _
                                              & "--(要望番号2108) 追加START 2013.10.10                      " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SEIQTO   AS  SQT               " & vbNewLine _
                                              & "ON     SQT.NRS_BR_CD        =    PRT.NRS_BR_CD             " & vbNewLine _
                                              & "AND    SQT.SEIQTO_CD        =    CST.HOKAN_SEIQTO_CD       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN1              " & vbNewLine _
                                              & "ON     KBN1.KBN_GROUP_CD    =    'S064'                    " & vbNewLine _
                                              & "AND    KBN1.KBN_NM1         =    '01'                      " & vbNewLine _
                                              & "AND    KBN1.KBN_NM3         =    PRT.TAX_KB                " & vbNewLine _
                                              & "AND    KBN1.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SEIQKMK  AS  KMK               " & vbNewLine _
                                              & "ON     KMK.GROUP_KB         =    '01'                      " & vbNewLine _
                                              & "AND    KMK.SEIQKMK_CD       =    KBN1.KBN_NM2              " & vbNewLine _
                                              & "AND    KMK.SYS_DEL_FLG      =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN2              " & vbNewLine _
                                              & "ON     KBN2.KBN_GROUP_CD    =    'Z001'                    " & vbNewLine _
                                              & "AND    KBN2.KBN_CD          =    PRT.TAX_KB                " & vbNewLine _
                                              & "AND    KBN2.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN3              " & vbNewLine _
                                              & "ON     KBN3.KBN_GROUP_CD    =    'B008'                    " & vbNewLine _
                                              & "AND    KBN3.KBN_NM2         =    STBL.WH_CD                " & vbNewLine _
                                              & "AND    KBN3.KBN_NM3         =    STBL.TOU_NO               " & vbNewLine _
                                              & "AND    KBN3.KBN_NM4         =    STBL.NRS_BR_CD            " & vbNewLine _
                                              & "AND    KBN3.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN4              " & vbNewLine _
                                              & "ON     KBN4.KBN_GROUP_CD    =    'B007'                    " & vbNewLine _
                                              & "AND    KBN4.KBN_NM2         =    @NRS_BR_CD_KBN            " & vbNewLine _
                                              & "AND    KBN4.KBN_NM3         =    '1'                       " & vbNewLine _
                                              & "AND    KBN4.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & ",(SELECT                                                   " & vbNewLine _
                                              & "       SUM(PRT2.STORAGE_AMO_TTL)  AS  SUM_STORAGE_AMO_TTL  " & vbNewLine _
                                              & "  FROM                                                     " & vbNewLine _
                                              & "       $LM_TRN$..G_SEKY_MEISAI_PRT          AS  PRT2       " & vbNewLine _
                                              & "   LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )  AS  GOD        " & vbNewLine _
                                              & "        ON     GOD.GOODS_CD_NRS    =   PRT2.GOODS_CD_NRS   " & vbNewLine _
                                              & "        AND    GOD.NRS_BR_CD       =   PRT2.NRS_BR_CD      " & vbNewLine _
                                              & "        LEFT JOIN       $LM_MST$..M_CUST    AS  CST        " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD       =   GOD.NRS_BR_CD       " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L       =   GOD.CUST_CD_L       " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M       =   GOD.CUST_CD_M       " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S       =   GOD.CUST_CD_S       " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS      =   GOD.CUST_CD_SS      " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "               PRT2.NRS_BR_CD      =   @NRS_BR_CD          " & vbNewLine _
                                              & "        AND    PRT2.INV_DATE_TO    =   @SKYU_DATE          " & vbNewLine _
                                              & "        AND    PRT2.SEKY_FLG       =   '00'                " & vbNewLine _
                                              & "        AND    PRT2.SYS_DEL_FLG    =   '0'                 " & vbNewLine _
                                              & "        AND    CST.HOKAN_SEIQTO_CD =   @SEIQTO_CD          " & vbNewLine _
                                              & " )      SUM_PRT                                            " & vbNewLine _
                                              & "WHERE                                                      " & vbNewLine _
                                              & "       PRT.NRS_BR_CD        =    @NRS_BR_CD                " & vbNewLine _
                                              & "AND    PRT.INV_DATE_TO      =    @SKYU_DATE                " & vbNewLine _
                                              & "AND    PRT.SEKY_FLG         =    '00'                      " & vbNewLine _
                                              & "AND    PRT.SYS_DEL_FLG      =    '0'                       " & vbNewLine _
                                              & "AND    CST.HOKAN_SEIQTO_CD  =    @SEIQTO_CD                " & vbNewLine _
                                              & "AND    STBL.SUM_SEKI_ARI_NB_PER_OKIBA <> 0                 " & vbNewLine _
                                              & "--(要望番号2108) 削除START 2013.10.10                      " & vbNewLine _
                                              & "--AND    SUB.SUM_SEKI_ARI_NB <> 0                            " & vbNewLine _
                                              & "--(要望番号2108) 削除END 2013.10.10                        " & vbNewLine _
                                              & "ORDER BY                                                   " & vbNewLine _
                                              & "       PRT.GOODS_CD_NRS                                    " & vbNewLine _
                                              & "      ,PRT.LOT_NO                                          " & vbNewLine _
                                              & "      ,PRT.TAX_KB                                          " & vbNewLine _
                                              & "      ,STBL.SUM_SEKI_ARI_NB_PER_OKIBA                      " & vbNewLine

#End Region

#Region "荷役料検索 SQL"

    ''' <summary>
    ''' 荷役料移行データ存在チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_EXIST_NIYAKU_IKO_DATA As String = "SELECT                                                    " & vbNewLine _
                                                  & "       COUNT(PRT.NRS_BR_CD)     AS    SELECT_CNT                  " & vbNewLine _
                                                  & "FROM                                                              " & vbNewLine _
                                                  & "       (SELECT                                                    " & vbNewLine _
                                                  & "            P.NRS_BR_CD              AS   NRS_BR_CD               " & vbNewLine _
                                                  & "           ,P.GOODS_CD_NRS           AS   GOODS_CD_NRS            " & vbNewLine _
                                                  & "           ,P.JOB_NO                 AS   JOB_NO                  " & vbNewLine _
                                                  & "           ,P.LOT_NO                 AS   LOT_NO                  " & vbNewLine _
                                                  & "           ,P.SEKY_FLG               AS   SEKY_FLG                " & vbNewLine _
                                                  & "           ,P.INV_DATE_TO            AS   INV_DATE_TO             " & vbNewLine _
                                                  & "           ,P.TAX_KB                 AS   TAX_KB                  " & vbNewLine _
                                                  & "           ,SUM(P.HANDLING_AMO_TTL)  AS   HANDLING_AMO_TTL        " & vbNewLine _
                                                  & "           ,P.SYS_DEL_FLG            AS   SYS_DEL_FLG             " & vbNewLine _
                                                  & "        FROM                                                      " & vbNewLine _
                                                  & "            $LM_TRN$..G_SEKY_MEISAI_PRT  AS  P                    " & vbNewLine _
                                                  & "        WHERE                                                     " & vbNewLine _
                                                  & "            P.SEKY_FLG             =   '00'                       " & vbNewLine _
                                                  & "        AND P.SYS_DEL_FLG          =   '0'                        " & vbNewLine _
                                                  & "        GROUP BY                                                  " & vbNewLine _
                                                  & "            P.NRS_BR_CD                                           " & vbNewLine _
                                                  & "           ,P.GOODS_CD_NRS                                        " & vbNewLine _
                                                  & "           ,P.JOB_NO                                              " & vbNewLine _
                                                  & "           ,P.LOT_NO                                              " & vbNewLine _
                                                  & "           ,P.SEKY_FLG                                            " & vbNewLine _
                                                  & "           ,P.INV_DATE_TO                                         " & vbNewLine _
                                                  & "           ,P.TAX_KB                                              " & vbNewLine _
                                                  & "           ,P.SYS_DEL_FLG                                         " & vbNewLine _
                                                  & "        HAVING SUM(P.HANDLING_AMO_TTL)      <>  '0'               " & vbNewLine _
                                                  & "       )   AS  PRT                                                " & vbNewLine _
                                                  & "  LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )  AS  GOD               " & vbNewLine _
                                                  & "       ON     GOD.GOODS_CD_NRS    =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                                  & "       AND    GOD.NRS_BR_CD       =   PRT.NRS_BR_CD               " & vbNewLine _
                                                  & "       LEFT JOIN       $LM_MST$..M_CUST     AS  CST               " & vbNewLine _
                                                  & "       ON     CST.NRS_BR_CD       =   GOD.NRS_BR_CD               " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_L       =   GOD.CUST_CD_L               " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_M       =   GOD.CUST_CD_M               " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_S       =   GOD.CUST_CD_S               " & vbNewLine _
                                                  & "       AND    CST.CUST_CD_SS      =   GOD.CUST_CD_SS              " & vbNewLine _
                                                  & "       LEFT OUTER JOIN    (SELECT                                 " & vbNewLine _
                                                  & "              ST.NRS_BR_CD     AS  NRS_BR_CD                      " & vbNewLine _
                                                  & "             ,ST.GOODS_CD_NRS  AS  GOODS_CD_NRS                   " & vbNewLine _
                                                  & "             ,ST.JOB_NO        AS  JOB_NO                         " & vbNewLine _
                                                  & "             ,ST.LOT_NO        AS  LOT_NO                         " & vbNewLine _
                                                  & "             ,ST.SEKY_FLG      AS  SEKY_FLG                       " & vbNewLine _
                                                  & "             ,ST.INV_DATE_TO   AS  INV_DATE_TO                    " & vbNewLine _
                                                  & "             ,ST.WH_CD         AS  WH_CD                          " & vbNewLine _
                                                  & "             ,ST.TOU_NO        AS  TOU_NO                         " & vbNewLine _
                                                  & "             ,ST.NIYAKU_YN     AS  NIYAKU_YN                      " & vbNewLine _
                                                  & "             ,ST.TAX_KB        AS  TAX_KB                         " & vbNewLine _
                                                  & "             ,SUM(ATUKAI_NB)   AS  SUM_ATUKAI_NB_PER_OKIBA        " & vbNewLine _
                                                  & "             ,ST.SYS_DEL_FLG   AS  SYS_DEL_FLG                    " & vbNewLine _
                                                  & "             ,ST.SYS_ENT_PGID  AS  SYS_ENT_PGID                   " & vbNewLine _
                                                  & "            FROM                                                  " & vbNewLine _
                                                  & "              $LM_TRN$..G_SEKY_TBL  AS  ST                        " & vbNewLine _
                                                  & "    LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )    AS  GOD       " & vbNewLine _
                                                  & "               ON     GOD.GOODS_CD_NRS     =   ST.GOODS_CD_NRS    " & vbNewLine _
                                                  & "               AND    GOD.NRS_BR_CD        =   ST.NRS_BR_CD       " & vbNewLine _
                                                  & "               LEFT JOIN       $LM_MST$..M_CUST     AS  CST       " & vbNewLine _
                                                  & "               ON     CST.NRS_BR_CD        =   GOD.NRS_BR_CD      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_L        =   GOD.CUST_CD_L      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_M        =   GOD.CUST_CD_M      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_S        =   GOD.CUST_CD_S      " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_SS       =   GOD.CUST_CD_SS     " & vbNewLine _
                                                  & "            WHERE                                                 " & vbNewLine _
                                                  & "                      ST.NRS_BR_CD         =   @NRS_BR_CD         " & vbNewLine _
                                                  & "               AND    ST.INV_DATE_TO       =   @SKYU_DATE         " & vbNewLine _
                                                  & "               AND    ST.SEKY_FLG          =   '00'               " & vbNewLine _
                                                  & "               AND    ST.SYS_DEL_FLG       =   '0'                " & vbNewLine _
                                                  & "               AND    CST.NIYAKU_SEIQTO_CD =   @SEIQTO_CD         " & vbNewLine _
                                                  & "               AND    ST.NIYAKU_YN         =   '01'               " & vbNewLine _
                                                  & "            GROUP BY                                              " & vbNewLine _
                                                  & "              ST.NRS_BR_CD                                        " & vbNewLine _
                                                  & "             ,ST.GOODS_CD_NRS                                     " & vbNewLine _
                                                  & "             ,ST.JOB_NO                                           " & vbNewLine _
                                                  & "             ,ST.LOT_NO                                           " & vbNewLine _
                                                  & "             ,ST.SEKY_FLG                                         " & vbNewLine _
                                                  & "             ,ST.INV_DATE_TO                                      " & vbNewLine _
                                                  & "             ,ST.WH_CD                                            " & vbNewLine _
                                                  & "             ,ST.TOU_NO                                           " & vbNewLine _
                                                  & "             ,ST.NIYAKU_YN                                        " & vbNewLine _
                                                  & "             ,ST.TAX_KB                                           " & vbNewLine _
                                                  & "             ,ST.SYS_DEL_FLG                                      " & vbNewLine _
                                                  & "             ,ST.SYS_ENT_PGID                                     " & vbNewLine _
                                                  & "            )       AS  STBL                                      " & vbNewLine _
                                                  & "       ON     STBL.NRS_BR_CD      =   PRT.NRS_BR_CD               " & vbNewLine _
                                                  & "       AND    STBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                                  & "       AND    STBL.JOB_NO         =   PRT.JOB_NO                  " & vbNewLine _
                                                  & "       AND    STBL.LOT_NO         =   PRT.LOT_NO                  " & vbNewLine _
                                                  & "       AND    STBL.INV_DATE_TO    =   PRT.INV_DATE_TO             " & vbNewLine _
                                                  & "       AND    STBL.TAX_KB         =   PRT.TAX_KB                  " & vbNewLine _
                                                  & "       AND    STBL.SYS_DEL_FLG    =   '0'                         " & vbNewLine _
                                                  & "       LEFT JOIN                                                  " & vbNewLine _
                                                  & "               (SELECT                                            " & vbNewLine _
                                                  & "                    SUBSTBL.NRS_BR_CD         AS    NRS_BR_CD     " & vbNewLine _
                                                  & "                   ,SUBSTBL.GOODS_CD_NRS      AS    GOODS_CD_NRS  " & vbNewLine _
                                                  & "                   ,SUBSTBL.JOB_NO            AS    JOB_NO        " & vbNewLine _
                                                  & "                   ,SUBSTBL.LOT_NO            AS    LOT_NO        " & vbNewLine _
                                                  & "                   ,SUBSTBL.TAX_KB            AS    TAX_KB        " & vbNewLine _
                                                  & "                   ,SUM(SUBSTBL.ATUKAI_NB)    AS    SUM_ATUKAI_NB " & vbNewLine _
                                                  & "               FROM                                               " & vbNewLine _
                                                  & "                   $LM_TRN$..G_SEKY_MEISAI_PRT     AS  PRT        " & vbNewLine _
                                                  & "               INNER JOIN       $LM_TRN$..G_SEKY_TBL   SUBSTBL    " & vbNewLine _
                                                  & "               ON     SUBSTBL.NRS_BR_CD      =   PRT.NRS_BR_CD    " & vbNewLine _
                                                  & "               AND    SUBSTBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS " & vbNewLine _
                                                  & "               AND    SUBSTBL.JOB_NO         =   PRT.JOB_NO       " & vbNewLine _
                                                  & "               AND    SUBSTBL.LOT_NO         =   PRT.LOT_NO       " & vbNewLine _
                                                  & "               AND    SUBSTBL.SEKY_FLG       =   PRT.SEKY_FLG     " & vbNewLine _
                                                  & "               AND    SUBSTBL.TAX_KB         =   PRT.TAX_KB       " & vbNewLine _
                                                  & "               AND    SUBSTBL.SYS_DEL_FLG    =   '0'              " & vbNewLine _
                                                  & "    LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   AS  GOD        " & vbNewLine _
                                                  & "               ON     GOD.GOODS_CD_NRS       =   PRT.GOODS_CD_NRS " & vbNewLine _
                                                  & "               AND    GOD.NRS_BR_CD          =   PRT.NRS_BR_CD    " & vbNewLine _
                                                  & "               LEFT JOIN       $LM_MST$..M_CUST    AS  CST        " & vbNewLine _
                                                  & "               ON     CST.NRS_BR_CD          =   GOD.NRS_BR_CD    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_L          =   GOD.CUST_CD_L    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_M          =   GOD.CUST_CD_M    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_S          =   GOD.CUST_CD_S    " & vbNewLine _
                                                  & "               AND    CST.CUST_CD_SS         =   GOD.CUST_CD_SS   " & vbNewLine _
                                                  & "               WHERE                                              " & vbNewLine _
                                                  & "                      PRT.NRS_BR_CD          =   @NRS_BR_CD       " & vbNewLine _
                                                  & "               AND    PRT.INV_DATE_TO        =   @SKYU_DATE       " & vbNewLine _
                                                  & "               AND    PRT.SEKY_FLG           =   '00'             " & vbNewLine _
                                                  & "               AND    PRT.SYS_DEL_FLG        =   '0'              " & vbNewLine _
                                                  & "               AND    CST.NIYAKU_SEIQTO_CD   =   @SEIQTO_CD       " & vbNewLine _
                                                  & "               AND    SUBSTBL.NIYAKU_YN      =   '01'             " & vbNewLine _
                                                  & "               GROUP    BY                                        " & vbNewLine _
                                                  & "                    SUBSTBL.NRS_BR_CD                             " & vbNewLine _
                                                  & "                   ,SUBSTBL.GOODS_CD_NRS                          " & vbNewLine _
                                                  & "                   ,SUBSTBL.JOB_NO                                " & vbNewLine _
                                                  & "                   ,SUBSTBL.LOT_NO                                " & vbNewLine _
                                                  & "                   ,SUBSTBL.TAX_KB                                " & vbNewLine _
                                                  & "               )   AS  SUB                                        " & vbNewLine _
                                                  & "       ON     STBL.NRS_BR_CD       =    SUB.NRS_BR_CD             " & vbNewLine _
                                                  & "       AND    STBL.GOODS_CD_NRS    =    SUB.GOODS_CD_NRS          " & vbNewLine _
                                                  & "       AND    STBL.JOB_NO          =    SUB.JOB_NO                " & vbNewLine _
                                                  & "       AND    STBL.LOT_NO          =    SUB.LOT_NO                " & vbNewLine _
                                                  & "       AND    STBL.TAX_KB          =    SUB.TAX_KB                " & vbNewLine _
                                                  & "       WHERE                                                      " & vbNewLine _
                                                  & "              PRT.NRS_BR_CD        =    @NRS_BR_CD                " & vbNewLine _
                                                  & "       AND    PRT.INV_DATE_TO      =    @SKYU_DATE                " & vbNewLine _
                                                  & "       AND    PRT.SEKY_FLG         =    '00'                      " & vbNewLine _
                                                  & "       AND    PRT.SYS_DEL_FLG      =    '0'                       " & vbNewLine _
                                                  & "       AND    CST.NIYAKU_SEIQTO_CD =    @SEIQTO_CD                " & vbNewLine _
                                                  & "       AND    STBL.NIYAKU_YN       =    '01'                      " & vbNewLine _
                                                  & "       AND    STBL.SUM_ATUKAI_NB_PER_OKIBA <> 0                   " & vbNewLine _
                                                  & "       AND    STBL.SYS_ENT_PGID    =  'IKOU '                     " & vbNewLine

    ''' <summary>
    ''' 荷役料取得(SELECT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NIYAKU As String = " SET ARITHABORT ON                                                     " & vbNewLine _
                                              & " SET ARITHIGNORE ON                                       " & vbNewLine _
                                              & "    SELECT             " & vbNewLine _
                                              & "     '02'                      AS    GROUP_KB              " & vbNewLine _
                                              & "    ,KBN1.KBN_NM2              AS    SEIQKMK_CD            " & vbNewLine _
                                              & "    ,KMK.SEIQKMK_NM            AS    SEIQKMK_NM            " & vbNewLine _
                                              & "    ,KMK.KEIRI_KB              AS    KEIRI_KB              " & vbNewLine _
                                              & "    ,PRT.TAX_KB                AS    TAX_KB                " & vbNewLine _
                                              & "    ,KBN2.KBN_NM1              AS    TAX_KB_NM             " & vbNewLine _
                                              & "    ,ISNULL(KBN3.KBN_NM1,KBN4.KBN_CD) AS    BUSYO_CD       " & vbNewLine _
                                              & "    ,PRT.HANDLING_AMO_TTL      AS    HANDLING_AMO_TTL      " & vbNewLine _
                                              & "--(2012.12.06)要望番号1654 KEISAN_TLGKがNULLの場合、0を設定 -- START --  " & vbNewLine _
                                              & "    --,ROUND(PRT.HANDLING_AMO_TTL * STBL.SUM_ATUKAI_NB_PER_OKIBA / SUB.SUM_ATUKAI_NB,0)         AS KEISAN_TLGK " & vbNewLine _
                                              & "    ,ISNULL(ROUND(PRT.HANDLING_AMO_TTL * STBL.SUM_ATUKAI_NB_PER_OKIBA / SUB.SUM_ATUKAI_NB,0),0) AS KEISAN_TLGK " & vbNewLine _
                                              & "--(2012.12.06)要望番号1654 KEISAN_TLGKがNULLの場合、0を設定 --  END  --  " & vbNewLine _
                                              & "    ,PRT.GOODS_CD_NRS          AS    GOODS_CD_NRS          " & vbNewLine _
                                              & "    ,PRT.LOT_NO                AS    LOT_NO                " & vbNewLine _
                                              & "    ,CAST(ISNULL(SQT.HANDLING_NR,0) AS DECIMAL(5,2)) AS    NEBIKI_RT             " & vbNewLine _
                                              & "    ,ISNULL(SQT.HANDLING_NG,0) AS    NEBIKI_GK             " & vbNewLine _
                                              & "    ,''                        AS    TEKIYO                " & vbNewLine _
                                              & "    ,'00'                      AS    TEMPLATE_IMP_FLG      " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "    ,STBL.WH_CD                AS    WH_CD                 " & vbNewLine _
                                              & "    ,STBL.WH_KB                AS    WH_KB                 " & vbNewLine _
                                              & "    ,STBL.TOU_NO               AS    TOU_N                 " & vbNewLine _
                                              & "    ,STBL.SITU_NO              AS    SITU_NO               " & vbNewLine _
                                              & "    ,STBL.JISYATASYA_KB        AS    JISYATASYA_KB         " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "--真荷主 start                                             " & vbNewLine _
                                              & "    ,CST.TCUST_BPCD            AS    TCUST_BPCD            " & vbNewLine _
                                              & "    ,MBP.BP_NM1                AS    TCUST_BPNM            " & vbNewLine _
                                              & "--真荷主 end                                               " & vbNewLine _
                                              & "--製品セグメント start                                     " & vbNewLine _
                                              & "    ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''            " & vbNewLine _
                                              & "          THEN SSG2.CNCT_SEG_CD                            " & vbNewLine _
                                              & "          ELSE SSG1.CNCT_SEG_CD                            " & vbNewLine _
                                              & "          END AS PRODUCT_SEG_CD                            " & vbNewLine _
                                              & "--製品セグメント end                                       " & vbNewLine _
                                              & "--地域セグメント start                                     " & vbNewLine _
                                              & "    ,STBL.CNCT_SEG_CD          AS    ORIG_SEG_CD           " & vbNewLine _
                                              & "    ,STBL.CNCT_SEG_CD          AS    DEST_SEG_CD           " & vbNewLine _
                                              & "--地域セグメント end                                       " & vbNewLine _
                                              & "FROM                                                       " & vbNewLine _
                                              & "       (SELECT                                             " & vbNewLine _
                                              & "            P.NRS_BR_CD              AS   NRS_BR_CD        " & vbNewLine _
                                              & "           ,P.GOODS_CD_NRS           AS   GOODS_CD_NRS     " & vbNewLine _
                                              & "           ,P.JOB_NO                 AS   JOB_NO           " & vbNewLine _
                                              & "           ,P.LOT_NO                 AS   LOT_NO           " & vbNewLine _
                                              & "           ,P.SEKY_FLG               AS   SEKY_FLG         " & vbNewLine _
                                              & "           ,P.INV_DATE_TO            AS   INV_DATE_TO      " & vbNewLine _
                                              & "           ,P.TAX_KB                 AS   TAX_KB           " & vbNewLine _
                                              & "           ,SUM(P.HANDLING_AMO_TTL)  AS   HANDLING_AMO_TTL " & vbNewLine _
                                              & "           ,P.SYS_DEL_FLG            AS   SYS_DEL_FLG      " & vbNewLine _
                                              & "        FROM                                               " & vbNewLine _
                                              & "            $LM_TRN$..G_SEKY_MEISAI_PRT  AS  P             " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "            P.SEKY_FLG             =   '00'                " & vbNewLine _
                                              & "        AND P.SYS_DEL_FLG          =   '0'                 " & vbNewLine _
                                              & "--(2012.03.29)Builder2についか -- START --                 " & vbNewLine _
                                              & "        AND P.NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                              & "        AND P.INV_DATE_TO          = @SKYU_DATE            " & vbNewLine _
                                              & "--        AND P.NIYAKU_SEIQTO_CD     = @SEIQTO_CD            " & vbNewLine _
                                              & "--(2012.03.29)Builder2についか -- END --                   " & vbNewLine _
                                              & "        GROUP BY                                           " & vbNewLine _
                                              & "            P.NRS_BR_CD                                    " & vbNewLine _
                                              & "           ,P.GOODS_CD_NRS                                 " & vbNewLine _
                                              & "           ,P.JOB_NO                                       " & vbNewLine _
                                              & "           ,P.LOT_NO                                       " & vbNewLine _
                                              & "           ,P.SEKY_FLG                                     " & vbNewLine _
                                              & "           ,P.INV_DATE_TO                                  " & vbNewLine _
                                              & "           ,P.TAX_KB                                       " & vbNewLine _
                                              & "           ,P.SYS_DEL_FLG                                  " & vbNewLine _
                                              & "       )   AS  PRT                                         " & vbNewLine _
                                              & "LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )  AS  GOD " & vbNewLine _
                                              & "ON     GOD.GOODS_CD_NRS    =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                              & "AND    GOD.NRS_BR_CD       =   PRT.NRS_BR_CD               " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_CUST     AS  CST               " & vbNewLine _
                                              & "ON     CST.NRS_BR_CD       =   GOD.NRS_BR_CD               " & vbNewLine _
                                              & "AND    CST.CUST_CD_L       =   GOD.CUST_CD_L               " & vbNewLine _
                                              & "AND    CST.CUST_CD_M       =   GOD.CUST_CD_M               " & vbNewLine _
                                              & "AND    CST.CUST_CD_S       =   GOD.CUST_CD_S               " & vbNewLine _
                                              & "AND    CST.CUST_CD_SS      =   GOD.CUST_CD_SS              " & vbNewLine _
                                              & "--真荷主取得 start                                         " & vbNewLine _
                                              & "LEFT JOIN       ABM_DB..M_BP         AS  MBP               " & vbNewLine _
                                              & "ON     MBP.BP_CD           =   CST.TCUST_BPCD              " & vbNewLine _
                                              & "AND    MBP.SYS_DEL_FLG     =   '0'                         " & vbNewLine _
                                              & "--真荷主取得 end                                           " & vbNewLine _
                                              & "--製品セグメント取得① start                               " & vbNewLine _
                                              & "LEFT JOIN                                                  " & vbNewLine _
                                              & "  (                                                        " & vbNewLine _
                                              & "    SELECT *                                               " & vbNewLine _
                                              & "    FROM $LM_MST$..M_TANKA A                               " & vbNewLine _
                                              & "    WHERE                                                  " & vbNewLine _
                                              & "      A.STR_DATE <= @SKYU_DATE                             " & vbNewLine _
                                              & "      AND A.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                              & "      AND NOT EXISTS (                                     " & vbNewLine _
                                              & "        SELECT 1                                           " & vbNewLine _
                                              & "        FROM                                               " & vbNewLine _
                                              & "          $LM_MST$..M_TANKA B                              " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "          B.NRS_BR_CD = A.NRS_BR_CD                        " & vbNewLine _
                                              & "          AND B.CUST_CD_L = A.CUST_CD_L                    " & vbNewLine _
                                              & "          AND B.CUST_CD_M = A.CUST_CD_M                    " & vbNewLine _
                                              & "          AND B.UP_GP_CD_1 = A.UP_GP_CD_1                  " & vbNewLine _
                                              & "          AND B.STR_DATE <= @SKYU_DATE                     " & vbNewLine _
                                              & "          AND B.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                              & "          AND B.STR_DATE > A.STR_DATE                      " & vbNewLine _
                                              & "        )                                                  " & vbNewLine _
                                              & "  ) AS TNK                                                 " & vbNewLine _
                                              & "ON     TNK.NRS_BR_CD       =   GOD.NRS_BR_CD               " & vbNewLine _
                                              & "AND    TNK.CUST_CD_L       =   GOD.CUST_CD_L               " & vbNewLine _
                                              & "AND    TNK.CUST_CD_M       =   GOD.CUST_CD_M               " & vbNewLine _
                                              & "AND    TNK.UP_GP_CD_1      =   GOD.UP_GP_CD_1              " & vbNewLine _
                                              & "AND    TNK.SYS_DEL_FLG     =   '0'                         " & vbNewLine _
                                              & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG1             " & vbNewLine _
                                              & "ON     SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD         " & vbNewLine _
                                              & "AND    SSG1.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                              & "AND    SSG1.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                              & "AND    SSG1.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                              & "--製品セグメント取得① end                                 " & vbNewLine _
                                              & "--製品セグメント取得② start                               " & vbNewLine _
                                              & "LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG2             " & vbNewLine _
                                              & "ON     SSG2.CNCT_SEG_CD     =   CST.PRODUCT_SEG_CD         " & vbNewLine _
                                              & "AND    SSG2.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                              & "AND    SSG2.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                              & "AND    SSG2.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                              & "--製品セグメント取得② end                                 " & vbNewLine _
                                              & "LEFT OUTER JOIN    (SELECT                                 " & vbNewLine _
                                              & "       ST.NRS_BR_CD     AS  NRS_BR_CD                      " & vbNewLine _
                                              & "      ,ST.GOODS_CD_NRS  AS  GOODS_CD_NRS                   " & vbNewLine _
                                              & "      ,ST.JOB_NO        AS  JOB_NO                         " & vbNewLine _
                                              & "      ,ST.LOT_NO        AS  LOT_NO                         " & vbNewLine _
                                              & "      ,ST.SEKY_FLG      AS  SEKY_FLG                       " & vbNewLine _
                                              & "      ,ST.INV_DATE_TO   AS  INV_DATE_TO                    " & vbNewLine _
                                              & "      ,ST.WH_CD         AS  WH_CD                          " & vbNewLine _
                                              & "      ,ST.TOU_NO        AS  TOU_NO                         " & vbNewLine _
                                              & "      ,ST.NIYAKU_YN     AS  NIYAKU_YN                      " & vbNewLine _
                                              & "      ,ST.TAX_KB        AS  TAX_KB                         " & vbNewLine _
                                              & "--(2012.12.05)要望番号1645 0除算対応 --- START ---         " & vbNewLine _
                                              & "--      ,SUM(ATUKAI_NB) AS  SUM_ATUKAI_NB_PER_OKIBA        " & vbNewLine _
                                              & "--(2017.01.18)      ,REPLACE(SUM(ATUKAI_NB),0,1) AS  SUM_ATUKAI_NB_PER_OKIBA                       " & vbNewLine _
                                              & "      ,CASE WHEN SUM(ATUKAI_NB) = 0 THEN 1 ELSE SUM(ATUKAI_NB) END AS  SUM_ATUKAI_NB_PER_OKIBA     " & vbNewLine _
                                              & "--(2012.12.05)要望番号1645 0除算対応 ---  END  ---         " & vbNewLine _
                                              & "      ,ST.SYS_DEL_FLG   AS  SYS_DEL_FLG                    " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "      ,ST.SITU_NO                                          " & vbNewLine _
                                              & "      ,SOKO.WH_KB                                          " & vbNewLine _
                                              & "      ,CASE WHEN SOKO.WH_KB = '02'  --他社倉庫             " & vbNewLine _
                                              & "            THEN '02'                                      " & vbNewLine _
                                              & "            ELSE CASE WHEN MTS.JISYATASYA_KB = '02'  ----他社倉庫  " & vbNewLine _
                                              & "                      THEN '02'                            " & vbNewLine _
                                              & "                      ELSE '01'                            " & vbNewLine _
                                              & "                  END                                      " & vbNewLine _
                                              & "       END    AS  JISYATASYA_KB                            " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "--地域セグメント start                                     " & vbNewLine _
                                              & "      ,CSG1.CNCT_SEG_CD                                    " & vbNewLine _
                                              & "--地域セグメント end                                       " & vbNewLine _
                                              & "     FROM                                                  " & vbNewLine _
                                              & "       $LM_TRN$..G_SEKY_TBL  AS  ST                        " & vbNewLine _
                                              & "LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )  AS  GOD " & vbNewLine _
                                              & "        ON     GOD.GOODS_CD_NRS     =   ST.GOODS_CD_NRS    " & vbNewLine _
                                              & "        AND    GOD.NRS_BR_CD        =   ST.NRS_BR_CD       " & vbNewLine _
                                              & "        LEFT JOIN       $LM_MST$..M_CUST     AS  CST       " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD        =   GOD.NRS_BR_CD      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L        =   GOD.CUST_CD_L      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M        =   GOD.CUST_CD_M      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S        =   GOD.CUST_CD_S      " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS       =   GOD.CUST_CD_SS     " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SOKO      AS  SOKO             " & vbNewLine _
                                              & "  ON     SOKO.WH_CD          =    ST.WH_CD                 " & vbNewLine _
                                              & "--地域セグメント取得① start                               " & vbNewLine _
                                              & "  LEFT JOIN       ABM_DB..Z_KBN         AS  AKB1           " & vbNewLine _
                                              & "        ON     AKB1.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'           " & vbNewLine _
                                              & "        AND    AKB1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                              & "        AND    AKB1.KBN_NM3         =   LEFT(SOKO.JIS_CD,2)   " & vbNewLine _
                                              & "        AND    AKB1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                              & "  LEFT JOIN       ABM_DB..M_SEGMENT     AS  CSG1           " & vbNewLine _
                                              & "        ON     CSG1.DATA_TYPE_CD    =   '00001'            " & vbNewLine _
                                              & "        AND    CSG1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                              & "        AND    CSG1.KBN_GROUP_CD    =   AKB1.KBN_GRP_REF1  " & vbNewLine _
                                              & "        AND    CSG1.KBN_CD          =   AKB1.KBN_CD_REF1   " & vbNewLine _
                                              & "        AND    CSG1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                              & "--地域セグメント取得 end                                   " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_TOU_SITU      AS  MTS          " & vbNewLine _
                                              & "  ON     MTS.WH_CD          =    ST.WH_CD                  " & vbNewLine _
                                              & "  AND    MTS.TOU_NO         =    ST.TOU_NO                 " & vbNewLine _
                                              & "  AND    MTS.SITU_NO        =    ST.SITU_NO                " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "     WHERE                                                 " & vbNewLine _
                                              & "               ST.NRS_BR_CD         =   @NRS_BR_CD         " & vbNewLine _
                                              & "        AND    ST.INV_DATE_TO       =   @SKYU_DATE         " & vbNewLine _
                                              & "        AND    ST.SEKY_FLG          =   '00'               " & vbNewLine _
                                              & "        AND    ST.SYS_DEL_FLG       =   '0'                " & vbNewLine _
                                              & "        AND    CST.NIYAKU_SEIQTO_CD =   @SEIQTO_CD         " & vbNewLine _
                                              & "        AND    ST.NIYAKU_YN         =   '01'               " & vbNewLine _
                                              & "     GROUP BY                                              " & vbNewLine _
                                              & "       ST.NRS_BR_CD                                        " & vbNewLine _
                                              & "      ,ST.GOODS_CD_NRS                                     " & vbNewLine _
                                              & "      ,ST.JOB_NO                                           " & vbNewLine _
                                              & "      ,ST.LOT_NO                                           " & vbNewLine _
                                              & "      ,ST.SEKY_FLG                                         " & vbNewLine _
                                              & "      ,ST.INV_DATE_TO                                      " & vbNewLine _
                                              & "      ,ST.WH_CD                                            " & vbNewLine _
                                              & "      ,ST.TOU_NO                                           " & vbNewLine _
                                              & "      ,ST.NIYAKU_YN                                        " & vbNewLine _
                                              & "      ,ST.TAX_KB                                           " & vbNewLine _
                                              & "      ,ST.SYS_DEL_FLG                                      " & vbNewLine _
                                              & "--ADD 2016/09/06 Start 再保管対応                          " & vbNewLine _
                                              & "      ,ST.SITU_NO                                          " & vbNewLine _
                                              & "      ,SOKO.WH_KB                                          " & vbNewLine _
                                              & "      ,MTS.JISYATASYA_KB                                   " & vbNewLine _
                                              & "--ADD 2016/09/06 End                                       " & vbNewLine _
                                              & "--地域セグメント start                                     " & vbNewLine _
                                              & "      ,CSG1.CNCT_SEG_CD                                    " & vbNewLine _
                                              & "--地域セグメント end                                       " & vbNewLine _
                                              & "     )       AS  STBL                                      " & vbNewLine _
                                              & "ON     STBL.NRS_BR_CD      =   PRT.NRS_BR_CD               " & vbNewLine _
                                              & "AND    STBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS            " & vbNewLine _
                                              & "AND    STBL.JOB_NO         =   PRT.JOB_NO                  " & vbNewLine _
                                              & "AND    STBL.LOT_NO         =   PRT.LOT_NO                  " & vbNewLine _
                                              & "AND    STBL.INV_DATE_TO    =   PRT.INV_DATE_TO             " & vbNewLine _
                                              & "AND    STBL.TAX_KB         =   PRT.TAX_KB                  " & vbNewLine _
                                              & "AND    STBL.SYS_DEL_FLG    =   '0'                         " & vbNewLine _
                                              & "LEFT JOIN                                                  " & vbNewLine _
                                              & "        (SELECT                                            " & vbNewLine _
                                              & "             SUBSTBL.NRS_BR_CD         AS    NRS_BR_CD     " & vbNewLine _
                                              & "            ,SUBSTBL.GOODS_CD_NRS      AS    GOODS_CD_NRS  " & vbNewLine _
                                              & "            ,SUBSTBL.JOB_NO            AS    JOB_NO        " & vbNewLine _
                                              & "            ,SUBSTBL.LOT_NO            AS    LOT_NO        " & vbNewLine _
                                              & "            ,SUBSTBL.TAX_KB            AS    TAX_KB        " & vbNewLine _
                                              & "            ,SUM(SUBSTBL.ATUKAI_NB)    AS    SUM_ATUKAI_NB " & vbNewLine _
                                              & "        FROM                                               " & vbNewLine _
                                              & "            $LM_TRN$..G_SEKY_MEISAI_PRT     AS  PRT        " & vbNewLine _
                                              & "        INNER JOIN       $LM_TRN$..G_SEKY_TBL   SUBSTBL    " & vbNewLine _
                                              & "        ON     SUBSTBL.NRS_BR_CD      =   PRT.NRS_BR_CD    " & vbNewLine _
                                              & "        AND    SUBSTBL.GOODS_CD_NRS   =   PRT.GOODS_CD_NRS " & vbNewLine _
                                              & "        AND    SUBSTBL.JOB_NO         =   PRT.JOB_NO       " & vbNewLine _
                                              & "        AND    SUBSTBL.LOT_NO         =   PRT.LOT_NO       " & vbNewLine _
                                              & "        AND    SUBSTBL.SEKY_FLG       =   PRT.SEKY_FLG     " & vbNewLine _
                                              & "        AND    SUBSTBL.TAX_KB         =   PRT.TAX_KB       " & vbNewLine _
                                              & "        AND    SUBSTBL.SYS_DEL_FLG    =   '0'              " & vbNewLine _
                                              & "   LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )  AS  GOD        " & vbNewLine _
                                              & "        ON     GOD.GOODS_CD_NRS       =   PRT.GOODS_CD_NRS " & vbNewLine _
                                              & "        AND    GOD.NRS_BR_CD          =   PRT.NRS_BR_CD    " & vbNewLine _
                                              & "        LEFT JOIN       $LM_MST$..M_CUST    AS  CST        " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD          =   GOD.NRS_BR_CD    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L          =   GOD.CUST_CD_L    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M          =   GOD.CUST_CD_M    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S          =   GOD.CUST_CD_S    " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS         =   GOD.CUST_CD_SS   " & vbNewLine _
                                              & "        WHERE                                              " & vbNewLine _
                                              & "               PRT.NRS_BR_CD          =   @NRS_BR_CD       " & vbNewLine _
                                              & "        AND    PRT.INV_DATE_TO        =   @SKYU_DATE       " & vbNewLine _
                                              & "        AND    PRT.SEKY_FLG           =   '00'             " & vbNewLine _
                                              & "        AND    PRT.SYS_DEL_FLG        =   '0'              " & vbNewLine _
                                              & "        AND    CST.NIYAKU_SEIQTO_CD   =   @SEIQTO_CD       " & vbNewLine _
                                              & "        AND    SUBSTBL.ATUKAI_NB <> 0       " & vbNewLine _
                                              & "        AND    SUBSTBL.NIYAKU_YN      =   '01'             " & vbNewLine _
                                              & "        GROUP    BY                                        " & vbNewLine _
                                              & "             SUBSTBL.NRS_BR_CD                             " & vbNewLine _
                                              & "            ,SUBSTBL.GOODS_CD_NRS                          " & vbNewLine _
                                              & "            ,SUBSTBL.JOB_NO                                " & vbNewLine _
                                              & "            ,SUBSTBL.LOT_NO                                " & vbNewLine _
                                              & "            ,SUBSTBL.TAX_KB                                " & vbNewLine _
                                              & "        )   AS  SUB                                        " & vbNewLine _
                                              & "ON     STBL.NRS_BR_CD       =    SUB.NRS_BR_CD             " & vbNewLine _
                                              & "AND    STBL.GOODS_CD_NRS    =    SUB.GOODS_CD_NRS          " & vbNewLine _
                                              & "AND    STBL.JOB_NO          =    SUB.JOB_NO                " & vbNewLine _
                                              & "AND    STBL.LOT_NO          =    SUB.LOT_NO                " & vbNewLine _
                                              & "AND    STBL.TAX_KB          =    SUB.TAX_KB                " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN1              " & vbNewLine _
                                              & "ON     KBN1.KBN_GROUP_CD    =    'S064'                    " & vbNewLine _
                                              & "AND    KBN1.KBN_NM1         =    '02'                      " & vbNewLine _
                                              & "AND    KBN1.KBN_NM3         =    PRT.TAX_KB                " & vbNewLine _
                                              & "AND    KBN1.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SEIQKMK  AS  KMK               " & vbNewLine _
                                              & "ON     KMK.GROUP_KB         =    '02'                      " & vbNewLine _
                                              & "AND    KMK.SEIQKMK_CD       =    KBN1.KBN_NM2              " & vbNewLine _
                                              & "AND    KMK.SYS_DEL_FLG      =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN2              " & vbNewLine _
                                              & "ON     KBN2.KBN_GROUP_CD    =    'Z001'                    " & vbNewLine _
                                              & "AND    KBN2.KBN_CD          =    PRT.TAX_KB                " & vbNewLine _
                                              & "AND    KBN2.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN3              " & vbNewLine _
                                              & "ON     KBN3.KBN_GROUP_CD    =    'B008'                    " & vbNewLine _
                                              & "AND    KBN3.KBN_NM2         =    STBL.WH_CD                " & vbNewLine _
                                              & "AND    KBN3.KBN_NM3         =    STBL.TOU_NO               " & vbNewLine _
                                              & "AND    KBN3.KBN_NM4         =    STBL.NRS_BR_CD            " & vbNewLine _
                                              & "AND    KBN3.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN4              " & vbNewLine _
                                              & "ON     KBN4.KBN_GROUP_CD    =    'B007'                    " & vbNewLine _
                                              & "AND    KBN4.KBN_NM2         =    @NRS_BR_CD                " & vbNewLine _
                                              & "AND    KBN4.KBN_NM3         =    '1'                       " & vbNewLine _
                                              & "AND    KBN4.SYS_DEL_FLG     =    '0'                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SEIQTO   AS  SQT               " & vbNewLine _
                                              & "ON     SQT.NRS_BR_CD        =    PRT.NRS_BR_CD             " & vbNewLine _
                                              & "AND    SQT.SEIQTO_CD        =    CST.NIYAKU_SEIQTO_CD      " & vbNewLine _
                                              & "WHERE                                                      " & vbNewLine _
                                              & "       PRT.NRS_BR_CD        =    @NRS_BR_CD                " & vbNewLine _
                                              & "AND    PRT.INV_DATE_TO      =    @SKYU_DATE                " & vbNewLine _
                                              & "AND    PRT.SEKY_FLG         =    '00'                      " & vbNewLine _
                                              & "AND    PRT.SYS_DEL_FLG      =    '0'                       " & vbNewLine _
                                              & "AND    CST.NIYAKU_SEIQTO_CD =    @SEIQTO_CD                " & vbNewLine _
                                              & "--(2012.12.06)要望番号1654 荷役料有無コメント -- START --  " & vbNewLine _
                                              & "--AND    STBL.NIYAKU_YN       =    '01'                    " & vbNewLine _
                                              & "--(2012.12.06)要望番号1654 荷役料有無コメント --  END  --  " & vbNewLine _
                                              & "--(2012.12.05)要望番号1645 0でOKとする --- START ---       " & vbNewLine _
                                              & "-- AND   STBL.SUM_ATUKAI_NB_PER_OKIBA <> 0                 " & vbNewLine _
                                              & "--(2012.12.05)要望番号1645 0でOKとする ---  END  ---       " & vbNewLine _
                                              & "--AND    SUB.SUM_ATUKAI_NB <> 0                            " & vbNewLine _
                                              & "ORDER BY                                                   " & vbNewLine _
                                              & "       PRT.GOODS_CD_NRS                                    " & vbNewLine _
                                              & "      ,PRT.LOT_NO                                          " & vbNewLine _
                                              & "      ,PRT.TAX_KB                                          " & vbNewLine _
                                              & "      ,STBL.SUM_ATUKAI_NB_PER_OKIBA                        " & vbNewLine

#End Region

#Region "運賃検索 SQL"

    ''' <summary>
    ''' 運賃確定チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_UNCHIN_KAKUTEI As String = "SELECT                                                                " & vbNewLine _
                                                   & "     ISNULL(SUM(SUB.SELECT_CNT),0)   AS    SELECT_CNT                 " & vbNewLine _
                                                   & "FROM                                                                  " & vbNewLine _
                                                   & "    (                                                                 " & vbNewLine _
                                                   & "    SELECT                                                            " & vbNewLine _
                                                   & "        '01'                     AS    SELECT_KBN                     " & vbNewLine _
                                                   & "        ,COUNT(TRS.UNSO_NO_L)    AS    SELECT_CNT                     " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_L           AS    CUST_CD_L                      " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_M           AS    CUST_CD_M                      " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_S           AS    CUST_CD_S                      " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_SS          AS    CUST_CD_SS                     " & vbNewLine _
                                                   & "    FROM                                                              " & vbNewLine _
                                                   & "        $LM_TRN$..F_UNCHIN_TRS            TRS                         " & vbNewLine _
                                                   & "        LEFT JOIN                                                             " & vbNewLine _
                                                   & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                                   & "             WHERE                                                            " & vbNewLine _
                                                   & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                                   & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                                   & "               AND    UNL.OUTKA_PLAN_DATE          >=    @UNC_SKYU_DATE_FROM  " & vbNewLine _
                                                   & "               AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE           " & vbNewLine _
                                                   & "            ) UNL                                                             " & vbNewLine _
                                                   & "    ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                   & "    AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                   & "    AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L            " & vbNewLine _
                                                   & "    AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M            " & vbNewLine _
                                                   & "    AND    UNL.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                   & "    WHERE                                                             " & vbNewLine _
                                                   & "           TRS.SEIQTO_CD                =    @SEIQTO_CD               " & vbNewLine _
                                                   & "    AND    TRS.SEIQ_TARIFF_BUNRUI_KB    <>   '40'                     " & vbNewLine _
                                                   & "    AND    TRS.SEIQ_FIXED_FLAG          =    '00'                     " & vbNewLine _
                                                   & "    AND    TRS.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                   & "    AND    UNL.OUTKA_PLAN_DATE          >=    @UNC_SKYU_DATE_FROM     " & vbNewLine _
                                                   & "    AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE              " & vbNewLine _
                                                   & "    AND    UNL.NRS_BR_CD                =   @NRS_BR_CD                " & vbNewLine _
                                                   & "    GROUP BY                                                          " & vbNewLine _
                                                   & "        TRS.CUST_CD_L                                                 " & vbNewLine _
                                                   & "       ,TRS.CUST_CD_M                                                 " & vbNewLine _
                                                   & "       ,TRS.CUST_CD_S                                                 " & vbNewLine _
                                                   & "       ,TRS.CUST_CD_SS                                                " & vbNewLine _
                                                   & "    UNION ALL                                                         " & vbNewLine _
                                                   & "    SELECT                                                            " & vbNewLine _
                                                   & "        '02'                     AS    SELECT_KBN                     " & vbNewLine _
                                                   & "        ,COUNT(TRS.UNSO_NO_L)    AS    SELECT_CNT                     " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_L           AS    CUST_CD_L                      " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_M           AS    CUST_CD_M                      " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_S           AS    CUST_CD_S                      " & vbNewLine _
                                                   & "        ,TRS.CUST_CD_SS          AS    CUST_CD_SS                     " & vbNewLine _
                                                   & "    FROM                                                              " & vbNewLine _
                                                   & "        $LM_TRN$..F_UNCHIN_TRS            TRS                         " & vbNewLine _
                                                   & "        LEFT JOIN                                                             " & vbNewLine _
                                                   & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                                   & "             WHERE                                                            " & vbNewLine _
                                                   & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                                   & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                                   & "               AND    UNL.ARR_PLAN_DATE            >=    @UNC_SKYU_DATE_FROM  " & vbNewLine _
                                                   & "               AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE           " & vbNewLine _
                                                   & "            ) UNL                                                             " & vbNewLine _
                                                   & "    ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                   & "    AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                   & "    AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L            " & vbNewLine _
                                                   & "    AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M            " & vbNewLine _
                                                   & "    AND    UNL.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                   & "    WHERE                                                             " & vbNewLine _
                                                   & "           TRS.SEIQTO_CD                =    @SEIQTO_CD               " & vbNewLine _
                                                   & "    AND    TRS.SEIQ_TARIFF_BUNRUI_KB    <>   '40'                     " & vbNewLine _
                                                   & "    AND    TRS.SEIQ_FIXED_FLAG          =    '00'                     " & vbNewLine _
                                                   & "    AND    TRS.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                   & "    AND    UNL.ARR_PLAN_DATE            >=    @UNC_SKYU_DATE_FROM     " & vbNewLine _
                                                   & "    AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE              " & vbNewLine _
                                                   & "    AND    UNL.NRS_BR_CD                =   @NRS_BR_CD                " & vbNewLine _
                                                   & "    GROUP BY                                                          " & vbNewLine _
                                                   & "        TRS.CUST_CD_L                                                 " & vbNewLine _
                                                   & "       ,TRS.CUST_CD_M                                                 " & vbNewLine _
                                                   & "       ,TRS.CUST_CD_S                                                 " & vbNewLine _
                                                   & "       ,TRS.CUST_CD_SS                                                " & vbNewLine _
                                                   & "    )    SUB                                                          " & vbNewLine _
                                                   & "LEFT JOIN       $LM_MST$..M_CUST   CST                                " & vbNewLine _
                                                   & "ON   CST.CUST_CD_L      =   SUB.CUST_CD_L                             " & vbNewLine _
                                                   & "AND  CST.CUST_CD_M      =   SUB.CUST_CD_M                             " & vbNewLine _
                                                   & "AND  CST.CUST_CD_S      =   SUB.CUST_CD_S                             " & vbNewLine _
                                                   & "AND  CST.CUST_CD_SS     =   SUB.CUST_CD_SS                            " & vbNewLine _
                                                   & "AND  CST.NRS_BR_CD      =   @NRS_BR_CD                                " & vbNewLine _
                                                   & "AND  CST.SYS_DEL_FLG    =   '0'                                       " & vbNewLine _
                                                   & "WHERE                                                                 " & vbNewLine _
                                                   & "     SUB.SELECT_KBN    =   CST.UNTIN_CALCULATION_KB                   " & vbNewLine

    ''' <summary>
    ''' 運賃取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_UNCHIN As String = " SET ARITHABORT ON                                                     " & vbNewLine _
                                              & " SET ARITHIGNORE ON                                       " & vbNewLine _
                                              & "    SELECT              " & vbNewLine _
                                              & "     '03'                               AS    GROUP_KB                    " & vbNewLine _
                                              & "    ,KBN1.KBN_NM2                       AS    SEIQKMK_CD                  " & vbNewLine _
                                              & "    ,KMK.SEIQKMK_NM                     AS    SEIQKMK_NM                  " & vbNewLine _
                                              & "    ,KMK.KEIRI_KB                       AS    KEIRI_KB                    " & vbNewLine _
                                              & "    ,MAIN.TAX_KB                        AS    TAX_KB                      " & vbNewLine _
                                              & "    ,KBN2.KBN_NM1                       AS    TAX_KB_NM                   " & vbNewLine _
                                              & "    ,KBN3.KBN_CD                        AS    BUSYO_CD                    " & vbNewLine _
                                              & "    ,SUM(MAIN.DECI_UNCHIN) + SUM(MAIN.DECI_CITY_EXTC) + SUM(MAIN.DECI_WINT_EXTC) + SUM(MAIN.DECI_RELY_EXTC) + SUM(MAIN.DECI_TOLL) + SUM(MAIN.DECI_INSU)    AS KEISAN_TLGK " & vbNewLine _
                                              & "    ,CAST(SEQ.UNCHIN_NR AS DECIMAL(5,2)) AS    NEBIKI_RT                   " & vbNewLine _
                                              & "    ,SEQ.UNCHIN_NG                      AS    NEBIKI_GK                   " & vbNewLine _
                                              & "    ,''                                 AS    TEKIYO                      " & vbNewLine _
                                              & "    ,'00'                               AS    TEMPLATE_IMP_FLG            " & vbNewLine _
                                              & "--ADD 2016/09/07 Start 再保管対応                                         " & vbNewLine _
                                              & "   ,MAIN.WH_CD                          AS    WH_CD                       " & vbNewLine _
                                              & "   ,MAIN.TOU_NO                         AS    TOU_NO                      " & vbNewLine _
                                              & "   ,MAIN.SITU_NO                        AS    SITU_NO                     " & vbNewLine _
                                              & "   ,MAIN.MOTO_DATA_KB                   AS    MOTO_DATA_KB                " & vbNewLine _
                                              & " --  ,CASE WHEN MAIN.MOTO_DATA_KB = '40'                                    " & vbNewLine _
                                              & " --        THEN '02'      -- 元データ区分 運送時は、再保管扱い              " & vbNewLine _
                                              & " --        ELSE CASE WHEN MAIN.TOU_NO > '' AND MAIN.SITU_NO > ''            " & vbNewLine _
                                              & " --                  THEN CASE WHEN ISNULL(MTS.JISYATASYA_KB,'') = '02'     " & vbNewLine _
                                              & " --                            THEN '02'                                    " & vbNewLine _
                                              & "  --                           ELSE '01'                                    " & vbNewLine _
                                              & "  --                      END                                               " & vbNewLine _
                                              & "  --                 ELSE '02'  --再保管                                    " & vbNewLine _
                                              & "  --            END                                                         " & vbNewLine _
                                              & "  --  END AS    JISYATASYA_KB                                               " & vbNewLine _
                                              & "  --20161122 運賃を自社倉庫のみに変更                                               " & vbNewLine _
                                              & "  ,  '01' AS    JISYATASYA_KB                                               " & vbNewLine _
                                              & "--真荷主 start                                                            " & vbNewLine _
                                              & "   ,MAIN.TCUST_BPCD                     AS    TCUST_BPCD                  " & vbNewLine _
                                              & "   ,MAIN.TCUST_BPNM                     AS    TCUST_BPNM                  " & vbNewLine _
                                              & "--真荷主 end                                                              " & vbNewLine _
                                              & "--製品セグメント start                                                    " & vbNewLine _
                                              & "   ,MAIN.PRODUCT_SEG_CD                 AS    PRODUCT_SEG_CD              " & vbNewLine _
                                              & "--製品セグメント end                                                      " & vbNewLine _
                                              & "--地域セグメント start                                                    " & vbNewLine _
                                              & "   ,MAIN.ORIG_SEG_CD                    AS    ORIG_SEG_CD                 " & vbNewLine _
                                              & "   ,MAIN.DEST_SEG_CD                    AS    DEST_SEG_CD                 " & vbNewLine _
                                              & "--地域セグメント end                                                      " & vbNewLine _
                                              & "--運賃科目分け start                                                      " & vbNewLine _
                                              & "   ,MAIN.SEIQKMK_CD_S                   AS    SEIQKMK_CD_S                " & vbNewLine _
                                              & "--運賃科目分け end                                                        " & vbNewLine _
                                              & "--ADD 2016/09/07 End                                                      " & vbNewLine _
                                              & "FROM                                                                      " & vbNewLine _
                                              & "    (                                                                     " & vbNewLine _
                                              & "    SELECT                                                                " & vbNewLine _
                                              & "         SUB.TAX_KB                  AS    TAX_KB                         " & vbNewLine _
                                              & "        ,SUB.DECI_UNCHIN             AS    DECI_UNCHIN                    " & vbNewLine _
                                              & "        ,SUB.DECI_CITY_EXTC          AS    DECI_CITY_EXTC                 " & vbNewLine _
                                              & "        ,SUB.DECI_WINT_EXTC          AS    DECI_WINT_EXTC                 " & vbNewLine _
                                              & "        ,SUB.DECI_RELY_EXTC          AS    DECI_RELY_EXTC                 " & vbNewLine _
                                              & "        ,SUB.DECI_TOLL               AS    DECI_TOLL                      " & vbNewLine _
                                              & "        ,SUB.DECI_INSU               AS    DECI_INSU                      " & vbNewLine _
                                              & "        ,SUB.NRS_BR_CD               AS    NRS_BR_CD                      " & vbNewLine _
                                              & "        ,SUB.SEIQTO_CD               AS    SEIQTO_CD                      " & vbNewLine _
                                              & "--ADD 2016/09/07 Start 再保管対応                                         " & vbNewLine _
                                              & "        ,SUB.INOUTKA_NO_L            AS    INOUTKA_NO_L                                                             " & vbNewLine _
                                              & "        ,SUB.MOTO_DATA_KB            AS    MOTO_DATA_KB                                                             " & vbNewLine _
                                              & "        ,CASE WHEN SUB.MOTO_DATA_KB = '20' THEN COL.WH_CD                                                           " & vbNewLine _
                                              & "                                           ELSE BIL.WH_CD    END WH_CD                                              " & vbNewLine _
                                              & "        ,CASE WHEN SUB.MOTO_DATA_KB = '20' THEN (SELECT top 1 TOU_NO FROM  $LM_TRN$..C_OUTKA_S COS                  " & vbNewLine _
                                              & "                                                                     WHERE                                          " & vbNewLine _
                                              & "                                                                            COS.NRS_BR_CD    = @NRS_BR_CD           " & vbNewLine _
                                              & "                                                                       AND  COS.SYS_DEL_FLG  = '0'                  " & vbNewLine _
                                              & "                                                                       AND  COS.OUTKA_NO_L   =  SUB.INOUTKA_NO_L    " & vbNewLine _
                                              & "                                                        ORDER BY COS.OUTKA_NO_L,COS.OUTKA_NO_M,COS.OUTKA_NO_S)      " & vbNewLine _
                                              & "                                           ELSE (SELECT top 1 TOU_NO FROM  $LM_TRN$..B_INKA_S BIS                   " & vbNewLine _
                                              & "                                                                     WHERE                                          " & vbNewLine _
                                              & "                                                                            BIS.NRS_BR_CD     =  @NRS_BR_CD         " & vbNewLine _
                                              & "                                                                       AND  BIS.SYS_DEL_FLG   =  '0'                " & vbNewLine _
                                              & "                                                                       AND  BIS.INKA_NO_L     =  SUB.INOUTKA_NO_L   " & vbNewLine _
                                              & "                                                        ORDER BY BIS.INKA_NO_L,BIS.INKA_NO_M,BIS.INKA_NO_S)         " & vbNewLine _
                                              & "         END  TOU_NO                                                                                                " & vbNewLine _
                                              & "        ,CASE WHEN SUB.MOTO_DATA_KB = '20' THEN (SELECT top 1 SITU_NO FROM  $LM_TRN$..C_OUTKA_S COS                 " & vbNewLine _
                                              & "                                                                      WHERE                                         " & vbNewLine _
                                              & "                                                                             COS.NRS_BR_CD     =  @NRS_BR_CD        " & vbNewLine _
                                              & "                                                                        AND  COS.SYS_DEL_FLG   =  '0'               " & vbNewLine _
                                              & "                                                                        AND  COS.OUTKA_NO_L    =  SUB.INOUTKA_NO_L  " & vbNewLine _
                                              & "                                                        ORDER BY COS.OUTKA_NO_L,COS.OUTKA_NO_M,COS.OUTKA_NO_S)      " & vbNewLine _
                                              & "                                           ELSE (SELECT top 1 SITU_NO FROM  $LM_TRN$..B_INKA_S BIS                  " & vbNewLine _
                                              & "                                                                      WHERE                                         " & vbNewLine _
                                              & "                                                                             BIS.NRS_BR_CD     =  @NRS_BR_CD        " & vbNewLine _
                                              & "                                                                        AND  BIS.SYS_DEL_FLG   =  '0'               " & vbNewLine _
                                              & "                                                                        AND  BIS.INKA_NO_L     =  SUB.INOUTKA_NO_L  " & vbNewLine _
                                              & "                                                        ORDER BY BIS.INKA_NO_L,BIS.INKA_NO_M,BIS.INKA_NO_S)         " & vbNewLine _
                                              & "         END  SITU_NO                                                                                               " & vbNewLine _
                                              & "--真荷主 start                                                                " & vbNewLine _
                                              & "        ,CST.TCUST_BPCD              AS    TCUST_BPCD                     " & vbNewLine _
                                              & "        ,MBP.BP_NM1                  AS    TCUST_BPNM                     " & vbNewLine _
                                              & "--真荷主 end                                                                  " & vbNewLine _
                                              & "--製品セグメント start                                                        " & vbNewLine _
                                              & "        ,SUB.PRODUCT_SEG_CD          AS    PRODUCT_SEG_CD                     " & vbNewLine _
                                              & "--製品セグメント end                                                          " & vbNewLine _
                                              & "--地域セグメント start                                                        " & vbNewLine _
                                              & "        ,CASE SUB.MOTO_DATA_KB                                                " & vbNewLine _
                                              & "              WHEN '20' THEN CSOH.CNCT_SEG_CD                                 " & vbNewLine _
                                              & "              WHEN '10' THEN CSIH.CNCT_SEG_CD                                 " & vbNewLine _
                                              & "              ELSE           SUB.ORIG_SEG_CD_UNSO                             " & vbNewLine _
                                              & "         END  ORIG_SEG_CD                                                     " & vbNewLine _
                                              & "        ,CASE SUB.MOTO_DATA_KB                                                " & vbNewLine _
                                              & "              WHEN '20' THEN CSOC.CNCT_SEG_CD                                 " & vbNewLine _
                                              & "              WHEN '10' THEN CSIC.CNCT_SEG_CD                                 " & vbNewLine _
                                              & "              ELSE           SUB.DEST_SEG_CD_UNSO                             " & vbNewLine _
                                              & "         END  DEST_SEG_CD                                                     " & vbNewLine _
                                              & "--地域セグメント end                                                          " & vbNewLine _
                                              & "--運賃科目分け start                                                          " & vbNewLine _
                                              & "        ,SUB.SEIQKMK_CD_S            AS    SEIQKMK_CD_S                       " & vbNewLine _
                                              & "--運賃科目分け end                                                            " & vbNewLine _
                                              & "    FROM                                                                      " & vbNewLine _
                                              & "        (                                                                     " & vbNewLine _
                                              & "        SELECT                                                                " & vbNewLine _
                                              & "             '01'                               AS    SELECT_KBN              " & vbNewLine _
                                              & "            ,TRS.TAX_KB                         AS    TAX_KB                  " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_UNCHIN,0)          AS    DECI_UNCHIN             " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_CITY_EXTC,0)       AS    DECI_CITY_EXTC          " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_WINT_EXTC,0)       AS    DECI_WINT_EXTC          " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_RELY_EXTC,0)       AS    DECI_RELY_EXTC          " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_TOLL,0)            AS    DECI_TOLL               " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_INSU,0)            AS    DECI_INSU               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_L                      AS    CUST_CD_L               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_M                      AS    CUST_CD_M               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_S                      AS    CUST_CD_S               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_SS                     AS    CUST_CD_SS              " & vbNewLine _
                                              & "            ,TRS.NRS_BR_CD                      AS    NRS_BR_CD               " & vbNewLine _
                                              & "            ,TRS.SEIQTO_CD                      AS    SEIQTO_CD               " & vbNewLine _
                                              & "            ,ISNULL(UNL.INOUTKA_NO_L,'')        AS    INOUTKA_NO_L   -- ADD 2016/09/07 " & vbNewLine _
                                              & "            ,ISNULL(UNL.MOTO_DATA_KB,'')        AS    MOTO_DATA_KB   -- ADD 2016/09/07 " & vbNewLine _
                                              & "            ,ISNULL(UNL.ORIG_CD,'')             AS    UNL_ORIG_CD             " & vbNewLine _
                                              & "--製品セグメント start                                                        " & vbNewLine _
                                              & "            ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''                       " & vbNewLine _
                                              & "                  THEN SSG2.CNCT_SEG_CD                                       " & vbNewLine _
                                              & "                  ELSE SSG1.CNCT_SEG_CD                                       " & vbNewLine _
                                              & "                  END AS PRODUCT_SEG_CD                                       " & vbNewLine _
                                              & "--製品セグメント end                                                          " & vbNewLine _
                                              & "--地域セグメント(運送/発地) start                                             " & vbNewLine _
                                              & "            ,CSH.CNCT_SEG_CD                    AS    ORIG_SEG_CD_UNSO        " & vbNewLine _
                                              & "--地域セグメント(運送/発地) end                                               " & vbNewLine _
                                              & "--地域セグメント(運送/着地) start                                             " & vbNewLine _
                                              & "            ,CSC.CNCT_SEG_CD                    AS    DEST_SEG_CD_UNSO        " & vbNewLine _
                                              & "--地域セグメント(運送/着地) end                                               " & vbNewLine _
                                              & "--運賃科目分け start                                                          " & vbNewLine _
                                              & "            ,CASE ISNULL(MUN.UNSOCO_KB,'')                                    " & vbNewLine _
                                              & "                  WHEN '02' THEN ''                                           " & vbNewLine _
                                              & "                  ELSE           '1'                                          " & vbNewLine _
                                              & "                  END AS SEIQKMK_CD_S                                         " & vbNewLine _
                                              & "--運賃科目分け end                                                            " & vbNewLine _
                                              & "        FROM                                                                  " & vbNewLine _
                                              & "            $LM_TRN$..F_UNCHIN_TRS             TRS                            " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                              & "             WHERE                                                            " & vbNewLine _
                                              & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                              & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                              & "               AND    UNL.OUTKA_PLAN_DATE          >=    @UNC_SKYU_DATE_FROM  " & vbNewLine _
                                              & "               AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE           " & vbNewLine _
                                              & "            ) UNL                                                             " & vbNewLine _
                                              & "        ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L                " & vbNewLine _
                                              & "        AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L                " & vbNewLine _
                                              & "        AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M                " & vbNewLine _
                                              & "        AND    UNL.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "--運賃科目分け start                                                          " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_UNSOCO MUN                                            " & vbNewLine _
                                              & "        ON     MUN.NRS_BR_CD                =    UNL.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    MUN.UNSOCO_CD                =    UNL.UNSO_CD                  " & vbNewLine _
                                              & "        AND    MUN.UNSOCO_BR_CD             =    UNL.UNSO_BR_CD               " & vbNewLine _
                                              & "        AND    MUN.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "--運賃科目分け end                                                            " & vbNewLine _
                                              & "--製品セグメント取得① start                                                  " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_TRN$..F_UNSO_M UNM                                            " & vbNewLine _
                                              & "        ON     UNM.NRS_BR_CD                =    TRS.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    UNM.UNSO_NO_L                =    TRS.UNSO_NO_L                " & vbNewLine _
                                              & "        AND    UNM.UNSO_NO_M                =    TRS.UNSO_NO_M                " & vbNewLine _
                                              & "        AND    UNM.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_GOODS GOD                                             " & vbNewLine _
                                              & "        ON     GOD.NRS_BR_CD                =    UNM.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    GOD.GOODS_CD_NRS             =    UNM.GOODS_CD_NRS             " & vbNewLine _
                                              & "        AND    GOD.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            (                                                                 " & vbNewLine _
                                              & "              SELECT *                                                        " & vbNewLine _
                                              & "              FROM $LM_MST$..M_TANKA A                                        " & vbNewLine _
                                              & "              WHERE                                                           " & vbNewLine _
                                              & "                A.STR_DATE <= @SKYU_DATE                                      " & vbNewLine _
                                              & "                AND A.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                              & "                AND NOT EXISTS (                                              " & vbNewLine _
                                              & "                  SELECT 1                                                    " & vbNewLine _
                                              & "                  FROM                                                        " & vbNewLine _
                                              & "                    $LM_MST$..M_TANKA B                                       " & vbNewLine _
                                              & "                  WHERE                                                       " & vbNewLine _
                                              & "                    B.NRS_BR_CD = A.NRS_BR_CD                                 " & vbNewLine _
                                              & "                    AND B.CUST_CD_L = A.CUST_CD_L                             " & vbNewLine _
                                              & "                    AND B.CUST_CD_M = A.CUST_CD_M                             " & vbNewLine _
                                              & "                    AND B.UP_GP_CD_1 = A.UP_GP_CD_1                           " & vbNewLine _
                                              & "                    AND B.STR_DATE <= @SKYU_DATE                              " & vbNewLine _
                                              & "                    AND B.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                              & "                    AND B.STR_DATE > A.STR_DATE                               " & vbNewLine _
                                              & "                  )                                                           " & vbNewLine _
                                              & "            ) AS TNK                                                          " & vbNewLine _
                                              & "        ON     TNK.NRS_BR_CD       =   GOD.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    TNK.CUST_CD_L       =   GOD.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    TNK.CUST_CD_M       =   GOD.CUST_CD_M                          " & vbNewLine _
                                              & "        AND    TNK.UP_GP_CD_1      =   GOD.UP_GP_CD_1                         " & vbNewLine _
                                              & "        AND    TNK.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..M_SEGMENT SSG1                                            " & vbNewLine _
                                              & "        ON     SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD                    " & vbNewLine _
                                              & "        AND    SSG1.DATA_TYPE_CD    =   '00002'                               " & vbNewLine _
                                              & "        AND    SSG1.KBN_LANG        =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    SSG1.SYS_DEL_FLG     =   '0'                                   " & vbNewLine _
                                              & "--製品セグメント取得① end                                                    " & vbNewLine _
                                              & "--製品セグメント取得② start                                                  " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_CUST CST                                              " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD       =   TRS.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L       =   TRS.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M       =   TRS.CUST_CD_M                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S       =   TRS.CUST_CD_S                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS      =   TRS.CUST_CD_SS                         " & vbNewLine _
                                              & "        AND    CST.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..M_SEGMENT SSG2                                            " & vbNewLine _
                                              & "        ON     SSG2.CNCT_SEG_CD    =   CST.PRODUCT_SEG_CD                     " & vbNewLine _
                                              & "        AND    SSG2.DATA_TYPE_CD   =   '00002'                                " & vbNewLine _
                                              & "        AND    SSG2.KBN_LANG       =   @KBN_LANG                              " & vbNewLine _
                                              & "        AND    SSG2.SYS_DEL_FLG    =   '0'                                    " & vbNewLine _
                                              & "--製品セグメント取得② end                                                    " & vbNewLine _
                                              & "--地域セグメント取得(運送/発地) start                                         " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_DEST DTH                                              " & vbNewLine _
                                              & "        ON     DTH.NRS_BR_CD       =   UNL.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    DTH.CUST_CD_L       =   UNL.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    DTH.DEST_CD         =   UNL.ORIG_CD                            " & vbNewLine _
                                              & "        AND    DTH.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_ZIP ZPH                                               " & vbNewLine _
                                              & "        ON     ZPH.ZIP_NO          =   DTH.ZIP                                " & vbNewLine _
                                              & "        AND    ZPH.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_SOKO SKH                                              " & vbNewLine _
                                              & "        ON     SKH.WH_CD            =   CST.DEFAULT_SOKO_CD                   " & vbNewLine _
                                              & "        AND    SKH.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..Z_KBN AKH                                                 " & vbNewLine _
                                              & "        ON     AKH.KBN_GROUP_CD     =   '" & ABM_DB_TODOFUKEN & "'                              " & vbNewLine _
                                              & "        AND    AKH.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    AKH.KBN_NM3          =   CASE WHEN RTRIM(LEFT(ISNULL(ZPH.JIS_CD,''),2)) = '' THEN LEFT(SKH.JIS_CD,2) ELSE LEFT(ZPH.JIS_CD,2) END " & vbNewLine _
                                              & "        AND    AKH.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "             ABM_DB..M_SEGMENT CSH                                            " & vbNewLine _
                                              & "        ON     CSH.DATA_TYPE_CD     =   '00001'                               " & vbNewLine _
                                              & "        AND    CSH.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    CSH.KBN_GROUP_CD     =   AKH.KBN_GRP_REF1                      " & vbNewLine _
                                              & "        AND    CSH.KBN_CD           =   AKH.KBN_CD_REF1                       " & vbNewLine _
                                              & "        AND    CSH.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "--地域セグメント取得(運送/発地) end                                           " & vbNewLine _
                                              & "--地域セグメント取得(運送/着地) start                                         " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_DEST DTC                                              " & vbNewLine _
                                              & "        ON     DTC.NRS_BR_CD       =   UNL.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    DTC.CUST_CD_L       =   UNL.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    DTC.DEST_CD         =   UNL.DEST_CD                            " & vbNewLine _
                                              & "        AND    DTC.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_ZIP ZPC                                               " & vbNewLine _
                                              & "        ON     ZPC.ZIP_NO          =   DTC.ZIP                                " & vbNewLine _
                                              & "        AND    ZPC.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..Z_KBN AKC                                                 " & vbNewLine _
                                              & "        ON     AKC.KBN_GROUP_CD     =   '" & ABM_DB_TODOFUKEN & "'                              " & vbNewLine _
                                              & "        AND    AKC.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    AKC.KBN_NM3          =   LEFT(ZPC.JIS_CD,2)                    " & vbNewLine _
                                              & "        AND    AKC.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "             ABM_DB..M_SEGMENT CSC                                            " & vbNewLine _
                                              & "        ON     CSC.DATA_TYPE_CD     =   '00001'                               " & vbNewLine _
                                              & "        AND    CSC.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    CSC.KBN_GROUP_CD     =   AKC.KBN_GRP_REF1                      " & vbNewLine _
                                              & "        AND    CSC.KBN_CD           =   AKC.KBN_CD_REF1                       " & vbNewLine _
                                              & "        AND    CSC.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "--地域セグメント取得(運送/着地) end                                           " & vbNewLine _
                                              & "        WHERE                                                                 " & vbNewLine _
                                              & "               TRS.SEIQ_TARIFF_BUNRUI_KB    <>   '40'                         " & vbNewLine _
                                              & "        AND    TRS.SEIQ_FIXED_FLAG          =    '01'                         " & vbNewLine _
                                              & "        AND    TRS.NRS_BR_CD                =    @NRS_BR_CD                   " & vbNewLine _
                                              & "        AND    TRS.SEIQTO_CD                =    @SEIQTO_CD                   " & vbNewLine _
                                              & "        AND    TRS.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "        AND    UNL.OUTKA_PLAN_DATE          >=    @UNC_SKYU_DATE_FROM         " & vbNewLine _
                                              & "        AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE                  " & vbNewLine _
                                              & "        UNION ALL                                                             " & vbNewLine _
                                              & "        SELECT                                                                " & vbNewLine _
                                              & "             '02'                               AS    SELECT_KBN              " & vbNewLine _
                                              & "            ,TRS.TAX_KB                         AS    TAX_KB                  " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_UNCHIN,0)          AS    DECI_UNCHIN             " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_CITY_EXTC,0)       AS    DECI_CITY_EXTC          " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_WINT_EXTC,0)       AS    DECI_WINT_EXTC          " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_RELY_EXTC,0)       AS    DECI_RELY_EXTC          " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_TOLL,0)            AS    DECI_TOLL               " & vbNewLine _
                                              & "            ,ISNULL(TRS.DECI_INSU,0)            AS    DECI_INSU               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_L                      AS    CUST_CD_L               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_M                      AS    CUST_CD_M               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_S                      AS    CUST_CD_S               " & vbNewLine _
                                              & "            ,TRS.CUST_CD_SS                     AS    CUST_CD_SS              " & vbNewLine _
                                              & "            ,TRS.NRS_BR_CD                      AS    NRS_BR_CD               " & vbNewLine _
                                              & "            ,TRS.SEIQTO_CD                      AS    SEIQTO_CD               " & vbNewLine _
                                              & "            ,ISNULL(UNL.INOUTKA_NO_L,'')        AS    INOUTKA_NO_L   -- ADD 2016/09/07 " & vbNewLine _
                                              & "            ,ISNULL(UNL.MOTO_DATA_KB,'')        AS    MOTO_DATA_KB   -- ADD 2016/09/07 " & vbNewLine _
                                              & "            ,ISNULL(UNL.ORIG_CD,'')             AS    UNL_ORIG_CD             " & vbNewLine _
                                              & "--製品セグメント start                                                        " & vbNewLine _
                                              & "            ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''                       " & vbNewLine _
                                              & "                  THEN SSG2.CNCT_SEG_CD                                       " & vbNewLine _
                                              & "                  ELSE SSG1.CNCT_SEG_CD                                       " & vbNewLine _
                                              & "                  END AS PRODUCT_SEG_CD                                       " & vbNewLine _
                                              & "--製品セグメント end                                                          " & vbNewLine _
                                              & "--地域セグメント(運送/発地) start                                             " & vbNewLine _
                                              & "            ,CSH.CNCT_SEG_CD                    AS    ORIG_SEG_CD_UNSO        " & vbNewLine _
                                              & "--地域セグメント(運送/発地) end                                               " & vbNewLine _
                                              & "--地域セグメント(運送/着地) start                                             " & vbNewLine _
                                              & "            ,CSC.CNCT_SEG_CD                    AS    DEST_SEG_CD_UNSO        " & vbNewLine _
                                              & "--地域セグメント(運送/着地) end                                               " & vbNewLine _
                                              & "--運賃科目分け start                                                          " & vbNewLine _
                                              & "            ,CASE ISNULL(MUN.UNSOCO_KB,'')                                    " & vbNewLine _
                                              & "                  WHEN '02' THEN ''                                           " & vbNewLine _
                                              & "                  ELSE           '1'                                          " & vbNewLine _
                                              & "                  END AS SEIQKMK_CD_S                                         " & vbNewLine _
                                              & "--運賃科目分け end                                                            " & vbNewLine _
                                              & "        FROM                                                                  " & vbNewLine _
                                              & "            $LM_TRN$..F_UNCHIN_TRS             TRS                            " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                              & "             WHERE                                                            " & vbNewLine _
                                              & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                              & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                              & "               AND    UNL.ARR_PLAN_DATE            >=    @UNC_SKYU_DATE_FROM  " & vbNewLine _
                                              & "               AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE           " & vbNewLine _
                                              & "            ) UNL                                                             " & vbNewLine _
                                              & "        ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L                " & vbNewLine _
                                              & "        AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L                " & vbNewLine _
                                              & "        AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M                " & vbNewLine _
                                              & "        AND    UNL.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "--運賃科目分け start                                                          " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_UNSOCO MUN                                            " & vbNewLine _
                                              & "        ON     MUN.NRS_BR_CD                =    UNL.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    MUN.UNSOCO_CD                =    UNL.UNSO_CD                  " & vbNewLine _
                                              & "        AND    MUN.UNSOCO_BR_CD             =    UNL.UNSO_BR_CD               " & vbNewLine _
                                              & "        AND    MUN.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "--運賃科目分け end                                                            " & vbNewLine _
                                              & "--製品セグメント取得① start                                                  " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_TRN$..F_UNSO_M UNM                                            " & vbNewLine _
                                              & "        ON     UNM.NRS_BR_CD                =    TRS.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    UNM.UNSO_NO_L                =    TRS.UNSO_NO_L                " & vbNewLine _
                                              & "        AND    UNM.UNSO_NO_M                =    TRS.UNSO_NO_M                " & vbNewLine _
                                              & "        AND    UNM.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_GOODS GOD                                             " & vbNewLine _
                                              & "        ON     GOD.NRS_BR_CD                =    UNM.NRS_BR_CD                " & vbNewLine _
                                              & "        AND    GOD.GOODS_CD_NRS             =    UNM.GOODS_CD_NRS             " & vbNewLine _
                                              & "        AND    GOD.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            (                                                                 " & vbNewLine _
                                              & "              SELECT *                                                        " & vbNewLine _
                                              & "              FROM $LM_MST$..M_TANKA A                                        " & vbNewLine _
                                              & "              WHERE                                                           " & vbNewLine _
                                              & "                A.STR_DATE <= @SKYU_DATE                                      " & vbNewLine _
                                              & "                AND A.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                              & "                AND NOT EXISTS (                                              " & vbNewLine _
                                              & "                  SELECT 1                                                    " & vbNewLine _
                                              & "                  FROM                                                        " & vbNewLine _
                                              & "                    $LM_MST$..M_TANKA B                                       " & vbNewLine _
                                              & "                  WHERE                                                       " & vbNewLine _
                                              & "                    B.NRS_BR_CD = A.NRS_BR_CD                                 " & vbNewLine _
                                              & "                    AND B.CUST_CD_L = A.CUST_CD_L                             " & vbNewLine _
                                              & "                    AND B.CUST_CD_M = A.CUST_CD_M                             " & vbNewLine _
                                              & "                    AND B.UP_GP_CD_1 = A.UP_GP_CD_1                           " & vbNewLine _
                                              & "                    AND B.STR_DATE <= @SKYU_DATE                              " & vbNewLine _
                                              & "                    AND B.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                              & "                    AND B.STR_DATE > A.STR_DATE                               " & vbNewLine _
                                              & "                  )                                                           " & vbNewLine _
                                              & "            ) AS TNK                                                          " & vbNewLine _
                                              & "        ON     TNK.NRS_BR_CD       =   GOD.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    TNK.CUST_CD_L       =   GOD.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    TNK.CUST_CD_M       =   GOD.CUST_CD_M                          " & vbNewLine _
                                              & "        AND    TNK.UP_GP_CD_1      =   GOD.UP_GP_CD_1                         " & vbNewLine _
                                              & "        AND    TNK.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..M_SEGMENT SSG1                                            " & vbNewLine _
                                              & "        ON     SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD                    " & vbNewLine _
                                              & "        AND    SSG1.DATA_TYPE_CD    =   '00002'                               " & vbNewLine _
                                              & "        AND    SSG1.KBN_LANG        =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    SSG1.SYS_DEL_FLG     =   '0'                                   " & vbNewLine _
                                              & "--製品セグメント取得① end                                                    " & vbNewLine _
                                              & "--製品セグメント取得② start                                                  " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_CUST CST                                              " & vbNewLine _
                                              & "        ON     CST.NRS_BR_CD       =   TRS.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_L       =   TRS.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_M       =   TRS.CUST_CD_M                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_S       =   TRS.CUST_CD_S                          " & vbNewLine _
                                              & "        AND    CST.CUST_CD_SS      =   TRS.CUST_CD_SS                         " & vbNewLine _
                                              & "        AND    CST.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..M_SEGMENT SSG2                                            " & vbNewLine _
                                              & "        ON     SSG2.CNCT_SEG_CD     =   CST.PRODUCT_SEG_CD                    " & vbNewLine _
                                              & "        AND    SSG2.DATA_TYPE_CD    =   '00002'                               " & vbNewLine _
                                              & "        AND    SSG2.KBN_LANG        =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    SSG2.SYS_DEL_FLG     =   '0'                                   " & vbNewLine _
                                              & "--製品セグメント取得② end                                                    " & vbNewLine _
                                              & "--地域セグメント取得(運送/発地) start                                         " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_DEST DTH                                              " & vbNewLine _
                                              & "        ON     DTH.NRS_BR_CD       =   UNL.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    DTH.CUST_CD_L       =   UNL.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    DTH.DEST_CD         =   UNL.ORIG_CD                            " & vbNewLine _
                                              & "        AND    DTH.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_ZIP ZPH                                               " & vbNewLine _
                                              & "        ON     ZPH.ZIP_NO          =   DTH.ZIP                                " & vbNewLine _
                                              & "        AND    ZPH.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_SOKO SKH                                              " & vbNewLine _
                                              & "        ON     SKH.WH_CD            =   CST.DEFAULT_SOKO_CD                   " & vbNewLine _
                                              & "        AND    SKH.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..Z_KBN AKH                                                 " & vbNewLine _
                                              & "        ON     AKH.KBN_GROUP_CD     =   '" & ABM_DB_TODOFUKEN & "'                              " & vbNewLine _
                                              & "        AND    AKH.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    AKH.KBN_NM3          =   CASE WHEN RTRIM(LEFT(ISNULL(ZPH.JIS_CD,''),2)) = '' THEN LEFT(SKH.JIS_CD,2) ELSE LEFT(ZPH.JIS_CD,2) END " & vbNewLine _
                                              & "        AND    AKH.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "             ABM_DB..M_SEGMENT CSH                                            " & vbNewLine _
                                              & "        ON     CSH.DATA_TYPE_CD     =   '00001'                               " & vbNewLine _
                                              & "        AND    CSH.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    CSH.KBN_GROUP_CD     =   AKH.KBN_GRP_REF1                      " & vbNewLine _
                                              & "        AND    CSH.KBN_CD           =   AKH.KBN_CD_REF1                       " & vbNewLine _
                                              & "        AND    CSH.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "--地域セグメント取得(運送/発地) end                                           " & vbNewLine _
                                              & "--地域セグメント取得(運送/着地) start                                         " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_DEST DTC                                              " & vbNewLine _
                                              & "        ON     DTC.NRS_BR_CD       =   UNL.NRS_BR_CD                          " & vbNewLine _
                                              & "        AND    DTC.CUST_CD_L       =   UNL.CUST_CD_L                          " & vbNewLine _
                                              & "        AND    DTC.DEST_CD         =   UNL.DEST_CD                            " & vbNewLine _
                                              & "        AND    DTC.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            $LM_MST$..M_ZIP ZPC                                               " & vbNewLine _
                                              & "        ON     ZPC.ZIP_NO          =   DTC.ZIP                                " & vbNewLine _
                                              & "        AND    ZPC.SYS_DEL_FLG     =   '0'                                    " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "            ABM_DB..Z_KBN AKC                                                 " & vbNewLine _
                                              & "        ON     AKC.KBN_GROUP_CD     =   '" & ABM_DB_TODOFUKEN & "'                              " & vbNewLine _
                                              & "        AND    AKC.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    AKC.KBN_NM3          =   LEFT(ZPC.JIS_CD,2)                    " & vbNewLine _
                                              & "        AND    AKC.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "        LEFT JOIN                                                             " & vbNewLine _
                                              & "             ABM_DB..M_SEGMENT CSC                                            " & vbNewLine _
                                              & "        ON     CSC.DATA_TYPE_CD     =   '00001'                               " & vbNewLine _
                                              & "        AND    CSC.KBN_LANG         =   @KBN_LANG                             " & vbNewLine _
                                              & "        AND    CSC.KBN_GROUP_CD     =   AKC.KBN_GRP_REF1                      " & vbNewLine _
                                              & "        AND    CSC.KBN_CD           =   AKC.KBN_CD_REF1                       " & vbNewLine _
                                              & "        AND    CSC.SYS_DEL_FLG      =   '0'                                   " & vbNewLine _
                                              & "--地域セグメント取得(運送/着地) end                                           " & vbNewLine _
                                              & "        WHERE                                                                 " & vbNewLine _
                                              & "               TRS.SEIQ_TARIFF_BUNRUI_KB    <>   '40'                         " & vbNewLine _
                                              & "        AND    TRS.SEIQ_FIXED_FLAG          =    '01'                         " & vbNewLine _
                                              & "        AND    TRS.NRS_BR_CD                =    @NRS_BR_CD                   " & vbNewLine _
                                              & "        AND    TRS.SEIQTO_CD                =    @SEIQTO_CD                   " & vbNewLine _
                                              & "        AND    TRS.SYS_DEL_FLG              =    '0'                          " & vbNewLine _
                                              & "        AND    UNL.ARR_PLAN_DATE            >=    @UNC_SKYU_DATE_FROM         " & vbNewLine _
                                              & "        AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE                  " & vbNewLine _
                                              & "        )    SUB                                                              " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_CUST   CST                                    " & vbNewLine _
                                              & "ON     CST.NRS_BR_CD       =    SUB.NRS_BR_CD                             " & vbNewLine _
                                              & "AND    CST.CUST_CD_L       =    SUB.CUST_CD_L                             " & vbNewLine _
                                              & "AND    CST.CUST_CD_M       =    SUB.CUST_CD_M                             " & vbNewLine _
                                              & "AND    CST.CUST_CD_S       =    SUB.CUST_CD_S                             " & vbNewLine _
                                              & "AND    CST.CUST_CD_SS      =    SUB.CUST_CD_SS                            " & vbNewLine _
                                              & "AND    CST.NRS_BR_CD       =    @NRS_BR_CD                                " & vbNewLine _
                                              & "AND    CST.SYS_DEL_FLG     =    '0'                                       " & vbNewLine _
                                              & "--ADD 2016/09/07 Start 再保管対応                                         " & vbNewLine _
                                              & "LEFT JOIN   $LM_TRN$..C_OUTKA_L COL                                       " & vbNewLine _
                                              & "      ON    COL.NRS_BR_CD      =    @NRS_BR_CD                            " & vbNewLine _
                                              & "      AND   COL.OUTKA_NO_L     =    SUB.INOUTKA_NO_L                      " & vbNewLine _
                                              & "LEFT JOIN   $LM_TRN$..B_INKA_L  BIL                                       " & vbNewLine _
                                              & "      ON    BIL.NRS_BR_CD     =    @NRS_BR_CD                             " & vbNewLine _
                                              & "      AND   BIL.INKA_NO_L     =    SUB.INOUTKA_NO_L                       " & vbNewLine _
                                              & "--真荷主取得 start                                                        " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..M_BP AS  MBP                                          " & vbNewLine _
                                              & "      ON    MBP.BP_CD         =    CST.TCUST_BPCD                         " & vbNewLine _
                                              & "      AND   MBP.SYS_DEL_FLG   =    '0'                                    " & vbNewLine _
                                              & "--真荷主取得 end                                                          " & vbNewLine _
                                              & "--地域セグメント取得(出荷/発地) start                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_SOKO SKOH1                                        " & vbNewLine _
                                              & "      ON    SKOH1.WH_CD        =    COL.WH_CD                             " & vbNewLine _
                                              & "      AND   SKOH1.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_SOKO SKOH2                                        " & vbNewLine _
                                              & "      ON    SKOH2.WH_CD        =    CST.DEFAULT_SOKO_CD                   " & vbNewLine _
                                              & "      AND   SKOH2.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..Z_KBN AKOH                                            " & vbNewLine _
                                              & "      ON    AKOH.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                              & "      AND   AKOH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   AKOH.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(SKOH1.JIS_CD,''),2)) = '' THEN LEFT(SKOH2.JIS_CD,2) ELSE LEFT(SKOH1.JIS_CD,2) END " & vbNewLine _
                                              & "      AND   AKOH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..M_SEGMENT CSOH                                        " & vbNewLine _
                                              & "      ON    CSOH.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                              & "      AND   CSOH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   CSOH.KBN_GROUP_CD    =   AKOH.KBN_GRP_REF1                    " & vbNewLine _
                                              & "      AND   CSOH.KBN_CD          =   AKOH.KBN_CD_REF1                     " & vbNewLine _
                                              & "      AND   CSOH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "--地域セグメント取得(出荷/発地) end                                       " & vbNewLine _
                                              & "--地域セグメント取得(出荷/着地) start                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_DEST DTOC                                         " & vbNewLine _
                                              & "      ON    DTOC.NRS_BR_CD    =    COL.NRS_BR_CD                          " & vbNewLine _
                                              & "      AND   DTOC.CUST_CD_L    =    COL.CUST_CD_L                          " & vbNewLine _
                                              & "      AND   DTOC.DEST_CD      =    COL.DEST_CD                            " & vbNewLine _
                                              & "      AND   DTOC.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_ZIP ZPOC                                          " & vbNewLine _
                                              & "      ON    ZPOC.ZIP_NO       =    DTOC.ZIP                               " & vbNewLine _
                                              & "      AND   ZPOC.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_SOKO SKOC                                         " & vbNewLine _
                                              & "      ON    SKOC.WH_CD        =    CST.DEFAULT_SOKO_CD                    " & vbNewLine _
                                              & "      AND   SKOC.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..Z_KBN AKOC                                            " & vbNewLine _
                                              & "      ON    AKOC.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                              & "      AND   AKOC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   AKOC.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(ZPOC.JIS_CD,''),2)) = '' THEN LEFT(SKOC.JIS_CD,2) ELSE LEFT(ZPOC.JIS_CD,2) END " & vbNewLine _
                                              & "      AND   AKOC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..M_SEGMENT CSOC                                        " & vbNewLine _
                                              & "      ON    CSOC.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                              & "      AND   CSOC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   CSOC.KBN_GROUP_CD    =   AKOC.KBN_GRP_REF1                    " & vbNewLine _
                                              & "      AND   CSOC.KBN_CD          =   AKOC.KBN_CD_REF1                     " & vbNewLine _
                                              & "      AND   CSOC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "--地域セグメント取得(出荷/着地) end                                       " & vbNewLine _
                                              & "--地域セグメント取得(入荷/発地) start                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_DEST DTIH                                         " & vbNewLine _
                                              & "      ON    DTIH.NRS_BR_CD    =    CST.NRS_BR_CD                          " & vbNewLine _
                                              & "      AND   DTIH.CUST_CD_L    =    CST.CUST_CD_L                          " & vbNewLine _
                                              & "      AND   DTIH.DEST_CD      =    SUB.UNL_ORIG_CD                        " & vbNewLine _
                                              & "      AND   DTIH.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_ZIP ZPIH                                          " & vbNewLine _
                                              & "      ON    ZPIH.ZIP_NO       =    DTIH.ZIP                               " & vbNewLine _
                                              & "      AND   ZPIH.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_SOKO SKIH                                         " & vbNewLine _
                                              & "      ON    SKIH.WH_CD        =    CST.DEFAULT_SOKO_CD                    " & vbNewLine _
                                              & "      AND   SKIH.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..Z_KBN AKIH                                            " & vbNewLine _
                                              & "      ON    AKIH.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                              & "      AND   AKIH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   AKIH.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(ZPIH.JIS_CD,''),2)) = '' THEN LEFT(SKIH.JIS_CD,2) ELSE LEFT(ZPIH.JIS_CD,2) END " & vbNewLine _
                                              & "      AND   AKIH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..M_SEGMENT CSIH                                        " & vbNewLine _
                                              & "      ON    CSIH.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                              & "      AND   CSIH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   CSIH.KBN_GROUP_CD    =   AKIH.KBN_GRP_REF1                    " & vbNewLine _
                                              & "      AND   CSIH.KBN_CD          =   AKIH.KBN_CD_REF1                     " & vbNewLine _
                                              & "      AND   CSIH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "--地域セグメント取得(入荷/発地) end                                       " & vbNewLine _
                                              & "--地域セグメント取得(入荷/着地) start                                     " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_SOKO SKIC1                                        " & vbNewLine _
                                              & "      ON    SKIC1.WH_CD        =    BIL.WH_CD                             " & vbNewLine _
                                              & "      AND   SKIC1.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                              & "LEFT JOIN   $LM_MST$..M_SOKO SKIC2                                        " & vbNewLine _
                                              & "      ON    SKIC2.WH_CD        =    CST.DEFAULT_SOKO_CD                   " & vbNewLine _
                                              & "      AND   SKIC2.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..Z_KBN AKIC                                            " & vbNewLine _
                                              & "      ON    AKIC.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                              & "      AND   AKIC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   AKIC.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(SKIC1.JIS_CD,''),2)) = '' THEN LEFT(SKIC2.JIS_CD,2) ELSE LEFT(SKIC1.JIS_CD,2) END " & vbNewLine _
                                              & "      AND   AKIC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "LEFT JOIN   ABM_DB..M_SEGMENT CSIC                                        " & vbNewLine _
                                              & "      ON    CSIC.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                              & "      AND   CSIC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                              & "      AND   CSIC.KBN_GROUP_CD    =   AKIC.KBN_GRP_REF1                    " & vbNewLine _
                                              & "      AND   CSIC.KBN_CD          =   AKIC.KBN_CD_REF1                     " & vbNewLine _
                                              & "      AND   CSIC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                              & "--地域セグメント取得(入荷/着地) end                                       " & vbNewLine _
                                              & "--ADD 2016/09/07 End                                                      " & vbNewLine _
                                              & "WHERE                                                                     " & vbNewLine _
                                              & "    SUB.SELECT_KBN    =   CST.UNTIN_CALCULATION_KB                        " & vbNewLine _
                                              & ") MAIN                                                                    " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN   KBN1                                    " & vbNewLine _
                                              & "ON     KBN1.KBN_GROUP_CD   =   'S064'                                     " & vbNewLine _
                                              & "AND    KBN1.KBN_NM1        =    '03'                                      " & vbNewLine _
                                              & "AND    KBN1.KBN_NM3        =    MAIN.TAX_KB                               " & vbNewLine _
                                              & "AND    KBN1.SYS_DEL_FLG    =    '0'                                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SEIQKMK       KMK                             " & vbNewLine _
                                              & "ON     KMK.GROUP_KB        =    '03'                                      " & vbNewLine _
                                              & "AND    KMK.SEIQKMK_CD      =    KBN1.KBN_NM2                              " & vbNewLine _
                                              & "--運賃科目分け start                                                      " & vbNewLine _
                                              & "AND    KMK.SEIQKMK_CD_S    =    MAIN.SEIQKMK_CD_S                         " & vbNewLine _
                                              & "--運賃科目分け end                                                        " & vbNewLine _
                                              & "AND    KMK.SYS_DEL_FLG     =    '0'                                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN   KBN2                                    " & vbNewLine _
                                              & "ON     KBN2.KBN_GROUP_CD   =   'Z001'                                     " & vbNewLine _
                                              & "AND    KBN2.KBN_CD         =    MAIN.TAX_KB                               " & vbNewLine _
                                              & "AND    KBN2.SYS_DEL_FLG    =    '0'                                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..Z_KBN   KBN3                                    " & vbNewLine _
                                              & "ON     KBN3.KBN_GROUP_CD   =    'B007'                                    " & vbNewLine _
                                              & "AND    KBN3.KBN_NM2        =    @NRS_BR_CD                                " & vbNewLine _
                                              & "AND    KBN3.KBN_CD         =    @BUSYO_CD                                 " & vbNewLine _
                                              & "AND    KBN3.SYS_DEL_FLG    =    '0'                                       " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_SEIQTO  SEQ                                   " & vbNewLine _
                                              & "ON     SEQ.NRS_BR_CD       =    MAIN.NRS_BR_CD                            " & vbNewLine _
                                              & "AND    SEQ.SEIQTO_CD       =    MAIN.SEIQTO_CD                            " & vbNewLine _
                                              & "--ADD 2016/09/07 Start 再保管対応                                         " & vbNewLine _
                                              & "LEFT JOIN       $LM_MST$..M_TOU_SITU MTS                                  " & vbNewLine _
                                              & "ON     MTS.WH_CD       =    MAIN.WH_CD                                    " & vbNewLine _
                                              & "AND    MTS.TOU_NO      =    MAIN.TOU_NO                                   " & vbNewLine _
                                              & "AND    MTS.SITU_NO     =    MAIN.SITU_NO                                  " & vbNewLine _
                                              & "--ADD 2016/09/07 End                                                      " & vbNewLine _
                                              & "GROUP BY                                                                  " & vbNewLine _
                                              & "     KBN1.KBN_NM2                                                         " & vbNewLine _
                                              & "    ,KMK.SEIQKMK_NM                                                       " & vbNewLine _
                                              & "    ,KMK.KEIRI_KB                                                         " & vbNewLine _
                                              & "    ,MAIN.TAX_KB                                                          " & vbNewLine _
                                              & "    ,KBN2.KBN_NM1                                                         " & vbNewLine _
                                              & "    ,KBN3.KBN_CD                                                          " & vbNewLine _
                                              & "    ,SEQ.UNCHIN_NR                                                        " & vbNewLine _
                                              & "    ,SEQ.UNCHIN_NG                                                        " & vbNewLine _
                                              & "--ADD 2016/09/07 Start 再保管対応                                         " & vbNewLine _
                                              & "    ,MAIN.WH_CD                                                           " & vbNewLine _
                                              & "    ,MAIN.TOU_NO                                                          " & vbNewLine _
                                              & "    ,MAIN.SITU_NO                                                         " & vbNewLine _
                                              & "    ,MTS.JISYATASYA_KB                                                    " & vbNewLine _
                                              & "    ,MAIN.MOTO_DATA_KB                                                    " & vbNewLine _
                                              & "--真荷主 start                                                            " & vbNewLine _
                                              & "    ,MAIN.TCUST_BPCD                                                      " & vbNewLine _
                                              & "    ,MAIN.TCUST_BPNM                                                      " & vbNewLine _
                                              & "--真荷主 end                                                              " & vbNewLine _
                                              & "--製品セグメント start                                                    " & vbNewLine _
                                              & "    ,MAIN.PRODUCT_SEG_CD                                                  " & vbNewLine _
                                              & "--製品セグメント end                                                      " & vbNewLine _
                                              & "--地域セグメント start                                                    " & vbNewLine _
                                              & "    ,MAIN.ORIG_SEG_CD                                                     " & vbNewLine _
                                              & "    ,MAIN.DEST_SEG_CD                                                     " & vbNewLine _
                                              & "--地域セグメント end                                                      " & vbNewLine _
                                              & "--運賃科目分け start                                                      " & vbNewLine _
                                              & "    ,MAIN.SEIQKMK_CD_S                                                    " & vbNewLine _
                                              & "--運賃科目分け end                                                        " & vbNewLine _
                                              & "--ADD 2016/09/07 End                                                      " & vbNewLine

#End Region

#Region "作業料検索 SQL"

    ''' <summary>
    ''' 作業料確定チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_SAGYO_KAKUTEI As String = "SELECT                                                      " & vbNewLine _
                                                  & "         COUNT(SAGYO_REC_NO)     AS    SELECT_CNT           " & vbNewLine _
                                                  & "FROM                                                        " & vbNewLine _
                                                  & "        $LM_TRN$..E_SAGYO        SAG                        " & vbNewLine _
                                                  & "        WHERE                                               " & vbNewLine _
                                                  & "               SAG.NRS_BR_CD       =    @NRS_BR_CD          " & vbNewLine _
                                                  & "        AND    SAG.SEIQTO_CD       =    @SEIQTO_CD          " & vbNewLine _
                                                  & "        AND    SAG.SAGYO_COMP_DATE >=   @SAG_SKYU_DATE_FROM " & vbNewLine _
                                                  & "        AND    SAG.SAGYO_COMP_DATE <=   @SKYU_DATE          " & vbNewLine _
                                                  & "        AND    SAG.SKYU_CHK        =    '00'                " & vbNewLine _
                                                  & "        AND    SAG.SYS_DEL_FLG     =    '0'                 " & vbNewLine

    ''' <summary>
    ''' 作業料取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGYO As String = "SELECT                                                      " & vbNewLine _
                                             & "   '04'                AS    GROUP_KB                       " & vbNewLine _
                                             & "  ,KBN1.KBN_NM2        AS    SEIQKMK_CD                     " & vbNewLine _
                                             & "  ,KMK.SEIQKMK_NM      AS    SEIQKMK_NM                     " & vbNewLine _
                                             & "  ,KMK.KEIRI_KB        AS    KEIRI_KB                       " & vbNewLine _
                                             & "  ,MAIN.TAX_KB         AS    TAX_KB                         " & vbNewLine _
                                             & "  ,KBN2.KBN_NM1        AS    TAX_KB_NM                      " & vbNewLine _
                                             & "  ,MAIN.BUSYO_CD       AS    BUSYO_CD                       " & vbNewLine _
                                             & "  ,MAIN.SAGYO_GK * MAIN.BUSYO_BETSU_KOSU / MAIN.ALL_KOSU  AS  KEISAN_TLGK" & vbNewLine _
                                             & "  ,CAST(SEQ.SAGYO_NR AS DECIMAL(5,2)) AS    NEBIKI_RT       " & vbNewLine _
                                             & "  ,SEQ.SAGYO_NG        AS    NEBIKI_GK                      " & vbNewLine _
                                             & "  ,''                  AS    TEKIYO                         " & vbNewLine _
                                             & "  ,'00'                AS    TEMPLATE_IMP_FLG               " & vbNewLine _
                                             & "  ,MAIN.SAGYO_REC_NO   AS    SAGYO_REC_NO                   " & vbNewLine _
                                             & "  ,MAIN.SAGYO_GK       AS    SAGYO_GK                       " & vbNewLine _
                                             & "--真荷主 start                                              " & vbNewLine _
                                             & "  ,MAIN.TCUST_BPCD     AS TCUST_BPCD                        " & vbNewLine _
                                             & "  ,MAIN.TCUST_BPNM     AS TCUST_BPNM                        " & vbNewLine _
                                             & "--真荷主 end                                                " & vbNewLine _
                                             & "--製品セグメント start                                      " & vbNewLine _
                                             & "  ,MAIN.PRODUCT_SEG_CD AS    PRODUCT_SEG_CD                 " & vbNewLine _
                                             & "--製品セグメント end                                        " & vbNewLine _
                                             & "--地域セグメント start                                      " & vbNewLine _
                                             & "  ,MAIN.ORIG_SEG_CD    AS    ORIG_SEG_CD                    " & vbNewLine _
                                             & "  ,MAIN.DEST_SEG_CD    AS    DEST_SEG_CD                    " & vbNewLine _
                                             & "--地域セグメント end                                        " & vbNewLine _
                                             & "FROM                                                        " & vbNewLine _
                                             & "  (                                                         " & vbNewLine _
                                             & "--■入荷作業                                                " & vbNewLine _
                                             & "      (                                                     " & vbNewLine _
                                             & "        SELECT                                              " & vbNewLine _
                                             & "               SAG.SAGYO_REC_NO   AS    SAGYO_REC_NO        " & vbNewLine _
                                             & "              ,SAG.TAX_KB         AS    TAX_KB              " & vbNewLine _
                                             & "              ,SAG.SAGYO_GK       AS    SAGYO_GK            " & vbNewLine _
                                             & "              ,SAG.NRS_BR_CD      AS    NRS_BR_CD           " & vbNewLine _
                                             & "              ,SAG.SEIQTO_CD      AS    SEIQTO_CD           " & vbNewLine _
                                             & "              ,SAG.IOZS_KB        AS    IOZS_KB             " & vbNewLine _
                                             & "              ,ISNULL(KBN_URIBU.KBN_NM1,KBN_BUCD.KBN_CD)                 AS  BUSYO_CD         " & vbNewLine _
                                             & "              ,ISNULL((SUM(IN_S.KONSU) * MG.PKG_NB) + SUM(IN_S.HASU),1)  AS  BUSYO_BETSU_KOSU " & vbNewLine _
                                             & "              ,CASE WHEN 0 = (ISNULL(SUB.ALL_KOSU,1))                                         " & vbNewLine _
                                             & "               THEN '1'                                                                       " & vbNewLine _
                                             & "               ELSE ISNULL(SUB.ALL_KOSU,1)                                                    " & vbNewLine _
                                             & "               END                                                       AS  ALL_KOSU         " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "              ,CST.TCUST_BPCD     AS TCUST_BPCD                      " & vbNewLine _
                                             & "              ,MBP.BP_NM1         AS TCUST_BPNM                      " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "--製品セグメント start                                               " & vbNewLine _
                                             & "              ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''            " & vbNewLine _
                                             & "                    THEN SSG2.CNCT_SEG_CD                            " & vbNewLine _
                                             & "                    ELSE SSG1.CNCT_SEG_CD                            " & vbNewLine _
                                             & "                    END AS PRODUCT_SEG_CD                            " & vbNewLine _
                                             & "--製品セグメント end                                                 " & vbNewLine _
                                             & "--地域セグメント start                                               " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD   AS ORIG_SEG_CD                     " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD   AS DEST_SEG_CD                     " & vbNewLine _
                                             & "--地域セグメント end                                                 " & vbNewLine _
                                             & "        FROM                                                         " & vbNewLine _
                                             & "            $LM_TRN$..E_SAGYO        SAG                             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_TRN$..B_INKA_L   IN_L               " & vbNewLine _
                                             & "             ON  IN_L.NRS_BR_CD           =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  IN_L.INKA_NO_L           =    SUBSTRING(SAG.INOUTKA_NO_LM,1,9)   " & vbNewLine _
                                             & "            AND  IN_L.SYS_DEL_FLG         =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_TRN$..B_INKA_M   IN_M               " & vbNewLine _
                                             & "             ON  IN_M.NRS_BR_CD           =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  IN_M.INKA_NO_L           =    IN_L.INKA_NO_L        " & vbNewLine _
                                             & "            AND  IN_M.INKA_NO_M           =  --  SUBSTRING(SAG.INOUTKA_NO_LM,10,3)  " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "         CASE WHEN SUBSTRING(SAG.INOUTKA_NO_LM,10,3) = '000'             " & vbNewLine _
                                             & "         THEN                                            " & vbNewLine _
                                             & "         (select MIN(INKA_NO_M) from $LM_TRN$..B_INKA_M IM2 where IM2.NRS_BR_CD = @NRS_BR_CD AND IM2.INKA_NO_L = SUBSTRING(SAG.INOUTKA_NO_LM,1,9) and IM2.SYS_DEL_FLG = '0')             " & vbNewLine _
                                             & "          ELSE   SUBSTRING(SAG.INOUTKA_NO_LM,10,3)             " & vbNewLine _
                                             & "           END                                           " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "            AND  IN_M.SYS_DEL_FLG         =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_TRN$..B_INKA_S   IN_S               " & vbNewLine _
                                             & "             ON  IN_S.NRS_BR_CD           =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  IN_S.INKA_NO_L           =    IN_M.INKA_NO_L        " & vbNewLine _
                                             & "            AND  IN_S.INKA_NO_M           =    IN_M.INKA_NO_M        " & vbNewLine _
                                             & "            AND  IN_S.SYS_DEL_FLG         =    '0'                   " & vbNewLine _
                                             & "   LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   MG                 " & vbNewLine _
                                             & "             ON  MG.NRS_BR_CD             =    IN_M.NRS_BR_CD        " & vbNewLine _
                                             & "            AND  MG.GOODS_CD_NRS          =    IN_M.GOODS_CD_NRS     " & vbNewLine _
                                             & "--真荷主取得 start                                                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..M_CUST CST                    " & vbNewLine _
                                             & "             ON    CST.NRS_BR_CD       =   MG.NRS_BR_CD              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_L       =   MG.CUST_CD_L              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_M       =   MG.CUST_CD_M              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_S       =   MG.CUST_CD_S              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_SS      =   MG.CUST_CD_SS             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  ABM_DB..M_BP MBP                        " & vbNewLine _
                                             & "            ON     MBP.BP_CD           =   CST.TCUST_BPCD            " & vbNewLine _
                                             & "            AND    MBP.SYS_DEL_FLG     =   '0'                       " & vbNewLine _
                                             & "--真荷主取得 end                                                     " & vbNewLine _
                                             & "--製品セグメント取得① start                                         " & vbNewLine _
                                             & "            LEFT JOIN                                                " & vbNewLine _
                                             & "                (                                                    " & vbNewLine _
                                             & "                  SELECT *                                           " & vbNewLine _
                                             & "                  FROM $LM_MST$..M_TANKA A                           " & vbNewLine _
                                             & "                  WHERE                                              " & vbNewLine _
                                             & "                    A.STR_DATE <= @SKYU_DATE                         " & vbNewLine _
                                             & "                    AND A.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                             & "                    AND NOT EXISTS (                                 " & vbNewLine _
                                             & "                      SELECT 1                                       " & vbNewLine _
                                             & "                      FROM                                           " & vbNewLine _
                                             & "                        $LM_MST$..M_TANKA B                          " & vbNewLine _
                                             & "                      WHERE                                          " & vbNewLine _
                                             & "                        B.NRS_BR_CD = A.NRS_BR_CD                    " & vbNewLine _
                                             & "                        AND B.CUST_CD_L = A.CUST_CD_L                " & vbNewLine _
                                             & "                        AND B.CUST_CD_M = A.CUST_CD_M                " & vbNewLine _
                                             & "                        AND B.UP_GP_CD_1 = A.UP_GP_CD_1              " & vbNewLine _
                                             & "                        AND B.STR_DATE <= @SKYU_DATE                 " & vbNewLine _
                                             & "                        AND B.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                             & "                        AND B.STR_DATE > A.STR_DATE                  " & vbNewLine _
                                             & "                      )                                              " & vbNewLine _
                                             & "                ) AS TNK                                             " & vbNewLine _
                                             & "             ON    TNK.NRS_BR_CD       =   MG.NRS_BR_CD                " & vbNewLine _
                                             & "            AND    TNK.CUST_CD_L       =   MG.CUST_CD_L                " & vbNewLine _
                                             & "            AND    TNK.CUST_CD_M       =   MG.CUST_CD_M                " & vbNewLine _
                                             & "            AND    TNK.UP_GP_CD_1      =   MG.UP_GP_CD_1               " & vbNewLine _
                                             & "            AND    TNK.SYS_DEL_FLG     =   '0'                         " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG1             " & vbNewLine _
                                             & "             ON    SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD         " & vbNewLine _
                                             & "            AND    SSG1.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                             & "            AND    SSG1.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                             & "            AND    SSG1.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                             & "--製品セグメント取得① end                                           " & vbNewLine _
                                             & "--製品セグメント取得② start                                         " & vbNewLine _
                                             & "            LEFT OUTER JOIN                                          " & vbNewLine _
                                             & "            (                                                        " & vbNewLine _
                                             & "              SELECT                                                 " & vbNewLine _
                                             & "                 NRS_BR_CD                                           " & vbNewLine _
                                             & "                ,CUST_CD_L                                           " & vbNewLine _
                                             & "                ,CUST_CD_M                                           " & vbNewLine _
                                             & "                ,MIN(PRODUCT_SEG_CD) AS PRODUCT_SEG_CD               " & vbNewLine _
                                             & "              FROM                                                   " & vbNewLine _
                                             & "                $LM_MST$..M_CUST                                     " & vbNewLine _
                                             & "              WHERE                                                  " & vbNewLine _
                                             & "                SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                             & "              GROUP BY                                               " & vbNewLine _
                                             & "                 NRS_BR_CD                                           " & vbNewLine _
                                             & "                ,CUST_CD_L                                           " & vbNewLine _
                                             & "                ,CUST_CD_M                                           " & vbNewLine _
                                             & "            ) MCT                                                    " & vbNewLine _
                                             & "             ON MCT.NRS_BR_CD = SAG.NRS_BR_CD                        " & vbNewLine _
                                             & "            AND MCT.CUST_CD_L = SAG.CUST_CD_L                        " & vbNewLine _
                                             & "            AND MCT.CUST_CD_M = SAG.CUST_CD_M                        " & vbNewLine _
                                             & "            LEFT JOIN ABM_DB..M_SEGMENT SSG2                         " & vbNewLine _
                                             & "            ON     SSG2.CNCT_SEG_CD     =   MCT.PRODUCT_SEG_CD         " & vbNewLine _
                                             & "            AND    SSG2.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                             & "            AND    SSG2.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                             & "            AND    SSG2.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                             & "--製品セグメント取得② end                                           " & vbNewLine _
                                             & "--地域セグメント取得 start                                           " & vbNewLine _
                                             & "            LEFT JOIN $LM_MST$..M_SOKO SOKO                          " & vbNewLine _
                                             & "                  ON     SOKO.WH_CD           =   SAG.WH_CD          " & vbNewLine _
                                             & "                  AND    SOKO.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..Z_KBN         AS  AKB1           " & vbNewLine _
                                             & "                  ON     AKB1.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'           " & vbNewLine _
                                             & "                  AND    AKB1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                             & "                  AND    AKB1.KBN_NM3         =   LEFT(SOKO.JIS_CD,2)   " & vbNewLine _
                                             & "                  AND    AKB1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..M_SEGMENT     AS  CSG1           " & vbNewLine _
                                             & "                  ON     CSG1.DATA_TYPE_CD    =   '00001'            " & vbNewLine _
                                             & "                  AND    CSG1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                             & "                  AND    CSG1.KBN_GROUP_CD    =   AKB1.KBN_GRP_REF1  " & vbNewLine _
                                             & "                  AND    CSG1.KBN_CD          =   AKB1.KBN_CD_REF1   " & vbNewLine _
                                             & "                  AND    CSG1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "--地域セグメント取得 end                                             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..Z_KBN     KBN_URIBU           " & vbNewLine _
                                             & "             ON  KBN_URIBU.KBN_GROUP_CD   =    'B008'                " & vbNewLine _
                                             & "            AND  KBN_URIBU.KBN_NM2        =    IN_L.WH_CD            " & vbNewLine _
                                             & "            AND  KBN_URIBU.KBN_NM3        =    IN_S.TOU_NO           " & vbNewLine _
                                             & "            AND  KBN_URIBU.KBN_NM4        =    IN_L.NRS_BR_CD        " & vbNewLine _
                                             & "            AND  KBN_URIBU.SYS_DEL_FLG    =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..Z_KBN     KBN_BUCD            " & vbNewLine _
                                             & "             ON  KBN_BUCD.KBN_GROUP_CD    =    'B007'                " & vbNewLine _
                                             & "            AND  KBN_BUCD.KBN_NM2         =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "--upd 20211201 026003           AND  KBN_BUCD.KBN_NM3         =    '1'                   " & vbNewLine _
                                             & "            AND  KBN_BUCD.KBN_CD         =    @BUSYO_CD              " & vbNewLine _
                                             & "            AND  KBN_BUCD.SYS_DEL_FLG     =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN                                          " & vbNewLine _
                                             & "                 (SELECT                                             " & vbNewLine _
                                             & "                     SAG2.NRS_BR_CD            AS  NRS_BR_CD         " & vbNewLine _
                                             & "                    ,SAG2.SAGYO_REC_NO         AS  SAGYO_REC_NO      " & vbNewLine _
                                             & "                    ,CASE WHEN 0 = (SUM(IN_S.KONSU) * MG.PKG_NB + SUM(IN_S.HASU)) " & vbNewLine _
                                             & "                     THEN '1'                                                     " & vbNewLine _
                                             & "                     ELSE SUM(IN_S.KONSU) * MG.PKG_NB + SUM(IN_S.HASU)            " & vbNewLine _
                                             & "                     END                                     AS  ALL_KOSU         " & vbNewLine _
                                             & "                  FROM                                               " & vbNewLine _
                                             & "                      $LM_TRN$..E_SAGYO        SAG2                  " & vbNewLine _
                                             & "                      LEFT OUTER JOIN  $LM_TRN$..B_INKA_L   IN_L     " & vbNewLine _
                                             & "                       ON  IN_L.NRS_BR_CD   =    SAG2.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  IN_L.INKA_NO_L   =    SUBSTRING(SAG2.INOUTKA_NO_LM,1,9) " & vbNewLine _
                                             & "                      AND  IN_L.SYS_DEL_FLG =    '0'                 " & vbNewLine _
                                             & "                      LEFT OUTER JOIN  $LM_TRN$..B_INKA_M   IN_M     " & vbNewLine _
                                             & "                       ON  IN_M.NRS_BR_CD   =    SAG2.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  IN_M.INKA_NO_L   =    IN_L.INKA_NO_L      " & vbNewLine _
                                             & "                      AND  IN_M.INKA_NO_M   =  --  SUBSTRING(SAG2.INOUTKA_NO_LM,10,3) " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "                      CASE WHEN SUBSTRING(SAG2.INOUTKA_NO_LM,10,3) = '000'             " & vbNewLine _
                                             & "                      THEN                                            " & vbNewLine _
                                             & "                      (select MIN(INKA_NO_M) from $LM_TRN$..B_INKA_M IM2 where IM2.NRS_BR_CD = @NRS_BR_CD AND IM2.INKA_NO_L = SUBSTRING(SAG2.INOUTKA_NO_LM,1,9) and IM2.SYS_DEL_FLG = '0')             " & vbNewLine _
                                             & "                      ELSE   SUBSTRING(SAG2.INOUTKA_NO_LM,10,3)             " & vbNewLine _
                                             & "                       END                                           " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "                      AND  IN_M.SYS_DEL_FLG =    '0'                 " & vbNewLine _
                                             & "                      LEFT OUTER JOIN  $LM_TRN$..B_INKA_S   IN_S     " & vbNewLine _
                                             & "                       ON  IN_S.NRS_BR_CD   =    SAG2.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  IN_S.INKA_NO_L   =    IN_M.INKA_NO_L      " & vbNewLine _
                                             & "                      AND  IN_S.INKA_NO_M   =    IN_M.INKA_NO_M      " & vbNewLine _
                                             & "                      AND  IN_S.SYS_DEL_FLG =    '0'                 " & vbNewLine _
                                             & "    LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   MG       " & vbNewLine _
                                             & "                       ON  MG.NRS_BR_CD     =    IN_M.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  MG.GOODS_CD_NRS  =    IN_M.GOODS_CD_NRS   " & vbNewLine _
                                             & "                  WHERE                                              " & vbNewLine _
                                             & "                       SAG2.IOZS_KB IN ('10','11','12')              " & vbNewLine _
                                             & "                   AND SAG2.SYS_DEL_FLG     =    '0'                 " & vbNewLine _
                                             & "                  GROUP BY                                           " & vbNewLine _
                                             & "                     SAG2.NRS_BR_CD                                  " & vbNewLine _
                                             & "                    ,SAG2.SAGYO_REC_NO                               " & vbNewLine _
                                             & "                    ,MG.PKG_NB                                       " & vbNewLine _
                                             & "                 ) SUB                                               " & vbNewLine _
                                             & "             ON  SUB.NRS_BR_CD            =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  SUB.SAGYO_REC_NO         =    SAG.SAGYO_REC_NO      " & vbNewLine _
                                             & "        WHERE                                                        " & vbNewLine _
                                             & "               SAG.NRS_BR_CD       =    @NRS_BR_CD                   " & vbNewLine _
                                             & "        AND    SAG.SEIQTO_CD       =    @SEIQTO_CD                   " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP_DATE >=   @SAG_SKYU_DATE_FROM          " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP_DATE <=   @SKYU_DATE                   " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP      =    '01'                         " & vbNewLine _
                                             & "        AND    SAG.SKYU_CHK        =    '01'                         " & vbNewLine _
                                             & "        AND    SAG.SYS_DEL_FLG     =    '0'                          " & vbNewLine _
                                             & "        AND    SAG.SAGYO_GK        IS NOT NULL                       " & vbNewLine _
                                             & "        AND    SAG.IOZS_KB IN ('10','11','12')                       " & vbNewLine _
                                             & "        GROUP BY                                                     " & vbNewLine _
                                             & "               SAG.SAGYO_REC_NO                                      " & vbNewLine _
                                             & "              ,SAG.TAX_KB                                            " & vbNewLine _
                                             & "              ,SAG.SAGYO_GK                                          " & vbNewLine _
                                             & "              ,SAG.NRS_BR_CD                                         " & vbNewLine _
                                             & "              ,SAG.SEIQTO_CD                                         " & vbNewLine _
                                             & "              ,SAG.IOZS_KB                                           " & vbNewLine _
                                             & "              ,KBN_BUCD.KBN_CD                                       " & vbNewLine _
                                             & "              ,MG.PKG_NB                                             " & vbNewLine _
                                             & "              ,SUB.ALL_KOSU                                          " & vbNewLine _
                                             & "              ,KBN_URIBU.KBN_NM1                                     " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "              ,CST.TCUST_BPCD                                        " & vbNewLine _
                                             & "              ,MBP.BP_NM1                                            " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "--製品セグメント start                                               " & vbNewLine _
                                             & "              ,SSG1.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "              ,SSG2.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "--製品セグメント end                                                 " & vbNewLine _
                                             & "--地域セグメント start                                               " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "--地域セグメント end                                                 " & vbNewLine _
                                             & "       )                                                             " & vbNewLine _
                                             & "       UNION                                                         " & vbNewLine _
                                             & "--■出荷作業                                                         " & vbNewLine _
                                             & "      (                                                              " & vbNewLine _
                                             & "        SELECT                                                       " & vbNewLine _
                                             & "               SAG.SAGYO_REC_NO   AS    SAGYO_REC_NO                 " & vbNewLine _
                                             & "              ,SAG.TAX_KB         AS    TAX_KB                       " & vbNewLine _
                                             & "              ,SAG.SAGYO_GK       AS    SAGYO_GK                     " & vbNewLine _
                                             & "              ,SAG.NRS_BR_CD      AS    NRS_BR_CD                    " & vbNewLine _
                                             & "              ,SAG.SEIQTO_CD      AS    SEIQTO_CD                    " & vbNewLine _
                                             & "              ,SAG.IOZS_KB        AS    IOZS_KB                      " & vbNewLine _
                                             & "              ,ISNULL(KBN_URIBU.KBN_NM1,KBN_BUCD.KBN_CD)  AS  BUSYO_CD         " & vbNewLine _
                                             & "              ,ISNULL(SUM(OT_S.OUTKA_TTL_NB),1)           AS  BUSYO_BETSU_KOSU " & vbNewLine _
                                             & "              ,CASE WHEN 0 = (ISNULL(SUB.ALL_KOSU,1))                          " & vbNewLine _
                                             & "               THEN '1'                                                        " & vbNewLine _
                                             & "               ELSE ISNULL(SUB.ALL_KOSU,1)                                     " & vbNewLine _
                                             & "               END                                        AS  ALL_KOSU         " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "              ,CST.TCUST_BPCD     AS TCUST_BPCD                      " & vbNewLine _
                                             & "              ,MBP.BP_NM1         AS TCUST_BPNM                      " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "--製品セグメント start                                               " & vbNewLine _
                                             & "              ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''            " & vbNewLine _
                                             & "                    THEN SSG2.CNCT_SEG_CD                            " & vbNewLine _
                                             & "                    ELSE SSG1.CNCT_SEG_CD                            " & vbNewLine _
                                             & "                    END AS PRODUCT_SEG_CD                            " & vbNewLine _
                                             & "--製品セグメント end                                                 " & vbNewLine _
                                             & "--地域セグメント start                                               " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD   AS ORIG_SEG_CD                     " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD   AS DEST_SEG_CD                     " & vbNewLine _
                                             & "--地域セグメント end                                                 " & vbNewLine _
                                             & "        FROM                                                         " & vbNewLine _
                                             & "            $LM_TRN$..E_SAGYO        SAG                             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_TRN$..C_OUTKA_L   OT_L              " & vbNewLine _
                                             & "             ON  OT_L.NRS_BR_CD           =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  OT_L.OUTKA_NO_L          =    SUBSTRING(SAG.INOUTKA_NO_LM,1,9)  " & vbNewLine _
                                             & "            AND  OT_L.SYS_DEL_FLG         =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_TRN$..C_OUTKA_M   OT_M              " & vbNewLine _
                                             & "             ON  OT_M.NRS_BR_CD           =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  OT_M.OUTKA_NO_L          =    OT_L.OUTKA_NO_L       " & vbNewLine _
                                             & "            AND  OT_M.OUTKA_NO_M          =   -- SUBSTRING(SAG.INOUTKA_NO_LM,10,3) " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "        CASE WHEN SUBSTRING(SAG.INOUTKA_NO_LM,10,3) = '000'             " & vbNewLine _
                                             & "        THEN                                            " & vbNewLine _
                                             & "        (select MIN(OUTKA_NO_M) from $LM_TRN$..C_OUTKA_M OM2 where OM2.NRS_BR_CD = @NRS_BR_CD AND OM2.OUTKA_NO_L = SUBSTRING(SAG.INOUTKA_NO_LM,1,9) and OM2.SYS_DEL_FLG = '0')             " & vbNewLine _
                                             & "        ELSE   SUBSTRING(SAG.INOUTKA_NO_LM,10,3)             " & vbNewLine _
                                             & "        END                                           " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "            AND  OT_M.SYS_DEL_FLG         =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_TRN$..C_OUTKA_S   OT_S              " & vbNewLine _
                                             & "             ON  OT_S.NRS_BR_CD           =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  OT_S.OUTKA_NO_L          =    OT_M.OUTKA_NO_L       " & vbNewLine _
                                             & "            AND  OT_S.OUTKA_NO_M          =    OT_M.OUTKA_NO_M       " & vbNewLine _
                                             & "            AND  OT_S.SYS_DEL_FLG         =    '0'                   " & vbNewLine _
                                             & "   LEFT JOIN  (SELECT * FROM $LM_MST$..M_GOODS WHERE NRS_BR_CD =  @NRS_BR_CD )   MG                 " & vbNewLine _
                                             & "             ON  MG.NRS_BR_CD             =    OT_M.NRS_BR_CD        " & vbNewLine _
                                             & "            AND  MG.GOODS_CD_NRS          =    OT_M.GOODS_CD_NRS     " & vbNewLine _
                                             & "--真荷主取得 start                                                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..M_CUST CST                    " & vbNewLine _
                                             & "             ON    CST.NRS_BR_CD       =   MG.NRS_BR_CD              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_L       =   MG.CUST_CD_L              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_M       =   MG.CUST_CD_M              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_S       =   MG.CUST_CD_S              " & vbNewLine _
                                             & "            AND    CST.CUST_CD_SS      =   MG.CUST_CD_SS             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  ABM_DB..M_BP MBP                        " & vbNewLine _
                                             & "            ON     MBP.BP_CD           =   CST.TCUST_BPCD            " & vbNewLine _
                                             & "            AND    MBP.SYS_DEL_FLG     =   '0'                       " & vbNewLine _
                                             & "--真荷主取得 end                                                     " & vbNewLine _
                                             & "--製品セグメント取得① start                                         " & vbNewLine _
                                             & "            LEFT JOIN                                                " & vbNewLine _
                                             & "                (                                                    " & vbNewLine _
                                             & "                  SELECT *                                           " & vbNewLine _
                                             & "                  FROM $LM_MST$..M_TANKA A                           " & vbNewLine _
                                             & "                  WHERE                                              " & vbNewLine _
                                             & "                    A.STR_DATE <= @SKYU_DATE                         " & vbNewLine _
                                             & "                    AND A.SYS_DEL_FLG = '0'                          " & vbNewLine _
                                             & "                    AND NOT EXISTS (                                 " & vbNewLine _
                                             & "                      SELECT 1                                       " & vbNewLine _
                                             & "                      FROM                                           " & vbNewLine _
                                             & "                        $LM_MST$..M_TANKA B                          " & vbNewLine _
                                             & "                      WHERE                                          " & vbNewLine _
                                             & "                        B.NRS_BR_CD = A.NRS_BR_CD                    " & vbNewLine _
                                             & "                        AND B.CUST_CD_L = A.CUST_CD_L                " & vbNewLine _
                                             & "                        AND B.CUST_CD_M = A.CUST_CD_M                " & vbNewLine _
                                             & "                        AND B.UP_GP_CD_1 = A.UP_GP_CD_1              " & vbNewLine _
                                             & "                        AND B.STR_DATE <= @SKYU_DATE                 " & vbNewLine _
                                             & "                        AND B.SYS_DEL_FLG = '0'                      " & vbNewLine _
                                             & "                        AND B.STR_DATE > A.STR_DATE                  " & vbNewLine _
                                             & "                      )                                              " & vbNewLine _
                                             & "                ) AS TNK                                             " & vbNewLine _
                                             & "             ON    TNK.NRS_BR_CD       =   MG.NRS_BR_CD                " & vbNewLine _
                                             & "            AND    TNK.CUST_CD_L       =   MG.CUST_CD_L                " & vbNewLine _
                                             & "            AND    TNK.CUST_CD_M       =   MG.CUST_CD_M                " & vbNewLine _
                                             & "            AND    TNK.UP_GP_CD_1      =   MG.UP_GP_CD_1               " & vbNewLine _
                                             & "            AND    TNK.SYS_DEL_FLG     =   '0'                         " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..M_SEGMENT     AS  SSG1             " & vbNewLine _
                                             & "             ON    SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD         " & vbNewLine _
                                             & "            AND    SSG1.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                             & "            AND    SSG1.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                             & "            AND    SSG1.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                             & "--製品セグメント取得① end                                           " & vbNewLine _
                                             & "--製品セグメント取得② start                                         " & vbNewLine _
                                             & "            LEFT OUTER JOIN                                          " & vbNewLine _
                                             & "            (                                                        " & vbNewLine _
                                             & "              SELECT                                                 " & vbNewLine _
                                             & "                 NRS_BR_CD                                           " & vbNewLine _
                                             & "                ,CUST_CD_L                                           " & vbNewLine _
                                             & "                ,CUST_CD_M                                           " & vbNewLine _
                                             & "                ,MIN(PRODUCT_SEG_CD) AS PRODUCT_SEG_CD               " & vbNewLine _
                                             & "              FROM                                                   " & vbNewLine _
                                             & "                $LM_MST$..M_CUST                                     " & vbNewLine _
                                             & "              WHERE                                                  " & vbNewLine _
                                             & "                SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                             & "              GROUP BY                                               " & vbNewLine _
                                             & "                 NRS_BR_CD                                           " & vbNewLine _
                                             & "                ,CUST_CD_L                                           " & vbNewLine _
                                             & "                ,CUST_CD_M                                           " & vbNewLine _
                                             & "            ) MCT                                                    " & vbNewLine _
                                             & "             ON MCT.NRS_BR_CD = SAG.NRS_BR_CD                        " & vbNewLine _
                                             & "            AND MCT.CUST_CD_L = SAG.CUST_CD_L                        " & vbNewLine _
                                             & "            AND MCT.CUST_CD_M = SAG.CUST_CD_M                        " & vbNewLine _
                                             & "            LEFT JOIN ABM_DB..M_SEGMENT SSG2                         " & vbNewLine _
                                             & "            ON     SSG2.CNCT_SEG_CD     =   MCT.PRODUCT_SEG_CD         " & vbNewLine _
                                             & "            AND    SSG2.DATA_TYPE_CD    =   '00002'                    " & vbNewLine _
                                             & "            AND    SSG2.KBN_LANG        =   @KBN_LANG                  " & vbNewLine _
                                             & "            AND    SSG2.SYS_DEL_FLG     =   '0'                        " & vbNewLine _
                                             & "--製品セグメント取得② end                                           " & vbNewLine _
                                             & "--地域セグメント取得 start                                           " & vbNewLine _
                                             & "            LEFT JOIN $LM_MST$..M_SOKO SOKO                          " & vbNewLine _
                                             & "                  ON     SOKO.WH_CD           =   SAG.WH_CD          " & vbNewLine _
                                             & "                  AND    SOKO.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..Z_KBN         AS  AKB1           " & vbNewLine _
                                             & "                  ON     AKB1.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'           " & vbNewLine _
                                             & "                  AND    AKB1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                             & "                  AND    AKB1.KBN_NM3         =   LEFT(SOKO.JIS_CD,2)   " & vbNewLine _
                                             & "                  AND    AKB1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..M_SEGMENT     AS  CSG1           " & vbNewLine _
                                             & "                  ON     CSG1.DATA_TYPE_CD    =   '00001'            " & vbNewLine _
                                             & "                  AND    CSG1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                             & "                  AND    CSG1.KBN_GROUP_CD    =   AKB1.KBN_GRP_REF1  " & vbNewLine _
                                             & "                  AND    CSG1.KBN_CD          =   AKB1.KBN_CD_REF1   " & vbNewLine _
                                             & "                  AND    CSG1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "--地域セグメント取得 end                                             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..Z_KBN     KBN_URIBU           " & vbNewLine _
                                             & "             ON  KBN_URIBU.KBN_GROUP_CD   =    'B008'                " & vbNewLine _
                                             & "            AND  KBN_URIBU.KBN_NM2        =    OT_L.WH_CD            " & vbNewLine _
                                             & "            AND  KBN_URIBU.KBN_NM3        =    OT_S.TOU_NO           " & vbNewLine _
                                             & "            AND  KBN_URIBU.KBN_NM4        =    OT_L.NRS_BR_CD        " & vbNewLine _
                                             & "            AND  KBN_URIBU.SYS_DEL_FLG    =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..Z_KBN     KBN_BUCD            " & vbNewLine _
                                             & "             ON  KBN_BUCD.KBN_GROUP_CD    =    'B007'                " & vbNewLine _
                                             & "            AND  KBN_BUCD.KBN_NM2         =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "--upd 20211201 026003            AND  KBN_BUCD.KBN_NM3         =    '1'                   " & vbNewLine _
                                             & "            AND  KBN_BUCD.KBN_CD         =    @BUSYO_CD              " & vbNewLine _
                                             & "            AND  KBN_BUCD.SYS_DEL_FLG     =    '0'                   " & vbNewLine _
                                             & "            LEFT OUTER JOIN                                          " & vbNewLine _
                                             & "                 (SELECT                                             " & vbNewLine _
                                             & "                     SAG2.NRS_BR_CD            AS  NRS_BR_CD         " & vbNewLine _
                                             & "                    ,SAG2.SAGYO_REC_NO         AS  SAGYO_REC_NO      " & vbNewLine _
                                             & "              ,CASE WHEN 0 = (SUM(OT_S.OUTKA_TTL_NB))                " & vbNewLine _
                                             & "               THEN '1'                                              " & vbNewLine _
                                             & "               ELSE SUM(OT_S.OUTKA_TTL_NB)                           " & vbNewLine _
                                             & "               END                             AS  ALL_KOSU          " & vbNewLine _
                                             & "                  FROM                                               " & vbNewLine _
                                             & "                      $LM_TRN$..E_SAGYO        SAG2                  " & vbNewLine _
                                             & "                      LEFT OUTER JOIN  $LM_TRN$..C_OUTKA_L   OT_L    " & vbNewLine _
                                             & "                       ON  OT_L.NRS_BR_CD   =    SAG2.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  OT_L.OUTKA_NO_L  =    SUBSTRING(SAG2.INOUTKA_NO_LM,1,9) " & vbNewLine _
                                             & "                      AND  OT_L.SYS_DEL_FLG =    '0'                 " & vbNewLine _
                                             & "                      LEFT OUTER JOIN  $LM_TRN$..C_OUTKA_M   OT_M    " & vbNewLine _
                                             & "                       ON  OT_M.NRS_BR_CD   =    SAG2.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  OT_M.OUTKA_NO_L  =    OT_L.OUTKA_NO_L     " & vbNewLine _
                                             & "                      AND  OT_M.OUTKA_NO_M  =  --  SUBSTRING(SAG2.INOUTKA_NO_LM,10,3) " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "                      CASE WHEN SUBSTRING(SAG2.INOUTKA_NO_LM,10,3) = '000'             " & vbNewLine _
                                             & "                      THEN                                            " & vbNewLine _
                                             & "                      (select MIN(OUTKA_NO_M) from $LM_TRN$..C_OUTKA_M OM2 where OM2.NRS_BR_CD = @NRS_BR_CD AND OM2.OUTKA_NO_L = SUBSTRING(SAG2.INOUTKA_NO_LM,1,9) and OM2.SYS_DEL_FLG = '0')             " & vbNewLine _
                                             & "                       ELSE   SUBSTRING(SAG2.INOUTKA_NO_LM,10,3)             " & vbNewLine _
                                             & "                       END                                           " & vbNewLine _
                                             & "        --20170117 YCC・大黒対応                                    " & vbNewLine _
                                             & "                      AND  OT_M.SYS_DEL_FLG =    '0'                 " & vbNewLine _
                                             & "                      LEFT OUTER JOIN  $LM_TRN$..C_OUTKA_S   OT_S    " & vbNewLine _
                                             & "                       ON  OT_S.NRS_BR_CD   =    SAG2.NRS_BR_CD      " & vbNewLine _
                                             & "                      AND  OT_S.OUTKA_NO_L  =    OT_M.OUTKA_NO_L     " & vbNewLine _
                                             & "                      AND  OT_S.OUTKA_NO_M  =    OT_M.OUTKA_NO_M     " & vbNewLine _
                                             & "                      AND  OT_S.SYS_DEL_FLG =    '0'                 " & vbNewLine _
                                             & "                  WHERE                                              " & vbNewLine _
                                             & "                       SAG2.IOZS_KB IN ('20','21','22')              " & vbNewLine _
                                             & "                   AND SAG2.SYS_DEL_FLG     =    '0'                 " & vbNewLine _
                                             & "                  GROUP BY                                           " & vbNewLine _
                                             & "                     SAG2.NRS_BR_CD                                  " & vbNewLine _
                                             & "                    ,SAG2.SAGYO_REC_NO                               " & vbNewLine _
                                             & "                 ) SUB                                               " & vbNewLine _
                                             & "             ON  SUB.NRS_BR_CD            =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "            AND  SUB.SAGYO_REC_NO         =    SAG.SAGYO_REC_NO      " & vbNewLine _
                                             & "        WHERE                                                        " & vbNewLine _
                                             & "               SAG.NRS_BR_CD       =    @NRS_BR_CD                   " & vbNewLine _
                                             & "        AND    SAG.SEIQTO_CD       =    @SEIQTO_CD                   " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP_DATE >=   @SAG_SKYU_DATE_FROM          " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP_DATE <=   @SKYU_DATE                   " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP      =    '01'                         " & vbNewLine _
                                             & "        AND    SAG.SKYU_CHK        =    '01'                         " & vbNewLine _
                                             & "        AND    SAG.SYS_DEL_FLG     =    '0'                          " & vbNewLine _
                                             & "        AND    SAG.SAGYO_GK        IS NOT NULL                       " & vbNewLine _
                                             & "        AND    SAG.IOZS_KB IN ('20','21','22')                       " & vbNewLine _
                                             & "        GROUP BY                                                     " & vbNewLine _
                                             & "               SAG.SAGYO_REC_NO                                      " & vbNewLine _
                                             & "              ,SAG.TAX_KB                                            " & vbNewLine _
                                             & "              ,SAG.SAGYO_GK                                          " & vbNewLine _
                                             & "              ,SAG.NRS_BR_CD                                         " & vbNewLine _
                                             & "              ,SAG.SEIQTO_CD                                         " & vbNewLine _
                                             & "              ,SAG.IOZS_KB                                           " & vbNewLine _
                                             & "              ,KBN_BUCD.KBN_CD                                       " & vbNewLine _
                                             & "              ,SUB.ALL_KOSU                                          " & vbNewLine _
                                             & "              ,KBN_URIBU.KBN_NM1                                     " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "              ,CST.TCUST_BPCD                                        " & vbNewLine _
                                             & "              ,MBP.BP_NM1                                            " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "--製品セグメント start                                               " & vbNewLine _
                                             & "              ,SSG1.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "              ,SSG2.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "--製品セグメント end                                                 " & vbNewLine _
                                             & "--地域セグメント start                                               " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "--地域セグメント end                                                 " & vbNewLine _
                                             & "        )                                                            " & vbNewLine _
                                             & "--■その他作業                                                       " & vbNewLine _
                                             & "       UNION                                                         " & vbNewLine _
                                             & "      (                                                              " & vbNewLine _
                                             & "        SELECT                                                       " & vbNewLine _
                                             & "               SAG.SAGYO_REC_NO   AS    SAGYO_REC_NO                 " & vbNewLine _
                                             & "              ,SAG.TAX_KB         AS    TAX_KB                       " & vbNewLine _
                                             & "              ,SAG.SAGYO_GK       AS    SAGYO_GK                     " & vbNewLine _
                                             & "              ,SAG.NRS_BR_CD      AS    NRS_BR_CD                    " & vbNewLine _
                                             & "              ,SAG.SEIQTO_CD      AS    SEIQTO_CD                    " & vbNewLine _
                                             & "              ,SAG.IOZS_KB        AS    IOZS_KB                      " & vbNewLine _
                                             & "              ,KBN_BUCD.KBN_CD    AS    BUSYO_CD                     " & vbNewLine _
                                             & "              ,SAG.SAGYO_NB       AS    BUSYO_BETSU_KOSU             " & vbNewLine _
                                             & "              ,SAG.SAGYO_NB       AS    ALL_KOSU                     " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "              ,SEQ.NRS_KEIRI_CD2  AS TCUST_BPCD                      " & vbNewLine _
                                             & "              ,MBP.BP_NM1         AS TCUST_BPNM                      " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "--製品セグメント start                                               " & vbNewLine _
                                             & "              ,SSG1.CNCT_SEG_CD   AS PRODUCT_SEG_CD                  " & vbNewLine _
                                             & "--製品セグメント end                                                 " & vbNewLine _
                                             & "--地域セグメント start                                               " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD   AS ORIG_SEG_CD                     " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD   AS DEST_SEG_CD                     " & vbNewLine _
                                             & "--地域セグメント end                                                 " & vbNewLine _
                                             & "        FROM                                                         " & vbNewLine _
                                             & "            $LM_TRN$..E_SAGYO        SAG                             " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..Z_KBN     KBN_BUCD            " & vbNewLine _
                                             & "             ON  KBN_BUCD.KBN_GROUP_CD    =    'B007'                " & vbNewLine _
                                             & "            AND  KBN_BUCD.KBN_NM2         =    SAG.NRS_BR_CD         " & vbNewLine _
                                             & "--upd 20211201 026003             AND  KBN_BUCD.KBN_NM3         =    '1'                   " & vbNewLine _
                                             & "            AND  KBN_BUCD.KBN_CD          =    @BUSYO_CD             " & vbNewLine _
                                             & "            AND  KBN_BUCD.SYS_DEL_FLG     =    '0'                   " & vbNewLine _
                                             & "--製品セグメント取得 start                                           " & vbNewLine _
                                             & "            LEFT OUTER JOIN                                          " & vbNewLine _
                                             & "            (                                                        " & vbNewLine _
                                             & "              SELECT                                                 " & vbNewLine _
                                             & "                 NRS_BR_CD                                           " & vbNewLine _
                                             & "                ,CUST_CD_L                                           " & vbNewLine _
                                             & "                ,CUST_CD_M                                           " & vbNewLine _
                                             & "                ,MIN(PRODUCT_SEG_CD) AS PRODUCT_SEG_CD               " & vbNewLine _
                                             & "              FROM                                                   " & vbNewLine _
                                             & "                $LM_MST$..M_CUST                                     " & vbNewLine _
                                             & "              WHERE                                                  " & vbNewLine _
                                             & "                SYS_DEL_FLG = '0'                                    " & vbNewLine _
                                             & "              GROUP BY                                               " & vbNewLine _
                                             & "                 NRS_BR_CD                                           " & vbNewLine _
                                             & "                ,CUST_CD_L                                           " & vbNewLine _
                                             & "                ,CUST_CD_M                                           " & vbNewLine _
                                             & "            ) MCT                                                    " & vbNewLine _
                                             & "             ON MCT.NRS_BR_CD = SAG.NRS_BR_CD                        " & vbNewLine _
                                             & "            AND MCT.CUST_CD_L = SAG.CUST_CD_L                        " & vbNewLine _
                                             & "            AND MCT.CUST_CD_M = SAG.CUST_CD_M                        " & vbNewLine _
                                             & "            LEFT JOIN ABM_DB..M_SEGMENT SSG1                         " & vbNewLine _
                                             & "            ON     SSG1.CNCT_SEG_CD     =   MCT.PRODUCT_SEG_CD       " & vbNewLine _
                                             & "            AND    SSG1.DATA_TYPE_CD    =   '00002'                  " & vbNewLine _
                                             & "            AND    SSG1.KBN_LANG        =   @KBN_LANG                " & vbNewLine _
                                             & "            AND    SSG1.SYS_DEL_FLG     =   '0'                      " & vbNewLine _
                                             & "--製品セグメント取得 end                                             " & vbNewLine _
                                             & "--地域セグメント取得 start                                           " & vbNewLine _
                                             & "            LEFT JOIN $LM_MST$..M_SOKO SOKO                          " & vbNewLine _
                                             & "                  ON     SOKO.WH_CD           =   SAG.WH_CD          " & vbNewLine _
                                             & "                  AND    SOKO.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..Z_KBN         AS  AKB1           " & vbNewLine _
                                             & "                  ON     AKB1.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'           " & vbNewLine _
                                             & "                  AND    AKB1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                             & "                  AND    AKB1.KBN_NM3         =   LEFT(SOKO.JIS_CD,2)   " & vbNewLine _
                                             & "                  AND    AKB1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "            LEFT JOIN       ABM_DB..M_SEGMENT     AS  CSG1           " & vbNewLine _
                                             & "                  ON     CSG1.DATA_TYPE_CD    =   '00001'            " & vbNewLine _
                                             & "                  AND    CSG1.KBN_LANG        =   @KBN_LANG          " & vbNewLine _
                                             & "                  AND    CSG1.KBN_GROUP_CD    =   AKB1.KBN_GRP_REF1  " & vbNewLine _
                                             & "                  AND    CSG1.KBN_CD          =   AKB1.KBN_CD_REF1   " & vbNewLine _
                                             & "                  AND    CSG1.SYS_DEL_FLG     =   '0'                " & vbNewLine _
                                             & "--地域セグメント取得 end                                             " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "            LEFT OUTER JOIN  $LM_MST$..M_SEIQTO SEQ                  " & vbNewLine _
                                             & "             ON  SEQ.NRS_BR_CD          =    SAG.NRS_BR_CD           " & vbNewLine _
                                             & "            AND  SEQ.SEIQTO_CD          =    SAG.SEIQTO_CD           " & vbNewLine _
                                             & "            AND  SEQ.SYS_DEL_FLG        =    '0'                     " & vbNewLine _
                                             & "            LEFT OUTER JOIN  ABM_DB..M_BP MBP                        " & vbNewLine _
                                             & "             ON  MBP.BP_CD              =    SEQ.NRS_KEIRI_CD2       " & vbNewLine _
                                             & "            AND  MBP.SYS_DEL_FLG        =    '0'                     " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "        WHERE                                                        " & vbNewLine _
                                             & "               SAG.NRS_BR_CD       =    @NRS_BR_CD                   " & vbNewLine _
                                             & "        AND    SAG.SEIQTO_CD       =    @SEIQTO_CD                   " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP_DATE >=   @SAG_SKYU_DATE_FROM          " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP_DATE <=   @SKYU_DATE                   " & vbNewLine _
                                             & "        AND    SAG.SAGYO_COMP      =    '01'                         " & vbNewLine _
                                             & "        AND    SAG.SKYU_CHK        =    '01'                         " & vbNewLine _
                                             & "        AND    SAG.SYS_DEL_FLG     =    '0'                          " & vbNewLine _
                                             & "        AND    SAG.SAGYO_GK        IS NOT NULL                       " & vbNewLine _
                                             & "        AND    SAG.IOZS_KB NOT IN ('10','11','12','20','21','22')    " & vbNewLine _
                                             & "        GROUP BY                                                     " & vbNewLine _
                                             & "               SAG.SAGYO_REC_NO                                      " & vbNewLine _
                                             & "              ,SAG.TAX_KB                                            " & vbNewLine _
                                             & "              ,SAG.SAGYO_GK                                          " & vbNewLine _
                                             & "              ,SAG.NRS_BR_CD                                         " & vbNewLine _
                                             & "              ,SAG.SEIQTO_CD                                         " & vbNewLine _
                                             & "              ,SAG.IOZS_KB                                           " & vbNewLine _
                                             & "              ,KBN_BUCD.KBN_CD                                       " & vbNewLine _
                                             & "              ,SAG.SAGYO_NB                                          " & vbNewLine _
                                             & "--製品セグメント start                                               " & vbNewLine _
                                             & "              ,SSG1.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "--製品セグメント end                                                 " & vbNewLine _
                                             & "--地域セグメント start                                               " & vbNewLine _
                                             & "              ,CSG1.CNCT_SEG_CD                                      " & vbNewLine _
                                             & "--地域セグメント end                                                 " & vbNewLine _
                                             & "--真荷主 start                                                       " & vbNewLine _
                                             & "              ,SEQ.NRS_KEIRI_CD2                                     " & vbNewLine _
                                             & "              ,MBP.BP_NM1                                            " & vbNewLine _
                                             & "--真荷主 end                                                         " & vbNewLine _
                                             & "        )                                                            " & vbNewLine _
                                             & "    ) MAIN                                                           " & vbNewLine _
                                             & "    LEFT JOIN       $LM_MST$..Z_KBN   KBN1                           " & vbNewLine _
                                             & "      ON     KBN1.KBN_GROUP_CD   =   'S064'                          " & vbNewLine _
                                             & "      AND    KBN1.KBN_NM1        =    '04'                           " & vbNewLine _
                                             & "      AND    KBN1.KBN_NM3        =    MAIN.TAX_KB                    " & vbNewLine _
                                             & "      AND    KBN1.SYS_DEL_FLG    =    '0'                            " & vbNewLine _
                                             & "    LEFT JOIN       $LM_MST$..M_SEIQKMK       KMK                    " & vbNewLine _
                                             & "      ON     KMK.GROUP_KB        =    '04'                           " & vbNewLine _
                                             & "      AND    KMK.SEIQKMK_CD      =    KBN1.KBN_NM2                   " & vbNewLine _
                                             & "      AND    KMK.SYS_DEL_FLG     =    '0'                            " & vbNewLine _
                                             & "    LEFT JOIN       $LM_MST$..Z_KBN   KBN2                           " & vbNewLine _
                                             & "      ON     KBN2.KBN_GROUP_CD   =   'Z001'                          " & vbNewLine _
                                             & "      AND    KBN2.KBN_CD         =    MAIN.TAX_KB                    " & vbNewLine _
                                             & "      AND    KBN2.SYS_DEL_FLG    =    '0'                            " & vbNewLine _
                                             & "    LEFT JOIN       $LM_MST$..Z_KBN   KBN3                           " & vbNewLine _
                                             & "      ON     KBN3.KBN_GROUP_CD   =    'B007'                         " & vbNewLine _
                                             & "      AND    KBN3.KBN_NM2        =    MAIN.NRS_BR_CD                 " & vbNewLine _
                                             & "--upd 20211201 026003      AND    KBN3.KBN_NM3        =    '1'                            " & vbNewLine _
                                             & "      AND    KBN3.KBN_CD         =    @BUSYO_CD                      " & vbNewLine _
                                             & "      AND    KBN3.SYS_DEL_FLG    =    '0'                            " & vbNewLine _
                                             & "    LEFT JOIN  $LM_MST$..M_SEIQTO   SEQ                              " & vbNewLine _
                                             & "      ON     SEQ.NRS_BR_CD       =    MAIN.NRS_BR_CD                 " & vbNewLine _
                                             & "      AND    SEQ.SEIQTO_CD       =    MAIN.SEIQTO_CD                 " & vbNewLine _
                                             & "ORDER BY                                                             " & vbNewLine _
                                             & "  MAIN.SAGYO_REC_NO                                                  " & vbNewLine _
                                             & " ,MAIN.BUSYO_BETSU_KOSU                                              " & vbNewLine

#End Region

#Region "横持料検索 SQL"

    ''' <summary>
    ''' 横持料確定チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_YOKOMOCHI_KAKUTEI As String = "SELECT                                                                " & vbNewLine _
                                                      & "    ISNULL(SUM(SUB.SELECT_CNT),0)  AS   SELECT_CNT                    " & vbNewLine _
                                                      & "FROM                                                                  " & vbNewLine _
                                                      & "    (                                                                 " & vbNewLine _
                                                      & "    SELECT                                                            " & vbNewLine _
                                                      & "        '01'                     AS    SELECT_KBN                     " & vbNewLine _
                                                      & "        ,COUNT(TRS.UNSO_NO_L)    AS    SELECT_CNT                     " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_L           AS    CUST_CD_L                      " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_M           AS    CUST_CD_M                      " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_S           AS    CUST_CD_S                      " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_SS          AS    CUST_CD_SS                     " & vbNewLine _
                                                      & "    FROM                                                              " & vbNewLine _
                                                      & "        $LM_TRN$..F_UNCHIN_TRS            TRS                         " & vbNewLine _
                                                      & "        LEFT JOIN                                                             " & vbNewLine _
                                                      & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                                      & "             WHERE                                                            " & vbNewLine _
                                                      & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                                      & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                                      & "               AND    UNL.OUTKA_PLAN_DATE          >=    @YOK_SKYU_DATE_FROM  " & vbNewLine _
                                                      & "               AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE           " & vbNewLine _
                                                      & "            ) UNL                                                             " & vbNewLine _
                                                      & "    ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                      & "    AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                      & "    AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L            " & vbNewLine _
                                                      & "    AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M            " & vbNewLine _
                                                      & "    AND    UNL.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                      & "    WHERE                                                             " & vbNewLine _
                                                      & "           TRS.SEIQTO_CD                =    @SEIQTO_CD               " & vbNewLine _
                                                      & "    AND    TRS.SEIQ_TARIFF_BUNRUI_KB    IN    ('10','20','30','50')   " & vbNewLine _
                                                      & "    AND    TRS.SEIQ_FIXED_FLAG          =    '00'                     " & vbNewLine _
                                                      & "    AND    TRS.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                      & "    AND    UNL.OUTKA_PLAN_DATE          >=    @YOK_SKYU_DATE_FROM     " & vbNewLine _
                                                      & "    AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE              " & vbNewLine _
                                                      & "    AND UNL.NRS_BR_CD                   =   @NRS_BR_CD                " & vbNewLine _
                                                      & "    GROUP BY                                                          " & vbNewLine _
                                                      & "        TRS.CUST_CD_L                                                 " & vbNewLine _
                                                      & "       ,TRS.CUST_CD_M                                                 " & vbNewLine _
                                                      & "       ,TRS.CUST_CD_S                                                 " & vbNewLine _
                                                      & "       ,TRS.CUST_CD_SS                                                " & vbNewLine _
                                                      & "    UNION ALL                                                         " & vbNewLine _
                                                      & "    SELECT                                                            " & vbNewLine _
                                                      & "        '02'                     AS    SELECT_KBN                     " & vbNewLine _
                                                      & "        ,COUNT(TRS.UNSO_NO_L)    AS    SELECT_CNT                     " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_L           AS    CUST_CD_L                      " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_M           AS    CUST_CD_M                      " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_S           AS    CUST_CD_S                      " & vbNewLine _
                                                      & "        ,TRS.CUST_CD_SS          AS    CUST_CD_SS                     " & vbNewLine _
                                                      & "    FROM                                                              " & vbNewLine _
                                                      & "        $LM_TRN$..F_UNCHIN_TRS            TRS                         " & vbNewLine _
                                                      & "        LEFT JOIN                                                             " & vbNewLine _
                                                      & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                                      & "             WHERE                                                            " & vbNewLine _
                                                      & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                                      & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                                      & "               AND    UNL.ARR_PLAN_DATE            >=    @YOK_SKYU_DATE_FROM  " & vbNewLine _
                                                      & "               AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE           " & vbNewLine _
                                                      & "            ) UNL                                                             " & vbNewLine _
                                                      & "    ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                      & "    AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                      & "    AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L            " & vbNewLine _
                                                      & "    AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M            " & vbNewLine _
                                                      & "    AND    UNL.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                      & "    WHERE                                                             " & vbNewLine _
                                                      & "           TRS.SEIQTO_CD                =    @SEIQTO_CD               " & vbNewLine _
                                                      & "    AND    TRS.SEIQ_TARIFF_BUNRUI_KB    =    '40'                     " & vbNewLine _
                                                      & "    AND    TRS.SEIQ_FIXED_FLAG          =    '00'                     " & vbNewLine _
                                                      & "    AND    TRS.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                      & "    AND    UNL.ARR_PLAN_DATE            >=    @YOK_SKYU_DATE_FROM     " & vbNewLine _
                                                      & "    AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE              " & vbNewLine _
                                                      & "    AND UNL.NRS_BR_CD                   =   @NRS_BR_CD                " & vbNewLine _
                                                      & "    GROUP BY                                                          " & vbNewLine _
                                                      & "        TRS.CUST_CD_L                                                 " & vbNewLine _
                                                      & "       ,TRS.CUST_CD_M                                                 " & vbNewLine _
                                                      & "       ,TRS.CUST_CD_S                                                 " & vbNewLine _
                                                      & "       ,TRS.CUST_CD_SS                                                " & vbNewLine _
                                                      & "    )    SUB                                                          " & vbNewLine _
                                                      & "LEFT JOIN       $LM_MST$..M_CUST   CST                                " & vbNewLine _
                                                      & "ON   CST.CUST_CD_L      =   SUB.CUST_CD_L                             " & vbNewLine _
                                                      & "AND  CST.CUST_CD_M      =   SUB.CUST_CD_M                             " & vbNewLine _
                                                      & "AND  CST.CUST_CD_S      =   SUB.CUST_CD_S                             " & vbNewLine _
                                                      & "AND  CST.CUST_CD_SS     =   SUB.CUST_CD_SS                            " & vbNewLine _
                                                      & "AND  CST.NRS_BR_CD      =   @NRS_BR_CD                                " & vbNewLine _
                                                      & "AND  CST.SYS_DEL_FLG    =   '0'                                       " & vbNewLine _
                                                      & "WHERE                                                                 " & vbNewLine _
                                                      & "     SUB.SELECT_KBN    =   CST.UNTIN_CALCULATION_KB                   " & vbNewLine

    ''' <summary>
    ''' 横持料取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_YOKOMOCHI As String = "SELECT                                                                    " & vbNewLine _
                                                 & "     '05'                               AS    GROUP_KB                    " & vbNewLine _
                                                 & "    ,KBN1.KBN_NM2                       AS    SEIQKMK_CD                  " & vbNewLine _
                                                 & "    ,KMK.SEIQKMK_NM                     AS    SEIQKMK_NM                  " & vbNewLine _
                                                 & "    ,KMK.KEIRI_KB                       AS    KEIRI_KB                    " & vbNewLine _
                                                 & "    ,MAIN.TAX_KB                        AS    TAX_KB                      " & vbNewLine _
                                                 & "    ,KBN2.KBN_NM1                       AS    TAX_KB_NM                   " & vbNewLine _
                                                 & "    ,KBN3.KBN_CD                        AS    BUSYO_CD                    " & vbNewLine _
                                                 & "    ,SUM(MAIN.DECI_UNCHIN) + SUM(MAIN.DECI_CITY_EXTC) + SUM(MAIN.DECI_WINT_EXTC) + SUM(MAIN.DECI_RELY_EXTC) + SUM(MAIN.DECI_TOLL) + SUM(MAIN.DECI_INSU)    AS KEISAN_TLGK " & vbNewLine _
                                                 & "    ,CAST(SEQ.YOKOMOCHI_NR AS DECIMAL(5,2))            AS    NEBIKI_RT    " & vbNewLine _
                                                 & "    ,SEQ.YOKOMOCHI_NG                                  AS    NEBIKI_GK    " & vbNewLine _
                                                 & "    ,''                                 AS    TEKIYO                      " & vbNewLine _
                                                 & "    ,'00'                               AS    TEMPLATE_IMP_FLG            " & vbNewLine _
                                                 & "--真荷主 start                                                            " & vbNewLine _
                                                 & "   ,MAIN.TCUST_BPCD                     AS    TCUST_BPCD                  " & vbNewLine _
                                                 & "   ,MAIN.TCUST_BPNM                     AS    TCUST_BPNM                  " & vbNewLine _
                                                 & "--真荷主 end                                                              " & vbNewLine _
                                                 & "--製品セグメント start                                                    " & vbNewLine _
                                                 & "    ,MAIN.PRODUCT_SEG_CD                AS    PRODUCT_SEG_CD              " & vbNewLine _
                                                 & "--製品セグメント end                                                      " & vbNewLine _
                                                 & "--地域セグメント start                                                    " & vbNewLine _
                                                 & "    ,MAIN.ORIG_SEG_CD                   AS    ORIG_SEG_CD                 " & vbNewLine _
                                                 & "    ,MAIN.DEST_SEG_CD                   AS    DEST_SEG_CD                 " & vbNewLine _
                                                 & "--地域セグメント end                                                      " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "   ,MAIN.SEIQKMK_CD_S                   AS    SEIQKMK_CD_S                " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "FROM                                                                      " & vbNewLine _
                                                 & "    (                                                                     " & vbNewLine _
                                                 & "    SELECT                                                                " & vbNewLine _
                                                 & "         SUB.TAX_KB                  AS    TAX_KB                         " & vbNewLine _
                                                 & "        ,SUB.DECI_UNCHIN             AS    DECI_UNCHIN                    " & vbNewLine _
                                                 & "        ,SUB.DECI_CITY_EXTC          AS    DECI_CITY_EXTC                 " & vbNewLine _
                                                 & "        ,SUB.DECI_WINT_EXTC          AS    DECI_WINT_EXTC                 " & vbNewLine _
                                                 & "        ,SUB.DECI_RELY_EXTC          AS    DECI_RELY_EXTC                 " & vbNewLine _
                                                 & "        ,SUB.DECI_TOLL               AS    DECI_TOLL                      " & vbNewLine _
                                                 & "        ,SUB.DECI_INSU               AS    DECI_INSU                      " & vbNewLine _
                                                 & "        ,SUB.NRS_BR_CD               AS    NRS_BR_CD                      " & vbNewLine _
                                                 & "        ,SUB.SEIQTO_CD               AS    SEIQTO_CD                      " & vbNewLine _
                                                 & "--真荷主 start                                                            " & vbNewLine _
                                                 & "        ,CST.TCUST_BPCD              AS    TCUST_BPCD                     " & vbNewLine _
                                                 & "        ,MBP.BP_NM1                  AS    TCUST_BPNM                     " & vbNewLine _
                                                 & "--真荷主 end                                                              " & vbNewLine _
                                                 & "--製品セグメント start                                                    " & vbNewLine _
                                                 & "        ,SUB.PRODUCT_SEG_CD          AS    PRODUCT_SEG_CD                 " & vbNewLine _
                                                 & "--製品セグメント end                                                      " & vbNewLine _
                                                 & "--地域セグメント start                                                    " & vbNewLine _
                                                 & "        ,CASE SUB.MOTO_DATA_KB                                            " & vbNewLine _
                                                 & "              WHEN '20' THEN CSOH.CNCT_SEG_CD                             " & vbNewLine _
                                                 & "              ELSE           CSIH.CNCT_SEG_CD                             " & vbNewLine _
                                                 & "         END  ORIG_SEG_CD                                                 " & vbNewLine _
                                                 & "        ,CASE SUB.MOTO_DATA_KB                                            " & vbNewLine _
                                                 & "              WHEN '20' THEN CSOC.CNCT_SEG_CD                             " & vbNewLine _
                                                 & "              ELSE           CSIC.CNCT_SEG_CD                             " & vbNewLine _
                                                 & "         END  DEST_SEG_CD                                                 " & vbNewLine _
                                                 & "--地域セグメント end                                                      " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "        ,SUB.SEIQKMK_CD_S            AS    SEIQKMK_CD_S                   " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "    FROM                                                                  " & vbNewLine _
                                                 & "        (                                                                 " & vbNewLine _
                                                 & "        SELECT                                                            " & vbNewLine _
                                                 & "             '01'                               AS    SELECT_KBN          " & vbNewLine _
                                                 & "            ,TRS.TAX_KB                         AS    TAX_KB              " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_UNCHIN,0)          AS    DECI_UNCHIN         " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_CITY_EXTC,0)       AS    DECI_CITY_EXTC      " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_WINT_EXTC,0)       AS    DECI_WINT_EXTC      " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_RELY_EXTC,0)       AS    DECI_RELY_EXTC      " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_TOLL,0)            AS    DECI_TOLL           " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_INSU,0)            AS    DECI_INSU           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_L                      AS    CUST_CD_L           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_M                      AS    CUST_CD_M           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_S                      AS    CUST_CD_S           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_SS                     AS    CUST_CD_SS          " & vbNewLine _
                                                 & "            ,TRS.NRS_BR_CD                      AS    NRS_BR_CD           " & vbNewLine _
                                                 & "            ,TRS.SEIQTO_CD                      AS    SEIQTO_CD           " & vbNewLine _
                                                 & "--製品セグメント start                                                    " & vbNewLine _
                                                 & "            ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''                   " & vbNewLine _
                                                 & "                  THEN SSG2.CNCT_SEG_CD                                   " & vbNewLine _
                                                 & "                  ELSE SSG1.CNCT_SEG_CD                                   " & vbNewLine _
                                                 & "                  END AS PRODUCT_SEG_CD                                   " & vbNewLine _
                                                 & "--製品セグメント end                                                      " & vbNewLine _
                                                 & "--地域セグメント start                                                    " & vbNewLine _
                                                 & "            ,ISNULL(UNL.INOUTKA_NO_L,'')        AS    INOUTKA_NO_L        " & vbNewLine _
                                                 & "            ,ISNULL(UNL.DEST_CD,'')             AS    UNSO_DEST_CD        " & vbNewLine _
                                                 & "            ,ISNULL(UNL.MOTO_DATA_KB,'')        AS    MOTO_DATA_KB        " & vbNewLine _
                                                 & "--地域セグメント end                                                      " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "            ,CASE ISNULL(MUN.UNSOCO_KB,'')                                " & vbNewLine _
                                                 & "                  WHEN '02' THEN ''                                       " & vbNewLine _
                                                 & "                  ELSE           '1'                                      " & vbNewLine _
                                                 & "                  END AS SEIQKMK_CD_S                                     " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "        FROM                                                              " & vbNewLine _
                                                 & "            $LM_TRN$..F_UNCHIN_TRS            TRS                         " & vbNewLine _
                                                 & "        LEFT JOIN                                                             " & vbNewLine _
                                                 & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                                 & "             WHERE                                                            " & vbNewLine _
                                                 & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                                 & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                                 & "               AND    UNL.OUTKA_PLAN_DATE          >=    @YOK_SKYU_DATE_FROM  " & vbNewLine _
                                                 & "               AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE           " & vbNewLine _
                                                 & "            ) UNL                                                             " & vbNewLine _
                                                 & "        ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                 & "        AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L            " & vbNewLine _
                                                 & "        AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M            " & vbNewLine _
                                                 & "        AND    UNL.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_MST$..M_UNSOCO MUN                                        " & vbNewLine _
                                                 & "        ON     MUN.NRS_BR_CD                =    UNL.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    MUN.UNSOCO_CD                =    UNL.UNSO_CD              " & vbNewLine _
                                                 & "        AND    MUN.UNSOCO_BR_CD             =    UNL.UNSO_BR_CD           " & vbNewLine _
                                                 & "        AND    MUN.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "--製品セグメント取得① start                                              " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_TRN$..F_UNSO_M UNM                                        " & vbNewLine _
                                                 & "        ON     UNM.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    UNM.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                 & "        AND    UNM.UNSO_NO_M                =    TRS.UNSO_NO_M            " & vbNewLine _
                                                 & "        AND    UNM.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_MST$..M_GOODS GOD                                         " & vbNewLine _
                                                 & "        ON     GOD.NRS_BR_CD                =    UNM.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    GOD.GOODS_CD_NRS             =    UNM.GOODS_CD_NRS         " & vbNewLine _
                                                 & "        AND    GOD.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            (                                                             " & vbNewLine _
                                                 & "              SELECT *                                                    " & vbNewLine _
                                                 & "              FROM $LM_MST$..M_TANKA A                                    " & vbNewLine _
                                                 & "              WHERE                                                       " & vbNewLine _
                                                 & "                A.STR_DATE <= @SKYU_DATE                                  " & vbNewLine _
                                                 & "                AND A.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "                AND NOT EXISTS (                                          " & vbNewLine _
                                                 & "                  SELECT 1                                                " & vbNewLine _
                                                 & "                  FROM                                                    " & vbNewLine _
                                                 & "                    $LM_MST$..M_TANKA B                                   " & vbNewLine _
                                                 & "                  WHERE                                                   " & vbNewLine _
                                                 & "                    B.NRS_BR_CD = A.NRS_BR_CD                             " & vbNewLine _
                                                 & "                    AND B.CUST_CD_L = A.CUST_CD_L                         " & vbNewLine _
                                                 & "                    AND B.CUST_CD_M = A.CUST_CD_M                         " & vbNewLine _
                                                 & "                    AND B.UP_GP_CD_1 = A.UP_GP_CD_1                       " & vbNewLine _
                                                 & "                    AND B.STR_DATE <= @SKYU_DATE                          " & vbNewLine _
                                                 & "                    AND B.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                                 & "                    AND B.STR_DATE > A.STR_DATE                           " & vbNewLine _
                                                 & "                  )                                                       " & vbNewLine _
                                                 & "            ) AS TNK                                                      " & vbNewLine _
                                                 & "        ON     TNK.NRS_BR_CD       =   GOD.NRS_BR_CD                      " & vbNewLine _
                                                 & "        AND    TNK.CUST_CD_L       =   GOD.CUST_CD_L                      " & vbNewLine _
                                                 & "        AND    TNK.CUST_CD_M       =   GOD.CUST_CD_M                      " & vbNewLine _
                                                 & "        AND    TNK.UP_GP_CD_1      =   GOD.UP_GP_CD_1                     " & vbNewLine _
                                                 & "        AND    TNK.SYS_DEL_FLG     =   '0'                                " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            ABM_DB..M_SEGMENT SSG1                                        " & vbNewLine _
                                                 & "        ON     SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD                " & vbNewLine _
                                                 & "        AND    SSG1.DATA_TYPE_CD    =   '00002'                           " & vbNewLine _
                                                 & "        AND    SSG1.KBN_LANG        =   @KBN_LANG                         " & vbNewLine _
                                                 & "        AND    SSG1.SYS_DEL_FLG     =   '0'                               " & vbNewLine _
                                                 & "--製品セグメント取得① end                                                " & vbNewLine _
                                                 & "--製品セグメント取得② start                                              " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_MST$..M_CUST CST                                          " & vbNewLine _
                                                 & "        ON     CST.NRS_BR_CD       =   TRS.NRS_BR_CD                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_L       =   TRS.CUST_CD_L                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_M       =   TRS.CUST_CD_M                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_S       =   TRS.CUST_CD_S                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_SS      =   TRS.CUST_CD_SS                     " & vbNewLine _
                                                 & "        AND    CST.SYS_DEL_FLG     =   '0'                                " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            ABM_DB..M_SEGMENT SSG2                                        " & vbNewLine _
                                                 & "        ON     SSG2.CNCT_SEG_CD     =   CST.PRODUCT_SEG_CD                " & vbNewLine _
                                                 & "        AND    SSG2.DATA_TYPE_CD    =   '00002'                           " & vbNewLine _
                                                 & "        AND    SSG2.KBN_LANG        =   @KBN_LANG                         " & vbNewLine _
                                                 & "        AND    SSG2.SYS_DEL_FLG     =   '0'                               " & vbNewLine _
                                                 & "--製品セグメント取得② end                                                " & vbNewLine _
                                                 & "        WHERE                                                             " & vbNewLine _
                                                 & "               TRS.SEIQ_TARIFF_BUNRUI_KB    =    '40'                     " & vbNewLine _
                                                 & "        AND    TRS.SEIQ_FIXED_FLAG          =    '01'                     " & vbNewLine _
                                                 & "        AND    TRS.NRS_BR_CD                =    @NRS_BR_CD               " & vbNewLine _
                                                 & "        AND    TRS.SEIQTO_CD                =    @SEIQTO_CD               " & vbNewLine _
                                                 & "        AND    TRS.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "        AND    UNL.OUTKA_PLAN_DATE          >=    @YOK_SKYU_DATE_FROM     " & vbNewLine _
                                                 & "        AND    UNL.OUTKA_PLAN_DATE          <=    @SKYU_DATE              " & vbNewLine _
                                                 & "        UNION ALL                                                         " & vbNewLine _
                                                 & "        SELECT                                                            " & vbNewLine _
                                                 & "             '02'                               AS    SELECT_KBN          " & vbNewLine _
                                                 & "            ,TRS.TAX_KB                         AS    TAX_KB              " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_UNCHIN,0)          AS    DECI_UNCHIN         " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_CITY_EXTC,0)       AS    DECI_CITY_EXTC      " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_WINT_EXTC,0)       AS    DECI_WINT_EXTC      " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_RELY_EXTC,0)       AS    DECI_RELY_EXTC      " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_TOLL,0)            AS    DECI_TOLL           " & vbNewLine _
                                                 & "            ,ISNULL(TRS.DECI_INSU,0)            AS    DECI_INSU           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_L                      AS    CUST_CD_L           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_M                      AS    CUST_CD_M           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_S                      AS    CUST_CD_S           " & vbNewLine _
                                                 & "            ,TRS.CUST_CD_SS                     AS    CUST_CD_SS          " & vbNewLine _
                                                 & "            ,TRS.NRS_BR_CD                      AS    NRS_BR_CD           " & vbNewLine _
                                                 & "            ,TRS.SEIQTO_CD                      AS    SEIQTO_CD           " & vbNewLine _
                                                 & "--製品セグメント start                                                    " & vbNewLine _
                                                 & "            ,CASE WHEN ISNULL(SSG1.CNCT_SEG_CD,'') = ''                   " & vbNewLine _
                                                 & "                  THEN SSG2.CNCT_SEG_CD                                   " & vbNewLine _
                                                 & "                  ELSE SSG1.CNCT_SEG_CD                                   " & vbNewLine _
                                                 & "                  END AS PRODUCT_SEG_CD                                   " & vbNewLine _
                                                 & "--製品セグメント end                                                      " & vbNewLine _
                                                 & "--地域セグメント start                                                    " & vbNewLine _
                                                 & "            ,ISNULL(UNL.INOUTKA_NO_L,'')        AS    INOUTKA_NO_L        " & vbNewLine _
                                                 & "            ,ISNULL(UNL.DEST_CD,'')             AS    UNSO_DEST_CD        " & vbNewLine _
                                                 & "            ,ISNULL(UNL.MOTO_DATA_KB,'')        AS    MOTO_DATA_KB        " & vbNewLine _
                                                 & "--地域セグメント end                                                      " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "            ,CASE ISNULL(MUN.UNSOCO_KB,'')                                " & vbNewLine _
                                                 & "                  WHEN '02' THEN ''                                       " & vbNewLine _
                                                 & "                  ELSE           '1'                                      " & vbNewLine _
                                                 & "                  END AS SEIQKMK_CD_S                                     " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "        FROM                                                              " & vbNewLine _
                                                 & "            $LM_TRN$..F_UNCHIN_TRS            TRS                         " & vbNewLine _
                                                 & "        LEFT JOIN                                                             " & vbNewLine _
                                                 & "            (SELECT * FROM  $LM_TRN$..F_UNSO_L UNL                            " & vbNewLine _
                                                 & "             WHERE                                                            " & vbNewLine _
                                                 & "                      UNL.NRS_BR_CD                =    @NRS_BR_CD            " & vbNewLine _
                                                 & "               AND    UNL.SYS_DEL_FLG              =    '0'                   " & vbNewLine _
                                                 & "               AND    UNL.ARR_PLAN_DATE            >=    @YOK_SKYU_DATE_FROM  " & vbNewLine _
                                                 & "               AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE           " & vbNewLine _
                                                 & "            ) UNL                                                             " & vbNewLine _
                                                 & "        ON     UNL.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    UNL.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                 & "        AND    UNL.CUST_CD_L                =    TRS.CUST_CD_L            " & vbNewLine _
                                                 & "        AND    UNL.CUST_CD_M                =    TRS.CUST_CD_M            " & vbNewLine _
                                                 & "        AND    UNL.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_MST$..M_UNSOCO MUN                                        " & vbNewLine _
                                                 & "        ON     MUN.NRS_BR_CD                =    UNL.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    MUN.UNSOCO_CD                =    UNL.UNSO_CD              " & vbNewLine _
                                                 & "        AND    MUN.UNSOCO_BR_CD             =    UNL.UNSO_BR_CD           " & vbNewLine _
                                                 & "        AND    MUN.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "--製品セグメント取得① start                                              " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_TRN$..F_UNSO_M UNM                                        " & vbNewLine _
                                                 & "        ON     UNM.NRS_BR_CD                =    TRS.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    UNM.UNSO_NO_L                =    TRS.UNSO_NO_L            " & vbNewLine _
                                                 & "        AND    UNM.UNSO_NO_M                =    TRS.UNSO_NO_M            " & vbNewLine _
                                                 & "        AND    UNM.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_MST$..M_GOODS GOD                                         " & vbNewLine _
                                                 & "        ON     GOD.NRS_BR_CD                =    UNM.NRS_BR_CD            " & vbNewLine _
                                                 & "        AND    GOD.GOODS_CD_NRS             =    UNM.GOODS_CD_NRS         " & vbNewLine _
                                                 & "        AND    GOD.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            (                                                             " & vbNewLine _
                                                 & "              SELECT *                                                    " & vbNewLine _
                                                 & "              FROM $LM_MST$..M_TANKA A                                    " & vbNewLine _
                                                 & "              WHERE                                                       " & vbNewLine _
                                                 & "                A.STR_DATE <= @SKYU_DATE                                  " & vbNewLine _
                                                 & "                AND A.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                                 & "                AND NOT EXISTS (                                          " & vbNewLine _
                                                 & "                  SELECT 1                                                " & vbNewLine _
                                                 & "                  FROM                                                    " & vbNewLine _
                                                 & "                    $LM_MST$..M_TANKA B                                   " & vbNewLine _
                                                 & "                  WHERE                                                   " & vbNewLine _
                                                 & "                    B.NRS_BR_CD = A.NRS_BR_CD                             " & vbNewLine _
                                                 & "                    AND B.CUST_CD_L = A.CUST_CD_L                         " & vbNewLine _
                                                 & "                    AND B.CUST_CD_M = A.CUST_CD_M                         " & vbNewLine _
                                                 & "                    AND B.UP_GP_CD_1 = A.UP_GP_CD_1                       " & vbNewLine _
                                                 & "                    AND B.STR_DATE <= @SKYU_DATE                          " & vbNewLine _
                                                 & "                    AND B.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                                 & "                    AND B.STR_DATE > A.STR_DATE                           " & vbNewLine _
                                                 & "                  )                                                       " & vbNewLine _
                                                 & "            ) AS TNK                                                      " & vbNewLine _
                                                 & "        ON     TNK.NRS_BR_CD       =   GOD.NRS_BR_CD                      " & vbNewLine _
                                                 & "        AND    TNK.CUST_CD_L       =   GOD.CUST_CD_L                      " & vbNewLine _
                                                 & "        AND    TNK.CUST_CD_M       =   GOD.CUST_CD_M                      " & vbNewLine _
                                                 & "        AND    TNK.UP_GP_CD_1      =   GOD.UP_GP_CD_1                     " & vbNewLine _
                                                 & "        AND    TNK.SYS_DEL_FLG     =   '0'                                " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            ABM_DB..M_SEGMENT SSG1                                        " & vbNewLine _
                                                 & "        ON     SSG1.CNCT_SEG_CD     =   TNK.PRODUCT_SEG_CD                " & vbNewLine _
                                                 & "        AND    SSG1.DATA_TYPE_CD    =   '00002'                           " & vbNewLine _
                                                 & "        AND    SSG1.KBN_LANG        =   @KBN_LANG                         " & vbNewLine _
                                                 & "        AND    SSG1.SYS_DEL_FLG     =   '0'                               " & vbNewLine _
                                                 & "--製品セグメント取得① end                                                " & vbNewLine _
                                                 & "--製品セグメント取得② start                                              " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            $LM_MST$..M_CUST CST                                          " & vbNewLine _
                                                 & "        ON     CST.NRS_BR_CD       =   TRS.NRS_BR_CD                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_L       =   TRS.CUST_CD_L                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_M       =   TRS.CUST_CD_M                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_S       =   TRS.CUST_CD_S                      " & vbNewLine _
                                                 & "        AND    CST.CUST_CD_SS      =   TRS.CUST_CD_SS                     " & vbNewLine _
                                                 & "        AND    CST.SYS_DEL_FLG     =   '0'                                " & vbNewLine _
                                                 & "        LEFT JOIN                                                         " & vbNewLine _
                                                 & "            ABM_DB..M_SEGMENT SSG2                                        " & vbNewLine _
                                                 & "        ON     SSG2.CNCT_SEG_CD     =   CST.PRODUCT_SEG_CD                " & vbNewLine _
                                                 & "        AND    SSG2.DATA_TYPE_CD    =   '00002'                           " & vbNewLine _
                                                 & "        AND    SSG2.KBN_LANG        =   @KBN_LANG                         " & vbNewLine _
                                                 & "        AND    SSG2.SYS_DEL_FLG     =   '0'                               " & vbNewLine _
                                                 & "--製品セグメント取得② end                                                " & vbNewLine _
                                                 & "        WHERE                                                             " & vbNewLine _
                                                 & "               TRS.SEIQ_TARIFF_BUNRUI_KB    =    '40'                     " & vbNewLine _
                                                 & "        AND    TRS.SEIQ_FIXED_FLAG          =    '01'                     " & vbNewLine _
                                                 & "        AND    TRS.NRS_BR_CD                =    @NRS_BR_CD               " & vbNewLine _
                                                 & "        AND    TRS.SEIQTO_CD                =    @SEIQTO_CD               " & vbNewLine _
                                                 & "        AND    TRS.SYS_DEL_FLG              =    '0'                      " & vbNewLine _
                                                 & "        AND    UNL.ARR_PLAN_DATE            >=    @YOK_SKYU_DATE_FROM     " & vbNewLine _
                                                 & "        AND    UNL.ARR_PLAN_DATE            <=    @SKYU_DATE              " & vbNewLine _
                                                 & "        )    SUB                                                          " & vbNewLine _
                                                 & "LEFT JOIN       $LM_MST$..M_CUST   CST                                    " & vbNewLine _
                                                 & "ON     CST.NRS_BR_CD       =    SUB.NRS_BR_CD                             " & vbNewLine _
                                                 & "AND    CST.CUST_CD_L       =    SUB.CUST_CD_L                             " & vbNewLine _
                                                 & "AND    CST.CUST_CD_M       =    SUB.CUST_CD_M                             " & vbNewLine _
                                                 & "AND    CST.CUST_CD_S       =    SUB.CUST_CD_S                             " & vbNewLine _
                                                 & "AND    CST.CUST_CD_SS      =    SUB.CUST_CD_SS                            " & vbNewLine _
                                                 & "AND    CST.NRS_BR_CD       =    @NRS_BR_CD                                " & vbNewLine _
                                                 & "AND    CST.SYS_DEL_FLG     =    '0'                                       " & vbNewLine _
                                                 & "--真荷主取得 start                                                        " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..M_BP AS  MBP                                          " & vbNewLine _
                                                 & "      ON    MBP.BP_CD         =    CST.TCUST_BPCD                         " & vbNewLine _
                                                 & "      AND   MBP.SYS_DEL_FLG   =    '0'                                    " & vbNewLine _
                                                 & "--真荷主取得 end                                                          " & vbNewLine _
                                                 & "--地域セグメント取得(入荷/発地) start                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_DEST DTIH                                         " & vbNewLine _
                                                 & "      ON    DTIH.NRS_BR_CD    =    SUB.NRS_BR_CD                          " & vbNewLine _
                                                 & "      AND   DTIH.CUST_CD_L    =    SUB.CUST_CD_L                          " & vbNewLine _
                                                 & "      AND   DTIH.DEST_CD      =    SUB.UNSO_DEST_CD                       " & vbNewLine _
                                                 & "      AND   DTIH.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_ZIP ZPIH                                          " & vbNewLine _
                                                 & "      ON    ZPIH.ZIP_NO       =    DTIH.ZIP                               " & vbNewLine _
                                                 & "      AND   ZPIH.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_SOKO SKIH                                         " & vbNewLine _
                                                 & "      ON    SKIH.WH_CD        =    CST.DEFAULT_SOKO_CD                    " & vbNewLine _
                                                 & "      AND   SKIH.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..Z_KBN AKIH                                            " & vbNewLine _
                                                 & "      ON    AKIH.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                                 & "      AND   AKIH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   AKIH.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(ZPIH.JIS_CD,''),2)) = '' THEN LEFT(SKIH.JIS_CD,2) ELSE LEFT(ZPIH.JIS_CD,2) END " & vbNewLine _
                                                 & "      AND   AKIH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..M_SEGMENT CSIH                                        " & vbNewLine _
                                                 & "      ON    CSIH.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                                 & "      AND   CSIH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   CSIH.KBN_GROUP_CD    =   AKIH.KBN_GRP_REF1                    " & vbNewLine _
                                                 & "      AND   CSIH.KBN_CD          =   AKIH.KBN_CD_REF1                     " & vbNewLine _
                                                 & "      AND   CSIH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "--地域セグメント取得(入荷/発地) end                                       " & vbNewLine _
                                                 & "--地域セグメント取得(入荷/着地) start                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_TRN$..B_INKA_L BIL                                        " & vbNewLine _
                                                 & "      ON    BIL.NRS_BR_CD      =    @NRS_BR_CD                            " & vbNewLine _
                                                 & "      AND   BIL.INKA_NO_L      =    SUB.INOUTKA_NO_L                      " & vbNewLine _
                                                 & "      AND   '20'               <>   SUB.MOTO_DATA_KB                      " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_SOKO SKIC1                                        " & vbNewLine _
                                                 & "      ON    SKIC1.WH_CD        =    BIL.WH_CD                             " & vbNewLine _
                                                 & "      AND   SKIC1.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_SOKO SKIC2                                        " & vbNewLine _
                                                 & "      ON    SKIC2.WH_CD        =    CST.DEFAULT_SOKO_CD                   " & vbNewLine _
                                                 & "      AND   SKIC2.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..Z_KBN AKIC                                            " & vbNewLine _
                                                 & "      ON    AKIC.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                                 & "      AND   AKIC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   AKIC.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(SKIC1.JIS_CD,''),2)) = '' THEN LEFT(SKIC2.JIS_CD,2) ELSE LEFT(SKIC1.JIS_CD,2) END " & vbNewLine _
                                                 & "      AND   AKIC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..M_SEGMENT CSIC                                        " & vbNewLine _
                                                 & "      ON    CSIC.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                                 & "      AND   CSIC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   CSIC.KBN_GROUP_CD    =   AKIC.KBN_GRP_REF1                    " & vbNewLine _
                                                 & "      AND   CSIC.KBN_CD          =   AKIC.KBN_CD_REF1                     " & vbNewLine _
                                                 & "      AND   CSIC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "--地域セグメント取得(入荷/着地) end                                       " & vbNewLine _
                                                 & "--地域セグメント取得(出荷/発地) start                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_TRN$..C_OUTKA_L COL                                       " & vbNewLine _
                                                 & "      ON    COL.NRS_BR_CD      =    @NRS_BR_CD                            " & vbNewLine _
                                                 & "      AND   COL.OUTKA_NO_L     =    SUB.INOUTKA_NO_L                      " & vbNewLine _
                                                 & "      AND   '20'               =    SUB.MOTO_DATA_KB                      " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_SOKO SKOH1                                        " & vbNewLine _
                                                 & "      ON    SKOH1.WH_CD        =    COL.WH_CD                             " & vbNewLine _
                                                 & "      AND   SKOH1.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_SOKO SKOH2                                        " & vbNewLine _
                                                 & "      ON    SKOH2.WH_CD        =    CST.DEFAULT_SOKO_CD                   " & vbNewLine _
                                                 & "      AND   SKOH2.SYS_DEL_FLG  =   '0'                                    " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..Z_KBN AKOH                                            " & vbNewLine _
                                                 & "      ON    AKOH.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                                 & "      AND   AKOH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   AKOH.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(SKOH1.JIS_CD,''),2)) = '' THEN LEFT(SKOH2.JIS_CD,2) ELSE LEFT(SKOH1.JIS_CD,2) END " & vbNewLine _
                                                 & "      AND   AKOH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..M_SEGMENT CSOH                                        " & vbNewLine _
                                                 & "      ON    CSOH.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                                 & "      AND   CSOH.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   CSOH.KBN_GROUP_CD    =   AKOH.KBN_GRP_REF1                    " & vbNewLine _
                                                 & "      AND   CSOH.KBN_CD          =   AKOH.KBN_CD_REF1                     " & vbNewLine _
                                                 & "      AND   CSOH.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "--地域セグメント取得(出荷/発地) end                                       " & vbNewLine _
                                                 & "--地域セグメント取得(出荷/着地) start                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_DEST DTOC                                         " & vbNewLine _
                                                 & "      ON    DTOC.NRS_BR_CD    =    COL.NRS_BR_CD                          " & vbNewLine _
                                                 & "      AND   DTOC.CUST_CD_L    =    COL.CUST_CD_L                          " & vbNewLine _
                                                 & "      AND   DTOC.DEST_CD      =    COL.DEST_CD                            " & vbNewLine _
                                                 & "      AND   DTOC.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_ZIP ZPOC                                          " & vbNewLine _
                                                 & "      ON    ZPOC.ZIP_NO       =    DTOC.ZIP                               " & vbNewLine _
                                                 & "      AND   ZPOC.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                                 & "LEFT JOIN   $LM_MST$..M_SOKO SKOC                                         " & vbNewLine _
                                                 & "      ON    SKOC.WH_CD        =    CST.DEFAULT_SOKO_CD                    " & vbNewLine _
                                                 & "      AND   SKOC.SYS_DEL_FLG  =   '0'                                     " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..Z_KBN AKOC                                            " & vbNewLine _
                                                 & "      ON    AKOC.KBN_GROUP_CD    =   '" & ABM_DB_TODOFUKEN & "'                             " & vbNewLine _
                                                 & "      AND   AKOC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   AKOC.KBN_NM3         =   CASE WHEN RTRIM(LEFT(ISNULL(ZPOC.JIS_CD,''),2)) = '' THEN LEFT(SKOC.JIS_CD,2) ELSE LEFT(ZPOC.JIS_CD,2) END " & vbNewLine _
                                                 & "      AND   AKOC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "LEFT JOIN   ABM_DB..M_SEGMENT CSOC                                        " & vbNewLine _
                                                 & "      ON    CSOC.DATA_TYPE_CD    =   '00001'                              " & vbNewLine _
                                                 & "      AND   CSOC.KBN_LANG        =   @KBN_LANG                            " & vbNewLine _
                                                 & "      AND   CSOC.KBN_GROUP_CD    =   AKOC.KBN_GRP_REF1                    " & vbNewLine _
                                                 & "      AND   CSOC.KBN_CD          =   AKOC.KBN_CD_REF1                     " & vbNewLine _
                                                 & "      AND   CSOC.SYS_DEL_FLG     =   '0'                                  " & vbNewLine _
                                                 & "--地域セグメント取得(出荷/着地) end                                       " & vbNewLine _
                                                 & "WHERE                                                                     " & vbNewLine _
                                                 & "    SUB.SELECT_KBN    =   CST.UNTIN_CALCULATION_KB                        " & vbNewLine _
                                                 & ") MAIN                                                                    " & vbNewLine _
                                                 & "LEFT JOIN       $LM_MST$..Z_KBN   KBN1                                    " & vbNewLine _
                                                 & "ON     KBN1.KBN_GROUP_CD   =   'S064'                                     " & vbNewLine _
                                                 & "AND    KBN1.KBN_NM1        =    '05'                                      " & vbNewLine _
                                                 & "AND    KBN1.KBN_NM3        =    MAIN.TAX_KB                               " & vbNewLine _
                                                 & "AND    KBN1.SYS_DEL_FLG    =    '0'                                       " & vbNewLine _
                                                 & "LEFT JOIN       $LM_MST$..M_SEIQKMK       KMK                             " & vbNewLine _
                                                 & "ON     KMK.GROUP_KB        =    '05'                                      " & vbNewLine _
                                                 & "AND    KMK.SEIQKMK_CD      =    KBN1.KBN_NM2                              " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "AND    KMK.SEIQKMK_CD_S    =    MAIN.SEIQKMK_CD_S                         " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "AND    KMK.SYS_DEL_FLG     =    '0'                                       " & vbNewLine _
                                                 & "LEFT JOIN       $LM_MST$..Z_KBN   KBN2                                    " & vbNewLine _
                                                 & "ON     KBN2.KBN_GROUP_CD   =   'Z001'                                     " & vbNewLine _
                                                 & "AND    KBN2.KBN_CD         =    MAIN.TAX_KB                               " & vbNewLine _
                                                 & "AND    KBN2.SYS_DEL_FLG    =    '0'                                       " & vbNewLine _
                                                 & "LEFT JOIN       $LM_MST$..Z_KBN   KBN3                                    " & vbNewLine _
                                                 & "ON     KBN3.KBN_GROUP_CD   =    'B007'                                    " & vbNewLine _
                                                 & "AND    KBN3.KBN_NM2        =    @NRS_BR_CD                                " & vbNewLine _
                                                 & "AND    KBN3.KBN_CD         =    @BUSYO_CD                                 " & vbNewLine _
                                                 & "AND    KBN3.SYS_DEL_FLG    =    '0'                                       " & vbNewLine _
                                                 & "LEFT JOIN       $LM_MST$..M_SEIQTO   SEQ                                  " & vbNewLine _
                                                 & "ON     SEQ.NRS_BR_CD       =    MAIN.NRS_BR_CD                            " & vbNewLine _
                                                 & "AND    SEQ.SEIQTO_CD       =    MAIN.SEIQTO_CD                            " & vbNewLine _
                                                 & "GROUP BY                                                                  " & vbNewLine _
                                                 & "     KBN1.KBN_NM2                                                         " & vbNewLine _
                                                 & "    ,KMK.SEIQKMK_NM                                                       " & vbNewLine _
                                                 & "    ,KMK.KEIRI_KB                                                         " & vbNewLine _
                                                 & "    ,MAIN.TAX_KB                                                          " & vbNewLine _
                                                 & "    ,KBN2.KBN_NM1                                                         " & vbNewLine _
                                                 & "    ,KBN3.KBN_CD                                                          " & vbNewLine _
                                                 & "    ,SEQ.YOKOMOCHI_NR                                                     " & vbNewLine _
                                                 & "    ,SEQ.YOKOMOCHI_NG                                                     " & vbNewLine _
                                                 & "--真荷主 start                                                            " & vbNewLine _
                                                 & "    ,MAIN.TCUST_BPCD                                                      " & vbNewLine _
                                                 & "    ,MAIN.TCUST_BPNM                                                      " & vbNewLine _
                                                 & "--真荷主 end                                                              " & vbNewLine _
                                                 & "--製品セグメント start                                                    " & vbNewLine _
                                                 & "    ,MAIN.PRODUCT_SEG_CD                                                  " & vbNewLine _
                                                 & "--製品セグメント end                                                      " & vbNewLine _
                                                 & "--地域セグメント start                                                    " & vbNewLine _
                                                 & "    ,MAIN.ORIG_SEG_CD                                                     " & vbNewLine _
                                                 & "    ,MAIN.DEST_SEG_CD                                                     " & vbNewLine _
                                                 & "--横持科目分け start                                                      " & vbNewLine _
                                                 & "    ,MAIN.SEIQKMK_CD_S                                                    " & vbNewLine _
                                                 & "--横持科目分け end                                                        " & vbNewLine _
                                                 & "--地域セグメント end                                                      " & vbNewLine

#End Region

#Region "テンプレートマスタ検索 SQL"

    ''' <summary>
    ''' テンプレートマスタ取り込み用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_TEMPLATE As String = "SELECT                                           " & vbNewLine _
                                                & "     TEMP.GROUP_KB        AS    GROUP_KB         " & vbNewLine _
                                                & "    ,TEMP.SEIQKMK_CD      AS    SEIQKMK_CD       " & vbNewLine _
                                                & "    ,KMK.SEIQKMK_NM       AS    SEIQKMK_NM       " & vbNewLine _
                                                & "    ,KMK.KEIRI_KB         AS    KEIRI_KB         " & vbNewLine _
                                                & "    ,KMK.TAX_KB           AS    TAX_KB           " & vbNewLine _
                                                & "    ,KBN.KBN_NM1          AS    TAX_KB_NM        " & vbNewLine _
                                                & "    ,@BUSYO_CD            AS    BUSYO_CD         " & vbNewLine _
                                                & "    ,TEMP.KEISAN_TLGK     AS    KEISAN_TLGK      " & vbNewLine _
                                                & "    ,TEMP.NEBIKI_RT       AS    NEBIKI_RT        " & vbNewLine _
                                                & "    ,TEMP.NEBIKI_GK       AS    NEBIKI_GK        " & vbNewLine _
                                                & "    ,TEMP.TEKIYO          AS    TEKIYO           " & vbNewLine _
                                                & "    ,'01'                 AS    TEMPLATE_IMP_FLG " & vbNewLine _
                                                & "--真荷主 start                                   " & vbNewLine _
                                                & "    ,TEMP.TCUST_BPCD      AS    TCUST_BPCD       " & vbNewLine _
                                                & "    ,MBP.BP_NM1           AS    TCUST_BPNM       " & vbNewLine _
                                                & "--真荷主 end                                     " & vbNewLine _
                                                & "--セグメント初期値 start                         " & vbNewLine _
                                                & "    ,DEF.DEF_SEG_SEIHIN   AS    PRODUCT_SEG_CD   " & vbNewLine _
                                                & "    ,DEF.DEF_SEG_CHIIKI   AS    ORIG_SEG_CD      " & vbNewLine _
                                                & "    ,DEF.DEF_SEG_CHIIKI   AS    DEST_SEG_CD      " & vbNewLine _
                                                & "--セグメント初期値 end                           " & vbNewLine _
                                                & "--運賃横持科目分け start                         " & vbNewLine _
                                                & "    ,TEMP.SEIQKMK_CD_S    AS    SEIQKMK_CD_S     " & vbNewLine _
                                                & "--運賃横持科目分け end                           " & vbNewLine _
                                                & "FROM                                             " & vbNewLine _
                                                & "    $LM_MST$..M_SEIQ_TEMPLATE    TEMP            " & vbNewLine _
                                                & "LEFT JOIN       $LM_MST$..M_SEIQKMK       KMK    " & vbNewLine _
                                                & "ON     KMK.GROUP_KB       =    TEMP.GROUP_KB     " & vbNewLine _
                                                & "AND    KMK.SEIQKMK_CD     =    TEMP.SEIQKMK_CD   " & vbNewLine _
                                                & "--運賃横持科目分け start                         " & vbNewLine _
                                                & "AND    KMK.SEIQKMK_CD_S   =    TEMP.SEIQKMK_CD_S " & vbNewLine _
                                                & "--運賃横持科目分け end                           " & vbNewLine _
                                                & "AND    KMK.SYS_DEL_FLG    =    '0'               " & vbNewLine _
                                                & "LEFT JOIN       $LM_MST$..Z_KBN           KBN    " & vbNewLine _
                                                & "ON     KBN.KBN_GROUP_CD   =   'Z001'             " & vbNewLine _
                                                & "AND    KBN.KBN_CD         =    KMK.TAX_KB        " & vbNewLine _
                                                & "AND    KBN.SYS_DEL_FLG    =    '0'               " & vbNewLine _
                                                & "--真荷主取得 start                               " & vbNewLine _
                                                & "LEFT JOIN       ABM_DB..M_BP              MBP    " & vbNewLine _
                                                & "ON     MBP.BP_CD          =   TEMP.TCUST_BPCD    " & vbNewLine _
                                                & "AND    MBP.SYS_DEL_FLG    =    '0'               " & vbNewLine _
                                                & "--真荷主取得 end                                 " & vbNewLine _
                                                & "--セグメント初期値取得 start                     " & vbNewLine _
                                                & "LEFT JOIN                                        " & vbNewLine _
                                                & "  (                                              " & vbNewLine _
                                                & "    SELECT TOP 1                                 " & vbNewLine _
                                                & "       SSG.CNCT_SEG_CD AS DEF_SEG_SEIHIN         " & vbNewLine _
                                                & "      ,CSG.CNCT_SEG_CD AS DEF_SEG_CHIIKI         " & vbNewLine _
                                                & "    FROM                                         " & vbNewLine _
                                                & "      $LM_MST$..M_CUST MCT                       " & vbNewLine _
                                                & "    LEFT JOIN                                    " & vbNewLine _
                                                & "      ABM_DB..M_SEGMENT SSG                      " & vbNewLine _
                                                & "    ON                                           " & vbNewLine _
                                                & "      SSG.CNCT_SEG_CD = MCT.PRODUCT_SEG_CD       " & vbNewLine _
                                                & "      AND SSG.DATA_TYPE_CD = '00002'             " & vbNewLine _
                                                & "      AND SSG.KBN_LANG = @KBN_LANG               " & vbNewLine _
                                                & "      AND SSG.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                                & "    LEFT JOIN                                    " & vbNewLine _
                                                & "      $LM_MST$..M_SOKO SOKO                      " & vbNewLine _
                                                & "    ON                                           " & vbNewLine _
                                                & "      SOKO.WH_CD = MCT.DEFAULT_SOKO_CD           " & vbNewLine _
                                                & "      AND SOKO.SYS_DEL_FLG = '0'                 " & vbNewLine _
                                                & "    LEFT JOIN                                    " & vbNewLine _
                                                & "      ABM_DB..Z_KBN AKB                          " & vbNewLine _
                                                & "    ON                                           " & vbNewLine _
                                                & "      AKB.KBN_GROUP_CD = '" & ABM_DB_TODOFUKEN & "'                " & vbNewLine _
                                                & "      AND AKB.KBN_LANG = @KBN_LANG               " & vbNewLine _
                                                & "      AND AKB.KBN_NM3 = LEFT(SOKO.JIS_CD,2)         " & vbNewLine _
                                                & "      AND AKB.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                                & "    LEFT JOIN                                    " & vbNewLine _
                                                & "      ABM_DB..M_SEGMENT CSG                      " & vbNewLine _
                                                & "    ON                                           " & vbNewLine _
                                                & "      CSG.DATA_TYPE_CD = '00001'                 " & vbNewLine _
                                                & "      AND CSG.KBN_LANG = @KBN_LANG               " & vbNewLine _
                                                & "      AND CSG.KBN_GROUP_CD = AKB.KBN_GRP_REF1    " & vbNewLine _
                                                & "      AND CSG.KBN_CD = AKB.KBN_CD_REF1           " & vbNewLine _
                                                & "      AND CSG.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                                & "    WHERE                                        " & vbNewLine _
                                                & "      MCT.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                                & "      AND (   MCT.OYA_SEIQTO_CD = @SEIQTO_CD     " & vbNewLine _
                                                & "           OR MCT.HOKAN_SEIQTO_CD = @SEIQTO_CD   " & vbNewLine _
                                                & "           OR MCT.NIYAKU_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine _
                                                & "           OR MCT.UNCHIN_SEIQTO_CD = @SEIQTO_CD  " & vbNewLine _
                                                & "           OR MCT.SAGYO_SEIQTO_CD = @SEIQTO_CD)  " & vbNewLine _
                                                & "      AND MCT.SYS_DEL_FLG = '0'                  " & vbNewLine _
                                                & "    ORDER BY                                     " & vbNewLine _
                                                & "       MCT.CUST_CD_L                             " & vbNewLine _
                                                & "      ,MCT.CUST_CD_M                             " & vbNewLine _
                                                & "  ) DEF                                          " & vbNewLine _
                                                & "ON 1 = 1                                         " & vbNewLine _
                                                & "--セグメント初期値取得 end                       " & vbNewLine _
                                                & "WHERE                                            " & vbNewLine _
                                                & "    TEMP.NRS_BR_CD        =    @NRS_BR_CD        " & vbNewLine _
                                                & "AND    TEMP.SEIQTO_CD     =    @SEIQTO_CD        " & vbNewLine _
                                                & "AND    TEMP.SYS_DEL_FLG   =    '0'               " & vbNewLine

#End Region

#Region ""

    ''' <summary>
    ''' SEIQTOデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                          " & vbNewLine _
                                            & "	      SEIQTO.NRS_BR_CD                               AS NRS_BR_CD               " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQTO_CD                               AS SEIQTO_CD               " & vbNewLine _
                                            & "	     ,SEIQTO.TOTAL_MIN_SEIQ_AMT                      AS TOTAL_MIN_SEIQ_AMT      " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_TOTAL_FLG                       AS STORAGE_TOTAL_FLG       " & vbNewLine _
                                            & "	     ,SEIQTO.HANDLING_TOTAL_FLG                      AS HANDLING_TOTAL_FLG      " & vbNewLine _
                                            & "	     ,SEIQTO.UNCHIN_TOTAL_FLG                        AS UNCHIN_TOTAL_FLG        " & vbNewLine _
                                            & "	     ,SEIQTO.SAGYO_TOTAL_FLG                         AS SAGYO_TOTAL_FLG         " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQ_CURR_CD                            AS SEIQ_CURR_CD            " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_MIN_AMT                         AS STORAGE_MIN_AMT         " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_OTHER_MIN_AMT                   AS STORAGE_OTHER_MIN_AMT   " & vbNewLine _
                                            & "	     ,SEIQTO.HANDLING_MIN_AMT                        AS HANDLING_MIN_AMT        " & vbNewLine _
                                            & "	     ,SEIQTO.HANDLING_OTHER_MIN_AMT                  AS HANDLING_OTHER_MIN_AMT  " & vbNewLine _
                                            & "	     ,SEIQTO.UNCHIN_MIN_AMT                          AS UNCHIN_MIN_AMT          " & vbNewLine _
                                            & "	     ,SEIQTO.SAGYO_MIN_AMT                           AS SAGYO_MIN_AMT           " & vbNewLine _
                                            & "FROM                                                                             " & vbNewLine _
                                            & "     $LM_MST$..M_SEIQTO AS SEIQTO                                                " & vbNewLine _
                                            & "WHERE                                                                            " & vbNewLine _
                                            & "         SEIQTO.NRS_BR_CD  = @NRS_BR_CD                                          " & vbNewLine _
                                            & "     AND SEIQTO.SEIQTO_CD  = @SEIQTO_CD                                          " & vbNewLine

#End Region

#Region "荷主マスタ検索 SQL"
    ''' <summary>
    ''' 荷主マスタ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_CUST_HED As String = "SELECT                                                       " & vbNewLine _
                                            & "         KBN1.KBN_NM2               AS    SEIQKMK_CD             " & vbNewLine _
                                            & "        ,KMK.SEIQKMK_NM             AS    SEIQKMK_NM             " & vbNewLine _
                                            & "        ,KMK.KEIRI_KB               AS    KEIRI_KB               " & vbNewLine _
                                            & "        ,CST.TAX_KB                 AS    TAX_KB                 " & vbNewLine _
                                            & "        ,KBN2.KBN_NM1               AS    TAX_KB_NM              " & vbNewLine _
                                            & "        ,SOKO.WH_KB                 AS    JISYATASYA_KB          " & vbNewLine _
                                            & "        ,SEIQ.STORAGE_ZERO_FLG      AS    STORAGE_ZERO_FLG       " & vbNewLine _
                                            & "        ,SEIQ.HANDLING_ZERO_FLG     AS    HANDLING_ZERO_FLG      " & vbNewLine _
                                            & "FROM                                                             " & vbNewLine _
                                            & "    (                                                            " & vbNewLine _
                                            & "         SELECT                                                  " & vbNewLine _
                                            & "             TOP 1 *                                             " & vbNewLine _
                                            & "         FROM                                                    " & vbNewLine _
                                            & "             $LM_MST$..M_CUST                                    " & vbNewLine _
                                            & "         WHERE                                                   " & vbNewLine _
                                            & "             NRS_BR_CD        = @NRS_BR_CD                       " & vbNewLine _
                                            & "         AND SYS_DEL_FLG      = '0'                              " & vbNewLine

    Private Const SQL_SELECT_CUST_FROM_HOKAN_SEIQTO As String = "         AND HOKAN_SEIQTO_CD  = @SEIQTO_CD     " & vbNewLine

    Private Const SQL_SELECT_CUST_FROM_NIYAKU_SEIQTO As String = "        AND NIYAKU_SEIQTO_CD  = @SEIQTO_CD    " & vbNewLine

    Private Const SQL_SELECT_CUST_DTL As String = "         ORDER BY                                            " & vbNewLine _
                                            & "              NRS_BR_CD                                          " & vbNewLine _
                                            & "             ,CUST_CD_L                                          " & vbNewLine _
                                            & "             ,CUST_CD_M                                          " & vbNewLine _
                                            & "             ,CUST_CD_S                                          " & vbNewLine _
                                            & "             ,CUST_CD_SS                                         " & vbNewLine _
                                            & "         ) AS CST                                                " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SOKO AS SOKO                               " & vbNewLine _
                                            & "ON     CST.NRS_BR_CD = SOKO.NRS_BR_CD                            " & vbNewLine _
                                            & "AND    CST.DEFAULT_SOKO_CD = SOKO.WH_CD                          " & vbNewLine _
                                            & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN1                    " & vbNewLine _
                                            & "ON     KBN1.KBN_GROUP_CD    =    'S064'                          " & vbNewLine _
                                            & "AND    KBN1.KBN_NM1         =    @GROUP_KB                       " & vbNewLine _
                                            & "AND    KBN1.KBN_NM3         =    CST.TAX_KB                      " & vbNewLine _
                                            & "AND    KBN1.SYS_DEL_FLG     =    '0'                             " & vbNewLine _
                                            & "LEFT JOIN       $LM_MST$..M_SEIQKMK  AS  KMK                     " & vbNewLine _
                                            & "ON     KMK.GROUP_KB         =    KBN1.KBN_NM1                    " & vbNewLine _
                                            & "AND    KMK.SEIQKMK_CD       =    KBN1.KBN_NM2                    " & vbNewLine _
                                            & "AND    KMK.SYS_DEL_FLG      =    '0'                             " & vbNewLine _
                                            & "LEFT JOIN       $LM_MST$..Z_KBN      AS  KBN2                    " & vbNewLine _
                                            & "ON     KBN2.KBN_GROUP_CD    =    'Z001'                          " & vbNewLine _
                                            & "AND    KBN2.KBN_CD          =    CST.TAX_KB                      " & vbNewLine _
                                            & "AND    KBN2.SYS_DEL_FLG     =    '0'                             " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SEIQTO AS SEIQ                             " & vbNewLine _
                                            & "ON     SEIQ.NRS_BR_CD = CST.NRS_BR_CD                            " & vbNewLine _
                                            & "AND    SEIQ.SYS_DEL_FLG     =    '0'                             " & vbNewLine

    Private Const SQL_SELECT_CUST_FROM_HOKAN_SEIQTO2 As String =
                                              "AND SEIQ.SEIQTO_CD = CST.HOKAN_SEIQTO_CD                         "

    Private Const SQL_SELECT_CUST_FROM_NIYAKU_SEIQTO2 As String =
                                              "AND SEIQ.SEIQTO_CD = CST.NIYAKU_SEIQTO_CD                        "


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

#Region "保管料取込処理"

    ''' <summary>
    ''' 保管料移行データ存在チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>保管料移行データ存在チェック処理SQLの構築・発行</remarks>
    Private Function ChkExistHokanIkoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_CHK_EXIST_HOKAN_IKO_DATA)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamHokanNiyaku()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "ChkExistHokanIkoData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E404", New String() {"保管料"})
        End If
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 坪貸し料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>坪貸し料検索処理SQLの構築・発行</remarks>
    Private Function SelectHokanTuboData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_TUBOKASHI)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTubo()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectHokanTuboData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行、マッピング処理
        Call Me.MappingImportDt(ds, cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 保管料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>差額検索処理SQLの構築・発行</remarks>
    Private Function SelectHokanData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_HOKAN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamHokanHOKAN()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectHokanData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行、マッピング処理
        Call Me.MappingHokanDt(ds, cmd)

        Return ds

    End Function

#End Region

#Region "荷役料取込処理"

    ''' <summary>
    ''' 荷役料移行データ存在チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷役料移行データ存在チェック処理SQLの構築・発行</remarks>
    Private Function ChkExistNiyakuIkoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_CHK_EXIST_NIYAKU_IKO_DATA)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamHokanNiyaku()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "ChkExistNiyakuIkoData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E404", New String() {"荷役料"})
        End If
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 荷役料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>差額検索処理SQLの構築・発行</remarks>
    Private Function SelectNiyakuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_NIYAKU)       'SELECT句

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamHokanNiyaku()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectNiyakuData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行、マッピング処理
        Call Me.MappingNiyakuDt(ds, cmd)

        Return ds

    End Function

#End Region

#Region "運賃取込処理"

    ''' <summary>
    ''' 運賃確定チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃検索処理SQLの構築・発行</remarks>
    Private Function CheckUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_CHK_UNCHIN_KAKUTEI)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUnchin()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "CheckUnchin", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E266", New String() {"運賃取込"})
        End If
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃検索処理SQLの構築・発行</remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_UNCHIN)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUnchin2()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectUnchinData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行、マッピング処理
        Call Me.MappingImportDt(ds, cmd, True)

        Return ds

    End Function

#End Region

#Region "作業料取込処理"

    ''' <summary>
    ''' 作業料確定チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業料検索処理SQLの構築・発行</remarks>
    Private Function CheckSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_CHK_SAGYO_KAKUTEI)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSagyo()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "CheckSagyo", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E266", New String() {"作業料取込"})
        End If
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 作業料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>坪貸し料検索処理SQLの構築・発行</remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_SAGYO)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamSagyo()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectSagyoData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行、マッピング処理
        '★ UPD START 2011/09/06 SUGA
        'Call Me.MappingImportDt(ds, cmd)
        Call Me.MappingSagyoDt(ds, cmd)
        '★ UPD E N D 2011/09/06 SUGA

        Return ds

    End Function

#End Region

#Region "横持料取込処理"

    ''' <summary>
    ''' 横持料確定チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持料検索処理SQLの構築・発行</remarks>
    Private Function CheckYokomochi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_CHK_YOKOMOCHI_KAKUTEI)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamYokomochi()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "CheckYokomochi", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E266", New String() {"横持料取込"})
        End If
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 横持料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持料検索処理SQLの構築・発行</remarks>
    Private Function SelectYokomochiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_YOKOMOCHI)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamYokomochi2()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectYokomochiData", cmd)
        'タイムアウトの設定
        cmd.CommandTimeout = 3000
        'SQLの発行、マッピング処理
        Call Me.MappingImportDt(ds, cmd)

        Return ds

    End Function

#End Region

#Region "テンプレートマスタ取込処理"

    ''' <summary>
    ''' テンプレートマスタ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>テンプレートマスタ検索処理SQLの構築・発行</remarks>
    Private Function SelectTemplateData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_TEMPLATE)      'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamTemplate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectTemplateData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行、マッピング処理
        Call Me.MappingImportDt(ds, cmd)

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(保管/荷役/請求開始日　共通)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHokanNiyaku()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

        End With

    End Sub
    ''' <summary>
    ''' パラメータ設定モジュール(保管/荷役/請求開始日　)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHokanHOKAN()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD_KBN", .Item("NRS_BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(坪貸し取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTubo()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            'START YANAI 要望番号939
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号939
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(運賃取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUnchin()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNC_SKYU_DATE_FROM", .Item("UNC_SKYU_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 要望番号939
    ''' <summary>
    ''' パラメータ設定モジュール(運賃取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUnchin2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNC_SKYU_DATE_FROM", .Item("UNC_SKYU_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END YANAI 要望番号939

    ''' <summary>
    ''' パラメータ設定モジュール(作業料取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSagyo()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAG_SKYU_DATE_FROM", .Item("SAG_SKYU_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))  'ADD 20211201 026003
        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持料取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokomochi()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOK_SKYU_DATE_FROM", .Item("YOK_SKYU_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'START YANAI 要望番号939
    ''' <summary>
    ''' パラメータ設定モジュール(横持料取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokomochi2()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOK_SKYU_DATE_FROM", .Item("YOK_SKYU_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", .Item("SKYU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END YANAI 要望番号939

    ''' <summary>
    ''' パラメータ設定モジュール(テンプレートマスタ取り込み用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTemplate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_CD", .Item("BUSYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "マッピング処理"

    ''' <summary>
    ''' SQLの発行、マッピング処理を行う(LMG900_HOKAN)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub MappingHokanDt(ByVal ds As DataSet, ByVal cmd As SqlCommand)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("STORAGE_AMO_TTL", "STORAGE_AMO_TTL")
        map.Add("STORAGE_MIN", "STORAGE_MIN")
        map.Add("ANBUN_RATE", "ANBUN_RATE")
        map.Add("SEIQ_STORAGE_AMO_TTL", "SEIQ_STORAGE_AMO_TTL")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")
        map.Add("SUM_STORAGE_AMO_TTL", "SUM_STORAGE_AMO_TTL")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")           'ADD 2016/09/06 再保管対応
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
        map.Add("DEST_SEG_CD", "DEST_SEG_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG900_HOKAN")

    End Sub

    ''' <summary>
    ''' SQLの発行、マッピング処理を行う(LMG900_NIYAKU)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub MappingNiyakuDt(ByVal ds As DataSet, ByVal cmd As SqlCommand)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("HANDLING_AMO_TTL", "HANDLING_AMO_TTL")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")           'ADD 2016/09/06 再保管対応
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
        map.Add("DEST_SEG_CD", "DEST_SEG_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG900_NIYAKU")

    End Sub
    '★ ADD START 2011/09/06 SUGA

    ''' <summary>
    ''' SQLの発行、マッピング処理を行う(LMG900_SAGYO)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub MappingSagyoDt(ByVal ds As DataSet, ByVal cmd As SqlCommand)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")
        map.Add("SAGYO_REC_NO", "SAGYO_REC_NO")
        map.Add("SAGYO_GK", "SAGYO_GK")
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
        map.Add("DEST_SEG_CD", "DEST_SEG_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG900_SAGYO")

    End Sub
    '★ ADD E N D 2011/09/06 SUGA

    ''' <summary>
    ''' SQLの発行、マッピング処理を行う(LMG900_IMPORT)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub MappingImportDt(ByVal ds As DataSet, ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GROUP_KB", "GROUP_KB")
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("BUSYO_CD", "BUSYO_CD")
        map.Add("KEISAN_TLGK", "KEISAN_TLGK")
        map.Add("NEBIKI_RT", "NEBIKI_RT")
        map.Add("NEBIKI_GK", "NEBIKI_GK")
        map.Add("TEKIYO", "TEKIYO")
        map.Add("TEMPLATE_IMP_FLG", "TEMPLATE_IMP_FLG")
        'ADD 2016/09/7 再保管対応
        If setFlg.Equals(True) Then
            map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        End If
        map.Add("TCUST_BPCD", "TCUST_BPCD")
        map.Add("TCUST_BPNM", "TCUST_BPNM")
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("ORIG_SEG_CD", "ORIG_SEG_CD")
        map.Add("DEST_SEG_CD", "DEST_SEG_CD")
        map.Add("SEIQKMK_CD_S", "SEIQKMK_CD_S")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG900_IMPORT")

    End Sub

#End Region

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

#Region "請求先マスタ更新対象データ検索"
    ''' <summary>
    ''' 請求先マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeiqtoData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_DATA)

        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectSeiqtoData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        'TODO:ログを入れる
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMG900DAC", "SelectSeiqtoData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMG900DAC", "SelectSeiqtoData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMG900DAC", "SelectSeiqtoData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("TOTAL_MIN_SEIQ_AMT", "TOTAL_MIN_SEIQ_AMT")
        map.Add("STORAGE_TOTAL_FLG", "STORAGE_TOTAL_FLG")
        map.Add("HANDLING_TOTAL_FLG", "HANDLING_TOTAL_FLG")
        map.Add("UNCHIN_TOTAL_FLG", "UNCHIN_TOTAL_FLG")
        map.Add("SAGYO_TOTAL_FLG", "SAGYO_TOTAL_FLG")
        map.Add("SEIQ_CURR_CD", "SEIQ_CURR_CD")
        map.Add("STORAGE_MIN_AMT", "STORAGE_MIN_AMT")
        map.Add("STORAGE_OTHER_MIN_AMT", "STORAGE_OTHER_MIN_AMT")
        map.Add("HANDLING_MIN_AMT", "HANDLING_MIN_AMT")
        map.Add("HANDLING_OTHER_MIN_AMT", "HANDLING_OTHER_MIN_AMT")
        map.Add("UNCHIN_MIN_AMT", "UNCHIN_MIN_AMT")
        map.Add("SAGYO_MIN_AMT", "SAGYO_MIN_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG900_SEIQTO")

        Return ds

    End Function
#End Region

#Region "荷主マスタ検索"
    ''' <summary>
    ''' 荷主マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主マスタ取得SQLの構築・発行</remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG900IN_CUST")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG900DAC.SQL_SELECT_CUST_HED)

        '請求グループコード区分で検索する請求先を変更
        Select Case Me._Row.Item("GROUP_KB").ToString()
            Case "01"
                Me._StrSql.Append(LMG900DAC.SQL_SELECT_CUST_FROM_HOKAN_SEIQTO)
            Case Else
                Me._StrSql.Append(LMG900DAC.SQL_SELECT_CUST_FROM_NIYAKU_SEIQTO)
        End Select

        Me._StrSql.Append(LMG900DAC.SQL_SELECT_CUST_DTL)

        '請求グループコード区分で検索する請求先を変更
        Select Case Me._Row.Item("GROUP_KB").ToString()
            Case "01"
                Me._StrSql.Append(LMG900DAC.SQL_SELECT_CUST_FROM_HOKAN_SEIQTO2)
            Case Else
                Me._StrSql.Append(LMG900DAC.SQL_SELECT_CUST_FROM_NIYAKU_SEIQTO2)
        End Select

        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GROUP_KB", Me._Row.Item("GROUP_KB").ToString(), DBDataType.NVARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG900DAC", "SelectCustData", cmd)

        'タイムアウトの設定
        cmd.CommandTimeout = 3000

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQKMK_CD", "SEIQKMK_CD")
        map.Add("SEIQKMK_NM", "SEIQKMK_NM")
        map.Add("KEIRI_KB", "KEIRI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_KB_NM", "TAX_KB_NM")
        map.Add("JISYATASYA_KB", "JISYATASYA_KB")
        map.Add("STORAGE_ZERO_FLG", "STORAGE_ZERO_FLG")
        map.Add("HANDLING_ZERO_FLG", "HANDLING_ZERO_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG900_CUST")

        Return ds

    End Function
#End Region

#End Region

End Class
