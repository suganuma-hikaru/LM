' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB810    : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.Text
Imports System.IO

''' <summary>
''' LMB810BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB810BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' 検品・ロケ設定ステータス(ヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KENPIN_STATE_KB
        Public Const UNPROCESSED As String = "00"
        Public Const KENPIN_PROCESSING As String = "01"
        Public Const KENPIN_COMPLETED As String = "02"
        Public Const LOCA_PROCESSING As String = "03"
        Public Const LOCA_COMPLETED As String = "04"
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
    Private _Dac As LMB810DAC = New LMB810DAC()


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

        Dim dt As DataTable = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN)

        If Not CheckInkaData(ds) Then
            Return ds
        End If

        '入荷作業登録
        Me.InsertSagyoData(ds)

        '検品削除・キャンセル処理
        Me.CancelData(ds)

        '検品データ登録
        Me.InsertKenpinData(ds)

        'LMSデータ更新
        Me.UpdateLMSData(ds)

        'LMS更新データ取得
        Me.SelectInkaLastUpdResults(ds)

        Return ds

    End Function

#End Region

#Region "現場作業取消"
    ''' <summary>
    ''' 現場作業取消
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN)
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables(LMB810DAC.TABLE_NM.LMB810IN)

        'タブレット対応営業所のチェック
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.CHECK_TABLET_USE, ds)
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If

        'タブレット 検品ヘッダ取得
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.KENPIN_SELECT_HEAD, ds)

        '検品キャンセル
        Me.CancelKenpinData(ds)

        'LMS入荷データ更新
        Me.UpdateLMSData(ds)

        'LMS更新データ取得
        Me.SelectInkaLastUpdResults(ds)

        Return ds
    End Function
#End Region

#Region "入荷作業登録"
    ''' <summary>
    ''' 入荷作業登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function InsertSagyoData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables(LMB810DAC.TABLE_NM.LMB810IN_SAGYO)
        Dim dt As DataTable = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN_SAGYO)

        '入荷作業取得
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.SAGYO_SELECT_LMS_DATA, ds)

        For Each dr As DataRow In dt.Rows

            inDt.Clear()
            inDt.ImportRow(dr)

            '入荷作業登録
            MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.SAGYO_INSERT, inDs)

        Next

        Return ds

    End Function

#End Region

#Region "入荷キャンセル処理"
    ''' <summary>
    ''' 入荷キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelData(ByVal ds As DataSet) As DataSet

        'タブレット 検品ヘッダ取得
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.KENPIN_SELECT_HEAD, ds)

        '検品キャンセル
        Me.CancelKenpinData(ds)

        Return ds
    End Function

    ''' <summary>
    ''' 検品キャンセル
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelKenpinData(ByVal ds As DataSet) As DataSet

        If ds.Tables(LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD).Rows.Count > 0 Then
            '指示済みの場合、ステータスをチェック
            For j As Integer = 0 To ds.Tables(LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD).Rows.Count - 1

                Dim upDs As DataSet = ds.Clone
                Dim upDt As DataTable = upDs.Tables(LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD)
                upDt.ImportRow(ds.Tables(LMB810DAC.TABLE_NM.LMB810OUT_KENPIN_HEAD).Rows(j))

                If LMB810BLC.KENPIN_STATE_KB.UNPROCESSED.Equals(upDt.Rows(0).Item("IN_KENPIN_LOC_STATE_KB")) Then
                    '指示削除
                    MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.KENPIN_DELETE, upDs)
                ElseIf LMB810BLC.KENPIN_STATE_KB.CANCEL.Equals(upDt.Rows(0).Item("IN_KENPIN_LOC_STATE_KB")) OrElse _
                       LMB810BLC.KENPIN_STATE_KB.DEL.Equals(upDt.Rows(0).Item("IN_KENPIN_LOC_STATE_KB")) Then
                    'キャンセル削除は何もしない
                Else
                    '指示キャンセル
                    MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.KENPIN_CANCEL, upDs)
                End If

            Next
        End If

        Return ds
    End Function

#End Region

#Region "入荷検品登録"
    ''' <summary>
    ''' 入荷検品登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertKenpinData(ByVal ds As DataSet) As DataSet

        '入荷検品登録(ヘッダ)
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.KENPIN_INSERT_HEAD, ds)

        '入荷検品登録(明細)
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.KENPIN_INSERT_DTL, ds)

        '画像登録
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.FILE_INSERT, ds)

        Return ds

    End Function
#End Region

#Region "LMS入荷データ更新"
    ''' <summary>
    ''' LMS入荷データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateLMSData(ByVal ds As DataSet) As DataSet

        '入荷L更新
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.UPDATE_WH_STATUS, ds)

        Return ds

    End Function
#End Region

#Region "LMS更新データ取得"
    ''' <summary>
    ''' LMS更新データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaLastUpdResults(ByVal ds As DataSet) As DataSet

        '入荷L更新
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.INKA_SELECT_UPD_RESULTS, ds)

        Return ds

    End Function
#End Region

#Region "データチェック"
    ''' <summary>
    ''' データチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function CheckInkaData(ByVal ds As DataSet) As Boolean

        Dim dtIn As DataTable = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN)
        Dim dtChk As DataTable = ds.Tables(LMB810DAC.TABLE_NM.LMB810CHECK)
        Dim chk As Boolean = True

        'ステータスチェック(検品済以外エラー)
        If Not "40".Equals(dtIn.Rows(0).Item("INKA_STATE_KB").ToString) Then
            MyBase.SetMessageStore("00", _
                                   "E00O", _
                                   New String() {dtIn.Rows(0).Item("INKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            'ステータスエラーの場合はここでチェック終了
            Return False
        End If

        If "05".Equals(dtIn.Rows(0).Item("WH_TAB_WORK_STATUS_KB").ToString) Then
            MyBase.SetMessageStore("00", _
                                   "E00X", _
                                   New String() {dtIn.Rows(0).Item("INKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            'ステータスエラーの場合はここでチェック終了
            Return False
        End If

        'データ取得必要なチェック
        'チェック用データ取得
        MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.CHECK_INKA_DATA, ds)
        'データ取得件数が0件の場合エラー
        If MyBase.GetResultCount() = 0 Then
            MyBase.SetMessageStore("00", _
                                   "E024", _
                                   New String() {dtIn.Rows(0).Item("ROW_NO").ToString})
            chk = False
        End If

        '【入荷Lに対するチェック】
        '現場作業対象チェック
        If Not "01".Equals(dtChk.Rows(0).Item("WH_TAB_YN").ToString) Then
            MyBase.SetMessageStore("00", _
                                   "E00P", _
                                   New String() {dtIn.Rows(0).Item("INKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            chk = False
        End If

        '現場作業指示のステータスチェック
        If "01".Equals(dtChk.Rows(0).Item("WH_TAB_STATUS").ToString) Then
            MyBase.SetMessageStore("00", _
                                   "E01Q", _
                                   New String() {dtIn.Rows(0).Item("INKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
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

        Dim dt As DataTable = ds.Tables(LMB810DAC.TABLE_NM.LMB810IN)

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, LMB810DAC.FUNCTION_NM.CHECK_HAITA, ds)

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
