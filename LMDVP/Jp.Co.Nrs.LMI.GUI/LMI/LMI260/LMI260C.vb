' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI260C : 引取運賃明細入力
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMI260定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI260C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI260IN"
    Friend Const TABLE_NM_OUT As String = "LMI260OUT"

    ''' <summary>
    ''' 帳票テーブル
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Friend Const TABLE_NM_CHK As String = "LMI630IN"
    Friend Const TABLE_NM_MEISAI As String = "LMI640IN"

    ''' <summary>
    ''' 印刷種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_CHECK_LIST As String = "01"
    Friend Const PRINT_MEISAI As String = "02"

    ''' <summary>
    ''' 印刷用商品種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const PRINT_HIN_NM_ALL As String = "00"
    Friend Const PRINT_HIN_NM_HB As String = "01"
    Friend Const PRINT_HIN_NM_BTS As String = "02"
    Friend Const PRINT_HIN_NM_KEMI As String = "03"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SHINKI = 0
        HENSHU
        FUKUSHA
        SAKUJO_HUKKATU
        KENSAKU
        MASTEROPEN
        HOZON
        TOJIRU
        DOUBLE_CLICK
        PRINT
        ENTER
        FC_TOTAL
        DM_TOTAL
        ALL_TOTAL

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        IMD_HIKI_DATE_FROM = 0
        IMD_HIKI_DATE_TO
        CMB_PRINT_SHUBETU
        CMB_PRINT_HIN_NM
        SPR_DETAIL
        CMB_NRS_BR
        TXT_CUST_CD_L
        LBL_CUST_NM_L
        TXT_CUST_CD_M
        LBL_CUST_NM_M
        IMD_HIKI_DATE
        NUM_MEISAI_NO
        CMB_HIN_NM
        TXT_HIKITORI_CD
        LBL_HIKITORI_NM
        GRP_FC
        NUM_FC_NB
        NUM_FC_TANKA
        NUM_FC_TOTAL
        GRP_DM
        NUM_DM_NB
        NUM_DM_TANKA
        NUM_DM_TOTAL
        NUM_KISU
        NUM_SEIHIN
        NUM_SUKURAP
        NUM_WARIMASI
        NUM_SEIKEI
        NUM_ROSEN
        NUM_KOUSOKU
        NUM_SONOTA
        NUM_ALL_TOTAL
        TXT_REMARK
        LBL_CRT_USER
        LBL_CRT_DATE
        LBL_UPD_USER
        LBL_UPD_DATE
        LBL_UPD_TIME

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprDetailColumnIndex

        DEF = 0
        STATUS
        HIKI_DATE
        MEISAI_NO
        HIKI_CD
        HIKI_NM
        HIN_NM
        FC_NB
        FC_TANKA
        FC_TOTAL
        DM_NB
        DM_TANKA
        DM_TOTAL
        KISU
        SEIHIN
        SUKURAP
        WARIMASI
        SEIKEI
        ROSEN
        KOUSOKU
        SONOTA
        ALL_TOTAL
        CREATE_USER
        CREATE_DATE
        CREATE_TIME
        UPDATE_USER
        UPDATE_DATE
        UPDATE_TIME
        SYS_DEL_FLG
        NRS_BR_CD
        CUST_CD_L
        CUST_CD_M
        CUST_NM_L
        CUST_NM_M
        REMARK
        HIN_CD
        EXIST_UPDATE_TIME
        CLM_NM


    End Enum

End Class
