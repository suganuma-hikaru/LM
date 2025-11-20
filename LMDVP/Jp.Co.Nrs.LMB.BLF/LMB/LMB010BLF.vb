' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB010BLC : 入荷データ検索
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB010BLC = New LMB010BLC()

#End Region

#Region "Const"

    '印刷種別
    Private Const PRINT_HOUKOKUSHO As String = "01"
    Private Const PRINT_CHECKLIST As String = "02"
    Private Const PRINT_UKETSUKEHYOU As String = "03"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索処理
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

    'START YANAI メモ②No.28
    ''' <summary>
    ''' EDIチェック対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListEdiData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListEdiData", ds)

    End Function
    'END YANAI メモ②No.28

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 初期出荷マスタ設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMB010IN_UPDATE"

        '******* 新規登録データ / 更新データを作成 ********

        '新規登録データ/更新データ 格納用
        Dim insDs As DataSet = New LMB010DS()
        Dim insDt As DataTable = insDs.Tables(tableNm)
        '新規登録用データ抽出
        Dim updDt As DataTable = ds.Tables(tableNm)
        Dim insdr As DataRow() = updDt.Select("UPD_FLG = '0'")
        Dim max As Integer = insdr.Length - 1
        For i As Integer = 0 To max
            '新規登録用データ設定
            insDt.ImportRow(insdr(i))
            '更新用データから新規登録対象データを削除
            updDt.Rows.Remove(insdr(i))
        Next

        '******* 新規登録 / 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If insDt.Rows.Count <> 0 Then
                '新規登録処理を行う
                Call Me.InsertData(insDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return insDs
                End If
            End If

            If updDt.Rows.Count <> 0 Then
                '更新処理を行う
                Call Me.UpdateData(ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub InsertData(ByVal ds As DataSet)

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistShokiShukkaM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertShokiShukkaM", ds)
        End If

    End Sub

    ''' <summary>
    ''' 初期出荷マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShokiShukkaM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateShokiShukkaM", ds)
        End If

    End Sub

    'START YANAI メモ②No.28
    ''' <summary>
    ''' EDI入荷データ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertDataEDI(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_EDI").Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMB010IN_EDI")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMB010IN_EDI").Rows(i))

                'BLCアクセス
                setDs = Me.BlcAccess(setDs, "InsertDataEDI")

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

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 作業一括作成処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertSagyo(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_SAGYO").Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMB010IN_SAGYO")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMB010IN_SAGYO").Rows(i))

                'BLCアクセス
                setDs = Me.BlcAccess(setDs, "InsertSagyo")

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

    ''' <summary>
    ''' 作業一括削除処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function DeleteSagyo(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_SAGYO").Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'START YANAI 要望番号1112 入荷作業削除が動作しない
                MyBase.SetMessage(Nothing)
                'END YANAI 要望番号1112 入荷作業削除が動作しない

                '値のクリア
                inTbl = setDs.Tables("LMB010IN_SAGYO")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMB010IN_SAGYO").Rows(i))

                'BLCアクセス
                setDs = Me.BlcAccess(setDs, "DeleteSagyo")

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
    'END YANAI 20120121 作業一括処理対応

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function MakeOutkaData(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_OUTKA").Rows.Count - 1
        Dim max2 As Integer = 0

        '■出荷データ作成用データ取得
        For i As Integer = 0 To max
            '値のクリア
            inTbl = setDs.Tables("LMB010IN_OUTKA")
            inTbl.Clear()

            'メッセージクリア
            MyBase.SetMessage(Nothing)

            '条件の設定
            inTbl.ImportRow(ds.Tables("LMB010IN_OUTKA").Rows(i))

            'BLCアクセス
            setDs = Me.BlcAccess(setDs, "SelectMakeOutkaData")

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                '取得したデータをsetDsからdsに移動
                max2 = setDs.Tables("LMB010OUT_OUTKA").Rows.Count - 1
                For j As Integer = 0 To max2
                    ds.Tables("LMB010OUT_OUTKA").ImportRow(setDs.Tables("LMB010OUT_OUTKA").Rows(j))
                Next
            End If

        Next

        'UTI追加修正 yamanaka 2012.12.21 Start
        If ds.Tables("LMB010IN_OUTKA").Rows(0).Item("JIKKO_FLG").ToString().Equals("1") = True _
           AndAlso MyBase.IsMessageStoreExist() = True Then
            Return ds
        End If
        'UTI追加修正 yamanaka 2012.12.21 End

        If ds.Tables("LMB010OUT_OUTKA").Rows.Count = 0 Then
            '1件も作成対象データがない場合は終了
            Return ds
        End If

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        '■出荷データ作成
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, "MakeOutkaData")

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'UTI追加修正 yamanaka 2012.12.21 Start
                If ds.Tables("LMB010IN_OUTKA").Rows(0).Item("JIKKO_FLG").ToString().Equals("1") = True Then
                    For i As Integer = 0 To ds.Tables("LMB010IN_OUTKA").Rows.Count - 1
                        '値のクリア
                        inTbl.Clear()

                        '条件の設定
                        inTbl.ImportRow(ds.Tables("LMB010IN_OUTKA").Rows(i))

                        '入荷(大)データ更新処理
                        setDs = Me.BlcAccess(setDs, "UpMakeOutkaData")
                    Next

                    'エラーがあるかを判定
                    If MyBase.IsMessageStoreExist() = True Then
                        Return ds
                    End If

                End If
                'UTI追加修正 yamanaka 2012.12.21 End

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'START 要望番号1784 s.kobayashi 
    ''' <summary>
    ''' 入荷報告取消処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InkaHokokuCancel(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_INKA_L").Rows.Count - 1
        Dim max2 As Integer = 0
        Dim sucDs As DataSet = ds.Copy
        sucDs.Clear()

        '■出荷データ作成用データ取得
        For i As Integer = 0 To max
            '値のクリア
            inTbl = setDs.Tables("LMB010IN_INKA_L")
            inTbl.Clear()

            'メッセージクリア
            MyBase.SetMessage(Nothing)

            '条件の設定
            inTbl.ImportRow(ds.Tables("LMB010IN_INKA_L").Rows(i))

            'BLCアクセス
            setDs = Me.BlcAccess(setDs, "IsCreatedOutkaData")

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                '取得したデータをsetDsからdsに移動
                max2 = setDs.Tables("LMB010IN_INKA_L").Rows.Count - 1
                For j As Integer = 0 To max2
                    sucDs.Tables("LMB010IN_INKA_L").ImportRow(setDs.Tables("LMB010IN_INKA_L").Rows(j))
                Next
            End If

        Next

        If sucDs.Tables("LMB010IN_INKA_L").Rows.Count = 0 Then
            '1件も作成対象データがない場合は終了
            Return sucDs
        End If

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        '■入荷報告取り消し処理
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            sucDs = Me.BlcAccess(sucDs, "UpdateInkaHokokuCancel")

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function
    'End 要望番号1784 s.kobayashi 

    'WIT対応 入荷データ一括取込対応 kasama Start
    ''' <summary>
    ''' 作業一括作成処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InkaIkkatuTorikomi(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_INKA_L").Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMB010IN_INKA_L")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMB010IN_INKA_L").Rows(i))

                'BLCアクセス
                setDs = Me.BlcAccess(setDs, "InkaIkkatuTorikomi")

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
    'WIT対応 入荷データ一括取込対応 kasama End

    'CALT連携対応 入荷予定データ作成対応 Ri Start
    ''' <summary>
    ''' 入荷予定データ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InkaYoteiiSakusei(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMB010IN_INKA_PLAN_SEND").Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージのクリア
                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMB010IN_INKA_PLAN_SEND")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMB010IN_INKA_PLAN_SEND").Rows(i))

                '入荷データの取得処理を行う
                'BLCアクセス
                setDs = Me.BlcAccess(setDs, "InkaYoteiInsert")

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = True Then
                    'resultのFalse戻し
                    rtnResult = False

                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function
    'CALT連携対応 入荷予定データ作成対応 Ri End

    ''' <summary>
    ''' RFIDラベルデータ取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectRfidLavelData(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Clone()

        For i As Integer = 0 To ds.Tables("LMB010IN_PRINT_RFID").Rows.Count() - 1

            ' メッセージクリア
            MyBase.SetMessage(Nothing)

            ' 条件の設定
            setDs.Tables("LMB010IN_PRINT_RFID").Clear()
            setDs.Tables("LMB010IN_PRINT_RFID").ImportRow(ds.Tables("LMB010IN_PRINT_RFID").Rows(i))

            ' BLCアクセス
            setDs = Me.BlcAccess(setDs, "SelectRfidLavelData")
            If MyBase.IsMessageExist() Then
                Return ds
            End If

            If setDs.Tables("LMB010OUT_PRINT_RFID").Rows.Count() > 0 Then
                ds.Tables("LMB010OUT_PRINT_RFID").Merge(setDs.Tables("LMB010OUT_PRINT_RFID"))
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function
    'EMD YANAI メモ②No.28

#End Region

#Region "印刷"

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMB010IN_INKA_L"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim InDs As DataSet = ds.Copy
        Dim InDt As DataTable = InDs.Tables(tableNm)
        InDt.Clear()

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim printType As String = dt.Rows(0).Item("PRINT_KB").ToString()

        'プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing = True Then

            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        Dim setRptDt As DataTable = Nothing
        Dim cnt As Integer = 0

        '2012/12/06処理追加開始
#If False Then  'UPD 2021/12/15
        If printType.Equals("08") = False AndAlso (printType.Equals("04") Or printType.Equals("05") = False) Then '2012/12/10入荷確定入力モニター表"05"追加
#Else
        If (printType.Equals("08") = False AndAlso printType.Equals("09") = False AndAlso printType.Equals("10") = False) AndAlso (printType.Equals("04") Or printType.Equals("05") = False) Then '2012/12/10入荷確定入力モニター表"05"追加

#End If


            For i As Integer = 0 To max

                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))

                '更新処理(エラーの場合、次レコードへ)
                If Me.UpdatePrintDataAction(setDs) = False Then
                    Continue For
                End If

                '印刷処理
                setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

                'プレビュー情報を設定
                If setRptDt Is Nothing = False Then
                    cnt = setRptDt.Rows.Count - 1
                    For j As Integer = 0 To cnt
                        rtnDt.ImportRow(setRptDt.Rows(j))
                    Next
                End If
            Next
            '以下追加分
#If False Then  'UPD 2021/12/15
         ElseIf printType.Equals("08") Then
#Else
        ElseIf printType.Equals("08") OrElse printType.Equals("09") OrElse printType.Equals("10") Then
#End If

            For i As Integer = 0 To max

                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))
                InDt.ImportRow(dt.Rows(i))
            Next

            '入荷報告チェックリスト用処理
            setRptDt = Me.DoPrint(InDs, printType).Tables(LMConst.RD)

            'プレビュー情報を設定
            If setRptDt Is Nothing = False Then
                cnt = setRptDt.Rows.Count - 1
                For j As Integer = 0 To cnt
                    rtnDt.ImportRow(setRptDt.Rows(j))
                Next
            End If
        Else

            For i As Integer = 0 To max

                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))

                '更新処理(エラーの場合、次レコードへ)
                If Me.UpdatePrintDataAction(setDs) = False Then
                    Continue For
                End If

                InDt.ImportRow(dt.Rows(i))
            Next

            '入荷報告チェックリスト用処理
            setRptDt = Me.DoPrint(InDs, printType).Tables(LMConst.RD)

            'プレビュー情報を設定
            If setRptDt Is Nothing = False Then
                cnt = setRptDt.Rows.Count - 1
                For j As Integer = 0 To cnt
                    rtnDt.ImportRow(setRptDt.Rows(j))
                Next
            End If

        End If
        '2012/12/06処理追加終了
        Return ds

    End Function

    'START アクサルタ　GHSラベル対応
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintGHSAction(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMB010IN_INKA_L"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        'Dim setDs As DataSet() = Nothing

        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim InDs As DataSet = ds.Copy
        Dim InDt As DataTable = InDs.Tables(tableNm)
        InDt.Clear()

        Dim dt800 As DataTable = ds.Tables(tableNm)

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim printType As String = dt.Rows(0).Item("PRINT_KB").ToString()

        Dim setRptDt As DataTable = Nothing
        Dim cnt As Integer = 0

        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        prtBlc = New Com.Base.BaseBLC() {New LMB800BLC()}
        setDs = Me.SetDataSetLMB800InData(ds)

        Dim rtnDs As DataSet = Nothing

        '検索処理
        rtnDs = MyBase.CallBLC(New LMB800BLC(), "DoPrintGHS", setDs)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printType">印刷種別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing
        Dim tableNm As String = "LMB010IN_INKA_L"

        Select Case printType

            Case "01" '入荷報告

                prtBlc = New Com.Base.BaseBLC() {New LMB520BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB520InData(ds)}


            Case "02" 'チェックリスト

                prtBlc = New Com.Base.BaseBLC() {New LMB510BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB510InData(ds)}

            Case "03" '入荷受付表

                If ds.Tables(tableNm).Rows(0).Item("SET_NAIYO").ToString().Equals("1") Then
                    prtBlc = New Com.Base.BaseBLC() {New LMB501BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMB501InData(ds)}

                Else
                    prtBlc = New Com.Base.BaseBLC() {New LMB500BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMB500InData(ds)}

                End If


            Case "04" '入荷報告チェックリスト

                prtBlc = New Com.Base.BaseBLC() {New LMB530BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB530InData(ds)}

            Case "05" '入荷確定入力モニター表

                prtBlc = New Com.Base.BaseBLC() {New LMB540BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB540InData(ds)}

                'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
            Case "07" '入荷報告(角印)

                prtBlc = New Com.Base.BaseBLC() {New LMB520BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB520InDataKakuin(ds)}

            Case "08" '入庫連絡票

                prtBlc = New Com.Base.BaseBLC() {New LMB550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB550InData(ds)}

            Case "09" '運送保険申込書　　LMB560

                prtBlc = New Com.Base.BaseBLC() {New LMB560BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB560InData(ds)}

            Case "10" 'コンテナ番号ラベル    LMB570

                prtBlc = New Com.Base.BaseBLC() {New LMB570BLC()}
                setDs = New DataSet() {Me.SetDataSetLMB570InData(ds)}
        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If
        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' LMB500DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB500InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB500InData(ds, New LMB500DS(), "LMB500IN")

    End Function

    ''' <summary>
    ''' LMB510DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB510InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB510InData(ds, New LMB510DS(), "LMB510IN")

    End Function

    ''' <summary>
    ''' LMB520DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB520InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB520InData(ds, New LMB520DS(), "LMB520IN", "0")

    End Function

#If True Then   'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
    ''' <summary>
    ''' LMB520DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB520InDataKakuin(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB520InData(ds, New LMB520DS(), "LMB520IN","1")

    End Function
#End If

    ''' <summary>
    ''' LMB530DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB530InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB530InData(ds, New LMB530DS(), "LMB530IN")

    End Function

    ''' <summary>
    ''' LMB540DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB540InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB540InData(ds, New LMB540DS(), "LMB540IN")

    End Function

    ''' <summary>
    ''' LMB540DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB550InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB550InData(ds, New LMB550DS(), "LMB550IN")

    End Function

    ''' <summary>
    ''' LMB560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB560InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB560InData(ds, New LMB560DS(), "LMB560IN")

    End Function

    ''' <summary>
    ''' LMB570DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB570InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB570InData(ds, New LMB570DS(), "LMB570IN")

    End Function

    ''' <summary>
    ''' LMB501DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB501InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB501InData(ds, New LMB501DS(), "LMB501IN")

    End Function

     ''' <summary>
     ''' LMB800DSを生成
     ''' </summary>
     ''' <param name="ds">DataSet</param>
     ''' <returns></returns>
     ''' <remarks></remarks>
    Private Function SetDataSetLMB800InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMB800InData(ds, New LMB800DS(), "LMB800IN")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB500InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMB010IN_INKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("INKA_NO_L") = setDr.Item("INKA_NO_L").ToString()
        dr.Item("USER_CD") = setDr.Item("TANTO_USER").ToString()

        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB510InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMB010IN_INKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("INKA_NO_L") = setDr.Item("INKA_NO_L").ToString()
        dr.Item("USER_CD") = setDr.Item("TANTO_USER").ToString()

        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB520InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String, Optional ByVal sKakuinFLG As String = "0") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMB010IN_INKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("INKA_NO_L") = setDr.Item("INKA_NO_L").ToString()
        dr.Item("PGID") = MyBase.GetPGID
        dr.Item("KAKUIN_FLG") = sKakuinFLG.ToString     'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能

        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB530InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()
            dr.Item("PGID") = MyBase.GetPGID

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB540InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()
            dr.Item("PGID") = MyBase.GetPGID

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB550InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()
            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function


    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB560InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()

            '運送保険指定時
            dr.Item("UNSO_TEHAI_CHK") = LMConst.FLG.ON

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB570InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB501InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()
            dr.Item("USER_CD") = MyBase.GetUserID

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB800InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMB010IN_INKA_L").Select()

        For i As Integer = 0 To setDr.Length - 1

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = setDr(i).Item("INKA_NO_L").ToString()
            dr.Item("LABEL_TYPE") = setDr(i).Item("LABEL_TYPE").ToString()

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintDataAction(ByVal ds As DataSet) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "LMB010IN_INKA_L"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateInkaLPrint", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist(Convert.ToInt32(dr.Item("ROW_NO").ToString())) = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

#End Region

#End Region

End Class
