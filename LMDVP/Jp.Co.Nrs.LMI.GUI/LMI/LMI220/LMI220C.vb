' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMI220C : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================

''' <summary>
''' LMI220定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI220C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI220IN"
    Public Const TABLE_NM_OUT As String = "LMI220OUT"

    'コンボボックス初期値
    Public Const LARGECAR As String = "01"

    '荷主マスタ存在チェックデフォルトコード
    Public Const NINUSHI As String = "00"

    Public Const MODE_SHOKI As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"

    'ExcelFile ColIndex
    Public Const COL_SERIAL_NO As Integer = 1
    Public Const COL_LAST_TEST_DATE As Integer = 2
    Public Const COL_NEXT_TEST_DATE As Integer = 3
    'ガイダンス区分(00)
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SHOKI = 0
        SHINKI
        HENSHU
        SAKUJO_HUKKATSU
        KENSAKU
        HOZON
        CLOSE
        DOUBLECLICK
        ENTER
        TORIKOMI_KOSHIN

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRS_BR_CD
        SERIAL_NO
        SIZE
        PROD_DATE
        LAST_TEST_DATE
        NEXT_TEST_DATE
        SYS_UPD_DATE
        SYS_UPD_TIME
        SYS_DEL_FLG
        SYSDELFLG
        LAST

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        SYS_DEL_NM = 0
        SERIAL_NO
        SIZE_NM
        PROD_DATE
        LAST_TEST_DATE
        NEXT_TEST_DATE
        NRS_BR_CD
        SYS_ENT_DATE
        SYS_ENT_TIME
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_TIME
        SYS_UPD_USER_NM
        SYS_DEL_FLG
        SIZE
        LAST

    End Enum

End Class
