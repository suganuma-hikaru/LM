' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH010C : EDI入荷データ検索
'  作  成  者       :  nishikawa
' ==========================================================================

''' <summary>
''' LMH010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH010INOUT"
    Public Const TABLE_NM_OUT As String = "LMH010OUT"
    Public Const TABLE_NM_JUDGE As String = "LMH010_JUDGE"
    Public Const TABLE_NM_INKAEDI_L As String = "LMH010_INKAEDI_L"
    Public Const TABLE_NM_INKAEDI_M As String = "LMH010_INKAEDI_M"
    Public Const TABLE_NM_RCV_HED As String = "LMH010_RCV_HED"
    Public Const TABLE_NM_RCV_DTL As String = "LMH010_RCV_DTL"
    Public Const TABLE_NM_INKA_L As String = "LMH010_B_INKA_L"
    Public Const TABLE_NM_SEND As String = "LMH010_SEND"
    Public Const TABLE_NM_WARNING_HED As String = "WARNING_HED"
    Public Const TABLE_NM_HIMODUKE As String = "LMH010_HIMODUKE"
    Public Const TABLE_NM_GUIERROR As String = "LMH010_GUIERROR"
    '2012.03.13 大阪対応START
    Public Const TABLE_NM_OUTPUTIN As String = "LMH010_OUTPUTIN"    '2012.03.03 大阪対応ADD
    '2012.03.13 大阪対応END
    '2012.09.11 富士フイルム対応START

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '2012.03.13 大阪対応START
    '出力種別
    Public Const JYUSIN_PRT As String = "01"             '受信帳票
    Public Const JYUSIN_ICHIRAN As String = "02"         '受信一覧表 '2012.03.16 ADD
    '2012.03.13 大阪対応END
    '未着・装着ファイル作成対応 Start
    Public Const MISOUTYAKU_FILE_MAKE As String = "03"         '未着・早着ファイル作成 
    '未着・装着ファイル作成対応End
    Public Const IKKATU_PRT As String = "99"             '一括印刷      '要望番号1007 2012.05.11 EDIT

    '要望番号1007 2012.05.11 追加START
    '出力区分
    Public Const OUTPUT_MI As String = "01"              '未出力
    Public Const OUTPUT_SUMI As String = "02"            '出力済

    '要望番号1007 2012.05.11 追加END

    '2015.04.13 追加START
    '取込処理用
    Public Const SEMIEDI_INFO As String = "LMH010_SEMIEDI_INFO"
    Public Const EDI_TORIKOMI_HED As String = "LMH010_EDI_TORIKOMI_HED"
    Public Const EDI_TORIKOMI_DTL As String = "LMH010_EDI_TORIKOMI_DTL"
    '2015.04.13 追加END


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks>
    ''' ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
    ''' この定義はサーバー側(LMH010DAC.vb)と定義を一致させる必要があります。
    ''' また、既存の要素の途中に新しく挿入すると、動かなく機能(BLF,BLC)があります。
    ''' ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
    ''' </remarks>
    Public Enum EventShubetsu As Integer

        TOROKU = 1                  '登録
        JISSEKI_SAKUSE              '実績作成
        HIMODUKE                    '紐付
        EDI_TORIKESI                'EDI取消
        TORIKOMI                    '取込
        JISSEKI_TORIKESI            '実績取消
        KENSAKU                     '検索
        MASTER                      'マスタ参照
        DEF_CUST                    '初期荷主変更
        CLOSE                       '閉じる
        DOUBLE_CLICK                'ダブルクリック
        JIKKOU_TORIKESI_MITOUROKU   '実行(EDI取消⇒未登録)12
        JIKKOU_HOUKOKU_EDI_TORIKESI '実行(実績報告用EDIデータ取消)13
        JIKKOU_SAKUSEI_JISSEKIMI    '実行(実績作成済⇒実績未)14
        JIKKOU_SOUSIN_SOUSINMI      '実行(実績送信済⇒送信待)15
        JIKKOU_SOUSIN_JISSEKIMI     '実行(実績送信済⇒実績未)16
        JIKKOU_TOUROKU_MITOUROKU
        ENTER                       'Enterキー押下 2011/12/01 篠原 追加(要望番号513)
        PRINT                       '印刷処理
        OUTPUTPRINT                 'CSV作成・出力
        COA_TOUROKU                 '分析表取り込み
        INKA_CONF_TORIKOMI          '入荷確認ファイル取込   '2012.11.30 追加
        CONF_DEL                    '確認データ削除         '2012.11.30 追加
        BULK_CUST_CHANGE            '荷主一括変更         　'2015.09.03 tsunehira add
        JIKKOU_TRANSFER_COND_M      'M品振替
        JIKKOU_GENPIN_PRINT         '実行(現品票印刷)       'ADD 2019/9/12 依頼番号:007111
        JIKKOU_GENPIN_REPRINT       '実行(現品票再印刷)     'ADD 2019/9/12 依頼番号:007111

    End Enum


    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0                     '選択列
        JOTAI
        HORYU_KBN
        ORDER_NO
        STATUS_NM
        INKA_DATE
        OUTKA_CTL_NO_L_COND_M
        CUST_NM
        ITEM_NM
        INKA_NB
        INKA_TTL_NB
        MDL_REC_CNT
        UNSOMOTO_KBN
        UNSO_CORP
        OUTKA_MOTO_NM               '2013.04.03 Notes1995
        EDI_NO
        MATOME_NO                   '2012.02.25 大阪対応ADD
        KANRI_NO
        BUYER_ORDER_NO
        INKA_SHUBETSU
        EDI_IMP_DATE
        EDI_IMP_TIME
        EDI_SEND_DATE
        EDI_SEND_TIME
        TANTO_USER_NM
        SYS_ENT_USER_NM
        SYS_UPD_USER_NM
        AKAKURO_FLG
        DEL_KB
        OUT_FLAG
        EDI_CUST_JISSEKI
        EDI_CUST_MATOME
        EDI_CUST_SPECIAL
        EDI_CUST_HOLD
        EDI_CUST_UNSO
        EDI_CUST_INDEX
        NRS_BR_CD
        WH_CD
        SYS_UPD_DATE
        SYS_UPD_TIME
        SND_UPD_DATE
        SND_UPD_TIME
        RCV_UPD_DATE
        RCV_UPD_TIME
        INKA_UPD_DATE
        INKA_UPD_TIME
        INKA_STATE_KB
        SYS_DEL_FLG
        INKA_DEL_FLG
        JISSEKI_FLAG
        FREE_C30
        CUST_CD_L
        CUST_CD_M
        ORDER_CHECK_FLG
        '▼▼▼二次
        AUTO_MATOME_FLG
        RCV_NM_HED
        RCV_NM_DTL
        RCV_NM_EXT
        SND_NM
        EDI_CUST_INOUTFLG           '2012.02.25 大阪対応ADD
        OUTKA_MOTO                  '2013.04.03 Notes1995
        '受信DTL排他用コメントアウト
        'RCV_DTL_UPD_DATE
        'RCV_DTL_UPD_TIME
        '▲▲▲二次
        CHG_CUST_CD           '2015.09.03 tsunehira add
        GENPINHYO_CHKFLG            '2019/12/18 009991
        LAST                  '2013.04.03 Notes1995

    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        GRP_SATUS = 0
        NRS_BR_CD
        WH_CD
        CUST_CD_L
        CUST_CD_M
        TANTO
        EDI_DATE_FROM
        EDI_DATE_TO
        INKA_DATE_FROM
        INKA_DATE_TO
        'INKA_TP
        'INKA_KB
        CMB_JIKKOU
        BTN_JIKKOU
        CMB_PRINT
        BTN_PRINT
        CMBOUTPUT
        TXTPRTCUSTCD_L
        TXTPRTCUSTCD_M
        CMBOUTPUTKB
        CMBOUTPUTDATEFROM
        CMBOUTPUTDATETO
        BTNOUTPUT
        'CMB_REPRINT
        'BTN_REPRINT
        SPR_MAIN

    End Enum

    'タブインデックス用列挙体(進捗区分)
    Public Enum CtlTabIndex_KBN
        MITOUROKU = 0
        INKAZUMI
        JISSEKIMI
        JISSEKIZUMI
        SOUSINZUMI
        AKA
        ALL
        DEL
    End Enum

    '印刷種別用列挙体(印刷コンボ)
    Public Enum Print_KBN
        DEF = 0
        EDIINKACHECKLIST
        EDIINKACHECKLISTAXALTA
    End Enum

    '一括変更用列挙対（変更コンボ）
    Public Enum Chg_KBN
        BULK_CUST_CHANGE
    End Enum

    ''' <summary>
    ''' 実行種別(E012)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JIKKOU_SHUBETSU

        ''' <summary>
        ''' M品一括振替
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TRANSFER_COND_M As String = "10"

    End Class

End Class
