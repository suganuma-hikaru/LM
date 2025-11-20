' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME800C : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================

''' <summary>
''' LMC930定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME800C
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
        ''' <summary>
        ''' 指示
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INSTRUCT As String = "00"
        ''' <summary>
        ''' 削除
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DELETE As String = "01"
        ''' <summary>
        ''' 取消
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CANCEL As String = "02"
    End Class

    ''' <summary>
    ''' テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM
        Public Const LME800IN As String = "LME800IN"
    End Class
End Class
