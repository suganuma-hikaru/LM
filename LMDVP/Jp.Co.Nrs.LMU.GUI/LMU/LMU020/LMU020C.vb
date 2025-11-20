' ==========================================================================
'  システム名       :  日本陸運産業株式会社 フォワーディングシステム
'  サブシステム名   :  
'  プログラムID     :  LMU020C
'  作  成  者       :  [IWAMOTO]
' ==========================================================================

''' <summary>
''' LMU020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2006/07/21 IWAMOTO
''' </histry>
Public Class LMU020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 閾値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMIT_REC_COUNT As Integer = 300

    ''' <summary>
    ''' マスタ権限区分 = 02
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRV_MST_KBN As String = "02"

    '区分コード(D020：JDE機械化関連フォルダ用)
    ' 第1階層(root)
    Public Const C028_FLD1_ROOT As String = "11"
    ' 第2階層
    Public Const C028_FLD2_IMP_IN As String = "21"
    Public Const C028_FLD2_IMP_RESULT As String = "22"
    Public Const C028_FLD2_SAVEOUT As String = "23"

    '区分コード(S072：システムID用)
    Public Const S072_NFS As String = "06"

    'アクションタイプ
    Public Enum ActionType
        Scan = 0
        Open
        JDEOut
        Other
        Save
        Close
    End Enum

    'データセットテーブル名
    Public Const TABLE_NM_OUT As String = "M_FILE"

    'イベント種別
    Public Const EVENT_IMPORT As String = "スキャンファイル取込"
    Public Const EVENT_FOLDEROPEN As String = "取込結果フォルダOpen"
    Public Const EVENT_SAVE As String = "保存"
    Public Const EVENT_CLOSE As String = "閉じる"

End Class
