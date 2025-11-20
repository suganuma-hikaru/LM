' ==========================================================================
'  システム名       :  LMI
'  サブシステム名   :  LMI     : 
'  プログラムID     :  LMI500C : 日次在庫報告用データ
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI500定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI500C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 日次在庫報告用データ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_DATA As String = "SelectNitijiData"

    ''' <summary>
    ''' サーバクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CLASS_NM As String = "LMI500"

    ''' <summary>
    ''' ファイル名(CustCdS = '00')
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FILE_NM_SK As String = "sk"

    ''' <summary>
    ''' ファイル名(CustCdS = '01')
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FILE_NM_DK As String = "dk"

    ''' <summary>
    ''' 荷主コード小(filename = 'sk')
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUST_CD_S_00 As String = "00"

    ''' <summary>
    ''' 荷主コード小(filename = 'dk')
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CUST_CD_S_01 As String = "01"

    ''' <summary>
    ''' LMI500SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_LMI500SET As String = "LMI500SET"

    ''' <summary>
    ''' 1ページ値の列数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PAGE_MAX_COL As String = "N"

    ''' <summary>
    ''' 1ページ値の行数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PAGE_MAX_ROW As Integer = 43

    ''' <summary>
    ''' Sheetインデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SheetColumnIndex

        GOODS_CD_CUST = 1
        SOURCD
        GMC
        GOODS_NM
        REMARK_OUT
        LOT
        SERIAL
        ZAI_NB
        ZAI_NB_UT
        ZAI_QT
        ZAI_QT_UT
        JYOTAI
        FRB
        INKO_DATE

    End Enum

End Class
