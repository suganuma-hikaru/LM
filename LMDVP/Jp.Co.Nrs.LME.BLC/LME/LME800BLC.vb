' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 作業
'  プログラムID     :  LME800    : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.Text
Imports System.IO

''' <summary>
''' LME800BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LME800BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"
    ''' <summary>
    ''' 作業ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WORK_STATE_KB
        Public Const UNPROCESSED As String = "00"
        Public Const PROCESSING As String = "01"
        Public Const COMPLETED As String = "02"
        Public Const CANCEL As String = "98"
        Public Const DEL As String = "99"
    End Class

    ''' <summary>
    ''' ファンクション名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NM
        Public Const WH_SAGYO_SHIJI As String = "WHSagyoShiji"
        Public Const WH_SAGYO_SHIJI_CANCEL As String = "WHSagyoShijiCancel"
        Public Const CHECK_HAITA As String = "CheckHaita"
    End Class

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LME800DAC = New LME800DAC()


#End Region

#Region "Method"

#Region "現場作業指示"

    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function WHSagyoShiji(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LME800DAC.TABLE_NM.LME800IN)

        '作業登録データ取得
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_DATA, ds)

        '作業登録データのチェック
        If Not CheckSijiData(ds) Then
            Return ds
        End If

        '作業指示テーブル取得
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_SIJI_SELECT, ds)

        'キャンセル
        Me.CancelData(ds)

        '作業指示登録
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_SIJI_INSERT, ds)
        If Me.GetResultCount = 0 Then
            MyBase.SetMessageStore("00", "E370", , dt.Rows(0).Item("ROW_NO").ToString)
            Return ds
        End If

        '作業登録
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_INSERT, ds)
        '現場作業指示ステータス更新
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.WH_STATUS_UPDATE, ds)

        Return ds

    End Function


#End Region

#Region "出荷削除処理"
    ''' <summary>
    ''' 出荷削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LME800DAC.TABLE_NM.LME800IN)
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables(LME800DAC.TABLE_NM.LME800IN)

        'タブレット対応営業所のチェック
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.CHECK_TABLET_USE, ds)
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If

        '作業指示取得
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_SIJI_SELECT, ds)

        '作業指示取得キャンセル
        Me.CancelData(ds)

        '現場作業指示ステータス更新
        MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.WH_STATUS_UPDATE, ds)

        Return ds
    End Function
#End Region

#Region "キャンセル処理"
    ''' <summary>
    ''' キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelData(ByVal ds As DataSet) As DataSet
        If ds.Tables(LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI).Rows.Count > 0 Then
            '指示済みの場合、ステータスをチェック
            For j As Integer = 0 To ds.Tables(LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI).Rows.Count - 1

                Dim upDs As DataSet = ds.Clone
                Dim upDt As DataTable = upDs.Tables(LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI)
                upDt.ImportRow(ds.Tables(LME800DAC.TABLE_NM.LME800OUT_SAGYO_SIJI).Rows(j))

                If LME800BLC.WORK_STATE_KB.UNPROCESSED.Equals(upDt.Rows(0).Item("WORK_STATE_KB")) Then
                    '指示削除
                    MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_SIJI_DELETE, upDs)
                ElseIf LME800BLC.WORK_STATE_KB.CANCEL.Equals(upDt.Rows(0).Item("WORK_STATE_KB")) OrElse _
                       LME800BLC.WORK_STATE_KB.DEL.Equals(upDt.Rows(0).Item("WORK_STATE_KB")) Then
                    '削除、キャンセルの場合なにもしない
                Else
                    '指示キャンセル
                    MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.SAGYO_SIJI_CANCEL, upDs)
                End If

            Next
        End If
        Return ds
    End Function

#End Region

#Region "データチェック"
    ''' <summary>
    ''' 出荷削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function CheckSijiData(ByVal ds As DataSet) As Boolean

        Dim dtIn As DataTable = ds.Tables(LME800DAC.TABLE_NM.LME800IN)
        Dim dtChk As DataTable = ds.Tables(LME800DAC.TABLE_NM.LME800IN_SAGYO)
        Dim chk As Boolean = True


        Dim jisya As Boolean = False
        Dim tasya As Boolean = False
        For Each drChk As DataRow In dtChk.Rows

            '主担当作業者のチェック
            If Not "02".Equals(drChk.Item("JISYATASYA_KB").ToString) AndAlso _
                String.IsNullOrEmpty(drChk.Item("USER_NM").ToString) Then
                MyBase.SetMessageStore("00", _
                                        "E00J", _
                                        New String() {drChk.Item("TOU_NO").ToString, _
                                                      drChk.Item("SITU_NO").ToString}, _
                                                      dtIn.Rows(0).Item("ROW_NO").ToString)
                chk = False
            End If

            '自社他社区分チェック
            If "02".Equals(drChk.Item("JISYATASYA_KB").ToString) Then
                tasya = True
            Else
                jisya = True
            End If
        Next

        If jisya = False AndAlso tasya = True Then
            MyBase.SetMessageStore("00", _
                                    "E01B", _
                                    New String() {dtIn.Rows(0).Item("SAGYO_SIJI_NO").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            chk = False
        End If

        Return chk

    End Function
#End Region


#Region "排他チェック"
    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LME800DAC.TABLE_NM.LME800IN)

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, LME800DAC.FUNCTION_NM.CHECK_HAITA, ds)

        Return ds

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
