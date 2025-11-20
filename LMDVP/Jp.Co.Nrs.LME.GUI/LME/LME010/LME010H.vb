' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME010H : 作業料明細書作成
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LME010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LME010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LME010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LME010G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconH As LMEControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconG As LMEControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 検索値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsSel As DataSet

    'START YANAI 20120319　作業画面改造
    '''' <summary>
    '''' 保存判定フラグ
    '''' </summary>
    '''' <remarks>True:未保存データあり,False:未保存データなし</remarks>
    'Private _FlgSave As Boolean
    'END YANAI 20120319　作業画面改造

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
        Dim frm As LME010F = New LME010F(Me)

        'Validateクラスの設定
        Me._V = New LME010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LME010G(Me, frm)

        'Hnadler共通クラスの設定
        Me._LMEconH = New LMEControlH(DirectCast(frm, Form))

        Me._LMEconV = New LMEControlV(Me, DirectCast(frm, Form))

        'Gamen共通クラスの設定
        Me._LMEconG = New LMEControlG(DirectCast(frm, Form))

        'START YANAI 20120319　作業画面改造
        'Me._FlgSave = False
        'END YANAI 20120319　作業画面改造

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LME010C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetInitControl(Me.GetPGID(), frm)

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
    Friend Function ActionControl(ByVal eventShubetsu As LME010C.EventShubetsu, ByVal frm As LME010F) As Boolean

        '処理開始アクション
        Call Me._LMEconH.StartAction(frm)

        frm.lblCustNM_L.TextValue = String.Empty
        frm.lblCustNM_M.TextValue = String.Empty
        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMEconH.EndAction(frm)
            Exit Function
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData

        'パラメータ設定
        prm.ReturnFlg = False
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing
        Dim errDs As DataSet = Nothing
        Dim errHashTable As Hashtable = New Hashtable

        Dim chkList As ArrayList = Me._V.getCheckList()

        'イベント種別による分岐
        Select Case eventShubetsu

            'START YANAI 20120319　作業画面改造
            '*****新規*****
            Case LME010C.EventShubetsu.SINKI
                '******************「新規」******************'
                '項目チェック
                If Me._V.IsSinkiSingleCheck(LME010C.EventShubetsu.SINKI) = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                Call Me.ShowLME020sinki(frm, "txtCustCD_L", LME010C.EventShubetsu.SINKI) '第二引数をtxtCustCD_Lにしているが、特に意図はない。何かしらいけないといけないでのtxtCustCD_Lにしているだけ。

            Case LME010C.EventShubetsu.RENZOKU
                '******************「連続入力」******************'
                '項目チェック
                If Me._V.IsRenzokuSingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '出荷編集画面に遷移
                Call Me.ShowLME020renzoku(frm)
                'END YANAI 20120319　作業画面改造

                '*****確定*****
            Case LME010C.EventShubetsu.KAKUTEI

                'START YANAI 20120319　作業画面改造
                ''未保存データがある場合、処理終了
                'If Me._FlgSave = True Then
                '    MyBase.ShowMessage(frm, "E346", New String() {"確定"})
                '    Call Me._LMEconH.EndAction(frm)
                '    Exit Function
                'End If
                'END YANAI 20120319　作業画面改造

                '項目チェック
                If Me._V.IsKakuteiSingleCheck(LME010C.EventShubetsu.KAKUTEI) = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsKakuteiKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                Call Me.UpdateKakutei(frm)

                '*****確定解除*****
            Case LME010C.EventShubetsu.KAKUTEIKAIJO

                'START YANAI 20120319　作業画面改造
                ''未保存データがある場合、処理終了
                'If Me._FlgSave = True Then
                '    MyBase.ShowMessage(frm, "E346", New String() {"確定解除"})
                '    Call Me._LMEconH.EndAction(frm)
                '    Exit Function
                'End If
                'END YANAI 20120319　作業画面改造

                '項目チェック
                If Me._V.IsKakuteiSingleCheck(LME010C.EventShubetsu.KAKUTEIKAIJO) = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsKakuteiKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                Call Me.UpdateKaijo(frm)

                'START YANAI 20120319　作業画面改造
                '*****完了*****
            Case LME010C.EventShubetsu.KANRYO

                '項目チェック
                If Me._V.IsKanryoSingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsKanryoKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '完了処理を行う
                Call Me.UpdateKanryo(frm)
                'END YANAI 20120319　作業画面改造

                '*****初期荷主変更*****
            Case LME010C.EventShubetsu.DEF_CUST

                Call Me.ShowPopup(frm, "DEF_CUST", LME010C.EventShubetsu.ENTER)

            Case LME010C.EventShubetsu.ENTER

                Call Me.MasterRefer(frm, prm, eventShubetsu)

                'START SHINOHARA 要望番号513
            Case LME010C.EventShubetsu.MASTER

                Call Me.MasterRefer(frm, prm, eventShubetsu)
                'END SHINOHARA 要望番号513

                '*****検索処理******
            Case LME010C.EventShubetsu.KENSAKU

                'START YANAI 20120319　作業画面改造
                'If Me._FlgSave = True Then

                '    Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "W158")

                '    If rtnResult = MsgBoxResult.Cancel Then
                '        Call Me._LMEconH.EndAction(frm) '終了処理
                '        Exit Function
                '    End If

                'End If

                'Me._FlgSave = False
                'END YANAI 20120319　作業画面改造

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '検索処理を行う
                Call Me.SelectData(frm)

                'フォーカスの設定
                Call Me._G.SetFoucus()

            Case LME010C.EventShubetsu.HOZON

                'START YANAI 20120319　作業画面改造
                'If Me._FlgSave = False Then
                '    MyBase.ShowMessage(frm, "E315")
                '    Call Me._LMEconH.EndAction(frm) '終了処理
                '    Exit Function
                'End If

                'Me._FlgSave = False
                'END YANAI 20120319　作業画面改造

                '関連チェック
                If Me._V.IsHozonKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                Return Me.Hozon(frm)

            Case LME010C.EventShubetsu.HENKO

                'START YANAI 20120319　作業画面改造
                ''未保存データがある場合、処理終了
                'If Me._FlgSave = True Then
                '    MyBase.ShowMessage(frm, "E346", New String() {"一括変更"})
                '    Call Me._LMEconH.EndAction(frm)
                '    Exit Function
                'End If
                'END YANAI 20120319　作業画面改造

                '項目チェック
                If Me._V.IsHenkoSingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                errHashTable = Me._V.IsHenkoKanrenCheck(errDs)

                '全行エラーの場合、処理終了()
                If chkList.Count = errHashTable.Count Then
                    'エラーをExcelに出力
                    If errDs.Tables("LME010_GUIERROR").Rows.Count <> 0 Then
                        Call Me.ExcelErrorSet(errDs)
                        Call Me.OutputExcel(frm)
                    End If

                    Call Me._LMEconH.EndAction(frm)
                    Exit Function
                End If

                Call Me.IkkatsuHenko(frm, errHashTable, errDs)

            Case LME010C.EventShubetsu.ROW_COPY

                '項目チェック
                If Me._V.IsRowCopySingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsRowCopyKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                'START YANAI 20120319　作業画面改造
                ''行複写処理を行う
                'Call Me.RowCopy(frm)
                '出荷編集画面に遷移
                Call Me.ShowLME020copy(frm)
                'END YANAI 20120319　作業画面改造

            Case LME010C.EventShubetsu.ROW_DEL

                '項目チェック
                If Me._V.IsRowDelSingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsRowDelKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                'START YANAI 20120319　作業画面改造
                ''行削除処理を行う
                'Call Me.RowDel(frm)
                '2016.01.06 UMANO 英語化対応START
                'If MyBase.ShowMessage(frm, "C001", New String() {"行削除"}) = MsgBoxResult.Ok Then
                If MyBase.ShowMessage(frm, "C001", New String() {frm.btnRowDel.Text()}) = MsgBoxResult.Ok Then
                    '行削除処理を行う
                    Call Me.RowDel(frm)
                End If
                '2016.01.06 UMANO 英語化対応END
                'END YANAI 20120319　作業画面改造

                'START YANAI 20120319　作業画面改造
            Case LME010C.EventShubetsu.DOUBLECLICK

                '******************「ダブルクリック」******************'

                'クリックした行が検索行の場合
                If frm.sprSagyo.Sheets(0).ActiveRow.Index() = 0 Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '単体チェック
                If Me._V.IsDoubleClickSingleCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsDoubleClickKanrenCheck() = False Then
                    Call Me._LMEconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '出荷編集画面に遷移
                Call Me.ShowLME020henshu(frm)
                'END YANAI 20120319　作業画面改造

        End Select

        '処理終了アクション
        Call Me._LMEconH.EndAction(frm)

    End Function

#End Region '外部メソッド

#Region "内部メソッド"

#Region "確定処理"
    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpdateKakutei(ByRef frm As LME010F)

        '続行確認
        Dim rtn As MsgBoxResult

        '2016.01.06 UMANO 英語化対応START
        'rtn = Me.ShowMessage(frm, "C001", New String() {"確定"})
        rtn = Me.ShowMessage(frm, "C001", New String() {frm.FunctionKey.F5ButtonName})
        '2016.01.06 UMANO 英語化対応END

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LME010DS()
        Call Me.SetDataKakutei(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Kakutei")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LME010BLF", "UpdateKakutei", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            Call Me._LMEconH.EndAction(frm)
            Exit Sub
        Else

            '確定処理成功時処理
            Call Me.SuccessKakutei(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Kakutei")

        Call Me._LMEconH.EndAction(frm)

    End Sub

#End Region '確定処理

#Region "確定解除処理"
    ''' <summary>
    ''' 確定解除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpdateKaijo(ByRef frm As LME010F)

        '続行確認
        Dim rtn As MsgBoxResult

        '2016.01.06 UMANO 英語化対応START
        'rtn = Me.ShowMessage(frm, "C001", New String() {"確定解除"})
        rtn = Me.ShowMessage(frm, "C001", New String() {frm.FunctionKey.F6ButtonName})
        '2016.01.06 UMANO 英語化対応END

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LME010DS()
        Call Me.SetDataKaijo(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Kaijo")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LME010BLF", "UpdateKaijo", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            Call Me._LMEconH.EndAction(frm)
            Exit Sub
        Else

            '確定解除処理成功時処理
            Call Me.SuccessKaijo(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Kaijo")

        Call Me._LMEconH.EndAction(frm)

    End Sub
#End Region '確定解除処理

    'START YANAI 20120319　作業画面改造
#Region "完了処理"
    ''' <summary>
    ''' 完了処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpdateKanryo(ByRef frm As LME010F)

        '続行確認
        Dim rtn As MsgBoxResult

        '2016.01.06 UMANO 英語化対応START
        'rtn = Me.ShowMessage(frm, "C001", New String() {"完了"})
        rtn = Me.ShowMessage(frm, "C001", New String() {frm.FunctionKey.F8ButtonName})
        '2016.01.06 UMANO 英語化対応END

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LME010DS()
        Call Me.SetDataKanryo(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateKanryo")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LME010BLF", "UpdateKanryo", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            Call Me._LMEconH.EndAction(frm)
            Exit Sub
        Else

            '完了処理成功時処理
            Call Me.SuccessKanryo(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateKanryo")

        Call Me._LMEconH.EndAction(frm)

    End Sub

#End Region '確定処理
    'END YANAI 20120319　作業画面改造

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByRef frm As LME010F)

        ''閾値の設定
        'Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)
        'MyBase.SetLimitCount(Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))))

        'DataSet設定
        Dim rtDs As DataSet = New LME010DS()
        Call Me.SetDataSetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMEconH.CallWSAAction(DirectCast(frm, Form), _
                                                "LME010BLF", "SelectListData", rtDs _
                                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                                 , Convert.ToInt32(Convert.ToDouble( _
                                                 MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                                 .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))


        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LME010F)

        With frm
            '20160621 tsunehira 要番2491 add start
            Dim NrsBrCd As String = .cmbEigyo.SelectedValue.ToString()
            '20160621 tsunehira 要番2491 add end
            Dim custCdL As String = frm.txtCustCD_L.TextValue
            Dim custCdM As String = frm.txtCustCD_M.TextValue

            '荷主名称
            .lblCustNM_L.TextValue = String.Empty
            .lblCustNM_M.TextValue = String.Empty
            If String.IsNullOrEmpty(custCdL) = False Then
                If String.IsNullOrEmpty(custCdM) = True Then
                    custCdM = "00"
                End If

                '20160621 tsunehira 要番2491 add start
                Dim custArray() As String = Me.GetCachedCust(NrsBrCd, custCdL, custCdM, "00", "00")
                '20160621 tsunehira 要番2491 add end

                'Dim custArray() As String = Me.GetCachedCust(custCdL, custCdM, "00", "00")

                .lblCustNM_L.TextValue = custArray(0)
                .lblCustNM_M.TextValue = custArray(1)
            End If

            '請求先名称
            .lblSeikyuNM.TextValue = String.Empty
            If String.IsNullOrEmpty(.txtSeikyuCD.TextValue) = False Then
                .lblSeikyuNM.TextValue = GetCachedSeikyu(.txtSeikyuCD.TextValue, .cmbEigyo.SelectedValue.ToString())
            End If

            '作業名称
            .lblSagyoNM.TextValue = String.Empty
            If String.IsNullOrEmpty(.txtSagyoCD.TextValue) = False Then
                .lblSagyoNM.TextValue = GetCachedSagyo(.txtSagyoCD.TextValue, .cmbEigyo.SelectedValue.ToString(), custCdL)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal NrsBrCd As String, _
                                   ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String()

        Dim dr As DataRow() = Nothing
        Dim custArray(1) As String

        '荷主名称
        '20160621 tsunehira  要番2491 add start 
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", NrsBrCd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        '20160621 tsunehira  要番2491 add end
        If 0 < dr.Length Then
            custArray(0) = dr(0).Item("CUST_NM_L").ToString()
            custArray(1) = dr(0).Item("CUST_NM_M").ToString()
            Return custArray
        End If

        custArray(0) = String.Empty
        custArray(1) = String.Empty

        Return custArray

    End Function

    ''' <summary>
    ''' 請求先キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedSeikyu(ByVal seikyuToCd As String, ByVal nrsBrCd As String) As String

        Dim dr As DataRow() = Nothing


        '請求先名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                         , "SEIQTO_CD = '", seikyuToCd, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("SEIQTO_NM").ToString

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' 作業項目キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedSagyo(ByVal sagyoCd As String, ByVal nrsBrCd As String, ByVal custCdL As String) As String

        Dim dr As DataRow() = Nothing

        '作業項目
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                         , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("SAGYO_NM").ToString

        End If

        Return String.Empty

    End Function

#End Region '検索処理

#Region "マスタ参照"

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub MasterRefer(ByVal frm As LME010F, ByVal prm As LMFormData, ByVal Eventshubetsu As LME010C.EventShubetsu)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        'Pop起動処理
        Call Me.ShowPopup(frm, objNm, EventShubetsu)

    End Sub

#End Region 'マスタ参照

#Region "保存処理"
    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function Hozon(ByVal frm As LME010F) As Boolean

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Hozon")
        '==== WSAクラス呼出 ====
        Call MyBase.CallWSA("LME010BLF", "UpdateHozon", _DsSel)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            Call Me._LMEconH.EndAction(frm)
            Return False
        Else

            '保存処理成功時処理
            Call Me._LMEconH.EndAction(frm)
            Call Me.SuccessHozon(frm)
            Return True

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Hozon")

    End Function

#End Region '保存処理

#Region "行複写処理"
    ''' <summary>
    ''' 行複写処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowCopy(ByVal frm As LME010F)

        Dim nrsBrCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim chkRow As Integer = Convert.ToInt32(chkList(0))

        With frm.sprSagyo.ActiveSheet
            nrsBrCd = Me._LMEconV.GetCellValue(.Cells(chkRow, LME010G.sprDetailDef.NRS_BR_CD.ColNo))
            custCdL = Me._LMEconV.GetCellValue(.Cells(chkRow, LME010G.sprDetailDef.CUST_CD_L.ColNo)).Trim()

        End With

        Dim sagyoPop As LMFormData = Me.ShowSagyoPopup(frm, nrsBrCd, custCdL)

        Me.ShowMessage(frm, "G007")

        If sagyoPop.ReturnFlg = True Then

            '複写行をデータセットに設定
            If Me.SetDataRowCopy(frm, sagyoPop) = False Then
                Exit Sub
            End If

            'Call Me._G.SetSpread(_DsSel.Tables(LME010C.TABLE_NM_INOUT))

            ''スプレッドに新規行追加
            Call Me.AddSpread(frm)

            'START YANAI 20120319　作業画面改造
            'Me._FlgSave = True
            'END YANAI 20120319　作業画面改造
        End If

    End Sub

#End Region '行追加処理

#Region "行削除処理"
    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowDel(ByVal frm As LME010F)

        '削除フラグをデータセットに設定
        Call Me.SetDataRowDel(frm)

        'START YANAI 20120319　作業画面改造
        '削除処理
        Me.DeleteData(Me._DsSel)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            Call Me.OutputExcel(frm)
            Exit Sub
        End If
        'END YANAI 20120319　作業画面改造

        'スプレッドから選択行を削除
        Call Me.DelSpread(frm)

        'START YANAI 20120319　作業画面改造
        'Me._FlgSave = True
        'データセットから選択行を削除
        Call Me.DelDataSet(frm)
        'END YANAI 20120319　作業画面改造

        Me.ShowMessage(frm, "G007")

    End Sub
#End Region '行削除処理

#Region "一括変更処理"
    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="errHashtable"></param>
    ''' <remarks></remarks>
    Private Sub IkkatsuHenko(ByVal frm As LME010F, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        '2016.01.06 UMANO 英語化対応START
        'rtn = Me.ShowMessage(frm, "C001", New String() {"一括変更"})
        rtn = Me.ShowMessage(frm, "C001", New String() {frm.btnAllChange.Text})
        '2016.01.06 UMANO 英語化対応END

        If rtn = MsgBoxResult.Ok Then
            'エラーをExcelに出力
            If errDs.Tables("LME010_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(errDs)
            End If

        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LME010DS()
        Call Me.SetDataHenkoKey(frm, rtDs, errHashtable)
        Call Me.SetDataHenkoValue(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Henko")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LME010BLF", "UpdateHenko", rtDs)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            Call Me._LMEconH.EndAction(frm)
            Exit Sub
        Else
            Call Me.SuccessHenko(frm)
        End If

        '請求先名称
        With frm
            If String.IsNullOrEmpty(.txtEditTxt.TextValue) = False AndAlso .cmbEditList.SelectedValue.Equals(LME010C.EDIT_SELECT_SEIQTO) = True Then
                .lblEditNM.TextValue = GetCachedSeikyu(.txtEditTxt.TextValue, .cmbEigyo.SelectedValue.ToString())
            End If
        End With

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Henko")

        Call Me._LMEconH.EndAction(frm)

    End Sub

#End Region '一括変更

#Region "検索成功時"
    ''' <summary>
    ''' 検索成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LME010F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LME010C.TABLE_NM_INOUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprSagyo.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        '検索結果データセットを格納
        _DsSel = ds.Copy()

        If Me._CntSelect.Equals("0") = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect, String.Empty})

        End If

    End Sub
#End Region

#Region "確定成功時"
    ''' <summary>
    ''' 確定成功時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SuccessKakutei(ByVal frm As LME010F)
        'メッセージエリアの設定
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {"確定", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F5ButtonName, String.Empty})
        '2016.01.06 UMANO 英語化対応END
    End Sub
#End Region

#Region "確定解除成功時"
    Private Sub SuccessKaijo(ByVal frm As LME010F)
        'メッセージエリアの設定
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {"確定解除", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F6ButtonName, String.Empty})
        '2016.01.06 UMANO 英語化対応END
    End Sub
#End Region

    'START YANAI 20120319　作業画面改造
#Region "完了成功時"
    ''' <summary>
    ''' 完了成功時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SuccessKanryo(ByVal frm As LME010F)
        'メッセージエリアの設定
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {"完了", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F8ButtonName, String.Empty})
        '2016.01.06 UMANO 英語化対応END
    End Sub
#End Region
    'END YANAI 20120319　作業画面改造

#Region "保存成功時"
    Private Sub SuccessHozon(ByVal frm As LME010F)

        Call SetDataHozonSuccess(frm)

        MyBase.ShowMessage(frm, "G002", New String() {"保存", String.Empty})


    End Sub
#End Region

#Region "一括変更成功時"
    ''' <summary>
    ''' 一括変更成功時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SuccessHenko(ByVal frm As LME010F)

        '2016.01.06 UMANO 英語化対応START
        'メッセージエリアの設定
        'MyBase.ShowMessage(frm, "G002", New String() {"一括変更", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {frm.btnAllChange.Text, String.Empty})
        '2016.01.06 UMANO 英語化対応END

    End Sub
#End Region

#Region "検索条件データセット"
    ''' <summary>
    ''' 検索条件データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LME010F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LME010C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        If frm.optNotKakutei.Checked Then
            dr("SAGYO_STATE_KB") = "00"
        Else
            dr("SAGYO_STATE_KB") = "01"
        End If
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbWare.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim()
        dr("CUST_CD_M") = frm.txtCustCD_M.TextValue.Trim()
        dr("SEIQTO_CD") = frm.txtSeikyuCD.TextValue.Trim()
        dr("SAGYO_CD") = frm.txtSagyoCD.TextValue.Trim()
        dr("SAGYO_DATE_FROM") = frm.imdSagyoDate_S.TextValue.Trim()
        dr("SAGYO_DATE_TO") = frm.imdSagyoDate_E.TextValue.Trim()
        dr("SAGYO_SIJI_NO") = frm.txtSagyoSijiNO.TextValue.Trim()
        'START YANAI 20120319　作業画面改造
        If frm.optNotKanryo.Checked Then
            dr("SAGYO_COMP") = "00"
        Else
            dr("SAGYO_COMP") = "01"
        End If
        'END YANAI 20120319　作業画面改造

        '検索条件　入力部（スプレッド）
        With frm.sprSagyo.ActiveSheet

            dr("GOODS_NM_NRS") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.GOODS_NM.ColNo))
            dr("LOT_NO") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.LOT_NO.ColNo))
            dr("SAGYO_NM") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SAGYO_NM.ColNo))
            dr("INV_TANI") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.INV_TANI_NM.ColNo))
            dr("SEIQTO_NM") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SQTO_NM.ColNo))
            dr("DEST_NM") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.DEST_NM.ColNo))
            dr("REMARK_SKYU") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SKYU_REMARK.ColNo))
            dr("TAX_KB") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.TAX_KB_NM.ColNo))
            dr("CUST_NM_L") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.CUST_NM_L.ColNo))
            dr("INOUTKA_NO_LM") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.IOKA_CTL_NO.ColNo))
            dr("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo))
            dr("SAGYO_COMP_NM") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SAGYO_COMP_NM.ColNo))
            dr("IOZS_KB") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.IOZS_NM.ColNo))
            dr("SYS_UPD_USER") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.UPT_NM.ColNo))
            'START YANAI 20120319　作業画面改造
            'dr("SAGYO_COMP") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SAGYO_COMP.ColNo))
            'END YANAI 20120319　作業画面改造
            dr("SYS_DEL_FLG") = Me._LMEconV.GetCellValue(.Cells(0, LME010G.sprDetailDef.SYS_DEL_FLG.ColNo))

        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LME010C.TABLE_NM_IN).Rows.Add(dr)

    End Sub
#End Region

#Region "確定データセット"
    Private Sub SetDataKakutei(ByVal frm As LME010F, ByVal rtDs As DataSet)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim sysUpdDate As String = String.Empty

        With frm.sprSagyo.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))
                dr = rtDs.Tables(LME010C.TABLE_NM_SAGYO).NewRow()

                dr("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo))
                dr("SKYU_CHK") = "01"
                dr("SYS_DEL_FLG") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SYS_DEL_FLG.ColNo))
                sysUpdDate = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.UPD_DT.ColNo))
                If String.IsNullOrEmpty(sysUpdDate) = False Then
                    dr("SYS_UPD_DATE") = Convert.ToDateTime(sysUpdDate).ToString("yyyyMMdd")
                Else
                    dr("SYS_UPD_DATE") = String.Empty
                End If

                dr("SYS_UPD_TIME") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.HAITA_UPD_TM.ColNo))
                dr("ROW_NO") = selectRow

                '確定データをデータセットに設定
                rtDs.Tables(LME010C.TABLE_NM_SAGYO).Rows.Add(dr)
            Next

        End With

    End Sub
#End Region

#Region "確定解除データセット"
    ''' <summary>
    ''' 確定解除データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataKaijo(ByVal frm As LME010F, ByVal rtDs As DataSet)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprSagyo.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))
                dr = rtDs.Tables(LME010C.TABLE_NM_SAGYO).NewRow()

                dr("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo))
                dr("SKYU_CHK") = "00"
                dr("SEIQTO_CD") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SQTO_CD.ColNo))
                dr("SAGYO_COMP_CD") = String.Empty
                dr("SYS_DEL_FLG") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SYS_DEL_FLG.ColNo))
                dr("SYS_UPD_DATE") = Convert.ToDateTime(Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.UPD_DT.ColNo))).ToString("yyyyMMdd")
                dr("SYS_UPD_TIME") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.HAITA_UPD_TM.ColNo))
                dr("SAGYO_COMP_DATE") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SAGYO_COMP_DATE.ColNo))
                dr("ROW_NO") = selectRow

                '確定データをデータセットに設定
                rtDs.Tables(LME010C.TABLE_NM_SAGYO).Rows.Add(dr)
            Next

        End With

    End Sub

#End Region

    'START YANAI 20120319　作業画面改造
#Region "完了データセット"
    Private Sub SetDataKanryo(ByVal frm As LME010F, ByVal rtDs As DataSet)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim sysUpdDate As String = String.Empty

        With frm.sprSagyo.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))
                dr = rtDs.Tables(LME010C.TABLE_NM_SAGYO).NewRow()

                dr("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo))
                dr("SAGYO_COMP") = "01"
                dr("SYS_DEL_FLG") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SYS_DEL_FLG.ColNo))
                sysUpdDate = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.UPD_DT.ColNo))
                If String.IsNullOrEmpty(sysUpdDate) = False Then
                    dr("SYS_UPD_DATE") = Convert.ToDateTime(sysUpdDate).ToString("yyyyMMdd")
                Else
                    dr("SYS_UPD_DATE") = String.Empty
                End If

                dr("SYS_UPD_TIME") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.HAITA_UPD_TM.ColNo))
                dr("ROW_NO") = selectRow

                '完了データをデータセットに設定
                rtDs.Tables(LME010C.TABLE_NM_SAGYO).Rows.Add(dr)
            Next

        End With

    End Sub
#End Region
    'END YANAI 20120319　作業画面改造

#Region "行複写データセット"
    ''' <summary>
    ''' 行複写データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function SetDataRowCopy(ByVal frm As LME010F, ByVal ds As LMFormData) As Boolean

        Dim sagyoDr As DataRow = ds.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)
        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim selectRow As Integer = 0
        Dim rowNo As Integer = 0
        Dim dt As DataTable = _DsSel.Tables(LME010C.TABLE_NM_INOUT)
        Dim drNo As Integer
        Dim dtRow As Integer = 0
        Dim setDr As DataRow
        Dim sagyoGk As Decimal = 0

        With frm.sprSagyo.ActiveSheet

            'チェック行のROW_NOを取得
            selectRow = Convert.ToInt32(chkList(0))
            rowNo = Convert.ToInt32(Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.ROW_NO.ColNo)))

            'チェック行のROW_NOと一致するDataSet行を取得
            drNo = GetDrFromRowNo(rowNo)

            'DataSet行を最下行に複写
            dt.ImportRow(dt.Rows(drNo))

            '最下行の行番号を取得
            dtRow = dt.Rows.Count - 1
            setDr = dt.Rows(dtRow)

            '複写行のDataSetの編集
            'setDr("ROW_NO") = dt.Rows(dtRow - 1).Item("ROW_NO").ToString()
            setDr("ROW_NO") = (dtRow + 1).ToString

            setDr("SAGYO_NM") = sagyoDr.Item("SAGYO_NM")

            setDr("SAGYO_UP") = sagyoDr.Item("SAGYO_UP")

            Debug.Print(setDr("SAGYO_COMP_DATE").ToString)


            Dim sysDateTime As String() = MyBase.GetSystemDateTime
            setDr("SAGYO_COMP_DATE") = sysDateTime(0)

            setDr("INV_TANI_NM") = sagyoDr.Item("INV_TANI_NM")

            If sagyoDr.Item("KOSU_BAI").ToString = "01" Then
                sagyoGk = Me.ToRound(Convert.ToDecimal(sagyoDr.Item("SAGYO_UP")), 0)
            Else
                sagyoGk = Me.ToRound(Convert.ToDecimal(sagyoDr.Item("SAGYO_UP")) * Convert.ToDecimal(setDr("SAGYO_NB")), 0)
            End If

            '作業金額の範囲チェック
            If Me._V.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME010C.SAGYO_GK_MIN), Convert.ToDecimal(LME010C.SAGYO_GK_MAX)) = False Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage(frm, "E413", New String() {"請求金額", String.Concat(LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)})
                MyBase.ShowMessage(frm, "E413", New String() {String.Concat(LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)})
                '2016.01.06 UMANO 英語化対応END
                setDr.Delete()
                Return False
            End If

            setDr("SAGYO_GK") = sagyoGk.ToString()

            setDr("TAX_KB_NM") = sagyoDr.Item("ZEI_KBN_NM")

            setDr("SAGYO_SIJI_NO") = String.Empty

            setDr("SAGYO_REC_NO") = String.Empty

            setDr("SAGYO_COMP_NM") = String.Empty

            setDr("SAGYO_CD") = sagyoDr.Item("SAGYO_CD")

            setDr("SYS_UPD_DATE") = String.Empty

            setDr("SYS_UPD_TIME") = String.Empty

            setDr("SYS_UPD_USER") = String.Empty

            Select Case setDr("IOZS_KB").ToString
                Case "10", "11"
                    setDr("IOZS_KB") = "12"
                Case "20", "21"
                    setDr("IOZS_KB") = "22"
            End Select

            setDr("SKYU_CHK") = "00"

            setDr("REMARK_ZAI") = sagyoDr("SAGYO_REMARK")

            setDr("SAGYO_COMP_CD") = String.Empty

            setDr("SYS_DEL_FLG") = "0"

            setDr("COPY_FLG") = "1"

            setDr("SAVE_FLG") = "0"

        End With

        Return True

    End Function
#End Region

#Region "行削除データセット"
    ''' <summary>
    ''' 行削除データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetDataRowDel(ByVal frm As LME010F)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim selectRow As Integer = 0
        Dim rowNo As Integer = 0
        Dim dt As DataTable = _DsSel.Tables(LME010C.TABLE_NM_INOUT)
        Dim drNo As Integer = 0

        With frm.sprSagyo.ActiveSheet

            For i As Integer = 0 To max - 1

                'チェック行のROW_NOを取得
                selectRow = Convert.ToInt32(chkList(i))
                rowNo = Convert.ToInt32(Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.ROW_NO.ColNo)))

                'チェック行のROW_NOと一致するDataSet行に削除フラグを設定する
                drNo = GetDrFromRowNo(rowNo)
                dt.Rows(drNo)("SYS_DEL_FLG") = "1"

            Next

        End With

    End Sub
#End Region

#Region "保存成功時（複写、削除）データセット"
    Private Sub SetDataHozonSuccess(ByVal frm As LME010F)

        Dim max As Integer = 0
        Dim setDt As DataTable = _DsSel.Tables(LME010C.TABLE_NM_INOUT)
        Dim dtRow As Integer = 0

        With frm.sprSagyo.ActiveSheet

            max = setDt.Rows.Count - 1

            For i As Integer = 0 To max

                If setDt.Rows(i)("SYS_DEL_FLG").ToString = "1" AndAlso setDt.Rows(i)("SAVE_FLG").ToString = "0" Then
                    setDt.Rows(i)("SAVE_FLG") = "1"
                End If

                If setDt.Rows(i)("COPY_FLG").ToString = "1" AndAlso setDt.Rows(i)("SAVE_FLG").ToString = "0" Then
                    setDt.Rows(i)("SAVE_FLG") = "1"
                End If

            Next

        End With

    End Sub
#End Region

#Region "一括変更選択行データセット"
    ''' <summary>
    ''' 一括変更選択行データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetDataHenkoKey(ByVal frm As LME010F, ByVal rtDs As DataSet, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim updDate As String

        With frm.sprSagyo.ActiveSheet

            For i As Integer = 0 To max - 1

                If errHashTable.ContainsKey(i) Then
                    Continue For
                End If

                selectRow = Convert.ToInt32(chkList(i))
                dr = rtDs.Tables(LME010C.TABLE_NM_UPDATE_KEY).NewRow()

                dr("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo))
                dr("ROW_NO") = selectRow

                updDate = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.UPD_DT.ColNo))
                If String.IsNullOrEmpty(updDate) = False Then
                    updDate = Convert.ToDateTime(Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.UPD_DT.ColNo))).ToString("yyyyMMdd")
                End If

                dr("SYS_UPD_DATE") = updDate
                dr("SYS_UPD_TIME") = Me._LMEconV.GetCellValue(.Cells(selectRow, LME010G.sprDetailDef.HAITA_UPD_TM.ColNo))

                'データセットに設定
                rtDs.Tables(LME010C.TABLE_NM_UPDATE_KEY).Rows.Add(dr)
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
    Private Sub SetDataHenkoValue(ByVal frm As LME010F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LME010C.TABLE_NM_UPDATE_VALUE).NewRow()
        'キャッシュの値(請求先マスタ)
        Dim mKbnDrs As DataRow() = Nothing
        Dim selectCmbValue As String = frm.cmbEditList.SelectedValue.ToString
        Dim editNm As String = String.Empty
        Dim editNmJ As String = String.Empty
        Dim editType As Integer = 3
        Dim itemKb As String = frm.cmbEditKbSkyu.SelectedValue.ToString

        mKbnDrs = Me._LMEconV.SelectKBNListDataRow(selectCmbValue, "S062")
        If 0 < mKbnDrs.Length Then
            editNm = mKbnDrs(0).Item("KBN_NM3").ToString()
            editType = Convert.ToInt32(mKbnDrs(0).Item("KBN_NM4"))
            editNmJ = mKbnDrs(0).Item("KBN_NM1").ToString()
        End If


        Select Case selectCmbValue
            Case LME010C.EDIT_SELECT_GOODS, LME010C.EDIT_SELECT_SAGYONM, LME010C.EDIT_SELECT_SEIQTO, LME010C.EDIT_SELECT_DEST, LME010C.EDIT_SELECT_REMARK
                dr("EDIT_ITEM_VALUE") = frm.txtEditTxt.TextValue.Trim()
            Case LME010C.EDIT_SELECT_LOT
                dr("EDIT_ITEM_VALUE") = frm.txtEditTxt.TextValue.Trim().ToUpper()
            Case LME010C.EDIT_SELECT_SAGYOSU, LME010C.EDIT_SELECT_SEIQUP, LME010C.EDIT_SELECT_SEIQGK
                dr("EDIT_ITEM_VALUE") = frm.txtEditNum.TextValue
            Case LME010C.EDIT_SELECT_SEIQUT
                dr("EDIT_ITEM_VALUE") = frm.cmbEditKbSkyu.SelectedValue
            Case LME010C.EDIT_SELECT_SAGYODATE
                dr("EDIT_ITEM_VALUE") = frm.cmbEditDate.TextValue
            Case LME010C.EDIT_SELECT_TAX
                dr("EDIT_ITEM_VALUE") = frm.cmbEditKbTax.SelectedValue

        End Select

        dr("EDIT_ITEM_NM") = editNm
        dr("EDIT_ITEM_TYPE") = editType

        'データセットに設定
        rtDs.Tables(LME010C.TABLE_NM_UPDATE_VALUE).Rows.Add(dr)

    End Sub
#End Region

#Region "コントロール初期化"
    Private Sub ClearControl(ByVal frm As LME010F)

        With frm
            .txtEditNum.TextValue = String.Empty
            .txtEditTxt.TextValue = String.Empty
            .cmbEditKbSkyu.TextValue = String.Empty
            .cmbEditDate.TextValue = String.Empty
            .cmbEditKbTax.TextValue = String.Empty
            .lblEditNM.TextValue = String.Empty
        End With

    End Sub
#End Region

#Region "PopUp"


    ''' <summary>
    ''' マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LME010F, ByVal objNM As String, ByVal eventshubetsu As LME010C.EventShubetsu) As Boolean

        If Me._V.IsPopSingleCheck(objNM) = False Then
            Exit Function
        End If

        With frm

            Select Case objNM

                Case .txtCustCD_L.Name, .txtCustCD_M.Name               '荷主マスタ参照

                    If String.IsNullOrEmpty(.txtCustCD_L.TextValue) = True Then
                        .lblCustNM_L.TextValue = String.Empty
                        .lblCustNM_M.TextValue = String.Empty
                    End If

                    '荷主マスタ参照POP起動
                    Call Me.SetMstResult(frm, eventshubetsu)

                    'メッセージの表示
                    Me.ShowMessage(frm, "G007")

                Case .txtSeikyuCD.Name                                  '請求先マスタ参照

                    If String.IsNullOrEmpty(.txtSeikyuCD.TextValue) = True Then
                        .lblSeikyuNM.TextValue = String.Empty
                    End If

                    '請求先マスタ参照POP起動
                    Call Me.SetSeikyuResult(frm, objNM, eventshubetsu)

                    'メッセージの表示
                    Me.ShowMessage(frm, "G007")

                Case .txtEditTxt.Name                                   '請求先マスタ参照

                    Select Case frm.cmbEditList.SelectedValue.ToString()

                        Case LME010C.EDIT_SELECT_SEIQTO

                            If String.IsNullOrEmpty(.txtEditTxt.TextValue) = True Then
                                .lblEditNM.TextValue = String.Empty
                            End If

                            '請求先マスタ参照POP起動
                            Call Me.SetSeikyuResult(frm, objNM, eventshubetsu)

                            'メッセージの表示
                            Me.ShowMessage(frm, "G007")

                        Case Else
                            Me.ShowMessage(frm, "G005")

                    End Select

                Case .txtSagyoCD.Name                                   '作業項目マスタ参照

                    '作業項目マスタ参照POP起動
                    If String.IsNullOrEmpty(.txtSagyoCD.TextValue) = True Then
                        .lblSagyoNM.TextValue = String.Empty
                    End If

                    Call Me.SetReturnSagyoKmkPop(frm, objNM, eventshubetsu)

                    'メッセージの表示
                    Me.ShowMessage(frm, "G007")

                Case "DEF_CUST"                                         '初期荷主変更

                    '初期荷主変更POP起動
                    Call Me.ShowDefPopup(frm)

                    'メッセージの表示
                    Me.ShowMessage(frm, "G007")

                Case Else
                    'メッセージの表示
                    Me.ShowMessage(frm, "G005")


            End Select
        End With

        Return True

    End Function

#Region "荷主マスタ"

    ''' <summary>
    ''' 荷主マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetMstResult(ByVal frm As LME010F, ByVal EventShubetsu As LME010C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.SetMstResultByVal(frm, EventShubetsu)
        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット()
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                frm.txtCustCD_L.TextValue = dr.Item("CUST_CD_L").ToString    '荷主コード（大）
                frm.lblCustNM_L.TextValue = dr.Item("CUST_NM_L").ToString    '荷主名（大）
                frm.txtCustCD_M.TextValue = dr.Item("CUST_CD_M").ToString    '荷主コード（中）
                frm.lblCustNM_M.TextValue = dr.Item("CUST_NM_M").ToString    '荷主名（大）

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">コントロール</param>
    ''' <remarks></remarks>
    Private Function SetMstResultByVal(ByVal frm As LME010F, ByVal EventShubetsu As LME010C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue().ToString()
            'START SHINOHARA 要望番号513
            If EventShubetsu = LME010C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim()
                .Item("CUST_CD_M") = frm.txtCustCD_M.TextValue.Trim()
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMEconH.FormShow(ds, Me._PopupSkipFlg, "LMZ260")

    End Function

#End Region

#Region "請求先マスタ"

    ''' <summary>
    ''' 請求先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetSeikyuResult(ByVal frm As LME010F, ByVal objNm As String, ByVal eventshubetsu As LME010C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.SetSeikyuResultByVal(frm, objNm, eventshubetsu)
        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット()
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            With frm

                Select Case objNm

                    Case frm.txtSeikyuCD.Name
                        frm.txtSeikyuCD.TextValue = dr.Item("SEIQTO_CD").ToString    '請求先コード
                        frm.lblSeikyuNM.TextValue = dr.Item("SEIQTO_NM").ToString    '請求先名

                    Case frm.txtEditTxt.Name
                        frm.txtEditTxt.TextValue = dr.Item("SEIQTO_CD").ToString    '請求先コード
                        frm.lblEditNM.TextValue = dr.Item("SEIQTO_NM").ToString     '請求先名

                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">コントロール</param>
    ''' <remarks></remarks>
    Private Function SetSeikyuResultByVal(ByVal frm As LME010F, ByVal objNm As String, ByVal EventShubetsu As LME010C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Select Case objNm

                Case frm.txtSeikyuCD.Name

                    'START SHINOHARA 要望番号513
                    If EventShubetsu = LME010C.EventShubetsu.ENTER Then
                        'END SHINOHARA 要望番号513
                        .Item("SEIQTO_CD") = frm.txtSeikyuCD.TextValue.Trim()
                        'START SHINOHARA 要望番号513
                    End If
                    'END SHINOHARA 要望番号513
                Case frm.txtEditTxt.Name

                    'START SHINOHARA 要望番号513
                    If EventShubetsu = LME010C.EventShubetsu.ENTER Then
                        'END SHINOHARA 要望番号513
                        .Item("SEIQTO_CD") = frm.txtEditTxt.TextValue.Trim()
                        'START SHINOHARA 要望番号513
                    End If
                    'END SHINOHARA 要望番号513
            End Select

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue().ToString()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMEconH.FormShow(ds, Me._PopupSkipFlg, "LMZ220")

    End Function

#End Region

#Region "作業項目マスタ"

    ''' <summary>
    ''' 作業項目マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoKmkPop(ByVal frm As LME010F, ByVal objNm As String, ByVal eventshubetsu As LME010C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowSagyoKmkPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット()
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)

            ctl.TextValue = dr.Item("SAGYO_CD").ToString    '作業コード
            frm.lblSagyoNM.TextValue = dr.Item("SAGYO_NM").ToString    '作業名称

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal frm As Form, ByVal objNm As String) As Win.InputMan.LMImTextBox
        Return DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)
    End Function


    ''' <summary>
    ''' 作業項目マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowSagyoKmkPopup(ByVal frm As LME010F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LME010C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue().ToString()
            .Item("CUST_CD_L") = frm.txtCustCD_L.TextValue.Trim()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LME010C.EventShubetsu.ENTER Then
                .Item("SAGYO_CD") = ctl.TextValue.Trim()
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SAGYO_CNT") = "1"

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMEconH.FormShow(ds, Me._PopupSkipFlg, "LMZ200")
    End Function

#End Region

#Region "初期荷主変更"

    ''' <summary>
    ''' 初期荷主変更参照POP起動
    ''' </summary>
    ''' <param name="frm">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDefPopup(ByVal frm As LME010F) As LMFormData

        Dim ds As DataSet = New LMZ010DS()
        Dim dt As DataTable = ds.Tables(LMZ010C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Dim prm As LMFormData = Me._LMEconH.FormShow(ds, Me._PopupSkipFlg, "LMZ010")

        '戻り処理
        If prm.ReturnFlg = True Then
            With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                frm.txtCustCD_L.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                frm.txtCustCD_M.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                frm.lblCustNM_L.TextValue = .Item("CUST_NM_L").ToString    '荷主名（大）
                frm.lblCustNM_M.TextValue = .Item("CUST_NM_M").ToString    '荷主名（中）
            End With
        End If

        Return prm

    End Function

#End Region


    ''' <summary>
    ''' 行複写時、作業項目マスタ参照POP起動
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ShowSagyoPopup(ByVal frm As LME010F, ByVal nrsBrCd As String, ByVal custCd As String) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = nrsBrCd
            .Item("CUST_CD_L") = custCd
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SAGYO_CNT") = "1"
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        'Return Me.PopFormShow(prm, "LMZ200")
        LMFormNavigate.NextFormNavigate(Me, "LMZ200", prm)

        Return prm

    End Function

#Region "ROW_NO取得"
    ''' <summary>
    ''' ROW_NO取得
    ''' </summary>
    ''' <param name="rowNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDrFromRowNo(ByVal rowNo As Integer) As Integer
        Dim dt As DataTable = _DsSel.Tables(LME010C.TABLE_NM_INOUT)
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            If Convert.ToInt32(dt.Rows(i)("ROW_NO")) = rowNo Then
                Return i
            End If
        Next
    End Function
#End Region

#Region "スプレッド行追加"

    ''' <summary>
    ''' スプレッドの行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub AddSpread(ByVal frm As LME010F)

        Dim rowNo As Integer = _DsSel.Tables(LME010C.TABLE_NM_INOUT).Rows.Count - 1
        Dim dr As DataRow = _DsSel.Tables(LME010C.TABLE_NM_INOUT).Rows(rowNo)

        Call Me._G.AddSpread(frm, dr)

    End Sub
#End Region

#Region "スプレッド行削除"
    ''' <summary>
    ''' スプレッドの行削除
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub DelSpread(ByVal frm As LME010F)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim selectRow As Integer = 0
        Dim max As Integer = chkList.Count - 1

        With frm.sprSagyo.ActiveSheet

            For i As Integer = max To 0 Step -1
                selectRow = Convert.ToInt32(chkList(i))
                .RemoveRows(selectRow, 1)
            Next

        End With

    End Sub
#End Region

#Region "データセットの中から削除したデータを削除"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' データセットの中から削除したデータを削除
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub DelDataSet(ByVal frm As LME010F)

        Dim outDr As DataRow() = Me._DsSel.Tables(LME010C.TABLE_NM_INOUT).Select("SYS_DEL_FLG = '1'")
        Dim max As Integer = outdr.length - 1

        For i As Integer = 0 To max
            Me._DsSel.Tables(LME010C.TABLE_NM_INOUT).Rows.Remove(outDr(i))
        Next

    End Sub
    'END YANAI 20120319　作業画面改造
#End Region

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LME010_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LME010_GUIERROR").Rows(i)

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

#Region "EXCEL出力処理"
    Private Sub OutputExcel(ByVal frm As LME010F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

#End Region

#Region "削除処理"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME010BLF", "DeleteData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        Return rtnDs

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "新規ボタン押下時"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 新規押下時、作業ポップを呼び出し⇒作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLME020sinki(ByVal frm As LME010F, ByVal objNm As String, ByVal eventshubetsu As LME010C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowSagyoKmkPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをLME020のDatasetInに設定
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)

            'inputDataSet作成
            Dim ds As DataSet = New LME020DS()
            Dim indr As DataRow = ds.Tables(LME020C.TABLE_NM_IN).NewRow()
            indr.Item("NRS_BR_CD") = dr.Item("NRS_BR_CD")                '営業所コード
            indr.Item("SAGYO_REC_NO") = String.Empty                     '作業レコード番号
            indr.Item("WH_CD") = frm.cmbWare.SelectedValue               '倉庫コード
            indr.Item("SAGYO_CD") = dr.Item("SAGYO_CD")                  '作業コード
            indr.Item("SAGYO_NM") = dr.Item("SAGYO_NM")                  '作業名
            indr.Item("CUST_CD_L") = dr.Item("CUST_CD_L")                '荷主コード(大)
            'SHINODA 要望管理2168
            If frm.txtCustCD_M.TextValue.Equals(String.Empty) = False Then

                Dim drTemp As DataRow() = Nothing

                '荷主存在チェック
                '20160621 tsunehira 要番2941 add start 
                drTemp = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                                   "NRS_BR_CD = '", frm.cmbEigyo.SelectedValue.ToString(), "' AND " _
                                                                                 , "CUST_CD_L = '", frm.txtCustCD_L.TextValue, "' AND " _
                                                                                 , "CUST_CD_M = '", frm.txtCustCD_M.TextValue, "' AND " _
                                                                                 , "SYS_DEL_FLG = '0'"))
                '20160621 tsunehira 要番2941 add end

                'drTemp = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                '                                                                   "CUST_CD_L = '", frm.txtCustCD_L.TextValue, "' AND " _
                '                                                                 , "CUST_CD_M = '", frm.txtCustCD_M.TextValue, "' AND " _
                '                                                                 , "SYS_DEL_FLG = '0'"))

                If 0 < drTemp.Length Then
                    '荷主コードMが存在する
                    indr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString() + frm.txtCustCD_M.TextValue
                End If

            End If
            'SHINODA 要望管理2168
            indr.Item("RENZOKU_FLG") = "00"                              '連続入力フラグ
            'START YANAI 要望番号875
            indr.Item("SAGYO_UP") = dr.Item("SAGYO_UP")                  '単価
            'END YANAI 要望番号875
            ds.Tables(LME020C.TABLE_NM_IN).Rows.Add(indr)


            Dim prmDs As DataSet = ds
            prm.ParamDataSet = prmDs
            prm.RecStatus = RecordStatus.NEW_REC

            '画面遷移
            LMFormNavigate.NextFormNavigate(Me, "LME020", prm)

            Return True

        End If

        Return False

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "行複写時"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 行複写時、作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLME020copy(ByVal frm As LME010F) As Boolean

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim rowNo As Integer = Convert.ToInt32(chkList(0))

        'inputDataSet作成
        Dim ds As DataSet = New LME020DS()
        Dim indr As DataRow = ds.Tables(LME020C.TABLE_NM_IN).NewRow()

        With frm.sprSagyo.ActiveSheet
            indr.Item("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(rowNo, LME010G.sprDetailDef.NRS_BR_CD.ColNo)) '営業所
            indr.Item("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(rowNo, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo)) '作業レコード番号
            indr.Item("WH_CD") = String.Empty '倉庫コード
            indr.Item("SAGYO_CD") = String.Empty '作業コード
            indr.Item("SAGYO_NM") = String.Empty '作業名
            indr.Item("CUST_CD_L") = String.Empty '荷主コード(大)
            indr.Item("RENZOKU_FLG") = "00" '連続入力フラグ
            ds.Tables(LME020C.TABLE_NM_IN).Rows.Add(indr)
        End With

        Dim prmDs As DataSet = ds
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.COPY_REC

        'モーダレスなので画面ロック必要なし
        Call Me._LMEconH.EndAction(frm) '終了処理

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LME020", prm)

        MyBase.ShowMessage(frm, "G007")

        Return True

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "スプレッドダブルクリック時"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' ダブルクリック時、作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLME020henshu(ByVal frm As LME010F) As Boolean

        Dim rowNo As Integer = frm.sprSagyo.Sheets(0).ActiveRow.Index '選択行番号

        'inputDataSet作成
        Dim ds As DataSet = New LME020DS()
        Dim indr As DataRow = ds.Tables(LME020C.TABLE_NM_IN).NewRow()

        With frm.sprSagyo.ActiveSheet
            indr.Item("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(rowNo, LME010G.sprDetailDef.NRS_BR_CD.ColNo)) '営業所
            indr.Item("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(rowNo, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo)) '作業レコード番号
            indr.Item("WH_CD") = String.Empty '倉庫コード
            indr.Item("SAGYO_CD") = String.Empty '作業コード
            indr.Item("SAGYO_NM") = String.Empty '作業名
            indr.Item("CUST_CD_L") = String.Empty '荷主コード(大)
            indr.Item("RENZOKU_FLG") = "00" '連続入力フラグ
            ds.Tables(LME020C.TABLE_NM_IN).Rows.Add(indr)
        End With

        Dim prmDs As DataSet = ds
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        'モーダレスなので画面ロック必要なし
        Call Me._LMEconH.EndAction(frm) '終了処理

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LME020", prm)

        MyBase.ShowMessage(frm, "G007")

        Return True

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "連続ボタン押下時"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 連続ボタン押下時、作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLME020renzoku(ByVal frm As LME010F) As Boolean

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count - 1

        'inputDataSet作成
        Dim ds As DataSet = New LME020DS()
        Dim indr As DataRow = ds.Tables(LME020C.TABLE_NM_IN).NewRow()

        With frm.sprSagyo.ActiveSheet

            For i As Integer = 0 To max
                indr = ds.Tables(LME020C.TABLE_NM_IN).NewRow()

                indr.Item("NRS_BR_CD") = Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(chkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo)) '営業所
                indr.Item("SAGYO_REC_NO") = Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(chkList(i)), LME010G.sprDetailDef.SAGYO_REC_NO.ColNo)) '作業レコード番号
                indr.Item("WH_CD") = String.Empty '倉庫コード
                indr.Item("SAGYO_CD") = String.Empty '作業コード
                indr.Item("SAGYO_NM") = String.Empty '作業名
                indr.Item("CUST_CD_L") = String.Empty '荷主コード(大)
                indr.Item("RENZOKU_FLG") = "01" '連続入力フラグ

                ds.Tables(LME020C.TABLE_NM_IN).Rows.Add(indr)

            Next

        End With

        Dim prmDs As DataSet = ds
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        'モーダレスなので画面ロック必要なし
        Call Me._LMEconH.EndAction(frm) '終了処理

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LME020", prm)

        MyBase.ShowMessage(frm, "G007")

        Return True

    End Function
    'END YANAI 20120319　作業画面改造

#End Region

#End Region

#Region "四捨五入"
    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="decValue"></param>
    ''' <param name="iDigits"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToRound(ByVal decValue As Decimal, ByVal iDigits As Integer) As Decimal

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If decValue > 0 Then
            Return Convert.ToDecimal(Math.Floor((decValue * dCoef) + 0.5) / dCoef)
        Else
            Return Convert.ToDecimal(Math.Ceiling((decValue * dCoef) - 0.5) / dCoef)
        End If
    End Function
#End Region

#End Region

#Region "印刷"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function PrintAction(ByVal frm As LME010F) As Boolean

        Dim eventShubetsu As LME010C.EventShubetsu = LME010C.EventShubetsu.PRINT

        '処理開始アクション
        Call Me._LMEconH.StartAction(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMEconH.EndAction(frm)
            Return False
        End If

        'If Convert.ToString(frm.cmbEigyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString() Then
        '    '営業所＋自営業所
        '    MyBase.ShowMessage(frm, "E178", New String() {"印刷"})
        '    Call Me._LMEconH.EndAction(frm)
        '    Return False
        'End If

        'START YANAI 20120319　作業画面改造
        ''未保存データがある場合、処理終了
        'If Me._FlgSave = True Then
        '    MyBase.ShowMessage(frm, "E346", New String() {"印刷"})
        '    Call Me._LMEconH.EndAction(frm)
        '    Return False
        'End If
        'END YANAI 20120319　作業画面改造

        frm.lblCustNM_L.TextValue = String.Empty
        frm.lblCustNM_M.TextValue = String.Empty
        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        Dim chkDs As DataSet = Me.GetChkDs(frm)

        '入力チェック
        If Me._V.IsPrintInputCheck() = False Then
            Call Me._LMEconH.EndAction(frm) '終了処理
            Return False
        End If

        If Me._V.IsPrintKanrenCheck(chkDs) = False Then
            Call Me._LMEconH.EndAction(frm) '終了処理
            Return False
        End If

        '印刷処理（作業明細書）
        Dim lme500Ds As DataSet = MyBase.CallWSA("LME010BLF", "PrintAction", Me.SetLME500InDataSet(frm))

        If IsMessageExist() = True Then

            MyBase.ShowMessage(frm)
            Me.ShowMessage(frm, "G007")
            Call Me._LMEconH.EndAction(frm) '終了処理
            Return False

        End If

        'プレビュー判定 
        Dim prevDt As DataTable = lme500Ds.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        '2016.01.06 UMANO 英語化対応START
        '終了メッセージ表示
        'MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})
        MyBase.ShowMessage(frm, "G002", New String() {frm.btnPrint.Text(), ""})
        '2016.01.06 UMANO 英語化対応END

        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        '処理終了アクション
        Call Me._LMEconH.EndAction(frm) '終了処理
        Return True

    End Function

    ''' <summary>
    ''' 印刷用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLME500InDataSet(ByVal frm As LME010F) As DataSet

        Dim inDs As DSL.LME500DS = New DSL.LME500DS
        Dim dt As DataTable = inDs.Tables("LME500IN")
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue()
            dr.Item("WH_CD") = .cmbWare.SelectedValue()
            dr.Item("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            dr.Item("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()
            dr.Item("SEIQTO_CD") = .txtSeikyuCD.TextValue.Trim()
            dr.Item("SAGYO_CD") = .txtSagyoCD.TextValue.Trim()
            dr.Item("SAGYO_SIJI_NO") = .txtSagyoSijiNO.TextValue.Trim()
            dr.Item("F_DATE") = Format(.imdSagyoDate_S.Value, "yyyyMMdd")
            dr.Item("T_DATE") = Format(.imdSagyoDate_E.Value, "yyyyMMdd")
            '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
            dr.Item("PRT_SHUBETU") = .cmbPrint.SelectedValue()
            '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END

        End With

        dt.Rows.Add(dr)

        inDs.Merge(New RdPrevInfoDS)

        Return inDs

    End Function

    Private Function GetChkDs(ByVal frm As LME010F) As DataSet

        Dim chkDs As DataSet = New LME010DS
        Dim inDr As DataRow = chkDs.Tables("LME010IN").NewRow()

        With frm
            inDr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue()
            inDr.Item("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            inDr.Item("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()
            inDr.Item("SEIQTO_CD") = .txtSeikyuCD.TextValue.Trim()
        End With

        chkDs.Tables("LME010IN").Rows.Add(inDr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectPrintCheck")
        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME010BLF", "SelectPrintCheck", chkDs)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectPrintCheck")

        Return rtnDs

    End Function

#End Region '印刷

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LME010F, ByVal e As FormClosingEventArgs)

        ''未保存データがない場合、処理終了
        'If Me._FlgSave = False Then
        '    Exit Sub
        'End If

        'START YANAI 20120319　作業画面改造
        ''メッセージの表示
        'Select Case MyBase.ShowMessage(frm, "W002")

        '    Case MsgBoxResult.Yes '「はい」押下時

        '        '保存処理
        '        If Me.ActionControl(LME010C.EventShubetsu.HOZON, frm) = False Then
        '            e.Cancel = True
        '        Else
        '            e.Cancel = False
        '        End If


        '    Case MsgBoxResult.Cancel '「キャンセル」押下時

        '        e.Cancel = True

        'End Select
        'END YANAI 20120319　作業画面改造

    End Sub

    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey1Press")

        '「新規」処理
        Call Me.ActionControl(LME010C.EventShubetsu.SINKI, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey4Press")

        '「連続入力」処理
        Call Me.ActionControl(LME010C.EventShubetsu.RENZOKU, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey4Press")

    End Sub
    'END YANAI 20120319　作業画面改造

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Kakutei")

        '「確定」処理
        Call Me.ActionControl(LME010C.EventShubetsu.KAKUTEI, frm)

        Logger.EndLog(Me.GetType.Name, "Kakutei")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Kaijo")

        '「確定」処理
        Call Me.ActionControl(LME010C.EventShubetsu.KAKUTEIKAIJO, frm)

        Logger.EndLog(Me.GetType.Name, "Kaijo")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "DefCust")

        '「初期荷主変更」処理
        Call Me.ActionControl(LME010C.EventShubetsu.DEF_CUST, frm)

        Logger.EndLog(Me.GetType.Name, "DefCust")

    End Sub

    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey8Press")

        '「完了」処理
        Call Me.ActionControl(LME010C.EventShubetsu.KANRYO, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey8Press")

    End Sub
    'END YANAI 20120319　作業画面改造

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Search")

        '検索処理
        Me.ActionControl(LME010C.EventShubetsu.KENSAKU, frm)

        Logger.EndLog(Me.GetType.Name, "Search")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Master")


        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False
            Me.ActionControl(LME010C.EventShubetsu.ENTER, frm)

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True
            Me.ActionControl(LME010C.EventShubetsu.MASTER, frm)
        End If

        '        Me.ActionControl(LME010C.EventShubetsu.MASTER, frm)

        Logger.EndLog(Me.GetType.Name, "Master")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Hozon")

        Me.ActionControl(LME010C.EventShubetsu.HOZON, frm)

        Logger.EndLog(Me.GetType.Name, "Hozon")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LME010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LME010F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    Friend Sub cmbEditList_SelectedValueChanged(ByVal frm As LME010F)

        '値のクリア
        Call Me.ClearControl(frm)

        'コントロールの設定
        Call Me._G.SetControl(frm)

    End Sub

    Friend Sub btnRowDel_Click(ByVal frm As LME010F)

        Call Me.ActionControl(LME010C.EventShubetsu.ROW_DEL, frm)

    End Sub

    Friend Sub btnRowCopy_Click(ByVal frm As LME010F)

        Call Me.ActionControl(LME010C.EventShubetsu.ROW_COPY, frm)

    End Sub

    Friend Sub btnAllChange_Click(ByVal frm As LME010F)

        Call Me.ActionControl(LME010C.EventShubetsu.HENKO, frm)

    End Sub

#End Region 'イベント振分け


#End Region 'Method

End Class