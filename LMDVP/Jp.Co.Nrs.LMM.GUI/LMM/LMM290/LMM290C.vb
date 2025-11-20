' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM290C : 振替対象商品マスタメンテ
'  作  成  者       :  [kisi]
' ==========================================================================

''' <summary>
''' LMM290定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM290C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM290IN"
    Public Const TABLE_NM_OUT As String = "LMM290OUT"

    'メッセージ置換文字
    Public Const GOODSMSG As String = "商品コード(振替元)"
    Public Const MSGL As String = "振替元の荷主コード(大)"
    Public Const MSGLTO As String = "振替先の荷主コード(大)"
    Public Const MSGGOODS As String = "振替元か振替先の商品コード"
    Public Const MSGIRIME As String = "振替元の入目"
    Public Const MSGIRIMETO As String = "振替先の入目"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MasterRow

        CDL = 0
        NML
        CDM
        NMM
        CDS
        NMS
        CDSS
        NMSS
        CDCUST
        CDNRS
        NM1
        PKG
        IRIME
        HIDCDL

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        CUSTCDL
        CUSTCDM
        CDCUST
        CUSTCDLTO
        CUSTCDMTO
        CDCUSTTO
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum


    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        CUST_CD_L
        CUST_NM_L
        GOODS_CD_CUST
        GOODS_NM_1
        CUST_CD_L_TO
        CUST_NM_L_TO
        GOODS_CD_CUST_TO
        GOODS_NM_1_TO
        CUST_CD_M
        CUST_NM_M
        CUST_CD_S
        CUST_NM_S
        CUST_CD_SS
        CUST_NM_SS
        CD_NRS
        PKG_UT_NM
        STD_IRIME_NB
        STD_IRIME_UT_NM
        STD_IRIME
        CUST_CD_M_TO
        CUST_NM_M_TO
        CUST_CD_S_TO
        CUST_NM_S_TO
        CUST_CD_SS_TO
        CUST_NM_SS_TO
        CD_NRS_TO
        PKG_UT_NM_TO
        STD_IRIME_NB_TO
        STD_IRIME_UT_NM_TO
        STD_IRIME_TO
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

End Class
