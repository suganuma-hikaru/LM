' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数と日陸在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMD080BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD080BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD080DAC = New LMD080DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckData(ByVal ds As DataSet) As DataSet

        '荷主在庫数データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiShoCust", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

#If True Then   'ADD 2018/08/31依頼番号 : 001723   【LMS】在庫照合画面_タイムアウトエラーのため実用できず対応
        Dim rtnDs As DataSet = Nothing

        'ワークファイル削除処理（D_WK_TORIKOMIZUMI_CHK）
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteD_WK_TORIKOMIZUMI_CHK", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'ワークファイル作成処理（D_WK_TORIKOMIZUMI_CHK）
        rtnDs = MyBase.CallDAC(Me._Dac, "InsertD_WK_TORIKOMIZUMI_CHK01", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        rtnDs = MyBase.CallDAC(Me._Dac, "InsertD_WK_TORIKOMIZUMI_CHK02", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        rtnDs = MyBase.CallDAC(Me._Dac, "InsertD_WK_TORIKOMIZUMI_CHK03", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If
#End If

        '月末在庫データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiZan", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '荷主在庫数データサマリの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiShoCustSum", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function TorikomiData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        '荷主在庫数データの物理削除①(照合日指定)
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteZaiShoCust1", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '荷主在庫数データの物理削除②(照合日の１か月以上前)
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteZaiShoCust2", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '荷主在庫数データの新規追加
        rtnDs = MyBase.CallDAC(Me._Dac, "InsertZaiShoCust", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 集計処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ShukeiData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        '荷主在庫数データサマリの物理削除①(照合日指定)
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteZaiShoCustSum1", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '荷主在庫数データサマリの物理削除②(照合日の１か月以上前)
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteZaiShoCustSum2", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '月末在庫データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiZan", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If
        '取得したデータの編集
        ds = Me.MakeDataEdit(ds)

        '荷主在庫数データサマリの新規追加
        rtnDs = MyBase.CallDAC(Me._Dac, "InsertZaiShoCustSum", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 荷主在庫数データサマリ追加前の編集
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeDataEdit(ByVal ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMD080INOUT_ZAIZAN").Rows.Count - 1
        Dim gyoNo As Integer = 0
        For i As Integer = 0 To max

            With ds.Tables("LMD080INOUT_ZAIZAN").Rows(i)

                '行番号の設定
                gyoNo = gyoNo + 1
                .Item("GYO_NO") = Me.MaeCoverData(Convert.ToString(gyoNo), "0", 5)

            End With

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 照合処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ShogohData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        '荷主在庫数データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiShoCust", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '荷主在庫数データサマリの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiShoCustSum", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ShogohMstData(ByVal ds As DataSet) As DataSet

        '荷主在庫数データ取込制御マスタの検索
        ds = MyBase.CallDAC(Me._Dac, "ShogohMstData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value) To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

End Class
