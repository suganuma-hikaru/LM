' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF810C : 支払データ生成メイン
'  作  成  者       :  YANAI
' ==========================================================================

''' <summary>
''' LMF810定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF810C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMF810IN"
    Public Const TABLE_NM_OUT As String = "LMF810OUT"
    Public Const TABLE_NM_RESULT As String = "LMF810RESULT"
    Public Const TABLE_NM_SHIHARAI As String = "F_SHIHARAI_TRS"
    Public Const TABLE_NM_UNSO_L As String = "F_UNSO_L"
    Public Const TABLE_NM_UNSO_M As String = "F_UNSO_M"
    Public Const TABLE_NM_YOKO_HD As String = "LMF810M_YOKO_TARIFF_HD_SHIHARAI"
    Public Const TABLE_NM_TARIFF_MAX As String = "SHIHARAI_TARIFF_MAX_REC"


    '処理結果コード
    Public Const STATUS_NOMAL As String = "00"
    Public Const STATUS_ERR_PRM As String = "10"
    Public Const STATUS_ERR_APR As String = "20"
    Public Const STATUS_ERR_SYS As String = "99"

End Class
