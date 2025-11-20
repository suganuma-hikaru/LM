' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ020BLC : 未使用荷主データ退避
'  作  成  者       :  [kobayashi]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMJ020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"
    'テーブル
    Private Const LMJ020DS_LMJ020IN As String = "LMJ020IN"
    Private Const LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD As String = "LMJ020IN_J_DATA_ESC_LOG_HEAD"
    Private Const LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_DTL As String = "LMJ020IN_J_DATA_ESC_LOG_DTL"
    Private Const LMJ020DS_TARGET_TABLES As String = "TargetTables"

    '区分
    Private Const KBN_SUCCESS As String = "00"
    Private Const KBN_ERROR As String = "90"

    Private Const KBN_JOKYO_PROCESSING As String = "00"   '処理中
    Private Const KBN_JOKYO_PROCESSED As String = "01"    '処理済
    Private Const KBN_KEKKA_SUCCESS As String = "00"      '正常終了
    Private Const KBN_KEKKA_ERROR As String = "90"        '異常終了
    Private Const KBN_TIMING_CHECK As String = "00"   '入力チェック完了
    Private Const KBN_TIMING_MST As String = "10"     'LM_MST退避完了
    Private Const KBN_TIMING_TRN As String = "20"     'LM_TRN退避完了
    Private Const KBN_TIMING_DEL As String = "30"     '元データ削除
    Private Const KBN_TIMING_ENDPROCESS As String = "40" '処理終了

    Private Const MSG_START As String = "処理開始"
    Private Const MSG_PROCESSING As String = "処理中"
    Private Const MSG_END As String = "処理終了"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMJ020BLC = New LMJ020BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
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

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function


#End Region

#Region "データ退避"

    ''' <summary>
    ''' データ退避処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ProcessDataEscape(ByVal ds As DataSet) As DataSet

        Dim custDs As DataSet
        Dim successCnt As Integer = 0
        '全体チェック
        ds = MyBase.CallBLC(Me._Blc, "BasicCheck", ds)

        If MyBase.IsErrorMessageExist = True Then
            Return ds
        End If

        For Each row As DataRow In ds.Tables(LMJ020DS_LMJ020IN).Rows

            custDs = ds.Copy
            custDs.Tables(LMJ020DS_LMJ020IN).Clear()
            custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Clear()
            custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_DTL).Clear()
            '荷主ごとに回す
            custDs.Tables(LMJ020DS_LMJ020IN).ImportRow(row)
            '開始ログ出力
            custDs = Me.StartInputLog(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            '入力チェック
            custDs = MyBase.CallBLC(Me._Blc, "InputCheckTaihi", custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            'ログ出力
            custDs = Me.ProcessInputLog(custDs, KBN_TIMING_CHECK)

            '退避先へ登録処理
            custDs = MyBase.CallBLC(Me._Blc, "EscapeData", custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            'ログ出力
            custDs = Me.ProcessInputLog(custDs, KBN_TIMING_TRN)

            'テーブル削除可能かチェック
            custDs = Me.DeleteDataCheck(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If

            'テーブル削除（トランザクションはテーブルIDのグループ単位）
            custDs = Me.DeleteData(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            'ログ出力
            custDs = Me.ProcessInputLog(custDs, KBN_TIMING_DEL)

            '荷主マスタ削除
            custDs = MyBase.CallBLC(Me._Blc, "UpdateM_CUST_DEL", custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            '正常終了ログ出力
            custDs = Me.EndInputLog(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If

            Me.EndCustProcess(ds, custDs)
            successCnt = successCnt + 1
        Next

        If MyBase.IsMessageStoreExist = True Then
            MyBase.SetMessage("E534", New String() {successCnt.ToString(), "データ退避"})
        Else
            MyBase.SetMessage("G055", New String() {successCnt.ToString(), "データ退避"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 大元のDSにCustDsの内容を移す
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="custDs"></param>
    ''' <remarks></remarks>
    Private Sub EndCustProcess(ByRef ds As DataSet, ByVal custDs As DataSet)

        For Each row As DataRow In custDs.Tables("LMJ020IN_J_DATA_ESC_LOG_HEAD").Rows
            ds.Tables("LMJ020IN_J_DATA_ESC_LOG_HEAD").ImportRow(row)
        Next

        For Each row As DataRow In custDs.Tables("LMJ020IN_J_DATA_ESC_LOG_DTL").Rows
            ds.Tables("LMJ020IN_J_DATA_ESC_LOG_DTL").ImportRow(row)
        Next

    End Sub

    Private Function OutputErrorLog(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "EndInputLog", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ削除チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteDataCheck(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "DeleteDataCheck", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0
        Dim idGroup As String = String.Empty
        Dim transDs As DataSet = Nothing
        Dim roopCnt As Integer = 0
        Me.CopyDsForDelete(ds, transDs)

        For Each row As DataRow In ds.Tables(LMJ020DS_TARGET_TABLES).Rows
            roopCnt = roopCnt + 1
            If String.IsNullOrEmpty(idGroup) = True OrElse idGroup.Equals(row("TableIDGroup").ToString()) = True Then
                '荷主ごとに回す
                transDs.Tables(LMJ020DS_TARGET_TABLES).ImportRow(row)
                idGroup = row("TableIDGroup").ToString()
                If roopCnt <> ds.Tables(LMJ020DS_TARGET_TABLES).Rows.Count Then
                    Continue For
                End If
            End If

            'ここで更新処理
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                transDs = MyBase.CallBLC(Me._Blc, "DeleteData", transDs)
                'エラーログの反映
                Me.CopyDsLogForDelete(transDs, ds)
                If KBN_ERROR.Equals(ds.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                    Return ds
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End Using

            idGroup = row("TableIDGroup").ToString()

            '処理が完了したら次レコードを設定する
            Me.CopyDsForDelete(ds, transDs)
            transDs.Tables(LMJ020DS_TARGET_TABLES).ImportRow(row)
            idGroup = row("TableIDGroup").ToString()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' TargetTablesを抽出し設定
    ''' </summary>
    ''' <param name="motoDs"></param>
    ''' <param name="sakiDs"></param>
    ''' <remarks></remarks>
    Private Sub CopyDsForDelete(ByVal motoDs As DataSet, ByRef sakiDs As DataSet)

        sakiDs = motoDs.Copy
        sakiDs.Tables(LMJ020DS_TARGET_TABLES).Rows.Clear()

    End Sub

    ''' <summary>
    ''' ログデータの反映
    ''' </summary>
    ''' <param name="motoDs"></param>
    ''' <param name="sakiDs"></param>
    ''' <remarks></remarks>
    Private Sub CopyDsLogForDelete(ByVal motoDs As DataSet, ByRef sakiDs As DataSet)

        sakiDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows.Clear()
        sakiDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).ImportRow(motoDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0))
        sakiDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_DTL).Rows.Clear()
        For Each dtlRow As DataRow In motoDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_DTL).Rows
            sakiDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_DTL).ImportRow(dtlRow)
        Next

    End Sub

    ''' <summary>
    ''' スタートログ
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StartInputLog(ByVal ds As DataSet) As DataSet

        ds = Me.SetStartLogDataset(ds)

        ds = MyBase.CallBLC(Me._Blc, "StartInputLog", ds)

        Return ds

    End Function

    ''' <summary>
    ''' スタートログの書き込み
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetStartLogDataset(ByVal ds As DataSet) As DataSet

        'ログの書き込み
        Dim row As DataRow = ds.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).NewRow
        Dim num As New NumberMasterUtility

        row("NRS_BR_CD") = ds.Tables(LMJ020DS_LMJ020IN).Rows(0)("NRS_BR_CD").ToString()
        row("BATCH_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.BATCH_NO, Me, ds.Tables(LMJ020DS_LMJ020IN).Rows(0)("NRS_BR_CD").ToString())
        row("OPE_USER_CD") = MyBase.GetUserID
        row("SYORI_KB") = ds.Tables(LMJ020DS_LMJ020IN).Rows(0)("PROCESS_KB").ToString()
        row("PROC_DATE") = MyBase.GetSystemDate
        row("CUST_CD_L") = ds.Tables(LMJ020DS_LMJ020IN).Rows(0)("CUST_CD_L").ToString()
        row("LAST_UPD_DATE") = ds.Tables(LMJ020DS_LMJ020IN).Rows(0)("LAST_UPD_DATE").ToString()
        row("EXEC_STATE_KB") = KBN_JOKYO_PROCESSING
        row("EXEC_RESULT_KB") = KBN_KEKKA_SUCCESS
        row("MESSAGE") = MSG_START
        row("EXEC_TIMING_KB") = String.Empty
        row("EXEC_START_DATE") = MyBase.GetSystemDate
        row("EXEC_START_TIME") = MyBase.GetSystemTime
        row("EXEC_END_DATE") = String.Empty
        row("EXEC_END_TIME") = String.Empty

        ds.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows.Add(row)

        Return ds

    End Function

    ''' <summary>
    ''' 経過ログ
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessInputLog(ByVal ds As DataSet, ByVal msg As String) As DataSet

        ds = Me.SetProcessLogDataset(ds, msg)

        ds = MyBase.CallBLC(Me._Blc, "EndInputLog", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 経過ログの書き込み
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetProcessLogDataset(ByRef ds As DataSet, ByVal timing As String) As DataSet

        'ログの書き込み
        Dim row As DataRow = ds.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0)

        row("EXEC_STATE_KB") = KBN_JOKYO_PROCESSING
        row("EXEC_RESULT_KB") = KBN_KEKKA_SUCCESS
        row("MESSAGE") = MSG_PROCESSING
        row("EXEC_TIMING_KB") = timing
        row("EXEC_END_DATE") = MyBase.GetSystemDate
        row("EXEC_END_TIME") = Format(Now, "HHmmssfff").ToString()

        Return ds

    End Function

    Private Function EndInputLog(ByVal ds As DataSet) As DataSet

        ds = Me.SetEndLogDataset(ds)

        ds = MyBase.CallBLC(Me._Blc, "EndInputLog", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 終了ログの書き込み
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetEndLogDataset(ByRef ds As DataSet) As DataSet

        'ログの書き込み
        Dim row As DataRow = ds.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0)

        row("EXEC_STATE_KB") = KBN_JOKYO_PROCESSED
        row("EXEC_RESULT_KB") = KBN_KEKKA_SUCCESS
        row("MESSAGE") = MSG_END
        row("EXEC_TIMING_KB") = KBN_TIMING_ENDPROCESS
        row("EXEC_END_DATE") = MyBase.GetSystemDate
        row("EXEC_END_TIME") = Format(Now, "HHmmssfff").ToString()

        Return ds

    End Function

#End Region

#Region "データ戻し"

    ''' <summary>
    ''' データ戻し
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ProcessDataModoshi(ByVal ds As DataSet) As DataSet


        Dim custDs As DataSet
        Dim successCnt As Integer = 0
        '全体チェック
        ds = MyBase.CallBLC(Me._Blc, "BasicCheck", ds)

        If MyBase.IsErrorMessageExist = True Then
            Return ds
        End If

        For Each row As DataRow In ds.Tables(LMJ020DS_LMJ020IN).Rows

            custDs = ds.Copy
            custDs.Tables(LMJ020DS_LMJ020IN).Clear()
            custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Clear()
            custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_DTL).Clear()
            '荷主ごとに回す
            custDs.Tables(LMJ020DS_LMJ020IN).ImportRow(row)
            '開始ログ出力
            custDs = Me.StartInputLog(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            '入力チェック
            custDs = MyBase.CallBLC(Me._Blc, "InputCheckModoshi", custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            'ログ出力
            custDs = Me.ProcessInputLog(custDs, KBN_TIMING_CHECK)

            '本DBへ登録処理
            custDs = MyBase.CallBLC(Me._Blc, "ModoshiData", custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            'ログ出力
            custDs = Me.ProcessInputLog(custDs, KBN_TIMING_TRN)

            '退避テーブル削除可能かチェック（比較は流用）
            custDs = Me.DeleteDataCheck(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If

            '退避テーブル削除（トランザクションはテーブルIDのグループ単位）
            custDs = Me.DeleteModoshiData(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            'ログ出力
            custDs = Me.ProcessInputLog(custDs, KBN_TIMING_DEL)

            '荷主マスタ復活
            custDs = MyBase.CallBLC(Me._Blc, "UpdateM_CUST_REBORN", custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                custDs = Me.OutputErrorLog(custDs)
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If
            '正常終了ログ出力
            custDs = Me.EndInputLog(custDs)
            If KBN_ERROR.Equals(custDs.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                Me.EndCustProcess(ds, custDs)
                Continue For
            End If

            Me.EndCustProcess(ds, custDs)
            successCnt = successCnt + 1
        Next

        If MyBase.IsMessageStoreExist = True Then
            MyBase.SetMessage("E534", New String() {successCnt.ToString(), "データ戻し"})
        Else
            MyBase.SetMessage("G055", New String() {successCnt.ToString(), "データ戻し"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ削除チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteModoshiDataCheck(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "DeleteModoshiDataCheck", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteModoshiData(ByVal ds As DataSet) As DataSet

        Dim rtnCount As Integer = 0
        Dim idGroup As String = String.Empty
        Dim transDs As DataSet = Nothing
        Dim roopCnt As Integer = 0
        Me.CopyDsForDelete(ds, transDs)

        For Each row As DataRow In ds.Tables(LMJ020DS_TARGET_TABLES).Rows
            roopCnt = roopCnt + 1
            If String.IsNullOrEmpty(idGroup) = True OrElse idGroup.Equals(row("TableIDGroup").ToString()) = True Then
                '荷主ごとに回す
                transDs.Tables(LMJ020DS_TARGET_TABLES).ImportRow(row)
                idGroup = row("TableIDGroup").ToString()
                If roopCnt <> ds.Tables(LMJ020DS_TARGET_TABLES).Rows.Count Then
                    Continue For
                End If
            Else

            End If

            'ここで更新処理
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                transDs = MyBase.CallBLC(Me._Blc, "DeleteModoshiData", transDs)
                'エラーログの反映
                Me.CopyDsLogForDelete(transDs, ds)
                If KBN_ERROR.Equals(ds.Tables(LMJ020DS_LMJ020IN_J_DATA_ESC_LOG_HEAD).Rows(0).Item("EXEC_RESULT_KB").ToString()) Then
                    Return ds
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End Using

            idGroup = row("TableIDGroup").ToString()

            '処理が完了したら次レコードを設定する
            Me.CopyDsForDelete(ds, transDs)
            transDs.Tables(LMJ020DS_TARGET_TABLES).ImportRow(row)
            idGroup = row("TableIDGroup").ToString()

        Next

        Return ds

    End Function
#End Region

#End Region

End Class
