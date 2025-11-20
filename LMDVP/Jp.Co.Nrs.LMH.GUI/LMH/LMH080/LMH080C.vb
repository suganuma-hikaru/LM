' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH080C : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMH080定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH080IN"
    Public Const TABLE_NM_OUT As String = "LMH080OUT"
    Public Const TABLE_NM_INDEL As String = "LMH080IN_DEL"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"
    'イベント種別
    Public Enum EventShubetsu As Integer

        KENSAKU = 0                 '検索
        Delete             '一括変更
        CLOSE                   '閉じる
        DOUBLE_CLICK            'ダブルクリック

    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN
        EDIDATEFROM = 0
        EDIDATETO
        GRPSTATUS
        SPREDILIST
    End Enum

    'タブインデックス用列挙体(進捗)
    Public Enum CtlTabIndex_optSTA

        OPTMIKKUNIN = 0
        OPTKAKUNINZUMI
        OPTSOSHINZUMI
        OPTSAKUJOZUMI
        OPTALL
    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0
        DELIV_NO
        GOODS_CD
        GOODS_NM
        QT
        NET_WT
        QT_UT
        LOT_NO
        DEST_NM
        OUTER_PKG
        PKG_UT
        INKA_DATE_YMD
        INKA_NB
        REPLY_DATE
        INKA_CTL_NO_L
        CRT_DATE
        FILE_NAME
        DATA_SEQ
        CUST_CD_L
        CUST_CD_M
        NRS_BR_CD
        DEL_KB
        SYS_UPD_DATE
        SYS_UPD_TIME
    End Enum


End Class
