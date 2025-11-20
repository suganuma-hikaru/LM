' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI070C : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI070IN"
    Public Const TABLE_NM_OUT As String = "LMI070OUT"
    Public Const TABLE_NM_INOUT_HOKANNIYAKU As String = "LMI070INOUT_HOKANNIYAKU"
    Public Const TABLE_NM_INOUT_SAGYO As String = "LMI070INOUT_SAGYO"
    Public Const TABLE_NM_INOUT_UNCHIN As String = "LMI070INOUT_UNCHIN"

    'EVENTNAME
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_MAKE As String = "作成"
    Public Const EVENTNAME_JIKKO As String = "実行"
    Public Const EVENTNAME_PRINT As String = "印刷"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        CUSTCDL
        CUSTCDM
        SEIKYUFROM
        SEIKYUTO
        GRPMAKE
        CMBMAKE
        BTNMAKE
        GRPJIKKO
        CMBJIKKO
        BTNJIKKO
        GRPPRINT
        CMBPRINT
        CMBPRINT2
        BTNPRINT

    End Enum


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MASTER = 0
        CLOSE
        MAKE
        JIKKO
        PRINT
      
    End Enum

End Class
