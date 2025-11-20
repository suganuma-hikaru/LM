' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求
'  プログラムID     :  LMI040C : 請求データ編集[デュポン用]
'  作  成  者       :  [HISHI]
' ==========================================================================

''' <summary>
''' LMI040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI040IN"
    Public Const TABLE_NM_OUT As String = "LMI040OUT"
    Public Const TABLE_NM_CSV_UNCHIN As String = "LMI040_UNCHINOUT"
    Public Const TABLE_NM_CSV_GL As String = "LMI040_GL"
    Public Const TABLE_NM_CSV_FPDE_HIKAZEI As String = "LMI040_FPDE_GL_HIKAZEI"
    Public Const TABLE_NM_CSV_FPDE_KAZEI As String = "LMI040_FPDE_GL_KAZEI"
    Public Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"

    'EVENTNAME
    Public Const EVENTNAME_SINKI As String = "新規"
    Public Const EVENTNAME_HENSHU As String = "編集"
    Public Const EVENTNAME_FUKUSHA As String = "複写"
    Public Const EVENTNAME_DEL As String = "削除"
    Public Const EVENTNAME_KENSAKU As String = "検索"
    Public Const EVENTNAME_HOZON As String = "保存"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_PRINT As String = "印刷"
    Public Const EVENTNAME_FILEMAKE As String = "ファイル作成"

    Public Const KINGAKU_MAX As String = "9999999999"
    'START YANAI 要望番号830
    Public Const KINGAKU_SUM_MAX As String = "999999999999"
    'END YANAI 要望番号830

    Public Const PRINT_CHECK_LIST As String = "01"
    Public Const PRINT_SEKY_KAGAMI As String = "02"
    Public Const PRINT_SEKY_SHUKEI As String = "03"
    Public Const PRINT_SEKY_SHUKEIKEIRI As String = "04"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        SEIKYUDATEKENSAKU
        MISUKUKENSAKU
        ZIGYOKENSAKU
        SEIKYUKENSAKU
        PRINT
        'START YANAI 要望番号830
        PRINTTYPE10
        PRINTTYPE11
        PRINTTYPE12
        PRINTTYPE13
        PRINTTYPE20
        PRINTTYPE21
        'END YANAI 要望番号830
        BTNPRINT
        FILESYUBETSU
        BTNFILE
        SPRDETAILS
        SEIKYUDATE
        ZIGYO
        SEIKYU
        FRBCD
        SRCCD
        COSTCENTER
        MISKCD
        DESTCITY
        KAZEI
        HIKAZEI
        ZEIGAKU

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SHOKI = 0
        SINKI
        HENSHU
        FUKUSHA
        DEL
        KENSAKU
        HOZON
        CLOSE
        PRINT
        FILEMAKE
        DOUBLECLICK
        SEIKYUKEISAN

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        TUKI
        JIGYOU
        SEIKYU
        FRBCD
        SRCCD
        COST
        MISKNM
        DESTCITY
        AMOUNT
        SOUND
        BOND
        VATAMOUNT
        JIDOU
        SHUDOU
        'START YANAI 要望番号830
        JIDOUFLG
        SHUDOUFLG
        'END YANAI 要望番号830
        'START YANAI 要望番号830
        'SYSENTDATE
        'SYSENTUSER
        'SYSUPDDATE
        'SYSUPDTIME
        'SYSUPDUSER
        NRSBRNM
        SYSENTDATE
        SYSENTUSER
        SYSUPDDATE
        SYSUPDUSER
        SYSUPDTIME
        'END YANAI 要望番号830
        SYSDELFLG
        NRSBRCD
        JIGYOUCD
        SEIKYUCD
        MISKCD
        LAST

    End Enum
End Class
