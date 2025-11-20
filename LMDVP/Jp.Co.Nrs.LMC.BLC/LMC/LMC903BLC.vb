' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC903    : 佐川e飛伝Ⅲ CSV出力
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC903BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC903BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC903DAC = New LMC903DAC()


#End Region

#Region "Method"

#Region "CSVデータ取得処理"

    ''' <summary>
    ''' CSVデータ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのYamatoB2Csvメソッド呼出</remarks>
    Private Function SagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        ' 印刷対象データの取得
        Dim rtnDs As DataSet = Me.SelectSagawaEHiden3Csv(ds)

        ' 出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC903OUT").Rows.Count = 0 Then
            ' 処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        ' 取得値の加工を行う（SQLでできないか煩雑なもの）
        For i As Integer = 0 To rtnDs.Tables("LMC903OUT").Rows.Count - 1
            Dim dr As DataRow = rtnDs.Tables("LMC903OUT").Rows(i)

            Dim fieldLen As Integer

            ' お届け先郵便番号
            ' ハイフン編集
            Dim destZip As String = dr.Item("DEST_ZIP").ToString().Trim()
            If destZip.Length() >= 3 AndAlso
                destZip.IndexOf("-") < 0 Then
                dr.Item("DEST_ZIP") = String.Concat(destZip.Substring(0, 3), "-", destZip.Substring(3))
            Else
                dr.Item("DEST_ZIP") = destZip
            End If

            ' お届け先住所１～３
            ' 全角変換と必要に応じた分割
            fieldLen = 16
            Dim destAd1 As String = StrConv(dr.Item("DEST_AD_1").ToString().Trim(), VbStrConv.Wide)
            Dim destAd2 As String = StrConv(dr.Item("DEST_AD_2").ToString().Trim(), VbStrConv.Wide)
            Dim destAd3 As String = StrConv(dr.Item("DEST_AD_3").ToString().Trim(), VbStrConv.Wide)
            If destAd1.Length() <= fieldLen AndAlso
                destAd2.Length() <= fieldLen AndAlso
                destAd3.Length() <= fieldLen Then
                dr.Item("DEST_AD_1") = destAd1
                dr.Item("DEST_AD_2") = destAd2
                dr.Item("DEST_AD_3") = destAd3
            Else
                Dim destAd As String =
                    String.Concat(destAd1,
                        If(destAd2.Length() = 0, "", StrConv(" ", VbStrConv.Wide)), destAd2,
                        If(destAd3.Length() = 0, "", StrConv(" ", VbStrConv.Wide)), destAd3,
                        StrConv(New String(" "c, fieldLen * 3), VbStrConv.Wide))
                dr.Item("DEST_AD_1") = destAd.Substring(fieldLen * 0, fieldLen).Trim()
                dr.Item("DEST_AD_2") = destAd.Substring(fieldLen * 1, fieldLen).Trim()
                dr.Item("DEST_AD_3") = destAd.Substring(fieldLen * 2, fieldLen).Trim()
            End If

            ' お届け先名称１～２
            ' 全角変換と必要に応じた分割
            fieldLen = 16
            Dim destNm1 As String = StrConv(dr.Item("DEST_NM_1").ToString().Trim(), VbStrConv.Wide)
            Dim destNm2 As String = StrConv(dr.Item("DEST_NM_2").ToString().Trim(), VbStrConv.Wide)
            If destNm1.Length() <= fieldLen AndAlso
                destNm2.Length() <= fieldLen Then
                dr.Item("DEST_NM_1") = destNm1
                dr.Item("DEST_NM_2") = destNm2
            Else
                Dim destNm As String =
                    String.Concat(destNm1,
                        If(destNm2.Length() = 0, "", StrConv(" ", VbStrConv.Wide)), destNm2,
                        StrConv(New String(" "c, fieldLen * 2), VbStrConv.Wide))
                dr.Item("DEST_NM_1") = destNm.Substring(fieldLen * 0, fieldLen).Trim()
                dr.Item("DEST_NM_2") = destNm.Substring(fieldLen * 1, fieldLen).Trim()
            End If

            ' ご依頼主郵便番号
            ' ハイフン編集
            Dim goirainushiZip As String = dr.Item("GOIRAINUSHI_ZIP").ToString().Trim()
            If goirainushiZip.Length() >= 3 AndAlso
                goirainushiZip.IndexOf("-") < 0 Then
                dr.Item("GOIRAINUSHI_ZIP") = String.Concat(goirainushiZip.Substring(0, 3), "-", goirainushiZip.Substring(3))
            Else
                dr.Item("GOIRAINUSHI_ZIP") = goirainushiZip
            End If

            ' ご依頼主住所１～２
            ' 全角変換と必要に応じた分割
            fieldLen = 16
            Dim goirainushiAd1 As String = StrConv(dr.Item("GOIRAINUSHI_AD1").ToString().Trim(), VbStrConv.Wide)
            Dim goirainushiAd2 As String = StrConv(dr.Item("GOIRAINUSHI_AD2").ToString().Trim(), VbStrConv.Wide)
            If goirainushiAd1.Length() <= fieldLen AndAlso
                goirainushiAd2.Length() <= fieldLen Then
                dr.Item("GOIRAINUSHI_AD1") = goirainushiAd1
                dr.Item("GOIRAINUSHI_AD2") = goirainushiAd2
            Else
                Dim goirainushiAd As String =
                    String.Concat(goirainushiAd1,
                        If(goirainushiAd2.Length() = 0, "", StrConv(" ", VbStrConv.Wide)), goirainushiAd2,
                        StrConv(New String(" "c, fieldLen * 3), VbStrConv.Wide))
                dr.Item("GOIRAINUSHI_AD1") = goirainushiAd.Substring(fieldLen * 0, fieldLen).Trim()
                dr.Item("GOIRAINUSHI_AD2") = goirainushiAd.Substring(fieldLen * 1, fieldLen).Trim()
            End If

            ' ご依頼主名称１～２
            ' 全角変換と必要に応じた分割
            fieldLen = 16
            Dim goirainushiNm1 As String = StrConv(dr.Item("GOIRAINUSHI_NM1").ToString().Trim(), VbStrConv.Wide)
            Dim goirainushiNm2 As String = StrConv(dr.Item("GOIRAINUSHI_NM2").ToString().Trim(), VbStrConv.Wide)
            If goirainushiNm1.Length() <= fieldLen AndAlso
                goirainushiNm2.Length() <= fieldLen Then
                dr.Item("GOIRAINUSHI_NM1") = goirainushiNm1
                dr.Item("GOIRAINUSHI_NM2") = goirainushiNm2
            Else
                Dim goirainushiNm As String =
                    String.Concat(goirainushiNm1,
                        If(goirainushiNm2.Length() = 0, "", StrConv(" ", VbStrConv.Wide)), goirainushiNm2,
                        StrConv(New String(" "c, fieldLen * 2), VbStrConv.Wide))
                dr.Item("GOIRAINUSHI_NM1") = goirainushiNm.Substring(fieldLen * 0, fieldLen).Trim()
                dr.Item("GOIRAINUSHI_NM2") = goirainushiNm.Substring(fieldLen * 1, fieldLen).Trim()
            End If
        Next

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectSagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC903IN")
        Dim outDt As DataTable = ds.Tables("LMC903OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC903IN")
        Dim setDt As DataTable = setDs.Tables("LMC903OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            ' 値のクリア
            setDs.Clear()

            ' 条件の設定
            inTbl.ImportRow(dt.Rows(i))

            ' データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectSagawaEHiden3Csv")

            count = MyBase.GetResultCount()

            ' 0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            ' 値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（大）更新（佐川e飛伝ⅢCSV作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateSagawaEHiden3Csv", ds)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

#End Region

#End Region

End Class
