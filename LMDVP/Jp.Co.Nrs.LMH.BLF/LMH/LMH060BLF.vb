' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    Const tableNmDic As String = "H_OUTKAEDI_HED_DIC" 'DIC荷主のテーブル名

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH060BLC = New LMH060BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectKensakuData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 荷主セット処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function UpdateNinushiSet(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMH060IN")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False

        '更新処理
        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMH060IN")
                inTbl.Clear()

                MyBase.SetMessage(Nothing)

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '荷主セット処理
                setDs = MyBase.CallBLC(Me._Blc, "UpdateNinushiSet", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

    ''' <summary>
    ''' キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function UpdateCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMH060IN")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False

        '更新処理
        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMH060IN")
                inTbl.Clear()

                MyBase.SetMessage(Nothing)

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                'キャンセル処理
                setDs = MyBase.CallBLC(Me._Blc, "UpdateCancel", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function UpdateHozon(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMH060IN")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False

        '更新処理
        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMH060IN")
                inTbl.Clear()

                MyBase.SetMessage(Nothing)

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '登録処理
                If (tableNmDic).Equals(dt.Rows(i).Item("RCV_NM_HED").ToString) = True Then
                    'DIC荷主の場合
                    setDs = MyBase.CallBLC(Me._Blc, "UpdateHozonDic", setDs)
                Else

                End If

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region

End Class