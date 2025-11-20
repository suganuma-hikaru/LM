' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ210C : 届先マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ210定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ210C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ210IN"
    Public Const TABLE_NM_OUT As String = "LMZ210OUT"

    '2015.11.02 tusnehira add Start
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2015.11.02 tusnehira add End

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        DEST_NM
        AD_1
        'START YANAI 要望番号881
        REMARK
        DELI_ATT
        'END YANAI 要望番号881
        DEST_CD
        ROW_INDEX
        COLUMN_COUNT
    End Enum

End Class
