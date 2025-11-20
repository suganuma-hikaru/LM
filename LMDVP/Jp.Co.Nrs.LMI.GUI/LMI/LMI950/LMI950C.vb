' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI950C : 運賃データ確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

''' <summary>
''' LMI950C定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI950C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INSERT_SENDUNCHIN As String = "InsertSendUnchin"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI950IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMI950OUT"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SENDUNCHIN_TARGET As String = "LMI950SENDUNCHIN_TARGET"

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

        CONDITION = 0
        EIGYO
        CUSTCDL
        CUSTNM
        OUTKA_DATA
        DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SHORI
        HORYU
        SEND
        OUTKA_NO_L
        DEST_NM
        KYORI
        JURYO
        UNCHIN
        HOUKOKU_UNCHIN
        OUTKA_DATE
        KOJO_KANRI_NO
        CRT_DATE
        FILE_NAME
        REC_NO
        UNSO_NO_L
        UNSO_L_UPD_DATE
        UNSO_L_UPD_TIME
        UNCHIN_UPD_DATE
        UNCHIN_UPD_TIME
        LAST

    End Enum

End Class
