' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI900  : ハネウェル管理Excel作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI900定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI900C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI900IN"

    'Excel出力列最低数
    Public Const EXCEL_COL As Integer = 17      '15 → 16 UPD 2019/12/10 009849

    'Excel出力最大行数
    Public Const EXCEL_MAX_ROW As Integer = 1000

    ''' <summary>
    ''' Excel出力列(出荷実績の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ExcelCol

        NRS_BR_CD = 0
        EMPTY_KB
        CYLINDER_TYPE
        TOFROM_NM
        SERIAL_NO
        ALBAS_CHG
        INOUT_DATE
        NEXT_TEST_DATE
        PROD_DATE           'ADD 2019/10/29 006786
        KEIKA_DATE
        IOZS_KB
        SHIP_CD_L
        SHIP_NM_L
        BUYER_ORD_NO_DTL
        GOODS_CD_CUST       'ADD 2019/10/31 008261
        GOODS_NM            'ADD 2019/10/31 008261
        SEARCH_KEY_2        'ADD 2019/12/10 009849
        REMARK_IN
    End Enum

End Class
