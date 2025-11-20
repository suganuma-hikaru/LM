' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI450  : 東レ・ダウExcelファイル取込
'  作  成  者       :  [hojo]
' ==========================================================================

''' <summary>
''' LMI450定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI450C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' フォームID(主)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MY_FORM_ID As String = "LMI450"

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

    ''' <summary>
    ''' 製品単位
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SEIHIN_TANI

        ''' <summary>
        ''' CTN
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CTN As String = "CTN"

    End Class
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
        Public Const INPUT As String = "LMI450IN"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI As String = "LMI450EDI"

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EXCEL As String = "LMI450EXCEL"
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
        Public Const GROUP_CD As String = "T030"

        ''' <summary>
        ''' KNB_CD
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KBN_CD As String = "01"

        ''' <summary>
        ''' 保存フォルダパス格納カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_PATH As String = "KBN_NM1"

        ''' <summary>
        ''' 保存ファイル名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_FILE_NM As String = "KBN_NM2"

        ''' <summary>
        ''' EDIバッチパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BATCH_PATH As String = "KBN_NM3"

        ''' <summary>
        ''' バックアップフォルダパス格納カラム
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BACKUP_PATH As String = "KBN_NM4"

        ''' <summary>
        ''' バックアップファイル接頭語
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BACKUP_FILE_PREFIX As String = "KBN_NM5"

        ''' <summary>
        ''' PSToolパス
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PSTOOL_PATH As String = "KBN_NM6"

    End Class

End Class
