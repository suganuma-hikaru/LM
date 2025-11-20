' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140  : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI140BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI140BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI140DAC = New LMI140DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            '0件でも表示するので、メッセージの設定はしない
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMI140_H_WHEDI_BAH").Rows(0).Item("ROW_NO").ToString()
        Dim nrsProcNo As String = ds.Tables("LMI140_H_WHEDI_BAH").Rows(0).Item("NRS_PROC_NO").ToString()

        '物産アニマルヘルス_倉庫内処理依頼EDIデータの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdi", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore("00", "E011", , rowNo, "NRS処理番号", nrsProcNo)
            Return ds
        End If

        '物産アニマルヘルス_倉庫内処理実績EDIデータの登録
        ds = MyBase.CallDAC(Me._Dac, "InsertSend", ds)

        Return ds

    End Function

#End Region 'Method

End Class
