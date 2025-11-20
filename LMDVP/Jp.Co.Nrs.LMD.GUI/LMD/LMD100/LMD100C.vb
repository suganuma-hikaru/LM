' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD100C : 在庫テーブル照会
'  作  成  者       :  矢内
' ==========================================================================

''' <summary>
''' LMD100定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD100C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMD100IN"
    'Public Const TABLE_NM_OUT As String = "LMD100OUT"
    Public Const TABLE_NM_FURIGOODS_IN As String = "LMD100_FURI_GOODS_IN"
    Public Const TABLE_NM_FURIGOODS As String = "LMD100_FURI_GOODS"
    'Public Const TABLE_NM_ZAI As String = "LMD100_ZAI"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '検索種別
    Public Enum SearchShubetsu As Integer

        FIRST_SEARCH = 0        '初期
        NEW_SEARCH              '検索
        RE_SEARCH               '再描画

    End Enum

    'イベント種別
    Public Enum EventShubetsu As Integer

        KENSAKU = 0     '検索
        LOTSENTAKU      'ロット選択
        SENTAKU         '選択
        DOUBLE_CLICK    'ダブルクリック

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        GOODS_CD_CUST
        GOODS_NM
        LOT_NO
        IRIME
        ALLOC_CAN_NB
        NB_UT
        PKG
        REMARK
        REMARK_OUT
        TAX_KB
        HIKIATE_ALERT_NM
        DEST_NM
        OUTKA_FROM_ORD_NO_L
        CD_NRS
        IRIME_UT
        HIKIATE_ALERT_YN
        SMPL
        ZAI_REC_NO
        DEST_CD
        LAST

    End Enum


End Class
