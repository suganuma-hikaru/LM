' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF040C : 運賃検索
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMF040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF040C
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

    'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
    ''' <summary>
    ''' まとめ指示アクション(日立物流用)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_GROUP_DIC As String = "SetGroupDataDic"
    'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

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
    Public Const TABLE_NM_IN As String = "LMF040IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMF040OUT"

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

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 並び順(日立物流用)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_DIC As String = "04"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    '2017/10/11 Annen アクサルタ 運賃按分計算の自動化対応 START
    ''' <summary>
    ''' 並び順（アクサルタ同送用）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ORDER_BY_AXALTA As String = "05"
    '2017/10/11 Annen アクサルタ 運賃按分計算の自動化対応 END

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
    'END YANAI 要望番号996

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
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
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    'Start s.kobayashi 20140519 Notes2183
    ''' <summary>
    ''' 修正項目(距離)
    ''' </summary>
    ''' <remarks></remarks>2
    Public Const SHUSEI_KYORI As String = "12"
    'END s.kobayashi 20140519 Notes2183

    ''' <summary>
    ''' 一括変更可能件数(LMF040)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_LMF040 As String = "03"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 再計算可能件数(LMF040)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SAIKEISAN_LMF040 As String = "03"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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
    'END YANAI 要望番号498

    'START YANAI 要望番号652
    ''' <summary>
    ''' 日付(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SYUKKA_DATE As String = "02"
    'END YANAI 要望番号652

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
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
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    'START YANAI 要望番号1285 運賃まとめ処理で納入日でまとまらない
    ''' <summary>
    ''' 日立物流まとめ荷主(埼玉)②
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_04 As String = "04"
    'END YANAI 要望番号1285 運賃まとめ処理で納入日でまとまらない

    '(要望番号2129) 2013.12.20 修正START
    ''' <summary>
    ''' 日立物流まとめ荷主(群馬BP)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DIC_MATOME_05 As String = "05"
    '(要望番号2129) 2013.12.20 修正START

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
        'START YANAI 要望番号561
        LOOPEDIT
        'END YANAI 要望番号561
        'START YANAI 要望番号582
        PRINT
        'END YANAI 要望番号582
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        SAIKEISAN
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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
        'START KURIHARA 要望番号:928
        DESTCD
        DESTNM
        'END KURIHARA 要望番号:928
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
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        VISIBLEKB
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応
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
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        CUST_REF_NO
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応
        SEIQTO_CD
        SEIQTO_NM
        DEST_NM
        UNSO_NM
        ''2013.02.27 / Notes1897:便区分追加
        BIN_KB 'YANAI 20120622 DIC運賃まとめ及び再計算対応より順列引き上げ
        BIN_NM 'YANAI 20120622 DIC運賃まとめ及び再計算対応より順列引き上げ
        ''2013.02.27 / Notes1897:便区分追加
        UNSOCO_NM
        TARIFF_KBN
        BUNRUI
        TARIFF_CD
        EXTC_TARIFF_CD
        JURYO
        KYORI
        UNCHIN
        ITEM_CURR_CD
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        ZBUKA_CD
        ABUKA_CD
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応
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
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        MINASHI_DEST_CD
        'BIN_KB 'ダミー
        'BIN_NM 'ダミー
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応
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
        'START YANAI 要望番号974
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
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        DECI_NG_NB
        SEIQ_PKG_UT
        SEIQ_SYARYO_KB
        SEIQ_DANGER_KB
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        SYS_UPD_FLG
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        DEST_ADDR
        LAST
        'END YANAI 要望番号974

    End Enum

End Class
