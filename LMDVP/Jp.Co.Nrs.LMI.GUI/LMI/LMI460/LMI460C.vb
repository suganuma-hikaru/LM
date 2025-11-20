' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI460  : ローム　請求コード変更（YCC)
'  作  成  者       :  [daikoku]
' ==========================================================================

''' <summary>
''' LMI460定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI460C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI460IN"
    Public Const TABLE_NM_OUT As String = "LMI460OUT"

    'EVENTNAME
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_JIKKO As String = "実行"

    '初期荷主コード（横浜：ロームアンドハーツ）
    Public Const NRS_YKO As String = "40"
    Public Const DEF_CUST_CD_L_YKO As String = "00061"
    Public Const DEF_CUST_CD_M_YKO As String = "00"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPOUTPUTTANI = 0
        ONEEIGYO
        ONECUST
        CMBEIGYO
        TXTCUSTCDL
        TXTCUSTCDM
        GRPOUTPUTTAISYO
        ALLJISSEKI
        OUTKAPLANJISSEKI
        OUTKAPLANDATEFROM
        OUTKAPLANDATETO
        GRPJIKKO
        CMBJIKKO
        BTNJIKKO

    End Enum


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MASTER = 0
        CLOSE
        JIKKO

    End Enum

End Class
