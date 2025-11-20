' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ021C : 商品マスタ(在庫)照会
'  作  成  者       :  Annen
' ==========================================================================

''' <summary>
''' LMZ021定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ021C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ021IN"
    Public Const TABLE_NM_OUT As String = "LMZ021OUT"
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        GOODS_NM_1
        GOODS_CD_CUST
        LOT_NO
        ALLOC_CAN_NB
        STD_IRIME_NB
        STD_IRIME_UT_NM
        NB_UT_NM
        NB_UT
        PKG_NM
        SEARCH_KEY_1
        SEARCH_KEY_2
        ONDO_KB_NM
        ONDO_KB
        SHOBO_CD
        CUST_NM_S
        CUST_NM_SS
        STD_IRIME_UT
        PKG_NB
        PKG_UT
        PKG_UT_NM
        CUST_CD_S
        CUST_CD_SS
        CUST_CD_L
        CUST_CD_M
        CUST_NM_L
        CUST_NM_M
        GOODS_CD_NRS
        ROW_INDEX

    End Enum

End Class
