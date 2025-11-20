' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI963C : 荷主自動振分画面(手動)（ハネウェル）
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On


''' <summary>
''' LMI963定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI963C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI963IN"
    Public Const TABLE_NM_OUT As String = "LMI963OUT"

    'ロック制御用
    Public Enum LOCK As Integer
        M = 0
        S
        SS
    End Enum

    '親荷主コード(00)
    Public Const OYA_CUST As String = "00"

#If True Then '商品マスタから荷主マスタ参照へ遷移した場合に、ダブルクリックで荷主を選択すると荷主CD(S,SS)が消える問題に対応 Added 20151110 INOUE 
    ''' <summary>
    ''' 荷主をダブルクリックで選択した場合の動作モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DobleClickRowSelectMode
        ''' <summary>
        ''' 設定する荷主コード(小,極小)をクリアする
        ''' </summary>
        ''' <remarks></remarks>
        CLEAR_CUST_CD_S_AND_SS = 0

        ''' <summary>
        ''' 設定する荷主コードのクリアをしない
        ''' </summary>
        ''' <remarks></remarks>
        DISABLED_CLEAR_CUST_CD
    End Enum
#End If

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        SHUBETSU
        NRS_BR
        NRS_WH
        SPR
        LOAD_NUMBER
        CMD_GYO

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex As Integer

        DEF = 0
        CUST_NM_L
        CUST_NM_M
        CUST_NM_S
        CUST_NM_SS
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        ROW_INDEX
        CLOSE_KB
        UNCHIN_TARIFF_CD1
        UNCHIN_TARIFF_REM1
        UNCHIN_TARIFF_CD2
        UNCHIN_TARIFF_REM2
        EXTC_TARIFF_CD
        EXTC_TARIFF_REM
        PICK_LIST_KB
        WH_CD
        LOAD_NUMBER

    End Enum

End Class

