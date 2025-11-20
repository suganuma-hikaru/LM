' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMHI     : 特定荷主機能
'  プログラムID     :  LMI511H : JNC EDI
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.LM.Utility
Imports Microsoft.Office.Interop
Imports FarPoint.Win.Spread

''' <summary>
''' LMI511ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI511H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI511V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI511G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG

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
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' ファイルパス・EDI荷主INDEX格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _RtnStr As ArrayList

    ''' <summary>
    ''' 初期表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean

    ''' <summary>
    ''' EDI荷主INDEX
    ''' </summary>
    ''' <remarks></remarks>
    Private _EdiCustIndex As String = String.Empty

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

    ''' <summary>
    ''' 処理モード
    ''' </summary>
    ''' <remarks></remarks>
    Private _Mode As Integer = LMI511C.Mode.INT

    ''' <summary>
    ''' 編集(訂正)中のスプレッド行番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _nowRow As Integer = 0

    ''' <summary>
    ''' 出荷登録先の営業所コード(出荷先で決定)
    ''' </summary>
    ''' <remarks></remarks>
    Private _outkaBrCd As String = String.Empty

    '画面編集前値退避
    Private beforeOutkaDate As String = String.Empty
    Private beforeActUnsoCd As String = String.Empty
    Private beforeMemoUnsoCd As String = String.Empty
    Private beforeUnsoRouteCd As String = String.Empty
    Private beforeSuryRpt As String = String.Empty
    Private beforeSuryRptUnso As String = String.Empty
    Private beforeCarNo As String = String.Empty

    ''' <summary>
    ''' アクティブセル離脱発生前 運送手段コード 退避値
    ''' </summary>
    Private beforeLeaveCellUnsoRouteCd As String = ""

    'EDIデータ作成日時用
    Private systemNow As Date

    ''' <summary>
    ''' 届先グループ情報
    ''' </summary>
    ''' <remarks></remarks>
    Private Structure stDestGroup
        ''' <summary>
        ''' 届先コード
        ''' </summary>
        ''' <remarks></remarks>
        Public destCd As String
        ''' <summary>
        ''' 同一届先コードの件数
        ''' </summary>
        ''' <remarks></remarks>
        Public Cnt As Integer
        ''' <summary>
        ''' 同一届先コードのうちチェックされている件数
        ''' </summary>
        ''' <remarks></remarks>
        Public CheckCnt As Integer
        ''' <summary>
        ''' まとめ番号
        ''' </summary>
        ''' <remarks></remarks>
        Public combiNo As String
    End Structure

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        Me._ShokiFlg = True

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI511F = New LMI511F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Hnadler共通クラスの設定
        Me._LMIconH = New LMIControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI511V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMI511G(Me, frm)

        'G共通クラスの設定
        Me._LMIconG = New LMIControlG(frm)

        '処理モードの設定
        Me._Mode = LMI511C.Mode.INT

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '営業所,倉庫コンボ関連設定
        'MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(Me._Mode)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetControl(MyBase.GetPGID(), frm, sysdate(0))

        'スプレッドの初期設定
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(Me._Mode)

        'フォーカスの設定(ヘッダー部の先頭)
        Call Me._G.SetFocusHeader()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'Gamen共通クラスの設定
        Me._LMIconG = New LMIControlG(DirectCast(frm, Form))

        '初期表示フラグの設定
        Me._ShokiFlg = False

        'スプレッドの初期設定
        Call Me._G.OutputSpread()

    End Sub

#End Region

#Region "イベントコントロール"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI511C.EventShubetsu, ByVal frm As LMI511F)

        '画面初期化処理中は抜ける
        If Me._ShokiFlg Then
            Exit Sub
        End If

        '権限チェック
        If Not Me._V.IsAuthorityChk(eventShubetsu) Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMIconH.StartAction(frm)

        'イベント種別による分岐
        Select Case eventShubetsu
            Case LMI511C.EventShubetsu.OUTKASAVE
                '出荷登録
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '出荷登録の対象外となる明細のチェックを外す
                    If OutkaSaveCheckOff(frm, chkList) Then
                        'チェックされた行番号のリストを再取得
                        chkList = New ArrayList()
                        chkList = Me._V.getCheckList()
                    End If

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsOutkaSaveSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '入力チェック（関連チェック）
                    If Not Me._V.IsOutkaSaveKanrenCheck(Me._G, chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '出荷登録先の営業所コードを取得
                    Call OutkaSaveGetBrCd(frm, chkList)

                    '出荷登録先の営業所が未登録ならエラー
                    If String.IsNullOrEmpty(Me._outkaBrCd) Then
                        MyBase.ShowMessage(frm, "E237", New String() {"出荷登録の対象ではない出荷先"})
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '実施確認
                    If MyBase.ShowMessage(frm, "W152", New String() {"ＥＤＩ出荷データ登録"}) = MsgBoxResult.Cancel Then
                        Call EndAction2(frm, , True)
                        Exit Sub
                    End If

                    '出荷登録処理
                    If Not Me.OutkaSave(frm, chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If
                End With

            Case LMI511C.EventShubetsu.EDIT
                '編集
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsEditSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    'チェックされた行番号(単一)を取得
                    Me._nowRow = Convert.ToInt32(chkList(0))

                    '入力チェック（関連チェック）
                    If Not Me._V.IsEditKanrenCheck(Me._G, Me._nowRow) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '編集処理
                    Call Me.Edit(frm, Me._nowRow)
                End With

            Case LMI511C.EventShubetsu.MTMSAVE
                'まとめ指示
                With Nothing
                    'まとめ指示処理
                    Call Me.MtmSave(frm)
                End With

            Case LMI511C.EventShubetsu.MTMCANCEL
                'まとめ解除
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsMtmCancelSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    'まとめ解除処理
                    Call Me.MtmCancel(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.SNDREQ
                '送信要求
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '編集中に旧データになっていないかチェック
                    For liIdx As Integer = 0 To chkList.Count - 1
                        Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))
                        Dim ediCtlNo As String = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
                        If Not Me._V.IsOldDataChk(ediCtlNo) Then
                            Call EndAction2(frm)
                            Exit Sub
                        End If
                    Next

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsSndReqSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '送信要求処理
                    Call Me.SndReq(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.REVISION
                '報告訂正
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsRevisionSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    'チェックされた行番号(単一)を取得
                    Me._nowRow = Convert.ToInt32(chkList(0))

                    '入力チェック（関連チェック）
                    If Not Me._V.IsRevisionKanrenCheck(Me._G, Me._nowRow) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '報告訂正処理
                    Call Me.Revision(frm, Me._nowRow)
                End With

            Case LMI511C.EventShubetsu.SNDCANCEL
                '送信取消
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '編集中に旧データになっていないかチェック
                    For liIdx As Integer = 0 To chkList.Count - 1
                        Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))
                        Dim ediCtlNo As String = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
                        If Not Me._V.IsOldDataChk(ediCtlNo) Then
                            Call EndAction2(frm)
                            Exit Sub
                        End If
                    Next

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsSndCancelSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '送信取消処理
                    Call Me.SndCancel(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.MTMSEARCH
                'まとめ候補検索
                With Nothing
                    '入力チェック（単項目チェック）
                    If Not Me._V.IsMtmSearchSingleCheck(Me._G) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '入力チェック（関連チェック）
                    If Not Me._V.IsMtmSearchKanrenCheck() Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '編集中なら実行確認
                    Select Case Me._Mode
                        Case LMI511C.Mode.EDT, LMI511C.Mode.REV
                            If MyBase.ShowMessage(frm, "W158") = MsgBoxResult.Cancel Then
                                Call EndAction2(frm)
                                Exit Sub
                            End If
                    End Select

                    'まとめ候補検索処理
                    Call Me.MtmSearch(frm)
                End With

            Case LMI511C.EventShubetsu.SEARCH
                '検索
                With Nothing
                    '入力チェック（単項目チェック）
                    If Not Me._V.IsSearchSingleCheck(Me._G) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '入力チェック（関連チェック）
                    If Not Me._V.IsSearchKanrenCheck() Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '編集中なら実行確認
                    Select Case Me._Mode
                        Case LMI511C.Mode.EDT, LMI511C.Mode.REV
                            If MyBase.ShowMessage(frm, "W158") = MsgBoxResult.Cancel Then
                                Call EndAction2(frm)
                                Exit Sub
                            End If
                    End Select

                    '検索処理
                    Call Me.Search(frm)
                End With

            Case LMI511C.EventShubetsu.SAVEEDT
                '保存(編集)
                With Nothing
                    '編集中に旧データになっていないかチェック
                    Dim ediCtlNo As String = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
                    If Not Me._V.IsOldDataChk(ediCtlNo) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsSaveEditSingleCheck(Me._G, Me._nowRow) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '画面に変更があったかチェック
                    If compareEditValue(frm, Me._nowRow) Then
                        '変更なし：更新処理はスキップして完了する(見た目上は保存したように振る舞う)
                        With Nothing
                            '検索処理を呼び出す
                            Call Search(frm)
                        End With

                    Else
                        '変更有：更新処理を行う
                        With Nothing
                            '保存処理(編集)
                            Call Me.SaveEdit(frm)
                        End With
                    End If
                End With

            Case LMI511C.EventShubetsu.SAVEREV
                '保存(訂正)
                With Nothing
                    '編集中に旧データになっていないかチェック
                    Dim ediCtlNo As String = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
                    If Not Me._V.IsOldDataChk(ediCtlNo) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '保存処理(訂正)
                    Call Me.SaveRevision(frm)
                End With

            Case LMI511C.EventShubetsu.MASTER, LMI511C.EventShubetsu.ENTER
                'マスタ参照
                With Nothing
                    '入力チェック（単項目チェック）
                    If Not Me._V.IsRefMstInputCheck() Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    Dim objNm As String = frm.ActiveControl.Name()
                    Select Case objNm
                        'Case frm.txtCustCD_L.Name, frm.txtCustCD_M.Name
                        '    '荷主コード(大),荷主コード(中)
                        '    frm.lblCustNM_L.TextValue = String.Empty
                        '    frm.lblCustNM_M.TextValue = String.Empty
                        '    Dim prm As LMFormData = New LMFormData
                        '    prm.ReturnFlg = False
                        '    Call Me.ShowPopup(frm, objNm, prm, eventShubetsu)
                        '    '初期メッセージ設定
                        '    MyBase.ShowMessage(frm, "G007")

                        Case Else
                            'ポップ対象外のテキストの場合
                            MyBase.ShowMessage(frm, "G005")
                    End Select

                    '処理終了アクション
                    Call EndAction2(frm)
                End With

            Case LMI511C.EventShubetsu.PRINT1
                '印刷(出荷予定表)
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkList As ArrayList = New ArrayList()
                    chkList = Me._V.getCheckList()

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsPrintSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '印刷処理(出荷予定表)
                    Call Me.Print1(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.PRINT2
                '印刷(酢酸出荷依頼書(川本倉庫))
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkListAll As ArrayList = New ArrayList()
                    chkListAll = Me._V.getCheckList()

                    '川本倉庫のみを対象としたリストにする
                    Dim chkList As ArrayList = New ArrayList()
                    With frm.sprEdiList.ActiveSheet
                        For liIdx As Integer = 0 To chkListAll.Count - 1
                            Dim spIdx As Integer = Convert.ToInt32(chkListAll(liIdx))
                            If LMI511C.OUTKA_POSI_BU_CD.KAWAMOTO.Equals(Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_POSI_BU_CD.ColNo)).Trim()) Then
                                chkList.Add(spIdx)
                            End If
                        Next
                    End With

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsPrintSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '印刷処理(酢酸出荷依頼書(川本倉庫))
                    Call Me.Print2(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.PRINT3
                '印刷(ファクシミリ連絡票(三菱ケミカル))
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkListAll As ArrayList = New ArrayList()
                    chkListAll = Me._V.getCheckList()

                    '三菱ケミカルのみを対象としたリストにする
                    Dim chkList As ArrayList = New ArrayList()
                    With frm.sprEdiList.ActiveSheet
                        For liIdx As Integer = 0 To chkListAll.Count - 1
                            Dim spIdx As Integer = Convert.ToInt32(chkListAll(liIdx))
                            If LMI511C.OUTKA_POSI_BU_CD.MITSUBISHI_CHEMICAL.Equals(Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_POSI_BU_CD.ColNo)).Trim()) Then
                                chkList.Add(spIdx)
                            End If
                        Next
                    End With

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsPrintSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '印刷処理(ファクシミリ連絡票(三菱ケミカル))
                    Call Me.Print3(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.PRINT4
                '印刷(酢酸注文書(KHネオケム))
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkListAll As ArrayList = New ArrayList()
                    chkListAll = Me._V.getCheckList()

                    'KHネオケムのみを対象としたリストにする
                    Dim chkList As ArrayList = New ArrayList()
                    With frm.sprEdiList.ActiveSheet
                        For liIdx As Integer = 0 To chkListAll.Count - 1
                            Dim spIdx As Integer = Convert.ToInt32(chkListAll(liIdx))
                            If LMI511C.OUTKA_POSI_BU_CD.KH_NEOKEM.Equals(Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_POSI_BU_CD.ColNo)).Trim()) Then
                                chkList.Add(spIdx)
                            End If
                        Next
                    End With

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsPrintSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '入力チェック(複数選択チェック)
                    If Not Me._V.IsPrintNotSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '印刷処理(ファクシミリ連絡票(KHネオケム))
                    Call Me.Print4(frm, chkList)
                End With

            Case LMI511C.EventShubetsu.PRINT5
                '印刷(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))
                With Nothing
                    'チェックされた行番号のリストを取得
                    Dim chkListAll As ArrayList = New ArrayList()
                    chkListAll = Me._V.getCheckList()

                    'ｼﾝｺｰｹﾐ神戸のみを対象としたリストにする
                    Dim chkList As ArrayList = New ArrayList()
                    With frm.sprEdiList.ActiveSheet
                        For liIdx As Integer = 0 To chkListAll.Count - 1
                            Dim spIdx As Integer = Convert.ToInt32(chkListAll(liIdx))
                            If LMI511C.OUTKA_POSI_BU_CD.SHINKO_CHEMICAL_KOBE.Equals(Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_POSI_BU_CD.ColNo)).Trim()) Then
                                chkList.Add(spIdx)
                            End If
                        Next
                    End With

                    '入力チェック（単項目チェック）
                    If Not Me._V.IsPrintSingleCheck(chkList) Then
                        Call EndAction2(frm)
                        Exit Sub
                    End If

                    '印刷処理(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))
                    Call Me.Print5(frm, chkList)
                End With
        End Select

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '出荷登録処理
        Call ActionControl(LMI511C.EventShubetsu.OUTKASAVE, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '編集処理
        Call ActionControl(LMI511C.EventShubetsu.EDIT, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'まとめ指示処理
        Call ActionControl(LMI511C.EventShubetsu.MTMSAVE, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'まとめ解除処理
        Call ActionControl(LMI511C.EventShubetsu.MTMCANCEL, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ActionControl(LMI511C.EventShubetsu.SNDREQ, frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ActionControl(LMI511C.EventShubetsu.REVISION, frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ActionControl(LMI511C.EventShubetsu.SNDCANCEL, frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'まとめ候補検索処理
        Call ActionControl(LMI511C.EventShubetsu.MTMSEARCH, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '検索処理
        Call ActionControl(LMI511C.EventShubetsu.SEARCH, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        If e.KeyCode = Keys.Enter Then
            'Enterキー押下
            Me._PopupSkipFlg = False
            Call Me.ActionControl(LMI511C.EventShubetsu.ENTER, frm)
        Else
            'F10押下
            Me._PopupSkipFlg = True
            Call Me.ActionControl(LMI511C.EventShubetsu.MASTER, frm)
        End If

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Select Case Me._Mode
            Case LMI511C.Mode.EDT
                '保存処理(編集)
                Call ActionControl(LMI511C.EventShubetsu.SAVEEDT, frm)
            Case LMI511C.Mode.REV
                '保存処理(訂正)
                Call ActionControl(LMI511C.EventShubetsu.SAVEREV, frm)
        End Select

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI511F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI511F)

        MyBase.Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Select Case frm.cmbPrint.SelectedValue.ToString()
            Case LMI511C.PRINT_TYPE.OUTKA_YOTEI
                '出荷予定表
                Call ActionControl(LMI511C.EventShubetsu.PRINT1, frm)
            Case LMI511C.PRINT_TYPE.SAKUSAN_KAWAMOTO
                '酢酸出荷依頼書(川本倉庫)
                Call ActionControl(LMI511C.EventShubetsu.PRINT2, frm)
            Case LMI511C.PRINT_TYPE.FAX_MITSUBISHI_CHEMICAL
                'ファクシミリ連絡票(三菱ケミカル)
                Call ActionControl(LMI511C.EventShubetsu.PRINT3, frm)
            Case LMI511C.PRINT_TYPE.SAKUSAN_KH_NEOKEMU
                '酢酸注文書(KHネオケム)
                Call ActionControl(LMI511C.EventShubetsu.PRINT4, frm)
            Case LMI511C.PRINT_TYPE.IBA_SHINKO_CHEMICAL_KOBE
                'イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸)
                Call ActionControl(LMI511C.EventShubetsu.PRINT5, frm)
        End Select

        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' Excel出力ボタン押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnExcel_Click(ByVal frm As LMI511F)

        '処理開始アクション
        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

        '配列でスプレッド情報を取得
        Dim arr As ArrayList = Me.GetList(frm.sprEdiList.ActiveSheet)

        Dim max As Integer = arr.Count - 1
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprEdiList.ActiveSheet
        Dim rowNo As Integer = 0

        Dim titleData As String() = {"赤黒", "変更", "報告", "送信訂正", "印刷", "専門印刷", "取込", "積合", "運送", "運送報告", "まとめ番号", "伝票番号", "出荷予定日", "納入予定日", "入荷予定日", "届先名", "届先住所", "出荷先", "運送会社名", "運送会社名(控)", "運送手段名", "商品名", "出荷数量", "単位", "実出荷数量(積み)", "実出荷数量(卸し)", "車両番号", "出荷個数", "印刷番号", "受注番号", "注文番号", "外部記事1", "外部記事2"}

        Dim rowDataList As New List(Of Object())
        Dim groupEndRowList As New List(Of Integer)
        Dim rowIdx As Integer = 0

        For i As Integer = 1 To max

            'スプレッドの行番号を設定

            rowNo = Convert.ToInt32(arr(i))
            rowDataList.Add({
                            spr.Cells(rowNo, _G.sprEdiListExcel.RB_KBN_NM.ColNo),          '赤黒
                            spr.Cells(rowNo, _G.sprEdiListExcel.MOD_KBN_NM.ColNo),         '変更
                            spr.Cells(rowNo, _G.sprEdiListExcel.RTN_FLG_NM.ColNo),         '報告
                            spr.Cells(rowNo, _G.sprEdiListExcel.SND_CANCEL_FLG_NM.ColNo),  '送信訂正
                            spr.Cells(rowNo, _G.sprEdiListExcel.PRTFLG_NM.ColNo),          '印刷
                            spr.Cells(rowNo, _G.sprEdiListExcel.PRTFLG_SUB_NM.ColNo),      '専門印刷
                            spr.Cells(rowNo, _G.sprEdiListExcel.NRS_SYS_FLG_NM.ColNo),     '取込
                            spr.Cells(rowNo, _G.sprEdiListExcel.COMBI.ColNo),              '積合
                            spr.Cells(rowNo, _G.sprEdiListExcel.UNSO_REQ_YN_NM.ColNo),     '運送
                            spr.Cells(rowNo, _G.sprEdiListExcel.UNSO_RTN_FLG_NM.ColNo),    '運送報告
                            spr.Cells(rowNo, _G.sprEdiListExcel.COMBI_NO.ColNo),           'まとめ番号
                            spr.Cells(rowNo, _G.sprEdiListExcel.SR_DEN_NO.ColNo),          '伝票番号
                            spr.Cells(rowNo, _G.sprEdiListExcel.OUTKA_DATE.ColNo),         '出荷予定日
                            spr.Cells(rowNo, _G.sprEdiListExcel.ARRIVAL_DATE.ColNo),       '納入予定日
                            spr.Cells(rowNo, _G.sprEdiListExcel.INKO_DATE.ColNo),          '入荷予定日
                            spr.Cells(rowNo, _G.sprEdiListExcel.DEST_NM.ColNo),            '届先名
                            spr.Cells(rowNo, _G.sprEdiListExcel.DEST_AD_NM.ColNo),         '届先住所
                            spr.Cells(rowNo, _G.sprEdiListExcel.OUTKA_POSI_BU_NM.ColNo),   '出荷先
                            spr.Cells(rowNo, _G.sprEdiListExcel.ACT_UNSO_NM.ColNo),        '運送会社名
                            spr.Cells(rowNo, _G.sprEdiListExcel.MEMO_UNSO_NM.ColNo),       '運送会社名(控)
                            spr.Cells(rowNo, _G.sprEdiListExcel.UNSO_ROUTE_NM.ColNo),      '運送手段名
                            spr.Cells(rowNo, _G.sprEdiListExcel.GOODS_NM.ColNo),           '商品名
                            spr.Cells(rowNo, _G.sprEdiListExcel.SURY_REQ.ColNo),           '出荷数量
                            spr.Cells(rowNo, _G.sprEdiListExcel.SURY_TANI_CD.ColNo),       '単位
                            spr.Cells(rowNo, _G.sprEdiListExcel.SURY_RPT.ColNo),           '実出荷数量(積み)
                            spr.Cells(rowNo, _G.sprEdiListExcel.SURY_RPT_UNSO.ColNo),      '実出荷数量(卸し)
                            spr.Cells(rowNo, _G.sprEdiListExcel.CAR_NO.ColNo),             '車両番号
                            spr.Cells(rowNo, _G.sprEdiListExcel.TUMI_SU.ColNo),            '出荷個数
                            spr.Cells(rowNo, _G.sprEdiListExcel.PRINT_NO.ColNo),           '印刷番号
                            spr.Cells(rowNo, _G.sprEdiListExcel.JYUCHU_NO.ColNo),          '受注番号
                            spr.Cells(rowNo, _G.sprEdiListExcel.ORDER_NO.ColNo),           '注文番号
                            spr.Cells(rowNo, _G.sprEdiListExcel.DELIVERY_NM.ColNo),        '外部記事1
                            spr.Cells(rowNo, _G.sprEdiListExcel.INV_REM_NM.ColNo)          '外部記事2
                            })

            groupEndRowList.Add(rowIdx)

        Next

        'String配列のListから2次元配列に転記
        Dim rowDataArray(rowDataList.Count - 1, titleData.Length - 1) As Object
        rowIdx = -1
        For Each rowData As Object() In rowDataList
            rowIdx += 1
            For colIdx As Integer = 0 To titleData.Length - 1
                rowDataArray(rowIdx, colIdx) = rowData(colIdx)
            Next
        Next

        'Excel出力
        Call Me.OutputExcel("OutputExcel", titleData, rowDataArray, groupEndRowList)

        '処理終了アクション
        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        MyBase.Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' スプレッド明細行の取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Public Function GetList(ByVal activeSheet As SheetView) As ArrayList

        '行取得
        With activeSheet

            Dim arr As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max

                arr.Add(i)
            Next

            Return arr

        End With

    End Function

    ''' <summary>
    ''' Excel出力
    ''' </summary>
    ''' <param name="sheetName">シート名</param>
    ''' <param name="titleData">タイトル行データ</param>
    ''' <param name="bodyData">明細行データ</param>
    ''' <param name="groupEndRowList">セル結合する範囲ごとの最終行のリスト</param>
    Private Sub OutputExcel(ByVal sheetName As String, ByVal titleData As String(), ByVal bodyData As Object(,), ByVal groupEndRowList As List(Of Integer))

        Const HeaderRow As Integer = 1
        Const BodyStartRow As Integer = HeaderRow + 1

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        Try
            'EXCEL開始
            xlApp = New Excel.Application
            xlApp.DisplayAlerts = False
            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()

            '作業シート設定
            xlSheets = xlBook.Worksheets
            xlSheet = DirectCast(xlSheets(1), Excel.Worksheet)
            xlSheet.Name = sheetName

            '全セルの表示形式を文字列に設定
            xlSheet.Cells.NumberFormat = "@"

            '表示形式の設定
            xlSheet.Range("W:W").NumberFormat = "###,###"
            xlSheet.Range("Y:Y").NumberFormat = "#####,###"
            xlSheet.Range("Z:Z").NumberFormat = "#####,###"
            xlSheet.Range("AB:AB").NumberFormat = "#####,###"

            'ヘッダ(1行)の値の設定
            startCell = DirectCast(xlSheet.Cells(HeaderRow, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(HeaderRow, titleData.Length), Excel.Range)
            range = xlSheet.Range(startCell, endCell)
            range.Value = titleData
            'ヘッダ(1行)の罫線の設定
            range.Borders.Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
            range.Borders.Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous

            'ボディの値の設定
            startCell = DirectCast(xlSheet.Cells(BodyStartRow, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(BodyStartRow + bodyData.GetLength(0) - 1, bodyData.GetLength(1)), Excel.Range)
            range = xlSheet.Range(startCell, endCell)
            range.Value = bodyData

            '表示形式設定の適用
            range.Value = range.Value

            '列幅の調整
            xlSheet.Range("A:AH").EntireColumn.AutoFit()

            xlApp.Visible = True

        Finally
            '参照の開放

            If xlSheet IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
            End If

            If xlSheets IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheets)
                xlSheets = Nothing
            End If

            If xlBook IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
            End If

            If xlBooks IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
            End If

            If xlApp IsNot Nothing Then
                xlApp.DisplayAlerts = True
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If

        End Try

    End Sub


    ''' <summary>
    ''' 全チェックボタン押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnAllCheck_Click(ByRef frm As LMI511F)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        With frm.sprEdiList
            .SuspendLayout()

            For spIdx As Integer = 1 To .ActiveSheet.Rows.Count - 1
                .SetCellValue(spIdx, _G.sprEdiListDef.DEF.ColNo, True)
            Next

            .ResumeLayout()
        End With

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 全クリアボタン押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnAllClear_Click(ByRef frm As LMI511F)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        With frm.sprEdiList
            .SuspendLayout()

            For spIdx As Integer = 1 To .ActiveSheet.Rows.Count - 1
                .SetCellValue(spIdx, _G.sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
            Next

            .ResumeLayout()
        End With

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI511F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm, e)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' SPREADのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprEdiList_LeaveCell(ByVal frm As LMI511F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprEdiList_LeaveCell")

        If Me._Mode = LMI511C.Mode.EDT AndAlso
            frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo).Locked = False AndAlso
            frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.ACT_UNSO_NM.ColNo).Locked = False Then
            ' 編集モードかつ運送手段および運送会社が編集可能な状態の場合
            ' 運送手段よりの運送会社設定
            Call SetUnsoCdFromUnsoRouteCd(frm, e)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprEdiList_LeaveCell")

    End Sub

#End Region

#Region "共通処理"

    '''' <summary>
    '''' キャッシュから名称取得（全項目）
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <remarks></remarks>
    'Private Sub SetCachedName(ByVal frm As LMI511F)

    '    With frm
    '        Dim custCdL As String = frm.txtCustCD_L.TextValue
    '        Dim custCdM As String = frm.txtCustCD_M.TextValue

    '        '荷主名称
    '        .lblCustNM_L.TextValue = String.Empty
    '        .lblCustNM_M.TextValue = String.Empty
    '        If Not String.IsNullOrEmpty(custCdL) Then
    '            If String.IsNullOrEmpty(custCdM) Then
    '                custCdM = "00"
    '            End If

    '            Dim custDr() As DataRow = Me._LMIconG.SelectCustListDataRowByNrsBrCd(Convert.ToString(.cmbEigyo.SelectedValue()), custCdL, custCdM, "00", "00")
    '            If 0 < custDr.Length Then
    '                .lblCustNM_L.TextValue = custDr(0).Item("CUST_NM_L").ToString()
    '                .lblCustNM_M.TextValue = custDr(0).Item("CUST_NM_M").ToString()
    '            End If
    '        End If
    '    End With

    'End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="msgClear"></param>
    ''' <remarks>LMIControlHのEndAction後にメッセージクリアロジックを付加</remarks>
    Private Sub EndAction2(ByVal frm As Form, Optional ByVal id As String = "G007", Optional ByVal msgClear As Boolean = False)

        '処理終了アクション
        Call Me._LMIconH.EndAction(frm, id)

        'メッセージをクリア
        If msgClear Then
            MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))
        End If

    End Sub

    ''' <summary>
    ''' 画面の編集前値を退避
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="spIdx">対象行番号</param>
    ''' <remarks></remarks>
    Private Sub backupEditBefore(ByVal frm As LMI511F, ByVal spIdx As Integer)

        With frm.sprEdiList.ActiveSheet
            '出荷予定日
            beforeOutkaDate = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_DATE.ColNo)).Trim()
            '運送会社コード
            beforeActUnsoCd = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.ACT_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(beforeActUnsoCd) Then
                beforeActUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", beforeActUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            '運送会社コード(控)
            beforeMemoUnsoCd = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.MEMO_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(beforeMemoUnsoCd) Then
                beforeMemoUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", beforeMemoUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            '運送手段コード
            beforeUnsoRouteCd = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(beforeUnsoRouteCd) Then
                beforeUnsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", beforeUnsoRouteCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            ' アクティブセル離脱発生前 運送手段コード 退避値
            beforeLeaveCellUnsoRouteCd = beforeUnsoRouteCd
            '実出荷数量(積み)
            beforeSuryRpt = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT.ColNo)).Trim()
            '実出荷数量(卸し)
            beforeSuryRptUnso = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT_UNSO.ColNo)).Trim()
            '車両番号
            beforeCarNo = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CAR_NO.ColNo)).Trim()
        End With

    End Sub

    ''' <summary>
    ''' 画面に変更があったかをチェック
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="spIdx">対象行番号</param>
    ''' <returns>True:変更なし False:変更あり</returns>
    ''' <remarks></remarks>
    Private Function compareEditValue(ByVal frm As LMI511F, ByVal spIdx As Integer) As Boolean

        With frm.sprEdiList.ActiveSheet
            '**現在(変更後)の値を取得**
            '出荷予定日
            Dim afterOutkaDate As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_DATE.ColNo)).Trim()
            '運送会社コード
            Dim afterActUnsoCd As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.ACT_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(afterActUnsoCd) Then
                afterActUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", afterActUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            '運送会社コード(控)
            Dim afterMemoUnsoCd As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.MEMO_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(afterMemoUnsoCd) Then
                afterMemoUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", afterMemoUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            '運送手段コード
            Dim afterUnsoRouteCd As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(afterUnsoRouteCd) Then
                afterUnsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", afterUnsoRouteCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            '実出荷数量(積み)
            Dim afterSuryRpt As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT.ColNo)).Trim()
            '実出荷数量(卸し)
            Dim afterSuryRptUnso As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT_UNSO.ColNo)).Trim()
            '車両番号
            Dim afterCarNo As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CAR_NO.ColNo)).Trim()

            '**変更前と比較**
            If Val(afterOutkaDate) <> Val(beforeOutkaDate) Then
                Return False
            End If
            If afterActUnsoCd <> beforeActUnsoCd Then
                Return False
            End If
            If afterMemoUnsoCd <> beforeMemoUnsoCd Then
                Return False
            End If
            If afterUnsoRouteCd <> beforeUnsoRouteCd Then
                Return False
            End If
            If Val(afterSuryRpt) <> Val(beforeSuryRpt) Then
                Return False
            End If
            If Val(afterSuryRptUnso) <> Val(beforeSuryRptUnso) Then
                Return False
            End If
            If afterCarNo <> beforeCarNo Then
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 運送手段よりの運送会社設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    Private Sub SetUnsoCdFromUnsoRouteCd(ByVal frm As LMI511F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        Dim targetUnsoRouteKbnCd As String = ""
        Dim targetUnsoRouteCd As String
        Dim whereUnsoRouteCd As String = ""

        If e.NewRow = Me._nowRow AndAlso e.NewColumn = _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo Then
            ' 編集中の行にて運送手段名セルへのフォーカス移動の場合
            targetUnsoRouteKbnCd = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(targetUnsoRouteKbnCd) Then
                whereUnsoRouteCd = String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", targetUnsoRouteKbnCd, "' ")
                targetUnsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereUnsoRouteCd)(0).Item("KBN_NM2").ToString()
                If Not String.IsNullOrEmpty(targetUnsoRouteCd) Then
                    ' アクティブセル離脱発生前 運送手段コード 退避
                    beforeLeaveCellUnsoRouteCd = targetUnsoRouteCd
                End If
            End If
        ElseIf e.Row = Me._nowRow AndAlso (e.Row <> e.NewRow OrElse e.Column = _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo) Then
            ' 編集中の行から違う行の選択、または編集中の行にて運送手段名セルからのフォーカス移動の場合
            ' 運送手段コード 取得
            targetUnsoRouteKbnCd = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(targetUnsoRouteKbnCd) Then
                whereUnsoRouteCd = String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", targetUnsoRouteKbnCd, "' ")
                targetUnsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereUnsoRouteCd)(0).Item("KBN_NM2").ToString()
                Dim setUnsoCd As Boolean = False
                If targetUnsoRouteCd <> beforeLeaveCellUnsoRouteCd Then
                    ' 運送手段コード 変更ありの場合
                    ' 運送手段コードに紐付く運送会社コードよりの設定を行う。
                    setUnsoCd = True
                Else
                    ' 運送会社コード 取得
                    Dim actUnsoKbnCd As String
                    Dim actUnsoCd As String = ""
                    actUnsoKbnCd = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(Me._nowRow, _G.sprEdiListDef.ACT_UNSO_NM.ColNo)).Trim()
                    If Not String.IsNullOrEmpty(actUnsoKbnCd) Then
                        actUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", actUnsoKbnCd, "' "))(0).Item("KBN_NM2").ToString()
                    End If
                    If actUnsoCd = "" Then
                        ' 運送会社コード 未設定の場合
                        ' 運送手段コードに紐付く運送会社コードよりの設定を行う。
                        setUnsoCd = True
                    End If
                End If
                If setUnsoCd Then
                    Dim whereNewUnsoCd As String = String.Concat("KBN_GROUP_CD = 'J028' AND KBN_NM1 = '", targetUnsoRouteCd, "' ")
                    Dim drNewUnsoCd As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereNewUnsoCd)
                    If drNewUnsoCd.Count > 0 Then
                        Dim newUnsoCd As String = drNewUnsoCd(0).Item("KBN_NM2").ToString()
                        If Not String.IsNullOrEmpty(newUnsoCd) Then
                            ' 運送手段コードに紐付く運送会社コードありの場合
                            ' 運送会社コードの設定
                            Dim whereActUnsoCd As String = String.Concat("KBN_GROUP_CD = 'J016' AND KBN_NM2 = '", newUnsoCd, "' ")
                            Dim drActUnsoCd As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereActUnsoCd)
                            If drActUnsoCd.Count > 0 Then
                                Dim actUnsoKbnCd As String = drActUnsoCd(0).Item("KBN_CD").ToString()
                                frm.sprEdiList.SetCellValue(Me._nowRow, _G.sprEdiListDef.ACT_UNSO_NM.ColNo, actUnsoKbnCd)
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Sub
#End Region

#Region "マスタデータ"

    ''' <summary>
    ''' ＪＮＣ営業所マスタ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BoMst(ByVal frm As LMI511F, nrsBrCd As String) As DataSet

        '強制実行フラグをオフ
        MyBase.SetForceOparation(False)

        '検索条件をDataSetに設定
        Dim rtDs As DataSet = New LMI511DS()
        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_BO_MST).NewRow()
        dr("NRS_BR_CD") = nrsBrCd
        rtDs.Tables(LMI511C.TABLE_NM.IN_BO_MST).Rows.Add(dr)

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "BoMst")
        Dim rtnDs As DataSet = Me._LMIconH.CallWSAAction(
                DirectCast(frm, Form),
                "LMI511BLF",
                "BoMst",
                rtDs,
                10000)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "BoMst")

        Return rtnDs

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集中に旧データになっていないかチェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <param name="ediCtlNo">EDI管理番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OldDataChk(ByVal frm As LMI511F, nrsBrCd As String, ediCtlNo As String) As DataSet

        '強制実行フラグをオフ
        MyBase.SetForceOparation(False)

        '検索条件をDataSetに設定
        Dim rtDs As DataSet = New LMI511DS()
        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.INOUT_OLD_DATA_CHK).NewRow()
        dr("NRS_BR_CD") = nrsBrCd
        dr("EDI_CTL_NO") = ediCtlNo
        rtDs.Tables(LMI511C.TABLE_NM.INOUT_OLD_DATA_CHK).Rows.Add(dr)

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "OldDataChk")
        Dim rtnDs As DataSet = Me._LMIconH.CallWSAAction(
                DirectCast(frm, Form),
                "LMI511BLF",
                "OldDataChk",
                rtDs,
                10000)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "OldDataChk")

        Return rtnDs

    End Function

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理：出荷登録先の営業所コードを取得
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList"></param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveGetBrCd(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        'DataSet設定
        '先の関連チェックにて出荷先の統一は保証されているので先頭行を代表で使用
        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_BO_MST).NewRow()
        Dim spIdx As Integer = Convert.ToInt32(chkList(0))
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("OUTKA_POSI_BU_CD") = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.OUTKA_POSI_BU_CD.ColNo)).Trim()
        rtDs.Tables(LMI511C.TABLE_NM.IN_BO_MST).Rows.Add(dr)

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "BoMst")
        rtDs = MyBase.CallWSA("LMI511BLF", "BoMst", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "BoMst")

        '取得
        Me._outkaBrCd = String.Empty
        If rtDs.Tables(LMI511C.TABLE_NM.OUT_BO_MST).Rows.Count > 0 Then
            Me._outkaBrCd = rtDs.Tables(LMI511C.TABLE_NM.OUT_BO_MST).Rows(0).Item("NRS_BO").ToString()
            If String.IsNullOrEmpty(Me._outkaBrCd) Then
                'ＪＮＣ営業所マスタから取得できない場合、ログインユーザのユーザマスタより取得
                Me._outkaBrCd = LMUserInfoManager.GetNrsBrCd()
            End If
        End If

    End Sub

    ''' <summary>
    ''' 出荷登録処理：出荷登録の対象外となる明細のチェックを外す
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList"></param>
    ''' <returns>True:チェックを外した,False:チェックを外さなかった</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveCheckOff(ByVal frm As LMI511F, ByVal chkList As ArrayList) As Boolean

        Dim flgOff As Boolean = False

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '赤黒区分
                Dim rbKbn As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.RB_KBN.ColNo)).Trim()
                If rbKbn = "1" Then
                    .SetCellValue(spIdx, _G.sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                    flgOff = True
                    Continue For
                End If
                '変更区分
                Dim modKbn As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.MOD_KBN.ColNo)).Trim()
                Select Case modKbn
                    Case "3", "9", "E", "L"
                        .SetCellValue(spIdx, _G.sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                        flgOff = True
                        Continue For
                End Select
                'NRS System取り込みフラグ
                Dim nrsSysFlg As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.NRS_SYS_FLG.ColNo)).Trim()
                Select Case nrsSysFlg
                    Case "1", "2"
                        .SetCellValue(spIdx, _G.sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                        flgOff = True
                        Continue For
                End Select
                '報告区分
                Dim rtnFlg As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.RTN_FLG.ColNo)).Trim()
                Select Case nrsSysFlg
                    Case "1", "2"
                        .SetCellValue(spIdx, _G.sprEdiListDef.DEF.ColNo, LMConst.FLG.OFF)
                        flgOff = True
                        Continue For
                End Select
            Next
        End With

        Return flgOff

    End Function

    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Function OutkaSave(ByVal frm As LMI511F, ByVal chkList As ArrayList) As Boolean

        Dim rtDs As DataSet = New LMI511DS()

        '更新用のDataSetはコピーを使用する
        Dim rtDsCopy As DataSet = rtDs.Copy()

        For liIdx As Integer = 0 To chkList.Count - 1
            Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

            '変数の初期化
            Dim targetFlg As String = String.Empty
            Dim ediCtlNo As String = String.Empty
            Dim outkaCtlNo As String = String.Empty
            Dim outFlag As String = String.Empty
            Dim exitNo As String = String.Empty

            '出荷データの更新方法を求める
            With Nothing
                'DataSet設定
                Call Me.OutkaSaveSetEdiLDataInSelect(frm, rtDs, spIdx, 0)

                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaSaveSelectEdiL")
                rtDs = MyBase.CallWSA("LMI511BLF", "OutkaSaveSelectEdiL", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaSaveSelectEdiL")

                '判定
                If rtDs.Tables(LMI511C.TABLE_NM.OUT_EDI_L).Rows.Count = 0 Then
                    targetFlg = "0"
                Else
                    ediCtlNo = rtDs.Tables(LMI511C.TABLE_NM.OUT_EDI_L).Rows(0).Item("EDI_CTL_NO").ToString()
                    outkaCtlNo = rtDs.Tables(LMI511C.TABLE_NM.OUT_EDI_L).Rows(0).Item("OUTKA_CTL_NO").ToString()
                    outFlag = rtDs.Tables(LMI511C.TABLE_NM.OUT_EDI_L).Rows(0).Item("OUT_FLAG").ToString()
                    targetFlg = IIf(outFlag = "1", "2", "1").ToString()
                End If
            End With

            '同一送受信伝票番号の存在を調べる
            With Nothing
                'DataSet設定
                Call Me.OutkaSaveSetEdiLDataInSelect(frm, rtDs, spIdx, 1)

                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaSaveSelectEdiL")
                rtDs = MyBase.CallWSA("LMI511BLF", "OutkaSaveSelectEdiL", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaSaveSelectEdiL")

                '判定
                exitNo = IIf(rtDs.Tables(LMI511C.TABLE_NM.OUT_EDI_L).Rows.Count = 0, "0", "1").ToString()
            End With

            '更新方法による分岐
            If targetFlg = "0" Then
                '登録
                With Nothing
                    '現行のヘッダーを元データとして取得
                    With Nothing
                        'DataSet設定
                        Call Me.OutkaSaveSetHedDataInSelect(frm, rtDs, spIdx)

                        'WSAクラス呼出
                        MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaSaveSelectHed")
                        rtDs = MyBase.CallWSA("LMI511BLF", "OutkaSaveSelectHed", rtDs)
                        MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaSaveSelectHed")
                    End With

                    '処理続行の判定
                    With Nothing
                        '荷届先住所コードが5桁を超えればエラーで処理終了
                        Dim destAdCd As String = rtDs.Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0).Item("DEST_AD_CD").ToString()
                        If destAdCd.Length > 5 Then
                            Call MyBase.ShowMessage(frm, "E00W", New String() {LMI511C.COMPANY_NAME})
                            Return False
                        End If

                        'NRS System取り込みフラグが特定の値ならスキップ
                        Dim nrsSysFlg As String = rtDs.Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0).Item("NRS_SYS_FLG").ToString()
                        Select Case nrsSysFlg
                            Case "1", "2", "9"
                                Continue For
                        End Select

                        '同じまとめ番号がすでに処理されていればスキップ
                        Dim combiNoA As String = rtDs.Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0).Item("COMBI_NO_A").ToString()
                        If Not String.IsNullOrEmpty(combiNoA) Then
                            For dtIdx As Integer = 0 To rtDsCopy.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Count - 1
                                If combiNoA = rtDsCopy.Tables(LMI511C.TABLE_NM.IN_HED).Rows(dtIdx).Item("COMBI_NO_A").ToString() Then
                                    Continue For
                                End If
                            Next
                        End If
                    End With

                    'ＥＤＩ出荷データＬへ登録
                    Call Me.OutkaSaveSetEdiLDataInInsert(frm, rtDs, rtDsCopy)

                    '現行の明細を元データとして取得
                    With Nothing
                        'DataSet設定
                        Call Me.OutkaSaveSetDtlDataInSelect(frm, rtDs)

                        'WSAクラス呼出
                        MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaSaveSelectDtl")
                        rtDs = MyBase.CallWSA("LMI511BLF", "OutkaSaveSelectDtl", rtDs)
                        MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaSaveSelectDtl")
                    End With

                    'ＥＤＩ出荷データＭへ登録
                    Call Me.OutkaSaveSetEdiMDataInInsert(frm, rtDs, rtDsCopy)

                    'ヘッダーを更新
                    Call Me.OutkaSaveSetHedDataInUpdate(frm, rtDs, rtDsCopy)
                End With

            ElseIf targetFlg = "1" Then
                '更新
                With Nothing
                    'データ構造的にこの状況は発生しないはず
                    '旧版ではコーディングしてあるがSQLに不具合があり実行できない、よって未使用機能と判断する
                End With
            End If
        Next

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaSave")
        rtDs = MyBase.CallWSA("LMI511BLF", "OutkaSave", rtDsCopy)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaSave")

        '印刷処理(出荷予定表)
        Call Me.Print1(frm, chkList, True)

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す(印刷プレビュー表示中の可能性あり)
            Call Search(frm, True)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 出荷登録処理：データセット設定(ＥＤＩ出荷データＬ：検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <param name="mode">0:更新方法決定用,1:同一番号調査用</param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSetEdiLDataInSelect(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer, ByVal mode As Integer)

        rtDs.Tables(LMI511C.TABLE_NM.IN_EDI_L).Clear()
        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_EDI_L).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._outkaBrCd
            '荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_L.ColNo)).Trim()
            '荷主コード(中)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_M.ColNo)).Trim()
            'EDI管理番号L
            If mode = 0 Then
                dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            End If
            '荷主注文番号（全体）
            If mode = 1 Then
                dr("CUST_ORD_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            End If
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_EDI_L).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 出荷登録処理：データセット設定(ＪＮＣＥＤＩ受信データ(ヘッダー)：検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSetHedDataInSelect(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Clear()
        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_HED).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            '入出荷区分
            dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 出荷登録処理：データセット設定(ＥＤＩ出荷データＬ：登録情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="rtDsCopy"></param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSetEdiLDataInInsert(ByVal frm As LMI511F, ByVal rtDs As DataSet, ByRef rtDsCopy As DataSet)

        With rtDsCopy
            Dim drTo As DataRow = .Tables(LMI511C.TABLE_NM.IN_EDI_L).NewRow()
            Dim drFrom As DataRow = rtDs.Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0)

            'ヘッダーの元データをベースとして登録用情報を設定する
            Dim outkaReqKbn As String = drFrom("OUTKA_REQ_KBN").ToString()
            Dim unsoEdiCtlNo As String = drFrom("UNSO_EDI_CTL_NO").ToString()
            Dim destAdCd As String = drFrom("DEST_AD_CD").ToString()

            systemNow = Now()

            drTo("DEL_KB") = "0"
            drTo("NRS_BR_CD") = Me._outkaBrCd
            drTo("EDI_CTL_NO") = drFrom("EDI_CTL_NO")
            drTo("OUTKA_CTL_NO") = drFrom("OUTKA_CTL_NO")
            Select Case outkaReqKbn
                Case "01"
                    drTo("OUTKA_KB") = "10"
                    drTo("SYUBETU_KB") = "10"
                Case "02"
                    drTo("OUTKA_KB") = "30"
                    drTo("SYUBETU_KB") = "50"
                Case "05"
                    drTo("OUTKA_KB") = "10"
                    drTo("SYUBETU_KB") = "30"
                Case Else
                    drTo("OUTKA_KB") = "10"
                    drTo("SYUBETU_KB") = "10"
            End Select
            drTo("NAIGAI_KB") = "01"
            drTo("OUTKA_STATE_KB") = "10"
            drTo("OUTKAHOKOKU_YN") = "1"
            drTo("PICK_KB") = "01"
            drTo("NRS_BR_NM") = ""
            drTo("WH_CD") = If(Not String.IsNullOrEmpty(drFrom("NRS_SOKO_CD").ToString), drFrom("NRS_SOKO_CD").ToString, LMUserInfoManager.GetWhCd)
            drTo("WH_NM") = ""
            drTo("OUTKA_PLAN_DATE") = drFrom("OUTKA_DATE_A")
            drTo("OUTKO_DATE") = ""
            drTo("ARR_PLAN_DATE") = drFrom("ARRIVAL_DATE_A")
            drTo("ARR_PLAN_TIME") = ""
            drTo("HOKOKU_DATE") = ""
            drTo("TOUKI_HOKAN_YN") = "1"
            drTo("CUST_CD_L") = drFrom("CUST_CD_L")
            drTo("CUST_CD_M") = drFrom("CUST_CD_M")
            drTo("CUST_NM_L") = ""
            drTo("CUST_NM_M") = ""
            drTo("SHIP_CD_L") = ""
            drTo("SHIP_CD_M") = ""
            drTo("SHIP_NM_L") = ""
            drTo("SHIP_NM_M") = ""
            drTo("EDI_DEST_CD") = drFrom("DEST_CD")
            drTo("DEST_CD") = drFrom("DEST_CD")
            drTo("DEST_NM") = Left(drFrom("DEST_NM").ToString(), 80)
            drTo("DEST_ZIP") = drFrom("DEST_YB_NO")
            drTo("DEST_AD_1") = Left(drFrom("DEST_AD_NM").ToString(), 40)
            drTo("DEST_AD_2") = ""
            drTo("DEST_AD_3") = ""
            drTo("DEST_AD_4") = ""
            drTo("DEST_AD_5") = ""
            drTo("DEST_TEL") = drFrom("DEST_TEL_NO")
            drTo("DEST_FAX") = drFrom("DEST_FAX_NO")
            drTo("DEST_MAIL") = ""
            drTo("DEST_JIS_CD") = IIf(String.IsNullOrEmpty(destAdCd), "", destAdCd & "0")
            drTo("SP_NHS_KB") = ""
            drTo("COA_YN") = ""
            drTo("CUST_ORD_NO") = drFrom("SR_DEN_NO")
            drTo("BUYER_ORD_NO") = drFrom("ORDER_NO")
            If String.IsNullOrEmpty(unsoEdiCtlNo) Then
                drTo("UNSO_MOTO_KB") = ""
                drTo("UNSO_TEHAI_KB") = ""
            Else
                drTo("UNSO_MOTO_KB") = "10"
                drTo("UNSO_TEHAI_KB") = "10"
            End If
            drTo("SYARYO_KB") = ""
            drTo("BIN_KB") = "01"
            drTo("UNSO_CD") = drFrom("SP_UNSO_CD")
            drTo("UNSO_NM") = drFrom("UNSOCO_NM")
            drTo("UNSO_BR_CD") = drFrom("SP_UNSO_BR_CD")
            drTo("UNSO_BR_NM") = drFrom("UNSOCO_BR_NM")
            drTo("UNCHIN_TARIFF_CD") = ""
            drTo("EXTC_TARIFF_CD") = ""
            drTo("REMARK") = drFrom("DELIVERY_NM")
            drTo("UNSO_ATT") = drFrom("DELIVERY_SAGYO")
            drTo("DENP_YN") = "1"
            drTo("PC_KB") = ""
            If String.IsNullOrEmpty(unsoEdiCtlNo) Then
                drTo("UNCHIN_YN") = "0"
            Else
                drTo("UNCHIN_YN") = "1"
            End If
            drTo("NIYAKU_YN") = "1"
            drTo("OUT_FLAG") = "0"
            drTo("AKAKURO_KB") = "0"
            drTo("JISSEKI_FLAG") = "0"
            drTo("JISSEKI_USER") = ""
            drTo("JISSEKI_DATE") = ""
            drTo("JISSEKI_TIME") = ""
            drTo("FREE_N01") = drFrom("HIS_NO")
            drTo("FREE_N02") = 0
            drTo("FREE_N03") = 0
            drTo("FREE_N04") = 0
            drTo("FREE_N05") = 0
            drTo("FREE_N06") = 0
            drTo("FREE_N07") = 0
            drTo("FREE_N08") = 0
            drTo("FREE_N09") = 0
            drTo("FREE_N10") = 0
            drTo("FREE_C01") = drFrom("EDI_CTL_NO")
            drTo("FREE_C02") = drFrom("JYUCHU_NO")
            drTo("FREE_C03") = drFrom("EM_OUTKA_KBN")
            drTo("FREE_C04") = drFrom("COMBI_NO_A")
            drTo("FREE_C05") = drFrom("DEST_NM")
            drTo("FREE_C06") = drFrom("INV_REM_NM")
            drTo("FREE_C07") = drFrom("INV_REM_KANA")
            drTo("FREE_C08") = drFrom("REMARK")
            drTo("FREE_C09") = drFrom("REMARK_KANA")
            drTo("FREE_C10") = drFrom("PRINT_NO")
            drTo("FREE_C11") = ""
            drTo("FREE_C12") = ""
            drTo("FREE_C13") = ""
            drTo("FREE_C14") = ""
            drTo("FREE_C15") = ""
            drTo("FREE_C16") = ""
            drTo("FREE_C17") = ""
            drTo("FREE_C18") = ""
            drTo("FREE_C19") = ""
            drTo("FREE_C20") = ""
            drTo("FREE_C21") = ""
            drTo("FREE_C22") = ""
            drTo("FREE_C23") = ""
            drTo("FREE_C24") = ""
            drTo("FREE_C25") = ""
            drTo("FREE_C26") = ""
            drTo("FREE_C27") = ""
            drTo("FREE_C28") = ""
            drTo("FREE_C29") = ""
            drTo("FREE_C30") = ""
            drTo("CRT_USER") = MyBase.GetUserID()
            drTo("CRT_DATE") = systemNow.ToString("yyyyMMdd")
            drTo("CRT_TIME") = systemNow.ToString("HH:mm:ss")
            drTo("UPD_USER") = ""
            drTo("UPD_DATE") = ""
            drTo("UPD_TIME") = ""
            drTo("SCM_CTL_NO_L") = ""
            drTo("EDIT_FLAG") = ""
            drTo("MATCHING_FLAG") = ""
            drTo("SYS_ENT_DATE") = ""
            drTo("SYS_ENT_TIME") = ""
            drTo("SYS_ENT_PGID") = ""
            drTo("SYS_ENT_USER") = ""
            drTo("SYS_UPD_DATE") = ""
            drTo("SYS_UPD_TIME") = ""
            drTo("SYS_UPD_PGID") = ""
            drTo("SYS_UPD_USER") = ""
            drTo("SYS_DEL_FLG") = "0"

            .Tables(LMI511C.TABLE_NM.IN_EDI_L).Rows.Add(drTo)
        End With

    End Sub

    ''' <summary>
    ''' 出荷登録処理：データセット設定(ＪＮＣＥＤＩ受信データ(明細)：検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSetDtlDataInSelect(ByVal frm As LMI511F, ByRef rtDs As DataSet)

        With rtDs
            .Tables(LMI511C.TABLE_NM.IN_HED).Clear()
            Dim drTo As DataRow = .Tables(LMI511C.TABLE_NM.IN_HED).NewRow()
            Dim drFrom As DataRow = .Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0)

            '営業所コード
            drTo("NRS_BR_CD") = drFrom("NRS_BR_CD")
            '積み合せ番号【報告】
            drTo("COMBI_NO_A") = drFrom("COMBI_NO_A")
            'EDI管理番号
            If String.IsNullOrEmpty(drTo("COMBI_NO_A").ToString()) Then
                drTo("EDI_CTL_NO") = drFrom("EDI_CTL_NO")
            End If

            .Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(drTo)
        End With

    End Sub

    ''' <summary>
    ''' 出荷登録処理：データセット設定(ＥＤＩ出荷データＭ：登録情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="rtDsCopy"></param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSetEdiMDataInInsert(ByVal frm As LMI511F, ByVal rtDs As DataSet, ByRef rtDsCopy As DataSet)

        With rtDsCopy
            Dim ediCtlnoChu As Integer = 0

            'EDIデータ作成日時
            Dim systemDate As String = systemNow.ToString("yyyyMMdd")
            Dim systemTime As String = systemNow.ToString("HH:mm:ss")

            For dtIdx As Integer = 0 To rtDs.Tables(LMI511C.TABLE_NM.OUT_DTL_EDI).Rows.Count - 1
                Dim drTo As DataRow = .Tables(LMI511C.TABLE_NM.IN_EDI_M).NewRow()
                Dim drFrom As DataRow = rtDs.Tables(LMI511C.TABLE_NM.OUT_DTL_EDI).Rows(dtIdx)

                '明細の元データをベースとして登録用情報を設定する
                Dim ediCtlNo As String = rtDs.Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0).Item("EDI_CTL_NO").ToString()
                Dim meiRem1 As String = drFrom("MEI_REM1").ToString()

                drTo("DEL_KB") = "0"
                drTo("NRS_BR_CD") = Me._outkaBrCd
                drTo("EDI_CTL_NO") = ediCtlNo
                ediCtlnoChu += 1
                drTo("EDI_CTL_NO_CHU") = ediCtlnoChu
                drTo("OUTKA_CTL_NO") = ""
                drTo("OUTKA_CTL_NO_CHU") = ""
                Select Case meiRem1
                    Case "Ｓ", "＊Ｓ"
                        drTo("COA_YN") = "1"
                    Case Else
                        drTo("COA_YN") = ""
                End Select
                drTo("CUST_ORD_NO_DTL") = drFrom("SR_DEN_NO")
                drTo("BUYER_ORD_NO_DTL") = drFrom("ORDER_NO")
                drTo("CUST_GOODS_CD") = drFrom("JYUCHU_GOODS_CD")
                drTo("NRS_GOODS_CD") = ""
                drTo("GOODS_NM") = drFrom("GOODS_KANA2")
                drTo("RSV_NO") = ""
                drTo("LOT_NO") = drFrom("LOT_NO_A")
                drTo("SERIAL_NO") = ""
                drTo("ALCTD_KB") = "02"
                drTo("OUTKA_PKG_NB") = 0
                drTo("OUTKA_HASU") = Fix(CInt(drFrom("SURY_REQ")) / CInt(drFrom("IRISUU")))
                drTo("OUTKA_QT") = drFrom("SURY_REQ")
                drTo("OUTKA_TTL_NB") = Fix(CInt(drFrom("SURY_REQ")) / CInt(drFrom("IRISUU")))
                drTo("OUTKA_TTL_QT") = drFrom("SURY_REQ")
                drTo("KB_UT") = ""
                drTo("QT_UT") = ""
                drTo("PKG_NB") = 0
                drTo("PKG_UT") = ""
                drTo("ONDO_KB") = ""
                drTo("UNSO_ONDO_KB") = ""
                drTo("IRIME") = drFrom("IRISUU")
                drTo("IRIME_UT") = drFrom("SURY_TANI_CD")
                drTo("BETU_WT") = 0
                drTo("REMARK") = ""
                drTo("OUT_KB") = 0
                drTo("AKAKURO_KB") = 0
                drTo("JISSEKI_FLAG") = 0
                drTo("JISSEKI_USER") = ""
                drTo("JISSEKI_DATE") = ""
                drTo("JISSEKI_TIME") = ""
                drTo("SET_KB") = ""
                drTo("FREE_N01") = drFrom("HIS_NO")
                drTo("FREE_N02") = 0
                drTo("FREE_N03") = 0
                drTo("FREE_N04") = 0
                drTo("FREE_N05") = 0
                drTo("FREE_N06") = 0
                drTo("FREE_N07") = 0
                drTo("FREE_N08") = 0
                drTo("FREE_N09") = 0
                drTo("FREE_N10") = 0
                drTo("FREE_C01") = drFrom("EDI_CTL_NO")
                drTo("FREE_C02") = drFrom("EDI_CTL_NO_CHU")
                drTo("FREE_C03") = drFrom("JYUCHU_NO")
                drTo("FREE_C04") = drFrom("MEI_NO_A")
                drTo("FREE_C05") = drFrom("GOODS_KANA1")
                drTo("FREE_C06") = drFrom("GOODS_NM")
                drTo("FREE_C07") = drFrom("MEI_REM1")
                drTo("FREE_C08") = drFrom("MEI_REM2")
                drTo("FREE_C09") = ""
                drTo("FREE_C10") = ""
                drTo("FREE_C11") = ""
                drTo("FREE_C12") = ""
                drTo("FREE_C13") = ""
                drTo("FREE_C14") = ""
                drTo("FREE_C15") = ""
                drTo("FREE_C16") = ""
                drTo("FREE_C17") = ""
                drTo("FREE_C18") = ""
                drTo("FREE_C19") = ""
                drTo("FREE_C20") = ""
                drTo("FREE_C21") = ""
                drTo("FREE_C22") = ""
                drTo("FREE_C23") = ""
                drTo("FREE_C24") = ""
                drTo("FREE_C25") = ""
                drTo("FREE_C26") = ""
                drTo("FREE_C27") = ""
                drTo("FREE_C28") = ""
                drTo("FREE_C29") = ""
                drTo("FREE_C30") = ""
                drTo("CRT_USER") = MyBase.GetUserID()
                drTo("CRT_DATE") = systemDate
                drTo("CRT_TIME") = systemTime
                drTo("UPD_USER") = ""
                drTo("UPD_DATE") = ""
                drTo("UPD_TIME") = ""
                drTo("SCM_CTL_NO_L") = ""
                drTo("SCM_CTL_NO_M") = ""
                drTo("SYS_ENT_DATE") = ""
                drTo("SYS_ENT_TIME") = ""
                drTo("SYS_ENT_PGID") = ""
                drTo("SYS_ENT_USER") = ""
                drTo("SYS_UPD_DATE") = ""
                drTo("SYS_UPD_TIME") = ""
                drTo("SYS_UPD_PGID") = ""
                drTo("SYS_UPD_USER") = ""
                drTo("SYS_DEL_FLG") = "0"

                .Tables(LMI511C.TABLE_NM.IN_EDI_M).Rows.Add(drTo)
            Next
        End With

    End Sub

    ''' <summary>
    ''' 出荷登録処理：データセット設定(ＪＮＣＥＤＩ受信データ(ヘッダー)：更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="rtDsCopy"></param>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSetHedDataInUpdate(ByVal frm As LMI511F, ByVal rtDs As DataSet, ByRef rtDsCopy As DataSet)

        With rtDsCopy
            Dim drTo As DataRow = .Tables(LMI511C.TABLE_NM.IN_HED).NewRow()
            Dim drFrom As DataRow = rtDs.Tables(LMI511C.TABLE_NM.OUT_HED_EDI).Rows(0)

            Dim combiNoA As String = drFrom("COMBI_NO_A").ToString()

            '営業所コード
            drTo("NRS_BR_CD") = drFrom("NRS_BR_CD")
            'EDI管理番号
            If String.IsNullOrEmpty(combiNoA) Then
                drTo("EDI_CTL_NO") = drFrom("EDI_CTL_NO")
            End If
            '積み合せ番号【報告】
            drTo("COMBI_NO_A") = combiNoA
            'NRS System取り込みフラグ
            drTo("NRS_SYS_FLG") = "1"
            '更新日
            drTo("SYS_UPD_DATE") = drFrom("SYS_UPD_DATE")
            '更新時刻
            drTo("SYS_UPD_TIME") = drFrom("SYS_UPD_TIME")

            .Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(drTo)
        End With

    End Sub

#End Region

#Region "編集処理"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <remarks></remarks>
    Private Sub Edit(ByVal frm As LMI511F, ByVal spIdx As Integer)

        '処理モードの設定
        Me._Mode = LMI511C.Mode.EDT

        'スプレッドの編集状態を制御
        Call Me._G.SetSpreadEdit(Me._Mode, spIdx)

        '画面の編集前値を退避
        Call backupEditBefore(frm, spIdx)

        'ロックを解除する
        Call Me._G.UnLockedForm(Me._Mode)

        '処理終了アクション
        Call EndAction2(frm, , True)

        '終了処理アクション後の画面制御
        Call _G.SetControlsStatus(Me._Mode)
        Call _G.SetSpreadEdit(LMI511C.Mode.EDT, Me._nowRow, True)

    End Sub

#End Region

#Region "まとめ指示処理"

    ''' <summary>
    ''' まとめ指示処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MtmSave(ByVal frm As LMI511F)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            '届先のグループ情報を作成
            Dim DestGroup As List(Of stDestGroup) = New List(Of stDestGroup)
            For spIdx As Integer = 1 To .ActiveSheet.Rows.Count - 1
                '項目取得
                Dim check As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.DEF.ColNo)).Trim()
                Dim destCd As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.DEST_CD.ColNo)).Trim()

                '処理済みの届先があれば更新
                Dim exist As Boolean = False
                For liIdx As Integer = 0 To DestGroup.Count - 1
                    If destCd = DestGroup(liIdx).destCd Then
                        Dim updList As stDestGroup = DestGroup(liIdx)
                        updList.Cnt += 1
                        updList.CheckCnt += Convert.ToInt32(IIf(check = LMConst.FLG.ON, 1, 0))
                        DestGroup(liIdx) = updList
                        exist = True
                        Exit For
                    End If
                Next

                '処理済みの届先がなければ追加
                If Not exist Then
                    Dim newlist As stDestGroup = New stDestGroup
                    newlist.destCd = destCd
                    newlist.Cnt = 1
                    newlist.CheckCnt = Convert.ToInt32(IIf(check = LMConst.FLG.ON, 1, 0))
                    DestGroup.Add(newlist)
                End If
            Next

            '更新用のDataSetを作成
            For spIdx As Integer = 1 To .ActiveSheet.Rows.Count - 1
                '項目取得
                Dim check As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.DEF.ColNo)).Trim()
                Dim destCd As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.DEST_CD.ColNo)).Trim()
                Dim combiNo As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.COMBI_NO.ColNo)).Trim()
                Dim nrsBrCd As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()

                '届先が同一のグループ情報を探す
                Dim liIdx As Integer = 0
                For liIdx = 0 To DestGroup.Count - 1
                    If destCd = DestGroup(liIdx).destCd Then
                        Exit For
                    End If
                Next

                '更新内容の決定
                If check = LMConst.FLG.ON Then
                    'チェックされている
                    If (DestGroup(liIdx).CheckCnt = 1) And (Not String.IsNullOrEmpty(combiNo)) Then
                        'チェック件数が1で画面のまとめ番号に値あり
                        With Nothing
                            'まとめ番号のクリア更新
                            Call Me.MtmSaveSetHedDataIn(frm, rtDs, spIdx, String.Empty)
                        End With

                    ElseIf DestGroup(liIdx).CheckCnt > 1 Then
                        'チェック件数が1より多い
                        With Nothing
                            'まとめ番号が未決定ならば採番しグループ情報を更新
                            If String.IsNullOrEmpty(DestGroup(liIdx).combiNo) Then
                                '採番用DataSet設定
                                rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).Clear()
                                Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).NewRow()
                                dr("NRS_BR_CD") = nrsBrCd
                                rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).Rows.Add(dr)

                                'WSAクラス呼出
                                MyBase.Logger.StartLog(MyBase.GetType.Name, "GetNewCombiNo")
                                rtDs = MyBase.CallWSA("LMI511BLF", "GetNewCombiNo", rtDs)
                                MyBase.Logger.EndLog(MyBase.GetType.Name, "GetNewCombiNo")

                                '採番値をグループ情報に設定
                                Dim updList As stDestGroup = DestGroup(liIdx)
                                updList.combiNo = rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).Rows(0).Item("NEW_CD").ToString()
                                DestGroup(liIdx) = updList
                            End If

                            'まとめ番号の上書き更新
                            Call Me.MtmSaveSetHedDataIn(frm, rtDs, spIdx, DestGroup(liIdx).combiNo)
                        End With
                    End If

                Else
                    'チェックされていない
                    If Not String.IsNullOrEmpty(combiNo) Then
                        '画面のまとめ番号に値あり
                        With Nothing
                            'まとめ番号のクリア更新
                            Call Me.MtmSaveSetHedDataIn(frm, rtDs, spIdx, String.Empty)
                        End With
                    End If
                End If
            Next

        End With

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MtmSave")
        rtDs = MyBase.CallWSA("LMI511BLF", "MtmSave", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MtmSave")

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            'まとめ候補検索処理を呼び出す
            Call MtmSearch(frm)
        End If

    End Sub

    ''' <summary>
    ''' まとめ指示処理：データセット設定(ヘッダー：更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <param name="combiNo">まとめ番号</param>
    ''' <remarks></remarks>
    Private Sub MtmSaveSetHedDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer, ByVal combiNo As String)

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_HED).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            '入出荷区分
            dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
            '積み合せ番号【報告】
            dr("COMBI_NO_A") = combiNo
            '更新日
            dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_DATE_HED.ColNo)).Trim()
            '更新時刻
            dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_TIME_HED.ColNo)).Trim()
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(dr)

    End Sub

#End Region

#Region "まとめ解除処理"

    ''' <summary>
    ''' まとめ解除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub MtmCancel(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '更新内容の決定
                Dim combiNo As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.COMBI_NO.ColNo)).Trim()
                If Not String.IsNullOrEmpty(combiNo) Then
                    '画面のまとめ番号に値あり
                    With Nothing
                        'まとめ番号のクリア更新
                        Call Me.MtmCancelSetHedDataIn(frm, rtDs, spIdx)
                    End With
                End If
            Next
        End With

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MtmCancel")
        rtDs = MyBase.CallWSA("LMI511BLF", "MtmCancel", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MtmCancel")

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            If Me._Mode = LMI511C.Mode.MTM Then
                'まとめ候補検索処理を呼び出す
                Call MtmSearch(frm)
            Else
                '検索処理を呼び出す
                Call Search(frm)
            End If
        End If

    End Sub

    ''' <summary>
    ''' まとめ解除処理：データセット設定(ヘッダー：更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub MtmCancelSetHedDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        'まとめ指示処理と同様につき利用
        Call MtmSaveSetHedDataIn(frm, rtDs, spIdx, String.Empty)

    End Sub

#End Region

#Region "送信要求処理"

    ''' <summary>
    ''' 送信要求処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub SndReq(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '更新内容の決定
                Dim rtnFlg As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.RTN_FLG.ColNo)).Trim()
                If rtnFlg <> LMI511C.RTN_FLG.COMP Then
                    '報告区分が完了でない
                    With Nothing
                        '更新
                        Call Me.SndReqSetHedDataIn(frm, rtDs, spIdx, LMI511C.RTN_FLG.WAIT)
                    End With
                End If
            Next
        End With

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SndReq")
        rtDs = MyBase.CallWSA("LMI511BLF", "SndReq", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SndReq")

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す
            Call Search(frm)
        End If

    End Sub

    ''' <summary>
    ''' 送信要求処理：データセット設定(ヘッダー：更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <param name="rtnFlg">報告区分</param>
    ''' <remarks></remarks>
    Private Sub SndReqSetHedDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer, ByVal rtnFlg As String)

        With frm.sprEdiList.ActiveSheet
            '変更区分が{"3", "L", "E"}はスキップ
            Dim modKbn As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.MOD_KBN.ColNo)).Trim()
            If {"3", "L", "E"}.Contains(modKbn) Then
                Exit Sub
            End If
        End With

        With frm.sprEdiList.ActiveSheet
            '同じ送受信伝票番号がすでに追加されていればスキップ
            Dim srDenNo As String = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            For dtIdx As Integer = 0 To rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Count - 1
                If srDenNo = rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows(dtIdx).Item("SR_DEN_NO").ToString() Then
                    Exit Sub
                End If
            Next
        End With

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_HED).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            '入出荷区分
            dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
            '送受信伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '報告区分
            dr("RTN_FLG") = rtnFlg
            '更新日
            dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_DATE_HED.ColNo)).Trim()
            '更新時刻
            dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_TIME_HED.ColNo)).Trim()
#If True Then   'ADD 2020/08/28 013087   【LMS】JNC EDI　改修
            '運送データEDI_CTL_NO
            dr("UNSO_EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO_UHD.ColNo)).Trim()
            '実出荷数量(積み)
            dr("SURY_RPT") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT.ColNo)).Trim()
            '実出荷数量(卸し)
            dr("SURY_RPT_UNSO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT_UNSO.ColNo)).Trim()
            '出庫・輸送RTN_FLG設定
            '出庫RTN_FLG（入庫）
            dr("OUT_RTN_FLG") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.RTN_FLG.ColNo)).Trim()
            '輸送RTN_FLG
            dr("UNSO_RTN_FLG") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.UNSO_RTN_FLG.ColNo)).Trim()
#End If
            'EDI_CTL_NO_CHU_UDL UNSO_EDI_CTL_NO
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(dr)

    End Sub

#End Region

#Region "報告訂正処理"

    ''' <summary>
    ''' 報告訂正処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <remarks></remarks>
    Private Sub Revision(ByVal frm As LMI511F, ByVal spIdx As Integer)

        '処理モードの設定
        Me._Mode = LMI511C.Mode.REV

        'スプレッドの編集状態を制御
        Call Me._G.SetSpreadEdit(Me._Mode, spIdx)

        'ロックを解除する
        Call Me._G.UnLockedForm(Me._Mode)

        '処理終了アクション
        Call EndAction2(frm, , True)

        '終了処理アクション後の画面制御
        Call _G.SetControlsStatus(Me._Mode)
        Call _G.SetSpreadEdit(LMI511C.Mode.REV, Me._nowRow, True)

    End Sub

#End Region

#Region "送信取消処理"

    ''' <summary>
    ''' 送信取消処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub SndCancel(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '更新内容の決定
                Dim rtnFlg As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.RTN_FLG.ColNo)).Trim()
                If rtnFlg <> LMI511C.RTN_FLG.COMP Then
                    '報告区分が完了でない
                    With Nothing
                        '更新
                        Call Me.SndCancelSetHedDataIn(frm, rtDs, spIdx)
                    End With
                End If
#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
                '運送更新内容の決定
                Dim UnsortnFlg As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.UNSO_RTN_FLG.ColNo)).Trim()
                If UnsortnFlg <> LMI511C.RTN_FLG.COMP Then
                    '報告区分が完了でない
                    With Nothing
                        '更新
                        Call Me.SndCancelSetHedDataIn(frm, rtDs, spIdx)
                    End With
                End If
#End If
            Next
        End With

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SndCancel")
        rtDs = MyBase.CallWSA("LMI511BLF", "SndCancel", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SndCancel")

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す
            Call Search(frm)
        End If

    End Sub

    ''' <summary>
    ''' 送信取消処理：データセット設定(ヘッダー：更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub SndCancelSetHedDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        '送信要求処理と同様につき利用
        Call SndReqSetHedDataIn(frm, rtDs, spIdx, LMI511C.RTN_FLG.MISO)

    End Sub

#End Region

#Region "まとめ候補検索処理"

    ''' <summary>
    ''' まとめ候補検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MtmSearch(ByVal frm As LMI511F)

        '強制実行フラグをオフ
        MyBase.SetForceOparation(False)

        'スプレッドシート初期化
        frm.sprEdiList.CrearSpread()

        '検索条件をDataSetに設定
        Dim rtDs As DataSet = New LMI511DS()
        If Not Me.MtmSearchSetDataIn(frm, rtDs) Then
            MyBase.ShowMessage(frm, "E361")
            'Me._LMIconV.SetErrorControl(frm.txtCustCD_L)
            Me._LMIconV.SetErrorControl(frm.cmbEigyo)
            Exit Sub
        End If

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MtmSearch")
        Dim rtnDs As DataSet = Me._LMIconH.CallWSAAction(
                DirectCast(frm, Form),
                "LMI511BLF",
                "MtmSearch",
                rtDs,
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))),
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MtmSearch")

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        '処理モードの設定
        Me._Mode = LMI511C.Mode.MTM

        'ロックを解除する
        Call Me._G.UnLockedForm(Me._Mode)

        ''キャッシュから名称取得
        'Call Me.SetCachedName(frm)

        'フォーカスの設定
        Call Me._G.SetFocusDetail()

        '処理終了アクション
        Call EndAction2(frm)

        '終了処理アクション後の画面制御
        Call _G.SetControlsStatus(Me._Mode)
        For i As Integer = 1 To frm.sprEdiList.ActiveSheet.Rows.Count - 1
            Call _G.SetSpreadEdit(LMI511C.Mode.REF, i, True)
        Next

    End Sub

    ''' <summary>
    ''' まとめ候補検索処理：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MtmSearchSetDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet) As Boolean

        '検索処理と同様につき利用
        Return SearchSetDataIn(frm, rtDs)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="previewFlg">True:印刷プレビューを表示中</param>
    ''' <remarks></remarks>
    Private Sub Search(ByVal frm As LMI511F, Optional ByVal previewFlg As Boolean = False)

        '強制実行フラグをオフ
        MyBase.SetForceOparation(False)

        'スプレッドシート初期化
        frm.sprEdiList.CrearSpread()

        '検索条件をDataSetに設定
        Dim rtDs As DataSet = New LMI511DS()
        If Not Me.SearchSetDataIn(frm, rtDs) Then
            MyBase.ShowMessage(frm, "E361")
            'Me._LMIconV.SetErrorControl(frm.txtCustCD_L)
            Me._LMIconV.SetErrorControl(frm.cmbEigyo)
            Exit Sub
        End If

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Search")
        Dim rtnDs As DataSet = Me._LMIconH.CallWSAAction(
                DirectCast(frm, Form),
                "LMI511BLF",
                "Search",
                rtDs,
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))),
                Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))
        MyBase.Logger.EndLog(MyBase.GetType.Name, "Search")

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        '処理モードの設定
        Me._Mode = LMI511C.Mode.REF

        'ロックを解除する
        Call Me._G.UnLockedForm(Me._Mode)

        ''キャッシュから名称取得
        'Call Me.SetCachedName(frm)

        'フォーカスの設定
        If Not previewFlg Then
            '印刷プレビュー表示中以外
            Call Me._G.SetFocusDetail()
        End If

        '処理終了アクション
        Call EndAction2(frm)

        '終了処理アクション後の画面制御
        Call _G.SetControlsStatus(Me._Mode)
        For i As Integer = 1 To frm.sprEdiList.ActiveSheet.Rows.Count - 1
            Call _G.SetSpreadEdit(LMI511C.Mode.REF, i, True)
        Next

    End Sub

    ''' <summary>
    ''' 検索処理：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SearchSetDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_SEARCH).NewRow()

        'ヘッダー部
        With frm
            '営業所コード
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            '荷主コード(大)
            'dr("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            dr("CUST_CD_L") = String.Empty
            '荷主コード(中)
            'dr("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()
            dr("CUST_CD_M") = String.Empty
            '出荷先
            dr("OUTKA_POSI_BU_CD") = .cmbOutkaPosi.SelectedValue
            'EDI取込日FROM
            dr("EDI_DATE_FROM") = .imdEdiDateFrom.TextValue
            'EDI取込日TO
            dr("EDI_DATE_TO") = .imdEdiDateTo.TextValue
            '検索日区分／検索日FROM／検索日TO
            If .cmbSelectDate.SelectedValue.ToString = LMI511C.SELECT_DATE.OUTKA Or .cmbSelectDate.SelectedValue.ToString = LMI511C.SELECT_DATE.INKA Then
                dr("SEARCH_DATE_KBN") = .cmbSelectDate.SelectedValue
                dr("SEARCH_DATE_FROM") = .imdSearchDateFrom.TextValue
                dr("SEARCH_DATE_TO") = .imdSearchDateTo.TextValue
            End If
            '入出庫区分
            If .optSyuko.Checked Then
                dr("INOUT_KB") = LMI511C.INOUT_KB.OUTKA
            Else
                dr("INOUT_KB") = LMI511C.INOUT_KB.INKA
            End If

#Region "表示区分"
            '消 × を非表示
            dr("HYOJI_KBN_1") = .chkHide1.GetBinaryValue.ToString().Replace("0", "")
            '赤データ非表示
            dr("HYOJI_KBN_2") = .chkHide2.GetBinaryValue.ToString().Replace("0", "")
#End Region

#Region "除外検索処理"
            '商品名A
            dr("GOODS_NM_A") = .txtExclusionA.TextValue
            '商品名B 
            dr("GOODS_NM_B") = .txtExclusionB.TextValue
            '商品名C
            dr("GOODS_NM_C") = .txtExclusionC.TextValue
#End Region

        End With

        'スプレッドシート
        With frm.sprEdiList.ActiveSheet
            '赤黒
            dr("RB_KBN") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.RB_KBN_NM.ColNo)).Trim()
            '変更
            dr("MOD_KBN") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.MOD_KBN_NM.ColNo)).Trim()
            '報告
            dr("RTN_FLG") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.RTN_FLG_NM.ColNo)).Trim()
            '送信訂正
            dr("SND_CANCEL_FLG") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.SND_CANCEL_FLG_NM.ColNo)).Trim()
            '印刷
            dr("PRTFLG") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.PRTFLG_NM.ColNo)).Trim()
            '専門印刷
            dr("PRTFLG_SUB") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.PRTFLG_SUB_NM.ColNo)).Trim()
            '取込
            dr("NRS_SYS_FLG") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.NRS_SYS_FLG_NM.ColNo)).Trim()
            '積合
            dr("COMBI") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.COMBI.ColNo)).Trim()
            '運送
            dr("UNSO_REQ_YN") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.UNSO_REQ_YN_NM.ColNo)).Trim()
            '伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '届先名
            dr("DEST_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.DEST_NM.ColNo)).Trim()
            '届先住所
            dr("DEST_AD_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.DEST_AD_NM.ColNo)).Trim()
            '運送会社名
            Dim actUnsoCd As String = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.ACT_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(actUnsoCd) Then
                actUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", actUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            dr("ACT_UNSO_CD") = actUnsoCd
            '運送会社名(控)
            Dim memoUnsoCd As String = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.MEMO_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(memoUnsoCd) Then
                memoUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", memoUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            dr("UNSO_CD_MEMO") = memoUnsoCd
            '商品名
            dr("GOODS_NM") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.GOODS_NM.ColNo)).Trim()
            '車両番号
            dr("CAR_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.CAR_NO.ColNo)).Trim()
            '受注番号
            dr("JYUCHU_NO") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.JYUCHU_NO.ColNo)).Trim()
#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
            '報告
            dr("UNSO_RTN_FLG") = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.UNSO_RTN_FLG_NM.ColNo)).Trim()
            Dim actUnsoRouteCd As String = Me._LMIconV.GetCellValue(.Cells(0, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(actUnsoRouteCd) Then
                actUnsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", actUnsoRouteCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            dr("UNSO_ROUTE_CD") = actUnsoRouteCd
#End If

        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_SEARCH).Rows.Add(dr)

        Return True

    End Function

#End Region

#Region "保存処理(編集)"

    ''' <summary>
    ''' 保存処理(編集)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SaveEdit(ByVal frm As LMI511F)

        '更新情報をDataSetに設定
        Dim rtDs As DataSet = New LMI511DS()
        If Not Me.SaveEditSetDataIn(frm, rtDs, Me._nowRow) Then
            MyBase.ShowMessage(frm, "E361")
            'Me._LMIconV.SetErrorControl(frm.txtCustCD_L)
            Me._LMIconV.SetErrorControl(frm.cmbEigyo)
            Exit Sub
        End If

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEdit")
        rtDs = MyBase.CallWSA("LMI511BLF", "SaveEdit", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEdit")

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す
            Call Search(frm)
        End If

    End Sub

    ''' <summary>
    ''' 保存処理(編集)：データセット設定(更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEditSetDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer) As Boolean

        Dim unsoRouteCd As String = String.Empty
        Dim actUnsoCd As String = String.Empty
        Dim memoUnsoCd As String = String.Empty

        'ＪＮＣＥＤＩ受信データ(ヘッダー)
        With frm.sprEdiList.ActiveSheet
            Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_HED).NewRow()

            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            '出荷予定日
            dr("OUTKA_DATE_A") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_DATE.ColNo)).Trim()
            '入出荷区分
            dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
            '運送手段【報告】
            unsoRouteCd = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.UNSO_ROUTE_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(unsoRouteCd) Then
                unsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", unsoRouteCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            dr("UNSO_ROUTE_A") = unsoRouteCd
            '車両番号【報告】
            dr("CAR_NO_A") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CAR_NO.ColNo)).Trim()
            '実運送会社コード
            actUnsoCd = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.ACT_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(actUnsoCd) Then
                actUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", actUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            dr("ACT_UNSO_CD") = actUnsoCd
            '実運送会社コード(控)
            memoUnsoCd = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.MEMO_UNSO_NM.ColNo)).Trim()
            If Not String.IsNullOrEmpty(memoUnsoCd) Then
                memoUnsoCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J016' AND KBN_CD = '", memoUnsoCd, "' "))(0).Item("KBN_NM2").ToString()
            End If
            dr("UNSO_CD_MEMO") = memoUnsoCd
            '更新日
            dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_DATE_HED.ColNo)).Trim()
            '更新時刻
            dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_TIME_HED.ColNo)).Trim()

            rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(dr)
        End With

        'ＪＮＣＥＤＩ受信データ(明細)
        With frm.sprEdiList.ActiveSheet
            Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_DTL).NewRow()

            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            '入出荷区分
            dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'EDI管理番号中番
            dr("EDI_CTL_NO_CHU") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO_CHU.ColNo)).Trim()
            '数量(報告)
            dr("SURY_RPT") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT.ColNo)).Trim()
            '更新日
            dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_DATE_DTL.ColNo)).Trim()
            '更新時刻
            dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_TIME_DTL.ColNo)).Trim()

            rtDs.Tables(LMI511C.TABLE_NM.IN_DTL).Rows.Add(dr)
        End With

        '運送用
        If frm.optSyuko.Checked Then
            Dim ediCtlNoUhd As String = Me._LMIconV.GetCellValue(frm.sprEdiList.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO_UHD.ColNo)).Trim()
            If Not String.IsNullOrEmpty(ediCtlNoUhd) Then
                'ＪＮＣＥＤＩ受信データ(ヘッダー)
                With frm.sprEdiList.ActiveSheet
                    Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_HED).NewRow()

                    '営業所コード
                    dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
                    'EDI管理番号
                    dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO_UHD.ColNo)).Trim()
                    'データ種別
                    dr("DATA_KIND") = LMI511C.DATA_KIND.UNSO
                    '入出荷区分
                    dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
                    '運送手段【報告】
                    dr("UNSO_ROUTE_A") = unsoRouteCd
                    '車両番号
                    dr("CAR_NO_A") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CAR_NO.ColNo)).Trim()
                    '実運送会社コード
                    dr("ACT_UNSO_CD") = actUnsoCd
                    '更新日
                    dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_DATE_UHD.ColNo)).Trim()
                    '更新時刻
                    dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_TIME_UHD.ColNo)).Trim()

                    rtDs.Tables(LMI511C.TABLE_NM.IN_HED).Rows.Add(dr)
                End With

                'ＪＮＣＥＤＩ受信データ(明細)
                With frm.sprEdiList.ActiveSheet
                    Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_DTL).NewRow()

                    '営業所コード
                    dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
                    '入出荷区分
                    dr("INOUT_KB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.INOUT_KB.ColNo)).Trim()
                    'EDI管理番号
                    dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO_UHD.ColNo)).Trim()
                    'EDI管理番号中番
                    dr("EDI_CTL_NO_CHU") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO_CHU_UDL.ColNo)).Trim()
                    '数量(報告)
                    dr("SURY_RPT") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SURY_RPT_UNSO.ColNo)).Trim()
                    '更新日
                    dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_DATE_UDL.ColNo)).Trim()
                    '更新時刻
                    dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SYS_UPD_TIME_UDL.ColNo)).Trim()

                    rtDs.Tables(LMI511C.TABLE_NM.IN_DTL).Rows.Add(dr)
                End With
            End If
        End If

        Return True

    End Function

#End Region

#Region "保存処理(訂正)"

    ''' <summary>
    ''' 保存処理(訂正)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SaveRevision(ByVal frm As LMI511F)

        '更新情報をDataSetに設定
        Dim rtDs As DataSet = New LMI511DS()
        If Not Me.SaveRevisionSetDataIn(frm, rtDs, Me._nowRow) Then
            MyBase.ShowMessage(frm, "E361")
            'Me._LMIconV.SetErrorControl(frm.txtCustCD_L)
            Me._LMIconV.SetErrorControl(frm.cmbEigyo)
            Exit Sub
        End If

        'WSAクラス呼出
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveRevision")
        rtDs = MyBase.CallWSA("LMI511BLF", "SaveRevision", rtDs)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveRevision")

        '終了処理
        If MyBase.IsErrorMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す
            Call Search(frm)
        End If

    End Sub

    ''' <summary>
    ''' 保存処理(訂正)：データセット設定(更新情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSetDataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer) As Boolean

        '保存処理(編集)と同様につき利用
        Return SaveEditSetDataIn(frm, rtDs, spIdx)

    End Function

#End Region

#Region "マスタ参照処理"

    '''' <summary>
    '''' マスタ参照処理
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Private Sub ShowPopup(ByVal frm As LMI511F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMI511C.EventShubetsu)

    '    'オブジェクト名による分岐
    '    Select Case objNM
    '        Case frm.txtCustCD_L.Name, frm.txtCustCD_M.Name
    '            '荷主コード(大)／荷主コード(中)
    '            Dim prmDs As DataSet = New LMZ260DS
    '            Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
    '            row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
    '            If eventShubetsu = LMI511C.EventShubetsu.ENTER Then
    '                row("CUST_CD_L") = frm.txtCustCD_L.TextValue
    '                row("CUST_CD_M") = frm.txtCustCD_M.TextValue
    '            End If
    '            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
    '            row("HYOJI_KBN") = LMZControlC.HYOJI_S
    '            prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
    '            prm.ParamDataSet = prmDs
    '            prm.SkipFlg = Me._PopupSkipFlg
    '            LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)
    '    End Select

    '    '戻り処理
    '    If prm.ReturnFlg Then
    '        Select Case objNM
    '            Case frm.txtCustCD_L.Name, frm.txtCustCD_M.Name
    '                '荷主コード(大)／荷主コード(中)
    '                With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
    '                    frm.txtCustCD_L.TextValue = .Item("CUST_CD_L").ToString()
    '                    frm.txtCustCD_M.TextValue = .Item("CUST_CD_M").ToString()
    '                    frm.lblCustNM_L.TextValue = .Item("CUST_NM_L").ToString()
    '                    frm.lblCustNM_M.TextValue = .Item("CUST_NM_M").ToString()
    '                End With
    '        End Select
    '    End If

    'End Sub

#End Region

#Region "印刷処理(出荷予定表)"

    ''' <summary>
    ''' 印刷処理(出荷予定表)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <param name="outkaSave">True:出荷登録処理からの呼び出し,False:通常の印刷実行</param>
    ''' <remarks></remarks>
    Private Sub Print1(ByVal frm As LMI511F, ByVal chkList As ArrayList, Optional ByVal outkaSave As Boolean = False)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '項目取得
                Dim nrsBrCd As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
                Dim prtFlg As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.PRTFLG.ColNo)).Trim()
                Dim printNo As String = Me._LMIconV.GetCellValue(.ActiveSheet.Cells(spIdx, _G.sprEdiListDef.PRINT_NO.ColNo)).Trim()

                'プリントフラグによる判定
                If prtFlg = LMI511C.JISSI_KBN.MI Then
                    '印刷：未
                    With Nothing
                        '印刷番号の採番用DataSet設定
                        rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).Clear()
                        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).NewRow()
                        dr("NRS_BR_CD") = nrsBrCd
                        rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).Rows.Add(dr)

                        'WSAクラス呼出
                        MyBase.Logger.StartLog(MyBase.GetType.Name, "GetNewPrintNo")
                        rtDs = MyBase.CallWSA("LMI511BLF", "GetNewPrintNo", rtDs)
                        MyBase.Logger.EndLog(MyBase.GetType.Name, "GetNewPrintNo")

                        '採番値を印刷番号としてセット
                        printNo = rtDs.Tables(LMI511C.TABLE_NM.INOUT_GET_AUTO_CD).Rows(0).Item("NEW_CD").ToString()
                    End With
                Else
                    '印刷：済
                    With Nothing
                        '出荷登録処理からの呼び出し：スキップ
                        If outkaSave Then
                            Continue For
                        End If
                    End With
                End If

                '印刷対象として検索条件を追加
                Call Me.PrintSetPrt1DataIn(frm, rtDs, spIdx, printNo)
            Next
        End With

        'DataSetにプレビュー情報をマージ
        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        '印刷処理
        With Nothing
            '印刷対象がある
            If rtDs.Tables(LMI511C.TABLE_NM.IN_PRT1).Rows.Count > 0 Then
                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "Print1")
                rtDs = MyBase.CallWSA("LMI511BLF", "Print1", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "Print1")
            End If

            '印刷プレビュー
            Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
            If prevDt.Rows.Count = 0 Then
                '印刷対象がない
                If Not outkaSave Then
                    '通常の印刷実行時のみ
                    Call MyBase.SetMessage("G021")
                End If
            Else
                '印刷対象がある
                With Nothing
                    Dim prevFrm As RDViewer = New RDViewer()
                    prevFrm.DataSource = prevDt
                    prevFrm.Run()
                    prevFrm.Show()
                    prevFrm.Focus()
                End With
            End If
        End With

        '終了処理
        If Not outkaSave Then
            '通常の印刷実行時のみ
            If MyBase.IsMessageExist() Then
                'メッセージ表示
                MyBase.ShowMessage(frm)

                '処理終了アクション
                Call EndAction2(frm)

            Else
                '検索処理を呼び出す(印刷プレビュー表示中)
                Call Search(frm, True)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理(出荷予定表)：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <param name="printNo">印刷番号</param>
    ''' <remarks></remarks>
    Private Sub PrintSetPrt1DataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer, ByVal printNo As String)

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_PRT1).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            'プリントフラグ
            dr("PRTFLG") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.PRTFLG.ColNo)).Trim()
            '送受信伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '印刷番号
            dr("PRINT_NO") = printNo
            '荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_L.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_M.ColNo)).Trim()
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_PRT1).Rows.Add(dr)

    End Sub

#End Region

#Region "印刷処理(酢酸出荷依頼書(川本倉庫))"

    ''' <summary>
    ''' 印刷処理(酢酸出荷依頼書(川本倉庫))
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub Print2(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '印刷対象として検索条件を追加
                Call Me.PrintSetPrt2DataIn(frm, rtDs, spIdx)
            Next
        End With

        'DataSetにプレビュー情報をマージ
        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        '印刷処理
        With Nothing
            '印刷対象がある
            If rtDs.Tables(LMI511C.TABLE_NM.IN_PRT2).Rows.Count > 0 Then
                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "Print2")
                rtDs = MyBase.CallWSA("LMI511BLF", "Print2", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "Print2")
            End If

            '印刷プレビュー
            Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
            If prevDt.Rows.Count = 0 Then
                '印刷対象がない
                Call MyBase.SetMessage("G021")
            Else
                '印刷対象がある
                With Nothing
                    Dim prevFrm As RDViewer = New RDViewer()
                    prevFrm.DataSource = prevDt
                    prevFrm.Run()
                    prevFrm.Show()
                    prevFrm.Focus()
                End With
            End If
        End With

        '終了処理
        If MyBase.IsMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す(印刷プレビュー表示中)
            Call Search(frm, True)
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理(酢酸出荷依頼書(川本倉庫))：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub PrintSetPrt2DataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_PRT2).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            'プリントフラグサブ
            dr("PRTFLG_SUB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.PRTFLG_SUB.ColNo)).Trim()
            '送受信伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_L.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_M.ColNo)).Trim()
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_PRT2).Rows.Add(dr)

    End Sub

#End Region

#Region "印刷処理(ファクシミリ連絡票(三菱ケミカル))"

    ''' <summary>
    ''' 印刷処理(ファクシミリ連絡票(三菱ケミカル))
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub Print3(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '印刷対象として検索条件を追加
                Call Me.PrintSetPrt3DataIn(frm, rtDs, spIdx)
            Next
        End With

        'DataSetにプレビュー情報をマージ
        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        '印刷処理
        With Nothing
            '印刷対象がある
            If rtDs.Tables(LMI511C.TABLE_NM.IN_PRT3).Rows.Count > 0 Then
                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "Print3")
                rtDs = MyBase.CallWSA("LMI511BLF", "Print3", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "Print3")
            End If

            '印刷プレビュー
            Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
            If prevDt.Rows.Count = 0 Then
                '印刷対象がない
                Call MyBase.SetMessage("G021")
            Else
                '印刷対象がある
                With Nothing
                    Dim prevFrm As RDViewer = New RDViewer()
                    prevFrm.DataSource = prevDt
                    prevFrm.Run()
                    prevFrm.Show()
                    prevFrm.Focus()
                End With
            End If
        End With

        '終了処理
        If MyBase.IsMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す(印刷プレビュー表示中)
            Call Search(frm, True)
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理(ファクシミリ連絡票(三菱ケミカル))：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub PrintSetPrt3DataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_PRT3).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            'プリントフラグサブ
            dr("PRTFLG_SUB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.PRTFLG_SUB.ColNo)).Trim()
            '送受信伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_L.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_M.ColNo)).Trim()
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_PRT3).Rows.Add(dr)

    End Sub

#End Region

#Region "印刷処理(酢酸注文書(KHネオケム))"

    ''' <summary>
    ''' 印刷処理(酢酸注文書(KHネオケム))
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub Print4(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '印刷対象として検索条件を追加
                Call Me.PrintSetPrt4DataIn(frm, rtDs, spIdx)
            Next
        End With

        'DataSetにプレビュー情報をマージ
        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        '印刷処理
        With Nothing
            '印刷対象がある
            If rtDs.Tables(LMI511C.TABLE_NM.IN_PRT4).Rows.Count > 0 Then
                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "Print4")
                rtDs = MyBase.CallWSA("LMI511BLF", "Print4", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "Print4")
            End If

            '印刷プレビュー
            Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
            If prevDt.Rows.Count = 0 Then
                '印刷対象がない
                Call MyBase.SetMessage("G021")
            Else
                '印刷対象がある
                With Nothing
                    Dim prevFrm As RDViewer = New RDViewer()
                    prevFrm.DataSource = prevDt
                    prevFrm.Run()
                    prevFrm.Show()
                    prevFrm.Focus()
                End With
            End If
        End With

        '終了処理
        If MyBase.IsMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す(印刷プレビュー表示中)
            Call Search(frm, True)
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理(酢酸注文書(KHネオケム))：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub PrintSetPrt4DataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_PRT4).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            'プリントフラグサブ
            dr("PRTFLG_SUB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.PRTFLG_SUB.ColNo)).Trim()
            '送受信伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_L.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_M.ColNo)).Trim()
            '出荷日
            dr("OUTKA_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_DATE.ColNo)).Trim()
            '出荷場所部門コード
            dr("OUTKA_POSI_BU_CD_PA") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_POSI_BU_CD.ColNo)).Trim()
            '商品コード
            dr("JYUCHU_GOODS_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.GOODS_CD.ColNo)).Trim()

        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_PRT4).Rows.Add(dr)

    End Sub

#End Region

#Region "印刷処理(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))"

    ''' <summary>
    ''' 印刷処理(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <remarks></remarks>
    Private Sub Print5(ByVal frm As LMI511F, ByVal chkList As ArrayList)

        Dim rtDs As DataSet = New LMI511DS()

        With frm.sprEdiList
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))

                '印刷対象として検索条件を追加
                Call Me.PrintSetPrt5DataIn(frm, rtDs, spIdx)
            Next
        End With

        'DataSetにプレビュー情報をマージ
        rtDs.Merge(New RdPrevInfoDS)
        rtDs.Tables(LMConst.RD).Clear()

        '印刷処理
        With Nothing
            '印刷対象がある
            If rtDs.Tables(LMI511C.TABLE_NM.IN_PRT5).Rows.Count > 0 Then
                'WSAクラス呼出
                MyBase.Logger.StartLog(MyBase.GetType.Name, "Print5")
                rtDs = MyBase.CallWSA("LMI511BLF", "Print5", rtDs)
                MyBase.Logger.EndLog(MyBase.GetType.Name, "Print5")
            End If

            '印刷プレビュー
            Dim prevDt As DataTable = rtDs.Tables(LMConst.RD)
            If prevDt.Rows.Count = 0 Then
                '印刷対象がない
                Call MyBase.SetMessage("G021")
            Else
                '印刷対象がある
                With Nothing
                    Dim prevFrm As RDViewer = New RDViewer()
                    prevFrm.DataSource = prevDt
                    prevFrm.Run()
                    prevFrm.Show()
                    prevFrm.Focus()
                End With
            End If
        End With

        '終了処理
        If MyBase.IsMessageExist() Then
            'メッセージ表示
            MyBase.ShowMessage(frm)

            '処理終了アクション
            Call EndAction2(frm)

        Else
            '検索処理を呼び出す(印刷プレビュー表示中)
            Call Search(frm, True)
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))：データセット設定(検索条件)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <param name="spIdx">カレントインデックス</param>
    ''' <remarks></remarks>
    Private Sub PrintSetPrt5DataIn(ByVal frm As LMI511F, ByRef rtDs As DataSet, ByVal spIdx As Integer)

        Dim dr As DataRow = rtDs.Tables(LMI511C.TABLE_NM.IN_PRT5).NewRow()

        With frm.sprEdiList.ActiveSheet
            '営業所コード
            dr("NRS_BR_CD") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.NRS_BR_CD.ColNo)).Trim()
            'EDI管理番号
            dr("EDI_CTL_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.EDI_CTL_NO.ColNo)).Trim()
            'データ種別
            dr("DATA_KIND") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.DATA_KIND.ColNo)).Trim()
            'プリントフラグサブ
            dr("PRTFLG_SUB") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.PRTFLG_SUB.ColNo)).Trim()
            '送受信伝票番号
            dr("SR_DEN_NO") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.SR_DEN_NO.ColNo)).Trim()
            '荷主コード(大)
            dr("CUST_CD_L") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_L.ColNo)).Trim()
            '荷主コード(中)
            dr("CUST_CD_M") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.CUST_CD_M.ColNo)).Trim()
            '出荷日
            dr("OUTKA_DATE") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.OUTKA_DATE.ColNo)).Trim()
            ''出荷個数
            'dr("TUMI_SU") = Me._LMIconV.GetCellValue(.Cells(spIdx, _G.sprEdiListDef.TUMI_SU.ColNo)).Trim()
        End With

        rtDs.Tables(LMI511C.TABLE_NM.IN_PRT5).Rows.Add(dr)

    End Sub

#End Region

#Region "画面データ取得成功時"

    ''' <summary>
    ''' 画面データ取得成功時
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">取得結果DataSet</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMI511F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI511C.TABLE_NM.OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'スプレッドに取得データをセット
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        'データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        'カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
        End If

    End Sub

#End Region

#Region "画面終了時"

    ''' <summary>
    ''' 画面終了時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI511F, ByVal e As FormClosingEventArgs) As Boolean

        '編集中なら実行確認
        Select Case Me._Mode
            Case LMI511C.Mode.EDT, LMI511C.Mode.REV
                If MyBase.ShowMessage(frm, "W016") = MsgBoxResult.Cancel Then
                    e.Cancel = True
                    Exit Function
                End If
        End Select

    End Function

#End Region

#End Region 'Method

End Class