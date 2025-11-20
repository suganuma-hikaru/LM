' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫管理
'  プログラムID     :  LMD030C : 在庫履歴
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMD030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD030IN"
    Public Const TABLE_NM_IN_DEL As String = "LMD030IN_ZAI_DEL"
    Public Const TABLE_NM_OUT As String = "LMD030OUT"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '閾値
    'Public Const LIMITED_COUNT As Integer = 500

    'イベント種別
    Public Enum EventShubetsu As Integer

        KENSAKU = 0        '検索
        DEL                '削除
        CLOSE              '閉じる

    End Enum

    '検索種別
    Public Enum SearchShubetsu As Integer

        FIRST_SEARCH = 0        '初期
        NEW_SEARCH              '検索
        RE_SEARCH               '再描画

    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        CHKSYUKKADELSHOW = 0
        OPTCNTSHOW
        OPTAMTSHOW
        SPRDETAIL

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0
        STATE_KB
        SYUBETU
        IDO_DATE
        INKA_NB
        INKA_QT
        OUTKA_NB
        OUTKA_QT
        BACKLOG_NB
        BACKLOG_QT
        PKG_UT
        STD_IRIME_UT
        TOU_NO
        SITU_NO
        ZONE_CD
        LOCA
        INOUTKA_NO_L
        O_ZAI_REC_NO
        N_ZAI_REC_NO
        REMARK_KBN
        CUST_ORD_NO
        BUYER_ORD_NO
        UNSOCO_NM
        REMARK
        REMARK_OUT
        GOODS_COND_NM_1
        GOODS_COND_NM_2
        GOODS_COND_NM_3
        SPD_KB_NM
        OFB_KB_NM
        ALLOC_PRIORITY_NM
        DEST_NM
        RSV_NO
        SORT_KEY
        STD_IRIME_NB
        PORA_ZAI_NB
        ALLOC_CAN_NB
        IDO_SYS_UPD_DATE
        IDO_SYS_UPD_TIME
        O_ZAI_SYS_UPD_DATE
        O_ZAI_SYS_UPD_TIME
        N_ZAI_SYS_UPD_DATE
        N_ZAI_SYS_UPD_TIME
        HOKAN_SEIQTO_CD

    End Enum


End Class
