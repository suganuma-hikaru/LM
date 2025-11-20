' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME020  : 作業料明細編集
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LME020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LME020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LME020DAC = New LME020DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '検索処理
        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        Return ds

    End Function

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        If String.IsNullOrEmpty(ds.Tables("LME020INOUT").Rows(0).Item("SAGYO_REC_NO").ToString) = True Then
            '作業レコード番号採番
            Dim sagyoRecNo As String = Me.GetSagyoRecNo(ds)
            ds.Tables("LME020INOUT").Rows(0).Item("SAGYO_REC_NO") = sagyoRecNo
        End If

        If String.IsNullOrEmpty(ds.Tables("LME020INOUT").Rows(0).Item("SAGYO_SIJI_NO").ToString) = True Then
            '作業指示書番号採番
            Dim sagyoSijiNo As String = Me.GetSagyoSijiNo(ds)
            ds.Tables("LME020INOUT").Rows(0).Item("SAGYO_SIJI_NO") = sagyoSijiNo
        End If

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        ds.Tables("LME020INOUT").Rows(0).Item("SYS_ENT_DATE") = systemDate
        ds.Tables("LME020INOUT").Rows(0).Item("SYS_ENT_TIME") = systemTime
        ds.Tables("LME020INOUT").Rows(0).Item("SYS_UPD_DATE") = systemDate
        ds.Tables("LME020INOUT").Rows(0).Item("SYS_UPD_TIME") = systemTime

        '新規登録
        Me.ServerChkJudge(ds, "InsertSaveAction")

        Return ds

    End Function

    ''' <summary>
    ''' SAGYO_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LME020INOUT").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' SAGYO_SIJI_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoSijiNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_SIJI_NO, Me, ds.Tables("LME020INOUT").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

#End Region

#Region "更新登録"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()
        ds.Tables("LME020INOUT").Rows(0).Item("SYS_UPD_DATE") = systemDate
        ds.Tables("LME020INOUT").Rows(0).Item("SYS_UPD_TIME") = systemTime

        '更新登録
        Me.ServerChkJudge(ds, "UpdateSaveAction")

        Return ds

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "DeleteSaveAction", ds)
        Return ds

    End Function

#End Region

#Region "請求日チェック(作業料)"
    ''' <summary>
    ''' 請求日チェック(作業料)確定解除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "ChkSeiqDateSagyo", ds)

        Dim chkDate As String = ds.Tables("LME020INOUT").Rows(0).Item("SAGYO_COMP_DATE").ToString().Replace("/", "")
        Dim seiqDate As String = SelectGheaderDate(ds)

        '作業の請求日チェック
        If Me.ChkDate(chkDate, seiqDate) = False Then

            '要望番号:1045 terakawa 2013.03.28 Start
            '新黒存在チェック用データセット作成
            Dim dr As DataRow = ds.Tables("G_HED_CHK").NewRow
            dr.Item("NRS_BR_CD") = ds.Tables("LME020INOUT").Rows(0).Item("NRS_BR_CD")
            dr.Item("SEIQTO_CD") = ds.Tables("LME020INOUT").Rows(0).Item("SEIQTO_CD")
            dr.Item("SKYU_DATE") = chkDate

            ds.Tables("G_HED_CHK").Rows.Add(dr)

            '新黒存在チェック
            ds = MyBase.CallDAC(Me._Dac, "NewKuroExistChk", ds)
            If MyBase.GetResultCount() >= 1 Then

                '請求期間内チェック
                ds = MyBase.CallDAC(Me._Dac, "InSkyuDateChk", ds)
                If MyBase.GetResultCount() >= 1 Then

                    Return ds
                End If

            End If
            '要望番号:1045 terakawa 2013.03.28 End

            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E285", New String() {"作業料"})
            MyBase.SetMessage("E805")
            '2016.01.06 UMANO 英語化対応END
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderDate(ByVal ds As DataSet) As String

        Dim dt As DataTable = ds.Tables("G_HED")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            Return False

        End If

        Return True

    End Function

#End Region

#Region "共通"

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
