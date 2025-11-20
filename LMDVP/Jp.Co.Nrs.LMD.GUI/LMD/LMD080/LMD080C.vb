' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数と日陸在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMD080定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD080IN"
    Public Const TABLE_NM_OUT_ZAISHOGOH As String = "LMD080OUT_ZAISHOGOH"
    Public Const TABLE_NM_IN_ZAISHOGOHCUST As String = "LMD080IN_ZAISHOGOHCUST"
    Public Const TABLE_NM_IN_ZAISHOGOHCUSTSUM As String = "LMD080IN_ZAISHOGOHCUSTSUM"
    Public Const TABLE_NM_INOUT_ZAIZAN As String = "LMD080INOUT_ZAIZAN"

    'EVENTNAME
    Public Const EVENTNAME_CHECK As String = "チェック"
    Public Const EVENTNAME_TORIKOMI As String = "取込"
    Public Const EVENTNAME_SHUKEI As String = "集計"
    Public Const EVENTNAME_SHOGO As String = "照合"
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"

    '数値の桁数
    Public Const NUM_MAX As String = "99999999999999999999.9999999999"

    'ファイル取込項目名
    Public Const FILEGOODSCD As String = "商品コード"
    Public Const FILEGOODSNM As String = "商品名"
    Public Const FILELOTNO As String = "ロット№"
    Public Const FILESERIALNO As String = "シリアル№"
    Public Const FILEIRIME As String = "入目"
    Public Const FILEIRIMEUT As String = "入目単位"
    Public Const FILECLASS1 As String = "分類項目１"
    Public Const FILECLASS2 As String = "分類項目２"
    Public Const FILECLASS3 As String = "分類項目３"
    Public Const FILECLASS4 As String = "分類項目４"
    Public Const FILECLASS5 As String = "分類項目５"
    Public Const FILENB As String = "個数"
    Public Const FILEQT As String = "数量"
    Public Const FILEFREEN1 As String = "数値０１"
    Public Const FILEFREEN2 As String = "数値０２"
    Public Const FILEFREEN3 As String = "数値０３"
    Public Const FILEFREEN4 As String = "数値０４"
    Public Const FILEFREEN5 As String = "数値０５"
    Public Const FILEFREEN6 As String = "数値０６"
    Public Const FILEFREEN7 As String = "数値０７"
    Public Const FILEFREEN8 As String = "数値０８"
    Public Const FILEFREEN9 As String = "数値０９"
    Public Const FILEFREEN10 As String = "数値１０"
    Public Const FILEFREEC1 As String = "文字列０１"
    Public Const FILEFREEC2 As String = "文字列０２"
    Public Const FILEFREEC3 As String = "文字列０３"
    Public Const FILEFREEC4 As String = "文字列０４"
    Public Const FILEFREEC5 As String = "文字列０５"
    Public Const FILEFREEC6 As String = "文字列０６"
    Public Const FILEFREEC7 As String = "文字列０７"
    Public Const FILEFREEC8 As String = "文字列０８"
    Public Const FILEFREEC9 As String = "文字列０９"
    Public Const FILEFREEC10 As String = "文字列１０"
    Public Const FILERBKB1 As String = "赤データ条件１"
    Public Const FILERBKB2 As String = "赤データ条件２"
    Public Const FILERBKB3 As String = "赤データ条件３"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        PNLSHOGOHTAISHO = 0
        EIGYO
        CUSTCDL
        CUSTNML
        CUSTCDM
        CUSTNMM
        JISSHIDATE
        LAYOUT
        PNLTORIKOMI
        BTNCHECK
        FOLDER
        FILENM
        BTNTORIKOMI
        PNLSHUKEI
        BTNSHUKEI
        PNLSHOGOH
        PNLSHOGOHKEY
        CHKCUSTCD
        CHKLOTNO
        CHKSERIALNO
        CHKIRIME
        CHKIRIMEUT
        CHKWRITEFLG
        BTNSHOGO

    End Enum


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        CHECK = 0
        TORIKOMI
        SHUKEI
        SHOGO
        MASTER
        CLOSE
        CUSTCDCHENGE
        LAYOUTCHENGE

    End Enum

End Class
