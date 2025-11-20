' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================

''' <summary>
''' LMQ010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMQ010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMQ010IN"
    Public Const TABLE_NM_OUT As String = "LMQ010INOUT"
    Public Const TABLE_NM_EXCEL As String = "LMQ010_EXCEL"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    'ファンクションキー名
    Public Const FNCNM1 As String = "新規"         '新規
    Public Const FNCNM2 As String = "編集"         '編集
    Public Const FNCNM3 As String = ""             '未使用
    Public Const FNCNM4 As String = "削除・復活"   '削除・復活
    Public Const FNCNM5 As String = ""             '未使用
    Public Const FNCNM6 As String = ""             '未使用
    Public Const FNCNM7 As String = ""             '未使用
    Public Const FNCNM8 As String = ""             '未使用
    Public Const FNCNM9 As String = "検索"         '検索
    Public Const FNCNM10 As String = "Excel作成"   'Excel作成
    Public Const FNCNM11 As String = "保存"        '保存
    Public Const FNCNM12 As String = "閉じる"      '閉じる

    '再描画
    Public Const NEW_MODE As String = "0"   '描画
    Public Const RE_MODE As String = "1"    '再描画

    'イベント種別
    Public Enum EventShubetsu As Integer

        MAIN = 0
        SINKI           '新規
        HENSYU          '編集
        DELREV          '削除・復活
        KENSAKU         '検索
        HOZON           '保存
        EXCEL           'EXCEL作成
        CLOSE           '閉じる
        DOUBLE_CLICK    'ダブルクリック

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprPatternColumnIndex

        DEF = 0
        SYS_DEL_FLG_NM
        PATTERN_ID
        EX_TYPE_NM
        FILE_NM
        FILE_TITLE_NM
        EX_CONTENTS
        SYS_ENT_DATE
        LAST_ACTION_DATE
        SYS_DEL_FLG

    End Enum

    ' DataSet部列インデックス用列挙対
    Public Enum DsPatternColumnIndex

        PATTERN_ID = 0
        EX_TYPE_KB
        EX_CONTENTS
        EX_SQL
        FILE_NM
        FILE_TITLE_NM
        LAST_ACTION_DATE
        LAST_ACTION_USER
        SYS_ENT_DATE
        SYS_ENT_TIME
        SYS_ENT_PGID
        SYS_ENT_USER
        SYS_UPD_DATE
        SYS_UPD_TIME
        SYS_UPD_PGID
        SYS_UPD_USER
        SYS_DEL_FLG
        SYS_ENT_USER_NM
        SYS_UPD_USER_NM
        SYS_DEL_FLG_NM
        EX_TYPE_NM

    End Enum

End Class
