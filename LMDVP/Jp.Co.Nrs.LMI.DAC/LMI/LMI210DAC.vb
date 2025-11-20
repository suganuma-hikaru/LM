' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : ハネウェル管理（在庫管理）
'  作  成  者       :  [黎]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI210DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI210DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' INデータテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI210IN"

#Region "検索データ取得処理（返却）"

#Region "検索データ取得処理（返却） 通常 SQL SELECT句"
    ''' <summary>
    ''' 検索データ取得処理（返却） SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKA As String = "--返却SQL                                                                                      " & vbNewLine _
                                            & " SELECT                                                                                        " & vbNewLine _
                                            & "    IN_TRACK.NRS_BR_CD          AS NRS_BR_CD                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.SERIAL_NO          AS SERIAL_NO                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.INOUT_DATE         AS INOUT_DATE                                                  " & vbNewLine _
                                            & "   ,IN_TRACK.IOZS_KB            AS IOZS_KB                                                     " & vbNewLine _
                                            & "   ,IN_TRACK.INOUTKA_NO_L       AS INOUTKA_NO_L                                                " & vbNewLine _
                                            & "   ,IN_TRACK.INOUTKA_NO_M       AS INOUTKA_NO_M                                                " & vbNewLine _
                                            & "   ,IN_TRACK.INOUTKA_NO_S       AS INOUTKA_NO_S                                                " & vbNewLine _
                                            & "   ,IN_TRACK.STATUS             AS STATUS                                                      " & vbNewLine _
                                            & "   ,IN_TRACK.WH_CD              AS WH_CD                                                       " & vbNewLine _
                                            & "   ,SOKO.WH_NM                  AS WH_NM                  --2013.02.12 ADD                     " & vbNewLine _
                                            & "   ,IN_TRACK.CUST_CD_L          AS CUST_CD_L                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.CUST_CD_M          AS CUST_CD_M                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.REMARK             AS REMARK                                                      " & vbNewLine _
                                            & "   ,IN_TRACK.TOFROM_CD          AS TOFROM_CD                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.TOFROM_NM          AS TOFROM_NM                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.EXP_FLG            AS EXP_FLG                                                     " & vbNewLine _
                                            & "   ,IN_TRACK.GOODS_CD_CUST      AS GOODS_CD               --商品キー                           " & vbNewLine _
                                            & "   ,IN_TRACK.CUST_ORD_NO_DTL    AS CUST_ORD_NO_DTL                                             " & vbNewLine _
                                            & "   ,IN_TRACK.BUYER_ORD_NO_DTL   AS BUYER_ORD_NO_DTL                                            " & vbNewLine _
                                            & "   ,IN_TRACK.NB                 AS NB                                                          " & vbNewLine _
                                            & "   ,IN_TRACK.QT                 AS QT                                                          " & vbNewLine _
                                            & "   ,IN_TRACK.LOT_NO             AS LOT_NO                                                      " & vbNewLine _
                                            & "   ,IN_TRACK.CYLINDER_TYPE      AS CYLINDER_TYPE                                               " & vbNewLine _
                                            & "   ,IN_TRACK.EMPTY_KB           AS EMPTY_KB                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.HAIKI_YN           AS HAIKI_YN                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.SHIP_CD_L          AS SHIP_CD_L                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.SHIP_NM_L          AS SHIP_NM_L                                                   " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_N01           AS FREE_N01                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_N02           AS FREE_N02                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_N03           AS FREE_N03                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_N04           AS FREE_N04                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_N05           AS FREE_N05                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_C01           AS FREE_C01                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_C02           AS FREE_C02                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_C03           AS FREE_C03                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_C04           AS FREE_C04                                                    " & vbNewLine _
                                            & "   ,IN_TRACK.FREE_C05           AS FREE_C05                                                    " & vbNewLine _
                                            & "   ,ALBAS.YOUKI_NO              AS YOUKI_NO                                                    " & vbNewLine _
                                            & "   ,(SELECT TOP 1                                                                              " & vbNewLine _
                                            & "     OUT_TRACK.TOFROM_CD AS TOFROM_CD_OUT                                                      " & vbNewLine _
                                            & "     FROM                                                                                      " & vbNewLine _
                                            & "     $LM_TRN$..I_CONT_TRACK       OUT_TRACK              --ハネウェルシリンダ管理#出荷届先CD   " & vbNewLine _
                                            & "     WHERE OUT_TRACK.NRS_BR_CD   = IN_TRACK.NRS_BR_CD                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.SERIAL_NO   = IN_TRACK.SERIAL_NO                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.INOUT_DATE  < IN_TRACK.INOUT_DATE                                         " & vbNewLine _
                                            & "       AND OUT_TRACK.IOZS_KB     = '20'                                                        " & vbNewLine _
                                            & "       AND OUT_TRACK.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加START                                                         " & vbNewLine _
                                            & "  --'入荷倉庫                                                                                                                      " & vbNewLine _
                                            & "  AND OUT_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')               " & vbNewLine _
                                            & "                         )                                                                                                         " & vbNewLine _
                                            & "  --'届け先                                                                                                                        " & vbNewLine _
                                            & " AND OUT_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                                )                                                                                                  " & vbNewLine _
                                            & "--     --入荷倉庫                                                                              " & vbNewLine _
                                            & "--    AND OUT_TRACK.WH_CD in ('101','103','104','105','106')                                   " & vbNewLine _
                                            & "--    --届け先                                                                                 " & vbNewLine _
                                            & "--    AND OUT_TRACK.TOFROM_CD not in ('00630','10630','20630','30630','40630','50630')         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine _
                                            & "     ORDER BY                                                                                  " & vbNewLine _
                                            & "       OUT_TRACK.INOUT_DATE DESC                                                               " & vbNewLine _
                                            & "     )                          AS TOFROM_CD_OUT                                               " & vbNewLine _
                                            & "   ,(SELECT TOP 1                                                                              " & vbNewLine _
                                            & "     OUT_TRACK.TOFROM_NM AS TOFROM_NM_OUT                                                      " & vbNewLine _
                                            & "     FROM                                                                                      " & vbNewLine _
                                            & "     $LM_TRN$..I_CONT_TRACK       OUT_TRACK              --ハネウェルシリンダ管理#出荷届先名   " & vbNewLine _
                                            & "     WHERE OUT_TRACK.NRS_BR_CD   = IN_TRACK.NRS_BR_CD                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.SERIAL_NO   = IN_TRACK.SERIAL_NO                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.INOUT_DATE  < IN_TRACK.INOUT_DATE                                         " & vbNewLine _
                                            & "       AND OUT_TRACK.IOZS_KB     = '20'                                                        " & vbNewLine _
                                            & "       AND OUT_TRACK.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加START                                                         " & vbNewLine _
                                            & "  --'入荷倉庫                                                                                                                      " & vbNewLine _
                                            & "  AND OUT_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')               " & vbNewLine _
                                            & "                         )                                                                                                         " & vbNewLine _
                                            & "  --'届け先                                                                                                                        " & vbNewLine _
                                            & " AND OUT_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                                )                                                                                                  " & vbNewLine _
                                            & "--     --入荷倉庫                                                                              " & vbNewLine _
                                            & "--    AND OUT_TRACK.WH_CD in ('101','103','104','105','106')                                   " & vbNewLine _
                                            & "--    --届け先                                                                                 " & vbNewLine _
                                            & "--    AND OUT_TRACK.TOFROM_CD not in ('00630','10630','20630','30630','40630','50630')         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine _
                                            & "     ORDER BY                                                                                  " & vbNewLine _
                                            & "       OUT_TRACK.INOUT_DATE DESC                                                               " & vbNewLine _
                                            & "     )                          AS TOFROM_NM_OUT                                               " & vbNewLine _
                                            & "   ,(SELECT TOP 1                                                                              " & vbNewLine _
                                            & "     OUT_TRACK.SHIP_CD_L AS SHIP_CD_L_OUT                                                      " & vbNewLine _
                                            & "     FROM                                                                                      " & vbNewLine _
                                            & "     $LM_TRN$..I_CONT_TRACK       OUT_TRACK              --ハネウェルシリンダ管理#出荷売上先CD " & vbNewLine _
                                            & "     WHERE OUT_TRACK.NRS_BR_CD   = IN_TRACK.NRS_BR_CD                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.SERIAL_NO   = IN_TRACK.SERIAL_NO                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.INOUT_DATE  < IN_TRACK.INOUT_DATE                                         " & vbNewLine _
                                            & "       AND OUT_TRACK.IOZS_KB     = '20'                                                        " & vbNewLine _
                                            & "       AND OUT_TRACK.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加START                                                         " & vbNewLine _
                                            & "  --'入荷倉庫                                                                                                                      " & vbNewLine _
                                            & "  AND OUT_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')               " & vbNewLine _
                                            & "                         )                                                                                                         " & vbNewLine _
                                            & "  --'届け先                                                                                                                        " & vbNewLine _
                                            & " AND OUT_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                                )                                                                                                  " & vbNewLine _
                                            & "--     --入荷倉庫                                                                              " & vbNewLine _
                                            & "--    AND OUT_TRACK.WH_CD in ('101','103','104','105','106')                                   " & vbNewLine _
                                            & "--    --届け先                                                                                 " & vbNewLine _
                                            & "--    AND OUT_TRACK.TOFROM_CD not in ('00630','10630','20630','30630','40630','50630')         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine _
                                            & "     ORDER BY                                                                                  " & vbNewLine _
                                            & "       OUT_TRACK.INOUT_DATE DESC                                                               " & vbNewLine _
                                            & "     )                          AS SHIP_CD_L_OUT                                               " & vbNewLine _
                                            & "   ,(SELECT TOP 1                                                                              " & vbNewLine _
                                            & "     OUT_TRACK.SHIP_NM_L AS SHIP_NM_L_OUT                                                      " & vbNewLine _
                                            & "     FROM                                                                                      " & vbNewLine _
                                            & "     $LM_TRN$..I_CONT_TRACK       OUT_TRACK              --ハネウェルシリンダ管理#出荷売上先名 " & vbNewLine _
                                            & "     WHERE OUT_TRACK.NRS_BR_CD   = IN_TRACK.NRS_BR_CD                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.SERIAL_NO   = IN_TRACK.SERIAL_NO                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.INOUT_DATE  < IN_TRACK.INOUT_DATE                                         " & vbNewLine _
                                            & "       AND OUT_TRACK.IOZS_KB     = '20'                                                        " & vbNewLine _
                                            & "       AND OUT_TRACK.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加START                                                         " & vbNewLine _
                                            & "  --'入荷倉庫                                                                                                                      " & vbNewLine _
                                            & "  AND OUT_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')               " & vbNewLine _
                                            & "                         )                                                                                                         " & vbNewLine _
                                            & "  --'届け先                                                                                                                        " & vbNewLine _
                                            & " AND OUT_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                                )                                                                                                  " & vbNewLine _
                                            & "--     --入荷倉庫                                                                              " & vbNewLine _
                                            & "--    AND OUT_TRACK.WH_CD in ('101','103','104','105','106')                                   " & vbNewLine _
                                            & "--   --届け先                                                                                 " & vbNewLine _
                                            & "--    AND OUT_TRACK.TOFROM_CD not in ('00630','10630','20630','30630','40630','50630')         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine _
                                            & "     ORDER BY                                                                                  " & vbNewLine _
                                            & "       OUT_TRACK.INOUT_DATE DESC                                                               " & vbNewLine _
                                            & "     )                          AS SHIP_NM_L_OUT                                               " & vbNewLine _
                                            & "   ,(SELECT TOP 1                                                                              " & vbNewLine _
                                            & "     OUT_TRACK.INOUT_DATE AS INOUT_DATE_OUT                                                    " & vbNewLine _
                                            & "     FROM                                                                                      " & vbNewLine _
                                            & "     $LM_TRN$..I_CONT_TRACK       OUT_TRACK              --ハネウェルシリンダ管理#出荷日       " & vbNewLine _
                                            & "     WHERE OUT_TRACK.NRS_BR_CD   = IN_TRACK.NRS_BR_CD                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.SERIAL_NO   = IN_TRACK.SERIAL_NO                                          " & vbNewLine _
                                            & "       AND OUT_TRACK.INOUT_DATE  < IN_TRACK.INOUT_DATE                                         " & vbNewLine _
                                            & "       AND OUT_TRACK.IOZS_KB     = '20'                                                        " & vbNewLine _
                                            & "       AND OUT_TRACK.SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine _
                                            & "  --'入荷倉庫                                                                                                                      " & vbNewLine _
                                            & "  AND OUT_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')               " & vbNewLine _
                                            & "                         )                                                                                                         " & vbNewLine _
                                            & "  --'届け先                                                                                                                        " & vbNewLine _
                                            & " AND OUT_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                                )                                                                                                  " & vbNewLine _
                                            & "--     --入荷倉庫                                                                              " & vbNewLine _
                                            & "--  AND OUT_TRACK.WH_CD in ('101','103','104','105','106')                                   " & vbNewLine _
                                            & "--    --届け先                                                                                 " & vbNewLine _
                                            & "--    AND OUT_TRACK.TOFROM_CD not in ('00630','10630','20630','30630','40630','50630')         " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine _
                                            & "     ORDER BY                                                                                  " & vbNewLine _
                                            & "       OUT_TRACK.INOUT_DATE DESC                                                               " & vbNewLine _
                                            & "     )                          AS INOUT_DATE_OUT                                              " & vbNewLine _
                                            & "   ,TEIKEN.NEXT_TEST_DATE      AS NEXT_TEST_DATE                                               " & vbNewLine
#End Region

#Region "検索データ取得処理（返却） SQL 区分マスタ用 SELECT句"
    ''' <summary>
    ''' 検索データ取得処理（返却） SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INKA_MST As String = "--区分値のみ寄集め(処理無)                                " & vbNewLine _
                                            & "--======================================================--" & vbNewLine _
                                            & "-- ,C014.KBN_NM1              AS Z_C014                   " & vbNewLine _
                                            & "   ,C015.KBN_NM1              AS Z_C015                   " & vbNewLine _
                                            & "   ,C016.KBN_NM1              AS Z_C0161                  " & vbNewLine _
                                            & "   ,C016.KBN_NM2              AS Z_C0162                  " & vbNewLine _
                                            & "   ,C0171.KBN_NM1             AS Z_C0171                  " & vbNewLine _
                                            & "   ,C0172.KBN_NM1             AS Z_C0172                  " & vbNewLine _
                                            & "   ,C0173.KBN_NM1             AS Z_C0173                  " & vbNewLine _
                                            & "   ,C0174.KBN_NM1             AS Z_C0174                  " & vbNewLine _
                                            & "   ,C018.KBN_NM1              AS Z_C0181                  " & vbNewLine _
                                            & "   ,C018.KBN_NM2              AS Z_C0182                  " & vbNewLine _
                                            & "   ,C018.KBN_NM3              AS Z_C0183                  " & vbNewLine _
                                            & "   ,C018.KBN_NM4              AS Z_C0184                  " & vbNewLine _
                                            & "   ,C019.KBN_NM1              AS Z_C0191                  " & vbNewLine _
                                            & "   ,C019.KBN_NM2              AS Z_C0192                  " & vbNewLine _
                                            & "--======================================================--" & vbNewLine
#End Region

#Region "検索データ取得処理 (返却) SQL 通常FROM句"
    Private Const SQL_FROM_INKA_NORMAL As String = "--返却SQL(通常FROM句)" & vbNewLine _
                                                 & "FROM      $LM_TRN$..I_CONT_TRACK        IN_TRACK          --ハネウェルシリンダ管理#入荷        " & vbNewLine _
                                                 & "LEFT JOIN $LM_TRN$..I_HONEY_ALBAS_CHG   ALBAS             --ハネウェルラベル管理               " & vbNewLine _
                                                 & "  ON ALBAS.LABEL_NO    = SUBSTRING(IN_TRACK.SERIAL_NO,1,3)                                     " & vbNewLine _
                                                 & " AND ALBAS.SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN               Z1                --区分ﾏｽﾀ#実入り・空                 " & vbNewLine _
                                                 & "  ON KBN_GROUP_CD      ='M016'                                                                 " & vbNewLine _
                                                 & " AND KBN_CD            ='01'                                                                   " & vbNewLine _
                                                 & "LEFT JOIN $LM_TRN$..I_HON_TEIKEN AS TEIKEN ON             --定期検査ﾏｽﾀ                        " & vbNewLine _
                                                 & "          IN_TRACK.SERIAL_NO  = TEIKEN.SERIAL_NO                                               " & vbNewLine _
                                                 & "      AND IN_TRACK.NRS_BR_CD  = TEIKEN.NRS_BR_CD                                               " & vbNewLine _
                                                 & "      AND TEIKEN.SYS_DEL_FLG  = '0'                                                            " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..M_SOKO AS SOKO ON                     --倉庫ﾏｽﾀ                            " & vbNewLine _
                                                 & "          IN_TRACK.NRS_BR_CD  = SOKO.NRS_BR_CD                                                 " & vbNewLine _
                                                 & "      AND IN_TRACK.WH_CD      = SOKO.WH_CD                                                     " & vbNewLine _
                                                 & "      AND SOKO.SYS_DEL_FLG    = '0'                                                            " & vbNewLine _
                                                 & " --(2013.08.15) 要望番号2095 追加START                                                         " & vbNewLine _
                                                 & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                        " & vbNewLine _
                                                 & " ON                                                                                            " & vbNewLine _
                                                 & "     MCD.NRS_BR_CD = IN_TRACK.NRS_BR_CD                                                        " & vbNewLine _
                                                 & " AND MCD.CUST_CD   = IN_TRACK.CUST_CD_L                                                        " & vbNewLine _
                                                 & " AND MCD.SUB_KB    = '44'                                                                      " & vbNewLine _
                                                 & " AND MCD.SET_NAIYO = @COOLANT_GOODS_KB_N                                                       " & vbNewLine _
                                                 & " LEFT JOIN $LM_MST$..Z_KBN GSKBN                                                               " & vbNewLine _
                                                 & " ON                                                                                            " & vbNewLine _
                                                 & "     GSKBN.KBN_GROUP_CD = 'H023'                                                               " & vbNewLine _
                                                 & " AND GSKBN.KBN_CD   = MCD.SET_NAIYO                                                            " & vbNewLine _
                                                 & " AND GSKBN.KBN_CD   = @COOLANT_GOODS_KB_C                                                      " & vbNewLine _
                                                 & " --(2013.08.15) 要望番号2095 追加END                                                           " & vbNewLine

#End Region

#Region "検索データ取得用処理 (返却) SQL 区分マスタ用FROM句"
    Private Const SQL_FROM_INKA_MST As String = "--返却SQLキャッシュ利用頻度緩和用区分FROM句                                        " & vbNewLine _
                                                    & "--=============================================================================--" & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C014 ON                                              " & vbNewLine _
                                                    & "          C014.SYS_DEL_FLG   = '0'                                               " & vbNewLine _
                                                    & "      AND C014.KBN_GROUP_CD  = 'C014'                                            " & vbNewLine _
                                                    & "      AND C014.KBN_CD        = '01'                                              " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C015 ON                                              " & vbNewLine _
                                                    & "          C015.SYS_DEL_FLG   = '0'                                               " & vbNewLine _
                                                    & "      AND C015.KBN_GROUP_CD  = 'C015'                                            " & vbNewLine _
                                                    & "      AND C015.KBN_NM1       = IN_TRACK.CYLINDER_TYPE                            " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C016 ON                                              " & vbNewLine _
                                                    & "          C016.SYS_DEL_FLG   = '0'                                               " & vbNewLine _
                                                    & "      AND C016.KBN_GROUP_CD  = 'C016'                                            " & vbNewLine _
                                                    & "      AND C016.KBN_CD        = '01'                                              " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C0171 ON                                             " & vbNewLine _
                                                    & "          C0171.SYS_DEL_FLG   = '0'                                              " & vbNewLine _
                                                    & "      AND C0171.KBN_GROUP_CD  = 'C017'                                           " & vbNewLine _
                                                    & "      AND C0171.KBN_CD        = '01'                                             " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C0172 ON                                             " & vbNewLine _
                                                    & "          C0172.SYS_DEL_FLG   = '0'                                              " & vbNewLine _
                                                    & "      AND C0172.KBN_GROUP_CD  = 'C017'                                           " & vbNewLine _
                                                    & "      AND C0172.KBN_CD        = '02'                                             " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C0173 ON                                             " & vbNewLine _
                                                    & "          C0173.SYS_DEL_FLG   = '0'                                              " & vbNewLine _
                                                    & "      AND C0173.KBN_GROUP_CD  = 'C017'                                           " & vbNewLine _
                                                    & "      AND C0173.KBN_CD        = '03'                                             " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C0174 ON                                             " & vbNewLine _
                                                    & "          C0174.SYS_DEL_FLG   = '0'                                              " & vbNewLine _
                                                    & "      AND C0174.KBN_GROUP_CD  = 'C017'                                           " & vbNewLine _
                                                    & "      AND C0174.KBN_CD        = '04'                                             " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C018 ON                                              " & vbNewLine _
                                                    & "          C018.SYS_DEL_FLG   = '0'                                               " & vbNewLine _
                                                    & "      AND C018.KBN_GROUP_CD  = 'C018'                                            " & vbNewLine _
                                                    & "      AND C018.KBN_CD        = '01'                                              " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "LEFT JOIN LM_MST..Z_KBN  AS C019 ON                                              " & vbNewLine _
                                                    & "          C019.SYS_DEL_FLG   = '0'                                               " & vbNewLine _
                                                    & "      AND C019.KBN_GROUP_CD  = 'C019'                                            " & vbNewLine _
                                                    & "      AND C019.KBN_CD        = '01'                                              " & vbNewLine _
                                                    & "                                                                                 " & vbNewLine _
                                                    & "--=============================================================================--" & vbNewLine
#End Region

#Region "検索データ取得用処理 (返却) SQL 固定WHERE句"
    Private Const SQL_WHERE_INKA As String = "--返却SQL固定WHERE文 --ここから                                                                                                     " & vbNewLine _
                                            & "WHERE IN_TRACK.NRS_BR_CD     = @NRS_BR_CD                 --パラメータ                                                             " & vbNewLine _
                                            & "  AND IN_TRACK.SYS_DEL_FLG   = '0'                                                                                                 " & vbNewLine _
                                            & "  --(2013.08.15) 要望番号2095 追加START                                                                                            " & vbNewLine _
                                            & "  AND GSKBN.KBN_CD IS NOT NULL                                                                                                     " & vbNewLine _
                                            & "  --(2013.08.15) 要望番号2095 追加END                                                                                              " & vbNewLine _
                                            & "  --'入荷倉庫                                                                                                                      " & vbNewLine _
                                            & "  AND IN_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')               " & vbNewLine _
                                            & "                         ,(SELECT ISNULL(KBN_NM1,'') FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')               " & vbNewLine _
                                            & "                         )                                                                                                         " & vbNewLine _
                                            & "  --'届け先                                                                                                                        " & vbNewLine _
                                            & " AND IN_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                                ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                                )                                                                                                  " & vbNewLine _
                                            & "  --'入荷 = 品目ｺｰﾄﾞの代用条件                                                                                                     " & vbNewLine _
                                            & "  -- AND IN_TRACK.EMPTY_KB = ( SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD='M016' AND KBN_CD='01')                      " & vbNewLine _
                                            & "--返却SQL固定WHERE文 --ここまで                                                                                                    " & vbNewLine
#End Region

#Region "検索データ取得処理（返却） SQL ORDER BY句"

    ''' <summary>
    ''' 在庫検索データ取得処理 SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_INKA As String = "ORDER BY                                                            " & vbNewLine _
                                                   & " IN_TRACK.INOUT_DATE DESC                                          " & vbNewLine _
                                                   & ",IN_TRACK.SERIAL_NO  ASC                                           " & vbNewLine
#End Region

#End Region

#Region "検索データ取得処理（出荷）"

#Region "検索データ取得処理（出荷） SQL SELECT句"

    ''' <summary>
    ''' 検索データ取得処理（出荷） SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKA As String = "--出荷                                                          " & vbNewLine _
                                            & " SELECT                                                          " & vbNewLine _
                                            & "    I_TRACK.NRS_BR_CD          AS NRS_BR_CD                      " & vbNewLine _
                                            & "   ,I_TRACK.SERIAL_NO          AS SERIAL_NO                      " & vbNewLine _
                                            & "   ,I_TRACK.INOUT_DATE         AS INOUT_DATE                     " & vbNewLine _
                                            & "   ,I_TRACK.IOZS_KB            AS IOZS_KB                        " & vbNewLine _
                                            & "   ,I_TRACK.INOUTKA_NO_L       AS INOUTKA_NO_L                   " & vbNewLine _
                                            & "   ,I_TRACK.INOUTKA_NO_M       AS INOUTKA_NO_M                   " & vbNewLine _
                                            & "   ,I_TRACK.INOUTKA_NO_S       AS INOUTKA_NO_S                   " & vbNewLine _
                                            & "   ,I_TRACK.STATUS             AS STATUS                         " & vbNewLine _
                                            & "   ,I_TRACK.WH_CD              AS WH_CD                          " & vbNewLine _
                                            & "   ,SOKO.WH_NM                 AS WH_NM  -- 追2013.02.12         " & vbNewLine _
                                            & "   ,I_TRACK.CUST_CD_L          AS CUST_CD_L                      " & vbNewLine _
                                            & "   ,I_TRACK.CUST_CD_M          AS CUST_CD_M                      " & vbNewLine _
                                            & "   ,I_TRACK.REMARK             AS REMARK                         " & vbNewLine _
                                            & "   ,I_TRACK.TOFROM_CD          AS TOFROM_CD                      " & vbNewLine _
                                            & "   ,I_TRACK.TOFROM_NM          AS TOFROM_NM                      " & vbNewLine _
                                            & "   ,I_TRACK.EXP_FLG            AS EXP_FLG                        " & vbNewLine _
                                            & "   ,I_TRACK.GOODS_CD_CUST      AS GOODS_CD                       " & vbNewLine _
                                            & "   ,I_TRACK.CUST_ORD_NO_DTL    AS CUST_ORD_NO_DTL                " & vbNewLine _
                                            & "   ,I_TRACK.BUYER_ORD_NO_DTL   AS BUYER_ORD_NO_DTL               " & vbNewLine _
                                            & "   ,I_TRACK.NB                 AS NB                             " & vbNewLine _
                                            & "   ,I_TRACK.QT                 AS QT                             " & vbNewLine _
                                            & "   ,I_TRACK.LOT_NO             AS LOT_NO                         " & vbNewLine _
                                            & "   ,I_TRACK.CYLINDER_TYPE      AS CYLINDER_TYPE                  " & vbNewLine _
                                            & "   ,I_TRACK.EMPTY_KB           AS EMPTY_KB                       " & vbNewLine _
                                            & "   ,I_TRACK.HAIKI_YN           AS HAIKI_YN                       " & vbNewLine _
                                            & "   ,I_TRACK.SHIP_CD_L          AS SHIP_CD_L                      " & vbNewLine _
                                            & "   ,I_TRACK.SHIP_NM_L          AS SHIP_NM_L                      " & vbNewLine _
                                            & "   ,I_TRACK.FREE_N01           AS FREE_N01                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_N02           AS FREE_N02                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_N03           AS FREE_N03                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_N04           AS FREE_N04                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_N05           AS FREE_N05                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_C01           AS FREE_C01                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_C02           AS FREE_C02                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_C03           AS FREE_C03                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_C04           AS FREE_C04                       " & vbNewLine _
                                            & "   ,I_TRACK.FREE_C05           AS FREE_C05                       " & vbNewLine _
                                            & "   ,ALBAS.YOUKI_NO             AS YOUKI_NO                       " & vbNewLine _
                                            & "   ,''                         AS TOFROM_NM_OUT                  " & vbNewLine _
                                            & "   ,''                         AS TOFROM_CD_OUT                  " & vbNewLine _
                                            & "   ,''                         AS INOUT_DATE_OUT                 " & vbNewLine _
                                            & "   ,''                         AS SHIP_CD_L_OUT                  " & vbNewLine _
                                            & "   ,''                         AS SHIP_NM_L_OUT                  " & vbNewLine _
                                            & "   ,TEIKEN.NEXT_TEST_DATE      AS NEXT_TEST_DATE                 " & vbNewLine

#End Region

#Region "追加SELECT"
    Private Const SQL_SELECT_OUTKA_MST As String = "--区分値のみ寄集め(処理無)                       " & vbNewLine _
                                        & "--======================================================--" & vbNewLine _
                                        & "-- ,''              AS Z_C014                   " & vbNewLine _
                                        & "   ,''              AS Z_C015                   " & vbNewLine _
                                        & "   ,''              AS Z_C0161                  " & vbNewLine _
                                        & "   ,''              AS Z_C0162                  " & vbNewLine _
                                        & "   ,''              AS Z_C0171                  " & vbNewLine _
                                        & "   ,''              AS Z_C0172                  " & vbNewLine _
                                        & "   ,''              AS Z_C0173                  " & vbNewLine _
                                        & "   ,''              AS Z_C0174                  " & vbNewLine _
                                        & "   ,''              AS Z_C0181                  " & vbNewLine _
                                        & "   ,''              AS Z_C0182                  " & vbNewLine _
                                        & "   ,''              AS Z_C0183                  " & vbNewLine _
                                        & "   ,''              AS Z_C0184                  " & vbNewLine _
                                        & "   ,''              AS Z_C0191                  " & vbNewLine _
                                        & "   ,''              AS Z_C0192                  " & vbNewLine _
                                        & "--======================================================--" & vbNewLine
#End Region

#Region "FROM"
    Private Const SQL_FROM_OUTKA As String = "FROM      $LM_TRN$..I_CONT_TRACK      AS I_TRACK     -- 013118 LM_TRN_10固定修正  " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..I_HONEY_ALBAS_CHG AS ALBAS  ON  -- 013118 LM_TRN_10固定修正  " & vbNewLine _
                                            & "          ALBAS.LABEL_NO     = SUBSTRING(I_TRACK.SERIAL_NO,1,3)  " & vbNewLine _
                                            & "      AND ALBAS.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                            & "LEFT JOIN $LM_TRN$..I_HON_TEIKEN      AS TEIKEN ON   -- 013118 LM_TRN_10固定修正 " & vbNewLine _
                                            & "          I_TRACK.SERIAL_NO  = TEIKEN.SERIAL_NO                  " & vbNewLine _
                                            & "      AND I_TRACK.NRS_BR_CD  = TEIKEN.NRS_BR_CD                  " & vbNewLine _
                                            & "      AND TEIKEN.SYS_DEL_FLG = '0'                               " & vbNewLine _
                                            & "LEFT JOIN LM_MST..M_SOKO               AS SOKO   ON              " & vbNewLine _
                                            & "          I_TRACK.NRS_BR_CD  = SOKO.NRS_BR_CD                    " & vbNewLine _
                                            & "      AND I_TRACK.WH_CD      = SOKO.WH_CD                        " & vbNewLine _
                                            & "      AND SOKO.SYS_DEL_FLG   = '0'                               " & vbNewLine _
                                            & "LEFT JOIN LM_MST..Z_KBN                AS Z1     ON              " & vbNewLine _
                                            & "          KBN_GROUP_CD       ='M016'                             " & vbNewLine _
                                            & "      AND KBN_CD             ='02'                               " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加START                           " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                          " & vbNewLine _
                                            & " ON                                                              " & vbNewLine _
                                            & "     MCD.NRS_BR_CD = I_TRACK.NRS_BR_CD                           " & vbNewLine _
                                            & " AND MCD.CUST_CD   = I_TRACK.CUST_CD_L                           " & vbNewLine _
                                            & " AND MCD.SUB_KB    = '44'                                        " & vbNewLine _
                                            & " AND MCD.SET_NAIYO = @COOLANT_GOODS_KB_N                         " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..Z_KBN GSKBN                                 " & vbNewLine _
                                            & " ON                                                              " & vbNewLine _
                                            & "     GSKBN.KBN_GROUP_CD = 'H023'                                 " & vbNewLine _
                                            & " AND GSKBN.KBN_CD   = MCD.SET_NAIYO                              " & vbNewLine _
                                            & " AND GSKBN.KBN_CD   = @COOLANT_GOODS_KB_C                        " & vbNewLine _
                                            & " --(2013.08.15) 要望番号2095 追加END                             " & vbNewLine

#End Region

#Region "WHERE"
    Private Const SQL_WHERE_OUTKA As String = "WHERE I_TRACK.NRS_BR_CD      = @NRS_BR_CD  --'10' UPD 2020/06/05                              " & vbNewLine _
                                            & "  AND I_TRACK.SYS_DEL_FLG    = '0'                               " & vbNewLine _
                                            & "  --(2013.08.15) 要望番号2095 追加START                          " & vbNewLine _
                                            & "  AND GSKBN.KBN_CD IS NOT NULL                                   " & vbNewLine _
                                            & "  --(2013.08.15) 要望番号2095 追加END                            " & vbNewLine _
                                            & "  --'出荷倉庫                                                    " & vbNewLine _
                                            & "  AND I_TRACK.WH_CD in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='01')" & vbNewLine _
                                            & "                        ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='02')" & vbNewLine _
                                            & "                        ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='03')" & vbNewLine _
                                            & "                        ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='04')" & vbNewLine _
                                            & "                        ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='S084' AND KBN_CD='05')" & vbNewLine _
                                            & "                        )                                                                                        " & vbNewLine _
                                            & "  --'届け先                                                                                                      " & vbNewLine _
                                            & " AND I_TRACK.TOFROM_CD not in ( (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='01')--'00630' " & vbNewLine _
                                            & "                               ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='02')--'10630' " & vbNewLine _
                                            & "                               ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='03')--'20630' " & vbNewLine _
                                            & "                               ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='04')--'30630' " & vbNewLine _
                                            & "                               ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='05')--'40630' " & vbNewLine _
                                            & "                               ,(SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='D017' AND KBN_CD='06')--'50630' " & vbNewLine _
                                            & "                              )                                                                                                   " & vbNewLine _
                                            & "  --'出荷 = 品目ｺｰﾄﾞの代用条件                                                                                   " & vbNewLine _
                                            & "  -- AND I_TRACK.EMPTY_KB = ( SELECT KBN_NM1 FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='M016' AND KBN_CD='01')       " & vbNewLine
#End Region

#Region "検索データ取得処理（出荷） SQL ORDER BY句"

    ''' <summary>
    ''' 検索データ取得処理（出荷） SQL ORDER BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_OUTKA As String = "ORDER BY                                                          " & vbNewLine _
                                                   & " I_TRACK.INOUT_DATE DESC                                          " & vbNewLine _
                                                   & ",I_TRACK.SERIAL_NO  ASC                                           " & vbNewLine
#End Region

#End Region

#End Region 'Const

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

    ' ********** 検索処理 **********

#Region "検索データ取得処理（返却）"

    ''' <summary>
    ''' 検索データ取得処理（返却）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuInkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI210DAC.SQL_SELECT_INKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI210DAC.SQL_SELECT_INKA_MST)   'SQL構築(追加Select句)
        Me._StrSql.Append(LMI210DAC.SQL_FROM_INKA_NORMAL)  'SQL構築(通常FROM句)
        Me._StrSql.Append(LMI210DAC.SQL_FROM_INKA_MST)     'SQL構築(追加FROM句)
        Me._StrSql.Append(LMI210DAC.SQL_WHERE_INKA)        'SQL構築(固定WHERE句)
        Call Me.SetSQLWhereInka(inTbl.Rows(0))             'SQL構築(追加EWhere句 & パラメータ設定)
        Me._StrSql.Append(LMI210DAC.SQL_SELECT_ORDER_INKA) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI210DAC", "SelectKensakuInkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = Me.GetLMI210OUTMap()
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI210OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "検索データ取得処理（出荷）"

    ''' <summary>
    ''' 検索データ取得処理（出荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuOutkaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI210DAC.SQL_SELECT_OUTKA)       'SQL構築(Select句)
        Me._StrSql.Append(LMI210DAC.SQL_SELECT_OUTKA_MST)   'SQL構築(追加Select句)
        Me._StrSql.Append(LMI210DAC.SQL_FROM_OUTKA)         'SQL構築(FROM句)
        Me._StrSql.Append(LMI210DAC.SQL_WHERE_OUTKA)        'SQL構築(固定WHERE句)
        Call Me.SetSQLWhereOutka(inTbl.Rows(0))             'SQL構築(追加EWhere句 & パラメータ設定)
        Me._StrSql.Append(LMI210DAC.SQL_SELECT_ORDER_OUTKA) 'SQL構築(Order句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI210DAC", "SelectKensakuOutkaData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = Me.GetLMI210OUTMap()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI210OUT")

        reader.Close()

        Return ds

    End Function

#End Region

#End Region 'SQLメイン処理

#Region "SQL条件設定"

    ' ********** 検索処理 **********

#Region "SQL条件設定 検索データ取得（返却）処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereInka(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            '必須パラメータ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            '冷媒商品
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_N", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_C", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.CHAR))
            '(2013.08.15) 要望番号2095 追加END

            'ｼﾘﾝﾀﾞﾀｲﾌﾟ設定
            whereStr = .Item("CYLINDER_TYPE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IN_TRACK.CYLINDER_TYPE = (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='C022' AND KBN_CD=@CYLINDER_TYPE)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_TYPE", whereStr, DBDataType.CHAR))
            End If

            '入荷日From
            whereStr = .Item("INKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IN_TRACK.INOUT_DATE >= @INKA_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日To
            whereStr = .Item("INKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IN_TRACK.INOUT_DATE <= @INKA_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '処理タイプ
            whereStr = .Item("IOZS_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND IN_TRACK.IOZS_KB = @IOZS_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", whereStr, DBDataType.CHAR))
            End If
            '品目ｺｰﾄﾞ(空容器時商品コード)
            Me._StrSql.Append(" AND IN_TRACK.GOODS_CD_CUST LIKE (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='G009' AND KBN_CD='02') + '%' ")
            Me._StrSql.Append(vbNewLine)

            '--ADD Start 2018/06/01 依頼番号 : 001552  冷媒商品「1234yf」対応
            Me._StrSql.Append("AND ((MCD.SET_NAIYO = '02' AND IN_TRACK.GOODS_NM LIKE '%' + GSKBN.KBN_NM1 + '%' )  ")
            Me._StrSql.Append("      OR (MCD.SET_NAIYO <> '02'))                                                 ")
            '--ADD End   2018/06/01

        End With

    End Sub

#End Region

#Region "SQL条件設定 検索データ取得（出荷）処理"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereOutka(ByVal inTblRow As DataRow)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With inTblRow

            '必須パラメータ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '(2013.08.15) 要望番号2095 追加START
            '冷媒商品
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_N", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COOLANT_GOODS_KB_C", .Item("COOLANT_GOODS_KB").ToString(), DBDataType.CHAR))
            '(2013.08.15) 要望番号2095 追加END

            'ｼﾘﾝﾀﾞﾀｲﾌﾟ設定
            whereStr = .Item("CYLINDER_TYPE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND I_TRACK.CYLINDER_TYPE = (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='C022' AND KBN_CD=@CYLINDER_TYPE)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CYLINDER_TYPE", whereStr, DBDataType.CHAR))
            End If

            '出荷日From
            whereStr = .Item("OUTKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND I_TRACK.INOUT_DATE >= @OUTKA_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '出荷日To
            whereStr = .Item("OUTKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND I_TRACK.INOUT_DATE <= @OUTKA_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '処理タイプ
            whereStr = .Item("IOZS_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND I_TRACK.IOZS_KB = @IOZS_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", whereStr, DBDataType.CHAR))
            End If
            '品目ｺｰﾄﾞ(実入り容器時商品コード)
            Me._StrSql.Append(" AND I_TRACK.GOODS_CD_CUST LIKE (SELECT ISNULL(KBN_NM1,'') FROM LM_MST..Z_KBN WHERE KBN_GROUP_CD='G009' AND KBN_CD='01') + '%' ")
            Me._StrSql.Append(vbNewLine)

            '--ADD Start 2018/06/01 依頼番号 : 001552  冷媒商品「1234yf」対応
            Me._StrSql.Append("AND ((MCD.SET_NAIYO = '02' AND I_TRACK.GOODS_NM LIKE '%' + GSKBN.KBN_NM1 + '%' )  ")
            Me._StrSql.Append("      OR (MCD.SET_NAIYO <> '02'))                                                 ")
            '--ADD End   2018/06/01

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

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

#End Region 'SQL条件設定

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索結果項目マッピング
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLMI210OUTMap() As Hashtable
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("INOUT_DATE", "INOUT_DATE")
        map.Add("IOZS_KB", "IOZS_KB")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("INOUTKA_NO_M", "INOUTKA_NO_M")
        map.Add("INOUTKA_NO_S", "INOUTKA_NO_S")
        map.Add("STATUS", "STATUS")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("REMARK", "REMARK")
        map.Add("TOFROM_CD", "TOFROM_CD")
        map.Add("TOFROM_NM", "TOFROM_NM")
        map.Add("EXP_FLG", "EXP_FLG")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("NB", "NB")
        map.Add("QT", "QT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("CYLINDER_TYPE", "CYLINDER_TYPE")
        map.Add("EMPTY_KB", "EMPTY_KB")
        map.Add("HAIKI_YN", "HAIKI_YN")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("YOUKI_NO", "YOUKI_NO")
        map.Add("WH_NM", "WH_NM")
        'map.Add("HINSHU", "HINSHU") 不必要
        map.Add("TOFROM_NM_OUT", "TOFROM_NM_OUT")
        map.Add("TOFROM_CD_OUT", "TOFROM_CD_OUT")
        map.Add("SHIP_CD_L_OUT", "SHIP_CD_L_OUT")
        map.Add("SHIP_NM_L_OUT", "SHIP_NM_L_OUT")
        'map.Add("BUYER_ORD_NO_DTL_OUT", "BUYER_ORD_NO_DTL_OUT")
        map.Add("INOUT_DATE_OUT", "INOUT_DATE_OUT")
        map.Add("NEXT_TEST_DATE", "NEXT_TEST_DATE")
        '2013.03.04処理速度向上のため追加開始
        map.Add("Z_C015", "Z_C015")
        map.Add("Z_C0161", "Z_C0161")
        map.Add("Z_C0162", "Z_C0162")
        map.Add("Z_C0171", "Z_C0171")
        map.Add("Z_C0172", "Z_C0172")
        map.Add("Z_C0173", "Z_C0173")
        map.Add("Z_C0174", "Z_C0174")
        map.Add("Z_C0181", "Z_C0181")
        map.Add("Z_C0182", "Z_C0182")
        map.Add("Z_C0183", "Z_C0183")
        map.Add("Z_C0184", "Z_C0184")
        map.Add("Z_C0191", "Z_C0191")
        map.Add("Z_C0192", "Z_C0192")
        '2013.03.04処理速度向上のため追加終了
        Return map

    End Function

#End Region

#End Region

End Class
