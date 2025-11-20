' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG010C : 保管料/荷役料計算
'  作  成  者       :  []
' ==========================================================================

''' <summary>
''' LMG010定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMG010IN"
    Public Const TABLE_NM_OUT As String = "LMG010OUT"
    Public Const TABLE_NM_DEL As String = "LMG010INOUT_DEL"
    Public Const TABLE_NM_EXECUTE As String = "LMG010IN_CALC"
    Public Const TABLE_NM_CHK_VAR As String = "LMG010CHK_VAR"
    Public Const TABLE_NM_CHK_APPROVAL_TANKA As String = "LMG010CHK_APPROVAL_TANKA"

    ''' <summary>
    '''  コンボボックス初期値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MATSUJIME As String = "00"
    Public Const ONLINE As String = "02"

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
        JOUKYOUSHOSAI
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
        TXT_CUSTCD_L
        TXT_CUSTCD_M
        LBL_CUSTNM
        CMB_SHIMEBI
        IMD_INV_DATE
        CHK_TANTOSHA
        GRP_MODE
        OPT_CHECK
        OPT_HONBAN
        CMB_BACTH
        CHK_MIKAN
        GRP_INSATSU_JOKEN
        OPT_YAKAN
        OPT_ONLINE
        CHK_PREVIEW
        NUM_BUSU
        LBL_JOB
        SPE_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        LAST_INV_DATE
        SHIMEBI
        CUST_CD
        CUST_NM
        SEIKIKAN_TO
        KIWARI_KBN
        NRS_BR_CD
        UPD_DATE_M_CUST
        UPD_TIME_M_CUST
        JOB_NO
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        CLOSE_KB
        KIWARI_KB
        INV_DATE

    End Enum

End Class
