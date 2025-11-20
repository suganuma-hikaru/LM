' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH080BLF : 
'  作  成  者       :  s.kobayashi
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH080BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH080BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH080BLC = New LMH080BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(_Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 削除変更処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateDelUTI(ByVal ds As DataSet) As DataSet

        Dim dtKey As DataTable = ds.Tables("LMH080IN_DEL")
        Dim max As Integer = dtKey.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtKey As DataTable = setDs.Tables("LMH080IN_DEL")

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                setDtKey.Clear()

                '条件の設定
                setDtKey.ImportRow(dtKey.Rows(i))

                'DACクラス呼出
                setDs = MyBase.CallBLC(Me._Blc, "UpdateDelUTI", setDs)

                rowNo = Convert.ToInt32(dtKey.Rows(i).Item("ROW_NO"))

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageStoreExist(rowNo)

                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using
        Next

        Return setDs

    End Function

#End Region

#End Region

End Class
