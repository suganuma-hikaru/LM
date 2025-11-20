' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI         : データ管理サブ
'  プログラムID     :  LMJControlC : データ管理サブ 共通コンスト
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMIControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMJControlC
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
    Public Const FLG_OFF As String = "00"

    ''' <summary>
    ''' フラグ(有)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_ON As String = "01"

    ''' <summary>
    ''' 実　行
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_CREATE As String = "実　行"

    ''' <summary>
    ''' 検　索
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_SEARCH As String = "検　索"

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
