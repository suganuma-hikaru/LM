' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI180BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI180BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI180BLC = New LMI180BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 出荷データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectOutkaData(ByVal ds As DataSet) As DataSet

        '出荷データ取得
        ds = MyBase.CallBLC(Me._Blc, "SelectOutkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 取込データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function TorikomiData(ByVal ds As DataSet) As DataSet

        '取込データ取得
        Dim rtnDs As DataSet = MyBase.CallBLC(Me._Blc, "TorikomiData", ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' NRC回収データ取得処理(保存時の入力チェック処理)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function ShukkaCheckData(ByVal ds As DataSet) As DataSet

        'NRC回収データ取得
        ds = MyBase.CallBLC(Me._Blc, "ShukkaCheckData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(出荷の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function ShukkaData(ByVal ds As DataSet) As DataSet

        '枝番号の最大値を取得
        Dim rtnDs As DataSet = MyBase.CallBLC(Me._Blc, "SelectMaxEdaNo", ds)
        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Dim maxEdaNo As String = String.Empty
        If rtnDs.Tables("LMI180OUT").Rows.Count > 0 Then
            maxEdaNo = rtnDs.Tables("LMI180OUT").Rows(0).Item("EDA_NO").ToString
        Else
            maxEdaNo = "000"
        End If

        Dim serialNoFrom As Integer = -1
        Dim serialNoTo As Integer = -1
        Dim serialNoCnt As Integer = -1
        Dim serialNo As Integer = -1
        If String.IsNullOrEmpty(ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO_FROM").ToString) = False Then
            serialNoFrom = Convert.ToInt32(ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO_FROM").ToString) 'From値を設定
            serialNoTo = Convert.ToInt32(ds.Tables("LMI180IN").Rows(0).Item("SERIAL_NO_TO").ToString) 'To値を設定
            serialNoCnt = serialNoTo - serialNoFrom 'ToとFromの間の数を設定
            serialNo = serialNoFrom
        End If

        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '■シリアル№ From～Toの間のデータを保存
            For i As Integer = 0 To serialNoCnt
                '枝番号を＋１する
                maxEdaNo = MaeCoverData(Convert.ToString(Convert.ToInt32(maxEdaNo) + 1), "0", 3)
                If Convert.ToInt32(maxEdaNo) > 999 Then
                    MyBase.SetMessage("E506")
                    Return ds
                End If

                '値のクリア
                inTbl = setDs.Tables("LMI180IN")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMI180IN").Rows(0))
                inTbl.Rows(0).Item("EDA_NO") = maxEdaNo
                inTbl.Rows(0).Item("SERIAL_NO") = MaeCoverData(Convert.ToString(Convert.ToInt32(serialNo)), "0", 7)

                'BLCアクセス
                setDs = MyBase.CallBLC(Me._Blc, "InsertNrcShukkaData", setDs)

                'エラーの場合、終了
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

                'シリアル番号を＋１する
                serialNo = serialNo + 1

            Next


            '■スプレッドに設定されているシリアル№のデータを保存
            Dim max As Integer = ds.Tables("LMI180IN").Rows.Count - 1
            For i As Integer = 0 To max
                If String.IsNullOrEmpty(ds.Tables("LMI180IN").Rows(i).Item("SERIAL_NO").ToString) = True Then
                    Continue For
                End If

                '枝番号を＋１する
                maxEdaNo = MaeCoverData(Convert.ToString(Convert.ToInt32(maxEdaNo) + 1), "0", 3)
                If Convert.ToInt32(maxEdaNo) > 999 Then
                    MyBase.SetMessage("E506")
                    Return ds
                End If

                '値のクリア
                inTbl = setDs.Tables("LMI180IN")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMI180IN").Rows(i))
                inTbl.Rows(0).Item("EDA_NO") = maxEdaNo

                'BLCアクセス
                setDs = MyBase.CallBLC(Me._Blc, "InsertNrcShukkaData", setDs)

                'エラーの場合、終了
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            Next

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(回収の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function KaishuData(ByVal ds As DataSet) As DataSet


        Dim max As Integer = ds.Tables("LMI180IN").Rows.Count - 1
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim torokuKb As String = String.Empty
        Dim outkaNo As String = String.Empty
        Dim maxEdaNo As String = String.Empty
        Dim rtnDs As DataSet = Nothing

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To max

                '値のクリア
                inTbl = setDs.Tables("LMI180IN")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMI180IN").Rows(i))

                torokuKb = ds.Tables("LMI180IN").Rows(i).Item("TOROKU_KB").ToString
                Select Case torokuKb
                    Case "01"
                        '正常の場合

                        'BLCアクセス
                        setDs = MyBase.CallBLC(Me._Blc, "UpdateNrcKaishuData", setDs)

                        'エラーの場合、終了
                        If MyBase.IsMessageExist() = True Then
                            Return ds
                        End If

                    Case "02", "03"
                        '該当なし、重複の場合
                        outkaNo = ds.Tables("LMI180IN").Rows(i).Item("OUTKA_NO_L").ToString
                        If String.IsNullOrEmpty(outkaNo) = False Then

                            '枝番号の最大値を取得
                            rtnDs = MyBase.CallBLC(Me._Blc, "SelectMaxEdaNo", setDs)
                            'エラーの場合、終了
                            If MyBase.IsMessageExist() = True Then
                                Return ds
                            End If

                            If rtnDs.Tables("LMI180OUT").Rows.Count > 0 Then
                                maxEdaNo = rtnDs.Tables("LMI180OUT").Rows(0).Item("EDA_NO").ToString
                            Else
                                maxEdaNo = "000"
                            End If

                            '枝番号を＋１する
                            maxEdaNo = MaeCoverData(Convert.ToString(Convert.ToInt32(maxEdaNo) + 1), "0", 3)
                            inTbl.Rows(0).Item("EDA_NO") = maxEdaNo
                            If Convert.ToInt32(maxEdaNo) > 999 Then
                                MyBase.SetMessage("E506")
                                Return ds
                            End If

                        Else
                            inTbl.Rows(0).Item("OUTKA_NO_L") = "999999999"
                            inTbl.Rows(0).Item("EDA_NO") = "001"

                        End If

                        'BLCアクセス
                        setDs = MyBase.CallBLC(Me._Blc, "InsertNrcKaishuData", setDs)

                        'エラーの場合、終了
                        If MyBase.IsMessageExist() = True Then
                            Return ds
                        End If

                    Case "99"
                        'エラーの場合

                End Select

            Next

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(取消の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function TorikeshiData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = MyBase.CallBLC(Me._Blc, "UpdateNrcTorikeshiData", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

End Class