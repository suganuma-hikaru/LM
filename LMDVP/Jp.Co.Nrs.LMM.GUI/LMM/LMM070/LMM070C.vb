' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM070C : 割増運賃マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM070IN"
    Public Const TABLE_NM_OUT As String = "LMM070OUT"

    'メッセージ用
    Public Const TOKIFROM As String = "冬期期間(FROM)"
    Public Const TOKITO As String = "冬期期間(TO)"

    ' 親レコード(JIS)
    Public Const OYA_JIS As String = "0000000"

    'START YANAI 要望番号377
    '10kgあたりの航送料桁数
    Public Const NB_MAX_9 As String = "999999999"
    'END YANAI 要望番号377

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        EXTC_TARIFF_CD
        EXTC_TARIFF_REM
        KEN
        SHI
        DATA_TYPE
        DATA_TYPE_NM
        JIS_CD
        WINT_KIKAN_FROM
        WINT_KIKAN_TO
        WINT_EXTC_YN
        WINT_EXTC_YN_NM
        CITY_EXTC_YN
        CITY_EXTC_YN_NM
        RELY_EXTC_YN
        RELY_EXTC_YN_NM
        FRRY_EXTC_YN
        FRRY_EXTC_YN_NM
        'START YANAI 要望番号377
        FRRY_EXTC_10KG
        'END YANAI 要望番号377
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_TIME
        SYS_UPD_USER_NM
        SYS_DEL_FLG
        OYA_DATE
        OYA_TIME
        OYA_SYS_DEL_FLG




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
        VALUECHANGED
        VALUECHANGED2
        VALUECHANGED3

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        EXTCTARIFFCD
        DATATYPE
        EXTCTARIFFREM
        JISCD
        WINTEXTCYN
        WINTKIKANFROM
        WINTKIKANTO
        CITYEXTCYN
        RELYEXTCYN
        FRRYEXTCYN
        'START YANAI 要望番号377
        FRRYEXTC10KG
        'END YANAI 要望番号377
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG
        OYADATE
        OYATIME
        OYASYSDELFLG


    End Enum

End Class
