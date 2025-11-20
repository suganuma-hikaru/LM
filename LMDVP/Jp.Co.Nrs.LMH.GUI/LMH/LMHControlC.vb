' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH         : EDI
'  プログラムID     :  LMHControlC : EDI 共通コンスト
'  作  成  者       :  [Kim]
' ==========================================================================

''' <summary>
''' LMHControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMHControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "EDI管理番号"


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
    ''' 編集
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_EDIT As String = "編　集"

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_POP As String = "マスタ参照"

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_SAVE As String = "保　存"

    ''' <summary>
    ''' 閉じる
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_CLOSE As String = "閉じる"

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
    ''' 運賃計算締め基準
    ''' </summary>
    ''' <remarks>タリフマスタの適用日取得で利用</remarks>
    Public Const UNTIN_CALCULATION_KB_OUTKA As String = "01"  '出荷予定日
    Public Const UNTIN_CALCULATION_KB_ARR As String = "02"    '納入予定日

    ''' <summary>
    ''' セット区分
    ''' </summary>
    ''' <remarks>運送タリフマスタセットで利用</remarks>
    Public Const SET_KB_CUST As String = "00"    '荷主
    Public Const SET_KB_DEST As String = "01"    '届先

    ''' <summary>
    ''' lblMsgAria
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_AREA As String = "lblMsgAria"

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

    ''' <summary>
    ''' 最大桁数(整数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_3 As String = "999"

    ''' <summary>
    ''' 最大桁数(整数4桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_4 As String = "9,999"

    ''' <summary>
    ''' 最大桁数(整数5桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_5 As String = "99,999"

    ''' <summary>
    ''' 最大桁数(整数6桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_6 As String = "999,999"

    ''' <summary>
    ''' 最大桁数(整数6桁　小数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_6_3 As String = "999,999.999"

    ''' <summary>
    ''' 最大桁数(整数9桁　小数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_9_3 As String = "999,999,999.999"

    ''' <summary>
    ''' 最大桁数(整数10桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_10 As String = "9,999,999,999"

    ''' <summary>
    ''' 最小値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MIN_0 As String = "0"

    ''' <summary>
    ''' 【区分Ｍ】入荷作業進捗(N004)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class INKA_STATE_KB
        ''' <summary>
        ''' 予定入力済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AlreadyScheduledInput As String = "10"

        ''' <summary>
        ''' 受付表印刷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AcceptanceVotePrint As String = "20"

        ''' <summary>
        ''' 受付済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AcceptanceSettled As String = "30"

        ''' <summary>
        ''' 検品済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InspectionCompleted As String = "40"

        ''' <summary>
        ''' 入荷済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AlreadyInStock As String = "50"

        ''' <summary>
        ''' 報告済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Reported As String = "90"
    End Class

End Class