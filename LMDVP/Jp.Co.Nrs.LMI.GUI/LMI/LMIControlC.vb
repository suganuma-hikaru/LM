' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI         : データ管理サブ
'  プログラムID     :  LMIControlC : データ管理サブ 共通コンスト
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMIControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMIControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

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
    ''' フラグ(有)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_OFF As String = "00"

    ''' <summary>
    ''' 作　成
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_CREATE As String = "作　成"

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_POP As String = "マスタ参照"

    ''' <summary>
    ''' 閉じる
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_CLOSE As String = "閉じる"

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
    ''' lblMsgAria
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_AREA As String = "lblMsgAria"

    ''' <summary>
    ''' BLF
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BLF As String = "BLF"

    ''' <summary>
    ''' yyyyMMdd
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_YYYYMMDD As String = "yyyyMMdd"

    ''' <summary>
    ''' yyyy/MM/dd
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_SLASH_YYYYMMDD As String = "yyyy/MM/dd"

    ''' <summary>
    ''' "　"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ZENKAKU_SPACE As String = "　"

    'START YANAI 20120120 請求データ作成対応
    ''' <summary>
    ''' Excel
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_EXEL As String = "Excel"

    ''' <summary>
    ''' 検　索
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_KENSAKU As String = "検　索"
    'END YANAI 20120120 請求データ作成対応

    'START YANAI 20120420 浮間在庫と日陸在庫の照合
    ''' <summary>
    ''' チェック
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_CHECK As String = "チェック"

    ''' <summary>
    ''' 取　込
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_TORIKOMI As String = "取　込"

    ''' <summary>
    ''' 集　計
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SHUKEI As String = "集　計"

    ''' <summary>
    ''' 照合
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SHOGO As String = "照　合"
    'END YANAI 20120420 浮間在庫と日陸在庫の照合

    'START YANAI 20120518 日医工詰め合わせ画面
    ''' <summary>
    ''' 追加
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_ADD As String = "追　加"

    ''' <summary>
    ''' クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_CLEAR As String = "クリア"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_PRINT As String = "印　刷"

    ''' <summary>
    ''' 荷札印刷
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_NIFUDAPRINT As String = "荷札印刷"


    ''' <summary>
    ''' 梱包明細印刷
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_KONPOPRINT As String = "梱包明細印刷"
    'END YANAI 20120518 日医工詰め合わせ画面


    'START 20120518 日医工製品メンテナンス画面
    ''' <summary>
    ''' 商品Ｍ反映
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_GOODSM As String = "商品Ｍ反映"
    'END 20120518 日医工製品メンテナンス画面

    'START YANAI 20120615 浮間出荷送状番号入力
    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_DEL As String = "削　除"

    ''' <summary>
    ''' 報告
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_HOKOKU As String = "報　告"

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_HOZON As String = "保　存"
    'END YANAI 20120615 浮間出荷送状番号入力

    'START YANAI 20120615 浮間出荷送状番号入力
    ''' <summary>
    ''' 詳細表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SHOSAI As String = "詳細表示"
    'END YANAI 20120615 浮間出荷送状番号入力

    'START YANAI 要望番号1338 篠崎運送倉庫 EDI月末在庫実績送信データ作成
    ''' <summary>
    ''' 実行
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_JIKKO As String = "実　行"
    'END YANAI 要望番号1338 篠崎運送倉庫 EDI月末在庫実績送信データ作成

    'START YANAI 要望番号1435 ハネウェル管理　の新規作成
    ''' <summary>
    ''' データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_GETDATA As String = "データ取得"

    ''' <summary>
    ''' 編集
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_EDIT As String = "編　集"

    ''' <summary>
    ''' 鈴商
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SUZUSHO As String = "鈴　商"

    ''' <summary>
    ''' 返却
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_HENKHAKU As String = "返　却"

    ''' <summary>
    ''' 出荷
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SHUKKA As String = "出　荷"

    '--------猪熊--------------------------------
    ''' <summary>
    ''' 返却／出荷
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_HENKHAKU_SHUKKA As String = "返却／出荷"

    ''' <summary>
    ''' 取得ログ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_GETLOG As String = "取得ログ"

    ''' <summary>
    ''' 廃棄
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_HAIKI As String = "廃　棄"

    ''' <summary>
    ''' 廃棄解除
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_HAIKI_KAIJO As String = "廃棄解除"

    ''' <summary>
    ''' 定期検査管理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_TEIKIKENSA_KANRI As String = "定期検査管理"
    'END YANAI 要望番号1435 ハネウェル管理　の新規作成

    'START KIM 20121003 ハネウェル管理（鈴木商館）の新規作成
    ''' <summary>
    ''' Excel
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_PRINT_EXEL As String = "EXCEL出力"
    'END KIM 20121003 ハネウェル管理（鈴木商館）の新規作成

    'START KIM 20121005 定期検査管理　の新規作成
    ''' <summary>
    ''' 新規
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SINKI As String = "新　規"

    ''' <summary>
    ''' 削除・復活
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_DEL_REV As String = "削除・復活"
    'END KIM 20121005 定期検査管理　の新規作成

    ''' <summary>
    ''' 取込更新
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_IMPORT_DATA As String = "取込更新"

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
