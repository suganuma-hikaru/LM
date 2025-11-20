' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG040BLC : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMG040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG040DAC = New LMG040DAC()

    ''' <summary>
    '''請求サブ共通DACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _CommonDac As LMG000DAC = New LMG000DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(件数取得)
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
    ''' 請求鑑ヘッダ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "存在チェック"

    ''' <summary>
    ''' 請求鑑ヘッダ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "ExistData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E257")
        End If

        Return ds

    End Function

#End Region

#Region "確定処理"

    ''' <summary>
    ''' 確定処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateKagamiHed(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMG040IN"
        '新規登録、更新用データの設定
        Dim autoDs As DataSet = New LMG040DS()
        Dim addDs As DataSet = New LMG040DS()
        addDs = ds.Copy()
        Me.SetUnpdateData(autoDs, addDs)

        'DACクラス呼出
        If addDs.Tables(tableNm).Rows.Count > 0 Then
            'ステージ更新を行う
            ds = MyBase.CallDAC(Me._Dac, "UpStageKagamiHed", addDs)
        End If

        If autoDs.Tables(tableNm).Rows.Count > 0 Then

            Dim invDs As DataSet = New LMG000DS()
            Dim invDt As DataTable = invDs.Tables("START_DATE_IN")
            Dim invDr As DataRow = Nothing
            Dim updDs As DataSet = New LMG040DS()
            Dim updDt As DataTable = updDs.Tables(tableNm)
            Dim setDate As String = String.Empty

            Dim max As Integer = autoDs.Tables(tableNm).Rows.Count - 1

            For i As Integer = 0 To max

                With autoDs.Tables(tableNm).Rows(i)

                    '更新対象データを格納
                    updDt.Rows.Clear()
                    invDt.Rows.Clear()
                    updDt.ImportRow(autoDs.Tables(tableNm).Rows(i))

                    updDt.Rows(0).Item("STATE_KB") = "01"

                    If String.IsNullOrEmpty(.Item("UNCHIN_IMP_FROM_DATE").ToString()) _
                    OrElse String.IsNullOrEmpty(.Item("YOKOMOCHI_IMP_FROM_DATE").ToString()) _
                    OrElse String.IsNullOrEmpty(.Item("SAGYO_IMP_FROM_DATE").ToString()) Then

                        '請求開始日を検索
                        invDr = invDt.NewRow()
                        invDr.Item("SEIQTO_CD") = .Item("SEIQTO_CD")
                        invDr.Item("SKYU_DATE") = .Item("SKYU_DATE")
                        invDr.Item("NRS_BR_CD") = .Item("NRS_BR_CD")
                        invDt.Rows.Add(invDr)

                        invDs = MyBase.CallDAC(Me._CommonDac, "GetInvFrom", invDs)

                        setDate = invDs.Tables("START_DATE_OUT").Rows(0).Item("SKYU_DATE_FROM").ToString()
                        If String.IsNullOrEmpty(setDate) Then
                            setDate = "00000000"
                        Else
                            setDate = Date.ParseExact(setDate, "yyyyMMdd", Nothing).AddDays(1).ToString.Replace("/", "").Substring(0, 8)
                        End If

                        '請求日を取得し取込請求書更新用DataSetに再設定する
                        If String.IsNullOrEmpty(.Item("UNCHIN_IMP_FROM_DATE").ToString()) Then
                            updDt.Rows(0).Item("UNCHIN_IMP_FROM_DATE") = setDate
                        End If
                        If String.IsNullOrEmpty(.Item("YOKOMOCHI_IMP_FROM_DATE").ToString()) Then
                            updDt.Rows(0).Item("YOKOMOCHI_IMP_FROM_DATE") = setDate
                        End If
                        If String.IsNullOrEmpty(.Item("SAGYO_IMP_FROM_DATE").ToString()) Then
                            updDt.Rows(0).Item("SAGYO_IMP_FROM_DATE") = setDate
                        End If

                    End If

                    'ステージ、取込開始日の更新を行う
                    ds = MyBase.CallDAC(Me._Dac, "UpKakuteiHed", updDs)

                End With
            Next
        End If

        Return ds

    End Function

    ''' <summary>
    '''  取込請求書、手書き請求書データをそれぞれの更新DSに設定する
    ''' </summary>
    ''' <param name="autoDs">自動取込データ格納DS</param>
    ''' <param name="addDs">手書きデータ格納DS</param>
    ''' <remarks></remarks>
    Private Sub SetUnpdateData(ByVal autoDs As DataSet, ByVal addDs As DataSet)

        Dim tableNm As String = "LMG040IN"
        Dim autoDt As DataTable = autoDs.Tables(tableNm)
        Dim addDt As DataTable = addDs.Tables(tableNm)

        Dim selectDr As DataRow() = addDt.Select("CRT_KB = '00' AND SKYU_NO_RELATED = ''") '自動取込且つ、元請求書番号が入っていないものを抽出
        If selectDr.Length > 0 Then
            Dim max As Integer = selectDr.Length - 1
            For i As Integer = 0 To max
                '新規登録データ格納テーブルに追加する
                autoDt.ImportRow(selectDr(i))
                '更新データ格納テーブルからさ駆除する
                addDt.Rows.Remove(selectDr(i))
            Next

        End If

    End Sub

#End Region

#Region "削除処理"

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 請求先マスタのチェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqtoMsub(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "ChkSeiqtoMsub", ds)

    End Function

    ''' <summary>
    ''' 削除処理(鑑ヘッダ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateDeleteHed(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "UpdateDeleteHed", ds)

    End Function

    ''' <summary>
    ''' 削除処理(鑑明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateDeleteDtl(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "UpdateDeleteDtl", ds)

    End Function

    ''' <summary>
    ''' 初期化処理(確定済データの場合)(鑑ヘッダ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateStateKbHed(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "UpdateStateKbHed", ds)

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(初期化対象データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectClearHed(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectClearHed", ds)

    End Function

    ''' <summary>
    ''' 請求鑑明細検索処理(初期化対象データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectClearDtl(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectClearDtl", ds)

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダ追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertHed(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertHed", ds)

    End Function

    ''' <summary>
    ''' 請求鑑明細追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertDtl(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertDtl", ds)

    End Function
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

#End Region

#Region "請求データ出力処理"

    ''' <summary>
    ''' 請求データ出力用データ抽出処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SkyuCsvSelect(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCsvData", ds)

    End Function

    ''' <summary>
    ''' 請求データ出力用フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SkyuCsvUpdate(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateCsvData", ds)

    End Function

#End Region

#End Region

End Class
