' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ050C : 棟・室ゾーン照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ050IN"
    Public Const TABLE_NM_OUT As String = "LMZ050OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        TOU_SITU_NM
        SHOBO_CD
        HOZEI_KB_NM
        HOZEI_KB
        TOU_ONDO_CTL_KB_NM
        TOU_ONDO_CTL_KB
        TOU_ONDO_CTL_FLG_NM
        TOU_ONDO_CTL_FLG
        TOU_ONDO
        TOU_NO
        SITU_NO
        ROW_INDEX

    End Enum

End Class
