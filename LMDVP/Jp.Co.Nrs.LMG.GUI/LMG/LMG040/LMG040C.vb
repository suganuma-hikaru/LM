' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG040C : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMG040定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMG040IN"
    Friend Const TABLE_NM_OUT As String = "LMG040OUT"
    Friend Const TABLE_NM_SAP_UPD_CNT As String = "LMG040SAPUPDCNT"
    Friend Const SHOW_PAGE As String = "LMG050IN"

    ''' <summary>
    ''' 区分マスタ 区分分類コード SAP出力連携可能日付
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const KBN_SAP_OUT_START_DATE As String = "B040"

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
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SINKI_TORIKOMI = 0
        SINKI_TEGAKI
        KAKUTEI
        KENSAKU
        MST_SANSHO
        CLOSE
        DOUBLE_CLICK
        ENTER
        'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
        DELETE
        CLEAR
        'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
        SAPOUT
        SAPCANCEL
        SKYUCSV

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        GRP_SELECT = 0
        CMB_BR
        IMD_INV_DATE
        TXT_SEIQT_CD
        LBL_SEIQT_NM
        TXT_SEIQT_NO
        CHK_MIKAKUTEI
        CHK_KAKUTEI
        CHK_INSATU_ZUMI
        CHK_KEIRI_TORIKOMI
        CHK_KEIRI_TORIKOMI_TAISHOGAI
        CHK_KEIRI_TORIKESHI
        CHK_SKYU_CSV_FLG
        LBL_BR_CD
        SPR_MEISAI

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprColumnIndex

        DEF = 0
        SEIQT_NO
        SAP_NO
        SEIQT_CD
        SEIQT_NM
        SEIQT_AMT
        SEIQT_DATE
        KAKUTEIZUMI
        INSATUZUMI
        KEIRI_TORIKOMI
        KEIRI_TORIKOMI_TAISHOGAI
        SKYU_CSV
        SHUBETU_KB
        SHUBETU_NM
        AKAKURO_KB
        AKAKURO_NM
        SEIQT_NO_RELATED
        STATE_KB
        UPDATE_DATE
        UPDATE_TIME
        UNCHIN_INV_FROM
        SAGYO_INV_FROM
        YOKOMOCHI_INV_FROM
        SYS_DEL_FLG             'ADD 2018/08/10 依頼番号 : 002136  
        SAP_OUT_USER
        SAP_OUT_USER_NM
    End Enum

End Class
