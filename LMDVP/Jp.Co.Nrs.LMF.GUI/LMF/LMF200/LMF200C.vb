' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF200C : 運行未登録一覧
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMF200定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF200C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMF200IN"
    Public Const TABLE_NM_OUT As String = "LMF200OUT"
    Public Const F_UNSO_L As String = "F_UNSO_L_IN"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        YOSO
        UnsocoCd
        UnsocoBrCd
        UnsocoNm
        ArrDate

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        UNSO_NO
        BINKBN
        UNSO_TEHAI_KBN
        CUST_REF_NO
        ORIG_NM
        DEST_NM
        DEST_ADD    'ADD 2022/08/29
        TASYA_WH_NM
        ARIA
        KOSU
        JURYO
        KANRI_NO
        NONYUDATE
        UNSOCO_NM
        CUST_NM
        REMARK
        UNCHIN
        KYORI
        GROUP_NO
        ONKAN
        MOTO_DATA_KBN
        SHUNI_TI
        HAINI_TI
        TRIP_NO_SHUKA
        TRIP_NO_CHUKEI
        TRIP_NO_HAIKA
        UNSOCO_NM_SHUKA
        UNSOCO_NM_CHUKEI
        UNSOCO_NM_HAIKA

        '隠し項目
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_TIME
        ROW_NO
        UNSO_CD
        UNSO_BR_CD

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MASTEROPEN
        ENTER

    End Enum

End Class
