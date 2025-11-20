' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ020C : 商品マスタ照会
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMZ020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ020IN"
    Public Const TABLE_NM_OUT As String = "LMZ020OUT"

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
        GOODS_NM_1
        GOODS_CD_CUST
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

    Public Class SYORI_KB
        Public Const INKA_TOUROKU As String = "10"
        Public Const INKA_HAND As String = "11"
        Public Const INKA_JISSEKI As String = "12"
        Public Const OUTKA_TOUROKU As String = "20"
        Public Const OUTKA_HAND As String = "21"
        Public Const OUTKA_JISSEKI As String = "22"
        Public Const UNSO_TOUROKU As String = "40"
    End Class
    Public Class INOUT_KB
        Public Const INKA As String = "1"
        Public Const OUTKA As String = "0"
    End Class
End Class
