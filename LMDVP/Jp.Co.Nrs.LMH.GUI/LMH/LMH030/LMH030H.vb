' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH030H : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility
Imports System.Text
Imports System.IO
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Linq
Imports System.Collections.Generic

''' <summary>
''' LMH030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "EDI荷主INDEX"

    Public Enum EdiCustIndex As Integer
        '2019/07/18 依頼番号:006754 add
        AgcW00440 = 140                     '(大阪)ＡＧＣ若狭化学
        '2019.09.17 要望番号:006984 add
        CJC00787 = 141                      '(千葉)コーヴァンス・ジャパン株式会社
        TrmSmpl00409_01 = 158               '(土気)テルモ サンプル
    End Enum

#End Region

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH030G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconH As LMHControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' 印刷種類格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintSybetu As String

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMH030DS

    '''' <summary>
    '''' 印刷種類（Enum)格納フィールド
    '''' </summary>
    '''' <remarks></remarks>
    'Private _PrintSybetuEnum As LMC030H.PrintShubetsu

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' ファイルパス・EDI荷主INDEX格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _RtnStr As ArrayList

    '要望番号1991 2013.04.02 追加START
    ''' <summary>
    ''' 初期表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean
    '要望番号1991 2013.04.02 追加END

    ''2013.09.26 追加START
    ' ''' <summary>
    ' ''' 日立物流SAP切替用
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _hitachiSapFlg As Boolean = False
    ''2013.09.26 追加END


#If True Then ' BP運送会社自動設定対応 20161115 added by inoue

    ''' <summary>
    ''' EDI荷主INDEX
    ''' </summary>
    ''' <remarks></remarks>
    Private _EdiCustIndex As String = String.Empty

    ''' <summary>
    ''' BPカストロール判定
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsCustBp As Boolean
        Get
            Return (LMH030C.EDI_CUST_INDEX.BP.Equals(_EdiCustIndex))
        End Get
    End Property

    ''' <summary>
    ''' 運送会社テーブル(BP用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _BpUnsocoCdTable As LMH030DS.LMH030OUT_UNSO_BY_WGT_AND_DESTDataTable = Nothing


    ''' <summary>
    ''' チャーター管理テーブル(BP用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _BpCharterTable As LMH030DS.LMH030OUT_CHARTER_MANAGEMENTDataTable = Nothing

    ''' <summary>
    ''' BLF名
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property BLF_NAME As String
        Get
            Return String.Concat(GetPGID, "BLF")
        End Get
    End Property
#End If


#End Region 'Field

#Region "内部クラス"
    ''' <summary>
    ''' 期間クラス
    ''' </summary>
    ''' <remarks></remarks>
    Class DateRange

        ''' <summary>
        ''' 期間開始日
        ''' </summary>
        ''' <remarks></remarks>
        Public FromDate As Date = Nothing

        ''' <summary>
        ''' 期間終了日
        ''' </summary>
        ''' <remarks></remarks>
        Public ToDate As Date = Nothing

    End Class
#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '要望番号1991 2013.04.02 追加START
        Me._ShokiFlg = True
        '要望番号1991 2013.04.02 追加END

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMH030F = New LMH030F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Validateクラスの設定
        Me._V = New LMH030V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMH030G(Me, frm)

        Me._LMHconG = New LMHControlG(frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMH030C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetControl(MyBase.GetPGID(), frm, sysdate(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue(frm)

        '2013.10.08 追加START DIC SAP対応
        Call Me._G.SetHitachiControl(MyBase.GetPGID())
        '2013.10.08 追加END DIC SAP対応

        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()


        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'Validate共通クラスの設定
        Me._LMHconV = New LMHControlV(Me, DirectCast(frm, Form), Me._LMHconG)

        'Hnadler共通クラスの設定
        Me._LMHconH = New LMHControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMHconG = New LMHControlG(DirectCast(frm, Form))

        '要望番号1991 2013.04.02 追加START
        Me._ShokiFlg = False
        '要望番号1991 2013.04.02 追加END

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
        Me.SetCmbNohinPrt(frm)
#End If

    End Sub

#End Region '初期処理

#Region "外部Method"
    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMH030C.EventShubetsu, ByVal frm As LMH030F)

        '要望番号1991 2013.04.02 追加START
        If Me._ShokiFlg = True Then
            '初期表示時、画面初期化処理で、未印刷コンボが変更されたと判断され、
            '未印刷コンボ変更時の処理が行われてしまうため、フラグにて判定
            Exit Sub
        End If
        '要望番号1991 2013.04.02 追加END

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

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

            '2012.03.25 大阪対応START
            '*****出荷登録*****
            '*****運送登録*****
            Case LMH030C.EventShubetsu.SAVEOUTKA _
               , LMH030C.EventShubetsu.SAVEUNSO


                '項目チェック
                'If Me._V.IsSaveoutkaSingleCheck() = False Then
                If Me._V.IsSaveoutkaSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

#If True Then ' BP運送会社自動設定対応 20161115 added by inoue
                _EdiCustIndex = Me.GetEdiCustIndexFromRows(frm, LMH030C.M_EDI_CUST_INOUT_KB_OUTKA)
                Dim isPreProSuccess As Boolean = True
                If (Me.IsCustBp) Then
                    isPreProSuccess = Me.OutkaSavePreProcessingBp(frm)
                End If
#End If

                '関連チェック
                errHashTable = Me._V.IsSaveoutkaKanrenCheck(eventShubetsu, errDs)

                '★★★
                'If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                '    Call Me.ExcelErrorSet(errDs)
                'End If

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

#If True Then ' BP運送会社自動設定対応 20161115 added by inoue
                If (Me.IsCustBp AndAlso _
                    isPreProSuccess = False) Then

                    Me.ShowMessage(frm, "S001", New String() {"運送会社の取得"})

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If
#End If

#If True Then ' 大阪　大日本住友対応　月末営業日日数設定時チェック　2017/05/19
                If Me.ChkDataOutkaGetumatuShori(frm, eventShubetsu, errHashTable) = False Then

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If
#End If

                '要望番号:1321 terakawa 2012.08.03 Start
                '春日部営業所の場合、システムエラー時は画面ロックを解除する(春日部営業所:55)
                Dim nrsBrCd As String = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet _
                                                                 .Cells(1, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo)).ToString().Trim()
                Dim custCdL As String = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet _
                                                                 .Cells(1, LMH030G.sprEdiListDef.CUST_CD_L.ColNo)).ToString().Trim()
                'If nrsBrCd.Equals("55") And eventShubetsu = LMH030C.EventShubetsu.SAVEUNSO Then
                If (nrsBrCd.Equals("30")) And (custCdL.Equals("30001") Or custCdL.Equals("30002") Or custCdL.Equals("30010")) And (eventShubetsu = LMH030C.EventShubetsu.SAVEUNSO) Then
                    Try
                        '★★★
                        ''出荷登録処理
                        Call Me.OutkaSaveShori(frm, eventShubetsu, errHashTable, errDs)
                    Catch
                        '運送登録に失敗した場合、画面ロックを解除して終了
                        Me.ShowMessage(frm, "S001", New String() {"運送登録"})
                        Call Me._LMHconH.EndAction(frm)
                        Exit Sub
                    End Try
                Else
                    Try
                        '★★★
                        ''出荷登録処理
                        Call Me.OutkaSaveShori(frm, eventShubetsu, errHashTable, errDs)
                    Catch
                        '運送登録に失敗した場合、画面ロックを解除して終了
                        Me.ShowMessage(frm, "S001", New String() {"出荷登録"})
                        Call Me._LMHconH.EndAction(frm)
                        Exit Sub
                    End Try
                End If
                '要望番号:1321 terakawa 2012.08.03 End

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '2012.03.25 大阪対応END
                '*****実績作成*****
            Case LMH030C.EventShubetsu.CREATEJISSEKI

                '項目チェック
                If Me._V.IsCreatejissekiSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '要望番号:1092 terakawa 2012.06.28 Start
                'システム日付を取得
                Dim sysDate As String()
                sysDate = MyBase.GetSystemDateTime()

                '関連チェック
                errHashTable = Me._V.IsCreatejissekiKanrenCheck(eventShubetsu, errDs, sysDate(0))
                '要望番号:1092 terakawa 2012.06.28 End

                'If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                '    Call Me.ExcelErrorSet(errDs)
                'End If

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.InsertCreatejisseki(frm, eventShubetsu, errHashTable, errDs)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '*****紐付け*****
            Case LMH030C.EventShubetsu.HIMODUKE

                '項目チェック
                If Me._V.IsHimodukeSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.IsHimodukeKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                '2013.10.08 追加START DIC SAP 対応
                Dim pgid As String = String.Empty
                pgid = Me.changePgid(MyBase.GetPGID)

                prmDs = Me.SetDataSetLMH070InData(frm, Nothing, pgid)
                'prmDs = Me.SetDataSetLMH070InData(frm, Nothing, MyBase.GetPGID())
                '2013.10.08 追加END DIC SAP 対応
                prm.ParamDataSet = prmDs

                'モーダレスなので画面ロック必要なし
                Call Me._LMHconH.EndAction(frm) '終了処理

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH070", prm)

                'メッセージの表示
                Me.ShowMessage(frm, "G007")

                '*****EDI取消処理******
            Case LMH030C.EventShubetsu.EDITORIKESI

                '項目チェック
                If Me._V.IsEditorikesiSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.IsEditorikesiKanrenCheck(eventShubetsu, errDs)

                'If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                '    Call Me.ExcelErrorSet(errDs)
                'End If

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.EdiTorikesi(frm, eventShubetsu, errHashTable, errDs)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '取込対応 20120305 Start
                '    '*****取込処理******(2次対応)
            Case LMH030C.EventShubetsu.TORIKOMI

                '単項目チェック
                If Me._V.IsTorikomiSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                'キャッシュから情報取得
                Dim rtDs As DataSet = New LMH030DS()
                rtDs = Me.SetCachedSemiEDI(frm)

                'キャッシュから情報を取得できなかった場合処理終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '続行確認
                Dim rtn As MsgBoxResult
                If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("SEMI_EDI_FLAG").ToString = "1" Then
                    rtn = Me.ShowMessage(frm, "C001", New String() {"取込処理"})
                Else
                    rtn = Me.ShowMessage(frm, "C001", New String() {"緊急時EDI手動受信"})
                End If

                If rtn = MsgBoxResult.Ok Then
                ElseIf rtn = MsgBoxResult.Cancel Then
                    Call MyBase.ShowMessage(frm, "G007")
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '2014/03/20 黎 セミEDI標準化対応 --ST--
                'チェックのスルー判定
                If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("FILE_CHICE_KBN").ToString().Equals("01") = False Then

                    '関連チェック
                    If Me._V.IsTorikomiKanrenCheck(rtDs) = False Then
                        Call Me._LMHconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                End If
                '2014/03/20 黎 セミEDI標準化対応 --ED--

                '2014/03/20 黎 セミEDI標準化対応 --ST--
                Select Case rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("FILE_CHICE_KBN").ToString
                    Case "01"
                        '取込処理
                        Call Me.TorikomiStanderdEdition(frm, eventShubetsu, errDs, rtDs)
                    Case Else
                        '取込処理
                        Call Me.Torikomi(frm, eventShubetsu, errDs, rtDs)
                End Select
                '2014/03/20 黎 セミEDI標準化対応 --ED--

                '取込対応 20120305 End

                '*****実績取消*****
            Case LMH030C.EventShubetsu.TORIKESIJISSEKI

                '項目チェック
                If Me._V.IsJissekiSakuseiSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.IsJissekiSakuseiKanrenCheck(eventShubetsu, errDs)

                'If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                '    Call Me.ExcelErrorSet(errDs)
                'End If

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                ''実績取消処理
                Call Me.JissekiTorikesi(frm, eventShubetsu, errHashTable, errDs)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '*****検索処理******
            Case LMH030C.EventShubetsu.KENSAKU

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If


                '検索処理を行う
                Call Me.SelectData(frm, "NEW")

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                'フォーカスの設定
                Call Me._G.SetFoucus()

            Case LMH030C.EventShubetsu.MASTER, LMH030C.EventShubetsu.ENTER


                '******************「マスタ参照」******************'

                '入力処理
                If Me._V.IsRefMstInputCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '現在フォーカスのあるコントロール名の取得
                'Dim objNm As String = frm.FocusedControlName()
                Dim objNm As String = frm.ActiveControl.Name()

                Select Case objNm

                    Case frm.txtCustCD_L.Name, frm.txtCustCD_M.Name
                        '荷主コード(大),荷主コード(中)
                        frm.lblCustNM_L.TextValue = String.Empty
                        frm.lblCustNM_M.TextValue = String.Empty
                        Call Me.ShowPopup(frm, objNm, prm, eventShubetsu)
                        '初期メッセージ設定
                        MyBase.ShowMessage(frm, "G007")

                    Case frm.txtTodokesakiCd.Name
                        '届先コード
                        frm.lblTodokesakiNM.TextValue = String.Empty
                        Call Me.ShowPopup(frm, objNm, prm, eventShubetsu)
                        '初期メッセージ設定
                        MyBase.ShowMessage(frm, "G007")

                    Case frm.txtEditMain.Name, frm.txtEditSub.Name
                        '運送会社コード, 運送会社支店コード
                        frm.lblEditNm.TextValue = String.Empty
                        Call Me.ShowPopup(frm, objNm, prm, eventShubetsu)
                        '初期メッセージ設定
                        MyBase.ShowMessage(frm, "G007")

                    Case frm.txtEditDestCD.Name     'ADD 2018/02/22
                        '届先コード
                        frm.lblEditNm.TextValue = String.Empty
                        Call Me.ShowPopup(frm, objNm, prm, eventShubetsu)
                        '初期メッセージ設定
                        MyBase.ShowMessage(frm, "G007")

                    Case Else
                        'ポップ対象外のテキストの場合
                        MyBase.ShowMessage(frm, "G005")

                End Select

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue                
                Me.SetCmbNohinPrt(frm)
#End If
                '*****初期荷主変更*****
            Case LMH030C.EventShubetsu.DEF_CUST

                'inputDataSet作成
                prmDs = Me.SetDataSetLMZ010InData(frm)
                prm.ParamDataSet = prmDs

                '初回荷主変更Popup呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ010", prm)

                '戻り処理
                If prm.ReturnFlg = True Then
                    With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD_L.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.lblCustNM_L.TextValue = .Item("CUST_NM_L").ToString    '荷主名（大）
                        frm.txtCustCD_M.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                        frm.lblCustNM_M.TextValue = .Item("CUST_NM_M").ToString    '荷主名（中）
                    End With

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
                    Me.SetCmbNohinPrt(frm)
#End If
                End If

                'メッセージの表示
                Call MyBase.ShowMessage(frm, "G007")

                '*****一括変更処理******
            Case LMH030C.EventShubetsu.IKKATUHENKO

                Dim rtn As String()
                rtn = MyBase.GetSystemDateTime()

                '項目チェック
                If Me._V.IsIkkatuhenkoSingleCheck(rtn(0)) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.IsIkkatuhenkoKanrenCheck(eventShubetsu, errDs)

                'If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                '    Call Me.ExcelErrorSet(errDs)
                'End If

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then

                    If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.IkkatsuHenko(frm, errHashTable, errDs)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '*****CSV作成・印刷処理******
                '▼▼▼要望番号:467
            Case LMH030C.EventShubetsu.OUTPUTPRINT

                '2012.03.03 大阪対応
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
                '▲▲▲要望番号:467

                '*****印刷処理(画面検索条件での印刷処理)******(2次対応)
            Case LMH030C.EventShubetsu.SELPRINT

                Dim printShubetsu As Integer = 0
                If String.IsNullOrEmpty(frm.cmbPrint.SelectedValue.ToString()) = False Then
                    printShubetsu = Convert.ToInt32(frm.cmbPrint.SelectedValue)
                End If

                '必須チェック
                If Me._V.IsSelPrintCheck(eventShubetsu, printShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '印刷ボタン押下処理を行う
                Call Me.SelPrint(frm, printShubetsu, eventShubetsu)

                '*****実行処理(EDI取消⇒未登録)******
            Case LMH030C.EventShubetsu.TORIKESI_MITOUROKU

                Dim JikkouShubetsu As Integer = Convert.ToInt32(frm.cmbExe.SelectedValue)

                '項目チェック
                If Me._V.JikkouSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.TorikesiMitouroku(frm, eventShubetsu)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '*****実行処理(実績作成済⇒実績未)******
            Case LMH030C.EventShubetsu.SAKUSEIZUMI_JISSEKIMI

                '項目チェック
                If Me._V.JikkouSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.JissekiSakuseiJissekimi(frm, eventShubetsu)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '*****実行処理(実績送信済⇒送信待)******
            Case LMH030C.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                '項目チェック
                If Me._V.JikkouSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.SousinSousinmi(frm, eventShubetsu)

                '*****実行処理(実績送信済⇒実績未)******
            Case LMH030C.EventShubetsu.SOUSINZUMI_JISSEKIMI

                '項目チェック
                If Me._V.JikkouSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.JissekiSousinJissekimi(frm, eventShubetsu)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '*****実行処理(追加実行処理)******
            Case LMH030C.EventShubetsu.SAKURA_TUIKAJIKKOU

                '要望番号1129 2012.06.11 修正START
                Me._RtnStr = Me.setCashetable(frm)

                '項目チェック
                Dim strRtn() As String
                strRtn = DirectCast(Me._RtnStr.ToArray(GetType(String)), String())

                If Me._V.JikkouSingleCheck(eventShubetsu, strRtn) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                Call Me.TuikaJikkou(frm, eventShubetsu, strRtn)
                '要望番号1129 2012.06.11 修正END

                '*****(実行)出荷取消⇒未登録******
            Case LMH030C.EventShubetsu.TOUROKUZUMI_MITOUROKU

                '項目チェック
                If Me._V.JikkouSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.Mitouroku(frm, eventShubetsu)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)

                '2012.04.04 大阪対応追加START
                '*****(実行)運送取消⇒未登録******
            Case LMH030C.EventShubetsu.UNSOTORIKESI_MITOUROKU

                '項目チェック
                If Me._V.JikkouSingleCheck(eventShubetsu) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                errHashTable = Me._V.JikkouKanrenCheck(eventShubetsu)

                '全行エラーの場合処理終了
                If chkList.Count = errHashTable.Count Then
                    Call Me._LMHconH.EndAction(frm)
                    Exit Sub
                End If

                Call Me.UnsoMitouroku(frm, eventShubetsu)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm)
                '2012.04.04 大阪対応追加END

                '2012.06.20 追加START
                '*****実行処理(EDI荷主コード設定処理)******
            Case LMH030C.EventShubetsu.CUST_CD_SETUP

                '2013.10.08 追加START DIC SAP 対応
                Dim pgid As String = String.Empty
                pgid = Me.changePgid(MyBase.GetPGID)

                prmDs = Me.SetDataSetLMH060InData(frm, Nothing, pgid)
                'prmDs = Me.SetDataSetLMH060InData(frm, Nothing, MyBase.GetPGID())
                '2013.10.08 追加END DIC SAP 対応
                prm.ParamDataSet = prmDs

                'モーダレスなので画面ロック必要なし
                Call Me._LMHconH.EndAction(frm) '終了処理

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH060", prm)

                'メッセージの表示
                Me.ShowMessage(frm, "G007")

                '2012.06.20 追加END

                '******************「ダブルクリック」******************'
            Case LMH030C.EventShubetsu.DOUBLE_CLICK

                '検索行をクリックしたのがどうかをチェックする
                If eventShubetsu = LMH030C.EventShubetsu.DOUBLE_CLICK Then
                    If Me.DoubleClickChk(frm) = False Then
                        Call Me._LMHconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If
                End If

                '=== DOTO : inputDataSet作成 ===='
                prmDs = Me.SetDataSetLMH040InData(frm, Nothing, LMH030C.LMH040_STA_COPY)
                prm.ParamDataSet = prmDs
                prm.RecStatus = RecordStatus.NOMAL_REC

                'モーダレスなので画面ロック必要なし
                Call Me._LMHconH.EndAction(frm) '終了処理

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMH040", prm)

                'メッセージの表示
                Call MyBase.ShowMessage(frm, "G007")

        End Select

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        ''2013.09.26 追加START
        ''日立物流SAP切替用()
        'If _hitachiSapFlg = True AndAlso frm.chkHitachiSap.Checked = True Then
        '    frm.chkHitachiSap.EnableStatus = False
        'End If
        ''2013.09.26 追加END


    End Sub

#End Region '外部メソッド

#Region "内部メソッド"

#Region "EXCEL出力処理"

    Private Sub OutputExcel(ByVal frm As LMH030F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

    '(2013.02.06)要望番号1822 BP対応 -- START --
    Private Sub OutputExcel_BP(ByVal frm As LMH030F, ByVal ds As DataSet)

        Dim cntDt As DataTable = ds.Tables("LMH030_PRT_CNT")
        Dim PRT_CNT As String = cntDt.Rows(0).Item("PRT_CNT").ToString.Trim

        'メッセージ出力
        If PRT_CNT.Equals("0") Or PRT_CNT.Equals("") Then
            '0件だったらエラーメッセージのに出力
            MyBase.ShowMessage(frm, "E235")

        Else
            'N件ならば、印刷件数 + エラーメッセージを出力
            MyBase.ShowMessage(frm, "E534", New String() {PRT_CNT, "印刷処理"})

        End If

        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub
    '(2013.02.06)要望番号1822 BP対応 --  END  --

#End Region

    '要望番号1129 2012.06.11 修正START
#Region "キャッシュ取得処理(追加処理時のパス)"

    Private Function setCashetable(ByVal frm As LMH030F) As ArrayList

        ''キャッシュの値(区分マスタ)
        'Dim mKbnDrs As DataRow() = Nothing
        'Dim selectCmbValue As String = frm.cmbExe.SelectedValue.ToString
        Dim dirNm As String = String.Empty
        Dim fileNm As String = String.Empty
        Dim extension As String = String.Empty
        'Dim fullPathNm As String = String.Empty
        Dim rtnStr(1) As String

        Me._RtnStr = New ArrayList

        'mKbnDrs = Me._LMHconV.SelectKBNListDataRow(selectCmbValue, "E006")
        'If 0 < mKbnDrs.Length Then
        '    dirNm = mKbnDrs(0).Item("KBN_NM2").ToString()
        '    fileNm = mKbnDrs(0).Item("KBN_NM3").ToString()
        '    extension = mKbnDrs(0).Item("KBN_NM4").ToString()
        'End If

        'If String.IsNullOrEmpty(dirNm) = True OrElse String.IsNullOrEmpty(fileNm) = True _
        '   OrElse String.IsNullOrEmpty(extension) = True Then

        'Else
        '    fullPathNm = String.Concat(dirNm, fileNm, extension)

        'End If

        Dim ediCustDrs As DataRow() = Nothing
        Dim inOutKb As String = "0"                                        '入出荷区分("0"(出荷))
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()         '営業所コード
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()          '倉庫コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString()   '荷主コード(大)
        Dim custCdM As String = frm.txtCustCD_M.TextValue.ToString()   '荷主コード(中)
        Dim ediCustIdx As String = String.Empty                            'EDI荷主番号
        'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
        ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
        If 0 < ediCustDrs.Length Then
            'EDI荷主情報キャッシュが修正後に切替を行う
            dirNm = ediCustDrs(0).Item("ADDEXE_INPUT_DIR").ToString()
            fileNm = ediCustDrs(0).Item("ADDEXE_FILE_NM").ToString()
            extension = ediCustDrs(0).Item("ADDEXE_FILE_EXTENTION").ToString()
            ediCustIdx = ediCustDrs(0)("EDI_CUST_INDEX").ToString()

            If String.IsNullOrEmpty(dirNm) = True OrElse String.IsNullOrEmpty(fileNm) = True _
                OrElse String.IsNullOrEmpty(extension) = True Then

                _RtnStr.Add(String.Empty)
                '_RtnStr.Add(String.Empty)
                _RtnStr.Add(ediCustIdx)

            Else

                _RtnStr.Add(String.Concat(dirNm, fileNm, extension))
                _RtnStr.Add(ediCustIdx)

            End If

        Else
            _RtnStr.Add(String.Empty)
            _RtnStr.Add(String.Empty)

        End If

        Return _RtnStr

    End Function

#End Region
    '要望番号1129 2012.06.11 修正END

#Region "キャッシュ取得処理(各イベント処理時)"

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMH030F)

        With frm

            Dim custCdL As String = frm.txtCustCD_L.TextValue
            Dim custCdM As String = frm.txtCustCD_M.TextValue

            '荷主名称
            .lblCustNM_L.TextValue = String.Empty
            .lblCustNM_M.TextValue = String.Empty
            If String.IsNullOrEmpty(custCdL) = False Then
                If String.IsNullOrEmpty(custCdM) = True Then
                    custCdM = "00"
                End If

                '2016.02.18 要望番号2491 修正START
                Dim custDr() As DataRow = Me._LMHconG.SelectCustListDataRow(Convert.ToString(.cmbEigyo.SelectedValue()), custCdL, custCdM, "00", "00")
                '2016.02.18 要望番号2491 修正END
                If 0 < custDr.Length Then
                    .lblCustNM_L.TextValue = custDr(0).Item("CUST_NM_L").ToString()
                    .lblCustNM_M.TextValue = custDr(0).Item("CUST_NM_M").ToString()
                End If

            End If

            '担当者名
            Dim usercd As String = frm.txtTantouCd.TextValue
            .lblTantouNM.TextValue = String.Empty
            If String.IsNullOrEmpty(usercd) = False Then

                Dim userDr() As DataRow = Me._LMHconG.SelectTantouListDataRow(usercd)
                If 0 < userDr.Length Then
                    .lblTantouNM.TextValue = userDr(0).Item("USER_NM").ToString()
                End If

            End If

            '届先名
            Dim destCd As String = frm.txtTodokesakiCd.TextValue
            .lblTodokesakiNM.TextValue = String.Empty
            If String.IsNullOrEmpty(destCd) = False Then

                Dim destDr() As DataRow = Me._LMHconG.SelectDestListDataRow(custCdL, destCd)
                If 0 < destDr.Length Then
                    .lblTodokesakiNM.TextValue = destDr(0).Item("DEST_NM").ToString()
                End If

            End If

#If True Then ' 日本合成化学対応(2646) 20170308 added inoue
            ' EDI納品書印刷抽出コンボ内データ
            Me.SetCmbNohinPrt(frm)
#End If

        End With

    End Sub

#End Region

    '取込対応 20120305 Start
#Region "キャッシュ取得処理(取込処理時)"

    '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start

    ''' <summary>
    ''' キャッシュから情報取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>データセット</returns>
    ''' <remarks>外部から入出力区分を設定できるようにするため、
    ''' 　　　　 既存関数に引数を追加する。現在既存関数を読んでいるところで不整合を起こさせないため
    ''' 　　　　 引数に入出力が設定されていない場合、以前と同様の"20を設定する入口を用意する"</remarks>
    Private Overloads Function SetCachedSemiEDI(ByVal frm As LMH030F) As DataSet
        Return SetCachedSemiEDI(frm, LMH030C.IN_OUT_KBN_BY_FILE.IN_OUT_CSV)
    End Function
    '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

    ''' <summary>
    ''' キャッシュから情報取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Overloads Function SetCachedSemiEDI(ByVal frm As LMH030F, ByVal inoutKb As String) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Dim dr As DataRow

        With frm

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim whCd As String = frm.cmbWare.SelectedValue.ToString()
            Dim custCdL As String = frm.txtCustCD_L.TextValue
            Dim custCdM As String = frm.txtCustCD_M.TextValue
            '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 del start
            'Dim inoutKb As String = "20"
            '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 del end
            Dim mNinushiInoutKb As String = "0"

            'セミEDI設定情報取得
            Dim semiEdiDr() As DataRow = Me._LMHconG.SelectSemiEDIListDatalow(brCd, whCd, custCdL, custCdM, inoutKb)
            If 0 < semiEdiDr.Length Then

                dr = rtDs.Tables(LMH030C.SEMIEDI_INFO).NewRow()

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

                '2014/03/19 黎 セミEDI標準化対応 --ST--
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
                '2014.12.24 追加START
                dr("L_BUYER_ORD_NO") = semiEdiDr(0).Item("L_BUYER_ORD_NO").ToString()
                dr("L_SHIP_CD_NO") = semiEdiDr(0).Item("L_SHIP_CD_NO").ToString()
                dr("M_IRIME_UT_NO") = semiEdiDr(0).Item("M_IRIME_UT_NO").ToString()
                dr("DTL_DATACHECK_FLG") = semiEdiDr(0).Item("DTL_DATACHECK_FLG").ToString()
                '2014.12.24 追加END

                '2014/03/19 黎 セミEDI標準化対応 --ED--
                '追加開始 --- 2015.04.08 キャッシュ追加
                dr("DTL_OUTKAPKGNB_CALC_FLG") = semiEdiDr(0).Item("DTL_OUTKAPKGNB_CALC_FLG").ToString()
                '追加開始 --- 2015.04.08

                Dim ninushi() As DataRow = Me._LMHconV.SelectEdiCustListDataRow(mNinushiInoutKb, brCd, whCd, custCdL, custCdM)
                If 0 < ninushi.Length Then
                    dr("RCV_NM_HED") = ninushi(0).Item("RCV_NM_HED").ToString()
                    '要望番号1277:(テーブル名が固定で記載されている) 2012/07/12 本明 Start(コメントを外した)
                    dr("RCV_NM_DTL") = ninushi(0).Item("RCV_NM_DTL").ToString()
                    dr("RCV_NM_EXT") = ninushi(0).Item("RCV_NM_EXT").ToString()
                    '要望番号1277:(テーブル名が固定で記載されている) 2012/07/12 本明 End

                    '2014/03/25 黎 セミ標準対応 フラグ17追加 --ST--
                    dr("FLAG_17") = ninushi(0).Item("FLAG_17").ToString() '<< コメント取敢えず
                    '2014/03/25 黎 セミ標準対応 フラグ17追加 --ED--

                End If
                rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows.Add(dr)
            Else
                '存在チェック(キャッシュで取得した値が0件の場合はエラー)
                MyBase.SetMessage("E459")   'エラーメッセージ
            End If

        End With

        Return rtDs

    End Function

#End Region
    '取込対応 20120305 End

#Region "検索処理"
    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>

    Private Sub SelectData(ByVal frm As LMH030F, ByVal reFlg As String)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        ''閾値の設定
        'Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0)
        'MyBase.SetLimitCount(Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))))

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()

        If reFlg.Equals("NEW") Then
            '新規検索の場合
            Call Me.SetDataSetInData(frm, rtDs)

        ElseIf reFlg.Equals("RE") Then
            '再検索の場合
            rtDs = Me._FindDs
        End If

        'SPREAD(表示行)初期化
        frm.sprEdiList.CrearSpread()

        If Me.SetDataSetInData(frm, rtDs) = False Then
            MyBase.ShowMessage(frm, "E361")
            Me._LMHconV.SetErrorControl(frm.txtCustCD_L)
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        'Dim rtnDs As DataSet = Me._LMHconH.CallWSAAction(DirectCast(frm, Form) _
        '                                                 , "LMH030BLF", "SelectListData", rtDs _
        '                                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))))

        '2013.10.08 追加START DIC SAP 対応
        Dim pgid As String = String.Empty
        pgid = Me.changePgid(MyBase.GetPGID)
        '2013.10.08 追加END DIC SAP 対応

        '2013.10.08 修正START DIC SAP 対応
        'Dim rtnDs As DataSet = Me._LMHconH.CallWSAAction(DirectCast(frm, Form), _
        '                                "LMH030BLF", "SelectListData", rtDs _
        '                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))) _
        '                                 , Convert.ToInt32(Convert.ToDouble( _
        '                                 MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        Dim rtnDs As DataSet = Me._LMHconH.CallWSAAction(DirectCast(frm, Form), _
                                "LMH030BLF", "SelectListData", rtDs _
                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))) _
                                 , Convert.ToInt32(Convert.ToDouble( _
                                 MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & pgid & "'")(0).Item("VALUE1"))))
        '2013.10.08 修正END DIC SAP 対応

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs, reFlg)

            ''2013.09.26 追加START
            ''日立物流SAP切替用()
            'If frm.chkHitachiSap.Checked = True Then
            '    _hitachiSapFlg = True
            'End If
            ''2013.09.26 追加END


        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutkaSaveShori(ByVal frm As LMH030F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()

        '2011.12.08 要望番号608 修正 START
        'rtDs = Me.SetDataOutkaSaveShori(frm, rtDs, eventshubetsu, errHashtable)
        rtDs = Me.SetDataOutkaSaveShori(frm, rtDs, eventshubetsu, errHashtable, 0)
        '2011.12.08 要望番号608 修正 END


        Dim autoMatomeF As String = String.Empty

        autoMatomeF = rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(0)("AUTO_MATOME_FLG").ToString()

        If autoMatomeF.Equals("1") = True Then

            rtn = Me.ShowMessage(frm, "W160", New String() {"自動まとめ処理"})

            '2011.09.30 修正 OK⇒Yesに変更
            If rtn = MsgBoxResult.Yes Then
                '2011.09.30 追加 START GUI側エラーが出力されない対応
                'エラーをExcelに出力
                If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If
                '2011.09.30 追加 END 
            ElseIf rtn = MsgBoxResult.No Then

                '2011.12.08 要望番号608 修正 START
                'まとめ処理対象外とする
                'rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(0)("AUTO_MATOME_FLG") = "9"
                rtDs = Me.SetDataOutkaSaveShori(frm, rtDs, eventshubetsu, errHashtable, 1)
                '2011.12.08 要望番号608 修正 END
                'エラーをExcelに出力
                If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If

            ElseIf rtn = MsgBoxResult.Cancel Then
                Call MyBase.ShowMessage(frm, "G007")
                Exit Sub

            End If

        Else
            '2012.03.25 大阪対応START
            Select Case eventshubetsu
                Case LMH030C.EventShubetsu.SAVEOUTKA
                    rtn = Me.ShowMessage(frm, "C001", New String() {"出荷登録"})
                Case LMH030C.EventShubetsu.SAVEUNSO
                    rtn = Me.ShowMessage(frm, "C001", New String() {"運送登録"})

            End Select

            '2012.03.25 大阪対応END

            If rtn = MsgBoxResult.Ok Then
                '2011.09.30 追加 START GUI側エラーが出力されない対応
                'エラーをExcelに出力
                If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If
                '2011.09.30 追加 END 
            ElseIf rtn = MsgBoxResult.Cancel Then
                Me.ShowMessage(frm, "G007")
                Exit Sub
            End If

        End If

        If rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(0).Item("EDI_CUST_INDEX").ToString() = "109" OrElse
            rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(0).Item("EDI_CUST_INDEX").ToString() = "159" Then
            ' 対象 EDI_CUST_INDEX が、丸和(横浜) または 丸和(横浜)(CSV) の場合

            ' キャッシュからの M_SEMIEDI_INFO_STATE 情報取得
            Dim dsSemiediInfoState As DataSet = New LMH030DS()
            dsSemiediInfoState = Me.SetCachedSemiEDI(frm)
            Dim drSemiediInfoState As DataRow
            If MyBase.IsMessageExist() = True Then
                ' キャッシュから情報を取得できなかった場合
                Call Me.ClearMessageData()
                drSemiediInfoState = dsSemiediInfoState.Tables(LMH030C.SEMIEDI_INFO).NewRow()
            Else
                drSemiediInfoState = dsSemiediInfoState.Tables(LMH030C.SEMIEDI_INFO).Rows(0)
            End If
            rtDs.Tables(LMH030C.SEMIEDI_INFO).ImportRow(drSemiediInfoState)
        End If

        '2012.03.25 大阪対応START
        Select Case eventshubetsu

            Case LMH030C.EventShubetsu.SAVEOUTKA
                'ログ出力
                MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaToroku")
            Case LMH030C.EventShubetsu.SAVEUNSO
                'ログ出力
                MyBase.Logger.StartLog(MyBase.GetType.Name, "UnsoToroku")

        End Select
        '2012.03.25 大阪対応END

        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
        Dim iCommitCnt As Integer = 0   '更新件数
        '便宜上LMH030_EDI_TORIKOMI_RETを使用する
        Dim dtCntRet As DataTable = rtDs.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数

        '処理件数クリア
        dtCntRet.Clear()
        dtCntRet.Rows.Add(0)

        dtCntRet.Rows(0).Item("ALL_CNT") = "0"
        dtCntRet.Rows(0).Item("RCV_HED_INS_CNT") = "0"
        dtCntRet.Rows(0).Item("RCV_DTL_INS_CNT") = "0"
        dtCntRet.Rows(0).Item("OUT_HED_INS_CNT") = "0"
        dtCntRet.Rows(0).Item("OUT_DTL_INS_CNT") = "0"
        dtCntRet.Rows(0).Item("RCV_HED_CAN_CNT") = "0"
        dtCntRet.Rows(0).Item("RCV_DTL_CAN_CNT") = "0"
        dtCntRet.Rows(0).Item("OUT_HED_CAN_CNT") = "0"
        dtCntRet.Rows(0).Item("OUT_DTL_CAN_CNT") = "0"
        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End

#If True Then ' BP運送会社自動設定対応 20161115 added by inoue
        If (Me.IsCustBp) Then
            rtDs.Tables(LMH030C.TABLE_NM_UNSO_BY_WGT_AND_DEST).Merge(_BpUnsocoCdTable)


            rtDs.Tables(LMH030C.TABLE_NM_CHARTER_MANAGEMENT).Merge(_BpCharterTable)
        End If
#End If

        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH030BLF", "OutkaToroku", rtDs)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            If rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

                '2011.09.20 修正START
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtnDs, frm, eventshubetsu)
                '2011.09.20 修正END
            End If

        ElseIf rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

            '2011.09.20 修正START
            'ワーニングが設定されている場合
            Call Me.CallWarning(rtnDs, frm, eventshubetsu)
            '2011.09.20 修正END

        Else

            '2012.03.25 大阪対応START
            Select Case eventshubetsu

                Case LMH030C.EventShubetsu.SAVEOUTKA
                    '出荷登録処理成功時処理
                    Call Me.SuccessOutkaSave(frm, rtnDs)
                    MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaToroku")
                Case LMH030C.EventShubetsu.SAVEUNSO

                    '出荷登録処理成功時処理
                    Call Me.SuccessUnsoSave(frm, rtnDs)
                    MyBase.Logger.EndLog(MyBase.GetType.Name, "UnsoToroku")

            End Select
            '2012.03.25 大阪対応END

            ''出荷登録処理成功時処理
            'Call Me.SuccessOutkaSave(frm, rtnDs)

        End If

        Call Me._LMHconH.EndAction(frm)

    End Sub

#If True Then ' BP運送会社自動設定対応 20161117 added by inoue

#Region "BP用追加メソッド"

    ''' <summary>
    ''' 先頭行のデータを取得する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="colnumIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFirstRowCellValue(ByVal frm As LMH030F _
                                        , ByVal colnumIndex As Integer) As String
        With frm.sprEdiList.ActiveSheet

            Dim firstIndex As Integer = 1

            If (.RowCount > 1) Then
                Return Me._LMHconV.GetCellValue(.Cells(firstIndex, colnumIndex)).ToString()
            End If
        End With

        Return ""

    End Function

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue



    ''' <summary>
    ''' EDI荷主INDEXを取得する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ediCustInOutKb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCustIndexFromRows(ByVal frm As LMH030F _
                                           , ByVal ediCustInOutKb As String) As String

        Dim ediCustIndex As String = String.Empty
        If (frm.sprEdiList.ActiveSheet.RowCount > 1) Then

            Dim nrsBrCd As String = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo)
            Dim whCd As String = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo)
            Dim custCdL As String = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.CUST_CD_L.ColNo)
            Dim custCdM As String = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.CUST_CD_M.ColNo)

            ediCustIndex = Me.GetEdiCustIndex(nrsBrCd, whCd, custCdL, custCdM, ediCustInOutKb)
        End If

        Return ediCustIndex

    End Function

    ''' <summary>
    ''' EDI荷主INDEXを取得する
    ''' </summary>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <param name="ediCustInOutKb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCustIndex(ByVal nrsBrCd As String _
                                   , ByVal whCd As String _
                                   , ByVal custCdL As String _
                                   , ByVal custCdM As String _
                                   , ByVal ediCustInOutKb As String) As String

        Dim ediCust As DataRow() = Me._LMHconV.SelectEdiCustListDataRow(ediCustInOutKb _
                                                                      , nrsBrCd _
                                                                      , whCd _
                                                                      , custCdL _
                                                                      , custCdM)

        Dim ediCustIndex As String = String.Empty
        If (ediCust.Count > 0) Then
            ediCustIndex = ediCust(0)(LMH030C.COLUMN_NM_EDI_CUST_INDEX).ToString()
        End If

        Return ediCustIndex

    End Function


    ''' <summary>
    ''' EDI荷主INDEXを取得する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ediCustInOutKb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEdiCustIndexFromTextBox(ByVal frm As LMH030F _
                                              , ByVal ediCustInOutKb As String) As String

        Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()
        Dim custCdL As String = frm.txtCustCD_L.TextValue
        Dim custCdM As String = frm.txtCustCD_M.TextValue

        Dim ediCustIndex As String = Me.GetEdiCustIndex(nrsBrCd _
                                                      , whCd _
                                                      , custCdL _
                                                      , custCdM _
                                                      , ediCustInOutKb)
        Return ediCustIndex

    End Function

#End If

    ''' <summary>
    ''' 選択された出荷予定日の最大と最小値を取得する
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaPlanDateRage(ByVal frm As LMH030F) As DateRange

        Dim range As New DateRange()

        With frm.sprEdiList.ActiveSheet

            range.FromDate = Date.MaxValue
            range.ToDate = Date.MinValue

            Dim rowOutkaPlanDate As New Date

            For Each rowNo As String In Me._V.getCheckList()

                Dim outkaPlanDate As String = Me._LMHconV.GetCellValue(.Cells(Convert.ToInt32(rowNo), LMH030G.sprEdiListDef.OUTKA_PLAN_DATE.ColNo))
                If (Date.TryParse(outkaPlanDate, rowOutkaPlanDate)) Then

                    If (rowOutkaPlanDate > range.ToDate) Then
                        range.ToDate = rowOutkaPlanDate
                    End If

                    If (rowOutkaPlanDate < range.FromDate) Then
                        range.FromDate = rowOutkaPlanDate
                    End If
                End If
            Next
        End With

        Return range

    End Function

    ''' <summary>
    ''' BP用運送会社設定クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BpUnsoTableClear()
        If (_BpUnsocoCdTable IsNot Nothing) Then
            _BpUnsocoCdTable.Dispose()
            _BpUnsocoCdTable = Nothing
        End If

        If (_BpCharterTable IsNot Nothing) Then
            _BpCharterTable.Dispose()

            _BpCharterTable = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 出荷登録前処理(BP用)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaSavePreProcessingBp(ByVal frm As LMH030F) As Boolean

        Dim isSuccess As Boolean = False

        Using input As LMH030DS = New LMH030DS()

            Me.BpUnsoTableClear()

            ' 出荷予定日の期間を取得
            Dim range As DateRange = GetOutkaPlanDateRage(frm)


            Dim newRow As LMH030DS.LMH030IN_OUTKA_SAVE_BPRow = _
                input.LMH030IN_OUTKA_SAVE_BP.NewLMH030IN_OUTKA_SAVE_BPRow

            newRow.EDI_CUST_INDEX = _EdiCustIndex
            newRow.NRS_BR_CD = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo)
            newRow.CUST_CD_L = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.CUST_CD_L.ColNo)
            newRow.CUST_CD_M = Me.GetFirstRowCellValue(frm, LMH030G.sprEdiListDef.CUST_CD_M.ColNo)
            newRow.MIN_OUTKA_PLAN_DATE = range.FromDate.ToString(LMH030C.DATE_FORMAT)
            newRow.MAX_OUTKA_PLAN_DATE = range.ToDate.ToString(LMH030C.DATE_FORMAT)

            input.LMH030IN_OUTKA_SAVE_BP.AddLMH030IN_OUTKA_SAVE_BPRow(newRow)

            ' 運送会社検索[サーバー]
            Dim result As DataSet = MyBase.CallWSA(BLF_NAME, LMH030C.BLF_FUNC_NM_SELECT_UNSO_BP, input)

            If (result IsNot Nothing AndAlso _
                result.Tables(LMH030C.TABLE_NM_UNSO_BY_WGT_AND_DEST).Rows.Count > 0) Then

                _BpUnsocoCdTable = New LMH030DS.LMH030OUT_UNSO_BY_WGT_AND_DESTDataTable()
                _BpUnsocoCdTable.Merge(result.Tables(LMH030C.TABLE_NM_UNSO_BY_WGT_AND_DEST))

                _BpCharterTable = New LMH030DS.LMH030OUT_CHARTER_MANAGEMENTDataTable()
                If (result.Tables(LMH030C.TABLE_NM_CHARTER_MANAGEMENT).Rows.Count > 0) Then
                    _BpCharterTable.Merge(result.Tables(LMH030C.TABLE_NM_CHARTER_MANAGEMENT))
                End If

                isSuccess = True

            End If

        End Using

        Return isSuccess

    End Function


#End Region
#End If

#End Region

#Region "ﾊﾞｰｺｰﾄﾞ検索チェック"

    ''' <summary>
    ''' ﾊﾞｰｺｰﾄﾞより該当データにチェックをつける ADD 2017/06/14
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="txtBarcd"></param>
    ''' <remarks></remarks>
    Public Sub ChackBarCD(ByVal frm As LMH030F, ByVal txtBarcd As String)

        'If Len(frm.txtBarCD.TextValue) <> LMH030C.DIC_HAISO_SIJI_MO.DIGIT_NUMBER Then Exit Sub

        Dim chkFLg As String = LMConst.FLG.OFF
        Dim chkCnt As Integer = 0
        Dim maxRow As Integer = frm.sprEdiList.ActiveSheet.Rows.Count - 1

        If maxRow = 0 Then Exit Sub

        With frm.sprEdiList

            'ﾊﾞｰｺｰﾄﾞのチェック項目と比較
            For row As Integer = 1 To maxRow
                If (txtBarcd.ToString.Trim).Equals(Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(row, LMH030G.sprEdiListDef.HAISO_SIJI_NO.ColNo))) Then

                    .SetCellValue(row, LMH030G.sprEdiListDef.DEF.ColNo, True) 'LMConst.FLG.ON

                    chkFLg = LMConst.FLG.ON
                    chkCnt = chkCnt + 1
                End If

            Next

            If chkFLg.Equals(LMConst.FLG.ON) Then
                Call MyBase.ShowMessage(frm, "G035", New String() {"検索", txtBarcd.ToString.Trim & "(" & chkCnt & "件)にチェックしました。"})

            Else
                Call MyBase.ShowMessage(frm, "E078", New String() {txtBarcd.ToString.Trim & "は検索表示"})

            End If


        End With

        '検したバーコードをクリア（次入力のため）
        frm.txtBarCD.TextValue = String.Empty
        'フォーカス設定
        frm.txtBarCD.Focus()
    End Sub

#End Region

#Region "実績作成処理"
    ''' <summary>
    ''' 実績作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertCreatejisseki(ByVal frm As LMH030F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        '2011.09.25 修正START
        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataJissekiSakusei(frm, rtDs, eventshubetsu, errHashtable)

        Dim autoMatomeF As String = String.Empty

        autoMatomeF = rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(0)("AUTO_MATOME_FLG").ToString()

        If autoMatomeF.Equals("1") = True Then

            rtn = Me.ShowMessage(frm, "W178", New String() {"実績作成"})

            If rtn = MsgBoxResult.Ok Then
                '2011.09.30 追加 START GUI側エラーが出力されない対応
                'エラーをExcelに出力
                If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If
                '2011.09.30 追加 END 
            ElseIf rtn = MsgBoxResult.Cancel Then
                Call MyBase.ShowMessage(frm, "G007")
                Exit Sub

            End If

        Else

            rtn = Me.ShowMessage(frm, "C001", New String() {"実績作成"})

            If rtn = MsgBoxResult.Ok Then
                'エラーをExcelに出力
                If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                    Call Me.ExcelErrorSet(errDs)
                End If
            ElseIf rtn = MsgBoxResult.Cancel Then
                Call MyBase.ShowMessage(frm, "G007")
                Exit Sub
            End If

        End If
        '2011.09.25 修正END

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSakusei")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "JissekiSakusei", rtDs)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '2011.09.15 修正START
        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            If rtDs.Tables("WARNING_DTL").Rows.Count > 0 Then

                '2011.09.20 修正START
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtDs, frm, eventshubetsu)
                '2011.09.20 修正END
            End If

        ElseIf rtDs.Tables("WARNING_DTL").Rows.Count > 0 Then

            '2011.09.20 修正START
            'ワーニングが設定されている場合
            Call Me.CallWarning(rtDs, frm, eventshubetsu)
            '2011.09.20 修正END
        Else
            '実績作成処理成功時処理
            Call Me.SuccessJissekiSakusei(frm, rtDs)

        End If
        '2011.09.15 修正END

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSakusei")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region

#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiTorikesi(ByVal frm As LMH030F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績取消"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataJissekiTorikesi(frm, rtDs, eventshubetsu, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiTorikesi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "JissekiTorikesi", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)

        Else
            'EDI取消処理成功時処理
            Call Me.SuccessJissekiTorikesi(frm, rtDs)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiTorikesi")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region

#Region "EDI取消処理"
    ''' <summary>
    ''' EDI取消
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EdiTorikesi(ByVal frm As LMH030F, ByVal eventshubetsu As Integer, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"EDI取消"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataEdiTorikeshi(frm, rtDs, eventshubetsu, errHashtable)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EdiTorikesi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "EdiTorikesi", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)

        Else
            'EDI取消処理成功時処理
            Call Me.SuccessEdiTorikesi(frm, rtDs)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EdiTorikesi")

        Call Me._LMHconH.EndAction(frm)

    End Sub


    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DoubleClickChk(ByVal frm As LMH030F) As Boolean

        'クリックした行が検索行の場合
        If frm.sprEdiList.Sheets(0).ActiveRow.Index() = 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMH030F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' コンボで選択された種別の実行処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function Jikkou(ByVal frm As LMH030F, ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH030BLF", "Jikkou", ds)

        Return rtnDs

    End Function

#End Region

#Region "EDI取消⇒未登録"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TorikesiMitouroku(ByVal frm As LMH030F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"EDI取消⇒未登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataTorikesiMitouroku(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikesiMitouroku")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "TorikesiMitouroku", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            'EDI取消⇒未登録処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            'EDI取消⇒未登録処理成功時処理
            Call Me.SuccessTorikesiMitouroku(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikesiMitouroku")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region 'EDI取消⇒未登録

#Region "実績作成済⇒実績未"
    ''' <summary>
    ''' 実績作成済⇒実績未
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiSakuseiJissekimi(ByVal frm As LMH030F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績作成済⇒実績未"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataJissekiSakuseiJissekimi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSakuseiJissekimi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "JissekiSakuseiJissekimi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績作成済⇒実績未処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績作成済⇒実績未処理成功時処理
            Call Me.SuccessJissekiSakuseiJissekimi(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSakuseiJissekimi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績作成済⇒実績未

#Region "実績送信済⇒実績未"
    ''' <summary>
    ''' 実績送信済⇒実績未
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub JissekiSousinJissekimi(ByVal frm As LMH030F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績送信済⇒実績未"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataJissekiSousinJissekimi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JissekiSousinJissekimi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "JissekiSousinJissekimi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績送信済⇒実績未処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績送信済⇒実績未処理成功時処理
            Call Me.SuccessJissekiSousinJissekimi(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "JissekiSousinJissekimi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績送信済⇒実績未

#Region "「～⇒実績未」処理共通 Rapidus次回分納情報取得"

    ''' <summary>
    ''' 「～⇒実績未」処理共通 Rapidus次回分納情報取得
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="outkaNoL"></param>
    ''' <param name="ediCtlNo"></param>
    ''' <returns></returns>
    Friend Function SelectJikaiBunnouInfo(ByVal nrsBrCd As String, ByVal outkaNoL As String, ByVal ediCtlNo As String) As DataSet

        Dim ds As DataSet = New LMZ390DS
        Dim dr As DataRow = ds.Tables("LMZ390IN").NewRow

        dr.Item("NRS_BR_CD") = nrsBrCd
        dr.Item("OUTKA_CTL_NO") = outkaNoL
        dr.Item("EDI_CTL_NO") = ediCtlNo
        dr.Item("TEMPLATE_PREFIX") = LMZ390C.TEMPLATE_PREFIX
        ds.Tables("LMZ390IN").Rows.Add(dr)

        ' Rapidus次回分納情報取得
        ds = MyBase.CallWSA("LMZ390BLF", "SelectJikaiBunnouInfo", ds)

        Return ds

    End Function

#End Region ' "「～⇒実績未」処理共通 Rapidus次回分納情報取得"

#Region "実績送信済⇒送信待"
    ''' <summary>
    ''' 実績送信済⇒送信待
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SousinSousinmi(ByVal frm As LMH030F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"実績送信済⇒実績送信待"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataSousinmi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SousinSousinmi")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "SousinSousinmi", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '実績送信済⇒送信待処理失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '実績送信済⇒送信待処理成功時処理
            Call Me.SuccessSakuseizumiJissekimi(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SousinSousinmi")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '実績送信済⇒送信待

    '要望番号1129 2012.06.11 修正START
#Region "追加実行処理"
    ''' <summary>
    ''' 追加実行
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TuikaJikkou(ByVal frm As LMH030F, ByVal eventShubetsu As Integer, ByVal strRtn() As String)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"追加実行"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TuikaJikkou")

        '(2012.10.09) 要望番号1502 追加START
        '既に存在するファイルの削除
        System.IO.File.Delete(strRtn(0))
        '(2012.10.09) 要望番号1502 追加END

        Dim sw As New LMTextUtility

        '変数の初期化
        Dim strvalue As StringBuilder = New StringBuilder

        '半角空白を書き込む
        strvalue.Append(Space(1))

        'ファイルの作成
        sw.CreateFile(strRtn(0), strvalue, False)
        'Dim sw As New System.IO.StreamWriter(strRtn, True, System.Text.Encoding.GetEncoding("Shift-JIS"))

        '閉じる

        '追加実行処理成功時処理
        Call Me.SuccessTuikaJikkou(frm)

        ''==== WSAクラス呼出 ====
        'rtDs = MyBase.CallWSA("LMH030BLF", "SakuraTuikaJikkou", rtDs)

        ''メッセージコードの判定
        'If MyBase.IsErrorMessageExist() = True Then
        '    'サクラ追加実行処理失敗時、返却メッセージを設定
        '    MyBase.ShowMessage(frm)
        '    Call Me._LMHconH.EndAction(frm)
        '    Exit Sub
        'Else

        'End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "TuikaJikkou")

        Call Me._LMHconH.EndAction(frm)
    End Sub
#End Region '追加実行
    '要望番号1129 2012.06.11 修正END

#Region "出荷取消⇒未登録"
    ''' <summary>
    ''' 出荷取消⇒未登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Mitouroku(ByVal frm As LMH030F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"出荷取消⇒未登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataTourokumi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Mitouroku")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "Mitouroku", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '出荷取消⇒未登録失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '出荷取消⇒未登録処理成功時処理
            Call Me.SuccessTourokuzumiMitouroku(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Mitouroku")

        Call Me._LMHconH.EndAction(frm)
    End Sub

#End Region '出荷取消⇒未登録

#Region "運送取消⇒未登録"
    ''' <summary>
    ''' 運送取消⇒未登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UnsoMitouroku(ByVal frm As LMH030F, ByVal eventShubetsu As Integer)
        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"運送取消⇒未登録"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataUnsoTourokumi(frm, rtDs, eventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UnsoMitouroku")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "UnsoMitouroku", rtDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            '運送取消⇒未登録失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        Else
            '運送取消⇒未登録処理成功時処理
            Call Me.SuccessUnsotorikesiMitouroku(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UnsoMitouroku")

        Call Me._LMHconH.EndAction(frm)
    End Sub

#End Region '出荷取消⇒未登録

#Region "一括変更処理"
    Private Sub IkkatsuHenko(ByVal frm As LMH030F, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"一括変更"})

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LMH030_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call MyBase.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Call Me.SetDataHenkoKey(frm, rtDs, errHashtable)
        Call Me.SetDataHenkoValue(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Henko")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH030BLF", "UpdateHenko", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)

        Else
            '一括変更成功時処理
            Call Me.IkkatuHenko(frm, rtDs)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Henko")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '一括変更

    '取込対応 20120305  Start
#Region "取込処理"
    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Torikomi(ByVal frm As LMH030F, ByVal eventshubetsu As Integer, ByVal errDs As DataSet, ByVal rtDs As DataSet)

        Dim rtDr As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)
        Dim rcv_dir As String = rtDr.Item("RCV_INPUT_DIR").ToString
        Dim work_dir As String = rtDr.Item("WORK_INPUT_DIR").ToString
        Dim bak_dir As String = rtDr.Item("BACKUP_INPUT_DIR").ToString

        ''続行確認
        'Dim rtn As MsgBoxResult

        'If rtDr.Item("SEMI_EDI_FLAG").ToString = "1" Then
        '    rtn = Me.ShowMessage(frm, "C001", New String() {"取込処理"})
        'Else
        '    rtn = Me.ShowMessage(frm, "C001", New String() {"緊急時EDI手動受信"})
        'End If

        'If rtn = MsgBoxResult.Ok Then
        'ElseIf rtn = MsgBoxResult.Cancel Then
        '    Call MyBase.ShowMessage(frm, "G007")
        '    Exit Sub
        'End If

        'DataSet設定（EDI取込DSヘッダー）
        rtDs = Me.SetDataEdiTorikomiHed(frm, rtDs, eventshubetsu)

        'ＪＴ物流（千葉）の場合、ファイル名等を表示し、続行確認を行う
        If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "70" Then

            'ファイル名称から翌日、翌営業日を取得する
            '要望番号1593:(【セミEDI】JT物流　取込ファイル名変更) 2012/11/14 本明 Start
            'Dim sFileDate As String = Mid(rtDs.Tables(LMH030C.EDI_TORIKOMI_HED).Rows(0).Item("FILE_NAME_RCV").ToString(), 11, 8)
            Dim sFileDate As String = Mid(rtDs.Tables(LMH030C.EDI_TORIKOMI_HED).Rows(0).Item("FILE_NAME_RCV").ToString(), 1, 8)
            '要望番号1593:(【セミEDI】JT物流　取込ファイル名変更) 2012/11/14 本明 End
            Dim sNextDate As String = Format(DateSerial(Convert.ToInt32(Left(sFileDate, 4)), Convert.ToInt32(Mid(sFileDate, 5, 2)), Convert.ToInt32(Right(sFileDate, 2)) + 1), "yyyyMMdd")
            Dim sNextEigyoDate As String = Format(Me.GetBussinessDay(sFileDate, +1), "yyyyMMdd")

            'メッセージ作成
            Dim sMsgDateInfo As String = String.Empty
            Dim sMsgFileInfo As String = String.Empty

            '翌日と翌営業日を比較してメッセージ作成
            If sNextDate = sNextEigyoDate Then
                sMsgDateInfo = String.Concat("出荷予定日：", sNextDate)
            Else
                sMsgDateInfo = String.Concat("出荷予定日：", sNextDate, "～", sNextEigyoDate)
            End If

            'ファイル日付と当日を比較してメッセージ作成
            If sFileDate = MyBase.GetSystemDateTime(0) Then
                sMsgFileInfo = String.Empty     '表示しない
            Else
                sMsgFileInfo = String.Concat("受信ファイル：", sFileDate, " 発行分")
            End If

            '続行確認
            Dim rtn As MsgBoxResult = Me.ShowMessage(frm, "C010", New String() {"[ＪＴ物流]", sMsgDateInfo, sMsgFileInfo, vbNewLine})

            If rtn = MsgBoxResult.Ok Then
            ElseIf rtn = MsgBoxResult.Cancel Then
                Call MyBase.ShowMessage(frm, "G007")
                Call Me._LMHconH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        '大日本住友製薬（大阪）の場合、月末２営業日以内の場合は、警告メッセージを表示する ADD 2017/05/19
        If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "82" Then
            '荷主詳細  SUB_KB = '0V'より、月末営業日日数取得
            Dim sSql As String = "NRS_BR_CD = '" & frm.cmbEigyo.SelectedValue.ToString() & "' AND CUST_CD = '" & frm.txtCustCD_L.TextValue.ToString() & frm.txtCustCD_M.TextValue.ToString() & "' " _
                                 & " AND SUB_KB = '0V' "
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sSql)
            If dr.Length > 0 Then
                Dim sSetNaiyo As String = CStr(dr(0).Item("SET_NAIYO"))
                Dim sGetEigyoDate As String = Format(Me.GetGetsumatu2Day(CInt(dr(0).Item("SET_NAIYO"))), "yyyyMMdd")

                Dim sysDate As String = MyBase.GetSystemDateTime()(0)

                '取得した月末対象営業日 <= システム日付のとき警告メッセージ
                If sGetEigyoDate <= sysDate Then
                    Me.ShowMessage(frm, "C016", New String() {sSetNaiyo})
                End If
            End If


        End If


        Try
            '受信ファイル操作
            Dim fileDr As DataRow
            Dim file_Name_Rcv As String = String.Empty
            Dim file_Name_Ope As String = String.Empty

            For i As Integer = 0 To rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows.Count - 1
                fileDr = rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i)
                'ファイル名
                file_Name_Rcv = fileDr.Item("FILE_NAME_RCV").ToString()
                file_Name_Ope = fileDr.Item("FILE_NAME_OPE").ToString()
                '受信ファイルを作業フォルダにコピー
                System.IO.File.Copy(String.Concat(rcv_dir, file_Name_Rcv), String.Concat(work_dir, file_Name_Ope))
                '受信ファイルをロック
                FileOpen(i + 1, String.Concat(rcv_dir, file_Name_Rcv), OpenMode.Binary, OpenAccess.ReadWrite)
            Next
        Catch ex As Exception
            MyBase.ShowMessage(frm, "E461")
            Exit Sub
        End Try

        'DataSet設定（EDI取込DS詳細）

        '取込 固定長対応 2012/08/06 本明 Start 
        'rtDs = Me.SetDataEdiTorikomiShosai(frm, rtDs, eventshubetsu)
        Select Case rtDr.Item("DELIMITER_KB").ToString()
            Case "01", "02"     'カンマ区切り、TAB区切りの場合
                rtDs = Me.SetDataEdiTorikomiShosai(frm, rtDs, eventshubetsu)

            Case "03"           '固定長の場合
                rtDs = Me.SetDataEdiTorikomiShosaiFixedLength(frm, rtDs, eventshubetsu)

            Case "04"           'EXCELの場合
                '取込 千葉対応 2012/09/03 本明 Start 
                rtDs = Me.SetDataEdiTorikomiShosaiExcel(frm, rtDs, eventshubetsu)
                '取込 千葉対応 2012/09/03 本明 End 
        End Select
        '取込 固定長対応 2012/08/06 本明 Start 

        'If rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Rows.Count = 0 Then
        '    MyBase.ShowMessage(frm, "E024")
        '    System.IO.File.Delete(String.Concat(work_dir, file_Name_Ope))
        '    Call Me._LMHconH.EndAction(frm)
        '    Exit Sub
        'End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SemiEdiTorikomi")
        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH030BLF", "SemiEdiTorikomi", rtDs)

        '受信ファイル操作
        Dim rtnFileDr As DataRow
        Dim rtnErrFlg As String
        Dim rtnFile_Name_Rcv As String = String.Empty
        Dim rtnFile_Name_Ope As String = String.Empty
        Dim rtnFile_Name_Bak As String = String.Empty

        For i As Integer = 0 To rtnDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows.Count - 1
            rtnFileDr = rtnDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i)

            rtnErrFlg = rtnFileDr.Item("ERR_FLG").ToString()
            rtnFile_Name_Rcv = rtnFileDr.Item("FILE_NAME_RCV").ToString()
            rtnFile_Name_Ope = rtnFileDr.Item("FILE_NAME_OPE").ToString()
            rtnFile_Name_Bak = rtnFileDr.Item("FILE_NAME_BAK").ToString()

            'エラーフラグが"0"（正常）の場合
            If rtnErrFlg.Equals("0") Then
                '作業ファイルをバックアップフォルダにコピー
                System.IO.File.Copy(String.Concat(work_dir, rtnFile_Name_Ope), String.Concat(bak_dir, rtnFile_Name_Bak))
                '受信ファイルのロックを解除
                FileClose(i + 1)
                '受信ファイルを削除
                System.IO.File.Delete(String.Concat(rcv_dir, rtnFile_Name_Rcv))
                '作業ファイルを削除
                System.IO.File.Delete(String.Concat(work_dir, rtnFile_Name_Ope))
            Else
                '受信ファイルのロックを解除
                FileClose(i + 1)
                '作業ファイルを削除
                System.IO.File.Delete(String.Concat(work_dir, rtnFile_Name_Ope))
            End If

        Next

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            If rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

                '2011.09.20 修正START
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtnDs, frm, eventshubetsu)
                '2011.09.20 修正END
            End If

        ElseIf rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

            '2011.09.20 修正START
            'ワーニングが設定されている場合
            Call Me.CallWarning(rtnDs, frm, eventshubetsu)
            '2011.09.20 修正END

        Else
            '取込処理成功時処理
            Call Me.SuccessTorikomi(frm, rtnDs)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SemiEdiTorikomi")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '取込処理
    '取込対応 20120305  End

    '2014/03/09 黎 取込処理(セミEDI標準化対応) -- ST --
#Region "取込処理(セミEDI標準化対応版)"
    ''' <summary>
    ''' 取込処理(セミEDI標準化対応版)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TorikomiStanderdEdition(ByVal frm As LMH030F, ByVal eventshubetsu As Integer, ByVal errDs As DataSet, ByVal rtDs As DataSet)

        Dim rtDr As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)
        Dim rcv_dir As String = String.Empty
        Dim work_dir As String = String.Empty

        '2019/07/18 依頼番号:006754 add start
        'EdiIndexを取得
        Dim iEdiIndex As Integer = Convert.ToInt32(rtDs.Tables("LMH030_SEMIEDI_INFO").Rows(0)("EDI_CUST_INDEX"))
        '2019/07/18 依頼番号:006754 add end

        '========= ファイル選択処理 =======
        'WindowsDialogインスタンス生成
        Dim ofd As New OpenFileDialog()

        'WindowsDialogのタイトル設定
        ofd.Title = "取込むファイルを選択してください"

        '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 upd start
        Dim filter As String = String.Empty

        If IsDspGokyu(rtDs).Equals(True) Then
            'DSP五協フード＆ケミカル株式会社の場合、以下を行う
            'フィルターにCSV、EXCELの両方を出力するように設定する
            filter = "CSVファイル(*.csv)Excelファイル(*.xls)|*.csv;*.xls"
        Else
            '[ファイルの種類]に表示される選択肢を制限
            '[M_SEMI_EDI_INFO_STATE]よりRCV_FILE_EXTENTIONに取込可能拡張子を設定
            Dim Delimiter As String = String.Concat("*.", rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("RCV_FILE_EXTENTION"))

            '取込ファイルのフィルタ設定
            Select Case rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("DELIMITER_KB").ToString()
                Case "01" 'カンマ区切り
                    filter = String.Concat("CSVファイル(", Delimiter, ")|", Delimiter)
                Case "02" 'TAB区切りの場合
                    filter = String.Concat("TSVファイル(", Delimiter, ")|", Delimiter)
                Case "03" '固定長の場合
                    filter = String.Concat("Textファイル(", Delimiter, ")|", Delimiter)
                Case "04" 'EXCELの場合
                    filter = String.Concat("Excelファイル(", Delimiter, ")|", Delimiter)
            End Select

        End If
        '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 upd end
        ofd.Filter = filter
        ofd.FilterIndex = 1

        '2015.05.18 修正START ローム2ファイル取込み対応
        Dim kbnDr() As DataRow = Nothing

        '複数選択対応()
        If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("PLURAL_FILE_FLAG").Equals("2") = True Then
            ofd.Multiselect = True
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                 ("NRS_BR_CD = '" & frm.cmbEigyo.SelectedValue.ToString() & "' AND CUST_CD = '" & frm.txtCustCD_L.TextValue.ToString() & "' AND SUB_KB = '99' ")

        Else
            ofd.Multiselect = False
        End If
        'ofd.Multiselect = False
        '2015.05.18 修正END ローム2ファイル取込み対応

        'ファイル名取得
        Dim objFiles As ArrayList = New ArrayList
        Dim arrCnt As Integer = 0
        If ofd.ShowDialog() = DialogResult.OK Then
            For Each newArr As String In ofd.SafeFileNames
                objFiles.Add(newArr)
                '2015.05.18 追加START ローム2ファイル取込み対応
                If Not kbnDr Is Nothing Then
                    Exit For
                End If
                '2015.05.18 追加END ローム2ファイル取込み対応
            Next
        Else
            Exit Sub
        End If

        '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
        If IsDspGokyu(rtDs).Equals(True) Then
            ' DSP五協フード＆ケミカル株式会社の場合
            If Path.GetExtension(objFiles(0).ToString()).Equals(".xls") Then
                rtDs = SetCachedSemiEDI(frm, LMH030C.IN_OUT_KBN_BY_FILE.IN_OUT_EXCEL)
                rtDr = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)
            End If
        End If
        '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

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
        '2015.05.18 修正START ローム2ファイル取込み対応
        If Me._V.IsTorikomiKanrenCheckStanderdEdition(rtDs, objFiles, ofd.SafeFileNames) = False Then
            Exit Sub
        End If

        Dim stTime As Date = Now
        '2015.05.18 修正END ローム2ファイル取込み対応

        '======================受信ファイル操作 -ED- ======================
        'ADD 2016/09/13 丸和(横浜)対応 Start
        '荷主明細取得
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString()    '荷主コード(大)
        Dim custCdM As String = frm.txtCustCD_M.TextValue.ToString()    '荷主コード(中)   
        Dim YokoMaruwaFLG As Boolean = False

        Dim kbnDr0S() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL + custCdM & "' AND SUB_KB = '0S' ")

        If kbnDr0S.Length > 0 Then

            YokoMaruwaFLG = True
        End If
        'ADD 2016/09/13 丸和(横浜)対応 End

        'コネクションリスト
        Dim arrCloser As ArrayList = New ArrayList

        Select Case rtDr.Item("DELIMITER_KB").ToString()
            Case "01", "02"     'カンマ区切り、TAB区切りの場合
                rtDs = Me.SetDataEdiTorikomiShosaiStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)

            Case "03"           '固定長の場合
                Select Case iEdiIndex
                    Case EdiCustIndex.TrmSmpl00409_01
                        ' テルモ サンプル(土気)
                        rtDs = Me.SetDataEdiTorikomiShosaiReadLineStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)
                    Case Else
                        rtDs = Me.SetDataEdiTorikomiShosaiFixedLengthStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)
                End Select

            Case "04"           'EXCELの場合
                'UPD 2016/09/13 丸和(横浜)対
                If YokoMaruwaFLG = True Then
                    rtDs = Me.SetDataEdiTorikomiShosaiExcelMaruwaEdition(frm, rtDs, eventshubetsu, arrCloser)
                Else
                    '2019/07/18 依頼番号:006754 del start
                    'rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)
                    '2019/07/18 依頼番号:006754 del end
                    '2019/07/18 依頼番号:006754 add start
                    Select Case iEdiIndex
                        Case EdiCustIndex.AgcW00440
                            '大阪：ＡＧＣ若狭化学
                            rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser, "EDI")
                        Case EdiCustIndex.CJC00787
                            '千葉：コーヴァンス・ジャパン
                            rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser, "Order Tracker")
                        Case Else
                            rtDs = Me.SetDataEdiTorikomiShosaiExcelStanderdEdition(frm, rtDs, eventshubetsu, arrCloser)
                    End Select
                    '2019/07/18 依頼番号:006754 add end

                End If
        End Select

        '2015.05.18 修正START ローム2ファイル取込み対応
        If Not kbnDr Is Nothing Then
            arrCloser = New ArrayList
            Select Case rtDr.Item("DELIMITER_KB").ToString()

                Case "04"           'EXCELの場合
                    'Shipmentファイルが含まれていない場合はエラー
                    If Not kbnDr Is Nothing AndAlso _
                        InStr(ofd.SafeFileNames(1).ToString(), kbnDr(0).Item("SET_NAIYO").ToString()) = 0 Then
                        Me.SetMessage("E199", New String() {"Shipmentファイル"})   'エラーメッセージ
                        'メッセージコードの判定
                        If MyBase.IsMessageExist = True Then
                            Me.aProcessKill("EXCEL", stTime)
                            MyBase.ShowMessage(frm)
                            Exit Sub
                        End If
                    End If
                    'UPD START 2022/10/28 033290 大阪ローム対EDI改修応
                    'rtDs = Me.SetDataEdiTorikomipluralfileExcel(frm, rtDs, eventshubetsu, arrCloser, ofd.FileNames(1).ToString(), ofd.SafeFileNames(1).ToString())
                    If iEdiIndex.Equals(136) Then
                        'ローム(大阪)
                        rtDs = Me.SetDataEdiTorikomipluralfileExcelRomeOsaka(frm, rtDs, eventshubetsu, arrCloser, ofd.FileNames(1).ToString(), ofd.SafeFileNames(1).ToString())
                    Else
                        rtDs = Me.SetDataEdiTorikomipluralfileExcel(frm, rtDs, eventshubetsu, arrCloser, ofd.FileNames(1).ToString(), ofd.SafeFileNames(1).ToString())
                    End If
                    'UPD END 2022/10/28 033290 大阪ロームEDI改修

                Case Else

            End Select

        End If
        '2015.05.18 修正END ローム2ファイル取込み対応

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SemiEdiTorikomi")
        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH030BLF", "SemiEdiTorikomi", rtDs)

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


        '20160830
        'ロームの場合はバックアップ処理を行わない
        '(バックアップ用ディレクトリの表示もしない)

        If Not kbnDr Is Nothing Then



        Else



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

        End If
        '=============ファイル保存実行 --ED-- =============

        For i As Integer = 0 To rtnDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows.Count - 1
            rtnFileDr = rtnDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i)

            rtnErrFlg = rtnFileDr.Item("ERR_FLG").ToString()
            rtnFile_Name_Rcv = rtnFileDr.Item("FILE_NAME_RCV").ToString()

            'エラーフラグ判定
            If rtnErrFlg.Equals("0") Then

                '2015.05.18 修正START ローム2ファイル取込み対応
                If Not kbnDr Is Nothing Then

                    '20160830 ロームのファイルは非同期のため
                    'バックアップのファイル名はそのままでプロセスのみKILL

                    'ファイル名の変更
                    'noExtends = System.IO.Path.GetFileNameWithoutExtension(String.Concat(rcv_dir, ofd.SafeFileNames(1).ToString()))
                    'rtnFile_Name_Bak = String.Concat(noExtends, "_", MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1), ofd.SafeFileNames(1).ToString().Replace(noExtends, ""))

                    'ファイルのコピーを作成
                    'System.IO.File.Copy(String.Concat(rcv_dir, ofd.SafeFileNames(1).ToString()), String.Concat(backDir, rtnFile_Name_Bak), True)

                    '各クローズ処理
                    DoCloseAction(rtDs, arrCloser, i)

                    'オリジナル削除
                    'System.IO.File.Delete(String.Concat(rcv_dir, ofd.SafeFileNames(1).ToString()))

                Else

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

                End If
                '2015.05.18 修正END ローム2ファイル取込み対応

                '2015.05.18 修正START ローム2ファイル取込み対応
                If Not kbnDr Is Nothing Then

                    '[正常時処理]
                    '受信ファイルのロックを解除 + オリジナルの削除＆コピーの作成

                    '20160830 ロームのファイルは非同期のため
                    'バックアップのファイル名はそのままでプロセスのみKILL

                    'ファイル名の変更
                    ' noExtends = String.Empty
                    ' rtnFile_Name_Bak = String.Empty
                    ' noExtends = System.IO.Path.GetFileNameWithoutExtension(String.Concat(rcv_dir, rtnFile_Name_Rcv))
                    ' rtnFile_Name_Bak = String.Concat(noExtends, "_", MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1), rtnFile_Name_Rcv.Replace(noExtends, ""))

                    'ファイルのコピーを作成
                    'System.IO.File.Copy(String.Concat(rcv_dir, rtnFile_Name_Rcv), String.Concat(backDir, rtnFile_Name_Bak), True)

                    Me.aProcessKill("EXCEL", stTime)

                    'オリジナル削除
                    'System.IO.File.Delete(String.Concat(rcv_dir, rtnFile_Name_Rcv))

                End If
                '2015.05.18 修正END ローム2ファイル取込み対応

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

    '2015.05.18 修正START ローム2ファイル取込み対応
    Function aProcessKill(ByVal pApplicationName As String, ByVal stTime As Date) As Integer

        '戻り値は処理を実行開始した時間以降に起動されたアプリケーションを強制終了させた数
        Dim sdProcesses As System.Diagnostics.Process() = System.Diagnostics.Process.GetProcessesByName(pApplicationName)
        aProcessKill = 0
        ' 取得できたプロセスからプロセス ID を取得する
        For Each sdProcess As System.Diagnostics.Process In sdProcesses
            '2016.01.28 要望番号2497 修正START
            If sdProcess.StartTime > stTime AndAlso sdProcess.HasExited = False Then
                '2016.01.28 要望番号2497 修正END
                sdProcess.Kill()
                aProcessKill += 1
                '2015.06.16 ローム複数取込　処理追加START
                Do While sdProcess.HasExited = False
                    'プロセスが切れていない場合は5秒待機
                    System.Threading.Thread.Sleep(5000)
                Loop
                '2015.06.16 ローム複数取込　処理追加END
            End If
        Next

        sdProcesses = Nothing
    End Function
    '2015.05.18 修正END ローム2ファイル取込み対応

    '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
    ''' <summary>
    ''' DSP五協フード＆ケミカル株式会社か否かを判断する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True = DSP五協フード＆ケミカル株式会社 : False = DSP五協フード＆ケミカル株式会社以外</returns>
    ''' <remarks></remarks>
    Private Function IsDspGokyu(ByVal ds As DataSet) As Boolean
        'DSP五協フード＆ケミカル株式会社（横浜）
        If ds.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("NRS_BR_CD").ToString = "40" AndAlso _
           ds.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("CUST_CD_L").ToString = "00456" Then
            Return True
        End If

        'DSP五協フード＆ケミカル株式会社（千葉）
        If ds.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("NRS_BR_CD").ToString = "10" AndAlso _
           ds.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("CUST_CD_L").ToString = "00074" Then
            Return True
        End If

        'DSP五協フード＆ケミカル株式会社（大阪）
        If ds.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("NRS_BR_CD").ToString = "20" AndAlso _
           ds.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("CUST_CD_L").ToString = "31791" Then
            Return True
        End If

        Return False
    End Function
    '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

    ''' <summary>
    ''' 各種閉じる処理の実行
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <param name="arrCloser"></param>
    ''' <remarks></remarks>
    Private Sub DoCloseAction(ByVal rtDs As DataSet, ByVal arrCloser As ArrayList, ByVal nawRow As Integer)
        'セミEDI情報
        Dim rtDr As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

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
    '2014/03/09 黎 取込処理(セミEDI標準化対応) -- ED --

#Region "印刷処理(画面の検索条件)"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelPrint(ByVal frm As LMH030F, ByVal printShubetsu As Integer, _
                                 ByVal eventShubetsu As LMH030C.EventShubetsu)

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()
        Select Case printShubetsu

            '2012.09.11 要望番号1429 修正START
            Case DirectCast(LMH030C.Print_KBN.EDIOUTKACHECKLIST, Integer) _
               , DirectCast(LMH030C.Print_KBN.EDIOUTKATORIKESILIST, Integer)
                '2012.09.11 要望番号1429 修正END

                If Me.SetDataSetInData(frm, rtDs, printShubetsu, eventShubetsu) = False Then
                    MyBase.ShowMessage(frm, "E361")
                    Me._LMHconV.SetErrorControl(frm.txtCustCD_L)
                    Exit Sub

                ElseIf 0 = rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Count Then
                    Exit Sub

                End If

            Case Else

                Exit Sub

        End Select

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH030BLF", "PrintData", rtDs)

        'エラー帳票出力の判定
        If MyBase.IsMessageStoreExist() = True Then
            Call Me.OutputExcel(frm)
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintData")

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})

        'プレビュー処理の呼出
        '2012.03.22 修正START
        Call Me.prev(rtnDs)
        'Call Me.prev(rtDs)
        '2012.03.22 修正END

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

#End Region 'Spread条件印刷処理

    '▼▼▼要望番号:467
#Region "出力(CSV作成・印刷)"

    ''' <summary>
    ''' 出力(CSV作成・印刷)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OutputPrint(ByVal frm As LMH030F)

        '2012.03.03 大阪対応START
        Select Case frm.cmbOutput.SelectedValue.ToString()

            Case LMH030C.PRINT_CSV  '出荷依頼送信データ作成
                Call Me.OutputCsv(frm)

                '(2012.03.16)大阪対応 START
                '(2012.05.08)要望番号1007  追加START '要望番号:1444 terakawa 2012.09.18 追加
                '大阪対応 START受信帳票,受信一覧表,出荷伝票,一括印刷,EDI出荷取消チェックリスト
                '(2012.12.17)EDI納品書_BP,EDI納品書_BPオートバックス(埼玉),EDI納品書_BPタクティー(埼玉),EDI納品書_BPイエローハット(埼玉) 追加 
                '(2012.12.17)EDI納品書_日興イエローハット(大阪),EDI納品書_ロンザ(千葉) 追加 
            Case LMH030C.JYUSIN_PRT _
               , LMH030C.JYUSIN_ICHIRAN _
               , LMH030C.OUTKA_PRT _
               , LMH030C.IKKATU_PRT _
               , LMH030C.EDIOUTKA_TORIKESHI_CHECKLIST _
               , LMH030C.NOHIN_OKURIJO _
               , LMH030C.NOHINSYO_AUTO_BAKKUSU _
               , LMH030C.NOHIN_TACTI _
               , LMH030C.NOHIN_YELLOW_HAT _
               , LMH030C.NOHIN_NIKKO _
               , LMH030C.NOHIN_RONZA _
               , LMH030C.NOHIN_OKURIJO_AUTO _
               , LMH030C.SHIKIRI_TERUMO _
               , LMH030C.NIHUDA_TOR _
               , LMH030C.INVOICE_NIPPON_EXPRESS_BP _
               , LMH030C.NOHIN_NICHIGO

                '2012.03.16 大阪対応 END

                '要望番号1061 2012.05.15 追加・修正START
                'DataSet設定
                Dim rtDs As DataSet = New LMH030DS()

                '(2013.02.05)要望番号1822 BP納品系は一覧選択で出力 -- START --
                Select Case frm.cmbOutput.SelectedValue.ToString()
                    Case LMH030C.NOHIN_OKURIJO _
                       , LMH030C.NOHINSYO_AUTO_BAKKUSU _
                       , LMH030C.NOHIN_TACTI _
                       , LMH030C.NOHIN_YELLOW_HAT _
                       , LMH030C.NOHIN_OKURIJO_AUTO _
                       , LMH030C.INVOICE_NIPPON_EXPRESS_BP

                        rtDs = Me.SetDataOutputZumi(frm, rtDs)

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
                    Case LMH030C.NOHIN_NICHIGO
                        rtDs = SetDataOutputSelectedRows(frm, rtDs)
#End If

                    Case Else
                        'BP系以外の帳票
                        Select Case frm.cmbOutputKb.SelectedValue.ToString()
                            Case LMH030C.OUTPUT_SUMI
                                rtDs = Me.SetDataOutputZumi(frm, rtDs)

                            Case Else
                                rtDs = Me.OutputPrt(frm, rtDs)

                        End Select

                End Select
                '(2013.02.05)要望番号1822 BP納品系は一覧選択で出力 --  END  --

                'Dim rtDs As DataSet = Me.OutputPrt(frm)
                '要望番号1061 2012.05.15 追加・修正END

                '要望番号1007 2012.05.08 修正START
                '一括印刷の場合はエラーEXCELを使用

                'メッセージ情報を初期化する
                MyBase.ClearMessageStoreData()


                '(2013.02.05)要望番号1822 BP納品系は一覧選択で出力 -- START --
                Select Case frm.cmbOutput.SelectedValue.ToString()
                    Case LMH030C.NOHIN_OKURIJO _
                       , LMH030C.NOHINSYO_AUTO_BAKKUSU _
                       , LMH030C.NOHIN_TACTI _
                       , LMH030C.NOHIN_YELLOW_HAT _
                       , LMH030C.NOHIN_OKURIJO_AUTO _
                       , LMH030C.INVOICE_NIPPON_EXPRESS_BP

                        Dim cntDt As DataTable = rtDs.Tables("LMH030_PRT_CNT")
                        Dim PRT_CNT As String = cntDt.Rows(0).Item("PRT_CNT").ToString
                        '2013.06.17 追加START
                        Dim OUTPUT_CNT As String = cntDt.Rows(0).Item("OUTPUT_CNT").ToString
                        '2013.06.17 追加END

                        '処理終了メッセージの表示
                        If MyBase.IsMessageStoreExist = False And MyBase.IsMessageExist = False Then
                            '2013.06.17 追加START
                            'MyBase.ShowMessage(frm, "G055", New String() {PRT_CNT, "印刷処理"})
                            MyBase.ShowMessage(frm, "G056", New String() {String.Concat("オーダー:", PRT_CNT), String.Concat("印刷枚数:", OUTPUT_CNT), "印刷処理"})
                            '2013.06.17 追加END
                        End If

                        'エラーEXCELを表示
                        'メッセージコードの判定
                        If MyBase.IsMessageStoreExist = True Then
                            Call Me.OutputExcel_BP(frm, rtDs)
                        End If

                    Case Else
                        '(2013.02.05)要望番号1822 BP納品系は一覧選択で出力 --  END  --

                        '(2013.01.23)印刷完了メッセージの追加 -- START --
                        '処理終了メッセージの表示
                        If MyBase.IsMessageStoreExist = False And MyBase.IsMessageExist = False Then
                            MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})
                        End If
                        '(2013.01.23)印刷完了メッセージの追加 --  END  --

                        '出力済はエラーEXCELを表示
                        If (frm.cmbOutputKb.SelectedValue.ToString()).Equals(LMH030C.OUTPUT_SUMI) = True Then

                            'メッセージコードの判定
                            If MyBase.IsMessageStoreExist = True Then

                                Call Me.OutputExcel(frm)

                            End If

                        Else
                            '未出力で一括印刷はエラーEXCELを表示
                            If (frm.cmbOutput.SelectedValue.ToString()).Equals(LMH030C.IKKATU_PRT) = True Then

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
                End Select

                'プレビュー処理の呼出
                Call Me.prev(rtDs)

                '処理終了アクション
                Call Me._LMHconH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.UnLockedForm()

                '2012.04.18 要望番号1005 追加START
            Case LMH030C.RCVCONF_SEND   '受信確認送信

                'データセット設定
                Dim rtDs As DataSet = Me.RcvConfirmSend(frm)
                If MyBase.IsMessageExist = True Then
                    MyBase.ShowMessage(frm)
                    Exit Sub
                End If
                '項目チェック
                If Me._V.IsRcvConfirmSendCheck(rtDs) = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '続行確認
                Dim rtn As MsgBoxResult

                rtn = Me.ShowMessage(frm, "C001", New String() _
                {String.Concat(rtDs.Tables(LMH030C.TABLE_NM_RCVCONF_INFO).Rows(0).Item("CUST_NM").ToString(), "の受信確認送信")})

                If rtn = MsgBoxResult.Ok Then
                ElseIf rtn = MsgBoxResult.Cancel Then
                    Call MyBase.ShowMessage(frm, "G007")
                    Exit Sub
                End If

                'ログ出力
                MyBase.Logger.StartLog(MyBase.GetType.Name, "RcvConFirmSendCsv")

                'CSVファイル検索処理
                '送信CSV作成
                If Me.SerchEdiCrtCsv(rtDs) = False Then

                    'メッセージコードの判定
                    If MyBase.IsMessageExist = True Then

                        MyBase.ShowMessage(frm)

                    End If

                Else

                    'ログ出力
                    MyBase.Logger.EndLog(MyBase.GetType.Name, "RcvConFirmSendCsv")

                    '処理終了アクション
                    Call Me._LMHconH.EndAction(frm)

                    'ファンクションキーの設定
                    Call Me._G.UnLockedForm()

                    '受信確認送信処理成功時処理
                    Call Me.SuccessRcvConfirmSendCsv(frm)

                End If


                '2012.04.18 要望番号1005 追加END
            Case Else

        End Select
        '2012.03.03 大阪対応END

    End Sub

    ''' <summary>
    ''' 出荷依頼送信データ作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutputCsv(ByVal frm As LMH030F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()

        'CSV作成INデータ設定
        Call Me.SetDataOutputCsv(frm, rtDs)

        rtDs = MyBase.CallWSA("LMH030BLF", "SetDsCsvData", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then

            MyBase.ShowMessage(frm)

        Else
            'CSV出力処理
            Call Me.OutFileData(frm, rtDs)

        End If

    End Sub

    ''' <summary>
    ''' CSV作成INデータ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataOutputCsv(ByVal frm As LMH030F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMH030C.TABLE_NM_CSVIN).NewRow()
        Dim kbnDr() As DataRow
        Dim ediCustDrs As DataRow()

        Dim inOutKb As String = String.Empty
        Dim brCd As String = String.Empty
        Dim whCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim prtShubetu As String = String.Empty

        Select Case frm.cmbOutPutCustKb.SelectedValue.ToString()

            Case LMH030C.CSV_SNTK '三徳（日合）

                kbnDr = Me._LMHconG.SelectKbnListDataRow("01", "E017")

                inOutKb = kbnDr(0)("KBN_NM2").ToString()
                brCd = kbnDr(0)("KBN_NM3").ToString()
                whCd = kbnDr(0)("KBN_NM4").ToString()
                custCdL = kbnDr(0)("KBN_NM5").ToString()
                custCdM = kbnDr(0)("KBN_NM6").ToString()
                prtShubetu = kbnDr(0)("KBN_NM7").ToString()

                'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
                ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
                If 0 < ediCustDrs.Length Then
                    dr("NRS_BR_CD") = brCd
                    dr("PRT_SHUBETU") = prtShubetu
                    dr("OUTKA_PLAN_DATE_FROM") = frm.imdOutputDateFrom.TextValue
                    dr("OUTKA_PLAN_DATE_TO") = frm.imdOutputDateTo.TextValue
                    dr("EDI_CUST_INDEX") = ediCustDrs(0)("EDI_CUST_INDEX")
                Else
                    MyBase.SetMessage("E361")
                    Exit Sub
                End If

                ds.Tables(LMH030C.TABLE_NM_CSVIN).Rows.Add(dr)

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 受信帳票,受信一覧表,出荷伝票作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Function OutputPrt(ByVal frm As LMH030F, ByVal rtDs As DataSet) As DataSet

        ''DataSet設定
        'Dim rtDs As DataSet = New LMH030DS()

        '受信帳票INデータ設定
        '受信一覧表INデータ設定
        '出荷伝票INデータ設定
        '一括印刷INデータ設定
        'EDI出荷取消チェックリストINデータ設定
        Call Me.SetDataOutputPrt(frm, rtDs)

        rtDs = MyBase.CallWSA("LMH030BLF", "SetDsPrtData", rtDs)

        Return rtDs

    End Function

    ''' <summary>
    ''' 受信帳票,受信一覧表,出荷伝票INデータ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataOutputPrt(ByVal frm As LMH030F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMH030C.TABLE_NM_OUTPUTIN).NewRow()
        Dim ediCustDrs As DataRow()

        Dim inOutKb As String = "0"                                     '入出荷区分("0"(出荷))
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim whCd As String = frm.cmbWare.SelectedValue.ToString()       '倉庫コード
        Dim custCdL As String = frm.txtPrt_CustCD_L.TextValue.ToString() '荷主コード(大)
        Dim custCdM As String = frm.txtPrt_CustCD_M.TextValue.ToString() '荷主コード(中)
        Dim outputKb As String = frm.cmbOutputKb.SelectedValue.ToString() '出力区分
        Dim prtShubetu As String = String.Empty

        Select Case frm.cmbOutput.SelectedValue.ToString()

            '受信帳票,受信一覧表,出荷伝票,一括印刷,EDI出荷取消チェックリスト
            '要望番号1007 2012.05.08 追加START
            '(2012.12.17)EDI納品書_BP,EDI納品書_BPオートバックス(埼玉),EDI納品書_BPタクティー(埼玉),EDI納品書_BPイエローハット(埼玉) 追加
            '(2012.12.17)EDI納品書_日興イエローハット(大阪),EDI納品書_ロンザ(千葉) 追加
            Case LMH030C.JYUSIN_PRT _
               , LMH030C.JYUSIN_ICHIRAN _
               , LMH030C.OUTKA_PRT _
               , LMH030C.IKKATU_PRT _
               , LMH030C.EDIOUTKA_TORIKESHI_CHECKLIST _
               , LMH030C.NOHIN_OKURIJO _
               , LMH030C.NOHINSYO_AUTO_BAKKUSU _
               , LMH030C.NOHIN_TACTI _
               , LMH030C.NOHIN_YELLOW_HAT _
               , LMH030C.NOHIN_NIKKO _
               , LMH030C.NOHIN_RONZA _
               , LMH030C.SHIKIRI_TERUMO _
               , LMH030C.NIHUDA_TOR _
               , LMH030C.INVOICE_NIPPON_EXPRESS_BP

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
                    dr("INOUT_KB") = "0"
                    dr("RCV_NM_HED") = ediCustDrs(0)("RCV_NM_HED")
                    'dr("RCV_NM_DTL") = ediCustDrs(0)("RCV_NM_DTL")
                    dr("INOUT_UMU_KB") = ediCustDrs(0)("FLAG_16")
                    dr("AKAKURO_KB") = frm.cmbAkakuroKb.SelectedValue.ToString()

                Else
                    MyBase.SetMessage("E361")
                    Exit Sub
                End If

                ds.Tables(LMH030C.TABLE_NM_OUTPUTIN).Rows.Add(dr)
                '2012.09.12 要望番号1429 追加START

            Case Else

        End Select

    End Sub

    '2012.04.18 要望番号1005 追加START
    ''' <summary>
    ''' 受信確認送信
    ''' </summary>
    ''' <remarks></remarks>
    Private Function RcvConfirmSend(ByVal frm As LMH030F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMH030DS()

        '受信確認送信INデータ設定

        Dim dr As DataRow = rtDs.Tables(LMH030C.TABLE_NM_RCVCONF_INFO).NewRow()
        Dim kbnDr() As DataRow
        Dim ediCustDrs As DataRow()

        Dim inOutKb As String = "0"     '出荷("0")
        Dim brCd As String = String.Empty
        Dim whCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim inkaBackupInputDir As String = String.Empty
        Dim outkaBackupInputDir As String = String.Empty
        Dim sendInputDir As String = String.Empty
        Dim WorkInputDir As String = String.Empty
        Dim inkaHokokuDir As String = String.Empty
        Dim outkaHokokuDir As String = String.Empty
        Dim sendFileNm As String = String.Empty
        Dim sendFileExtention As String = String.Empty
        Dim skipStr As String = String.Empty
        Dim custNm As String = String.Empty
        '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo Start 
        Dim sendBackupDir As String = String.Empty
        '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo End 

        '要望番号:2249(浮間千葉対応) 2014/11/14 Shinoda Start 
        'Select Case frm.cmbRcvSendCustkbn.SelectedValue.ToString()

        'Case LMH030C.RCV_SEND_CUST_UKM '浮間合成(大阪)

        Dim nrs_br_cd As String = frm.cmbEigyo.SelectedValue.ToString()

        If nrs_br_cd <> frm.cmbRcvSendCustkbn.SelectedValue.ToString() Then
            '営業所が違うためエラー
            MyBase.SetMessage("E178", New String() {"受信確認送信"})   'エラーメッセージ
            Return rtDs
            Exit Function
        End If

        '区分マスタの(受信送信確認荷主)情報取得(キャッシュ)
        'kbnDr = Me._LMHconG.SelectKbnListDataRow("01", "E026")
        kbnDr = Me._LMHconG.SelectKbnListDataRow(nrs_br_cd, "E026")
        '要望番号:2249(浮間千葉対応) 2014/11/14 Shinoda END 

        If 0 < kbnDr.Length Then

            custNm = kbnDr(0)("KBN_NM1").ToString()
            brCd = kbnDr(0)("KBN_NM2").ToString().Substring(0, 2)
            whCd = kbnDr(0)("KBN_NM2").ToString().Substring(3, 3)
            custCdL = kbnDr(0)("KBN_NM2").ToString().Substring(7, 5)
            custCdM = kbnDr(0)("KBN_NM2").ToString().Substring(13, 2)
            inkaBackupInputDir = kbnDr(0)("KBN_NM3").ToString()
            outkaBackupInputDir = kbnDr(0)("KBN_NM4").ToString()
            sendInputDir = kbnDr(0)("KBN_NM5").ToString()
            'WorkInputDir = String.Concat(outkaBackupInputDir, kbnDr(0)("KBN_NM6").ToString(), "\")
            WorkInputDir = kbnDr(0)("KBN_NM6").ToString()
            inkaHokokuDir = kbnDr(0)("KBN_NM7").ToString()
            outkaHokokuDir = kbnDr(0)("KBN_NM8").ToString()
            sendFileNm = kbnDr(0)("KBN_NM9").ToString()
            skipStr = kbnDr(0)("KBN_NM10").ToString()
            '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo Start 
            sendBackupDir = kbnDr(0)("REM").ToString
            '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo End 

        Else
            MyBase.SetMessage("S001", New String() {"区分値の取得"})   'エラーメッセージ
            Return rtDs
            Exit Function
        End If

        'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
        ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
        If 0 < ediCustDrs.Length Then
            dr("CUST_NM") = custNm
            dr("INKA_BACKUP_INPUT_DIR") = inkaBackupInputDir
            dr("OUTKA_BACKUP_INPUT_DIR") = outkaBackupInputDir
            dr("SEND_INPUT_DIR") = sendInputDir
            dr("WORK_INPUT_DIR") = WorkInputDir
            dr("INKA_HOKOKU_DIR") = inkaHokokuDir
            dr("OUTKA_HOKOKU_DIR") = outkaHokokuDir
            dr("SEND_FILE_NM") = sendFileNm
            dr("SKIP_STR") = skipStr
            dr("EDI_CUST_INDEX") = ediCustDrs(0)("EDI_CUST_INDEX")
            '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo Start 
            dr("SEND_BACKUP_DIR") = sendBackupDir
            '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo End 
        Else
            MyBase.SetMessage("E361")
            Return rtDs
            Exit Function
        End If

        rtDs.Tables(LMH030C.TABLE_NM_RCVCONF_INFO).Rows.Add(dr)

        'Case Else

        'End Select

        Return rtDs

    End Function
    '2012.04.18 要望番号1005 追加END

    ''' <summary>
    ''' ファイル出力処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function OutFileData(ByVal frm As LMH030F, ByVal ds As DataSet) As Boolean

        Dim dt As DataTable
        Dim max As Integer = 0

        Dim setData As StringBuilder = New StringBuilder()
        Dim csvPath As String = String.Empty

        Select Case frm.cmbOutPutCustKb.SelectedValue.ToString()

            Case LMH030C.CSV_SNTK '三徳（日合）

                dt = ds.Tables(LMH030C.TABLE_NM_SNTK_CSVOUT)
                max = dt.Rows.Count - 1

                'データが存在しない場合、スルー
                If max < 0 Then
                    MyBase.ShowMessage(frm, "G001")
                    Return False
                End If

                For i As Integer = 0 To max

                    With dt.Rows(i)
#If False Then ' 日本合成化学対応(2646) 20170116 changed by inoue
                        setData.Append(SetDblQuotation(.Item("UKETSUKENO_EDA").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("IRAI_YMD").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("OUTKA_YMD").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NOUKI_YMD").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NONYU_TIME_NM").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NONYU_CD").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NONYU_ADD").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NONYU_NM_LONG").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("GOODS_NM").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("LOT_APPO_KBN").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("LOT_NO").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("KOSU").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("YORYO").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("UT").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NISUGATA_CD").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("SENPO_ORDER_NO").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("IN_BIKO_BIKO_1").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("IN_BIKO_BIKO_2").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("OUT_BIKO_BIKO_1").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("OUT_BIKO_BIKO_2").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("OUTKA_CTL_NO_CHU").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("TEL").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("GOODS_RYAKU").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("GRADE_1").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("SEISEKI_HAKKO_NM").ToString()))
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("SURYO").ToString()))
                        '▼▼▼要望番号:563,564
                        setData.Append(",")
                        setData.Append(SetDblQuotation(.Item("NONYU_JOHO").ToString()))
                        '▲▲▲要望番号:563,564
#Else
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.UKETSUKENO_EDA)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.IRAI_YMD)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_CD)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_ADD_LINE1)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_ADD_LINE2)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_TEL)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_NM1)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_NM2)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_NM3)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKASAKI_NM4)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUKI_DATE)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYU_JIKOKU_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SYUKKA_DATE)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SHIHARAININ_NM1L)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.JYUCHUSAKI_NM1L)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SENPO_ORDER_NO)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.ITEM_RYAKUGO)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.ITEM_GROUP)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.ITEM_AISYO)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.GRADE1)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.GRADE2)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.YOURYOU)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KOBETSU_NISUGATA_CD)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NISUGATA_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.ITEM_LENGTH)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.ITEM_WIDTH)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.THICKNESS)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.CONCENTRATION)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.PRIOR_DENP_NO)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.OUTKA_DENP_NO)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SEIZO_LOT)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SUURYO)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KOSU)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.WH_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KIHON_SURYO_TANI)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KANSAN_SURYO_KG)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KANSAN_SURYO_TANI_KG)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KANSAN_SURYO_LEN)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.KANSAN_SURYO_TANI_LEN)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SEISEKISYO_HAKKOU_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SHITEI_DENP_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM1)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM2)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM3)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM4)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM5)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM6)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM7)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM8)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM9)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_NM10)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.NOUNYUJI_JYOUKEN_BIKOU)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.YUSO_COMP_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.BIN_KBN_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SOTO_BIKOU)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.UCHI_BIKOU)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.OKURIJYO_HIHYOJI_KBN)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SHIP_NM)))
                        setData.Append(String.Format("{0},", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SHIP_AD)))
                        setData.Append(String.Format("{0}", .Item(LMH030C.SNTK_CSVOUT_COLUMNS.SHIP_TEL)))

#End If

                    End With

                    '改行の設定
                    setData.Append(vbNewLine)
                Next

                Dim kbnDr() As DataRow = Me._LMHconG.SelectKbnListDataRow("01", "E017")
                Dim filePath As String = kbnDr(0)("KBN_NM8").ToString()

                '保存先のCSVファイルのパス
                csvPath = String.Concat(filePath, "\", "三徳様向け出荷指図データ", _
                                        GetSystemDateTime(0).ToString, GetSystemDateTime(1).ToString, _
                                        "_", frm.imdOutputDateFrom.TextValue, "-", frm.imdOutputDateTo.TextValue, ".csv")

            Case Else 'その他

        End Select

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        '開く
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

        '保存したファイルを表示
        System.Diagnostics.Process.Start(csvPath)

        Return True

    End Function

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' プレビュー処理
    ''' </summary>
    ''' <param name="rtnDs"></param>
    ''' <remarks></remarks>
    Private Sub prev(ByVal rtnDs As DataSet)

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

#End Region '印刷
    '▲▲▲要望番号:467

#Region "PopUp"

    ''' <summary>
    ''' マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMH030F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMH030C.EventShubetsu)

        Dim value As String = String.Empty
        ''開始処理
        'Me._LMCconH.StartAction(frm)

        'オブジェクト名による分岐
        Select Case objNM

            Case "txtCustCD_L", "txtCustCD_M"                   '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                'START SHINOHARA 要望番号513
                If eventShubetsu = LMH030C.EventShubetsu.ENTER Then
                    'END SHINOHARA 要望番号513
                    row("CUST_CD_L") = frm.txtCustCD_L.TextValue
                    row("CUST_CD_M") = frm.txtCustCD_M.TextValue
                    'START SHINOHARA 要望番号513
                End If
                'END SHINOHARA 要望番号513
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("HYOJI_KBN") = LMZControlC.HYOJI_S
                'row("SEARCH_CS_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case "txtTodokesakiCd"                              '届先マスタ参照

                'value値の設定
                Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                value = txtCtl.TextValue

                Dim prmDs As DataSet = New LMZ210DS
                Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow
                'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                row("CUST_CD_L") = frm.txtCustCD_L.TextValue
                'START SHINOHARA 要望番号513
                If eventShubetsu = LMH030C.EventShubetsu.ENTER Then
                    'END SHINOHARA 要望番号513
                    row("DEST_CD") = value
                    'START SHINOHARA 要望番号513
                End If
                'END SHINOHARA 要望番号513
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)

            Case "txtEditDestCD"                              'ADD 2018/02/22 一括変更対応　届先マスタ参照

                ''value値の設定
                'Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                'value = txtCtl.TextValue

                Dim prmDs As DataSet = New LMZ210DS
                Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow
                'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                row("CUST_CD_L") = frm.txtCustCD_L.TextValue

                If eventShubetsu = LMH030C.EventShubetsu.ENTER Then
                    row("DEST_CD") = frm.txtEditDestCD.TextValue
                    row("DEST_NM") = frm.lblEditNm.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)


            Case "txtEditMain", "txtEditSub"                     '運送会社マスタ参照

                Dim prmDs As DataSet = New LMZ250DS()
                Dim row As DataRow = prmDs.Tables(LMZ250C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                'START SHINOHARA 要望番号513
                If eventShubetsu = LMH030C.EventShubetsu.ENTER Then
                    'END SHINOHARA 要望番号513
                    row("UNSOCO_CD") = frm.txtEditMain.TextValue
                    row("UNSOCO_BR_CD") = frm.txtEditSub.TextValue
                    'START SHINOHARA 要望番号513
                End If
                'END SHINOHARA 要望番号513
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ250", prm)

        End Select

        '戻り処理
        If prm.ReturnFlg = True Then
            Select Case objNM

                Case "txtCustCD_L", "txtCustCD_M"
                    '荷主マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD_L.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                        frm.txtCustCD_M.TextValue = .Item("CUST_CD_M").ToString()      '荷主コード（大）
                        frm.lblCustNM_L.TextValue = .Item("CUST_NM_L").ToString()      '荷主名(大)
                        frm.lblCustNM_M.TextValue = .Item("CUST_NM_M").ToString()      '荷主名(中)
                    End With

                Case "txtTodokesakiCd"
                    '届先マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                        frm.txtTodokesakiCd.TextValue = .Item("DEST_CD").ToString()     '届先コード
                        frm.lblTodokesakiNM.TextValue = .Item("DEST_NM").ToString()     '届先名

                    End With

                Case "txtEditDestCD"          'ADD 2018/02/22 一括変更
                    '届先マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                        frm.txtEditDestCD.TextValue = .Item("DEST_CD").ToString()       '届先コード
                        frm.lblEditNm.TextValue = .Item("DEST_NM").ToString()           '届先名

                    End With


                Case "txtEditMain", "txtEditSub"
                    '運送会社マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
                        frm.txtEditMain.TextValue = .Item("UNSOCO_CD").ToString()       '運送会社コード
                        frm.txtEditSub.TextValue = .Item("UNSOCO_BR_CD").ToString()     '運送会社支店コード
                        frm.lblEditNm.TextValue = String.Concat(.Item("UNSOCO_NM").ToString(), Space(2), .Item("UNSOCO_BR_NM").ToString())

                    End With

            End Select
        End If

    End Sub

#End Region 'PopUp

    '2013.10.08 追加START DIC SAP 対応
#Region "PGID変換設定"

    ''' <summary>
    ''' PGID変換設定
    ''' </summary>
    ''' <param name="pgid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function changePgid(ByVal pgid As String) As String

        If pgid.Equals("LMH031") = True Then
            pgid = "LMH030"
        End If

        Return pgid

    End Function

#End Region
    '2013.10.08 追加END DIC SAP 対応

#Region "DataSet設定"

#Region "検索時データセット"

    '=== TODO : 遷移先画面完成後、格画面用inputDataSet作成 ==='

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMH030F, ByRef rtDs As DataSet, _
                                      Optional ByVal printShubetsu As Integer = 0, _
                                      Optional ByVal eventShubetsu As Integer = 0) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
        Dim ediCustDrs As DataRow()
        Dim ediCustIndex As String = String.Empty
        Dim ediRcvHed As String = String.Empty
        Dim ediSend As String = String.Empty
        '▼▼▼二次
        Dim ediRcvDtl As String = String.Empty
        Dim inOutFlg As String = String.Empty
        '▲▲▲二次

        '2012.03.25 大阪対応START
        Dim unsoFlg As String = String.Empty
        '2012.03.25 大阪対応END

        '検索条件　単項目部
        dr("EDIOUTKA_STATE_KB1") = frm.chkStaMitouroku.GetBinaryValue.ToString().Replace("0", "")
        dr("EDIOUTKA_STATE_KB2") = frm.chkStaTourokuzumi.GetBinaryValue.ToString().Replace("0", "")
        dr("EDIOUTKA_STATE_KB3") = frm.chkStaJissekimi.GetBinaryValue.ToString().Replace("0", "")
        dr("EDIOUTKA_STATE_KB4") = frm.chkStaJissekiSakusei.GetBinaryValue.ToString().Replace("0", "")
        dr("EDIOUTKA_STATE_KB5") = frm.chkStaJissekiSousin.GetBinaryValue.ToString().Replace("0", "")
        dr("EDIOUTKA_STATE_KB6") = frm.chkstaRedData.GetBinaryValue.ToString().Replace("0", "")
        dr("EDIOUTKA_STATE_KB8") = frm.chkStaTorikesi.GetBinaryValue.ToString().Replace("0", "")
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbWare.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim()
        dr("CUST_CD_M") = frm.txtCustCD_M.TextValue.Trim()
        dr("TANTO_CD") = frm.txtTantouCd.TextValue.Trim()
        dr("DEST_CD") = frm.txtTodokesakiCd.TextValue.Trim()
        dr("EDI_DATE_FROM") = frm.imdEdiDateFrom.TextValue
        dr("EDI_DATE_TO") = frm.imdEdiDateTo.TextValue
        dr("SEARCH_DATE_KBN") = frm.cmbSelectDate.SelectedValue
        dr("SEARCH_DATE_FROM") = frm.imdSearchDateFrom.TextValue
        dr("SEARCH_DATE_TO") = frm.imdSearchDateTo.TextValue
        '(2013.02.13)要望番号1851 BP納品書検索対応 -- START --
        dr("BP_PRT_FLG") = frm.cmbNohinPRT.SelectedValue
        '(2013.02.13)要望番号1851 BP納品書検索対応 --  END  --

        '2013.09.20 追加START
        If frm.chkHitachiSap.Checked = True Then
            dr("HITACHI_SAP_FLG") = "1"
        Else
            dr("HITACHI_SAP_FLG") = "0"
        End If
        '2013.09.20 追加END

        '検索条件　入力部（スプレッド）
        With frm.sprEdiList.ActiveSheet

            dr("JYOTAI_KB") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.STATUS_KBN.ColNo)).Trim()
            dr("HORYU_KB") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.HORYU_KBN.ColNo)).Trim()
            dr("CUST_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.ORDER_NO.ColNo)).Trim()
            dr("CUST_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.CUST_NM.ColNo)).Trim()
            dr("DEST_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.DEST_NM.ColNo)).Trim()
            dr("REMARK") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.REMARK.ColNo)).Trim()
            dr("UNSO_ATT") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.UNSO_ATT.ColNo)).Trim()
            '(2013.01.11)要望番号1700 -- START --
            'dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.GOODS_NM.ColNo)).Trim()
            'dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.GOODS_NM.ColNo)).Trim().Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
            dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.GOODS_NM.ColNo)).Trim().Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")   '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
            '(2013.01.11)要望番号1700 --  END  --
            dr("DEST_AD") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.DEST_AD.ColNo)).Trim()
            dr("UNSO_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.UNSO_CORP.ColNo)).Trim()
            dr("BIN_KB") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.BIN.ColNo))
            dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.EDI_NO.ColNo)).Trim()
            dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.KANRI_NO.ColNo)).Trim()
            dr("MATOME_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.MATOME_NO.ColNo)).Trim()
            '2012.03.26 大阪対応START
            dr("UNSO_NO_L") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.UNSO_NO_L.ColNo)).Trim()
            '2012.03.26 大阪対応END
            '2012.11.11 センコー対応START
            dr("TRIP_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.TRIP_NO.ColNo)).Trim()
            '2012.11.11 センコー対応END
            dr("BUYER_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.BUYER_ORDER_NO.ColNo)).Trim()
            dr("SYUBETU_KB") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.OUTKA_SHUBETSU.ColNo))
            dr("PICK_KB") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.PICK_KB.ColNo))
            dr("UNSO_MOTO_KB") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.UNSOMOTO_KBN.ColNo))
            dr("TANTO_USER") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.TANTO_USER_NM.ColNo)).Trim()
            dr("SYS_ENT_USER") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.SYS_ENT_USER_NM.ColNo)).Trim()
            dr("SYS_UPD_USER") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.SYS_UPD_USER_NM.ColNo)).Trim()

            '区分マスタ参照項目値判定
            If dr("SYUBETU_KB").ToString().Length() < 2 Then
                dr("SYUBETU_KB") = String.Empty
            End If

            If dr("BIN_KB").ToString().Length() < 2 Then
                dr("BIN_KB") = String.Empty
            End If

            If dr("PICK_KB").ToString().Length() < 2 Then
                dr("PICK_KB") = String.Empty
            End If

            If dr("UNSO_MOTO_KB").ToString().Length() < 2 Then
                dr("UNSO_MOTO_KB") = String.Empty
            End If

            dr("FILE_NAME") = Me._LMHconV.GetCellValue(.Cells(0, LMH030G.sprEdiListDef.EDI_FILE_NAME.ColNo)).Trim()

        End With

        'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
        ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow("0", frm.cmbEigyo.SelectedValue.ToString(), _
                                             frm.cmbWare.SelectedValue.ToString(), frm.txtCustCD_L.TextValue, frm.txtCustCD_M.TextValue)
        If 0 < ediCustDrs.Length Then
            ediCustIndex = ediCustDrs(0).Item("EDI_CUST_INDEX").ToString()
            ediRcvHed = ediCustDrs(0).Item("RCV_NM_HED").ToString()
            ediSend = ediCustDrs(0).Item("SND_NM").ToString()

            '2012.02.25 大阪対応 START
            inOutFlg = ediCustDrs(0).Item("FLAG_16").ToString()
            '2012.02.25 大阪対応 END

            '2012.03.25 大阪暫定対応START
            'キャッシュが修正されたら↓のコメントをはずす
            unsoFlg = ediCustDrs(0).Item("FLAG_14").ToString()
            '2012.03.25 大阪暫定対応END

        Else
            Return False
        End If

        dr("EDI_CUST_INDEX") = ediCustIndex
        dr("RCV_NM_HED") = ediRcvHed
        dr("SND_NM") = ediSend
        dr("EDI_CUST_INOUTFLG") = inOutFlg

        '2012.03.25 大阪暫定対応START
        'If ediCustIndex.Equals("42") = True Then
        '    dr("EDI_CUST_UNSOFLG") = "1"
        'End If
        'キャッシュが修正されたら↓のコメントと切り替え
        dr("EDI_CUST_UNSOFLG") = unsoFlg
        '2012.03.25 大阪暫定対応END

        '処理種別
        dr("EVENT_SHUBETSU") = eventShubetsu
        '印刷種別
        dr("PRINT_SHUBETSU") = printShubetsu

        '検索条件をデータセットに設定
        rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

        '再検索用データセットに保存
        Me._FindDs = rtDs

        Return True

    End Function

#End Region

    '2012.06.20 追加START
#Region "LMH060(荷主コード設定画面遷移時)データセット"

    ''' <summary>
    ''' データセット設定(LMH060引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMH060InData(ByVal frm As LMH030F, ByVal prmDs As DataSet, ByVal pgId As String) As DataSet

        ''DataSet設定
        Dim ds As DataSet = New LMH060DS()
        Dim dr As DataRow = ds.Tables(LMH060C.TABLE_NM_IN).NewRow()

        dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()         '営業所コード
        dr.Item("CUST_CD_L") = frm.txtCustCD_L.TextValue                     '荷主コード(大)
        dr.Item("CUST_CD_M") = frm.txtCustCD_M.TextValue                     '荷主コード(中)

        ds.Tables(LMH060C.TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function

#End Region
    '2012.06.20 追加END

#Region "LMH070(紐付け画面遷移時)データセット"

    ''' <summary>
    ''' データセット設定(LMH070引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMH070InData(ByVal frm As LMH030F, ByVal prmDs As DataSet, ByVal pgId As String) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMH070DS()
        Dim dr As DataRow = ds.Tables(LMH070C.TABLE_NM_IN).NewRow()

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim selectRow As Integer = Convert.ToInt32(chkList(0))

        With frm.sprEdiList.Sheets(0)
            dr.Item("EDI_CTL_NO") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.EDI_NO.ColNo))                       'EDI管理番号
            dr.Item("NRS_BR_CD") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))                     '営業所コード
            dr.Item("WH_CD") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo))                            'EDI管理番号
            dr.Item("INOUT_KB") = "0"                                                                                                                                     '入出荷区分
            dr.Item("CUST_CD_L") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))                     '荷主コード(大)
            dr.Item("CUST_CD_M") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))                        '荷主コード(中)
            dr.Item("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))           '荷主INDEX番号
            dr.Item("EVENT_SHUBETSU") = DirectCast(LMH030C.EventShubetsu.HIMODUKE, Integer)

        End With

        ds.Tables(LMH070C.TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function

#End Region

#Region "LMH050(ワーニング画面)データセット + 呼出処理"
    Private Sub CallWarning(ByVal ds As DataSet, ByVal frm As LMH030F, ByVal eventshubetsu As Integer)

        Dim drW As DataRow = ds.Tables(LMH030C.TABLE_NM_WARNING_HED).NewRow()
        Dim drIN As DataRow = ds.Tables(LMH030C.TABLE_NM_IN).Rows(0)

        Dim prm As LMFormData = New LMFormData

        With frm.sprEdiList.ActiveSheet

            '2012.03.25 大阪対応START
            '2011.09.20 修正START
            Select Case eventshubetsu
                Case LMH030C.EventShubetsu.SAVEOUTKA
                    drW.Item("SYORI_KB") = LMH050C.SHORI_OUTKA_TOROKU '出荷登録

                Case LMH030C.EventShubetsu.CREATEJISSEKI
                    drW.Item("SYORI_KB") = LMH050C.SHORI_OUTKA_JISSEKI '実績作成(出荷)

                Case LMH030C.EventShubetsu.SAVEUNSO
                    drW.Item("SYORI_KB") = LMH050C.SHORI_UNSO_TOROKU '運送登録

                Case Else

            End Select
            '2011.09.20 修正END
            '2012.03.25 大阪対応END

            drW.Item("NRS_BR_CD") = drIN("NRS_BR_CD")
            drW.Item("WH_CD") = drIN("WH_CD")
            drW.Item("CUST_CD_L") = drIN("CUST_CD_L")
            drW.Item("CUST_CD_M") = drIN("CUST_CD_M")

        End With

        ds.Tables(LMH030C.TABLE_NM_WARNING_HED).Rows.Add(drW)

        prm.ParamDataSet = ds
        LMFormNavigate.NextFormNavigate(Me, "LMH050", prm)

    End Sub

#End Region

#Region "LMH040(編集画面遷移時)データセット"

    ''' <summary>
    ''' データセット設定(LMH040引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMH040InData(ByVal frm As LMH030F, ByVal prmDs As DataSet, ByVal sta As String) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMH040DS()
        Dim dr As DataRow = ds.Tables(LMH040C.TABLE_NM_IN).NewRow()

        Select Case sta

            Case LMH030C.LMH040_STA_REF    '参照

            Case LMH030C.LMH040_STA_COPY   'ダブルクリック

                With frm.sprEdiList.Sheets(0)
                    dr.Item("EDI_CTL_NO") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.EDI_NO.ColNo))                       'EDI管理番号
                    dr.Item("NRS_BR_CD") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))                     '営業所コード
                    dr.Item("WH_CD") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo))                             '倉庫コード
                    dr.Item("CUST_CD_L") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))                     '荷主コード（大）
                    dr.Item("CUST_CD_M") = Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))                     '荷主コード（中）
                    dr.Item("MATOME_FLG") = "1"                        'まとめ済F
                    If (Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.MATOME_NO.ColNo)) = String.Empty _
                    OrElse InStr(Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.MATOME_NO.ColNo)).ToString(), "00000000") = 1) _
                    OrElse (Me._LMHconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(.ActiveRowIndex, LMH030G.sprEdiListDef.AUTO_MATOME_FLG.ColNo)).ToString()).Equals("9") = True Then
                        dr.Item("MATOME_FLG") = "0"                         'まとめ済F
                    End If
                    dr.Item("INOUT_KB") = "0"                       '入出荷区分

                End With

        End Select

        ds.Tables(LMH040C.TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function

#End Region

#Region "LMZ010データセット"

    ''' <summary>
    ''' データセット設定(LMZ010引数格納)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMZ010InData(ByVal frm As LMH030F) As DataSet

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

#Region "出荷登録データセット"


#If True Then ' BP運送会社自動設定対応 20161115 added by inoue

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="outkaPlanDate">出荷予定日</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUnsoByWgtAndDestRowBp(ByVal destCd As String _
                                    , ByVal outkaPlanDate As String _
                                    , ByVal mosiokuriKb As String) _
                                As LMH030DS.LMH030OUT_UNSO_BY_WGT_AND_DESTRow

        If (_BpUnsocoCdTable Is Nothing OrElse _
            String.IsNullOrEmpty(destCd) OrElse _
            String.IsNullOrEmpty(outkaPlanDate)) Then

            Return Nothing
        End If



        If (LMH030C.MOSIOKURI_KB_POMPUP.Equals(mosiokuriKb) = False) Then
            ' ポンプアップ以外は空文字を設定する
            mosiokuriKb = ""
        End If

        ' 出荷予定日のセルがCellType.DateTimeCellTypeではないため、ここで/を削除する
        outkaPlanDate = outkaPlanDate.Replace("/", "")

        Return _BpUnsocoCdTable.Where(Function(s) s.DEST_CD.Equals(destCd) AndAlso _
                                     s.OUTKA_PLAN_DATE.Equals(outkaPlanDate) AndAlso _
                                     s.MOSIOKURI_KB.Equals(mosiokuriKb)).FirstOrDefault()

    End Function


#End If

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataOutkaSaveShori(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, _
                                           ByVal errHashTable As Hashtable, ByVal setCount As Integer) As DataSet

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim row As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))


                '2011.12.08 要望番号608 修正 START
                If setCount = 0 Then
                    'LMH030IN
                    dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                    dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                    dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                    dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                    dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                    dr("RCV_SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                    dr("RCV_SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                    dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                    dr("AUTO_MATOME_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.AUTO_MATOME_FLG.ColNo))
                    dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo))
                    dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))
                    dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))
                    dr("ORDER_CHECK_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_CHECK_FLG.ColNo))
                    dr("ROW_NO") = selectRow
                    dr("CUST_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo))       'ADD 2018/05/22 千葉 日立FN対応
                    '▼▼▼二次
                    dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                    dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                    dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                    dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                    '▲▲▲二次
                    '2012.03.20 大阪対応START
                    dr("EDI_CUST_UNSOFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo))
                    '2012.03.20 大阪対応END

                    '2013.10.10 追加START
                    If frm.chkHitachiSap.Checked = True Then
                        dr("HITACHI_SAP_FLG") = "1"
                    Else
                        dr("HITACHI_SAP_FLG") = "0"
                    End If
                    '2013.10.10 追加END

                    '2014/03/31 セミ標準対応 --ST--
                    dr("FLAG_17") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.FLAG_17.ColNo))
                    '2014.03.31 セミ標準対応 --ED--
                    dr("FLAG_19") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.FLAG_19.ColNo))

#If True Then   'ADD 2019/02/18 依頼番号 : 004085   【LMS】古河事業所日立物流_危険品と一般品の運賃請求：オーダー合算を廃止、別オーダーとして請求

                    'If ("55").Equals(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))) = True _
                    If ("30").Equals(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))) = True _
                        AndAlso ("30001").Equals(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))) = True _
                        AndAlso ("00").Equals(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))) = True Then

                        dr("CUST_CD_L2") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))
                        dr("CUST_CD_M2") = "08"

                    Else
                        dr("CUST_CD_L2") = String.Empty
                        dr("CUST_CD_M2") = String.Empty

                    End If

#End If
                    rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                ElseIf setCount = 1 Then
                    rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(i)("AUTO_MATOME_FLG") = "9"
                End If
                '2011.12.08 要望番号608 修正 END

            Next

        End With

        '2011.12.08 要望番号608 修正 START
        If setCount = 0 Then
            dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
            dr("EVENT_SHUBETSU") = eventShubetsu
            rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)
        ElseIf setCount = 1 Then

        End If
        '2011.12.08 要望番号608 修正 END

        Return rtDs

    End Function


#End Region

    ''' <summary>
    ''' 月末営業日設定チェック　ADD 2017/05/19
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ChkDataOutkaGetumatuShori(ByVal frm As LMH030F, ByVal eventShubetsu As Integer, _
                                           ByVal errHashTable As Hashtable) As Boolean


        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim selectRow As Integer = 0
        Dim row As Integer = 0
        Dim sSetNaiyo As String
        Dim sGetEigyoDate As String
        Dim sSql As String = String.Empty

        sSql = "INOUT_KB = '0' AND NRS_BR_CD = '" & frm.cmbEigyo.SelectedValue.ToString() & "' AND CUST_CD_L = '" & frm.txtCustCD_L.TextValue.ToString() & "' AND CUST_CD_M = '" & frm.txtCustCD_M.TextValue.ToString() & " ' " _
                     & " AND WH_CD = '" & frm.cmbWare.SelectedValue.ToString() & "'"
        Dim ediCustDetaDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(sSql)
        'EDI荷主取得
        If ediCustDetaDr.Length > 0 Then
            If CStr(ediCustDetaDr(0).Item("EDI_CUST_INDEX")) = "82" Then
                '大阪　大日本製薬のみ対象

            Else
                Return True

            End If
        Else
            Return True
        End If

        '荷主詳細より取得
        sSql = "NRS_BR_CD = '" & frm.cmbEigyo.SelectedValue.ToString() & "' AND CUST_CD = '" & frm.txtCustCD_L.TextValue.ToString() & frm.txtCustCD_M.TextValue.ToString() & "' " _
                     & " AND SUB_KB = '0V' "
        Dim custDetaiksDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sSql)

        If custDetaiksDr.Length > 0 Then
            sSetNaiyo = CStr(custDetaiksDr(0).Item("SET_NAIYO"))
            sGetEigyoDate = Format(Me.GetGetsumatu2Day(CInt(custDetaiksDr(0).Item("SET_NAIYO"))), "yyyyMMdd")

            Dim sysDate As String = MyBase.GetSystemDateTime()(0)

            '取得した月末対象営業日 <= システム日付のとき警告メッセージ
            If sGetEigyoDate <= sysDate Then
                'チェックされたデータをチェックする

            Else
                Return True
            End If
        Else
            Return True
        End If

        Dim sOUTKO_DATE As String = String.Empty
        Dim OUTKA_PLAN_DATE As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1
                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))

                sOUTKO_DATE = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKO_DATE.ColNo))).ToString("yyyyMMdd")
                OUTKA_PLAN_DATE = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_PLAN_DATE.ColNo))).ToString("yyyyMMdd")

                '月末対象日チェック（同じ月末対象日年月対象）
                If Mid(sGetEigyoDate, 1, 6) = Mid(sOUTKO_DATE, 1, 6) Or _
                    Mid(sGetEigyoDate, 1, 6) = Mid(OUTKA_PLAN_DATE, 1, 6) Then

                    'Me.ShowMessage(frm, "C017", New String() {sSetNaiyo})
                    If MsgBoxResult.Ok = Me.ShowMessage(frm, "W267", New String() {sSetNaiyo}) Then
                        Return True
                    Else
                        Return False
                    End If

                End If

            Next

        End With

        Return True

    End Function


#End Region

#Region "実績作成データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSakusei(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

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

                'LMH030IN
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                '2011.09.16 追加START
                dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))
                '2011.09.16 追加END
                '▼▼▼20011.09.21
                dr("MATOME_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.MATOME_NO.ColNo))
                '▲▲▲20011.09.21
                '2011.09.25 追加START
                dr("AUTO_MATOME_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.AUTO_MATOME_FLG.ColNo))
                '2011.09.25 追加END
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                ''2011.09.28★★ 追加START EDI取消データの実績作成対応
                'dr("SYS_DEL_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_DEL_FLG.ColNo))
                ''2011.09.28★★ 追加END
                '★★★2011.11.16 要望番号542 START
                dr("CUST_ORD_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo))
                '★★★2011.11.16 要望番号542 END

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

                'LMH030_EDI_SND
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).Rows.Add(dr)

                'LMH030_C_OUTKA_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_C_OUTKA_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("OUTKA_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                dr("OUTKA_STATE_KB") = "90"
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo))
                '2011.09.28 追加START 出荷取消データの実績作成対応
                dr("SYS_DEL_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_DEL_KB.ColNo))
                '2011.09.28 追加END

                rtDs.Tables(LMH030C.TABLE_NM_C_OUTKA_L).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績取消データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiTorikesi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

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

                'LMH030INOUT
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("ROW_NO") = selectRow
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "9"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "4"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "EDI取消データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataEdiTorikeshi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByVal errHashTable As Hashtable)

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

                'LMH030INOUT
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("ROW_NO") = selectRow
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                '2013.10.10 追加START
                If frm.chkHitachiSap.Checked = True Then
                    dr("HITACHI_SAP_FLG") = "1"
                Else
                    dr("HITACHI_SAP_FLG") = "0"
                End If
                '2013.10.10 追加END

                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString()
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("DEL_KB") = "1"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "EDI取消⇒未登録データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTorikesiMitouroku(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1


                selectRow = Convert.ToInt32(chkList(i))

                'LMH030IN
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END

                '2013.10.10 追加START
                If frm.chkHitachiSap.Checked = True Then
                    dr("HITACHI_SAP_FLG") = "1"
                Else
                    dr("HITACHI_SAP_FLG") = "0"
                End If
                '2013.10.10 追加END

                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("DEL_KB") = "0"
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_DEL_FLG") = "0"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績作成済⇒実績未データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSakuseiJissekimi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1


                selectRow = Convert.ToInt32(chkList(i))

                'LMH030INOUT
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END
                '2012.11.11 センコー対応 START
                dr("EDI_CUST_UNSOFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo))
                '2012.11.11 センコー対応 END
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "0"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                ' ''2011.09.30★★ 追加START EDI取消データの実績作成⇒実績未対応
                ''dr("SYS_DEL_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_DEL_FLG.ColNo))
                ' ''2011.09.30★★ 追加END

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "0"
                'dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

                'LMH030_C_OUTKA_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_C_OUTKA_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("OUTKA_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                jissekiFlg = Convert.ToInt32(.Cells(Convert.ToInt32(selectRow), LMH030G.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value())
                '2011.10.07 START デュポンEDIデータ即実績作成対応
                'If jissekiFlg = 1 OrElse jissekiFlg = 2 OrElse jissekiFlg = 4 Then
                If jissekiFlg = 1 OrElse jissekiFlg = 2 OrElse jissekiFlg = 3 OrElse jissekiFlg = 4 Then
                    '2011.10.07 END

                    dr("OUTKA_STATE_KB") = "60"
                Else
                    dr("OUTKA_STATE_KB") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_STATE_KB.ColNo))
                End If
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo))
                '2011.09.30 追加START 出荷取消データの実績作成⇒実績未対応
                dr("SYS_DEL_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_DEL_KB.ColNo))
                '2011.09.30 追加END

                rtDs.Tables(LMH030C.TABLE_NM_C_OUTKA_L).Rows.Add(dr)

                'LMH030_EDI_SND
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績送信済⇒送信待データセット"
    ''' <summary>
    ''' 実績送信済⇒送信待データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSousinmi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim OUTKANo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH030INOUT
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "1"


                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

                'LMH030_EDI_SND
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "実績送信済⇒実績未データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSousinJissekimi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1


                selectRow = Convert.ToInt32(chkList(i))

                'LMH030IN
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END
                '2012.11.11 センコー対応 START
                dr("EDI_CUST_UNSOFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo))
                '2012.11.11 センコー対応 END
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "0"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("JISSEKI_FLAG") = "0"
                'dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                'dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "1"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

                'LMH030_C_OUTKA_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_C_OUTKA_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("OUTKA_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                jissekiFlg = Convert.ToInt32(.Cells(Convert.ToInt32(selectRow), LMH030G.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value())
                If jissekiFlg = 1 OrElse jissekiFlg = 2 OrElse jissekiFlg = 4 Then
                    dr("OUTKA_STATE_KB") = "60"
                Else
                    dr("OUTKA_STATE_KB") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_STATE_KB.ColNo))
                End If
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo))
                '2011.09.30 追加START 出荷取消データの実績作成⇒実績未対応
                dr("SYS_DEL_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_DEL_KB.ColNo))
                '2011.09.30 追加END

                rtDs.Tables(LMH030C.TABLE_NM_C_OUTKA_L).Rows.Add(dr)

                'LMH030_EDI_SND
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_SYS_UPD_TIME.ColNo))
                dr("JISSEKI_SHORI_FLG") = "2"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_SND).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

#Region "出荷取消⇒未登録データセット"
    ''' <summary>
    ''' 出荷取消⇒未登録データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTourokumi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim OUTKANo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH030INOUT
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                '2011.09.27 追加START
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_SYS_UPD_TIME.ColNo))
                dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                dr("MATOME_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.MATOME_NO.ColNo))
                dr("AUTO_MATOME_FLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.AUTO_MATOME_FLG.ColNo))
                '2011.09.27 追加END
                '▼▼▼二次
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                '▲▲▲二次
                '2012.02.25 大阪対応 START
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                '2012.02.25 大阪対応 END
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                dr("OUT_FLAG") = "0"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                'dr("OUTKA_CTL_NO_CHU") = String.Empty
                dr("OUT_KB") = "0"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))
                dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("OUTKA_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo))
                'dr("OUTKA_CTL_NO_CHU") = "000"

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub


#End Region

    '2012.04.04 大阪対応 追加START
#Region "運送取消⇒未登録データセット"
    ''' <summary>
    ''' 運送取消⇒未登録データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataUnsoTourokumi(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim jissekiFlg As Integer = 0
        Dim OUTKANo As String = String.Empty

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                'LMH030INOUT
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("ROW_NO") = selectRow
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                '運送(大)の更新日付を取得
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.UNSO_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.UNSO_SYS_UPD_TIME.ColNo))
                dr("UNSO_NO_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.UNSO_NO_L.ColNo))
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                dr("RCV_NM_EXT") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_EXT.ColNo))
                dr("SND_NM") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SND_NM.ColNo))
                dr("EDI_CUST_INOUTFLG") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INOUTFLG.ColNo))
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)

                'LMH030_OUTKAEDI_L
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("OUT_FLAG") = "0"
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_L).Rows.Add(dr)

                'LMH030_OUTKAEDI_M
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("OUT_KB") = "0"

                rtDs.Tables(LMH030C.TABLE_NM_OUTKAEDI_M).Rows.Add(dr)

                'LMH030_RCV_HED
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_SYS_UPD_TIME.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_HED).Rows.Add(dr)

                'LMH030_RCV_DTL
                dr = rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))

                rtDs.Tables(LMH030C.TABLE_NM_EDI_RCV_DTL).Rows.Add(dr)

            Next

        End With

        dr = rtDs.Tables(LMH030C.TABLE_NM_JUDGE).NewRow()
        dr("EVENT_SHUBETSU") = eventShubetsu
        rtDs.Tables(LMH030C.TABLE_NM_JUDGE).Rows.Add(dr)

    End Sub

    '2012.04.04 大阪対応 追加END
#End Region

#Region "一括変更選択行データセット"
    ''' <summary>
    ''' 一括変更選択行データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetDataHenkoKey(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal errHashtable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashtable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))
                dr = rtDs.Tables(LMH030C.TABLE_NM_UPDATE_KEY).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_DATE.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.SYS_UPD_TIME.ColNo))

                'データセットに設定
                rtDs.Tables(LMH030C.TABLE_NM_UPDATE_KEY).Rows.Add(dr)

                'LMH030IN
                dr = rtDs.Tables(LMH030C.TABLE_NM_IN).NewRow()
                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                'ADD Start 2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更
                dr("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim
                'ADD End   2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更
                dr("ROW_NO") = selectRow
                rtDs.Tables(LMH030C.TABLE_NM_IN).Rows.Add(dr)
            Next

        End With

    End Sub
#End Region

#Region "一括変更値データセット"
    ''' <summary>
    ''' 一括変更値データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetDataHenkoValue(ByVal frm As LMH030F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMH030C.TABLE_NM_UPDATE_VALUE).NewRow()
        'キャッシュの値(区分マスタ)
        Dim mKbnDrs As DataRow() = Nothing
        Dim selectCmbValue As String = frm.cmbIkkatuChangeKbn.SelectedValue.ToString
        Dim editNm As String = String.Empty
        Dim editNmSub As String = String.Empty
        Dim editType As Integer = 3
        Dim editTypeSub As Integer = 3
        Dim editContoler As String = String.Empty

        mKbnDrs = Me._LMHconV.SelectKBNListDataRow(selectCmbValue, "E007")
        If 0 < mKbnDrs.Length Then
            editNm = mKbnDrs(0).Item("KBN_NM3").ToString()
            editNmSub = mKbnDrs(0).Item("KBN_NM5").ToString()
            editType = Convert.ToInt32(mKbnDrs(0).Item("KBN_NM4"))
            If mKbnDrs(0).Item("KBN_NM6").ToString().Equals(String.Empty) = False Then
                editTypeSub = Convert.ToInt32(mKbnDrs(0).Item("KBN_NM6"))
            End If
        End If

        editContoler = frm.cmbIkkatuChangeKbn.SelectedValue.ToString
        dr("EDIT_ITEM_KBN") = editContoler
        dr("EDIT_ITEM_NM1") = editNm

        Select Case editContoler

            '便区分
            Case "01"
                dr("EDIT_ITEM_VALUE1") = frm.cmbEditKbn.SelectedValue
                dr("EDIT_ITEM_TYPE1") = editType

                '運送会社コード,運送会社支店コード
            Case "02"
                dr("EDIT_ITEM_VALUE1") = frm.txtEditMain.TextValue
                dr("EDIT_ITEM_TYPE1") = editType
                dr("EDIT_ITEM_NM2") = editNmSub
                dr("EDIT_ITEM_VALUE2") = frm.txtEditSub.TextValue
                dr("EDIT_ITEM_TYPE2") = editTypeSub

                'BP・カストロール対応 terakawa 2012.12.26 Start
                '    '出庫日,出荷予定日,納入予定日
                'Case "03", "04", "05"
                '    dr("EDIT_ITEM_VALUE1") = frm.cmbEditDate.TextValue
                '    dr("EDIT_ITEM_TYPE1") = editType

                '出庫日,出荷予定日
            Case "03", "04"
                dr("EDIT_ITEM_VALUE1") = frm.cmbEditDate.TextValue
                dr("EDIT_ITEM_TYPE1") = editType

                If rtDs.Tables(LMH030C.TABLE_NM_IN).Rows(0).Item("EDI_CUST_INDEX").ToString().Equals(LMH030C.EDI_CUST_INDEX.BP) Then
                    dr("EDIT_ITEM_NM2") = editNmSub
                    dr("EDIT_ITEM_VALUE2") = frm.cmbEditDate.TextValue
                    dr("EDIT_ITEM_TYPE2") = editTypeSub
                End If
                'BP・カストロール対応 terakawa 2012.12.26 End

                '納入予定日
            Case "05"
                dr("EDIT_ITEM_VALUE1") = frm.cmbEditDate.TextValue
                dr("EDIT_ITEM_TYPE1") = editType

                '届先コード  ADD 2018/02/22
            Case "06"
                dr("EDIT_ITEM_VALUE1") = frm.txtEditDestCD.TextValue
                dr("EDIT_ITEM_TYPE1") = editType

                'Dac SetsqlEdit でおかしくなるので（M_DEST取得でセットする）
                'dr("EDIT_ITEM_NM2") = editNmSub
                'dr("EDIT_ITEM_VALUE2") = frm.lblEditNm.TextValue
                'dr("EDIT_ITEM_TYPE2") = editTypeSub

                'ピック区分
            Case "07"
                dr("EDIT_ITEM_VALUE1") = frm.cmbEditKbn2.SelectedValue
                dr("EDIT_ITEM_TYPE1") = editType

        End Select

        'データセットに設定
        rtDs.Tables(LMH030C.TABLE_NM_UPDATE_VALUE).Rows.Add(dr)

    End Sub
#End Region

#Region "進捗区分のデータセット設定"

    ''' <summary>
    ''' 検索時の進捗区分の設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function StatusSet(ByVal dt As DataTable) As DataTable
        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow
        Dim unsokbn As String = String.Empty
        Dim unsoNo As String = String.Empty

        For i As Integer = 0 To max
            dr = dt.Rows(i)

            '2012.03.20 大阪対応START
            If String.IsNullOrEmpty(dr("FREE_C30").ToString()) = False Then
                unsokbn = dr("FREE_C30").ToString().Substring(0, 2)
                If dr("FREE_C30").ToString().Length > 2 Then
                    unsoNo = dr("FREE_C30").ToString().Substring(4, 8)
                End If
            End If
            '2012.03.20 大阪対応END

            Select Case unsokbn

                Case "01"

                    '2017/05/29 日合　運送対応　START
                    If dr("EDI_CUST_INDEX").ToString().Equals("57") = True Then
                        dr("UNSO_DEL_KB") = dr("UNSONCG_DEL_KB").ToString()
                    End If
                    '2017/05/29 日合　運送対応　END

                    If dr("UNSO_DEL_KB").ToString().Equals("1") = True Then
                        dr("OUTKA_STATE_KB_NM") = "運送取消"

                    ElseIf unsoNo.Equals("00000000") = True Then
                        dr("OUTKA_STATE_KB_NM") = "運送EDI"

                        '2012.03.20 大阪対応START
                    ElseIf unsoNo.Equals("00000000") = False Then
                        dr("OUTKA_STATE_KB_NM") = "運送登録済"
                        '2012.03.20 大阪対応END

                        '2012.11.13 センコー対応START
                        If String.IsNullOrEmpty(dr("TRIP_NO").ToString()) = False AndAlso _
                           dr("EDI_CUST_JISSEKI").ToString().Equals("1") = True AndAlso _
                           dr("JISSEKI_FLAG").ToString().Equals("0") Then
                            dr("OUTKA_STATE_KB_NM") = "運送実績未"
                        ElseIf dr("JISSEKI_FLAG").ToString().Equals("1") Then
                            dr("OUTKA_STATE_KB_NM") = "運送実績作成済"
                        ElseIf dr("JISSEKI_FLAG").ToString().Equals("2") Then
                            dr("OUTKA_STATE_KB_NM") = "運送実績送信済"
                        End If
                        '2012.11.13 センコー対応END

                    End If

                Case Else

                    If dr("OUTKA_DEL_KB").ToString().Equals("1") Then
                        '↓FFEM特殊処理↓
                        '2014.06.09 追加START
                        If dr("JISSEKI_FLAG").ToString().Equals("1") Then
                            dr("OUTKA_STATE_KB_NM") = "作済/出荷取消"
                        ElseIf dr("JISSEKI_FLAG").ToString().Equals("2") Then
                            dr("OUTKA_STATE_KB_NM") = "送済/出荷取消"
                        Else
                            dr("OUTKA_STATE_KB_NM") = "出荷取消"
                        End If
                        '↑FFEM特殊処理↑
                        '2014.06.09 追加END
                    ElseIf dr("JISSEKI_FLAG").ToString().Equals("1") Then
                        dr("OUTKA_STATE_KB_NM") = "実績作成済"
                    ElseIf dr("JISSEKI_FLAG").ToString().Equals("2") Then
                        dr("OUTKA_STATE_KB_NM") = "実績送信済"

                    ElseIf String.IsNullOrEmpty(dr("OUTKA_STATE_KB_NM").ToString()) = True Then
                        If dr("SYS_DEL_FLG").ToString().Equals("1") Then
                            dr("OUTKA_STATE_KB_NM") = "EDI取消"
                            '2013.10.23 追加START
                            If dr("EDI_DEL_KB").ToString().Equals("2") Then
                                dr("OUTKA_STATE_KB_NM") = "キャンセル"
                            End If
                            '2013.10.23 追加END
                        Else

                            If dr("OUT_FLAG").ToString().Equals("2") = True Then
                                If dr("JISSEKI_FLAG").ToString().Equals("9") = True Then
                                    dr("OUTKA_STATE_KB_NM") = "実績対象外"
                                Else
                                    dr("OUTKA_STATE_KB_NM") = "実績未"
                                End If
                            Else
                                dr("OUTKA_STATE_KB_NM") = "EDI"
                            End If
                        End If

                    End If

            End Select

        Next

        Return dt

    End Function

#End Region

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMH030_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LMH030_GUIERROR").Rows(i)

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

    '取込対応 20120306 Start
#Region "EDI取込ヘッダーデータセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiHed(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dr As DataRow
        Dim fileCount As Integer = 0
        Dim sysDatetime As String = String.Empty
        Dim extention As String = String.Empty

        Dim ope_File_Name As String = String.Empty
        Dim backup_File_Name As String = String.Empty

        'システム時間を取得
        sysDatetime = String.Concat(MyBase.GetSystemDateTime()(0), MyBase.GetSystemDateTime()(1)).Remove(14)

        '受信格納フォルダのファイル数だけデータセットを作成
        For Each stFilePath As String In System.IO.Directory.GetFiles(drSemiEdiInfo.Item("RCV_INPUT_DIR").ToString(), String.Concat("*.", drSemiEdiInfo.Item("RCV_FILE_EXTENTION").ToString()))
            extention = System.IO.Path.GetExtension(stFilePath)
            If String.Compare(extention, String.Concat(".", drSemiEdiInfo.Item("RCV_FILE_EXTENTION").ToString()), True) = 0 Then
                'ファイル数カウント
                fileCount += 1

                '作業用ファイル名の作成
                ope_File_Name = String.Concat(System.IO.Path.GetFileNameWithoutExtension(stFilePath), "_", sysDatetime, _
                                              "_", String.Format("{0:D3}", fileCount), System.IO.Path.GetExtension(stFilePath))

                ''バックアップ用ファイル名
                'backup_File_Name = String.Concat("RCV_", sysDatetime, "_", String.Format("{0:D3}", fileCount), System.IO.Path.GetExtension(stFilePath))

                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED).NewRow()
                dr("FILE_NAME_RCV") = Path.GetFileName(stFilePath)
                dr("FILE_NAME_OPE") = ope_File_Name
                dr("FILE_NAME_BAK") = ope_File_Name
                dr("ERR_FLG") = "9"

                rtDs.Tables(LMH030C.EDI_TORIKOMI_HED).Rows.Add(dr)
            End If
        Next stFilePath

        Return rtDs

    End Function

#End Region

#Region "EDI取込詳細データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosai(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty

        'EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1


            ' StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(String.Concat(drSemiEdiInfo.Item("WORK_INPUT_DIR"), dtHed.Rows(i).Item("FILE_NAME_OPE")), System.Text.Encoding.Default)

            '先頭行飛ばしカウントの数だけ行を読み込む
            For j As Integer = 0 To Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT")) - 1
                cReader.ReadLine()
            Next

            ' 読み込みできる文字がなくなるまで繰り返す
            While (cReader.Peek() >= 0)

                ''先頭行飛ばしカウントの数だけ行を読み込む
                'If gyoCount = 0 Then
                '    For j As Integer = 0 To Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT")) - 1
                '        cReader.ReadLine()
                '    Next
                'End If

                ' ファイルを 1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                gyoCount += 1

                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("FILE_NAME_OPE") = dtHed.Rows(i).Item("FILE_NAME_OPE")
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

                    dr(String.Concat("COLUMN_", columnCount.ToString)) = aryf
                    columnCount += 1
                Next

                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            cReader.Close()

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            '行カウントをリセット
            gyoCount = 0
        Next

        Return rtDs

    End Function

#End Region
    '取込対応 20120306 End

    '取込 固定長対応 2012/08/06 本明 Start 
#Region "EDI取込詳細データセット(固定長)篠崎運送対応"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiFixedLength(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer) As DataSet

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty

        '固定長のサイズを取得
        Dim iLength As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT"))
        'ファイルを読み込むバイト型配列を作成する
        'Dim cs(iLength - 1) As Char
        Dim bs(iLength - 1) As Char

        'EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1


            ' StreamReader の新しいインスタンスを生成する
            Dim cReader As New System.IO.StreamReader(String.Concat(drSemiEdiInfo.Item("WORK_INPUT_DIR"), dtHed.Rows(i).Item("FILE_NAME_OPE")), System.Text.Encoding.Default)

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
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("FILE_NAME_OPE") = dtHed.Rows(i).Item("FILE_NAME_OPE")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"
                dr("COLUMN_1") = stBuffer

                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            cReader.Close()

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            '行カウントをリセット
            gyoCount = 0
        Next

        Return rtDs

    End Function
#End Region
    '取込 固定長対応 2012/08/06 本明 End 

    '取込 千葉対応 2012/09/03 本明 Start 
#Region "EDI取込詳細データセット(EXCEL対応)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiExcel(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer) As DataSet


        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty

        Dim folderNm As String = drSemiEdiInfo.Item("WORK_INPUT_DIR").ToString                      'フォルダ名
        Dim rowNoMin As Integer = Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT").ToString) + 1   '行の開始数
        Dim colNoMax As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT").ToString)  '列の最大数
        'Dim rowNoKey As Integer = Convert.ToInt32(drSemiEdiInfo.Item("ROW_KEY_NO").ToString)        '最大行取得用RowNo
        Dim rowNoKey As Integer = 1        'Cashに登録されるまで、とりあえず１列目を設定

        Dim fileNm As String = String.Empty

        '-----------------------------------------------------------------------------------------------
        ' EXCELファイル用
        '-----------------------------------------------------------------------------------------------
        Dim xlApp As Excel.Application = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim xlCell As Excel.Range = Nothing

        xlApp = New Excel.Application()
        'xlApp = CreateObject("Excel.Application")

        xlBooks = xlApp.Workbooks


        'EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1

            ' EXCEL OPEN
            fileNm = dtHed.Rows(i).Item("FILE_NAME_OPE").ToString
            xlBook = xlBooks.Open(String.Concat(folderNm, fileNm))

            'シート
            'xlSheet = DirectCast(xlBook.Worksheets(drSemiEdiInfo.Item("SHEET_NM").ToString), Excel.Worksheet)

            ''If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "70" Then
            ''    '動作不安定の為コメント
            ''    'xlSheet = DirectCast(xlBook.Worksheets("輸送計画一覧表"), Excel.Worksheet)  'Cashに登録されるまで、固定値設定
            ''    'JT物流の場合EXCELシート2枚目を読み取る
            ''    xlSheet = DirectCast(xlBook.Worksheets(2), Excel.Worksheet)                 'Cashに登録されるまで、１番目のシートを設定
            ''Else
            ''    'その他の場合EXCELシート1枚目を読み取る
            ''    xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)                 'Cashに登録されるまで、１番目のシートを設定
            ''End If
            xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)                 'とりあえず１番目のシートを設定

            xlApp.Visible = False

            '最大行を取得(rowNoKey列の最終入力行を取得)
            Dim rowNoMax As Integer = 0
            'rowNoMax = xlApp.Rows.Count

            'Dim rowNoMax As Integer = DirectCast(xlCell(xlCell.Rows.Count, rowNoKey), Excel.Range).End(Excel.XlDirection.xlUp).Row
            xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

            rowNoMax = xlApp.ActiveCell.Row

            '要望番号1593:(【セミEDI】JT物流　EXCEL取込時 高速化) 2012/11/14 本明 Start
            '' 最大行まで繰り返す
            'For j As Integer = rowNoMin To rowNoMax

            '    'データセットに登録

            '    'Key列が空の場合は空行とみなしデータセットに登録しない
            '    xlCell = DirectCast(xlSheet.Cells(j, rowNoKey), Excel.Range)
            '    If String.IsNullOrEmpty(Convert.ToString(xlCell.Value)) Then
            '        Continue For
            '    End If

            '    gyoCount += 1
            '    dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
            '    dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
            '    dr("FILE_NAME_OPE") = dtHed.Rows(i).Item("FILE_NAME_OPE")
            '    dr("REC_NO") = gyoCount
            '    dr("ERR_FLG") = "9"

            '    '列を取得
            '    For k As Integer = 1 To colNoMax
            '        xlCell = DirectCast(xlSheet.Cells(j, k), Excel.Range)
            '        dr(String.Concat("COLUMN_", k.ToString)) = Convert.ToString(xlCell.Value)
            '    Next

            '    'DSにAdd
            '    rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            'Next

            '２次元配列に取得する
            Dim arrData(,) As Object
            arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

            '(2013.02.21) START ロンザの場合、数値が入っていない場合は行カウントしない
            If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString = "84" Then
                rowNoKey = 10
            End If
            '(2013.02.21) END ロンザの場合、数値が入っていない場合は行カウントしない

            '２次元→DSにセットする
            For j As Integer = rowNoMin To rowNoMax

                'データセットに登録
                'Key列が空の場合は空行とみなしデータセットに登録しない
                '要望番号1845 2013.02.08 修正START
                '(ロンザの場合キー項目(DELIVERY_NO)が空白でも取込を行う)
                '(2013.02.21) START ロンザは商品数量をキーに替えたのでコメント排除
                'If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString <> "84" Then
                If arrData(j, rowNoKey) Is Nothing Then

                    Continue For
                Else
                    If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                        Continue For
                    End If
                End If
                'End If
                '要望番号1845 2013.02.08 修正END
                '(2013.02.21) END ロンザは商品数量をキーに替えたのでコメント排除

                gyoCount += 1
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("FILE_NAME_OPE") = dtHed.Rows(i).Item("FILE_NAME_OPE")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"

                'DSに格納
                For k As Integer = 1 To colNoMax
                    If arrData(j, k) Is Nothing Then
                        dr(String.Concat("COLUMN_", k.ToString)) = String.Empty
                    Else
                        dr(String.Concat("COLUMN_", k.ToString)) = arrData(j, k).ToString
                    End If
                Next

                'DSにAdd
                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)
            Next
            '要望番号1593:(【セミEDI】JT物流　EXCEL取込時 高速化) 2012/11/14 本明 End

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            '行カウントをリセット
            gyoCount = 0

            'EXCEL CLOSE
            '要望番号1593:(【セミEDI】JT物流　EXCEL取込時 高速化) 2012/11/14 本明 Start
            'System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
            'xlCell = Nothing
            If xlCell IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
                xlCell = Nothing
            End If
            '要望番号1593:(【セミEDI】JT物流　EXCEL取込時 高速化) 2012/11/14 本明 End

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

        Next

        Return rtDs

    End Function
#End Region
    '取込 千葉対応 2012/09/03 本明 End 

    '要望番号1061 2012.05.15 追加START
#Region "CSV作成・出力(出力済)データセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataOutputZumi(ByVal frm As LMH030F, ByVal rtDs As DataSet) As DataSet

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim row As Integer = 0
        '2013.03.12(BP新システム対応START) 修正START
        Dim preOrderNo As String = String.Empty
        '2013.03.12(BP新システム対応END) 修正END

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                '2013.03.12 修正START
#If False Then ' BP運送会社自動設定対応 20170110 changed by inoue
                If i = 0 OrElse _
                   preOrderNo <> Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo)) Then
#Else
                If (i = 0 OrElse _
                    LMH030C.INVOICE_NIPPON_EXPRESS_BP.Equals(frm.cmbOutput.SelectedValue) OrElse _
                    preOrderNo <> Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo))) Then
#End If
                    'LMH030OUTPUTIN
                    dr = rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).NewRow()
                    dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                    dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo))
                    dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))
                    dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))
                    dr("OUTPUT_SHUBETU") = frm.cmbOutput.SelectedValue.ToString()
                    '要望番号:1446 terakawa 2012.09.19 Start
                    'Dim freeC30 As String = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo))
                    'EDI出荷取消チェックリストかつ、FREEC_30の頭3文字が"04-"(まとめデータ)の場合、FREE_C30のEDI管理番号をセット
                    'If dr("OUTPUT_SHUBETU").ToString().Equals(LMH030C.EDIOUTKA_TORIKESHI_CHECKLIST) AndAlso _
                    '   Mid(freeC30, 1, 3).Equals("04-") = True Then
                    'dr("EDI_CTL_NO") = Mid(freeC30, 4, 9)
                    'Else
                    dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo))
                    'End If
                    '要望番号:1446 terakawa 2012.09.19 End
                    '2012.05.29 要望番号1077 追加START
                    dr("ORDER_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo))
                    '2012.05.29 要望番号1077 追加END
                    dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                    dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                    dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))
                    dr("PRTFLG") = "1"
                    dr("INOUT_KB") = "0"
                    dr("ROW_NO") = selectRow
                    '要望番号:1446 terakawa 2012.09.19 Start
                    dr("DEL_KB") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo))
                    '要望番号:1446 terakawa 2012.09.19 End

                    dr("YAKUJO_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.BUYER_ORDER_NO.ColNo))
                    dr("AKAKURO_KB") = frm.cmbAkakuroKb.SelectedValue.ToString()

                    preOrderNo = String.Empty
                    preOrderNo = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo))

                Else
                    preOrderNo = String.Empty
                    preOrderNo = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH030G.sprEdiListDef.ORDER_NO.ColNo))
                    Continue For
                End If
                '2013.03.12 修正END
                rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).Rows.Add(dr)

            Next

        End With

        '2013.03.12 コメントSTART
        'Dim dv As DataView = rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).DefaultView
        'Dim resultDt As DataTable

        'resultDt = dv.ToTable(LMH030C.TABLE_NM_OUTPUTIN, True, "NRS_BR_CD", "WH_CD", "CUST_CD_L", "CUST_CD_M", "OUTPUT_SHUBETU", "ORDER_NO", "EDI_CUST_INDEX", "RCV_NM_HED", "RCV_NM_DTL", "PRTFLG", "INOUT_KB", "DEL_KB")

        'rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).Clear()
        'rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).Merge(resultDt)

        '2013.03.12 コメントEND

        rtDs = MyBase.CallWSA("LMH030BLF", "SetDsPrtData", rtDs)

        Return rtDs

    End Function

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataOutputSelectedRows(ByVal frm As LMH030F _
                                             , ByVal rtDs As DataSet) As DataSet

        Dim chkList As ArrayList = Me._V.getCheckList()

        Dim dr As DataRow = Nothing
        Dim selectRowIndex As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To chkList.Count() - 1

                selectRowIndex = Convert.ToInt32(chkList(i))

                'LMH030OUTPUTIN
                dr = rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).NewRow()
                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("WH_CD") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.NRS_WH_CD.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.CUST_CD_M.ColNo))
                dr("OUTPUT_SHUBETU") = frm.cmbOutput.SelectedValue.ToString()


                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.EDI_NO.ColNo))
#If False Then ' 日本合成化学対応(2646) 20170116 added inoue
                dr("ORDER_NO") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.ORDER_NO.ColNo))
#Else
                dr("ORDER_NO") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.FREE_C05.ColNo))
#End If

                dr("EDI_CUST_INDEX") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo))
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.RCV_NM_HED.ColNo))
                dr("RCV_NM_DTL") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.RCV_NM_DTL.ColNo))

                dr("CRT_DATE_FROM") = ""
                dr("CRT_DATE_TO") = ""

                ' 印刷フラグ
                If (LMH030C.OUTPUT_KB.PENDING_OUTPUT _
                        .Equals(frm.cmbOutputKb.SelectedValue)) Then
                    dr("PRTFLG") = LMConst.FLG.OFF

                ElseIf (LMH030C.OUTPUT_KB.OUTPUTTED _
                        .Equals(frm.cmbOutputKb.SelectedValue)) Then
                    dr("PRTFLG") = LMConst.FLG.ON
                Else
                    dr("PRTFLG") = String.Empty
                End If

                dr("INOUT_KB") = "0"
                dr("ROW_NO") = selectRowIndex
                dr("DEL_KB") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo))

                dr("YAKUJO_NO") = Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.BUYER_ORDER_NO.ColNo))
                dr("AKAKURO_KB") = frm.cmbAkakuroKb.SelectedValue.ToString()


                If (LMH030C.NICHIGO_YUSO_COMP_CD.NRS.Equals(Me._LMHconV.GetCellValue(.Cells(selectRowIndex, LMH030G.sprEdiListDef.FREE_C07.ColNo)))) Then
                    ' 日陸手配
                    dr("UNSO_TEHAI_KB") = LMH030C.UNSO_TEHAI_KB.NRS
                Else
                    ' 先方手配
                    dr("UNSO_TEHAI_KB") = LMH030C.UNSO_TEHAI_KB.OTHER_PARTY
                End If

                rtDs.Tables(LMH030C.TABLE_NM_OUTPUTIN).Rows.Add(dr)

            Next

        End With

        rtDs = MyBase.CallWSA("LMH030BLF", "SetDsPrtData", rtDs)

        Return rtDs

    End Function
#End If





#End Region
    '要望番号1061 2012.05.15 追加END

    '2012.04.18 要望番号1005 追加START
#Region "受信確認送信対象CSVセット"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SerchEdiCrtCsv(ByVal rtDs As DataSet) As Boolean

        Dim drRcvConfInfo As DataRow = rtDs.Tables(LMH030C.TABLE_NM_RCVCONF_INFO).Rows(0)

        Dim fileCount As Integer = 0                            '入荷・出荷ファイル件数
        Dim fileAllcnt As Integer = 0                           '入出荷総ファイル件数
        Dim extention As String = String.Empty

        Dim backupInputDir As String = drRcvConfInfo.Item("INKA_BACKUP_INPUT_DIR").ToString()       'BACKUP格納フォルダパス
        Dim workInputDir As String = drRcvConfInfo.Item("WORK_INPUT_DIR").ToString()                '作業用フォルダパス
        Dim houkokuInputDir As String = drRcvConfInfo.Item("INKA_HOKOKU_DIR").ToString()            '報告済格納フォルダパス
        Dim sendInputDir As String = drRcvConfInfo.Item("SEND_INPUT_DIR").ToString()                '送信データ格納フォルダパス
        Dim backupFileNm As String = String.Empty
        Dim workAllFileNmHt As Hashtable = New Hashtable                                            '作業フォルダ内ファイル名(入荷・出荷全て)
        Dim workFileHt As Hashtable = New Hashtable                                                 '作業フォルダ内ファイル名(入荷または出荷)
        Dim houkokuDirFileHt As Hashtable = New Hashtable                                           '報告済格納フォルダパス+ファイル名(入荷・出荷全て)
        Dim kugiri As String = String.Empty
        Dim sendFileNm As String = drRcvConfInfo.Item("SEND_FILE_NM").ToString()                    '送信用ファイル名の作成
        Dim workFileNm As String = String.Empty
        Dim workAllFileNm As String = String.Empty
        Dim houkokuFileNm As String = String.Empty
        'Dim stock As String = String.Empty
        'Dim stockFlag As Boolean = False

        '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo Start 
        Dim sendBackupDir As String = drRcvConfInfo.Item("SEND_BACKUP_DIR").ToString()              'バックアップ送信データ格納フォルダパス
        Dim sendBackupFileNm As String = String.Concat(System.IO.Path.GetFileNameWithoutExtension(sendFileNm) _
                                                       , MyBase.GetSystemDateTime(0).Substring(2) _
                                                       , MyBase.GetSystemDateTime(1).Substring(0, 6) _
                                                       , System.IO.Path.GetExtension(sendFileNm))   'バックアップ送信用ファイル名の作成
        '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo End 

        Try
            '入荷⇒出荷の順にファイル操作を行う(入荷：1回目,出荷：2回目)
            For i As Integer = 0 To 1

                If i = 1 Then
                    'ファイル数を初期化
                    fileCount = 0
                    'BACKUP格納フォルダを出荷に切り替え
                    backupInputDir = drRcvConfInfo.Item("OUTKA_BACKUP_INPUT_DIR").ToString()
                    '報告済格納フォルダを出荷に切り替え
                    houkokuInputDir = drRcvConfInfo.Item("OUTKA_HOKOKU_DIR").ToString()
                    'hashtableの初期化
                    workFileHt.Clear()
                End If

                'BACKUP格納フォルダのファイル操作
                'For Each stFilePath As String In System.IO.Directory.GetFiles(backup_File_Name, String.Concat("*.", drRcvConfInfo.Item("SEND_FILE_EXTENTION").ToString()))
                For Each stFilePath As String In System.IO.Directory.GetFiles(backupInputDir)
                    extention = Path.GetExtension(stFilePath)
                    'If String.Compare(extention, String.Concat(".", drRcvConfInfo.Item("SEND_FILE_EXTENTION").ToString()), True) = 0 Then

                    backupFileNm = System.IO.Path.GetFileName(stFilePath)
                    'BACK UPファイルを作業フォルダに移動する
                    System.IO.File.Move(String.Concat(backupInputDir, backupFileNm), String.Concat(workInputDir, backupFileNm))

                    'ファイル数カウント
                    fileCount += 1

                    'BACKUPファイル名をHashtableに格納
                    workFileHt.Add(fileCount, backupFileNm)

                    'End If
                Next stFilePath

                For j As Integer = 1 To fileCount

                    workFileNm = workFileHt(j).ToString()

                    ' StreamReader の新しいインスタンスを生成する
                    Dim cReader As New System.IO.StreamReader(String.Concat(workInputDir, System.IO.Path.GetFileName(workFileNm)), System.Text.Encoding.Default)

                    '送信ファイルを開く
                    Dim cWriter As StreamWriter = New System.IO.StreamWriter(String.Concat(workInputDir, "\", sendFileNm), True, System.Text.Encoding.Default)

                    ' 読み込みできる文字がなくなるまで繰り返す
                    While (cReader.Peek() >= 0)

                        ' ファイルを 1 行ずつ読み込む
                        Dim stBuffer As String = cReader.ReadLine()

                        'カンマ区切りの場合
                        kugiri = ","

                        Dim aryf As String() = Split(stBuffer, kugiri)
                        'For Each aryf As String In Split(stBuffer, kugiri)

                        ''ダブルクォーテーションで始まっている場合
                        'If Left(aryf(0), 1).Equals(Chr(34)) And Right(aryf(0), 1).Equals(Chr(34)) = False Then
                        '    stock = aryf(0)
                        '    stockFlag = True
                        '    Continue For
                        'End If

                        'If stockFlag = True Then
                        '    'ダブルクォーテーションで閉じられている場合
                        '    If Right(aryf(0), 1).Equals(Chr(34)) Then
                        '        aryf(0) = String.Concat(stock, kugiri, aryf(0))
                        '        stockFlag = False
                        '        stock = String.Empty

                        '        'ダブルクォーテーションで閉じられていない場合
                        '    Else
                        '        stock = String.Concat(stock, kugiri, aryf(0))
                        '        Continue For
                        '    End If
                        'End If

                        ''ダブルクォーテーションで囲まれていた場合は、除外した文字列をセットする
                        'If Left(aryf(0), 1).Equals(Chr(34)) And Right(aryf(0), 1).Equals(Chr(34)) Then
                        '    aryf(0) = Mid(aryf(0), 2, Len(aryf(0)) - 2)
                        '    If aryf(0).Equals(drRcvConfInfo.Item("SKIP_STR").ToString()) = True Then
                        '        Continue For
                        '    Else

                        '        '送信用ファイル名の作成
                        '        send_File_Name = "UKIDAILY.CSV"

                        '        'Print(send_File_Name, stBuffer)
                        '    End If
                        'End If

                        If (aryf(0).Substring(1, 6)).Equals(drRcvConfInfo.Item("SKIP_STR").ToString()) = True OrElse _
                           (aryf(0).Substring(0, 6)).Equals(drRcvConfInfo.Item("SKIP_STR").ToString()) = True Then
                            Continue While
                        Else

                            '値の設定
                            cWriter.WriteLine(stBuffer)

                        End If

                        'Next

                    End While

                    '書込みファイルを閉じる
                    cWriter.Close()

                    '読み込みファイルを閉じる
                    cReader.Close()


                    fileAllcnt += 1
                    houkokuDirFileHt.Add(fileAllcnt, String.Concat(houkokuInputDir, workFileNm))
                    workAllFileNmHt.Add(fileAllcnt, workFileNm)

                Next

            Next

            '作業したBACKUPファイルのコピー処理
            For k As Integer = 1 To fileAllcnt

                workAllFileNm = workAllFileNmHt(k).ToString()
                houkokuFileNm = houkokuDirFileHt(k).ToString()

                ''作業フォルダのBACKUPファイル報告済フォルダに移動する
                'System.IO.File.Move(String.Concat(workInputDir, workFileNm), houkokuFileNm)
                '作業フォルダのBACKUPファイル報告済フォルダにコピーする(同一ファイル名が存在した場合は上書き)
                System.IO.File.Copy(String.Concat(workInputDir, workAllFileNm), houkokuFileNm, True)

            Next

            '作業したBACKUPファイルの削除処理
            For k As Integer = 1 To fileAllcnt

                workAllFileNm = workAllFileNmHt(k).ToString()

                '作業フォルダのBACKUPファイルを消す
                System.IO.File.Delete(String.Concat(workInputDir, workAllFileNm))

            Next

            '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo Start 
            '入荷・出荷を合算した送信データ作成したら、バックアップフォルダへコピーする
            System.IO.File.Copy(String.Concat(workInputDir, sendFileNm), String.Concat(sendBackupDir, sendBackupFileNm))
            '要望番号:1018(浮間日次報告のバックアップ先) 2012/05/21 Honmyo End 

            '入荷・出荷を合算した送信データ作成したら、作成フォルダへ移動する
            System.IO.File.Move(String.Concat(workInputDir, sendFileNm), String.Concat(sendInputDir, sendFileNm))

        Catch ex As Exception
            MyBase.SetMessage("S002")
            Return False
            Exit Function
        End Try

        Return True

    End Function


#End Region
    '2012.04.18 要望番号1005 追加END

    '2014/03/24 黎 取込(セミEDI標準化対応) -ST-
#Region "EDI取込ヘッダーデータセット"
    ''' <summary>
    ''' EDI取込ヘッダ取込(標準化版用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiHedStanderdEdition(ByVal flNmArr As ArrayList, ByVal rtDs As DataSet) As DataSet

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

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
            dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED).NewRow()

            'ファイル名取得
            dr("FILE_NAME_RCV") = stFilePath
            dr("ERR_FLG") = "9"

            '格納
            rtDs.Tables(LMH030C.EDI_TORIKOMI_HED).Rows.Add(dr)
        Next stFilePath

        Return rtDs

    End Function

#End Region

#Region "EDI取込詳細データセット"
    ''' <summary>
    ''' EDI取込詳細(セミ標準対応)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiStanderdEdition(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList) As DataSet

        'ふるい落とし機能の追加を忘れずに
        'ふるい落としキー格納用(キー番号がNullならやらない)

        '初期化
        arrCloser = New ArrayList

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty
        Dim cReader As System.IO.StreamReader = Nothing
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()       '営業所コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString() '荷主コード(大)
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '88' ")

        Dim kbnDr2() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '91' ")

        Dim kbnDr3() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '92' ")

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

        '2017/12/01 セミEDI_千葉ITW_新規登録対応 Annen add start
        '"yyyyMMddhhmmssfff"形式でシステム時間を取得する
        Dim sysDatetime As String = String.Concat(MyBase.GetSystemDateTime()(0), MyBase.GetSystemDateTime()(1)).Remove(14)
        '2017/12/01 セミEDI_千葉ITW_新規登録対応 Annen add end

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

                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                '2017/12/01 セミEDI_千葉ITW_新規登録対応 Annen add start
                '千葉ITWの場合、項目「FILE_NAME_OPEN」にファイル名を設定する
                If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("121") OrElse _
                   drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("130") Then
                    'ファイル名と拡張子を分割する
                    Dim temp() As String = dr("FILE_NAME_RCV").ToString.Split("."c)
                    Dim filename As New StringBuilder
                    Dim extention As String
                    If temp.Length < 2 Then
                        filename.Append(temp(0))
                        extention = String.Empty
                    Else
                        For cnt As Integer = 0 To temp.Length - 2
                            filename.Append(temp(cnt))
                        Next
                        extention = temp(temp.Length - 1)
                    End If
                    filename.Append("-")
                    'システムの時間を結合する
                    filename.Append(sysDatetime)
                    '拡張子が取得できたのであれば拡張子を末尾に設定する
                    If String.IsNullOrEmpty(extention).Equals(False) Then
                        filename.Append(".")
                        filename.Append(extention)
                    End If
                    dr("FILE_NAME_OPE") = filename.ToString
                    filename = Nothing
                End If
                '2017/12/01 セミEDI_千葉ITW_新規登録対応 Annen add end
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

                    'SHINODA START
                    'If kbnDr.Length > 0 AndAlso kbnDr(0).Item("SET_NAIYO").ToString() = columnCount.ToString() Then
                    '    aryf = dr(String.Concat("COLUMN_", kbnDr(0).Item("SET_NAIYO_2").ToString())).ToString + aryf
                    'End If

                    If kbnDr.Length > 0 Then
                        Dim KbnTempStr As String() = Split(kbnDr(0).Item("SET_NAIYO").ToString(), ",")
                        Dim KbnTempStr2 As String() = Split(kbnDr(0).Item("SET_NAIYO_2").ToString(), ",")
                        For j As Integer = 0 To KbnTempStr.Length - 1
                            If KbnTempStr(j) = columnCount.ToString() Then
                                aryf = dr(String.Concat("COLUMN_", KbnTempStr2(j))).ToString + aryf
                            End If
                        Next
                    End If

                    'For j As Integer = 0 To kbnDr.Length - 1
                    '    If kbnDr(j).Item("SET_NAIYO").ToString() = columnCount.ToString() Then
                    '        aryf = dr(String.Concat("COLUMN_", kbnDr(j).Item("SET_NAIYO_2").ToString())).ToString + aryf
                    '    End If
                    'Next
                    'SHINODA END

                    'SHINODA START
                    If kbnDr2.Length > 0 AndAlso kbnDr2(0).Item("SET_NAIYO").ToString() = columnCount.ToString() Then
                        aryf = Trim(aryf)
                    End If
                    'SHINODA END

                    dr(String.Concat("COLUMN_", columnCount.ToString)) = aryf
                    columnCount += 1

                Next

                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            'cReader.Close()

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
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

        'SHINODA
        If kbnDr3.Length > 0 Then
            Dim AddKeys As String = kbnDr3(0).Item("SET_NAIYO").ToString().Replace("@", "'")
            If Keys = String.Empty Then
                Keys = AddKeys
            Else
                Keys = "(" + Keys + ") AND " + AddKeys
            End If
        End If
        'SHINODA

        If valIdx <> -1 Then
            Dim dttmp As DataTable = rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clone()

            drBucking = rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Select(Keys)

            For Each row As DataRow In drBucking
                dttmp.ImportRow(row)
            Next

            rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clear()

            rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Merge(dttmp)
        End If

        Return rtDs

    End Function

#End Region

    '2015.05.18 修正START ローム2ファイル取込み対応
#Region "EDI取込詳細データセット(２ファイル EXCEL)"
    ''' <summary>
    ''' EDI取込詳細(セミEDI 内容違い2ファイル取込み対応)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomipluralfileExcel(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList _
                                                  , ByVal fileDir As String, ByVal fileNm As String) As DataSet

        'Close関連 --ST--
        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList
        'Close関連 --ED--

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow()
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0

        'ローカル作業                                      
        fileDir = drSemiEdiInfo.Item("RCV_INPUT_DIR").ToString()                                   'フォルダ名(ローカル)

        Dim rowNoMin As Integer = Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT").ToString) + 1   '行の開始数
        Dim colNoMax As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT").ToString)  '列の最大数
        Dim rowNoKey As Integer = 1                                                                 'Cashに登録されるまで、とりあえず１列目を設定

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

        ' EXCEL OPEN
        Try
            xlBook = xlBooks.Open(String.Concat(fileDir, fileNm))
        Catch ex As Exception
            '例外がスローされたら処理強制終了
            rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(0).Item("ERR_FLG") = "2"  '2'は当PGについて例外エラーとする
            Return rtDs
        End Try

        'シート
        xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)

        xlApp.Visible = False

        '最大行を取得(rowNoKey列の最終入力行を取得)
        Dim rowNoMax As Integer = 0

        xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

        rowNoMax = xlApp.ActiveCell.Row

        '２次元配列に取得する
        Dim arrData(,) As Object
        arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

        Dim arrList As New List(Of String)

        '２次元→DSにセットする
        For j As Integer = rowNoMin To rowNoMax

            If arrData(j, rowNoKey) Is Nothing Then

                Continue For
            Else
                If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                    Continue For
                End If
            End If

            If arrList.Contains(arrData(j, 2).ToString()) = False Then
                arrList.Add(arrData(j, 2).ToString())
            Else
                Continue For
            End If

            'DSに格納
            dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Select(String.Concat(" COLUMN_1 = '", arrData(j, 2).ToString().Trim(), "'"))
            If dr.Length > 0 Then
                For i As Integer = 0 To dr.Length - 1
                    dr(i).Item("COLUMN_1") = arrData(j, 1).ToString().Trim()
                Next
            End If
        Next

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

        Return rtDs

    End Function

    'ADD START 2022/10/28 033290 大阪ロームEDI改修
    ''' <summary>
    ''' EDI取込詳細(ローム(大阪))(セミEDI 内容違い2ファイル取込み対応)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomipluralfileExcelRomeOsaka(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList _
                                                  , ByVal fileDir As String, ByVal fileNm As String) As DataSet

        'Close関連 --ST--
        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList
        'Close関連 --ED--

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow()

        'ローカル作業                                      
        fileDir = drSemiEdiInfo.Item("RCV_INPUT_DIR").ToString()                                   'フォルダ名(ローカル)

        Dim rowNoMin As Integer = Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT").ToString) + 1   '行の開始数
        Dim colNoMax As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT").ToString)  '列の最大数

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

        ' EXCEL OPEN
        Try
            xlBook = xlBooks.Open(String.Concat(fileDir, fileNm))
        Catch ex As Exception
            '例外がスローされたら処理強制終了
            rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(0).Item("ERR_FLG") = "2"  '2'は当PGについて例外エラーとする
            Return rtDs
        End Try

        'シート
        xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)

        xlApp.Visible = False

        '最大行を取得
        Dim rowNoMax As Integer = 0

        xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

        rowNoMax = xlApp.ActiveCell.Row

        '２次元配列に取得する
        Dim arrData(,) As Object
        arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

        '取得データをソートする
        Dim sortDs As DataSet = New LMH030DS()
        Dim sortDt As DataTable = sortDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Clone()
        Dim sortDt2 As DataTable = sortDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Clone()
        Dim sortDr As DataRow
        '1  シップメント
        '2  出荷伝票
        '17 経路
        '18 出庫予定日
        Dim setClmn() As Integer = {1, 2, 17, 18}
        For j As Integer = rowNoMin To rowNoMax
            '未設定チェック
            Dim isContinue As Boolean = False
            For k As Integer = 0 To setClmn.Length - 1
                If arrData(j, setClmn(k)) Is Nothing Then
                    isContinue = True
                    Exit For
                Else
                    If String.IsNullOrEmpty(arrData(j, setClmn(k)).ToString) Then
                        isContinue = True
                        Exit For
                    End If
                End If
            Next
            If isContinue Then
                Continue For
            End If

            'DataTableにセット
            sortDr = sortDt.NewRow()
            For k As Integer = 0 To setClmn.Length - 1
                sortDr("COLUMN_" + setClmn(k).ToString()) = arrData(j, setClmn(k)).ToString().Trim()
            Next
            sortDt.Rows.Add(sortDr)
        Next
        Dim dv As DataView = New DataView(sortDt)
        'ソート順：経路、出庫予定日、シップメント、出荷伝票
        dv.Sort = "COLUMN_17 ASC, COLUMN_18 ASC, COLUMN_1 ASC, COLUMN_2 ASC"
        For Each drv As DataRowView In dv
            sortDt2.ImportRow(drv.Row)
        Next

        Dim breakKey2 As String = String.Empty
        Dim breakKey17 As String = String.Empty
        Dim breakKey18 As String = String.Empty
        Dim shipno As String = String.Empty

        '取込ファイルの経路、出庫予定日が同一のものでシップメント番号が複数ある場合、
        '一番若いシップメント番号をLMSのオーダー番号に設定する。
        For Each drData As LMH030DS.LMH030_EDI_TORIKOMI_DTLRow In sortDt2.Rows
            '経路または出庫予定日のブレイク時、一番若いシップメント番号を変数に設定
            If Not drData.COLUMN_17.Trim().Equals(breakKey17) OrElse
               Not drData.COLUMN_18.Trim().Equals(breakKey18) Then
                shipno = drData.COLUMN_1.Trim()
            End If

            '出荷伝票番号のブレイク時、シップメント番号をオーダー番号に設定
            If Not drData.COLUMN_2.Trim().Equals(breakKey2) Then
                'DSに格納
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Select(String.Concat(" COLUMN_256 = '", drData.COLUMN_2.Trim(), "'"))
                If dr.Length > 0 Then
                    For i As Integer = 0 To dr.Length - 1
                        dr(i).Item("COLUMN_1") = shipno
                    Next
                End If
            End If

            breakKey2 = drData.COLUMN_2.Trim()
            breakKey17 = drData.COLUMN_17.Trim()
            breakKey18 = drData.COLUMN_18.Trim()
        Next

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

        Return rtDs

    End Function
    'ADD END 2022/10/28 033290 大阪ロームEDI改修

#End Region
    '2015.05.18 修正START ローム2ファイル取込み対応

#Region "EDI取込詳細データセット(固定長)"
    ''' <summary>
    ''' EDI取込詳細(セミ標準対応)(固定長)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiFixedLengthStanderdEdition(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList) As DataSet

        'ふるい落とし機能の追加を忘れずに
        'ふるい落としキー格納用(キー番号がNullならやらない)

        arrCloser = New ArrayList

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
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
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"
                dr("COLUMN_1") = stBuffer

                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            'cReader.Close()

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            '行カウントをリセット
            gyoCount = 0
        Next

        Return rtDs

    End Function

#End Region

#Region "EDI取込詳細(セミ標準対応)(固定長を前提とするが、改行までを1行として取り込む。空行はスキップする)"

    ''' <summary>
    ''' EDI取込詳細(セミ標準対応)(固定長を前提とするが、改行までを1行として取り込む。空行はスキップする)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <param name="arrCloser"></param>
    ''' <returns></returns>
    Private Function SetDataEdiTorikomiShosaiReadLineStanderdEdition(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, ByRef arrCloser As ArrayList) As DataSet

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim gyoCount As Integer = 0

        Dim stBuffer As String = ""

        Dim cReader As System.IO.StreamReader = Nothing

        ' EDI取込HEDの数だけループ
        For i As Integer = 0 To dtHed.Rows.Count - 1

            ' StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(String.Concat(drSemiEdiInfo.Item("RCV_INPUT_DIR"), dtHed.Rows(i).Item("FILE_NAME_RCV")), System.Text.Encoding.Default)

            ' Closeコレクションにコレクト
            arrCloser.Add(DirectCast(cReader, System.IO.StreamReader))

            ' 読み込みできる文字がなくなるまで繰り返す
            While (cReader.Peek() >= 0)

                ' 先頭行飛ばしカウントの数だけ行を空読みする。
                If gyoCount = 0 Then
                    For j As Integer = 0 To Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT")) - 1
                        ' ファイルを改行単位に読み飛ばす
                        cReader.ReadLine()
                        If cReader.Peek() < 0 Then
                            Exit While
                        End If
                    Next
                End If

                ' ファイルを改行単位に読み込む
                stBuffer = cReader.ReadLine()

                If stBuffer.Length = 0 Then
                    Continue While
                End If

                'データセットに登録
                gyoCount += 1
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("FILE_NAME_OPE") = dtHed.Rows(i).Item("FILE_NAME_OPE")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"
                dr("COLUMN_1") = stBuffer

                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)

            End While

            'cReader を閉じる
            'cReader.Close()

            ' 空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            End If

            ' 行カウントをリセット
            gyoCount = 0
        Next

        Return rtDs

    End Function

#End Region

#Region "EDI取込詳細データセット(EXCEL対応:CLOSE改良)"
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
    Private Function SetDataEdiTorikomiShosaiExcelStanderdEdition(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, _
                                                                  ByRef arrCloser As ArrayList, Optional ByVal sheet As Object = 1) As DataSet

        'Close関連 --ST--
        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList
        'Close関連 --ED--

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty
        Dim folderNm As String = String.Empty

        '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
        '"yyyyMMddhhmmssfff"形式でシステム時間を取得する
        Dim sysDatetime As String = String.Concat(MyBase.GetSystemDateTime()(0), MyBase.GetSystemDateTime()(1)).Remove(14)
        '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

        'ローカル作業                                      
        folderNm = drSemiEdiInfo.Item("RCV_INPUT_DIR").ToString()                                   'フォルダ名(ローカル)

        Dim rowNoMin As Integer = Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT").ToString) + 1   '行の開始数
        Dim colNoMax As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT").ToString)  '列の最大数
        Dim rowNoKey As Integer = 1                                                                 'Cashに登録されるまで、とりあえず１列目を設定

        '明細 DataTable の COLUMN_n の列数カウント(本実装時点で列数256)
        Dim maxColSuffix As Integer = 0
        Dim colSuffix As Integer
        For Each col As DataColumn In rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Columns
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
                rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Columns.Add(String.Concat("COLUMN_", i.ToString()), Type.GetType("System.String"))
            Next
        End If

        '2015.06.17 追加START 協立化学対応
        If String.IsNullOrEmpty(drSemiEdiInfo.Item("DEVIDE_NO_1").ToString) = False Then
            rowNoKey = Convert.ToInt32(drSemiEdiInfo.Item("DEVIDE_NO_1").ToString)
        End If
        '2015.06.17 追加END 協立化学対応
        If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("155") Then
            ' SBS東芝ロジスティクス(群馬) の場合、データありの行でも先頭列は未設定の場合がある。
            rowNoKey = 2
        ElseIf drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("157") Then
            ' シグマアルドリッチジャパン(千葉) の場合
            ' 先頭列のみ値(項番)の設定ありの場合がある。
            rowNoKey = 2
            ' EDI取込HED に出荷予定日 列を追加する。
            If Not dtHed.Columns.Contains("OUTKA_PLAN_DATE") Then
                dtHed.Columns.Add("OUTKA_PLAN_DATE", GetType(String))
            End If
        End If

        Dim fileNm As String = String.Empty

        '荷主明細取得
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString()    '荷主コード(大)
        Dim kbnDr92() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                         ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '92' ")
        '2015.05.21 千葉・MRCデュポン対応 追加START
        Dim CustDtl88() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '88' ")
        '2015.05.21 千葉・MRCデュポン対応 追加END

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
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "2"  '2'は当PGについて例外エラーとする
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

            If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("157") Then
                ' シグマアルドリッチジャパン(千葉) の場合
                If arrData.GetLength(0) >= (rowNoMin - 4) AndAlso arrData.GetLength(1) >= 3 Then
                    ' EDI取込HED に追加した出荷予定日に、ヘッダ部(取込範囲より上) の特定セルより値を設定する。
                    Dim outokaPlanDateObj As Object = arrData(rowNoMin - 4, 3)
                    If (Not (outokaPlanDateObj Is Nothing)) AndAlso
                        String.IsNullOrEmpty(outokaPlanDateObj.ToString()) = False Then
                        dtHed.Rows(i).Item("OUTKA_PLAN_DATE") = outokaPlanDateObj.ToString()
                    End If
                End If
            End If

            '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
            Dim isDsp As Boolean = IsDspGokyu(rtDs)
            '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

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
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount
                dr("ERR_FLG") = "9"

                '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
                '2018/01/31 Annen 001007 【LMS】_セミEDI開発_片山ナルコ UPD start
                '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
                'セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会、千葉片山ナルコの場合、項目「FILE_NAME_OPEN」にファイル名を設定する
                If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("122") OrElse _
                   drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("123") OrElse _
                   drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("124") OrElse _
                   drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("130") Then

                    ''セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会の場合、項目「FILE_NAME_OPEN」にファイル名を設定する
                    'If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("131") Then
                    '2018/01/31 Annen 001007 【LMS】_セミEDI開発_片山ナルコ UPD end
                    '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end

                    'ファイル名と拡張子を分割する
                    Dim temp() As String = dr("FILE_NAME_RCV").ToString.Split("."c)
                    Dim filename As New StringBuilder
                    Dim extention As String
                    If temp.Length < 2 Then
                        filename.Append(temp(0))
                        extention = String.Empty
                    Else
                        For cnt As Integer = 0 To temp.Length - 2
                            filename.Append(temp(cnt))
                        Next
                        extention = temp(temp.Length - 1)
                    End If
                    filename.Append("-")
                    'システムの時間を結合する
                    filename.Append(sysDatetime)
                    '拡張子が取得できたのであれば拡張子を末尾に設定する
                    If String.IsNullOrEmpty(extention).Equals(False) Then
                        filename.Append(".")
                        filename.Append(extention)
                    End If
                    dr("FILE_NAME_OPE") = filename.ToString
                    filename = Nothing
                End If
                '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

                'DSに格納
                For k As Integer = 1 To colNoMax

                    '2015.05.21 千葉・MRCデュポン対応 追加START
                    Dim concatFlg As Boolean = False
                    '2015.05.21 千葉・MRCデュポン対応 追加END

                    If arrData(j, k) Is Nothing Then
                        dr(String.Concat("COLUMN_", k.ToString)) = String.Empty
                    Else

                        '2015.05.21 千葉・MRCデュポン対応 追加START
                        If CustDtl88.Length > 0 Then
                            Dim KbnTempStr As String() = Split(CustDtl88(0).Item("SET_NAIYO").ToString(), ",")
                            Dim KbnTempStr2 As String() = Split(CustDtl88(0).Item("SET_NAIYO_2").ToString(), ",")
                            For l As Integer = 0 To KbnTempStr.Length - 1
                                If KbnTempStr2(l) = Convert.ToString(k) Then
                                    concatFlg = True
                                    dr(String.Concat("COLUMN_", KbnTempStr2(0).ToString)) = String.Concat(Convert.ToString(arrData(j, Convert.ToInt32(KbnTempStr(0)))), Convert.ToString(arrData(j, Convert.ToInt32(KbnTempStr2(0)))))
                                End If
                            Next
                        End If
                        '2015.05.21 千葉・MRCデュポン対応 追加END

                        '2015.05.21 千葉・MRCデュポン対応 修正START
                        If concatFlg = False Then
                            '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 upd start
                            'DSP五協フード＆ケミカル株式会社で取得した値の頭1文字がアポストロフィの場合、そのアポストロフィを削除して値を格納する
                            Dim isAposCut As Boolean = False
                            If isDsp.Equals(True) Then
                                Dim val As String = arrData(j, k).ToString().Trim()
                                If val.Length > 0 Then
                                    If Strings.Left(val, 1).Equals("'") Then
                                        isAposCut = True
                                    End If
                                End If
                            End If
                            If isAposCut.Equals(True) Then
                                Dim val As String = arrData(j, k).ToString().Trim()
                                dr(String.Concat("COLUMN_", k.ToString)) = val.Substring(1)
                            Else
                                dr(String.Concat("COLUMN_", k.ToString)) = arrData(j, k).ToString().Trim()
                            End If
                            '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 upd end
                        End If
                        '2015.05.21 千葉・MRCデュポン対応 修正END
                    End If
                Next

                'ADD START 2018/11/07 要望番号002046
                '横浜ロームの場合のみCOLUMN_1「出荷伝票」の値をCOLUMN_256にコピーする
                '(SetDataEdiTorikomipluralfileExcel()でCOLUMN_1は「シップメント」で上書きされることがあるため)
                'UPD START 2022/10/28 033290 大阪ローム対応
                'If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("135") Then
                If drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("135") OrElse
                   drSemiEdiInfo.Item("EDI_CUST_INDEX").ToString.Equals("136") Then
                    'UPD END 2022/10/28 033290 大阪ローム対応
                    dr("COLUMN_256") = dr("COLUMN_1")
                End If
                'END START 2018/11/07 要望番号002046

                'DSにAdd
                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)
            Next

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
            Else

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

                '2015.06.11 追加　ローム並び順対応 START
                Dim sortKeys As String = String.Empty
                '2015.06.11 追加　ローム並び順対応 END

                'AND条件の追加
                If kbnDr92.Length > 0 Then
                    Dim AddKeys As String = kbnDr92(0).Item("SET_NAIYO").ToString().Replace("@", "'")
                    '2015.06.11 追加　ローム並び順対応 START
                    sortKeys = kbnDr92(0).Item("SET_NAIYO_2").ToString()
                    '2015.06.11 追加　ローム並び順対応 END
                    If Keys = String.Empty Then
                        Keys = AddKeys
                    Else
                        Keys = "(" + Keys + ") AND " + AddKeys
                    End If
                End If

                '追加終了 --- 2015.03.16

                If valIdx <> -1 Then
                    Dim dttmp As DataTable = rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clone()

                    '2015.06.11 修正　ローム並び順対応 START
                    drBucking = rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Select(Keys, sortKeys)
                    '2015.06.11 修正　ローム並び順対応 END

                    For Each row As DataRow In drBucking
                        dttmp.ImportRow(row)
                    Next

                    rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clear()

                    rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Merge(dttmp)
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
    '2014/03/24 黎 取込(セミEDI標準化対応) -ED-

    'ADD 2016/09/13 丸和(横浜)対応 Start
#Region "丸和(横浜)EDI取込詳細データセット(EXCEL対応:CLOSE改良)"
    ''' <summary>
    ''' 取込明細(セミ標準を改良:SetDataEdiTorikomiShosaiExcelStanderdEdition)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataEdiTorikomiShosaiExcelMaruwaEdition(ByVal frm As LMH030F, ByVal rtDs As DataSet, ByVal eventShubetsu As Integer, _
                                                                  ByRef arrCloser As ArrayList) As DataSet

        arrCloser = New ArrayList
        Dim colection As ArrayList = New ArrayList

        Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim dtHed As DataTable = rtDs.Tables(LMH030C.EDI_TORIKOMI_HED)
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

        Dim fileNm As String = String.Empty

        '荷主明細取得
        Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()      '営業所コード
        Dim custCdL As String = frm.txtCustCD_L.TextValue.ToString()    '荷主コード(大)
        Dim custCdM As String = frm.txtCustCD_M.TextValue.ToString()    '荷主コード(中)   
        Dim kbnDr92() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                         ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL & "' AND SUB_KB = '92' ")

        Dim YokoMaruwaFLG As Boolean = False
        Dim rowGoodNmKey As Integer = 0
        Dim rowDestCdKey As Integer = 0

        'ADD 2016/09/27 丸和（横浜）対応
        Dim kbnDr0S() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select _
                 ("NRS_BR_CD = '" & brCd & "' AND CUST_CD = '" & custCdL + custCdM & "' AND SUB_KB = '0S' ")

        If kbnDr0S.Length > 0 Then
            YokoMaruwaFLG = True

            rowGoodNmKey = Convert.ToInt32(drSemiEdiInfo.Item("M_GOODS_NM_NO").ToString)    '商品名
            rowDestCdKey = Convert.ToInt32(drSemiEdiInfo.Item("L_DEST_CD_NO").ToString)     '納品先CD

        End If

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
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "2"  '2'は当PGについて例外エラーとする
                Return rtDs
            End Try

            'シート
            xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)

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
                    'rowGoodNmKey
                    If arrData(j, rowGoodNmKey) Is Nothing _
                        AndAlso arrData(j, rowDestCdKey) Is Nothing Then
                        '横浜　丸和は終了(納品先CD,商品名がカラ)
                        Exit For
                    Else
                        Continue For
                    End If

                Else
                    If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then

                        If arrData(j, rowGoodNmKey) Is Nothing _
                             AndAlso arrData(j, rowDestCdKey) Is Nothing Then
                            '横浜　丸和は終了(納品先CD,商品名がカラ)
                            Exit For
                        Else
                            Continue For
                        End If

                    End If
                    '依頼No設定されていても、納品先CD・商品名がないときは終了する
                    If arrData(j, rowGoodNmKey) Is Nothing _
                        AndAlso arrData(j, rowDestCdKey) Is Nothing Then
                        '横浜　丸和は終了(納品先CD,商品名がカラ)
                        Exit For

                    End If

                End If

                gyoCount += 1
                dr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).NewRow()
                dr("FILE_NAME_RCV") = dtHed.Rows(i).Item("FILE_NAME_RCV")
                dr("REC_NO") = gyoCount
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
                rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Add(dr)
            Next

            '納品先CD再設定
            '  依頼Noの初めで同じ依頼Noに設定する
            If YokoMaruwaFLG = True Then
                Dim maruwaDr As DataRow = Nothing
                Dim sDestCD As String = String.Empty
                Dim sIraiNo As String = String.Empty
                Dim sGyoNo As String = String.Empty

                For y As Integer = 0 To rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows.Count - 1
                    maruwaDr = rtDs.Tables(LMH030C.EDI_TORIKOMI_DTL).Rows(y)

                    If (sIraiNo).Equals(maruwaDr.Item("COLUMN_1").ToString.Trim) = False _
                        Or (sGyoNo).Equals(maruwaDr.Item("COLUMN_3").ToString.Trim) = False Then

                        sDestCD = maruwaDr.Item("COLUMN_4").ToString.Trim

                        sIraiNo = maruwaDr.Item("COLUMN_1").ToString.Trim
                        sGyoNo = maruwaDr.Item("COLUMN_3").ToString.Trim
                    Else
                        maruwaDr.Item("COLUMN_4") = sDestCD
                    End If

                Next

            End If

            '空ファイルの場合は、HEDにエラーフラグを立てる
            If gyoCount.ToString().Equals("0") Then
                rtDs.Tables("LMH030_EDI_TORIKOMI_HED").Rows(i).Item("ERR_FLG") = "1"
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

                '2015.06.11 追加　ローム並び順対応 START
                Dim sortKeys As String = String.Empty
                '2015.06.11 追加　ローム並び順対応 END

                'AND条件の追加
                If kbnDr92.Length > 0 Then
                    Dim AddKeys As String = kbnDr92(0).Item("SET_NAIYO").ToString().Replace("@", "'")
                    '2015.06.11 追加　ローム並び順対応 START
                    sortKeys = kbnDr92(0).Item("SET_NAIYO_2").ToString()
                    '2015.06.11 追加　ローム並び順対応 END
                    If Keys = String.Empty Then
                        Keys = AddKeys
                    Else
                        Keys = "(" + Keys + ") AND " + AddKeys
                    End If
                End If

                If valIdx <> -1 Then
                    Dim dttmp As DataTable = rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clone()

                    '2015.06.11 修正　ローム並び順対応 START
                    drBucking = rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Select(Keys, sortKeys)
                    '2015.06.11 修正　ローム並び順対応 END

                    For Each row As DataRow In drBucking
                        dttmp.ImportRow(row)
                    Next

                    rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Clear()

                    rtDs.Tables("LMH030_EDI_TORIKOMI_DTL").Merge(dttmp)
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

        Next

        Return rtDs

    End Function
#End Region
    'ADD 2016/09/13 丸和(横浜)対応 End

#End Region 'DataSet設定

#Region "検索成功時"

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMH030F, ByVal ds As DataSet, ByVal reFlg As String)

        Dim dt As DataTable = ds.Tables(LMH030C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        ''SPREAD(表示行)初期化
        'frm.sprEdiList.CrearSpread()

        '進捗区分名の設定
        dt = Me.StatusSet(dt)

        ''取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '要望番号1991 2013.04.02 追加START
        'スプレッドの列の表示・非表示設定
        Call Me._G.SetSpreadVisible()
        '要望番号1991 2013.04.02 追加END

        'SPREAD行の背景色設定
        Call Me._G.SetSpreadColor(dt)

        'Me._CntSelect = MyBase.GetResultCount.ToString()
        Me._CntSelect = dt.Rows.Count.ToString()

        ''再描画する場合は検索結果メッセージを表示しない
        'If reFlg.Equals("NEW") = True Then

        '    'メッセージエリアの設定
        '    MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})

        'End If

        'データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        'カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})

        End If

    End Sub

#End Region

#Region "出荷登録成功時"
    ''' <summary>
    ''' 出荷登録時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessOutkaSave(ByVal frm As LMH030F, ByVal ds As DataSet)

        MyBase.ShowMessage(frm, "G002", New String() {"出荷登録", String.Empty})
    End Sub

#End Region

    '2012.03.25 大阪対応START
#Region "運送登録成功時"
    ''' <summary>
    ''' 運送登録時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessUnsoSave(ByVal frm As LMH030F, ByVal ds As DataSet)


        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
        Dim dtCntRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数
        Dim sCnt As String = "(" & dtCntRet.Rows(0).Item("RCV_HED_INS_CNT").ToString & "件)"
        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End


        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
        'MyBase.ShowMessage(frm, "G002", New String() {"運送登録", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {"運送登録", sCnt})
        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End

    End Sub

#End Region
    '2012.03.25 大阪対応END

#Region "実績作成成功時"
    ''' <summary>
    ''' 実績作成時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJissekiSakusei(ByVal frm As LMH030F, ByVal ds As DataSet)

        MyBase.ShowMessage(frm, "G002", New String() {"実績作成", String.Empty})
    End Sub

#End Region

#Region "取込処理成功時"
    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">取込処理データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessTorikomi(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"取込処理", String.Empty})
    End Sub

#End Region

#Region "実績取消成功時"
    ''' <summary>
    ''' 実績取消時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJissekiTorikesi(ByVal frm As LMH030F, ByVal ds As DataSet)

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
    Private Sub SuccessEdiTorikesi(ByVal frm As LMH030F, ByVal ds As DataSet)

        '■要望番号972対応 2012/05/14 Honmyo Start
        'MyBase.ShowMessage(frm, "G002", New String() {"EDI取消", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {"EDI取消", String.Concat("(", ds.Tables("LMH030_OUTKAEDI_L").Rows.Count, "件)")})
        '■要望番号972対応 2012/05/14 Honmyo End

    End Sub

#End Region

#Region "実績作成済⇒実績未成功時"
    ''' <summary>
    ''' 実績作成済⇒実績未成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJissekiSakuseiJissekimi(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"実績作成済⇒実績未", String.Empty})
    End Sub

#End Region

#Region "EDI取消⇒未登録成功時"
    ''' <summary>
    ''' EDI取消⇒未登録成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessTorikesiMitouroku(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"EDI取消⇒未登録", String.Empty})
    End Sub

#End Region

#Region "実績送信済⇒送信待成功時"
    ''' <summary>
    ''' 実績送信済⇒実績未時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSakuseizumiJissekimi(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"実績送信済⇒実績送信待", String.Empty})
    End Sub

#End Region

#Region "実績送信済⇒実績未成功時"
    ''' <summary>
    ''' 実績送信済⇒実績未時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessJissekiSousinJissekimi(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"実績送信済⇒実績未", String.Empty})
    End Sub

#End Region

#Region "出荷取消⇒未登録成功時"
    ''' <summary>
    ''' 出荷取消⇒未登録時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessTourokuzumiMitouroku(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"出荷取消⇒未登録", String.Empty})
    End Sub

#End Region

#Region "運送取消⇒未登録成功時"
    ''' <summary>
    ''' 運送取消⇒未登録時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessUnsotorikesiMitouroku(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"運送取消⇒未登録", String.Empty})
    End Sub

#End Region

#Region "一括変更成功時"
    ''' <summary>
    ''' 一括変更時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub IkkatuHenko(ByVal frm As LMH030F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G002", New String() {"一括変更", String.Empty})
    End Sub

#End Region

    '要望番号1129 2012.06.11 修正START
#Region "追加実行時"
    ''' <summary>
    ''' 追加実行処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SuccessTuikaJikkou(ByVal frm As LMH030F)
        MyBase.ShowMessage(frm, "G002", New String() {"追加実行", String.Empty})
    End Sub

#End Region
    '要望番号1129 2012.06.11 修正END

#Region "受信確認送信時"
    ''' <summary>
    ''' 受信確認送信(出力ボタン押下時)処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SuccessRcvConfirmSendCsv(ByVal frm As LMH030F)
        MyBase.ShowMessage(frm, "G002", New String() {"受信確認送信", String.Empty})
    End Sub

#End Region

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetCmbNohinPrt(ByVal frm As LMH030F)

        Dim ediCustIndex As String = Me.GetEdiCustIndexFromTextBox(frm _
                                                                 , LMH030C.M_EDI_CUST_INOUT_KB_OUTKA)

        ' BPカストロール
        Dim kbnGrouopCd As String = LMH030C.COMBO_NOHIN_PRT_KBN_GROUP.BP
        If (LMH030C.EDI_CUST_INDEX.NICHIGO.Equals(ediCustIndex)) Then
            ' 日本合成化学
            kbnGrouopCd = LMH030C.COMBO_NOHIN_PRT_KBN_GROUP.NICHIGO
        End If

        _G.SetCmbNohinPrt(_LMHconV.SelectKBNGroupListDataRow(kbnGrouopCd))

    End Sub

#End If



#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(出荷登録処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "OutkaSave")

        '「出荷登録」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.SAVEOUTKA, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "OutkaSave")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(実績作成)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "JissekiSakusei")

        '「実績作成」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.CREATEJISSEKI, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "JissekiSakusei")


    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(紐付け)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "Himoduke")

        '「紐付け」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.HIMODUKE, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "Himoduke")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し(EDI取消処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "EdiCansel")

        '「EDI取消」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.EDITORIKESI, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "EdiCansel")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(セミEDI：画面手動取込)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Torikomi")

        Me.ActionControl(LMH030C.EventShubetsu.TORIKOMI, frm)

        Logger.EndLog(Me.GetType.Name, "Torikomi")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し(運送登録)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "UnsoSave")

        Call Me.ActionControl(LMH030C.EventShubetsu.SAVEUNSO, frm)

        Logger.EndLog(Me.GetType.Name, "UnsoSave")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し(実績取消)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "JissekiDel")

        '「実績取消」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.TORIKESIJISSEKI, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "JissekiDel")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Call ActionControl(LMH030C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '「マスタ参照」処理
        MyBase.Logger.StartLog(Me.GetType.Name, "REF Master")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False
            'START SHINOHARA 要望番号513
            Call Me.ActionControl(LMH030C.EventShubetsu.ENTER, frm)
            'END SHINOHARA 要望番号513
        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True
            'START SHINOHARA 要望番号513
            Call Me.ActionControl(LMH030C.EventShubetsu.MASTER, frm)
            'END SHINOHARA 要望番号513
        End If
        'START SHINOHARA 要望番号513
        'Call Me.ActionControl(LMH030C.EventShubetsu.MASTER, frm)
        'END SHINOHARA 要望番号513
        MyBase.Logger.EndLog(Me.GetType.Name, "REF Master")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(初期荷主変更)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "")

        '「初期荷主変更」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.DEF_CUST, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMH030F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByVal frm As LMH030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "RowSelection")

        '「ダブルクリック」処理
        Call Me.ActionControl(LMH030C.EventShubetsu.DOUBLE_CLICK, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 実行ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnExe_Click(ByRef frm As LMH030F)

        MyBase.Logger.StartLog(Me.GetType.Name, "jikkou")

        '処理コンボの必須チェック
        If Me._V.JikkouHissuCheck() = False Then
            Exit Sub
        End If

        Dim JikkouShubetsu As String = String.Empty

        JikkouShubetsu = frm.cmbExe.SelectedValue.ToString()

        Select Case JikkouShubetsu

            Case "01"
                '「EDI取消⇒未登録の実行」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.TORIKESI_MITOUROKU, frm)

            Case "02"
                '「実績作成済⇒実績未の実行」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.SAKUSEIZUMI_JISSEKIMI, frm)

            Case "03"
                '「実績送信済⇒送信待の実行」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.SOUSINZUMI_SOUSINMACHI, frm)

            Case "04"
                '「実績送信済⇒実績未の実行」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.SOUSINZUMI_JISSEKIMI, frm)

            Case "05"
                '「追加実行」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.SAKURA_TUIKAJIKKOU, frm)

            Case "06"
                '「出荷取消⇒未登録」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.TOUROKUZUMI_MITOUROKU, frm)

                '2012.04.04 大阪対応追加START
            Case "07"
                '「運送取消⇒未登録」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.UNSOTORIKESI_MITOUROKU, frm)
                '2012.04.04 大阪対応追加END

                '2012.06.21 埼玉対応追加START
            Case "08"
                '「EDI出荷データ荷主コード設定」処理
                Call Me.ActionControl(LMH030C.EventShubetsu.CUST_CD_SETUP, frm)
                '2012.06.21 埼玉対応追加END

        End Select

        MyBase.Logger.EndLog(Me.GetType.Name, "jikkou")

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理呼び出し(検索条件での印刷処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMH030F)

        Logger.StartLog(Me.GetType.Name, "Print")
        '印刷処理(検索条件での印刷)
        Me.ActionControl(LMH030C.EventShubetsu.SELPRINT, frm)

        Logger.EndLog(Me.GetType.Name, "Print")

    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnIkkatuChange_Click(ByRef frm As LMH030F)

        MyBase.Logger.StartLog(Me.GetType.Name, "ikkatuhenko")

        Call Me.ActionControl(LMH030C.EventShubetsu.IKKATUHENKO, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ikkatuhenko")

    End Sub

    ''' <summary>
    ''' 一括変更コンボ選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbIkkatuChangeKbn_SelectedValueChanged(ByVal frm As LMH030F)

        Call Me._G.IkkatuClearControl(frm)

        'BP・カストロール対応 terakawa 2012.12.26 Start
        Dim sysdate As String() = MyBase.GetSystemDateTime()
        'Call Me._G.IkkatuSetControl(frm)
        Call Me._G.IkkatuSetControl(frm, sysdate(0))
        'BP・カストロール対応 terakawa 2012.12.26 End

    End Sub

    ''' <summary>
    ''' 進捗区分排他制御
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkObj"></param>
    ''' <remarks></remarks>
    Friend Sub StatusControlHaita(ByVal frm As LMH030F, ByVal chkObj As Object)

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
    Friend Sub StatusControl(ByVal frm As LMH030F, ByVal chkObj As Object)

        Dim chkitem As CheckBox = DirectCast(chkObj, CheckBox)

        If chkitem.Checked = False Then
            Exit Sub
        End If

        frm.chkstaRedData.Checked = False
        frm.chkStaTorikesi.Checked = False
        'frm.chkStaAll.Checked = False

    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベントです。(担当者コード時のみ)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub LMH030F_KeyDown(ByVal frm As LMH030F, ByVal e As System.Windows.Forms.KeyEventArgs)
        'キャッシュから名称取得
        Call Me.SetCachedName(frm)
    End Sub

    '▼▼▼要望番号:467
    ''' <summary>
    ''' 出力ボタン(印刷・CSV作成)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnOutput_Click(ByVal frm As LMH030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "outputprint")

        Call Me.ActionControl(LMH030C.EventShubetsu.OUTPUTPRINT, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "outputprint")

    End Sub

    ''' <summary>
    ''' 出力(印刷・CSV作成)コンボ選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbOutput_SelectedValueChanged(ByVal frm As LMH030F)

        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetOutputControl(frm, sysdate(0))

    End Sub
    '▲▲▲要望番号:467

    '要望番号1061 2012.05.15 追加START
    ''' <summary>
    ''' 出力(出力区分)コンボ選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbOutputKb_SelectedValueChanged(ByVal frm As LMH030F)

        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetOutputkbControl(frm, sysdate(0))

    End Sub
    '要望番号1061 2012.05.15 追加END

    '要望番号1991 2013.04.02 追加START
    ''' <summary>
    ''' 項目表示コンボ選択時処理呼び出し(検索と同処理)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbVisibleKb_SelectedValueChanged(ByVal frm As LMH030F)

        MyBase.Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Call ActionControl(LMH030C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub
    '要望番号1991 2013.04.02 追加END

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "営業日取得"
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime((Convert.ToInt32(sStartDay)).ToString("0000/00/00"))

        For i As Integer = 1 To System.Math.Abs(iBussinessDays)  'マイナス値に対応するため絶対値指定

            '基準日からの営業日数分、Doループを繰り返す
            Do
                '日付加算
                If iBussinessDays > 0 Then
                    dBussinessDate = dBussinessDate.AddDays(1)      '翌営業日
                Else
                    dBussinessDate = dBussinessDate.AddDays(-1)     '前営業日
                End If

                If Weekday(dBussinessDate) = 1 OrElse Weekday(dBussinessDate) = 7 Then
                Else
                    '土日でない場合

                    '該当する日付が休日マスタに存在するか？
                    Dim sBussinessDate As String = Format(dBussinessDate, "yyyyMMdd")
                    Dim holDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sBussinessDate & "'")
                    If holDr.Count = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function

#End Region

#Region "月末２営業日取得"
    ''' <summary>
    ''' 月末２営業日取得 土・日・休日チェック(営業日の抽出)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGetsumatu2Day(ByVal iBussinessDays As Integer) As DateTime

        'システム日付より月末日の取得
        Dim MyDate As Date = Now
        MyDate = DateSerial(MyDate.Year, MyDate.Month + 1, 1).AddDays(-1)

        Dim sWorkDayDate As DateTime = MyDate   'Convert.ToDateTime(Me._Blc.GetSlashEditDate(sWorkDay))
        Dim iCnt As Integer = 0

        '土・日は営業日として認めない
        For i As Integer = 1 To 31

            If i > 1 Then
                sWorkDayDate = sWorkDayDate.AddDays(-1)
            End If

            If Weekday(sWorkDayDate) = 1 OrElse Weekday(sWorkDayDate) = 7 Then

            Else

                '土日でない場合

                '該当する日付が休日マスタに存在するか？
                Dim sBussinessDate As String = Format(sWorkDayDate, "yyyyMMdd")
#If False Then  'UPD 2022/12/23
                Dim holDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sWorkDayDate & "'")
#Else
                Dim holDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sBussinessDate & "'")
#End If
                If holDr.Count = 0 Then
                    '営業日なのでカウントアップ
                    iCnt += 1

                End If

            End If

            If iCnt = iBussinessDays Then
                '月末設定営業日取得なので終了
                Exit For
            End If

        Next

        Return sWorkDayDate

    End Function
#End Region

End Class