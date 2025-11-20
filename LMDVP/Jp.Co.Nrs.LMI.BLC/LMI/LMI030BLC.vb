' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI030  : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI030DAC = New LMI030DAC()

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
        Dim hokanFlg As Boolean = False     '保管の実施判定フラグ
        Dim niyakuFlg As Boolean = False    '荷役の実施判定フラグ
        Dim unchinFlg As Boolean = False    '運賃の実施判定フラグ
        Dim max As Integer = 0

        If ("00").Equals(ds.Tables("LMI030IN").Rows(0).Item("SEKY_KMK1")) = False Then
            '保管
            hokanFlg = True
        End If
        If ("00").Equals(ds.Tables("LMI030IN").Rows(0).Item("SEKY_KMK2")) = False Then
            '荷役
            niyakuFlg = True
        End If
        If ("00").Equals(ds.Tables("LMI030IN").Rows(0).Item("SEKY_KMK3")) = False Then
            '運賃
            unchinFlg = True
        End If

        'デュポン請求ファイルの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectInterFace", ds)

        If 1 <= ds.Tables("LMI030OUT_SELECT").Rows.Count Then
            'デュポン請求ファイルのデータが存在する場合

            'デュポン請求ファイルの物理削除
            ds = MyBase.CallDAC(Me._Dac, "DeleteInterFace", ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

            'デュポン請求GLマスタの取得件数を求める
            Dim glDr As DataRow() = ds.Tables("LMI030OUT_SELECT").Select(String.Concat( _
                                                                                      "NRS_BR_CD_GL = '", ds.Tables("LMI030IN").Rows(0).Item("NRS_BR_CD").ToString, "'" _
                                                                                      ) _
                                                                        )
            Dim jidoFlg As String = String.Empty
            Dim shudoFlg As String = String.Empty

            max = glDr.Length - 1
            For i As Integer = 0 To max
                'デュポン請求GLマスタの取得件数分処理を行う
                jidoFlg = glDr(i).Item("JIDO_FLAG").ToString
                shudoFlg = glDr(i).Item("SHUDO_FLAG").ToString

                rtnDs = New LMI030DS
                '値のクリア
                inTbl = setDs.Tables("LMI030OUT_SELECT")
                inTbl.Clear()
                inTbl.ImportRow(glDr(i))

                If ("01").Equals(jidoFlg) = True AndAlso _
                   ("01").Equals(shudoFlg) = True Then

                    'デュポン請求GLマスタの更新
                    rtnDs = Me.UpdateSekyGL(setDs)

                    'エラー判定
                    If MyBase.IsMessageExist() = True Then
                        MyBase.SetMessage("E011")
                        Return ds
                    End If

                ElseIf ("01").Equals(jidoFlg) = True AndAlso _
                       ("01").Equals(shudoFlg) = False Then

                    'デュポン請求GLマスタの物理削除
                    rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSekyGL", setDs)
                End If

            Next

        End If

        If hokanFlg = True Then
            '保管または全部の場合
            Me.MakeInterFaceData(ds, "01")
            'エラー判定
            If MyBase.IsMessageExist() = True Then
                'START YANAI 要望番号830
                'MyBase.SetMessage("E011")
                'END YANAI 要望番号830
                Return ds
            End If
        End If

        If niyakuFlg = True Then
            '荷役または全部の場合
            Me.MakeInterFaceData(ds, "02")
            'エラー判定
            If MyBase.IsMessageExist() = True Then
                'START YANAI 要望番号830
                'MyBase.SetMessage("E011")
                'END YANAI 要望番号830
                Return ds
            End If
        End If

        If unchinFlg = True Then
            '運賃または全部の場合
            Me.MakeInterFaceData(ds, "03")
            'エラー判定
            If MyBase.IsMessageExist() = True Then
                'START YANAI 要望番号830
                'MyBase.SetMessage("E011")
                'END YANAI 要望番号830
                Return ds
            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' デュポン請求GLマスタの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSekyGL(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        With ds.Tables("LMI030OUT_SELECT").Rows(0)

            '請求金額の再計算
            If Convert.ToDecimal(.Item("AMOUNT").ToString) < _
                Convert.ToDecimal(.Item("SOUND").ToString) + _
                Convert.ToDecimal(.Item("BOND").ToString) Then
                .Item("AMOUNT") = "0"
            Else
                .Item("AMOUNT") = Convert.ToString( _
                                                   Convert.ToDecimal(.Item("AMOUNT").ToString) - _
                                                   Convert.ToDecimal(.Item("SOUND").ToString) - _
                                                   Convert.ToDecimal(.Item("BOND").ToString) _
                                                  )
            End If

            '課税金額の再計算
            If Convert.ToDecimal(.Item("SOUND_GL").ToString) < _
                Convert.ToDecimal(.Item("SOUND").ToString) Then
                .Item("SOUND") = "0"
            Else
                .Item("SOUND") = Convert.ToString( _
                                                  Convert.ToDecimal(.Item("SOUND_GL").ToString) - _
                                                  Convert.ToDecimal(.Item("SOUND").ToString) _
                                                 )
            End If

            '非課税金額の再計算
            If Convert.ToDecimal(.Item("BOND_GL").ToString) < _
                Convert.ToDecimal(.Item("BOND").ToString) Then
                .Item("BOND") = "0"
            Else
                .Item("BOND") = Convert.ToString( _
                                                 Convert.ToDecimal(.Item("BOND_GL").ToString) - _
                                                 Convert.ToDecimal(.Item("BOND").ToString) _
                                                )
            End If

            '税額の再計算
            If Convert.ToDecimal(.Item("VAT_AMOUNT").ToString) < _
                Convert.ToDecimal(.Item("TAX").ToString) Then
                .Item("VAT_AMOUNT") = "0"
            Else
                .Item("VAT_AMOUNT") = Convert.ToString( _
                                                       Convert.ToDecimal(.Item("VAT_AMOUNT").ToString) - _
                                                       Convert.ToDecimal(.Item("TAX").ToString) _
                                                      )
            End If

        End With

        'デュポン請求GLマスタの更新
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdateSekyGL", ds)

        Return ds

    End Function

    ''' <summary>
    ''' デュポン請求ファイル、デュポン請求GLマスタの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeInterFaceData(ByVal ds As DataSet, _
                                       ByVal mode As String) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        'START YANAI 要望番号830
        Dim unchinDr() As DataRow = Nothing
        'END YANAI 要望番号830

        If ("01").Equals(mode) = True Then
            '保管の場合

            '保管料荷役明細印刷テーブルの検索
            ds = MyBase.CallDAC(Me._Dac, "SelectGMeisaiHOKAN", ds)

            'START YANAI 要望番号830
            If ds.Tables("LMI030OUT_SELECT").Rows.Count = 0 Then
                MyBase.SetMessage("E463")
                Return ds
            End If
            'END YANAI 要望番号830

        ElseIf ("02").Equals(mode) = True Then
            '荷役の場合

            '保管料荷役明細印刷テーブルの検索
            ds = MyBase.CallDAC(Me._Dac, "SelectGMeisaiNIYAKU", ds)

            'START YANAI 要望番号830
            If ds.Tables("LMI030OUT_SELECT").Rows.Count = 0 Then
                MyBase.SetMessage("E463")
                Return ds
            End If
            'END YANAI 要望番号830

        ElseIf ("03").Equals(mode) = True Then
            '運賃の場合

            '運賃テーブルの検索
            ds = MyBase.CallDAC(Me._Dac, "SelectFUnchin", ds)
            'START YANAI 要望番号830
            unchinDr = ds.Tables("LMI030OUT_SELECT").Select("SEIQ_FIXED_FLAG = '00'")
            If 0 < unchinDr.Length Then
                MyBase.SetMessage("E467")
                Return ds
            End If
            'END YANAI 要望番号830


        End If

        Dim max As Integer = ds.Tables("LMI030OUT_SELECT").Rows.Count
        Dim insertFlg As Boolean = True

        For i As Integer = 1 To max

            If insertFlg = True Then
                '値のクリア
                inTbl = setDs.Tables("LMI030OUT_SELECT")
                inTbl.Clear()
                inTbl.ImportRow(ds.Tables("LMI030OUT_SELECT").Rows(i - 1))
            End If

            If i < max Then
                '手動GROUP BY（まとめ処理）を行いながら、キーが変わった時だけ、DB更新処理を行うようにする
                If (inTbl.Rows(0).Item("NRS_BR_CD")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("NRS_BR_CD").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("CUST_CD_L")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("CUST_CD_L").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("CUST_CD_M")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("CUST_CD_M").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("CUST_CD_S")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("CUST_CD_S").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("CUST_CD_SS")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("CUST_CD_SS").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("DEPART")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("DEPART").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("SRC_CD")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("SRC_CD").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("FRB_CD")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("FRB_CD").ToString) = True AndAlso _
                   (inTbl.Rows(0).Item("TAX_KB")).Equals(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("TAX_KB").ToString) = True Then
                    'GROUP BY対象
                    inTbl.Rows(0).Item("SOUND") = Convert.ToDecimal( _
                                                                    Convert.ToDecimal(inTbl.Rows(0).Item("SOUND").ToString) + _
                                                                    Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("SOUND").ToString) _
                                                                   )

                    inTbl.Rows(0).Item("BOND") = Convert.ToDecimal( _
                                                                   Convert.ToDecimal(inTbl.Rows(0).Item("BOND").ToString) + _
                                                                   Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("BOND").ToString) _
                                                                  )
                    inTbl.Rows(0).Item("TAX") = Convert.ToDecimal( _
                                                                  Convert.ToDecimal(inTbl.Rows(0).Item("TAX").ToString) + _
                                                                  Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(i).Item("TAX").ToString) _
                                                                 )

                    insertFlg = False
                Else
                    insertFlg = True
                End If
            Else
                '一番最後のデータの場合
                insertFlg = True
            End If

            If insertFlg = True Then
                '最終的な行ができたら税の計算
                If inTbl.Rows(0).Item("TAX_KB").Equals("01") = True OrElse inTbl.Rows(0).Item("TAX_KB").Equals("04") = True Then
                    Dim sound As Decimal = Convert.ToDecimal(inTbl.Rows(0).Item("SOUND").ToString)
                    If sound <> 0 Then
                        '税率マスタの取得
                        Dim rows As DataRow() = ds.Tables("LMI030OUT_TAX").Select("ZEI_CD = '" & inTbl.Rows(0).Item("TAX_KB").ToString() & "'")

                        If 0 < rows.Count Then
                            Dim taxRate As Decimal = Convert.ToDecimal(rows(0).Item("TAX_RATE").ToString())
                            If inTbl.Rows(0).Item("TAX_KB").Equals("01") = True Then
                                '課税の場合
                                inTbl.Rows(0).Item("TAX") = Convert.ToString(Math.Round(sound * taxRate + 0.001, 0))
                            ElseIf inTbl.Rows(0).Item("TAX_KB").Equals("04") = True Then
                                '内税の場合
                                inTbl.Rows(0).Item("TAX") = Convert.ToString(Math.Floor(sound * taxRate / (taxRate + 1) + 0.001))
                            End If

                        End If
                    End If
                End If

                'どちらも０なら登録しない
                If (Convert.ToDecimal(inTbl.Rows(0).Item("SOUND").ToString()) = 0 _
                   AndAlso Convert.ToDecimal(inTbl.Rows(0).Item("BOND").ToString()) = 0) Then
                    Continue For
                End If

                'デュポン請求ファイルの作成を行う
                rtnDs = MyBase.CallDAC(Me._Dac, "InsertInterFace", setDs)
                'エラー判定
                If MyBase.IsMessageExist() = True Then
                    MyBase.SetMessage("E011")
                    Return ds
                End If

                'デュポン請求GLマスタの検索
                setDs = MyBase.CallDAC(Me._Dac, "SelectSekyGL", setDs)

                'デュポン請求GLマスタの更新
                setDs = Me.InsUpdSekyGL(setDs)
                'エラー判定
                If MyBase.IsMessageExist() = True Then
                    MyBase.SetMessage("E011")
                    Return ds
                End If
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' デュポン請求GLマスタの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsUpdSekyGL(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim dr As DataRow = Nothing

        If ds.Tables("LMI030OUT_SELECT_GL").Rows.Count = 0 Then
            '追加処理
            With ds.Tables("LMI030OUT_SELECT").Rows(0)

                dr = ds.Tables("LMI030OUT_SELECT_GL").NewRow()

                dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString
                dr.Item("SEKY_YM") = .Item("SEKY_YM").ToString
                dr.Item("DEPART") = .Item("DEPART").ToString
                dr.Item("SEKY_KMK") = .Item("SEKY_KMK").ToString
                dr.Item("FRB_CD") = .Item("FRB_CD").ToString
                dr.Item("SRC_CD") = .Item("SRC_CD").ToString
                dr.Item("COST_CENTER") = String.Empty
                dr.Item("MISK_CD") = String.Empty
                dr.Item("DEST_CTY") = String.Empty
                dr.Item("AMOUNT") = Convert.ToString( _
                                                     Convert.ToDecimal(.Item("SOUND").ToString) + _
                                                     Convert.ToDecimal(.Item("BOND").ToString) _
                                                    )
                dr.Item("VAT_AMOUNT") = .Item("TAX").ToString
                dr.Item("SOUND") = .Item("SOUND").ToString
                dr.Item("BOND") = .Item("BOND").ToString
                dr.Item("JIDO_FLAG") = "01"
                dr.Item("SHUDO_FLAG") = "00"

                'データセットに設定
                ds.Tables("LMI030OUT_SELECT_GL").Rows.Add(dr)

            End With

            'デュポン請求GLマスタの作成を行う
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSekyGL", ds)
            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

        Else
            '更新処理
            Dim max As Integer = ds.Tables("LMI030OUT_SELECT_GL").Rows.Count - 1

            For i As Integer = 0 To max
                With ds.Tables("LMI030OUT_SELECT_GL").Rows(i)

                    .Item("AMOUNT") = Convert.ToString( _
                                                       Convert.ToDecimal(.Item("AMOUNT").ToString) + _
                                                       Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(0).Item("SOUND").ToString) + _
                                                       Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(0).Item("BOND").ToString) _
                                                      )
                    .Item("VAT_AMOUNT") = Convert.ToString( _
                                                           Convert.ToDecimal(.Item("VAT_AMOUNT").ToString) + _
                                                           Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(0).Item("TAX").ToString) _
                                                          )
                    .Item("SOUND") = Convert.ToString( _
                                                      Convert.ToDecimal(.Item("SOUND").ToString) + _
                                                      Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(0).Item("SOUND").ToString) _
                                                     )
                    .Item("BOND") = Convert.ToString( _
                                                     Convert.ToDecimal(.Item("BOND").ToString) + _
                                                     Convert.ToDecimal(ds.Tables("LMI030OUT_SELECT").Rows(0).Item("BOND").ToString) _
                                                    )
                    .Item("JIDO_FLAG") = "01"

                End With
            Next

            'デュポン請求GLマスタの更新
            rtnDs = MyBase.CallDAC(Me._Dac, "UpdateSekyGL2", ds)
            'エラー判定
            If MyBase.IsMessageExist() = True Then
                MyBase.SetMessage("E011")
                Return ds
            End If

        End If

        Return ds

    End Function

    ''' <summary>
    ''' EXCEL出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectExcelメソッド呼出</remarks>
    Private Function SelectExcel(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectExcel", ds)

    End Function

    ''' <summary>
    ''' データ件数検索
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
            '0件でも表示するので、メッセージの設定はしない
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
    ''' <summary>
    ''' 荷主明細件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustDetails(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectCustDetails", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E431")
        End If

        Return ds

    End Function
    'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

#End Region

End Class
