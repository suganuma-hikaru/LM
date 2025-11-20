' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM550BLF : 下払いタリフマスタメンテ
'  作  成  者       :  matsumoto
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM550BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM550BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM550BLC = New LMM550BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 支払運賃タリフマスタ更新対象データ検索処理
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

    ''' <summary>
    ''' 支払運賃タリフマスタExcelデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExcelDataMake(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "ExcelDataMake", ds)

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

        Return Me.ScopeStartEnd(ds, "InsertSaveAction")

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "UpdateSaveAction")

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "DeleteAction", False)

    End Function

    ''' <summary>
    ''' 支払運賃タリフマスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        Return MyBase.CallBLC(Me._Blc, "CheckHaitaShiharaiM", ds)

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

        If actionStr.Equals("InsertSaveAction") = True Then
            '存在チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckExistShiharaiM", ds)
        ElseIf actionStr.Equals("DeleteAction") = True Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShiharaiM", ds)
        End If

        If actionStr.Equals("DeleteAction") = False Then
            '支払タリフコード・2次タリフコードの関連チェック
            If String.IsNullOrEmpty(ds.Tables("LMM550_KYORI").Rows(0).Item("SHIHARAI_TARIFF_CD2").ToString()) = False Then
                If MyBase.IsErrorMessageExist() = False Then
                    '支払タリフコード・2次タリフコード(世代チェック_①)
                    ds = MyBase.CallBLC(Me._Blc, "CheckExistShiharaiSedai1", ds)
                End If
                If MyBase.IsErrorMessageExist() = False Then
                    '支払タリフコード・2次タリフコード(世代チェック_②)
                    ds = MyBase.CallBLC(Me._Blc, "CheckExistShiharaiSedai2", ds)

                End If
                If MyBase.IsErrorMessageExist() = False Then
                    '支払タリフコード・2次タリフコード(整合性チェック)
                    ds = MyBase.CallBLC(Me._Blc, "CheckExistShiharaiSeigo", ds)
                End If
            End If
        End If

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

        Return ds

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

#End Region

#End Region

End Class
