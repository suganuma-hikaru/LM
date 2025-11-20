' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMN010C : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================

''' <summary>
''' LMN010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMN010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMN010IN"
    Public Const TABLE_NM_OUT As String = "LMN010OUT"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

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

        SOUSHIN_SHIJI = 0
        KEPPIN_SHOUKAI
        KEPPIN_JOUTAI_UPD
        KENSAKU
        SETTEI
        JISSEKI_TORIKOMI
        JISSEKI_SOUSHIN
        SPREAD_DOUBLE_CLICK
        TOJIRU

    End Enum

    ''' <summary>
    ''' ステータス区分コード値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KbnCdMisettei As String = "00"
    Public Const KbnCdSetteiZumi As String = "01"
    Public Const KbnCdSokoShijiZumi As String = "02"
    Public Const KbnCdJissekiSoushinZumi As String = "03"

    ''' <summary>
    ''' SCM荷主コードBPコード値
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ScmCustCdBP As String = "00001"

End Class
