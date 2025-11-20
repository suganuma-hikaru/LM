' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG020C : 保管料/荷役料計算 [明細検索画面]
'  作  成  者       :  []
' ==========================================================================

''' <summary>
''' LMG020定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMG020IN"
    Public Const TABLE_NM_OUT As String = "LMG020OUT"

    ''' <summary>
    ''' 閾値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMITED_COUNT As Integer = 500

    ''' <summary>
    ''' モード条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CHECK As String = "01"                     'チェック
    Public Const HONBAN As String = "00"                    '本番

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
        SETCUST
        SETSEKY
        PRINT
        DOUBLECLICK
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
        BTN_CUST
        TXT_SEKY
        LBL_SEKY
        BTN_SEKY
        CMB_SHIMEBI
        IMD_INV_DATE
        CHK_TANTOSHA
        CMB_PRINT
        CHK_PRE
        NUM_BUSU
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
        SHIMEBI
        CUST_CD
        CUST_NM
        JOB_NO
        CREATE_DATE
        CREATE_USER
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        OYA_SEIQTO_CD
        SEKY_FLG
        CUST_NM_L_M
        SYS_ENT_PGID
    End Enum

End Class
