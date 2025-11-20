' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060BLC : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN060DAC = New LMN060DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''  検索処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDetail(ByVal ds As DataSet) As DataSet

        '列ヘッダ設定内容取得
        ds = MyBase.CallDAC(Me._Dac, "SelectColumnHdr", ds)

        '******************↓↓ Detail検索用に検索条件格納テーブルを編集する ↓↓******************

        '使用テーブル定義
        Dim dtIn As DataTable = ds.Tables("LMN060IN")
        Dim dtHdr As DataTable = ds.Tables("LMN060OUT_HDR")
        Dim dtOut As DataTable = ds.Tables("LMN060OUT")

        '検索結果を検索条件格納用テーブルに格納
        Dim max As Integer = dtHdr.Rows.Count - 1
        Dim drHdr As DataRow = Nothing
        For i As Integer = 0 To max
            drHdr = dtHdr.Rows(i)
            If i <> 0 Then
                dtIn.ImportRow(dtIn.Rows(0))
            End If
            dtIn.Rows(i).Item("SOKO_CD") = drHdr.Item("SOKO_CD")
            dtIn.Rows(i).Item("BR_CD") = drHdr.Item("BR_CD")

        Next

        '******************↑↑ Detail検索用に検索条件格納テーブルを編集する ↑↑******************

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables("LMN060IN")
        Dim selectOutDt As DataTable = selectDs.Tables("LMN060OUT")
        Dim maxOut As Integer = 0
        Dim setZaiko As String = String.Empty
        For i As Integer = 0 To max

            '検索条件格納用テーブル初期化・設定
            selectDt.Rows.Clear()
            selectDt.ImportRow(dtIn.Rows(i))

            'Detail表示用データ取得
            selectDs = MyBase.CallDAC(Me._Dac, "SelectDetail", selectDs)

            maxOut = selectOutDt.Rows.Count - 1

            For j As Integer = 0 To maxOut

                '各倉庫ごとのレコードを合わせる
                dtOut.ImportRow(selectOutDt.Rows(j))

            Next

        Next

        Dim addRow As DataRow = Nothing
        max = dtOut.Rows.Count - 1
        For i As Integer = 0 To max

            '在庫データ取得用にテーブル編集
            selectDt.Rows.Clear()
            addRow = selectDt.NewRow()
            addRow.Item("NRS_GOODS_CD") = dtOut.Rows(i).Item("NRS_GOODS_CD")
            addRow.Item("SOKO_CD") = dtOut.Rows(i).Item("SOKO_CD")
            addRow.Item("BR_CD") = dtOut.Rows(i).Item("BR_CD")
            addRow.Item("IKO_FLG") = dtOut.Rows(i).Item("IKO_FLG")
            selectDt.Rows.Add(addRow)

            '在庫データの検索を行う
            selectDs = MyBase.CallDAC(Me._Dac, Me.GetZaikoDB(selectDt), selectDs)

            '取得した結果を明細表示用データテーブルにあわせる
            setZaiko = selectDs.Tables("LMN060OUT_ZAIKO").Rows(0).Item("ZAIKO_NB").ToString()
            If String.IsNullOrEmpty(setZaiko) Then
                setZaiko = "0"
            End If
            dtOut.Rows(i).Item("ZAIKO_NB") = setZaiko

        Next

        Return ds

    End Function

    ''' <summary>
    '''  在庫データテーブル接続先
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetZaikoDB(ByVal dt As DataTable) As String

        '移行済みか否かで参照先が異なる
        Dim dacNm As String = String.Empty
        Select Case dt.Rows(0).Item("IKO_FLG").ToString()
            Case "00" '未移行

                'LMSVer1参照 DAC呼び出しメソッド名
                dacNm = "SelectZaikoDataSver1"

            Case "01" '移行済

                'LMSVer2参照 DAC呼び出しメソッド名
                dacNm = "SelectZaikoDataSver2"

        End Select

        Return dacNm

    End Function

#End Region

#Region "在庫日数"

    ''' <summary>
    ''' 在庫日数算出、更新処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdZaikoNissu(ByVal ds As DataSet) As DataSet

        '明細データテーブル名取得
        Dim DtName As String = "LMN060OUT"
        Dim Dt As DataTable = ds.Tables(DtName)
        Dim dtNum As Integer = Dt.Rows.Count
        '明細データ1件毎に処理
        For i As Integer = 0 To dtNum - 1

            '格納用データセット
            Dim inDs As DataSet = New LMN060DS()
            Dim inDt As DataTable = inDs.Tables(DtName)

            '明細データ設定
            inDt.ImportRow(Dt.Rows(i))

            '月間出荷重量取得
            inDs = MyBase.CallDAC(Me._Dac, Me.GetMonthOutkaNbDB(inDt), inDs)

            '在庫日数算出用変数
            Dim constDay As Decimal = 30
            Dim monthOutkaNb As Decimal = 0
            Dim zaikoNb As Decimal = Convert.ToDecimal(Dt.Rows(i).Item("ZAIKO_NB"))
            Dim zaikoNissu As Decimal = 0
            '月間出荷重量チェック
            If (Not String.IsNullOrEmpty(inDt.Rows(0).Item("MONTH_OUTKA_NB").ToString())) Then
                monthOutkaNb = Convert.ToDecimal(inDt.Rows(0).Item("MONTH_OUTKA_NB").ToString())
            End If
            '在庫日数算出
            If monthOutkaNb <> 0 Then
                zaikoNissu = Decimal.Floor((constDay * zaikoNb) / monthOutkaNb)
            End If

            '取得した在庫日数をリターンデータセットに設定
            Dt.Rows(i).Item("ZAIKO_NISSU") = zaikoNissu.ToString()

        Next

        '在庫日数テーブル更新処理
        ds = MyBase.CallDAC(Me._Dac, "UpdateN_ZAIKO_NISSU", ds)

        Return ds

    End Function

    ''' <summary>
    '''  出荷データテーブル接続先
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetMonthOutkaNbDB(ByVal dt As DataTable) As String

        '移行済みか否かで参照先が異なる
        Dim dacNm As String = String.Empty
        Select Case dt.Rows(0).Item("IKO_FLG").ToString()
            Case "00" '未移行

                'LMSVer1参照 DAC呼び出しメソッド名
                dacNm = "GetMonthOutkaNbSver1"

            Case "01" '移行済

                'LMSVer2参照 DAC呼び出しメソッド名
                dacNm = "GetMonthOutkaNbSver2"

        End Select

        Return dacNm

    End Function

#End Region

#End Region

End Class
