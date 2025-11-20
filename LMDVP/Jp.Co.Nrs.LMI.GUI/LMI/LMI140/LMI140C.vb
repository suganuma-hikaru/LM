' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140C : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

''' <summary>
''' LMI140定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI140C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI140IN"
    Public Const TABLE_NM_OUT As String = "LMI140OUT"
    Public Const TABLE_NM_GUIERROR As String = "LMI140_GUIERROR"
    Public Const TABLE_NM_H_WHEDI_BAH As String = "LMI140_H_WHEDI_BAH"
    Public Const TABLE_NM_H_SENDWHEDI_BAH As String = "LMI140_H_SENDWHEDI_BAH"

    'EVENTNAME
    Public Const EVENTNAME_SINKI As String = "新規"
    Public Const EVENTNAME_JISSEKI As String = "実績作成"
    Public Const EVENTNAME_KENSAKU As String = "検索"
    Public Const EVENTNAME_CLOSE As String = "閉じる"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        JISSEKI_SHORI_FLG_1
        JISSEKI_SHORI_FLG_2
        PROC_DATE_FROM
        PROC_DATE_TO
        PROC_TYPE_1
        PROC_TYPE_2
        PROC_KBN_1
        PROC_KBN_2
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SINKI = 0
        JISSEKI
        KENSAKU
        CLOSE
        DOUBLE_CLICK

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        '表示列
        DEF = 0
        JISSEKI_FUYO_NM
        JISSEKI_SHORI_FLG_NM
        PROC_TYPE_NM
        PROC_KBN_NM
        PROC_DATE
        OUTKA_WH_TYPE_NM
        INKA_WH_TYPE_NM
        BEFORE_GOODS_RANK_NM
        AFTER_GOODS_RANK_NM
        GOODS_CD
        GOODS_NM
        NB
        LOT_NO
        LT_DATE
        REMARK
        NRS_PROC_NO
        '隠し列
        DEL_KB
        CRT_DATE
        FILE_NAME
        REC_NO
        GYO_NO
        NRS_BR_CD
        PROC_TYPE
        PROC_KBN
        JISSEKI_FUYO
        OUTKA_WH_TYPE
        BEFORE_GOODS_RANK
        AFTER_GOODS_RANK
        YOBI1
        YOBI2
        YOBI3
        YOBI4
        YOBI5
        JISSEKI_SHORI_FLG
        SYS_UPD_DATE
        SYS_UPD_TIME
        LAST

    End Enum
End Class
