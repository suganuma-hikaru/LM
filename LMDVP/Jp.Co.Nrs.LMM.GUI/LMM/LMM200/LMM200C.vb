' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM200C : 車輌マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM200定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM200C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM200IN"
    Public Const TABLE_NM_OUT As String = "LMM200OUT"

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        UNSOCO_CD
        UNSOCO_BR_CD
        UNSOCO_NM
        UNSOCO_BR_NM
        CAR_NO
        INSPC_DATE_TRUCK
        AVAL_YN
        AVAL_YN_NM
        VCLE_KB
        VCLE_KB_NM
        CAR_KEY
        TRAILER_NO
        JSHA_KB
        CAR_TP_KB
        LOAD_WT
        TEMP_YN
        ONDO_MM
        ONDO_MX
        FUKUSU_ONDO_YN
        INSPC_DATE_TRAILER
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        '2022.09.06 追加START
        DAY_UNCHIN
        '2022.09.06 追加END


        


    End Enum

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
        VALUECHANGED1
        VALUECHANGED2

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        CARKEY
        CARTPKB
        GRPFRONT
        CARNO
        INSPCDATETRUCK
        GRPBACK
        TRAILERNO
        INSPCDATETRAILER
        AVALYN
        UNSOCOCD
        UNSOCOBRCD
        UNSOCONM
        JSHAKB
        VCLEKB
        LOADWT
        TEMPYN
        ONDOMM
        ONDOMX
        FUKUSUONDOYN
        '2022.09.06 追加START
        DAYUNCHIN
        '2022.09.06 追加END
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG


    End Enum

End Class
