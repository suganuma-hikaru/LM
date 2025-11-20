' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD530    : 日次出荷別在庫リスト（ＹＣＣサクラ限定）印刷
'  作  成  者       :  [hagimoto]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD530DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD530DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	ZAITRS.NRS_BR_CD                                         AS NRS_BR_CD " & vbNewLine _
                                            & ",'24'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine

    'START KISHI 要望番号No.633
    '''' <summary>
    '''' 印刷データ抽出用
    '''' </summary>
    '''' <remarks></remarks>
    '    Private Const SQL_SELECT_DATA As String = " SELECT                                              " & vbNewLine _
    '                                            & " ZAI.RPT_ID AS RPT_ID                                " & vbNewLine _
    '                                            & ",ZAI.NRS_BR_CD  AS NRS_BR_CD                         " & vbNewLine _
    '                                            & ",ZAI.CUST_CD_L  AS CUST_CD_L                         " & vbNewLine _
    '                                            & ",ZAI.CUST_CD_M  AS CUST_CD_M                         " & vbNewLine _
    '                                            & ",OUTKA.OUTKA_PLAN_DATE  AS OUTKA_DATE                " & vbNewLine _
    '                                            & ",ZAI.TOU_NO  AS TOU_NO                               " & vbNewLine _
    '                                            & ",ZAI.SITU_NO  AS SITU_NO                             " & vbNewLine _
    '                                            & ",ZAI.ZONE_CD  AS ZONE_CD                             " & vbNewLine _
    '                                            & ",ZAI.LOCA    AS LOCA                                 " & vbNewLine _
    '                                            & ",MG.GOODS_CD_CUST  AS GOODS_CD_CUST                  " & vbNewLine _
    '                                            & ",MG.GOODS_NM_1   AS GOODS_NM                         " & vbNewLine _
    '                                            & ",ZAI.LOT_NO  AS LOT_NO                               " & vbNewLine _
    '                                            & ",ZAI.SERIAL_NO  AS SERIAL_NO                         " & vbNewLine _
    '                                            & "--★2011/12 追加 要望番号=598対応 START--------------" & vbNewLine _
    '                                            & "--,ZAI.PORA_ZAI_NB  AS PORA_ZAI_NB                   " & vbNewLine _
    '                                            & "--,ZAI.ZAI_ALCTD_NB  AS ZAI_ALCTD_NB                 " & vbNewLine _
    '                                            & "--,ZAI.ALLOC_CAN_NB  AS ALLOC_CAN_NB                 " & vbNewLine _
    '                                            & "--★2011/12 追加 要望番号=598対応 END--------------- " & vbNewLine _
    '                                            & ",OUTKA.OUTKA_NO_L   AS OUTKA_NO_L                    " & vbNewLine _
    '                                            & ",OUTKA.OUTKA_NO_M   AS OUTKA_NO_M                    " & vbNewLine _
    '                                            & ",OUTKA.FREE_C07 AS FREE_C07                          " & vbNewLine _
    '                                            & ",OUTKA.DEST_CD   AS DEST_CD                          " & vbNewLine _
    '                                            & ",OUTKA.DEST_NM   AS DEST_NM                          " & vbNewLine _
    '                                            & ",OUTKA.OUTKA_ALCTD_NB  AS OUTKA_ALCTD_NB             " & vbNewLine _
    '                                            & "--★★2011/12 追加② 要望番号=598対応 START--------- " & vbNewLine _
    '                                            & ",OUTKA.GOODS_CD_NRS  AS GOODS_CD_NRS                 " & vbNewLine _
    '                                            & "--★★2011/12 追加② 要望番号=598対応 END------------" & vbNewLine _
    '                                            & "--★2011/12 追加 要望番号=598対応 START--------------" & vbNewLine _
    '                                            & "--入数                                               " & vbNewLine _
    '                                            & ",OUTKA.PKG_NB AS PKG_NB                              " & vbNewLine _
    '                                            & "--個数                                               " & vbNewLine _
    '                                            & ",OUTKA.KONSU AS KONSU                                " & vbNewLine _
    '                                            & "--端数                                               " & vbNewLine _
    '                                            & ",OUTKA.HASU AS HASU                                  " & vbNewLine _
    '                                            & "--★★2011/12 削除 要望番号=598対応 START------------" & vbNewLine _
    '                                            & "--出荷完了個数                                       " & vbNewLine _
    '                                            & "--,ISNULL(OUTEND.OUTEND_ALCTD_NB, 0) AS OUTEND_ALCTD_NB " & vbNewLine _
    '                                            & "--現在庫数（入荷個数 - 出荷完了個数）                   " & vbNewLine _
    '                                            & "--,ISNULL((OUTKA.KONSU * OUTKA.PKG_NB + OUTKA.HASU), 0)- ISNULL(OUTEND.OUTEND_ALCTD_NB, 0) AS GENZAIKO  " & vbNewLine _
    '                                            & "--引当中個数                                                                                            " & vbNewLine _
    '                                            & "--,ISNULL(HIKIATE.HIKIATE_ALCTD_NB, 0) AS HIKIATE_ALCTD_NB                                              " & vbNewLine _
    '                                            & "--引当可能個数（入荷個数 - (出荷完了個数 + 引当中個数)）                                                " & vbNewLine _
    '                                            & "--,ISNULL((OUTKA.KONSU * OUTKA.PKG_NB + OUTKA.HASU), 0)- (ISNULL(OUTEND.OUTEND_ALCTD_NB, 0) + ISNULL(HIKIATE.HIKIATE_ALCTD_NB, 0)) AS HIKIATECAN_ALCTD_NB " & vbNewLine _
    '                                            & "--★★2011/12 削除 要望番号=598対応 END--------------" & vbNewLine _
    '                                            & "--★2011/12 追加 要望番号=598対応 END--------------- " & vbNewLine _
    '                                            & "--★★2011/12 追加② 要望番号=598対応 START---------- " & vbNewLine _
    '                                            & "--合計出荷完了個数                                    " & vbNewLine _
    '                                            & ",ISNULL(SUMEND.SUMOUTEND_ALCTD_NB, 0) AS SUMOUTEND_ALCTD_NB  " & vbNewLine _
    '                                            & "--現在庫数（入荷個数 - 出荷完了個数）                      " & vbNewLine _
    '                                            & ",ISNULL((OUTKA.KONSU * OUTKA.PKG_NB + OUTKA.HASU), 0)- ISNULL(SUMEND.SUMOUTEND_ALCTD_NB, 0) AS GENZAIKO   " & vbNewLine _
    '                                            & "--合計引当中個数                                      " & vbNewLine _
    '                                            & ",ISNULL(SUMHIKIATE.SUMHIKIATE_ALCTD_NB, 0) AS SUMHIKIATE_ALCTD_NB  " & vbNewLine _
    '                                            & "--引当可能個数（入荷個数 - (出荷完了個数 + 引当中個数)）           " & vbNewLine _
    '                                            & ",ISNULL((OUTKA.KONSU * OUTKA.PKG_NB + OUTKA.HASU), 0)- (ISNULL(SUMEND.SUMOUTEND_ALCTD_NB, 0) + ISNULL(SUMHIKIATE.SUMHIKIATE_ALCTD_NB, 0)) AS HIKIATECAN_ALCTD_NB  " & vbNewLine _
    '                                            & "--★★2011/12 追加② 要望番号=598対応 END------------" & vbNewLine
    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
        Private Const SQL_SELECT_DATA As String = " SELECT                                                  " & vbNewLine _
                                                & "  OUTKA.RPT_ID AS RPT_ID                                 " & vbNewLine _
                                                & " ,OUTKA.NRS_BR_CD  AS NRS_BR_CD                          " & vbNewLine _
                                                & " ,OUTKA.CUST_CD_L  AS CUST_CD_L                          " & vbNewLine _
                                                & " ,OUTKA.CUST_CD_M  AS CUST_CD_M                          " & vbNewLine _
                                                & " ,OUTKA.OUTKA_PLAN_DATE  AS OUTKA_DATE                   " & vbNewLine _
                                                & " ,OUTKA.TOU_NO  AS TOU_NO                                " & vbNewLine _
                                                & " ,OUTKA.SITU_NO  AS SITU_NO                              " & vbNewLine _
                                                & " ,OUTKA.ZONE_CD  AS ZONE_CD                              " & vbNewLine _
                                                & " ,OUTKA.LOCA    AS LOCA                                  " & vbNewLine _
                                                & " ,MG.GOODS_CD_CUST  AS GOODS_CD_CUST                     " & vbNewLine _
                                                & " ,MG.GOODS_NM_1   AS GOODS_NM                            " & vbNewLine _
                                                & " ,OUTKA.LOT_NO  AS LOT_NO                                " & vbNewLine _
                                                & " ,OUTKA.SERIAL_NO  AS SERIAL_NO                          " & vbNewLine _
                                                & " ,OUTKA.OUTKA_NO_L   AS OUTKA_NO_L                       " & vbNewLine _
                                                & " ,OUTKA.OUTKA_NO_M   AS OUTKA_NO_M                       " & vbNewLine _
                                                & " ,OUTKA.FREE_C07 AS FREE_C07                             " & vbNewLine _
                                                & " ,OUTKA.DEST_CD   AS DEST_CD                             " & vbNewLine _
                                                & " ,OUTKA.DEST_NM   AS DEST_NM                             " & vbNewLine _
                                                & " ,OUTKA.OUTKA_ALCTD_NB  AS OUTKA_ALCTD_NB                " & vbNewLine _
                                                & " --現在庫                                                " & vbNewLine _
                                                & " ,ZAI.PORA_ZAI_NB  AS GENZAIKO                           " & vbNewLine _
                                                & " --引当中個数                                            " & vbNewLine _
                                                & " ,ZAI.ZAI_ALCTD_NB  AS SUMHIKIATE_ALCTD_NB               " & vbNewLine _
                                                & " --引当可能個数                                          " & vbNewLine _
                                                & " ,ZAI.ALLOC_CAN_NB  AS HIKIATECAN_ALCTD_NB               " & vbNewLine 

    'END KISHI 要望番号No.633

    ''' <summary>
    ''' データ抽出用FROM句(帳票種別取得用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                                                                                                  " & vbNewLine _
                                      & "$LM_TRN$..D_ZAI_TRS ZAITRS                                                                                           " & vbNewLine _
                                      & "LEFT OUTER JOIN                                                                                                      " & vbNewLine _
                                      & "$LM_MST$..M_GOODS GOODS ON                                                                                           " & vbNewLine _
                                      & "GOODS.NRS_BR_CD = @NRS_BR_CD                                                                                         " & vbNewLine _
                                      & "AND GOODS.GOODS_CD_NRS = ZAITRS.GOODS_CD_NRS                                                                         " & vbNewLine _
                                      & "--在庫の荷主での荷主帳票パターン取得                                                                                 " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                                                                  " & vbNewLine _
                                      & "ON  MCR1.NRS_BR_CD = @NRS_BR_CD                                                                                      " & vbNewLine _
                                      & "AND MCR1.CUST_CD_L = @CUST_CD_L                                                                                      " & vbNewLine _
                                      & "AND MCR1.CUST_CD_M = @CUST_CD_M                                                                                      " & vbNewLine _
                                      & "AND MCR1.PTN_ID = '24'                                                                                               " & vbNewLine _
                                      & "AND MCR1.CUST_CD_S = '00'                                                                                            " & vbNewLine _
                                      & "--帳票パターン取得                                                                                                   " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR1                                                                                        " & vbNewLine _
                                      & "ON  MR1.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                      & "AND MR1.PTN_ID = MCR1.PTN_ID                                                                                         " & vbNewLine _
                                      & "AND MR1.PTN_CD = MCR1.PTN_CD                                                                                         " & vbNewLine _
                                      & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "--商品Mの荷主での荷主帳票パターン取得                                                                                " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                                                                  " & vbNewLine _
                                      & "ON  MCR2.NRS_BR_CD = @NRS_BR_CD                                                                                      " & vbNewLine _
                                      & "AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                                                                                 " & vbNewLine _
                                      & "AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                                                                                 " & vbNewLine _
                                      & "AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                                                                                 " & vbNewLine _
                                      & "AND MCR2.PTN_ID = '24'                                                                                               " & vbNewLine _
                                      & "--帳票パターン取得                                                                                                   " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR2                                                                                        " & vbNewLine _
                                      & "ON  MR2.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                      & "AND MR2.PTN_ID = MCR2.PTN_ID                                                                                         " & vbNewLine _
                                      & "AND MR2.PTN_CD = MCR2.PTN_CD                                                                                         " & vbNewLine _
                                      & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "--存在しない場合の帳票パターン取得                                                                                   " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR3                                                                                        " & vbNewLine _
                                      & "ON  MR3.NRS_BR_CD = @NRS_BR_CD                                                                                       " & vbNewLine _
                                      & "AND MR3.PTN_ID = '24'                                                                                                " & vbNewLine _
                                      & "AND MR3.STANDARD_FLAG = '01'                                                                                         " & vbNewLine _
                                      & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                      & "WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                                                                                  " & vbNewLine _
                                      & "AND ZAITRS.CUST_CD_L = @CUST_CD_L                                                                                    " & vbNewLine _
                                      & "AND ZAITRS.CUST_CD_M = @CUST_CD_M             " & vbNewLine


    'START KISHI 要望番号No.633
    '''' <summary>
    '''' データ抽出用FROM句
    '''' </summary>
    '''' <remarks></remarks>
    '    Private Const SQL_FROM As String = "FROM                                                              " & vbNewLine _
    '                                     & "--在庫テーブル                                                    " & vbNewLine _
    '                                     & "(SELECT                                                           " & vbNewLine _
    '                                     & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                 " & vbNewLine _
    '                                     & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
    '                                     & "      ELSE MR3.RPT_ID END  AS RPT_ID                              " & vbNewLine _
    '                                     & ",ZAITRS.NRS_BR_CD AS NRS_BR_CD                                    " & vbNewLine _
    '                                     & ",ZAITRS.ZAI_REC_NO AS ZAI_REC_NO                                  " & vbNewLine _
    '                                     & ",ZAITRS.CUST_CD_L AS CUST_CD_L                                    " & vbNewLine _
    '                                     & ",ZAITRS.CUST_CD_M AS CUST_CD_M                                    " & vbNewLine _
    '                                     & ",ZAITRS.TOU_NO AS TOU_NO                                          " & vbNewLine _
    '                                     & ",ZAITRS.SITU_NO AS SITU_NO                                        " & vbNewLine _
    '                                     & ",ZAITRS.ZONE_CD AS ZONE_CD                                        " & vbNewLine _
    '                                     & ",ZAITRS.LOCA AS LOCA                                              " & vbNewLine _
    '                                     & ",ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS                              " & vbNewLine _
    '                                     & ",ZAITRS.LOT_NO AS LOT_NO                                          " & vbNewLine _
    '                                     & ",ZAITRS.SERIAL_NO        AS SERIAL_NO                             " & vbNewLine _
    '                                     & ",ZAITRS.PORA_ZAI_NB      AS PORA_ZAI_NB                           " & vbNewLine _
    '                                     & ",ZAITRS.ALCTD_NB         AS ZAI_ALCTD_NB                          " & vbNewLine _
    '                                     & ",ZAITRS.ALLOC_CAN_NB     AS ALLOC_CAN_NB                          " & vbNewLine _
    '                                     & "FROM                                                              " & vbNewLine _
    '                                     & "--在庫データ                                                      " & vbNewLine _
    '                                     & "$LM_TRN$..D_ZAI_TRS ZAITRS                                        " & vbNewLine _
    '                                     & "--商品M                                                           " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_GOODS GOODS                                 " & vbNewLine _
    '                                     & "ON  GOODS.NRS_BR_CD = ZAITRS.NRS_BR_CD                            " & vbNewLine _
    '                                     & "AND GOODS.GOODS_CD_NRS = ZAITRS.GOODS_CD_NRS                      " & vbNewLine _
    '                                     & "AND GOODS.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                     & "--在庫の荷主での荷主帳票パターン取得                              " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                               " & vbNewLine _
    '                                     & "ON  ZAITRS.NRS_BR_CD = MCR1.NRS_BR_CD                             " & vbNewLine _
    '                                     & "AND ZAITRS.CUST_CD_L = MCR1.CUST_CD_L                             " & vbNewLine _
    '                                     & "AND ZAITRS.CUST_CD_M = MCR1.CUST_CD_M                             " & vbNewLine _
    '                                     & "AND MCR1.PTN_ID = '24'                                            " & vbNewLine _
    '                                     & "AND '00' = MCR1.CUST_CD_S                                         " & vbNewLine _
    '                                     & "--帳票パターン取得                                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_RPT MR1                                     " & vbNewLine _
    '                                     & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                " & vbNewLine _
    '                                     & "AND MR1.PTN_ID = MCR1.PTN_ID                                      " & vbNewLine _
    '                                     & "AND MR1.PTN_CD = MCR1.PTN_CD                                      " & vbNewLine _
    '                                     & "AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                     & "--商品Mの荷主での荷主帳票パターン取得                             " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                               " & vbNewLine _
    '                                     & "ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                              " & vbNewLine _
    '                                     & "AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                              " & vbNewLine _
    '                                     & "AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                              " & vbNewLine _
    '                                     & "AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                              " & vbNewLine _
    '                                     & "AND MCR2.PTN_ID = '24'                                            " & vbNewLine _
    '                                     & "--帳票パターン取得                                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_RPT MR2                                     " & vbNewLine _
    '                                     & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                " & vbNewLine _
    '                                     & "AND MR2.PTN_ID = MCR2.PTN_ID                                      " & vbNewLine _
    '                                     & "AND MR2.PTN_CD = MCR2.PTN_CD                                      " & vbNewLine _
    '                                     & "AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                     & "--存在しない場合の帳票パターン取得                                " & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_RPT MR3                                     " & vbNewLine _
    '                                     & "ON  MR3.NRS_BR_CD = ZAITRS.NRS_BR_CD                              " & vbNewLine _
    '                                     & "AND MR3.PTN_ID = '24'                                             " & vbNewLine _
    '                                     & "AND MR3.STANDARD_FLAG = '01'                                      " & vbNewLine _
    '                                     & "AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
    '                                     & "WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
    '                                     & "AND ZAITRS.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
    '                                     & "AND ZAITRS.CUST_CD_M = @CUST_CD_M                                 " & vbNewLine _
    '                                     & "AND ZAITRS.SYS_DEL_FLG = '0'                                      " & vbNewLine _
    '                                     & " ) ZAI                                                            " & vbNewLine _
    '                                     & "--出荷テーブル                                                    " & vbNewLine _
    '                                     & "LEFT JOIN                                                         " & vbNewLine _
    '                                     & "(SELECT                                                           " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD  AS NRS_BR_CD                                  " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_L  AS CUST_CD_L                                  " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_M  AS CUST_CD_M                                  " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                      " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_NO_L  AS OUTKA_NO_L                                " & vbNewLine _
    '                                     & " ,OUTKAM.OUTKA_NO_M  AS OUTKA_NO_M                                " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO  AS ZAI_REC_NO                                " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD               " & vbNewLine _
    '                                     & "       ELSE OUTKAL.DEST_CD                                        " & vbNewLine _
    '                                     & "  END                       AS DEST_CD                            " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM             " & vbNewLine _
    '                                     & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM               " & vbNewLine _
    '                                     & "       ELSE DEST.DEST_NM                                          " & vbNewLine _
    '                                     & "  END                       AS DEST_NM                            " & vbNewLine _
    '                                     & " ,OUTKAM.GOODS_CD_NRS  AS GOODS_CD_NRS                            " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07 AS FREE_C07                                       " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO  AS LOT_NO                                        " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO  AS SERIAL_NO                                  " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO  AS TOU_NO                                        " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO AS SITU_NO                                       " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD AS ZONE_CD                                       " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA  AS LOCA                                            " & vbNewLine _
    '                                     & " ,SUM(OUTKAS.ALCTD_NB) AS OUTKA_ALCTD_NB                          " & vbNewLine _
    '                                     & "--★2011/12 追加 要望番号=598対応 START---------------------------" & vbNewLine _
    '                                     & ",GOODS.PKG_NB                                                     " & vbNewLine _
    '                                     & ",SUM(ISNULL(INKA_S.KONSU, 0)) AS KONSU                            " & vbNewLine _
    '                                     & ",SUM(ISNULL(INKA_S.HASU, 0)) AS HASU                              " & vbNewLine _
    '                                     & "--★2011/12 追加 要望番号=598対応 END---------------------------- " & vbNewLine _
    '                                     & " FROM                                                             " & vbNewLine _
    '                                     & " --出荷L                                                          " & vbNewLine _
    '                                     & " $LM_TRN$..C_OUTKA_L OUTKAL                                       " & vbNewLine _
    '                                     & " --出荷M                                                          " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " $LM_TRN$..C_OUTKA_M OUTKAM                                       " & vbNewLine _
    '                                     & " ON  OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                          " & vbNewLine _
    '                                     & " AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                        " & vbNewLine _
    '                                     & " AND OUTKAM.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & " --出荷S                                                          " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " $LM_TRN$..C_OUTKA_S OUTKAS                                       " & vbNewLine _
    '                                     & " ON  OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                          " & vbNewLine _
    '                                     & " AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                        " & vbNewLine _
    '                                     & " AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                        " & vbNewLine _
    '                                     & " AND OUTKAS.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & " --出荷EDIL                                                       " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " (SELECT                                                          " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                      " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
    '                                     & "  ,MIN(DEST_CD)  AS DEST_CD                                       " & vbNewLine _
    '                                     & "  ,MIN(DEST_NM)  AS DEST_NM                                       " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
    '                                     & "  FROM                                                            " & vbNewLine _
    '                                     & "  $LM_TRN$..H_OUTKAEDI_L                                          " & vbNewLine _
    '                                     & "  GROUP BY                                                        " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                      " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
    '                                     & " ) EDIL                                                           " & vbNewLine _
    '                                     & " ON  EDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD                            " & vbNewLine _
    '                                     & " AND EDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                        " & vbNewLine _
    '                                     & " AND EDIL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " (SELECT                                                          " & vbNewLine _
    '                                     & "  NRS_BR_CD                                                       " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO_CHU                                               " & vbNewLine _
    '                                     & "  ,FREE_C07                                                       " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
    '                                     & "  FROM                                                            " & vbNewLine _
    '                                     & "  $LM_TRN$..H_OUTKAEDI_M                                          " & vbNewLine _
    '                                     & "  GROUP BY                                                        " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                      " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO_CHU                                               " & vbNewLine _
    '                                     & "  ,FREE_C07                                                       " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
    '                                     & "  ) EDIM                                                          " & vbNewLine _
    '                                     & " ON EDIM.NRS_BR_CD = OUTKAM.NRS_BR_CD                             " & vbNewLine _
    '                                     & " AND EDIM.OUTKA_CTL_NO = OUTKAM.OUTKA_NO_L                        " & vbNewLine _
    '                                     & " AND EDIM.OUTKA_CTL_NO_CHU = OUTKAM.OUTKA_NO_M                    " & vbNewLine _
    '                                     & " AND EDIM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                     & " --商品M                                                          " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " $LM_MST$..M_GOODS GOODS                                          " & vbNewLine _
    '                                     & " ON  GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                           " & vbNewLine _
    '                                     & " AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                     " & vbNewLine _
    '                                     & " AND GOODS.SYS_DEL_FLG = '0'                                      " & vbNewLine _
    '                                     & " --届先M                                                          " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " $LM_MST$..M_DEST DEST                                            " & vbNewLine _
    '                                     & " ON  DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                            " & vbNewLine _
    '                                     & " AND DEST.CUST_CD_L = OUTKAL.CUST_CD_L                            " & vbNewLine _
    '                                     & " AND DEST.DEST_CD = OUTKAL.DEST_CD                                " & vbNewLine _
    '                                     & " AND DEST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
    '                                     & "--★2011/12 追加 要望番号=598対応 START---------------------------" & vbNewLine _
    '                                     & " --入荷S                                                          " & vbNewLine _
    '                                     & " LEFT JOIN                                                        " & vbNewLine _
    '                                     & " $LM_TRN$..B_INKA_S INKA_S                                   " & vbNewLine _
    '                                     & " ON  INKA_S.NRS_BR_CD = OUTKAS.NRS_BR_CD                          " & vbNewLine _
    '                                     & " AND INKA_S.INKA_NO_L = OUTKAS.INKA_NO_L                          " & vbNewLine _
    '                                     & " AND INKA_S.INKA_NO_M = OUTKAS.INKA_NO_M                          " & vbNewLine _
    '                                     & " AND INKA_S.INKA_NO_S = OUTKAS.INKA_NO_S                          " & vbNewLine _
    '                                     & " AND INKA_S.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 END----------------------------" & vbNewLine _
    '                                     & " WHERE                                                            " & vbNewLine _
    '                                     & "     OUTKAL.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
    '                                     & " AND OUTKAL.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
    '                                     & " AND OUTKAL.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_PLAN_DATE = @OUTKA_DATE                         " & vbNewLine _
    '                                     & " AND OUTKAL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & " GROUP BY                                                         " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD                                                " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_L                                                " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_M                                                " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE                                          " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_NO_L                                               " & vbNewLine _
    '                                     & " ,OUTKAM.OUTKA_NO_M                                               " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO                                               " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD               " & vbNewLine _
    '                                     & "       ELSE OUTKAL.DEST_CD                                        " & vbNewLine _
    '                                     & "  END                                                             " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM             " & vbNewLine _
    '                                     & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM               " & vbNewLine _
    '                                     & "       ELSE DEST.DEST_NM                                          " & vbNewLine _
    '                                     & "  END                                                             " & vbNewLine _
    '                                     & " ,OUTKAM.GOODS_CD_NRS                                             " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO                                                   " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO                                                " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO                                                   " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO                                                  " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD                                                  " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA                                                     " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07                                                   " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 START--------------------------" & vbNewLine _
    '                                     & " ,GOODS.PKG_NB                                                   " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 END-------------------------------------- " & vbNewLine _
    '                                     & "UNION ALL                                                                    " & vbNewLine _
    '                                     & " SELECT                                                                      " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD  AS NRS_BR_CD                                             " & vbNewLine _
    '                                     & " ,FURI.CUST_CD_L  AS CUST_CD_L                                               " & vbNewLine _
    '                                     & " ,FURI.CUST_CD_M  AS CUST_CD_M                                               " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                                 " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_NO_L  AS OUTKA_NO_L                                           " & vbNewLine _
    '                                     & " ,OUTKAM.OUTKA_NO_M  AS OUTKA_NO_M                                           " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO  AS ZAI_REC_NO                                           " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD                          " & vbNewLine _
    '                                     & "       ELSE OUTKAL.DEST_CD                                                   " & vbNewLine _
    '                                     & "  END                       AS DEST_CD                                       " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                        " & vbNewLine _
    '                                     & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM                          " & vbNewLine _
    '                                     & "       ELSE DEST.DEST_NM                                                     " & vbNewLine _
    '                                     & "  END                       AS DEST_NM                                       " & vbNewLine _
    '                                     & " ,FURI.GOODS_CD_NRS  AS GOODS_CD_NRS                                         " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07 AS FREE_C07                                                  " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO  AS LOT_NO                                                   " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO  AS SERIAL_NO                                             " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO  AS TOU_NO                                                   " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO AS SITU_NO                                                  " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD AS ZONE_CD                                                  " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA  AS LOCA                                                       " & vbNewLine _
    '                                     & " ,SUM(OUTKAS.ALCTD_NB) AS OUTKA_ALCTD_NB                                     " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 START-------------------------------------" & vbNewLine _
    '                                     & "  ,GOODS.PKG_NB                                                              " & vbNewLine _
    '                                     & "  ,SUM(ISNULL(INKA_S.KONSU, 0)) AS KONSU                                     " & vbNewLine _
    '                                     & "  ,SUM(ISNULL(INKA_S.HASU, 0)) AS HASU                                       " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 END-------------------------------------- " & vbNewLine _
    '                                     & "           FROM                                                              " & vbNewLine _
    '                                     & "             (SELECT DISTINCT                                                " & vbNewLine _
    '                                     & "               NRS_BR_CD               AS NRS_BR_CD                          " & vbNewLine _
    '                                     & "              ,GOODS_CD_NRS            AS GOODS_CD_NRS                       " & vbNewLine _
    '                                     & "              ,CUST_CD_L               AS CUST_CD_L                          " & vbNewLine _
    '                                     & "              ,CUST_CD_M               AS CUST_CD_M                          " & vbNewLine _
    '                                     & "              ,ALCTD_GOODS_CD          AS ALCTD_GOODS_CD                     " & vbNewLine _
    '                                     & "              ,ALCTD_CUST_CD_L         AS ALCTD_CUST_CD_L                    " & vbNewLine _
    '                                     & "              ,ALCTD_CUST_CD_M         AS ALCTD_CUST_CD_M                    " & vbNewLine _
    '                                     & "              FROM                                                           " & vbNewLine _
    '                                     & "                (SELECT                                                      " & vbNewLine _
    '                                     & "                  F1.NRS_BR_CD         AS NRS_BR_CD                          " & vbNewLine _
    '                                     & "                 ,F1.CD_NRS_TO         AS GOODS_CD_NRS                       " & vbNewLine _
    '                                     & "                 ,GTO1.CUST_CD_L       AS CUST_CD_L                          " & vbNewLine _
    '                                     & "                 ,GTO1.CUST_CD_M       AS CUST_CD_M                          " & vbNewLine _
    '                                     & "                 ,F1.CD_NRS_TO         AS ALCTD_GOODS_CD                     " & vbNewLine _
    '                                     & "                 ,G1.CUST_CD_L         AS ALCTD_CUST_CD_L                    " & vbNewLine _
    '                                     & "                 ,G1.CUST_CD_M         AS ALCTD_CUST_CD_M                    " & vbNewLine _
    '                                     & "                 FROM                                                        " & vbNewLine _
    '                                     & "                 $LM_MST$..M_FURI_GOODS F1                                   " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS GTO1                            " & vbNewLine _
    '                                     & "                 ON  GTO1.SYS_DEL_FLG = '0'                                  " & vbNewLine _
    '                                     & "                 AND GTO1.NRS_BR_CD = F1.NRS_BR_CD                           " & vbNewLine _
    '                                     & "                 AND GTO1.GOODS_CD_NRS = F1.CD_NRS_TO                        " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS G1                              " & vbNewLine _
    '                                     & "                 ON  G1.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "                 AND G1.NRS_BR_CD = F1.NRS_BR_CD                             " & vbNewLine _
    '                                     & "                 AND G1.GOODS_CD_NRS = F1.CD_NRS                             " & vbNewLine _
    '                                     & "                 WHERE                                                       " & vbNewLine _
    '                                     & "                 F1.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                     & "                 UNION ALL                                                   " & vbNewLine _
    '                                     & "                 SELECT                                                      " & vbNewLine _
    '                                     & "                  F2.NRS_BR_CD         AS NRS_BR_CD                          " & vbNewLine _
    '                                     & "                 ,F2.CD_NRS_TO         AS GOODS_CD_NRS                       " & vbNewLine _
    '                                     & "                 ,GTO2.CUST_CD_L       AS CUST_CD_L                          " & vbNewLine _
    '                                     & "                 ,GTO2.CUST_CD_M       AS CUST_CD_M                          " & vbNewLine _
    '                                     & "                 ,F2.CD_NRS            AS ALCTD_GOODS_CD                     " & vbNewLine _
    '                                     & "                 ,GTO2.CUST_CD_L       AS ALCTD_CUST_CD_L                    " & vbNewLine _
    '                                     & "                 ,GTO2.CUST_CD_M       AS ALCTD_CUST_CD_M                    " & vbNewLine _
    '                                     & "                 FROM                                                        " & vbNewLine _
    '                                     & "                 $LM_MST$..M_FURI_GOODS F2                                   " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS GTO2                            " & vbNewLine _
    '                                     & "                 ON  GTO2.SYS_DEL_FLG = '0'                                  " & vbNewLine _
    '                                     & "                 AND GTO2.NRS_BR_CD = F2.NRS_BR_CD                           " & vbNewLine _
    '                                     & "                 AND GTO2.GOODS_CD_NRS = F2.CD_NRS_TO                        " & vbNewLine _
    '                                     & "                 WHERE                                                       " & vbNewLine _
    '                                     & "                 F2.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                     & "                 UNION ALL                                                   " & vbNewLine _
    '                                     & "                 SELECT                                                      " & vbNewLine _
    '                                     & "                  F3.NRS_BR_CD         AS NRS_BR_CD                          " & vbNewLine _
    '                                     & "                 ,F3.CD_NRS_TO         AS GOODS_CD_NRS                       " & vbNewLine _
    '                                     & "                 ,GTO3.CUST_CD_L       AS CUST_CD_L                          " & vbNewLine _
    '                                     & "                 ,GTO3.CUST_CD_M       AS CUST_CD_M                          " & vbNewLine _
    '                                     & "                 ,F3.CD_NRS            AS ALCTD_GOODS_CD                     " & vbNewLine _
    '                                     & "                 ,G3.CUST_CD_L         AS ALCTD_CUST_CD_L                    " & vbNewLine _
    '                                     & "                 ,G3.CUST_CD_M         AS ALCTD_CUST_CD_M                    " & vbNewLine _
    '                                     & "                 FROM                                                        " & vbNewLine _
    '                                     & "                 $LM_MST$..M_FURI_GOODS F3                                   " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS GTO3                            " & vbNewLine _
    '                                     & "                 ON  GTO3.SYS_DEL_FLG = '0'                                  " & vbNewLine _
    '                                     & "                 AND GTO3.NRS_BR_CD = F3.NRS_BR_CD                           " & vbNewLine _
    '                                     & "                 AND GTO3.GOODS_CD_NRS = F3.CD_NRS_TO                        " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS G3                              " & vbNewLine _
    '                                     & "                 ON  G3.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "                 AND G3.NRS_BR_CD = F3.NRS_BR_CD                             " & vbNewLine _
    '                                     & "                 AND G3.GOODS_CD_NRS = F3.CD_NRS                             " & vbNewLine _
    '                                     & "                 WHERE                                                       " & vbNewLine _
    '                                     & "                 F3.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                     & "                 UNION ALL                                                   " & vbNewLine _
    '                                     & "                 SELECT                                                      " & vbNewLine _
    '                                     & "                 F4.NRS_BR_CD        AS NRS_BR_CD                            " & vbNewLine _
    '                                     & "                 ,F4.CD_NRS          AS GOODS_CD_NRS                         " & vbNewLine _
    '                                     & "                 ,GTO4.CUST_CD_L     AS CUST_CD_L                            " & vbNewLine _
    '                                     & "                 ,GTO4.CUST_CD_M     AS CUST_CD_M                            " & vbNewLine _
    '                                     & "                 ,F4.CD_NRS_TO       AS ALCTD_GOODS_CD                       " & vbNewLine _
    '                                     & "                 ,GTO4.CUST_CD_L     AS ALCTD_CUST_CD_L                      " & vbNewLine _
    '                                     & "                 ,GTO4.CUST_CD_M     AS ALCTD_CUST_CD_M                      " & vbNewLine _
    '                                     & "                 FROM                                                        " & vbNewLine _
    '                                     & "                 $LM_MST$..M_FURI_GOODS F4                                   " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS GTO4                            " & vbNewLine _
    '                                     & "                 ON  GTO4.SYS_DEL_FLG = '0'                                  " & vbNewLine _
    '                                     & "                 AND GTO4.NRS_BR_CD = F4.NRS_BR_CD                           " & vbNewLine _
    '                                     & "                 AND GTO4.GOODS_CD_NRS = F4.CD_NRS_TO                        " & vbNewLine _
    '                                     & "                 WHERE                                                       " & vbNewLine _
    '                                     & "                 F4.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                     & "                 UNION ALL                                                   " & vbNewLine _
    '                                     & "                 SELECT                                                      " & vbNewLine _
    '                                     & "                  F5.NRS_BR_CD       AS NRS_BR_CD                            " & vbNewLine _
    '                                     & "                 ,F5.CD_NRS          AS GOODS_CD_NRS                         " & vbNewLine _
    '                                     & "                 ,GTO5.CUST_CD_L     AS CUST_CD_L                            " & vbNewLine _
    '                                     & "                 ,GTO5.CUST_CD_M     AS CUST_CD_M                            " & vbNewLine _
    '                                     & "                 ,F5.CD_NRS_TO       AS ALCTD_GOODS_CD                       " & vbNewLine _
    '                                     & "                 ,G5.CUST_CD_L       AS ALCTD_CUST_CD_L                      " & vbNewLine _
    '                                     & "                 ,G5.CUST_CD_M       AS ALCTD_CUST_CD_M                      " & vbNewLine _
    '                                     & "                 FROM                                                        " & vbNewLine _
    '                                     & "                 $LM_MST$..M_FURI_GOODS F5                                   " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS GTO5                            " & vbNewLine _
    '                                     & "                 ON  GTO5.SYS_DEL_FLG = '0'                                  " & vbNewLine _
    '                                     & "                 AND GTO5.NRS_BR_CD = F5.NRS_BR_CD                           " & vbNewLine _
    '                                     & "                 AND GTO5.GOODS_CD_NRS = F5.CD_NRS_TO                        " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS G5                              " & vbNewLine _
    '                                     & "                 ON  G5.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "                 AND G5.NRS_BR_CD = F5.NRS_BR_CD                             " & vbNewLine _
    '                                     & "                 AND G5.GOODS_CD_NRS = F5.CD_NRS                             " & vbNewLine _
    '                                     & "                 WHERE                                                       " & vbNewLine _
    '                                     & "                 F5.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                     & "                 UNION ALL                                                   " & vbNewLine _
    '                                     & "                 SELECT                                                      " & vbNewLine _
    '                                     & "                  F6.NRS_BR_CD       AS NRS_BR_CD                            " & vbNewLine _
    '                                     & "                 ,F6.CD_NRS          AS GOODS_CD_NRS                         " & vbNewLine _
    '                                     & "                 ,GTO6.CUST_CD_L     AS CUST_CD_L                            " & vbNewLine _
    '                                     & "                 ,GTO6.CUST_CD_M     AS CUST_CD_M                            " & vbNewLine _
    '                                     & "                 ,F6.CD_NRS          AS ALCTD_GOODS_CD                       " & vbNewLine _
    '                                     & "                 ,G6.CUST_CD_L       AS ALCTD_CUST_CD_L                      " & vbNewLine _
    '                                     & "                 ,G6.CUST_CD_M       AS ALCTD_CUST_CD_M                      " & vbNewLine _
    '                                     & "                 FROM                                                        " & vbNewLine _
    '                                     & "                 $LM_MST$..M_FURI_GOODS F6                                   " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS GTO6                            " & vbNewLine _
    '                                     & "                 ON  GTO6.SYS_DEL_FLG = '0'                                  " & vbNewLine _
    '                                     & "                 AND GTO6.NRS_BR_CD = F6.NRS_BR_CD                           " & vbNewLine _
    '                                     & "                 AND GTO6.GOODS_CD_NRS = F6.CD_NRS_TO                        " & vbNewLine _
    '                                     & "                 LEFT JOIN $LM_MST$..M_GOODS G6                              " & vbNewLine _
    '                                     & "                 ON  G6.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "                 AND G6.NRS_BR_CD = F6.NRS_BR_CD                             " & vbNewLine _
    '                                     & "                 AND G6.GOODS_CD_NRS = F6.CD_NRS                             " & vbNewLine _
    '                                     & "                 WHERE                                                       " & vbNewLine _
    '                                     & "                 F6.SYS_DEL_FLG = '0'                                        " & vbNewLine _
    '                                     & "                ) F0                                                         " & vbNewLine _
    '                                     & "                GROUP BY                                                     " & vbNewLine _
    '                                     & "                 F0.NRS_BR_CD                                                " & vbNewLine _
    '                                     & "                ,F0.GOODS_CD_NRS                                             " & vbNewLine _
    '                                     & "                ,F0.CUST_CD_L                                                " & vbNewLine _
    '                                     & "                ,F0.CUST_CD_M                                                " & vbNewLine _
    '                                     & "                ,F0.ALCTD_GOODS_CD                                           " & vbNewLine _
    '                                     & "                ,F0.ALCTD_CUST_CD_L                                          " & vbNewLine _
    '                                     & "                ,F0.ALCTD_CUST_CD_M                                          " & vbNewLine _
    '                                     & "             ) FURI                                                          " & vbNewLine _
    '                                     & "             LEFT JOIN $LM_TRN$..D_ZAI_TRS ZAITRS                            " & vbNewLine _
    '                                     & "             ON  ZAITRS.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "             AND ZAITRS.NRS_BR_CD = FURI.NRS_BR_CD                           " & vbNewLine _
    '                                     & "             AND ZAITRS.GOODS_CD_NRS = FURI.GOODS_CD_NRS                     " & vbNewLine _
    '                                     & "             LEFT JOIN $LM_TRN$..C_OUTKA_S OUTKAS                            " & vbNewLine _
    '                                     & "             ON  OUTKAS.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "             AND OUTKAS.NRS_BR_CD = ZAITRS.NRS_BR_CD                         " & vbNewLine _
    '                                     & "             AND OUTKAS.ZAI_REC_NO = ZAITRS.ZAI_REC_NO                       " & vbNewLine _
    '                                     & "             LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM                            " & vbNewLine _
    '                                     & "             ON  OUTKAM.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "             AND OUTKAM.NRS_BR_CD = OUTKAS.NRS_BR_CD                         " & vbNewLine _
    '                                     & "             AND OUTKAM.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                       " & vbNewLine _
    '                                     & "             AND OUTKAM.OUTKA_NO_M = OUTKAS.OUTKA_NO_M                       " & vbNewLine _
    '                                     & "             AND OUTKAM.GOODS_CD_NRS = FURI.ALCTD_GOODS_CD                   " & vbNewLine _
    '                                     & "             LEFT JOIN $LM_TRN$..C_OUTKA_L OUTKAL                            " & vbNewLine _
    '                                     & "             ON  OUTKAL.SYS_DEL_FLG = '0'                                    " & vbNewLine _
    '                                     & "             AND OUTKAL.NRS_BR_CD = OUTKAM.NRS_BR_CD                         " & vbNewLine _
    '                                     & "             AND OUTKAL.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                       " & vbNewLine _
    '                                     & "             AND OUTKAL.CUST_CD_L = FURI.ALCTD_CUST_CD_L                     " & vbNewLine _
    '                                     & "             AND OUTKAL.CUST_CD_M = FURI.ALCTD_CUST_CD_M                     " & vbNewLine _
    '                                     & " --出荷EDIL                                                                    " & vbNewLine _
    '                                     & " LEFT JOIN                                                                   " & vbNewLine _
    '                                     & " (SELECT                                                                     " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                                 " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                              " & vbNewLine _
    '                                     & "  ,MIN(DEST_CD)  AS DEST_CD                                                  " & vbNewLine _
    '                                     & "  ,MIN(DEST_NM)  AS DEST_NM                                                  " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                               " & vbNewLine _
    '                                     & "  FROM                                                                       " & vbNewLine _
    '                                     & "  $LM_TRN$..H_OUTKAEDI_L                                                     " & vbNewLine _
    '                                     & "  GROUP BY                                                                   " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                                 " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                              " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                               " & vbNewLine _
    '                                     & " ) EDIL                                                                      " & vbNewLine _
    '                                     & " ON  EDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
    '                                     & " AND EDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                                   " & vbNewLine _
    '                                     & " AND EDIL.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
    '                                     & " LEFT JOIN                                                                   " & vbNewLine _
    '                                     & " (SELECT                                                                     " & vbNewLine _
    '                                     & "  NRS_BR_CD                                                                  " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                              " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO_CHU                                                          " & vbNewLine _
    '                                     & "  ,FREE_C07                                                                  " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                               " & vbNewLine _
    '                                     & "  FROM                                                                       " & vbNewLine _
    '                                     & "  $LM_TRN$..H_OUTKAEDI_M                                                     " & vbNewLine _
    '                                     & "  GROUP BY                                                                   " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                                 " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO                                                              " & vbNewLine _
    '                                     & "  ,OUTKA_CTL_NO_CHU                                                          " & vbNewLine _
    '                                     & "  ,FREE_C07                                                                  " & vbNewLine _
    '                                     & "  ,SYS_DEL_FLG                                                               " & vbNewLine _
    '                                     & "  ) EDIM                                                                     " & vbNewLine _
    '                                     & " ON EDIM.NRS_BR_CD = OUTKAM.NRS_BR_CD                                        " & vbNewLine _
    '                                     & " AND EDIM.OUTKA_CTL_NO = OUTKAM.OUTKA_NO_L                                   " & vbNewLine _
    '                                     & " AND EDIM.OUTKA_CTL_NO_CHU = OUTKAM.OUTKA_NO_M                               " & vbNewLine _
    '                                     & " AND EDIM.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
    '                                     & " --届先M                                                                       " & vbNewLine _
    '                                     & " LEFT JOIN                                                                   " & vbNewLine _
    '                                     & " $LM_MST$..M_DEST DEST                                                       " & vbNewLine _
    '                                     & " ON  DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                       " & vbNewLine _
    '                                     & " AND DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                       " & vbNewLine _
    '                                     & " AND DEST.DEST_CD = OUTKAL.DEST_CD                                           " & vbNewLine _
    '                                     & " AND DEST.SYS_DEL_FLG = '0'                                                  " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 START-------------------------------------" & vbNewLine _
    '                                     & "  --商品M                                                                    " & vbNewLine _
    '                                     & "  LEFT JOIN                                                                  " & vbNewLine _
    '                                     & "   $LM_MST$..M_GOODS GOODS                                               " & vbNewLine _
    '                                     & "  ON  GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                                     " & vbNewLine _
    '                                     & "  AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                               " & vbNewLine _
    '                                     & "  AND GOODS.SYS_DEL_FLG = '0'                                                " & vbNewLine _
    '                                     & "  --入荷S                                                                    " & vbNewLine _
    '                                     & "  LEFT JOIN                                                                  " & vbNewLine _
    '                                     & "  $LM_TRN$..B_INKA_S INKA_S                                             " & vbNewLine _
    '                                     & "  ON  INKA_S.NRS_BR_CD = OUTKAS.NRS_BR_CD                                    " & vbNewLine _
    '                                     & "  AND INKA_S.INKA_NO_L = OUTKAS.INKA_NO_L                                    " & vbNewLine _
    '                                     & "  AND INKA_S.INKA_NO_M = OUTKAS.INKA_NO_M                                    " & vbNewLine _
    '                                     & "  AND INKA_S.INKA_NO_S = OUTKAS.INKA_NO_S                                    " & vbNewLine _
    '                                     & "  AND INKA_S.SYS_DEL_FLG = '0'                                               " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 END---------------------------------------" & vbNewLine _
    '                                     & " WHERE                                                                       " & vbNewLine _
    '                                     & "     OUTKAM.ALCTD_KB <> '04'                                                 " & vbNewLine _
    '                                     & " AND FURI.NRS_BR_CD = @NRS_BR_CD                                             " & vbNewLine _
    '                                     & " AND FURI.CUST_CD_L = @CUST_CD_L                                             " & vbNewLine _
    '                                     & " AND FURI.CUST_CD_M = @CUST_CD_M                                             " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_STATE_KB < '60'                                            " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_PLAN_DATE = @OUTKA_DATE                                    " & vbNewLine _
    '                                     & " AND OUTKAL.SYS_DEL_FLG = '0'                                                " & vbNewLine _
    '                                     & " GROUP BY                                                                    " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD                                                           " & vbNewLine _
    '                                     & " ,FURI.CUST_CD_L                                                             " & vbNewLine _
    '                                     & " ,FURI.CUST_CD_M                                                             " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE                                                     " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_NO_L                                                          " & vbNewLine _
    '                                     & " ,OUTKAM.OUTKA_NO_M                                                          " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO                                                          " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD                          " & vbNewLine _
    '                                     & "       ELSE OUTKAL.DEST_CD                                                   " & vbNewLine _
    '                                     & "  END                                                                        " & vbNewLine _
    '                                     & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM                        " & vbNewLine _
    '                                     & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM                          " & vbNewLine _
    '                                     & "       ELSE DEST.DEST_NM                                                     " & vbNewLine _
    '                                     & "  END                                                                        " & vbNewLine _
    '                                     & " ,FURI.GOODS_CD_NRS                                                          " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO                                                              " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO                                                           " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO                                                              " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO                                                             " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD                                                             " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA                                                                " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07                                                              " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 START-------------------------------------" & vbNewLine _
    '                                     & "  ,GOODS.PKG_NB                                                              " & vbNewLine _
    '                                     & " --★2011/12 追加 要望番号=598対応 END---------------------------------------" & vbNewLine _
    '                                     & " ) OUTKA                                                          " & vbNewLine _
    '                                     & "ON  OUTKA.NRS_BR_CD = ZAI.NRS_BR_CD                               " & vbNewLine _
    '                                     & "AND OUTKA.ZAI_REC_NO = ZAI.ZAI_REC_NO                             " & vbNewLine _
    '                                     & "--★★2011/12 削除 要望番号=598対応 START-------------------------" & vbNewLine _
    '                                     & "--★2011/12 追加 要望番号=598対応 START---------------------------" & vbNewLine _
    '                                     & "--完了個数                                                        " & vbNewLine _
    '                                     & "-- LEFT JOIN                                                         " & vbNewLine _
    '                                     & "-- (SELECT                                                           " & vbNewLine _
    '                                     & "-- OUTKAS.NRS_BR_CD                                                 " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_L                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_M                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ZAI_REC_NO                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ALCTD_NB  AS OUTEND_ALCTD_NB                             " & vbNewLine _
    '                                     & "-- ,OUTKAS.SYS_DEL_FLG                                              " & vbNewLine _
    '                                     & "-- FROM                                                             " & vbNewLine _
    '                                     & "--  $LM_MST$..C_OUTKA_S OUTKAS                                  " & vbNewLine _
    '                                     & "--出荷L                                                          " & vbNewLine _
    '                                     & "-- LEFT JOIN                                                        " & vbNewLine _
    '                                     & "-- $LM_TRN$..C_OUTKA_L OUTKAL                                  " & vbNewLine _
    '                                     & "-- ON  OUTKAL.NRS_BR_CD = OUTKAS.NRS_BR_CD                          " & vbNewLine _
    '                                     & "-- AND OUTKAL.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                        " & vbNewLine _
    '                                     & "-- AND OUTKAL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & "-- WHERE                                                            " & vbNewLine _
    '                                     & "--     OUTKAL.NRS_BR_CD = '40'                                      " & vbNewLine _
    '                                     & "-- AND OUTKAL.CUST_CD_L = '00237'                                   " & vbNewLine _
    '                                     & "-- AND OUTKAL.CUST_CD_M = '00'                                      " & vbNewLine _
    '                                     & "-- AND OUTKAL.OUTKA_STATE_KB IN('60','90')                          " & vbNewLine _
    '                                     & "-- AND OUTKAL.OUTKA_PLAN_DATE <= '20110901'                         " & vbNewLine _
    '                                     & "-- AND OUTKAL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & "--GROUP BY                                                          " & vbNewLine _
    '                                     & "--  OUTKAS.NRS_BR_CD                                                " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_L                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_M                                               " & vbNewLine _
    '                                     & "--,OUTKAS.ZAI_REC_NO                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ALCTD_NB                                                 " & vbNewLine _
    '                                     & "-- ,OUTKAS.SYS_DEL_FLG                                              " & vbNewLine _
    '                                     & "-- ) OUTEND                                                         " & vbNewLine _
    '                                     & "--ON OUTEND.NRS_BR_CD = OUTKA.NRS_BR_CD                             " & vbNewLine _
    '                                     & "--AND OUTEND.OUTKA_NO_L = OUTKA.OUTKA_NO_L                          " & vbNewLine _
    '                                     & "--AND OUTEND.OUTKA_NO_M = OUTKA.OUTKA_NO_M                          " & vbNewLine _
    '                                     & "--AND OUTEND.ZAI_REC_NO = OUTKA.ZAI_REC_NO                          " & vbNewLine _
    '                                     & "--AND OUTEND.SYS_DEL_FLG = '0'                                      " & vbNewLine _
    '                                     & "--引当中個数                                                      " & vbNewLine _
    '                                     & "--LEFT JOIN                                                         " & vbNewLine _
    '                                     & "--(SELECT                                                           " & vbNewLine _
    '                                     & "-- OUTKAS.NRS_BR_CD                                                 " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_L                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_M                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ZAI_REC_NO                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ALCTD_NB  AS HIKIATE_ALCTD_NB                            " & vbNewLine _
    '                                     & "-- ,OUTKAS.SYS_DEL_FLG                                              " & vbNewLine _
    '                                     & "-- FROM                                                             " & vbNewLine _
    '                                     & "-- $LM_TRN$..C_OUTKA_S OUTKAS                                  " & vbNewLine _
    '                                     & " --出荷L                                                          " & vbNewLine _
    '                                     & "-- LEFT JOIN                                                        " & vbNewLine _
    '                                     & "-- $LM_TRN$..C_OUTKA_L OUTKAL                                  " & vbNewLine _
    '                                     & "-- ON  OUTKAL.NRS_BR_CD = OUTKAS.NRS_BR_CD                          " & vbNewLine _
    '                                     & "-- AND OUTKAL.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                        " & vbNewLine _
    '                                     & "-- AND OUTKAL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & "-- WHERE                                                            " & vbNewLine _
    '                                     & "--     OUTKAL.NRS_BR_CD = '40'                                      " & vbNewLine _
    '                                     & "-- AND OUTKAL.CUST_CD_L = '00237'                                   " & vbNewLine _
    '                                     & "-- AND OUTKAL.CUST_CD_M = '00'                                      " & vbNewLine _
    '                                     & "-- AND OUTKAL.OUTKA_STATE_KB  <= '50'                               " & vbNewLine _
    '                                     & "-- AND OUTKAL.OUTKA_PLAN_DATE <= '20110901'                         " & vbNewLine _
    '                                     & "-- AND OUTKAL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & "--GROUP BY                                                          " & vbNewLine _
    '                                     & "--  OUTKAS.NRS_BR_CD                                                " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_L                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.OUTKA_NO_M                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ZAI_REC_NO                                               " & vbNewLine _
    '                                     & "-- ,OUTKAS.ALCTD_NB                                                 " & vbNewLine _
    '                                     & "-- ,OUTKAS.SYS_DEL_FLG                                              " & vbNewLine _
    '                                     & "-- ) HIKIATE                                                        " & vbNewLine _
    '                                     & "--ON HIKIATE.NRS_BR_CD = OUTKA.NRS_BR_CD                            " & vbNewLine _
    '                                     & "--AND HIKIATE.OUTKA_NO_L = OUTKA.OUTKA_NO_L                         " & vbNewLine _
    '                                     & "--AND HIKIATE.OUTKA_NO_M = OUTKA.OUTKA_NO_M                         " & vbNewLine _
    '                                     & "--AND HIKIATE.ZAI_REC_NO = OUTKA.ZAI_REC_NO                         " & vbNewLine _
    '                                     & "--AND HIKIATE.SYS_DEL_FLG = '0'                                     " & vbNewLine _
    '                                     & "--★2011/12 追加 要望番号=598対応 END-----------------------------" & vbNewLine _
    '                                     & "--★★2011/12 削除 要望番号=598対応 END---------------------------" & vbNewLine _
    '                                     & "LEFT JOIN $LM_MST$..M_GOODS MG                             " & vbNewLine _
    '                                     & "ON  MG.NRS_BR_CD = ZAI.NRS_BR_CD                         " & vbNewLine _
    '                                     & "AND MG.GOODS_CD_NRS = ZAI.GOODS_CD_NRS                   " & vbNewLine _
    '                                     & "--★★2011/12 追加② 要望番号=598対応 START--------------" & vbNewLine _
    '                                     & "--出荷S(合計完了個数)                                    " & vbNewLine _      
    '                                     & "LEFT JOIN                                                " & vbNewLine _
    '                                     & "(SELECT                                                 " & vbNewLine _
    '                                     & "OUTKAL.NRS_BR_CD  AS NRS_BR_CD                         " & vbNewLine _
    '                                     & ",OUTKAL.CUST_CD_L  AS CUST_CD_L                         " & vbNewLine _
    '                                     & ",OUTKAL.CUST_CD_M  AS CUST_CD_M                         " & vbNewLine _
    '                                     & ",OUTKAL.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO  AS TOU_NO                               " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO  AS SITU_NO                             " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD  AS ZONE_CD                             " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA    AS LOCA                                 " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_NRS AS GOODS_CD_NRS                      " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_CUST  AS GOODS_CD_CUST                  " & vbNewLine _
    '                                     & " ,GOODS.GOODS_NM_1   AS GOODS_NM_1                         " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO  AS LOT_NO                               " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO  AS SERIAL_NO                         " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07 AS FREE_C07                              " & vbNewLine _
    '                                     & " ,OUTKAS.SYS_DEL_FLG  AS SYS_DEL_FLG                     " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO  AS ZAI_REC_NO                       " & vbNewLine _
    '                                     & " ,SUM(ISNULL(OUTKAS.ALCTD_NB, 0)) AS SUMOUTEND_ALCTD_NB  " & vbNewLine _
    '                                     & "  FROM                                                  " & vbNewLine _
    '                                     & "  $LM_TRN$..C_OUTKA_S  OUTKAS                       " & vbNewLine _
    '                                     & "  --出荷L                                               " & vbNewLine _
    '                                     & "  LEFT JOIN                                              " & vbNewLine _
    '                                     & "  $LM_TRN$..C_OUTKA_L OUTKAL                         " & vbNewLine _    
    '                                     & "  ON  OUTKAL.NRS_BR_CD = OUTKAS.NRS_BR_CD               " & vbNewLine _
    '                                     & "  AND OUTKAL.OUTKA_NO_L = OUTKAS.OUTKA_NO_L             " & vbNewLine _
    '                                     & "  AND OUTKAL.SYS_DEL_FLG = '0'                          " & vbNewLine _
    '                                     & "  --出荷M                                               " & vbNewLine _
    '                                     & "  LEFT JOIN                                              " & vbNewLine _
    '                                     & "  $LM_TRN$..C_OUTKA_M OUTKAM                        " & vbNewLine _ 
    '                                     & "  ON  OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                " & vbNewLine _
    '                                     & "  AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L             " & vbNewLine _
    '                                     & "  AND OUTKAM.OUTKA_NO_M = OUTKAS.OUTKA_NO_M   --追加     " & vbNewLine _
    '                                     & "  AND OUTKAM.SYS_DEL_FLG = '0'                               " & vbNewLine _
    '                                     & "  --商品M                                                  " & vbNewLine _        
    '                                     & "  LEFT JOIN                                                " & vbNewLine _        
    '                                     & "  $LM_MST$..M_GOODS GOODS                             " & vbNewLine _
    '                                     & "  ON  GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                    " & vbNewLine _
    '                                     & "  AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS              " & vbNewLine _
    '                                     & "  AND GOODS.SYS_DEL_FLG = '0'                               " & vbNewLine _
    '                                     & "  --EDIM                                                    " & vbNewLine _
    '                                     & "  LEFT JOIN                                                 " & vbNewLine _
    '                                     & "  (SELECT                                                   " & vbNewLine _
    '                                     & "   NRS_BR_CD                                                " & vbNewLine _                  
    '                                     & "   ,OUTKA_CTL_NO                                            " & vbNewLine _                  
    '                                     & "   ,OUTKA_CTL_NO_CHU                                        " & vbNewLine _
    '                                     & "   ,FREE_C07                                                " & vbNewLine _
    '                                     & "   ,SYS_DEL_FLG                                             " & vbNewLine _
    '                                     & "   FROM                                                     " & vbNewLine _
    '                                     & "   $LM_TRN$..H_OUTKAEDI_M                              " & vbNewLine _
    '                                     & "   GROUP BY                                                 " & vbNewLine _                  
    '                                     & "    NRS_BR_CD                                               " & vbNewLine _                  
    '                                     & "   ,OUTKA_CTL_NO                                            " & vbNewLine _                  
    '                                     & "   ,OUTKA_CTL_NO_CHU                                        " & vbNewLine _                  
    '                                     & "   ,FREE_C07                                                " & vbNewLine _                  
    '                                     & "   ,SYS_DEL_FLG                                             " & vbNewLine _                  
    '                                     & "   ) EDIM                                                   " & vbNewLine _                  
    '                                     & "  ON EDIM.NRS_BR_CD = OUTKAM.NRS_BR_CD                      " & vbNewLine _                  
    '                                     & "  AND EDIM.OUTKA_CTL_NO = OUTKAM.OUTKA_NO_L                 " & vbNewLine _                  
    '                                     & "  AND EDIM.OUTKA_CTL_NO_CHU = OUTKAM.OUTKA_NO_M             " & vbNewLine _                  
    '                                     & "  AND EDIM.SYS_DEL_FLG = '0'                                 " & vbNewLine _                                                        
    '                                     & " WHERE                                                       " & vbNewLine _     
    '                                     & "     OUTKAL.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
    '                                     & " AND OUTKAL.CUST_CD_L = @CUST_CD_L                            " & vbNewLine _
    '                                     & " AND OUTKAL.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_STATE_KB IN('60','90')                     " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_PLAN_DATE = @OUTKA_DATE                    " & vbNewLine _
    '                                     & " AND OUTKAL.SYS_DEL_FLG = '0'                                " & vbNewLine _
    '                                     & "  GROUP BY                                                  " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD                                          " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_L                                           " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_M                                          " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE                                    " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO                                             " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO                                           " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD                                           " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA                                             " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_NRS                                      " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_CUST                                     " & vbNewLine _
    '                                     & " ,GOODS.GOODS_NM_1                                       " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO                                        " & vbNewLine _
    '                                     & ",OUTKAS.SERIAL_NO                                        " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07                                          " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO                                       " & vbNewLine _
    '                                     & " ,OUTKAS.SYS_DEL_FLG                                      " & vbNewLine _
    '                                     & "  ) SUMEND                                               " & vbNewLine _
    '                                     & " ON SUMEND.NRS_BR_CD = OUTKA.NRS_BR_CD                   " & vbNewLine _
    '                                     & " AND SUMEND.CUST_CD_L = OUTKA.CUST_CD_L                   " & vbNewLine _
    '                                     & " AND SUMEND.CUST_CD_M = OUTKA.CUST_CD_M                   " & vbNewLine _
    '                                     & " AND SUMEND.OUTKA_PLAN_DATE = OUTKA.OUTKA_PLAN_DATE         " & vbNewLine _
    '                                     & " AND SUMEND.TOU_NO = OUTKA.TOU_NO                         " & vbNewLine _
    '                                     & " AND SUMEND.SITU_NO = OUTKA.SITU_NO                        " & vbNewLine _
    '                                     & " AND SUMEND.ZONE_CD = OUTKA.ZONE_CD                         " & vbNewLine _
    '                                     & " AND SUMEND.LOCA = OUTKA.LOCA                              " & vbNewLine _
    '                                     & " AND SUMEND.GOODS_CD_NRS = OUTKA.GOODS_CD_NRS           " & vbNewLine _
    '                                     & " AND SUMEND.GOODS_CD_CUST = MG.GOODS_CD_CUST              " & vbNewLine _
    '                                     & " AND SUMEND.GOODS_NM_1 = MG.GOODS_NM_1                       " & vbNewLine _
    '                                     & " AND SUMEND.LOT_NO = OUTKA.LOT_NO                          " & vbNewLine _
    '                                     & " AND SUMEND.SERIAL_NO = OUTKA.SERIAL_NO                    " & vbNewLine _ 
    '                                     & " AND SUMEND.FREE_C07 = OUTKA.FREE_C07                      " & vbNewLine _
    '                                     & " AND SUMEND.ZAI_REC_NO = OUTKA.ZAI_REC_NO                  " & vbNewLine _
    '                                     & "AND SUMEND.SYS_DEL_FLG = '0'                               " & vbNewLine _
    '                                     & "--出荷S(合計引当中個数)                                    " & vbNewLine _       
    '                                     & " LEFT JOIN                                                 " & vbNewLine _
    '                                     & " (SELECT                                                   " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD  AS NRS_BR_CD                        " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_L  AS CUST_CD_L                         " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_M  AS CUST_CD_M                         " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO  AS TOU_NO                               " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO  AS SITU_NO                             " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD  AS ZONE_CD                             " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA    AS LOCA                                 " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_NRS AS GOODS_CD_NRS                     " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_CUST  AS GOODS_CD_CUST                  " & vbNewLine _
    '                                     & " ,GOODS.GOODS_NM_1   AS GOODS_NM_1                       " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO  AS LOT_NO                               " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO  AS SERIAL_NO                         " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07 AS FREE_C07                              " & vbNewLine _
    '                                     & " ,OUTKAS.SYS_DEL_FLG  AS SYS_DEL_FLG                       " & vbNewLine _
    '                                     & " ,OUTKAS.ZAI_REC_NO  AS ZAI_REC_NO                         " & vbNewLine _
    '                                     & " ,SUM(ISNULL(OUTKAS.ALCTD_NB, 0)) AS SUMHIKIATE_ALCTD_NB   " & vbNewLine _
    '                                     & "  FROM                                                     " & vbNewLine _
    '                                     & "  $LM_TRN$..C_OUTKA_S  OUTKAS                         " & vbNewLine _
    '                                     & "  --出荷L                                                  " & vbNewLine _
    '                                     & "  LEFT JOIN                                                " & vbNewLine _
    '                                     & "  $LM_TRN$..C_OUTKA_L OUTKAL                          " & vbNewLine _                  
    '                                     & "  ON  OUTKAL.NRS_BR_CD = OUTKAS.NRS_BR_CD                  " & vbNewLine _
    '                                     & "  AND OUTKAL.OUTKA_NO_L = OUTKAS.OUTKA_NO_L                " & vbNewLine _
    '                                     & "  AND OUTKAL.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & "  --出荷M                                                  " & vbNewLine _
    '                                     & "  LEFT JOIN                                                " & vbNewLine _        
    '                                     & "  $LM_TRN$..C_OUTKA_M OUTKAM                          " & vbNewLine _             
    '                                     & "  ON  OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                  " & vbNewLine _        
    '                                     & "  AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                " & vbNewLine _        
    '                                     & "  AND OUTKAM.OUTKA_NO_M = OUTKAS.OUTKA_NO_M   --追加       " & vbNewLine _              
    '                                     & "  AND OUTKAM.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & "  --商品M                                                  " & vbNewLine _
    '                                     & "  LEFT JOIN                                                " & vbNewLine _
    '                                     & "  $LM_MST$..M_GOODS GOODS                             " & vbNewLine _             
    '                                     & "  ON  GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                   " & vbNewLine _
    '                                     & "  AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS             " & vbNewLine _
    '                                     & "  AND GOODS.SYS_DEL_FLG = '0'                              " & vbNewLine _
    '                                     & "  --EDIM                                                   " & vbNewLine _
    '                                     & "  LEFT JOIN                                                " & vbNewLine _
    '                                     & "  (SELECT                                                  " & vbNewLine _                   
    '                                     & "   NRS_BR_CD                                               " & vbNewLine _                   
    '                                     & "   ,OUTKA_CTL_NO                                           " & vbNewLine _                   
    '                                     & "   ,OUTKA_CTL_NO_CHU                                       " & vbNewLine _                   
    '                                     & "   ,FREE_C07                                               " & vbNewLine _                    
    '                                     & "   ,SYS_DEL_FLG                                            " & vbNewLine _                   
    '                                     & "   FROM                                                    " & vbNewLine _                   
    '                                     & "  $LM_TRN$..H_OUTKAEDI_M                             " & vbNewLine _                        
    '                                     & "   GROUP BY                                                " & vbNewLine _                   
    '                                     & "    NRS_BR_CD                                              " & vbNewLine _                  
    '                                     & "   ,OUTKA_CTL_NO                                           " & vbNewLine _                    
    '                                     & "   ,OUTKA_CTL_NO_CHU                                       " & vbNewLine _                   
    '                                     & "   ,FREE_C07                                               " & vbNewLine _                   
    '                                     & "   ,SYS_DEL_FLG                                            " & vbNewLine _                   
    '                                     & "   ) EDIM                                                  " & vbNewLine _                   
    '                                     & "  ON EDIM.NRS_BR_CD = OUTKAM.NRS_BR_CD                   " & vbNewLine _      
    '                                     & "  AND EDIM.OUTKA_CTL_NO = OUTKAM.OUTKA_NO_L              " & vbNewLine _      
    '                                     & "  AND EDIM.OUTKA_CTL_NO_CHU = OUTKAM.OUTKA_NO_M          " & vbNewLine _      
    '                                     & "  AND EDIM.SYS_DEL_FLG = '0'                             " & vbNewLine _
    '                                     & " WHERE                                                   " & vbNewLine _
    '                                     & "     OUTKAL.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
    '                                     & " AND OUTKAL.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
    '                                     & " AND OUTKAL.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_STATE_KB <= '50'                          " & vbNewLine _
    '                                     & " AND OUTKAL.OUTKA_PLAN_DATE = @OUTKA_DATE                          " & vbNewLine _
    '                                     & " AND OUTKAL.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                     & "  GROUP BY                                               " & vbNewLine _
    '                                     & "  OUTKAL.NRS_BR_CD                                       " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_L                                       " & vbNewLine _
    '                                     & " ,OUTKAL.CUST_CD_M                                       " & vbNewLine _
    '                                     & " ,OUTKAL.OUTKA_PLAN_DATE                                 " & vbNewLine _
    '                                     & " ,OUTKAS.TOU_NO                                          " & vbNewLine _
    '                                     & " ,OUTKAS.SITU_NO                                         " & vbNewLine _
    '                                     & " ,OUTKAS.ZONE_CD                                         " & vbNewLine _
    '                                     & " ,OUTKAS.LOCA                                            " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_NRS                                     " & vbNewLine _
    '                                     & " ,GOODS.GOODS_CD_CUST                                    " & vbNewLine _
    '                                     & " ,GOODS.GOODS_NM_1                                       " & vbNewLine _
    '                                     & " ,OUTKAS.LOT_NO                                          " & vbNewLine _
    '                                     & " ,OUTKAS.SERIAL_NO                                       " & vbNewLine _
    '                                     & " ,EDIM.FREE_C07                                          " & vbNewLine _
    '                                     & ",OUTKAS.ZAI_REC_NO                                       " & vbNewLine _
    '                                     & ",OUTKAS.SYS_DEL_FLG                                      " & vbNewLine _
    '                                     & ") SUMHIKIATE                                             " & vbNewLine _
    '                                     & "ON SUMHIKIATE.NRS_BR_CD = OUTKA.NRS_BR_CD                " & vbNewLine _
    '                                     & "AND SUMHIKIATE.CUST_CD_L = OUTKA.CUST_CD_L               " & vbNewLine _
    '                                     & " AND SUMHIKIATE.CUST_CD_M = OUTKA.CUST_CD_M              " & vbNewLine _
    '                                     & " AND SUMHIKIATE.OUTKA_PLAN_DATE = OUTKA.OUTKA_PLAN_DATE  " & vbNewLine _
    '                                     & " AND SUMHIKIATE.TOU_NO = OUTKA.TOU_NO                    " & vbNewLine _
    '                                     & " AND SUMHIKIATE.SITU_NO = OUTKA.SITU_NO                  " & vbNewLine _
    '                                     & " AND SUMHIKIATE.ZONE_CD = OUTKA.ZONE_CD                  " & vbNewLine _
    '                                     & " AND SUMHIKIATE.LOCA = OUTKA.LOCA                        " & vbNewLine _
    '                                     & " AND SUMHIKIATE.GOODS_CD_NRS = OUTKA.GOODS_CD_NRS        " & vbNewLine _
    '                                     & " AND SUMHIKIATE.GOODS_CD_CUST = MG.GOODS_CD_CUST         " & vbNewLine _
    '                                     & " AND SUMHIKIATE.GOODS_NM_1 = MG.GOODS_NM_1               " & vbNewLine _
    '                                     & " AND SUMHIKIATE.LOT_NO = OUTKA.LOT_NO                    " & vbNewLine _
    '                                     & " AND SUMHIKIATE.SERIAL_NO = OUTKA.SERIAL_NO              " & vbNewLine _
    '                                     & " AND SUMHIKIATE.FREE_C07 = OUTKA.FREE_C07                " & vbNewLine _
    '                                     & " AND SUMHIKIATE.ZAI_REC_NO = OUTKA.ZAI_REC_NO            " & vbNewLine _
    '                                     & " AND SUMHIKIATE.SYS_DEL_FLG = '0'                        " & vbNewLine _
    '                                     & "--★★2011/12 追加② 要望番号=598対応 END----------------" & vbNewLine _
    '                                     & "WHERE                                                    " & vbNewLine _
    '                                     & "OUTKA.OUTKA_PLAN_DATE = @OUTKA_DATE                      " & vbNewLine
    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
        Private Const SQL_FROM As String = "FROM                                                              " & vbNewLine _
                                         & "--出荷テーブル                                                    " & vbNewLine _
                                         & "(SELECT                                                           " & vbNewLine _
                                         & " CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                 " & vbNewLine _
                                         & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                         & "      ELSE MR3.RPT_ID END  AS RPT_ID                              " & vbNewLine _
                                         & " ,OUTKAL.NRS_BR_CD  AS NRS_BR_CD                                  " & vbNewLine _
                                         & " ,OUTKAL.CUST_CD_L  AS CUST_CD_L                                  " & vbNewLine _
                                         & " ,OUTKAL.CUST_CD_M  AS CUST_CD_M                                  " & vbNewLine _
                                         & " ,OUTKAL.OUTKA_PLAN_DATE  AS OUTKA_PLAN_DATE                      " & vbNewLine _
                                         & " ,OUTKAL.OUTKA_NO_L  AS OUTKA_NO_L                                " & vbNewLine _
                                         & " ,OUTKAM.OUTKA_NO_M  AS OUTKA_NO_M                                " & vbNewLine _
                                         & " ,OUTKAS.ZAI_REC_NO  AS ZAI_REC_NO                                " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD               " & vbNewLine _
                                         & "       ELSE OUTKAL.DEST_CD                                        " & vbNewLine _
                                         & "  END                       AS DEST_CD                            " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM             " & vbNewLine _
                                         & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM               " & vbNewLine _
                                         & "       ELSE DEST.DEST_NM                                          " & vbNewLine _
                                         & "  END                       AS DEST_NM                            " & vbNewLine _
                                         & " ,OUTKAM.GOODS_CD_NRS  AS GOODS_CD_NRS                            " & vbNewLine _
                                         & " ,EDIM.FREE_C07 AS FREE_C07                                       " & vbNewLine _
                                         & " ,OUTKAS.LOT_NO  AS LOT_NO                                        " & vbNewLine _
                                         & " ,OUTKAS.SERIAL_NO  AS SERIAL_NO                                  " & vbNewLine _
                                         & " ,OUTKAS.TOU_NO  AS TOU_NO                                        " & vbNewLine _
                                         & " ,OUTKAS.SITU_NO AS SITU_NO                                       " & vbNewLine _
                                         & " ,OUTKAS.ZONE_CD AS ZONE_CD                                       " & vbNewLine _
                                         & " ,OUTKAS.LOCA  AS LOCA                                            " & vbNewLine _
                                         & " ,SUM(OUTKAS.ALCTD_NB) AS OUTKA_ALCTD_NB                          " & vbNewLine _
                                         & " FROM                                                             " & vbNewLine _
                                         & " --出荷L                                                          " & vbNewLine _
                                         & " $LM_TRN$..C_OUTKA_L OUTKAL                                       " & vbNewLine _
                                         & " --出荷M                                                          " & vbNewLine _
                                         & " LEFT JOIN                                                        " & vbNewLine _
                                         & " $LM_TRN$..C_OUTKA_M OUTKAM                                       " & vbNewLine _
                                         & " ON  OUTKAM.NRS_BR_CD = OUTKAL.NRS_BR_CD                          " & vbNewLine _
                                         & " AND OUTKAM.OUTKA_NO_L = OUTKAL.OUTKA_NO_L                        " & vbNewLine _
                                         & " AND OUTKAM.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                         & " --出荷S                                                          " & vbNewLine _
                                         & " LEFT JOIN                                                        " & vbNewLine _
                                         & " $LM_TRN$..C_OUTKA_S OUTKAS                                       " & vbNewLine _
                                         & " ON  OUTKAS.NRS_BR_CD = OUTKAM.NRS_BR_CD                          " & vbNewLine _
                                         & " AND OUTKAS.OUTKA_NO_L = OUTKAM.OUTKA_NO_L                        " & vbNewLine _
                                         & " AND OUTKAS.OUTKA_NO_M = OUTKAM.OUTKA_NO_M                        " & vbNewLine _
                                         & " AND OUTKAS.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                         & " --出荷EDIL                                                       " & vbNewLine _
                                         & " LEFT JOIN                                                        " & vbNewLine _
                                         & " (SELECT                                                          " & vbNewLine _
                                         & "   NRS_BR_CD                                                      " & vbNewLine _
                                         & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
                                         & "  ,MIN(DEST_CD)  AS DEST_CD                                       " & vbNewLine _
                                         & "  ,MIN(DEST_NM)  AS DEST_NM                                       " & vbNewLine _
                                         & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
                                         & "  FROM                                                            " & vbNewLine _
                                         & "  $LM_TRN$..H_OUTKAEDI_L                                          " & vbNewLine _
                                         & "  GROUP BY                                                        " & vbNewLine _
                                         & "   NRS_BR_CD                                                      " & vbNewLine _
                                         & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
                                         & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
                                         & " ) EDIL                                                           " & vbNewLine _
                                         & " ON  EDIL.NRS_BR_CD = OUTKAL.NRS_BR_CD                            " & vbNewLine _
                                         & " AND EDIL.OUTKA_CTL_NO = OUTKAL.OUTKA_NO_L                        " & vbNewLine _
                                         & " AND EDIL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                         & " LEFT JOIN                                                        " & vbNewLine _
                                         & " (SELECT                                                          " & vbNewLine _
                                         & "  NRS_BR_CD                                                       " & vbNewLine _
                                         & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
                                         & "  ,OUTKA_CTL_NO_CHU                                               " & vbNewLine _
                                         & "  ,FREE_C07                                                       " & vbNewLine _
                                         & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
                                         & "  FROM                                                            " & vbNewLine _
                                         & "  $LM_TRN$..H_OUTKAEDI_M                                          " & vbNewLine _
                                         & "  GROUP BY                                                        " & vbNewLine _
                                         & "   NRS_BR_CD                                                      " & vbNewLine _
                                         & "  ,OUTKA_CTL_NO                                                   " & vbNewLine _
                                         & "  ,OUTKA_CTL_NO_CHU                                               " & vbNewLine _
                                         & "  ,FREE_C07                                                       " & vbNewLine _
                                         & "  ,SYS_DEL_FLG                                                    " & vbNewLine _
                                         & "  ) EDIM                                                          " & vbNewLine _
                                         & " ON EDIM.NRS_BR_CD = OUTKAM.NRS_BR_CD                             " & vbNewLine _
                                         & " AND EDIM.OUTKA_CTL_NO = OUTKAM.OUTKA_NO_L                        " & vbNewLine _
                                         & " AND EDIM.OUTKA_CTL_NO_CHU = OUTKAM.OUTKA_NO_M                    " & vbNewLine _
                                         & " AND EDIM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                         & " --商品M                                                          " & vbNewLine _
                                         & " LEFT JOIN                                                        " & vbNewLine _
                                         & " $LM_MST$..M_GOODS GOODS                                          " & vbNewLine _
                                         & " ON  GOODS.NRS_BR_CD = OUTKAM.NRS_BR_CD                           " & vbNewLine _
                                         & " AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                     " & vbNewLine _
                                         & " AND GOODS.SYS_DEL_FLG = '0'                                      " & vbNewLine _
                                         & " --届先M                                                          " & vbNewLine _
                                         & " LEFT JOIN                                                        " & vbNewLine _
                                         & " $LM_MST$..M_DEST DEST                                            " & vbNewLine _
                                         & " ON  DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                            " & vbNewLine _
                                         & " AND DEST.CUST_CD_L = OUTKAL.CUST_CD_L                            " & vbNewLine _
                                         & " AND DEST.DEST_CD = OUTKAL.DEST_CD                                " & vbNewLine _
                                         & " AND DEST.SYS_DEL_FLG = '0'                                       " & vbNewLine _
                                         & " --出荷の荷主での荷主帳票パターン取得                             " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                              " & vbNewLine _
                                         & " ON  OUTKAL.NRS_BR_CD = MCR1.NRS_BR_CD                            " & vbNewLine _
                                         & " AND OUTKAL.CUST_CD_L = MCR1.CUST_CD_L                            " & vbNewLine _
                                         & " AND OUTKAL.CUST_CD_M = MCR1.CUST_CD_M                            " & vbNewLine _
                                         & " AND MCR1.PTN_ID = '24'                                           " & vbNewLine _
                                         & " AND '00' = MCR1.CUST_CD_S                                        " & vbNewLine _
                                         & " --帳票パターン取得                                               " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_RPT MR1                                    " & vbNewLine _
                                         & " ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                               " & vbNewLine _
                                         & " AND MR1.PTN_ID = MCR1.PTN_ID                                     " & vbNewLine _
                                         & " AND MR1.PTN_CD = MCR1.PTN_CD                                     " & vbNewLine _
                                         & " AND MR1.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                         & " --商品Mの荷主での荷主帳票パターン取得                            " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                              " & vbNewLine _
                                         & " ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                             " & vbNewLine _
                                         & " AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                             " & vbNewLine _
                                         & " AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                             " & vbNewLine _
                                         & " AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                             " & vbNewLine _
                                         & " AND MCR2.PTN_ID = '24'                                           " & vbNewLine _
                                         & " --帳票パターン取得                                               " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_RPT MR2                                    " & vbNewLine _
                                         & " ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                               " & vbNewLine _
                                         & " AND MR2.PTN_ID = MCR2.PTN_ID                                     " & vbNewLine _
                                         & " AND MR2.PTN_CD = MCR2.PTN_CD                                     " & vbNewLine _
                                         & " AND MR2.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                         & " --存在しない場合の帳票パターン取得                               " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_RPT MR3                                    " & vbNewLine _
                                         & " ON  MR3.NRS_BR_CD = OUTKAL.NRS_BR_CD                             " & vbNewLine _
                                         & " AND MR3.PTN_ID = '24'                                            " & vbNewLine _
                                         & " AND MR3.STANDARD_FLAG = '01'                                     " & vbNewLine _
                                         & " AND MR3.SYS_DEL_FLG = '0'                                        " & vbNewLine _
                                         & " WHERE                                                            " & vbNewLine _
                                         & "     OUTKAL.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                         & " AND OUTKAL.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                                         & " AND OUTKAL.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
                                         & " AND OUTKAL.OUTKA_PLAN_DATE = @OUTKA_DATE                         " & vbNewLine _
                                         & " AND OUTKAL.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                         & " GROUP BY                                                         " & vbNewLine _
                                         & "  OUTKAL.NRS_BR_CD                                                " & vbNewLine _
                                         & " ,OUTKAL.CUST_CD_L                                                " & vbNewLine _
                                         & " ,OUTKAL.CUST_CD_M                                                " & vbNewLine _
                                         & " ,OUTKAL.OUTKA_PLAN_DATE                                          " & vbNewLine _
                                         & " ,OUTKAL.OUTKA_NO_L                                               " & vbNewLine _
                                         & " ,OUTKAM.OUTKA_NO_M                                               " & vbNewLine _
                                         & " ,OUTKAS.ZAI_REC_NO                                               " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_CD               " & vbNewLine _
                                         & "       ELSE OUTKAL.DEST_CD                                        " & vbNewLine _
                                         & "  END                                                             " & vbNewLine _
                                         & " ,CASE WHEN OUTKAL.DEST_KB = '01' THEN OUTKAL.DEST_NM             " & vbNewLine _
                                         & "       WHEN OUTKAL.DEST_KB = '02' THEN EDIL.DEST_NM               " & vbNewLine _
                                         & "       ELSE DEST.DEST_NM                                          " & vbNewLine _
                                         & "  END                                                             " & vbNewLine _
                                         & " ,OUTKAM.GOODS_CD_NRS                                             " & vbNewLine _
                                         & " ,OUTKAS.LOT_NO                                                   " & vbNewLine _
                                         & " ,OUTKAS.SERIAL_NO                                                " & vbNewLine _
                                         & " ,OUTKAS.TOU_NO                                                   " & vbNewLine _
                                         & " ,OUTKAS.SITU_NO                                                  " & vbNewLine _
                                         & " ,OUTKAS.ZONE_CD                                                  " & vbNewLine _
                                         & " ,OUTKAS.LOCA                                                     " & vbNewLine _
                                         & " ,EDIM.FREE_C07                                                   " & vbNewLine _
                                         & " ,CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                " & vbNewLine _
                                         & "      WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                         & "      ELSE MR3.RPT_ID                                             " & vbNewLine _
                                         & "  END                                                             " & vbNewLine _
                                         & " ) OUTKA                                                          " & vbNewLine _
                                         & "--在庫テーブル                                                    " & vbNewLine _
                                         & "LEFT JOIN                                                         " & vbNewLine _
                                         & "(SELECT                                                           " & vbNewLine _
                                         & " ZAITRS.NRS_BR_CD     AS NRS_BR_CD                                " & vbNewLine _
                                         & ",ZAITRS.ZAI_REC_NO    AS ZAI_REC_NO                               " & vbNewLine _
                                         & ",ZAITRS.PORA_ZAI_NB   AS PORA_ZAI_NB                              " & vbNewLine _
                                         & ",ZAITRS.ALCTD_NB      AS ZAI_ALCTD_NB                             " & vbNewLine _
                                         & ",ZAITRS.ALLOC_CAN_NB  AS ALLOC_CAN_NB                             " & vbNewLine _
                                         & "FROM                                                              " & vbNewLine _
                                         & "--在庫データ                                                      " & vbNewLine _
                                         & " $LM_TRN$..D_ZAI_TRS ZAITRS                                       " & vbNewLine _
                                         & " WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                         & " AND ZAITRS.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
                                         & " AND ZAITRS.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
                                         & " AND ZAITRS.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                         & " ) ZAI                                                            " & vbNewLine _
                                         & "ON  ZAI.NRS_BR_CD = OUTKA.NRS_BR_CD                               " & vbNewLine _
                                         & "AND ZAI.ZAI_REC_NO = OUTKA.ZAI_REC_NO                             " & vbNewLine _
                                         & "--商品M                                                           " & vbNewLine _
                                         & "LEFT JOIN $LM_MST$..M_GOODS MG                                    " & vbNewLine _
                                         & "ON  MG.NRS_BR_CD = OUTKA.NRS_BR_CD                                " & vbNewLine _
                                         & "AND MG.GOODS_CD_NRS = OUTKA.GOODS_CD_NRS                          " & vbNewLine _
                                         & "WHERE                                                             " & vbNewLine _
                                         & "OUTKA.OUTKA_PLAN_DATE = @OUTKA_DATE                               " & vbNewLine 

    'START END 要望番号No.633


    'START KISHI 要望番号No.633
    '''' <summary>
    '''' ORDER BY
    '''' </summary>
    '''' <remarks></remarks>
    '    Private Const SQL_ORDER_BY As String = "ORDER BY                  " & vbNewLine _
    '                                         & "OUTKA.OUTKA_PLAN_DATE     " & vbNewLine _
    '                                         & ",ZAI.TOU_NO               " & vbNewLine _
    '                                         & ",ZAI.SITU_NO              " & vbNewLine _
    '                                         & ",ZAI.ZONE_CD              " & vbNewLine _
    '                                         & ",ZAI.LOCA                 " & vbNewLine _
    '                                         & ",MG.GOODS_CD_CUST         " & vbNewLine _
    '                                         & ",OUTKA.FREE_C07           " & vbNewLine _
    '                                         & ",ZAI.LOT_NO               " & vbNewLine _
    '                                         & ",ZAI.SERIAL_NO            " & vbNewLine _
    '                                         & ",OUTKA.DEST_NM            " & vbNewLine
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
        Private Const SQL_ORDER_BY As String = "ORDER BY                    " & vbNewLine _
                                             & "OUTKA.OUTKA_PLAN_DATE       " & vbNewLine _
                                             & ",OUTKA.TOU_NO               " & vbNewLine _
                                             & ",OUTKA.SITU_NO              " & vbNewLine _
                                             & ",OUTKA.ZONE_CD              " & vbNewLine _
                                             & ",OUTKA.LOCA                 " & vbNewLine _
                                             & ",MG.GOODS_CD_CUST           " & vbNewLine _
                                             & ",OUTKA.FREE_C07             " & vbNewLine _
                                             & ",OUTKA.LOT_NO               " & vbNewLine _
                                             & ",OUTKA.SERIAL_NO            " & vbNewLine _
                                             & ",OUTKA.DEST_NM              " & vbNewLine
    'START END 要望番号No.633

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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD530IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD530DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMD530DAC.SQL_FROM_MPrt)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD530DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds


    End Function

    ''' <summary>
    ''' 出荷テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷テーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD530IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD530DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMD530DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        'Me._StrSql.Append(LMD530DAC.SQL_GROUP_BY)         'SQL構築(データ抽出用GROUP BY句)
        Me._StrSql.Append(LMD530DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD530DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("GENZAIKO", "GENZAIKO")
        map.Add("SUMHIKIATE_ALCTD_NB", "SUMHIKIATE_ALCTD_NB")
        map.Add("HIKIATECAN_ALCTD_NB", "HIKIATECAN_ALCTD_NB")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_NO_M", "OUTKA_NO_M")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_ALCTD_NB", "OUTKA_ALCTD_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD530OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", Me._Row.Item("OUTKA_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub


#End Region

#Region "設定処理"

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


#End Region

End Class
