' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG040BLF : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMG040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMG040BLC = New LMG040BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc530 As LMG050BLC = New LMG050BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理
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

#Region "存在チェック"

    ''' <summary>
    ''' 請求鑑ヘッダ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "ExistData", ds)

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateDelete(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNm As String = "LMG040IN"
        Dim dt As DataTable = ds.Tables(tableNm)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNm)

        Dim rtnResult As Boolean = False
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージクリア
                MyBase.SetMessage(Nothing)

                '初期化
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '請求先マスタの存在チェック
                ds = MyBase.CallBLC(Me._Blc, "ChkSeiqtoMsub", setDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Continue For
                End If

                'データの更新(鑑ヘッダ)
                ds = MyBase.CallBLC(Me._Blc, "UpdateDeleteHed", setDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Continue For
                End If

                'データの更新(鑑明細)
                ds = MyBase.CallBLC(Me._Blc, "UpdateDeleteDtl", setDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Continue For
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End Using
        Next

        Return ds

    End Function

#End Region

#Region "確定処理"

    ''' <summary>
    ''' 確定処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateKakutei(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateKagamiHed", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "初期化処理"

    ''' <summary>
    ''' 初期化処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateClear(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNm As String = "LMG040IN"
        Dim dt As DataTable = ds.Tables(tableNm)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNm)

        Dim rtnResult As Boolean = False
        Dim max As Integer = dt.Rows.Count - 1

        Dim num As New NumberMasterUtility
        Dim skyuNo As String = String.Empty
        Dim nrsBrCd As String = String.Empty
        Dim inMax As Integer = 0

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージクリア
                MyBase.SetMessage(Nothing)

                '初期化
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '請求先マスタの存在チェック
                ds = MyBase.CallBLC(Me._Blc, "ChkSeiqtoMsub", setDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Continue For
                End If

                If ("01").Equals(inTbl.Rows(0).Item("STATE_KB").ToString) = True Then
                    '確定済の場合
                    'データの更新(鑑ヘッダ)
                    ds = MyBase.CallBLC(Me._Blc, "UpdateStateKbHed", setDs)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If
                ElseIf ("02").Equals(inTbl.Rows(0).Item("STATE_KB").ToString) = True Then
                    '印刷済の場合
                    '初期化対象のデータ取得(鑑ヘッダ)
                    ds = MyBase.CallBLC(Me._Blc, "SelectClearHed", setDs)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If

                    '初期化対象のデータ取得(鑑明細)
                    ds = MyBase.CallBLC(Me._Blc, "SelectClearDtl", ds)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If

                    'データの更新(鑑ヘッダ)
                    ds = MyBase.CallBLC(Me._Blc, "UpdateDeleteHed", ds)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If

                    'データの更新(鑑明細)
                    ds = MyBase.CallBLC(Me._Blc, "UpdateDeleteDtl", ds)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If

                    '採番
                    skyuNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.SKYU_NO, Me, inTbl.Rows(0).Item("NRS_BR_CD").ToString)

                    '採番した番号をデータセットに設定
                    inMax = ds.Tables("LMG040HED").Rows.Count - 1
                    For j As Integer = 0 To inMax
                        ds.Tables("LMG040HED").Rows(j).Item("SKYU_NO") = skyuNo
                        nrsBrCd = ds.Tables("LMG040HED").Rows(j).Item("NRS_BR_CD").ToString
                    Next
                    inMax = ds.Tables("LMG040DTL").Rows.Count - 1
                    For j As Integer = 0 To inMax
                        ds.Tables("LMG040DTL").Rows(j).Item("SKYU_NO") = skyuNo
                        ds.Tables("LMG040DTL").Rows(j).Item("NRS_BR_CD") = nrsBrCd
                    Next

                    'データの新規作成(鑑ヘッダ)
                    ds = MyBase.CallBLC(Me._Blc, "InsertHed", ds)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If

                    'データの新規作成(鑑明細)
                    ds = MyBase.CallBLC(Me._Blc, "InsertDtl", ds)
                    'メッセージの判定
                    If MyBase.IsMessageExist() = True Then
                        Continue For
                    End If

                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End Using
        Next

        Return ds

    End Function

#End Region

#Region "SAP処理"

    ''' <summary>
    ''' SAP出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SapOut(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMG040IN")
        Dim sapUpdCnt As Integer = 0
        ds.Tables("LMG040SAPUPDCNT").Clear()

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        For i As Integer = 0 To dt.Rows.Count - 1
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                Dim rowNo As Integer = i + 1
                Dim setDs As DataSet = SetSapUpdDataSet(dt.Rows(i), rowNo)

                ' SAP出力処理（LMG050BLC）
                Dim retDs As DataSet = MyBase.CallBLC(Me._Blc530, "SapOut", setDs)

                If Not MyBase.IsMessageStoreExist(rowNo) Then
                    ' エラーなしの場合、処理件数加算
                    sapUpdCnt += 1
                    ' トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If
            End Using
        Next

        Dim cntDr As DataRow = ds.Tables("LMG040SAPUPDCNT").NewRow()
        cntDr.Item("SAP_UPD_CNT") = sapUpdCnt.ToString()
        ds.Tables("LMG040SAPUPDCNT").Rows.Add(cntDr)

        Return ds

    End Function

    ''' <summary>
    ''' SAP取消処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SapCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMG040IN")
        Dim sapUpdCnt As Integer = 0
        ds.Tables("LMG040SAPUPDCNT").Clear()

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        'トランザクション開始
        For i As Integer = 0 To dt.Rows.Count - 1
            Using scope As TransactionScope = MyBase.BeginTransaction()

                Dim rowNo As Integer = i + 1
                Dim setDs As DataSet = SetSapUpdDataSet(dt.Rows(i), rowNo)

                ' SAP取消処理（LMG050BLC）
                Dim retDs As DataSet = MyBase.CallBLC(Me._Blc530, "SapCancel", setDs)

                ' エラーなしの場合、処理件数加算
                If Not MyBase.IsMessageStoreExist(rowNo) Then
                    sapUpdCnt += 1
                    ' トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If
            End Using
        Next

        Dim cntDr As DataRow = ds.Tables("LMG040SAPUPDCNT").NewRow()
        cntDr.Item("SAP_UPD_CNT") = sapUpdCnt.ToString()
        ds.Tables("LMG040SAPUPDCNT").Rows.Add(cntDr)

        Return ds

    End Function

    ''' <summary>
    ''' SAP処理共通 LMG050BLC メソッド引数用 DataSet 作成
    ''' </summary>
    ''' <param name="dr">編集元 DataRow</param>
    ''' <param name="rowNo">複数行一括更新時の行番号(1起算)</param>
    ''' <returns>作成した DataSet</returns>
    Private Function SetSapUpdDataSet(dr As DataRow, ByVal rowNo As Integer) As DataSet

        Dim setDs As DataSet = New LMG050DS()
        Dim setDt As DataTable
        Dim setDr As DataRow

        setDt = setDs.Tables("LMG050HED")
        setDr = setDt.NewRow()
        ' PKey
        setDr.Item("SKYU_NO") = dr.Item("SKYU_NO")
        ' 更新する進捗区分の値
        setDr.Item("STATE_KB") = dr.Item("STATE_KB")
        ' 営業所コード
        setDr.Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        ' 排他制御用
        setDr.Item("SYS_UPD_DATE") = dr.Item("SYS_UPD_DATE")
        setDr.Item("SYS_UPD_TIME") = dr.Item("SYS_UPD_TIME")

        setDt.Rows.Add(setDr)


        setDt = setDs.Tables("LMG050SAPUPDIN")
        setDr = setDt.NewRow()
        setDr.Item("ROW_NO") = rowNo.ToString()
        setDr.Item("LANG_FLG") = dr.Item("LANG_FLG")    'ADD 2023/04/10 依頼番号:036535

        setDt.Rows.Add(setDr)

        Return setDs

    End Function

#End Region

#Region "請求データ出力処理"

    ''' <summary>
    ''' 請求データ出力用データ抽出処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SkyuCsvSelect(ByVal ds As DataSet) As DataSet

        '抽出結果格納用テーブルをクリア
        ds.Tables("LMG040CSVOUT").Rows.Clear()

        '作業用にDatasetを複製
        Dim dsCopy As DataSet = ds.Copy()

        '処理対象データ（≒請求番号）をループ
        For Each inRow As DataRow In ds.Tables("LMG040IN").Rows

            '抽出キーを作業用Datasetにセット
            Dim dt As DataTable = dsCopy.Tables("LMG040IN")
            dt.Rows.Clear()
            Dim dr As DataRow = dt.NewRow()
            dr("NRS_BR_CD") = inRow("NRS_BR_CD")
            dr("SKYU_NO") = inRow("SKYU_NO")
            dt.Rows.Add(dr)

            'データ抽出
            dsCopy = MyBase.CallBLC(Me._Blc, "SkyuCsvSelect", dsCopy)

            'メッセージの判定
            If MyBase.IsMessageExist() Then
                Return ds
            End If

            '抽出結果を元Datasetに追記
            ds.Tables("LMG040CSVOUT").Merge(dsCopy.Tables("LMG040CSVOUT"))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 請求データ出力用フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SkyuCsvUpdate(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "SkyuCsvUpdate", ds)

            'コミット
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class