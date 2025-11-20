' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 
'  プログラムID     :  LMB050BLC : 
'  作  成  者       :  阿達
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMB050BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB050BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB050DAC = New LMB050DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 入荷検品選択対象データ件数検索
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
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ区分が「変更」かつ入荷大に注文番号が存在するかをチェック
    ''' </remarks>
    Private Function SelectHenkouChk(ByVal ds As DataSet) As DataSet

        Dim tmpDs As DataSet = New DataSet()
        tmpDs = ds.Copy()
        Dim max As Integer = ds.Tables("LMB050IN").Rows.Count

        
        Dim kbnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectHenkouChk", tmpDs)
        Dim kbnCd As String = kbnDs.Tables("LMB050IN").Rows(0).Item("STATE_KBN").ToString()

        For i As Integer = 0 To max - 1

            tmpDs.Clear()
            '選択データを１件ずつインポートしチェック
            tmpDs.Tables("LMB050IN").ImportRow(ds.Tables("LMB050IN").Rows(i))

            '区分が新規かつ取り込み済みの場合は取り込み済みエラーとして空のデータセットを返す
            If tmpDs.Tables("LMB050IN").Rows(0).Item("STATE_KBN").Equals(kbnCd) Then
                MyBase.CallDAC(Me._Dac, "SelectHenkouChk", tmpDs)

                If MyBase.GetResultCount > 0 Then

                    '初期化して返還
                    ds.Tables("LMB050IN").Clear()
                    Return ds
                End If
            End If

        Next

        Return ds

    End Function

#End Region


#End Region

End Class
