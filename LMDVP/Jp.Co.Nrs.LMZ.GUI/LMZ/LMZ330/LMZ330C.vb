' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ330C : UNマスタ照会
'  作  成  者       :  asatsuma
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ330定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ330C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ330IN"
    Public Const TABLE_NM_OUT As String = "LMZ330OUT"

    '商品マスタ UN、PG非該当
    Public Const M_GOODS_UN_HIGAITO As String = "-"
    Public Const M_GOODS_PG_KB_HIGAITO As String = "-"
    Public Const M_GOODS_UN_HIGAITO_NAME As String = "非該当"

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UN_NO
        PG_KBN
        IMDG_CLASS
        IMDG_CLASS1
        IMDG_CLASS2
        MP_FLG_NM
        MP_FLG
        ROW_INDEX
        LAST

    End Enum

End Class
