' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD010    : 在庫振替入力
'  作  成  者       :  [高道]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "確定処理 SQL"

#Region "在庫振替データ作成用抽出データ"

    ''' <summary>
    ''' 在庫振替データ作成用抽出データ（SELECT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LMD010 As String = " SELECT                                                     " & vbNewLine _
                                              & "MOTOCUST.HOKAN_FREE_KIKAN AS HOKAN_FREE_KIKAN,         " & vbNewLine _
                                              & "ZAITRS.HOKAN_YN AS HOKAN_YN,            	" & vbNewLine _
                                              & "MOTOGOODS.UNSO_ONDO_KB AS UNSO_ONDO_KB,            	" & vbNewLine _
                                              & "MOTOGOODS.STD_IRIME_NB AS IRIME,            			" & vbNewLine _
                                              & "MOTOGOODS.STD_IRIME_UT AS IRIME_UT,            		" & vbNewLine _
                                              & "MOTOGOODS.CUST_CD_S AS CUST_CD_S,            		" & vbNewLine _
                                              & "MOTOGOODS.CUST_CD_SS AS CUST_CD_SS,            	" & vbNewLine _
                                              & "SAKIGOODS.GOODS_CD_NRS AS SAKI_GOODS_CD_NRS,            			" & vbNewLine _
                                              & "MOTOCUST.SAGYO_SEIQTO_CD AS SAGYO_SEIQTO_CD,           " & vbNewLine _
                                              & "MOTOCUST.OYA_SEIQTO_CD AS OYA_SEIQTO_CD            	" & vbNewLine

    'START YANAI 要望番号616
    '''' <summary>
    '''' 在庫振替データ作成用抽出データ（FROM句）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_FROM_LMD010 As String = "FROM                                             " & vbNewLine _
    '                                          & "(SELECT             	" & vbNewLine _
    '                                          & "GOODS.UNSO_ONDO_KB,            	" & vbNewLine _
    '                                          & "GOODS.STD_IRIME_NB,            	" & vbNewLine _
    '                                          & "GOODS.STD_IRIME_UT,            	" & vbNewLine _
    '                                          & "GOODS.CUST_CD_S,            	" & vbNewLine _
    '                                          & "GOODS.CUST_CD_SS            	" & vbNewLine _
    '                                          & "FROM $LM_MST$..M_GOODS AS GOODS            	" & vbNewLine _
    '                                          & "WHERE GOODS.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
    '                                          & "AND GOODS.CUST_CD_L = @CUST_CD_L            	" & vbNewLine _
    '                                          & "AND GOODS.CUST_CD_M = @CUST_CD_M            	" & vbNewLine _
    '                                          & "AND GOODS.GOODS_CD_CUST = @MOTO_GOODS_CD_CUST            	" & vbNewLine _
    '                                          & " ) AS MOTOGOODS,                           	" & vbNewLine _
    '                                          & "(SELECT             	" & vbNewLine _
    '                                          & "GOODS.GOODS_CD_NRS            	" & vbNewLine _
    '                                          & "FROM $LM_MST$..M_GOODS AS GOODS            	" & vbNewLine _
    '                                          & "WHERE GOODS.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
    '                                          & "AND GOODS.CUST_CD_L = @SAKI_CUST_CD_L            	" & vbNewLine _
    '                                          & "AND GOODS.CUST_CD_M = @SAKI_CUST_CD_M            	" & vbNewLine _
    '                                          & "AND GOODS.GOODS_CD_CUST = @SAKI_GOODS            	" & vbNewLine _
    '                                          & " ) AS SAKIGOODS,            	" & vbNewLine _
    '                                          & "(SELECT             	" & vbNewLine _
    '                                          & "CUST.HOKAN_FREE_KIKAN,            	" & vbNewLine _
    '                                          & "CUST.SAGYO_SEIQTO_CD,            	" & vbNewLine _
    '                                          & "CUST.OYA_SEIQTO_CD            	" & vbNewLine _
    '                                          & "FROM $LM_MST$..M_CUST AS CUST            	" & vbNewLine _
    '                                          & "WHERE CUST.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
    '                                          & "AND CUST.CUST_CD_L = @CUST_CD_L            	" & vbNewLine _
    '                                          & "AND CUST.CUST_CD_M = @CUST_CD_M            	" & vbNewLine _
    '                                          & "AND CUST.CUST_CD_S = '00'            	" & vbNewLine _
    '                                          & "AND CUST.CUST_CD_SS = '00'            	" & vbNewLine _
    '                                          & " ) AS MOTOCUST,            	" & vbNewLine _
    '                                          & "(SELECT             	" & vbNewLine _
    '                                          & "HOKAN_YN            	" & vbNewLine _
    '                                          & "FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS            	" & vbNewLine _
    '                                          & "WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
    '                                          & "AND ZAITRS.ZAI_REC_NO = @ZAI_REC_NO            	" & vbNewLine _
    '                                          & "AND ZAITRS.SYS_DEL_FLG     = '0') AS ZAITRS            	" & vbNewLine
    ''' <summary>
    ''' 在庫振替データ作成用抽出データ（FROM句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_LMD010 As String = "FROM                                             " & vbNewLine _
                                              & "(SELECT             	" & vbNewLine _
                                              & "GOODS.UNSO_ONDO_KB,            	" & vbNewLine _
                                              & "GOODS.STD_IRIME_NB,            	" & vbNewLine _
                                              & "GOODS.STD_IRIME_UT,            	" & vbNewLine _
                                              & "GOODS.CUST_CD_S,            	" & vbNewLine _
                                              & "GOODS.CUST_CD_SS            	" & vbNewLine _
                                              & "FROM $LM_MST$..M_GOODS AS GOODS            	" & vbNewLine _
                                              & "WHERE GOODS.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
                                              & "AND GOODS.CUST_CD_L = @CUST_CD_L            	" & vbNewLine _
                                              & "AND GOODS.CUST_CD_M = @CUST_CD_M            	" & vbNewLine _
                                              & "AND GOODS.GOODS_CD_NRS = @MOTO_GOODS_CD_CUST            	" & vbNewLine _
                                              & " ) AS MOTOGOODS,                           	" & vbNewLine _
                                              & "(SELECT             	" & vbNewLine _
                                              & "GOODS.GOODS_CD_NRS            	" & vbNewLine _
                                              & "FROM $LM_MST$..M_GOODS AS GOODS            	" & vbNewLine _
                                              & "WHERE GOODS.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
                                              & "AND GOODS.CUST_CD_L = @SAKI_CUST_CD_L            	" & vbNewLine _
                                              & "AND GOODS.CUST_CD_M = @SAKI_CUST_CD_M            	" & vbNewLine _
                                              & "AND GOODS.GOODS_CD_NRS = @SAKI_GOODS            	" & vbNewLine _
                                              & " ) AS SAKIGOODS,            	" & vbNewLine _
                                              & "(SELECT             	" & vbNewLine _
                                              & "CUST.HOKAN_FREE_KIKAN,            	" & vbNewLine _
                                              & "CUST.SAGYO_SEIQTO_CD,            	" & vbNewLine _
                                              & "CUST.OYA_SEIQTO_CD            	" & vbNewLine _
                                              & "FROM $LM_MST$..M_CUST AS CUST            	" & vbNewLine _
                                              & "WHERE CUST.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
                                              & "AND CUST.CUST_CD_L = @CUST_CD_L            	" & vbNewLine _
                                              & "AND CUST.CUST_CD_M = @CUST_CD_M            	" & vbNewLine _
                                              & "AND CUST.CUST_CD_S = '00'            	" & vbNewLine _
                                              & "AND CUST.CUST_CD_SS = '00'            	" & vbNewLine _
                                              & " ) AS MOTOCUST,            	" & vbNewLine _
                                              & "(SELECT             	" & vbNewLine _
                                              & "HOKAN_YN            	" & vbNewLine _
                                              & "FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS            	" & vbNewLine _
                                              & "WHERE ZAITRS.NRS_BR_CD = @NRS_BR_CD            	" & vbNewLine _
                                              & "AND ZAITRS.ZAI_REC_NO = @ZAI_REC_NO            	" & vbNewLine _
                                              & "AND ZAITRS.SYS_DEL_FLG     = '0') AS ZAITRS            	" & vbNewLine
    'END YANAI 要望番号616

#End Region '在庫振替データ作成用抽出データ'

#Region "振替元在庫データを取得"
    ''' <summary>
    ''' 振替元在庫データを取得（SELECT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_LMD010_ZAI_OLD As String = " SELECT                            " & vbNewLine _
                                              & "ZAITRS.NRS_BR_CD AS NRS_BR_CD,            	" & vbNewLine _
                                              & "ZAITRS.ZAI_REC_NO AS ZAI_REC_NO,           " & vbNewLine _
                                              & "ZAITRS.TOU_NO AS TOU_NO,                   " & vbNewLine _
                                              & "ZAITRS.SITU_NO AS SITU_NO,                 " & vbNewLine _
                                              & "ZAITRS.ZONE_CD AS ZONE_CD,                 " & vbNewLine _
                                              & "ZAITRS.LOCA AS LOCA,                       " & vbNewLine _
                                              & "ZAITRS.LOT_NO AS LOT_NO,                   " & vbNewLine _
                                              & "ZAITRS.CUST_CD_L AS CUST_CD_L,             " & vbNewLine _
                                              & "ZAITRS.CUST_CD_M AS CUST_CD_M,             " & vbNewLine _
                                              & "ZAITRS.GOODS_CD_NRS AS GOODS_CD_NRS,       " & vbNewLine _
                                              & "ZAITRS.GOODS_KANRI_NO AS GOODS_KANRI_NO,   " & vbNewLine _
                                              & "ZAITRS.INKA_NO_L AS INKA_NO_L,            	" & vbNewLine _
                                              & "ZAITRS.INKA_NO_M AS INKA_NO_M,            	" & vbNewLine _
                                              & "ZAITRS.INKA_NO_S AS INKA_NO_S,            	" & vbNewLine _
                                              & "ZAITRS.RSV_NO AS RSV_NO,                   " & vbNewLine _
                                              & "ZAITRS.SERIAL_NO AS SERIAL_NO,            	" & vbNewLine _
                                              & "ZAITRS.HOKAN_YN AS HOKAN_YN,            	" & vbNewLine _
                                              & "ZAITRS.PORA_ZAI_NB AS PORA_ZAI_NB,         " & vbNewLine _
                                              & "ZAITRS.ALLOC_CAN_NB AS ALLOC_CAN_NB,       " & vbNewLine _
                                              & "ZAITRS.IRIME AS IRIME,            	        " & vbNewLine _
                                              & "ZAITRS.PORA_ZAI_QT AS PORA_ZAI_QT,         " & vbNewLine _
                                              & "ZAITRS.ALCTD_QT AS ALCTD_QT,            	" & vbNewLine _
                                              & "ZAITRS.ALLOC_CAN_QT AS ALLOC_CAN_QT,       " & vbNewLine _
                                              & "ZAITRS.REMARK AS REMARK,            	    " & vbNewLine _
                                              & "ZAITRS.SMPL_FLAG AS SMPL_FLAG            	" & vbNewLine

    ''' <summary>
    ''' 振替元在庫データを取得（FROM句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_LMD010_ZAI_OLD As String = "FROM $LM_TRN$..D_ZAI_TRS AS ZAITRS            	" & vbNewLine


    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private Const SQL_SELECT_TOU_SITU_EXP As String = "SELECT [NRS_BR_CD] AS NRS_BR_CD                              " & vbNewLine _
                                                 & "    ,[WH_CD] AS WH_CD                                           " & vbNewLine _
                                                 & "    ,[TOU_NO] AS TOU_NO                                         " & vbNewLine _
                                                 & "    ,[SITU_NO] AS SITU_NO                                       " & vbNewLine _
                                                 & "    ,[SERIAL_NO] AS SERIAL_NO                                   " & vbNewLine _
                                                 & "    ,[NO_APL_GOODS_STR_RULE_APL_DATE_FROM] AS APL_DATE_FROM     " & vbNewLine _
                                                 & "    ,[NO_APL_GOODS_STR_RULE_APL_DATE_TO] AS APL_DATE_TO         " & vbNewLine _
                                                 & "    ,[CUST_CD_L] AS CUST_CD_L                                   " & vbNewLine _
                                                 & "FROM $LM_MST$..[M_TOU_SITU_EXP]                                 " & vbNewLine _
                                                 & "WHERE [NRS_BR_CD] = @NRS_BR_CD                                  " & vbNewLine _
                                                 & " AND [WH_CD] = @WH_CD                                           " & vbNewLine _
                                                 & " AND [CUST_CD_L] = @CUST_CD_L                                   " & vbNewLine _
                                                 & " AND [NO_APL_GOODS_STR_RULE_APL_DATE_FROM] <= @TRANSFER_DATE    " & vbNewLine _
                                                 & " AND [NO_APL_GOODS_STR_RULE_APL_DATE_TO] >= @TRANSFER_DATE      " & vbNewLine _
                                                 & " AND [SYS_DEL_FLG] = '0'                                        " & vbNewLine
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region '振替元在庫データを取得'

#If True Then   'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能

#Region "振替データを更新"

    ''' <summary>
    ''' 出荷データ（大）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_FURIKAE As String = "INSERT INTO $LM_TRN$..D_FURIKAE_TRS        " & vbNewLine _
                                                & " ( 		             " & vbNewLine _
                                                & "  NRS_BR_CD           " & vbNewLine _
                                                & " ,WH_CD               " & vbNewLine _
                                                & " ,FURI_NO             " & vbNewLine _
                                                & " ,FURI_DATE           " & vbNewLine _
                                                & " ,FURI_KBN            " & vbNewLine _
                                                & " ,YOUKI_HENKO_KBN     " & vbNewLine _
                                                & " ,TAX_KBN             " & vbNewLine _
                                                & " ,HOKAN_ALLOC_KBN     " & vbNewLine _
                                                & " ,MOTO_CUST_CD_L      " & vbNewLine _
                                                & " ,MOTO_CUST_CD_M      " & vbNewLine _
                                                & " ,MOTO_ORD_NO         " & vbNewLine _
                                                & " ,MOTO_GOODS_CD_NRS   " & vbNewLine _
                                                & " ,MOTO_SAGYO_CD1      " & vbNewLine _
                                                & " ,MOTO_SAGYO_CD2      " & vbNewLine _
                                                & " ,MOTO_SAGYO_CD3      " & vbNewLine _
                                                & " ,MOTO_REMARK         " & vbNewLine _
                                                & " ,SAKI_CUST_CD_L      " & vbNewLine _
                                                & " ,SAKI_CUST_CD_M      " & vbNewLine _
                                                & " ,SAKI_GOODS_CD_NRS   " & vbNewLine _
                                                & " ,SAKI_SAGYO_CD1      " & vbNewLine _
                                                & " ,SAKI_SAGYO_CD2      " & vbNewLine _
                                                & " ,SAKI_SAGYO_CD3      " & vbNewLine _
                                                & " ,SAKI_REMARK         " & vbNewLine _
                                                & " ,SYS_ENT_DATE        " & vbNewLine _
                                                & " ,SYS_ENT_TIME        " & vbNewLine _
                                                & " ,SYS_ENT_PGID        " & vbNewLine _
                                                & " ,SYS_ENT_USER        " & vbNewLine _
                                                & " ,SYS_UPD_DATE        " & vbNewLine _
                                                & " ,SYS_UPD_TIME        " & vbNewLine _
                                                & " ,SYS_UPD_PGID        " & vbNewLine _
                                                & " ,SYS_UPD_USER        " & vbNewLine _
                                                & " ,SYS_DEL_FLG         " & vbNewLine _
                                                & " ) VALUES (           " & vbNewLine _
                                                & "  @NRS_BR_CD          " & vbNewLine _
                                                & " ,@WH_CD              " & vbNewLine _
                                                & " ,@FURI_NO            " & vbNewLine _
                                                & " ,@FURI_DATE          " & vbNewLine _
                                                & " ,@FURI_KBN           " & vbNewLine _
                                                & " ,@YOUKI_HENKO_KBN    " & vbNewLine _
                                                & " ,@TAX_KBN            " & vbNewLine _
                                                & " ,@HOKAN_ALLOC_KBN    " & vbNewLine _
                                                & " ,@MOTO_CUST_CD_L     " & vbNewLine _
                                                & " ,@MOTO_CUST_CD_M     " & vbNewLine _
                                                & " ,@MOTO_ORD_NO        " & vbNewLine _
                                                & " ,@MOTO_GOODS_CD_NRS  " & vbNewLine _
                                                & " ,@MOTO_SAGYO_CD1     " & vbNewLine _
                                                & " ,@MOTO_SAGYO_CD2     " & vbNewLine _
                                                & " ,@MOTO_SAGYO_CD3     " & vbNewLine _
                                                & " ,@MOTO_REMARK        " & vbNewLine _
                                                & " ,@SAKI_CUST_CD_L     " & vbNewLine _
                                                & " ,@SAKI_CUST_CD_M     " & vbNewLine _
                                                & " ,@SAKI_GOODS_CD_NRS  " & vbNewLine _
                                                & " ,@SAKI_SAGYO_CD1     " & vbNewLine _
                                                & " ,@SAKI_SAGYO_CD2     " & vbNewLine _
                                                & " ,@SAKI_SAGYO_CD3     " & vbNewLine _
                                                & " ,@SAKI_REMARK        " & vbNewLine _
                                                & " ,@SYS_ENT_DATE       " & vbNewLine _
                                                & " ,@SYS_ENT_TIME       " & vbNewLine _
                                                & " ,@SYS_ENT_PGID       " & vbNewLine _
                                                & " ,@SYS_ENT_USER       " & vbNewLine _
                                                & " ,@SYS_UPD_DATE       " & vbNewLine _
                                                & " ,@SYS_UPD_TIME       " & vbNewLine _
                                                & " ,@SYS_UPD_PGID       " & vbNewLine _
                                                & " ,@SYS_UPD_USER       " & vbNewLine _
                                                & " ,@SYS_DEL_FLG        " & vbNewLine _
                                                & " )                    " & vbNewLine

#End Region

#End If

#Region "出荷データを更新"

    ''' <summary>
    ''' 出荷データ（大）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTLA_L As String = "INSERT INTO $LM_TRN$..C_OUTKA_L        " & vbNewLine _
                                              & " ( 		           " & vbNewLine _
                                              & " NRS_BR_CD, 		           " & vbNewLine _
                                              & " OUTKA_NO_L, 		           " & vbNewLine _
                                              & " FURI_NO, 		           " & vbNewLine _
                                              & " OUTKA_KB,           " & vbNewLine _
                                              & " SYUBETU_KB,           " & vbNewLine _
                                              & " OUTKA_STATE_KB,       " & vbNewLine _
                                              & " OUTKAHOKOKU_YN," & vbNewLine _
                                              & " PICK_KB,           " & vbNewLine _
                                              & " DENP_NO,           " & vbNewLine _
                                              & " ARR_KANRYO_INFO,           " & vbNewLine _
                                              & " WH_CD,           " & vbNewLine _
                                              & " OUTKA_PLAN_DATE,           " & vbNewLine _
                                              & " OUTKO_DATE,           " & vbNewLine _
                                              & " ARR_PLAN_DATE,           " & vbNewLine _
                                              & " ARR_PLAN_TIME,           " & vbNewLine _
                                              & " HOKOKU_DATE,           " & vbNewLine _
                                              & " TOUKI_HOKAN_YN,           " & vbNewLine _
                                              & " END_DATE,           " & vbNewLine _
                                              & " CUST_CD_L,           " & vbNewLine _
                                              & " CUST_CD_M,           " & vbNewLine _
                                              & " SHIP_CD_L,           " & vbNewLine _
                                              & " SHIP_CD_M,           " & vbNewLine _
                                              & " DEST_CD,           " & vbNewLine _
                                              & " DEST_AD_3,        " & vbNewLine _
                                              & " DEST_TEL,           " & vbNewLine _
                                              & " NHS_REMARK,        " & vbNewLine _
                                              & " SP_NHS_KB,           " & vbNewLine _
                                              & " COA_YN,        " & vbNewLine _
                                              & " CUST_ORD_NO,           " & vbNewLine _
                                              & " BUYER_ORD_NO,        " & vbNewLine _
                                              & " REMARK,           " & vbNewLine _
                                              & " OUTKA_PKG_NB,        " & vbNewLine _
                                              & " DENP_YN,           " & vbNewLine _
                                              & " PC_KB,        " & vbNewLine _
                                              & " NIYAKU_YN,        " & vbNewLine _
                                              & " ALL_PRINT_FLAG,           " & vbNewLine _
                                              & " NIHUDA_FLAG,        " & vbNewLine _
                                              & " NHS_FLAG,           " & vbNewLine _
                                              & " DENP_FLAG,        " & vbNewLine _
                                              & " COA_FLAG,           " & vbNewLine _
                                              & " HOKOKU_FLAG,        " & vbNewLine _
                                              & " MATOME_PICK_FLAG,           " & vbNewLine _
                                              & " LAST_PRINT_DATE,           " & vbNewLine _
                                              & " LAST_PRINT_TIME,        " & vbNewLine _
                                              & " SASZ_USER,           " & vbNewLine _
                                              & " OUTKO_USER,          " & vbNewLine _
                                              & " KEN_USER,            " & vbNewLine _
                                              & " OUTKA_USER,          " & vbNewLine _
                                              & " HOU_USER,            " & vbNewLine _
                                              & " ORDER_TYPE,          " & vbNewLine _
                                              & " DEST_KB,             " & vbNewLine _
                                              & " DEST_NM,             " & vbNewLine _
                                              & " DEST_AD_1,           " & vbNewLine _
                                              & " DEST_AD_2,           " & vbNewLine _
                                              & " SYS_ENT_DATE,        " & vbNewLine _
                                              & " SYS_ENT_TIME,        " & vbNewLine _
                                              & " SYS_ENT_PGID,        " & vbNewLine _
                                              & " SYS_ENT_USER,        " & vbNewLine _
                                              & " SYS_UPD_DATE,        " & vbNewLine _
                                              & " SYS_UPD_TIME,        " & vbNewLine _
                                              & " SYS_UPD_PGID,        " & vbNewLine _
                                              & " SYS_UPD_USER,        " & vbNewLine _
                                              & " SYS_DEL_FLG          " & vbNewLine _
                                              & " ) VALUES (           " & vbNewLine _
                                              & " @NRS_BR_CD,          " & vbNewLine _
                                              & " @OUTKA_NO_L,         " & vbNewLine _
                                              & " @FURI_NO,            " & vbNewLine _
                                              & " @OUTKA_KB,           " & vbNewLine _
                                              & " @SYUBETU_KB,         " & vbNewLine _
                                              & " @OUTKA_STATE_KB,        " & vbNewLine _
                                              & " @OUTKAHOKOKU_YN,        " & vbNewLine _
                                              & " @PICK_KB,        " & vbNewLine _
                                              & " @DENP_NO,        " & vbNewLine _
                                              & " @ARR_KANRYO_INFO,        " & vbNewLine _
                                              & " @WH_CD,        " & vbNewLine _
                                              & " @OUTKA_PLAN_DATE,        " & vbNewLine _
                                              & " @OUTKO_DATE,        " & vbNewLine _
                                              & " @ARR_PLAN_DATE,        " & vbNewLine _
                                              & " @ARR_PLAN_TIME,        " & vbNewLine _
                                              & " @HOKOKU_DATE,        " & vbNewLine _
                                              & " @TOUKI_HOKAN_YN,        " & vbNewLine _
                                              & " @END_DATE,        " & vbNewLine _
                                              & " @CUST_CD_L,        " & vbNewLine _
                                              & " @CUST_CD_M,        " & vbNewLine _
                                              & " @SHIP_CD_L,        " & vbNewLine _
                                              & " @SHIP_CD_M,        " & vbNewLine _
                                              & " @DEST_CD,        " & vbNewLine _
                                              & " @DEST_AD_3,        " & vbNewLine _
                                              & " @DEST_TEL,        " & vbNewLine _
                                              & " @NHS_REMARK,        " & vbNewLine _
                                              & " @SP_NHS_KB,        " & vbNewLine _
                                              & " @COA_YN,        " & vbNewLine _
                                              & " @CUST_ORD_NO,        " & vbNewLine _
                                              & " @BUYER_ORD_NO,        " & vbNewLine _
                                              & " @REMARK,        " & vbNewLine _
                                              & " @OUTKA_PKG_NB,        " & vbNewLine _
                                              & " @DENP_YN,        " & vbNewLine _
                                              & " @PC_KB,        " & vbNewLine _
                                              & " @NIYAKU_YN,        " & vbNewLine _
                                              & " @ALL_PRINT_FLAG,        " & vbNewLine _
                                              & " @NIHUDA_FLAG,        " & vbNewLine _
                                              & " @NHS_FLAG,        " & vbNewLine _
                                              & " @DENP_FLAG,        " & vbNewLine _
                                              & " @COA_FLAG,        " & vbNewLine _
                                              & " @HOKOKU_FLAG,        " & vbNewLine _
                                              & " @MATOME_PICK_FLAG,           " & vbNewLine _
                                              & " @LAST_PRINT_DATE,        " & vbNewLine _
                                              & " @LAST_PRINT_TIME,        " & vbNewLine _
                                              & " @SASZ_USER,        " & vbNewLine _
                                              & " @OUTKO_USER,        " & vbNewLine _
                                              & " @KEN_USER,        " & vbNewLine _
                                              & " @OUTKA_USER,        " & vbNewLine _
                                              & " @HOU_USER,        " & vbNewLine _
                                              & " @ORDER_TYPE,        " & vbNewLine _
                                              & " @DEST_KB,             " & vbNewLine _
                                              & " @DEST_NM,             " & vbNewLine _
                                              & " @DEST_AD_1,           " & vbNewLine _
                                              & " @DEST_AD_2,           " & vbNewLine _
                                              & " @SYS_ENT_DATE,        " & vbNewLine _
                                              & " @SYS_ENT_TIME,        " & vbNewLine _
                                              & " @SYS_ENT_PGID,        " & vbNewLine _
                                              & " @SYS_ENT_USER,        " & vbNewLine _
                                              & " @SYS_UPD_DATE,        " & vbNewLine _
                                              & " @SYS_UPD_TIME,        " & vbNewLine _
                                              & " @SYS_UPD_PGID,        " & vbNewLine _
                                              & " @SYS_UPD_USER,        " & vbNewLine _
                                              & " @SYS_DEL_FLG        " & vbNewLine _
                                              & " )        " & vbNewLine

    ''' <summary>
    ''' 出荷データ（中）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTLA_M As String = "INSERT INTO $LM_TRN$..C_OUTKA_M        " & vbNewLine _
                                              & " ( 		           " & vbNewLine _
                                              & " NRS_BR_CD, 		           " & vbNewLine _
                                              & " OUTKA_NO_L, 		           " & vbNewLine _
                                              & " OUTKA_NO_M, 		           " & vbNewLine _
                                              & " EDI_SET_NO,           " & vbNewLine _
                                              & " COA_YN,           " & vbNewLine _
                                              & " CUST_ORD_NO_DTL,       " & vbNewLine _
                                              & " BUYER_ORD_NO_DTL," & vbNewLine _
                                              & " GOODS_CD_NRS,           " & vbNewLine _
                                              & " RSV_NO,           " & vbNewLine _
                                              & " LOT_NO,           " & vbNewLine _
                                              & " SERIAL_NO,           " & vbNewLine _
                                              & " ALCTD_KB,           " & vbNewLine _
                                              & " OUTKA_PKG_NB,           " & vbNewLine _
                                              & " OUTKA_HASU,           " & vbNewLine _
                                              & " OUTKA_QT,           " & vbNewLine _
                                              & " OUTKA_TTL_NB,           " & vbNewLine _
                                              & " OUTKA_TTL_QT,           " & vbNewLine _
                                              & " ALCTD_NB,           " & vbNewLine _
                                              & " ALCTD_QT,           " & vbNewLine _
                                              & " BACKLOG_NB,           " & vbNewLine _
                                              & " BACKLOG_QT,           " & vbNewLine _
                                              & " UNSO_ONDO_KB,           " & vbNewLine _
                                              & " IRIME,           " & vbNewLine _
                                              & " IRIME_UT,        " & vbNewLine _
                                              & " OUTKA_M_PKG_NB,           " & vbNewLine _
                                              & " REMARK,        " & vbNewLine _
                                              & " SIZE_KB,           " & vbNewLine _
                                              & " ZAIKO_KB,        " & vbNewLine _
                                              & " SOURCE_CD,           " & vbNewLine _
                                              & " YELLOW_CARD,        " & vbNewLine _
                                              & " PRINT_SORT,           " & vbNewLine _
                                              & " GOODS_CD_NRS_FROM,   " & vbNewLine _
                                              & " SYS_ENT_DATE,        " & vbNewLine _
                                              & " SYS_ENT_TIME,        " & vbNewLine _
                                              & " SYS_ENT_PGID,        " & vbNewLine _
                                              & " SYS_ENT_USER,        " & vbNewLine _
                                              & " SYS_UPD_DATE,        " & vbNewLine _
                                              & " SYS_UPD_TIME,        " & vbNewLine _
                                              & " SYS_UPD_PGID,        " & vbNewLine _
                                              & " SYS_UPD_USER,        " & vbNewLine _
                                              & " SYS_DEL_FLG        " & vbNewLine _
                                              & " ) VALUES (        " & vbNewLine _
                                              & " @NRS_BR_CD,        " & vbNewLine _
                                              & " @OUTKA_NO_L,        " & vbNewLine _
                                              & " @OUTKA_NO_M,        " & vbNewLine _
                                              & " @EDI_SET_NO,        " & vbNewLine _
                                              & " @COA_YN,        " & vbNewLine _
                                              & " @CUST_ORD_NO_DTL,        " & vbNewLine _
                                              & " @BUYER_ORD_NO_DTL,        " & vbNewLine _
                                              & " @GOODS_CD_NRS,        " & vbNewLine _
                                              & " @RSV_NO,        " & vbNewLine _
                                              & " @LOT_NO,        " & vbNewLine _
                                              & " @SERIAL_NO,        " & vbNewLine _
                                              & " @ALCTD_KB,        " & vbNewLine _
                                              & " @OUTKA_PKG_NB,        " & vbNewLine _
                                              & " @OUTKA_HASU,        " & vbNewLine _
                                              & " @OUTKA_QT,        " & vbNewLine _
                                              & " @OUTKA_TTL_NB,        " & vbNewLine _
                                              & " @OUTKA_TTL_QT,        " & vbNewLine _
                                              & " @ALCTD_NB,        " & vbNewLine _
                                              & " @ALCTD_QT,        " & vbNewLine _
                                              & " @BACKLOG_NB,        " & vbNewLine _
                                              & " @BACKLOG_QT,        " & vbNewLine _
                                              & " @UNSO_ONDO_KB,        " & vbNewLine _
                                              & " @IRIME,        " & vbNewLine _
                                              & " @IRIME_UT,        " & vbNewLine _
                                              & " @OUTKA_M_PKG_NB,        " & vbNewLine _
                                              & " @REMARK,        " & vbNewLine _
                                              & " @SIZE_KB,        " & vbNewLine _
                                              & " @ZAIKO_KB,        " & vbNewLine _
                                              & " @SOURCE_CD,        " & vbNewLine _
                                              & " @YELLOW_CARD,        " & vbNewLine _
                                              & " @PRINT_SORT,        " & vbNewLine _
                                              & " @GOODS_CD_NRS_FROM,   " & vbNewLine _
                                              & " @SYS_ENT_DATE,        " & vbNewLine _
                                              & " @SYS_ENT_TIME,        " & vbNewLine _
                                              & " @SYS_ENT_PGID,        " & vbNewLine _
                                              & " @SYS_ENT_USER,        " & vbNewLine _
                                              & " @SYS_UPD_DATE,        " & vbNewLine _
                                              & " @SYS_UPD_TIME,        " & vbNewLine _
                                              & " @SYS_UPD_PGID,        " & vbNewLine _
                                              & " @SYS_UPD_USER,        " & vbNewLine _
                                              & " @SYS_DEL_FLG        " & vbNewLine _
                                              & " )        " & vbNewLine

    ''' <summary>
    ''' 出荷データ（小）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTLA_S As String = "INSERT INTO $LM_TRN$..C_OUTKA_S        " & vbNewLine _
                                              & " ( 		           " & vbNewLine _
                                              & " NRS_BR_CD, 		           " & vbNewLine _
                                              & " OUTKA_NO_L, 		           " & vbNewLine _
                                              & " OUTKA_NO_M, 		           " & vbNewLine _
                                              & " OUTKA_NO_S, 		           " & vbNewLine _
                                              & " TOU_NO, 		           " & vbNewLine _
                                              & " SITU_NO,           " & vbNewLine _
                                              & " ZONE_CD,           " & vbNewLine _
                                              & " LOCA,       " & vbNewLine _
                                              & " LOT_NO," & vbNewLine _
                                              & " SERIAL_NO,           " & vbNewLine _
                                              & " OUTKA_TTL_NB,           " & vbNewLine _
                                              & " OUTKA_TTL_QT,           " & vbNewLine _
                                              & " ZAI_REC_NO,           " & vbNewLine _
                                              & " INKA_NO_L,           " & vbNewLine _
                                              & " INKA_NO_M,           " & vbNewLine _
                                              & " INKA_NO_S,           " & vbNewLine _
                                              & " ZAI_UPD_FLAG,           " & vbNewLine _
                                              & " ALCTD_CAN_NB,           " & vbNewLine _
                                              & " ALCTD_NB,           " & vbNewLine _
                                              & " ALCTD_CAN_QT,           " & vbNewLine _
                                              & " ALCTD_QT,           " & vbNewLine _
                                              & " IRIME,           " & vbNewLine _
                                              & " BETU_WT,           " & vbNewLine _
                                              & " COA_FLAG,           " & vbNewLine _
                                              & " REMARK,           " & vbNewLine _
                                              & " SMPL_FLAG,        " & vbNewLine _
                                              & " SYS_ENT_DATE,        " & vbNewLine _
                                              & " SYS_ENT_TIME,        " & vbNewLine _
                                              & " SYS_ENT_PGID,        " & vbNewLine _
                                              & " SYS_ENT_USER,        " & vbNewLine _
                                              & " SYS_UPD_DATE,        " & vbNewLine _
                                              & " SYS_UPD_TIME,        " & vbNewLine _
                                              & " SYS_UPD_PGID,        " & vbNewLine _
                                              & " SYS_UPD_USER,        " & vbNewLine _
                                              & " SYS_DEL_FLG        " & vbNewLine _
                                              & " ) VALUES (        " & vbNewLine _
                                              & " @NRS_BR_CD,        " & vbNewLine _
                                              & " @OUTKA_NO_L,        " & vbNewLine _
                                              & " @OUTKA_NO_M,        " & vbNewLine _
                                              & " @OUTKA_NO_S,        " & vbNewLine _
                                              & " @TOU_NO,        " & vbNewLine _
                                              & " @SITU_NO,        " & vbNewLine _
                                              & " @ZONE_CD,        " & vbNewLine _
                                              & " @LOCA,        " & vbNewLine _
                                              & " @LOT_NO,        " & vbNewLine _
                                              & " @SERIAL_NO,        " & vbNewLine _
                                              & " @OUTKA_TTL_NB,        " & vbNewLine _
                                              & " @OUTKA_TTL_QT,        " & vbNewLine _
                                              & " @ZAI_REC_NO,        " & vbNewLine _
                                              & " @INKA_NO_L,        " & vbNewLine _
                                              & " @INKA_NO_M,        " & vbNewLine _
                                              & " @INKA_NO_S,        " & vbNewLine _
                                              & " @ZAI_UPD_FLAG,        " & vbNewLine _
                                              & " @ALCTD_CAN_NB,        " & vbNewLine _
                                              & " @ALCTD_NB,        " & vbNewLine _
                                              & " @ALCTD_CAN_QT,        " & vbNewLine _
                                              & " @ALCTD_QT,        " & vbNewLine _
                                              & " @IRIME,        " & vbNewLine _
                                              & " @BETU_WT,        " & vbNewLine _
                                              & " @COA_FLAG,        " & vbNewLine _
                                              & " @REMARK,        " & vbNewLine _
                                              & " @SMPL_FLAG,        " & vbNewLine _
                                              & " @SYS_ENT_DATE,        " & vbNewLine _
                                              & " @SYS_ENT_TIME,        " & vbNewLine _
                                              & " @SYS_ENT_PGID,        " & vbNewLine _
                                              & " @SYS_ENT_USER,        " & vbNewLine _
                                              & " @SYS_UPD_DATE,        " & vbNewLine _
                                              & " @SYS_UPD_TIME,        " & vbNewLine _
                                              & " @SYS_UPD_PGID,        " & vbNewLine _
                                              & " @SYS_UPD_USER,        " & vbNewLine _
                                              & " @SYS_DEL_FLG        " & vbNewLine _
                                              & " )        " & vbNewLine

#End Region '出荷データを更新'

#Region "入荷データを更新"

    ''' <summary>
    ''' 入荷データ（大）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKA_L As String = "INSERT INTO $LM_TRN$..B_INKA_L        " & vbNewLine _
                                              & " ( 		           " & vbNewLine _
                                              & " NRS_BR_CD, 		           " & vbNewLine _
                                              & " INKA_NO_L, 		           " & vbNewLine _
                                              & " FURI_NO, 		           " & vbNewLine _
                                              & " INKA_TP,           " & vbNewLine _
                                              & " INKA_KB,           " & vbNewLine _
                                              & " INKA_STATE_KB,       " & vbNewLine _
                                              & " INKA_DATE," & vbNewLine _
                                              & " WH_CD,           " & vbNewLine _
                                              & " CUST_CD_L,           " & vbNewLine _
                                              & " CUST_CD_M,           " & vbNewLine _
                                              & " INKA_PLAN_QT,           " & vbNewLine _
                                              & " INKA_PLAN_QT_UT,           " & vbNewLine _
                                              & " INKA_TTL_NB,           " & vbNewLine _
                                              & " BUYER_ORD_NO_L,           " & vbNewLine _
                                              & " OUTKA_FROM_ORD_NO_L,           " & vbNewLine _
                                              & " SEIQTO_CD,           " & vbNewLine _
                                              & " TOUKI_HOKAN_YN,           " & vbNewLine _
                                              & " HOKAN_YN,           " & vbNewLine _
                                              & " HOKAN_FREE_KIKAN,           " & vbNewLine _
                                              & " HOKAN_STR_DATE,           " & vbNewLine _
                                              & " NIYAKU_YN,           " & vbNewLine _
                                              & " TAX_KB,           " & vbNewLine _
                                              & " REMARK,           " & vbNewLine _
                                              & " REMARK_OUT,        " & vbNewLine _
                                              & " CHECKLIST_PRT_DATE,           " & vbNewLine _
                                              & " CHECKLIST_PRT_USER,        " & vbNewLine _
                                              & " UKETSUKELIST_PRT_DATE,           " & vbNewLine _
                                              & " UKETSUKELIST_PRT_USER,        " & vbNewLine _
                                              & " UKETSUKE_DATE,           " & vbNewLine _
                                              & " UKETSUKE_USER,        " & vbNewLine _
                                              & " KEN_DATE,           " & vbNewLine _
                                              & " KEN_USER,        " & vbNewLine _
                                              & " INKO_DATE,           " & vbNewLine _
                                              & " INKO_USER,        " & vbNewLine _
                                              & " HOUKOKUSYO_PR_DATE,           " & vbNewLine _
                                              & " HOUKOKUSYO_PR_USER,       " & vbNewLine _
                                              & " UNCHIN_TP,           " & vbNewLine _
                                              & " UNCHIN_KB,        " & vbNewLine _
                                              & " SYS_ENT_DATE,        " & vbNewLine _
                                              & " SYS_ENT_TIME,        " & vbNewLine _
                                              & " SYS_ENT_PGID,        " & vbNewLine _
                                              & " SYS_ENT_USER,        " & vbNewLine _
                                              & " SYS_UPD_DATE,        " & vbNewLine _
                                              & " SYS_UPD_TIME,        " & vbNewLine _
                                              & " SYS_UPD_PGID,        " & vbNewLine _
                                              & " SYS_UPD_USER,        " & vbNewLine _
                                              & " SYS_DEL_FLG        " & vbNewLine _
                                              & " ) VALUES (        " & vbNewLine _
                                              & " @NRS_BR_CD,        " & vbNewLine _
                                              & " @INKA_NO_L,        " & vbNewLine _
                                              & " @FURI_NO,        " & vbNewLine _
                                              & " @INKA_TP,        " & vbNewLine _
                                              & " @INKA_KB,        " & vbNewLine _
                                              & " @INKA_STATE_KB,        " & vbNewLine _
                                              & " @INKA_DATE,        " & vbNewLine _
                                              & " @WH_CD,        " & vbNewLine _
                                              & " @CUST_CD_L,        " & vbNewLine _
                                              & " @CUST_CD_M,        " & vbNewLine _
                                              & " @INKA_PLAN_QT,        " & vbNewLine _
                                              & " @INKA_PLAN_QT_UT,        " & vbNewLine _
                                              & " @INKA_TTL_NB,        " & vbNewLine _
                                              & " @BUYER_ORD_NO_L,        " & vbNewLine _
                                              & " @OUTKA_FROM_ORD_NO_L,        " & vbNewLine _
                                              & " @SEIQTO_CD,        " & vbNewLine _
                                              & " @TOUKI_HOKAN_YN,        " & vbNewLine _
                                              & " @HOKAN_YN,        " & vbNewLine _
                                              & " @HOKAN_FREE_KIKAN,        " & vbNewLine _
                                              & " @HOKAN_STR_DATE,        " & vbNewLine _
                                              & " @NIYAKU_YN,        " & vbNewLine _
                                              & " @TAX_KB,        " & vbNewLine _
                                              & " @REMARK,        " & vbNewLine _
                                              & " @REMARK_OUT,        " & vbNewLine _
                                              & " @CHECKLIST_PRT_DATE,        " & vbNewLine _
                                              & " @CHECKLIST_PRT_USER,        " & vbNewLine _
                                              & " @UKETSUKELIST_PRT_DATE,        " & vbNewLine _
                                              & " @UKETSUKELIST_PRT_USER,        " & vbNewLine _
                                              & " @UKETSUKE_DATE,        " & vbNewLine _
                                              & " @UKETSUKE_USER,        " & vbNewLine _
                                              & " @KEN_DATE,        " & vbNewLine _
                                              & " @KEN_USER,        " & vbNewLine _
                                              & " @INKO_DATE,        " & vbNewLine _
                                              & " @INKO_USER,        " & vbNewLine _
                                              & " @HOUKOKUSYO_PR_DATE,        " & vbNewLine _
                                              & " @HOUKOKUSYO_PR_USER,        " & vbNewLine _
                                              & " @UNCHIN_TP,        " & vbNewLine _
                                              & " @UNCHIN_KB,        " & vbNewLine _
                                              & " @SYS_ENT_DATE,        " & vbNewLine _
                                              & " @SYS_ENT_TIME,        " & vbNewLine _
                                              & " @SYS_ENT_PGID,        " & vbNewLine _
                                              & " @SYS_ENT_USER,        " & vbNewLine _
                                              & " @SYS_UPD_DATE,        " & vbNewLine _
                                              & " @SYS_UPD_TIME,        " & vbNewLine _
                                              & " @SYS_UPD_PGID,        " & vbNewLine _
                                              & " @SYS_UPD_USER,        " & vbNewLine _
                                              & " @SYS_DEL_FLG        " & vbNewLine _
                                              & " )        " & vbNewLine


    ''' <summary>
    ''' 入荷データ（中）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKA_M As String = "INSERT INTO $LM_TRN$..B_INKA_M        " & vbNewLine _
                                              & " ( 		           " & vbNewLine _
                                              & " NRS_BR_CD, 		           " & vbNewLine _
                                              & " INKA_NO_L, 		           " & vbNewLine _
                                              & " INKA_NO_M, 		           " & vbNewLine _
                                              & " GOODS_CD_NRS,           " & vbNewLine _
                                              & " OUTKA_FROM_ORD_NO_M,           " & vbNewLine _
                                              & " BUYER_ORD_NO_M,           " & vbNewLine _
                                              & " REMARK,           " & vbNewLine _
                                              & " PRINT_SORT,           " & vbNewLine _
                                              & " SYS_ENT_DATE,        " & vbNewLine _
                                              & " SYS_ENT_TIME,        " & vbNewLine _
                                              & " SYS_ENT_PGID,        " & vbNewLine _
                                              & " SYS_ENT_USER,        " & vbNewLine _
                                              & " SYS_UPD_DATE,        " & vbNewLine _
                                              & " SYS_UPD_TIME,        " & vbNewLine _
                                              & " SYS_UPD_PGID,        " & vbNewLine _
                                              & " SYS_UPD_USER,        " & vbNewLine _
                                              & " SYS_DEL_FLG        " & vbNewLine _
                                              & " ) VALUES (        " & vbNewLine _
                                              & " @NRS_BR_CD,        " & vbNewLine _
                                              & " @INKA_NO_L,        " & vbNewLine _
                                              & " @INKA_NO_M,        " & vbNewLine _
                                              & " @GOODS_CD_NRS,        " & vbNewLine _
                                              & " @OUTKA_FROM_ORD_NO_M,        " & vbNewLine _
                                              & " @BUYER_ORD_NO_M,        " & vbNewLine _
                                              & " @REMARK,        " & vbNewLine _
                                              & " @PRINT_SORT,        " & vbNewLine _
                                              & " @SYS_ENT_DATE,        " & vbNewLine _
                                              & " @SYS_ENT_TIME,        " & vbNewLine _
                                              & " @SYS_ENT_PGID,        " & vbNewLine _
                                              & " @SYS_ENT_USER,        " & vbNewLine _
                                              & " @SYS_UPD_DATE,        " & vbNewLine _
                                              & " @SYS_UPD_TIME,        " & vbNewLine _
                                              & " @SYS_UPD_PGID,        " & vbNewLine _
                                              & " @SYS_UPD_USER,        " & vbNewLine _
                                              & " @SYS_DEL_FLG        " & vbNewLine _
                                              & " )        " & vbNewLine


    ''' <summary>
    ''' 入荷データ（小）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_INKA_S As String = "INSERT INTO $LM_TRN$..B_INKA_S        " & vbNewLine _
                                              & " ( 		           " & vbNewLine _
                                              & " NRS_BR_CD, 		           " & vbNewLine _
                                              & " INKA_NO_L, 		           " & vbNewLine _
                                              & " INKA_NO_M, 		           " & vbNewLine _
                                              & " INKA_NO_S,           " & vbNewLine _
                                              & " ZAI_REC_NO,           " & vbNewLine _
                                              & " LOT_NO,       " & vbNewLine _
                                              & " LOCA," & vbNewLine _
                                              & " TOU_NO,           " & vbNewLine _
                                              & " SITU_NO,           " & vbNewLine _
                                              & " ZONE_CD,           " & vbNewLine _
                                              & " KONSU,           " & vbNewLine _
                                              & " HASU,           " & vbNewLine _
                                              & " IRIME,           " & vbNewLine _
                                              & " BETU_WT,           " & vbNewLine _
                                              & " SERIAL_NO,           " & vbNewLine _
                                              & " GOODS_COND_KB_1,           " & vbNewLine _
                                              & " GOODS_COND_KB_2,           " & vbNewLine _
                                              & " GOODS_COND_KB_3,           " & vbNewLine _
                                              & " GOODS_CRT_DATE,           " & vbNewLine _
                                              & " LT_DATE,           " & vbNewLine _
                                              & " SPD_KB,           " & vbNewLine _
                                              & " OFB_KB,           " & vbNewLine _
                                              & " DEST_CD,        " & vbNewLine _
                                              & " REMARK,           " & vbNewLine _
                                              & " ALLOC_PRIORITY,        " & vbNewLine _
                                              & " REMARK_OUT,           " & vbNewLine _
                                              & " SYS_ENT_DATE,        " & vbNewLine _
                                              & " SYS_ENT_TIME,        " & vbNewLine _
                                              & " SYS_ENT_PGID,        " & vbNewLine _
                                              & " SYS_ENT_USER,        " & vbNewLine _
                                              & " SYS_UPD_DATE,        " & vbNewLine _
                                              & " SYS_UPD_TIME,        " & vbNewLine _
                                              & " SYS_UPD_PGID,        " & vbNewLine _
                                              & " SYS_UPD_USER,        " & vbNewLine _
                                              & " SYS_DEL_FLG        " & vbNewLine _
                                              & " ) VALUES (        " & vbNewLine _
                                              & " @NRS_BR_CD,        " & vbNewLine _
                                              & " @INKA_NO_L,        " & vbNewLine _
                                              & " @INKA_NO_M,        " & vbNewLine _
                                              & " @INKA_NO_S,        " & vbNewLine _
                                              & " @ZAI_REC_NO,        " & vbNewLine _
                                              & " @LOT_NO,        " & vbNewLine _
                                              & " @LOCA,        " & vbNewLine _
                                              & " @TOU_NO,        " & vbNewLine _
                                              & " @SITU_NO,        " & vbNewLine _
                                              & " @ZONE_CD,        " & vbNewLine _
                                              & " @KONSU,        " & vbNewLine _
                                              & " @HASU,        " & vbNewLine _
                                              & " @IRIME,        " & vbNewLine _
                                              & " @BETU_WT,        " & vbNewLine _
                                              & " @SERIAL_NO,        " & vbNewLine _
                                              & " @GOODS_COND_KB_1,        " & vbNewLine _
                                              & " @GOODS_COND_KB_2,        " & vbNewLine _
                                              & " @GOODS_COND_KB_3,        " & vbNewLine _
                                              & " @GOODS_CRT_DATE,        " & vbNewLine _
                                              & " @LT_DATE,        " & vbNewLine _
                                              & " @SPD_KB,        " & vbNewLine _
                                              & " @OFB_KB,        " & vbNewLine _
                                              & " @DEST_CD,        " & vbNewLine _
                                              & " @REMARK,        " & vbNewLine _
                                              & " @ALLOC_PRIORITY,        " & vbNewLine _
                                              & " @REMARK_OUT,        " & vbNewLine _
                                              & " @SYS_ENT_DATE,        " & vbNewLine _
                                              & " @SYS_ENT_TIME,        " & vbNewLine _
                                              & " @SYS_ENT_PGID,        " & vbNewLine _
                                              & " @SYS_ENT_USER,        " & vbNewLine _
                                              & " @SYS_UPD_DATE,        " & vbNewLine _
                                              & " @SYS_UPD_TIME,        " & vbNewLine _
                                              & " @SYS_UPD_PGID,        " & vbNewLine _
                                              & " @SYS_UPD_USER,        " & vbNewLine _
                                              & " @SYS_DEL_FLG        " & vbNewLine _
                                              & " )        " & vbNewLine

#End Region

#Region "在庫データを更新"

    ''' <summary>
    ''' 在庫データ（振替元）を登録（UPDATA句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATA_MOTO_ZAITRS As String = "UPDATE $LM_TRN$..D_ZAI_TRS SET        " & vbNewLine _
                                              & " PORA_ZAI_NB 		= @PORA_ZAI_NB,         " & vbNewLine _
                                              & " ALLOC_CAN_NB = @ALLOC_CAN_NB,             " & vbNewLine _
                                              & " PORA_ZAI_QT = @PORA_ZAI_QT,               " & vbNewLine _
                                              & " ALLOC_CAN_QT = @ALLOC_CAN_QT,             " & vbNewLine _
                                              & " TAX_KB = @TAX_KB,             " & vbNewLine _
                                              & " SYS_UPD_DATE = @SYS_UPD_DATE,             " & vbNewLine _
                                              & " SYS_UPD_TIME = @SYS_UPD_TIME,             " & vbNewLine _
                                              & " SYS_UPD_PGID = @SYS_UPD_PGID,             " & vbNewLine _
                                              & " SYS_UPD_USER = @SYS_UPD_USER              " & vbNewLine _
                                              & " WHERE                                     " & vbNewLine _
                                              & " NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND ZAI_REC_NO = @ZAI_REC_NO              " & vbNewLine _
                                              & " AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE      " & vbNewLine _
                                              & " AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME      " & vbNewLine


    ''' <summary>
    ''' 在庫データ（振替先）を登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAKI_ZAITRS As String = "INSERT INTO $LM_TRN$..D_ZAI_TRS        " & vbNewLine _
                                   & "(                                                      " & vbNewLine _
                                   & "       NRS_BR_CD                                       " & vbNewLine _
                                   & "      ,ZAI_REC_NO                                      " & vbNewLine _
                                   & "      ,WH_CD                                           " & vbNewLine _
                                   & "      ,TOU_NO                                          " & vbNewLine _
                                   & "      ,SITU_NO                                         " & vbNewLine _
                                   & "      ,ZONE_CD                                         " & vbNewLine _
                                   & "      ,LOCA                                            " & vbNewLine _
                                   & "      ,LOT_NO                                          " & vbNewLine _
                                   & "      ,CUST_CD_L                                       " & vbNewLine _
                                   & "      ,CUST_CD_M                                       " & vbNewLine _
                                   & "      ,GOODS_CD_NRS                                    " & vbNewLine _
                                   & "      ,GOODS_KANRI_NO                                  " & vbNewLine _
                                   & "      ,INKA_NO_L                                       " & vbNewLine _
                                   & "      ,INKA_NO_M                                       " & vbNewLine _
                                   & "      ,INKA_NO_S                                       " & vbNewLine _
                                   & "      ,ALLOC_PRIORITY                                  " & vbNewLine _
                                   & "      ,RSV_NO                                          " & vbNewLine _
                                   & "      ,SERIAL_NO                                       " & vbNewLine _
                                   & "      ,HOKAN_YN                                        " & vbNewLine _
                                   & "      ,TAX_KB                                          " & vbNewLine _
                                   & "      ,GOODS_COND_KB_1                                 " & vbNewLine _
                                   & "      ,GOODS_COND_KB_2                                 " & vbNewLine _
                                   & "      ,GOODS_COND_KB_3                                 " & vbNewLine _
                                   & "      ,OFB_KB                                          " & vbNewLine _
                                   & "      ,SPD_KB                                          " & vbNewLine _
                                   & "      ,REMARK_OUT                                      " & vbNewLine _
                                   & "      ,PORA_ZAI_NB                                     " & vbNewLine _
                                   & "      ,ALCTD_NB                                        " & vbNewLine _
                                   & "      ,ALLOC_CAN_NB                                    " & vbNewLine _
                                   & "      ,IRIME                                           " & vbNewLine _
                                   & "      ,PORA_ZAI_QT                                     " & vbNewLine _
                                   & "      ,ALCTD_QT                                        " & vbNewLine _
                                   & "      ,ALLOC_CAN_QT                                    " & vbNewLine _
                                   & "      ,INKO_DATE                                       " & vbNewLine _
                                   & "      ,INKO_PLAN_DATE                                  " & vbNewLine _
                                   & "      ,ZERO_FLAG                                       " & vbNewLine _
                                   & "      ,LT_DATE                                         " & vbNewLine _
                                   & "      ,GOODS_CRT_DATE                                  " & vbNewLine _
                                   & "      ,DEST_CD_P                                       " & vbNewLine _
                                   & "      ,REMARK                                          " & vbNewLine _
                                   & "      ,SMPL_FLAG                                       " & vbNewLine _
                                   & "      ,BYK_KEEP_GOODS_CD                               " & vbNewLine _
                                   & "      ,SYS_ENT_DATE                                    " & vbNewLine _
                                   & "      ,SYS_ENT_TIME                                    " & vbNewLine _
                                   & "      ,SYS_ENT_PGID                                    " & vbNewLine _
                                   & "      ,SYS_ENT_USER                                    " & vbNewLine _
                                   & "      ,SYS_UPD_DATE                                    " & vbNewLine _
                                   & "      ,SYS_UPD_TIME                                    " & vbNewLine _
                                   & "      ,SYS_UPD_PGID                                    " & vbNewLine _
                                   & "      ,SYS_UPD_USER                                    " & vbNewLine _
                                   & "      ,SYS_DEL_FLG                                     " & vbNewLine _
                                   & "       ) VALUES (" & vbNewLine _
                                   & "       @NRS_BR_CD" & vbNewLine _
                                   & "      ,@ZAI_REC_NO" & vbNewLine _
                                   & "      ,@WH_CD" & vbNewLine _
                                   & "      ,@TOU_NO" & vbNewLine _
                                   & "      ,@SITU_NO" & vbNewLine _
                                   & "      ,@ZONE_CD" & vbNewLine _
                                   & "      ,@LOCA" & vbNewLine _
                                   & "      ,@LOT_NO" & vbNewLine _
                                   & "      ,@CUST_CD_L" & vbNewLine _
                                   & "      ,@CUST_CD_M" & vbNewLine _
                                   & "      ,@GOODS_CD_NRS" & vbNewLine _
                                   & "      ,@GOODS_KANRI_NO" & vbNewLine _
                                   & "      ,@INKA_NO_L" & vbNewLine _
                                   & "      ,@INKA_NO_M" & vbNewLine _
                                   & "      ,@INKA_NO_S" & vbNewLine _
                                   & "      ,@ALLOC_PRIORITY" & vbNewLine _
                                   & "      ,@RSV_NO" & vbNewLine _
                                   & "      ,@SERIAL_NO" & vbNewLine _
                                   & "      ,@HOKAN_YN" & vbNewLine _
                                   & "      ,@TAX_KB" & vbNewLine _
                                   & "      ,@GOODS_COND_KB_1" & vbNewLine _
                                   & "      ,@GOODS_COND_KB_2" & vbNewLine _
                                   & "      ,@GOODS_COND_KB_3" & vbNewLine _
                                   & "      ,@OFB_KB" & vbNewLine _
                                   & "      ,@SPD_KB" & vbNewLine _
                                   & "      ,@REMARK_OUT" & vbNewLine _
                                   & "      ,@PORA_ZAI_NB" & vbNewLine _
                                   & "      ,@ALCTD_NB" & vbNewLine _
                                   & "      ,@ALLOC_CAN_NB" & vbNewLine _
                                   & "      ,@IRIME" & vbNewLine _
                                   & "      ,@PORA_ZAI_QT" & vbNewLine _
                                   & "      ,@ALCTD_QT" & vbNewLine _
                                   & "      ,@ALLOC_CAN_QT" & vbNewLine _
                                   & "      ,@INKO_DATE" & vbNewLine _
                                   & "      ,@INKO_PLAN_DATE" & vbNewLine _
                                   & "      ,@ZERO_FLAG" & vbNewLine _
                                   & "      ,@LT_DATE" & vbNewLine _
                                   & "      ,@GOODS_CRT_DATE" & vbNewLine _
                                   & "      ,@DEST_CD_P" & vbNewLine _
                                   & "      ,@REMARK" & vbNewLine _
                                   & "      ,@SMPL_FLAG" & vbNewLine _
                                   & "      ,@BYK_KEEP_GOODS_CD " & vbNewLine _
                                   & "      ,@SYS_ENT_DATE" & vbNewLine _
                                   & "      ,@SYS_ENT_TIME" & vbNewLine _
                                   & "      ,@SYS_ENT_PGID" & vbNewLine _
                                   & "      ,@SYS_ENT_USER" & vbNewLine _
                                   & "      ,@SYS_UPD_DATE" & vbNewLine _
                                   & "      ,@SYS_UPD_TIME" & vbNewLine _
                                   & "      ,@SYS_UPD_PGID" & vbNewLine _
                                   & "      ,@SYS_UPD_USER" & vbNewLine _
                                   & "      ,@SYS_DEL_FLG" & vbNewLine _
                                   & "      )" & vbNewLine



#End Region

#Region "作業データを更新"

    ''' <summary>
    ''' 作業データを登録（INSERT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO $LM_TRN$..E_SAGYO        " & vbNewLine _
                                       & "(                                 " & vbNewLine _
                                       & "       NRS_BR_CD" & vbNewLine _
                                       & "      ,SAGYO_REC_NO" & vbNewLine _
                                       & "      ,SAGYO_COMP" & vbNewLine _
                                       & "      ,SKYU_CHK" & vbNewLine _
                                       & "      ,SAGYO_SIJI_NO" & vbNewLine _
                                       & "      ,INOUTKA_NO_LM" & vbNewLine _
                                       & "      ,WH_CD" & vbNewLine _
                                       & "      ,IOZS_KB" & vbNewLine _
                                       & "      ,SAGYO_CD" & vbNewLine _
                                       & "      ,SAGYO_NM" & vbNewLine _
                                       & "      ,CUST_CD_L" & vbNewLine _
                                       & "      ,CUST_CD_M" & vbNewLine _
                                       & "      ,DEST_CD" & vbNewLine _
                                       & "      ,DEST_NM" & vbNewLine _
                                       & "      ,GOODS_CD_NRS" & vbNewLine _
                                       & "      ,GOODS_NM_NRS" & vbNewLine _
                                       & "      ,LOT_NO" & vbNewLine _
                                       & "      ,INV_TANI" & vbNewLine _
                                       & "      ,SAGYO_NB" & vbNewLine _
                                       & "      ,SAGYO_UP" & vbNewLine _
                                       & "      ,SAGYO_GK" & vbNewLine _
                                       & "      ,TAX_KB" & vbNewLine _
                                       & "      ,SEIQTO_CD" & vbNewLine _
                                       & "      ,REMARK_ZAI" & vbNewLine _
                                       & "      ,REMARK_SKYU" & vbNewLine _
                                       & "      ,SAGYO_COMP_CD" & vbNewLine _
                                       & "      ,SAGYO_COMP_DATE" & vbNewLine _
                                       & "      ,DEST_SAGYO_FLG" & vbNewLine _
                                       & "      ,SYS_ENT_DATE" & vbNewLine _
                                       & "      ,SYS_ENT_TIME" & vbNewLine _
                                       & "      ,SYS_ENT_PGID" & vbNewLine _
                                       & "      ,SYS_ENT_USER" & vbNewLine _
                                       & "      ,SYS_UPD_DATE" & vbNewLine _
                                       & "      ,SYS_UPD_TIME" & vbNewLine _
                                       & "      ,SYS_UPD_PGID" & vbNewLine _
                                       & "      ,SYS_UPD_USER" & vbNewLine _
                                       & "      ,SYS_DEL_FLG" & vbNewLine _
                                       & "       ) VALUES (" & vbNewLine _
                                       & "       @NRS_BR_CD" & vbNewLine _
                                       & "      ,@SAGYO_REC_NO" & vbNewLine _
                                       & "      ,@SAGYO_COMP" & vbNewLine _
                                       & "      ,@SKYU_CHK" & vbNewLine _
                                       & "      ,@SAGYO_SIJI_NO" & vbNewLine _
                                       & "      ,@INOUTKA_NO_LM" & vbNewLine _
                                       & "      ,@WH_CD" & vbNewLine _
                                       & "      ,@IOZS_KB" & vbNewLine _
                                       & "      ,@SAGYO_CD" & vbNewLine _
                                       & "      ,@SAGYO_NM" & vbNewLine _
                                       & "      ,@CUST_CD_L" & vbNewLine _
                                       & "      ,@CUST_CD_M" & vbNewLine _
                                       & "      ,@DEST_CD" & vbNewLine _
                                       & "      ,@DEST_NM" & vbNewLine _
                                       & "      ,@GOODS_CD_NRS" & vbNewLine _
                                       & "      ,@GOODS_NM_NRS" & vbNewLine _
                                       & "      ,@LOT_NO" & vbNewLine _
                                       & "      ,@INV_TANI" & vbNewLine _
                                       & "      ,@SAGYO_NB" & vbNewLine _
                                       & "      ,@SAGYO_UP" & vbNewLine _
                                       & "      ,@SAGYO_GK" & vbNewLine _
                                       & "      ,@TAX_KB" & vbNewLine _
                                       & "      ,@SEIQTO_CD" & vbNewLine _
                                       & "      ,@REMARK_ZAI" & vbNewLine _
                                       & "      ,@REMARK_SKYU" & vbNewLine _
                                       & "      ,@SAGYO_COMP_CD" & vbNewLine _
                                       & "      ,@SAGYO_COMP_DATE" & vbNewLine _
                                       & "      ,@DEST_SAGYO_FLG" & vbNewLine _
                                       & "      ,@SYS_ENT_DATE" & vbNewLine _
                                       & "      ,@SYS_ENT_TIME" & vbNewLine _
                                       & "      ,@SYS_ENT_PGID" & vbNewLine _
                                       & "      ,@SYS_ENT_USER" & vbNewLine _
                                       & "      ,@SYS_UPD_DATE" & vbNewLine _
                                       & "      ,@SYS_UPD_TIME" & vbNewLine _
                                       & "      ,@SYS_UPD_PGID" & vbNewLine _
                                       & "      ,@SYS_UPD_USER" & vbNewLine _
                                       & "      ,@SYS_DEL_FLG" & vbNewLine _
                                       & "      )" & vbNewLine


#End Region '作業データを更新'

#Region "在庫レコード番号に紐づく移動日を取得"

    'START YANAI 要望番号1038 振替の振替元確定時チェックで倉移動の削除フラグが判断されているため日付チェックに引っ掛かってしまう
    'Public Const SQL_SELECT_IDO_DATE As String = "SELECT IDO_DATE AS IDO_DATE                                       " & vbNewLine _
    '                                           & " FROM  $LM_TRN$..D_IDO_TRS                                        " & vbNewLine _
    '                                           & " WHERE N_ZAI_REC_NO = @ZAI_REC_NO OR O_ZAI_REC_NO = @ZAI_REC_NO   " & vbNewLine
    Public Const SQL_SELECT_IDO_DATE As String = "SELECT IDO.IDO_DATE AS IDO_DATE                                   " & vbNewLine _
                                               & " FROM  $LM_TRN$..D_IDO_TRS IDO                                    " & vbNewLine _
                                               & " WHERE (IDO.N_ZAI_REC_NO = @ZAI_REC_NO OR IDO.O_ZAI_REC_NO = @ZAI_REC_NO) " & vbNewLine _
                                               & "   AND IDO.SYS_DEL_FLG = '0'                                      " & vbNewLine
    'END YANAI 要望番号1038 振替の振替元確定時チェックで倉移動の削除フラグが判断されているため日付チェックに引っ掛かってしまう


#End Region

#Region "荷主明細 取得"

    Public Const SQL_SELECT_CUST_DETAILS As String = "" _
        & " SELECT                                " & vbNewLine _
        & "       SET_NAIYO                       " & vbNewLine _
        & "     , SET_NAIYO_2                     " & vbNewLine _
        & "     , SET_NAIYO_3                     " & vbNewLine _
        & "     , REMARK                          " & vbNewLine _
        & " FROM                                  " & vbNewLine _
        & "     $LM_MST$..M_CUST_DETAILS          " & vbNewLine _
        & " WHERE                                 " & vbNewLine _
        & "     NRS_BR_CD = @NRS_BR_CD            " & vbNewLine _
        & " AND CUST_CD = @CUST_CD_L + @CUST_CD_M " & vbNewLine _
        & " AND CUST_CLASS = @CUST_CLASS          " & vbNewLine _
        & " AND SUB_KB = @SUB_KB                  " & vbNewLine _
        & " AND SYS_DEL_FLG = '0'                 " & vbNewLine _
        & ""

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
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row2 As Data.DataRow

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

#Region "確定処理"

    ''' <summary>
    ''' 振替データ作成用のデータを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function FuriMakeData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'Me._Row2 = inTbl.Rows(1)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQL作成
        Me._StrSql.Append(LMD010DAC.SQL_SELECT_LMD010)      'SQL構築(データ抽出用Select句1)
        Me._StrSql.Append(LMD010DAC.SQL_FROM_LMD010)        'SQL構築(データ抽出用From)


        Call Me.SetParamSelect()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "FuriMakeData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("IRIME", "IRIME")
        map.Add("IRIME_UT", "IRIME_UT")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("SAKI_GOODS_CD_NRS", "SAKI_GOODS_CD_NRS")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")
        map.Add("OYA_SEIQTO_CD", "OYA_SEIQTO_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD010OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD010OUT").Rows.Count())

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 振替元在庫データを取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FuriZaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_ZAI_OLDIN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD010DAC.SQL_SELECT_LMD010_ZAI_OLD)      'SQL構築(データ抽出用Select句1)
        Me._StrSql.Append(LMD010DAC.SQL_FROM_LMD010_ZAI_OLD)        'SQL構築(データ抽出用From)
        'SQLパラメータ初期化/設定
        Call Me.SetCondition()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "FuriZaiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("ZAI_REC_NO", "ZAI_REC_NO")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("ZONE_CD", "ZONE_CD")
        map.Add("LOCA", "LOCA")
        map.Add("LOT_NO", "LOT_NO")
        'START ADD 2013/09/10 KOBAYASHI WIT対応
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_KANRI_NO", "GOODS_KANRI_NO")
        'END   ADD 2013/09/10 KOBAYASHI WIT対応
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("INKA_NO_S", "INKA_NO_S")
        map.Add("RSV_NO", "RSV_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("HOKAN_YN", "HOKAN_YN")
        map.Add("PORA_ZAI_NB", "PORA_ZAI_NB")
        map.Add("ALLOC_CAN_NB", "ALLOC_CAN_NB")
        map.Add("IRIME", "IRIME")
        map.Add("PORA_ZAI_QT", "PORA_ZAI_QT")
        map.Add("ALCTD_QT", "ALCTD_QT")
        map.Add("ALLOC_CAN_QT", "ALLOC_CAN_QT")
        map.Add("REMARK", "REMARK")
        map.Add("SMPL_FLAG", "SMPL_FLAG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD010_ZAI_OLDOUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD010_ZAI_OLDOUT").Rows.Count())

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 最新の請求日検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectSeiqDate(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_KAGAMI_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '元、先ともに作業がない場合、スルー
        If ds.Tables("LMD010_SAGYO_OUTKA").Rows.Count < 1 _
            AndAlso ds.Tables("LMD010_SAGYO_INKA").Rows.Count < 1 Then
            Return ds
        End If

        '作業料取込チェック用の日付を取得する
        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMG000DAC.SQL_SELECT_SAGYO_CHK_DATE)

        Call Me.SetComSelectChkDateParamSAGYO()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "SelectSeiqDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        map = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD010_SAGYO_SKYU_DATE")

        Return ds

    End Function

    ''' <summary>
    ''' 移動日検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectIdoDate(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_IDO_TRS_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMD010DAC.SQL_SELECT_IDO_DATE)         'SQL構築(データ抽出用Select句1)
        Call Me.SetComSelectIdoDateParam()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "SelectIdoDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("IDO_DATE", "IDO_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD010_IDO_TRS_OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMD010_IDO_TRS_OUT").Rows.Count())

        Return ds

    End Function

    ''' <summary>
    ''' 荷主明細 BYKキープ品管理 有無 検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function SelectIsBykKeepGoodsCd(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_CUST_DETAILS_IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成

        ' SQL構築(データ抽出用Select句1)
        Me._StrSql.Append(LMD010DAC.SQL_SELECT_CUST_DETAILS)
        Call Me.SetSelectIsCustDetailsParam()

        Dim resultCount As Integer = 0

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMD010DAC", "SelectIsBykKeepGoodsCd", cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("IS_BYK_KEEP_GOODS_CD", "SET_NAIYO")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD010_IS_BYK_KEEP_GOODS_CD")

                resultCount = ds.Tables("LMD010_IS_BYK_KEEP_GOODS_CD").Rows.Count()

            End Using

        End Using

        ' 処理件数の設定
        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Dim inTblL As DataTable = ds.Tables("LMD010IN")
        Dim inTblLRow As DataRow = inTblL.Rows(0)
        '営業所コード取得
        Dim nrsBrCd As String = inTblLRow.Item("NRS_BR_CD").ToString
        '倉庫コード取得
        Dim whCd As String = inTblLRow.Item("WH_CD").ToString
        '荷主コード取得
        Dim custCdL As String = inTblLRow.Item("SAKI_CUST_CD_L").ToString
        '移動日取得
        Dim transferDate As String = inTblLRow.Item("TRANSFER_DATE").ToString

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        Me._StrSql.Append(SQL_SELECT_TOU_SITU_EXP)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd))

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", nrsBrCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", custCdL, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRANSFER_DATE", transferDate, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "getTouSituExp", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("TOU_NO", "TOU_NO")
        map.Add("SITU_NO", "SITU_NO")
        map.Add("SERIAL_NO", "SERIAL_NO")
        map.Add("APL_DATE_FROM", "APL_DATE_FROM")
        map.Add("APL_DATE_TO", "APL_DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMD010_TOU_SITU_EXP")

        Return ds

    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCondition()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '在庫レコード番号
            whereStr = .Item("ZAI_REC_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(" ZAITRS.ZAI_REC_NO = @ZAI_REC_NO")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If


        End With

    End Sub

#If True Then       'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能
#Region "振替データ登録"


    ''' <summary>
    ''' 振替データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データ（大）新規登録SQLの構築・発行</remarks>
    Private Function InsertFurikae(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_FURIKAE")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_FURIKAE, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetFurikaeComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertFurikae", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function


#End Region
#End If


#Region "出荷データ登録"

    ''' <summary>
    ''' 出荷データ（大）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データ（大）新規登録SQLの構築・発行</remarks>
    Private Function InsertOutKaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_OUTKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_OUTLA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetOutkaLComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertOutKaL", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（中）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データ（中）新規登録SQLの構築・発行</remarks>
    Private Function InsertOutKaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_OUTKA_M")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_OUTLA_M, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetOutkaMComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertOutKaM", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（小）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データ（小）新規登録SQLの構築・発行</remarks>
    Private Function InsertOutKaS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_OUTKA_S")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_OUTLA_S, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetOutkaSComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertOutKaS", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region '出荷データ登録

#Region "入荷データ登録"

    ''' <summary>
    ''' 入荷データ（大）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データ（大）新規登録SQLの構築・発行</remarks>
    Private Function InsertInKaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_INKA_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_INKA_L, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetInkaLComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertInKaL", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データ（中）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データ（中）新規登録SQLの構築・発行</remarks>
    Private Function InsertInKaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_INKA_M")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_INKA_M, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetInkaMComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertInKaM", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データ（小）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データ（小）新規登録SQLの構築・発行</remarks>
    Private Function InsertInKaS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_INKA_S")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_INKA_S, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetInkaSComParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertInKaS", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region '入荷データ登録

#Region "在庫データ登録"

    ''' <summary>
    ''' 在庫データ（振替元）更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ（振替元）更新SQLの構築・発行</remarks>
    Private Function UpdataMotoZai(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_ZAI_OLD")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_UPDATA_MOTO_ZAITRS, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetZaiTrsComUpdateParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemUp()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "UpdataMotoZai", cmd)

        '更新出来なかった場合エラーメッセージをセットして終了
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
        End If


        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ（振替先）新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>在庫データ（振替先）新規登録SQLの構築・発行</remarks>
    Private Function InsertSakiZai(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_ZAI_NEW")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_SAKI_ZAITRS, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetZaiTrsComInsertParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertSakiZai", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region '在庫データ登録

#Region "作業データ登録"

    ''' <summary>
    ''' 振替先の作業データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業データ新規登録SQLの構築・発行</remarks>
    Private Function InsertSakiSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_SAGYO_INKA")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_SAGYO, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetSagyoParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertSakiSagyo", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 振替元の作業データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>作業データ新規登録SQLの構築・発行</remarks>
    Private Function InsertMotoSagyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD010_SAGYO_OUTKA")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm _
                                                        (LMD010DAC.SQL_INSERT_SAGYO, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ（共通項目）設定
        Call Me.SetSagyoParameter()

        'SQLパラメータ（システム項目）設定
        Call Me.SetParamCommonSystemIns()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD010DAC", "InsertMotoSagyo", cmd)

        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region '作業データ登録

#End Region '確定処理

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

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

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
    ''' パラメータ設定モジュール(ZAI_OLDIN)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索項目
        Call Me.SetComSelectParam()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(ZAI_OLDIN)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectParam()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        'START YANAI 要望番号886
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_GOODS_CD_CUST", Me._Row.Item("MOTO_GOODS_CD_CUST").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_GOODS", Me._Row.Item("SAKI_GOODS_CD_CUST").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_GOODS_CD_CUST", Me._Row.Item("MOTO_GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_GOODS", Me._Row.Item("SAKI_GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))
        'END YANAI 要望番号886
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_CD_L", Me._Row.Item("SAKI_CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_CD_M", Me._Row.Item("SAKI_CUST_CD_M").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(SEIQ_HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectChkDateParam()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR)) '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(SEIQ_HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectChkDateParamSTORAGE()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("STORAGE_SEIQTO_CD").ToString(), DBDataType.NVARCHAR)) '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(SEIQ_HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectChkDateParamHANDLING()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("HANDLING_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(SEIQ_HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectChkDateParamSAGYO()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SAGYO_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub



    ''' <summary>
    ''' パラメータ設定モジュール(IDO_TRS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectIdoDateParam()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", Me._Row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(M_CUST_DETAILS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectIsCustDetailsParam()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CLASS", Me._Row.Item("CUST_CLASS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SUB_KB", Me._Row.Item("SUB_KB").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' INKA_Lの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaLComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", .Item("INKA_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", .Item("INKA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", .Item("INKA_PLAN_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", .Item("INKA_PLAN_QT_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", .Item("INKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", .Item("BUYER_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", .Item("HOKAN_STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_DATE", .Item("CHECKLIST_PRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_USER", .Item("CHECKLIST_PRT_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKELIST_PRT_DATE", .Item("UKETSUKELIST_PRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKELIST_PRT_USER", .Item("UKETSUKELIST_PRT_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_DATE", .Item("UKETSUKE_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UKETSUKE_USER", .Item("UKETSUKE_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_DATE", .Item("KEN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_USER", .Item("KEN_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE", .Item("INKO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_USER", .Item("INKO_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_DATE", .Item("HOUKOKUSYO_PR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_USER", .Item("HOUKOKUSYO_PR_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", .Item("UNCHIN_TP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))

        End With




    End Sub

    ''' <summary>
    ''' INKA_Mの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaMComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", .Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", .Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' INKA_Sの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaSComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KONSU", Me.FormatNumValue(.Item("KONSU").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_1", .Item("GOODS_COND_KB_1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_2", .Item("GOODS_COND_KB_2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_COND_KB_3", .Item("GOODS_COND_KB_3").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CRT_DATE", .Item("GOODS_CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LT_DATE", .Item("LT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", .Item("SPD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", .Item("OFB_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", .Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", .Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' ZAI_TRSの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComInsertParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

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
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KANRI_NO", .Item("GOODS_KANRI_NO").ToString(), DBDataType.CHAR))
            'END   ADD 2013/09/10 KOBAYASHI WIT対応
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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
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
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD_P", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BYK_KEEP_GOODS_CD", .Item("BYK_KEEP_GOODS_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' ZAI_TRSの更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZaiTrsComUpdateParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_NB", Me.FormatNumValue(.Item("PORA_ZAI_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_NB", Me.FormatNumValue(.Item("ALLOC_CAN_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PORA_ZAI_QT", Me.FormatNumValue(.Item("PORA_ZAI_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_CAN_QT", Me.FormatNumValue(.Item("ALLOC_CAN_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.FormatNumValue(.Item("TAX_KB").ToString()), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item("GUI_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item("GUI_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' SAGYOの登録パラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))    '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#If True Then   'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能

    ''' <summary>
    ''' OUTKA_Lの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFurikaeComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_DATE", .Item("FURI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_KBN", .Item("FURI_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOUKI_HENKO_KBN", .Item("YOUKI_HENKO_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KBN", .Item("TAX_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_ALLOC_KBN", .Item("HOKAN_ALLOC_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_CUST_CD_L", .Item("MOTO_CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_CUST_CD_M", .Item("MOTO_CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_ORD_NO", .Item("MOTO_ORD_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_GOODS_CD_NRS", .Item("MOTO_GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_SAGYO_CD1", .Item("MOTO_SAGYO_CD1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_SAGYO_CD2", .Item("MOTO_SAGYO_CD2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_SAGYO_CD3", .Item("MOTO_SAGYO_CD3").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO_REMARK", .Item("MOTO_REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_CD_L", .Item("SAKI_CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_CUST_CD_M", .Item("SAKI_CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_GOODS_CD_NRS", .Item("SAKI_GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_SAGYO_CD1", .Item("SAKI_SAGYO_CD1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_SAGYO_CD2", .Item("SAKI_SAGYO_CD2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_SAGYO_CD3", .Item("SAKI_SAGYO_CD3").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAKI_REMARK", .Item("SAKI_REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End If

    ''' <summary>
    ''' OUTKA_Lの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", .Item("FURI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", .Item("OUTKA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", .Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Item("PICK_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", .Item("ARR_KANRYO_INFO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@END_DATE", .Item("END_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Item("SHIP_CD_M").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Item("DENP_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", .Item("ALL_PRINT_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", .Item("NIHUDA_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", .Item("NHS_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", .Item("DENP_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Item("COA_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", .Item("HOKOKU_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", .Item("MATOME_PICK_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", .Item("LAST_PRINT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", .Item("LAST_PRINT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SASZ_USER", .Item("SASZ_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", .Item("OUTKO_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEN_USER", .Item("KEN_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", .Item("OUTKA_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOU_USER", .Item("HOU_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", .Item("ORDER_TYPE").ToString(), DBDataType.NVARCHAR))
            '2011/08/22 追加
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_KB", .Item("DEST_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' OUTKA_Mの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOutkaMComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.FormatNumValue(.Item("BACKLOG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.FormatNumValue(.Item("BACKLOG_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.FormatNumValue(.Item("OUTKA_M_PKG_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))
            '2011/08/22 追加
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", .Item("GOODS_CD_NRS_FROM").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' OUTKA_Sの登録パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOutkaSComParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_S", .Item("OUTKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOU_NO", .Item("TOU_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SITU_NO", .Item("SITU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZONE_CD", .Item("ZONE_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOCA", .Item("LOCA").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", .Item("INKA_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", .Item("INKA_NO_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_UPD_FLAG", .Item("ZAI_UPD_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_NB", Me.FormatNumValue(.Item("ALCTD_CAN_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_CAN_QT", Me.FormatNumValue(.Item("ALCTD_CAN_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_FLAG", .Item("COA_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SMPL_FLAG", .Item("SMPL_FLAG").ToString(), DBDataType.CHAR))

        End With

    End Sub

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

#End Region 'SQL

#End Region 'Method

End Class
