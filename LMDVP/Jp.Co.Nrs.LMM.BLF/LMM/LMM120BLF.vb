' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM120BLF : 単価マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMM120BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM120BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM120BLC = New LMM120BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''編集時チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "HaitaChk", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)
            If IsMessageExist() = True Then
                Exit Sub
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 単価マスタ検索処理
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

#Region "保存処理"

#If True Then   'ADD 2020/12/23 017521　【LMS】単価マスタエラー通知仕様追加
    ''' <summary>
    '''　単価マスタコードチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function CheckTankaM_UP_GP_CD_1(ByVal ds As DataSet) As DataSet

        '単価マスタコードチェック
        ds = MyBase.CallBLC(Me._Blc, "CheckTankaM_UP_GP_CD_1", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        Return ds

    End Function

#End If

    ''' <summary>
    ''' 保存処理(単価マスタ新規登録/更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'レコード番号の取得
            If Me.SetRecNo(ds) = True Then

                '新規登録時チェックを行う
                If ChkInsertData(ds) = False Then
                    Return ds
                End If

                '新規登録処理を行う
                ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

            Else

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSaveData", ds)

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録処理の場合、レコードNoを採番する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>TRUE:新規登録、FALSE:更新登録</returns>
    ''' <remarks></remarks>
    Private Function SetRecNo(ByVal ds As DataSet) As Boolean

        Dim dtIn As DataTable = ds.Tables("LMM120IN")

        If String.IsNullOrEmpty(dtIn.Rows(0).Item("REC_NO").ToString()) = False Then
            Return False
        End If

        'レコード番号を新規採番する
        Dim brCd As String = dtIn.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim recNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.TANKA_REC_NO, Me, brCd)

        '新規登録内容を設定する
        dtIn.Rows(0).Item("REC_NO") = recNo

        Return True

    End Function

    ''' <summary>
    ''' 新規登録時チェック処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function ChkInsertData(ByVal ds As DataSet) As Boolean

        '単価マスタ存在チェック
        ds = MyBase.CallBLC(Me._Blc, "ExistTankaM", ds)
        If MyBase.IsMessageExist() Then
            Return False
        End If

        '適用開始日チェック(新規登録時)
        ds = MyBase.CallBLC(Me._Blc, "ChkStartDate", ds)
        If MyBase.IsMessageExist() Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "承認処理"

    ''' <summary>
    ''' 承認処理（申請、承認、差し戻し）
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub ApprovalData(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "ApprovalData", ds)
            If IsMessageExist() = True Then
                Exit Sub
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectComboSeihin", ds)

    End Function

#End Region

#End Region

End Class
