' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM360BLF : 請求先テンプレートマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM360BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM360BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM360BLC = New LMM360BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 請求先テンプレートマスタ更新対象データ検索処理
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

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 請求先テンプレートマスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub InsertData(ByVal ds As DataSet)

        
        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistSeiqTemplateM", ds)


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの新規登録
                ds = MyBase.CallBLC(Me._Blc, "InsertSeiqTemplateM", ds)
            End If
            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

    End Sub

    ''' <summary>
    ''' 請求先テンプレートマスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateSeiqTemplateM", ds)
            'メッセージの判定

            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

            
        End Using

    End Sub

    ''' <summary>
    ''' 請求先テンプレートマスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSeiqTemplateM", ds)

    End Sub

#If True Then 'ADD 2022/11/10 025485 【LMS】ABP_指摘・要望-69_荷主未登録請求先で製品セグメントが空(ABP大野)

    ''' <summary>
    ''' 請求先テンプレート請求先チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub CHK_SeiqtoCd(ByVal ds As DataSet)

        '請求先コードチェックチェック
        'Dim GROUP_KB As String = ds.Tables("LMM360IN").Rows(0).Item("GROUP_KB").ToString
        Dim CUST_CHK_SQL_NO As String = ds.Tables("LMM360IN").Rows(0).Item("CUST_CHK_SQL_NO").ToString

        'Select Case GROUP_KB
        '    Case "01", "08", "13"
        '        ds = MyBase.CallBLC(Me._Blc, "CheckCust_HOKAN", ds)
        '    Case "02", "09"
        '        ds = MyBase.CallBLC(Me._Blc, "CheckCust_NIYAKU", ds)
        '    Case "03", "05", "10", "12"
        '        ds = MyBase.CallBLC(Me._Blc, "CheckCust_UNCHIN", ds)
        '    Case "04", "11"
        '        ds = MyBase.CallBLC(Me._Blc, "CheckCust_SAGYO", ds)
        '    Case "06", "07", "14", "15", "99"
        '        ds = MyBase.CallBLC(Me._Blc, "CheckCust_OYA", ds)
        Select Case CUST_CHK_SQL_NO
            Case "1"
                ds = MyBase.CallBLC(Me._Blc, "CheckCust_HOKAN", ds)
            Case "2"
                ds = MyBase.CallBLC(Me._Blc, "CheckCust_NIYAKU", ds)
            Case "3"
                ds = MyBase.CallBLC(Me._Blc, "CheckCust_UNCHIN", ds)
            Case "4"
                ds = MyBase.CallBLC(Me._Blc, "CheckCust_SAGYO", ds)
            Case "5"
                ds = MyBase.CallBLC(Me._Blc, "CheckCust_OYA", ds)
            Case Else
                'CUST_CHK_SQL_NO不明でチェックできない
                MyBase.SetMessage("E02J", New String() {"区分マスタS024", "CUST_CHK_SQL_No.(KBN_NM2)", "SQL No."})
        End Select

    End Sub
#End If

    ''' <summary>
    ''' 請求先テンプレートマスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteSeiqTemplateM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If


        End Using

    End Sub

#End Region

#End Region

End Class
