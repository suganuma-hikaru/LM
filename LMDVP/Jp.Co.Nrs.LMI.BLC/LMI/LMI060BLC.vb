' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI060  : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI060DAC = New LMI060DAC()

#End Region

#Region "Method"

#Region "データ検索"

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectUnchinRecastDataメソッド呼出</remarks>
    Private Function SelectUnchinRecastData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectUnchinRecastData", ds)

    End Function

#End Region

#Region "作成処理時、既存データ検索処理"

    ''' <summary>
    ''' 作成処理時、既存データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのMakeDataCHKメソッド呼出</remarks>
    Private Function MakeDataCHK(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "MakeDataCHK", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If 1 <= count Then
            '0件の場合
            MyBase.SetMessage("W201")
        End If

        Return rtnDs

    End Function

#End Region

#Region "削除"

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        '削除処理
        Dim rtnBoolean As Boolean = Me.ServerChkJudge(ds, "DeleteData")

        Return ds

    End Function

#End Region

#Region "作成対象データ検索"

    ''' <summary>
    ''' 作成対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMakeDataメソッド呼出</remarks>
    Private Function SelectMakeData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectMakeData", ds)

    End Function

#End Region

#Region "作成処理"

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeData(ByVal ds As DataSet) As DataSet

        '税率取得
        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectTax", ds)

        'まとめ処理を行う
        rtnDs = Me.matomeShori(ds)

        Dim tariff As String = ds.Tables("LMI060IN").Rows(0).Item("TARIFF_CD").ToString
        Dim dr() As DataRow = rtnDs.Tables("LMI060INOUT").Select("1=1")
        Dim max As Integer = dr.Length - 1

        Dim setDs As DataSet = rtnDs.Copy()
        Dim inTbl As DataTable = Nothing
        Dim rtnFlg As Boolean = True

        'MCPU運賃チェックの作成(1回目)
        rtnFlg = Me.MakeDataDAC(rtnDs)
        'エラー判定
        If rtnFlg = False Then
            Return ds
        End If

        '更新区分クリア
        rtnDs = Me.upKbnClear(rtnDs)

        'データ再設定処理(毒劇、消防)
        Dim rtnDs2 As DataSet = Me.dokuShori(rtnDs)

        'MCPU運賃チェックの作成(2回目)
        rtnFlg = Me.MakeDataDAC(rtnDs2)
        'エラー判定
        If rtnFlg = False Then
            Return ds
        End If

        '更新区分クリア
        rtnDs = Me.upKbnClear(rtnDs)

        'データ再設定処理(冬期間)
        Dim rtnDs3 As DataSet = Me.toukiShori(rtnDs)

        'MCPU運賃チェックの作成(3回目)
        rtnFlg = Me.MakeDataDAC(rtnDs3)
        'エラー判定
        If rtnFlg = False Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function MakeDataDAC(ByVal ds As DataSet) As Boolean

        Dim tariff As String = ds.Tables("LMI060IN").Rows(0).Item("TARIFF_CD").ToString
        Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("UP_KBN = '01'")
        Dim max As Integer = dr.Length - 1

        Dim rtnDs As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        For i As Integer = 0 To max

            rtnDs = New LMI060DS
            '値のクリア
            inTbl = setDs.Tables("LMI060INOUT")
            inTbl.Clear()
            inTbl.ImportRow(dr(i))

            ' MCPU運賃チェックの作成
            rtnDs = MyBase.CallDAC(Me._Dac, "MakeData", setDs)

            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("S001", New String() {"MPCU運賃チェックの作成"})
                Return False
            End If

        Next

        Return True

    End Function

#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        Dim rtnds As DataSet = Me.DacAccess(ds, actionId)

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

    ''' <summary>
    ''' まとめ処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function matomeShori(ByVal ds As DataSet) As DataSet

        Dim tariff As String = ds.Tables("LMI060IN").Rows(0).Item("TARIFF_CD").ToString
        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        'Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("1=1")
        Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("1=1", "UNSO_NO_L,INOUTKA_NO_L,INOUTKA_NO_M")
        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        Dim max As Integer = dr.Length - 1

        Dim grpDr() As DataRow = Nothing
        Dim unchin As Decimal = 0
        Dim wt As Decimal = 0
        Dim seiqWt As Decimal = 0
        Dim max2 As Integer = 0

        Dim TAX_RATE As Double = 0.08

        If ds.Tables("LMI060TAX").Rows.Count > 0 Then
            TAX_RATE = CDbl(ds.Tables("LMI060TAX").Rows(0).Item("TAX_RATE").ToString)
        End If


        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        Dim seiqGroupNo As String = String.Empty
        Dim oyaUnchin As Decimal = 0
        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

        For i As Integer = 0 To max

            If (dr(i).Item("SEIQ_GROUP_NO").ToString).Equals((dr(i).Item("UNSO_NO_L").ToString)) = True Then
                '①確定請求運賃まとめ処理

                'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
                If (seiqGroupNo).Equals(dr(i).Item("SEIQ_GROUP_NO").ToString) = True Then
                    Continue For
                End If
                seiqGroupNo = dr(i).Item("SEIQ_GROUP_NO").ToString
                'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

                'グループの親の場合
                grpDr = ds.Tables("LMI060INOUT").Select(String.Concat("SEIQ_GROUP_NO = '", dr(i).Item("SEIQ_GROUP_NO").ToString, "'"))
                unchin = 0
                wt = 0
                max2 = grpDr.Length - 1
                For j As Integer = 0 To max2
                    '同じグループ内のWTの合計を求める
                    wt = wt + Convert.ToDecimal(grpDr(j).Item("WT").ToString)
                Next

                If wt = 0 Then
                    'ZERO割り回避のため、１を設定している
                    wt = 1
                End If

                'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
                'unchin = Convert.ToDecimal(System.Math.Round((Convert.ToDecimal(dr(i).Item("UNCHIN").ToString) * _
                '                                              Convert.ToDecimal(dr(i).Item("WT").ToString)) / _
                '                                              wt + 0.00001, 0))
                'For j As Integer = 0 To max2
                '    '同じグループ内の値を更新する
                '    grpDr(j).Item("UNCHIN") = Convert.ToString(unchin)
                '    grpDr(j).Item("SEIQ_WT") = grpDr(j).Item("WT").ToString
                'Next
                oyaUnchin = Convert.ToDecimal(dr(i).Item("UNCHIN").ToString)
                For j As Integer = 0 To max2
                    unchin = Convert.ToDecimal(System.Math.Round((oyaUnchin * _
                                                                  Convert.ToDecimal(grpDr(j).Item("WT").ToString)) / _
                                                                  wt + 0.00001, 0))
                    '同じグループ内の値を更新する
                    grpDr(j).Item("UNCHIN") = Convert.ToString(unchin)
                    grpDr(j).Item("SEIQ_WT") = grpDr(j).Item("WT").ToString
                Next
                'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

            ElseIf String.IsNullOrEmpty(dr(i).Item("SEIQ_GROUP_NO").ToString) = True AndAlso _
                ("1").Equals(dr(i).Item("M_CNT").ToString) = False Then
                '②確定請求運賃再計算処理

                unchin = Convert.ToDecimal(dr(i).Item("UNCHIN").ToString)
                wt = Convert.ToDecimal(dr(i).Item("WT").ToString)
                seiqWt = Convert.ToDecimal(dr(i).Item("SEIQ_WT").ToString)

                If seiqWt = 0 Then
                    'ZERO割り回避のため、１を設定している
                    seiqWt = 1
                End If

                dr(i).Item("UNCHIN") = Convert.ToDecimal((unchin * Convert.ToDecimal(wt)) / seiqWt + 0.00001)
            ElseIf String.IsNullOrEmpty(dr(i).Item("SEIQ_GROUP_NO").ToString) = True AndAlso _
                ("1").Equals(dr(i).Item("M_CNT").ToString) = True Then
                '③確定重量設定処理
                dr(i).Item("WT") = dr(i).Item("SEIQ_WT").ToString
            End If

        Next

        For i As Integer = 0 To max
            '④税込み運賃計算処理
            unchin = Convert.ToDecimal(dr(i).Item("UNCHIN").ToString)
            dr(i).Item("ZEIKOMI_UNCHIN") = Convert.ToString(unchin + System.Math.Round(unchin * TAX_RATE + 0.00001, 0))

            'この時点でのUNCHINの値をUNCHIN_HOZONに設定する
            dr(i).Item("UNCHIN_HOZON") = dr(i).Item("UNCHIN").ToString

            '更新フラグをオンにする
            dr(i).Item("UP_KBN") = "01"
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 更新区分のクリア
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function upKbnClear(ByVal ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMI060INOUT").Rows.Count - 1

        For i As Integer = 0 To max

            '更新区分のクリア
            ds.Tables("LMI060INOUT").Rows(i).Item("UP_KBN") = String.Empty

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データ再設定処理(毒劇、消防)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function dokuShori(ByVal ds As DataSet) As DataSet

        Dim inDr() As DataRow = Nothing

        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        'Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("1=1")
        Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("1=1", "UNSO_NO_L,INOUTKA_NO_L,INOUTKA_NO_M")
        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        Dim max As Integer = dr.Length - 1

        Dim seiqTariffDr() As DataRow = Nothing
        Dim seiqTariff As String = String.Empty

        Dim TAX_RATE As Double = 0.08

        If ds.Tables("LMI060TAX").Rows.Count > 0 Then
            TAX_RATE = CDbl(ds.Tables("LMI060TAX").Rows(0).Item("TAX_RATE").ToString)
        End If

        For i As Integer = 0 To max

            '更新区分のクリア
            dr(i).Item("UP_KBN") = String.Empty

            inDr = ds.Tables("LMI060IN").Select(String.Concat("TARIFF_CD = '", dr(i).Item("LOGI_CLASS_HOZON").ToString, "'"))
            If inDr.Length = 0 Then
                'INがない場合は次へ
                Continue For
            End If

            seiqTariff = String.Empty
            If String.IsNullOrEmpty(dr(i).Item("SEIQ_GROUP_NO").ToString) = False Then
                'グループの場合は親の請求タリフコードを取得
                'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
                'seiqTariffDr = ds.Tables("LMI060INOUT").Select(String.Concat("UNSO_NO_L = '", dr(i).Item("SEIQ_GROUP_NO").ToString, "'"))
                seiqTariffDr = ds.Tables("LMI060INOUT").Select(String.Concat("UNSO_NO_L = '", dr(i).Item("SEIQ_GROUP_NO").ToString, "'"), "UNSO_NO_L,INOUTKA_NO_L,INOUTKA_NO_M")
                'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
                If 0 < seiqTariffDr.Length Then
                    seiqTariff = seiqTariffDr(0).Item("SEIQ_TARIFF_CD").ToString
                End If
            Else
                seiqTariff = dr(i).Item("SEIQ_TARIFF_CD").ToString
            End If

            If (String.IsNullOrEmpty(dr(i).Item("DOKU_KB").ToString) = False OrElse _
                String.IsNullOrEmpty(dr(i).Item("SHOBO_CD").ToString) = False) AndAlso _
                ("39_MCPU").Equals(seiqTariff) = False Then
                dr(i).Item("UNCHIN") = Convert.ToString(Convert.ToDecimal(dr(i).Item("UNCHIN_HOZON").ToString) * _
                                                        Convert.ToDecimal(inDr(0).Item("WARIMASHI_NR").ToString) / 100)

                '四捨五入、切捨て、切り上げ処理を行う
                dr(i).Item("UNCHIN") = Convert.ToString(Me.roundKeisan(Convert.ToDecimal(dr(i).Item("UNCHIN").ToString), inDr(0)))

                dr(i).Item("ZEIKOMI_UNCHIN") = Convert.ToString(Convert.ToDecimal(dr(i).Item("UNCHIN").ToString) + System.Math.Round(Convert.ToDecimal(dr(i).Item("UNCHIN").ToString) * TAX_RATE + 0.00001, 0))
                dr(i).Item("LOGI_CLASS") = "15000"
                dr(i).Item("SEIQ_WT") = "0"
                dr(i).Item("WT") = "0"
                dr(i).Item("UP_KBN") = "01"

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データ再設定処理(冬期)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function toukiShori(ByVal ds As DataSet) As DataSet

        Dim inDr() As DataRow = Nothing
        Dim freeC01 As String = String.Empty

        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        'Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("1=1")
        Dim dr() As DataRow = ds.Tables("LMI060INOUT").Select("1=1", "UNSO_NO_L,INOUTKA_NO_L,INOUTKA_NO_M")
        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        Dim max As Integer = dr.Length - 1
        Dim max2 As Integer = 0
        Dim wintFrom As String = String.Empty
        Dim wintTo As String = String.Empty
        Dim outkaPlanDate As String = String.Empty
        Dim extcFlg As Boolean = False

        Dim TAX_RATE As Double = 0.08

        If ds.Tables("LMI060TAX").Rows.Count > 0 Then
            TAX_RATE = CDbl(ds.Tables("LMI060TAX").Rows(0).Item("TAX_RATE").ToString)
        End If

        '割増運賃タリフの値を取得
        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectExtcUnchin", ds)
        Dim extcDr() As DataRow = Nothing

        For i As Integer = 0 To max

            inDr = ds.Tables("LMI060IN").Select(String.Concat("TARIFF_CD = '", dr(i).Item("LOGI_CLASS_HOZON").ToString, "'"))
            If inDr.Length = 0 Then
                'INがない場合は次へ
                Continue For
            End If

            freeC01 = inDr(0).Item("FREE_C01").ToString
            If String.IsNullOrEmpty(freeC01) = True Then
                'FREE_C01が空の場合は次へ
                Continue For
            End If

            extcDr = rtnDs.Tables("EXTC_UNCHIN").Select(String.Concat("EXTC_TARIFF_CD = '", freeC01, "' AND ", _
                                                                      "JIS_CD = '", dr(i).Item("JIS").ToString, "'"))
            max2 = extcDr.Length - 1
            outkaPlanDate = Mid(dr(i).Item("OUTKA_PLAN_DATE").ToString, 5, 4)
            extcFlg = False
            For j As Integer = 0 To max2
                wintFrom = extcDr(j).Item("WINT_KIKAN_FROM").ToString
                wintTo = extcDr(j).Item("WINT_KIKAN_TO").ToString

                '期間判定
                If wintFrom <= wintTo Then
                    If (wintFrom <= outkaPlanDate AndAlso outkaPlanDate <= wintTo) Then
                        extcFlg = True
                        Exit For
                    End If
                Else
                    '年跨りVer
                    If (wintFrom <= outkaPlanDate AndAlso outkaPlanDate <= "1231") Or _
                       ("0101" <= outkaPlanDate AndAlso outkaPlanDate <= wintTo) Then
                        extcFlg = True
                        Exit For
                    End If

                End If
            Next

            If extcFlg = True Then
                dr(i).Item("UNCHIN") = Convert.ToString(System.Math.Round(Convert.ToDecimal(dr(i).Item("UNCHIN_HOZON").ToString) * 0.1 + 0.00001, 0))
                dr(i).Item("ZEIKOMI_UNCHIN") = Convert.ToString(Convert.ToDecimal(dr(i).Item("UNCHIN").ToString) + System.Math.Round(Convert.ToDecimal(dr(i).Item("UNCHIN").ToString) * TAX_RATE + 0.00001, 0))
                dr(i).Item("LOGI_CLASS") = "15500"
                dr(i).Item("SEIQ_WT") = "0"
                dr(i).Item("WT") = "0"
                dr(i).Item("UP_KBN") = "01"
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 計算処理
    ''' </summary>
    ''' <param name="value">Decimal</param>
    ''' <returns>Decimal</returns>
    ''' <remarks></remarks>
    Private Function roundKeisan(ByVal value As Decimal, ByVal indr As DataRow) As Decimal

        Dim roundKb As String = indr.Item("ROUND_KB").ToString
        Dim roundUt As Decimal = Convert.ToDecimal(indr.Item("ROUND_UT_LEN").ToString)

        Dim editValue As Decimal = value

        If roundUt = 0 Then
            'ZERO割り回避のため、１を設定している
            roundUt = 1
        End If

        If ("01").Equals(roundKb) = True Then
            '切り上げ
            editValue = Convert.ToDecimal(System.Math.Ceiling(value / roundUt) * roundUt)
        ElseIf ("02").Equals(roundKb) = True Then
            '切捨て
            editValue = Convert.ToDecimal(System.Math.Floor(value / roundUt) * roundUt)
        ElseIf ("03").Equals(roundKb) = True Then
            '四捨五入
            editValue = Convert.ToDecimal(System.Math.Floor((value / roundUt) + 0.5) * roundUt)
        End If

        Return editValue

    End Function

#End Region

End Class
