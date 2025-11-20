'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ190C : 請求項目マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ190定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ190C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ190IN"
    Public Const TABLE_NM_OUT As String = "LMZ190OUT"
    '2014.09.12 追加START 多通貨対応
    Public Const TABLE_NM_COUNTRY As String = "LMZ190COUNTRY"
    '2014.09.12 追加END 多通貨対応

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        '2014.09.12 追加START 多通貨対応
        COUNTRY_CD
        '2014.09.12 追加END 多通貨対応
        GROUP_KB_NM
        GROUP_KB
        SEIQKMK_NM
        TAX_KB_NM
        TAX_KB
        REMARK
        SEIQKMK_CD
        SEIQKMK_CD_S
        KEIRI_KB_NM
        KEIRI_KB
        ROW_INDEX

    End Enum

End Class
