' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC830  : 日立物流音声データCSV作成
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMC830定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC830C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMC830IN"
    Public Const TABLE_NM_OUT_CSV As String = "LMC830OUT_CSV"
    Public Const TABLE_NM_OUT_CSV_DEL As String = "LMC830OUT_CSV_DEL"

    '区分コード
    Public Const KBN_CD_OUTKA As String = "00"
    Public Const KBN_CD_CANCEL As String = "01"

    '印刷種別
    Public Const OUTKA_SASHIZU As String = "05"

    'RPT_ID
    Public Const RPT_ID_OUTKA As String = "SDP0020"
    Public Const RPT_ID_CANCEL As String = "SDP0090"

End Class
