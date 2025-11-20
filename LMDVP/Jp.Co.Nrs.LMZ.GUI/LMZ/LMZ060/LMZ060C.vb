' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ060C : 商品マスタ照会
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMZ060定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ060IN"
    Public Const TABLE_NM_OUT As String = "LMZ060OUT"

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        ZIP_NO
        KEN_N
        CITY_N
        TOWN_N
        JIS_CD
        ROW_INDEX

    End Enum

End Class
