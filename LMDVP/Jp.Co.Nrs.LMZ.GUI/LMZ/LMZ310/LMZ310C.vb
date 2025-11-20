' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ310C : 支払先マスタ照会
'  作  成  者       :  大貫和正
' ==========================================================================

''' <summary>
''' LMZ310定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ310C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ310IN"
    Public Const TABLE_NM_OUT As String = "LMZ310OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SHIHARAITO_CD
        SHIHARAITO_NM
        SHIHARAITO_BUSYO_NM
        OYA_PIC
        CLOSE_KB_NM
        CLOSE_KB
        ROW_INDEX

    End Enum

End Class
