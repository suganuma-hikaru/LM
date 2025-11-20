' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI050IN"
    Public Const TABLE_NM_OUT_SNZ As String = "LMI050OUT_SNZ"

    'EVENTNAME
    Public Const EVENTNAME_JIKKO As String = "実行"
    Public Const EVENTNAME_CLOSE As String = "閉じる"

    '日付フラグ
    Public Const DATE_FLG00 As String = "00"
    Public Const DATE_FLG98 As String = "98"
    Public Const DATE_FLG99 As String = "99"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPSERCH = 0
        EIGYO
        CUST
        JIKKODATE
        CHKEDI
        CHKEXCEL
        CHKMAIL
        GRPZAIKO
        OPTZAIKO
        OPTZAIZAN

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        JIKKO = 0
        CLOSE
        CHANGECUST

    End Enum

End Class
