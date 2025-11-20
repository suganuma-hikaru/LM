' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME010    : 作業料明細書作成
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LME010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LME010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "作業レコード番号"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LME010DAC = New LME010DAC()

#End Region

#Region "Method"

#Region "作業データ更新処理"

    ''' <summary>
    ''' 作業データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyo(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LME010_SAGYO").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010_SAGYO").Rows(0).Item("SAGYO_REC_NO").ToString()

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateSagyo", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E011", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            Return ds
        End If

        Return ds

    End Function

#End Region

    'START YANAI 20120319　作業画面改造
#Region "作業データ完了処理"
    ''' <summary>
    ''' 作業データ完了
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateKanryo(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LME010_SAGYO").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010_SAGYO").Rows(0).Item("SAGYO_REC_NO").ToString()

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateKanryo", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E011", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            MyBase.SetMessage("E011") 'BLFでエラー判定用に設定しているだけ
            Return ds
        End If

        Return ds

    End Function

#End Region
    'END YANAI 20120319　作業画面改造

#Region "検索処理"

    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

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
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "更新処理（削除）"

    ''' <summary>
    ''' 作業データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyoDel(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LME010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_REC_NO").ToString()

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateSagyoDel", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E011", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "削除処理"

    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 作業データ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LME010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_REC_NO").ToString()

        'メッセージ情報を初期化する
        MyBase.SetMessage(Nothing)

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "DeleteData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E011", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            Return ds
        End If

        Return ds

    End Function
    'END YANAI 20120319　作業画面改造

#End Region

#Region "追加処理（複写）"

    ''' <summary>
    ''' 作業データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoCopy(ByVal ds As DataSet) As DataSet

        Dim sagyoRec As String = String.Empty
        sagyoRec = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_REC_NO").ToString

        If String.IsNullOrEmpty(sagyoRec) = True Then

            'ナンバーマスタから新規採番
            Dim num As New NumberMasterUtility
            sagyoRec = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LME010INOUT").Rows(0).Item("NRS_BR_CD").ToString())
            'データセットに設定
            ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_REC_NO") = sagyoRec

        End If

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "InsertSagyoCopy", ds)

        Return ds

    End Function

#End Region

#Region "一括変更処理"

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LME010OUT_UPDATE_KEY").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010OUT_UPDATE_KEY").Rows(0).Item("SAGYO_REC_NO").ToString()

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateHenko", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E011", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            Return ds
        End If


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

        ds = MyBase.CallDAC(Me._Dac, "SelectGheaderDataSagyo", ds)

        Dim rowNo As String = ds.Tables("LME010_SAGYO").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010_SAGYO").Rows(0).Item("SAGYO_REC_NO").ToString()

        Dim chkDate As String = ds.Tables("LME010_SAGYO").Rows(0).Item("SAGYO_COMP_DATE").ToString().Replace("/", "")
        Dim seiqDate As String = SelectGheaderDate(ds)

        '作業の請求日チェック
        If Me.ChkDate(chkDate, seiqDate) = False Then

            '要望番号:1045 terakawa 2013.03.28 Start
            '新黒存在チェック用データセット作成
            Dim dr As DataRow = ds.Tables("G_HED_CHK").NewRow
            dr.Item("NRS_BR_CD") = ds.Tables("LME010_SAGYO").Rows(0).Item("NRS_BR_CD")
            dr.Item("SEIQTO_CD") = ds.Tables("LME010_SAGYO").Rows(0).Item("SEIQTO_CD")
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
            'MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E285", New String() {"作業料"}, rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            'MyBase.SetMessage("E285", New String() {"作業料"})
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E805", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            '2016.01.06 UMANO 英語化対応END
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求日チェック(作業料)行複写
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyoRowCopy(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectGheaderDataSagyoHozon", ds)

        Dim rowNo As String = ds.Tables("LME010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_REC_NO").ToString()

        Dim chkDate As String = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_COMP_DATE").ToString().Replace("/", "")
        Dim seiqDate As String = SelectGheaderDate(ds)

        '作業の請求日チェック
        If Me.ChkDate(chkDate, seiqDate) = False Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E285", New String() {"作業料"}, rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            'MyBase.SetMessage("E285", New String() {"作業料"})
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E805", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            '2016.01.06 UMANO 英語化対応END
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求日チェック(作業料)行削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyoRowDel(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectGheaderDataSagyoHozon", ds)

        Dim rowNo As String = ds.Tables("LME010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim sagyoRecNo As String = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_REC_NO").ToString()

        Dim chkDate As String = ds.Tables("LME010INOUT").Rows(0).Item("SAGYO_COMP_DATE").ToString().Replace("/", "")
        Dim seiqDate As String = SelectGheaderDate(ds)
        Dim stateKb As String = SelectGheaderState(ds)
        '作業の請求日チェック
        If Me.ChkDate(chkDate, seiqDate) = False AndAlso stateKb.Equals("01") Then
            MyBase.SetMessageStore(LME010BLC.GUIDANCE_KBN, "E127", , rowNo, LME010BLC.EXCEL_COLTITLE, sagyoRecNo)
            'MyBase.SetMessage("E127")

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
    ''' 進捗区分を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>進捗区分</returns>
    ''' <remarks>取得できない場合は"00"を返却</remarks>
    Private Function SelectGheaderState(ByVal ds As DataSet) As String

        Dim dt As DataTable = ds.Tables("G_HED")
        If dt.Rows.Count < 1 Then

            Return "00"

        End If

        Return dt.Rows(0).Item("STATE_KB").ToString()

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

#Region "印刷チェック"

    Private Function SelectPrintCheck(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectPrintCheck", ds)

        Return rtnDs

    End Function

#End Region

#End Region

End Class
