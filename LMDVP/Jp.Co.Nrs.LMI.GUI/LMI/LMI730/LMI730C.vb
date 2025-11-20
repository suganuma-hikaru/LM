' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI730C : 運賃差分抽出（JXTG）
'  作  成  者       :  katagiri
' ==========================================================================

''' <summary>
''' LMI730C定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI730C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 運賃データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 一括変更アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SAVE As String = "SaveUnchinItemData"

    ''' <summary>
    ''' 運賃バックアップアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INSERT As String = "BackupUnchin"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI730IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMI730OUT"

    ''' <summary>
    ''' データセットテーブル名(UNCHINテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNCHIN As String = "UNCHIN"

    ''' <summary>
    ''' バックアップ用運賃テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_BKUNCHIN As String = "UNCHIN_BACKUP"

    ''' <summary>
    ''' データセットテーブル名(ERRテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' 並び順(荷主 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_CUSTTRIP As String = "01"

    ''' <summary>
    ''' 並び順(届先コード)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_DEST As String = "02"

    ''' <summary>
    ''' 並び順(届先JIS)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_DESTJIS As String = "03"

    ''' <summary>
    ''' 並び順(日立物流用)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_DIC As String = "04"

    ''' <summary>
    ''' 並び順（アクサルタ同送用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_AXALTA As String = "05"

    ''' <summary>
    ''' 修正項目(請求先)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_SEIQTO As String = "05"

    ''' <summary>
    ''' 修正項目(タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_TARIFF As String = "06"

    ''' <summary>
    ''' 修正項目(横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_YOKO As String = "07"

    ''' <summary>
    ''' 修正項目(荷主)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_CUST As String = "08"

    'START YANAI 要望番号996
    ''' <summary>
    ''' 修正項目(割増タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_ETARIFF As String = "09"

    ''' <summary>
    ''' 修正項目(在庫部課)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_ZBUKACD As String = "10"

    ''' <summary>
    ''' 修正項目(扱い部課)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_ABUKACD As String = "11"

    ''' <summary>
    ''' 修正項目(距離)
    ''' </summary>
    ''' <remarks></remarks>2
    Public Const SHUSEI_KYORI As String = "12"

    ''' <summary>
    ''' 一括変更可能件数(LMI730)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_LMI730 As String = "03"

    ''' <summary>
    ''' 再計算可能件数(LMI730)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SAIKEISAN_LMI730 As String = "03"

    ''' <summary>
    ''' 変更
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HENKO As String = "変更"

    'START YANAI 要望番号498
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
    ''' 営業所コード(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_10 As String = "10"

    ''' <summary>
    ''' 営業所コード(群馬)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_30 As String = "30"

    ''' <summary>
    ''' 営業所コード(埼玉)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_50 As String = "50"

    ''' <summary>
    ''' 営業所コード(春日部)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_55 As String = "55"

    ''' <summary>
    ''' 日立物流まとめ荷主(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_01 As String = "01"

    ''' <summary>
    ''' 日立物流まとめ荷主(群馬)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_02 As String = "02"

    ''' <summary>
    ''' 日立物流まとめ荷主(埼玉)①
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_03 As String = "03"

    ''' <summary>
    ''' 日立物流まとめ荷主(埼玉)②
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_04 As String = "04"

    ''' <summary>
    ''' 日立物流まとめ荷主(群馬BP)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_05 As String = "05"

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        KENSAKU = 0
        ENTER
        MASTEROPEN
        DOUBLECLICK
        SAVE
        CLOSE
        LOOPEDIT
        PRINT
        SAIKEISAN
        BACKUP

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
        DRIVERCD
        DRIVERNM
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
        PNL_HENKO
        SHUSEI
        SHUSEIL
        SHUSEIM
        SHUSEIS
        SHUSEISS
        HENKO
        VISIBLEKB
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
        SEIQTO_CD
        SEIQTO_NM
        DEST_NM
        UNSO_NM
        BIN_KB
        BIN_NM
        UNSOCO_NM
        TARIFF_KBN
        BUNRUI
        TARIFF_CD
        EXTC_TARIFF_CD
        JURYO
        KYORI
        UNCHIN
        ITEM_CURR_CD
        ZBUKA_CD
        ABUKA_CD
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
        SHUKA_RELY_POINT
        HAIKA_RELY_POINT
        TRIP_NO_SHUKA
        TRIP_NO_CHUKEI
        TRIP_NO_HAIKA
        UNSOCO_SHUKA
        UNSOCO_CHUKEI
        UNSOCO_HAIKA
        DEST_CD
        MINASHI_DEST_CD
        DEST_JIS_CD
        CUST_CD
        CUST_CD_L
        CUST_CD_M
        UNSO_CD
        UNSO_BR_CD
        UNSOCO_CD
        UNSOCO_BR_CD
        NRS_BR_CD
        TYUKEI_HAISO_FLG
        UNTIN_CALCULATION_KB
        VCLE_KB
        UNSO_ONDO_KB
        SIZE_KB
        SYS_UPD_DATE
        SYS_UPD_TIME
        CHK_UNCHIN
        SEIQ_UNCHIN
        SEIQ_CITY_EXTC
        SEIQ_WINT_EXTC
        SEIQ_RELY_EXTC
        SEIQ_TOLL
        SEIQ_INSU
        DECI_UNCHIN
        DECI_CITY_EXTC
        DECI_WINT_EXTC
        DECI_RELY_EXTC
        DECI_TOLL
        DECI_INSU
        DECI_NG_NB
        SEIQ_PKG_UT
        SEIQ_SYARYO_KB
        SEIQ_DANGER_KB
        SYS_UPD_FLG
        DEST_ADDR
        BEFORE_TARIFF_CD
        BEFORE_UNCHIN
        LAST

    End Enum

End Class
