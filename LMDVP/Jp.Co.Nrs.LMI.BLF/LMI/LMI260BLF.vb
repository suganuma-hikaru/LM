' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI260  : 引取運賃明細入力
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI260BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI260BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI260BLC = New LMI260BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkBlc As LMI630BLC = New LMI630BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _MeisaiBlc As LMI640BLC = New LMI640BLC()

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
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "SelectInsertData", ds)

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '新規登録処理
        Return MyBase.CallBLC(Me._Blc, "InsertData", ds)

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "UpdateData", ds)

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 引取運賃明細チェックリスト
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function ChkPrint(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._ChkBlc, "DoPrint", ds)

    End Function

    ''' <summary>
    ''' 引取運賃明細
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function MeisaiPrint(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._MeisaiBlc, "DoPrint", ds)

    End Function

#End Region

#End Region

End Class