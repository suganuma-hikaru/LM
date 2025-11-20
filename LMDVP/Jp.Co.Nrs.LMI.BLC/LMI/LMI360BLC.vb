' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI360  : ＤＩＣ運賃請求明細書作成
'  作  成  者       :  [篠原]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI360BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI360BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI360DAC = New LMI360DAC()

    ''' <summary>
    ''' データ作成判定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ErrFlg As Boolean = False

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
        Dim max As Integer = 0
        Dim outZero As String = String.Empty

        'DIC横持運賃データテーブルの物理削除
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteYokoUnchin", ds)
        'エラー判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'DIC横持運賃データテーブルに追加するデータの検索
        rtnDs = MyBase.CallDAC(Me._Dac, "SelectMakeData", ds)
        'エラー判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        If rtnDs.Tables("LMI360INOUT").Rows.Count <= 0 Then
            '作成対象データがない場合
            '出荷で件数ゼロを示すフラグ ゼロ件なら"1" それ以外なら"0"
            outZero = "1"
        Else
            outZero = "0"

            '取得したデータの編集
            rtnDs = Me.MakeDataEdit(rtnDs)
            If Me._ErrFlg = True Then
                'エラーの場合終了
                Return ds
            End If

            '浮間横持運賃データテーブルの新規追加
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertYokoUnchin", rtnDs)
            'エラー判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        ''取得したデータの編集
        'rtnDs = Me.MakeDataEdit(rtnDs)
        'If Me._ErrFlg = True Then
        '    'エラーの場合終了
        '    Return ds
        'End If

        ''浮間横持運賃データテーブルの新規追加
        'rtnDs = MyBase.CallDAC(Me._Dac, "InsertYokoUnchin", rtnDs)
        ''エラー判定
        'If MyBase.IsMessageExist() = True Then
        '    Return ds
        'End If


        'DIC横持運賃データテーブル_INに追加するデータの検索
        rtnDs = MyBase.CallDAC(Me._Dac, "SelectMakeData_IN", ds)
        'エラー判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        If rtnDs.Tables("LMI360INOUT").Rows.Count <= 0 AndAlso _
        outZero = "1" Then
            '作成対象データがない場合
            MyBase.SetMessage("E463")
            Return ds
        End If

        '取得したデータの編集
        rtnDs = Me.MakeDataEdit(rtnDs)
        If Me._ErrFlg = True Then
            'エラーの場合終了
            Return ds
        End If

        '浮間横持運賃データテーブルの新規追加
        rtnDs = MyBase.CallDAC(Me._Dac, "InsertYokoUnchin", rtnDs)
        'エラー判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 浮間横持運賃データテーブル追加前の編集
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeDataEdit(ByVal ds As DataSet) As DataSet

        Dim konsu As Decimal = 0
        Dim hasu As Decimal = 0
        Dim pkgNb As Decimal = 0
        Dim irime As Decimal = 0
        Dim kgsPrice As Decimal = 0
        Dim unso_wt As Decimal = 0  'LMI690用に追加
        Dim inka_no_s As String = String.Empty    'LMI690用に追加
        Dim wflg As String = String.Empty         'LMI690用に追加
        Dim Wgaku As Decimal = 0    'LMI690用に追加
        Dim cal_unchin As Decimal = 0 'LMI690用に追加
        Dim deci_unchin As Decimal = 0 'LMI690用に追加
        Dim moto_data_kb As String = String.Empty 'LMI690用に追加。10：入荷　20：出荷 ☆
        Dim nb As Decimal = 0
        Dim max As Integer = ds.Tables("LMI360INOUT").Rows.Count - 1
        For i As Integer = 0 To max

            With ds.Tables("LMI360INOUT").Rows(i)

                konsu = Convert.ToDecimal(.Item("KONSU").ToString)
                hasu = Convert.ToDecimal(.Item("HASU").ToString)
                pkgNb = Convert.ToDecimal(.Item("PKG_NB").ToString)
                irime = Convert.ToDecimal(.Item("IRIME").ToString)
                kgsPrice = Convert.ToDecimal(.Item("KGS_PRICE").ToString)
                unso_wt = Convert.ToDecimal(.Item("UNSO_WT").ToString)    'LMI690向けに追加 

                moto_data_kb = (.Item("MOTO_DATA_KB").ToString) 'LMI690向けに追加 


                'NBの再設定
                Select Case moto_data_kb '元データ区分
                    Case "10" '入荷の場合
                        nb = (konsu * pkgNb) + hasu '☆2013/01/17以前から
                        inka_no_s = (.Item("INKA_NO_S").ToString) 'LMI690向けに追加 
                    Case "20" '出荷の場合
                        nb = Convert.ToDecimal(.Item("NB").ToString)
                        If nb = 0 Then
                            nb = Convert.ToDecimal(.Item("SEIQ_NG_NB").ToString)
                            If nb = 0 Then
                                nb = 1
                            End If
                        End If
                End Select
                'SURYOの再設定　(LMI690:CAL_SURYO) 
                '※System.Math.Roundは四捨五入関数ではないので、使用するべきではないのだが、
                '旧システムにて、ROUND関数を用いていて、合わせるためにしかたなく使用している

                '要望番号1979対応　2013/03/28　本明 Start
                'If unso_wt < irime Then
                '    '目欠
                '    '.Item("CAL_SURYO") = Convert.ToString(System.Math.Round(((konsu * pkgNb) + hasu) * unso_wt))
                '    '.Item("CAL_SURYO") = Convert.ToString(((konsu * pkgNb) + hasu) * unso_wt) '☆2013/01/17以前から
                '    .Item("CAL_SURYO") = Convert.ToDecimal(nb * unso_wt)
                '    '.Item("CAL_UNCHIN") = Convert.ToString(System.Math.Round(Convert.ToDecimal(.Item("CAL_SURYO").ToString) * kgsPrice + 0.0001))'☆2013/01/17以前から
                '    '.Item("CAL_UNCHIN") = Convert.ToString(System.Math.Round(nb * unso_wt * kgsPrice + 0.0001))
                '    .Item("CAL_UNCHIN") = Convert.ToDecimal(System.Math.Round(nb * unso_wt * kgsPrice + 0.0001))
                'Else
                '    '標準
                '    ' NB * IRIME
                '    '.Item("SURYO") = Convert.ToString(System.Math.Round(((konsu * pkgNb) + hasu) * irime)) 'LMI150から引き継ぎ
                '    '.Item("CAL_SURYO") = Convert.ToString(System.Math.Round(((konsu * pkgNb) + hasu) * irime))
                '    '.Item("CAL_SURYO") = Convert.ToString(((konsu * pkgNb) + hasu) * irime)'☆2013/01/17以前から
                '    .Item("CAL_SURYO") = Convert.ToDecimal(nb * irime)
                '    '.Item("CAL_UNCHIN") = Convert.ToString(System.Math.Round(Convert.ToDecimal(.Item("CAL_SURYO").ToString) * kgsPrice + 0.0001))'☆2013/01/17以前から
                '    '.Item("CAL_UNCHIN") = Convert.ToString(System.Math.Round((nb * irime * kgsPrice + 0.0001), 1))
                '    .Item("CAL_UNCHIN") = Convert.ToDecimal(System.Math.Round((nb * irime * kgsPrice + 0.0001)))
                'End If

                Dim dCalSuryo As Decimal = 0
                Dim dCalUnchin As Decimal = 0

                If unso_wt < irime Then
                    dCalSuryo = Math.Ceiling(Convert.ToDecimal(nb * unso_wt))
                Else
                    dCalSuryo = Math.Ceiling(Convert.ToDecimal(nb * irime))
                End If

                'dCalUnchin = Convert.ToDecimal(System.Math.Round(dCalSuryo * kgsPrice + 0.0001))
                dCalUnchin = Math.Round(dCalSuryo * kgsPrice, MidpointRounding.AwayFromZero)

                .Item("CAL_SURYO") = dCalSuryo
                .Item("CAL_UNCHIN") = dCalUnchin
                '要望番号1979対応　2013/03/28　本明 End

                cal_unchin = Convert.ToDecimal(.Item("CAL_UNCHIN").ToString)
                deci_unchin = Convert.ToDecimal(.Item("DECI_UNCHIN").ToString)


                '2013/03/29 本明　以下コメント　Start
                ''入荷管理番号（小）が複数ある場合、まとめ金額との誤差を算出して先頭（００１）へ受け渡す
                'Select moto_data_kb '元データ区分
                '    Case "10" '入荷の場合
                '        If inka_no_s <> "001" Then
                '            If wflg = "" Then
                '                wflg = "1"
                '                Wgaku = deci_unchin - cal_unchin
                '            Else
                '                Wgaku = Wgaku - cal_unchin
                '            End If
                '        Else
                '            If wflg = "1" Then
                '                If Wgaku <> 0 Then
                '                    .Item("CAL_UNCHIN") = Wgaku
                '                End If
                '            Else
                '                .Item("CAL_UNCHIN") = Convert.ToDecimal(.Item("DECI_UNCHIN").ToString)
                '            End If

                '            wflg = ""
                '            Wgaku = 0
                '        End If
                'End Select
                '2013/03/29 本明　以下コメント　End


                'YOKO_GAKUの再設定 　(LMI690:CAL_UNCHIN)
                '※System.Math.Roundは四捨五入関数ではないので、使用するべきではないのだが、
                '旧システムにて、ROUND関数を用いていて、合わせるためにしかたなく使用している
                '.Item("YOKO_GAKU") = Convert.ToString(System.Math.Round(Convert.ToDecimal(.Item("SURYO").ToString) * kgsPrice))



            End With

        Next

        Return ds

    End Function

#End Region

End Class
