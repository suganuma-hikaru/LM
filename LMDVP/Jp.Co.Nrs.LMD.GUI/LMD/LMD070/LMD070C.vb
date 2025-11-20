' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD070C : 在庫帳票印刷
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMD070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD070IN"
    Public Const TABLE_NM_GETU_IN As String = "LMD070_GETU_IN"
    Public Const TABLE_NM_GETU_OUT As String = "LMD070_GETU_OUT"

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_NITI As String = "01"                        '日次出荷別在庫リスト
    Public Const PRINT_ZAIKO As String = "02"                       '在庫表
    Public Const PRINT_ZAIKO_SHOUMEI As String = "03"               '在庫証明書
    'Public Const PRINT_ZAIKO_OKI As String = "04"                  '在庫受払表（月報(置場別)）
    'Public Const PRINT_ZAIKO_CUST As String = "05"                 '在庫受払表（月報(荷主別））
    'Public Const PRINT_ZAIKO_SOKO As String = "06"                 '
    Public Const PRINT_FUDOU As String = "07"                       'SLOW MOVING INVENTORY REPORT
    Public Const PRINT_ZAIKO_SEIGOUSEI_JITU As String = "08"        '在庫整合性リスト(実在庫)
    Public Const PRINT_ZAIKO_SEIGOUSEI_SHUKA As String = "09"       '在庫整合性リスト(出荷予定)
    Public Const PRINT_ZAIKO_SEIGOUSEI_HIKI As String = "10"        '在庫整合性リスト(引当数)
    Public Const PRINT_NYUSHUKA As String = "11"                    'HISTORICAL-REPORT
    '(2012.12.13)要望番号1671 在庫証明書 印刷条件追加 --- START ---
    Public Const PRINT_ZAIKO_SHOUMEI_S_SS As String = "12"          '在庫証明書（小・極小毎）
    '(2012.12.13)要望番号1671 在庫証明書 印刷条件追加 ---  END  ---
    '(2013.01.10)要望番号1752 消防分類別在庫重量表 印刷条件追加 --- START ---
    Public Const PRINT_SYOUBOU_BUNRUI As String = "13"              '消防分類別在庫重量表
    '(2013.01.10)要望番号1752 消防分類別在庫重量表 印刷条件追加 ---  END  ---
    '(2014.11.14) 受払表 印刷条件追加 --- START ---
    Public Const PRINT_ZAIKO_UKEHARAI As String = "14"              '在庫受払表
    '(2014.11.14) 受払表 印刷条件追加 --- END ---
    '(2017.08.15) 消防分類別在庫重量表(全荷主・現在庫版)追加 --- START ---
    Public Const PRINT_SYOUBOU_BUNRUI_ALL As String = "15"              '消防分類別在庫重量表(全荷主・現在庫版)
    '(2017.08.15) 消防分類別在庫重量表(全荷主・現在庫版)追加 --- END ---

    '追加開始 --- 2014.11.17
    ''' <summary>
    ''' 印刷区分(サブ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINTSUB_ZAIKO_NORMAL As String = "01"             '在庫受払表　通常
    Public Const PRINTSUB_ZAIKO_NONINUSHI As String = "02"          '在庫受払表　荷主指定なし
    Public Const PRINTSUB_ZAIKO_SOKO As String = "03"               '在庫受払表　倉庫指定
    Public Const PRINTSUB_ZAIKO_OKIBA_NORMAL As String = "04"      '在庫受払表（置場別）
    Public Const PRINTSUB_ZAIKO_OKIBA_NONINUSHI As String = "05"    '在庫受払表（置場別）荷主指定なし
    Public Const PRINTSUB_ZAIKO_OKIBA_KIKEN As String = "06"        '在庫受払表（置場別）危険品区分順
    Public Const PRINTSUB_GEKKAN_GOTO As String = "07"              '月間入出庫重量集計表（号棟別）
    Public Const PRINTSUB_GEKKAN_NINUSHI As String = "08"           '月間入出庫重量集計表（荷主別）
    Public Const PRINTSUB_GEKKAN_GOTO_NINUSHI As String = "09"      '月間入出庫重量集計表（号棟別・荷主別）
#If True Then   'ADD 2020/10/29 新
    Public Const PRINTSUB_ZAIKO_GOODS As String = "10"             '在庫受払表　商品ごと
#End If
    '追加終了 --- 2014.11.17

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"
    Public Const ACTION_ID_PRINT As String = "PrintData"
    Public Const ACTION_ID_CHECK As String = "SelectCheck"

    ''' <summary>
    ''' プリントフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_FLG As String = "00"

    ''' <summary>
    ''' プリントフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHOKI_ZAIKO_FLG As String = "01"

    ''' <summary>
    ''' 日付
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_YYYYMMDD As String = "yyyy/MM/dd"


    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        Print = 0
        PrintSub
        Eigyo
        CustCD_L
        CustCD_M
        CustCD_S
        CustCD_SS
        SyukkaDate
        DataInsDate
        Nyuko
        Syukka
        Niugoki
        PrintDate_S
        PrintDate_E
        'START YANAI 要望番号1057 在庫証明書出力順変更
        CMBSORT
        'END YANAI 要望番号1057 在庫証明書出力順変更

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
