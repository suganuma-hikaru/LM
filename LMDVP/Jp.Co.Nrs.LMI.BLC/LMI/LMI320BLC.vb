' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI320  : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI320BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI320BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI320DAC = New LMI320DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function DoMake(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMI320IN")
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMI320IN")
        Dim max As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '検索結果取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectData", setDs)

            'メッセージの判定
            If setDs.Tables("LMI320INS").Rows.Count < 1 Then
                MyBase.SetMessageStore("00", "E024", , inTbl.Rows(0).Item("ROW_NO").ToString())
                Continue For
            End If

            setDs = Me.EditDataSet(setDs)

            Call MyBase.CallDAC(Me._Dac, "DeleteData", setDs)

            Call MyBase.CallDAC(Me._Dac, "InsertData", setDs)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセット編集
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function EditDataSet(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow() = ds.Tables("LMI320INS").Select
        Dim wkSeiqtoCd As String = String.Empty
        Dim max As Integer = dr.Length - 1
        Dim strCnt As Integer = 0

        For i As Integer = 0 To max

            With dr(i)

                '保管条件
                strCnt = Trim(.Item("SEIQTO_NM").ToString()).Length - InStr(StrConv(Trim(.Item("SEIQTO_NM").ToString()), VbStrConv.Narrow), " ")
                .Item("H_JOKEN") = Trim(Right(.Item("SEIQTO_NM").ToString(), strCnt))


                '月末在庫(同じ請求先コードの場合は1レコード目以降に0を入れる)
                If .Item("SEIQTO_CD").ToString().Equals(wkSeiqtoCd) = False _
                 AndAlso String.IsNullOrEmpty(.Item("ZAN_QT").ToString()) = False Then

                    wkSeiqtoCd = .Item("SEIQTO_CD").ToString()

                Else

                    .Item("ZAN_QT") = 0

                End If

                '荷造り(請求先コード:0001027の場合のみ保管料の更新)
                Select Case .Item("SEIQTO_CD").ToString()
                    Case "0001000", "0001025", "0001026", "0001028",
                         "0001030", "0001035", "0001045",
                         "0001083"
                        If String.IsNullOrEmpty(.Item("GAICHU").ToString()) = False Then
                            .Item("NIZUKURI") = .Item("GAICHU")
                            .Item("GAICHU") = 0
                        End If

                    Case "0001027"
                        If String.IsNullOrEmpty(.Item("GAICHU").ToString()) = False Then
                            .Item("HOKAN") = .Item("GAICHU")
                            .Item("NIZUKURI") = 0
                            .Item("GAICHU") = 0
                        End If

                    Case Else
                        If String.IsNullOrEmpty(.Item("GAICHU").ToString()) = False Then
                            .Item("NIZUKURI") = 0
                        End If
                End Select

                If String.IsNullOrEmpty(.Item("NIZUKURI").ToString()) = True Then
                    .Item("GAICHU") = 0
                    .Item("NIZUKURI") = 0

                End If

                '荷役料　+ 保管料
                .Item("HN_TTL") = Convert.ToDecimal(.Item("NIYAKU")) + Convert.ToDecimal(.Item("HOKAN"))

                '税抜き合計
                .Item("TTL") = Convert.ToDecimal(.Item("HN_TTL")) + Convert.ToDecimal(.Item("NIZUKURI")) _
                               + Convert.ToDecimal(.Item("YOKOMOCHI")) + Convert.ToDecimal(.Item("GAICHU"))

                '消費税計算
                .Item("TAX_TTL") = Convert.ToDecimal(.Item("TTL")) * Convert.ToDecimal(.Item("TAX"))

                '総合計
                .Item("G_TTL") = Convert.ToDecimal(.Item("TTL")) + Convert.ToDecimal(.Item("TAX_TTL"))

            End With

        Next

        Return ds

    End Function

#End Region

End Class
