' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC030    : 送状番号入力
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMC030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC030DAC = New LMC030DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GUIDANCE_KBN As String = "00"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC030IN")
        Dim outDt As DataTable = ds.Tables("LMC030OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC030IN")
        Dim setDt As DataTable = setDs.Tables("LMC030OUT")
        Dim errDr As DataRow = Nothing


        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            setDs = MyBase.CallDAC(Me._Dac, "SelectListData", setDs)

            'エラー判定
            If MyBase.GetResultCount() = 0 Then
                '0件の場合
                MyBase.SetMessageStore(LMC030BLC.GUIDANCE_KBN, "E079", New String() {"出荷データ（大）", dt.Rows(i).Item("OUTKA_NO_L").ToString()}, dt.Rows(i).Item("ROW_NO").ToString())

                errDr = outDt.NewRow()

                errDr("UNSOCO_NM") = String.Empty
                errDr("DEST_NM") = String.Empty

                'データセットに設定
                outDt.Rows.Add(errDr)

                Continue For
            End If

            '値設定
            outDt.ImportRow(setDt.Rows(0))

        Next

        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 更新前の存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUpdData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC030IN").Rows(0)

        'データの抽出
        ds = MyBase.CallDAC(Me._Dac, "SelectUpdData", ds)

        'エラー判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合
            MyBase.SetMessageStore(LMC030BLC.GUIDANCE_KBN, "E079", New String() {"出荷データ（大）", dr.Item("OUTKA_NO_L").ToString()}, dr.Item("ROW_NO").ToString())
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（大）更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaL(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateDataOutkaL", ds)
        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送データ（大）更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoL(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateDataUnsoL", ds)

    End Function

#End Region '保存処理

#End Region

End Class