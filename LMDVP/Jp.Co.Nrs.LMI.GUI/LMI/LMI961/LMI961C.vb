' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI961C : GLIS見積情報照会（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMI961C定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI961C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 受注作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SHUKKA_TOUROKU As String = "JuchuSakusei"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_KENSAKU_IN As String = "LMI961KENSAKU_IN"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GAMEN_IN As String = "LMI961GAMEN_IN"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GAMEN_OUT As String = "LMI961GAMEN_OUT"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SHUKKA_TOUROKU = 0          '受注作成
        KENSAKU                     '検索

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        KENSAKU = 0
        ENTER
        MASTEROPEN
        DOUBLECLICK
        SAVE
        CLOSE
        LOOPEDIT
        PRINT
        SAIKEISAN
        BACKUP

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        PNL_CONDITION = 0
        CHK_YUSHUTSU
        CHK_YUNYU
        CHK_KOKUNAI
        EST_NO
        EST_NO_EDA
        FWD_USER_NM
        EST_MAKE_USER_NM
        GOODS_NM
        SEARCH_REM
        PNL_SAKUSEI_NAIYO
        OPT_YUKI
        OPT_KAERI
        DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        EST_NO_DISP
        JOB_CATEGORY_NM
        MOVE_TYPE_NM
        SEARCH_REM
        PORT_NM
        GOODS_NM
        CONT_SEQ
        CONT_TRN_NM
        PLACE_CD_A_NM
        STAR_PLACE_NM
        PLACE_CD_B_NM
        EXPIRY_TO_DATE_FWD
        FWD_USER_NM
        SYS_ENT_USER_NM
        EST_NO
        EST_NO_EDA
        JOB_OUT_EXP_KBN
        JOB_OUT_IMP_KBN
        JOB_IN_EXP_KBN
        JOB_IN_IMP_KBN
        JOB_LOC_KBN
        SYS_UPD_DATE
        SYS_UPD_TIME
        CONT_LEG_SEQ
        CARGO_SEQ
        LAST

    End Enum

End Class
