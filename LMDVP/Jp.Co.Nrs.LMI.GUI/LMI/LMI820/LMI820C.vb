' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI820C : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI820定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI820C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI820IN"
    Public Const TABLE_NM_OUT_TXT As String = "LMI820OUT_TXT"

    'TXTタイトル列名
    Public Const TXT_COL As Integer = 22
    Public Const TXT_COL_01 As String = "ダウのOrder Number"
    Public Const TXT_COL_02 As String = "Shipment Number"
    Public Const TXT_COL_03 As String = "出荷日"
    Public Const TXT_COL_04 As String = "届け先"
    Public Const TXT_COL_05 As String = "届け先住所"
    Public Const TXT_COL_06 As String = "届け先県名"
    Public Const TXT_COL_07 As String = "運送距離"
    Public Const TXT_COL_08 As String = "GMID（商品コード）"
    Public Const TXT_COL_09 As String = "商品名"
    Public Const TXT_COL_10 As String = "ロット Number"
    Public Const TXT_COL_11 As String = "有効期限"
    Public Const TXT_COL_12 As String = "Order Item Number"
    Public Const TXT_COL_13 As String = "輸送手段"
    Public Const TXT_COL_14 As String = "輸送区分"
    Public Const TXT_COL_15 As String = "運送会社名"
    Public Const TXT_COL_16 As String = "個数"
    Public Const TXT_COL_17 As String = "重量（KgまたはL）"
    Public Const TXT_COL_18 As String = "運賃（円）"
    Public Const TXT_COL_19 As String = "その他（１）"
    Public Const TXT_COL_20 As String = "その他（２）"
    Public Const TXT_COL_21 As String = "その他（３）"
    Public Const TXT_COL_22 As String = "その他（４）"

End Class
