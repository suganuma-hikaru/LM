' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020C   : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMN020定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMN020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMN020IN"
    Public Const TABLE_NM_OUT_HDR As String = "LMN020OUT_L"
    Public Const TABLE_NM_OUT_DTL As String = "LMN020OUT_M"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SAKUJHO = 0
        SHOKIKA

    End Enum

    ''' <summary>
    '''【区分マスタ】S031 ステータスコード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STATUS_MISETTEI As String = "00"
    Public Const STATUS_SETTEIZUMI As String = "01"
    Public Const STATUS_SOKOSIJIZUMI As String = "02"
    Public Const STATUS_JISSEKIHOKOKUZUMI As String = "03"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_SHUKKA = 0
        SOKO
        STASUS
        NINUSHI
        ORDER_NO
        MOSHIOKURI
        SHUKKA_BI
        NONYU_BI
        EDI_TORIKOMI
        TODOKESAKI_NM
        TODOKESAKI_ADD
        TODOKESAKI_ADD_NM
        TODOKESAKI_TEL
        BIKO
        SPR_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        CHECK = 0
        SHOHIN_CD
        SHOHIN_NM
        SHUKKA_KOSU
        ZAIKO_KOSU
        BIKO

    End Enum

End Class
