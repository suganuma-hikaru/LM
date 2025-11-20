' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMH060定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH060IN"
    Public Const TABLE_NM_OUT As String = "LMH060OUT"

    'FunctionKey
    Public Const FUNCTION_SET As String = "荷主セット"
    Public Const FUNCTION_CANCEL As String = "キャンセル"
    Public Const FUNCTION_KENSAKU As String = "検　索"
    Public Const FUNCTION_MASTER As String = "マスタ参照"
    Public Const FUNCTION_SAVE As String = "登　録"
    Public Const FUNCTION_CLOSE As String = "閉じる"

    'EVENTNAME
    Public Const EVENTNAME_SET As String = "荷主セット"
    Public Const EVENTNAME_CANCEL As String = "キャンセル"
    Public Const EVENTNAME_KENSAKU As String = "検索"
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_SAVE As String = "登録"
    Public Const EVENTNAME_CLOSE As String = "閉じる"

    'X固定荷主コード
    Public Const CUST_CD_LX As String = "XXXXX"
    Public Const CUST_CD_MX As String = "XX"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPSERCH = 0
        EIGYO
        CUSTCDL
        CUSTCDM
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        NINUSHISET = 0
        CANCEL 
        KENSAKU
        MASTER
        HOZON
        CLOSE

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex As Integer

        DEF = 0
        CUSTCD        '荷主コード
        CUSTNM        '荷主名
        EDICTLNO      'EDI番号
        OUTKAPLANDATE '出荷予定日
        DESTCD        '届先コード
        DESTNM        '届先名
        ZBUKACD       '在庫部課コード
        CUSTCDUPD     '対象荷主コード
        RCVNMHED      '更新対象テーブル名
        SYSUPDDATE    '更新日
        SYSUPDTIME    '更新時間
        LAST

    End Enum

End Class
