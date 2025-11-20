' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI150C : 物産アニマルヘルス倉庫内処理編集
'  作  成  者       :  [HORI]
' ==========================================================================

''' <summary>
''' LMI150定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI150C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI150IN"
    Public Const TABLE_NM_OUT As String = "LMI150OUT"
    Public Const TABLE_NM_H_WHEDI_BAH As String = "LMI150_H_WHEDI_BAH"
    Public Const TABLE_NM_NRS_PROC_NO As String = "NRS_PROC_NO"

    'インベント表示名
    Public Const EVENTNAME_HENSHU As String = "編　集"
    Public Const EVENTNAME_HOZON As String = "保　存"
    Public Const EVENTNAME_CLOSE As String = "閉じる"

    Public Const KINGAKU_MAX As String = "9999999999"
    Public Const KINGAKU_SUM_MAX As String = "999999999999"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        NRS_PROC_NO = 0
        NRS_BR_CD
        JISSEKI_FUYO
        JISSEKI_SHORI_FLG
        PROC_TYPE
        PROC_KBN
        PROC_DATE
        GRP_WH
        OUTKA_WH_TYPE
        OUTKA_CUST_CD
        OUTKA_CUST_NM
        INKA_WH_TYPE
        INKA_CUST_CD
        INKA_CUST_NM
        GRP_GOODS_RANK
        BEFORE_GOODS_RANK
        AFTER_GOODS_RANK
        BTN_ZAIKO_SEL
        GOODS_CD
        GOODS_CD_NRS
        GOODS_NM
        LOT_NO
        NB
        LT_DATE
        REMARK
        GRP_OBIC
        OBIC_SHUBETU
        OBIC_TORIHIKI_KBN
        OBIC_DENP_NO
        OBIC_GYO_NO
        OBIC_DETAIL_NO

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SHOKI = 0
        VIEW_ONLY
        VIEW
        HENSHU
        HOZON
        CLOSE
        DOUBLECLICK
        ZAIKO_SEL

    End Enum

End Class
