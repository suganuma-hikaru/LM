' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI410BLC : ビックケミー取込データ確認／報告
'  作  成  者       :  [umano]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports System.Text.RegularExpressions

''' <summary>
''' LMI410BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI410BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI410DAC = New LMI410DAC()

#End Region

#Region "Method"

#Region "BYK出荷報告対象データ取得処理"

    ''' <summary>
    ''' 報告対象データ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaHokokuメソッド呼出</remarks>
    Private Function OutkaHokoku(ByVal ds As DataSet) As DataSet

        Dim outkaNoL As String = String.Empty
        Dim preOutkaNoL As String = String.Empty

        '出荷報告対象データの取得
        Dim rtnDs As DataSet = Me.SelectOutkaHokoku(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("H_SENDOUTEDI_BYK").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E024")
            Return ds

        Else
            Dim rtnDt As DataTable = rtnDs.Tables("H_SENDINOUTEDI_BYK")
            For i As Integer = 0 To rtnDt.Rows.Count - 1

                If (rtnDt.Rows(i).Item("CUST_CD_M").ToString().Equals("02") OrElse _
                    rtnDt.Rows(i).Item("CUST_CD_M").ToString().Equals("03")) AndAlso _
                    String.IsNullOrEmpty(rtnDt.Rows(i).Item("E1EDKA1_PARVW_LIFNR_WE").ToString()) = True AndAlso _
                    rtnDt.Rows(i).Item("SAMPLE_HOUKOKU_FLG").ToString().Equals("0") = True Then
                    outkaNoL = rtnDt.Rows(i).Item("OUTKA_CTL_NO").ToString()

                    If String.IsNullOrEmpty(preOutkaNoL) = False AndAlso preOutkaNoL.Equals(outkaNoL) = False Then
                        MyBase.SetMessageStore("00", "E454", New String() {"送状番号未入力", "実績作成", String.Concat("出荷管理番号: ", outkaNoL)})
                    ElseIf String.IsNullOrEmpty(preOutkaNoL) = True AndAlso String.IsNullOrEmpty(outkaNoL) = False Then
                        MyBase.SetMessageStore("00", "E454", New String() {"送状番号未入力", "実績作成", String.Concat("出荷管理番号: ", outkaNoL)})
                    End If
                    preOutkaNoL = outkaNoL
                    outkaNoL = String.Empty
                End If

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectOutkaHokoku(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMI410IN")
        Dim outDt As DataTable = ds.Tables("H_SENDOUTEDI_BYK")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMI410IN")
        Dim setDt As DataTable = setDs.Tables("H_SENDOUTEDI_BYK")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectOutkaHokoku")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next


        Next

        Return ds

    End Function

    ''' <summary>
    ''' BYK出荷実績TBL(代理店用)追加処理（BYK出荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutBykAgtData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "InsertSendOutBykAgtData", ds)

    End Function

    ''' <summary>
    ''' 出荷データ(大)更新（BYK出荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateOutkaData", ds)

    End Function

    ''' <summary>
    ''' 出荷報告したレコードと対象のC_OUTKA_Mの数に差異が無いかを検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckSendOutBykAgtData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.calldac(Me._DAc, "CheckSendOutBykAgtData", ds)

    End Function

#End Region

#Region "BYK入荷報告対象データ取得処理"

    ''' <summary>
    ''' BYK入荷報告対象データ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaHokokuメソッド呼出</remarks>
    Private Function InkaHokoku(ByVal ds As DataSet) As DataSet

        Dim inkaNoL As String = String.Empty
        Dim preInkaNoL As String = String.Empty

        '入荷報告対象データの取得
        Dim rtnDs As DataSet = Me.SelectInkaHokoku(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("H_SENDINOUTEDI_BYK").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E024")
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' BYK入荷報告データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectInkaHokoku(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMI410IN")
        Dim outDt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMI410IN")
        Dim setDt As DataTable = setDs.Tables("H_SENDINOUTEDI_BYK")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectInkaHokoku")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BYK入荷実績TBL追加処理（BYK入荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSendInOutBykData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("H_SENDINOUTEDI_BYK")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        '20151026 tsunehira add
        '桁数調整
        Dim cnt As String = ""

        For i As Integer = 0 To max

            '18桁に設定
            setDs.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("E1BP2017_GM_ITEM_CREATE_MATERIAL") = Me.Convert18Digit(setDs.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("E1BP2017_GM_ITEM_CREATE_MATERIAL").ToString)
            setDs.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("E1BP2017_GM_ITEM_CREATE_MOVE_MAT") = Me.Convert18Digit(setDs.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("E1BP2017_GM_ITEM_CREATE_MOVE_MAT").ToString)

            '数字のみ・英字のみ如何に関わらず、10桁にする
            Dim str As String = setDs.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH").ToString
            If str.Length = 9 Then
                For j As Integer = 1 To 10 - str.Length
                    cnt = cnt + 0.ToString()
                Next
                Dim Result As String = cnt + str
                setDs.Tables("H_SENDINOUTEDI_BYK").Rows(0).Item("E1BP2017_GM_ITEM_CREATE_DELIV_NUMB_TO_SEARCH") = Result
            End If

            'DACクラス呼出
            MyBase.CallDAC(Me._Dac, "InsertSendInOutBykData", setDs)
        Next

        Return ds

    End Function

    ''' <summary>
    ''' EDI入荷データ(大)更新（BYK入荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaEdiLData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateInkaEdiLData", ds)

    End Function

    ''' <summary>
    ''' EDI入荷データ(中)更新（BYK入荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaEdiMData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateInkaEdiMData", ds)

    End Function

    ''' <summary>
    ''' BYK入荷EDI受信データ(明細)更新（BYK入荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaEdiDtlBykData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateInkaEdiDtlBykData", ds)

    End Function

    ''' <summary>
    ''' 入荷データ(大)更新（BYK入荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateInkaData", ds)

    End Function

#End Region

#Region "BYK移動報告対象データ取得処理"

    ''' <summary>
    ''' BYK移動報告対象データ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectIdoHokokuメソッド呼出</remarks>
    Private Function IdoHokoku(ByVal ds As DataSet) As DataSet

        '移動報告対象データの取得
        Dim rtnDs As DataSet = Me.SelectIdoHokoku(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("H_SENDINOUTEDI_BYK").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E024")
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' BYK移動報告データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectIdoHokokuメソッド呼出</remarks>
    Private Function SelectIdoHokoku(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMI410IN_IDO_HOKOKU")
        Dim outDt As DataTable = ds.Tables("H_SENDINOUTEDI_BYK")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMI410IN_IDO_HOKOKU")
        Dim setDt As DataTable = setDs.Tables("H_SENDINOUTEDI_BYK")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectIdoHokoku")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BYK移動EDI受信データ(明細)更新（BYK移動(入荷)報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateIdoEdiDtlBykData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateIdoEdiDtlBykData", ds)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' BYK取込データ検索処理(件数取得)
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
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    '''BYK取込データ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '商品マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        Return ds

    End Function

#End Region

#Region "取込"

    ''' <summary>
    ''' BYK入荷報告データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのInsertIdoEdiDataメソッド呼出</remarks>
    Private Function InsertIdoEdiData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("H_IDOEDI_DTL_BYK")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("H_IDOEDI_DTL_BYK")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '20151023 tsunehira add
            '日付のスラッシュを取る編集
            '日付を変数に格納、文字列を空にする
            Dim str As String = setDs.Tables("H_IDOEDI_DTL_BYK").Rows(0).Item("POSTING_DATE").ToString
            setDs.Tables("H_IDOEDI_DTL_BYK").Rows(0).Item("POSTING_DATE") = ""
            'スラッシュを取り除く
            Dim result As String = str.Replace("/", "")
            '先頭から8文字切り出して格納
            setDs.Tables("H_IDOEDI_DTL_BYK").Rows(0).Item("POSTING_DATE") = result.Substring(0, 8)


            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectCustGoodsZaiData")

            If setDs.Tables("GOODS_CUST").Rows.Count = 0 Then
                rtnResult = Me.ServerChkJudge(setDs, "SelectCustGoodsData")
            End If

            If setDs.Tables("GOODS_CUST").Rows.Count = 1 Then
                setDs.Tables("H_IDOEDI_DTL_BYK").Rows(0).Item("CUST_CD_L") = setDs.Tables("GOODS_CUST").Rows(0).Item("CUST_CD_L").ToString()
                setDs.Tables("H_IDOEDI_DTL_BYK").Rows(0).Item("CUST_CD_M") = setDs.Tables("GOODS_CUST").Rows(0).Item("CUST_CD_M").ToString()
            End If

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "InsertIdoEdiData")

            If rtnResult = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

#End Region

#Region "一括変更処理"

    ''' <summary>
    ''' BYK移動EDI受信データ(明細)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateIkkatuIdoData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMI410IN_IKKATU_HENKO")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMI410IN_IKKATU_HENKO")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'DACクラス呼出
            setDs = MyBase.CallDAC(Me._Dac, "UpdateIkkatuIdoData", setDs)

        Next

        Return ds

    End Function

#End Region

#Region "処理"

    ''' <summary>
    ''' 18桁処理の対応
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Convert18Digit(ByVal str As String) As String

        Dim cnt As String = String.Empty

        '2015/12/08 adachi
        'ED\+数字などはIsNumeric関数では数字(指数)と判定されるため
        '使用不可　コメントアウト
        'If IsNumeric(str) = True Then
        '    For i As Integer = 0 To (17 - str.Length)
        '        cnt = cnt + 0.ToString()
        '    Next
        '    Dim Result As String = cnt + str
        '    str = String.Empty
        '    str = Result
        'End If

        '2015/12/08 adachi
        '正規表現でのチェックに変更(数字のみ)
        Dim oMatch As Match = Regex.Match(str, "^[0-9]+$")
        If oMatch.Success = True Then
            '数字のみのため、0埋め
            For i As Integer = 0 To (17 - str.Length)
                cnt = cnt + 0.ToString()
            Next
            Dim Result As String = cnt + str
            str = String.Empty
            str = Result
        Else
            '文字を含むためそのまま
        End If


        Return str




    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#End Region

#Region "ユーティリティ"

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

#End Region

#End Region

End Class
