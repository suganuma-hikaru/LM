' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI150H : 物産アニマルヘルス倉庫内処理編集
'  作  成  者       :  [HORI]
' ==========================================================================

Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text
Imports System.IO
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI150ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI150H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI150F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI150V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI150G

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
    Private _LMIConV As LMIControlV

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMI150DS

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' キャンセルフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _CancelFlg As Boolean

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
        Dim frm As LMI150F = New LMI150F(Me)

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
        Me._G = New LMI150G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI150V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '数値コントロールの書式設定
        Call Me._G.SetNumberControl()

        'コンボボックスの生成
        Call Me._G.CreateComboBox()

        '引き渡されたデータによる処理判定
        Dim ds As DataSet = New LMI150DS()
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        Dim procType As String = "1"
        Dim eventShubetu As LMI150C.EventShubetsu = LMI150C.EventShubetsu.SHOKI

        If String.IsNullOrEmpty(prmDs.Tables(LMI150C.TABLE_NM_IN).Rows(0).Item("NRS_PROC_NO").ToString()) Then
            '新規登録（最初から編集モード）
            mode = DispMode.EDIT
            status = RecordStatus.NEW_REC
            eventShubetu = LMI150C.EventShubetsu.HENSHU
        Else
            mode = DispMode.VIEW
            status = RecordStatus.NOMAL_REC
            If "0".Equals(prmDs.Tables(LMI150C.TABLE_NM_IN).Rows(0).Item("PROC_TYPE").ToString()) Then
                '顧客→NRSのデータ（参照モードのみ）
                eventShubetu = LMI150C.EventShubetsu.VIEW_ONLY
            Else
                'NRS→顧客のデータ（編集へ移行可能な参照モード）
                eventShubetu = LMI150C.EventShubetsu.VIEW
            End If
            ds = Me.ServerAccess(prmDs, "SelectData")
        End If

        'シチュエーションラベルの設定
        Call Me._G.SetSituation(mode, status)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(eventShubetu)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()
        Call Me._G.SetInitValue(ds)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI150C.EventShubetsu, ByVal frm As LMI150F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI150C.EventShubetsu.HENSHU    '編集

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NOMAL_REC)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI150C.EventShubetsu.HENSHU)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()

                frm.imdProcDate.Focus()

            Case LMI150C.EventShubetsu.HOZON    '保存

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '保存処理
                rtnDs = Me.HozonData(frm)

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey(LMI150C.EventShubetsu.HOZON)

                    'コントロールの入力制御
                    Call Me._G.SetControlsStatus()
                    Exit Sub
                End If

                '画面のNRS処理番号が空ならセット
                If String.IsNullOrEmpty(frm.txtNrsProcNo.TextValue) Then
                    frm.txtNrsProcNo.TextValue = rtnDs.Tables(LMI150C.TABLE_NM_H_WHEDI_BAH).Rows(0).Item("NRS_PROC_NO").ToString()
                End If

                '最新の更新日に刷新
                frm.txtSysUpdDate.TextValue = rtnDs.Tables(LMI150C.TABLE_NM_H_WHEDI_BAH).Rows(0).Item("SYS_UPD_DATE_RESULT").ToString()
                frm.txtSysUpdTime.TextValue = rtnDs.Tables(LMI150C.TABLE_NM_H_WHEDI_BAH).Rows(0).Item("SYS_UPD_TIME_RESULT").ToString()

                '完了メッセージ表示
                MyBase.ShowMessage(frm, "G002", New String() {"保存処理", String.Empty})

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.VIEW, RecordStatus.NOMAL_REC)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI150C.EventShubetsu.VIEW)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()
        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI150F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        '「編集」処理
        Me.ActionControl(LMI150C.EventShubetsu.HENSHU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        '「保存」処理
        Me.ActionControl(LMI150C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        '「閉じる」処理
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI150F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' 在庫選択ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnZaikoSel_Click(ByRef frm As LMI150F)

        '入力チェック
        If Me._V.IsSingleCheck(LMI150C.EventShubetsu.ZAIKO_SEL) = False Then
            Exit Sub
        End If

        ''出庫倉庫種類が未決定ならば抜ける
        'If String.IsNullOrEmpty(frm.cmbOutkaWhType.SelectedValue.ToString()) Then
        '    Exit Sub
        'End If

        'パラメータ設定
        Dim ds As DataSet = New LMZ380DS()
        Dim dt As DataTable = ds.Tables(LMZ380C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim custCd As String() = frm.txtOutkaCustNo.TextValue.Split("-"c)
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("WH_TYPE") = frm.cmbOutkaWhType.SelectedValue.ToString()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Dim prm As LMFormData = Me._LMIConH.FormShow(ds, "LMZ380", "", True)

        '戻り
        If prm.ReturnFlg = True Then
            Dim retDr As DataRow = prm.ParamDataSet.Tables(LMZ380C.TABLE_NM_OUT).Rows(0)

            frm.txtGoodsCd.TextValue = retDr.Item("GOODS_CD").ToString()
            frm.txtGoodsCdNrs.TextValue = retDr.Item("GOODS_CD_NRS").ToString()
            frm.txtGoodsNm.TextValue = retDr.Item("GOODS_NM").ToString()
            frm.txtLotNo.TextValue = retDr.Item("LOT_NO").ToString()
            frm.imdLtDate.TextValue = retDr.Item("LT_DATE").ToString()
            frm.cmbBeforeGoodsRank.SelectedValue = retDr.Item("GOODS_RANK").ToString()
        End If

    End Sub

    ''' <summary>
    ''' 出庫倉庫種類コンボボックス選択時イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub ChangeCmbOutkaWhType(ByVal frm As LMI150F, ByVal e As System.EventArgs)

        'コントロールが読み取り専用ならば抜ける
        If frm.cmbOutkaWhType.ReadOnly Then
            Exit Sub
        End If

        '処理開始アクション
        Me._LMIConH.StartAction(frm)

        '対応する荷主情報を取得
        Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim outkaWhType As String = frm.cmbOutkaWhType.SelectedValue.ToString()
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim custNmL As String = String.Empty
        Dim custNmM As String = String.Empty

        If Not String.IsNullOrEmpty(outkaWhType) Then
            '区分マスタにて荷主コードに変換
            Dim where As String = String.Concat("KBN_GROUP_CD = 'B047' AND KBN_NM1 = '", outkaWhType, "'")
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(where)
            If dr.Count > 0 Then
                custCdL = dr(0).Item("KBN_NM2").ToString()
                custCdM = dr(0).Item("KBN_NM3").ToString()

                '荷主マスタから名称を取得
                where = String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND CUST_CD_L = '", custCdL, "' AND CUST_CD_M = '", custCdM, "'")
                dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(where)
                If dr.Count > 0 Then
                    custNmL = dr(0).Item("CUST_NM_L").ToString()
                    custNmM = dr(0).Item("CUST_NM_M").ToString()
                End If
            End If
        End If

        '荷主情報を設定
        frm.txtOutkaCustNo.TextValue = String.Concat(custCdL, If(String.IsNullOrEmpty(custCdM), "", "-"), custCdM)
        frm.txtOutkaCustNm.TextValue = String.Concat(custNmL, If(String.IsNullOrEmpty(custNmM), "", "　"), custNmM)

        '処理区分がステータス変更ならば入庫倉庫種類に複写
        If "1".Equals(frm.cmbProcKbn.SelectedValue.ToString()) Then
            frm.cmbInkaWhType.SelectedValue = frm.cmbOutkaWhType.SelectedValue
            frm.txtInkaCustNo.TextValue = frm.txtOutkaCustNo.TextValue
            frm.txtInkaCustNm.TextValue = frm.txtOutkaCustNm.TextValue
        End If

        '倉庫種類が変わった（＝荷主が変わった）なので商品情報をクリア
        frm.txtGoodsCd.TextValue = String.Empty
        frm.txtGoodsCdNrs.TextValue = String.Empty
        frm.txtGoodsNm.TextValue = String.Empty
        frm.txtLotNo.TextValue = String.Empty
        frm.numNb.Value = "0"
        frm.imdLtDate.TextValue = String.Empty

        '処理終了アクション
        Me._LMIConH.EndAction(frm)

    End Sub

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function KensakuData(ByVal frm As LMI150F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI150DS()

        'InDataSetの場合
        ''''Call Me.SetInDataKensaku(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KensakuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI150BLF", _
                                                         "SelectListData", _
                                                         rtDs, _
                                                         Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                         -1, _
                                                         False)

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
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function HozonData(ByVal frm As LMI150F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI150DS()

        'InDataSetの場合
        Call Me.SetInDataHozon(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HozonData")

        '更新処理
        Dim saveMode As String = String.Empty
        If frm.lblSituation.RecordStatus = RecordStatus.NEW_REC Then
            saveMode = "InsertSaveAction"
        Else
            saveMode = "UpdateSaveAction"
        End If

        '==== WSAクラス呼出 ====
        rtDs = Me.ServerAccess(rtDs, saveMode)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HozonData")

        Return rtDs

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(保存)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInDataHozon(ByVal frm As LMI150F, ByRef rtnDs As DataSet)

        Dim dr As DataRow = rtnDs.Tables(LMI150C.TABLE_NM_H_WHEDI_BAH).NewRow()

        dr("DEL_KB") = "0"
        dr("CRT_DATE") = ""     '後でセット
        dr("FILE_NAME") = ""
        dr("REC_NO") = "001"
        dr("GYO_NO") = "001"
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue

        If String.IsNullOrEmpty(frm.txtNrsProcNo.TextValue) Then
            Dim nrsProcNo As Integer = 0
            Dim noDs As DataSet = New LMI150DS()
            Dim noDr As DataRow = noDs.Tables(LMI150C.TABLE_NM_IN).NewRow()
            noDr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            noDs.Tables(LMI150C.TABLE_NM_IN).Rows.Add(noDr)
            noDs = Me.ServerAccess(noDs, "SelectNrsProcNo")
            If noDs.Tables(LMI150C.TABLE_NM_NRS_PROC_NO).Rows.Count > 0 Then
                nrsProcNo = Convert.ToInt32(noDs.Tables(LMI150C.TABLE_NM_NRS_PROC_NO).Rows(0).Item("NRS_PROC_NO").ToString())
            End If
            nrsProcNo += 1
            dr("NRS_PROC_NO") = nrsProcNo.ToString().PadLeft(9, CChar("0"))
        Else
            dr("NRS_PROC_NO") = frm.txtNrsProcNo.TextValue
        End If

        dr("PROC_TYPE") = frm.cmbProcType.SelectedValue
        dr("PROC_KBN") = frm.cmbProcKbn.SelectedValue
        dr("PRTFLG") = "0"
        dr("JISSEKI_FUYO") = If("01".Equals(frm.cmbJissekiFuyo.SelectedValue.ToString()), "0", "1")
        dr("OBIC_SHUBETU") = frm.txtObicShubetu.TextValue
        dr("OBIC_TORIHIKI_KBN") = frm.txtObicTorihikiKbn.TextValue
        dr("OBIC_DENP_NO") = frm.txtObicDenpNo.TextValue
        dr("OBIC_GYO_NO") = frm.txtObicGyoNo.TextValue
        dr("OBIC_DETAIL_NO") = frm.txtObicDetailNo.TextValue
        dr("PROC_DATE") = frm.imdProcDate.TextValue
        dr("OUTKA_WH_TYPE") = frm.cmbOutkaWhType.SelectedValue

        Dim outkaCustCd As String() = frm.txtOutkaCustNo.TextValue.Split("-"c)
        dr("OUTKA_CUST_CD_L") = outkaCustCd(0)
        dr("OUTKA_CUST_CD_M") = outkaCustCd(1)

        dr("INKA_WH_TYPE") = frm.cmbInkaWhType.SelectedValue

        Dim inkaCustCd As String() = frm.txtInkaCustNo.TextValue.Split("-"c)
        dr("INKA_CUST_CD_L") = inkaCustCd(0)
        dr("INKA_CUST_CD_M") = inkaCustCd(1)

        dr("BEFORE_GOODS_RANK") = frm.cmbBeforeGoodsRank.SelectedValue
        dr("AFTER_GOODS_RANK") = frm.cmbAfterGoodsRank.SelectedValue
        dr("GOODS_CD") = frm.txtGoodsCd.TextValue
        dr("GOODS_NM") = frm.txtGoodsNm.TextValue
        dr("NB") = frm.numNb.Value
        dr("LOT_NO") = frm.txtLotNo.TextValue
        dr("LT_DATE") = frm.imdLtDate.TextValue
        dr("REMARK") = frm.txtRemark.TextValue
        dr("YOBI1") = ""
        dr("YOBI2") = ""
        dr("YOBI3") = ""
        dr("YOBI4") = ""
        dr("YOBI5") = ""
        dr("RECORD_STATUS") = ""
        dr("JISSEKI_SHORI_FLG") = "1"
        dr("JISSEKI_USER") = ""
        dr("JISSEKI_DATE") = ""
        dr("JISSEKI_TIME") = ""
        dr("SEND_USER") = ""
        dr("SEND_DATE") = ""
        dr("SEND_TIME") = ""
        dr("DELETE_USER") = ""
        dr("DELETE_DATE") = ""
        dr("DELETE_TIME") = ""
        dr("DELETE_EDI_NO") = ""
        dr("DELETE_EDI_NO_CHU") = ""
        dr("PRT_USER") = ""
        dr("PRT_DATE") = ""
        dr("PRT_TIME") = ""
        dr("EDI_USER") = ""
        dr("EDI_DATE") = ""
        dr("EDI_TIME") = ""
        dr("UPD_USER") = ""
        dr("UPD_DATE") = ""
        dr("UPD_TIME") = ""
        dr("SYS_UPD_DATE_HAITA") = frm.txtSysUpdDate.TextValue
        dr("SYS_UPD_TIME_HAITA") = frm.txtSysUpdTime.TextValue
        dr("SYS_UPD_DATE_RESULT") = ""
        dr("SYS_UPD_TIME_RESULT") = ""

        rtnDs.Tables(LMI150C.TABLE_NM_H_WHEDI_BAH).Rows.Add(dr)

    End Sub

#End Region

#Region "外部メソッド"

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ServerAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Dim rtnDs As DataSet = MyBase.CallWSA("LMI150BLF", actionId, ds)

        Return rtnDs

    End Function

#End Region

#End Region 'Method

End Class
