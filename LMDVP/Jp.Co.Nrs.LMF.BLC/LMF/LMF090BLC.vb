' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF090BLC : 支払編集
'  作  成  者       :  YANAI
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF090BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF090BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF090DAC = New LMF090DAC()

    ''' <summary>
    ''' データセットテーブル名(G_KAGAMI_HEDテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const G_HED As String = "G_KAGAMI_HED"

    ''' <summary>
    ''' データセットテーブル名(OUTテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF090OUT"

    ''' <summary>
    ''' OUT_ANBUNテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT_ANBUN As String = "LMF090OUT_ANBUN"

    ''' <summary>
    ''' IN_SHIHARAIテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN_SHIHARAI As String = "LMF090IN_SHIHARAI"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃マスタ更新対象データ件数検索
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
    ''' 運賃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 運賃マスタ更新(保存)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnchinM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateUnchinM", ds)

    End Function

    ''' <summary>
    ''' 運賃マスタ更新(確定)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnchinKakuteiM(ByVal ds As DataSet) As DataSet

        Dim newDs As DataSet = New LMF090DS()
        Dim newDt As DataTable = newDs.Tables(LMF090BLC.TABLE_NM_OUT)
        Dim newDr As DataRow = newDt.NewRow()

        Dim inDr() As DataRow = ds.Tables(LMF090BLC.TABLE_NM_OUT).Select(String.Empty, " TRIP_NO, UNSO_NO_L, UNSO_NO_M ")
        Dim max As Integer = inDr.Length - 1
        Dim tripNo As String = String.Empty

        For i As Integer = 0 To max
            newDt.Clear()

            If String.IsNullOrEmpty(inDr(i).Item("TRIP_NO").ToString) = True Then
                '運行番号が空の場合
                '同じ運行番号のデータを取得
                newDt.ImportRow(inDr(i))
                newDs = MyBase.CallDAC(Me._Dac, "SelectAnbunData", newDs)

                '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
                newDs = Me.SetUnchinData(newDs)
                newDs = MyBase.CallDAC(Me._Dac, "UpdateUnchinKakuteiAnbun", newDs)

            ElseIf (tripNo).Equals(inDr(i).Item("TRIP_NO").ToString) = False Then

                '按分対象データの取得(同じ運行番号のデータを取得)
                newDt.ImportRow(inDr(i))
                newDs = MyBase.CallDAC(Me._Dac, "SelectAnbunData", newDs)

                '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
                newDs = Me.SetUnchinData(newDs)
                newDs = MyBase.CallDAC(Me._Dac, "UpdateUnchinKakuteiAnbun", newDs)

                '運行番号の保存
                tripNo = inDr(i).Item("TRIP_NO").ToString
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新(確定解除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnchinKakuteiKijoM(ByVal ds As DataSet) As DataSet

        Dim newDs As DataSet = New LMF090DS()
        Dim newDt As DataTable = newDs.Tables(LMF090BLC.TABLE_NM_OUT)
        Dim newDr As DataRow = newDt.NewRow()

        Dim inDr() As DataRow = ds.Tables(LMF090BLC.TABLE_NM_OUT).Select(String.Empty, " TRIP_NO, UNSO_NO_L, UNSO_NO_M ")
        Dim max As Integer = inDr.Length - 1
        Dim tripNo As String = String.Empty

        For i As Integer = 0 To max
            newDt.Clear()

            If String.IsNullOrEmpty(inDr(i).Item("TRIP_NO").ToString) = True Then
                '運行番号が空の場合
                '同じ運行番号のデータを取得
                newDt.ImportRow(inDr(i))
                newDs = MyBase.CallDAC(Me._Dac, "UpdateUnchinKakuteiKijoM", ds)

            ElseIf (tripNo).Equals(inDr(i).Item("TRIP_NO").ToString) = False Then

                '按分対象データの取得(同じ運行番号のデータを取得)
                newDt.ImportRow(inDr(i))
                newDs = MyBase.CallDAC(Me._Dac, "SelectAnbunData", newDs)

                '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
                newDs = Me.SetCancelUnchinData(newDs)
                newDs = MyBase.CallDAC(Me._Dac, "UpdateUnchinKakuteiKijoAnbun", newDs)

                '運行番号の保存
                tripNo = inDr(i).Item("TRIP_NO").ToString
            End If

        Next

        Return ds

    End Function

    'START YANAI 要望番号1424 支払処理 按分しないようにする
    '''' <summary>
    '''' 按分処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function SetUnchinData(ByVal ds As DataSet) As DataSet

    '    Dim max As Integer = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows.Count - 1
    '    Dim sumDeciWt As Double = 0
    '    Dim sumDeciUnchin As Double = 0
    '    Dim sumDeciCity As Double = 0
    '    Dim sumDeciWint As Double = 0
    '    Dim sumDeciRely As Double = 0
    '    Dim sumDeciToll As Double = 0
    '    Dim sumDeciInsu As Double = 0

    '    Dim deciWt As Double = 0
    '    Dim deciUnchin As Double = 0
    '    Dim deciCity As Double = 0
    '    Dim deciWint As Double = 0
    '    Dim deciRely As Double = 0
    '    Dim deciToll As Double = 0
    '    Dim deciInsu As Double = 0

    '    Dim cntDeciUnchin As Double = 0
    '    Dim cntDeciCity As Double = 0
    '    Dim cntDeciWint As Double = 0
    '    Dim cntDeciRely As Double = 0
    '    Dim cntDeciToll As Double = 0
    '    Dim cntDeciInsu As Double = 0

    '    For i As Integer = 0 To max
    '        '各値の合計を求める
    '        sumDeciWt = sumDeciWt + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_WT").ToString) 'DECI_WTにはまとめ時、親に合計が設定され、子供はまとめ前の値が設定されているため、おかしくなってしまう
    '        sumDeciUnchin = sumDeciUnchin + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_UNCHIN").ToString)
    '        sumDeciCity = sumDeciCity + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_CITY_EXTC").ToString)
    '        sumDeciWint = sumDeciWint + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_WINT_EXTC").ToString)
    '        sumDeciRely = sumDeciRely + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_RELY_EXTC").ToString)
    '        sumDeciToll = sumDeciToll + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_TOLL").ToString)
    '        sumDeciInsu = sumDeciInsu + Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_INSU").ToString)
    '    Next

    '    ''運行の支払金額(手入力金額)が設定されている場合
    '    'If Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(0).Item("SHIHARAI_UNCHIN").ToString) > 0 Then
    '    '    sumDeciUnchin = Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(0).Item("SHIHARAI_UNCHIN").ToString)
    '    '    sumDeciCity = 0
    '    '    sumDeciWint = 0
    '    '    sumDeciRely = 0
    '    '    sumDeciToll = 0
    '    '    sumDeciInsu = 0
    '    'End If

    '    Dim newDs As DataSet = New LMF090DS()
    '    Dim newDt As DataTable = newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI)
    '    Dim newDr As DataRow = newDt.NewRow()
    '    For i As Integer = 0 To max
    '        newDr = newDt.NewRow()

    '        newDr("NRS_BR_CD") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NRS_BR_CD").ToString
    '        newDr("UNSO_NO_L") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_L").ToString
    '        newDr("UNSO_NO_M") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_M").ToString
    '        newDr("SHIHARAI_FIXED_FLAG") = "01"
    '        newDr("SYS_UPD_DATE") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_DATE").ToString
    '        newDr("SYS_UPD_TIME") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_TIME").ToString
    '        newDr("SHIHARAI_SYARYO_KB") = String.Empty
    '        newDr("SHIHARAI_PKG_UT_KB") = String.Empty
    '        newDr("SHIHARAI_DANGER_KB") = String.Empty
    '        newDr("SHIHARAI_TARIFF_BUNRUI_KB") = String.Empty
    '        newDr("SHIHARAI_WT") = "0"
    '        newDr("DECI_NG_NB") = "0"
    '        newDr("DECI_KYORI") = "0"
    '        newDr("DECI_WT") = "0"
    '        newDr("DECI_UNCHIN") = "0"
    '        newDr("DECI_CITY_EXTC") = "0"
    '        newDr("DECI_WINT_EXTC") = "0"
    '        newDr("DECI_RELY_EXTC") = "0"
    '        newDr("DECI_TOLL") = "0"
    '        newDr("DECI_INSU") = "0"
    '        newDr("TAX_KB") = String.Empty
    '        newDr("REMARK") = String.Empty
    '        newDr("SHIHARAI_TARIFF_CD") = String.Empty
    '        newDr("SHIHARAI_ETARIFF_CD") = String.Empty
    '        newDr("SHIHARAI_GROUP_NO") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString
    '        newDr("SHIHARAI_GROUP_NO_M") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString
    '        newDr("UNTIN_CALCULATION_KB") = String.Empty

    '        deciWt = Convert.ToDouble(ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_WT").ToString) 'DECI_WTにはまとめ時、親に合計が設定され、子供はまとめ前の値が設定されているため、おかしくなってしまう

    '        If sumDeciUnchin * deciWt = 0 Then
    '            '0割り対策
    '            deciUnchin = 0
    '        Else
    '            deciUnchin = Me.ToHalfAdjust(sumDeciUnchin * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciCity * deciWt = 0 Then
    '            '0割り対策
    '            deciCity = 0
    '        Else
    '            deciCity = Me.ToHalfAdjust(sumDeciCity * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciWint * deciWt = 0 Then
    '            deciWint = 0
    '            '0割り対策
    '        Else
    '            deciWint = Me.ToHalfAdjust(sumDeciWint * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciRely * deciWt = 0 Then
    '            '0割り対策
    '            deciRely = 0
    '        Else
    '            deciRely = Me.ToHalfAdjust(sumDeciRely * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciToll * deciWt = 0 Then
    '            '0割り対策
    '            deciToll = 0
    '        Else
    '            deciToll = Me.ToHalfAdjust(sumDeciToll * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciInsu * deciWt = 0 Then
    '            '0割り対策
    '            deciInsu = 0
    '        Else
    '            deciInsu = Me.ToHalfAdjust(sumDeciInsu * deciWt / sumDeciWt, 2)
    '        End If

    '        newDr("KANRI_UNCHIN") = Convert.ToString(deciUnchin)
    '        newDr("KANRI_CITY_EXTC") = Convert.ToString(deciCity)
    '        newDr("KANRI_WINT_EXTC") = Convert.ToString(deciWint)
    '        newDr("KANRI_RELY_EXTC") = Convert.ToString(deciRely)
    '        newDr("KANRI_TOLL") = Convert.ToString(deciToll)
    '        newDr("KANRI_INSU") = Convert.ToString(deciInsu)

    '        '行追加
    '        newDt.Rows.Add(newDr)

    '        cntDeciUnchin = cntDeciUnchin + deciUnchin
    '        cntDeciCity = cntDeciCity + deciCity
    '        cntDeciWint = cntDeciWint + deciWint
    '        cntDeciRely = cntDeciRely + deciRely
    '        cntDeciToll = cntDeciToll + deciToll
    '        cntDeciInsu = cntDeciInsu + deciInsu

    '    Next

    '    '残額調整
    '    newDt.Rows(0).Item("KANRI_UNCHIN") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_UNCHIN").ToString) + sumDeciUnchin - cntDeciUnchin)
    '    newDt.Rows(0).Item("KANRI_CITY_EXTC") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_CITY_EXTC").ToString) + sumDeciCity - cntDeciCity)
    '    newDt.Rows(0).Item("KANRI_WINT_EXTC") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_WINT_EXTC").ToString) + sumDeciWint - cntDeciWint)
    '    newDt.Rows(0).Item("KANRI_RELY_EXTC") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_RELY_EXTC").ToString) + sumDeciRely - cntDeciRely)
    '    newDt.Rows(0).Item("KANRI_TOLL") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_TOLL").ToString) + sumDeciToll - cntDeciToll)
    '    newDt.Rows(0).Item("KANRI_INSU") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_INSU").ToString) + sumDeciInsu - cntDeciInsu)

    '    'まとめデータの場合、親に子の金額を加算。子は0を設定。
    '    Dim grpDr() As DataRow = Nothing
    '    max = newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows.Count - 1
    '    For i As Integer = 0 To max
    '        If String.IsNullOrEmpty(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("SHIHARAI_GROUP_NO").ToString) = False AndAlso _
    '            ((newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("UNSO_NO_L").ToString).Equals(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("SHIHARAI_GROUP_NO").ToString) = False OrElse _
    '             (newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("UNSO_NO_M").ToString).Equals(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString) = False) Then
    '            '親データを探して、加算する
    '            grpDr = newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Select(String.Concat("UNSO_NO_L = '", newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("SHIHARAI_GROUP_NO").ToString, "' AND ", _
    '                                                                                      "UNSO_NO_M = '", newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString, "'"))
    '            If grpDr.Length > 0 Then
    '                grpDr(0).Item("KANRI_UNCHIN") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_UNCHIN").ToString) + Convert.ToDouble(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_UNCHIN").ToString))
    '                grpDr(0).Item("KANRI_CITY_EXTC") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_CITY_EXTC").ToString) + Convert.ToDouble(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_CITY_EXTC").ToString))
    '                grpDr(0).Item("KANRI_WINT_EXTC") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_WINT_EXTC").ToString) + Convert.ToDouble(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_WINT_EXTC").ToString))
    '                grpDr(0).Item("KANRI_RELY_EXTC") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_RELY_EXTC").ToString) + Convert.ToDouble(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_RELY_EXTC").ToString))
    '                grpDr(0).Item("KANRI_TOLL") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_TOLL").ToString) + Convert.ToDouble(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_TOLL").ToString))
    '                grpDr(0).Item("KANRI_INSU") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_INSU").ToString) + Convert.ToDouble(newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_INSU").ToString))

    '                newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_UNCHIN") = "0"
    '                newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_CITY_EXTC") = "0"
    '                newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_WINT_EXTC") = "0"
    '                newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_RELY_EXTC") = "0"
    '                newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_TOLL") = "0"
    '                newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI).Rows(i).Item("KANRI_INSU") = "0"
    '            End If

    '        End If
    '    Next

    '    Return newDs

    'End Function
    ''' <summary>
    ''' 按分処理☆☆確定時の按分処理は必要ないということなので、実際は按分してません
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows.Count - 1

        Dim newDs As DataSet = New LMF090DS()
        Dim newDt As DataTable = newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI)
        Dim newDr As DataRow = newDt.NewRow()
        For i As Integer = 0 To max
            newDr = newDt.NewRow()

            newDr("NRS_BR_CD") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NRS_BR_CD").ToString
            newDr("UNSO_NO_L") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_L").ToString
            newDr("UNSO_NO_M") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_M").ToString
            newDr("SHIHARAI_FIXED_FLAG") = "01"
            newDr("SYS_UPD_DATE") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_DATE").ToString
            newDr("SYS_UPD_TIME") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_TIME").ToString
            newDr("SHIHARAI_SYARYO_KB") = String.Empty
            newDr("SHIHARAI_PKG_UT_KB") = String.Empty
            newDr("SHIHARAI_DANGER_KB") = String.Empty
            newDr("SHIHARAI_TARIFF_BUNRUI_KB") = String.Empty
            newDr("SHIHARAI_WT") = "0"
            newDr("DECI_NG_NB") = "0"
            newDr("DECI_KYORI") = "0"
            newDr("DECI_WT") = "0"
            newDr("DECI_UNCHIN") = "0"
            newDr("DECI_CITY_EXTC") = "0"
            newDr("DECI_WINT_EXTC") = "0"
            newDr("DECI_RELY_EXTC") = "0"
            newDr("DECI_TOLL") = "0"
            newDr("DECI_INSU") = "0"
            newDr("TAX_KB") = String.Empty
            newDr("REMARK") = String.Empty
            newDr("SHIHARAI_TARIFF_CD") = String.Empty
            newDr("SHIHARAI_ETARIFF_CD") = String.Empty
            newDr("SHIHARAI_GROUP_NO") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString
            newDr("SHIHARAI_GROUP_NO_M") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString
            newDr("UNTIN_CALCULATION_KB") = String.Empty

            newDr("KANRI_UNCHIN") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_UNCHIN").ToString
            newDr("KANRI_CITY_EXTC") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_CITY_EXTC").ToString
            newDr("KANRI_WINT_EXTC") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_WINT_EXTC").ToString
            newDr("KANRI_RELY_EXTC") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_RELY_EXTC").ToString
            newDr("KANRI_TOLL") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_TOLL").ToString
            newDr("KANRI_INSU") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_INSU").ToString

            '行追加
            newDt.Rows.Add(newDr)

        Next

        Return newDs

    End Function
    'END YANAI 要望番号1424 支払処理 按分しないようにする

    ''' <summary>
    ''' 確定キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetCancelUnchinData(ByVal ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows.Count - 1

        Dim newDs As DataSet = New LMF090DS()
        Dim newDt As DataTable = newDs.Tables(LMF090BLC.TABLE_NM_IN_SHIHARAI)
        Dim newDr As DataRow = newDt.NewRow()
        For i As Integer = 0 To max
            newDr = newDt.NewRow()

            newDr("NRS_BR_CD") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NRS_BR_CD").ToString
            newDr("UNSO_NO_L") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_L").ToString
            newDr("UNSO_NO_M") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_M").ToString
            newDr("SHIHARAI_FIXED_FLAG") = "00"
            newDr("SYS_UPD_DATE") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_DATE").ToString
            newDr("SYS_UPD_TIME") = ds.Tables(LMF090BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_TIME").ToString
            newDr("SHIHARAI_SYARYO_KB") = String.Empty
            newDr("SHIHARAI_PKG_UT_KB") = String.Empty
            newDr("SHIHARAI_DANGER_KB") = String.Empty
            newDr("SHIHARAI_TARIFF_BUNRUI_KB") = String.Empty
            newDr("SHIHARAI_WT") = "0"
            newDr("DECI_NG_NB") = "0"
            newDr("DECI_KYORI") = "0"
            newDr("DECI_WT") = "0"
            newDr("DECI_UNCHIN") = "0"
            newDr("DECI_CITY_EXTC") = "0"
            newDr("DECI_WINT_EXTC") = "0"
            newDr("DECI_RELY_EXTC") = "0"
            newDr("DECI_TOLL") = "0"
            newDr("DECI_INSU") = "0"
            newDr("TAX_KB") = String.Empty
            newDr("REMARK") = String.Empty
            newDr("SHIHARAI_TARIFF_CD") = String.Empty
            newDr("SHIHARAI_ETARIFF_CD") = String.Empty
            newDr("SHIHARAI_GROUP_NO") = String.Empty
            newDr("SHIHARAI_GROUP_NO_M") = String.Empty
            newDr("UNTIN_CALCULATION_KB") = String.Empty
            newDr("KANRI_UNCHIN") = "0"
            newDr("KANRI_CITY_EXTC") = "0"
            newDr("KANRI_WINT_EXTC") = "0"
            newDr("KANRI_RELY_EXTC") = "0"
            newDr("KANRI_TOLL") = "0"
            newDr("KANRI_INSU") = "0"

            '行追加
            newDt.Rows.Add(newDr)

        Next

        Return newDs

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運賃テーブル排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaUnchinM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "SelectUnchinM", ds)

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HedChcik(ByVal ds As DataSet) As DataSet

        '最大日付の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSeiChek", ds)

        Dim Kijun As String = "01"
        Dim drs As DataRow() = Nothing
        Dim Haneti As String = String.Empty
        Dim dt As DataTable = ds.Tables(LMF090BLC.TABLE_NM_OUT)
        Dim Moto As String = String.Empty
        Dim Hkaku As String = "40"
        Dim Shuka As String = String.Empty
        Dim Arr As String = String.Empty
        Dim max As Integer = dt.Rows.Count - 1


        For i As Integer = 0 To max

            Dim dr As DataRow = dt.Rows(i)

            '元データ区分、出荷日、納入日を取得
            Moto = dr.Item("MOTO_DATA_KB").ToString()
            Shuka = dr.Item("OUTKA_PLAN_DATE").ToString()
            Arr = dr.Item("ARR_PLAN_DATE").ToString()

            '出荷日、納入日が入力されていない場合
            If String.IsNullOrEmpty(Shuka) = True AndAlso _
            String.IsNullOrEmpty(Arr) = True Then

                Return ds

            End If

            Select Case Moto

                Case "10"

                    '元データ区分が10の場合
                    Return Me.KagamiHantei(ds, Kijun, "02", Arr, "")

                Case "20"

                    '元データ区分が20の場合
                    Return Me.KagamiHantei(ds, Kijun, Haneti, Arr, Shuka)

                Case "40"

                    '元データ区分が40の場合
                    Return Me.KagamiHantei(ds, Kijun, Haneti, Arr, Shuka)

            End Select

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 経理取込判定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="kijun">判定用の基準</param>
    ''' <param name="hantei">運賃締め基準</param>
    ''' <param name="arr">納入日</param>
    ''' <param name="shuka">出荷日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function KagamiHantei(ByVal ds As DataSet, ByVal kijun As String, ByVal hantei As String _
                                 , ByVal arr As String, ByVal shuka As String) As DataSet

        Dim prmdt As DataTable = ds.Tables(LMF090BLC.G_HED)
        Dim prmdr As DataRow = Nothing
        Dim skyu As String = String.Empty
        Dim count As Integer = prmdt.Rows.Count

        '請求ヘッダのレコードが取得できなかった場合
        If count < 1 Then

            skyu = "00000000"

        Else

            prmdr = prmdt.Rows(0)
            skyu = prmdr.Item("SKYU_DATE").ToString()

        End If

        '運賃締め基準が01の場合
        If kijun.Equals(hantei) = True Then

            '最大の請求日と出荷日を見比べる
            If shuka <= skyu Then

                'メッセージのセット
                MyBase.SetMessage("E307")

                Return ds

            End If

        Else

            '最大の請求日と納入日を見比べる
            If arr <= skyu Then
                'メッセージのセット
                MyBase.SetMessage("E307")

                Return ds

            End If

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="dValue">Double</param>
    ''' <param name="iDigits">Integer</param>
    ''' <returns>計算結果</returns>
    ''' <remarks>四捨五入を行う。</remarks>
    Private Function ToHalfAdjust(ByVal dValue As Double, ByVal iDigits As Integer) As Double

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return System.Math.Floor((dValue * dCoef) + 0.5) / dCoef
        Else
            Return System.Math.Ceiling((dValue * dCoef) - 0.5) / dCoef
        End If

    End Function

#End Region

#End Region

End Class
