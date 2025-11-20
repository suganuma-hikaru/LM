' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI180定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI180C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI180IN"
    Public Const TABLE_NM_OUT As String = "LMI180OUT"

    'EVENTNAME
    Public Const EVENTNAME_TORIKOMI As String = "取込"
    Public Const EVENTNAME_HOZON As String = "保存"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_EXCEL As String = "Excel出力"
    Public Const EVENTNAME_ROWADD As String = "行追加"
    Public Const EVENTNAME_ROWDEL As String = "行削除"

    '状態区分
    Public Const JOTAIKB_SEIJO As String = "01"
    Public Const JOTAIKB_GAITONASHI As String = "02"
    Public Const JOTAIKB_JYUFUKU As String = "03"
    Public Const JOTAIKB_ERR As String = "99"

    '状態名
    Public Const JOTAI_SEIJO As String = ""
    Public Const JOTAI_GAITONASHI As String = "該当なし"
    Public Const JOTAI_JYUFUKU As String = "重複"
    Public Const JOTAI_ERR As String = "エラー"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPMODE = 0
        OPTSHUKKA
        OPTKAISHU
        OPTTORIKESHI
        GRPEXCEL
        HOKOKUDATEFROM
        HOKOKUDATETO
        BTNEXCEL
        OUTKANOL
        SERIALNOFROM
        SERIALNOTO
        KAISHUDATE
        BTNROWADD
        BTNROWDEL
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        TORIKOMI = 0
        HOZON
        CLOSE
        EXCEL
        ROWADD
        ROWDEL
        CHANGEOPTBUTTOM
        CHANGEOUTKANOL
        '要望番号:1917 yamanaka 2013.03.06 Start
        FILESELECT
        '要望番号:1917 yamanaka 2013.03.06 End

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SERIALNO
        JOTAI
        NRCRECNO
        OUTKANOL
        EDANO
        TOROKUKB
        HOKOKUDATE
        LAST

    End Enum

End Class
