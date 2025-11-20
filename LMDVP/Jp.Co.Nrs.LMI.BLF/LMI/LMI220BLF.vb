' ==========================================================================
'  システム名       :  LMrowIndex'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI220  : 定期検査管理
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI220BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI220BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI220BLC = New LMI220BLC()

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#End Region

#Region "Method"

#Region "HaitaData"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック処理 
        ds = MyBase.CallBLC(Me._Blc, "HaitaData", ds)

        Return ds

    End Function

#End Region



#Region "SelectListData"

    ''' <summary>
    ''' 検索データ取得処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索データ取得処理 
        ds = MyBase.CallBLC(Me._Blc, "SelectListData", ds)

        Return ds

    End Function

#End Region

#Region "IsDataExistChk"

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function IsDataExistChk(ByVal ds As DataSet) As DataSet

        'データ存在チェック処理 
        ds = MyBase.CallBLC(Me._Blc, "IsDataExistChk", ds)

        Return ds

    End Function

#End Region

#Region "InsertData"

    ''' <summary>
    ''' 保存（新規登録）処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '保存処理 
        ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)

        Return ds

    End Function

#End Region

#Region "UpdateData"

    ''' <summary>
    ''' 保存（更新）処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        '保存処理 
        ds = MyBase.CallBLC(Me._Blc, "UpdateData", ds)

        Return ds

    End Function

#End Region

#Region "DeleteData"

    ''' <summary>
    ''' 削除処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '削除処理 
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)

            'エラーがあるかを判定
            If MyBase.IsMessageExist() = False Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region

#Region "ReviveData"

    ''' <summary>
    ''' 復活処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ReviveData(ByVal ds As DataSet) As DataSet

        '復活処理 
        ds = MyBase.CallBLC(Me._Blc, "ReviveData", ds)

        Return ds

    End Function

#End Region

#Region "ImportData"

    ''' <summary>
    ''' 取込更新処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ImportData(ByVal ds As DataSet) As DataSet
        Dim inDs As DataSet = Nothing
        Dim rowIndex As Integer = 1

        '存在チェック
        For Each dr As DataRow In ds.Tables("LMI220IN").Rows
            inDs = New LMI220DS
            inDs.Tables("LMI220IN").ImportRow(dr)
            '更新対象だけ処理する
            If inDs.Tables("LMI220IN").Rows(0).Item("UP_OK_FLG").Equals("1") = True Then

                inDs = MyBase.CallBLC(Me._Blc, "IsDataExistChk", inDs)

                '存在チェックで存在しない場合はInsert　存在したらUpdate

                If inDs.Tables("LMI220OUT").Rows.Count = 0 Then
                    '新規登録
                    ds = MyBase.CallBLC(Me._Blc, "InsertData", inDs)
                    'MyBase.SetMessageStore("00", "E079", New String() {"定期検査管理マスタ", String.Concat("シリアル番号:", dr.Item("SERIAL_NO").ToString(), " ")}, rowIndex.ToString())
                Else
                    '更新処理
                    ds = MyBase.CallBLC(Me._Blc, "ImportData", inDs)
                End If

            End If

            rowIndex = rowIndex + 1
        Next
        '保存処理 
        'If IsMessageStoreExist() = False Then
        '    ds = MyBase.CallBLC(Me._Blc, "ImportData", ds)
        'End If

        Return ds

    End Function

#End Region

#End Region

End Class