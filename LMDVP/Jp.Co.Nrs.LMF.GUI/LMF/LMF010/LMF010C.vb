' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃
'  プログラムID     :  LMF010C : 運行・運送情報
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMF010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 運送(大)データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 保存(登録)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SAVE As String = "SaveAction"

    ''' <summary>
    ''' 保存(解除)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_REMOVED As String = "RemovedAction"

    '2012.06.22 要望番号1189 追加START
    ''' <summary>
    ''' 印刷アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT_ONLY As String = "DoPrint"
    '2012.06.22 要望番号1189 追加END

    ''' <summary>
    ''' 排他チェックアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_HAITA_DATA As String = "ChkHaitaData"

    ''' <summary>
    ''' 運行レコード検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_TRIP As String = "SelectLLCountData"

    ''' <summary>
    ''' コンボデータ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_CMB As String = "SelectCombData"

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- START ---
    ''' <summary>
    ''' 車載受注渡しアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_SYASAI_EATASI As String = "SyasaiWatashi"
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

    ''' <summary>
    ''' 対象運行データキャンセルチェックアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_CANCEL_DATA As String = "ChkCancelData"

    ''' <summary>
    ''' 修正項目(運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_TRIP As String = "01"

    ''' <summary>
    ''' 修正項目(便区分)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_BIN As String = "02"

    ''' <summary>
    ''' 修正項目(運送会社)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_UNSOCO As String = "03"

    ''' <summary>
    ''' 修正項目(中継配送)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_CHUKEI As String = "04"

    ''' <summary>
    ''' 一括変更可能件数(LMF010)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_LMF010 As String = "03"

    ''' <summary>
    ''' データセットテーブル名(INテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMF010IN"

    ''' <summary>
    ''' データセットテーブル名(OUTテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMF010OUT"

    ''' <summary>
    ''' データセットテーブル名(ITEMテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_ITEM As String = "ITEM"

    ''' <summary>
    ''' データセットテーブル名(UNSO_Lテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNSO_L As String = "UNSO_L"

    ''' <summary>
    ''' データセットテーブル名(ERRテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' データセットテーブル名(CMBテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_CMB As String = "CMB"

    'ADD Start 2017/02/27
    ''' <summary>
    ''' データセットテーブル名(SYS_DATETIMEテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_UPDATE As String = "LMF010IN_UPDATE"
    Public Const TABLE_NM_IN_PRINT As String = "LMF010IN_UNSO_L"
    'ADD End 2017/02/27

    '2014.07.01 追加START
    Public Const TABLE_NM_IN_CSV As String = "LMF010IN_CSV"
    Public Const MOTO_DATA_KB_UNSO As String = "運送"
    '2014.07.01 追加END

    '2016.01.06 UMANO 英語化対応START
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2016.01.06 UMANO 英語化対応END

    Public Const TABLE_NM_OKURIJYO_WK As String = "LMF010_OKURIJYO_WK"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        BETSUEIGYO
        UNSOCO_CD1
        UNSOCO_BR_CD1
        UNSOCO_NM1
        UNSOCO_CD2
        UNSOCO_BR_CD2
        UNSOCO_NM2
        CUST_CDL
        CUST_CDM
        CUST_NM
        JSHA_KB
        DATE_KB
        TRIPDATE_FROM
        TRIPDATE_TO
        CNTUSER_CD
        CNTUSER_NM
        TRIP
        TRIP_N
        TRIP_Y
        TRIP_ALL
        TYUKEI
        CHUKEI_N
        CHUKEI_Y
        CHUKEI_ALL
        PNL_UNKO
        HAISO
        BTN_UNKO
        'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
        VISIBLEKB
        'END YANAI 要望番号737 運送検索画面：全体が見えるようにする
        EDIT
        PNL_EVENT
        EVENT_Y
        EVENT_N
        SHUSEI
        HENKO
        TRIP_NO
        BIN_KB
        UNSOCO_CD0
        UNSOCO_BR_CD0
        UNSOCO_NM0
        CHUKEI_FROM
        CHUKEI_TO
        'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
        PNL_FUKUSHA
        ORIG_DATE
        DEST_DATE
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する
        PRINT_KB                '2012.06.22 要望番号1189 追加
        BTN_PRINT               '2012.06.22 要望番号1189 追加
        SPR

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UNSO_NO_L
        BIN_KB
        BUNRUI
        TARIFF_BUNRUI_KB
        UNSOCO_CD_2
        UNSOCO_BR_CD_2
        UNSOCO_NM_2
        UNSOCO_BR_NM_2
        UNSOCO_2
        CUST_REF_NO
        ORIG_CD
        ORIG_NM
        DEST_CD
        DEST_NM
        DEST_AD
        TASYA_WH_NM
        AREA
        UNSO_NB
        WT
        SHOMI_WT
        INOUTKA_NO_L
        WH_CD                       'ADD 2019/08/05 005193
        OUTKA_PLAN_DATE
        ARR_PLAN_DATE
        TRIP_NO
        DRIVER
        TRIP_DATE
        VCLE_KB
        CAR_NO
        UNSOCO_CD_1
        UNSOCO_BR_CD_1
        UNSOCO_1
        UNSOCO_NM_1
        UNSOCO_BR_NM_1
        CUST_NM
        CUST_NM_L
        CUST_NM_M
        UNSO_REM
        UNCHIN
        SHIHARAI_UNCHIN             'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        SHIHARAI_FIXED_FLAG         'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        KYORI
        DENP_NO
        GROUP_NO
        UNSO_ONDO_KB
        MOTO_DATA
        MOTO_DATA_KB
        SHUKA_RELY_POINT
        HAIKA_RELY_POINT
        TRIP_NO_SHUKA
        TRIP_NO_CHUKEI
        TRIP_NO_HAIKA
        UNSOCO_SHUKA
        UNSOCO_NM_SHUKA
        UNSOCO_BR_NM_SHUKA
        UNSOCO_CHUKEI
        UNSOCO_NM_CHUKEI
        UNSOCO_BR_NM_CHUKEI
        UNSOCO_HAIKA
        UNSOCO_NM_HAIKA
        UNSOCO_BR_NM_HAIKA
        TYUKEI_FLG
        UNSO_TEHAI_KB
        DEST_ADD2
        ALCTD_STS
        CNT_USER
        CNT_DATE
        NRS_BR_CD
        SYS_UPD_DATE
        SYS_UPD_TIME
        'TEHAI_JYOKYO                'ADD 2013.10.22 要望番号2063
        'TEHAI_SYUBETSU              'ADD 2013.10.22 要望番号2063
#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
        AUTO_DENP_KBN
        AUTO_DENP_NO
#End If
        BIN_KB_UNSO_LL  'ADD 2018/12/19 要望管理000880
        '2022.08.22 追加START
        PF_SOSHIN
        PF_SOSHIN_NM
        '2022.08.22 追加END
        KAKUTEI
        INDEX_COUNT ' added 20160705 inoue SprColumnIndexの定義数

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        UNCO_NEW = 0
        UNCO_EDIT
        UNSO_NEW
        KENSAKU
        MASTEROPEN
        ENTER
        DOUBLECLICK
        CLOSE
        SAVE
        BTN_UNCO_EDIT
        BTN_UNSO_PRINT                  '2012.06.22 要望番号1189 追加
        BTN_UNSOCO_PRINT                '2017/02/27 運送会社帳票印刷 追加
        'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
        UNSO_COPY
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する
        '(2012.08.13) 要望番号1341 車載受注渡し対応 --- STRAT ---
        SYASAI_WATASHI
        '(2012.08.13) 要望番号1341 車載受注渡し対応 ---  END  ---
        '(2013.01.18) 要望番号1617 出荷編集遷移対応 --- STRAT ---
        OUTKA_EDIT
        '(2013.01.18) 要望番号1617 出荷編集遷移対応 ---  END  ---

    End Enum

    ''' <summary>
    ''' 運送会社タイプ
    ''' </summary>
    ''' <remarks>
    ''' 0：編集部の運送会社
    ''' 1：1次の運送会社
    ''' 2：2次の運送会社
    ''' </remarks>
    Public Enum UNSOTYPE As Integer
        UNSOTYPE_0 = 0
        UNSOTYPE_1
        UNSOTYPE_2
    End Enum

    ''' <summary>
    ''' 運送会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNSOCO_CD As String = "txtUnsocoCd"

    ''' <summary>
    ''' 運送会社支店コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNSOCO_BR As String = "txtUnsocoBrCd"

    ''' <summary>
    ''' 運送会社名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNSOCO_NM As String = "lblUnsocoNm"

    ''' <summary>
    ''' 集荷
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUKA As String = "集荷"

    ''' <summary>
    ''' 中継
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYUKEI As String = "中継"

    ''' <summary>
    ''' 配荷
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HAIKA As String = "配荷"

    ''' <summary>
    ''' あり
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ARI As String = "あり"

    ''' <summary>
    ''' なし
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NASHI As String = "なし"

    ''' <summary>
    ''' 配送区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HAISO_KBN As String = "配送区分"

    ''' <summary>
    ''' 中継配送未設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYUKEI_MI As String = "中継配送未設定"

    ''' <summary>
    ''' 運行番号未設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TRIP_MI As String = "運行番号未設定"

    ''' <summary>
    ''' 印刷種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_KB As String = "印刷種別"


    ''' <summary>
    ''' 運送会社帳票印刷区分(U033)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TrapoPrintSelectValues

        ''' <summary>
        ''' 名鉄・帳票作成(バラ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CREATE_MEITESU_CSV_WITHOUT_GROUPING As String = "01"

    End Class
End Class
