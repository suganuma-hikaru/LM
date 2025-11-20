' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ040 : 単価マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ040IN"
    Public Const TABLE_NM_OUT As String = "LMZ040OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UP_GP_CD_1
        KIWARI_KB_NM
        KIWARI_KB
        REMARK
        STR_DATE
        STORAGE_KB1_NM
        STORAGE_KB1
        STORAGE_1
        STORAGE_KB2_NM
        STORAGE_KB2
        STORAGE_2
        HANDLING_IN_KB_NM
        HANDLING_IN_KB
        HANDLING_IN
        MINI_TEKI_IN_AMO
        HANDLING_OUT_KB_NM
        HANDLING_OUT_KB
        HANDLING_OUT
        MINI_TEKI_OUT_AMO
        REC_NO
        ROW_INDEX

    End Enum

End Class
