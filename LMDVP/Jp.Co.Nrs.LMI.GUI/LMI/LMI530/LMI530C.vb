' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530C : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI530定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI530C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 処理可能営業所
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ENABLE_NRS_BR_CD As String = "40"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        SELECT_KB = 0

    End Enum

    ''' <summary>
    ''' 現在の取込対象(cmbSelectKb選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cmbSelectKbItems
        Public Const CSV As String = "CSV"
        Public Const Excel As String = "Excel"
    End Class

    ''' <summary>
    ''' EDI_CUST_INDEX
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ediCustIndex
        Public Const CSV As Integer = 159
        Public Const Excel As Integer = 109
    End Class

End Class
