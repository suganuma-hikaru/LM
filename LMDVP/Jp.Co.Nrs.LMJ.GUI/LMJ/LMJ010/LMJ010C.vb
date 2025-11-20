' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ010C : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  Shinohara
' ==========================================================================

''' <summary>
''' LMJ010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMJ010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 月末在庫情報取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_GET_GETUDATA As String = "SelectGetuData"

    ''' <summary>
    ''' 実行時のデータ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_GET_CREATE_DATA As String = "SelectCreateData"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMJ010IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMJ010OUT"

    ''' <summary>
    ''' SYS_DATETIMEテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"

    ''' <summary>
    ''' ERRテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' GETU_INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GETU_IN As String = "GETU_IN"

    ''' <summary>
    ''' GETU_OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GETU_OUT As String = "GETU_OUT"

    ''' <summary>
    ''' 処理内容(末締め)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHORI_MATSU As String = "00"

    ''' <summary>
    ''' 処理内容 = 指定された荷主のみチェックする
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHORI_SONOTA As String = "99"

    ''' <summary>
    ''' 締め日基準(10)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIME_10 As String = "10"

    ''' <summary>
    ''' 締め日基準(20)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIME_20 As String = "20"

    ''' <summary>
    ''' 締め日基準(25)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIME_25 As String = "25"

    ''' <summary>
    ''' サーバチェック①
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ERR_01 As String = "01"

    ''' <summary>
    ''' サーバチェック②
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ERR_02 As String = "02"

    ''' <summary>
    ''' 初期在庫(区分)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const START_DATE As String = "01"

    ''' <summary>
    ''' コンボに設定するリストの数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIST_CNT As Integer = 3

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        PRINT = 0
        SEARCH
        SHORI
        EIGYO
        CUSTCDL
        CUSTNML
        CUSTCDM
        CUSTNMM
        SEIQDATE
        SEIQCOMB
        ZAIKO
        SERIAL
        SERIALARI
        SERIALNASHI

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        CREATE = 0
        ZAIKO
        MASTEROPEN
        ENTER
        CLOSE
        CUSTL_LEAVE
        CUSTM_LEAVE

    End Enum

End Class
