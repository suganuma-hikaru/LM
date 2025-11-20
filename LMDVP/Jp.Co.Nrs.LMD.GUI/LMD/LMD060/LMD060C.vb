' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD060C : 月末在庫履歴作成
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMD060定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD060IN"
    Public Const TABLE_NM_OUT As String = "LMD060OUT"
    'Public Const TABLE_NM_OUT_PRE As String = "LMD060OUT_PRE"
    Public Const TABLE_NM_IN_DEL As String = "LMD060IN_DEL"
    Public Const TABLE_NM_INOUT_JIKKOU As String = "LMD060_ZAIJITSU"

    '''' <summary>
    '''' 在庫履歴データ検索アクション（サブ）
    '''' </summary>
    '''' <remarks></remarks>
    'Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 在庫履歴データ検索アクション（メイン）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 排他チェックアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_HAITA As String = "ChkHaitaData"

    ''' <summary>
    ''' 在庫履歴データ削除アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_DELETE As String = "DeleteAction"

    ''' <summary>
    ''' 実行アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_JIKKOU As String = "JikkouAction"

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        DELETE = 0
        KENSAKU
        REKENSAKU
        MASTEROPEN
        ENTER
        JIKKOU
        CLOSE

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        TANTOU_CD
        TANTOU_NM
        CUST_CDL
        CUST_CDM
        CUST_NM
        SIMEBI_KB
        ZAIRIREKIDATE
        SPR

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        CUST_CD
        CUST_NM
        CLOSE_KB_NM
        CULC_DATE
        TANTO_NM
        ZAI_REC_NO_1
        CUST_CD_L
        CUST_CD_M
        CLOSE_KB
        SYS_UPD_DATE
        SYS_UPD_TIME
        RIREKI_1
        LAST

    End Enum

    ''' <summary>
    ''' 担当者
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TANTOU As String = "txtTantouCd"

    ''' <summary>
    ''' 担当者名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TANTOU_NM As String = "lblTantouNM"

    ''' <summary>
    ''' 荷主コード（大）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUST_CDL As String = "txtCustCdL"

    ''' <summary>
    ''' 荷主コード（中）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUST_CDM As String = "txtCustCdM"

    ''' <summary>
    ''' 荷主名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUST_NM As String = "lblCustNm"

    ''' <summary>
    ''' 締め日区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SIMEBI_KB As String = "cmbSimebi"

    ''' <summary>
    ''' 在庫履歴作成日
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ZAIRIREKIDATE As String = "imdZaiRirekiDate"


End Class
