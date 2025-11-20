' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI470  : 日本合成　物流費データ送信
'  作  成  者       :  [daikoku]
' ==========================================================================

''' <summary>
''' LMI470定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI470C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI470IN"
    Public Const TABLE_NM_OUT As String = "LMI470OUT"

    'EVENTNAME
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_JIKKO As String = "実行"

    '初期荷主コード（中部：日本合成）
    Public Const NRS_60 As String = "60"
    Public Const DEF_SEIKYU_CD_L_60 As String = "3251699"

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
