' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD020BLC : 在庫移動
'  作  成  者       :  [高道]
' ==========================================================================
Imports System.IO
Imports System.Text
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMD020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD020BLC = New LMD020BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成（自動倉庫置場変更一覧）
    ''' </summary>
    Private _Print660Blc As LMD660BLC = New LMD660BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成（強制出庫在庫一覧）
    ''' </summary>
    Private _Print670Blc As LMD670BLC = New LMD670BLC()

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

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "getTouSituExp")

    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region

#Region "保存処理"

    ''' <summary>
    ''' データ検索処理(振替データ作成用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "InsertSaveAction")

    End Function


#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理（自動倉庫置場変更一覧）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>要望管理009859</returns>
    Private Function DoLMD660Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)
        ds = MyBase.CallBLC(_Print660Blc, "DoPrint", ds)

        rdPrevDt.Merge(ds.Tables(LMConst.RD))
        ds.Tables(LMConst.RD).Clear()
        ds.Tables(LMConst.RD).Merge(rdPrevDt)

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理（強制出庫在庫一覧）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>要望管理017415</returns>
    Private Function DoLMD670Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)
        ds = MyBase.CallBLC(_Print670Blc, "DoPrint", ds)

        rdPrevDt.Merge(ds.Tables(LMConst.RD))
        ds.Tables(LMConst.RD).Clear()
        ds.Tables(LMConst.RD).Merge(rdPrevDt)

        Return ds

    End Function

#End Region

    '要望番号:1350 terakawa 2012.08.29 Start
#Region "入力チェック"
    ''' <summary>
    ''' 同一置き場（商品・ロット）のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "ChkGoodsLot")

    End Function
#End Region
    '要望番号:1350 terakawa 2012.08.29 End

#Region "強制出庫"

    ''' <summary>
    ''' 出庫依頼
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' WITシステムのWTD120BLFを流用している
    ''' </remarks>
    Private Function ShukoIrai(ByVal ds As DataSet) As DataSet

        ' 番号取得（8桁のみ採用）
        Dim outkoReqNo As String = (New NumberMasterUtility).GetAutoCode(NumberMasterUtility.NumberKbn.AUTO_SOKO_OUTKA_NO, Me)
        If outkoReqNo.Length > 8 Then
            outkoReqNo = Right(outkoReqNo, 8)
        End If

        'ファイル情報/4号機指定ステーション取得
        Dim sendInfoTbl As DataTable = Nothing
        Dim sendInfoRow As DataRow = Nothing
        Using wkData As DataSet = ds.Clone
            wkData.Tables("LMD020IN_REG").Merge(ds.Tables("LMD020IN_REG"))
            Dim wkDs As DataSet = ds.Clone
            wkDs = Me.BlcAccess(wkData, "SelectSendInfo")
            sendInfoTbl = wkDs.Tables("LMD020_SEND_INFO")
            If sendInfoTbl.Rows.Count = 0 Then
                MyBase.SetMessage("E215", New String() {"出庫", "依頼先", "区分(O012)"})
                Return ds
            End If
            sendInfoRow = sendInfoTbl.Rows(0)
        End Using

        Dim resultCount As Integer = 0
        Using wkData As DataSet = ds.Clone
            Dim palletNoList As New Dictionary(Of String, Integer)
            Dim fileText As New System.Text.StringBuilder
            Dim staNo As String = String.Empty

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '画面から受け取ったデータ分ループ（棚卸し一覧表(LMD511)と同じ並びで出力）
                For Each row As DataRow In ds.Tables("LMD020IN_REG").Select("", "WH_CD, TOU_NO, SITU_NO, ZONE_CD, PALLET_NO, GOODS_NM, LOT_NO")
                    Try
                        'ステーションの決定
                        staNo = String.Empty
                        Select Case row.Item("SITU_NO").ToString()
                            Case "01"
                                staNo = "1310"
                            Case "02"
                                staNo = "1320"
                            Case "03"
                                staNo = "1330"
                            Case "04"
                                Dim wkRows As DataRow() = sendInfoTbl.Select(String.Concat("CUST_CD_L = '", row.Item("CUST_CD_L").ToString(), "'"))
                                If wkRows.Length = 0 Then
                                    staNo = "1350"
                                Else
                                    staNo = wkRows(0).Item("STA_NO_04").ToString()
                                End If
                        End Select

                        '棟/室/パレットNo/ステーションを組み合わせたキーを作成
                        Dim palletKey As String = String.Concat(
                                row.Item("TOU_NO"),
                                "/",
                                row.Item("SITU_NO"),
                                "/",
                                row.Item("PALLET_NO"),
                                "/",
                                staNo)

                        'ファイル用行情報作成
                        If Not palletNoList.Keys.Contains(palletKey) Then
                            'リストに存在しない（キーが重複しない）場合のみ

                            'リストに追加
                            palletNoList.Add(palletKey, palletNoList.Count + 1)

                            'パレットNoの加工
                            Dim palletNo As String = row.Item("PALLET_NO").ToString()
                            If Not String.IsNullOrEmpty(palletNo) Then
                                palletNo = String.Concat("""", palletNo, """")
                            End If

                            '行情報作成
                            fileText.AppendFormat("{0},{1},{2},{3},{4},{5},{6}{7}",
                                                  "21",
                                                  String.Concat(GetSystemDate(), Left(GetSystemTime(), 6)),
                                                  String.Concat("""", outkoReqNo, """"),
                                                  palletNo,
                                                  "1",
                                                  staNo,
                                                  "2",
                                                  vbCrLf)
                        End If

                        '自動倉庫出庫予定テーブル登録
                        wkData.Tables("LMD020IN_REG").Clear()
                        row.Item("OUTKO_ORD_NO") = outkoReqNo
                        row.Item("OUTKO_ORD_NO_DTL") = String.Format("{0:D4}", palletNoList(palletKey))
                        row.Item("STA_NO") = staNo
                        wkData.Tables("LMD020IN_REG").ImportRow(row)

                        Me.BlcAccess(wkData, "InsertOutkoPlanAutoWh")

                        If (Me.GetResultCount() = 0) Then
                            resultCount = 0
                            Exit For
                        End If

                        resultCount += Me.GetResultCount()

                    Catch ex As Exception
                        Logger.WriteErrorLog(MyBase.GetType.Name _
                                           , Reflection.MethodBase.GetCurrentMethod().Name _
                                           , ex.Message _
                                           , ex)

                        resultCount = 0
                        Exit For
                    End Try
                Next

                If (ds.Tables("LMD020IN_REG").Rows.Count = resultCount) Then
                    'ファイル作成
                    If Me.CreateFile(ds, sendInfoRow, fileText.ToString()) Then
                        Me.CommitTransaction(scope)
                    End If

                Else
                    'エラー
                    Me.SetMessage("E547", New String() {"出庫依頼"})
                End If
            End Using
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' ファイル作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="sendInfoRows"></param>
    ''' <param name="requestText"></param>
    ''' <returns></returns>
    Function CreateFile(ByVal ds As DataSet, ByVal sendInfoRows As DataRow, ByVal requestText As String) As Boolean

        '出力フォルダパス
        Dim folderPath As String = sendInfoRows.Item("FOLDER_PATH").ToString()

        '出力ファイルパス
        Dim outputFilePath As String = Path.Combine(folderPath, sendInfoRows.Item("OUT_FILE_NAME").ToString())

        '一時ファイルパス
        Dim tempFilePath As String = Path.Combine(folderPath, sendInfoRows.Item("TEMP_FILE_NAME").ToString())

        Try
            '出力フォルダ作成
            Directory.CreateDirectory(folderPath)

            '一時ファイルがすでに存在していればエラー
            If File.Exists(tempFilePath) Then
                '別の出庫依頼を処理中です。時間をおいて、再度実施してください
                Me.SetMessage("E953")
                Return False
            End If

            '一時ファイル出力(同ファイルが存在する場合エラー)
            Using fs As FileStream = File.Open(tempFilePath _
                                             , FileMode.CreateNew _
                                             , FileAccess.Write)

                '出庫依頼一時ファイル作成
                fs.Write(Encoding.UTF8.GetBytes(requestText).ToArray() _
                       , 0 _
                       , Encoding.UTF8.GetBytes(requestText).Length)

                fs.Flush(True)

            End Using

            '日別連番の採番
            Dim fileSeq As Integer = -1
            Dim outputDate As String = String.Empty

            Using wkData As DataSet = ds.Clone
                wkData.Tables("LMD020IN_REG").Merge(ds.Tables("LMD020IN_REG"))

                Dim fileSeqRow As DataRow = Nothing
                Dim wkDs As DataSet = ds.Clone
                wkDs = Me.BlcAccess(wkData, "SelectFileSeq")
                If wkDs.Tables("LMD020OUT_FILESEQ").Rows.Count = 0 Then
                    outputDate = MyBase.GetSystemDate()
                    fileSeq = 1

                    wkData.Tables("LMD020IN_FILESEQ").Clear()
                    Dim row As DataRow = wkData.Tables("LMD020IN_FILESEQ").NewRow()
                    row.Item("NRS_BR_CD") = ds.Tables("LMD020IN_REG").Rows(0).Item("NRS_BR_CD")
                    row.Item("WH_CD") = ds.Tables("LMD020IN_REG").Rows(0).Item("WH_CD")
                    row.Item("OUTPUT_DATE") = outputDate
                    row.Item("LAST_SEQ") = fileSeq.ToString()
                    wkData.Tables("LMD020IN_FILESEQ").Rows.Add(row)

                    Me.BlcAccess(wkData, "InsertFileSeq")

                Else
                    fileSeqRow = wkDs.Tables("LMD020OUT_FILESEQ").Rows(0)

                    outputDate = fileSeqRow.Item("OUTPUT_DATE").ToString()
                    fileSeq = Convert.ToInt32(fileSeqRow.Item("LAST_SEQ").ToString()) + 1

                    wkData.Tables("LMD020IN_FILESEQ").Clear()
                    Dim row As DataRow = wkData.Tables("LMD020IN_FILESEQ").NewRow()
                    row.Item("NRS_BR_CD") = ds.Tables("LMD020IN_REG").Rows(0).Item("NRS_BR_CD")
                    row.Item("WH_CD") = ds.Tables("LMD020IN_REG").Rows(0).Item("WH_CD")
                    row.Item("OUTPUT_DATE") = outputDate
                    row.Item("LAST_SEQ") = fileSeq.ToString()
                    wkData.Tables("LMD020IN_FILESEQ").Rows.Add(row)

                    Me.BlcAccess(wkData, "UpdateFileSeq")
                End If
            End Using

            '出力ファイル名を加工
            outputFilePath = Replace(outputFilePath, "%1", outputDate)
            outputFilePath = Replace(outputFilePath, "%2", String.Format("{0:D4}", fileSeq))

            '一時ファイルを出力ファイル名（＝連携ファイル名）にリネーム
            File.Move(tempFilePath, outputFilePath)

        Catch ex As Exception
            Me.Logger.WriteErrorLog(Me.GetType.Name _
                                  , System.Reflection.MethodBase.GetCurrentMethod.Name _
                                  , ex.Message _
                                  , ex)

            If (ex.GetType() = GetType(System.IO.IOException) AndAlso File.Exists(tempFilePath)) Then
                '別の出庫依頼を処理中です。時間をおいて、再度実施してください
                Me.SetMessage("E953")

            Else
                Me.SetMessage("S001", New String() {"出庫依頼ファイルの作成"})
            End If

            Return False
        End Try

        Return True

    End Function

#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

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

#End Region

End Class
