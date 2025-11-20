' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI380  : 住化カラー実績報告データ作成
'  作  成  者       :  [馬野]
' ==========================================================================

''' <summary>
''' LMI380定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI380C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI380IN"
    Public Const TABLE_NM_OUT As String = "LMI380OUT"

    'EVENTNAME
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_JIKKO As String = "実行"

    '初期荷主コード（群馬：住化カラー）
    Public Const NRS_GNM As String = "30"
    Public Const DEF_CUST_CD_L_GNM As String = "00952"
    Public Const DEF_CUST_CD_M_GNM As String = "00"

    '初期荷主コード（千葉：住化カラー）
    Public Const NRS_CBA As String = "10"
    Public Const DEF_CUST_CD_L_CBA As String = "00002"
    Public Const DEF_CUST_CD_M_CBA As String = "00"


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
