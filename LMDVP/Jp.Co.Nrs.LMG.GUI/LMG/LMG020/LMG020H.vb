' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG020H : 保管料/荷役料計算 [明細検索画面]
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base      '2021/06/28 
Imports Jp.Co.Nrs.LM.Utility    '2021/06/28 

''' <summary>
''' LMG020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMG020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG020G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGConV As LMGControlV

    '''' <summary>
    '''' Handler共通クラスを格納するフィールド
    '''' </summary>
    '''' <remarks></remarks>
    Private _LMGConH As LMGControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGConG As LMGControlG

    '画面間データを取得する
    Dim prmDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region

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
        prmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMG020F = New LMG020F(Me)

        '画面共通クラスの設定
        Me._LMGConG = New LMGControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._LMGConV = New LMGControlV(Me, DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMG020V(Me, frm, Me._LMGConV)

        'Gamenクラスの設定
        Me._G = New LMG020G(Me, frm, Me._LMGConG)

        'ハンドラー共通クラスの設定
        Me._LMGConH = New LMGControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMGConV, Me._LMGConG)

        'EnterKey判定用
        frm.KeyPreview = True

        'フォームの初期化
        Call Me.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID(), MyBase.GetSystemDateTime(0))


        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Me._G.SetInitValue(frm)

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

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMG020F, ByVal Row As Integer)

        'DoubleClick行判定SelectListData
        If Row.Equals(-1) = True _
        Or Row.Equals(0) = True Then
            Exit Sub
        End If

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG020C.EventShubetsu.DOUBLECLICK, Row) = False Then
            'エンドアクション
            Me._LMGConH.EndAction(frm)
            'コントロールロック処理
            MyBase.UnLockedControls(frm)
            Exit Sub
        End If

        'スタートアクション
        Me._LMGConH.StartAction(frm)

        'コントロールロック処理
        MyBase.LockedControls(frm)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        'スプレッドデータ設定（LMG030呼出）
        Call Me.getSpreadData(frm, Row, lc, mc)

        'エンドアクション
        Me._LMGConH.EndAction(frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        'コントロールロック解除処理
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMG020F)

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG020C.EventShubetsu.KENSAKU) = False Then
            Exit Sub
        End If

        '2011/08/02 菱刈 処理開始アクション追加 スタート
        'スタート処理
        Call Me._LMGConH.StartAction(frm)
        '2011/08/02 菱刈 処理開始アクション追加 エンド

        '画面項目全ロック
        MyBase.LockedControls(frm)

        'DataSet設定
        Dim ds As DataSet = New DataSet
        Call Me.SetSearchData(frm)

        'SPREAD(表示行)初期化
        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMGConH.CallWSAAction(DirectCast(frm, Form), _
                                                 "LMG020BLF", "SelectListData", prmDs _
                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                          , Convert.ToInt32(Convert.ToDouble( _
                                             MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))


        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            'データテーブルの取得
            Dim dt As DataTable = rtnDs.Tables(LMG020C.TABLE_NM_OUT)
            If "0".Equals(dt.Rows.Count.ToString()) = False Then

                'スプレッドデータをクリアする
                frm.sprMeisai.CrearSpread()

                '取得データをスプレッドに反映
                Call Me._G.SetSelectListData(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G008", New String() {dt.Rows.Count.ToString()})
            Else
                'スプレッドデータをクリアする
                frm.sprMeisai.CrearSpread()
            End If
        End If

        frm.lblCustNm.TextValue = String.Empty

        frm.lblSeqtoNm.TextValue = String.Empty

        '荷主名称・請求先名称の設定
        Call Me._G.SetCustSeqName()

        '画面ロック解除
        Call MyBase.UnLockedControls(frm)
    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMG020F, ByVal eventShubetsu As LMG020C.EventShubetsu)

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.MasterSelect(frm, eventShubetsu)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterSelect(ByVal frm As LMG020F, ByVal eventShubetsu As LMG020C.EventShubetsu)

        'スタートアクション
        Call Me._LMGConH.StartAction(frm)

        '権限・項目チェック
        'START SHINOHARA 要望番号513
        'If Me.IsCheckCall(frm, LMG020C.EventShubetsu.MASTER) = False Then
        '    Call Me._LMGConH.EndAction(frm) '終了処理
        '    Exit Sub
        'End If
        'END SHINOHARA 要望番号513		

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        '現在フォーカスのコントロール名がNullまたはブランクの場合
        If String.IsNullOrEmpty(objNm) = True Then
            'メッセージの設定
            MyBase.ShowMessage(frm, "G005")
            '全画面ロック解除
            Call MyBase.UnLockedControls(frm)
            Exit Sub
        End If
        With frm
            '参照PopUpの判定処理
            Select Case objNm
                Case .txtCustCdL.Name, .txtCustCdM.Name _
                   , .txtCustCdS.Name, .txtCustCdSs.Name                      '荷主コード（L・M・S・Ss）の場合

                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtCustCdM.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtCustCdS.TextValue) = True _
                    AndAlso String.IsNullOrEmpty(.txtCustCdSs.TextValue) = True Then

                        '荷主名称のクリア
                        .lblCustNm.TextValue = String.Empty

                    End If

                    '荷主マスタ
                    Call Me.ShowCustPopup(frm, objNm, prm, eventShubetsu)

                Case .txtSekySaki.Name                                        '請求先コードの場合

                    If String.IsNullOrEmpty(.txtSekySaki.TextValue) = True Then

                        '請求先名称のクリア
                        .lblSeqtoNm.TextValue = String.Empty

                    End If
                    '請求先マスタ
                    Call Me.ShowSekyPopup(frm, objNm, prm, eventShubetsu)

                Case Else
                    '荷主コード(大・中）以外の場合はメッセージ表示
                    MyBase.ShowMessage(frm, "G005")

                    '全画面ロック解除
                    Call MyBase.UnLockedControls(frm)

                    Exit Sub
            End Select
        End With

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 荷主設定クリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub NinushiBtnClick(ByVal frm As LMG020F)

        Dim Item As String = String.Empty
        Dim num As Integer = 0

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG020C.EventShubetsu.SETCUST) = False Then
            'エンドアクション
            Me._LMGConH.EndAction(frm)
            Exit Sub
        End If

        Me._LMGConH.StartAction(frm)

        MyBase.LockedControls(frm)

        With frm.sprMeisai.ActiveSheet
            Dim max As Integer = .Rows.Count - 1
            For i As Integer = 0 To max

                Item = Me._LMGConV.GetCellValue(.Cells(i, LMG020G.sprMeisaiDef.DEF.ColNo))

                If Item.Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    num = i
                    Exit For
                End If
            Next
        End With

        'スプレッドよりコントロールへデータ設定
        Call Me._G.SetControlSpreadDataCust(num)

        MyBase.UnLockedControls(frm)

        Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 請求先設定クリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SeiqBtnClick(ByVal frm As LMG020F)

        Dim Item As String = String.Empty
        Dim num As Integer = 0
        Dim max As Integer = 0

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG020C.EventShubetsu.SETSEKY) = False Then
            'エンドアクション
            Me._LMGConH.EndAction(frm)
            Exit Sub
        End If

        Me._LMGConH.StartAction(frm)

        'コントロールロック処理
        MyBase.LockedControls(frm)

        With frm.sprMeisai.ActiveSheet
            max = .Rows.Count - 1
            For i As Integer = 0 To max

                Item = Me._LMGConV.GetCellValue(.Cells(i, LMG020G.sprMeisaiDef.DEF.ColNo))

                If Item.Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    num = i
                    Exit For
                End If
            Next
        End With

        'スプレッドよりコントロールへデータ設定
        Call Me._G.SetControlSpreadDataSeiq(num)

        MyBase.UnLockedControls(frm)

        Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 印刷ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintBtnClick(ByVal frm As LMG020F)

        ''印刷処理　TODO
        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG020C.EventShubetsu.PRINT) = False Then
            'エンドアクション
            Me._LMGConH.EndAction(frm)
            Exit Sub
        End If

        Me._LMGConH.StartAction(frm)

        'コントロールロック処理
        MyBase.LockedControls(frm)

      
        '印刷処理を行う。
        Me.Print(frm)

      
        MyBase.UnLockedControls(frm)

        Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Print(ByVal frm As LMG020F) As DataSet


        With frm
            Dim ds As DataSet = New LMG500DS
            Dim dt As DataTable = ds.Tables("LMG500IN")
            'START YANAI 要望番号603
            'Dim dr As DataRow = dt.NewRow
            Dim dr As DataRow = Nothing
            Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
            Dim rtnDs As DataSet = Nothing
            Dim arr As ArrayList = Me._LMGConH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG020C.SprColumnIndex.DEF)
            Dim max As Integer = arr.Count - 1
            'END YANAI 要望番号603

            'START YANAI 要望番号581
            'メッセージ情報を初期化する
            MyBase.ClearMessageStoreData()
            'END YANAI 要望番号581

            'START YANAI 要望番号603
            For i As Integer = 0 To max

                dr = dt.NewRow
                'END YANAI 要望番号603

                'モード条件を設定
                Select Case True
                    Case .optSeikyuC.Checked
                        dr("SEKY_FLG") = LMG020C.CHECK
                    Case .optSeikyuH.Checked
                        dr("SEKY_FLG") = LMG020C.HONBAN
                End Select

                Dim flg As String = String.Empty
                '担当者フラグにtrueであれば1
                If .chkSelectByNrsB.Checked = True Then
                    flg = "1"
                Else
                    flg = "0"
                End If

                '請求期間Toの設定
                Dim seiqto As String = .imdInvDate.TextValue.ToString
                Dim sime As String = .cmbSimebi.SelectedValue.ToString
                Dim matu As String = String.Empty
                Dim Seiqmatu As String = String.Empty
                matu = String.Concat(seiqto.Substring(0, 6), "01")
                matu = Convert.ToString(Date.Parse(Format(CInt(matu) _
                                                                        , "0000/00/00")).AddMonths(1).AddDays(-1))
                matu = matu.Replace("/", "").Substring(0, 8)


                '請求月＋締日
                Seiqmatu = String.Concat(seiqto.Substring(0, 6), sime)

                Dim num As Integer = 0

                dr("NRS_BR_CD") = .cmbBr.SelectedValue.ToString
                '担当者フラグ1(true)の時に設定
                If flg.Equals("1") Then

                    dr("USER_CD") = LMUserInfoManager.GetUserID

                End If

                '(2013.01.17)要望番号1762 -- START --
                ''START YANAI 要望番号603
                ''dr("CUST_CD_L") = .txtCustCdL.TextValue
                ''dr("CUST_CD_M") = .txtCustCdM.TextValue
                ''dr("CUST_CD_S") = .txtCustCdS.TextValue
                ''dr("CUST_CD_SS") = .txtCustCdSs.TextValue
                ''dr("SEIQTO_CD") = .txtSekySaki.TextValue
                'dr("CUST_CD_L") = Me._LMGConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMG020C.SprColumnIndex.CUST_CD_L))
                'dr("CUST_CD_M") = Me._LMGConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMG020C.SprColumnIndex.CUST_CD_M))
                'dr("CUST_CD_S") = Me._LMGConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMG020C.SprColumnIndex.CUST_CD_S))
                'dr("CUST_CD_SS") = Me._LMGConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMG020C.SprColumnIndex.CUST_CD_SS))
                'dr("SEIQTO_CD") = Me._LMGConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMG020C.SprColumnIndex.OYA_SEIQTO_CD))
                ''END YANAI 要望番号603
                ''締め区分が00の場合
                'If sime.Equals("00") = True Then
                '    dr("INV_DATE_TO") = matu
                'Else
                '    dr("INV_DATE_TO") = Seiqmatu

                'End If

                dr("JOB_NO") = Me._LMGConV.GetCellValue(frm.sprMeisai.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMG020C.SprColumnIndex.JOB_NO))
                '(2013.01.17)要望番号1762 --  END  --

                dr("TANTO_USER_FLG") = flg
                '2011/08/04 菱刈 プレビュー表示、印刷部数の設定 スタート
                'プレビューにチェックがついていたら1を設定
                Dim flgP As String = String.Empty
                If frm.chkMeisaiPrev.Checked = True Then
                    'プレビューを表示する。
                    flgP = "1"
                Else
                    flgP = "0"

                End If
                dr("PREVIEW_FLG") = flgP
                dr("PRT_NB") = .numPrintCnt.TextValue.ToString

                '2011/08/04 菱刈 プレビュー表示、印刷部数の設定 エンド

                'START YANAI 要望番号581
                dr("SPREAD_GYO_CNT") = Convert.ToInt32(arr(i))
                dr("FROM_PGID") = MyBase.GetPGID
                'END YANAI 要望番号581

                ds.Tables("LMG500IN").Rows.Add(dr)

                '印刷のBLFへ
                'START YANAI 要望番号603
                'Dim blf As String = String.Concat(MyBase.GetPGID(), LMFControlC.BLF)
                'END YANAI 要望番号603

                'START YANAI 要望番号581
                'ds.Merge(New RdPrevInfoDS)
                'END YANAI 要望番号581

                'START YANAI 要望番号603
                'Dim rtnDs As DataSet = MyBase.CallWSA(blf, "Print", ds)
                'START YANAI 要望番号581
                'rtnDs = MyBase.CallWSA(blf, "Print", ds)
                'END YANAI 要望番号581
                'END YANAI 要望番号603

                'START YANAI 要望番号581
                'メッセージ判定
                'If IsMessageExist() = True Then

                '    'エラーメッセージ判定
                '    If MyBase.IsErrorMessageExist() = False Then


                '        ''処理終了アクション
                '        '印刷処理でエラーメッセージあったらメッセージを表示してG007を設定
                '        '2011/08/02 菱刈 メッセージの変更 スタート
                '        MyBase.ShowMessage(frm)
                '        MyBase.ShowMessage(frm, "G007")
                '        'MyBase.ShowMessage(frm, "S001", New String() {"印刷"})
                '        '2011/08/02 菱刈 メッセージの変更 エンド
                '        Return ds

                '    End If
                'End If
                'END YANAI 要望番号581

                'START YANAI 要望番号603
            Next
            'END YANAI 要望番号603

            'START YANAI 要望番号581
            ds.Merge(New RdPrevInfoDS)
            rtnDs = MyBase.CallWSA(blf, "Print", ds)

            If MyBase.IsMessageStoreExist = True Then
                'EXCEL起動()
                MyBase.MessageStoreDownload()
                Return ds
            End If

            'メッセージ判定
            If IsMessageExist() = True Then

                'エラーメッセージ判定
                If MyBase.IsErrorMessageExist() = False Then

                    ''処理終了アクション
                    '印刷処理でエラーメッセージあったらメッセージを表示してG007を設定
                    MyBase.ShowMessage(frm)
                    MyBase.ShowMessage(frm, "G007")
                    Return ds

                End If

            End If
            'END YANAI 要望番号581

            'プレビュー判定 
            Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
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

            '終了メッセージ表示
            MyBase.SetMessage("G002", New String() {"印刷", ""})



            MyBase.ShowMessage(frm)


            'Return ds
            Return ds
        End With




    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMG020F) As Boolean

        Return True

    End Function

#End Region

#Region "内部処理"

    ''' <summary>
    ''' 権限・入力チェック
    ''' </summary>
    ''' <param name="SHUBETSU"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMG020F, ByVal SHUBETSU As LMG020C.EventShubetsu _
                                 , Optional ByVal Row As Integer = 0) As Boolean

        'フォームの背景色を初期化する
        Me._G.SetBackColor(frm)

        '背景色クリア
        Me._LMGConG.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(SHUBETSU) = False Then
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck(SHUBETSU) = False Then
            Return False
        End If

        'スプレッド入力チェック
        If SHUBETSU.Equals(LMG020C.EventShubetsu.KENSAKU) = True Then
            If Me._V.IsSpreadInputChk() = False Then
                Return False
            End If
        End If

        'START YANAI 要望番号603
        Dim arr As ArrayList = Me._LMGConH.GetCheckList(frm.sprMeisai.ActiveSheet, LMG020C.SprColumnIndex.DEF)
        '印刷時のスプレッド入力チェック
        If SHUBETSU.Equals(LMG020C.EventShubetsu.PRINT) = True Then
            If Me._V.IsSpreadInputPrintChk(arr) = False Then
                Return False
            End If
        End If
        'END YANAI 要望番号603

        '関連チェック
        If SHUBETSU.Equals(LMG020C.EventShubetsu.SETCUST) = True _
        Or SHUBETSU.Equals(LMG020C.EventShubetsu.SETSEKY) = True _
        Or SHUBETSU.Equals(LMG020C.EventShubetsu.KENSAKU) = True Then
            If Me._V.IsRelationChk(SHUBETSU) = False Then
                Return False
            End If
        End If

        If SHUBETSU.Equals(LMG020C.EventShubetsu.DOUBLECLICK) = True Then
            '請求フラグ判定処理
            If Me._V.IsSpreadSekyCheck(Row) = False Then
                Return False
            End If

            '移行データ判定処理
            If Me._V.IsSpreadIkoDataCheck(Row) = False Then
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowCustPopup(ByVal frm As LMG020F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMG020C.EventShubetsu)

        Dim prmDs As DataSet = New LMZ260DS()

        'パラメータ生成
        Dim dr As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow()
        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
        'START SHINOHARA 要望番号513
        If eventshubetsu = LMG020C.EventShubetsu.ENTER Then
            dr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            dr.Item("CUST_CD_S") = frm.txtCustCdS.TextValue
            dr.Item("CUST_CD_SS") = frm.txtCustCdSs.TextValue
        End If
        'END SHINOHARA 要望番号513		
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        'START YANAI 要望番号935
        dr.Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        'END YANAI 要望番号935
        prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '荷主マスタ照会(LMZ260)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                'PopUpから取得したデータをコントロールにセット
                frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()    '荷主コード大
                frm.txtCustCdM.TextValue = .Item("CUST_CD_M").ToString()    '荷主コード中
                frm.txtCustCdS.TextValue = .Item("CUST_CD_S").ToString()    '荷主コード大
                frm.txtCustCdSs.TextValue = .Item("CUST_CD_SS").ToString()
                frm.lblCustNm.TextValue = String.Concat(.Item("CUST_NM_L").ToString(), " " _
                                                        , .Item("CUST_NM_M").ToString(), " " _
                                                        , .Item("CUST_NM_S").ToString(), " " _
                                                        , .Item("CUST_NM_SS").ToString(), " ")
                'START YANAI 要望番号558
                frm.cmbSimebi.SelectedValue = .Item("CLOSE_KB").ToString()    '締日区分
                frm.imdInvDate.TextValue = Me._LMGConG.SetControlDate(.Item("HOKAN_NIYAKU_CALCULATION").ToString(), 0) '請求月
                'END YANAI 要望番号558
            End With
        End If

    End Sub

    ''' <summary>
    ''' 請求先マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowSekyPopup(ByVal frm As LMG020F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMG020C.EventShubetsu)

        Dim prmDs As DataSet = New LMZ220DS()

        'パラメータ生成
        Dim dr As DataRow = prmDs.Tables(LMZ220C.TABLE_NM_IN).NewRow()
        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
        'START SHINOHARA 要望番号513
        If eventShubetsu = LMG020C.EventShubetsu.ENTER Then
            dr.Item("SEIQTO_CD") = frm.txtSekySaki.TextValue
            dr.Item("SEIQTO_NM") = frm.lblSeqtoNm.TextValue
        End If
        'END SHINOHARA 要望番号513		
        dr.Item("CLOSE_KB") = frm.cmbSimebi.SelectedValue
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        prmDs.Tables(LMZ220C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '荷主マスタ照会(LMZ220)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            'PopUpから取得したデータをコントロールにセット
            frm.txtSekySaki.TextValue = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0).Item("SEIQTO_CD").ToString()    '荷主コード大
            frm.lblSeqtoNm.TextValue = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0).Item("SEIQTO_NM").ToString()
        End If

    End Sub

    ''' <summary>
    ''' EnterKey処理判定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Private Sub EnterkeyControl(ByRef frm As LMG020F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String, ByVal eventShubetsu As LMG020C.EventShubetsu)

        'マスタ検索フラグ
        Dim MasterFlg As Boolean = False

        If e.KeyCode = Keys.Enter Then
            Select Case controlNm
                Case frm.txtCustCdL.Name, frm.txtCustCdM.Name, frm.txtCustCdS.Name, frm.txtCustCdSs.Name    'カーソルが荷主コード(大、中、小、極小）の場合
                    '荷主コード（大）にデータが入力されていない場合
                    If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtCustCdS.TextValue) = False OrElse _
                    String.IsNullOrEmpty(frm.txtCustCdSs.TextValue) = False Then


                        MasterFlg = True

                        '2011/08/01 菱刈 検証一覧結果一覧 スタート
                    Else

                        '荷主コードがすべてブランクだったら名称のクリア
                        frm.lblCustNm.TextValue = String.Empty
                    End If
                    'Case frm.txtCustCdM.Name      'カーソルが荷主コード（中）の場合
                    '    '荷主コード（中）にデータが入力されていない場合
                    '    If String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = False Then

                    '        MasterFlg = True

                    '    End If
                    'Case frm.txtCustCdS.Name
                    '    '荷主コード（小）にデータが入力されていない場合
                    '    If String.IsNullOrEmpty(frm.txtCustCdS.TextValue) = False Then

                    '        MasterFlg = True
                    '    End If

                    'Case frm.txtCustCdSs.Name
                    '    '荷主コード（極小）にデータが入力されていない場合
                    '    If String.IsNullOrEmpty(frm.txtCustCdSs.TextValue) = False Then

                    '        MasterFlg = True
                    '    End If

                Case frm.txtSekySaki.Name
                    '請求先コードにデータが入力されていない場合
                    If String.IsNullOrEmpty(frm.txtSekySaki.TextValue) = False Then

                        MasterFlg = True
                    Else
                        '請求先コードがブランクの場合名称のクリア
                        frm.lblSeqtoNm.TextValue = String.Empty
                        '2011/08/01 菱刈 検証決壊一覧エンド
                    End If
                Case Else                      'カーソルが荷主コード、請求先コード以外の場合

                    'EnterKeyによるタブ遷移
                    frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
                    Exit Sub
            End Select

        End If

        'Popup参照判定
        Select Case MasterFlg

            Case True                                      'Trueの場合

                'マスタPopUp参照処理：１件時表示なし
                Me._PopupSkipFlg = False
                Call Me.MasterSelect(frm, eventShubetsu)

            Case False

                'EnterKeyによるタブ遷移
                frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
        End Select

    End Sub

#End Region

#Region "DataSet"

    ''' <summary>
    ''' 検索用データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchData(ByVal frm As LMG020F)


        Dim SEKY_DATE As String = String.Empty         '今回請求日
        Dim SHIMEBI As String = String.Empty           '締日
        Dim USER_FLG As String = String.Empty          '担当分のみ表示フラグ
        Dim MATSUJITU As String = "00"                 '締日区分（末日）
        Dim DTFMT As String = "0000/00/00"             '日付フォーマット
        Dim JIKKOUMODE As String = String.Empty        '実行モード

        'データテーブル
        Me.prmDs = New LMG020DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG020C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm

            Select Case True
                Case .optSeikyuC.Checked
                    JIKKOUMODE = LMG020C.CHECK
                Case .optSeikyuH.Checked
                    JIKKOUMODE = LMG020C.HONBAN
            End Select

            '今回請求月
            SEKY_DATE = .imdInvDate.TextValue.ToString()

            '締日
            SHIMEBI = .cmbSimebi.SelectedValue.ToString()

            If .imdInvDate.TextValue.Length <> 0 Then

                '今回請求日の設定
                If MATSUJITU.Equals(SHIMEBI) = True Then

                    '月末日の設定
                    SEKY_DATE = String.Concat(SEKY_DATE.Substring(0, 6), "01")
                    SEKY_DATE = Convert.ToString(Date.Parse(Format(CInt(SEKY_DATE) _
                                                                            , DTFMT)).AddMonths(1).AddDays(-1))
                    SEKY_DATE = SEKY_DATE.Substring(0, 10).Replace("/", "")
                Else

                    '請求月＋締日
                    SEKY_DATE = String.Concat(SEKY_DATE.Substring(0, 6), SHIMEBI)

                End If
            End If

            '担当分のみ表示フラグ設定
            If .chkSelectByNrsB.Checked = True Then
                USER_FLG = LMConst.FLG.ON
            Else
                USER_FLG = LMConst.FLG.OFF
            End If

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue                                                  '営業所コード
            dr.Item("USER_CD") = LMUserInfoManager.GetUserID                                             '実行ユーザーコード
            dr.Item("TANTO_USER_FLG") = USER_FLG                                                         '担当分のみ表示フラグ
            dr.Item("INV_DATE_TO") = SEKY_DATE                                                           '請求日
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue.ToString()                                      '荷主コード（大）
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue.ToString()                                      '荷主コード（中）
            '2011/08/01 菱刈 検証結果一覧(No4(荷主コード(大)が入力されているときは小、極小を設定しない))スタート
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True Then
                dr.Item("CUST_CD_S") = .txtCustCdS.TextValue.ToString()                                      '荷主コード（小）
                dr.Item("CUST_CD_SS") = .txtCustCdSs.TextValue.ToString()                                    '荷主コード（極小）
            End If
            '2011/08/01 菱刈 検証結果一覧(No4(荷主コード(大)が入力されているときは小、極小を設定しない)) エンド
            dr.Item("SEIQTO_CD") = .txtSekySaki.TextValue.ToString()                                     '請求先コード
            dr.Item("CLOSE_KB") = SHIMEBI                                                                '締め日区分
            dr.Item("SEKY_FLG") = JIKKOUMODE                                                             '請求フラグ

            'スプレッド入力データ取得
            With .sprMeisai.ActiveSheet
                dr.Item("CUST_NM") = Me._LMGConV.GetCellValue(.Cells(0, LMG020G.sprMeisaiDef.CUST_NM.ColNo))  '荷主名
                dr.Item("JOB_NO") = Me._LMGConV.GetCellValue(.Cells(0, LMG020G.sprMeisaiDef.SIKYU_JOB_NO.ColNo))    'JOB_NO
            End With
        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' LMG030呼出用データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub getSpreadData(ByVal frm As LMG020F, ByVal row As Integer _
                              , ByVal lc As Integer, ByVal mc As Integer)

        Dim prmDs As DataSet = New LMG030DS()
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim datatable As DataTable = prmDs.Tables(LMG030C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'スプレッドの内容取得
        With frm.sprMeisai.ActiveSheet

            dr.Item("JOB_NO") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.SIKYU_JOB_NO.ColNo))
            dr.Item("INV_DATE_TO") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.SEIKYU_DATE.ColNo))
            dr.Item("CUST_CD_L") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_L.ColNo))
            dr.Item("CUST_CD_M") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_M.ColNo))
            dr.Item("CUST_NM_L_M") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.CUST_NM_L_M.ColNo))
            dr.Item("CUST_CD_S") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_S.ColNo))
            dr.Item("CUST_CD_SS") = Me._LMGConV.GetCellValue(.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_SS.ColNo))

        End With

        '営業所の設定
        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()

        'ベトナム対応
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        dr.Item("LANG_FLG") = lgm.MessageLanguage()

        'データの設定
        datatable.Rows.Add(dr)

        prm.ParamDataSet = prmDs

        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectSeiq")

        MyBase.SetLimitCount(lc)
        MyBase.SetMaxResultCount(mc)

        '存在チェック
        rtnDs = MyBase.CallWSA("LMG020BLF", "SelectSeiq", prmDs)

        If MyBase.IsMessageExist() = False Then
            'LMG030呼出処理
            LMFormNavigate.NextFormNavigate(Me, "LMG030", prm)
        Else
            'メッセージエリアの設定
            MyBase.ShowMessage(frm)
        End If

    End Sub

    ''' <summary>
    ''' 保管料荷役料明細(LMG500)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub setPrint500(ByVal frm As LMG020F)

        'データテーブル
        'Me.prmDs = New LMG500DS()
        'Dim datatable As DataTable = Me.prmDs.Tables(LMG020C.TABLE_NM_IN)
        'Dim dr As DataRow = datatable.NewRow()

    End Sub

    ''' <summary>
    ''' 保管料荷役料明細(LMG501)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub setPrint501(ByVal frm As LMG020F)

        'データテーブル
        'Me.prmDs = New LMG501DS()
        'Dim datatable As DataTable = Me.prmDs.Tables(LMG020C.TABLE_NM_IN)
        'Dim dr As DataRow = datatable.NewRow()

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMG020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "ShowMaster")

        Me.OpenMasterPop(frm, LMG020C.EventShubetsu.MASTER)

        Logger.EndLog(Me.GetType.Name, "ShowMaster")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG020F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMG020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        'DBより該当データの取得処理
        Me.SelectListData(frm, e.Row)

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' Enterキー押下処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMG020F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        If e.KeyCode = Keys.Enter Then

            Me.EnterkeyControl(frm, e, controlNm, LMG020C.EventShubetsu.ENTER)

        End If
    End Sub

    ''' <summary>
    ''' 荷主設定ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnCustSet_Click(ByRef frm As LMG020F, ByVal e As System.EventArgs)

        Call Me.NinushiBtnClick(frm)

    End Sub

    ''' <summary>
    ''' 請求先設定ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnSeqtoSet_Click(ByRef frm As LMG020F, ByVal e As System.EventArgs)

        Call Me.SeiqBtnClick(frm)

    End Sub

    ''' <summary>
    ''' 印刷ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMG020F, ByVal e As System.EventArgs)

        Call Me.PrintBtnClick(frm)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class