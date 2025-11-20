' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB810    : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================

''' <summary>
''' LMB810定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB810C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 現場作業指示ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WH_TAB_SIJI_STATUS
        Public Const NOT_INSTRUCTED As String = "00"
        Public Const INSTRUCTED As String = "01"
        Public Const CHANGED As String = "02"
    End Class
    ''' <summary>
    ''' 処理種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PROC_TYPE
        Public Const INSTRUCT As String = "00"
        Public Const DELETE As String = "01"
        Public Const CANCEL As String = "02"
    End Class

    ''' <summary>
    ''' テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM
        Public Const LMB810IN As String = "LMB810IN"
    End Class

End Class
