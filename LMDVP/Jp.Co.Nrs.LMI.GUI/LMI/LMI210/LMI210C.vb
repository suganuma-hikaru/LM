' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : ハネウェル管理
'  作  成  者       :  [KIM]
' ==========================================================================

''' <summary>
''' LMI210定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI210C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI210IN"
    Public Const TABLE_NM_OUT As String = "LMI210OUT"
    Public Const TABLE_NM_EXCEL_IN As String = "LMI910IN"

    '桁数
    Public Const NB_MAX_4 As String = "9999"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPSEARCH = 0
        EIGYO
        IDODATEFROM
        IDODATETO
        COOLANTGOODSKB            '2013.08.15 要望番号2095 START
        CYLTYPE
        BASEDATE
        GRPSHORI
        OPTINKA
        OPTOUTKA
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        GETDATA = 0
        HENSHU
        SUZUSHO
        HENKHAKU
        SHUKKA
        GETLOG
        HAIKI
        HAIKIKAIJO
        KENSAKU
        TEIKIKENSAKANRI
        HOZON
        CLOSE
        EXCEL
        CHANGEOPTBUTTOM
        DOUBLECLICK

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(返却検索の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexINKA
        SERIAL_NO = 0
        YOUKINO
        CYLINDER_TYPE
        NEXT_TEST_DATE
        WH_CD
        WH_NM
        INOUT_DATE
        TOFROM_CD
        TOFROM_NM
        INOUT_DATE_OUT
        TOFROM_CD_OUT
        TOFROM_NM_OUT
        SHIP_CD_L
        SHIP_NM_L
        LAYT_DAY
        PENALTY
        BONUS
        SALES_TO
        DUMY1
        DUMY2
        LAST

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(出荷検索の場合)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexOUTKA

        SERIAL_NO = 0
        YOUKINO
        CYLINDER_TYPE
        NEXT_TEST_DATE
        WH_CD
        WH_NM
        INOUT_DATE
        DUMY1
        DUMY2
        DUMY3
        TOFROM_CD
        TOFROM_NM
        SHIP_CD_L
        SHIP_NM_L
        DUMY4
        DUMY5
        DUMY6
        DUMY7
        BUYER_ORD_NO_DTL
        CUST_ORD_NO_DTL
        LAST

    End Enum

End Class
