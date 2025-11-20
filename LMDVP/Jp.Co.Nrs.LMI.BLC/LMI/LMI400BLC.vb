' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400  : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI400BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI400BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI400DAC = New LMI400DAC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 編集時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaChk(ByVal ds As DataSet)

        'DACクラス呼出
        Call MyBase.CallDAC(Me._Dac, "HaitaChk", ds)

    End Sub

#End Region

#Region "削除・復活処理"

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMI400DS
        Dim inDt As DataTable = inDs.Tables("LMI400IN")
        Dim outDt As DataTable = ds.Tables("LMI400OUT")

        For i As Integer = 0 To ds.Tables("LMI400IN").Rows.Count - 1

            inDt.Clear()
            inDt.ImportRow(ds.Tables("LMI400IN").Rows(i))

            '親コードに紐づくデータを取得
            Call MyBase.CallDAC(Me._Dac, "ChkData", inDs)

            For j As Integer = 0 To inDs.Tables("LMI400OUT").Rows.Count - 1

                outDt.Clear()
                outDt.ImportRow(inDs.Tables("LMI400OUT").Rows(j))
                outDt.Rows(0).Item("SYS_DEL_FLG") = inDt.Rows(0).Item("SYS_DEL_FLG")

                Call MyBase.CallDAC(Me._Dac, "DeleteData", ds)

            Next

        Next

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 存在チェック(商品)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ExistChk(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = ds.Copy()
        Dim inDt As DataTable = inDs.Tables("LMI400IN")

        For i As Integer = 0 To ds.Tables("LMI400IN").Rows.Count - 1

            inDt.Clear()
            inDt.ImportRow(ds.Tables("LMI400IN").Rows(i))

            Call MyBase.CallDAC(Me._Dac, "ExistChk", ds)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 保存時存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInsertData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectInsertData", ds)

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertData", ds)

    End Function

    ''' <summary>
    ''' 編集登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DelInsData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = ds.Copy()
        Dim inDt As DataTable = inDs.Tables("LMI400IN")

        '物理削除
        Call MyBase.CallDAC(Me._Dac, "DelData", ds)

        For i As Integer = 0 To ds.Tables("LMI400IN").Rows.Count - 1

            inDt.Clear()
            inDt.ImportRow(ds.Tables("LMI400IN").Rows(i))

            Me.InsertData(inDs)

        Next

        Return ds

    End Function

#End Region

#End Region

End Class
