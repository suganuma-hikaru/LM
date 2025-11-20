' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB050C : 入荷検品取込
'  作  成  者       :  菊池
' ==========================================================================

''' <summary>
''' LMB050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB050IN"
    Public Const TABLE_NM_OUT As String = "LMB050OUT"

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        SOKO = 0
        SYS_ENT_DATE
        CHK_MISHORI
        SPRDETAIL

    End Enum

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0

        STATE
        BUYER_ORD_NO_L
        GOODS_NM
        GOODS_CD_CUST
        STD_IRIME_NB
        STD_IRIME_UT
        JISSEKI_INKA_NB
        NB_UT
        JISSEKI_INKA_QT
        IRIME
        JISSEKI_PKG_UT
        PKG_NB
        PKG_UT
        LOT_NO
        REMARK_L
        REMARK_M
        TOU_NO
        SITU_NO
        ZONE_CD
        LOCA
        OFB_KB
        OUTKA_FROM_ORD_NO_L
        CRT_DATE
        GOODS_CD_NRS
        ONDO_KB
        ONDO_STR_DATE
        ONDO_END_DATE
        SYS_UPD_DATE
        SYS_UPD_TIME
        M_GOODS_COUNT
        M_GOODS_UT_NB_COUNT
        M_ZONE_COUNT
        STD_WT_KGS

    End Enum


End Class
