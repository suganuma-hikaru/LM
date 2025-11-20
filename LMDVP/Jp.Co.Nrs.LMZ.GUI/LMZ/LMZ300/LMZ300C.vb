' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ300C : 支払割増運賃タリフマスタ照会
'  作  成  者       :  terakawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ300定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ300C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ300IN"
    Public Const TABLE_NM_OUT As String = "LMZ300OUT"
    Public Const TABLE_NM_UNSOCO As String = "LMZ300UNSOCO"


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
