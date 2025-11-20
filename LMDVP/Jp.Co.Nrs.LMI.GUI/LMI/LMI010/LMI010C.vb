' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMA     : メニュー
'  プログラムID     :  LMI010C : 荷主選択
'  作  成  者       :  [笈川]
' ==========================================================================

''' <summary>
''' LMI010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI010C
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
