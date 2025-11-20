' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI440  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================

''' <summary>
''' LMI440定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI440C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' フォームID(主)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MY_FORM_ID As String = "LMI440"

    ''' <summary>
    ''' メッセージID
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MESSAGE_ID

        ''' <summary>
        ''' 正常
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NORMAL As String = "G006"

        ''' <summary>
        ''' エラー(文字長超過)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ERROR_CHARACTER_LENGTH As String = "E482"

    End Class

    ''' <summary>
    ''' ガイダンス区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"


#Region "入力項目定義"
    Public Const FROM_TEXT As String = "(From)"
    Public Const TO_TEXT As String = "(To)"

    Public Const FILE_NAME_TEXT As String = "ファイル名"
    Public Const FILE_NAME_LENGTH As Integer = 50
    Public Const CHARACTER_TEXT As String = "文字"

#End Region

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = "LMI440IN"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT As String = "LMI440OUT"


    End Class

    ''' <summary>
    ''' カラム名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class COL_NAME

        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const CUST_CD_L As String = "CUST_CD_L"
        Public Const CUST_CD_M As String = "CUST_CD_M"
        Public Const KBN_GROUP_CD As String = "KBN_GROUP_CD"
        Public Const KBN_CD As String = "KBN_CD"
        Public Const KBN_NM1 As String = "KBN_NM1"
    End Class

    Public Const YES_CD As String = "01"



    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME

        ''' <summary>
        ''' シリンダー登録ファイル一覧取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectData As String = "SelectData"

    End Class

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        ''' <summary>
        ''' フォームを閉じる
        ''' </summary>
        ''' <remarks></remarks>
        CLOSE_FORM = 0

        ''' <summary>
        ''' 実行
        ''' </summary>
        ''' <remarks></remarks>
        EXECUTE

        ''' <summary>
        ''' 項目数
        ''' </summary>
        ''' <remarks></remarks>
        INDEX_COUNT

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        ACTION_NAME = 0
        ARR_DATE_FROM
        ARR_DATE_TO

    End Enum



    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        ''' <summary>
        ''' メイン画面
        ''' </summary>
        ''' <remarks></remarks>
        MAIN = 0

        ''' <summary>
        ''' 実行
        ''' </summary>
        ''' <remarks></remarks>
        EXECUTE

    End Enum

    ''' <summary>
    ''' 2次元配列行列インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ARRAY_INDEX

        ''' <summary>
        ''' 行
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ROW_DIMENSION As Integer = 0

        ''' <summary>
        ''' 列
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COL_DIMENSION As Integer = 1

    End Class


    ''' <summary>
    ''' Excelファイルフォーマット
    ''' </summary>
    ''' <remarks></remarks>
    Class READ_FILE_FORMAT

        ''' <summary>
        ''' 対象シート番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TARGET_SHEET_NO As Integer = 1

        ''' <summary>
        ''' タイトル行番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TITLE_ROW_NO As Integer = 1

        ''' <summary>
        ''' 開始行番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_DATA_ROW_NO As Integer = 2

        ''' <summary>
        ''' 開始列番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_COL_NO As Integer = 1

        ''' <summary>
        ''' 列数
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COLUMN_COUNT As Integer = 26


        ''' <summary>
        ''' カラムINDEX
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum COLUMN_INDEX
            DELIVERING_DOW_PLANT_ID = 1
            SAP_CLIENT_CODE
            SHIPMENT_SAP_COMPANY_CODE
            SHIPMENT_DOCUMENT_NUMBER
            GOODS_ISSUE_POSTING_DATE
            DISPATCH_TYPE_HEADER_DESCRIPTION
            CARRIER_CODE
            CARRIER_NAME
            SHIP_TO_CITY
            SHIP_FROM_CITY
            ACTUAL_DELIVERY_MONTH
            ACTUAL_DELIVERY_DAY
            ACTUAL_DELIVERY_YEAR
            ACTUAL_DELIVERY_HOUR
            ACTUAL_DELIVERY_MINUTE
            LATE_REASON_CODE
            HDR_DEADLINE_PLANNED_DATE_AND_TIME
            PLANNED_DELIVERY_TIME_FOR_ECC
            NUMBER
            CARRIER_CONTACT_EMAIL_ADDRESS
            CARRIER_CONTACT_EMAIL_ADDRESS_2
            ORDER_NUMBER
            MODE
            DELIVERY_NOTE_NUMBER
            CONSIGNEE_BP_NAME
            CARRIER_PASTE_IN_DATES
        End Enum

    End Class


    ''' <summary>
    ''' ファイルダイアログ設定
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FILE_DIALOG

        ''' <summary>
        ''' タイトル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TITLE As String = "取込ファイルを選択してください"

        ''' <summary>
        ''' フィルター
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FILTER As String = "Excelファイル(*.xlsx;*.xls)|*.xlsx;*.xls|All files (*.*)|*.*"

    End Class

    ''' <summary>
    ''' ファイル保存情報取得区分(LM_MST.Z_KBN)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SAVE_FILE_KBN

        ''' <summary>
        ''' KBN_GROUP_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GROUP_CD As String = "E046"

        ''' <summary>
        ''' KNB_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_CD As String = "01"

        ''' <summary>
        ''' 保存フォルダパス格納カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FOLDER_PATH_COL As String = "KBN_NM1"

        ''' <summary>
        ''' 保存ファイル名接頭文字カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FILE_PREFIX_COL As String = "KBN_NM2"


    End Class


    ''' <summary>
    ''' 検索対象荷主定義区分(LM_MST.Z_KBN)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CUST_CD_KBN

        ''' <summary>
        ''' KBN_GROUP_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GROUP_CD As String = "D029"


        ''' <summary>
        ''' 営業所コード格納カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS_BR_CD_COL As String = "KBN_NM1"

        ''' <summary>
        ''' 荷主コード(大)格納カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CUST_CD_L_COL As String = "KBN_NM2"

        ''' <summary>
        ''' 荷主コード(中)格納カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CUST_CD_M_COL As String = "KBN_NM3"
    End Class

    ''' <summary>
    ''' 輸送データベース名定義区分(LM_MST.Z_KBN)
    ''' </summary>
    ''' <remarks>
    ''' LM_TRN系はD003に定義され、NRSフレームワークで変換している
    ''' ToDo:NRS共通フレームワークへ組み込むか要検討
    ''' </remarks>
    Public Class TRANSPORT_DB_NAME_KBN

        ''' <summary>
        ''' KBN_GROUP_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GROUP_CD As String = "D030"

        ''' <summary>
        ''' KBN_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_CD As String = "01"

        ''' <summary>
        ''' データーベース名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DB_NAME_COL As String = "KBN_NM3"

    End Class





End Class
