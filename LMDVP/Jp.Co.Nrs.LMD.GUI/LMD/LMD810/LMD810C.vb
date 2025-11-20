' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD810  : 在庫照合結果EXCEL作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMD810定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD810C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD810IN"
    Public Const TABLE_NM_IN_FLG As String = "LMD810IN_FLG"

    'Excel列タイトル名
    Public Const EXCEL_GOODSNM As String = "品名"
    Public Const EXCEL_GOODSCD As String = "品目コード"
    Public Const EXCEL_LOTNO As String = "ロット№"
    Public Const EXCEL_SERIALNO As String = "シリアル№"
    Public Const EXCEL_IRIME As String = "入目"
    Public Const EXCEL_IRIMEUT As String = "入目単位"
    Public Const EXCEL_NRSNB As String = "NRS_個数"
    Public Const EXCEL_CUSTNB As String = "荷主_個数"
    Public Const EXCEL_KBN As String = "区分"

    'Excel設定値
    Public Const EXCEL_NRSKBN As String = "nrs"
    Public Const EXCEL_CUSTKBN As String = "cust"

    'Excel出力列最低数
    Public Const EXCEL_COL As Integer = 5

End Class
