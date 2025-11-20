' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF060C : 運賃試算
'  作  成  者       :  菱刈
' ==========================================================================

''' <summary>
''' LMF060定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "M_KYORI"
    Public Const TABLE_NM_OUT As String = "LMF060OUT"
    Public Const TABLE_NM_EXTC As String = "M_EXTC_UNCHIN"
    Public Const TABLE_NM_540_IN As String = "LMF540IN"
    Public Const TABLE_NM_540_OUT As String = "LMF540OUT"
    Public Const TABLE_NM_540_RPT As String = "M_RPT"

    'コンボボックスの初期値
    Public Const Combo As String = "00"

    'タリフ分類区分
    Private Const TARIFF_BUNRUI_KONSAI As String = "10"
    Private Const TARIFF_BUNRUI_SHAATU As String = "20"
    Private Const TARIFF_BUNRUI_TOKBIN As String = "30"
    Private Const TARIFF_BUNRUI_YKMOTI As String = "40"
    Private Const TARIFF_BUNRUI_NYUTYA As String = "50"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"



    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL
        Eigyo
        Print
        btnprint
        Unso
        Syasyu
        CustCdL
        CustCdM
        CustNM
        UnsoDate
        OrigJis
        OrigJisNM
        TodokedeJisCd
        TodokedeJisNM
        KyoriteiCd
        Btnkyori
        Kyori
        TariffCd
        Jyuryo
        WarimashiCd
        WarimashiNM
        BtnGet
       

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
        DEL
        UNCHINGET
        SISANSET
        KYORIGET
        ENTER

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        ' DEF = 0
        DEF
        SEIQ_TARIFF_BUNRUI_KB
        CAR_TP
        ORIG_JIS_CD
        ORIG_JIS_NM
        DEST_JIS_CD
        DEST_JIS_NM
        UNCHIN_TARIFF_CD
        EXTC_TARIFF_CD
        STR_DATE
        KYORI_CD
        KYORI
        WT_LV
        DECI_UNCHIN
        DECI_WINT_EXTC
        DECI_CITY_EXTC
        DECI_RELY_EXTC
        DECI_TOLL
        DECI_INSU
        CUST


    End Enum
End Class
