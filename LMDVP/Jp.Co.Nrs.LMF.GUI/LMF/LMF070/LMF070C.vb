' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF070C : 運賃試算比較
'  作  成  者       :　yamanaka
' ==========================================================================

''' <summary>
''' LMF070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMF070IN"
    Public Const TABLE_NM_OUT As String = "LMF070OUT"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"


    ''' <summary>
    ''' 最大桁数(整数13桁、少数2桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_13 As String = "9999999999999"


    ''' <summary>
    ''' 最大桁数(整数13桁、少数2桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const OVER As String = "9,999,999,999,999"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL
        NrsBrCd
        OutakaDate_From
        OutakaDate_To
        CustCdL
        CustCdM
        CustNm
        OldTariffCd
        OldTariffNm
        OldETariffCd
        OldETariffNm
        TitleUnsoCoNM
        UnsoCd
        UnsoNm2
        SeiqtoCd
        SeiqtoNm
        CmbSoko
        NewKyoriCd
        NewTariffCd
        NewTariffNm
        ETariffCd
        ETariffNm
        OldTariffSum
        OldETariffSum
        NewTariffSum
        NewETariffSum


    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        KENSAKU
        MASTEROPEN
        CLOSE
        UNCHINGET
        ENTER

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        OUTKA_PLAN_DATE
        DEST_NM
        DEST_JIS_NM
        SEIQ_UNCHIN
        NEW_SEIQ_UNCHIN
        WT
        OLD_KYORI
        SEIQ_TARIFF_CD
        SEIQ_ETARIFF_CD
        NEW_KYORI
        NEW_SEIQ_TARIFF_CD
        NEW_SEIQ_ETARIFF_CD
        MOTO_DATA_KB
        INOUTKA_NO
        SOKO_NM
        NEW_SOKO_NM
        SEIQ_CD
        SEIQ_NM
        UNSOCO_NM

        '隠し項目
        MOTO_DATA
        CLM_NM '列数用変数

    End Enum

End Class
