' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 特殊荷主機能
'  プログラムID     :  LMI110H : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI110ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI110H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI110F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI110V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI110G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _InDs As DataSet

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

    ''' <summary>
    '''最大枝番格納フィールド(チェック時使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxEdaNo As Integer

    ''' <summary>
    '''最大枝番格納フィールド(設定時使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxEdaNoSet As Integer

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
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Me._Frm = New LMI110F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH("LMI110")

        'Gamenクラスの設定
        Me._G = New LMI110G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMI110V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        'ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '営業所は自営業所
        Me._G.SetcmbNrsBrCd()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'コントロール個別設定(EDI取込日、取込区分)
        Dim sysDate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetInitControl(Me.GetPGID(), Me._Frm, sysDate(0))

        '営業所コード、荷主コードをセット（日医工固定）
        Call Me._G.SetCustData()

        '荷主名をキャッシュよりセット
        Me.SetCachedNameCust(Me._Frm)

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI110C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent()

        '背景色クリア
        'Me._ControlG.SetBackColor(Me._Frm)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI110C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMIControlC.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.StartAction()

        '項目チェック
        If Me._V.IsMasterShowInputChk = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'Pop起動処理
        Call Me.ShowPopupControl(objNm, LMI110C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 商品M反映処理
    ''' </summary>
    ''' <param name="eventShubetu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveGoodsMEvent(ByVal eventShubetu As LMI110C.EventShubetsu) As Boolean

        Dim chkList As ArrayList = Me._V.getCheckList()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI110C.EventShubetsu.SAVEGOODSM) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目チェック
        If Me._V.IsSaveGoodsMChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'umano 追加START
        Dim errDs As DataSet = Nothing
        Dim errHashTable As Hashtable = New Hashtable

        Dim ds As DataSet = New LMI110DS()

        '関連チェック
        errHashTable = Me._V.IsSaveGoodsMRelationChk(ds)

        '全行エラーの場合処理終了
        If chkList.Count = errHashTable.Count Then

            If ds.Tables("LMI110_GUIERROR").Rows.Count <> 0 Then
                Call Me.ExcelErrorSet(ds)
                Call Me.OutputExcel(Me._Frm)
            End If

            Call Me._ControlH.EndAction(Me._Frm)
            Exit Function
        End If

        'DataSet設定
        'Dim ds As DataSet = New LMI110DS()
        'umano 追加END

        '商品マスタ用データセット設定
        Call Me.SetDataSetSaveGoodsM(ds, errHashTable)

        '要望番号:1250 terakawa 2012.07.12 Start
        '商品明細マスタ用データセット設定
        Call Me.SetDataSetSaveGoodsMDtl(ds)
        '要望番号:1250 terakawa 2012.07.12 End

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveGoodsMData")

        '要望番号1093 terakawa 20120530 Start
        'Dim ctl As Control() = Nothing
        'Dim focus As Control = Nothing
        'If Me.SaveCallBLF(ds, ctl, focus) = False Then
        '    Call Me._ControlH.EndAction(Me._Frm) '終了処理　
        '    Return False
        'End If

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMI110BLF", "SaveGoodsM", ds)
        '要望番号1093 terakawa 20120530 End

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveGoodsMData")

        '---↓
        ''キャッシュ最新化
        'Call Me.GetNewCache()
        '---↑

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(Me._Frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
        Else

            '完了メッセージ表示
            Call Me.SetCompleteMessage(LMI110C.EventShubetsu.SAVEGOODSM)

        End If

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        Dim chkList As ArrayList = Me._V.getCheckList()

        If chkList.Count = 0 Then

            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                'If Me.SaveEvent(LMI110C.EventShubetsu.TOJIRU) = False Then
                '商品M反映()
                Me.SaveGoodsMEvent(LMI110C.EventShubetsu.SAVEGOODSM)

                'End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            If Me._V.IsAuthorityChk(LMI110C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            'カーソル位置チェック
            If Me._V.IsFocusChk(objNm, LMIControlC.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            '処理開始アクション
            Call Me.StartAction()

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(objNm, LMI110C.ActionType.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction()

        'マスタメンテ共通処理
        Me._ControlH.StartAction(Me._Frm)

        '背景色クリア
        'Me._ControlG.SetBackColor(Me._Frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectData()

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '04'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)


        'DataSet設定
        Me._InDs = New LMI110DS()
        Call SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(DirectCast(Me._Frm, Form), "LMI110BLF", "SelectListData", Me._InDs, lc)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI110C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprNichikoGoods.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMI110C.TABLE_NM_OUT)
        Call Me._G.SetSpread(dt)

        '文字色を変更
        Call Me._G.SetSpreadColor(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprNichikoGoods.CrearSpread()

    End Sub

    '要望番号1093 terakawa 20120530 Start
    '''' <summary>
    '''' ワーニング表示時、強制フラグの設定
    '''' </summary>
    '''' <remarks></remarks>
    'Private Function SaveShowMessage(ByVal msgId As String _
    '                                 , ByVal ds As DataSet) As Boolean

    '    Dim dr As DataRow = ds.Tables(LMI110C.TABLE_NM_OUT).Rows(0)

    '    'メッセージを表示し、戻り値により処理を分ける
    '    If MyBase.ShowMessage(Me._Frm) = MsgBoxResult.Ok Then '「はい」を選択
    '        '強制実行フラグの設定
    '        Dim flg As String = LMConst.FLG.OFF
    '        Select Case msgId
    '            Case "W139"
    '                flg = "1"
    '            Case "W136"
    '                flg = "2"
    '            Case "W134"
    '                flg = "3"
    '            Case "W135"
    '                flg = "4"
    '            Case "W157"
    '                flg = "5"
    '        End Select
    '        dr.Item("WARNING_FLG") = flg
    '        Return False
    '    End If

    '    Return True

    'End Function


    '''' <summary>
    '''' 保存時BLF呼び出し
    '''' </summary>
    '''' <param name="ds"></param>
    '''' <param name="errorCtl">エラーコントロール</param>
    '''' <param name="focusCtl">フォーカス設定コントロール</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SaveCallBLF(ByVal ds As DataSet _
    '                           , ByRef errorCtl As Control() _
    '                           , ByRef focusCtl As Control _
    '                          ) As Boolean

    '    With Me._Frm

    '        '==========================
    '        'WSAクラス呼出
    '        '==========================
    '        ds = MyBase.CallWSA("LMI110BLF", "SaveGoodsM", ds)
    '        Dim msgId As String = MyBase.GetMessageID
    '        Dim warningFlg As Boolean = MyBase.IsWarningMessageExist

    '        'メッセージ未設定時、処理終了
    '        If String.IsNullOrEmpty(msgId) Then
    '            Return True
    '        End If

    '        'フォーカス、エラーコントロールの設定
    '        Select Case msgId
    '            Case Else

    '                focusCtl = Nothing
    '        End Select

    '        'エラーメッセージ設定時、メッセージ表示後処理終了
    '        If warningFlg = False Then
    '            MyBase.ShowMessage(Me._Frm)
    '            Return False
    '        End If

    '        'ワーニング表示、[OK]選択時
    '        If Me.SaveShowMessage(msgId, ds) = False Then
    '            If Me.SaveCallBLF(ds, errorCtl, focusCtl) = False Then
    '                Return False
    '            End If
    '        Else
    '            'メッセージエリアの設定
    '            Call Me._V.SetBaseMsg()
    '            Return False
    '        End If

    '        Return True

    '    End With

    'End Function
    '要望番号1093 terakawa 20120530 End


    ''' <summary>
    ''' 処理完了メッセージ
    ''' </summary>
    ''' <param name="eventShubetu">イベント種別</param>
    ''' <remarks></remarks>
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMI110C.EventShubetsu)

        With Me._Frm

            Dim shoriMsg As String = String.Empty

            If LMI110C.EventShubetsu.SAVEGOODSM = eventShubetu Then
                shoriMsg = "商品Ｍ反映"
            End If

            MyBase.ShowMessage(Me._Frm, "G002", New String() {shoriMsg, String.Empty})

        End With

    End Sub

    '---↓
    '''' <summary>
    '''' キャッシュ最新化処理
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub GetNewCache()

    '    'キャッシュ最新化
    '    MyBase.LMCacheMasterData(LMConst.CacheTBL.GOODS)
    '    'MyBase.LMCacheMasterData(LMConst.CacheTBL.GOODS_DETAILS)

    'End Sub
    '---↑

    ''' <summary>
    ''' 荷主名取得処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCachedNameCust(ByVal frm As LMI110F)

        With frm

            Dim custCdL As String = .txtCustCdL.TextValue
            Dim custCdM As String = .txtCustCdM.TextValue

            '荷主名称
            .txtCustNm.TextValue = String.Empty
            If String.IsNullOrEmpty(custCdL) = False Then
                If String.IsNullOrEmpty(custCdM) = True Then
                    custCdM = "00"
                End If

                Dim custDr() As DataRow = Me._ControlG.SelectCustListDataRow(custCdL, custCdM)

                If 0 < custDr.Length Then
                    .txtCustNm.TextValue = custDr(0).Item("CUST_NM_L").ToString()
                End If
            End If
        End With
    End Sub

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal actionType As LMI110C.ActionType) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtSerchGoodsCd.Name _
                   , .txtSerchGoodsNm.Name
                    '商品マスタ参照POP起動
                    Call Me.SetReturnGoodsPop(Me._Frm, actionType)

            End Select

        End With

        Return True

    End Function


#Region "商品マスタ"
    '''' <summary>
    '''' 商品マスタ戻り値設定
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetReturnGoodsPop(ByVal frm As LMB020F) As Boolean
    ''' <summary>
    ''' 商品マスタ戻り値設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMI110F, ByVal actionType As LMI110C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, actionType)


        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtSerchGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()
                .txtSerchGoodsNm.TextValue = dr.Item("GOODS_NM_1").ToString()
                .cmbTareYn.SelectedValue = dr.Item("TARE_YN").ToString()
                .cmbLotCtlKb.SelectedValue = dr.Item("LOT_CTL_KB").ToString()
                .txtUpGroupCd1.TextValue = dr.Item("UP_GP_CD_1").ToString()
                .cmbSpNhsYn.SelectedValue = dr.Item("SP_NHS_YN").ToString()
                .cmbCoaYn.SelectedValue = dr.Item("COA_YN").ToString()
                '.cmbLtDateCtlKb.SelectedValue = dr.Item("LT_DATE_CTL_KB").ToString()
                .cmbCrtDateCtlKb.SelectedValue = dr.Item("CRT_DATE_CTL_KB").ToString()
                .cmbHikiateAlertYn.SelectedValue = dr.Item("HIKIATE_ALERT_YN").ToString()
                .cmbSkyuMeiYn.SelectedValue = dr.Item("SKYU_MEI_YN").ToString()

                .txtGoodsCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtGoodsCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .txtGoodsCustCdS.TextValue = dr.Item("CUST_CD_S").ToString()
                .txtGoodsCustCdSS.TextValue = dr.Item("CUST_CD_SS").ToString()
                .txtGoodsCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .txtGoodsCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()
                .txtGoodsCustNmS.TextValue = dr.Item("CUST_NM_S").ToString()
                .txtGoodsCustNmSS.TextValue = dr.Item("CUST_NM_SS").ToString()

                .txtGoodsNm1.TextValue = dr.Item("GOODS_NM_1").ToString()
                .txtGoodsNm2.TextValue = dr.Item("GOODS_NM_2").ToString()
                .txtGoodsKey.TextValue = dr.Item("GOODS_CD_NRS").ToString()
                .txtGoodsCd.TextValue = dr.Item("GOODS_CD_CUST").ToString()

            End With

        End If

        Return True

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMI110F, ByVal actionType As LMI110C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            If (LMI110C.ActionType.ENTER).Equals(actionType) = True Then
                .Item("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
                .Item("GOODS_NM_1") = frm.txtSerchGoodsNm.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

    End Function
#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dr As DataRow = Me._InDs.Tables(LMI110C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprNichikoGoods.ActiveSheet

            '検索条件を設定
            dr.Item("TORIKOMI_DATE_FROM") = Me._Frm.imdEdiDateFrom.TextValue
            dr.Item("TORIKOMI_DATE_TO") = Me._Frm.imdEdiDateTo.TextValue

            If Me._Frm.chkImport.GetBinaryValue = "0" AndAlso _
                  Me._Frm.chkNotImport.GetBinaryValue = "1" Then
                dr.Item("TORIKOMI_KB") = LMI110C.NOT_IMPORT
            ElseIf Me._Frm.chkImport.GetBinaryValue = "1" AndAlso _
                  Me._Frm.chkNotImport.GetBinaryValue = "0" Then
                dr.Item("TORIKOMI_KB") = LMI110C.IMPORT
            End If

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            dr.Item("NRS_BR_CD") = Me._Frm.cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = Me._Frm.txtCustCdM.TextValue
            dr.Item("GOODS_CD_NIK") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.GOODS_CD.ColNo))
            dr.Item("GOODS_NM") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.GOODS_NM.ColNo))
            dr.Item("GOODS_NM_KANA") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.GOODS_NM_KANA.ColNo))
            dr.Item("GOODS_KIKAKU") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.GOODS_KIKAKU.ColNo))
            dr.Item("GOODS_KIKAKU_KANA") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.GOODS_KIKAKU_KANA.ColNo))
            dr.Item("JAN_CD") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.JAN_CD.ColNo))
            dr.Item("KANRI_KB") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.KANRI_KB_NM.ColNo))
            dr.Item("ONDO_KB") = Me._ControlV.GetCellValue(.Cells(0, LMI110G.sprNichikoGoods.ONDO_KB_NM.ColNo))

            Me._InDs.Tables(LMI110C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub
    ''' <summary>
    ''' データセット設定(商品Ｍ反映処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetSaveGoodsM(ByVal ds As DataSet, ByVal errHashTable As Hashtable)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count()
        Dim selectRow As Integer = 0
        Dim dr As DataRow
        Dim ondo As String = String.Empty
        Dim drs As DataRow()



        For i As Integer = 0 To max - 1

            If errHashTable.ContainsKey(i) Then
                Continue For
            End If

            selectRow = Convert.ToInt32(chkList(i))

            dr = ds.Tables(LMI110C.TABLE_NM_M_GOODS_HANEI).NewRow()


            With Me._Frm.sprNichikoGoods.ActiveSheet
                '排他処理条件を格納
                dr.Item("M_SEIHIN_SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.M_SEIHIN_SYS_UPD_DATE.ColNo))
                dr.Item("M_SEIHIN_SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.M_SEIHIN_SYS_UPD_TIME.ColNo))
                dr.Item("M_GOODS_SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.M_GOODS_SYS_UPD_DATE.ColNo))
                dr.Item("M_GOODS_SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.M_GOODS_SYS_UPD_TIME.ColNo))
                '登録項目格納
                '******************* スプレッド項目 *****************************
                dr.Item("STATUS") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.STATUS.ColNo))
                dr.Item("TORIKOMI_KB") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.TORIKOMI_KBN.ColNo))
                dr.Item("GOODS_CD_CUST") = Mid(Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_CD.ColNo)).ToString, 4, 6)
                dr.Item("JAN_CD") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.JAN_CD.ColNo))
                dr.Item("GOODS_NM_1") = String.Concat(Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_NM.ColNo)), "　", _
                                                      Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_KIKAKU.ColNo)))
                dr.Item("GOODS_NM_2") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_NM.ColNo))
                dr.Item("GOODS_NM_3") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_KIKAKU.ColNo))
                '温度区分、運送温度区分
                ondo = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.ONDO_KB.ColNo))
                If ondo = LMI110C.HOKAN_ONDO_SHITSUON Then
                    dr.Item("ONDO_KB") = LMI110C.ONDO_KANRI_JOON
                    dr.Item("UNSO_ONDO_KB") = LMI110C.UNSO_KANRI_NASHI
                ElseIf ondo = LMI110C.HOKAN_ONDO_HOREI Or ondo = LMI110C.HOKAN_ONDO_REIZO OrElse ondo = LMI110C.HOKAN_ONDO_REITO Then
                    dr.Item("ONDO_KB") = LMI110C.ONDO_KANRI_TEON
                    dr.Item("UNSO_ONDO_KB") = LMI110C.UNSO_KANRI_TEON
                Else
                    dr.Item("ONDO_KB") = String.Empty
                    dr.Item("UNSO_ONDO_KB") = LMI110C.UNSO_KANRI_NASHI
                End If

                dr.Item("NB_UT") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.NB_UT.ColNo))
                dr.Item("PKG_NB") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.PKG_NB.ColNo))
                dr.Item("PKG_UT") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.NB_UT.ColNo))
                dr.Item("STD_IRIME_NB") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_NB.ColNo))
                dr.Item("STD_IRIME_UT") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_UT.ColNo))
                dr.Item("STD_WT_KGS") = (Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_NB.ColNo))) / 1000).ToString
                'dr.Item("STD_CBM") = ((Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.NB_FORM_LENGTH.ColNo)))) * _
                '                     (Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.NB_FORM_WIDTH.ColNo)))) * _
                '                     (Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.NB_FORM_HEIGHT.ColNo)))) / 1000000000).ToString
                dr.Item("GOODS_CD_NRS") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_CD_NRS.ColNo))
                dr.Item("CRT_DATE_CTL_KB") = Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.YUKO_MONTH.ColNo))
                dr.Item("ROW_NO") = selectRow.ToString()
                '賞味期限管理区分
                If Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.YUKO_MONTH.ColNo)).ToString = "00" Then
                    dr.Item("LT_DATE_CTL_KB") = LMI110C.SHOMI_NASHI
                Else
                    dr.Item("LT_DATE_CTL_KB") = LMI110C.SHOMI_ARI
                End If

                '管理区分を元に、区分マスタから必要項目を抽出
                drs = Me._ControlV.SelectKbnListDataRow( _
                                       Me._ControlV.GetCellValue(.Cells(selectRow, LMI110G.sprNichikoGoods.KANRI_KB.ColNo)).ToString, LMKbnConst.KBN_I005)

                dr.Item("KIKEN_KB") = drs(0).Item("KBN_NM2")
                dr.Item("CHEM_MTRL_KB") = drs(0).Item("KBN_NM3")
                dr.Item("DOKU_KB") = drs(0).Item("KBN_NM4")
                dr.Item("KYOKAI_GOODS_KB") = drs(0).Item("KBN_NM5")

            End With
            '******************* 画面項目 *****************************
            With Me._Frm
                dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
                dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
                dr.Item("CUST_CD_S") = .txtGoodsCustCdS.TextValue
                dr.Item("CUST_CD_SS") = .txtGoodsCustCdSS.TextValue
                dr.Item("UP_GP_CD_1") = .txtUpGroupCd1.TextValue
                'dr.Item("SHOBO_CD") = .txtShoboCd.TextValue
                dr.Item("TARE_YN") = .cmbTareYn.SelectedValue
                dr.Item("SP_NHS_YN") = .cmbSpNhsYn.SelectedValue
                dr.Item("COA_YN") = .cmbCoaYn.SelectedValue
                dr.Item("LOT_CTL_KB") = .cmbLotCtlKb.SelectedValue
                dr.Item("CRT_DATE_CTL_KB") = .cmbCrtDateCtlKb.SelectedValue
                dr.Item("SKYU_MEI_YN") = .cmbSkyuMeiYn.SelectedValue
                dr.Item("HIKIATE_ALERT_YN") = .cmbHikiateAlertYn.SelectedValue
            End With

            '******************* 固定項目 *****************************
            dr.Item("ALCTD_KB") = LMI110C.KOSU
            dr.Item("ONDO_STR_DATE") = LMI110C.STR_DATE
            dr.Item("ONDO_END_DATE") = LMI110C.END_DATE
            dr.Item("ONDO_UNSO_STR_DATE") = LMI110C.STR_DATE
            dr.Item("ONDO_UNSO_END_DATE") = LMI110C.END_DATE
            dr.Item("PRINT_NB") = 1

            ds.Tables(LMI110C.TABLE_NM_M_GOODS_HANEI).Rows.Add(dr)
        Next



    End Sub

    '要望番号:1250 terakawa 2012.07.12 Start
    ''' <summary>
    ''' データセット設定(商品Ｍ反映処理：荷主明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetSaveGoodsMDtl(ByVal ds As DataSet)

        Dim dr As DataRow

        dr = ds.Tables(LMI110C.TABLE_NM_M_GOODS_DETAILS).NewRow()

        '******************* 商品明細マスタ（固定項目) *****************************
        dr.Item("NRS_BR_CD") = Me._Frm.cmbEigyo.SelectedValue
        dr.Item("GOODS_CD_NRS_EDA") = LMI110C.GOODS_CD_NRS_EDA_0     '枝番は、新規レコードのため"00"固定
        dr.Item("SUB_KB") = LMI110C.YOUTO_NYUKOBI_KANRRI             '用途区分は、入庫日管理区分で固定
        dr.Item("SET_NAIYO") = LMI110C.NYUKOBI_KANRI_NASHI           '設定値は、"1"固定(入庫日管理なし)
        dr.Item("REMARK") = String.Empty

        ds.Tables(LMI110C.TABLE_NM_M_GOODS_DETAILS).Rows.Add(dr)

    End Sub
    '要望番号:1250 terakawa 2012.07.12 End

#End Region

    'umano 追加START
#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMI110_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LMI110_GUIERROR").Rows(i)

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
    Private Sub OutputExcel(ByVal frm As LMI110F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub


#End Region

    'umano 追加END

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(商品M反映)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '商品M反映()
        Me.SaveGoodsMEvent(LMI110C.EventShubetsu.SAVEGOODSM)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI110F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI110FKeyDown(ByVal frm As LMI110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMI110F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMI110F_KeyDown")

    End Sub


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region
#End Region

End Class