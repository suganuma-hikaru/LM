' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU010BLC : 文書管理画面
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

Public Class LMU010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMU010BLC = New LMU010BLC()

#End Region

    ''' <summary>
    ''' データ情報一覧検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataList(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If Me.GetForceOparation = False Then

            'データ件数取得
            ds = Me.CallBLC(New LMU010BLC, "SelectDataCount", ds)
            'メッセージの判定
            If Me.IsMessageExist = True Then
                ds.Clear()
                Return ds
            End If

        End If

        '検索結果取得
        ds = Me.CallBLC(New LMU010BLC, "SelectDataList", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function DeleteListData(ByVal ds As DataSet) As DataSet

        Dim blc As LMU010BLC = New LMU010BLC()

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '削除処理
            ds = MyBase.CallBLC(blc, "DeleteListData", ds)

            'エラーがない場合、Commit
            If MyBase.IsMessageExist() = False Then

                'トランザクション終了
                Me.CommitTransaction(scope)

            End If

        End Using

        '検索結果取得
        ds = Me.CallBLC(blc, "SelectDataList", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function chkDataListHaita(ByVal ds As DataSet) As DataSet

        '検索結果取得
        ds = Me.CallBLC(New LMU010BLC, "chkDataListHaita", ds)

        'DataSet初期化
        ds.Tables("LMU010IN").Clear()
        Return ds

    End Function

    ''' <summary>
    ''' データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveDataList(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = Me.BeginTransaction()

            Dim dsTmp As DataSet = ds.Clone

            For i As Integer = 0 To ds.Tables("M_FILE").Rows.Count - 1

                dsTmp.Clear()
                dsTmp.Tables("M_FILE").ImportRow(ds.Tables("M_FILE").Rows(i))

                With ds.Tables("M_FILE").Rows(i)

                    If String.IsNullOrEmpty(.Item("UPD_FLG").ToString) = False AndAlso _
                       .Item("UPD_FLG").ToString.Equals("0") = True AndAlso _
                       .Item("SYS_DEL_FLG").ToString.Equals("1") = False Then

                        '[データ登録]
                        'M_FILE
                        Me.CallBLC(New LMU010BLC, "InsertData", dsTmp.Copy)
                        If Me.IsMessageExist = True Then
                            Return ds
                        End If

                    ElseIf String.IsNullOrEmpty(.Item("UPD_FLG").ToString) = False AndAlso _
                           (.Item("UPD_FLG").ToString.Equals("1") = True OrElse .Item("UPD_FLG").ToString.Equals("2") = True) AndAlso _
                           String.IsNullOrEmpty(.Item("SYS_DEL_FLG").ToString) = False AndAlso _
                           .Item("SYS_DEL_FLG").ToString.Equals("1") = True Then

                        '[排他チェック]
                        Me.chkDataListHaita(dsTmp.Copy)
                        If Me.IsMessageExist = True Then
                            Return ds
                        End If

                        '[データ論理削除]
                        'M_FILE
                        Me.CallBLC(New LMU010BLC, "DeleteData", dsTmp)
                        If Me.IsMessageExist = True Then
                            Return ds
                        End If

                    ElseIf String.IsNullOrEmpty(.Item("UPD_FLG").ToString) = False AndAlso _
                           .Item("UPD_FLG").ToString.Equals("2") = True AndAlso _
                           String.IsNullOrEmpty(.Item("SYS_DEL_FLG").ToString) = False AndAlso _
                           .Item("SYS_DEL_FLG").ToString.Equals("0") = True Then

                        '[排他チェック]
                        Me.chkDataListHaita(dsTmp.Copy)
                        If Me.IsMessageExist = True Then
                            Return ds
                        End If

                        '[データ更新]
                        'M_FILE
                        Me.CallBLC(New LMU010BLC, "UpdateData", dsTmp)
                        If Me.IsMessageExist = True Then
                            Return ds
                        End If

                    End If

                    '[データ更新]
                    'WK_WEB_ORDER_HED
                    If .Item("FILE_TYPE_KBN").ToString.Equals("L1") AndAlso _
                        .Item("UPD_FLG").ToString.Equals("0") Then

                        Me.CallBLC(New LMU010BLC, "UpdateWebData", dsTmp)

                    End If

                End With

            Next

            'トランザクション終了
            Me.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#Region "検索処理"

    ''' <summary>
    ''' コンボ作成データ取得　2015/1/6 大野ｱﾙﾍﾞ対応
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

End Class
