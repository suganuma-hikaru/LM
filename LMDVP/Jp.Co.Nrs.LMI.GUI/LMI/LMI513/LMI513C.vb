' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI513C : JNC_運賃照合
'  作  成  者       :  daikoku
' ==========================================================================

''' <summary>
''' LMI513定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI513C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI513IN"
    Public Const TABLE_NM_IN_TXT As String = "LMI513IN_TXT"
    Public Const TABLE_NM_IN_RPT As String = "LMI513IN_RPT"

    ''' <summary>
    ''' 印刷アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT_DATA As String = "PrintData"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_SAKUSEI = 0
        CMB_EIGYO
        TXT_FILE_PATH
        SYORI_DATE

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        TOJIRU
        SAKUSEI

    End Enum

    ''' <summary>
    ''' ガイダンス区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN_ERR As String = "00"

    ''' <summary>
    ''' メッセージコード(初期表示)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG_INIT As String = "G006"

    ''' <summary>
    ''' メッセージコード(非数値)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MSG_NOT_NUMERIC As String = "E005"

    ''' <summary>
    ''' 取込ファイルパス
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FILE_PATH As String = "C:\LMUSER\ﾁｯｿUNCHINOUT.txt"

End Class
