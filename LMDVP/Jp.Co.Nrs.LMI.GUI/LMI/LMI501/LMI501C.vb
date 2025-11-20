' ==========================================================================
'  システム名       :  LMI
'  サブシステム名   :  LMI     : 
'  プログラムID     :  LMI501C : 在庫証明書
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI501定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI501C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 在庫証明書データ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_DATA As String = "SelectZaiData"

    ''' <summary>
    ''' LMI501SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_LMI501SET As String = "LMI501SET"

    ''' <summary>
    ''' 1ページ値の列数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PAGE_MAX_COL As String = "AM"

    ''' <summary>
    ''' 1ページ値の行数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PAGE_MAX_ROW As Integer = 43

    ''' <summary>
    ''' Sheetインデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SheetColumnIndex

        WH_CD = 1
        WH_NM
        SEIQTO_CD
        SEIQTO_NM
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        CUST_NM_L
        CUST_NM_M
        CUST_NM_S
        CUST_NM_SS
        SEARCH_KEY_1
        SEARCH_KEY_2
        CUST_COST_CD1
        CUST_COST_CD2
        GOODS_CD_NRS
        GOODS_NM
        LOT_NO
        SERIAL_NO
        INKO_DATE
        GOODS_COND_NM_1
        GOODS_COND_NM_2
        GOODS_COND_NM_3
        GOODS_COND_FREE
        OFB
        SPD
        TAX
        SYOBO
        SYOBO_SBT
        REMARK_OUT
        NB_UT
        IRIME
        STD_IRIME_UT
        ZAI_NB
        ZAI_QT
        PKG_NB
        PKG_UT
        REMARK

    End Enum

End Class
