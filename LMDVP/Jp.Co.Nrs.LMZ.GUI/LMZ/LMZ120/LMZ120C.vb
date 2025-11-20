' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ120C : 棟・室ゾーン照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ120定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ120C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ120IN"
    Public Const TABLE_NM_OUT As String = "LMZ120OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0

        TOU_NO
        SITU_NO
        TOU_SITU_NM
        HOZEI_KB_NM
        HOZEI_KB
        SHOBO_YN
        SHOBO_YN_NM
        ZONE_CD
        ZONE_NM
        ZONE_HOZEI_KB_NM
        ZONE_HOZEI_KB
        ZONE_ONDO_CTL_KB_NM
        ZONE_ONDO_CTL_KB
        ZONE_ONDO_CTL_FLG_NM
        ZONE_ONDO_CTL_FLG
        ZONE_ONDO
        YAKUJI_YN_NM
        YAKUJI_YN
        DOKU_YN_NM
        DOKU_YN
        GASS_YN_NM
        GASS_YN
        ROW_INDEX

    End Enum

End Class
