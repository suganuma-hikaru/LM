' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI560BLC : TSMC請求データ計算
'  作  成  者       :  [HORI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI560BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI560BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI560DAC = New LMI560DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理(件数取得)
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
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "前回計算取消処理"

    ''' <summary>
    ''' 前々回情報の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOldInfo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectOldInfo", ds)

    End Function

    ''' <summary>
    ''' 前回計算取消
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelCalc(ByVal ds As DataSet) As DataSet

        '荷主マスタを更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateCustCancel", ds)

        'TSMC在庫データを更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateZaiTsmcCancel", ds)

        'TSMC請求明細データテーブルから削除
        ds = MyBase.CallDAC(Me._Dac, "DeleteSekyMeisaiTsmc", ds)

        Return ds

    End Function

#End Region

#Region "実行処理"

    ''' <summary>
    ''' JOB番号の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectJobNo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectJobNo", ds)

    End Function

    ''' <summary>
    ''' 請求データ計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SeikyuCalc(ByVal ds As DataSet) As DataSet

        '請求対象データ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectCalcData", ds)

        'レコード番号
        Dim recNo As Integer = 0

        '請求対象データのループ
        For Each dr As DataRow In ds.Tables("LMI560OUT_CALC").Rows

            '入力チェック

            Dim lineNo As String = ds.Tables("LMI560IN_CALC").Rows(0).Item("LINE_NO").ToString()

            '入力チェック 商品マスタ
            If dr.Item("GOODS_CD_NRS").ToString() = "" Then
                Dim custCd As String = String.Concat(
                                          dr.Item("CUST_CD_L").ToString(), ", ", dr.Item("CUST_CD_M").ToString(), ", " _
                                        , dr.Item("CUST_CD_S").ToString(), ", ", dr.Item("CUST_CD_SS").ToString())
                Dim custGoodsCd As String = dr.Item("CUST_GOODS_CD").ToString()
                MyBase.SetMessageStore("00", "E493", New String() {String.Concat("商品コード", "(", custGoodsCd, ")"), "商品マスタ", ""}, lineNo, "荷主コードL・M・S・SS", custCd)
            Else
                '入力チェック 単価マスタ
                '(商品マスタ未登録であれば商品マスタを介した単価も当然紐付かないため、商品マスタ登録済みの場合のみのチェックとする)
                If dr.Item("SET_TANKA").ToString() = "" Then
                    Dim custCd As String = String.Concat(dr.Item("CUST_CD_L").ToString(), ", ", dr.Item("CUST_CD_M").ToString())
                    Dim upGpCd1 As String = dr.Item("UP_GP_CD_1").ToString()
                    MyBase.SetMessageStore("00", "E493", New String() {String.Concat("単価マスタコード", "(", upGpCd1, ")"), "単価マスタ", ""}, lineNo, "荷主コードL・M", custCd)
                End If
            End If

            If MyBase.IsMessageStoreExist Then
                'エラーチェック該当であれば請求計算は行わない。
                Continue For
            End If

            'レコード番号をカウントアップしてデータセットに書き戻す
            recNo += 1
            dr.Item("REC_NO") = recNo.ToString("D10")

            '請求金額を計算する
            With ""
                'セット料金日数を求める
                Dim setAmoDays As Double = 90
                If InStr(dr.Item("GRLVL1_PPNID").ToString, "@") = 0 Then
                    'ISOタンクなら45日間
                    setAmoDays = 45
                End If
                ' 上記で決定したセット料金日数(90 or 45) を I_SEKY_MEISAI_TSMC.UNIT_KB に設定する。
                ' (請求鑑の取込時の勘定科目判定のため)
                dr.Item("UNIT_KB") = setAmoDays.ToString()

                If dr.Item("SET_AMO_DAYS").ToString() <> "" AndAlso Convert.ToDecimal(dr.Item("SET_AMO_DAYS").ToString()) > 0 Then
                    ' 単価マスタよりのセット料金の期間に値ありの場合
                    ' 超過日数の計算には、上記で決定したセット料金日数(90 or 45)ではなく、単価マスタのセット料金の期間を用いる。
                    setAmoDays = Convert.ToDecimal(dr.Item("SET_AMO_DAYS").ToString())
                End If
                dr.Item("SET_CLC_DATE") = setAmoDays.ToString()

                '保管日数を求める（出荷日も含む）
                Dim inkaDate As DateTime = DateTime.ParseExact(dr.Item("INKA_DATE").ToString, "yyyyMMdd", Nothing)
                Dim outkaDate As DateTime = DateTime.ParseExact(dr.Item("OUTKA_PLAN_DATE").ToString, "yyyyMMdd", Nothing)
                Dim timeSpan As TimeSpan = outkaDate - inkaDate
                Dim storageDays As Double = Convert.ToDouble(timeSpan.Days) + 1

                'セット料金合計を求める
                Dim setTanka As Double = 0
                If String.IsNullOrEmpty(dr.Item("DEPLT_NO").ToString) Then
                    'ばらパレットNoが空値ならばセット単価を使用
                    If Not Double.TryParse(dr.Item("SET_TANKA").ToString, setTanka) Then
                        setTanka = 0
                    End If
                Else
                    'ばらパレットNoに値があればセット単価(デパレタイズ)を使用
                    If Not Double.TryParse(dr.Item("SET_TANKA_DEPLT").ToString, setTanka) Then
                        setTanka = 0
                    End If
                End If
                Dim setAmo As Double = setTanka

                '超過日数を求める
                Dim overDays As Double = storageDays - setAmoDays
                If overDays < 0 Then
                    overDays = 0
                End If

                '超過セット料金合計を求める
                Dim setOverAmo As Double = 0
                If overDays > 0 Then
                    Dim setOverTanka As Double = 0
                    If Not Double.TryParse(dr.Item("SET_OVER_TANKA").ToString, setOverTanka) Then
                        setOverTanka = 0
                    End If
                    setOverAmo = setOverTanka * overDays
                End If

                'セット料金合計金額を求める
                Dim setAmoTtl As Double = setAmo + setOverAmo

                '求めた金額をデータセットに書き戻す
                dr.Item("OVER_DATE") = overDays.ToString()
                dr.Item("SET_AMO") = setAmo.ToString()
                dr.Item("SET_OVER_AMO") = setOverAmo.ToString()
                dr.Item("SET_AMO_TTL") = setAmoTtl.ToString()
            End With

        Next

        If MyBase.IsMessageStoreExist Then
            'エラーチェック該当であれば更新は行わない。
            Return ds
        End If

        'TSMC請求明細データテーブルに登録
        ds = MyBase.CallDAC(Me._Dac, "InsertSekyMeisaiTsmc", ds)

        'TSMC在庫データを更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateZaiTsmc", ds)
        ds = MyBase.CallDAC(Me._Dac, "UpdateZaiTsmc2", ds)

        '荷主マスタを更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateCust", ds)

        'ナンバーマスタを更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateNumber", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
