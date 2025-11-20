' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC050C : 出荷帳票印刷
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMC050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMC050IN"

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT01 As String = "01"   '出荷データチェックリスト
    Public Const PRINT02 As String = "02"   '日別出荷報告書
    Public Const PRINT03 As String = "03"   '出荷報告書(月次)
    '(2012.11.06)MDB帳票化対応 -- START --
    Public Const PRINT04 As String = "04"   'JAL改定
    Public Const PRINT05 As String = "05"   'ANA改定
    '(2012.11.06)MDB帳票化対応 --  END  --
    '2013.07.31 追加START
    Public Const PRINT06 As String = "06"   'BYK出荷報告作成
    '2013.07.31 追加END

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT As String = "PrintData"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        Print = 0
        Eigyo
        CustCD_L
        CustNM_L
        CustCD_M
        CustNM_M
        '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
        CustCD_S
        CustNM_S
        '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --
        SyukkaDate
        Furikae
        DataInsDate
        PrintDate_S
        PrintDate_E
        GoodsCd
        GoodsNm

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        MASTEROPEN
        TOJIRU
        PRINT
        ENTER

    End Enum


End Class
