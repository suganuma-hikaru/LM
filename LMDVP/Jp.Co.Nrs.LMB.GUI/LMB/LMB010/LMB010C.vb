' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB010    : 入荷データ検索
'  作  成  者       :  [金ヘスル]
' ==========================================================================

''' <summary>
''' LMB010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB010IN"
    Public Const TABLE_NM_OUT As String = "LMB010OUT"
    'START YANAI メモ②No.28
    Public Const TABLE_NM_IN_CHK As String = "LMB010IN_CHK"
    Public Const TABLE_NM_OUT_EDI_L1 As String = "LMB010OUT_EDI_L1"
    Public Const TABLE_NM_OUT_EDI_L2 As String = "LMB010OUT_EDI_L2"
    Public Const TABLE_NM_OUT_INKAS As String = "LMB010OUT_INKA_S"
    Public Const TABLE_NM_IN_EDI As String = "LMB010IN_EDI"
    'EMD YANAI メモ②No.28
    'START YANAI 20120121 作業一括処理対応
    Public Const TABLE_NM_IN_SAGYO As String = "LMB010IN_SAGYO"
    Public Const TABLE_NM_OUT_SAGYO As String = "LMB010OUT_SAGYO"
    'END YANAI 20120121 作業一括処理対応
    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    Public Const TABLE_NM_IN_INKAL As String = "LMB010IN_INKA_L"
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    Public Const TABLE_NM_IN_OUTKA As String = "LMB010IN_OUTKA"
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'ST CALT対応（入荷:テーブル名コンスト） Ri
    Public Const TABLE_NM_IN_INKA_PLAN_SEND As String = "LMB010IN_INKA_PLAN_SEND"
    Public Const TABLE_NM_OUT_INKA_PLAN_SEND As String = "LMB010OUT_INKA_PLAN_SEND"
    'ED CALT対応（入荷:テーブル名コンスト） Ri

    Public Const TABLE_NM_IN_PRINT_RFID As String = "LMB010IN_PRINT_RFID"
    Public Const TABLE_NM_OUT_PRINT_RFID As String = "LMB010OUT_PRINT_RFID"



    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    'LMB020ステータス定数
    Public Const LMB020_STA_REF As String = "0"        '参照
    Public Const LMB020_STA_SINKI As String = "1"      '新規
    Public Const LMB020_STA_COPY As String = "2"       '複写  

    '進捗区分
    Public Const INKASTATEKB_10 As String = "10"
    Public Const INKASTATEKB_40 As String = "40"
    Public Const INKASTATEKB_50 As String = "50"
    Public Const INKASTATEKB_90 As String = "90"

    '2015.10.20 tusnehira add
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

    ''' <summary>
    ''' 複数更新可能件数(実行)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IKKATU_JIKKO As String = "06"

    'イベント種別
    Public Enum EventShubetsu As Integer

        SINKI = 0       '新規
        KANRYO          '完了
        KENSAKU         '検索
        MASTER          'マスタ参照
        ENTER           'Enter
        DEF_CUST        '初期荷主変更
        CLOSE           '閉じる
        DOUBLE_CLICK    'ダブルクリック
        'START YANAI メモ②No.28
        JIKKOU          '実行
        'EMD YANAI メモ②No.28
        'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
        PRINT            '印刷
        'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        GRPSTATUS = 0
        CMBEIGYO
        CMBSOKO
        TXTCUSTCD
        IMDNYUKADATE_FROM
        IMDNYUKADATE_TO
        GRPUNSO
        GRPINKANOL
        SPRDETAIL

    End Enum

    'タブインデックス用列挙体(進捗)
    Public Enum CtlTabIndex_chkSTA

        CHKSTAYOTEI = 0
        CHKSTAPRINT
        CHKSTAUKETSUKE
        CHKSTAKENPIN
        CHKSTANYUKA
        CHKSTAHOUKOKU

    End Enum

    'タブインデックス用列挙体(運送)
    Public Enum CtlTabIndex_chkUNSO

        CHKTRANUNSO = 0
        CHKTRANYOKO
        CHKTRANALL

    End Enum

    'タブインデックス用列挙体(運送)
    Public Enum CtlTabIndex_grpINKANOL

        TXTINKANOL = 0

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0
        OUTKA_FROM_ORD_NO_L
        STATUS_NM        
        INKA_DATE
        WH_WORK_STATUS_NM
        WH_TAB_STATUS_NM
        WH_TAB_WORK_STATUS_KB
        WH_TAB_WORK_STATUS_NM
        CUST_NM
        'START YANAI 要望番号748
        CUST_CD_S
        'END YANAI 要望番号748        
        GOODS_NM
        INKA_TTL_NB
        WT
        REMARK
        REC_CNT
        DEST_NM
        UNCHIN_KB
        UNSOCO_NM
        WEB_INKA_NO_L
        INKA_NO_L
        BUYER_ORD_NO_L
        INKA_TP
        INKA_KB
        'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        LOT_NO
        SERIAL_NO
        'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
        WH_NM
        NRS_BR_CD
        NRS_BR_NM
        TANTO_USER
        SYS_ENT_USER
        SYS_UPD_USER
        SYS_UPD_DATE
        SYS_UPD_TIME
        CUST_CD_L
        CUST_CD_M
        INKA_STATE_KB
        WH_CD
        OUTKA_FROM_ORD_NO_M
        INKA_TP_CD
        'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
        REC_S_CNT
        PIC        
        LAST
        'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

    End Enum

End Class
