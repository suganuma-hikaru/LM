' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD         : 在庫サブシステム
'  プログラムID     :  LMDControlC : 在庫サブ 共通コンスト
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMDControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMDControlC
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
    ''' フラグ(無)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_ON As String = "01"

    ''' <summary>
    ''' フラグ(有)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_OFF As String = "00"

    ''' <summary>
    ''' 閾値(検索)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMIT_SELECT As String = "02"

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
    ''' yyyyMM
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_YYYYMM As String = "yyyyMM"

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
    ''' 検　索
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_KENSAKU As String = "検　索"

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
    ''' 実行
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_JIKKOUE As String = "実　行"

    ''' <summary>
    ''' 実行
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_DELETE As String = "削　除"

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

    ' 最終請求日チェック関連
    'Public Const TABLE_NM_LMD000_IN As String = "LMD000IN"
    'Public Const TABLE_NM_LMD000_OUT As String = "LMD000OUT"
    'Public Const COL_NRS_BR_CD As String = "NRS_BR_CD"
    'Public Const COL_CHK_DATE As String = "CHK_DATE"
    'Public Const COL_REPLACE_STR1 As String = "REPLACE_STR1"
    'Public Const COL_REPLACE_STR2 As String = "REPLACE_STR2"
    'Public Const COL_GOODS_CD_NRS As String = "GOODS_CD_NRS"

    ''' <summary>
    ''' YES/NOフラグ(NO)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const YN_FLG_NO As String = "00"

    ''' <summary>
    ''' YES/NOフラグ(YES)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const YN_FLG_YES As String = "01"

End Class