' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG050C : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMG050定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMG050IN"
    Friend Const TABLE_NM_HED As String = "LMG050HED"
    Friend Const TABLE_NM_DTL As String = "LMG050DTL"
    Friend Const TABLE_NM_SEIQTO As String = "LMG050_SEIQTOM"
    Friend Const TABLE_NM_IN_TSMC As String = "LMG050IN_TSMC"

    ''' <summary>
    ''' 進捗区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const STATE_MIKAKUTEI As String = "00"           '未確定
    Friend Const STATE_KAKUTEI As String = "01"             '確定
    Friend Const STATE_INSATU_ZUMI As String = "02"         '印刷済
    Friend Const STATE_KEIRI_TORIKOMI_ZUMI As String = "03" '経理取込済
    Friend Const STATE_KEIRI_TAISHO_GAI As String = "04"    '経理取込対象外

    ''' <summary>
    ''' 赤黒区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const KURO_DEN As String = "00"                  '黒伝票
    Friend Const AKA_DEN As String = "01"                   '赤伝票

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_SEIKYU_SHO As String = "01"          '請求書
    Friend Const PRINT_CHECK_LIST As String = "02"          'チェックリスト
    Friend Const PRINT_IKKATSU As String = "03"             '一括印刷

    ''' <summary>
    ''' 印刷PGID
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_NEBIKI_ARI_PGID As String = "LMG520"          '請求書(値引有り)
    Friend Const PRINT_NEBIKI_NASHI_PGID As String = "LMG521"        '請求書(値引無し)
    Friend Const PRINT_CHECK_LIST_PGID As String = "LMG530"          'チェックリスト

    ''' <summary>
    ''' 取込区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MITORIKOMI As String = "00"                '未取込
    Friend Const TORIKOMI_ZUMI As String = "01"             '取込済

    ''' <summary>
    ''' 締日区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const CLOSE_MATU As String = "00"             '末締め
    Friend Const CLOSE_10 As String = "10"               '10日締め
    Friend Const CLOSE_15 As String = "15"               '15日締め
    Friend Const CLOSE_20 As String = "20"               '20日締め
    Friend Const CLOSE_25 As String = "25"               '25日締め

    ''' <summary>
    ''' 請求グループコード区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SKYU_GROUP_HOKAN As String = "01"
    Friend Const SKYU_GROUP_NIYAKU As String = "02"
    Friend Const SKYU_GROUP_UNCHIN As String = "03"
    Friend Const SKYU_GROUP_SAGYO As String = "04"
    Friend Const SKYU_GROUP_YOKOMOCHI As String = "05"
    Friend Const SKYU_GROUP_DEPOT_HOKAN As String = "16"
    Friend Const SKYU_GROUP_DEPOT_LIFT As String = "17"
    Friend Const SKYU_GROUP_CONTAINER_UNSO As String = "20"

    ''' <summary>
    ''' 印刷対象区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_NOT As String = "00"             '印刷しない
    Friend Const PRINT_DO As String = "01"             '印刷する

    ''' <summary>
    ''' 取込区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const MAKE_SYU_KB_AUTO As String = "00"             '自動取込

    '2018/04/06 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add
    ''' <summary>
    ''' 作成種別_自動設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const Auto As String = "自動"
    '2018/04/06 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen end

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        EDIT = 0
        DELETE
        KAKUTEI
        IMPORT
        INITIALIZE
        KEIRITAISHOGAI
        KEIRIMODOSHI
        MSTSANSHO
        SAVE
        CLOSE
        PRINT
        ADDROW
        DELETEROW
        ENTER
        SAPOUT
        SAPCANCEL

    End Enum

    ''' <summary>
    ''' 一括印刷時明細区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum IkkatsuPrintMeisaiKbn As Integer

        HokanNiyaku = 0
        Unchin
        Sagyo

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_BR = 0
        CMB_SEIQT_SHUBETSU
        CHK_AKADEN
        LBL_SEIQT_NO
        CMB_STATE_KBN
        LBL_SAP_NO
        LBL_CREATE_USER
        PNL_SEIQT
        TXT_SEIQT_CD
        LBL_SEIQT_NM
        CMB_SEIQ_CURR_CD
        TXT_SEIQT_TANTO_NM
        IMD_INV_DATE
        LBL_SEIQT_MEIGI
        TXT_BIKO
        UNSO_WT
        CHK_HOKANRYO
        CHK_NIYAKURYO
        CHK_UNCHIN
        CHK_SAGYORYO
        CHK_YOKOMOCHI
        CHK_DEPOT_HOKAN
        CHK_DEPOT_LIFT
        CHK_SKYU_GROUP_CONTAINER_UNSO
        CMB_MOTO_CURR
        NUM_EX_RATE
        CMB_SAKI_CURR
        CHK_TEMPLATE
        BTN_SAP_OUT
        BTN_SAP_CANCEL
        PNL_PRT_JUN
        CHK_PRT_MAIN
        CHK_PRT_SUB
        CHK_PRT_KEIRI_HIKAE
        CHK_PRT_HIKAE
        CMB_PRINT
        BTN_PRINT
        NUM_CAL_ALL_K
        NUM_CAL_ALL_M
        NUM_CAL_ALL_H
        NUM_CAL_ALL_U
        NUM_NEBIKI_RATE_K
        NUM_NEBIKI_RATE_M
        NUM_RATE_NEBIKI_GAKU_K
        NUM_RATE_NEBIKI_GAKU_M
        NUM_NEBIKI_GAKU_K
        NUM_NEBIKI_GAKU_M
        NUM_ZEI_GAKU_K
        NUM_ZEI_GAKU_U
        NUM_ZEI_HASU_K
        NUM_SEIQ_GAKU_K
        NUM_SEIQ_GAKU_M
        NUM_SEIQ_GAKU_H
        NUM_SEIQ_GAKU_U
        NUM_SEIQ_ALL
        BTN_GYOTUIKA
        BTN_GYOSAKUJO
        UNCHIN_IMP_DATE
        SAGYO_IMP_DATE
        YOKOMOCHI_IMP_DATE
        SYS_UPD_DATE
        SYS_UPD_TIME
        MAX_EDABAN
        LBL_SEIQT_NO_RELATED
        SPR_MEISAI

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        IN_JUN
        SHUBETU_NM
        SHUBETU_KBN
        BUSHO
        KANJOKMK_CD
        KAZEI_NM
        KAZEI_KBN
        KEISANGAKU
        NEBIKIRITU
        RITU_NEBIKIGAKU
        KOTEI_NEBIKIGAKU
        SEIQT_GAKU
        ITEM_CURR_CD            '2014.08.21 ADD
        TCUST_BPCD
        TCUST_BPNM
        PRODUCT_SEG_CD
        ORIG_SEG_CD
        DEST_SEG_CD
        TEKIYOU
        EDABAN
        SAKUSEI_SHUBETU_NM
        SAKUSEI_SHUBETU_KBN
        GROUP_CD_KBN
        KEIRI_KB
        TEMPLATE_FLG
        RECORD_NO
        JISYATASYA_KB           'ADD 2016/09/06
        SEIQKMK_CD_S

    End Enum

End Class
