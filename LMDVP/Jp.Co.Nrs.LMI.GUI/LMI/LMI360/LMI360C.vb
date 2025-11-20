' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI360  : ＤＩＣ運賃請求明細書作成
'  作  成  者       :  [篠原]
' ==========================================================================

''' <summary>
''' LMI360定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI360C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI360IN"
    Public Const TABLE_NM_OUT As String = "LMI360INOUT"

    'EVENTNAME
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_MAKE As String = "作成"
    'Public Const EVENTNAME_JIKKO As String = "実行" '当PGでは使わない。あとでコメントアウト
    Public Const EVENTNAME_PRINT As String = "印刷"

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
        GRPJIKKO
        CMBJIKKO
        BTNJIKKO
        GRPPRINT
        CMBPRINT
        CMBROSEN
        BTNPRINT

    End Enum


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MASTER = 0
        CLOSE
        MAKE
        'JIKKO
        PRINT

    End Enum

End Class
