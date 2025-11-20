' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF050BLC : 運賃編集
'  作  成  者       :  菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMF050BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF050BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF050DAC = New LMF050DAC()

    ''' <summary>
    ''' データセットテーブル名(G_KAGAMI_HEDテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const G_HED As String = "G_KAGAMI_HED"

    ''' <summary>
    ''' データセットテーブル名(OUTテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF050OUT"

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' データセットテーブル名(G_HED_CHKテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End



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

        Return MyBase.CallDAC(Me._Dac, "UpdateUnchinKakuteiM", ds)

    End Function

    ''' <summary>
    ''' 運賃マスタ更新(確定解除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnchinKakuteiKijoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateUnchinKakuteiKijoM", ds)

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
        Dim dt As DataTable = ds.Tables(LMF050BLC.TABLE_NM_OUT)
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

            '運賃計算締め基準
            Haneti = dr.Item("UNTIN_CALCULATION_KB").ToString()

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

        Dim prmdt As DataTable = ds.Tables(LMF050BLC.G_HED)
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

                '要望番号:1045 terakawa 2013.03.28 Start
                '新黒存在チェック用データセット作成
                ds = Me.SetHedChkData(ds, shuka)

                '新黒存在チェック
                ds = MyBase.CallDAC(Me._Dac, "NewKuroExistChk", ds)
                If MyBase.GetResultCount() >= 1 Then

                    '請求期間内チェック
                    ds = MyBase.CallDAC(Me._Dac, "InSkyuDateChk", ds)
                    If MyBase.GetResultCount() >= 1 Then

                        '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd start
                        '新黒鑑情報の運賃チェック
                        ds = MyBase.CallDAC(Me._Dac, "IsExistNewKuroKagamiUnchin", ds)
                        If MyBase.GetResultCount().Equals(0) Then
                            Return ds
                        End If
                        'Return ds
                        '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd end
                    End If

                End If
                '要望番号:1045 terakawa 2013.03.28 End

                'メッセージのセット
                'START YANAI 要望番号607
                'MyBase.SetMessage("E232")
                '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd start
                MyBase.SetMessage("E885")
                'MyBase.SetMessage("E307")
                '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd end
                'END YANAI 要望番号607

                Return ds

            End If

        Else

            'START KIM 要望番号1484 2012/10/18

            ''最大の請求日と納入日を見比べる
            'If arr <= skyu Then
            '    'メッセージのセット
            '    'START YANAI 要望番号607
            '    'MyBase.SetMessage("E232")
            '    MyBase.SetMessage("E307")
            '    'END YANAI 要望番号607

            '    Return ds

            'End If

            '最大の請求日と納入日(※納入日が空白の場合、出荷日を使用する)を見比べる
            Dim chk As String = arr
            If String.IsNullOrEmpty(arr) = True Then
                chk = shuka
            End If
            If chk <= skyu Then
                '要望番号:1045 terakawa 2013.03.28 Start
                '新黒存在チェック用データセット作成
                ds = Me.SetHedChkData(ds, chk)

                '新黒存在チェック
                ds = MyBase.CallDAC(Me._Dac, "NewKuroExistChk", ds)
                If MyBase.GetResultCount() >= 1 Then

                    '請求期間内チェック
                    ds = MyBase.CallDAC(Me._Dac, "InSkyuDateChk", ds)
                    If MyBase.GetResultCount() >= 1 Then

                        '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd start
                        '新黒鑑情報の運賃チェック
                        ds = MyBase.CallDAC(Me._Dac, "IsExistNewKuroKagamiUnchin", ds)
                        If MyBase.GetResultCount().Equals(0) Then
                            Return ds
                        End If
                        'Return ds
                        '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd end
                    End If

                End If
                '要望番号:1045 terakawa 2013.03.28 End

                'メッセージのセット
                '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd start
                MyBase.SetMessage("E885")
                'MyBase.SetMessage("E307")
                '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen upd end
                Return ds

            End If

            'END KIM 要望番号1484 2012/10/18

        End If

        Return ds

    End Function

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 新黒存在チェック用データセット作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetHedChkData(ByVal ds As DataSet, ByVal skyuDate As String) As DataSet

        Dim dr As DataRow = ds.Tables(LMF050BLC.TABLE_NM_G_HED_CHK).NewRow
        dr.Item("NRS_BR_CD") = ds.Tables(LMF050BLC.TABLE_NM_OUT).Rows(0).Item("NRS_BR_CD")
        dr.Item("SEIQ_TARIFF_BUNRUI_KB") = ds.Tables(LMF050BLC.TABLE_NM_OUT).Rows(0).Item("SEIQ_TARIFF_BUNRUI_KB")
        dr.Item("SEIQTO_CD") = ds.Tables(LMF050BLC.TABLE_NM_OUT).Rows(0).Item("SEIQTO_CD")
        dr.Item("SKYU_DATE") = skyuDate

        ds.Tables(LMF050BLC.TABLE_NM_G_HED_CHK).Rows.Add(dr)

        Return ds
    End Function
    '要望番号:1045 terakawa 2013.03.28 End


#End Region

#End Region

End Class
