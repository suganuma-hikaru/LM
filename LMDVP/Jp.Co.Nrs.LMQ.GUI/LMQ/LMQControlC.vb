' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ          : 
'  プログラムID     :  LMQControlC  : LMQ データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================

''' <summary>
''' 定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMQControlC

    'データセットテーブル名
    Public Const TABLE_NM_OUT As String = "LMQ000OUT"

    ''' <summary>
    ''' 取得項目
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Public Enum ChkObject As Integer

        JOB_NO_ONLY = 0
        ALL_OBJECT

    End Enum

End Class