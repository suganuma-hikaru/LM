' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ260C : 荷主マスタ照会（大・中）
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ260定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ260C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ260IN"
    Public Const TABLE_NM_OUT As String = "LMZ260OUT"

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
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

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
        'START YANAI 要望番号558
        CLOSE_KB
        'END YANAI 要望番号558
        'START YANAI 要望番号836
        UNCHIN_TARIFF_CD1
        UNCHIN_TARIFF_REM1
        UNCHIN_TARIFF_CD2
        UNCHIN_TARIFF_REM2
        EXTC_TARIFF_CD
        EXTC_TARIFF_REM
        'END YANAI 要望番号836
        '要望番号:1839 terakawa 2013.02.08 Start
        PICK_LIST_KB
        '要望番号:1839 terakawa 2013.02.08 End
    End Enum

End Class
