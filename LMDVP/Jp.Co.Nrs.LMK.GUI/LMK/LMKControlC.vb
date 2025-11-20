' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK         : 請求サブシステム
'  プログラムID     :  LMKControlC : 請求サブシステム共通コンスト
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMKControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMKControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' ファンクションキー設定名称
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FUNCTION_SHINKI As String = "新　規"
    Friend Const FUNCTION_HENSHU As String = "編　集"
    Friend Const FUNCTION_FUKUSHA As String = "複　写"
    Friend Const FUNCTION_SAKUJO As String = "削　除"
    Friend Const FUNCTION_KAKUTEI As String = "確　定"
    Friend Const FUNCTION_TORIKOMI As String = "取　込"
    Friend Const FUNCTION_HOUKOKU As String = "報　告"
    Friend Const FUNCTION_SHOKIKA As String = "初期化"
    Friend Const FUNCTION_INSATU As String = "印　刷"
    Friend Const FUNCTION_KENSAKU As String = "検　索"
    Friend Const FUNCTION_MST_SANSHO As String = "マスタ参照"
    Friend Const FUNCTION_HOZON As String = "保　存"
    Friend Const FUNCTION_TOJIRU As String = "閉じる"

    Friend Const FUNCTION_EXEL As String = "Excel"
    Friend Const FUNCTION_SHINKI_TORIKOMI As String = "新規取込"
    Friend Const FUNCTION_SHINKI_TEGAKI As String = "新規手書き"
    Friend Const FUNCTION_KEIRI_TAISHOGAI As String = "経理対象外"
    Friend Const FUNCTION_KEIRI_MODOSHI As String = "経理戻し"
    Friend Const FUNCTION_YOYAKU_TORIKESI As String = "予約取消"
    Friend Const FUNCTION_SHORI_KEKKA_SHOSAI As String = "処理結果詳細"

    Friend Const FUNCTION_JOKYO_SHOSAI As String = "状況詳細"
    Friend Const FUNCTION_JIKKOU As String = "実　行"
    Friend Const FUNCTION_ZENKAI_KEISAN_TORIKESHI As String = "前回計算取消"
    Friend Const FUNCTION_KYOUSEI_JIKKOU As String = "強制実行"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_GET_INV_IN As String = "START_DATE_IN"
    Friend Const TABLE_NM_GET_INV_OUT As String = "START_DATE_OUT"


    ''' <summary>
    ''' YES/NOフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const YN_FLG_NO As String = "00"         'NO
    Friend Const YN_FLG_YES As String = "01"        'YES

    ''' <summary>
    ''' 鑑作成区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const CRT_TORIKOMI As String = "00"      '自動取込請求書
    Friend Const CRT_TEGAKI As String = "01"        '手書き取込請求書

    ''' <summary>
    ''' 作成種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const DETAIL_SAKUSEI_AUTO As String = "00"       '自動
    Friend Const DETAIL_SAKUSEI_ADD As String = "01"        '追加

    ''' <summary>
    ''' 課税区分
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TAX_KAZEI As String = "01"         '課税
    Friend Const TAX_MENZEI As String = "02"        '免税
    Friend Const TAX_HIKAZEI As String = "03"       '非課税
    Friend Const TAX_UCHIZEI As String = "04"       '内税

    ''' <summary>
    ''' 勘定科目コード設定時取得項目
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum GetKanjoKmkInfo

        KANJO_KMK_CD = 0
        KEIRI_BUMON_CD

    End Enum


End Class
