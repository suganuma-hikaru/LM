' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM050DAC : 請求先マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM050DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM050DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(SEIQTO.SEIQTO_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' 非必須変更用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_BUSYO As String = "" _
                              & " SELECT                                          " & vbNewLine _
                              & "   KBN1.KBN_NM2 AS BUSYO_CD                      " & vbNewLine _
                              & " FROM                                            " & vbNewLine _
                              & "   $LM_MST$..Z_KBN     AS KBN1                   " & vbNewLine _
                              & " WHERE KBN1.KBN_GROUP_CD   = 'M032'              " & vbNewLine

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_VAR_STRAGE As String = "" _
                              & " SELECT                                    " & vbNewLine _
                              & "    CST.CUST_CD_L                          " & vbNewLine _
                              & "   ,CST.CUST_CD_M                          " & vbNewLine _
                              & "   ,CST.CUST_NM_L                          " & vbNewLine _
                              & "   ,CST.CUST_NM_M                          " & vbNewLine _
                              & " FROM                                      " & vbNewLine _
                              & "   LM_MST..M_CUST AS CST                   " & vbNewLine _
                              & " WHERE                                     " & vbNewLine _
                              & "       CST.NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
                              & "   AND CST.HOKAN_SEIQTO_CD = @SEIQTO_CD    " & vbNewLine _
                              & "   AND CST.SAITEI_HAN_KB <> '03'           " & vbNewLine _
                              & "   AND CST.SYS_DEL_FLG = '0'               " & vbNewLine

    'START YANAI 要望番号661
    '''' <summary>
    '''' SEIQTO_Mデータ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                                         " & vbNewLine _
    '                                        & "	      SEIQTO.NRS_BR_CD                               AS NRS_BR_CD                                                              " & vbNewLine _
    '                                        & "	     ,NRSBR.NRS_BR_NM                                AS NRS_BR_NM                                                              " & vbNewLine _
    '                                        & "	     ,SEIQTO.SEIQTO_CD                               AS SEIQTO_CD                                                              " & vbNewLine _
    '                                        & "	     ,SEIQTO.SEIQTO_NM                               AS SEIQTO_NM                                                              " & vbNewLine _
    '                                        & "	     ,SEIQTO.SEIQTO_BUSYO_NM                         AS SEIQTO_BUSYO_NM                                                        " & vbNewLine _
    '                                        & "	     ,SEIQTO.KOUZA_KB                                AS KOUZA_KB                                                               " & vbNewLine _
    '                                        & "	     ,BANK.MEIGI_NM +' ' + BANK.BANK_NM +' ' + BANK.YOKIN_SYU +' ' + BANK.KOZA_NO               AS KOUZA_KB_NM                 " & vbNewLine _
    '                                        & "	     ,SEIQTO.MEIGI_KB                                AS MEIGI_KB                                                               " & vbNewLine _
    '                                        & "	     ,SEIQTO.NRS_KEIRI_CD1                           AS NRS_KEIRI_CD1                                                          " & vbNewLine _
    '                                        & "	     ,SEIQTO.NRS_KEIRI_CD2                           AS NRS_KEIRI_CD2                                                          " & vbNewLine _
    '                                        & "	     ,SEIQTO.SEIQ_SND_PERIOD                         AS SEIQ_SND_PERIOD                                                        " & vbNewLine _
    '                                        & "	     ,SEIQTO.CUST_KAGAMI_TYPE1                       AS CUST_KAGAMI_TYPE1                                                      " & vbNewLine _
    '                                        & "	     ,SEIQTO.CUST_KAGAMI_TYPE2                       AS CUST_KAGAMI_TYPE2                                                      " & vbNewLine _
    '                                        & "	     ,SEIQTO.CUST_KAGAMI_TYPE3                       AS CUST_KAGAMI_TYPE3                                                      " & vbNewLine _
    '                                        & "	     ,SEIQTO.OYA_PIC                                 AS OYA_PIC                                                                " & vbNewLine _
    '                                        & "	     ,SEIQTO.TEL                                     AS TEL                                                                    " & vbNewLine _
    '                                        & "	     ,SEIQTO.FAX                                     AS FAX                                                                    " & vbNewLine _
    '                                        & "	     ,SEIQTO.CLOSE_KB                                AS CLOSE_KB                                                               " & vbNewLine _
    '                                        & "	     ,KBN1.KBN_NM1                                   AS CLOSE_KB_NM                                                            " & vbNewLine _
    '                                        & "	     ,SEIQTO.ZIP                                     AS ZIP                                                                    " & vbNewLine _
    '                                        & "	     ,SEIQTO.AD_1                                    AS AD_1                                                                   " & vbNewLine _
    '                                        & "	     ,SEIQTO.AD_2                                    AS AD_2                                                                   " & vbNewLine _
    '                                        & "	     ,SEIQTO.AD_3                                    AS AD_3                                                                   " & vbNewLine _
    '                                        & "	     ,SEIQTO.STORAGE_NR                              AS STORAGE_NR                                                             " & vbNewLine _
    '                                        & "	     ,SEIQTO.STORAGE_NG                              AS STORAGE_NG                                                             " & vbNewLine _
    '                                        & "	     ,SEIQTO.STORAGE_MIN                             AS STORAGE_MIN                                                            " & vbNewLine _
    '                                        & "	     ,SEIQTO.HANDLING_NR                             AS HANDLING_NR                                                            " & vbNewLine _
    '                                        & "	     ,SEIQTO.HANDLING_NG                             AS HANDLING_NG                                                            " & vbNewLine _
    '                                        & "	     ,SEIQTO.UNCHIN_NR                               AS UNCHIN_NR                                                              " & vbNewLine _
    '                                        & "	     ,SEIQTO.UNCHIN_NG                               AS UNCHIN_NG                                                              " & vbNewLine _
    '                                        & "	     ,SEIQTO.SAGYO_NR                                AS SAGYO_NR                                                               " & vbNewLine _
    '                                        & "	     ,SEIQTO.SAGYO_NG                                AS SAGYO_NG                                                               " & vbNewLine _
    '                                        & "	     ,SEIQTO.CLEARANCE_NR                            AS CLEARANCE_NR                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.CLEARANCE_NG                            AS CLEARANCE_NG                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.YOKOMOCHI_NR                            AS YOKOMOCHI_NR                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.YOKOMOCHI_NG                            AS YOKOMOCHI_NG                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.TOTAL_NR                                AS TOTAL_NR                                                               " & vbNewLine _
    '                                        & "	     ,SEIQTO.TOTAL_NG                                AS TOTAL_NG                                                               " & vbNewLine _
    '                                        & "	     ,SEIQTO.DOC_PTN                                 AS DOC_PTN                                                                " & vbNewLine _
    '                                        & "	     ,SEIQTO.DOC_SEI_YN                              AS DOC_SEI_YN                                                             " & vbNewLine _
    '                                        & "	     ,SEIQTO.DOC_HUKU_YN                             AS DOC_HUKU_YN                                                            " & vbNewLine _
    '                                        & "	     ,SEIQTO.DOC_HIKAE_YN                            AS DOC_HIKAE_YN                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.DOC_KEIRI_YN                            AS DOC_KEIRI_YN                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.SYS_ENT_DATE                            AS SYS_ENT_DATE                                                           " & vbNewLine _
    '                                        & "	     ,USER1.USER_NM                                  AS SYS_ENT_USER_NM                                                        " & vbNewLine _
    '                                        & "	     ,SEIQTO.SYS_UPD_DATE                            AS SYS_UPD_DATE                                                           " & vbNewLine _
    '                                        & "	     ,SEIQTO.SYS_UPD_TIME                            AS SYS_UPD_TIME                                                           " & vbNewLine _
    '                                        & "	     ,USER2.USER_NM                                  AS SYS_UPD_USER_NM                                                        " & vbNewLine _
    '                                        & "	     ,SEIQTO.SYS_DEL_FLG                             AS SYS_DEL_FLG                                                            " & vbNewLine _
    '                                        & "	     ,KBN2.KBN_NM1                                   AS SYS_DEL_NM                                                             " & vbNewLine
    ''' <summary>
    ''' SEIQTO_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                                                         " & vbNewLine _
                                            & "	      SEIQTO.NRS_BR_CD                               AS NRS_BR_CD                                                              " & vbNewLine _
                                            & "	     ,NRSBR.NRS_BR_NM                                AS NRS_BR_NM                                                              " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQTO_CD                               AS SEIQTO_CD                                                              " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQTO_NM                               AS SEIQTO_NM                                                              " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQTO_BUSYO_NM                         AS SEIQTO_BUSYO_NM                                                        " & vbNewLine _
                                            & "	     ,SEIQTO.KOUZA_KB                                AS KOUZA_KB                                                               " & vbNewLine _
                                            & "	     ,BANK.MEIGI_NM +' ' + BANK.BANK_NM +' ' + BANK.YOKIN_SYU +' ' + BANK.KOZA_NO               AS KOUZA_KB_NM                 " & vbNewLine _
                                            & "	     ,SEIQTO.MEIGI_KB                                AS MEIGI_KB                                                               " & vbNewLine _
                                            & "	     ,SEIQTO.NRS_KEIRI_CD1                           AS NRS_KEIRI_CD1                                                          " & vbNewLine _
                                            & "	     ,SEIQTO.NRS_KEIRI_CD2                           AS NRS_KEIRI_CD2                                                          " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQ_SND_PERIOD                         AS SEIQ_SND_PERIOD                                                        " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE1                       AS CUST_KAGAMI_TYPE1                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE2                       AS CUST_KAGAMI_TYPE2                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE3                       AS CUST_KAGAMI_TYPE3                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.OYA_PIC                                 AS OYA_PIC                                                                " & vbNewLine _
                                            & "	     ,SEIQTO.TEL                                     AS TEL                                                                    " & vbNewLine _
                                            & "	     ,SEIQTO.FAX                                     AS FAX                                                                    " & vbNewLine _
                                            & "	     ,SEIQTO.CLOSE_KB                                AS CLOSE_KB                                                               " & vbNewLine _
                                            & "	     ,KBN1.KBN_NM1                                   AS CLOSE_KB_NM                                                            " & vbNewLine _
                                            & "	     ,SEIQTO.ZIP                                     AS ZIP                                                                    " & vbNewLine _
                                            & "	     ,SEIQTO.AD_1                                    AS AD_1                                                                   " & vbNewLine _
                                            & "	     ,SEIQTO.AD_2                                    AS AD_2                                                                   " & vbNewLine _
                                            & "	     ,SEIQTO.AD_3                                    AS AD_3                                                                   " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_NR                              AS STORAGE_NR                                                             " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_NG                              AS STORAGE_NG                                                             " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_MIN                             AS STORAGE_MIN                                                            " & vbNewLine _
                                            & "	     ,SEIQTO.HANDLING_NR                             AS HANDLING_NR                                                            " & vbNewLine _
                                            & "	     ,SEIQTO.HANDLING_NG                             AS HANDLING_NG                                                            " & vbNewLine _
                                            & "	     ,SEIQTO.UNCHIN_NR                               AS UNCHIN_NR                                                              " & vbNewLine _
                                            & "	     ,SEIQTO.UNCHIN_NG                               AS UNCHIN_NG                                                              " & vbNewLine _
                                            & "	     ,SEIQTO.SAGYO_NR                                AS SAGYO_NR                                                               " & vbNewLine _
                                            & "	     ,SEIQTO.SAGYO_NG                                AS SAGYO_NG                                                               " & vbNewLine _
                                            & "	     ,SEIQTO.CLEARANCE_NR                            AS CLEARANCE_NR                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.CLEARANCE_NG                            AS CLEARANCE_NG                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.YOKOMOCHI_NR                            AS YOKOMOCHI_NR                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.YOKOMOCHI_NG                            AS YOKOMOCHI_NG                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.TOTAL_NR                                AS TOTAL_NR                                                               " & vbNewLine _
                                            & "	     ,SEIQTO.TOTAL_NG                                AS TOTAL_NG                                                               " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_PTN                                 AS DOC_PTN                                                                " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_PTN2                                AS DOC_PTN2                                                               " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_SEI_YN                              AS DOC_SEI_YN                                                             " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_HUKU_YN                             AS DOC_HUKU_YN                                                            " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_HIKAE_YN                            AS DOC_HIKAE_YN                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_KEIRI_YN                            AS DOC_KEIRI_YN                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.DOC_DEST_YN                             AS DOC_DEST_YN      --'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入                                                     " & vbNewLine _
                                            & "	     ,SEIQTO.SYS_ENT_DATE                            AS SYS_ENT_DATE                                                           " & vbNewLine _
                                            & "	     ,USER1.USER_NM                                  AS SYS_ENT_USER_NM                                                        " & vbNewLine _
                                            & "	     ,SEIQTO.SYS_UPD_DATE                            AS SYS_UPD_DATE                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.SYS_UPD_TIME                            AS SYS_UPD_TIME                                                           " & vbNewLine _
                                            & "	     ,USER2.USER_NM                                  AS SYS_UPD_USER_NM                                                        " & vbNewLine _
                                            & "	     ,SEIQTO.SYS_DEL_FLG                             AS SYS_DEL_FLG                                                            " & vbNewLine _
                                            & "	     ,KBN2.KBN_NM1                                   AS SYS_DEL_NM                                                             " & vbNewLine _
                                            & "	     --(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --                                                                " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE4                       AS CUST_KAGAMI_TYPE4                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE5                       AS CUST_KAGAMI_TYPE5                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE6                       AS CUST_KAGAMI_TYPE6                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE7                       AS CUST_KAGAMI_TYPE7                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE8                       AS CUST_KAGAMI_TYPE8                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.CUST_KAGAMI_TYPE9                       AS CUST_KAGAMI_TYPE9                                                      " & vbNewLine _
                                            & "	     --(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --                                                                " & vbNewLine _
                                            & "	     ,SEIQTO.SEIQ_CURR_CD                            AS SEIQ_CURR_CD                                                           " & vbNewLine _
                                            & "	     ,SEIQTO.TOTAL_MIN_SEIQ_AMT                      AS TOTAL_MIN_SEIQ_AMT                                                     " & vbNewLine _
                                            & "	     ,SEIQTO.STORAGE_TOTAL_FLG                       AS STORAGE_TOTAL_FLG                                                      " & vbNewLine _
                                            & "	     ,SEIQTO.HANDLING_TOTAL_FLG                      AS HANDLING_TOTAL_FLG                                                     " & vbNewLine _
                                            & "	     ,SEIQTO.UNCHIN_TOTAL_FLG                        AS UNCHIN_TOTAL_FLG                                                       " & vbNewLine _
                                            & "	     ,SEIQTO.SAGYO_TOTAL_FLG                         AS SAGYO_TOTAL_FLG                                                        " & vbNewLine _
                                            & "      ,STORAGE_MIN_AMT                                AS	STORAGE_MIN_AMT         	                                           " & vbNewLine _
                                            & "      ,STORAGE_OTHER_MIN_AMT                          AS STORAGE_OTHER_MIN_AMT                                                  " & vbNewLine _
                                            & "      ,HANDLING_MIN_AMT                               AS HANDLING_MIN_AMT         	                                           " & vbNewLine _
                                            & "      ,HANDLING_OTHER_MIN_AMT                         AS HANDLING_OTHER_MIN_AMT                                                 " & vbNewLine _
                                            & "      ,UNCHIN_MIN_AMT                                 AS	UNCHIN_MIN_AMT         	                                               " & vbNewLine _
                                            & "      ,SAGYO_MIN_AMT         	                     AS SAGYO_MIN_AMT         	                                               " & vbNewLine _
                                            & "      ,STORAGE_ZERO_FLG                               AS STORAGE_ZERO_FLG         	                                           " & vbNewLine _
                                            & "      ,HANDLING_ZERO_FLG                              AS HANDLING_ZERO_FLG         	                                           " & vbNewLine _
                                            & " --2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start                                            " & vbNewLine _
                                            & "      ,EIGYO_TANTO         	                         AS EIGYO_TANTO         	                                               " & vbNewLine _
                                            & " --2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end                                              " & vbNewLine _
                                            & "      ,REMARK         	                             AS REMARK     --ADD 20190710 002520                                       " & vbNewLine _
                                            & "      ,VAR_STRAGE_FLG   	                             AS VAR_STRAGE_FLG                                                         " & vbNewLine _
                                            & "      ,VAR_RATE_3    	                             AS VAR_RATE_3                                                             " & vbNewLine _
                                            & "      ,VAR_RATE_6    	                             AS VAR_RATE_6                                                             " & vbNewLine


    'END YANAI 要望番号661

#End Region

#Region "FROM句"

    Private Const SQL_FROM_DATA As String = "FROM                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_SEIQTO AS SEIQTO           " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER1             " & vbNewLine _
                                          & "        ON SEIQTO.SYS_ENT_USER = USER1.USER_CD               " & vbNewLine _
                                          & "       AND USER1.SYS_DEL_FLG   = '0'                         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..S_USER  AS USER2             " & vbNewLine _
                                          & "       ON SEIQTO.SYS_UPD_USER  = USER2.USER_CD               " & vbNewLine _
                                          & "       AND USER2.SYS_DEL_FLG   = '0'                         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR  AS NRSBR           " & vbNewLine _
                                          & "        ON SEIQTO.NRS_BR_CD    = NRSBR.NRS_BR_CD             " & vbNewLine _
                                          & "       AND NRSBR.SYS_DEL_FLG   = '0'                         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRSBANK AS BANK            " & vbNewLine _
                                          & "        ON BANK.MEIGI_CD       = SEIQTO.KOUZA_KB             " & vbNewLine _
                                          & "       AND BANK.SYS_DEL_FLG    = '0'                         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN3            " & vbNewLine _
                                          & "        ON SEIQTO.MEIGI_KB     = KBN3.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN3.KBN_GROUP_CD   = 'K011'                      " & vbNewLine _
                                          & "       AND KBN3.SYS_DEL_FLG    = '0'                         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN1            " & vbNewLine _
                                          & "        ON SEIQTO.CLOSE_KB     = KBN1.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN1.KBN_GROUP_CD   = 'S008'                      " & vbNewLine _
                                          & "       AND KBN1.SYS_DEL_FLG    = '0'                         " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..Z_KBN     AS KBN2            " & vbNewLine _
                                          & "        ON SEIQTO.SYS_DEL_FLG  = KBN2.KBN_CD                 " & vbNewLine _
                                          & "       AND KBN2.KBN_GROUP_CD   = 'S051'                      " & vbNewLine _
                                          & "       AND KBN2.SYS_DEL_FLG    = '0'                         " & vbNewLine


#End Region

#Region "ORDER BY"

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                               " & vbNewLine _
                                         & "     SEIQTO.SEIQTO_CD                                         " & vbNewLine

#End Region

#Region "共通"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "請求通貨コンボ取得"

    ''' <summary>
    ''' 請求通貨コンボ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COMBO_SEIQ_CURR_CD As String = "SELECT CURR_CD AS SEIQ_CURR_CD      " & vbNewLine _
                                         & "    FROM COM_DB..M_CURR                       " & vbNewLine _
                                         & "   WHERE SYS_DEL_FLG = '0'                    " & vbNewLine _
                                         & "     AND      UP_FLG = '00000'                " & vbNewLine _
                                         & "   AND  BASE_CURR_CD = (SELECT                " & vbNewLine _
                                         & "   CASE WHEN BASE_CURR_CD IS NULL             " & vbNewLine _
                                         & "          OR BASE_CURR_CD = '' THEN 'JPY'     " & vbNewLine _
                                         & "   ELSE BASE_CURR_CD END CURR_CD              " & vbNewLine _
                                         & "   FROM LM_MST..M_NRS_BR MNRS                 " & vbNewLine _
                                         & "   LEFT JOIN LM_MST..S_USER SU                " & vbNewLine _
                                         & "   ON MNRS.NRS_BR_CD = SU.NRS_BR_CD           " & vbNewLine _
                                         & "   WHERE SU.USER_CD = @USER_CD)               " & vbNewLine
#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 請求先コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_SEIQTO As String = "SELECT                            " & vbNewLine _
                                            & "   COUNT(SEIQTO_CD)  AS REC_CNT   " & vbNewLine _
                                            & "FROM $LM_MST$..M_SEIQTO           " & vbNewLine _
                                            & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                            & "  AND SEIQTO_CD    = @SEIQTO_CD   " & vbNewLine

    ''' <summary>
    ''' 郵便番号コード存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_ZIP As String = "SELECT                      " & vbNewLine _
                                         & "   COUNT(ZIP_NO)  AS REC_CNT" & vbNewLine _
                                         & "FROM $LM_MST$..M_ZIP        " & vbNewLine _
                                         & "WHERE ZIP_NO  = @ZIP        " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    'START YANAI 要望番号661
    '''' <summary>
    '''' 新規登録SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SEIQTO    " & vbNewLine _
    '                                   & "(                                 " & vbNewLine _
    '                                   & "      NRS_BR_CD               	" & vbNewLine _
    '                                   & "      ,SEIQTO_CD         		    " & vbNewLine _
    '                                   & "      ,SEIQTO_NM         		    " & vbNewLine _
    '                                   & "      ,SEIQTO_BUSYO_NM         	" & vbNewLine _
    '                                   & "      ,KOUZA_KB         		    " & vbNewLine _
    '                                   & "      ,MEIGI_KB         		    " & vbNewLine _
    '                                   & "      ,ZIP         			    " & vbNewLine _
    '                                   & "      ,AD_1        		 	    " & vbNewLine _
    '                                   & "      ,AD_2         			    " & vbNewLine _
    '                                   & "      ,AD_3         			    " & vbNewLine _
    '                                   & "      ,OYA_PIC         		    " & vbNewLine _
    '                                   & "      ,TEL         			    " & vbNewLine _
    '                                   & "      ,FAX         			    " & vbNewLine _
    '                                   & "      ,CLOSE_KB         		    " & vbNewLine _
    '                                   & "      ,DOC_PTN         		    " & vbNewLine _
    '                                   & "      ,DOC_SEI_YN         		" & vbNewLine _
    '                                   & "      ,DOC_HUKU_YN         		" & vbNewLine _
    '                                   & "      ,DOC_HIKAE_YN         		" & vbNewLine _
    '                                   & "      ,DOC_KEIRI_YN         		" & vbNewLine _
    '                                   & "      ,NRS_KEIRI_CD1         		" & vbNewLine _
    '                                   & "      ,NRS_KEIRI_CD2         		" & vbNewLine _
    '                                   & "      ,SEIQ_SND_PERIOD         	" & vbNewLine _
    '                                   & "      ,TOTAL_NR         		    " & vbNewLine _
    '                                   & "      ,STORAGE_NR         		" & vbNewLine _
    '                                   & "      ,HANDLING_NR         		" & vbNewLine _
    '                                   & "      ,UNCHIN_NR         		    " & vbNewLine _
    '                                   & "      ,SAGYO_NR         		    " & vbNewLine _
    '                                   & "      ,CLEARANCE_NR         		" & vbNewLine _
    '                                   & "      ,YOKOMOCHI_NR         		" & vbNewLine _
    '                                   & "      ,TOTAL_NG        		    " & vbNewLine _
    '                                   & "      ,STORAGE_NG         		" & vbNewLine _
    '                                   & "      ,HANDLING_NG         		" & vbNewLine _
    '                                   & "      ,UNCHIN_NG         		    " & vbNewLine _
    '                                   & "      ,SAGYO_NG         		    " & vbNewLine _
    '                                   & "      ,CLEARANCE_NG         		" & vbNewLine _
    '                                   & "      ,YOKOMOCHI_NG         		" & vbNewLine _
    '                                   & "      ,STORAGE_MIN         		" & vbNewLine _
    '                                   & "      ,CUST_KAGAMI_TYPE1         	" & vbNewLine _
    '                                   & "      ,CUST_KAGAMI_TYPE2         	" & vbNewLine _
    '                                   & "      ,CUST_KAGAMI_TYPE3         	" & vbNewLine _
    '                                   & "      ,SYS_ENT_DATE         		" & vbNewLine _
    '                                   & "      ,SYS_ENT_TIME         		" & vbNewLine _
    '                                   & "      ,SYS_ENT_PGID         		" & vbNewLine _
    '                                   & "      ,SYS_ENT_USER         		" & vbNewLine _
    '                                   & "      ,SYS_UPD_DATE         		" & vbNewLine _
    '                                   & "      ,SYS_UPD_TIME         		" & vbNewLine _
    '                                   & "      ,SYS_UPD_PGID         		" & vbNewLine _
    '                                   & "      ,SYS_UPD_USER         		" & vbNewLine _
    '                                   & "      ,SYS_DEL_FLG         		" & vbNewLine _
    '                                   & "      ) VALUES (                  " & vbNewLine _
    '                                   & "      @NRS_BR_CD                  " & vbNewLine _
    '                                   & "      ,@SEIQTO_CD         		" & vbNewLine _
    '                                   & "      ,@SEIQTO_NM         		" & vbNewLine _
    '                                   & "      ,@SEIQTO_BUSYO_NM         	" & vbNewLine _
    '                                   & "      ,@KOUZA_KB         		    " & vbNewLine _
    '                                   & "      ,@MEIGI_KB         		    " & vbNewLine _
    '                                   & "      ,@ZIP         			    " & vbNewLine _
    '                                   & "      ,@AD_1        		 	    " & vbNewLine _
    '                                   & "      ,@AD_2         			    " & vbNewLine _
    '                                   & "      ,@AD_3         			    " & vbNewLine _
    '                                   & "      ,@OYA_PIC         		    " & vbNewLine _
    '                                   & "      ,@TEL         			    " & vbNewLine _
    '                                   & "      ,@FAX         		        " & vbNewLine _
    '                                   & "      ,@CLOSE_KB         		    " & vbNewLine _
    '                                   & "      ,@DOC_PTN         		    " & vbNewLine _
    '                                   & "      ,@DOC_SEI_YN         		" & vbNewLine _
    '                                   & "      ,@DOC_HUKU_YN         		" & vbNewLine _
    '                                   & "      ,@DOC_HIKAE_YN         		" & vbNewLine _
    '                                   & "      ,@DOC_KEIRI_YN         		" & vbNewLine _
    '                                   & "      ,@NRS_KEIRI_CD1         	" & vbNewLine _
    '                                   & "      ,@NRS_KEIRI_CD2         	" & vbNewLine _
    '                                   & "      ,@SEIQ_SND_PERIOD         	" & vbNewLine _
    '                                   & "      ,@TOTAL_NR         		    " & vbNewLine _
    '                                   & "      ,@STORAGE_NR         		" & vbNewLine _
    '                                   & "      ,@HANDLING_NR         		" & vbNewLine _
    '                                   & "      ,@UNCHIN_NR         		" & vbNewLine _
    '                                   & "      ,@SAGYO_NR         		    " & vbNewLine _
    '                                   & "      ,@CLEARANCE_NR         		" & vbNewLine _
    '                                   & "      ,@YOKOMOCHI_NR         		" & vbNewLine _
    '                                   & "      ,@TOTAL_NG        		    " & vbNewLine _
    '                                   & "      ,@STORAGE_NG         		" & vbNewLine _
    '                                   & "      ,@HANDLING_NG         		" & vbNewLine _
    '                                   & "      ,@UNCHIN_NG         		" & vbNewLine _
    '                                   & "      ,@SAGYO_NG         		    " & vbNewLine _
    '                                   & "      ,@CLEARANCE_NG         		" & vbNewLine _
    '                                   & "      ,@YOKOMOCHI_NG         		" & vbNewLine _
    '                                   & "      ,@STORAGE_MIN         		" & vbNewLine _
    '                                   & "      ,@CUST_KAGAMI_TYPE1         " & vbNewLine _
    '                                   & "      ,@CUST_KAGAMI_TYPE2         " & vbNewLine _
    '                                   & "      ,@CUST_KAGAMI_TYPE3         " & vbNewLine _
    '                                   & "      ,@SYS_ENT_DATE         		" & vbNewLine _
    '                                   & "      ,@SYS_ENT_TIME         		" & vbNewLine _
    '                                   & "      ,@SYS_ENT_PGID         		" & vbNewLine _
    '                                   & "      ,@SYS_ENT_USER         		" & vbNewLine _
    '                                   & "      ,@SYS_UPD_DATE         		" & vbNewLine _
    '                                   & "      ,@SYS_UPD_TIME         		" & vbNewLine _
    '                                   & "      ,@SYS_UPD_PGID         		" & vbNewLine _
    '                                   & "      ,@SYS_UPD_USER         		" & vbNewLine _
    '                                   & "      ,@SYS_DEL_FLG         		" & vbNewLine _
    '                                   & ")                                 " & vbNewLine
    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_SEIQTO    " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "      NRS_BR_CD               	" & vbNewLine _
                                       & "      ,SEIQTO_CD         		    " & vbNewLine _
                                       & "      ,SEIQTO_NM         		    " & vbNewLine _
                                       & "      ,SEIQTO_BUSYO_NM         	" & vbNewLine _
                                       & "      ,KOUZA_KB         		    " & vbNewLine _
                                       & "      ,MEIGI_KB         		    " & vbNewLine _
                                       & "      ,ZIP         			    " & vbNewLine _
                                       & "      ,AD_1        		 	    " & vbNewLine _
                                       & "      ,AD_2         			    " & vbNewLine _
                                       & "      ,AD_3         			    " & vbNewLine _
                                       & "      ,OYA_PIC         		    " & vbNewLine _
                                       & "      ,TEL         			    " & vbNewLine _
                                       & "      ,FAX         			    " & vbNewLine _
                                       & "      ,CLOSE_KB         		    " & vbNewLine _
                                       & "      ,DOC_PTN         		    " & vbNewLine _
                                       & "      ,DOC_PTN2        		    " & vbNewLine _
                                       & "      ,DOC_SEI_YN         		" & vbNewLine _
                                       & "      ,DOC_HUKU_YN         		" & vbNewLine _
                                       & "      ,DOC_HIKAE_YN         		" & vbNewLine _
                                       & "      ,DOC_KEIRI_YN         		" & vbNewLine _
                                       & "      ,DOC_DEST_YN   -- 'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入      		" & vbNewLine _
                                       & "      ,NRS_KEIRI_CD1         		" & vbNewLine _
                                       & "      ,NRS_KEIRI_CD2         		" & vbNewLine _
                                       & "      ,SEIQ_SND_PERIOD         	" & vbNewLine _
                                       & "      ,TOTAL_NR         		    " & vbNewLine _
                                       & "      ,STORAGE_NR         		" & vbNewLine _
                                       & "      ,HANDLING_NR         		" & vbNewLine _
                                       & "      ,UNCHIN_NR         		    " & vbNewLine _
                                       & "      ,SAGYO_NR         		    " & vbNewLine _
                                       & "      ,CLEARANCE_NR         		" & vbNewLine _
                                       & "      ,YOKOMOCHI_NR         		" & vbNewLine _
                                       & "      ,TOTAL_NG        		    " & vbNewLine _
                                       & "      ,STORAGE_NG         		" & vbNewLine _
                                       & "      ,HANDLING_NG         		" & vbNewLine _
                                       & "      ,UNCHIN_NG         		    " & vbNewLine _
                                       & "      ,SAGYO_NG         		    " & vbNewLine _
                                       & "      ,CLEARANCE_NG         		" & vbNewLine _
                                       & "      ,YOKOMOCHI_NG         		" & vbNewLine _
                                       & "      ,STORAGE_MIN         		" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE1         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE2         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE3         	" & vbNewLine _
                                       & "--(2013.03.14)要望番号1950 START  " & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE4         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE5         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE6         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE7         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE8         	" & vbNewLine _
                                       & "      ,CUST_KAGAMI_TYPE9         	" & vbNewLine _
                                       & "--(2013.03.14)要望番号1950  END   " & vbNewLine _
                                       & "      ,SEIQ_CURR_CD         		" & vbNewLine _
                                       & "      ,TOTAL_MIN_SEIQ_AMT         " & vbNewLine _
                                       & "      ,STORAGE_TOTAL_FLG         	" & vbNewLine _
                                       & "      ,HANDLING_TOTAL_FLG         " & vbNewLine _
                                       & "      ,UNCHIN_TOTAL_FLG         	" & vbNewLine _
                                       & "      ,SAGYO_TOTAL_FLG         	" & vbNewLine _
                                       & "      ,STORAGE_MIN_AMT         	" & vbNewLine _
                                       & "      ,STORAGE_OTHER_MIN_AMT      " & vbNewLine _
                                       & "      ,HANDLING_MIN_AMT         	" & vbNewLine _
                                       & "      ,HANDLING_OTHER_MIN_AMT     " & vbNewLine _
                                       & "      ,UNCHIN_MIN_AMT         	" & vbNewLine _
                                       & "      ,SAGYO_MIN_AMT         	    " & vbNewLine _
                                       & "      ,STORAGE_ZERO_FLG      	    " & vbNewLine _
                                       & "      ,HANDLING_ZERO_FLG     	    " & vbNewLine _
                                       & "-- 2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start " & vbNewLine _
                                       & "      ,EIGYO_TANTO         	    " & vbNewLine _
                                       & "-- 2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end " & vbNewLine _
                                       & "      ,REMARK            	  --ADD 20190710 002520   " & vbNewLine _
                                       & "      ,VAR_STRAGE_FLG       		" & vbNewLine _
                                       & "      ,VAR_RATE_3            		" & vbNewLine _
                                       & "      ,VAR_RATE_6            		" & vbNewLine _
                                       & "      ,SYS_ENT_DATE         		" & vbNewLine _
                                       & "      ,SYS_ENT_TIME         		" & vbNewLine _
                                       & "      ,SYS_ENT_PGID         		" & vbNewLine _
                                       & "      ,SYS_ENT_USER         		" & vbNewLine _
                                       & "      ,SYS_UPD_DATE         		" & vbNewLine _
                                       & "      ,SYS_UPD_TIME         		" & vbNewLine _
                                       & "      ,SYS_UPD_PGID         		" & vbNewLine _
                                       & "      ,SYS_UPD_USER         		" & vbNewLine _
                                       & "      ,SYS_DEL_FLG         		" & vbNewLine _
                                       & "      ) VALUES (                  " & vbNewLine _
                                       & "      @NRS_BR_CD                  " & vbNewLine _
                                       & "      ,@SEIQTO_CD         		" & vbNewLine _
                                       & "      ,@SEIQTO_NM         		" & vbNewLine _
                                       & "      ,@SEIQTO_BUSYO_NM         	" & vbNewLine _
                                       & "      ,@KOUZA_KB         		    " & vbNewLine _
                                       & "      ,@MEIGI_KB         		    " & vbNewLine _
                                       & "      ,@ZIP         			    " & vbNewLine _
                                       & "      ,@AD_1        		 	    " & vbNewLine _
                                       & "      ,@AD_2         			    " & vbNewLine _
                                       & "      ,@AD_3         			    " & vbNewLine _
                                       & "      ,@OYA_PIC         		    " & vbNewLine _
                                       & "      ,@TEL         			    " & vbNewLine _
                                       & "      ,@FAX         		        " & vbNewLine _
                                       & "      ,@CLOSE_KB         		    " & vbNewLine _
                                       & "      ,@DOC_PTN         		    " & vbNewLine _
                                       & "      ,@DOC_PTN2        		    " & vbNewLine _
                                       & "      ,@DOC_SEI_YN         		" & vbNewLine _
                                       & "      ,@DOC_HUKU_YN         		" & vbNewLine _
                                       & "      ,@DOC_HIKAE_YN         		" & vbNewLine _
                                       & "      ,@DOC_KEIRI_YN         		" & vbNewLine _
                                       & "      ,@DOC_DEST_YN      --'ADD 2020/09/30 014230	" & vbNewLine _
                                       & "      ,@NRS_KEIRI_CD1         	" & vbNewLine _
                                       & "      ,@NRS_KEIRI_CD2         	" & vbNewLine _
                                       & "      ,@SEIQ_SND_PERIOD         	" & vbNewLine _
                                       & "      ,@TOTAL_NR         		    " & vbNewLine _
                                       & "      ,@STORAGE_NR         		" & vbNewLine _
                                       & "      ,@HANDLING_NR         		" & vbNewLine _
                                       & "      ,@UNCHIN_NR         		" & vbNewLine _
                                       & "      ,@SAGYO_NR         		    " & vbNewLine _
                                       & "      ,@CLEARANCE_NR         		" & vbNewLine _
                                       & "      ,@YOKOMOCHI_NR         		" & vbNewLine _
                                       & "      ,@TOTAL_NG        		    " & vbNewLine _
                                       & "      ,@STORAGE_NG         		" & vbNewLine _
                                       & "      ,@HANDLING_NG         		" & vbNewLine _
                                       & "      ,@UNCHIN_NG         		" & vbNewLine _
                                       & "      ,@SAGYO_NG         		    " & vbNewLine _
                                       & "      ,@CLEARANCE_NG         		" & vbNewLine _
                                       & "      ,@YOKOMOCHI_NG         		" & vbNewLine _
                                       & "      ,@STORAGE_MIN         		" & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE1         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE2         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE3         " & vbNewLine _
                                       & "--(2013.03.14)要望番号1950 START  " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE4         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE5         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE6         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE7         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE8         " & vbNewLine _
                                       & "      ,@CUST_KAGAMI_TYPE9         " & vbNewLine _
                                       & "--(2013.03.14)要望番号1950  END   " & vbNewLine _
                                       & "      ,@SEIQ_CURR_CD         		" & vbNewLine _
                                       & "      ,@TOTAL_MIN_SEIQ_AMT        " & vbNewLine _
                                       & "      ,@STORAGE_TOTAL_FLG         " & vbNewLine _
                                       & "      ,@HANDLING_TOTAL_FLG        " & vbNewLine _
                                       & "      ,@UNCHIN_TOTAL_FLG         	" & vbNewLine _
                                       & "      ,@SAGYO_TOTAL_FLG         	" & vbNewLine _
                                       & "      ,@STORAGE_MIN_AMT         	" & vbNewLine _
                                       & "      ,@STORAGE_OTHER_MIN_AMT     " & vbNewLine _
                                       & "      ,@HANDLING_MIN_AMT         	" & vbNewLine _
                                       & "      ,@HANDLING_OTHER_MIN_AMT    " & vbNewLine _
                                       & "      ,@UNCHIN_MIN_AMT         	" & vbNewLine _
                                       & "      ,@SAGYO_MIN_AMT         	" & vbNewLine _
                                       & "      ,@STORAGE_ZERO_FLG          " & vbNewLine _
                                       & "      ,@HANDLING_ZERO_FLG         " & vbNewLine _
                                       & "-- 2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start " & vbNewLine _
                                       & "      ,@EIGYO_TANTO         	    " & vbNewLine _
                                       & "-- 2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end " & vbNewLine _
                                       & "      ,@REMARK         --ADD 20190710 002520	         " & vbNewLine _
                                       & "      ,@VAR_STRAGE_FLG       		" & vbNewLine _
                                       & "      ,@VAR_RATE_3           		" & vbNewLine _
                                       & "      ,@VAR_RATE_6           		" & vbNewLine _
                                       & "      ,@SYS_ENT_DATE         		" & vbNewLine _
                                       & "      ,@SYS_ENT_TIME         		" & vbNewLine _
                                       & "      ,@SYS_ENT_PGID         		" & vbNewLine _
                                       & "      ,@SYS_ENT_USER         		" & vbNewLine _
                                       & "      ,@SYS_UPD_DATE         		" & vbNewLine _
                                       & "      ,@SYS_UPD_TIME         		" & vbNewLine _
                                       & "      ,@SYS_UPD_PGID         		" & vbNewLine _
                                       & "      ,@SYS_UPD_USER         		" & vbNewLine _
                                       & "      ,@SYS_DEL_FLG         		" & vbNewLine _
                                       & ")                                 " & vbNewLine
    'END YANAI 要望番号661

    'START YANAI 要望番号661
    '''' <summary>
    '''' 更新SQL
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SEIQTO SET                         " & vbNewLine _
    '                                   & "        SEIQTO_NM             = @SEIQTO_NM            " & vbNewLine _
    '                                   & "       ,SEIQTO_BUSYO_NM       = @SEIQTO_BUSYO_NM      " & vbNewLine _
    '                                   & "       ,KOUZA_KB              = @KOUZA_KB             " & vbNewLine _
    '                                   & "       ,MEIGI_KB              = @MEIGI_KB             " & vbNewLine _
    '                                   & "       ,ZIP                   = @ZIP                  " & vbNewLine _
    '                                   & "       ,AD_1                  = @AD_1                 " & vbNewLine _
    '                                   & "       ,AD_2                  = @AD_2                 " & vbNewLine _
    '                                   & "       ,AD_3                  = @AD_3                 " & vbNewLine _
    '                                   & "       ,OYA_PIC               = @OYA_PIC              " & vbNewLine _
    '                                   & "       ,TEL                   = @TEL                  " & vbNewLine _
    '                                   & "       ,FAX                   = @FAX                  " & vbNewLine _
    '                                   & "       ,CLOSE_KB              = @CLOSE_KB             " & vbNewLine _
    '                                   & "       ,DOC_PTN               = @DOC_PTN              " & vbNewLine _
    '                                   & "       ,DOC_SEI_YN            = @DOC_SEI_YN           " & vbNewLine _
    '                                   & "       ,DOC_HUKU_YN           = @DOC_HUKU_YN          " & vbNewLine _
    '                                   & "       ,DOC_HIKAE_YN          = @DOC_HIKAE_YN         " & vbNewLine _
    '                                   & "       ,DOC_KEIRI_YN          = @DOC_KEIRI_YN         " & vbNewLine _
    '                                   & "       ,NRS_KEIRI_CD1         = @NRS_KEIRI_CD1        " & vbNewLine _
    '                                   & "       ,NRS_KEIRI_CD2         = @NRS_KEIRI_CD2        " & vbNewLine _
    '                                   & "       ,SEIQ_SND_PERIOD       = @SEIQ_SND_PERIOD      " & vbNewLine _
    '                                   & "       ,TOTAL_NR              = @TOTAL_NR             " & vbNewLine _
    '                                   & "       ,STORAGE_NR            = @STORAGE_NR           " & vbNewLine _
    '                                   & "       ,HANDLING_NR           = @HANDLING_NR          " & vbNewLine _
    '                                   & "       ,UNCHIN_NR             = @UNCHIN_NR            " & vbNewLine _
    '                                   & "       ,SAGYO_NR              = @SAGYO_NR             " & vbNewLine _
    '                                   & "       ,CLEARANCE_NR          = @CLEARANCE_NR         " & vbNewLine _
    '                                   & "       ,YOKOMOCHI_NR          = @YOKOMOCHI_NR         " & vbNewLine _
    '                                   & "       ,TOTAL_NG              = @TOTAL_NG             " & vbNewLine _
    '                                   & "       ,STORAGE_NG            = @STORAGE_NG           " & vbNewLine _
    '                                   & "       ,HANDLING_NG           = @HANDLING_NG          " & vbNewLine _
    '                                   & "       ,UNCHIN_NG             = @UNCHIN_NG            " & vbNewLine _
    '                                   & "       ,SAGYO_NG              = @SAGYO_NG             " & vbNewLine _
    '                                   & "       ,CLEARANCE_NG          = @CLEARANCE_NG         " & vbNewLine _
    '                                   & "       ,YOKOMOCHI_NG          = @YOKOMOCHI_NG         " & vbNewLine _
    '                                   & "       ,STORAGE_MIN           = @STORAGE_MIN          " & vbNewLine _
    '                                   & "       ,CUST_KAGAMI_TYPE1     = @CUST_KAGAMI_TYPE1    " & vbNewLine _
    '                                   & "       ,CUST_KAGAMI_TYPE2     = @CUST_KAGAMI_TYPE2    " & vbNewLine _
    '                                   & "       ,CUST_KAGAMI_TYPE3     = @CUST_KAGAMI_TYPE3    " & vbNewLine _
    '                                   & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
    '                                   & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
    '                                   & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
    '                                   & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
    '                                   & " WHERE                                                " & vbNewLine _
    '                                   & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
    '                                   & " AND     SEIQTO_CD            = @SEIQTO_CD            " & vbNewLine
    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_SEIQTO SET                         " & vbNewLine _
                                       & "        SEIQTO_NM             = @SEIQTO_NM            " & vbNewLine _
                                       & "       ,SEIQTO_BUSYO_NM       = @SEIQTO_BUSYO_NM      " & vbNewLine _
                                       & "       ,KOUZA_KB              = @KOUZA_KB             " & vbNewLine _
                                       & "       ,MEIGI_KB              = @MEIGI_KB             " & vbNewLine _
                                       & "       ,ZIP                   = @ZIP                  " & vbNewLine _
                                       & "       ,AD_1                  = @AD_1                 " & vbNewLine _
                                       & "       ,AD_2                  = @AD_2                 " & vbNewLine _
                                       & "       ,AD_3                  = @AD_3                 " & vbNewLine _
                                       & "       ,OYA_PIC               = @OYA_PIC              " & vbNewLine _
                                       & "       ,TEL                   = @TEL                  " & vbNewLine _
                                       & "       ,FAX                   = @FAX                  " & vbNewLine _
                                       & "       ,CLOSE_KB              = @CLOSE_KB             " & vbNewLine _
                                       & "       ,DOC_PTN               = @DOC_PTN              " & vbNewLine _
                                       & "       ,DOC_PTN2              = @DOC_PTN2             " & vbNewLine _
                                       & "       ,DOC_SEI_YN            = @DOC_SEI_YN           " & vbNewLine _
                                       & "       ,DOC_HUKU_YN           = @DOC_HUKU_YN          " & vbNewLine _
                                       & "       ,DOC_HIKAE_YN          = @DOC_HIKAE_YN         " & vbNewLine _
                                       & "       ,DOC_KEIRI_YN          = @DOC_KEIRI_YN         " & vbNewLine _
                                       & "       ,DOC_DEST_YN           = @DOC_DEST_YN   --ADD 2020/09/30 014230      " & vbNewLine _
                                       & "       ,NRS_KEIRI_CD1         = @NRS_KEIRI_CD1        " & vbNewLine _
                                       & "       ,NRS_KEIRI_CD2         = @NRS_KEIRI_CD2        " & vbNewLine _
                                       & "       ,SEIQ_SND_PERIOD       = @SEIQ_SND_PERIOD      " & vbNewLine _
                                       & "       ,TOTAL_NR              = @TOTAL_NR             " & vbNewLine _
                                       & "       ,STORAGE_NR            = @STORAGE_NR           " & vbNewLine _
                                       & "       ,HANDLING_NR           = @HANDLING_NR          " & vbNewLine _
                                       & "       ,UNCHIN_NR             = @UNCHIN_NR            " & vbNewLine _
                                       & "       ,SAGYO_NR              = @SAGYO_NR             " & vbNewLine _
                                       & "       ,CLEARANCE_NR          = @CLEARANCE_NR         " & vbNewLine _
                                       & "       ,YOKOMOCHI_NR          = @YOKOMOCHI_NR         " & vbNewLine _
                                       & "       ,TOTAL_NG              = @TOTAL_NG             " & vbNewLine _
                                       & "       ,STORAGE_NG            = @STORAGE_NG           " & vbNewLine _
                                       & "       ,HANDLING_NG           = @HANDLING_NG          " & vbNewLine _
                                       & "       ,UNCHIN_NG             = @UNCHIN_NG            " & vbNewLine _
                                       & "       ,SAGYO_NG              = @SAGYO_NG             " & vbNewLine _
                                       & "       ,CLEARANCE_NG          = @CLEARANCE_NG         " & vbNewLine _
                                       & "       ,YOKOMOCHI_NG          = @YOKOMOCHI_NG         " & vbNewLine _
                                       & "       ,STORAGE_MIN           = @STORAGE_MIN          " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE1     = @CUST_KAGAMI_TYPE1    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE2     = @CUST_KAGAMI_TYPE2    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE3     = @CUST_KAGAMI_TYPE3    " & vbNewLine _
                                       & "--(2013.03.14)要望番号1950 -- START --                " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE4     = @CUST_KAGAMI_TYPE4    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE5     = @CUST_KAGAMI_TYPE5    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE6     = @CUST_KAGAMI_TYPE6    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE7     = @CUST_KAGAMI_TYPE7    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE8     = @CUST_KAGAMI_TYPE8    " & vbNewLine _
                                       & "       ,CUST_KAGAMI_TYPE9     = @CUST_KAGAMI_TYPE9    " & vbNewLine _
                                       & "--(2013.03.14)要望番号1950 --  END  --                " & vbNewLine _
                                       & "       ,SEIQ_CURR_CD          = @SEIQ_CURR_CD         " & vbNewLine _
                                       & "       ,TOTAL_MIN_SEIQ_AMT    = @TOTAL_MIN_SEIQ_AMT   " & vbNewLine _
                                       & "       ,STORAGE_TOTAL_FLG     = @STORAGE_TOTAL_FLG    " & vbNewLine _
                                       & "       ,HANDLING_TOTAL_FLG    = @HANDLING_TOTAL_FLG   " & vbNewLine _
                                       & "       ,UNCHIN_TOTAL_FLG      = @UNCHIN_TOTAL_FLG     " & vbNewLine _
                                       & "       ,SAGYO_TOTAL_FLG       = @SAGYO_TOTAL_FLG      " & vbNewLine _
                                       & "       ,STORAGE_MIN_AMT       = @STORAGE_MIN_AMT             " & vbNewLine _
                                       & "       ,STORAGE_OTHER_MIN_AMT = @STORAGE_OTHER_MIN_AMT       " & vbNewLine _
                                       & "       ,HANDLING_MIN_AMT      = @HANDLING_MIN_AMT            " & vbNewLine _
                                       & "       ,HANDLING_OTHER_MIN_AMT= @HANDLING_OTHER_MIN_AMT      " & vbNewLine _
                                       & "-- 2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start " & vbNewLine _
                                       & "       ,EIGYO_TANTO= @EIGYO_TANTO                           " & vbNewLine _
                                       & "-- 2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start " & vbNewLine _
                                       & "       ,REMARK                = @REMARK         --ADD 2019/07/10 002520 " & vbNewLine _
                                       & "       ,VAR_STRAGE_FLG        = @VAR_STRAGE_FLG              " & vbNewLine _
                                       & "       ,VAR_RATE_3            = @VAR_RATE_3                  " & vbNewLine _
                                       & "       ,VAR_RATE_6            = @VAR_RATE_6                  " & vbNewLine _
                                       & "       ,UNCHIN_MIN_AMT        = @UNCHIN_MIN_AMT              " & vbNewLine _
                                       & "       ,SAGYO_MIN_AMT         = @SAGYO_MIN_AMT               " & vbNewLine _
                                       & "       ,STORAGE_ZERO_FLG      = @STORAGE_ZERO_FLG     " & vbNewLine _
                                       & "       ,HANDLING_ZERO_FLG     = @HANDLING_ZERO_FLG    " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     SEIQTO_CD            = @SEIQTO_CD            " & vbNewLine
    'END YANAI 要望番号661

    ''' <summary>
    ''' 削除・復活SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE As String = "UPDATE $LM_MST$..M_SEIQTO SET                         " & vbNewLine _
                                       & "        SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = @SYS_DEL_FLG          " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     SEIQTO_CD            = @SEIQTO_CD            " & vbNewLine
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

#Region "起動時処理"

    ''' <summary>
    ''' JDE非必須ユーザ確認
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JDE非必須ユーザ確認</remarks>
    Private Function SelectBusyo(ByVal ds As DataSet) As DataSet

        'ログインユーザの営業所のIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050_BUSYO_CD")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM050DAC.SQL_SELECT_BUSYO)     'SQL構築

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMM050DAC", "SelectBusyo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'レコードをクリア
        ds.Tables("LMM050_BUSYO_CD").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("BUSYO_CD", "BUSYO_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM050_BUSYO_CD")

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 請求先マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM050DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM050DAC.SQL_FROM_DATA)        'SQL構築(カウント用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
       
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "SelectData", cmd)

        'SQLの発行
        'TODO:ログを入れる
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMM050DAC", "SelectCountData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMM050DAC", "SelectCountData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMM050DAC", "SelectCountData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 請求先マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM050DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM050DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        Me._StrSql.Append(LMM050DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "SelectListData", cmd)

        'SQLの発行
        'TODO:ログを入れる
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMM050DAC", "SelectListData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMM050DAC", "SelectListData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMM050DAC", "SelectListData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("SEIQTO_BUSYO_NM", "SEIQTO_BUSYO_NM")
        map.Add("KOUZA_KB", "KOUZA_KB")
        map.Add("KOUZA_KB_NM", "KOUZA_KB_NM")
        map.Add("MEIGI_KB", "MEIGI_KB")
        map.Add("NRS_KEIRI_CD1", "NRS_KEIRI_CD1")
        map.Add("NRS_KEIRI_CD2", "NRS_KEIRI_CD2")
        map.Add("SEIQ_SND_PERIOD", "SEIQ_SND_PERIOD")
        map.Add("CUST_KAGAMI_TYPE1", "CUST_KAGAMI_TYPE1")
        map.Add("CUST_KAGAMI_TYPE2", "CUST_KAGAMI_TYPE2")
        map.Add("CUST_KAGAMI_TYPE3", "CUST_KAGAMI_TYPE3")
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
        map.Add("CUST_KAGAMI_TYPE4", "CUST_KAGAMI_TYPE4")
        map.Add("CUST_KAGAMI_TYPE5", "CUST_KAGAMI_TYPE5")
        map.Add("CUST_KAGAMI_TYPE6", "CUST_KAGAMI_TYPE6")
        map.Add("CUST_KAGAMI_TYPE7", "CUST_KAGAMI_TYPE7")
        map.Add("CUST_KAGAMI_TYPE8", "CUST_KAGAMI_TYPE8")
        map.Add("CUST_KAGAMI_TYPE9", "CUST_KAGAMI_TYPE9")
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
        map.Add("OYA_PIC", "OYA_PIC")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("CLOSE_KB", "CLOSE_KB")
        map.Add("CLOSE_KB_NM", "CLOSE_KB_NM")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("STORAGE_NR", "STORAGE_NR")
        map.Add("STORAGE_NG", "STORAGE_NG")
        map.Add("STORAGE_MIN", "STORAGE_MIN")
        map.Add("HANDLING_NR", "HANDLING_NR")
        map.Add("HANDLING_NG", "HANDLING_NG")
        map.Add("UNCHIN_NR", "UNCHIN_NR")
        map.Add("UNCHIN_NG", "UNCHIN_NG")
        map.Add("SAGYO_NR", "SAGYO_NR")
        map.Add("SAGYO_NG", "SAGYO_NG")
        map.Add("CLEARANCE_NR", "CLEARANCE_NR")
        map.Add("CLEARANCE_NG", "CLEARANCE_NG")
        map.Add("YOKOMOCHI_NR", "YOKOMOCHI_NR")
        map.Add("YOKOMOCHI_NG", "YOKOMOCHI_NG")
        map.Add("TOTAL_NR", "TOTAL_NR")
        map.Add("TOTAL_NG", "TOTAL_NG")
        map.Add("DOC_PTN", "DOC_PTN")
        'START YANAI 要望番号661
        map.Add("DOC_PTN2", "DOC_PTN2")
        'END YANAI 要望番号661
        map.Add("DOC_SEI_YN", "DOC_SEI_YN")
        map.Add("DOC_HUKU_YN", "DOC_HUKU_YN")
        map.Add("DOC_HIKAE_YN", "DOC_HIKAE_YN")
        map.Add("DOC_KEIRI_YN", "DOC_KEIRI_YN")
        map.Add("DOC_DEST_YN", "DOC_DEST_YN")    'ADD 2020/09/30 014230 
        map.Add("SEIQ_CURR_CD", "SEIQ_CURR_CD")
        map.Add("TOTAL_MIN_SEIQ_AMT", "TOTAL_MIN_SEIQ_AMT")
        map.Add("STORAGE_TOTAL_FLG", "STORAGE_TOTAL_FLG")
        map.Add("HANDLING_TOTAL_FLG", "HANDLING_TOTAL_FLG")
        map.Add("UNCHIN_TOTAL_FLG", "UNCHIN_TOTAL_FLG")
        map.Add("SAGYO_TOTAL_FLG", "SAGYO_TOTAL_FLG")
        map.Add("STORAGE_MIN_AMT", "STORAGE_MIN_AMT")
        map.Add("STORAGE_OTHER_MIN_AMT", "STORAGE_OTHER_MIN_AMT")
        map.Add("HANDLING_MIN_AMT", "HANDLING_MIN_AMT")
        map.Add("HANDLING_OTHER_MIN_AMT", "HANDLING_OTHER_MIN_AMT")
        map.Add("UNCHIN_MIN_AMT", "UNCHIN_MIN_AMT")
        map.Add("SAGYO_MIN_AMT", "SAGYO_MIN_AMT")
        map.Add("STORAGE_ZERO_FLG", "STORAGE_ZERO_FLG")
        map.Add("HANDLING_ZERO_FLG", "HANDLING_ZERO_FLG")
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
        map.Add("EIGYO_TANTO", "EIGYO_TANTO")
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
        map.Add("REMARK", "REMARK")                     'ADD 2019/07/10 002520
        map.Add("VAR_STRAGE_FLG", "VAR_STRAGE_FLG")
        map.Add("VAR_RATE_3", "VAR_RATE_3")
        map.Add("VAR_RATE_6", "VAR_RATE_6")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM050OUT")

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
                andstr.Append(" (SEIQTO.SYS_DEL_FLG = @SYS_DEL_FLG  OR SEIQTO.SYS_DEL_FLG IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If


            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" (SEIQTO.NRS_BR_CD = @NRS_BR_CD OR SEIQTO.NRS_BR_CD IS NULL)")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTO.SEIQTO_CD LIKE @SEIQTO_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTO.SEIQTO_NM LIKE @SEIQTO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("SEIQTO_BUSYO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTO_BUSYO_NM LIKE @SEIQTO_BUSYO_NM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_BUSYO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            whereStr = .Item("KOUZA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" BANK.MEIGI_CD = @KOUZA_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOUZA_KB", whereStr, DBDataType.CHAR))
            End If

            whereStr = .Item("OYA_PIC").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" SEIQTO.OYA_PIC LIKE @OYA_PIC")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_PIC", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>JDE非必須ユーザ確認</remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        'ログインユーザの営業所のIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050_VAR_STRAGE")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM050DAC.SQL_SELECT_VAR_STRAGE)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "SelectVarStrage", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'レコードをクリア
        ds.Tables("LMM050_VAR_STRAGE").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM050_VAR_STRAGE")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 請求先マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMM050DAC.SQL_EXIT_SEIQTO)
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

        MyBase.Logger.WriteSQLLog("LMM050DAC", "SelectSeikyusakiM", cmd)

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
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM050DAC.SQL_EXIT_SEIQTO, Me._Row.Item("USER_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "CheckExistSeikyusakiM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    '2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks>郵便番号マスタ件数取得SQLの構築・発行</remarks>
    'Private Function CheckExistZipM(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables("LMM050IN")

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM050DAC.SQL_EXIT_ZIP, Me._Row.Item("USER_BR_CD").ToString()))

    '    Dim reader As SqlDataReader = Nothing

    '    'SQLパラメータ初期化/設定
    '    Call Me.SetParamZipExistChk()

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMM050DAC", "CheckExistZipM", cmd)

    '    'SQLの発行
    '    reader = MyBase.GetSelectResult(cmd)

    '    cmd.Parameters.Clear()

    '    '処理件数の設定
    '    reader.Read()
    '    MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
    '    reader.Close()

    '    Return ds

    'End Function

    '2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' 請求先マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM050DAC.SQL_INSERT, Me._Row.Item("USER_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "InsertSeikyusakiM", cmd)

        
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 請求先マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMM050DAC.SQL_UPDATE _
                                                                                     , LMM050DAC.SQL_COM_UPDATE_CONDITION) _
                                                                                     , Me._Row.Item("USER_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "UpdateSeikyusakiM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 請求先マスタ削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求先マスタ削除・復活SQLの構築・発行</remarks>
    Private Function DeleteSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM050IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMM050DAC.SQL_DELETE, Me._Row.Item("USER_BR_CD").ToString()))
        
        'SQLパラメータ初期化/設定
        Call Me.SetParamDelete()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "DeleteSeikyusakiM", cmd)

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
    ''' パラメータ設定モジュール(請求先マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))		'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(郵便番号マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamZipExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'ハイフン入力があれば除去
            If IsNumeric(.Item("ZIP").ToString()) = False Then
                Dim prmZip As String = String.Empty
                prmZip = System.Text.RegularExpressions.Regex.Replace(.Item("ZIP").ToString(), "[^0-9]", "")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", prmZip, DBDataType.NVARCHAR))
                Exit Sub
            End If

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))

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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))		'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", .Item("SEIQTO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_BUSYO_NM", .Item("SEIQTO_BUSYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KOUZA_KB", .Item("KOUZA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEIGI_KB", .Item("MEIGI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", .Item("ZIP").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", .Item("AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_2", .Item("AD_2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OYA_PIC", .Item("OYA_PIC").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", .Item("TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", .Item("FAX").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", .Item("CLOSE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_PTN", .Item("DOC_PTN").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号661
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_PTN2", .Item("DOC_PTN2").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号661
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_SEI_YN", .Item("DOC_SEI_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_HUKU_YN", .Item("DOC_HUKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_HIKAE_YN", .Item("DOC_HIKAE_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_KEIRI_YN", .Item("DOC_KEIRI_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOC_DEST_YN", .Item("DOC_DEST_YN").ToString(), DBDataType.CHAR))    'ADD 2020/09/30 014230
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_KEIRI_CD1", .Item("NRS_KEIRI_CD1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_KEIRI_CD2", .Item("NRS_KEIRI_CD2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_SND_PERIOD", .Item("SEIQ_SND_PERIOD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOTAL_NR", .Item("TOTAL_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_NR", .Item("STORAGE_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_NR", .Item("HANDLING_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_NR", .Item("UNCHIN_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NR", .Item("SAGYO_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLEARANCE_NR", .Item("CLEARANCE_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_NR", .Item("YOKOMOCHI_NR").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOTAL_NG", .Item("TOTAL_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_NG", .Item("STORAGE_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_NG", .Item("HANDLING_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_NG", .Item("UNCHIN_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NG", .Item("SAGYO_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLEARANCE_NG", .Item("CLEARANCE_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_NG", .Item("YOKOMOCHI_NG").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_MIN", .Item("STORAGE_MIN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE1", .Item("CUST_KAGAMI_TYPE1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE2", .Item("CUST_KAGAMI_TYPE2").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE3", .Item("CUST_KAGAMI_TYPE3").ToString(), DBDataType.NVARCHAR))
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE4", .Item("CUST_KAGAMI_TYPE4").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE5", .Item("CUST_KAGAMI_TYPE5").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE6", .Item("CUST_KAGAMI_TYPE6").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE7", .Item("CUST_KAGAMI_TYPE7").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE8", .Item("CUST_KAGAMI_TYPE8").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_KAGAMI_TYPE9", .Item("CUST_KAGAMI_TYPE9").ToString(), DBDataType.NVARCHAR))
            '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_CURR_CD", .Item("SEIQ_CURR_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_TOTAL_FLG", .Item("STORAGE_TOTAL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_TOTAL_FLG", .Item("HANDLING_TOTAL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TOTAL_FLG", .Item("UNCHIN_TOTAL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_TOTAL_FLG", .Item("SAGYO_TOTAL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOTAL_MIN_SEIQ_AMT", .Item("TOTAL_MIN_SEIQ_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_MIN_AMT", .Item("STORAGE_MIN_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_OTHER_MIN_AMT", .Item("STORAGE_OTHER_MIN_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_MIN_AMT", .Item("HANDLING_MIN_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_OTHER_MIN_AMT", .Item("HANDLING_OTHER_MIN_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_MIN_AMT", .Item("UNCHIN_MIN_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_MIN_AMT", .Item("SAGYO_MIN_AMT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_ZERO_FLG", .Item("STORAGE_ZERO_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_ZERO_FLG", .Item("HANDLING_ZERO_FLG").ToString(), DBDataType.CHAR))
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EIGYO_TANTO", .Item("EIGYO_TANTO").ToString(), DBDataType.NVARCHAR))
            '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))      'ADD 2019/07/10 002520
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VAR_STRAGE_FLG", .Item("VAR_STRAGE_FLG").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VAR_RATE_3", .Item("VAR_RATE_3").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VAR_RATE_6", .Item("VAR_RATE_6").ToString(), DBDataType.NUMERIC))
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))		'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
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

#Region "請求通貨コンボ取得"

    ''' <summary>
    ''' 請求通貨コンボ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求通貨コンボ取得</remarks>
    Private Function SelectComboSeiqCurrCd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM050DAC.SQL_COMBO_SEIQ_CURR_CD)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", Me.GetUserID(), DBDataType.NVARCHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM050DAC", "SelectComboSeiqCurrCd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQ_CURR_CD", "SEIQ_CURR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM050OUT")

        Return ds

    End Function
#End Region

#End Region

End Class
