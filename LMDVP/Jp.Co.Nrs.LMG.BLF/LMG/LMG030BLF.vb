' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG030BLF : 保管料荷役料明細編集
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMG030BLC = New LMG030BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print500 As LMG500BLC = New LMG500BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '変動保管料情報検索
        ds = MyBase.CallBLC(Me._Blc, "SelectVarStrage", ds)

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

#Region "更新処理"

    ''' <summary>
    ''' 保管荷役明細印刷テーブル更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpDateTable(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = MyBase.CallBLC(Me._Blc, "UpDateTable", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 保管荷役明細印刷テーブル一括変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IkkatuUpDateTable(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMG030IN_IKKATU_UPDATE").Rows.Count - 1
        Dim rtnDs As DataSet = Nothing

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMG030IN_IKKATU_UPDATE")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMG030IN_IKKATU_UPDATE").Rows(i))

                'BLCアクセス
                rtnDs = MyBase.CallBLC(Me._Blc, "IkkatuUpDateTable", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = True Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function
    'END YANAI 20111014 一括変更追加

#End Region

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Acquisithion(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = MyBase.CallBLC(Me._Blc, "Acquisithion", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 取込排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AcquisithionHaita(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "AcquisithionHaita", ds)

    End Function
#End Region

#Region "排他処理"

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "CheckHaita", ds)

        Return ds
    End Function


#End Region

#Region "印刷"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print500, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

#End Region

#End Region

End Class