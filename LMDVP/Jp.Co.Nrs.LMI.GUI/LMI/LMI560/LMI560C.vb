' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI560C : TSMC請求データ計算
'  作  成  者       :  [HORI]
' ==========================================================================

''' <summary>
''' LMI560定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI560C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI560IN"
    Public Const TABLE_NM_IN_CALC As String = "LMI560IN_CALC"
    Public Const TABLE_NM_IN_DEL As String = "LMI560IN_DEL"
    Public Const TABLE_NM_OUT As String = "LMI560OUT"
    Public Const TABLE_NM_OUT_CALC As String = "LMI560OUT_CALC"
    Public Const TABLE_NM_OUT_JOBNO As String = "LMI560OUT_JOBNO"

    ''' <summary>
    ''' 閾値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMITED_COUNT As Integer = 500

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        KENSAKU = 0
        ZENKEISANTORI
        JIKKOU
        MASTER
        TOJIRU
        ENTER

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_SELECT = 0
        CMB_BR
        TXT_SEIQTO_CD
        LBL_SEIQTO_NM
        IMD_INV_DATE
        SPE_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        LAST_DATE
        SEIQTO_CD
        SEIQTO_NM
        CLOSE_KB
        LAST_DATE_ORG
        LAST_JOB_NO
        BEFORE_DATE
        BEFORE_JOB_NO

    End Enum

End Class
