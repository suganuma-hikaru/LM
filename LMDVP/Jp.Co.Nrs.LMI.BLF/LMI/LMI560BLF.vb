' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI560BLF : TSMC請求データ計算
'  作  成  者       :  [HORI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI560BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI560BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI560BLC = New LMI560BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
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

#Region "前回計算取消処理"

    ''' <summary>
    ''' 前々回情報の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOldInfo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectOldInfo", ds)

    End Function

    ''' <summary>
    ''' 前回計算取消処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelCalc(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '前回計算取消計算
            ds = MyBase.CallBLC(Me._Blc, "CancelCalc", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "実行処理"

    ''' <summary>
    ''' 実行処理（請求データ計算）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SeikyuCalc(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '請求先のループ
            For i As Integer = 0 To ds.Tables("LMI560IN_CALC").Rows.Count - 1

                '実行用データセットにコピー
                Dim dsCalc As DataSet = ds.Copy()

                '対象請求先の条件のみをセット
                dsCalc.Tables("LMI560IN_CALC").Clear()
                dsCalc.Tables("LMI560IN_CALC").ImportRow(ds.Tables("LMI560IN_CALC").Rows(i))

                'JOB番号の取得
                dsCalc = MyBase.CallBLC(Me._Blc, "SelectJobNo", dsCalc)
                dsCalc.Tables("LMI560IN_CALC").Rows(0).Item("JOB_NO") = dsCalc.Tables("LMI560OUT_JOBNO").Rows(0).Item("JOB_NO").ToString()
                dsCalc.Tables("LMI560IN_CALC").Rows(0).Item("JOB_NO_ORG") = dsCalc.Tables("LMI560OUT_JOBNO").Rows(0).Item("JOB_NO_ORG").ToString()

                '請求データ計算
                dsCalc = MyBase.CallBLC(Me._Blc, "SeikyuCalc", dsCalc)

            Next

            If MyBase.IsMessageStoreExist Then
                ' エラーチェック該当であれば更新確定は行わない。
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class