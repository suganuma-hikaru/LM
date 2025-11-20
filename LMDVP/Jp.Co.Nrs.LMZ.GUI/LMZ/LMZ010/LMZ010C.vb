' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ010C : 初期荷主変更
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ010IN"
    Public Const TABLE_NM_OUT As String = "LMZ010OUT"

    '有無フラグ(初期荷主該当フラグ)
    Public Const UMU_NASHI As String = "00"
    Public Const UMU_ARI As String = "01"

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        DEFAULT_CUST_YN_NM
        DEFAULT_CUST_YN
        CUST_NM_L
        CUST_NM_M
        CUST_CD_L
        CUST_CD_M
        USER_CD
        USER_CD_EDA
        ROW_INDEX

    End Enum

End Class
