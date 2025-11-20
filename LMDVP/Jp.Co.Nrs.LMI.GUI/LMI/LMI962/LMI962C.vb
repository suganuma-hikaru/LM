' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI962C : EDIワーニング・エラーチェック画面
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMI962定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI962C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_HED As String = "WARNING_HED"
    Public Const TABLE_NM_DTL As String = "WARNING_DTL"
    Public Const TABLE_NM_SHORI As String = "WARNING_SHORI"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        SHUBETSU
        NRS_BR
        NRS_WH
        CUST_CD_L
        CUST_CD_M
        CUST_NM
        GRP_WARNING
        SPR_WARNING
        LOAD_NUMBER
        CMD_GYO
        KOMOKU_NM
        KOMOKU_VAL
        MST_VAL
        TXT_WARNING

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprWarning As Integer

        DEF = 0
        SHORI           '処理区分
        ORDER_NO_L      'オーダー番号（大）
        ORDER_NO_M      'オーダー番号（中）
        LOAD_NUMBER
        CMD_GYO
        MESSAGE_ID      'メッセージID
        WARNING_MSG     'メッセージ
        GOODS_NM        '商品名
        DEST_NM         '届先名             '2012.06.01 ディック対応 追加ADD
        KOMOKU_NM       '項目名
        KOMOKU_VAL      '項目値
        ADDITIONAL_FIELD_VALUE_1 ' 付加項目値1
        MASTER_VAL      'マスタ値
        EDI_CTL_NO_L    'EDI管理番号L
        EDI_CTL_NO_M    'EDI管理番号M
        EDI_WARNING_ID  'ワーニングID
        CUST_CD_L
        CUST_CD_M
        WH_CD
        CRT_DATE
        FILE_NAME
        LAST_INDEX

    End Enum

    '選択値
    Public Const SELECT_YES As String = "01"
    Public Const SELECT_NO As String = "02"
    Public Const SELECT_CANCEL As String = "03"

    ''' <summary>
    ''' ワーニングIDフォーマット
    ''' </summary>
    Public Class WARNING_ID_FMT
        Public Class MST_FLG
            ''' <summary>
            ''' マスタフラグ開始位置
            ''' </summary>
            Public Const START_IDX As Integer = 7

            ''' <summary>
            ''' マスタフラグ文字長
            ''' </summary>
            Public Const LEN As Integer = 1

            ''' <summary>
            ''' 指定なし
            ''' </summary>
            Public Const NONE As String = "0"

            ''' <summary>
            ''' 商品マスタ
            ''' </summary>
            Public Const M_GOODS As String = "1"

            ''' <summary>
            ''' 届先マスタ
            ''' </summary>
            Public Const M_DEST As String = "2"
        End Class
    End Class

    ''' <summary>
    ''' 処理済みフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShorizumiFlg
        Public Const Mishori As String = "0"
        Public Const ShoriZumi As String = "1"
        Public Const Cancel As String = "2"
    End Class

End Class
