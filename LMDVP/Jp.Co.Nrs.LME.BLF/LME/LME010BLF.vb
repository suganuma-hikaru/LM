' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME010BLF : 作業料明細書作成
'  作  成  者       :  nishikawa
' ==========================================================================

Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LME010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LME010BLC = New LME010BLC()

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

#Region "確定処理"

    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateKakutei(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME010_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LME010_SAGYO")

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDt.ImportRow(dt.Rows(i))

                'DACクラス呼出
                setDs = MyBase.CallBLC(Me._Blc, "UpdateSagyo", setDs)

                rowNo = Convert.ToInt32(dt.Rows(i).Item("ROW_NO"))

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

#Region "確定解除処理"

    ''' <summary>
    ''' 確定解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateKaijo(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME010_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LME010_SAGYO")

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDt.ImportRow(dt.Rows(i))

                'BLCクラス呼出
                Call MyBase.CallBLC(Me._Blc, "ChkSeiqDateSagyo", setDs)

                rowNo = Convert.ToInt32(dt.Rows(i).Item("ROW_NO"))

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageStoreExist(rowNo)

                If rtnResult = True Then
                    'エラーが無ければ処理継続

                    'BLCクラス呼出
                    setDs = MyBase.CallBLC(Me._Blc, "UpdateSagyo", setDs)

                    rowNo = Convert.ToInt32(dt.Rows(i).Item("ROW_NO"))

                    'エラーがあるかを判定
                    rtnResult = Not MyBase.IsMessageStoreExist(rowNo)

                    If rtnResult = True Then
                        'エラーが無ければCommit
                        MyBase.CommitTransaction(scope)
                    End If
                Else
                    'エラーがある場合Commitせず、次のレコードへ
                End If

            End Using
        Next

        Return setDs

    End Function

#End Region

    'START YANAI 20120319　作業画面改造
#Region "完了処理"
    ''' <summary>
    ''' 完了処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateKanryo(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME010_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LME010_SAGYO")

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDt.ImportRow(dt.Rows(i))

                'DACクラス呼出
                setDs = MyBase.CallBLC(Me._Blc, "UpdateKanryo", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist

                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using
        Next

        Return setDs

    End Function

#End Region
    'END YANAI 20120319　作業画面改造

#Region "保存処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozon(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME010INOUT")
        Dim max As Integer = dt.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LME010INOUT")
        'Dim shoriCount As Integer = 0

        For i As Integer = 0 To max

            If dt.Rows(i).Item("SYS_DEL_FLG").ToString() = "1" AndAlso dt.Rows(i).Item("COPY_FLG").ToString = "1" AndAlso dt.Rows(i).Item("SAVE_FLG").ToString = "0" Then
                '行複写⇒行削除⇒保存の場合処理なし

            ElseIf dt.Rows(i).Item("SYS_DEL_FLG").ToString() = "1" AndAlso dt.Rows(i).Item("SAVE_FLG").ToString = "0" Then
                '行削除


                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    setDt.ImportRow(dt.Rows(i))

                    '作業料取込チェック
                    Call MyBase.CallBLC(Me._Blc, "ChkSeiqDateSagyoRowDel", setDs)
                    'エラーがあるかを判定
                    rtnResult = Not MyBase.IsMessageExist()

                    If rtnResult = False Then
                        'エラーがある場合次のレコード
                    Else

                        'DACクラス呼出
                        setDs = MyBase.CallBLC(Me._Blc, "UpdateSagyoDel", setDs)

                        'エラーがあるかを判定
                        rtnResult = Not MyBase.IsMessageStoreExist()

                        If rtnResult = True Then
                            'エラーが無ければCommit
                            MyBase.CommitTransaction(scope)
                        End If
                    End If

                End Using


            ElseIf dt.Rows(i).Item("COPY_FLG").ToString = "1" AndAlso dt.Rows(i).Item("SAVE_FLG").ToString = "0" Then

                '行複写

                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    setDt.ImportRow(dt.Rows(i))

                    '作業料取込チェック
                    Call MyBase.CallBLC(Me._Blc, "ChkSeiqDateSagyoRowCopy", setDs)
                    'エラーがあるかを判定
                    rtnResult = Not MyBase.IsMessageExist()

                    If rtnResult = False Then
                        'エラーがある場合次のレコード
                    Else

                        'DACクラス呼出
                        setDs = MyBase.CallBLC(Me._Blc, "InsertSagyoCopy", setDs)

                        'エラーがあるかを判定
                        rtnResult = Not MyBase.IsMessageStoreExist()

                        If rtnResult = True Then
                            'エラーが無ければCommit
                            MyBase.CommitTransaction(scope)
                        End If
                    End If

                End Using

            End If

        Next

        Return setDs

    End Function

#End Region

#Region "一括変更処理"

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        Dim dtKey As DataTable = ds.Tables("LME010OUT_UPDATE_KEY")
        Dim max As Integer = dtKey.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtKey As DataTable = setDs.Tables("LME010OUT_UPDATE_KEY")

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                setDtKey.Clear()

                '条件の設定
                setDtKey.ImportRow(dtKey.Rows(i))

                'DACクラス呼出
                setDs = MyBase.CallBLC(Me._Blc, "UpdateHenko", setDs)

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

    'START YANAI 20120319　作業画面改造
#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LME010INOUT")
        Dim max As Integer = dt.Rows.Count - 1
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LME010INOUT")

        For i As Integer = 0 To max

            If ("1").Equals(dt.Rows(i).Item("SYS_DEL_FLG").ToString) = True Then
                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    setDt.ImportRow(dt.Rows(i))

                    '削除処理
                    Call MyBase.CallBLC(Me._Blc, "DeleteData", setDs)

                    rtnResult = Not MyBase.IsMessageExist()
                    If rtnResult = False Then
                        'エラーがある場合次のレコード
                    Else
                        'エラーが無ければCommit
                        MyBase.CommitTransaction(scope)
                    End If

                End Using
            End If
        Next

        Return setDs

    End Function

#End Region
    'END YANAI 20120319　作業画面改造

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="lme500ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal lme500ds As DataSet) As DataSet

        Dim prtBlc As Com.Base.BaseBLC

        '作業明細書
        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        lme500ds.Merge(New RdPrevInfoDS)

        prtBlc = New LME500BLC()
        lme500ds = MyBase.CallBLC(prtBlc, "DoPrint", lme500ds)

        Return lme500ds

    End Function

    Private Function SelectPrintCheck(ByVal lme010ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallBLC(Me._Blc, "SelectPrintCheck", lme010ds)

        Return rtnDs

    End Function

#End Region


#End Region

End Class
