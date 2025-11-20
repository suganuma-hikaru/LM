' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC030C : 送状番号入力
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMC030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMC030IN"
    Public Const TABLE_NM_OUT As String = "LMC030OUT"

    '閾値
    Public Const LIMITED_COUNT As Integer = 500

    Public Const CHECKED_TRUE As String = "True"

    'イベント種別
    Public Enum EventShubetsu As Integer

        TSUIKA = 0      '追加
        KOUSHIN         '更新
        TORIKOMI        '取込
        HOZON           '保存
        CLOSE          '閉じる
        DOUBLE_CLICK    'ダブルクリック

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex As Integer

        DEF = 0
        OUTKA_CTL_NO    '出荷管理番号
        DENP_NO         '送り状番号
        UNSO_NM         '運送会社名
        CUST_NM_L       '届先名

    End Enum

    'Formタブインデックス
    Public Enum FrmControlIndex As Integer

        SYUKKA_L_NO = 0 '出荷管理番号
        DENP_NO     '送り状番号
        OKURI_LIST  '一覧

    End Enum
End Class
