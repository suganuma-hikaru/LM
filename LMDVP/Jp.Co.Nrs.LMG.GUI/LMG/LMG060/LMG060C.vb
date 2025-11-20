' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG060C : 請求印刷指示
'  作  成  者       :  [菱刈]
' ==========================================================================

''' <summary>
''' LMG060定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_SEIKYU As String = "LMF510IN"
    Public Const TABLE_NM_IN_TARIFF As String = "LMF520IN"
    Public Const TABLE_NM_IN_CHECK As String = "LMF530IN"
    'START YANAI 要望番号582
    'Public Const TABLE_NM_IN As String = "LMG060IN"
    'END YANAI 要望番号582
    Public Const TABLE_NM_IN_INKA As String = "LMF590IN"
    Public Const TABLE_NM_IN_OUTKA As String = "LMF630IN"   '(2012.09.25) 追加 運賃請求明細書(出荷)
    '(2013.02.18)要望番号1832 -- START --
    Public Const TABLE_NM_IN_RENZOKU As String = "LMF510IN" '(2013.02.18) 追加 運賃請求明細書(連続)
    Public Const TABLE_NM_IN_CUST As String = "LMG060IN_CUST"
    Public Const TABLE_NM_OUT_CUST As String = "LMG060OUT_CUST"
    '(2013.02.18)要望番号1832 --  END  --

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_UNCHIN_SEIKYU As String = "01"
    Public Const PRINT_UNCHIN_TARIFF As String = "02"
    Public Const PRINT_UNCHIN_CHECK As String = "03"
    Public Const PRINT_UNCHIN_INKA As String = "04"
    Public Const PRINT_UNCHIN_OUTKA As String = "05"    '(2012.09.25) 追加 運賃請求明細書(出荷)
    Public Const PRINT_UNCHIN_RENZOKU As String = "06"  '(2013.02.14) 追加 運賃請求明細書(連続) 要望番号1832

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT_SEIKYU As String = "PrintSeikyu"
    Public Const ACTION_ID_PRINT_TARIFF As String = "PrintSeikyuTariff"
    Public Const ACTION_ID_PRINT_CHECK As String = "PrintCheck"
    Public Const ACTION_ID_PRINT_INKA As String = "PrintSeikyuInka"
    Public Const ACTION_ID_PRINT_OUTKA As String = "PrintSeikyuOutka"     '(2012.09.25) 追加 運賃請求明細書(出荷)
    Public Const ACTION_ID_PRINT_RENZOKU As String = "PrintSeikyuRenzoku" '(2013.02.18) 追加 運賃請求明細書(連続) 要望番号1832

    'START YANAI 要望番号582
    Public Const PGID_LMF040 As String = "LMF040"
    'END YANAI 要望番号582

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        Print = 0
        Br
        CustCdL
        CustNmL
        CustCdM
        CustNmM
        SeiqCd
        SeiqNm
        OutkaDateFrom
        OutkaDateTo
        CloseKb

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        MASTEROPEN
        TOJIRU
        PRINT
        ENTER

    End Enum

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        CUST_NM_L
        CUST_NM_M
        CUST_CD_L
        CUST_CD_M
        ROW_INDEX
        CLOSE_KB

    End Enum

    '親荷主コード(00)
    Public Const OYA_CUST As String = "00"

End Class
