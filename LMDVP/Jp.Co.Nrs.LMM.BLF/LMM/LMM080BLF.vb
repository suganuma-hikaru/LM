' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM080BLF : 運送会社マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM080BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM080BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM080BLC = New LMM080BLC()

#End Region

#Region "Const"

    Private Const INSERT As String = "InsertSaveAction"
    Private Const UPDATE As String = "UpdateSaveAction"
    Private Const DELETE As String = "DeleteAction"
    Private Const ZIP As String = "CheckZipM"


    ''' <summary>
    ''' データセットテーブル名(SYS_DATETIMEテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_DATE As String = "LMM080DATE"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運送会社マスタ更新対象データ検索処理
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
                Return Me.SetSysDateTime(ds)
            End If

        End If

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectListData", ds)
        Return Me.SetSysDateTime(ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, INSERT)

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, UPDATE)

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, DELETE, False)

    End Function

    ''' <summary>
    ''' 運送会社マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnsocoM", ds)

        Return Me.SetSysDateTime(ds)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        Select Case actionStr

            Case INSERT

                '2011.09.08 検証結果_導入時要望№1対応 START
                ''郵便番号存在チェック
                'ds = Me.BlcAccess(ds, ZIP)
                '2011.09.08 検証結果_導入時要望№1対応 END
                'メッセージの判定
                If MyBase.IsErrorMessageExist() = False Then

                    '存在チェック
                    ds = Me.BlcAccess(ds, "CheckExistUnsocoM")

                End If

            Case UPDATE
                '2011.09.08 検証結果_導入時要望№1対応 START
                ''郵便番号存在チェック
                'ds = Me.BlcAccess(ds, ZIP)
                '2011.09.08 検証結果_導入時要望№1対応 END

            Case Else 'DELETE
        End Select

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'BLCアクセス
                ds = Me.BlcAccess(ds, actionStr)

            End If

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return Me.SetSysDateTime(ds)

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#Region "システム日時取得"

    ''' <summary>
    ''' システム日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMM080BLF.TABLE_NM_DATE)
        dt.Clear()
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dt.Rows.Add(dr)
        Return ds

    End Function

#End Region
#End Region

#End Region

End Class
