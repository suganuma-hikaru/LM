' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC520    : 出荷指示書印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC520BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC520BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC520DAC = New LMC520DAC()
    Private _521Dac As LMC521DAC = New LMC521DAC()
    Private _812Dac As LMC812DAC = New LMC812DAC()

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

        '部数の設定がない場合は初期値0を設定
        Dim prtNb As Integer = 0
        If String.IsNullOrEmpty(ds.Tables("LMC520IN").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMC520IN").Rows(0).Item("PRT_NB"))
        End If

        'IN条件0件チェック
        If ds.Tables("LMC520IN").Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds

        End If

        '棟別出力フラグを取得
        Dim touNoFlg As String = ds.Tables("LMC520IN").Rows(0).Item("TOU_BETU_FLG").ToString()

        '使用帳票ID取得
        ds = Me.SelectMPrt(ds)

        '棟別に出力の場合、存在する棟番号を取得
        If touNoFlg = "1" Then
            ds = Me.SelectTouNo(ds)
        End If

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        ds = Me.SelectPrintData(ds)

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
        'レポートID分繰り返す
        Dim prtDs As DataSet
        '帳票種別用
        Dim rptId As String = String.Empty

        Dim prtmax As Integer = 0
        Dim prtDsMax As Integer = 0
        '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- START --
        Dim prtCkMax As Integer = 0
        '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- END --

        '(2012.07.25) コンソリ業務対応 -- START --
        Dim tableName As String = "LMC520OUT"
        '(2012.07.25) コンソリ業務対応 --  END  --

#If True Then   'ADD 2018/12/06 依頼番号 : 003561   【LMS】横浜BC_アクサルタ出荷指図書＿棟ごと印刷網掛け+立合書は通常通り印刷
        Dim tachiai_FLG As String = LMConst.FLG.OFF
        Dim FLG_40_00588 As String = LMConst.FLG.OFF
#End If

        For Each dr As DataRow In ds.Tables("M_RPT").Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            prtDs = New DataSet

            'レポートID取得
            rptId = dr.Item("RPT_ID").ToString()

            '2012.10.31 yamanaka Start
            '(2012.08.07) 群馬対応         -- START --
            '(2012.07.25) コンソリ業務対応 -- START --
            'If rptId = "LMC539" Then
            '    tableName = "LMC539OUT"
            'End If

            'Select rptId
            '    Case "LMC539"
            '        tableName = "LMC539OUT"
            '    Case "LMC530", "LMC533", "LMC534"
            '        '群馬系帳票
            '        tableName = "LMC530OUT"
            'End Select

            Select Case rptId
                Case "LMC538"
                    tableName = "LMC538OUT"
                Case "LMC539"
                    tableName = "LMC539OUT"
                Case "LMC530", "LMC533", "LMC534"
                    '群馬系帳票
                    tableName = "LMC530OUT"

                    '2014/01/24 昭和電工用追加 START
                Case "LMC751"
                    tableName = "LMC751OUT"
                    '2014/01/24 昭和電工用追加 END
                    '20151027 シンガポール対応 START
                Case "LMC812", "LMC814", "LMC845"
                    'tableName = "LMC520OUT"
                    '2016.01.14 英語化対応　修正START
                    tableName = "LMC812OUT"
                    '2016.01.14 英語化対応　修正END
                    '20151027 シンガポール対応 END

                Case LMC520DAC.RPT_ID_FIR   ' フィルメニッヒ用
                    tableName = LMC520DAC.OUT_TABLE_NAME_FIR
#If False Then  'UPD 2020/08/12 013777   【LMS】FFEM_出荷指示書同一シリアル番号ごとの改行抑抑止
                Case LMC520DAC.RPT_ID_FFEM, LMC520DAC.RPT_ID_FFEM_TAK   ' FFFM用 追加 20160916
#Else
                Case LMC520DAC.RPT_ID_FFEM, LMC520DAC.RPT_ID_FFEM_TAK, "LMC749"
#End If
                    tableName = LMC520DAC.OUT_TABLE_NAME_FFEM
                Case "LMC890"
                    tableName = "LMC890OUT"

            End Select

            '(2012.07.25) コンソリ業務対応 --  END  --
            '(2012.08.07) 群馬対応         --  END  --
            '2012.10.31 yamanaka End

            '空まはたNULLの場合は処理を飛ばす
            If String.IsNullOrEmpty(rptId) = False Then
                '指定したレポートIDのデータを抽出する。
                'prtDs = comPrt.CallDataSet(ds.Tables("LMC520OUT"), dr.Item("RPT_ID").ToString())   '(2012.07.25) コンソリ業務対応 [コメント]
                prtDs = comPrt.CallDataSet(ds.Tables(tableName), dr.Item("RPT_ID").ToString())      '(2012.07.25) コンソリ業務対応 [修正]
                'データセットの編集(出力用テーブルに抽出データを設定)
                prtDs = Me.EditPrintDataSet(rptId, prtDs)

                '2012.10.31 yamanaka Start
                If rptId.Equals("LMC538") Then
                    Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                    Continue For
                End If
                '2012.10.31 yamanaka End

                '棟別に出力の場合、各出力レコードの印刷対象棟フラグを編集
                If touNoFlg = "1" Then

                    '棟毎に印刷対象棟フラグを編集しCSV出力
                    Dim touNum As Integer = ds.Tables("TOU_NO_LIST").Rows.Count - 1
                    For i As Integer = 0 To touNum

                        comPrt = New LMReportDesignerUtility

#If False Then      'UPD 2018/09/04 依頼番号 : 001868   【LMS】出荷指示書_横浜アクサルタで4枚同時印刷したい
                        '印刷対象棟フラグを編集
                        prtDs = Me.EditTouNoDataSet(ds.Tables("TOU_NO_LIST").Rows(i).Item("TOU_NO").ToString, rptId, prtDs, tableName)

#Else
                        If ds.Tables(tableName).Rows(i).Item("NRS_BR_CD").ToString.Equals("40") Then
                            If ds.Tables(tableName).Rows(i).Item("CUST_CD_L").ToString.Equals("00588") Then

                                '印刷対象棟フラグを編集
                                prtDs = Me.EditTouNoDataSet2(ds.Tables("TOU_NO_LIST").Rows(i).Item("TOU_NO").ToString, ds.Tables("TOU_NO_LIST").Rows(i).Item("LOCA_FLG").ToString, rptId, prtDs, tableName)

#If True Then   'ADD 2018/12/06 依頼番号 : 003561   【LMS】横浜BC_アクサルタ出荷指図書＿棟ごと印刷網掛け+立合書は通常通り印刷

                                If FLG_40_00588 = LMConst.FLG.OFF Then
                                    tachiai_FLG = LMConst.FLG.ON
                                    FLG_40_00588 = LMConst.FLG.ON
                                End If
#End If
                            End If
                        Else

                            '印刷対象棟フラグを編集
                            prtDs = Me.EditTouNoDataSet(ds.Tables("TOU_NO_LIST").Rows(i).Item("TOU_NO").ToString, rptId, prtDs, tableName)

                        End If
#End If

                        'prtDsMax = prtDs.Tables("LMC520OUT").Rows.Count - 1    '(2012.07.25) コンソリ業務対応 [コメント]
                        prtDsMax = prtDs.Tables(tableName).Rows.Count - 1       '(2012.07.25) コンソリ業務対応 [修正]

                        '(2012.07.25) コンソリ業務対応 -- START --
                        '(2012.06.04) 埼玉対応 -- START --
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True Then
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True Then
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True Then
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True OrElse (rptId).Equals("LMC536") = True Then
                        If (rptId).Equals("LMC527") = True _
                            OrElse (rptId).Equals("LMC528") = True _
                            OrElse (rptId).Equals("LMC886") = True _
                            OrElse (rptId).Equals("LMC529") = True _
                            OrElse (rptId).Equals("LMC539") = True _
                            OrElse (rptId).Equals("LMC536") = True _
                            OrElse (rptId).Equals("LMC861") = True _
                            OrElse (rptId).Equals("LMC542") = True Then
                            '(2012.06.04) 埼玉対応 --  END  --
                            '(2012.07.25) コンソリ業務対応 --  END  --

                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                            prtmax = 1
                            'prtmax = 2
                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --
                            '2015.07.01 住化バイエル　指示書3部出力　追加START
                        ElseIf (rptId).Equals("LMC618") = True Then
                            prtmax = 2
                            '2015.07.01 住化バイエル　指示書3部出力　追加END
                        ElseIf (rptId).Equals("LMC881") = True Then
                            prtmax = 2
                        Else
                            prtmax = 0
                        End If
                        For j As Integer = 0 To prtmax
                            If j = 0 Then
                                For k As Integer = 0 To prtDsMax
                                    'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "01"   '(2012.07.25) コンソリ業務対応 [コメント]
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "01"      '(2012.07.25) コンソリ業務対応 [修正]
                                Next
                                '(2012.07.25) コンソリ業務対応 -- START --
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                'Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                '(2012.07.25) コンソリ業務対応 --  END  --

                            ElseIf j = 1 AndAlso rptId.Equals("LMC881") Then
                                For k As Integer = 0 To prtDsMax
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"
                                Next
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)

                            ElseIf j = 1 Then

#If False Then   'ADD 2018/12/06 依頼番号 : 003561   【LMS】横浜BC_アクサルタ出荷指図書＿棟ごと印刷網掛け+立合書は通常通り印刷

                                For k As Integer = 0 To prtDsMax
                                    '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                                    'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "02"
                                    'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "03"   '(2012.07.25) コンソリ業務対応 [コメント]
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"      '(2012.07.25) コンソリ業務対応 [修正]
                                    '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END --
                                Next
                                '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- START --
                                'If (rptId).Equals("LMC529") = True Then
                                prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())
                                'End If
                                For l As Integer = 1 To prtCkMax
                                    '(2012.07.25) コンソリ業務対応 -- START --
                                    Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                    'Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                    '(2012.07.25) コンソリ業務対応 --  END  --
                                Next
                                '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- END --

                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                                '2015.07.01 住化バイエル　指示書3部出力　追加START
#Else
                                If (FLG_40_00588).Equals(LMConst.FLG.ON) = True _
                                    AndAlso (tachiai_FLG).Equals(LMConst.FLG.ON) = True Then
                                    'アクサルタのとき
                                    For k As Integer = 0 To prtDsMax
                                        prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"

                                        prtDs.Tables(tableName).Rows(k).Item("TOU_BETU_FLG") = "0"  '網掛けなし

                                    Next

                                    prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())
                                    'End If
                                    For l As Integer = 1 To prtCkMax
                                        Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                    Next

                                    ' '立合書印刷終了
                                    tachiai_FLG = LMConst.FLG.OFF
                                Else
                                    If (FLG_40_00588).Equals(LMConst.FLG.OFF) = True Then

                                        For k As Integer = 0 To prtDsMax
                                            prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"      '(2012.07.25) コンソリ業務対応 [修正]
                                        Next

                                        prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())
                                        'End If
                                        For l As Integer = 1 To prtCkMax
                                            Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                        Next

                                    End If

                                End If

#End If

                            ElseIf j = 2 AndAlso rptId.Equals("LMC618") Then
                                For k As Integer = 0 To prtDsMax
                                    prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "04"
                                Next
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                '2015.07.01 住化バイエル　指示書3部出力　追加END

                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --

                            ElseIf j = 2 AndAlso rptId.Equals("LMC881") Then
                                For k As Integer = 0 To prtDsMax
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "04"
                                Next
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)

                            End If
                        Next

                        'For J As Integer用

                        'TODO 開発元の回答により対応
                        '★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
                        '1秒（1000ミリ秒）待機する
                        'System.Threading.Thread.Sleep(1000)
                        '★★★ END


                        ''棟別の帳票CSV出力
                        'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                        '                      dr.Item("PTN_ID").ToString(), _
                        '                      dr.Item("PTN_CD").ToString(), _
                        '                      dr.Item("RPT_ID").ToString(), _
                        '                      prtDs.Tables("LMC520OUT"), _
                        '                      ds.Tables(LMConst.RD), _
                        '                      String.Empty, _
                        '                      String.Empty, _
                        '                      prtNb)
                    Next

                Else '棟別出力を行わない場合(そのまま出力)

                    comPrt = New LMReportDesignerUtility

                    'prtDsMax = prtDs.Tables("LMC520OUT").Rows.Count - 1    '(2012.07.25) コンソリ業務対応 [コメント]
                    prtDsMax = prtDs.Tables(tableName).Rows.Count - 1       '(2012.07.25) コンソリ業務対応 [修正]

                    '(2012.07.25) コンソリ業務対応 -- START --
                    '(2012.06.04) 出荷指図書印刷(埼玉標準) -- START --
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True Then
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True Then
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True Then
                    '2015.07.01 住化バイエル　指示書3部出力　追加START
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True OrElse (rptId).Equals("LMC536") = True OrElse (rptId).Equals("LMC618") = True Then
                    If (rptId).Equals("LMC527") = True _
                        OrElse (rptId).Equals("LMC528") = True _
                        OrElse (rptId).Equals("LMC886") = True _
                        OrElse (rptId).Equals("LMC529") = True _
                        OrElse (rptId).Equals("LMC539") = True _
                        OrElse (rptId).Equals("LMC536") = True _
                        OrElse (rptId).Equals("LMC618") = True _
                        OrElse (rptId).Equals("LMC861") = True _
                        OrElse (rptId).Equals("LMC881") = True _
                        OrElse (rptId).Equals("LMC542") = True Then
                        '2015.07.01 住化バイエル　指示書3部出力　追加END
                        '(2012.06.04) 出荷指図書印刷(埼玉標準) -- END --
                        '(2012.07.25) コンソリ業務対応 -- END --
                        prtmax = 2
                    Else
                        prtmax = 0
                    End If
                    For j As Integer = 0 To prtmax
                        If j = 0 Then
                            For k As Integer = 0 To prtDsMax
                                'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "01"   '(2012.07.25) コンソリ業務対応 [コメント]
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "01"      '(2012.07.25) コンソリ業務対応 [修正]
                            Next
                            '(2012.07.25) コンソリ業務対応 -- START --
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                            'Call Me.CSV_OUT(ds, dr, prtDs, prtNb)
                            '(2012.07.25) コンソリ業務対応 --  END  --

                        ElseIf j = 1 AndAlso rptId.Equals("LMC881") Then
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)

                        ElseIf j = 1 Then
                            For k As Integer = 0 To prtDsMax
                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                                'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "02"
                                'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "03"   '(2012.07.25) コンソリ業務対応 [コメント]
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"      '(2012.07.25) コンソリ業務対応 [修正]
                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --
                            Next
                            '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- START --
                            'If (rptId).Equals("LMC529") = True Then
                            prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())
                            'End If
                            For l As Integer = 1 To prtCkMax
                                '(2012.07.25) コンソリ業務対応 -- START --
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                'Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                '(2012.07.25) コンソリ業務対応 --  END  --
                            Next
                            '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- END --

                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --

                            '2015.07.01 住化バイエル　指示書3部出力　追加START
                        ElseIf j = 2 AndAlso rptId.Equals("LMC618") Then
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "04"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                            '2015.07.01 住化バイエル　指示書3部出力　追加END

                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --

                        ElseIf j = 2 AndAlso rptId.Equals("LMC881") Then
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "04"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)

                        End If
                    Next

                    'For J As Integer用

                    'TODO 開発元の回答により対応
                    '★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
                    '1秒（1000ミリ秒）待機する
                    'System.Threading.Thread.Sleep(1000)
                    '★★★ END


                    '帳票CSV出力
                    'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                    '                      dr.Item("PTN_ID").ToString(), _
                    '                      dr.Item("PTN_CD").ToString(), _
                    '                      dr.Item("RPT_ID").ToString(), _
                    '                      prtDs.Tables("LMC520OUT"), _
                    '                      ds.Tables(LMConst.RD), _
                    '                      String.Empty, _
                    '                      String.Empty, _
                    '                      prtNb)
                End If

            End If

        Next

        Return ds

    End Function
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint2(ByVal ds As DataSet) As DataSet

        '部数の設定がない場合は初期値0を設定
        Dim prtNb As Integer = 0
        If String.IsNullOrEmpty(ds.Tables("LMC520IN").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMC520IN").Rows(0).Item("PRT_NB"))
        End If

        'IN条件0件チェック
        If ds.Tables("LMC520IN").Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds

        End If

        '棟別出力フラグを取得
        Dim touNoFlg As String = ds.Tables("LMC520IN").Rows(0).Item("TOU_BETU_FLG").ToString()

        '使用帳票ID取得
        ds = Me.SelectMPrt(ds)

        '棟別に出力の場合、存在する棟番号を取得
        If touNoFlg = "1" Then
            ds = Me.SelectTouNo(ds)
        End If

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        ds = Me.SelectPrintData(ds)

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
        'レポートID分繰り返す
        Dim prtDs As DataSet
        '帳票種別用
        Dim rptId As String = String.Empty

        Dim prtmax As Integer = 0
        Dim prtDsMax As Integer = 0
        '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- START --
        Dim prtCkMax As Integer = 0
        '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- END --

        '(2012.07.25) コンソリ業務対応 -- START --
        Dim tableName As String = "LMC520OUT"
        '(2012.07.25) コンソリ業務対応 --  END  --

        For Each dr As DataRow In ds.Tables("M_RPT").Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            prtDs = New DataSet

            'レポートID取得
            rptId = dr.Item("RPT_ID").ToString()

            '2012.10.31 yamanaka Start
            '(2012.08.07) 群馬対応         -- START --
            '(2012.07.25) コンソリ業務対応 -- START --
            'If rptId = "LMC539" Then
            '    tableName = "LMC539OUT"
            'End If

            'Select rptId
            '    Case "LMC539"
            '        tableName = "LMC539OUT"
            '    Case "LMC530", "LMC533", "LMC534"
            '        '群馬系帳票
            '        tableName = "LMC530OUT"
            'End Select

            Select Case rptId
                Case "LMC538"
                    tableName = "LMC538OUT"
                Case "LMC539"
                    tableName = "LMC539OUT"
                Case "LMC530", "LMC533", "LMC534"
                    '群馬系帳票
                    tableName = "LMC530OUT"

                    '2014/01/24 昭和電工用追加 START
                Case "LMC751"
                    tableName = "LMC751OUT"
                    '2014/01/24 昭和電工用追加 END
                    '2015/10/27 シンガポール START
                Case "LMC812", "LMC814", "LMC845"
                    tableName = "LMC812OUT"
                    '2015/10/27 シンガポール END
                Case LMC520DAC.RPT_ID_FIR   ' フィルメニッヒ用
                    tableName = LMC520DAC.OUT_TABLE_NAME_FIR
#If False Then  'UPD 2020/08/12 013777   【LMS】FFEM_出荷指示書同一シリアル番号ごとの改行抑抑止
                 Case LMC520DAC.RPT_ID_FFEM, LMC520DAC.RPT_ID_FFEM_TAK   ' FFFM用 追加 20160916
#Else
                Case LMC520DAC.RPT_ID_FFEM, LMC520DAC.RPT_ID_FFEM_TAK, "LMC749"
#End If
                    tableName = LMC520DAC.OUT_TABLE_NAME_FFEM
            End Select

            '(2012.07.25) コンソリ業務対応 --  END  --
            '(2012.08.07) 群馬対応         --  END  --
            '2012.10.31 yamanaka End

            '空まはたNULLの場合は処理を飛ばす
            If String.IsNullOrEmpty(rptId) = False Then
                '指定したレポートIDのデータを抽出する。
                'prtDs = comPrt.CallDataSet(ds.Tables("LMC520OUT"), dr.Item("RPT_ID").ToString())   '(2012.07.25) コンソリ業務対応 [コメント]
                prtDs = comPrt.CallDataSet(ds.Tables(tableName), dr.Item("RPT_ID").ToString())      '(2012.07.25) コンソリ業務対応 [修正]
                'データセットの編集(出力用テーブルに抽出データを設定)
                prtDs = Me.EditPrintDataSet(rptId, prtDs)

                '2012.10.31 yamanaka Start
                If rptId.Equals("LMC538") Then
                    Call Me.CSV_OUT2(ds, dr, prtDs, prtNb, tableName)
                    Continue For
                End If
                '2012.10.31 yamanaka End

                '棟別に出力の場合、各出力レコードの印刷対象棟フラグを編集
                If touNoFlg = "1" Then

                    '棟毎に印刷対象棟フラグを編集しCSV出力
                    Dim touNum As Integer = ds.Tables("TOU_NO_LIST").Rows.Count - 1
                    For i As Integer = 0 To touNum

                        comPrt = New LMReportDesignerUtility

                        '印刷対象棟フラグを編集
                        prtDs = Me.EditTouNoDataSet(ds.Tables("TOU_NO_LIST").Rows(i).Item("TOU_NO").ToString, rptId, prtDs, tableName)

                        'prtDsMax = prtDs.Tables("LMC520OUT").Rows.Count - 1    '(2012.07.25) コンソリ業務対応 [コメント]
                        prtDsMax = prtDs.Tables(tableName).Rows.Count - 1       '(2012.07.25) コンソリ業務対応 [修正]

                        '(2012.07.25) コンソリ業務対応 -- START --
                        '(2012.06.04) 埼玉対応 -- START --
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True Then
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True Then
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True Then
                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True OrElse (rptId).Equals("LMC536") = True Then
                        If (rptId).Equals("LMC527") = True _
                            OrElse (rptId).Equals("LMC528") = True _
                            OrElse (rptId).Equals("LMC886") = True _
                            OrElse (rptId).Equals("LMC529") = True _
                            OrElse (rptId).Equals("LMC539") = True _
                            OrElse (rptId).Equals("LMC536") = True _
                            OrElse (rptId).Equals("LMC861") = True _
                            OrElse (rptId).Equals("LMC542") = True Then
                            '(2012.06.04) 埼玉対応 --  END  --
                            '(2012.07.25) コンソリ業務対応 --  END  --

                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                            prtmax = 1
                            'prtmax = 2
                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --

                        Else
                            prtmax = 0
                        End If
                        For j As Integer = 0 To prtmax
                            If j = 0 Then
                                For k As Integer = 0 To prtDsMax
                                    'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "01"   '(2012.07.25) コンソリ業務対応 [コメント]
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "01"      '(2012.07.25) コンソリ業務対応 [修正]
                                Next
                                '(2012.07.25) コンソリ業務対応 -- START --
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                'Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                '(2012.07.25) コンソリ業務対応 --  END  --

                            ElseIf j = 1 Then
                                For k As Integer = 0 To prtDsMax
                                    '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                                    'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "02"
                                    'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "03"   '(2012.07.25) コンソリ業務対応 [コメント]
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"      '(2012.07.25) コンソリ業務対応 [修正]
                                    '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END --
                                Next
                                '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- START --
                                'If (rptId).Equals("LMC529") = True Then
                                prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())
                                'End If
                                For l As Integer = 1 To prtCkMax
                                    '(2012.07.25) コンソリ業務対応 -- START --
                                    Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                    'Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                    '(2012.07.25) コンソリ業務対応 --  END  --
                                Next
                                '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- END --

                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                                'ElseIf j = 2 Then
                                '    For k As Integer = 0 To prtDsMax
                                '        prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "03"
                                '    Next
                                '    Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --

                            End If
                        Next

                        'For J As Integer用

                        'TODO 開発元の回答により対応
                        '★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
                        '1秒（1000ミリ秒）待機する
                        'System.Threading.Thread.Sleep(1000)
                        '★★★ END


                        ''棟別の帳票CSV出力
                        'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                        '                      dr.Item("PTN_ID").ToString(), _
                        '                      dr.Item("PTN_CD").ToString(), _
                        '                      dr.Item("RPT_ID").ToString(), _
                        '                      prtDs.Tables("LMC520OUT"), _
                        '                      ds.Tables(LMConst.RD), _
                        '                      String.Empty, _
                        '                      String.Empty, _
                        '                      prtNb)
                    Next

                Else '棟別出力を行わない場合(そのまま出力)

                    comPrt = New LMReportDesignerUtility

                    'prtDsMax = prtDs.Tables("LMC520OUT").Rows.Count - 1    '(2012.07.25) コンソリ業務対応 [コメント]
                    prtDsMax = prtDs.Tables(tableName).Rows.Count - 1       '(2012.07.25) コンソリ業務対応 [修正]

                    '(2012.07.25) コンソリ業務対応 -- START --
                    '(2012.06.04) 出荷指図書印刷(埼玉標準) -- START --
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True Then
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True Then
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True Then
                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True OrElse (rptId).Equals("LMC536") = True Then
                    If (rptId).Equals("LMC527") = True _
                        OrElse (rptId).Equals("LMC528") = True _
                        OrElse (rptId).Equals("LMC886") = True _
                        OrElse (rptId).Equals("LMC529") = True _
                        OrElse (rptId).Equals("LMC539") = True _
                        OrElse (rptId).Equals("LMC536") = True _
                        OrElse (rptId).Equals("LMC861") = True _
                        OrElse (rptId).Equals("LMC542") = True Then
                        '(2012.06.04) 出荷指図書印刷(埼玉標準) -- END --
                        '(2012.07.25) コンソリ業務対応 -- END --
                        prtmax = 2
                    Else
                        prtmax = 0
                    End If
                    For j As Integer = 0 To prtmax
                        If j = 0 Then
                            For k As Integer = 0 To prtDsMax
                                'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "01"   '(2012.07.25) コンソリ業務対応 [コメント]
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "01"      '(2012.07.25) コンソリ業務対応 [修正]
                            Next
                            '(2012.07.25) コンソリ業務対応 -- START --
                            Call Me.CSV_OUT2(ds, dr, prtDs, prtNb, tableName)
                            'Call Me.CSV_OUT(ds, dr, prtDs, prtNb)
                            '(2012.07.25) コンソリ業務対応 --  END  --

                        ElseIf j = 1 Then
                            For k As Integer = 0 To prtDsMax
                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                                'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "02"
                                'prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "03"   '(2012.07.25) コンソリ業務対応 [コメント]
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"      '(2012.07.25) コンソリ業務対応 [修正]
                                '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --
                            Next
                            '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- START --
                            'If (rptId).Equals("LMC529") = True Then
                            prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())
                            'End If
                            For l As Integer = 1 To prtCkMax
                                '(2012.07.25) コンソリ業務対応 -- START --
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                'Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb)
                                '(2012.07.25) コンソリ業務対応 --  END  --
                            Next
                            '(2012.06.08) Notes№1123 荷主明細マスタの値をセット -- END --

                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 -- START --
                            'ElseIf j = 2 Then
                            '    For k As Integer = 0 To prtDsMax
                            '        prtDs.Tables("LMC520OUT").Rows(k).Item("RPT_FLG") = "03"
                            '    Next
                            '    Call Me.CSV_OUT(ds, dr, prtDs, prtNb)
                            '(2012.03.27) Notes№914 3部出力→2部出力に変更 --  END  --

                        End If
                    Next

                    'For J As Integer用

                    'TODO 開発元の回答により対応
                    '★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
                    '1秒（1000ミリ秒）待機する
                    'System.Threading.Thread.Sleep(1000)
                    '★★★ END


                    '帳票CSV出力
                    'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                    '                      dr.Item("PTN_ID").ToString(), _
                    '                      dr.Item("PTN_CD").ToString(), _
                    '                      dr.Item("RPT_ID").ToString(), _
                    '                      prtDs.Tables("LMC520OUT"), _
                    '                      ds.Tables(LMConst.RD), _
                    '                      String.Empty, _
                    '                      String.Empty, _
                    '                      prtNb)
                End If

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象棟フラグの編集を行う。
    ''' </summary>
    ''' <param name="touNo"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditTouNoDataSet(ByVal touNo As String, ByVal rptId As String, ByVal ds As DataSet, ByVal outTableNm As String) As DataSet

        '各出力レコードについて印刷対象棟フラグを編集
        '編集対象テーブル名
        '(2012.07.25)コンソリ業務対応 パラメータに変更
        'Dim outTableNm As String = "LMC520OUT"

        '印刷対象棟フラグ設定(対象棟:0 その他棟:1)
        Dim count As Integer = ds.Tables(outTableNm).Rows.Count - 1
        For i As Integer = 0 To count
            If ds.Tables(outTableNm).Rows(i).Item("TOU_NO").ToString = touNo Then
                '対象棟
                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
            Else
                'その他棟
                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象棟フラグの編集を行う。(横浜ｱｸｻﾙﾀ専用)
    ''' </summary>
    ''' <param name="touNo"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditTouNoDataSet2(ByVal touNo As String, ByVal locaFlg As String, ByVal rptId As String, ByVal ds As DataSet, ByVal outTableNm As String) As DataSet

        '各出力レコードについて印刷対象棟フラグを編集
        '編集対象テーブル名
        '(2012.07.25)コンソリ業務対応 パラメータに変更
        'Dim outTableNm As String = "LMC520OUT"

        '印刷対象棟フラグ設定(対象棟:0 その他棟:1)
        Dim count As Integer = ds.Tables(outTableNm).Rows.Count - 1
        For i As Integer = 0 To count

            If ds.Tables(outTableNm).Rows(i).Item("TOU_NO").ToString = touNo Then

                If touNo.Equals("01") = True Then


                    Select Case locaFlg.ToString
                        Case "OFF"

                            If ds.Tables(outTableNm).Rows(i).Item("LOCA").ToString = "" Then
                                '対象
                                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
                            Else
                                '対象外
                                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
                            End If
                        Case Else
                            'ON 時
                            If ds.Tables(outTableNm).Rows(i).Item("LOCA").ToString <> "" Then
                                '対象
                                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
                            Else
                                '対象外
                                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
                            End If

                    End Select

                Else
                    If ds.Tables(outTableNm).Rows(i).Item("TOU_NO").ToString = touNo Then
                        '対象棟
                        ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
                    Else
                        'その他棟
                        ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
                    End If

                End If
            Else

                '対象外
                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"

            End If




            ''Select Case ds.Tables(outTableNm).Rows(i).Item("TOU_NO").ToString
            ''    Case "01"
            ''        Select Case locaFlg.ToString
            ''            Case "OFF"

            ''                If ds.Tables(outTableNm).Rows(i).Item("LOCA").ToString = "" Then
            ''                    '対象
            ''                    ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
            ''                Else
            ''                    '対象外
            ''                    ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
            ''                End If
            ''            Case Else
            ''                'ON 時
            ''                If ds.Tables(outTableNm).Rows(i).Item("LOCA").ToString <> "" Then
            ''                    '対象
            ''                    ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
            ''                Else
            ''                    '対象外
            ''                    ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
            ''                End If

            ''        End Select

            ''    Case Else

            ''        If ds.Tables(outTableNm).Rows(i).Item("TOU_NO").ToString = touNo Then
            ''            '対象棟
            ''            ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
            ''        Else
            ''            'その他棟
            ''            ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
            ''        End If

            ''End Select

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        '2011/9/8 SBS)佐川 横浜荷主インターコンチネンタルについてのデータソート処理を削除
        ''横浜荷主インターコンチネンタル（00125）の場合は入力順（出荷管理番号L）にソートする
        ''営業所コード取得
        'Dim brCd As String = ds.Tables("LMC520OUT").Rows(0).Item("NRS_BR_CD").ToString()
        ''荷主コード取得
        'Dim custCd As String = ds.Tables("LMC520OUT").Rows(0).Item("CUST_CD_L").ToString()

        'If brCd = "40" AndAlso custCd = "00125" Then

        '    'ソート対象データテーブル取得
        '    Dim outDt As DataTable = ds.Tables("LMC520OUT")
        '    'ソート条件設定
        '    Dim sort As String = "OUTKA_NO_L ASC, OUTKA_NO_M ASC"
        '    'ソート実行
        '    Dim dr As DataRow() = outDt.Select(Nothing, sort)
        '    'ソート実行後データ格納データセット作成
        '    Dim rtnDs As DataSet = ds.Clone
        '    'ソート済みデータ格納
        '    For Each row As DataRow In dr
        '        rtnDs.Tables("LMC520OUT").ImportRow(row)
        '    Next

        '    Return rtnDs

        'End If


        Select Case rptId
            '追記
            Case "LMC522"

                'LOT_NO
                Dim lot As String = String.Empty
                '探したい文字列
                Dim NO As String = "NO."
                '文字列検出用
                Dim numYN As Integer = 0
                '左4桁格納用
                Dim left As String = String.Empty
                '中央3桁格納用
                Dim center As String = String.Empty
                '右3桁格納用
                Dim right As String = String.Empty
                Dim editLotNo As String = String.Empty
                'スペース
                Dim space As String = "  "
                'データテーブル取得
                Dim outDt As DataTable = ds.Tables("LMC520OUT")
                Dim max As Integer = outDt.Rows.Count - 1

                '抽出データ明細行の梱数チェック()
                For i As Integer = 0 To max
                    '初期化
                    left = String.Empty
                    center = String.Empty
                    right = String.Empty
                    editLotNo = String.Empty
                    lot = ((outDt.Rows(i).Item("LOT_NO").ToString())).ToUpper
                    numYN = lot.IndexOf(NO) '「NO.」を探す。あればゼロ以上になる。
                    If numYN = -1 Then
                        'ゼロ(NO.なし)なので加工。
                        If lot.Length >= 10 Then
                            left = lot.Substring(0, 4)
                            center = lot.Substring(4, 3)
                            right = Mid(lot, 8)
                            editLotNo = String.Concat(left, space, center, space, right)
                        Else
                            editLotNo = lot
                        End If

                        'データテーブルに戻す。
                        outDt.Rows(i).Item("LOT_NO") = editLotNo
                    Else
                    End If
                Next
                Return ds
                '追記終わり

            Case "LMC537", "LMC750", LMC520DAC.RPT_ID_NCG_HIDE_SERIAL_NO
                '(2012.11.13) 千葉_日油物流対応 [LMC537]
                '同一条件に該当するデータのまとめ処理
                Dim outDt As DataTable = ds.Tables("LMC520OUT")  'データ取得
                Dim cloneDt As DataTable = outDt.Clone
                '
                Dim wkKONSU As Decimal = 0
                Dim wkALCTD_QT As Decimal = 0
                Dim wkZAN_KONSU As Decimal = 0
                Dim wkALCTD_NB As Decimal = 0
                Dim wkALCTD_NB_HEADKEI As Decimal = 0
                Dim wkALCTD_QT_HEADKEI As Decimal = 0
                Dim wkEdit As String = String.Empty
                Dim wkKey As New ArrayList

                '検索条件の作成、引当済個数、引当済数量総計
                For Each row As DataRow In outDt.Rows

                    'ロット№
                    If IsDBNull(row.Item("LOT_NO")) = False Then
                        wkEdit += String.Concat("LOT_NO = '", row.Item("LOT_NO").ToString, "'")

                        If (LMC520DAC.RPT_ID_NCG_HIDE_SERIAL_NO.Equals(rptId)) Then
                            wkEdit += String.Concat(" AND GOODS_CD_CUST = '", row.Item("GOODS_CD_CUST").ToString, "'")
                            wkEdit += String.Concat(" AND IRIME = '", row.Item("IRIME").ToString, "'")
                            wkEdit += String.Concat(" AND IRIME_UT = '", row.Item("IRIME_UT").ToString, "'")
                            wkEdit += String.Concat(" AND TOU_NO = '", row.Item("TOU_NO").ToString, "'")
                            wkEdit += String.Concat(" AND SITU_NO = '", row.Item("SITU_NO").ToString, "'")
                            wkEdit += String.Concat(" AND ZONE_CD = '", row.Item("ZONE_CD").ToString, "'")
                            wkEdit += String.Concat(" AND REMARK_OUT = '", row.Item("REMARK_OUT").ToString, "'")
                            wkEdit += String.Concat(" AND REMARK_M = '", row.Item("REMARK_M").ToString, "'")
                            wkEdit += String.Concat(" AND INKA_DATE = '", row.Item("INKA_DATE").ToString, "'")
                            wkEdit += String.Concat(" AND REMARK_S = '", row.Item("REMARK_S").ToString, "'")
                            wkEdit += String.Concat(" AND GOODS_COND_NM_1 = '", row.Item("GOODS_COND_NM_1").ToString, "'")
                            wkEdit += String.Concat(" AND GOODS_COND_NM_2 = '", row.Item("GOODS_COND_NM_2").ToString, "'")
                            wkEdit += String.Concat(" AND SAGYO_MEI_REC_NO_1 = '", row.Item("SAGYO_MEI_REC_NO_1").ToString, "'")
                            wkEdit += String.Concat(" AND SAGYO_MEI_REC_NO_2 = '", row.Item("SAGYO_MEI_REC_NO_2").ToString, "'")
                            wkEdit += String.Concat(" AND SAGYO_MEI_REC_NO_3 = '", row.Item("SAGYO_MEI_REC_NO_3").ToString, "'")
                            wkEdit += String.Concat(" AND SAGYO_MEI_REC_NO_4 = '", row.Item("SAGYO_MEI_REC_NO_4").ToString, "'")
                            wkEdit += String.Concat(" AND SAGYO_MEI_REC_NO_5 = '", row.Item("SAGYO_MEI_REC_NO_5").ToString, "'")
                        End If

                    Else
                        wkEdit += String.Concat("LOT_NO IS NULL", "")
                    End If

                    '重複不可
                    If wkKey.Contains(wkEdit) = False Then
                        wkKey.Add(wkEdit)
                    End If

                    wkEdit = String.Empty

                    If IsDBNull(row.Item("ALCTD_NB")) = False Then
                        wkALCTD_NB_HEADKEI += Convert.ToDecimal(row.Item("ALCTD_NB"))
                    End If
                    If IsDBNull(row.Item("ALCTD_QT")) = False Then
                        wkALCTD_QT_HEADKEI += Convert.ToDecimal(row.Item("ALCTD_QT"))
                    End If

                Next

                'まとめ処理
                For i As Integer = 0 To wkKey.Count - 1
                    '検索条件による同一データの抽出
                    Dim wkDataRow() As DataRow = outDt.Select(wkKey(i).ToString)
                    If wkDataRow.Length > 1 Then
                        wkKONSU = 0
                        wkALCTD_QT = 0
                        wkZAN_KONSU = 0
                        wkALCTD_NB = 0
                        For Each tmpRow As DataRow In wkDataRow
                            wkKONSU += Convert.ToDecimal(tmpRow.Item("KONSU"))
                            wkALCTD_QT += Convert.ToDecimal(tmpRow.Item("ALCTD_QT"))
                            wkZAN_KONSU += Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                            wkALCTD_NB += Convert.ToDecimal(tmpRow.Item("ALCTD_NB"))
                        Next
                        wkDataRow(0).Item("KONSU") = wkKONSU
                        wkDataRow(0).Item("ALCTD_QT") = wkALCTD_QT
                        wkDataRow(0).Item("ZAN_KONSU") = wkZAN_KONSU
                        wkDataRow(0).Item("ALCTD_NB") = wkALCTD_NB
                        wkDataRow(0).Item("ZAN_HASU") = CLng(wkDataRow(0).Item("ALCTD_NB")) Mod CLng(wkDataRow(0).Item("PKG_NB"))
                    End If
                    cloneDt.ImportRow(wkDataRow(0))
                Next

                '元データクリア
                outDt.Clear()

                '集約データを元データへ
                For Each cloneRow As DataRow In cloneDt.Rows
                    Dim wkNewRow As DataRow = ds.Tables("LMC520OUT").NewRow
                    '-00-
                    wkNewRow("RPT_ID") = cloneRow("RPT_ID")
                    wkNewRow("NRS_BR_CD") = cloneRow("NRS_BR_CD")
                    wkNewRow("PRINT_SORT") = cloneRow("PRINT_SORT")
                    wkNewRow("TOU_BETU_FLG") = cloneRow("TOU_BETU_FLG")
                    wkNewRow("OUTKA_NO_L") = cloneRow("OUTKA_NO_L")
                    wkNewRow("DEST_CD") = cloneRow("DEST_CD")
                    wkNewRow("DEST_NM") = cloneRow("DEST_NM")
                    wkNewRow("DEST_AD_1") = cloneRow("DEST_AD_1")
                    wkNewRow("DEST_AD_2") = cloneRow("DEST_AD_2")
                    wkNewRow("DEST_AD_3") = cloneRow("DEST_AD_3")
                    '-01-
                    wkNewRow("DEST_TEL") = cloneRow("DEST_TEL")
                    wkNewRow("CUST_CD_L") = cloneRow("CUST_CD_L")
                    wkNewRow("CUST_NM_L") = cloneRow("CUST_NM_L")
                    wkNewRow("CUST_NM_M") = cloneRow("CUST_NM_M")
                    wkNewRow("CUST_NM_S") = cloneRow("CUST_NM_S")
                    wkNewRow("OUTKA_PKG_NB") = cloneRow("OUTKA_PKG_NB")
                    wkNewRow("CUST_ORD_NO") = cloneRow("CUST_ORD_NO")
                    wkNewRow("BUYER_ORD_NO") = cloneRow("BUYER_ORD_NO")
                    wkNewRow("OUTKA_PLAN_DATE") = cloneRow("OUTKA_PLAN_DATE")
                    wkNewRow("ARR_PLAN_DATE") = cloneRow("ARR_PLAN_DATE")
                    '-02-
                    wkNewRow("ARR_PLAN_TIME") = cloneRow("ARR_PLAN_TIME")
                    wkNewRow("UNSOCO_NM") = cloneRow("UNSOCO_NM")
                    wkNewRow("PC_KB") = cloneRow("PC_KB")
                    wkNewRow("KYORI") = cloneRow("KYORI")
                    wkNewRow("UNSO_WT") = cloneRow("UNSO_WT")
                    wkNewRow("URIG_NM") = cloneRow("URIG_NM")
                    wkNewRow("FREE_C03") = cloneRow("FREE_C03")
                    wkNewRow("REMARK_L") = cloneRow("REMARK_L")
                    wkNewRow("REMARK_UNSO") = cloneRow("REMARK_UNSO")
                    wkNewRow("SAGYO_REC_NO_1") = cloneRow("SAGYO_REC_NO_1")
                    '-03-
                    wkNewRow("SAGYO_CD_1") = cloneRow("SAGYO_CD_1")
                    wkNewRow("SAGYO_NM_1") = cloneRow("SAGYO_NM_1")
                    wkNewRow("SAGYO_REC_NO_2") = cloneRow("SAGYO_REC_NO_2")
                    wkNewRow("SAGYO_CD_2") = cloneRow("SAGYO_CD_2")
                    wkNewRow("SAGYO_NM_2") = cloneRow("SAGYO_NM_2")
                    wkNewRow("SAGYO_REC_NO_3") = cloneRow("SAGYO_REC_NO_3")
                    wkNewRow("SAGYO_CD_3") = cloneRow("SAGYO_CD_3")
                    wkNewRow("SAGYO_NM_3") = cloneRow("SAGYO_NM_3")
                    wkNewRow("SAGYO_REC_NO_4") = cloneRow("SAGYO_REC_NO_4")
                    wkNewRow("SAGYO_CD_4") = cloneRow("SAGYO_CD_4")
                    '-04-
                    wkNewRow("SAGYO_NM_4") = cloneRow("SAGYO_NM_4")
                    wkNewRow("SAGYO_REC_NO_5") = cloneRow("SAGYO_REC_NO_5")
                    wkNewRow("SAGYO_CD_5") = cloneRow("SAGYO_CD_5")
                    wkNewRow("SAGYO_NM_5") = cloneRow("SAGYO_NM_5")
                    wkNewRow("CRT_USER") = cloneRow("CRT_USER")
                    wkNewRow("OUTKA_NO_M") = cloneRow("OUTKA_NO_M")
                    wkNewRow("GOODS_NM") = cloneRow("GOODS_NM")
                    wkNewRow("FREE_C08") = cloneRow("FREE_C08")
                    wkNewRow("IRIME") = cloneRow("IRIME")
                    wkNewRow("IRIME_UT") = cloneRow("IRIME_UT")
                    '-05-
                    wkNewRow("KONSU") = cloneRow("KONSU")
                    wkNewRow("HASU") = cloneRow("HASU")
                    wkNewRow("ALCTD_NB") = cloneRow("ALCTD_NB")
                    wkNewRow("NB_UT") = cloneRow("NB_UT")
                    wkNewRow("ALCTD_CAN_NB") = cloneRow("ALCTD_CAN_NB")
                    wkNewRow("FREE_C07") = cloneRow("FREE_C07")
                    wkNewRow("ALCTD_QT") = cloneRow("ALCTD_QT")
                    wkNewRow("ZAN_KONSU") = cloneRow("ZAN_KONSU")
                    wkNewRow("ZAN_HASU") = cloneRow("ZAN_HASU")
                    wkNewRow("SERIAL_NO") = cloneRow("SERIAL_NO")
                    '-06-
                    wkNewRow("PKG_NB") = cloneRow("PKG_NB")
                    wkNewRow("PKG_UT") = cloneRow("PKG_UT")
                    wkNewRow("ALCTD_KB") = cloneRow("ALCTD_KB")
                    wkNewRow("ALCTD_CAN_QT") = cloneRow("ALCTD_CAN_QT")
                    wkNewRow("REMARK_OUT") = cloneRow("REMARK_OUT")
                    wkNewRow("LOT_NO") = cloneRow("LOT_NO")
                    wkNewRow("LT_DATE") = cloneRow("LT_DATE")
                    wkNewRow("INKA_DATE") = cloneRow("INKA_DATE")
                    wkNewRow("REMARK_S") = cloneRow("REMARK_S")
                    wkNewRow("GOODS_COND_NM_1") = cloneRow("GOODS_COND_NM_1")
                    '-07-
                    wkNewRow("GOODS_COND_NM_2") = cloneRow("GOODS_COND_NM_2")
                    wkNewRow("GOODS_CD_CUST") = cloneRow("GOODS_CD_CUST")
                    wkNewRow("BETU_WT") = cloneRow("BETU_WT")
                    wkNewRow("CUST_ORD_NO_DTL") = cloneRow("CUST_ORD_NO_DTL")
                    wkNewRow("TOU_NO") = cloneRow("TOU_NO")
                    wkNewRow("SITU_NO") = cloneRow("SITU_NO")
                    wkNewRow("ZONE_CD") = cloneRow("ZONE_CD")
                    wkNewRow("LOCA") = cloneRow("LOCA")
                    wkNewRow("REMARK_M") = cloneRow("REMARK_M")
                    wkNewRow("SAGYO_MEI_REC_NO_1") = cloneRow("SAGYO_MEI_REC_NO_1")
                    '-08-
                    wkNewRow("SAGYO_MEI_CD_1") = cloneRow("SAGYO_MEI_CD_1")
                    wkNewRow("SAGYO_MEI_NM_1") = cloneRow("SAGYO_MEI_NM_1")
                    wkNewRow("SAGYO_MEI_REC_NO_2") = cloneRow("SAGYO_MEI_REC_NO_2")
                    wkNewRow("SAGYO_MEI_CD_2") = cloneRow("SAGYO_MEI_CD_2")
                    wkNewRow("SAGYO_MEI_NM_2") = cloneRow("SAGYO_MEI_NM_2")
                    wkNewRow("SAGYO_MEI_REC_NO_3") = cloneRow("SAGYO_MEI_REC_NO_3")
                    wkNewRow("SAGYO_MEI_CD_3") = cloneRow("SAGYO_MEI_CD_3")
                    wkNewRow("SAGYO_MEI_NM_3") = cloneRow("SAGYO_MEI_NM_3")
                    wkNewRow("SAGYO_MEI_REC_NO_4") = cloneRow("SAGYO_MEI_REC_NO_4")
                    wkNewRow("SAGYO_MEI_CD_4") = cloneRow("SAGYO_MEI_CD_4")
                    '-09-
                    wkNewRow("SAGYO_MEI_NM_4") = cloneRow("SAGYO_MEI_NM_4")
                    wkNewRow("SAGYO_MEI_REC_NO_5") = cloneRow("SAGYO_MEI_REC_NO_5")
                    wkNewRow("SAGYO_MEI_CD_5") = cloneRow("SAGYO_MEI_CD_5")
                    wkNewRow("SAGYO_MEI_NM_5") = cloneRow("SAGYO_MEI_NM_5")
                    wkNewRow("SAIHAKKO_FLG") = cloneRow("SAIHAKKO_FLG")
                    wkNewRow("OYA_CUST_GOODS_CD") = cloneRow("OYA_CUST_GOODS_CD")
                    wkNewRow("OYA_GOODS_NM") = cloneRow("OYA_GOODS_NM")
                    wkNewRow("OYA_KATA") = cloneRow("OYA_KATA")
                    wkNewRow("OYA_OUTKA_TTL_NB") = cloneRow("OYA_OUTKA_TTL_NB")
                    wkNewRow("SET_NAIYO") = cloneRow("SET_NAIYO")
                    '-10-
                    wkNewRow("OUTKO_DATE") = cloneRow("OUTKO_DATE")
                    wkNewRow("UNSOCO_BR_NM") = cloneRow("UNSOCO_BR_NM")
                    wkNewRow("CUST_NM_S_H") = cloneRow("CUST_NM_S_H")
                    wkNewRow("RPT_FLG") = cloneRow("RPT_FLG")
                    wkNewRow("GOODS_COND_NM_3") = cloneRow("GOODS_COND_NM_3")
                    wkNewRow("OUTKA_NO_S") = cloneRow("OUTKA_NO_S")
                    wkNewRow("WH_CD") = cloneRow("WH_CD")
                    wkNewRow("CUST_NAIYO_1") = cloneRow("CUST_NAIYO_1")
                    wkNewRow("CUST_NAIYO_2") = cloneRow("CUST_NAIYO_2")
                    wkNewRow("CUST_NAIYO_3") = cloneRow("CUST_NAIYO_3")
                    '-11-
                    wkNewRow("DEST_REMARK") = cloneRow("DEST_REMARK")
                    wkNewRow("DEST_SALES_CD") = cloneRow("DEST_SALES_CD")
                    wkNewRow("DEST_SALES_NM_L") = cloneRow("DEST_SALES_NM_L")
                    wkNewRow("DEST_SALES_NM_M") = cloneRow("DEST_SALES_NM_M")
                    wkNewRow("ALCTD_NB_HEADKEI") = wkALCTD_NB_HEADKEI
                    wkNewRow("ALCTD_QT_HEADKEI") = wkALCTD_QT_HEADKEI
                    wkNewRow("HINMEI") = cloneRow("HINMEI")
                    wkNewRow("NISUGATA") = cloneRow("NISUGATA")

                    ds.Tables("LMC520OUT").Rows.Add(wkNewRow)
                Next

                Return ds

                '2015.02.20 追加START 運送依頼書アルベマール対応
            Case "LMC759"

                Dim outDt As DataTable = ds.Tables("LMC520OUT")  'データ取得

                For Each lmc759Row As DataRow In outDt.Select(String.Concat("RPT_ID ='", "LMC759", "'"), "NRS_BR_CD ASC, PRINT_SORT ASC, OUTKA_NO_L ASC, OUTKA_NO_M ASC ")

                    If lmc759Row.RowState = DataRowState.Detached Then
                        Continue For
                    End If

                    Dim nrsBrCd As String = lmc759Row.Item("NRS_BR_CD").ToString()
                    Dim outkaNoL As String = lmc759Row.Item("OUTKA_NO_L").ToString()
                    Dim outkaNoM As String = lmc759Row.Item("OUTKA_NO_M").ToString()
                    Dim lotNo As String = lmc759Row.Item("LOT_NO").ToString()

                    Dim rowNo As Integer = 0

                    For Each tagRow As DataRow In outDt.Select(String.Concat("RPT_ID = '", "LMC759", "' AND NRS_BR_CD = '", nrsBrCd, "' AND OUTKA_NO_L = '", outkaNoL, "' AND OUTKA_NO_M = '", outkaNoM, "' AND LOT_NO = '", lotNo, "'"))

                        If rowNo = 0 Then
                            Dim rowSubNo As Integer = 0
                            For Each sumRow As DataRow In outDt.Select(String.Concat("RPT_ID = '", "LMC759", "' AND NRS_BR_CD = '", nrsBrCd, "' AND OUTKA_NO_L = '", outkaNoL, "' AND OUTKA_NO_M = '", outkaNoM, "' AND LOT_NO = '", lotNo, "'"))
                                If rowSubNo = 0 Then
                                    tagRow("KONSU") = Convert.ToDecimal(sumRow("KONSU"))
                                    tagRow("HASU") = Convert.ToDecimal(sumRow("HASU"))
                                    tagRow("ALCTD_QT") = Convert.ToDecimal(sumRow("ALCTD_QT"))
                                    tagRow("ZAN_KONSU") = Convert.ToDecimal(sumRow("ZAN_KONSU"))
                                    tagRow("ZAN_HASU") = Convert.ToDecimal(sumRow("ZAN_HASU"))
                                Else
                                    tagRow("KONSU") = Convert.ToDecimal(tagRow("KONSU")) + Convert.ToDecimal(sumRow("KONSU"))
                                    tagRow("HASU") = Convert.ToDecimal(tagRow("HASU")) + Convert.ToDecimal(sumRow("HASU"))
                                    tagRow("ALCTD_QT") = Convert.ToDecimal(tagRow("ALCTD_QT")) + Convert.ToDecimal(sumRow("ALCTD_QT"))
                                    tagRow("ZAN_KONSU") = Convert.ToDecimal(tagRow("ZAN_KONSU")) + Convert.ToDecimal(sumRow("ZAN_KONSU"))
                                    tagRow("ZAN_HASU") = Convert.ToDecimal(tagRow("ZAN_HASU")) + Convert.ToDecimal(sumRow("ZAN_HASU"))
                                End If
                                rowSubNo = rowNo + 1
                            Next
                            rowNo = rowNo + 1
                        Else
                            tagRow.Delete()
                        End If

                    Next

                Next

                Return ds
                '2015.02.20 追加END 運送依頼書アルベマール対応

            Case ""

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

    ''' <summary>
    '''　棟番号取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTouNo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTouNo", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        '2015/1/23
        'LMC520DACが肥大化したため521に分岐する処理を追加
        '
        '
        '
        Dim rptId As String = ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString

        Select Case rptId

            Case "LMC758", "LMC759"
                Return MyBase.CallDAC(Me._521Dac, "SelectPrintData", ds)

            Case "LMC812", "LMC814", "LMC845"
                Return MyBase.CallDAC(Me._812Dac, "SelectPrintData", ds)

            Case Else
                Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

        End Select


    End Function

    ''' <summary>
    ''' 棟別の帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function TOU_CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet
        'Private Function TOU_CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer) As DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        ''TODO 開発元の回答により対応
        ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
        ''1秒（1000ミリ秒）待機する
        'System.Threading.Thread.Sleep(1000)
        ''★★★ END

        '(2012.07.25)コンソリ業務対応
        '棟別の帳票CSV出力
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                              dr.Item("PTN_ID").ToString(), _
                              dr.Item("PTN_CD").ToString(), _
                              dr.Item("RPT_ID").ToString(), _
                              prtDs.Tables(tableName), _
                              ds.Tables(LMConst.RD), _
                              String.Empty, _
                              String.Empty, _
                              prtNb)

        'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
        '                      dr.Item("PTN_ID").ToString(), _
        '                      dr.Item("PTN_CD").ToString(), _
        '                      dr.Item("RPT_ID").ToString(), _
        '                      prtDs.Tables("LMC520OUT"), _
        '                      ds.Tables(LMConst.RD), _
        '                      String.Empty, _
        '                      String.Empty, _
        '                      prtNb)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet
        'Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer) As DataSet
        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        ''TODO 開発元の回答により対応
        ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
        ''1秒（1000ミリ秒）待機する
        'System.Threading.Thread.Sleep(1000)
        ''★★★ END

        Dim addFilePath As String = String.Empty
        Dim outkaNoL As String = String.Empty
        'Dim filePath As String = "/rexport [5,C:\LMUSER\"
        'Dim pdf As String = ".pdf] /rop"

        '(2012.07.25)コンソリ業務対応
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      dr.Item("RPT_ID").ToString(), _
                      prtDs.Tables(tableName), _
                      ds.Tables(LMConst.RD), _
                      String.Empty, _
                      String.Empty, _
                      prtNb)

        'Select Case dr.Item("NRS_BR_CD").ToString()

        '    '高取倉庫はPDF化し、分析表と出荷指示書を交互に出すため
        '    Case "93"

        '        For i As Integer = 0 To ds.Tables(LMConst.RD).Rows.Count - 1

        '            outkaNoL = prtDs.Tables(tableName).Rows(0).Item("OUTKA_NO_L").ToString()
        '            ds.Tables(LMConst.RD).Rows(i).Item("FILE_PATH") = String.Concat(ds.Tables(LMConst.RD).Rows(i).Item("FILE_PATH"), filePath, outkaNoL, pdf)

        '        Next

        '    Case Else



        'End Select



        'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
        '              dr.Item("PTN_ID").ToString(), _
        '              dr.Item("PTN_CD").ToString(), _
        '              dr.Item("RPT_ID").ToString(), _
        '              prtDs.Tables("LMC520OUT"), _
        '              ds.Tables(LMConst.RD), _
        '              String.Empty, _
        '              String.Empty, _
        '              prtNb)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT2(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet
        'Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer) As DataSet
        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        ''TODO 開発元の回答により対応
        ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
        ''1秒（1000ミリ秒）待機する
        'System.Threading.Thread.Sleep(1000)
        ''★★★ END

        Dim addFilePath As String = String.Empty
        Dim outkaNoL As String = String.Empty
        Dim filePath As String = "/rexport [5,C:\LMUSER\"
        Dim pdf As String = ".pdf] /rop"

        '(2012.07.25)コンソリ業務対応
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      dr.Item("RPT_ID").ToString(), _
                      prtDs.Tables(tableName), _
                      ds.Tables(LMConst.RD), _
                      String.Empty, _
                      String.Empty, _
                      prtNb)

        Select Case dr.Item("NRS_BR_CD").ToString()

            '高取倉庫はPDF化し、分析表と出荷指示書を交互に出すため
#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山) 
            Case "93", "96", "98"    'MOD 2019/03/25 要望管理005124
#Else
            Case "93", "96", "98", "F1", "F2", "F3" 'UPD 2022/10/19 033380   【LMS】FFEM足柄工場LMS導入 F2追加, 2023/11/28 039659 F3 追加

#End If

                For i As Integer = 0 To ds.Tables(LMConst.RD).Rows.Count - 1

                    outkaNoL = prtDs.Tables(tableName).Rows(0).Item("OUTKA_NO_L").ToString()
                    ds.Tables(LMConst.RD).Rows(i).Item("FILE_PATH") = String.Concat(ds.Tables(LMConst.RD).Rows(i).Item("FILE_PATH"), filePath, outkaNoL, pdf)

                Next

            Case Else



        End Select



        'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
        '              dr.Item("PTN_ID").ToString(), _
        '              dr.Item("PTN_CD").ToString(), _
        '              dr.Item("RPT_ID").ToString(), _
        '              prtDs.Tables("LMC520OUT"), _
        '              ds.Tables(LMConst.RD), _
        '              String.Empty, _
        '              String.Empty, _
        '              prtNb)

        Return ds

    End Function
#End Region

#End Region



End Class
