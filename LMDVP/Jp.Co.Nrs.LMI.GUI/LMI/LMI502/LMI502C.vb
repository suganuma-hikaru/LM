' ==========================================================================
'  システム名       :  LMI
'  サブシステム名   :  LMI     : 
'  プログラムID     :  LMI502C : 在庫証明書
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI502定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI502C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' SFTPデータ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_DATA As String = "SelectSftpData"

    ''' <summary>
    ''' LMI502SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_LMI502SET As String = "LMI502SET"

    ''' <summary>
    ''' 特殊プラントコード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PLANTCD_SPECIAL As String = "WQ49"

    ''' <summary>
    ''' 荷主コード セラニーズ
    ''' </summary>
    Public Const CUST_CD_L_CELANESE As String = "00688"

    ''' <summary>
    ''' 個数単位(EA)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NB_UT_EA As String = "EA"

End Class
