' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB070C : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================

''' <summary>
''' LMB070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB070IN"
    Public Const TABLE_NM_OUT As String = "LMB070OUT"
    Public Const TABLE_NM_OUT_INKA_PHOTO As String = "LMB070OUT_INKA_PHOTO"
    Public Const TABLE_NM_IN_INKA_PHOTO As String = "LMB070IN_INKA_PHOTO"
    Public Const TABLE_NM_SAVE As String = "LMB070SAVE"

    '画像選択区分
    Public Const PHOTO_SEL As String = "1"

    '自動生成する項目名
    Public Const CTLNM_PANELB As String = "pnlPhotoB_"
    Public Const CTLNM_SHOHIN_NM As String = "lblShohinNm_"
    Public Const CTLNM_SATSUEI_DATE As String = "lblSatsueiDate_"
    Public Const CTLNM_USER_LNM As String = "lblUserLnm_"
    Public Const CTLNM_SYS_UPD_DATE As String = "lblSysUpdDate_"
    Public Const CTLNM_SYS_UPD_TIME As String = "lblSysUpdTime_"
    Public Const CTLNM_PHOTO As String = "picPhoto_"
    Public Const CTLNM_PHOTOLABEL As String = "lblPhotoBorder_"

    '自動生成するコンテナのサイズ
    Public Const SIZE_PANEL_Y As Integer = 380

    '画像毎に加算するコンテナのサイズ
    Public Const SIZE_PANEL_ADD_X As Integer = 140
    Public Const SIZE_PANEL_ADD_Y As Integer = 140

    'ボタンキャプション
    Public Const BTNTITLE_ALLDISP As String = "全てを表示"
    Public Const BTNTITLE_HIDEDISP As String = "一部を表示"

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        SATSUEI_DATE_FROM = 0
        SATSUEI_DATE_TO
        KEYWORD
        PNL_DETAIL

    End Enum

End Class
