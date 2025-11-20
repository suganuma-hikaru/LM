' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI250C : シリンダ番号チェック
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMI250定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI250C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI610IN"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        PRINT
        TOJIRU

    End Enum

End Class
