' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA010C : ログイン
'  作  成  者       :  [iwamoto]
' ==========================================================================

''' <summary>
''' LMA010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 画面パターン定数
    ''' </summary>
    ''' <remarks>ログイン</remarks>
    Friend Const FORM_PATTERN_LOGIN As String = "LOGIN"

    ''' <summary>
    ''' 画面パターン定数
    ''' </summary>
    ''' <remarks>パスワード変更</remarks>
    Friend Const FORM_PATTERN_CHANGE_PWD As String = "CHANGE_PWD"

    ''' <summary>
    ''' 画面パターン定数
    ''' </summary>
    ''' <remarks>パスワード変更</remarks>
    Friend Const TABLE_NM_IN As String = "LMA010IN"

    ''' <summary>
    ''' タブインデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        USERID = 0
        PASSWORD
        REPASSWORD
        OK
        CANCEL
        GRP_MESSAGE_TYPE
        OPT_JP
        OPT_EN
        OPT_KO
        OPT_CN

    End Enum

End Class
