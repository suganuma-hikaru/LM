' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI060C : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI060定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI060IN"
    Public Const TABLE_NM_OUT As String = "LMI060OUT"
    Public Const TABLE_NM_INOUT As String = "LMI060INOUT"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DATEFROM = 0
        DATETO
        CMBMAKE
        BTNMAKE
        CMBPRINT
        BTNPRINT
        SPRDETAILS
       
    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        CLOSE = 0
        MAKE
        PRINT
        CELLCLICK
      
    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        CUST_NM
        TARIFF_CD
        TARIFF_KB
        WARIMASHI_NR
        ROUND_KB
        ROUND_UT
        REMARK
        CUST_NM_L
        CUST_NM_M
        CUST_NM_S
        CUST_NM_SS
        ROUND_UT_LEN
        FREE_C01
        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        ROW_CNT
        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        LAST

    End Enum

End Class
