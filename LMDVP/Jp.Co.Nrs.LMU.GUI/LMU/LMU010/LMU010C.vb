' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU     : 文書管理
'  プログラムID     :  LMU010C : 文書管理画面
'  作  成  者       :  大野
' ==========================================================================

''' <summary>
''' LMU010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMU010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMU010IN"
    Public Const TABLE_NM_OUT As String = "M_FILE"
    Public Const TABLE_NM_COMBO As String = "LMU010_COMBO"
    Public Const TABLE_NM_KANRITYPE As String = "LMU010_KANRITYPE"
    Public Const TABLE_NM_FILETYPE As String = "LMU010_FILETYPE"

    Public Const KBN_CD As String = "KBN_CD"     '【S072】システムID
    Public Const KBN_NM As String = "KBN_NM3"

    Public Const KBN_KCD As String = "KBN_KCD"   '【S012】アタッチ種別区分
    Public Const KBN_KNM As String = "KBN_KNM1"

    Public Const KBN_FCD As String = "KBN_FCD"   '【S014】添付ファイルタイプ区分
    Public Const KBN_FNM As String = "KBN_FNM1"


    '閾値
    Public Const LIMITED_COUNT As Integer = 300

    'アクションタイプ
    Public Enum ActionType As Integer
        Save
        Close

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColNoIndex

        Def = 0
        Fileno
        System
        SystemNm
        Filetype
        Filename
        Remark
        Filepath
        Sysentuser
        Sysentdate
        Updflg
        Sysupddate
        Sysupdtime
        RecordNo

    End Enum

    'System区分
    Public Const SYSTEM_NVO As String = "01"
    Public Const SYSTEM_LEASE As String = "02"

    'アタッチファイル種別区分
    Public Const ATTACH_TANK As String = "02"
    Public Const ATTACH_ACTION As String = "09"

    'イベント種別
    Public Const EVENT_EDIT As String = "編集"
    Public Const EVENT_SAVE As String = "保存"
    Public Const EVENT_CLOSE As String = "閉じる"
    Public Const EVENT_ADD As String = "行追加"
    Public Const EVENT_DEL As String = "行削除"
    Public Const EVENT_DCLICK As String = "ダブルクリック"


End Class
