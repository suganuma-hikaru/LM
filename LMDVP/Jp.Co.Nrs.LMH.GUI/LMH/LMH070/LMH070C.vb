' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH070C : 先行手入力入出荷とEDIの紐付け
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMH070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH070IN"
    Public Const TABLE_NM_OUT_L As String = "LMH070OUT_L"
    Public Const TABLE_NM_OUT_M As String = "LMH070OUT_M"
    Public Const TABLE_NM_INOUTKA As String = "LMH070_INOUTKA"
    Public Const TABLE_NM_SAVE As String = "LMH070_SAVE"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    ' アクションID
    Public Const ACTION_ID_SEARCH As String = "SelectListData"

    ' Spread部列インデックス用列挙対
    Public Enum SprDtl As Integer

        DEF = 0
        CTL_NO          '管理番号
        GOODS_CD_NRS    'NRS商品コード
        GOODS_CD_CUST   '荷主商品コード
        GOODS_NM        '商品名
        LOT_NO          'ロット番号
        IRIME           '入目
        IRIME_UT        '入目単位
        NB              '個数
        EDI_NO          'EDI管理番号
        'SYS_UPD_DATE    '更新日
        'SYS_UPD_TIME    '更新時間
        'RCV_UPD_DATE    '受信HED更新日
        'RCV_UPD_TIME    '受信HED更新時間

    End Enum

    'イベント種別
    Public Enum EventShubetsu As Integer

        KENSAKU = 0
        HOZON

    End Enum

End Class
