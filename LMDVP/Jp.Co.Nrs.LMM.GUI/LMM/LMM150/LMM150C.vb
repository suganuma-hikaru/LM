' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM150C : 倉庫マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM150定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM150C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM150IN"
    Public Const TABLE_NM_OUT As String = "LMM150OUT"

    Public Const JITASHAFLG As String = "01"
    Public Const UMUFLG As String = "00"

    Public Const SOKOMSG As String = "倉庫コード"

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
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        WHCD
        WHNM
        ZIP
        AD1
        AD2
        AD3
        JITASHAFLG
        TEL
        FAX
        JIS
        KEN
        SHI
        MXCNT
        STAGE
        INKAYOTEI
        INKAUKEPRT
        INKAKENPIN
        INKAKAKUNIN
        INKAINFO
        OUTKAYOTEI
        OUTKASASHIZUPRT
        OUTKAKANRYO
        OUTKAKENPIN
        OUTKAINFO
        CHK
        LOCMANAGER
        TOUKANRI
        TOUHANSASHIZU
        GOODSLOTCHECKYN
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
        WH_CD
        WH_NM
        TEL
        FAX
        ZIP
        AD_1
        AD_2
        AD_3
        WH_KB
        JIS_CD
        KEN
        SHI
        NIHUDA_MX_CNT
        INKA_YOTEI_YN
        INKA_UKE_PRT_YN
        INKA_KENPIN_YN
        INKA_KAKUNIN_YN
        INKA_INFO_YN
        OUTKA_YOTEI_YN
        OUTKA_SASHIZU_PRT_YN
        OUTOKA_KANRYO_YN
        OUTKA_KENPIN_YN
        OUTKA_INFO_YN
        LOC_MANAGER_YN
        TOU_KANRI_YN
        TOUHAN_SASHIZU_YN
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        GOODSLOT_CHECK_YN

    End Enum

End Class
