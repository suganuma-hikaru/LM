' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM320C : 請求項目マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================

''' <summary>
''' LMM320定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM320C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM320IN"
    Public Const TABLE_NM_OUT As String = "LMM320OUT"

    '新規画面コンボボックス初期値
    Public Const COMBO_BOX_DEFAULT As String = "01"

    'メッセージ置換文字
    Public Const SEIQGRPKBNMSG As String = "請求グループコード区分"
    Public Const SEIQKMKMSG As String = "請求項目コード"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabindex

        DATAIL = 0
        GROUP_KB
        SEIQKMK_CD
        SEIQKMK_CD_S
        SEIQKMK_NM
        TAX_KB
        KEIRI_KB
        REMARK
        PRINT_SORT

    End Enum


    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        PRINT_SORT
        GROUP_KB
        GROUP_KB_NM
        SEIQKMK_CD
        SEIQKMK_CD_S
        SEIQKMK_NM
        TAX_KB
        TAX_KB_NM
        KEIRI_KB
        KEIRI_KB_NM
        REMARK
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum
End Class
