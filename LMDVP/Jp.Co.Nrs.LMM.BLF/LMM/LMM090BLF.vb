' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM090BLF : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMM090BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM090BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM090BLC = New LMM090BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''編集時チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "HaitaChk", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        '在庫データ存在チェック
        ds = MyBase.CallBLC(Me._Blc, "ZaikoExistChk", ds)

        Return ds

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '在庫データ存在チェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ChkZaiT", ds)
        If MyBase.IsMessageExist() Then
            Exit Sub
        End If

        '更新対象レコードのチェックを行う
        ds = MyBase.CallBLC(Me._Blc, "ChkUpdateData", ds)
        If MyBase.IsMessageExist() Then
            Exit Sub
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)
            If IsMessageExist() = True Then
                Exit Sub
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 荷主マスタ検索処理
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
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectVarStrage", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 新規登録処理(荷主マスタ、運賃タリフセットマスタ、荷主別帳票マスタ、荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        ''2011.09.08 検証結果_導入時要望№1対応 START
        ''保存時共通チェック
        'If Me.ChkSaveCommon(ds) = False Then
        '    Return ds
        'End If
        ''2011.09.08 検証結果_導入時要望№1対応 END

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '荷主コードの取得/新規、複写時チェック
            Me.SetCustCd(ds)
            If IsMessageExist() = True Then
                Return ds
            End If

            '新規登録処理を行う
            ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)
            If IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理(荷主マスタ、運賃タリフセットマスタ、荷主別帳票マスタ、荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        ''2011.09.08 検証結果_導入時要望№1対応 START
        ''保存時共通チェック
        'If Me.ChkSaveCommon(ds) = False Then
        '    Return ds
        'End If
        ''2011.09.08 検証結果_導入時要望№1対応 END

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "HaitaChk", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateSaveData", ds)
            If IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録処理の場合、荷主コードを採番する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetCustCd(ByVal ds As DataSet)

        Dim drIn As DataRow = ds.Tables("LMM090IN").Rows(0)

        If String.IsNullOrEmpty(drIn.Item("CUST_CD_L").ToString()) = False _
        AndAlso String.IsNullOrEmpty(drIn.Item("CUST_CD_M").ToString()) = False _
        AndAlso String.IsNullOrEmpty(drIn.Item("CUST_CD_S").ToString()) = False _
        AndAlso String.IsNullOrEmpty(drIn.Item("CUST_CD_SS").ToString()) = False Then
            '荷主マスタ存在チェック
            ds = MyBase.CallBLC(Me._Blc, "ExistCustM", ds)
            Exit Sub
        End If

        '荷主コードを新規採番する
        ds = MyBase.CallBLC(Me._Blc, "GetCustCd", ds)

        '取得結果を各テーブルに設定する
        Dim drOut As DataRow = ds.Tables("LMM090OUT").Rows(0)
        Dim dtTariff As DataTable = ds.Tables("LMM090_TARIFF_SET")
        Dim dtCustPrt As DataTable = ds.Tables("LMM090_CUST_RPT")
        '要望番号:349 yamanaka 2012.07.26 Start
        Dim dtCustDetail As DataTable = ds.Tables("LMM090_CUST_DETAILS")
        ''要望番号:349 yamanaka 2012.07.26 End

        Dim custCdL As String = drOut.Item("CUST_CD_L").ToString()
        Dim custCdM As String = drOut.Item("CUST_CD_M").ToString()
        Dim custCdS As String = drOut.Item("CUST_CD_S").ToString()
        Dim custCdSS As String = drOut.Item("CUST_CD_SS").ToString()

        '新規登録内容を設定する
        '荷主マスタ
        drIn.Item("CUST_CD_M") = custCdM
        drIn.Item("CUST_CD_S") = custCdS
        drIn.Item("CUST_CD_SS") = custCdSS
        '運賃タリフセットマスタ
        Dim maxTariff As Integer = dtTariff.Rows.Count - 1
        For i As Integer = 0 To maxTariff
            dtTariff.Rows(i).Item("CUST_CD_M") = custCdM
        Next

        '荷主別帳票マスタ
        Dim maxPrt As Integer = dtCustPrt.Rows.Count - 1
        For i As Integer = 0 To maxPrt
            dtCustPrt.Rows(i).Item("CUST_CD_M") = custCdM
            dtCustPrt.Rows(i).Item("CUST_CD_S") = custCdS
        Next

        '要望番号:349 yamanaka 2012.07.26 Start
        '荷主明細マスタ
        Dim maxDtl As Integer = dtCustDetail.Rows.Count - 1
        Dim custCdDtlL As String = drIn.Item("CUST_CD_L").ToString()
        For i As Integer = 0 To maxDtl
            If dtCustDetail.Rows(i).Item("CUST_CD").Equals("M") Then
                dtCustDetail.Rows(i).Item("CUST_CD") = String.Concat(custCdDtlL, custCdM)
            ElseIf dtCustDetail.Rows(i).Item("CUST_CD").Equals("S") Then
                dtCustDetail.Rows(i).Item("CUST_CD") = String.Concat(custCdDtlL, custCdM, custCdS)
            ElseIf dtCustDetail.Rows(i).Item("CUST_CD").Equals("SS") Then
                dtCustDetail.Rows(i).Item("CUST_CD") = String.Concat(custCdDtlL, custCdM, custCdS, custCdSS)
            End If
        Next
        '要望番号:349 yamanaka 2012.07.26 End


    End Sub

    ''2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 保存時共通チェック
    '''' </summary>
    '''' <param name="ds"></param>
    '''' <returns>True:エラーなし、False:エラー有り</returns>
    '''' <remarks></remarks>
    'Private Function ChkSaveCommon(ByVal ds As DataSet) As Boolean

    '    '郵便番号マスタ存在チェック
    '    ds = MyBase.CallBLC(Me._Blc, "ChkZipM", ds)
    '    If MyBase.IsMessageExist() Then
    '        Return False
    '    End If

    '    Return True

    'End Function
    ''2011.09.08 検証結果_導入時要望№1対応 END

#End Region

#Region "契約通貨コンボ取得"

    ''' <summary>
    ''' 契約通貨コンボ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectComboItemCurrCd(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectComboItemCurrCd", ds)

    End Function

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectComboSeihin", ds)

    End Function

#End Region

#End Region

End Class