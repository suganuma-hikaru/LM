' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ290C : 支払運賃タリフマスタ照会
'  作  成  者       :  馬野
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ290定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ290C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ290IN"
    Public Const TABLE_NM_OUT As String = "LMZ290OUT"
    Public Const TABLE_NM_UNSOCO As String = "LMZ290UNSOCO"


    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SHIHARAI_TARIFF_CD
        SHIHARAI_TARIFF_REM
        TABLE_TP_NM
        TABLE_TP
        STR_DATE
        ROW_INDEX

    End Enum

End Class
