' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF540BLC : 運賃試算重量別
'  作  成  者       :  菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF540BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF540BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "コンスト"
    'データセットテーブル名
    Private Const TABLE_NM_IN As String = "LMF540IN"
    Private Const TABLE_NM_OUT As String = "LMF540OUT"
    Private Const TABLE_NM_TARIFF As String = "M_UNCHIN_TARIFF"
    Private Const TABLE_NM_EXTC As String = "M_EXTC_UNCHIN_OUT"
    Private Const TABLE_NM_RPT As String = "M_RPT"

#End Region

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF540DAC = New LMF540DAC()

    Private _kyoriRow As Integer        '距離列格納変数

#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet


        '使用帳票ID取得
        ds = Me.SelectMPrt(ds)
        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '距離の特定の呼び出し
        ds = Me.getKyoriRow(ds)
        Return ds

    End Function
    ''' <summary>
    ''' 印刷実行
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrintJikou(ByVal ds As DataSet) As DataSet



        'レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables(TABLE_NM_RPT).Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet
            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables(TABLE_NM_OUT), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables(TABLE_NM_OUT), _
                                ds.Tables(LMConst.RD))

        Next


        Return ds
    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="prtId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal prtId As String, ByVal ds As DataSet) As DataSet

        Select Case prtId
            Case ""
            Case Else

        End Select

        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

#End Region

#Region "運賃距離等取得"
    ''' <summary>
    ''' 距離の特定
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（距離列取得エラー）</returns>
    ''' <remarks>[inDataTable]適用距離 / [運賃タリフマスタ]距離行を元に、距離対象列を取得</remarks>
    Private Function getKyoriRow(ByVal ds As DataSet) As DataSet


        Dim inDt As DataTable = ds.Tables(TABLE_NM_IN)

        '距離の取得へ
        ds = MyBase.CallDAC(Me._Dac, "GetTariffKyori", ds)


        Dim kyoriDt As DataTable = ds.Tables(TABLE_NM_TARIFF)

        '距離
        Dim inKyori As Double = Convert.ToDouble(inDt.Rows(0).Item("KYORI").ToString)
        Dim mstKyori As Double = 0

        Me._kyoriRow = -1                '格納変数初期化

        '属性の先頭から距離比較を行う
        For idx As Integer = 1 To 70

            '[マスタ設定]距離取得
            mstKyori = Convert.ToDouble(kyoriDt.Rows(0).Item(String.Concat("KYORI_", idx)).ToString)

            '距離Zeroは適用終了（対象タリフなし）
            If mstKyori = 0 Then
                MyBase.SetMessage("E296", New String() {"タリフマスタに指定された距離"})
                Return ds
                Exit For
            Else
                '距離判定
                If inKyori <= mstKyori Then
                    '処理継続
                    Me._kyoriRow = idx

                    Exit For
                Else
                    '距離確定
                    Me._kyoriRow = idx
                End If
            End If
        Next


        'インの情報に運賃タリフ名称を設定
        inDt.Rows(0).Item("UNCHIN_TARIFF_REM") = kyoriDt.Rows(0).Item("UNCHIN_TARIFF_REM").ToString

        '距離の確定ができたら金額&重量の取得へ
        ds = Me.getJuryouRow(ds)
        Return ds

    End Function

    ''' <summary>
    ''' 重量の特定
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（距離列取得エラー）</returns>
    ''' <remarks>[inDataTable]適用距離 / [運賃タリフマスタ]距離行を元に、距離対象列を取得</remarks>
    Private Function getJuryouRow(ByVal ds As DataSet) As DataSet

        '距離が特定
        Dim kyori As Integer = Me._kyoriRow


        Dim InDt As DataTable = ds.Tables(TABLE_NM_IN)
        Dim tariffdt As DataTable = ds.Tables(TABLE_NM_TARIFF)

        'DACクラスへ
        ds = MyBase.CallDAC(Me._Dac, "GetTariffJuryou", ds)

        Dim max As Integer = tariffdt.Rows.Count - 1
        Dim out As DataTable = ds.Tables(TABLE_NM_OUT)
        Dim dr As DataRow = out.NewRow()
        For i As Integer = 0 To max

            '運賃、重量の取得
            out.Rows.Add()

            out.Rows(i).Item("UNCHIN") = tariffdt.Rows(i).Item(String.Concat("KYORI_", kyori)).ToString
            out.Rows(i).Item("WT_LV") = tariffdt.Rows(i).Item("WT_LV").ToString


        Next

        '割増の区分の呼び出し
        ds = Me.GetWarimasi(ds)
        Return ds

    End Function

    ''' <summary>
    ''' 割増の区分を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetWarimasi(ByVal ds As DataSet) As DataSet

        'データセットINの情報の取得
        Dim InDt As DataTable = ds.Tables(TABLE_NM_IN)

        Dim jis As String = InDt.Rows(0).Item("DEST_JIS_CD").ToString
        Dim warimasi As String = InDt.Rows(0).Item("EXTC_TARIFF_CD").ToString

        '割増がnullだったら割増区分の処理をしない
        If String.IsNullOrEmpty(warimasi) = True Then
            '取得した内容をOUTの情報につめる
            ds = Me.DateOutSet(ds)
            Return ds
        End If
        '届け先jisがnullだったとき
        If String.IsNullOrEmpty(jis) = True Then

            InDt.Rows(0).Item("DEST_JIS_CD") = "0000000"

        End If
        'DACクラスへ
        ds = MyBase.CallDAC(Me._Dac, "GetWarimasi", ds)

        Dim Extc As DataTable = ds.Tables(TABLE_NM_EXTC)

        '1件以上の時
        If Extc.Rows.Count >= 1 Then
            'インの情報に割増運賃タリフ名称を設定
            InDt.Rows(0).Item("EXTC_TARIFF_REM") = Extc.Rows(0).Item("EXTC_TARIFF_REM").ToString
        End If
        '取得した内容をOUTの情報につめる
        ds = Me.DateOutSet(ds)
        Return ds
    End Function
    ''' <summary>
    ''' OUTの情報につめる
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DateOutSet(ByVal ds As DataSet) As DataSet
        Dim copy As DataSet = ds.Copy
        'DACクラスへ名称取得
        ds = MyBase.CallDAC(Me._Dac, "GetNm", ds)


        'それぞれのデータテーブルの宣言()
        Dim InDt As DataTable = copy.Tables(TABLE_NM_IN)
        'INの情報を取得
        Dim CustCdL As String = String.Empty
        Dim OrigJisCd As String = String.Empty
        Dim DestJisCd As String = String.Empty
        Dim Kyori As String = String.Empty
        Dim Kyoritei As String = String.Empty
        Dim TariffCd As String = String.Empty
        Dim ExtcTariff As String = String.Empty
        Dim StrDate As String = String.Empty
        Dim TariffNm As String = String.Empty
        Dim ExtcNm As String = String.Empty

        '1件以上の場合
        If InDt.Rows.Count >= 1 Then
            CustCdL = InDt.Rows(0).Item("CUST_CD_L").ToString
            OrigJisCd = InDt.Rows(0).Item("ORIG_JIS_CD").ToString
            DestJisCd = InDt.Rows(0).Item("DEST_JIS_CD").ToString

            '届け先住所が"0000000"のの時ブランクに設定
            If DestJisCd.Equals("0000000") = True Then

                DestJisCd = String.Empty
            End If
            Kyori = InDt.Rows(0).Item("KYORI").ToString
            Kyoritei = InDt.Rows(0).Item("KYORI_CD").ToString
            TariffCd = InDt.Rows(0).Item("UNCHIN_TARIFF_CD").ToString
            ExtcTariff = InDt.Rows(0).Item("EXTC_TARIFF_CD").ToString
            StrDate = InDt.Rows(0).Item("STR_DATE").ToString
            TariffNm = InDt.Rows(0).Item("UNCHIN_TARIFF_REM").ToString
            ExtcNm = InDt.Rows(0).Item("EXTC_TARIFF_REM").ToString

        End If


        Dim InDrNm As DataTable = ds.Tables(TABLE_NM_IN)

        Dim CustNmL As String = String.Empty
        Dim OrigJisNm As String = String.Empty
        Dim DestJisNm As String = String.Empty


        '名称の取得
        If InDrNm.Rows.Count >= 1 Then
            CustNmL = InDrNm.Rows(0).Item("CUST_NM_L").ToString
            OrigJisNm = InDrNm.Rows(0).Item("ORIG_JIS_NM").ToString
            DestJisNm = InDrNm.Rows(0).Item("DEST_JIS_NM").ToString
            'ExtcNm = InDrNm.Rows(0).Item("EXTC_TARIFF_REM").ToString

        End If
        Dim Extc As DataTable = ds.Tables(TABLE_NM_EXTC)
        '割増分の情報を取得
        Dim rely As String = String.Empty
        Dim frry As String = String.Empty
        Dim city As String = String.Empty
        Dim wint As String = String.Empty
        Dim post As String = String.Empty
        '割増の情報が１件以上だったら
        If Extc.Rows.Count >= 1 Then
            rely = Extc.Rows(0).Item("RELY_EXTC_NM").ToString

            '中継の区分値がダブル（中継料を２倍で計算）だった場合ダブルに変更
            If rely.Equals("ダブル（中継料を２倍で計算）") = True Then

                rely = "ダブル"

            End If

            frry = Extc.Rows(0).Item("FRRY_EXTC_NM").ToString
            city = Extc.Rows(0).Item("CITY_EXTC_NM").ToString
            wint = Extc.Rows(0).Item("WINT_EXTC_NM").ToString
            post = Extc.Rows(0).Item("POST_EXTC_NM").ToString
        End If

        Dim Out As DataTable = ds.Tables(TABLE_NM_OUT)


        Dim rpt As DataTable = ds.Tables(TABLE_NM_RPT)
        'レポートIDの取得
        Dim rptid As String = String.Empty
        '1件以上だったら
        If rpt.Rows.Count >= 1 Then
            rptid = rpt.Rows(0).Item("RPT_ID").ToString
        End If

        'OUTのデータ分まわす
        Dim max As Integer = Out.Rows.Count - 1
        With Out
            For i As Integer = 0 To max
                'OUTにデータセットをする
                'プリントID
                .Rows(i).Item("RPT_ID") = rptid
                ' 名称など
                .Rows(i).Item("CUST_NM_L") = CustNmL
                .Rows(i).Item("ORIG_JIS_NM") = OrigJisNm
                .Rows(i).Item("DEST_JIS_NM") = DestJisNm
                .Rows(i).Item("UNCHIN_TARIFF_REM") = TariffNm
                .Rows(i).Item("EXTC_TARIFF_REM") = ExtcNm

                '割増区分の取得
                .Rows(i).Item("RELY_EXTC_NM") = rely
                .Rows(i).Item("FRRY_EXTC_NM") = frry
                .Rows(i).Item("CITY_EXTC_NM") = city
                .Rows(i).Item("WINT_EXTC_NM") = wint
                .Rows(i).Item("POST_EXTC_NM") = post


                '入力されているもの
                .Rows(i).Item("ORIG_JIS_CD") = OrigJisCd
                .Rows(i).Item("DEST_JIS_CD") = DestJisCd
                .Rows(i).Item("KYORI") = Kyori
                .Rows(i).Item("KYORI_CD") = Kyoritei
                .Rows(i).Item("UNCHIN_TARIFF_CD") = TariffCd
                .Rows(i).Item("EXTC_TARIFF_CD") = ExtcTariff
                .Rows(i).Item("STR_DATE") = StrDate


                '要望番号:1556 KIM 2012/11/02 START
                If InDt.Rows(0).Item("CHANGE_FLG").Equals("1") = True Then
                    .Rows(i).Item("ORIG_JIS_CD") = DestJisCd
                    .Rows(i).Item("DEST_JIS_CD") = OrigJisCd
                    .Rows(i).Item("ORIG_JIS_NM") = DestJisNm
                    .Rows(i).Item("DEST_JIS_NM") = OrigJisNm
                End If
                '要望番号:1556 KIM 2012/11/02 START

            Next

        End With


        '印刷実行の呼び出し
        ds = Me.DoPrintJikou(ds)

        Return ds
    End Function

#End Region
#End Region

End Class
