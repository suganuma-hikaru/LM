' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI890  : NRC出荷／回収情報Excel作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI890定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI890C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI890IN"
    Public Const TABLE_NM_OUT_SHUKKA As String = "LMI890OUT_EXCEL_SHUKKA"
    Public Const TABLE_NM_OUT_HENSO As String = "LMI890OUT_EXCEL_HENSO"
    Public Const TABLE_NM_OUT_MIHEN As String = "LMI890OUT_EXCEL_MIHEN"

    'Excel出力列最低数
    Public Const EXCEL_COL_SHUKKA As Integer = 12
    Public Const EXCEL_COL_HENSO As Integer = 14
    Public Const EXCEL_COL_MIHEN As Integer = 13

    'Excel出力最大行数
    Public Const EXCEL_MAX_ROW As Integer = 20000

    'Excelシート名
    Public Const EXCEL_SHUKKA As String = "出荷実績"
    Public Const EXCEL_HENSO As String = "返送実績"
    Public Const EXCEL_MIHEN As String = "長期未返却"

    'Excelタイトル列名
    Public Const EXCEL_CUSTNML As String = "出荷場所"
    Public Const EXCEL_ARRPLANDATE As String = "納入予定日"
    Public Const EXCEL_CUSTORDNO As String = "オーダー番号"
    Public Const EXCEL_BUYERORDNO As String = "注文番号"
    Public Const EXCEL_GOODSNM As String = "商品"
    Public Const EXCEL_SERIALNO As String = "シリアル№"
    Public Const EXCEL_DESTNM As String = "届先名"
    Public Const EXCEL_SHIPNM As String = "販売先"
    Public Const EXCEL_DESTAD As String = "届先住所"
    Public Const EXCEL_CUSTCDL As String = "日陸荷主コード"
    Public Const EXCEL_DESTCD As String = "届先コード"
    Public Const EXCEL_SHIPCDL As String = "販売先"
    Public Const EXCEL_HOKOKUDATE As String = "返却日"
    Public Const EXCEL_TAIRYU As String = "滞留日数"

    ''' <summary>
    ''' Excel出力列(出荷実績の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ExcelShukkaCol

        CUSTNML = 0
        ARRPLANDATE
        CUSTORDNO
        BUYERORDNO
        GOODSNM
        SERIALNO
        DESTNM
        SHIPNM
        DESTAD
        CUSTCDL
        DESTCD
        SHIPCDL

    End Enum

    ''' <summary>
    ''' Excel出力列(返送実績の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ExcelHensoCol

        CUSTNML = 0
        ARRPLANDATE
        CUSTORDNO
        BUYERORDNO
        GOODSNM
        SERIALNO
        HOKOKUDATE
        TAIRYU
        DESTNM
        SHIPNM
        DESTAD
        CUSTCDL
        DESTCD
        SHIPCDL

    End Enum

    ''' <summary>
    ''' Excel出力列(長期未返却の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ExcelMihenCol

        CUSTNML = 0
        ARRPLANDATE
        TAIRYU
        CUSTORDNO
        BUYERORDNO
        GOODSNM
        SERIALNO
        DESTNM
        SHIPNM
        DESTAD
        CUSTCDL
        DESTCD
        SHIPCDL

    End Enum

End Class
