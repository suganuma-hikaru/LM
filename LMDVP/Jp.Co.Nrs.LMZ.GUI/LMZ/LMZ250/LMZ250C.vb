' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ250C : 運送会社マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ250定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ250C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ250IN"
    Public Const TABLE_NM_OUT As String = "LMZ250OUT"

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    '''マイ運送会社区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MY_UNSOCO_YN_TRUE As String = "01"
    Public Const MY_UNSOCO_YN_FALSE As String = "00"
    '要望対応:1248 terakawa 2013.03.21 End

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UNSOCO_NM
        UNSOCO_BR_NM
        MOTOUKE_KB_NM
        MOTOUKE_KB
        UNSOCO_CD
        UNSOCO_BR_CD
        ROW_INDEX

    End Enum

End Class
