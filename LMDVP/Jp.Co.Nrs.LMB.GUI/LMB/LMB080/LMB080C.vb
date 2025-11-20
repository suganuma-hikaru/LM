' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB080C : 登録済み画像照会
'  作  成  者       :  matsumoto
' ==========================================================================

''' <summary>
''' LMB080定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB080IN"

    '自動生成する項目名
    Public Const CTLNM_PANELB As String = "pnlPhotoB_"
    Public Const CTLNM_SHOHIN_NM As String = "lblShohinNm_"
    Public Const CTLNM_SATSUEI_DATE As String = "lblSatsueiDate_"
    Public Const CTLNM_USER_LNM As String = "lblUserLnm_"
    Public Const CTLNM_PHOTO As String = "picPhoto_"

    '画像毎に加算するコンテナのサイズ
    Public Const SIZE_PANEL_ADD_X As Integer = 140
    Public Const SIZE_PANEL_ADD_Y As Integer = 140

End Class
