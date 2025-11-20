' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF080C : 支払検索
'  作  成  者       :  YANAI
' ==========================================================================

''' <summary>
''' LMF080定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 運賃データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 運賃データ検索アクション(まとめ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_GROUP As String = "SelectListGroupData"

    ''' <summary>
    ''' 確定アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_FIX As String = "SetFixData"

    ''' <summary>
    ''' 確定解除アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_FIX_CANCELL As String = "SetFixCancellData"

    ''' <summary>
    ''' まとめ指示アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_GROUP As String = "SetGroupData"

    ''' <summary>
    ''' まとめ解除アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_GROUP_CANCELL As String = "SetGroupCancellData"

    ''' <summary>
    ''' 一括変更アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SAVE As String = "SaveUnchinItemData"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMF080IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMF080OUT"

    ''' <summary>
    ''' データセットテーブル名(UNCHINテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNCHIN As String = "UNCHIN"

    ''' <summary>
    ''' データセットテーブル名(ERRテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' まとめ条件(荷主 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MATOME_CUSTTRIP As String = "01"

    ''' <summary>
    ''' まとめ条件(届先コード , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MATOME_DEST As String = "02"

    ''' <summary>
    ''' まとめ条件(届先JIS , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MATOME_DESTJIS As String = "03"

    'START YANAI 要望番号1424 支払処理
    ''' <summary>
    ''' まとめ条件(届先住所 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MATOME_DESTAD As String = "04"
    'END YANAI 要望番号1424 支払処理

    ''' <summary>
    ''' 確定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FIX_ACTION As String = "01"

    ''' <summary>
    ''' 確定解除
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FIX_CANCELL_ACTION As String = "02"

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GROUP_ACTION As String = "03"

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GROUP_CANCELL_ACTION As String = "04"

    ''' <summary>
    ''' 修正項目(支払先)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_SHIHARAI As String = "05"

    ''' <summary>
    ''' 修正項目(支払タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_TARIFF As String = "06"

    ''' <summary>
    ''' 修正項目(支払横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_YOKO As String = "07"

    ''' <summary>
    ''' 修正項目(支払割増タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_ETARIFF As String = "08"

    ''' <summary>
    ''' 一括変更可能件数(LMF080)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_LMF080 As String = "03"

    ''' <summary>
    ''' 再計算可能件数(LMF080)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SAIKEISAN_LMF080 As String = "03"

    ''' <summary>
    ''' 変更
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HENKO As String = "変更"

    ''' <summary>
    ''' 項目名（荷主コード）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUST_CD As String = "荷主コード"

    ''' <summary>
    ''' 日付(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYUKKA_DATE As String = "02"

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        FIX = 0
        FIX_CANCELL
        GROUP
        GROUP_CANCELL
        KENSAKU
        ENTER
        MASTEROPEN
        DOUBLECLICK
        SAVE
        CLOSE
        LOOPEDIT
        PRINT
        SAIKEISAN

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        CONDITION
        DATEKB
        FROM_DATA
        TO_DATA
        TARIFFKBN
        TARIFFCD
        TARIFFNM
        EXTCCD
        EXTCNM
        UNSOCD
        UNSOBRCD
        UNSONM
        CUSTCDL
        CUSTCDM
        CUSTNM
        DESTCD
        DESTNM
        CMB_GROUP
        KEY
        NOMAL
        GROUP
        CONDITIONKBN
        UNCHIN
        SHADATE
        TONKIRO
        UNCHINRYOHO
        GROUPNO
        GROUPMI
        GROUPSUMI
        GROUPRYOHO
        REV
        REVMI
        REVKAKU
        REVRYOHO
        MOTO
        IN_DATA
        OUT
        UNSO
        ALL
        'START YANAI 要望番号1424 支払処理
        SHIHARAI_NOMARL
        SHIHARAI_UNCO
        'END YANAI 要望番号1424 支払処理
        PNL_HENKO
        SHUSEI
        SHUSEIL
        HENKO
        SOKEITHI
        DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        KAKUTEI_FLG
        KAKUTEI
        SHUKKA
        NONYU
        CUST_NM
        CUST_REF_NO
        SHIHARAITO_CD
        SHIHARAI_NM
        DEST_NM
        'START YANAI 要望番号1424 支払処理
        DEST_AD
        'END YANAI 要望番号1424 支払処理
        UNSO_CD
        UNSO_BR_CD
        UNSO_NM
        TARIFF_KBN
        BUNRUI
        TARIFF_CD
        EXTC_TARIFF_CD
        JURYO
        KYORI
        UNCHIN
        ZEI
        ZEI_KBN
        GROUP
        GROUP_M
        REMARK
        KANRI_NO
        UNSO_NO
        UNSO_NO_EDA
        TRIP_NO
        MOTO_DATA_KBN
        MOTO_DATA
        DEST_CD
        MINASHI_DEST_CD
        BIN_KB
        BIN_NM
        DEST_JIS_CD
        CUST_CD
        CUST_CD_L
        CUST_CD_M
        NRS_BR_CD
        UNTIN_CALCULATION_KB
        VCLE_KB
        UNSO_ONDO_KB
        SIZE_KB
        SYS_UPD_DATE
        SYS_UPD_TIME
        CHK_UNCHIN
        SHIHARAI_UNCHIN
        SHIHARAI_CITY_EXTC
        SHIHARAI_WINT_EXTC
        SHIHARAI_RELY_EXTC
        SHIHARAI_TOLL
        SHIHARAI_INSU
        DECI_UNCHIN
        DECI_CITY_EXTC
        DECI_WINT_EXTC
        DECI_RELY_EXTC
        DECI_TOLL
        DECI_INSU
        DECI_NG_NB
        SHIHARAI_PKG_UT
        SHIHARAI_SYARYO_KB
        SHIHARAI_DANGER_KB
        SYS_UPD_FLG
        'START YANAI 要望番号1424 支払処理
        SHIHARAI_UNCHIN_UNSOLL
        'END YANAI 要望番号1424 支払処理
        LAST

    End Enum

End Class
