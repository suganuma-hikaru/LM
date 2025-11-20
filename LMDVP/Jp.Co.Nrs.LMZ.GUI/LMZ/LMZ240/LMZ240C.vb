' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ240C : 割増運賃タリフマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ240定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ240C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ240IN"
    Public Const TABLE_NM_OUT As String = "LMZ240OUT"
    Public Const TABLE_NM_CUST As String = "LMZ240CUST"

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        EXTC_TARIFF_CD
        EXTC_TARIFF_REM
        ROW_INDEX

    End Enum

End Class
