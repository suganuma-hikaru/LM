' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMR010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMR010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMR010BLC = New LMR010BLC()

#End Region

#Region "Method"

#Region "検索処理"

#Region "入荷検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListINKAData(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectINKAData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        ' 特定の荷主固有のテーブルが存在するか否かの判定
        Dim dtTblExists As DataTable = ds.Tables("LMR010_TBL_EXISTS").Clone()
        Dim tblNames As String() = New String() {"H_INKAEDI_DTL_TSMC", "D_ZAI_TSMC"}
        For Each tblName As String In tblNames
            ds.Tables("LMR010_TBL_EXISTS").Clear()
            Dim drTblExists As DataRow = ds.Tables("LMR010_TBL_EXISTS").NewRow()
            drTblExists.Item("NRS_BR_CD") = ds.Tables("LMR010IN").Rows(0).Item("NRS_BR_CD")
            drTblExists.Item("TBL_NM") = tblName
            ds.Tables("LMR010_TBL_EXISTS").Rows.Add(drTblExists)
            ds = MyBase.CallBLC(Me._Blc, "GetTrnTblExits", ds)
            dtTblExists.ImportRow(ds.Tables("LMR010_TBL_EXISTS").Rows(0))
        Next
        ds.Tables("LMR010_TBL_EXISTS").Clear()
        ds.Merge(dtTblExists)

        '検索結果取得
        'START YANAI 要望番号653
        'Return MyBase.CallBLC(Me._Blc, "SelectListINKAData", ds)
        ds = MyBase.CallBLC(Me._Blc, "SelectListINKAData", ds)
        Return MyBase.CallBLC(Me._Blc, "SelectListINKAZAIData", ds)
        'END YANAI 要望番号653
        
    End Function

#End Region

#Region "出荷検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListOUTKAData(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectOUTKAData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        'START YANAI 要望番号653
        'Return MyBase.CallBLC(Me._Blc, "SelectListOUTKAData", ds)
        ds = MyBase.CallBLC(Me._Blc, "SelectListOUTKAData", ds)
        Return MyBase.CallBLC(Me._Blc, "SelectListOUTKAZAIData", ds)
        'END YANAI 要望番号653

    End Function

    ''' <summary>
    ''' 検索処理（出荷データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListOUTKADataKANRYO(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListOUTKADataKANRYO", ds)

    End Function

    ''' <summary>
    ''' 検索処理（在庫データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListZAIDataKANRYO(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListZAIDataKANRYO", ds)

    End Function

#End Region

#Region "入荷チェックデータ検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckINKAData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCheckINKAData", ds)

    End Function

#End Region

#Region "在庫チェックデータ検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckZAIData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCheckZAIData", ds)

    End Function

#End Region

#Region "入荷進捗区分チェックデータ検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckDataInka(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCheckDataInka", ds)

    End Function

#End Region

#Region "出荷進捗区分チェックデータ検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckDataOutka(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCheckDataOutka", ds)

    End Function

#End Region

#Region "荷主検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCustData", ds)

    End Function

#End Region

#Region "作業検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListSagyoSijiData(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectSagyoSijiData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectListSagyoSijiData", ds)
        Return ds

    End Function

#End Region

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 更新登録（入荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveInkaDataAction(ByVal ds As DataSet) As DataSet
        '入荷

        Return Me.ScopeStartEnd(ds, "UpdateSaveInkaDataAction")

    End Function

    ''' <summary>
    ''' 更新登録（出荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveOutkaDataAction(ByVal ds As DataSet) As DataSet
        '出荷

        Return Me.ScopeStartEnd(ds, "UpdateSaveOutkaDataAction")

    End Function

    ''' <summary>
    ''' 更新登録作業指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveSagyoSijiDataAction(ByVal ds As DataSet) As DataSet
        '出荷

        Return Me.ScopeStartEnd(ds, "UpdateSaveSagyoSijiDataAction")

    End Function

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

        Dim dt As DataTable = ds.Tables("LMR010INOUT")
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim saveCnt As Integer = 0

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LMR010INOUT")

        Dim outKanryoFlg As Boolean = False
        Dim zaidt As DataTable = Nothing
        Dim setzaidt As DataTable = Nothing
        Dim zaidr As DataRow = Nothing
        Dim outdt As DataTable = Nothing
        Dim outdr() As DataRow = Nothing
        Dim outMax As Integer = 0
        'START YANAI 要望番号653
        Dim outZaiDs As DataSet = ds.Copy()
        Dim max2 As Integer = outZaiDs.Tables("LMR010_ZAI").Rows.Count - 1
        'END YANAI 要望番号653
        If ("UpdateSaveOutkaDataAction").Equals(actionStr) = True Then
            '出荷の時のみTrue
            outKanryoFlg = True
            zaidt = ds.Tables("LMR010_ZAI_UPDDATA")
            setzaidt = setDs.Tables("LMR010_ZAI_UPDDATA")
            outdt = ds.Tables("LMR010_OUTKAS_UPDDATA")
        End If
        Dim dtJikaiBunnnouIn As DataTable = Nothing
        For i As Integer = 0 To max
            dr = dt.Rows(i)
            If ("60").Equals(Convert.ToString(dr.Item("INOUTKA_STATE_KB"))) = True Then
                ' 出荷完了の場合
                ' Rapidus次回分納情報取得用IN DataTable の 退避
                dtJikaiBunnnouIn = ds.Tables("LMR010_JIKAI_BUNNOU_IN").Copy()
                Exit For
            End If
        Next

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            If (LMConst.FLG.ON).Equals(dr.Item("SYORI_FLG")) = True Then
                '処理対象フラグがオンのデータのみ対象

                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    setDt.ImportRow(dt.Rows(i))

                    If outKanryoFlg = True AndAlso _
                        ("60").Equals(Convert.ToString(dr.Item("INOUTKA_STATE_KB"))) = True Then
                        '出荷完了の処理を行う時のみ、実予在庫個数等もsetDsに設定
                        outdr = outdt.Select(String.Concat("OUTKA_NO_L = '", dt.Rows(i).Item("INOUTKA_NO_L").ToString, "'"))
                        outMax = outdr.Length - 1
                        For j As Integer = 0 To outMax
                            zaidr = zaidt.Select(String.Concat("ZAI_REC_NO = '", outdr(j).Item("ZAI_REC_NO").ToString, "'"))(0)
                            setzaidt.ImportRow(zaidr)
                        Next
                    End If

                    'START YANAI 要望番号653
                    For j As Integer = 0 To max2
                        setDs.Tables("LMR010_ZAI").ImportRow(outZaiDs.Tables("LMR010_ZAI").Rows(j))
                    Next
                    'END YANAI 要望番号653

                    If ("60").Equals(Convert.ToString(dr.Item("INOUTKA_STATE_KB"))) = True Then
                        ' 出荷完了の場合
                        ' Rapidus次回分納情報取得用IN DataTable の DataSet への設定
                        dtJikaiBunnnouIn.Rows(0).Item("NRS_BR_CD") = dr.Item("NRS_BR_CD").ToString()
                        dtJikaiBunnnouIn.Rows(0).Item("OUTKA_NO_L") = dr.Item("INOUTKA_NO_L").ToString()
                        setDs.Tables("LMR010_JIKAI_BUNNOU_IN").Merge(dtJikaiBunnnouIn)
                    End If

                    'BLCアクセス
                    ds = Me.BlcAccess(setDs, actionStr)

                    'エラーがあるかを判定
                    If MyBase.IsMessageStoreExist(Convert.ToInt32(dt.Rows(i).Item("RECORD_NO"))) = False Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)

                        saveCnt = saveCnt + 1

                        'START YANAI 要望番号653
                        outZaiDs = ds.Copy()
                        'END YANAI 要望番号653

                    End If

                End Using

            End If

        Next

        Dim insRows As DataRow = ds.Tables("LMR010_SAVECNT").NewRow
        insRows.Item("SAVECNT") = saveCnt
        ds.Tables("LMR010_SAVECNT").Rows.Add(insRows)

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

#End Region

End Class
