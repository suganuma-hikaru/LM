' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI420C : 運賃比較
'  作  成  者       :  daikoku
' ==========================================================================

''' <summary>
''' LMI420定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI420C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI420IN"
    Friend Const TABLE_NM_OUT As String = "LMI420OUT"
    Friend Const TABLE_NM_EXCEL_IN As String = "LMI940IN"

    ''' <summary>
    ''' 帳票テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_RPT_IN As String = "LMH584IN"

    ''' <summary>
    ''' 遷移先フォームID
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FORM_ID As String = "LMI400"
    Friend Const FORM_EXCEL_ID As String = "LMI940"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        GETFILE = 0
        TOJIRU

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_NRS_BR = 0
        TXT_CUST_CD_L
        SPR_SEARCH

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprSearchColumnIndex

        'DEF = 0
        ORDER_NO = 0
        NRS_UNCHIN
        JX_UNCHIN
        SAGAKU
        OUTKA_PLAN_DATE
        UNSO_NO_L

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprDetailColumnIndex

        DEF = 0
        EDI_CTL_NO_CHU
        GOODS_CD_CUST
        LOT_NO
        GOODS_NM
        BUYER_ORD_NO
        OUTKA_TTL_NB

        NRS_BR_CD
        CUST_CD_L
        CLM_NM


    End Enum

End Class
