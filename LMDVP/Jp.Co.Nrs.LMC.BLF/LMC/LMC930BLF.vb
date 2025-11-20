' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC930    : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMC930BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC930BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMC930BLC = New LMC930BLC()

#End Region

#Region "Method"

#Region "作業指示"
    ''' <summary>
    ''' 庫内作業指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCの庫内作業指示メソッドに飛ぶ</remarks>
    Private Function WHSagyoShiji(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet
        'トランザクション開始

        Dim dt As DataTable = ds.Tables("LMC930IN")
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables("LMC930IN")

        For i As Integer = 0 To dt.Rows.Count - 1

            Using scope As TransactionScope = MyBase.BeginTransaction()
                inDt.Clear()
                inDt.ImportRow(dt.Rows(i))

                '排他チェック
                rtnDs = MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.CHECK_HAITA, inDs)

                If "0".Equals(rtnDs.Tables("LMC930CNT").Rows(0).Item("CNT")) Then
                    MyBase.SetMessageStore("00", "E262", New String() {dt.Rows(i).Item("OUTKA_NO_L").ToString}, dt.Rows(i).Item("ROW_NO").ToString)
                    Continue For
                End If

                '更新
                rtnDs = MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.WH_SAGYO_SHIJI, inDs)

                'エラーがあるかを判定
                If Not MyBase.IsMessageExist() Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                Else
                    Return ds
                End If

            End Using

        Next

        Return ds

    End Function
#End Region

#Region "現場作業指示取消"
    ''' <summary>
    ''' 現場作業指示取消
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet
        'トランザクション開始

        Dim dt As DataTable = ds.Tables("LMC930IN")
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables("LMC930IN")

        For i As Integer = 0 To dt.Rows.Count - 1

            Using scope As TransactionScope = MyBase.BeginTransaction()
                inDt.Clear()
                inDt.ImportRow(dt.Rows(i))

                '排他チェック
                rtnDs = MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.CHECK_HAITA, inDs)

                If "0".Equals(rtnDs.Tables("LMC930CNT").Rows(0).Item("CNT")) Then
                    MyBase.SetMessage("E011")
                    Return ds
                End If

                '更新
                rtnDs = MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.WH_SAGYO_SHIJI_CANCEL, inDs)

                'エラーがあるかを判定
                If Not MyBase.IsMessageExist() Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                Else
                    Return ds
                End If

            End Using

        Next

        Return ds

    End Function
#End Region


#End Region

End Class
