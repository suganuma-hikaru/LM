' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330  : 納品データ選択&編集
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI330BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI330BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI330BLC = New LMI330BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc584 As LMH584BLC = New LMH584BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''編集時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub EditChk(ByVal ds As DataSet)

        'BLCクラス呼出
        Call MyBase.CallBLC(Me._Blc, "HaitaChk", ds)

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet


        Dim inDs As DataSet = ds.Copy
        Dim inDt As DataTable = inDs.Tables("LMI330IN")

        For i As Integer = 0 To ds.Tables("LMI330IN").Rows.Count - 1

            inDt.Clear()
            inDt.ImportRow(ds.Tables("LMI330IN").Rows(i))


            If inDt.Rows(0).Item("SYS_DEL_FLG").ToString().Equals("1") = True Then
                Continue For
            End If

            '存在チェック
            Call MyBase.CallBLC(Me._Blc, "SelectExistData", inDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        Next

        Return MyBase.CallBLC(Me._Blc, "UpdateData", ds)

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 納品書(ロンザ専用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc584, "DoPrint", ds)

    End Function

#End Region

#End Region

End Class