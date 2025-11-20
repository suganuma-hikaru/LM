' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH070    : 先行手入力入出荷とEDIの紐付け
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH070BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH070DAC = New LMH070DAC()

#End Region

#Region "Method"

#Region "初期検索処理"

    ''' <summary>
    ''' EDIの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEdi(ByVal ds As DataSet) As DataSet

        'EDI（大）取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        'EDI（中）取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 入出荷の検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInOutka(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectInOutka", ds)

        Return ds

    End Function


#End Region


#End Region

End Class
