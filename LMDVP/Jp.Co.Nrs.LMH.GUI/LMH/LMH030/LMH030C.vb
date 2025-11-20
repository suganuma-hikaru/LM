' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH030C : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMH030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH030INOUT"
    Public Const TABLE_NM_OUT As String = "LMH030OUT"
    Public Const TABLE_NM_JUDGE As String = "LMH030_JUDGE"
    Public Const TABLE_NM_OUTKAEDI_L As String = "LMH030_OUTKAEDI_L"
    Public Const TABLE_NM_OUTKAEDI_M As String = "LMH030_OUTKAEDI_M"
    Public Const TABLE_NM_EDI_RCV_HED As String = "LMH030_EDI_RCV_HED"
    Public Const TABLE_NM_EDI_RCV_DTL As String = "LMH030_EDI_RCV_DTL"
    Public Const TABLE_NM_C_OUTKA_L As String = "LMH030_C_OUTKA_L"
    Public Const TABLE_NM_C_OUTKA_M As String = "LMH030_C_OUTKA_M"
    Public Const TABLE_NM_EDI_SND As String = "LMH030_EDI_SND"
    Public Const TABLE_NM_UPDATE_KEY As String = "LMH030OUT_UPDATE_KEY"
    Public Const TABLE_NM_UPDATE_VALUE As String = "LMH030OUT_UPDATE_VALUE"
    Public Const TABLE_NM_WARNING_HED As String = "WARNING_HED"
    Public Const TABLE_NM_HIMODUKE As String = "LMH030_HIMODUKE"
    Public Const TABLE_NM_GUIERROR As String = "LMH030_GUIERROR"
    '▼▼▼要望番号:467
    Public Const TABLE_NM_CSVIN As String = "LMH030_CSVIN"
    Public Const TABLE_NM_SNTK_CSVOUT As String = "LMH030_SNTK_CSVOUT"
    Public Const TABLE_NM_OUTPUTIN As String = "LMH030_OUTPUTIN"    '2012.03.03 大阪対応ADD
    '▲▲▲要望番号:467

#If True Then ' BP運送会社自動設定対応 20161115 added by inoue

    Public Const TABLE_NM_OUTKA_SAVE As String = "LMH030IN_OUTKA_SAVE_BP"
    Public Const TABLE_NM_UNSO_BY_WGT_AND_DEST As String = "LMH030OUT_UNSO_BY_WGT_AND_DEST"
    Public Const TABLE_NM_CHARTER_MANAGEMENT As String = "LMH030OUT_CHARTER_MANAGEMENT"

#End If

    '取込処理用
    Public Const SEMIEDI_INFO As String = "LMH030_SEMIEDI_INFO" '取込対応 20120305
    Public Const EDI_TORIKOMI_HED As String = "LMH030_EDI_TORIKOMI_HED" '取込対応 20120305
    Public Const EDI_TORIKOMI_DTL As String = "LMH030_EDI_TORIKOMI_DTL" '取込対応 20120305

    '受信確認送信用
    Public Const TABLE_NM_RCVCONF_INFO As String = "LMH030_EDI_RCVCONF_INFO" '要望番号1005 2012.04.18 ADD

    '出荷梱包個数自動計算用
    Public Const TABLE_NM_CALC_OUTKA_PKG_NB_IN As String = "LMH030_CALC_OUTKA_PKG_NB_IN"   '2018/12/07 ADD 要望管理002171

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    'LMH040ステータス定数
    Public Const LMH040_STA_REF As String = "0"        '参照
    Public Const LMH040_STA_SINKI As String = "1"      '新規
    Public Const LMH040_STA_COPY As String = "2"       '複写 
    '▼▼▼要望番号:467
    '出力種別
    Public Const PRINT_SAKURA_CHECKLIST As String = "01" 'サクラチェックリスト(未使用)
    Public Const PRINT_CSV As String = "02"              '出荷依頼送信データ作成
    Public Const JYUSIN_PRT As String = "03"             '受信帳票
    Public Const JYUSIN_ICHIRAN As String = "04"         '受信一覧表 '2012.03.16 ADD
    Public Const OUTKA_PRT As String = "05"              '出荷伝票  '2012.03.16 ADD
    Public Const RCVCONF_SEND As String = "06"           '受信確認送信  '2012.04.18 ADD
    Public Const EDIOUTKA_TORIKESHI_CHECKLIST As String = "07" 'EDI出荷取消チェックリスト '2012.09.12 要望番号1429 ADD
    Public Const NOHIN_OKURIJO As String = "08"          'EDI納品送状_BP(埼玉) 2012.12.07
    Public Const NOHINSYO_AUTO_BAKKUSU As String = "09"  'EDI納品書_BP･オートバックス(埼玉) 2012.12.07
    Public Const NOHIN_YELLOW_HAT As String = "10"       'EDI納品書_BP･クティー(埼玉) 2012.12.07
    Public Const NOHIN_TACTI As String = "11"            'EDI納品書_BP･イエローハット(埼玉) 2012.12.07
    Public Const NOHIN_NIKKO As String = "12"            'EDI納品書_日興産業･イエローハット(大阪) 2012.12.17
    Public Const NOHIN_RONZA As String = "13"            'EDI納品書_ロンザ(千葉) 2012.12.17
    Public Const NOHIN_OKURIJO_AUTO As String = "14"     'EDI納品書【ｵｰﾄ用】BP(群馬) 2014.2.1
    Public Const SHIKIRI_TERUMO As String = "15"         'テルモ仕切書　テルモ(千葉) 2015.1.20
    Public Const NIHUDA_TOR As String = "16"             '東レ・ダウ荷札 東レ・ダウ(千葉) 2015.02.26
    Public Const INVOICE_NIPPON_EXPRESS_BP As String = "17"  ' EDI送状_BP(日通) 20161115
    Public Const NOHIN_NICHIGO As String = "18"              ' EDI納品書_日本合成化学 20170113
    Public Const IKKATU_PRT As String = "99"             '一括印刷      '要望番号1007 2012.05.11 EDIT

    '要望番号1007 2012.05.11 追加START
    '出力区分
    Public Const OUTPUT_MI As String = "01"              '未出力
    Public Const OUTPUT_SUMI As String = "02"            '出力済

    '要望番号1007 2012.05.11 追加END

    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    '受信伝票印刷済表示
    Public Const PRT_SUMI As String = "済"
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End


    'CSV作成先区分
    Public Const CSV_SNTK As String = "01"
    '▲▲▲要望番号:467

    '受信確認送信荷主区分
    Public Const RCV_SEND_CUST_UKM As String = "01"     '浮間合成(大阪)     '2012.04.18 ADD


    ''' <summary>
    ''' EDI荷主インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EDI_CUST_INDEX

        ''' <summary>
        ''' 'BP・カストロール(群馬)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BP As String = "77"         'BP・カストロール対応 terakawa 2012.12.26

        ''' <summary>
        ''' 日本合成
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NICHIGO As String = "57"

#If True Then   'ADD 2020/03/13   011441【LMS】ITWﾊﾟﾌｫｰﾏﾝｽ_出荷データに意図しない売り上げ先が設定されている     
        ''' <summary>
        ''' ITWﾊﾟﾌｫｰﾏﾝｽ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ITW As String = "121"

#End If


    End Class


#If True Then ' BP運送会社自動設定対応 20161116 added by inoue

    ''' <summary>
    ''' 入出荷区分(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const M_EDI_CUST_INOUT_KB_INKA As String = "1"

    ''' <summary>
    ''' 入出荷区分(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const M_EDI_CUST_INOUT_KB_OUTKA As String = "0"

    ''' <summary>
    ''' カラム名(EDI荷主INDEX)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const COLUMN_NM_EDI_CUST_INDEX As String = "EDI_CUST_INDEX"

    ''' <summary>
    ''' BLFメソッド名(BP運送会社取得)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BLF_FUNC_NM_SELECT_UNSO_BP As String = "SelectUnsoByWgtAndDestList"

    ''' <summary>
    ''' 日付フォーマット
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_FORMAT As String = "yyyyMMdd"


    ''' <summary>
    ''' 申し送り区分(ポンプアップ)
    ''' </summary>
    ''' <remarks>
    ''' BPカストロールのEDIデータ
    ''' </remarks>
    Public Const MOSIOKURI_KB_POMPUP As String = "JP"

#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue

    ''' <summary>
    ''' 出力区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OUTPUT_KB

        ''' <summary>
        ''' 未出力
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PENDING_OUTPUT As String = "01"

        ''' <summary>
        ''' 出力済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUTTED As String = "02"

    End Class

    ''' <summary>
    ''' 運送手配区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UNSO_TEHAI_KB

        ''' <summary>
        ''' 日陸手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS As String = "10"

        ''' <summary>
        ''' 先方手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OTHER_PARTY As String = "20"

        ''' <summary>
        ''' 未定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNFIXED As String = "90"

    End Class

    ''' <summary>
    ''' 輸送会社コード(日本合成化学用)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class NICHIGO_YUSO_COMP_CD

        ''' <summary>
        ''' 日陸手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS As String = "K7"

    End Class

    Public Class COMBO_NOHIN_PRT_KBN_GROUP

        ''' <summary>
        ''' BP
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BP As String = "B013"

        ''' <summary>
        ''' 日本合成
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NICHIGO As String = "E044"
    End Class

    ''' <summary>
    ''' DIC配送指示No.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DIC_HAISO_SIJI_MO

        ''' <summary>
        ''' 配送指示No.桁数
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DIGIT_NUMBER As Integer = 11

    End Class

    Public Class SNTK_CSVOUT_COLUMNS
        Public Const UKETSUKENO_EDA As String = "UKETSUKENO_EDA"
        Public Const IRAI_YMD As String = "IRAI_YMD"
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const PRINT_SORT As String = "PRINT_SORT"
        Public Const TYPE As String = "PRINT_TYPE"
        Public Const EDI_CTL_NO As String = "EDI_CTL_NO"
        Public Const EDI_CTL_NO_CHU As String = "EDI_CTL_NO_CHU"
        Public Const OUTKA_NO_L As String = "OUTKA_NO_L"
        Public Const OUTKA_NO_M As String = "OUTKA_NO_M"
        Public Const SYUKKASAKI_CD As String = "SYUKKASAKI_CD"
        Public Const SYUKKASAKI_ADD_LINE1 As String = "SYUKKASAKI_ADD_LINE1"
        Public Const SYUKKASAKI_ADD_LINE2 As String = "SYUKKASAKI_ADD_LINE2"
        Public Const SYUKKASAKI_TEL As String = "SYUKKASAKI_TEL"
        Public Const SYUKKASAKI_NM1 As String = "SYUKKASAKI_NM1"
        Public Const SYUKKASAKI_NM2 As String = "SYUKKASAKI_NM2"
        Public Const SYUKKASAKI_NM3 As String = "SYUKKASAKI_NM3"
        Public Const SYUKKASAKI_NM4 As String = "SYUKKASAKI_NM4"
        Public Const NOUKI_DATE As String = "NOUKI_DATE"
        Public Const NOUNYU_JIKOKU_NM As String = "NOUNYU_JIKOKU_NM"
        Public Const SYUKKA_DATE As String = "SYUKKA_DATE"
        Public Const SHIHARAININ_NM1L As String = "SHIHARAININ_NM1L"
        Public Const JYUCHUSAKI_NM1L As String = "JYUCHUSAKI_NM1L"
        Public Const SENPO_ORDER_NO As String = "SENPO_ORDER_NO"
        Public Const ITEM_RYAKUGO As String = "ITEM_RYAKUGO"
        Public Const ITEM_GROUP As String = "ITEM_GROUP"
        Public Const ITEM_AISYO As String = "ITEM_AISYO"
        Public Const GRADE1 As String = "GRADE1"
        Public Const GRADE2 As String = "GRADE2"
        Public Const YOURYOU As String = "YOURYOU"
        Public Const KOBETSU_NISUGATA_CD As String = "KOBETSU_NISUGATA_CD"
        Public Const NISUGATA_NM As String = "NISUGATA_NM"
        Public Const ITEM_LENGTH As String = "ITEM_LENGTH"
        Public Const ITEM_WIDTH As String = "ITEM_WIDTH"
        Public Const THICKNESS As String = "THICKNESS"
        Public Const CONCENTRATION As String = "CONCENTRATION"
        Public Const PRIOR_DENP_NO As String = "PRIOR_DENP_NO"
        Public Const OUTKA_DENP_NO As String = "OUTKA_DENP_NO"
        Public Const OUTKA_DENP_DTL_NO As String = "OUTKA_DENP_DTL_NO"
        Public Const SEIZO_LOT As String = "SEIZO_LOT"
        Public Const SUURYO As String = "SUURYO"
        Public Const KOSU As String = "KOSU"
        Public Const WH_NM As String = "WH_NM"
        Public Const KIHON_SURYO_TANI As String = "KIHON_SURYO_TANI"
        Public Const KANSAN_SURYO_KG As String = "KANSAN_SURYO_KG"
        Public Const KANSAN_SURYO_TANI_KG As String = "KANSAN_SURYO_TANI_KG"
        Public Const KANSAN_SURYO_LEN As String = "KANSAN_SURYO_LEN"
        Public Const KANSAN_SURYO_TANI_LEN As String = "KANSAN_SURYO_TANI_LEN"
        Public Const SEISEKISYO_HAKKOU_NM As String = "SEISEKISYO_HAKKOU_NM"
        Public Const SHITEI_DENP_NM As String = "SHITEI_DENP_NM"
        Public Const NOUNYUJI_JYOUKEN_NM1 As String = "NOUNYUJI_JYOUKEN_NM1"
        Public Const NOUNYUJI_JYOUKEN_NM2 As String = "NOUNYUJI_JYOUKEN_NM2"
        Public Const NOUNYUJI_JYOUKEN_NM3 As String = "NOUNYUJI_JYOUKEN_NM3"
        Public Const NOUNYUJI_JYOUKEN_NM4 As String = "NOUNYUJI_JYOUKEN_NM4"
        Public Const NOUNYUJI_JYOUKEN_NM5 As String = "NOUNYUJI_JYOUKEN_NM5"
        Public Const NOUNYUJI_JYOUKEN_NM6 As String = "NOUNYUJI_JYOUKEN_NM6"
        Public Const NOUNYUJI_JYOUKEN_NM7 As String = "NOUNYUJI_JYOUKEN_NM7"
        Public Const NOUNYUJI_JYOUKEN_NM8 As String = "NOUNYUJI_JYOUKEN_NM8"
        Public Const NOUNYUJI_JYOUKEN_NM9 As String = "NOUNYUJI_JYOUKEN_NM9"
        Public Const NOUNYUJI_JYOUKEN_NM10 As String = "NOUNYUJI_JYOUKEN_NM10"
        Public Const NOUNYUJI_JYOUKEN_BIKOU As String = "NOUNYUJI_JYOUKEN_BIKOU"
        Public Const YUSO_COMP_NM As String = "YUSO_COMP_NM"
        Public Const BIN_KBN_NM As String = "BIN_KBN_NM"
        Public Const SOTO_BIKOU As String = "SOTO_BIKOU"
        Public Const UCHI_BIKOU As String = "UCHI_BIKOU"
        Public Const OKURIJYO_HIHYOJI_KBN As String = "OKURIJYO_HIHYOJI_KBN"
        Public Const SHIP_NM As String = "SHIP_NM"
        Public Const SHIP_AD As String = "SHIP_AD"
        Public Const SHIP_TEL As String = "SHIP_TEL"
        Public Const IS_REPRINT As String = "IS_REPRINT"
        Public Const PAGE_NO As String = "PAGE_NO"


    End Class

    'DSP五協フード＆ケミカル株式会社でEDIにおいてCSVファイルを読み込んだ場合は"20"、EXCELファイルを読み込んだ場合は"21"とする
    Public Class IN_OUT_KBN_BY_FILE
        Public Const IN_OUT_CSV As String = "20"
        Public Const IN_OUT_EXCEL As String = "21"
    End Class

#End If


    'イベント種別
    Public Enum EventShubetsu As Integer

        SAVEOUTKA = 1           '出荷登録(=1)
        CREATEJISSEKI           '実績作成(=2)
        HIMODUKE                '紐付け(=3)
        EDITORIKESI             'EDI取消(=4)
        TORIKOMI                '取込(=5)
        SAVEUNSO                '運送登録(=6)
        TORIKESIJISSEKI         '実績取消(=7)
        KENSAKU                 '検索(=8)
        IKKATUHENKO             '一括変更(=9)
        OUTPUTPRINT             '出力(=10)
        SELPRINT                '検索条件印刷(=11)
        EXE                     '実行(=12)
        TORIKESI_MITOUROKU      'EDI取消⇒未登録(=13)
        SAKUSEIZUMI_JISSEKIMI   '実績作成済⇒実績未(=14)
        SOUSINZUMI_SOUSINMACHI  '実績送信済⇒送信待(=15)
        SOUSINZUMI_JISSEKIMI    '実績送信済⇒実績未(=16)
        SAKURA_TUIKAJIKKOU      'サクラ追加実行(=17)
        TOUROKUZUMI_MITOUROKU   '出荷登録済⇒未登録(=18)
        MASTER                  'マスタ参照(=19)
        ENTER                   'Enter(=20)
        DEF_CUST                '初期荷主変更(=21)
        CLOSE                   '閉じる(=22)
        DOUBLE_CLICK            'ダブルクリック(=23)
        UNSOTORIKESI_MITOUROKU  '運送取消⇒未登録(=24)  '2012.04.04 大阪対応 ADD
        CUST_CD_SETUP           '荷主コード設定(=25)  '2012.06.20 埼玉対応 ADD

    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN
        '▼▼▼要望番号:467
        'GRPSTATUS = 0
        'CMBEIGYO
        'CMBWARE
        'TXTCUSTCD_L
        'TXTCUSTCD_M
        'TANTOUCD
        'TODOKESAKICD
        ''IMDEDIDATE
        'EDIDATEFROM
        'EDIDATETO
        'CMBSELECTDATE
        'SEARCHDATEFROM
        'SEARCHDATETO
        'PNLEDIT
        'IKKATUCHANGEKBN
        'TXTEDITMAIN
        'TXTEDITSUB
        'CMBEDIT
        'BTNIKKATUCHANGE
        'CMBPRINT
        'BTNPRINT
        'CMBREPRINT
        'BTNREPRINT
        'CMBEXE
        'BTNEXE
        'SPREDILIST
        GRPSTATUS = 0
        CMBEIGYO
        CMBWARE
        TXTCUSTCD_L
        TXTCUSTCD_M
        TANTOUCD
        TODOKESAKICD
        EDIDATEFROM
        EDIDATETO
        CMBSELECTDATE
        SEARCHDATEFROM
        SEARCHDATETO
        PNLOUTPUT
        TXTBARCD            'ADD 2017/06/20
        CMBOUTPUT
        CMBOUTPUTCUST
        TXTPRTCUSTCD_L
        TXTPRTCUSTCD_M
        CMBOUTPUTKB
        CMBAKAKUROKB
        CMBOUTPUTDATEFROM
        CMBOUTPUTDATETO
        BTNOUTPUT
        PNLEDIT
        IKKATUCHANGEKBN
        TXTEDITMAIN
        TXTEDITSUB
        CMBEDIT
        CMBEDIT2
        TXTEDITDESTCD               'ADD 2018/02/28 一括更新　届先CD追加
        BTNIKKATUCHANGE
        CMBEXE
        BTNEXE
        CMBPRINT
        BTNPRINT
        SPREDILIST
        '▲▲▲要望番号:467
    End Enum

    'タブインデックス用列挙体(進捗)
    Public Enum CtlTabIndex_chkSTA

        CHKMITOUROKU = 0
        CHKSTATOUROKUZUMI
        CHKSTAJISSEKIMI
        CHKSTAJISSEKISAKUSEI
        CHKSTAJISSEKISOUSIN
        CHKSTAREDDATA
        CHKSTAALL
        CHKSTATORIKESI

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0
        STATUS_KBN
        HORYU_KBN
        ORDER_NO
        STATUS_NM
        OUTKO_DATE
        OUTKA_PLAN_DATE
        ARR_PLAN_DATE
        'CUST_NM
        DEST_NM
        REMARK
        UNSO_ATT
        BUYER_ORDER_NO              '2012.04.09 要望番号923 表示順の変更
        GOODS_NM
        OUTKA_CNT
        DEST_AD
        UNSO_CORP
        BIN
        MDL_REC_CNT
        EDI_NO
        MATOME_NO
        KANRI_NO
        UNSO_NO_L                   '2012.03.25 表示順の変更
        TRIP_NO                     '2012.11.11 運行番号の追加
        'BUYER_ORDER_NO
        OUTKA_SHUBETSU
        OUTKA_SU
        PICK_KB
        UNSOMOTO_KBN
        PRTFLG                      '要望番号:1786 terakara 2013.01.23 PRTFLG追加
        EDI_IMP_DATE
        EDI_IMP_TIME
        EDI_FILE_NAME               '2013.03.01 / Notes1909受信ファイル名追加
        EDI_SEND_DATE
        EDI_SEND_TIME
        NRS_BR_NM
        NRS_WH_NM
        CUST_NM                     '2012.04.09 要望番号923 表示順の変更
        TANTO_USER_NM
        SYS_ENT_USER_NM
        SYS_UPD_USER_NM
        SYS_UPD_DATE
        SYS_UPD_TIME
        NRS_BR_CD
        NRS_WH_CD
        CUST_CD_L
        CUST_CD_M
        GOODS_CD_NRS
        DEST_CD
        OUTKA_STATE_KB
        UNSO_CD
        UNSO_BR_CD
        'UNSO_NO_L
        UNSO_SYS_UPD_DATE
        UNSO_SYS_UPD_TIME
        MIN_NB
        EDI_DEL_KB
        OUTKA_DEL_KB
        UNSO_DEL_KB
        FREE_C01
        FREE_C02
        FREE_C03
        FREE_C04
        FREE_C30
        AKAKURO_FLG
        EDI_CUST_JISSEKI
        EDI_CUST_MATOMEF
        EDI_CUST_DELDISP
        EDI_CUST_SPECIAL
        EDI_CUST_HOLDOUT
        EDI_CUST_UNSOFLG
        EDI_CUST_INDEX
        SND_SYS_UPD_DATE
        SND_SYS_UPD_TIME
        RCV_SYS_UPD_DATE
        RCV_SYS_UPD_TIME
        OUTKA_SYS_UPD_DATE
        OUTKA_SYS_UPD_TIME
        JISSEKI_FLAG
        OUT_FLAG
        AUTO_MATOME_FLG
        SYS_DEL_FLG
        ORDER_CHECK_FLG
        '▼▼▼二次
        RCV_NM_HED
        RCV_NM_DTL
        RCV_NM_EXT
        SND_NM
        EDI_CUST_INOUTFLG       '2012.02.25 大阪対応 ADD
        '▲▲▲二次
        FLAG_17                 '25014/03/31 セミ標準対応
        FREE_C05
        FREE_C07
        FREE_C08

        HAISO_SIJI_NO           'ADD 2017/06/16 DIC配送指示No.
        FLAG_19
        UNSOEDI_EXISTS_FLAG
        NCGO_OPEOUT_ONLY_FLG    'Add 2018/10/31 要望番号002808
        LAST

    End Enum

    '印刷種別用列挙体(印刷コンボ)
    Public Enum Print_KBN
        DEF = 0
        EDIOUTKACHECKLIST
        EDIOUTKATORIKESILIST
    End Enum

End Class
