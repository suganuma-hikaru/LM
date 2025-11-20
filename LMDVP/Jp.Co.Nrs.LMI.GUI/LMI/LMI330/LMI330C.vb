' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330C : 納品データ選択&編集
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMI330定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI330C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI330IN"
    Friend Const TABLE_NM_OUT As String = "LMI330OUT"
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

        SETHIN = 0
        HENSHU
        KENSAKU
        MASTEROPEN
        HOZON
        TOJIRU
        DOUBLE_CLICK
        PRINT
        ENTER
        ROW_DEL
        EXCEL

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_NRS_BR = 0
        CMB_SOKO
        TXT_CUST_CD_L
        LBL_CUST_NM_L
        IMD_OUTKA_PLAN_DATE_FROM
        IMD_OUTKA_PLAN_DATE_TO
        CMB_PRINT_SHUBETU
        BTN_PRINT

        '要望対応:1853 yamanaka 2013.02.14 Start
        GRP_EXCEL
        TXT_GOODS_CD_POSITION
        CMB_EXCEL
        BTN_EXCEL
        '要望対応:1853 yamanaka 2013.02.14 End

        SPR_SEARCH
        TXT_DELIVERY
        IMD_ARR_PLAN_DATE
        TXT_DEST_CD
        LBL_DEST_NM
        BTN_DEL
        SPR_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprSearchColumnIndex

        DEF = 0
        DELIVERY_NO
        DEST_CD
        DEST_NM
        OUTKA_PLAN_DATE
        ARR_PLAN_DATE
        EDI_CTL_NO
        FREE_C06
        CREATE_USER
        CREATE_DATE
        CREATE_TIME

        NRS_BR_CD
        CUST_CD_L
        UPDATE_USER
        UPDATE_DATE
        UPDATE_TIME
        SYS_DEL_FLG
        CLM_NM


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
