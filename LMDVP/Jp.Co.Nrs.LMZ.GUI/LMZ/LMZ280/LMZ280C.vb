'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ280C : JISマスタ検索
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ280定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ280C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ280IN"
    Public Const TABLE_NM_OUT As String = "LMZ280OUT"


    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SHOBO_CD
        RUI_NM
        RUI
        HINMEI
        SEISITSU
        KIKEN_TOKYU_NM
        KIKEN_TOKYU
        SYU_NM
        SYU
        ROW_INDEX

    End Enum

End Class
