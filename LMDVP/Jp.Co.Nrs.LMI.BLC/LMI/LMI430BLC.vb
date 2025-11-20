' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI430BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI430BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NAME
        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = LMI430DAC.TABLE_NAME.INPUT

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT As String = LMI430DAC.TABLE_NAME.OUTPUT

        ''' <summary>
        ''' 入力テーブル(シリンダー)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_CYLINDER As String = LMI430DAC.TABLE_NAME.IN_CYLINDER

        ''' <summary>
        ''' 出力テーブル(検品データ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_INSPECTION_DATA As String = LMI430DAC.TABLE_NAME.OUT_INSPECTION_DATA

    End Class

    ''' <summary>
    ''' 入力テーブル(シリンダー)カラム名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IN_CYL_COLUMN_NM
        Public Const NRS_BR_CD As String = LMI430DAC.IN_CYL_COLUMN_NM.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = LMI430DAC.IN_CYL_COLUMN_NM.INKA_CYL_FILE_NO_L
        Public Const INKA_CYL_FILE_NO_M As String = LMI430DAC.IN_CYL_COLUMN_NM.INKA_CYL_FILE_NO_M
        Public Const ROW_NO As String = LMI430DAC.IN_CYL_COLUMN_NM.ROW_NO
        Public Const GAS_NAME As String = LMI430DAC.IN_CYL_COLUMN_NM.GAS_NAME
        Public Const VOLUME As String = LMI430DAC.IN_CYL_COLUMN_NM.VOLUME
        Public Const SERIAL_NO As String = LMI430DAC.IN_CYL_COLUMN_NM.SERIAL_NO
    End Class

    ''' <summary>
    ''' 入力テーブルカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class INPUT_COLUMN_NM
        Public Const NRS_BR_CD As String = LMI430DAC.INPUT_COLUMN_NM.NRS_BR_CD
        Public Const INKA_CYL_FILE_NO_L As String = LMI430DAC.INPUT_COLUMN_NM.INKA_CYL_FILE_NO_L
        Public Const LOAD_FILE_NAME As String = LMI430DAC.INPUT_COLUMN_NM.LOAD_FILE_NAME
        Public Const CUST_CD_L As String = LMI430DAC.INPUT_COLUMN_NM.CUST_CD_L
        Public Const CUST_CD_M As String = LMI430DAC.INPUT_COLUMN_NM.CUST_CD_M
        Public Const INKA_DATE As String = LMI430DAC.INPUT_COLUMN_NM.INKA_DATE
        Public Const INKA_DATE_FROM As String = LMI430DAC.INPUT_COLUMN_NM.INKA_DATE_FROM
        Public Const INKA_DATE_TO As String = LMI430DAC.INPUT_COLUMN_NM.INKA_DATE_TO
        Public Const REMARK_1 As String = LMI430DAC.INPUT_COLUMN_NM.REMARK_1
        Public Const REMARK_2 As String = LMI430DAC.INPUT_COLUMN_NM.REMARK_2
        Public Const REMARK_3 As String = LMI430DAC.INPUT_COLUMN_NM.REMARK_3
        Public Const USER_CD As String = LMI430DAC.INPUT_COLUMN_NM.USER_CD
        Public Const LAST_UPD_DATE As String = LMI430DAC.INPUT_COLUMN_NM.LAST_UPD_DATE
        Public Const LAST_UPD_TIME As String = LMI430DAC.INPUT_COLUMN_NM.LAST_UPD_TIME
        Public Const SPREAD_ROW_NO As String = LMI430DAC.COL_NAME.SPREAD_ROW_NO
    End Class

    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME

        ''' <summary>
        ''' 取込ファイル一覧取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectLoadedInkaCylFileList As String = LMI430DAC.FUNCTION_NAME.SelectLoadedInkaCylFileList

        ''' <summary>
        ''' 取込ファイル一覧件数取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectLoadedInkaCylFileListCount As String = LMI430DAC.FUNCTION_NAME.SelectLoadedInkaCylFileListCount

        ''' <summary>
        ''' 検品データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectInspectionData As String = LMI430DAC.FUNCTION_NAME.SelectInspectionData


        ''' <summary>
        ''' シリンダー登録
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InsertCylinderData As String = "InsertCylinderData"

        ''' <summary>
        ''' シリンダー削除
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DeleteCylinderData As String = "DeleteCylinderData"


        ''' <summary>
        ''' 論理削除(InkaCylinderL)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SoftDeleteInkaCylinderL As String = "SoftDeleteInkaCylinderL"


        ''' <summary>
        ''' 論理削除(InkaCylinderM)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SoftDeleteInkaCylinderM As String = "SoftDeleteInkaCylinderM"


        ''' <summary>
        ''' 読取データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectReadResulData As String = LMI430DAC.FUNCTION_NAME.SelectReadResulData



    End Class


    ''' <summary>
    ''' ガイダンスID
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_ID As String = "00"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI430DAC = New LMI430DAC()

#End Region

#Region "Method"


    ''' <summary>
    ''' 取込ファイル一覧を取得する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectLoadedInkaCylFileListCount(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SelectLoadedInkaCylFileListCount, ds)
        Dim count As Integer = Me.GetResultCount
        If (count = 0) Then
            Me.SetMessage("G001")
        ElseIf (count > MyBase.GetMaxResultCount()) Then

            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf (count > MyBase.GetLimitCount()) Then

            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 取込ファイル一覧を取得する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectLoadedInkaCylFileList(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SelectLoadedInkaCylFileList, ds)

    End Function

    ''' <summary>
    ''' 検品データを取得する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInspectionData(ByVal ds As DataSet) As DataSet


        Dim wkData As DataSet = ds.Clone
        For Each row As DataRow In ds.Tables(TABLE_NAME.INPUT).Rows
            wkData.Clear()

            wkData.Tables(TABLE_NAME.INPUT).ImportRow(row)
            wkData = MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SelectInspectionData, wkData)
            ds.Tables(TABLE_NAME.OUT_INSPECTION_DATA).Merge(wkData.Tables(TABLE_NAME.OUT_INSPECTION_DATA))

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 入荷シリンダーデータを追加する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertCylinderData(ByVal ds As DataSet) As DataSet

        ' 管理番号付与
        ds = Me.SetInkaCylFileNo(ds)
        ds = MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.InsertInkaCylinderL, ds)
        If (MyBase.GetResultCount <> 1) Then
            Me.SetMessage("E547", New String() {"取込データ(大)追加"})

            Return ds
        End If


        ds = MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.InsertInkaCylinderM, ds)
        If (MyBase.GetResultCount <> ds.Tables(TABLE_NAME.IN_CYLINDER).Rows.Count) Then
            Me.SetMessage("E547", New String() {"取込データ(中)追加"})

        End If

        Return ds

    End Function


    ''' <summary>
    ''' 入荷シリンダーデータを削除する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteCylinderData(ByVal ds As DataSet) As DataSet

        Dim wkData As DataSet = ds.Clone
        For Each row As DataRow In ds.Tables(TABLE_NAME.INPUT).Rows

            wkData.Clear()
            wkData.Tables(TABLE_NAME.INPUT).ImportRow(row)

            wkData = MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SoftDeleteInkaCylinderL, wkData)
            If (MyBase.GetResultCount = 0) Then
                Me.SetMessageStore(GUIDANCE_ID, "E011", Nothing, row.Item(LMI430DAC.INPUT_COLUMN_NM.SPREAD_ROW_NO).ToString())

                Continue For
            End If

            wkData = MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SoftDeleteInkaCylinderM, wkData)

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 論理削除(InkaCylinderL)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoftDeleteInkaCylinderL(ByVal ds As DataSet) As DataSet
        Return MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SoftDeleteInkaCylinderL, ds)
    End Function

    ''' <summary>
    ''' 論理削除(InkaCylinderM)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoftDeleteInkaCylinderM(ByVal ds As DataSet) As DataSet
        Return MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SoftDeleteInkaCylinderM, ds)
    End Function


    ''' <summary>
    ''' 入荷シリンダーファイル番号設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetInkaCylFileNo(ByVal ds As DataSet) As DataSet

        ' 営業所コード取得
        Dim nrsBrCd As String = ds.Tables(TABLE_NAME.INPUT).Rows(0) _
                                  .Item(INPUT_COLUMN_NM.NRS_BR_CD).ToString()

        ' 入荷シリンダー番号生成
        Dim inkaCylFileNo As String = Me.CreateInkaCylFileNo(nrsBrCd)

        For Each row As DataRow In ds.Tables(TABLE_NAME.INPUT).Rows()
            row.Item(INPUT_COLUMN_NM.INKA_CYL_FILE_NO_L) = inkaCylFileNo
        Next

        Dim middleNoFormat As String = "{0:D3}"
        Dim middleNo As Integer = 1
        For Each row As DataRow In ds.Tables(TABLE_NAME.IN_CYLINDER).Rows()
            row.Item(IN_CYL_COLUMN_NM.INKA_CYL_FILE_NO_L) = inkaCylFileNo
            row.Item(IN_CYL_COLUMN_NM.INKA_CYL_FILE_NO_M) = String.Format(middleNoFormat, middleNo)
            middleNo += 1
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷シリンダーファイル番号生成
    ''' </summary>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <returns>入荷シリンダーファイル番号</returns>
    ''' <remarks>
    ''' </remarks>
    Private Function CreateInkaCylFileNo(ByVal nrsBrCd As String) As String

        Return (New NumberMasterUtility) _
            .GetAutoCode(NumberMasterUtility.NumberKbn.INKA_CYL_FILE_NO_L, Me, nrsBrCd)

    End Function


    ''' <summary>
    ''' 読取データを取得する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectReadResulData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, LMI430DAC.FUNCTION_NAME.SelectReadResulData, ds)


    End Function
#End Region

End Class
