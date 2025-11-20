' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140  : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

Imports System.Transactions
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI140BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI140BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI140BLC = New LMI140BLC()

#End Region

#Region "Method"

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

            If ds Is Nothing Then
                '0件の時はメッセージを設定していないため、ここで判定を行う
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 実績作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim dtEdi As DataTable = ds.Tables("LMI140_H_WHEDI_BAH")
        Dim dtSend As DataTable = ds.Tables("LMI140_H_SENDWHEDI_BAH")

        Dim max As Integer = dtEdi.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtEdi As DataTable = setDs.Tables("LMI140_H_WHEDI_BAH")
        Dim setDtSend As DataTable = setDs.Tables("LMI140_H_SENDWHEDI_BAH")

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                Dim rowNo As Integer = Convert.ToInt32(ds.Tables("LMI140_H_WHEDI_BAH").Rows(i).Item("ROW_NO"))

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtEdi.ImportRow(dtEdi.Rows(i))
                setDtSend.ImportRow(dtSend.Rows(i))

                setDs = MyBase.CallBLC(Me._Blc, "JissekiSakusei", setDs)

                If MyBase.IsMessageStoreExist(rowNo) = False Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region 'Method

End Class