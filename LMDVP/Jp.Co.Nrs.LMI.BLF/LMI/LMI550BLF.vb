' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI550BLF : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI550BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI550BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Search(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI550BLC

        ' 強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If Not MyBase.GetForceOparation() Then
            ' 件数
            ds = MyBase.CallBLC(rtnBlc, "SearchCount", ds)

            ' メッセージの判定
            If MyBase.IsMessageExist() Then
                Return ds
            End If
        End If

        '取得
        Return MyBase.CallBLC(rtnBlc, "SearchSelect", ds)

    End Function

#End Region ' "検索処理"

#Region "更新処理"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateZaiTsmc(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI550BLC

        ' トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ' 更新
            ds = MyBase.CallBLC(rtnBlc, "UpdateZaiTsmc", ds)


            If Not MyBase.IsMessageExist() Then
                ' 更新後のタイムスタンプ項目設定
                For Each drIn As DataRow In ds.Tables("LMI550IN").Rows
                    Dim drOut As DataRow = ds.Tables("LMI550OUT").NewRow()
                    drOut.Item("TSMC_REC_NO") = drIn.Item("TSMC_REC_NO")
                    drOut.Item("SYS_UPD_DATE") = MyBase.GetSystemDate()
                    drOut.Item("SYS_UPD_Time") = MyBase.GetSystemTime()
                    ds.Tables("LMI550OUT").Rows.Add(drOut)
                Next

                ' トランザクション終了
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region ' "更新処理"

#End Region ' "Method"

End Class
