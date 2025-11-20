' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF210C : 運行情報一覧検索
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMF210定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF210C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMF210IN"
    Public Const TABLE_NM_OUT As String = "LMF210OUT"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO
        LoadWtZanFrom
        LoadWtZanTo
        UncouFrom
        UncouTo
        OnkanFrom
        OnkanTo

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        TRIP_NO
        TRIP_DATE
        UNSO_NM
        CAR_NO
        BIN
        REMARK
        JSHA_NM
        DRIVER_NM
        UNSO_PKG_NB
        UNSO_WT
        LOAD_WT
        ON_KAN
        PAY_AMT
        
    End Enum

End Class
