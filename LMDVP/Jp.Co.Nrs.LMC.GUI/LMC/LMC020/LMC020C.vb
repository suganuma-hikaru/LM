' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC020C : 出荷データ編集
'  作  成  者       :  矢内
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMC020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMC020IN"
    Public Const TABLE_NM_OUT_L As String = "LMC020_OUTKA_L"
    Public Const TABLE_NM_OUT_M As String = "LMC020_OUTKA_M"
    Public Const TABLE_NM_OUT_S As String = "LMC020_OUTKA_S"
    Public Const TABLE_NM_SAGYO As String = "LMC020_SAGYO"
    Public Const TABLE_NM_UNSO_L As String = "LMC020_UNSO_L"
    Public Const TABLE_NM_UNSO_M As String = "LMC020_UNSO_M"
    Public Const TABLE_NM_ZAI As String = "LMC020_ZAI"
    Public Const TABLE_NM_MAX As String = "LMC020_MAX_NO"
    Public Const TABLE_NM_KAGAMI_IN As String = "LMC020_KAGAMI_IN"
    Public Const TABLE_NM_STORAGE_SKYU_DATE As String = "LMC020_STORAGE_SKYU_DATE"
    Public Const TABLE_NM_HANDLING_SKYU_DATE As String = "LMC020_HANDLING_SKYU_DATE"
    Public Const TABLE_NM_UNCHIN_SKYU_DATE As String = "LMC020_UNCHIN_SKYU_DATE"
    Public Const TABLE_NM_SAGYO_SKYU_DATE As String = "LMC020_SAGYO_SKYU_DATE"
    Public Const TABLE_NM_YOKOMOCHI_SKYU_DATE As String = "LMC020_YOKOMOCHI_SKYU_DATE"
    Public Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"
    Public Const TABLE_NM_CUST As String = "LMC020_CUST"
    Public Const TABLE_NM_EDI_L As String = "LMC020_EDI_OUTKA_L"
    'START YANAI 要望番号853 まとめ処理対応
    Public Const TABLE_NM_ZAI_MATOME_IN As String = "LMC020_ZAI_MATOME_IN"
    Public Const TABLE_NM_ZAI_MATOME_OUT As String = "LMC020_ZAI_MATOME_OUT"
    'END YANAI 要望番号853 まとめ処理対応
    '要望番号:997 terakawa 2012.10.22 Start
    Public Const TABLE_NM_EDI_UPD_TBL As String = "LMC020_EDI_UPD_TBL"
    '要望番号:997 terakawa 2012.10.22 End
    '要望番号:1350 terakawa 2012.08.27 Start
    Public Const TABLE_NM_WORNING As String = "LMC020_WORNING"
    '要望番号:1350 terakawa 2012.08.27 End

    '要望番号:612 振替削除対応 nakamura 2012.11.20 Start
    Public Const TABLE_NM_FURIDEL As String = "LMC020_FURI_DEL"
    '要望番号:612 振替削除対応 nakamura 2012.11.20 End

    '2014/01/22 輸出情報追加 START
    Public Const TABLE_NM_EXPORT_L As String = "LMC020_EXPORT_L"
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピング対応 追加START
    Public Const TABLE_NM_MARK_HED As String = "LMC020_C_MARK_HED"
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
    Public Const TABLE_NM_MARK_DTL As String = "LMC020_C_MARK_DTL"
    '2015.07.21 協立化学　シッピング対応 追加END

    'ADD 2017/08/29 トール送状先頭５桁取得用
    Public Const TABLE_NM_OKURIJYO_WK As String = "LMC020_OKURIJYO_WK"

    '出荷梱包個数自動計算用
    Public Const TABLE_NM_CALC_OUTKA_PKG_NB_IN As String = "LMC020_CALC_OUTKA_PKG_NB_IN"    '2018/12/07 ADD 要望管理002171

    Public Const UNSO_KITAKUGAKU As String = "LMC020_UNSO_KITAKU"    '2019/05/31 依頼番号 : 005136  

    Public Const TABLE_INOUTKAEDI_HED_FJF As String = "LMC020_INOUTKAEDI_HED_FJF"

    Public Const TABLE_JIKAI_BUNNOU As String = "LMC020_JIKAI_BUNNOU"

    'モード別画面ロック用
    Public Const MODE_READONLY As String = "0"
    Public Const MODE_EDIT As String = "1"
    Public Const MODE_UNSO As String = "2"
    Public Const MODE_SHUSAN As String = "3"
    Public Const MODE_UNSO_CHANGE As String = "4"
    Public Const MODE_TODOKESAKI_CHANGE As String = "5"

    '引当モード
    Public Const MODE_HIKIATE As String = "00"
    Public Const MODE_AUTO_HIKIATE As String = "01"

    '進捗区分
    Public Const SINTYOKU10 As String = "10" '予定入力済
    Public Const SINTYOKU30 As String = "30" '指図書印刷
    Public Const SINTYOKU40 As String = "40" '出庫済
    Public Const SINTYOKU50 As String = "50" '検品済
    Public Const SINTYOKU60 As String = "60" '出荷済
    Public Const SINTYOKU90 As String = "90" '報告済

    '計算コード区分
    Public Const CALC_KB_NISUGATA As String = "01"
    Public Const CALC_KB_KURUMA As String = "02"

    'スプレッドチェックボックス用
    Public Const FLG_TRUE As String = "True"
    Public Const FLG_FALSE As String = "False"

    'ソートの桁数
    Public Const SORT_MAX As String = "99"

    '梱数の桁数
    Public Const NB_MAX_10 As String = "9999999999"
    Public Const NB_MAX_5 As String = "99999"
    Public Const NB_MAX_4 As String = "9999"
    Public Const NB_MIN As String = "0"
    Public Const NB_MAX_5_NUM As Decimal = 99999
    Public Const NB_MAX_4_NUM As Decimal = 9999
    '2015.07.08 協立化学　シッピングマーク対応 追加START
    Public Const NB_MAX_3 As String = "999"
    '2015.07.08 協立化学　シッピングマーク対応 追加END

    '数量・入目の桁数
    Public Const IRIME_MAX As String = "999999.999"
    Public Const IRIME_NUM As String = "0.001"
    Public Const QT_MAX As String = "999999999.999"
    Public Const QT_MIN As String = "0"
    Public Const QT_MAX_NUM As Double = 999999999.999
    Public Const QT_MIN_NUM As Decimal = 0


    '出荷(小)個数の桁数
    Public Const KOSU_MAX As String = "9999999999"
    Public Const KOSU_MIN As String = "0"
    Public Const KOSU_MAX_NUM As Decimal = 9999999999
    Public Const KOSU_MIN_NUM As Decimal = 0

    '出荷(小)数量の桁数
    Public Const SURYO_MAX As String = "999999999999.999"
    Public Const SURYO_MIN As String = "0"
    Public Const SURYO_MAX_NUM As Double = 999999999999.999
    Public Const SURYO_MIN_NUM As Decimal = 0

    '重量の桁数
    Public Const UNSO_JURYO_MAX As String = "999999999.999"
    Public Const UNSO_JURYO_MIN As String = "0"

    '作業レコードMax数
    Public Const SAGYO_L_REC_CNT As Integer = 5
    Public Const SAGYO_M_REC_CNT As Integer = 7

    '日付初期値
    'Public Const OUTKA_DATE_INIT_01 As String = "01"
    'Public Const OUTKA_DATE_INIT_02 As String = "02"
    'Public Const OUTKA_DATE_INIT_03 As String = "03"
    Public Const OUTKA_DATE_INIT_04 As String = "04"
    Public Const OUTKA_DATE_INIT_05 As String = "05"

    '0固定値
    Public Const PLUS_ZERO As String = "0.000"
    Public Const MINUS_ZERO As String = "-0.000"

    'プログラムID
    Public Const PGID_LMC020 As String = "LMC020"
    Public Const PGID_LMD040 As String = "LMD040"

    '印刷種別(出荷報告)
    Public Const PRINT_HOKOKU As String = "06"

    '印刷種別(出荷指図)
    Public Const PRINT_SASHIZUSHO As String = "07"

    '届先マスタ使用箇所のフィールド名
    Public Const TODOKECD As String = "txtTodokesakiCd"
    Public Const URICD As String = "txtUriCd"

    '作業マスタ使用箇所のフィールド名
    Public Const SAGYO_L01 As String = "txtSagyoL1"
    Public Const SAGYO_L02 As String = "txtSagyoL2"
    Public Const SAGYO_L03 As String = "txtSagyoL3"
    Public Const SAGYO_L04 As String = "txtSagyoL4"
    Public Const SAGYO_L05 As String = "txtSagyoL5"
    Public Const SAGYO_M01 As String = "txtSagyoM1"
    Public Const SAGYO_M02 As String = "txtSagyoM2"
    Public Const SAGYO_M03 As String = "txtSagyoM3"
    Public Const SAGYO_M04 As String = "txtSagyoM4"
    Public Const SAGYO_M05 As String = "txtSagyoM5"
    Public Const SAGYO_DESTM01 As String = "txtDestSagyoM1"
    Public Const SAGYO_DESTM02 As String = "txtDestSagyoM2"

    '課税区分
    Public Const TAX_KB01 As String = "01"  '課税
    Public Const TAX_KB02 As String = "02"  '免税
    Public Const TAX_KB03 As String = "03"  '非課税
    Public Const TAX_KB04 As String = "04"  '内税

    '要望番号:1454 yamanaka 2012.09.20 Start
    'CSV種別
    Public Const OUTKA_SASHIZU As String = "05"  
    Public Const OUTKA_CANCEL As String = "17"
    '要望番号:1454 yamanaka 2012.09.20 End

    '要望番号:612 nakamura 2012.12.07 Start
    '引当状況
    Public Const HIKIATE_ARI As String = "済"
    Public Const HIKIATE_NASI As String = "未"

    Public Const HIKIATE_ARI_ENG As String = "Fin"
    Public Const HIKIATE_NASI_ENG As String = "Yet"

    '営業所コード　
    Public Const BR_CD_GUNMA As String = "30"       'ADD 2018/04/24

    '2017/09/25 追加 李↓
    Public Const HIKIATE_NASI_KR As String = "미(未)"
    'Public Const HIKIATE_NASI_CHI As String = "Yet"
    '2017/09/25 追加 李↑

    '要望番号:612 nakamura 2012.12.07 End

    Public Const SYS_DATA As Integer = 2

    '2015.10.30 tusnehira add Start
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2015.10.30 tusnehira add End

    '2018/12/07 ADD START 要望管理002171
    '出荷梱包個数自動計算用
    Public Const USE_GAMEN_VALUE_FALSE As String = "0"  '自動計算する
    Public Const USE_GAMEN_VALUE_TRUE As String = "1"   '画面入力値を使用する
    '2018/12/07 ADD END   要望管理002171

#If True Then   'ADD 2020/03/25 011614   【LMS】アクタス_引当て時に商品コード先頭7桁のみで引当て変更
    'Public Const AKUTASU_BRCUST As String = "1000750"  '千葉アクタス荷主CD_L
    Public Const AKUTASU_BRCUST As String = "1500750"  '土気アクタス荷主CD_L
#End If
    Public Enum SysData As Integer
        YYYYMMDD = 0
        HHMMSSsss
    End Enum

    'イベント種別
    Public Enum EventShubetsu As Integer

        SINKI = 0       '新規
        HENSHU          '編集
        FUKUSHA         '複写
        DEL             '削除
        HIKIATE         '引当
        TORIKESHI       '完了取消
        UNSO            '運送修正
        SHUSAN          '終算日修正
        MASTER          'マスタ参照
        HOZON           '保存
        CLOSE           '閉じる
        PRINT           '印刷
        TODOKESAKI      '新規（届先）
        INS_M           '行追加（中）
        INS_M_ZERO      '行追加（中）（出荷(中)が0件の時)
        DEL_M           '行削除（中）
        COPY_M          '行複写（中）
        RIREKI          '履歴照会
        DEL_S           '行削除（小）
        HENKO           '一括変更
        DOUBLE_CLICK    'ダブルクリック
        LEAVE_CELL      'セルのロストフォーカス
        CHANGE_KOSU     '梱数・端数変更時
        CHANGE_SURYO    '数量変更時
        CHANGE_IRIME    '入目変更時
        OPT_KOSU        '出荷単位（個数）
        OPT_SURYO       '出荷単位（数量）
        OPT_KOWAKE      '出荷単位（小分け）
        OPT_SAMPLE      '出荷単位（サンプル）
        UNSO_CHANGE     '運送手配変更時
        UNSOTARIFF_CHANGE 'タリフ分類区分変更時
        MASTER_ENTER    'Enterボタン押下時のマスタ参照（作業項目マスタ用）
        LEAVE_DEST_CD   '届先コードのロストフォーカス
        TODOKESAKI_CHANGE '届先区分の変更
        PRINT_CHANGE    '印刷種別変更時
        CHANGE_GOODS    '商品変更ボタン押下時
        'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
        LEAVE_UNSO_CD   '運送会社コードのロストフォーカス
        'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
        'START 中村 要望番号0612 振替一括削除対応
        FURI_DEL        '振替削除
        'END 中村 要望番号0612 振替一括削除対応
        JIKKOU           '実行
        TAB_LEAVE        '2015.07.08 協立化学　シッピング対応 追加START
        OPTIONAL_MODE    '2015.07.08 協立化学　シッピング対応 追加START

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex
        JIKKOU = 0
        BTN_JIKKOU
        PRINT
        PRINT_CNT
        PRINT_CNT_FROM
        PRINT_CNT_TO
        BTN_PRINT
        GRPSYUKKA
        KANRINOL
        EIGYO
        SOKO
        FURINO
        SYUKKADATE
        SYUKKAYOTEIDATE
        NONYUDATE
        NONYUKBN
        HOUKOKUDATE
        SYUSANDATE
        OUTKAKBN
        OUTKASYUBETU
        OUTKASTATEKBN
        WHSTATEKBN
        NIFUDA
        NOUHIN
        OKURI
        COA
        HOUKOKU
        ORDERNO
        BUYERORDNO
        PICK
        SYUKKAHOUKOKU
        HOKANRYO
        CUSTCDL
        CUSTCDM
        CUSTNM
        NIYAKURYO
        KONPOSU
        SOUKOSU
        URICD
        URINM
        YOUOKURI
        OKURINO
        SITEINOUHIN
        BUNSEKIHYO
        BTNNEW
        DESTKBN
        DESTCD
        DESTNM
        DESTADRESS1
        DESTADRESS2
        DESTADRESS3
        DESTTEL
        NOUHINTEKIYO
        SYUKKATYUI
        HAISOUTYUI
        WH_TAB_RMK
        ORDTYPE
        URGENT_YN
        WH_TAB_YN
        WH_TAB_HOKOKU_YN
        WH_TAB_NO_SIJI
        TAB
        BTN_INM
        BTN_COPYM
        BTN_DELM
        BTN_RIREKI
        PNL_KENSAKU
        SER_SYOHINCD
        SER_SYOHINNM
        SER_LOT
        PNL_HENKO
        PRINTSORTHENKO
        SPR_OUTKAM
        KANRINOM
        PRINTSORT
        JOKYO
        GOODSCD
        GOODSNM
        LOT
        ORDERNOM
        RSVNO
        SERIALNO
        BUYERORDNOM
        BUNSEKIHYOM
        OUTKATANI
        GRPSHUKATANI
        KOSU_TANI
        SURYO_TANI
        KOWAKE_TANI
        SAMPLE_TANI
        IRIME
        KONPOKOSU
        ONDO
        TAKYUBIN
        GRPKOSU
        KONSU
        IRISU
        KOSU
        HASU
        HIKI_ZUMIKOSU
        HIKI_ZANKOSU
        GRPSURYO
        SURYO
        HIKI_ZUMISURYO
        HIKI_ZANSURYO
        GOODSREMARK
        EDI_OUTKATTL_NB     'ADD 要望番号1959
        EDI_OUTKATTL_QT     'ADD 要望番号1959
        GRPSAGYOM
        SAGYOCD_M1
        SAGYONM_M1
        SAGYORMK_M1
        SAGYOCD_M2
        SAGYONM_M2
        SAGYORMK_M2
        SAGYOCD_M3
        SAGYONM_M3
        SAGYORMK_M3
        SAGYOCD_M4
        SAGYONM_M4
        SAGYORMK_M4
        SAGYOCD_M5
        SAGYONM_M5
        SAGYORMK_M5
        DESTSAGYOCD_M1
        DESTSAGYONM_M1
        DESTSAGYORMK_M1
        DESTSAGYOCD_M2
        DESTSAGYONM_M2
        DESTSAGYORMK_M2
        BTN_DELS
        SPR_OUTKAS
        GRPUNSO
        UNSONO
        UNSOKBN
        TEHAI
        SYARYO
        BIN
        MOTOTYAKU
        JURYO
        KYORI
        UNSOCOCD_L
        UNSOCOCD_M
        UNSOCONM_L
        UNSOCONM_M
        UNSOKAZEIKBN
        TARIFFCD
        TARIFFNM
        WARITARIFF
        SIHARAI_TARIFFCD            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        SIHARAI_TARIFFNM            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        SIHARAI_WARITARIFF          'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        YUSOEIGYO
        AUTO_DENP_KBN                '要望番号:2408 2015.09.17 ADD
        AUTO_DENP_NO                 '要望番号:2408 2015.09.17 ADD   
        GRPSAGYOL
        SAGYOCD_L1
        SAGYONM_L1
        SAGYORMK_L1
        SAGYOCD_L2
        SAGYONM_L2
        SAGYORMK_L2
        SAGYOCD_L3
        SAGYONM_L3
        SAGYORMK_L3
        SAGYOCD_L4
        SAGYONM_L4
        SAGYORMK_L4
        SAGYOCD_L5
        SAGYONM_L5
        SAGYORMK_L5
        '2014/01/22 追加 START
        TAB_TOP '2014/01/22 追加
        SHIP_NM
        DESTINATION
        BOOKING_NO
        VOYAGE_NO
        SHIPPER_CD
        CONT_LOADING_DATE
        STORAGE_TEST_DATE
        STORAGE_TEST_TIME
        DEPARTURE_DATE
        CONTAINER_NO
        CONTAINER_NM
        CONTAINER_SIZE
        '2014/01/22 追加 END
        '2015.07.08 協立化学　シッピングマーク対応 追加START
        CASE_NO_FROM
        CASE_NO_TO
        MARK_INFO_1
        MARK_INFO_2
        MARK_INFO_3
        MARK_INFO_4
        MARK_INFO_5
        MARK_INFO_6
        MARK_INFO_7
        MARK_INFO_8
        MARK_INFO_9
        MARK_INFO_10
        '2015.07.08 協立化学　シッピングマーク対応 追加END

    End Enum

    'スプレッド列数
    Public Const sprSyukkaMColCount As Integer = 38  '出荷（中）

    ' Spread部列インデックス用列挙対    出荷（中）
    Public Enum sprSyukkaMColumnIndex

        DEFM = 0
        PRT_ORDER
        SHOBO_CD
        KANRI_NO
        GOODS_CD
        GOODS_NM
        SYUKKA_TANI
        IRIME
        NB
        ALL_SURYO
        ZANSU
        HIKIATE_JK
        GOODS_COMMENT
        M_LOT_NO
        SHOBO_NM
        SYS_DEL_FLG
        REC_NO
        SEARCH_KEY_1
        UNSO_ONDO_KB
        PKG_UT
        STD_IRIME_NB
        STD_WT_KGS
        TARE_YN
        OUTKA_ATT
        REMARK
        REMARK_OUT
        TAX_KB
        HIKIATE_ALERT_YN
        GOODS_CD_NRS_FROM
        CUST_CD_S
        CUST_CD_SS
        NB_UT
        EDI_FLG
        SYUKKA_TANI_KOWAKE
        CASE_NO_FROM
        CASE_NO_TO
        MARK_SYS_DEL_FLG
        MARK_UP_KBN

    End Enum

    ''2015.07.08 協立化学　シッピングマーク対応　追加START

    'Public Enum sprMarkHedcolumnIndex

    '    DEF_HED = 0
    '    SYS_DEL_FLG
    '    UP_KBN
    '    OUTKA_NO_M
    '    CASE_NO_FROM
    '    CASE_NO_TO
    '    MARK_INFO_1
    '    MARK_INFO_2
    '    MARK_INFO_3
    '    MARK_INFO_4
    '    MARK_INFO_5
    '    MARK_INFO_6
    '    MARK_INFO_7
    '    MARK_INFO_8
    '    MARK_INFO_9
    '    MARK_INFO_10

    'End Enum

    '2015.07.08 協立化学　シッピングマーク対応　追加END

    'スプレッド列数
    Public Const sprDtlMColCount As Integer = 57   '出荷（小）

    ' Spread部列インデックス用列挙対    出荷（小）
    Public Enum sprDtlMColumnIndex

        DEF = 0
        LOT_NO
        IRIME
        TOU_NO
        SHITSU_NO
        ZONE_CD
        LOCA
        ALCTD_NB
        ALCTD_QT
        ALCTD_CAN_NB
        ALCTD_CAN_QT
        NAKAMI
        GAIKAN
        JOTAI_NM
        REMARK
        INKO_DATE
        LT_DATE
        HORYUHIN
        BOGAIHIN
        RSV_NO
        SERIAL_NO
        GOODS_CRT_DATE
        WARIATE_NM
        NYUBAN_S
        SHO_NO
        ZAI_REC_NO
        INKA_NO_L
        INKA_NO_M
        INKA_NO_S
        SMPL_FLAG
        GOODS_COND_KB_1
        GOODS_COND_KB_2
        GOODS_COND_KB_3
        OFB_KB
        SPD_KB
        PORA_ZAI_NB
        PORA_ZAI_QT
        ALLOC_CAN_NB_HOZON
        ALLOC_CAN_QT_HOZON
        ALLOC_CAN_NB
        ALLOC_CAN_QT
        SYS_DEL_FLG
        REC_NO
        SYS_UPD_DATE
        SYS_UPD_TIME
        HOKAN_YN
        TAX_KB
        INKO_PLAN_DATE
        WARIATE
        ALCTD_NB_HOZON
        ALCTD_QT_HOZON
        DEST_CD_P
        UPDATE_FLG
        ALCTD_CAN_NB_MATOME
        ALCTD_CAN_QT_MATOME
        ALCTD_NB_MATOME
        ALCTD_QT_MATOME

    End Enum

    '追加開始 --- 2014.07.24 kikuchi
    '分析表添付　値
    Public Const BUNSEKI_ARI As String = "01"
    Public Const BUNSEKI_NASI As String = "02"
    '追加終了 ---

    'タブレット対応
    Public Const WH_TAB_STATUS_00 As String = "00"
    Public Const WH_TAB_STATUS_01 As String = "01"
    Public Const WH_TAB_STATUS_02 As String = "02"
    Public Const WH_TAB_STATUS_99 As String = "99"
    Public Const WH_TAB_YN_NO As String = "00"
    Public Const WH_TAB_YN_YES As String = "01"
End Class
