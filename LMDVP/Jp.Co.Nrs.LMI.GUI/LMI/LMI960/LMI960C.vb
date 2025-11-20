' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI960C : 出荷データ確認（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMI960C定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI960C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INSERT_SENDOUTEDI As String = "InsertSendOutEdi"

    ''' <summary>
    ''' 受注作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INSERT_SENDOUTEDI_JUCHU As String = "InsertSendOutEdiJuchu"    'ADD 2019/12/12 009741

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INSERT_SENDOUTEDI_DELAY As String = "InsertSendOutEdiDelay"

    ''' <summary>
    ''' 出荷登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SHUKKA_TOUROKU As String = "ShukkaTouroku"    'ADD 2020/02/07 010901

    ''' <summary>
    ''' GLIS受注削除アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_DELETE_GLIS_JUCHU As String = "DelBookingForHwl"    'ADD 2020/02/07 010901

    ''' <summary>
    ''' 入荷登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_NYUKA_TOUROKU As String = "NyukaTouroku"

    ''' <summary>
    ''' 運送登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_UNSO_TOUROKU As String = "UnsoTouroku"

    ''' <summary>
    ''' 一括変更アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_IKKATSU_CHANGE As String = "IkkatsuChange"    'ADD 2019/03/27

    ''' <summary>
    ''' 受注ステータス戻しアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_ROLLBACK_JUCHU_STATUS As String = "RollbackJuchuStatus"

    ''' <summary>
    ''' JOB NO変更アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_MOD_JOB_NO As String = "ModJobNo"

    ''' <summary>
    ''' EDI取消アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_EDI_TORIKESHI As String = "EdiTorikeshi"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI960IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMI960OUT"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SAKUSEI_TARGET As String = "LMI960SAKUSEI_TARGET"

    'ワーニング処理用データテーブル
    Public Const TABLE_NM_WARNING_HED As String = "WARNING_HED"
    Public Const TABLE_NM_WARNING_DTL As String = "WARNING_DTL"
    Public Const TABLE_NM_WARNING_SHORI As String = "WARNING_SHORI"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        JISSEKI_SAKUSEI = 0     '実績作成
        KENSAKU                 '検索
        IKKATSU_CHANGE          '一括変更       'ADD 2019/03/27
        JUCHU_SAKUSEI           '受注返答       'ADD 2019/12/12 009741
        SHUKKA_TOUROKU          '出荷/受注登録  'ADD 2020/02/07 010901
        DELETE_GLIS_JUCHU       'GLIS受注削除   'ADD 2020/02/07 010901
        UPDATE_CUST_AUTO        '荷主自動振分   'ADD 2020/02/27 010901
        UPDATE_CUST_MANUAL      '荷主手動振分   'ADD 2020/02/27 010901
        DELAY_SAKUSEI           '遅延送信
        ROLLBACK_JUCHU_STATUS   '受注ステータス戻し
        MOD_JOB_NO              'JOB NO変更
        EDI_TORIKESHI           '未処理→EDI取消
        ROLLBACK_EDI_TORIKESHI  'EDI取消→未処理
        NYUKA_TOUROKU           '入荷登録
        IMPORT_CYLINDER         'シリンダー取込
        UNSO_TOUROKU            '運送登録

    End Enum

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
        BTN_UNSO_TOUROKU

        PNL_CONDITION
        'ADD S 2020/02/07 010901
        CHK_MISHORI
        CHK_SHUKKA_TOUROKU_ZUMI
        CHK_JUCHU_OK
        CHK_JUCHU_NG
        CHK_JISSEKI_SAKUSEI_ZUMI
        'ADD E 2020/02/07 010901
        CHK_TORIKESHI
        BUMON
        OUTKA_DATE_FROM
        OUTKA_DATE_TO
        CHK_MITEI
        CHK_INKA
        CHK_OUTKA
        CHK_UNSO

        'ADD START 2019/03/27
        PNL_IKKATSU_CHANGE
        CMB_CHANGE_ITEM
        IMD_CHANGE_DATE
        BTN_IKKATSU_CHANGE
        'ADD END   2019/03/27

        'ADD S 2019/12/12 009741
        PNL_JUCHU
        OPT_JUCHU_OK
        OPT_JUCHU_NG
        CMB_DECLINE_REASON  'ADD 2020/03/06 011377
        'ADD E 2019/12/12 009741

        PNL_DELAY
        CMB_DELAY_SHUBETSU
        CMB_DELAY_REASON
        CMB_DELAY_HOSOKU

        PNL_SAKUSEI_NAIYO
        BASHO_KB
        ARRIVAL_TIME
        DEPARTURE_TIME

        DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks>
    ''' ★★★スプレッド列定義体(sprDetailDef)でのみ使用すること(LASTを除く)★★★
    ''' ★★★列移動可能なため列追加は必ず最後尾にすること★★★
    ''' </remarks>
    Public Enum SprColumnIndex

        DEF = 0
        'ADD S 2019/12/12 009741
        REC_STATUS
        HORYU_KB            'ADD 2020/02/07 010901
        JUCHU_STATUS
        'ADD E 2019/12/12 009741
        STATUS
        DELAY_STATUS
        CYLINDER_SERIAL_NO
        GOODS_CD
        GOODS_NM
        LOAD_NUMBER
        SAP_ORD_NO
        CUST_ORD_NO
        INOUT_KB
        INOUTKA_CTL_NO        'ADD 2020/02/07 010901
        CUST_CD_L           'ADD 2020/02/27 010901
        CUST_CD_M           'ADD 2020/02/27 010901
        SHUKKA_DATE
        NONYU_DATE
        SHUKKA_MOTO_CD
        SHUKKA_MOTO
        NONYU_SAKI_CD
        NONYU_SAKI
        JURYO
        HED_CRT_DATE
        HED_FILE_NAME
        HED_UPD_DATE
        HED_UPD_TIME
        'ADD START 2019/03/27
        STP1_GYO
        STP1_UPD_DATE
        STP1_UPD_TIME
        STP2_GYO
        STP2_UPD_DATE
        STP2_UPD_TIME
        'ADD END  2019/03/27
        P_STOP_NOTE
        D_STOP_NOTE
        SKU_NUMBER
        NUMBER_PIECES
        INOUTKA_CTL_NO_DELETED
        SEQ_DESC
        BUYID
        LAST

    End Enum

    ''' <summary>
    ''' 部門名称(cmbBumon選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbBumonItems
        Public Const Soko As String = "倉庫"
        Public Const ISO As String = "ISO"
        Public Const ChilledLorry As String = "Chilled Lorry"
    End Class

    ''' <summary>
    ''' 場所区分(cmbBashoKb選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbBashoKbItems
        Public Const Tsumikomi As String = "積込場"
        Public Const Nioroshi As String = "荷下場"
        Public Const NonyuYotei As String = "納入予定"
    End Class

    ''' <summary>
    ''' 入出荷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InOutKb
        Public Const Mitei As String = ""
        Public Const Inka As String = "1"
        Public Const Outka As String = "2"
        Public Const Unso As String = "3"
    End Class

    ''' <summary>
    ''' 入出荷区分名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InOutKbName
        Public Const Mitei As String = "未定"
        Public Const Inka As String = "入荷"
        Public Const Outka As String = "出荷"
        Public Const Unso As String = "輸送"
    End Class

    'ADD S 2019/12/12 009741
    ''' <summary>
    ''' ステータス区分(TMC取消)名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RecStatusName
        Public Const NewBooking As String = "新規"
        Public Const Updated As String = "訂正"
        Public Const Cancelled As String = "取消"
    End Class

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' EDI保留区分名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DelKbName
        Public Const Seijou As String = "正常"
        Public Const Horyu As String = "保留"
    End Class
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' 進捗区分(受注ステータス)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum ShinchokuKbJuchu As Integer
        Mishori = 1            '未処理
        JuchuOK                 '受注OK
        JuchuNG                 '受注NG
        NyuShukkaTourokuZumi    '入出荷/受注登録済  'ADD 2020/02/07 010901
        JissekiSakuseiZumi      '実績作成済         'ADD 2020/02/07 010901
        EdiTorikeshi            'EDI取消
    End Enum

    ''' <summary>
    ''' 受注ステータス名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JuchuStatusName
        Public Const Mishori As String = "未処理"                            'MOD 2020/02/07 010901
        Public Const JuchuOK As String = "受注OK"
        Public Const JuchuNG As String = "受注NG"
        Public Const NyuShukkaTourokuZumi As String = "入出荷輸送登録済"     '進捗区分(受注ステータス)=4で倉庫の場合    'ADD 2020/02/07 010901
        Public Const JuchuTourokuZumi As String = "受注登録済"               '進捗区分(受注ステータス)=4でISOの場合     'ADD 2020/02/07 010901
        Public Const JissekiSakuseiZumi As String = "実績作成済"             'ADD 2020/02/07 010901
        Public Const EdiTorikeshi As String = "EDI取消"
    End Class
    'ADD E 2019/12/12 009741

    ''' <summary>
    ''' 配送ステータス名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StatusName
        Public Const Misoushin As String = "未送信"
        Public Const PickZumi As String = "ピック済"
        Public Const NioroshiZumi As String = "荷下ろし済"
        Public Const NonyuYotei As String = "納入予定"
    End Class

    'ADD START 2019/03/27
    ''' <summary>
    ''' 一括変更項目(cmbIkkatsuChange選択肢)コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbIkkatsuChangeItems
        Public Const ShukkaDate As String = "01"
        Public Const NonyuDate As String = "02"
        Public Const RollbackJuchuStatus As String = "03"
        Public Const JobNo As String = "04"
        Public Const EdiTorikeshi As String = "05"
        Public Const RollbackEdiTorikeshi As String = "06"
    End Class
    'ADD END  2019/03/27

    ''' <summary>
    ''' 遅延種別(cmbDelayShubetsu選択肢)コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbDelayShubetsuItems
        Public Const Shukka As String = "01"
        Public Const Nonyu As String = "02"
    End Class

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' 入出荷管理番号/JOB NO 削除済
    ''' </summary>
    Public Const DELETED As String = "削除済"
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' 営業所コード
    ''' </summary>
    Public Class NrsBrCd
        Public Const Chiba As String = "10"
        Public Const Forwarding As String = "90"
    End Class

    ''' <summary>
    ''' メッセージ用文言（変数WordsNyuShukka用）
    ''' </summary>
    Public Class WordsNyuShukka
        Public Const ForSoko As String = "入荷/出荷/輸送"
        Public Const ForISO As String = "受注"
    End Class

    ''' <summary>
    ''' メッセージ用文言（変数WordsShukkaTouroku用）
    ''' </summary>
    Public Class WordsShukkaTouroku
        Public Const ForSoko As String = "出荷登録"
        Public Const ForISO As String = "受注登録"
    End Class

    ''' <summary>
    ''' 明細部INOUTKA_CTL_NO列の見出し文言
    ''' </summary>
    Public Class ColTitleInoutkaCtlNo
        Public Const ForSoko As String = "入出荷管理番号" & vbCrLf & "/運送番号"
        Public Const ForISO As String = "JOB NO"
    End Class

End Class
