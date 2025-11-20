' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI140H : 物産アニマルヘルス倉庫内処理検索
'  作  成  者       :  [HORI]
' ==========================================================================

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.Win.Utility

''' <summary>
''' LMI140ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI140H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI140V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI140G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI140F = New LMI140F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI140G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI140V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "内部処理"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI140C.EventShubetsu, ByVal frm As LMI140F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI140C.EventShubetsu.SINKI
                '新規

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '物産アニマルヘルス倉庫内処理編集(LMI150)の呼出
                Dim prmDs As DataSet = New LMI150DS
                Dim row As DataRow = prmDs.Tables(LMI150C.TABLE_NM_IN).NewRow
                row("NRS_PROC_NO") = String.Empty
                row("PROC_TYPE") = "1"
                prmDs.Tables(LMI150C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                LMFormNavigate.NextFormNavigate(Me, "LMI150", prm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI140C.EventShubetsu.JISSEKI
                '実績作成

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '単項目入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '関連項目入力チェック
                Dim errHashTable As Hashtable = New Hashtable
                Dim errDs As DataSet = Nothing
                Dim chkList As ArrayList = Me._V.getCheckList()

                errHashTable = Me._V.IsKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMI140_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '実績作成処理
                Call Me.JissekiSakusei(frm, eventShubetsu, errHashTable, errDs)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI140C.EventShubetsu.KENSAKU    '検索

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '検索処理
                rtnDs = Me.KensakuData(frm)

                'Spread表示処理
                Me.SetSpread(frm, rtnDs)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)
        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI140F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMI140F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        If rowNo > 0 Then

            '処理開始アクション
            Call Me._LMIConH.StartAction(frm)

            '権限チェック
            If Me._V.IsAuthorityChk(LMI140C.EventShubetsu.DOUBLE_CLICK) = False Then
                Call Me._LMIConH.EndAction(frm)
                Exit Sub
            End If

            '検索行ならば抜ける
            If frm.sprDetail.Sheets(0).ActiveRow.Index() = 0 Then
                Call Me._LMIConH.EndAction(frm)
                Exit Sub
            End If

            With frm.sprDetail.ActiveSheet

                'inputDataSet作成
                Dim prmDs As DataSet = Me.SetDataSetLMI150InData(frm, rowNo)
                Dim prm As LMFormData = New LMFormData()
                prm.ParamDataSet = prmDs

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMI150", prm)

            End With

            '処理終了アクション
            Call Me._LMIConH.EndAction(frm)

        End If

    End Sub

    ''' <summary>
    ''' データセット設定(LMI150引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI150InData(ByVal frm As LMI140F, ByVal rowno As Integer) As DataSet

        'DataSet設定
        Dim prmDs As DataSet = New LMI150DS()
        Dim row As DataRow = prmDs.Tables(LMI150C.TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            row("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(rowno, LMI140G.sprDetailDef.NRS_BR_CD.ColNo))
            row("NRS_PROC_NO") = Me._LMIconV.GetCellValue(.Cells(rowno, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo))
            row("PROC_TYPE") = Me._LMIconV.GetCellValue(.Cells(rowno, LMI140G.sprDetailDef.PROC_TYPE.ColNo))

        End With

        prmDs.Tables(LMI150C.TABLE_NM_IN).Rows.Add(row)

        Return prmDs

    End Function

#End Region '内部処理

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        '「新規」処理
        Me.ActionControl(LMI140C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        '「実績作成」処理
        Me.ActionControl(LMI140C.EventShubetsu.JISSEKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        '「検索」処理
        Me.ActionControl(LMI140C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI140F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI140F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByVal frm As LMI140F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "RowSelection")

        '「ダブルクリック」処理
        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiSakusei(ByVal frm As LMI140F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult = Me.ShowMessage(frm, "C001", New String() {"実績作成"})
        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables(LMI140C.TABLE_NM_GUIERROR).Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMI140DS()
        Call Me.SetDataJissekiSakusei(frm, rtDs, eventshubetsu, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSakusei")

        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMI140BLF", "JissekiSakusei", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then
            '処理失敗時、返却メッセージを設定
            Me.OutputExcel(frm)
            Exit Sub
        Else
            '処理成功時
            MyBase.ShowMessage(frm, "G002", New String() {"実績作成", String.Empty})
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSakusei")

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function KensakuData(ByVal frm As LMI140F) As DataSet

        ''SPREAD初期化
        frm.sprDetail.CrearSpread()

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI140DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KensakuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI140BLF", _
                                                         "SelectListData", _
                                                         rtDs, _
                                                         Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                         -1)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KensakuData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 検索結果をSpreadに表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SetSpread(ByVal frm As LMI140F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI140C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G016", New String() {Convert.ToString(frm.sprDetail.ActiveSheet.Rows.Count)})

    End Sub

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables(LMI140C.TABLE_NM_GUIERROR).Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables(LMI140C.TABLE_NM_GUIERROR).Rows(i)

            If String.IsNullOrEmpty(dr("PARA1").ToString()) = False Then
                prm1 = dr("PARA1").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA2").ToString()) = False Then
                prm2 = dr("PARA2").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA3").ToString()) = False Then
                prm3 = dr("PARA3").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA4").ToString()) = False Then
                prm4 = dr("PARA4").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA5").ToString()) = False Then
                prm5 = dr("PARA5").ToString()
            End If
            MyBase.SetMessageStore(dr("GUIDANCE_ID").ToString() _
                     , dr("MESSAGE_ID").ToString() _
                     , New String() {prm1, prm2, prm3, prm4, prm5} _
                     , dr("ROW_NO").ToString() _
                     , dr("KEY_NM").ToString() _
                     , dr("KEY_VALUE").ToString())

        Next

        Return ds

    End Function

    Private Sub OutputExcel(ByVal frm As LMI140F)

        MyBase.ShowMessage(frm, "E235")

        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMI140F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI140C.TABLE_NM_IN).NewRow()

        '検索条件　単項目
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("JISSEKI_SHORI_FLG_1") = If(frm.chkJissekiShoriFlg_1.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
        dr("JISSEKI_SHORI_FLG_2") = If(frm.chkJissekiShoriFlg_2.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
        dr("PROC_DATE_FROM") = frm.imdProcDateFrom.TextValue
        dr("PROC_DATE_TO") = frm.imdProcDateTo.TextValue
        dr("PROC_TYPE_1") = If(frm.chkProcType_1.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
        dr("PROC_TYPE_2") = If(frm.chkProcType_2.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
        dr("PROC_KBN_1") = If(frm.chkProcKbn_1.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)
        dr("PROC_KBN_2") = If(frm.chkProcKbn_2.Checked, LMConst.FLG.ON, LMConst.FLG.OFF)

        '検索条件　スプレッドシート
        With frm.sprDetail.ActiveSheet

            dr("JISSEKI_FUYO") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.JISSEKI_FUYO_NM.ColNo)).Trim()
            dr("OUTKA_WH_TYPE") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.OUTKA_WH_TYPE_NM.ColNo)).Trim()
            dr("INKA_WH_TYPE") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.INKA_WH_TYPE_NM.ColNo)).Trim()
            dr("BEFORE_GOODS_RANK") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.BEFORE_GOODS_RANK_NM.ColNo)).Trim()
            dr("AFTER_GOODS_RANK") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.AFTER_GOODS_RANK_NM.ColNo)).Trim()
            dr("GOODS_CD") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.GOODS_CD.ColNo)).Trim()
            dr("GOODS_NM") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.GOODS_NM.ColNo)).Trim()
            dr("LOT_NO") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.LOT_NO.ColNo)).Trim()
            dr("REMARK") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.REMARK.ColNo)).Trim()
            dr("NRS_PROC_NO") = Me._LMIconV.GetCellValue(.Cells(0, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo)).Trim()

        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMI140C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 実績作成データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSakusei(ByVal frm As LMI140F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprDetail.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                'LMI140_H_WHEDI_BAH
                dr = rtDs.Tables(LMI140C.TABLE_NM_H_WHEDI_BAH).NewRow()
                dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("NRS_PROC_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.SYS_UPD_TIME.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                rtDs.Tables(LMI140C.TABLE_NM_H_WHEDI_BAH).Rows.Add(dr)

                'LMI140_H_SENDWHEDI_BAH
                dr = rtDs.Tables(LMI140C.TABLE_NM_H_SENDWHEDI_BAH).NewRow()
                dr("DEL_KB") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.DEL_KB.ColNo))
                dr("CRT_DATE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.CRT_DATE.ColNo))
                dr("FILE_NAME") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.FILE_NAME.ColNo))
                dr("REC_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.REC_NO.ColNo))
                dr("GYO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.GYO_NO.ColNo))
                dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("NRS_PROC_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo))
                dr("PROC_TYPE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.PROC_TYPE.ColNo))
                dr("PROC_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.NRS_PROC_NO.ColNo))
                dr("PROC_GYO") = "001"
                dr("PROC_DATE") = DateFormatUtility.DeleteSlash(Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.PROC_DATE.ColNo)))
                dr("WH_TYPE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.OUTKA_WH_TYPE.ColNo))
                dr("BEFORE_GOODS_RANK") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.BEFORE_GOODS_RANK.ColNo))
                dr("AFTER_GOODS_RANK") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.AFTER_GOODS_RANK.ColNo))
                dr("GOODS_CD") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.GOODS_CD.ColNo))
                dr("GOODS_NM") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.GOODS_NM.ColNo))
                dr("NB") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.NB.ColNo))
                dr("LOT_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.LOT_NO.ColNo))
                dr("LT_DATE") = DateFormatUtility.DeleteSlash(Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.LT_DATE.ColNo)))
                dr("YOBI1") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.YOBI1.ColNo))
                dr("YOBI2") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.YOBI2.ColNo))
                dr("YOBI3") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.YOBI3.ColNo))
                dr("YOBI4") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.YOBI4.ColNo))
                dr("YOBI5") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI140G.sprDetailDef.YOBI5.ColNo))
                dr("RECORD_STATUS") = String.Empty
                dr("JISSEKI_SHORI_FLG") = "2"
                dr("JISSEKI_USER") = String.Empty
                dr("JISSEKI_DATE") = String.Empty
                dr("JISSEKI_TIME") = String.Empty
                dr("SEND_USER") = String.Empty
                dr("SEND_DATE") = String.Empty
                dr("SEND_TIME") = String.Empty
                dr("DELETE_USER") = String.Empty
                dr("DELETE_DATE") = String.Empty
                dr("DELETE_TIME") = String.Empty
                dr("DELETE_EDI_NO") = String.Empty
                dr("DELETE_EDI_NO_CHU") = String.Empty
                dr("UPD_USER") = String.Empty
                dr("UPD_DATE") = String.Empty
                dr("UPD_TIME") = String.Empty
                rtDs.Tables(LMI140C.TABLE_NM_H_SENDWHEDI_BAH).Rows.Add(dr)

            Next

        End With

    End Sub

#End Region 'DataSet設定

#End Region 'Method

End Class
