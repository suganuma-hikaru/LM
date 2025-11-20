' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM040C : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================

''' <summary>
''' LMM040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMM040IN"
    Public Const TABLE_NM_OUT As String = "LMM040OUT"
    Public Const TABLE_NM_OUT2 As String = "LMM040_DEST_DETAILS"
    Public Const TABLE_NM_DEST_DETAILS_MAXEDA As String = "LMM040_DEST_DETAILS_MAXEDA"
    Public Const TABLE_NM_UNCHIN_TARIFF_SET_MAXCD As String = "LMM040_UNCHIN_TARIFF_SET_MAXCD"

    'コンボボックス初期値
    Public Const LARGECAR As String = "01"

    '荷主マスタ存在チェックデフォルトコード
    Public Const NINUSHI As String = "00"

    '2015.11.02 tusnehira add Start
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2015.11.02 tusnehira add End

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        'START ADD 2013/09/10 KOBAYASHI WIT対応
        INSATSU
        'END   ADD 2013/09/10 KOBAYASHI WIT対応
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK
        INS_T           '行追加
        DEL_T           '行削除

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        CUSTCDL
        CUSTNML
        DESTCD
        EDICD
        DESTNM
        '要望番号:1330 terakawa 2012.08.09 Start
        KANANM
        '要望番号:1330 terakawa 2012.08.09 End
        ZIP
        AD1
        AD2
        AD3
        DICDESTCD
        '要望番号:1424② yamanaka 2012.09.20 Start
        SHIHARAI_AD
        '要望番号:1424② yamanaka 2012.09.20 End
        TEL
        JIS
        SPNHSKB
        FAX
        KYORI
        COAYN
        UNSO
        SPUNSOCD
        SPUNSOBRCD
        SPUNSONM
        PICK
        BIN
        MOTOCHAKUKBN
        CARGOTIMELIMIT
        LARGECARYN
        DELIATT
        'START YANAI 要望番号881
        REMARK
        'END YANAI 要望番号881
        SALESCD
        SALESNM
        URIAGECD
        URIAGENM
        UNCHINSEIQTOCD
        UNCHINSEIQTONM
        UNCHINTARIFFCD1
        UNCHINTARIFFREM1
        TARIFFBUNRUIKBN
        UNCHINTARIFFCD2
        UNCHINTARIFFREM2
        EXTCTARIFFCD
        EXTCTARIFFREM
        YOKOTARIFFCD
        YOKOTARIFFREM
        DETAIL2
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum

    ''' <summary>
    ''' Spread部(上部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        CUST_CD_L
        CUST_NM_L
        DEST_CD
        DEST_NM
        '要望番号:1330 terakawa 2012.08.09 Start
        KANA_NM
        '要望番号:1330 terakawa 2012.08.09 End
        AD
        TEL
        EDI_CD
        ZIP
        AD_2
        AD_3
        CUST_DEST_CD
        '要望番号:1424② yamanaka 2012.09.20 Start
        SHIHARAI_AD
        '要望番号:1424② yamanaka 2012.09.20 End
        SALES_CD
        SALES_NM
        SP_NHS_KB
        COA_YN
        SP_UNSO_CD
        SP_UNSO_BR_CD
        SP_UNSO_BR_NM
        DELI_ATT
        'START YANAI 要望番号881
        REMARK
        'END YANAI 要望番号881
        CARGO_TIME_LIMIT
        LARGE_CAR_YN
        FAX
        UNCHIN_SEIQTO_CD
        UNCHIN_SEIQTO_NM
        JIS
        KYORI
        PIC_KB
        BIN_KB
        MOTO_CHAKU_KB
        URIAGE_CD
        URIAGE_NM
        TARIFF_BUNRUI_KB
        UNCHIN_TARIFF_CD1
        UNCHIN_TARIFF_NM1
        UNCHIN_TARIFF_CD2
        UNCHIN_TARIFF_NM2
        EXTC_TARIFF_CD
        EXTC_TARIFF_NM
        YOKO_TARIFF_CD
        YOKO_TARIFF_NM
        CUST_CD_M
        SET_MST_CD
        SET_KB
        SYS_UPD_DATE_T
        SYS_UPD_TIME_T
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        '要望番号:1330 terakawa 2012.08.09 Start
        COLMUN_COUNT
        '要望番号:1330 terakawa 2012.08.09 End

    End Enum

    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        DEST_CD_EDA
        SUB_KB
        SET_NAIYO
        REMARK
        DEST_CD
        CUST_CD_L
        UPD_FLG
        SYS_DEL_FLG_T

    End Enum

End Class
