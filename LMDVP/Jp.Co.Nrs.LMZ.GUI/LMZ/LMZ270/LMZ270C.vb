'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ270V : 注意書テーブル照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ270定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ270C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ270IN"
    Public Const TABLE_NM_OUT As String = "LMZ270OUT"

    '用途区分のグループコード
    Public Const YOUTO As String = "Y005"

    '前ゼロ
    Public Const ZERO_DATA As String = "000"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        REMARK
        REM_NO
        ROW_INDEX

    End Enum

End Class
