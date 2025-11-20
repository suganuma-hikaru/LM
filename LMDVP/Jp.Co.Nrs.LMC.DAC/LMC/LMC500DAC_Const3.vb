' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC500    : 納品書印刷（定数定義のみ）
'  作  成  者       :  []
' ==========================================================================
'  LMC500DACのファイル肥大化によりVSが落ちるので対策として分割した
'  LMC500DAC_Const1, LMC500DAC_Const2 への追加を避けるため新設する
' ==========================================================================

''' <summary>
''' LMC500DAC_Constクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC500DAC_Const3

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 納品書摘要欄追加情報(TSMC在庫データ・シリンダーNo.)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_SELECT_ZAI_TSMC_CYLINDER_NO As String = "" _
        & "SELECT                       " & vbNewLine _
        & "      NRS_BR_CD              " & vbNewLine _
        & "    , OUTKA_NO_L             " & vbNewLine _
        & "    , OUTKA_NO_M             " & vbNewLine _
        & "    , CYLINDER_NO            " & vbNewLine _
        & "    , '0' AS USE_FLG         " & vbNewLine _
        & "FROM                         " & vbNewLine _
        & "    $LM_TRN$..D_ZAI_TSMC     " & vbNewLine _
        & "WHERE                        " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD   " & vbNewLine _
        & "AND OUTKA_NO_L = @OUTKA_NO_L " & vbNewLine _
        & "AND OUTKA_NO_M = @OUTKA_NO_M " & vbNewLine _
        & "AND RTRIM(CYLINDER_NO) <> '' " & vbNewLine _
        & "AND SYS_DEL_FLG = '0'        " & vbNewLine _
        & ""

    ''' <summary>
    ''' 納品書印刷データ抽出用(出荷テーブル:追加WHERE句)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const SQL_WHERE_OUTKA_2 As String = "" _
        & "   AND(    OUTL.SYUBETU_KB <> '60'                                                " & vbNewLine _
        & "       OR (OUTL.SYUBETU_KB =  '60' AND(OUTM.ALCTD_NB <> 0 OR OUTM.ALCTD_QT <> 0)) " & vbNewLine _
        & "       )                                                                          " & vbNewLine _
        & ""

#End Region ' "検索処理 SQL"

#End Region ' "Const"

End Class
