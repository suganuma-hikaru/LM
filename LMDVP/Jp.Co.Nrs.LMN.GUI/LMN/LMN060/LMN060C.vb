' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060C   : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMN060定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMN060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMN060IN"
    Public Const TABLE_NM_OUT_HDR As String = "LMN060OUT_HDR"
    Public Const TABLE_NM_OUT As String = "LMN060OUT"
    Public Const TABLE_NM_OUT_ZAIKO As String = "LMN060OUT_ZAIKO"

    ''' <summary>
    ''' 荷主コンボボックスの初期値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INIT_NINUSHI As String = "00"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_SEARCH = 0
        NINUSHI
        KEPPIN_ONLY
        DISP_SOKO
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
        SPACE
        SOKO_CD
        BR_CD

    End Enum

End Class
