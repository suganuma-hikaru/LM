' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI470  : 日本合成　物流費送信
'  作  成  者       :  [DAIKOKU]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports System.Text
Imports System.IO

''' <summary>
''' LMI470BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI470BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    '''' <summary>
    '''' 使用するDACクラスの生成
    '''' </summary>
    '''' <remarks></remarks>
    '''' 
    Private _Dac As LMI470DAC = New LMI470DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 物流費チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function Butsuryuhi_Rtn(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing


        'LMI470OUTテーブル
        Dim LMI470OUT As DataTable = ds.Tables("LMI470OUT")
        Dim LMI470OUTRow As DataRow

        Dim i As Integer = 0

        '物流費チェック
        rtnDs = MyBase.CallDAC(Me._Dac, "Butsuryuhi_Rtn1", ds)
        If rtnDs.Tables("LMI470OUTWK").Rows.Count > 0 Then

            For i = 0 To rtnDs.Tables("LMI470OUTWK").Rows.Count - 1
                LMI470OUTRow = LMI470OUT.NewRow

                LMI470OUTRow("NET_AMT") = rtnDs.Tables("LMI470OUTWK").Rows(i).Item("NET_AMT").ToString

                '行追加
                LMI470OUT.Rows.Add(LMI470OUTRow)

            Next

        End If


        rtnDs = MyBase.CallDAC(Me._Dac, "Butsuryuhi_Rtn2", ds)
        If rtnDs.Tables("LMI470OUTWK").Rows.Count > 0 Then

            For i = 0 To rtnDs.Tables("LMI470OUTWK").Rows.Count - 1
                LMI470OUTRow = LMI470OUT.NewRow

                LMI470OUTRow("NET_AMT") = rtnDs.Tables("LMI470OUTWK").Rows(i).Item("NET_AMT").ToString

                '行追加
                LMI470OUT.Rows.Add(LMI470OUTRow)

            Next

        End If

#If False Then      'DEL 2018/07/04 運賃　鑑追加分がダブって処理されているので削除
        rtnDs = MyBase.CallDAC(Me._Dac, "Butsuryuhi_Rtn3", ds)
        If rtnDs.Tables("LMI470OUTWK").Rows.Count > 0 Then

            For i = 0 To rtnDs.Tables("LMI470OUTWK").Rows.Count - 1
                LMI470OUTRow = LMI470OUT.NewRow

                LMI470OUTRow("NET_AMT") = rtnDs.Tables("LMI470OUTWK").Rows(i).Item("NET_AMT").ToString

                '行追加
                LMI470OUT.Rows.Add(LMI470OUTRow)

            Next

        End If

#End If

        rtnDs = MyBase.CallDAC(Me._Dac, "Butsuryuhi_Rtn4", ds)
        If rtnDs.Tables("LMI470OUTWK").Rows.Count > 0 Then

            For i = 0 To rtnDs.Tables("LMI470OUTWK").Rows.Count - 1
                LMI470OUTRow = LMI470OUT.NewRow

                LMI470OUTRow("NET_AMT") = rtnDs.Tables("LMI470OUTWK").Rows(i).Item("NET_AMT").ToString

                '行追加
                LMI470OUT.Rows.Add(LMI470OUTRow)

            Next

        End If

        rtnDs = MyBase.CallDAC(Me._Dac, "Butsuryuhi_Rtn5", ds)
        If rtnDs.Tables("LMI470OUTWK").Rows.Count > 0 Then

            For i = 0 To rtnDs.Tables("LMI470OUTWK").Rows.Count - 1
                LMI470OUTRow = LMI470OUT.NewRow

                LMI470OUTRow("NET_AMT") = rtnDs.Tables("LMI470OUTWK").Rows(i).Item("NET_AMT").ToString

                '行追加
                LMI470OUT.Rows.Add(LMI470OUTRow)

            Next

        End If

        rtnDs = MyBase.CallDAC(Me._Dac, "Butsuryuhi_Rtn6", ds)
        If rtnDs.Tables("LMI470OUTWK").Rows.Count > 0 Then

            For i = 0 To rtnDs.Tables("LMI470OUTWK").Rows.Count - 1
                LMI470OUTRow = LMI470OUT.NewRow

                LMI470OUTRow("NET_AMT") = rtnDs.Tables("LMI470OUTWK").Rows(i).Item("NET_AMT").ToString

                '行追加
                LMI470OUT.Rows.Add(LMI470OUTRow)

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 物流費データ作成・設定（1データ575バイト）
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeData(ByVal prmDs As DataSet)

        Dim setDs As DataSet = prmDs.Copy()

        'データ出力処理
        Dim setData As StringBuilder = New StringBuilder()
        Dim setOutDt As DataTable = setDs.Tables("LMI470OUT")

        For i As Integer = 0 To setOutDt.Rows.Count - 1
            With setOutDt.Rows(i)


                '内部コードをShiftJISのバイト配列にする    --スペースを付加し575バイトを取り出す
                Dim byteSJIS As Byte() = Encoding.GetEncoding("Shift_JIS").GetBytes(String.Concat(.Item("NET_AMT").ToString(), Space(100)))

                'ShiftJIS配列の指定範囲を文字列に変換（先頭から575バイト分抜き取る)
                'setData.Append(Encoding.Default.GetString(byteSJIS, 1, 575))
                setData.Append(Encoding.Default.GetString(byteSJIS, 0, 575))        'UPD 2018/08/03

                ''575バイトで設定する(文字があるため575バイトにならず)
                'If Len(.Item("NET_AMT").ToString) < 575 Then

                '    setData.Append(String.Concat(.Item("NET_AMT").ToString(), Space(575 - Len(.Item("NET_AMT").ToString))))
                'Else
                '    setData.Append(.Item("NET_AMT").ToString())
                'End If

                setData.Append(vbNewLine)
            End With
        Next

        '保存先のファイルのパス
        Dim filePath As String = String.Concat(prmDs.Tables("LMI470IN").Rows(0).Item("FOLDER_NM").ToString, _
                                              prmDs.Tables("LMI470IN").Rows(0).Item("FILE_NM").ToString)

        'ファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMI470IN").Rows(0).Item("FOLDER_NM").ToString)
        Dim sr As StreamWriter = New StreamWriter(filePath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub


#End Region

End Class
