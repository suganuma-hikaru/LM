' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ220C : 請求先マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ220定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ220C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ220IN"
    Public Const TABLE_NM_OUT As String = "LMZ220OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SEIQTO_CD
        SEIQTO_NM
        SEIQTO_BUSYO_NM
        OYA_PIC
        CLOSE_KB_NM
        CLOSE_KB
        NRS_KEIRI_CD1
        ROW_INDEX

    End Enum

End Class
