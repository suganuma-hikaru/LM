'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ160F :　乗務員マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ160定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ160C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ160IN"
    Public Const TABLE_NM_OUT As String = "LMZ160OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        DRIVER_CD
        DRIVER_NM
        AVAL_YN
        LCAR_LICENSE_YN
        TRAILER_LICENSE_YN
        OTSU1_YN
        OTSU2_YN
        OTSU3_YN
        OTSU4_YN
        OTSU5_YN
        OTSU6_YN
        HICOMPGAS_YN
        ROW_INDEX

    End Enum
End Class
