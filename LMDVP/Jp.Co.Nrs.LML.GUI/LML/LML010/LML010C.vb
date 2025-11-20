' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LML     : メニュー
'  プログラムID     :  LML010C : 荷主選択
'  作  成  者       :  [笈川]
' ==========================================================================

''' <summary>
''' LML010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LML010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        SENTAKU
        TOJIRU

    End Enum

End Class
