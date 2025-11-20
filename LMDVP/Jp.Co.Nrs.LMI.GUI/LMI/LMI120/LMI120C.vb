' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI120  : 日医工在庫照合データ作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI120定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI120C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'EVENTNAME
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_JIKKO As String = "実行"
    
    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPSEARCH = 0
        EIGYO
        CUSTCDL
        CUSTCDM
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
