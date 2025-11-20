' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI190BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI190BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI190DAC = New LMI190DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 荷主明細データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustDetailsData(ByVal ds As DataSet) As DataSet

        '荷主明細データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectCustDetailsData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除対象データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDeleteData(ByVal ds As DataSet) As DataSet

        '削除対象データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectDeleteData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaData(ByVal ds As DataSet) As DataSet

        '入荷データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectInkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal ds As DataSet) As DataSet

        '出荷データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaData", ds)

        Return ds

    End Function

    'タイムアウトのため修正 2013.02.28
    ''' <summary>
    ''' 取得した削除対象データを元に削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackData2(ByVal ds As DataSet) As DataSet

        '取得した削除対象データを元に削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteContTrackData2", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した入荷データを元に削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackInkaData2(ByVal ds As DataSet) As DataSet

        '取得した入荷データを元に削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteContTrackInkaData2", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した出荷データを元に削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackOutkaData2(ByVal ds As DataSet) As DataSet

        '取得した出荷データを元に削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteContTrackOutkaData2", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した入荷データを元に追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackInkaData2(ByVal ds As DataSet) As DataSet

        '取得した入荷データを元に追加処理
        ds = MyBase.CallDAC(Me._Dac, "InsertContTrackInkaData2", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した出荷データを元に追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackOutkaData2(ByVal ds As DataSet) As DataSet

        '取得した出荷データを元に追加処理
        ds = MyBase.CallDAC(Me._Dac, "InsertContTrackOutkaData2", ds)

        Return ds

    End Function
    'タイムアウトのため修正 2013.02.28

    ''' <summary>
    ''' 取得した削除対象データを元に削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackData(ByVal ds As DataSet) As DataSet

        '取得した削除対象データを元に削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteContTrackData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した入荷データを元に削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackInkaData(ByVal ds As DataSet) As DataSet

        '取得した入荷データを元に削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteContTrackInkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した出荷データを元に削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteContTrackOutkaData(ByVal ds As DataSet) As DataSet

        '取得した出荷データを元に削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteContTrackOutkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した入荷データを元に追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackInkaData(ByVal ds As DataSet) As DataSet

        '取得した入荷データを元に追加処理
        ds = MyBase.CallDAC(Me._Dac, "InsertContTrackInkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取得した出荷データを元に追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertContTrackOutkaData(ByVal ds As DataSet) As DataSet

        '取得した出荷データを元に追加処理
        ds = MyBase.CallDAC(Me._Dac, "InsertContTrackOutkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェル用データ管理取得ログ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertLogData(ByVal ds As DataSet) As DataSet

        Dim newDs As DataSet = New LMI190DS()
        Dim dr As DataRow = newDs.Tables("LMI190IN_LOG").NewRow()

        dr = newDs.Tables("LMI190IN_LOG").NewRow()

        dr("NRS_BR_CD") = ds.Tables("LMI190IN").Rows(0).Item("NRS_BR_CD").ToString
        dr("STATUS") = "01" '失敗したらここに来ないので、"01"固定
        dr("R_CNT") = Convert.ToString(ds.Tables("LMI190INOUT_INKA_GET").Rows.Count + ds.Tables("LMI190INOUT_OUTKA_GET").Rows.Count)

        'データセットに設定
        newDs.Tables("LMI190IN_LOG").Rows.Add(dr)

        'ハネウェル用データ管理取得ログ作成処理
        newDs = MyBase.CallDAC(Me._Dac, "InsertLogData", newDs)

        Return ds

    End Function

    ''' <summary>
    ''' シリンダタイプコンボの値を取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCylinder(ByVal ds As DataSet) As DataSet

        'シリンダタイプコンボの値を取得
        ds = MyBase.CallDAC(Me._Dac, "SelectCylinder", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫場所コンボの値を取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectToFromNm(ByVal ds As DataSet) As DataSet

        '在庫場所コンボの値を取得
        ds = MyBase.CallDAC(Me._Dac, "SelectToFromNm", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountZaiko(ByVal ds As DataSet) As DataSet

        '在庫検索データ件数取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuCountZaiko", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        Else
            '在庫検索データ取得
            ds = MyBase.CallDAC(Me._Dac, "SelectKensakuZaikoData", ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 在庫検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuZaikoData(ByVal ds As DataSet) As DataSet

        '在庫検索データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuZaikoData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 履歴検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountRireki(ByVal ds As DataSet) As DataSet

        '履歴検索データ件数取得()
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuCountRireki", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        Else
            '履歴検索データ取得
            ds = MyBase.CallDAC(Me._Dac, "SelectKensakuRirekiData", ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 履歴検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuRirekiData(ByVal ds As DataSet) As DataSet

        '履歴検索データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuRirekiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄済検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountHaiki(ByVal ds As DataSet) As DataSet

        '履歴検索データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuCountHaiki", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        Else
            '履歴検索データ取得
            ds = MyBase.CallDAC(Me._Dac, "SelectKensakuHaikiData", ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄済検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuHaikiData(ByVal ds As DataSet) As DataSet

        '履歴検索データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuHaikiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiData(ByVal ds As DataSet) As DataSet

        '廃棄済検索データ取得処理
        ds = MyBase.CallDAC(Me._Dac, "UpdateHaikiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiKaijoData(ByVal ds As DataSet) As DataSet

        '廃棄済検索データ取得処理
        ds = MyBase.CallDAC(Me._Dac, "UpdateHaikiKaijoData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonData(ByVal ds As DataSet) As DataSet

        '保存処理
        ds = MyBase.CallDAC(Me._Dac, "UpdateHozonData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' N40コード取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ImportN40Code(ByVal ds As DataSet) As DataSet

        ''ハネウェルN40変換マスタの更新(Merge文)
        ''取込データのほとんどがInsert/Update対象の場合は、こちらの方が高速
        ''ds = MyBase.CallDAC(Me._Dac, "MergeHoneyN40Chg", ds)


        'ハネウェルN40変換マスタの更新(Insert/Update文)
        Dim dsDac As DataSet = ds.Clone
        For Each drIn As DataRow In ds.Tables("LMI190IN_N40_CHG").Rows
            dsDac.Clear()
            dsDac.Tables("LMI190IN_N40_CHG").ImportRow(drIn)

            '既存チェック
            dsDac = MyBase.CallDAC(Me._Dac, "SelectHoneyN40Chg", dsDac)

            Dim dtOut As DataTable = dsDac.Tables("LMI190OUT_N40_CHG")
            If dtOut.Rows.Count > 0 Then
                '存在する場合
                If dtOut.Rows(0)("CYLINDER_NO").ToString <> drIn("CYLINDER_NO").ToString Then
                    '値が異なる場合⇒Update
                    dsDac = MyBase.CallDAC(Me._Dac, "UpdateHoneyN40Chg", dsDac)
                    If MyBase.IsMessageExist Then
                        Return ds
                    End If
                End If
            Else
                '存在しない場合⇒Insert
                dsDac = MyBase.CallDAC(Me._Dac, "InsertHoneyN40Chg", dsDac)
            End If
        Next

        Return ds

    End Function

#End Region

End Class
