' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB810    : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMB810BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB810BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB810BLC = New LMB810BLC()

#End Region

#Region "Method"

#Region "現場作業指示"
    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCの庫内作業指示メソッドに飛ぶ</remarks>
    Private Function WHSagyoShiji(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet
        'トランザクション開始

        Dim dt As DataTable = ds.Tables("LMB810IN")
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables("LMB810IN")

        For i As Integer = 0 To dt.Rows.Count - 1

            Using scope As TransactionScope = MyBase.BeginTransaction()

                inDt.Clear()
                inDt.ImportRow(dt.Rows(i))

                '排他チェック
                ds = MyBase.CallBLC(New LMB810BLC(), LMB810BLC.FUNCTION_NM.CHECK_HAITA, inDs)

                '処理件数による判定
                If "0".Equals(ds.Tables("LMB810CNT").Rows(0).Item("CNT")) Then
                    '0件の場合、論理排他メッセージを設定する
                    MyBase.SetMessageStore("00", "E262", New String() {dt.Rows(0).Item("INKA_NO_L").ToString}, dt.Rows(0).Item("ROW_NO").ToString)
                    Continue For
                End If

                '更新
                ds = MyBase.CallBLC(New LMB810BLC(), LMB810BLC.FUNCTION_NM.WH_SAGYO_SHIJI, inDs)

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
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCの庫内作業指示メソッドに飛ぶ</remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet
        'トランザクション開始

        Dim dt As DataTable = ds.Tables("LMB810IN")
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables("LMB810IN")

        For i As Integer = 0 To dt.Rows.Count - 1

            Using scope As TransactionScope = MyBase.BeginTransaction()

                inDt.Clear()
                inDt.ImportRow(dt.Rows(i))

                '排他チェック
                ds = MyBase.CallBLC(New LMB810BLC(), LMB810BLC.FUNCTION_NM.CHECK_HAITA, inDs)

                '処理件数による判定
                If "0".Equals(ds.Tables("LMB810CNT").Rows(0).Item("CNT")) Then
                    '0件の場合、論理排他メッセージを設定する
                    MyBase.SetMessage("E011")
                    Return ds
                End If

                '更新
                ds = MyBase.CallBLC(New LMB810BLC(), LMB810BLC.FUNCTION_NM.WH_SAGYO_SHIJI_CANCEL, inDs)

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
