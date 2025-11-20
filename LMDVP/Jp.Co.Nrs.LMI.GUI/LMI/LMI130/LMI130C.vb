' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI130定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI130C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI130IN"
    Public Const TABLE_NM_INOUT As String = "LMI130INOUT"

    'EVENTNAME
    Public Const EVENTNAME_ADD As String = "追加"
    Public Const EVENTNAME_PRINT As String = "印刷"
    Public Const EVENTNAME_NIFUDAPRINT As String = "荷札印刷"
    Public Const EVENTNAME_KONPOPRINT As String = "梱包明細印刷"
    Public Const EVENTNAME_CLEAR As String = "クリア"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_ROWDEL As String = "行削除"

    ''' <summary>
    ''' ソートの桁数
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SORT_MAX As String = "99"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPSEARCH = 0
        EIGYO
        OUTKANO
        GRPPRINT
        CMBPRINT
        BUSU
        BTNPRINT
        BTNROWDEL
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        ADD = 0
        CLEAR
        NIFUDAPRINT
        KONPOPRINT
        PRINT
        CLOSE
        ROWDEL

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex As Integer

        DEF = 0
        GOODSNM1    '商品名
        GOODSNM2    '規格名
        LTDATE      '使用期限・有効期限
        LOTNO       'ロット№
        TSUMENB     '詰め合わせ個数
        ALCTDNB     '出荷個数
        DESTNM      '納品先名
        CUSTNM      '荷主名
        CUSTCD      '荷主コード
        OUTKANO     '出荷管理番号
        GOODSCDNRS  '商品キー
        GOODSCDCUST '商品コード
        DESTCD      '届先コード
        GOODSSYUBETU '商品種別
        WHCD        '倉庫コード
        COMPDATE    '作業日
        LAST
    End Enum

End Class
