' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM         : マスタサブ
'  プログラムID     :  LMMControlC : マスタサブ 共通コンスト
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMMControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMMControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' ファンクションキー設定名称(マスタ共通)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_F1_SHINKI As String = "新　規"
    Friend Const FUNCTION_F2_HENSHU As String = "編　集"
    Friend Const FUNCTION_F3_FUKUSHA As String = "複　写"
    '2016.01.06 UMANO 英語化対応START
    'Friend Const FUNCTION_F4_SAKUJO_HUKKATU As String = "削除・復活"
    Friend Const FUNCTION_F4_SAKUJO_HUKKATU As String = "削除･復活"
    '2016.01.06 UMANO 英語化対応END
    Friend Const FUNCTION_F9_KENSAKU As String = "検　索"
    '要望管理2166 SHINODA START
    Friend Const FUNCTION_F10_KENSAKU As String = "DIC検索"
    '要望管理2166 SHINODA END
    Friend Const FUNCTION_F10_MST_SANSHO As String = "マスタ参照"
    Friend Const FUNCTION_F11_HOZON As String = "保　存"
    Friend Const FUNCTION_F12_TOJIRU As String = "閉じる"
    ''' <summary>
    ''' ファンクションキー設定名称(各画面固有)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_F5_TANKA_IKKATU As String = "単価一括変更"
    Friend Const FUNCTION_F5_DEST_IKKATU As String = "一括登録"
    Friend Const FUNCTION_F8_HIRAKU As String = "開く"
    Friend Const FUNCTION_F7_INSATSU As String = "印　刷"
    Friend Const FUNCTION_F7_INPUTXLS As String = "Excel取込"
    '2015.10.02 他荷主対応START
    Friend Const FUNCTION_F7_TANINUSI As String = "他荷主"
    '2015.10.02 他荷主対応END
    Friend Const FUNCTION_F8_OUTPUTXLS As String = "Excel出力"
    'START YANAI 要望番号372
    Friend Const FUNCTION_F6_NINUSHI_IKKATU As String = "荷主一括変更"
    'END YANAI 要望番号372
    Friend Const FUNCTION_F8_KIKEN As String = "危険情報確認"
    Friend Const FUNCTION_F5_REQUEST As String = "申請"
    Friend Const FUNCTION_F6_APPROVAL As String = "承認"

    ''' <summary>
    ''' 閾値(区分マスタ:S054)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const LIMIT_COUNT_OKURIJO As String = "00"                     '送り状番号
    Friend Const LIMIT_COUNT_SAGYO_MEISAISHO_SAKUSEI As String = "01"     '作業明細書作成画面
    Friend Const LIMIT_COUNT_KENSAKU_GAMEN As String = "02"               '検索画面

    ''' <summary>
    ''' 大小チェック用に使用
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const HAN_ALPHAMERIC As String = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

    ''' <summary>
    ''' 荷主M存在チェック(置換文字)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CustMsgType As Integer

        CUST_L
        CUST_M
        CUST_S
        CUST_SS

    End Enum

    ''' <summary>
    ''' 運送会社M存在チェック(置換文字)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum UnsoMsgType As Integer

        UNSO_COMP
        UNSO_SHITEN
        
    End Enum

    ''' <summary>
    ''' YES/NOフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_ON As String = "01"   '有り
    Public Const FLG_OFF As String = "00" '無し


    ''' <summary>
    ''' 閾値(検索)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMIT_SELECT As String = "02"

    ''' <summary>
    ''' マスタ参照ボタンイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MASTEROPEN As String = "MASTEROPEN"

    ''' <summary>
    ''' Enterイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ENTER As String = "ENTER"

    ''' <summary>
    ''' BLF
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BLF As String = "BLF"

    ''' <summary>
    ''' コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CD As String = "コード"

    ''' <summary>
    ''' 支店コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BR_CD As String = "支店コード"

    ''' <summary>
    ''' 棟番号
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TOU_NO As String = "棟番号"

    ''' <summary>
    ''' 室番号
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SITU_NO As String = "室番号"

    ''' <summary>
    ''' (大)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const L_NM As String = "(大)"

    ''' <summary>
    ''' (中)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const M_NM As String = "(中)"

    ''' <summary>
    ''' (From)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FROM_NM As String = "(From)"

    ''' <summary>
    ''' (To)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TO_NM As String = "(To)"

    ''' <summary>
    ''' 括弧[
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKKO_1 As String = "["

    ''' <summary>
    ''' 括弧]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKKO_2 As String = "]"

    ''' <summary>
    ''' " = "
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EQUAL As String = " = "

    ''' <summary>
    ''' ,
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KANMA As String = ","

    ''' <summary>
    ''' 印刷種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_KBN As String = "印刷種別"

    ''' <summary>
    ''' "　"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ZENKAKU_SPACE As String = "　"


    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GROUP As String = "まとめ指示"

    ''' <summary>
    ''' 運賃確定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKUTEI As String = "運賃確定"

    ''' <summary>
    ''' 運行新規
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNCONEW As String = "運行新規"

    ''' <summary>
    ''' 運行編集
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNCOEDIT As String = "運行編集"

    ''' <summary>
    ''' 運送新規
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNSONEW As String = "運送新規"

    ''' <summary>
    ''' タリフ区分(混載)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_KONSAI As String = "10"

    ''' <summary>
    ''' タリフ区分(車扱い)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_KURUMA As String = "20"

    ''' <summary>
    ''' タリフ区分(特便)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_TOKUBIN As String = "30"

    ''' <summary>
    ''' タリフ区分(横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_YOKO As String = "40"

    ''' <summary>
    ''' タリフ区分(路線)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_ROSEN As String = "50"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' 日付項目設定時使用
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum DATE_FORMAT As Integer

        YYYY_MM_DD = 0
        YYYY_MM
        MM_DD

    End Enum

    ''' <summary>
    ''' クリア処理対象コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CLERA_DATA As Integer
        IMTEXT
        IMNUMBER
        IMCOMB
        IMKBN_COMB
        IMNRS_COMB
        IMSOK_COMB
        IMDATE
        ISNULL
    End Enum

End Class