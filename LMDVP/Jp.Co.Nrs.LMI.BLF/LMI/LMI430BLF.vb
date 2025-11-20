' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI430BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI430BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI430BLC = New LMI430BLC()

#End Region

#Region "Method"


    ''' <summary>
    ''' 入荷シリンダーデータ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectLoadedInkaCylFileList(ByVal ds As DataSet) As DataSet

        If (MyBase.GetForceOparation() = False) Then

            ds = MyBase.CallBLC(Me._Blc _
                              , LMI430BLC.FUNCTION_NAME.SelectLoadedInkaCylFileListCount _
                              , ds)

            'メッセージの判定
            If (MyBase.IsMessageExist()) Then
                Return ds
            End If

        End If

        ' 入荷シリンダーデータ取得
        Return MyBase.CallBLC(Me._Blc _
                            , LMI430BLC.FUNCTION_NAME.SelectLoadedInkaCylFileList _
                            , ds)

    End Function


    ''' <summary>
    ''' 入荷シリンダーデータ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInspectionData(ByVal ds As DataSet) As DataSet


        ' 入荷シリンダーデータ取得
        Return MyBase.CallBLC(Me._Blc _
                            , LMI430BLC.FUNCTION_NAME.SelectInspectionData _
                            , ds)

    End Function

    ''' <summary>
    ''' 入荷シリンダー追加
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertCylinderData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '新規登録処理を行う
            ds = MyBase.CallBLC(Me._Blc _
                              , LMI430BLC.FUNCTION_NAME.InsertCylinderData _
                              , ds)

            If (IsMessageExist()) Then

                ' ロールバック
                Return ds
            End If

            'コミット
            MyBase.CommitTransaction(scope)

        End Using
        'トランザクション終了


        Return ds

    End Function


    ''' <summary>
    ''' 入荷シリンダー削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteCylinderData(ByVal ds As DataSet) As DataSet
        Dim wkData As DataSet = ds.Clone
        Try

            For Each row As DataRow In ds.Tables(LMI430BLC.TABLE_NAME.INPUT).Rows

                wkData.Clear()
                wkData.Tables(LMI430BLC.TABLE_NAME.INPUT).ImportRow(row)
                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    ' Mの更新成否は異常終了以外考慮しない
                    wkData = MyBase.CallBLC(Me._Blc, LMI430BLC.FUNCTION_NAME.SoftDeleteInkaCylinderM, wkData)

                    wkData = MyBase.CallBLC(Me._Blc, LMI430BLC.FUNCTION_NAME.SoftDeleteInkaCylinderL, wkData)
                    If (MyBase.GetResultCount = 0) Then

                        Me.SetMessageStore(LMI430BLC.GUIDANCE_ID _
                                        , "E011" _
                                        , Nothing _
                                        , row.Item(LMI430BLC.INPUT_COLUMN_NM.SPREAD_ROW_NO).ToString())
                        Continue For
                    Else
                        'コミット
                        MyBase.CommitTransaction(scope)
                    End If
                End Using
            Next

        Finally
            wkData.Dispose()
        End Try


        Return ds

    End Function


    ''' <summary>
    ''' 読取結果データ取得処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectReadResulData(ByVal ds As DataSet) As DataSet


        ' 読取結果取得
        Return MyBase.CallBLC(Me._Blc _
                            , LMI430BLC.FUNCTION_NAME.SelectReadResulData _
                            , ds)

    End Function

#End Region

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

End Class