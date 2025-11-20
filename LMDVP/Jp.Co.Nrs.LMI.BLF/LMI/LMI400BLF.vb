' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400  : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI400BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI400BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI400BLC = New LMI400BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc584 As LMH584BLC = New LMH584BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''編集時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub EditChk(ByVal ds As DataSet)

        'BLCクラス呼出
        Call MyBase.CallBLC(Me._Blc, "HaitaChk", ds)

    End Sub

#End Region

#Region "削除・復活処理"

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "DeleteData", ds)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ExistChk(ByVal ds As DataSet) As DataSet

        '存在チェック
        Return MyBase.CallBLC(Me._Blc, "ExistChk", ds)

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = ds.Copy()
        Dim inDt As DataTable = inDs.Tables("LMI400IN")

        '存在チェック
        Call MyBase.CallBLC(Me._Blc, "SelectInsertData", inDs)

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        For i As Integer = 0 To ds.Tables("LMI400IN").Rows.Count - 1

            inDt.Clear()
            inDt.ImportRow(ds.Tables("LMI400IN").Rows(i))

            '新規登録処理
            Call MyBase.CallBLC(Me._Blc, "InsertData", inDs)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DelInsData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "DelInsData", ds)

    End Function

#End Region

#End Region

End Class