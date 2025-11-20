' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ320C : 支払横持ちタリフ照会
'  作  成  者       :  本明
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ320定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ320C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ320IN"
    Public Const TABLE_NM_OUT As String = "LMZ320OUT"
    Public Const TABLE_NM_CUST As String = "LMZ320CUST"


    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        YOKO_TARIFF_CD
        CALC_KB_NM
        CALC_KB
        YOKO_REM
        ROW_INDEX

    End Enum

End Class
