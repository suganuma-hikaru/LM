' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF050C : 運賃編集
'  作  成  者       :  菱刈
' ==========================================================================

''' <summary>
''' LMF050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMF050IN"
    Public Const TABLE_NM_OUT As String = "LMF050OUT"
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
        CUSTCDL
        CUSTCDM
        CUSTNM
        TARIFFKBN
        KYORI
        TARIFFCD
        CARNM
        KIKEN
        JURYO
        WARIMASHICD
        WARIMASHINM
        SHASHU
        KOSU
        'START KAI 要望番号707
        UNSONO
        'END KAI 要望番号707
        NISUGATA
        'START KAI 要望番号707
        KANRINO
        'END KAI 要望番号707
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
        'START YANAI 要望番号561
        SKIP
        'END YANAI 要望番号561

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
        UNSOCO_NM
        TARIFF_CD
        JURYO
        KYORI
        KOSU
        UNCHIN
        ITEM_CURR_CD
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

        '請求鑑ヘッダー用
        OUTKA_PLAN_DATE
        ARR_PLAN_DATE
        CUST_CD_L
        CUST_CD_M

        'START YANAI 要望番号446
        SEIQTO_CD
        'END YANAI 要望番号446

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
