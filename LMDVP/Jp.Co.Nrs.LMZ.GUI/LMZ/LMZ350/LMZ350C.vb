' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ350C : 真荷主照会
'  作  成  者       :  hori
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ350定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ350C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ350IN"
    Public Const TABLE_NM_OUT As String = "LMZ350OUT"

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        BP_NM1
        BP_CD
        COUNTRY_CD
        ROW_INDEX
        LAST

    End Enum

End Class
