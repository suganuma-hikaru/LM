' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF090C : 支払編集
'  作  成  者       :  YANAI
' ==========================================================================

''' <summary>
''' LMF090定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF090C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMF090IN"
    Public Const TABLE_NM_OUT As String = "LMF090OUT"
    Public Const G_HED As String = "G_KAGAMI_HED"

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

        DETAIL = 0
        EIGYO
        SHUKKA
        NONYU
        UNSOCDL
        UNSOCDM
        UNSONM
        TARIFFKBN
        KYORI
        TARIFFCD
        TARIFFNM
        KIKEN
        JURYO
        WARIMASHICD
        WARIMASHINM
        SHASHU
        KOSU
        UNSONO
        NISUGATA
        KANRINO
        BTNKEISAN

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        MASTEROPEN
        TOJIRU
        HENSHU
        SAVE
        KAKUTEI
        KAKUTEIKAIJO
        UNCHINGET
        ENTER
        SKIP

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UNSO_TEHAI
        DEST_JIS
        DEST_NM
        CUST_CD_L
        CUST_CD_M
        CUST_NM
        TARIFF_CD
        SHIHARAITO_CD
        SHIHARAI_NM
        JURYO
        KYORI
        KOSU
        UNCHIN
        TOSHI
        TOKI
        TYUKEI
        KOSO
        SONOTA
        ZEI_KB
        WARIMASHI
        GROUP
        GROUP_UNSO
        REMARK
        UNSO_NO
        UNSO_NO_EDA
        TRIP_NO
        ONDO
        NISUGATA
        SHASHU
        KIKEN

        '隠し項目
        TARIFF_KB
        PKG_KB
        SYARYO_KB
        DANGER_KB
        NRS_BR_CD
        SYS_UPD_DATE
        SYS_UPD_TIME
        OUTKA_PLAN_DATE
        ARR_PLAN_DATE
        SHIHARAI_TARIFF_CD_LL
        SHIHARAI_ETARIFF_CD_LL
        SHIHARAI_UNSO_WT_LL
        SHIHARAI_COUNT_LL
        SHIHARAI_UNCHIN_LL
        LAST

    End Enum

    ''' <summary>
    ''' キャッシュテーブル検索時使用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ConditionPattern As Integer

        equal
        pre
        all

    End Enum

End Class
