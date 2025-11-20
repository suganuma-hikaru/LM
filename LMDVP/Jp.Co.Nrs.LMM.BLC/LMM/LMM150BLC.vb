' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM150BLC : 倉庫マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM150BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM150BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM150DAC = New LMM150DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 倉庫マスタ更新対象データ件数検索
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
    ''' 倉庫マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 倉庫マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistSokoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistSokoM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 倉庫マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSokoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSokoM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSokoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertSokoM", ds)

    End Function

    ''' <summary>
    ''' 倉庫マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSokoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateSokoM", ds)

    End Function

    ''' <summary>
    ''' 倉庫マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSokoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSokoM", ds)

    End Function

    '2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function CheckZipM(ByVal ds As DataSet) As DataSet

    '    ds = MyBase.CallDAC(Me._Dac, "CheckExistZipM", ds)

    '    '処理件数による判定
    '    If MyBase.GetResultCount() = 0 Then

    '        '0件の場合、存在なしエラーメッセージを設定する
    '        MyBase.SetMessage("E079")

    '    End If

    '    Return ds

    'End Function
    '2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' JISコード整合性存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckJisWng(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMM150IN").Rows(0)

        'JISコードが入力なしのときスルー
        If String.IsNullOrEmpty(dr.Item("JIS_CD").ToString()) = True Then

            Return ds

        End If

        ds = MyBase.CallDAC(Me._Dac, "CheckJisWng", ds)

        '画面で入力されたJISコードと郵便番号マスタに登録されているJISコードが異なるときワーニング

        '2011.09.08 検証結果_導入時要望№1対応 START
        '画面の郵便番号がマスタに存在しない場合、JISコードが取得できないためワーニング
        If ds.Tables("LMM150JIS").Rows.Count = 0 Then

            MyBase.SetMessage("W176", New String() {"郵便番号", "JISコード"})

        Else
            If dr.Item("JIS_CD").ToString().Equals _
              (ds.Tables("LMM150JIS").Rows(0).Item("JIS_CD").ToString()) = False Then

                MyBase.SetMessage("W129", New String() {"JISコード", "郵便番号マスタ"})

            End If
        End If
        '2011.09.08 検証結果_導入時要望№1対応 END

        Return ds

    End Function

#End Region

#End Region

End Class
