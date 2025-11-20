' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG900BLF : 請求処理 請求取込データ抽出作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMG900BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG900BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG900DAC = New LMG900DAC()

    ''' <summary>
    '''請求サブ共通DACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _CommonDac As LMG000DAC = New LMG000DAC()

#End Region

#Region "Method"

#Region "請求開始日取得処理"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetInvFrom(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallDAC(Me._CommonDac, "GetInvFrom", ds)

    End Function

#End Region

#Region "保管料取込処理"

    ''' <summary>
    ''' 保管料移行データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkExistHokanIkoData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ChkExistHokanIkoData", ds)

    End Function

    ''' <summary>
    ''' 坪貸し料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectHokanTuboData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectHokanTuboData", ds)

    End Function

    ''' <summary>
    ''' 保管料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectHokanData(ByVal ds As DataSet) As DataSet

        '保管料取得
        ds = MyBase.CallDAC(Me._Dac, "SelectHokanData", ds)

        Return ds

    End Function

#End Region

#Region "荷役料取込処理"

    ''' <summary>
    ''' 荷役料移行データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkExistNiyakuIkoData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ChkExistNiyakuIkoData", ds)

    End Function

    ''' <summary>
    ''' 荷役料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectNiyakuData(ByVal ds As DataSet) As DataSet

        '荷役料取得
        ds = MyBase.CallDAC(Me._Dac, "SelectNiyakuData", ds)

        Return ds

    End Function

#End Region

#Region "運賃取込処理"

    ''' <summary>
    ''' 運賃確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckUnchin(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "CheckUnchin", ds)

    End Function

    ''' <summary>
    ''' 運賃検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectUnchinData", ds)

    End Function

#End Region

#Region "作業料取込処理"

    ''' <summary>
    ''' 作業料確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckSagyo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "CheckSagyo", ds)

    End Function

    ''' <summary>
    ''' 作業料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectSagyoData", ds)

    End Function

#End Region

#Region "横持料取込処理"

    ''' <summary>
    ''' 横持料確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckYokomochi(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "CheckYokomochi", ds)

    End Function

    ''' <summary>
    ''' 横持料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectYokomochiData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectYokomochiData", ds)

    End Function

#End Region

#Region "テンプレートマスタ取込処理"

    ''' <summary>
    ''' テンプレートマスタ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTemplateData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectTemplateData", ds)

    End Function

#End Region

#Region "請求先情報取得処理"

    ''' <summary>
    ''' 請求先情報取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectSeiqtoData", ds)

    End Function

#End Region

#Region "荷主マスタ取得処理"

    ''' <summary>
    ''' 荷主マスタ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCustData", ds)

    End Function

#End Region

#End Region

End Class
