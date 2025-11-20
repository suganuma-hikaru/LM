' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタメンテ
'  プログラムID     :  LMM350C : 初期出荷元マスタ
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMM350定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMM350C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMM350IN"
    Public Const TABLE_NM_OUT As String = "LMM350OUT"
    Public Const TABLE_NM_UPDATE As String = "LMM350IN_UPDATE"

    ''' <summary>
    ''' 閾値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMITED_COUNT As Integer = 500

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        KENSAKU = 0
        SETTEI
        ENTER
        DCLICK


    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_KENSAKU = 0
        CMB_NINUSHI
        CHK_MISETTEI
        TXT_YUBIN_BANGO
        GRP_SETTEI
        CMB_SOKO
        BTN_SETTEI
        SPR_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        CHECK = 0
        JIS_CD
        JIS_NM
        SOKO_CD
        SOKO_NM
        UPD_FLG
        UPD_DATE
        UPD_TIME

    End Enum

    ''' <summary>
    ''' SCM荷主コードBPコード値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ScmCustCdBP As String = "00001"

End Class
