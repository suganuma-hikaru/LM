' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG080C : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================

''' <summary>
''' LMG080定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMG080IN"
    Public Const TABLE_NM_OUT As String = "LMG080OUT"
    Public Const TABLE_NM_SELECTIN As String = "LMG080IN_SELECT"
    Public Const TABLE_NM_DEL As String = "LMG080IN_DEL"
    Public Const TABLE_NM_IN_RESULT As String = "LMG080IN_RESULTS"
    Public Const TABLE_NM_OUT_RESULT As String = "LMG080_OUT_RESULTS"
    Public Const TABLE_NM_IN_EXECUTE As String = "LMG080IN_EXECUTE"
    Public Const TABLE_NM_OUT_ERR As String = "LMG080OUT_ERR"

    Public Const ONLINE As String = "02"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        KENSAKU = 0
        YOYAKU_CANCEL
        SHORISHOUSAI
        KYOUSEI
        CLOSE
        KYOUSEI_DEL

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        BATCH = 0
        SHORI_MI
        SHORI_ZUMI
        SHORI_CHU
        SHORI_TORIKESHI
        JIKKOU_FROM
        JIKKOU_TO
        MODE
        IMD_FROM_DATE
        IMD_TO_DATE
        SPREAD

    End Enum

    ''' <summary>
    ''' スプレッド用インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        KEKKA_KB
        BATCH_CONDITION_CD
        BATCH_CONDITION_NM
        JIKKOU_MODE
        NRS_BR_CD
        NRS_BR_NM
        USER_ID
        USER_NM
        SEKY_DATE
        CUST_CD
        CUST_NM
        JIKKO_DATE
        JIKKO_TIME
        SHORI_DATE
        SHORI_TIME
        SYURYO_DATE
        SYURYO_TIME
        JOB_NO
        JIKKOZUMI_KB
        SEKY_FLG
        BATCH_NO
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        SYS_UPD_DATE
        SYS_UPD_TIME
        REC_NO
        EXEC_STATE_KB

    End Enum

End Class
