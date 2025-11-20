' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================

''' <summary>
''' LMI430定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI430C
    Inherits Jp.Co.Nrs.LM.Const.LMConst


    ''' <summary>
    ''' フォームID(主)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MY_FORM_ID As String = "LMI430"

    ''' <summary>
    ''' フォームID(従)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FORM_ID
        ''' <summary>
        ''' 荷主マスタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const M_CUST As String = "LMZ260"
    End Class


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
    Public Const ROW_NO_TEXT As String = "行番号"
    Public Const ROW_NO_LENGTH As Integer = 3

    Public Const GAS_NAME_TEXT As String = "ガス種別"
    Public Const GAS_NAME_LENGTH As Integer = 60

    Public Const VOLUME_TEXT As String = "容量"
    Public Const VOLUME_LENGTH As Integer = 40

    Public Const SERIAL_NO_TEXT As String = "シリアル番号"
    Public Const SERIAL_NO_LENGTH As Integer = 40

    Public Const FILE_NAME_TEXT As String = "ファイル名"
    Public Const FILE_NAME_LENGTH As Integer = 30

    Public Const CUST_CD_L_TEXT As String = "荷主(大)コード"
    Public Const CUST_CD_L_LENGTH As Integer = 5

    Public Const CUST_CD_M_TEXT As String = "荷主(中)コード"
    Public Const CUST_CD_M_LENGTH As Integer = 2

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
        Public Const INPUT As String = "LMI430IN"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT As String = "LMI430OUT"


        ''' <summary>
        ''' 入力テーブル(シリンダー)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_CYLINDER As String = "LMI430IN_CYLINDER"

        ''' <summary>
        ''' 出力テーブル(検品データ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_INSPECTION_DATA As String = "LMI430OUT_INSPECTION_DATA"

    End Class

    ''' <summary>
    ''' カラム名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class COL_NAME

        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const CUST_CD_L As String = "CUST_CD_L"
        Public Const CUST_CD_M As String = "CUST_CD_M"
        Public Const CUST_CD_S As String = "CUST_CD_S"
        Public Const CUST_CD_SS As String = "CUST_CD_SS"
        Public Const CUST_NM_L As String = "CUST_NM_L"
        Public Const CUST_NM_M As String = "CUST_NM_M"
        Public Const SYS_DEL_FLG As String = "SYS_DEL_FLG"
        Public Const USER_CD As String = "USER_CD"
        Public Const DEFAULT_CUST_YN As String = "DEFAULT_CUST_YN"
        Public Const LOCK_FLG As String = "LOCK_FLG"
        Public Const INKA_CYL_FILE_NO_L As String = "INKA_CYL_FILE_NO_L"
        Public Const KBN_GROUP_CD As String = "KBN_GROUP_CD"
        Public Const KBN_CD As String = "KBN_CD"
        Public Const KBN_NM1 As String = "KBN_NM1"
        Public Const VALUE1 As String = "VALUE1"

    End Class

    Public Const YES_CD As String = "01"

    Public Const DEFAUL_CUST_CD_M As String = "00"
    Public Const DEFAUL_CUST_CD_S As String = "00"
    Public Const DEFAUL_CUST_CD_SS As String = "00"

    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME

        ''' <summary>
        ''' シリンダー登録ファイル一覧取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectLoadedInkaCylFileList As String = "SelectLoadedInkaCylFileList"

        ''' <summary>
        ''' シリンダー登録
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InsertCylinderData As String = "InsertCylinderData"

        ''' <summary>
        ''' シリンダー削除
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DeleteCylinderData As String = "DeleteCylinderData"

        ''' <summary>
        ''' 検品結果取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectInspectionData As String = "SelectInspectionData"

        'ADD 2017/04/24
        ''' <summary>
        ''' 検品結果取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectReadResulData As String = "SelectReadResulData"

    End Class

    ''' <summary>
    ''' イベント名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EVENT_NAME

        ''' <summary>
        ''' ファイル取込
        ''' </summary>
        ''' <remarks></remarks>
        Public Const LOAD_FILE As String = "取込"

        ''' <summary>
        ''' 検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEARCH_DATA As String = "検索"

        ''' <summary>
        ''' フォームを閉じる
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CLOSE_FORM As String = "閉じる"

        ''' <summary>
        ''' Excel出力
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT_EXCEL As String = "Excel出力"

        ''' <summary>
        ''' 選択行削除
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DELETE_SELECTED_ROW As String = "行削除"
    End Class


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        ''' <summary>
        ''' ファイル読込
        ''' </summary>
        ''' <remarks></remarks>
        LOAD_FILE = 0

        ''' <summary>
        ''' 検索
        ''' </summary>
        ''' <remarks></remarks>
        SEARCH

        ''' <summary>
        ''' フォームを閉じる
        ''' </summary>
        ''' <remarks></remarks>
        CLOSE_FORM

        ''' <summary>
        ''' Excel出力
        ''' </summary>
        ''' <remarks></remarks>
        CREATE_EXCEL


        ''' <summary>
        ''' マスタ参照
        ''' </summary>
        ''' <remarks></remarks>
        OPEN_MASTER


        ''' <summary>
        ''' 選択行削除
        ''' </summary>
        ''' <remarks></remarks>
        DELETE_SELECTED_ROW


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

        CUSTCD_L = 0
        CUSTCD_M
        INKADATE_FROM
        INKADATE_TO
        INKADATE
        REMARK1
        REMARK2
        REMARK3
        SPRDETAILS
        READDATE_FROM
        READDATE_TO

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0

        ''' <summary>
        ''' 入荷日
        ''' </summary>
        ''' <remarks></remarks>
        INKA_DATE

        ''' <summary>
        ''' 作成ユーザー名
        ''' </summary>
        ''' <remarks></remarks>
        CRT_USER_NAME

        ''' <summary>
        ''' 読込ファイル名
        ''' </summary>
        ''' <remarks></remarks>
        LOAD_FILE_NAME

        ''' <summary>
        ''' 読込データ数
        ''' </summary>
        ''' <remarks></remarks>
        LOAD_DATA_COUNT

        ''' <summary>
        ''' 備考1
        ''' </summary>
        ''' <remarks></remarks>
        REMARK_1

        ''' <summary>
        ''' 備考2
        ''' </summary>
        ''' <remarks></remarks>
        REMARK_2

        ''' <summary>
        ''' 備考3
        ''' </summary>
        ''' <remarks></remarks>
        REMARK_3

        ''' <summary>
        ''' 作成日
        ''' </summary>
        ''' <remarks></remarks>
        CRT_DATE

        ''' <summary>
        ''' 作成時間
        ''' </summary>
        ''' <remarks></remarks>
        CRT_TIME

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <remarks></remarks>
        NRS_BR_CD

        ''' <summary>
        ''' 入荷シリンダーファイル取込番号(大)
        ''' </summary>
        ''' <remarks></remarks>
        INKA_CYL_FILE_NO_L

        ''' <summary>
        ''' 最終更新日
        ''' </summary>
        ''' <remarks></remarks>
        LAST_UPD_DATE

        ''' <summary>
        ''' 最終更新時間
        ''' </summary>
        ''' <remarks></remarks>
        LAST_UPD_TIME

        ''' <summary>
        ''' 項目数
        ''' </summary>
        ''' <remarks></remarks>
        INDEX_COUNT
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
        ''' マスタ参照
        ''' </summary>
        ''' <remarks></remarks>
        MASTER

        ''' <summary>
        ''' Enter押下
        ''' </summary>
        ''' <remarks></remarks>
        ENTER

        ''' <summary>
        ''' ファイル取込
        ''' </summary>
        ''' <remarks></remarks>
        LOAD_FILE

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
    ''' 取込シリンダーファイルフォーマット
    ''' </summary>
    ''' <remarks></remarks>
    Class CYL_FILE_FORMAT

        ''' <summary>
        ''' 対象シート番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TARGET_SHEET_NO As Integer = 1

        ''' <summary>
        ''' 開始行番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_ROW_NO As Integer = 2

        ''' <summary>
        ''' 開始列番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_COL_NO As Integer = 1

        ''' <summary>
        ''' 列数
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COLUMN_COUNT As Integer = 5

        ''' <summary>
        ''' 最大行数
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MAX_ROW_COUNT As Integer = 999

        ''' <summary>
        ''' カラムINDEX
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum COLUMN_INDEX
            ROW_NUMBER = 1
            ITEM_COUNT
            GAS_NAME
            VOLUME
            SERIAL_NO
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
    ''' 保存ファイルフォーマット
    ''' </summary>
    ''' <remarks></remarks>
    Class SAVE_FILE_FORMAT

        ''' <summary>
        ''' 対象シート番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_SHEET_NO As Integer = 1

        ''' <summary>
        ''' 開始行番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_ROW_NO As Integer = 1


        ''' <summary>
        ''' 一シートの最大行数
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MAX_ROW_COUNT_PER_SHEET As Integer = 20000


        ''' <summary>
        ''' 開始列番号(1～)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const START_COL_NO As Integer = 1

        ''' <summary>
        ''' 列数
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COLUMN_COUNT As Integer = COLUMN_INDEX.IS_SCAN + 1

        ''' <summary>
        ''' カラムインデックス
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum COLUMN_INDEX
            ''' <summary>
            ''' 入荷日
            ''' </summary>
            ''' <remarks></remarks>
            INKA_DATE = 0

            ''' <summary>
            ''' シリンダ番号
            ''' </summary>
            ''' <remarks></remarks>
            SERIAL_NO

            ''' <summary>
            ''' ガス種別
            ''' </summary>
            ''' <remarks></remarks>
            GAS_NAME

            ''' <summary>
            ''' 容量
            ''' </summary>
            ''' <remarks></remarks>
            VOLUME

            ''' <summary>
            ''' WTI作業者
            ''' </summary>
            ''' <remarks></remarks>
            INSPECTION_USER_NM

            ''' <summary>
            ''' 取込有無
            ''' </summary>
            ''' <remarks></remarks>
            IS_LOAD

            ''' <summary>
            ''' 読取(スキャン)有無
            ''' </summary>
            ''' <remarks></remarks>
            IS_SCAN
        End Enum

        Public Shared TITLE As String() =
            {
                "入荷日",
                "シリアル番号",
                "ガス種別",
                "容量",
                "WTI作業者",
                "取込",
                "読取(スキャン)"
            }

        ''' <summary>
        ''' シート名フォーマット
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SHEET_NAME_FORMAT As String = "{0}{1}{2}"

        ''' <summary>
        ''' 保存するExcelファイルのフォーマット
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EXCEL_FORMAT As Microsoft.Office.Interop.Excel.XlFileFormat _
            = Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8

        ''' <summary>
        ''' 保存ファイル拡張子
        ''' </summary>
        ''' <remarks>
        ''' XlFileFormatと対応させる
        ''' </remarks>
        Public Const SAVE_FILE_EXTENSION As String = ".xls"

        '***読取結果用


        ''' <summary>
        ''' カラムインデックス
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum COLUMN_INDEX2
            ''' <summary>
            ''' シリンダーファイル番号（大）
            ''' </summary>
            ''' <remarks></remarks>
            INKA_CYL_FILE_NO_L = 0

            ''' <summary>
            ''' シリンダ番号
            ''' </summary>
            ''' <remarks></remarks>
            SERIAL_NO

            ''' <summary>
            ''' 検品日
            ''' </summary>
            ''' <remarks></remarks>
            INSPECTION_DATE

            ''' <summary>
            ''' 検品時刻
            ''' </summary>
            ''' <remarks></remarks>
            INSPECTION_TIME

            ''' <summary>
            ''' 検品ユーザー
            ''' </summary>
            ''' <remarks></remarks>
            INSPECTION_USER_NM

            ''' <summary>
            ''' 荷主コードl
            ''' </summary>
            ''' <remarks></remarks>
            CUST_CD_L

            ''' <summary>
            ''' 荷主コードM
            ''' </summary>
            ''' <remarks></remarks>
            CUST_CD_M

 
        End Enum

        Public Shared TITLE2 As String() =
            {
                "シリンダーファイル番号（大）",
                "シリアル番号",
                "検品日",
                "検品時刻",
                "検品ユーザー",
                "荷主CD_L",
                "荷主CD_M"
            }
    End Class


    ''' <summary>
    ''' ファイル保存情報取得区分(Z_KBN)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SAVE_FILE_KBN

        ''' <summary>
        ''' KBN_GROUP_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GROUP_CD As String = "E045"

        ''' <summary>
        ''' KNB_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_CD As String = "01"

        Public Const KBN_CD2 As String = "02"       '読取結果　ADD 2017/04/24

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


End Class
