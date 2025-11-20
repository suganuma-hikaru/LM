' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG900C : 請求処理 請求取込データ抽出作成
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMG900定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG900C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMG900IN"
    Friend Const TABLE_NM_OUT As String = "LMG900OUT"
    Friend Const TABLE_NM_DATE As String = "LMG900_START_DATE"
    Friend Const TABLE_NM_IMPORT As String = "LMG900_IMPORT"
    Friend Const TABLE_NM_HOKAN As String = "LMG900_HOKAN"
    Friend Const TABLE_NM_NIYAKU As String = "LMG900_NIYAKU"
    Friend Const TABLE_NM_SAGAKU As String = "LMG900_SAGAKU"
    '★ ADD START 2011/09/06 SUGA
    Friend Const TABLE_NM_SAGYO As String = "LMG900_SAGYO"
    '★ ADD E N D 2011/09/06 SUGA
    Friend Const TABLE_NM_SEIQTO As String = "LMG900_SEIQTO"

    Friend Const TABLE_NM_IN_CUST As String = "LMG900IN_CUST"
    Friend Const TABLE_NM_CUST As String = "LMG900_CUST"

    Friend Const MIN_SEIQKMK_CD As String = "05"

    Friend Const PREFIX_TOTAL_MIN As String = "鑑最低保証"
    Friend Const PREFIX_KOBETU_MIN As String = "最低保証"
End Class
