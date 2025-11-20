' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ230C : 運賃タリフマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ230定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ230C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ230IN"
    Public Const TABLE_NM_OUT As String = "LMZ230OUT"
    Public Const TABLE_NM_CUST As String = "LMZ230CUST"

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    '''マイ運賃タリフ区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MY_UNCHIN_TARIFF_YN_TRUE As String = "01"
    Public Const MY_UNCHIN_TARIFF_YN_FALSE As String = "00"
    '要望対応:1248 terakawa 2013.03.21 End


    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UNCHIN_TARIFF_CD
        UNCHIN_TARIFF_REM
        TABLE_TP_NM
        TABLE_TP
        STR_DATE
        ROW_INDEX

    End Enum

End Class
