' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI570C : TSMC請求データ検索
'  作  成  者       :  [HORI]
' ==========================================================================

''' <summary>
''' LMI570定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI570C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI570IN"
    Public Const TABLE_NM_OUT As String = "LMI570OUT"

    ''' <summary>
    ''' 閾値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMITED_COUNT As Integer = 500

    ''' <summary>
    ''' 作成プログラムＩＤ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ENT_PG_IKOU As String = "IKOU"            '移行データ

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        KENSAKU = 0
        MASTER
        CLOSE
        PRINT
        ENTER

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_SELECT = 0
        CMB_BR
        TXT_CUSTCD_L
        TXT_CUSTCD_M
        TXT_CUSTCD_S
        TXT_CUSTCD_SS
        LBL_CUSTNM
        TXT_SEKY
        LBL_SEKY
        IMD_INV_DATE
        CMB_PRINT
        BTN_PRINT
        SPE_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        INV_DATE
        SEIQTO_CD
        SEIQTO_NM
        CREATE_DATE
        CREATE_USER
        JOB_NO
    End Enum

End Class
