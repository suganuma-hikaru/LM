' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM120BLC : 単価マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM120BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM120BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM120DAC = New LMM120DAC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallDAC(Me._Dac, "HaitaChk", ds)

        Return ds

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '単価マスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 単価マスタ検索処理(件数取得)
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
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    '''単価マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '単価マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectDataTankaM", ds)

        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 単価マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistTankaM(ByVal ds As DataSet) As DataSet

        ' 単価マスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "ExistTankaM", ds)

    End Function
#If True Then   'ADD 2020/12/23 017521　【LMS】単価マスタエラー通知仕様追加
    ''' <summary>
    ''' 単価マスタコード存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckTankaM_UP_GP_CD_1(ByVal ds As DataSet) As DataSet

        ' 単価マスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "CheckTankaM_UP_GP_CD_1", ds)

    End Function
#End If
    ''' <summary>
    ''' 適用開始日チェック(新規登録時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkStartDate(ByVal ds As DataSet) As DataSet

        ' 適用開始日チェック
        ds = MyBase.CallDAC(Me._Dac, "ChkStartDate", ds)
        Dim outDt As DataTable = ds.Tables("LMM120OUT")
        Dim cnt As Integer = outDt.Rows.Count
        Dim maxStartDate As String = String.Empty
        Dim inputStartDate As String = ds.Tables("LMM120IN").Rows(0).Item("STR_DATE").ToString()

        If cnt > 0 Then
            maxStartDate = outDt.Rows(0).Item("STR_DATE").ToString()
            If Convert.ToInt32(maxStartDate) >= Convert.ToInt32(inputStartDate) Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.SetMessage("E359", New String() {"適用開始日を変えて登録する", "登録済みの適用開始日より後"})
                MyBase.SetMessage("E359")
                '2016.01.06 UMANO 英語化対応END
                'START YANAI 要望番号485
                Return ds
                'END YANAI 要望番号485
            End If
        End If

        'START YANAI 要望番号485
        If cnt = 0 Then
            If ("01").Equals(Mid(inputStartDate, 7, 2)) = False Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.SetMessage("E359", New String() {"新規の単価マスターコードを登録する", "適用開始日は各月の1日"})
                MyBase.SetMessage("E888")
                '2016.01.06 UMANO 英語化対応END
                Return ds
            End If
        End If
        'END YANAI 要望番号485

        Return ds

    End Function

#Region "新規登録/更新"

    ''' <summary>
    ''' 単価マスタ 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '単価マスタ新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 単価マスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        '単価マスタ更新登録
        ds = MyBase.CallDAC(Me._Dac, "UpdateSaveData", ds)

        Return ds

    End Function

#End Region

#End Region

#Region "承認処理"

    ''' <summary>
    ''' 承認処理（申請、承認、差し戻し）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ApprovalData(ByVal ds As DataSet) As DataSet

        '承認
        ds = MyBase.CallDAC(Me._Dac, "ApprovalData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectComboSeihin", ds)

    End Function

#End Region

#End Region

End Class
