' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LME030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LME030IN"
    Public Const TABLE_NM_OUT As String = "LME030OUT"

    'FunctionKey
    Public Const FUNCTION_SINKI As String = "新　規"
    Public Const FUNCTION_KENSAKU As String = "検　索"
    Public Const FUNCTION_MASTER As String = "マスタ参照"
    Public Const FUNCTION_CLOSE As String = "閉じる"
    Public Const FUNCTION_COMPLETE As String = "完　了"

    'EVENTNAME
    Public Const EVENTNAME_SINKI As String = "新規"
    Public Const EVENTNAME_KENSAKU As String = "検索"
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_COMPLETE As String = "完　了"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPSERCH = 0
        EIGYO
        CUSTCDL
        CUSTCDM
        DATEFROM
        DATETO
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SINKI = 0
        KENSAKU
        MASTER
        CLOSE
        DOUBLECLICK
        JIKKOU
        COMPLETE

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex As Integer

        DEF = 0
        SAGYOSIJINO   '作業指示書番号
        SAGYOSIJISTATUSNM '作業進捗
        SAGYODATE     '作業日
        CUSTNM        '荷主名
        GOODSNM       '商品名
        WHTABSTATUSNM '現場作業指示ステータス
        SAGYONM       '作業内容
        USERNM        '作成者
        NRSBRCD       '営業所
        CUSTCDL       '荷主コード(大)
        CUSTCDM       '荷主コード(中)
        SYSUPDDATE    '更新年月日
        SYSUPDTIME    '更新時間
        SAGYOSIJISTATUS '作業進捗
        LAST

    End Enum

End Class
