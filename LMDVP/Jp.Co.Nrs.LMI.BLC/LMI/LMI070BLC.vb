' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI070  : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI070BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI070BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI070DAC = New LMI070DAC()

    ''' <summary>
    ''' データ作成判定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _MakeFlg As Boolean = False

#End Region

#Region "Method"

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim hokanNiyakuFlg As Boolean = False     '保管料・荷役料の実施判定フラグ
        Dim sagyoFlg As Boolean = False           '作業料の実施判定フラグ
        Dim unchinFlg As Boolean = False          '運賃の実施判定フラグ
        Dim max As Integer = 0

        Me._MakeFlg = False

        If ("01").Equals(ds.Tables("LMI070IN").Rows(0).Item("MAKE_KB")) = True Then
            '全量
            hokanNiyakuFlg = True
            sagyoFlg = True
            unchinFlg = True
        ElseIf ("02").Equals(ds.Tables("LMI070IN").Rows(0).Item("MAKE_KB")) = True Then
            '保管料・荷役料
            hokanNiyakuFlg = True
        ElseIf ("03").Equals(ds.Tables("LMI070IN").Rows(0).Item("MAKE_KB")) = True Then
            '作業料
            sagyoFlg = True
        ElseIf ("04").Equals(ds.Tables("LMI070IN").Rows(0).Item("MAKE_KB")) = True Then
            '運賃
            unchinFlg = True
        End If

        'ダウケミ請求印刷テーブルの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectSeiqPrt", ds)

        If 1 <= ds.Tables("LMI070OUT_SEIQPRT").Rows.Count Then
            'ダウケミ請求印刷テーブルのデータが存在する場合

            'ダウケミ請求印刷テーブルの物理削除
            rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSeiqPrt", ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

        End If

        If hokanNiyakuFlg = True Then
            '保管料・荷役料の場合

            '作成処理
            rtnDs = Me.MakeHokanNiyaku(ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

        End If

        If sagyoFlg = True Then
            '作業の場合

            '作成処理
            rtnDs = Me.MakeSagyo(ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

        End If

        If unchinFlg = True Then
            '運賃の場合

            '作成処理
            rtnDs = Me.MakeUnchin(ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

        End If

        If Me._MakeFlg = False Then
            MyBase.SetMessage("E463")
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 保管料・荷役料の場合の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeHokanNiyaku(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        ''ダウケミ請求明細テーブルの検索
        'ds = MyBase.CallDAC(Me._Dac, "SelectSeiqMeisai", ds)

        'If 1 <= ds.Tables("LMI070OUT_SEIQMEISAI").Rows.Count Then
        '    'ダウケミ請求明細テーブルのデータが存在する場合

        '    'ダウケミ請求明細テーブルの物理削除
        '    rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSeiqMeisai", ds)

        '    'エラー判定
        '    If MyBase.IsMessageExist() = True Then
        '        MyBase.SetMessage("E011")
        '        Return ds
        '    End If

        'End If

        '保管料・荷役料を作成するためのデータの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectHokanNiyaku", ds)

        If 1 < ds.Tables("LMI070INOUT_HOKANNIYAKU").Rows.Count Then
            ''ダウケミ請求明細の作成を行う
            'rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqMeisai", ds)

            'ダウケミ請求印刷テーブルの作成を行う(保管料)
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqPrtHokan", ds)

            'ダウケミ請求印刷テーブルの作成を行う(荷役料)
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqPrtNiyaku", ds)

            Me._MakeFlg = True

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業料の場合の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeSagyo(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        '作業料を作成するためのデータの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectSagyo", ds)

        If 1 < ds.Tables("LMI070INOUT_SAGYO").Rows.Count Then

            'ダウケミ請求印刷テーブルの作成を行う(作業料)
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqPrtSagyo", ds)

            Me._MakeFlg = True

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運賃の場合の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeUnchin(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim rtnDsOutka As DataSet = Nothing
        Dim rtnDsInka As DataSet = Nothing
        Dim rtnDsUnchin As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        ''ダウケミ請求運賃テーブルの検索
        'ds = MyBase.CallDAC(Me._Dac, "SelectSeiqUnchin", ds)

        'If 1 <= ds.Tables("LMI070OUT_SEIQUNCHIN").Rows.Count Then
        '    'ダウケミ請求運賃テーブルのデータが存在する場合

        '    'ダウケミ請求運賃テーブルの物理削除
        '    rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSeiqUnchin", ds)

        '    'エラー判定
        '    If MyBase.IsMessageExist() = True Then
        '        MyBase.SetMessage("E011")
        '        Return ds
        '    End If

        'End If

        '運賃を作成するためのデータの検索(出荷のデータ）
        rtnDsOutka = MyBase.CallDAC(Me._Dac, "SelectUnchinOutka", ds)

        If 0 < rtnDsOutka.Tables("LMI070INOUT_UNCHIN").Rows.Count Then

            'ダウケミ請求明細の作成を行う前に取得したデータをいろいろ編集する
            rtnDsOutka = Me.unchinOutka(rtnDsOutka)

            ''ダウケミ請求明細の作成を行う
            'rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqUnchinOutka", rtnDsOutka)

            'ダウケミ請求印刷テーブルの作成を行う(運賃)(出荷)
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqPrtUnchinOutka", rtnDsOutka)

            Me._MakeFlg = True

        End If

        '運賃を作成するためのデータの検索(入荷のデータ）
        rtnDsInka = MyBase.CallDAC(Me._Dac, "SelectUnchinInka", ds)

        If 0 < rtnDsInka.Tables("LMI070INOUT_UNCHIN").Rows.Count Then

            'ダウケミ請求明細の作成を行う前に取得したデータをいろいろ編集する
            rtnDsInka = Me.unchinInka(rtnDsInka)

            ''ダウケミ請求明細の作成を行う
            'rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqUnchinInka", rtnDsInka)

            'ダウケミ請求印刷テーブルの作成を行う(運賃)(入荷)
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqPrtUnchinInka", rtnDsInka)

            Me._MakeFlg = True

        End If

        '運賃を作成するためのデータの検索(運賃のデータ）
        rtnDsUnchin = MyBase.CallDAC(Me._Dac, "SelectUnchin", ds)

        If 0 < rtnDsUnchin.Tables("LMI070INOUT_UNCHIN").Rows.Count Then

            'ダウケミ請求明細の作成を行う前に取得したデータをいろいろ編集する
            rtnDsUnchin = Me.unchinUnchin(rtnDsUnchin)

            ''ダウケミ請求明細の作成を行う
            'rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqUnchin", rtnDsUnchin)

            'ダウケミ請求印刷テーブルの作成を行う(運賃)(運賃)
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSeiqPrtUnchin", rtnDsUnchin)

            Me._MakeFlg = True

        End If

        Return ds

    End Function

    ''' <summary>
    ''' ダウケミ請求明細作成前の編集
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function unchinOutka(ByVal ds As DataSet) As DataSet

        Dim calWt As Decimal = 0
        Dim calKingaku As Decimal = 0

        Dim max As Integer = ds.Tables("LMI070INOUT_UNCHIN").Rows.Count - 1

        Dim outkaNoL As String = String.Empty
        Dim outkaNoMFlg As Boolean = False '出荷Lが切り変わった時の
        Dim outkaNoMFirstFlg As Boolean = False
        Dim stdWtKgs As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim seiqWt As Decimal = 0
        Dim deciKingaku As Decimal = 0
        Dim outTtlNb As Decimal = 0
        Dim value1 As Decimal = 0 'CALWTの変数A
        Dim value2 As Decimal = 0 'CALWT、CALKINGAKUで共通の変数B
        'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        Dim deciWt As Decimal = 0
        'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

        If 0 < max Then
            outkaNoL = ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("INOUTKA_NO_L").ToString
            outkaNoMFirstFlg = True
        End If

        For i As Integer = 0 To max

            With ds.Tables("LMI070INOUT_UNCHIN").Rows(i)

                '出荷(中)の一番若い数字判定処理
                If i <> max Then
                    If (outkaNoL).Equals(ds.Tables("LMI070INOUT_UNCHIN").Rows(i + 1).Item("INOUTKA_NO_L").ToString) = False Then
                        '一番若い数字の時はここ
                        outkaNoMFlg = True
                        outkaNoMFirstFlg = True
                        outkaNoL = ds.Tables("LMI070INOUT_UNCHIN").Rows(i + 1).Item("INOUTKA_NO_L").ToString
                    Else
                        '一番若い数字以外はここ
                        outkaNoMFlg = False
                    End If
                Else
                    '一番最後のレコードも必然的に一番若い数字
                    outkaNoMFlg = True
                    outkaNoMFirstFlg = True
                    outkaNoL = .Item("INOUTKA_NO_L").ToString
                End If

                stdWtKgs = Convert.ToDecimal(.Item("STD_WT_KGS").ToString)
                stdIrimeNb = Convert.ToDecimal(.Item("STD_IRIME_NB").ToString)
                outTtlNb = Convert.ToDecimal(.Item("OUTKA_TTL_NB").ToString)

                If outkaNoMFlg = False Then
                    If stdWtKgs < stdIrimeNb Then
                        '①の条件
                        calWt = Convert.ToDecimal(System.Math.Ceiling(outTtlNb * stdIrimeNb))
                    ElseIf 1 <= stdWtKgs Then
                        '②の条件
                        calWt = Convert.ToDecimal(System.Math.Ceiling(outTtlNb * stdWtKgs))
                    Else
                        '③の条件
                        calWt = stdWtKgs
                    End If
                End If

                seiqWt = Convert.ToDecimal(.Item("SEIQ_WT").ToString)
                deciKingaku = Convert.ToDecimal(.Item("DECI_KINGAKU").ToString)

                'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
                'calKingaku = Convert.ToDecimal(Math.Round((deciKingaku / seiqWt) * calWt, MidpointRounding.AwayFromZero))
                If seiqWt = 0 AndAlso _
                    deciWt = 0 Then
                    '0割り対策
                    calKingaku = 0
                Else
                    deciWt = Convert.ToDecimal(.Item("DECI_WT").ToString)
                    If 0 < seiqWt Then
                        calKingaku = Convert.ToDecimal(Math.Round((deciKingaku / seiqWt) * calWt, MidpointRounding.AwayFromZero))
                    Else
                        calKingaku = Convert.ToDecimal(Math.Round((deciKingaku / deciWt) * calWt, MidpointRounding.AwayFromZero))
                    End If
                End If
                'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

                'value1は旧システムのWwtのこと
                'value2は旧システムのWgakuのこと
                If outkaNoMFlg = False AndAlso outkaNoMFirstFlg = True Then
                    'ここに入るのは、出荷管理番号(大)が変わった後の1個目のレコード(出荷管理番号(中)の一番高い数字の時)
                    '①の条件
                    If calKingaku = 0 Then
                        calKingaku = deciKingaku / 2
                    End If
                    value1 = seiqWt - calWt
                    value2 = deciKingaku - calKingaku

                    .Item("OUTKA_NO_M_FIRST_FLG") = "00"
                    outkaNoMFirstFlg = False

                ElseIf outkaNoMFlg = False AndAlso outkaNoMFirstFlg = False Then
                    'ここに入るのは、出荷管理番号(中)が一番高い数字・若い数字以外の時
                    '②の条件
                    value1 = value1 - calWt
                    value2 = value2 - calKingaku
                    .Item("OUTKA_NO_M_FIRST_FLG") = "00"

                ElseIf outkaNoMFlg = True AndAlso outkaNoMFirstFlg = True Then
                    'ここに入るのは、出荷管理番号(中）が一番若い番号の時
                    '③の条件
                    If 0 < value1 AndAlso 0 < value2 Then
                        calWt = value1
                        calKingaku = value2
                    Else
                        calWt = seiqWt
                        calKingaku = deciKingaku
                    End If
                    .Item("OUTKA_NO_M_FIRST_FLG") = "01"
                    value1 = 0
                    value2 = 0
                End If

                .Item("CAL_WT") = calWt
                .Item("CAL_KINGAKU") = calKingaku

            End With

        Next

        Return ds

    End Function

    ''' <summary>
    ''' ダウケミ請求明細作成前の編集
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function unchinInka(ByVal ds As DataSet) As DataSet

        Dim inkaNoMFlg As Boolean = False '出荷Lが切り変わった時の
        Dim inkaNoMFirstFlg As Boolean = False
        Dim inkaNoL As String = String.Empty
        Dim unsoNoL As String = String.Empty
        Dim unsoNoM As String = String.Empty

        Dim deciKingaku As Decimal = 0
        Dim seiqWt As Decimal = 0
        Dim calNb As Decimal = 0
        Dim calWt As Decimal = 0
        Dim value As Decimal = 0
        'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        Dim deciWt As Decimal = 0
        'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

        Dim max As Integer = ds.Tables("LMI070INOUT_UNCHIN").Rows.Count - 1

        If 0 < max Then
            inkaNoL = ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("INOUTKA_NO_L").ToString
            inkaNoMFirstFlg = True
            unsoNoL = ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("UNSO_NO_L").ToString
            unsoNoM = ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("UNSO_NO_M").ToString
            ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("UNSO_NO_FLG") = "01"
        End If

        For i As Integer = 0 To max

            With ds.Tables("LMI070INOUT_UNCHIN").Rows(i)

                deciKingaku = Convert.ToDecimal(.Item("DECI_KINGAKU").ToString)
                seiqWt = Convert.ToDecimal(.Item("SEIQ_WT").ToString)
                calNb = Convert.ToDecimal(.Item("CAL_NB").ToString)
                calWt = Convert.ToDecimal(.Item("CAL_WT").ToString)
                'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
                deciWt = Convert.ToDecimal(.Item("DECI_WT").ToString)
                'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

                'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
                'If deciKingaku = 0 OrElse _
                '    seiqWt = 0 OrElse _
                '    calNb = 0 OrElse _
                '    calWt = 0 Then
                '    '0割り対策
                '    value = 0
                'Else
                '    value = Convert.ToDecimal(Math.Round(calNb * calWt, MidpointRounding.AwayFromZero))
                '    value = Convert.ToDecimal(Math.Round(deciKingaku / seiqWt * value, MidpointRounding.AwayFromZero))
                'End If
                If seiqWt = 0 AndAlso _
                    deciWt = 0 Then
                    '0割り対策
                    value = 0
                Else
                    value = Convert.ToDecimal(Math.Round(calNb * calWt, MidpointRounding.AwayFromZero))
                    If 0 < seiqWt Then
                        value = Convert.ToDecimal(Math.Round(deciKingaku / seiqWt * value, MidpointRounding.AwayFromZero))
                    Else
                        value = Convert.ToDecimal(Math.Round(deciKingaku / deciWt * value, MidpointRounding.AwayFromZero))
                    End If
                End If
                'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

                .Item("CAL_KINGAKU") = Convert.ToString(value)

                '入荷(中)の一番若い数字判定処理
                If i <> max Then
                    If (inkaNoL).Equals(ds.Tables("LMI070INOUT_UNCHIN").Rows(i + 1).Item("INOUTKA_NO_L").ToString) = False Then
                        '一番若い数字の時はここ
                        inkaNoMFlg = True
                        inkaNoMFirstFlg = True
                        inkaNoL = ds.Tables("LMI070INOUT_UNCHIN").Rows(i + 1).Item("INOUTKA_NO_L").ToString
                        .Item("OUTKA_NO_M_FIRST_FLG") = "01"
                    Else
                        '一番若い数字以外はここ
                        inkaNoMFlg = False
                        .Item("OUTKA_NO_M_FIRST_FLG") = "00"
                    End If
                Else
                    '一番最後のレコードも必然的に一番若い数字
                    inkaNoMFlg = True
                    inkaNoMFirstFlg = True
                    inkaNoL = .Item("INOUTKA_NO_L").ToString
                    .Item("OUTKA_NO_M_FIRST_FLG") = "01"
                End If

                If (unsoNoL).Equals(ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("UNSO_NO_L").ToString) OrElse _
                    (unsoNoM).Equals(ds.Tables("LMI070INOUT_UNCHIN").Rows(0).Item("UNSO_NO_M").ToString) Then
                    .Item("UNSO_NO_FLG") = "01"
                Else
                    .Item("UNSO_NO_FLG") = "00"
                End If

            End With

        Next

        Return ds

    End Function

    ''' <summary>
    ''' ダウケミ請求明細作成前の編集
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function unchinUnchin(ByVal ds As DataSet) As DataSet

        Dim deciKingaku As Decimal = 0
        Dim seiqWt As Decimal = 0
        Dim calNb As Decimal = 0
        Dim calWt As Decimal = 0
        Dim value As Decimal = 0
        'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
        Dim deciWt As Decimal = 0
        'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

        Dim max As Integer = ds.Tables("LMI070INOUT_UNCHIN").Rows.Count - 1

        For i As Integer = 0 To max

            With ds.Tables("LMI070INOUT_UNCHIN").Rows(i)

                deciKingaku = Convert.ToDecimal(.Item("DECI_KINGAKU").ToString)
                seiqWt = Convert.ToDecimal(.Item("SEIQ_WT").ToString)
                calNb = Convert.ToDecimal(.Item("CAL_NB").ToString)
                calWt = Convert.ToDecimal(.Item("CAL_WT").ToString)
                'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
                deciWt = Convert.ToDecimal(.Item("DECI_WT").ToString)
                'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド

                'START YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
                'If deciKingaku = 0 OrElse _
                '    seiqWt = 0 OrElse _
                '    calNb = 0 OrElse _
                '    calWt = 0 Then
                '    '0割り対策
                '    value = 0
                'Else
                '    value = Convert.ToDecimal(Math.Round(calNb * calWt, MidpointRounding.AwayFromZero))
                '    value = Convert.ToDecimal(Math.Round(deciKingaku / seiqWt * value, MidpointRounding.AwayFromZero))
                '    If 0 < seiqWt Then
                '        value = Convert.ToDecimal(Math.Round(deciKingaku / seiqWt * value, MidpointRounding.AwayFromZero))
                '    Else
                '        value = Convert.ToDecimal(Math.Round(deciKingaku / deciWt * value, MidpointRounding.AwayFromZero))
                '    End If
                'End If
                If seiqWt = 0 AndAlso _
                    deciWt = 0 Then
                    '0割り対策
                    value = 0
                Else
                    value = Convert.ToDecimal(Math.Round(calNb * calWt, MidpointRounding.AwayFromZero))
                    If 0 < seiqWt Then
                        value = Convert.ToDecimal(Math.Round(deciKingaku / seiqWt * value, MidpointRounding.AwayFromZero))
                    Else
                        value = Convert.ToDecimal(Math.Round(deciKingaku / deciWt * value, MidpointRounding.AwayFromZero))
                    End If
                    'END YANAI 要望番号1042 請求データ作成（ダウケミカル用）の作成処理にて、アベンド
                End If

                .Item("CAL_KINGAKU") = Convert.ToString(value)

            End With

        Next

        Return ds

    End Function

#End Region

End Class
