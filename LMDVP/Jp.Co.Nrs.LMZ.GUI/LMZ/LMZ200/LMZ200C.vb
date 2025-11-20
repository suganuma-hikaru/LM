' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ200C : 作業項目マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ200定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ200C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ200IN"
    Public Const TABLE_NM_OUT As String = "LMZ200OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SAGYO_NM
        SAGYO_RYAK
        INV_YN_NM
        INV_YN
        FLWP_YN_NM
        FLWP_YN
        INV_TANI_NM
        INV_TANI
        SAGYO_UP
        ZEI_KBN_NM
        ZEI_KBN
        SAGYO_REMARK
        SAGYO_CD
        ROW_INDEX
        WH_SAGYO_YN_NM
        WH_SAGYO_YN
        WH_SAGYO_REMARK

    End Enum

End Class
