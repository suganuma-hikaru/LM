' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG900BLF : 請求処理 請求取込データ抽出作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMG900BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG900BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMG900BLC = New LMG900BLC()

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
        Return MyBase.CallBLC(Me._Blc, "GetInvFrom", ds)

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

        'チェックを行う
        Return MyBase.CallBLC(Me._Blc, "ChkExistHokanIkoData", ds)

    End Function

    ''' <summary>
    ''' 坪貸し料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectHokanTuboData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectHokanTuboData", ds)

    End Function

    ''' <summary>
    ''' 保管料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectHokanData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectHokanData", ds)

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

        'チェックを行う
        Return MyBase.CallBLC(Me._Blc, "ChkExistNiyakuIkoData", ds)

    End Function

    ''' <summary>
    ''' 荷役料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectNiyakuData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectNiyakuData", ds)

    End Function

#End Region

#Region "運賃取込処理"

    ''' <summary>
    ''' 運賃確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkUnchinData(ByVal ds As DataSet) As DataSet

        'チェックを行う
        Return MyBase.CallBLC(Me._Blc, "CheckUnchin", ds)

    End Function

    ''' <summary>
    ''' 運賃検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectUnchinData", ds)

    End Function

#End Region

#Region "作業料取込処理"

    ''' <summary>
    ''' 作業料確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkSagyoData(ByVal ds As DataSet) As DataSet

        'チェックを行う
        Return MyBase.CallBLC(Me._Blc, "CheckSagyo", ds)

    End Function

    ''' <summary>
    ''' 作業料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectSagyoData", ds)

    End Function

#End Region

#Region "横持料取込処理"

    ''' <summary>
    ''' 横持料確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkYokomochiData(ByVal ds As DataSet) As DataSet

        'チェックを行う
        Return Me.CallBLC(Me._Blc, "CheckYokomochi", ds)

    End Function

    ''' <summary>
    ''' 横持料検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectYokomochiData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectYokomochiData", ds)

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

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectTemplateData", ds)

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

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectSeiqtoData", ds)

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

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCustData", ds)

    End Function

#End Region

#End Region

End Class