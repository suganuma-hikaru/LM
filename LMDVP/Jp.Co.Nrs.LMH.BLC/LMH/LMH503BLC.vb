' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブシステム
'  プログラムID     :  LMH503BLC : 現品票印刷
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH503BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH503BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH503DAC = New LMH503DAC()

#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Const TblNmIn As String = "PRINT_DATA_IN"
        Const TblNmOut As String = "GENPIN"
        Const TblNmRpt As String = "M_RPT"
        Const TblNmGenpinPrt As String = "H_GENPIN_PRT_FJF"

        '元のデータ
        Dim dtIn As DataTable = ds.Tables(TblNmIn)
        Dim dtOut As DataTable = ds.Tables(TblNmOut)
        Dim dtRpt As DataTable = ds.Tables(TblNmRpt)
        Dim dtGenpinPrt As DataTable = ds.Tables(TblNmGenpinPrt)

        '別インスタンス
        Dim dsWorkInOut As DataSet = ds.Clone()
        Dim dtWorkGenpinPrt As DataTable = dsWorkInOut.Tables(TblNmGenpinPrt)

        'ワーク用RPTテーブル
        Dim dtRptWork As DataTable = dtRpt.Clone()

        'IN条件0件チェック
        If dtIn.Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")   '印刷条件が設定されていません。
            Return ds
        End If

        '処理区分（未発行／再発行）の取得
        Dim shoriKb As String = dtIn.Rows(0).Item("SHORI_KB").ToString

        '----------------------------
        ' 現品票印刷ステータスの確認
        '----------------------------
        If shoriKb = "未発行" Then
            '処理区分＝未発行の場合、印刷済データが含まれていないか確認
            For Each drIn As DataRow In dtIn.Rows
                '値のクリア
                dsWorkInOut.Clear()

                '条件の設定
                dsWorkInOut.Tables(TblNmIn).ImportRow(drIn)

                '現品票印刷ステータス取得
                dsWorkInOut = MyBase.CallDAC(Me._Dac, "SelectGenpinPrt", dsWorkInOut)

                '取得件数判定
                If dtWorkGenpinPrt.Rows.Count >= 1 Then
                    'E454:[%1]の為、[%2]できません。[%3]
                    Dim variableColvalue As String = dtWorkGenpinPrt.Rows(0).Item("EDI_CTL_NO").ToString & " / " & dtWorkGenpinPrt.Rows(0).Item("OUTKA_FROM_ORD_NO").ToString
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, _
                                           "E454", _
                                           {"印刷済", "印刷", "「現品票再印刷」を実行して下さい。"}, _
                                           drIn.Item("SPREAD_ROW_NO").ToString, _
                                           "EDI管理番号 / オーダー番号", _
                                           variableColvalue)
                End If
            Next

            If MyBase.IsMessageStoreExist Then
                Return ds
            End If
        End If

        '----------------------
        ' 印刷対象データの取得
        '----------------------
        '画面で選択された明細ごとに印刷データを取得
        For Each drIn As DataRow In dtIn.Rows
            '値のクリア
            dsWorkInOut.Clear()

            '条件の設定
            dsWorkInOut.Tables(TblNmIn).ImportRow(drIn)

            '使用帳票ID取得
            dsWorkInOut = MyBase.CallDAC(Me._Dac, "SelectMPrt", dsWorkInOut)

            '取得件数判定
            If dsWorkInOut.Tables(TblNmRpt).Rows.Count = 0 Then
                MyBase.SetMessage("E078", {"帳票パターンマスタ"})    '[%1]に該当データが存在しません。
                Return ds
            End If

            '検索結果取得
            dsWorkInOut = MyBase.CallDAC(Me._Dac, "SelectPrintData", dsWorkInOut)

            '取得件数判定
            If dsWorkInOut.Tables("SELECT_PRINT_DATA_OUT").Rows.Count = 0 Then
                MyBase.SetMessage("E011")   '対象データは他のユーザーによって変更されています。再度、検索してください。
                Return ds
            End If

            '印刷データの作成
            dsWorkInOut = EditPrintDataSet(dsWorkInOut)

            '検索結果を詰め替え
            dtOut.Merge(dsWorkInOut.Tables(TblNmOut))
            dtRptWork.Merge(dsWorkInOut.Tables(TblNmRpt))
            dtGenpinPrt.Merge(dtWorkGenpinPrt)
        Next

        '取得件数判定
        If dtOut.Rows.Count = 0 Then
            MyBase.SetMessage("E070")   '印刷対象データがありませんでした。
            Return ds
        End If

        '----------------------
        ' レポートIDの重複削除
        '----------------------
        'ワーク用RPTワークテーブルの重複を除外する
        Dim viewRpt As DataView = New DataView(dtRptWork)
        dtRptWork = viewRpt.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        'ソート実行
        Dim drsRptWork As DataRow() = dtRptWork.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")
        'ソート実行後データ格納データセット作成
        Dim dtRptSort As DataTable = dtRpt.Clone()
        'ソート済みデータ格納
        dtRptSort.Merge(drsRptWork.CopyToDataTable)

        'キーブレイク用(NRS_BR_CD,PTN_ID,RPT_ID)
        Dim keyBrCd As String = String.Empty
        Dim keyPtnId As String = String.Empty
        Dim keyRptId As String = String.Empty

        '重複分を除外したワーク用RPTテーブルを帳票出力に使用するDSにセットする
        For Each drRptSort As DataRow In dtRptSort.Rows
            '営業所コード、パターンID、レポートIDが一致するレコードは除外する
            If keyBrCd <> drRptSort.Item("NRS_BR_CD").ToString() OrElse _
               keyPtnId <> drRptSort.Item("PTN_ID").ToString() OrElse _
               keyRptId <> drRptSort.Item("RPT_ID").ToString() Then

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(drRptSort)
                'キー更新
                keyBrCd = drRptSort.Item("NRS_BR_CD").ToString()
                keyPtnId = drRptSort.Item("PTN_ID").ToString()
                keyRptId = drRptSort.Item("RPT_ID").ToString()

            End If
        Next

        '-------------
        ' 帳票CSV出力
        '-------------
        'レポートID分繰り返す
        For Each drRpt As DataRow In dtRpt.Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(drRpt.Item("RPT_ID").ToString()) Then
                Continue For
            End If

            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            '指定したレポートIDのデータを抽出する。
            Dim dsPrt As DataSet = comPrt.CallDataSet(dtOut, drRpt.Item("RPT_ID").ToString())
            '帳票CSV出力
            comPrt.StartPrint(Me, drRpt.Item("NRS_BR_CD").ToString(), _
                                drRpt.Item("PTN_ID").ToString(), _
                                drRpt.Item("PTN_CD").ToString(), _
                                drRpt.Item("RPT_ID").ToString(), _
                                dsPrt.Tables(TblNmOut), _
                                ds.Tables(LMConst.RD))
        Next

        '--------------------------------
        ' 現品票印刷ステータス登録・更新
        '--------------------------------
        'テーブルの重複を除外する
        Dim viewGenpinPrt As DataView = New DataView(dtGenpinPrt)
        Dim dtGenpinPrtDistinct As DataTable = viewGenpinPrt.ToTable(True, "EDI_CTL_NO", "OUTKA_FROM_ORD_NO")

        For Each drOutWork As DataRow In dtGenpinPrtDistinct.Rows
            '値のクリア
            dsWorkInOut.Clear()

            '条件の設定
            dsWorkInOut.Tables(TblNmIn).ImportRow(dtIn.Rows(0))

            Dim drGenpinPrt As DataRow = dtWorkGenpinPrt.NewRow
            drGenpinPrt.ItemArray = drOutWork.ItemArray
            dtWorkGenpinPrt.Rows.Add(drGenpinPrt)

            '更新件数を初期化
            Dim updRecCnt As Integer = 0

            If shoriKb = "再発行" Then
                '現品票印刷ステータス更新
                dsWorkInOut = MyBase.CallDAC(Me._Dac, "UpdateGenpinPrt", dsWorkInOut)

                '更新件数を取得
                updRecCnt = MyBase.GetResultCount()
            End If

            '取得件数判定
            If updRecCnt <= 0 Then
                '現品票印刷ステータス登録
                dsWorkInOut = MyBase.CallDAC(Me._Dac, "InsertGenpinPrt", dsWorkInOut)
            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データの作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal ds As DataSet) As DataSet

        Dim inRow As DataRow = ds.Tables("PRINT_DATA_IN").Rows(0)

        Dim genpinTbl As DataTable = ds.Tables("GENPIN")
        Dim genpinPrtTbl As DataTable = ds.Tables("H_GENPIN_PRT_FJF")

        'レコード数分繰り返し
        For Each outRow As DataRow In ds.Tables("SELECT_PRINT_DATA_OUT").Rows

            '枚数計算
            Dim maisu As Decimal
            Try
                maisu = CDec(outRow.Item("ZFVYBRGEW").ToString) / CDec(outRow.Item("STD_IRIME").ToString)
                If maisu Mod 1D <> 0 Then
                    maisu = 1
                End If
            Catch ex As Exception
                maisu = 1
            End Try

            '枚数分繰り返し
            For cnt As Integer = 1 To CInt(maisu)

                'シークエンスNoの取得処理
                Dim nextSeqNo As String = GetSeqNo(ds)

                'QRコードのデータ作成
                Dim QRCode As New System.Text.StringBuilder()
                QRCode.Append("CI:")
                QRCode.Append("FE")
                QRCode.Append(",DC:")
                QRCode.Append("30")
                QRCode.Append(",CD:")
                QRCode.Append(outRow.Item("CUST_GOODS_CD").ToString)
                QRCode.Append(",LOT:")
                QRCode.Append(outRow.Item("LOT_NO").ToString)
                QRCode.Append(",MLOT:")
                QRCode.Append(outRow.Item("MLOT").ToString)
                QRCode.Append(",PL:")
                QRCode.Append(inRow.Item("WH_CD").ToString)
                QRCode.Append(",SP:")
                QRCode.Append(inRow.Item("CUST_CD_L").ToString)
                QRCode.Append(",SG:")
                QRCode.Append("")
                QRCode.Append(",ORD:")
                QRCode.Append(outRow.Item("OUTKA_FROM_ORD_NO").ToString)
                QRCode.Append(",QT:")
                QRCode.Append(outRow.Item("STD_IRIME").ToString)
                QRCode.Append(",SQ:")
                QRCode.Append(nextSeqNo)

                Dim genpinRow As DataRow = genpinTbl.NewRow
                '行の設定
                genpinRow.Item("RPT_ID") = ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString
                genpinRow.Item("NONYU_DATE") = DateTime.ParseExact(outRow.Item("NONYU_DATE").ToString, "yyyyMMdd", Nothing).ToString("yyyy年MM月dd日(ddd)")
                genpinRow.Item("NONYU_BASHO") = outRow.Item("NONYU_BASHO1").ToString & " " & outRow.Item("NONYU_BASHO2").ToString
                genpinRow.Item("GOODS_CD_CUST") = outRow.Item("CUST_GOODS_CD").ToString
                genpinRow.Item("GOODS_NM") = outRow.Item("GOODS_NM").ToString
                genpinRow.Item("MLOT") = outRow.Item("MLOT").ToString
                genpinRow.Item("LOT_NO") = outRow.Item("LOT_NO").ToString
                genpinRow.Item("QRCODE") = QRCode.ToString
                genpinRow.Item("WT") = outRow.Item("STD_IRIME").ToString
                '行の追加
                genpinTbl.Rows.Add(genpinRow)


                Dim genpinPrtRow As DataRow = genpinPrtTbl.NewRow
                '行の設定
                genpinPrtRow.Item("EDI_CTL_NO") = outRow.Item("EDI_CTL_NO").ToString
                genpinPrtRow.Item("OUTKA_FROM_ORD_NO") = outRow.Item("OUTKA_FROM_ORD_NO").ToString
                '行の追加
                genpinPrtTbl.Rows.Add(genpinPrtRow)

            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' シークエンスNo取得、更新メソッド
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSeqNo(ByVal ds As DataSet) As String

        '検索条件の設定
        Dim seqInTbl As DataTable = ds.Tables("GET_SEQ_NO_IN")
        Dim seqInRow As DataRow = seqInTbl.NewRow

        Dim outRow As DataRow = ds.Tables("SELECT_PRINT_DATA_OUT").Rows(0)

        seqInRow.Item("NRS_BR_CD") = outRow.Item("NRS_BR_CD")
        seqInRow.Item("CUST_CD_L") = ds.Tables("PRINT_DATA_IN").Rows(0).Item("CUST_CD_L")
        seqInRow.Item("GOODS_CD_CUST") = outRow.Item("CUST_GOODS_CD")
        seqInRow.Item("LOT_NO") = outRow.Item("LOT_NO")

        seqInTbl.Rows.Add(seqInRow)

        'シークエンスNoの検索
        ds = Me.CallDAC(Me._Dac, "SelectSeqNo", ds)

        If ds.Tables("GET_SEQ_NO_OUT").Rows.Count = 0 Then
            'シークエンスNoなし

            'シークエンスNoに1を設定
            Dim row As DataRow = ds.Tables("GET_SEQ_NO_OUT").NewRow
            row.Item("NEXT_SEQ_NO") = "1"
            ds.Tables("GET_SEQ_NO_OUT").Rows.Add(row)

            'シークエンスNoの新規登録
            Me.CallDAC(Me._Dac, "InsertSeqNo", ds)
        Else
            'シークエンスNoあり

            'シークエンスNoのインクリメント
            Me.CallDAC(Me._Dac, "UpdateSeqNo", ds)
        End If

        Return ds.Tables("GET_SEQ_NO_OUT").Rows(0).Item("NEXT_SEQ_NO").ToString

    End Function

#End Region

#End Region

End Class
