' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMI     : データ管理サブ
'  プログラムID     :  LMI020C : デュポン在庫
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 月末在庫情報取得アクション(初期表示)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_LOAD_DATA As String = "SelectLoadData"

    ''' <summary>
    ''' 月末在庫情報取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_GET_GETUDATA As String = "SelectGetuData"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "CREATE_IN"

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
    ''' COUNTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_COUNT As String = "COUNT"

    ''' <summary>
    ''' データセットテーブル名(SYS_DATETIMEテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"

    'START YANAI 要望番号410
    '''' <summary>
    '''' 月末在庫
    '''' </summary>
    '''' <remarks></remarks>
    'Public Const END_DATE As String = "99999999"
    'END YANAI 要望番号410

    ''' <summary>
    ''' 印刷種別(日次在庫報告用データ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_NITHIJI As String = "10"

    ''' <summary>
    ''' 印刷種別(在庫証明書作成)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_ZAIKO As String = "20"

    ''' <summary>
    ''' 印刷種別(報告データ作成（SFTP）)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_SFTP As String = "30"

    ''' <summary>
    ''' 営業所(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_TIBA As String = "10"

    ''' <summary>
    ''' 営業所(大阪)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_OSAKA As String = "20"

    ''' <summary>
    ''' 営業所(横浜)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD_YOKOHAMA As String = "40"

    ''' <summary>
    ''' プラントコードの初期値(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PLANT_TIBA As String = "C1"

    ''' <summary>
    ''' プラントコードの初期値(大阪)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PLANT_OSAKA As String = "O2"

    ''' <summary>
    ''' プラントコードの初期値(横浜)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PLANT_YOKOHAMA As String = "Y1"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        PRINT = 0
        PLANTCD
        PLANTNM
        SEARCH
        EIGYO
        CUSTCDL
        CUSTNML
        CUSTCDM
        CUSTNMM
        CUSTCDS
        CUSTNMS
        HOKOKUDATE
        ZAIRIREKIDATE
        CUST
        CUSTDTL

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        CREATE = 0
        MASTEROPEN
        ENTER
        CLOSE
        CUSTL_LEAVE
        CUSTM_LEAVE

    End Enum

End Class
