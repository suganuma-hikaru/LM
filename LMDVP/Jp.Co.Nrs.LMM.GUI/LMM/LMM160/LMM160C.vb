' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM160C : 届先商品マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================

''' <summary>
''' LMM160定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM160C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM160IN"
    Public Const TABLE_NM_OUT As String = "LMM160OUT"

    '作業項目数(POP)
    Public Const SAGYO_CNT As Integer = 1

    'メッセージ置換文字
    Public Const CUSTCDLMSG As String = "荷主コード（大）"
    Public Const DESTCDMSG As String = "届先コード"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
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
    Public Enum CtlTabIndex

        DETAIL = 0
        CUSTCDL
        CUSTNML
        CUSTCDM
        CUSTNMM
        DESTCD
        DESTNM
        GOODSCD
        GOODSNM
        GOODSNRS
        DELVERGOODSNM
        WORKDEMANDCD
        WORKDEMANDNM
        WORK1KB
        WORK1NM
        WORK2KB
        WORK2NM
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
        CUST_CD_M
        CUST_NM_L
        CUST_NM_M
        CD
        DEST_NM
        GOODS_CD
        GOODS_NM
        SAGYO_KB_1
        SAGYO_KB_2
        SAGYO_SEIQTO_CD
        SAGYO_SEIQTO_NM
        SAGYO_NM_1
        SAGYO_NM_2
        GOODS_CD_NRS
        GOODS_NM_1
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_USER_TIME
        SYS_DEL_FLG

    End Enum

End Class

