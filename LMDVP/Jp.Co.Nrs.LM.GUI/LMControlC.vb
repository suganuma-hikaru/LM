' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :              : 共通
'  プログラムID     :  LMControlC  : 共通コンスト
'  作  成  者       :  坂本
' ==========================================================================

''' <summary>
''' LMControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' BLF
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BLF As String = "BLF"

#Region "テーブル名"

    ''' <summary>
    ''' LMB020CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMB020C_TABLE_NM_IN As String = "LMB020IN"

    ''' <summary>
    ''' LMC020CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMC020C_TABLE_NM_IN As String = "LMC020IN"

    ''' <summary>
    ''' LMC040CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMC040C_TABLE_NM_IN As String = "LMC040IN"

    ''' <summary>
    ''' LMC040C_TABLE_NM_OUT
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMC040C_TABLE_NM_OUT As String = "LMC040OUT"

    ''' <summary>
    ''' LMDControlCのTABLE_NM_LMD000_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_TABLE_NM_IN As String = "LMD000IN"

    ''' <summary>
    ''' LMDControlCのTABLE_NM_LMD000_OUT
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_TABLE_NM_OUT As String = "LMD000OUT"

    ''' <summary>
    ''' LMD040CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD040C_TABLE_NM_IN As String = "LMD040IN"

    ''' <summary>
    ''' LMD100CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD100C_TABLE_NM_IN As String = "LMD100IN"

    ''' <summary>
    ''' LMD100CのTABLE_NM_OUT
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD100C_TABLE_NM_OUT As String = "LMD100OUT"

    ''' <summary>
    ''' LMD100CのTABLE_NM_ZAI
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD100C_TABLE_NM_ZAI As String = "LMD100_ZAI"

    ''' <summary>
    ''' LMG060CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMG060C_TABLE_NM_IN As String = "LMG060IN"

    ''' <summary>
    ''' LMK060CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMK060C_TABLE_NM_IN As String = "LMK060IN"

    ''' <summary>
    ''' LMM010CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMM010C_TABLE_NM_IN As String = "LMM010IN"

    ''' <summary>
    ''' LMM010CのTABLE_NM_OUT2
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMM010C_TABLE_NM_OUT2 As String = "LMM010_TCUST"

    ''' <summary>
    ''' LMM020CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMM020C_TABLE_NM_IN As String = "LMM020IN"

    ''' <summary>
    ''' LMM040CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMM040C_TABLE_NM_IN As String = "LMM040IN"

    ''' <summary>
    ''' LMR010CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMR010C_TABLE_NM_IN As String = "LMR010IN"

    ''' <summary>
    ''' LMU010CのTABLE_NM_IN
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMU010C_TABLE_NM_IN As String = "LMU010IN"

#End Region ' テーブル名

#Region "カラム名"

    ''' <summary>
    ''' LMDControlCのCOL_GOODS_CD_NRS
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_COL_GOODS_CD_NRS As String = "GOODS_CD_NRS"

    ''' <summary>
    ''' LMDControlCのCOL_NRS_BR_CD
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_COL_NRS_BR_CD As String = "NRS_BR_CD"

    ''' <summary>
    ''' LMDControlCのCOL_CHK_DATE
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_COL_CHK_DATE As String = "CHK_DATE"

    ''' <summary>
    ''' LMDControlCのCOL_REPLACE_STR1
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_COL_REPLACE_STR1 As String = "REPLACE_STR1"

    ''' <summary>
    ''' LMDControlCのCOL_REPLACE_STR2
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMD000_COL_REPLACE_STR2 As String = "REPLACE_STR2"

#End Region ' カラム名

    ''' <summary>
    ''' 日付初期値(システム日付の１日前)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMC020C_OUTKA_DATE_INIT_01 As String = "01"

    ''' <summary>
    '''  日付初期値(システム日付)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMC020C_OUTKA_DATE_INIT_02 As String = "02"

    ''' <summary>
    '''  日付初期値(システム日付の１日後)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LMC020C_OUTKA_DATE_INIT_03 As String = "03"

End Class