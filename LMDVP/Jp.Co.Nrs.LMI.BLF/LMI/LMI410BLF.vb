' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI410BLF : ビックケミー取込データ確認／報告
'  作  成  者       :  [Umano]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI410BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI410BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI410BLC = New LMI410BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' BYK取込データ検索処理
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

#Region "実行処理(実績報告作成)"

    ''' <summary>
    ''' 実行(実績報告データ作成)処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function JIssekiData(ByVal ds As DataSet) As DataSet

        '実行種別を取得
        Dim dt As DataTable = ds.Tables("LMI410IN")
        Dim dr As DataRow = dt.Rows(0)

        Dim jissekiKbn As String = dr.Item("CMB_JISSEKI").ToString()

        Select Case jissekiKbn

            Case "01"
                'BYK出荷報告作成
                '呼び出し(出荷報告データ抽出)
                ds = MyBase.CallBLC(Me._Blc, "OutkaHokoku", ds)

                '呼び出し(出荷報告データ作成・出荷ステージUP)
                ds = Me.UpdateStatus(ds)

                '出荷報告作成数チェック
                ds = Me.registrationResultCountCheck(ds)


            Case "02"
                'BYK入荷報告作成

                '呼び出し(入荷報告データ抽出)
                ds = MyBase.CallBLC(Me._Blc, "InkaHokoku", ds)

                '呼び出し(入荷報告データ作成・入荷ステージUP)
                ds = Me.UpdateInStatus(ds)

        End Select

        Return ds

    End Function

#End Region

#Region "Method"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateStatus(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "H_SENDOUTEDI_BYK"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)

        '出荷管理番号の重複を除いたDataSetを用意
        Dim view As New DataView(dt)
        Dim dtFilter As datatable = view.ToTable(True, "OUTKA_CTL_NO")
        Dim max As Integer = dtFilter.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        For i As Integer = 0 To max
            '出荷管理番号単位で更新処理を行う
            Dim selectDrCollection As DataRow() = dt.Select(String.Concat("OUTKA_CTL_NO = '" + _
                                                                dtFilter.Rows(i).Item("OUTKA_CTL_NO").ToString() + "'"))

            '更新情報の設定
            setDt.Clear()

            For Each selectDr As DataRow In selectDrCollection
                setDt.ImportRow(selectDr)
            Next

            '更新処理
            If Me.UpdateOutkaDataAction(setDs) = False Then
                Continue For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷報告作成時、更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaDataAction(ByVal ds As DataSet) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "H_SENDOUTEDI_BYK"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                '出荷報告データの更新
                ds = MyBase.CallBLC(Me._Blc, "InsertSendOutBykAgtData", ds)

                '出荷データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateOutkaData", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist() = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function

    ''' <summary>
    ''' 登録した出荷報告データ数チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function registrationResultCountCheck(ByVal ds As DataSet) As DataSet

        'カウントの取得
        ds = MyBase.CallBLC(Me._Blc, "CheckSendOutBykAgtData", ds)
        Dim resultCount As Integer = Int32.Parse(ds.Tables("LMI410CNT").Rows(0).Item("CNT").ToString)

        'チェック結果で1件でもレコードがヒットしたとき
        If resultCount > 0 Then
            'メッセージ表示処理
            MyBase.SetMessage("E980")
            Return ds

        End If

        Return ds
    End Function


    ''' <summary>
    ''' 更新処理(BYK入荷報告作成)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInStatus(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "H_SENDINOUTEDI_BYK"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        Dim preNrsBrCd As String = String.Empty
        Dim preEdiCtlNo As String = String.Empty
        Dim preOrder As String = String.Empty

        Dim preCrtDate As String = String.Empty
        Dim preFileName As String = String.Empty
        Dim preRecNo As String = String.Empty
        Dim preKey As String = String.Empty

        'Dim preGyo As String = String.Empty

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        For i As Integer = 0 To max

            '更新情報の設定
            setDt.Clear()
            setDt.ImportRow(dt.Rows(i))

            '更新処理
            If Me.UpdateInkaDataAction(setDs, preOrder, preKey) = False Then
                preNrsBrCd = dt.Rows(i).Item("NRS_BR_CD").ToString()
                preEdiCtlNo = dt.Rows(i).Item("EDI_CTL_NO").ToString()
                preOrder = String.Concat(preNrsBrCd, preEdiCtlNo)

                preCrtDate = dt.Rows(i).Item("CRT_DATE").ToString()
                preFileName = dt.Rows(i).Item("FILE_NAME").ToString()
                preRecNo = dt.Rows(i).Item("REC_NO").ToString()
                preKey = String.Concat(preCrtDate, preFileName, preRecNo)


                Continue For
            End If

            preNrsBrCd = dt.Rows(i).Item("NRS_BR_CD").ToString()
            preEdiCtlNo = dt.Rows(i).Item("EDI_CTL_NO").ToString()
            preOrder = String.Concat(preNrsBrCd, preEdiCtlNo)

            preCrtDate = dt.Rows(i).Item("CRT_DATE").ToString()
            preFileName = dt.Rows(i).Item("FILE_NAME").ToString()
            preRecNo = dt.Rows(i).Item("REC_NO").ToString()
            preKey = String.Concat(preCrtDate, preFileName, preRecNo)

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 入荷報告作成時、更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaDataAction(ByVal ds As DataSet, ByVal preOrder As String, ByVal preKey As String) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "H_SENDINOUTEDI_BYK"
        Dim updDt As DataTable = ds.Tables(tableNm)


        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                'If String.IsNullOrEmpty(preGyo) = True Then
                ' preGyo = dr("GYO").ToString()
                'End If

                '                If preKey.Equals(String.Concat(dr("CRT_DATE"), dr("FILE_NAME"), dr("REC_NO"))) = True Then
                'dr("GYO") = Format(Convert.ToInt32(preGyo) + 1, "000")
                'preGyo = dr("GYO").ToString()
                'Else
                '    preGyo = String.Empty
                'End If

                '入荷報告データの更新
                ds = MyBase.CallBLC(Me._Blc, "InsertSendInOutBykData", ds)

                '2015.08.25 追加START
                If String.IsNullOrEmpty(preOrder) = True OrElse preOrder.Equals(String.Concat(dr("NRS_BR_CD"), dr("EDI_CTL_NO"))) = False Then

                    'EDI入荷(大)データの更新
                    ds = MyBase.CallBLC(Me._Blc, "UpdateInkaEdiLData", ds)

                    'EDI入荷(中)データの更新
                    ds = MyBase.CallBLC(Me._Blc, "UpdateInkaEdiMData", ds)

                    'BYK入荷EDI受信データ(明細)の更新
                    ds = MyBase.CallBLC(Me._Blc, "UpdateInkaEdiDtlBykData", ds)

                End If
                '2015.08.25 追加END

                '入荷データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateInkaData", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist() = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function

#End Region

#Region "取込処理(BYK倉庫間移動データ作成)"

    ''' <summary>
    ''' 取込(BYK倉庫間移動データ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function TorikomiByk(ByVal ds As DataSet) As DataSet

        Using scope As TransactionScope = MyBase.BeginTransaction()

            MyBase.SetMessage(Nothing)

            '新規登録処理を行う
            ds = MyBase.CallBLC(Me._Blc, "InsertIdoEdiData", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "移動報告処理(移動実績報告作成)"

    ''' <summary>
    ''' 移動報告(移動実績報告データ作成)処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function IdoHokoku(ByVal ds As DataSet) As DataSet

        '実行種別を取得
        Dim dt As DataTable = ds.Tables("LMI410IN_IDO_HOKOKU")
        Dim dr As DataRow = dt.Rows(0)

        'BYK移動報告作成

        '呼び出し(移動報告データ抽出)
        ds = MyBase.CallBLC(Me._Blc, "IdoHokoku", ds)

        '呼び出し(BYK移動EDI受信データ ステージUP)
        ds = Me.UpdateIdoStatus(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理(BYK移動(入荷)報告作成)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateIdoStatus(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "H_SENDINOUTEDI_BYK"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim crtDate As String = String.Empty
        Dim fileName As String = String.Empty
        Dim recNo As String = String.Empty
        Dim dr As DataRow()

        For i As Integer = 0 To max

            '更新情報の設定
            setDt.Clear()
            setDt.ImportRow(dt.Rows(i))

            crtDate = setDt.Rows(0).Item("CRT_DATE").ToString()
            fileName = setDt.Rows(0).Item("FILE_NAME").ToString()
            recNo = setDt.Rows(0).Item("REC_NO").ToString()

            dr = ds.Tables("LMI410IN_IDO_HOKOKU").Select(String.Concat(" CRT_DATE = '", crtDate, _
                                                                       "' AND FILE_NAME = '", fileName, _
                                                                       "' AND REC_NO = '", recNo, "'"))
            If dr.Length > 0 Then
                setDt.Rows(0).Item("HAITA_SYS_UPD_DATE") = dr(0).Item("HAITA_SYS_UPD_DATE").ToString()
                setDt.Rows(0).Item("HAITA_SYS_UPD_TIME") = dr(0).Item("HAITA_SYS_UPD_TIME").ToString()
            End If

            '更新処理
            Me.UpdateIdoDataAction(setDs)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 移動報告作成時、BYK移動EDI受信テーブル更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateIdoDataAction(ByVal ds As DataSet) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "H_SENDINOUTEDI_BYK"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                '入荷報告データの更新
                ds = MyBase.CallBLC(Me._Blc, "InsertSendInOutBykData", ds)

                'BYK移動EDI受信データ(明細)の更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateIdoEdiDtlBykData", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist() = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function

#End Region

#Region "一括変更処理(BYK移動EDI受信テーブル更新)"

    ''' <summary>
    ''' 一括変更時、BYK移動EDI受信テーブル更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Ikkatuhenko(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMI410IN_IKKATU_HENKO"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                'BYK移動EDI受信データ(明細)の更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateIkkatuIdoData", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist() = True Then
                    Return ds
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class