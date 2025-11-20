' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM050BLC : 請求先マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM050BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM050BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM050DAC = New LMM050DAC()



#End Region

#Region "Method"

#Region "起動時処理"

    ''' <summary>
    ''' JDE非必須ユーザ確認
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectBusyo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectBusyo", ds)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 請求先マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求先マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectVarStrage", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistSeikyusakiM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function
   

    ''' <summary>
    ''' 請求先マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSeikyusakiM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSeikyusakiM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 請求先マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSeikyusakiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertSeikyusakiM", ds)

    End Function

    ''' <summary>
    ''' 請求先マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSeikyusakiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateSeikyusakiM", ds)

    End Function

    ''' <summary>
    ''' 請求先マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSeikyusakiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSeikyusakiM", ds)

    End Function

    '2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function CheckZipM(ByVal ds As DataSet) As DataSet

    '    If String.IsNullOrEmpty(ds.Tables("LMM050IN").Rows(0).Item("ZIP").ToString()) = False Then
    '        'DACクラス呼出
    '        ds = MyBase.CallDAC(Me._Dac, "CheckExistZipM", ds)
    '        '処理件数による判定
    '        If MyBase.GetResultCount() = 0 Then
    '            '0件の場合、存在なしエラーメッセージを設定する
    '            MyBase.SetMessage("E079", New String() {"郵便番号マスタ", "郵便番号"})
    '        End If
    '    End If


    '    Return ds

    'End Function
    '2011.09.08 検証結果_導入時要望№1対応 END

#End Region


#Region "請求通貨コンボ取得"
    ''' <summary>
    '''請求通貨コンボ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>請求通貨コンボ取得</remarks>
    Private Function SelectComboSeiqCurrCd(ByVal ds As DataSet) As DataSet

        '契約通貨コンボ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectComboSeiqCurrCd", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
