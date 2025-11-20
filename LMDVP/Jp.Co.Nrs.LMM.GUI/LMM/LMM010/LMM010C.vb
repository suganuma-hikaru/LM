' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM010C : ユーザーマスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================

''' <summary>
''' LMM010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMM010IN"
    Public Const TABLE_NM_OUT As String = "LMM010OUT"
    'Public Const TABLE_NM_OUT2 As String = "LMM010_TCUST"
    Public Const TABLE_NM_TCUST_MAXEDA As String = "LMM010_TCUST_MAXEDA"

    '要望番号:1248 yamanaka 2013.03.21 Start
    Public Const TABLE_NM_OUT_TUNSOCO As String = "LMM010_TUNSOCO"
    Public Const TABLE_NM_OUT_TTARIFF As String = "LMM010_TUNCHIN_TARIFF"
    '要望番号:1248 yamanaka 2013.03.21 End

    'コンボボックス初期値
    Public Const INKADATEINIT As String = "02"
    Public Const OUTKADATEINIT As String = "03"

    '入力チェック用文字列
    Public Const DEFAULT_CUST_Y As String = "○"

    '有無区分
    Public Const UMU_FLG_ON As String = "01"
    Public Const UMU_FLG_OFF As String = "00"

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
        INS_T           '行追加
        DEL_T           '行削除
        DEF_T           '初期荷主

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        USERID
        USERNM
        EMAIL
        NOTICE
        AUTHOLV
        RIYOUSHAKBN
        WARE
        SAP_LINK_AUTHO
        BUSYOCD
        PW
        INKADATEINIT
        OUTKADATEINIT
        ROWADD
        ROWDEL
        DEFAULTCUST
        DETAIL2
        PRT
        DEFPRT1
        DEFPRT2
        'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
        DEFPRT3
        DEFPRT4
        DEFPRT5
        DEFPRT6
        DEFPRT7
        'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
        DEFPRT8
        COAPRT
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum

    ''' <summary>
    ''' Spread部(上部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        USER_ID
        USER_NM
        AUTHO_LV
        AUTHO_LV_NM
        RIYOUSHA_KBN
        RIYOUSHA_KBN_NM
        EMAIL
        NOTICE
        PW
        WH_CD
        WH_NM
        BUSYO_CD
        BUSYO_NM
        INKA_DATE_INIT
        INKA_DATE_INIT_NM
        OUTKA_DATE_INIT
        OUTKA_DATE_INIT_NM
        DEF_PRT1
        DEF_PRT2
        'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
        DEF_PRT3
        DEF_PRT4
        DEF_PRT5
        DEF_PRT6
        DEF_PRT7
        'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
        DEF_PRT8
        COA_PRT
        YELLOW_CARD_PRT
        SAP_LINK_AUTHO
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
        LAST
        'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

    End Enum

    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        CUST_CD_L
        CUST_CD_M
        CUST_NM_L
        CUST_NM_M
        DEF_CUST
        DEFAULT_CUST_YN
        USER_CD_T
        USER_CD_EDA
        UPD_FLG
        SYS_DEL_FLG_T
        'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
        LAST
        'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

    End Enum

    '要望番号:1248 yamanaka 2013.03.19 Start
    ''' <summary>
    ''' Spread部(My運送会社)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex3

        DEF = 0
        UNSOCO_CD
        UNSOCO_BR_CD
        UNSOCO_NM
        UNSOCO_BR_NM

        '隠し項目
        USER_CD_T
        USER_CD_EDA
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

    ''' <summary>
    ''' Spread部(My運賃タリフ)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex4

        DEF = 0
        UNCHIN_TARIFF
        REMARK

        '隠し項目
        USER_CD_T
        USER_CD_EDA
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

    '要望番号:1248 yamanaka 2013.03.19 End

End Class
