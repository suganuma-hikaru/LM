' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ380C   : 物産アニマルヘルス在庫選択
'  作  成  者       :  HORI
' ==========================================================================

''' <summary>
''' LMZ380定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ380C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ380IN"
    Public Const TABLE_NM_OUT As String = "LMZ380OUT"
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        GOODS_CD
        GOODS_NM
        LOT_NO
        GOODS_RANK_NM
        NB
        LT_DATE
        NRS_BR_CD
        ZAI_REC_NO
        GOODS_CD_NRS
        GOODS_RANK
        ROW_INDEX
        LAST

    End Enum

End Class
