' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ         : 共通
'  プログラムID     :  LMZControlC : LMZ共通定数定義クラス
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ共通定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    '有無フラグスプレッド表示
    Public Const UMU_ON As String = "○"
    Public Const UMU_OFF As String = "×"

    '有無フラグ用区分コード
    Public Const KBNCD0 As String = "00"
    Public Const KBNCD1 As String = "01"
    Public Const KBNCD2 As String = "02"

    'タリフ区分コード
    Public Const TARIFFKBN0 As String = "00"
    Public Const TARIFFKBN1 As String = "01"
    Public Const TARIFFKBN2 As String = "02"

    '表示制御区分
    Public Const HYOJI_M As String = "0"
    Public Const HYOJI_S As String = "1"
    Public Const HYOJI_SS As String = "2"

    ''' <summary>
    ''' コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CD As String = "コード"

    ''' <summary>
    ''' 支店コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BR_CD As String = "支店コード"

    ''' <summary>
    ''' (大)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const L_NM As String = "(大)"

    ''' <summary>
    ''' (中)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const M_NM As String = "(中)"

    ''' <summary>
    ''' F10
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum F10Pattern As Integer

        ptn1
        ptn2
        ptn3
        'START YANAI 要望番号604
        ptn4
        'END YANAI 要望番号604
        ptn5

    End Enum

    ''' <summary>
    ''' F11
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum F11Pattern As Integer

        ptn1
        ptn2

    End Enum

    ''' <summary>
    ''' 抽出条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ConditionPattern As Integer

        equal
        pre
        all
        more
        less

    End Enum

End Class
