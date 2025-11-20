' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI180BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI180BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI180DAC = New LMI180DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 出荷データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal ds As DataSet) As DataSet

        '出荷データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取込データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function TorikomiData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtnDs.Tables("LMI180OUT").NewRow()

        Dim max As Integer = ds.Tables("LMI180IN").Rows.Count - 1

        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        For i As Integer = 0 To max
            dr = rtnDs.Tables("LMI180OUT").NewRow()

            If ("99").Equals(ds.Tables("LMI180IN").Rows(i).Item("TOROKU_KB").ToString) = True Then
                '■①入力チェックでエラーの場合

                '取込データ取設定
                dr("NRC_REC_NO") = String.Empty
                dr("NRS_BR_CD") = ds.Tables("LMI180IN").Rows(i).Item("NRS_BR_CD").ToString
                dr("OUTKA_NO_L") = String.Empty
                dr("EDA_NO") = String.Empty
                dr("TOROKU_KB") = "99"
                dr("SERIAL_NO") = ds.Tables("LMI180IN").Rows(i).Item("SERIAL_NO").ToString
                dr("HOKOKU_DATE") = String.Empty

                'データセットに設定
                rtnDs.Tables("LMI180OUT").Rows.Add(dr)

                Continue For
            End If


            '入力チェックでエラーでない場合

            '値のクリア
            inTbl = setDs.Tables("LMI180IN")
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(ds.Tables("LMI180IN").Rows(i))

            '取込データ取得
            setDs = MyBase.CallDAC(Me._Dac, "TorikomiData", setDs)
            If setDs.Tables("LMI180OUT").Rows.Count = 0 Then
                '■②取込データが取得できなかった場合

                '取込データ取設定
                dr("NRC_REC_NO") = String.Empty
                dr("NRS_BR_CD") = setDs.Tables("LMI180IN").Rows(0).Item("NRS_BR_CD").ToString
                dr("OUTKA_NO_L") = String.Empty
                dr("EDA_NO") = String.Empty
                dr("TOROKU_KB") = String.Empty
                dr("SERIAL_NO") = setDs.Tables("LMI180IN").Rows(0).Item("SERIAL_NO").ToString
                dr("HOKOKU_DATE") = String.Empty

                'データセットに設定
                rtnDs.Tables("LMI180OUT").Rows.Add(dr)

            Else
                '■③取込データが取得できた場合

                '取込データ取設定
                dr("NRC_REC_NO") = setDs.Tables("LMI180OUT").Rows(0).Item("NRC_REC_NO").ToString
                dr("NRS_BR_CD") = setDs.Tables("LMI180OUT").Rows(0).Item("NRS_BR_CD").ToString
                dr("OUTKA_NO_L") = setDs.Tables("LMI180OUT").Rows(0).Item("OUTKA_NO_L").ToString
                dr("EDA_NO") = setDs.Tables("LMI180OUT").Rows(0).Item("EDA_NO").ToString
                dr("TOROKU_KB") = setDs.Tables("LMI180OUT").Rows(0).Item("TOROKU_KB").ToString
                dr("SERIAL_NO") = setDs.Tables("LMI180OUT").Rows(0).Item("SERIAL_NO").ToString
                dr("HOKOKU_DATE") = setDs.Tables("LMI180OUT").Rows(0).Item("HOKOKU_DATE").ToString

                'データセットに設定
                rtnDs.Tables("LMI180OUT").Rows.Add(dr)

            End If

        Next

        Return rtnDs

    End Function

    ''' <summary>
    ''' 枝番号の最大値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMaxEdaNo(ByVal ds As DataSet) As DataSet

        '枝番号の最大値を取得
        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectMaxEdaNo", ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' NRC回収データ取得処理(保存時の入力チェック処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ShukkaCheckData(ByVal ds As DataSet) As DataSet

        'NRC回収データ取得
        ds = MyBase.CallDAC(Me._Dac, "ShukkaCheckData", ds)
        If ds.Tables("LMI180OUT").Rows.Count > 0 Then
            Return ds
        End If

        If String.IsNullOrEmpty(ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO").ToString) = True Then
            Return ds
        End If
        'LMI180INの中でMINのSERIALNOとMAXのSERIALNOを求め、ROWS(0)に設定する
        Dim minSerialNo As String = ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO").ToString
        Dim maxSerialNo As String = ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO").ToString
        Dim max As Integer = ds.Tables("LMI180IN").Rows.Count - 1
        For i As Integer = 0 To max
            '最小シリアル№
            If minSerialNo > ds.Tables("LMI180IN").Rows(i).Item("SERIAL_NO").ToString Then
                minSerialNo = ds.Tables("LMI180IN").Rows(i).Item("SERIAL_NO").ToString
            End If

            '最大シリアル№
            If maxSerialNo < ds.Tables("LMI180IN").Rows(i).Item("SERIAL_NO").ToString Then
                maxSerialNo = ds.Tables("LMI180IN").Rows(i).Item("SERIAL_NO").ToString
            End If
        Next
        ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO_FROM") = minSerialNo
        ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO_TO") = maxSerialNo
        'NRC回収データ取得
        ds = MyBase.CallDAC(Me._Dac, "ShukkaCheckData", ds)
        If ds.Tables("LMI180OUT").Rows.Count > 0 Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(出荷の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertNrcShukkaData(ByVal ds As DataSet) As DataSet

        'NRCレコード番号採番
        Dim nrcRecNo As String = Me.GetNrcRecNo(ds)
        ds.Tables("LMI180IN").Rows(0).Item("NRC_REC_NO") = nrcRecNo

        '保存処理(出荷の場合)
        ds = MyBase.CallDAC(Me._Dac, "InsertNrcShukkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(回収の追加の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertNrcKaishuData(ByVal ds As DataSet) As DataSet

        'NRCレコード番号採番
        Dim nrcRecNo As String = Me.GetNrcRecNo(ds)
        ds.Tables("LMI180IN").Rows(0).Item("NRC_REC_NO") = nrcRecNo

        '保存処理(回収の場合)
        ds = MyBase.CallDAC(Me._Dac, "InsertNrcKaishuData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(回収の更新の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateNrcKaishuData(ByVal ds As DataSet) As DataSet

        '保存処理(回収の場合)
        ds = MyBase.CallDAC(Me._Dac, "UpdateNrcKaishuData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(取消の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateNrcTorikeshiData(ByVal ds As DataSet) As DataSet

        '保存処理(取消の場合)
        ds = MyBase.CallDAC(Me._Dac, "UpdateNrcTorikeshiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' NRC_RE_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetNrcRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.NRC_REC_NO, Me, ds.Tables("LMI180IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

#End Region

End Class
