' ' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI511BLC : JNC EDI
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI511BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI511DAC = New LMI511DAC()

#End Region

#Region "Method"

#Region "マスタデータ"

    ''' <summary>
    ''' ＪＮＣ営業所マスタ：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BoMstSelect(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "BoMstSelect", ds)
        Return rtnDs

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集中に旧データになっていないかチェック：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OldDataChkSelect(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "OldDataChkSelect", ds)
        Return rtnDs

    End Function

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理：取得：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectEdiL(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "OutkaSaveSelectEdiL", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectHed(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "OutkaSaveSelectHed", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectDtl(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "OutkaSaveSelectDtl", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：登録：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveInsertEdiL(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "OutkaSaveInsertEdiL", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：登録：ＥＤＩ出荷データＭ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveInsertEdiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "OutkaSaveInsertEdiM", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "OutkaSaveUpdateHed", ds)
        Return rtnDs

    End Function

#End Region

#Region "まとめ指示処理"

    ''' <summary>
    ''' まとめ指示処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSaveExitHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "MtmSaveExitHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' まとめ指示処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSaveUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "MtmSaveUpdateHed", ds)
        Return rtnDs

    End Function

#End Region

#Region "まとめ解除処理"

    ''' <summary>
    ''' まとめ解除処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmCancelExitHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "MtmCancelExitHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' まとめ解除処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmCancelUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "MtmCancelUpdateHed", ds)
        Return rtnDs

    End Function

#End Region

#Region "送信要求処理"

    ''' <summary>
    ''' 送信要求処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndReqExitHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SndReqExitHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 送信要求処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndReqUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SndReqUpdateHed", ds)
        Return rtnDs

    End Function

#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
    ''' <summary>
    ''' 送信要求処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー運送)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndReqUpdateHedUnso(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SndReqUpdateHedUnso", ds)
        Return rtnDs

    End Function
#End If

#End Region

#Region "送信取消処理"

    ''' <summary>
    ''' 送信取消処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndCancelExitHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SndCancelExitHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 送信取消処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndCancelUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SndCancelUpdateHed", ds)
        Return rtnDs

    End Function

#End Region

#Region "まとめ候補検索処理"

    ''' <summary>
    ''' まとめ候補検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSearchCount(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "MtmSearchCount", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' まとめ候補検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSearchSelect(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "MtmSearchSelect", ds)
        Return rtnDs

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchCount(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SearchCount", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchSelect(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SearchSelect", ds)
        Return rtnDs

    End Function

#End Region

#Region "保存処理(編集)"

    ''' <summary>
    ''' 保存処理(編集)：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditExitHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveEditExitHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(編集)：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveEditUpdateHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(編集)：排他：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditExitDtl(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveEditExitDtl", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(編集)：更新：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditUpdateDtl(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveEditUpdateDtl", ds)
        Return rtnDs

    End Function

#End Region

#Region "保存処理(訂正)"

    ''' <summary>
    ''' 保存処理(訂正)：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionExitHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveRevisionExitHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionUpdateHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveRevisionUpdateHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSelectHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveRevisionSelectHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：登録：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionInsertHed(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveRevisionInsertHed", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSelectDtl(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveRevisionSelectDtl", ds)
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：登録：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionInsertDtl(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SaveRevisionInsertDtl", ds)
        Return rtnDs

    End Function

#End Region

#End Region

End Class
