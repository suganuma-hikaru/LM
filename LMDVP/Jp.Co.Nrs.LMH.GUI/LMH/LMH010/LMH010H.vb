' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH010H : EDI入荷データ検索
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop
Imports Jp.Co.Nrs.LM.Base.GUI

''' <summary>
''' LMH010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "EDI荷主INDEX"

    Public Enum EdiCustIndex As Integer
        '2019/07/18 依頼番号:006754 add
        AgcW00440 = 140                     '(大阪)ＡＧＣ若狭化学
    End Enum

#End Region

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH010G

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconH As LMHControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Gamen共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

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
        Dim frm As LMH010F = New LMH010F(Me)

        'Validateクラスの設定
        Me._V = New LMH010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMH010G(Me, frm)

        Me._LMHconH = New LMHControlH(DirectCast(frm, Form), MyBase.GetPGID)

        Me._LMHconG = New LMHControlG(frm)

        Me._LMHconV = New LMHControlV(Me, DirectCast(frm, Form), Me._LMHconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMH010C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID())

        'コントロール個別設定
        Dim sysDate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetInitControl(Me.GetPGID(), frm, sysDate(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Me._G.SetInitValue(frm)

        '↓ データ取得の必要があればここにコーディングする。


        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "外部Method"
    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMH010C.EventShubetsu, ByVal frm As LMH010F)
        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        frm.lblCustNM_L.TextValue = String.Empty
        frm.lblCustNM_M.TextValue = String.Empty
        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData

        'パラメータ設定
        prm.ReturnFlg = False
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing
        Dim errHashTable As Hashtable = New Hashtable
        Dim errDs As DataSet = Nothing

        Dim chkList As ArrayList = Me._V.getCheckList()

        'イベント種別による分岐
        Select Case eventShubetsu
            '*****検索処理******
            Case LMH010C.EventShubetsu.KENSAKU

                '項目チェック
                If Me._V.IsKensakuSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '検索処理を行う
                Call Me.SelectListEvent(frm)

                'フォーカスの設定
                Call Me._G.SetFoucus()

                '*****入荷登録******
            Case LMH010C.EventShubetsu.TOROKU

                '項目チェック
                If Me._V.InkaTorokuSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.InkaTorokuKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.InkaToroku(frm, eventShubetsu, errHashTable, errDs)

                '*****実績作成******
            Case LMH010C.EventShubetsu.JISSEKI_SAKUSE

                If Me._V.JissekiSakuseiSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                errHashTable = Me._V.JissekiSakuseiKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub

                End If

                Call Me.JissekiSakusei(frm, eventShubetsu, errHashTable, errDs)

                '*****紐付け*****
            Case LMH010C.EventShubetsu.HIMODUKE

                '項目チェック
                If Me._V.IsHimodukeSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.IsHimodukeKanrenCheck(eventShubetsu, Me._G)


                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                '=== DOTO : inputDataSet作成 ===='
                prmDs = Me.SetDataSetLMH070InData(frm, Nothing)
                prm.ParamDataSet = prmDs

                'モーダレスなので画面ロック必要なし
                Call Me._LMHconH.EndAction(frm) '終了処理

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH070", prm)

                'メッセージの表示
                Me.ShowMessage(frm, "G007")

                '*****EDI取消******
            Case LMH010C.EventShubetsu.EDI_TORIKESI

                '項目チェック
                If Me._V.DelEdiSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.DelEdiKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub

                End If

                Call Me.EdiTorikesi(frm, eventShubetsu, errHashTable, errDs)

                '2015.04.13 追加START
                '*****取込処理******
            Case LMH010C.EventShubetsu.TORIKOMI

                '単項目チェック
                If Me._V.IsTorikomiSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                'キャッシュから情報取得
                Dim rtDs As DataSet = New LMH010DS()
                rtDs = Me.SetCachedSemiEDI(frm)

                'キャッシュから情報を取得できなかった場合処理終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                'チェックのスルー判定
                If rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0).Item("FILE_CHICE_KBN").ToString().Equals("01") = False Then

                    '関連チェック
                    If Me._V.IsTorikomiKanrenCheck(rtDs) = False Then
                        Call Me._LMHconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                End If

                Select Case rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0).Item("FILE_CHICE_KBN").ToString
                    Case "01"
                        '取込処理
                        Call Me.TorikomiStanderdEdition(frm, eventShubetsu, errDs, rtDs)
                        'Case Else
                        '    '取込処理
                        '    Call Me.Torikomi(frm, eventShubetsu, errDs, rtDs)
                End Select
                '2015.04.13 追加END

                '*****実績取消******
            Case LMH010C.EventShubetsu.JISSEKI_TORIKESI

                '項目チェック
                If Me._V.JissekiTorikesiSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JissekiTorikesiKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub

                End If

                Call Me.JissekiTorikesi(frm, eventShubetsu, errHashTable, errDs)

                '*****(実行)EDI取消⇒未登録******
            Case LMH010C.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbJikkou.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.TorikesiMitouroku(frm, eventShubetsu)

                '*****(実行)報告用EDI取消******
            Case LMH010C.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbJikkou.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.HoukokuEdiTorikesi(frm, eventShubetsu)

                '*****(実行)実績作成済⇒実績未******
            Case LMH010C.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbJikkou.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit sub
                End If

                Call Me.SakuseizumiJissekimi(frm, eventShubetsu)

                '*****(実行)実績送信済⇒実績未******
            Case LMH010C.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbJikkou.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.SousinzumiJissekimi(frm, eventShubetsu)

                '*****(実行)実績送信済⇒送信未******
            Case LMH010C.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbJikkou.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.SousinSousinmi(frm, eventShubetsu)
                '*****(実行)入荷取消⇒未登録******
            Case LMH010C.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbJikkou.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.Mitouroku(frm, eventShubetsu)

                '*****初期荷主変更******
            Case LMH010C.EventShubetsu.DEF_CUST

                'inputDataSet作成
                prmDs = Me.SetDataSetLMZ010InData(frm)
                prm.ParamDataSet = prmDs

                '初回荷主変更Popup呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ010", prm)

                'メッセージの表示
                Me.ShowMessage(frm, "G007")

                '戻り処理
                If prm.ReturnFlg = True Then
                    With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD_L.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.txtCustCD_M.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                        frm.lblCustNM_L.TextValue = .Item("CUST_NM_L").ToString    '荷主名（大）
                        frm.lblCustNM_M.TextValue = .Item("CUST_NM_M").ToString    '荷主名（中）
                    End With
                End If

                '*****印刷処理******
            Case LMH010C.EventShubetsu.PRINT

                Dim printShubetsu As Integer = 0
                If String.IsNullOrEmpty(frm.cmbPrint.SelectedValue.ToString()) = False Then
                    printShubetsu = Convert.ToInt32(frm.cmbPrint.SelectedValue)
                End If

                '必須チェック
                If Me._V.printSingleCheck(eventShubetsu, printShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '項目チェック
                If Me._V.IsKensakuSingleCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '印刷処理を行う
                Call Me.SelectPrintEvent(frm, printShubetsu, eventShubetsu)

                '*****CSV作成・出力処理******
                '2012.03.13 大阪対応START
            Case LMH010C.EventShubetsu.OUTPUTPRINT

                Dim outPutKb As String = frm.cmbOutput.SelectedValue.ToString()
                '要望番号1061 2012.05.15 追加START
                Dim outPutKb2 As String = frm.cmbOutputKb.SelectedValue.ToString()
                '要望番号1061 2012.05.15 追加END

                '要望番号1061 2012.05.15 修正START
                '項目チェック
                If Me._V.IsOutputPrintCheck(outPutKb, outPutKb2) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If
                '要望番号1061 2012.05.15 修正END

                '印刷処理を行う
                Call Me.OutputPrint(frm)
                '2012.03.13 大阪対応END

            Case LMH010C.EventShubetsu.MASTER

                Call Me.MasterRefer(frm, prm, LMH010C.EventShubetsu.MASTER)

                'START SHINOHARA 要望番号513
            Case LMH010C.EventShubetsu.ENTER

                Call Me.MasterRefer(frm, prm, LMH010C.EventShubetsu.ENTER)

                'END SHINOHARA 要望番号513

                '*****ダブルクリック******
            Case LMH010C.EventShubetsu.DOUBLE_CLICK

                '検索行をクリックしたのがどうかをチェックする
                If eventShubetsu = LMH010C.EventShubetsu.DOUBLE_CLICK Then
                    If Me.DoubleClickChk(frm) = False Then
                        Call Me._LMHconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If
                End If

                'モーダレスなので画面ロック必要なし
                Call Me._LMHconH.EndAction(frm) '終了処理

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH020", prm)

            Case LMH010C.EventShubetsu.COA_TOUROKU
                '*****分析票取り込み処理******
                '2012.09.11 富士フイルム対応START

                '項目チェック
                If Me._V.IsCoaTourokuCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '取り込処理を行う
                Call Me.CoaTouroku(frm)

            Case LMH010C.EventShubetsu.INKA_CONF_TORIKOMI
                '*****入荷確認データ取り込み処理******
                '2012.11.30 ユーティーアイ対応START

                '入荷確認ファイルのパス
#If False Then  'UPD 2019/01/11 依頼番号 : 004191   【LMS】東レDOW・デュポン分社化_DSV入荷EDI対応
                Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E030' AND ", _
                                                                                                                "KBN_CD = '02'"))

#Else
                '営業所・荷主ごと設定出来るように変更
                Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
                Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString()    '荷主コード(大)
                Dim custCdM As String = frm.txtCustCD_M.TextValue.ToString()    '荷主コード(中)

                Dim sSQL As String = String.Concat("KBN_GROUP_CD = 'E030' AND  KBN_NM6 = '" & brCd.ToString, "' AND KBN_NM7 = '" & custCdL.ToString, "' AND KBN_NM8 = '" & custCdM.ToString, "'")
                Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sSQL)

#End If

                If kbnDr.Length = 0 Then
                    Me.SetMessage("E223", New String() {"入荷確認ファイル読み込み元フォルダ"})
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub

                End If

                Dim rcvPath As String = kbnDr(0).Item("KBN_NM2").ToString().Trim()           '入荷確認ファイル格納先
                Dim backupPath As String = kbnDr(0).Item("KBN_NM3").ToString().Trim()        'BACKUPファイル格納先
                Dim errorPath As String = kbnDr(0).Item("KBN_NM4").ToString().Trim()         'ERRORファイル格納先
                Dim rcvExtention As String = kbnDr(0).Item("KBN_NM5").ToString().Trim()      'ファイル拡張子

                If String.IsNullOrEmpty(rcvPath) = True Then
                    Me.SetMessage("E237", New String() {"入荷確認ファイル読み込み元フォルダパスが空"})
                End If

                '項目チェック
                If Me._V.IsInkaConfTourokuCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '入荷確認ファイル取込処理を行う
                Call Me.InkaConfTouroku(frm, rcvPath, backupPath, errorPath, rcvExtention)

            Case LMH010C.EventShubetsu.CONF_DEL
                '*****実行処理(確認データ削除(UTI)処理)******

                '項目チェック
                If Me._V.IsInkaConfDelCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                prmDs = Me.SetDataSetLMH080InData(frm, Nothing, MyBase.GetPGID())
                prm.ParamDataSet = prmDs

                'モーダレスなので画面ロック必要なし
                Call Me._LMHconH.EndAction(frm) '終了処理

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH080", prm)

                'メッセージの表示
                Me.ShowMessage(frm, "G007")

                '2015.09.03 tsunehira add
                '荷主一括変更
            Case LMH010C.EventShubetsu.BULK_CUST_CHANGE

                '項目チェック
                If Me._V.IsChgCheck(Me._G) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '未登録データチェック
                errHashTable = Me._V.InkaChgKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                '画面制御
                Call Me.SelectBulkChgEvent(frm, eventShubetsu, errHashTable)

                '全行エラーの場合処理終了
                'If chkList.Count = errHashTable.Count Then
                '    Call Me._LMHconH.EndAction(frm)
                '    Exit Sub
                'Else

                'End If

            Case LMH010C.EventShubetsu.JIKKOU_TRANSFER_COND_M

                ' エラークリア
                errDs = New LMH010DS()
                MyBase.ClearMessageStoreData()
                MyBase.ClearMessageData()

                '項目チェック
                If Me._V.TransferCondMSingleCheck(Me._G) = False Then
                    Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.TransferCondMKanrenCheck(eventShubetsu, errDs, Me._G)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If (errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Count > 0) Then
                        Me.ExcelErrorSet(errDs)
                        Me.OutputExcel(frm)
                    End If

                    Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                ' M品振替
                Me.TransferCondM(frm, eventShubetsu, errHashTable, errDs)

                'ADD START 2019/9/12 依頼番号:007111
            Case LMH010C.EventShubetsu.JIKKOU_GENPIN_PRINT
                '現品票印刷

                '項目チェック
                If Me._V.JikkouGenpinPrint(Me._G, eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

#If True Then   'ADD 2019/12/17
                If Me._V.GenpinPrintInDataCHK(Me._G, eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub

                End If


#End If
                '印刷処理を行う
                Call Me.SelectPrintEvent(frm, Integer.MinValue, eventShubetsu)

            Case LMH010C.EventShubetsu.JIKKOU_GENPIN_REPRINT
                '現品票再印刷

                '項目チェック
                If Me._V.JikkouGenpinPrint(Me._G, eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '画面遷移時のパラメータの設定
                Dim ds As DataSet = New LMH090DS()
                Dim dt As DataTable = ds.Tables("LMH090_IN")
                Dim dr As DataRow = dt.NewRow()

                If chkList.Count >= 1 Then
                    '選択行がある場合、選択行より
                    With frm.sprEdiList.ActiveSheet
                        Dim rowNo As Integer = Convert.ToInt32(chkList(0))
                        dr.Item("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(rowNo, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                        dr.Item("WH_CD") = Me._LMHconV.GetCellValue(.Cells(rowNo, _G.sprEdiListDef.WH_CD.ColNo))
                        dr.Item("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(rowNo, _G.sprEdiListDef.CUST_CD_L.ColNo))
                        dr.Item("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(rowNo, _G.sprEdiListDef.CUST_CD_M.ColNo))
                        dr.Item("OUTKA_FROM_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(rowNo, _G.sprEdiListDef.ORDER_NO.ColNo))
                    End With
                Else
                    '選択行がない場合、ヘッダ項目より
                    dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                    dr.Item("WH_CD") = frm.cmbWare.SelectedValue.ToString
                    dr.Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
                    dr.Item("CUST_CD_M") = If(frm.txtCustCD_M.TextValue <> "", frm.txtCustCD_M.TextValue, "00")
                    'dr.Item("OUTKA_FROM_ORD_NO") = ""

                End If

                dt.Rows.Add(dr)

                prm.ParamDataSet = ds

                '現品票印刷画面に遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH090", prm)

                'エラーがある場合、メッセージ表示
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                Else
                    MyBase.ShowMessage(frm, "G007")
                End If

                'ADD END 2019/9/12 依頼番号:007111

        End Select

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '外部Method

#Region "内部Method"

#Region "検索処理"
    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMH010F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()

        'SPREAD(表示行)初期化
        frm.sprEdiList.CrearSpread()

        If Me.SetDataSelectInData(frm, rtDs) = False Then
            MyBase.ShowMessage(frm, "E361")
            Me._LMHconV.SetErrorControl(frm.txtCustCD_L)
            Exit Sub
        End If


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMHconH.CallWSAAction(DirectCast(frm, Form), _
                                                "LMH010BLF", "SelectListData", rtDs _
                                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))) _
                                                 , Convert.ToInt32(Convert.ToDouble( _
                                                 MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))


        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

#End Region 'Spread検索処理

#Region "入荷登録処理"
    ''' <summary>
    ''' 入荷登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InkaToroku(ByVal frm As LMH010F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataInkaData(frm, rtDs, eventshubetsu, errHashtable, 0)

        Dim autoMatomeF As String = String.Empty

        autoMatomeF = rtDs.Tables(LMH010C.TABLE_NM_IN).Rows(0)("AUTO_MATOME_FLG").ToString()

        If autoMatomeF.Equals("1") = True Then

            '要望番号:1652 terakawa 2012.12.03 Start
            'rtn = Me.ShowMessage(frm, "W160", New String() {"自動まとめ処理"})
            rtn = Me.ShowMessage(frm, "W224", New String() {"自動まとめ処理"})
            '要望番号:1652 terakawa 2012.12.03 End

            '2011.09.30 修正 OK⇒Yesに変更  
            If rtn = MsgBoxResult.Yes Then
                '2011.09.30 追加 START GUI側エラーが出力されない対応
                'エラーをExcelに出力
                If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If
                '2011.09.30 追加 END 

                'ADD 2018/04/27 Start
                'まとめで入荷日・荷主が同じかチェックするか？
                '荷主明細取得
                Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
                Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString()    '荷主コード(大)
                Dim kbnDr92() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '9E' AND SET_NAIYO = '01' ")


                If kbnDr92.Length > 0 Then
                    '入荷日・荷主が同じかチェック
                    If Me._V.IsMatomeChk(Me._G) = False Then

                        'メッセージの表示
                        Me.ShowMessage(frm, "E144", New String() {"入荷日または荷主CD"})

                        Me._LMHconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If
                End If

                'ADD 2018/04/27 End

            ElseIf rtn = MsgBoxResult.No Then

                'まとめ処理対象外とする
                Call Me.SetDataInkaData(frm, rtDs, eventshubetsu, errHashtable, 1)
                'エラーをExcelに出力
                If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If

            ElseIf rtn = MsgBoxResult.Cancel Then
                Call MyBase.ShowMessage(frm, "G007")
                Exit Sub

            End If

        Else

            rtn = Me.ShowMessage(frm, "C001", New String() {"入荷登録"})

            If rtn = MsgBoxResult.Ok Then
                'エラーをExcelに出力
                If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If
            ElseIf rtn = MsgBoxResult.Cancel Then
                Me.ShowMessage(frm, "G007")
                Exit Sub
            End If
        End If


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "InkaToroku")

        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH010BLF", "InkaToroku", rtDs)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

            If rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtnDs, frm, eventshubetsu)
            End If

        ElseIf rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

            'ワーニングが設定されている場合
            Call Me.CallWarning(rtnDs, frm, eventshubetsu)

        Else
            '入荷登録処理成功時処理
            Call Me.SuccessInkaToroku(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "InkaToroku")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '入荷登録処理

#Region "実績作成処理"
    ''' <summary>
    ''' 実績作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiSakusei(ByVal frm As LMH010F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績作成"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataJissekiSakusei(frm, rtDs, eventshubetsu, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSakusei")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "JissekiSakusei", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then
            '入荷登録処理失敗時、返却メッセージを設定
            Me.OutputExcel(frm)

            If rtDs.Tables("WARNING_DTL").Rows.Count > 0 Then
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtDs, frm, eventshubetsu)
            End If

            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            If rtDs.Tables("WARNING_DTL").Rows.Count > 0 Then
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtDs, frm, eventshubetsu)
            Else

                'EDI取消処理成功時処理
                Call Me.SuccessJissekiSakusei(frm, rtDs)
            End If
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSakusei")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '実績作成処理

#Region "EDI取消処理"
    ''' <summary>
    ''' EDI取消
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EdiTorikesi(ByVal frm As LMH010F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"EDI取消"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataEdiTorikeshi(frm, rtDs, eventshubetsu, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EdiTorikesi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "EdiTorikesi", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then
            '入荷登録処理失敗時、返却メッセージを設定
            Me.OutputExcel(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else

            'EDI取消処理成功時処理
            Call Me.SuccessEdiTorikesi(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EdiTorikesi")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region ' EDI取消処理

#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiTorikesi(ByVal frm As LMH010F, ByVal eventshubetsu As Integer, ByVal errHashTable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績取消"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMH010_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataJissekiTorikeshi(frm, rtDs, eventshubetsu, errHashTable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiTorikesi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "JissekiTorikesi", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist() = True Then
            '実績取消処理失敗時、返却メッセージを設定
            Me.OutputExcel(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else

            '実績取消処理成功時処理
            Call Me.SuccessJissekiTorikesi(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiTorikesi")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region ' 実績取消処理

#Region "EDI取消⇒未登録"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TorikesiMitouroku(ByVal frm As LMH010F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"EDI取消⇒未登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataTorikesiMitouroku(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikesiMitouroku")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "TorikesiMitouroku", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '入荷登録処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '入荷登録処理成功時処理
            Call Me.SuccessTorikesiMitouroku(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikesiMitouroku")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region 'EDI取消⇒未登録

#Region "実績報告用EDIデータ取消"
    ''' <summary>
    ''' 実績報告用EDIデータ取消
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HoukokuEdiTorikesi(ByVal frm As LMH010F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績報告用EDIデータ取消"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataHoukokuEdiTorikeshi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HoukokuEdiTorikesi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "HoukokuEdiTorikesi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績報告用EDIデータ取消処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績報告用EDIデータ取消処理成功時処理
            Call Me.SuccessHoukokuEdiTorikesi(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "HoukokuEdiTorikesi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績報告用EDIデータ取消

#Region "実績作成済⇒実績未"
    ''' <summary>
    ''' 実績作成済⇒実績未
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SakuseizumiJissekimi(ByVal frm As LMH010F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績作成済⇒実績未"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataJissekimi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SakuseizumiJissekimi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "SakuseizumiJissekimi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績作成済⇒実績未処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績作成済⇒実績未処理成功時処理
            Call Me.SuccessJikkou(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SakuseizumiJissekimi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績作成済⇒実績未

#Region "実績送信済⇒実績未"
    ''' <summary>
    ''' 実績送信済⇒実績未
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SousinzumiJissekimi(ByVal frm As LMH010F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績送信済⇒実績未"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataJissekimi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SousinzumiJissekimi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "SousinzumiJissekimi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績送信済⇒実績未処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績送信済⇒実績未処理成功時処理
            Call Me.SuccessJikkou(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SousinzumiJissekimi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績送信済⇒実績未

#Region "実績送信済⇒送信未"
    ''' <summary>
    ''' 実績送信済⇒送信未
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SousinSousinmi(ByVal frm As LMH010F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績送信済⇒送信待"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataSousinmi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SousinSousinmi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "SousinSousinmi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績送信済⇒送信未処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績送信済⇒送信未処理成功時処理
            Call Me.SuccessJikkou(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SousinSousinmi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績送信済⇒送信未

#Region "入荷取消⇒未登録"
    ''' <summary>
    '''入荷取消⇒未登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Mitouroku(ByVal frm As LMH010F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"入荷取消⇒未登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Call Me.SetDataTourokumi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Mitouroku")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH010BLF", "Mitouroku", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '入荷取消⇒未登録失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '入荷取消⇒未登録処理成功時処理
            Call Me.SuccessJikkou(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Mitouroku")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '入荷取消⇒未登録

#Region "印刷処理"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectPrintEvent(ByVal frm As LMH010F, ByVal printShubetsu As Integer, _
                                 ByVal eventShubetsu As LMH010C.EventShubetsu)

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()

        'ADD START 2019/9/12 依頼番号:007111
        If eventShubetsu = LMH010C.EventShubetsu.JIKKOU_GENPIN_PRINT Then
            'データセット設定
            SetGenpinPrintInData(frm, rtDs, eventShubetsu)

            GoTo LABEL_CALL_WSA
        End If
        'ADD END   2019/9/12 依頼番号:007111

        Select Case printShubetsu

            Case DirectCast(LMH010C.Print_KBN.EDIINKACHECKLIST, Integer) _
                , DirectCast(LMH010C.Print_KBN.EDIINKACHECKLISTAXALTA, Integer)

                If Me.SetDataSelectInData(frm, rtDs, printShubetsu, eventShubetsu) = False Then
                    MyBase.ShowMessage(frm, "E361")
                    Me._LMHconV.SetErrorControl(frm.txtCustCD_L)
                    Exit Sub

                ElseIf 0 = rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Count Then
                    Exit Sub

                End If

            Case Else

                Exit Sub

        End Select


LABEL_CALL_WSA:  'ADD 2019/9/12 依頼番号:007111

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH010BLF", "PrintData", rtDs)

        'エラー帳票出力の判定
        If MyBase.IsMessageStoreExist() = True Then
            Call Me.OutputExcel(frm)
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintData")

        '処理終了メッセージの表示
        If MyBase.IsMessageStoreExist Then  'ADD START 2019/9/12 依頼番号:007111
        ElseIf MyBase.IsMessageExist Then
            MyBase.ShowMessage(frm)
        Else                                'ADD END   2019/9/12 依頼番号:007111    
            MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})
        End If                              'ADD       2019/9/12 依頼番号:007111

        'プレビュー判定 
        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If 0 < prevDt.Rows.Count Then

            'プレビューの生成
            Dim prevFrm As RDViewer = New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            'フォーカス設定
            prevFrm.Focus()

        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

#End Region 'Spread検索処理

#Region "マスタ参照"

    Private Sub MasterRefer(ByVal frm As LMH010F, ByVal prm As LMFormData, ByVal actionType As LMH010C.EventShubetsu)


        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.ActiveControl.Name()

        Select Case objNm
            Case "txtCustCD_L", "txtCustCD_M"
                Call Me.ShowPopup(frm, objNm, prm, actionType)
                Me.ShowMessage(frm, "G007")
            Case Else
                MyBase.ShowMessage(frm, "G005")
        End Select

        If prm.ReturnFlg = True Then
            Call SetMstResult(frm, objNm, prm)
        End If

    End Sub


#End Region 'マスタ参照

    '2015.09.03 tsunehira add
#Region "一括変更"
    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectBulkChgEvent(ByVal frm As LMH010F, ByVal eventShubetsu As LMH010C.EventShubetsu, ByVal errHashtable As Hashtable)

        Dim ds As DataSet = New LMH010DS
        ds = Me.SetDataBulkChgData(frm, eventShubetsu, ds, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "BulkChg")

        '==== WSAクラス呼出 ===
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH010BLF", "SetDataEDI_L", ds)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

            If rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtnDs, frm, eventShubetsu)
            End If

        ElseIf rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

            'ワーニングが設定されている場合
            Call Me.CallWarning(rtnDs, frm, eventShubetsu)

        Else
            '一括変更処理成功時処理
            Call Me.SuccessBulkChg(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "BulkChg")

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

#End Region '一括変更

#Region "ダブルクリック"

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMH010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        If rowNo > 0 Then

            '処理開始アクション
            Call Me._LMHconH.StartAction(frm)

            '権限チェック
            If Me._V.IsAuthorityChk(LMH010C.EventShubetsu.DOUBLE_CLICK) = False Then
                Call Me._LMHconH.EndAction(frm)
                Exit Sub
            End If

            'ダブルクリックの場合、検索行をクリックしたのがどうかをチェックする
            If Me.DoubleClickChk(frm) = False Then
                Call Me._LMHconH.EndAction(frm)
                Exit Sub
            End If

            'inputDataSet作成
            Dim prmDs As DataSet = Me.SetDataSetLMH020InData(frm, rowNo)
            Dim prm As LMFormData = New LMFormData()
            prm.ParamDataSet = prmDs
            prm.RecStatus = RecordStatus.NOMAL_REC

            '画面遷移
            LMFormNavigate.NextFormNavigate(Me, "LMH020", prm)

            'メッセージの表示
            Me.ShowMessage(frm, "G007")

            '処理終了アクション
            Call Me._LMHconH.EndAction(frm)

        End If

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DoubleClickChk(ByVal frm As LMH010F) As Boolean

        'クリックした行が検索行の場合
        If frm.sprEdiList.Sheets(0).ActiveRow.Index() = 0 Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "検索,印刷条件データセット"
    ''' <summary>
    ''' 検索,印刷条件データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataSelectInData(ByVal frm As LMH010F, ByVal rtDs As DataSet, _
                                         Optional ByVal printShubetsu As Integer = 0, _
                                         Optional ByVal eventShubetsu As Integer = 0) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
        Dim ediCustDrs As DataRow()
        Dim ediIndex As String = String.Empty
        Dim ediRcvHed As String = String.Empty
        Dim ediSend As String = String.Empty
        '▼▼▼二次
        Dim ediRcvDtl As String = String.Empty
        Dim inOutFlg As String = String.Empty       '2012.02.25大阪対応ADD
        '▲▲▲二次

        '検索,印刷条件　単項目部

        '進捗区分（未登録）
        dr("EDIINKA_STATE_KB1") = Replace(frm.chkStaMitouroku.GetBinaryValue, "0", String.Empty)

        '進捗区分（登録済）
        dr("EDIINKA_STATE_KB2") = Replace(frm.chkStaTourokuzumi.GetBinaryValue, "0", String.Empty)

        '進捗区分（実績未）
        dr("EDIINKA_STATE_KB3") = Replace(frm.chkStaJissekimi.GetBinaryValue, "0", String.Empty)

        '進捗区分（実績作成済）
        dr("EDIINKA_STATE_KB4") = Replace(frm.chkStaJissekizumi1.GetBinaryValue, "0", String.Empty)

        '進捗区分（実績送信済）
        dr("EDIINKA_STATE_KB5") = Replace(frm.chkStaJissekizumi2.GetBinaryValue, "0", String.Empty)

        '進捗区分（赤データ）
        dr("EDIINKA_STATE_KB6") = Replace(frm.chkstaRedData.GetBinaryValue, "0", String.Empty)

        ''進捗区分（全部）
        'dr("EDIINKA_STATE_KB7") = Replace(frm.chkStaAll.GetBinaryValue, "0", String.Empty)

        '進捗区分（取消のみ）
        dr("EDIINKA_STATE_KB8") = Replace(frm.chkStaTorikesi.GetBinaryValue, "0", String.Empty)

        '営業所コード
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue

        '倉庫コード
        dr("WH_CD") = frm.cmbWare.SelectedValue

        '荷主コードL
        dr("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim()

        '荷主コードM
        dr("CUST_CD_M") = frm.txtCustCD_M.TextValue.Trim()

        '担当者コード
        dr("TANTO_CD") = frm.txtTantouCd.TextValue.Trim()

        ''日付選択区分
        'dr("SELECT_DATE_KB") = frm.cmbSelectDate.SelectedValue

        'EDI取込日（FROM）
        dr("TORIKOMI_DATE_FROM") = frm.imdEdiDateFrom.TextValue

        'EDI取込日（TO）
        dr("TORIKOMI_DATE_TO") = frm.imdEdiDateTo.TextValue

        '入荷日（FROM）
        dr("INKA_DATE_FROM") = frm.imdInkaDateFrom.TextValue

        '入荷日（TO）
        dr("INKA_DATE_TO") = frm.imdInkaDateTo.TextValue

        '検索,印刷条件　入力部（スプレッド）
        With frm.sprEdiList.ActiveSheet

            dr("JYOTAI_KB") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.JOTAI.ColNo)).Trim()
            dr("HORYU_KB") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.HORYU.ColNo)).Trim()
            dr("OUTKA_FROM_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.ORDER_NO.ColNo)).Trim()
            dr("CUST_NM") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.CUST_NM.ColNo)).Trim()
            '(2013.01.11)要望番号1700 -- START --
            'dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH010G.sprEdiListDef.ITEM_NM.ColNo)).Trim()
            'dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH010G.sprEdiListDef.ITEM_NM.ColNo)).Trim().Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
            dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.ITEM_NM.ColNo)).Trim().Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")    '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
            '(2013.01.11)要望番号1700 --  END  --
            dr("INKA_TP") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.INKA_SHUBETSU.ColNo)).Trim()
            dr("UNSO_KB") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.UNSOMOTO_KBN.ColNo)).Trim()
            dr("UNSOCO_NM") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.UNSO_CORP.ColNo)).Trim()
            '2013.04.03 Notes1995 START
            dr("OUTKA_MOTO_NM") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.OUTKA_MOTO_NM.ColNo)).Trim()
            '2013.04.03 Notes1995 END
            dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.EDI_NO.ColNo)).Trim()
            dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.KANRI_NO.ColNo)).Trim()
            dr("BUYER_ORD_NO_L") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.BUYER_ORDER_NO.ColNo)).Trim()
            dr("TANTO_USER") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.TANTO_USER_NM.ColNo)).Trim()
            dr("SYS_ENT_USER") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.SYS_ENT_USER_NM.ColNo)).Trim()
            dr("SYS_UPD_USER") = Me._LMHconV.GetCellValue(.Cells(0, _G.sprEdiListDef.SYS_UPD_USER_NM.ColNo)).Trim()

        End With

        Dim inOutKb As String = "1" '入荷

        'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
        ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow(inOutKb, frm.cmbEigyo.SelectedValue.ToString(), _
                                             frm.cmbWare.SelectedValue.ToString(), frm.txtCustCD_L.TextValue, frm.txtCustCD_M.TextValue)
        If 0 < ediCustDrs.Length Then
            ediIndex = ediCustDrs(0).Item("EDI_CUST_INDEX").ToString()
            ediRcvHed = ediCustDrs(0).Item("RCV_NM_HED").ToString()
            ediSend = ediCustDrs(0).Item("SND_NM").ToString()
            '2012.02.25 大阪対応 START
            inOutFlg = ediCustDrs(0).Item("FLAG_16").ToString()
            '2012.02.25 大阪対応 END
            '▼▼▼二次
            '受信DTL排他用コメントアウト
            'ediRcvDtl = ediCustDrs(0).Item("RCV_NM_DTL").ToString()
            '▲▲▲二次
        Else
            Return False
        End If

        If (_G.IsShowOutkaCtlNoLCondM(frm.cmbEigyo.SelectedValue.ToString() _
                                    , frm.txtCustCD_L.TextValue _
                                    , frm.txtCustCD_M.TextValue)) Then

            dr("IS_SHOW_COND_M") = LMConst.FLG.ON
        Else
            dr("IS_SHOW_COND_M") = LMConst.FLG.OFF
        End If

        dr("EDI_CUST_INDEX") = ediIndex
        dr("RCV_NM_HED") = ediRcvHed
        dr("SND_NM") = ediSend
        '2012.02.25 大阪対応 START
        dr("EDI_CUST_INOUTFLG") = inOutFlg
        '2012.02.25 大阪対応 END
        '▼▼▼二次
        '受信DTL排他用コメントアウト
        'dr("RCV_NM_DTL") = ediRcvDtl
        '▲▲▲二次
        '処理種別
        dr("EVENT_SHUBETSU") = eventShubetsu
        '印刷種別
        dr("PRINT_SHUBETSU") = printShubetsu
        '検索,印刷条件をデータセットに設定
        rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

        Return True

    End Function

    'ADD START 2019/9/12 依頼番号:007111
    ''' <summary>
    ''' 現品票印刷条件データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Function SetGenpinPrintInData(ByVal frm As LMH010F, _
                                          ByVal rtDs As DataSet, _
                                          ByVal eventShubetsu As Integer) As Boolean

        With frm.sprEdiList.ActiveSheet

            Dim chkList As ArrayList = Me._V.getCheckList()

            '選択された明細行分DataRowを作成
            For Each selectRow As Integer In chkList

#If False Then  'UPD 2019/12/17
                Dim dr As DataRow = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()

                dr("EVENT_SHUBETSU") = eventShubetsu

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("ROW_NO") = selectRow.ToString

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

#Else
                If ("OK").Equals(Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.GENPINHYO_CHKFLG.ColNo))) = True Then
                    Dim dr As DataRow = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()

                    dr("EVENT_SHUBETSU") = eventShubetsu

                    dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                    dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))
                    dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))
                    dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))
                    dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                    dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                    dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                    dr("ROW_NO") = selectRow.ToString

                    rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                End If

#End If

            Next

        End With

        Return True

    End Function
    'ADD END   2019/9/12 依頼番号:007111

#End Region

    '2012.03.13 大阪対応START
#Region "出力(CSV作成・印刷)"

    ''' <summary>
    ''' 出力(CSV作成・印刷)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OutputPrint(ByVal frm As LMH010F)

        Select Case frm.cmbOutput.SelectedValue.ToString()

            Case LMH010C.JYUSIN_PRT _
               , LMH010C.JYUSIN_ICHIRAN '受信帳票,受信一覧表

                '要望番号1061 2012.05.15 追加・修正START
                'DataSet設定
                Dim rtDs As DataSet = New LMH010DS()

                Select Case frm.cmbOutputKb.SelectedValue.ToString()

                    Case LMH010C.OUTPUT_SUMI
                        rtDs = Me.SetDataOutputZumi(frm, rtDs)

                    Case Else
                        rtDs = Me.OutputPrt(frm, rtDs)

                End Select

                'Dim rtDs As DataSet = Me.OutputPrt(frm)
                '要望番号1061 2012.05.15 追加・修正END

                'メッセージ情報を初期化する
                MyBase.ClearMessageStoreData()

                '(2013.01.23)印刷完了メッセージの追加 -- START --
                '処理終了メッセージの表示
                If MyBase.IsMessageStoreExist = False And MyBase.IsMessageExist = False Then
                    MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})
                End If
                '(2013.01.23)印刷完了メッセージの追加 --  END  --

                '出力済はエラーEXCELを表示
                If (frm.cmbOutputKb.SelectedValue.ToString()).Equals(LMH010C.OUTPUT_SUMI) = True Then

                    'メッセージコードの判定
                    If MyBase.IsMessageStoreExist = True Then

                        Call Me.OutputExcel(frm)

                    End If

                Else
                    '未出力で一括印刷はエラーEXCELを表示
                    If (frm.cmbOutput.SelectedValue.ToString()).Equals(LMH010C.IKKATU_PRT) = True Then

                        'メッセージコードの判定
                        If MyBase.IsMessageStoreExist = True Then

                            Call Me.OutputExcel(frm)

                        End If
                    Else
                        '未出力で一括印刷以外はエラーメッセージで表示
                        '2012.03.19 修正START
                        'メッセージコードの判定
                        If MyBase.IsMessageExist = True Then

                            MyBase.ShowMessage(frm)

                        End If
                        '2012.03.19 修正END

                    End If
                    '要望番号1007 2012.05.08 修正END

                End If

                ''2012.03.19 修正START
                ''メッセージコードの判定
                'If MyBase.IsMessageExist = True Then

                '    MyBase.ShowMessage(frm)

                'End If
                ''2012.03.19 修正END

                'プレビュー判定の共通ロジック
                Call Me.PrtOutPutPrev(rtDs)

                '処理終了アクション
                Call Me._LMHconH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.UnLockedForm()

                '未着・早着ファイル作成対応 Start
            Case LMH010C.MISOUTYAKU_FILE_MAKE '未着・早着ファイル作成

                '続行確認
                Dim rtn As MsgBoxResult

                rtn = Me.ShowMessage(frm, "C001", New String() {"未着・早着ファイル作成"})

                If rtn = MsgBoxResult.Ok Then
                ElseIf rtn = MsgBoxResult.Cancel Then
                    Me.ShowMessage(frm, "G007")
                    Exit Sub
                End If

                'DataSet設定
                Dim rtDs As DataSet = New LMH830DS()

                rtDs = Me.OutputMisoutyakuFile(frm, rtDs)

                'CSV出力データ作成処理
                Dim rtnFlg As Boolean = Me.MakeMiSoucyakuCSV(frm, rtDs)

                'メッセージ情報を初期化する
                MyBase.ClearMessageStoreData()

                'エラーEXCELを表示
                If MyBase.IsMessageStoreExist = True Then
                    Call Me.OutputExcel(frm)
                ElseIf MyBase.IsMessageExist = True Then
                    MyBase.ShowMessage(frm)
                Else
                    Call Me.SuccessOutputMisoutyakuFile(frm, rtDs)
                End If

                '処理終了アクション
                Call Me._LMHconH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.UnLockedForm()
                '未着・早着ファイル作成対応 End


            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 受信帳票作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Function OutputPrt(ByVal frm As LMH010F, ByVal rtDs As DataSet) As DataSet

        ''DataSet設定
        'Dim rtDs As DataSet = New LMH010DS()

        '受信帳票INデータ設定
        Call Me.SetDataOutputPrt(frm, rtDs)

        rtDs = MyBase.CallWSA("LMH010BLF", "SetDsPrtData", rtDs)

        Return rtDs

    End Function

    ''' <summary>
    ''' 受信帳票INデータ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataOutputPrt(ByVal frm As LMH010F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMH010C.TABLE_NM_OUTPUTIN).NewRow()
        Dim ediCustDrs As DataRow()

        Dim inOutKb As String = "1"                                     '入出荷区分("1"(入荷))
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()       '倉庫コード
        Dim custCdL As String = frm.txtPrt_CustCD_L.TextValue.ToString() '荷主コード(大)
        Dim custCdM As String = frm.txtPrt_CustCD_M.TextValue.ToString() '荷主コード(中)
        Dim outputKb As String = frm.cmbOutputKb.SelectedValue.ToString() '出力区分
        Dim prtShubetu As String = String.Empty

        Select Case frm.cmbOutput.SelectedValue.ToString()

            Case LMH010C.JYUSIN_PRT _
               , LMH010C.JYUSIN_ICHIRAN            '受信帳票,受信一覧表

                'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
                ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
                If 0 < ediCustDrs.Length Then
                    dr("NRS_BR_CD") = brCd
                    dr("WH_CD") = whCd
                    dr("CUST_CD_L") = custCdL
                    dr("CUST_CD_M") = custCdM
                    dr("OUTPUT_SHUBETU") = frm.cmbOutput.SelectedValue.ToString()
                    dr("CRT_DATE_FROM") = frm.imdOutputDateFrom.TextValue
                    dr("CRT_DATE_TO") = frm.imdOutputDateTo.TextValue
                    dr("EDI_CUST_INDEX") = ediCustDrs(0)("EDI_CUST_INDEX")
                    If outputKb = "01" Then
                        dr("PRTFLG") = "0"
                    ElseIf outputKb = "02" Then
                        dr("PRTFLG") = "1"
                    Else
                        dr("PRTFLG") = String.Empty
                    End If
                    dr("INOUT_KB") = inOutKb

                    dr("RCV_NM_HED") = ediCustDrs(0)("RCV_NM_HED")
                    dr("INOUT_UMU_KB") = ediCustDrs(0)("FLAG_16")
                    '要望番号1062 2012.05.15 修正START
                    '↓に関してはキャッシュの修正が入り次第切替え
                    dr("RCV_NM_DTL") = ediCustDrs(0)("RCV_NM_DTL")
                    'dr("RCV_NM_EXT") = ediCustDrs(0)("RCV_NM_EXT")
                    '要望番号1062 2012.05.15 修正END

                Else
                    MyBase.SetMessage("E361")
                    Exit Sub
                End If

                ds.Tables(LMH010C.TABLE_NM_OUTPUTIN).Rows.Add(dr)

            Case Else

        End Select

    End Sub

    '未着・早着ファイル作成対応 Start
    ''' <summary>
    '''未着・早着ファイル作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Function OutputMisoutyakuFile(ByVal frm As LMH010F, ByVal rtDs As DataSet) As DataSet

        '未着・早着ファイルINデータ設定
        Call Me.SetDataOutputMisoutyakuFile(frm, rtDs)

        rtDs = MyBase.CallWSA("LMH830BLF", "OutputMisoutyakuFile", rtDs)

        Return rtDs

    End Function
    '未着・早着ファイル作成対応 End

    '未着・装着ファイル作成対応 Start
    ''' <summary>
    ''' 未着・早着ファイル作成INデータ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataOutputMisoutyakuFile(ByVal frm As LMH010F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables("LMH830IN").NewRow()

        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()       '倉庫コード
        Dim custCdL As String = frm.txtPrt_CustCD_L.TextValue.ToString() '荷主コード(大)
        Dim custCdM As String = frm.txtPrt_CustCD_M.TextValue.ToString() '荷主コード(中)
        Dim crtDateFrom As String = frm.imdOutputDateFrom.TextValue.ToString() 'EDI取込日

        Select Case frm.cmbOutput.SelectedValue.ToString()

            Case LMH010C.MISOUTYAKU_FILE_MAKE            '未着・早着ファイル作成

                dr("NRS_BR_CD") = brCd
                dr("WH_CD") = whCd
                dr("CUST_CD_L") = custCdL
                dr("CUST_CD_M") = custCdM
                dr("EDI_CRT_DATE") = crtDateFrom
                ds.Tables("LMH830IN").Rows.Add(dr)

            Case Else

        End Select

    End Sub
    '未着・装着ファイル作成対応 End

    ''' <summary>
    ''' プレビュー出力処理(共通)
    ''' </summary>
    ''' <param name="rtnDs"></param>
    ''' <remarks></remarks>
    Private Sub PrtOutPutPrev(ByVal rtnDs As DataSet)

        'プレビュー判定 
        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If 0 < prevDt.Rows.Count Then

            'プレビューの生成
            Dim prevFrm As RDViewer = New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            'フォーカス設定
            prevFrm.Focus()

        End If
    End Sub

#End Region

#Region "分析表取込"

    ''' <summary>
    ''' 分析表取込
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CoaTouroku(ByVal frm As LMH010F)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"分析票ファイル取込"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH810DS()

        rtDs = Me.ProcessCoaTouroku(frm, rtDs)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        '(2012.10.24)追加START 要望番号1531
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(frm)
            '(2012.10.24)追加START 要望番号1531
        ElseIf MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            MyBase.ShowMessage(frm, "G035", New String() {"分析票取込", String.Concat("取込件数：", rtDs.Tables("LMH810Result").Rows(0).Item("NORMALCNT").ToString(), "件。　" _
                                                                                , "エラー件数：", rtDs.Tables("LMH810Result").Rows(0).Item("ERRORCNT").ToString(), "件。")})

        Else
            MyBase.ShowMessage(frm, "G035", New String() {"分析票取込", String.Concat("取込件数：", rtDs.Tables("LMH810Result").Rows(0).Item("NORMALCNT").ToString(), "件。　" _
                                                                                , "エラー件数：", rtDs.Tables("LMH810Result").Rows(0).Item("ERRORCNT").ToString(), "件。")})
        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()


    End Sub

    ''' <summary>
    ''' 分析票取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ProcessCoaTouroku(ByVal frm As LMH010F, ByVal rtDs As DataSet) As DataSet

        '分析票登録INデータ設定
        Call Me.SetDataCoaTouroku(frm, rtDs)

        rtDs = MyBase.CallWSA("LMH810BLF", "CoaTouroku", rtDs)

        Return rtDs

    End Function

    ''' <summary>
    ''' 受信帳票INデータ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataCoaTouroku(ByVal frm As LMH010F, ByVal ds As DataSet)

        ' 分析票
        Dim PrmDt As DataTable = ds.Tables("LMH810IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        '(2012.09.28) 追加START
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()      '倉庫コード
        '(2012.09.28) 追加END
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString() '荷主コード(大)
        Dim custCdM As String = frm.txtCustCD_M.TextValue.ToString() '荷主コード(中)
        PrmDr("NRS_BR_CD") = brCd
        '(2012.09.28) 追加START
        PrmDr("WH_CD") = whCd
        '(2012.09.28) 追加END
        PrmDr("CUST_CD_L") = custCdL
        PrmDr("CUST_CD_M") = custCdM

        PrmDt.Rows.Add(PrmDr)

    End Sub

#End Region

    '2012.11.30 追加START(UTI対応)
#Region "入荷確認ファイル取込"

    ''' <summary>
    ''' 入荷確認ファイル取込
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub InkaConfTouroku(ByVal frm As LMH010F, ByVal rcvPath As String, _
                                ByVal backupPath As String, ByVal errorPath As String, _
                                ByVal rcvExtention As String)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"入荷確認ファイル取込"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH820DS()

        rtDs = Me.ProcessInkaConfTouroku(frm, rtDs, rcvPath, backupPath, errorPath, rcvExtention)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'Dim max As Integer = rtDs.Tables("LMH820_WORNING").Rows.Count - 1
        'Dim strWrn As String = String.Empty

        ''メッセージコードの判定
        'If max > 0 Then

        '    For i As Integer = 0 To max

        '        If i = 0 Then
        '            strWrn = String.Concat(rtDs.Tables("LMH820_WORNING").Rows(i).Item("H4_DELIVERY_NO").ToString())
        '        Else
        '            strWrn = String.Concat(strWrn, ", ", rtDs.Tables("LMH820_WORNING").Rows(i).Item("H4_DELIVERY_NO").ToString())
        '        End If

        '    Next

        '    rtn = Me.ShowMessage(frm, "W225", New String() {strWrn})

        '    If rtn = MsgBoxResult.Ok Then

        '        If String.IsNullOrEmpty(rtDs.Tables("LMH820_WORNING").Rows(0).Item("WORNING_FLG").ToString()) = False Then
        '            rtDs.Tables("LMH820_WORNING").Rows(0).Item("WORNING_FLG") = "1"
        '        End If

        '        rtDs = Me.ProcessInkaConfTouroku(frm, rtDs, rcvPath, backupPath, errorPath, rcvExtention)

        '    ElseIf rtn = MsgBoxResult.Cancel Then
        '        Me.ShowMessage(frm, "G007")
        '        Exit Sub
        '    End If
        'End If

        If MyBase.IsErrorMessageExist = True Then

            MyBase.ShowMessage(frm)

        ElseIf MyBase.IsMessageStoreExist = True Then

            If rtDs.Tables("LMH820_DELI_REPETE").Rows.Count > 0 AndAlso rtDs.Tables("LMH820_DELI_REPETE").Rows(0).Item("REPETE_FLAG").Equals("1") = True Then

                Dim files As String() = System.IO.Directory.GetFiles(rcvPath, _
                                             String.Concat("*", rcvExtention), _
                                             System.IO.SearchOption.TopDirectoryOnly)


                For Each fileName As String In files
                    'ファイルをバックアップへコピー
                    Call Me.CopyAndDelete(fileName, backupPath, rtDs)
                Next

                MyBase.ShowMessage(frm, "G035", New String() {"入荷確認ファイル取込", String.Concat("取込ファイル件数：", rtDs.Tables("LMH820Result").Rows(0).Item("NORMALCNT").ToString(), "件。　" _
                , "エラー件数：", rtDs.Tables("LMH820Result").Rows(0).Item("ERRORCNT").ToString(), "件。" _
                , "ヘッダー件数：", rtDs.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT").ToString(), "件。" _
                , "明細件数：", rtDs.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT").ToString(), "件。")})

            End If

            Call Me.OutputExcel(frm)


            'MyBase.ShowMessage(frm, "G035", New String() {"入荷確認ファイル取込", String.Concat("取込ファイル件数：", rtDs.Tables("LMH820Result").Rows(0).Item("NORMALCNT").ToString(), "件。　" _
            '                                                                    , "エラー件数：", rtDs.Tables("LMH820Result").Rows(0).Item("ERRORCNT").ToString(), "件。" _
            '                                                                    , "ヘッダー件数：", rtDs.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT").ToString(), "件。" _
            '                                                                    , "明細件数：", rtDs.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT").ToString(), "件。")})

        Else

            Dim files As String() = System.IO.Directory.GetFiles(rcvPath, _
                                         String.Concat("*", rcvExtention), _
                                         System.IO.SearchOption.TopDirectoryOnly)


            For Each fileName As String In files
                'ファイルをバックアップへコピー
                Call Me.CopyAndDelete(fileName, backupPath, rtDs)
            Next

            MyBase.ShowMessage(frm, "G035", New String() {"入荷確認ファイル取込", String.Concat("取込ファイル件数：", rtDs.Tables("LMH820Result").Rows(0).Item("NORMALCNT").ToString(), "件。　" _
            , "エラー件数：", rtDs.Tables("LMH820Result").Rows(0).Item("ERRORCNT").ToString(), "件。" _
            , "ヘッダー件数：", rtDs.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT").ToString(), "件。" _
            , "明細件数：", rtDs.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT").ToString(), "件。")})
        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()


    End Sub

    ''' <summary>
    ''' 入荷確認ファイル取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ProcessInkaConfTouroku(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal rcvPath As String, _
                                            ByVal backupPath As String, ByVal errorPath As String, ByVal rcvExtention As String) As DataSet

        '入荷確認ファイル取込INデータ設定
        Call Me.SetDataInkaConfTouroku(frm, rtDs, rcvPath, backupPath, errorPath, rcvExtention)

        rtDs = MyBase.CallWSA("LMH820BLF", "InkaConfTouroku", rtDs)

        Return rtDs

    End Function

    ''' <summary>
    ''' 入荷確認ファイル取込INデータ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataInkaConfTouroku(ByVal frm As LMH010F, ByVal ds As DataSet, ByVal rcvPath As String, _
                                       ByVal backupPath As String, ByVal errorPath As String, _
                                       ByVal rcvExtention As String)

        ' 入荷確認ファイル
        Dim PrmDt As DataTable = ds.Tables("LMH820IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()      '倉庫コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString() '荷主コード(大)
        Dim custCdM As String = frm.txtCustCD_M.TextValue.ToString() '荷主コード(中)
        PrmDr("NRS_BR_CD") = brCd
        PrmDr("WH_CD") = whCd
        PrmDr("CUST_CD_L") = custCdL
        PrmDr("CUST_CD_M") = custCdM
        PrmDr("RCV_WORK_INPUT_DIR") = rcvPath
        PrmDr("BACKUP_INPUT_DIR") = backupPath
        PrmDr("ERROR_INPUT_DIR") = errorPath
        PrmDr("RCV_FILE_EXTENTION") = rcvExtention

        PrmDt.Rows.Add(PrmDr)

    End Sub

#End Region
    '2012.11.30 追加END(UTI対応)

    '2015.04.13 追加START
#Region "キャッシュ取得処理(取込処理時)"

    ''' <summary>
    ''' キャッシュから情報取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetCachedSemiEDI(ByVal frm As LMH010F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMH010DS()
        Dim dr As DataRow

        With frm

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim whCd As String = frm.cmbWare.SelectedValue.ToString()
            Dim custCdL As String = frm.txtCustCD_L.TextValue
            Dim custCdM As String = frm.txtCustCD_M.TextValue
            Dim inoutKb As String = "10"
            Dim mNinushiInoutKb As String = "1"

            'セミEDI設定情報取得
            Dim semiEdiDr() As DataRow = Me._LMHconG.SelectSemiEDIListDatalow(brCd, whCd, custCdL, custCdM, inoutKb)
            If 0 < semiEdiDr.Length Then

                dr = rtDs.Tables(LMH010C.SEMIEDI_INFO).NewRow()

                'キャッシュで取得した値をセミEDI設定情報のDATATABLEに入替
                dr("NRS_BR_CD") = semiEdiDr(0).Item("NRS_BR_CD").ToString()
                dr("WH_CD") = semiEdiDr(0).Item("WH_CD").ToString()
                dr("CUST_CD_L") = semiEdiDr(0).Item("CUST_CD_L").ToString()
                dr("CUST_CD_M") = semiEdiDr(0).Item("CUST_CD_M").ToString()
                dr("INOUT_KB") = semiEdiDr(0).Item("INOUT_KB").ToString()
                dr("EDI_CUST_INDEX") = semiEdiDr(0).Item("EDI_CUST_INDEX").ToString()
                dr("SEMI_EDI_FLAG") = semiEdiDr(0).Item("SEMI_EDI_FLAG").ToString()
                dr("SEMI_EDI_PRINT_FLAG") = semiEdiDr(0).Item("SEMI_EDI_PRINT_FLAG").ToString()
                dr("RCV_INPUT_DIR") = semiEdiDr(0).Item("RCV_INPUT_DIR").ToString()
                dr("WORK_INPUT_DIR") = semiEdiDr(0).Item("WORK_INPUT_DIR").ToString()
                dr("BACKUP_INPUT_DIR") = semiEdiDr(0).Item("BACKUP_INPUT_DIR").ToString()
                dr("RCV_FILE_EXTENTION") = semiEdiDr(0).Item("RCV_FILE_EXTENTION").ToString()
                dr("DELIMITER_KB") = semiEdiDr(0).Item("DELIMITER_KB").ToString()
                dr("RCV_FILE_NM") = semiEdiDr(0).Item("RCV_FILE_NM").ToString()
                dr("RCV_FILE_COL_CNT") = semiEdiDr(0).Item("RCV_FILE_COL_CNT").ToString()
                dr("RCV_FILE_WORKRENAME") = semiEdiDr(0).Item("RCV_FILE_WORKRENAME").ToString()
                dr("RCV_FILE_BACKRENAME") = semiEdiDr(0).Item("RCV_FILE_BACKRENAME").ToString()
                dr("TOP_ROW_CNT") = semiEdiDr(0).Item("TOP_ROW_CNT").ToString()
                dr("PLURAL_FILE_FLAG") = semiEdiDr(0).Item("PLURAL_FILE_FLAG").ToString()
                dr("PRINT_CLASS_NM") = semiEdiDr(0).Item("PRINT_CLASS_NM").ToString()

                dr("RCV_TBL_INS_FLG") = semiEdiDr(0).Item("RCV_TBL_INS_FLG").ToString()
                dr("FILE_CHICE_KBN") = semiEdiDr(0).Item("FILE_CHICE_KBN").ToString()
                dr("BUCKING_NO_1") = semiEdiDr(0).Item("BUCKING_NO_1").ToString()
                dr("BUCKING_NO_2") = semiEdiDr(0).Item("BUCKING_NO_2").ToString()
                dr("BUCKING_NO_3") = semiEdiDr(0).Item("BUCKING_NO_3").ToString()
                dr("BUCKING_TERM_1") = semiEdiDr(0).Item("BUCKING_TERM_1").ToString()
                dr("BUCKING_TERM_2") = semiEdiDr(0).Item("BUCKING_TERM_2").ToString()
                dr("BUCKING_TERM_3") = semiEdiDr(0).Item("BUCKING_TERM_3").ToString()
                dr("DEVIDE_NO_1") = semiEdiDr(0).Item("DEVIDE_NO_1").ToString()
                dr("DEVIDE_NO_2") = semiEdiDr(0).Item("DEVIDE_NO_2").ToString()
                dr("DEVIDE_NO_3") = semiEdiDr(0).Item("DEVIDE_NO_3").ToString()
                dr("L_DEL_KB_NO") = semiEdiDr(0).Item("L_DEL_KB_NO").ToString()
                dr("L_OUTKA_PLAN_DATE_NO") = semiEdiDr(0).Item("L_OUTKA_PLAN_DATE_NO").ToString()
                dr("L_ARR_PLAN_DATE_NO") = semiEdiDr(0).Item("L_ARR_PLAN_DATE_NO").ToString()
                dr("L_DEST_CD_NO") = semiEdiDr(0).Item("L_DEST_CD_NO").ToString()
                dr("L_DEST_NM_NO") = semiEdiDr(0).Item("L_DEST_NM_NO").ToString()
                dr("L_DEST_ZIP_NO") = semiEdiDr(0).Item("L_DEST_ZIP_NO").ToString()
                dr("L_DEST_AD_1_NO") = semiEdiDr(0).Item("L_DEST_AD_1_NO").ToString()
                dr("L_DEST_AD_2_NO") = semiEdiDr(0).Item("L_DEST_AD_2_NO").ToString()
                dr("L_DEST_AD_3_NO") = semiEdiDr(0).Item("L_DEST_AD_3_NO").ToString()
                dr("L_DEST_TEL_NO") = semiEdiDr(0).Item("L_DEST_TEL_NO").ToString()
                dr("L_DEST_JIS_CD_NO") = semiEdiDr(0).Item("L_DEST_JIS_CD_NO").ToString()

                dr("L_DEST_CUST_ORD_NO") = semiEdiDr(0).Item("L_DEST_CUST_ORD_NO").ToString()

                dr("L_REMARK_NO") = semiEdiDr(0).Item("L_REMARK_NO").ToString()
                dr("L_FREE_C01_NO") = semiEdiDr(0).Item("L_FREE_C01_NO").ToString()
                dr("L_FREE_C02_NO") = semiEdiDr(0).Item("L_FREE_C02_NO").ToString()
                dr("L_FREE_C03_NO") = semiEdiDr(0).Item("L_FREE_C03_NO").ToString()
                dr("L_FREE_C24_NO") = semiEdiDr(0).Item("L_FREE_C24_NO").ToString()
                dr("L_FREE_C25_NO") = semiEdiDr(0).Item("L_FREE_C25_NO").ToString()
                dr("L_FREE_C26_NO") = semiEdiDr(0).Item("L_FREE_C26_NO").ToString()
                dr("L_FREE_N01_NO") = semiEdiDr(0).Item("L_FREE_N01_NO").ToString()
                dr("L_FREE_N02_NO") = semiEdiDr(0).Item("L_FREE_N02_NO").ToString()
                dr("L_FREE_N03_NO") = semiEdiDr(0).Item("L_FREE_N03_NO").ToString()
                dr("M_DEL_KB_NO") = semiEdiDr(0).Item("M_DEL_KB_NO").ToString()
                dr("M_CUST_ORD_NO_DTL_NO") = semiEdiDr(0).Item("M_CUST_ORD_NO_DTL_NO").ToString()
                dr("M_CUST_GOODS_CD_NO") = semiEdiDr(0).Item("M_CUST_GOODS_CD_NO").ToString()
                dr("M_GOODS_NM_NO") = semiEdiDr(0).Item("M_GOODS_NM_NO").ToString()
                dr("M_LOT_NO_NO") = semiEdiDr(0).Item("M_LOT_NO_NO").ToString()
                dr("M_SERIAL_NO_NO") = semiEdiDr(0).Item("M_SERIAL_NO_NO").ToString()
                dr("M_DEF_ALCTD_KB") = semiEdiDr(0).Item("M_DEF_ALCTD_KB").ToString()
                dr("M_OUTKA_TTL_NB_NO") = semiEdiDr(0).Item("M_OUTKA_TTL_NB_NO").ToString()
                dr("M_OUTKA_TTL_QT_NO") = semiEdiDr(0).Item("M_OUTKA_TTL_QT_NO").ToString()
                dr("M_IRIME_NO") = semiEdiDr(0).Item("M_IRIME_NO").ToString()

                dr("M_REMARK_NO") = semiEdiDr(0).Item("M_REMARK_NO").ToString()
                dr("M_FREE_C01_NO") = semiEdiDr(0).Item("M_FREE_C01_NO").ToString()
                dr("M_FREE_C02_NO") = semiEdiDr(0).Item("M_FREE_C02_NO").ToString()
                dr("M_FREE_C03_NO") = semiEdiDr(0).Item("M_FREE_C03_NO").ToString()
                dr("M_FREE_C24_NO") = semiEdiDr(0).Item("M_FREE_C24_NO").ToString()
                dr("M_FREE_C25_NO") = semiEdiDr(0).Item("M_FREE_C25_NO").ToString()
                dr("M_FREE_C26_NO") = semiEdiDr(0).Item("M_FREE_C26_NO").ToString()
                dr("M_FREE_N01_NO") = semiEdiDr(0).Item("M_FREE_N01_NO").ToString()
                dr("M_FREE_N02_NO") = semiEdiDr(0).Item("M_FREE_N02_NO").ToString()
                dr("M_FREE_N03_NO") = semiEdiDr(0).Item("M_FREE_N03_NO").ToString()
                dr("EDI_TORIKESI_FLG") = semiEdiDr(0).Item("EDI_TORIKESI_FLG").ToString()
                dr("EDI_TORITERM_FLG") = semiEdiDr(0).Item("EDI_TORITERM_FLG").ToString()
                dr("L_BUYER_ORD_NO") = semiEdiDr(0).Item("L_BUYER_ORD_NO").ToString()
                dr("L_SHIP_CD_NO") = semiEdiDr(0).Item("L_SHIP_CD_NO").ToString()
                dr("M_IRIME_UT_NO") = semiEdiDr(0).Item("M_IRIME_UT_NO").ToString()
                dr("DTL_DATACHECK_FLG") = semiEdiDr(0).Item("DTL_DATACHECK_FLG").ToString()
                dr("DTL_OUTKAPKGNB_CALC_FLG") = semiEdiDr(0).Item("DTL_OUTKAPKGNB_CALC_FLG").ToString()

                Dim ninushi() As DataRow = Me._LMHconV.SelectEdiCustListDataRow(mNinushiInoutKb, brCd, whCd, custCdL, custCdM)
                If 0 < ninushi.Length Then

                    dr("RCV_NM_HED") = ninushi(0).Item("RCV_NM_HED").ToString()
                    dr("RCV_NM_DTL") = ninushi(0).Item("RCV_NM_DTL").ToString()
                    dr("RCV_NM_EXT") = ninushi(0).Item("RCV_NM_EXT").ToString()
                    dr("FLAG_17") = ninushi(0).Item("FLAG_17").ToString() '<< コメント取敢えず

                End If

                Dim kbnDr() As DataRow = Me._LMHconG.SelectKbnListDataRow(brCd, "D003")
                If 0 < kbnDr.Length Then
                    dr("BR_INITIAL") = kbnDr(0).Item("KBN_NM6").ToString()
                End If

                rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows.Add(dr)
            Else
                '存在チェック(キャッシュで取得した値が0件の場合はエラー)
                MyBase.SetMessage("E459")   'エラーメッセージ
            End If

        End With

        Return rtDs

    End Function

#End Region

#Region "取込処理(セミEDI標準化対応版)"
    ''' <summary>
    ''' 取込処理(セミEDI標準化対応版)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TorikomiStanderdEdition(ByVal frm As LMH010F, ByVal eventshubetsu As Integer, ByVal errDs As DataSet, ByVal rtDs As DataSet)

        Dim rtDr As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)
        Dim rcv_dir As String = String.Empty
        Dim work_dir As String = String.Empty

        '2019/07/18 依頼番号:006754 add start
        'EdiIndexを取得
        Dim iEdiIndex As Integer = Convert.ToInt32(rtDs.Tables("LMH010_SEMIEDI_INFO").Rows(0)("EDI_CUST_INDEX"))
        '2019/07/18 依頼番号:006754 add end

        '========= ファイル選択処理 =======
        'WindowsDialogインスタンス生成
        Dim ofd As New OpenFileDialog()

        'WindowsDialogのタイトル設定
        ofd.Title = "取込むファイルを選択してください"

        '[ファイルの種類]に表示される選択肢を制限
        '[M_SEMI_EDI_INFO_STATE]よりRCV_FILE_EXTENTIONに取込可能拡張子を設定
        Dim Delimiter As String = String.Concat("*.", rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0).Item("RCV_FILE_EXTENTION"))

        '取込ファイルのフィルタ設定
        Dim filter As String = String.Empty
        Select Case rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0).Item("DELIMITER_KB").ToString()
            Case "01" 'カンマ区切り
                filter = String.Concat("CSVファイル(", Delimiter, ")|", Delimiter)
            Case "02" 'TAB区切りの場合
                filter = String.Concat("TSVファイル(", Delimiter, ")|", Delimiter)
            Case "03" '固定長の場合
                filter = String.Concat("Textファイル(", Delimiter, ")|", Delimiter)
            Case "04" 'EXCELの場合
                filter = String.Concat("Excelファイル(", Delimiter, ")|", Delimiter)
        End Select

        ofd.Filter = filter
        ofd.FilterIndex = 1

        '複数選択対応(必要であれば)
        '取敢えずは単一ファイル選択のみの対応
        'If rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0).Item("PLURAL_FILE_FLAG").Equals(LMConst.FLG.ON) Then
        '    ofd.Multiselect = True
        'Else
        '    ofd.Multiselect = False
        'End If
        ofd.Multiselect = False
        '取敢えずは単一ファイル選択のみの対応

        'ファイル名取得
        Dim objFiles As ArrayList = New ArrayList
        Dim arrCnt As Integer = 0
        If ofd.ShowDialog() = DialogResult.OK Then
            For Each newArr As String In ofd.SafeFileNames
                objFiles.Add(newArr)
            Next
        Else
            Exit Sub
        End If

        '不要ダイアログのゴミ削除
        ofd.Dispose()

        arrCnt = objFiles.Count
        '========= ファイル選択処理 =======

        'ファイルパス取得(ファイルまでのフルパスからファイル分のパスを消去でディレクトリを確保)
        rcv_dir = ofd.FileNames(0).ToString()
        rcv_dir = rcv_dir.Replace(objFiles(0).ToString(), "")
        rtDr.Item("RCV_INPUT_DIR") = rcv_dir

        '受信ファイル名セット
        SetDataEdiTorikomiHedStanderdEdition(objFiles, rtDs)

        'TODO:関連チェック実行
        '①受信フォルダの存在チェック
        '②受信ファイルの存在チェック
        '③受信ファイルのオープン可否チェック(システム外でのファイルオープン処理の有無確認)
        '④受信ファイルの総数(999)を超えるならエラー)
        If Me._V.IsTorikomiKanrenCheckStanderdEdition(rtDs, objFiles) = False Then
            Exit Sub
        End If

        '======================受信ファイル操作 -ED- ======================
        'コネクションリスト
        Dim arrCloser As ArrayList = New ArrayList

        Select Case rtDr.Item("DELIMITER_KB").ToString()
            Case "01", "02"     'カンマ区切り、TAB区切りの場合
                rtDs = Me.SetDataEdiTorikomiShosaiStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)

            Case "03"           '固定長の場合
                rtDs = Me.SetDataEdiTorikomiShosaiFixedLengthStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)

            Case "04"           'EXCELの場合
                '2019/07/18 依頼番号:006754 del start
                'rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)
                '2019/07/18 依頼番号:006754 del end
                '2019/07/18 依頼番号:006754 add start
                Select Case iEdiIndex
                    Case EdiCustIndex.AgcW00440
                        '大阪：ＡＧＣ若狭化学
                        rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser, "EDI")
                    Case Else
                        rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)
                End Select
                '2019/07/18 依頼番号:006754 add end
        End Select

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SemiEdiTorikomi")
        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH010BLF", "SemiEdiTorikomi", rtDs)

        '====================== 受信ファイル操作 -ED- ======================
        Dim rtnFileDr As DataRow
        Dim rtnErrFlg As String
        Dim rtnFile_Name_Rcv As String = String.Empty
        Dim rtnFile_Name_Bak As String = String.Empty
        Dim noExtends As String = String.Empty
        '=============  ファイル保存実行 --ST-- =============
        'ファイル保存ダイアログ
        Dim sfd As New FolderBrowserDialog

        'バックアップディレクトリ
        Dim backDir As String = String.Empty

        'ダイアログタイトル
        sfd.Description = "バックアップファイルを保存するフォルダを選択してください"

        'ファイル保存ダイアログ[初期ディレクトリ]
        sfd.RootFolder = Environment.SpecialFolder.Desktop

        '選択フォルダ設定
        sfd.SelectedPath = rcv_dir

        '新規フォルダ作成の許可

        'ダイアログ展開
        Dim dlogResult As DialogResult = sfd.ShowDialog()

        If dlogResult = DialogResult.OK Then
            'OKなら

            '選択ディレクトリ
            backDir = String.Concat(sfd.SelectedPath, "\")

        ElseIf dlogResult = DialogResult.Cancel Then
            'CANCELなら

            '取込時ディレクトリ
            backDir = rcv_dir
        End If

        'ダイアログのごみを破棄する
        sfd.Dispose()
        '=============ファイル保存実行 --ED-- =============

        For i As Integer = 0 To rtnDs.Tables("LMH010_EDI_TORIKOMI_HED").Rows.Count - 1
            rtnFileDr = rtnDs.Tables("LMH010_EDI_TORIKOMI_HED").Rows(i)

            rtnErrFlg = rtnFileDr.Item("ERR_FLG").ToString()
            rtnFile_Name_Rcv = rtnFileDr.Item("FILE_NAME_RCV").ToString()

            'エラーフラグ判定
            If rtnErrFlg.Equals("0") Then
                '[正常時処理]
                '受信ファイルのロックを解除 + オリジナルの削除＆コピーの作成

                'ファイル名の変更
                noExtends = System.IO.Path.GetFileNameWithoutExtension(String.Concat(rcv_dir, rtnFile_Name_Rcv))
                rtnFile_Name_Bak = String.Concat(noExtends, "_", MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1), rtnFile_Name_Rcv.Replace(noExtends, ""))

                'ファイルのコピーを作成
                System.IO.File.Copy(String.Concat(rcv_dir, rtnFile_Name_Rcv), String.Concat(backDir, rtnFile_Name_Bak), True)

                '各クローズ処理
                DoCloseAction(rtDs, arrCloser, i)

                'オリジナル削除
                System.IO.File.Delete(String.Concat(rcv_dir, rtnFile_Name_Rcv))
            Else
                '[エラー時処理]
                '受信ファイルのロックを解除

                '各クローズ処理
                DoCloseAction(rtDs, arrCloser, i)
            End If
        Next
        '====================== 受信ファイル操作 -ED- ======================

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'エラーエクセルの出力
            Call Me.OutputExcel(frm)
        Else
            '取込処理成功時処理
            Call Me.SuccessTorikomi(frm, rtnDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SemiEdiTorikomi")

        Call Me._LMHconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 各種閉じる処理の実行
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <param name="arrCloser"></param>
    ''' <remarks></remarks>
    Private Sub DoCloseAction(ByVal rtDs As DataSet, ByVal arrCloser As ArrayList, ByVal nawRow As Integer)
        'セミEDI情報
        Dim rtDr As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

        '拡張子により分岐
        Select Case rtDr.Item("DELIMITER_KB").ToString()
            Case "01", "02", "03"   'カンマ区切り、TAB区切りの場合、固定長の場合

                'コネクションの確認
                If arrCloser(nawRow) Is Nothing Then
                    '間違いなくエラーです。
                    Exit Sub
                End If

                'コネクション
                Dim connect As System.IO.StreamReader = DirectCast(arrCloser(nawRow), System.IO.StreamReader)

                '閉じる
                connect.Close()

            Case "04"           'EXCELの場合

                Dim connect As ArrayList = New ArrayList


                If arrCloser(nawRow) Is Nothing Then
                    '間違いなくエラーです。
                    Exit Sub
                End If

                'アレイの一行コピー
                connect.AddRange(CType(arrCloser(nawRow), Collections.ICollection))

                'コネクションの確認
                If arrCloser(nawRow) Is Nothing Then
                    '間違いなくエラーです。
                End If

                '分解
                '=============
                '(xlCell)  '0 
                '(xlSheet) '1 
                '(xlBook)  '2 
                '(xlBooks) '3 
                '(xlApp)   '4 
                '=============
                Dim xlApp As Excel.Application = DirectCast(connect(4), Excel.Application)
                Dim xlBook As Excel.Workbook = DirectCast(connect(2), Excel.Workbook)
                Dim xlBooks As Excel.Workbooks = DirectCast(connect(3), Excel.Workbooks)
                Dim xlSheet As Excel.Worksheet = DirectCast(connect(1), Excel.Worksheet)
                Dim xlCell As Excel.Range = DirectCast(connect(0), Excel.Range)

                If xlCell IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
                    xlCell = Nothing
                End If

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing

                xlBook.Close(False) 'Excelを閉じる
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing

                xlApp.DisplayAlerts = False
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing

        End Select

    End Sub

#End Region
    '2015.04.13 追加END

#Region "LMH080(確認データ削除画面(UTI)遷移時)データセット"

    ''' <summary>
    ''' データセット設定(LMH080引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMH080InData(ByVal frm As LMH010F, ByVal prmDs As DataSet, ByVal pgId As String) As DataSet

        ''DataSet設定
        Dim ds As DataSet = New LMH080DS()
        Dim dr As DataRow = ds.Tables(LMH080C.TABLE_NM_IN).NewRow()

        dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()         '営業所コード
        dr.Item("CUST_CD_L") = frm.txtCustCD_L.TextValue                     '荷主コード(大)
        dr.Item("CUST_CD_M") = frm.txtCustCD_M.TextValue                     '荷主コード(中)

        ds.Tables(LMH080C.TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function

#End Region

#Region "入荷登録データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataInkaData(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable, ByVal setCount As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

#If True Then   'ADD cmbEigyo/06/08 007999 
        Dim custCd As String = frm.txtCustCD_L.TextValue
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()       '営業所コード
        Dim JJ_FLG As String = LMConst.FLG.OFF
        Dim sGOODS_COND_KB_3 As String = String.Empty
        Dim drjj As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C038' AND KBN_NM1 = '", brCd, "' And KBN_NM2 = '", custCd, "'"))
        If 0 < drjj.Length Then
            JJ_FLG = LMConst.FLG.ON
            sGOODS_COND_KB_3 = drjj(0).Item("KBN_NM3").ToString.Trim
        End If
#End If

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                If setCount = 0 Then
                    dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                    dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                    dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))
                    dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                    dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                    dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                    dr("RCV_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                    dr("RCV_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                    dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                    dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))
                    dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))
                    dr("ORDER_CHECK_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.ORDER_CHECK_FLG.ColNo))
                    dr("ROW_NO") = selectRow.ToString()
                    '▼▼▼二次
                    dr("AUTO_MATOME_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.AUTO_MATOME_FLG.ColNo))
                    '▲▲▲二次
#If True Then   'ADD 2020/06/08 007999 
                    dr("JJ_FLG") = JJ_FLG.ToString
                    dr("JJ_KBN_NM3") = sGOODS_COND_KB_3.ToString
#End If
                    '入荷登録用検索データをデータセットに設定
                    rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                ElseIf setCount = 1 Then
                    rtDs.Tables(LMH010C.TABLE_NM_IN).Rows(i)("AUTO_MATOME_FLG") = "9"
                End If
            Next

        End With

        If setCount = 0 Then
            dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
            dr("EVENT_SHUBETSU") = eventShubetsu
            rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)
        End If

    End Sub


#End Region

#Region "実績作成データセット"
    ''' <summary>
    ''' 実績作成データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSakusei(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010INOUT
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                dr("OUTKA_FROM_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.ORDER_NO.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))
                dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("JISSEKI_FLAG") = "1"
                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("INKA_JISSEKI_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("INKA_JISSEKI_FLG") = "1" '日合のみ
                dr("JISSEKI_SHORI_FLG") = "2"
                '↓デュポン用
                dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

                'LMH010_B_INKA_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKA_L).NewRow

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("INKA_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))
                dr("INKA_STATE_KB") = "90"
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_INKA_L).Rows.Add(dr)
            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "LMH070(紐付け画面遷移時)データセット"

    ''' <summary>
    ''' データセット設定(LMH070引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMH070InData(ByVal frm As LMH010F, ByVal prmDs As DataSet) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMH070DS()
        Dim dr As DataRow = ds.Tables(LMH070C.TABLE_NM_IN).NewRow()

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim selectRow As Integer = Convert.ToInt32(chkList(0))

        With frm.sprEdiList.Sheets(0)
            dr.Item("EDI_CTL_NO") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))                       'EDI管理番号
            dr.Item("NRS_BR_CD") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))                     '営業所コード
            dr.Item("WH_CD") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))                             '倉庫コード
            dr.Item("INOUT_KB") = "1"                                                                                                                                     '入出荷区分
            dr.Item("CUST_CD_L") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))                     '荷主コード(大)
            dr.Item("CUST_CD_M") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))                        '荷主コード(中)
            dr.Item("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))           '荷主INDEX番号
            dr.Item("EVENT_SHUBETSU") = DirectCast(LMH010C.EventShubetsu.HIMODUKE, Integer)

        End With

        ds.Tables(LMH070C.TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function

#End Region

#Region "EDI取消データセット"
    ''' <summary>
    ''' EDI取消データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataEdiTorikeshi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region


#Region "EDI取込ヘッダーデータセット"
    ''' <summary>
    ''' EDI取込ヘッダ取込(標準化版用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiHedStanderdEdition(ByVal flNmArr As ArrayList, ByVal rtDs As DataSet) As DataSet

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

        Dim dr As DataRow
        Dim sysDatetime As String = String.Empty
        Dim extention As String = String.Empty

        Dim ope_File_Name As String = String.Empty
        Dim backup_File_Name As String = String.Empty

        'システム時間を取得
        sysDatetime = String.Concat(MyBase.GetSystemDateTime()(0), MyBase.GetSystemDateTime()(1)).Remove(14)

        '受信格納フォルダのファイル数だけデータセットを作成
        For Each stFilePath As String In flNmArr
            'NewRow生成
            dr = rtDs.Tables(LMH010C.EDI_TORIKOMI_HED).NewRow()

            'ファイル名取得
            dr("FILE_NAME_RCV") = stFilePath
            dr("ERR_FLG") = "9"

            '格納
            rtDs.Tables(LMH010C.EDI_TORIKOMI_HED).Rows.Add(dr)
        Next stFilePath

        Return rtDs

    End Function

#End Region

    '2015.04.13 追加START
#Region "EDI取込詳細データセット"
    ''' <summary>
    ''' EDI取込詳細(セミ標準対応)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiStanderdEdition(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList) As DataSet

        'ふるい落とし機能の追加を忘れずに
        'ふるい落としキー格納用(キー番号がNullならやらない)

        '初期化
        arrCloser = New ArrayList

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH010C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty
        Dim cReader As System.IO.StreamReader = Nothing
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()       '営業所コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString() '荷主コード(大)
        'Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
        '                         ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '88' ")

        'ふるい落とし後データ格納用
        Dim drBucking() As DataRow = Nothing
        '-----------------------------------------------------------------------------------------------
        ' ふるい落とし設定
        '-----------------------------------------------------------------------------------------------
        'Function化期待の★
        'ふるい落とし機能の追加を忘れずに
        'ふるい落としキー格納用(キー番号がNullならいらない)
        Dim arrCutKey As ArrayList = New ArrayList
        'ふるい落とし条件格納用(キー番号がNullならいらない)
        Dim arrCutValue As ArrayList = New ArrayList

        'キー1
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("BUCKING_NO_1").ToString()) = False Then
            arrCutKey.Add(drSemiEdiInfo.Item("BUCKING_NO_1").ToString())
            arrCutValue.Add(drSemiEdiInfo.Item("BUCKING_TERM_1").ToString())
        End If
        'キー2
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("BUCKING_NO_2").ToString()) = False Then
            arrCutKey.Add(drSemiEdiInfo.Item("BUCKING_NO_2").ToString())
            arrCutValue.Add(drSemiEdiInfo.Item("BUCKING_TERM_2").ToString())
        End If
        'キー3
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("BUCKING_NO_3").ToString()) = False Then
            arrCutKey.Add(drSemiEdiInfo.Item("BUCKING_NO_3").ToString())
            arrCutValue.Add(drSemiEdiInfo.Item("BUCKING_TERM_3").ToString())
        End If

        'EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1

            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(String.Concat(drSemiEdiInfo.Item("RCV_INPUT_DIR"), dtHed.Rows(i).Item("FILE_NAME_RCV")), System.Text.Encoding.Default)

            'Closeコレクションにコレクト
            arrCloser.Add(DirectCast(cReader, System.IO.StreamReader))

            '先頭行飛ばしカウントの数だけ行を読み込む
            For j As Integer = 0 To Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT")) - 1
                cReader.ReadLine()
            Next

            '読み込みできる文字がなくなるまで繰り返す
            While (cReader.Peek() >= 0)

                ' ファイルを 1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                gyoCount += 1

                dr = rtDs.Tables(LMH010C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"

                'カンマ区切りの場合
                If drSemiEdiInfo.Item("DELIMITER_KB").ToString() = "01" Then
                    kugiri = ","
                    'TAB区切りの場合
                ElseIf drSemiEdiInfo.Item("DELIMITER_KB").ToString() = "02" Then
                    kugiri = vbTab
                End If

                'カラムカウント
                Dim columnCount As Integer = 1

                For Each aryf As String In Split(stBuffer, kugiri)

                    'ダブルクォーテーションで始まっている場合
                    If Left(aryf, 1).Equals(Chr(34)) And Right(aryf, 1).Equals(Chr(34)) = False Then
                        stock = aryf
                        stockFlag = True
                        Continue For
                    End If

                    If stockFlag = True Then
                        'ダブルクォーテーションで閉じられている場合
                        If Right(aryf, 1).Equals(Chr(34)) Then
                            aryf = String.Concat(stock, kugiri, aryf)
                            stockFlag = False
                            stock = String.Empty

                            'ダブルクォーテーションで閉じられていない場合
                        Else
                            stock = String.Concat(stock, kugiri, aryf)
                            Continue For
                        End If
                    End If

                    'ダブルクォーテーションで囲まれていた場合は、除外した文字列をセットする
                    If Left(aryf, 1).Equals(Chr(34)) And Right(aryf, 1).Equals(Chr(34)) Then
                        aryf = Mid(aryf, 2, Len(aryf) - 2)
                    End If

                    'If kbnDr.Length > 0 Then
                    '    Dim KbnTempStr As String() = Split(kbnDr(0).Item("SET_NAIYO").ToString(), ",")
                    '    Dim KbnTempStr2 As String() = Split(kbnDr(0).Item("SET_NAIYO_2").ToString(), ",")
                    '    For j As Integer = 0 To KbnTempStr.Length - 1
                    '        If KbnTempStr(j) = columnCount.ToString() Then
                    '            aryf = dr(String.Concat("COLUMN_", KbnTempStr2(j))).ToString + aryf
                    '        End If
                    '    Next
                    'End If

                    dr(String.Concat("COLUMN_", columnCount.ToString)) = aryf
                    columnCount += 1

                Next

                rtDs.Tables(LMH010C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            'cReader.Close()

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH010_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            '行カウントをリセット
            gyoCount = 0
        Next

        '条件設定
        Dim valIdx As Integer = -1
        Dim Keys As String = String.Empty
        Dim iCnt As Integer = 0
        Dim sOperator As String = String.Empty

        '追加開始 --- 2015.03.16
        '検索対象に"<>"が含まれている場合検索条件を"="から"<>"に変更
        For Each setKey As String In arrCutKey
            If (valIdx + 1) = 0 Then
                Dim stArrayData As String() = arrCutValue(valIdx + 1).ToString().Split(","c)
                For Each tempKey As String In stArrayData
                    '比較条件の変換
                    If tempKey.IndexOf("<>") >= 0 Then
                        sOperator = "<>'"
                        tempKey = tempKey.Replace("<>", "")
                    Else
                        sOperator = "='"
                    End If

                    If iCnt = 0 Then
                        Keys += String.Concat("COLUMN_", setKey, sOperator, tempKey, "'")
                    Else
                        Keys += String.Concat(" OR COLUMN_", setKey, sOperator, tempKey, "'")
                    End If
                    iCnt = iCnt + 1
                Next
            Else
                Dim stArrayData As String() = arrCutValue(valIdx + 1).ToString().Split(","c)
                For Each tempKey As String In stArrayData
                    '比較条件の変換
                    If tempKey.IndexOf("<>") >= 0 Then
                        sOperator = "<>'"
                        tempKey = tempKey.Replace("<>", "")
                    Else
                        sOperator = "='"
                    End If

                    Keys += String.Concat(" OR COLUMN_", setKey, sOperator, tempKey, "'")
                Next
            End If
            'インクリメント
            valIdx += 1
        Next

        ''SHINODA
        'If kbnDr3.Length > 0 Then
        '    Dim AddKeys As String = kbnDr3(0).Item("SET_NAIYO").ToString().Replace("@", "'")
        '    If Keys = String.Empty Then
        '        Keys = AddKeys
        '    Else
        '        Keys = "(" + Keys + ") AND " + AddKeys
        '    End If
        'End If
        ''SHINODA

        If valIdx <> -1 Then
            Dim dttmp As DataTable = rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Clone()

            drBucking = rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Select(Keys)

            For Each row As DataRow In drBucking
                dttmp.ImportRow(row)
            Next

            rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Clear()

            rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Merge(dttmp)
        End If

        Return rtDs

    End Function

#End Region

#Region "EDI取込詳細データセット(固定長)"
    ''' <summary>
    ''' EDI取込詳細(セミ標準対応)(固定長)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiFixedLengthStanderdEdition(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList) As DataSet

        'ふるい落とし機能の追加を忘れずに
        'ふるい落としキー格納用(キー番号がNullならやらない)

        arrCloser = New ArrayList

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH010C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty

        '固定長のサイズを取得
        Dim iLength As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT"))
        'ファイルを読み込むバイト型配列を作成する
        Dim bs(iLength - 1) As Char
        '外だしリーダ
        Dim cReader As System.IO.StreamReader = Nothing

        'EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1

            ' StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(String.Concat(drSemiEdiInfo.Item("RCV_INPUT_DIR"), dtHed.Rows(i).Item("FILE_NAME_RCV")), System.Text.Encoding.Default)

            'Closeコレクションにコレクト
            arrCloser.Add(DirectCast(cReader, System.IO.StreamReader))

            ' 読み込みできる文字がなくなるまで繰り返す
            While (cReader.Peek() >= 0)

                '先頭行飛ばしカウントの数だけ行を読み込む
                If gyoCount = 0 Then
                    For j As Integer = 0 To Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT")) - 1
                        'ファイルの内容を読み込む
                        cReader.Read(bs, 0, iLength)
                    Next
                End If

                ' ファイルを指定された文字数で読み込む
                cReader.Read(bs, 0, iLength)

                '読み込んだ文字列を結合する
                Dim stBuffer As String = ""
                For j As Integer = 0 To iLength - 1
                    stBuffer = stBuffer & bs(j)
                Next

                'データセットに登録
                gyoCount += 1
                dr = rtDs.Tables(LMH010C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"
                dr("COLUMN_1") = stBuffer

                rtDs.Tables(LMH010C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            'cReader.Close()

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH010_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            '行カウントをリセット
            gyoCount = 0
        Next

        Return rtDs

    End Function

#End Region

#Region "EDI取込詳細データセット(EXCEL対応)"
    ''' <summary>
    ''' 取込明細(セミ標準対応)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <param name="arrCloser"></param>
    ''' <param name="sheet">シート名またはシート番号（省略すれば従来通り1シート目を対象） 2019/07/18 依頼番号:006754 add</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiExcelStanderdEdition(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList, Optional ByVal sheet As Object = 1) As DataSet

        'Close関連 --ST--
        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList
        'Close関連 --ED--

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH010C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty
        Dim folderNm As String = String.Empty

        'ローカル作業                                      
        folderNm = drSemiEdiInfo.Item("RCV_INPUT_DIR").ToString()                                   'フォルダ名(ローカル)

        Dim rowNoMin As Integer = Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT").ToString) + 1   '行の開始数
        Dim colNoMax As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT").ToString)  '列の最大数
        Dim rowNoKey As Integer = 1                                                                 'Cashに登録されるまで、とりあえず１列目を設定

        '明細 DataTable の COLUMN_n の列数カウント(本実装時点で列数256)
        Dim maxColSuffix As Integer = 0
        Dim colSuffix As Integer
        For Each col As DataColumn In rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Columns
            If col.ColumnName.StartsWith("COLUMN_") AndAlso
                Integer.TryParse(col.ColumnName.Substring("COLUMN_".Length), colSuffix) Then
                If maxColSuffix < colSuffix Then
                    maxColSuffix = colSuffix
                End If
            End If
        Next
        If colNoMax > maxColSuffix Then
            '明細 DataTable の列の動的追加(本実装時点で列数256。既存機能への万一の影響の予防)
            For i As Integer = maxColSuffix + 1 To colNoMax
                rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Columns.Add(String.Concat("COLUMN_", i.ToString()), Type.GetType("System.String"))
            Next
        End If

        Dim fileNm As String = String.Empty

        '荷主明細取得
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()       '営業所コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString() '荷主コード(大)
        'Dim kbnDr92() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
        '                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '92' ")


        'ふるい落とし後データ格納用
        Dim drBucking() As DataRow = Nothing
        '-----------------------------------------------------------------------------------------------
        ' ふるい落とし設定
        '-----------------------------------------------------------------------------------------------
        'Function化期待の★
        'ふるい落とし機能の追加を忘れずに
        'ふるい落としキー格納用(キー番号がNullならいらない)
        Dim arrCutKey As ArrayList = New ArrayList
        'ふるい落とし条件格納用(キー番号がNullならいらない)
        Dim arrCutValue As ArrayList = New ArrayList

        'キー1
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("BUCKING_NO_1").ToString()) = False Then
            arrCutKey.Add(drSemiEdiInfo.Item("BUCKING_NO_1").ToString())
            arrCutValue.Add(drSemiEdiInfo.Item("BUCKING_TERM_1").ToString())
        End If
        'キー2
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("BUCKING_NO_2").ToString()) = False Then
            arrCutKey.Add(drSemiEdiInfo.Item("BUCKING_NO_2").ToString())
            arrCutValue.Add(drSemiEdiInfo.Item("BUCKING_TERM_2").ToString())
        End If
        'キー3
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("BUCKING_NO_3").ToString()) = False Then
            arrCutKey.Add(drSemiEdiInfo.Item("BUCKING_NO_3").ToString())
            arrCutValue.Add(drSemiEdiInfo.Item("BUCKING_TERM_3").ToString())
        End If

        '-----------------------------------------------------------------------------------------------
        ' EXCELファイル用
        '-----------------------------------------------------------------------------------------------
        Dim xlApp As Excel.Application = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim xlCell As Excel.Range = Nothing

        xlApp = New Excel.Application()

        xlBooks = xlApp.Workbooks

        'EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1

            ' EXCEL OPEN
            fileNm = dtHed.Rows(i).Item("FILE_NAME_RCV").ToString()
            Try
                xlBook = xlBooks.Open(String.Concat(folderNm, fileNm))
            Catch ex As Exception
                '例外がスローされたら処理強制終了
                rtDs.Tables("LMH010_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "2"  '2'は当PGについて例外エラーとする
                Return rtDs
            End Try

            'シート
            '2019/07/18 依頼番号:006754 mod start
            'xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)
            xlSheet = DirectCast(xlBook.Worksheets(sheet), Excel.Worksheet)
            xlSheet.Activate()
            '2019/07/18 依頼番号:006754 mod end

            xlApp.Visible = False

            '最大行を取得(rowNoKey列の最終入力行を取得)
            Dim rowNoMax As Integer = 0

            xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

            rowNoMax = xlApp.ActiveCell.Row

            '２次元配列に取得する
            Dim arrData(,) As Object
            arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

            '２次元→DSにセットする
            For j As Integer = rowNoMin To rowNoMax

                If arrData(j, rowNoKey) Is Nothing Then

                    Continue For
                Else
                    If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                        Continue For
                    End If
                End If

                gyoCount += 1
                dr = rtDs.Tables(LMH010C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount.ToString("00000")
                dr("ERR_FLG") = "9"

                'DSに格納
                For k As Integer = 1 To colNoMax
                    If arrData(j, k) Is Nothing Then
                        dr(String.Concat("COLUMN_", k.ToString)) = String.Empty
                    Else
                        dr(String.Concat("COLUMN_", k.ToString)) = arrData(j, k).ToString().Trim()
                    End If
                Next

                'DSにAdd
                rtDs.Tables(LMH010C.EDI_TORIKOMI_DTL).Rows.Add(dr)
            Next

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH010_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            Else

                '条件設定
                Dim valIdx As Integer = -1
                Dim Keys As String = String.Empty
                Dim iCnt As Integer = 0
                Dim sOperator As String = String.Empty

                '検索対象に"<>"が含まれている場合検索条件を"="から"<>"に変更
                For Each setKey As String In arrCutKey
                    If (valIdx + 1) = 0 Then
                        Dim stArrayData As String() = arrCutValue(valIdx + 1).ToString().Split(","c)
                        For Each tempKey As String In stArrayData
                            '比較条件の変換
                            If tempKey.IndexOf("<>") >= 0 Then
                                sOperator = "<>'"
                                tempKey = tempKey.Replace("<>", "")
                            Else
                                sOperator = "='"
                            End If

                            If iCnt = 0 Then
                                Keys += String.Concat("COLUMN_", setKey, sOperator, tempKey, "'")
                            Else
                                Keys += String.Concat(" OR COLUMN_", setKey, sOperator, tempKey, "'")
                            End If
                            iCnt = iCnt + 1
                        Next
                    Else
                        Dim stArrayData As String() = arrCutValue(valIdx + 1).ToString().Split(","c)
                        For Each tempKey As String In stArrayData
                            '比較条件の変換
                            If tempKey.IndexOf("<>") >= 0 Then
                                sOperator = "<>'"
                                tempKey = tempKey.Replace("<>", "")
                            Else
                                sOperator = "='"
                            End If

                            Keys += String.Concat(" OR COLUMN_", setKey, sOperator, tempKey, "'")
                        Next
                    End If
                    'インクリメント
                    valIdx += 1
                Next

                ''AND条件の追加
                'If kbnDr92.Length > 0 Then
                '    Dim AddKeys As String = kbnDr92(0).Item("SET_NAIYO").ToString().Replace("@", "'")
                '    If Keys = String.Empty Then
                '        Keys = AddKeys
                '    Else
                '        Keys = "(" + Keys + ") AND " + AddKeys
                '    End If
                'End If

                If valIdx <> -1 Then
                    Dim dttmp As DataTable = rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Clone()

                    drBucking = rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Select(Keys)

                    For Each row As DataRow In drBucking
                        dttmp.ImportRow(row)
                    Next

                    rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Clear()

                    rtDs.Tables("LMH010_EDI_TORIKOMI_DTL").Merge(dttmp)
                End If
            End If

            '行カウントをリセット
            gyoCount = 0

            '====== 試験的にコネクションをファンクション外で閉じる ======
            '====== 以下のものをNothing指定する ======

            '== Closeコネクション ==
            'colection = {"", "", "", "", ""}

            colection.Clear()
            colection.Add(xlCell)    '0
            colection.Add(xlSheet)   '1
            colection.Add(xlBook)    '2
            colection.Add(xlBooks)   '3
            colection.Add(xlApp)     '4

            arrCloser.Add(colection)

            '配列 In 配列(ジャグ配列)

            'If xlCell IsNot Nothing Then
            '    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
            '    xlCell = Nothing
            'End If

            'System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
            'xlSheet = Nothing

            'xlBook.Close(False) 'Excelを閉じる
            'System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
            'xlBook = Nothing

            'System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
            'xlBooks = Nothing

            'xlApp.DisplayAlerts = False
            'xlApp.Quit()
            'System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
            'xlApp = Nothing
            '====== 試験的にコネクションをファンクション外で閉じる ======

        Next

        Return rtDs

    End Function
#End Region


#Region "報告用EDI取消データセット"
    ''' <summary>
    ''' 報告用EDI取消データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataHoukokuEdiTorikeshi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow.ToString()

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "EDI取消⇒未登録データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTorikesiMitouroku(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1


                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績取消データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiTorikeshi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("JISSEKI_FLAG") = "9"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_SHORI_FLG") = "4"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績作成済⇒実績未データセット, 実績送信済⇒実績未データセット"
    ''' <summary>
    ''' 実績作成済⇒実績未データセット, 実績送信済⇒実績未データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekimi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim inkaNo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "0"
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "0"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("INKA_JISSEKI_FLG") = "0"                '入荷実績作成フラグ（日合で使用）

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_SHORI_FLG") = "1"
                dr("INKA_JISSEKI_FLG") = "0"                '入荷実績作成フラグ（日合で使用）

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

                'LMH010_INKA_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKA_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("INKA_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))

                inkaNo = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))
                jissekiFlg = Convert.ToInt32(Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_JISSEKI.ColNo)))
                If jissekiFlg = 1 OrElse jissekiFlg = 4 OrElse (jissekiFlg = 2 AndAlso String.IsNullOrEmpty(inkaNo) = False) Then
                    dr("INKA_STATE_KB") = "50"
                Else
                    dr("INKA_STATE_KB") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_STATE_KB.ColNo))
                End If

                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_INKA_L).Rows.Add(dr)

                'LMH010_EDI_SND
                dr = rtDs.Tables(LMH010C.TABLE_NM_SEND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH010C.TABLE_NM_SEND).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績送信済⇒送信未データセット"
    ''' <summary>
    ''' 実績送信済⇒送信未データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSousinmi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim inkaNo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)


                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

                'LMH010_EDI_SND
                dr = rtDs.Tables(LMH010C.TABLE_NM_SEND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH010C.TABLE_NM_SEND).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績送信済⇒実績未データセット"
    ''' <summary>
    ''' 実績送信済⇒実績未データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSousinJissekimi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim inkaNo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("INKA_JISSEKI_FLG") = "0"                '入荷実績作成フラグ（日合で使用）

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"
                dr("INKA_JISSEKI_FLG") = "0"                '入荷実績作成フラグ（日合で使用）

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

                'LMH010_EDI_SND
                dr = rtDs.Tables(LMH010C.TABLE_NM_SEND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH010C.TABLE_NM_SEND).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "入荷取消⇒未登録データセット"
    ''' <summary>
    ''' 入荷取消⇒未登録データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTourokumi(ByVal frm As LMH010F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim inkaNo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010IN
                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                '2012.02.25 大阪対応 START
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.INKA_UPD_TIME.ColNo))
                dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))
                dr("MATOME_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.MATOME_NO.ColNo))
                dr("AUTO_MATOME_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.AUTO_MATOME_FLG.ColNo))
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)

                'LMH010_INKAEDI_L
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))
                dr("OUT_FLAG") = "0"
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)

                'LMH010_INKAEDI_M
                dr = rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))
                'dr("INKA_CTL_NO_M") = String.Empty
                'dr("OUT_KB") = "0"

                rtDs.Tables(LMH010C.TABLE_NM_INKAEDI_M).Rows.Add(dr)

                'LMH010_RCV_HED
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))

                rtDs.Tables(LMH010C.TABLE_NM_RCV_HED).Rows.Add(dr)

                'LMH010_RCV_DTL
                dr = rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.KANRI_NO.ColNo))
                'dr("INKA_CTL_NO_M") = "000"

                rtDs.Tables(LMH010C.TABLE_NM_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

    '要望番号1061 2012.05.15 追加START
#Region "CSV作成・出力(出力済)データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataOutputZumi(ByVal frm As LMH010F, ByVal rtDs As DataSet) As DataSet

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim row As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH010OUTPUTIN
                dr = rtDs.Tables(LMH010C.TABLE_NM_OUTPUTIN).NewRow()
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))
                dr("OUTPUT_SHUBETU") = frm.cmbOutput.SelectedValue.ToString()
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                '2012.05.29 要望番号1077 追加START
                dr("ORDER_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.ORDER_NO.ColNo))
                '2012.05.29 要望番号1077 追加END
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("PRTFLG") = "1"
                dr("INOUT_KB") = "1"
                dr("ROW_NO") = selectRow
                rtDs.Tables(LMH010C.TABLE_NM_OUTPUTIN).Rows.Add(dr)

            Next

        End With

        rtDs = MyBase.CallWSA("LMH010BLF", "SetDsPrtData", rtDs)

        Return rtDs

    End Function

#End Region
    '要望番号1061 2012.05.15 追加END

#Region "初期荷主変更データセット"
    ''' <summary>
    ''' データセット設定(LMZ010引数格納)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMZ010InData(ByVal frm As LMH010F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ010DS()
        Dim dr As DataRow = ds.Tables(LMZ010C.TABLE_NM_IN).NewRow()
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        dr("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        dr("USER_CD") = LM.Base.LMUserInfoManager.GetUserID()
        ds.Tables(LMZ010C.TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function
#End Region

#Region "検索成功時"
    ''' <summary>
    ''' 検索成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMH010F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMH010C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        ''SPREAD(表示行)初期化
        'frm.sprEdiList.CrearSpread()

        '進捗区分名の設定
        dt = Me.StatusSet(dt)


        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Call Me._G.SetSpreadVisible()

        Call Me._G.SetSpreadColor(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        'データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        'カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})

        End If


    End Sub

    Private Function StatusSet(ByVal dt As DataTable) As DataTable
        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max
            dr = dt.Rows(i)

            If dr("INKA_DEL_FLG").ToString().Equals("1") Then

                '↓FFEM特殊処理↓
                '2014.06.09 追加START
                If dr("JISSEKI_FLAG").ToString().Equals("1") Then
                    dr("INKA_STATE_NM") = "作済/入荷取消"
                ElseIf dr("JISSEKI_FLAG").ToString().Equals("2") Then
                    dr("INKA_STATE_NM") = "送済/入荷取消"
                Else
                    dr("INKA_STATE_NM") = "入荷取消"
                End If
                '↑FFEM特殊処理↑
                '2014.06.09 追加END

            ElseIf dr("JISSEKI_FLAG").ToString().Equals("1") Then
                dr("INKA_STATE_NM") = "実績作成済"
            ElseIf dr("JISSEKI_FLAG").ToString().Equals("2") Then
                dr("INKA_STATE_NM") = "実績送信済"

            ElseIf String.IsNullOrEmpty(dr("INKA_STATE_NM").ToString()) = True Then
                If dr("SYS_DEL_FLG").ToString().Equals("1") Then
                    If dr("DEL_KB").ToString().Equals("2") Then
                        dr("INKA_STATE_NM") = "キャンセル"
                    Else
                        dr("INKA_STATE_NM") = "EDI取消"
                    End If
                Else

                    If dr("OUT_FLAG").ToString().Equals("2") = True Then
                        If dr("JISSEKI_FLAG").ToString().Equals("9") = True Then
                            dr("INKA_STATE_NM") = "実績対象外"
                        Else
                            dr("INKA_STATE_NM") = "実績未"
                        End If
                    Else
                        dr("INKA_STATE_NM") = "EDI"
                    End If
                End If

            End If

        Next

        Return dt

    End Function

#End Region

#Region "入荷登録成功時"
    ''' <summary>
    ''' 入荷登録成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SuccessInkaToroku(ByVal frm As LMH010F)

        MyBase.ShowMessage(frm, "G002", New String() {"入荷登録", String.Empty})
    End Sub

#End Region

#Region "実績作成成功時"
    ''' <summary>
    ''' 実績作成成功処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJissekiSakusei(ByVal frm As LMH010F, ByVal ds As DataSet)

        MyBase.ShowMessage(frm, "G002", New String() {"実績作成", String.Empty})
    End Sub

#End Region

#Region "実績取消成功時"
    ''' <summary>
    ''' 実績取消成功処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJissekiTorikesi(ByVal frm As LMH010F, ByVal ds As DataSet)

        MyBase.ShowMessage(frm, "G002", New String() {"実績取消", String.Empty})
    End Sub

#End Region

#Region "EDI取消成功時"
    ''' <summary>
    ''' EDI取消時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessEdiTorikesi(ByVal frm As LMH010F, ByVal ds As DataSet)

        '■要望番号972対応 2012/05/14 Honmyo Start
        'MyBase.ShowMessage(frm, "G002", New String() {"EDI取消", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {"EDI取消", String.Concat("(", ds.Tables("LMH010_INKAEDI_L").Rows.Count, "件)")})
        '■要望番号972対応 2012/05/14 Honmyo End

    End Sub

#End Region

#Region "EDI取消⇒未登録成功時"
    ''' <summary>
    ''' EDI取消⇒未登録成功時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessTorikesiMitouroku(ByVal frm As LMH010F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"EDI取消⇒未登録", String.Empty})
    End Sub

#End Region

#Region "実績報告用EDIデータ取消成功時"
    ''' <summary>
    ''' 実績報告用EDIデータ取消成功
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessHoukokuEdiTorikesi(ByVal frm As LMH010F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"実績報告用EDIデータ取消", String.Empty})
    End Sub

#End Region

#Region "実行処理成功時"
    ''' <summary>
    '''実行成功時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJikkou(ByVal frm As LMH010F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"実行処理", String.Empty})
    End Sub

#End Region

    '2015.04.13 追加START
#Region "取込処理成功時"
    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">取込処理データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessTorikomi(ByVal frm As LMH010F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"取込処理", String.Empty})
    End Sub

#End Region
    '2015.04.13 追加END

    '未着・装着ファイル作成対応 Start
#Region "未着・早着ファイル作成成功時"
    ''' <summary>
    '''未着・早着ファイル作成成功時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessOutputMisoutyakuFile(ByVal frm As LMH010F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"未着・早着ファイル作成", String.Empty})
    End Sub

#End Region
    '未着・装着ファイル作成対応 End

    '2015.09.04 tsunehira add
#Region "一括変更成功時"
    ''' <summary>
    ''' 一括変更成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SuccessBulkChg(ByVal frm As LMH010F)

        MyBase.ShowMessage(frm, "G002", New String() {"荷主一括変更", String.Empty})
    End Sub

#End Region

#Region "一括変更失敗時"
    ''' <summary>
    ''' 一括変更失敗時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FailuerBulkChg(ByVal frm As LMH010F)

        MyBase.ShowMessage(frm, "E547", New String() {"変更対象外荷主", String.Empty})
    End Sub

#End Region

#Region "POP"
    ''' <summary>
    ''' マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowPopup(ByVal frm As LMH010F, ByVal objNM As String, ByVal prm As LMFormData, ByVal actionType As LMH010C.EventShubetsu)

        If Me._V.IsPopSingleCheck(objNM) = False Then
            Exit Sub
        End If

        Select Case objNM

            Case "txtCustCD_L", "txtCustCD_M"                    '荷主マスタ参照

                With frm

                    If String.IsNullOrEmpty(.txtCustCD_L.TextValue) = True Then
                        .lblCustNM_L.TextValue = String.Empty
                        .lblCustNM_M.TextValue = String.Empty
                    End If

                    Dim prmDs As DataSet = New LMZ260DS
                    Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue().ToString()
                    'START SHINOHARA 要望番号513
                    If actionType = LMH010C.EventShubetsu.ENTER Then
                        'END SHINOHARA 要望番号513
                        row("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim()
                        row("CUST_CD_M") = frm.txtCustCD_M.TextValue.Trim()
                        'START SHINOHARA 要望番号513
                    End If
                    'END SHINOHARA 要望番号513
                    row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                    row("HYOJI_KBN") = LMZControlC.HYOJI_S
                    prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                    prm.ParamDataSet = prmDs
                    prm.SkipFlg = Me._PopupSkipFlg

                    'POP呼出
                    LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

                End With

        End Select


    End Sub

    Private Sub SetMstResult(ByVal frm As LMH010F, ByVal objNM As String, ByVal prm As LMFormData)

        '戻り処理
        Select Case objNM

            Case "txtCustCD_L", "txtCustCD_M"

                'PopUpから取得したデータをコントロールにセット
                With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                    frm.txtCustCD_L.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                    frm.lblCustNM_L.TextValue = .Item("CUST_NM_L").ToString    '荷主名（大）
                    frm.txtCustCD_M.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                    frm.lblCustNM_M.TextValue = .Item("CUST_NM_M").ToString    '荷主名（大）
                End With

        End Select

    End Sub
#End Region

#Region "LMH020呼出データセット"
    ''' <summary>
    ''' データセット設定(LMH020引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMH020InData(ByVal frm As LMH010F, ByVal rowno As Integer) As DataSet


        'DataSet設定
        Dim ds As DataSet = New LMH020DS()
        Dim dr As DataRow = ds.Tables(LMH020C.TABLE_NM_IN).NewRow()

        With frm.sprEdiList.ActiveSheet

            '要望管理番号 2506 tsunehira add start

            'dr.Item("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(rowno, LMH010C.SprColumnIndex.EDI_NO))
            'dr.Item("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(rowno, LMH010C.SprColumnIndex.NRS_BR_CD))
            'dr.Item("NRS_WH_CD") = Me._LMHconV.GetCellValue(.Cells(rowno, LMH010C.SprColumnIndex.WH_CD))
            'dr.Item("MATOME_FLG") = "0"
            'dr.Item("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(rowno, LMH010C.SprColumnIndex.CUST_CD_L))
            'dr.Item("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(rowno, LMH010C.SprColumnIndex.CUST_CD_M))
            'dr.Item("INOUT_KB") = "1"

            dr.Item("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(rowno, _G.sprEdiListDef.EDI_NO.ColNo))
            dr.Item("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(rowno, _G.sprEdiListDef.NRS_BR_CD.ColNo))
            dr.Item("NRS_WH_CD") = Me._LMHconV.GetCellValue(.Cells(rowno, _G.sprEdiListDef.WH_CD.ColNo))
            dr.Item("MATOME_FLG") = "0"
            dr.Item("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(rowno, _G.sprEdiListDef.CUST_CD_L.ColNo))
            dr.Item("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(rowno, _G.sprEdiListDef.CUST_CD_M.ColNo))
            dr.Item("INOUT_KB") = "1"

            '要望管理番号 2506 tsunehira add end

        End With

        ds.Tables(LMH020C.TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function

#End Region

#Region "ワーニング画面呼出処理"
    Private Sub CallWarning(ByVal ds As DataSet, ByVal frm As LMH010F, ByVal eventShubetsu As Integer)

        Dim drW As DataRow = ds.Tables(LMH010C.TABLE_NM_WARNING_HED).NewRow()
        Dim drIN As DataRow = ds.Tables(LMH010C.TABLE_NM_IN).Rows(0)

        Dim prm As LMFormData = New LMFormData

        Select Case eventShubetsu
            Case LMH010C.EventShubetsu.TOROKU
                drW.Item("SYORI_KB") = LMH050C.SHORI_INKA_TOROKU '入荷登録

            Case LMH010C.EventShubetsu.JISSEKI_SAKUSE
                drW.Item("SYORI_KB") = LMH050C.SHORI_INKA_JISSEKI '実績作成

                '2015.09.04 tsunehria add???
                'Case LMH010C.EventShubetsu.BULK_CUST_CHANGE
                '    drW.Item("SYORI_KB") = LMH050C. '実績作成

            Case Else

        End Select

        drW.Item("NRS_BR_CD") = drIN("NRS_BR_CD")
        drW.Item("WH_CD") = drIN("WH_CD")
        drW.Item("CUST_CD_L") = drIN("CUST_CD_L")
        drW.Item("CUST_CD_M") = drIN("CUST_CD_M")

        ds.Tables(LMH010C.TABLE_NM_WARNING_HED).Rows.Add(drW)

        prm.ParamDataSet = ds
        LMFormNavigate.NextFormNavigate(Me, "LMH050", prm)

    End Sub

#End Region

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables(LMH010C.TABLE_NM_GUIERROR).Rows(i)

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

#End Region

#Region "EXCEL出力"
    Private Sub OutputExcel(ByVal frm As LMH010F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub
#End Region

#Region "ファイルコピー"

    ''' <summary>
    '''  処理正常時ファイルコピー
    ''' </summary>
    ''' <param name="tgtFile"></param>
    ''' <param name="CopyTOFolder"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CopyAndDelete(ByVal tgtFile As String, ByVal CopyTOFolder As String, ByVal rtnDs As DataSet) As Boolean

        Try
            '上書きOKとしてコピー可能
            If rtnDs.Tables("LMH820_H_INKAEDI_HED_UTI").Rows.Count = 0 Then
                Dim sysdate As String = Mid(MyBase.GetSystemDateTime(0), 1, 8)
                Dim systime As String = Mid(MyBase.GetSystemDateTime(1), 1, 9)
                System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", sysdate, "_", systime, "_INVOICE.xlsx"), True)
            Else
                System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", rtnDs.Tables("LMH820_H_INKAEDI_HED_UTI").Rows(0).Item("FILE_NAME").ToString()), True)
            End If

            System.IO.File.Delete(tgtFile)

        Catch ex As Exception
            Me.SetMessage("S002")
        End Try

    End Function

#End Region

#Region "CSV作成"

    ''' <summary>
    ''' CSV作成(未着・早着ファイル作成(UTI))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>未着・早着ファイル作成UTI報告用</remarks>
    Private Function MakeMiSoucyakuCSV(ByVal frm As LMH010F, ByVal ds As DataSet) As Boolean

        If ds.Tables("LMH830_MICYAKU_OUT").Rows.Count = 0 AndAlso ds.Tables("LMH830_SOUCYAKU_OUT").Rows.Count = 0 Then
            Return False
        End If

        Dim strData As String = String.Empty
        Dim torikomiDate As String = frm.imdOutputDateFrom.TextValue.ToString()
        Dim max As Integer = 0

        Dim mckFlg As Integer = 0

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()

        '1回目(未着ファイル作成)
        '2回目(早着ファイル作成)
        For i As Integer = 0 To 1

            '初期化
            If i = 1 Then
                strData = String.Empty
                setData = New StringBuilder()
            End If

            'EDI取込日
            strData = String.Concat("入荷確認データ処理日:", torikomiDate)
            setData.Append(String.Concat("""", strData, """", ","))

            setData.Append(vbNewLine)

            '固定文字(タイトル)
            If i = 0 Then
                'strData = String.Concat("未着データ:Delivery №", ",", "CONSIGNEEコード", ",", "商品コード", ",", "個数", ",", "ロット№")
                strData = String.Concat("未着データ:Delivery №", ",", "CONSIGNEEコード", ",", "商品コード", ",", "商品名", ",", "ロット№", ",", "データ取込日")
                max = ds.Tables("LMH830_MICYAKU_OUT").Rows.Count - 1
            ElseIf i = 1 Then
                'strData = String.Concat("早着データ:Delivery №", ",", "CONSIGNEEコード", ",", "商品コード", ",", "個数", ",", "ロット№")
                strData = String.Concat("早着データ:Delivery №", ",", "CONSIGNEEコード", ",", "商品コード", ",", "ロット№")
                max = ds.Tables("LMH830_SOUCYAKU_OUT").Rows.Count - 1
            End If

            If max < 0 Then
                strData = String.Empty
                Continue For

            End If

            '未着・早着データが存在しない時
            If i = 0 AndAlso ds.Tables("LMH830_MICYAKU_OUT").Rows.Count = 0 Then
                MyBase.SetMessage("E531", New String() {"未着"})
                mckFlg = 1
            End If

            If i = 1 AndAlso ds.Tables("LMH830_SOUCYAKU_OUT").Rows.Count = 0 Then
                If mckFlg = 1 Then
                    MyBase.SetMessage("E531", New String() {"未着・早着"})
                    Return True
                Else
                    MyBase.SetMessage("E531", New String() {"早着"})
                End If

            End If

            setData.Append(String.Concat("""", strData, """", ","))
            setData.Append(vbNewLine)


            'Delivery №
            If i = 0 Then

                For j As Integer = 0 To max

                    'DELIVERY_NO
                    If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DELIVERY_NO").ToString) = False Then
                        strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DELIVERY_NO").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    'DCT_CONSIGNEE
                    If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_CONSIGNEE").ToString) = False Then
                        strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_CONSIGNEE").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    'GOODS_CD
                    If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_GOODS_CD").ToString) = False Then
                        strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_GOODS_CD").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    'GOODS_NM
                    If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_GOODS_NM").ToString) = False Then
                        strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_GOODS_NM").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    ''個数
                    'If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_KOSU").ToString) = False Then
                    '    strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_KOSU").ToString
                    'Else
                    '    strData = String.Empty
                    'End If

                    'setData.Append(String.Concat("""", strData, """", ","))

                    'LOT_NO
                    If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_LOT_NO").ToString) = False Then
                        strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("DCT_LOT_NO").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    '取込日
                    If String.IsNullOrEmpty(ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("CRT_DATE").ToString) = False Then
                        strData = ds.Tables("LMH830_MICYAKU_OUT").Rows(j).Item("CRT_DATE").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    setData.Append(vbNewLine)

                Next

            ElseIf i = 1 Then

                For j As Integer = 0 To max

                    'DELIVERY_NO
                    If String.IsNullOrEmpty(ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("SERIAL_NO").ToString) = False Then
                        strData = ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("SERIAL_NO").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    'DCT_CONSIGNEE
                    If String.IsNullOrEmpty(ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_CONSIGNEE").ToString) = False Then
                        strData = ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_CONSIGNEE").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    'GOODS_CD
                    If String.IsNullOrEmpty(ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_GOODS_CD").ToString) = False Then
                        strData = ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_GOODS_CD").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))

                    ''個数
                    'If String.IsNullOrEmpty(ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_KOSU").ToString) = False Then
                    '    strData = ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_KOSU").ToString
                    'Else
                    '    strData = String.Empty
                    'End If

                    'setData.Append(String.Concat("""", strData, """", ","))

                    'LOT_NO
                    If String.IsNullOrEmpty(ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_LOT_NO").ToString) = False Then
                        strData = ds.Tables("LMH830_SOUCYAKU_OUT").Rows(j).Item("DCT_LOT_NO").ToString
                    Else
                        strData = String.Empty
                    End If

                    setData.Append(String.Concat("""", strData, """", ","))
                    setData.Append(vbNewLine)

                Next

            End If



            '保存先のCSVファイルのパス
#If False Then  'UPD 2019/01/11 依頼番号 : 004191   【LMS】東レDOW・デュポン分社化_DSV入荷EDI対応
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F006' AND ", _
                                                                                                            "KBN_CD = '01'"))

#Else
            '営業所・荷主ごと設定出来るように変更
            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()          '営業所コード
            Dim custCdL As String = frm.txtPrt_CustCD_L.TextValue.ToString()    '荷主コード(大)
            Dim custCdM As String = frm.txtPrt_CustCD_M.TextValue.ToString()    '荷主コード(中)

            Dim sSQL As String = String.Concat("KBN_GROUP_CD = 'F006' AND  KBN_NM6 = '" & brCd.ToString, "' AND KBN_NM7 = '" & custCdL.ToString, "' AND KBN_NM8 = '" & custCdM.ToString, "'")
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sSQL)

#End If
            If kbnDr.Length = 0 Then
                Return False
            End If

            Dim CSVPath As String = String.Empty

#If False Then   'ADD 2019/02/12 依頼番号 : 004676   【LMS】入荷EDI_早着未着ファイル作成時、ファイル名に荷主名を追加
                        If i = 0 Then
                '未着ファイル保存先
                CSVPath = String.Concat(kbnDr(0).Item("KBN_NM2").ToString, _
                                                      "(未着)取込日(", torikomiDate, _
                                                      ")_", _           
                                                      Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                      Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                      ".csv")
            ElseIf i = 1 Then

                '早着ファイル保存先
                CSVPath = String.Concat(kbnDr(0).Item("KBN_NM3").ToString, _
                                                      "(早着)取込日(", torikomiDate, _
                                                      ")_", _
                                                      Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                      Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                      ".csv")


            End If

            Dim CSVBackUpPath As String = String.Empty

            If i = 0 Then

                'バックアップファイル保存先
                CSVBackUpPath = String.Concat(kbnDr(0).Item("KBN_NM4").ToString, _
                                                            "(未着)取込日(", torikomiDate, _
                                                            ")_", _
                                                            Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                            Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                            ".csv")

            ElseIf i = 1 Then

                'バックアップファイル保存先
                CSVBackUpPath = String.Concat(kbnDr(0).Item("KBN_NM4").ToString, _
                                                            "(早着)取込日(", torikomiDate, _
                                                            ")_", _
                                                            Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                            Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                            ".csv")

            End If

#Else
            Dim sCustNM As String = kbnDr(0).Item("KBN_NM9").ToString

            If i = 0 Then
                '未着ファイル保存先
                CSVPath = String.Concat(kbnDr(0).Item("KBN_NM2").ToString, _
                                                      "(未着)取込日(", torikomiDate, _
                                                      ")_", _
                                                      sCustNM.ToString, "_", _
                                                      Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                      Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                      ".csv")
            ElseIf i = 1 Then

                '早着ファイル保存先
                CSVPath = String.Concat(kbnDr(0).Item("KBN_NM3").ToString, _
                                                      "(早着)取込日(", torikomiDate, _
                                                      ")_", _
                                                      sCustNM.ToString, "_", _
                                                      Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                      Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                      ".csv")


            End If

            Dim CSVBackUpPath As String = String.Empty

            If i = 0 Then

                'バックアップファイル保存先
                CSVBackUpPath = String.Concat(kbnDr(0).Item("KBN_NM4").ToString, _
                                                            "(未着)取込日(", torikomiDate, _
                                                            ")_", _
                                                            sCustNM.ToString, "_", _
                                                            Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                            Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                            ".csv")

            ElseIf i = 1 Then

                'バックアップファイル保存先
                CSVBackUpPath = String.Concat(kbnDr(0).Item("KBN_NM4").ToString, _
                                                            "(早着)取込日(", torikomiDate, _
                                                            ")_", _
                                                            sCustNM.ToString, "_", _
                                                            Mid(MyBase.GetSystemDateTime(0), 1, 8), _
                                                            Mid(MyBase.GetSystemDateTime(1), 1, 6), _
                                                            ".csv")

            End If

#End If
            'CSVファイルに書き込むときに使うEncoding
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

            If i = 0 Then
                '未着ファイルを開く
                System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM2").ToString)
            ElseIf i = 1 Then
                '早着ファイルを開く
                System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM3").ToString)
            End If

            Dim sr As StreamWriter = New StreamWriter(CSVPath, False, enc)

            '値の設定
            sr.Write(setData.ToString())

            'ファイルを閉じる
            sr.Close()

            'ファイルを開く(バックアップファイル)
            System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM4").ToString)
            sr = New StreamWriter(CSVBackUpPath, False, enc)
            '値の設定
            sr.Write(setData.ToString())
            'ファイルを閉じる
            sr.Close()

        Next


        Return True

    End Function

#End Region

#Region "キャッシュ"
    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMH010F)

        Call Me.SetCachedNameCust(frm)

        Call Me.SetCachedNameTanto(frm)
        'With frm

        '    Dim custCdL As String = .txtCustCD_L.TextValue
        '    Dim custCdM As String = .txtCustCD_M.TextValue

        '    '荷主名称
        '    .lblCustNM_L.TextValue = String.Empty
        '    .lblCustNM_M.TextValue = String.Empty
        '    If String.IsNullOrEmpty(custCdL) = False Then
        '        If String.IsNullOrEmpty(custCdM) = True Then
        '            custCdM = "00"
        '        End If

        '        Dim custDr() As DataRow = Me._LMHconG.SelectCustListDataRow(custCdL, custCdM)

        '        If 0 < custDr.Length Then
        '            .lblCustNM_L.TextValue = custDr(0).Item("CUST_NM_L").ToString()
        '            .lblCustNM_M.TextValue = custDr(0).Item("CUST_NM_M").ToString()
        '        End If
        '    End If

        '    Dim tantoNM As String = .txtTantouCd.TextValue

        '    '担当者名称
        '    .lblTantouNM.TextValue = String.Empty
        '    If String.IsNullOrEmpty(tantoNM) = False Then

        '        Dim tantoDr() As DataRow = Me._LMHconG.SelectTantouListDataRow(tantoNM)

        '        If 0 < tantoDr.Length Then
        '            .lblTantouNM.TextValue = tantoDr(0).Item("USER_NM").ToString()

        '        End If
        '    End If

        'End With

    End Sub

    Private Sub SetCachedNameCust(ByVal frm As LMH010F)

        With frm

            Dim custCdL As String = .txtCustCD_L.TextValue
            Dim custCdM As String = .txtCustCD_M.TextValue

            '荷主名称
            .lblCustNM_L.TextValue = String.Empty
            .lblCustNM_M.TextValue = String.Empty
            If String.IsNullOrEmpty(custCdL) = False Then
                If String.IsNullOrEmpty(custCdM) = True Then
                    custCdM = "00"
                End If

                '2016.02.18 要望番号2491 修正START
                'Dim custDr() As DataRow = Me._LMHconG.SelectCustListDataRow(custCdL, custCdM)
                Dim custDr() As DataRow = Me._LMHconG.SelectCustListDataRow(Convert.ToString(.cmbEigyo.SelectedValue()), custCdL, custCdM)
                '2016.02.18 要望番号2491 修正END

                If 0 < custDr.Length Then
                    .lblCustNM_L.TextValue = custDr(0).Item("CUST_NM_L").ToString()
                    .lblCustNM_M.TextValue = custDr(0).Item("CUST_NM_M").ToString()
                End If
            End If
        End With
    End Sub

    Public Sub SetCachedNameTanto(ByVal frm As LMH010F)

        With frm

            Dim tantoNM As String = .txtTantouCd.TextValue

            '担当者名称
            .lblTantouNM.TextValue = String.Empty
            If String.IsNullOrEmpty(tantoNM) = False Then

                Dim tantoDr() As DataRow = Me._LMHconG.SelectTantouListDataRow(tantoNM)

                If 0 < tantoDr.Length Then
                    .lblTantouNM.TextValue = tantoDr(0).Item("USER_NM").ToString()

                End If
            End If

        End With

    End Sub

#End Region

    '2015.09.03 tsunehira add
#Region "一括変更データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SetDataBulkChgData(ByVal frm As LMH010F, ByVal eventShubetsu As Integer, ByVal ds As DataSet, ByVal errHashTable As Hashtable) As DataSet

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet
            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))
                dr = ds.Tables(LMH010C.TABLE_NM_INKAEDI_L).NewRow()
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("CUST_CD_L") = Left(Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CHG_CUST_CD.ColNo)), 5) '右から5文字の切り出し
                dr("CUST_CD_M") = Right(Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CHG_CUST_CD.ColNo)), 2) '左から2文字の切り出し

                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                'データをデータセットに設定
                ds.Tables(LMH010C.TABLE_NM_INKAEDI_L).Rows.Add(dr)
            Next
        End With

        dr = ds.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        ds.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

        Return ds

    End Function


#End Region





#Region "実行(M品振替出荷処理)"


    ''' <summary>
    ''' 実行(M品振替出荷処理)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="eventshubetsu"></param>
    ''' <param name="errHashtable"></param>
    ''' <param name="errDs"></param>
    ''' <remarks></remarks>
    Private Sub TransferCondM(ByVal frm As LMH010F _
                            , ByVal eventshubetsu As LMH010C.EventShubetsu _
                            , ByVal errHashtable As Hashtable _
                            , ByVal errDs As DataSet)

        Try

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name _
                                 , Reflection.MethodBase.GetCurrentMethod().Name)



            Using ds As New LMH010DS

                Me.SetDataTransferItemMInkaData(frm, ds, eventshubetsu, errHashtable)

                Const blfName As String = "LMH010BLF"
                Const funcName As String = "TransferCondM"

                If (Me.ShowMessage(frm, "C001", New String() {frm.cmbJikkou.SelectedText}) <> MsgBoxResult.Ok) Then
                    Me.ShowMessage(frm, "G007")
                    Return
                End If

                ' 事前にクライアントでエラーとなった行の情報を格納
                If (errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Count > 0) Then
                    Me.ExcelErrorSet(errDs)
                End If

                '==== WSAクラス呼出 ====
                Using result As DataSet = MyBase.CallWSA(blfName, funcName, ds)

                    'メッセージコードの判定
                    If (MyBase.IsMessageStoreExist) Then
                        Me.OutputExcel(frm)
                    Else

                        MyBase.ShowMessage(frm _
                                         , "G002" _
                                         , New String() {frm.cmbJikkou.SelectedItem.Text, String.Empty})
                    End If


                End Using
            End Using


        Catch ex As Exception

            MyBase.Logger.WriteErrorLog(MyBase.GetType.Name _
                                      , Reflection.MethodBase.GetCurrentMethod().Name _
                                      , ex.Message _
                                      , ex)


            Me.ShowMessage(frm, "S002")

        End Try

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name _
                           , Reflection.MethodBase.GetCurrentMethod().Name)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTransferItemMInkaData(ByVal frm As LMH010F _
                                           , ByVal rtDs As DataSet _
                                           , ByVal eventShubetsu As Integer _
                                           , ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0


        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                dr = rtDs.Tables(LMH010C.TABLE_NM_IN).NewRow()
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.WH_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("RCV_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_DATE.ColNo))
                dr("RCV_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.RCV_UPD_TIME.ColNo))
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.CUST_CD_M.ColNo))
                dr("ORDER_CHECK_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.ORDER_CHECK_FLG.ColNo))
                dr("ROW_NO") = selectRow.ToString()
                dr("AUTO_MATOME_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, _G.sprEdiListDef.AUTO_MATOME_FLG.ColNo))
                dr("EVENT_SHUBETSU") = eventShubetsu

                rtDs.Tables(LMH010C.TABLE_NM_IN).Rows.Add(dr)


            Next

            dr = rtDs.Tables(LMH010C.TABLE_NM_JUDGE).NewRow()
            dr("EVENT_SHUBETSU") = eventShubetsu
            rtDs.Tables(LMH010C.TABLE_NM_JUDGE).Rows.Add(dr)

        End With

    End Sub








#End Region






#End Region '内部Method

#Region "イベント定義(一覧)"
    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMH010F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(入荷登録)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")
        '検索処理
        Me.ActionControl(LMH010C.EventShubetsu.TOROKU, frm)
        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")
        '実績作成処理
        Me.ActionControl(LMH010C.EventShubetsu.JISSEKI_SAKUSE, frm)
        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")
        Me.ActionControl(LMH010C.EventShubetsu.HIMODUKE, frm)
        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")
        'EDI取消
        Me.ActionControl(LMH010C.EventShubetsu.EDI_TORIKESI, frm)
        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Torikomi")

        Call Me.ActionControl(LMH010C.EventShubetsu.TORIKOMI, frm)

        Logger.EndLog(Me.GetType.Name, "Torikomi")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Import JDE")

        ''Import JDE
        'Me.sendPrint(frm, e)

        Logger.EndLog(Me.GetType.Name, "Import JDE")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        '検索処理
        Me.ActionControl(LMH010C.EventShubetsu.JISSEKI_TORIKESI, frm)

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "SelecListEvent")

        '検索処理
        Me.ActionControl(LMH010C.EventShubetsu.KENSAKU, frm)

        'Logger.EndLog(Me.GetType.Name, "SelectListEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False
            Me.ActionControl(LMH010C.EventShubetsu.ENTER, frm)

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True
            Me.ActionControl(LMH010C.EventShubetsu.MASTER, frm)

        End If


        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")
        '初期荷主変更
        Me.ActionControl(LMH010C.EventShubetsu.DEF_CUST, frm)
        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMH010F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    ''''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    Friend Sub sprCellDoubleClick(ByVal frm As LMH010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "RowSelection")

        '「ダブルクリック」処理
        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    Friend Sub btnJikkou_Click(ByRef frm As LMH010F)

        Dim jikkouShubetsu As String = frm.cmbJikkou.SelectedValue.ToString

        Select Case jikkouShubetsu
            Case "01"
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU, frm)

            Case "02"
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI, frm)

            Case "03"
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI, frm)

            Case "04"
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI, frm)

            Case "05"
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI, frm)
            Case "06"
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU, frm)

            Case "07"
                Me.ActionControl(LMH010C.EventShubetsu.COA_TOUROKU, frm)

                '入荷確認ファイル取込
            Case "08"
                Me.ActionControl(LMH010C.EventShubetsu.INKA_CONF_TORIKOMI, frm)

                '確認データ削除
            Case "09"
                Me.ActionControl(LMH010C.EventShubetsu.CONF_DEL, frm)

            Case LMH010C.JIKKOU_SHUBETSU.TRANSFER_COND_M
                ' M品一括振替
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_TRANSFER_COND_M, frm)

                'ADD START 2019/9/12 依頼番号:007111
            Case "11"
                ' 現品票印刷
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_GENPIN_PRINT, frm)

            Case "12"
                ' 現品票再印刷
                Me.ActionControl(LMH010C.EventShubetsu.JIKKOU_GENPIN_REPRINT, frm)
                'ADD END 2019/9/12 依頼番号:007111

        End Select


    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理呼び出し(印刷処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMH010F)

        Logger.StartLog(Me.GetType.Name, "Print")
        '印刷処理
        Me.ActionControl(LMH010C.EventShubetsu.PRINT, frm)
        Logger.EndLog(Me.GetType.Name, "Print")

    End Sub

    '2015.09.03 tsunehira add
    ''' <summary>
    ''' 変更ボタン押下時処理呼び出し(変更処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnChg_Click(ByRef frm As LMH010F)

        Logger.StartLog(Me.GetType.Name, "Change")
        '変更処理
        Me.ActionControl(LMH010C.EventShubetsu.BULK_CUST_CHANGE, frm)
        Logger.EndLog(Me.GetType.Name, "Change")

    End Sub


    '2012.03.13 大阪対応START
    ''' <summary>
    ''' 出力ボタン(印刷・CSV作成)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnOutput_Click(ByVal frm As LMH010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "outputprint")

        Call Me.ActionControl(LMH010C.EventShubetsu.OUTPUTPRINT, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "outputprint")

    End Sub

    ''' <summary>
    ''' 出力(印刷・CSV作成)コンボ選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbOutput_SelectedValueChanged(ByVal frm As LMH010F)

        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetOutputControl(frm, sysdate(0))

    End Sub
    '2012.03.13 大阪対応START

    '要望番号1061 2012.05.15 追加START
    ''' <summary>
    ''' 出力(出力区分)コンボ選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbOutputKb_SelectedValueChanged(ByVal frm As LMH010F)

        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetOutputkbControl(frm, sysdate(0))

    End Sub
    '要望番号1061 2012.05.15 追加END

    ''' <summary>
    ''' 進捗区分排他制御
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkObj"></param>
    ''' <remarks></remarks>
    Friend Sub StatusControlHaita(ByVal frm As LMH010F, ByVal chkObj As Object)

        Dim chkitem As CheckBox = DirectCast(chkObj, CheckBox)

        If chkitem.Checked = False Then
            Exit Sub
        End If

        For Each item As Control In frm.grpSTATUS.Controls
            If item.GetType().Equals(GetType(LMCheckBox)) Then
                If item.Name = chkitem.Name Then

                Else
                    DirectCast(item, CheckBox).Checked = False
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' 進捗区分制御
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkObj"></param>
    ''' <remarks></remarks>
    Friend Sub StatusControl(ByVal frm As LMH010F, ByVal chkObj As Object)

        Dim chkitem As CheckBox = DirectCast(chkObj, CheckBox)

        If chkitem.Checked = False Then
            Exit Sub
        End If

        frm.chkstaRedData.Checked = False
        frm.chkStaTorikesi.Checked = False
        'frm.chkStaAll.Checked = False

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class