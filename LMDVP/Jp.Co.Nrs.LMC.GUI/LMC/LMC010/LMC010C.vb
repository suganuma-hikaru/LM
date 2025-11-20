' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMM     : マスタメンテナンス
'  プログラムID     :  LMC010C : 会社マスタ
'  作  成  者       :  [iwamoto]
' ==========================================================================

''' <summary>
''' LMC010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMC010IN"
    Public Const TABLE_NM_OUT As String = "LMC010OUT"
    Public Const TABLE_NM_IN_UPDATE As String = "LMC010IN_UPDATE"
    Public Const TABLE_NM_IN_PRINT As String = "LMC010IN_OUTKA_L"
    Public Const TABLE_NM_OUTKA_M_IN As String = "LMC010_OUTKA_M_IN"
    Public Const TABLE_NM_OUTKA_M_OUT As String = "LMC010_OUTKA_M_OUT"
    Public Const TABLE_NM_OUTKA_M_BUNSEKI_IN As String = "LMC010_OUTKA_M_BUNSEKI_IN"
    Public Const TABLE_NM_OUTKA_M_BUNSEKI_OUT As String = "LMC010_OUTKA_M_BUNSEKI_OUT"
    Public Const TABLE_NM_NOUHINSYO_IN As String = "LMC010_NOUHINSYO_IN"
    'START YANAI 20110914 一括引当対応
    Public Const TABLE_NM_OUTKA_L_IN As String = "LMC010_OUTKA_L_IN"
    'END YANAI 20110914 一括引当対応
    '要望番号:1533 terakawa 2012.10.30 Start
    Public Const TABLE_NM_HAITA As String = "LMC010_HAITA"
    '要望番号:1533 terakawa 2012.10.30 End

    '社内入荷データ作成対応 terakawa 2012.11.19 Start
    Public Const TABLE_NM_INT_EDI As String = "LMC010_INT_EDI"
    Public Const TABLE_NM_INT_EDI_IN As String = "LMC010_INT_EDI_IN"
    Public Const TABLE_NM_INKA_IN As String = "LMC010_INKA_IN"
    '社内入荷データ作成対応 terakawa 2012.11.19 End

    Public Const TABLE_NM_OUTKA_S As String = "LMC010OUT_OUTKA_S"
    Public Const TABLE_NM_OUTKA_M As String = "LMC010OUT_OUTKA_M"
    Public Const TABLE_NM_ZAI_TRS As String = "LMC010OUT_ZAI"
    'START YANAI 20110914 一括引当対応
    Public Const TABLE_NM_PRINT_CUST As String = "LMC010PRINT_CUST"
    'END YANAI 20110914 一括引当対応
    'START YANAI 要望番号585
    Public Const TABLE_NM_UNSO_L As String = "LMC010OUT_UNSO_L"
    Public Const TABLE_NM_JURYO_UNSO_L As String = "LMC010OUT_JURYO_UNSO_L"
    Public Const TABLE_NM_JURYO_OUTKA_M As String = "LMC010OUT_JURYO_OUTKA_M"
    Public Const TABLE_NM_JURYO_OUTKA_S As String = "LMC010OUT_JURYO_OUTKA_S"
    'END YANAI 要望番号585
    'START YANAI 要望番号638
    Public Const TABLE_NM_UNCHIN As String = "LMC010OUT_UNCHIN"
    'END YANAI 要望番号638
    'START YANAI 要望番号627　こぐまくん対応
    Public Const TABLE_NM_IN_CSV As String = "LMC010IN_CSV"
    Public Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"
    'END YANAI 要望番号627　こぐまくん対応
    'START YANAI 要望番号773
    Public Const TABLE_NM_IN_HOUKOKU_EXCEL As String = "LMC010IN_HOUKOKU_EXCEL"
    Public Const TABLE_NM_OUT_HOUKOKU_EXCEL As String = "LMC010OUT_HOUKOKU_EXCEL"
    'END YANAI 要望番号773
    'START YANAI 要望番号853 まとめ処理対応
    Public Const TABLE_NM_IN_MATOME As String = "LMC010_ZAI_MATOME_IN"
    Public Const TABLE_NM_OUT_MATOME As String = "LMC010_ZAI_MATOME_OUT"
    'END YANAI 要望番号853 まとめ処理対応

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    Public Const TABLE_NM_SHIHARAI As String = "LMC010OUT_SHIHARAI"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    'START YANAI 引当エラーは音声CSV出力しない
    Public Const TABLE_NM_ERR As String = "LMC010_ERR_ROWNO"
    'END YANAI 引当エラーは音声CSV出力しない

    'START 中村 シグマ出荷対応
    Public Const TABLE_SIGMA_IN_CSV As String = "LMC010IN_SIGMA"
    Public Const TABLE_SIGMA_SYS_DATETIME As String = "SYS_DATETIME"
    'END  中村 シグマ出荷対応

    '2014.04.21 CALT対応 黎 --ST--
    Public Const TABLE_NM_LMC100IN_OUTKA_DIRECT_SEND As String = "LMC010IN_OUTKA_DIRECT_SEND"
    Public Const TABLE_NM_LMC100OUT_OUTKA_DIRECT_SEND As String = "LMC010OUT_OUTKA_DIRECT_SEND"
    '2014.04.21 CALT対応 黎 --ED--

    'ADD 要望番号:2580 佐川・ヤマト送状番号取り込み
    Public Const TABLE_NM_DENP_NO As String = "LMC010IN_OUTKA_DENP_NO"
    'END 要望番号:2580 佐川・ヤマト送状番号取り込み

    'ADD 要望番号:2720 トール状番号取り込み 2017/10/11
    Public Const TABLE_NM_OKURIJYO_WK As String = "LMC010_OKURIJYO_WK"

    '2018/01/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    Public Const TABLE_NM_PRE_NO_OF_INVOICE_NO As String = "LMC010_PRE_NO_OF_INVOICE_NO"
    '2018/01/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start

    Public Const TABLE_NM_MEIHEN_INKA_IN As String = "LMC010_MEIHEN_INKA_IN"
    Public Const TABLE_NM_SBS_SAIHOKAN As String = "LMC010_SBS_SAIHOKAN"

    '印刷種別(出荷報告)
    Public Const PRINT_HOKOKU As String = "07"

    '進捗区分
    Public Const SINTYOKU10 As String = "10" '予定入力済
    Public Const SINTYOKU30 As String = "30" '指図書印刷
    Public Const SINTYOKU40 As String = "40" '出庫済
    Public Const SINTYOKU50 As String = "50" '検品済
    Public Const SINTYOKU60 As String = "60" '出荷済
    Public Const SINTYOKU90 As String = "90" '報告済

    '社内入荷データ作成対応 terakawa Start
    '出荷実行区分
    Public Const SHANAI_INKA_MAKE As String = "09" '社内入荷データ作成
    '社内入荷データ作成対応 terakawa End

    'LMC020ステータス定数
    Public Const LMC020_STA_REF As String = "0"        '参照
    Public Const LMC020_STA_SINKI As String = "1"      '新規
    Public Const LMC020_STA_COPY As String = "2"       '複写 

    'ガイダンス区分(00)
    Public Const GUIDANCE_KBN As String = "00"

    '要望番号:1793 terakawa 2013.01.21 Start
    'まとめピック区分
    Public Const PICK_KB_MATOMENASHI As String = "01"
    '要望番号:1793 terakawa 2013.01.21 End

    ''' <summary>
    ''' 複数更新可能件数(一括変更)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_HENKO As String = "01"

    ''' <summary>
    ''' 複数更新可能件数(実行)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_JIKKO As String = "06"

    ''' <summary>
    ''' 複数更新可能件数(完了)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_KANRYO As String = "07"

    'START YANAI 要望番号627　こぐまくん対応
    Public Enum SysData As Integer
        YYYYMMDD = 0
        HHMMSSsss
    End Enum
    'END YANAI 要望番号627　こぐまくん対応

    'START YANAI 要望番号808
    '出荷報告・EXCELタイトル列名
    Public Const HOUKOKU_COL As Integer = 11
    Public Const HOUKOKU_COL_01 As String = "オーダー番号"
    Public Const HOUKOKU_COL_02 As String = "オーダー番号(明細)"
    Public Const HOUKOKU_COL_03 As String = "出荷予定日"
    Public Const HOUKOKU_COL_04 As String = "届先コード"
    Public Const HOUKOKU_COL_05 As String = "商品コード"
    Public Const HOUKOKU_COL_06 As String = "商品名称"
    Public Const HOUKOKU_COL_07 As String = "ロット番号"
    Public Const HOUKOKU_COL_08 As String = "出荷数"
    Public Const HOUKOKU_COL_09 As String = "送状番号"
    Public Const HOUKOKU_COL_10 As String = "届先名称 住所"
    Public Const HOUKOKU_COL_11 As String = "運送会社名"
    'END YANAI 要望番号808

    Public Const HOUKOKU_COL_ENG As Integer = 11
    Public Const HOUKOKU_COL_01_ENG As String = "Order No"
    Public Const HOUKOKU_COL_02_ENG As String = "Order No (specification)"
    Public Const HOUKOKU_COL_03_ENG As String = "Shipping time"
    Public Const HOUKOKU_COL_04_ENG As String = "Destination code"
    Public Const HOUKOKU_COL_05_ENG As String = "Goods Code"
    Public Const HOUKOKU_COL_06_ENG As String = "Goods Name"
    Public Const HOUKOKU_COL_07_ENG As String = "Lot No"
    Public Const HOUKOKU_COL_08_ENG As String = "Shipments"
    Public Const HOUKOKU_COL_09_ENG As String = "Invoice number"
    Public Const HOUKOKU_COL_10_ENG As String = "Name And Address"
    Public Const HOUKOKU_COL_11_ENG As String = "Transportation company name"

    '2017/09/25 追加 李↓
    Public Const HOUKOKU_COL_KR As Integer = 11
    Public Const HOUKOKU_COL_01_KR As String = "오더번호"
    Public Const HOUKOKU_COL_02_KR As String = "오더번호 (명세)"
    Public Const HOUKOKU_COL_03_KR As String = "출하예정일"
    Public Const HOUKOKU_COL_04_KR As String = "송달처코드"
    Public Const HOUKOKU_COL_05_KR As String = "상품코드"
    Public Const HOUKOKU_COL_06_KR As String = "상품명칭"
    Public Const HOUKOKU_COL_07_KR As String = "로트번호"
    Public Const HOUKOKU_COL_08_KR As String = "출하수"
    Public Const HOUKOKU_COL_09_KR As String = "송장번호"
    Public Const HOUKOKU_COL_10_KR As String = "송달처명칭 주소"
    Public Const HOUKOKU_COL_11_KR As String = "운송회사명"
    '2017/09/25 追加 李↑

    '2015.10.20 tusnehira add
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"


    'イベント種別
    Public Enum EventShubetsu As Integer

        SINKI = 0       '新規
        KENSAKU         '検索
        MASTER          'マスタ参照
        DEF_CUST        '初期荷主変更
        CLOSE           '閉じる
        HENKO           '変更
        PRINT           'プリント
        JIKKOU          '実行
        KANRYO          '完了
        DOUBLE_CLICK    'ダブルクリック
        'START YANAI 要望番号481
        ENTER           'ENTER
        'END YANAI 要望番号481
        'START YANAI 要望番号917
        CMBPRINTSYUBETUCHENGE
        'END YANAI 要望番号917
        TRAPOPRINT      '運送会社帳票印刷   名鉄対応(2499) 20160323 added inoue
    End Enum

    '印刷種別
    Public Enum PrintShubetsu As Integer

        '01	出荷印刷帳票名（出荷データ検索）	荷札
        '02	出荷印刷帳票名（出荷データ検索）	送状
        '03	出荷印刷帳票名（出荷データ検索）	納品書
        '04	出荷印刷帳票名（出荷データ検索）	分析表
        '05	出荷印刷帳票名（出荷データ検索）	纏めピック
        '06	出荷印刷帳票名（出荷データ検索）	纏め送状
        '07	出荷印刷帳票名（出荷データ検索）	出荷報告
        '08	出荷印刷帳票名（出荷データ検索）	一括

        NIHUDA = 1     '荷札
        DENP           '送状
        NHS            '納品書
        COA            '分析表
        PIG_GRP        '纏めピック
        DENP_GRP       '纏め送状
        HOKOKU         '出荷報告
        ALL_PRINT      '一括
        'START YANAI 20120122 立会書印刷対応
        TACHIAI        '立会書
        'END YANAI 20120122 立会書印刷対応
        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        NIHUDA_GRP     'まとめ荷札
        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        'START s.kobayashi 20130128 ITW荷札
        DENP_GRP_CHK     'まとめ送状（選択）
        ITW_NIHUDA     'ITW荷札
        'End s.kobayashi 20130128 ITW荷札
        PACKAGE_DETAILS
        UNSO_HOKEN          'ADD 2018/07/24 依頼番号 : 001540  
        HOKOKU_DATE         'ADD 2018/10/10 依頼番号 : 002381  
        YELLOW_CARD
        ATTEND
        OUTBOUND_CHECK
        SHIPMENTGUIDE       'ADD 2023/03/29 送品案内書(FFEM)追加
        DOKU_JOJU
    End Enum


    ''' <summary>
    ''' 出荷帳票印刷区分(S034)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class OutkaPrintSelectValues

        ''' <summary>
        ''' 梱包明細
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PACKAGE_DETAILS As String = "20"

#If True Then       'ADD 2018/07/24 依頼番号 : 001540  
        ''' <summary>
        ''' 運送保険
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO_HOKEN As String = "21"

        ''' <summary>
        ''' イエローカード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const YELLOW_CARD As String = "22"

#End If

#If True Then       'ADD 2018/10/10   002381 出荷データ検索_出荷報告(日付毎)印刷_エラーバグ
        ''' <summary>
        ''' 出荷報告(日付毎)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const HOKOKU_DATE As String = "12"

#End If
        ''' <summary>
        ''' 立合書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ATTEND As String = "23"

        ''' <summary>
        ''' 出荷チェックリスト
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTBOUND_CHECK As String = "24"

#If True Then       'ADD 2023/03/29 送品案内書(FFEM)追加
        ''' <summary>
        ''' 送品案内書(FFEM)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SHIPMENTGUIDE As String = "25"
#End If

        ''' <summary>
        ''' 毒劇物譲受書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DOKU_JOJU As String = "26"

    End Class


#If True Then ' 名鉄対応(2499) 20160323 chnaged inoue


    ''' <summary>
    ''' 運送会社帳票印刷区分(U032)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TrapoPrintSelectValues


        ''' <summary>
        ''' 名鉄・帳票作成(バラ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CREATE_MEITESU_CSV_WITHOUT_GROUPING As String = "1"

        ''' <summary>
        ''' 名鉄・帳票作成(まとめ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CREATE_MEITESU_CSV_WITH_GROUPING As String = "2"

        ''' <summary>
        ''' 名鉄・帳票作成(バラ)送状
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CREATE_MEITESU_BARA_OKURIJYO As String = "3"

        ''' <summary>
        ''' 名鉄・帳票作成(バラ)荷札
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CREATE_MEITESU_BARA_NIFUDA As String = "4"

    End Class
#End If

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        GRPSTATUS = 0
        CMBEIGYO
        CMBSOKO
        TXTCUSTCD
        TXTPICCD
        CMBPRINTSYUBETU
        'START YANAI 要望番号917
        'CMBPRINTSTATUS
        'END YANAI 要望番号917
        CMBPICKSYUBETU
        CHKSELECTBYNRSB
        CMBSEARCHDATE
        CMBSEARCHDATE_FROM
        CMBSEARCHDATE_To
        IMDPRINTDATEF
        IMDPRINTDATET
        GRPUNSO
        GRPOUTKANOL
        GRPTRAPOPRINT  ' 名鉄対応(2499) 20160323 added inoue
        SPRDETAIL
    End Enum

    'タブインデックス用列挙体(進捗)
    Public Enum CtlTabIndex_chkSTA

        CHKSTAYOTEI = 0
        CHKSTAPRINT
        CHKSTASHUKKO
        CHKSTAKENPIN
        CHKSTASHUKKA
        CHKSTAHOUKOKU
        CHKSTATORIKESHI

    End Enum

    'タブインデックス用列挙体(grpUNSO)
    Public Enum CtlTabIndex_grpUNSO

        TXTTRNCD = 0
        TXTTRNBRCD
        BTNUNSO
        CMBPRINT
        BTNPRINT
        CMBJIKKOU
        BTNJIKKOU

    End Enum

    'タブインデックス用列挙体(grpUNSO)
    Public Enum CtlTabIndex_grpOUTKANOL

        TXTOUTKANOL = 0

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0
        CUST_ORD_NO
        OUTKA_STATE_KB_NM
        OUTKA_PLAN_DATE
        ARR_PLAN_DATE
#If True Then ' 出荷作業ステータス対応 20160822 added inoue
        WH_WORK_STATUS_NM ' 庫内作業ステータス追加 
#End If
        WH_TAB_STATUS_NM ' 現場作業指示ステータス追加 
        CUST_NM
        'START YANAI 要望番号748
        CUST_CD_S
        'END YANAI 要望番号748
        DEST_NM
        GOODS_NM
        OUTKA_PKG_NB
        OUTKA_TTL_NB
        DEST_AD
        UNSOCO_NM
        '2013.02.27 / Notes1807:便区分追加
        BIN_KB_NM
        '2013.02.27 / Notes1807:便区分追加
        DENP_NO
        M_COUNT
        '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
        BUYER_ORD_NO
        '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add end
        WEB_OUTKA_NO_L
        OUTKA_NO_L
        SYUBETU_KB_NM
        REMARK_UNSO     '要望番号1856 2013/02/21 本明
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
        LOT_NO_S
        SERIAL_NO_S
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
        COA_UMU          'ADD 2019/06/18 004870
        WH_NM
        NRS_BR_NM
        TANTO_USER
        SYS_ENT_USER
        SYS_UPD_USER
        LAST_PRINT_DATE
        LAST_PRINT_TIME
        SYS_UPD_DATE
        SYS_UPD_TIME
        NRS_BR_CD
        WH_CD
        PICK_KB
        OUTKA_SASHIZU_PRT_YN
        OUTOKA_KANRYO_YN
        OUTKA_KENPIN_YN
        CUST_CD_L
        CUST_CD_M
        GOODS_CD_NRS
        DEST_CD
        OUTKA_STATE_KB
        UNSO_CD
        UNSO_BR_CD
        UNSO_NO_L
        UNSO_SYS_UPD_DATE
        UNSO_SYS_UPD_TIME
        LOT_NO
        SEIQ_FIXED_FLAG
        S_COUNT
        BACKLOG_NB
        BACKLOG_QT
        FURI_NO
        NIHUDA_YN
        'START YANAI メモ②No.2
        SASZ_USER
        'END YANAI メモ②No.2
        'START YANAI 20120122 立会書印刷対応
        TACHIAI_FLG
        'END YANAI 20120122 立会書印刷対応
        '(2012.03.08) 再発行フラグ制御追加 LMC513対応 -- START --
        NHS_FLAG
        '(2012.03.08) 再発行フラグ制御追加 LMC513対応 --  END  --
        'START YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう
        MIN_ALCTD_NB
        MIN_ALCTD_QT
        'END YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        TRIP_NO
        TRIP_NO_SYUKA
        TRIP_NO_TYUKEI
        TRIP_NO_HAIKA
        SHIHARAI_FIXED_FLAG
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  
        '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加START
        CUST_DEST_CD
        '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加END

#If True Then ' 西濃自動送り状番号対応 20160704 added inoue
        AUTO_DENP_KBN
        AUTO_DENP_NO
#End If
        SYUBETU_KB
        SHIKAKARI_HIN_FLG
        ZFVYHKKBN
        ZFVYDENTYP
        MATNR

        LAST

    End Enum

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added


    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Functions

        Public Const SelectNeedlessNhsData As String = "SelectNeedlessNhsData"

    End Class


    ''' <summary>
    ''' BLF名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BLF_NAME As String = "LMC010BLF"

    ''' <summary>
    ''' 行色設定定義用のKBN_GROUP_CD
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ROW_COLOR_TYPE_Z_KBN_GRP_CD As String = "R018"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUSTOMIZE_ROW_COLOR_TYPE_COLUMN_NAME As String = "CUSTOMIZE_ROW_COLOR_TYPE"

    ''' <summary>
    ''' 検索結果行カラーカスタマイズ種別
    ''' </summary>
    ''' <remarks></remarks>
    Class CustomizeRowColorType

        ''' <summary>
        ''' FFEM(納品書/COAファイルの添付有無により色変更)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FFEM As String = "01"

    End Class


#End If

    '運送会社一括変更時、運送会社無しの場合、運送（大）への設定値
    Public Const UNSO_TEHAI_KB01 As String = "10"    '日陸手配
    Public Const PC_KB As String = "01"              '元払い
    Public Const TAX_KB01 As String = "01"           '課税

End Class
