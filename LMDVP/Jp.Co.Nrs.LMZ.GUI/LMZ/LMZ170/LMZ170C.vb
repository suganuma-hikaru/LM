' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ170C : 車輌マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ170定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ170C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ170IN"
    Public Const TABLE_NM_OUT As String = "LMZ170OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        CAR_KEY
        CAR_NO
        VCLE_KB_NM
        VCLE_KB
        TRAILER_NO
        LOAD_WT
        TEMP_YN_NM
        TEMP_YN
        ONDO_MX
        ONDO_MM
        FUKUSU_ONDO_YN_NM
        FUKUSU_ONDO_YN
        INSPC_DATE_TRUCK
        INSPC_DATE_TRAILER
        UNSOCO_NM
        UNSOCO_BR_NM
        ROW_INDEX

    End Enum

End Class
