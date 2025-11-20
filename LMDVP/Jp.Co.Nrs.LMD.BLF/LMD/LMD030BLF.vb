' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD030BLC : 在庫履歴
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMD030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD030BLC = New LMD030BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectListData", ds)

        Return ds

    End Function

#End Region '検索処理

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNm As String = "LMD030IN_ZAI_DEL"

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '在庫トランザクション,在庫データ（元）,在庫データ（先）の削除処理を行う
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)

            If MyBase.IsMessageExist() = False Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
