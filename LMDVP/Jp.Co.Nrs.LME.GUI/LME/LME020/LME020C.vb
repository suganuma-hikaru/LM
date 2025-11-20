' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME020C : 作業料明細編集
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LME020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LME020IN"
    Public Const TABLE_NM_INOUT As String = "LME020INOUT"

    '桁数
    Public Const MAX_10 As String = "9,999,999,999"
    Public Const MAX_9_3 As String = "999,999,999.999"
    Public Const MAX_9_2 As String = "999,999,999.99"
    Public Const MIN_0 As String = "0"

    '元データ区分
    Public Const MOTO_DATA_KBN_10 As String = "10"
    Public Const MOTO_DATA_KBN_11 As String = "11"
    Public Const MOTO_DATA_KBN_12 As String = "12"
    Public Const MOTO_DATA_KBN_20 As String = "20"
    Public Const MOTO_DATA_KBN_21 As String = "21"
    Public Const MOTO_DATA_KBN_22 As String = "22"
    Public Const MOTO_DATA_KBN_30 As String = "30"
    Public Const MOTO_DATA_KBN_40 As String = "40"
    Public Const MOTO_DATA_KBN_50 As String = "50"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        'START YANAI 要望番号875
        'IOZSKB = 0
        SAGYOCOMPDATE = 0
        IOZSKB
        'END YANAI 要望番号875
        SAGYONM
        CUSTCDL
        CUSTCDM
        DESTCD
        DESTNM
        'START YANAI 要望番号875
        'DESTSAGYOFLG
        'END YANAI 要望番号875
        GOODSCDCUST
        GOODSNM
        LOTNO
        SEIQTOCD
        TAXKB
        SAGYONB
        SAGYOUP
        SAGYOGK
        INVTANI
        REMARKZAI
        REMARKSKYU

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        SINKI = 0
        HENSHU
        COPY
        DEL
        SKIP
        MASTER
        HOZON
        CLOSE
        'START YANAI 要望番号875
        SAGYOKINGAKU
        'END YANAI 要望番号875

    End Enum

End Class
