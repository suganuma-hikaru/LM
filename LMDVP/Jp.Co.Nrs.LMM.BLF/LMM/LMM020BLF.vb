' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM020BLF : 分析票管理マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM020BLC = New LMM020BLC()

    
#End Region

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名(SYS_DATETIMEテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SYS_DATETIME As String = "LMM020TIME"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 分析票管理マスタ更新対象データ検索処理
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

#Region "設定処理"

    ''' <summary>
    ''' 分析票管理マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub InsertData(ByVal ds As DataSet)


        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistCoaM", ds)


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの新規登録
                ds = MyBase.CallBLC(Me._Blc, "InsertCoaM", ds)
            End If
            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

    End Sub

    ''' <summary>
    ''' 分析票管理マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateCoaM", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

    End Sub

    ''' <summary>
    ''' 分析票管理マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaCoaM", ds)

    End Sub


    ''' <summary>
    ''' 分析票管理マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaCoaM", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "DeleteCoaM", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

    End Sub

#End Region

#Region "システム日時取得"

    ''' <summary>
    ''' システム日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMM020BLF.TABLE_NM_SYS_DATETIME)
        dt.Clear()
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dr.Item("SYS_TIME") = MyBase.GetSystemTime()
        dt.Rows.Add(dr)
        Return ds

    End Function

#End Region

#End Region

End Class
