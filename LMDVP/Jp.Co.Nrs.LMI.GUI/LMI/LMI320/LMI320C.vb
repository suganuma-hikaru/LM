' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI320C : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMI320定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI320C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI320IN"
    Friend Const TABLE_NM_OUT As String = "LMI320OUT"

    ''' <summary>
    ''' 帳票テーブル
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Friend Const TABLE_NM_SEIQ_MEISAI As String = "LMI650IN"
    Friend Const TABLE_NM_KAGAMI As String = "LMI660IN"
    Friend Const TABLE_NM_DENPYO As String = "LMI670IN"

    ''' <summary>
    ''' 印刷種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_SEIQ_MEISAI As String = "01"
    Friend Const PRINT_SEIQ_KAGAMI As String = "02"
    Friend Const PRINT_SHIIRE_DENPYOU As String = "03"
    Friend Const PRINT_IKKATU As String = "04"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        KENSAKU = 0
        SAKUSEI
        TOJIRU
        PRINT

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_NRS_BR = 0
        IMD_SEIQ_DATE
        CMB_MAKE
        BTN_MAKE
        CMB_PRINT_SHUBETU
        BTN_PRINT_SHUBETU
        SPR_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprDetailColumnIndex

        DEF = 0
        SEIQTO_CD
        SEIQTO_NM
        KAGAMI_SHUBETU
        TOKUISAKI_CD
        HUTANKA
        KIGYO_CD 'notes1907 add
        CLM_NM


    End Enum

End Class
